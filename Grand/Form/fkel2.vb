Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fkel2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private namakab As String
    Private handlekeys As Boolean

    Private Sub kosongkan()
        tkd_kec.Text = ""
        tnama_kec.Text = ""
        tkode.Text = ""
        tnama.Text = ""
    End Sub

    Private Sub isi()
        tkd_kec.EditValue = dv(position)("kd_kec").ToString
        tnama_kec.EditValue = dv(position)("nama_kec").ToString
        tkode.EditValue = dv(position)("kd_kel").ToString
        tnama.EditValue = dv(position)("nama_kel").ToString
        namakab = dv(position)("nama_kab").ToString
    End Sub

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqlc As String = String.Format("select kd_kel from ms_kel where kd_kel='{0}'", tkode.Text.Trim)
            Dim sql_insert As String = String.Format("insert into ms_kel (kd_kel,nama_kel,kd_kec) values('{0}','{1}','{2}')", tkode.Text.Trim, tnama.Text.Trim, tkd_kec.EditValue)
            Dim sql_update As String = String.Format("update ms_kel set nama_kel='{0}',kd_kec='{1}' where kd_kel='{2}'", tnama.Text.Trim, tkd_kec.EditValue, tkode.Text.Trim)

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

                            Clsmy.InsertToLog(cn, "btkel", 1, 0, 0, 0, tkode.Text.Trim, "", sqltrans)

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

                        Clsmy.InsertToLog(cn, "btkel", 1, 0, 0, 0, tkode.Text.Trim, "", sqltrans)

                        insertview()
                    End If
                Else
                    comd = New OleDbCommand(sql_insert, cn, sqltrans)
                    comd.ExecuteNonQuery()

                    Clsmy.InsertToLog(cn, "btkel", 1, 0, 0, 0, tkode.Text.Trim, "", sqltrans)

                    insertview()
                End If

                dread.Close()


            Else
                comd = New OleDbCommand(sql_update, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btkel", 0, 1, 0, 0, tkode.Text.Trim, "", sqltrans)

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

        'dv(position)("kd_kec") = tkode.Text.Trim
        dv(position)("nama_kel") = tnama.Text.Trim
        dv(position)("kd_kec") = tkd_kec.EditValue
        dv(position)("nama_kec") = tnama_kec.Text.Trim
        dv(position)("kd_kec") = tkd_kec.Text.Trim
        dv(position)("nama_kab") = namakab

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("kd_kel") = tkode.Text.Trim
        orow("nama_kel") = tnama.Text.Trim
        orow("kd_kec") = tkd_kec.EditValue
        orow("nama_kec") = tnama_kec.Text.Trim
        orow("kd_kec") = tkd_kec.Text.Trim
        orow("nama_kab") = namakab

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
            tkd_kec.Focus()
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

    Private Sub tnama_kec_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_kec.LostFocus
        If tkd_kec.Text.Trim.Equals("") Then
            tnama_kec.Text = ""
            namakab = ""
        End If
    End Sub

    Private Sub tnama_kec_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tnama_kec.KeyDown
        'handlekeys = Clsmy.cek_keys(e)
    End Sub

    Private Sub tnama_kec_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles tnama_kec.KeyPress

        'If handlekeys = True Then
        '    Return
        'End If

        'Dim ftext As String = e.KeyChar

        'e.KeyChar = ""

    End Sub

    Private Sub tkd_kec_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_kec.KeyDown
        If e.KeyCode = Keys.F4 Then

            bts_kec_Click(sender, Nothing)

        End If
    End Sub

    Private Sub bts_kec_Click(sender As System.Object, e As System.EventArgs) Handles bts_kec.Click
        Dim fs As New fskec With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .ftext = ""}
        fs.ShowDialog(Me)

        namakab = fs.get_nama_kab
        tkd_kec.EditValue = fs.get_KODE
        tnama_kec.EditValue = fs.get_NAMA
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
                        namakab = dread("nama_kab").ToString
                        hasil = True
                    End If
                End If

                If hasil = False Then
                    tnama_kec.Text = ""
                    namakab = ""
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

            tnama_kec.Text = "" : namakab = ""

        End If
    End Sub
End Class