Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpr_ingud_vs_inkantor

    Dim dvg As DataView
    Dim dvnopol As DataView

    Private Sub isi_barang()

        Const sql As String = "select kd_barang,nama_barang from ms_barang where jenis='FISIK'"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet


        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvg = dvm.CreateDataView(ds.Tables(0))

            Dim orow As DataRow = dvg.Table.NewRow
            orow("kd_barang") = "All"
            orow("nama_barang") = "All"
            dvg.Table.Rows.InsertAt(orow, 0)

            tbarang.Properties.DataSource = dvg

            If dvg.Count > 0 Then
                tbarang.ItemIndex = 0
            End If

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub isi_nopol()

        Const sql As String = "SELECT     ms_kendaraan.nopol " & _
            "FROM  ms_kendaraan WHERE ms_kendaraan.aktif=1"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvnopol = dvm.CreateDataView(ds.Tables(0))

            Dim orow As DataRow = dvnopol.Table.NewRow
            orow("nopol") = "All"
            dvnopol.Table.Rows.InsertAt(orow, 0)

            tnopol.Properties.DataSource = dvnopol

            tnopol.ItemIndex = 0

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fpr_ingud_vs_inkantor_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        cb1.Focus()
    End Sub

    Private Sub fpr_ingud_vs_inkantor_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        isi_barang()
        isi_nopol()


        ttgl.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

        cb1.SelectedIndex = 0

    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        Dim sql As String = ""

        If cb1.SelectedIndex = 1 Then
            sql = String.Format("select tradm_gud.nopol,tradm_gud.tglberangkat as tanggal,hbarang_kendaraan.kd_barang,ms_barang.nama_lap as nama_barang,(hbarang_kendaraan.jmlin / (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3)) as jmlin ,(hbarang_kendaraan.jmlout /  (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3)) as jmlout ,0 as jmlin_k,0 as jmlout_k " & _
              "from hbarang_kendaraan inner join tradm_gud on hbarang_kendaraan.nobukti=tradm_gud.nobukti " & _
              "inner join ms_barang on hbarang_kendaraan.kd_barang=ms_barang.kd_barang " & _
              "where tradm_gud.tglberangkat>='{0}' and tradm_gud.tglberangkat<='{1}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))
        Else
            sql = String.Format("select tradm_gud.nopol,hbarang_kendaraan.tanggal as tanggal,hbarang_kendaraan.kd_barang,ms_barang.nama_lap as nama_barang,(hbarang_kendaraan.jmlin / (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3)) as jmlin ,(hbarang_kendaraan.jmlout /  (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3)) as jmlout,0 as jmlin_k,0 as jmlout_k " & _
              "from hbarang_kendaraan inner join tradm_gud on hbarang_kendaraan.nobukti=tradm_gud.nobukti " & _
              "inner join ms_barang on hbarang_kendaraan.kd_barang=ms_barang.kd_barang " & _
              "where hbarang_kendaraan.tanggal>='{0}' and hbarang_kendaraan.tanggal<='{1}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))
        End If

        If Not tnopol.EditValue.ToString.Equals("All") Then
            sql = String.Format(" {0} and tradm_gud.nopol='{1}'", sql, tnopol.EditValue)
        End If

        If Not tbarang.EditValue.ToString.Equals("All") Then
            sql = String.Format(" {0} and hbarang_kendaraan.kd_barang='{1}'", sql, tbarang.EditValue)
        End If

        sql = String.Format(" {0} union all ", sql)

        If cb1.SelectedIndex = 1 Then
            sql = String.Format(" {0} select v_bukti_tglkirim_all.nopol,v_bukti_tglkirim_all.tanggal,hbarang_gudang.kd_barang,ms_barang.nama_lap as nama_barang,0,0,(hbarang_gudang.jmlin / (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3)) as jmlin,(hbarang_gudang.jmlout / (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3)) as jmlout " & _
              "from hbarang_gudang inner join v_bukti_tglkirim_all on hbarang_gudang.nobukti=v_bukti_tglkirim_all.nobukti " & _
              "inner join ms_barang on hbarang_gudang.kd_barang=ms_barang.kd_barang " & _
              "where hbarang_gudang.kd_gudang in (select kd_gudang from ms_gudang where tipe_gudang='FISIK') and not(kd_gudang like 'None%') " & _
              "and v_bukti_tglkirim_all.tanggal>='{1}' and v_bukti_tglkirim_all.tanggal<='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))
        Else
            sql = String.Format(" {0} select v_bukti_tglkirim_all.nopol,hbarang_gudang.tanggal,hbarang_gudang.kd_barang,ms_barang.nama_lap as nama_barang,0,0,(hbarang_gudang.jmlin / (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3)) as jmlin,(hbarang_gudang.jmlout / (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3)) as jmlout " & _
              "from hbarang_gudang inner join v_bukti_tglkirim_all on hbarang_gudang.nobukti=v_bukti_tglkirim_all.nobukti " & _
              "inner join ms_barang on hbarang_gudang.kd_barang=ms_barang.kd_barang " & _
              "where hbarang_gudang.kd_gudang in (select kd_gudang from ms_gudang where tipe_gudang='FISIK') and not(kd_gudang like 'None%') " & _
              "and hbarang_gudang.tanggal>='{1}' and hbarang_gudang.tanggal<='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))
        End If

        If Not tnopol.EditValue.ToString.Equals("All") Then
            sql = String.Format(" {0} and v_bukti_tglkirim_all.nopol='{1}'", sql, tnopol.EditValue)
        End If

        If Not tbarang.EditValue.ToString.Equals("All") Then
            sql = String.Format(" {0} and hbarang_gudang.kd_barang='{1}'", sql, tbarang.EditValue)
        End If

        Dim periode As String = String.Format("Periode : {0} s.d {1}", convert_date_to_ind(ttgl.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Cursor = Cursors.WaitCursor

        Dim fs As New fpr_ingud_vs_inkantor2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .periode = periode}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub

End Class