Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class trpindahgud2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private tgl_old As String

    Private Sub kosongkan()
        tbukti.Text = "<< New >>"

        tket.EditValue = ""

        opengrid()

    End Sub

    Private Sub isi_gudang()

        Const sql As String = "select * from ms_gudang where tipe_gudang='FISIK'"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            Dim dvgudang As DataView = dvm.CreateDataView(ds.Tables(0))

            tgudang1.Properties.DataSource = dvgudang
            tgudang2.Properties.DataSource = dvgudang

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

    Private Sub opengrid()

        Dim sql As String = String.Format("SELECT trpindahgud2.noid, trpindahgud2.kd_barang,ms_barang.nama_barang, trpindahgud2.satuan, trpindahgud2.qty, trpindahgud2.qtykecil " & _
            "FROM trpindahgud2 INNER JOIN ms_barang ON trpindahgud2.kd_barang = ms_barang.kd_barang where trpindahgud2.nobukti='{0}'", tbukti.Text.Trim)


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
        Dim sql As String = String.Format("SELECT trpindahgud.nobukti, trpindahgud.tanggal, trpindahgud.tgl_pindah, trpindahgud.kd_gudang1, trpindahgud.kd_gudang2, trpindahgud.kd_karyawan, trpindahgud.note, ms_pegawai.nama_karyawan " & _
            " FROM trpindahgud INNER JOIN ms_pegawai ON trpindahgud.kd_karyawan = ms_pegawai.kd_karyawan where trpindahgud.nobukti='{0}'", nobukti)

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

                        tkd_supir.EditValue = dread("kd_karyawan").ToString
                        tnama_supir.EditValue = dread("nama_karyawan").ToString

                        ttgl.EditValue = DateValue(dread("tanggal").ToString)
                        ttgl_mt.EditValue = DateValue(dread("tgl_pindah").ToString)

                        tgl_old = ttgl_mt.EditValue

                        tgudang1.EditValue = dread("kd_gudang1").ToString
                        tgudang2.EditValue = dread("kd_gudang2").ToString

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

    Private Function cekbukti(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim tahun As String = Year(ttgl.EditValue)
        tahun = Microsoft.VisualBasic.Right(tahun, 2)
        Dim bulan As String = Month(ttgl.EditValue)

        If bulan.Length = 1 Then
            bulan = "0" & bulan
        End If

        Dim tahunbulan As String = String.Format("{0}{1}", tahun, bulan)

        Dim sql As String = String.Format("select max(nobukti) from trpindahgud where nobukti like '%PG.{0}%'", tahunbulan)

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

        Return String.Format("PG.{0}{1}{2}", tahun, bulan, kbukti)

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

                '0. insert spm
                Dim sqlin As String = String.Format("insert into trpindahgud (nobukti,tanggal,tgl_pindah,kd_gudang1,kd_gudang2,kd_karyawan,note) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", _
                                                        tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_mt.EditValue), tgudang1.EditValue, tgudang2.EditValue, tkd_supir.Text.Trim, tket.Text.Trim)


                cmd = New OleDbCommand(sqlin, cn, sqltrans)
                cmd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btpindahgud", 1, 0, 0, 0, tbukti.Text.Trim, ttgl_mt.EditValue, sqltrans)



            Else

                '1. update rekap
                Dim sqlup As String = String.Format("update trpindahgud set tanggal='{0}',tgl_pindah='{1}',kd_gudang1='{2}',kd_gudang2='{3}',kd_karyawan='{4}',note='{5}' where nobukti='{6}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_mt.EditValue), tgudang1.EditValue, tgudang2.EditValue, tkd_supir.Text.Trim, tket.Text.Trim, tbukti.Text.Trim)

                cmd = New OleDbCommand(sqlup, cn, sqltrans)
                cmd.ExecuteNonQuery()


                Clsmy.InsertToLog(cn, "btpindahgud", 0, 1, 0, 0, tbukti.Text.Trim, ttgl_mt.EditValue, sqltrans)
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

                MsgBox("Data disimpan...", vbOKOnly + vbInformation, "Informasi")

                If addstat = True Then
                    kosongkan()
                    ttgl.Focus()
                Else
                    close_wait()
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

    Private Function simpan2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        'Dim cmdc As OleDbCommand
        'Dim drdc As OleDbDataReader
        Dim hasil As String = ""

        Dim kdbar As String
        Dim satuan As String
        Dim qty, qtykecil As Integer

        For i As Integer = 0 To dv1.Count - 1

            kdbar = dv1(i)("kd_barang").ToString
            satuan = dv1(i)("satuan").ToString
            qty = Integer.Parse(dv1(i)("qty").ToString)
            qtykecil = Integer.Parse(dv1(i)("qtykecil").ToString)

            If Not dv1(i)("noid").Equals(0) Then

                If addstat = False Then
                    If DateValue(tgl_old) <> DateValue(ttgl_mt.EditValue) Then
                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, tgudang1.EditValue, kdbar, qtykecil, 0, "Pindah Gudang", "-", "BE XXXX XX")
                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, tgudang2.EditValue, kdbar, 0, qtykecil, "Pindah Gudang", "-", "BE XXXX XX")
                    End If
                End If

                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, tgudang2.EditValue, kdbar, qtykecil, 0, "Pindah Gudang", "-", "BE XXXX XX")
                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, tgudang1.EditValue, kdbar, 0, qtykecil, "Pindah Gudang", "-", "BE XXXX XX")


            End If


            If dv1(i)("noid").Equals(0) Then
                Dim sqlins As String = String.Format("insert into trpindahgud2 (nobukti,kd_barang,satuan,qty,qtykecil) values('{0}','{1}','{2}',{3},{4})", tbukti.Text.Trim, _
                                                      kdbar, satuan, qty, qtykecil)

                Using cmd As OleDbCommand = New OleDbCommand(sqlins, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

                '2. update barang
                Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, tgudang1.EditValue, False, False, True)
                If Not hasilplusmin.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                    hasil = "error"
                    Exit For
                Else

                    Dim hasilplusmin2 As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, tgudang2.EditValue, True, False, True)

                    If Not hasilplusmin2.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin2, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        Exit For

                    End If
                End If

                Dim hasilplusmin3 As String = PlusMin_Barang_Fsk(cn, sqltrans, qtykecil, kdbar, tgudang1.EditValue, False, False, True)
                If Not hasilplusmin3.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilplusmin3, vbOKOnly + vbExclamation, "Informasi")
                    hasil = "error"
                    Exit For
                Else

                    Dim hasilplusmin4 As String = PlusMin_Barang_Fsk(cn, sqltrans, qtykecil, kdbar, tgudang2.EditValue, True, False, True)

                    If Not hasilplusmin4.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin4, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        Exit For

                    End If
                End If

                '3. insert to hist stok

                If addstat = False Then
                    If DateValue(tgl_old) <> DateValue(ttgl_mt.EditValue) Then
                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, tgudang1.EditValue, kdbar, qtykecil, 0, "Pindah Gudang", "-", "BE XXXX XX")
                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, tgudang2.EditValue, kdbar, 0, qtykecil, "Pindah Gudang", "-", "BE XXXX XX")
                    End If
                End If

                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, tgudang2.EditValue, kdbar, qtykecil, 0, "Pindah Gudang", "-", "BE XXXX XX")
                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, tgudang1.EditValue, kdbar, 0, qtykecil, "Pindah Gudang", "-", "BE XXXX XX")

            End If

        Next

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

                open_wait()

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                sqltrans = cn.BeginTransaction

                Dim sqldel As String = String.Format("delete from trpindahgud2 where noid={0}", noid)
                Using cmd As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

                Dim kdbar As String
                Dim satuan As String
                Dim qty, qtykecil As Integer

                kdbar = dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString
                satuan = dv1(Me.BindingContext(dv1).Position)("satuan").ToString
                qty = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qty").ToString)
                qtykecil = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtykecil").ToString)



                '2. update barang
                Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, tgudang1.EditValue, True, False, True)
                If Not hasilplusmin.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                    GoTo langsung
                Else
                    Dim hasilplusmin2 As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, tgudang2.EditValue, False, False, True)
                    If Not hasilplusmin2.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin2, vbOKOnly + vbExclamation, "Informasi")
                        GoTo langsung
                    End If
                End If

                Dim hasilplusmin3 As String = PlusMin_Barang_Fsk(cn, sqltrans, qtykecil, kdbar, tgudang1.EditValue, True, False, True)
                If Not hasilplusmin3.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilplusmin3, vbOKOnly + vbExclamation, "Informasi")
                    GoTo langsung
                Else
                    Dim hasilplusmin4 As String = PlusMin_Barang_Fsk(cn, sqltrans, qtykecil, kdbar, tgudang2.EditValue, False, False, True)
                    If Not hasilplusmin4.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin4, vbOKOnly + vbExclamation, "Informasi")
                        GoTo langsung
                    End If
                End If

                '3. insert to hist stok

                'If addstat = False Then
                If DateValue(tgl_old) <> DateValue(ttgl_mt.EditValue) Then
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, tgudang1.EditValue, kdbar, 0, qtykecil, "Pindah Gudang", "-", "BE XXXX XX")
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, tgudang2.EditValue, kdbar, qtykecil, 0, "Pindah Gudang", "-", "BE XXXX XX")
                Else
                    '3. insert to hist stok
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, tgudang1.EditValue, kdbar, qtykecil, 0, "Pindah Gudang", "-", "BE XXXX XX")
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, tgudang2.EditValue, kdbar, 0, qtykecil, "Pindah Gudang", "-", "BE XXXX XX")
                End If
                'End If

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

        Dim orow As DataRowView = dv.AddNew
        orow("sbatal") = 0
        orow("nobukti") = tbukti.Text.Trim
        orow("tanggal") = ttgl.EditValue
        orow("tgl_pindah") = ttgl_mt.EditValue
        orow("kd_karyawan") = tkd_supir.Text.Trim
        orow("nama_karyawan") = tnama_supir.Text.Trim
        orow("kd_gudang1") = tgudang1.EditValue
        orow("kd_gudang2") = tgudang2.EditValue
        orow("note") = tket.Text.Trim

        dv.EndInit()

    End Sub

    Private Sub updateview()

        dv(position)("nobukti") = tbukti.Text.Trim
        dv(position)("tanggal") = ttgl.EditValue
        dv(position)("tgl_pindah") = ttgl_mt.EditValue
        dv(position)("kd_karyawan") = tkd_supir.Text.Trim
        dv(position)("nama_karyawan") = tnama_supir.Text.Trim
        dv(position)("kd_gudang1") = tgudang1.EditValue
        dv(position)("kd_gudang2") = tgudang2.EditValue
        dv(position)("note") = tket.Text.Trim

    End Sub

    '' krani

    Private Sub bts_supir_Click(sender As System.Object, e As System.EventArgs) Handles bts_supir.Click

        Dim fs As New fskrani With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
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
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and bagian='KRANI' and kd_karyawan='{0}'", tkd_supir.Text.Trim)

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

    Private Sub btadd_Click(sender As System.Object, e As System.EventArgs) Handles btadd.Click

        Dim fs As New trpindahgud3 With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .addstat = True, .position = 0, .dv = dv1, .kdgudang = tgudang1.EditValue}
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

        If tgudang1.EditValue = "" Then
            MsgBox("Gudang awal harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tgudang1.Focus()
            Return
        End If

        If tgudang2.EditValue = "" Then
            MsgBox("Gudang akhir harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tgudang2.Focus()
            Return
        End If

        If tkd_supir.Text.Trim.Length = 0 Then
            MsgBox("Krani harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tkd_supir.Focus()
            Return
        End If

        If MsgBox("Yakin sudah benar ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
            simpan()
        End If

    End Sub

    Private Sub frekap_to2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub frekap_to2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        isi_gudang()

        ttgl.EditValue = Date.Now
        ttgl_mt.EditValue = Date.Now

        If addstat = False Then

            tgudang1.Enabled = False
            tgudang2.Enabled = False

            isi()
        Else
            kosongkan()
        End If

    End Sub

End Class