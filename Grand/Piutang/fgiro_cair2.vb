Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fgiro_cair2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean
    Public statview As Boolean

    Public jenistrans As Integer

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private Sub kosongkan()
        tbukti.Text = "<< New >>"
        tnote.Text = ""

        opengrid()

    End Sub

    Private Sub opengrid()

        Dim sql As String = String.Format("SELECT ms_giro.nogiro, ms_giro.tgl_giro, ms_giro.tgl_jt, ms_giro.namabank, ms_giro.jumlah " & _
        "FROM tr_bg2 INNER JOIN ms_giro ON tr_bg2.nogiro = ms_giro.nogiro " & _
        "WHERE tr_bg2.nobukti='{0}'", tbukti.Text.Trim)

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

    Private Sub isi()

        Dim nobukti As String = dv(position)("nobukti").ToString

        Dim sql As String = String.Format("SELECT nobukti, tanggal, note FROM tr_bg WHERE nobukti='{0}'", nobukti)

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

                        tnote.EditValue = dread("note").ToString

                        opengrid()

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

        Dim noawal As String
        If jenistrans = 1 Then
            noawal = "PG"
        Else
            noawal = "TG"
        End If

        Dim sql As String = String.Format("select max(nobukti) from tr_bg where jenis={0} and nobukti like '%{1}.{2}%'", jenistrans, noawal, tahunbulan)

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

        

        Return String.Format("{0}.{1}{2}{3}", noawal, tahun, bulan, kbukti)

    End Function

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
                Dim sqlin_faktur As String = String.Format("insert into tr_bg (nobukti,tanggal,note,total,jenis) values('{0}','{1}','{2}',{3},{4})", tbukti.Text.Trim, _
                                                            convert_date_to_eng(ttgl.EditValue), tnote.Text.Trim, Replace(GridView1.Columns("jumlah").SummaryItem.SummaryValue, ",", "."), jenistrans)


                cmd = New OleDbCommand(sqlin_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()

                If jenistrans = 1 Then
                    Clsmy.InsertToLog(cn, "btgiro_cair", 1, 0, 0, 0, tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), sqltrans)
                Else
                    Clsmy.InsertToLog(cn, "btgiro_tolak", 1, 0, 0, 0, tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), sqltrans)
                End If


            Else

                '1. update faktur
                Dim sqlup_faktur As String = String.Format("update tr_bg set tanggal='{0}',note='{1}',total={2},jenis={3} where nobukti='{4}'", convert_date_to_eng(ttgl.EditValue), tnote.Text.Trim, Replace(GridView1.Columns("jumlah").SummaryItem.SummaryValue, ",", "."), jenistrans, tbukti.Text.Trim)

                cmd = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()

                If jenistrans = 1 Then
                    Clsmy.InsertToLog(cn, "btgiro_cair", 0, 1, 0, 0, tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), sqltrans)
                Else
                    Clsmy.InsertToLog(cn, "btgiro_tolak", 0, 1, 0, 0, tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), sqltrans)
                End If



            End If

            simpan2(cn, sqltrans)

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

    Private Sub cek_dulu_sebelum_simpan2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        Dim dtcek As DataTable = New DataTable
        dtcek = dv1.ToTable

        Dim sqlcek As String = String.Format("select * from tr_bg2 where nobukti='{0}'", tbukti.Text.Trim)
        Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn, sqltrans)
        Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

        While drdcek.Read

            Dim nogiro As String = drdcek("nogiro").ToString

            Dim dtcari As DataTable = New DataTable
            dtcari = dv1.ToTable
            Dim rows() As DataRow = dtcari.Select(String.Format("nogiro='{0}'", nogiro))

            Dim ada As Boolean = False
            If rows.Length > 1 Then
                ada = True
            End If

            If ada = False Then

                If jenistrans = 1 Then
                    GoTo Of_GiroCair
                Else
                    GoTo Of_GiroTolak
                End If

                ' giro cair
