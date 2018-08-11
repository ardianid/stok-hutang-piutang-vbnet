Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpr_pasar

    Private Sub tkd_kec_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_kec.KeyDown
        If e.KeyCode = Keys.F4 Then

            bts_kec_Click(sender, Nothing)

        End If
    End Sub

    Private Sub bts_kec_Click(sender As System.Object, e As System.EventArgs) Handles bts_kec.Click
        Dim fs As New fskec With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .ftext = ""}
        fs.ShowDialog(Me)

        tkd_kec.EditValue = fs.get_KODE
        tnama_kec.EditValue = fs.get_NAMA
    End Sub

    Private Sub tkd_kec_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_kec.LostFocus
        If tkd_kec.Text.Trim.Length = 0 Then
            tnama_kec.Text = ""
        End If
    End Sub

    Private Sub tkd_kec_Validated(sender As System.Object, e As System.EventArgs) Handles tkd_kec.Validated
        If Not tkd_kec.Text.Trim.Equals("") Then

            Dim cn As OleDbConnection = Nothing

            Try

                Dim sql As String = String.Format("select a.kd_kec,a.nama_kec,b.kd_kab,b.nama_kab from ms_kec a inner join ms_kab b on a.kd_kab=b.kd_kab where a.kd_kec='{0}'", tkd_kec.Text.Trim)

                cn = New OleDbConnection()
                cn = Clsmy.open_conn

                Dim comd As New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                Dim hasil As Boolean = False

                If dread.HasRows Then
                    If dread.Read Then
                        tnama_kec.EditValue = dread("nama_kec").ToString
                        hasil = True
                    End If
                End If

                If hasil = False Then
                    tnama_kec.Text = ""
                    tkd_kec.Text = ""
                End If

            Catch ex As Exception
                MsgBox(ex.ToString)
            Finally

                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If
            End Try

        Else

            tnama_kec.Text = ""

        End If
    End Sub

    Private Sub bts_kel_Click(sender As System.Object, e As System.EventArgs) Handles bts_kel.Click

        Dim fs As New fskel With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_kel.EditValue = fs.get_KODE
        tnama_kel.EditValue = fs.get_NAMA

    End Sub

    Private Sub tkd_kel_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_kel.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_kel_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_kel_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_kel.LostFocus
        If tkd_kel.Text.Trim.Length = 0 Then
            tnama_kel.Text = ""
        End If
    End Sub

    Private Sub tkd_kel_Validated(sender As System.Object, e As System.EventArgs) Handles tkd_kel.Validated

        If tkd_kel.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select a.kd_kel,a.nama_kel,b.kd_kec,b.nama_kec,c.kd_kab,c.nama_kab from ms_kel a inner join ms_kec b on a.kd_kec=b.kd_kec inner join ms_kab c on b.kd_kab=c.kd_kab where a.kd_kel='{0}'", tkd_kel.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_kel.EditValue = dread("kd_kel").ToString
                        tnama_kel.EditValue = dread("nama_kel").ToString
                        

                    Else
                        '  tkd_kel.EditValue = ""
                        tnama_kel.EditValue = ""
                       
                    End If
                Else
                    '   tkd_kel.EditValue = ""
                    tnama_kel.EditValue = ""
                   
                End If


                dread.Close()

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Finally


                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If
            End Try

        End If

    End Sub

    Private Sub fpr_pasar_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tkd_kec.Focus()
    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        Cursor = Cursors.WaitCursor

        Dim sql As String = "SELECT     ms_kec.nama_kec, ms_kel.nama_kel, ms_pasar.kd_pasar, ms_pasar.nama_pasar " & _
            "FROM         ms_pasar INNER JOIN  " & _
            "ms_kel ON ms_pasar.kd_kel = ms_kel.kd_kel INNER JOIN " & _
            "ms_kec ON ms_kel.kd_kec = ms_kec.kd_kec"

        If Not tkd_kec.Text.Trim.Length = 0 Then
            sql = String.Format("{0} and ms_kec.kd_kec='{1}'", sql, tkd_kec.Text.Trim)
        End If

        If Not tkd_kel.Text.Trim.Length = 0 Then
            sql = String.Format("{0} andms_kel.kd_kel='{1}'", sql, tkd_kec.Text.Trim)
        End If

        Dim fs As New fpr_pasar2 With {.WindowState = FormWindowState.Maximized, .sql = sql}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub

End Class