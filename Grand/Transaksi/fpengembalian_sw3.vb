Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpengembalian_sw3

    Public dv As DataView
    Public nobuktisewa As String
    Public statadd As Boolean

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private Sub isi_gudang()

        Const sql As String = "select kd_gudang,nama_gudang from ms_gudang where tipe_gudang='FISIK'"

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

        Dim sql As String = String.Format("SELECT trsewa2.kd_barang, ms_barang.nama_barang, trsewa2.qty, trsewa2.satuan, trsewa2.qtykecil,0 as qtyb," & _
            "ms_barang.qty1,ms_barang.qty2,ms_barang.qty3,ms_barang.satuan1,ms_barang.satuan2,ms_barang.satuan3 " & _
            "FROM trsewa2 INNER JOIN ms_barang ON trsewa2.kd_barang = ms_barang.kd_barang where trsewa2.nobukti='{0}'", nobuktisewa)


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

            cek_jumlsebelumnya()

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

    Private Sub cek_jumlsebelumnya()

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim dta As DataTable = dv.ToTable

        For i As Integer = 0 To dv1.Count - 1


            Dim rows() As DataRow = dta.Select(String.Format("kd_barang='{0}'", dv1(i)("kd_barang").ToString))

            If rows.Length > 0 Then
                dv1(i)("qtyb") = rows(0)("qty").ToString
            End If

        Next

    End Sub

    Private Function kalkulasi(ByVal jmlqt As Integer, ByVal vqty1 As Integer, ByVal vqty2 As Integer, ByVal vqty3 As Integer, _
                          ByVal satuan1 As String, ByVal satuan2 As String, ByVal satuan3 As String, ByVal satuanawal As String) As Integer

        Dim kqty As Integer

        If jmlqt = 0 Then
            kqty = 0
            GoTo akhir
        End If

        Dim jml As String = jmlqt
        Dim jml1 As Integer
        If Not jml.Equals("") Then
            jml1 = Integer.Parse(jml)
        Else
            jml1 = 0
        End If

        Dim xjumlah As Double = jml1

        If satuanawal.Equals(satuan1) Then
            kqty = (vqty1 * vqty2 * vqty3) * jml1
        ElseIf satuanawal.Equals(satuan2) Then
            kqty = (vqty3) * jml1
        ElseIf satuanawal.Equals(satuan3) Then
            kqty = jml1
        End If

akhir:

        Return kqty

    End Function

    Private Sub manipulate()

        Dim hasil As Boolean = True

        Dim dta As DataTable = dv.ToTable

        For i As Integer = 0 To dv1.Count - 1

            If dv1(i)("qtyb") > dv1(i)("qty") Then
                MsgBox(dv1(i)("kd_barang") & " Melebihi jumlah sewa..", vbOKOnly + vbInformation, "Informasi")
                hasil = False
                Exit For
            End If

            Dim rows() As DataRow = dta.Select(String.Format("kd_barang='{0}'", dv1(i)("kd_barang").ToString))
            If rows.Length > 0 Then

                Dim jmlold As Integer = Integer.Parse(rows(0)("qty").ToString)
                If (jmlold > 0) And (dv1(i)("qtyb") = 0) Then

                    If statadd Then
                        For x = 0 To dv.Count - 1
                            If dv(x)("kd_barang").Equals(dv1(i)("kd_barang").ToString) Then
                                dv.Delete(x)
                            End If
                        Next


                    Else

                        MsgBox(dv1(i)("kd_barang").ToString & " Jumlah sebelumnya sudah ada,tidak boleh 0", vbOKOnly + vbInformation, "Informasi")
                        hasil = False
                        Exit For

                    End If

                   

                Else


                    For x = 0 To dv.Count - 1

                        If dv(x)("kd_barang").Equals(dv1(i)("kd_barang").ToString) Then
                            dv(x)("qty") = dv1(i)("qtyb").ToString
                            dv(x)("qtykecil") = kalkulasi(dv1(i)("qtyb").ToString, dv1(i)("qty1").ToString, dv1(i)("qty2").ToString, dv1(i)("qty3").ToString, _
                                                           dv1(i)("satuan1").ToString, dv1(i)("satuan2").ToString, dv1(i)("satuan3").ToString, dv1(i)("satuan").ToString)
                            dv(x)("kd_gudang") = tgudang.EditValue
                        End If

                    Next

                   

                End If


            Else

                If dv1(i)("qtyb") > 0 Then

                    Dim orow As DataRowView = dv.AddNew
                    orow("noid") = 0
                    orow("kd_gudang") = tgudang.EditValue
                    orow("kd_barang") = dv1(i)("kd_barang").ToString
                    orow("nama_barang") = dv1(i)("nama_barang").ToString
                    orow("qty") = dv1(i)("qtyb").ToString
                    orow("satuan") = dv1(i)("satuan").ToString
                    orow("qtykecil") = kalkulasi(dv1(i)("qtyb").ToString, dv1(i)("qty1").ToString, dv1(i)("qty2").ToString, dv1(i)("qty3").ToString, _
                                                       dv1(i)("satuan1").ToString, dv1(i)("satuan2").ToString, dv1(i)("satuan3").ToString, dv1(i)("satuan").ToString)

                    dv.EndInit()

                End If

            End If


        Next

        If hasil = True Then
            Me.Close()
        End If


    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        If tgudang.EditValue = "" Then
            MsgBox("Isi dulu gudang masuk...", vbOKOnly + vbInformation, "Informasi")
            tgudang.Focus()
            Return
        End If

        manipulate()

    End Sub

    Private Sub fpengembalian_sw3_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tgudang.Focus()
    End Sub

    Private Sub fpengembalian_sw3_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        isi_gudang()
        opengrid()

        If Not IsNothing(dv) Then

            If dv.Count > 0 Then
                tgudang.EditValue = dv(0)("kd_gudang").ToString
            End If

        End If

    End Sub


End Class