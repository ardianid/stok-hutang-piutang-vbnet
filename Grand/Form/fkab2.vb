﻿Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fkab2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private Sub kosongkan()
        tkode.Text = ""
        tnama.Text = ""
    End Sub

    Private Sub isi()
        tkode.EditValue = dv(position)("kd_kab").ToString
        tnama.EditValue = dv(position)("nama_kab").ToString
    End Sub

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqlc As String = String.Format("select kd_kab from ms_kab where kd_kab='{0}'", tkode.Text.Trim)
            Dim sql_insert As String = String.Format("insert into ms_kab (kd_kab,nama_kab) values('{0}','{1}')", tkode.Text.Trim, tnama.Text.Trim)
            Dim sql_update As String = String.Format("update ms_kab set nama_kab='{0}' where kd_kab='{1}'", tnama.Text.Trim, tkode.Text.Trim)

            sqltrans = cn.BeginTransaction

            Dim comd As OleDbCommand

            If addstat = True Then

                Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
                Dim dread As OleDbDataReader = cmdc.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        Dim kdka As String = dread(0).ToString

                        If kdka.Trim.Length = 0 Then
                            comd = New OleDbCommand(sql_insert, cn, sqltrans)
                            comd.ExecuteNonQuery()

                            Clsmy.InsertToLog(cn, "btkab", 1, 0, 0, 0, tkode.Text.Trim, "", sqltrans)

                            insertview()
                        Else

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox("Kode sudah ada ...", vbOKOnly + vbExclamation, "Informasi")
                            tkode.Focus()
                            Return
                        End If
                    Else
                        comd = New OleDbCommand(sql_insert, cn, sqltrans)
                        comd.ExecuteNonQuery()

                        Clsmy.InsertToLog(cn, "btkab", 1, 0, 0, 0, tkode.Text.Trim, "", sqltrans)

                        insertview()
                    End If
                Else
                    comd = New OleDbCommand(sql_insert, cn, sqltrans)
                    comd.ExecuteNonQuery()

                    Clsmy.InsertToLog(cn, "btkab", 1, 0, 0, 0, tkode.Text.Trim, "", sqltrans)

                    insertview()
                End If

                dread.Close()
                

            Else
                comd = New OleDbCommand(sql_update, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btkab", 0, 1, 0, 0, tkode.Text.Trim, "", sqltrans)

                updateview()

            End If

            sqltrans.Commit()
            MsgBox("Data telah disimpan...", vbOKOnly + vbInformation, "Informasi")

            If addstat = True Then
                kosongkan()
                tkode.Focus()
            Else
                Me.Close()
            End If


        Catch ex As Exception
            close_wait()

            If Not IsNothing(sqltrans) Then
                sqltrans.Rollback()
            End If

            MsgBox(ex.ToString)
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try


    End Sub

    Private Sub updateview()

        dv(position)("kd_kab") = tkode.Text.Trim
        dv(position)("nama_kab") = tnama.Text.Trim

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("kd_kab") = tkode.Text.Trim
        orow("nama_kab") = tnama.Text.Trim

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