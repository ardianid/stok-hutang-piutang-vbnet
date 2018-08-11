Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class trspm

    Private bs1 As BindingSource
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Dim statadd As Boolean = False

    Private Sub opendata()

        Dim sql As String = String.Format("SELECT trspm.kd_gudang,trspm.nobukti, trspm.tanggal, trspm.tglmuat, trspm.tglberangkat, trspm.nopol, trspm.kd_supir, ms_pegawai.nama_karyawan AS namasupir,trspm.kd_sales,sales.nama_karyawan as nama_sales, trspm.note, trspm.sbatal, trspm.smuat, trspm.spulang " & _
            "FROM trspm INNER JOIN ms_pegawai ON trspm.kd_supir = ms_pegawai.kd_karyawan " & _
            "left outer join ms_pegawai as sales on trspm.kd_sales=sales.kd_karyawan where trspm.tanggal >='{0}' and trspm.tanggal <='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))


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

        If dv1.Count > 0 Then
            bs1.MoveFirst()
            opendetail()
        Else
            opendetail()
        End If

    End Sub

    Private Sub opendetail()

        grid2.DataSource = Nothing
        dv2 = Nothing

        If statadd Then
            Return
        End If

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim sql As String = String.Format("SELECT     trspm2.noid, trspm2.kd_gudang, trspm2.kd_barang, ms_barang.nama_barang, trspm2.qty, trspm2.satuan, trspm2.harga, trspm2.jumlah, trspm2.qtykecil, trspm2.hargakecil " & _
                "FROM         trspm2 INNER JOIN " & _
                "ms_barang ON trspm2.kd_barang = ms_barang.kd_barang INNER JOIN ms_gudang ON trspm2.kd_gudang = ms_gudang.kd_gudang where trspm2.nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)


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

        sql = String.Format("SELECT trspm.kd_gudang,trspm.nobukti, trspm.tanggal, trspm.tglmuat, trspm.tglberangkat, trspm.nopol, trspm.kd_supir, ms_pegawai.nama_karyawan AS namasupir,trspm.kd_sales,sales.nama_karyawan as nama_sales, trspm.note, trspm.sbatal, trspm.smuat, trspm.spulang " & _
            "FROM trspm INNER JOIN ms_pegawai ON trspm.kd_supir = ms_pegawai.kd_karyawan " & _
            "left outer join ms_pegawai as sales on trspm.kd_sales=sales.kd_karyawan where trspm.tanggal >='{0}' and trspm.tanggal <='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))

        Select Case tcbofind.SelectedIndex
            Case 0 ' kode
                sql = String.Format("{0} and trspm.nobukti like '%{1}%'", sql, tfind.Text.Trim)
            Case 1 ' tgl rekap

                If Not IsDate(tfind.Text.Trim) Then
                    MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                    Return
                Else

                    If tfind.Text.Trim.Length < 10 Or tfind.Text.Trim.Length > 10 Then
                        MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                        Return
                    End If

                End If

                sql = String.Format("{0} and trspm.tanggal='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))
            Case 2 ' tgl muat

                If Not IsDate(tfind.Text.Trim) Then
                    MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                    Return
                Else

                    If tfind.Text.Trim.Length < 10 Or tfind.Text.Trim.Length > 10 Then
                        MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                        Return
                    End If

                End If

                sql = String.Format("{0} and trspm.tglmuat='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))

            Case 3 ' tgl kirim

                If Not IsDate(tfind.Text.Trim) Then
                    MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                    Return
                Else

                    If tfind.Text.Trim.Length < 10 Or tfind.Text.Trim.Length > 10 Then
                        MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                        Return
                    End If

                End If

                sql = String.Format("{0} and trspm.tglberangkat='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))
            Case 4 ' sales
                sql = String.Format("{0} and sales.nama_karyawan like '%{1}%'", sql, tfind.Text.Trim)
            Case 5 ' supir
                sql = String.Format("{0} and ms_pegawai.nama_karyawan like '%{1}%'", sql, tfind.Text.Trim)
            Case 6 ' nopol
                sql = String.Format("{0} and trspm.nopol like '%{1}%'", sql, tfind.Text.Trim)
            Case 7 ' ket
                sql = String.Format("{0} and trspm.note like '%{1}%'", sql, tfind.Text.Trim)
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

        Dim sql As String = String.Format("update trspm set sbatal=1,alasan_batal='{0}' where nobukti='{1}'", alasan.ToUpper, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString)

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            If hapus2(cn, sqltrans) = "ok" Then

                comd = New OleDbCommand(sql, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btspm", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString, dv1(Me.BindingContext(bs1).Position)("tanggal").ToString, sqltrans)

                sqltrans.Commit()

                dv1(bs1.Position)("sbatal") = 1

                close_wait()

                MsgBox("Data telah dibatalkan...", vbOKOnly + vbInformation, "Informasi")

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

    Private Function hapus2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim kdgudang_mobil As String = dv1(bs1.Position)("kd_gudang").ToString
        Dim nobukti As String = dv1(bs1.Position)("nobukti").ToString
        Dim tgl_muat As String = dv1(bs1.Position)("tglmuat").ToString
        Dim kdsup As String = dv1(bs1.Position)("kd_supir").ToString
        Dim nopol As String = dv1(bs1.Position)("nopol").ToString

        Dim sql As String = String.Format("select * from trspm2 where nobukti='{0}'", nobukti)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim hasil As String = ""

        If drd.HasRows Then

            While drd.Read

                Dim qtykecil As Integer = Integer.Parse(drd("qtykecil").ToString)
                Dim kdbar As String = drd("kd_barang").ToString
                Dim kdgud As String = drd("kd_gudang").ToString

                '2. update barang
                Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, True, False, True)
                If Not hasilplusmin.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                    hasil = "error"
                    Exit While
                Else
                    Dim hasilplusmin2 As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, kdgudang_mobil, False, False, True)
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
                Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tgl_muat, kdgud, kdbar, qtykecil, 0, "Muat Barang (Kanvas)(Batal)", kdsup, nopol)
                Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tgl_muat, kdgudang_mobil, kdbar, 0, qtykecil, "Muat Barang (Kanvas)(Batal)", kdsup, nopol)

            End While
        Else
            hasil = "error"
        End If

        drd.Close()

        If Not (hasil.Equals("error")) Then
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
        Else
            tsprint.Enabled = False
        End If

    End Sub

    Private Sub cekbatal_onserver()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select smuat,spulang,sbatal from trspm where nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd(0).ToString) Then
                    dv1(bs1.Position)("smuat") = drd(0).ToString
                    dv1(bs1.Position)("spulang") = drd(1).ToString
                    dv1(bs1.Position)("sbatal") = drd(2).ToString
                End If
            End If
            drd.Close()


        Catch ex As Exception

        End Try

    End Sub

    Private Sub fuser_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        statadd = False

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

        If dv1(bs1.Position)("spulang").ToString.Equals("1") Then
            MsgBox("Mobil sudah pulang...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If dv1(bs1.Position)("smuat").ToString.Equals("1") Then
            MsgBox("Barang telah Dimuat...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("SPM telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If MsgBox("Yakin akan dibatalkan ?", vbYesNo + vbQuestion, "Konfirmasi") = vbNo Then
            Exit Sub
        End If

        hapus()

    End Sub

    Private Sub tsadd_Click(sender As System.Object, e As System.EventArgs) Handles tsadd.Click

        Dim stprint As Boolean
        If tsprint.Enabled = True Then
            stprint = True
        Else
            stprint = False
        End If

        statadd = True
        Using fkar2 As New trsppm2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0, .statprint = stprint}
            fkar2.ShowDialog()
            statadd = False
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

        If dv1(bs1.Position)("smuat").ToString.Equals("1") Then
            MsgBox("Rekap telah Dimuat...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If dv1(bs1.Position)("spulang").ToString.Equals("1") Then
            MsgBox("Rekap telah diantar...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Rekap telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        Dim stprint As Boolean
        If tsprint.Enabled = True Then
            stprint = True
        Else
            stprint = False
        End If

        statadd = True
        Using fkar2 As New trsppm2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position,.statprint=stprint}
            fkar2.ShowDialog()
            statadd = False
        End Using


    End Sub

    Private Sub tsview_Click(sender As System.Object, e As System.EventArgs) Handles tsview.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        Using fkar2 As New trsppm2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.btsimpan.Enabled = False
            fkar2.btadd.Enabled = False
            fkar2.btdel.Enabled = False
            fkar2.ShowDialog()
        End Using

    End Sub

    Private Sub GallonToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles GallonToolStripMenuItem.Click

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

        Using fkar2 As New fpr_spm With {.nobukti = nobukti, .tipe = 1}
            fkar2.ShowDialog(Me)
        End Using

    End Sub

    Private Sub DusToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DusToolStripMenuItem.Click
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

        Using fkar2 As New fpr_spm With {.nobukti = nobukti, .tipe = 2}
            fkar2.ShowDialog(Me)
        End Using
    End Sub

    Private Sub AllToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AllToolStripMenuItem.Click

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

        Using fkar2 As New fpr_spm With {.nobukti = nobukti, .tipe = 3}
            fkar2.ShowDialog(Me)
        End Using

    End Sub

    Private Sub BandedGridView1_Click(sender As Object, e As System.EventArgs) Handles BandedGridView1.Click
        opendetail()
    End Sub

    Private Sub BandedGridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles BandedGridView1.FocusedRowChanged
        opendetail()
    End Sub

    Private Sub BandedGridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles BandedGridView1.RowClick
        opendetail()
    End Sub

    Private Sub BandedGridView1_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles BandedGridView1.SelectionChanged
        opendetail()
    End Sub

End Class