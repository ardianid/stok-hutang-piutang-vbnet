Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class ffaktur_b2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean
    Public statview As Boolean

    Public statverif As Boolean = False
    Public statverif_realisasi As Boolean

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private stat_faktur_kosong As Integer

    Private Sub kosongkan()
        tbukti.Text = "<< New >>"

        tnorekap.Text = ""
        tnopol.Text = ""
        tsupir.Text = ""
        ttgl_kirim.Text = ""
        tket.Text = ""
        tjalur.Text = ""

        stat_faktur_kosong = 0

        opengrid()
        opengrid2()

        XtraTabControl1.SelectedTabPageIndex = 0

    End Sub

    Private Sub opengrid()

        grid1.DataSource = Nothing
        dv1 = Nothing

        If tnopol.Text.Trim.Length = 0 Then
            Return
        End If

        Dim sql As String = String.Format("SELECT   1 as ok,'TERKIRIM'  as statkirim,trfaktur_to.alasan_batal, trrekap_to2.nobukti_fak as nobukti, trfaktur_to.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, trfaktur_to.netto, trfaktur_to.tanggal,0 as sbalik,trfaktur_to.jnis_jual2 " & _
            "FROM         trrekap_to2 INNER JOIN " & _
            "trfaktur_to ON trrekap_to2.nobukti_fak = trfaktur_to.nobukti INNER JOIN " & _
            "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko where trrekap_to2.nobukti='{0}'", tnorekap.Text.Trim)

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet



        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            grid1.DataSource = dv1

            cekon_fakturbalik(cn)

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

    Private Sub opengrid2()

        grid2.DataSource = Nothing
        dv2 = Nothing

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim sql As String = "SELECT     trfaktur_to2.nobukti, ms_barang.kd_barang, ms_barang.nama_barang, trfaktur_to2.qty, ms_barang.qty1, ms_barang.qty2, ms_barang.qty3, ms_barang.satuan1, " & _
                      "ms_barang.satuan2, ms_barang.satuan3, trfaktur_to2.qty AS qty_k, 0 AS qtykecil, trfaktur_to2.satuan, ms_barang3.kd_barang2 " & _
                     "FROM         trfaktur_to2 INNER JOIN " & _
                      "ms_barang3 ON trfaktur_to2.kd_barang = ms_barang3.kd_barang2 INNER JOIN " & _
                      "ms_barang ON ms_barang3.kd_barang = ms_barang.kd_barang " & _
                        "WHERE trfaktur_to2.nobukti in ("

        Dim sql2 As String = ""

        Dim a As Integer = 0
        For i As Integer = 0 To dv1.Count - 1

            If dv1(i)("ok").ToString.Equals("1") Then

                sql2 = String.Format("{0}'{1}',", sql2, dv1(i)("nobukti").ToString)
                a = a + 1
            End If

        Next

        If a > 0 Then
            sql2 = sql2.Substring(0, sql2.Length - 1)
            sql2 = String.Format("{0})", sql2)

            sql = String.Format("{0}{1}", sql, sql2)

        Else
            Return
        End If

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager2 = New DataViewManager(ds)
            dv2 = dvmanager2.CreateDataView(ds.Tables(0))

            grid2.DataSource = dv2

            cek_onkosong(cn)

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

    Private Sub cek_onkosong(ByVal cn As OleDbConnection)

        Dim cmd As OleDbCommand
        Dim drd As OleDbDataReader

        Dim kdgudang As String = ""

        For i As Integer = 0 To dv2.Count - 1

            Dim sql As String = String.Format("select * from trfaktur_to3 where nobukti='{0}' and kd_barang='{1}' and satuan='{2}'", _
                                              dv2(i)("nobukti").ToString, dv2(i)("kd_barang").ToString, dv2(i)("satuan"))
            cmd = New OleDbCommand(sql, cn)
            drd = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd("noid").ToString) Then

                    kdgudang = drd("kd_gudang").ToString

                    dv2(i)("qty_k") = drd("qty").ToString
                    dv2(i)("qtykecil") = drd("qtykecil").ToString
                End If
            End If

            drd.Close()

        Next

        tgudang.EditValue = kdgudang

    End Sub

    Private Sub cekon_fakturbalik(ByVal cn As OleDbConnection)

        Dim cmd As OleDbCommand
        Dim drd As OleDbDataReader

        For i As Integer = 0 To dv1.Count - 1

            Dim sql As String = String.Format("select nobukti_fak,statkirim,spulang from trfaktur_balik2 where nobukti='{0}' and nobukti_fak='{1}'", tbukti.Text.Trim, dv1(i)("nobukti").ToString)
            cmd = New OleDbCommand(sql, cn)
            drd = cmd.ExecuteReader

            If drd.Read Then

                If Not drd("statkirim").ToString.Equals("") Then
                    If Integer.Parse(drd("spulang").ToString) = 1 Then
                        dv1(i)("ok") = 1
                    Else
                        dv1(i)("ok") = 0
                    End If

                    dv1(i)("statkirim") = drd("statkirim").ToString

                Else
                    dv1(i)("ok") = 0
                    dv1(i)("statkirim") = "BELUM TERKIRIM"
                End If

            End If

            drd.Close()

        Next

    End Sub

    Private Sub isi()

        Dim nobukti As String = ""

        If statverif = False Then
            nobukti = dv(position)("nobukti").ToString
        Else
            nobukti = dv(position)("nobukti_real").ToString
        End If

        Dim sql As String = "SELECT     trfaktur_balik.tanggal,trfaktur_balik.tgl_masuk, trfaktur_balik.nobukti_rkp,trfaktur_balik.kd_gudang, trrekap_to.tglkirim, trrekap_to.nopol, ms_pegawai.nama_karyawan, trfaktur_balik.note," & _
            "ms_jalur_kirim.nama_jalur,trrekap_to.sfaktur_kosong " & _
            "FROM         trfaktur_balik INNER JOIN " & _
            "trrekap_to ON trfaktur_balik.nobukti_rkp = trrekap_to.nobukti LEFT OUTER JOIN " & _
            "ms_pegawai ON trrekap_to.kd_supir = ms_pegawai.kd_karyawan LEFT OUTER JOIN " & _
            "ms_jalur_kirim ON trrekap_to.kd_jalur = ms_jalur_kirim.kd_jalur"

        sql = String.Format(" {0} where trfaktur_balik.nobukti='{1}'", sql, nobukti)

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim dread As OleDbDataReader = comd.ExecuteReader
            Dim hasil As Boolean

            If dread.HasRows Then
                If dread.Read Then

                    If Not dread("nobukti_rkp").ToString.Equals("") Then

                        hasil = True

                        tbukti.EditValue = nobukti
                        ttgl.EditValue = DateValue(dread("tanggal").ToString)
                        ttgl_msk.EditValue = DateValue(dread("tgl_masuk").ToString)
                        tnorekap.EditValue = dread("nobukti_rkp").ToString
                        ttgl_kirim.EditValue = convert_date_to_ind(dread("tglkirim").ToString)
                        tnopol.EditValue = dread("nopol").ToString
                        tsupir.EditValue = dread("nama_karyawan").ToString
                        tket.EditValue = dread("note").ToString
                        tjalur.EditValue = dread("nama_jalur").ToString

                        tgudang.EditValue = dread("kd_gudang").ToString

                        stat_faktur_kosong = Integer.Parse(dread("sfaktur_kosong").ToString)

                        opengrid()
                        'opengrid2()

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

    Private Sub isi_gudang()

        Dim sql As String = "select kd_gudang,nama_gudang from ms_gudang where tipe_gudang='FISIK'"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet
        Dim dvg As DataView

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            'Dim orow As DataRow = ds.Tables(0).NewRow
            'orow("kd_barang") = "None"
            'orow("nama_barang") = "None"
            'ds.Tables(0).Rows.InsertAt(orow, 0)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvg = dvm.CreateDataView(ds.Tables(0))

            tgudang.Properties.DataSource = dvg

            tgudang.ItemIndex = 0

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

        Dim bulantahun As String = String.Format("{0}{1}", tahun, bulan)

        Dim sql As String = String.Format("select max(nobukti) from trfaktur_balik where nobukti like '%FB.{0}%'", bulantahun)

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

        Return String.Format("FB.{0}{1}{2}", tahun, bulan, kbukti)

    End Function

    Private Function proses_stok(ByVal tambah As Boolean, ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim hasil As String = "ok"

        Dim sql As String = String.Format("select * from trrekap_to3 where nobukti='{0}'", tnorekap.Text.Trim)
        Dim cmd_rek As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd_rek As OleDbDataReader = cmd_rek.ExecuteReader

        While drd_rek.Read

            Dim kdbarang As String = drd_rek("kd_barang").ToString
            Dim jmlmuat As Integer = Integer.Parse(drd_rek("jml").ToString)
            Dim jmlpakai As Integer = Integer.Parse(drd_rek("jmlpakai").ToString)

            Dim selisih As Integer = jmlmuat - jmlpakai

            If tambah Then

                Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, selisih, kdbarang, tgudang.EditValue, True, False, False)
                If Not hasilplusmin.Equals("ok") Then
                    close_wait()
                    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                    hasil = "error"
                    Exit While
                End If

            Else

                '2. update barang
                Dim hasilplusmin_ed As String = PlusMin_Barang(cn, sqltrans, selisih, kdbarang, tgudang.EditValue, False, False, False)
                If Not hasilplusmin_ed.Equals("ok") Then
                    close_wait()
                    MsgBox(hasilplusmin_ed, vbOKOnly + vbExclamation, "Informasi")
                    hasil = "error"
                    Exit While
                End If

            End If

        End While
        drd_rek.Close()

        Return hasil

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
            Dim cmdrekap As OleDbCommand

            If addstat = True Then
                Dim bukti As String = cekbukti(cn, sqltrans)
                tbukti.EditValue = bukti

                Dim sqlins As String = String.Format("insert into trfaktur_balik (nobukti,tanggal,nobukti_rkp,note,tgl_masuk,kd_gudang) values('{0}','{1}','{2}','{3}','{4}','{5}')", tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), tnorekap.Text.Trim, tket.Text.Trim, convert_date_to_eng(ttgl_msk.EditValue), tgudang.EditValue)

                cmd = New OleDbCommand(sqlins, cn, sqltrans)
                cmd.ExecuteNonQuery()

                Dim sqlup_rekap As String = String.Format("update trrekap_to set spulang=1,tglkirim='{0}' where nobukti='{1}'", convert_date_to_eng(ttgl_kirim.EditValue), tnorekap.EditValue)
                cmdrekap = New OleDbCommand(sqlup_rekap, cn, sqltrans)
                cmdrekap.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btfakt_blk", 1, 0, 0, 0, tbukti.Text.Trim, tnorekap.Text.Trim, sqltrans)

            Else

                Dim sqlup As String = String.Format("update trfaktur_balik set tanggal='{0}',nobukti_rkp='{1}',note='{2}',tgl_masuk='{3}',kd_gudang='{4}' where nobukti='{5}'", convert_date_to_eng(ttgl.EditValue), tnorekap.Text.Trim, tket.Text.Trim, convert_date_to_eng(ttgl_msk.EditValue), tgudang.EditValue, tbukti.Text.Trim)

                cmd = New OleDbCommand(sqlup, cn, sqltrans)
                cmd.ExecuteNonQuery()

                Dim sqlup_rekap As String = String.Format("update trrekap_to set spulang=1,tglkirim='{0}' where nobukti='{1}'", convert_date_to_eng(ttgl_kirim.EditValue), tnorekap.EditValue)
                cmdrekap = New OleDbCommand(sqlup_rekap, cn, sqltrans)
                cmdrekap.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btfakt_blk", 0, 1, 0, 0, tbukti.Text.Trim, tnorekap.Text.Trim, sqltrans)

                If Not proses_stok(False, cn, sqltrans) = "ok" Then
                    sqltrans.Rollback()
                    close_wait()
                    Return
                End If

            End If
            

            If simpan2(cn, sqltrans).Equals("ok") Then

                '' cek antara jml rekap dengan realisasi
                Dim sqlcek_rkap As String = String.Format("select sum(jml) - sum(jmlpakai) as sisamuat from trrekap_to3 where nobukti='{0}'", tnorekap.Text.Trim)
                Dim cmdcek_rkap As OleDbCommand = New OleDbCommand(sqlcek_rkap, cn, sqltrans)
                Dim drd_rkap As OleDbDataReader = cmdcek_rkap.ExecuteReader

                Dim sisarekap As Integer = 0
                If drd_rkap.Read Then
                    If IsNumeric(drd_rkap(0).ToString) Then
                        sisarekap = drd_rkap(0).ToString
                    End If
                End If

                If sisarekap < 0 Then
                    sqltrans.Rollback()
                    close_wait()
                    MsgBox("Realisasi lebih dari jumlah muat, periksa kembali...", vbOKOnly + vbInformation, "Informasi")
                    Return
                End If

                If Not proses_stok(True, cn, sqltrans) = "ok" Then
                    sqltrans.Rollback()
                    close_wait()
                    Return
                End If


                If addstat = True Then
                    insertview()
                Else

                    If statverif = False Then
                        updateview()
                    End If

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

            Else
                close_wait()
            End If