Of_GiroCair:

                Dim sql_lain As String = String.Format("select a.jmlgiro,b.kd_toko,b.nobukti,a.nogiro,b.kd_karyawan " & _
                    "from trbayar2_giro a inner join trfaktur_to b on a.nobukti_fak=b.nobukti " & _
                    "inner join tr_bg2 c on c.nogiro=a.nogiro " & _
                    "inner join trbayar d on a.nobukti=d.nobukti " & _
                    "where d.sbatal=0 and a.nogiro='{0}' and c.nobukti='{1}'", nogiro, tbukti.Text.Trim)
                Dim cmd_lain As OleDbCommand = New OleDbCommand(sql_lain, cn, sqltrans)
                Dim dr_lain As OleDbDataReader = cmd_lain.ExecuteReader

                If dr_lain.HasRows Then

                    While dr_lain.Read

                        Dim total_up As Double = Double.Parse(dr_lain("jmlgiro").ToString)

                        Dim sqlfak As String = String.Format("update trfaktur_to set jmlgiro=jmlgiro + {0} where nobukti='{1}'", total_up, dr_lain("nobukti").ToString)
                        Using cmdfak As OleDbCommand = New OleDbCommand(sqlfak, cn, sqltrans)
                            cmdfak.ExecuteNonQuery()
                        End Using

                        Dim sqlfak2 As String = String.Format("update trfaktur_to set jmlgiro_real=jmlgiro_real - {0} where nobukti='{1}'", total_up, dr_lain("nobukti").ToString)
                        Using cmdfak2 As OleDbCommand = New OleDbCommand(sqlfak2, cn, sqltrans)
                            cmdfak2.ExecuteNonQuery()
                        End Using

                        Dim sqltokoup As String = String.Format("update ms_toko set piutangbeli=piutangbeli + {0} where kd_toko='{1}'", Replace(total_up, ",", "."), dr_lain("kd_toko").ToString)
                        Using cmdtoko As OleDbCommand = New OleDbCommand(sqltokoup, cn, sqltrans)
                            cmdtoko.ExecuteNonQuery()
                        End Using

                        Dim sqltoko2_up As String = String.Format("update ms_toko2 set jmlpiutang=jmlpiutang + {0} where kd_toko='{1}' and kd_karyawan='{2}'", Replace(total_up, ",", "."), dr_lain("kd_toko").ToString, dr_lain("kd_karyawan").ToString)
                        Using cmdtoko2 As OleDbCommand = New OleDbCommand(sqltoko2_up, cn, sqltrans)
                            cmdtoko2.ExecuteNonQuery()
                        End Using

                        Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, dr_lain("nobukti").ToString, tbukti.Text.Trim, ttgl.EditValue, dr_lain("kd_toko").ToString, total_up, 0, "Pencairan Giro (Edit)")

                    End While

                End If
                dr_lain.Close()

                Dim sqlgiro As String = String.Format("update ms_giro set scair=0,tgl_cair=null where nogiro='{0}'", nogiro)
                Using cmdgiro As OleDbCommand = New OleDbCommand(sqlgiro, cn, sqltrans)
                    cmdgiro.ExecuteNonQuery()
                End Using

                ' end of giro cair
                GoTo Of_EndOfFile


                '-----------------------------------------------------------

                ' giro tolak
Of_GiroTolak:

                Dim sqlgiro2 As String = String.Format("update ms_giro set stolak=0,tgl_tolak=null where nogiro='{0}'", nogiro)
                Using cmdgiro As OleDbCommand = New OleDbCommand(sqlgiro2, cn, sqltrans)
                    cmdgiro.ExecuteNonQuery()
                End Using

                Dim sqllain2 As String = String.Format("select a.jmlgiro_batal as jmlgiro,b.kd_toko,b.nobukti,a.nogiro,b.kd_karyawan,a.nobukti as nobukti_byr " & _
                "from trbayar2_giro a inner join trfaktur_to b on a.nobukti_fak=b.nobukti " & _
                "inner join tr_bg2 c on c.nogiro=a.nogiro " & _
                "inner join trbayar d on a.nobukti=d.nobukti " & _
                "where d.sbatal=0 and a.nogiro='{0}' and c.nobukti='{1}'", nogiro, tbukti.Text.Trim)

                Dim cmdlain2 As OleDbCommand = New OleDbCommand(sqllain2, cn, sqltrans)
                Dim drlain2 As OleDbDataReader = cmdlain2.ExecuteReader

                If drlain2.HasRows Then

                    While drlain2.Read

                        Dim totalup2 As Double = Double.Parse(drlain2("jmlgiro").ToString)

                        Dim sqlfak2 As String = String.Format("update trfaktur_to set jmlgiro=jmlgiro + {0} where nobukti='{1}'", Replace(totalup2, ",", "."), drlain2("nobukti").ToString)
                        Using cmdfak2 As OleDbCommand = New OleDbCommand(sqlfak2, cn, sqltrans)
                            cmdfak2.ExecuteNonQuery()
                        End Using

                        Dim sqlbayar2 As String = String.Format("update trbayar2_giro set jmlgiro=jmlgiro + {0},jmlgiro_batal=jmlgiro_batal - {0} where nobukti='{1}' and nobukti_fak='{2}' and nogiro='{3}'", Replace(totalup2, ",", "."), drlain2("nobukti_byr").ToString, drlain2("nobukti").ToString, nogiro)
                        Using cmdbayar2 As OleDbCommand = New OleDbCommand(sqlbayar2, cn, sqltrans)
                            cmdbayar2.ExecuteNonQuery()
                        End Using

                    End While

                End If

                drlain2.Close()


                ' end of giro tolak

