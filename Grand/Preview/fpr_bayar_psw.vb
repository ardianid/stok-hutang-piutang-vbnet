Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpr_bayar_psw

    Public nobukti As String

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = String.Format("SELECT     tr_bayar_psw.nobukti, tr_bayar_psw.tanggal, ms_toko.kd_toko,ms_toko.kd_toko+ ' - ' +ms_toko.nama_toko as nama_toko, ms_toko.alamat_toko, tr_bayar_psw.note, tr_bayar_psw.jumlah, " & _
                      "tr_bayar_psw2.nobukti_sw, tr_bayar_psw2.jumlah AS jmlbayar, trsewa3.netto AS jmlpanjang, trsewa3.tanggal AS tglpanjang, trsewa3.tanggal1, trsewa3.tanggal2 " & _
                "FROM         tr_bayar_psw INNER JOIN " & _
                "tr_bayar_psw2 ON tr_bayar_psw.nobukti = tr_bayar_psw2.nobukti INNER JOIN " & _
                      "ms_toko ON tr_bayar_psw.kd_toko = ms_toko.kd_toko INNER JOIN " & _
                      "trsewa3 ON tr_bayar_psw2.nobukti_sw = trsewa3.nobukti " & _
                  "where tr_bayar_psw.sbatal=0 and tr_bayar_psw.nobukti='{0}'", nobukti)

            Dim ds As DataSet = New dsbayar_psw
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_bayar_psw() With {.DataSource = ds.Tables(0)}
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