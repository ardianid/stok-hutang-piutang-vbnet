Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpr_kirim_jb

    Public nobukti As String

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = String.Format("SELECT     tr_kirimjb.nobukti, tr_kirimjb.tanggal, tr_kirimjb.tgl_keluar, tr_kirimjb.tgl_kirim, tr_kirimjb.nopol, ms_pegawai.nama_karyawan, tr_kirimjb.note, tr_kirimjb2.kd_barang, " & _
                      "ms_barang.nama_barang, tr_kirimjb2.qty, tr_kirimjb2.satuan,tr_kirimjb2.kd_gudang+ ' - ' +ms_gudang.nama_gudang as gudang " & _
                    "FROM         tr_kirimjb INNER JOIN " & _
                      "tr_kirimjb2 ON tr_kirimjb.nobukti = tr_kirimjb2.nobukti INNER JOIN " & _
                      "ms_barang ON tr_kirimjb2.kd_barang = ms_barang.kd_barang INNER JOIN " & _
                      "ms_pegawai ON tr_kirimjb.kd_supir = ms_pegawai.kd_karyawan INNER JOIN " & _
                      "ms_gudang ON tr_kirimjb2.kd_gudang = ms_gudang.kd_gudang " & _
                    "WHERE     tr_kirimjb.sbatal = 0 and tr_kirimjb.nobukti='{0}'", nobukti)

            Dim ds As DataSet = New dskirim_jb
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_kirimjb() With {.DataSource = ds.Tables(0)}
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