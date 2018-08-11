Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class ffaktur_b

    Private bs1 As BindingSource
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private statadd As Boolean = False

    Private Sub opendata()

        Dim sql As String = String.Format("SELECT     trfaktur_balik.nobukti, trfaktur_balik.tanggal, trfaktur_balik.tgl_masuk, trfaktur_balik.nobukti_rkp, trrekap_to.tglkirim, trrekap_to.nopol, " & _
                      "ms_pegawai.nama_karyawan AS nama_supir, ms_jalur_kirim.nama_jalur, trfaktur_balik.note, trfaktur_balik.sbatal, trfaktur_balik.kd_gudang,trrekap_to.sfaktur_kosong,trfaktur_balik.sverif " & _
                    "FROM         trfaktur_balik INNER JOIN " & _
                      "trrekap_to ON trfaktur_balik.nobukti_rkp = trrekap_to.nobukti LEFT OUTER JOIN " & _
                      "ms_pegawai ON trrekap_to.kd_supir = ms_pegawai.kd_karyawan LEFT OUTER JOIN " & _
                      "ms_jalur_kirim ON trrekap_to.kd_jalur = ms_jalur_kirim.kd_jalur where trfaktur_balik.tanggal >='{0}' and  trfaktur_balik.tanggal <='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))

        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing

        Try

            open_wait()

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet
            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            bs1 = New BindingSource() With {.DataSource = dv1}
            bn1.BindingSource = bs1

            grid1.DataSource = bs1

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

            If dv1.Count > 0 Then
                bs1.MoveLast()
                opendata2()
            Else
                opendata2()
            End If

        End Try

    End Sub

    Private Sub opendata2()

        grid2.DataSource = Nothing
        dv2 = Nothing

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        If statadd = True Then
            Return
        End If

        Dim sql As String = String.Format("SELECT   1 as ok,'TERKIRIM'  as statkirim,trfaktur_to.alasan_batal, trrekap_to2.nobukti_fak as nobukti, trfaktur_to.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko,  trfaktur_to.tanggal " & _
            "FROM         trrekap_to2 INNER JOIN " & _
            "trfaktur_to ON trrekap_to2.nobukti_fak = trfaktur_to.nobukti INNER JOIN " & _
            "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko where trrekap_to2.nobukti='{0}'", dv1(bs1.Position)("nobukti_rkp").ToString)

        Dim cn As OleDbConnection = Nothing



        Try

            '  open_wait()



            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet
            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager2 = New DataViewManager(ds)
            dv2 = dvmanager2.CreateDataView(ds.Tables(0))

            grid2.DataSource = dv2

            cekon_fakturbalik(cn)

            '   close_wait()


        Catch ex As OleDb.OleDbException
            '  close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub cekon_fakturbalik(ByVal cn As OleDbConnection)

        Dim cmd As OleDbCommand
        Dim drd As OleDbDataReader

        For i As Integer = 0 To dv2.Count - 1

            Dim sql As String = String.Format("select nobukti_fak,statkirim,spulang from trfaktur_balik2 where nobukti='{0}' and nobukti_fak='{1}'", dv1(bs1.Position)("nobukti").ToString, dv2(i)("nobukti").ToString)
            cmd = New OleDbCommand(sql, cn)
            drd = cmd.ExecuteReader

            If drd.Read Then

                If Not drd("statkirim").ToString.Equals("") Then
                    If Integer.Parse(drd("spulang").ToString) = 1 Then
                        dv2(i)("ok") = 1
                    Else
                        dv2(i)("ok") = 0
                    End If

                    dv2(i)("statkirim") = drd("statkirim").ToString

                Else
                    dv2(i)("ok") = 0
                    dv2(i)("statkirim") = "BELUM TERKIRIM"
                End If

            End If

            drd.Close()

        Next

    End Sub

    Private Sub cari()

        'bs1.DataSource = Nothing
        grid1.DataSource = Nothing
        Dim cn As OleDbConnection = Nothing

        Dim sql As String = ""
        Dim scbo As Integer = tcbofind.SelectedIndex

        sql = String.Format("SELECT     trfaktur_balik.nobukti, trfaktur_balik.tanggal, trfaktur_balik.tgl_masuk, trfaktur_balik.nobukti_rkp, trrekap_to.tglkirim, trrekap_to.nopol, " & _
                      "ms_pegawai.nama_karyawan AS nama_supir, ms_jalur_kirim.nama_jalur, trfaktur_balik.note, trfaktur_balik.sbatal, trfaktur_balik.kd_gudang,trrekap_to.sfaktur_kosong,trfaktur_balik.sverif " & _
                    "FROM         trfaktur_balik INNER JOIN " & _
                      "trrekap_to ON trfaktur_balik.nobukti_rkp = trrekap_to.nobukti LEFT OUTER JOIN " & _
                      "ms_pegawai ON trrekap_to.kd_supir = ms_pegawai.kd_karyawan LEFT OUTER JOIN " & _
                      "ms_jalur_kirim ON trrekap_to.kd_jalur = ms_jalur_kirim.kd_jalur where trfaktur_balik.tanggal >='{0}' and  trfaktur_balik.tanggal <='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))

        Select Case tcbofind.SelectedIndex
            Case 0 ' nobukti
                sql = String.Format("{0} and trfaktur_balik.nobukti like '%{1}%'", sql, tfind.Text.Trim)
            Case 1 ' tanggal

                If Not IsDate(tfind.Text.Trim) Then
                    MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                    Return
                Else

                    If tfind.Text.Trim.Length < 10 Or tfind.Text.Trim.Length > 10 Then
                        MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                        Return
                    End If

                End If

                sql = String.Format("{0} and trfaktur_balik.tanggal='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))
            Case 2 ' no rekap
                sql = String.Format("{0} and trfaktur_balik.nobukti_rkp like '%{1}%'", sql, tfind.Text.Trim)
            Case 3 ' tgl kirim

                If Not IsDate(tfind.Text.Trim) Then
                    MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                    Return
                Else

                    If tfind.Text.Trim.Length < 10 Or tfind.Text.Trim.Length > 10 Then
                        MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                        Return
                    End If

                End If

                sql = String.Format("{0} and trrekap_to.tglkirim='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))
            Case 4 ' no polisi
                sql = String.Format("{0} and trrekap_to.nopol like '%{1}%'", sql, tfind.Text.Trim)
            Case 5 ' supir
                sql = String.Format("{0} and ms_pegawai.nama_karyawan like '%{1}%'", sql, tfind.Text.Trim)
            Case 6 ' jalur pengiriman
                sql = String.Format("{0} and ms_jalur_kirim.nama_jalur like '%{1}%'", sql, tfind.Text.Trim)
            Case 7 ' no faktur
                sql = String.Format("{0} and trfaktur_balik.nobukti in (select nobukti from trfaktur_balik2 where nobukti_fak='{1}')", sql, tfind.Text.Trim)
            Case 8 ' nama toko
                sql = String.Format("{0} and trfaktur_balik.nobukti in (select trfaktur_balik2.nobukti from trfaktur_balik2 inner join trfaktur_to on trfaktur_balik2.nobukti_fak=trfaktur_to.nobukti " & _
                    "inner join ms_toko on trfaktur_to.kd_toko=ms_toko.kd_toko where ms_toko.nama_toko like '%{1}%')", sql, tfind.Text.Trim)
        End Select

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            bs1.DataSource = dv1
            bn1.BindingSource = bs1

            grid1.DataSource = bs1

            close_wait()

        Catch ex As Exception
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

    Private Function proses_stok(ByVal tambah As Boolean, ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim hasil As String = "ok"

        Dim kdgudang As String = dv1(Me.BindingContext(bs1).Position)("kd_gudang").ToString
        Dim nobukti_rekap As String = dv1(Me.BindingContext(bs1).Position)("nobukti_rkp").ToString

        Dim sql As String = String.Format("select * from trrekap_to3 where nobukti='{0}'", nobukti_rekap)
        Dim cmd_rek As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd_rek As OleDbDataReader = cmd_rek.ExecuteReader

        While drd_rek.Read

            Dim kdbarang As String = drd_rek("kd_barang").ToString
            Dim jmlmuat As Integer = Integer.Parse(drd_rek("jml").ToString)
            Dim jmlpakai As Integer = Integer.Parse(drd_rek("jmlpakai").ToString)

            Dim selisih As Integer = jmlmuat - jmlpakai

            If tambah Then

                Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, selisih, kdbarang, kdgudang, True, False, False)
                If Not hasilplusmin.Equals("ok") Then
                    close_wait()
                    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                    hasil = "error"
                    Exit While
                End If

            Else

                '2. update barang
                Dim hasilplusmin_ed As String = PlusMin_Barang(cn, sqltrans, selisih, kdbarang, kdgudang, False, False, False)
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

    Private Sub hapus()

        Dim alasan = ""
        Using falasanb As New fkonfirm_hapus With {.WindowState = FormWindowState.Normal, .StartPosition = FormStartPosition.CenterScreen}
            falasanb.ShowDialog()
            alasan = falasanb.get_alasan
        End Using

        If alasan = "" Then
            Return
        End If

        Dim sql As String = String.Format("update trfaktur_balik set sbatal=1,alasan_batal='{0}' where nobukti='{1}'", alasan.ToUpper, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString)
        Dim sqlrekap As String = String.Format("update trrekap_to set spulang=0 where nobukti='{0}'", dv1(Me.BindingContext(bs1).Position)("nobukti_rkp").ToString)

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand = Nothing
        Dim cmdtoko As OleDbCommand = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            comd = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            cmdtoko = New OleDbCommand(sqlrekap, cn, sqltrans)
            cmdtoko.ExecuteNonQuery()

            If Not proses_stok(False, cn, sqltrans) = "ok" Then
                sqltrans.Rollback()
                close_wait()
                Return
            End If

            If hapusdetail(cn, sqltrans).Equals("ok") Then

                Clsmy.InsertToLog(cn, "btfakt_blk", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString, dv1(Me.BindingContext(bs1).Position)("nobukti_rkp").ToString, sqltrans)

                sqltrans.Commit()

                dv1(bs1.Position)("sbatal") = 1

                close_wait()

                MsgBox("Data telah dibatalkan...", vbOKOnly + vbInformation, "Informasi")

            Else
                close_wait()
            End If

lansung_aja:

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

    Private Function hapusdetail(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim hasil As String = ""

        Dim nobukti As String = dv1(Me.BindingContext(bs1).Position)("nobukti").ToString
        Dim tglmasuk As String = dv1(Me.BindingContext(bs1).Position)("tgl_masuk").ToString
        Dim kdgudang As String = dv1(Me.BindingContext(bs1).Position)("kd_gudang").ToString
        Dim nobukti_rekap As String = dv1(Me.BindingContext(bs1).Position)("nobukti_rkp").ToString

        '' jika faktur kosong
        If Integer.Parse(dv1(Me.BindingContext(bs1).Position)("sfaktur_kosong").ToString) = 1 Then

            Dim sqlcek As String = String.Format("select trrekap_to3.jml,trrekap_to3.noid,trrekap_to3.kd_barang,trrekap_to.kd_supir,trrekap_to.nopol from trrekap_to3 inner join trrekap_to on trrekap_to3.nobukti=trrekap_to.nobukti where trrekap_to.nobukti='{0}'", nobukti_rekap)
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
                        "where trfaktur_balik.nobukti_rkp='{0}' and trfaktur_balik22.kd_barang='{1}' and trfaktur_balik.nobukti='{2}'", nobukti_rekap, drdcek("kd_barang").ToString, nobukti)
                    Dim cmdfb As OleDbCommand = New OleDbCommand(sqlfb2, cn, sqltrans)
                    Dim drdfb As OleDbDataReader = cmdfb.ExecuteReader

                    If drdfb.Read Then
                        If IsNumeric(drdfb(0).ToString) Then
                            jmlold2 = jmlold - Integer.Parse(drdfb(0).ToString)
                        End If
                    End If
                    drdfb.Close()

                    Dim sqlfb3 As String = String.Format("select SUM(qty_msk) as qty from trfaktur_balik23 inner join trfaktur_balik on trfaktur_balik23.nobukti= trfaktur_balik.nobukti " & _
                        "where trfaktur_balik.nobukti_rkp='{0}' and trfaktur_balik.nobukti='{1}'", nobukti_rekap, nobukti)
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
                Clsmy.Insert_HistBarang(cn, sqltrans, nobukti_rekap, tglmasuk, kdgudang, "G0001", 0, jmlold2, "Faktur Balik/Realisasi TO-K (Batal)", kdsupir, nopol)
                Clsmy.Insert_HistBarang(cn, sqltrans, nobukti_rekap, tglmasuk, kdgudang, "G0003", 0, jmlold3, "Faktur Balik/Realisasi TO-K (Batal)", kdsupir, nopol)

                '2. update barang
                Dim hasilplusmin2 As String = PlusMin_Barang(cn, sqltrans, jmlold2, "G0001", kdgudang, False, False, False)
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
                Dim hasilplusmin3 As String = PlusMin_Barang(cn, sqltrans, jmlold3, "G0003", kdgudang, False, False, False)
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
        '' akhir jika faktur kosong

        Dim sql As String = String.Format("select * from trfaktur_balik2 inner join trfaktur_to on trfaktur_balik2.nobukti_fak=trfaktur_to.nobukti where trfaktur_balik2.nobukti='{0}'", nobukti)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        While drd.Read

            Dim kdtoko As String = drd("kd_toko").ToString
            Dim nobukti_fak As String = drd("nobukti_fak").ToString
            Dim statkirim As String = drd("statkirim").ToString
            Dim sbatal As Integer = Integer.Parse(drd("sbatal").ToString)
            Dim nettbalik As Double = Double.Parse(drd("nett_faktur").ToString)

            Dim jmlbayar As Double = Double.Parse(drd("jmlbayar")) + Double.Parse(drd("jmlgiro")) + Double.Parse(drd("jmlgiro_real"))

            If cek_retur(cn, sqltrans, nobukti_fak) = True Then

                If Not IsNothing(sqltrans) Then
                    sqltrans.Rollback()
                End If

                MsgBox(String.Format("No bukti {0} ada barang retur,perbaiki dulu..", nobukti_fak), vbOKOnly + vbInformation, "Informasi")
                hasil = "error"
                GoTo langsung_keluar

            End If

            If statkirim.Equals("TERKIRIM") Then

                If jmlbayar <> 0 Then
                    MsgBox(String.Format("No bukti {0} sudah dibayar", nobukti_fak), vbOKOnly + vbInformation, "Informasi")
                    hasil = "error"
                    GoTo langsung_keluar
                End If

                Dim sqluptoko As String = String.Format("update ms_toko set piutangbeli=piutangbeli - {0} where kd_toko='{1}'", Replace(nettbalik, ",", "."), kdtoko)
                Using cmduptoko As OleDbCommand = New OleDbCommand(sqluptoko, cn, sqltrans)
                    cmduptoko.ExecuteNonQuery()
                End Using

                Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, nobukti_fak, nobukti_fak, tglmasuk, kdtoko, 0, Replace(nettbalik, ",", "."), "Jual Real (Batal)")

            End If

            If simpan21(cn, sqltrans, nobukti_fak, nobukti, tglmasuk, kdtoko, kdgudang, nobukti_rekap, statkirim).Equals("ok") Then

                If simpan22(cn, sqltrans, nobukti_fak, nobukti, tglmasuk, kdtoko, kdgudang, nobukti_rekap).Equals("ok") Then
                Else
                    hasil = "error"
                    Exit While
                End If

            Else
                hasil = "error"
                Exit While
            End If

            If Not simpan_retur(cn, sqltrans, nobukti_fak, nobukti_rekap).Equals("ok") Then
                hasil = "error"
                Exit While
            End If

            Dim sqlup_rekap As String = String.Format("update trrekap_to2 set statkirim='BELUM TERKIRIM' where nobukti_fak='{0}' and nobukti='{1}'", nobukti_fak, nobukti_rekap)
            Using cmdup_rekap As OleDbCommand = New OleDbCommand(sqlup_rekap, cn, sqltrans)
                cmdup_rekap.ExecuteNonQuery()
            End Using

langsung_update:

            Dim sqlupfak As String = String.Format("update trfaktur_to set disc_per=disc_per0,disc_rp=disc_rp0,brutto=brutto0,netto=netto0,jmlkelebihan=0,jmlkembali=0 where nobukti='{0}'", nobukti_fak)
            Using cmdupfak As OleDbCommand = New OleDbCommand(sqlupfak, cn, sqltrans)
                cmdupfak.ExecuteNonQuery()
            End Using


            Dim sql1 As String = String.Format("update trfaktur_to set spulang=0,statkirim='BELUM TERKIRIM' where nobukti='{0}'", drd("nobukti_fak").ToString)
            Using cmd1 As OleDbCommand = New OleDbCommand(sql1, cn, sqltrans)
                cmd1.ExecuteNonQuery()
            End Using

        End While
        drd.Close()

langsung_keluar:

        If hasil.Equals("") Then
            hasil = "ok"
        End If

        Return hasil

    End Function

    Private Function simpan21(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal nobukti_fak As String, ByVal nobukti As String, _
                              ByVal tanggal As String, ByVal kdtoko As String, ByVal gudang As String, ByVal nobuktirekap As String, ByVal statkirim As String) As String

        Dim hasil As String = ""

        Dim sqlc As String = String.Format("select trfaktur_to2.*,trrekap_to.kd_supir,trrekap_to.nopol,ms_barang.jenis,ms_barang.qty1,ms_barang.qty2,ms_barang.qty3 from trfaktur_to2 inner join trrekap_to2 on trfaktur_to2.nobukti=trrekap_to2.nobukti_fak " & _
            "inner join trrekap_to on trrekap_to2.nobukti=trrekap_to.nobukti inner join ms_barang on trfaktur_to2.kd_barang=ms_barang.kd_barang where trrekap_to.nobukti='{0}' and  trfaktur_to2.nobukti='{1}'", nobuktirekap, nobukti_fak)
        Dim cmds As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
        Dim drds As OleDbDataReader = cmds.ExecuteReader

        While drds.Read

            Dim jenis As String = drds("jenis").ToString
            Dim kdbar As String = drds("kd_barang").ToString
            Dim qty As String = drds("qty").ToString
            Dim satuan As String = drds("satuan").ToString
            Dim qtykecil As String = drds("qtykecil").ToString
            Dim noid As String = drds("noid").ToString
            Dim kdsup As String = drds("kd_supir").ToString
            Dim nopol As String = drds("nopol").ToString

            Dim qty0 As String = drds("qty0").ToString
            Dim qtykecil0 As String = drds("qtykecil0").ToString

            If qty0.Equals("") Then
                qty0 = 0
            End If

            If qtykecil0.Equals("") Then
                qtykecil0 = 0
            End If

            Dim qty1 As Integer = Integer.Parse(drds("qty1").ToString)
            Dim qty2 As Integer = Integer.Parse(drds("qty2").ToString)
            Dim qty3 As Integer = Integer.Parse(drds("qty3").ToString)

            Dim noidold As Integer

            If jenis = "FISIK" Then

                Dim sqlsel_old As String = String.Format("select trfaktur_balik22.*,trrekap_to.kd_supir,trrekap_to.nopol,trrekap_to2.statkirim from trfaktur_balik22 inner join trrekap_to2 on trfaktur_balik22.nobukti_fak=trrekap_to2.nobukti_fak " & _
                    "inner join trrekap_to on trrekap_to2.nobukti=trrekap_to.nobukti where trfaktur_balik22.nobukti='{0}' and trfaktur_balik22.nobukti_fak='{1}' and trfaktur_balik22.kd_barang='{2}' and trrekap_to.nobukti='{3}'", nobukti, nobukti_fak, kdbar, nobuktirekap)
                Dim cmdsel_old As OleDbCommand = New OleDbCommand(sqlsel_old, cn, sqltrans)
                Dim drsel_old As OleDbDataReader = cmdsel_old.ExecuteReader


                Dim qty_r As Integer = 0

                If drsel_old.Read Then

                    Dim qtyold As Integer = Integer.Parse(drsel_old("qty").ToString)
                    qty_r = Integer.Parse(drsel_old("qty_r").ToString)
                    Dim stkirim As String = drsel_old("statkirim").ToString
                    noidold = Integer.Parse(drsel_old("noid").ToString)

                    If Integer.Parse(dv1(Me.BindingContext(bs1).Position)("sfaktur_kosong").ToString) = 1 Then
                        GoTo langsung_update_rekap3
                    End If

                    Dim simpankosong_hh As String = simpankosong_f(cn, sqltrans, kdbar, kdtoko, qty1, qty2, qty3, qtyold, False)
                    If Not simpankosong_hh.Equals("ok") Then

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If
                        MsgBox(simpankosong_hh)
                        hasil = "error"
                        GoTo lanjut_bawah
                    Else
                        If qtyold <> 0 Then
                            Clsmy.Insert_HistBarang(cn, sqltrans, nobukti_fak, tanggal, "None", kdbar, 0, qtyold, "Faktur Balik/Realisasi (Batal)", drsel_old("kd_supir").ToString, drsel_old("nopol").ToString)
                        End If

                    End If

                    If stkirim.Equals("TERKIRIM") Then
                        If qtyold <> 0 Then
                            '3. insert to hist stok
                            Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tanggal, gudang, kdbar, 0, qtyold, "Faktur Balik/Realisasi (Batal)", kdsup, nopol)
                        End If
                    Else
                        If qty_r <> 0 Then
                            '3. insert to hist stok
                            Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tanggal, gudang, kdbar, 0, qty_r, "Faktur Balik/Realisasi (Batal)", kdsup, nopol)
                        End If
                        
                    End If

langsung_update_rekap3:

                    If stkirim.Equals("TERKIRIM") Then

                        Dim sqlrek As String = String.Format("update trrekap_to3 set jmlpakai = jmlpakai - {0} " & _
                        " where nobukti='{1}' and kd_barang='{2}'", qty_r, dv1(bs1.Position)("nobukti_rkp").ToString, kdbar)
                        Using cmdrek As OleDbCommand = New OleDbCommand(sqlrek, cn, sqltrans)
                            cmdrek.ExecuteNonQuery()
                        End Using

                    End If

                End If
                drsel_old.Close()

                ' balikin ke nilai awal
                Dim sqlup2 As String = String.Format("update trfaktur_to2 set qty=qty0,harga=harga0,disc_per=disc_per0,disc_rp=disc_rp0,jumlah=jumlah0,qtykecil=qtykecil0 where noid={0}", noid)
                Using cmdup2 As OleDbCommand = New OleDbCommand(sqlup2, cn, sqltrans)
                    cmdup2.ExecuteNonQuery()
                End Using


            Else ' jika  bukan barang fisik

                ' balikin ke nilai awal
                Dim sqlup2 As String = String.Format("update trfaktur_to2 set qty=qty0,harga=harga0,disc_per=disc_per0,disc_rp=disc_rp0,jumlah=jumlah0,qtykecil=qtykecil0 where noid={0}", noid)
                Using cmdup2 As OleDbCommand = New OleDbCommand(sqlup2, cn, sqltrans)
                    cmdup2.ExecuteNonQuery()
                End Using

            End If

        End While
        drds.Close()

        If hasil.Equals("") Then
            hasil = "ok"
        End If

lanjut_bawah:

        Return hasil

    End Function

    Private Function simpan22(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal nobukti_fak As String, ByVal nobukti As String, _
                              ByVal tanggal As String, ByVal kdtoko As String, ByVal gudang As String, ByVal nobuktirekap As String) As String

        Dim hasil As String = ""

        Dim sqlc As String = String.Format("select * from trfaktur_to3 inner join ms_barang on trfaktur_to3.kd_barang=ms_barang.kd_barang where trfaktur_to3.nobukti='{0}'", nobukti_fak)
        Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
        Dim drc As OleDbDataReader = cmdc.ExecuteReader

        While drc.Read

            Dim jenis As String = drc("jenis").ToString
            Dim kdbar As String = drc("kd_barang").ToString
            Dim qty As String = drc("qty").ToString
            Dim satuan As String = drc("satuan").ToString
            Dim qtykecil As String = drc("qtykecil").ToString
            Dim noid As String = drc("noid").ToString

            Dim qty1 As Integer = Integer.Parse(drc("qty1").ToString)
            Dim qty2 As Integer = Integer.Parse(drc("qty2").ToString)
            Dim qty3 As Integer = Integer.Parse(drc("qty3").ToString)

            Dim noidold As Integer


            Dim jdulmess As String = ""
            If Integer.Parse(dv1(Me.BindingContext(bs1).Position)("sfaktur_kosong").ToString) = 1 Then
                jdulmess = "Faktur Balik/Realisasi TO-K (Batal)"
            Else
                jdulmess = "Faktur Balik/Realisasi (Batal)"
            End If

            Dim sqlsel_old As String = String.Format("select trfaktur_balik23.*,trrekap_to.kd_supir,trrekap_to.nopol,trrekap_to2.statkirim from trfaktur_balik23 inner join trrekap_to2 on trfaktur_balik23.nobukti_fak=trrekap_to2.nobukti_fak " & _
                "inner join trrekap_to on trrekap_to2.nobukti=trrekap_to.nobukti where trfaktur_balik23.nobukti='{0}' and trfaktur_balik23.nobukti_fak='{1}' and trfaktur_balik23.kd_barang='{2}' and trrekap_to.nobukti='{3}'", nobukti, nobukti_fak, kdbar, nobuktirekap)
            Dim cmdsel_old As OleDbCommand = New OleDbCommand(sqlsel_old, cn, sqltrans)
            Dim drsel_old As OleDbDataReader = cmdsel_old.ExecuteReader

            If drsel_old.Read Then

                Dim qtyold As Integer = Integer.Parse(drsel_old("qty").ToString)
                Dim qtyold_pinj As Integer = Integer.Parse(drsel_old("qty_pinj").ToString)
                Dim qtyold_msk As Integer = Integer.Parse(drsel_old("qty_msk").ToString)
                noidold = Integer.Parse(drsel_old("noid").ToString)

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

                If Integer.Parse(dv1(Me.BindingContext(bs1).Position)("sfaktur_kosong").ToString) = 0 Then

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtyold_msk, kdbar, gudang, False, False, False)
                    If Not hasilplusmin.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        Exit While
                    End If

                    If qtyold_msk <> 0 Then
                        Clsmy.Insert_HistBarang(cn, sqltrans, nobukti_fak, tanggal, gudang, kdbar, 0, qtyold_msk, "Faktur Balik/Realisasi (Batal)", drsel_old("kd_supir").ToString, drsel_old("nopol").ToString)
                    End If

                End If


                'If qtyold < 0 Then
                '    qtyold = qtyold - (qtyold + qtyold)
                'End If

                If drsel_old("statkirim").ToString.Equals("TERKIRIM") Then

                    '3. insert to hist stok
                    If qtyold_msk > 0 And qtyold <> 0 Then
                        Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tanggal, "None B", kdbar, qtyold_msk, 0, jdulmess, drsel_old("kd_supir").ToString, drsel_old("nopol").ToString)
                    End If

                    If qtyold <> 0 Then

                        qtyold = qtyold + qtyold_msk

                        Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tanggal, "None B", kdbar, 0, qtyold, jdulmess, drsel_old("kd_supir").ToString, drsel_old("nopol").ToString)
                    End If


                    If qtyold_pinj <> 0 Then
                        Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tanggal, "None P", kdbar, 0, qtyold_pinj, jdulmess, drsel_old("kd_supir").ToString, drsel_old("nopol").ToString)
                    End If


                End If

            End If
            drsel_old.Close()

            Dim sqlup3 As String = String.Format("update trfaktur_to3 set qty=0,qtykecil=0,jumlah=0 where noid={0}", noid)
            Using cmdup3 As OleDbCommand = New OleDbCommand(sqlup3, cn, sqltrans)
                cmdup3.ExecuteNonQuery()
            End Using

        End While
        drc.Close()


        If hasil.Equals("") Then
            hasil = "ok"
        End If