Of_EndOfFile:
                Dim sqldetail As String = String.Format("delete from tr_bg2 where nobukti='{0}' and nogiro='{1}'", tbukti.Text.Trim, nogiro)
                Using cmd_detail As OleDbCommand = New OleDbCommand(sqldetail, cn, sqltrans)
                    cmd_detail.ExecuteNonQuery()
                End Using


            End If
        End While
        drdcek.Close()


    End Sub

    Private Sub simpan2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        If addstat = False Then
            cek_dulu_sebelum_simpan2(cn, sqltrans)
        End If


        For i As Integer = 0 To dv1.Count - 1

            Dim sqlc As String = String.Format("select * from tr_bg2 where nobukti='{0}' and nogiro='{1}'", tbukti.Text.Trim, dv1(i)("nogiro").ToString)
            Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
            Dim drc As OleDbDataReader = cmdc.ExecuteReader

            If drc.Read Then

                If Not IsNumeric(drc("noid")) Then
                    drc.Close()
                    GoTo on_insert
                Else
                    drc.Close()
                    GoTo on_close
                End If

            Else
                drc.Close()
                GoTo on_insert
            End If


on_insert:

            Dim sqlin As String = String.Format("insert into tr_bg2 (nobukti,nogiro) values('{0}','{1}')", tbukti.Text.Trim, dv1(i)("nogiro").ToString)

            Using cmd As OleDbCommand = New OleDbCommand(sqlin, cn, sqltrans)
                cmd.ExecuteNonQuery()
            End Using

            If jenistrans = 1 Then
                GoTo masuk_girocair
            Else
                GoTo masuk_giro_tolak
            End If

            ' untuk jenis giro cair
