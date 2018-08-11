Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpr_retur

    Public nobukti As String

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = String.Format("SELECT     trretur.nobukti, trretur.tanggal, trretur.tgl_masuk, ms_toko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, trretur.alasan_retur, trretur.brutto, " & _
                      "trretur.disc_rp AS disc, trretur.netto, ms_barang.kd_barang, ms_barang.nama_barang, trretur2.qty, trretur2.satuan, trretur2.harga, trretur2.disc_rp, trretur2.jumlah,trretur.nopol,ms_pegawai.nama_karyawan as nama_supir,trretur.no_nota " & _
                     "FROM         trretur INNER JOIN " & _
                      "trretur2 ON trretur.nobukti = trretur2.nobukti INNER JOIN " & _
                      "ms_toko ON trretur.kd_toko = ms_toko.kd_toko INNER JOIN " & _
                      "ms_barang ON trretur2.kd_barang = ms_barang.kd_barang LEFT OUTER JOIN " & _
                      "ms_pegawai on trretur.kd_supir=ms_pegawai.kd_karyawan " & _
                      "where trretur.sbatal=0 and trretur.nobukti='{0}'", nobukti)

            Dim ds As DataSet = New dsretur
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_returbar() With {.DataSource = ds.Tables(0)}
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