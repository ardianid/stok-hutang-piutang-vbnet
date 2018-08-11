Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpr_limitpiutang2

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView
    Private bs1 As BindingSource

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = "SELECT     ms_toko.kd_toko, ms_toko.nama_toko, ms_toko2.kd_karyawan, ms_pegawai.nama_karyawan, " & _
            "ms_toko2.limit_val, ms_toko2.jmlpiutang,ms_toko2.limit_val - ms_toko2.jmlpiutang as sisalimit " & _
            "FROM         ms_toko2 INNER JOIN " & _
            "ms_toko ON ms_toko2.kd_toko = ms_toko.kd_toko INNER JOIN " & _
            "ms_pegawai ON ms_toko2.kd_karyawan = ms_pegawai.kd_karyawan WHERE ms_toko.aktif=1"

            Dim ds As New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            bs1 = New BindingSource
            bs1.DataSource = dv1
            bn1.BindingSource = bs1

            grid1.DataSource = bs1

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

    Private Sub fpr_limitpiutang2_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        load_print()
    End Sub

    Private Sub tsprint_Click(sender As System.Object, e As System.EventArgs) Handles tsprint.Click
        GridView1.ShowRibbonPrintPreview()
    End Sub
End Class