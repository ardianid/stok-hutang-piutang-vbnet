Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fklas2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private Sub kosongkan()
        tkode.Text = ""
        tnama.Text = ""
        tket.Text = ""
    End Sub

    Private Sub isi()
        tkode.EditValue = dv(position)("kd_klas").ToString
        tnama.EditValue = dv(position)("nama_klas").ToString
        tket.EditValue = dv(position)("ket").ToString
    End Sub

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqlin As String = String.Format("insert into ms_klas (kd_klas,nama_klas,ket) values('{0}','{1}','{2}')", tkode.Text.Trim, tnama.Text.Trim, tket.Text.Trim)
            Dim sqlup As String = String.Format("update ms_klas set nama_klas='{0}',ket='{1}' where kd_klas='{2}'", tnama.Text.Trim, tket.Text.Trim, tkode.Text.Trim)

            If addstat Then
                Using cmd As OleDbCommand = New OleDbCommand(sqlin, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

                insertview()

                tkode.Text = ""
                tnama.Text = ""
                tket.Text = ""

                tkode.Focus()

            Else

                Using cmd As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

                updateview()
                Me.Close()

            End If

        Catch ex As Exception
            If Not IsNothing(sqltrans) Then
                sqltrans.Rollback()
            End If

            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub updateview()

        dv(position)("kd_klas") = tkode.Text.Trim
        dv(position)("nama_klas") = tnama.Text.Trim
        dv(position)("ket") = tket.Text.Trim

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("kd_klas") = tkode.Text.Trim
        orow("nama_klas") = tnama.Text.Trim
        orow("ket") = tket.Text.Trim

        dv.EndInit()

    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click


        If tkode.Text.Trim.Length = 0 Then
            MsgBox("Kode tidak boleh kosong...", vbOKOnly + vbExclamation, "Informasi")
            tkode.Focus()
            Return
        End If

        If tnama.Text.Trim.Length = 0 Then
            MsgBox("Nama tidak boleh kosong...", vbOKOnly + vbExclamation, "Informasi")
            tnama.Focus()
            Return
        End If

        simpan()

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fkab2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If addstat = True Then
            tkode.Focus()
        Else
            tnama.Focus()
        End If
    End Sub

    Private Sub fkab2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If addstat = True Then

            tkode.Enabled = True
            kosongkan()
        Else
            tkode.Enabled = False
            isi()
        End If

    End Sub

End Class