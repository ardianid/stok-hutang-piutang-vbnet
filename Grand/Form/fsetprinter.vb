Imports System.Drawing.Printing

Public Class fsetprinter

    Private Sub loadprinter_tocombo()

        Dim pkInstalledPrinters As String

        For Each pkInstalledPrinters In _
        PrinterSettings.InstalledPrinters
            cbprint1.Properties.Items.Add(pkInstalledPrinters)
            cbprint2.Properties.Items.Add(pkInstalledPrinters)
        Next pkInstalledPrinters

    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        Me.Close()
    End Sub

    Private Sub fsetprinter_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        cbprint1.Focus()
    End Sub

    Private Sub fsetprinter_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        loadprinter_tocombo()

        cbos.SelectedIndex = 1

        cbprint1.Text = varprinter1
        cbprint2.Text = varprinter2
        cbos.Text = varos


    End Sub

    Private Sub SimpleButton2_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton2.Click

        Try

            Clsmy.WritePrinterSet(cbprint1.Text.Trim, cbprint2.Text.Trim, cbos.Text.Trim)

            varprinter1 = cbprint1.Text.Trim
            varprinter2 = cbprint2.Text.Trim
            varos = cbos.Text

            Me.Close()

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        End Try

    End Sub


End Class