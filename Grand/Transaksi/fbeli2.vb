Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fbeli2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private tgl_old As String
    Private nopol_old As String
    Private supir_old As String

    Private Sub kosongkan()
        tbukti.Text = "<< New >>"

        tkd_supir.Text = ""
        tnama_supir.Text = ""
        tnosj.Text = ""
        tnote.Text = ""

        opengrid()

    End Sub

    Private Sub opengrid()

        Dim sql As String = String.Format("SELECT     trbeli2.noid, ms_gudang.kd_gudang, ms_gudang.nama_gudang, ms_barang.kd_barang, ms_barang.nama_barang, trbeli2.qty, trbeli2.satuan, trbeli2.harga, " & _
            "trbeli2.jumlah, trbeli2.qtykecil, trbeli2.hargakecil, ms_barang.qty1, ms_barang.qty2, ms_barang.qty3 " & _
            "FROM         trbeli2 INNER JOIN " & _
            "ms_barang ON trbeli2.kd_barang = ms_barang.kd_barang INNER JOIN " & _
            "ms_gudang ON trbeli2.kd_gudang = ms_gudang.kd_gudang where trbeli2.nobukti='{0}'", tbukti.Text.Trim)


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

    Private Sub loadGrid()

        Dim sql As String = String.Format("SELECT     ms_gudang.kd_gudang, ms_gudang.nama_gudang, ms_barang.kd_barang, ms_barang.nama_barang, tradm_gud2.qtyin AS qty, tradm_gud2.satuan, " & _
                      "tradm_gud2.qtyinkecil AS qtykecil, ms_barang.qty1, ms_barang.qty2, ms_barang.qty3,0 as noid,0 as harga,0 as jumlah,0 as hargakecil " & _
                        "FROM         tradm_gud INNER JOIN " & _
                      "tradm_gud2 ON tradm_gud.nobukti = tradm_gud2.nobukti INNER JOIN " & _
                      "ms_gudang ON tradm_gud.kd_gudang = ms_gudang.kd_gudang INNER JOIN " & _
                      "ms_barang ON tradm_gud2.kd_barang = ms_barang.kd_barang " & _
                        "WHERE tradm_gud.jenistrans='TR PEMB' and tradm_gud.nobukti_gd='{0}'", tnosj.Text.Trim)


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

        Dim sql As String = String.Format("SELECT     trbeli.nobukti, trbeli.tanggal, trbeli.kd_supp, trbeli.nopol, trbeli.kd_karyawan, ms_pegawai.nama_karyawan, trbeli.nosj, trbeli.tglsj, trbeli.note " & _
            "FROM         trbeli INNER JOIN " & _
            "ms_supplier ON trbeli.kd_supp = ms_supplier.kd_supp INNER JOIN " & _
            "ms_pegawai ON trbeli.kd_karyawan = ms_pegawai.kd_karyawan where trbeli.nobukti='{0}'", nobukti)

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

                        tgl_old = ttgl.EditValue

                        tsupp.EditValue = dread("kd_supp").ToString
                        tnopol.EditValue = dread("nopol").ToString

                        nopol_old = dread("nopol").ToString

                        tkd_supir.EditValue = dread("kd_karyawan").ToString

                        supir_old = dread("kd_karyawan").ToString

                        tnama_supir.EditValue = dread("nama_karyawan").ToString
                        tnosj.EditValue = dread("nosj").ToString
                        ttgl_sj.EditValue = DateValue(dread("tglsj").ToString)
                        tnote.EditValue = dread("note").ToString

                        If tnosj.EditValue.ToString.Trim.Length = 0 Then
                            btadd.Enabled = True
                            btdel.Enabled = True
                        Else
                            btadd.Enabled = False
                            btdel.Enabled = False
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

        Dim sql As String = String.Format("select max(nobukti) from trbeli where  nobukti like '%BL.{0}%'", tahunbulan)

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

        Return String.Format("BL.{0}{1}{2}", tahun, bulan, kbukti)

    End Function


    Private Function cek_nosj(ByVal cn As OleDbConnection) As Boolean

        Dim hasil As Boolean = False

        Dim sql As String = String.Format("select nobukti from trbeli where nosj='{0}' and sbatal=0", tnosj.Text.Trim)
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
                If tnosj.Text.Trim.Length > 0 Then
                    If cek_nosj(cn) = True Then
                        MsgBox("No surat jalan sudah diinput..", vbOKOnly + vbInformation, "Informasi")
                        Return
                    End If
                End If
                
            End If

            sqltrans = cn.BeginTransaction

            Dim cmd As OleDbCommand
            Dim cmdtoko As OleDbCommand

            If addstat Then
                Dim bukti As String = cekbukti(cn, sqltrans)
                tbukti.EditValue = bukti

                '1 . update faktur
                Dim sqlin_faktur As String = String.Format("insert into trbeli (nobukti,tanggal,kd_supp,kd_karyawan,nopol,nosj,tglsj,note,brutto,netto) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8},{9})", tbukti.Text.Trim, _
                                                            convert_date_to_eng(ttgl.EditValue), tsupp.EditValue, tkd_supir.EditValue, tnopol.EditValue, tnosj.Text.Trim, convert_date_to_eng(ttgl_sj.EditValue), tnote.Text.Trim, Replace(GridView1.Columns("jumlah").SummaryItem.SummaryValue, ",", "."), GridView1.Columns("jumlah").SummaryItem.SummaryValue)


                cmd = New OleDbCommand(sqlin_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()

                '2. update hutang supp
                Dim sqltoko As String = String.Format("update ms_supplier set jmlhutang=jmlhutang + {0} where kd_supp='{1}'", GridView1.Columns("jumlah").SummaryItem.SummaryValue, tsupp.EditValue)

                cmdtoko = New OleDbCommand(sqltoko, cn, sqltrans)
                cmdtoko.ExecuteNonQuery()

                If tnosj.EditValue.ToString.Trim.Length > 0 Then

                    'update adm gudang
                    Dim sqladm As String = String.Format("update tradm_gud set sambil=1 where jenistrans='TR PEMB' and nobukti_gd='{0}'", tnosj.Text.Trim)

                    Using cmdadm As OleDbCommand = New OleDbCommand(sqladm, cn, sqltrans)
                        cmdadm.ExecuteNonQuery()
                    End Using

                End If

                Clsmy.InsertToLog(cn, "btbeli", 1, 0, 0, 0, tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), sqltrans)

            Else

                '1. update faktur
                Dim sqlup_faktur As String = String.Format("update trbeli set tanggal='{0}',kd_supp='{1}',kd_karyawan='{2}',nopol='{3}',nosj='{4}',tglsj='{5}',note='{6}',brutto={7},netto={8} where nobukti='{9}'", convert_date_to_eng(ttgl.EditValue), tsupp.EditValue, tkd_supir.EditValue, tnopol.EditValue, tnosj.Text.Trim, convert_date_to_eng(ttgl_sj.EditValue), tnote.Text.Trim, Replace(GridView1.Columns("jumlah").SummaryItem.SummaryValue, ",", "."), Replace(GridView1.Columns("jumlah").SummaryItem.SummaryValue, ",", "."), tbukti.Text.Trim)

                cmd = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()


                '2. update hutang supp
                Dim sqlct As String = String.Format("select netto,kd_supp from trbeli where nobukti='{0}'", tbukti.Text.Trim)

                Dim cmdt As OleDbCommand = New OleDbCommand(sqlct, cn, sqltrans)
                Dim drdt As OleDbDataReader = cmdt.ExecuteReader

                If drdt.Read Then
                    If IsNumeric(drdt("netto").ToString) Then

                        Dim nett_sebelum As Double = drdt("netto").ToString

                        Dim sqluptoko As String = String.Format("update ms_supplier set jmlhutang=jmlhutang - {0} where kd_supp='{1}'", Replace(nett_sebelum, ",", "."), tsupp.EditValue)
                        Dim sqluptoko2 As String = String.Format("update ms_supplier set jmlhutang=jmlhutang + {0} where kd_supp='{1}'", Replace(GridView1.Columns("jumlah").SummaryItem.SummaryValue, ",", "."), tsupp.EditValue)

                        Dim cmdtk As OleDbCommand = New OleDbCommand(sqluptoko, cn, sqltrans)
                        cmdtk.ExecuteNonQuery()

                        Dim cmdtk2 As OleDbCommand = New OleDbCommand(sqluptoko2, cn, sqltrans)
                        cmdtk2.ExecuteNonQuery()

                    End If
                End If
                drdt.Close()


                If tnosj.EditValue.ToString.Trim.Length > 0 Then
                    'update adm gudang
                    Dim sqladm As String = String.Format("update tradm_gud set sambil=1 where jenistrans='TR PEMB' and nobukti_gd='{0}'", tnosj.Text.Trim)

                    Using cmdadm As OleDbCommand = New OleDbCommand(sqladm, cn, sqltrans)
                        cmdadm.ExecuteNonQuery()
                    End Using
                End If

                Clsmy.InsertToLog(cn, "btbeli", 0, 1, 0, 0, tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), sqltrans)

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
            Dim harga As String = dv1(i)("harga").ToString
            Dim disc_per As String = 0
            Dim disc_rp As String = 0
            Dim jumlah As String = dv1(i)("jumlah").ToString
            Dim qtykecil As String = dv1(i)("qtykecil").ToString
            Dim hargakecil As String = dv1(i)("hargakecil").ToString
            Dim jumlah0 As String = 0
            Dim noid As String = dv1(i)("noid").ToString

            cek_tglhist(cn, sqltrans, kdbar)

            If addstat = True Then

                '1. insert faktur_to
                Dim sqlins As String = String.Format("insert into trbeli2 (nobukti,kd_barang,kd_gudang,satuan,qty,qtykecil,harga,jumlah,hargakecil) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", tbukti.EditValue, _
                                                        kdbar, kdgud, satuan, Replace(qty, ",", "."), Replace(qtykecil, ",", "."), Replace(harga, ",", "."), Replace(jumlah, ",", "."), Replace(hargakecil, ",", "."))


                cmd = New OleDbCommand(sqlins, cn, sqltrans)
                cmd.ExecuteNonQuery()

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
                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, kdgud, kdbar, qtykecil, 0, "Beli", tkd_supir.Text.Trim, tnopol.EditValue)


            Else

                Dim sqlc As String = String.Format("select qtykecil from trbeli2 where noid={0}", noid)
                Dim cmds As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
                Dim drds As OleDbDataReader = cmds.ExecuteReader

                Dim shasil As Boolean = False

                If drds.HasRows Then
                    If drds.Read Then

                        If IsNumeric(drds(0).ToString) Then

                            shasil = True

                            '1. update faktur to
                            Dim sqlup As String = String.Format("update trbeli2 set kd_barang='{0}',kd_gudang='{1}',satuan='{2}',qty={3},qtykecil={4},harga={5},jumlah={6},hargakecil={7} where nobukti='{8}' and noid={9}", _
                                                               kdbar, kdgud, satuan, Replace(qty, ",", "."), Replace(qtykecil, ",", "."), Replace(harga, ",", "."), Replace(jumlah, ",", "."), Replace(hargakecil, ",", "."), tbukti.Text.Trim, noid)

                            cmd = New OleDbCommand(sqlup, cn, sqltrans)
                            cmd.ExecuteNonQuery()

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

                                    If Not IsNothing(sqltrans) Then
                                        sqltrans.Rollback()
                                    End If

                                    MsgBox(hasilplusmin2, vbOKOnly + vbExclamation, "Informasi")
                                    hasil = "error"
                                    Exit For
                                End If

                            End If

                            '3. insert to hist stok

                            If DateValue(tgl_old) <> DateValue(ttgl.EditValue) Or supir_old <> tkd_supir.EditValue Or nopol_old <> tnopol.EditValue Then
                                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, kdgud, kdbar, 0, drds(0).ToString, "Beli (Edit)", supir_old, nopol_old)
                            Else
                                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, kdgud, kdbar, 0, drds(0).ToString, "Beli (Edit)", tkd_supir.Text.Trim, tnopol.EditValue)
                            End If


                            Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, kdgud, kdbar, qtykecil, 0, "Beli (Edit)", tkd_supir.Text.Trim, tnopol.EditValue)

                        End If

                    End If
                End If

                If shasil = False Then

                    '1. insert faktur_to
                    Dim sqlins As String = String.Format("insert into trbeli2 (nobukti,kd_barang,kd_gudang,satuan,qty,qtykecil,harga,jumlah,hargakecil) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", tbukti.EditValue, _
                                                            kdbar, kdgud, satuan, Replace(qty, ",", "."), Replace(qtykecil, ",", "."), Replace(harga, ",", "."), Replace(jumlah, ",", "."), Replace(hargakecil, ",", "."))


                    cmd = New OleDbCommand(sqlins, cn, sqltrans)
                    cmd.ExecuteNonQuery()

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
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, kdgud, kdbar, qtykecil, 0, "Beli", tkd_supir.Text.Trim, tnopol.EditValue)

                End If


            End If

        Next

        If Not hasil.Equals("error") Then
            hasil = "ok"
        End If

        Return hasil

    End Function

    Private Sub cek_tglhist(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal kdbarang As String)

        Dim sql As String = String.Format("select tglbeli from ms_barang where kd_barang='{0}'", kdbarang)
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
            sqlup = String.Format("update ms_barang set tglbeli='{0}' where kd_barang='{1}'", convert_date_to_eng(ttgl.EditValue), kdbarang)

            Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                cmdup.ExecuteNonQuery()
            End Using

        Else

            If DateValue(ttgl.EditValue) > DateValue(tglhasil) Then
                sqlup = String.Format("update ms_barang set tglbeli='{0}' where kd_barang='{1}'", convert_date_to_eng(ttgl.EditValue), kdbarang)

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
        orow("kd_supp") = tsupp.EditValue
        orow("nama_supp") = tsupp.Text.Trim
        orow("nopol") = tnopol.EditValue
        orow("nosj") = tnosj.Text.Trim
        orow("jmlbayar") = 0
        orow("netto") = GridView1.Columns("jumlah").SummaryItem.SummaryValue
        orow("sbatal") = 0
        orow("kd_karyawan") = tkd_supir.Text.Trim

        dv.EndInit()

    End Sub

    Private Sub updateview()

        dv(position)("nobukti") = tbukti.Text.Trim
        dv(position)("tanggal") = ttgl.Text.Trim
        dv(position)("kd_supp") = tsupp.EditValue
        dv(position)("nama_supp") = tsupp.Text.Trim
        dv(position)("nopol") = tnopol.EditValue
        dv(position)("nosj") = tnosj.Text.Trim
        dv(position)("netto") = GridView1.Columns("jumlah").SummaryItem.SummaryValue
        dv(position)("kd_karyawan") = tkd_supir.Text.Trim
        '  dv(position)("sbatal") = 0

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

    Private Sub isi_supplier()

        Const sql As String = "select kd_supp,nama_supp from ms_supplier"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet
        Dim dvg As DataView

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvg = dvm.CreateDataView(ds.Tables(0))

            tsupp.Properties.DataSource = dvg

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
        Using fkar2 As New fbeli3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0}
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

        Using fkar2 As New fbeli3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = Me.BindingContext(dv1).Position}
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

            Dim cn As OleDbConnection = Nothing
            Dim sqltrans As OleDbTransaction = Nothing

            Try

                Dim qtykecil As Integer = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtykecil").ToString)
                Dim kdbar As String = dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString
                Dim kdgud As String = dv1(Me.BindingContext(dv1).Position)("kd_gudang").ToString

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                sqltrans = cn.BeginTransaction

                Dim sql As String = String.Format("delete from trbeli2 where noid={0}", dv1(Me.BindingContext(dv1).Position)("noid").ToString)

                Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()

                ' Clsmy.InsertToLog(cn, "btfaktur_to", 0, 0, 1, 0, tbukti.Text.Trim, tnama_toko.Text.Trim, sqltrans)

                '2. update barang
                Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, False, False, False)
                If Not hasilplusmin.Equals("ok") Then

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                    GoTo langsung
                End If

                '3. insert to hist stok
                If DateValue(tgl_old) <> DateValue(ttgl.EditValue) Or supir_old <> tkd_supir.EditValue Or nopol_old <> tnopol.EditValue Then
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, kdgud, kdbar, 0, qtykecil, "Beli (Del)", supir_old, nopol_old)
                Else
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, kdgud, kdbar, 0, qtykecil, "Beli (Del)", tkd_supir.Text.Trim, tnopol.EditValue)
                End If


                dv1.Delete(Me.BindingContext(dv1).Position)
                ' tbrutto.EditValue = GridView1.Columns("jumlah").SummaryItem.SummaryValue

                ' update headernya .............................................................

                '1. update faktur
                Dim sqlup_faktur As String = String.Format("update trbeli set brutto={0},netto={1} where nobukti='{2}'", Replace(GridView1.Columns("jumlah").SummaryItem.SummaryValue, ",", "."), Replace(GridView1.Columns("jumlah").SummaryItem.SummaryValue, ",", "."), tbukti.Text.Trim)

                Using cmdup As OleDbCommand = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                    cmdup.ExecuteNonQuery()
                End Using

                '2. update hutang supp
                Dim sqlct As String = String.Format("select netto,kd_supp from trbeli where nobukti='{0}'", tbukti.Text.Trim)

                Dim cmdt As OleDbCommand = New OleDbCommand(sqlct, cn, sqltrans)
                Dim drdt As OleDbDataReader = cmdt.ExecuteReader

                If drdt.Read Then
                    If IsNumeric(drdt("netto").ToString) Then

                        Dim nett_sebelum As Double = drdt("netto").ToString

                        Dim sqluptoko As String = String.Format("update ms_supplier set jmlhutang=jmlhutang - {0} where kd_supp='{1}'", Replace(nett_sebelum, ",", "."), tsupp.EditValue)
                        Dim sqluptoko2 As String = String.Format("update ms_supplier set jmlhutang=jmlhutang + {0} where kd_supp='{1}'", Replace(GridView1.Columns("jumlah").SummaryItem.SummaryValue, ",", "."), tsupp.EditValue)

                        Dim cmdtk As OleDbCommand = New OleDbCommand(sqluptoko, cn, sqltrans)
                        cmdtk.ExecuteNonQuery()

                        Dim cmdtk2 As OleDbCommand = New OleDbCommand(sqluptoko2, cn, sqltrans)
                        cmdtk2.ExecuteNonQuery()

                    End If
                End If
                drdt.Close()


                ' akhir update headernya .......................................................

                sqltrans.Commit()

                MsgBox("Data dihapus...", vbOKOnly + vbInformation, "Informasi")

