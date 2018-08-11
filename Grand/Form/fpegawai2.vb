Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpegawai2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private pathfoto As String

    Private Sub kosongkan()
        ' caktif.Checked = True
        tkode.EditValue = ""
        tnama.EditValue = ""
        talamat.EditValue = ""
        ttelp.EditValue = ""
        ttelp2.EditValue = ""
        ttelp3.EditValue = ""
        tfoto.Image = Nothing
        pathfoto = ""
        tnote.EditValue = ""
        ttempat_lhr.EditValue = ""

        tins_dus.EditValue = 0
        tins_gln.EditValue = 0

        tins_dus_per.EditValue = 0.0
        tins_gln_per.EditValue = 0.0

    End Sub

    Private Sub isi()

        Dim sql As String = String.Format("select * from ms_pegawai  where kd_karyawan='{0}'", dv(position)("kd_karyawan").ToString)

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand
        Dim dred As OleDbDataReader = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            comd = New OleDbCommand(sql, cn)
            dred = comd.ExecuteReader

            If dred.HasRows Then
                If dred.Read Then

                    If dred("aktif").ToString.Equals("1") Then
                        caktif.Checked = True
                    Else
                        caktif.Checked = False
                    End If

                    tkode.EditValue = dred("kd_karyawan").ToString
                    tnama.EditValue = dred("nama_karyawan").ToString

                    tnama.EditValue = dred("nama_karyawan").ToString
                    ttempat_lhr.EditValue = dred("tempat_lahir").ToString

                    ttgl.Text = convert_date_to_ind(dred("tgl_lahir").ToString)

                    talamat.EditValue = dred("alamat").ToString
                    cbjkel.EditValue = dred("jenis_kelamin").ToString
                    ttelp.EditValue = dred("notelp1").ToString
                    ttelp2.EditValue = dred("notelp2").ToString
                    ttelp3.EditValue = dred("notelp3").ToString

                    cbgol.EditValue = dred("bagian").ToString

                    tins_gln.EditValue = dred("ins_gln").ToString
                    tins_dus.EditValue = dred("ins_dus").ToString

                    tins_gln_per.EditValue = dred("ins_gln_per").ToString
                    tins_dus_per.EditValue = dred("ins_dus_per").ToString

                    tnote.EditValue = dred("note").ToString

                    If dred("path_foto").ToString.Trim.Length > 0 Then
                        loadfoto(dred("path_foto").ToString)
                    End If