lanjut_bawah:

        Return hasil

    End Function

    Private Function simpan_retur(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, _
                                  ByVal nobukti As String, ByVal nobukti_rekap As String) As String

        Dim hasil As String = ""

        Dim jjenis As String = "Faktur Balik/Realisasi-Ret (Batal)"


        Dim sqlsebelum As String = String.Format("SELECT trfaktur_balik24.qty_masuk, trfaktur_balik24.noid, trfaktur_balik24.kd_barang, ms_barang.qty1, ms_barang.qty2, ms_barang.qty3, trfaktur_to.kd_toko, " & _
            "trfaktur_balik24.nobukti_fak, trrekap_to.kd_supir, trrekap_to.nopol, trfaktur_balik24.kd_gudang " & _
            "FROM trrekap_to INNER JOIN " & _
            "trfaktur_balik ON trrekap_to.nobukti = trfaktur_balik.nobukti_rkp INNER JOIN " & _
            "trfaktur_balik24 INNER JOIN " & _
            "ms_barang ON trfaktur_balik24.kd_barang = ms_barang.kd_barang INNER JOIN " & _
            "trfaktur_to ON trfaktur_balik24.nobukti_fak = trfaktur_to.nobukti ON trfaktur_balik.nobukti = trfaktur_balik24.nobukti " & _
            "WHERE trfaktur_to.nobukti='{0}' and trrekap_to.nobukti='{1}'", nobukti, nobukti_rekap)

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

                If qtysebelumnya <> 0 Then
                    '3. insert to hist stok
                    Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, dv1(Me.BindingContext(bs1).Position)("tgl_masuk").ToString, kdgudang_sebelum, kdbar_sebelum, 0, qtysebelumnya, jjenis, kd_supir_sebelum, nopol_sebelum)
                End If
                

                If apakah_brg_kembali(cn, sqltrans, kdbar_sebelum) Then
                    If qtysebelumnya <> 0 Then
                        Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, dv1(Me.BindingContext(bs1).Position)("tgl_masuk").ToString, "None", kdbar_sebelum, qtysebelumnya, 0, jjenis, kd_supir_sebelum, nopol_sebelum)
                    End If
                End If

                Dim sqlup_balik2 As String = String.Format("update trfaktur_balik24 set qty_masuk={0} where noid={1}", 0, drdseb("noid").ToString)
                Using cmdup_balik2 As OleDbCommand = New OleDbCommand(sqlup_balik2, cn, sqltrans)
                    cmdup_balik2.ExecuteNonQuery()
                End Using

            End If
        End While
        drdseb.Close()

        If hasil.Equals("") Then
            hasil = "ok"
        End If

