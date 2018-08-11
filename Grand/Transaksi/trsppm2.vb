Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy
Imports DevExpress.XtraReports.UI

Public Class trsppm2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Dim dvnopol As DataView

    Private tglmuat_old As String
    Private gudang_old As String
    Private supir_old As String
    Private nopol_old As String

    Private iscek_supsal As Boolean

    Public statprint As Boolean

    Private Sub kosongkan()
        tbukti.Text = "<< New >>"

        tkd_sales.Text = ""
        tnama_sales.Text = ""

        tkd_supir.EditValue = ""
        tnama_supir.EditValue = ""

        tkd_ken1.EditValue = ""
        tnama_ken1.EditValue = ""

        tkd_ken2.EditValue = ""
        tnama_ken2.EditValue = ""

        tkd_ken3.EditValue = ""
        tnama_ken3.EditValue = ""

        tket.EditValue = ""

        opengrid()

    End Sub

    Private Sub opengrid()

        Dim sql As String = String.Format("SELECT     trspm2.noid, trspm2.kd_gudang, trspm2.kd_barang, ms_barang.nama_barang, trspm2.qty, trspm2.satuan, trspm2.harga, trspm2.jumlah, trspm2.qtykecil, trspm2.hargakecil " & _
                "FROM         trspm2 INNER JOIN " & _
                "ms_barang ON trspm2.kd_barang = ms_barang.kd_barang INNER JOIN ms_gudang ON trspm2.kd_gudang = ms_gudang.kd_gudang where trspm2.nobukti='{0}'", tbukti.Text.Trim)


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
        Dim sql As String = String.Format("SELECT     trspm.nobukti, trspm.tanggal, trspm.tglmuat, trspm.tglberangkat, trspm.nopol, trspm.kd_gudang, trspm.kd_supir, supir.nama_karyawan AS nama_supir, " & _
                      "trspm.kd_kenek1, kenek1.nama_karyawan AS nama_kenek1, trspm.kd_kenek2, kenek2.nama_karyawan AS nama_kenek2, trspm.kd_kenek3, " & _
                      "kenek3.nama_karyawan AS nama_kenek3, trspm.note,trspm.kd_sales,sales.nama_karyawan as nama_sales " & _
                    "FROM         trspm INNER JOIN " & _
                     "ms_pegawai AS supir ON trspm.kd_supir = supir.kd_karyawan LEFT OUTER JOIN " & _
                     "ms_pegawai AS kenek3 ON trspm.kd_kenek3 = kenek3.kd_karyawan LEFT OUTER JOIN " & _
                     "ms_pegawai AS kenek2 ON trspm.kd_kenek2 = kenek2.kd_karyawan LEFT OUTER JOIN " & _
                      "ms_pegawai AS kenek1 ON trspm.kd_kenek1 = kenek1.kd_karyawan LEFT OUTER JOIN " & _
                      "ms_pegawai As sales ON trspm.kd_sales=sales.kd_karyawan " & _
                      "where trspm.nobukti='{0}'", nobukti)

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
                        tnopol.EditValue = dread("nopol").ToString

                        nopol_old = dread("nopol").ToString

                        tgudang.EditValue = dread("kd_gudang").ToString

                        gudang_old = tgudang.EditValue

                        tkd_sales.EditValue = dread("kd_sales").ToString
                        tnama_sales.EditValue = dread("nama_sales").ToString

                        tkd_supir.EditValue = dread("kd_supir").ToString

                        supir_old = dread("kd_supir").ToString

                        tnama_supir.EditValue = dread("nama_supir").ToString

                        tkd_ken1.EditValue = dread("kd_kenek1").ToString
                        tnama_ken1.EditValue = dread("nama_kenek1").ToString

                        tkd_ken2.EditValue = dread("kd_kenek2").ToString
                        tnama_ken2.EditValue = dread("nama_kenek2").ToString

                        tkd_ken3.EditValue = dread("kd_kenek3").ToString
                        tnama_ken3.EditValue = dread("nama_kenek3").ToString

                        ttgl.EditValue = DateValue(dread("tanggal").ToString)
                        ttgl_mt.EditValue = DateValue(dread("tglmuat").ToString)

                        tglmuat_old = ttgl_mt.EditValue

                        ttgl_krm.EditValue = DateValue(dread("tglberangkat").ToString)


                        tket.EditValue = dread("note").ToString

                        Dim sqlcekfak As String = String.Format("select nobukti from trfaktur_to where tanggal='{0}' and kd_karyawan='{1}' and jnis_fak='K'", convert_date_to_eng(ttgl_krm.EditValue), tkd_sales.Text.Trim)
                        Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcekfak, cn)
                        Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader
                        If drdcek.Read Then
                            If Not drdcek(0).ToString.Equals("") Then
                                ttgl_krm.Enabled = False
                            Else
                                ttgl_krm.Enabled = True
                            End If
                        End If
                        drdcek.Close()

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

    Private Sub isi_nopol()

        Const sql As String = "SELECT   ms_gudang.kd_gudang as nopol " & _
            "FROM         ms_gudang INNER JOIN ms_kendaraan ON ms_gudang.nopol = ms_kendaraan.nopol where ms_kendaraan.aktif=1"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvnopol = dvm.CreateDataView(ds.Tables(0))

            tnopol.Properties.DataSource = dvnopol

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

        Dim sql As String = String.Format("select max(nobukti) from trspm where nobukti like '%SPM.{0}%'", tahunbulan)

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

        Return String.Format("SPM.{0}{1}{2}", tahun, bulan, kbukti)

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
                Dim sqlin As String = String.Format("insert into trspm (nobukti,tanggal,tglmuat,tglberangkat,nopol,kd_gudang,kd_supir,kd_kenek1,kd_kenek2,kd_kenek3,note,netto,kd_sales) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',{11},'{12}')", _
                                                        tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_mt.EditValue), convert_date_to_eng(ttgl_krm.EditValue), tnopol.EditValue, tgudang.EditValue, tkd_supir.Text.Trim, _
                                                        tkd_ken1.Text.Trim, tkd_ken2.Text.Trim, tkd_ken3.Text.Trim, tket.Text.Trim, Replace(GridView1.Columns("jumlah").SummaryItem.SummaryValue, ",", "."), tkd_sales.Text.Trim)


                cmd = New OleDbCommand(sqlin, cn, sqltrans)
                cmd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btspm", 1, 0, 0, 0, tbukti.Text.Trim, ttgl.EditValue, sqltrans)



            Else

                '1. update rekap
                Dim sqlup As String = String.Format("update trspm set tanggal='{0}',tglmuat='{1}',tglberangkat='{2}',nopol='{3}',kd_gudang='{4}',kd_supir='{5}',kd_kenek1='{6}',kd_kenek2='{7}',kd_kenek3='{8}',note='{9}',netto={10},kd_sales='{11}' where nobukti='{12}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_mt.EditValue), convert_date_to_eng(ttgl_krm.EditValue), tnopol.EditValue, tgudang.EditValue, tkd_supir.Text.Trim, _
                                                        tkd_ken1.Text.Trim, tkd_ken2.Text.Trim, tkd_ken3.Text.Trim, tket.Text.Trim, Replace(GridView1.Columns("jumlah").SummaryItem.SummaryValue, ",", "."), tkd_sales.Text.Trim, tbukti.Text.Trim)

                cmd = New OleDbCommand(sqlup, cn, sqltrans)
                cmd.ExecuteNonQuery()


                Clsmy.InsertToLog(cn, "btspm", 0, 1, 0, 0, tbukti.Text.Trim, ttgl.EditValue, sqltrans)
            End If

            If simpan2(cn, sqltrans) = "ok" Then
                '------------------------------

                cek_difaktur(cn, sqltrans)

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
                        Using fs As New trspm2_pr With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
                            fs.ShowDialog(Me)
                            load_print(fs.get_konfirm)
                        End Using

                    End If

                    kosongkan()
                    tnopol.Focus()
                Else
                    close_wait()

                    If statprint Then
                        Using fs As New trspm2_pr With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
                            fs.ShowDialog(Me)
                            load_print(fs.get_konfirm)
                        End Using

                    End If

                    Me.Close()
                End If

                '----------------------------------
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

        'Dim cmdc As OleDbCommand
        'Dim drdc As OleDbDataReader
        Dim hasil As String = ""

        Dim kdbar As String
        Dim kdgudang As String
        Dim satuan As String
        Dim qty, qtykecil As Integer
        Dim harga, jumlah, hargakecil As String

        For i As Integer = 0 To dv1.Count - 1

            kdbar = dv1(i)("kd_barang").ToString
            kdgudang = dv1(i)("kd_gudang").ToString
            satuan = dv1(i)("satuan").ToString
            qty = Integer.Parse(dv1(i)("qty").ToString)
            qtykecil = Integer.Parse(dv1(i)("qtykecil").ToString)
            harga = dv1(i)("harga").ToString
            jumlah = dv1(i)("jumlah").ToString
            hargakecil = dv1(i)("hargakecil").ToString

            If dv1(i)("noid").Equals(0) Then
                Dim sqlins As String = String.Format("insert into trspm2 (nobukti,kd_gudang,kd_barang,satuan,qty,harga,jumlah,qtykecil,hargakecil) values('{0}','{1}','{2}','{3}',{4},{5},{6},{7},{8})", tbukti.Text.Trim, _
                                                     kdgudang, kdbar, satuan, qty, Replace(harga, ",", "."), Replace(jumlah, ",", "."), qtykecil, Replace(hargakecil, ",", "."))

                Using cmd As OleDbCommand = New OleDbCommand(sqlins, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using


                '2. update barang
                Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgudang, False, False, False)
                If Not hasilplusmin.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                    hasil = "error"
                    Exit For
                Else

                    Dim hasilplusmin2 As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, tgudang.EditValue, True, False, False)

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

                'If addstat = False Then
                '    If DateValue(tglmuat_old) <> DateValue(ttgl_mt.EditValue) Or tgudang.EditValue <> gudang_old Then
                '        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tglmuat_old, kdgudang, kdbar, qtykecil, 0, "Perintah Muat Barang (Kanvas) (Edit)")
                '        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tglmuat_old, gudang_old, kdbar, 0, qtykecil, "Perintah Muat Barang (Kanvas) (Edit)")
                '    End If
                'End If

                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, kdgudang, kdbar, 0, qtykecil, "Muat Barang (Kanvas)", tkd_supir.Text.Trim, tnopol.EditValue)
                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, tgudang.Text.Trim, kdbar, qtykecil, 0, "Muat Barang (Kanvas)", tkd_supir.Text.Trim, tnopol.EditValue)

            Else

                If addstat = False Then

                    If tgudang.EditValue <> gudang_old Then

                        Dim hasilplusmin_old As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgudang, True, False, False)
                        If Not hasilplusmin_old.Equals("ok") Then
                            close_wait()

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox(hasilplusmin_old, vbOKOnly + vbExclamation, "Informasi")
                            hasil = "error"
                            Exit For
                        Else

                            Dim hasilplusmin_old2 As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, gudang_old, False, False, False)

                            If Not hasilplusmin_old2.Equals("ok") Then
                                close_wait()

                                If Not IsNothing(sqltrans) Then
                                    sqltrans.Rollback()
                                End If

                                MsgBox(hasilplusmin_old2, vbOKOnly + vbExclamation, "Informasi")
                                hasil = "error"
                                Exit For

                            End If
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
                        Exit For
                    Else

                        Dim hasilplusmin2 As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, tgudang.EditValue, True, False, False)

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

                    If addstat = False Then
                        If DateValue(tglmuat_old) <> DateValue(ttgl_mt.EditValue) Or tgudang.EditValue <> gudang_old Or nopol_old <> tnopol.EditValue Or supir_old <> tkd_supir.EditValue Then
                            Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tglmuat_old, kdgudang, kdbar, qtykecil, 0, "Muat Barang (Kanvas)(Edit)", supir_old, nopol_old)
                            Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tglmuat_old, gudang_old, kdbar, 0, qtykecil, "Muat Barang (Kanvas)(Edit)", supir_old, nopol_old)
                        End If
                    End If

                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, kdgudang, kdbar, 0, qtykecil, "Muat Barang (Kanvas)(Edit)", tkd_supir.Text.Trim, tnopol.EditValue)
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, tgudang.Text.Trim, kdbar, qtykecil, 0, "Muat Barang (Kanvas)(Edit)", tkd_supir.Text.Trim, tnopol.EditValue)


                End If


            End If

        Next

        If Not hasil.Equals("error") Then
            hasil = "ok"
        End If

        Return hasil

    End Function

    Private Sub load_print(ByVal tipe As Integer)

        If tipe = 0 Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = String.Format("SELECT     trspm.nobukti, trspm.tanggal, trspm.tglmuat, trspm.tglberangkat, trspm.nopol, ms_supir.nama_karyawan AS nama_supir, " & _
                      "ms_kenek1.nama_karyawan AS nama_kenek1, ms_kenek2.nama_karyawan AS nama_kenek2, ms_kenek3.nama_karyawan AS nama_kenek3, " & _
                      "ms_gudang.kd_gudang, ms_gudang.nama_gudang, ms_barang.kd_barang, ms_barang.nama_lap as nama_barang, trspm2.qty, trspm2.satuan,ms_barang.nohrus " & _
                        "FROM         trspm INNER JOIN " & _
                      "trspm2 ON trspm.nobukti = trspm2.nobukti INNER JOIN " & _
                      "ms_gudang ON trspm2.kd_gudang = ms_gudang.kd_gudang INNER JOIN " & _
                      "ms_barang ON trspm2.kd_barang = ms_barang.kd_barang LEFT OUTER JOIN " & _
                      "ms_pegawai AS ms_kenek3 ON trspm.kd_kenek3 = ms_kenek3.kd_karyawan LEFT OUTER JOIN " & _
                      "ms_pegawai AS ms_kenek2 ON trspm.kd_kenek2 = ms_kenek2.kd_karyawan LEFT OUTER JOIN " & _
                      "ms_pegawai AS ms_kenek1 ON trspm.kd_kenek1 = ms_kenek1.kd_karyawan LEFT OUTER JOIN " & _
                      "ms_pegawai AS ms_supir ON trspm.kd_supir = ms_supir.kd_karyawan " & _
                      "WHERE ms_barang.jenis='FISIK' and trspm.sbatal=0 and trspm.nobukti='{0}'", tbukti.Text.Trim)

            Dim ds As DataSet = New dsspmx
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_spm() With {.DataSource = ds.Tables(0)}
            rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)

            If tipe = 1 Then
                rrekap.xjudul.Text = "SURAT  PERINTAH  MUAT  BARANG (GALLON)"
            ElseIf tipe = 2 Then
                rrekap.xjudul.Text = "SURAT  PERINTAH  MUAT  BARANG (DUS)"
            Else
                rrekap.xjudul.Text = "SURAT  PERINTAH  MUAT  BARANG"
            End If

            rrekap.DataMember = rrekap.DataMember
            rrekap.PrinterName = varprinter2
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

                Dim sqldel As String = String.Format("delete from trspm2 where noid={0}", noid)
                Using cmd As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

                Dim kdbar As String
                Dim kdgudang As String
                Dim satuan As String
                Dim qty, qtykecil As Integer
                Dim harga, jumlah, hargakecil As String

                kdbar = dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString
                kdgudang = dv1(Me.BindingContext(dv1).Position)("kd_gudang").ToString
                satuan = dv1(Me.BindingContext(dv1).Position)("satuan").ToString
                qty = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qty").ToString)
                qtykecil = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtykecil").ToString)
                harga = dv1(Me.BindingContext(dv1).Position)("harga").ToString
                jumlah = dv1(Me.BindingContext(dv1).Position)("jumlah").ToString
                hargakecil = dv1(Me.BindingContext(dv1).Position)("hargakecil").ToString


                If tgudang.EditValue <> gudang_old Then

                    '2. update barang
                    Dim hasilplusmin_old As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgudang, True, False, False)
                    If Not hasilplusmin_old.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin_old, vbOKOnly + vbExclamation, "Informasi")
                        GoTo langsung
                    Else

                        Dim hasilplusmin_old2 As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, gudang_old, False, False, False)

                        If Not hasilplusmin_old2.Equals("ok") Then
                            close_wait()

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox(hasilplusmin_old2, vbOKOnly + vbExclamation, "Informasi")
                            GoTo langsung
                        End If
                    End If

                End If

                '2. update barang
                Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgudang, True, False, False)
                If Not hasilplusmin.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                    GoTo langsung
                Else

                    Dim hasilplusmin2 As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, tgudang.EditValue, False, False, False)

                    If Not hasilplusmin2.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin2, vbOKOnly + vbExclamation, "Informasi")
                        GoTo langsung
                    End If
                End If

                '3. insert to hist stok

                'If addstat = False Then
                If DateValue(tglmuat_old) <> DateValue(ttgl_mt.EditValue) Or tgudang.EditValue <> gudang_old Or supir_old <> tkd_supir.EditValue Or nopol_old <> tnopol.EditValue Then
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tglmuat_old, kdgudang, kdbar, qtykecil, 0, "Perintah Muat Barang (Kanvas)", supir_old, nopol_old)
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tglmuat_old, gudang_old, kdbar, 0, qtykecil, "Perintah Muat Barang (Kanvas)", supir_old, nopol_old)
                Else
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, kdgudang, kdbar, qtykecil, 0, "Perintah Muat Barang (Kanvas)", tkd_supir.Text.Trim, tnopol.EditValue)
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, tgudang.Text.Trim, kdbar, 0, qtykecil, "Perintah Muat Barang (Kanvas)", tkd_supir.Text.Trim, tnopol.EditValue)
                End If
                'End If

                sqltrans.Commit()

                dv1.Delete(Me.BindingContext(dv1).Position)

                ' update headernya .....
                '1. update rekap
                Dim sqlup As String = String.Format("update trspm set netto={0} where nobukti='{1}'", Replace(GridView1.Columns("jumlah").SummaryItem.SummaryValue, ",", "."), tbukti.Text.Trim)

                Using cmd As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using


                ' akhir update headernya ......................

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

    Private Sub cek_difaktur(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        Dim sqlcek As String = String.Format("select nobukti from trfaktur_to where tanggal='{0}' and kd_karyawan='{1}' and jnis_fak='K'", convert_date_to_eng(ttgl_krm.EditValue), tkd_sales.Text.Trim)
        Dim cmd As OleDbCommand = New OleDbCommand(sqlcek, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        If drd.HasRows Then
            While drd.Read

                Dim adacek2 As Boolean = False
                Dim sqlcek2 As String = String.Format("select noid from trfaktur_to4 where nobukti='{0}' and nobukti_spm='{1}'", drd(0).ToString, tbukti.Text.Trim)
                Dim cmdcek2 As OleDbCommand = New OleDbCommand(sqlcek2, cn, sqltrans)
                Dim drdcek2 As OleDbDataReader = cmdcek2.ExecuteReader

                If drdcek2.Read Then
                    If IsNumeric(drdcek2(0).ToString) Then
                        adacek2 = True
                    End If
                End If
                drdcek2.Close()

                If adacek2 = False Then

                    Dim sql As String = String.Format("insert into trfaktur_to4 (nobukti,nobukti_spm) values('{0}','{1}')", drd(0).ToString, tbukti.Text.Trim)
                    Using cmdin As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                        cmdin.ExecuteNonQuery()
                    End Using

                End If


            End While
        End If
        drd.Close()

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("sbatal") = 0
        orow("smuat") = 0
        orow("spulang") = 0
        orow("nobukti") = tbukti.Text.Trim
        orow("tanggal") = ttgl.EditValue
        orow("tglmuat") = ttgl_mt.EditValue
        orow("tglberangkat") = ttgl_krm.EditValue
        orow("kd_supir") = tkd_supir.Text.Trim
        orow("namasupir") = tnama_supir.Text.Trim
        orow("kd_sales") = tkd_sales.Text.Trim
        orow("nama_sales") = tnama_sales.Text.Trim
        orow("kd_gudang") = tgudang.Text.Trim
        orow("nopol") = tnopol.EditValue
        orow("note") = tket.Text.Trim

        dv.EndInit()

    End Sub

    Private Sub updateview()

        dv(position)("nobukti") = tbukti.Text.Trim
        dv(position)("tanggal") = ttgl.EditValue
        dv(position)("tglmuat") = ttgl_mt.EditValue
        dv(position)("tglberangkat") = ttgl_krm.EditValue
        dv(position)("kd_sales") = tkd_sales.Text.Trim
        dv(position)("nama_sales") = tnama_sales.Text.Trim
        dv(position)("kd_supir") = tkd_supir.Text.Trim
        dv(position)("namasupir") = tnama_supir.Text.Trim
        dv(position)("kd_gudang") = tgudang.Text.Trim
        dv(position)("nopol") = tnopol.EditValue
        dv(position)("note") = tket.Text.Trim

    End Sub

    Private Sub cek_supirkenek(ByVal cn As OleDbConnection, ByVal stat_cari As String)

        tkd_ken1.Text = ""
        tnama_ken1.Text = ""

        tkd_ken2.Text = ""
        tnama_ken2.Text = ""

        tkd_ken3.Text = ""
        tnama_ken3.Text = ""

        Dim sql As String = "SELECT     ms_supirkenek.noid,ms_supirkenek.kd_supir, supir.nama_karyawan AS nama_supir, ms_supirkenek.kd_kenek1, kenek1.nama_karyawan AS nama_kenek1, ms_supirkenek.kd_kenek2, " & _
                      "kenek2.nama_karyawan AS nama_kenek2, ms_supirkenek.kd_kenek3, kenek3.nama_karyawan AS nama_kenek3, ms_supirkenek.kd_sales, " & _
                      "sales.nama_karyawan AS nama_sales, ms_supirkenek.nopol " & _
        "FROM         ms_pegawai AS sales RIGHT OUTER JOIN " & _
                      "ms_supirkenek ON sales.kd_karyawan = ms_supirkenek.kd_sales LEFT OUTER JOIN " & _
                      "ms_pegawai AS kenek3 ON ms_supirkenek.kd_kenek3 = kenek3.kd_karyawan LEFT OUTER JOIN " & _
                      "ms_pegawai AS kenek1 ON ms_supirkenek.kd_kenek1 = kenek1.kd_karyawan LEFT OUTER JOIN " & _
                      "ms_pegawai AS kenek2 ON ms_supirkenek.kd_kenek2 = kenek2.kd_karyawan LEFT OUTER JOIN " & _
                      "ms_pegawai AS supir ON ms_supirkenek.kd_supir = supir.kd_karyawan"

        If stat_cari.Equals("nopol") Then
            sql = String.Format(" {0} where ms_supirkenek.kd_supir='{1}' or ms_supirkenek.kd_sales='{2}'", sql, tkd_supir.Text.Trim, tkd_sales.Text.Trim)
        ElseIf stat_cari.Equals("sales") Then
            sql = String.Format(" {0} where ms_supirkenek.kd_sales='{1}'", sql, tkd_sales.Text.Trim)
        ElseIf stat_cari.Equals("supir") Then
            sql = String.Format(" {0} where ms_supirkenek.kd_supir='{1}'", sql, tkd_supir.Text.Trim)
        End If

        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim ada As Boolean = False

        If drd.Read Then

            If IsNumeric(drd("noid").ToString) Then

                ada = True

                Dim kd_kenek1 As String = drd("kd_kenek1").ToString
                Dim kd_kenek2 As String = drd("kd_kenek2").ToString
                Dim kd_kenek3 As String = drd("kd_kenek3").ToString

                Dim kdsales As String = drd("kd_sales").ToString
                Dim kdsupir As String = drd("kd_supir").ToString

                If stat_cari.Equals("nopol") Then
                    tkd_sales.Text = kdsales
                    tnama_sales.Text = drd("nama_sales").ToString
                End If

                If stat_cari.Equals("nopol") Or stat_cari.Equals("sales") Then
                    tkd_supir.Text = kdsupir
                    tnama_supir.Text = drd("nama_supir").ToString
                End If

                tkd_ken1.Text = kd_kenek1
                tnama_ken1.Text = drd("nama_kenek1").ToString

                tkd_ken2.Text = kd_kenek2
                tnama_ken2.Text = drd("nama_kenek2").ToString

                tkd_ken3.Text = kd_kenek3
                tnama_ken3.Text = drd("nama_kenek3").ToString


            End If

        End If
        drd.Close()

        If ada = False Then
            tkd_ken1.Text = ""
            tkd_ken2.Text = ""
            tkd_ken3.Text = ""

            tnama_ken1.Text = ""
            tnama_ken2.Text = ""
            tnama_ken3.Text = ""
        End If

    End Sub

    '' sales

    Private Sub bts_sales_Click(sender As System.Object, e As System.EventArgs) Handles bts_sales.Click

        Dim fs As New fssales With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_sales.EditValue = fs.get_KODE

        tkd_sales_Validated(sender, Nothing)

    End Sub

    Private Sub tkd_sales_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_sales.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_sales_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_sales_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_sales.LostFocus
        If tkd_sales.Text.Trim.Length = 0 Then
            tkd_sales.Text = ""
            tnama_sales.Text = ""
        End If
    End Sub

    Private Sub tkd_sales_Validated(sender As Object, e As System.EventArgs) Handles tkd_sales.Validated
        If tkd_sales.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and  bagian='SALES' and kd_karyawan='{0}'", tkd_sales.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_sales.EditValue = dread("kd_karyawan").ToString
                        tnama_sales.EditValue = dread("nama_karyawan").ToString

                    Else
                        tkd_sales.EditValue = ""
                        tnama_sales.EditValue = ""

                    End If
                Else
                    tkd_sales.EditValue = ""
                    tnama_sales.EditValue = ""

                End If


                dread.Close()

                If iscek_supsal = False Then
                    iscek_supsal = True

                    cek_supirkenek(cn, "sales")

                    iscek_supsal = False
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


        Else

            tkd_supir.Text = ""
            tnama_supir.Text = ""

            tkd_ken1.Text = ""
            tnama_ken1.Text = ""

            tkd_ken2.Text = ""
            tnama_ken2.Text = ""

            tkd_ken3.Text = ""
            tnama_ken3.Text = ""

        End If
    End Sub


    '' akhir sales

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

                If iscek_supsal = False Then

                    iscek_supsal = True

                    cek_supirkenek(cn, "supir")

                    iscek_supsal = False

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


        Else

            tkd_ken1.Text = ""
            tnama_ken1.Text = ""

            tkd_ken2.Text = ""
            tnama_ken2.Text = ""

            tkd_ken3.Text = ""
            tnama_ken3.Text = ""

        End If
    End Sub

    '' kenek 1

    Private Sub bts_ken1_Click(sender As System.Object, e As System.EventArgs) Handles bts_ken1.Click

        Dim fs As New fskenek With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_ken1.EditValue = fs.get_KODE
        tnama_ken1.EditValue = fs.get_NAMA

    End Sub

    Private Sub tkd_ken1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_ken1.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_ken1_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_ken1_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_ken1.LostFocus
        If tkd_ken1.Text.Trim.Length = 0 Then
            tkd_ken1.Text = ""
            tnama_ken1.Text = ""
        End If
    End Sub

    Private Sub tkd_ken1_Validated(sender As Object, e As System.EventArgs) Handles tkd_ken1.Validated
        If tkd_ken1.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and bagian='KENEK' and kd_karyawan='{0}'", tkd_ken1.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_ken1.EditValue = dread("kd_karyawan").ToString
                        tnama_ken1.EditValue = dread("nama_karyawan").ToString

                    Else
                        tkd_ken1.EditValue = ""
                        tnama_ken1.EditValue = ""

                    End If
                Else
                    tkd_ken1.EditValue = ""
                    tnama_ken1.EditValue = ""

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

    '' kenek 2

    Private Sub bts_ken2_Click(sender As System.Object, e As System.EventArgs) Handles bts_ken2.Click

        Dim fs As New fskenek With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_ken2.EditValue = fs.get_KODE
        tnama_ken2.EditValue = fs.get_NAMA

    End Sub

    Private Sub tkd_ken2_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_ken2.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_ken2_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_ken2_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_ken2.LostFocus
        If tkd_ken2.Text.Trim.Length = 0 Then
            tkd_ken2.Text = ""
            tnama_ken2.Text = ""
        End If
    End Sub

    Private Sub tkd_ken2_Validated(sender As Object, e As System.EventArgs) Handles tkd_ken2.Validated
        If tkd_ken2.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and bagian='KENEK' and kd_karyawan='{0}'", tkd_ken2.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_ken2.EditValue = dread("kd_karyawan").ToString
                        tnama_ken2.EditValue = dread("nama_karyawan").ToString

                    Else
                        tkd_ken2.EditValue = ""
                        tnama_ken2.EditValue = ""

                    End If
                Else
                    tkd_ken2.EditValue = ""
                    tnama_ken2.EditValue = ""

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

    '' kenek 3

    Private Sub bts_ken3_Click(sender As System.Object, e As System.EventArgs) Handles bts_ken3.Click

        Dim fs As New fskenek With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_ken3.EditValue = fs.get_KODE
        tnama_ken3.EditValue = fs.get_NAMA

    End Sub

    Private Sub tkd_ken3_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_ken3.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_ken3_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_ken3_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_ken3.LostFocus
        If tkd_ken3.Text.Trim.Length = 0 Then
            tkd_ken3.Text = ""
            tnama_ken3.Text = ""
        End If
    End Sub

    Private Sub tkd_ken3_Validated(sender As Object, e As System.EventArgs) Handles tkd_ken3.Validated
        If tkd_ken3.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and bagian='KENEK' and kd_karyawan='{0}'", tkd_ken3.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_ken3.EditValue = dread("kd_karyawan").ToString
                        tnama_ken3.EditValue = dread("nama_karyawan").ToString

                    Else
                        tkd_ken3.EditValue = ""
                        tnama_ken3.EditValue = ""

                    End If
                Else
                    tkd_ken3.EditValue = ""
                    tnama_ken3.EditValue = ""

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



    Private Sub btadd_Click(sender As System.Object, e As System.EventArgs) Handles btadd.Click

        Dim fs As New trspm3 With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .addstat = True, .position = 0, .dv = dv1}
        fs.ShowDialog(Me)

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

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tnopol.EditValue = "" Then
            MsgBox("No Polisi kendaraan harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tnopol.Focus()
            Return
        End If

        If tkd_sales.Text.Trim.Length = 0 Then
            MsgBox("Sales harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tkd_sales.Focus()
            Return
        End If

        If tkd_supir.Text.Trim.Length = 0 Then
            MsgBox("Supir harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tkd_supir.Focus()
            Return
        End If

        If tkd_ken1.Text.Trim.Length = 0 Then
            MsgBox("Kenek 1 harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tkd_ken1.Focus()
            Return
        End If

        If MsgBox("Yakin sudah benar ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
            simpan()
        End If

    End Sub

    Private Sub frekap_to2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tbukti.Focus()
    End Sub

    Private Sub frekap_to2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        isi_nopol()

        ttgl.EditValue = Date.Now
        ttgl_mt.EditValue = Date.Now
        ttgl_krm.EditValue = Date.Now

        If addstat = False Then
            isi()
        Else
            kosongkan()
        End If

    End Sub

    Private Sub tnopol_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tnopol.EditValueChanged

        Try
            tgudang.EditValue = tnopol.EditValue
        Catch ex As Exception
            MsgBox("Kendaraan belum didaftarkan pada gudang mobil..", vbOKOnly + vbInformation, "Informasi")
        End Try


    End Sub

    Private Sub tnopol_Validated(sender As System.Object, e As System.EventArgs) Handles tnopol.Validated

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select kd_supir,kd_sales from ms_supirkenek where nopol='{0}'", tnopol.EditValue)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            Dim ada As Boolean = False

            If drd.Read Then
                If Not drd(0).ToString.Equals("") Then

                    ada = True

                    iscek_supsal = True

                    tkd_sales.EditValue = drd(1).ToString
                    tkd_supir.EditValue = drd(0).ToString

                    iscek_supsal = True

                    cek_supirkenek(cn, "nopol")

                    iscek_supsal = False


                End If
            End If
            drd.Close()

            If ada = False Then

                tkd_sales.Text = ""
                tnama_sales.Text = ""

                tkd_sales_Validated(Nothing, Nothing)

                tkd_supir.EditValue = ""
                tnama_supir.Text = ""
                tkd_supir_Validated(sender, Nothing)
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

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click

        Dim fs As New fssupirkenek With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .ifkanvas = True}
        fs.ShowDialog(Me)

        tnopol.EditValue = fs.get_nopol
        tnopol_Validated(sender, Nothing)


        If addstat Then
            '   cek_supirkenek(Nothing)
        End If

    End Sub

    Private Sub tnopol_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tnopol.KeyDown
        If e.KeyCode = Keys.F4 Then
            SimpleButton1_Click(sender, Nothing)
        End If
    End Sub

End Class