lanjut_aja:

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

        Dim hasil As String = ""
        Dim cmd As OleDbCommand

        '' jika faktur kosong
        If stat_faktur_kosong = 1 Then

            If addstat = False Then

                Dim sqlcek As String = String.Format("select trrekap_to3.jml,trrekap_to3.noid,trrekap_to3.kd_barang,trrekap_to.kd_supir,trrekap_to.nopol from trrekap_to3 inner join trrekap_to on trrekap_to3.nobukti=trrekap_to.nobukti where trrekap_to.nobukti='{0}'", tnorekap.Text.Trim)
                Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn, sqltrans)
                Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

                Dim jmlold As Integer = 0
                Dim noid3 As Integer = 0
                Dim kdsupir As String = ""
                Dim nopol As String = ""

                Dim jmlold2 As Integer = 0
                Dim jmlold3 As Integer = 0

                If drdcek.Read Then
                    If IsNumeric(drdcek(0).ToString) Then
                        jmlold = Integer.Parse(drdcek(0).ToString)
                        noid3 = Integer.Parse(drdcek(1).ToString)
                        kdsupir = drdcek("kd_supir").ToString
                        nopol = drdcek("nopol").ToString

                        Dim sqlfb2 As String = String.Format("select SUM(qty_r) as qty from trfaktur_balik22 inner join trfaktur_balik on trfaktur_balik22.nobukti=trfaktur_balik.nobukti " & _
                            "where trfaktur_balik.nobukti_rkp='{0}' and trfaktur_balik22.kd_barang='{1}' and trfaktur_balik.nobukti='{2}'", tnorekap.Text.Trim, drdcek("kd_barang").ToString, tbukti.Text.Trim)
                        Dim cmdfb As OleDbCommand = New OleDbCommand(sqlfb2, cn, sqltrans)
                        Dim drdfb As OleDbDataReader = cmdfb.ExecuteReader

                        If drdfb.Read Then
                            If IsNumeric(drdfb(0).ToString) Then
                                jmlold2 = jmlold - Integer.Parse(drdfb(0).ToString)
                            End If
                        End If
                        drdfb.Close()

                        Dim sqlfb3 As String = String.Format("select SUM(qty_msk) as qty from trfaktur_balik23 inner join trfaktur_balik on trfaktur_balik23.nobukti= trfaktur_balik.nobukti " & _
                            "where trfaktur_balik.nobukti_rkp='{0}' and trfaktur_balik.nobukti='{1}'", tnorekap.Text.Trim, tbukti.Text.Trim)
                        Dim cmdfb3 As OleDbCommand = New OleDbCommand(sqlfb3, cn, sqltrans)
                        Dim drdfb3 As OleDbDataReader = cmdfb3.ExecuteReader

                        If drdfb3.Read Then
                            If IsNumeric(drdfb3(0).ToString) Then
                                jmlold3 = Integer.Parse(drdfb3(0).ToString)
                            End If
                        End If
                        drdfb3.Close()

                    End If
                End If
                drdcek.Close()

                If jmlold > 0 Then

                    '3. insert to hist stok
                    Clsmy.Insert_HistBarang(cn, sqltrans, tnorekap.Text.Trim, ttgl_msk.EditValue, tgudang.EditValue, "G0001", 0, jmlold2, "Faktur Balik/Realisasi TO-K (Edit)", kdsupir, nopol)
                    Clsmy.Insert_HistBarang(cn, sqltrans, tnorekap.Text.Trim, ttgl_msk.EditValue, tgudang.EditValue, "G0003", 0, jmlold3, "Faktur Balik/Realisasi TO-K (Edit)", kdsupir, nopol)

                    '2. update barang
                    Dim hasilplusmin2 As String = PlusMin_Barang(cn, sqltrans, jmlold2, "G0001", tgudang.EditValue, False, False, False)
                    If Not hasilplusmin2.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin2, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        GoTo langsung_keluar
                    End If

                    '2. update barang
                    Dim hasilplusmin3 As String = PlusMin_Barang(cn, sqltrans, jmlold3, "G0003", tgudang.EditValue, False, False, False)
                    If Not hasilplusmin3.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin3, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        GoTo langsung_keluar
                    End If

                End If


            End If

        End If
        '' akhir jika faktur kosong

        For i As Integer = 0 To dv1.Count - 1

            Dim adaedit As Boolean = False

            If Not DateValue(convert_date_to_eng(dv1(i)("tanggal").ToString)) = DateValue(convert_date_to_eng(ttgl_kirim.EditValue)) And dv1(i)("statkirim").ToString.Equals("TERKIRIM") Then

                Dim sqlcektop_toko As String = String.Format("select ms_toko.top_toko from trfaktur_to inner join ms_toko on trfaktur_to.kd_toko=ms_toko.kd_toko where trfaktur_to.nobukti='{0}'", dv1(i)("nobukti").ToString)
                Dim cmd_cektoko As OleDbCommand = New OleDbCommand(sqlcektop_toko, cn, sqltrans)
                Dim drd_cektoko As OleDbDataReader = cmd_cektoko.ExecuteReader

                Dim toptoko As Integer = 0
                If drd_cektoko.Read Then
                    If IsNumeric(drd_cektoko(0).ToString) Then
                        toptoko = drd_cektoko(0).ToString
                    End If
                End If
                drd_cektoko.Close()

                If toptoko = 0 Then
                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox("No bukti " & dv1(i)("nobukti").ToString & " top toko diisi dulu..", vbOKOnly + vbInformation, "Informasi")
                    hasil = "error"
                    GoTo langsung_keluar
                End If

                Dim tgltempo As String = DateAdd(DateInterval.Day, toptoko, ttgl_kirim.EditValue)

                Dim sqlupdatefaktur_tempo As String = String.Format("update trfaktur_to set tanggal='{0}',tgl_tempo='{1}' where nobukti='{2}'", convert_date_to_eng(ttgl_kirim.EditValue), convert_date_to_eng(tgltempo), dv1(i)("nobukti").ToString)
                Using cmdup_fakturtempo As OleDbCommand = New OleDbCommand(sqlupdatefaktur_tempo, cn, sqltrans)
                    cmdup_fakturtempo.ExecuteNonQuery()
                End Using

                dv1(i)("tanggal") = DateValue(ttgl_kirim.EditValue)

            End If
            ' akhir kalau update tidak sama tanggal

            If Not dv1(i)("statkirim").ToString.Equals("TERKIRIM") Then
                If cek_retur(cn, sqltrans, dv1(i)("nobukti").ToString) = True Then

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox("No bukti " & dv1(i)("nobukti").ToString & " ada barang retur,perbaiki dulu..", vbOKOnly + vbInformation, "Informasi")
                    hasil = "error"
                    GoTo langsung_keluar

                End If
            End If

            If cek_rekap(cn, sqltrans, dv1(i)("nobukti").ToString, tnorekap.Text.Trim) = False Then
                GoTo langsung_next
            Else

                Dim sqlclama As String = String.Format("select trfaktur_balik2.statkirim,trfaktur_balik2.spulang,trfaktur_balik2.nett_faktur,trfaktur_to.kd_toko from trfaktur_balik2 inner join trfaktur_to on trfaktur_balik2.nobukti_fak=trfaktur_to.nobukti where trfaktur_balik2.nobukti='{0}' and trfaktur_balik2.nobukti_fak='{1}'", tbukti.Text.Trim, dv1(i)("nobukti").ToString)
                Dim cmdclama As OleDbCommand = New OleDbCommand(sqlclama, cn, sqltrans)
                Dim drdclama As OleDbDataReader = cmdclama.ExecuteReader

                If drdclama.Read Then
                    If drdclama("statkirim").Equals("TERKIRIM") Then

                        Dim nettsebelum As Double = Double.Parse(drdclama("nett_faktur").ToString)
                        Dim kdtoko_old As String = drdclama("kd_toko").ToString

                        Dim sqluptoko As String = String.Format("update ms_toko set piutangbeli=piutangbeli - {0} where kd_toko='{1}'", Replace(nettsebelum, ",", "."), kdtoko_old)
                        Using cmduptoko As OleDbCommand = New OleDbCommand(sqluptoko, cn, sqltrans)
                            cmduptoko.ExecuteNonQuery()
                        End Using

                        Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, dv1(i)("nobukti").ToString, dv1(i)("nobukti").ToString, ttgl_msk.EditValue, kdtoko_old, 0, Replace(nettsebelum, ",", "."), "Jual Real (Edit)")

                    End If
                End If
                drdclama.Close()

                Dim sqldel As String = String.Format("delete from trfaktur_balik2 where nobukti='{0}' and nobukti_fak='{1}'", tbukti.Text.Trim, dv1(i)("nobukti").ToString)
                Using cmddel As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
                    cmddel.ExecuteNonQuery()
                End Using

                adaedit = True

            End If

            Dim sqlupd As String = String.Format("update trfaktur_to set spulang=0,statkirim='BELUM TERKIRIM' where nobukti='{0}'", dv1(i)("nobukti").ToString)
            Using cmdup_fak As OleDbCommand = New OleDbCommand(sqlupd, cn, sqltrans)
                cmdup_fak.ExecuteNonQuery()
            End Using

            Dim sqlupterkirim As String = String.Format("update trfaktur_to set statkirim='{0}',jnis_jual2='{1}' where nobukti='{2}'", dv1(i)("statkirim").ToString, dv1(i)("jnis_jual2").ToString, dv1(i)("nobukti").ToString)
            Using cmdup_kirim As OleDbCommand = New OleDbCommand(sqlupterkirim, cn, sqltrans)
                cmdup_kirim.ExecuteNonQuery()
            End Using

            If dv1(i)("ok").Equals(1) Then

                Dim sql_stoko As String = String.Format("select kd_toko from trfaktur_to where nobukti='{0}'", dv1(i)("nobukti").ToString)
                Dim cmd_tko As OleDbCommand = New OleDbCommand(sql_stoko, cn, sqltrans)
                Dim drd_tko As OleDbDataReader = cmd_tko.ExecuteReader

                If drd_tko.Read Then
                    If Not drd_tko(0).ToString.Equals("") Then

                        If dv1(i)("statkirim").ToString.Equals("TERKIRIM") Then

                            Dim sqluptoko2 As String = String.Format("update ms_toko set piutangbeli=piutangbeli + {0} where kd_toko='{1}'", Replace(dv1(i)("netto").ToString, ",", "."), drd_tko(0).ToString)

                            Using cmduptoko2 As OleDbCommand = New OleDbCommand(sqluptoko2, cn, sqltrans)
                                cmduptoko2.ExecuteNonQuery()
                            End Using

                            Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, dv1(i)("nobukti").ToString, dv1(i)("nobukti").ToString, ttgl_msk.EditValue, drd_tko(0).ToString, Replace(dv1(i)("netto").ToString, ",", "."), 0, IIf(adaedit = True, "Jual Real (Edit)", "Jual Real"))

                        End If
                    End If
                End If
                drd_tko.Close()

                Dim sqlupd2 As String = String.Format("update trfaktur_to set spulang=1 where nobukti='{0}'", dv1(i)("nobukti").ToString)
                Using cmdup_fak2 As OleDbCommand = New OleDbCommand(sqlupd2, cn, sqltrans)
                    cmdup_fak2.ExecuteNonQuery()
                End Using

                Dim smasuk_all As Boolean
                If dv1(i)("statkirim").ToString.Equals("BELUM TERKIRIM") Or dv1(i)("statkirim").ToString.Equals("BATAL") Then
                    smasuk_all = True
                Else
                    smasuk_all = False
                End If

                Dim nobukti As String = dv1(i)("nobukti").ToString
                Dim kdtoko As String = dv1(i)("kd_toko").ToString

                If simpan21(cn, sqltrans, nobukti, kdtoko, smasuk_all).Equals("ok") Then

                    If simpan22(cn, sqltrans, nobukti, kdtoko, smasuk_all).Equals("ok") Then

                        If smasuk_all = True Then

                            Dim sqlupfak As String = String.Format("update trfaktur_to set disc_per=disc_per0,disc_rp=disc_rp0,brutto=brutto0,netto=netto0 where nobukti='{0}'", dv1(i)("nobukti").ToString)
                            Using cmdupfak As OleDbCommand = New OleDbCommand(sqlupfak, cn, sqltrans)
                                cmdupfak.ExecuteNonQuery()
                            End Using

                        End If

                    Else
                        hasil = "error"
                        Exit For
                    End If

                Else
                    hasil = "error"
                    Exit For
                End If

                If Not simpan_retur(cn, sqltrans, nobukti).Equals("ok") Then
                    hasil = "error"
                    Exit For
                End If

            End If

            Dim nettbalik As Double = 0
            If dv1(i)("statkirim").ToString.Equals("TERKIRIM") Then
                nettbalik = Double.Parse(dv1(i)("netto").ToString)
            End If

            Dim sqlins As String = String.Format("insert into trfaktur_balik2 (nobukti,nobukti_fak,statkirim,spulang,nett_faktur) values('{0}','{1}','{2}',{3},{4})", tbukti.Text.Trim, dv1(i)("nobukti").ToString, dv1(i)("statkirim").ToString, dv1(i)("ok").ToString, Replace(nettbalik, ",", "."))
            cmd = New OleDbCommand(sqlins, cn, sqltrans)
            cmd.ExecuteNonQuery()

            Dim sqlup_rekap As String = String.Format("update trrekap_to2 set statkirim='{0}' where nobukti_fak='{1}' and nobukti='{2}'", dv1(i)("statkirim").ToString, dv1(i)("nobukti").ToString, tnorekap.Text.Trim)
            Using cmdup_rekap As OleDbCommand = New OleDbCommand(sqlup_rekap, cn, sqltrans)
                cmdup_rekap.ExecuteNonQuery()
            End Using

