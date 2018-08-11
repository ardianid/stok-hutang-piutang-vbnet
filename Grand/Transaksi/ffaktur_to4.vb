Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class ffaktur_to4

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean
    Public jenisjual As String
    Public dvkembali As DataView
    Public position_kembali As Integer
    Public jmlfisik_real As Integer
    Public spulang As Boolean

    Dim qty1, qty2, qty3 As Integer
    Dim kqty As Integer = 0
    Dim sjumlah0 As Double = 0
    Dim totjumlah As Double = 0
    Dim satuan1, satuan2, satuan3 As String
    Dim harga2 As Double = 0
    Dim harga3 As Double = 0
    Dim xharga As Double

    Private Sub kalkulasi()

        xharga = tharga.EditValue
        Dim disc_rp As Double
        Dim xjumlah As Double = tjml.EditValue

        sjumlah0 = xharga * xjumlah

        If xharga > 0 Then

            harga2 = xharga / qty2
            harga3 = harga2 / qty3

            disc_rp = 0

            If tsatuan.Text.Equals(satuan1) Then

                kqty = (qty1 * qty2 * qty3) * xjumlah
                xjumlah = (xharga * xjumlah)

            ElseIf tsatuan.Text.Equals(satuan2) Then

                kqty = (qty3) * xjumlah
                xjumlah = (harga2 * xjumlah) - disc_rp

            ElseIf tsatuan.Text.Equals(satuan3) Then

                kqty = xjumlah
                xjumlah = (harga3 * xjumlah) - disc_rp

            End If

            totjumlah = xjumlah

        Else

            If tsatuan.Text.Equals(satuan1) Then
                kqty = (qty1 * qty2 * qty3) * xjumlah
            ElseIf tsatuan.Text.Equals(satuan2) Then
                kqty = (qty3) * xjumlah
            ElseIf tsatuan.Text.Equals(satuan3) Then
                kqty = xjumlah
            End If

            harga2 = 0
            harga3 = 0

            totjumlah = 0

        End If

        tjumlah.EditValue = totjumlah

    End Sub

    Private Sub cek_non()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn


            Dim sqlc As String = String.Format("select * from ms_barang where jenis='NON FISIK' and kd_barang='{0}'", dv(position)("kd_barang").ToString)
            Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn)
            Dim drdc As OleDbDataReader = cmdc.ExecuteReader

            If drdc.Read Then

                If Not (drdc("kd_barang").ToString.Equals("")) Then



                    qty1 = Integer.Parse(drdc("qty1").ToString)
                    qty2 = Integer.Parse(drdc("qty2").ToString)
                    qty3 = Integer.Parse(drdc("qty3").ToString)

                    satuan1 = drdc("satuan1").ToString
                    satuan2 = drdc("satuan2").ToString
                    satuan3 = drdc("satuan3").ToString

                    tkode.EditValue = drdc("kd_barang").ToString
                    tnama.EditValue = drdc("nama_barang").ToString

                    tjml.EditValue = dv(position)("qty").ToString
                    tsatuan.EditValue = dv(position)("satuan").ToString
                    tharga.EditValue = dv(position)("harga").ToString

                    kalkulasi()


                End If

            End If

            drdc.Close()


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

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select kd_barang_kmb from ms_barang where kd_barang_jmn='{0}'", dv(position)("kd_barang").ToString)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Not (drd(0).ToString.Equals("")) Then

                    Dim sqlc As String = String.Format("select * from ms_barang where jenis='FISIK' and kd_barang='{0}'", drd(0).ToString)
                    Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn)
                    Dim drdc As OleDbDataReader = cmdc.ExecuteReader

                    If drdc.Read Then

                        If Not (drdc("kd_barang").ToString.Equals("")) Then

                            Dim xharga As Double = Double.Parse(dvkembali(position_kembali)("hargajual").ToString)
                            Dim disc_rp As Double
                            Dim xjumlah As Integer = tjml.EditValue 'jmlfisik_real - tjml.EditValue

                            If jenisjual.Equals("T") Then
                                If spulang = False And tjml.EditValue = 0 Then
                                    xjumlah = 0
                                End If
                            End If

                            If xjumlah < 0 Then
                                xjumlah = 0
                            End If

                            Dim qty1, qty2, qty3 As Integer
                            qty1 = Integer.Parse(drdc("qty1").ToString)
                            qty2 = Integer.Parse(drdc("qty2").ToString)
                            qty3 = Integer.Parse(drdc("qty3").ToString)

                            Dim satuan1 As String = drdc("satuan1").ToString
                            Dim satuan2 As String = drdc("satuan2").ToString
                            Dim satuan3 As String = drdc("satuan3").ToString

                            Dim harga2 As Double = 0
                            Dim harga3 As Double = 0

                            Dim sjumlah0 As Double = xharga * xjumlah
                            Dim totjumlah As Double = 0

                            If xharga > 0 Then

                                harga2 = xharga / qty2
                                harga3 = harga2 / qty3

                                disc_rp = 0

                                If tsatuan.EditValue.ToString.Equals(satuan1) Then

                                    kqty = (qty1 * qty2 * qty3) * xjumlah
                                    xjumlah = (xharga * xjumlah)

                                ElseIf tsatuan.EditValue.ToString.Equals(satuan2) Then

                                    kqty = (qty3) * xjumlah
                                    xjumlah = (harga2 * xjumlah) - disc_rp

                                ElseIf tsatuan.EditValue.ToString.Equals(satuan3) Then

                                    kqty = xjumlah
                                    xjumlah = (harga3 * xjumlah) - disc_rp

                                End If

                                totjumlah = xjumlah

                            Else

                                If tsatuan.EditValue.ToString.Equals(satuan1) Then
                                    kqty = (qty1 * qty2 * qty3) * xjumlah
                                ElseIf tsatuan.EditValue.ToString.Equals(satuan2) Then
                                    kqty = (qty3) * xjumlah
                                ElseIf tsatuan.EditValue.ToString.Equals(satuan3) Then
                                    kqty = xjumlah
                                End If

                                harga2 = 0
                                harga3 = 0

                                totjumlah = 0

                            End If

                            

                            ' Dim dvkembali(position_kembali) As DataRowView = dvkembali.AddNew

                            dvkembali(position_kembali)("kd_barang") = drdc("kd_barang").ToString
                            dvkembali(position_kembali)("nama_barang") = drdc("nama_barang").ToString

                            If jenisjual = "T" Then
                                If spulang = True Then
                                    dvkembali(position_kembali)("qty") = tjml.EditValue 'jmlfisik_real - tjml.EditValue
                                Else
                                    dvkembali(position_kembali)("qty") = 0
                                End If
                            Else
                                dvkembali(position_kembali)("qty") = IIf(jmlfisik_real > 0, tjml.EditValue, 0)
                            End If

                            dvkembali(position_kembali)("satuan") = tsatuan.EditValue

                            If jenisjual = "T" Then
                                If spulang = True Then
                                    dvkembali(position_kembali)("jumlah") = totjumlah
                                Else
                                    dvkembali(position_kembali)("jumlah") = 0
                                End If
                            Else
                                dvkembali(position_kembali)("jumlah") = IIf(jmlfisik_real > 0, totjumlah, 0)
                            End If

                            dvkembali(position_kembali)("qtykecil") = IIf(jmlfisik_real > 0, kqty, 0)
                            dvkembali(position_kembali)("hargakecil") = IIf(jmlfisik_real > 0, harga3, 0)
                            dvkembali(position_kembali)("hargajual") = IIf(jmlfisik_real > 0, xharga, 0)

                            dvkembali(position_kembali)("qty1") = qty1
                            dvkembali(position_kembali)("qty2") = qty2
                            dvkembali(position_kembali)("qty3") = qty3

                            dvkembali(position_kembali)("satuan1") = satuan1
                            dvkembali(position_kembali)("satuan2") = satuan2
                            dvkembali(position_kembali)("satuan3") = satuan3

                            'dv.EndInit()

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

        dv(position)("qty") = tjml.EditValue
        dv(position)("jumlah") = totjumlah
        dv(position)("qtykecil") = kqty
        dv(position)("hargakecil") = harga3
        dv(position)("jumlah0") = sjumlah0
        dv(position)("harga") = xharga

        dv(position)("qty1") = qty1
        dv(position)("qty2") = qty2
        dv(position)("qty3") = qty3

        cek_harus_kembali()

        Me.Close()

    End Sub

    Private Sub tjml_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tjml.EditValueChanged
        kalkulasi()
    End Sub

    Private Sub tharga_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tharga.EditValueChanged
        kalkulasi()
    End Sub

    Private Sub ffaktur_to4_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tjml.Focus()
    End Sub

    Private Sub ffaktur_to4_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If jenisjual = "T" Then
            Me.Text = "Faktur TO"
        Else
            Me.Text = "Faktur Kanvas"
        End If

        cek_non()
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