masuk_girocair:

            Dim sqlgiro As String = String.Format("update ms_giro set scair=1,tgl_cair='{0}' where nogiro='{1}'", convert_date_to_eng(ttgl.EditValue), dv1(i)("nogiro").ToString)
            Using cmdgiro As OleDbCommand = New OleDbCommand(sqlgiro, cn, sqltrans)
                cmdgiro.ExecuteNonQuery()
            End Using


            Dim sql_lain As String = String.Format("select a.jmlgiro,b.kd_toko,b.nobukti,a.nogiro,b.kd_karyawan " & _
            "from trbayar2_giro a inner join trfaktur_to b on a.nobukti_fak=b.nobukti " & _
            "inner join trbayar c on a.nobukti=c.nobukti WHERE c.sbatal=0 and a.nogiro='{0}'", dv1(i)("nogiro").ToString)


            Dim cmd_lain As OleDbCommand = New OleDbCommand(sql_lain, cn, sqltrans)
            Dim dread_lain As OleDbDataReader = cmd_lain.ExecuteReader
            Dim dt_lain As DataTable = New DataTable
            dt_lain.Load(dread_lain)

            If dt_lain.Rows.Count > 0 Then

                For x As Integer = 0 To dt_lain.Rows.Count - 1

                    Dim total_up As Double = Double.Parse(dt_lain(x)("jmlgiro").ToString)

                    Dim sqlfak As String = String.Format("update trfaktur_to set jmlgiro_real=jmlgiro_real + {0} where nobukti='{1}'", Replace(total_up, ",", "."), dt_lain(x)("nobukti").ToString)
                    Using cmdfak As OleDbCommand = New OleDbCommand(sqlfak, cn, sqltrans)
                        cmdfak.ExecuteNonQuery()
                    End Using

                    Dim sqlfak2 As String = String.Format("update trfaktur_to set jmlgiro=jmlgiro - {0} where nobukti='{1}'", Replace(total_up, ",", "."), dt_lain(x)("nobukti").ToString)
                    Using cmdfak2 As OleDbCommand = New OleDbCommand(sqlfak2, cn, sqltrans)
                        cmdfak2.ExecuteNonQuery()
                    End Using

                    Dim sqltokoup As String = String.Format("update ms_toko set piutangbeli=piutangbeli - {0} where kd_toko='{1}'", Replace(total_up, ",", "."), dt_lain(x)("kd_toko").ToString)
                    Using cmdtoko As OleDbCommand = New OleDbCommand(sqltokoup, cn, sqltrans)
                        cmdtoko.ExecuteNonQuery()
                    End Using

                    Dim sqltoko2_up As String = String.Format("update ms_toko2 set jmlpiutang=jmlpiutang - {0} where kd_toko='{1}' and kd_karyawan='{2}'", Replace(total_up, ",", "."), dt_lain(x)("kd_toko").ToString, dt_lain(x)("kd_karyawan").ToString)
                    Using cmdtoko2 As OleDbCommand = New OleDbCommand(sqltoko2_up, cn, sqltrans)
                        cmdtoko2.ExecuteNonQuery()
                    End Using

                    Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, dt_lain(x)("nobukti").ToString, tbukti.Text.Trim, ttgl.EditValue, dt_lain(x)("kd_toko").ToString, 0, Replace(total_up, ",", "."), "Pencairan Giro")

                Next

            End If
            dread_lain.Close()

            GoTo on_close

            ' end jenis giro cair

            ' ----------------------------------------------------------------------------------------------------------------

            ' untuk jenis giro tolak
masuk_giro_tolak:

            Dim sqlgiro2 As String = String.Format("update ms_giro set stolak=1,tgl_tolak='{0}' where nogiro='{1}'", convert_date_to_eng(ttgl.EditValue), dv1(i)("nogiro").ToString)
            Using cmdgiro As OleDbCommand = New OleDbCommand(sqlgiro2, cn, sqltrans)
                cmdgiro.ExecuteNonQuery()
            End Using


            Dim sql_lain2 As String = String.Format("select a.jmlgiro,b.kd_toko,b.nobukti,a.nogiro,b.kd_karyawan,a.nobukti as nobukti_byr " & _
            "from trbayar2_giro a inner join trfaktur_to b on a.nobukti_fak=b.nobukti " & _
            "inner join trbayar c on a.nobukti=c.nobukti WHERE c.sbatal=0 and a.nogiro='{0}'", dv1(i)("nogiro").ToString)

            Dim cmdlain2 As OleDbCommand = New OleDbCommand(sql_lain2, cn, sqltrans)
            Dim dread_lain2 As OleDbDataReader = cmdlain2.ExecuteReader
            Dim dt_lain2 As DataTable = New DataTable
            dt_lain2.Load(dread_lain2)

            If dt_lain2.Rows.Count > 0 Then

                For x = 1 To dt_lain2.Rows.Count - 1

                    Dim totalup2 As Double = Double.Parse(dt_lain2(x)("jmlgiro").ToString)

                    Dim sqlfak2 As String = String.Format("update trfaktur_to set jmlgiro=jmlgiro - {0} where nobukti='{1}'", Replace(totalup2, ",", "."), dt_lain2(x)("nobukti").ToString)
                    Using cmdfak2 As OleDbCommand = New OleDbCommand(sqlfak2, cn, sqltrans)
                        cmdfak2.ExecuteNonQuery()
                    End Using

                    Dim sqlbayar2 As String = String.Format("update trbayar2_giro set jmlgiro=jmlgiro-{0},jmlgiro_batal=jmlgiro_batal + {0} where nobukti='{1}' and nobukti_fak='{2}' and nogiro='{3}'", Replace(totalup2, ",", "."), dt_lain2(x)("nobukti_byr").ToString, dt_lain2(x)("nobukti").ToString, dv1(i)("nogiro").ToString)
                    Using cmdbayar2 As OleDbCommand = New OleDbCommand(sqlbayar2, cn, sqltrans)
                        cmdbayar2.ExecuteNonQuery()
                    End Using

                Next

            End If
            dread_lain2.Close()


            ' end jenis giro tolak


