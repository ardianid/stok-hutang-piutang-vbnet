Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fkendaraan2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private Sub kosongkan()
        tnopol.Text = ""
        tket.Text = ""
        caktif.Checked = True
    End Sub

    Private Sub isi()
        tnopol.EditValue = dv(position)("nopol").ToString
        ttipe.EditValue = dv(position)("tipe").ToString
        tket.EditValue = dv(position)("note").ToString

        Dim takt As String = dv(position)("aktif").ToString
        If takt.Equals("1") Then
            caktif.Checked = True
        Else
            caktif.Checked = False
        End If


    End Sub

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            Dim taktif As Integer
            If caktif.Checked = True Then
                taktif = 1
            Else
                taktif = 0
            End If

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqlc As String = String.Format("select nopol from ms_kendaraan where nopol='{0}'", tnopol.Text.Trim)
            Dim sql_insert As String = String.Format("insert into ms_kendaraan (nopol,tipe,note,aktif) values('{0}','{1}','{2}',{3})", tnopol.Text.Trim, ttipe.Text.Trim, tket.Text.Trim, taktif)
            Dim sql_update As String = String.Format("update ms_kendaraan set tipe='{0}',note='{1}',aktif={2} where nopol='{3}'", ttipe.Text.Trim, tket.Text.Trim, taktif, tnopol.Text.Trim)

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

                            Clsmy.InsertToLog(cn, "btkendaraan", 1, 0, 0, 0, tnopol.Text.Trim, "", sqltrans)

                            insertview()
                        Else

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox("Kode sudah ada ...", vbOKOnly + vbExclamation, "Informasi")
                            tnopol.Focus()
                            Return
                        End If
                    Else
                        comd = New OleDbCommand(sql_insert, cn, sqltrans)
                        comd.ExecuteNonQuery()

                        Clsmy.InsertToLog(cn, "btkendaraan", 1, 0, 0, 0, tnopol.Text.Trim, "", sqltrans)

                        insertview()
                    End If
                Else
                    comd = New OleDbCommand(sql_insert, cn, sqltrans)
                    comd.ExecuteNonQuery()

                    Clsmy.InsertToLog(cn, "btkendaraan", 1, 0, 0, 0, tnopol.Text.Trim, "", sqltrans)

                    insertview()
                End If

                dread.Close()


            Else
                comd = New OleDbCommand(sql_update, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btkendaraan", 0, 1, 0, 0, tnopol.Text.Trim, "", sqltrans)

                updateview()

            End If

            sqltrans.Commit()
            MsgBox("Data telah disimpan...", vbOKOnly + vbInformation, "Informasi")

            If addstat = True Then
                kosongkan()
                tnopol.Focus()
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

        dv(position)("nopol") = tnopol.Text.Trim
        dv(position)("tipe") = ttipe.Text.Trim
        dv(position)("note") = tket.Text.Trim

        Dim taktif As Integer
        If caktif.Checked = True Then
            taktif = 1
        Else
            taktif = 0
        End If

        dv(position)("aktif") = taktif

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew

        orow("nopol") = tnopol.Text.Trim
        orow("tipe") = ttipe.Text.Trim
        orow("note") = tket.Text.Trim

        Dim taktif As Integer
        If caktif.Checked = True Then
            taktif = 1
        Else
            taktif = 0
        End If

        orow("aktif") = taktif

        dv.EndInit()

    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click


        If tnopol.Text.Trim.Length = 0 Then
            MsgBox("No Polisi tidak boleh kosong...", vbOKOnly + vbExclamation, "Informasi")
            tnopol.Focus()
            Return
        End If

        If ttipe.Text.Trim.Length = 0 Then
            MsgBox("Tipe tidak boleh kosong...", vbOKOnly + vbExclamation, "Informasi")
            ttipe.Focus()
            Return
        End If

        simpan()

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fkab2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If addstat = True Then
            tnopol.Focus()
        Else
            ttipe.Focus()
        End If
    End Sub

    Private Sub fkab2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If addstat = True Then

            tnopol.Enabled = True
            kosongkan()
        Else
            tnopol.Enabled = False
            isi()
        End If

    End Sub

End Class