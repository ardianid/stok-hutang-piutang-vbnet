Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpr_insentif2

    Public sql As String
    Public tgl As String
    Public tipe As Integer
    Public jenis_lap As Integer

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            If tipe = 0 Then
                load0(cn)
            ElseIf tipe = 1 Then
                load1(cn)
            ElseIf tipe = 2 Then
                load2(cn)
            ElseIf tipe = 3 Then
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

    Private Sub load0(ByVal cn As OleDbConnection)

        Dim ds As DataSet = New ds_jualbyitem
        ds = Clsmy.GetDataSet(sql, cn)

        Dim ops As New System.Drawing.Printing.PrinterSettings
        Dim rrekap As New r_ins_sales() With {.DataSource = ds.Tables(0)}
        rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
        rrekap.xperiode.Text = tgl

        If jenis_lap = 0 Then
            rrekap.GroupHeader3.Visible = True
            rrekap.GroupFooter1.Visible = True
            rrekap.GroupFooter3.Visible = True
            rrekap.GroupFooter4.Visible = False
            rrekap.GroupHeader4.Visible = False
            rrekap.ReportHeader.Visible = True
        Else
            rrekap.GroupHeader3.Visible = False
            rrekap.GroupFooter1.Visible = False
            rrekap.GroupFooter3.Visible = False
            rrekap.GroupFooter4.Visible = True
            rrekap.GroupHeader4.Visible = True
            rrekap.ReportHeader.Visible = False
        End If

        rrekap.DataMember = rrekap.DataMember
        rrekap.PrinterName = ops.PrinterName
        rrekap.CreateDocument(True)

        PrintControl1.PrintingSystem = rrekap.PrintingSystem

    End Sub

    Private Sub load1(ByVal cn As OleDbConnection)

        Dim ds As DataSet = New ds_jualbyitem
        ds = Clsmy.GetDataSet(sql, cn)

        If jenis_lap = 1 Then
            GoTo masuk_2
        End If

        Dim ops As New System.Drawing.Printing.PrinterSettings
        Dim rrekap As New r_kirimbysupir() With {.DataSource = ds.Tables(0)}
        rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
        rrekap.xperiode.Text = tgl

        If jenis_lap = 0 Then
            rrekap.GroupHeader3.Visible = False
            rrekap.GroupHeader4.Visible = True
            rrekap.GroupFooter5.Visible = False
            rrekap.ReportHeader.Visible = True
            rrekap.GroupFooter2.Visible = True
            rrekap.GroupFooter1.Visible = True
        Else
            rrekap.GroupHeader3.Visible = True
            rrekap.GroupHeader4.Visible = False
            rrekap.GroupFooter5.Visible = True
            rrekap.ReportHeader.Visible = False
            rrekap.GroupFooter2.Visible = False
            rrekap.GroupFooter1.Visible = False
        End If

        rrekap.DataMember = rrekap.DataMember
        rrekap.PrinterName = ops.PrinterName
        rrekap.CreateDocument(True)

        PrintControl1.PrintingSystem = rrekap.PrintingSystem
        Exit Sub

masuk_2:


        Dim ops2 As New System.Drawing.Printing.PrinterSettings
        Dim rrekap2 As New r_rkap_ins_supir() With {.DataSource = ds.Tables(0)}
        rrekap2.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
        rrekap2.xperiode.Text = tgl

        rrekap2.DataMember = rrekap2.DataMember
        rrekap2.PrinterName = ops2.PrinterName
        rrekap2.CreateDocument(True)

        PrintControl1.PrintingSystem = rrekap2.PrintingSystem


    End Sub

    Private Sub load2(ByVal cn As OleDbConnection)

        Dim ds As DataSet = New ds_jualbyitem
        ds = Clsmy.GetDataSet(sql, cn)

        If jenis_lap = 1 Then
            GoTo masuk_2
        End If

        Dim ops As New System.Drawing.Printing.PrinterSettings
        Dim rrekap As New r_kirimbykenek() With {.DataSource = ds.Tables(0)}
        rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
        rrekap.xperiode.Text = tgl
        rrekap.DataMember = rrekap.DataMember
        rrekap.PrinterName = ops.PrinterName
        rrekap.CreateDocument(True)

        PrintControl1.PrintingSystem = rrekap.PrintingSystem

        Exit Sub

masuk_2:

        Dim ops2 As New System.Drawing.Printing.PrinterSettings
        Dim rrekap2 As New r_rkap_ins_kenek() With {.DataSource = ds.Tables(0)}
        rrekap2.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
        rrekap2.xperiode.Text = tgl

        rrekap2.DataMember = rrekap2.DataMember
        rrekap2.PrinterName = ops2.PrinterName
        rrekap2.CreateDocument(True)

        PrintControl1.PrintingSystem = rrekap2.PrintingSystem

    End Sub

    Private Sub load3(ByVal cn As OleDbConnection)

        Dim ds As DataSet

        If jenis_lap = 0 Then

            ds = New ds_jualbyitem_bli
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_kirimbysupir_bli() With {.DataSource = ds.Tables(0)}
            rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
            rrekap.xperiode.Text = tgl
            rrekap.DataMember = rrekap.DataMember
            rrekap.PrinterName = ops.PrinterName
            rrekap.CreateDocument(True)

            PrintControl1.PrintingSystem = rrekap.PrintingSystem

        Else

            ds = New ds_jualbyitem_bli2
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap2 As New r_rkap_ins_supir_bli() With {.DataSource = ds.Tables(0)}
            rrekap2.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
            rrekap2.xperiode.Text = tgl
            rrekap2.DataMember = rrekap2.DataMember
            rrekap2.PrinterName = ops.PrinterName
            rrekap2.CreateDocument(True)

            PrintControl1.PrintingSystem = rrekap2.PrintingSystem

        End If

        

    End Sub

    Private Sub fpr_rekapaktur_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        load_print()
    End Sub

End Class