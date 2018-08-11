Imports System.Drawing.Printing
Imports DevExpress.XtraReports.UI


Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class r_rekapfak2

    Private Sub PageHeader_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles PageHeader.BeforePrint

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

    Private Sub ReportFooter_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles ReportFooter.BeforePrint

        lbkembali.Text = ""
        ReportFooter.Visible = False

        Dim nobukti As String = GetCurrentColumnValue("nobukti").ToString

        Dim sql As String = String.Format("select ms_barang.nama_lap from ms_barang where kd_barang in (select ms_barang.kd_barang_kmb from trrekap_to3 inner join ms_barang " & _
            "on trrekap_to3.kd_barang=ms_barang.kd_barang where trrekap_to3.nobukti='{0}')", nobukti)

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Not drd(0).ToString.Equals("") Then
                    lbkembali.Text = String.Format("{0} (Kemb)", drd(0).ToString)
                    ReportFooter.Visible = True
                End If
            End If
            drd.Close()

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

    Private Sub Detail_BeforePrint(sender As Object, e As PrintEventArgs) Handles Detail.BeforePrint

        'CType((CType(sender, XRSubreport)).ReportSource, r_rekapfak2_detail).Parameters("cnobukti").Value = GetCurrentColumnValue("nobukti")
        'CType((CType(sender, XRSubreport)).ReportSource, r_rekapfak_bonus).Parameters("cnobukti").Value = GetCurrentColumnValue("nobukti")

    End Sub
End Class