lanjut:

                Else
                    kosongkan()
                End If
            Else
                kosongkan()
            End If

        Catch ex As Exception

            If ex.Message.ToString.ToLower.Equals("the path is not of a legal form.") Then

                If MsgBox("Path foto tidak ditemukan, akan dilajutkan ?", vbYesNo + vbInformation, "Informasi") = MsgBoxResult.Yes Then
                    GoTo lanjut
                Else

                    If Not cn Is Nothing Then
                        If cn.State = ConnectionState.Open Then
                            cn.Close()
                        End If
                    End If

                    Me.Close()
                    Exit Sub

                End If

            End If

            MsgBox(ex.Message.ToString, MsgBoxStyle.Information, "Informasi")

        Finally
            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub loadfoto(ByVal path As String)
        pathfoto = path
        tfoto.Image = Image.FromFile(path)
    End Sub

    Private Sub simpandbase()

        Dim aktif As Integer
        If caktif.Checked = True Then
            aktif = 1
        Else
            aktif = 0
        End If

        Dim hit_gln As Double
        Dim hit_dus As Double
        If tins_gln_per.EditValue > 0 And tins_gln.EditValue > 0 Then
            hit_gln = (Double.Parse(tins_gln_per.EditValue) / 100) * Double.Parse(tins_gln.EditValue)
        Else
            hit_gln = 0
        End If

        If tins_dus_per.EditValue > 0 And tins_dus.EditValue > 0 Then
            hit_dus = (Double.Parse(tins_dus_per.EditValue) / 100) * Double.Parse(tins_dus.EditValue)
        Else
            hit_dus = 0
        End If


            Dim sqlsearch As String = String.Format("select kd_karyawan from ms_pegawai where kd_karyawan='{0}'", tkode.Text.Trim)
        Dim sqlinsert As String = String.Format("insert into ms_pegawai (aktif,kd_karyawan,nama_karyawan,alamat,notelp1,notelp2,notelp3," _
                    + "tempat_lahir,tgl_lahir,note," _
                    + "path_foto,bagian,jenis_kelamin,ins_gln,ins_dus,ins_gln_per,ins_dus_per,hit_gln,hit_dus) values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',{13},{14},{15},{16},{17},{18})", aktif,
                    tkode.Text.Trim, tnama.Text.Trim, talamat.Text.Trim, ttelp.Text.Trim, ttelp2.Text.Trim, ttelp3.Text.Trim, ttempat_lhr.Text.Trim, _
                    convert_date_to_eng(ttgl.Text), tnote.Text.Trim, pathfoto, cbgol.EditValue, cbjkel.EditValue, Replace(tins_gln.EditValue, ",", "."), Replace(tins_dus.EditValue, ",", "."), Replace(tins_gln_per.EditValue, ",", "."), Replace(tins_dus_per.EditValue, ",", "."), Replace(hit_gln, ",", "."), Replace(hit_dus, ",", "."))
        Dim sqlupdate As String = String.Format("update ms_pegawai set aktif={0},nama_karyawan='{1}',alamat='{2}',notelp1='{3}'," _
                                                + "notelp2='{4}',notelp3='{5}',tgl_lahir='{6}',note='{7}'," _
                                                + "path_foto='{8}',bagian='{9}',jenis_kelamin='{10}',ins_gln={11},ins_dus={12},ins_gln_per={13},ins_dus_per={14},hit_gln={15},hit_dus={16} where kd_karyawan='{17}'", aktif, tnama.Text.Trim, _
                                                talamat.Text.Trim, ttelp.Text.Trim, ttelp2.Text.Trim, ttelp3.Text.Trim, _
                   convert_date_to_eng(ttgl.Text), tnote.Text.Trim, _
                    pathfoto, cbgol.EditValue, cbjkel.EditValue, Replace(tins_gln.EditValue, ",", "."), Replace(tins_dus.EditValue, ",", "."), Replace(tins_gln_per.EditValue, ",", "."), Replace(tins_dus_per.EditValue, ",", "."), Replace(hit_gln, ",", "."), Replace(hit_dus, ",", "."), tkode.Text.Trim)


            Dim cn As OleDbConnection = Nothing
            Dim dread As OleDbDataReader
            Dim comds As OleDbCommand
            Dim comd As OleDbCommand
        Dim sqltrans As OleDbTransaction = Nothing

            Try

                open_wait()

                cn = New OleDbConnection
                cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

                If addstat = True Then

                    comds = New OleDbCommand(sqlsearch, cn, sqltrans)
                    dread = comds.ExecuteReader

                    If dread.HasRows Then

                    If dread.Read Then

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox("NIP sudah ada...", vbOKOnly + vbInformation, "Informasi")
                        close_wait()
                        Exit Sub
                    Else
                        comd = New OleDbCommand(sqlinsert, cn, sqltrans)
                        comd.ExecuteNonQuery()

                        Clsmy.InsertToLog(cn, "btkary", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                        insertview()

                    End If

                    Else
                        comd = New OleDbCommand(sqlinsert, cn, sqltrans)
                        comd.ExecuteNonQuery()

                        Clsmy.InsertToLog(cn, "btkary", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                        insertview()

                    End If

                Else
                    comd = New OleDbCommand(sqlupdate, cn, sqltrans)
                    comd.ExecuteNonQuery()

                    Clsmy.InsertToLog(cn, "btkary", 0, 1, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                    updateview()

                End If

                sqltrans.Commit()

                close_wait()

                MsgBox("Data disimpan...", vbOKOnly + vbInformation, "Informasi")

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
        'dv(position)("kd_karyawan") = tkode.Text.Trim
        dv(position)("nama_karyawan") = tnama.Text.Trim
        dv(position)("tgl_lahir") = ttgl.Text
        dv(position)("tempat_lahir") = ttempat_lhr.Text.Trim
        dv(position)("alamat") = talamat.Text.Trim
        dv(position)("notelp1") = ttelp.Text.Trim
        dv(position)("notelp2") = ttelp2.Text.Trim
        dv(position)("notelp3") = ttelp3.Text.Trim
        dv(position)("bagian") = cbgol.Text.Trim
        dv(position)("note") = tnote.Text.Trim
        dv(position)("aktif") = IIf(caktif.Checked = True, 1, 0)
        dv(position)("path_foto") = pathfoto
        dv(position)("jenis_kelamin") = cbjkel.Text.Trim
    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("kd_karyawan") = tkode.Text.Trim
        orow("nama_karyawan") = tnama.Text.Trim
        orow("tgl_lahir") = ttgl.Text
        orow("tempat_lahir") = ttempat_lhr.Text.Trim
        orow("alamat") = talamat.Text.Trim
        orow("notelp1") = ttelp.Text.Trim
        orow("notelp2") = ttelp2.Text.Trim
        orow("notelp3") = ttelp3.Text.Trim
        orow("bagian") = cbgol.Text.Trim
        orow("note") = tnote.Text.Trim
        orow("aktif") = IIf(caktif.Checked = True, 1, 0)
        orow("path_foto") = pathfoto
        orow("jenis_kelamin") = cbjkel.Text.Trim

        dv.EndInit()

    End Sub

    Private Sub btfoto_Click(sender As System.Object, e As System.EventArgs) Handles btfoto.Click

        Dim fd As OpenFileDialog = New OpenFileDialog()
        fd.Title = "Open File Dialog"
        ' fd.InitialDirectory = "C:\"
        fd.Filter = "Image Files (*.bmp;*.jpg;*.gif)|*.bmp;*.jpg;*.gif|" _
            & "All Files(*.*)|"
        ' fd.FilterIndex = 2
        fd.RestoreDirectory = True

        If fd.ShowDialog() = DialogResult.OK Then
            loadfoto(fd.FileName)
        End If

    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tkode.Text.Trim.Length = 0 Then
            MsgBox("Kode tidak boleh kosong....", vbOKOnly + vbInformation, "Informasi")
            tkode.Focus()
            Exit Sub
        End If

        If tnama.Text.Trim.Length = 0 Then
            MsgBox("Nama tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            tnama.Focus()
            Exit Sub
        End If

        If talamat.Text.Trim.Length = 0 Then
            MsgBox("Alamat tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            talamat.Focus()
            Exit Sub
        End If

        If Not IsDate(ttgl.Text.Trim) Then
            MsgBox("Tanggal Salah...", vbOKOnly + vbInformation, "Informasi")
            ttgl.Focus()
            Exit Sub
        End If

        If IsNothing(cbgol.EditValue) Then
            MsgBox("Bagian tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            cbgol.Focus()
            Exit Sub
        End If

        If MsgBox("Yakin semua sudah benar ... ???", vbYesNo + vbQuestion, "Konfirmasi") = vbYes Then
            simpandbase()
        End If

    End Sub

    Private Sub fkaryawan2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If addstat = True Then
            tkode.Focus()
        Else
            tnama.Focus()
        End If
    End Sub

    Private Sub fkaryawan2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ttgl.EditValue = Date.Now

        cbjkel.SelectedIndex = 0

        If addstat = True Then
           
            pathfoto = ""
            tkode.Enabled = True

            kosongkan()

            cbgol.SelectedIndex = 0

        Else

            isi()

          
            tkode.Enabled = False

        End If
    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btclearfot_Click(sender As System.Object, e As System.EventArgs) Handles btclearfot.Click
        tfoto.Image = Nothing
        pathfoto = ""
    End Sub

    Private Sub caktif_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles caktif.CheckedChanged
        If caktif.Checked = True Then
            caktif.Text = "Ya"
        Else
            caktif.Text = "Tidak"
        End If
    End Sub

    Private Sub cbgol_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbgol.SelectedIndexChanged
        If cbgol.Text.Trim = "SALES" Or cbgol.Text.Trim = "SUPIR" Or cbgol.Text.Trim = "KENEK" Then
            tins_dus.Enabled = True
            tins_gln.Enabled = True

            tins_dus_per.Enabled = True
            tins_gln_per.Enabled = True

        Else
            tins_dus.Enabled = False
            tins_gln.Enabled = False

            tins_dus_per.Enabled = False
            tins_gln_per.Enabled = False

            tins_dus.EditValue = 0
            tins_gln.EditValue = 0
            tins_dus_per.EditValue = 0.0
            tins_gln_per.EditValue = 0.0

        End If
    End Sub
End Class