on_close:

            'drc.Close()

            ' Debug.WriteLine(i)

        Next

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("nobukti") = tbukti.Text.Trim
        orow("tanggal") = ttgl.Text.Trim
        orow("total") = GridView1.Columns("jumlah").SummaryItem.SummaryValue
        orow("note") = tnote.Text.Trim
        orow("sbatal") = 0

        dv.EndInit()

    End Sub

    Private Sub updateview()

        dv(position)("nobukti") = tbukti.Text.Trim
        dv(position)("tanggal") = ttgl.Text.Trim
        dv(position)("total") = GridView1.Columns("jumlah").SummaryItem.SummaryValue
        dv(position)("note") = tnote.Text.Trim

    End Sub

    Private Sub ceknogiro(ByVal nogiro As String, ByVal addnew As Boolean)

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("SELECT ms_giro.nogiro, ms_giro.tgl_giro,ms_giro.tgl_jt, ms_giro.namabank, ms_giro.jumlah,ms_giro.sgunakan " & _
            "FROM ms_giro WHERE ms_giro.sbatal=0 and ms_giro.scair=0 and ms_giro.stolak=0 and ms_giro.nogiro='{0}'", nogiro)

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then

                If drd("nogiro").ToString.Equals("") Then
                    drd.Close()
                    GoTo kosongkan

                Else

                    If drd("sgunakan") = 0 Then
                        MsgBox("Giro ini belum digunakan pada pelunasan manapun", vbOKOnly + vbInformation, "Informasi")
                    End If

                    Dim dtcari As DataTable = New DataTable
                    dtcari = dv1.ToTable
                    Dim rows() As DataRow = dtcari.Select(String.Format("nogiro='{0}'", nogiro))

                    If rows.Length > 1 Then

                        drd.Close()

                        MsgBox("No Giro sudah ada dalam daftar", vbOKOnly + vbInformation, "Informasi")

                        GoTo kosongkan

                    End If

                    'If addnew = True Then

                    '    Dim orow As DataRowView = dv1.AddNew

                    '    orow("nogiro") = drd("nogiro").ToString
                    '    orow("tgl_giro") = drd("tgl_giro").ToString

                    '    If Not (drd("tgl_jt").ToString.Equals("")) Then
                    '        orow("tgl_jt") = drd("tgl_jt").ToString
                    '    Else
                    '        orow("tgl_jt") = DBNull.Value
                    '    End If

                    '    orow("namabank") = drd("namabank").ToString
                    '    orow("jumlah") = drd("jumlah").ToString

                    '    dv1.EndInit()

                    'Else

                    dv1(Me.BindingContext(dv1).Position)("nogiro") = drd("nogiro").ToString
                    dv1(Me.BindingContext(dv1).Position)("tgl_giro") = drd("tgl_giro").ToString

                    If Not (drd("tgl_jt").ToString.Equals("")) Then
                        dv1(Me.BindingContext(dv1).Position)("tgl_jt") = drd("tgl_jt").ToString
                    Else
                        dv1(Me.BindingContext(dv1).Position)("tgl_jt") = DBNull.Value
                    End If


                    dv1(Me.BindingContext(dv1).Position)("namabank") = drd("namabank").ToString
                    dv1(Me.BindingContext(dv1).Position)("jumlah") = drd("jumlah").ToString

                    'End If


                    drd.Close()
                    GoTo finishing

                End If


            Else

            drd.Close()
            GoTo kosongkan

            End If


kosongkan:

            If addnew = False Then

                dv1.Delete(Me.BindingContext(dv1).Position)

            End If


