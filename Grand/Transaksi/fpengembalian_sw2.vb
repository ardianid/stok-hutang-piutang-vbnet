Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpengembalian_sw2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private tglmuat_old As String

    Private Sub kosongkan()
        tbukti.Text = "<< New >>"

        tkd_toko.Text = ""
        tnama_toko.Text = ""
        talamat_toko.Text = ""

        tket.EditValue = ""

        opengrid("xxxx")

    End Sub

    Private Sub opengrid(ByVal nobukti As String)

        Dim sql As String = String.Format("SELECT trpengembalian_sw2.noid, trpengembalian_sw2.kd_gudang, trpengembalian_sw2.kd_barang, ms_barang.nama_barang, trpengembalian_sw2.qty, " & _
        "trpengembalian_sw2.satuan, trpengembalian_sw2.qtykecil " & _
        "FROM trpengembalian_sw2 INNER JOIN ms_barang ON trpengembalian_sw2.kd_barang = ms_barang.kd_barang where trpengembalian_sw2.nobukti='{0}'", nobukti)


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
        Dim sql As String = String.Format("SELECT trpengembalian_sw.nobukti, trpengembalian_sw.tanggal, trpengembalian_sw.tgl_masuk, trpengembalian_sw.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, " & _
        "trpengembalian_sw.nobukti_sw, trpengembalian_sw.note " & _
        "FROM trpengembalian_sw INNER JOIN ms_toko ON trpengembalian_sw.kd_toko = ms_toko.kd_toko where trpengembalian_sw.nobukti='{0}'", nobukti)

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
                        ttgl_mt.EditValue = DateValue(dread("tgl_masuk").ToString)

                        tglmuat_old = ttgl_mt.EditValue

                        tkd_toko.EditValue = dread("kd_toko").ToString
                        tnama_toko.EditValue = dread("nama_toko").ToString
                        talamat_toko.EditValue = dread("alamat_toko").ToString

                        isi_nosewa(dread("nobukti_sw").ToString)
                        tnosewa.EditValue = dread("nobukti_sw").ToString

                        tket.EditValue = dread("note").ToString

                        opengrid(tbukti.Text.Trim)

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

        Dim sql As String = String.Format("select max(nobukti) from trpengembalian_sw where nobukti like '%KSW.{0}%'", tahunbulan)

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

        Return String.Format("KSW.{0}{1}{2}", tahun, bulan, kbukti)

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
                Dim sqlin As String = String.Format("insert into trpengembalian_sw (nobukti,tanggal,tgl_masuk,kd_toko,nobukti_sw,note) values('{0}','{1}','{2}','{3}','{4}','{5}')", _
                                                        tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_mt.EditValue), tkd_toko.Text.Trim, tnosewa.EditValue, tket.Text.Trim)


                cmd = New OleDbCommand(sqlin, cn, sqltrans)
                cmd.ExecuteNonQuery()


                Dim sqlsewa As String = String.Format("update trsewa set skembali=1 where nobukti='{0}'", tnosewa.EditValue)
                Using cmdsewa As OleDbCommand = New OleDbCommand(sqlsewa, cn, sqltrans)
                    cmdsewa.ExecuteNonQuery()
                End Using


                Dim sqlhistsewa As String = String.Format("update hsewa set skembali=1 where nobukti='{0}'", tnosewa.EditValue)
                Using cmdhist As OleDbCommand = New OleDbCommand(sqlhistsewa, cn, sqltrans)
                    cmdhist.ExecuteNonQuery()
                End Using


                Clsmy.InsertToLog(cn, "btpengembalian_sw", 1, 0, 0, 0, tbukti.Text.Trim, ttgl.EditValue, sqltrans)





            Else

                '1. update rekap
                Dim sqlup As String = String.Format("update trpengembalian_sw set tanggal='{0}',tgl_masuk='{1}',kd_toko='{2}',nobukti_sw='{3}',note='{4}' where nobukti='{5}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_mt.EditValue), _
                                                   tkd_toko.Text.Trim, tnosewa.EditValue, tket.Text.Trim, tbukti.Text.Trim)

                cmd = New OleDbCommand(sqlup, cn, sqltrans)
                cmd.ExecuteNonQuery()


                Clsmy.InsertToLog(cn, "btpengembalian_sw", 0, 1, 0, 0, tbukti.Text.Trim, ttgl.EditValue, sqltrans)
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

        Dim hasil As String = ""

        Dim kdbar As String
        Dim kdgudang As String
        Dim satuan As String
        Dim qty, qtykecil As Integer
        'Dim harga, jumlah, hargakecil As String

        For i As Integer = 0 To dv1.Count - 1

            kdbar = dv1(i)("kd_barang").ToString
            kdgudang = dv1(i)("kd_gudang").ToString
            satuan = dv1(i)("satuan").ToString
            qty = Integer.Parse(dv1(i)("qty").ToString)
            qtykecil = Integer.Parse(dv1(i)("qtykecil").ToString)
            'harga = dv1(i)("harga").ToString
            'jumlah = dv1(i)("jumlah").ToString
            'hargakecil = dv1(i)("hargakecil").ToString

            If dv1(i)("noid").Equals(0) Then
                Dim sqlins As String = String.Format("insert into trpengembalian_sw2 (nobukti,kd_gudang,kd_barang,satuan,qty,qtykecil) values('{0}','{1}','{2}','{3}',{4},{5})", tbukti.Text.Trim, _
                                                     kdgudang, kdbar, satuan, qty, qtykecil)

                Using cmd As OleDbCommand = New OleDbCommand(sqlins, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

                '2. update barang
                Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgudang, True, False, False)
                If Not hasilplusmin.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                    hasil = "error"
                    Exit For

                End If

                ' historical toko
                Dim hasiltok As String = Hist_PinjamSewa_Toko(tkd_toko.Text.Trim, kdbar, qtykecil, cn, sqltrans, False, False)
                If Not hasiltok.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hasiltok, vbOKOnly + vbExclamation, "Informasi")
                    hasil = "error"
                    Exit For
                End If


                ' update jmlsewa toko
                Dim sqljum As String = String.Format("update trsewa2 set qtykecil_km=qtykecil_km +{0} where nobukti='{1}' and kd_barang='{2}'", qtykecil, tnosewa.EditValue, kdbar)
                Using cmdsw As OleDbCommand = New OleDbCommand(sqljum, cn, sqltrans)
                    cmdsw.ExecuteNonQuery()
                End Using


                '3. insert to hist stok
                If addstat = False Then
                    If DateValue(tglmuat_old) <> DateValue(ttgl_mt.EditValue) Then
                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tglmuat_old, kdgudang, kdbar, 0, qtykecil, "Pengembalian Barang Sewa", "-", "BE XXXX XX")
                    End If
                End If

                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, kdgudang, kdbar, qtykecil, 0, "Pengembalian Barang Sewa", "-", "BE XXXX XX")


            Else


                Dim sqls As String = String.Format("select kd_gudang,kd_barang,qtykecil from trpengembalian_sw2 where noid={0}", dv1(i)("noid").ToString)
                Dim cmds As OleDbCommand = New OleDbCommand(sqls, cn, sqltrans)
                Dim drs As OleDbDataReader = cmds.ExecuteReader

                If drs.Read Then

                    If IsNumeric(drs("qtykecil").ToString) Then


                        Dim gudangold As String = drs("kd_gudang").ToString
                        Dim barangold As String = drs("kd_barang").ToString
                        Dim qtyold As Integer = Integer.Parse(drs("qtykecil").ToString)


                        '2. update barang
                        Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtyold, barangold, gudangold, False, False, False)
                        If Not hasilplusmin.Equals("ok") Then
                            close_wait()

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                            hasil = "error"
                            Exit For

                        Else

                            Dim hasilplusmin2 As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgudang, True, False, False)
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


                        ' historical toko
                        Dim hasiltok As String = Hist_PinjamSewa_Toko(tkd_toko.Text.Trim, barangold, qtyold, cn, sqltrans, True, False)
                        If Not hasiltok.Equals("ok") Then
                            close_wait()

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox(hasiltok, vbOKOnly + vbExclamation, "Informasi")
                            hasil = "error"
                            Exit For

                        Else

                            Dim hasiltok2 As String = Hist_PinjamSewa_Toko(tkd_toko.Text.Trim, kdbar, qtykecil, cn, sqltrans, False, False)
                            If Not hasiltok2.Equals("ok") Then
                                close_wait()

                                If Not IsNothing(sqltrans) Then
                                    sqltrans.Rollback()
                                End If

                                MsgBox(hasiltok2, vbOKOnly + vbExclamation, "Informasi")
                                hasil = "error"
                                Exit For
                            End If


                        End If


                        ' update jmlsewa toko
                        Dim sqljum As String = String.Format("update trsewa2 set qtykecil_km=qtykecil_km -{0} where nobukti='{1}' and kd_barang='{2}'", qtyold, tnosewa.EditValue, barangold)
                        Using cmdsw As OleDbCommand = New OleDbCommand(sqljum, cn, sqltrans)
                            cmdsw.ExecuteNonQuery()
                        End Using

                        Dim sqljum2 As String = String.Format("update trsewa2 set qtykecil_km=qtykecil_km +{0} where nobukti='{1}' and kd_barang='{2}'", qtykecil, tnosewa.EditValue, kdbar)
                        Using cmdsw2 As OleDbCommand = New OleDbCommand(sqljum2, cn, sqltrans)
                            cmdsw2.ExecuteNonQuery()
                        End Using


                        Dim sqlup As String = String.Format("update trpengembalian_sw2 set kd_gudang='{0}',kd_barang='{1}',satuan='{2}',qty={3},qtykecil={4} where nobukti='{5}'", kdgudang, kdbar, satuan, qty, qtykecil, tbukti.Text.Trim)
                        Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                            cmdup.ExecuteNonQuery()
                        End Using


                        '3. insert to hist stok
                        If addstat = False Then
                            If DateValue(tglmuat_old) <> DateValue(ttgl_mt.EditValue) Then
                                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tglmuat_old, gudangold, barangold, 0, qtyold, "Pengembalian Barang Sewa", "-", "BE XXXX XX")
                            Else
                                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, gudangold, barangold, 0, qtyold, "Pengembalian Barang Sewa", "-", "BE XXXX XX")
                            End If
                        End If

                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_mt.EditValue, kdgudang, kdbar, qtykecil, 0, "Pengembalian Barang Sewa", "-", "BE XXXX XX")


                    End If

                    ' sampe sini

                End If



            End If

        Next

        If Not hasil.Equals("error") Then
            hasil = "ok"
        End If

        Return hasil

    End Function

    Private Sub isi_nosewa(ByVal nosewa As String)

        Dim sql As String = ""

        If nosewa.Equals("") Then
            sql = String.Format("select nobukti from trsewa where nobukti in (select nobukti from trsewa2 where ((qtykecil > qtykecil_km) or qtykecil_km=0) ) and sbatal=0 and kd_toko='{0}'", tkd_toko.Text.Trim)
        Else
            sql = String.Format("select nobukti from trsewa where nobukti='{0}'", nosewa)
        End If

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet
        Dim dvg As DataView

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvg = dvm.CreateDataView(ds.Tables(0))

            tnosewa.Properties.DataSource = dvg

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

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("sbatal") = 0
        orow("nobukti") = tbukti.Text.Trim
        orow("nobukti_sw") = tnosewa.EditValue
        orow("tanggal") = ttgl.EditValue
        orow("tgl_masuk") = ttgl_mt.EditValue
        orow("kd_toko") = tkd_toko.Text.Trim
        orow("nama_toko") = tnama_toko.Text.Trim
        orow("alamat_toko") = talamat_toko.Text.Trim

        dv.EndInit()

    End Sub

    Private Sub updateview()

        dv(position)("nobukti") = tbukti.Text.Trim
        dv(position)("nobukti_sw") = tnosewa.EditValue
        dv(position)("tanggal") = ttgl.EditValue
        dv(position)("tgl_masuk") = ttgl_mt.EditValue
        dv(position)("kd_toko") = tkd_toko.Text.Trim
        dv(position)("nama_toko") = tnama_toko.Text.Trim
        dv(position)("alamat_toko") = talamat_toko.Text.Trim

    End Sub

    Private Sub bts_toko_Click(sender As System.Object, e As System.EventArgs) Handles bts_toko.Click
        Dim fs As New fsoutlet With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .kdsales = ""}
        fs.ShowDialog(Me)

        tkd_toko.EditValue = fs.get_KODE
        tnama_toko.EditValue = fs.get_NAMA
        talamat_toko.EditValue = fs.get_ALAMAT

        tkd_toko_EditValueChanged(sender, Nothing)


    End Sub

    Private Sub tkd_toko_EditValueChanged(sender As Object, e As System.EventArgs) Handles tkd_toko.Validated
        If tkd_toko.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select kd_toko,nama_toko,alamat_toko from ms_toko where kd_toko='{0}' and aktif=1", tkd_toko.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_toko.EditValue = dread("kd_toko").ToString
                        tnama_toko.EditValue = dread("nama_toko").ToString
                        talamat_toko.EditValue = dread("alamat_toko").ToString

                        isi_nosewa("")

                    Else
                        tkd_toko.EditValue = ""
                        tnama_toko.EditValue = ""
                        talamat_toko.Text = ""


                    End If
                Else
                    tkd_toko.EditValue = ""
                    tnama_toko.EditValue = ""
                    talamat_toko.Text = ""

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

    Private Sub tkd_toko_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_toko.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_toko_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_toko_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_toko.LostFocus
        If tkd_toko.Text.Trim.Length = 0 Then
            tkd_toko.EditValue = ""
            tnama_toko.EditValue = ""
            talamat_toko.Text = ""
        End If
    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tkd_toko.Text.Trim.Length = 0 Then
            MsgBox("Outlet harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tkd_toko.Focus()
            Return
        End If

        If tnosewa.Text.Trim.Length = 0 Then
            MsgBox("No Bukti sewa harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tnosewa.Focus()
            Return
        End If

        If MsgBox("Yakin sudah benar ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
            simpan()
        End If

    End Sub

    Private Sub frekap_to2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tkd_toko.Focus()
    End Sub

    Private Sub frekap_to2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ttgl.EditValue = Date.Now
        ttgl_mt.EditValue = Date.Now

        If addstat = False Then

            tkd_toko.Enabled = False
            bts_toko.Enabled = False
            tnosewa.Enabled = False

            isi()
        Else
            kosongkan()
        End If

    End Sub

    Private Sub btadd_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        If tnosewa.EditValue = "" Then
            MsgBox("Isi dulu nobukti sewa...", vbOKOnly + vbInformation, "Informasi")
            tnosewa.Focus()
            Return
        End If

        Dim fs As New fpengembalian_sw3 With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .dv = dv1, .nobuktisewa = tnosewa.EditValue, .statadd = addstat}
        fs.ShowDialog(Me)

    End Sub

    Private Sub tnosewa_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tnosewa.EditValueChanged
        opengrid("xxxx")
    End Sub

End Class