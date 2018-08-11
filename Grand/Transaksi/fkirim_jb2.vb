Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fkirim_jb2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private tgl_old As String
    Private supir_old As String
    Private nopol_old As String

    Private Sub kosongkan()
        tbukti.Text = "<< New >>"

        tkd_supir.Text = ""
        tnama_supir.Text = ""
        tnote.Text = ""

        tno_sj.Text = ""

        opengrid()

    End Sub

    Private Sub opengrid()

        Dim sql As String = String.Format("SELECT     tr_kirimjb2.noid, tr_kirimjb2.kd_gudang, ms_gudang.nama_gudang, tr_kirimjb2.kd_barang, ms_barang.nama_barang, tr_kirimjb2.qty, " & _
            "tr_kirimjb2.satuan, tr_kirimjb2.qtykecil " & _
            "FROM         tr_kirimjb2 INNER JOIN " & _
                      "ms_barang ON tr_kirimjb2.kd_barang = ms_barang.kd_barang INNER JOIN " & _
                      "ms_gudang ON tr_kirimjb2.kd_gudang = ms_gudang.kd_gudang " & _
            "WHERE tr_kirimjb2.nobukti='{0}'", tbukti.Text.Trim)


        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        grid1.DataSource = Nothing

        Try

            open_wait()

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            grid1.DataSource = dv1

            close_wait()


        Catch ex As OleDb.OleDbException
            close_wait()
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

        Dim nobukti As String = dv(position)("nobukti").ToString

        Dim sql As String = String.Format("SELECT     tr_kirimjb.nobukti, tr_kirimjb.tanggal, tr_kirimjb.tgl_keluar, tr_kirimjb.tgl_kirim, tr_kirimjb.nopol, tr_kirimjb.kd_supir, ms_pegawai.nama_karyawan, tr_kirimjb.note,tr_kirimjb.no_sj " & _
        "FROM         tr_kirimjb INNER JOIN " & _
                      "ms_pegawai ON tr_kirimjb.kd_supir = ms_pegawai.kd_karyawan WHERE tr_kirimjb.nobukti='{0}'", nobukti)

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim dread As OleDbDataReader = comd.ExecuteReader
            Dim hasil As Boolean

            If dread.HasRows Then
                If dread.Read Then

                    If Not dread("nobukti").ToString.Equals("") Then

                        hasil = True

                        tbukti.EditValue = dread("nobukti").ToString
                        ttgl.EditValue = DateValue(dread("tanggal").ToString)
                        ttgl_klr.EditValue = DateValue(dread("tgl_keluar").ToString)
                        ttgl_krm.EditValue = DateValue(dread("tgl_kirim").ToString)

                        tgl_old = ttgl_klr.EditValue

                        tnopol.EditValue = dread("nopol").ToString

                        nopol_old = dread("nopol").ToString

                        tkd_supir.EditValue = dread("kd_supir").ToString

                        supir_old = dread("kd_supir").ToString

                        tno_sj.EditValue = dread("no_sj").ToString

                        tnama_supir.EditValue = dread("nama_karyawan").ToString
                        tnote.EditValue = dread("note").ToString

                        opengrid()

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

    Private Function cekbukti(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String


        Dim tahun As String = Year(ttgl.EditValue)
        tahun = Microsoft.VisualBasic.Right(tahun, 2)
        Dim bulan As String = Month(ttgl.EditValue)

        If bulan.Length = 1 Then
            bulan = "0" & bulan
        End If

        Dim tahunbulan As String = String.Format("{0}{1}", tahun, bulan)

        Dim sql As String = String.Format("select max(nobukti) from tr_kirimjb where nobukti like '%PBK.{0}%'", tahunbulan)

        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim nilai As Integer = 0

        If drd.HasRows Then
            If drd.Read Then

                If Not drd(0).ToString.Equals("") Then
                    nilai = Microsoft.VisualBasic.Right(drd(0).ToString, 4)
                End If

            End If
        End If

        nilai = nilai + 1
        Dim kbukti As String = nilai

        Select Case kbukti.Length
            Case 1
                kbukti = "000" & nilai
            Case 2
                kbukti = "00" & nilai
            Case 3
                kbukti = "0" & nilai
            Case Else
                kbukti = nilai
        End Select

        Return String.Format("PBK.{0}{1}{2}", tahun, bulan, kbukti)

    End Function

    Private Function cek_nosj(ByVal cn As OleDbConnection) As Boolean

        Dim hasil As Boolean = False

        Dim sql As String = String.Format("select nobukti from tr_kirimjb where no_sj='{0}' and sbatal=0", tno_sj.Text.Trim)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        If drd.Read Then
            If Not drd(0).ToString.Equals("") Then
                hasil = True
            End If
        End If
        drd.Close()

        Return hasil
    End Function

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try
            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            If addstat Then
                If cek_nosj(cn) = True Then
                    MsgBox("No surat jalan sudah diinput..", vbOKOnly + vbInformation, "Informasi")
                    Return
                End If
            End If

            sqltrans = cn.BeginTransaction

            Dim cmd As OleDbCommand
            ' Dim cmdtoko As OleDbCommand

            If addstat Then
                Dim bukti As String = cekbukti(cn, sqltrans)
                tbukti.EditValue = bukti

                '1 . update faktur
                Dim sqlin_faktur As String = String.Format("insert into tr_kirimjb (nobukti,tanggal,tgl_keluar,tgl_kirim,nopol,kd_supir,note,no_sj) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", tbukti.Text.Trim, _
                                                            convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_klr.EditValue), convert_date_to_eng(ttgl_krm.EditValue), tnopol.EditValue, tkd_supir.EditValue, tnote.Text.Trim, tno_sj.Text.Trim)

                cmd = New OleDbCommand(sqlin_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()

                Dim sqltr As String = String.Format("update tradm_gud set sambil=1 where jenistrans='TR KIRIM JABUNG' and nobukti_gd='{0}'", tno_sj.Text.Trim)
                Using cmdtr As OleDbCommand = New OleDbCommand(sqltr, cn, sqltrans)
                    cmdtr.ExecuteNonQuery()
                End Using

                Clsmy.InsertToLog(cn, "btkirim_jb", 1, 0, 0, 0, tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), sqltrans)

            Else

                '1. update faktur
                Dim sqlup_faktur As String = String.Format("update tr_kirimjb set tanggal='{0}',tgl_keluar='{1}',tgl_kirim='{2}',nopol='{3}',kd_supir='{4}',note='{5}',no_sj='{6}' where nobukti='{7}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_klr.EditValue), convert_date_to_eng(ttgl_krm.EditValue), tnopol.EditValue, tkd_supir.EditValue, tnote.Text.Trim, tno_sj.Text.Trim, tbukti.Text.Trim)

                cmd = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()

                Dim sqltr As String = String.Format("update tradm_gud set sambil=1 where jenistrans='TR KIRIM JABUNG' and nobukti_gd='{0}'", tno_sj.Text.Trim)
                Using cmdtr As OleDbCommand = New OleDbCommand(sqltr, cn, sqltrans)
                    cmdtr.ExecuteNonQuery()
                End Using

                Clsmy.InsertToLog(cn, "btkirim_jb", 0, 1, 0, 0, tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), sqltrans)

            End If

            If simpan2(cn, sqltrans) = "ok" Then

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
                    ttgl.Focus()
                Else
                    close_wait()
                    Me.Close()
                End If

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

    Private Function simpan2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim cmd As OleDbCommand
        Dim hasil As String = ""

        For i As Integer = 0 To dv1.Count - 1

            Dim kdgud As String = dv1(i)("kd_gudang").ToString
            Dim kdbar As String = dv1(i)("kd_barang").ToString
            Dim qty As String = dv1(i)("qty").ToString
            Dim satuan As String = dv1(i)("satuan").ToString
            '   Dim harga As String = 0 'dv1(i)("harga").ToString
            '   Dim disc_per As String = 0
            '   Dim disc_rp As String = 0
            '   Dim jumlah As String = 0 ' dv1(i)("jumlah").ToString
            Dim qtykecil As String = dv1(i)("qtykecil").ToString
            '   Dim hargakecil As String = 0 ' dv1(i)("hargakecil").ToString
            '    Dim jumlah0 As String = 0
            Dim noid As String = dv1(i)("noid").ToString

            If addstat = True Then

                '1. insert faktur_to
                Dim sqlins As String = String.Format("insert into tr_kirimjb2 (nobukti,kd_gudang,kd_barang,satuan,qty,qtykecil) values('{0}','{1}','{2}','{3}',{4},{5})", tbukti.EditValue, _
                                                         kdgud, kdbar, satuan, Replace(qty, ",", "."), Replace(qtykecil, ",", "."))


                cmd = New OleDbCommand(sqlins, cn, sqltrans)
                cmd.ExecuteNonQuery()

                '2. update barang
                Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, False, False, False)
                If Not hasilplusmin.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                    hasil = "error"
                    Exit For
                End If

                '3. insert to hist stok
                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, kdgud, kdbar, 0, qtykecil, "Pengiriman Barang Kosong (Pabrik)", tkd_supir.Text.Trim, tnopol.EditValue)


            Else

                Dim sqlc As String = String.Format("select qtykecil from tr_kirimjb2 where noid={0}", noid)
                Dim cmds As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
                Dim drds As OleDbDataReader = cmds.ExecuteReader

                Dim shasil As Boolean = False

                If drds.HasRows Then
                    If drds.Read Then

                        If IsNumeric(drds(0).ToString) Then

                            shasil = True

                            '1. update faktur to
                            Dim sqlup As String = String.Format("update tr_kirimjb2 set kd_gudang='{0}',kd_barang='{1}',satuan='{2}',qty={3},qtykecil={4} where nobukti='{5}' and noid={6}", _
                                                               kdgud, kdbar, satuan, Replace(qty, ",", "."), Replace(qtykecil, ",", "."), tbukti.Text.Trim, noid)

                            cmd = New OleDbCommand(sqlup, cn, sqltrans)
                            cmd.ExecuteNonQuery()

                            '2. update barang
                            Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, drds(0).ToString, kdbar, kdgud, True, False, False)
                            If Not hasilplusmin.Equals("ok") Then
                                close_wait()

                                If Not IsNothing(sqltrans) Then
                                    sqltrans.Rollback()
                                End If

                                MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                                hasil = "error"
                                Exit For
                            Else

                                Dim hasilplusmin2 As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, False, False, False)

                                If Not hasilplusmin2.Equals("ok") Then

                                    If Not IsNothing(sqltrans) Then
                                        sqltrans.Rollback()
                                    End If

                                    MsgBox(hasilplusmin2, vbOKOnly + vbExclamation, "Informasi")
                                    hasil = "error"
                                    Exit For
                                End If

                            End If

                            '3. insert to hist stok

                            If DateValue(tgl_old) <> DateValue(ttgl.EditValue) Or nopol_old <> tnopol.EditValue Or supir_old <> tkd_supir.EditValue Then
                                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, kdgud, kdbar, drds(0).ToString, 0, "Pengiriman Barang Kosong (Pabrik)", supir_old, nopol_old)
                            Else
                                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, kdgud, kdbar, drds(0).ToString, 0, "Pengiriman Barang Kosong (Pabrik)", tkd_supir.Text.Trim, tnopol.EditValue)
                            End If

                            Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, kdgud, kdbar, 0, qtykecil, "Pengiriman Barang Kosong (Pabrik)", tkd_supir.Text.Trim, tnopol.EditValue)

                        End If

                    End If
                End If

                If shasil = False Then

                    '1. insert faktur_to
                    Dim sqlins As String = String.Format("insert into tr_kirimjb2 (nobukti,kd_gudang,kd_barang,satuan,qty,qtykecil) values('{0}','{1}','{2}','{3}',{4},{5})", tbukti.EditValue, _
                                                             kdgud, kdbar, satuan, Replace(qty, ",", "."), Replace(qtykecil, ",", "."))

                    cmd = New OleDbCommand(sqlins, cn, sqltrans)
                    cmd.ExecuteNonQuery()

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, False, False, False)
                    If Not hasilplusmin.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        Exit For
                    End If

                    '3. insert to hist stok
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, kdgud, kdbar, 0, qtykecil, "Pengiriman Barang Kosong (Pabrik)", tkd_supir.Text.Trim, tnopol.EditValue)

                End If


            End If

        Next

        If Not hasil.Equals("error") Then
            hasil = "ok"
        End If

        Return hasil

    End Function

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("nobukti") = tbukti.Text.Trim
        orow("tanggal") = ttgl.Text.Trim
        orow("tgl_keluar") = ttgl_klr.Text.Trim
        orow("tgl_kirim") = ttgl_krm.Text.Trim
        orow("nopol") = tnopol.EditValue
        orow("nama_karyawan") = tnama_supir.Text.Trim
        orow("note") = tnote.Text.Trim
        orow("sbatal") = 0
        orow("kd_supir") = tkd_supir.Text.Trim
        orow("no_sj") = tno_sj.Text.Trim

        dv.EndInit()

    End Sub

    Private Sub updateview()

        dv(position)("nobukti") = tbukti.Text.Trim
        dv(position)("tanggal") = ttgl.Text.Trim
        dv(position)("tgl_keluar") = ttgl_klr.Text.Trim
        dv(position)("tgl_kirim") = ttgl_krm.Text.Trim
        dv(position)("nopol") = tnopol.EditValue
        dv(position)("nama_karyawan") = tnama_supir.Text.Trim
        dv(position)("kd_supir") = tkd_supir.Text.Trim
        dv(position)("note") = tnote.Text.Trim
        dv(position)("no_sj") = tno_sj.Text.Trim
        ' orow("sbatal") = 0

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

    Private Sub btadd_Click(sender As System.Object, e As System.EventArgs) Handles btadd.Click
        Using fkar2 As New fkirim_jb3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0}
            fkar2.ShowDialog(Me)

            '  tbrutto.EditValue = GridView1.Columns("jumlah").SummaryItem.SummaryValue

        End Using
    End Sub

    Private Sub btedit_Click(sender As System.Object, e As System.EventArgs) Handles btedit.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Using fkar2 As New fkirim_jb3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = Me.BindingContext(dv1).Position}
            fkar2.ShowDialog(Me)

            ' tbrutto.EditValue = GridView1.Columns("jumlah").SummaryItem.SummaryValue

        End Using

    End Sub

    Private Sub btdel_Click(sender As System.Object, e As System.EventArgs) Handles btdel.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        If addstat = True Then
            dv1.Delete(Me.BindingContext(dv1).Position)
            'tbrutto.EditValue = GridView1.Columns("jumlah").SummaryItem.SummaryValue
        Else

            If Integer.Parse(dv1(Me.BindingContext(dv1).Position)("noid").ToString) = 0 Then
                Return
            End If

            Dim cn As OleDbConnection = Nothing
            Dim sqltrans As OleDbTransaction = Nothing

            Try

                Dim qtykecil As Integer = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtykecil").ToString)
                Dim kdbar As String = dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString
                Dim kdgud As String = dv1(Me.BindingContext(dv1).Position)("kd_gudang").ToString

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                sqltrans = cn.BeginTransaction

                Dim sql As String = String.Format("delete from tr_kirimjb2 where noid={0}", dv1(Me.BindingContext(dv1).Position)("noid").ToString)

                Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()

                ' Clsmy.InsertToLog(cn, "btfaktur_to", 0, 0, 1, 0, tbukti.Text.Trim, tnama_toko.Text.Trim, sqltrans)

                '2. update barang
                Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, True, False, False)
                If Not hasilplusmin.Equals("ok") Then

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                    GoTo langsung
                End If

                '3. insert to hist stok
                If DateValue(tgl_old) <> DateValue(ttgl.EditValue) Or supir_old <> tkd_supir.EditValue Or nopol_old <> tnopol.EditValue Then
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, kdgud, kdbar, qtykecil, 0, "Pengiriman Barang Kosong (Pabrik)", supir_old, nopol_old)
                Else
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, kdgud, kdbar, qtykecil, 0, "Pengiriman Barang Kosong (Pabrik)", tkd_supir.Text.Trim, tnote.EditValue)
                End If


                dv1.Delete(Me.BindingContext(dv1).Position)
                ' tbrutto.EditValue = GridView1.Columns("jumlah").SummaryItem.SummaryValue

                sqltrans.Commit()

                MsgBox("Data dihapus...", vbOKOnly + vbInformation, "Informasi")

