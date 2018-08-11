Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI
Public Class fpr_inout_aslbrg2

    Public sql As String
    Public tgl As String
    Public jenislap As Integer

    Private Sub load_lap1(ByVal ds As DataSet)

        Dim ops As New System.Drawing.Printing.PrinterSettings
        Dim rrekap As New r_inoutg_asalbrg() With {.DataSource = ds.Tables(0)}
        rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
        rrekap.xperiode.Text = tgl
        rrekap.DataMember = rrekap.DataMember
        rrekap.PrinterName = ops.PrinterName
        rrekap.CreateDocument(True)

        PrintControl1.PrintingSystem = rrekap.PrintingSystem

    End Sub

    Private Sub load_lap2(ByVal ds As DataSet)

        Dim ops As New System.Drawing.Printing.PrinterSettings
        Dim rrekap As New r_inoutg_asalbrg2() With {.DataSource = ds.Tables(0)}
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

            Dim ds As DataSet = New ds_jmlinout_aslbrg
            ds = Clsmy.GetDataSet(sql, cn)

            If jenislap = 1 Then
                load_lap1(ds)
            Else
                load_lap2(ds)
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