finishing:

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

        If IsNothing(dv1) Then
            MsgBox("Tidak ada giro yang diproses..", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If dv1.Count <= 0 Then
            MsgBox("Tidak ada giro yang diproses..", vbOKOnly + vbInformation, "Informasi")
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

        If addstat Then
            kosongkan()

        Else
            isi()
        End If

        If statview = True Then
            GridView1.Columns("nogiro").OptionsColumn.AllowEdit = False
        End If

    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If e.Column.FieldName = "nogiro" Then
            ceknogiro(e.Value, False)
        End If

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

            Dim sql As String = String.Format("SELECT ms_giro.jumlah, ms_giro.jumlahpakai, ms_giro.nogiro " & _
            "FROM tr_bg2 INNER JOIN ms_giro ON tr_bg2.nogiro = ms_giro.nogiro " & _
            "WHERE tr_bg2.nobukti='{0}' and ms_giro.nogiro='{1}' ", tbukti.Text.Trim, dv1(Me.BindingContext(dv1).Position)("nogiro").ToString)

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            Dim ada As Boolean = False

            If drd.HasRows Then
                While drd.Read

                    ada = True

                    If jenistrans = 1 Then
                        GoTo Of_GiroCair
                    Else
                        GoTo Of_GiroTolak
                    End If

                    ' giro cair
Of_GiroCair:

                    Dim sql_lain As String = String.Format("select a.jmlgiro,b.kd_toko,b.nobukti,a.nogiro,b.kd_karyawan " & _
                        "from trbayar2_giro a inner join trfaktur_to b on a.nobukti_fak=b.nobukti " & _
                        "inner join tr_bg2 c on c.nogiro=a.nogiro " & _
                        "inner join trbayar d on a.nobukti=d.nobukti " & _
                        "where d.sbatal=0 and a.nogiro='{0}' and c.nobukti='{1}'", drd("nogiro").ToString, tbukti.Text.Trim)
                    Dim cmd_lain As OleDbCommand = New OleDbCommand(sql_lain, cn, sqltrans)
                    Dim dr_lain As OleDbDataReader = cmd_lain.ExecuteReader

                    If dr_lain.HasRows Then

                        While dr_lain.Read

                            Dim total_up As Double = Double.Parse(dr_lain("jmlgiro").ToString)

                            Dim sqlfak As String = String.Format("update trfaktur_to set jmlgiro=jmlgiro + {0} where nobukti='{1}'", total_up, dr_lain("nobukti").ToString)
                            Using cmdfak As OleDbCommand = New OleDbCommand(sqlfak, cn, sqltrans)
                                cmdfak.ExecuteNonQuery()
                            End Using

                            Dim sqlfak2 As String = String.Format("update trfaktur_to set jmlgiro_real=jmlgiro_real - {0} where nobukti='{1}'", total_up, dr_lain("nobukti").ToString)
                            Using cmdfak2 As OleDbCommand = New OleDbCommand(sqlfak2, cn, sqltrans)
                                cmdfak2.ExecuteNonQuery()
                            End Using

                            Dim sqltokoup As String = String.Format("update ms_toko set piutangbeli=piutangbeli + {0} where kd_toko='{1}'", Replace(total_up, ",", "."), dr_lain("kd_toko").ToString)
                            Using cmdtoko As OleDbCommand = New OleDbCommand(sqltokoup, cn, sqltrans)
                                cmdtoko.ExecuteNonQuery()
                            End Using

                            Dim sqltoko2_up As String = String.Format("update ms_toko2 set jmlpiutang=jmlpiutang + {0} where kd_toko='{1}' and kd_karyawan='{2}'", Replace(total_up, ",", "."), dr_lain("kd_toko").ToString, dr_lain("kd_karyawan").ToString)
                            Using cmdtoko2 As OleDbCommand = New OleDbCommand(sqltoko2_up, cn, sqltrans)
                                cmdtoko2.ExecuteNonQuery()
                            End Using

                            Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, dr_lain("nobukti").ToString, tbukti.Text.Trim, ttgl.EditValue, dr_lain("kd_toko").ToString, total_up, 0, "Pencairan Giro (Edit)")

                        End While

                    End If
                    dr_lain.Close()

                    Dim sqlgiro As String = String.Format("update ms_giro set scair=0,tgl_cair=null where nogiro='{0}'", drd("nogiro").ToString)
                    Using cmdgiro As OleDbCommand = New OleDbCommand(sqlgiro, cn, sqltrans)
                        cmdgiro.ExecuteNonQuery()
                    End Using

                    ' end of giro cair
                    GoTo Of_EndOfFile


                    '-----------------------------------------------------------

                    ' giro tolak