langsung_next:

        Next

        '' jika faktur kosong
        If stat_faktur_kosong = 1 And Not (hasil.Equals("error")) Then

            Dim sqlcek As String = String.Format("select trrekap_to3.jml,trrekap_to3.noid,trrekap_to3.kd_barang,trrekap_to.kd_supir,trrekap_to.nopol from trrekap_to3 inner join trrekap_to on trrekap_to3.nobukti=trrekap_to.nobukti where trrekap_to.nobukti='{0}'", tnorekap.Text.Trim)
            Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn, sqltrans)
            Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

            Dim jmlold As Integer = 0
            Dim noid3 As Integer = 0
            Dim kdsupir As String = ""
            Dim nopol As String = ""

            Dim jmlold2 As Integer = 0
            Dim jmlold3 As Integer = 0

            If drdcek.Read Then
                If IsNumeric(drdcek(0).ToString) Then
                    jmlold = Integer.Parse(drdcek(0).ToString)
                    noid3 = Integer.Parse(drdcek(1).ToString)
                    kdsupir = drdcek("kd_supir").ToString
                    nopol = drdcek("nopol").ToString

                    Dim sqlfb2 As String = String.Format("select SUM(qty_r) as qty from trfaktur_balik22 inner join trfaktur_balik on trfaktur_balik22.nobukti=trfaktur_balik.nobukti " & _
                        "where trfaktur_balik.nobukti_rkp='{0}' and trfaktur_balik22.kd_barang='{1}' and trfaktur_balik.nobukti='{2}'", tnorekap.Text.Trim, drdcek("kd_barang").ToString, tbukti.Text.Trim)
                    Dim cmdfb As OleDbCommand = New OleDbCommand(sqlfb2, cn, sqltrans)
                    Dim drdfb As OleDbDataReader = cmdfb.ExecuteReader

                    If drdfb.Read Then
                        If IsNumeric(drdfb(0).ToString) Then
                            jmlold2 = jmlold - Integer.Parse(drdfb(0).ToString)
                        End If
                    End If
                    drdfb.Close()

                    Dim sqlfb3 As String = String.Format("select SUM(qty_msk) as qty from trfaktur_balik23 inner join trfaktur_balik on trfaktur_balik23.nobukti= trfaktur_balik.nobukti " & _
                        "where trfaktur_balik.nobukti_rkp='{0}' and trfaktur_balik.nobukti='{1}'", tnorekap.Text.Trim, tbukti.Text.Trim)
                    Dim cmdfb3 As OleDbCommand = New OleDbCommand(sqlfb3, cn, sqltrans)
                    Dim drdfb3 As OleDbDataReader = cmdfb3.ExecuteReader

                    If drdfb3.Read Then
                        If IsNumeric(drdfb3(0).ToString) Then
                            jmlold3 = Integer.Parse(drdfb3(0).ToString)
                        End If
                    End If
                    drdfb3.Close()

                End If
            End If
            drdcek.Close()

            If jmlold > 0 Then

                Dim jdul As String
                If addstat Then
                    jdul = "Faktur Balik/Realisasi TO-K"
                Else
                    jdul = "Faktur Balik/Realisasi TO-K (Edit)"
                End If

                '3. insert to hist stok
                Clsmy.Insert_HistBarang(cn, sqltrans, tnorekap.Text.Trim, ttgl_msk.EditValue, tgudang.EditValue, "G0001", jmlold2, 0, jdul, kdsupir, nopol)
                Clsmy.Insert_HistBarang(cn, sqltrans, tnorekap.Text.Trim, ttgl_msk.EditValue, tgudang.EditValue, "G0003", jmlold3, 0, jdul, kdsupir, nopol)


                '2. update barang
                Dim hasilplusmin2 As String = PlusMin_Barang(cn, sqltrans, jmlold2, "G0001", tgudang.EditValue, True, False, False)
                If Not hasilplusmin2.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilplusmin2, vbOKOnly + vbExclamation, "Informasi")
                    hasil = "error"
                    GoTo langsung_keluar
                End If

                '2. update barang
                Dim hasilplusmin3 As String = PlusMin_Barang(cn, sqltrans, jmlold3, "G0003", tgudang.EditValue, True, False, False)
                If Not hasilplusmin3.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilplusmin3, vbOKOnly + vbExclamation, "Informasi")
                    hasil = "error"
                    GoTo langsung_keluar
                End If

            End If

            'Dim sqlcek_nonkirim As String = String.Format("select statkirim from trfaktur_balik2 where nobukti='{0}'", tbukti.Text.Trim)
            'Dim cmdcek_nonkirim As OleDbCommand = New OleDbCommand(sqlcek_nonkirim, cn, sqltrans)
            'Dim drdcek_nonkirim As OleDbDataReader = cmdcek_nonkirim.ExecuteReader

            'Dim jml_nonkirim As Integer = 0
            'Dim jml_allcek As Integer = 0
            'While drdcek_nonkirim.Read
            '    If drdcek_nonkirim(0).Equals("BELUM TERKIRIM") Then
            '        jml_nonkirim = jml_nonkirim + 1
            '    End If
            '    jml_allcek = jml_allcek + 1
            'End While
            'drdcek_nonkirim.Close()

            'If jml_nonkirim = jml_allcek Then

            '    Dim sqlback_tomuat As String = String.Format("update trrekap_to3 set jmlpakai = jml where nobukti='{0}'", tnorekap.Text.Trim)
            '    Using cmdback_tomuat As OleDbCommand = New OleDbCommand(sqlback_tomuat, cn, sqltrans)
            '        cmdback_tomuat.ExecuteNonQuery()
            '    End Using

            'End If

        End If
        '' akhir faktur kosong

