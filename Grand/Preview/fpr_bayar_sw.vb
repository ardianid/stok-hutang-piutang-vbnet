Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpr_bayar_sw

    Public nobukti As String

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = String.Format("SELECT     trbayar_sw.nobukti, trbayar_sw.tanggal, ms_toko.kd_toko,ms_toko.kd_toko + ' - ' + ms_toko.nama_toko as nama_toko, ms_toko.alamat_toko, trbayar_sw2.nobukti_sw, trsewa.netto AS jml_sewa, " & _
                "trbayar_sw2.jumlah AS jml_bayar, trbayar_sw.note, trbayar_sw.jumlah,trsewa.tanggal as tglsewa,trsewa.tanggal1,trsewa.tanggal2 " & _
                "FROM         trbayar_sw INNER JOIN " & _
                "trbayar_sw2 ON trbayar_sw.nobukti = trbayar_sw2.nobukti INNER JOIN " & _
                "ms_toko ON trbayar_sw.kd_toko = ms_toko.kd_toko INNER JOIN " & _
                "trsewa ON trbayar_sw2.nobukti_sw = trsewa.nobukti " & _
                "WHERE trbayar_sw.sbatal=0 and  trbayar_sw.nobukti='{0}'", nobukti)

            Dim ds As DataSet = New dsbayar_sw3
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_bayar_sw() With {.DataSource = ds.Tables(0)}
            rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
            rrekap.DataMember = rrekap.DataMember
            rrekap.PrinterName = varprinter1
            rrekap.CreateDocument(True)

            PrintControl1.PrintingSystem = rrekap.PrintingSystem

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

    Private Sub fpr_rekapaktur_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        load_print()
    End Sub

End Class