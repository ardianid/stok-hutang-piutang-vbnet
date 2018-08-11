Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fsewa2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Dim tgl_old As String


    Private Sub kosongkan()
        tbukti.Text = "<< New >>"

        tkd_toko.Text = ""
        tnama_toko.Text = ""
        talamat_toko.Text = ""

        tnote.Text = ""

        tdisc_per.EditValue = 0.0
        tdisc_rp.EditValue = 0
        tnetto.EditValue = 0

        opengrid()

        tbrutto.EditValue = 0

    End Sub

    Private Sub opengrid()

        Dim sql As String = String.Format("SELECT trsewa2.noid, trsewa2.nobukti, trsewa2.kd_gudang, trsewa2.kd_barang, ms_barang.nama_barang, trsewa2.qty, trsewa2.satuan, trsewa2.harga, trsewa2.jumlah, trsewa2.qtykecil " & _
            "FROM trsewa2 INNER JOIN ms_barang ON trsewa2.kd_barang = ms_barang.kd_barang where trsewa2.nobukti='{0}'", tbukti.Text.Trim)


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

            totalnetto()

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
        Dim sql As String = String.Format("SELECT trsewa.nobukti, trsewa.tanggal, trsewa.tgl_keluar, trsewa.tanggal1, trsewa.tanggal2, trsewa.kd_toko, " & _
            "ms_toko.nama_toko, ms_toko.alamat_toko, trsewa.disc_per, trsewa.disc_rp, trsewa.brutto, trsewa.netto, trsewa.note " & _
            "FROM trsewa INNER JOIN ms_toko ON trsewa.kd_toko = ms_toko.kd_toko where trsewa.nobukti='{0}'", nobukti)

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
                        ttgl_mt.EditValue = DateValue(dread("tgl_keluar").ToString)

                        tgl_old = ttgl_mt.EditValue()

                        ttgl_sewa1.EditValue = DateValue(dread("tanggal1").ToString)
                        ttgl_sewa2.EditValue = DateValue(dread("tanggal2").ToString)

                        tkd_toko.EditValue = dread("kd_toko").ToString
                        tnama_toko.EditValue = dread("nama_toko").ToString
                        talamat_toko.EditValue = dread("alamat_toko").ToString

                        tbrutto.EditValue = dread("brutto").ToString
                        tdisc_per.EditValue = dread("disc_per").ToString
                        tdisc_rp.EditValue = dread("disc_rp").ToString
                        tnetto.EditValue = dread("netto").ToString

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

                tbukti.EditValue = ""

                tkd_toko.EditValue = ""
                tnama_toko.EditValue = ""
                talamat_toko.EditValue = ""

                tbrutto.EditValue = 0
                tdisc_per.EditValue = 0
                tdisc_rp.EditValue = 0
                tnetto.EditValue = 0

                tnote.EditValue = ""

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

    Private Sub totalnetto()

        If IsNothing(dv1) Then

            tbrutto.EditValue = 0
            tdisc_per.EditValue = 0.0
            tdisc_rp.EditValue = 0
            tnetto.EditValue = 0

            Return
        End If

        If dv1.Count <= 0 Then

            tbrutto.EditValue = 0
            tdisc_per.EditValue = 0.0
            tdisc_rp.EditValue = 0
            tnetto.EditValue = 0

            Return
        End If

        Dim jumlah As Double = 0

        For i As Integer = 0 To dv1.Count - 1

            jumlah = jumlah + Double.Parse(dv1(i)("jumlah").ToString)

        Next

        tbrutto.EditValue = jumlah

        Dim diskon As Double = tdisc_rp.EditValue

        If diskon > 0 Then
            jumlah = jumlah - diskon
        End If

        tnetto.EditValue = jumlah

    End Sub

    Private Function cekbukti(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim sql As String = ""

        Dim tahun As String = Year(ttgl.EditValue)
        tahun = Microsoft.VisualBasic.Right(tahun, 2)
        Dim bulan As String = Month(ttgl.EditValue)

        If bulan.Length = 1 Then
            bulan = "0" & bulan
        End If

        Dim tahunbulan As String = String.Format("{0}{1}", tahun, bulan)

        sql = String.Format("select max(nobukti) from trsewa where nobukti like '%FSW.{0}%'", tahunbulan)


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

        Dim jenisnota As String
        jenisnota = "FSW."


        Return String.Format("{0}{1}{2}{3}", jenisnota, tahun, bulan, kbukti)

    End Function

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try
            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            Dim cmd As OleDbCommand
            Dim cmdtoko As OleDbCommand

            If addstat Then
                Dim bukti As String = cekbukti(cn, sqltrans)
                tbukti.EditValue = bukti

                '1 . update faktur
                Dim sqlin_faktur As String = String.Format("insert into trsewa (nobukti,tanggal,tgl_keluar,tanggal1,tanggal2,kd_toko,disc_per,disc_rp,brutto,netto,note) values('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7},{8},{9},'{10}')", _
                                    tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_mt.EditValue), convert_date_to_eng(ttgl_sewa1.EditValue), convert_date_to_eng(ttgl_sewa2.EditValue), tkd_toko.EditValue, Replace(tdisc_per.EditValue, ",", "."), Replace(tdisc_rp.EditValue, ",", "."), Replace(tbrutto.EditValue, ",", "."), Replace(tnetto.EditValue, ",", "."), tnote.Text.Trim)


                cmd = New OleDbCommand(sqlin_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()

                '2. update piutangtoko
                Dim sqltoko As String = String.Format("update ms_toko set piutangsewa=piutangsewa + {0} where kd_toko='{1}'", Replace(tnetto.EditValue, ",", "."), tkd_toko.Text.Trim)

                cmdtoko = New OleDbCommand(sqltoko, cn, sqltrans)
                cmdtoko.ExecuteNonQuery()

                ' hist sewa
                Dim sqlhist As String = String.Format("insert into hsewa (nobukti,nobukti2,tanggal1,tanggal2) values('{0}','{1}','{2}','{3}')", tbukti.Text.Trim, tbukti.Text.Trim, convert_date_to_eng(ttgl_sewa1.EditValue), convert_date_to_eng(ttgl_sewa2.EditValue))
                Using cmdhist As OleDbCommand = New OleDbCommand(sqlhist, cn, sqltrans)
                    cmdhist.ExecuteNonQuery()
                End Using

                Clsmy.InsertToLog(cn, "btsewa", 1, 0, 0, 0, tbukti.Text.Trim, ttgl.EditValue, sqltrans)


            Else

                '2. update piutang toko
                Dim sqlct As String = String.Format("select netto,kd_toko from trsewa where nobukti='{0}'", tbukti.Text.Trim)

                Dim cmdt As OleDbCommand = New OleDbCommand(sqlct, cn, sqltrans)
                Dim drdt As OleDbDataReader = cmdt.ExecuteReader

                If drdt.Read Then
                    If IsNumeric(drdt("netto").ToString) Then

                        Dim nett_sebelum As Double = drdt("netto").ToString

                        Dim sqluptoko As String = String.Format("update ms_toko set piutangsewa=piutangsewa - {0} where kd_toko='{1}'", Replace(nett_sebelum, ",", "."), tkd_toko.Text.Trim)
                        Dim sqluptoko2 As String = String.Format("update ms_toko set piutangsewa=piutangsewa + {0} where kd_toko='{1}'", Replace(tnetto.EditValue, ",", "."), tkd_toko.Text.Trim)

                        Dim cmdtk As OleDbCommand = New OleDbCommand(sqluptoko, cn, sqltrans)
                        cmdtk.ExecuteNonQuery()

                        Dim cmdtk2 As OleDbCommand = New OleDbCommand(sqluptoko2, cn, sqltrans)
                        cmdtk2.ExecuteNonQuery()

                    End If
                End If
                drdt.Close()

                '1. update faktur
                Dim sqlup_faktur As String = String.Format("update trsewa set tanggal='{0}',tgl_keluar='{1}',tanggal1='{2}',tanggal2='{3}',kd_toko='{4}',disc_per={5},disc_rp={6},brutto={7},netto={8},note='{9}' where nobukti='{10}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_mt.EditValue), convert_date_to_eng(ttgl_sewa1.EditValue), convert_date_to_eng(ttgl_sewa2.EditValue), tkd_toko.EditValue, Replace(tdisc_per.EditValue, ",", "."), Replace(tdisc_rp.EditValue, ",", "."), Replace(tbrutto.EditValue, ",", "."), Replace(tnetto.EditValue, ",", "."), tnote.Text.Trim, tbukti.Text.Trim)

                cmd = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()


                Dim sqlhist As String = String.Format("update hsewa set tanggal1='{0}',tanggal2='{1}' where nobukti='{2}' and nobukti2='{2}'", convert_date_to_eng(ttgl_sewa1.EditValue), convert_date_to_eng(ttgl_sewa2.EditValue), tbukti.Text.Trim)
                Using cmdhist As OleDbCommand = New OleDbCommand(sqlhist, cn, sqltrans)
                    cmdhist.ExecuteNonQuery()
                End Using

                Clsmy.InsertToLog(cn, "btsewa", 0, 1, 0, 0, tbukti.Text.Trim, ttgl.EditValue, sqltrans)

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
                    tkd_toko.Focus()
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
            Dim harga As String = dv1(i)("harga").ToString
            ' Dim disc_per As String = dv1(i)("disc_per").ToString
            ' Dim disc_rp As String = dv1(i)("disc_rp").ToString
            Dim jumlah As String = dv1(i)("jumlah").ToString
            Dim qtykecil As String = dv1(i)("qtykecil").ToString
            'Dim hargakecil As String = dv1(i)("hargakecil").ToString
            Dim noid As String = dv1(i)("noid").ToString


            If addstat = True Then

                '1. insert faktur_to
                Dim sqlins As String = String.Format("insert into trsewa2 (nobukti,kd_gudang,kd_barang,qty,satuan,harga,jumlah,qtykecil) values ('{0}','{1}','{2}',{3},'{4}',{5},{6},{7})", tbukti.EditValue, _
                                                     kdgud, kdbar, Replace(qty, ",", "."), satuan, Replace(harga, ",", "."), Replace(jumlah, ",", "."), Replace(qtykecil, ",", "."))

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

                ' historical toko
                Dim hasiltok As String = Hist_PinjamSewa_Toko(tkd_toko.Text.Trim, kdbar, qtykecil, cn, sqltrans, True, False)
                If Not hasiltok.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasiltok, vbOKOnly + vbExclamation, "Informasi")
                    hasil = "error"
                    Exit For
                End If


                '3. insert to hist stok
                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, kdgud, kdbar, 0, qtykecil, "Sewa Barang", "-", "BE XXXX XX")

            Else

                Dim sqlc As String = String.Format("select qtykecil from trsewa2 where noid={0}", noid)
                Dim cmds As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
                Dim drds As OleDbDataReader = cmds.ExecuteReader

                Dim shasil As Boolean
                shasil = False

                If drds.HasRows Then
                    If drds.Read Then

                        If IsNumeric(drds(0).ToString) Then

                            shasil = True

                            '1. update faktur to
                            Dim sqlup As String = String.Format("update trsewa2 set kd_gudang='{0}',kd_barang='{1}',qty={2},satuan='{3}',harga={4},jumlah={5},qtykecil={6} where nobukti='{7}' and noid={8}", _
                                                                kdgud, kdbar, Replace(qty, ",", "."), satuan, Replace(harga, ",", "."), Replace(jumlah, ",", "."), Replace(qtykecil, ",", "."), tbukti.Text.Trim, noid)

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
                                    close_wait()

                                    If Not IsNothing(sqltrans) Then
                                        sqltrans.Rollback()
                                    End If

                                    MsgBox(hasilplusmin2, vbOKOnly + vbExclamation, "Informasi")
                                    hasil = "error"
                                    Exit For
                                End If

                            End If


                            ' historical toko
                            Dim hasiltok As String = Hist_PinjamSewa_Toko(tkd_toko.Text.Trim, kdbar, drds(0).ToString, cn, sqltrans, False, False)
                            If Not hasiltok.Equals("ok") Then
                                close_wait()

                                If Not IsNothing(sqltrans) Then
                                    sqltrans.Rollback()
                                End If

                                MsgBox(hasiltok, vbOKOnly + vbExclamation, "Informasi")
                                hasil = "error"
                                Exit For
                            Else

                                Dim hasiltok2 As String = Hist_PinjamSewa_Toko(tkd_toko.Text.Trim, kdbar, qtykecil, cn, sqltrans, True, False)
                                If Not hasiltok2.Equals("ok") Then
                                    close_wait()

                                    If Not IsNothing(sqltrans) Then
                                        sqltrans.Rollback()
                                    End If

                                    MsgBox(hasiltok2, vbOKOnly + vbExclamation, "Informasi")
                                    hasil = "error"
                                    Exit For
                                End If

                            End If


                            '3. insert to hist stok

                            If DateValue(tgl_old) <> DateValue(ttgl.EditValue) Then
                                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, kdgud, kdbar, drds(0).ToString, 0, "Sewa Barang", "-", "BE XXXX XX")
                            Else
                                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, kdgud, kdbar, drds(0).ToString, 0, "Sewa Barang", "-", "BE XXXX XX")
                            End If

                            Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, kdgud, kdbar, 0, qtykecil, "Sewa Barang", "-", "BE XXXX XX")




                        End If

                    End If
                End If

                If shasil = False Then

                    '1. insert faktur_to
                    Dim sqlins As String = String.Format("insert into trsewa2 (nobukti,kd_gudang,kd_barang,qty,satuan,harga,jumlah,qtykecil) values ('{0}','{1}','{2}',{3},'{4}',{5},{6},{7})", tbukti.EditValue, _
                                                         kdgud, kdbar, Replace(qty, ",", "."), satuan, Replace(harga, ",", "."), Replace(jumlah, ",", "."), Replace(qtykecil, ",", "."))

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

                    ' historical toko
                    Dim hasiltok As String = Hist_PinjamSewa_Toko(tkd_toko.Text.Trim, kdbar, qtykecil, cn, sqltrans, True, False)
                    If Not hasiltok.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasiltok, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        Exit For
                    End If


                    '3. insert to hist stok
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, kdgud, kdbar, 0, qtykecil, "Sewa Barang", "-", "BE XXXX XX")

                End If


            End If

        Next

        If Not hasil.Equals("error") Then
            hasil = "ok"
        End If

        Return hasil

    End Function

    Private Sub hapus()

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        If addstat = True Then
            dv1.Delete(Me.BindingContext(dv1).Position)
            tbrutto.EditValue = GridView1.Columns("jumlah").SummaryItem.SummaryValue
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

                Dim sql As String = String.Format("delete from trsewa2 where noid={0}", dv1(Me.BindingContext(dv1).Position)("noid").ToString)

                Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()

                Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, True, False, False)
                If Not hasilplusmin.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                    GoTo langsung
                End If

                ' historical toko
                Dim hasiltok As String = Hist_PinjamSewa_Toko(tkd_toko.Text.Trim, kdbar, qtykecil, cn, sqltrans, False, False)
                If Not hasiltok.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasiltok, vbOKOnly + vbExclamation, "Informasi")
                    GoTo langsung
                End If

                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, kdgud, kdbar, qtykecil, 0, "Sewa Barang", "-", "BE XXXX XX")

                dv1.Delete(Me.BindingContext(dv1).Position)
                tbrutto.EditValue = GridView1.Columns("jumlah").SummaryItem.SummaryValue


                '' update header -------------------------------

                '2. update piutang toko
                Dim sqlct As String = String.Format("select netto,kd_toko from trsewa where nobukti='{0}'", tbukti.Text.Trim)

                Dim cmdt As OleDbCommand = New OleDbCommand(sqlct, cn, sqltrans)
                Dim drdt As OleDbDataReader = cmdt.ExecuteReader

                If drdt.Read Then
                    If IsNumeric(drdt("netto").ToString) Then

                        Dim nett_sebelum As Double = drdt("netto").ToString

                        Dim sqluptoko As String = String.Format("update ms_toko set piutangsewa=piutangsewa - {0} where kd_toko='{1}'", Replace(nett_sebelum, ",", "."), tkd_toko.Text.Trim)
                        Dim sqluptoko2 As String = String.Format("update ms_toko set piutangsewa=piutangsewa + {0} where kd_toko='{1}'", Replace(tnetto.EditValue, ",", "."), tkd_toko.Text.Trim)

                        Dim cmdtk As OleDbCommand = New OleDbCommand(sqluptoko, cn, sqltrans)
                        cmdtk.ExecuteNonQuery()

                        Dim cmdtk2 As OleDbCommand = New OleDbCommand(sqluptoko2, cn, sqltrans)
                        cmdtk2.ExecuteNonQuery()

                    End If
                End If
                drdt.Close()

                '1. update faktur
                Dim sqlup_faktur As String = String.Format("update trsewa set disc_per={0},disc_rp={1},brutto={2},netto={3} where nobukti='{4}'", Replace(tdisc_per.EditValue, ",", "."), Replace(tdisc_rp.EditValue, ",", "."), Replace(tbrutto.EditValue, ",", "."), Replace(tnetto.EditValue, ",", "."), tbukti.Text.Trim)

                cmd = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()


                ' end of update faktur ---------------------------------------------------

                sqltrans.Commit()

                MsgBox("Data dihapus...", vbOKOnly + vbInformation, "Informasi")

