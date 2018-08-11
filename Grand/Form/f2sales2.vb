Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class f2sales2

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
        tkode.EditValue = dv(position)("kd_sales2").ToString
        tnama.EditValue = dv(position)("kd_karyawan").ToString
        tket.EditValue = dv(position)("note").ToString

        If dv(position)("aktif").ToString.Equals("1") Then
            caktif.Checked = True
        Else
            caktif.Checked = False
        End If

        tspv.EditValue = dv(position)("kd_sales1").ToString

    End Sub

    Private Sub isi_karyawan()

        Const sql = "select kd_karyawan,nama_karyawan from ms_pegawai where aktif=1 and bagian='SALES' and kd_karyawan not in (select kd_karyawan from ms_sales1 where aktif=1)"

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

    Private Sub isi_spv()

        Const sql = "SELECT     ms_sales1.kd_sales1, ms_sales1.kd_karyawan, ms_pegawai.nama_karyawan " & _
                "FROM         ms_sales1 INNER JOIN " & _
                "ms_pegawai ON ms_sales1.kd_karyawan = ms_pegawai.kd_karyawan where ms_sales1.aktif=1 and ms_pegawai.aktif=1"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet
        Dim dvs As DataView

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvs = dvm.CreateDataView(ds.Tables(0))

            tspv.Properties.DataSource = dvs

            tspv.ItemIndex = 0

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

            Dim sqlc As String = String.Format("select kd_sales2 from ms_sales2 where (kd_sales2='{0}' or (kd_karyawan='{1}' and kd_sales1='{2}'))", tkode.Text.Trim, tnama.EditValue, tspv.EditValue)
            Dim sql_insert As String = String.Format("insert into ms_sales2 (kd_sales2,kd_sales1,kd_karyawan,note,aktif) values('{0}','{1}','{2}','{3}',{4})", tkode.Text.Trim, tspv.EditValue, tnama.EditValue, tket.Text.Trim, kaktif)
            Dim sql_update As String = String.Format("update ms_sales2 set kd_karyawan='{0}',note='{1}',aktif={2},kd_sales1='{3}' where kd_sales2='{4}'", tnama.EditValue, tket.Text.Trim, kaktif, tspv.EditValue, tkode.Text.Trim)

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

                            Clsmy.InsertToLog(cn, "btlevsales2", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

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

                        Clsmy.InsertToLog(cn, "btlevsales2", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                        insertview()
                    End If
                Else
                    comd = New OleDbCommand(sql_insert, cn, sqltrans)
                    comd.ExecuteNonQuery()

                    Clsmy.InsertToLog(cn, "btlevsales2", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                    insertview()
                End If

                dread.Close()


            Else
                comd = New OleDbCommand(sql_update, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btlevsales2", 0, 1, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

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

        dv(position)("kd_sales2") = tkode.Text.Trim
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
        dv(position)("kd_sales1") = tspv.EditValue
        dv(position)("supervisor") = tspv.Text.Trim

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("kd_sales2") = tkode.Text.Trim
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
        orow("kd_sales1") = tspv.EditValue
        orow("supervisor") = tspv.Text.Trim

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

        If tspv.Text.Trim.Length = 0 Then
            MsgBox("Supervisor tidak boleh kosong...", vbOKOnly + vbExclamation, "Informasi")
            tspv.Focus()
            Return
        End If

        simpan()

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fkab2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If addstat = True Then
            tspv.Focus()
        Else
            tnama.Focus()
        End If
    End Sub

    Private Sub fkab2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        isi_karyawan()
        isi_spv()

        If addstat = True Then

            tkode.Enabled = True
            kosongkan()
        Else
            tkode.Enabled = False
            isi()
        End If

    End Sub

End Class