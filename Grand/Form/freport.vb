Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class freport

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Public jenislap As Integer

    Private Sub loaddata()

        Dim sql As String = String.Format("select b1.namaform,b1.namamenu,b1.keterangan,b1.indialog,b1.namagroup from ms_usersys3 a1 inner join ms_menu2 b1 on a1.kodemenu=b1.kodemenu " & _
                "where a1.t_lap=1 and b1.inform=0 and a1.namauser='{0}'", userprog)

        If RadioGroup1.SelectedIndex = 0 Then
            sql = String.Format(" {0} and b1.jenislap=0", sql)
        ElseIf RadioGroup1.SelectedIndex = 1 Then
            sql = String.Format(" {0} and b1.jenislap=1", sql)
        ElseIf RadioGroup1.SelectedIndex = 2 Then
            sql = String.Format(" {0} and b1.jenislap=2", sql)
        End If

        sql = String.Format(" {0} order by b1.namagroup,b1.namamenu", sql)

        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing

        Try

            '  open_wait()

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet
            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            grid1.DataSource = dv1

            '  close_wait()

        Catch ex As OleDb.OleDbException
            ' close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub cek_statradio()

        Dim sql As String = String.Format("select a.jenislap,count(a.kodemenu) as jml from ms_menu2 a inner join ms_usersys3 b on a.kodemenu=b.kodemenu " & _
            "where b.namauser='{0}' group by a.jenislap", userprog)

        Dim cn As OleDbConnection = Nothing
        Try
            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.HasRows Then
                ' If Not drd(0).ToString.Equals("") Then

                Dim jd1 As Integer = 0
                Dim jd2 As Integer = 0
                Dim jd3 As Integer = 0

                While drd.Read

                    If Integer.Parse(drd(0).ToString) = 0 Then
                        jd1 = Integer.Parse(drd(1).ToString)
                    ElseIf Integer.Parse(drd(0).ToString) = 1 Then
                        jd2 = Integer.Parse(drd(1).ToString)
                    ElseIf Integer.Parse(drd(0).ToString) = 2 Then
                        jd3 = Integer.Parse(drd(1).ToString)
                    End If

                End While

                If jd1 > jd2 And jd1 > jd3 Then
                    RadioGroup1.SelectedIndex = 0
                ElseIf jd2 > jd1 And jd2 > jd3 Then
                    RadioGroup1.SelectedIndex = 1
                ElseIf jd3 > jd1 And jd3 > jd2 Then
                    RadioGroup1.SelectedIndex = 2
                End If

                '  End If

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

    Private Sub fload_reports_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        cek_statradio()

        loaddata()
    End Sub

    Private Sub SimpleButton2_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim indialog As String = dv1(Me.BindingContext(dv1).Position)("indialog").ToString

        Dim sform As String = dv1(Me.BindingContext(dv1).Position)("namaform").ToString
        Dim frm As Form = ObjectFinder.CreateForm(sform)

        If indialog.Equals("1") Then
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.ShowDialog(Me)
        Else
            futama.LoadOtherForm(frm)
        End If

    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As System.EventArgs) Handles GridView1.DoubleClick
        SimpleButton1_Click(sender, Nothing)
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = 13 Then
            SimpleButton1_Click(sender, Nothing)
        End If
    End Sub

    Private Sub RadioGroup1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles RadioGroup1.SelectedIndexChanged
        loaddata()
    End Sub

End Class