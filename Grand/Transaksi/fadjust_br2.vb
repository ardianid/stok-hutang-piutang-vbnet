Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fadjust_br2

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

        Dim sql As String = String.Format("SELECT     tradjust_br2.noid, tradjust_br2.kd_barang, ms_barang.nama_barang, tradjust_br2.satuan, tradjust_br2.qty_asal, tradjust_br2.qty_sel, tradjust_br2.qty_akh, " & _
            "tradjust_br2.qtykecil_asal, tradjust_br2.qtykecil_sel, tradjust_br2.qtykecil_akh " & _
            "FROM tradjust_br2 INNER JOIN ms_barang ON tradjust_br2.kd_barang = ms_barang.kd_barang where tradjust_br2.nobukti='{0}'", tbukti.Text.Trim)


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
        Dim sql As String = String.Format("SELECT * FROM  tradjust_br where nobukti='{0}'", nobukti)

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

                        tgudang1.EditValue = dread("kd_gudang").ToString

                        tket.EditValue = dread("note").ToString

                        tjenis.EditValue = dread("jenis").ToString

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

        Dim sql As String = String.Format("select max(nobukti) from tradjust_br where  nobukti like '%ADJ.{0}%' ", tahunbulan)

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

        Return String.Format("ADJ.{0}{1}{2}", tahun, bulan, kbukti)

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

                '0. insert adjust
                Dim sqlin As String = String.Format("insert into tradjust_br (nobukti,tanggal,kd_gudang,jenis,note) values('{0}','{1}','{2}','{3}','{4}')", _
                                                        tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), tgudang1.EditValue, tjenis.Text.Trim, tket.Text.Trim)


                cmd = New OleDbCommand(sqlin, cn, sqltrans)
                cmd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btadjust_br", 1, 0, 0, 0, tbukti.Text.Trim, ttgl.EditValue, sqltrans)



            Else

                '1. update adjust
                Dim sqlup As String = String.Format("update tradjust_br set tanggal='{0}',kd_gudang='{1}',jenis='{2}',note='{3}' where nobukti='{4}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl.EditValue), tgudang1.EditValue, tjenis.Text.Trim, tket.Text.Trim, tbukti.Text.Trim)

                cmd = New OleDbCommand(sqlup, cn, sqltrans)
                cmd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btadjust_br", 0, 1, 0, 0, tbukti.Text.Trim, ttgl.EditValue, sqltrans)
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
        Dim satuan As String
        Dim qty_asal As Integer
        Dim qty_akh As Integer
        Dim qty_sel As Integer
        Dim qtykecil_asl As Integer
        Dim qtykecil_sel As Integer
        Dim qtykecil_akh As Integer


        For i As Integer = 0 To dv1.Count - 1

            kdbar = dv1(i)("kd_barang").ToString
            satuan = dv1(i)("satuan").ToString
            qty_asal = Integer.Parse(dv1(i)("qty_asal").ToString)
            qty_akh = Integer.Parse(dv1(i)("qty_akh").ToString)
            qty_sel = Integer.Parse(dv1(i)("qty_sel").ToString)

            qtykecil_asl = Integer.Parse(dv1(i)("qtykecil_asal").ToString)
            qtykecil_akh = Integer.Parse(dv1(i)("qtykecil_akh").ToString)
            qtykecil_sel = Integer.Parse(dv1(i)("qtykecil_sel").ToString)

            If Not dv1(i)("noid").Equals(0) Then

                If addstat = False Then
                    If DateValue(tgl_old) <> DateValue(ttgl.EditValue) Then
                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, tgudang1.EditValue, kdbar, qtykecil_sel, 0, "Adjustment (Edit)", "-", "BE XXXX XX")
                    End If
                End If

                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, tgudang1.EditValue, kdbar, 0, qtykecil_sel, "Adjustment", "-", "BE XXXX XX")

            End If


            If dv1(i)("noid").Equals(0) Then
                Dim sqlins As String = String.Format("insert into tradjust_br2 (nobukti,kd_barang,satuan,qty_asal,qty_akh,qty_sel,qtykecil_asal,qtykecil_akh,qtykecil_sel) values('{0}','{1}','{2}',{3},{4},{5},{6},{7},{8})", tbukti.Text.Trim, _
                                                      kdbar, satuan, qty_asal, qty_akh, qty_sel, qtykecil_asl, qtykecil_akh, qtykecil_sel)

                Using cmd As OleDbCommand = New OleDbCommand(sqlins, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

                If tjenis.SelectedIndex = 0 Then
                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang_Adj(cn, sqltrans, qtykecil_akh, qtykecil_sel, kdbar, tgudang1.EditValue, False)
                    If Not hasilplusmin.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        Exit For
                    End If
                Else

                    '2. update barang
                    Dim hasilplusmin2 As String = PlusMin_Barang_Fsk_Adj(cn, sqltrans, qtykecil_akh, qtykecil_sel, kdbar, tgudang1.EditValue, False)
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
                
                '3. insert to hist stok

                If addstat = False Then
                    If DateValue(tgl_old) <> DateValue(ttgl.EditValue) Then
                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, tgudang1.EditValue, kdbar, qtykecil_sel, 0, "Adjustment (Edit)", "-", "BE XXXX XX")
                    End If
                End If

                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, tgudang1.EditValue, kdbar, 0, qtykecil_sel, "Adjustment", "-", "BE XXXX XX")

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

                Dim sqldel As String = String.Format("delete from tradjust_br2 where noid={0}", noid)
                Using cmd As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

                Dim kdbar As String
                Dim satuan As String
                Dim qty_asal As Integer
                Dim qty_akh As Integer
                Dim qty_sel As Integer
                Dim qtykecil_asl As Integer
                Dim qtykecil_sel As Integer
                Dim qtykecil_akh As Integer

                kdbar = dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString
                satuan = dv1(Me.BindingContext(dv1).Position)("satuan").ToString

                qty_asal = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qty_asal").ToString)
                qty_akh = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qty_akh").ToString)
                qty_sel = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qty_sel").ToString)

                qtykecil_asl = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtykecil_asal").ToString)
                qtykecil_akh = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtykecil_akh").ToString)
                qtykecil_sel = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtykecil_sel").ToString)

                If tjenis.SelectedIndex = 0 Then
                    Dim hasilplusmin As String = PlusMin_Barang_Adj(cn, sqltrans, qtykecil_akh, qtykecil_sel, kdbar, tgudang1.EditValue, True)
                    If Not hasilplusmin.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        GoTo langsung
                    End If
                Else
                    Dim hasilplusmin2 As String = PlusMin_Barang_Fsk_Adj(cn, sqltrans, qtykecil_akh, qtykecil_sel, kdbar, tgudang1.EditValue, True)
                    If Not hasilplusmin2.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin2, vbOKOnly + vbExclamation, "Informasi")
                        GoTo langsung
                    End If
                End If

                '3. insert to hist stok

                'If addstat = False Then
                If DateValue(tgl_old) <> DateValue(ttgl.EditValue) Then
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, tgudang1.EditValue, kdbar, qtykecil_sel, 0, "Adjustment (Edit)", "-", "BE XXXX XX")
                Else
                    '3. insert to hist stok
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, tgudang1.EditValue, kdbar, qtykecil_sel, 0, "Adjustment (Edit)", "-", "BE XXXX XX")
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
        orow("kd_gudang") = tgudang1.EditValue
        orow("jenis") = tjenis.Text.Trim
        orow("note") = tket.Text.Trim

        dv.EndInit()

    End Sub

    Private Sub updateview()

        dv(position)("nobukti") = tbukti.Text.Trim
        dv(position)("tanggal") = ttgl.EditValue
        dv(position)("kd_gudang") = tgudang1.EditValue
        dv(position)("jenis") = tjenis.Text.Trim
        dv(position)("note") = tket.Text.Trim

    End Sub

    Private Sub btadd_Click(sender As System.Object, e As System.EventArgs) Handles btadd.Click

        If tgudang1.EditValue = "" Then
            Return
        End If

        If tjenis.Text.Trim.Length = 0 Then
            Return
        End If

        Dim fs As New tradjust_br3 With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .addstat = True, .position = 0, .dv = dv1, .kdgudang = tgudang1.EditValue, .jenisjual = tjenis.EditValue}
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
            MsgBox("Gudang harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tgudang1.Focus()
            Return
        End If

        If tjenis.Text.Trim.Length = 0 Then
            MsgBox("Jenis harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tjenis.Focus()
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

        If addstat = False Then

            tgudang1.Enabled = False
            tjenis.Enabled = False

            isi()
        Else
            kosongkan()
        End If

    End Sub

End Class