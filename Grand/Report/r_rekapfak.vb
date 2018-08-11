Public Class r_rekapfak

    Dim hqty1 As Integer
    Dim hqty2 As Integer
    Dim hqty3 As Integer

    Private Sub GroupHeader1_BeforePrint(sender As System.Object, e As System.Drawing.Printing.PrintEventArgs) Handles GroupHeader1.BeforePrint

        Dim kenek1 As String = GetCurrentColumnValue("nama_kenek1").ToString
        Dim kenek2 As String = GetCurrentColumnValue("nama_kenek2").ToString
        Dim kenek3 As String = GetCurrentColumnValue("nama_kenek3").ToString

        If kenek2.Length > 0 Then
            kenek1 = String.Format("{0} | {1}", kenek1, kenek2)
        End If

        If kenek3.Length > 0 Then
            kenek1 = String.Format("{0} | {1}", kenek1, kenek2)
        End If

        lbkenek.Text = kenek1

    End Sub

    Private Sub xtotqty_SummaryCalculated(sender As System.Object, e As DevExpress.XtraReports.UI.TextFormatEventArgs) Handles xtotqty.SummaryCalculated

        Dim qty1 As Integer = Integer.Parse(GetCurrentColumnValue("qty1"))
        Dim qty2 As Integer = Integer.Parse(GetCurrentColumnValue("qty2"))
        Dim qty3 As Integer = Integer.Parse(GetCurrentColumnValue("qty3"))

        Dim totqty As String = e.Value

        Dim sisa As Integer = totqty Mod (qty1 * qty2 * qty3)

        hqty1 = (totqty - sisa) / (qty1 * qty2 * qty3)

        If sisa > qty2 Then
            hqty2 = sisa
            sisa = sisa Mod qty2

            hqty2 = (hqty2 - sisa) / qty2
            hqty3 = sisa

        Else
            hqty2 = sisa
            hqty3 = 0
        End If

    End Sub

    Private Sub lqty1_SummaryGetResult(sender As System.Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles lqty1.SummaryGetResult
        e.Result = hqty1
        e.Handled = True
    End Sub

    Private Sub lqty1_SummaryReset(sender As Object, e As System.EventArgs) Handles lqty1.SummaryReset
        hqty1 = 0
    End Sub

    Private Sub lqty2_SummaryGetResult(sender As System.Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles lqty2.SummaryGetResult
        e.Result = hqty2
        e.Handled = True
    End Sub

    Private Sub lqty2_SummaryReset(sender As Object, e As System.EventArgs) Handles lqty2.SummaryReset
        hqty2 = 0
    End Sub

    Private Sub lqty3_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles lqty3.SummaryGetResult
        e.Result = hqty3
        e.Handled = True
    End Sub

    Private Sub lqty3_SummaryReset(sender As Object, e As System.EventArgs) Handles lqty3.SummaryReset
        hqty3 = 0
    End Sub

    Private Sub GroupHeader2_BeforePrint(sender As System.Object, e As System.Drawing.Printing.PrintEventArgs) Handles GroupHeader2.BeforePrint

        Dim sgudang As String = GetCurrentColumnValue("kd_gudang").ToString & " - " & GetCurrentColumnValue("nama_gudang").ToString
        xgudang.Text = sgudang

    End Sub

End Class