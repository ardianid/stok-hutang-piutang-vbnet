Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fadm_gud3

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean
    Public dttrans1 As DataTable

    Private dvs As DataView

    Public kdgudang As String
    Public jenistrans As String

    Private vqty1, vqty2, vqty3 As Integer
    Private kqty As Integer
    Private kqty2 As Integer

    Private berat1, totberat As Decimal

    Private Sub kosongkan()

        tjml.EditValue = 0
        tjml2.EditValue = 0

    End Sub

    Private Sub isi_barang()

        Dim sql As String = String.Format("select a.kd_barang,b.nama_barang,b.satuan1,b.satuan2,b.satuan3,a.jmlstok_f1,a.jmlstok_f2,a.jmlstok_f3,b.qty1,b.qty2,b.qty3,b.berat1" & _
            " from ms_barang2 a inner join ms_barang b on a.kd_barang=b.kd_barang where b.jenis='FISIK' and a.kd_gudang='{0}'", kdgudang)

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

    Private Sub tbarang_EditValueChanged(sender As Object, e As System.EventArgs) Handles tbarang.EditValueChanged

        tbarang0.EditValue = tbarang.EditValue

        tsat.Properties.Items.Clear()

        With tsat.Properties.Items
            .Add(dvs(tbarang.ItemIndex)("satuan1").ToString)
            .Add(dvs(tbarang.ItemIndex)("satuan2").ToString)
            .Add(dvs(tbarang.ItemIndex)("satuan3").ToString)
        End With

        tsat.SelectedIndex = 0

        'tstok1.EditValue = dvs(tbarang.ItemIndex)("jmlstok1").ToString
        'tstok2.EditValue = dvs(tbarang.ItemIndex)("jmlstok2").ToString
        'tstok3.EditValue = dvs(tbarang.ItemIndex)("jmlstok3").ToString

        vqty1 = dvs(tbarang.ItemIndex)("qty1").ToString
        vqty2 = dvs(tbarang.ItemIndex)("qty2").ToString
        vqty3 = dvs(tbarang.ItemIndex)("qty3").ToString

        berat1 = Decimal.Parse(dvs(tbarang.ItemIndex)("berat1").ToString)

        If tjml.EditValue > 0 Or tjml2.EditValue > 0 Then
            kalkulasi()
        End If

    End Sub

    Private Sub isi()

        Dim kdbar As String = dv(position)("kd_barang").ToString

        isi_barang()
        tbarang0.EditValue = kdbar
        tbarang.EditValue = kdbar

        If Integer.Parse(dv(position)("qtyin").ToString) > 0 Then
            tjenis.SelectedIndex = 0
        Else
            tjenis.SelectedIndex = 1
        End If

        tjml.EditValue = dv(position)("qty").ToString
        tsat.EditValue = dv(position)("satuan").ToString

        kalkulasi()


    End Sub

    Private Sub kalkulasi()

        kqty = 0
        kqty2 = 0
        totberat = 0

        If tjml2.EditValue > 0 And tjml.EditValue = 0 Then
            GoTo lanjut2
        End If

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

        If jenistrans.Equals("TR PEMB") Then
            totberat = jml1 * berat1
        End If

        Dim xjumlah As Double = jml1

        If xjumlah > 0 Then

            Select Case tsat.SelectedIndex
                Case 0
                    kqty = (vqty1 * vqty2 * vqty3) * jml1
                Case 1
                    kqty = (vqty3) * jml1
                Case 2
                    kqty = jml1
            End Select


        Else

            kqty = 0

        End If


