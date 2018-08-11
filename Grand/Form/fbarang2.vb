Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fbarang2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private kd_old As String

    Private Sub kosongkan()
        tkode.Text = ""
        tnama.Text = ""
        tnama2.Text = ""

        tqty1.EditValue = 0
        tqty2.EditValue = 0
        tqty3.EditValue = 0

        tsat1.EditValue = ""
        tsat2.EditValue = ""
        tsat3.EditValue = ""

        cjual.Checked = True
        cpinjam.Checked = True
        csewa.Checked = True
        charus.Checked = False

        charus_CheckedChanged(Nothing, Nothing)

        csales.Checked = False
        csupir.Checked = False
        ckenek.Checked = False

        thargajual.EditValue = 0
        thargabeli.EditValue = 0
        thargasew.EditValue = 0

        tberat1.EditValue = 0.0
        tberat2.EditValue = 0.0
        tberat3.EditValue = 0.0

    End Sub

    Private Sub isi()
        tkode.EditValue = dv(position)("kd_barang").ToString
        tnama.EditValue = dv(position)("nama_barang").ToString
        tnama2.EditValue = dv(position)("nama_lap").ToString

        isi_pengganti()
        isi_nonfisik()

        If dv(position)("kd_barang_kmb").ToString.Equals("") Then
            tpengganti.EditValue = "None"
        Else
            tpengganti.EditValue = dv(position)("kd_barang_kmb").ToString
        End If

        If dv(position)("kd_barang_jmn").ToString.Equals("") Then
            tnon.EditValue = "None"
        Else
            tnon.EditValue = dv(position)("kd_barang_jmn").ToString
        End If


        tjenis.EditValue = dv(position)("jenis").ToString
        tkelompok.EditValue = dv(position)("kelompok").ToString

        tqty1.EditValue = dv(position)("qty1").ToString
        tqty2.EditValue = dv(position)("qty2").ToString
        tqty3.EditValue = dv(position)("qty3").ToString

        tsat1.EditValue = dv(position)("satuan1").ToString
        tsat2.EditValue = dv(position)("satuan2").ToString
        tsat3.EditValue = dv(position)("satuan3").ToString

        If Decimal.Parse(dv(position)("berat1").ToString) = 0 Then
            tberat1.EditValue = 0.0
        Else
            tberat1.EditValue = dv(position)("berat1").ToString
        End If

        If Decimal.Parse(dv(position)("berat2").ToString) = 0 Then
            tberat2.EditValue = 0.0
        Else
            tberat2.EditValue = dv(position)("berat2").ToString
        End If

        If Decimal.Parse(dv(position)("berat3").ToString) = 0 Then
            tberat3.EditValue = 0.0
        Else
            tberat3.EditValue = dv(position)("berat3").ToString
        End If

        Dim ssales, ssupir, skenek As String
        ssales = dv(position)("ins_sales").ToString
        ssupir = dv(position)("ins_supir").ToString
        skenek = dv(position)("ins_kenek").ToString

        If ssales.Equals("1") Then
            csales.Checked = True
        Else
            csales.Checked = False
        End If

        If ssupir.Equals("1") Then
            csupir.Checked = True
        Else
            csupir.Checked = False
        End If

        If skenek.Equals("1") Then
            ckenek.Checked = True
        Else
            ckenek.Checked = False
        End If

        Dim sjual, spinjam, ssewa, sharus As String
        sjual = dv(position)("sjual").ToString
        spinjam = dv(position)("spinjam").ToString
        ssewa = dv(position)("ssewa").ToString
        sharus = dv(position)("hrusfaktur").ToString

        If sjual.Equals("1") Then
            cjual.Checked = True
        Else
            cjual.Checked = False
        End If

        If spinjam.Equals("1") Then
            cpinjam.Checked = True
        Else
            cpinjam.Checked = False
        End If

        If ssewa.Equals("1") Then
            csewa.Checked = True
        Else
            csewa.Checked = False
        End If

        If sharus.Equals("1") Then
            charus.Checked = True
        Else
            charus.Checked = False
        End If

        cjual_CheckedChanged(Nothing, Nothing)
        cpinjam_CheckedChanged(Nothing, Nothing)
        csewa_CheckedChanged(Nothing, Nothing)
        charus_CheckedChanged(Nothing, Nothing)

        Dim hargabeli, hargajual, hargasewa, nno_urut As String
        hargabeli = dv(position)("hargabeli").ToString
        hargajual = dv(position)("hargajual").ToString
        hargasewa = dv(position)("hargasewa").ToString
        nno_urut = dv(position)("nohrus").ToString

        If hargabeli.Equals("") Then
            thargabeli.EditValue = 0
        Else
            thargabeli.EditValue = hargabeli
        End If

        If hargajual.Equals("") Then
            thargajual.EditValue = 0
        Else
            thargajual.EditValue = hargajual
        End If

        If hargasewa.Equals("") Then
            thargasew.EditValue = 0
        Else
            thargasew.EditValue = hargasewa
        End If

        If nno_urut.Equals("") Then
            tnourut.EditValue = 0
        Else
            tnourut.EditValue = nno_urut
        End If

        '    cekisi_pengganti()

    End Sub

    Private Sub cekisi_pengganti()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select noid,kd_barang2 from ms_barang3 where kd_barang='{0}'", tkode.Text.Trim)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd(0).ToString) Then
                    tpengganti.EditValue = drd(1).ToString
                    kd_old = drd(1).ToString
                Else
                    kd_old = ""
                End If
            Else
                kd_old = ""
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

    Private Sub isi_pengganti()

        Dim sql As String = String.Format("select kd_barang,nama_barang from ms_barang where not(kd_barang='{0}')", tkode.Text.Trim)

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet
        Dim dvg As DataView

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim orow As DataRow = ds.Tables(0).NewRow
            orow("kd_barang") = "None"
            orow("nama_barang") = "None"
            ds.Tables(0).Rows.InsertAt(orow, 0)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvg = dvm.CreateDataView(ds.Tables(0))

            tpengganti.Properties.DataSource = dvg

            tpengganti.EditValue = "None"


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

    Private Sub isi_nonfisik()

        Dim sql As String = String.Format("select kd_barang,nama_barang from ms_barang where jenis='NON FISIK' and not(kd_barang='{0}')", tkode.Text.Trim)

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet
        Dim dvg As DataView

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim orow As DataRow = ds.Tables(0).NewRow
            orow("kd_barang") = "None"
            orow("nama_barang") = "None"
            ds.Tables(0).Rows.InsertAt(orow, 0)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvg = dvm.CreateDataView(ds.Tables(0))

            tnon.Properties.DataSource = dvg

            tnon.EditValue = "None"


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

    Private Sub hitung_berat23(ByVal set2 As Boolean)

        Dim berat1 As Decimal = tberat1.EditValue

        If berat1 = 0 Then
            tberat2.EditValue = 0.0
            tberat3.EditValue = 0.0
            Return
        End If

        Dim qty2 As Double = tqty2.EditValue
        Dim qty3 As Double = tqty3.EditValue

        Dim berat2 As Decimal = 0.0

        If set2 Then
            berat2 = tberat2.EditValue
        Else
            berat2 = berat1 / qty2
            tberat2.EditValue = berat2
        End If

        Dim berat3 As Decimal = berat2 / qty3

        tberat3.EditValue = berat3

    End Sub

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim kjual, ksewa, kpinjam As Integer

            If cjual.Checked = True Then
                kjual = 1
            Else
                kjual = 0
            End If

            If csewa.Checked = True Then
                ksewa = 1
            Else
                ksewa = 0
            End If

            If cpinjam.Checked = True Then
                kpinjam = 1
            Else
                kpinjam = 0
            End If

            Dim kharus As Integer
            If charus.Checked = True Then
                kharus = 1
            Else
                kharus = 0
            End If

            Dim ksales As Integer
            If csales.Checked = True Then
                ksales = 1
            Else
                ksales = 0
            End If

            Dim ksupir As Integer
            If csupir.Checked = True Then
                ksupir = 1
            Else
                ksupir = 0
            End If

            Dim kkenek As Integer
            If ckenek.Checked = True Then
                kkenek = 1
            Else
                kkenek = 0
            End If

            Dim sqlc As String = String.Format("select kd_barang from ms_barang where kd_barang='{0}'", tkode.Text.Trim, tnama.EditValue)
            Dim sql_insert As String = String.Format("insert into ms_barang (kd_barang,nama_barang,qty1,qty2,qty3,satuan1,satuan2,satuan3,sjual,spinjam,ssewa,hargabeli,hargajual,hargasewa,jenis,hrusfaktur,nohrus,kelompok,nama_lap,ins_sales,ins_supir,ins_kenek,berat1,berat2,berat3) values('{0}','{1}',{2},{3},{4},'{5}','{6}','{7}',{8},{9},{10},{11},{12},{13},'{14}',{15},{16},'{17}','{18}',{19},{20},{21},{22},{23},{24})", tkode.Text.Trim, tnama.EditValue, _
                                                     Replace(tqty1.EditValue, ",", "."), Replace(tqty2.EditValue, ",", "."), Replace(tqty3.EditValue, ",", "."), tsat1.EditValue, tsat2.EditValue, tsat3.EditValue, kjual, kpinjam, ksewa, Replace(thargabeli.EditValue, ",", "."), Replace(thargajual.EditValue, ",", "."), Replace(thargasew.EditValue, ",", "."), tjenis.EditValue, kharus, Replace(tnourut.EditValue, ",", "."), tkelompok.EditValue, tnama2.Text.Trim, ksales, ksupir, kkenek, Replace(tberat1.EditValue, ",", "."), Replace(tberat2.EditValue, ",", "."), Replace(tberat3.EditValue, ",", "."))
            Dim sql_update As String = String.Format("update ms_barang set nama_barang='{0}',qty1={1},qty2={2},qty3={3},satuan1='{4}',satuan2='{5}',satuan3='{6}',sjual={7},spinjam={8},ssewa={9},hargabeli={10},hargajual={11},hargasewa={12},jenis='{13}',hrusfaktur={14},nohrus={15},kelompok='{16}',nama_lap='{17}',ins_sales={18},ins_supir={19},ins_kenek={20},berat1={21},berat2={22},berat3={23} where kd_barang='{24}'", tnama.EditValue, _
                                                     Replace(tqty1.EditValue, ",", "."), Replace(tqty2.EditValue, ",", "."), Replace(tqty3.EditValue, ",", "."), tsat1.EditValue, tsat2.EditValue, tsat3.EditValue, kjual, kpinjam, ksewa, Replace(thargabeli.EditValue, ",", "."), Replace(thargajual.EditValue, ",", "."), Replace(thargasew.EditValue, ",", "."), tjenis.EditValue, kharus, Replace(tnourut.EditValue, ",", "."), tkelompok.EditValue, tnama2.Text.Trim, ksales, ksupir, kkenek, Replace(tberat1.EditValue, ",", "."), Replace(tberat2.EditValue, ",", "."), Replace(tberat3.EditValue, ",", "."), tkode.Text.Trim)

            Dim sqlinsert_pengganti As String
            If tpengganti.EditValue = "None" Then
                sqlinsert_pengganti = String.Format("update ms_barang set kd_barang_kmb='{0}' where kd_barang='{1}' ", "", tkode.Text.Trim)
            Else
                sqlinsert_pengganti = String.Format("update ms_barang set kd_barang_kmb='{0}' where kd_barang='{1}' ", tpengganti.EditValue, tkode.Text.Trim)
            End If

            Dim sqlinsert_non As String

            If tnon.EditValue = "None" Then

                sqlinsert_non = String.Format("update ms_barang set kd_barang_jmn='{0}' where kd_barang='{1}' ", "", tkode.Text.Trim)

            Else
                sqlinsert_non = String.Format("update ms_barang set kd_barang_jmn='{0}' where kd_barang='{1}' ", tnon.EditValue, tkode.Text.Trim)
            End If


            sqltrans = cn.BeginTransaction

            Dim comd As OleDbCommand

            If addstat = True Then

                Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
                Dim dread As OleDbDataReader = cmdc.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        Dim kdka As String = dread(0).ToString

                        If kdka.Trim.Length = 0 Then
                            comd = New OleDbCommand(sql_insert, cn, sqltrans)
                            comd.ExecuteNonQuery()

                            Clsmy.InsertToLog(cn, "btbarang", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                            insertview()
                        Else

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox("Kode/Nama sudah ada ...", vbOKOnly + vbExclamation, "Informasi")
                            tkode.Focus()
                            Return
                        End If
                    Else
                        comd = New OleDbCommand(sql_insert, cn, sqltrans)
                        comd.ExecuteNonQuery()

                        Clsmy.InsertToLog(cn, "btbarang", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                        insertview()
                    End If
                Else
                    comd = New OleDbCommand(sql_insert, cn, sqltrans)
                    comd.ExecuteNonQuery()

                    Clsmy.InsertToLog(cn, "btbarang", 1, 0, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                    insertview()
                End If

                dread.Close()


            Else

                ' jika rubah

                comd = New OleDbCommand(sql_update, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btbarang", 0, 1, 0, 0, tkode.Text.Trim, tnama.Text.Trim, sqltrans)

                updateview()

            End If

            ' update barang jaminan dan barang kosong

            'If Not (tpengganti.EditValue = "None") Then

            Using cmdpengganti As OleDbCommand = New OleDbCommand(sqlinsert_pengganti, cn, sqltrans)
                cmdpengganti.ExecuteNonQuery()
            End Using

            'End If

            'If Not (tnon.EditValue = "None") Then

            Using cmdpengganti As OleDbCommand = New OleDbCommand(sqlinsert_non, cn, sqltrans)
                cmdpengganti.ExecuteNonQuery()
            End Using

            'End If

            '-------------------------

            sqltrans.Commit()
            MsgBox("Data telah disimpan...", vbOKOnly + vbInformation, "Informasi")

            If addstat = True Then
                kosongkan()
                tkode.Focus()
            Else
                Me.Close()
            End If


        Catch ex As Exception
            close_wait()

            If Not IsNothing(sqltrans) Then
                sqltrans.Rollback()
            End If

            MsgBox(ex.ToString)
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try


    End Sub

    Private Sub updateview()

        dv(position)("kd_barang") = tkode.Text.Trim
        dv(position)("nama_barang") = tnama.EditValue
        dv(position)("nama_lap") = tnama2.EditValue

        dv(position)("qty1") = tqty1.EditValue
        dv(position)("qty2") = tqty2.EditValue
        dv(position)("qty3") = tqty3.EditValue

        dv(position)("satuan1") = tsat1.Text.Trim
        dv(position)("satuan2") = tsat2.Text.Trim
        dv(position)("satuan3") = tsat3.Text.Trim

        dv(position)("berat1") = tberat1.EditValue
        dv(position)("berat2") = tberat2.EditValue
        dv(position)("berat3") = tberat3.EditValue

        If cjual.Checked = True Then
            dv(position)("sjual") = 1
        Else
            dv(position)("sjual") = 0
        End If

        If cpinjam.Checked = True Then
            dv(position)("spinjam") = 1
        Else
            dv(position)("spinjam") = 0
        End If

        If csewa.Checked = True Then
            dv(position)("ssewa") = 1
        Else
            dv(position)("ssewa") = 0
        End If

        If charus.Checked = True Then
            dv(position)("hrusfaktur") = 1
        Else
            dv(position)("hrusfaktur") = 0
        End If

        Dim ksales As Integer
        If csales.Checked = True Then
            ksales = 1
        Else
            ksales = 0
        End If

        Dim ksupir As Integer
        If csupir.Checked = True Then
            ksupir = 1
        Else
            ksupir = 0
        End If

        Dim kkenek As Integer
        If ckenek.Checked = True Then
            kkenek = 1
        Else
            kkenek = 0
        End If

        dv(position)("ins_sales") = ksales
        dv(position)("ins_supir") = ksupir
        dv(position)("ins_kenek") = kkenek

        dv(position)("hargabeli") = thargabeli.EditValue
        dv(position)("hargajual") = thargajual.EditValue
        dv(position)("hargasewa") = thargasew.EditValue
        dv(position)("nohrus") = tnourut.EditValue

        dv(position)("jenis") = tjenis.EditValue
        dv(position)("kelompok") = tkelompok.EditValue

        If tpengganti.EditValue = "None" Then
            dv(position)("kd_barang_kmb") = ""
        Else
            dv(position)("kd_barang_kmb") = tpengganti.EditValue
        End If

        If tnon.EditValue = "None" Then
            dv(position)("kd_barang_jmn") = ""
        Else
            dv(position)("kd_barang_jmn") = tnon.EditValue
        End If

        'dv(position)("pos_pinjam") = 0
        'dv(position)("pos_sewa") = 0

        'dv(position)("jmlstok1") = 0
        'dv(position)("jmlstok2") = 0
        'dv(position)("jmlstok3") = 0

        'dv(position)("jmlstok_k1") = 0
        'dv(position)("jmlstok_k2") = 0
        'dv(position)("jmlstok_k3") = 0

        'dv(position)("jmlstok_f1") = 0
        'dv(position)("jmlstok_f2") = 0
        'dv(position)("jmlstok_f3") = 0

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("kd_barang") = tkode.Text.Trim
        orow("nama_barang") = tnama.EditValue
        orow("nama_lap") = tnama2.EditValue

        orow("qty1") = tqty1.EditValue
        orow("qty2") = tqty2.EditValue
        orow("qty3") = tqty3.EditValue

        orow("satuan1") = tsat1.Text.Trim
        orow("satuan2") = tsat2.Text.Trim
        orow("satuan3") = tsat3.Text.Trim

        orow("berat1") = tberat1.EditValue
        orow("berat2") = tberat2.EditValue
        orow("berat3") = tberat3.EditValue

        If cjual.Checked = True Then
            orow("sjual") = 1
        Else
            orow("sjual") = 0
        End If

        If cpinjam.Checked = True Then
            orow("spinjam") = 1
        Else
            orow("spinjam") = 0
        End If

        If csewa.Checked = True Then
            orow("ssewa") = 1
        Else
            orow("ssewa") = 0
        End If

        If charus.Checked = True Then
            orow("hrusfaktur") = 1
        Else
            orow("hrusfaktur") = 0
        End If


        Dim ksales As Integer
        If csales.Checked = True Then
            ksales = 1
        Else
            ksales = 0
        End If

        Dim ksupir As Integer
        If csupir.Checked = True Then
            ksupir = 1
        Else
            ksupir = 0
        End If

        Dim kkenek As Integer
        If ckenek.Checked = True Then
            kkenek = 1
        Else
            kkenek = 0
        End If

        orow("ins_sales") = ksales
        orow("ins_supir") = ksupir
        orow("ins_kenek") = kkenek

        orow("hargabeli") = thargabeli.EditValue
        orow("hargajual") = thargajual.EditValue
        orow("hargasewa") = thargasew.EditValue
        orow("nohrus") = tnourut.EditValue

        orow("pos_pinjam") = 0
        orow("pos_sewa") = 0

        orow("jmlstok1") = 0
        orow("jmlstok2") = 0
        orow("jmlstok3") = 0

        orow("jmlstok_k1") = 0
        orow("jmlstok_k2") = 0
        orow("jmlstok_k3") = 0

        orow("jmlstok_f1") = 0
        orow("jmlstok_f2") = 0
        orow("jmlstok_f3") = 0

        orow("jenis") = tjenis.EditValue
        orow("kelompok") = tkelompok.EditValue

        If tpengganti.EditValue = "None" Then
            orow("kd_barang_kmb") = ""
        Else
            orow("kd_barang_kmb") = tpengganti.EditValue
        End If

        If tnon.EditValue = "None" Then
            orow("kd_barang_jmn") = ""
        Else
            orow("kd_barang_jmn") = tnon.EditValue
        End If


        dv.EndInit()

    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click


        If tkode.Text.Trim.Length = 0 Then
            MsgBox("Kode tidak boleh kosong...", vbOKOnly + vbExclamation, "Informasi")
            tkode.Focus()
            Return
        End If

        If tnama.Text.Trim.Length = 0 Then
            MsgBox("Nama barang tidak boleh kosong...", vbOKOnly + vbExclamation, "Informasi")
            tnama.Focus()
            Return
        End If

        If tqty1.EditValue = 0 Or tqty2.EditValue = 0 Or tqty3.EditValue = 0 Then
            MsgBox("Qty min 1 !!!", vbOKOnly + vbInformation, "Informasi")
            tqty1.Focus()
        End If

        simpan()

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fkab2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If addstat = True Then
            tkode.Focus()
        Else
            tnama.Focus()
        End If
    End Sub

    Private Sub fkab2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If addstat = True Then

            tkode.Enabled = True
            kosongkan()

            isi_pengganti()
            isi_nonfisik()

        Else
            tkode.Enabled = False
            isi()

        End If

    End Sub

    Private Sub cjual_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cjual.CheckedChanged
        If cjual.Checked = True Then
            cjual.Text = "Ya"
            thargajual.Enabled = True
        Else
            cjual.Text = "Tidak"
            thargajual.Enabled = False
            thargajual.EditValue = 0
        End If
    End Sub

    Private Sub csewa_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles csewa.CheckedChanged
        If csewa.Checked = True Then
            csewa.Text = "Ya"
            thargasew.Enabled = True
        Else
            csewa.Text = "Tidak"
            thargasew.Enabled = False
            thargasew.EditValue = 0
        End If
    End Sub

    Private Sub cpinjam_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cpinjam.CheckedChanged
        If cpinjam.Checked = True Then
            cpinjam.Text = "Ya"
        Else
            cpinjam.Text = "Tidak"
        End If
    End Sub

    Private Sub charus_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles charus.CheckedChanged
        If charus.Checked = True Then
            charus.Text = "Ya"
            tnourut.Enabled = True
        Else
            tnourut.Enabled = False
            tnourut.EditValue = 0
            charus.Text = "Tidak"
        End If
    End Sub

    Private Sub tberat1_Validated(sender As System.Object, e As System.EventArgs) Handles tberat1.Validated
        hitung_berat23(False)
    End Sub

    Private Sub tberat2_Validated(sender As System.Object, e As System.EventArgs) Handles tberat2.Validated
        hitung_berat23(True)
    End Sub

End Class