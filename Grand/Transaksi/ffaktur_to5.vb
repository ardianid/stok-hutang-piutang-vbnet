Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class ffaktur_to5

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean
    Public jenisjual As String
    Public spulang As Boolean

    Dim kqty As Integer = 0
    Dim jumlah1 As Double
    Dim hargakecil1 As Double
    Public satuan As String
    Public harga As Double

    Private statload As Boolean = True

    Private Sub isidata()

        tkode.Text = dv(position)("kd_barang").ToString
        tnama.Text = dv(position)("nama_barang").ToString

        tjml.EditValue = dv(position)("qty").ToString
        tjumlah.EditValue = dv(position)("jumlah").ToString

        tsatuan.EditValue = satuan
        tharga.EditValue = harga

    End Sub

    Private Sub kalkulasi()

        Dim qty As Integer

        If spulang = False Then
            If jenisjual = "T" Then
                qty = 0
            Else
                qty = tjml.EditValue 'Integer.Parse(dv(position)("qty").ToString)
            End If
        Else
            qty = tjml.EditValue 'Integer.Parse(dv(position)("qty").ToString)
        End If

        Dim hargajual As Double = tharga.EditValue 'Double.Parse(dv(position)("hargajual").ToString)

        Dim satuan1 As String = dv(position)("satuan1").ToString
        Dim satuan2 As String = dv(position)("satuan2").ToString
        Dim satuan3 As String = dv(position)("satuan3").ToString

        Dim vqty1 As Integer = Integer.Parse(dv(position)("qty1").ToString)
        Dim vqty2 As Integer = Integer.Parse(dv(position)("qty2").ToString)
        Dim vqty3 As Integer = Integer.Parse(dv(position)("qty3").ToString)

        Dim jumlah As Double = 0
        Dim hargakecil As Integer = 0

        If hargajual > 0 Then
            hargakecil = hargajual / vqty2
            hargakecil = hargakecil / vqty3
        Else
            hargakecil = 0
        End If

        If satuan.Equals(satuan1) Then
            kqty = (vqty1 * vqty2 * vqty3) * qty
        ElseIf satuan.Equals(satuan2) Then
            kqty = vqty3 * qty
        ElseIf satuan.Equals(satuan3) Then
            kqty = qty
        End If

        jumlah = hargakecil * kqty

        jumlah1 = jumlah
        hargakecil1 = hargakecil

        tjumlah.EditValue = jumlah1

    End Sub

    Private Sub updateview()

        dv(position)("qty") = tjml.EditValue
        dv(position)("jumlah") = jumlah1
        dv(position)("qtykecil") = kqty
        dv(position)("hargajual") = tharga.EditValue
        dv(position)("hargakecil") = hargakecil1

        Me.Close()

    End Sub

    Private Sub ffaktur_to5_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        statload = True

        isidata()

        statload = False

    End Sub

    Private Sub tjml_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tjml.EditValueChanged

        If statload = True Then
            Return
        End If

        kalkulasi()
    End Sub

    Private Sub tharga_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tharga.EditValueChanged

        If statload = True Then
            Return
        End If

        kalkulasi()
    End Sub

    Private Sub ffaktur_to4_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tjml.Focus()
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        'If tjml.EditValue = 0 Then
        '    MsgBox("Jumlah tidak boleh kosong...", vbOKOnly + vbInformation, "Informasi")
        '    tjml.Focus()
        '    Return
        'End If

        'If MsgBox("Yakin sudah benar ?", vbYesNo + vbQuestion, "Konfirmasi") = vbYes Then
        updateview()
        'End If

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub


End Class