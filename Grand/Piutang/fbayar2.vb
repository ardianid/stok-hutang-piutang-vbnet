Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fbayar2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Public statview As Boolean

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager_dtg As Data.DataViewManager
    Private dv_dtg As Data.DataView

    Private dvmanager_sal As Data.DataViewManager
    Private dv_sal As Data.DataView

    Private dvmanager_tko As Data.DataViewManager
    Private dv_tko As Data.DataView

    Private dvmanager_kemb As Data.DataViewManager
    Private dv_kemb As Data.DataView

    Private dv_saleskemb As Data.DataView

    Private dtgiro As DataTable
    Private dtretur As DataTable
    Private dtkelebihan As DataTable

    Private tgl_old As String

    Private Sub kosongkan()
        tbukti.Text = "<< New >>"

        tnote.Text = ""

        opengrid()
        opengrid_kemb()
        opengrid_sales()
        opengrid_outl()
        opengrid_dtg()

        open_giropakai()
        open_returpakai()
        open_kelebihanpakai()

        ttot_byr.EditValue = 0
        ttot_kemb.EditValue = 0
        ttot_all.EditValue = 0

    End Sub

    Private Sub opengrid()

        Dim sql As String = String.Format("SELECT trbayar2.noid, trfaktur_to.nobukti,trfaktur_to.no_nota, trfaktur_to.tanggal, trbayar2.netto, trbayar2.sisapiutang, trbayar2.jmltunai,trbayar2.jmltrans, trbayar2.jmlgiro, trbayar2.jmlretur, trbayar2.pembulatan, " & _
        "trbayar2.jumlah, ms_toko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko,ms_toko.kd_group,trfaktur_to.kd_karyawan,trbayar2.disc_susulan,trbayar2.jmlkelebihan,trbayar2.jmlkelebihan_dr,trbayar2.note " & _
        "FROM trbayar2 INNER JOIN " & _
        "trfaktur_to ON trbayar2.nobukti_fak = trfaktur_to.nobukti INNER JOIN " & _
        "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko " & _
        "WHERE trbayar2.nobukti='{0}'", tbukti.Text.Trim)

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

    Private Sub opengrid_kemb()

        Dim sql As String = String.Format("SELECT trbayar2_kemb.noid,trfaktur_to.nobukti, trfaktur_to.no_nota, trfaktur_to.tanggal, ms_toko.kd_toko, ms_toko.nama_toko, " & _
        "ms_toko.alamat_toko, ms_pegawai.kd_karyawan, " & _
        "ms_pegawai.nama_karyawan, trbayar2_kemb.jmlfak, trbayar2_kemb.jmlbayar " & _
        "FROM ms_toko INNER JOIN trfaktur_to ON ms_toko.kd_toko = trfaktur_to.kd_toko INNER JOIN " & _
        "trbayar2_kemb INNER JOIN ms_pegawai ON trbayar2_kemb.kd_karyawan = ms_pegawai.kd_karyawan " & _
        "ON trfaktur_to.nobukti = trbayar2_kemb.nobukti_fak " & _
        "where trbayar2_kemb.nobukti='{0}'", tbukti.Text.Trim)

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

    Private Sub opengrid_sales()

        Dim sql As String = String.Format("SELECT ms_pegawai.kd_karyawan, ms_pegawai.nama_karyawan " & _
        "FROM trbayar_sles INNER JOIN " & _
        "ms_pegawai ON trbayar_sles.kd_karyawan = ms_pegawai.kd_karyawan WHERE trbayar_sles.nobukti='{0}'", tbukti.Text.Trim)

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        grid_sal.DataSource = Nothing

        Try

            open_wait()

            dv_sal = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager_sal = New DataViewManager(ds)
            dv_sal = dvmanager_sal.CreateDataView(ds.Tables(0))

            grid_sal.DataSource = dv_sal

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

    Private Sub opengrid_outl()

        Dim sql As String = String.Format("SELECT ms_toko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko " & _
        "FROM trbayar_tko INNER JOIN " & _
        "ms_toko ON trbayar_tko.kd_toko = ms_toko.kd_toko where trbayar_tko.nobukti='{0}'", tbukti.Text.Trim)

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        grid_outl.DataSource = Nothing

        Try

            open_wait()

            dv_tko = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager_tko = New DataViewManager(ds)
            dv_tko = dvmanager_tko.CreateDataView(ds.Tables(0))

            grid_outl.DataSource = dv_tko

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

    Private Sub opengrid_dtg()

        Dim sql As String = String.Format("SELECT trdaftar_tgh.nobukti, trdaftar_tgh.tanggal, trdaftar_tgh.tgl_tagih,ms_pegawai.kd_karyawan, ms_pegawai.nama_karyawan " & _
        "FROM trbayar_dtg INNER JOIN " & _
        "trdaftar_tgh ON trbayar_dtg.nobukti_dtg = trdaftar_tgh.nobukti INNER JOIN " & _
        "ms_pegawai ON trdaftar_tgh.kd_kolek = ms_pegawai.kd_karyawan WHERE trbayar_dtg.nobukti='{0}'", tbukti.Text.Trim)

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        grid_dtg.DataSource = Nothing

        Try

            open_wait()

            dv_dtg = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager_dtg = New DataViewManager(ds)
            dv_dtg = dvmanager_dtg.CreateDataView(ds.Tables(0))

            grid_dtg.DataSource = dv_dtg

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

    Private Sub open_giropakai()

        Dim sql As String = String.Format("select a.nobukti_fak,b.jumlah, a.nogiro,a.jmlgiro,b.namabank,b.tgl_giro,ms_pegawai.nama_karyawan " & _
            "from trbayar2_giro a inner join ms_giro b on a.nogiro=b.nogiro inner join ms_pegawai on b.kd_karyawan=ms_pegawai.kd_karyawan " & _
            "where a.nobukti='{0}'", tbukti.Text.Trim)

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            dtgiro = New DataTable
            dtgiro = ds.Tables(0)

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

    Private Sub open_returpakai()

        Dim sql As String = String.Format("select x1.nobukti_fak,x1.nobukti_ret,x1.jmlretur,x2.tanggal,x2.alasan_retur,x2.netto " & _
            "from trbayar2_ret x1 inner join trretur x2 on x1.nobukti_ret=x2.nobukti " & _
            "where x1.nobukti='{0}'", tbukti.Text.Trim)

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            dtretur = New DataTable
            dtretur = ds.Tables(0)

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

    Private Sub open_kelebihanpakai()

        Dim sql As String = String.Format("select a.nobukti_fak,a.nobukti_pot, b.tanggal,b.tgl_tempo,b.jmlkelebihan,a.jumlah " & _
        "from trbayar2_kelebihan a inner join trfaktur_to b on a.nobukti_pot=b.nobukti WHERE a.nobukti='{0}'", tbukti.Text.Trim)

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            dtkelebihan = New DataTable
            dtkelebihan = ds.Tables(0)

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

    Private Sub isi()

        Dim nobukti As String = dv(position)("nobukti").ToString

        Dim sql As String = String.Format("SELECT nobukti, tanggal, tglbayar, note, jfaktur " & _
        "FROM trbayar WHERE nobukti='{0}'", nobukti)

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
                        ttgl.EditValue = DateValue(dread("tanggal").ToString)

                        tgl_old = ttgl.EditValue

                        ttgl_tgh.EditValue = DateValue(dread("tglbayar").ToString)

                        tnote.EditValue = dread("note").ToString

                        tjfaktur.EditValue = dread("jfaktur").ToString

                        opengrid()
                        opengrid_kemb()
                        opengrid_sales()
                        opengrid_outl()
                        opengrid_dtg()

                        open_giropakai()
                        open_returpakai()
                        open_kelebihanpakai()

                        hitung_total()

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

    Private Function cekbukti(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim tahun As String = Year(ttgl.EditValue)
        tahun = Microsoft.VisualBasic.Right(tahun, 2)
        Dim bulan As String = Month(ttgl.EditValue)

        If bulan.Length = 1 Then
            bulan = "0" & bulan
        End If

        Dim tahunbulan As String = String.Format("{0}{1}", tahun, bulan)

        Dim sql As String = String.Format("select max(nobukti) from trbayar where nobukti like '%PP.{0}%'", tahunbulan)

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

        Return String.Format("PP.{0}{1}{2}", tahun, bulan, kbukti)

    End Function

    Private Sub LoadData()

        Dim sql As String = "SELECT trfaktur_to.nobukti,trfaktur_to.no_nota, trfaktur_to.tanggal, trfaktur_to.netto,(trfaktur_to.netto - (trfaktur_to.jmlbayar + trfaktur_to.jmlgiro + trfaktur_to.jmlgiro_real)) as sisapiutang, " & _
        "ms_toko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko,ms_toko.kd_group, trfaktur_to.kd_karyawan ,0 as noid,0 as jmltunai,0 as jmltrans,0 as jmlgiro,0 as jmlretur,0 as pembulatan,0 as jumlah,0 as disc_susulan,0 as jmlkelebihan,0 as jmlkelebihan_dr,'' as note " & _
        "FROM trfaktur_to INNER JOIN ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko " & _
        "WHERE trfaktur_to.sbatal=0 and trfaktur_to.netto > (trfaktur_to.jmlbayar + trfaktur_to.jmlgiro + trfaktur_to.jmlgiro_real) And trfaktur_to.spulang = 1 and trfaktur_to.statkirim='TERKIRIM'"


        If dv_dtg.Count <= 0 Then
            If tjfaktur.EditValue = "TO" Then
                sql = String.Format("{0} and trfaktur_to.jnis_fak='T'", sql)
            ElseIf tjfaktur.EditValue = "KANVAS" Then
                sql = String.Format("{0} and trfaktur_to.jnis_fak='K'", sql)
            End If
        Else
            tjfaktur.SelectedIndex = 0
        End If

        

        ' search by sales

        If Not (IsNothing(dv_sal)) Then
            If dv_sal.Count > 0 Then

                Dim x As Integer = 0

                For i As Integer = 0 To dv_sal.Count - 1

                    If i = 0 Then
                        sql = String.Format("{0} and trfaktur_to.kd_karyawan in ('{1}'", sql, dv_sal(i)("kd_karyawan").ToString)
                    Else
                        sql = String.Format("{0},'{1}'", sql, dv_sal(i)("kd_karyawan").ToString)
                    End If

                    x = x + 1

                Next

                If x > 0 Then
                    sql = String.Format("{0})", sql)
                End If

            End If
        End If

        '------------------------------------------

        ' search by outlet

        If Not (IsNothing(dv_tko)) Then
            If dv_tko.Count > 0 Then

                Dim x As Integer = 0

                For i As Integer = 0 To dv_tko.Count - 1

                    If i = 0 Then
                        sql = String.Format("{0} and ms_toko.kd_toko in ('{1}'", sql, dv_tko(i)("kd_toko").ToString)
                    Else
                        sql = String.Format("{0},'{1}'", sql, dv_tko(i)("kd_toko").ToString)
                    End If

                    x = x + 1

                Next

                If x > 0 Then
                    sql = String.Format("{0})", sql)
                End If

            End If
        End If

        '-----------------------------------------------------------

        ' search by dtg

        If Not (IsNothing(dv_dtg)) Then
            If dv_dtg.Count > 0 Then

                Dim x As Integer = 0

                For i As Integer = 0 To dv_dtg.Count - 1

                    If i = 0 Then
                        sql = String.Format("{0} and trfaktur_to.nobukti in (select nobukti_fak from trdaftar_tgh2 where nobukti='{1}'", sql, dv_dtg(i)("nobukti").ToString)
                    Else
                        sql = String.Format("{0},'{1}'", sql, dv_dtg(i)("nobukti").ToString)
                    End If

                    x = x + 1

                Next

                If x > 0 Then
                    sql = String.Format("{0})", sql)
                End If

            End If
        End If

        '-----------------------------------------------------------

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

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try
            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            Dim cmd As OleDbCommand

            Dim total As Double = ttot_all.EditValue


            If addstat Then
                Dim bukti As String = cekbukti(cn, sqltrans)
                tbukti.EditValue = bukti

                '1 . update faktur
                Dim sqlin_faktur As String = String.Format("insert into trbayar (nobukti,tanggal,tglbayar,note,jfaktur,total) values('{0}','{1}','{2}','{3}','{4}',{5})", tbukti.Text.Trim, _
                                                            convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_tgh.EditValue), tnote.Text.Trim, tjfaktur.EditValue, Replace(total, ",", "."))


                cmd = New OleDbCommand(sqlin_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btbayar", 1, 0, 0, 0, tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), sqltrans)

            Else

                '1. update faktur
                Dim sqlup_faktur As String = String.Format("update trbayar set tanggal='{0}',tglbayar='{1}',note='{2}',jfaktur='{3}',total={4} where nobukti='{5}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_tgh.EditValue), tnote.Text.Trim, tjfaktur.EditValue, Replace(total, ",", "."), tbukti.Text.Trim)

                cmd = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btbayar", 0, 1, 0, 0, tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), sqltrans)

            End If

            simpan2(cn, sqltrans)
            simpan_detail(cn, sqltrans)
            simpan_giro(cn, sqltrans)
            simpan_retur(cn, sqltrans)
            simpan_kelebihan(cn, sqltrans)

            Dim hasil_simpankemb As String = simpan_kemb(cn, sqltrans)

            If Not hasil_simpankemb.Trim.Equals("ok") Then

                close_wait()
                If Not IsNothing(sqltrans) Then
                    sqltrans.Rollback()
                End If

                MsgBox(hasil_simpankemb, vbOKOnly + vbInformation, "Informasi")
                GoTo finish_him

            End If

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

finish_him:

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

    Private Sub simpan2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        ' dtg --------------------------------

        Dim sqldel As String = String.Format("delete from trbayar_dtg where nobukti='{0}'", tbukti.Text.Trim)
        Using cmddel As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
            cmddel.ExecuteNonQuery()
        End Using

        For i As Integer = 0 To dv_dtg.Count - 1

            Dim sql As String = String.Format("insert into trbayar_dtg (nobukti,nobukti_dtg) values('{0}','{1}')", tbukti.Text.Trim, _
                                              dv_dtg(i)("nobukti").ToString)

            Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()
            End Using

            Dim sqldtg As String = String.Format("update trdaftar_tgh set spulang=1 where nobukti='{0}'", dv_dtg(i)("nobukti").ToString)

            Using cmddtg As OleDbCommand = New OleDbCommand(sqldtg, cn, sqltrans)
                cmddtg.ExecuteNonQuery()
            End Using


        Next


        ' sales -----------------------------------

        Dim sqlsal As String = String.Format("delete from trbayar_sles where nobukti='{0}'", tbukti.Text.Trim)
        Using cmddel As OleDbCommand = New OleDbCommand(sqlsal, cn, sqltrans)
            cmddel.ExecuteNonQuery()
        End Using

        If Not (IsNothing(dv_sal)) Then

            For i As Integer = 0 To dv_sal.Count - 1
                Dim sql As String = String.Format("insert into trbayar_sles (nobukti,kd_karyawan) values('{0}','{1}')", tbukti.Text.Trim, dv_sal(i)("kd_karyawan").ToString)
                Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using
            Next

        End If

        ' ------------------------------------------------------------------------

        ' outlet -----------------------------------

        Dim sqlout As String = String.Format("delete from trbayar_tko where nobukti='{0}'", tbukti.Text.Trim)
        Using cmddel As OleDbCommand = New OleDbCommand(sqlout, cn, sqltrans)
            cmddel.ExecuteNonQuery()
        End Using

        If Not (IsNothing(dv_tko)) Then

            For i As Integer = 0 To dv_tko.Count - 1
                Dim sql As String = String.Format("insert into trbayar_tko (nobukti,kd_toko) values('{0}','{1}')", tbukti.Text.Trim, dv_tko(i)("kd_toko").ToString)
                Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using
            Next

        End If

        ' ------------------------------------------------------------------------




    End Sub

    Private Sub simpan_detail(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        For i As Integer = 0 To dv1.Count - 1

            Dim sqlc As String = String.Format("select * from trbayar2 where noid={0}", dv1(i)("noid"))
            Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
            Dim drdc As OleDbDataReader = cmdc.ExecuteReader

            If drdc.Read Then

                If drdc("nobukti").ToString.Equals("") Then
                    drdc.Close()
                    GoTo to_insert
                Else

                    Dim sqlup As String = String.Format("update trbayar2 set netto={0},sisapiutang={1},jmltunai={2},jmlgiro={3},jmlretur={4},pembulatan={5},jumlah={6},disc_susulan={7},jmlkelebihan={8},jmlkelebihan_dr={9},note='{10}',jmltrans={11} where noid={12}", _
                         Replace(dv1(i)("netto").ToString, ",", "."), Replace(dv1(i)("sisapiutang").ToString, ",", "."), Replace(dv1(i)("jmltunai").ToString, ",", "."), Replace(dv1(i)("jmlgiro").ToString, ",", "."), _
                         Replace(dv1(i)("jmlretur").ToString, ",", "."), Replace(dv1(i)("pembulatan").ToString, ",", "."), Replace(dv1(i)("jumlah").ToString, ",", "."), Replace(dv1(i)("disc_susulan").ToString, ",", "."), Replace(dv1(i)("jmlkelebihan").ToString, ",", "."), Replace(dv1(i)("jmlkelebihan_dr").ToString, ",", "."), dv1(i)("note").ToString, Replace(dv1(i)("jmltrans").ToString, ",", "."), dv1(i)("noid").ToString)

                    Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                        cmdup.ExecuteNonQuery()
                    End Using

                    Dim sisapiutang_old As Double = Double.Parse(drdc("sisapiutang").ToString)
                    Dim total_up As Double = (Double.Parse(drdc("jmltunai").ToString) + drdc("jmltrans").ToString + Double.Parse(drdc("jmlretur").ToString) + Double.Parse(drdc("pembulatan").ToString) + Double.Parse(drdc("jmlkelebihan_dr").ToString) + Double.Parse(drdc("disc_susulan").ToString))
                    Dim jmlold As Double = Double.Parse(drdc("jumlah").ToString)

                    If total_up > sisapiutang_old Then
                        total_up = sisapiutang_old
                    End If

                    Dim sqltokoup As String = String.Format("update ms_toko set piutangbeli=piutangbeli + {0},jumlahretur=jumlahretur + {1} where kd_toko='{2}'", Replace(total_up, ",", "."), Replace(drdc("jmlretur").ToString, ",", "."), dv1(i)("kd_toko").ToString)
                    Using cmdtoko As OleDbCommand = New OleDbCommand(sqltokoup, cn, sqltrans)
                        cmdtoko.ExecuteNonQuery()
                    End Using

                    Dim sqltoko2_up As String = String.Format("update ms_toko2 set jmlpiutang=jmlpiutang + {0} where kd_toko='{1}' and kd_karyawan='{2}'", Replace(total_up, ",", "."), dv1(i)("kd_toko").ToString, dv1(i)("kd_karyawan").ToString)
                    Using cmdtoko2 As OleDbCommand = New OleDbCommand(sqltoko2_up, cn, sqltrans)
                        cmdtoko2.ExecuteNonQuery()
                    End Using

                    Dim sqlfak_up As String = String.Format("update trfaktur_to set jmlbayar=jmlbayar- {0},jmlgiro=jmlgiro- {1},jmlkelebihan=jmlkelebihan - {2} where nobukti='{3}'", Replace(total_up, ",", "."), Replace(drdc("jmlgiro").ToString, ",", "."), Replace(drdc("jmlkelebihan").ToString, ",", "."), dv1(i)("nobukti").ToString)
                    Using cmdfak As OleDbCommand = New OleDbCommand(sqlfak_up, cn, sqltrans)
                        cmdfak.ExecuteNonQuery()
                    End Using

                    If IsDate(ttgl.EditValue) <> IsDate(tgl_old) Then

                        Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, dv1(i)("nobukti").ToString, tbukti.Text.Trim, tgl_old, dv1(i)("kd_toko").ToString, total_up, 0, "Pembayaran (Edit)")
                        Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, dv1(i)("nobukti").ToString, tbukti.Text.Trim, tgl_old, dv1(i)("kd_toko").ToString, 0, total_up, "Pembayaran (Edit)")

                    End If

                    Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, dv1(i)("nobukti").ToString, tbukti.Text.Trim, ttgl.EditValue, dv1(i)("kd_toko").ToString, total_up, 0, "Pembayaran (Edit)")

                    GoTo to_update

                End If

            Else

                drdc.Close()
                GoTo to_insert

            End If



