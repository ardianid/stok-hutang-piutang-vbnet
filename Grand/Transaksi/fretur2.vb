Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy
Imports DevExpress.XtraReports.UI

Public Class fretur2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private tgl_old As String
    Private supir_old As String
    Private nopol_old As String
    Private no_nota_old As String
    Public statprint As Boolean

    Private Sub kosongkan()
        tbukti.Text = "<< New >>"

        tkd_toko.Text = ""
        tnama_toko.Text = ""
        talamat_toko.Text = ""


        tket.Text = ""

        tdisc_per.EditValue = 0.0
        tdisc_rp.EditValue = 0
        tnetto.EditValue = 0

        tkd_supir.Text = ""
        tnama_supir.Text = ""

        opengrid()

        tbrutto.EditValue = 0

    End Sub

    Private Sub opengrid()

        Dim sql As String = String.Format("SELECT trretur2.noid, trretur2.kd_gudang,trretur2.jenis_ret, trretur2.kd_barang, ms_barang.nama_barang, trretur2.qty, trretur2.satuan, trretur2.harga, trretur2.disc_per, trretur2.disc_rp, trretur2.jumlah, trretur2.qtykecil " & _
            "FROM trretur2 INNER JOIN ms_barang ON trretur2.kd_barang = ms_barang.kd_barang where trretur2.nobukti='{0}'", tbukti.Text.Trim)


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
        Dim sql As String = String.Format("SELECT trretur.nobukti, trretur.tanggal, trretur.tgl_masuk, trretur.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, " & _
        "trretur.alasan_retur, trretur.brutto, trretur.disc_per, trretur.disc_rp, trretur.netto, trretur.note,trretur.nopol,trretur.kd_supir,trretur.no_nota " & _
        "FROM   trretur INNER JOIN ms_toko ON trretur.kd_toko = ms_toko.kd_toko where trretur.nobukti='{0}'", nobukti)

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
                        ttgl_tempo.EditValue = DateValue(dread("tgl_masuk").ToString)

                        tgl_old = ttgl_tempo.EditValue

                        tkd_toko.EditValue = dread("kd_toko").ToString
                        tnama_toko.EditValue = dread("nama_toko").ToString
                        talamat_toko.EditValue = dread("alamat_toko").ToString

                        tnota.EditValue = dread("no_nota").ToString

                        no_nota_old = dread("no_nota").ToString

                        talasan.Text = dread("alasan_retur").ToString

                        tbrutto.EditValue = dread("brutto").ToString
                        tdisc_per.EditValue = dread("disc_per").ToString
                        tdisc_rp.EditValue = dread("disc_rp").ToString
                        tnetto.EditValue = dread("netto").ToString

                        tket.EditValue = dread("note").ToString

                        If Not dread("nopol").ToString.Equals("") Then
                            tnopol.EditValue = dread("nopol").ToString
                            nopol_old = dread("nopol").ToString
                        Else
                            nopol_old = ""
                        End If

                        If Not dread("kd_supir").ToString.Equals("") Then
                            tkd_supir.EditValue = dread("kd_supir").ToString
                            supir_old = dread("kd_supir").ToString
                            tkd_supir_Validated(Nothing, Nothing)
                        Else
                            supir_old = ""
                        End If


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

                tkd_supir.EditValue = ""
                tnama_supir.EditValue = ""

                tbrutto.EditValue = 0
                tdisc_per.EditValue = 0
                tdisc_rp.EditValue = 0
                tnetto.EditValue = 0

                tket.EditValue = ""

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

    Private Sub isi_alasan()

        Const sql As String = "select alasan from ms_alasan where tipe='RETUR'"

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            talasan.Properties.Items.Clear()

            While drd.Read

                talasan.Properties.Items.Add(drd(0).ToString)

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

        sql = String.Format("select max(nobukti) from trretur where nobukti like '%RT.{0}%'", tahunbulan)

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

        Dim jenisnota As String = "RT."

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
                Dim sqlin_faktur As String = String.Format("insert into trretur (nobukti,tanggal,tgl_masuk,kd_toko,alasan_retur,note,brutto,disc_per,disc_rp,netto,nopol,kd_supir,no_nota) values('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7},{8},{9},'{10}','{11}','{12}')", _
                                    tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_tempo.EditValue), tkd_toko.EditValue, talasan.Text.Trim, tket.Text.Trim, Replace(tbrutto.EditValue, ",", "."), Replace(tdisc_per.EditValue, ",", "."), Replace(tdisc_rp.EditValue, ",", "."), Replace(tnetto.EditValue, ",", "."), tnopol.EditValue, tkd_supir.Text.Trim, tnota.Text.Trim)


                cmd = New OleDbCommand(sqlin_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()

                '2. update piutangtoko
                Dim sqltoko As String = String.Format("update ms_toko set jumlahretur=jumlahretur + {0} where kd_toko='{1}'", Replace(tnetto.EditValue, ",", "."), tkd_toko.Text.Trim)

                cmdtoko = New OleDbCommand(sqltoko, cn, sqltrans)
                cmdtoko.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btretur", 1, 0, 0, 0, tbukti.Text.Trim, ttgl.EditValue, sqltrans)


            Else

            '2. update piutang toko
                Dim sqlct As String = String.Format("select netto,kd_toko from trretur where nobukti='{0}'", tbukti.Text.Trim)

            Dim cmdt As OleDbCommand = New OleDbCommand(sqlct, cn, sqltrans)
            Dim drdt As OleDbDataReader = cmdt.ExecuteReader

            If drdt.Read Then
                If IsNumeric(drdt("netto").ToString) Then

                    Dim nett_sebelum As Double = drdt("netto").ToString

                        Dim sqluptoko As String = String.Format("update ms_toko set jumlahretur=jumlahretur - {0} where kd_toko='{1}'", Replace(nett_sebelum, ",", "."), tkd_toko.Text.Trim)
                        Dim sqluptoko2 As String = String.Format("update ms_toko set jumlahretur=jumlahretur + {0} where kd_toko='{1}'", Replace(tnetto.EditValue, ",", "."), tkd_toko.Text.Trim)

                    Dim cmdtk As OleDbCommand = New OleDbCommand(sqluptoko, cn, sqltrans)
                    cmdtk.ExecuteNonQuery()

                    Dim cmdtk2 As OleDbCommand = New OleDbCommand(sqluptoko2, cn, sqltrans)
                    cmdtk2.ExecuteNonQuery()

                End If
            End If
            drdt.Close()

            '1. update faktur
                Dim sqlup_faktur As String = String.Format("update trretur set tanggal='{0}',tgl_masuk='{1}',kd_toko='{2}',alasan_retur='{3}',note='{4}',brutto={5},disc_per={6},disc_rp={7},netto={8},nopol='{9}',kd_supir='{10}',no_nota='{11}' where nobukti='{12}'", convert_date_to_eng(ttgl.EditValue), _
                                                           convert_date_to_eng(ttgl_tempo.EditValue), tkd_toko.EditValue, talasan.Text.Trim, tket.Text.Trim, Replace(tbrutto.EditValue, ",", "."), Replace(tdisc_per.EditValue, ",", "."), Replace(tdisc_rp.EditValue, ",", "."), Replace(tnetto.EditValue, ",", "."), tnopol.EditValue, tkd_supir.Text.Trim, tnota.Text.Trim, tbukti.Text.Trim)

            cmd = New OleDbCommand(sqlup_faktur, cn, sqltrans)
            cmd.ExecuteNonQuery()


                Clsmy.InsertToLog(cn, "btretur", 0, 1, 0, 0, tbukti.Text.Trim, ttgl.EditValue, sqltrans)


            End If

            If simpan2(cn, sqltrans) = "ok" Then

                If addstat = True Then
                    insertview()
                Else
                    updateview()
                End If

                sqltrans.Commit()

                close_wait()

                '  MsgBox("Data disimpan...", vbOKOnly + vbInformation, "Informasi")


                If addstat = True Then

                    If statprint Then
                        If MsgBox("Retur akan langsung diprint.. ???", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
                            LoadPrint()
                        End If
                    End If

                    kosongkan()
                    ttgl.Focus()
                Else
                    close_wait()

                    If statprint Then
                        If MsgBox("Retur akan langsung diprint.. ???", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
                            LoadPrint()
                        End If
                    End If

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
            Dim jenis_ret As String = dv1(i)("jenis_ret").ToString
            Dim harga As String = dv1(i)("harga").ToString
            Dim disc_per As String = dv1(i)("disc_per").ToString
            Dim disc_rp As String = dv1(i)("disc_rp").ToString
            Dim jumlah As String = dv1(i)("jumlah").ToString
            Dim qtykecil As String = dv1(i)("qtykecil").ToString
            Dim noid As String = dv1(i)("noid").ToString

            ' If i = 0 Then
            cek_tglhist(cn, sqltrans, kdbar)
            ' End If

            If addstat = True Then

                '1. insert faktur_to
                Dim sqlins As String = String.Format("insert into trretur2 (nobukti,kd_gudang,kd_barang,qty,satuan,harga,disc_per,disc_rp,jumlah,qtykecil,jenis_ret) " & _
                                                     "values('{0}','{1}','{2}',{3},'{4}',{5},{6},{7},{8},{9},'{10}')", tbukti.EditValue, kdgud, kdbar, Replace(qty, ",", "."), satuan, Replace(harga, ",", "."), Replace(disc_per, ",", "."), Replace(disc_rp, ",", "."), Replace(jumlah, ",", "."), Replace(qtykecil, ",", "."), jenis_ret)

                cmd = New OleDbCommand(sqlins, cn, sqltrans)
                cmd.ExecuteNonQuery()

                ' If tnota.Text.Trim.Length = 0 Then

                If apakah_brg_kembali(cn, sqltrans, kdbar) = True Then

                    Dim sql As String = String.Format("select qty1,qty2,qty3 from ms_barang where kd_barang='{0}'", kdbar)
                    Dim cmdc As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                    Dim drc As OleDbDataReader = cmdc.ExecuteReader

                    If drc.Read Then

                        If IsNumeric(drc(0).ToString) Then

                            If jenis_ret.Equals("RETUR") Then

                                Dim simpankosong_fh As String = simpankosong_f(cn, sqltrans, kdbar, tkd_toko.Text.Trim, drc("qty1").ToString, drc("qty2").ToString, drc("qty3").ToString, qtykecil, False)

                                If Not simpankosong_fh.Equals("ok") Then
                                    close_wait()
                                    If Not IsNothing(sqltrans) Then
                                        sqltrans.Rollback()
                                    End If

                                    MsgBox(simpankosong_fh)
                                    hasil = "error"
                                    Exit For
                                Else
                                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, "None B", kdbar, 0, qtykecil, "Retur", tkd_supir.Text.Trim, tnopol.EditValue)
                                End If


                            Else

                                Dim hsilpinjm As String = Hist_PinjamSewa_Toko(tkd_toko.Text.Trim, kdbar, qtykecil, cn, sqltrans, False, True)
                                If Not hsilpinjm.Equals("ok") Then
                                    close_wait()

                                    If Not IsNothing(sqltrans) Then
                                        sqltrans.Rollback()
                                    End If

                                    MsgBox(hsilpinjm, vbOKOnly + vbExclamation, "Informasi")
                                    hasil = "error"
                                    Exit For
                                End If

                                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, "None P", kdbar, 0, qtykecil, "Retur", tkd_supir.Text.Trim, tnopol.EditValue)


                            End If
                        End If

                    End If
                    drc.Close()

                End If

                '2. update barang
                Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, True, False, False)
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
                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, kdgud, kdbar, qtykecil, 0, "Retur Barang", tkd_supir.Text.Trim, tnopol.EditValue)

                '  End If

            Else

                Dim sqlc As String = String.Format("select qtykecil,jenis_ret from trretur2 where noid={0}", noid)
                Dim cmds As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
                Dim drds As OleDbDataReader = cmds.ExecuteReader

                Dim shasil As Boolean
                shasil = False

                If drds.HasRows Then
                    If drds.Read Then

                        If IsNumeric(drds(0).ToString) Then

                            shasil = True

                            Dim qtyold As Integer = drds("qtykecil").ToString
                            Dim jenis_ret_old As String = drds("jenis_ret").ToString

                            '  If no_nota_old.Trim.ToString.Length = 0 Then

                            If apakah_brg_kembali(cn, sqltrans, kdbar) = True Then

                                Dim sql As String = String.Format("select qty1,qty2,qty3 from ms_barang where kd_barang='{0}'", kdbar)
                                Dim cmdc As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                                Dim drc As OleDbDataReader = cmdc.ExecuteReader

                                If drc.Read Then

                                    If IsNumeric(drc(0).ToString) Then

                                        If jenis_ret_old.Equals("RETUR") Then
                                            Dim simpan_kosongf As String = simpankosong_f(cn, sqltrans, kdbar, tkd_toko.Text.Trim, drc("qty1").ToString, drc("qty2").ToString, drc("qty3").ToString, qtyold, True)
                                            If Not simpan_kosongf.Equals("ok") Then

                                                close_wait()
                                                If Not IsNothing(sqltrans) Then
                                                    sqltrans.Rollback()
                                                End If

                                                MsgBox(simpan_kosongf)
                                                hasil = "error"
                                                Exit For
                                            Else
                                                '3. insert to hist stok
                                                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, "None B", kdbar, qtyold, 0, "Retur (Edit)", tkd_supir.Text.Trim, tnopol.EditValue)
                                            End If


                                        Else

                                            Dim hsilpinjm As String = Hist_PinjamSewa_Toko(tkd_toko.Text.Trim, kdbar, qtyold, cn, sqltrans, True, True)
                                            If Not hsilpinjm.Equals("ok") Then
                                                close_wait()

                                                If Not IsNothing(sqltrans) Then
                                                    sqltrans.Rollback()
                                                End If

                                                MsgBox(hsilpinjm, vbOKOnly + vbExclamation, "Informasi")
                                                hasil = "error"
                                                Exit For
                                            End If

                                            Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, "None P", kdbar, qtyold, 0, "Retur (Edit)", tkd_supir.Text.Trim, tnopol.EditValue)


                                        End If



                                        If jenis_ret.Equals("RETUR") Then

                                            Dim simpan_kosongf2 As String = simpankosong_f(cn, sqltrans, kdbar, tkd_toko.Text.Trim, drc("qty1").ToString, drc("qty2").ToString, drc("qty3").ToString, qtykecil, False)
                                            If Not simpan_kosongf2.Equals("ok") Then

                                                close_wait()
                                                If Not IsNothing(sqltrans) Then
                                                    sqltrans.Rollback()
                                                End If

                                                MsgBox(simpan_kosongf2)
                                                hasil = "error"
                                                Exit For
                                            Else
                                                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, "None B", kdbar, 0, qtykecil, "Retur (Edit)", tkd_supir.Text.Trim, tnopol.EditValue)
                                            End If

                                        Else

                                            Dim hsilpinjm As String = Hist_PinjamSewa_Toko(tkd_toko.Text.Trim, kdbar, qtykecil, cn, sqltrans, False, True)
                                            If Not hsilpinjm.Equals("ok") Then
                                                close_wait()

                                                If Not IsNothing(sqltrans) Then
                                                    sqltrans.Rollback()
                                                End If

                                                MsgBox(hsilpinjm, vbOKOnly + vbExclamation, "Informasi")
                                                hasil = "error"
                                                Exit For
                                            End If

                                            Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, "None P", kdbar, 0, qtykecil, "Retur (Edit)", tkd_supir.Text.Trim, tnopol.EditValue)

                                        End If

                                        

                                    End If

                                End If
                                drc.Close()

                            End If

                            '2. update barang
                            Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, drds(0).ToString, kdbar, kdgud, False, False, False)
                            If Not hasilplusmin.Equals("ok") Then
                                close_wait()

                                If Not IsNothing(sqltrans) Then
                                    sqltrans.Rollback()
                                End If

                                MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                                hasil = "error"
                                Exit For
                            Else

                                Dim hasilplusmin2 As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, True, False, False)

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


                            '3. insert to hist stok

                            If DateValue(tgl_old) <> DateValue(ttgl_tempo.EditValue) Or nopol_old <> tnopol.EditValue Or supir_old <> tkd_supir.EditValue Then
                                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, kdgud, kdbar, 0, drds(0).ToString, "Retur Barang", supir_old, nopol_old)
                            Else
                                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, kdgud, kdbar, 0, drds(0).ToString, "Retur Barang", tkd_supir.Text.Trim, tnopol.EditValue)
                            End If

                            Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, kdgud, kdbar, qtykecil, 0, "Retur Barang", tkd_supir.Text.Trim, tnopol.EditValue)
                            ' End If

                            '1. update faktur to
                            Dim sqlup As String = String.Format("update trretur2 set kd_gudang='{0}',kd_barang='{1}',qty={2},satuan='{3}',harga={4},disc_per={5},disc_rp={6},jumlah={7},qtykecil={8},jenis_ret='{9}' where nobukti='{10}' and noid={11}", _
                                                                kdgud, kdbar, Replace(qty, ",", "."), satuan, Replace(harga, ",", "."), Replace(disc_per, ",", "."), Replace(disc_rp, ",", "."), Replace(jumlah, ",", "."), Replace(qtykecil, ",", "."), jenis_ret, tbukti.Text.Trim, noid)

                            cmd = New OleDbCommand(sqlup, cn, sqltrans)
                            cmd.ExecuteNonQuery()

                        End If



                    End If

                End If


                If shasil = False Then

                    '1. insert faktur_to
                    Dim sqlins As String = String.Format("insert into trretur2 (nobukti,kd_gudang,kd_barang,qty,satuan,harga,disc_per,disc_rp,jumlah,qtykecil,jenis_ret) " & _
                                                         "values('{0}','{1}','{2}',{3},'{4}',{5},{6},{7},{8},{9},'{10}')", tbukti.EditValue, kdgud, kdbar, Replace(qty, ",", "."), satuan, Replace(harga, ",", "."), Replace(disc_per, ",", "."), Replace(disc_rp, ",", "."), Replace(jumlah, ",", "."), Replace(qtykecil, ",", "."), jenis_ret)

                    cmd = New OleDbCommand(sqlins, cn, sqltrans)
                    cmd.ExecuteNonQuery()

                    ' If tnota.Text.Trim.Length = 0 Then

                    If apakah_brg_kembali(cn, sqltrans, kdbar) = True Then

                        Dim sql As String = String.Format("select qty1,qty2,qty3 from ms_barang where kd_barang='{0}'", kdbar)
                        Dim cmdc As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                        Dim drc As OleDbDataReader = cmdc.ExecuteReader

                        If drc.Read Then
                            If IsNumeric(drc(0).ToString) Then

                                If jenis_ret.Equals("RETUR") Then

                                    Dim simpan_kosongf3 As String = simpankosong_f(cn, sqltrans, kdbar, tkd_toko.Text.Trim, drc("qty1").ToString, drc("qty2").ToString, drc("qty3").ToString, qtykecil, False)
                                    If Not simpan_kosongf3.Trim.Equals("ok") Then

                                        close_wait()
                                        If Not IsNothing(sqltrans) Then
                                            sqltrans.Rollback()
                                        End If

                                        MsgBox(simpan_kosongf3)
                                        hasil = "error"
                                        Exit For
                                    Else
                                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, "None B", kdbar, 0, qtykecil, "Retur", tkd_supir.Text.Trim, tnopol.EditValue)
                                    End If

                                Else

                                    Dim hsilpinjm As String = Hist_PinjamSewa_Toko(tkd_toko.Text.Trim, kdbar, qtykecil, cn, sqltrans, False, True)
                                    If Not hsilpinjm.Equals("ok") Then
                                        close_wait()

                                        If Not IsNothing(sqltrans) Then
                                            sqltrans.Rollback()
                                        End If

                                        MsgBox(hsilpinjm, vbOKOnly + vbExclamation, "Informasi")
                                        hasil = "error"
                                        Exit For
                                    End If

                                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, "None P", kdbar, 0, qtykecil, "Retur", tkd_supir.Text.Trim, tnopol.EditValue)


                                End If

                                

                            End If

                        End If
                        drc.Close()

                    End If

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, True, False, False)
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
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, kdgud, kdbar, qtykecil, 0, "Retur Barang", tkd_supir.Text.Trim, tnopol.EditValue)
                    ' End If

                End If

            End If

        Next

        If Not hasil.Equals("error") Then
            hasil = "ok"
        End If

        Return hasil

    End Function

    Private Sub cek_tglhist(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal kdbarang As String)

        Dim sql As String = String.Format("select tglretur from ms_barang where kd_barang='{0}'", kdbarang)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim hasil As Boolean = False
        Dim tglhasil As String = ""

        If drd.Read Then
            If IsDate(drd(0).ToString) Then
                hasil = True
                tglhasil = drd(0).ToString
            End If
        End If

        Dim sqlup As String = ""
        If hasil = False Then
            sqlup = String.Format("update ms_barang set tglretur='{0}' where kd_barang='{1}'", convert_date_to_eng(ttgl.EditValue), kdbarang)

            Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                cmdup.ExecuteNonQuery()
            End Using

        Else

            If DateValue(ttgl.EditValue) > DateValue(tglhasil) Then
                sqlup = String.Format("update ms_barang set tglretur='{0}' where kd_barang='{1}'", convert_date_to_eng(ttgl.EditValue), kdbarang)

                Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                    cmdup.ExecuteNonQuery()
                End Using

            End If

        End If

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("nobukti") = tbukti.Text.Trim
        orow("tanggal") = ttgl.Text.Trim
        orow("tgl_masuk") = ttgl_tempo.Text.Trim
        orow("kd_toko") = tkd_toko.Text.Trim
        orow("nama_toko") = tnama_toko.Text.Trim
        orow("alamat_toko") = talamat_toko.Text.Trim
        orow("nopol") = tnopol.EditValue
        orow("nama_supir") = tnama_supir.Text.Trim
        orow("kd_karyawan") = tkd_supir.Text.Trim
        orow("alasan_retur") = talasan.Text.Trim
        orow("netto") = tnetto.EditValue
        orow("spotong") = 0
        orow("sbatal") = 0

        dv.EndInit()

    End Sub

    Private Sub updateview()

        dv(position)("nobukti") = tbukti.Text.Trim
        dv(position)("tanggal") = ttgl.Text.Trim
        dv(position)("tgl_masuk") = ttgl_tempo.Text.Trim
        dv(position)("kd_toko") = tkd_toko.Text.Trim
        dv(position)("nama_toko") = tnama_toko.Text.Trim
        dv(position)("alamat_toko") = talamat_toko.Text.Trim
        dv(position)("nopol") = tnopol.EditValue
        dv(position)("nama_supir") = tnama_supir.Text.Trim
        dv(position)("kd_karyawan") = tkd_supir.Text.Trim
        dv(position)("alasan_retur") = talasan.Text.Trim
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


    '' supir

    Private Sub bts_supir_Click(sender As System.Object, e As System.EventArgs) Handles bts_supir.Click

        Dim fs As New fssupir With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .issales = True}
        fs.ShowDialog(Me)

        tkd_supir.EditValue = fs.get_KODE
        ' tnama_supir.EditValue = fs.get_NAMA

        tkd_supir_Validated(sender, Nothing)

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
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and (bagian like 'SUPIR%' or bagian='SALES') and kd_karyawan='{0}'", tkd_supir.Text.Trim)

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

                ' cek_supirkenek(cn)

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


    Private Sub btadd_Click(sender As System.Object, e As System.EventArgs) Handles btadd.Click
        Using fkar2 As New fretur3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0}
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

        Using fkar2 As New fretur3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = Me.BindingContext(dv1).Position}
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

        If addstat = True Then
            dv1.Delete(Me.BindingContext(dv1).Position)
            tbrutto.EditValue = GridView1.Columns("jumlah").SummaryItem.SummaryValue
        Else

            Dim cn As OleDbConnection = Nothing
            Dim sqltrans As OleDbTransaction = Nothing

            Try

                If Integer.Parse(dv1(Me.BindingContext(dv1).Position)("noid").ToString) = 0 Then
                    dv1.Delete(Me.BindingContext(dv1).Position)
                    tbrutto.EditValue = GridView1.Columns("jumlah").SummaryItem.SummaryValue
                    Return
                End If

                Dim qtykecil As Integer = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtykecil").ToString)
                Dim kdbar As String = dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString
                Dim kdgud As String = dv1(Me.BindingContext(dv1).Position)("kd_gudang").ToString
                Dim jenis_ret As String = dv1(Me.BindingContext(dv1).Position)("jenis_ret").ToString

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                sqltrans = cn.BeginTransaction

                Dim sqlc As String = String.Format("select * from trretur2 inner join ms_barang on trretur2.kd_barang=ms_barang.kd_barang where noid={0}", dv1(Me.BindingContext(dv1).Position)("noid").ToString)
                Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
                Dim drc As OleDbDataReader = cmdc.ExecuteReader

                If drc.Read Then
                    If IsNumeric(drc("noid").ToString) Then

                        Dim qtyold As Integer = Integer.Parse(drc("qtykecil").ToString)

                        If jenis_ret.Equals("RETUR") Then

                            Dim simpankosong_fh As String = simpankosong_f(cn, sqltrans, kdbar, tkd_toko.Text.Trim, drc("qty1").ToString, drc("qty2").ToString, drc("qty3").ToString, qtyold, True)
                            If Not simpankosong_fh.Equals("ok") Then
                                close_wait()
                                If Not IsNothing(sqltrans) Then
                                    sqltrans.Rollback()
                                End If

                                MsgBox(simpankosong_fh)
                                Return
                            Else
                                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, "None B", kdbar, qtyold, 0, "Retur (Edit)", tkd_supir.Text.Trim, tnopol.EditValue)
                            End If

                        Else

                            Dim hsilpinjm As String = Hist_PinjamSewa_Toko(tkd_toko.Text.Trim, kdbar, qtyold, cn, sqltrans, True, True)
                            If Not hsilpinjm.Equals("ok") Then
                                close_wait()

                                If Not IsNothing(sqltrans) Then
                                    sqltrans.Rollback()
                                End If

                                MsgBox(hsilpinjm, vbOKOnly + vbExclamation, "Informasi")
                                Return
                            End If

                            Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, "None P", kdbar, qtyold, 0, "Retur (Edit)", tkd_supir.Text.Trim, tnopol.EditValue)


                        End If

                        

                    End If
                End If
                drc.Close()

                Dim sql As String = String.Format("delete from trretur2 where noid={0}", dv1(Me.BindingContext(dv1).Position)("noid").ToString)

                Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()

                '2. update barang
                Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, False, False, False)
                If Not hasilplusmin.Equals("ok") Then

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                    GoTo langsung
                End If

                If addstat = False Then

                    If DateValue(tgl_old) <> DateValue(ttgl.EditValue) Or nopol_old <> tnopol.EditValue Or supir_old <> tkd_supir.EditValue Then
                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, kdgud, kdbar, qtykecil, 0, "Retur Barang", supir_old, nopol_old)
                    End If

                End If

                '3. insert to hist stok
                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, kdgud, kdbar, 0, qtykecil, "Retur Barang", tkd_supir.Text.Trim, tnopol.EditValue)

                dv1.Delete(Me.BindingContext(dv1).Position)
                tbrutto.EditValue = GridView1.Columns("jumlah").SummaryItem.SummaryValue

                ' update header ----------------------------------------
                '2. update piutang toko
                Dim sqlct As String = String.Format("select netto,kd_toko from trretur where nobukti='{0}'", tbukti.Text.Trim)

                Dim cmdt As OleDbCommand = New OleDbCommand(sqlct, cn, sqltrans)
                Dim drdt As OleDbDataReader = cmdt.ExecuteReader

                If drdt.Read Then
                    If IsNumeric(drdt("netto").ToString) Then

                        Dim nett_sebelum As Double = drdt("netto").ToString

                        Dim sqluptoko As String = String.Format("update ms_toko set jumlahretur=jumlahretur - {0} where kd_toko='{1}'", Replace(nett_sebelum, ",", "."), tkd_toko.Text.Trim)
                        Dim sqluptoko2 As String = String.Format("update ms_toko set jumlahretur=jumlahretur + {0} where kd_toko='{1}'", Replace(tnetto.EditValue, ",", "."), tkd_toko.Text.Trim)

                        Dim cmdtk As OleDbCommand = New OleDbCommand(sqluptoko, cn, sqltrans)
                        cmdtk.ExecuteNonQuery()

                        Dim cmdtk2 As OleDbCommand = New OleDbCommand(sqluptoko2, cn, sqltrans)
                        cmdtk2.ExecuteNonQuery()

                    End If
                End If
                drdt.Close()

                '1. update faktur
                Dim sqlup_faktur As String = String.Format("update trretur set brutto={0},disc_per={1},disc_rp={2},netto={3} where nobukti='{4}'",  Replace(tbrutto.EditValue, ",", "."), Replace(tdisc_per.EditValue, ",", "."), Replace(tdisc_rp.EditValue, ",", "."), Replace(tnetto.EditValue, ",", "."), tbukti.Text.Trim)

                Using cmdupfaktur As OleDbCommand = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                    cmdupfaktur.ExecuteNonQuery()
                End Using


                ' akhir update header ----------------------------------------


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

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub ffaktur_to2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tnota.Focus()
    End Sub

    Private Sub ffaktur_to2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        isi_nopol()
        isi_alasan()

        If addstat = True Then
            ttgl.EditValue = Date.Now
            ttgl_tempo.EditValue = Date.Now

            kosongkan()
        Else

            isi()

        End If
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tkd_toko.Text.Trim.Length = 0 Then
            MsgBox("Outlet harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tkd_toko.Focus()
            Return
        End If

        If IsNothing(dv1) Then
            MsgBox("Tidak ada barang yang akan diretur", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If dv1.Count <= 0 Then
            MsgBox("Tidak ada barang yang akan diretur", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If talasan.EditValue = "" Then
            MsgBox("Alasan harus dipilih...", vbOKOnly + vbInformation, "Informasi")
            talasan.Focus()
            Return
        End If

        If tnopol.EditValue = "" Then
            MsgBox("No Polisi harus diisi..", vbOKOnly + vbInformation, "Informasi")
            tnopol.Focus()
            Return
        End If

        If tkd_supir.EditValue = "" Then
            MsgBox("Supir harus diisi..", vbOKOnly + vbInformation, "Informasi")
            tkd_supir.Focus()
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

    Private Sub LoadPrint()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = String.Format("SELECT     trretur.nobukti, trretur.tanggal, trretur.tgl_masuk, ms_toko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, trretur.alasan_retur, trretur.brutto, " & _
                      "trretur.disc_rp AS disc, trretur.netto, ms_barang.kd_barang, ms_barang.nama_barang, trretur2.qty, trretur2.satuan, trretur2.harga, trretur2.disc_rp, trretur2.jumlah,trretur.nopol,ms_pegawai.nama_karyawan as nama_supir,trretur.no_nota " & _
                     "FROM         trretur INNER JOIN " & _
                      "trretur2 ON trretur.nobukti = trretur2.nobukti INNER JOIN " & _
                      "ms_toko ON trretur.kd_toko = ms_toko.kd_toko INNER JOIN " & _
                      "ms_barang ON trretur2.kd_barang = ms_barang.kd_barang LEFT OUTER JOIN " & _
                      "ms_pegawai on trretur.kd_supir=ms_pegawai.kd_karyawan " & _
                      "where trretur.sbatal=0 and trretur.nobukti='{0}'", tbukti.Text.Trim)

            Dim ds As DataSet = New dsretur
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_returbar() With {.DataSource = ds.Tables(0)}
            rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
            rrekap.DataMember = rrekap.DataMember
            rrekap.PrinterName = varprinter1
            rrekap.CreateDocument(True)
            rrekap.Print()

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

End Class