langsung:


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

        If tsupp.EditValue = "" Then
            MsgBox("Supplier tidak boleh kosong..", vbOKOnly + vbInformation, "Informasi")
            tsupp.Focus()
            Return
        End If

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

        'If tnosj.Text.Trim.Length = 0 Then
        '    MsgBox("Surat jalan boleh kosong..", vbOKOnly + vbInformation, "Informasi")
        '    tnosj.Focus()
        '    Return
        'End If

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

        isi_supplier()
        isi_nopol()

        ttgl.EditValue = Date.Now
        ttgl_sj.EditValue = Date.Now

        If addstat Then
            kosongkan()
        Else
            isi()
        End If

    End Sub

    Private Sub bts_sj_Click(sender As System.Object, e As System.EventArgs) Handles bts_sj.Click

        Dim fs As New fsbarang_masuk With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tnosj.Text = fs.get_NoSJ
        tnosj_Validated(sender, Nothing)

        'loadGrid()

    End Sub

    Private Sub tnosj_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tnosj.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_sj_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tnosj_Validated(sender As System.Object, e As System.EventArgs) Handles tnosj.Validated

        Dim cn As OleDbConnection = Nothing
        Dim nohasil As String = ""

        btadd.Enabled = True
        btdel.Enabled = True

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select a.nobukti,a.tanggal,a.tglmuat,a.tglberangkat,a.nobukti_gd,a.nopol,b.kd_karyawan,b.nama_karyawan,a.kd_gudang " & _
             "from tradm_gud a inner join ms_pegawai b on a.kd_supir=b.kd_karyawan " & _
                "where a.jenistrans='TR PEMB' and a.sambil=0 and a.nobukti_gd='{0}'", tnosj.Text.Trim)

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader


            If drd.Read Then
                If drd("nobukti_gd").ToString.Equals("") Then
                    nohasil = ""
                Else
                    nohasil = drd("nobukti_gd").ToString
                    ttgl_sj.EditValue = DateValue(drd("tanggal").ToString)
                    tnopol.EditValue = drd("nopol").ToString
                    tkd_supir.Text = drd("kd_karyawan").ToString
                    tnama_supir.Text = drd("nama_karyawan").ToString

                    btadd.Enabled = False
                    btdel.Enabled = False

                End If
            Else
                nohasil = ""
            End If

            If nohasil = "" Then
                '  tnopol.Text = ""
                tkd_supir.Text = ""
                tnama_supir.Text = ""
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

        loadGrid()

    End Sub

    Private Sub tnopol_Validated(sender As System.Object, e As System.EventArgs) Handles tnopol.Validated

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select kd_supir from ms_supirkenek where nopol='{0}'", tnopol.EditValue)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            Dim ada As Boolean = False

            If drd.Read Then
                If Not drd(0).ToString.Equals("") Then
                    ada = True
                    tkd_supir.EditValue = drd(0).ToString
                    tkd_supir_Validated(sender, Nothing)
                End If
            End If
            drd.Close()

            If ada = False Then
                tkd_supir.Text = ""
                tnama_supir.Text = ""
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

End Class