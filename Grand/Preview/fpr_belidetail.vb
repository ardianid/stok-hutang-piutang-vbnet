Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpr_belidetail

    Private Sub isi_nopol()

        Const sql As String = "select * from ms_kendaraan where aktif=1"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet
        Dim dvg As DataView

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvg = dvm.CreateDataView(ds.Tables(0))

            Dim orow As DataRow = dvg.Table.NewRow
            orow("nopol") = "All"
            orow("tipe") = "All"
            dvg.Table().Rows.InsertAt(orow, 0)

            tnopol.Properties.DataSource = dvg

            If dvg.Count > 0 Then
                tnopol.ItemIndex = 0
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

    '' supir

    Private Sub bts_supir_Click(sender As System.Object, e As System.EventArgs) Handles bts_supir.Click

        Dim fs As New fssupir With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_supir.EditValue = fs.get_KODE
        tnama_supir.EditValue = fs.get_NAMA

    End Sub

    Private Sub tkd_supir_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_supir.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_supir_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_supir_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_supir.LostFocus
        If tkd_supir.Text.Trim.Length = 0 Then
            tkd_supir.Text = ""
            tnama_supir.Text = ""
        End If
    End Sub

    Private Sub tkd_supir_Validated(sender As Object, e As System.EventArgs) Handles tkd_supir.Validated
        If tkd_supir.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and bagian like 'SUPIR%' and kd_karyawan='{0}'", tkd_supir.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_supir.EditValue = dread("kd_karyawan").ToString
                        tnama_supir.EditValue = dread("nama_karyawan").ToString

                    Else
                        tkd_supir.EditValue = ""
                        tnama_supir.EditValue = ""

                    End If
                Else
                    tkd_supir.EditValue = ""
                    tnama_supir.EditValue = ""

                End If


                dread.Close()

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Finally


                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If
            End Try

        End If
    End Sub

    Private Sub isi_barang()

        Dim sql As String = ""

        sql = "select kd_barang,nama_barang from ms_barang where jenis='FISIK'"


        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvs As DataView

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvs = dvm.CreateDataView(ds.Tables(0))

            Dim orow As DataRow = dvs.Table.NewRow
            orow("kd_barang") = "All"
            orow("nama_barang") = "All"
            dvs.Table.Rows.InsertAt(orow, 0)

            tbarang.Properties.DataSource = dvs

            tbarang.ItemIndex = 0

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

    Private Sub fpr_belidetail_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tjenis.Focus()
    End Sub

    Private Sub fpr_belidetail_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        tjenis.SelectedIndex = 0
        ttgl.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

        isi_nopol()
        isi_barang()

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        Cursor = Cursors.WaitCursor

        Dim sql As String = "SELECT     trbeli.nobukti, trbeli.tanggal, trbeli2.qty, trbeli2.satuan, trbeli2.harga, trbeli2.jumlah, trbeli.kd_karyawan, ms_pegawai.nama_karyawan, trbeli.nopol, trbeli.nosj, " & _
        "trbeli.tglsj, ms_barang.kd_barang, ms_barang.nama_barang " & _
        "FROM         trbeli INNER JOIN " & _
                      "trbeli2 ON trbeli.nobukti = trbeli2.nobukti INNER JOIN " & _
                      "ms_barang ON trbeli2.kd_barang = ms_barang.kd_barang INNER JOIN " & _
                      "ms_pegawai ON trbeli.kd_karyawan = ms_pegawai.kd_karyawan " & _
        "WHERE trbeli.sbatal = 0"

        If tjenis.Text.Trim.Equals("Tanggal") Then
            sql = String.Format("{0} and trbeli.tanggal>='{1}' and trbeli.tanggal<='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))
        Else
            sql = String.Format("{0} and trbeli.tglsj>='{1}' and trbeli.tglsj<='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))
        End If


        If tnosj.Text.Trim.Length > 0 Then
            sql = String.Format("{0} and trbeli.nosj='{1}'", sql, tnosj.Text.Trim)
        End If

        If tkd_supir.Text.Trim.Length > 0 Then
            sql = String.Format("{0} and trbeli.kd_karyawan='{1}'", sql, tkd_supir.Text.Trim)
        End If

        If Not tnopol.Text.Trim.ToString.Equals("All") Then
            sql = String.Format("{0} and trbeli.nopol='{1}'", sql, tnopol.EditValue)
        End If

        If Not tbarang.Text.Trim.ToString.Equals("All") Then
            sql = String.Format("{0} and ms_barang.kd_barang='{1}'", sql, tbarang.EditValue)
        End If


        Dim periode As String = String.Format("{0} s.d {1}", convert_date_to_ind(ttgl.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Dim fs As New fpr_belidetail2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .tgl = periode}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub



End Class