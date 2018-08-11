Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fbayar

    Private bs1 As BindingSource
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private dvmanager_kemb As Data.DataViewManager
    Private dv_kemb As Data.DataView

    Private IsLoadForm As Boolean

    Private Sub opendata()

        Dim sql As String = String.Format("SELECT nobukti, tanggal, tglbayar, total, note, sbatal FROM trbayar where tanggal >='{0}' and  tanggal <='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))


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

            opengrid_detail()

        End Try

    End Sub

    Private Sub opengrid_detail()

        If IsLoadForm = True Then
            Return
        End If

        Dim nobukti As String
        If IsNothing(dv1) Then
            nobukti = "xx1"
        ElseIf dv1.Count <= 0 Then
            nobukti = "xx1"
        Else
            nobukti = dv1(bs1.Position)("nobukti").ToString
        End If

        Dim sql As String = String.Format("SELECT trbayar2.noid, trfaktur_to.nobukti,trfaktur_to.no_nota, trfaktur_to.tanggal, trbayar2.netto, trbayar2.sisapiutang, trbayar2.jmltunai,trbayar2.jmltrans, trbayar2.jmlgiro, trbayar2.jmlretur, trbayar2.pembulatan, " & _
        "trbayar2.jumlah, ms_toko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko,ms_toko.kd_group,trfaktur_to.kd_karyawan,trbayar2.disc_susulan,trbayar2.jmlkelebihan_dr,trbayar2.jmlkelebihan " & _
        "FROM trbayar2 INNER JOIN " & _
        "trfaktur_to ON trbayar2.nobukti_fak = trfaktur_to.nobukti INNER JOIN " & _
        "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko " & _
        "WHERE trbayar2.nobukti='{0}'", nobukti)

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        grid2.DataSource = Nothing

        Try

            '  open_wait()

            dv2 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager2 = New DataViewManager(ds)
            dv2 = dvmanager2.CreateDataView(ds.Tables(0))

            grid2.DataSource = dv2

            '   close_wait()


        Catch ex As OleDb.OleDbException
            '  close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub opengrid_kemb()

        If IsLoadForm = True Then
            Return
        End If

        Dim nobukti As String
        If IsNothing(dv1) Then
            nobukti = "xx1"
        ElseIf dv1.Count <= 0 Then
            nobukti = "xx1"
        Else
            nobukti = dv1(bs1.Position)("nobukti").ToString
        End If

        Dim sql As String = String.Format("SELECT trbayar2_kemb.noid,trfaktur_to.nobukti, trfaktur_to.no_nota, trfaktur_to.tanggal, ms_toko.kd_toko, ms_toko.nama_toko, " & _
         "ms_toko.alamat_toko, ms_pegawai.kd_karyawan, " & _
         "ms_pegawai.nama_karyawan, trbayar2_kemb.jmlfak, trbayar2_kemb.jmlbayar " & _
         "FROM ms_toko INNER JOIN trfaktur_to ON ms_toko.kd_toko = trfaktur_to.kd_toko INNER JOIN " & _
         "trbayar2_kemb INNER JOIN ms_pegawai ON trbayar2_kemb.kd_karyawan = ms_pegawai.kd_karyawan " & _
         "ON trfaktur_to.nobukti = trbayar2_kemb.nobukti_fak " & _
         "where trbayar2_kemb.nobukti='{0}'", nobukti)

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        grid_kmb.DataSource = Nothing

        Try

            '  open_wait()

            dv_kemb = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager_kemb = New DataViewManager(ds)
            dv_kemb = dvmanager_kemb.CreateDataView(ds.Tables(0))

            grid_kmb.DataSource = dv_kemb

            'close_wait()


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

        sql = String.Format("SELECT nobukti, tanggal, tglbayar, total, note, sbatal FROM trbayar where tanggal >='{0}' and  tanggal <='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))

        Select Case tcbofind.SelectedIndex
            Case 0 ' nobukti
                sql = String.Format("{0} and nobukti like '%{1}%'", sql, tfind.Text.Trim)
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

                sql = String.Format("{0} and tanggal='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))
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

                sql = String.Format("{0} and tglbayar='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))
            Case 3
                sql = String.Format("{0} and nobukti in (select nobukti from trbayar2 where nobukti_fak like '%{1}%')", sql, tfind.Text.Trim)
            Case 4
                sql = String.Format("{0} and nobukti in (select trbayar2.nobukti from trbayar2 inner join trfaktur_to on trbayar2.nobukti_fak=trfaktur_to.nobukti where trfaktur_to.no_nota like '%{1}%')", sql, tfind.Text.Trim)
            Case 5
                sql = String.Format("{0} and nobukti in (select x1.nobukti from trbayar2 x1 inner join trfaktur_to x2 on x1.nobukti_fak=x2.nobukti inner join ms_toko x3 on x2.kd_toko=x3.kd_toko where x3.nama_toko like '%{1}%')", sql, tfind.Text.Trim)
            Case 6
                sql = String.Format("{0} and note like '%{1}%'", sql, tfind.Text.Trim)
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

        Dim nobukti As String = dv1(Me.BindingContext(bs1).Position)("nobukti").ToString

        Dim sql As String = String.Format("update trbayar set sbatal=1,alasan_batal='{0}' where nobukti='{1}'", alasan.ToUpper, nobukti)
        Dim sql2 As String = String.Format("select a.jumlah,a.jmltunai,a.jmlgiro,a.jmlretur,a.pembulatan,b.nobukti,b.kd_toko,b.kd_karyawan,a.jmlkelebihan,a.disc_susulan,a.jmlkelebihan_dr,a.sisapiutang,a.jmltrans from trbayar2 a inner join trfaktur_to b on a.nobukti_fak=b.nobukti where a.nobukti='{0}'", nobukti)

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand = Nothing
        Dim cmdtoko As OleDbCommand = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds_bayar2 As DataSet = New DataSet
            ds_bayar2 = Clsmy.GetDataSet(sql2, cn)
            Dim dt_byr2 As DataTable
            dt_byr2 = ds_bayar2.Tables(0)

            sqltrans = cn.BeginTransaction

            comd = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            Dim sql_dtg As String = String.Format("select * from trbayar_dtg where nobukti='{0}'", nobukti)
            Dim cmd_dtg As OleDbCommand = New OleDbCommand(sql_dtg, cn, sqltrans)
            Dim dr_dtg As OleDbDataReader = cmd_dtg.ExecuteReader

            If dr_dtg.HasRows Then

                While dr_dtg.Read

                    Dim sqlup_dtg As String = String.Format("update trdaftar_tgh set spulang=0 where nobukti='{0}'", dr_dtg("nobukti_dtg").ToString)
                    Using cmdup_dtg As OleDbCommand = New OleDbCommand(sqlup_dtg, cn, sqltrans)
                        cmdup_dtg.ExecuteNonQuery()
                    End Using

                End While

            End If

            dr_dtg.Close()

            hapus2(cn, sqltrans, dt_byr2, nobukti)
            hapus_kemb(cn, sqltrans, nobukti)

            Clsmy.InsertToLog(cn, "btbayar", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString, dv1(Me.BindingContext(bs1).Position)("tanggal").ToString, sqltrans)

            sqltrans.Commit()

            dv1(bs1.Position)("sbatal") = 1

            close_wait()

            MsgBox("Data telah dibatalkan...", vbOKOnly + vbInformation, "Informasi")


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

    Private Sub hapus2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal dt2 As DataTable, ByVal nobukti As String)

        'Dim sql As String = String.Format("select a.jumlah,a.jmltunai,a.jmlgiro,a.jmlretur,a.pembulatan,b.nobukti,b.kd_toko,b.kd_karyawan,a.jmlkelebihan,a.disc_susulan,a.jmlkelebihan_dr,a.sisapiutang,a.jmltrans from trbayar2 a inner join trfaktur_to b on a.nobukti_fak=b.nobukti where a.nobukti='{0}'", nobukti)

        'Dim cmd_hap2 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        'Dim dt2(i) As OleDbDataReader = cmd_hap2.ExecuteReader

        'If dt2(i).HasRows Then
        For i As Integer = 0 To dt2.Rows.Count - 1

            Dim jumlah As Double = 0
            If IsNumeric(dt2(i)("jumlah").ToString) Then
                jumlah = Double.Parse(dt2(i)("jumlah").ToString)
            End If

            If jumlah > 0 Then

                Dim kdtoko As String = dt2(i)("kd_toko").ToString
                Dim kdsales As String = dt2(i)("kd_karyawan").ToString
                Dim nobukti_fak As String = dt2(i)("nobukti").ToString

                Dim jmltunai As Double = Double.Parse(dt2(i)("jmltunai").ToString)
                Dim jmltransfer As Double = Double.Parse(dt2(i)("jmltrans").ToString)
                Dim pembulatan As Double = Double.Parse(dt2(i)("pembulatan").ToString)
                Dim jmlretur As Double = Double.Parse(dt2(i)("jmlretur").ToString)
                Dim disc_susulan As Double = Double.Parse(dt2(i)("disc_susulan").ToString)
                Dim jmlkelebihan_dr As Double = Double.Parse(dt2(i)("jmlkelebihan_dr").ToString)
                Dim sisapiutang As Double = Double.Parse(dt2(i)("sisapiutang").ToString)

                Dim jmlgiro As Double = Double.Parse(dt2(i)("jmlgiro").ToString)


                Dim jmlmin_giro As Double = (jmltunai + jmltransfer + jmlretur + pembulatan + jmlkelebihan_dr + disc_susulan)

                If jmlmin_giro > sisapiutang Then
                    jmlmin_giro = sisapiutang
                End If

                Dim jmlkelebihan As Double = Double.Parse(dt2(i)("jmlkelebihan").ToString)

                Dim sqltoko As String = String.Format("update ms_toko set piutangbeli = piutangbeli + {0},jumlahretur=jumlahretur + {1} where kd_toko='{2}'", Replace(jmlmin_giro, ",", "."), Replace(jmlretur, ",", "."), kdtoko)
                Using cmdtoko As OleDbCommand = New OleDbCommand(sqltoko, cn, sqltrans)
                    cmdtoko.ExecuteNonQuery()
                End Using

                Dim sqlsales As String = String.Format("update ms_toko2 set jmlpiutang=jmlpiutang + {0} where kd_toko='{1}' and kd_karyawan='{2}'", Replace(jmlmin_giro, ",", "."), kdtoko, kdsales)
                Using cmdsales As OleDbCommand = New OleDbCommand(sqlsales, cn, sqltrans)
                    cmdsales.ExecuteNonQuery()
                End Using

                Dim sqlfaktur As String = String.Format("update trfaktur_to set jmlbayar=jmlbayar - {0},jmlgiro=jmlgiro - {1},jmlkelebihan=jmlkelebihan - {2} where nobukti='{3}'", Replace(jmlmin_giro, ",", "."), Replace(jmlgiro, ",", "."), Replace(jmlkelebihan, ",", "."), nobukti_fak)
                Using cmdfaktur As OleDbCommand = New OleDbCommand(sqlfaktur, cn, sqltrans)
                    cmdfaktur.ExecuteNonQuery()
                End Using

                Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, nobukti_fak, nobukti, dv1(bs1.Position)("tanggal").ToString, kdtoko, Replace(jmlmin_giro, ",", "."), 0, "Pembayaran (Batal)")

                hapus3(cn, sqltrans, nobukti, nobukti_fak)

            End If

        Next
        ' End If

    End Sub

    Private Sub hapus3(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal nobukti As String, ByVal nobukti_fak As String)

        '' update giro

        Dim sqlcgiro As String = String.Format("select a.nogiro,a.jmlgiro,b.jumlah from trbayar2_giro a inner join ms_giro b on a.nogiro=b.nogiro where a.nobukti='{0}' and a.nobukti_fak='{1}'", nobukti, nobukti_fak)
        Dim cmdgiro As OleDbCommand = New OleDbCommand(sqlcgiro, cn, sqltrans)
        Dim drgiro As OleDbDataReader = cmdgiro.ExecuteReader

        If drgiro.Read Then

            Dim nogiro As String = drgiro("nogiro").ToString

            If Not nogiro = "" Then

                Dim jmlbayar As Double = Double.Parse(drgiro("jmlgiro").ToString)
                Dim jmlgiro As Double = Double.Parse(drgiro("jumlah").ToString)

                Dim sisa As Double = jmlgiro - jmlbayar

                If sisa <= 5 Then

                    Dim sqlpakai As String = String.Format("update ms_giro set sgunakan=0 where nogiro='{0}'", nogiro)
                    Using cmdpakai As OleDbCommand = New OleDbCommand(sqlpakai, cn, sqltrans)
                        cmdpakai.ExecuteNonQuery()
                    End Using

                End If

                Dim sqlkurang As String = String.Format("update ms_giro set jumlahpakai=jumlahpakai - {0} where nogiro='{1}'", Replace(jmlbayar, ",", "."), nogiro)
                Using cmdkurang As OleDbCommand = New OleDbCommand(sqlkurang, cn, sqltrans)
                    cmdkurang.ExecuteNonQuery()
                End Using

            End If
        End If

        drgiro.Close()

        '' update retur

        Dim sqlcretur As String = String.Format("select b.nobukti,a.jmlretur,b.netto from trbayar2_ret a inner join trretur b on a.nobukti_ret=b.nobukti where a.nobukti='{0}' and a.nobukti_fak='{1}'", nobukti, nobukti_fak)
        Dim cmdret As OleDbCommand = New OleDbCommand(sqlcretur, cn, sqltrans)
        Dim drretur As OleDbDataReader = cmdret.ExecuteReader

        If drretur.Read Then

            Dim nobukti_ret As String = drretur("nobukti").ToString

            If Not nobukti_ret = "" Then

                Dim jmlretur As Double = Double.Parse(drretur("jmlretur").ToString)
                Dim jmlnett As Double = Double.Parse(drretur("netto").ToString)

                Dim sisaretur As Double = jmlnett - jmlretur

                If sisaretur <= 5 Then

                    Dim sqlup_potong As String = String.Format("update trretur set spotong=0 where nobukti='{0}'", nobukti_ret)
                    Using cmdPotong_ret As OleDbCommand = New OleDbCommand(sqlup_potong, cn, sqltrans)
                        cmdPotong_ret.ExecuteNonQuery()
                    End Using

                End If

                Dim sqlret As String = String.Format("update trretur set jmlpotong=jmlpotong - {0} where nobukti='{1}'", Replace(jmlretur, ",", "."), nobukti_ret)
                Using cmdret_ptong As OleDbCommand = New OleDbCommand(sqlret, cn, sqltrans)
                    cmdret_ptong.ExecuteNonQuery()
                End Using

            End If

        End If

        drretur.Close()

        '' update kelebihan

        Dim sql_lebih As String = String.Format("select * from trbayar2_kelebihan where nobukti='{0}' and nobukti_fak='{1}'", nobukti, nobukti_fak)
        Dim cmdlebih As OleDbCommand = New OleDbCommand(sql_lebih, cn, sqltrans)
        Dim drlebih As OleDbDataReader = cmdlebih.ExecuteReader

        If drlebih.Read Then

            Dim nobukti_lebih As String = drlebih("nobukti_pot").ToString

            If Not nobukti_lebih = "" Then

                Dim jmllebih As Double = Double.Parse(drlebih("jumlah").ToString)

                Dim sqlret As String = String.Format("update trfaktur_to set jmlkelebihan_pot=jmlkelebihan_pot - {0} where nobukti='{1}'", Replace(jmllebih, ",", "."), nobukti_lebih)
                Using cmdret_ptong As OleDbCommand = New OleDbCommand(sqlret, cn, sqltrans)
                    cmdret_ptong.ExecuteNonQuery()
                End Using

            End If

        End If

        drretur.Close()

    End Sub

    Private Sub hapus_kemb(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal nobukti As String)

        Dim sqlcek As String = String.Format("select * from trbayar2_kemb where nobukti='{0}'", nobukti)
        Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn, sqltrans)
        Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

        While drdcek.Read
            If IsNumeric(drdcek("noid").ToString) Then

                Dim nofaktur2 As String = drdcek("nobukti_fak").ToString
                Dim jmlbay2 As Double = Double.Parse(drdcek("jmlbayar").ToString)

                Dim sqlbalikkan As String = String.Format("update trfaktur_to set jmlkelebihan_pot=jmlkelebihan_pot-{0} where nobukti='{1}'", jmlbay2, nofaktur2)
                Using cmdbalik2 As OleDbCommand = New OleDbCommand(sqlbalikkan, cn, sqltrans)
                    cmdbalik2.ExecuteNonQuery()
                End Using

            End If
        End While
        drdcek.Close()

    End Sub

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

    Private Sub cekbatal_onserver()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select sbatal from trbayar where nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)
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

    Private Function CekGiro_SudahCair() As Boolean

        Dim hasil As Boolean = False

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select a.noid from trbayar2_giro a inner join ms_giro b on a.nogiro=b.nogiro " & _
            "where (b.scair=1 or b.stolak=1) and a.nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd(0).ToString) Then
                    hasil = True
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

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Data telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If CekGiro_SudahCair() = True Then
            MsgBox("Terdapat giro yang sudah cair/tolak", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If MsgBox("Yakin akan dibatalkan ?", vbYesNo + vbQuestion, "Konfirmasi") = vbNo Then
            Exit Sub
        End If

        hapus()

    End Sub

    Private Sub tsadd_Click(sender As System.Object, e As System.EventArgs) Handles tsadd.Click

        IsLoadForm = True

        Using fkar2 As New fbayar2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0, .statview = False}
            fkar2.ShowDialog()

            IsLoadForm = False

        End Using
    End Sub

    Private Sub tsedit_Click(sender As System.Object, e As System.EventArgs) Handles tsedit.Click

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

        If CekGiro_SudahCair() = True Then
            MsgBox("Terdapat giro yang sudah cair/tolak", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        IsLoadForm = True

        Using fkar2 As New fbayar2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position, .statview = False}
            fkar2.ShowDialog()
            IsLoadForm = False
        End Using

    End Sub

    Private Sub tsview_Click(sender As System.Object, e As System.EventArgs) Handles tsview.Click

        IsLoadForm = True

        Using fkar2 As New fbayar2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position, .statview = True}

            fkar2.btsimpan.Enabled = False

            fkar2.ShowDialog()

            IsLoadForm = False

        End Using

    End Sub

    Private Sub GridView1_Click(sender As Object, e As System.EventArgs) Handles GridView1.Click
        opengrid_detail()
        opengrid_kemb()
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        opengrid_detail()
        opengrid_kemb()
    End Sub

End Class