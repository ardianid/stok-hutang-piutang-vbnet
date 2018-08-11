Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class fpr_kirimbysupir_bli

    Dim crReportDocument As cr_kirimbysupir_bli

    Public sql As String
    Public tgl1 As String
    Public tgl2 As String

    Private Sub load_print()

        Cursor = Cursors.WaitCursor

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim ds As New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ds1 As New ds_jualbyitem_bli
            ds1.Clear()
            ds1.Tables(0).Merge(ds.Tables(0))

            crReportDocument = New cr_kirimbysupir_bli
            crReportDocument.SetDataSource(ds1)

            crReportDocument.SetParameterValue("periode", convert_date_to_ind(tgl1) & " s.d " & convert_date_to_ind(tgl2))
            '  crReportDocument.SetParameterValue("userprint", String.Format("User : {0} | Tgl : {1}", userprog, Date.Now))

            CrystalReportViewer1.ReportSource = crReportDocument
            CrystalReportViewer1.Refresh()

            Cursor = Cursors.Default

        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            Cursor = Cursors.Default

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub fpr_kirimbysupir_bli_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        load_print()
    End Sub

End Class