to_insert:


            ' insert  ---------------------

            Dim sql As String = String.Format("insert into trbayar2 (nobukti,nobukti_fak,netto,sisapiutang,jmltunai,jmlgiro,jmlretur,pembulatan,jumlah,disc_susulan,jmlkelebihan,jmlkelebihan_dr,note,jmltrans) values('{0}','{1}',{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},'{12}',{13})", tbukti.Text.Trim, _
                                              dv1(i)("nobukti").ToString, Replace(dv1(i)("netto").ToString, ",", "."), Replace(dv1(i)("sisapiutang").ToString, ",", "."), Replace(dv1(i)("jmltunai").ToString, ",", "."), Replace(dv1(i)("jmlgiro").ToString, ",", "."), _
                                              Replace(dv1(i)("jmlretur").ToString, ",", "."), Replace(dv1(i)("pembulatan").ToString, ",", "."), Replace(dv1(i)("jumlah").ToString, ",", "."), Replace(dv1(i)("disc_susulan").ToString, ",", "."), Replace(dv1(i)("jmlkelebihan").ToString, ",", "."), Replace(dv1(i)("jmlkelebihan_dr").ToString, ",", "."), dv1(i)("note").ToString, Replace(dv1(i)("jmltrans").ToString, ",", "."))
            Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()
            End Using

