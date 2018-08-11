Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy
Imports DevExpress.XtraReports.UI

'Imports System.Drawing.Printing
'Imports DevExpress.XtraPrinting
'Imports DevExpress.XtraReports.UI

Public Class frekap_to

    Private bs1 As BindingSource
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager_tk As Data.DataViewManager
    Private dv_tk As Data.DataView

    Private dvmanager_br As Data.DataViewManager
    Private dv_br As Data.DataView

    Private statmanipulate As Boolean = False

    Private Sub opendata_tk()

        grid2.DataSource = Nothing
        dv_tk = Nothing

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim sql As String = String.Format("select trfaktur_to.nobukti,ms_toko.nama_toko,ms_toko.alamat_toko,trrekap_to2.statkirim " & _
        "from trrekap_to2 " & _
        "inner join trfaktur_to on trrekap_to2.nobukti_fak=trfaktur_to.nobukti " & _
        "inner join ms_toko on trfaktur_to.kd_toko=ms_toko.kd_toko where trrekap_to2.nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)

        Dim cn As OleDbConnection = Nothing

        Try


            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager_tk = New DataViewManager(ds)
            dv_tk = dvmanager_tk.CreateDataView(ds.Tables(0))

            grid2.DataSource = dv_tk


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

    Private Sub opendata_br()

        grid3.DataSource = Nothing
        dv_br = Nothing

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim sql As String = String.Format("select ms_barang.kd_barang,ms_barang.nama_barang, " & _
        "trrekap_to3.jml / (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3) as jml,ms_barang.satuan1 " & _
        "from trrekap_to3 inner join ms_barang " & _
        "on trrekap_to3.kd_barang=ms_barang.kd_barang WHERE trrekap_to3.nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)

        Dim cn As OleDbConnection = Nothing

        Try


            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager_br = New DataViewManager(ds)
            dv_br = dvmanager_br.CreateDataView(ds.Tables(0))

            grid3.DataSource = dv_br


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


    Private Sub opendata()

        Dim sql As String = String.Format("SELECT    trrekap_to.smuat,trrekap_to.spulang, trrekap_to.sbatal,trrekap_to.nobukti, trrekap_to.tgl, trrekap_to.tglmuat, trrekap_to.tglkirim, trrekap_to.kd_supir,'' AS namasupir, trrekap_to.nopol," & _
            "trrekap_to.kd_jalur, ms_jalur_kirim.nama_jalur, trrekap_to.note, trrekap_to.kd_kenek1, trrekap_to.kd_kenek2, trrekap_to.kd_kenek3,trrekap_to.tot_nota,trrekap_to.sfaktur_kosong,trrekap_to.jmlprint " & _
            "FROM         trrekap_to INNER JOIN " & _
            "ms_jalur_kirim ON trrekap_to.kd_jalur = ms_jalur_kirim.kd_jalur where trrekap_to.tgl >='{0}' and trrekap_to.tgl <='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))


        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing

        Try

            open_wait()

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New DataSet()
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

        End Try

        If dv1.Count > 0 Then
            bs1.MoveLast()
            opendata_br()
            opendata_tk()
        Else
            opendata_br()
            opendata_tk()
        End If

    End Sub

    Private Sub cari()

        'bs1.DataSource = Nothing
        grid1.DataSource = Nothing
        Dim cn As OleDbConnection = Nothing

        Dim sql As String = ""
        Dim scbo As Integer = tcbofind.SelectedIndex

        sql = String.Format("SELECT    trrekap_to.smuat,trrekap_to.spulang, trrekap_to.sbatal,trrekap_to.nobukti, trrekap_to.tgl, trrekap_to.tglmuat, trrekap_to.tglkirim, trrekap_to.kd_supir, '' AS namasupir, trrekap_to.nopol," & _
            "trrekap_to.kd_jalur, ms_jalur_kirim.nama_jalur, trrekap_to.note, trrekap_to.kd_kenek1, trrekap_to.kd_kenek2, trrekap_to.kd_kenek3,trrekap_to.tot_nota,trrekap_to.sfaktur_kosong,trrekap_to.jmlprint " & _
            "FROM         trrekap_to INNER JOIN " & _
            "ms_jalur_kirim ON trrekap_to.kd_jalur = ms_jalur_kirim.kd_jalur  where trrekap_to.tgl >='{0}' and trrekap_to.tgl <='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))

        Select Case tcbofind.SelectedIndex
            Case 0 ' kode
                sql = String.Format("{0} and trrekap_to.nobukti like '%{1}%'", sql, tfind.Text.Trim)
            Case 1 ' tgl rekap

                If Not IsDate(tfind.Text.Trim) Then
                    MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                    Return
                Else

                    If tfind.Text.Trim.Length < 10 Or tfind.Text.Trim.Length > 10 Then
                        MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                        Return
                    End If

                End If

                sql = String.Format("{0} and trrekap_to.tgl='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))
            Case 2 ' tgl muat

                If Not IsDate(tfind.Text.Trim) Then
                    MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                    Return
                Else

                    If tfind.Text.Trim.Length < 10 Or tfind.Text.Trim.Length > 10 Then
                        MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                        Return
                    End If

                End If

                sql = String.Format("{0} and trrekap_to.tglmuat='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))

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
            Case 4 ' supir
                sql = String.Format("{0} and trrekap_to.kd_supir in (select kd_karyawan from ms_pegawai where nama_karyawan like '%{1}%')", sql, tfind.Text.Trim)
            Case 5 ' nopol
                sql = String.Format("{0} and trrekap_to.nopol like '%{1}%'", sql, tfind.Text.Trim)
            Case 6 ' jalur
                sql = String.Format("{0} and ms_jalur_kirim.nama_jalur like '%{1}%'", sql, tfind.Text.Trim)
            Case 7 ' no-faktur
                sql = String.Format("{0} and trrekap_to.nobukti in (select nobukti from trrekap_to2 where nobukti_fak='{1}')", sql, tfind.Text.Trim)
            Case 8 ' toko
                sql = String.Format("{0} and trrekap_to.nobukti in (select trrekap_to2.nobukti from trrekap_to2 inner join trfaktur_to on trrekap_to2.nobukti_fak=trfaktur_to.nobukti " & _
                "inner join ms_toko on trfaktur_to.kd_toko=ms_toko.kd_toko where ms_toko.nama_toko like '%{1}%')", sql, tfind.Text.Trim)
            Case 9 ' ket
                sql = String.Format("{0} and trrekap_to.note like '%{1}%'", sql, tfind.Text.Trim)
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

    Private Sub hapus()

        Dim alasan = ""
        Using falasanb As New fkonfirm_hapus With {.WindowState = FormWindowState.Normal, .StartPosition = FormStartPosition.CenterScreen}
            falasanb.ShowDialog()
            alasan = falasanb.get_alasan
        End Using

        If alasan = "" Then
            Return
        End If

        Dim sql As String = String.Format("update trrekap_to set sbatal=1,alasan_batal='{0}' where nobukti='{1}'", alasan.ToUpper, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString)

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            comd = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            If hapus2(cn, sqltrans) = "ok" Then

                Clsmy.InsertToLog(cn, "btrekap_to", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString, dv1(Me.BindingContext(bs1).Position)("nopol").ToString, sqltrans)

                sqltrans.Commit()

                dv1(bs1.Position)("sbatal") = 1

                close_wait()

                MsgBox("Data telah dibatalkan...", vbOKOnly + vbInformation, "Informasi")

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

    Private Function hapus2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim sql As String = String.Format("select * from trrekap_to2 where nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim hasil As String = ""
        Dim cmdtrans As OleDbCommand

        While drd.Read
            Dim sqlup As String = String.Format("update trfaktur_to set skirim=0 where nobukti='{0}'", drd("nobukti_fak").ToString)
            cmdtrans = New OleDbCommand(sqlup, cn, sqltrans)
            cmdtrans.ExecuteNonQuery()


            If Integer.Parse(dv1(bs1.Position)("sfaktur_kosong").ToString) = 1 Then
                GoTo lanjut_karnafaktur_kosong
            End If

            Dim sqlc As String = String.Format("select trfaktur_to2.kd_barang,trfaktur_to2.kd_gudang,trfaktur_to2.qtykecil,trfaktur_to2.qtykecil0,ms_barang.qty1,ms_barang.qty2,ms_barang.qty3,trfaktur_to.kd_toko from trfaktur_to2 inner join ms_barang on trfaktur_to2.kd_barang=ms_barang.kd_barang inner join trfaktur_to on trfaktur_to.nobukti=trfaktur_to2.nobukti where ms_barang.jenis='FISIK' and trfaktur_to2.nobukti='{0}'", drd("nobukti_fak").ToString)
            Dim cmdc = New OleDbCommand(sqlc, cn, sqltrans)
            Dim drdc As OleDbDataReader = cmdc.ExecuteReader

            If drdc.HasRows Then

                While drdc.Read

                    Dim qty1 As Integer = Integer.Parse(drdc("qty1").ToString)
                    Dim qty2 As Integer = Integer.Parse(drdc("qty2").ToString)
                    Dim qty3 As Integer = Integer.Parse(drdc("qty3").ToString)
                    Dim kd_toko As String = drdc("kd_toko").ToString

                    Dim kdbar As String = drdc("kd_barang").ToString
                    Dim kdgud As String = drdc("kd_gudang").ToString
                    Dim qtykecil As Integer = Integer.Parse(drdc("qtykecil0").ToString)

                    'If qtykecil = 0 Then
                    '    Dim sqltok As String = String.Format("select spred_to from ms_toko where kd_toko='{0}'", kd_toko)
                    '    Dim cmdtok As OleDbCommand = New OleDbCommand(sqltok, cn, sqltrans)
                    '    Dim drtok As OleDbDataReader = cmdtok.ExecuteReader

                    '    Dim spread_to As Integer = 0

                    '    If drtok.Read Then
                    '        spread_to = drtok(0).ToString
                    '    End If
                    '    drtok.Close()


                    '    If spread_to = 0 Then

                    '        close_wait()
                    '        MsgBox("Jumlah jual tidak boleh kosong", vbOKOnly + vbExclamation, "Informasi")
                    '        hasil = "error"
                    '        Exit While

                    '    End If

                    '    qtykecil = 200
                    '    kdbar = "G0001"
                    '    kdgud = "G000"

                    'End If

                    ' cek apakah barang kosong
                    'Dim sqlcekapa As String = String.Format("select kd_barang_kmb from ms_barang where kd_barang_kmb='{0}'", kdbar)
                    'Dim cmdcekapa As OleDbCommand = New OleDbCommand(sqlcekapa, cn, sqltrans)
                    'Dim drapa As OleDbDataReader = cmdcekapa.ExecuteReader

                    'If drapa.Read Then
                    '    If Not drapa(0).ToString.Equals("") Then

                    '        Dim sqlkosong As String = String.Format("select noid,jml from ms_toko4 where kd_toko='{0}' and kd_barang='{1}'", kd_toko, kdbar)
                    '        Dim cmdkosong As OleDbCommand = New OleDbCommand(sqlkosong, cn, sqltrans)
                    '        Dim drkosong As OleDbDataReader = cmdkosong.ExecuteReader

                    '        If drkosong.Read Then

                    '            If IsNumeric(drkosong(0).ToString) Then

                    '                Dim totqty As Integer = qty1 * qty2 * qty3
                    '                Dim hasilqty As Integer = Integer.Parse(drkosong(1).ToString)
                    '                Dim jml1 As Integer = 0
                    '                Dim jml2 As Integer = 0
                    '                Dim jml3 As Integer = 0

                    '                hasilqty = hasilqty - qtykecil

                    '                If hasilqty >= totqty Then

                    '                    Dim sisa As Integer = hasilqty Mod totqty

                    '                    jml1 = (hasilqty - sisa) / totqty

                    '                    If sisa > qty2 Then
                    '                        jml2 = sisa
                    '                        sisa = sisa Mod qty2

                    '                        jml2 = (jml2 - sisa) / qty2
                    '                        jml3 = sisa

                    '                    Else
                    '                        jml2 = sisa
                    '                        jml3 = 0
                    '                    End If

                    '                Else
                    '                    hasilqty = 0
                    '                    jml1 = 0
                    '                    jml2 = 0
                    '                    jml3 = 0
                    '                End If

                    '                Dim sqlup_ks As String = String.Format("update ms_toko4 set jml={0},jml1={1},jml2={2},jml3={3} where noid='{4}'", hasilqty, jml1, jml2, jml3, drkosong(0).ToString)
                    '                Using cmdup_ks As OleDbCommand = New OleDbCommand(sqlup_ks, cn, sqltrans)
                    '                    cmdup_ks.ExecuteNonQuery()
                    '                End Using

                    '            End If

                    '        End If
                    '        drkosong.Close()

                    '    End If
                    'End If
                    'drapa.Close()




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

                    '3. insert to hist stok
                    Clsmy.Insert_HistBarang(cn, sqltrans, drd("nobukti_fak").ToString, dv1(Me.BindingContext(dv1).Position)("tglmuat").ToString, kdgud, kdbar, qtykecil, 0, "Jual TO (Batal)", dv1(bs1.Position)("kd_supir").ToString, dv1(bs1.Position)("nopol").ToString)


                    ' masukkan ke rekap 3 yaitu total barangnya
                    Dim sqlcek3 As String = String.Format("select noid from trrekap_to3 where nobukti='{0}' and kd_barang='{1}'", drd("nobukti").ToString, kdbar)
                    Dim cmdcek3 As OleDbCommand = New OleDbCommand(sqlcek3, cn, sqltrans)
                    Dim drdcek3 As OleDbDataReader = cmdcek3.ExecuteReader

                    Dim noidrek3 As Integer = 0
                    If drdcek3.Read Then
                        If IsNumeric(drdcek3(0).ToString) Then
                            noidrek3 = Integer.Parse(drdcek3(0).ToString)
                        End If
                    End If
                    drdcek3.Close()

                    If noidrek3 = 0 Then
                    Else
                        Dim sqlup3 As String = String.Format("update trrekap_to3 set jml=jml-{0} where noid='{1}'", qtykecil, noidrek3)
                        Using cmdtok3 As OleDbCommand = New OleDbCommand(sqlup3, cn, sqltrans)
                            cmdtok3.ExecuteNonQuery()
                        End Using

                    End If



                End While

            End If

            drdc.Close()

lanjut_karnafaktur_kosong:

        End While

        drd.Close()

        ' khusus nota kosong
        If Integer.Parse(dv1(bs1.Position)("sfaktur_kosong").ToString) = 1 Then

            Dim sqlcek As String = String.Format("select jml,noid from trrekap_to3 where nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)
            Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn, sqltrans)
            Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

            Dim jmlold As Integer = 0
            Dim noid3 As Integer = 0

            If drdcek.Read Then
                If IsNumeric(drdcek(0).ToString) Then
                    jmlold = Integer.Parse(drdcek(0).ToString)
                    noid3 = Integer.Parse(drdcek(1).ToString)
                End If
            End If
            drdcek.Close()

            If jmlold > 0 Then

                '3. insert to hist stok
                Clsmy.Insert_HistBarang(cn, sqltrans, dv1(bs1.Position)("nobukti").ToString, dv1(bs1.Position)("tglmuat").ToString, "G000", "G0001", jmlold, 0, "Jual TO Kanvas (Batal)", dv1(bs1.Position)("kd_supir").ToString, dv1(bs1.Position)("nopol").ToString)


                '' kurangi
                Dim sqlup3 As String = String.Format("update trrekap_to3 set jml=jml-{0} where noid='{1}'", jmlold, noid3)
                Using cmdtok3 As OleDbCommand = New OleDbCommand(sqlup3, cn, sqltrans)
                    cmdtok3.ExecuteNonQuery()
                End Using

                '2. update barang
                Dim hasilplusmin2 As String = PlusMin_Barang(cn, sqltrans, jmlold, "G0001", "G000", True, False, False)
                If Not hasilplusmin2.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilplusmin2, vbOKOnly + vbExclamation, "Informasi")
                    hasil = "error"
                End If

            End If



        End If


        If Not (hasil.Equals("error")) Then
            hasil = "ok"
        End If

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

        Dim rows2() As DataRow = dtmenu2.Select(String.Format("NAMAFORM='{0}'", Me.Name.ToUpper.ToString))

        If rows2.Length > 0 Then
            tsprint.Enabled = True
            tsprint2.Enabled = True
        Else
            tsprint.Enabled = False
            tsprint2.Enabled = False
        End If

    End Sub

    Private Sub cekbatal_onserver()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select spulang,smuat,sbatal,jmlprint from trrekap_to where nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd(0).ToString) Then
                    dv1(bs1.Position)("spulang") = drd(0).ToString
                    dv1(bs1.Position)("smuat") = drd(1).ToString
                    dv1(bs1.Position)("sbatal") = drd(2).ToString
                    dv1(bs1.Position)("jmlprint") = drd(3).ToString
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

        If dv1(bs1.Position)("spulang").ToString.Equals("1") Then
            MsgBox("Rekap telah diantar...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If dv1(bs1.Position)("smuat").ToString.Equals("1") Then
            MsgBox("Rekap telah Dimuat...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Rekap telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If MsgBox("Yakin akan dibatalkan ?", vbYesNo + vbQuestion, "Konfirmasi") = vbNo Then
            Exit Sub
        End If

        hapus()

    End Sub

    Private Sub tsadd_Click(sender As System.Object, e As System.EventArgs) Handles tsadd.Click

        Dim stprint As Boolean
        If tsprint.Enabled = True Then
            stprint = True
        Else
            stprint = False
        End If

        statmanipulate = True

        Using fkar2 As New frekap_to2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0, .statprint = stprint}
            fkar2.ShowDialog()
            statmanipulate = False
        End Using
    End Sub

    Private Sub tsedit_Click(sender As System.Object, e As System.EventArgs) Handles tsedit.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        cekbatal_onserver()

        If dv1(bs1.Position)("smuat").ToString.Equals("1") Then
            MsgBox("Rekap telah Dimuat...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If dv1(bs1.Position)("spulang").ToString.Equals("1") Then
            MsgBox("Rekap telah diantar...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Rekap telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        Dim stprint As Boolean
        If tsprint.Enabled = True Then
            stprint = True
        Else
            stprint = False
        End If

        statmanipulate = True

        Using fkar2 As New frekap_to2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position, .statprint = stprint}
            fkar2.ShowDialog()
            statmanipulate = False
        End Using

    End Sub

    Private Sub tsview_Click(sender As System.Object, e As System.EventArgs) Handles tsview.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        statmanipulate = True
        Using fkar2 As New frekap_to2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.btsimpan.Enabled = False
            fkar2.btadd.Enabled = False
            fkar2.btload.Enabled = False
            fkar2.btdel.Enabled = False
            fkar2.ShowDialog()
            statmanipulate = False
        End Using

    End Sub

    Private Sub cekjmlprint()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqlupprint As String = String.Format("update trrekap_to set jmlprint=jmlprint+1 where nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)
            Using cmdupprint As OleDbCommand = New OleDbCommand(sqlupprint, cn)
                cmdupprint.ExecuteNonQuery()
            End Using

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


    Private Sub tsprint_Click(sender As System.Object, e As System.EventArgs) Handles tsprint.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Rekap telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        Dim nobukti As String = dv1(bs1.Position)("nobukti").ToString

        Using fkar2 As New fpr_rekapaktur With {.nobukti = nobukti}
            fkar2.ShowDialog(Me)
        End Using

        cekbatal_onserver()

    End Sub

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            'Dim sql As String = String.Format("SELECT     trrekap_to.nobukti, trrekap_to.tgl, trrekap_to.tglmuat, trrekap_to.tglkirim, ms_supir.nama_karyawan AS nama_supir, ms_kenek1.nama_karyawan AS nama_kenek1, " & _
            '          "ms_kenek2.nama_karyawan AS nama_kenek2, ms_kenek3.nama_karyawan AS nama_kenek3, ms_jalur_kirim.nama_jalur, trrekap_to.nopol, trrekap_to.note, " & _
            '          "ms_barang.nama_barang, trfaktur_to2.qtykecil, ms_barang.satuan1, ms_barang.satuan2, ms_barang.satuan3, ms_gudang.kd_gudang, ms_gudang.nama_gudang,  " & _
            '          "ms_barang.qty1, ms_barang.qty2, ms_barang.qty3,ms_barang.nourut_lap,trrekap_to.tot_nota " & _
            '            "FROM         trrekap_to INNER JOIN " & _
            '          "trrekap_to2 ON trrekap_to.nobukti = trrekap_to2.nobukti INNER JOIN " & _
            '          "trfaktur_to ON trrekap_to2.nobukti_fak = trfaktur_to.nobukti INNER JOIN " & _
            '          "trfaktur_to2 ON trfaktur_to.nobukti = trfaktur_to2.nobukti INNER JOIN " & _
            '          "ms_barang ON trfaktur_to2.kd_barang = ms_barang.kd_barang INNER JOIN " & _
            '          "ms_gudang ON trfaktur_to2.kd_gudang = ms_gudang.kd_gudang LEFT OUTER JOIN " & _
            '          "ms_jalur_kirim ON trrekap_to.kd_jalur = ms_jalur_kirim.kd_jalur LEFT OUTER JOIN " & _
            '          "ms_pegawai AS ms_kenek3 ON trrekap_to.kd_kenek3 = ms_kenek3.kd_karyawan LEFT OUTER JOIN " & _
            '          "ms_pegawai AS ms_kenek2 ON trrekap_to.kd_kenek2 = ms_kenek2.kd_karyawan LEFT OUTER JOIN " & _
            '          "ms_pegawai AS ms_kenek1 ON trrekap_to.kd_kenek1 = ms_kenek1.kd_karyawan LEFT OUTER JOIN " & _
            '          "ms_pegawai AS ms_supir ON trrekap_to.kd_supir = ms_supir.kd_karyawan " & _
            '            "WHERE trrekap_to.sbatal = 0 AND ms_barang.jenis = 'FISIK' AND trrekap_to.nobukti = '{0}'", dv1(bs1.Position)("nobukti").ToString)

            Dim nobukti As String = dv1(bs1.Position)("nobukti").ToString

            Dim sql As String = String.Format("SELECT     trrekap_to.nobukti, trrekap_to.tgl, trrekap_to.tglmuat, trrekap_to.tglkirim, trrekap_to.nopol, supir.nama_karyawan AS nama_supir, " & _
                      "kenek1.nama_karyawan AS nama_kenek1, kenek2.nama_karyawan AS nama_kenek2, kenek3.nama_karyawan AS nama_kenek3, ms_jalur_kirim.nama_jalur, " & _
                      "trrekap_to.tot_nota, ms_barang.kd_barang, ms_barang.nama_lap as nama_barang, (trrekap_to3.jml / (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3)) as jml , ms_barang.satuan1,ms_barang.nourut_lap as nohrus " & _
                      "FROM         ms_pegawai AS kenek1 RIGHT OUTER JOIN " & _
                      "ms_pegawai AS kenek2 RIGHT OUTER JOIN " & _
                      "ms_jalur_kirim RIGHT OUTER JOIN " & _
                      "trrekap_to INNER JOIN " & _
                      "trrekap_to3 ON trrekap_to.nobukti = trrekap_to3.nobukti INNER JOIN " & _
                      "ms_barang ON trrekap_to3.kd_barang = ms_barang.kd_barang ON ms_jalur_kirim.kd_jalur = trrekap_to.kd_jalur LEFT OUTER JOIN " & _
                      "ms_pegawai AS kenek3 ON trrekap_to.kd_kenek3 = kenek3.kd_karyawan ON kenek2.kd_karyawan = trrekap_to.kd_kenek2 ON  " & _
                      "kenek1.kd_karyawan = trrekap_to.kd_kenek1 LEFT OUTER JOIN " & _
                      "ms_pegawai AS supir ON trrekap_to.kd_supir = supir.kd_karyawan " & _
                      "WHERE not(ms_barang.kd_barang in ('BN0002','BN0003')) and trrekap_to.sbatal=0 and trrekap_to.nobukti='{0}'", nobukti)

            Dim sqlsub As String = String.Format("SELECT     trrekap_to.nobukti, " & _
            "ms_barang.kd_barang, ms_barang.nama_lap as nama_barang, sum(trrekap_to3.jml) as jml " & _
            "FROM         ms_pegawai AS kenek1 RIGHT OUTER JOIN  " & _
            "ms_pegawai AS kenek2 RIGHT OUTER JOIN  " & _
            "ms_jalur_kirim RIGHT OUTER JOIN  " & _
            "trrekap_to INNER JOIN  " & _
            "trrekap_to3 ON trrekap_to.nobukti = trrekap_to3.nobukti INNER JOIN  " & _
            "ms_barang ON trrekap_to3.kd_barang = ms_barang.kd_barang ON ms_jalur_kirim.kd_jalur = trrekap_to.kd_jalur LEFT OUTER JOIN  " & _
            "ms_pegawai AS kenek3 ON trrekap_to.kd_kenek3 = kenek3.kd_karyawan ON kenek2.kd_karyawan = trrekap_to.kd_kenek2 ON   " & _
            "kenek1.kd_karyawan = trrekap_to.kd_kenek1 LEFT OUTER JOIN  " & _
            "ms_pegawai AS supir ON trrekap_to.kd_supir = supir.kd_karyawan  " & _
            "WHERE ms_barang.kd_barang in ('BN0002','BN0003') and trrekap_to.sbatal=0 and trrekap_to.nobukti='{0}' " & _
            "group by trrekap_to.nobukti,ms_barang.kd_barang, ms_barang.nama_lap", nobukti)

            Dim ds As DataSet = New dsrekap2
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dsbonus As DataSet = New DataSet
            dsbonus = Clsmy.GetDataSet(sqlsub, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_rekapfak2() With {.DataSource = ds.Tables(0)}
            rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
            rrekap.DataMember = rrekap.DataMember

            If dsbonus.Tables(0).Rows.Count > 0 Then
                If Integer.Parse(dsbonus.Tables(0).Rows(0)("jml").ToString) > 0 Then
                    rrekap.XrSubreport1.ReportSource = New r_rekapfak_bonus
                    rrekap.XrSubreport1.ReportSource.DataSource = dsbonus.Tables(0)
                    rrekap.XrSubreport1.ReportSource.DataMember = rrekap.XrSubreport1.ReportSource.DataMember
                Else
                    rrekap.XrSubreport1.Visible = False
                End If
            Else
                rrekap.XrSubreport1.Visible = False
            End If

            rrekap.XrSubreport2.ReportSource = New r_rekapfak2_detail
            rrekap.XrSubreport2.ReportSource.DataSource = ds.Tables(0)
            rrekap.XrSubreport2.ReportSource.DataMember = rrekap.XrSubreport2.ReportSource.DataMember

            rrekap.PrinterName = varprinter2
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

        cekjmlprint()
        cekbatal_onserver()

    End Sub

    Private Sub tsprint2_Click(sender As System.Object, e As System.EventArgs) Handles tsprint2.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Rekap telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        load_print()

    End Sub

    Private Sub GridView1_Click(sender As Object, e As System.EventArgs) Handles BandedGridView1.Click

        If statmanipulate Then
            Return
        End If

        opendata_tk()
        opendata_br()
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles BandedGridView1.FocusedRowChanged

        If statmanipulate Then
            Return
        End If

        opendata_tk()
        opendata_br()
    End Sub

    Private Sub GridView1_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles BandedGridView1.RowCellClick

        If statmanipulate Then
            Return
        End If

        opendata_tk()
        opendata_br()
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles BandedGridView1.RowClick

        If statmanipulate Then
            Return
        End If

        opendata_tk()
        opendata_br()
    End Sub

End Class