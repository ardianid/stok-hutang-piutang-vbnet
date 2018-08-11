Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class fpr_hgallon_toko2

    Public tgl1 As String
    Public tgl2 As String
    Public kdtoko As String
    Public jenislap As Integer

    Dim crReportDocument As cr_hgallon_toko
    Dim crReportDocument2 As cr_hgallon_toko_p

    Private Sub load1(ByVal cn As OleDbConnection)

        Dim sql As String = String.Format("SELECT    hbarang_gudang.noid, ms_toko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, hbarang_gudang.nobukti, hbarang_gudang.tanggal, ms_barang.kd_barang, ms_barang.nama_barang, " & _
            "hbarang_gudang.jmlin, hbarang_gudang.jmlout, hbarang_gudang.jenis " & _
            "FROM         v_faktur_retur INNER JOIN " & _
                      "ms_toko ON v_faktur_retur.kd_toko = ms_toko.kd_toko INNER JOIN " & _
                      "hbarang_gudang INNER JOIN " & _
                      "ms_barang ON hbarang_gudang.kd_barang = ms_barang.kd_barang ON v_faktur_retur.nobukti = hbarang_gudang.nobukti " & _
            "WHERE hbarang_gudang.kd_gudang='None B' and hbarang_gudang.tanggal>='{0}' and hbarang_gudang.tanggal<='{1}' and ms_toko.kd_toko='{2}'", convert_date_to_eng(tgl1), convert_date_to_eng(tgl2), kdtoko)


        Dim ds As New DataSet
        ds = Clsmy.GetDataSet(sql, cn)

        Dim ds1 As New ds_hgallon_toko
        ds1.Clear()
        ds1.Tables(0).Merge(ds.Tables(0))

        crReportDocument = New cr_hgallon_toko
        crReportDocument.SetDataSource(ds1)

        Dim jmlold As String = cek_jmlold(cn).ToString

        'crReportDocument.SetParameterValue("tgl1", convert_date_to_ind(tgl1))
        'crReportDocument.SetParameterValue("tgl2", convert_date_to_ind(tgl2))
        crReportDocument.SetParameterValue("ajmlold2", jmlold)
        crReportDocument.SetParameterValue("userprint", String.Format("User : {0} | Tgl : {1}", userprog, Date.Now))

        CrystalReportViewer1.ReportSource = crReportDocument
        CrystalReportViewer1.Refresh()

    End Sub

    Private Sub load2(ByVal cn As OleDbConnection)

        Dim sql As String = String.Format("SELECT    hbarang_gudang.noid, ms_toko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, hbarang_gudang.nobukti, hbarang_gudang.tanggal, ms_barang.kd_barang, ms_barang.nama_barang, " & _
            "hbarang_gudang.jmlin, hbarang_gudang.jmlout, hbarang_gudang.jenis " & _
            "FROM         v_faktur_retur INNER JOIN " & _
                      "ms_toko ON v_faktur_retur.kd_toko = ms_toko.kd_toko INNER JOIN " & _
                      "hbarang_gudang INNER JOIN " & _
                      "ms_barang ON hbarang_gudang.kd_barang = ms_barang.kd_barang ON v_faktur_retur.nobukti = hbarang_gudang.nobukti " & _
            "WHERE hbarang_gudang.kd_gudang='None P' and hbarang_gudang.tanggal>='{0}' and hbarang_gudang.tanggal<='{1}' and ms_toko.kd_toko='{2}'", convert_date_to_eng(tgl1), convert_date_to_eng(tgl2), kdtoko)


        Dim ds As New DataSet
        ds = Clsmy.GetDataSet(sql, cn)

        Dim ds1 As New ds_hgallon_toko
        ds1.Clear()
        ds1.Tables(0).Merge(ds.Tables(0))

        crReportDocument2 = New cr_hgallon_toko_p
        crReportDocument2.SetDataSource(ds1)

        Dim jmlold As String = cek_jmlold(cn).ToString

        'crReportDocument.SetParameterValue("tgl1", convert_date_to_ind(tgl1))
        'crReportDocument.SetParameterValue("tgl2", convert_date_to_ind(tgl2))
        crReportDocument2.SetParameterValue("ajmlold2", jmlold)
        crReportDocument2.SetParameterValue("userprint", String.Format("User : {0} | Tgl : {1}", userprog, Date.Now))

        CrystalReportViewer1.ReportSource = crReportDocument2
        CrystalReportViewer1.Refresh()

    End Sub



    Private Sub load_print()

        Cursor = Cursors.WaitCursor

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            If jenislap = 1 Then
                load1(cn)
            Else
                load2(cn)
            End If

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

        Dim sql As String = String.Format("SELECT  sum(hbarang_gudang.jmlin) - sum(hbarang_gudang.jmlout) as jml " & _
        "FROM         v_faktur_retur INNER JOIN " & _
        "ms_toko ON v_faktur_retur.kd_toko = ms_toko.kd_toko INNER JOIN " & _
                     "hbarang_gudang INNER JOIN " & _
                      "ms_barang ON hbarang_gudang.kd_barang = ms_barang.kd_barang ON v_faktur_retur.nobukti = hbarang_gudang.nobukti " & _
        "WHERE hbarang_gudang.tanggal<'{0}' and ms_toko.kd_toko='{1}'", convert_date_to_eng(tgl1), kdtoko)

        If jenislap = 1 Then
            sql = String.Format("{0} and hbarang_gudang.kd_gudang='None B'", sql)
        Else
            sql = String.Format("{0} and hbarang_gudang.kd_gudang='None P'", sql)
        End If

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

    Private Sub fpr_hgallon_toko2_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        load_print()
    End Sub

End Class