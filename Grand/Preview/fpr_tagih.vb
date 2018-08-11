Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpr_tagih

    Public nobukti As String

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = String.Format("SELECT     trdaftar_tgh.nobukti, trdaftar_tgh.tanggal, trdaftar_tgh.tgl_tagih, ms_pegawai.nama_karyawan, trdaftar_tgh2.nobukti_fak, trfaktur_to.tanggal AS tanggal_fak, " & _
            "trfaktur_to.tgl_tempo, trdaftar_tgh2.netto, trdaftar_tgh2.jmlbayar, trdaftar_tgh2.jmlgiro,trdaftar_tgh2.jmlgiro_gt, trdaftar_tgh2.sisa, trdaftar_tgh.jumlah, ms_toko.nama_toko " & _
            "FROM         trdaftar_tgh INNER JOIN " & _
            "trdaftar_tgh2 ON trdaftar_tgh.nobukti = trdaftar_tgh2.nobukti INNER JOIN " & _
            "trfaktur_to ON trdaftar_tgh2.nobukti_fak = trfaktur_to.nobukti INNER JOIN " & _
            "ms_pegawai ON trdaftar_tgh.kd_kolek = ms_pegawai.kd_karyawan INNER JOIN " & _
            "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko " & _
            "WHERE     (trdaftar_tgh.sbatal = 0) AND (trdaftar_tgh.spulang = 0) and (trdaftar_tgh.nobukti='{0}')", nobukti)

            Dim ds As DataSet = New dstagih
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_daftartagih1() With {.DataSource = ds.Tables(0)}
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