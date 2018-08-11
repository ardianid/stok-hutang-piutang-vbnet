Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI

Public Class ffaktur_to

    Private bs1 As BindingSource
    Private ds As DataSet
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private nopolrek As String

    Private Sub opendata()

        Dim sql As String = String.Format("SELECT     trfaktur_to.nobukti, trfaktur_to.tanggal, trfaktur_to.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, trfaktur_to.kd_karyawan, ms_pegawai.nama_karyawan," & _
            "trfaktur_to.ket, trfaktur_to.netto, trfaktur_to.skirim, trfaktur_to.spulang, trfaktur_to.sbatal,trfaktur_to.statkirim,trfaktur_to.jmlprint " & _
            "FROM         trfaktur_to INNER JOIN " & _
            "ms_pegawai ON trfaktur_to.kd_karyawan = ms_pegawai.kd_karyawan INNER JOIN " & _
            "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko where trfaktur_to.jnis_fak='T' and trfaktur_to.tanggal >='{0}' and  trfaktur_to.tanggal <='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))


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

        sql = String.Format("SELECT     trfaktur_to.nobukti, trfaktur_to.tanggal, trfaktur_to.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, trfaktur_to.kd_karyawan, ms_pegawai.nama_karyawan," & _
            "trfaktur_to.ket, trfaktur_to.netto, trfaktur_to.skirim, trfaktur_to.spulang, trfaktur_to.sbatal,trfaktur_to.statkirim,trfaktur_to.jmlprint " & _
            "FROM         trfaktur_to INNER JOIN " & _
            "ms_pegawai ON trfaktur_to.kd_karyawan = ms_pegawai.kd_karyawan INNER JOIN " & _
            "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko where trfaktur_to.jnis_fak='T'and trfaktur_to.tanggal >='{0}' and  trfaktur_to.tanggal <='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))

        Select Case tcbofind.SelectedIndex
            Case 0 ' kode
                sql = String.Format("{0} and trfaktur_to.nobukti like '%{1}%'", sql, tfind.Text.Trim)
            Case 1
                sql = String.Format("{0} and trfaktur_to.no_nota like '%{1}%'", sql, tfind.Text.Trim)
            Case 2 ' nama

                If Not IsDate(tfind.Text.Trim) Then
                    MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                    Return
                Else

                    If tfind.Text.Trim.Length < 10 Or tfind.Text.Trim.Length > 10 Then
                        MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                        Return
                    End If

                End If

                sql = String.Format("{0} and trfaktur_to.tanggal='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))
            Case 3 ' sales
                sql = String.Format("{0} and ms_pegawai.nama_karyawan like '%{1}%'", sql, tfind.Text.Trim)
            Case 4 ' outlet
                sql = String.Format("{0} and ms_toko.nama_toko like '%{1}%'", sql, tfind.Text.Trim)
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

        Dim sql As String = String.Format("update trfaktur_to set sbatal=1,alasan_batal='{0}' where nobukti='{1}'", alasan.ToUpper, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString)
        'Dim sqltoko As String = String.Format("update ms_toko set piutangbeli=piutangbeli - {0} where kd_toko='{1}'", Replace(dv1(bs1.Position)("netto").ToString, ",", "."), dv1(bs1.Position)("kd_toko").ToString)

        Dim sqluptoko21 As String = String.Format("update ms_toko2 set jmlpiutang=jmlpiutang - {0} where kd_toko='{1}' and kd_karyawan='{2}'", Replace(dv1(bs1.Position)("netto").ToString, ",", "."), dv1(bs1.Position)("kd_toko").ToString, dv1(bs1.Position)("kd_karyawan").ToString)

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

            'cmdtoko = New OleDbCommand(sqltoko, cn, sqltrans)
            'cmdtoko.ExecuteNonQuery()

            Using cmdtoko2 As OleDbCommand = New OleDbCommand(sqluptoko21, cn, sqltrans)
                cmdtoko2.ExecuteNonQuery()
            End Using

            If hapus_detail(cn, sqltrans) = True Then

                If hapus_detail3(cn, sqltrans) = False Then
                    GoTo langsung_out
                End If


                Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, dv1(bs1.Position)("nobukti").ToString, dv1(bs1.Position)("nobukti").ToString, dv1(bs1.Position)("tanggal").ToString, dv1(bs1.Position)("kd_toko").ToString, 0, Replace(dv1(bs1.Position)("netto").ToString, ",", "."), "Jual (Batal)")

                Clsmy.InsertToLog(cn, "btfaktur_to", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString, dv1(Me.BindingContext(bs1).Position)("tanggal").ToString, sqltrans)

                sqltrans.Commit()
1:
                dv1(bs1.Position)("sbatal") = 1

                close_wait()

                MsgBox("Data telah dibatalkan...", vbOKOnly + vbInformation, "Informasi")

            End If

langsung_out:

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

    Private Function ceksupir(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal nobukti As String) As String

        nopolrek = ""

        Dim sql As String = String.Format("select trrekap_to.kd_supir,trrekap_to.nopol from trfaktur_balik2 inner join trfaktur_balik on trfaktur_balik2.nobukti=trfaktur_balik.nobukti " & _
            "inner join trfaktur_to on trfaktur_to.nobukti=trfaktur_balik2.nobukti_fak " & _
            "inner join trrekap_to on trfaktur_balik.nobukti_rkp=trrekap_to.nobukti where trfaktur_balik.sbatal=0 and trfaktur_to.nobukti='{0}'", nobukti)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim kode As String = "-"

        If drd.Read Then
            If Not drd(0).ToString.Equals("") Then
                kode = drd(0).ToString
                nopolrek = drd(1).ToString
            End If
        End If
        drd.Close()

        Return kode

    End Function


    Private Function hapus_detail(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As Boolean

        Dim hasil As Boolean = True

        If dv1(bs1.Position)("spulang").ToString.Equals("0") Then
            hasil = True
            GoTo langsung_keluar
        End If

        Dim nobukti As String = dv1(Me.BindingContext(bs1).Position)("nobukti").ToString
        Dim tanggal As String = dv1(Me.BindingContext(bs1).Position)("tanggal").ToString

        Dim kdsup As String = ceksupir(cn, sqltrans, nobukti)

        Dim sql As String = String.Format("select * from trfaktur_to2 inner join ms_barang on trfaktur_to2.kd_barang=ms_barang.kd_barang where trfaktur_to2.nobukti='{0}'", nobukti)

        Dim comd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = comd.ExecuteReader

        If drd.HasRows Then
            While drd.Read

                Dim qtykecil As Integer = Integer.Parse(drd("qtykecil").ToString)
                Dim kdbar As String = drd("kd_barang").ToString
                Dim kdgud As String = drd("kd_gudang").ToString
                Dim jenis As String = drd("jenis").ToString

                If IsNumeric(drd("noid").ToString) Then

                    If jenis = "FISIK" Then
                        '2. update barang
                        Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, True, False, False)
                        If Not hasilplusmin.Equals("ok") Then

                            close_wait()

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                            hasil = False

                            Exit While
                        End If

                        ''3. insert to hist stok
                        Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tanggal, kdgud, kdbar, qtykecil, 0, "Jual TO (Batal)", kdsup, nopolrek)
                    End If

                    

                End If

            End While
        End If

        drd.Close()

langsung_keluar:

        Return hasil

    End Function

    Private Function hapus_detail3(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As Boolean

        Dim hasil As Boolean = True

        If dv1(bs1.Position)("spulang").ToString.Equals("0") Then
            hasil = True
            GoTo langsung_keluar
        End If

        Dim nobukti As String = dv1(Me.BindingContext(bs1).Position)("nobukti").ToString
        Dim tanggal As String = dv1(Me.BindingContext(bs1).Position)("tanggal").ToString

        Dim kdsup As String = ceksupir(cn, sqltrans, nobukti)

        Dim sql As String = String.Format("select * from trfaktur_to3 inner join ms_barang on trfaktur_to3.kd_barang=ms_barang.kd_barang where trfaktur_to3.nobukti='{0}'", nobukti)

        Dim comd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = comd.ExecuteReader

        If drd.HasRows Then
            While drd.Read

                Dim qtykecil As Integer = Integer.Parse(drd("qtykecil").ToString)
                Dim kdbar As String = drd("kd_barang").ToString
                Dim kdgud As String = drd("kd_gudang").ToString
                Dim jenis As String = drd("jenis").ToString

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

                    ''3. insert to hist stok
                    Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tanggal, kdgud, kdbar, 0, qtykecil, "Jual TO (Batal)", kdsup, nopolrek)

                End If

            End While
        End If

        drd.Close()

langsung_keluar:

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
            tsprint3.Enabled = True
        Else
            tsprint.Enabled = False
            tsprint2.Enabled = False
            tsprint3.Enabled = False
        End If


    End Sub

    Private Function cek_SudahBayar(ByVal nobukti As String) As Boolean

        Dim hasil As Boolean = False
        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select jmlbayar,jmlgiro,jmlgiro_real,jmlkelebihan_pot from trfaktur_to where nobukti='{0}'", nobukti)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then

                Dim jmlbayar As String = drd(0).ToString
                Dim jmlgiro As String = drd(1).ToString
                Dim jmlgiro_real As String = drd(2).ToString
                Dim jmlkelebihan_pot As String = drd(3).ToString

                If jmlbayar = "" Then
                    jmlbayar = 0
                End If

                If jmlgiro = "" Then
                    jmlgiro = 0
                End If

                If jmlgiro_real = "" Then
                    jmlgiro_real = 0
                End If

                If jmlkelebihan_pot = "" Then
                    jmlkelebihan_pot = 0
                End If

                Dim total As Double = Double.Parse(jmlbayar) + Double.Parse(jmlgiro) + Double.Parse(jmlgiro_real) + Double.Parse(jmlkelebihan_pot)

                If total > 0 Then
                    hasil = True
                Else
                    hasil = False
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

    Private Sub cek_stat2(ByVal nobukti As String)

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select nobukti,skirim,spulang,sbatal,statkirim,jmlprint from trfaktur_to where nobukti='{0}'", nobukti)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Not drd(0).ToString.Equals("") Then
                    dv1(bs1.Position)("skirim") = drd("skirim").ToString
                    dv1(bs1.Position)("spulang") = drd("spulang").ToString
                    dv1(bs1.Position)("sbatal") = drd("sbatal").ToString
                    dv1(bs1.Position)("statkirim") = drd("statkirim").ToString
                    dv1(bs1.Position)("jmlprint") = drd("jmlprint").ToString
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

    Private Function cektoko_boleh() As Boolean

        Dim hasil = True
        If ins_alltokouser = 1 Then
            Return hasil
        End If


        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select id from ms_usersys4 where kd_toko='{0}' and nama_user='{1}'", dv1(bs1.Position)("kd_toko").ToString, userprog)
            Dim cmdc As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drdc As OleDbDataReader = cmdc.ExecuteReader

            If drdc.Read Then
                If IsNumeric(drdc(0).ToString) Then
                    If Integer.Parse(drdc(0).ToString) > 0 Then
                        hasil = True
                    Else
                        hasil = False
                    End If
                Else
                    hasil = False
                End If
            Else
                hasil = False
            End If
            drdc.Close()

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

        If cek_SudahBayar(dv1(bs1.Position)("nobukti").ToString) = True Then
            MsgBox("Faktur sudah dibayar", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        cek_stat2(dv1(bs1.Position)("nobukti").ToString)

        If dv1(bs1.Position)("statkirim").ToString.Equals("TERKIRIM") Then
            MsgBox("Faktur sudah terkirim", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If dv1(bs1.Position)("skirim").ToString.Equals("1") And dv1(bs1.Position)("spulang").ToString.Equals("0") Then
            MsgBox("Barang sedang dikirim...", vbOKOnly + vbExclamation, "Informasi")
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

        Using fkar2 As New ffaktur_to2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0, .jenisjual = "T", .spulang = False, .statprint = stprint}
            fkar2.SplitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
            fkar2.ShowDialog()
        End Using

        cek_stat2(dv1(bs1.Position)("nobukti").ToString)

    End Sub

    Private Sub tsedit_Click(sender As System.Object, e As System.EventArgs) Handles tsedit.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        If cek_SudahBayar(dv1(bs1.Position)("nobukti").ToString) = True Then
            MsgBox("Faktur sudah dibayar", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        cek_stat2(dv1(bs1.Position)("nobukti").ToString)

        If dv1(bs1.Position)("statkirim").ToString.Equals("TERKIRIM") Then
            MsgBox("Faktur sudah terkirim", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If dv1(bs1.Position)("skirim").ToString.Equals("1") And dv1(bs1.Position)("spulang").ToString.Equals("0") Then
            MsgBox("Barang sedang dikirim...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Faktur telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If cektoko_boleh() = False Then
            MsgBox("Anda tidak berhak merubah data di transaksi ini (bukan toko yang berhak diakses)", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        Dim stprint As Boolean
        If tsprint.Enabled = True Then
            stprint = True
        Else
            stprint = False
        End If

        If dv1(bs1.Position)("spulang").ToString.Equals("1") Then

            Using fkar2 As New ffaktur_to2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position, .jenisjual = "T", .spulang = False}

                If dv1(bs1.Position)("statkirim").ToString.Equals("TERKIRIM") Then
                    fkar2.tkd_toko.Enabled = False
                    fkar2.tkd_sales.Enabled = False
                    fkar2.bts_toko.Enabled = False
                    fkar2.bts_sal.Enabled = False
                    fkar2.ttgl.Enabled = False
                    fkar2.ttgl_tempo.Enabled = False
                    fkar2.cbjenis.Enabled = False

                    fkar2.btadd.Enabled = False
                    fkar2.btdel.Enabled = False

                    fkar2.tdisc_per.Properties.ReadOnly = True
                    fkar2.tdisc_rp.Properties.ReadOnly = True

                    fkar2.statprint = False

                    fkar2.SplitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both
                Else
                    fkar2.SplitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
                    fkar2.statprint = stprint
                End If

                fkar2.ShowDialog()
            End Using

        Else
            Using fkar2 As New ffaktur_to2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position, .jenisjual = "T", .spulang = False, .statprint = stprint}
                fkar2.SplitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
                fkar2.ShowDialog()
            End Using

            cek_stat2(dv1(bs1.Position)("nobukti").ToString)

        End If

    End Sub

    Private Sub tsview_Click(sender As System.Object, e As System.EventArgs) Handles tsview.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        If dv1(bs1.Position)("spulang").ToString.Equals("1") Then

            Using fkar2 As New ffaktur_to2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position, .jenisjual = "T", .spulang = True}
                fkar2.btsimpan.Enabled = False
                fkar2.btadd.Enabled = False
                fkar2.btedit.Enabled = False
                fkar2.btdel.Enabled = False
                fkar2.btdel_ret.Enabled = False

                If dv1(bs1.Position)("statkirim").ToString.Equals("TERKIRIM") Then
                    fkar2.SplitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both
                Else
                    fkar2.SplitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
                End If


                fkar2.ShowDialog()
            End Using

        Else


            Using fkar2 As New ffaktur_to2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position, .jenisjual = "T"}
                fkar2.btsimpan.Enabled = False
                fkar2.btadd.Enabled = False
                fkar2.btedit.Enabled = False
                fkar2.btdel.Enabled = False
                fkar2.btdel_ret.Enabled = False
                fkar2.SplitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
                fkar2.ShowDialog()
            End Using
        End If

       

    End Sub

    Private Sub tsprint_Click(sender As System.Object, e As System.EventArgs) Handles tsprint.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If


        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Faktur telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        

        Dim nobukti As String = dv1(bs1.Position)("nobukti").ToString

        Dim sql1 As String = String.Format("SELECT     trfaktur_to.nobukti, trfaktur_to.tanggal, trfaktur_to.tgl_tempo, ms_toko.kd_toko +' - ' + ms_toko.nama_toko as nama_toko, ms_toko.alamat_toko, ms_pegawai.nama_karyawan + case when len(ms_pegawai.notelp1)>0 then '('+ms_pegawai.notelp1+')' else '' end as nama_karyawan, trfaktur_to.jnis_jual, " & _
                     "trfaktur_to.brutto - trfaktur_to.jmlkembali AS brutto, trfaktur_to.disc_rp AS disc_tot, trfaktur_to.netto,trfaktur_to.ongkos_angkut, " & _
                     "case when LEN(ms_toko.notelp1)>0 then " & _
                    "ms_toko.notelp1 else '' end + " & _
                    "case when LEN(ms_toko.notelp2)>0 then " & _
                    "case when LEN(ms_toko.notelp1)>0 then " & _
                    "' , '+ms_toko.notelp2 else ms_toko.notelp2 end " & _
                    "else '' end + " & _
                    "case when LEN(ms_toko.notelp3) > 0 then " & _
                    "case when LEN(ms_toko.notelp1) > 0 or LEN(ms_toko.notelp2)> 0 then " & _
                    "' , ' + ms_toko.notelp3 else ms_toko.notelp3 end " & _
                    "else '' end as notelp_toko,trfaktur_to.ket " & _
                   "FROM         trfaktur_to INNER JOIN " & _
                     "ms_pegawai ON trfaktur_to.kd_karyawan = ms_pegawai.kd_karyawan INNER JOIN " & _
                     "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko " & _
                   "WHERE     (trfaktur_to.sbatal = 0) AND (trfaktur_to.nobukti = '{0}')", nobukti)

        Dim sql2 As String = String.Format("SELECT ms_barang.nama_barang, trfaktur_to3.qty, trfaktur_to3.satuan, trfaktur_to3.harga, trfaktur_to3.jumlah, trfaktur_to3.nobukti " & _
        "FROM   trfaktur_to3 INNER JOIN " & _
        "ms_barang ON trfaktur_to3.kd_barang = ms_barang.kd_barang WHERE trfaktur_to3.nobukti='{0}'", nobukti)

        Dim sql3 As String = "select kd_barang,case when kd_barang='G0003' then nama_barang+' (JUAL)' else nama_barang end as nama_barang,nohrus,0 as qty,'' as satuan,0 as harga,0 as disc,0 as jumlah from ms_barang where hrusfaktur=1"

        Dim sqldetail As String = String.Format("select * from trfaktur_to2 where nobukti='{0}'", nobukti)

        Dim sqlbonus As String = String.Format("select trfaktur_to.nobukti, trfaktur_to.tanggal, " & _
        "ms_toko.kd_toko +' - ' + ms_toko.nama_toko as nama_toko, ms_toko.alamat_toko,  " & _
        "ms_pegawai.nama_karyawan + case when len(ms_pegawai.notelp1)>0 then '('+ms_pegawai.notelp1+')' else '' end as nama_karyawan, " & _
        "case when LEN(ms_toko.notelp1)>0 then " & _
        "ms_toko.notelp1 else '' end + " & _
        "case when LEN(ms_toko.notelp2)>0 then " & _
        "case when LEN(ms_toko.notelp1)>0 then " & _
        "' , '+ms_toko.notelp2 else ms_toko.notelp2 end " & _
        "else '' end + " & _
        "case when LEN(ms_toko.notelp3) > 0 then " & _
        " case when LEN(ms_toko.notelp1) > 0 or LEN(ms_toko.notelp2)> 0 then " & _
        "' , ' + ms_toko.notelp3 else ms_toko.notelp3 end " & _
        "else '' end as notelp_toko, " & _
        "ms_barang.kd_barang, ms_barang.nama_lap as nama_barang, trfaktur_to2.qtykecil as jml " & _
        "from trfaktur_to inner join trfaktur_to2 on trfaktur_to.nobukti=trfaktur_to2.nobukti " & _
        "inner join ms_toko on trfaktur_to.kd_toko=ms_toko.kd_toko " & _
        "inner join ms_pegawai on trfaktur_to.kd_karyawan=ms_pegawai.kd_karyawan " & _
        "inner join ms_barang on trfaktur_to2.kd_barang=ms_barang.kd_barang " & _
        "where ms_barang.kd_barang in ('BN0002','BN0003') and trfaktur_to.sbatal=0 and trfaktur_to.nobukti='{0}'", nobukti)


        Using fkar2 As New fpr_invoice2 With {.sql1 = sql1, .sql2 = sql2, .sql3 = sql3, .sqlbonus = sqlbonus, .sqldetail = sqldetail, .nobukti = nobukti}
            fkar2.ShowDialog(Me)
        End Using

        cek_stat2(dv1(bs1.Position)("nobukti").ToString)

    End Sub


    Private Sub tsprint2_Click(sender As System.Object, e As System.EventArgs) Handles tsprint2.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If


        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Faktur telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If


        Dim nobukti As String = dv1(bs1.Position)("nobukti").ToString

        Dim sql1 As String = String.Format("SELECT     trfaktur_to.nobukti, trfaktur_to.tanggal, trfaktur_to.tgl_tempo, ms_toko.kd_toko +' - ' + ms_toko.nama_toko as nama_toko, ms_toko.alamat_toko, ms_pegawai.nama_karyawan + case when len(ms_pegawai.notelp1)>0 then '('+ms_pegawai.notelp1+')' else '' end as nama_karyawan, trfaktur_to.jnis_jual, " & _
                     "trfaktur_to.brutto - trfaktur_to.jmlkembali AS brutto, trfaktur_to.disc_rp AS disc_tot, trfaktur_to.netto,trfaktur_to.ongkos_angkut, " & _
                     "case when LEN(ms_toko.notelp1)>0 then " & _
                    "ms_toko.notelp1 else '' end + " & _
                    "case when LEN(ms_toko.notelp2)>0 then " & _
                    "case when LEN(ms_toko.notelp1)>0 then " & _
                    "' , '+ms_toko.notelp2 else ms_toko.notelp2 end " & _
                    "else '' end + " & _
                    "case when LEN(ms_toko.notelp3) > 0 then " & _
                    "case when LEN(ms_toko.notelp1) > 0 or LEN(ms_toko.notelp2)> 0 then " & _
                    "' , ' + ms_toko.notelp3 else ms_toko.notelp3 end " & _
                    "else '' end as notelp_toko,trfaktur_to.ket " & _
                   "FROM         trfaktur_to INNER JOIN " & _
                     "ms_pegawai ON trfaktur_to.kd_karyawan = ms_pegawai.kd_karyawan INNER JOIN " & _
                     "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko " & _
                   "WHERE     (trfaktur_to.sbatal = 0) AND (trfaktur_to.nobukti = '{0}')", nobukti)

        Dim sql2 As String = String.Format("SELECT ms_barang.nama_barang, trfaktur_to3.qty, trfaktur_to3.satuan, trfaktur_to3.harga, trfaktur_to3.jumlah, trfaktur_to3.nobukti " & _
        "FROM   trfaktur_to3 INNER JOIN " & _
        "ms_barang ON trfaktur_to3.kd_barang = ms_barang.kd_barang WHERE trfaktur_to3.nobukti='{0}'", nobukti)

        Dim sql3 As String = "select kd_barang,case when kd_barang='G0003' then nama_barang+' (JUAL)' else nama_barang end as nama_barang,nohrus,0 as qty,'' as satuan,0 as harga,0 as disc,0 as jumlah from ms_barang where hrusfaktur=1"

        Dim sqldetail As String = String.Format("select * from trfaktur_to2 where nobukti='{0}'", nobukti)


        Dim sqlbonus As String = String.Format("select trfaktur_to.nobukti, trfaktur_to.tanggal, " & _
        "ms_toko.kd_toko +' - ' + ms_toko.nama_toko as nama_toko, ms_toko.alamat_toko,  " & _
        "ms_pegawai.nama_karyawan + case when len(ms_pegawai.notelp1)>0 then '('+ms_pegawai.notelp1+')' else '' end as nama_karyawan, " & _
        "case when LEN(ms_toko.notelp1)>0 then " & _
        "ms_toko.notelp1 else '' end + " & _
        "case when LEN(ms_toko.notelp2)>0 then " & _
        "case when LEN(ms_toko.notelp1)>0 then " & _
        "' , '+ms_toko.notelp2 else ms_toko.notelp2 end " & _
        "else '' end + " & _
        "case when LEN(ms_toko.notelp3) > 0 then " & _
        " case when LEN(ms_toko.notelp1) > 0 or LEN(ms_toko.notelp2)> 0 then " & _
        "' , ' + ms_toko.notelp3 else ms_toko.notelp3 end " & _
        "else '' end as notelp_toko, " & _
        "ms_barang.kd_barang, ms_barang.nama_lap as nama_barang, trfaktur_to2.qtykecil as jml " & _
        "from trfaktur_to inner join trfaktur_to2 on trfaktur_to.nobukti=trfaktur_to2.nobukti " & _
        "inner join ms_toko on trfaktur_to.kd_toko=ms_toko.kd_toko " & _
        "inner join ms_pegawai on trfaktur_to.kd_karyawan=ms_pegawai.kd_karyawan " & _
        "inner join ms_barang on trfaktur_to2.kd_barang=ms_barang.kd_barang " & _
        "where ms_barang.kd_barang in ('BN0002','BN0003') and trfaktur_to.sbatal=0 and trfaktur_to.nobukti='{0}'", nobukti)


        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New dsinvoice
            ds = Clsmy.GetDataSet(sql1, cn)

            dsinvoice_ku = New DataSet
            dsinvoice_ku = ds

            Dim ds2 As DataSet = New dsinvoice2
            ds2 = Clsmy.GetDataSet(sql2, cn)

            Dim dsbonus As DataSet = New ds_bonus
            dsbonus = Clsmy.GetDataSet(sqlbonus, cn)

            Dim ds3 As DataSet = New dsbarang_to
            ds3 = Clsmy.GetDataSet(sql3, cn)

            Dim dsdetail As DataSet = New DataSet
            dsdetail = Clsmy.GetDataSet(sqldetail, cn)

            Dim dtdetail As DataTable = dsdetail.Tables(0)

            For i As Integer = 0 To dtdetail.Rows.Count - 1

                Dim kdbar As String = dtdetail(i)("kd_barang").ToString
                Dim qty As Integer = Integer.Parse(dtdetail(i)("qty").ToString)
                Dim satuan As String = dtdetail(i)("satuan").ToString
                Dim harga As Integer = Double.Parse(dtdetail(i)("harga").ToString)
                Dim disc As Integer = Double.Parse(dtdetail(i)("disc_rp").ToString)
                Dim jumlah As Integer = Double.Parse(dtdetail(i)("jumlah").ToString)

                For x As Integer = 0 To ds3.Tables(0).Rows.Count - 1

                    If ds3.Tables(0).Rows(x)("kd_barang").ToString.Equals(kdbar) Then

                        ds3.Tables(0).Rows(x)("qty") = qty
                        ds3.Tables(0).Rows(x)("satuan") = satuan
                        ds3.Tables(0).Rows(x)("harga") = harga
                        ds3.Tables(0).Rows(x)("disc") = disc
                        ds3.Tables(0).Rows(x)("jumlah") = jumlah

                        Exit For

                    End If

                Next

            Next



            Dim ops As New System.Drawing.Printing.PrinterSettings

            ' Dim rinvoice2 As New r_invoice2

            'rinvoice2.DataSource = ds2.Tables(0)
            'rinvoice2.DataMember = rinvoice2.DataMember
            '  rinvoice2.PrinterName = ops.PrinterName
            '  rinvoice2.CreateDocument(True)

            'PrintControl1.PrintingSystem = rinvoice2.PrintingSystem

            Dim rinvoice As New r_invoice() With {.DataSource = ds.Tables(0)}
            rinvoice.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
            rinvoice.DataMember = rinvoice.DataMember

            rinvoice.XrSubreport1.ReportSource = New r_invoice2
            rinvoice.XrSubreport1.ReportSource.DataSource = ds2.Tables(0)
            rinvoice.XrSubreport1.ReportSource.DataMember = rinvoice.XrSubreport1.ReportSource.DataMember

            rinvoice.XrSubreport2.ReportSource = New r_invoice3
            rinvoice.XrSubreport2.ReportSource.DataSource = ds3.Tables(0)
            rinvoice.XrSubreport2.ReportSource.DataMember = rinvoice.XrSubreport2.ReportSource.DataMember
            rinvoice.PrinterName = varprinter1
            rinvoice.CreateDocument()

            Dim jmljual As Integer = 0
            For i As Integer = 0 To dsbonus.Tables(0).Rows.Count - 1
                jmljual = jmljual + Integer.Parse(dsbonus.Tables(0).Rows(i)("jml").ToString)
            Next

            If jmljual > 0 Then
                Dim rbonus As New r_invoice_bns() With {.DataSource = dsbonus.Tables(0)}
                rbonus.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
                rbonus.DataMember = rinvoice.DataMember

                rbonus.CreateDocument()

                rinvoice.Pages.AddRange(rbonus.Pages)
                rinvoice.PrintingSystem.ContinuousPageNumbering = True

            End If

            rinvoice.Print()


        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

        cekjmlprint()

        cek_stat2(dv1(bs1.Position)("nobukti").ToString)

    End Sub

    Private Sub cekjmlprint()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqlupprint As String = String.Format("update trfaktur_to set jmlprint=jmlprint+1 where nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)
            Using cmdupprint As OleDbCommand = New OleDbCommand(sqlupprint, cn)
                cmdupprint.ExecuteNonQuery()
            End Using

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


    Private Sub tsprint3_Click(sender As System.Object, e As System.EventArgs) Handles tsprint3.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If


        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Faktur telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If


        Dim nobukti As String = dv1(bs1.Position)("nobukti").ToString

        Dim sql1 As String = String.Format("SELECT     trfaktur_to.nobukti, trfaktur_to.tanggal, trfaktur_to.tgl_tempo, ms_toko.kd_toko +' - ' + ms_toko.nama_toko as nama_toko, ms_toko.alamat_toko, ms_pegawai.nama_karyawan, trfaktur_to.jnis_jual, " & _
                      "0 AS brutto, 0 AS disc_tot, 0 as netto " & _
                    "FROM         trfaktur_to INNER JOIN " & _
                      "ms_pegawai ON trfaktur_to.kd_karyawan = ms_pegawai.kd_karyawan INNER JOIN " & _
                      "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko " & _
                    "WHERE     (trfaktur_to.sbatal = 0) AND (trfaktur_to.nobukti = '{0}')", nobukti)

        Dim sql2 As String = String.Format("SELECT ms_barang.nama_barang,  0 as qty, '' as satuan,0 as harga, 0 as jumlah, trfaktur_to3.nobukti " & _
        "FROM   trfaktur_to3 INNER JOIN " & _
        "ms_barang ON trfaktur_to3.kd_barang = ms_barang.kd_barang WHERE trfaktur_to3.nobukti='{0}'", nobukti)

        Dim sql3 As String = "select kd_barang,nama_barang,nohrus,0 as qty,'' as satuan,0 as harga,0 as disc,0 as jumlah from ms_barang where hrusfaktur=1"

        Dim sqldetail As String = String.Format("select * from trfaktur_to2 where nobukti='{0}'", nobukti)

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New dsinvoice
            ds = Clsmy.GetDataSet(sql1, cn)

            dsinvoice_ku = New DataSet
            dsinvoice_ku = ds

            Dim ds2 As DataSet = New dsinvoice2
            ds2 = Clsmy.GetDataSet(sql2, cn)


            Dim ds3 As DataSet = New dsbarang_to
            ds3 = Clsmy.GetDataSet(sql3, cn)

            Dim dsdetail As DataSet = New DataSet
            dsdetail = Clsmy.GetDataSet(sqldetail, cn)

            Dim dtdetail As DataTable = dsdetail.Tables(0)

            For i As Integer = 0 To dtdetail.Rows.Count - 1

                Dim kdbar As String = dtdetail(i)("kd_barang").ToString
                Dim qty As Integer = 0 ' Integer.Parse(dtdetail(i)("qty").ToString)
                Dim satuan As String = "" 'dtdetail(i)("satuan").ToString
                Dim harga As Integer = 0 ' Double.Parse(dtdetail(i)("harga").ToString)
                Dim disc As Integer = 0 'Double.Parse(dtdetail(i)("disc_rp").ToString)
                Dim jumlah As Integer = 0 'Double.Parse(dtdetail(i)("jumlah").ToString)

                For x As Integer = 0 To ds3.Tables(0).Rows.Count - 1

                    If ds3.Tables(0).Rows(x)("kd_barang").ToString.Equals(kdbar) Then

                        ds3.Tables(0).Rows(x)("qty") = qty
                        ds3.Tables(0).Rows(x)("satuan") = satuan
                        ds3.Tables(0).Rows(x)("harga") = harga
                        ds3.Tables(0).Rows(x)("disc") = disc
                        ds3.Tables(0).Rows(x)("jumlah") = jumlah

                        Exit For

                    End If

                Next

            Next



            Dim ops As New System.Drawing.Printing.PrinterSettings

            ' Dim rinvoice2 As New r_invoice2

            'rinvoice2.DataSource = ds2.Tables(0)
            'rinvoice2.DataMember = rinvoice2.DataMember
            '  rinvoice2.PrinterName = ops.PrinterName
            '  rinvoice2.CreateDocument(True)

            'PrintControl1.PrintingSystem = rinvoice2.PrintingSystem

            Dim rinvoice As New r_invoice() With {.DataSource = ds.Tables(0)}
            rinvoice.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
            rinvoice.DataMember = rinvoice.DataMember

            rinvoice.XrSubreport1.ReportSource = New r_invoice2
            rinvoice.XrSubreport1.ReportSource.DataSource = ds2.Tables(0)
            rinvoice.XrSubreport1.ReportSource.DataMember = rinvoice.XrSubreport1.ReportSource.DataMember

            rinvoice.XrSubreport2.ReportSource = New r_invoice3
            rinvoice.XrSubreport2.ReportSource.DataSource = ds3.Tables(0)
            rinvoice.XrSubreport2.ReportSource.DataMember = rinvoice.XrSubreport2.ReportSource.DataMember



            rinvoice.PrinterName = ops.PrinterName
            rinvoice.CreateDocument(True)
            rinvoice.Print()
            '  PrintControl1.PrintingSystem = rinvoice.PrintingSystem

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