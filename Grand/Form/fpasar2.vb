Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpasar2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private namakec As String

    Private Sub kosongkan()
        tkd_kel.Text = ""
        tnama_kel.Text = ""
        tkode.Text = ""
        tnama.Text = ""
    End Sub

    Private Sub isi()
        tkd_kel.EditValue = dv(position)("kd_kel").ToString
        tnama_kel.EditValue = dv(position)("nama_kel").ToString
        namakec = dv(position)("nama_kec").ToString

        tkode.EditValue = dv(position)("kd_pasar").ToString
        tnama.EditValue = dv(position)("nama_pasar").ToString

    End Sub

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqlc As String = String.Format("select kd_pasar from ms_pasar where kd_pasar='{0}'", tkode.Text.Trim)
            Dim sql_insert As String = String.Format("insert into ms_pasar (kd_pasar,nama_pasar,kd_kel) values('{0}','{1}','{2}')", tkode.Text.Trim, tnama.Text.Trim, tkd_kel.EditValue)
            Dim sql_update As String = String.Format("update ms_pasar set nama_pasar='{0}',kd_kel='{1}' where kd_pasar='{2}'", tnama.Text.Trim, tkd_kel.EditValue, tkode.Text.Trim)

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

                            Clsmy.InsertToLog(cn, "btpasar", 1, 0, 0, 0, tkode.Text.Trim, "", sqltrans)

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

                        Clsmy.InsertToLog(cn, "btpasar", 1, 0, 0, 0, tkode.Text.Trim, "", sqltrans)

                        insertview()
                    End If
                Else
                    comd = New OleDbCommand(sql_insert, cn, sqltrans)
                    comd.ExecuteNonQuery()

                    Clsmy.InsertToLog(cn, "btpasar", 1, 0, 0, 0, tkode.Text.Trim, "", sqltrans)

                    insertview()
                End If

                dread.Close()


            Else
                comd = New OleDbCommand(sql_update, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btpasar", 0, 1, 0, 0, tkode.Text.Trim, "", sqltrans)

                updateview()

            End If

            sqltrans.Commit()
            MsgBox("Data telah disimpan...", vbOKOnly + vbInformation, "Informasi")

            If addstat = True Then
                tkode.Text = ""
                tnama.Text = ""
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

        dv(position)("kd_pasar") = tkode.Text.Trim
        dv(position)("nama_pasar") = tnama.EditValue
        dv(position)("kd_kel") = tkd_kel.Text.Trim
        dv(position)("nama_kel") = tnama_kel.Text.Trim
        dv(position)("nama_kec") = namakec

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("kd_pasar") = tkode.Text.Trim
        orow("nama_pasar") = tnama.EditValue
        orow("kd_kel") = tkd_kel.Text.Trim
        orow("nama_kel") = tnama_kel.Text.Trim
        orow("nama_kec") = namakec

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
            tkd_kel.Focus()
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

    Private Sub tnama_kec_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_kel.LostFocus
        If tkd_kel.Text.Trim.Equals("") Then
            tnama_kel.Text = ""
            namakec = ""
        End If
    End Sub

    Private Sub tkd_kec_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_kel.KeyDown
        If e.KeyCode = Keys.F4 Then

            bts_kec_Click(sender, Nothing)

        End If
    End Sub

    Private Sub bts_kec_Click(sender As System.Object, e As System.EventArgs) Handles bts_kec.Click
        Dim fs As New fskel With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        namakec = fs.get_nama_kec
        tkd_kel.EditValue = fs.get_KODE
        tnama_kel.EditValue = fs.get_NAMA
    End Sub

    Private Sub tkd_kec_Validated(sender As System.Object, e As System.EventArgs) Handles tkd_kel.Validated
        If Not tkd_kel.Text.Trim.Equals("") Then

            Dim cn As OleDbConnection = Nothing

            Try

                Dim sql As String = String.Format("select a.kd_kel,a.nama_kel,b.nama_kec from ms_kel a inner join ms_kec b on a.kd_kec=b.kd_kec where a.kd_kel='{0}'", tkd_kel.Text.Trim)

                cn = New OleDbConnection()
                cn = Clsmy.open_conn

                Dim comd As New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                Dim hasil As Boolean = False

                If dread.HasRows Then
                    If dread.Read Then
                        tnama_kel.EditValue = dread("nama_kel").ToString
                        namakec = dread("nama_kec").ToString
                        hasil = True
                    End If
                End If

                If hasil = False Then
                    tnama_kel.Text = ""
                    namakec = ""
                    tkd_kel.Text = ""
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

            tnama_kel.Text = "" : namakec = ""

        End If
    End Sub

End Class