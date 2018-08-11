Public Class r_spm

    Private Sub GroupHeader1_BeforePrint(sender As System.Object, e As System.Drawing.Printing.PrintEventArgs) Handles GroupHeader1.BeforePrint


        'Dim kenek1 As String = GetCurrentColumnValue("nama_kenek1").ToString
        'Dim kenek2 As String = GetCurrentColumnValue("nama_kenek2").ToString
        'Dim kenek3 As String = GetCurrentColumnValue("nama_kenek3").ToString

        'If kenek2.Length > 0 Then
        '    kenek1 = String.Format("{0} | {1}", kenek1, kenek2)
        'End If

        'If kenek3.Length > 0 Then
        '    kenek1 = String.Format("{0} | {1}", kenek1, kenek2)
        'End If

        'lbkenek.Text = kenek1

    End Sub

    Private Sub GroupHeader2_BeforePrint(sender As System.Object, e As System.Drawing.Printing.PrintEventArgs) Handles GroupHeader2.BeforePrint

        Dim sgudang As String = GetCurrentColumnValue("kd_gudang").ToString & " - " & GetCurrentColumnValue("nama_gudang").ToString
        xgudang.Text = sgudang

    End Sub
End Class