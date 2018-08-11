Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fgudang2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private Sub kosongkan()
        tkode.Text = ""
        tnama.Text = ""
        tket.Text = ""

        cdef.Checked = False

    End Sub

    Private Sub isi_nopol()

        Const sql As String = "select * from ms_kendaraan where aktif=1"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet
        Dim dvg As DataView

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvg = dvm.CreateDataView(ds.Tables(0))

            tnopol.Properties.DataSource = dvg

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

    Private Sub isi()
        tkode.EditValue = dv(position)("kd_gudang").ToString
        tnama.EditValue = dv(position)("nama_gudang").ToString
        tket.EditValue = dv(position)("note").ToString
        ttipe.EditValue = dv(position)("tipe_gudang").ToString

        If dv(position)("tipe_gudang").ToString.Equals("MOBIL") Then
            tnopol.EditValue = dv(position)("nopol").ToString
        End If

        If dv(position)("def_kosong").ToString.Equals("1") Then
            cdef.Checked = True
        Else
            cdef.Checked = False
        End If

        ttipe_SelectedIndexChanged(Nothing, Nothing)
        cdef_CheckedChanged(Nothing, Nothing)

    End Sub

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim nopol As String = ""
            If ttipe.SelectedIndex = 1 Then
                nopol = tnopol.EditValue
            Else
                nopol = "-"
            End If

            Dim defkosong As Integer
            If cdef.Checked = True Then
                defkosong = 1
            Else
                defkosong = 0
            End If

            Dim sqlc As String = String.Format("select kd_gudang from ms_gudang where kd_gudang='{0}'", tkode.Text.Trim)
            Dim sql_insert As String = String.Format("insert into ms_gudang (kd_gudang,nama_gudang,tipe_gudang,note,nopol,def_kosong) values('{0}','{1}','{2}','{3}','{4}',{5})", tkode.Text.Trim, tnama.EditValue, ttipe.EditValue, tket.Text.Trim, nopol, defkosong)
            Dim sql_update As String = String.Format("update ms_gudang set nama_gudang='{0}',tipe_gudang='{1}',note='{2}',nopol='{3}',def_kosong='{4}' where kd_gudang='{5}'", tnama.EditValue, ttipe.EditValue, tket.Text.Trim, nopol, defkosong, tkode.Text.Trim)

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

                            Clsmy.InsertToLog(cn, "btgudang", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

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

                        Clsmy.InsertToLog(cn, "btgudang", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                        insertview()
                    End If
                Else
                    comd = New OleDbCommand(sql_insert, cn, sqltrans)
                    comd.ExecuteNonQuery()

                    Clsmy.InsertToLog(cn, "btgudang", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                    insertview()
                End If

                dread.Close()


            Else
                comd = New OleDbCommand(sql_update, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btgudang", 0, 1, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

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

        dv(position)("kd_gudang") = tkode.Text.Trim
        dv(position)("nama_gudang") = tnama.EditValue
        dv(position)("tipe_gudang") = ttipe.EditValue
        dv(position)("note") = tket.Text.Trim

        If cdef.Checked = True Then
            dv(position)("def_kosong") = 1
        Else
            dv(position)("def_kosong") = 0
        End If

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("kd_gudang") = tkode.Text.Trim
        orow("nama_gudang") = tnama.EditValue
        orow("tipe_gudang") = ttipe.EditValue
        orow("note") = tket.Text.Trim

        If cdef.Checked = True Then
            orow("def_kosong") = 1
        Else
            orow("def_kosong") = 0
        End If

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

        If ttipe.SelectedIndex = 1 Then
            If tnopol.Text.Trim.Length = 0 Then
                MsgBox("Nopol harus diisi..", vbOKOnly + vbExclamation, "Informasi")

                If tnopol.Enabled = True Then
                    tnopol.Focus()
                Else
                    ttipe.Focus()
                End If

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
            tnama.Focus()
        End If
    End Sub

    Private Sub fkab2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        isi_nopol()

        If addstat = True Then

            ttipe.SelectedIndex = 0

            tnopol.Enabled = True
            ttipe.Enabled = True

            tkode.Enabled = True
            kosongkan()
        Else

            tkode.Enabled = False

            tnopol.Enabled = False
            ttipe.Enabled = False

            isi()
        End If

    End Sub

    Private Sub ttipe_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ttipe.SelectedIndexChanged
        If ttipe.SelectedIndex = 1 Then
            lbnopol.Visible = True
            tnopol.Visible = True

            LabelControl5.Visible = False
            cdef.Visible = False

        Else
            lbnopol.Visible = False
            tnopol.Visible = False

            LabelControl5.Visible = True
            cdef.Visible = True

        End If
    End Sub

    Private Sub cdef_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cdef.CheckedChanged

        If cdef.Checked = True Then
            cdef.Text = "&Ya"
        Else
            cdef.Text = "&Tidak"
        End If

    End Sub


    Private Sub tnopol_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tnopol.EditValueChanged

    End Sub


End Class