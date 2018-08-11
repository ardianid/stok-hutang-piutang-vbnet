Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class ffaktur_to6

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Public jenisjual As String
    Public kdsales As String

    Private dvs As DataView

    Private vqty1, vqty2, vqty3 As Integer
    Private vharga2, vharga3 As Double
    Private kqty As Integer

    Private Sub isi()

        Dim kdbar As String = dv(position)("kd_barang").ToString
        Dim kdgud As String = dv(position)("kd_gudang").ToString

        tgudang.EditValue = kdgud

        isi_barang()
        tbarang.EditValue = kdbar

        tharga.EditValue = dv(position)("harga").ToString

        tjml.EditValue = dv(position)("qty").ToString

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
            Return
        End If

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
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvg = dvm.CreateDataView(ds.Tables(0))

            tgudang.Properties.DataSource = dvg

            tgudang.EditValue = "G000"

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

        sql = String.Format("select a.kd_barang,b.nama_barang,b.satuan1,b.satuan2,b.satuan3,a.jmlstok1,a.jmlstok2,a.jmlstok3,b.qty1,b.qty2,b.qty3,b.hargajual" & _
          " from ms_barang2 a inner join ms_barang b on a.kd_barang=b.kd_barang where b.sjual=1 and b.jenis='FISIK' and a.kd_gudang='{0}'", tgudang.EditValue)

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

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("noid") = 0
        orow("kd_gudang") = tgudang.EditValue
        orow("kd_barang") = tbarang.EditValue
        orow("nama_barang") = tbarang.Text.Trim
        orow("qty") = tjml.EditValue
        orow("satuan") = tsat.EditValue
        orow("disc_per") = tdisc_per.EditValue
        orow("disc_rp") = tdisc_rp.EditValue
        orow("jumlah") = tjumlah.EditValue
        orow("qtykecil") = kqty
        orow("harga") = tharga.EditValue
        orow("qty1") = vqty1
        orow("qty2") = vqty2
        orow("qty3") = vqty3

        dv.EndInit()

        kosongkan()
        tbarang0.Focus()


    End Sub

    Private Sub updateview()

        dv(position)("kd_gudang") = tgudang.EditValue
        dv(position)("kd_barang") = tbarang.EditValue
        dv(position)("nama_barang") = tbarang.Text.Trim
        dv(position)("qty") = tjml.EditValue
        dv(position)("satuan") = tsat.EditValue
        dv(position)("disc_per") = tdisc_per.EditValue
        dv(position)("disc_rp") = tdisc_rp.EditValue
        dv(position)("jumlah") = tjumlah.EditValue
        dv(position)("qtykecil") = kqty
        dv(position)("harga") = tharga.EditValue
        dv(position)("qty1") = vqty1
        dv(position)("qty2") = vqty2
        dv(position)("qty3") = vqty3

        Me.Close()

    End Sub

    Private Sub cek_gudang_mobil_default()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select nopol from ms_supirkenek where nopol in (select nopol from ms_gudang) and kd_sales='{0}'", kdsales)
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
            tbarang0.Focus()
        Else
            tjml.Focus()
        End If
    End Sub

    Private Sub ffaktur_to3_Load(sender As Object, e As System.EventArgs) Handles Me.Load

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

        If tjml.EditValue = 0 Then
            MsgBox("Jumlah harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tjml.Focus()
            Return
        End If

        If tharga.EditValue = 0 Then
            MsgBox("Harga harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tharga.Focus()
            Return
        End If

        If tsat.EditValue = "" Then
            MsgBox("Satuan harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tsat.Focus()
            Return
        End If

        If addstat = True Then
            insertview()
        Else
            updateview()
        End If

    End Sub

    Private Sub tsat_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles tsat.SelectedIndexChanged
        kalkulasi()
    End Sub

    Private Sub tgudang_EditValueChanged(sender As Object, e As System.EventArgs) Handles tgudang.EditValueChanged
        isi_barang()
    End Sub

    Private Sub tbarang0_EditValueChanged(sender As Object, e As System.EventArgs) Handles tbarang0.EditValueChanged
        tbarang.EditValue = tbarang0.EditValue
    End Sub


End Class