langsung_keluar:

        If hasil.Equals("") Then
            hasil = "ok"
        End If

        Return hasil

    End Function

    Private Function cek_rekap(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal nofaktur As String, ByVal nobukti_rkap As String) As Boolean

        Dim cnstat As Boolean = False
        If Not IsNothing(cn) Then
            cnstat = True
        End If

        Dim hasil As Boolean = False

        Dim sql As String = String.Format("select max(trrekap_to2.nobukti) from trrekap_to2 inner join trrekap_to on trrekap_to.nobukti=trrekap_to2.nobukti where trrekap_to.sbatal=0 and trrekap_to2.nobukti_fak='{0}'", nofaktur)
        Dim cmd As OleDbCommand

        Try

            If Not IsNothing(cn) Then
                cmd = New OleDbCommand(sql, cn, sqltrans)
            Else
                cn = New OleDbConnection
                cn = Clsmy.open_conn
                cmd = New OleDbCommand(sql, cn)
            End If

            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Not drd(0).ToString.Equals("") Then
                    If drd(0).ToString.Equals(nobukti_rkap) Then
                        hasil = True
                    Else
                        hasil = False
                    End If
                End If
            End If
            drd.Read()

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If cnstat = False Then
                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If
            End If

        End Try

        Return hasil

    End Function

    Private Function cek_retur(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal nofaktur As String) As Boolean

        Dim hasil As Boolean = False

        Dim sqlret As String = String.Format("select nobukti from trfaktur_to5 where nobukti='{0}'", nofaktur)
        Dim cmdret As OleDbCommand = New OleDbCommand(sqlret, cn, sqltrans)
        Dim drdret As OleDbDataReader = cmdret.ExecuteReader

        If drdret.Read Then
            If Not drdret(0).ToString.Equals("") Then
                hasil = True
            End If
        End If
        drdret.Close()

        Return hasil

    End Function

    Private Function simpan21(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal nobukti As String, ByVal kdtoko As String, ByVal smasuk_all As Boolean) As String

        Dim hasil As String = ""

        Dim sqlc As String = String.Format("select trfaktur_to2.*,ms_barang.*,trrekap_to.kd_supir,trrekap_to.nopol from trfaktur_to2 inner join ms_barang on trfaktur_to2.kd_barang=ms_barang.kd_barang inner join trrekap_to2 on trfaktur_to2.nobukti=trrekap_to2.nobukti_fak " & _
            "inner join trrekap_to on trrekap_to2.nobukti=trrekap_to.nobukti where trfaktur_to2.nobukti='{0}' and trrekap_to.nobukti='{1}'", nobukti, tnorekap.Text.Trim)
        Dim cmds As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
        Dim drds As OleDbDataReader = cmds.ExecuteReader

        While drds.Read

            Dim jenis As String = drds("jenis").ToString
            Dim kdbar As String = drds("kd_barang").ToString
            Dim qty As String = drds("qty").ToString
            Dim satuan As String = drds("satuan").ToString
            Dim qtykecil As String = drds("qtykecil").ToString
            Dim noid As String = drds("noid").ToString

            Dim qty0 As String = drds("qty0").ToString
            Dim qtykecil0 As String = drds("qtykecil0").ToString

            Dim kdsup As String = drds("kd_supir").ToString
            Dim nopol As String = drds("nopol").ToString

            If qty0.Equals("") Then
                qty0 = 0
            End If

            If qtykecil0.Equals("") Then
                qtykecil0 = 0
            End If

            Dim qty1 As Integer = Integer.Parse(drds("qty1").ToString)
            Dim qty2 As Integer = Integer.Parse(drds("qty2").ToString)
            Dim qty3 As Integer = Integer.Parse(drds("qty3").ToString)

            Dim adarubah As Boolean = False
            Dim noidold As Integer

            If jenis = "FISIK" Then

                If addstat = True Then

                Else ' jika rubah

                    Dim sqlsel_old As String = String.Format("select trfaktur_balik22.*,trrekap_to.kd_supir,trrekap_to.nopol,trrekap_to2.statkirim from trfaktur_balik22 inner join trrekap_to2 on trfaktur_balik22.nobukti_fak=trrekap_to2.nobukti_fak " & _
                        "inner join trrekap_to on trrekap_to2.nobukti=trrekap_to.nobukti " & _
                        "where trfaktur_balik22.nobukti='{0}' and trfaktur_balik22.nobukti_fak='{1}' and trfaktur_balik22.kd_barang='{2}' and trrekap_to.nobukti='{3}'", tbukti.Text.Trim, nobukti, kdbar, tnorekap.Text.Trim)
                    Dim cmdsel_old As OleDbCommand = New OleDbCommand(sqlsel_old, cn, sqltrans)
                    Dim drsel_old As OleDbDataReader = cmdsel_old.ExecuteReader

                    If drsel_old.Read Then

                        Dim qtyold As Integer = Integer.Parse(drsel_old("qty").ToString)
                        Dim qtyold2 As Integer = Integer.Parse(drsel_old("qty2").ToString)
                        Dim qty_r As Integer = Integer.Parse(drsel_old("qty_r").ToString)
                        Dim statkirim As String = drsel_old("statkirim").ToString
                        noidold = Integer.Parse(drsel_old("noid").ToString)

                        If stat_faktur_kosong = 1 Then
                            GoTo langsung_update_rekap3
                        End If

                        Dim simpankosong_hh As String = simpankosong_f(cn, sqltrans, kdbar, kdtoko, qty1, qty2, qty3, qtyold, False)
                        If Not simpankosong_hh.Equals("ok") Then
                            close_wait()
                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox(simpankosong_hh)
                            hasil = "error"
                            GoTo lanjut_bawah

                        Else

                            If qtyold <> 0 Then
                                Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, ttgl_msk.EditValue, "None", kdbar, 0, qtyold, "Faktur Balik/Realisasi (Edit)", drsel_old("kd_supir").ToString, drsel_old("nopol").ToString)
                            End If

                        End If


                        If statkirim.Equals("TERKIRIM") Then

                            If qtyold <> 0 Then

                                '3. insert to hist stok
                                Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, ttgl_msk.EditValue, tgudang.EditValue, kdbar, 0, qtyold, "Faktur Balik/Realisasi (Edit)", drsel_old("kd_supir").ToString, drsel_old("nopol").ToString)

                            End If

                        Else

                            If qty_r <> 0 Then
                                '3. insert to hist stok
                                Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, ttgl_msk.EditValue, tgudang.EditValue, kdbar, 0, qty_r, "Faktur Balik/Realisasi (Edit)", drsel_old("kd_supir").ToString, drsel_old("nopol").ToString)
                            End If

                        End If

langsung_update_rekap3:

                        If statkirim.Equals("TERKIRIM") Then

                            Dim sqlrek As String = String.Format("update trrekap_to3 set jmlpakai = jmlpakai - {0} " & _
                            " where nobukti='{1}' and kd_barang='{2}'", qty_r, tnorekap.Text.Trim, kdbar)
                            Using cmdrek As OleDbCommand = New OleDbCommand(sqlrek, cn, sqltrans)
                                cmdrek.ExecuteNonQuery()
                            End Using

                        End If

                        adarubah = True

                    End If
                    drsel_old.Close()

                    End If

                    '' akhir update 

                    Dim selisih As Integer = 0
                    If smasuk_all = False Then

                        If qtykecil0 = 0 Then
                            selisih = qtykecil
                        ElseIf qtykecil = 0 Then
                            selisih = 0
                        Else
                            selisih = qtykecil0 - qtykecil
                        End If

                    Else
                        If stat_faktur_kosong = 0 Then
                            selisih = qtykecil0
                        End If
                    End If

                If smasuk_all = False Then

                    Dim sqlrek2 As String = String.Format("update trrekap_to3 set jmlpakai=jmlpakai+{0} where nobukti='{1}' and kd_barang='{2}'", qtykecil, tnorekap.Text.Trim, kdbar)
                    Using cmdrek2 As OleDbCommand = New OleDbCommand(sqlrek2, cn, sqltrans)
                        cmdrek2.ExecuteNonQuery()
                    End Using

                End If


                If adarubah = False Then

                    If smasuk_all Then

                        Dim sqlins22 As String = String.Format("insert into trfaktur_balik22 (nobukti,nobukti_fak,kd_barang,qty,qty2,qty_r) values('{0}','{1}','{2}',{3},{4},{5})", tbukti.Text.Trim, nobukti, kdbar, 0, 0, qtykecil0)
                        Using cmdiins22 As OleDbCommand = New OleDbCommand(sqlins22, cn, sqltrans)
                            cmdiins22.ExecuteNonQuery()
                        End Using

                    Else

                        Dim sqlins22 As String = String.Format("insert into trfaktur_balik22 (nobukti,nobukti_fak,kd_barang,qty,qty2,qty_r) values('{0}','{1}','{2}',{3},{4},{5})", tbukti.Text.Trim, nobukti, kdbar, selisih, selisih, qtykecil)
                        Using cmdiins22 As OleDbCommand = New OleDbCommand(sqlins22, cn, sqltrans)
                            cmdiins22.ExecuteNonQuery()
                        End Using

                    End If


                End If

                If stat_faktur_kosong = 1 Then
                    GoTo langsung_faktur_kosong_selisih
                End If

                If smasuk_all = False Then

                    Dim simpankosong_hh2 As String = simpankosong_f(cn, sqltrans, kdbar, kdtoko, qty1, qty2, qty3, selisih, True)
                    If Not simpankosong_hh2.Equals("ok") Then

                        close_wait()
                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(simpankosong_hh2)
                        hasil = "error"
                        GoTo lanjut_bawah
                    Else

                        '3. insert to hist stok
                        If addstat Then
                            If selisih <> 0 Then
                                Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, ttgl_msk.EditValue, "None", kdbar, selisih, 0, "Faktur Balik/Realisasi", kdsup, nopol)
                            End If
                        Else
                            If selisih <> 0 Then
                                Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, ttgl_msk.EditValue, "None", kdbar, selisih, 0, "Faktur Balik/Realisasi (Edit)", kdsup, nopol)
                            End If
                        End If

                    End If

                End If

                '3. insert to hist stok
                If addstat Then

                    If selisih <> 0 Then
                        Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, ttgl_msk.EditValue, tgudang.EditValue, kdbar, selisih, 0, "Faktur Balik/Realisasi", kdsup, nopol)
                    End If

                Else

                    If selisih <> 0 Then
                        Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, ttgl_msk.EditValue, tgudang.EditValue, kdbar, selisih, 0, "Faktur Balik/Realisasi (Edit)", kdsup, nopol)
                    End If

                End If

              

