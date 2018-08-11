Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fgroup2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private Sub kosongkan()
        tkode.Text = ""
        tnama.Text = ""
        tnpw.Text = ""
        talamat.Text = ""
    End Sub

    Private Sub isi()
        tkode.EditValue = dv(position)("kd_group").ToString
        tnama.EditValue = dv(position)("nama_np").ToString
        tnpw.EditValue = dv(position)("npw").ToString
        talamat.EditValue = dv(position)("alamat_np").ToString
        ttipe.EditValue = dv(position)("tipe").ToString
    End Sub

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqlc As String = String.Format("select kd_group from ms_group where kd_group='{0}'", tkode.Text.Trim)
            Dim sql_insert As String = String.Format("insert into ms_group (kd_group,npw,nama_np,alamat_np,tipe) values('{0}','{1}','{2}','{3}','{4}')", tkode.Text.Trim, tnpw.Text.Trim, tnama.Text.Trim, talamat.Text.Trim, ttipe.EditValue)
            Dim sql_update As String = String.Format("update ms_group set npw='{0}',nama_np='{1}',alamat_np='{2}',tipe='{3}' where kd_group='{4}'", tnpw.Text.Trim, tnama.Text.Trim, talamat.Text.Trim, ttipe.EditValue, tkode.Text.Trim)

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

                            Clsmy.InsertToLog(cn, "btgroup", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                            insertview()
                        Else

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox("Kode/Nama sudah ada ...", vbOKOnly + vbExclamation, "Informasi")
                            tkode.Focus()
                            Return
                        End If
                    Else
                        comd = New OleDbCommand(sql_insert, cn, sqltrans)
                        comd.ExecuteNonQuery()

                        Clsmy.InsertToLog(cn, "btgroup", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                        insertview()
                    End If
                Else
                    comd = New OleDbCommand(sql_insert, cn, sqltrans)
                    comd.ExecuteNonQuery()

                    Clsmy.InsertToLog(cn, "btgroup", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                    insertview()
                End If

                dread.Close()


            Else
                comd = New OleDbCommand(sql_update, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btgroup", 0, 1, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

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

        dv(position)("kd_group") = tkode.Text.Trim
        dv(position)("npw") = tnpw.Text.Trim
        dv(position)("nama_np") = tnama.Text.Trim
        dv(position)("alamat_np") = talamat.Text.Trim
        dv(position)("tipe") = ttipe.Text.Trim

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        
        orow("kd_group") = tkode.Text.Trim
        orow("npw") = tnpw.Text.Trim
        orow("nama_np") = tnama.Text.Trim
        orow("alamat_np") = talamat.Text.Trim
        orow("tipe") = ttipe.Text.Trim


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

        If tnpw.Text.Trim.Length > 0 Then
            If tnpw.Text.Trim.Length < 20 Or tnpw.Text.Trim.Length > 20 Then
                MsgBox("NPWP harus 20 Digit...", vbOKOnly + vbInformation, "Informasi")
                tnpw.Focus()
                Return
            End If
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
            tnpw.Focus()
        End If
    End Sub

    Private Sub fkab2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ttipe.SelectedIndex = 0

        If addstat = True Then

            tkode.Enabled = True
            kosongkan()
        Else
            tkode.Enabled = False
            isi()
        End If

    End Sub

End Class