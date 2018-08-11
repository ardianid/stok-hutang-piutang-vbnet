Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpr_pos_sewapinjam

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = "SELECT     ms_toko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, ms_barang.kd_barang, ms_barang.nama_lap as nama_barang, ms_toko3.jml_pinjam1 as jml_beli, ms_toko3.jml_sewa1 as jml_sewa " & _
                "FROM         ms_toko INNER JOIN " & _
                "ms_toko3 ON ms_toko.kd_toko = ms_toko3.kd_toko INNER JOIN " & _
                "ms_barang ON ms_toko3.kd_barang = ms_barang.kd_barang WHERE ms_toko.aktif=1"

            Dim ds As DataSet = New dssewapinjam
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_pos_pinjamsewa() With {.DataSource = ds.Tables(0)}
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