langsung_faktur_kosong_selisih:

                If smasuk_all = True Then

                    Dim sqlins22 As String = String.Format("update trfaktur_balik22 set qty={0},qty2=0,qty_r={1} where noid={2}", 0, qtykecil0, noidold)
                    Using cmdiins22 As OleDbCommand = New OleDbCommand(sqlins22, cn, sqltrans)
                        cmdiins22.ExecuteNonQuery()
                    End Using

                    ' balikin ke nilai awal
                    Dim sqlup2 As String = String.Format("update trfaktur_to2 set qty=qty0,harga=harga0,disc_per=disc_per0,disc_rp=disc_rp0,jumlah=jumlah0,qtykecil=qtykecil0 where noid={0}", noid)
                    Using cmdup2 As OleDbCommand = New OleDbCommand(sqlup2, cn, sqltrans)
                        cmdup2.ExecuteNonQuery()
                    End Using

                Else

                    Dim sqlins22 As String = String.Format("update trfaktur_balik22 set qty={0},qty2=0,qty_r={1} where noid={2}", selisih, qtykecil, noidold)
                    Using cmdiins22 As OleDbCommand = New OleDbCommand(sqlins22, cn, sqltrans)
                        cmdiins22.ExecuteNonQuery()
                    End Using

                End If

            Else ' jika bukan barang fisik

                If smasuk_all Then
                    ' balikin ke nilai awal
                    Dim sqlup2 As String = String.Format("update trfaktur_to2 set qty=qty0,harga=harga0,disc_per=disc_per0,disc_rp=disc_rp0,jumlah=jumlah0,qtykecil=qtykecil0 where noid={0}", noid)
                    Using cmdup2 As OleDbCommand = New OleDbCommand(sqlup2, cn, sqltrans)
                        cmdup2.ExecuteNonQuery()
                    End Using
                End If

            End If

        End While
        drds.Close()

        If hasil.Equals("") Then
            hasil = "ok"
        End If

lanjut_bawah:

        Return hasil

    End Function

    Private Function simpan22(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal nobukti As String, ByVal kdtoko As String, ByVal smasuk_all As Boolean) As String

        Dim hasil As String = ""

        Dim sqlc As String = String.Format("select trfaktur_to3.*,ms_barang.*,trrekap_to.kd_supir,trrekap_to.nopol from trfaktur_to3 inner join ms_barang on trfaktur_to3.kd_barang=ms_barang.kd_barang inner join trrekap_to2 on trfaktur_to3.nobukti=trrekap_to2.nobukti_fak " & _
        "inner join trrekap_to on trrekap_to2.nobukti=trrekap_to.nobukti where trfaktur_to3.nobukti='{0}' and trrekap_to.nobukti='{1}'", nobukti, tnorekap.Text.Trim)
        Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
        Dim drc As OleDbDataReader = cmdc.ExecuteReader

        While drc.Read

            Dim jenis As String = drc("jenis").ToString
            Dim kdbar As String = drc("kd_barang").ToString
            Dim qty As String = drc("qty").ToString
            Dim satuan As String = drc("satuan").ToString
            Dim qtykecil As String = drc("qtykecil").ToString
            Dim noid As String = drc("noid").ToString

            Dim kdsup As String = drc("kd_supir").ToString
            Dim nopol As String = drc("nopol").ToString

            Dim qty1 As Integer = Integer.Parse(drc("qty1").ToString)
            Dim qty2 As Integer = Integer.Parse(drc("qty2").ToString)
            Dim qty3 As Integer = Integer.Parse(drc("qty3").ToString)

            Dim adarubah As Boolean = False
            Dim noidold As Integer

            Dim qty2_bukti As Integer = cek_balik2_sebelumnya(cn, sqltrans, nobukti, kdbar)

            If addstat = True Then

            Else

                Dim jdulmess As String = ""
                If stat_faktur_kosong = 1 Then
                    jdulmess = "Faktur Balik/Realisasi TO-K (Edit)"
                Else
                    jdulmess = "Faktur Balik/Realisasi (Edit)"
                End If

                Dim sqlsel_old As String = String.Format("select trfaktur_balik23.*,trrekap_to.kd_supir,trrekap_to.nopol,trrekap_to2.statkirim from trfaktur_balik23 inner join trrekap_to2 on trfaktur_balik23.nobukti_fak=trrekap_to2.nobukti_fak " & _
                    "inner join trrekap_to on trrekap_to2.nobukti=trrekap_to.nobukti where trfaktur_balik23.nobukti='{0}' and trfaktur_balik23.nobukti_fak='{1}' and trfaktur_balik23.kd_barang='{2}' and trrekap_to.nobukti='{3}'", tbukti.Text.Trim, nobukti, kdbar, tnorekap.Text.Trim)
                Dim cmdsel_old As OleDbCommand = New OleDbCommand(sqlsel_old, cn, sqltrans)
                Dim drsel_old As OleDbDataReader = cmdsel_old.ExecuteReader

                If drsel_old.Read Then

                    Dim qtyold As Integer = Integer.Parse(drsel_old("qty").ToString)
                    Dim qtyold_pinj As Integer = Integer.Parse(drsel_old("qty_pinj").ToString)
                    Dim qtyold_msk As Integer = Integer.Parse(drsel_old("qty_msk").ToString)
                    noidold = Integer.Parse(drsel_old("noid").ToString)

                    If qtyold <> 0 Then
                        Dim hsilsimkos As String = simpankosong(cn, sqltrans, kdbar, kdtoko, qty1, qty2, qty3, qtyold, False)
                        If Not hsilsimkos.Equals("ok") Then

                            close_wait()
                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox(hsilsimkos, vbOKOnly + vbExclamation, "Informasi")
                            hasil = "error"
                            Exit While
                        End If
                    End If
                    

                    If qtyold_pinj <> 0 Then
                        Dim hsilpinjm As String = Hist_PinjamSewa_Toko(kdtoko, kdbar, qtyold_pinj, cn, sqltrans, False, True)
                        If Not hsilpinjm.Equals("ok") Then
                            close_wait()

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox(hsilpinjm, vbOKOnly + vbExclamation, "Informasi")
                            hasil = "error"
                            Exit While
                        End If
                    End If
                    

                    If stat_faktur_kosong = 0 Then

                        If qtyold_msk > 0 Then

                            '2. update barang
                            Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtyold_msk, kdbar, tgudang.EditValue, False, False, False)
                            If Not hasilplusmin.Equals("ok") Then
                                close_wait()

                                If Not IsNothing(sqltrans) Then
                                    sqltrans.Rollback()
                                End If

                                MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                                hasil = "error"
                                Exit While
                            End If

                        End If

                        If qtyold_msk <> 0 Then
                            Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, ttgl_msk.EditValue, tgudang.EditValue, kdbar, 0, qtyold_msk, jdulmess, drsel_old("kd_supir").ToString, drsel_old("nopol").ToString)
                        End If

                    End If
                    

                    If drsel_old("statkirim").ToString.Equals("TERKIRIM") Then

                        '3. insert to hist stok
                        If qtyold_msk > 0 And qtyold <> 0 Then

                            If qtyold_msk > 0 Then
                                Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, ttgl_msk.EditValue, "None B", kdbar, qtyold_msk, 0, jdulmess, drsel_old("kd_supir").ToString, drsel_old("nopol").ToString)
                            End If

                        End If


                        If qtyold <> 0 Then

                            qtyold = qtyold + qtyold_msk

                            Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, ttgl_msk.EditValue, "None B", kdbar, 0, qtyold, jdulmess, drsel_old("kd_supir").ToString, drsel_old("nopol").ToString)
                        End If


                        If qtyold_pinj <> 0 Then
                            Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, ttgl_msk.EditValue, "None P", kdbar, 0, qtyold_pinj, jdulmess, drsel_old("kd_supir").ToString, drsel_old("nopol").ToString)
                        End If

                    End If

                    adarubah = True

            End If
            drsel_old.Close()

            End If

            Dim judl As String
            If adarubah = False Then

                If stat_faktur_kosong = 1 Then
                    judl = "Faktur Balik/Realisasi TO-K"
                Else
                    judl = "Faktur Balik/Realisasi"
                End If

            Else

                If stat_faktur_kosong = 1 Then
                    judl = "Faktur Balik/Realisasi TO-K (Edit)"
                Else
                    judl = "Faktur Balik/Realisasi (Edit)"
                End If

            End If


            Dim selisih As Integer = 0
            Dim jmljual As Integer = cek_balik2_bli_sbelumnya(cn, sqltrans, nobukti, kdbar)
            Dim jmlpinjam As Integer = cek_balik2_pinj_sbelumnya(cn, sqltrans, nobukti, kdbar)

            If smasuk_all = False Then

                selisih = jmljual - jmlpinjam - qtykecil

                'If selisih > 0 Then

                '    Dim sqlsisa As String = String.Format("select jml from ms_toko4 where kd_toko='{0}' and kd_barang='{1}'", kdtoko, kdbar)
                '    Dim cmdsisa As OleDbCommand = New OleDbCommand(sqlsisa, cn, sqltrans)
                '    Dim drsisa As OleDbDataReader = cmdsisa.ExecuteReader

                '    Dim jmsisa As Integer = 0
                '    If drsisa.Read Then
                '        If IsNumeric(drsisa(0).ToString) Then
                '            jmsisa = drsisa(0).ToString
                '        End If
                '    End If
                '    drsisa.Close()

                '    If jmsisa = 0 Then

                '        If Not IsNothing(sqltrans) Then
                '            sqltrans.Rollback()
                '        End If

                '        MsgBox("Jml gallon kosong 0", vbOKOnly + vbInformation, "Informasi")
                '        hasil = "error"
                '        GoTo lanjut_bawah
                '    ElseIf jmsisa < (selisih) Then

                '        If Not IsNothing(sqltrans) Then
                '            sqltrans.Rollback()
                '        End If

                '        MsgBox("Jml gallon kosong melebihi sisa ditoko", vbOKOnly + vbInformation, "Informasi")
                '        hasil = "error"
                '        GoTo lanjut_bawah
                '    Else
                '        'selisih = qty2_bukti - qtykecil
                '    End If

                'End If

            Else

                If qtykecil > 0 Then

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox("Sudah ada barang kosong kembali pada faktur " & nobukti, vbOKOnly + vbInformation, "Informasi")
                    hasil = "error"
                    GoTo lanjut_bawah
                End If

                selisih = qty2_bukti
            End If

            If smasuk_all = False Then

                Dim hasilsimpankosong As String = simpankosong(cn, sqltrans, kdbar, kdtoko, qty1, qty2, qty3, selisih, True)

                If Not hasilsimpankosong.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilsimpankosong, vbOKOnly + vbExclamation, "Informasi")
                    hasil = "error"
                    Exit While
                End If

                '3. insert to hist stok
                If jmljual - jmlpinjam <> 0 Then
                    Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, ttgl_msk.EditValue, "None B", kdbar, jmljual - jmlpinjam, 0, judl, kdsup, nopol)
                End If

                If qtykecil <> 0 Then
                    Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, ttgl_msk.EditValue, "None B", kdbar, 0, qtykecil, judl, kdsup, nopol)
                End If

                If jmlpinjam <> 0 Then

                    Dim hsilpinjm As String = Hist_PinjamSewa_Toko(kdtoko, kdbar, jmlpinjam, cn, sqltrans, True, True)
                    If Not hsilpinjm.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hsilpinjm, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        Exit While
                    End If

                    Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, ttgl_msk.EditValue, "None P", kdbar, jmlpinjam, 0, judl, kdsup, nopol)
                End If

            End If

            If stat_faktur_kosong = 0 Then

                If qtykecil > 0 Then

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, tgudang.EditValue, True, False, False)
                    If Not hasilplusmin.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        Exit While
                    End If

                End If

                If qtykecil <> 0 Then
                    '3. insert to hist stok
                    Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, ttgl_msk.EditValue, tgudang.EditValue, kdbar, qtykecil, 0, judl, kdsup, nopol)
                End If

            End If


            If smasuk_all = True Then
                selisih = 0
            End If

            If adarubah = False Then

                Dim sqlins22 As String = String.Format("insert into trfaktur_balik23 (nobukti,nobukti_fak,kd_barang,qty,qty_pinj,qty_msk) values('{0}','{1}','{2}',{3},{4},{5})", tbukti.Text.Trim, nobukti, kdbar, selisih, jmlpinjam, qtykecil)
                Using cmdiins22 As OleDbCommand = New OleDbCommand(sqlins22, cn, sqltrans)
                    cmdiins22.ExecuteNonQuery()
                End Using
            Else

                Dim sqlins22 As String = String.Format("update trfaktur_balik23 set qty={0},qty_msk={1},qty_pinj={2} where noid={3}", selisih, qtykecil, jmlpinjam, noidold)
                Using cmdiins22 As OleDbCommand = New OleDbCommand(sqlins22, cn, sqltrans)
                    cmdiins22.ExecuteNonQuery()
                End Using

            End If

            'balikin ke awal
            If smasuk_all = True Then

                Dim sqlup3 As String = String.Format("update trfaktur_to3 set qty=0,qtykecil=0,jumlah=0 where noid={0}", noid)
                Using cmdup3 As OleDbCommand = New OleDbCommand(sqlup3, cn, sqltrans)
                    cmdup3.ExecuteNonQuery()
                End Using

            End If

        End While
        drc.Close()


        If hasil.Equals("") Then
            hasil = "ok"
        End If

