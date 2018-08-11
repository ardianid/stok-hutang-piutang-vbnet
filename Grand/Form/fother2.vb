Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fother2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private Sub kosongkan()
        tval.Text = ""
    End Sub

    Private Sub isi()
        ttipe.EditValue = dv(position)("tipe").ToString
        tval.EditValue = dv(position)("alasan").ToString
    End Sub

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqlc As String = String.Format("select noid from ms_alasan where alasan='{0}' and tipe='{1}'", tval.Text.Trim, ttipe.EditValue)
            Dim sql_insert As String = String.Format("insert into ms_alasan (alasan,tipe) values('{0}','{1}')", tval.Text.Trim, ttipe.EditValue)
            Dim sql_update As String = String.Format("update ms_alasan set alasan='{0}',tipe='{1}' where noid={2}", tval.Text.Trim, ttipe.EditValue, dv(position)("noid").ToString)

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

                            Clsmy.InsertToLog(cn, "btother", 1, 0, 0, 0, tval.Text.Trim, ttipe.EditValue, sqltrans)

                            insertview()
                        Else

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox("Data sudah ada ...", vbOKOnly + vbExclamation, "Informasi")
                            tval.Focus()
                            Return
                        End If
                    Else
                        comd = New OleDbCommand(sql_insert, cn, sqltrans)
                        comd.ExecuteNonQuery()

                        Clsmy.InsertToLog(cn, "btother", 1, 0, 0, 0, tval.Text.Trim, ttipe.EditValue, sqltrans)

                        insertview()
                    End If
                Else
                    comd = New OleDbCommand(sql_insert, cn, sqltrans)
                    comd.ExecuteNonQuery()

                    Clsmy.InsertToLog(cn, "btother", 1, 0, 0, 0, tval.Text.Trim, ttipe.EditValue, sqltrans)

                    insertview()
                End If

                dread.Close()


            Else
                comd = New OleDbCommand(sql_update, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btother", 0, 1, 0, 0, tval.Text.Trim, ttipe.EditValue, sqltrans)

                updateview()

            End If

            sqltrans.Commit()
            MsgBox("Data telah disimpan...", vbOKOnly + vbInformation, "Informasi")

            If addstat = True Then
                kosongkan()
                ttipe.Focus()
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

        dv(position)("alasan") = tval.Text.Trim
        dv(position)("tipe") = ttipe.EditValue

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("noid") = 0
        orow("alasan") = tval.Text.Trim
        orow("tipe") = ttipe.EditValue

        dv.EndInit()

    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click


        If tval.Text.Trim.Length = 0 Then
            MsgBox("val tidak boleh kosong...", vbOKOnly + vbExclamation, "Informasi")
            tval.Focus()
            Return
        End If

        simpan()

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fkab2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If addstat = True Then
            ttipe.Focus()
        Else
            tval.Focus()
        End If
    End Sub

    Private Sub fkab2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ttipe.SelectedIndex = 0

        If addstat = True Then

            ttipe.Enabled = True
            kosongkan()
        Else
            ttipe.Enabled = False
            isi()
        End If

    End Sub

End Class