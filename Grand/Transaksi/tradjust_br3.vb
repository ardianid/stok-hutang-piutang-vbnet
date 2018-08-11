Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class tradjust_br3

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean
    Public kdgudang As String
    Public jenisjual As String

    Private dvs As DataView

    Private vqty1, vqty2, vqty3 As Integer
    Private kstok As Integer
    Private kselisih As Integer
    Private kqty As Integer

    Private Sub isi()

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

        vqty1 = dvs(tbarang.ItemIndex)("qty1").ToString
        vqty2 = dvs(tbarang.ItemIndex)("qty2").ToString
        vqty3 = dvs(tbarang.ItemIndex)("qty3").ToString

        kstok = dvs(tbarang.ItemIndex)("jmlstok").ToString

        If tjml.EditValue > 0 Then
            kalkulasi()
        End If

    End Sub

    Private Sub kosongkan()

        tjml.EditValue = 0
        tsel.EditValue = 0

    End Sub

    Private Sub isi_barang()

        Dim sql As String = ""

        If jenisjual = "STOK KOMP" Then
            sql = String.Format("select a.kd_barang,b.nama_barang,b.satuan1,b.satuan2,b.satuan3,a.jmlstok1,a.jmlstok2,a.jmlstok3,b.qty1,b.qty2,b.qty3,a.jmlstok" & _
              " from ms_barang2 a inner join ms_barang b on a.kd_barang=b.kd_barang where b.jenis='FISIK' and b.sjual=1 and a.kd_gudang='{0}'", kdgudang)
        Else
            sql = String.Format("select a.kd_barang,b.nama_barang,b.satuan1,b.satuan2,b.satuan3,a.jmlstok_f1 as jmlstok1,a.jmlstok_f2 as jmlstok2,a.jmlstok_f3 as jmlstok3,b.qty1,b.qty2,b.qty3,a.jmlstok_f as jmlstok" & _
            " from ms_barang2 a inner join ms_barang b on a.kd_barang=b.kd_barang where b.jenis='FISIK' and b.sjual=1 and a.kd_gudang='{0}'", kdgudang)
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

    Private Sub kalkulasi()

        'If tjml.EditValue = 0 Then
        '    Return
        'End If

        Dim jml As String = tjml.EditValue
        Dim jml1 As Integer
        If Not jml.Equals("") Then
            jml1 = Integer.Parse(jml)
        Else
            jml1 = 0
        End If

            Select Case tsat.SelectedIndex
                Case 0
                    kqty = (vqty1 * vqty2 * vqty3) * jml1
                Case 1
                kqty = (vqty3) * jml1
                Case 2
                kqty = jml1
            End Select

        kselisih = kstok - kqty

        Select Case tsat.SelectedIndex
            Case 0
                tsel.EditValue = tstok1.EditValue - tjml.EditValue
            Case 1
                tsel.EditValue = tstok2.EditValue - tjml.EditValue
            Case 2
                tsel.EditValue = tstok3.EditValue - tjml.EditValue
        End Select

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("noid") = 0
        
        orow("kd_barang") = tbarang.EditValue
        orow("nama_barang") = tbarang.Text.Trim
        orow("satuan") = tsat.EditValue

        If tsat.SelectedIndex = 0 Then
            orow("qty_asal") = tstok1.EditValue
        ElseIf tsat.SelectedIndex = 1 Then
            orow("qty_asal") = tstok2.EditValue
        Else
            orow("qty_asal") = tstok3.EditValue
        End If

        orow("qty_sel") = tsel.EditValue
        orow("qty_akh") = tjml.EditValue
        orow("qtykecil_asal") = kstok
        orow("qtykecil_sel") = kselisih
        orow("qtykecil_akh") = kqty

        dv.EndInit()

        kosongkan()
        tbarang0.Focus()

    End Sub

    Private Sub updateview()

        dv(position)("kd_barang") = tbarang.EditValue
        dv(position)("nama_barang") = tbarang.Text.Trim
        dv(position)("satuan") = tsat.EditValue

        If tsat.SelectedIndex = 0 Then
            dv(position)("qty_asal") = tstok1.EditValue
        ElseIf tsat.SelectedIndex = 1 Then
            dv(position)("qty_asal") = tstok2.EditValue
        Else
            dv(position)("qty_asal") = tstok3.EditValue
        End If

        dv(position)("qty_sel") = tsel.EditValue
        dv(position)("qty_akh") = tjml.EditValue
        dv(position)("qtykecil_asal") = kstok
        dv(position)("qtykecil_sel") = kselisih
        dv(position)("qtykecil_akh") = kqty

        Me.Close()

    End Sub

    Private Sub tjml_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tjml.EditValueChanged
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

        isi_barang()

        If addstat = True Then

            tbarang0.Enabled = True
            tbarang.Enabled = True

            kosongkan()

        Else

            tbarang0.Enabled = False
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

        'If tjml.EditValue = 0 Then
        '    MsgBox("Jumlah harus diisi...", vbOKOnly + vbInformation, "Informasi")
        '    tjml.Focus()
        '    Return
        'End If

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

    Private Sub tbarang0_EditValueChanged(sender As Object, e As System.EventArgs) Handles tbarang0.EditValueChanged
        tbarang.EditValue = tbarang0.EditValue
    End Sub
End Class