Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fverif_supirkenek

    Private dv_supir As DataView
    Private dv_kenek1 As DataView
    Private dv_kenek2 As DataView
    Private dv_kenek3 As DataView

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager_tk As Data.DataViewManager
    Private dv_tk As Data.DataView

    Private statedit As Boolean

    Private Sub load_data()

        dv1 = Nothing
        grid1.DataSource = Nothing

        Dim sql As String = ""

        If cbjenis.SelectedIndex = 0 Then

            sql = "SELECT     trrekap_to.nobukti, supir.kd_karyawan AS kd_supir,case when len(trrekap_to.kd_supir)=0 then '' else supir.kd_karyawan end as nama_supir,kenek1.kd_karyawan AS kd_kenek1, " & _
                      "case when len(trrekap_to.kd_kenek1)=0 then '' else kenek1.kd_karyawan end AS nama_kenek1, kenek2.kd_karyawan AS kd_kenek2,case when len(trrekap_to.kd_kenek2)=0 then '' else kenek2.kd_karyawan end AS nama_kenek2, kenek3.kd_karyawan AS kd_kenek3, " & _
                      "case when len(trrekap_to.kd_kenek3)=0 then '' else kenek3.kd_karyawan end AS nama_kenek3, trrekap_to.nopol, trrekap_to.tot_nota, " & _
                      "supir.kd_karyawan as kd_supir0,kenek1.kd_karyawan as kd_kenek10,kenek2.kd_karyawan as kd_kenek20,kenek3.kd_karyawan as kd_kenek30,trrekap_to.nopol as nopol0,'01.TO' AS jenisfak, " & _
                      "trrekap_to.tglmuat,trrekap_to.tglkirim,trrekap_to.sfaktur_kosong,trfaktur_balik.nobukti as nobukti_real " & _
                      "FROM         ms_pegawai AS kenek3 RIGHT OUTER JOIN " & _
                      "trrekap_to ON kenek3.kd_karyawan = trrekap_to.kd_kenek3 LEFT OUTER JOIN " & _
                      "ms_pegawai AS kenek2 ON trrekap_to.kd_kenek2 = kenek2.kd_karyawan LEFT OUTER JOIN " & _
                      "ms_pegawai AS kenek1 ON trrekap_to.kd_kenek1 = kenek1.kd_karyawan LEFT OUTER JOIN " & _
                      "ms_pegawai AS supir ON trrekap_to.kd_supir = supir.kd_karyawan LEFT OUTER JOIN " & _
                      "trfaktur_balik ON trrekap_to.nobukti=trfaktur_balik.nobukti_rkp " & _
                      "WHERE trrekap_to.sbatal = 0 And trrekap_to.spulang = 1 and trfaktur_balik.sbatal=0 "

            If cb1.SelectedIndex = 0 Then
                sql = String.Format(" {0} and trrekap_to.tglmuat>='{1}' and trrekap_to.tglmuat<='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))
            Else
                sql = String.Format(" {0} and trrekap_to.tglkirim>='{1}' and  trrekap_to.tglkirim<='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))
            End If

        Else

            sql = String.Format(" {0} SELECT     trspm.nobukti, supir.kd_karyawan AS kd_supir, case when len(trspm.kd_supir)=0 then '' else supir.kd_karyawan end AS nama_supir, kenek1.kd_karyawan AS kd_kenek1,case when len(trspm.kd_kenek1)=0 then '' else kenek1.kd_karyawan end AS nama_kenek1, " & _
                      "kenek2.kd_karyawan AS kd_kenek2,case when len(trspm.kd_kenek2)=0 then '' else kenek2.kd_karyawan end AS nama_kenek2, kenek3.kd_karyawan AS kd_kenek3,case when len(trspm.kd_kenek3)=0 then '' else kenek3.kd_karyawan end AS nama_kenek3, " & _
                      "trspm.nopol, 0 AS tot_nota, supir.kd_karyawan AS kd_supir0, kenek1.kd_karyawan AS kd_kenek10, kenek2.kd_karyawan AS kd_kenek20, " & _
                      "kenek3.kd_karyawan AS kd_kenek30,trspm.nopol as nopol0, '02.KANVAS'  AS jenisfak,trspm.tglmuat,trspm.tglberangkat as tglkirim,0 as sfaktur_kosong,'' as nobukti_real " & _
                    "FROM         ms_pegawai AS kenek3 RIGHT OUTER JOIN " & _
                      "trspm ON kenek3.kd_karyawan = trspm.kd_kenek3 LEFT OUTER JOIN " & _
                      "ms_pegawai AS kenek2 ON trspm.kd_kenek2 = kenek2.kd_karyawan LEFT OUTER JOIN " & _
                      "ms_pegawai AS kenek1 ON trspm.kd_kenek1 = kenek1.kd_karyawan LEFT OUTER JOIN " & _
                      "ms_pegawai AS supir ON trspm.kd_supir = supir.kd_karyawan " & _
                    "WHERE trspm.sbatal = 0 And trspm.spulang = 1", sql)

            If cb1.SelectedIndex = 0 Then
                sql = String.Format(" {0} and trspm.tglmuat>='{1}' and trspm.tglmuat<='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))
            Else
                sql = String.Format(" {0} and trspm.tglberangkat>='{1}' and trspm.tglberangkat<='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))
            End If

        End If

        Dim cn As OleDbConnection = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            grid1.DataSource = dv1

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

        If dv1.Count > 0 Then
            GridView1.MoveFirst()
        End If

    End Sub

    Private Sub opendata_tk()

        If statedit = True Then
            Return
        End If

        grid2.DataSource = Nothing
        dv_tk = Nothing

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim sql As String = ""

        If dv1(Me.BindingContext(dv1).Position)("jenisfak").ToString.Equals("01.TO") Then
            sql = String.Format("select trfaktur_to.nobukti,ms_toko.nama_toko,ms_toko.alamat_toko,trrekap_to2.statkirim,ms_pegawai.nama_karyawan,trfaktur_to.netto,trfaktur_to.jmlkelebihan " & _
                "from trrekap_to2 " & _
                "inner join trfaktur_to on trrekap_to2.nobukti_fak=trfaktur_to.nobukti " & _
                "inner join ms_toko on trfaktur_to.kd_toko=ms_toko.kd_toko " & _
                "inner join ms_pegawai on trfaktur_to.kd_karyawan=ms_pegawai.kd_karyawan " & _
                "where trfaktur_to.sbatal=0 and  trrekap_to2.nobukti='{0}'", dv1(Me.BindingContext(dv1).Position)("nobukti").ToString)
        Else
            sql = String.Format("SELECT     trfaktur_to.nobukti, ms_toko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, trfaktur_to.statkirim, ms_pegawai.nama_karyawan,trfaktur_to.netto,trfaktur_to.jmlkelebihan " & _
                "FROM         trfaktur_to INNER JOIN " & _
                "trfaktur_to4 ON trfaktur_to.nobukti = trfaktur_to4.nobukti INNER JOIN " & _
                "ms_pegawai ON trfaktur_to.kd_karyawan = ms_pegawai.kd_karyawan INNER JOIN " & _
                "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko " & _
                "WHERE trfaktur_to.sbatal=0 and trfaktur_to4.nobukti_spm='{0}'", dv1(Me.BindingContext(dv1).Position)("nobukti").ToString)
        End If

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

    Private Sub isi_nopol()

        Const sql As String = "select * from ms_kendaraan where aktif=1"

        Dim cn As OleDbConnection = Nothing

        Try

            cn = Clsmy.open_conn

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            While drd.Read

                cbnopol.Items.Add(drd("nopol").ToString)

            End While
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

    Private Sub isi_supir()

        dv_supir = Nothing

        Const sql As String = "select kd_karyawan,nama_karyawan from ms_pegawai where bagian in ('SALES','SUPIR') and aktif=1 order by kd_karyawan asc"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        Try
            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dv_supir = dvm.CreateDataView(ds.Tables(0))

            Dim orow As DataRow = dv_supir.Table.NewRow
            orow("kd_karyawan") = ""
            orow("nama_karyawan") = ""
            dv_supir.Table.Rows.InsertAt(orow, 0)

            rnama_supir.DataSource = dv_supir

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

    Private Sub isi_kenek()

        dv_kenek1 = Nothing
        dv_kenek2 = Nothing
        dv_kenek3 = Nothing

        Const sql As String = "select kd_karyawan,nama_karyawan from ms_pegawai where bagian in ('KENEK') and aktif=1 order by kd_karyawan asc"

        Dim cn As OleDbConnection = Nothing


        Try

            cn = Clsmy.open_conn

            Dim ds As DataSet = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim orow As DataRow = ds.Tables(0).NewRow
            orow("kd_karyawan") = ""
            orow("nama_karyawan") = ""
            ds.Tables(0).Rows.InsertAt(orow, 0)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dv_kenek1 = dvm.CreateDataView(ds.Tables(0))
            rnama_kenek1.DataSource = dv_kenek1

            Dim dvm2 As DataViewManager = New DataViewManager(ds)
            dv_kenek2 = dvm2.CreateDataView(ds.Tables(0))
            rnama_kenek2.DataSource = dv_kenek2

            Dim dvm3 As DataViewManager = New DataViewManager(ds)
            dv_kenek3 = dvm3.CreateDataView(ds.Tables(0))
            rnama_kenek3.DataSource = dv_kenek3

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

    Private Function cek_kodesupir(ByVal kd_karyawan As String) As String

        Dim hasil As String = ""

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select nama_karyawan from ms_pegawai where bagian in ('SUPIR','SALES') and aktif=1 and kd_karyawan='{0}'", kd_karyawan)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Not drd(0).ToString.Equals("") Then
                    hasil = drd(0).ToString
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

    Private Function cek_kodekenek(ByVal kd_karyawan As String) As String

        Dim hasil As String = ""

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select nama_karyawan from ms_pegawai where bagian in ('KENEK') and aktif=1 and kd_karyawan='{0}'", kd_karyawan)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Not drd(0).ToString.Equals("") Then
                    hasil = drd(0).ToString
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

    Private Sub Get_Aksesform()

        Dim rows() As DataRow = dtmenu.Select(String.Format("NAMAFORM='{0}'", Me.Name.ToUpper.ToString))

        If Convert.ToInt16(rows(0)("t_add")) = 1 Or Convert.ToInt16(rows(0)("t_edit")) = 1 Then
            btsimpan.Enabled = True
        Else
            btsimpan.Enabled = False
        End If

    End Sub

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            For i As Integer = 0 To dv1.Count - 1

                If Not dv1(i)("kd_supir").ToString.Equals(dv1(i)("kd_supir0").ToString) Or _
                    Not dv1(i)("kd_kenek1").ToString.Equals(dv1(i)("kd_kenek10").ToString) Or _
                     Not dv1(i)("kd_kenek2").ToString.Equals(dv1(i)("kd_kenek20").ToString) Or _
                      Not dv1(i)("kd_kenek3").ToString.Equals(dv1(i)("kd_kenek30").ToString) Or _
                       Not dv1(i)("nopol").ToString.Equals(dv1(i)("nopol0").ToString) Then

                    Dim sql As String = ""
                    If dv1(i)("nobukti").ToString.Substring(0, 3).Equals("RKF") Then

                        sql = String.Format("update trrekap_to set nopol='{0}',kd_supir='{1}',kd_kenek1='{2}',kd_kenek2='{3}',kd_kenek3='{4}' where nobukti='{5}'", dv1(i)("nopol").ToString, _
                                           dv1(i)("kd_supir").ToString, dv1(i)("kd_kenek1").ToString, dv1(i)("kd_kenek2").ToString, dv1(i)("kd_kenek3").ToString, dv1(i)("nobukti").ToString)

                        Dim sqlup As String = String.Format("update trfaktur_balik set sverif=1 where nobukti_rkp='{0}'", dv1(i)("nobukti").ToString)
                        Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                            cmdup.ExecuteNonQuery()
                        End Using

                        simpan_histrekap(cn, sqltrans, dv1(i)("sfaktur_kosong").ToString, dv1(i)("nobukti").ToString, _
                                         dv1(i)("tglmuat").ToString, dv1(i)("kd_supir").ToString, dv1(i)("nopol").ToString, _
                                         dv1(i)("kd_supir0").ToString, dv1(i)("nopol0").ToString)

                    Else

                        If Not dv1(i)("nopol").ToString.Equals(dv1(i)("nopol0").ToString) Then

                            Dim sqlc As String = String.Format("select kd_gudang from ms_gudang where tipe_gudang='MOBIL' and kd_gudang='{0}'", dv1(i)("nopol").ToString)
                            Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
                            Dim drdc As OleDbDataReader = cmdc.ExecuteReader

                            Dim ada As Boolean = False
                            If drdc.Read Then
                                If Not drdc(0).ToString.Equals("") Then
                                    ada = True
                                End If
                            End If
                            drdc.Close()

                            If ada = False Then

                                If Not IsNothing(sqltrans) Then
                                    sqltrans.Rollback()
                                End If

                                MsgBox(String.Format("No polisi tidak ditemukan digudang mobil ({0}-{1})", dv1(i)("nobukti").ToString, dv1(i)("nopol").ToString), vbOKOnly + vbInformation, "Informasi")
                                GoTo langsung_keluar
                            Else

                                If simpan_spm2(dv1(i)("nobukti").ToString, dv1(i)("nopol").ToString, dv1(i)("nopol0").ToString, dv1(i)("kd_supir").ToString, dv1(i)("kd_supir0").ToString, cn, sqltrans) = "error" Then
                                    GoTo langsung_keluar
                                End If

                            End If

                        End If

                        sql = String.Format("update trspm set nopol='{0}',kd_gudang='{0}',kd_supir='{1}',kd_kenek1='{2}',kd_kenek2='{3}',kd_kenek3='{4}' where nobukti='{5}'", dv1(i)("nopol").ToString, _
                                           dv1(i)("kd_supir").ToString, dv1(i)("kd_kenek1").ToString, dv1(i)("kd_kenek2").ToString, dv1(i)("kd_kenek3").ToString, dv1(i)("nobukti").ToString)

                        'simpan_histspm(cn, sqltrans, dv1(i)("nobukti").ToString, _
                        '                 dv1(i)("tglmuat").ToString, dv1(i)("kd_supir").ToString, dv1(i)("nopol").ToString, _
                        '                 dv1(i)("kd_supir0").ToString, dv1(i)("nopol0").ToString)

                    End If

                    Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                        cmd.ExecuteNonQuery()
                    End Using

                End If

            Next

            sqltrans.Commit()

            MsgBox("Data disimpan..", vbOKOnly + vbInformation, "Infromasi")

langsung_keluar:

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


    End Sub

    Private Function simpan_spm2(ByVal nobukti As String, ByVal nopolbaru As String, ByVal nopollama As String, _
                            ByVal kdsupir_baru As String, ByVal kdsupir_lama As String, _
                            ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim hasil As String = ""

        Dim sql As String = String.Format("SELECT     trspm.tglmuat,trspm2.noid, trspm2.kd_gudang, trspm2.kd_barang, ms_barang.nama_barang, trspm2.qty, trspm2.satuan, trspm2.harga, trspm2.jumlah, trspm2.qtykecil, trspm2.hargakecil " & _
                "FROM         trspm2 INNER JOIN " & _
                "trspm on trspm2.nobukti=trspm.nobukti INNER JOIN " & _
                "ms_barang ON trspm2.kd_barang = ms_barang.kd_barang INNER JOIN ms_gudang ON trspm2.kd_gudang = ms_gudang.kd_gudang where trspm2.nobukti='{0}'", nobukti)
        Dim cmdspm2 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drdspm2 As OleDbDataReader = cmdspm2.ExecuteReader

        Dim kdbar As String
        Dim kdgudang As String
        Dim satuan As String
        Dim qty, qtykecil As Integer
        Dim harga, jumlah, hargakecil As String
        Dim tglmuat As String

        While drdspm2.Read

            kdbar = drdspm2("kd_barang").ToString
            kdgudang = drdspm2("kd_gudang").ToString
            satuan = drdspm2("satuan").ToString
            qty = Integer.Parse(drdspm2("qty").ToString)
            qtykecil = Integer.Parse(drdspm2("qtykecil").ToString)
            harga = drdspm2("harga").ToString
            jumlah = drdspm2("jumlah").ToString
            hargakecil = drdspm2("hargakecil").ToString
            tglmuat = drdspm2("tglmuat").ToString

            Dim hasilplusmin_old As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgudang, True, False, False)
            If Not hasilplusmin_old.Equals("ok") Then
                close_wait()

                If Not IsNothing(sqltrans) Then
                    sqltrans.Rollback()
                End If

                MsgBox(hasilplusmin_old, vbOKOnly + vbExclamation, "Informasi")
                hasil = "error"
                Exit While
            Else

                Dim hasilplusmin_old2 As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, nopollama, False, False, False)

                If Not hasilplusmin_old2.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilplusmin_old2, vbOKOnly + vbExclamation, "Informasi")
                    hasil = "error"
                    Exit While

                End If
            End If

            '2. update barang
            Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgudang, False, False, False)
            If Not hasilplusmin.Equals("ok") Then
                close_wait()

                If Not IsNothing(sqltrans) Then
                    sqltrans.Rollback()
                End If

                MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                hasil = "error"
                Exit While
            Else

                Dim hasilplusmin2 As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, nopolbaru, True, False, False)

                If Not hasilplusmin2.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilplusmin2, vbOKOnly + vbExclamation, "Informasi")
                    hasil = "error"
                    Exit While

                End If
            End If

            Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tglmuat, kdgudang, kdbar, qtykecil, 0, "Muat Barang (Kanvas)(Edit-Ver)", kdsupir_lama, nopollama)
            Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tglmuat, nopollama, kdbar, 0, qtykecil, "Muat Barang (Kanvas)(Edit-Ver)", kdsupir_lama, nopollama)

            Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tglmuat, kdgudang, kdbar, 0, qtykecil, "Muat Barang (Kanvas)(Edit-Ver)", kdsupir_baru, nopolbaru)
            Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tglmuat, nopolbaru, kdbar, qtykecil, 0, "Muat Barang (Kanvas)(Edit-Ver)", kdsupir_baru, nopolbaru)


        End While
        drdspm2.Close()

        If Not hasil.Equals("error") Then
            hasil = "ok"
        End If

        Return hasil

    End Function

    Private Sub simpan_histrekap(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal sfakturkosong As Integer, _
                                 ByVal nobukti As String, ByVal tglmuat As String, ByVal kdsupir As String, ByVal nopol As String, _
                                 kdsupir0 As String, nopol0 As String)

        If sfakturkosong = 0 Then

            Dim sqlk As String = String.Format("select * from trrekap_to2  where nobukti='{0}'", nobukti)
            Dim cmdk As OleDbCommand = New OleDbCommand(sqlk, cn, sqltrans)
            Dim drdk As OleDbDataReader = cmdk.ExecuteReader

            While drdk.Read

                Dim sqlc As String = String.Format("select * from trfaktur_to2 inner join ms_barang on trfaktur_to2.kd_barang=ms_barang.kd_barang inner join trfaktur_to on trfaktur_to.nobukti=trfaktur_to2.nobukti where ms_barang.jenis='FISIK' and trfaktur_to2.nobukti='{0}'", drdk("nobukti_fak").ToString)
                Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
                Dim drdc As OleDbDataReader = cmdc.ExecuteReader

                While drdc.Read

                    Dim qty1 As Integer = Integer.Parse(drdc("qty1").ToString)
                    Dim qty2 As Integer = Integer.Parse(drdc("qty2").ToString)
                    Dim qty3 As Integer = Integer.Parse(drdc("qty3").ToString)
                    Dim kd_toko As String = drdc("kd_toko").ToString

                    Dim qtykecil As Integer = Integer.Parse(drdc("qtykecil").ToString)
                    Dim kdbar As String = drdc("kd_barang").ToString
                    Dim kdgud As String = drdc("kd_gudang").ToString

                    '3. insert to hist stok
                    Clsmy.Insert_HistBarang(cn, sqltrans, drdk("nobukti_fak").ToString, tglmuat, kdgud, kdbar, qtykecil, 0, "Jual TO (Edit-Ver)", kdsupir0, nopol0)

                    Clsmy.Insert_HistBarang(cn, sqltrans, drdk("nobukti_fak").ToString, tglmuat, kdgud, kdbar, 0, qtykecil, "Jual TO (Edit-Ver)", kdsupir, nopol)


                End While
                drdc.Close()

            End While
            drdk.Close()

        Else

            Dim sqlcek As String = String.Format("select jml,noid from trrekap_to3 where nobukti='{0}'", nobukti)
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
                Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tglmuat, "G000", "G0001", jmlold, 0, "Jual TO Kanvas (Edit-Ver)", kdsupir0, nopol0)

                Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tglmuat, "G000", "G0001", 0, jmlold, "Jual TO Kanvas (Edit-Ver)", kdsupir, nopol)

            End If

        End If

    End Sub

    Private Sub simpan_histspm(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, _
                               ByVal nobukti As String, ByVal tglmuat As String, ByVal kdsupir As String, ByVal nopol As String, _
                                 kdsupir0 As String, nopol0 As String)

        Dim sql As String = String.Format("select trspm.kd_gudang,trspm2.kd_barang,trspm2.qtykecil,trspm2.kd_gudang as gudangf from trspm inner join trspm2 on trspm.nobukti=trspm2.nobukti where trspm.nobukti='{0}'", nobukti)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        While drd.Read

            Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tglmuat, drd("kd_gudangf").ToString, drd("kd_barang").ToString, drd("qtykecil").ToString, 0, "Muat Barang (Kanvas)(Edit)", kdsupir0, nopol0)
            Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tglmuat, drd("kd_gudang").ToString, drd("kd_barang").ToString, 0, drd("qtykecil").ToString, "Muat Barang (Kanvas)(Edit)", kdsupir0, nopol0)

            Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tglmuat, drd("kd_gudangf").ToString, drd("kd_barang").ToString, 0, drd("qtykecil").ToString, "Muat Barang (Kanvas)(Edit)", kdsupir, nopol)
            Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tglmuat, drd("kd_gudang").ToString, drd("kd_barang").ToString, drd("qtykecil").ToString, 0, "Muat Barang (Kanvas)(Edit)", kdsupir, nopol)

        End While
        drd.Close()

    End Sub

    Private Sub fverif_supirkenek_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub fverif_supirkenek_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        cb1.SelectedIndex = 1
        cbjenis.SelectedIndex = 0

        statedit = False

        Get_Aksesform()

        ' cb1.SelectedIndex = 0

        ttgl.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

        'isi_nopol()
        'isi_supir()
        'isi_kenek()

    End Sub

    Private Sub bttampil_Click(sender As System.Object, e As System.EventArgs) Handles bttampil.Click

        isi_nopol()
        isi_supir()
        isi_kenek()

        load_data()

    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If e.Column.FieldName.Equals("kd_supir") Then

            Dim namasupir = cek_kodesupir(e.Value)

            If namasupir.Equals("") Then
                dv1(Me.BindingContext(dv1).Position)("kd_supir") = ""
                dv1(Me.BindingContext(dv1).Position)("nama_supir") = ""
            Else
                dv1(Me.BindingContext(dv1).Position)("kd_supir") = e.Value.ToString.ToUpper
                dv1(Me.BindingContext(dv1).Position)("nama_supir") = e.Value.ToString.ToUpper
            End If

        ElseIf e.Column.FieldName.Equals("nama_supir") Then

            dv1(Me.BindingContext(dv1).Position)("kd_supir") = e.Value.ToString.ToUpper

        ElseIf e.Column.FieldName.Equals("kd_kenek1") Then

            Dim nama_kenek As String = cek_kodekenek(e.Value)

            If nama_kenek.Equals("") Then
                dv1(Me.BindingContext(dv1).Position)("kd_kenek1") = ""
                dv1(Me.BindingContext(dv1).Position)("nama_kenek1") = ""
            Else
                dv1(Me.BindingContext(dv1).Position)("kd_kenek1") = e.Value.ToString.ToUpper
                dv1(Me.BindingContext(dv1).Position)("nama_kenek1") = e.Value.ToString.ToUpper
            End If

        ElseIf e.Column.FieldName.Equals("nama_kenek1") Then

            dv1(Me.BindingContext(dv1).Position)("kd_kenek1") = e.Value.ToString.ToUpper

        ElseIf e.Column.FieldName.Equals("kd_kenek2") Then

            Dim nama_kenek As String = cek_kodekenek(e.Value)

            If nama_kenek.Equals("") Then
                dv1(Me.BindingContext(dv1).Position)("kd_kenek2") = ""
                dv1(Me.BindingContext(dv1).Position)("nama_kenek2") = ""
            Else
                dv1(Me.BindingContext(dv1).Position)("kd_kenek2") = e.Value.ToString.ToUpper
                dv1(Me.BindingContext(dv1).Position)("nama_kenek2") = e.Value.ToString.ToUpper
            End If

        ElseIf e.Column.FieldName.Equals("nama_kenek2") Then

            dv1(Me.BindingContext(dv1).Position)("kd_kenek2") = e.Value.ToString.ToUpper

        ElseIf e.Column.FieldName.Equals("kd_kenek3") Then

            Dim nama_kenek As String = cek_kodekenek(e.Value)

            If nama_kenek.Equals("") Then
                dv1(Me.BindingContext(dv1).Position)("kd_kenek3") = ""
                dv1(Me.BindingContext(dv1).Position)("nama_kenek3") = ""
            Else
                dv1(Me.BindingContext(dv1).Position)("kd_kenek3") = e.Value.ToString.ToUpper
                dv1(Me.BindingContext(dv1).Position)("nama_kenek3") = e.Value.ToString.ToUpper
            End If

        ElseIf e.Column.FieldName.Equals("nama_kenek3") Then

            dv1(Me.BindingContext(dv1).Position)("kd_kenek3") = e.Value.ToString.ToUpper

        ElseIf e.Column.FieldName.Equals("nopol") Then

            If Not dv1(Me.BindingContext(dv1).Position)("jenisfak").ToString.Equals("01.TO") Then
                '  dv1(Me.BindingContext(dv1).Position)("nopol") = dv1(Me.BindingContext(dv1).Position)("nopol0").ToString.ToUpper
            End If

        End If

    End Sub

    Private Sub GridView1_Click(sender As Object, e As System.EventArgs) Handles GridView1.Click
        opendata_tk()
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As System.EventArgs) Handles GridView1.DoubleClick

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        If Not dv1(Me.BindingContext(dv1).Position)("nobukti").ToString().Substring(0, 3).Equals("RKF") Then
            Return
        End If

        statedit = True
        Using fkar2 As New ffaktur_b2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = Me.BindingContext(dv1).Position, .statview = False, .statverif = True}
            fkar2.ShowDialog()
            statedit = False
            opendata_tk()
        End Using

    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        opendata_tk()
    End Sub

    Private Sub GridView1_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles GridView1.RowCellClick
        opendata_tk()
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        opendata_tk()
    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If dv1.Count <= 0 Then
            MsgBox("Tidak ada data yang akan disimpan...", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If MsgBox("Yakin semua data sudah benar ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
            simpan()
        End If

    End Sub


End Class