Public Class frekap_to5 

    Private jml As Integer = 0

    Public ReadOnly Property get_jml As String
        Get
            Return jml
        End Get
    End Property

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        jml = 0
        Me.Close()
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click
        jml = tjml.EditValue
        Me.Close()
    End Sub

    Private Sub tjml_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tjml.KeyDown

        If e.KeyCode = 13 Then
            btsimpan_Click(sender, Nothing)
        ElseIf e.KeyCode = Keys.Escape Then
            btclose_Click(sender, Nothing)
        End If

    End Sub

    Private Sub frekap_to5_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tjml.Focus()
    End Sub

    Private Sub frekap_to5_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tjml.EditValue = 200
    End Sub

End Class