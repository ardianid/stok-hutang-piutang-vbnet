Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI

Public Class frekap_to2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private tglmuat_old As String
    Private nopol_old As String
    Private supir_old As String

    Public statprint As Boolean

    Private stat_fak_kosong As Boolean = False
    Private jmlgallon_fak As Integer

    Private Sub kosongkan()
        tbukti.Text = "<< New >>"

        tkd_supir.EditValue = ""
        tnama_supir.EditValue = ""

        tkd_ken1.EditValue = ""
        tnama_ken1.EditValue = ""

        tkd_ken2.EditValue = ""
        tnama_ken2.EditValue = ""

        tkd_ken3.EditValue = ""
        tnama_ken3.EditValue = ""

        tket.EditValue = ""

        opengrid()

    End Sub

    Private Sub opengrid()

        Dim sql As String = String.Format("select a.noid,a.nobukti_fak,b.tanggal,c.kd_toko,c.nama_toko,c.alamat_toko,d.kd_karyawan,d.nama_karyawan,b.netto from trrekap_to2 a inner join trfaktur_to b on a.nobukti_fak=b.nobukti " & _
         "inner join ms_toko c on b.kd_toko=c.kd_toko inner join ms_pegawai d on b.kd_karyawan=d.kd_karyawan where a.nobukti='{0}'", tbukti.Text.Trim)


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
        Dim sql As String = String.Format("SELECT     trrekap_to.nobukti, trrekap_to.tgl, trrekap_to.tglmuat, trrekap_to.tglkirim, trrekap_to.kd_supir, ms_pegawai.nama_karyawan AS nama_supir, trrekap_to.kd_kenek1, " & _
                      "ms_pegawai2.nama_karyawan AS nama_kenek1, trrekap_to.kd_kenek2, ms_pegawai3.nama_karyawan AS nama_kenek2, trrekap_to.kd_kenek3, " & _
                      "ms_pegawai4.nama_karyawan AS nama_kenek3, trrekap_to.kd_jalur, ms_jalur_kirim.nama_jalur, trrekap_to.nopol, trrekap_to.note,trrekap_to.sfaktur_kosong " & _
                        "FROM         ms_pegawai AS ms_pegawai4 RIGHT OUTER JOIN " & _
                      "ms_pegawai RIGHT OUTER JOIN " & _
                      "ms_jalur_kirim INNER JOIN " & _
                      "trrekap_to ON ms_jalur_kirim.kd_jalur = trrekap_to.kd_jalur ON ms_pegawai.kd_karyawan = trrekap_to.kd_supir LEFT OUTER JOIN " & _
                      "ms_pegawai AS ms_pegawai2 ON trrekap_to.kd_kenek1 = ms_pegawai2.kd_karyawan LEFT OUTER JOIN " & _
                      "ms_pegawai AS ms_pegawai3 ON trrekap_to.kd_kenek2 = ms_pegawai3.kd_karyawan ON ms_pegawai4.kd_karyawan = trrekap_to.kd_kenek3 where trrekap_to.nobukti='{0}'", nobukti)

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
                        tnopol.EditValue = dread("nopol").ToString

                        nopol_old = dread("nopol").ToString

                        If Integer.Parse(dread("sfaktur_kosong").ToString) = 0 Then
                            stat_fak_kosong = False
                        Else
                            stat_fak_kosong = True
                        End If

                        tkd_supir.EditValue = dread("kd_supir").ToString

                        supir_old = dread("kd_supir").ToString

                        tnama_supir.EditValue = dread("nama_supir").ToString

                        tkd_ken1.EditValue = dread("kd_kenek1").ToString
                        tnama_ken1.EditValue = dread("nama_kenek1").ToString

                        tkd_ken2.EditValue = dread("kd_kenek2").ToString
                        tnama_ken2.EditValue = dread("nama_kenek2").ToString

                        tkd_ken3.EditValue = dread("kd_kenek3").ToString
                        tnama_ken3.EditValue = dread("nama_kenek3").ToString

                        ttgl.EditValue = DateValue(dread("tgl").ToString)
                        ttgl_mt.EditValue = DateValue(dread("tglmuat").ToString)

                        tglmuat_old = ttgl_mt.EditValue

                        ttgl_krm.EditValue = DateValue(dread("tglkirim").ToString)

                        tjalur.EditValue = dread("kd_jalur").ToString

                        tket.EditValue = dread("note").ToString

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

    Private Sub isi_nopol()

        Const sql As String = "select * from ms_kendaraan where aktif=1"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet
        Dim dvg As DataView

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvg = dvm.CreateDataView(ds.Tables(0))

            tnopol.Properties.DataSource = dvg

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

    Private Sub isi_jalur()

        Const sql As String = "select * from ms_jalur_kirim"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet
        Dim dvg As DataView

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvg = dvm.CreateDataView(ds.Tables(0))

            tjalur.Properties.DataSource = dvg

            tjalur.EditValue = "-"

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

    'Private Sub load2()

    '    Dim nobukti As String = "XXXXXXXXX"

    '    If Not IsNothing(dv1) Then
    '        If dv1.Count > 0 Then

    '            For i As Integer = 0 To dv1.Count - 1

    '                If i > 0 Then
    '                    nobukti = String.Format(",{0}", nobukti)
    '                End If

    '                nobukti = String.Format("{0}{1}",nobukti,dv1(i)

    '            Next

    '        End If
    '    End If


    'End Sub

    Private Function cekbukti(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim tahun As String = Year(ttgl.EditValue)
        tahun = Microsoft.VisualBasic.Right(tahun, 2)
        Dim bulan As String = Month(ttgl.EditValue)

        If bulan.Length = 1 Then
            bulan = "0" & bulan
        End If

        Dim bulantahun As String = String.Format("{0}{1}", tahun, bulan)

        Dim sql As String = String.Format("select max(nobukti) from trrekap_to where nobukti like '%RKF.{0}%'", bulantahun)

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

        Return String.Format("RKF.{0}{1}{2}", tahun, bulan, kbukti)

    End Function

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            If cek_campur_kosongNon(cn) = True Then
                MsgBox("Faktur kosong tidak boleh digabung dengan Faktur TO", vbOKOnly + vbInformation, "Informasi")
                Return
            Else

                If stat_fak_kosong = True Then

                    Using fs As New frekap_to5 With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
                        fs.ShowDialog(Me)

                        jmlgallon_fak = fs.get_jml

                    End Using

                    If jmlgallon_fak = 0 Then
                        Return
                    End If

                End If

            End If


            open_wait()

            sqltrans = cn.BeginTransaction

            Dim cmd As OleDbCommand

            Dim tnota As String
            tnota = GridView1.Columns("alamat_toko").SummaryItem.SummaryValue.ToString

            If addstat Then
                Dim bukti As String = cekbukti(cn, sqltrans)
                tbukti.EditValue = bukti

                '0. insert rekap
                Dim sqlin As String = String.Format("insert into trrekap_to (nobukti,tgl,tglmuat,tglkirim,kd_supir,kd_kenek1,kd_kenek2,kd_kenek3,kd_jalur,nopol,note,tot_nota,sfaktur_kosong) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',{11},{12})", _
                                                        tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_mt.EditValue), convert_date_to_eng(ttgl_krm.EditValue), tkd_supir.Text.Trim, _
                                                        tkd_ken1.Text.Trim, tkd_ken2.Text.Trim, tkd_ken3.Text.Trim, tjalur.EditValue, tnopol.EditValue, tket.Text.Trim, tnota, IIf(stat_fak_kosong = True, 1, 0))


                cmd = New OleDbCommand(sqlin, cn, sqltrans)
                cmd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btrekap_to", 1, 0, 0, 0, tbukti.Text.Trim, tnopol.EditValue, sqltrans)



            Else

                '1. update rekap
                Dim sqlup As String = String.Format("update trrekap_to set tgl='{0}',tglmuat='{1}',tglkirim='{2}',kd_supir='{3}',kd_kenek1='{4}',kd_kenek2='{5}',kd_kenek3='{6}',kd_jalur='{7}',nopol='{8}',note='{9}',tot_nota={10},sfaktur_kosong={11} where nobukti='{12}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_mt.EditValue), convert_date_to_eng(ttgl_krm.EditValue), tkd_supir.Text.Trim, _
                                                        tkd_ken1.Text.Trim, tkd_ken2.Text.Trim, tkd_ken3.Text.Trim, tjalur.EditValue, tnopol.EditValue, tket.Text.Trim, tnota, IIf(stat_fak_kosong = True, 1, 0), tbukti.Text.Trim)

                cmd = New OleDbCommand(sqlup, cn, sqltrans)
                cmd.ExecuteNonQuery()


                Clsmy.InsertToLog(cn, "btrekap_to", 0, 1, 0, 0, tbukti.Text.Trim, tnopol.EditValue, sqltrans)
            End If

            If simpan2(cn, sqltrans) = "ok" Then
                '------------------------------

                If addstat = True Then
                    insertview()
                Else
                    updateview()
                End If

                sqltrans.Commit()

                close_wait()

                ' MsgBox("Data disimpan...", vbOKOnly + vbInformation, "Informasi")

                If addstat = True Then

                    If statprint Then
                        If MsgBox("Rekap akan langsung diprint.. ???", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
                            load_print()
                        End If
                    End If
                    
                    kosongkan()
                    tkd_supir.Focus()
                Else
                    close_wait()

                    If statprint Then
                        If MsgBox("Rekap akan langsung diprint.. ???", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
                            load_print()
                        End If
                    End If

                    Me.Close()
                End If

                '----------------------------------
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

    Private Sub cekjmlprint()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqlupprint As String = String.Format("update trrekap_to set jmlprint=jmlprint+1 where nobukti='{0}'", tbukti.Text.Trim)
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


    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            'Dim sql As String = String.Format("SELECT     trrekap_to.nobukti, trrekap_to.tgl, trrekap_to.tglmuat, trrekap_to.tglkirim, ms_supir.nama_karyawan AS nama_supir, ms_kenek1.nama_karyawan AS nama_kenek1, " & _
            '          "ms_kenek2.nama_karyawan AS nama_kenek2, ms_kenek3.nama_karyawan AS nama_kenek3, ms_jalur_kirim.nama_jalur, trrekap_to.nopol, trrekap_to.note, " & _
            '          "ms_barang.nama_barang, trfaktur_to2.qtykecil, ms_barang.satuan1, ms_barang.satuan2, ms_barang.satuan3, ms_gudang.kd_gudang, ms_gudang.nama_gudang,  " & _
            '          "ms_barang.qty1, ms_barang.qty2, ms_barang.qty3,ms_barang.nourut_lap,trrekap_to.tot_nota " & _
            '            "FROM         trrekap_to INNER JOIN " & _
            '          "trrekap_to2 ON trrekap_to.nobukti = trrekap_to2.nobukti INNER JOIN " & _
            '          "trfaktur_to ON trrekap_to2.nobukti_fak = trfaktur_to.nobukti INNER JOIN " & _
            '          "trfaktur_to2 ON trfaktur_to.nobukti = trfaktur_to2.nobukti INNER JOIN " & _
            '          "ms_barang ON trfaktur_to2.kd_barang = ms_barang.kd_barang INNER JOIN " & _
            '          "ms_gudang ON trfaktur_to2.kd_gudang = ms_gudang.kd_gudang LEFT OUTER JOIN " & _
            '          "ms_jalur_kirim ON trrekap_to.kd_jalur = ms_jalur_kirim.kd_jalur LEFT OUTER JOIN " & _
            '          "ms_pegawai AS ms_kenek3 ON trrekap_to.kd_kenek3 = ms_kenek3.kd_karyawan LEFT OUTER JOIN " & _
            '          "ms_pegawai AS ms_kenek2 ON trrekap_to.kd_kenek2 = ms_kenek2.kd_karyawan LEFT OUTER JOIN " & _
            '          "ms_pegawai AS ms_kenek1 ON trrekap_to.kd_kenek1 = ms_kenek1.kd_karyawan LEFT OUTER JOIN " & _
            '          "ms_pegawai AS ms_supir ON trrekap_to.kd_supir = ms_supir.kd_karyawan " & _
            '            "WHERE trrekap_to.sbatal = 0 AND ms_barang.jenis = 'FISIK' AND trrekap_to.nobukti = '{0}'", tbukti.Text.Trim)

            Dim nobukti As String = tbukti.Text.Trim

            Dim sql As String = String.Format("SELECT     trrekap_to.nobukti, trrekap_to.tgl, trrekap_to.tglmuat, trrekap_to.tglkirim, trrekap_to.nopol, supir.nama_karyawan AS nama_supir, " & _
                      "kenek1.nama_karyawan AS nama_kenek1, kenek2.nama_karyawan AS nama_kenek2, kenek3.nama_karyawan AS nama_kenek3, ms_jalur_kirim.nama_jalur, " & _
                      "trrekap_to.tot_nota, ms_barang.kd_barang, ms_barang.nama_lap as nama_barang, (trrekap_to3.jml / (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3)) as jml , ms_barang.satuan1,ms_barang.nourut_lap as nohrus " & _
                      "FROM         ms_pegawai AS kenek1 RIGHT OUTER JOIN " & _
                      "ms_pegawai AS kenek2 RIGHT OUTER JOIN " & _
                      "ms_jalur_kirim RIGHT OUTER JOIN " & _
                      "trrekap_to INNER JOIN " & _
                      "trrekap_to3 ON trrekap_to.nobukti = trrekap_to3.nobukti INNER JOIN " & _
                      "ms_barang ON trrekap_to3.kd_barang = ms_barang.kd_barang ON ms_jalur_kirim.kd_jalur = trrekap_to.kd_jalur LEFT OUTER JOIN " & _
                      "ms_pegawai AS kenek3 ON trrekap_to.kd_kenek3 = kenek3.kd_karyawan ON kenek2.kd_karyawan = trrekap_to.kd_kenek2 ON  " & _
                      "kenek1.kd_karyawan = trrekap_to.kd_kenek1 LEFT OUTER JOIN " & _
                      "ms_pegawai AS supir ON trrekap_to.kd_supir = supir.kd_karyawan " & _
                      "WHERE not(ms_barang.kd_barang in ('BN0002','BN0003')) and trrekap_to.sbatal=0 and trrekap_to.nobukti='{0}'", nobukti)

            Dim sqlsub As String = String.Format("SELECT     trrekap_to.nobukti, " & _
            "ms_barang.kd_barang, ms_barang.nama_lap as nama_barang, sum(trrekap_to3.jml) as jml " & _
            "FROM         ms_pegawai AS kenek1 RIGHT OUTER JOIN  " & _
            "ms_pegawai AS kenek2 RIGHT OUTER JOIN  " & _
            "ms_jalur_kirim RIGHT OUTER JOIN  " & _
            "trrekap_to INNER JOIN  " & _
            "trrekap_to3 ON trrekap_to.nobukti = trrekap_to3.nobukti INNER JOIN  " & _
            "ms_barang ON trrekap_to3.kd_barang = ms_barang.kd_barang ON ms_jalur_kirim.kd_jalur = trrekap_to.kd_jalur LEFT OUTER JOIN  " & _
            "ms_pegawai AS kenek3 ON trrekap_to.kd_kenek3 = kenek3.kd_karyawan ON kenek2.kd_karyawan = trrekap_to.kd_kenek2 ON   " & _
            "kenek1.kd_karyawan = trrekap_to.kd_kenek1 LEFT OUTER JOIN  " & _
            "ms_pegawai AS supir ON trrekap_to.kd_supir = supir.kd_karyawan  " & _
            "WHERE ms_barang.kd_barang in ('BN0002','BN0003') and trrekap_to.sbatal=0 and trrekap_to.nobukti='{0}' " & _
            "group by trrekap_to.nobukti,ms_barang.kd_barang, ms_barang.nama_lap", nobukti)

            Dim ds As DataSet = New dsrekap2
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dsbonus As DataSet = New DataSet
            dsbonus = Clsmy.GetDataSet(sqlsub, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_rekapfak2() With {.DataSource = ds.Tables(0)}
            rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
            rrekap.DataMember = rrekap.DataMember

            If dsbonus.Tables(0).Rows.Count > 0 Then
                If Integer.Parse(dsbonus.Tables(0).Rows(0)("jml").ToString) > 0 Then
                    rrekap.XrSubreport1.ReportSource = New r_rekapfak_bonus
                    rrekap.XrSubreport1.ReportSource.DataSource = dsbonus.Tables(0)
                    rrekap.XrSubreport1.ReportSource.DataMember = rrekap.XrSubreport1.ReportSource.DataMember
                Else
                    rrekap.XrSubreport1.Visible = False
                End If
            Else
                rrekap.XrSubreport1.Visible = False
            End If

            rrekap.XrSubreport2.ReportSource = New r_rekapfak2_detail
            rrekap.XrSubreport2.ReportSource.DataSource = ds.Tables(0)
            rrekap.XrSubreport2.ReportSource.DataMember = rrekap.XrSubreport2.ReportSource.DataMember

            rrekap.PrinterName = varprinter2
            rrekap.CreateDocument(True)
            rrekap.Print()

            ' PrintControl1.PrintingSystem = rrekap.PrintingSystem

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

    End Sub

    Private Function cek_campur_kosongNon(ByVal cn As OleDbConnection) As Boolean

        stat_fak_kosong = False
        Dim hasil As Boolean = False

        Dim sqlgabung As String = ""
        Dim sqltoko As String = ""
        For i As Integer = 0 To dv1.Count - 1
            If i = 0 Then
                sqlgabung = String.Format("'{0}'", dv1(i)("nobukti_fak").ToString)
                sqltoko = String.Format("'{0}'", dv1(i)("kd_toko").ToString)
            Else

                ' If i <> dv1.Count - 1 Then
                sqlgabung = String.Format("{0},", sqlgabung)
                sqltoko = String.Format("{0},", sqltoko)
                'End If

                sqlgabung = String.Format("{0}'{1}'", sqlgabung, dv1(i)("nobukti_fak").ToString)
                sqltoko = String.Format("{0}'{1}'", sqltoko, dv1(i)("kd_toko").ToString)
            End If
        Next

        Dim sql As String = String.Format("select sum(trfaktur_to2.qtykecil),trfaktur_to.nobukti,trfaktur_to.kd_toko from trfaktur_to2 inner join trfaktur_to on " & _
        "trfaktur_to2.nobukti=trfaktur_to.nobukti where trfaktur_to.nobukti in ({0}) " & _
        "and (trfaktur_to2.kd_barang in (select kd_barang from ms_barang where len(kd_barang_jmn)>0) or " & _
        "trfaktur_to2.kd_barang in (select kd_barang_jmn from ms_barang where len(kd_barang_jmn)>0)) " & _
        "group by trfaktur_to.nobukti,trfaktur_to.kd_toko " & _
        "having sum(trfaktur_to2.qtykecil)=0", sqlgabung)

        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim adakosong As Boolean = False
        If drd.Read Then
            If IsNumeric(drd(0).ToString) Then
                adakosong = True
            End If
        End If
        drd.Close()

        If adakosong Then
            '    hasil = True
            Dim sql2 As String = String.Format("select kd_toko from ms_toko where spred_to=0 and kd_toko in ({0})", sqltoko)
            Dim cmd2 As OleDbCommand = New OleDbCommand(sql2, cn)
            Dim drd2 As OleDbDataReader = cmd2.ExecuteReader

            If drd2.Read Then
                If Not drd2(0).ToString.Equals("") Then
                    ' If Integer.Parse(drd2(1).ToString) = 1 Then
                    hasil = True
                    'End If

                End If
            End If
            drd2.Close()

        End If

        If hasil = False And adakosong = True Then
            stat_fak_kosong = True
        End If

        Return hasil

    End Function

    Private Function simpan2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim cmdc As OleDbCommand
        Dim drdc As OleDbDataReader
        Dim hasil As String = ""

        For i As Integer = 0 To dv1.Count - 1

            If Not dv1(i)("noid").Equals(0) Then

                If stat_fak_kosong = False Then

                    Dim sqlc As String = String.Format("select * from trfaktur_to2 inner join ms_barang on trfaktur_to2.kd_barang=ms_barang.kd_barang inner join trfaktur_to on trfaktur_to.nobukti=trfaktur_to2.nobukti where ms_barang.jenis='FISIK' and trfaktur_to2.nobukti='{0}'", dv1(i)("nobukti_fak").ToString)
                    cmdc = New OleDbCommand(sqlc, cn, sqltrans)
                    drdc = cmdc.ExecuteReader

                    While drdc.Read

                        Dim qty1 As Integer = Integer.Parse(drdc("qty1").ToString)
                        Dim qty2 As Integer = Integer.Parse(drdc("qty2").ToString)
                        Dim qty3 As Integer = Integer.Parse(drdc("qty3").ToString)
                        Dim kd_toko As String = drdc("kd_toko").ToString

                        Dim qtykecil As Integer = Integer.Parse(drdc("qtykecil0").ToString)
                        Dim kdbar As String = drdc("kd_barang").ToString
                        Dim kdgud As String = drdc("kd_gudang").ToString

                        '3. insert to hist stok
                        If addstat = False Then
                            If DateValue(tglmuat_old) <> DateValue(ttgl_mt.EditValue) Or supir_old <> tkd_supir.EditValue Or nopol_old <> tnopol.EditValue Then
                                Clsmy.Insert_HistBarang(cn, sqltrans, dv1(i)("nobukti_fak").ToString, tglmuat_old, kdgud, kdbar, qtykecil, 0, "Jual TO (Edit)", supir_old, nopol_old)
                            End If

                            Clsmy.Insert_HistBarang(cn, sqltrans, dv1(i)("nobukti_fak").ToString, ttgl_mt.EditValue, kdgud, kdbar, 0, qtykecil, "Jual TO (Edit)", tkd_supir.Text.Trim, tnopol.EditValue)

                        End If

                    End While

                End If

            Else
                Dim sqlins As String = String.Format("insert into trrekap_to2 (nobukti,nobukti_fak,statkirim) values('{0}','{1}','BELUM TERKIRIM')", tbukti.Text.Trim, dv1(i)("nobukti_fak").ToString)

                Using cmd As OleDbCommand = New OleDbCommand(sqlins, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

                Dim sqlup As String = String.Format("update trfaktur_to set skirim=1 where nobukti='{0}'", dv1(i)("nobukti_fak").ToString)
                Using cmd2 As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                    cmd2.ExecuteNonQuery()
                End Using


                If stat_fak_kosong = True Then
                    GoTo lompati_karna_faktur_kosong
                End If

                Dim sqlc As String = String.Format("select * from trfaktur_to2 inner join ms_barang on trfaktur_to2.kd_barang=ms_barang.kd_barang inner join trfaktur_to on trfaktur_to.nobukti=trfaktur_to2.nobukti where ms_barang.jenis='FISIK' and trfaktur_to2.nobukti='{0}'", dv1(i)("nobukti_fak").ToString)
                cmdc = New OleDbCommand(sqlc, cn, sqltrans)
                drdc = cmdc.ExecuteReader

                While drdc.Read

                    Dim qty1 As Integer = Integer.Parse(drdc("qty1").ToString)
                    Dim qty2 As Integer = Integer.Parse(drdc("qty2").ToString)
                    Dim qty3 As Integer = Integer.Parse(drdc("qty3").ToString)
                    Dim kd_toko As String = drdc("kd_toko").ToString

                    Dim qtykecil As Integer = Integer.Parse(drdc("qtykecil0").ToString)
                    Dim kdbar As String = drdc("kd_barang").ToString
                    Dim kdgud As String = drdc("kd_gudang").ToString

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, False, False, False)
                    If Not hasilplusmin.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        Exit For
                    End If

                    '3. insert to hist stok
                    If addstat = False Then
                        If DateValue(tglmuat_old) <> DateValue(ttgl_mt.EditValue) Or supir_old <> tkd_supir.EditValue Or nopol_old <> tnopol.EditValue Then
                            Clsmy.Insert_HistBarang(cn, sqltrans, dv1(i)("nobukti_fak").ToString, tglmuat_old, kdgud, kdbar, qtykecil, 0, "Jual TO", supir_old, nopol_old)
                        End If

                    End If

                    Clsmy.Insert_HistBarang(cn, sqltrans, dv1(i)("nobukti_fak").ToString, ttgl_mt.EditValue, kdgud, kdbar, 0, qtykecil, "Jual TO", tkd_supir.Text.Trim, tnopol.EditValue)


                    '' masukkan ke rekap 3 yaitu total barangnya
                    Dim sqlcek3 As String = String.Format("select noid,jml from trrekap_to3 where nobukti='{0}' and kd_barang='{1}'", tbukti.Text.Trim, kdbar)
                    Dim cmdcek3 As OleDbCommand = New OleDbCommand(sqlcek3, cn, sqltrans)
                    Dim drdcek3 As OleDbDataReader = cmdcek3.ExecuteReader

                    Dim noidrek3 As Integer = 0
                    Dim jmlrek3 As Integer = 0

                    If drdcek3.Read Then
                        If IsNumeric(drdcek3(0).ToString) Then
                            noidrek3 = Integer.Parse(drdcek3(0).ToString)
                            jmlrek3 = Integer.Parse(drdcek3(1).ToString)
                        End If
                    End If
                    drdcek3.Close()

                    If noidrek3 = 0 Then

                        Dim sqlins3 As String = String.Format("insert into trrekap_to3 (nobukti,kd_barang,jml) values('{0}','{1}',{2})", tbukti.Text.Trim, kdbar, qtykecil)
                        Using cmdtok3 As OleDbCommand = New OleDbCommand(sqlins3, cn, sqltrans)
                            cmdtok3.ExecuteNonQuery()
                        End Using

                    Else

                        jmlrek3 = jmlrek3 + qtykecil

                        Dim sqlup3 As String = String.Format("update trrekap_to3 set jml=jml+{0} where noid='{1}'", qtykecil, noidrek3)
                        Using cmdtok3 As OleDbCommand = New OleDbCommand(sqlup3, cn, sqltrans)
                            cmdtok3.ExecuteNonQuery()
                        End Using

                    End If

                End While

                drdc.Close()

lompati_karna_faktur_kosong:

            End If

        Next

        ' khusus nota kosong
        If stat_fak_kosong = True Then
            If addstat = False Then

                Dim sqlcek As String = String.Format("select jml,noid from trrekap_to3 where nobukti='{0}'", tbukti.Text.Trim)
                Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn, sqltrans)
                Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

                Dim jmlold As Integer = 0
                Dim noid3 As Integer = 0

                If drdcek.Read Then
                    If IsNumeric(drdcek(0).ToString) Then
                        jmlold = Integer.Parse(drdcek(0).ToString)
                        noid3 = Integer.Parse(drdcek(1).ToString)
                    End If
                End If
                drdcek.Close()

                If jmlold > 0 Then

                    '3. insert to hist stok
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, "G000", "G0001", jmlold, 0, "Jual TO Kanvas (Edit)", tkd_supir.Text.Trim, tnopol.EditValue)


                    '' kurangi
                    Dim sqlup3 As String = String.Format("update trrekap_to3 set jml=jml-{0} where noid='{1}'", jmlold, noid3)
                    Using cmdtok3 As OleDbCommand = New OleDbCommand(sqlup3, cn, sqltrans)
                        cmdtok3.ExecuteNonQuery()
                    End Using

                    '' tambah lagi
                    Dim sqlup31 As String = String.Format("update trrekap_to3 set jml=jml + {0} where noid='{1}'", jmlgallon_fak, noid3)
                    Using cmdtok31 As OleDbCommand = New OleDbCommand(sqlup31, cn, sqltrans)
                        cmdtok31.ExecuteNonQuery()
                    End Using

                    '2. update barang
                    Dim hasilplusmin2 As String = PlusMin_Barang(cn, sqltrans, jmlold, "G0001", "G000", True, False, False)
                    If Not hasilplusmin2.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin2, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                    End If

                End If

                GoTo masuk_wilayah_tambah

            Else

                Dim sqlins3 As String = String.Format("insert into trrekap_to3 (nobukti,kd_barang,jml) values('{0}','{1}',{2})", tbukti.Text.Trim, "G0001", jmlgallon_fak)
                Using cmdtok3 As OleDbCommand = New OleDbCommand(sqlins3, cn, sqltrans)
                    cmdtok3.ExecuteNonQuery()
                End Using

masuk_wilayah_tambah:

                '3. insert to hist stok
                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, "G000", "G0001", 0, jmlgallon_fak, "Jual TO Kanvas", tkd_supir.Text.Trim, tnopol.EditValue)

                '2. update barang
                Dim hasilplusmin2 As String = PlusMin_Barang(cn, sqltrans, jmlgallon_fak, "G0001", "G000", False, False, False)
                If Not hasilplusmin2.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilplusmin2, vbOKOnly + vbExclamation, "Informasi")
                    hasil = "error"
                End If

            End If
        End If

        If Not hasil.Equals("error") Then
            hasil = "ok"
        End If

        Return hasil

    End Function

    Private Sub hapus()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Dim noid As Integer = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("noid"))

        Try

            If noid = 0 Then
                dv1.Delete(Me.BindingContext(dv1).Position)
            Else

                If noid = 0 Then
                    dv1.Delete(Me.BindingContext(dv1).Position)
                    Return
                End If

                open_wait()

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                sqltrans = cn.BeginTransaction

                Dim sqldel As String = String.Format("delete from trrekap_to2 where noid={0}", noid)
                Using cmd As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

                Dim sqlup As String = String.Format("update trfaktur_to set skirim=0 where nobukti='{0}'", dv1(Me.BindingContext(dv1).Position)("nobukti_fak").ToString)
                Using cmd2 As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                    cmd2.ExecuteNonQuery()
                End Using


                Dim sqlc As String = String.Format("select * from trfaktur_to2 inner join ms_barang on trfaktur_to2.kd_barang=ms_barang.kd_barang inner join trfaktur_to on trfaktur_to.nobukti=trfaktur_to2.nobukti where ms_barang.jenis='FISIK' and trfaktur_to2.nobukti='{0}'", dv1(Me.BindingContext(dv1).Position)("nobukti_fak").ToString)
                Dim cmdc = New OleDbCommand(sqlc, cn, sqltrans)
                Dim drdc As OleDbDataReader = cmdc.ExecuteReader

                While drdc.Read

                    Dim qty1 As Integer = Integer.Parse(drdc("qty1").ToString)
                    Dim qty2 As Integer = Integer.Parse(drdc("qty2").ToString)
                    Dim qty3 As Integer = Integer.Parse(drdc("qty3").ToString)
                    Dim kd_toko As String = drdc("kd_toko").ToString

                    Dim qtykecil As Integer = Integer.Parse(drdc("qtykecil0").ToString)
                    Dim kdbar As String = drdc("kd_barang").ToString
                    Dim kdgud As String = drdc("kd_gudang").ToString

                    'If qtykecil = 0 Then
                    '    Dim sqltok As String = String.Format("select spred_to from ms_toko where kd_toko='{0}'", kd_toko)
                    '    Dim cmdtok As OleDbCommand = New OleDbCommand(sqltok, cn, sqltrans)
                    '    Dim drtok As OleDbDataReader = cmdtok.ExecuteReader

                    '    Dim spread_to As Integer = 0

                    '    If drtok.Read Then
                    '        spread_to = drtok(0).ToString
                    '    End If
                    ''    drtok.Close()


                    '    If spread_to = 0 Then

                    '        close_wait()
                    '        MsgBox("Jumlah jual tidak boleh kosong", vbOKOnly + vbExclamation, "Informasi")
                    '        GoTo langsung

                    '    End If

                    '    qtykecil = 200
                    '    kdbar = "G0001"
                    '    kdgud = "G000"

                    'End If

                    ' cek apakah barang kosong
                    'Dim sqlcekapa As String = String.Format("select kd_barang_kmb from ms_barang where kd_barang_kmb='{0}'", kdbar)
                    'Dim cmdcekapa As OleDbCommand = New OleDbCommand(sqlcekapa, cn, sqltrans)
                    'Dim drapa As OleDbDataReader = cmdcekapa.ExecuteReader

                    'If drapa.Read Then
                    '    If Not drapa(0).ToString.Equals("") Then

                    '        Dim sqlkosong As String = String.Format("select noid,jml from ms_toko4 where kd_toko='{0}' and kd_barang='{1}'", kd_toko, kdbar)
                    '        Dim cmdkosong As OleDbCommand = New OleDbCommand(sqlkosong, cn, sqltrans)
                    '        Dim drkosong As OleDbDataReader = cmdkosong.ExecuteReader

                    '        If drkosong.Read Then

                    '            If IsNumeric(drkosong(0).ToString) Then

                    '                Dim totqty As Integer = qty1 * qty2 * qty3
                    '                Dim hasilqty As Integer = Integer.Parse(drkosong(1).ToString)
                    '                Dim jml1 As Integer = 0
                    '                Dim jml2 As Integer = 0
                    '                Dim jml3 As Integer = 0

                    '                hasilqty = hasilqty - qtykecil

                    '                If hasilqty >= totqty Then

                    '                    Dim sisa As Integer = hasilqty Mod totqty

                    '                    jml1 = (hasilqty - sisa) / totqty

                    '                    If sisa > qty2 Then
                    '                        jml2 = sisa
                    '                        sisa = sisa Mod qty2

                    '                        jml2 = (jml2 - sisa) / qty2
                    '                        jml3 = sisa

                    '                    Else
                    '                        jml2 = sisa
                    '                        jml3 = 0
                    '                    End If
                    '                Else
                    '                    hasilqty = 0
                    '                    jml1 = 0
                    '                    jml2 = 0
                    '                    jml3 = 0
                    '                End If

                    '                Dim sqlup_ks As String = String.Format("update ms_toko4 set jml={0},jml1={1},jml2={2},jml3={3} where noid='{4}'", hasilqty, jml1, jml2, jml3, drkosong(0).ToString)
                    '                Using cmdup_ks As OleDbCommand = New OleDbCommand(sqlup_ks, cn, sqltrans)
                    '                    cmdup_ks.ExecuteNonQuery()
                    '                End Using

                    '            End If

                    '        End If
                    '        drkosong.Close()

                    '    End If
                    'End If
                    'drapa.Close()

                    If stat_fak_kosong = False Then

                        '2. update barang
                        Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, True, False, False)
                        If Not hasilplusmin.Equals("ok") Then
                            close_wait()

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                            GoTo langsung
                        End If

                        '3. insert to hist stok
                        If DateValue(tglmuat_old) <> DateValue(ttgl_mt.EditValue) Or nopol_old <> tnopol.EditValue Or supir_old <> tkd_supir.EditValue Then
                            Clsmy.Insert_HistBarang(cn, sqltrans, dv1(Me.BindingContext(dv1).Position)("nobukti_fak").ToString, tglmuat_old, kdgud, kdbar, qtykecil, 0, "Jual TO", supir_old, nopol_old)
                        Else
                            Clsmy.Insert_HistBarang(cn, sqltrans, dv1(Me.BindingContext(dv1).Position)("nobukti_fak").ToString, ttgl_mt.EditValue, kdgud, kdbar, qtykecil, 0, "Jual TO", tkd_supir.Text.Trim, tnopol.EditValue)
                        End If



                        ' masukkan ke rekap 3 yaitu total barangnya
                        Dim sqlcek3 As String = String.Format("select noid from trrekap_to3 where nobukti='{0}' and kd_barang='{1}'", tbukti.Text.Trim, kdbar)
                        Dim cmdcek3 As OleDbCommand = New OleDbCommand(sqlcek3, cn, sqltrans)
                        Dim drdcek3 As OleDbDataReader = cmdcek3.ExecuteReader

                        Dim noidrek3 As Integer = 0
                        If drdcek3.Read Then
                            If IsNumeric(drdcek3(0).ToString) Then
                                noidrek3 = Integer.Parse(drdcek3(0).ToString)
                            End If
                        End If
                        drdcek3.Close()

                        If noidrek3 = 0 Then
                        Else
                            Dim sqlup3 As String = String.Format("update trrekap_to3 set jml=jml-{0} where noid='{1}'", qtykecil, noidrek3)
                            Using cmdtok3 As OleDbCommand = New OleDbCommand(sqlup3, cn, sqltrans)
                                cmdtok3.ExecuteNonQuery()
                            End Using

                        End If
                    End If


                End While

                drdc.Close()

                sqltrans.Commit()

                dv1.Delete(Me.BindingContext(dv1).Position)

                close_wait()

            End If