lanjut2:

        Dim xjumlah2 As Double = tjml2.EditValue
        If xjumlah2 > 0 Then

            Select Case tsat.SelectedIndex
                Case 0
                    kqty2 = (vqty1 * vqty2 * vqty3) * xjumlah2
                Case 1
                    kqty2 = (vqty3) * xjumlah2
                Case 2
                    kqty2 = xjumlah2
            End Select


        Else

            kqty2 = 0

        End If

        '     LabelControl6.Text = kqty

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("noid") = 0
        orow("kd_barang") = tbarang.EditValue
        orow("nama_barang") = tbarang.Text.Trim
        orow("satuan") = tsat.EditValue

        If tjenis.SelectedIndex = 0 Then
            orow("qtyin") = tjml.EditValue
            orow("qtyinkecil") = kqty
            orow("qtyout") = 0
            orow("qtyoutkecil") = 0

            orow("qtyin_bad") = tjml2.EditValue
            orow("qtyinkecil_bad") = kqty2

            orow("berat1") = berat1
            orow("total_berat") = totberat

        Else
            orow("qtyin") = 0
            orow("qtyinkecil") = 0

            orow("qtyin_bad") = 0
            orow("qtyinkecil_bad") = 0

            orow("berat1") = 0
            orow("total_berat") = 0

            orow("qtyout") = tjml.EditValue
            orow("qtyoutkecil") = kqty


        End If

        dv.EndInit()

        kosongkan()
        tbarang0.Focus()

    End Sub

    Private Sub tjml_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tjml.EditValueChanged, tjml2.EditValueChanged
        kalkulasi()
    End Sub

    Private Sub tsat_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles tsat.SelectedIndexChanged
        kalkulasi()
    End Sub

    Private Sub ffaktur_to3_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If addstat = True Then
            tbarang0.Focus()
        Else
            tjml.Focus()
        End If
    End Sub

    Private Sub ffaktur_to3_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        tjml2.EditValue = 0
        tjml2.Enabled = False

        tsat.Enabled = True

        isi_barang()

        Select Case jenistrans
            Case "TR PEMB"
                tjenis.SelectedIndex = 0
                tjenis.Enabled = False

                tjml2.Enabled = True
                tsat.Enabled = False

            Case "TR PENJ TO"
                tjenis.SelectedIndex = 1
                tjenis.Enabled = False
            Case "TR KANVAS"
                tjenis.SelectedIndex = 1
                tjenis.Enabled = False
            Case "TR RET SISA"
                tjenis.SelectedIndex = 0
                tjenis.Enabled = False
            Case "TR RET OUTL"
                tjenis.SelectedIndex = 0
                tjenis.Enabled = False
            Case "TR GLN KOSONG"
                tjenis.Enabled = True
            Case "TR PINJAM"
                tjenis.Enabled = True
            Case "TR SEWA"
                tjenis.Enabled = True
            Case "TR LAIN-LAIN"
                tjenis.Enabled = True
            Case "TR BOCORAN"
                tjenis.SelectedIndex = 1
                tjenis.Enabled = False
            Case "TR KIRIM JABUNG"
                tjenis.SelectedIndex = 1
                tjenis.Enabled = False
        End Select

        If addstat = True Then
            tbarang0.Enabled = True
            tbarang.Enabled = True

            kosongkan()

        Else

            tbarang.Enabled = False
            tbarang.Enabled = False

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

        If tsat.EditValue = "" Then
            MsgBox("Satuan harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tsat.Focus()
            Return
        End If

        If jenistrans.Equals("TR PENJ TO") Then

            If Not IsNothing(dttrans1) Then

                If DateValue(convert_date_to_eng(Date.Now)) <= DateValue(convert_date_to_eng("07/01/2015")) Then
                Else

                    Dim rows() As DataRow = dttrans1.Select(String.Format("kd_barang='{0}' and kd_gudang='{1}'", tbarang.EditValue, kdgudang))

                    If rows.Length <= 0 Then
                        MsgBox("Barang tidak ada dalam daftar rekap barang...", vbOKOnly + vbExclamation, "Informasi")
                        tbarang.Focus()
                        Return
                    End If

                End If

               
            End If

        ElseIf jenistrans.Equals("TR KANVAS") Then

            If Not IsNothing(dttrans1) Then

                Dim rows() As DataRow = dttrans1.Select(String.Format("kd_barang='{0}' and kd_gudang='{1}'", tbarang.EditValue, kdgudang))

                If rows.Length <= 0 Then
                    MsgBox("Barang tidak ada dalam daftar perintah muat...", vbOKOnly + vbExclamation, "Informasi")
                    tbarang.Focus()
                    Return
                End If

            End If
            

        ElseIf jenistrans.Equals("TR RET SISA/OUTL") Then

            'Dim rows() As DataRow = dttrans1.Select(String.Format("kd_barang='{0}' and kd_gudang='{1}'", tbarang.EditValue, kdgudang))

            'If rows.Length <= 0 Then
            '    MsgBox("Barang tidak ada dalam daftar perintah muat...", vbOKOnly + vbExclamation, "Informasi")
            '    tbarang.Focus()
            '   Return
            'End If

        End If

        If addstat = True Then
            insertview()
        End If

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub tbarang0_EditValueChanged(sender As Object, e As System.EventArgs) Handles tbarang0.EditValueChanged
        tbarang.EditValue = tbarang0.EditValue
    End Sub

    Private Sub tjenis_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tjenis.EditValueChanged
        tjml.EditValue = 0
        tjml2.EditValue = 0
    End Sub


End Class