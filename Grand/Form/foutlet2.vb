Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class foutlet2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private bs_sal As BindingSource
    Private dvmanager_sal As Data.DataViewManager
    Private dv_sal As Data.DataView

    Private dvmanager_his As Data.DataViewManager
    Private dv_his As Data.DataView

    Private dvmanager_ksong As Data.DataViewManager
    Private dv_ksong As Data.DataView

    Private Sub kosongkan()
        tkode.Text = ""
        tnama.Text = ""
        talamat.Text = ""
        talamat2.Text = ""
        ttelp1.Text = ""
        ttelp2.Text = ""
        ttelp3.Text = ""
        ttglorder.Text = ""
        ttglregis.EditValue = Date.Now

        tpemilik1.Text = ""
        tpemilik2.Text = ""
        tpemilik3.Text = ""

        ttglhub1.EditValue = Date.Now
        ttglhub2.EditValue = Date.Now
        ttglhub3.EditValue = Date.Now

        tkd_kel.Text = ""
        tnama_kel.Text = ""
        tkd_kec.Text = ""
        tnamakec.Text = ""
        tkd_kab.Text = ""
        tnamakab.Text = ""

        tkd_ord.Text = ""
        tnama_ord.Text = ""

        tkd_krm.Text = ""
        tnama_krm.Text = ""

        tkd_psar.Text = ""
        tnama_psar.Text = ""

        '  tkd_klas.Text = ""

        tnpw.Text = ""
        tnama_np.Text = ""
        talamat_np.Text = ""

        tnote.Text = ""

        tpiutbeli.EditValue = 0
        tpiutsewa.EditValue = 0
        tretur.EditValue = 0

        ttop.EditValue = 30

        caktif.Checked = True

        caktif_CheckedChanged(Nothing, Nothing)

        isi_sales()
        isi_history()
        isi_ksong()

    End Sub

    Private Sub isi_sales()

        Dim kdtoko As String
        If tkode.Text.Trim.Length = 0 Then
            kdtoko = "xxx1xxxxx"
        Else
            kdtoko = tkode.Text.Trim
        End If

        Dim sql As String = String.Format("select a.kd_karyawan,b.nama_karyawan,a.limit_val,a.jmlpiutang from ms_toko2 a inner join ms_pegawai b on a.kd_karyawan=b.kd_karyawan where a.kd_toko='{0}'", kdtoko)

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        gridsal.DataSource = Nothing

        Try

            open_wait()

            dv_sal = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager_sal = New DataViewManager(ds)
            dv_sal = dvmanager_sal.CreateDataView(ds.Tables(0))

            bs_sal = New BindingSource
            bs_sal.DataSource = dv_sal
            bn1.BindingSource = bs_sal

            gridsal.DataSource = bs_sal

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

    Private Sub isi_history()

        Dim kdtoko As String
        If tkode.Text.Trim.Length = 0 Then
            kdtoko = "xxx1xxxxx"
        Else
            kdtoko = tkode.Text.Trim
        End If

        Dim sql As String = String.Format("select a.kd_barang,b.nama_barang,a.jml_beli,a.jml_pinjam,a.jml_sewa,a.jml_pinjam1,jml_pinjam2,jml_pinjam3,jml_sewa1,jml_sewa2,jml_sewa3 from ms_toko3 a inner join ms_barang b on a.kd_barang=b.kd_barang where a.kd_toko='{0}'", kdtoko)

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        gridhis.DataSource = Nothing

        Try

            open_wait()

            dv_his = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager_his = New DataViewManager(ds)
            dv_his = dvmanager_his.CreateDataView(ds.Tables(0))

            gridhis.DataSource = dv_his

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

    Private Sub isi_ksong()

        Dim kdtoko As String
        If tkode.Text.Trim.Length = 0 Then
            kdtoko = "xxx1xxxxx"
        Else
            kdtoko = tkode.Text.Trim
        End If

        Dim sql As String = String.Format("select ms_barang.kd_barang,ms_barang.nama_barang,ms_toko4.jml1,ms_toko4.jml2,ms_toko4.jml3 " & _
        "from ms_toko4 inner join ms_barang on ms_toko4.kd_barang=ms_barang.kd_barang where kd_toko='{0}'", kdtoko)

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        grid_ksong.DataSource = Nothing

        Try

            open_wait()

            dv_ksong = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager_ksong = New DataViewManager(ds)
            dv_ksong = dvmanager_ksong.CreateDataView(ds.Tables(0))

            grid_ksong.DataSource = dv_ksong

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

        Dim kdtoko As String = dv(position)("kd_toko").ToString
        'If tkode.Text.Trim.Length = 0 Then
        '    kdtoko = "xxx1xxxxx"
        'Else
        '    kdtoko = tkode.Text.Trim
        'End If

        Dim cn As OleDbConnection = Nothing

        Dim sql As String = String.Format("SELECT  ms_toko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, ms_toko.alamat_toko2, ms_toko.aktif, ms_toko.notelp1, ms_toko.notelp2, ms_toko.notelp3, " & _
        "ms_toko.pemilik1, ms_toko.hubungan1, ms_toko.tglhub1, ms_toko.pemilik2, ms_toko.hubungan2, ms_toko.tglhub2, ms_toko.pemilik3, ms_toko.hubungan3, " & _
        "ms_toko.tglhub3, ms_toko.kd_kel, ms_kel.nama_kel, ms_kec.kd_kec, ms_kec.nama_kec, ms_kab.kd_kab, ms_kab.nama_kab, ms_toko.kd_klas, ms_klas.nama_klas, " & _
        "ms_toko.kd_jalurkirim, ms_jalur_kirim.nama_jalur AS nama_jalur_kr, ms_toko.kd_jalurorder, ms_jalur.nama_jalur AS nama_jalur_or, ms_toko.kd_pasar, " & _
        "ms_pasar.nama_pasar, ms_toko.kd_group, ms_group.nama_np AS nama_group, ms_toko.tgl_regis, ms_toko.tgl_order, ms_toko.piutangbeli, ms_toko.piutangsewa,ms_toko.jumlahretur, " & _
        "ms_toko.piutangpinjam, ms_toko.note, ms_toko.pk, ms_toko.npw, ms_toko.nama_np, ms_toko.alamat_np,ms_toko.tgl_akhir,ms_toko.spred_to,ms_toko.top_toko " & _
        "FROM  ms_jalur_kirim INNER JOIN ms_toko INNER JOIN " & _
        "ms_group ON ms_toko.kd_group = ms_group.kd_group INNER JOIN " & _
        "ms_kec ON ms_toko.kd_kec = ms_kec.kd_kec INNER JOIN " & _
        "ms_kel ON ms_toko.kd_kel = ms_kel.kd_kel INNER JOIN " & _
            "ms_kab ON ms_toko.kd_kab = ms_kab.kd_kab INNER JOIN " & _
        "ms_klas ON ms_toko.kd_klas = ms_klas.kd_klas INNER JOIN " & _
        "ms_pasar ON ms_toko.kd_pasar = ms_pasar.kd_pasar ON ms_jalur_kirim.kd_jalur = ms_toko.kd_jalurkirim INNER JOIN " & _
        "ms_jalur ON ms_toko.kd_jalurorder = ms_jalur.kd_jalur where ms_toko.kd_toko='{0}'", kdtoko)


        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim comd As OleDbCommand = New OleDbCommand(sql, cn)

            Dim dread As OleDbDataReader = comd.ExecuteReader

            If dread.HasRows Then
                If dread.Read Then

                    tkode.EditValue = dread("kd_toko").ToString
                    tnama.EditValue = dread("nama_toko").ToString
                    talamat.EditValue = dread("alamat_toko").ToString
                    talamat2.EditValue = dread("alamat_toko2").ToString

                    Dim fakt As String = dread("aktif").ToString
                    If fakt.Equals("1") Then
                        caktif.Checked = True
                    Else
                        caktif.Checked = False
                    End If

                    Dim fspred As String = dread("spred_to").ToString
                    If fspred.Equals("1") Then
                        cspred.Checked = True
                    Else
                        cspred.Checked = False
                    End If

                    ttelp1.EditValue = dread("notelp1").ToString
                    ttelp2.EditValue = dread("notelp2").ToString
                    ttelp3.EditValue = dread("notelp3").ToString

                    tpemilik1.EditValue = dread("pemilik1").ToString
                    thub1.EditValue = dread("hubungan1").ToString

                    Dim tglhub1 As String = dread("tglhub1").ToString
                    tglhub1 = convert_date_to_ind(tglhub1)

                    ttglhub1.EditValue = tglhub1

                    tpemilik2.EditValue = dread("pemilik2").ToString
                    thub2.EditValue = dread("hubungan2").ToString

                    Dim tglhub2 As String = dread("tglhub2").ToString
                    tglhub2 = convert_date_to_ind(tglhub2)

                    ttglhub2.EditValue = tglhub2

                    tpemilik3.EditValue = dread("pemilik3").ToString
                    thub3.EditValue = dread("hubungan3").ToString

                    Dim tglhub3 As String = dread("tglhub3").ToString
                    tglhub3 = convert_date_to_ind(tglhub3)

                    ttglhub3.EditValue = tglhub3

                    tkd_krm.EditValue = dread("kd_jalurkirim").ToString
                    tnama_krm.EditValue = dread("nama_jalur_kr").ToString

                    tkd_ord.EditValue = dread("kd_jalurorder").ToString
                    tnama_ord.EditValue = dread("nama_jalur_or").ToString

                    tkd_psar.EditValue = dread("kd_pasar").ToString
                    tnama_psar.EditValue = dread("nama_pasar").ToString

                    tgroup.EditValue = dread("kd_group").ToString

                    Dim tglregis As String = dread("tgl_regis").ToString

                    tglregis = convert_date_to_ind(tglregis)

                    ttglregis.EditValue = tglregis

                    Dim tglorder As String = dread("tgl_order").ToString
                    If tglorder.Length = 0 Then
                        ttglorder.Text = ""
                    Else
                        ttglorder.EditValue = convert_date_to_ind(tglorder)
                    End If

                    Dim tglakhir As String = dread("tgl_akhir").ToString
                    If tglakhir.Length = 0 Then
                        ttglakhir.Text = ""
                    Else
                        ttglakhir.EditValue = convert_date_to_ind(tglakhir)
                    End If

                    tpiutbeli.EditValue = dread("piutangbeli").ToString
                    tpiutsewa.EditValue = dread("piutangsewa").ToString
                    tretur.EditValue = dread("jumlahretur").ToString
                    tnote.EditValue = dread("note").ToString

                    Dim spkp As String = dread("pk").ToString
                    If spkp.Equals("1") Then
                        cpkp.Checked = True
                    Else
                        cpkp.Checked = False
                    End If

                    tnpw.EditValue = dread("npw").ToString
                    tnama_np.EditValue = dread("nama_np").ToString
                    talamat_np.EditValue = dread("alamat_np").ToString

                    tkd_kel.EditValue = dread("kd_kel").ToString
                    tnama_kel.EditValue = dread("nama_kel").ToString

                    tkd_kec.EditValue = dread("kd_kec").ToString
                    tnamakec.EditValue = dread("nama_kec").ToString

                    tkd_kab.EditValue = dread("kd_kab").ToString
                    tnamakab.EditValue = dread("nama_kab").ToString

                    tkd_klas.EditValue = dread("kd_klas").ToString

                    ttop.EditValue = Integer.Parse(dread("top_toko").ToString)

                    caktif_CheckedChanged(Nothing, Nothing)

                End If
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

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Dim kaktif As String
        If caktif.Checked = True Then
            kaktif = 1
        Else
            kaktif = 0
        End If

        Dim kspred As String
        If cspred.Checked = True Then
            kspred = 1
        Else
            kspred = 0
        End If

        Dim kpkp As String
        If cpkp.Checked = True Then
            kpkp = 1
        Else
            kpkp = 0
        End If

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqlc As String = String.Format("select kd_toko from ms_toko where kd_toko='{0}'", tkode.Text.Trim)


            Dim sql_insert As String = String.Format("INSERT INTO [ms_toko] " & _
                "([kd_toko],[nama_toko],[alamat_toko],[alamat_toko2],[aktif]" & _
                ",[notelp1],[notelp2],[notelp3]" & _
                ",[pemilik1],[hubungan1],[tglhub1]" & _
                ",[pemilik2],[hubungan2],[tglhub2]" & _
                ",[pemilik3],[hubungan3],[tglhub3]" & _
                ",[kd_kel],[kd_klas],[kd_jalurkirim],[kd_jalurorder],[kd_pasar],[kd_group]" & _
                ",[tgl_regis],[note],[pk],[npw],[nama_np],[alamat_np],[spred_to],[kd_kec],[kd_kab],[top_toko]) values(" & _
                "'{0}','{1}','{2}','{3}',{4}," & _
                "'{5}','{6}','{7}'," & _
                "'{8}','{9}','{10}'," & _
                "'{11}','{12}','{13}'," & _
                "'{14}','{15}','{16}'," & _
                "'{17}','{18}','{19}','{20}','{21}','{22}'," & _
                "'{23}','{24}'," & _
                "{25},'{26}'," & _
                "'{27}','{28}',{29},'{30}','{31}',{32})", tkode.Text.Trim, tnama.Text.Trim, talamat.Text.Trim, talamat2.Text.Trim, kaktif, ttelp1.Text.Trim, ttelp2.Text.Trim, ttelp3.Text.Trim, _
            tpemilik1.Text.Trim, thub1.Text.Trim, convert_date_to_eng(ttglhub1.EditValue), tpemilik2.Text.Trim, thub2.Text.Trim, convert_date_to_eng(ttglhub2.EditValue), tpemilik3.Text.Trim, thub3.Text.Trim, convert_date_to_eng(ttglhub3.EditValue), _
            tkd_kel.EditValue, tkd_klas.EditValue, tkd_krm.EditValue, tkd_ord.EditValue, tkd_psar.EditValue, tgroup.EditValue, _
            convert_date_to_eng(ttglregis.EditValue), tnote.Text.Trim, kpkp, tnpw.Text.Trim, tnama_np.Text.Trim, talamat_np.Text.Trim, kspred, tkd_kec.EditValue, tkd_kab.EditValue, ttop.EditValue)


            Dim sql_update As String = String.Format("UPDATE [ms_toko] " & _
                "SET [nama_toko] ='{0}'" & _
                ",[alamat_toko] ='{1}'" & _
                ",[alamat_toko2] ='{2}'" & _
                ",[aktif] ={3}" & _
                ",[notelp1] ='{4}'" & _
                ",[notelp2] ='{5}'" & _
                ",[notelp3] ='{6}'" & _
                ",[pemilik1] ='{7}'" & _
                ",[hubungan1] ='{8}'" & _
                ",[tglhub1] ='{9}'" & _
                ",[pemilik2] ='{10}'" & _
                ",[hubungan2] = '{11}'" & _
                ",[tglhub2] ='{12}'" & _
                ",[pemilik3] ='{13}'" & _
                ",[hubungan3] ='{14}'" & _
                ",[tglhub3] = '{15}'" & _
                ",[kd_kel] ='{16}'" & _
                ",[kd_klas] ='{17}'" & _
                ",[kd_jalurkirim] ='{18}'" & _
                ",[kd_jalurorder] ='{19}'" & _
                ",[kd_pasar] ='{20}'" & _
                ",[kd_group] ='{21}'" & _
                ",[tgl_regis] ='{22}'" & _
                ",[note] ='{23}'" & _
                ",[pk] ={24}" & _
                ",[npw] ='{25}'" & _
                ",[nama_np] ='{26}'" & _
                ",[alamat_np] ='{27}'" & _
                ",[spred_to]={28} " & _
                ",[kd_kec]='{29}' " & _
                ",[kd_kab]='{30}' " & _
                ",[top_toko]={31} " & _
                "WHERE [kd_toko] ='{32}'", tnama.Text.Trim, talamat.Text.Trim, talamat2.Text.Trim, kaktif, ttelp1.Text.Trim, ttelp2.Text.Trim, ttelp3.Text.Trim, _
            tpemilik1.Text.Trim, thub1.Text.Trim, convert_date_to_eng(ttglhub1.EditValue), tpemilik2.Text.Trim, thub2.Text.Trim, convert_date_to_eng(ttglhub2.EditValue), tpemilik3.Text.Trim, thub3.Text.Trim, convert_date_to_eng(ttglhub3.EditValue), _
            tkd_kel.EditValue, tkd_klas.EditValue, tkd_krm.EditValue, tkd_ord.EditValue, tkd_psar.EditValue, tgroup.EditValue, _
            convert_date_to_eng(ttglregis.EditValue), tnote.Text.Trim, kpkp, tnpw.Text.Trim, tnama_np.Text.Trim, talamat_np.Text.Trim, kspred, tkd_kec.EditValue, tkd_kab.EditValue, ttop.EditValue, tkode.Text.Trim)

            sqltrans = cn.BeginTransaction

            Dim comd As OleDbCommand

            If addstat = True Then

                Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
                Dim dread As OleDbDataReader = cmdc.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        Dim kdka As String = dread(0).ToString

                        If kdka.Trim.Length = 0 Then
                            comd = New OleDbCommand(sql_insert, cn, sqltrans)
                            comd.ExecuteNonQuery()

                            Clsmy.InsertToLog(cn, "bttoko", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                            insertview()
                        Else

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox("Kode sudah ada ...", vbOKOnly + vbExclamation, "Informasi")
                            tkode.Focus()
                            Return
                        End If
                    Else
                        comd = New OleDbCommand(sql_insert, cn, sqltrans)
                        comd.ExecuteNonQuery()

                        Clsmy.InsertToLog(cn, "bttoko", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                        insertview()
                    End If
                Else
                    comd = New OleDbCommand(sql_insert, cn, sqltrans)
                    comd.ExecuteNonQuery()

                    Clsmy.InsertToLog(cn, "bttoko", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                    insertview()
                End If

                dread.Close()


            Else
                comd = New OleDbCommand(sql_update, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "bttoko", 0, 1, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                updateview()

            End If

            simpan2(cn, sqltrans)

            sqltrans.Commit()
            MsgBox("Data telah disimpan...", vbOKOnly + vbInformation, "Informasi")

            If addstat = True Then
                kosongkan()
                tkode.Focus()
            Else
                Me.Close()
            End If


        Catch ex As Exception
            close_wait()

            If Not IsNothing(sqltrans) Then
                sqltrans.Rollback()
            End If

            MsgBox(ex.ToString)
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try


    End Sub

    Private Sub simpan2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        If IsNothing(dv_sal) Then
            Return
        End If

        If dv_sal.Count <= 0 Then
            Return
        End If

        Dim cmds As OleDbCommand
        Dim cmd As OleDbCommand
        Dim dred As OleDbDataReader

        For i As Integer = 0 To dv_sal.Count - 1

            Dim kdkary As String = dv_sal(i)("kd_karyawan").ToString

            Dim sqlc As String = String.Format("select noid from ms_toko2 where kd_toko='{0}' and kd_karyawan='{1}'", tkode.Text.Trim, kdkary)
            Dim sqlin As String = String.Format("insert into ms_toko2 (kd_toko,kd_karyawan,limit_val) values('{0}','{1}',{2})", tkode.Text.Trim, kdkary, Replace(dv_sal(i)("limit_val").ToString, ",", "."))
            Dim sqlup As String = String.Format("update ms_toko2 set limit_val={0} where kd_toko='{1}' and kd_karyawan='{2}'", Replace(dv_sal(i)("limit_val").ToString, ",", "."), tkode.Text.Trim, kdkary)

            cmds = New OleDbCommand(sqlc, cn, sqltrans)
            dred = cmds.ExecuteReader

            If dred.HasRows Then

                If dred.Read Then
                    Dim noids As String = dred(0).ToString

                    If IsNumeric(noids) Then
                        cmd = New OleDbCommand(sqlup, cn, sqltrans)
                        cmd.ExecuteNonQuery()
                    Else
                        cmd = New OleDbCommand(sqlin, cn, sqltrans)
                        cmd.ExecuteNonQuery()
                    End If

                Else
                    cmd = New OleDbCommand(sqlin, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End If

            Else
                cmd = New OleDbCommand(sqlin, cn, sqltrans)
                cmd.ExecuteNonQuery()
            End If

            dred.Close()

        Next

    End Sub

    Private Sub updateview()

        Dim kaktif As String
        If caktif.Checked = True Then
            kaktif = 1
        Else
            kaktif = 0
        End If

        dv(position)("aktif") = kaktif
        dv(position)("kd_toko") = tkode.Text.Trim
        dv(position)("nama_toko") = tnama.Text.Trim
        dv(position)("alamat_toko") = talamat.Text.Trim

        dv(position)("notelp1") = ttelp1.Text.Trim
        dv(position)("notelp2") = ttelp2.Text.Trim
        dv(position)("notelp3") = ttelp3.Text.Trim

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew

        Dim kaktif As String
        If caktif.Checked = True Then
            kaktif = 1
        Else
            kaktif = 0
        End If

        orow("aktif") = kaktif
        orow("kd_toko") = tkode.Text.Trim
        orow("nama_toko") = tnama.Text.Trim
        orow("alamat_toko") = talamat.Text.Trim

        orow("notelp1") = ttelp1.Text.Trim
        orow("notelp2") = ttelp2.Text.Trim
        orow("notelp3") = ttelp3.Text.Trim

        dv.EndInit()

    End Sub

    Private Sub isi_group()

        Const sql = "select * from ms_group"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet
        Dim dvs As DataView

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvs = dvm.CreateDataView(ds.Tables(0))

            tgroup.Properties.DataSource = dvs


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

    Private Sub isi_klas()

        Const sql = "select * from ms_klas"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet
        Dim dvs As DataView

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvs = dvm.CreateDataView(ds.Tables(0))

            tkd_klas.Properties.DataSource = dvs


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

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tkode.Text.Trim.Length = 0 Then
            MsgBox("Kode tidak boleh kosong...", vbOKOnly + vbExclamation, "Informasi")
            tkode.Focus()
            Return
        End If

        If tnama.Text.Trim.Length = 0 Then
            MsgBox("Nama tidak boleh kosong...", vbOKOnly + vbExclamation, "Informasi")
            tnama.Focus()
            Return
        End If

        If tgroup.EditValue = "" Then
            MsgBox("Group outlet tidak boleh kosong...", vbOKOnly + vbExclamation, "Informasi")
            tgroup.Focus()
            Return
        End If

        If tkd_kel.Text.Trim.Length <= 0 Or tnama_kel.Text.Trim.Length <= 0 Then
            XtraTabControl1.SelectedTabPageIndex = 1
            MsgBox("Kelurahan tidak boleh kosong...", vbOKOnly + vbExclamation, "Informasi")
            tkd_kel.Focus()
            Return
        End If

        If tkd_ord.Text.Trim.Length <= 0 Or tnama_ord.Text.Trim.Length <= 0 Then
            XtraTabControl1.SelectedTabPageIndex = 1
            MsgBox("Jalur order tidak boleh kosong...", vbOKOnly + vbExclamation, "Informasi")
            tkd_ord.Focus()
            Return
        End If

        If tkd_krm.Text.Trim.Length <= 0 Or tnama_krm.Text.Trim.Length <= 0 Then
            XtraTabControl1.SelectedTabPageIndex = 1
            MsgBox("Jalur kirim tidak boleh kosong...", vbOKOnly + vbExclamation, "Informasi")
            tkd_krm.Focus()
            Return
        End If

        If tkd_psar.Text.Trim.Length <= 0 Or tnama_psar.Text.Trim.Length <= 0 Then
            XtraTabControl1.SelectedTabPageIndex = 1
            MsgBox("Pasar tidak boleh kosong...", vbOKOnly + vbExclamation, "Informasi")
            tkd_psar.Focus()
            Return
        End If

        If tkd_klas.EditValue = "" Then
            XtraTabControl1.SelectedTabPageIndex = 1
            MsgBox("Klasifikasi outlet tidak boleh kosong...", vbOKOnly + vbExclamation, "Informasi")
            tkd_klas.Focus()
            Return
        End If

        If cpkp.Checked = True Then
            If tnpw.Text.Trim.Length = 0 Then
                MsgBox("NPWP Harus Diisi...", vbOKOnly + vbInformation, "Informasi")
                tnpw.Focus()
                Return
            End If

            If tnpw.Text.Trim.Length < 20 And tnpw.Text.Trim.Length > 20 Then
                MsgBox("NPWP harus 20 digit...", vbOKOnly + vbInformation, "Informasi")
                tnpw.Focus()
                Return
            End If

        End If

        If ttop.EditValue = 0 Then
            MsgBox("TOP harus diisi...", vbOKOnly + vbInformation, "Informasi")
            ttop.Focus()
            Return
        End If

        simpan()

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fkab2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If addstat = True Then
            tkode.Focus()
        Else
            tnama.Focus()
        End If
    End Sub

    Private Sub fkab2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        isi_group()
        isi_klas()

        If addstat = True Then

            tkode.Enabled = True
            kosongkan()

            btcek.Visible = True

        Else

            btcek.Visible = False

            tkode.Enabled = False
            isi()

            isi_sales()
            isi_history()
            isi_ksong()

        End If

    End Sub

    Private Sub bts_kel_Click(sender As System.Object, e As System.EventArgs) Handles bts_kel.Click

        Dim fs As New fskel With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_kel.EditValue = fs.get_KODE
        tkd_kel_Validated(sender, Nothing)

        'tnamakec.Text = fs.get_nama_kec
        'tnama_kel.EditValue = fs.get_NAMA
        'tnamakab.Text = fs.get_nama_kab

    End Sub

    Private Sub tkd_kel_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_kel.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_kel_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_kel_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_kel.LostFocus
        If tkd_kel.Text.Trim.Length = 0 Then
            tnama_kel.Text = ""
            ' tnamakec.Text = ""
            ' tnamakab.Text = ""
        End If
    End Sub

    Private Sub tkd_kel_Validated(sender As System.Object, e As System.EventArgs) Handles tkd_kel.Validated

        If tkd_kel.Text.Trim.Length > 0 Then

            'If addstat = False Then
            '    If tnamakec.Text.Trim.Length > 0 Or tnamakab.Text.Trim.Length > 0 Then
            '        If MsgBox("Kab dan Kec akan dirubah ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.No Then
            '            Return
            '        End If
            '    End If
            'End If

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select a.kd_kel,a.nama_kel,b.kd_kec,b.nama_kec,c.kd_kab,c.nama_kab from ms_kel a inner join ms_kec b on a.kd_kec=b.kd_kec inner join ms_kab c on b.kd_kab=c.kd_kab where a.kd_kel='{0}'", tkd_kel.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_kel.EditValue = dread("kd_kel").ToString
                        tnama_kel.EditValue = dread("nama_kel").ToString

                        'tkd_kec.EditValue = dread("kd_kec").ToString
                        'tnamakec.EditValue = dread("nama_kec").ToString

                        'tkd_kab.EditValue = dread("kd_kab").ToString
                        'tnamakab.EditValue = dread("nama_kab").ToString

                    Else
                        '  tkd_kel.EditValue = ""
                        tnama_kel.EditValue = ""
                        'tnamakec.EditValue = ""
                        'tnamakab.EditValue = ""

                        'tkd_kab.EditValue = ""
                        'tkd_kec.EditValue = ""

                    End If
                Else
                    '   tkd_kel.EditValue = ""

                    'tkd_kab.EditValue = ""
                    'tkd_kec.EditValue = ""

                    tnama_kel.EditValue = ""
                    'tnamakec.EditValue = ""
                    'tnamakab.EditValue = ""
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

    Private Sub bts_ord_Click(sender As System.Object, e As System.EventArgs) Handles bts_ord.Click

        Dim fs As New fsjalur_ord With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_ord.EditValue = fs.get_KODE
        tnama_ord.EditValue = fs.get_NAMA

    End Sub

    Private Sub tkd_ord_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_ord.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_ord_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_ord_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_ord.LostFocus
        If tkd_ord.Text.Trim.Length = 0 Then
            tkd_ord.EditValue = ""
            tnama_ord.EditValue = ""
        End If
    End Sub

    Private Sub tkd_ord_Validated(sender As System.Object, e As System.EventArgs) Handles tkd_ord.Validated

        If tkd_ord.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select * from ms_jalur where kd_jalur='{0}'", tkd_ord.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_ord.EditValue = dread("kd_jalur").ToString
                        tnama_ord.EditValue = dread("nama_jalur").ToString


                    Else
                        tkd_ord.EditValue = ""
                        tnama_ord.EditValue = ""
                    End If
                Else
                    tkd_ord.EditValue = ""
                    tnama_ord.EditValue = ""
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

    Private Sub bts_krm_Click(sender As System.Object, e As System.EventArgs) Handles bts_krm.Click
        Dim fs As New fsjalur_kr With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_krm.EditValue = fs.get_KODE
        tnama_krm.EditValue = fs.get_NAMA
    End Sub

    Private Sub tkd_krm_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_krm.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_krm_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_krm_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_krm.LostFocus
        If tkd_krm.Text.Trim.Length = 0 Then
            tkd_krm.EditValue = ""
            tnama_krm.EditValue = ""
        End If
    End Sub

    Private Sub tkd_krm_Validated(sender As System.Object, e As System.EventArgs) Handles tkd_krm.Validated
        If tkd_krm.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select * from ms_jalur_kirim where kd_jalur='{0}'", tkd_krm.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_krm.EditValue = dread("kd_jalur").ToString
                        tnama_krm.EditValue = dread("nama_jalur").ToString


                    Else
                        tkd_krm.EditValue = ""
                        tnama_krm.EditValue = ""
                    End If
                Else
                    tkd_krm.EditValue = ""
                    tnama_krm.EditValue = ""
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

    Private Sub bts_psar_Click(sender As System.Object, e As System.EventArgs) Handles bts_psar.Click
        Dim fs As New fspasar With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_psar.EditValue = fs.get_KODE
        tnama_psar.EditValue = fs.get_NAMA
    End Sub

    Private Sub tkd_psar_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_psar.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_psar_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_psar_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_psar.LostFocus
        If tkd_psar.Text.Trim.Length = 0 Then
            tkd_psar.EditValue = ""
            tnama_psar.EditValue = ""
        End If
    End Sub

    Private Sub tkd_psar_Validated(sender As System.Object, e As System.EventArgs) Handles tkd_psar.Validated
        If tkd_psar.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select  * from ms_pasar where kd_pasar='{0}'", tkd_psar.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_psar.EditValue = dread("kd_pasar").ToString
                        tnama_psar.EditValue = dread("nama_pasar").ToString


                    Else
                        tkd_psar.EditValue = ""
                        tnama_psar.EditValue = ""
                    End If
                Else
                    tkd_psar.EditValue = ""
                    tnama_psar.EditValue = ""
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

    Private Sub tsref_Click(sender As System.Object, e As System.EventArgs) Handles tsref.Click
        isi_sales()
    End Sub

    Private Sub tsadd_Click(sender As System.Object, e As System.EventArgs) Handles tsadd.Click

        Using fkar2 As New foutlet3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv_sal, .addstat = True, .position = 0}
            fkar2.ShowDialog()
        End Using

    End Sub

    Private Sub tsedit_Click(sender As System.Object, e As System.EventArgs) Handles tsedit.Click

        If IsNothing(dv_sal) Then
            Exit Sub
        End If

        If dv_sal.Count < 1 Then
            Exit Sub
        End If

        Using fkar2 As New foutlet3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv_sal, .addstat = False, .position = bs_sal.Position}
            fkar2.ShowDialog()
        End Using

    End Sub

    Private Sub tsdel_Click(sender As System.Object, e As System.EventArgs) Handles tsdel.Click

        If IsNothing(dv_sal) Then
            Return
        End If

        If dv_sal.Count <= 0 Then
            Return
        End If

        Dim kdkaryawan As String = dv_sal.Item(bs_sal.Position)("kd_karyawan").ToString

        Dim cn As OleDbConnection = Nothing
        Dim sql As String = String.Format("delete from ms_toko2 where kd_toko='{0}' and kd_karyawan='{1}'", tkode.Text.Trim, kdkaryawan)

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim comd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            dv_sal.Delete(bs_sal.Position)

            sqltrans.Commit()

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

    Private Sub caktif_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles caktif.CheckedChanged, cpkp.CheckedChanged, cspred.CheckedChanged
        If caktif.Checked = True Then
            caktif.Text = "Ya"
        Else
            caktif.Text = "Tidak"
        End If

        If cspred.Checked = True Then
            cspred.Text = "Ya"
        Else
            cspred.Text = "Tidak"
        End If

        If cpkp.Checked = True Then
            cpkp.Text = "Ya"

            tnpw.Enabled = True
            tnama_np.Enabled = True
            talamat_np.Enabled = True

        Else
            cpkp.Text = "Tidak"

            tnpw.Enabled = False
            tnama_np.Enabled = False
            talamat_np.Enabled = False

        End If

    End Sub

    Private Sub btcek_Click(sender As System.Object, e As System.EventArgs) Handles btcek.Click

        Dim fs As New fsoutlet With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .kdsales = "cek_outlet"}
        fs.ShowDialog(Me)

    End Sub

    Private Sub tkd_kec_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_kec.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_kec_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_kec_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_kec.LostFocus
        If tkd_kec.Text.Trim.Length = 0 Then
            tnamakec.EditValue = ""
        End If
    End Sub

    Private Sub tkd_kec_Validated(sender As System.Object, e As System.EventArgs) Handles tkd_kec.Validated

        If tkd_kec.Text.Trim.Length > 0 Then

            'If addstat = False Then
            '    If tnamakab.Text.Trim.Length > 0 Then
            '        If MsgBox("Kab akan dirubah ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.No Then
            '            Return
            '        End If
            '    End If
            'End If

            Dim sql As String = String.Format("SELECT ms_kec.kd_kec, ms_kec.nama_kec, ms_kab.kd_kab, ms_kab.nama_kab " & _
            "FROM  ms_kec INNER JOIN ms_kab ON ms_kec.kd_kab = ms_kab.kd_kab " & _
            "WHERE ms_kec.kd_kec='{0}'", tkd_kec.Text.Trim)

            Dim cn As OleDbConnection = Nothing
            Try

                tnamakec.EditValue = ""
                'tkd_kab.EditValue = ""
                'tnamakab.EditValue = ""

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim drd As OleDbDataReader = cmd.ExecuteReader

                If drd.Read Then
                    tnamakec.EditValue = drd("nama_kec").ToString
                    'tkd_kab.EditValue = drd("kd_kab").ToString
                    'tnamakab.EditValue = drd("nama_kab").ToString
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


        End If

    End Sub

    Private Sub bts_kec_Click(sender As System.Object, e As System.EventArgs) Handles bts_kec.Click

        Dim fs As New fskec With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_kec.EditValue = fs.get_KODE
        tkd_kec_Validated(sender, Nothing)

    End Sub

    Private Sub tkd_kab_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_kab.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_kab_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_kab_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_kab.LostFocus
        If tkd_kab.Text.Trim.Length = 0 Then
            tnamakab.EditValue = ""
        End If
    End Sub

    Private Sub tkd_kab_Validated(sender As System.Object, e As System.EventArgs) Handles tkd_kab.Validated

        If tkd_kab.Text.Trim.Length > 0 Then

            Dim sql As String = String.Format("select * from ms_kab where kd_kab='{0}'", tkd_kab.Text.Trim)

            Dim cn As OleDbConnection = Nothing
            Try

                tnamakab.EditValue = ""

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim drd As OleDbDataReader = cmd.ExecuteReader

                If drd.Read Then
                    tkd_kab.EditValue = drd("kd_kab").ToString
                    tnamakab.EditValue = drd("nama_kab").ToString
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


        End If

    End Sub

    Private Sub bts_kab_Click(sender As System.Object, e As System.EventArgs) Handles bts_kab.Click

        Dim fs As New fs_kab With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_kab.EditValue = fs.get_KODE
        tkd_kab_Validated(sender, Nothing)

    End Sub

End Class