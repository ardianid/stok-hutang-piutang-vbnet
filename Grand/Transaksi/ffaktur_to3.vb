Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class ffaktur_to3

    Public dv As DataView
    Public dvkembali As DataView
    Public position As Integer
    Public addstat As Boolean

    Private dvs As DataView

    Private vqty1, vqty2, vqty3 As Integer
    Private vharga2, vharga3 As Double
    Private kqty As Integer
    Private jumlah0 As Double
    Private kqty0 As Integer

    Public jenisjual As String
    Public spulang As Boolean
    Public kdsales As String
    Public tanggalfaktur As String

    Private qtyold As Integer

    Private Sub isi()

        Dim kdbar As String = dv(position)("kd_barang").ToString
        Dim kdgud As String = dv(position)("kd_gudang").ToString

        tgudang.EditValue = kdgud

        isi_barang()
        tbarang0.EditValue = kdbar
        tbarang.EditValue = kdbar

        tharga.EditValue = dv(position)("harga").ToString

        tjml.EditValue = dv(position)("qty").ToString

        qtyold = dv(position)("qty0").ToString

        tdisc_per.EditValue = dv(position)("disc_per").ToString
        tdisc_rp.EditValue = dv(position)("disc_rp").ToString
        tjumlah.EditValue = dv(position)("jumlah").ToString

        kalkulasi()

      
    End Sub

    Private Sub tbarang_EditValueChanged(sender As Object, e As System.EventArgs) Handles tbarang.EditValueChanged

        tbarang0.EditValue = tbarang.EditValue

        tsat.Properties.Items.Clear()

        With tsat.Properties.Items
            .Add(dvs(tbarang.ItemIndex)("satuan1").ToString)
            .Add(dvs(tbarang.ItemIndex)("satuan2").ToString)
            .Add(dvs(tbarang.ItemIndex)("satuan3").ToString)
        End With

        tsat.SelectedIndex = 0

        tstok1.EditValue = dvs(tbarang.ItemIndex)("jmlstok1").ToString
        tstok2.EditValue = dvs(tbarang.ItemIndex)("jmlstok2").ToString
        tstok3.EditValue = dvs(tbarang.ItemIndex)("jmlstok3").ToString

        tharga.EditValue = dvs(tbarang.ItemIndex)("hargajual").ToString

        vqty1 = dvs(tbarang.ItemIndex)("qty1").ToString
        vqty2 = dvs(tbarang.ItemIndex)("qty2").ToString
        vqty3 = dvs(tbarang.ItemIndex)("qty3").ToString

        tjml_EditValueChanged(Nothing, Nothing)

    End Sub

    Private Sub kosongkan()

        tstok1.EditValue = 0
        tstok2.EditValue = 0
        tstok3.EditValue = 0

        tjml.EditValue = 0
        tsat.Properties.Items.Clear()
        tharga.EditValue = 0
        tdisc_per.EditValue = 0.0
        tdisc_rp.EditValue = 0
        tjumlah.EditValue = 0

        vqty1 = 0
        vqty2 = 0
        vqty3 = 0


    End Sub

    Private Sub kalkulasi()

        If tjml.EditValue = 0 Then

            tdisc_per.EditValue = 0
            tdisc_rp.EditValue = 0
            tjumlah.EditValue = 0

            kqty = 0
            kqty0 = kqty

            Return
        End If

        'If tharga.EditValue = 0 Then
        '    vharga2 = 0
        '    vharga3 = 0
        '    tjumlah.EditValue = 0
        '    Return
        'End If

        Dim jml As String = tjml.EditValue
        Dim jml1 As Integer
        If Not jml.Equals("") Then
            jml1 = Integer.Parse(jml)
        Else
            jml1 = 0
        End If

        Dim xharga As Double = tharga.EditValue
        Dim disc_rp As Double
        Dim xjumlah As Double = jml1

        jumlah0 = xharga * xjumlah

        If xharga > 0 Then

            vharga2 = xharga / vqty2
            vharga3 = vharga2 / vqty3

            disc_rp = tdisc_rp.EditValue

            Select Case tsat.SelectedIndex
                Case 0
                    xjumlah = (xharga * xjumlah) - disc_rp
                    kqty = (vqty1 * vqty2 * vqty3) * jml1
                Case 1
                    xjumlah = (vharga2 * xjumlah) - disc_rp
                    kqty = (vqty3) * jml1
                Case 2
                    xjumlah = (vharga3 * xjumlah) - disc_rp
                    kqty = jml1
            End Select

            tjumlah.EditValue = xjumlah

        Else

            Select Case tsat.SelectedIndex
                Case 0
                    kqty = (vqty1 * vqty2 * vqty3) * jml1
                Case 1
                    kqty = (vqty3) * jml1
                Case 2
                    kqty = jml1
            End Select

            vharga2 = 0
            vharga3 = 0

            tjumlah.EditValue = 0

        End If

        kqty0 = kqty

    End Sub

    Private Sub isi_gudang()

        Dim sql As String = ""

        If jenisjual = "T" Then
            sql = "select kd_gudang,nama_gudang from ms_gudang where tipe_gudang='FISIK'"
        Else
            sql = "select kd_gudang,nama_gudang from ms_gudang where tipe_gudang='MOBIL'"
        End If

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet
        Dim dvg As DataView

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(Sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvg = dvm.CreateDataView(ds.Tables(0))

            tgudang.Properties.DataSource = dvg

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

    Private Sub isi_barang()

        Dim sql As String = ""

        If jenisjual = "T" Then
            sql = String.Format("select a.kd_barang,b.nama_barang,b.satuan1,b.satuan2,b.satuan3,a.jmlstok1,a.jmlstok2,a.jmlstok3,b.qty1,b.qty2,b.qty3,b.hargajual" & _
              " from ms_barang2 a inner join ms_barang b on a.kd_barang=b.kd_barang where b.jenis='FISIK' and b.sjual=1 and a.kd_gudang='{0}'", tgudang.EditValue)
        Else
            sql = String.Format("select a.kd_barang,b.nama_barang,b.satuan1,b.satuan2,b.satuan3,a.jmlstok_k1 as jmlstok1,a.jmlstok_k2 as jmlstok2,a.jmlstok_k3 as jmlstok3,b.qty1,b.qty2,b.qty3,b.hargajual" & _
            " from ms_barang2 a inner join ms_barang b on a.kd_barang=b.kd_barang where b.jenis='FISIK' and b.sjual=1 and a.kd_gudang='{0}'", tgudang.EditValue)
        End If


        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvs = dvm.CreateDataView(ds.Tables(0))

            tbarang0.Properties.DataSource = dvs
            tbarang.Properties.DataSource = dvs

            'tbarang.ItemIndex = 0
            'tbarang_EditValueChanged(Nothing, Nothing)

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

    Private Function cekjml_real() As Integer

        Dim cn As OleDbConnection = Nothing
        Dim jml As Integer = 0

        Try

            If dv(position)("noid").ToString.Equals("0") Then
                jml = tjml.EditValue
            Else

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim sql As String = String.Format("select qty from trfaktur_to2 where noid={0}", dv(position)("noid").ToString)

                Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dr As OleDbDataReader = cmd.ExecuteReader

                If dr.Read Then
                    If IsNumeric(dr(0).ToString) Then
                        jml = Integer.Parse(dr(0).ToString)
                    Else
                        jml = 0
                    End If
                Else
                    jml = 0
                End If

                dr.Close()

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

        Return jml

    End Function

    Private Function melebihi() As Boolean

        Dim hasil As Boolean

        If tsat.SelectedIndex = 0 Then

            If addstat = True Then
                If tjml.EditValue > tstok1.EditValue Then
                    hasil = True
                Else
                    hasil = False
                End If
            Else

                Dim qtyakhir As Integer = cekjml_real() + tstok1.EditValue

                If tjml.EditValue > qtyakhir Then
                    hasil = True
                Else
                    hasil = False
                End If

            End If

            

        ElseIf tsat.SelectedIndex = 1 Then

            If addstat = True Then
                If tjml.EditValue > vqty2 Then
                    hasil = True
                Else
                    hasil = False
                End If

            Else

                Dim qtyakhir As Integer = cekjml_real() + vqty2

                If tjml.EditValue > qtyakhir Then
                    hasil = True
                Else
                    hasil = False
                End If



            End If

            

        ElseIf tsat.SelectedIndex = 2 Then


            If addstat = True Then

                If tjml.EditValue > vqty3 Then
                    hasil = False
                Else
                    hasil = False
                End If

            Else

                Dim qtyakhir As Integer = cekjml_real() + vqty3

                If tjml.EditValue > qtyakhir Then
                    hasil = True
                Else
                    hasil = False
                End If


            End If

           

        End If

        Return hasil

    End Function

    Private Sub insertview()

        Dim dta As DataTable = dv.ToTable

        Dim rows() As DataRow = dta.Select(String.Format("kd_barang='{0}' and satuan='{1}'", tbarang.EditValue, tsat.EditValue))

        If rows.Length > 0 Then
            MsgBox("Barang sudah ada dalam daftar...", vbOKOnly + vbInformation, "Informasi")
            tbarang.Focus()
            Return
        End If

        Dim orow As DataRowView = dv.AddNew
        orow("noid") = 0
        orow("kd_gudang") = tgudang.EditValue
        orow("nama_gudang") = tgudang.Text.Trim
        orow("kd_barang") = tbarang.EditValue
        orow("nama_barang") = tbarang.Text.Trim
        orow("qty") = tjml.EditValue
        orow("qty0") = tjml.EditValue
        orow("satuan") = tsat.EditValue
        orow("disc_per") = tdisc_per.EditValue
        orow("disc_rp") = tdisc_rp.EditValue
        orow("jumlah") = tjumlah.EditValue
        orow("qtykecil") = kqty
        orow("qtykecil0") = kqty0
        orow("hargakecil") = vharga3
        orow("jumlah0") = jumlah0
        orow("harga") = tharga.EditValue

        orow("jenis") = "FISIK"

        orow("qty1") = vqty1
        orow("qty2") = vqty2
        orow("qty3") = vqty3

        dv.EndInit()

        cek_non()
        cek_harus_kembali()

        kosongkan()
        tbarang0.Focus()

    End Sub

    Private Sub cek_non()

        Dim cn As OleDbConnection = Nothing

        Try


            Dim nosudah As Integer = 0
            Dim noada As Boolean = False

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select kd_barang_jmn from ms_barang where kd_barang='{0}'", tbarang.EditValue)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Not (drd(0).ToString.Equals("")) Then

                    Dim sqlc As String = String.Format("select * from ms_barang where jenis='NON FISIK' and kd_barang='{0}'", drd(0).ToString)
                    Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn)
                    Dim drdc As OleDbDataReader = cmdc.ExecuteReader

                    If drdc.Read Then

                        If Not (drdc("kd_barang").ToString.Equals("")) Then

                            If dv.Count > 0 Then

                                For i As Integer = 0 To dv.Count - 1
                                    If dv(i)("kd_barang").ToString.Equals(drdc("kd_barang").ToString) And dv(i)("satuan").ToString.Equals(tsat.EditValue) Then
                                        nosudah = i
                                        noada = True
                                        Exit For
                                    End If
                                Next

                            End If

                            Dim xharga As Double

                            If noada Then
                                xharga = Double.Parse(dv(nosudah)("harga").ToString)
                            Else
                                xharga = Double.Parse(drdc("hargajual").ToString)
                            End If

                            Dim disc_rp As Double
                            Dim xjumlah As Integer = tjml.EditValue
                            

                            Dim qty1, qty2, qty3 As Integer
                            qty1 = Integer.Parse(drdc("qty1").ToString)
                            qty2 = Integer.Parse(drdc("qty2").ToString)
                            qty3 = Integer.Parse(drdc("qty3").ToString)

                            Dim harga2 As Double = 0
                            Dim harga3 As Double = 0

                            Dim sjumlah0 As Double = xharga * xjumlah
                            Dim totjumlah As Double = 0

                            If xharga > 0 Then

                                harga2 = xharga / qty2
                                harga3 = harga2 / qty3

                                disc_rp = 0

                                Select Case tsat.SelectedIndex
                                    Case 0
                                        kqty = (qty1 * qty2 * qty3) * xjumlah
                                        xjumlah = (xharga * xjumlah)
                                    Case 1
                                        kqty = (qty3) * xjumlah
                                        xjumlah = (harga2 * xjumlah) - disc_rp
                                    Case 2
                                        kqty = xjumlah
                                        xjumlah = (harga3 * xjumlah) - disc_rp
                                End Select

                                totjumlah = xjumlah

                            Else

                                Select Case tsat.SelectedIndex
                                    Case 0
                                        kqty = (qty1 * qty2 * qty3) * xjumlah
                                    Case 1
                                        kqty = (qty3) * xjumlah
                                    Case 2
                                        kqty = xjumlah
                                End Select

                                harga2 = 0
                                harga3 = 0

                                totjumlah = 0

                            End If

                            If noada Then

                                dv(nosudah)("qty") = tjml.EditValue
                                dv(nosudah)("satuan") = tsat.EditValue
                                dv(nosudah)("disc_per") = Double.Parse(0.0)
                                dv(nosudah)("disc_rp") = Double.Parse(0)

                                dv(nosudah)("jumlah") = totjumlah
                                dv(nosudah)("qtykecil") = kqty
                                dv(nosudah)("hargakecil") = harga3
                                dv(nosudah)("jumlah0") = sjumlah0

                                If jenisjual.Equals("T") Then
                                    If Not spulang = True Then
                                        dv(position)("qty0") = tjml.EditValue
                                        dv(position)("qtykecil0") = kqty0
                                    End If
                                Else
                                    dv(position)("qty0") = tjml.EditValue
                                    dv(position)("qtykecil0") = kqty0
                                End If

                            Else

                                Dim orow As DataRowView = dv.AddNew
                                orow("noid") = 0
                                orow("kd_gudang") = tgudang.EditValue
                                orow("nama_gudang") = tgudang.Text.Trim
                                orow("kd_barang") = drdc("kd_barang").ToString
                                orow("nama_barang") = drdc("nama_barang").ToString
                                orow("qty") = tjml.EditValue
                                orow("satuan") = tsat.EditValue
                                orow("disc_per") = Double.Parse(0.0)
                                orow("disc_rp") = Double.Parse(0)

                                orow("jumlah") = totjumlah
                                orow("qtykecil") = kqty
                                orow("hargakecil") = harga3
                                orow("jumlah0") = sjumlah0
                                orow("harga") = xharga

                                orow("qty0") = tjml.EditValue
                                orow("qtykecil0") = kqty0

                                orow("jenis") = "NON FISIK"

                                orow("qty1") = qty1
                                orow("qty2") = qty2
                                orow("qty3") = qty3

                                dv.EndInit()

                            End If



                        End If

                    End If

                    drdc.Close()

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

    Private Sub cek_harus_kembali()

        Dim cn As OleDbConnection = Nothing

        Try

            Dim nosudah As Integer = 0
            Dim noada As Boolean = False

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select kd_barang_kmb from ms_barang where kd_barang='{0}'", tbarang.EditValue)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Not (drd(0).ToString.Equals("")) Then

                    Dim sqlc As String = String.Format("select * from ms_barang where jenis='FISIK' and kd_barang='{0}'", drd(0).ToString)
                    Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn)
                    Dim drdc As OleDbDataReader = cmdc.ExecuteReader

                    If drdc.Read Then

                        If Not (drdc("kd_barang").ToString.Equals("")) Then

                            If dvkembali.Count > 0 Then

                                For i As Integer = 0 To dvkembali.Count - 1
                                    If dvkembali(i)("kd_barang").ToString.Equals(drdc("kd_barang").ToString) And dvkembali(i)("satuan").ToString.Equals(tsat.EditValue) Then
                                        nosudah = i
                                        noada = True
                                        Exit For
                                    End If
                                Next

                            End If

                            Dim xharga As Double

                            If noada Then
                                xharga = Double.Parse(dvkembali(nosudah)("hargajual").ToString)
                            Else
                                xharga = Double.Parse(drdc("hargajual").ToString)
                            End If

                            Dim disc_rp As Double
                            Dim xjumlah As Integer

                            If addstat = True Then

                                If jenisjual = "T" Then
                                    xjumlah = 0
                                Else
                                    xjumlah = tjml.EditValue
                                End If

                            Else

                                If noada Then
                                    xjumlah = Integer.Parse(dvkembali(nosudah)("qty").ToString)
                                Else

                                    If addstat = True Then
                                        xjumlah = 0
                                    Else
                                        xjumlah = 0
                                    End If


                                End If

                            End If

                            Dim qty1, qty2, qty3 As Integer
                            qty1 = Integer.Parse(drdc("qty1").ToString)
                            qty2 = Integer.Parse(drdc("qty2").ToString)
                            qty3 = Integer.Parse(drdc("qty3").ToString)

                            Dim harga2 As Double = 0
                            Dim harga3 As Double = 0

                            Dim sjumlah0 As Double = xharga * xjumlah
                            Dim totjumlah As Double = 0

                            If xharga > 0 Then

                                harga2 = xharga / qty2
                                harga3 = harga2 / qty3

                                disc_rp = 0

                                Select Case tsat.SelectedIndex
                                    Case 0
                                        kqty = (qty1 * qty2 * qty3) * xjumlah
                                        xjumlah = (xharga * xjumlah)
                                    Case 1
                                        kqty = (qty3) * xjumlah
                                        xjumlah = (harga2 * xjumlah) - disc_rp
                                    Case 2
                                        kqty = xjumlah
                                        xjumlah = (harga3 * xjumlah) - disc_rp
                                End Select

                                totjumlah = xjumlah

                            Else

                                Select Case tsat.SelectedIndex
                                    Case 0
                                        kqty = (qty1 * qty2 * qty3) * xjumlah
                                    Case 1
                                        kqty = (qty3) * xjumlah
                                    Case 2
                                        kqty = xjumlah
                                End Select

                                harga2 = 0
                                harga3 = 0

                                totjumlah = 0

                            End If


                            If noada Then

                                dvkembali(nosudah)("qty") = Integer.Parse(dvkembali(nosudah)("qty").ToString)
                                dvkembali(nosudah)("satuan") = tsat.EditValue

                                dvkembali(nosudah)("jumlah") = totjumlah
                                dvkembali(nosudah)("qtykecil") = kqty
                                dvkembali(nosudah)("hargakecil") = harga3
                                dvkembali(nosudah)("hargajual") = xharga

                            Else

                                Dim orow As DataRowView = dvkembali.AddNew

                                orow("kd_barang") = drdc("kd_barang").ToString
                                orow("nama_barang") = drdc("nama_barang").ToString

                                If addstat = True Then

                                    If jenisjual = "T" Then
                                        orow("qty") = 0
                                    Else
                                        orow("qty") = tjml.EditValue
                                    End If


                                Else

                                    If noada = True Then

                                        If addstat = True Then

                                            If jenisjual = "T" Then
                                                orow("qty") = 0
                                            Else
                                                orow("qty") = tjml.EditValue
                                            End If

                                        Else
                                            orow("qty") = Integer.Parse(dvkembali(nosudah)("qty").ToString)
                                        End If

                                    Else

                                        If jenisjual = "T" Then
                                            orow("qty") = 0
                                        Else
                                            orow("qty") = tjml.EditValue
                                        End If

                                    End If

                                End If


                                orow("satuan") = tsat.EditValue

                                orow("jumlah") = totjumlah
                                orow("qtykecil") = kqty
                                orow("hargakecil") = harga3
                                orow("hargajual") = xharga

                                orow("qty1") = qty1
                                orow("qty2") = qty2
                                orow("qty3") = qty3

                                orow("satuan1") = drdc("satuan1").ToString
                                orow("satuan2") = drdc("satuan2").ToString
                                orow("satuan3") = drdc("satuan3").ToString

                                dv.EndInit()

                            End If

                        End If

                    End If

                    drdc.Close()

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

    Private Sub updateview()

        dv(position)("kd_gudang") = tgudang.EditValue
        dv(position)("nama_gudang") = tgudang.Text.Trim
        dv(position)("kd_barang") = tbarang.EditValue
        dv(position)("nama_barang") = tbarang.Text.Trim
        dv(position)("qty") = tjml.EditValue

        dv(position)("satuan") = tsat.EditValue
        dv(position)("disc_per") = tdisc_per.EditValue
        dv(position)("disc_rp") = tdisc_rp.EditValue
        dv(position)("jumlah") = tjumlah.EditValue
        dv(position)("qtykecil") = kqty

        dv(position)("hargakecil") = vharga3
        dv(position)("jumlah0") = jumlah0
        dv(position)("harga") = tharga.EditValue

        If jenisjual.Equals("T") Then
            If Not spulang = True Then
                dv(position)("qty0") = tjml.EditValue
                dv(position)("qtykecil0") = kqty0
            End If
        Else
            dv(position)("qty0") = tjml.EditValue
            dv(position)("qtykecil0") = kqty0
        End If

        dv(position)("jenis") = "FISIK"

        dv(position)("qty1") = vqty1
        dv(position)("qty2") = vqty2
        dv(position)("qty3") = vqty3

        cek_non()
        cek_harus_kembali()

        Me.Close()

    End Sub

    Private Sub cek_gudang_mobil_default()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            '   Dim sql As String = String.Format("select nopol from ms_supirkenek where nopol in (select nopol from ms_gudang) and kd_sales='{0}'", kdsales)
            Dim sql As String = String.Format("select kd_gudang from trspm where kd_sales='{0}' and tglberangkat='{1}'", kdsales, convert_date_to_eng(tanggalfaktur))
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Not drd(0).ToString.Equals("") Then
                    tgudang.EditValue = drd(0).ToString
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

    Private Sub tdisc_per_Validated(sender As System.Object, e As System.EventArgs) Handles tdisc_per.Validated

        If tdisc_per.EditValue > 0 Then

            Dim jml As String = tjml.EditValue
            Dim jml1 As Integer
            If Not jml.Equals("") Then
                jml1 = Integer.Parse(jml)
            Else
                jml1 = 0
            End If

            Dim xharga As Double = tharga.EditValue
            Dim vharga2, vharga3 As Double
            Dim disc_rp As Double
            Dim xjumlah As Double = jml1

            If xharga > 0 Then

                vharga2 = xharga / vqty2
                vharga3 = vharga2 / vqty3

                Select Case tsat.SelectedIndex
                    Case 0
                        xjumlah = (xharga * xjumlah)
                    Case 1
                        xjumlah = (xharga * vharga2)
                    Case 2
                        xjumlah = (xharga * vharga3)
                End Select

                disc_rp = xjumlah * (tdisc_per.EditValue / 100)

                tdisc_rp.EditValue = disc_rp

            Else

                tdisc_rp.EditValue = 0

            End If

        Else
            tdisc_rp.EditValue = 0
        End If

    End Sub

    Private Sub tdisc_rp_EditValueChanged(sender As Object, e As System.EventArgs) Handles tdisc_rp.EditValueChanged
        kalkulasi()
    End Sub

    Private Sub tdisc_rp_Validated(sender As System.Object, e As System.EventArgs) Handles tdisc_rp.Validated
        If tdisc_rp.EditValue > 0 Then

            Dim jml As String = tjml.EditValue
            Dim jml1 As Integer
            If Not jml.Equals("") Then
                jml1 = Integer.Parse(jml)
            Else
                jml1 = 0
            End If

            Dim xharga As Double = tharga.EditValue
            Dim vharga2, vharga3 As Double
            Dim disc_rp As Double
            Dim xjumlah As Double = jml1

            If xharga > 0 Then

                vharga2 = xharga / vqty2
                vharga3 = vharga2 / vqty3

                Select Case tsat.SelectedIndex
                    Case 0
                        xjumlah = (xharga * xjumlah)
                    Case 1
                        xjumlah = (xharga * vharga2)
                    Case 2
                        xjumlah = (xharga * vharga3)
                End Select

                disc_rp = (tdisc_rp.EditValue / xjumlah) * 100

                tdisc_per.EditValue = disc_rp

            Else

                tdisc_per.EditValue = 0.0

            End If

        Else
            tdisc_per.EditValue = 0.0
        End If

    End Sub

    Private Sub tjml_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tjml.EditValueChanged
        kalkulasi()
    End Sub

    Private Sub tharga_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tharga.EditValueChanged
        kalkulasi()
    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub ffaktur_to3_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If addstat = True Then

            If jenisjual.Equals("T") Then
                tbarang0.Focus()
            Else
                tbarang0.Focus()
            End If

        Else
            tjml.Focus()
        End If
    End Sub

    Private Sub ffaktur_to3_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If jenisjual = "T" Then
            Me.Text = "Faktur TO"
        Else
            Me.Text = "Faktur Kanvas"
        End If


        isi_gudang()

        If addstat = True Then
            tbarang0.Enabled = True
            tbarang.Enabled = True
            tgudang.Enabled = True
            '    tsat.Enabled = True

            kosongkan()

            If jenisjual = "T" Then
                tgudang.EditValue = "G000"
            Else
                cek_gudang_mobil_default()
            End If



        Else

            tbarang0.Enabled = False
            tbarang.Enabled = False
            tgudang.Enabled = False
            '    tsat.Enabled = False

            isi()

        End If
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tbarang.EditValue = "" Then
            MsgBox("Barang harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tbarang.Focus()
            Return
        End If

        'If tjml.EditValue = 0 Then
        '    MsgBox("Jumlah harus diisi...", vbOKOnly + vbInformation, "Informasi")
        '    tjml.Focus()
        '    Return
        'End If

        'If tharga.EditValue = 0 Then
        '    MsgBox("Harga harus diisi...", vbOKOnly + vbInformation, "Informasi")
        '    tharga.Focus()
        '    Return
        'End If

        If tsat.EditValue = "" Then
            MsgBox("Satuan harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tsat.Focus()
            Return
        End If

        'If melebihi() = True Then
        '    MsgBox("Periksa kembali jumlah jual,karna melebihi qty barang...", vbOKOnly + vbExclamation, "Konfirmasi")
        '    tjml.Focus()
        '    Return
        'End If

        If addstat = True Then
            insertview()
        Else

            If spulang = True And jenisjual = "T" Then
                If qtyold < tjml.EditValue Then
                    MsgBox("Jumlah barang melebihi dari yang sudah dimuat...", vbOKOnly + vbInformation, "Informasi")
                    'tjml.Focus()
                    'Exit Sub
                End If
            End If

            updateview()
        End If

    End Sub

    Private Sub tsat_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles tsat.SelectedIndexChanged
        kalkulasi()
        'Dim harga As Double = dvs(tbarang.ItemIndex)("hargajual").ToString
        'Dim harga2 As Double = harga / vqty2
        'Dim harga3 As Double = harga2 / vqty3

        'Select Case tsat.SelectedIndex
        '    Case 0
        '        tharga.EditValue = harga
        '    Case 1
        '        tharga.EditValue = harga2
        '    Case 2
        '        tharga.EditValue = harga3
        'End Select

    End Sub

    Private Sub tgudang_EditValueChanged(sender As Object, e As System.EventArgs) Handles tgudang.EditValueChanged
        isi_barang()
    End Sub

    Private Sub tbarang0_EditValueChanged(sender As Object, e As System.EventArgs) Handles tbarang0.EditValueChanged
        tbarang.EditValue = tbarang0.EditValue
    End Sub

End Class