langsung:


            Catch ex As Exception

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


        End If

    End Sub

    Private Sub bts_supir_Click(sender As System.Object, e As System.EventArgs) Handles bts_supir.Click

        Dim fs As New fssupir With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_supir.EditValue = fs.get_KODE
        tnama_supir.EditValue = fs.get_NAMA

    End Sub

    Private Sub tkd_supir_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_supir.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_supir_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_supir_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_supir.LostFocus
        If tkd_supir.Text.Trim.Length = 0 Then
            tkd_supir.Text = ""
            tnama_supir.Text = ""
        End If
    End Sub

    Private Sub tkd_supir_Validated(sender As Object, e As System.EventArgs) Handles tkd_supir.Validated
        If tkd_supir.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and bagian like 'SUPIR%' and kd_karyawan='{0}'", tkd_supir.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_supir.EditValue = dread("kd_karyawan").ToString
                        tnama_supir.EditValue = dread("nama_karyawan").ToString

                    Else
                        tkd_supir.EditValue = ""
                        tnama_supir.EditValue = ""

                    End If
                Else
                    tkd_supir.EditValue = ""
                    tnama_supir.EditValue = ""

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

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tnopol.EditValue = "" Then
            MsgBox("No Polisi tidak boleh kosong..", vbOKOnly + vbInformation, "Informasi")
            tnopol.Focus()
            Return
        End If

        If tkd_supir.EditValue = "" Then
            MsgBox("Supir tidak boleh kosong..", vbOKOnly + vbInformation, "Informasi")
            tkd_supir.Focus()
            Return
        End If

        If IsNothing(dv1) Then
            MsgBox("Tidak ada barang yang diproses..", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If dv1.Count <= 0 Then
            MsgBox("Tidak ada barang yang diproses..", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If MsgBox("Yakin sudah benar ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
            simpan()
        End If

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fbeli2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub fbeli2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        isi_nopol()

        ttgl.EditValue = Date.Now
        ttgl_klr.EditValue = Date.Now
        ttgl_krm.EditValue = Date.Now

        If addstat Then
            kosongkan()
        Else
            isi()

            tno_sj.Enabled = False
            bts_sj.Enabled = False

        End If

    End Sub

    Private Sub tnopol_Validated(sender As System.Object, e As System.EventArgs) Handles tnopol.Validated

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select kd_supir from ms_supirkenek where nopol='{0}'", tnopol.EditValue)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Not drd(0).ToString.Equals("") Then
                    tkd_supir.EditValue = drd(0).ToString
                    tkd_supir_Validated(sender, Nothing)
                End If
            End If
            drd.Close()

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

    Private Sub tno_sj_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tno_sj.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_sj_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tno_sj_Validated(sender As System.Object, e As System.EventArgs) Handles tno_sj.Validated

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select 0 as noid,tradm_gud.kd_gudang,tradm_gud.kd_gudang,ms_barang.kd_barang,ms_barang.nama_barang, " & _
            "tradm_gud2.qtyout as qty,tradm_gud2.satuan,tradm_gud2.qtyoutkecil as qtykecil " & _
            "from tradm_gud inner join tradm_gud2 on tradm_gud.nobukti=tradm_gud2.nobukti " & _
            "inner join ms_barang on tradm_gud2.kd_barang=ms_barang.kd_barang " & _
            "where tradm_gud.jenistrans='TR KIRIM JABUNG' and tradm_gud.sbatal=0 and tradm_gud.sambil=0 and tradm_gud.nobukti_gd='{0}'", tno_sj.Text.Trim)

            Dim sqlatas As String = String.Format("select tradm_gud.nobukti,tradm_gud.nopol,tradm_gud.kd_supir,tradm_gud.tglmuat,tradm_gud.tglberangkat,ms_pegawai.nama_karyawan " & _
            "from tradm_gud inner join tradm_gud2 on tradm_gud.nobukti=tradm_gud2.nobukti " & _
            "inner join ms_pegawai on tradm_gud.kd_supir=ms_pegawai.kd_karyawan " & _
            "where tradm_gud.jenistrans='TR KIRIM JABUNG' and tradm_gud.sbatal=0 and tradm_gud.sambil=0 and tradm_gud.nobukti_gd='{0}'", tno_sj.Text.Trim)

            Dim cmdatas As OleDbCommand = New OleDbCommand(sqlatas, cn)
            Dim drdatas As OleDbDataReader = cmdatas.ExecuteReader

            Dim ada As Boolean = False

            grid1.DataSource = Nothing
            dv1 = Nothing

            If drdatas.Read Then
                If Not drdatas(0).ToString.Equals("") Then

                    ada = True

                    tnopol.EditValue = drdatas("nopol").ToString
                    tkd_supir.Text = drdatas("kd_supir").ToString
                    tnama_supir.Text = drdatas("nama_karyawan").ToString
                    ttgl_klr.EditValue = DateValue(drdatas("tglmuat").ToString)
                    ttgl_krm.EditValue = DateValue(drdatas("tglberangkat").ToString)


                    Dim ds As DataSet
                    ds = New DataSet()
                    ds = Clsmy.GetDataSet(sql, cn)

                    dvmanager = New DataViewManager(ds)
                    dv1 = dvmanager.CreateDataView(ds.Tables(0))

                    grid1.DataSource = dv1

                End If
            End If
            drdatas.Close()

            If ada = False Then
                ' tnopol.Text = ""
                ' tkd_supir.Text = ""
                ' tnama_supir.Text = ""
            End If

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

    Private Sub bts_sj_Click(sender As System.Object, e As System.EventArgs) Handles bts_sj.Click

        Dim fs As New fs_jb With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tno_sj.EditValue = fs.get_NoBukti

        tno_sj_Validated(sender, Nothing)

    End Sub

End Class