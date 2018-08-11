Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class fpr_hbarang_gudang

    Public tgl1 As String
    Public tgl2 As String
    Public kdbarang As String

    Dim crReportDocument As cr_hbarang_gudang

    Dim dvg As DataView

    Private Sub load_print()

        Cursor = Cursors.WaitCursor

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = "SELECT     hbarang_gudang.noid, hbarang_gudang.nobukti, hbarang_gudang.tanggal, hbarang_gudang.kd_gudang, hbarang_gudang.kd_barang, ms_barang.nama_barang, " & _
            "hbarang_gudang.jmlin, hbarang_gudang.jmlout, hbarang_gudang.jenis, ms_barang.satuan1, ms_barang.satuan2, ms_barang.satuan3, ms_barang.qty1, " & _
            "ms_barang.qty2, ms_barang.qty3, ms_pegawai.nama_karyawan as supir " & _
            "FROM         hbarang_gudang INNER JOIN " & _
                      "ms_barang ON hbarang_gudang.kd_barang = ms_barang.kd_barang " & _
                      "LEFT JOIN ms_pegawai on hbarang_gudang.supirsales=ms_pegawai.kd_karyawan " & _
            "WHERE not(hbarang_gudang.kd_gudang like 'None%') "

            If cb1.SelectedIndex = 0 Then
                sql = String.Format(" {0} and hbarang_gudang.tanggal >='{1}' and hbarang_gudang.tanggal <='{2}' and hbarang_gudang.kd_barang='{3}' ", sql, convert_date_to_eng(tgl1), convert_date_to_eng(tgl2), kdbarang)
            Else
                sql = String.Format(" {0} and hbarang_gudang.nobukti in (select nobukti from v_bukti_tglkirim_all where tanggal>='{1}' and tanggal<='{2}') and hbarang_gudang.kd_barang='{3}' ", sql, convert_date_to_eng(tgl1), convert_date_to_eng(tgl2), kdbarang)
            End If


            If Not tgudang.EditValue.ToString.Equals("All") Then

                sql = String.Format("{0} and hbarang_gudang.kd_gudang='{1}'", sql, tgudang.EditValue)

            End If

            Dim ds As New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ds1 As New ds_hbaranggudang
            ds1.Clear()
            ds1.Tables(0).Merge(ds.Tables(0))

            crReportDocument = New cr_hbarang_gudang
            crReportDocument.SetDataSource(ds1)

            Dim jmlold As String = cek_jmlold(cn).ToString

            crReportDocument.SetParameterValue("tgl1", convert_date_to_ind(tgl1))
            crReportDocument.SetParameterValue("tgl2", convert_date_to_ind(tgl2))
            crReportDocument.SetParameterValue("ajmlold2", jmlold)
            crReportDocument.SetParameterValue("userprint", String.Format("User : {0} | Tgl : {1}", userprog, Date.Now))

            CrystalReportViewer1.ReportSource = crReportDocument
            CrystalReportViewer1.Refresh()

            Cursor = Cursors.Default

        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            Cursor = Cursors.Default

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Function cek_jmlold(ByVal cn As OleDbConnection) As Integer

        Dim jmlold As Integer = 0

        Dim sql As String = String.Format("select sum(jmlin)- sum(jmlout) as jmlold from hbarang_gudang where tanggal <'{0}' and kd_barang='{1}'", convert_date_to_eng(tgl1), kdbarang)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        If drd.Read Then
            If IsNumeric(drd(0).ToString) Then
                jmlold = Integer.Parse(drd(0).ToString)
            End If
        End If

        drd.Close()

        Return jmlold

    End Function

    Private Sub isi_gudang()

        Const sql As String = "select kd_gudang,nama_gudang from ms_gudang"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet
        Dim dvg As DataView

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(Sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvg = dvm.CreateDataView(ds.Tables(0))

            Dim orow As DataRow = dvg.Table.NewRow
            orow("kd_gudang") = "All"
            orow("nama_gudang") = "All"
            dvg.Table.Rows.InsertAt(orow, 0)

            tgudang.Properties.DataSource = dvg

            tgudang.ItemIndex = 0

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

    Private Sub fpr_hbarang_gudang_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        cb1.Focus()
    End Sub

    Private Sub fpr_rekapaktur_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        cb1.SelectedIndex = 0

        ttgl.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

        isi_gudang()
        isi_barang()

    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        tgl1 = ttgl.EditValue
        tgl2 = ttgl2.EditValue

        kdbarang = tbarang.EditValue

        load_print()

    End Sub

    Private Sub tbarang_EditValueChanged(sender As Object, e As System.EventArgs) Handles tbarang.EditValueChanged

        tnamabarang.Text = dvg(tbarang.ItemIndex)("nama_barang").ToString

    End Sub

End Class