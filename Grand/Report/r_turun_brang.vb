'Imports System.Drawing.Printing
'Imports DevExpress.XtraReports.UI

'Imports System.Data
'Imports System.Data.OleDb
'Imports Grand.Clsmy

Public Class r_turun_brang

    'Private Sub XrSubreport1_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs)

    '    Dim hasil As Boolean = True
    '    Dim cn As OleDbConnection = Nothing

    '    Try

    '        cn = New OleDbConnection
    '        cn = Clsmy.open_conn

    '        Dim sql As String = String.Format("SELECT count(trturun_br3.nobukti)as jml " & _
    '        "FROM trturun_br3 INNER JOIN " & _
    '        "ms_barang ON trturun_br3.kd_barang = ms_barang.kd_barang where trturun_br3.nobukti='{0}'", GetCurrentColumnValue("nobukti"))

    '        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
    '        Dim drd As OleDbDataReader = cmd.ExecuteReader

    '        If drd.Read Then
    '            If drd(0).ToString.Equals("") Then
    '                hasil = False
    '            Else

    '                If drd(0) = 0 Then
    '                    hasil = False
    '                Else
    '                    hasil = True
    '                End If

    '            End If
    '        Else
    '            hasil = False
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

    '    If hasil = True Then
    '        CType((CType(sender, XRSubreport)).ReportSource, r_turun_brang2).Parameters("cnobukti").Value = GetCurrentColumnValue("nobukti")
    '    Else
    '        e.Cancel = True
    '    End If


    'End Sub

End Class