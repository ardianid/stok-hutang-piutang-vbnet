Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fadm_gud2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private tglmuat_old As String
    Private dttrans As DataTable

    Dim kdsupir_old As String
    Dim nopol_old As String

    Private Sub kosongkan()
        tbukti.Text = "<< New >>"

        tbukti_tr.Text = ""
        tnodo.Text = ""
        tkd_supir.Text = ""
        tnama_supir.Text = ""
        tkd_krani.Text = ""
        tnama_krani.Text = ""
        tket.Text = ""

        ttimb.EditValue = 0

        opengrid()

    End Sub

    Private Sub opengrid()

        Dim sql As String = String.Format("SELECT tradm_gud2.noid,tradm_gud2.kd_barang, ms_barang.nama_barang, tradm_gud2.qtyin,tradm_gud2.qtyin_bad, tradm_gud2.qtyout, tradm_gud2.satuan, tradm_gud2.qtyinkecil,tradm_gud2.qtyinkecil_bad, tradm_gud2.qtyoutkecil,tradm_gud2.berat1,tradm_gud2.total_berat " & _
            "FROM  tradm_gud2 INNER JOIN ms_barang ON tradm_gud2.kd_barang = ms_barang.kd_barang where tradm_gud2.nobukti='{0}'", tbukti.Text.Trim)


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
        Dim sql As String = String.Format("SELECT     tradm_gud.nobukti, tradm_gud.tanggal, tradm_gud.tglmuat, tradm_gud.tglberangkat, tradm_gud.jenistrans, tradm_gud.nobukti_trans, tradm_gud.nobukti_gd, " & _
            "tradm_gud.kd_gudang, tradm_gud.kd_krani, ms_pegawai.nama_karyawan AS nama_krani, tradm_gud.nopol, tradm_gud.shit, tradm_gud.kd_supir, ms_pegawai2.nama_karyawan AS nama_supir, tradm_gud.note,tradm_gud.kd_gudang2,tradm_gud.jmltimb,tradm_gud.asal_barang " & _
            "FROM         ms_pegawai INNER JOIN " & _
            "tradm_gud ON ms_pegawai.kd_karyawan = tradm_gud.kd_krani INNER JOIN " & _
            "ms_pegawai AS ms_pegawai2 ON tradm_gud.kd_supir = ms_pegawai2.kd_karyawan where tradm_gud.nobukti='{0}'", nobukti)

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

                        nopol_old = tnopol.EditValue

                        tkd_supir.EditValue = dread("kd_supir").ToString
                        tnama_supir.EditValue = dread("nama_supir").ToString

                        kdsupir_old = tkd_supir.EditValue

                        tkd_krani.EditValue = dread("kd_krani").ToString
                        tnama_krani.EditValue = dread("nama_krani").ToString

                        tjenis.EditValue = dread("jenistrans").ToString

                        ttgl.EditValue = DateValue(dread("tanggal").ToString)
                        ttgl_muat.EditValue = DateValue(dread("tglmuat").ToString)

                        tglmuat_old = ttgl_muat.EditValue

                        ttgl_krim.EditValue = DateValue(dread("tglberangkat").ToString)

                        tbukti_tr.EditValue = dread("nobukti_trans").ToString
                        tnodo.EditValue = dread("nobukti_gd").ToString

                        tgudang.EditValue = dread("kd_gudang").ToString
                        tnopol.EditValue = dread("nopol").ToString
                        tshift.EditValue = dread("shit").ToString

                        tket.EditValue = dread("note").ToString
                        tasal.EditValue = dread("asal_barang").ToString

                        tgudang2.EditValue = dread("kd_gudang2").ToString
                        ttimb.EditValue = Integer.Parse(dread("jmltimb").ToString)

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

        Dim sql As String = String.Format("select max(nobukti) from tradm_gud where len(nobukti)=13 and nobukti like '%IOG.{0}%'", bulantahun)

        '   sql = String.Format(" {0} and tanggal<'2014/11/07'", sql)

        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim nilai As Integer = 0

        If drd.HasRows Then
            If drd.Read Then

                If Not drd(0).ToString.Equals("") Then
                    nilai = Microsoft.VisualBasic.Right(drd(0).ToString, 5)
                End If

            End If
        End If

        nilai = nilai + 1
        Dim kbukti As String = nilai

        Select Case kbukti.Length
            Case 1
                kbukti = "0000" & nilai
            Case 2
                kbukti = "000" & nilai
            Case 3
                kbukti = "00" & nilai
            Case 4
                kbukti = "0" & nilai
            Case Else
                kbukti = nilai
        End Select

        Return String.Format("IOG.{0}{1}{2}", tahun, bulan, kbukti)

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

                '0. insert transaksi
                Dim sqlin As String = String.Format("insert into tradm_gud (nobukti,tanggal,tglmuat,tglberangkat,jenistrans,nobukti_trans,nobukti_gd,kd_gudang,kd_krani,kd_supir,nopol,shit,note,kd_gudang2,jmltimb,asal_barang) " & _
                 "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}',{14},'{15}')", tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_muat.EditValue), convert_date_to_eng(ttgl_krim.EditValue), _
                         tjenis.EditValue, tbukti_tr.EditValue, tnodo.EditValue, tgudang.EditValue, tkd_krani.EditValue, tkd_supir.EditValue, tnopol.EditValue, tshift.EditValue, tket.Text.Trim, tgudang2.EditValue, Replace(ttimb.EditValue, ",", "."), tasal.EditValue)

                cmd = New OleDbCommand(sqlin, cn, sqltrans)
                cmd.ExecuteNonQuery()

                '1. update faktur to / bila dari to
                If tjenis.Text.Trim.Equals("TR PENJ TO") Then

                    Dim sqlup_rekap As String = String.Format("update trrekap_to set smuat=1 where nobukti='{0}'", tbukti_tr.Text.Trim)
                    Using cmdup_rekap As New OleDbCommand(sqlup_rekap, cn, sqltrans)
                        cmdup_rekap.ExecuteNonQuery()
                    End Using

                ElseIf tjenis.Text.Trim.Equals("TR KANVAS") Then

                    Dim sqlup_spm As String = String.Format("update trspm set smuat=1 where nobukti='{0}'", tbukti_tr.Text.Trim)
                    Using cmdup_rekap As New OleDbCommand(sqlup_spm, cn, sqltrans)
                        cmdup_rekap.ExecuteNonQuery()
                    End Using


                End If

                Clsmy.InsertToLog(cn, "btadm_g", 1, 0, 0, 0, tbukti.Text.Trim, ttgl.EditValue, sqltrans)



            Else

                '1. update faktur to / bila dari to
                If tjenis.Text.Trim.Equals("TR PENJ TO") Then

                    Dim sqluprekap0 As String = String.Format("update trrekap_to set smuat=0 where nobukti in (select nobukti_trans from tradm_gud where nobukti='{0}')", tbukti.Text.Trim)
                    Using cmdup_rekap0 As OleDbCommand = New OleDbCommand(sqluprekap0, cn, sqltrans)
                        cmdup_rekap0.ExecuteNonQuery()
                    End Using

                    Dim sqlup_rekap As String = String.Format("update trrekap_to set smuat=1 where nobukti='{0}'", tbukti_tr.Text.Trim)
                    Using cmdup_rekap As New OleDbCommand(sqlup_rekap, cn, sqltrans)
                        cmdup_rekap.ExecuteNonQuery()
                    End Using

                ElseIf tjenis.Text.Trim.Equals("TR KANVAS") Then

                    Dim sqluprekap0 As String = String.Format("update trspm set smuat=0 where nobukti in (select nobukti_trans from tradm_gud where nobukti='{0}')", tbukti.Text.Trim)
                    Using cmdup_rekap0 As OleDbCommand = New OleDbCommand(sqluprekap0, cn, sqltrans)
                        cmdup_rekap0.ExecuteNonQuery()
                    End Using

                    Dim sqlup_spm As String = String.Format("update trspm set smuat=1 where nobukti='{0}'", tbukti_tr.Text.Trim)
                    Using cmdup_rekap As New OleDbCommand(sqlup_spm, cn, sqltrans)
                        cmdup_rekap.ExecuteNonQuery()
                    End Using

                End If

                '1. update rekap
                Dim sqlup As String = String.Format("update tradm_gud set tanggal='{0}',tglmuat='{1}',tglberangkat='{2}',jenistrans='{3}',nobukti_trans='{4}',nobukti_gd='{5}',kd_gudang='{6}',kd_krani='{7}',nopol='{8}',shit='{9}',kd_supir='{10}',note='{11}',kd_gudang2='{12}',jmltimb={13},asal_barang='{14}' where nobukti='{15}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_muat.EditValue), convert_date_to_eng(ttgl_krim.EditValue), _
                                                    tjenis.EditValue, tbukti_tr.Text.Trim, tnodo.Text.Trim, tgudang.EditValue, tkd_krani.Text.Trim, tnopol.Text.Trim, tshift.Text.Trim, tkd_supir.Text.Trim, tket.Text.Trim, tgudang2.EditValue, Replace(ttimb.EditValue, ",", "."), tasal.EditValue, tbukti.Text.Trim)

                cmd = New OleDbCommand(sqlup, cn, sqltrans)
                cmd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btadm_g", 0, 1, 0, 0, tbukti.Text.Trim, ttgl.EditValue, sqltrans)
            End If

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
                    tkd_supir.Focus()
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

        'Dim cmdc As OleDbCommand
        'Dim drdc As OleDbDataReader
        Dim hasil As String = ""

        Dim kdbar, satuan As String
        Dim qtyin, qtyout As Integer
        Dim qtyink, qtyoutk As Integer

        Dim qtyin_bad As Integer
        Dim qtyink_bad As Integer

        Dim berat1 As Decimal
        Dim totberat As Decimal

        For i As Integer = 0 To dv1.Count - 1

            kdbar = dv1(i)("kd_barang").ToString
            satuan = dv1(i)("satuan").ToString
            qtyin = Integer.Parse(dv1(i)("qtyin").ToString)
            qtyout = Integer.Parse(dv1(i)("qtyout").ToString)
            qtyink = Integer.Parse(dv1(i)("qtyinkecil").ToString)
            qtyoutk = Integer.Parse(dv1(i)("qtyoutkecil").ToString)

            qtyin_bad = Integer.Parse(dv1(i)("qtyin_bad").ToString)
            qtyink_bad = Integer.Parse(dv1(i)("qtyinkecil_bad").ToString)

            berat1 = Decimal.Parse(dv1(i)("berat1").ToString)
            totberat = Decimal.Parse(dv1(i)("total_berat").ToString)

            If dv1(i)("noid").Equals(0) Then
                Dim sqlins As String = String.Format("insert into tradm_gud2 (nobukti,kd_barang,satuan,qtyin,qtyout,qtyinkecil,qtyoutkecil,qtyin_bad,qtyinkecil_bad,berat1,total_berat) values('{0}','{1}','{2}',{3},{4},{5},{6},{7},{8},{9},{10})", tbukti.Text.Trim, _
                                                     kdbar, satuan, qtyin, qtyout, qtyink, qtyoutk, qtyin_bad, qtyink_bad, Replace(berat1, ",", "."), Replace(totberat, ",", "."))

                Using cmd As OleDbCommand = New OleDbCommand(sqlins, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using


                If qtyin > 0 Then

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang_Fsk(cn, sqltrans, qtyink, kdbar, tgudang.EditValue, True, False, False)
                    If Not hasilplusmin.Equals("ok") Then

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        Exit For
                    End If

                    '3. insert to hist stok
                    Clsmy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, tbukti_tr.Text.Trim, tnopol.Text.Trim, ttgl_muat.EditValue, tgudang.EditValue, kdbar, qtyink, 0, tjenis.Text.Trim, tkd_supir.Text.Trim)

                ElseIf qtyout > 0 Then

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang_Fsk(cn, sqltrans, qtyoutk, kdbar, tgudang.EditValue, False, False, False)
                    If Not hasilplusmin.Equals("ok") Then

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        Exit For
                    End If

                    '3. insert to hist stok
                    Clsmy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, tbukti_tr.Text.Trim, tnopol.Text.Trim, ttgl_muat.EditValue, tgudang.EditValue, kdbar, 0, qtyoutk, tjenis.Text.Trim, tkd_supir.Text.Trim)

                End If

            Else

                If DateValue(tglmuat_old) <> DateValue(ttgl_muat.EditValue) Or nopol_old <> tnopol.EditValue Or kdsupir_old <> tkd_supir.EditValue Then

                    If qtyin > 0 Then

                        '3. insert to hist stok
                        Clsmy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, tbukti_tr.Text.Trim, nopol_old, tglmuat_old, tgudang.EditValue, kdbar, 0, qtyink, tjenis.Text.Trim, kdsupir_old)

                        Clsmy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, tbukti_tr.Text.Trim, nopol_old, ttgl_muat.EditValue, tgudang.EditValue, kdbar, qtyink, 0, tjenis.Text.Trim, tkd_supir.Text.Trim)

                    ElseIf qtyout > 0 Then

                        '3. insert to hist stok
                        Clsmy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, tbukti_tr.Text.Trim, nopol_old, tglmuat_old, tgudang.EditValue, kdbar, qtyoutk, 0, tjenis.Text.Trim, kdsupir_old)

                        Clsmy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, tbukti_tr.Text.Trim, nopol_old, ttgl_muat.EditValue, tgudang.EditValue, kdbar, 0, qtyoutk, tjenis.Text.Trim, tkd_supir.Text.Trim)

                    End If

                End If


              

            End If

        Next

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
        orow("tglmuat") = ttgl_muat.EditValue
        orow("tglberangkat") = ttgl_krim.EditValue
        orow("jenistrans") = tjenis.Text.Trim
        orow("nobukti_trans") = tbukti_tr.Text.Trim
        orow("nobukti_gd") = tnodo.Text.Trim
        orow("kd_gudang") = tgudang.EditValue
        orow("kd_gudang2") = tgudang2.EditValue
        orow("nama_krani") = tnama_krani.Text.Trim
        orow("nopol") = tnopol.EditValue
        orow("shit") = tshift.Text.Trim
        orow("kd_supir") = tkd_supir.Text.Trim
        orow("nama_supir") = tnama_supir.Text.Trim
        orow("note") = tket.Text.Trim
        dv.EndInit()

    End Sub

    Private Sub updateview()

        dv(position)("nobukti") = tbukti.Text.Trim
        dv(position)("tanggal") = ttgl.EditValue
        dv(position)("tglmuat") = ttgl_muat.EditValue
        dv(position)("tglberangkat") = ttgl_krim.EditValue
        dv(position)("jenistrans") = tjenis.Text.Trim
        dv(position)("nobukti_trans") = tbukti_tr.Text.Trim
        dv(position)("nobukti_gd") = tnodo.Text.Trim
        dv(position)("kd_gudang") = tgudang.EditValue
        dv(position)("kd_gudang2") = tgudang2.EditValue
        dv(position)("nama_krani") = tnama_krani.Text.Trim
        dv(position)("nopol") = tnopol.EditValue
        dv(position)("shit") = tshift.Text.Trim
        dv(position)("kd_supir") = tkd_supir.Text.Trim
        dv(position)("nama_supir") = tnama_supir.Text.Trim
        dv(position)("note") = tket.Text.Trim

    End Sub

    Private Sub hapus()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Dim kdbar, satuan As String
        Dim qtyin, qtyout As Integer
        Dim qtyink, qtyoutk As Integer

        Dim qtyin_bad As Integer
        Dim qtyink_bad As Integer

        Dim noid As Integer = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("noid"))

        Try

            If noid = 0 Then
                dv1.Delete(Me.BindingContext(dv1).Position)
            Else

                open_wait()

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                sqltrans = cn.BeginTransaction

                Dim sqldel As String = String.Format("delete from tradm_gud2 where noid={0}", noid)
                Using cmd As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

                kdbar = dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString
                satuan = dv1(Me.BindingContext(dv1).Position)("satuan").ToString
                qtyin = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtyin").ToString)
                qtyout = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtyout").ToString)
                qtyink = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtyinkecil").ToString)
                qtyoutk = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtyoutkecil").ToString)

                qtyin_bad = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtyin_bad").ToString)
                qtyink_bad = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtyinkecil_bad").ToString)

                'If qtyin_bad > 0 Then

                '    '2. update barang
                '    Dim hasilplusmin As String = PlusMin_Barang_Fsk(cn, sqltrans, qtyink_bad, kdbar, tgudang2.EditValue, False, False, False)
                '    If Not hasilplusmin.Equals("ok") Then
                '        close_wait()
                '        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                '        GoTo langsung
                '    End If

                '    '3. insert to hist stok
                '    Clsmy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, tbukti_tr.Text.Trim, tnopol.Text.Trim, ttgl_muat.EditValue, tgudang2.EditValue, kdbar, 0, qtyink_bad, tjenis.Text.Trim)

                'End If

                If qtyin > 0 Then

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang_Fsk(cn, sqltrans, qtyink, kdbar, tgudang.EditValue, False, False, False)
                    If Not hasilplusmin.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        GoTo langsung
                    End If

                    '3. insert to hist stok
                    Clsmy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, tbukti_tr.Text.Trim, tnopol.Text.Trim, ttgl_muat.EditValue, tgudang.EditValue, kdbar, 0, qtyink, tjenis.Text.Trim, tkd_supir.Text.Trim)

                ElseIf qtyout > 0 Then

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang_Fsk(cn, sqltrans, qtyink, kdbar, tgudang.EditValue, True, False, False)
                    If Not hasilplusmin.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        GoTo langsung
                    End If

                    '3. insert to hist stok
                    Clsmy.Insert_HistBarang_Fsk(cn, sqltrans, tbukti.Text.Trim, tbukti_tr.Text.Trim, tnopol.Text.Trim, ttgl_muat.EditValue, tgudang.EditValue, kdbar, qtyoutk, 0, tjenis.Text.Trim, tkd_supir.Text.Trim)

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

    '' supir

    Private Sub bts_supir_Click(sender As System.Object, e As System.EventArgs) Handles bts_supir.Click

        Dim fs As New fssupir With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .issales = True}
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

    '' krani

    Private Sub bts_krani_Click(sender As System.Object, e As System.EventArgs) Handles bts_krani.Click
        Dim fs As New fskrani With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_krani.EditValue = fs.get_KODE
        tnama_krani.EditValue = fs.get_NAMA
    End Sub

    Private Sub tkd_krani_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_krani.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_krani_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_krani_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_krani.LostFocus
        If tkd_krani.Text.Trim.Length = 0 Then
            tkd_krani.Text = ""
            tnama_krani.Text = ""
        End If
    End Sub

    Private Sub tkd_krani_Validated(sender As Object, e As System.EventArgs) Handles tkd_krani.Validated
        If tkd_krani.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and bagian='KRANI' and kd_karyawan='{0}'", tkd_krani.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_krani.EditValue = dread("kd_karyawan").ToString
                        tnama_krani.EditValue = dread("nama_karyawan").ToString

                    Else
                        tkd_krani.EditValue = ""
                        tnama_krani.EditValue = ""

                    End If
                Else
                    tkd_krani.EditValue = ""
                    tnama_krani.EditValue = ""

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

    Private Sub btdel_Click(sender As System.Object, e As System.EventArgs) Handles btdel.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        hapus()

    End Sub

    Private Sub btadd_Click(sender As System.Object, e As System.EventArgs) Handles btadd.Click

        If tgudang.Text.Trim.Length <= 0 Then
            MsgBox("Pilih dulu gudang...", vbOKOnly + vbExclamation, "Konfirmasi")
            tgudang.Focus()
            Return
        End If

        If tbukti_tr.Text.Trim.Length <= 0 Then
            MsgBox("Isi dulu nobukti transaksi...", vbOKOnly + vbExclamation, "Konfirmasi")
            tbukti_tr.Focus()
            Return
        End If

        If addstat = False Then
            tbukti_tr_Validated(Nothing, Nothing)
        End If

        Using fkar2 As New fadm_gud3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0, .jenistrans = tjenis.EditValue, .kdgudang = tgudang.EditValue, .dttrans1 = dttrans}
            fkar2.ShowDialog(Me)
        End Using
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If IsNothing(dv1) Then
            MsgBox("Tidak ada barang yang akan diproses...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If dv1.Count <= 0 Then
            MsgBox("Tidak ada barang yang akan diproses...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If tgudang.Text.Trim.Trim.Length <= 0 Then
            MsgBox("Pilih dulu gudang..", vbOKOnly + vbExclamation, "Konfirmasi")
            tgudang.Focus()
            Return
        End If

        If tnopol.Text.Trim.Trim.Length <= 0 Then
            MsgBox("No polisi harus diisi..", vbOKOnly + vbExclamation, "Konfirmasi")
            tnopol.Focus()
            Return
        End If

        If tjenis.Text.Trim.Trim.Length <= 0 Then
            MsgBox("Jenis Trans harus diisi..", vbOKOnly + vbExclamation, "Konfirmasi")
            tjenis.Focus()
            Return
        End If

        If tkd_supir.Text.Trim.Trim.Length <= 0 Then
            MsgBox("Supir harus diisi..", vbOKOnly + vbExclamation, "Konfirmasi")
            tkd_supir.Focus()
            Return
        End If

        If tkd_krani.Text.Trim.Trim.Length <= 0 Then
            MsgBox("Krani harus diisi..", vbOKOnly + vbExclamation, "Konfirmasi")
            tkd_krani.Focus()
            Return
        End If

        If tshift.Text.Trim.Trim.Length <= 0 Then
            MsgBox("Shift harus diisi..", vbOKOnly + vbExclamation, "Konfirmasi")
            tshift.Focus()
            Return
        End If

        If tbukti_tr.Text.Trim.Length = 0 Then
            MsgBox("No Bukti transaksi harus diisi..", vbOKOnly + vbExclamation, "Konfirmasi")
            tbukti_tr.Focus()
            Return
        End If

        If tnodo.Text.Trim.Length = 0 Then
            MsgBox("No-DO/SJ transaksi harus diisi..", vbOKOnly + vbExclamation, "Konfirmasi")
            tnodo.Focus()
            Return
        End If

        If tjenis.EditValue.Equals("TR PEMB") Then
            If tnodo.Text.Trim = "-" Then
                MsgBox("No-DO/SJ transaksi harus diisi..", vbOKOnly + vbExclamation, "Konfirmasi")
                tnodo.Focus()
                Return
            End If
        End If

        If tjenis.EditValue.Equals("TR PENJ TO") Then

            If Not tbukti_tr.Text.Trim.Equals("-") Then

                If addstat = False Then
                    tbukti_tr_Validated(Nothing, Nothing)
                End If

                If IsNothing(dttrans) Then
                    MsgBox("No Transaksi tidak ditemukan...", vbOKOnly + vbInformation, "Informasi")
                    tbukti_tr.Focus()
                    Return
                ElseIf dttrans.Rows.Count <= 0 Then
                    MsgBox("No Transaksi tidak ditemukan...", vbOKOnly + vbInformation, "Informasi")
                    tbukti_tr.Focus()
                    Return
                End If

            End If

        ElseIf tjenis.EditValue.Equals("TR KANVAS") Then

            If Not tbukti_tr.Text.Trim.Equals("-") Then
                If addstat = False Then
                    tbukti_tr_Validated(Nothing, Nothing)
                End If

                If IsNothing(dttrans) Then
                    MsgBox("No Transaksi tidak ditemukan...", vbOKOnly + vbInformation, "Informasi")
                    tbukti_tr.Focus()
                    Return
                ElseIf dttrans.Rows.Count <= 0 Then
                    MsgBox("No Transaksi tidak ditemukan...", vbOKOnly + vbInformation, "Informasi")
                    tbukti_tr.Focus()
                    Return
                End If

            End If

            ElseIf tjenis.EditValue.Equals("TR RET SISA/OUTL") Then

                If Not tbukti_tr.Text.Trim = "-" Then

                    If addstat = False Then
                        tbukti_tr_Validated(Nothing, Nothing)
                    End If

                    If IsNothing(dttrans) Then
                        MsgBox("No Transaksi tidak ditemukan...", vbOKOnly + vbInformation, "Informasi")
                        'tbukti_tr.Focus()
                        ' Return
                    ElseIf dttrans.Rows.Count <= 0 Then
                        MsgBox("No Transaksi tidak ditemukan...", vbOKOnly + vbInformation, "Informasi")
                        ' tbukti_tr.Focus()
                        ' Return
                    End If

                End If

            End If

            If tjenis.EditValue.Equals("TR PEMB") Then
                If tnodo.Text.Trim = "-" Or tnodo.Text.Trim.Length = 0 Then
                    MsgBox("No SJ harus diisi..", vbOKOnly + vbInformation, "Informasi")
                    tnodo.Focus()
                    Return
                Else

                    If addstat Then

                        If cek_noSJ() = True Then
                            MsgBox(String.Format("No SJ {0} sudah diinput", tnodo.Text.Trim), vbOKOnly + vbInformation, "Informasi")
                            tnodo.Focus()
                            Return
                        End If

                    End If

                End If
            End If

            If addstat Then
                If Not tbukti_tr.Text.Trim.Equals("-") Then
                    If cek_notrans() = True Then
                        MsgBox(String.Format("No Bukti Transaksi {0} sudah diinput", tbukti_tr.Text.Trim), vbOKOnly + vbInformation, "Informasi")
                        tbukti_tr.Focus()
                        Return
                    End If
                End If
            End If

        If tjenis.EditValue.ToString.Equals("TR PEMB") Then
            If cek_plat() = False Then
                MsgBox("Plat tidak boleh dijadikan satu dengan barang..", vbOKOnly + vbInformation, "Informasi")
                Return
            End If
        End If

        If MsgBox("Yakin semua data sudah benar?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
            simpan()
        End If

    End Sub

    Private Function cek_noSJ() As Boolean

        Dim cn As OleDbConnection = Nothing
        Dim hasil As Boolean = False

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select COUNT(*) from tradm_gud where sbatal=0 and nobukti_gd='{0}' and jenistrans='{1}'", tnodo.Text.Trim, tjenis.EditValue)

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd(0).ToString) Then
                    If Integer.Parse(drd(0).ToString) > 0 Then
                        hasil = True
                    End If
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

    Private Function cek_plat() As Boolean

        Dim hasil As Boolean = True

        Dim sql As String = "select kelompok from ms_barang where"

        Dim jmlplat As Integer = 0
        Dim jmlnonplat As Integer = 0

        Dim cn As OleDbConnection = Nothing
        Try

            cn = Clsmy.open_conn

            For i As Integer = 0 To dv1.Count - 1

                Dim sql1 As String = String.Format("{0} kd_barang='{1}'", sql, dv1(i)("kd_barang").ToString)
                Dim cmd As OleDbCommand = New OleDbCommand(sql1, cn)
                Dim drd As OleDbDataReader = cmd.ExecuteReader

                If drd.Read Then
                    If Not drd(0).ToString.Equals("") Then
                        If drd(0).ToString.Equals("PLAT") Then
                            jmlplat = jmlplat + 1
                        Else
                            jmlnonplat = jmlnonplat + 1
                        End If
                    End If
                End If
                drd.Close()

            Next

            If jmlplat >= 1 And jmlnonplat >= 1 Then
                hasil = False
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

        Return hasil

    End Function

    Private Function cek_notrans() As Boolean

        Dim cn As OleDbConnection = Nothing
        Dim hasil As Boolean = False

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select COUNT(*) from tradm_gud where sbatal=0 and nobukti_trans='{0}' and jenistrans='{1}'", tbukti_tr.Text.Trim, tjenis.EditValue)

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd(0).ToString) Then
                    If Integer.Parse(drd(0).ToString) > 0 Then
                        hasil = True
                    End If
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

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fadm_gud2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If addstat Then
            tjenis.Focus()
        Else
            ttgl.Focus()
        End If
    End Sub

    Private Sub fadm_gud2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ttgl.EditValue = Date.Now
        ttgl_muat.EditValue = Date.Now
        ttgl_krim.EditValue = Date.Now

        tjenis.SelectedIndex = 0

        isi_gudang()
        isi_nopol()

        If addstat Then
            kosongkan()

            tjenis.Enabled = True
            tgudang.Enabled = True
            tnopol.Enabled = True
            tbukti_tr.Enabled = True

            tjenis_SelectedIndexChanged(Nothing, Nothing)


        Else
            isi()

            tjenis.Enabled = False
            tgudang.Enabled = False
            tgudang.Enabled = False
            tnopol.Enabled = False

            If tjenis.EditValue.Equals("TR PENJ TO") Then
                tbukti_tr.Enabled = False
            End If

        End If

    End Sub

    Private Sub tbukti_tr_Validated(sender As System.Object, e As System.EventArgs) Handles tbukti_tr.Validated

        If tbukti_tr.EditValue = "-" Then
            Return
        End If

        If tbukti_tr.Text.Trim.Length > 0 Then

            Dim sql As String = ""
            Dim cmd As OleDbCommand = Nothing
            Dim dsa As DataSet
            Dim cn As OleDbConnection = Nothing

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                If tjenis.EditValue.Equals("TR PENJ TO") Then
                    sql = String.Format("select b.kd_barang,b.kd_gudang,a.nobukti from trrekap_to2 a inner join trfaktur_to2 b on a.nobukti_fak=b.nobukti inner join trrekap_to c on a.nobukti=c.nobukti where c.sbatal=0 and a.nobukti='{0}'", tbukti_tr.Text.Trim)

                    dsa = New DataSet
                    dsa = Clsmy.GetDataSet(sql, cn)
                    dttrans = New DataTable
                    dttrans.Clear()
                    dttrans = dsa.Tables(0)

                    If dttrans.Rows.Count <= 0 Then
                        MsgBox("No bukti tidak ditemukan..", vbOKOnly + vbInformation, "Informasi")
                        tbukti_tr.Focus()
                    End If

                ElseIf tjenis.EditValue.Equals("TR KANVAS") Then
                    sql = String.Format("SELECT     trspm2.kd_barang,trspm2.kd_gudang,trspm2.nobukti " & _
                        "FROM trspm INNER JOIN trspm2 ON trspm.nobukti = trspm2.nobukti where trspm.sbatal=0 and trspm2.nobukti='{0}'", tbukti_tr.Text.Trim)

                    dsa = New DataSet
                    dsa = Clsmy.GetDataSet(sql, cn)
                    dttrans = New DataTable
                    dttrans.Clear()
                    dttrans = dsa.Tables(0)

                    If dttrans.Rows.Count <= 0 Then
                        MsgBox("No bukti tidak ditemukan..", vbOKOnly + vbInformation, "Informasi")
                        tbukti_tr.Focus()
                    End If

                ElseIf tjenis.EditValue.Equals("TR RET SISA/OUTL") Then

                    sql = String.Format("select b.kd_barang,b.kd_gudang,a.nobukti from trrekap_to2 a inner join trfaktur_to2 b on a.nobukti_fak=b.nobukti inner join trrekap_to c on a.nobukti=c.nobukti where c.sbatal=0 and a.nobukti='{0}'", tbukti_tr.Text.Trim)
                    sql = String.Format(" {0} union all ", sql)
                    sql = String.Format(" {0} SELECT     trspm2.kd_barang,trspm2.kd_gudang,trspm2.nobukti " & _
                        "FROM trspm INNER JOIN trspm2 ON trspm.nobukti = trspm2.nobukti where trspm.sbatal=0 and trspm2.nobukti='{1}'", sql, tbukti_tr.Text.Trim)

                    dsa = New DataSet
                    dsa = Clsmy.GetDataSet(sql, cn)
                    dttrans = New DataTable
                    dttrans.Clear()
                    dttrans = dsa.Tables(0)

                    If dttrans.Rows.Count <= 0 Then
                        MsgBox("No bukti tidak ditemukan..", vbOKOnly + vbInformation, "Informasi")
                        'tbukti_tr.Focus()
                    End If

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

        End If

    End Sub

    Private Sub bts_notrans_Click(sender As System.Object, e As System.EventArgs) Handles bts_notrans.Click


        If tjenis.Text.Trim.Equals("TR PENJ TO") Then

            Dim fs As New fs_notrans With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
            fs.ShowDialog(Me)

            tbukti_tr.Text = fs.get_NoBukti
            tbukti_tr_Validated(sender, Nothing)

        ElseIf tjenis.Text.Trim.Equals("TR KANVAS") Then

            Dim fs As New fs_notrans2 With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
            fs.ShowDialog(Me)

            tbukti_tr.Text = fs.get_NoBukti
            tbukti_tr_Validated(sender, Nothing)

        End If

    End Sub

    Private Sub tjenis_LostFocus(sender As Object, e As System.EventArgs) Handles tjenis.LostFocus
        If Not tjenis.Text.Trim.Equals("TR PEMB") Then
            ttimb.EditValue = 0
        End If
    End Sub

    Private Sub tjenis_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles tjenis.SelectedIndexChanged

        lblgudang.Text = "Gudang :"
        tgudang2.Enabled = False
        ttimb.Enabled = False
        ttimb.EditValue = 0

        tgudang.EditValue = "G000"
        tgudang2.EditValue = "G000"

        '  GridView2.Columns("qtyout").Visible = True
        ' GridView2.Columns("qtyin").Visible = True

        If tjenis.SelectedIndex = 1 Or tjenis.SelectedIndex = 2 Then
            bts_notrans.Visible = True

            tbukti_tr.Text = ""

        Else
            bts_notrans.Visible = False
        End If

        If tjenis.Text.Trim.Equals("TR PEMB") Then
            lbtglmuat.Text = "Tgl Bongkar :"
            lbtglberangkat.Text = "Tgl Sampai :"

            If addstat = True Then
                tbukti_tr.EditValue = "-"
            End If

            ' lblgudang.Text = "Gudang/Gd Brg Bocor :"
            tgudang2.Enabled = True
            ttimb.Enabled = True
            ttimb.EditValue = 0

            '  tgudang.EditValue = "G000"
            ' tgudang2.EditValue = "G003"

            '   GridView2.Columns("qtyout").Visible = False

        ElseIf tjenis.Text.Trim.Equals("TR PENJ TO") Then
            lbtglmuat.Text = "Tgl Muat :"
            lbtglberangkat.Text = "Tgl Berangkat :"

            If addstat = True Then
                tnodo.EditValue = "-"
            End If

            '   GridView2.Columns("qtyin").Visible = False

        ElseIf tjenis.Text.Trim.Equals("TR KANVAS") Then
            lbtglmuat.Text = "Tgl Muat :"
            lbtglberangkat.Text = "Tgl Berangkat :"

            If addstat = True Then
                tnodo.EditValue = "-"
            End If

            ' GridView2.Columns("qtyin").Visible = False

        ElseIf tjenis.Text.Trim.Equals("TR RET SISA/OUTL") Then
            lbtglmuat.Text = "Tgl Bongkar :"
            lbtglberangkat.Text = "Tgl Sampai :"

            If addstat = True Then
                tbukti_tr.EditValue = "-"
                tnodo.EditValue = "-"
            End If

            '   GridView2.Columns("qtyout").Visible = False

        ElseIf tjenis.Text.Trim.Equals("TR GLN KOSONG") Then
            lbtglmuat.Text = "Tgl Bongkar/Muat :"
            lbtglberangkat.Text = "Tgl Berangkat/Sampai :"

            If addstat = True Then
                tbukti_tr.EditValue = "-"
                tnodo.EditValue = "-"
            End If

        ElseIf tjenis.Text.Trim.Equals("TR PINJAM") Then
            lbtglmuat.Text = "Tgl Muat :"
            lbtglberangkat.Text = "Tgl Berangkat :"

            If addstat = True Then
                tbukti_tr.EditValue = "-"
                tnodo.EditValue = "-"
            End If

        ElseIf tjenis.Text.Trim.Equals("TR SEWA") Then
            lbtglmuat.Text = "Tgl Muat :"
            lbtglberangkat.Text = "Tgl Berangkat :"

            If addstat = True Then
                tbukti_tr.EditValue = "-"
                tnodo.EditValue = "-"
            End If

        ElseIf tjenis.Text.Trim.Equals("TR LAIN-LAIN") Then
            lbtglmuat.Text = "Tgl Bongkar/Muat :"
            lbtglberangkat.Text = "Tgl Berangkat/Sampai :"

            If addstat = True Then
                tbukti_tr.EditValue = "-"
                tnodo.EditValue = "-"
            End If

        ElseIf tjenis.Text.Trim.Equals("TR BOCORAN") Then

            lbtglmuat.Text = "Tgl Keluar :"
            lbtglberangkat.Text = "Tgl Keluar :"

            If addstat = True Then
                tbukti_tr.EditValue = "-"
                tnodo.EditValue = "-"
            End If

        ElseIf tjenis.Text.Trim.Equals("TR KIRIM JABUNG") Then

            lbtglmuat.Text = "Tgl Keluar :"
            lbtglberangkat.Text = "Tgl Keluar :"

            If addstat = True Then
                tbukti_tr.EditValue = "-"
                tnodo.EditValue = "-"
            End If

        End If

    End Sub

    Private Sub tbukti_tr_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tbukti_tr.KeyDown

        If e.KeyCode = Keys.F4 Then
            If bts_notrans.Visible = True Then
                bts_notrans_Click(sender, Nothing)
            End If
        End If

    End Sub

    Private Sub tnopol_Validated(sender As Object, e As System.EventArgs) Handles tnopol.Validated

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

    Private Sub tgudang_EditValueChanged(sender As Object, e As System.EventArgs) Handles tgudang.EditValueChanged
        '  If Not tjenis.EditValue.ToString.Trim.Equals("TR PEMB") Then
        tgudang2.EditValue = tgudang.EditValue
        ' End If
    End Sub

End Class

