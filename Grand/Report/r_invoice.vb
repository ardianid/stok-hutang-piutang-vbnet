Imports System.Drawing.Printing
Imports DevExpress.XtraReports.UI

Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class r_invoice

    Private nobukti As String

    Private Sub XrSubreport1_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles XrSubreport1.BeforePrint

        nobukti = GetCurrentColumnValue("nobukti")

        '   CType((CType(sender, XRSubreport)).ReportSource.DataSource
        CType((CType(sender, XRSubreport)).ReportSource, r_invoice2).Parameters("cnobukti").Value = GetCurrentColumnValue("nobukti")

    End Sub

    Private Sub Detail_BeforePrint(sender As System.Object, e As System.Drawing.Printing.PrintEventArgs) Handles Detail.BeforePrint
        'Dim kembali As Integer = GetCurrentColumnValue("kembali")
        'If kembali = 1 Then
        '    lblkembali.Text = "* KEMBALI BOTOL GRAND 5 GALLON"
        '    ' Else
        '    '    lblkembali.Text = ""
        'End If
    End Sub

    'Private Sub GroupFooter2_BeforePrint(sender As System.Object, e As System.Drawing.Printing.PrintEventArgs) Handles GroupFooter2.BeforePrint

    '    Dim nobukti As String = GetCurrentColumnValue("nobukti")

    '    Dim sql2 As String = String.Format("SELECT ms_barang.nama_barang, trfaktur_to3.qty, trfaktur_to3.satuan, trfaktur_to3.harga, trfaktur_to3.jumlah, trfaktur_to3.nobukti " & _
    '    "FROM   trfaktur_to3 INNER JOIN " & _
    '    "ms_barang ON trfaktur_to3.kd_barang = ms_barang.kd_barang WHERE trfaktur_to3.nobukti='{0}'", nobukti)

    '    Dim cn As OleDbConnection = Nothing

    '    Try

    '        cn = New OleDbConnection
    '        cn = Clsmy.open_conn

    '        Dim cmd As OleDbCommand = New OleDbCommand(sql2, cn)
    '        Dim drd As OleDbDataReader = cmd.ExecuteReader

    '        If drd.Read Then

    '            If Not drd("nama_barang").ToString.Equals("") Then

    '                lblkembali.Text = drd("nama_barang").ToString

    '                If drd("qty") = 0 Then
    '                    lblqty_kmb.Text = ""
    '                Else
    '                    lblqty_kmb.Text = drd("qty").ToString
    '                End If

    '                lblsatuan_kmb.Text = drd("satuan").ToString

    '                If drd("harga") = 0 Then
    '                    lblharga_kmb.Text = ""
    '                Else
    '                    lblharga_kmb.Text = drd("harga").ToString
    '                End If

    '                If drd("jumlah") = 0 Then
    '                    lbljumlah_kmb.Text = ""
    '                Else
    '                    lbljumlah_kmb.Text = drd("jumlah").ToString
    '                End If

    '            End If

    '        End If
    '        drd.Close()

    '    Catch ex As Exception
    '        MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
    '    Finally


    '        If Not cn Is Nothing Then
    '            If cn.State = ConnectionState.Open Then
    '                cn.Close()
    '            End If
    '        End If
    '    End Try

    'End Sub


    Private Sub r_invoice_AfterPrint(sender As Object, e As System.EventArgs) Handles Me.AfterPrint
        'cekjmlprint()
    End Sub

End Class