lanjut_bawah:

        Return hasil

    End Function

    Private Function cek_balik2_sebelumnya(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal nobukti As String, ByVal kdbar As String) As Integer

        Dim hasil As Integer = 0

        Dim sql As String = String.Format("select trfaktur_to2.noid,trfaktur_to2.qtykecil from trfaktur_to2 inner join ms_barang " & _
        "on trfaktur_to2.kd_barang=ms_barang.kd_barang " & _
        "where trfaktur_to2.nobukti='{0}' and ms_barang.kd_barang_kmb='{1}'", nobukti, kdbar)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drc As OleDbDataReader = cmd.ExecuteReader

        If drc.Read Then
            If IsNumeric(drc("noid").ToString) Then
                hasil = Integer.Parse(drc("qtykecil").ToString)
            End If
        End If
        drc.Close()

        Return hasil

    End Function

    Private Function cek_balik2_bli_sbelumnya(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal nobukti As String, ByVal kdbar As String) As Integer

        Dim hasil As Integer = 0

        Dim sql As String = String.Format("select trfaktur_to2.qtykecil from trfaktur_to2 inner join ms_barang " & _
        "on trfaktur_to2.kd_barang=ms_barang.kd_barang " & _
        "where trfaktur_to2.jenis_trans='JUAL' and trfaktur_to2.nobukti='{0}' and trfaktur_to2.kd_barang in (select kd_barang_jmn from ms_barang where kd_barang_kmb='{1}')", nobukti, kdbar)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drc As OleDbDataReader = cmd.ExecuteReader

        If drc.Read Then
            If IsNumeric(drc(0).ToString) Then
                hasil = Integer.Parse(drc(0).ToString)
            End If
        End If
        drc.Close()

        Return hasil

    End Function

    Private Function cek_balik2_pinj_sbelumnya(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal nobukti As String, ByVal kdbar As String) As Integer

        Dim hasil As Integer = 0

        Dim sql As String = String.Format("select trfaktur_to2.noid,trfaktur_to2.qtykecil from trfaktur_to2 inner join ms_barang " & _
        "on trfaktur_to2.kd_barang=ms_barang.kd_barang " & _
        "where trfaktur_to2.jenis_trans='PINJAM' and trfaktur_to2.nobukti='{0}' and trfaktur_to2.kd_barang in (select kd_barang_jmn from ms_barang where kd_barang_kmb='{1}')", nobukti, kdbar)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drc As OleDbDataReader = cmd.ExecuteReader

        If drc.Read Then
            If IsNumeric(drc("noid").ToString) Then
                hasil = Integer.Parse(drc("qtykecil").ToString)
            End If
        End If
        drc.Close()

        Return hasil

    End Function


    Private Function simpan_retur(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal nobukti As String) As String

        '  Dim cmd As OleDbCommand
        Dim hasil As String = ""

        Dim jjenis As String
        If addstat Then
            jjenis = "Faktur Balik/Realisasi-Ret"
        Else
            jjenis = "Faktur Balik/Realisasi-Ret (Edit)"
        End If

        'Dim noidsimpan As Integer = 0
        If addstat = False Then

            Dim sqlsebelum As String = String.Format("SELECT trfaktur_balik24.qty_masuk, trfaktur_balik24.noid, trfaktur_balik24.kd_barang, ms_barang.qty1, ms_barang.qty2, ms_barang.qty3, trfaktur_to.kd_toko, " & _
            "trfaktur_balik24.nobukti_fak, trrekap_to.kd_supir, trrekap_to.nopol, trfaktur_balik24.kd_gudang " & _
            "FROM trrekap_to INNER JOIN " & _
            "trfaktur_balik ON trrekap_to.nobukti = trfaktur_balik.nobukti_rkp INNER JOIN " & _
            "trfaktur_balik24 INNER JOIN " & _
            "ms_barang ON trfaktur_balik24.kd_barang = ms_barang.kd_barang INNER JOIN " & _
            "trfaktur_to ON trfaktur_balik24.nobukti_fak = trfaktur_to.nobukti ON trfaktur_balik.nobukti = trfaktur_balik24.nobukti " & _
            "WHERE trfaktur_to.nobukti='{0}' and trrekap_to.nobukti='{1}'", nobukti, tnorekap.Text.Trim)

            Dim cmdseb As OleDbCommand = New OleDbCommand(sqlsebelum, cn, sqltrans)
            Dim drdseb As OleDbDataReader = cmdseb.ExecuteReader

            While drdseb.Read
                If Integer.Parse(drdseb(0).ToString) > 0 Then

                    Dim qtysebelumnya As Integer = drdseb("qty_masuk").ToString
                    Dim kdbar_sebelum As String = drdseb("kd_barang").ToString
                    Dim qty1_sebelum As Integer = Integer.Parse(drdseb("qty1").ToString)
                    Dim qty2_sebelum As Integer = Integer.Parse(drdseb("qty2").ToString)
                    Dim qty3_sebelum As Integer = Integer.Parse(drdseb("qty3").ToString)

                    Dim kdtoko_sebelum As String = drdseb("kd_toko").ToString
                    Dim kdgudang_sebelum As String = drdseb("kd_gudang").ToString
                    Dim nobuktifak_sebelum As String = drdseb("nobukti_fak").ToString

                    Dim kd_supir_sebelum As String = drdseb("kd_supir").ToString
                    Dim nopol_sebelum As String = drdseb("nopol").ToString

                    Dim hsilsimkos2 As String = simpankosong(cn, sqltrans, kdbar_sebelum, kdtoko_sebelum, qty1_sebelum, qty2_sebelum, qty3_sebelum, qtysebelumnya, True)
                    If Not hsilsimkos2.Equals("ok") Then
                        close_wait()
                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hsilsimkos2, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        GoTo langsung_keluar
                    End If

                    Dim hasilplusmin_seb As String = PlusMin_Barang(cn, sqltrans, qtysebelumnya, kdbar_sebelum, kdgudang_sebelum, False, False, False)
                    If Not hasilplusmin_seb.Equals("ok") Then

                        close_wait()
                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin_seb, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        GoTo langsung_keluar
                    End If

                    If qtysebelumnya > 0 Then
                        '3. insert to hist stok
                        Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, ttgl_msk.EditValue, kdgudang_sebelum, kdbar_sebelum, 0, qtysebelumnya, jjenis, kd_supir_sebelum, nopol_sebelum)
                    End If
                   

                    If apakah_brg_kembali(cn, sqltrans, kdbar_sebelum) Then

                        If qtysebelumnya > 0 Then
                            Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, ttgl_msk.EditValue, "None", kdbar_sebelum, qtysebelumnya, 0, jjenis, kd_supir_sebelum, nopol_sebelum)
                        End If

                    End If

                    Dim sqlup_balik2 As String = String.Format("update trfaktur_balik24 set qty_masuk={0} where noid={1}", 0, drdseb("noid").ToString)
                    Using cmdup_balik2 As OleDbCommand = New OleDbCommand(sqlup_balik2, cn, sqltrans)
                        cmdup_balik2.ExecuteNonQuery()
                    End Using

                End If
            End While
            drdseb.Close()

        End If

        Dim sql As String = String.Format("SELECT trrekap_to.kd_supir,trrekap_to.nopol,trfaktur_to.kd_toko,trfaktur_to5.kd_gudang, trfaktur_to5.kd_barang, trfaktur_to5.qty, trfaktur_to5.satuan, trfaktur_to5.harga, " & _
        "trfaktur_to5.disc_per, trfaktur_to5.disc_rp, trfaktur_to5.jumlah, " & _
        "trfaktur_to5.qtykecil, trfaktur_to5.noid, ms_barang.qty1, ms_barang.qty2, ms_barang.qty3 " & _
        "FROM trfaktur_to5 INNER JOIN trfaktur_to on trfaktur_to5.nobukti=trfaktur_to.nobukti INNER JOIN " & _
        "ms_barang ON trfaktur_to5.kd_barang = ms_barang.kd_barang INNER JOIN " & _
        "trrekap_to2 ON trfaktur_to5.nobukti = trrekap_to2.nobukti_fak " & _
        "INNER JOIN trrekap_to on trrekap_to2.nobukti=trrekap_to.nobukti " & _
        "WHERE trfaktur_to5.nobukti = '{0}' and trrekap_to2.nobukti='{1}'", nobukti, tnorekap.Text.Trim)

        Dim cmdret As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drdred As OleDbDataReader = cmdret.ExecuteReader

        While drdred.Read

            Dim kdgud As String = drdred("kd_gudang").ToString
            Dim kdbar As String = drdred("kd_barang").ToString
            Dim qty As String = drdred("qty").ToString
            Dim satuan As String = drdred("satuan").ToString
            Dim harga As String = drdred("harga").ToString
            Dim disc_per As String = drdred("disc_per").ToString
            Dim disc_rp As String = drdred("disc_rp").ToString
            Dim jumlah As String = drdred("jumlah").ToString
            Dim qtykecil As String = drdred("qtykecil").ToString
            Dim noid As String = drdred("noid").ToString
            Dim qty1 As String = drdred("qty1").ToString
            Dim qty2 As String = drdred("qty2").ToString
            Dim qty3 As String = drdred("qty3").ToString

            Dim kdtoko As String = drdred("kd_toko").ToString

            Dim kdsupir As String = drdred("kd_supir").ToString
            Dim nopol As String = drdred("nopol").ToString


            Dim hsilsimkos As String = simpankosong(cn, sqltrans, kdbar, kdtoko, qty1, qty2, qty3, qtykecil, False)
            If Not hsilsimkos.Equals("ok") Then
                close_wait()

                If Not IsNothing(sqltrans) Then
                    sqltrans.Rollback()
                End If

                MsgBox(hsilsimkos, vbOKOnly + vbExclamation, "Informasi")
                hasil = "error"
                Exit While
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
                Exit While
            End If

            If qtykecil <> 0 Then
                '3. insert to hist stok
                Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, ttgl_msk.EditValue, kdgud, kdbar, qtykecil, 0, jjenis, kdsupir, nopol)
            End If
            

            If apakah_brg_kembali(cn, sqltrans, kdbar) Then
                If qtykecil <> 0 Then
                    Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, ttgl_msk.EditValue, "None", kdbar, 0, qtykecil, jjenis, kdsupir, nopol)
                End If
            End If

            Dim sql24 As String = String.Format("select noid from trfaktur_balik24 where nobukti='{0}' and nobukti_fak='{1}' and kd_barang='{2}'", tbukti.Text.Trim, nobukti, kdbar)
            Dim cmd24 As OleDbCommand = New OleDbCommand(sql24, cn, sqltrans)
            Dim drd24 As OleDbDataReader = cmd24.ExecuteReader

            Dim noidsimpan As Integer = 0
            If drd24.Read Then
                If Integer.Parse(drd24(0).ToString) > 0 Then
                    noidsimpan = drd24(0).ToString
                End If
            End If
            drd24.Close()

            If noidsimpan = 0 Then

                Dim sqlin_balik As String = String.Format("insert into trfaktur_balik24 (nobukti,nobukti_fak,kd_barang,qty_masuk,kd_gudang) values('{0}','{1}','{2}',{3},'{4}')", _
                                        tbukti.Text.Trim, nobukti, kdbar, qtykecil, kdgud)
                Using cmdin_balik As OleDbCommand = New OleDbCommand(sqlin_balik, cn, sqltrans)
                    cmdin_balik.ExecuteNonQuery()
                End Using

            Else

                Dim sqlup_balik As String = String.Format("update trfaktur_balik24 set qty_masuk={0},kd_gudang='{1}' where noid={2}", qtykecil, kdgud, noidsimpan)
                Using cmdup_balik As OleDbCommand = New OleDbCommand(sqlup_balik, cn, sqltrans)
                    cmdup_balik.ExecuteNonQuery()
                End Using

            End If

        End While

        If hasil.Equals("") Then
            hasil = "ok"
        End If

