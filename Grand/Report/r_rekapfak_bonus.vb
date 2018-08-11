Public Class r_rekapfak_bonus

    Private Sub ReportHeader_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles ReportHeader.BeforePrint

        'If IsDBNull(GetCurrentColumnValue("kd_barang").ToString) Then
        '    XrLabel3.Visible = False
        'Else
        '    XrLabel3.Visible = True
        'End If

    End Sub

End Class