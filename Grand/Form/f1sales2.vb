Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class f1sales2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private Sub kosongkan()
        tkode.Text = ""
        tnama.Text = ""
        tket.Text = ""
        caktif.Checked = True
    End Sub

    Private Sub isi()
        tkode.EditValue = dv(position)("kd_sales1").ToString
        tnama.EditValue = dv(position)("kd_karyawan").ToString
        tket.EditValue = dv(position)("note").ToString

        If dv(position)("aktif").ToString.Equals("1") Then
            caktif.Checked = True
        Else
            caktif.Checked = False
        End If

    End Sub

    Private Sub isi_karyawan()

        Const sql = "select kd_karyawan,nama_karyawan from ms_pegawai where aktif=1 and bagian='SALES'"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet
        Dim dvs As DataView

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvs = dvm.CreateDataView(ds.Tables(0))

            tnama.Properties.DataSource = dvs

            tnama.ItemIndex = 0

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

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            Dim kaktif As Integer
            If caktif.Checked = True Then
                kaktif = 1
            Else
                kaktif = 0
            End If

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqlc As String = String.Format("select kd_sales1 from ms_sales1 where (kd_sales1='{0}' or kd_karyawan='{1}')", tkode.Text.Trim, tnama.EditValue)
            Dim sql_insert As String = String.Format("insert into ms_sales1 (kd_sales1,kd_karyawan,note,aktif) values('{0}','{1}','{2}',{3})", tkode.Text.Trim, tnama.EditValue, tket.Text.Trim, kaktif)
            Dim sql_update As String = String.Format("update ms_sales1 set kd_karyawan='{0}',note='{1}',aktif={2} where kd_sales1='{3}'", tnama.EditValue, tket.Text.Trim, kaktif, tkode.Text.Trim)

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

                            Clsmy.InsertToLog(cn, "btlevsales1", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

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

                        Clsmy.InsertToLog(cn, "btlevsales1", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                        insertview()
                    End If
                Else
                    comd = New OleDbCommand(sql_insert, cn, sqltrans)
                    comd.ExecuteNonQuery()

                    Clsmy.InsertToLog(cn, "btlevsales1", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                    insertview()
                End If

                dread.Close()


            Else
                comd = New OleDbCommand(sql_update, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btlevsales1", 0, 1, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

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

        dv(position)("kd_sales1") = tkode.Text.Trim
        dv(position)("kd_karyawan") = tnama.EditValue
        dv(position)("nama_karyawan") = tnama.Text.Trim
        dv(position)("note") = tket.Text.Trim

        Dim kaktif As Integer
        If caktif.Checked = True Then
            kaktif = 1
        Else
            kaktif = 0
        End If

        dv(position)("aktif") = kaktif

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("kd_sales1") = tkode.Text.Trim
        orow("kd_karyawan") = tnama.EditValue
        orow("nama_karyawan") = tnama.Text.Trim
        orow("note") = tket.Text.Trim

        Dim kaktif As Integer
        If caktif.Checked = True Then
            kaktif = 1
        Else
            kaktif = 0
        End If

        orow("aktif") = kaktif

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

        isi_karyawan()

        If addstat = True Then

            tkode.Enabled = True
            kosongkan()
        Else
            tkode.Enabled = False
            isi()
        End If

    End Sub


End Class