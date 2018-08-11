Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fgiro2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private Sub kosongkan()
        
        tnogiro.Text = ""

        tkd_sales.Text = ""
        tnama_sales.Text = ""

        tkd_toko.Text = ""
        tnama_toko.Text = ""
        talamat_toko.Text = ""

        tjumlah.EditValue = 0
        tnote.Text = ""

    End Sub

    Private Sub isi()

        Dim nobukti As String = dv(position)("nogiro").ToString
        Dim sql As String = String.Format("SELECT     ms_giro.nogiro, ms_giro.tgl_giro,ms_giro.tgl_jt, ms_giro.namabank, ms_giro.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, ms_giro.kd_karyawan, " & _
        "ms_pegawai.nama_karyawan, ms_giro.jumlah, ms_giro.tgl_cair, ms_giro.note " & _
        "FROM         ms_giro INNER JOIN " & _
                      "ms_pegawai ON ms_giro.kd_karyawan = ms_pegawai.kd_karyawan INNER JOIN " & _
                      "ms_toko ON ms_giro.kd_toko = ms_toko.kd_toko where ms_giro.nogiro='{0}'", nobukti)

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim dread As OleDbDataReader = comd.ExecuteReader
            Dim hasil As Boolean

            If dread.HasRows Then
                If dread.Read Then

                    If Not dread("nogiro").ToString.Equals("") Then

                        hasil = True

                        tnogiro.EditValue = dread("nogiro").ToString
                        ttgl.EditValue = DateValue(dread("tgl_giro").ToString)

                        If Not dread("tgl_jt").ToString.Equals("") Then
                            ttgl_jt.EditValue = DateValue(dread("tgl_jt").ToString)
                        End If

                        tbank.EditValue = dread("namabank").ToString

                        tkd_toko.EditValue = dread("kd_toko").ToString
                        tnama_toko.EditValue = dread("nama_toko").ToString
                        talamat_toko.EditValue = dread("alamat_toko").ToString

                        tkd_sales.EditValue = dread("kd_karyawan").ToString
                        tnama_sales.EditValue = dread("nama_karyawan").ToString

                        tjumlah.EditValue = dread("jumlah").ToString

                        tnote.EditValue = dread("note").ToString


                    Else
                        hasil = False
                    End If


                Else
                    hasil = False
                End If
            Else
                hasil = False
            End If

            If hasil = False Then

                kosongkan()

            End If

            dread.Close()

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
            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            Dim cmd As OleDbCommand

            If addstat Then

                Dim sqlc As String = String.Format("select nogiro from ms_giro where nogiro='{0}'", tnogiro.Text.Trim)
                Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
                Dim drc As OleDbDataReader = cmdc.ExecuteReader

                If drc.Read Then
                    If Not (drc(0).ToString.Equals("")) Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox("No giro sudah ada...", vbOKOnly + vbInformation, "Informasi")
                        tnogiro.Focus()
                        drc.Close()
                        Return
                    End If
                End If

                drc.Close()

                '1 . update faktur
                Dim sqlin_faktur As String = String.Format("insert into ms_giro (nogiro,tgl_giro,namabank,kd_toko,kd_karyawan,note,jumlah,tgl_jt) values('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}')", _
                                    tnogiro.Text.Trim, convert_date_to_eng(ttgl.EditValue), tbank.EditValue, tkd_toko.Text.Trim, tkd_sales.Text.Trim, tnote.Text.Trim, Replace(tjumlah.EditValue, ",", "."), convert_date_to_eng(ttgl_jt.EditValue))


                cmd = New OleDbCommand(sqlin_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()

                
                Clsmy.InsertToLog(cn, "btgiro", 1, 0, 0, 0, tnogiro.Text.Trim, ttgl.EditValue, sqltrans)
               


            Else

                

                '1. update faktur
                Dim sqlup_faktur As String = String.Format("update ms_giro set tgl_giro='{0}',namabank='{1}',kd_toko='{2}',kd_karyawan='{3}',note='{4}',jumlah={5},tgl_jt='{6}' where nogiro='{7}'", convert_date_to_eng(ttgl.EditValue), tbank.EditValue, tkd_toko.Text.Trim, tkd_sales.Text.Trim, tnote.Text.Trim, Replace(tjumlah.EditValue, ",", "."), convert_date_to_eng(ttgl_jt.EditValue), tnogiro.Text.Trim)

                cmd = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btgiro", 0, 1, 0, 0, tnogiro.Text.Trim, ttgl.EditValue, sqltrans)


            End If



            If addstat = True Then
                insertview()
            Else
                updateview()
            End If

            sqltrans.Commit()

            close_wait()

            MsgBox("Data disimpan...", vbOKOnly + vbInformation, "Informasi")

            If addstat = True Then
                kosongkan()
                tnogiro.Focus()
            Else
                close_wait()
                Me.Close()
            End If


        Catch ex As Exception
            close_wait()

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

    Private Sub isiBank()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Const sql As String = "select alasan from ms_alasan where tipe='BANK' order by alasan asc"
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            tbank.Properties.Items.Clear()

            While drd.Read
                tbank.Properties.Items.Add(drd(0).ToString)
            End While


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

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("nogiro") = tnogiro.Text.Trim
        orow("tgl_giro") = ttgl.Text
        orow("tgl_jt") = ttgl_jt.Text
        orow("namabank") = tbank.Text.Trim
        orow("kd_toko") = tkd_toko.Text.Trim
        orow("nama_toko") = tnama_toko.Text.Trim
        orow("alamat_toko") = talamat_toko.Text.Trim
        orow("kd_karyawan") = tkd_sales.Text.Trim
        orow("nama_karyawan") = tnama_sales.Text.Trim
        orow("jumlah") = tjumlah.EditValue

        orow("stolak") = 0
        orow("sgunakan") = 0
        orow("sbatal") = 0

        dv.EndInit()

    End Sub

    Private Sub updateview()

        dv(position)("nogiro") = tnogiro.Text.Trim
        dv(position)("tgl_giro") = ttgl.Text
        dv(position)("tgl_jt") = ttgl_jt.Text
        dv(position)("namabank") = tbank.Text.Trim
        dv(position)("kd_toko") = tkd_toko.Text.Trim
        dv(position)("nama_toko") = tnama_toko.Text.Trim
        dv(position)("alamat_toko") = talamat_toko.Text.Trim
        dv(position)("kd_karyawan") = tkd_sales.Text.Trim
        dv(position)("nama_karyawan") = tnama_sales.Text.Trim
        dv(position)("jumlah") = tjumlah.EditValue

    End Sub

    Private Sub bts_sal_Click(sender As System.Object, e As System.EventArgs) Handles bts_sal.Click
        Dim fs As New fssales With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_sales.EditValue = fs.get_KODE
        tnama_sales.EditValue = fs.get_NAMA
    End Sub

    Private Sub tkd_sales_EditValueChanged(sender As Object, e As System.EventArgs) Handles tkd_sales.Validated
        If tkd_sales.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and bagian='SALES' and kd_karyawan='{0}'", tkd_sales.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_sales.EditValue = dread("kd_karyawan").ToString
                        tnama_sales.EditValue = dread("nama_karyawan").ToString

                        tkd_toko.Text = ""
                        tnama_toko.Text = ""
                        talamat_toko.Text = ""

                    Else
                        tkd_sales.EditValue = ""
                        tnama_sales.EditValue = ""

                        tkd_toko.Text = ""
                        tnama_toko.Text = ""
                        talamat_toko.Text = ""

                    End If
                Else
                    tkd_sales.EditValue = ""
                    tnama_sales.EditValue = ""

                    tkd_toko.Text = ""
                    tnama_toko.Text = ""
                    talamat_toko.Text = ""

                End If


                dread.Close()

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Finally


                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If
            End Try

        End If
    End Sub

    Private Sub tkd_sales_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_sales.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_sal_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_sales_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_sales.LostFocus
        If tkd_sales.Text.Trim.Length = 0 Then
            tkd_sales.EditValue = ""
            tnama_sales.EditValue = ""

            tkd_toko_EditValueChanged(sender, Nothing)


        End If
    End Sub

    Private Sub bts_toko_Click(sender As System.Object, e As System.EventArgs) Handles bts_toko.Click
        Dim fs As New fsoutlet With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .kdsales = tkd_sales.Text.Trim}
        fs.ShowDialog(Me)

        tkd_toko.EditValue = fs.get_KODE
        tnama_toko.EditValue = fs.get_NAMA
        talamat_toko.EditValue = fs.get_ALAMAT

        tkd_toko_EditValueChanged(sender, Nothing)


    End Sub

    Private Sub tkd_toko_EditValueChanged(sender As Object, e As System.EventArgs) Handles tkd_toko.Validated
        If tkd_toko.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select kd_toko,nama_toko,alamat_toko from ms_toko where kd_toko in (select ms_toko2.kd_toko from ms_toko2 where ms_toko2.kd_karyawan='{0}') and kd_toko='{1}' and aktif=1", tkd_sales.Text.Trim, tkd_toko.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_toko.EditValue = dread("kd_toko").ToString
                        tnama_toko.EditValue = dread("nama_toko").ToString
                        talamat_toko.EditValue = dread("alamat_toko").ToString

                    Else
                        tkd_toko.EditValue = ""
                        tnama_toko.EditValue = ""
                        talamat_toko.Text = ""


                    End If
                Else
                    tkd_toko.EditValue = ""
                    tnama_toko.EditValue = ""
                    talamat_toko.Text = ""

                End If


                dread.Close()

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Finally


                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If
            End Try

        End If
    End Sub

    Private Sub tkd_toko_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_toko.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_toko_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_toko_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_toko.LostFocus
        If tkd_toko.Text.Trim.Length = 0 Then
            tkd_toko.EditValue = ""
            tnama_toko.EditValue = ""
            talamat_toko.Text = ""
        End If
    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub ffaktur_to2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tnogiro.Focus()
    End Sub

    Private Sub ffaktur_to2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        isiBank()

        If addstat = True Then

            ttgl.EditValue = Date.Now
            ttgl_jt.EditValue = Date.Now

            kosongkan()
        Else

            tnogiro.Enabled = False


            isi()

        End If
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tkd_toko.Text.Trim.Length = 0 Then
            MsgBox("Outlet harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tkd_toko.Focus()
            Return
        End If

        If tkd_sales.Text.Trim.Length = 0 Then
            MsgBox("Outlet harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tkd_toko.Focus()
            Return
        End If

        If tjumlah.EditValue = 0 Then
            MsgBox("Jumlah tidak boleh 0", vbOKOnly + vbInformation, "Informasi")
            tjumlah.Focus()
            Return
        End If

        If MsgBox("Yakin sudah benar.. ???", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.No Then
            Return
        Else
            simpan()
        End If

    End Sub

End Class