langsung:

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

    Private Sub insertview()

        Dim tnota As String
        tnota = GridView1.Columns("alamat_toko").SummaryItem.SummaryValue.ToString

        Dim orow As DataRowView = dv.AddNew
        orow("sbatal") = 0
        orow("smuat") = 0
        orow("spulang") = 0
        orow("nobukti") = tbukti.Text.Trim
        orow("tgl") = ttgl.EditValue
        orow("tglmuat") = ttgl_mt.EditValue
        orow("tglkirim") = ttgl_krm.EditValue
        orow("kd_supir") = tkd_supir.Text.Trim
        orow("namasupir") = tnama_supir.Text.Trim
        orow("nopol") = tnopol.EditValue
        orow("kd_jalur") = tjalur.EditValue
        orow("nama_jalur") = tjalur.Text.Trim
        orow("note") = tket.Text.Trim
        orow("kd_kenek1") = tkd_ken1.Text.Trim
        orow("kd_kenek2") = tkd_ken2.Text.Trim
        orow("kd_kenek3") = tkd_ken3.Text.Trim
        orow("tot_nota") = Integer.Parse(tnota)
        orow("sfaktur_kosong") = IIf(stat_fak_kosong = True, 1, 0)

        dv.EndInit()

    End Sub

    Private Sub updateview()

        Dim tnota As String
        tnota = GridView1.Columns("alamat_toko").SummaryItem.SummaryValue.ToString
        '    tnota = tnota.Substring(10, tnota.Length - 10)

        '  dv(position)("nobukti") = tbukti.Text.Trim
        dv(position)("tgl") = ttgl.EditValue
        dv(position)("tglmuat") = ttgl_mt.EditValue
        dv(position)("tglkirim") = ttgl_krm.EditValue
        dv(position)("kd_supir") = tkd_supir.Text.Trim
        dv(position)("namasupir") = tnama_supir.Text.Trim
        dv(position)("nopol") = tnopol.EditValue
        dv(position)("kd_jalur") = tjalur.EditValue
        dv(position)("nama_jalur") = tjalur.Text.Trim
        dv(position)("note") = tket.Text.Trim
        dv(position)("kd_kenek1") = tkd_ken1.Text.Trim
        dv(position)("kd_kenek2") = tkd_ken2.Text.Trim
        dv(position)("kd_kenek3") = tkd_ken3.Text.Trim
        dv(position)("tot_nota") = Integer.Parse(tnota)
        dv(position)("sfaktur_kosong") = IIf(stat_fak_kosong = True, 1, 0)

    End Sub

    Private Sub cek_supirkenek(ByVal cn As OleDbConnection)

        tkd_ken1.Text = ""
        tnama_ken1.Text = ""

        tkd_ken2.Text = ""
        tnama_ken2.Text = ""

        tkd_ken3.Text = ""
        tnama_ken3.Text = ""

        If IsNothing(cn) Then
            cn = New OleDbConnection
            cn = Clsmy.open_conn
        End If

        Dim sql As String = String.Format("SELECT    ms_supirkenek.noid, ms_supirkenek.kd_kenek1, kenek1.nama_karyawan AS nama_kenek1, ms_supirkenek.kd_kenek2, kenek2.nama_karyawan AS nama_kenek2, " & _
                  "ms_supirkenek.kd_kenek3, kenek3.nama_karyawan AS nama_kenek3,ms_supirkenek.nopol " & _
                "FROM         ms_supirkenek LEFT OUTER JOIN " & _
                  "ms_pegawai AS kenek3 ON ms_supirkenek.kd_kenek3 = kenek3.kd_karyawan LEFT OUTER JOIN " & _
                  "ms_pegawai AS kenek2 ON ms_supirkenek.kd_kenek2 = kenek2.kd_karyawan LEFT OUTER JOIN " & _
                  "ms_pegawai AS kenek1 ON ms_supirkenek.kd_kenek1 = kenek1.kd_karyawan " & _
                  "WHERE ms_supirkenek.kd_supir='{0}'", tkd_supir.Text.Trim)

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            Dim ada As Boolean = False


            If drd.Read Then

                If IsNumeric(drd("noid").ToString) Then

                    ada = True


                    Dim kd_kenek1 As String = drd("kd_kenek1").ToString
                    Dim kd_kenek2 As String = drd("kd_kenek2").ToString
                    Dim kd_kenek3 As String = drd("kd_kenek3").ToString

                Dim namakenek1 As String = drd("nama_kenek1").ToString
                Dim namakenek2 As String = drd("nama_kenek2").ToString
                Dim namakenek3 As String = drd("nama_kenek3").ToString

                    tkd_ken1.Text = kd_kenek1
                    tkd_ken2.Text = kd_kenek2
                    tkd_ken3.Text = kd_kenek3

                tnama_ken1.Text = namakenek1
                tnama_ken2.Text = namakenek2
                tnama_ken3.Text = namakenek3

                'tnopol.EditValue = drd("nopol").ToString

                ' tkd_ken1_Validated(Nothing, Nothing)
                'tkd_ken2_Validated(Nothing, Nothing)
                'tkd_ken2_Validated(Nothing, Nothing)

                End If

            End If
            drd.Close()

            If ada = False Then
                tkd_ken1.Text = ""
                tkd_ken2.Text = ""
                tkd_ken3.Text = ""

                tnama_ken1.Text = ""
                tnama_ken2.Text = ""
                tnama_ken3.Text = ""
            End If


    End Sub

    '' supir

    Private Sub bts_supir_Click(sender As System.Object, e As System.EventArgs) Handles bts_supir.Click

        Dim fs As New fssupir With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .issales = True}
        fs.ShowDialog(Me)

        tkd_supir.EditValue = fs.get_KODE
        ' tnama_supir.EditValue = fs.get_NAMA

        tkd_supir_Validated(sender, Nothing)

        If addstat Then
            cek_supirkenek(Nothing)
        End If

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
        Else

            If addstat Then
                cek_supirkenek(Nothing)
            End If

        End If
    End Sub

    Private Sub tkd_supir_Validated(sender As Object, e As System.EventArgs) Handles tkd_supir.Validated
        If tkd_supir.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and (bagian like 'SUPIR%' or bagian='SALES') and kd_karyawan='{0}'", tkd_supir.Text.Trim)

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

                ' cek_supirkenek(cn)

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Finally


                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If
            End Try

        Else

            tkd_ken1.Text = ""
            tnama_ken1.Text = ""

            tkd_ken2.Text = ""
            tnama_ken2.Text = ""

            tkd_ken3.Text = ""
            tnama_ken3.Text = ""


        End If
    End Sub

    '' kenek 1

    Private Sub bts_ken1_Click(sender As System.Object, e As System.EventArgs) Handles bts_ken1.Click

        Dim fs As New fskenek With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_ken1.EditValue = fs.get_KODE
        tnama_ken1.EditValue = fs.get_NAMA

    End Sub

    Private Sub tkd_ken1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_ken1.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_ken1_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_ken1_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_ken1.LostFocus
        If tkd_ken1.Text.Trim.Length = 0 Then
            tkd_ken1.Text = ""
            tnama_ken1.Text = ""
        End If
    End Sub

    Private Sub tkd_ken1_Validated(sender As Object, e As System.EventArgs) Handles tkd_ken1.Validated
        If tkd_ken1.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and bagian='KENEK' and kd_karyawan='{0}'", tkd_ken1.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_ken1.EditValue = dread("kd_karyawan").ToString
                        tnama_ken1.EditValue = dread("nama_karyawan").ToString

                    Else
                        tkd_ken1.EditValue = ""
                        tnama_ken1.EditValue = ""

                    End If
                Else
                    tkd_ken1.EditValue = ""
                    tnama_ken1.EditValue = ""

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

    '' kenek 2

    Private Sub bts_ken2_Click(sender As System.Object, e As System.EventArgs) Handles bts_ken2.Click

        Dim fs As New fskenek With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_ken2.EditValue = fs.get_KODE
        tnama_ken2.EditValue = fs.get_NAMA

    End Sub

    Private Sub tkd_ken2_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_ken2.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_ken2_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_ken2_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_ken2.LostFocus
        If tkd_ken2.Text.Trim.Length = 0 Then
            tkd_ken2.Text = ""
            tnama_ken2.Text = ""
        End If
    End Sub

    Private Sub tkd_ken2_Validated(sender As Object, e As System.EventArgs) Handles tkd_ken2.Validated
        If tkd_ken2.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and bagian='KENEK' and kd_karyawan='{0}'", tkd_ken2.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_ken2.EditValue = dread("kd_karyawan").ToString
                        tnama_ken2.EditValue = dread("nama_karyawan").ToString

                    Else
                        tkd_ken2.EditValue = ""
                        tnama_ken2.EditValue = ""

                    End If
                Else
                    tkd_ken2.EditValue = ""
                    tnama_ken2.EditValue = ""

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

    '' kenek 3

    Private Sub bts_ken3_Click(sender As System.Object, e As System.EventArgs) Handles bts_ken3.Click

        Dim fs As New fskenek With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_ken3.EditValue = fs.get_KODE
        tnama_ken3.EditValue = fs.get_NAMA

    End Sub

    Private Sub tkd_ken3_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_ken3.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_ken3_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_ken3_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_ken3.LostFocus
        If tkd_ken3.Text.Trim.Length = 0 Then
            tkd_ken3.Text = ""
            tnama_ken3.Text = ""
        End If
    End Sub

    Private Sub tkd_ken3_Validated(sender As Object, e As System.EventArgs) Handles tkd_ken3.Validated
        If tkd_ken3.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and bagian='KENEK' and kd_karyawan='{0}'", tkd_ken3.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_ken3.EditValue = dread("kd_karyawan").ToString
                        tnama_ken3.EditValue = dread("nama_karyawan").ToString

                    Else
                        tkd_ken3.EditValue = ""
                        tnama_ken3.EditValue = ""

                    End If
                Else
                    tkd_ken3.EditValue = ""
                    tnama_ken3.EditValue = ""

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



    Private Sub btadd_Click(sender As System.Object, e As System.EventArgs) Handles btadd.Click

        Dim fs As New frekap_to3 With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .dv = dv1}
        fs.ShowDialog(Me)

    End Sub

    Private Sub btdel_Click(sender As System.Object, e As System.EventArgs) Handles btdel.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        hapus()

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tnopol.EditValue = "" Then
            MsgBox("No Polisi kendaraan harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tnopol.Focus()
            Return
        End If

        'If tkd_supir.Text.Trim.Length = 0 Then
        '    MsgBox("Supir harus diisi...", vbOKOnly + vbInformation, "Informasi")
        '    tkd_supir.Focus()
        '    Return
        'End If

        'If tkd_ken1.Text.Trim.Length = 0 Then
        '    MsgBox("Kenek 1 harus diisi...", vbOKOnly + vbInformation, "Informasi")
        '    tkd_ken1.Focus()
        '    Return
        'End If

        If MsgBox("Yakin sudah benar ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
            simpan()
        End If

    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        If tjalur.EditValue = "" Then
            Return
        End If

        'If tjalur.Text.Trim.Equals("-") Then
        '    Return
        'End If

        Dim fs As New frekap_to4 With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .dv = dv1, .kdjalur = tjalur.EditValue, .namajalur = tjalur.Text.Trim}
        fs.ShowDialog(Me)

    End Sub

    Private Sub frekap_to2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tbukti.Focus()
    End Sub

    Private Sub frekap_to2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        isi_nopol()
        isi_jalur()

        ttgl.EditValue = Date.Now
        ttgl_mt.EditValue = Date.Now
        ttgl_krm.EditValue = Date.Now

        If addstat = False Then
            isi()
        Else
            kosongkan()
        End If

    End Sub

    Private Sub tjalur_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tjalur.EditValueChanged
        opengrid()
    End Sub

    Private Sub tnopol_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tnopol.KeyDown
        If e.KeyCode = Keys.F4 Then
            SimpleButton1_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tnopol_LostFocus(sender As Object, e As System.EventArgs) Handles tnopol.LostFocus
        If addstat Then
            ' tnopol_Validated(sender, Nothing)
            ' cek_supirkenek(Nothing)
        End If
    End Sub


    Private Sub tnopol_Validated(sender As System.Object, e As System.EventArgs)


        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select kd_supir from ms_supirkenek where nopol='{0}'", tnopol.EditValue)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            Dim ada As Boolean = False

            If drd.Read Then
                If Not drd(0).ToString.Equals("") Then

                    ada = True

                    tkd_supir.EditValue = drd(0).ToString
                    tkd_supir_Validated(sender, Nothing)
                    ' cek_supirkenek(cn)
                End If
            End If
            drd.Close()

            If ada = False Then
                tkd_supir.Text = ""
                tnama_supir.Text = ""
                tkd_supir_Validated(sender, Nothing)
            End If

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

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click

        Dim fs As New fssupirkenek With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tnopol.EditValue = fs.get_nopol
        ' tkd_supir.EditValue = fs.get_supir

        ' tkd_supir_Validated(sender, Nothing)

        If addstat Then
            '   cek_supirkenek(Nothing)
        End If

    End Sub


End Class