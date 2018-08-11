Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class finfo

    Private dvmanager As Data.DataViewManager
    Public dv1 As Data.DataView

    'Private Sub load()
    '    Const sql As String = "select '' as ket1,'' as ket2 from ms_usersys where namauser='xxx1xxxxxx'"
    '    Dim cn As OleDbConnection = Nothing
    '    Dim ds As DataSet

    '    grid1.DataSource = Nothing

    '    Try

    '        open_wait()

    '        dv1 = Nothing

    '        cn = New OleDbConnection
    '        cn = Clsmy.open_conn

    '        ds = New DataSet()
    '        ds = Clsmy.GetDataSet(sql, cn)

    '        dvmanager = New DataViewManager(ds)
    '        dv1 = dvmanager.CreateDataView(ds.Tables(0))


    '        grid1.DataSource = dv1

    '        close_wait()


    '    Catch ex As OleDb.OleDbException
    '        close_wait()
    '        MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
    '    Finally


    '        If Not cn Is Nothing Then
    '            If cn.State = ConnectionState.Open Then
    '                cn.Close()
    '            End If
    '        End If

    '    End Try
    'End Sub

    Private Sub finfo_Layout(sender As Object, e As System.Windows.Forms.LayoutEventArgs) Handles Me.Layout
        ' load()
    End Sub
End Class