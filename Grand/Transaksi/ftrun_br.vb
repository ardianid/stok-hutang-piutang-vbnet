Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy
Imports DevExpress.XtraReports.UI

Public Class ftrun_br

    Private bs1 As BindingSource
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private dvmanager3 As Data.DataViewManager
    Private dv3 As Data.DataView

    Dim addstat As Boolean = False

    Private Sub opengrid2()

        grid2.DataSource = Nothing
        dv2 = Nothing

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        If addstat Then
            Return
        End If

        Dim sql As String = String.Format("SELECT     trturun_br2.noid, trturun_br2.kd_barang, ms_barang.nama_barang, trturun_br2.kd_gudang, trturun_br2.qty, trturun_br2.satuan, trturun_br2.qty_tr, trturun_br2.harga, " & _
        "trturun_br2.jumlah, trturun_br2.qtykecil, trturun_br2.qtykecil_tr, trturun_br2.hargakecil, ms_barang.qty1, ms_barang.qty2, ms_barang.qty3, ms_barang.satuan1, ms_barang.satuan2, ms_barang.satuan3 " & _
        "FROM trturun_br2 INNER JOIN ms_barang ON trturun_br2.kd_barang = ms_barang.kd_barang where trturun_br2.nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager2 = New DataViewManager(ds)
            dv2 = dvmanager2.CreateDataView(ds.Tables(0))

            grid2.DataSource = dv2


            '  close_wait()


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

    Private Sub opengrid3()

        grid3.DataSource = Nothing
        dv3 = Nothing

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        If addstat Then
            Return
        End If

        Dim sql As String = String.Format("SELECT      ms_barangk.kd_barang, ms_barangk.nama_barang, ms_barangk.qty1, ms_barangk.qty2, ms_barangk.qty3, ms_barangk.satuan1, ms_barangk.satuan2, ms_barangk.satuan3,  " & _
                      "trspm2.qty, trspm2.satuan, trspm2.qty AS qty_k, 0 AS qtykecil " & _
            "FROM         trspm2 INNER JOIN " & _
                      "ms_barang ON trspm2.kd_barang = ms_barang.kd_barang INNER JOIN " & _
                      "ms_barang AS ms_barangk ON ms_barang.kd_barang_kmb = ms_barangk.kd_barang " & _
            "WHERE trspm2.nobukti='{0}'", dv1(bs1.Position)("nobukti_spm").ToString)

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        Try


            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager3 = New DataViewManager(ds)
            dv3 = dvmanager3.CreateDataView(ds.Tables(0))

            grid3.DataSource = dv3

            cek_onkosong(cn)

            '  close_wait()


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

        For i As Integer = 0 To dv3.Count - 1

            Dim sql As String = String.Format("select * from trturun_br3 where nobukti='{0}' and kd_barang='{1}' and satuan='{2}'", _
                                              dv1(bs1.Position)("nobukti").ToString, dv2(i)("kd_barang").ToString, dv2(i)("satuan"))
            cmd = New OleDbCommand(sql, cn)
            drd = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd("noid").ToString) Then

                    kdgudang = drd("kd_gudang").ToString

                    dv3(i)("qty_k") = drd("qty").ToString
                    dv3(i)("qtykecil") = drd("qtykecil").ToString
                End If
            End If

            drd.Close()

        Next

    End Sub


    Private Sub opendata()

        Dim sql As String = String.Format("SELECT trturun_br.nobukti, trturun_br.tanggal, trturun_br.tgl_turun, trturun_br.nobukti_spm, trspm.nopol, trturun_br.kd_gudang, trturun_br.note, trturun_br.sbatal,trspm.kd_supir " & _
            "FROM trturun_br INNER JOIN trspm ON trturun_br.nobukti_spm = trspm.nobukti where trturun_br.tanggal >='{0}' and trturun_br.tanggal <='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))


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

    End Sub

    Private Sub cari()

        'bs1.DataSource = Nothing
        grid1.DataSource = Nothing
        Dim cn As OleDbConnection = Nothing

        Dim sql As String = ""
        Dim scbo As Integer = tcbofind.SelectedIndex

        sql = String.Format("SELECT trturun_br.nobukti, trturun_br.tanggal, trturun_br.tgl_turun, trturun_br.nobukti_spm, trspm.nopol, trturun_br.kd_gudang, trturun_br.note, trturun_br.sbatal,trspm.kd_supir " & _
            "FROM trturun_br INNER JOIN trspm ON trturun_br.nobukti_spm = trspm.nobukti where trturun_br.tanggal >='{0}' and trturun_br.tanggal <='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))

        Select Case tcbofind.SelectedIndex
            Case 0 ' kode
                sql = String.Format("{0} and trturun_br.nobukti like '%{1}%'", sql, tfind.Text.Trim)
            Case 1 ' tgl

                If Not IsDate(tfind.Text.Trim) Then
                    MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                    Return
                Else

                    If tfind.Text.Trim.Length < 10 Or tfind.Text.Trim.Length > 10 Then
                        MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                        Return
                    End If

                End If

                sql = String.Format("{0} and trturun_br.tanggal='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))
            Case 2 ' tgl turun

                If Not IsDate(tfind.Text.Trim) Then
                    MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                    Return
                Else

                    If tfind.Text.Trim.Length < 10 Or tfind.Text.Trim.Length > 10 Then
                        MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                        Return
                    End If

                End If

                sql = String.Format("{0} and trturun_br.tgl_turun='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))
           
            Case 3 ' nopol
                sql = String.Format("{0} and trspm.nopol like '%{1}%'", sql, tfind.Text.Trim)
            Case 4 ' nobukti spm
                sql = String.Format("{0} and trturun_br.nobukti_spm like '%{1}%'", sql, tfind.Text.Trim)
            Case 5 ' sales
                sql = String.Format("{0} and trturun_br.nobukti_spm in (select trspm.nobukti from trspm inner join ms_pegawai on trspm.kd_sales=ms_pegawai.kd_karyawan where ms_pegawai.nama_karyawan like '%{1}%')", sql, tfind.Text.Trim)
            Case 6 ' supir
                sql = String.Format("{0} and trturun_br.nobukti_spm in (select trspm.nobukti from trspm inner join ms_pegawai on trspm.kd_supir=ms_pegawai.kd_karyawan where ms_pegawai.nama_karyawan like '%{1}%')", sql, tfind.Text.Trim)
            Case 7 ' ket
                sql = String.Format("{0} and trturun_br.note like '%{1}%'", sql, tfind.Text.Trim)
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

        Dim sql As String = String.Format("update trturun_br set sbatal=1,alasan_batal='{0}' where nobukti='{1}'", alasan.ToUpper, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString)
        Dim sql_spm As String = String.Format("update trspm set spulang=0 where nobukti='{0}'", dv1(Me.BindingContext(bs1).Position)("nobukti_spm").ToString)

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

            Using cmdspm As OleDbCommand = New OleDbCommand(sql_spm, cn, sqltrans)
                cmdspm.ExecuteNonQuery()
            End Using


            If hapus2(cn, sqltrans) = "ok" Then

                If Not (hapusdetail2(cn, sqltrans) = "ok") Then

                    GoTo langsung_aja

                End If

                Clsmy.InsertToLog(cn, "bbturun_kv", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString, dv1(Me.BindingContext(bs1).Position)("tanggal").ToString, sqltrans)

                sqltrans.Commit()

                dv1(bs1.Position)("sbatal") = 1

                close_wait()

                MsgBox("Data telah dibatalkan...", vbOKOnly + vbInformation, "Informasi")

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

    Private Function hapus2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim kdgudang As String = dv1(bs1.Position)("kd_gudang").ToString
        Dim nobukti As String = dv1(bs1.Position)("nobukti").ToString
        Dim kdsup As String = dv1(bs1.Position)("kd_supir").ToString
        Dim nopol As String = dv1(bs1.Position)("nopol").ToString

        Dim sql As String = String.Format("select * from trturun_br2 where nobukti='{0}'", nobukti)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim hasil As String = ""

        While drd.Read

            Dim qtykecil0 As Integer = Integer.Parse(drd("qtykecil").ToString)
            Dim qtykecil As Integer = Integer.Parse(drd("qtykecil_tr").ToString)
            Dim kdbar As String = drd("kd_barang").ToString
            Dim kdgud_mbil As String = drd("kd_gudang").ToString

            If qtykecil0 = 0 Then

                Dim hasilplusmin_old2 As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, kdgud_mbil, False, False, False)

                If Not hasilplusmin_old2.Equals("ok") Then

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilplusmin_old2, vbOKOnly + vbExclamation, "Informasi")
                    Exit While

                End If

                Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, dv1(Me.BindingContext(dv1).Position)("tgl_turun").ToString, kdgud_mbil, kdbar, qtykecil, 0, "Turun Barang (Kanvas)(Batal)", kdsup, nopol)

            Else

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
                    Dim hasilplusmin2 As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, kdgud_mbil, True, False, False)
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

                '3. insert to hist stok
                Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, dv1(Me.BindingContext(dv1).Position)("tgl_turun").ToString, kdgudang, kdbar, 0, qtykecil, "Turun Barang (Kanvas)(Batal)", kdsup, nopol)
                Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, dv1(Me.BindingContext(dv1).Position)("tgl_turun").ToString, kdgud_mbil, kdbar, qtykecil, 0, "Turun Barang (Kanvas)(Batal)", kdsup, nopol)


            End If

        End While

        drd.Close()

        If Not (hasil.Equals("error")) Then
            hasil = "ok"
        End If

        Return hasil

    End Function

    Private Function hapusdetail2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim kdgud As String
        Dim kdbar As String
        Dim qtykecil As Integer
        Dim satuan As String
        Dim qty As Integer

        Dim hasil As String = ""
        Dim ada As Boolean = False


        Dim nobukti As String = dv1(Me.BindingContext(bs1).Position)("nobukti").ToString
        Dim tglmasuk As String = dv1(Me.BindingContext(bs1).Position)("tgl_turun").ToString
        Dim kdsup As String = dv1(Me.BindingContext(bs1).Position)("kd_supir").ToString
        Dim nopol As String = dv1(Me.BindingContext(bs1).Position)("nopol").ToString

        Dim sql As String = String.Format("SELECT   *  FROM    trturun_br3  where nobukti='{0}'", nobukti)
        Dim cmds As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drds As OleDbDataReader = cmds.ExecuteReader

        While drds.Read

            kdgud = drds("kd_gudang").ToString
            kdbar = drds("kd_barang").ToString
            qtykecil = drds("qtykecil").ToString
            satuan = drds("satuan").ToString
            qty = Integer.Parse(drds("qtykecil").ToString)

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

            '3. insert to hist stok
            Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tglmasuk, kdgud, kdbar, 0, qtykecil, "Turun Barang (Kanvas)", kdsup, nopol)

        End While

        drds.Close()

        If hasil.Equals("") Then
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

            Dim sql As String = String.Format("select sbatal from trturun_br where nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd(0).ToString) Then
                    dv1(bs1.Position)("sbatal") = drd(0).ToString
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

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Data telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If MsgBox("Yakin akan dibatalkan ?", vbYesNo + vbQuestion, "Konfirmasi") = vbNo Then
            Exit Sub
        End If

        hapus()

    End Sub

    Private Sub tsadd_Click(sender As System.Object, e As System.EventArgs) Handles tsadd.Click

        addstat = True
        Using fkar2 As New ftrun_br2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0}
            fkar2.ShowDialog()
            addstat = False
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

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Data telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        addstat = True
        Using fkar2 As New ftrun_br2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.ShowDialog()
            addstat = False
        End Using

    End Sub

    Private Sub tsview_Click(sender As System.Object, e As System.EventArgs) Handles tsview.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        Using fkar2 As New ftrun_br2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.btsimpan.Enabled = False
            fkar2.btload.Enabled = False
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
            MsgBox("Data telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        Dim nobukti As String = dv1(bs1.Position)("nobukti").ToString

        Using fkar2 As New fpr_turun_br With {.nobukti = nobukti}
            fkar2.ShowDialog(Me)
        End Using

    End Sub

    Private Sub tsprint2_Click(sender As System.Object, e As System.EventArgs) Handles tsprint2.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = String.Format("select a.nobukti,a.tanggal,a.tgl_turun,a.nobukti_spm,a.kd_gudang,b.kd_barang,c.nama_lap as nama_barang,b.qty,b.satuan,b.qty_tr,b.kd_gudang as nopol,c.nohrus " & _
                "from trturun_br a inner join trturun_br2 b on a.nobukti=b.nobukti " & _
                "inner join ms_barang c on b.kd_barang=c.kd_barang " & _
                "where a.sbatal=0 and a.nobukti='{0}' " & _
                "union all " & _
                "select x1.nobukti,null as tanggal,null as tgl_turun,'' as nobukti_spm,'' as kd_gudang,x2.kd_barang,x2.nama_lap +' *(Ksg)' as nama_barang,0 as qty_a,x1.satuan,x1.qty,'' as nopol,x2.nohrus " & _
                "from trturun_br3 x1 inner join ms_barang x2 on x1.kd_barang=x2.kd_barang " & _
                "where x1.nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)

            'Dim sql2 As String = String.Format("SELECT trturun_br3.nobukti,ms_barang.kd_barang, ms_barang.nama_barang, trturun_br3.kd_gudang, trturun_br3.qty, trturun_br3.satuan " & _
            '"FROM trturun_br3 INNER JOIN " & _
            '"ms_barang ON trturun_br3.kd_barang = ms_barang.kd_barang where trturun_br3.nobukti='{0}'", nobukti)

            Dim ds As DataSet = New dsturun_brg
            ds = Clsmy.GetDataSet(sql, cn)

            'Dim ds2 As DataSet = New dsturun_brg2
            'ds2 = Clsmy.GetDataSet(sql2, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_turun_brang() With {.DataSource = ds.Tables(0)}
            rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
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


    Private Sub GridView1_Click(sender As Object, e As System.EventArgs) Handles BandedGridView1.Click
        opengrid2()
        opengrid3()
    End Sub

    Private Sub GridView1_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles BandedGridView1.SelectionChanged
        opengrid2()
        opengrid3()
    End Sub

    Private Sub BandedGridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles BandedGridView1.FocusedRowChanged
        opengrid2()
        opengrid3()
    End Sub

End Class