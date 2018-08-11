Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fbeli_cust

    Private bs1 As BindingSource
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private addstat As Boolean = False

    Private Sub opendata()

        Dim sql As String = String.Format("SELECT     tr_belicust.nobukti, tr_belicust.tanggal, tr_belicust.tanggal_masuk, ms_pegawai.kd_karyawan, " & _
        "ms_pegawai.nama_karyawan, ms_toko.kd_toko, ms_toko.nama_toko, " & _
        "ms_toko.alamat_toko, tr_belicust.total, tr_belicust.sbatal,tr_belicust.jenis_trans " & _
        "FROM tr_belicust INNER JOIN " & _
        "ms_toko ON tr_belicust.kd_toko = ms_toko.kd_toko INNER JOIN " & _
        "ms_pegawai ON tr_belicust.kd_sales = ms_pegawai.kd_karyawan " & _
        "where tr_belicust.tanggal>='{0}' and tr_belicust.tanggal<='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))


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

    End Sub

    Private Sub cari()

        'bs1.DataSource = Nothing
        grid1.DataSource = Nothing

        Dim sql As String = ""
        Dim scbo As Integer = tcbofind.SelectedIndex

        sql = String.Format("SELECT     tr_belicust.nobukti, tr_belicust.tanggal, tr_belicust.tanggal_masuk, ms_pegawai.kd_karyawan, " & _
        "ms_pegawai.nama_karyawan, ms_toko.kd_toko, ms_toko.nama_toko, " & _
        "ms_toko.alamat_toko, tr_belicust.total, tr_belicust.sbatal,tr_belicust.jenis_trans " & _
        "FROM tr_belicust INNER JOIN " & _
        "ms_toko ON tr_belicust.kd_toko = ms_toko.kd_toko INNER JOIN " & _
        "ms_pegawai ON tr_belicust.kd_sales = ms_pegawai.kd_karyawan " & _
        "where tr_belicust.tanggal>='{0}' and tr_belicust.tanggal<='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))

        Select Case tcbofind.SelectedIndex
            Case 0 ' nobukti
                sql = String.Format("{0} and tr_belicust.nobukti like '%{1}%'", sql, tfind.Text.Trim)
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

                sql = String.Format("{0} and tr_belicust.tanggal='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))
            Case 2 ' supp
                sql = String.Format("{0} and ms_toko.nama_toko like '%{1}%'", sql, tfind.Text.Trim)
            Case 3 ' nopol
                sql = String.Format("{0} and ms_pegawai.nama_karyawan like '%{1}%'", sql, tfind.Text.Trim)
        End Select

        Dim cn As OleDbConnection = Nothing

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

        Dim sql As String = String.Format("update tr_belicust set sbatal=1,alasan_batal='{0}' where nobukti='{1}'", alasan.ToUpper, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString)

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction


            If hapus_detail(cn, sqltrans) = True Then


                Using comd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                    comd.ExecuteNonQuery()
                End Using

                Clsmy.InsertToLog(cn, "btbeli_cust", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString, dv1(Me.BindingContext(bs1).Position)("tanggal").ToString, sqltrans)

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

    Private Function cek_nopolspm(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal nobukti As String) As String

        Dim hasil As String = ""

        Dim sqlspm As String = String.Format("select trspm.nopol from tr_belicust3 inner join trspm on tr_belicust3.nobukti_spm=trspm.nobukti " & _
                                            "where tr_belicust3.nobukti='{0}'", nobukti)
        Dim cmdspm As OleDbCommand = New OleDbCommand(sqlspm, cn, sqltrans)
        Dim drdspm As OleDbDataReader = cmdspm.ExecuteReader

        If drdspm.Read Then
            If Not drdspm(0).ToString.Equals("") Then
                hasil = drdspm(0).ToString
            End If
        End If
        drdspm.Close()

        Return hasil

    End Function

    Private Function cek_nopolfak(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal tglmasuk As String, ByVal kdsales As String) As String

        Dim hasil As String = ""

        Dim sqlkir As String = String.Format("select trrekap_to.nopol from trrekap_to inner join trrekap_to2 on trrekap_to.nobukti=trrekap_to2.nobukti " & _
        "inner join trfaktur_to on trrekap_to2.nobukti_fak=trfaktur_to.nobukti " & _
        "inner join trfaktur_balik on trfaktur_balik.nobukti_rkp=trrekap_to.nobukti " & _
        "where trfaktur_balik.tanggal='{0}' and trfaktur_to.kd_karyawan='{1}'", convert_date_to_eng(tglmasuk), kdsales)

        Dim cmdkir As OleDbCommand = New OleDbCommand(sqlkir, cn, sqltrans)
        Dim drdkir As OleDbDataReader = cmdkir.ExecuteReader

        If drdkir.Read Then
            If Not drdkir(0).ToString.Equals("") Then
                hasil = drdkir(0).ToString
            End If
        End If
        drdkir.Close()

        Return hasil

    End Function

    Private Function hapus_detail(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As Boolean

        Dim hasil = True

        Dim nobukti As String = dv1(Me.BindingContext(bs1).Position)("nobukti").ToString
        Dim tanggal As String = dv1(Me.BindingContext(bs1).Position)("tanggal").ToString
        Dim tanggal_masuk As String = dv1(Me.BindingContext(bs1).Position)("tanggal_masuk").ToString
        Dim kdsales As String = dv1(Me.BindingContext(bs1).Position)("kd_karyawan").ToString
        Dim jenistrans As String = dv1(Me.BindingContext(bs1).Position)("jenis_trans").ToString
        Dim kdtoko As String = dv1(Me.BindingContext(bs1).Position)("kd_toko").ToString

        Dim nopol As String = ""
        If jenistrans.Equals("TO") Then
            nopol = cek_nopolfak(cn, sqltrans, tanggal_masuk, kdsales)
        Else
            nopol = cek_nopolspm(cn, sqltrans, nobukti)
        End If

        Dim sql As String = String.Format("select * from tr_belicust2 where nobukti='{0}'", nobukti)

        Dim comd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd2 As OleDbDataReader = comd.ExecuteReader

        comd.Dispose()

        '   If drd.HasRows Then
        While drd2.Read

            Dim qtykecil As Integer = Integer.Parse(drd2("qtykecil").ToString)
            Dim kdbar As String = drd2("kd_barang").ToString
            Dim kdgud As String = drd2("kd_gudang").ToString

            If IsNumeric(drd2("noid").ToString) Then

                If apakah_brg_kembali(cn, sqltrans, kdbar) = True Then

                    Dim sql2 As String = String.Format("select qty1,qty2,qty3 from ms_barang where kd_barang='{0}'", kdbar)
                    Dim cmdc As OleDbCommand = New OleDbCommand(sql2, cn, sqltrans)
                    Dim drc As OleDbDataReader = cmdc.ExecuteReader

                    If drc.Read Then

                        If IsNumeric(drc(0).ToString) Then

                            Dim simpankosong_fh As String = simpankosong_f(cn, sqltrans, kdbar, kdtoko, drc("qty1").ToString, drc("qty2").ToString, drc("qty3").ToString, qtykecil, True)

                            If Not simpankosong_fh.Equals("ok") Then
                                close_wait()
                                If Not IsNothing(sqltrans) Then
                                    sqltrans.Rollback()
                                End If

                                MsgBox(simpankosong_fh)
                                hasil = False
                            Else
                                Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tanggal_masuk, "None", kdbar, qtykecil, 0, "Beli Cust (Btl)", kdsales, nopol)
                            End If

                        End If

                    End If
                    drc.Close()

                End If


                If jenistrans.Equals("TO") Then

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, False, False, False)
                    If Not hasilplusmin.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        hasil = False
                    End If

                    '3. insert to hist stok
                    Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tanggal_masuk, kdgud, kdbar, 0, qtykecil, "Beli Cust (Btl)", kdsales, nopol)

                End If

                If Not (jenistrans.Equals("TO")) Then

                    If apakah_brg_kembali(cn, sqltrans, kdbar) = False Then

                        '2. update barang
                        Dim hasilplusmin As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, kdgud, True, False, False)
                        If Not hasilplusmin.Equals("ok") Then
                            close_wait()

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                            hasil = False
                        End If

                        '3. insert to hist stok
                        Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tanggal_masuk, kdgud, kdbar, qtykecil, 0, "Beli Cust (Btl)", kdsales, nopol)

                    End If

                End If

            End If

        End While
        '  End If

        drd2.Close()

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

            Dim sql As String = String.Format("select sbatal,jmlbayar from trbeli where nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd(0).ToString) Then
                    dv1(bs1.Position)("sbatal") = drd(0).ToString
                    dv1(bs1.Position)("jmlbayar") = Double.Parse(drd(1).ToString)
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

        cekbatal_onserver()

        If Double.Parse(dv1(bs1.Position)("total")) > 0 Then

            'If Double.Parse(dv1(bs1.Position)("jmlbayar")) >= Double.Parse(dv1(bs1.Position)("netto")) Then
            '    MsgBox("Nota sudah lunas...", vbOKOnly + vbExclamation, "Informasi")
            '    Return
            'End If

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

        addstat = True
        Using fkar2 As New fbeli_cust2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0}
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

        If Double.Parse(dv1(bs1.Position)("total")) > 0 Then

            'If Double.Parse(dv1(bs1.Position)("jmlbayar")) >= Double.Parse(dv1(bs1.Position)("netto")) Then
            '    MsgBox("Nota sudah lunas...", vbOKOnly + vbExclamation, "Informasi")
            '    Return
            'End If

        End If

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Faktur telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        addstat = True
        Using fkar2 As New fbeli_cust2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
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

        Using fkar2 As New fbeli_cust2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.btsimpan.Enabled = False
            fkar2.btadd.Enabled = False
            ' fkar2.btedit.Enabled = False
            fkar2.btdel.Enabled = False
            fkar2.ShowDialog()
        End Using

    End Sub

End Class