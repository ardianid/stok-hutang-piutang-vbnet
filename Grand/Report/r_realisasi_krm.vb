Public Class r_realisasi_krm

    Private jmlmasuk As Double = 0
    Private jmlretur As Double = 0

    Private Sub XrLabel10_SummaryCalculated(sender As Object, e As DevExpress.XtraReports.UI.TextFormatEventArgs) Handles XrLabel10.SummaryCalculated

        If XrLabel14.Text = "1. FAKTUR" Then

            jmlmasuk = e.Value

        Else
            jmlretur = e.Value
        End If

        XrLabel13.Text = String.Format("{0:n0}", jmlmasuk - jmlretur)

    End Sub

End Class