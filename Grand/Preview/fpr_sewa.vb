Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpr_sewa

    Public nobukti As String

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = String.Format("SELECT     trsewa.nobukti, trsewa.tanggal, trsewa.tgl_keluar, trsewa.tanggal1 AS tgl_mulai, trsewa.tanggal2 AS tgl_akhir, ms_toko.kd_toko,ms_toko.kd_toko+ ' - ' +ms_toko.nama_toko as nama_toko, " & _
                      "ms_toko.alamat_toko, trsewa.disc_rp AS disc_tot, trsewa.brutto, trsewa.netto, trsewa.note, ms_barang.kd_barang, ms_barang.nama_barang, trsewa2.qty, " & _
                      "trsewa2.satuan, trsewa2.harga, trsewa2.jumlah " & _
                        "FROM         trsewa INNER JOIN " & _
                      "trsewa2 ON trsewa.nobukti = trsewa2.nobukti INNER JOIN " & _
                      "ms_toko ON trsewa.kd_toko = ms_toko.kd_toko INNER JOIN " & _
                      "ms_barang ON trsewa2.kd_barang = ms_barang.kd_barang " & _
                      "WHERE trsewa.sbatal=0 and trsewa.nobukti='{0}'", nobukti)

            Dim ds As DataSet = New dssewa
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_sewa() With {.DataSource = ds.Tables(0)}
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