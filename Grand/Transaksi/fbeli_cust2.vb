Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fbeli_cust2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Dim tgl_old As String

    Dim dtspm As DataTable

    Private Sub kosongkan()
        tbukti.Text = "<< New >>"

        tkd_toko.Text = ""
        tnama_toko.Text = ""
        talamat_toko.Text = ""

        tket.Text = ""

        tkd_supir.Text = ""
        tnama_supir.Text = ""


        opengrid()


    End Sub

    Private Sub opengrid()

        Dim sql As String = String.Format("SELECT tr_belicust2.noid, tr_belicust2.kd_gudang, ms_barang.kd_barang, ms_barang.nama_barang, tr_belicust2.qty, " & _
        "tr_belicust2.satuan, tr_belicust2.harga, tr_belicust2.jumlah, tr_belicust2.qtykecil " & _
        "FROM tr_belicust2 INNER JOIN ms_barang ON tr_belicust2.kd_barang = ms_barang.kd_barang WHERE tr_belicust2.nobukti='{0}'", tbukti.Text.Trim)


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
        Dim sql As String = String.Format("SELECT tr_belicust.nobukti, tr_belicust.tanggal, tr_belicust.tanggal_masuk, ms_toko.kd_toko, " & _
        "ms_toko.nama_toko, ms_toko.alamat_toko, ms_pegawai.kd_karyawan, " & _
        "ms_pegawai.nama_karyawan, tr_belicust.jenis_trans, tr_belicust.note,tr_belicust.nopol " & _
        "FROM tr_belicust INNER JOIN " & _
        "ms_toko ON tr_belicust.kd_toko = ms_toko.kd_toko INNER JOIN " & _
        "ms_pegawai ON tr_belicust.kd_sales = ms_pegawai.kd_karyawan " & _
        "WHERE tr_belicust.nobukti='{0}'", nobukti)

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
                        ttgl_tempo.EditValue = DateValue(dread("tanggal_masuk").ToString)

                        tgl_old = ttgl_tempo.EditValue

                        tkd_toko.EditValue = dread("kd_toko").ToString
                        tnama_toko.EditValue = dread("nama_toko").ToString
                        talamat_toko.EditValue = dread("alamat_toko").ToString

                        tjenis.EditValue = dread("jenis_trans").ToString

                        tket.EditValue = dread("note").ToString

                        tkd_supir.EditValue = dread("kd_karyawan").ToString
                        tnama_supir.EditValue = dread("nama_karyawan").ToString

                        tnopol.EditValue = dread("nopol").ToString

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

    Private Function cekbukti(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim sql As String = ""

        Dim tahun As String = Year(ttgl.EditValue)
        tahun = Microsoft.VisualBasic.Right(tahun, 2)
        Dim bulan As String = Month(ttgl.EditValue)

        If bulan.Length = 1 Then
            bulan = "0" & bulan
        End If

        Dim tahunbulan As String = String.Format("{0}{1}", tahun, bulan)

        sql = String.Format("select max(nobukti) from tr_belicust where nobukti like '%BO.{0}%'", tahunbulan)

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

        Dim jenisnota As String = "BO."

        Return String.Format("{0}{1}{2}{3}", jenisnota, tahun, bulan, kbukti)

    End Function

    Private Sub cek_nopolspm(ByVal cn As OleDbConnection)

        'Dim cn As OleDbConnection = Nothing
        'Try

        '    cn = New OleDbConnection
        '    cn = Clsmy.open_conn

        Dim sqlspm As String = String.Format("select trspm.nobukti,trspm.nopol from trturun_br inner join trspm on trturun_br.nobukti_spm=trspm.nobukti " & _
        "where trspm.kd_sales='{0}' and trspm.tglberangkat='{1}'", tkd_supir.Text.Trim, convert_date_to_eng(ttgl_tempo.EditValue))

        dtspm = New DataTable

        Dim dsspm As DataSet = New DataSet
        dsspm = Clsmy.GetDataSet(sqlspm, cn)

        dtspm = dsspm.Tables(0)

        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        'Finally


        '    If Not cn Is Nothing Then
        '        If cn.State = ConnectionState.Open Then
        '            cn.Close()
        '        End If
        '    End If
        'End Try

    End Sub

    Private Function cek_nopolfak(ByVal cn As OleDbConnection) As String

        Dim hasil As String = ""

        Dim sqlkir As String = String.Format("select trrekap_to.nopol from trrekap_to inner join trrekap_to2 on trrekap_to.nobukti=trrekap_to2.nobukti " & _
        "inner join trfaktur_to on trrekap_to2.nobukti_fak=trfaktur_to.nobukti " & _
        "inner join trfaktur_balik on trfaktur_balik.nobukti_rkp=trrekap_to.nobukti " & _
        "where trrekap_to.tglkirim='{0}' and trfaktur_to.kd_karyawan='{1}'", convert_date_to_eng(ttgl_tempo.EditValue), tkd_supir.EditValue)

        Dim cmdkir As OleDbCommand = New OleDbCommand(sqlkir, cn)
        Dim drdkir As OleDbDataReader = cmdkir.ExecuteReader

        If drdkir.Read Then
            If Not drdkir(0).ToString.Equals("") Then
                hasil = drdkir(0).ToString
            End If
        End If
        drdkir.Close()

        Return hasil

    End Function

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


    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try
            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim nopol As String = tnopol.EditValue
            'If tjenis.EditValue = "TO" Then

            '    If DateValue(convert_date_to_eng(Date.Now)) <= DateValue(convert_date_to_eng("07/01/2015")) Then
            '        nopol = "BE XXXX XX"
            '    Else

            '        nopol = cek_nopolfak(cn)

            '        If nopol.Length = 0 Then
            '            close_wait()
            '            MsgBox("Tidak ditemukan sales/toko pada tanggal ini..", vbOKOnly + vbInformation, "Informasi")
            '            Return
            '        End If

            '    End If

            'Else
            '    cek_nopolspm(cn)

            '    If dtspm.Rows.Count > 0 Then
            '        nopol = dtspm(0)("nopol").ToString
            '    Else

            '        close_wait()
            '        MsgBox("Tidak ditemukan sales yang berangkat pada tanggal ini..", vbOKOnly + vbInformation, "Informasi")
            '        Return

            '    End If

            'End If

            sqltrans = cn.BeginTransaction

            Dim cmd As OleDbCommand

            If addstat Then
                Dim bukti As String = cekbukti(cn, sqltrans)
                tbukti.EditValue = bukti

                '1 . update faktur
                Dim sqlin_faktur As String = String.Format("insert into tr_belicust (nobukti,tanggal,tanggal_masuk,kd_toko,kd_sales,jenis_trans,note,total,nopol) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}',{7},'{8}')", _
                                            tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_tempo.EditValue), tkd_toko.Text.Trim, tkd_supir.Text.Trim, tjenis.EditValue, tket.Text.Trim, Replace(GridView1.Columns("jumlah").SummaryItem.SummaryValue, ",", "."), nopol)


                cmd = New OleDbCommand(sqlin_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btbeli_cust", 1, 0, 0, 0, tbukti.Text.Trim, ttgl.EditValue, sqltrans)


            Else

                '1. update faktur
                Dim sqlup_faktur As String = String.Format("update tr_belicust set tanggal='{0}',tanggal_masuk='{1}',kd_toko='{2}',kd_sales='{3}',jenis_trans='{4}',note='{5}',total={6},nopol='{7}' where nobukti='{8}'", _
                                            convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_tempo.EditValue), tkd_toko.Text.Trim, tkd_supir.Text.Trim, tjenis.EditValue, tket.Text.Trim, Replace(GridView1.Columns("jumlah").SummaryItem.SummaryValue, ",", "."), nopol, tbukti.Text.Trim)

                cmd = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()


                Clsmy.InsertToLog(cn, "btbeli_cust", 0, 1, 0, 0, tbukti.Text.Trim, ttgl.EditValue, sqltrans)


            End If

            If simpan2(cn, sqltrans, nopol) = "ok" Then

                If Not simpan3_spm(cn, sqltrans) = "ok" Then
                    Return
                End If

                If addstat = True Then
                    insertview()
                Else
                    updateview()
                End If

                sqltrans.Commit()

                close_wait()

                '  MsgBox("Data disimpan...", vbOKOnly + vbInformation, "Informasi")


                If addstat = True Then

                    'If statprint Then
                    '    If MsgBox("Retur akan langsung diprint.. ???", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
                    '        LoadPrint()
                    '    End If
                    'End If

                    kosongkan()
                    ttgl.Focus()
                Else
                    close_wait()

                    'If statprint Then
                    '    If MsgBox("Retur akan langsung diprint.. ???", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
                    '        LoadPrint()
                    '    End If
                    'End If

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

    Private Function simpan2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal nopol As String) As String

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
            Dim noid As String = dv1(i)("noid").ToString

            If noid <> 0 Then

                If DateValue(ttgl_tempo.EditValue) <> DateValue(tgl_old) Then

                    If apakah_brg_kembali(cn, sqltrans, kdbar) = True Then

                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, "None", kdbar, 0, qtykecil, "Beli Cust", tkd_supir.Text.Trim, nopol)

                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, "None", kdbar, 0, qtykecil, "Beli Cust", tkd_supir.Text.Trim, nopol)
                    End If

                    If tjenis.EditValue = "TO" Then

                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, kdgud, kdbar, 0, qtykecil, "Beli Cust", tkd_supir.Text.Trim, nopol)

                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, kdgud, kdbar, qtykecil, 0, "Beli Cust", tkd_supir.Text.Trim, nopol)
                    End If

                    If Not (tjenis.EditValue = "TO") Then


                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, kdgud, kdbar, qtykecil, 0, "Beli Cust", tkd_supir.Text.Trim, nopol)

                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, kdgud, kdbar, 0, qtykecil, "Beli Cust", tkd_supir.Text.Trim, nopol)

                    End If

                End If
            End If

            If addstat = True Or noid = 0 Then

                '1. insert faktur_to
                Dim sqlins As String = String.Format("insert into tr_belicust2 (nobukti,kd_gudang,kd_barang,satuan,qty,qtykecil,harga,jumlah) values('{0}','{1}','{2}','{3}',{4},{5},{6},{7})", _
                                                     tbukti.EditValue, kdgud, kdbar, satuan, Replace(qty, ",", "."), Replace(qtykecil, ",", "."), Replace(harga, ",", "."), Replace(jumlah, ",", "."))

                cmd = New OleDbCommand(sqlins, cn, sqltrans)
                cmd.ExecuteNonQuery()

                ' If tnota.Text.Trim.Length = 0 Then

                If apakah_brg_kembali(cn, sqltrans, kdbar) = True Then

                    Dim sql As String = String.Format("select qty1,qty2,qty3 from ms_barang where kd_barang='{0}'", kdbar)
                    Dim cmdc As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                    Dim drc As OleDbDataReader = cmdc.ExecuteReader

                    If drc.Read Then

                        If IsNumeric(drc(0).ToString) Then

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
                                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, "None", kdbar, 0, qtykecil, "Beli Cust", tkd_supir.Text.Trim, nopol)
                            End If


                        End If

                    End If
                    drc.Close()

                End If


                If tjenis.EditValue = "TO" Then

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
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, kdgud, kdbar, qtykecil, 0, "Beli Cust", tkd_supir.Text.Trim, nopol)

                End If

                If Not (tjenis.EditValue = "TO") Then

                    If apakah_brg_kembali(cn, sqltrans, kdbar) = False Then

                        '2. update barang
                        Dim hasilplusmin As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, kdgud, False, False, False)
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
                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, kdgud, kdbar, 0, qtykecil, "Beli Cust", tkd_supir.Text.Trim, nopol)

                    End If

                End If

            End If
        Next

        If Not hasil.Equals("error") Then
            hasil = "ok"
        End If

        Return hasil

    End Function

    Private Function simpan3_spm(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim hasil = ""

        If IsNothing(dtspm) Then
            GoTo langsung_aja
        End If

        If dtspm.Rows.Count = 0 Then
            GoTo langsung_aja
        End If

        hasil = "error"

        Dim sqldel As String = String.Format("delete from tr_belicust3 where nobukti='{0}'", tbukti.Text.Trim)
        Using cmddel As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
            cmddel.ExecuteNonQuery()
        End Using

        For i As Integer = 0 To dtspm.Rows.Count - 1
            Dim sqlin As String = String.Format("insert into tr_belicust3 (nobukti,nobukti_spm) values('{0}','{1}')", tbukti.Text.Trim, dtspm(i)("nobukti").ToString)
            Using cmdin As OleDbCommand = New OleDbCommand(sqlin, cn, sqltrans)
                cmdin.ExecuteNonQuery()
            End Using
        Next


langsung_aja:
        hasil = "ok"

        Return hasil
    End Function

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("nobukti") = tbukti.Text.Trim
        orow("tanggal") = ttgl.Text.Trim
        orow("tanggal_masuk") = ttgl_tempo.Text.Trim
        orow("kd_toko") = tkd_toko.Text.Trim
        orow("nama_toko") = tnama_toko.Text.Trim
        orow("alamat_toko") = talamat_toko.Text.Trim
        orow("nama_karyawan") = tnama_supir.Text.Trim
        orow("kd_karyawan") = tkd_supir.Text.Trim
        orow("jenis_trans") = tjenis.EditValue
        orow("total") = GridView1.Columns("jumlah").SummaryItem.SummaryValue
        orow("sbatal") = 0

        dv.EndInit()

    End Sub

    Private Sub updateview()

        dv(position)("nobukti") = tbukti.Text.Trim
        dv(position)("tanggal") = ttgl.Text.Trim
        dv(position)("tanggal_masuk") = ttgl_tempo.Text.Trim
        dv(position)("kd_toko") = tkd_toko.Text.Trim
        dv(position)("nama_toko") = tnama_toko.Text.Trim
        dv(position)("alamat_toko") = talamat_toko.Text.Trim
        dv(position)("nama_karyawan") = tnama_supir.Text.Trim
        dv(position)("kd_karyawan") = tkd_supir.Text.Trim
        dv(position)("jenis_trans") = tjenis.EditValue
        dv(position)("total") = GridView1.Columns("jumlah").SummaryItem.SummaryValue

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

        Dim fs As New fssales With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .kd_toko = ""}
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
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and  bagian='SALES' and kd_karyawan='{0}'", tkd_supir.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim nopol As String = ""
                If tjenis.EditValue = "TO" Then
                    nopol = cek_nopolfak(cn)
                Else
                    cek_nopolspm(cn)

                    If dtspm.Rows.Count > 0 Then
                        nopol = dtspm(0)("nopol").ToString
                    End If

                End If

                If nopol.Length = 0 Then
                    nopol = "BE XXXX XX"
                End If

                tnopol.EditValue = nopol

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

        If tjenis.EditValue = "" Then
            MsgBox("Asal Trans harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tjenis.Focus()
            Return
        End If

        If Not IsDate(ttgl_tempo.EditValue) Then
            MsgBox("Tanggal masuk salah..", vbOKOnly + vbInformation, "Informasi")
            ttgl_tempo.Focus()
            Return
        End If

        If tkd_supir.Text.Trim.Length = 0 Then
            MsgBox("Sales harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tkd_supir.Focus()
            Return
        End If

        Using fkar2 As New fbeli_cust3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0, .tglbalik = ttgl_tempo.EditValue, .jenistrans = tjenis.EditValue, .kdsales = tkd_supir.Text.Trim}
            fkar2.ShowDialog(Me)
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
        Else

            Dim cn As OleDbConnection = Nothing
            Dim sqltrans As OleDbTransaction = Nothing

            Try

                Dim nopol As String = ""
                If tjenis.EditValue = "TO" Then

                    If DateValue(convert_date_to_eng(Date.Now)) <= DateValue(convert_date_to_eng("07/01/2015")) Then
                        nopol = "BE XXXX XX"
                    Else
                        nopol = cek_nopolfak(cn)
                    End If

                Else
                    cek_nopolspm(cn)

                    If dtspm.Rows.Count > 0 Then
                        nopol = dtspm(0)("nopol").ToString
                    End If

                End If

                    If Integer.Parse(dv1(Me.BindingContext(dv1).Position)("noid").ToString) = 0 Then
                        dv1.Delete(Me.BindingContext(dv1).Position)
                        Return
                    End If

                    Dim qtykecil As Integer = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtykecil").ToString)
                    Dim kdbar As String = dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString
                    Dim kdgud As String = dv1(Me.BindingContext(dv1).Position)("kd_gudang").ToString

                    cn = New OleDbConnection
                    cn = Clsmy.open_conn

                    sqltrans = cn.BeginTransaction


                    If apakah_brg_kembali(cn, sqltrans, kdbar) = True Then

                        Dim sql As String = String.Format("select qty1,qty2,qty3 from ms_barang where kd_barang='{0}'", kdbar)
                        Dim cmdc As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                        Dim drc As OleDbDataReader = cmdc.ExecuteReader

                        If drc.Read Then

                            If IsNumeric(drc(0).ToString) Then

                                Dim simpankosong_fh As String = simpankosong_f(cn, sqltrans, kdbar, tkd_toko.Text.Trim, drc("qty1").ToString, drc("qty2").ToString, drc("qty3").ToString, qtykecil, True)

                                If Not simpankosong_fh.Equals("ok") Then
                                    close_wait()
                                    If Not IsNothing(sqltrans) Then
                                        sqltrans.Rollback()
                                    End If

                                    MsgBox(simpankosong_fh)
                                    GoTo langsung
                                Else
                                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, "None", kdbar, qtykecil, 0, "Beli Cust (Del)", tkd_supir.Text.Trim, nopol)
                                End If

                            End If

                        End If
                        drc.Close()

                    End If


                    If tjenis.EditValue = "TO" Then

                        '2. update barang
                        Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, False, False, False)
                        If Not hasilplusmin.Equals("ok") Then
                            close_wait()

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                            GoTo langsung
                        End If

                        '3. insert to hist stok
                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, kdgud, kdbar, 0, qtykecil, "Beli Cust (Del)", tkd_supir.Text.Trim, nopol)

                    End If

                    If Not (tjenis.EditValue = "TO") Then

                        If apakah_brg_kembali(cn, sqltrans, kdbar) = False Then

                            '2. update barang
                            Dim hasilplusmin As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, kdgud, True, False, False)
                            If Not hasilplusmin.Equals("ok") Then
                                close_wait()

                                If Not IsNothing(sqltrans) Then
                                    sqltrans.Rollback()
                                End If

                                MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                                GoTo langsung
                            End If

                            '3. insert to hist stok
                            Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_tempo.EditValue, kdgud, kdbar, qtykecil, 0, "Beli Cust (Del)", tkd_supir.Text.Trim, nopol)

                        End If

                    End If

                    dv1.Delete(Me.BindingContext(dv1).Position)


                    ' update header ----------------------------------------

                    '1. update faktur
                    Dim sqlup_faktur As String = String.Format("update tr_belicust set total={0} where nobukti='{1}'", Replace(GridView1.Columns("jumlah").SummaryItem.SummaryValue, ",", "."), tbukti.Text.Trim)

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
        ttgl.Focus()
    End Sub

    Private Sub ffaktur_to2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        tjenis.EditValue = "TO"

        isi_nopol()

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
            MsgBox("Tidak ada barang yang akan dibeli", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If dv1.Count <= 0 Then
            MsgBox("Tidak ada barang yang akan dibeli", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If tjenis.EditValue = "" Then
            MsgBox("Asal Barang harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tjenis.Focus()
            Return
        End If

        If tkd_supir.EditValue = "" Then
            MsgBox("Sales harus diisi..", vbOKOnly + vbInformation, "Informasi")
            tkd_supir.Focus()
            Return
        End If

        If MsgBox("Yakin sudah benar.. ???", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.No Then
            Return
        Else
            simpan()
        End If

    End Sub

    Private Sub ttgl_tempo_Validated(sender As Object, e As EventArgs) Handles ttgl_tempo.Validated

        If IsDate(ttgl_tempo.EditValue) Then

            Dim cn As OleDbConnection = Nothing
            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim nopol As String = ""
                If tjenis.EditValue = "TO" Then
                    nopol = cek_nopolfak(cn)
                Else
                    cek_nopolspm(cn)

                    If dtspm.Rows.Count > 0 Then
                        nopol = dtspm(0)("nopol").ToString
                    End If

                End If

                If nopol.Length = 0 Then
                    nopol = "BE XXXX XX"
                End If

                tnopol.EditValue = nopol

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


End Class