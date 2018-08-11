Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class ffaktur_kv

    Private bs1 As BindingSource
    Private ds As DataSet
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager_spm As Data.DataViewManager
    Private dv_spm As Data.DataView

    Private isloadform As Boolean = False
    Private cnopol As String

    Private Sub opendata()

        Dim sql As String = String.Format("SELECT     trfaktur_to.nobukti, trfaktur_to.tanggal, trfaktur_to.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, trfaktur_to.kd_karyawan, ms_pegawai.nama_karyawan," & _
            "trfaktur_to.ket, trfaktur_to.netto, trfaktur_to.skirim, trfaktur_to.spulang, trfaktur_to.sbatal,trfaktur_to.no_nota,trfaktur_to.statkirim " & _
            "FROM         trfaktur_to INNER JOIN " & _
            "ms_pegawai ON trfaktur_to.kd_karyawan = ms_pegawai.kd_karyawan INNER JOIN " & _
            "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko where trfaktur_to.jnis_fak='K' and trfaktur_to.tanggal >='{0}' and  trfaktur_to.tanggal <='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))


        Dim cn As OleDbConnection = Nothing

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

            bs1 = New BindingSource
            bs1.DataSource = dv1
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

    End Sub

    Private Sub cari()

        'bs1.DataSource = Nothing
        grid1.DataSource = Nothing
        Dim cn As OleDbConnection = Nothing

        Dim sql As String = ""
        Dim scbo As Integer = tcbofind.SelectedIndex

        sql = String.Format("SELECT     trfaktur_to.nobukti, trfaktur_to.tanggal, trfaktur_to.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, trfaktur_to.kd_karyawan, ms_pegawai.nama_karyawan," & _
            "trfaktur_to.ket, trfaktur_to.netto, trfaktur_to.skirim, trfaktur_to.spulang, trfaktur_to.sbatal,trfaktur_to.no_nota,trfaktur_to.statkirim " & _
            "FROM         trfaktur_to INNER JOIN " & _
            "ms_pegawai ON trfaktur_to.kd_karyawan = ms_pegawai.kd_karyawan INNER JOIN " & _
            "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko where trfaktur_to.jnis_fak='K' and trfaktur_to.tanggal >='{0}' and  trfaktur_to.tanggal <='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))

        Select Case tcbofind.SelectedIndex
            Case 0 ' kode
                sql = String.Format("{0} and trfaktur_to.nobukti like '%{1}%'", sql, tfind.Text.Trim)
            Case 1 ' no nota
                sql = String.Format("{0} and trfaktur_to.no_nota like '%{1}%'", sql, tfind.Text.Trim)
            Case 2 ' tgl

                If Not IsDate(tfind.Text.Trim) Then
                    MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                    Return
                Else

                    If tfind.Text.Trim.Length < 10 Or tfind.Text.Trim.Length > 10 Then
                        MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                        Return
                    End If

                End If

                sql = String.Format("{0} and trfaktur_to.tanggal='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))
            Case 3 ' sales
                sql = String.Format("{0} and ms_pegawai.nama_karyawan like '%{1}%'", sql, tfind.Text.Trim)
            Case 4 ' outlet
                sql = String.Format("{0} and ms_toko.nama_toko like '%{1}%'", sql, tfind.Text.Trim)
        End Select

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
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

    Private Sub open_spm(ByVal nobukti As String)

        Dim sql As String = String.Format("SELECT     trspm.nobukti, trspm.tglberangkat, trspm.nopol, supir.nama_karyawan AS supir, trspm.note, kenek1.nama_karyawan AS kenek1, kenek2.nama_karyawan AS kenek2, " & _
                      "kenek3.nama_karyawan AS kenek3 " & _
            "FROM         ms_pegawai AS supir INNER JOIN " & _
                      "trspm ON supir.kd_karyawan = trspm.kd_supir INNER JOIN " & _
                      "trfaktur_to4 ON trspm.nobukti = trfaktur_to4.nobukti_spm LEFT OUTER JOIN " & _
                      "ms_pegawai AS kenek3 ON trspm.kd_kenek3 = kenek3.nama_karyawan LEFT OUTER JOIN " & _
                      "ms_pegawai AS kenek2 ON trspm.kd_kenek2 = kenek2.kd_karyawan LEFT OUTER JOIN " & _
                      "ms_pegawai AS kenek1 ON trspm.kd_kenek1 = kenek1.kd_karyawan " & _
                    "WHERE trfaktur_to4.nobukti='{0}'", nobukti)


        Dim cn As OleDbConnection = Nothing

        grid_spm.DataSource = Nothing

        Try

            dv_spm = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim dsspm As DataSet
            dsspm = New DataSet()
            dsspm = Clsmy.GetDataSet(sql, cn)

            dvmanager_spm = New DataViewManager(dsspm)
            dv_spm = dvmanager_spm.CreateDataView(dsspm.Tables(0))

            grid_spm.DataSource = dv_spm

        Catch ex As OleDb.OleDbException
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

        Dim sql As String = String.Format("update trfaktur_to set sbatal=1,alasan_batal='{0}' where nobukti='{1}'", alasan.ToUpper, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString)
        Dim sqltoko As String = String.Format("update ms_toko set piutangbeli=piutangbeli - {0} where kd_toko='{1}'", Replace(dv1(bs1.Position)("netto").ToString, ",", "."), dv1(bs1.Position)("kd_toko").ToString)

        Dim sqluptoko21 As String = String.Format("update ms_toko2 set jmlpiutang=jmlpiutang - {0} where kd_toko='{1}' and kd_karyawan='{2}'", Replace(dv1(bs1.Position)("netto").ToString, ",", "."), dv1(bs1.Position)("kd_toko").ToString, dv1(bs1.Position)("kd_karyawan").ToString)

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

            cmdtoko = New OleDbCommand(sqltoko, cn, sqltrans)
            cmdtoko.ExecuteNonQuery()

            Using cmdtoko2 As OleDbCommand = New OleDbCommand(sqluptoko21, cn, sqltrans)
                cmdtoko2.ExecuteNonQuery()
            End Using

            If Not cek_beli_pinjam_gallon(cn, sqltrans) = "ok" Then
                GoTo langsung_out
            End If

            If hapus_detail(cn, sqltrans) = True Then

                If hapus_detail3(cn, sqltrans) = False Then
                    GoTo langsung_out
                End If
            Else
                GoTo langsung_out
            End If

            If hapus_retur(cn, sqltrans) = False Then
                GoTo langsung_out
            End If

            Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, dv1(bs1.Position)("nobukti").ToString, dv1(bs1.Position)("nobukti").ToString, dv1(bs1.Position)("tanggal").ToString, dv1(bs1.Position)("kd_toko").ToString, 0, dv1(bs1.Position)("netto").ToString, "Jual (Batal)")

            Clsmy.InsertToLog(cn, "btfaktur_kv", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString, dv1(Me.BindingContext(bs1).Position)("tanggal").ToString, sqltrans)

            sqltrans.Commit()

            dv1(bs1.Position)("sbatal") = 1

            close_wait()

            MsgBox("Data telah dibatalkan...", vbOKOnly + vbInformation, "Informasi")


langsung_out:

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

    Private Function ceksupir(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal nobukti As String) As String

        cnopol = ""

        Dim sql As String = String.Format("select trspm.kd_supir,trspm.nopol from trfaktur_to4 inner join trspm on trfaktur_to4.nobukti_spm=trspm.nobukti where trfaktur_to4.nobukti='{0}'", nobukti)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim kode As String = "-"

        If drd.Read Then
            If Not drd(0).ToString.Equals("") Then
                kode = drd(0).ToString
                cnopol = drd(1).ToString
            End If
        End If
        drd.Close()

        Return kode

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

    Private Function cek_beli_pinjam_gallon(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim hasil As String = "ok"

        Dim jmljual As Integer = 0
        Dim jmlpinjam As Integer = 0
        Dim jmlbalik As Integer = 0

        Dim qty1, qty2, qty3 As Integer
        Dim kdbarang_kembali As String = ""


        Dim kdtoko As String = dv1(Me.BindingContext(bs1).Position)("kd_toko").ToString
        Dim tanggal As String = dv1(Me.BindingContext(bs1).Position)("tanggal").ToString
        Dim nofaktur As String = dv1(Me.BindingContext(bs1).Position)("nobukti").ToString

        Dim kdsup As String = ceksupir(cn, sqltrans, nofaktur)

        Dim sql2 As String = String.Format("select trfaktur_to2.jenis_trans,SUM(trfaktur_to2.qtykecil) as jml from trfaktur_to2 inner join ms_barang " & _
        "on trfaktur_to2.kd_barang=ms_barang.kd_barang " & _
        "where not(ms_barang.jenis='FISIK') and trfaktur_to2.nobukti='{0}' " & _
        "group by trfaktur_to2.jenis_trans", nofaktur)

        Dim sql3 As String = String.Format("select trfaktur_to3.qtykecil,trfaktur_to3.kd_barang,ms_barang.qty1,ms_barang.qty2,ms_barang.qty3 from trfaktur_to3 inner join ms_barang on trfaktur_to3.kd_barang=ms_barang.kd_barang " & _
        "where trfaktur_to3.nobukti='{0}'", nofaktur)

        Dim cmd2 As OleDbCommand = New OleDbCommand(sql2, cn, sqltrans)
        Dim drd2 As OleDbDataReader = cmd2.ExecuteReader
        While drd2.Read

            If drd2("jenis_trans").ToString.Equals("JUAL") Then
                jmljual = Integer.Parse(drd2("jml").ToString)
            ElseIf drd2("jenis_trans").ToString.Equals("PINJAM") Then
                jmlpinjam = Integer.Parse(drd2("jml").ToString)
            End If

        End While
        drd2.Close()

        Dim cmd3 As OleDbCommand = New OleDbCommand(sql3, cn, sqltrans)
        Dim drd3 As OleDbDataReader = cmd3.ExecuteReader
        If drd3.Read Then
            If IsNumeric(drd3(0).ToString) Then
                jmlbalik = Integer.Parse(drd3(0).ToString)
                kdbarang_kembali = drd3(1).ToString
                qty1 = drd3(2).ToString
                qty2 = drd3(3).ToString
                qty3 = drd3(4).ToString
            End If
        End If
        drd3.Close()

        Dim selgln_ksong As Integer = jmljual - jmlpinjam - jmlbalik
        If selgln_ksong > 0 Then

            Dim hsilsimkos As String = simpankosong(cn, sqltrans, kdbarang_kembali, kdtoko, qty1, qty2, qty3, selgln_ksong, False)
            If Not hsilsimkos.Equals("ok") Then
                close_wait()

                If Not IsNothing(sqltrans) Then
                    sqltrans.Rollback()
                End If

                MsgBox(hsilsimkos, vbOKOnly + vbExclamation, "Informasi")
                hasil = "error"
                GoTo exit_
            End If

        End If

        If jmljual - jmlpinjam <> 0 Then
            Clsmy.Insert_HistBarang(cn, sqltrans, nofaktur, tanggal, "None B", kdbarang_kembali, 0, jmljual - jmlpinjam, "Jual Kanvas (Batal)", kdsup, cnopol)
        End If

        If jmlbalik <> 0 Then
            Clsmy.Insert_HistBarang(cn, sqltrans, nofaktur, tanggal, "None B", kdbarang_kembali, jmlbalik, 0, "Jual Kanvas (Batal)", kdsup, cnopol)
        End If

        If jmlpinjam > 0 Then

            Dim hsilpinjm As String = Hist_PinjamSewa_Toko(kdtoko, kdbarang_kembali, jmlpinjam, cn, sqltrans, False, True)
            If Not hsilpinjm.Equals("ok") Then
                close_wait()

                If Not IsNothing(sqltrans) Then
                    sqltrans.Rollback()
                End If

                MsgBox(hsilpinjm, vbOKOnly + vbExclamation, "Informasi")
                hasil = "error"
                GoTo exit_
            End If

            Clsmy.Insert_HistBarang(cn, sqltrans, nofaktur, tanggal, "None P", kdbarang_kembali, 0, jmlpinjam, "Jual Kanvas (Batal)", kdsup, cnopol)

        End If

exit_:

        Return hasil
    End Function


    Private Function hapus_detail(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As Boolean

        Dim hasil As Boolean = True

        Dim nobukti As String = dv1(Me.BindingContext(bs1).Position)("nobukti").ToString
        Dim tanggal As String = dv1(Me.BindingContext(bs1).Position)("tanggal").ToString

        Dim kdtoko As String = dv1(Me.BindingContext(bs1).Position)("kd_toko").ToString

        Dim kdsup As String = ceksupir(cn, sqltrans, nobukti)

        Dim sql As String = String.Format("select * from trfaktur_to2 inner join ms_barang on trfaktur_to2.kd_barang=ms_barang.kd_barang where trfaktur_to2.nobukti='{0}'", nobukti)

        Dim comd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = comd.ExecuteReader

        If drd.HasRows Then
            While drd.Read

                Dim qtykecil As Integer = Integer.Parse(drd("qtykecil").ToString)
                Dim kdbar As String = drd("kd_barang").ToString
                Dim kdgud As String = drd("kd_gudang").ToString
                Dim jenis As String = drd("jenis").ToString
                Dim jenis_trans As String = drd("jenis_trans").ToString

                Dim qty1 As Integer = Integer.Parse(drd("qty1").ToString)
                Dim qty2 As Integer = Integer.Parse(drd("qty2").ToString)
                Dim qty3 As Integer = Integer.Parse(drd("qty3").ToString)

                If IsNumeric(drd("noid").ToString) Then

                    If cek_retur(cn, sqltrans, drd("nobukti").ToString) = True Then

                        close_wait()
                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(String.Format("No bukti {0} ada barang retur,perbaiki dulu..", drd("nobukti").ToString), vbOKOnly + vbInformation, "Informasi")
                        hasil = False
                        Exit While

                    End If

                    If jenis = "FISIK" And jenis_trans.Equals("JUAL") Then

                        Dim hsilsimkos As String = simpankosong(cn, sqltrans, kdbar, kdtoko, qty1, qty2, qty3, qtykecil, False)
                        If Not hsilsimkos.Equals("ok") Then
                            close_wait()

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox(hsilsimkos, vbOKOnly + vbExclamation, "Informasi")
                            hasil = False

                            Exit While
                        End If

                        '2. update barang
                        Dim hasilplusmin As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, kdgud, True, False, False)
                        If Not hasilplusmin.Equals("ok") Then

                            close_wait()

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                            hasil = False

                            Exit While
                        End If



                        ''3. insert to hist stok
                        Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tanggal, kdgud, kdbar, qtykecil, 0, "Jual Kanvas (Batal)", kdsup, cnopol)

                    End If

                End If

            End While
        End If

        drd.Close()

        Return hasil

    End Function

    Private Function hapus_detail3(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As Boolean

        Dim hasil As Boolean = True

        If dv1(bs1.Position)("spulang").ToString.Equals("0") Then
            hasil = True
            GoTo langsung_keluar
        End If

        Dim nobukti As String = dv1(Me.BindingContext(bs1).Position)("nobukti").ToString
        Dim tanggal As String = dv1(Me.BindingContext(bs1).Position)("tanggal").ToString
        Dim kdtoko As String = dv1(Me.BindingContext(bs1).Position)("kd_toko").ToString

        Dim kdsup As String = ceksupir(cn, sqltrans, nobukti)

        Dim sql As String = String.Format("select * from trfaktur_to3 inner join ms_barang on trfaktur_to3.kd_barang=ms_barang.kd_barang where trfaktur_to3.nobukti='{0}'", nobukti)

        Dim comd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = comd.ExecuteReader

        If drd.HasRows Then
            While drd.Read

                Dim qtykecil As Integer = Integer.Parse(drd("qtykecil").ToString)
                Dim kdbar As String = drd("kd_barang").ToString
                Dim kdgud As String = drd("kd_gudang").ToString
                Dim jenis As String = drd("jenis").ToString
                Dim satuan As String = drd("satuan").ToString

                Dim qty1 As Integer = Integer.Parse(drd("qty1").ToString)
                Dim qty2 As Integer = Integer.Parse(drd("qty2").ToString)
                Dim qty3 As Integer = Integer.Parse(drd("qty3").ToString)

                If IsNumeric(drd("noid").ToString) Then

                    ' cek apakah barang kosong
                    Dim sqlcekapa As String = String.Format("select * from trfaktur_to2 where kd_barang in (select kd_barang from ms_barang where kd_barang_kmb='{0}') and nobukti='{1}' and satuan='{2}'", kdbar, nobukti, satuan)
                    Dim cmdcekapa As OleDbCommand = New OleDbCommand(sqlcekapa, cn, sqltrans)
                    Dim drapa As OleDbDataReader = cmdcekapa.ExecuteReader

                    If drapa.Read Then
                        If Not drapa("nobukti").ToString.Equals("") Then

                            Dim qty2old As Integer = Integer.Parse(drapa("qtykecil").ToString)
                            'Dim qty22old As Integer = qty2old

                            'If qty2old > qtykecil Then
                            '    qty2old = qty2old - qtykecil
                            'ElseIf qty2old = qtykecil Then
                            '    qty2old = 0
                            'End If

                            'Dim hsilsimkos As String = simpankosong(cn, sqltrans, kdbar, kdtoko, qty1, qty2, qty3, qty2old, False)
                            'If Not hsilsimkos.Equals("ok") Then

                            '    close_wait()

                            '    If Not IsNothing(sqltrans) Then
                            '        sqltrans.Rollback()
                            '    End If

                            '    MsgBox(hsilsimkos, vbOKOnly + vbExclamation, "Informasi")
                            '    hasil = "error"
                            '    Exit While

                            'End If

                            If Not kdgud.Equals("None") Then
                                '2. update barang
                                Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, False, False, False)
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

                            'If qty2old <> 0 Then
                            '    '3. insert to hist stok
                            '    Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tanggal, kdgud, kdbar, 0, qty22old, "Jual Kanvas (Batal)", kdsup, cnopol)
                            '    Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tanggal, kdgud, kdbar, qtykecil, 0, "Jual Kanvas (Batal)", kdsup, cnopol)

                            '    ' Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tanggal, kdgud, kdbar, 0, qtykecil, "Jual Kanvas (Batal)")

                            'End If


                        End If
                    End If
                    drapa.Close()

                    'If Not kdgud = "None" Then

                    '    '2. update barang
                    '    Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, False, False, False)
                    '    If Not hasilplusmin.Equals("ok") Then
                    '        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                    '        hasil = False
                    '        close_wait()
                    '        Exit While
                    '    End If

                    '    ''3. insert to hist stok
                    '    Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tanggal, kdgud, kdbar, 0, qtykecil, "Jual Kanvas (Batal)")

                    'End If

                End If

            End While
        End If

        drd.Close()

langsung_keluar:

        Return hasil

    End Function

    Private Function hapus_retur(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As Boolean

        Dim hasil As Boolean = True

        Dim nobukti As String = dv1(Me.BindingContext(bs1).Position)("nobukti").ToString
        Dim tanggal As String = dv1(Me.BindingContext(bs1).Position)("tanggal").ToString

        Dim kdtoko As String = dv1(Me.BindingContext(bs1).Position)("kd_toko").ToString

        Dim kdsup As String = ceksupir(cn, sqltrans, nobukti)

        Dim sql As String = String.Format("SELECT     trfaktur_to5.noid, trfaktur_to5.kd_gudang, ms_barang.kd_barang, ms_barang.nama_barang, trfaktur_to5.qty, trfaktur_to5.satuan, trfaktur_to5.harga, " & _
       "trfaktur_to5.disc_per, trfaktur_to5.disc_rp, trfaktur_to5.jumlah, trfaktur_to5.qtykecil,ms_barang.qty1,ms_barang.qty2,ms_barang.qty3 " & _
       "FROM         trfaktur_to5 INNER JOIN ms_barang ON trfaktur_to5.kd_barang = ms_barang.kd_barang " & _
       "WHERE trfaktur_to5.nobukti='{0}'", nobukti)

        Dim cmds As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drds As OleDbDataReader = cmds.ExecuteReader

        While drds.Read

            Dim kdgud As String = drds("kd_gudang").ToString
            Dim kdbar As String = drds("kd_barang").ToString
            Dim qty As String = drds("qty").ToString
            Dim satuan As String = drds("satuan").ToString
            Dim harga As String = drds("harga").ToString
            Dim disc_per As String = drds("disc_per").ToString
            Dim disc_rp As String = drds("disc_rp").ToString
            Dim jumlah As String = drds("jumlah").ToString
            Dim qtykecil As String = drds("qtykecil").ToString
            Dim noid As String = drds("noid").ToString
            Dim qty1 As String = drds("qty1").ToString
            Dim qty2 As String = drds("qty2").ToString
            Dim qty3 As String = drds("qty3").ToString

            Dim hsilsimkos As String = simpankosong(cn, sqltrans, kdbar, kdtoko, qty1, qty2, qty3, qtykecil, True)
            If Not hsilsimkos.Equals("ok") Then
                If Not IsNothing(sqltrans) Then
                    sqltrans.Rollback()
                End If

                hasil = False
                MsgBox(hsilsimkos, vbOKOnly + vbExclamation, "Informasi")
                Return hasil
            End If


            Dim sqlcek_kembali As String = String.Format("select kd_barang from ms_barang where kd_barang_kmb='{0}'", kdbar)
            Dim cmdcek_kembali As OleDbCommand = New OleDbCommand(sqlcek_kembali, cn, sqltrans)
            Dim drdcek_kembali As OleDbDataReader = cmdcek_kembali.ExecuteReader

            Dim apakah_kembali As Boolean = False
            If drdcek_kembali.Read Then
                If Not drdcek_kembali(0).ToString.Equals("") Then
                    apakah_kembali = True
                End If
            End If
            drdcek_kembali.Close()

            If apakah_kembali = False Then

                '2. update barang
                Dim hasilplusmin As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, kdgud, True, False, False)
                If Not hasilplusmin.Equals("ok") Then

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    hasil = False
                    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                    Return hasil
                End If

                '3. insert to hist stok
                Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tanggal, kdgud, kdbar, qtykecil, 0, "Jual Kanvas (Retur-Batal)", kdsup, cnopol)

            End If

        End While
        drds.Close()

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
        Else
            tsprint.Enabled = False
        End If

    End Sub

    Private Function cek_SudahBayar(ByVal nobukti As String) As Boolean

        Dim hasil As Boolean = False
        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select jmlbayar,jmlgiro,jmlgiro_real,jmlkelebihan_pot from trfaktur_to where nobukti='{0}'", nobukti)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then

                Dim jmlbayar As String = drd(0).ToString
                Dim jmlgiro As String = drd(1).ToString
                Dim jmlgiro_real As String = drd(2).ToString
                Dim kelebihan_pot As String = drd(3).ToString

                If jmlbayar = "" Then
                    jmlbayar = 0
                End If

                If jmlgiro = "" Then
                    jmlgiro = 0
                End If

                If jmlgiro_real = "" Then
                    jmlgiro_real = 0
                End If

                If kelebihan_pot = "" Then
                    kelebihan_pot = 0
                End If

                Dim total As Double = Double.Parse(jmlbayar) + Double.Parse(jmlgiro) + Double.Parse(jmlgiro_real) + Double.Parse(kelebihan_pot)

                If total > 0 Then
                    hasil = True
                Else
                    hasil = False
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

        Return hasil

    End Function

    Private Sub cek_stat2(ByVal nobukti As String)

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select nobukti,skirim,spulang,sbatal,statkirim from trfaktur_to where nobukti='{0}'", nobukti)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Not drd(0).ToString.Equals("") Then
                    dv1(bs1.Position)("skirim") = drd("skirim").ToString
                    dv1(bs1.Position)("spulang") = drd("spulang").ToString
                    dv1(bs1.Position)("sbatal") = drd("sbatal").ToString
                    '  dv1(bs1.Position)("statkirim") = drd("statkirim").ToString
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

    Private Function cektoko_boleh() As Boolean

        Dim hasil = True
        If ins_alltokouser = 1 Then
            Return hasil
        End If


        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select id from ms_usersys4 where kd_toko='{0}' and nama_user='{1}'", dv1(bs1.Position)("kd_toko").ToString, userprog)
            Dim cmdc As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drdc As OleDbDataReader = cmdc.ExecuteReader

            If drdc.Read Then
                If IsNumeric(drdc(0).ToString) Then
                    If Integer.Parse(drdc(0).ToString) > 0 Then
                        hasil = True
                    Else
                        hasil = False
                    End If
                Else
                    hasil = False
                End If
            Else
                hasil = False
            End If
            drdc.Close()

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

        Return hasil
    End Function



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
        tfind.Text = ""
        opendata()
    End Sub

    Private Sub tsdel_Click(sender As System.Object, e As System.EventArgs) Handles tsdel.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If


        If cek_SudahBayar(dv1(bs1.Position)("nobukti").ToString) = True Then
            MsgBox("Faktur sudah dibayar", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        cek_stat2(dv1(bs1.Position)("nobukti").ToString)

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Faktur telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If MsgBox("Yakin akan dibatalkan ?", vbYesNo + vbQuestion, "Konfirmasi") = vbNo Then
            Exit Sub
        End If

        hapus()

    End Sub

    Private Sub tsadd_Click(sender As System.Object, e As System.EventArgs) Handles tsadd.Click

        isloadform = True

        Using fkar2 As New ffaktur_to2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0, .jenisjual = "K", .spulang = True}
            fkar2.ShowDialog()
            isloadform = False
        End Using
    End Sub

    Private Sub tsedit_Click(sender As System.Object, e As System.EventArgs) Handles tsedit.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        If cek_SudahBayar(dv1(bs1.Position)("nobukti").ToString) = True Then
            MsgBox("Faktur sudah dibayar", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Faktur telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If cektoko_boleh() = False Then
            MsgBox("Anda tidak berhak merubah data di transaksi ini (bukan toko yang berhak diakses)", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        isloadform = True

        Using fkar2 As New ffaktur_to2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position, .jenisjual = "K", .spulang = True}
            fkar2.ShowDialog()
            isloadform = False

        End Using

    End Sub

    Private Sub tsview_Click(sender As System.Object, e As System.EventArgs) Handles tsview.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        Using fkar2 As New ffaktur_to2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position, .jenisjual = "K"}
            fkar2.btsimpan.Enabled = False
            fkar2.btadd.Enabled = False
            fkar2.btedit.Enabled = False
            fkar2.btdel.Enabled = False
            'fkar2.btadd_ret.Enabled = False
            'fkar2.btedit_ret.Enabled = False
            fkar2.btdel_ret.Enabled = False
            fkar2.ShowDialog()
        End Using

    End Sub

    Private Sub tsprint_Click(sender As System.Object, e As System.EventArgs) Handles tsprint.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If


        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Faktur telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If


        Dim nobukti As String = dv1(bs1.Position)("nobukti").ToString

        Dim sql1 As String = String.Format("SELECT     trfaktur_to.nobukti, trfaktur_to.tanggal, trfaktur_to.tgl_tempo, ms_toko.kd_toko +' - ' + ms_toko.nama_toko as nama_toko, ms_toko.alamat_toko, ms_pegawai.nama_karyawan, trfaktur_to.jnis_jual, " & _
                      "trfaktur_to.brutto - trfaktur_to.jmlkembali AS brutto, trfaktur_to.disc_rp AS disc_tot, trfaktur_to.netto,trfaktur_to.ket " & _
                    "FROM         trfaktur_to INNER JOIN " & _
                      "ms_pegawai ON trfaktur_to.kd_karyawan = ms_pegawai.kd_karyawan INNER JOIN " & _
                      "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko " & _
                    "WHERE     (trfaktur_to.sbatal = 0) AND (trfaktur_to.nobukti = '{0}')", nobukti)

        Dim sql2 As String = String.Format("SELECT ms_barang.nama_barang, trfaktur_to3.qty, trfaktur_to3.satuan, trfaktur_to3.harga, trfaktur_to3.jumlah, trfaktur_to3.nobukti " & _
        "FROM   trfaktur_to3 INNER JOIN " & _
        "ms_barang ON trfaktur_to3.kd_barang = ms_barang.kd_barang WHERE trfaktur_to3.nobukti='{0}'", nobukti)

        Dim sql3 As String = "select kd_barang,nama_barang,nohrus,0 as qty,'' as satuan,0 as harga,0 as disc,0 as jumlah from ms_barang where hrusfaktur=1"

        Dim sqldetail As String = String.Format("select * from trfaktur_to2 where nobukti='{0}'", nobukti)

        Using fkar2 As New fpr_invoice2 With {.sql1 = sql1, .sql2 = sql2, .sql3 = sql3, .sqldetail = sqldetail}
            fkar2.ShowDialog(Me)
        End Using


    End Sub

    Private Sub GridView1_Click(sender As Object, e As System.EventArgs) Handles GridView1.Click
        GridView1_FocusedRowChanged(sender, Nothing)
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged

        If isloadform Then
            Return
        End If

        If IsNothing(dv1) Then
            open_spm("")
            Return
        End If

        If dv1.Count <= 0 Then
            open_spm("")
            Return
        End If

        open_spm(dv1(bs1.Position)("nobukti").ToString)


    End Sub

 
End Class