Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI
Public Class fpr_akum_penjualan_perbulan2

    Public sql As String
    Public tgl As String
    Public namatoko As String
    Public jenislap As Integer

    Private Sub load1(ByVal cn As OleDbConnection)

        Dim ds As DataSet = New ds_akum_penjualan_perbulan
        ds = Clsmy.GetDataSet(sql, cn)

        Dim ops As New System.Drawing.Printing.PrinterSettings
        Dim rrekap As New r_akum_penjualan_perbulan() With {.DataSource = ds.Tables(0)}
        rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
        rrekap.xperiode.Text = tgl
        rrekap.DataMember = rrekap.DataMember
        rrekap.PrinterName = ops.PrinterName
        rrekap.CreateDocument(True)

        PrintControl1.PrintingSystem = rrekap.PrintingSystem

    End Sub

    Private Sub load2(ByVal cn As OleDbConnection)

        Dim ds As DataSet = New ds_akum_penjualan_perbulan_pertko
        ds = Clsmy.GetDataSet(sql, cn)

        Dim ops As New System.Drawing.Printing.PrinterSettings
        Dim rrekap As New r_akum_penjualan_perbulan_pertko() With {.DataSource = ds.Tables(0)}
        rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
        rrekap.xperiode.Text = tgl
        rrekap.DataMember = rrekap.DataMember
        rrekap.PrinterName = ops.PrinterName
        rrekap.CreateDocument(True)

        PrintControl1.PrintingSystem = rrekap.PrintingSystem

    End Sub

    Private Sub load3(ByVal cn As OleDbConnection)

        Dim ds As DataSet = New ds_akum_penjualan_perbulan_persales
        ds = Clsmy.GetDataSet(sql, cn)

        Dim ops As New System.Drawing.Printing.PrinterSettings
        Dim rrekap As New r_akum_penjualan_perbulan_persales() With {.DataSource = ds.Tables(0)}
        rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
        rrekap.xperiode.Text = tgl
        rrekap.DataMember = rrekap.DataMember
        rrekap.PrinterName = ops.PrinterName
        rrekap.CreateDocument(True)

        PrintControl1.PrintingSystem = rrekap.PrintingSystem

    End Sub

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            If jenislap = 1 Then
                load1(cn)
            ElseIf jenislap = 2 Then
                load2(cn)
            Else
                load3(cn)
            End If

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