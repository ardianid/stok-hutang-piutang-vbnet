Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class tr_bocoran2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private tgl_old As String
    Private nobukti_gd As String

    Private Sub kosongkan()
        tbukti.Text = "<< New >>"

        tbukti_gd.Text = ""
        tkrani.Text = ""
        tshift.Text = ""

        opengrid()
        opengrid2()

    End Sub

    Private Sub opengrid()

        Dim sql As String = String.Format("SELECT  tr_bocoran2.noid, tr_bocoran2.kd_gudang, tr_bocoran2.kd_barang, ms_barang.nama_barang, tr_bocoran2.qty, tr_bocoran2.qtykecil, tr_bocoran2.satuan " & _
            "FROM  tr_bocoran2 INNER JOIN ms_barang ON tr_bocoran2.kd_barang = ms_barang.kd_barang where tr_bocoran2.nobukti='{0}'", tbukti.Text.Trim)


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

        Dim sql As String = String.Format("SELECT     tr_bocoran3.noid, tr_bocoran3.nobukti, tr_bocoran3.kd_gudang, tr_bocoran3.kd_barang, ms_barang.nama_barang, tr_bocoran3.qty, tr_bocoran3.qtykecil, tr_bocoran3.satuan,ms_barang.qty1,ms_barang.qty2,ms_barang.qty3 " & _
            "FROM         tr_bocoran3 INNER JOIN ms_barang ON tr_bocoran3.kd_barang = ms_barang.kd_barang where tr_bocoran3.nobukti='{0}'", tbukti.Text.Trim)


        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        grid2.DataSource = Nothing

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

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader
            If drd.Read Then
                If Not drd("kd_gudang").ToString.Equals("") Then
                    tgudang.EditValue = drd("kd_gudang").ToString
                End If
            End If
            drd.Close()

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

    Private Sub opengrid_load()

        Dim sql As String = String.Format("SELECT    0 as noid,tradm_gud.kd_gudang, ms_barang.kd_barang, ms_barang.nama_barang, tradm_gud2.qtyout as qty, tradm_gud2.qtyoutkecil as qtykecil, tradm_gud2.satuan " & _
            "FROM         tradm_gud INNER JOIN tradm_gud2 ON tradm_gud.nobukti = tradm_gud2.nobukti INNER JOIN ms_barang ON tradm_gud2.kd_barang = ms_barang.kd_barang " & _
            "WHERE tradm_gud.sbatal=0 and tradm_gud.jenistrans='TR BOCORAN' and tradm_gud.sambil=0 and tradm_gud.nobukti='{0}'", tbukti_gd.Text.Trim)

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

    Private Sub opengrid_load2()

        Dim sql As String = String.Format("SELECT     0 as noid,'' as nobukti,'' as kd_gudang,ms_jamin.kd_barang, ms_jamin.nama_barang,0 as qty,0 as qtykecil, ms_jamin.satuan1 as satuan,ms_barang.qty1,ms_barang.qty2,ms_barang.qty3 " & _
            "FROM         ms_barang INNER JOIN ms_barang AS ms_jamin ON ms_barang.kd_barang_kmb = ms_jamin.kd_barang " & _
            "WHERE ms_barang.kd_barang in ( " & _
            "Select ms_barang.kd_barang " & _
            "FROM         tradm_gud INNER JOIN tradm_gud2 ON tradm_gud.nobukti = tradm_gud2.nobukti INNER JOIN ms_barang ON tradm_gud2.kd_barang = ms_barang.kd_barang " & _
            "WHERE tradm_gud.sbatal=0 and tradm_gud.jenistrans='TR BOCORAN' and tradm_gud.sambil=0 and tradm_gud.nobukti='{0}')", tbukti_gd.Text.Trim)
        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        grid2.DataSource = Nothing

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
        Dim sql As String = String.Format("SELECT     tr_bocoran.nobukti, tr_bocoran.tanggal, tr_bocoran.tgl_keluar, tr_bocoran.nobukti_gd, tr_bocoran.note, tr_bocoran.sbatal, ms_pegawai.nama_karyawan, tradm_gud.shit,tradm_gud.nobukti_gd as nodo " & _
            "FROM         ms_pegawai INNER JOIN " & _
            "tradm_gud ON ms_pegawai.kd_karyawan = tradm_gud.kd_krani RIGHT OUTER JOIN " & _
            "tr_bocoran ON tradm_gud.nobukti = tr_bocoran.nobukti_gd where tr_bocoran.nobukti='{0}'", nobukti)

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

                        tbukti.Text = dread("nobukti").ToString
                        ttgl.EditValue = DateValue(dread("tanggal").ToString)
                        ttgl2.EditValue = DateValue(dread("tgl_keluar").ToString)
                        tbukti_gd.Text = dread("nobukti_gd").ToString
                        tkrani.Text = dread("nama_karyawan").ToString
                        tshift.Text = dread("shit").ToString

                        nobukti_gd = dread("nodo").ToString

                        tno_sj.Text = nobukti_gd

                        tgl_old = DateValue(dread("tgl_keluar").ToString)

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

        Dim sql As String = String.Format("select max(nobukti) from tr_bocoran where nobukti like '%SVB.{0}%' ", tahunbulan)

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

        Return String.Format("SVB.{0}{1}{2}", tahun, bulan, kbukti)

    End Function

    Private Function cek_nosj(ByVal cn As OleDbConnection) As Boolean

        Dim hasil As Boolean = False

        Dim sql As String = String.Format("select nobukti from tr_bocoran where nobukti_gd='{0}' and sbatal=0", tbukti_gd.Text.Trim)
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
                If cek_nosj(cn) = True Then
                    MsgBox("No-DO jalan sudah diinput..", vbOKOnly + vbInformation, "Informasi")
                    Return
                End If
            End If

            sqltrans = cn.BeginTransaction

            Dim cmd As OleDbCommand

            If addstat Then
                Dim bukti As String = cekbukti(cn, sqltrans)
                tbukti.EditValue = bukti

                '0. insert transaksi
                Dim sqlin As String = String.Format("insert into tr_bocoran (nobukti,tanggal,tgl_keluar,note,nobukti_gd) values('{0}','{1}','{2}','{3}','{4}')", _
                                                    tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue), tket.Text.Trim, tbukti_gd.Text.Trim)

                cmd = New OleDbCommand(sqlin, cn, sqltrans)
                cmd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "bt_bocoran", 1, 0, 0, 0, tbukti.Text.Trim, ttgl.EditValue, sqltrans)


            Else

                '1. update rekap
                Dim sqlup As String = String.Format("update tr_bocoran set tanggal='{0}',tgl_keluar='{1}',note='{2}',nobukti_gd='{3}' where nobukti='{4}'", _
                                convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue), tket.Text.Trim, tbukti_gd.Text.Trim, tbukti.Text.Trim)

                cmd = New OleDbCommand(sqlup, cn, sqltrans)
                cmd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "bt_bocoran", 0, 1, 0, 0, tbukti.Text.Trim, ttgl.EditValue, sqltrans)
            End If

            Dim sqlupadm As String = String.Format("update tradm_gud set sambil=1 where jenistrans='TR BOCORAN' and nobukti='{0}'", tbukti_gd.Text.Trim)
            Using cmdadm As OleDbCommand = New OleDbCommand(sqlupadm, cn, sqltrans)
                cmdadm.ExecuteNonQuery()
            End Using

            If simpan2(cn, sqltrans) = "ok" Then
                '------------------------------

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

        Dim hasil As String = ""

        Dim kdbar, satuan As String
        Dim qtyin As Integer
        Dim qtyink As Integer

        Dim kdgudang As String

        For i As Integer = 0 To dv1.Count - 1

            kdgudang = dv1(i)("kd_gudang").ToString

            kdbar = dv1(i)("kd_barang").ToString
            satuan = dv1(i)("satuan").ToString
            qtyin = Integer.Parse(dv1(i)("qty").ToString)
            qtyink = Integer.Parse(dv1(i)("qtykecil").ToString)

            If dv1(i)("noid").Equals(0) Then
                Dim sqlins As String = String.Format("insert into tr_bocoran2 (nobukti,kd_gudang,kd_barang,qty,qtykecil,satuan) values('{0}','{1}','{2}',{3},{4},'{5}')", tbukti.Text.Trim, _
                                                     kdgudang, kdbar, qtyin, qtyink, satuan)

                Using cmd As OleDbCommand = New OleDbCommand(sqlins, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using



                If qtyin > 0 Then

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtyink, kdbar, kdgudang, False, False, False)
                    If Not hasilplusmin.Equals("ok") Then

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        Exit For
                    End If

                    '3. insert to hist stok
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl2.EditValue, kdgudang, kdbar, 0, qtyink, "Serv Bocoran", "-", "BE XXXX XX")

                End If

            Else


                If DateValue(tgl_old) <> DateValue(ttgl2.EditValue) Then

                    If qtyin > 0 Then

                        '3. insert to hist stok

                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, tgudang.EditValue, kdbar, qtyink, 0, "Serv Bocoran (Edit)", "-", "BE XXXX XX")

                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl2.EditValue, tgudang.EditValue, kdbar, 0, qtyink, "Serv Bocoran (Edit)", "-", "BE XXXX XX")


                    End If

                End If


            End If

        Next


        '' untuk yang barang balik

        Dim kdbar2, satuan2 As String
        Dim qtyin2 As Integer
        Dim qtyink2 As Integer

        For x As Integer = 0 To dv2.Count - 1

            kdbar2 = dv2(x)("kd_barang").ToString
            satuan2 = dv2(x)("satuan").ToString
            qtyin2 = Integer.Parse(dv2(x)("qty").ToString)
            qtyink2 = Integer.Parse(dv2(x)("qtykecil").ToString)

            If dv2(x)("noid").Equals(0) Then
                Dim sqlins As String = String.Format("insert into tr_bocoran3 (nobukti,kd_gudang,kd_barang,qty,qtykecil,satuan) values('{0}','{1}','{2}',{3},{4},'{5}')", tbukti.Text.Trim, _
                                                     tgudang.EditValue, kdbar2, qtyin2, qtyink2, satuan2)

                Using cmd As OleDbCommand = New OleDbCommand(sqlins, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

                If qtyin2 > 0 Then

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtyink2, kdbar2, tgudang.EditValue, True, False, False)
                    If Not hasilplusmin.Equals("ok") Then

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        Exit For
                    End If

                    '3. insert to hist stok
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl2.EditValue, tgudang.EditValue, kdbar2, qtyink2, 0, "Serv Bocoran", "-", "BE XXXX XX")

                End If

            Else


                If DateValue(tgl_old) <> DateValue(ttgl2.EditValue) Then

                    If qtyin > 0 Then

                        '3. insert to hist stok

                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, tgudang.EditValue, kdbar2, 0, qtyink2, "Serv Bocoran (Edit)", "-", "BE XXXX XX")

                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl2.EditValue, tgudang.EditValue, kdbar2, qtyink2, 0, "Serv Bocoran (Edit)", "-", "BE XXXX XX")


                    End If

                End If


            End If

        Next

        '' ---------------------------------------------------------


        If Not hasil.Equals("error") Then
            hasil = "ok"
        End If

        Return hasil

    End Function

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("sbatal") = 0
        orow("nobukti") = tbukti.Text.Trim
        orow("tanggal") = ttgl.EditValue
        orow("tgl_keluar") = ttgl2.EditValue
        orow("nobukti_gd") = tbukti_gd.EditValue
        orow("note") = tket.Text.Trim
        orow("nama_karyawan") = tkrani.Text.Trim
        orow("shit") = tshift.Text.Trim
        orow("nodo") = nobukti_gd

        dv.EndInit()

    End Sub

    Private Sub updateview()

        dv(position)("sbatal") = 0
        dv(position)("nobukti") = tbukti.Text.Trim
        dv(position)("tanggal") = ttgl.EditValue
        dv(position)("tgl_keluar") = ttgl2.EditValue
        dv(position)("nobukti_gd") = tbukti_gd.EditValue
        dv(position)("note") = tket.Text.Trim
        dv(position)("nama_karyawan") = tkrani.Text.Trim
        dv(position)("shit") = tshift.Text.Trim
        dv(position)("nodo") = nobukti_gd

    End Sub

    Private Sub hapus()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Dim kdbar, satuan As String
        Dim qtyin As Integer
        Dim qtyink As Integer

        Dim kdgudang As String

        Dim noid As Integer = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("noid"))

        Try

            If noid = 0 Then
                dv1.Delete(Me.BindingContext(dv1).Position)
            Else

                open_wait()

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                sqltrans = cn.BeginTransaction

                Dim sqldel As String = String.Format("delete from tr_bocoran2 where noid={0}", noid)
                Using cmd As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

                kdbar = dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString
                satuan = dv1(Me.BindingContext(dv1).Position)("satuan").ToString
                qtyin = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qty").ToString)
                qtyink = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtykecil").ToString)

                kdgudang = dv1(Me.BindingContext(dv1).Position)("kd_gudang").ToString

                If qtyin > 0 Then

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtyink, kdbar, kdgudang, True, False, False)
                    If Not hasilplusmin.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        GoTo langsung
                    End If

                    '3. insert to hist stok
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl2.EditValue, kdgudang, kdbar, qtyink, 0, "Serv Bocoran (Del)", "-", "BE XXXX XX")

                End If

                If hapus2(cn, sqltrans, kdbar).Trim.Equals("ok") Then
                    sqltrans.Commit()

                    dv1.Delete(Me.BindingContext(dv1).Position)

                    close_wait()
                End If

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

    Private Function hapus2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal kdbar As String) As String

        Dim hasil As String = ""

        Dim kdbar2, satuan As String
        Dim qtyin As Integer
        Dim qtyink As Integer

        Dim sqlc As String = String.Format("select kd_barang_jmn from ms_barang where kd_barang='{0}'", kdbar)
        Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
        Dim drdc As OleDbDataReader = cmdc.ExecuteReader

        If drdc.Read Then
            If Not drdc(0).ToString.Equals("") Then

                For i As Integer = 0 To dv2.Count - 1

                    If dv2(i)("kd_barang").ToString.Equals(drdc(0).ToString) Then

                        kdbar2 = dv2(i)("kd_barang").ToString
                        satuan = dv2(i)("satuan").ToString
                        qtyin = Integer.Parse(dv2(i)("qty").ToString)
                        qtyink = Integer.Parse(dv2(i)("qtykecil").ToString)

                        If qtyin > 0 Then

                            '2. update barang
                            Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtyink, kdbar2, tgudang.EditValue, False, False, False)
                            If Not hasilplusmin.Equals("ok") Then
                                hasil = "error"
                                close_wait()

                                If Not IsNothing(sqltrans) Then
                                    sqltrans.Rollback()
                                End If

                                MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                                GoTo langsung
                            End If

                            '3. insert to hist stok
                            Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl2.EditValue, tgudang.EditValue, kdbar2, 0, qtyink, "Serv Bocoran (Del)", "-", "BE XXXX XX")

                            Dim sqldel As String = String.Format("delete from tr_bocoran3 where noid={0}", dv2(i)("noid").ToString)
                            Using cmddel As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
                                cmddel.ExecuteNonQuery()
                            End Using

                            dv2.Delete(i)

                        End If

                    End If

                Next

            End If
        End If
        drdc.Close()

