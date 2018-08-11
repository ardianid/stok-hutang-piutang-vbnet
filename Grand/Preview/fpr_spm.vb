Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpr_spm

    Public nobukti As String
    Public tipe As Integer

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = String.Format("SELECT     trspm.nobukti, trspm.tanggal, trspm.tglmuat, trspm.tglberangkat, trspm.nopol, ms_supir.nama_karyawan AS nama_supir, " & _
                      "ms_kenek1.nama_karyawan AS nama_kenek1, ms_kenek2.nama_karyawan AS nama_kenek2, ms_kenek3.nama_karyawan AS nama_kenek3, " & _
                      "ms_gudang.kd_gudang, ms_gudang.nama_gudang, ms_barang.kd_barang, ms_barang.nama_lap as nama_barang, trspm2.qty, trspm2.satuan,ms_barang.nohrus " & _
                        "FROM         trspm INNER JOIN " & _
                      "trspm2 ON trspm.nobukti = trspm2.nobukti INNER JOIN " & _
                      "ms_gudang ON trspm2.kd_gudang = ms_gudang.kd_gudang INNER JOIN " & _
                      "ms_barang ON trspm2.kd_barang = ms_barang.kd_barang LEFT OUTER JOIN " & _
                      "ms_pegawai AS ms_kenek3 ON trspm.kd_kenek3 = ms_kenek3.kd_karyawan LEFT OUTER JOIN " & _
                      "ms_pegawai AS ms_kenek2 ON trspm.kd_kenek2 = ms_kenek2.kd_karyawan LEFT OUTER JOIN " & _
                      "ms_pegawai AS ms_kenek1 ON trspm.kd_kenek1 = ms_kenek1.kd_karyawan LEFT OUTER JOIN " & _
                      "ms_pegawai AS ms_supir ON trspm.kd_supir = ms_supir.kd_karyawan " & _
                      "WHERE ms_barang.jenis='FISIK' and trspm.sbatal=0 and trspm.nobukti='{0}'", nobukti)

            Dim ds As DataSet = New dsspmx
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_spm() With {.DataSource = ds.Tables(0)}
            rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)

            If tipe = 1 Then
                rrekap.xjudul.Text = "SURAT  PERINTAH  MUAT  BARANG (GALLON)"
            ElseIf tipe = 2 Then
                rrekap.xjudul.Text = "SURAT  PERINTAH  MUAT  BARANG (DUS)"
            Else
                rrekap.xjudul.Text = "SURAT  PERINTAH  MUAT  BARANG"
            End If

            rrekap.DataMember = rrekap.DataMember
            rrekap.PrinterName = varprinter2
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