Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpr_limitpiutang



    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = "SELECT     ms_toko.kd_toko, ms_toko.nama_toko, ms_toko2.kd_karyawan, ms_pegawai.nama_karyawan, " & _
            "ms_toko2.limit_val, ms_toko2.jmlpiutang,ms_toko2.limit_val - ms_toko2.jmlpiutang as sisalimit " & _
            "FROM         ms_toko2 INNER JOIN " & _
            "ms_toko ON ms_toko2.kd_toko = ms_toko.kd_toko INNER JOIN " & _
            "ms_pegawai ON ms_toko2.kd_karyawan = ms_pegawai.kd_karyawan WHERE ms_toko.aktif=1"

            Dim ds As DataSet = New ds_limitpiutang_sales
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_limitpiutang_bysal() With {.DataSource = ds.Tables(0)}
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