langsung:

        If hasil.Equals("") Then
            hasil = "ok"
        End If

        Return hasil

    End Function

    Private Sub tbukti_gd_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tbukti_gd.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_gd_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tbukti_gd_Validated(sender As System.Object, e As System.EventArgs) Handles tbukti_gd.Validated

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select tradm_gud.nobukti,ms_pegawai.nama_karyawan,tradm_gud.shit,tradm_gud.nobukti_gd " & _
                "from tradm_gud inner join ms_pegawai on tradm_gud.kd_krani=ms_pegawai.kd_karyawan where tradm_gud.sbatal=0 and tradm_gud.sambil=0 and tradm_gud.nobukti='{0}'", tbukti_gd.Text.Trim)

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If drd("nobukti").ToString.Equals("") Then
                    tbukti_gd.Text = ""
                    tkrani.Text = ""
                    tshift.Text = ""
                    nobukti_gd = ""
                    tno_sj.Text = ""
                Else
                    tbukti_gd.Text = drd("nobukti").ToString
                    tkrani.Text = drd("nama_karyawan").ToString
                    tshift.Text = drd("shit").ToString

                    nobukti_gd = drd("nobukti_gd").ToString

                    tno_sj.Text = nobukti_gd

                    opengrid_load()
                    opengrid_load2()

                End If
            Else
                tbukti_gd.Text = ""
                tkrani.Text = ""
                tshift.Text = ""
                nobukti_gd = ""
                tno_sj.Text = ""
            End If

            drd.Close()

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

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tbukti_gd.Text.Trim.Length = 0 Then
            MsgBox("No Bukti Gudang boleh kosong..", vbOKOnly + vbInformation, "Informasi")
            tbukti_gd.Focus()
            Return
        End If

        If tgudang.EditValue = "" Then
            MsgBox("Gudang boleh kosong..", vbOKOnly + vbInformation, "Informasi")
            XtraTabControl1.SelectedTabPageIndex = 1
            tgudang.Focus()
            Return
        End If

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

        isi_gudang()

        tgudang.EditValue = "G000"

        ttgl.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

        If addstat Then
            kosongkan()

            tbukti_gd.Enabled = True
            bts_gd.Enabled = True

        Else
            isi()

            tbukti_gd.Enabled = False
            bts_gd.Enabled = False

        End If

    End Sub

    Private Sub GridView2_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView2.CellValueChanged

        Dim qty As Integer

        qty = Integer.Parse(dv2(Me.BindingContext(dv2).Position)("qty").ToString)


        '  Dim hargajual As Double = Double.Parse(dv2(Me.BindingContext(dv2).Position)("hargajual").ToString)

        Dim satuan As String = dv2(Me.BindingContext(dv2).Position)("satuan").ToString

        Dim vqty1 As Integer = Integer.Parse(dv2(Me.BindingContext(dv2).Position)("qty1").ToString)
        Dim vqty2 As Integer = Integer.Parse(dv2(Me.BindingContext(dv2).Position)("qty2").ToString)
        Dim vqty3 As Integer = Integer.Parse(dv2(Me.BindingContext(dv2).Position)("qty3").ToString)

        Dim kqty As Integer = 0

        kqty = (vqty1 * vqty2 * vqty3) * qty

        dv2(Me.BindingContext(dv2).Position)("qty") = qty
        dv2(Me.BindingContext(dv2).Position)("qtykecil") = kqty

    End Sub

    Private Sub bts_gd_Click(sender As System.Object, e As System.EventArgs) Handles bts_gd.Click
        Dim fs As New fs_bocoran_ad With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tbukti_gd.Text = fs.get_NoBukti

        tbukti_gd_Validated(sender, Nothing)

    End Sub

    Private Sub btdel_Click(sender As System.Object, e As System.EventArgs) Handles btdel.Click

    End Sub

    Private Sub btadd_Click(sender As System.Object, e As System.EventArgs) Handles btadd.Click

    End Sub

End Class