Of_GiroTolak:

                    Dim sqlgiro2 As String = String.Format("update ms_giro set stolak=0,tgl_tolak=null where nogiro='{0}'", drd("nogiro").ToString)
                    Using cmdgiro As OleDbCommand = New OleDbCommand(sqlgiro2, cn, sqltrans)
                        cmdgiro.ExecuteNonQuery()
                    End Using

                    Dim sqllain2 As String = String.Format("select a.jmlgiro_batal as jmlgiro,b.kd_toko,b.nobukti,a.nogiro,b.kd_karyawan,a.nobukti as nobukti_byr " & _
                    "from trbayar2_giro a inner join trfaktur_to b on a.nobukti_fak=b.nobukti " & _
                    "inner join tr_bg2 c on c.nogiro=a.nogiro " & _
                    "inner join trbayar d on a.nobukti=d.nobukti " & _
                    "where d.sbatal=0 and a.nogiro='{0}' and c.nobukti='{1}'", drd("nogiro").ToString, tbukti.Text.Trim)

                    Dim cmdlain2 As OleDbCommand = New OleDbCommand(sqllain2, cn, sqltrans)
                    Dim drlain2 As OleDbDataReader = cmdlain2.ExecuteReader

                    If drlain2.HasRows Then

                        While drlain2.Read

                            Dim totalup2 As Double = Double.Parse(drlain2("jmlgiro").ToString)

                            Dim sqlfak2 As String = String.Format("update trfaktur_to set jmlgiro=jmlgiro + {0} where nobukti='{1}'", totalup2, drlain2("nobukti").ToString)
                            Using cmdfak2 As OleDbCommand = New OleDbCommand(sqlfak2, cn, sqltrans)
                                cmdfak2.ExecuteNonQuery()
                            End Using

                            Dim sqlbayar2 As String = String.Format("update trbayar2_giro set jmlgiro=jmlgiro + {0},jmlgiro_batal=jmlgiro_batal - {0} where nobukti='{1}' and nobukti_fak='{2}' and nogiro='{3}'", totalup2, drlain2("nobukti_byr").ToString, drlain2("nobukti").ToString, drd("nogiro").ToString)
                            Using cmdbayar2 As OleDbCommand = New OleDbCommand(sqlbayar2, cn, sqltrans)
                                cmdbayar2.ExecuteNonQuery()
                            End Using

                        End While

                    End If

                    drlain2.Close()


                    ' end of giro tolak

Of_EndOfFile:
                    Dim sqldetail As String = String.Format("delete from tr_bg2 where nobukti='{0}' and nogiro='{1}'", tbukti.Text.Trim, dv1(Me.BindingContext(dv1).Position)("nogiro").ToString)
                    Using cmd_detail As OleDbCommand = New OleDbCommand(sqldetail, cn, sqltrans)
                        cmd_detail.ExecuteNonQuery()
                    End Using

                End While
            End If

            drd.Close()

            dv1.Delete(Me.BindingContext(dv1).Position)

            If ada = True Then

                'Dim jumlah As Double = 0
                'For i As Integer = 0 To dv1.Count - 1
                '    jumlah = jumlah + Double.Parse(dv1(i)("jumlah").ToString)
                'Next

                '1. update faktur
                Dim sqlup_faktur As String = String.Format("update tr_bg set tanggal='{0}',note='{1}',total={2} where nobukti='{3}'", convert_date_to_eng(ttgl.EditValue), tnote.Text.Trim, Replace(GridView1.Columns("jumlah").SummaryItem.SummaryValue, ",", "."), tbukti.Text.Trim)

                Using cmd_head As OleDbCommand = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                    cmd_head.ExecuteNonQuery()
                End Using

            End If

            sqltrans.Commit()


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

    Private Sub GridView1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown

        If e.KeyCode = Keys.Delete Then

            btdel_Click(sender, Nothing)

        ElseIf e.KeyCode = Keys.F4 Then

            If GridView1.FocusedColumn.FieldName = "nogiro" Then

                RepositoryItemButtonEdit1_ButtonClick(sender, Nothing)

            End If
        End If

    End Sub

    Private Sub RepositoryItemButtonEdit1_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles RepositoryItemButtonEdit1.ButtonClick

        If statview = True Then
            Return
        End If

        Dim fs As New fgiro_cair3 With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        Dim nogiro As String = fs.get_nogiro

        If nogiro = "" Then
            Return
        End If

        If GridView1.IsNewItemRow(GridView1.FocusedRowHandle) Then
            ceknogiro(nogiro, True)
        Else
            ceknogiro(nogiro, False)
        End If

    End Sub

End Class