langsung_keluar:

        Return hasil

    End Function

    Private Sub cek_onRekap3()

        Dim sql As String = String.Format("select noid from trrekap_to3 where nobukti='{0}'", tnorekap.Text.Trim)

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader


            Dim apakahada As Boolean = False
            If drd.Read Then
                If IsNumeric(drd(0).ToString) Then
                    apakahada = True
                End If
            End If
            drd.Close()

            If apakahada = False Then

                Dim sqltrans As OleDbTransaction = cn.BeginTransaction

                Dim sql2 As String = String.Format("select trrekap_to2.nobukti,trfaktur_to.kd_toko, trfaktur_to2.kd_barang, trfaktur_to2.qtykecil,trfaktur_to2.kd_gudang  " & _
                "from trrekap_to2 inner join trfaktur_to2 on trrekap_to2.nobukti_fak=trfaktur_to2.nobukti " & _
                "inner join trfaktur_to on trfaktur_to.nobukti=trfaktur_to2.nobukti " & _
                "inner join ms_barang on trfaktur_to2.kd_barang=ms_barang.kd_barang " & _
                "where ms_barang.jenis='FISIK' and trrekap_to2.nobukti='{0}'", tnorekap.Text.Trim)

                Dim cmd2 As OleDbCommand = New OleDbCommand(sql2, cn, sqltrans)
                Dim drd2 As OleDbDataReader = cmd2.ExecuteReader

                While drd2.Read

                    Dim qtykecil As Integer = Integer.Parse(drd2("qtykecil").ToString)
                    Dim kdbar As String = drd2("kd_barang").ToString

                    Dim kdgud As String = drd2("kd_gudang").ToString

                    Dim apakahkosong As Boolean = False

                    'If qtykecil = 0 Then
                    '    Dim sqltok As String = String.Format("select spred_to from ms_toko where kd_toko='{0}'", drd2("kd_toko").ToString)
                    '    Dim cmdtok As OleDbCommand = New OleDbCommand(sqltok, cn, sqltrans)
                    '    Dim drtok As OleDbDataReader = cmdtok.ExecuteReader

                    '    Dim spread_to As Integer = 0

                    '    If drtok.Read Then
                    '        spread_to = drtok(0).ToString
                    '    End If
                    '    drtok.Close()

                    '    If spread_to = 0 Then

                    '        '    close_wait()
                    '        MsgBox("Jumlah jual tidak boleh kosong", vbOKOnly + vbExclamation, "Informasi")
                    '        '   hasil = "error"

                    '        tnorekap.Text = ""
                    '        Exit While

                    '    End If

                    '    apakahkosong = True
                    '    qtykecil = 200
                    '    kdbar = "G0001"
                    '    kdgud = "G000"

                    'End If

                    '' masukkan ke rekap 3 yaitu total barangnya
                    Dim sqlcek3 As String = String.Format("select noid,jml from trrekap_to3 where nobukti='{0}' and kd_barang='{1}'", tbukti.Text.Trim, kdbar)
                    Dim cmdcek3 As OleDbCommand = New OleDbCommand(sqlcek3, cn, sqltrans)
                    Dim drdcek3 As OleDbDataReader = cmdcek3.ExecuteReader

                    Dim noidrek3 As Integer = 0
                    Dim jmlrek3 As Integer = 0

                    If drdcek3.Read Then
                        If IsNumeric(drdcek3(0).ToString) Then
                            noidrek3 = Integer.Parse(drdcek3(0).ToString)
                            jmlrek3 = Integer.Parse(drdcek3(1).ToString)
                        End If
                    End If
                    drdcek3.Close()

                    If noidrek3 = 0 Then
                        Dim sqlins3 As String = String.Format("insert into trrekap_to3 (nobukti,kd_barang,jml) values('{0}','{1}',{2})", tnorekap.Text.Trim, kdbar, qtykecil)
                        Using cmdtok3 As OleDbCommand = New OleDbCommand(sqlins3, cn, sqltrans)
                            cmdtok3.ExecuteNonQuery()
                        End Using
                    Else

                        jmlrek3 = jmlrek3 + qtykecil

                        If jmlrek3 < 200 Then
                            Dim sqlup3 As String = String.Format("update trrekap_to3 set jml=jml+{0} where noid='{1}'", qtykecil, noidrek3)
                            Using cmdtok3 As OleDbCommand = New OleDbCommand(sqlup3, cn, sqltrans)
                                cmdtok3.ExecuteNonQuery()
                            End Using
                        End If

                    End If

                End While


                sqltrans.Commit()

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

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("nobukti") = tbukti.Text.Trim
        orow("tanggal") = ttgl.Text.Trim
        orow("tgl_masuk") = ttgl_msk.Text.Trim
        orow("nobukti_rkp") = tnorekap.Text.Trim
        orow("tglkirim") = ttgl_kirim.Text.Trim
        orow("nopol") = tnopol.Text.Trim
        orow("nama_supir") = tsupir.Text.Trim
        orow("nama_jalur") = tjalur.Text.Trim
        orow("note") = tket.Text.Trim
        orow("kd_gudang") = tgudang.EditValue
        orow("sfaktur_kosong") = stat_faktur_kosong
        orow("sbatal") = 0
        orow("sverif") = 0

        dv.EndInit()

    End Sub

    Private Sub updateview()
        dv(position)("tanggal") = ttgl.Text.Trim
        dv(position)("nobukti_rkp") = tnorekap.Text.Trim
        dv(position)("tglkirim") = ttgl_kirim.Text.Trim
        dv(position)("tgl_masuk") = ttgl_msk.Text.Trim
        dv(position)("nopol") = tnopol.Text.Trim
        dv(position)("nama_supir") = tsupir.Text.Trim
        dv(position)("nama_jalur") = tjalur.Text.Trim
        dv(position)("note") = tket.Text.Trim
        dv(position)("kd_gudang") = tgudang.EditValue
        dv(position)("sfaktur_kosong") = stat_faktur_kosong
    End Sub

    Private Sub bts_rekap_Click(sender As System.Object, e As System.EventArgs) Handles bts_rekap.Click

        Dim fs As New fsrekap With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tnorekap.EditValue = fs.get_Nobukti

        cek_onRekap3()

        If tnorekap.Text.Trim.Length = 0 Then
            tnopol.Text = ""
            tsupir.Text = ""
            ttgl_kirim.Text = ""
            tjalur.Text = ""
            stat_faktur_kosong = 0
            Return
        End If

        tnopol.EditValue = fs.get_Nopol
        tsupir.EditValue = fs.get_supir
        ttgl_kirim.EditValue = convert_date_to_ind(fs.get_tglkirim)
        tjalur.EditValue = fs.get_jalur

        stat_faktur_kosong = fs.get_stat_fak_kosong

        opengrid()

        XtraTabControl1.SelectedTabPageIndex = 0

    End Sub

    Private Sub tnorekap_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tnorekap.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_rekap_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tnorekap_LostFocus(sender As Object, e As System.EventArgs) Handles tnorekap.LostFocus
        If tnorekap.Text.Trim.Length = 0 Then
            tnorekap.Text = ""
            tnopol.Text = ""
            tsupir.Text = ""
            ttgl_kirim.Text = ""
            tjalur.Text = ""
        End If
    End Sub

    Private Sub tnorekap_Validated(sender As Object, e As System.EventArgs) Handles tnorekap.Validated

        If tnorekap.Text.Trim.Length > 0 Then

            cek_onRekap3()

            If tnorekap.Text.Trim.Length = 0 Then
                tnopol.Text = ""
                tsupir.Text = ""
                ttgl_kirim.Text = ""
                tjalur.Text = ""
                stat_faktur_kosong = 0
                Return
            End If

            Dim cn As OleDbConnection = Nothing
            'Dim sql As String = String.Format("SELECT     trrekap_to.nobukti, trrekap_to.tgl, trrekap_to.tglkirim, ms_pegawai.nama_karyawan AS nama_supir, ms_jalur_kirim.nama_jalur, trrekap_to.nopol " & _
            '    "FROM         trrekap_to INNER JOIN " & _
            '    "ms_pegawai ON trrekap_to.kd_supir = ms_pegawai.kd_karyawan INNER JOIN " & _
            '    "ms_jalur_kirim ON trrekap_to.kd_jalur = ms_jalur_kirim.kd_jalur where trrekap_to.sbatal=0 and trrekap_to.smuat=1 and trrekap_to.spulang=0 and trrekap_to.nobukti='{0}'", tnorekap.Text.Trim)

            Dim sql As String = String.Format("SELECT     trrekap_to.nobukti, trrekap_to.tgl, trrekap_to.tglkirim, ms_pegawai.nama_karyawan AS nama_supir, ms_jalur_kirim.nama_jalur, trrekap_to.nopol,trrekap_to.sfaktur_kosong " & _
                "FROM         trrekap_to LEFT JOIN " & _
                "ms_pegawai ON trrekap_to.kd_supir = ms_pegawai.kd_karyawan INNER JOIN " & _
                "ms_jalur_kirim ON trrekap_to.kd_jalur = ms_jalur_kirim.kd_jalur where trrekap_to.sbatal=0 and trrekap_to.spulang=0 and trrekap_to.nobukti='{0}'", tnorekap.Text.Trim)


            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tnorekap.EditValue = dread("nobukti").ToString
                        ttgl_kirim.EditValue = DateValue(dread("tglkirim").ToString)
                        tsupir.EditValue = dread("nama_supir").ToString
                        tjalur.EditValue = dread("nama_jalur").ToString
                        tnopol.EditValue = dread("nopol").ToString

                        stat_faktur_kosong = Integer.Parse(dread("sfaktur_kosong").ToString)

                        '        opengrid()

                    Else
                        tnopol.Text = ""
                        tsupir.Text = ""
                        ttgl_kirim.Text = ""
                        tjalur.Text = ""
                        stat_faktur_kosong = 0
                    End If
                Else
                    tnopol.Text = ""
                    tsupir.Text = ""
                    ttgl_kirim.Text = ""
                    tjalur.Text = ""
                    stat_faktur_kosong = 0
                End If

                opengrid()
                XtraTabControl1.SelectedTabPageIndex = 0

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

    Private Sub ComboBoxEdit1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBoxEdit1.SelectedIndexChanged

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        For i As Integer = 0 To dv1.Count - 1
            If ComboBoxEdit1.SelectedIndex = 1 Then
                dv1(i)("ok") = 1
            Else
                dv1(i)("ok") = 0
            End If
        Next

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tnorekap.Text.Trim.Length = 0 Then
            MsgBox("No rekap tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
            tnorekap.Focus()
            Return
        End If

        If IsNothing(dv1) Then
            MsgBox("Tidak ada faktur yang akan diproses..", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If dv1.Count <= 0 Then
            MsgBox("Tidak ada faktur yang akan diproses..", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        'If dv2.Count > 0 Then

        '    If tgudang.EditValue = "" Then
        '        MsgBox("Pilih dahulu gudang masuk...", vbOKOnly + vbInformation, "Informasi")
        '        XtraTabControl1.SelectedTabPageIndex = 1
        '        tgudang.Focus()
        '        Return
        '    End If

        'End If

        If MsgBox("Yakin semua data sudah benar ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.No Then
            Return
        End If

        simpan()

    End Sub

    Private Sub ffaktur_b2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub ffaktur_b2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ComboBoxEdit1.SelectedIndex = 0
        ttgl.EditValue = Date.Now
        ttgl_msk.EditValue = Date.Now

        isi_gudang()

        XtraTabControl1.SelectedTabPageIndex = 0

        If addstat = True Then
            kosongkan()
        Else

            tnorekap.Enabled = False
            bts_rekap.Enabled = False
            tgudang.Enabled = False

            isi()


        End If

        If statverif_realisasi = True Then
            btedit.Enabled = False
            ttgl.Enabled = False
            tnorekap.Enabled = False
            bts_rekap.Enabled = False

        End If

    End Sub

    Private Sub XtraTabControl1_Selected(sender As System.Object, e As DevExpress.XtraTab.TabPageEventArgs) Handles XtraTabControl1.Selected
        If e.PageIndex = 1 Then
            '  opengrid2()
        End If
    End Sub

    Private Function kalkulasi(ByVal jmlqt As Integer, ByVal vqty1 As Integer, ByVal vqty2 As Integer, ByVal vqty3 As Integer, _
                       ByVal satuan1 As String, ByVal satuan2 As String, ByVal satuan3 As String, ByVal satuanawal As String) As Integer

        Dim kqty As Integer

        If jmlqt = 0 Then
            kqty = 0
            GoTo akhir
        End If

        Dim jml As String = jmlqt
        Dim jml1 As Integer
        If Not jml.Equals("") Then
            jml1 = Integer.Parse(jml)
        Else
            jml1 = 0
        End If

        Dim xjumlah As Double = jml1

        If satuanawal.Equals(satuan1) Then
            kqty = (vqty1 * vqty2 * vqty3) * jml1
        ElseIf satuanawal.Equals(satuan2) Then
            kqty = (vqty2 * vqty3) * jml
        ElseIf satuanawal.Equals(satuan3) Then
            kqty = vqty3 * jml1
        End If

akhir:

        Return kqty

    End Function

    Private Sub btedit_Click(sender As System.Object, e As System.EventArgs) Handles btedit.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        Dim sql As String = String.Format("select sbatal,(jmlbayar+jmlgiro+jmlgiro_real) as jml from trfaktur_to where nobukti='{0}'", dv1(Me.BindingContext(dv1).Position)("nobukti"))

        Dim cn As OleDbConnection = Nothing

        Dim ada As Boolean = False

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd(0).ToString) Then

                    If Integer.Parse(drd(0).ToString) = 1 Then
                        MsgBox("Faktur telah dibatalkan,tidak bisa dirubah...", vbOKOnly + vbInformation, "Informasi")
                        ada = True
                    End If

                    If Double.Parse(drd(1).ToString) > 0 Then
                        MsgBox("Faktur telah dibayar,tidak bisa dirubah...", vbOKOnly + vbInformation, "Informasi")
                        ada = True
                    End If

                End If
            End If

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Exit Sub
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

        If ada = True Then
            Exit Sub
        End If

        If cek_rekap(Nothing, Nothing, dv1(Me.BindingContext(dv1).Position)("nobukti").ToString, tnorekap.Text.Trim) = False Then
            MsgBox("Faktur telah dimuat dengan no rekap selanjutnya, tidak dapat dirubah..", vbOKOnly + vbInformation, "Infromasi")
            Exit Sub
        End If

        Dim sbalik As Boolean
        If dv1(Me.BindingContext(dv1).Position)("sbalik").Equals("0") Then
            sbalik = False
        Else
            sbalik = True
        End If

        Dim crbayarfak As String = dv1(Me.BindingContext(dv1).Position)("jnis_jual2")

        Using fkar2 As New ffaktur_to2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = Me.BindingContext(dv1).Position, .jenisjual = "T", .spulang = True, .tglkembali = ttgl_msk.EditValue, .statsebelumnya = sbalik, .crbayar_fak = crbayarfak}

            fkar2.SplitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both

            If statview Then
                fkar2.btedit.Enabled = False
                fkar2.SimpleButton1.Enabled = False
                fkar2.btsimpan.Enabled = False
            End If

            fkar2.btadd.Enabled = False
            fkar2.btdel.Enabled = False

            fkar2.ShowDialog()

            If fkar2.get_statbalik = True Then

                dv1(Me.BindingContext(dv1).Position)("sbalik") = 1
                dv1(Me.BindingContext(dv1).Position)("ok") = 1
                '    dv1(Me.BindingContext(dv1).Position)("statkirim") = "TERKIRIM"
                dv1(Me.BindingContext(dv1).Position)("jnis_jual2") = fkar2.get_crbayar
                dv1(Me.BindingContext(dv1).Position)("tanggal") = fkar2.get_tanggal
                dv1(Me.BindingContext(dv1).Position)("nama_toko") = fkar2.get_namatoko
                dv1(Me.BindingContext(dv1).Position)("alamat_toko") = fkar2.get_alamattoko
                dv1(Me.BindingContext(dv1).Position)("netto") = fkar2.get_netto

            Else
                dv1(Me.BindingContext(dv1).Position)("sbalik") = 0
            End If

        End Using

    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If e.Column.FieldName.Equals("ok") Then

            If e.Value.ToString.Equals("0") Then
                dv1(Me.BindingContext(dv1).Position)("statkirim") = "BELUM TERKIRIM"
                dv1(Me.BindingContext(dv1).Position)("jnis_jual2") = "KREDIT"
            Else
                dv1(Me.BindingContext(dv1).Position)("statkirim") = "TERKIRIM"
            End If

        End If

    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As System.EventArgs) Handles GridView1.DoubleClick

        If btedit.Enabled = False Then
            Return
        End If

        btedit_Click(sender, Nothing)

        'If cek_rekap(Nothing, Nothing, dv1(Me.BindingContext(dv1).Position)("nobukti").ToString, tnorekap.Text.Trim) = False Then
        '    btedit_Click(sender, Nothing)
        'Else
        '    MsgBox("Nota telah masuk ke no rekap yang lain, tidak dapat dirubah...", vbOKOnly + vbExclamation, "Informasi")
        'End If

    End Sub

End Class