Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpr_beli

    Public nobukti As String

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = String.Format("SELECT     trbeli.nobukti, trbeli.tanggal, trbeli.kd_supp+ ' -' +ms_supplier.nama_supp as supplier, ms_supplier.alamat, trbeli.disc_rp AS disc_tot, trbeli.brutto, trbeli.netto, trbeli2.kd_barang, " & _
                      "trbeli2.kd_gudang as gudang, ms_barang.nama_barang, trbeli2.qty, trbeli2.satuan, trbeli2.harga, trbeli2.jumlah, trbeli.note, trbeli.kd_karyawan+ ' - ' +ms_pegawai.nama_karyawan as supir, " & _
                    "trbeli.nopol, trbeli.nosj, trbeli.tglsj " & _
                    "FROM         trbeli INNER JOIN " & _
                      "trbeli2 ON trbeli.nobukti = trbeli2.nobukti INNER JOIN " & _
                      "ms_supplier ON trbeli.kd_supp = ms_supplier.kd_supp INNER JOIN " & _
                      "ms_barang ON trbeli2.kd_barang = ms_barang.kd_barang INNER JOIN " & _
                      "ms_gudang ON trbeli2.kd_gudang = ms_gudang.kd_gudang INNER JOIN " & _
                      "ms_pegawai ON trbeli.kd_karyawan = ms_pegawai.kd_karyawan " & _
                      "WHERE trbeli.sbatal=0 and trbeli.nobukti='{0}'", nobukti)

            Dim ds As DataSet = New dsbeli
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_beli() With {.DataSource = ds.Tables(0)}
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