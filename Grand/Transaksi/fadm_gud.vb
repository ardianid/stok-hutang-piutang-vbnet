Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fadm_gud

    Private bs1 As BindingSource
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private statmanipulate As Boolean

    Private Sub opendata()

        Dim tglawal As String = DateAdd(DateInterval.Day, -5, Date.Now)

        Dim sql As String = String.Format("SELECT dbo.tradm_gud.nobukti, dbo.tradm_gud.tanggal, dbo.tradm_gud.tglmuat, dbo.tradm_gud.tglberangkat, dbo.tradm_gud.jenistrans, " & _
            "dbo.tradm_gud.nobukti_trans, dbo.tradm_gud.nobukti_gd, dbo.tradm_gud.kd_gudang, dbo.ms_pegawai.nama_karyawan AS nama_krani, dbo.tradm_gud.nopol, " & _
            "dbo.tradm_gud.shit, dbo.tradm_gud.sbatal, dbo.tradm_gud.kd_gudang2, dbo.tradm_gud.kd_supir, supir.nama_karyawan AS nama_supir, dbo.tradm_gud.note " & _
            "FROM         dbo.tradm_gud INNER JOIN " & _
            "dbo.ms_pegawai ON dbo.tradm_gud.kd_krani = dbo.ms_pegawai.kd_karyawan INNER JOIN " & _
            "dbo.ms_pegawai AS supir ON dbo.tradm_gud.kd_supir = supir.kd_karyawan " & _
            "where dbo.tradm_gud.tanggal >='{0}' and  dbo.tradm_gud.tanggal <='{1}'", convert_date_to_eng(tglawal), convert_date_to_eng(tglperiod2))


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

        If dv1.Count > 0 Then
            bs1.MoveLast()
            opendata_br()
        Else
            opendata_br()
        End If

    End Sub

    Private Sub cari()

        'bs1.DataSource = Nothing
        grid1.DataSource = Nothing
        Dim cn As OleDbConnection = Nothing

        Dim sql As String = ""
        Dim scbo As Integer = tcbofind.SelectedIndex

        sql = String.Format("SELECT dbo.tradm_gud.nobukti, dbo.tradm_gud.tanggal, dbo.tradm_gud.tglmuat, dbo.tradm_gud.tglberangkat, dbo.tradm_gud.jenistrans, " & _
            "dbo.tradm_gud.nobukti_trans, dbo.tradm_gud.nobukti_gd, dbo.tradm_gud.kd_gudang, dbo.ms_pegawai.nama_karyawan AS nama_krani, dbo.tradm_gud.nopol, " & _
            "dbo.tradm_gud.shit, dbo.tradm_gud.sbatal, dbo.tradm_gud.kd_gudang2, dbo.tradm_gud.kd_supir, supir.nama_karyawan AS nama_supir, dbo.tradm_gud.note " & _
            "FROM         dbo.tradm_gud INNER JOIN " & _
            "dbo.ms_pegawai ON dbo.tradm_gud.kd_krani = dbo.ms_pegawai.kd_karyawan INNER JOIN " & _
            "dbo.ms_pegawai AS supir ON dbo.tradm_gud.kd_supir = supir.kd_karyawan " & _
            "where dbo.tradm_gud.tanggal >='{0}' and  dbo.tradm_gud.tanggal <='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))

        Select Case tcbofind.SelectedIndex
            Case 0 ' nobukti
                sql = String.Format("{0} and dbo.tradm_gud.nobukti like '%{1}%'", sql, tfind.Text.Trim)
            Case 1 ' tanggal

                If Not IsDate(tfind.Text.Trim) Then
                    MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                    Return
                Else

                    If tfind.Text.Trim.Length < 10 Or tfind.Text.Trim.Length > 10 Then
                        MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                        Return
                    End If

                End If

                sql = String.Format("{0} and dbo.tradm_gud.tanggal='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))
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

                sql = String.Format("{0} and dbo.tradm_gud.tglmuat='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))
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

                sql = String.Format("{0} and dbo.tradm_gud.tglberangkat='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))
            Case 4 ' no trans
                sql = String.Format("{0} and dbo.tradm_gud.nobukti_trans like '%{1}%'", sql, tfind.Text.Trim)
            Case 5 ' nobukti gudang
                sql = String.Format("{0} and dbo.tradm_gud.nobukti_gd like '%{1}%'", sql, tfind.Text.Trim)
            Case 6 ' no polisi
                sql = String.Format("{0} and dbo.tradm_gud.nopol like '%{1}%'", sql, tfind.Text.Trim)
            Case 7 ' supir
                sql = String.Format("{0} and supir.nama_karyawan like '%{1}%'", sql, tfind.Text.Trim)
            Case 8 ' pengawas
                sql = String.Format("{0} and ms_pegawai.nama_karyawan like '%{1}%'", sql, tfind.Text.Trim)
            Case 9
                sql = String.Format("{0} and dbo.tradm_gud.note like '%{1}%'", sql, tfind.Text.Trim)
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


    Private Sub opendata_br()

        grid2.DataSource = Nothing
        dv2 = Nothing

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim sql As String = String.Format("select ms_barang.kd_barang,ms_barang.nama_barang,tradm_gud2.qtyin,tradm_gud2.qtyout,tradm_gud2.satuan " & _
        "from tradm_gud2 inner join ms_barang on tradm_gud2.kd_barang=ms_barang.kd_barang " & _
        "where tradm_gud2.nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)

        Dim cn As OleDbConnection = Nothing

        Try


            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New DataSet()
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


    Private Sub hapus()

        Dim alasan = ""
        Using falasanb As New fkonfirm_hapus With {.WindowState = FormWindowState.Normal, .StartPosition = FormStartPosition.CenterScreen}
            falasanb.ShowDialog()
            alasan = falasanb.get_alasan
        End Using

        If alasan = "" Then
            Return
        End If


            Dim sql As String = String.Format("update tradm_gud set sbatal=1,alasan_batal='{0}' where nobukti='{1}'", alasan.ToUpper, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString)

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

                '1. update faktur to / bila dari to
                If dv1(Me.BindingContext(bs1).Position)("jenistrans").ToString.Equals("TR PENJ TO") Then

                    Dim sqlup_rekap As String = String.Format("update trrekap_to set smuat=0 where nobukti='{0}'", dv1(Me.BindingContext(bs1).Position)("nobukti_trans").ToString)
                    Using cmdup_rekap As New OleDbCommand(sqlup_rekap, cn, sqltrans)
                        cmdup_rekap.ExecuteNonQuery()
                    End Using

                ElseIf dv1(Me.BindingContext(bs1).Position)("jenistrans").ToString.Equals("TR KANVAS") Then

                    Dim sqlup_spm As String = String.Format("update trspm set smuat=0 where nobukti='{0}'", dv1(Me.BindingContext(bs1).Position)("nobukti_trans").ToString)
                    Using cmdup_rekap As New OleDbCommand(sqlup_spm, cn, sqltrans)
                        cmdup_rekap.ExecuteNonQuery()
                    End Using

                End If

                If hapus_detail(cn, sqltrans) = True Then
                    Clsmy.InsertToLog(cn, "btadm_g", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString, dv1(Me.BindingContext(bs1).Position)("tanggal").ToString, sqltrans)

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

    Private Function hapus_detail(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As Boolean

        Dim hasil As Boolean = True

        Dim nobukti As String = dv1(Me.BindingContext(bs1).Position)("nobukti").ToString
        Dim nobukti2 As String = dv1(Me.BindingContext(bs1).Position)("nobukti_trans").ToString
        Dim nopol As String = dv1(Me.BindingContext(bs1).Position)("nopol").ToString
        Dim tanggal As String = dv1(Me.BindingContext(bs1).Position)("tanggal").ToString
        Dim tglmuat As String = dv1(Me.BindingContext(bs1).Position)("tglmuat").ToString
        Dim kdgud As String = dv1(Me.BindingContext(bs1).Position)("kd_gudang").ToString
        Dim kdgud2 As String = dv1(Me.BindingContext(bs1).Position)("kd_gudang2").ToString
        Dim jenistrans As String = dv1(Me.BindingContext(bs1).Position)("jenistrans").ToString
        Dim kdsupir As String = dv1(Me.BindingContext(bs1).Position)("kd_supir").ToString

        Dim sql As String = String.Format("select * from tradm_gud2 where nobukti='{0}'", nobukti)

        Dim comd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = comd.ExecuteReader

        If drd.HasRows Then
            While drd.Read

                Dim qtyinkecil As Integer = Integer.Parse(drd("qtyinkecil").ToString)
                Dim qtyoutkecil As Integer = Integer.Parse(drd("qtyoutkecil").ToString)

                Dim qtyinkecil_bad As Integer = Integer.Parse(drd("qtyinkecil_bad").ToString)

                Dim kdbar As String = drd("kd_barang").ToString

                If IsNumeric(drd("noid").ToString) Then

                    '2. update barang

                    'If qtyinkecil_bad > 0 Then
                    '    Dim hasilplusmin As String = PlusMin_Barang_Fsk(cn, sqltrans, qtyinkecil_bad, kdbar, kdgud2, False, False, False)
                    '    If Not hasilplusmin.Equals("ok") Then
                    '        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                    '        hasil = False
                    '        close_wait()
                    '        Exit While
                    '    End If

                    '    ''3. insert to hist stok
                    '    Clsmy.Insert_HistBarang_Fsk(cn, sqltrans, nobukti, nobukti2, nopol, tglmuat, kdgud2, kdbar, 0, qtyinkecil_bad, jenistrans)

                    'End If

                    If qtyinkecil > 0 Then
                        Dim hasilplusmin As String = PlusMin_Barang_Fsk(cn, sqltrans, qtyinkecil, kdbar, kdgud, False, False, False)
                        If Not hasilplusmin.Equals("ok") Then

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                            hasil = False
                            close_wait()
                            Exit While
                        End If

                        ''3. insert to hist stok
                        Clsmy.Insert_HistBarang_Fsk(cn, sqltrans, nobukti, nobukti2, nopol, tglmuat, kdgud, kdbar, 0, qtyinkecil, jenistrans, kdsupir)

                    End If

                    If qtyoutkecil > 0 Then
                        Dim hasilplusmin As String = PlusMin_Barang_Fsk(cn, sqltrans, qtyoutkecil, kdbar, kdgud, True, False, False)
                        If Not hasilplusmin.Equals("ok") Then

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                            hasil = False
                            close_wait()
                            Exit While
                        End If

                        'Dim hasilplusmin2 As String = PlusMin_Barang_Kend(cn, sqltrans, qtyoutkecil, kdbar, kdgud, False, False, False)
                        'If Not hasilplusmin2.Equals("ok") Then
                        '    MsgBox(hasilplusmin2, vbOKOnly + vbExclamation, "Informasi")
                        '    hasil = False
                        '    close_wait()
                        '    Exit While
                        'End If

                        ''3. insert to hist stok
                        Clsmy.Insert_HistBarang_Fsk(cn, sqltrans, nobukti, nobukti2, nopol, tglmuat, kdgud, kdbar, qtyoutkecil, 0, jenistrans, kdsupir)

                    End If


                End If

            End While
        End If

        drd.Close()

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

    End Sub

    Private Function cek_sambil() As Boolean

        Dim hasil As Boolean = False
        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select sambil,sbatal from tradm_gud where nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If drd(0).ToString.Equals("") Then
                    hasil = False
                Else

                    dv1(bs1.Position)("sbatal") = drd(1).ToString

                    If drd(0) = 1 Then
                        hasil = True
                    Else
                        hasil = False
                    End If

                End If
            Else
                hasil = False
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

        If cek_sambil() = True Then
            MsgBox("Data telah digunakan transaksi lainnya...", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

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

        statmanipulate = True
        Using fkar2 As New fadm_gud2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0}
            fkar2.ShowDialog()
            statmanipulate = False
        End Using
    End Sub

    Private Sub tsedit_Click(sender As System.Object, e As System.EventArgs) Handles tsedit.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If


        If cek_sambil() = True Then
            MsgBox("Data telah digunakan transaksi lainnya...", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Faktur telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        statmanipulate = True
        Using fkar2 As New fadm_gud2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.ShowDialog()
            statmanipulate = False
        End Using

    End Sub

    Private Sub tsview_Click(sender As System.Object, e As System.EventArgs) Handles tsview.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        statmanipulate = True
        Using fkar2 As New fadm_gud2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.btsimpan.Enabled = False
            fkar2.btadd.Enabled = False
            fkar2.btdel.Enabled = False
            fkar2.tbukti_tr.Enabled = False
            fkar2.ShowDialog()
            statmanipulate = False
        End Using

    End Sub

    Private Sub GridView1_Click(sender As Object, e As System.EventArgs) Handles GridView1.Click

        If statmanipulate Then
            Return
        End If

        opendata_br()

    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged

        If statmanipulate Then
            Return
        End If

        opendata_br()
    End Sub

    Private Sub GridView1_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles GridView1.RowCellClick

        If statmanipulate Then
            Return
        End If

        opendata_br()
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick

        If statmanipulate Then
            Return
        End If

        opendata_br()
    End Sub


End Class