Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpr_sewa_p

    Public nobukti As String

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = String.Format("SELECT     trsewa3.nobukti, trsewa3.tanggal, trsewa3.tanggal1 AS tgl_awal, trsewa3.tanggal2 AS tgl_akhir, trsewa3.nobukti_sw, ms_toko.kd_toko, " & _
                      "ms_toko.kd_toko + ' - ' + ms_toko.nama_toko AS nama_toko, ms_toko.alamat_toko, trsewa3.brutto, trsewa3.disc_rp, trsewa3.netto, trsewa3.note, " & _
                    "ms_barang.kd_barang, ms_barang.nama_barang, trsewa2.qty, trsewa2.satuan " & _
                    "FROM         trsewa3 INNER JOIN " & _
                      "trsewa ON trsewa3.nobukti_sw = trsewa.nobukti INNER JOIN " & _
                      "ms_toko ON trsewa.kd_toko = ms_toko.kd_toko INNER JOIN " & _
                      "trsewa2 ON trsewa.nobukti = trsewa2.nobukti INNER JOIN " & _
                      "ms_barang ON trsewa2.kd_barang = ms_barang.kd_barang " & _
                    "WHERE     (trsewa3.sbatal = 0) and (trsewa3.nobukti='{0}')", nobukti)

            Dim ds As DataSet = New dssewa_panj
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_sewa_panj() With {.DataSource = ds.Tables(0)}
            rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
            rrekap.DataMember = rrekap.DataMember
            rrekap.PrinterName = ops.PrinterName
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