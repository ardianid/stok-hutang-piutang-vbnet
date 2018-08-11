Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class ftrun_br2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private tgltrun_old As String

    Dim kdsupir As String

    Private Sub kosongkan()
        tbukti.Text = "<< New >>"

        tspm.EditValue = ""
        tnopol.EditValue = ""
        tgudang_mob.EditValue = ""
        tket.EditValue = ""

        opengrid()

    End Sub

    Private Sub opengrid()

        Dim sql As String = String.Format("SELECT     trturun_br2.noid, trturun_br2.kd_barang, ms_barang.nama_barang, trturun_br2.kd_gudang, trturun_br2.qty, trturun_br2.satuan, trturun_br2.qty_tr, trturun_br2.harga, " & _
        "trturun_br2.jumlah, trturun_br2.qtykecil, trturun_br2.qtykecil_tr, trturun_br2.hargakecil, ms_barang.qty1, ms_barang.qty2, ms_barang.qty3, ms_barang.satuan1, ms_barang.satuan2, ms_barang.satuan3 " & _
        "FROM trturun_br2 INNER JOIN ms_barang ON trturun_br2.kd_barang = ms_barang.kd_barang where trturun_br2.nobukti='{0}'", tbukti.Text.Trim)

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

    Private Sub opengrid2()

        grid2.DataSource = Nothing

        Dim sql As String = String.Format("SELECT      ms_barangk.kd_barang, ms_barangk.nama_barang, ms_barangk.qty1, ms_barangk.qty2, ms_barangk.qty3, ms_barangk.satuan1, ms_barangk.satuan2, ms_barangk.satuan3,  " & _
                      "trspm2.qty, trspm2.satuan, trspm2.qty AS qty_k, 0 AS qtykecil " & _
            "FROM         trspm2 INNER JOIN " & _
                      "ms_barang ON trspm2.kd_barang = ms_barang.kd_barang INNER JOIN " & _
                      "ms_barang AS ms_barangk ON ms_barang.kd_barang_kmb = ms_barangk.kd_barang " & _
            "WHERE trspm2.nobukti='{0}'", tspm.Text.Trim)

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        Try

            open_wait()

            dv2 = Nothing

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

            Dim sql As String = String.Format("select * from trturun_br3 where nobukti='{0}' and kd_barang='{1}' and satuan='{2}'", _
                                              tbukti.Text.Trim, dv2(i)("kd_barang").ToString, dv2(i)("satuan"))
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

        If kdgudang.Equals("") Then
            tgudang.EditValue = "G000"
        Else
            tgudang2.EditValue = kdgudang
        End If

    End Sub

    Private Sub load_data()

        Dim sql As String = ""

        If addstat = False Then

            sql = String.Format("SELECT     trturun_br2.noid, trturun_br2.kd_barang, ms_barang.nama_barang, trturun_br2.kd_gudang, trturun_br2.qty, trturun_br2.satuan, trturun_br2.qty_tr, trturun_br2.harga, " & _
        "trturun_br2.jumlah, trturun_br2.qtykecil, trturun_br2.qtykecil_tr, trturun_br2.hargakecil, ms_barang.qty1, ms_barang.qty2, ms_barang.qty3, ms_barang.satuan1, ms_barang.satuan2, ms_barang.satuan3 " & _
        "FROM trturun_br2 INNER JOIN ms_barang ON trturun_br2.kd_barang = ms_barang.kd_barang where trturun_br2.nobukti='{0}'", tbukti.Text.Trim)

        Else

            sql = String.Format("SELECT trspm2.kd_barang, ms_barang.nama_barang, trspm2.kd_gudang, trspm2.qty, trspm2.satuan, trspm2.harga, 0 AS noid, 0 AS qty_tr, 0 AS jumlah,trspm2.qtykecil, " & _
            "0 AS qtykecil_tr, 0 AS hargakecil, ms_barang.qty1, ms_barang.qty2, ms_barang.qty3, ms_barang.satuan1, ms_barang.satuan2, ms_barang.satuan3 " & _
            "FROM trspm2 INNER JOIN ms_barang ON trspm2.kd_barang = ms_barang.kd_barang where trspm2.nobukti='{0}'", tspm.Text.Trim)

        End If

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

        opengrid2()
        XtraTabControl1.TabIndex = 0


    End Sub

    Private Sub isi()

        Dim nobukti As String = dv(position)("nobukti").ToString
        Dim sql As String = String.Format("SELECT trturun_br.nobukti, trturun_br.tanggal, trturun_br.tgl_turun, trturun_br.nobukti_spm, trspm.nopol, trspm.kd_gudang AS kd_gudang_mbl, trturun_br.kd_gudang,trturun_br.note,trspm.kd_supir " & _
            "FROM  trturun_br INNER JOIN trspm ON trturun_br.nobukti_spm = trspm.nobukti where trturun_br.nobukti='{0}'", nobukti)

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

                        kdsupir = dread("kd_supir").ToString

                        tbukti.EditValue = dread("nobukti").ToString

                        ttgl.EditValue = DateValue(dread("tanggal").ToString)
                        ttgl_trun.EditValue = DateValue(dread("tgl_turun").ToString)

                        tgltrun_old = ttgl_trun.EditValue

                        tspm.EditValue = dread("nobukti_spm").ToString
                        tnopol.EditValue = dread("nopol").ToString
                        tgudang_mob.EditValue = dread("kd_gudang_mbl").ToString

                        tgudang.EditValue = dread("kd_gudang").ToString

                        tket.EditValue = dread("note").ToString

                        opengrid()
                        opengrid2()

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

        Const sql As String = "select kd_gudang,nama_gudang from ms_gudang where tipe_gudang='FISIK'"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet
        Dim dvg As DataView

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvg = dvm.CreateDataView(ds.Tables(0))

            tgudang.Properties.DataSource = dvg
            tgudang2.Properties.DataSource = dvg

            tgudang.EditValue = "G000"
            tgudang2.EditValue = "G000"

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

        Dim sql As String = String.Format("select max(nobukti) from trturun_br where nobukti like '%TRK.{0}%'", tahunbulan)

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

        

        Return String.Format("TRK.{0}{1}{2}", tahun, bulan, kbukti)

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

            If addstat Then
                Dim bukti As String = cekbukti(cn, sqltrans)
                tbukti.EditValue = bukti

                '0. insert spm
                Dim sqlin As String = String.Format("insert into trturun_br (nobukti,tanggal,tgl_turun,nobukti_spm,kd_gudang,note,netto) values('{0}','{1}','{2}','{3}','{4}','{5}',{6})", _
                                                        tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_trun.EditValue), tspm.Text.Trim, tgudang.EditValue, tket.Text.Trim, Replace(GridView1.Columns("jumlah").SummaryItem.SummaryValue, ",", "."))


                cmd = New OleDbCommand(sqlin, cn, sqltrans)
                cmd.ExecuteNonQuery()

                Dim sql_spm As String = String.Format("update trspm set spulang=1 where nobukti='{0}'", tspm.Text.Trim)
                Using cmdspm As OleDbCommand = New OleDbCommand(sql_spm, cn, sqltrans)
                    cmdspm.ExecuteNonQuery()
                End Using

                Clsmy.InsertToLog(cn, "bbturun_kv", 1, 0, 0, 0, tbukti.Text.Trim, ttgl.EditValue, sqltrans)

            Else

                '1. update rekap
                Dim sqlup As String = String.Format("update trturun_br set tanggal='{0}',tgl_turun='{1}',nobukti_spm='{2}',kd_gudang='{3}',note='{4}',netto={5} where nobukti='{6}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_trun.EditValue), tspm.Text.Trim, tgudang.EditValue, tket.Text.Trim, Replace(GridView1.Columns("jumlah").SummaryItem.SummaryValue, ",", "."), tbukti.Text.Trim)

                cmd = New OleDbCommand(sqlup, cn, sqltrans)
                cmd.ExecuteNonQuery()


                Clsmy.InsertToLog(cn, "bbturun_kv", 0, 1, 0, 0, tbukti.Text.Trim, ttgl.EditValue, sqltrans)
            End If

            If simpan2(cn, sqltrans) = "ok" Then
                '------------------------------

                If dv2.Count > 0 Then

                    If Not (simpan3(cn, sqltrans) = "ok") Then
                        GoTo langsung_aja
                    End If

                End If

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
                    tnopol.Focus()
                Else
                    close_wait()
                    Me.Close()
                End If

                '----------------------------------
            End If

langsung_aja:

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

        Dim cmdc As OleDbCommand
        Dim drdc As OleDbDataReader
        Dim hasil As String = ""

        Dim kdbar As String
        Dim kdgudang As String
        Dim satuan As String
        Dim qty, qtykecil As Integer
        Dim harga, jumlah, hargakecil As String
        Dim qty_tr, qtykecil_tr As Integer

        For i As Integer = 0 To dv1.Count - 1

            kdbar = dv1(i)("kd_barang").ToString
            kdgudang = dv1(i)("kd_gudang").ToString
            satuan = dv1(i)("satuan").ToString
            qty = Integer.Parse(dv1(i)("qty").ToString)
            qtykecil = Integer.Parse(dv1(i)("qtykecil").ToString)
            harga = dv1(i)("harga").ToString
            jumlah = dv1(i)("jumlah").ToString
            hargakecil = dv1(i)("hargakecil").ToString
            qty_tr = Integer.Parse(dv1(i)("qty_tr").ToString)
            '  qtykecil0 = Integer.Parse(dv1(i)("qtykecil0").ToString)
            qtykecil_tr = Integer.Parse(dv1(i)("qtykecil_tr").ToString)

            If dv1(i)("noid").Equals(0) Then
                Dim sqlins As String = String.Format("insert into trturun_br2(nobukti,kd_gudang,kd_barang,satuan,qty,qty_tr,harga,jumlah,qtykecil,qtykecil_tr,hargakecil) values('{0}','{1}','{2}','{3}',{4},{5},{6},{7},{8},{9},{10})", tbukti.Text.Trim, _
                                                     tgudang_mob.Text.Trim, kdbar, satuan, qty, qty_tr, Replace(harga, ",", "."), Replace(jumlah, ",", "."), qtykecil, qtykecil_tr, Replace(hargakecil, ",", "."))

                Using cmd As OleDbCommand = New OleDbCommand(sqlins, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

                If qtykecil = 0 Then

                    Dim hasilplusmin_tamb As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil_tr, kdbar, tgudang_mob.Text.Trim, True, False, False)

                    If Not hasilplusmin_tamb.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin_tamb, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        Exit For

                    End If

                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_trun.EditValue, tgudang_mob.EditValue, kdbar, qtykecil_tr, 0, "Turun Barang (Kanvas)", kdsupir, tnopol.Text.Trim)

                Else

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil_tr, kdbar, tgudang.EditValue, True, False, False)
                    If Not hasilplusmin.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        Exit For
                    Else

                        Dim hasilplusmin2 As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil_tr, kdbar, tgudang_mob.Text.Trim, False, False, False)

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
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_trun.EditValue, tgudang_mob.Text.Trim, kdbar, 0, qtykecil_tr, "Turun Barang (Kanvas)", kdsupir, tnopol.Text.Trim)
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_trun.EditValue, tgudang.EditValue, kdbar, qtykecil_tr, 0, "Turun Barang (Kanvas)", kdsupir, tnopol.Text.Trim)

                End If


            Else

                ' jika rubah

                Dim sqlc As String = String.Format("select qtykecil_tr,qtykecil from trturun_br2 where noid={0}", dv1(i)("noid").ToString)
                cmdc = New OleDbCommand(sqlc, cn, sqltrans)
                drdc = cmdc.ExecuteReader

                If drdc.Read Then
                    If IsNumeric(drdc(0).ToString) Then

                        If qtykecil_tr <> Integer.Parse(drdc(0).ToString) Then

                            Dim sqlup As String = String.Format("update trturun_br2 set qty_tr={0},qtykecil_tr={1},jumlah={2} where noid={3}", qty_tr, qtykecil_tr, jumlah, dv1(i)("noid").ToString)
                            Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                                cmdup.ExecuteNonQuery()
                            End Using

                            If Integer.Parse(drdc(1).ToString) = 0 Then

                                Dim hasilplusmin_br As String = PlusMin_Barang_Kend(cn, sqltrans, drdc(0).ToString, kdbar, tgudang_mob.Text.Trim, False, False, False)

                                If Not hasilplusmin_br.Equals("ok") Then
                                    close_wait()

                                    If Not IsNothing(sqltrans) Then
                                        sqltrans.Rollback()
                                    End If

                                    MsgBox(hasilplusmin_br, vbOKOnly + vbExclamation, "Informasi")
                                    hasil = "error"
                                    Exit For

                                End If

                                Dim hasilplusmin_br2 As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil_tr, kdbar, tgudang_mob.Text.Trim, True, False, False)

                                If Not hasilplusmin_br2.Equals("ok") Then
                                    close_wait()

                                    If Not IsNothing(sqltrans) Then
                                        sqltrans.Rollback()
                                    End If

                                    MsgBox(hasilplusmin_br2, vbOKOnly + vbExclamation, "Informasi")
                                    hasil = "error"
                                    Exit For

                                End If

                                If addstat = False Then
                                    If DateValue(tgltrun_old) <> DateValue(ttgl_trun.EditValue) Then
                                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgltrun_old, tgudang_mob.Text.Trim, kdbar, 0, drdc(0).ToString, "Turun Barang (Kanvas)(Edit)", kdsupir, tnopol.Text.Trim)
                                    Else
                                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_trun.EditValue, tgudang_mob.Text.Trim, kdbar, 0, drdc(0).ToString, "Turun Barang (Kanvas)(Edit)", kdsupir, tnopol.Text.Trim)
                                    End If
                                End If

                                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_trun.EditValue, tgudang_mob.Text.Trim, kdbar, qtykecil_tr, 0, "Turun Barang (Kanvas)(Edit)", kdsupir, tnopol.Text.Trim)

                            Else

                                '2. update barang
                                Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, drdc(0).ToString, kdbar, tgudang.EditValue, False, False, False)
                                If Not hasilplusmin.Equals("ok") Then
                                    close_wait()

                                    If Not IsNothing(sqltrans) Then
                                        sqltrans.Rollback()
                                    End If

                                    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                                    hasil = "error"
                                    Exit For
                                Else

                                    Dim hasilplusmin2 As String = PlusMin_Barang_Kend(cn, sqltrans, drdc(0).ToString, kdbar, tgudang_mob.Text.Trim, True, False, False)

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

                                ' add lagi

                                Dim hasilplusmin3 As String = PlusMin_Barang(cn, sqltrans, qtykecil_tr, kdbar, tgudang.EditValue, True, False, False)
                                If Not hasilplusmin3.Equals("ok") Then
                                    close_wait()

                                    If Not IsNothing(sqltrans) Then
                                        sqltrans.Rollback()
                                    End If

                                    MsgBox(hasilplusmin3, vbOKOnly + vbExclamation, "Informasi")
                                    hasil = "error"
                                    Exit For
                                Else

                                    Dim hasilplusmin4 As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil_tr, kdbar, tgudang_mob.Text.Trim, False, False, False)

                                    If Not hasilplusmin4.Equals("ok") Then
                                        close_wait()

                                        If Not IsNothing(sqltrans) Then
                                            sqltrans.Rollback()
                                        End If

                                        MsgBox(hasilplusmin4, vbOKOnly + vbExclamation, "Informasi")
                                        hasil = "error"
                                        Exit For

                                    End If
                                End If

                                ' insert histori


                                If addstat = False Then
                                    If DateValue(tgltrun_old) <> DateValue(ttgl_trun.EditValue) Then
                                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgltrun_old, tgudang.EditValue, kdbar, 0, drdc(0).ToString, "Turun Barang (Kanvas)(Edit)", kdsupir, tnopol.Text.Trim)
                                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgltrun_old, tgudang_mob.Text.Trim, kdbar, drdc(0).ToString, 0, "Turun Barang (Kanvas)(Edit)", kdsupir, tnopol.Text.Trim)
                                    Else
                                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_trun.EditValue, tgudang.EditValue, kdbar, 0, drdc(0).ToString, "Turun Barang (Kanvas)(Edit)", kdsupir, tnopol.Text.Trim)
                                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_trun.EditValue, tgudang_mob.Text.Trim, kdbar, drdc(0).ToString, 0, "Turun Barang (Kanvas)(Edit)", kdsupir, tnopol.Text.Trim)
                                    End If
                                End If

                                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_trun.EditValue, tgudang_mob.Text.Trim, kdbar, 0, qtykecil_tr, "Turun Barang (Kanvas)(Edit)", kdsupir, tnopol.Text.Trim)
                                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_trun.EditValue, tgudang.EditValue, kdbar, qtykecil_tr, 0, "Turun Barang (Kanvas)(Edit)", kdsupir, tnopol.Text.Trim)


                            End If

                          
                        End If

                    End If
                End If




            End If

        Next

        If Not hasil.Equals("error") Then
            hasil = "ok"
        End If

        Return hasil

    End Function

    Private Function simpan3(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim cmd As OleDbCommand
        Dim drd As OleDbDataReader

        Dim kdgud As String = tgudang.EditValue
        Dim kdbar As String
        Dim qtykecil As Integer
        Dim satuan As String
        Dim qty As Integer

        Dim hasil As String = ""
        Dim ada As Boolean = False

        Dim sqlfak3 As String = ""


        For i As Integer = 0 To dv2.Count - 1

            Dim sql As String = String.Format("select * from trturun_br3 where nobukti='{0}' and kd_barang='{1}' and satuan='{2}'", _
                                              tbukti.Text.Trim, dv2(i)("kd_barang").ToString, dv2(i)("satuan"))

            cmd = New OleDbCommand(sql, cn, sqltrans)
            drd = cmd.ExecuteReader

            kdbar = dv2(i)("kd_barang").ToString
            qtykecil = kalkulasi2(dv2(i)("qty_k").ToString, dv2(i)("qty1").ToString, dv2(i)("qty2").ToString, dv2(i)("qty3").ToString, _
                                 dv2(i)("satuan1").ToString, dv2(i)("satuan2").ToString, dv2(i)("satuan3").ToString, dv2(i)("satuan").ToString)
            satuan = dv2(i)("satuan").ToString
            qty = Integer.Parse(dv2(i)("qty_k").ToString)

            If drd.Read Then
                If IsNumeric(drd("noid").ToString) Then

                    Dim qtyold As Integer = Integer.Parse(drd("qtykecil").ToString)

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtyold, kdbar, kdgud, False, False, False)
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
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_trun.EditValue, kdgud, kdbar, 0, qtyold, "Turun Barang (Kanvas)", kdsupir, tnopol.Text.Trim)

                    ada = True

                End If
            End If

            drd.Close()

            '2. update barang
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

            '3. insert to hist stok
            Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_trun.EditValue, kdgud, kdbar, qtykecil, 0, "Penurunan Barang (Kanvas)", kdsupir, tnopol.Text.Trim)

            If ada Then
                sqlfak3 = String.Format("update trturun_br3 set kd_gudang='{0}',qty={1},qtykecil={2} where nobukti='{3}' and kd_barang='{4}' and satuan='{5}'", _
                                            kdgud, qty, qtykecil, tbukti.Text.Trim, dv2(i)("kd_barang").ToString, dv2(i)("satuan"))
            Else
                sqlfak3 = String.Format("insert into trturun_br3 (nobukti,kd_gudang,kd_barang,satuan,qty,qtykecil) values('{0}','{1}','{2}','{3}',{4},{5})", tbukti.Text.Trim, kdgud, kdbar, satuan, qty, qtykecil)
            End If

            Using cmdfak As OleDbCommand = New OleDbCommand(sqlfak3, cn, sqltrans)
                cmdfak.ExecuteNonQuery()
            End Using

        Next

        If hasil.Equals("") Then
            hasil = "ok"
        End If

        Return hasil

    End Function

    Private Sub hapus()

        Dim cn As OleDbConnection = Nothing

        Dim sqltrans As OleDbTransaction = Nothing

        Dim noid As Integer = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("noid"))

        Try

            If noid = 0 Then
                dv1.Delete(Me.BindingContext(dv1).Position)
            Else

                open_wait()

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                sqltrans = cn.BeginTransaction

                Dim sqldel As String = String.Format("delete from trturun_br2 where noid={0}", noid)
                Using cmd As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

                Dim kdbar As String
                Dim kdgudang_mbl As String
                Dim satuan As String
                Dim qty, qtykecil As Integer
                Dim harga, jumlah, hargakecil As String
                Dim qty_tr, qtykecil_tr As Integer

                kdbar = dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString
                kdgudang_mbl = dv1(Me.BindingContext(dv1).Position)("kd_gudang").ToString
                satuan = dv1(Me.BindingContext(dv1).Position)("satuan").ToString
                qty = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qty").ToString)
                qtykecil = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtykecil").ToString)
                harga = dv1(Me.BindingContext(dv1).Position)("harga").ToString
                jumlah = dv1(Me.BindingContext(dv1).Position)("jumlah").ToString
                hargakecil = dv1(Me.BindingContext(dv1).Position)("hargakecil").ToString
                qty_tr = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qty_tr").ToString)
                qtykecil_tr = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtykecil_tr").ToString)


                '2. update barang
                Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil_tr, kdbar, tgudang.EditValue, False, False, False)
                If Not hasilplusmin.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                    GoTo langsung
                Else
                    Dim hasilplusmin2 As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil_tr, kdbar, kdgudang_mbl, True, False, False)
                    If Not hasilplusmin2.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin2, vbOKOnly + vbExclamation, "Informasi")
                        GoTo langsung
                    End If
                End If


                If DateValue(tgltrun_old) <> DateValue(ttgl_trun.EditValue) Then
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgltrun_old, tgudang.EditValue, kdbar, 0, qtykecil_tr, "Penurunan Barang (Kanvas)", kdsupir, tnopol.Text.Trim)
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgltrun_old, kdgudang_mbl, kdbar, qtykecil_tr, 0, "Penurunan Barang (Kanvas)", kdsupir, tnopol.Text.Trim)
                Else
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_trun.EditValue, tgudang.EditValue, kdbar, 0, qtykecil_tr, "Penurunan Barang (Kanvas)", kdsupir, tnopol.Text.Trim)
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_trun.EditValue, kdgudang_mbl, kdbar, qtykecil_tr, 0, "Penurunan Barang (Kanvas)", kdsupir, tnopol.Text.Trim)
                End If

                

                sqltrans.Commit()

                dv1.Delete(Me.BindingContext(dv1).Position)

                close_wait()

            End If

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


    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("sbatal") = 0
        orow("nobukti") = tbukti.Text.Trim
        orow("nopol") = tnopol.Text.Trim
        orow("tanggal") = ttgl.EditValue
        orow("tgl_turun") = ttgl_trun.EditValue
        orow("nobukti_spm") = tspm.Text.Trim
        orow("kd_gudang") = tgudang.EditValue
        orow("note") = tket.Text.Trim
        orow("kd_supir") = kdsupir

        dv.EndInit()

    End Sub

    Private Sub updateview()

        dv(position)("nobukti") = tbukti.Text.Trim
        dv(position)("tanggal") = ttgl.EditValue
        dv(position)("nopol") = tnopol.Text.Trim
        dv(position)("tgl_turun") = ttgl_trun.EditValue
        dv(position)("nobukti_spm") = tspm.Text.Trim
        dv(position)("kd_gudang") = tgudang.EditValue
        dv(position)("note") = tket.Text.Trim
        dv(position)("kd_supir") = kdsupir

    End Sub

    Private Function cek_barangdispm(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal kdbarang As String) As Boolean

        Dim hasil As Boolean = False

        Dim sql As String = String.Format("select nobukti from trspm2 where nobukti='{0}' and kd_barang='{1}' and kd_gudang='{2}'", tspm.Text.Trim, kdbarang, tgudang.EditValue)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        If drd.Read Then
            If Not drd(0).ToString.Equals("") Then
                hasil = True
            End If
        End If
        drd.Close()

        Return hasil

    End Function

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tspm.Text.Trim.Length = 0 Then
            MsgBox("NoBukti SPM kendaraan harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tspm.Focus()
            Return
        End If

        If tgudang.Text.Trim.Length = 0 Then
            MsgBox("NoBukti SPM kendaraan harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tspm.Focus()
            Return
        End If

        If dv2.Count > 0 Then

            If tgudang2.EditValue = "" Then
                MsgBox("Gudang masuk harus diisi", vbOKOnly + vbInformation, "Informasi")
                XtraTabControl1.TabIndex = 0
                tgudang2.Focus()
                Return
            End If

        End If


        If MsgBox("Yakin sudah benar ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
            simpan()
        End If

    End Sub

    Private Sub frekap_to2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub frekap_to2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        isi_gudang()

        ttgl.EditValue = Date.Now
        ttgl_trun.EditValue = Date.Now

        If addstat = False Then

            tspm.Enabled = False
            bts_spm.Enabled = False
            tgudang.Enabled = False

            isi()
        Else
            kosongkan()


            tspm.Enabled = True
            bts_spm.Enabled = True
            tgudang.Enabled = True

        End If

    End Sub

    Private Sub tspm_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tspm.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_spm_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tspm_LostFocus(sender As Object, e As System.EventArgs) Handles tspm.LostFocus
        If tspm.Text.Trim.Length = 0 Then
            tspm.EditValue = ""
            tnopol.EditValue = ""
            tgudang_mob.EditValue = ""
        End If
    End Sub

    Private Sub tspm_Validated(sender As System.Object, e As System.EventArgs) Handles tspm.Validated

        If tspm.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            ' Dim sql As String = "SELECT trspm.nobukti, trspm.tanggal, trspm.tglmuat, trspm.tglberangkat, trspm.nopol, trspm.kd_gudang,  ms_pegawai.nama_karyawan AS nama_supir " & _
            '"FROM  trspm INNER JOIN ms_pegawai ON trspm.kd_supir = ms_pegawai.kd_karyawan where trspm.sbatal=0 and spulang=0 and smuat=1"

            Dim sql As String = "SELECT trspm.nobukti, trspm.tanggal, trspm.tglmuat, trspm.tglberangkat, trspm.nopol, trspm.kd_gudang,  ms_pegawai.nama_karyawan AS nama_supir,trspm.kd_supir " & _
          "FROM  trspm INNER JOIN ms_pegawai ON trspm.kd_supir = ms_pegawai.kd_karyawan where trspm.sbatal=0 and spulang=0"

            sql = String.Format("{0} and trspm.nobukti='{1}'", sql, tspm.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tspm.EditValue = dread("nobukti").ToString
                        tnopol.EditValue = dread("nopol").ToString
                        tgudang_mob.EditValue = dread("kd_gudang").ToString

                        kdsupir = dread("kd_supir").ToString

                    Else
                        tspm.EditValue = ""
                        tnopol.EditValue = ""
                        tgudang_mob.EditValue = ""
                        kdsupir = ""
                    End If
                Else
                    tspm.EditValue = ""
                    tnopol.EditValue = ""
                    tgudang_mob.EditValue = ""
                    kdsupir = ""
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

    Private Sub bts_spm_Click(sender As System.Object, e As System.EventArgs) Handles bts_spm.Click
        Dim fs As New fsspm With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tspm.EditValue = fs.get_NoSPM

        tspm_Validated(sender, Nothing)

        ' tnopol.EditValue = fs.get_Nopol
        ' tgudang_mob.EditValue = fs.get_Gudang

        '  load_data()

    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        If tgudang.Text.Trim.Length = 0 Then
            MsgBox("Gudang harus diisi dulu...", vbOKOnly + vbInformation, "Informasi")
            tgudang.Focus()
            Return
        End If

        If tspm.Text.Trim.Length = 0 Then
            MsgBox("No SPM harus diisi dulu...", vbOKOnly + vbInformation, "Informasi")
            tspm.Focus()
            Return
        End If

        load_data()

    End Sub

    Private Sub kalkulasi(ByVal jml As Integer)

        Dim posisi As Integer = Me.BindingContext(dv1).Position

        If jml = 0 Then

            dv1(posisi)("jumlah") = 0
            dv1(posisi)("qtykecil_tr") = 0
            dv1(posisi)("hargakecil") = 0

            Return

        End If

        Dim vqty1, vqty2, vqty3 As Integer

        vqty1 = Integer.Parse(dv1(posisi)("qty1").ToString)
        vqty2 = Integer.Parse(dv1(posisi)("qty2").ToString)
        vqty3 = Integer.Parse(dv1(posisi)("qty3").ToString)

        Dim sat1, sat2, sat3 As String

        sat1 = dv1(posisi)("satuan1").ToString
        sat2 = dv1(posisi)("satuan2").ToString
        sat3 = dv1(posisi)("satuan3").ToString


        '  Dim jml As String = dv1(position)("qty_tr")
        Dim jml1 As Integer
        If Not jml.Equals("") Then
            jml1 = Integer.Parse(jml)
        Else
            jml1 = 0
        End If

        Dim xharga As Double = Double.Parse(dv1(posisi)("harga").ToString)
        Dim disc_rp As Double
        Dim xjumlah As Double = jml1

        Dim vharga2, vharga3 As Double
        Dim satuanOld As String = dv1(posisi)("satuan").ToString
        Dim kqty As Integer

        If xharga > 0 Then

            vharga2 = xharga / vqty2
            vharga3 = vharga2 / vqty3

            If satuanOld.Equals(sat1) Then
                xjumlah = (xharga * xjumlah) - disc_rp
                kqty = (vqty1 * vqty2 * vqty3) * jml1
            ElseIf satuanOld.Equals(sat2) Then
                xjumlah = (vharga2 * xjumlah) - disc_rp
                kqty = (vqty3) * jml1
            ElseIf satuanOld.Equals(sat3) Then
                xjumlah = (vharga3 * xjumlah) - disc_rp
                kqty = jml1
            End If

            dv1(posisi)("jumlah") = xjumlah
            dv1(posisi)("qty_tr") = jml
            dv1(posisi)("qtykecil_tr") = kqty
            dv1(posisi)("hargakecil") = vharga3

        Else

            If satuanOld.Equals(sat1) Then
                kqty = (vqty1 * vqty2 * vqty3) * jml1
            ElseIf satuanOld.Equals(sat2) Then
                kqty = (vqty3) * jml1
            ElseIf satuanOld.Equals(sat3) Then
                kqty = jml1
            End If

            vharga2 = 0
            vharga3 = 0

            dv1(posisi)("jumlah") = 0
            dv1(posisi)("qty_tr") = jml
            dv1(posisi)("qtykecil_tr") = kqty
            dv1(posisi)("hargakecil") = vharga3

        End If




    End Sub

    Private Function kalkulasi2(ByVal jmlqt As Integer, ByVal vqty1 As Integer, ByVal vqty2 As Integer, ByVal vqty3 As Integer, _
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
            kqty = (vqty3) * jml1
        ElseIf satuanawal.Equals(satuan3) Then
            kqty = jml1
        End If

akhir:

        Return kqty

    End Function

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        kalkulasi(e.Value)
    End Sub

    Private Sub btadd_Click(sender As System.Object, e As System.EventArgs) Handles btadd.Click

        Dim fs As New ftrun_br3 With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .dv = dv1}
        fs.ShowDialog(Me)

    End Sub

    Private Sub btdel_Click(sender As System.Object, e As System.EventArgs) Handles btdel.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim kdbarang As String = dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            If cek_barangdispm(cn, sqltrans, kdbarang) = True Then
                sqltrans.Rollback()
                MsgBox("Barang tidak bisa dihapus karna ada didalam surat perintah muat..", vbOKOnly + vbExclamation, "Informasi")
                Return
            End If

            If Integer.Parse(dv1(Me.BindingContext(dv1).Position)("noid").ToString) = 0 Then
                dv1.Delete(Me.BindingContext(dv1).Position)
            Else

                Dim sqldel As String = String.Format("delete from trturun_br2 where noid={0}", dv1(Me.BindingContext(dv1).Position)("noid").ToString)
                Using cmd As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

                Dim kdbar As String
                Dim kdgudang As String
                Dim satuan As String
                Dim qty, qtykecil As Integer
                Dim qty0, qtykecil0 As Integer

                kdbar = dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString
                kdgudang = dv1(Me.BindingContext(dv1).Position)("kd_gudang").ToString
                satuan = dv1(Me.BindingContext(dv1).Position)("satuan").ToString
                qty = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qty_tr").ToString)
                qtykecil = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtykecil_tr").ToString)
                qty0 = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qty").ToString)
                qtykecil0 = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtykecil").ToString)

                Dim hasilplusmin_old2 As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, kdgudang, False, False, False)

                If Not hasilplusmin_old2.Equals("ok") Then

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilplusmin_old2, vbOKOnly + vbExclamation, "Informasi")
                    Return
                End If

                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_trun.EditValue, kdgudang, kdbar, qtykecil, 0, "Turun Barang (Kanvas)(Del)", kdsupir, tnopol.Text.Trim)

                dv1.Delete(Me.BindingContext(dv1).Position)

                Dim sqlup1 As String = String.Format("update trturun_br set netto={0} where nobukti='{1}'", Replace(GridView1.Columns("jumlah").SummaryItem.SummaryValue, ",", "."), tbukti.Text.Trim)
                Using cmdup1 As OleDbCommand = New OleDbCommand(sqlup1, cn, sqltrans)
                    cmdup1.ExecuteNonQuery()
                End Using

                sqltrans.Commit()

            End If

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

End Class