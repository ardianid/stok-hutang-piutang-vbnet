Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fdtg2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean
    Public statprint As Boolean

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager_sal As Data.DataViewManager
    Private dv_sal As Data.DataView

    Private dvmanager_ord As Data.DataViewManager
    Private dv_ord As Data.DataView

    Private dvmanager_krm As Data.DataViewManager
    Private dv_krm As Data.DataView

    Private dvmanager_tko As Data.DataViewManager
    Private dv_tko As Data.DataView

    Private dvmanager_psar As Data.DataViewManager
    Private dv_psar As Data.DataView

    Private Sub kosongkan()
        tbukti.Text = "<< New >>"

        tkd_supir.Text = ""
        tnama_supir.Text = ""

        tjml.EditValue = 0
        tjml1.EditValue = 0

        tnote.Text = ""

        opengrid()
        opengrid_sales()
        opengrid_ord()
        opengrid_krm()
        opengrid_outl()
        opengrid_psar()


    End Sub

    Private Sub opengrid()

        Dim sql As String = String.Format("SELECT trfaktur_to.nobukti, trfaktur_to.tanggal,trfaktur_to.tgl_tempo, ms_pegawai.nama_karyawan, ms_toko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, trdaftar_tgh2.netto, " & _
        "trdaftar_tgh2.jmlbayar, trdaftar_tgh2.jmlgiro,trdaftar_tgh2.jmlgiro_gt,trdaftar_tgh2.sisa " & _
        "FROM trfaktur_to INNER JOIN " & _
        "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko INNER JOIN " & _
        "ms_pegawai ON trfaktur_to.kd_karyawan = ms_pegawai.kd_karyawan INNER JOIN " & _
        "trdaftar_tgh2 ON trfaktur_to.nobukti = trdaftar_tgh2.nobukti_fak where trdaftar_tgh2.nobukti='{0}'", tbukti.Text.Trim)

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

    Private Sub opengrid_sales()

        Dim sql As String = String.Format("SELECT ms_pegawai.kd_karyawan, ms_pegawai.nama_karyawan " & _
        "FROM  trdaftar_tgh_sal INNER JOIN " & _
        "ms_pegawai ON trdaftar_tgh_sal.kd_sales = ms_pegawai.kd_karyawan where trdaftar_tgh_sal.nobukti='{0}'", tbukti.Text.Trim)

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

    Private Sub opengrid_ord()

        Dim sql As String = String.Format("SELECT ms_jalur.kd_jalur, ms_jalur.nama_jalur " & _
            "FROM trdaftar_tgh_jlur INNER JOIN " & _
            "ms_jalur ON trdaftar_tgh_jlur.kd_jalur_ord = ms_jalur.kd_jalur where trdaftar_tgh_jlur.nobukti='{0}'", tbukti.Text.Trim)

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        grid_ord.DataSource = Nothing

        Try

            open_wait()

            dv_ord = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager_ord = New DataViewManager(ds)
            dv_ord = dvmanager_ord.CreateDataView(ds.Tables(0))

            grid_ord.DataSource = dv_ord

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

    Private Sub opengrid_krm()

        Dim sql As String = String.Format("SELECT ms_jalur_kirim.kd_jalur, ms_jalur_kirim.nama_jalur " & _
        "FROM trdaftar_tgh_jlur INNER JOIN " & _
        "ms_jalur_kirim ON trdaftar_tgh_jlur.kd_jalur_krm = ms_jalur_kirim.kd_jalur where trdaftar_tgh_jlur.nobukti='{0}'", tbukti.Text.Trim)

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        grid_krm.DataSource = Nothing

        Try

            open_wait()

            dv_krm = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager_krm = New DataViewManager(ds)
            dv_krm = dvmanager_krm.CreateDataView(ds.Tables(0))

            grid_krm.DataSource = dv_krm

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
            "FROM  trdaftar_tgh_tko INNER JOIN ms_toko ON trdaftar_tgh_tko.kd_toko = ms_toko.kd_toko where trdaftar_tgh_tko.nobukti='{0}'", tbukti.Text.Trim)

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

    Private Sub opengrid_psar()

        Dim sql As String = String.Format("SELECT ms_pasar.kd_pasar, ms_pasar.nama_pasar " & _
        "FROM trdaftar_tgh_psr INNER JOIN " & _
        "ms_pasar ON trdaftar_tgh_psr.kd_pasar = ms_pasar.kd_pasar where trdaftar_tgh_psr.nobukti='{0}'", tbukti.Text.Trim)

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        grid_psr.DataSource = Nothing

        Try

            open_wait()

            dv_psar = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager_psar = New DataViewManager(ds)
            dv_psar = dvmanager_psar.CreateDataView(ds.Tables(0))

            grid_psr.DataSource = dv_psar

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

        Dim sql As String = String.Format("SELECT trdaftar_tgh.nobukti, trdaftar_tgh.tanggal, trdaftar_tgh.tgl_tagih, ms_pegawai.kd_karyawan, ms_pegawai.nama_karyawan, trdaftar_tgh.note, trdaftar_tgh.joverdue, trdaftar_tgh.joverdue2,trdaftar_tgh.jfaktur " & _
            "FROM  trdaftar_tgh INNER JOIN " & _
            "ms_pegawai ON trdaftar_tgh.kd_kolek = ms_pegawai.kd_karyawan where trdaftar_tgh.nobukti='{0}'", nobukti)

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
                        ttgl_tgh.EditValue = DateValue(dread("tgl_tagih").ToString)

                        tkd_supir.EditValue = dread("kd_karyawan").ToString
                        tnama_supir.EditValue = dread("nama_karyawan").ToString
   
                        tnote.EditValue = dread("note").ToString

                        tjml.EditValue = Integer.Parse(dread("joverdue").ToString)
                        tjml1.EditValue = Integer.Parse(dread("joverdue2").ToString)

                        tjfaktur.EditValue = dread("jfaktur").ToString

                        opengrid()
                        opengrid_sales()
                        opengrid_ord()
                        opengrid_krm()
                        opengrid_outl()
                        opengrid_psar()

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

        Dim sql As String = String.Format("select max(nobukti) from trdaftar_tgh where nobukti like '%DT.{0}%'", tahunbulan)

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

        Return String.Format("DT.{0}{1}{2}", tahun, bulan, kbukti)

    End Function

    Private Sub LoadData()

        Dim sql As String = "SELECT trfaktur_to.nobukti, trfaktur_to.tanggal, trfaktur_to.tgl_tempo, ms_pegawai.nama_karyawan, ms_toko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, " & _
        "trfaktur_to.netto, trfaktur_to.jmlbayar, trfaktur_to.jmlgiro_real as jmlgiro,trfaktur_to.jmlgiro as jmlgiro_gt,(trfaktur_to.netto - (trfaktur_to.jmlbayar + trfaktur_to.jmlgiro_real + trfaktur_to.jmlgiro)) as sisa " & _
        "FROM trfaktur_to INNER JOIN ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko INNER JOIN " & _
        "ms_pegawai ON trfaktur_to.kd_karyawan = ms_pegawai.kd_karyawan " & _
        "WHERE trfaktur_to.sbatal=0 and trfaktur_to.netto > (trfaktur_to.jmlbayar + trfaktur_to.jmlgiro_real) And trfaktur_to.spulang = 1 and trfaktur_to.statkirim='TERKIRIM'"


        If tjfaktur.EditValue = "TO" Then
            sql = String.Format("{0} and trfaktur_to.jnis_fak='T'", sql)
        ElseIf tjfaktur.EditValue = "KANVAS" Then
            sql = String.Format("{0} and trfaktur_to.jnis_fak='K'", sql)
        End If

        If tjml.EditValue > 0 And tjml1.EditValue > 0 Then

            sql = String.Format("{0} and ( datediff(day,trfaktur_to.tanggal,'{1}')>={2} and datediff(day,trfaktur_to.tanggal,'{3}')<={4} )", sql, convert_date_to_eng(ttgl.EditValue), tjml.EditValue, convert_date_to_eng(ttgl.EditValue), tjml1.EditValue)

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

        ' search by ord

        If Not (IsNothing(dv_ord)) Then
            If dv_ord.Count > 0 Then

                Dim x As Integer = 0

                For i As Integer = 0 To dv_ord.Count - 1

                    If i = 0 Then
                        sql = String.Format("{0} and ms_toko.kd_jalurorder in ('{1}'", sql, dv_ord(i)("kd_jalur").ToString)
                    Else
                        sql = String.Format("{0},'{1}'", sql, dv_ord(i)("kd_jalur").ToString)
                    End If

                    x = x + 1

                Next

                If x > 0 Then
                    sql = String.Format("{0})", sql)
                End If

            End If
        End If

        '-----------------------------------------------------------

        ' search by krim

        If Not (IsNothing(dv_krm)) Then
            If dv_krm.Count > 0 Then

                Dim x As Integer = 0

                For i As Integer = 0 To dv_krm.Count - 1

                    If i = 0 Then
                        sql = String.Format("{0} and ms_toko.kd_jalurkirim in ('{1}'", sql, dv_krm(i)("kd_jalur").ToString)
                    Else
                        sql = String.Format("{0},'{1}'", sql, dv_krm(i)("kd_jalur").ToString)
                    End If

                    x = x + 1

                Next

                If x > 0 Then
                    sql = String.Format("{0})", sql)
                End If

            End If
        End If

        '-----------------------------------------------------------

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

        ' search by outlet

        If Not (IsNothing(dv_psar)) Then
            If dv_psar.Count > 0 Then

                Dim x As Integer = 0

                For i As Integer = 0 To dv_psar.Count - 1

                    If i = 0 Then
                        sql = String.Format("{0} and ms_toko.kd_pasar in ('{1}'", sql, dv_psar(i)("kd_pasar").ToString)
                    Else
                        sql = String.Format("{0},'{1}'", sql, dv_psar(i)("kd_pasar").ToString)
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

    Private Sub load_print(ByVal cn As OleDbConnection)

        Dim nobukti As String = tbukti.Text.Trim

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

        Dim sql As String = String.Format("SELECT     trdaftar_tgh.nobukti, trdaftar_tgh.tanggal, trdaftar_tgh.tgl_tagih, ms_pegawai.nama_karyawan, trdaftar_tgh2.nobukti_fak, trfaktur_to.tanggal AS tanggal_fak, " & _
        "trfaktur_to.tgl_tempo, trdaftar_tgh2.netto, trdaftar_tgh2.jmlbayar, trdaftar_tgh2.jmlgiro,trdaftar_tgh2.jmlgiro_gt, trdaftar_tgh2.sisa, trdaftar_tgh.jumlah, ms_toko.nama_toko " & _
        "FROM         trdaftar_tgh INNER JOIN " & _
        "trdaftar_tgh2 ON trdaftar_tgh.nobukti = trdaftar_tgh2.nobukti INNER JOIN " & _
        "trfaktur_to ON trdaftar_tgh2.nobukti_fak = trfaktur_to.nobukti INNER JOIN " & _
        "ms_pegawai ON trdaftar_tgh.kd_kolek = ms_pegawai.kd_karyawan INNER JOIN " & _
        "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko " & _
        "WHERE     (trdaftar_tgh.sbatal = 0) AND (trdaftar_tgh.spulang = 0) and (trdaftar_tgh.nobukti='{0}')", nobukti)

            Dim ds As DataSet = New dstagih
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_daftartagih1() With {.DataSource = ds.Tables(0)}
            rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
            rrekap.DataMember = rrekap.DataMember
            rrekap.PrinterName = varprinter1
            rrekap.CreateDocument(True)
        rrekap.Print()


            ' PrintControl1.PrintingSystem = rrekap.PrintingSystem

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

            If addstat Then
                Dim bukti As String = cekbukti(cn, sqltrans)
                tbukti.EditValue = bukti

                '1 . update faktur
                Dim sqlin_faktur As String = String.Format("insert into trdaftar_tgh (nobukti,tanggal,tgl_tagih,kd_kolek,note,jumlah,joverdue,joverdue2,jfaktur) values('{0}','{1}','{2}','{3}','{4}',{5},{6},{7},'{8}')", tbukti.Text.Trim, _
                                                            convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_tgh.EditValue), tkd_supir.EditValue, tnote.Text.Trim, Replace(GridView1.Columns("sisa").SummaryItem.SummaryValue, ",", "."), tjml.EditValue, tjml1.EditValue, tjfaktur.EditValue)


                cmd = New OleDbCommand(sqlin_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btdtg", 1, 0, 0, 0, tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), sqltrans)

            Else

                '1. update faktur
                Dim sqlup_faktur As String = String.Format("update trdaftar_tgh set tanggal='{0}',tgl_tagih='{1}',kd_kolek='{2}',note='{3}',jumlah={4},joverdue={5},joverdue2={6},jfaktur='{7}' where nobukti='{8}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_tgh.EditValue), tkd_supir.EditValue, tnote.Text.Trim, Replace(GridView1.Columns("sisa").SummaryItem.SummaryValue, ",", "."), tjml.EditValue, tjml1.EditValue, tjfaktur.EditValue, tbukti.Text.Trim)

                cmd = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btdtg", 0, 1, 0, 0, tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), sqltrans)

            End If

            simpan2(cn, sqltrans)

            If addstat = True Then
                insertview()
            Else
                updateview()
            End If

            sqltrans.Commit()

            close_wait()

            If addstat = True Then

                If statprint Then
                    If MsgBox("Akan langsung diprint ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
                        load_print(cn)
                    End If
                End If
               

                kosongkan()
                ttgl.Focus()
            Else
                close_wait()
                MsgBox("Data disimpan...", vbOKOnly + vbInformation, "Informasi")
                Me.Close()
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

    Private Sub simpan2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        Dim sqldel As String = String.Format("delete from trdaftar_tgh2 where nobukti='{0}'", tbukti.Text.Trim)
        Using cmddel As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
            cmddel.ExecuteNonQuery()
        End Using

        For i As Integer = 0 To dv1.Count - 1

            Dim sql As String = String.Format("insert into trdaftar_tgh2 (nobukti,nobukti_fak,netto,jmlbayar,jmlgiro,jmlgiro_gt,sisa) values('{0}','{1}',{2},{3},{4},{5},{6})", tbukti.Text.Trim, _
                                              dv1(i)("nobukti").ToString, Replace(dv1(i)("netto").ToString, ",", "."), Replace(dv1(i)("jmlbayar").ToString, ",", "."), Replace(dv1(i)("jmlgiro").ToString, ",", "."), Replace(dv1(i)("jmlgiro_gt").ToString, ",", "."), Replace(dv1(i)("sisa").ToString, ",", "."))

            Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()
            End Using

        Next


        ' sales -----------------------------------

        Dim sqlsal As String = String.Format("delete from trdaftar_tgh_sal where nobukti='{0}'", tbukti.Text.Trim)
        Using cmddel As OleDbCommand = New OleDbCommand(sqlsal, cn, sqltrans)
            cmddel.ExecuteNonQuery()
        End Using

        If Not (IsNothing(dv_sal)) Then

            For i As Integer = 0 To dv_sal.Count - 1
                Dim sql As String = String.Format("insert into trdaftar_tgh_sal (nobukti,kd_sales) values('{0}','{1}')", tbukti.Text.Trim, dv_sal(i)("kd_karyawan").ToString)
                Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using
            Next

        End If

        ' ------------------------------------------------------------------------


        Dim sqlc As String
        Dim cmdc As OleDbCommand
        Dim drdc As OleDbDataReader

        ' order -----------------------------------

        If Not (IsNothing(dv_ord)) Then

            For i As Integer = 0 To dv_ord.Count - 1

                sqlc = String.Format("select noid from trdaftar_tgh_jlur where nobukti='{0}' and kd_jalur_ord='{1}'", tbukti.Text.Trim, dv_ord(i)("kd_jalur").ToString)
                cmdc = New OleDbCommand(sqlc, cn, sqltrans)
                drdc = cmdc.ExecuteReader

                If drdc.Read Then

                    If IsNumeric(drdc(0).ToString) Then

                        Dim sqlminus As String = String.Format("delete from trdaftar_tgh_jlur where noid={0}", drdc(0).ToString)
                        Using cmdmin As OleDbCommand = New OleDbCommand(sqlminus, cn, sqltrans)
                            cmdmin.ExecuteNonQuery()
                        End Using

                    End If

                End If

                drdc.Close()


                Dim sql As String = String.Format("insert into trdaftar_tgh_jlur (nobukti,kd_jalur_ord) values('{0}','{1}')", tbukti.Text.Trim, dv_ord(i)("kd_jalur").ToString)
                Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

            Next

        End If

        ' ------------------------------------------------------------------------


        ' kirim -----------------------------------

        If Not (IsNothing(dv_krm)) Then

            For i As Integer = 0 To dv_krm.Count - 1

                sqlc = String.Format("select noid from trdaftar_tgh_jlur where nobukti='{0}' and kd_jalur_krm='{1}'", tbukti.Text.Trim, dv_krm(i)("kd_jalur").ToString)
                cmdc = New OleDbCommand(sqlc, cn, sqltrans)
                drdc = cmdc.ExecuteReader

                If drdc.Read Then

                    If IsNumeric(drdc(0).ToString) Then

                        Dim sqlminus As String = String.Format("delete from trdaftar_tgh_jlur where noid={0}", drdc(0).ToString)
                        Using cmdmin As OleDbCommand = New OleDbCommand(sqlminus, cn, sqltrans)
                            cmdmin.ExecuteNonQuery()
                        End Using

                    End If

                End If

                drdc.Close()


                Dim sql As String = String.Format("insert into trdaftar_tgh_jlur (nobukti,kd_jalur_krm) values('{0}','{1}')", tbukti.Text.Trim, dv_krm(i)("kd_jalur").ToString)
                Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

            Next

        End If

        ' ------------------------------------------------------------------------

        ' outlet -----------------------------------

        Dim sqlout As String = String.Format("delete from trdaftar_tgh_tko where nobukti='{0}'", tbukti.Text.Trim)
        Using cmddel As OleDbCommand = New OleDbCommand(sqlout, cn, sqltrans)
            cmddel.ExecuteNonQuery()
        End Using

        If Not (IsNothing(dv_tko)) Then

            For i As Integer = 0 To dv_tko.Count - 1
                Dim sql As String = String.Format("insert into trdaftar_tgh_tko (nobukti,kd_toko) values('{0}','{1}')", tbukti.Text.Trim, dv_tko(i)("kd_toko").ToString)
                Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using
            Next

        End If

        ' ------------------------------------------------------------------------


        ' pasar -----------------------------------

        Dim sqlpasar As String = String.Format("delete from trdaftar_tgh_psr where nobukti='{0}'", tbukti.Text.Trim)
        Using cmddel As OleDbCommand = New OleDbCommand(sqlpasar, cn, sqltrans)
            cmddel.ExecuteNonQuery()
        End Using

        If Not (IsNothing(dv_psar)) Then

            For i As Integer = 0 To dv_psar.Count - 1
                Dim sql As String = String.Format("insert into trdaftar_tgh_psr (nobukti,kd_pasar) values('{0}','{1}')", tbukti.Text.Trim, dv_psar(i)("kd_psar").ToString)
                Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using
            Next

        End If

        ' ------------------------------------------------------------------------

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("nobukti") = tbukti.Text.Trim
        orow("tanggal") = ttgl.Text.Trim
        orow("tgl_tagih") = ttgl_tgh.Text.Trim
        orow("nama_karyawan") = tnama_supir.Text.Trim
        orow("jumlah") = GridView1.Columns("sisa").SummaryItem.SummaryValue
        orow("note") = tnote.Text.Trim
        orow("sbatal") = 0
        orow("spulang") = 0

        dv.EndInit()

    End Sub

    Private Sub updateview()

        dv(position)("nobukti") = tbukti.Text.Trim
        dv(position)("tanggal") = ttgl.Text.Trim
        dv(position)("tgl_tagih") = ttgl_tgh.Text.Trim
        dv(position)("nama_karyawan") = tnama_supir.Text.Trim
        dv(position)("jumlah") = GridView1.Columns("sisa").SummaryItem.SummaryValue
        dv(position)("note") = tnote.Text.Trim

    End Sub

    Private Sub bts_supir_Click(sender As System.Object, e As System.EventArgs) Handles bts_supir.Click

        Dim fs As New fssales With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
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
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and bagian='SALES' and kd_karyawan='{0}'", tkd_supir.Text.Trim)

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

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tkd_supir.EditValue = "" Then
            MsgBox("Kolektor tidak boleh kosong..", vbOKOnly + vbInformation, "Informasi")
            tkd_supir.Focus()
            Return
        End If

        If IsNothing(dv1) Then
            MsgBox("Tidak ada faktur yang diproses..", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If dv1.Count <= 0 Then
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

        If addstat Then
            kosongkan()

            tjfaktur.SelectedIndex = 0

        Else
            isi()
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
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            Dim kdsal As String = dv_sal(Me.BindingContext(dv_sal).Position)("kd_karyawan").ToString

            Dim sql As String = String.Format("delete from trdaftar_tgh_sal where nobukti='{0}' and kd_sales='{1}'", tbukti.Text.Trim, kdsal)
            Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()
            End Using

            sqltrans.Commit()

            dv_sal.Delete(Me.BindingContext(dv_sal).Position)

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

    ' -----------------------------------------

    ' option jalur order

    Private Sub btadd_ord_Click(sender As System.Object, e As System.EventArgs) Handles btadd_ord.Click

        Dim fs As New fdtg_ord With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .dv = dv_ord}
        fs.ShowDialog(Me)

    End Sub

    Private Sub btdel_ord_Click(sender As System.Object, e As System.EventArgs) Handles btdel_ord.Click

        If IsNothing(dv_ord) Then
            Return
        End If

        If dv_ord.Count <= 0 Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            Dim kdord As String = dv_ord(Me.BindingContext(dv_ord).Position)("kd_jalur").ToString

            Dim sql As String = String.Format("delete from trdaftar_tgh_jlur where nobukti='{0}' and kd_jalur_ord='{1}'", tbukti.Text.Trim, kdord)
            Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()
            End Using

            sqltrans.Commit()

            dv_ord.Delete(Me.BindingContext(dv_ord).Position)

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

    ' -----------------------------------------

    ' option jalur kirim

    Private Sub btadd_krm_Click(sender As System.Object, e As System.EventArgs) Handles btadd_krm.Click

        Dim fs As New fdtg_krm With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .dv = dv_krm}
        fs.ShowDialog(Me)

    End Sub

    Private Sub btdel_krm_Click(sender As System.Object, e As System.EventArgs) Handles btdel_krm.Click

        If IsNothing(dv_krm) Then
            Return
        End If

        If dv_krm.Count <= 0 Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            Dim kdord As String = dv_krm(Me.BindingContext(dv_krm).Position)("kd_jalur").ToString

            Dim sql As String = String.Format("delete from trdaftar_tgh_jlur where nobukti='{0}' and kd_jalur_krm='{1}'", tbukti.Text.Trim, kdord)
            Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()
            End Using

            sqltrans.Commit()

            dv_krm.Delete(Me.BindingContext(dv_krm).Position)

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
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            Dim kdord As String = dv_tko(Me.BindingContext(dv_tko).Position)("kd_toko").ToString

            Dim sql As String = String.Format("delete from trdaftar_tgh_tko where nobukti='{0}' and kd_toko='{1}'", tbukti.Text.Trim, kdord)
            Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()
            End Using

            sqltrans.Commit()

            dv_tko.Delete(Me.BindingContext(dv_tko).Position)

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

    ' -----------------------------------------

    ' option pasar

    Private Sub btadd_psr_Click(sender As System.Object, e As System.EventArgs) Handles btadd_psr.Click

        Dim fs As New fdtg_psar With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .dv = dv_psar}
        fs.ShowDialog(Me)

    End Sub

    Private Sub btdel_psr_Click(sender As System.Object, e As System.EventArgs) Handles btdel_psr.Click

        If IsNothing(dv_psar) Then
            Return
        End If

        If dv_psar.Count <= 0 Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            Dim kdord As String = dv_psar(Me.BindingContext(dv_psar).Position)("kd_pasar").ToString

            Dim sql As String = String.Format("delete from trdaftar_tgh_psr where nobukti='{0}' and kd_pasar='{1}'", tbukti.Text.Trim, kdord)
            Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()
            End Using

            sqltrans.Commit()

            dv_psar.Delete(Me.BindingContext(dv_psar).Position)

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

    ' -----------------------------------------


    ' del faktur detail

    Private Sub btdel_Click(sender As System.Object, e As System.EventArgs) Handles btdel.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            Dim kdord As String = dv1(Me.BindingContext(dv1).Position)("nobukti").ToString

            Dim sisa As Double = Double.Parse(dv1(Me.BindingContext(dv1).Position)("sisa").ToString)

            Dim sqlup_faktur As String = String.Format("update trdaftar_tgh set jumlah=jumlah -{0} where nobukti='{1}'", sisa, tbukti.Text.Trim)
            Using cmdup_fak As OleDbCommand = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                cmdup_fak.ExecuteNonQuery()
            End Using

            Dim sql As String = String.Format("delete from trdaftar_tgh2 where nobukti='{0}' and nobukti_fak='{1}'", tbukti.Text.Trim, kdord)
            Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()
            End Using

            sqltrans.Commit()

            dv1.Delete(Me.BindingContext(dv1).Position)

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

    Private Sub btadd_Click(sender As System.Object, e As System.EventArgs) Handles btadd.Click

        Dim nofaktur = InputBox("Masukkan No Faktur", "Add NoFaktur")

        If nofaktur.Trim.Length = 0 Then
            Return
        End If

        Dim dta As DataTable = dv1.ToTable
        Dim rows() As DataRow = dta.Select(String.Format("nobukti='{0}'", nofaktur))
        If rows.Length > 0 Then
            MsgBox("No Faktur sudah ada", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn


            Dim sql As String = String.Format("SELECT     trfaktur_to.nobukti, trfaktur_to.tanggal,trfaktur_to.tgl_tempo, ms_pegawai.nama_karyawan, ms_toko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, trfaktur_to.netto, " & _
            "trfaktur_to.jmlbayar, trfaktur_to.jmlgiro,trfaktur_to.netto - (trfaktur_to.jmlbayar + trfaktur_to.jmlgiro) as sisa " & _
            "FROM         trfaktur_to INNER JOIN " & _
            "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko INNER JOIN " & _
            "ms_pegawai ON trfaktur_to.kd_karyawan = ms_pegawai.kd_karyawan " & _
            "WHERE trfaktur_to.sbatal = 0 And trfaktur_to.netto > (trfaktur_to.jmlbayar + trfaktur_to.jmlgiro) And trfaktur_to.spulang = 1 and trfaktur_to.statkirim ='TERKIRIM' and trfaktur_to.nobukti='{0}'", nofaktur)


            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Not (drd(0).ToString.Equals("")) Then

                    Dim orow As DataRowView = dv1.AddNew

                    orow("nobukti") = drd("nobukti").ToString
                    orow("tanggal") = drd("tanggal").ToString
                    orow("nama_karyawan") = drd("nama_karyawan").ToString
                    orow("kd_toko") = drd("kd_toko").ToString
                    orow("nama_toko") = drd("nama_toko").ToString
                    orow("alamat_toko") = drd("alamat_toko").ToString
                    orow("netto") = drd("netto").ToString
                    orow("jmlbayar") = drd("jmlbayar").ToString
                    orow("sisa") = drd("sisa").ToString

                    dv1.EndInit()


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

    ' -----------------------------------------------

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If GridView1.FocusedColumn.FieldName = "nobukti" Then

            Dim nofaktur As String = e.Value

            Dim dta As DataTable = dv1.ToTable
            Dim rows() As DataRow = dta.Select(String.Format("nobukti='{0}'", nofaktur))
            If rows.Length > 1 Then
                MsgBox("No Faktur sudah ada", vbOKOnly + vbInformation, "Informasi")
                dv1.Delete(Me.BindingContext(dv1).Position)
                Return
            End If

            Dim cn As OleDbConnection = Nothing

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn


                Dim sql As String = String.Format("SELECT     trfaktur_to.nobukti, trfaktur_to.tanggal,trfaktur_to.tgl_tempo, ms_pegawai.nama_karyawan, ms_toko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, trfaktur_to.netto, " & _
                "trfaktur_to.jmlbayar, trfaktur_to.jmlgiro_real as jmlgiro,trfaktur_to.jmlgiro as jmlgiro_gt,trfaktur_to.netto - (trfaktur_to.jmlbayar + trfaktur_to.jmlgiro_real) as sisa " & _
                "FROM         trfaktur_to INNER JOIN " & _
                "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko INNER JOIN " & _
                "ms_pegawai ON trfaktur_to.kd_karyawan = ms_pegawai.kd_karyawan " & _
                "WHERE trfaktur_to.sbatal = 0 And trfaktur_to.netto > (trfaktur_to.jmlbayar + trfaktur_to.jmlgiro_real) And trfaktur_to.spulang = 1 and trfaktur_to.statkirim ='TERKIRIM' and trfaktur_to.nobukti='{0}'", nofaktur)


                Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim drd As OleDbDataReader = cmd.ExecuteReader

                If drd.Read Then
                    If Not (drd(0).ToString.Equals("")) Then

                        '   Dim orow As DataRowView = dv1.AddNew

                        dv1(Me.BindingContext(dv1).Position)("nobukti") = drd("nobukti").ToString
                        dv1(Me.BindingContext(dv1).Position)("tanggal") = drd("tanggal").ToString
                        dv1(Me.BindingContext(dv1).Position)("nama_karyawan") = drd("nama_karyawan").ToString
                        dv1(Me.BindingContext(dv1).Position)("kd_toko") = drd("kd_toko").ToString
                        dv1(Me.BindingContext(dv1).Position)("nama_toko") = drd("nama_toko").ToString
                        dv1(Me.BindingContext(dv1).Position)("alamat_toko") = drd("alamat_toko").ToString
                        dv1(Me.BindingContext(dv1).Position)("netto") = drd("netto").ToString
                        dv1(Me.BindingContext(dv1).Position)("jmlbayar") = drd("jmlbayar").ToString
                        dv1(Me.BindingContext(dv1).Position)("jmlgiro") = drd("jmlgiro").ToString
                        dv1(Me.BindingContext(dv1).Position)("jmlgiro_gt") = drd("jmlgiro_gt").ToString
                        dv1(Me.BindingContext(dv1).Position)("sisa") = drd("sisa").ToString

                        ' dv1.EndInit()

                    Else
                        dv1.Delete(Me.BindingContext(dv1).Position)
                    End If
                Else
                    dv1.Delete(Me.BindingContext(dv1).Position)
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

    Private Sub GridView1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown

        If e.KeyCode = Keys.Delete Then
            btdel_Click(sender, Nothing)
        End If

    End Sub

End Class