to_update:

            Dim sisapiutang_real As Double = Double.Parse(dv1(i)("sisapiutang").ToString)
            Dim total_t As Double = (Double.Parse(dv1(i)("jmltunai").ToString) + Double.Parse(dv1(i)("jmltrans").ToString) + Double.Parse(dv1(i)("jmlretur").ToString) + Double.Parse(dv1(i)("pembulatan").ToString) + Double.Parse(dv1(i)("jmlkelebihan_dr").ToString) + Double.Parse(dv1(i)("disc_susulan").ToString))

            If total_t > sisapiutang_real Then
                total_t = sisapiutang_real
            End If

            Dim sqltoko As String = String.Format("update ms_toko set piutangbeli=piutangbeli - {0},jumlahretur=jumlahretur - {1} where kd_toko='{2}'", Replace(total_t, ",", "."), Replace(dv1(i)("jmlretur").ToString, ",", "."), dv1(i)("kd_toko").ToString)
            Using cmdtoko As OleDbCommand = New OleDbCommand(sqltoko, cn, sqltrans)
                cmdtoko.ExecuteNonQuery()
            End Using

            Dim sqltoko2 As String = String.Format("update ms_toko2 set jmlpiutang=jmlpiutang - {0} where kd_toko='{1}' and kd_karyawan='{2}'", Replace(total_t, ",", "."), dv1(i)("kd_toko").ToString, dv1(i)("kd_karyawan").ToString)
            Using cmdtoko2 As OleDbCommand = New OleDbCommand(sqltoko2, cn, sqltrans)
                cmdtoko2.ExecuteNonQuery()
            End Using

            Dim sqlfak As String = String.Format("update trfaktur_to set jmlbayar=jmlbayar+ {0},jmlgiro=jmlgiro+ {1},jmlkelebihan=jmlkelebihan+ {2} where nobukti='{3}'", Replace(total_t, ",", "."), Replace(dv1(i)("jmlgiro").ToString, ",", "."), Replace(dv1(i)("jmlkelebihan").ToString, ",", "."), dv1(i)("nobukti").ToString)
            Using cmdfak As OleDbCommand = New OleDbCommand(sqlfak, cn, sqltrans)
                cmdfak.ExecuteNonQuery()
            End Using

            If addstat = True Then
                Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, dv1(i)("nobukti").ToString, tbukti.Text.Trim, ttgl.EditValue, dv1(i)("kd_toko").ToString, 0, total_t, "Pembayaran")
            Else
                Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, dv1(i)("nobukti").ToString, tbukti.Text.Trim, ttgl.EditValue, dv1(i)("kd_toko").ToString, 0, total_t, "Pembayaran (Edit)")
            End If

            ' end insert ---------------------------


        Next

    End Sub

    Private Sub simpan_giro(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        If IsNothing(dtgiro) Then
            Return
        End If

        '' update dulu

        Dim sqlcek As String = String.Format("select * from trbayar2_giro where nobukti='{0}'", tbukti.Text.Trim)
        Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn, sqltrans)
        Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

        While drdcek.Read

            Dim rows() As DataRow = dtgiro.Select(String.Format("nogiro='{0}'", drdcek("nogiro").ToString))

            Dim ada As Boolean = False
            If rows.Length > 1 Then
                ada = True
            End If

            If ada = False Then

                Dim jmlbayargiro As Double = Double.Parse(drdcek("jmlgiro").ToString)

                Dim sqlgiro As String = String.Format("update ms_giro set jumlahpakai=jumlahpakai - {0} where nogiro='{1}'", jmlbayargiro, drdcek("nogiro").ToString)
                Using cmdgiro2 As OleDbCommand = New OleDbCommand(sqlgiro, cn, sqltrans)
                    cmdgiro2.ExecuteNonQuery()
                End Using

                Dim sqlcek_giro As String = String.Format("select jumlahpakai from ms_giro where nogiro='{0}'", drdcek("nogiro").ToString)
                Dim cmdcek_giro As OleDbCommand = New OleDbCommand(sqlcek_giro, cn, sqltrans)
                Dim drcek_giro As OleDbDataReader = cmdcek_giro.ExecuteReader

                If drcek_giro.Read Then

                    If IsNumeric(drcek_giro(0).ToString) Then

                        If drcek_giro(0) <= 0 Then

                            Dim sqlgunagiro As String = String.Format("update ms_giro set sgunakan=0 where nogiro='{0}'", drdcek("nogiro").ToString)
                            Using cmdgunagiro As OleDbCommand = New OleDbCommand(sqlgunagiro, cn, sqltrans)
                                cmdgunagiro.ExecuteNonQuery()
                            End Using

                        End If

                    End If

                End If
                drcek_giro.Close()

                Dim sqldel_giro As String = String.Format("delete from trbayar2_giro where noid={0}", drdcek("noid").ToString)
                Using cmddel_giro As OleDbCommand = New OleDbCommand(sqldel_giro, cn, sqltrans)
                    cmddel_giro.ExecuteNonQuery()
                End Using

            End If
            '' akhir ada
            
        End While
        drdcek.Close()

        '' selesai update


        For i As Integer = 0 To dtgiro.Rows.Count - 1

            Dim jmlgiro As Double = Double.Parse(dtgiro(i)("jmlgiro").ToString)

            Dim sqlins_giro As String = String.Format("insert into trbayar2_giro (nobukti,nobukti_fak,nogiro,jmlgiro) values('{0}','{1}','{2}',{3})", tbukti.Text.Trim, dtgiro(i)("nobukti_fak").ToString, dtgiro(i)("nogiro").ToString, jmlgiro)


            Dim sqlc As String = String.Format("select a.* from trbayar2_giro a where a.nobukti='{0}' and a.nobukti_fak='{1}' and a.nogiro='{2}'", tbukti.Text.Trim, dtgiro(i)("nobukti_fak").ToString, dtgiro(i)("nogiro").ToString)
            Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
            Dim drc As OleDbDataReader = cmdc.ExecuteReader

            Dim apakahada As Boolean = False
            Dim jmlgiroada As Double = 0
            Dim noidada As Integer = 0

            If drc.Read Then
                If IsNumeric(drc("noid")) Then
                    noidada = drc("noid").ToString
                    apakahada = True
                    jmlgiroada = Double.Parse(drc("jmlgiro").ToString)
                End If
            End If
            drc.Close()

            If apakahada = False Then

                Using cmdin_giro As OleDbCommand = New OleDbCommand(sqlins_giro, cn, sqltrans)
                    cmdin_giro.ExecuteNonQuery()
                End Using

                Dim sqlgiro2 As String = String.Format("update ms_giro set sgunakan=1,jumlahpakai=jumlahpakai + {0} where nogiro='{1}'", jmlgiro, dtgiro(i)("nogiro").ToString)
                Using cmdgiro As OleDbCommand = New OleDbCommand(sqlgiro2, cn, sqltrans)
                    cmdgiro.ExecuteNonQuery()
                End Using

            Else

                Dim sql1 As String = String.Format("update ms_giro set jumlahpakai=jumlahpakai - {0} where nogiro='{1}'", jmlgiroada, dtgiro(i)("nogiro").ToString)
                Using cmd1 As OleDbCommand = New OleDbCommand(sql1, cn, sqltrans)
                    cmd1.ExecuteNonQuery()
                End Using

                Dim sql2 As String = String.Format("update ms_giro set jumlahpakai=jumlahpakai + {0} where nogiro='{1}'", jmlgiro, dtgiro(i)("nogiro").ToString)
                Using cmd2 As OleDbCommand = New OleDbCommand(sql2, cn, sqltrans)
                    cmd2.ExecuteNonQuery()
                End Using

                Dim sqlupgiro As String = String.Format("update trbayar2_giro set jmlgiro={0} where noid={1}", jmlgiro, noidada)
                Using cmdupgiro As OleDbCommand = New OleDbCommand(sqlupgiro, cn, sqltrans)
                    cmdupgiro.ExecuteNonQuery()
                End Using

            End If


        Next

    End Sub

    Private Sub simpan_retur(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        If IsNothing(dtretur) Then
            Return
        End If

        '' update dulu

        Dim sqlcek As String = String.Format("select * from trbayar2_ret where nobukti='{0}'", tbukti.Text.Trim)
        Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn, sqltrans)
        Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

        While drdcek.Read

            Dim rows() As DataRow = dtretur.Select(String.Format("nobukti_ret='{0}'", drdcek("nobukti_ret").ToString))

            Dim ada As Boolean = False
            If rows.Length > 1 Then
                ada = True
            End If

            If ada = False Then

                Dim jmlbayar_retur As Double = Double.Parse(drdcek("jmlretur").ToString)

                Dim sqlup_retur As String = String.Format("update trretur set jmlpotong=jmlpotong - {0} where nobukti='{1}'", jmlbayar_retur, drdcek("nobukti_ret").ToString)
                Using cmdup_retur As OleDbCommand = New OleDbCommand(sqlup_retur, cn, sqltrans)
                    cmdup_retur.ExecuteNonQuery()
                End Using

                Dim sqlcek_retur As String = String.Format("select jmlpotong from trretur where nobukti='{0}'", drdcek("nobukti_ret").ToString)
                Dim cmdcek_retur As OleDbCommand = New OleDbCommand(sqlcek_retur, cn, sqltrans)
                Dim drcek_retur As OleDbDataReader = cmdcek_retur.ExecuteReader

                If drcek_retur.Read Then

                    If IsNumeric(drcek_retur(0).ToString) Then

                        If drcek_retur(0) <= 0 Then

                            Dim sqlgunaretur As String = String.Format("update trretur set spotong=0 where nobukti='{0}'", drdcek("nobukti_ret").ToString)
                            Using cmdgunaretur As OleDbCommand = New OleDbCommand(sqlgunaretur, cn, sqltrans)
                                cmdgunaretur.ExecuteNonQuery()
                            End Using

                        End If

                    End If

                End If
                drcek_retur.Close()

                Dim sqldel_ret As String = String.Format("delete from trbayar2_ret where noid={0}", drdcek("noid").ToString)
                Using cmddel_rel As OleDbCommand = New OleDbCommand(sqldel_ret, cn, sqltrans)
                    cmddel_rel.ExecuteNonQuery()
                End Using

            End If

        End While
        drdcek.Close()

        '' akhir update


        For i As Integer = 0 To dtretur.Rows.Count - 1

            Dim jmlgiro As Double = Double.Parse(dtretur(i)("jmlretur").ToString)

            Dim sqlins_retur As String = String.Format("insert into trbayar2_ret (nobukti,nobukti_fak,nobukti_ret,jmlretur) values('{0}','{1}','{2}',{3})", tbukti.Text.Trim, dtretur(i)("nobukti_fak").ToString, dtretur(i)("nobukti_ret").ToString, jmlgiro)

            Dim sqlc As String = String.Format("select a.* from trbayar2_ret a where a.nobukti='{0}' and a.nobukti_fak='{1}' and a.nobukti_ret='{2}'", tbukti.Text.Trim, dtretur(i)("nobukti_fak").ToString, dtretur(i)("nobukti_ret").ToString)
            Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
            Dim drc As OleDbDataReader = cmdc.ExecuteReader

            Dim apakahada As Boolean = False
            Dim jumlahada As Double = 0
            If drc.Read Then
                If IsNumeric(drc("noid")) Then
                    apakahada = True
                    jumlahada = Double.Parse(drc("jmlretur").ToString)
                End If
            End If
            drc.Close()

            If apakahada = False Then

                Using cmdins As OleDbCommand = New OleDbCommand(sqlins_retur, cn, sqltrans)
                    cmdins.ExecuteNonQuery()
                End Using

                Dim sqlgiro2 As String = String.Format("update trretur set spotong=1,jmlpotong=jmlpotong + {0} where nobukti='{1}'", jmlgiro, dtretur(i)("nobukti_ret").ToString)
                Using cmdgiro As OleDbCommand = New OleDbCommand(sqlgiro2, cn, sqltrans)
                    cmdgiro.ExecuteNonQuery()
                End Using

            Else

                Dim sqlgiro As String = String.Format("update trretur set jmlpotong=jmlpotong - {0} where nobukti='{1}'", jumlahada, dtretur(i)("nobukti_ret").ToString)
                Using cmdgiro As OleDbCommand = New OleDbCommand(sqlgiro, cn, sqltrans)
                    cmdgiro.ExecuteNonQuery()
                End Using

                Dim sqlgiro2 As String = String.Format("update trretur set spotong=1,jmlpotong=jmlpotong + {0} where nobukti='{1}'", jmlgiro, dtretur(i)("nobukti_ret").ToString)
                Using cmdgiro As OleDbCommand = New OleDbCommand(sqlgiro2, cn, sqltrans)
                    cmdgiro.ExecuteNonQuery()
                End Using

                Dim sqlbayargiro As String = String.Format("update trbayar2_ret set jmlretur={0} where nobukti='{1}' and nobukti_fak='{2}' and nobukti_ret='{3}'", jmlgiro, tbukti.Text.Trim, dtretur(i)("nobukti_fak").ToString, dtretur(i)("nobukti_ret").ToString)
                Using cmdbayargiro As OleDbCommand = New OleDbCommand(sqlbayargiro, cn, sqltrans)
                    cmdbayargiro.ExecuteNonQuery()
                End Using

            End If

        Next

    End Sub

    Private Sub simpan_kelebihan(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        If IsNothing(dtkelebihan) Then
            Return
        End If

        '' update dulu

        Dim sqlcek As String = String.Format("select * from trbayar2_kelebihan where nobukti='{0}'", tbukti.Text.Trim)
        Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn, sqltrans)
        Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

        Dim apaada As Boolean = False

        While drdcek.Read

            apaada = True

            Dim jmlbayar_pot As Double = Double.Parse(drdcek("jumlah").ToString)

            Dim sqlup_retur As String = String.Format("update trfaktur_to set jmlkelebihan_pot=jmlkelebihan_pot - {0} where nobukti='{1}'", jmlbayar_pot, drdcek("nobukti_pot").ToString)
            Using cmdup_retur As OleDbCommand = New OleDbCommand(sqlup_retur, cn, sqltrans)
                cmdup_retur.ExecuteNonQuery()
            End Using

        End While
        drdcek.Close()

        If apaada Then

            Dim sqldel_lebih As String = String.Format("delete from trbayar2_kelebihan where nobukti='{0}'", tbukti.Text.Trim)
            Using cmddel_lbih As OleDbCommand = New OleDbCommand(sqldel_lebih, cn, sqltrans)
                cmddel_lbih.ExecuteNonQuery()
            End Using

        End If

        '' sampe sini update

        For i As Integer = 0 To dtkelebihan.Rows.Count - 1

            Dim jumlahkel As Double = Double.Parse(dtkelebihan(i)("jumlah").ToString)

            Dim sqlins_retur As String = String.Format("insert into trbayar2_kelebihan (nobukti,nobukti_fak,nobukti_pot,jumlah) values('{0}','{1}','{2}',{3})", tbukti.Text.Trim, dtkelebihan(i)("nobukti_fak").ToString, dtkelebihan(i)("nobukti_pot").ToString, jumlahkel)

            Using cmdins As OleDbCommand = New OleDbCommand(sqlins_retur, cn, sqltrans)
                cmdins.ExecuteNonQuery()
            End Using

            Dim sqlgiro2 As String = String.Format("update trfaktur_to set jmlkelebihan_pot=jmlkelebihan_pot + {0} where nobukti='{1}'", jumlahkel, dtkelebihan(i)("nobukti_pot").ToString)
            Using cmdgiro As OleDbCommand = New OleDbCommand(sqlgiro2, cn, sqltrans)
                cmdgiro.ExecuteNonQuery()
            End Using

            'Dim sqlc As String = String.Format("select a.* from trbayar2_kelebihan a where a.nobukti='{0}' and a.nobukti_fak='{1}'", tbukti.Text.Trim, dtkelebihan(i)("nobukti_fak").ToString)
            'Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
            'Dim drc As OleDbDataReader = cmdc.ExecuteReader

            'If drc.Read Then
            '    If IsNumeric(drc("noid")) Then

            '        '  Dim jumlah As Double = Double.Parse(drc("jumlah").ToString)
            '        Dim jumlahpot As Double = Double.Parse(drc("jumlah").ToString)

            '        Dim sqlgiro As String = String.Format("update trfaktur_to set jmlkelebihan_pot=jmlkelebihan_pot - {0} where nobukti='{1}'", jumlahpot, dtkelebihan(i)("nobukti_pot").ToString)
            '        Using cmdgiro As OleDbCommand = New OleDbCommand(sqlgiro, cn, sqltrans)
            '            cmdgiro.ExecuteNonQuery()
            '        End Using

            '        Dim sqlbayargiro As String = String.Format("update trbayar2_kelebihan set jumlah={0} where nobukti='{1}' and nobukti_fak='{2}'", jumlahkel, tbukti.Text.Trim, dtkelebihan(i)("nobukti_fak").ToString)
            '        Using cmdbayargiro As OleDbCommand = New OleDbCommand(sqlbayargiro, cn, sqltrans)
            '            cmdbayargiro.ExecuteNonQuery()
            '        End Using

            'Dim sqlupbayar2 As String = String.Format("update trbayar2 set jmlkelebihan_pot = jmlkelebihan_pot - {0} where nobukti='{1}' and nobukti_fak='{2}'", jumlahpot, tbukti.Text.Trim, dtkelebihan(i)("nobukti_pot").ToString)
            'Using cmdbayar2 As OleDbCommand = New OleDbCommand(sqlupbayar2, cn, sqltrans)
            '    cmdbayar2.ExecuteNonQuery()
            'End Using

            'Else

            '    End If

            'Else

            'Using cmdins As OleDbCommand = New OleDbCommand(sqlins_retur, cn, sqltrans)
            '    cmdins.ExecuteNonQuery()
            'End Using

            'End If
            'drc.Close()

            'Dim sqlupbayar22 As String = String.Format("update trbayar2 set jmlkelebihan_pot = jmlkelebihan_pot + {0} where nobukti='{1}' and nobukti_fak='{2}'", jumlahkel, tbukti.Text.Trim, dtkelebihan(i)("nobukti_pot").ToString)
            'Using cmdbayar2 As OleDbCommand = New OleDbCommand(sqlupbayar22, cn, sqltrans)
            '    cmdbayar2.ExecuteNonQuery()
            'End Using

        Next

    End Sub

    Private Function simpan_kemb(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim hasil As String = "ok"

        If IsNothing(dv_kemb) Then
            GoTo langsung_aja
        End If

        '' rubah dulu
        Dim sqlcek As String = String.Format("select * from trbayar2_kemb where nobukti='{0}'", tbukti.Text.Trim)
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

        Dim sqldel2 As String = String.Format("delete from trbayar2_kemb where nobukti='{0}'", tbukti.Text.Trim)
        Using cmddel2 As OleDbCommand = New OleDbCommand(sqldel2, cn, sqltrans)
            cmddel2.ExecuteNonQuery()
        End Using

        '' akhir rubah dulu

        For i As Integer = 0 To dv_kemb.Count - 1

            Dim nobukti_fak As String = dv_kemb(i)("nobukti").ToString
            Dim kd_karyawn As String = dv_kemb(i)("kd_karyawan").ToString
            Dim jmlfak As Double = Double.Parse(dv_kemb(i)("jmlfak").ToString)
            Dim jmlbayar As Double = Double.Parse(dv_kemb(i)("jmlbayar").ToString)

            If jmlbayar <= 0 Then
                hasil = "Jumlah pengembalian tidak boleh <=0 (" & nobukti_fak & ")"
                GoTo langsung_aja
            End If

            If jmlbayar > jmlfak Then
                hasil = "Jml pengembalian lebih dari yang seharusnya (" & nobukti_fak & ")"
                GoTo langsung_aja
            End If

            If kd_karyawn.Trim.Length = 0 Then
                hasil = "Kolektor harus diisi (" & nobukti_fak & ")"
                GoTo langsung_aja
            End If

                Dim sqlins As String = String.Format("insert into trbayar2_kemb (nobukti,nobukti_fak,kd_karyawan,jmlfak,jmlbayar) values('{0}','{1}','{2}',{3},{4})", _
                                                     tbukti.Text.Trim, nobukti_fak, kd_karyawn, jmlfak, jmlbayar)
                Using cmdins As OleDbCommand = New OleDbCommand(sqlins, cn, sqltrans)
                    cmdins.ExecuteNonQuery()
                End Using

                Dim sqlfak As String = String.Format("update trfaktur_to set jmlkelebihan_pot=jmlkelebihan_pot+{0} where nobukti='{1}'", jmlbayar, nobukti_fak)
                Using cmdfak As OleDbCommand = New OleDbCommand(sqlfak, cn, sqltrans)
                    cmdfak.ExecuteNonQuery()
                End Using

        Next

langsung_aja:

        Return hasil

    End Function

    Private Sub insertview()

        Dim total As Double = GridView3.Columns("jumlah").SummaryItem.SummaryValue

        Dim orow As DataRowView = dv.AddNew
        orow("nobukti") = tbukti.Text.Trim
        orow("tanggal") = ttgl.Text.Trim
        orow("tglbayar") = ttgl_tgh.Text.Trim
        orow("total") = total
        orow("note") = tnote.Text.Trim
        orow("sbatal") = 0

        dv.EndInit()

    End Sub

    Private Sub updateview()

        Dim total As Double = GridView3.Columns("jumlah").SummaryItem.SummaryValue

        dv(position)("nobukti") = tbukti.Text.Trim
        dv(position)("tanggal") = ttgl.Text.Trim
        dv(position)("tglbayar") = ttgl_tgh.Text.Trim
        dv(position)("total") = total
        dv(position)("note") = tnote.Text.Trim

    End Sub

    Private Sub isi_rsales_kemb()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select kd_karyawan,nama_karyawan from ms_pegawai where aktif=1 and bagian='SALES' order by kd_karyawan asc"
            Dim ds As DataSet = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dv_saleskemb = dvm.CreateDataView(ds.Tables(0))

            Dim orow As DataRow = dv_saleskemb.Table.NewRow
            orow("kd_karyawan") = ""
            orow("nama_karyawan") = ""
            dv_saleskemb.Table.Rows.InsertAt(orow, 0)

            rsales.DataSource = dv_saleskemb

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

        If IsNothing(dv1) And IsNothing(dv_kemb) Then
            MsgBox("Tidak ada faktur yang diproses..", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If dv1.Count <= 0 And dv_kemb.Count <= 0 Then
            MsgBox("Tidak ada faktur yang diproses..", vbOKOnly + vbInformation, "Informasi")
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

        ttgl.EditValue = Date.Now
        ttgl_tgh.EditValue = Date.Now

        isi_rsales_kemb()

        If addstat Then
            kosongkan()

            tjfaktur.SelectedIndex = 1

        Else
            isi()

            btload.Enabled = False

            btadd_daf.Enabled = False
            btdel_daf.Enabled = False

            btadd_sal.Enabled = False
            btdel_sal.Enabled = False

            btadd_outl.Enabled = False
            btdel_outl.Enabled = False

            tjfaktur.Enabled = False

        End If

    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click
        LoadData()
    End Sub

    ' option sales

    Private Sub btadd_sal_Click(sender As System.Object, e As System.EventArgs) Handles btadd_sal.Click

        Dim fs As New fdtg_sal With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .dv = dv_sal}
        fs.ShowDialog(Me)

    End Sub

    Private Sub btdel_sal_Click(sender As System.Object, e As System.EventArgs) Handles btdel_sal.Click

        If IsNothing(dv_sal) Then
            Return
        End If

        If dv_sal.Count <= 0 Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim kdsal As String = dv_sal(Me.BindingContext(dv_sal).Position)("kd_karyawan").ToString

            Dim sql As String = String.Format("delete from trbayar_sles where nobukti='{0}' and kd_karyawan='{1}'", tbukti.Text.Trim, kdsal)
            Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()
            End Using

            sqltrans.Commit()

            dv_sal.Delete(Me.BindingContext(dv_sal).Position)

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

    ' -----------------------------------------

    ' option outlet

    Private Sub btadd_outl_Click(sender As System.Object, e As System.EventArgs) Handles btadd_outl.Click

        Dim fs As New fdtg_outl With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .dv = dv_tko}
        fs.ShowDialog(Me)

    End Sub

    Private Sub btdel_outl_Click(sender As System.Object, e As System.EventArgs) Handles btdel_outl.Click

        If IsNothing(dv_tko) Then
            Return
        End If

        If dv_tko.Count <= 0 Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim kdord As String = dv_tko(Me.BindingContext(dv_tko).Position)("kd_toko").ToString

            Dim sql As String = String.Format("delete from trbayar_tko where nobukti='{0}' and kd_toko='{1}'", tbukti.Text.Trim, kdord)
            Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()
            End Using

            sqltrans.Commit()

            dv_tko.Delete(Me.BindingContext(dv_tko).Position)

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

    ' -----------------------------------------

    ' option dtg

    Private Sub btadd_daf_Click(sender As System.Object, e As System.EventArgs) Handles btadd_daf.Click

        Dim fs As New fbayar_dtg With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .dvdtg = dv_dtg}
        fs.ShowDialog(Me)

    End Sub

    Private Sub btdel_daf_Click(sender As System.Object, e As System.EventArgs) Handles btdel_daf.Click

        If IsNothing(dv_dtg) Then
            Return
        End If

        If dv_dtg.Count <= 0 Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim kdord As String = dv_dtg(Me.BindingContext(dv_dtg).Position)("nobukti").ToString

            Dim sql As String = String.Format("delete from trbayar_dtg where nobukti='{0}' and nobukti_dtg='{1}'", tbukti.Text.Trim, kdord)
            Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()
            End Using

            sqltrans.Commit()

            dv_dtg.Delete(Me.BindingContext(dv_dtg).Position)

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

    Private Sub btdel_Click(sender As System.Object, e As System.EventArgs) Handles btdel.Click

        If statview = True Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            Dim tanggal As String = dv1(Me.BindingContext(dv1).Position)("tanggal").ToString
            Dim nobukti_fak As String = dv1(Me.BindingContext(dv1).Position)("nobukti").ToString
            Dim kdtoko As String = dv1(Me.BindingContext(dv1).Position)("kd_toko").ToString
            Dim kd_karyawan As String = dv1(Me.BindingContext(dv1).Position)("kd_karyawan").ToString


            Dim jumlah As Double


            Dim sqlc As String = String.Format("select noid,jumlah,jmltunai,jmlgiro,jmlretur,pembulatan from trbayar2 where nobukti='{0}' and nobukti_fak='{1}'", tbukti.Text.Trim, nobukti_fak)
            Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
            Dim drc As OleDbDataReader = cmdc.ExecuteReader

            If drc.Read Then

                If IsNumeric(drc(0).ToString) Then

                    jumlah = Double.Parse(drc(1).ToString)

                    Dim jmltunai As Double = Double.Parse(drc("jmltunai").ToString)
                    Dim jmlgiro As Double = Double.Parse(drc("jmlgiro").ToString)
                    Dim jmlretur As Double = Double.Parse(drc("jmlretur").ToString)
                    Dim pembulatan As Double = Double.Parse(drc("pembulatan").ToString)

                    Dim total_up As Double = jmltunai + jmlretur + pembulatan

                    ' update toko

                    Dim sqltokoup As String = String.Format("update ms_toko set piutangbeli=piutangbeli + {0} where kd_toko='{1}'", Replace(total_up, ",", "."), kdtoko)
                    Using cmdtoko As OleDbCommand = New OleDbCommand(sqltokoup, cn, sqltrans)
                        cmdtoko.ExecuteNonQuery()
                    End Using

                    Dim sqltoko2_up As String = String.Format("update ms_toko2 set jmlpiutang=jmlpiutang + {0} where kd_toko='{1}' and kd_karyawan='{2}'", Replace(total_up, ",", "."), kdtoko, kd_karyawan)
                    Using cmdtoko2 As OleDbCommand = New OleDbCommand(sqltoko2_up, cn, sqltrans)
                        cmdtoko2.ExecuteNonQuery()
                    End Using


                    If DateValue(tgl_old) <> DateValue(ttgl.EditValue) Then
                        Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, nobukti_fak, tbukti.Text.Trim, tgl_old, kdtoko, total_up, 0, "Pembayaran (Edit)")
                        Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, nobukti_fak, tbukti.Text.Trim, tgl_old, kdtoko, 0, total_up, "Pembayaran (Edit)")
                    End If

                    Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, nobukti_fak, tbukti.Text.Trim, ttgl.EditValue, kdtoko, total_up, 0, "Pembayaran (Edit)")

                    ' update faktur to

                    Dim sqlfak_up As String = String.Format("update trfaktur_to set jmlbayar=jmlbayar- {0},jmlgiro=jmlgiro- {1} where nobukti='{2}'", Replace(total_up, ",", "."), jmlgiro, nobukti_fak)
                    Using cmdfak As OleDbCommand = New OleDbCommand(sqlfak_up, cn, sqltrans)
                        cmdfak.ExecuteNonQuery()
                    End Using

                    ' delete giro bayar

                    Dim sql_giro As String = String.Format("select * from trbayar2_giro where nobukti='{0}' and nobukti_fak='{1}'", tbukti.Text.Trim, nobukti_fak)
                    Dim cmdgiro As OleDbCommand = New OleDbCommand(sql_giro, cn, sqltrans)
                    Dim drgiro As OleDbDataReader = cmdgiro.ExecuteReader

                    If drgiro.HasRows Then

                        While drgiro.Read

                            Dim jmlbayargiro As Double = Double.Parse(drgiro("jmlgiro").ToString)

                            Dim sqlgiro As String = String.Format("update ms_giro set jumlahpakai=jumlahpakai - {0} where nogiro='{1}'", jmlbayargiro, drgiro("nogiro").ToString)
                            Using cmdgiro2 As OleDbCommand = New OleDbCommand(sqlgiro, cn, sqltrans)
                                cmdgiro2.ExecuteNonQuery()
                            End Using

                            Dim sqlcek_giro As String = String.Format("select jumlahpakai from ms_giro where nogiro='{0}'", drgiro("nogiro").ToString)
                            Dim cmdcek_giro As OleDbCommand = New OleDbCommand(sqlcek_giro, cn, sqltrans)
                            Dim drcek_giro As OleDbDataReader = cmdcek_giro.ExecuteReader

                            If drcek_giro.Read Then

                                If IsNumeric(drcek_giro(0).ToString) Then

                                    If drcek_giro(0) <= 0 Then

                                        Dim sqlgunagiro As String = String.Format("update ms_giro set sgunakan=0 where nogiro='{0}'", drgiro("nogiro").ToString)
                                        Using cmdgunagiro As OleDbCommand = New OleDbCommand(sqlgunagiro, cn, sqltrans)
                                            cmdgunagiro.ExecuteNonQuery()
                                        End Using

                                    End If

                                End If

                            End If
                            drcek_giro.Close()

                        End While

                    End If

                    drgiro.Close()

                    For i As Integer = 0 To dtgiro.Rows.Count - 1

                        If dtgiro(i)("nobukti_fak").ToString.Equals(nobukti_fak) Then
                            dtgiro.Rows.RemoveAt(i)
                        End If

                    Next

                    ' ------------------- akhir delete giro


                    ' delete retur bayar

                    Dim sql_retur As String = String.Format("select * from trbayar2_ret where nobukti='{0}' and nobukti_fak='{1}'", tbukti.Text.Trim, nobukti_fak)
                    Dim cmdretur As OleDbCommand = New OleDbCommand(sql_retur, cn, sqltrans)
                    Dim drretur As OleDbDataReader = cmdretur.ExecuteReader

                    If drretur.HasRows Then

                        While drretur.Read

                            Dim jmlbayar_retur As Double = Double.Parse(drretur("jmlretur").ToString)

                            Dim sqlup_retur As String = String.Format("update trretur set jmlpotong=jmlpotong - {0} where nobukti='{1}'", jmlbayar_retur, dtretur("nobukti_ret").ToString)
                            Using cmdup_retur As OleDbCommand = New OleDbCommand(sqlup_retur, cn, sqltrans)
                                cmdup_retur.ExecuteNonQuery()
                            End Using

                            Dim sqlcek_retur As String = String.Format("select jmlpotong from trretur where nobukti='{0}'", dtretur("nobukti_ret").ToString)
                            Dim cmdcek_retur As OleDbCommand = New OleDbCommand(sqlcek_retur, cn, sqltrans)
                            Dim drcek_retur As OleDbDataReader = cmdcek_retur.ExecuteReader

                            If drcek_retur.Read Then

                                If IsNumeric(drcek_retur(0).ToString) Then

                                    If drcek_retur(0) <= 0 Then

                                        Dim sqlgunaretur As String = String.Format("update trretur set spotong=0 where nobukti='{0}'", dtretur("nobukti_ret").ToString)
                                        Using cmdgunaretur As OleDbCommand = New OleDbCommand(sqlgunaretur, cn, sqltrans)
                                            cmdgunaretur.ExecuteNonQuery()
                                        End Using

                                    End If

                                End If

                            End If
                            drcek_retur.Close()

                        End While

                    End If

                    For i As Integer = 0 To dtretur.Rows.Count - 1

                        If dtretur(i)("nobukti_fak").ToString.Equals(nobukti_fak) Then
                            dtretur.Rows.RemoveAt(i)
                        End If

                    Next

                    ' ------------------- akhir delete retur bayar

                    ' delete kelebihan bayar

                    Dim sql_kelebihan As String = String.Format("select * from trbayar2_kelebihan where nobukti='{0}' and nobukti_fak='{1}'", tbukti.Text.Trim, nobukti_fak)
                    Dim cmdkelebihan As OleDbCommand = New OleDbCommand(sql_kelebihan, cn, sqltrans)
                    Dim dtlebih As OleDbDataReader = cmdkelebihan.ExecuteReader

                    If dtlebih.HasRows Then

                        While dtlebih.Read

                            Dim jmlbayar_lebih As Double = Double.Parse(dtlebih("jumlah").ToString)

                            Dim sqlup_retur As String = String.Format("update trfaktur_to set jmlkelebihan_pot=jmlkelebihan_pot - {0} where nobukti='{1}'", jmlbayar_lebih, dtlebih("nobukti_pot").ToString)
                            Using cmdup_retur As OleDbCommand = New OleDbCommand(sqlup_retur, cn, sqltrans)
                                cmdup_retur.ExecuteNonQuery()
                            End Using

                            'Dim sqluppot2 As String = String.Format("update trbayar2 set jmlkelebihan_pot=jmlkelebihan_pot  - {0} where nobukti='{0}' and nobukti_fak='{1}'", jmlbayar_lebih, tbukti.Text.Trim, dtlebih("nobukti_pot").ToString)
                            'Using cmduppot2 As OleDbCommand = New OleDbCommand(sqluppot2, cn, sqltrans)
                            '    cmduppot2.ExecuteNonQuery()
                            'End Using

                        End While

                    End If



                    For i As Integer = 0 To dtkelebihan.Rows.Count - 1

                        If dtkelebihan(i)("nobukti_fak").ToString.Equals(nobukti_fak) Then
                            dtkelebihan.Rows.RemoveAt(i)
                        End If

                    Next

                    Dim jmlkelebihan_byr As String = Double.Parse(dv1(Me.BindingContext(dv1).Position)("jmlkelebihan").ToString)

                    Dim sqlup_kelebihanfak As String = String.Format("update trfaktur_to set jmlkelebihan=jmlkelebihan - {0} where nobukti='{1}'", jmlkelebihan_byr, nobukti_fak)
                    Using cmdlebih As OleDbCommand = New OleDbCommand(sqlup_kelebihanfak, cn, sqltrans)
                        cmdlebih.ExecuteNonQuery()
                    End Using

                    ' ------------------- akhir delete kelebihan bayar

                    ' delete bayar

                    Dim sql2 As String = String.Format("delete from trbayar2 where nobukti='{0}' and nobukti_fak='{1}'", tbukti.Text.Trim, nobukti_fak)
                    Using cmd2 As OleDbCommand = New OleDbCommand(sql2, cn, sqltrans)
                        cmd2.ExecuteNonQuery()
                    End Using

                End If

            Else

                ' giro
                For i As Integer = 0 To dtgiro.Rows.Count - 1

                    If dtgiro(i)("nobukti_fak").ToString.Equals(nobukti_fak) Then
                        dtgiro.Rows.RemoveAt(i)
                    End If

                Next
                '' akhir giro

                ' retur bayar
                For i As Integer = 0 To dtretur.Rows.Count - 1

                    If dtretur(i)("nobukti_fak").ToString.Equals(nobukti_fak) Then
                        dtretur.Rows.RemoveAt(i)
                    End If

                Next
                '' akhir retur bayar

                ' kelebihan bayar
                For i As Integer = 0 To dtkelebihan.Rows.Count - 1

                    If dtkelebihan(i)("nobukti_fak").ToString.Equals(nobukti_fak) Then
                        dtkelebihan.Rows.RemoveAt(i)
                    End If
                Next
                '' akhir kelebihan bayar

            End If

            dv1.Delete(Me.BindingContext(dv1).Position)
            sqltrans.Commit()

            drc.Close()


        Catch ex As Exception

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

    Private Sub GridView3_DoubleClick(sender As Object, e As System.EventArgs) Handles GridView3.DoubleClick

        If GridView3.FocusedColumn.FieldName = "jmlgiro" Then

            'If GridView3.IsNewItemRow(GridView3.FocusedRowHandle) Then
            '    Return
            'End If

            If dv1(Me.BindingContext(dv1).Position)("nobukti").ToString.Equals("") Then
                Return
            End If

            Dim fs As New fbayar2_giro With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .dt = dtgiro, .dv = dv1, .posisi = Me.BindingContext(dv1).Position, .nobukti = tbukti.Text.Trim, .statview = statview}
            fs.ShowDialog(Me)

            dv1(Me.BindingContext(dv1).Position)("jmlgiro") = fs.get_jmlgiro

            kalkulasi()

        ElseIf GridView3.FocusedColumn.FieldName = "jmlretur" Then

            'If GridView3.IsNewItemRow(GridView3.FocusedRowHandle) Then
            '    Return
            'End If

            If dv1(Me.BindingContext(dv1).Position)("nobukti").ToString.Equals("") Then
                Return
            End If

            Dim fs As New fbayar2_retur With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .dt = dtretur, .dv = dv1, .posisi = Me.BindingContext(dv1).Position, .nobukti = tbukti.Text.Trim, .statview = statview}
            fs.ShowDialog(Me)

            dv1(Me.BindingContext(dv1).Position)("jmlretur") = fs.get_jmlgiro

            kalkulasi()

        ElseIf GridView3.FocusedColumn.FieldName = "jmlkelebihan_dr" Then

            'If GridView3.IsNewItemRow(GridView3.FocusedRowHandle) Then
            '    Return
            'End If

            If dv1(Me.BindingContext(dv1).Position)("nobukti").ToString.Equals("") Then
                Return
            End If

            Dim fs As New fbayar2_lebih With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .dt = dtkelebihan, .dv = dv1, .posisi = Me.BindingContext(dv1).Position, .nobukti = tbukti.Text.Trim, .statview = statview}
            fs.ShowDialog(Me)

            dv1(Me.BindingContext(dv1).Position)("jmlkelebihan_dr") = fs.get_jmlgiro

            kalkulasi()


        End If


    End Sub

    Private Sub GridView3_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles GridView3.KeyDown

        If e.KeyCode = Keys.Delete Then

            btdel_Click(sender, Nothing)

        ElseIf e.KeyCode = Keys.F4 Then

            If GridView3.FocusedColumn.FieldName = "jmlgiro" Then

                If GridView3.IsNewItemRow(GridView3.FocusedRowHandle) Then
                    Return
                End If

                If dv1(Me.BindingContext(dv1).Position)("nobukti").ToString.Equals("") Then
                    Return
                End If

                Dim fs As New fbayar2_giro With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .dt = dtgiro, .dv = dv1, .posisi = Me.BindingContext(dv1).Position, .nobukti = tbukti.Text.Trim, .statview = statview}
                fs.ShowDialog(Me)

                dv1(Me.BindingContext(dv1).Position)("jmlgiro") = fs.get_jmlgiro

                kalkulasi()

            ElseIf GridView3.FocusedColumn.FieldName = "jmlretur" Then

                If GridView3.IsNewItemRow(GridView3.FocusedRowHandle) Then
                    Return
                End If

                If dv1(Me.BindingContext(dv1).Position)("nobukti").ToString.Equals("") Then
                    Return
                End If

                Dim fs As New fbayar2_retur With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .dt = dtretur, .dv = dv1, .posisi = Me.BindingContext(dv1).Position, .nobukti = tbukti.Text.Trim, .statview = statview}
                fs.ShowDialog(Me)

                dv1(Me.BindingContext(dv1).Position)("jmlretur") = fs.get_jmlgiro

                kalkulasi()

            ElseIf GridView3.FocusedColumn.FieldName = "jmlkelebihan_dr" Then

                If GridView3.IsNewItemRow(GridView3.FocusedRowHandle) Then
                    Return
                End If

                If dv1(Me.BindingContext(dv1).Position)("nobukti").ToString.Equals("") Then
                    Return
                End If

                Dim fs As New fbayar2_lebih With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .dt = dtkelebihan, .dv = dv1, .posisi = Me.BindingContext(dv1).Position, .nobukti = tbukti.Text.Trim, .statview = statview}
                fs.ShowDialog(Me)

                dv1(Me.BindingContext(dv1).Position)("jmlkelebihan_dr") = fs.get_jmlgiro

                kalkulasi()

            End If


        End If

    End Sub

    Private Sub GridView3_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView3.CellValueChanged

        If e.Column.FieldName.Equals("jmltunai") Then
            kalkulasi()
        ElseIf e.Column.FieldName.Equals("jmltrans") Then
            kalkulasi()
        ElseIf e.Column.FieldName.Equals("jmlgiro") Then
            kalkulasi()
        ElseIf e.Column.FieldName.Equals("jmlretur") Then
            kalkulasi()
        ElseIf e.Column.FieldName.Equals("pembulatan") Then
            kalkulasi()
        ElseIf e.Column.FieldName.Equals("disc_susulan") Then
            kalkulasi()
        ElseIf e.Column.FieldName.Equals("note") Then
            kalkulasi()
        ElseIf e.Column.FieldName.Equals("nobukti") Then

            cek_nobukti_in_grid(e.Value, False)


        ElseIf e.Column.FieldName.Equals("no_nota") Then

            cek_nobukti_in_grid(e.Value, True)

        Else
            dv1(Me.BindingContext(dv1).Position)("jumlah") = 0
            Return
        End If


    End Sub

    Private Sub cek_nobukti_in_grid(ByVal nobukti As String, ByVal isnota As Boolean)

        Dim sql As String = "SELECT trfaktur_to.nobukti,trfaktur_to.no_nota, trfaktur_to.tanggal, trfaktur_to.netto,(trfaktur_to.netto - (trfaktur_to.jmlbayar + trfaktur_to.jmlgiro + trfaktur_to.jmlgiro_real)) as sisapiutang, " & _
                "ms_toko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko,ms_toko.kd_group, trfaktur_to.kd_karyawan ,0 as noid,0 as jmltunai,0 as jmltrans,0 as jmlgiro,0 as jmlretur,0 as pembulatan,0 as jumlah,0 as disc_susulan,0 as jmlkelebihan,0 as jmlkelebihan_dr,'' as note " & _
                "FROM trfaktur_to INNER JOIN ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko " & _
                "WHERE trfaktur_to.sbatal=0 and trfaktur_to.netto > (trfaktur_to.jmlbayar + trfaktur_to.jmlgiro + trfaktur_to.jmlgiro_real) And trfaktur_to.spulang = 1 and trfaktur_to.statkirim='TERKIRIM'"


        If isnota = False Then
            sql = String.Format("{0} and trfaktur_to.nobukti='{1}'", sql, nobukti)
        Else
            sql = String.Format("{0} and trfaktur_to.no_nota='{1}'", sql, nobukti)
        End If




        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then

                If drd("nobukti").ToString.Equals("") Then

                    drd.Close()
                    MsgBox("No Faktur tidak ditemukan", vbOKOnly + vbInformation, "Informasi")

                    dv1.Delete(Me.BindingContext(dv1).Position)
                    Return

                Else

                    Dim dtcari As DataTable = New DataTable
                    dtcari = dv1.ToTable
                    If isnota = False Then
                        Dim rows() As DataRow = dtcari.Select(String.Format("nobukti='{0}'", nobukti))

                        If rows.Length > 1 Then

                            drd.Close()

                            MsgBox("No faktur sudah ada dalam daftar", vbOKOnly + vbInformation, "Informasi")

                            dv1.Delete(Me.BindingContext(dv1).Position)
                            Return

                        End If

                    Else

                        Dim rows() As DataRow = dtcari.Select(String.Format("no_nota='{0}'", nobukti))

                        If rows.Length > 1 Then

                            drd.Close()

                            MsgBox("No-nota sudah ada dalam daftar", vbOKOnly + vbInformation, "Informasi")

                            dv1.Delete(Me.BindingContext(dv1).Position)
                            Return

                        End If

                    End If


                    dv1(Me.BindingContext(dv1).Position)("nobukti") = drd("nobukti").ToString
                    dv1(Me.BindingContext(dv1).Position)("no_nota") = drd("no_nota").ToString
                    dv1(Me.BindingContext(dv1).Position)("tanggal") = drd("tanggal").ToString
                    dv1(Me.BindingContext(dv1).Position)("netto") = drd("netto").ToString
                    dv1(Me.BindingContext(dv1).Position)("sisapiutang") = drd("sisapiutang").ToString

                    dv1(Me.BindingContext(dv1).Position)("kd_toko") = drd("kd_toko").ToString
                    dv1(Me.BindingContext(dv1).Position)("nama_toko") = drd("nama_toko").ToString
                    dv1(Me.BindingContext(dv1).Position)("kd_group") = drd("kd_group").ToString
                    dv1(Me.BindingContext(dv1).Position)("alamat_toko") = drd("alamat_toko").ToString
                    dv1(Me.BindingContext(dv1).Position)("kd_karyawan") = drd("kd_karyawan").ToString
                    dv1(Me.BindingContext(dv1).Position)("noid") = drd("noid").ToString
                    dv1(Me.BindingContext(dv1).Position)("jmltunai") = drd("jmltunai").ToString
                    dv1(Me.BindingContext(dv1).Position)("jmltrans") = drd("jmltrans").ToString
                    dv1(Me.BindingContext(dv1).Position)("jmlgiro") = drd("jmlgiro").ToString
                    dv1(Me.BindingContext(dv1).Position)("jmlretur") = drd("jmlretur").ToString
                    dv1(Me.BindingContext(dv1).Position)("pembulatan") = drd("pembulatan").ToString
                    dv1(Me.BindingContext(dv1).Position)("jumlah") = drd("jumlah").ToString
                    dv1(Me.BindingContext(dv1).Position)("disc_susulan") = drd("disc_susulan").ToString
                    dv1(Me.BindingContext(dv1).Position)("jmlkelebihan") = drd("jmlkelebihan").ToString
                    dv1(Me.BindingContext(dv1).Position)("jmlkelebihan_dr") = drd("jmlkelebihan_dr").ToString
                    dv1(Me.BindingContext(dv1).Position)("note") = drd("note").ToString

                End If

            Else

                drd.Close()

                MsgBox("No Faktur tidak ditemukan", vbOKOnly + vbInformation, "Informasi")

                dv1.Delete(Me.BindingContext(dv1).Position)

            End If

            drd.Close()

        Catch ex As OleDb.OleDbException
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try


        kalkulasi()

    End Sub

    Private Sub kalkulasi()

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count < 1 Then
            Return
        End If

        If dv1(Me.BindingContext(dv1).Position)("tanggal").ToString.Equals("") Then
            Return
        End If

        Dim jmltunai As Double

        If IsNumeric(dv1(Me.BindingContext(dv1).Position)("jmltunai").ToString) Then
            jmltunai = dv1(Me.BindingContext(dv1).Position)("jmltunai").ToString
        Else
            jmltunai = 0
        End If


        Dim jmltransfer As Double

        If IsNumeric(dv1(Me.BindingContext(dv1).Position)("jmltrans").ToString) Then
            jmltransfer = dv1(Me.BindingContext(dv1).Position)("jmltrans").ToString
        Else
            jmltransfer = 0
        End If

        Dim jmlgiro As Double

        If IsNumeric(dv1(Me.BindingContext(dv1).Position)("jmlgiro").ToString) Then
            jmlgiro = dv1(Me.BindingContext(dv1).Position)("jmlgiro").ToString
        Else
            jmlgiro = 0
        End If

        Dim jmlretur As Double

        If IsNumeric(dv1(Me.BindingContext(dv1).Position)("jmlretur").ToString) Then
            jmlretur = dv1(Me.BindingContext(dv1).Position)("jmlretur").ToString
        Else
            jmlretur = 0
        End If

        Dim disc_susul As Double

        If IsNumeric(dv1(Me.BindingContext(dv1).Position)("disc_susulan").ToString) Then
            disc_susul = dv1(Me.BindingContext(dv1).Position)("disc_susulan").ToString
        Else
            disc_susul = 0
        End If

        Dim kelebihan_dr As Double

        If IsNumeric(dv1(Me.BindingContext(dv1).Position)("jmlkelebihan_dr").ToString) Then
            kelebihan_dr = dv1(Me.BindingContext(dv1).Position)("jmlkelebihan_dr").ToString
        Else
            kelebihan_dr = 0
        End If

        Dim pembulatan As Double

        If IsNumeric(dv1(Me.BindingContext(dv1).Position)("pembulatan").ToString) Then
            pembulatan = dv1(Me.BindingContext(dv1).Position)("pembulatan").ToString
        Else
            pembulatan = dv1(Me.BindingContext(dv1).Position)("pembulatan").ToString
        End If

        Dim total As Double = (jmltunai + jmltransfer + jmlgiro + jmlretur + pembulatan + kelebihan_dr + disc_susul)

        Dim sisapiutang As Double

        If IsNumeric(dv1(Me.BindingContext(dv1).Position)("sisapiutang").ToString) Then
            sisapiutang = dv1(Me.BindingContext(dv1).Position)("sisapiutang").ToString
        Else
            sisapiutang = 0
        End If

        Dim hasil As Double = total - sisapiutang

        If hasil > 0 Then
            dv1(Me.BindingContext(dv1).Position)("jmlkelebihan") = hasil
            dv1(Me.BindingContext(dv1).Position)("jumlah") = sisapiutang
            'ElseIf hasil >= 0 And hasil < 500 Then
            '    dv1(Me.BindingContext(dv1).Position)("jmlkelebihan") = 0
            '    dv1(Me.BindingContext(dv1).Position)("pembulatan") = -hasil
            '    dv1(Me.BindingContext(dv1).Position)("jumlah") = sisapiutang
            'Else

            '    dv1(Me.BindingContext(dv1).Position)("jmlkelebihan") = 0
            '    dv1(Me.BindingContext(dv1).Position)("pembulatan") = 0
            '    dv1(Me.BindingContext(dv1).Position)("jumlah") = total
        Else
            dv1(Me.BindingContext(dv1).Position)("jmlkelebihan") = 0
            dv1(Me.BindingContext(dv1).Position)("jumlah") = total
        End If

        hitung_total()

    End Sub

    Private Sub hitung_total()

        Dim jumlah As Double = GridView3.Columns("jumlah").SummaryItem.SummaryValue
        Dim pengembalian As Double = GridView4.Columns("jmlbayar").SummaryItem.SummaryValue

        Dim totalpemb As Double = jumlah - pengembalian

        ttot_byr.EditValue = jumlah
        ttot_kemb.EditValue = pengembalian
        ttot_all.EditValue = totalpemb

    End Sub

    Private Sub cek_nobukti_kemb_grid(ByVal nobukti As String, ByVal isnota As Boolean)

        Dim sql As String = "SELECT trfaktur_to.nobukti, trfaktur_to.no_nota, trfaktur_to.tanggal, ms_toko.kd_toko, ms_toko.nama_toko, " & _
        "ms_toko.alamat_toko, trfaktur_to.jmlkelebihan - trfaktur_to.jmlkelebihan_pot as jmllebih " & _
        "FROM trfaktur_to INNER JOIN ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko " & _
        "where(trfaktur_to.jmlkelebihan - trfaktur_to.jmlkelebihan_pot) > 0 " & _
        "and trfaktur_to.spulang = 1 and trfaktur_to.statkirim='TERKIRIM'"

        If isnota = False Then
            sql = String.Format("{0} and trfaktur_to.nobukti='{1}'", sql, nobukti)
        Else
            sql = String.Format("{0} and trfaktur_to.no_nota='{1}'", sql, nobukti)
        End If

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then

                If drd("nobukti").ToString.Equals("") Then

                    drd.Close()
                    MsgBox("No Faktur tidak ditemukan", vbOKOnly + vbInformation, "Informasi")

                    dv_kemb.Delete(Me.BindingContext(dv_kemb).Position)
                    Return

                Else

                    Dim dtcari As DataTable = New DataTable
                    dtcari = dv_kemb.ToTable
                    If isnota = False Then
                        Dim rows() As DataRow = dtcari.Select(String.Format("nobukti='{0}'", nobukti))

                        If rows.Length > 1 Then

                            drd.Close()

                            MsgBox("No faktur sudah ada dalam daftar", vbOKOnly + vbInformation, "Informasi")

                            dv_kemb.Delete(Me.BindingContext(dv_kemb).Position)
                            Return

                        End If

                    Else

                        Dim rows() As DataRow = dtcari.Select(String.Format("no_nota='{0}'", nobukti))

                        If rows.Length > 1 Then

                            drd.Close()

                            MsgBox("No-nota sudah ada dalam daftar", vbOKOnly + vbInformation, "Informasi")

                            dv_kemb.Delete(Me.BindingContext(dv_kemb).Position)
                            Return

                        End If

                    End If

                    Dim kd_kolektor As String = ""
                    If Not IsNothing(dv_dtg) Then
                        If Not dv_dtg.Count <= 0 Then
                            kd_kolektor = dv_dtg(Me.BindingContext(dv_dtg).Position)("kd_karyawan").ToString
                        End If
                    End If

                    dv_kemb(Me.BindingContext(dv_kemb).Position)("noid") = 0
                    dv_kemb(Me.BindingContext(dv_kemb).Position)("nobukti") = drd("nobukti").ToString
                    dv_kemb(Me.BindingContext(dv_kemb).Position)("no_nota") = drd("no_nota").ToString
                    dv_kemb(Me.BindingContext(dv_kemb).Position)("tanggal") = drd("tanggal").ToString
                    dv_kemb(Me.BindingContext(dv_kemb).Position)("jmlfak") = drd("jmllebih").ToString
                    dv_kemb(Me.BindingContext(dv_kemb).Position)("jmlbayar") = 0

                    dv_kemb(Me.BindingContext(dv_kemb).Position)("kd_toko") = drd("kd_toko").ToString
                    dv_kemb(Me.BindingContext(dv_kemb).Position)("nama_toko") = drd("nama_toko").ToString
                    dv_kemb(Me.BindingContext(dv_kemb).Position)("alamat_toko") = drd("alamat_toko").ToString

                    If Not kd_kolektor.Equals("") Then
                        dv_kemb(Me.BindingContext(dv_kemb).Position)("kd_karyawan") = kd_kolektor
                    Else
                        dv_kemb(Me.BindingContext(dv_kemb).Position)("kd_karyawan") = ""
                    End If

                End If

            Else

                drd.Close()

                MsgBox("No Faktur tidak ditemukan", vbOKOnly + vbInformation, "Informasi")

                dv_kemb.Delete(Me.BindingContext(dv_kemb).Position)

            End If

            drd.Close()

        Catch ex As OleDb.OleDbException
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub GridView4_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView4.CellValueChanged

        If e.Column.FieldName.Equals("nobukti") Then

            cek_nobukti_kemb_grid(e.Value, False)

            hitung_total()

        ElseIf e.Column.FieldName.Equals("no_nota") Then

            cek_nobukti_kemb_grid(e.Value, True)

            hitung_total()

        End If

    End Sub

    Private Sub btdel_kemb_Click(sender As Object, e As EventArgs) Handles btdel_kemb.Click

        If IsNothing(dv_kemb) Then
            Return
        End If

        If dv_kemb.Count <= 0 Then
            Return
        End If

        If statview = True Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try


            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            Dim nobukti_fak As String = dv_kemb(Me.BindingContext(dv_kemb).Position)("nobukti").ToString
            Dim jmlkemb As Double = Double.Parse(dv_kemb(Me.BindingContext(dv_kemb).Position)("jmlbayar").ToString)
            Dim noid As Integer = Integer.Parse(dv_kemb(Me.BindingContext(dv_kemb).Position)("noid").ToString)

            If noid > 0 Then

                Dim sqlbalikkan As String = String.Format("update trfaktur_to set jmlkelebihan_pot=jmlkelebihan_pot-{0} where nobukti='{1}'", jmlkemb, nobukti_fak)
                Using cmdbalik2 As OleDbCommand = New OleDbCommand(sqlbalikkan, cn, sqltrans)
                    cmdbalik2.ExecuteNonQuery()
                End Using

                Dim sqldel_kemb As String = String.Format("delete from trbayar2_kemb where noid={0}", noid)
                Using cmddel_kemb As OleDbCommand = New OleDbCommand(sqldel_kemb, cn, sqltrans)
                    cmddel_kemb.ExecuteNonQuery()
                End Using

            End If

            sqltrans.Commit()
            dv_kemb.Delete(Me.BindingContext(dv_kemb).Position)


        Catch ex As Exception

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

    Private Sub GridView4_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView4.KeyDown
        If e.KeyCode = Keys.Delete Then
            btdel_kemb_Click(sender, Nothing)
        End If
    End Sub

End Class