langsung:

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


        End If

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("nobukti") = tbukti.Text.Trim
        orow("tanggal") = ttgl.Text.Trim
        orow("tgl_keluar") = ttgl_mt.Text.Trim
        orow("tanggal1") = ttgl_sewa1.Text.Trim
        orow("tanggal2") = ttgl_sewa2.Text.Trim
        orow("kd_toko") = tkd_toko.Text.Trim
        orow("nama_toko") = tnama_toko.Text.Trim
        orow("alamat_toko") = talamat_toko.Text.Trim
        orow("netto") = tnetto.EditValue
        orow("skembali") = 0
        orow("sbatal") = 0
        orow("jmlbayar") = 0

        dv.EndInit()

    End Sub

    Private Sub updateview()

        dv(position)("nobukti") = tbukti.Text.Trim
        dv(position)("tanggal") = ttgl.Text.Trim
        dv(position)("tgl_keluar") = ttgl_mt.Text.Trim
        dv(position)("tanggal1") = ttgl_sewa1.Text.Trim
        dv(position)("tanggal2") = ttgl_sewa2.Text.Trim
        dv(position)("kd_toko") = tkd_toko.Text.Trim
        dv(position)("nama_toko") = tnama_toko.Text.Trim
        dv(position)("alamat_toko") = talamat_toko.Text.Trim
        dv(position)("netto") = tnetto.EditValue

    End Sub

    Private Sub bts_toko_Click(sender As System.Object, e As System.EventArgs) Handles bts_toko.Click
        Dim fs As New fsoutlet With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .kdsales = ""}
        fs.ShowDialog(Me)

        tkd_toko.EditValue = fs.get_KODE
        tnama_toko.EditValue = fs.get_NAMA
        talamat_toko.EditValue = fs.get_ALAMAT

        tkd_toko_EditValueChanged(sender, Nothing)


    End Sub

    Private Sub tkd_toko_EditValueChanged(sender As Object, e As System.EventArgs) Handles tkd_toko.Validated
        If tkd_toko.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select kd_toko,nama_toko,alamat_toko from ms_toko where kd_toko='{0}' and aktif=1", tkd_toko.Text.Trim)

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

    Private Sub btadd_Click(sender As System.Object, e As System.EventArgs) Handles btadd.Click
        Using fkar2 As New fsewa3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0}
            fkar2.ShowDialog(Me)

            tbrutto.EditValue = GridView1.Columns("jumlah").SummaryItem.SummaryValue

        End Using
    End Sub

    Private Sub btedit_Click(sender As System.Object, e As System.EventArgs) Handles btedit.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Using fkar2 As New fsewa3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = Me.BindingContext(dv1).Position}
            fkar2.ShowDialog(Me)

            tbrutto.EditValue = GridView1.Columns("jumlah").SummaryItem.SummaryValue

        End Using

    End Sub

    Private Sub btdel_Click(sender As System.Object, e As System.EventArgs) Handles btdel.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        hapus()

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub frekap_to2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tkd_toko.Focus()
    End Sub

    Private Sub frekap_to2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ttgl.EditValue = Date.Now
        ttgl_mt.EditValue = Date.Now

        ttgl_sewa1.EditValue = Date.Now
        ttgl_sewa2.EditValue = Date.Now

        If addstat = False Then
            isi()
        Else
            kosongkan()
        End If

    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tkd_toko.Text.Trim.Length = 0 Then
            MsgBox("Outlet harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tkd_toko.Focus()
            Return
        End If

       
        If IsNothing(dv1) Then
            MsgBox("Tidak ada barang yang akan dijual", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If dv1.Count <= 0 Then
            MsgBox("Tidak ada barang yang akan dijual", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If MsgBox("Yakin sudah benar.. ???", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.No Then
            Return
        Else
            simpan()
        End If

    End Sub

    Private Sub tdisc_per_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tdisc_per.Validated

        If tdisc_per.EditValue > 0 Then

            Dim brutto As Double = tbrutto.EditValue
            Dim disc As Double = tdisc_per.EditValue / 100
            Dim hasil As Double = brutto * disc

            tdisc_rp.EditValue = hasil
        Else
            tdisc_rp.EditValue = 0
        End If

        totalnetto()

    End Sub

    Private Sub tdisc_rp_Validated(sender As Object, e As System.EventArgs) Handles tdisc_rp.Validated

        If tdisc_rp.EditValue > 0 Then

            Dim brutto As Double = tbrutto.EditValue
            Dim disc As Double = tdisc_rp.EditValue
            Dim hasil As Double = (disc / brutto) * 100

            tdisc_per.EditValue = hasil
        Else
            tdisc_per.EditValue = 0.0
        End If

        totalnetto()

    End Sub

    Private Sub tbrutto_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbrutto.EditValueChanged
        tdisc_per_EditValueChanged(sender, Nothing)
        totalnetto()
    End Sub

End Class