langsung_keluar:

        Return hasil

    End Function


    Private Sub Get_Aksesform()

        Dim rows() As DataRow = dtmenu.Select(String.Format("NAMAFORM='{0}'", Me.Name.ToUpper.ToString))

        If Convert.ToInt16(rows(0)("t_add")) = 1 Then
            tsadd.Enabled = True
        Else
            tsadd.Enabled = False
        End If

        If Convert.ToInt16(rows(0)("t_edit")) = 1 Then
            tsedit.Enabled = True
        Else
            tsedit.Enabled = False
        End If

        If Convert.ToInt16(rows(0)("t_del")) = 1 Then
            tsdel.Enabled = True
        Else
            tsdel.Enabled = False
        End If

        If Convert.ToInt16(rows(0)("t_active")) = 1 Then
            tsview.Enabled = True
        Else
            tsview.Enabled = False
        End If

    End Sub

    Private Sub cekbatal_onserver()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select sbatal,sverif from trfaktur_balik where nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd(0).ToString) Then
                    dv1(bs1.Position)("sbatal") = drd(0).ToString
                    dv1(bs1.Position)("sverif") = drd(1).ToString
                End If
            End If
            drd.Close()


        Catch ex As Exception

        End Try

    End Sub

    Private Sub fuser_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        tcbofind.SelectedIndex = 0

        Get_Aksesform()

        opendata()
    End Sub

    Private Sub tsfind_Click(sender As System.Object, e As System.EventArgs) Handles tsfind.Click
        cari()
    End Sub

    Private Sub tfind_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tfind.KeyDown
        If e.KeyCode = 13 Then
            cari()
        End If
    End Sub

    Private Sub tsref_Click(sender As System.Object, e As System.EventArgs) Handles tsref.Click
        tsfind.Text = ""
        opendata()
    End Sub

    Private Sub tsdel_Click(sender As System.Object, e As System.EventArgs) Handles tsdel.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        cekbatal_onserver()

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Faktur telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If dv1(bs1.Position)("sverif").ToString.Equals("1") Then
            MsgBox("Faktur telah diverifikasi...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If MsgBox("Yakin akan dibatalkan ?", vbYesNo + vbQuestion, "Konfirmasi") = vbNo Then
            Exit Sub
        End If

        hapus()

    End Sub

    Private Sub tsadd_Click(sender As System.Object, e As System.EventArgs) Handles tsadd.Click

        statadd = True

        Using fkar2 As New ffaktur_b2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0, .statview = False}
            fkar2.ShowDialog()
            statadd = False
        End Using
    End Sub

    Private Sub tsedit_Click(sender As System.Object, e As System.EventArgs) Handles tsedit.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        'If dv1(bs1.Position)("skirim").ToString.Equals("1") And dv1(bs1.Position)("spulang").ToString.Equals("0") Then
        '    MsgBox("Barang sedang dikirim...", vbOKOnly + vbExclamation, "Informasi")
        '    Return
        'End If

        cekbatal_onserver()

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Faktur telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        Dim sverif As Boolean = False
        If dv1(bs1.Position)("sverif").ToString.Equals("1") Then
            'MsgBox("Faktur telah diverifikasi...", vbOKOnly + vbExclamation, "Informasi")
            'Return
            sverif = True
        End If

        statadd = True
        Using fkar2 As New ffaktur_b2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position, .statview = False, .statverif_realisasi = sverif}
            fkar2.ShowDialog()
            statadd = False
        End Using

    End Sub

    Private Sub tsview_Click(sender As System.Object, e As System.EventArgs) Handles tsview.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        Using fkar2 As New ffaktur_b2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position, .statview = True}
            fkar2.btsimpan.Enabled = False
            fkar2.ShowDialog()
        End Using

    End Sub

    Private Sub GridView1_Click(sender As Object, e As System.EventArgs) Handles GridView1.Click
        opendata2()
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        opendata2()
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        opendata2()
    End Sub

    Private Sub GridView1_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles GridView1.SelectionChanged
        opendata2()
    End Sub


End Class