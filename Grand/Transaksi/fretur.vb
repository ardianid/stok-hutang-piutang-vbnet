Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy
Imports DevExpress.XtraReports.UI

Public Class fretur

    Private bs1 As BindingSource
    Private ds As DataSet
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private Sub opendata()

        Dim sql As String = String.Format("SELECT     trretur.nobukti, trretur.tanggal, trretur.tgl_masuk, ms_toko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, trretur.alasan_retur, " & _
            "trretur.netto, trretur.sbatal,trretur.spotong,trretur.nopol,ms_pegawai.nama_karyawan as nama_supir,ms_pegawai.kd_karyawan " & _
            "FROM         trretur LEFT OUTER JOIN " & _
            "ms_toko ON trretur.kd_toko = ms_toko.kd_toko LEFT OUTER JOIN " & _
            "ms_pegawai ON trretur.kd_supir = ms_pegawai.kd_karyawan where trretur.tanggal >='{0}' and  trretur.tanggal <='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))


        Dim cn As OleDbConnection = Nothing

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
        Dim cn As OleDbConnection = Nothing

        Dim sql As String = ""
        Dim scbo As Integer = tcbofind.SelectedIndex

        sql = String.Format("SELECT     trretur.nobukti, trretur.tanggal, trretur.tgl_masuk, ms_toko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, trretur.alasan_retur, " & _
            "trretur.netto, trretur.sbatal,trretur.spotong,trretur.nopol,ms_pegawai.nama_karyawan as nama_supir,ms_pegawai.kd_karyawan " & _
            "FROM         trretur LEFT OUTER JOIN " & _
            "ms_toko ON trretur.kd_toko = ms_toko.kd_toko LEFT OUTER JOIN " & _
            "ms_pegawai ON trretur.kd_supir = ms_pegawai.kd_karyawan where trretur.tanggal >='{0}' and  trretur.tanggal <='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))

        Select Case tcbofind.SelectedIndex
            Case 0 ' nobukti
                sql = String.Format("{0} and trretur.nobukti like '%{1}%'", sql, tfind.Text.Trim)
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

                sql = String.Format("{0} and trretur.tanggal='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))
            Case 2

                If Not IsDate(tfind.Text.Trim) Then
                    MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                    Return
                Else

                    If tfind.Text.Trim.Length < 10 Or tfind.Text.Trim.Length > 10 Then
                        MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                        Return
                    End If

                End If

                sql = String.Format("{0} and trretur.tgl_masuk='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))

            Case 3 ' outlet
                sql = String.Format("{0} and ms_toko.nama_toko like '%{1}%'", sql, tfind.Text.Trim)
            Case 4
                sql = String.Format("{0} and trretur.nopol like '%{1}%'", sql, tfind.Text.Trim)
            Case 5
                sql = String.Format("{0} and ms_pegawai.nama_karyawan like '%{1}%'", sql, tfind.Text.Trim)
        End Select

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
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

        Dim sql As String = String.Format("update trretur set sbatal=1,alasan_batal='{0}' where nobukti='{1}'", alasan.ToUpper, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString)
        Dim sqltoko As String = String.Format("update ms_toko set jumlahretur=jumlahretur - {0} where kd_toko='{1}'", dv1(bs1.Position)("netto").ToString, dv1(bs1.Position)("kd_toko").ToString)

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

            cmdtoko = New OleDbCommand(sqltoko, cn, sqltrans)
            cmdtoko.ExecuteNonQuery()

            If hapus_detail(cn, sqltrans) = True Then
                Clsmy.InsertToLog(cn, "btretur", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString, dv1(Me.BindingContext(bs1).Position)("tanggal").ToString, sqltrans)

                sqltrans.Commit()

                dv1(bs1.Position)("sbatal") = 1

                close_wait()

                MsgBox("Data telah dibatalkan...", vbOKOnly + vbInformation, "Informasi")
            End If

            'opendata()

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
        Dim tanggal As String = dv1(Me.BindingContext(bs1).Position)("tgl_masuk").ToString
        Dim kdtoko As String = dv1(Me.BindingContext(bs1).Position)("kd_toko").ToString
        Dim kdsupir As String = dv1(Me.BindingContext(bs1).Position)("kd_karyawan").ToString
        Dim nopol As String = dv1(Me.BindingContext(bs1).Position)("nopol").ToString

        Dim sql As String = String.Format("select * from trretur2 where nobukti='{0}'", nobukti)

        Dim comd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = comd.ExecuteReader

        If drd.HasRows Then
            While drd.Read

                Dim qtykecil As Integer = Integer.Parse(drd("qtykecil").ToString)
                Dim kdbar As String = drd("kd_barang").ToString
                Dim kdgud As String = drd("kd_gudang").ToString
                Dim jenisret As String = drd("jenis_ret").ToString

                Dim sqlc As String = String.Format("select qty1,qty2,qty3 from  ms_barang  where kd_barang='{0}'", kdbar)
                Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
                Dim drc As OleDbDataReader = cmdc.ExecuteReader

                If drc.Read Then
                    If IsNumeric(drc("qty1").ToString) Then

                        If jenisret.Equals("RETUR") Then

                            Dim simpankosonghh As String = simpankosong_f(cn, sqltrans, kdbar, kdtoko, drc("qty1").ToString, drc("qty2").ToString, drc("qty3").ToString, qtykecil, True)
                            If Not simpankosonghh.Equals("ok") Then

                                If Not IsNothing(sqltrans) Then
                                    sqltrans.Rollback()
                                End If

                                MsgBox(simpankosonghh, vbOKOnly + vbExclamation, "Informasi")
                                hasil = False
                                Exit While

                            Else
                                '3. insert to hist stok
                                Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tanggal, "None B", kdbar, qtykecil, 0, "Retur (Batal)", kdsupir, nopol)

                            End If

                        Else

                            Dim hsilpinjm As String = Hist_PinjamSewa_Toko(kdtoko, kdbar, qtykecil, cn, sqltrans, True, True)
                            If Not hsilpinjm.Equals("ok") Then
                                close_wait()

                                If Not IsNothing(sqltrans) Then
                                    sqltrans.Rollback()
                                End If

                                MsgBox(hsilpinjm, vbOKOnly + vbExclamation, "Informasi")
                                hasil = False
                                Exit While
                            End If

                            Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tanggal, "None P", kdbar, qtykecil, 0, "Retur (Batal)", kdsupir, nopol)


                        End If

                        

                    End If
                End If
                drc.Close()

                If IsNumeric(drd("noid").ToString) Then

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, False, False, False)
                    If Not hasilplusmin.Equals("ok") Then

                        close_wait()
                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        hasil = False
                        Exit While
                    End If

                    '3. insert to hist stok
                    Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tanggal, kdgud, kdbar, 0, qtykecil, "Retur Barang", kdsupir, nopol)

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

            Dim sql As String = String.Format("select sbatal,spotong from trretur where nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd(0).ToString) Then
                    dv1(bs1.Position)("sbatal") = drd(0).ToString
                    dv1(bs1.Position)("spotong") = drd(1).ToString
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

        If dv1(bs1.Position)("spotong").ToString.Equals("1") Then
            MsgBox("Retur sudah dilakukan pemotongan..", vbOKOnly + vbExclamation, "Informasi")
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

        Dim stprint As Boolean
        If tsprint.Enabled = True Then
            stprint = True
        Else
            stprint = False
        End If

        Using fkar2 As New fretur2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0, .statprint = stprint}
            fkar2.ShowDialog()
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

        If dv1(bs1.Position)("spotong").ToString.Equals("1") Then
            MsgBox("Retur sudah dilakukan pemotongan..", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Faktur telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        Dim stprint As Boolean
        If tsprint.Enabled = True Then
            stprint = True
        Else
            stprint = False
        End If

        Using fkar2 As New fretur2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position, .statprint = stprint}
            fkar2.ShowDialog()
        End Using

    End Sub

    Private Sub tsview_Click(sender As System.Object, e As System.EventArgs) Handles tsview.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        Using fkar2 As New fretur2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.btsimpan.Enabled = False
            fkar2.btadd.Enabled = False
            fkar2.btedit.Enabled = False
            fkar2.btdel.Enabled = False
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
            MsgBox("Rekap telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        Dim nobukti As String = dv1(bs1.Position)("nobukti").ToString

        Using fkar2 As New fpr_retur With {.nobukti = nobukti}
            fkar2.ShowDialog(Me)
        End Using

    End Sub

    Private Sub tsprint2_Click(sender As System.Object, e As System.EventArgs) Handles tsprint2.Click

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = String.Format("SELECT     trretur.nobukti, trretur.tanggal, trretur.tgl_masuk, ms_toko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, trretur.alasan_retur, trretur.brutto, " & _
                      "trretur.disc_rp AS disc, trretur.netto, ms_barang.kd_barang, ms_barang.nama_barang, trretur2.qty, trretur2.satuan, trretur2.harga, trretur2.disc_rp, trretur2.jumlah,trretur.nopol,ms_pegawai.nama_karyawan as nama_supir,trretur.no_nota " & _
                     "FROM         trretur INNER JOIN " & _
                      "trretur2 ON trretur.nobukti = trretur2.nobukti INNER JOIN " & _
                      "ms_toko ON trretur.kd_toko = ms_toko.kd_toko INNER JOIN " & _
                      "ms_barang ON trretur2.kd_barang = ms_barang.kd_barang LEFT OUTER JOIN " & _
                      "ms_pegawai on trretur.kd_supir=ms_pegawai.kd_karyawan " & _
                      "where trretur.sbatal=0 and trretur.nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)

            Dim ds As DataSet = New dsretur
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_returbar() With {.DataSource = ds.Tables(0)}
            rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
            rrekap.DataMember = rrekap.DataMember
            rrekap.PrinterName = varprinter1
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

End Class