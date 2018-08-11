Imports System.Data
Imports System.Data.OleDb

Public Class ftutup_bku

    Private Sub btproses_Click(sender As Object, e As EventArgs) Handles btproses.Click

        Dim sql As String = ""

    End Sub

    Private Sub ftutup_bku_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        cbbulan.Focus()
    End Sub

    Private Sub ftutup_bku_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Month(Date.Now) = 1 Then
            cbbulan.EditValue = "Desember"
        Else

            cbbulan.SelectedIndex = Month(Date.Now) - 2

        End If

    End Sub

End Class