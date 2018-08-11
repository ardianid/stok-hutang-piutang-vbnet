Public Class trspm2_pr 

    Private statprint As String

    Public ReadOnly Property get_konfirm As String
        Get
            Return statprint
        End Get
    End Property

    Private Sub btcanc_Click(sender As System.Object, e As System.EventArgs) Handles btcanc.Click
        statprint = "0"
        Me.Close()
    End Sub

    Private Sub bt_gln_Click(sender As System.Object, e As System.EventArgs) Handles bt_gln.Click
        statprint = "1"
        Me.Close()
    End Sub

    Private Sub bt_dus_Click(sender As System.Object, e As System.EventArgs) Handles bt_dus.Click
        statprint = "2"
        Me.Close()
    End Sub

    Private Sub bt_all_Click(sender As System.Object, e As System.EventArgs) Handles bt_all.Click
        statprint = "3"
        Me.Close()
    End Sub

End Class