Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy
Imports System.Text.RegularExpressions
Imports DevExpress.XtraReports.UI

Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid

Public Class ffaktur_to2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Public statbalik As Boolean = False
    Public statsebelumnya As Boolean = False

    Public jenisjual As String
    Public spulang As Boolean
    Public tglkembali As String
    Public crbayar_fak As String

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private dvmanager_spm As Data.DataViewManager
    Private dv_spm As Data.DataView

    Private dvmanager_ret As Data.DataViewManager
    Private dv_ret As Data.DataView


    Dim tgl_old As String
    Private tglkembali_old As String
    Private kdbarang_real As String
    Private dtgudang As DataTable
    Private kdgud_edit As String
    Private nonota_old As String
    Private kdtoko_old As String
    Public statprint As Boolean
    '    Public kdsupir, nopol As String


#Region "siapkan data2 master"

    Private Sub r_datagudang()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = ""

            If jenisjual.Equals("T") Then
                sql = "select kd_gudang,nama_gudang from ms_gudang where tipe_gudang='FISIK'"
            Else
                sql = "select kd_gudang,nama_gudang from ms_gudang where not(tipe_gudang='FISIK')"
            End If

            Dim ds As DataSet = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim orow As DataRow = ds.Tables(0).NewRow
            orow("kd_gudang") = "None"
            orow("nama_gudang") = "None"
            ds.Tables(0).Rows.Add(orow)
            ds.EndInit()

            rgudang.DataSource = ds.Tables(0)
            rgudang_ret.DataSource = ds.Tables(0)

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

    Private Sub r_databarang()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select kd_barang,nama_lap from ms_barang where sjual=1"

            Dim ds As DataSet = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            rnamabarang.DataSource = ds.Tables(0)
            rkdbarang.DataSource = ds.Tables(0)

            rkd_barang_ret.DataSource = ds.Tables(0)
            rnama_brang_ret.DataSource = ds.Tables(0)

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

#End Region

#Region "script - script"

    Private Sub hitung_bonus()

        '' bonus2

        Dim jml02 As Integer = 0
        Dim jml03 As Integer = 0
        Dim jmlqty As Integer = 0


        For i As Integer = 0 To dv1.Count - 1

            If dv1(i)("kd_barang").ToString = "B0002" Then
                jml02 = jml02 + Integer.Parse(dv1(i)("qty").ToString)
                jmlqty = jmlqty + Integer.Parse(dv1(i)("qty").ToString)
            End If

            If dv1(i)("kd_barang").ToString = "B0003" Then
                jml03 = jml03 + Integer.Parse(dv1(i)("qty").ToString)
                jmlqty = jmlqty + Integer.Parse(dv1(i)("qty").ToString)
            End If

        Next

        Dim jbonus As Integer = 0
        If jmlqty >= 500 Then
            jbonus = 10
        ElseIf jmlqty <= 499 And jmlqty >= 200 Then
            jbonus = 12
        ElseIf jmlqty <= 199 And jmlqty >= 100 Then
            jbonus = 15
        ElseIf jmlqty <= 99 And jmlqty >= 50 Then
            jbonus = 18
        ElseIf jmlqty <= 49 And jmlqty >= 20 Then
            jbonus = 20
        End If

        If jbonus > 0 Then

            For ii As Integer = 0 To dv1.Count - 1

                If dv1(ii)("kd_barang").ToString = "BN0002" Then

                    Dim hhitung As Double = jml02 / jbonus

                    If hhitung >= 1 Then
                        hhitung = Math.Floor(hhitung) * 12
                    Else
                        hhitung = (jml02 * 12) / 24
                    End If

                    dv1(ii)("qty") = hhitung
                    dv1(ii)("qty0") = hhitung
                    dv1(ii)("qtykecil") = hhitung
                    dv1(ii)("qtykecil0") = hhitung
                End If

                If dv1(ii)("kd_barang").ToString = "BN0003" Then

                    Dim hhitung As Double = jml03 / jbonus

                    If hhitung >= 1 Then
                        hhitung = Math.Floor(hhitung) * 24
                    Else
                        hhitung = (jml03 * 24) / 24
                    End If

                    dv1(ii)("qty") = hhitung
                    dv1(ii)("qty0") = hhitung
                    dv1(ii)("qtykecil") = hhitung
                    dv1(ii)("qtykecil0") = hhitung

                End If
            Next

        Else

            For ii As Integer = 0 To dv1.Count - 1

                If dv1(ii)("kd_barang").ToString = "BN0002" Then

                    Dim hhitung As Double = (jml02 * 12) / 24
                    hhitung = Math.Floor(hhitung)

                    dv1(ii)("qty") = hhitung
                    dv1(ii)("qty0") = hhitung
                    dv1(ii)("qtykecil") = hhitung
                    dv1(ii)("qtykecil0") = hhitung
                End If

                If dv1(ii)("kd_barang").ToString = "BN0003" Then

                    Dim hhitung As Double = (jml03 * 24) / 24
                    hhitung = Math.Floor(hhitung)

                    dv1(ii)("qty") = hhitung
                    dv1(ii)("qty0") = hhitung
                    dv1(ii)("qtykecil") = hhitung
                    dv1(ii)("qtykecil0") = hhitung

                End If
            Next

        End If



    End Sub

    Private Sub cek_kdbarang(ByVal kdbarang As String, ByVal statkode As Boolean, ByVal nopos As Integer)

        If addstat = False Then
            If statkode = True Then
                If Not dv1(nopos)("kd_barang0").Equals(kdbarang) And dv1(nopos)("kd_barang0").ToString.Length > 0 Then
                    dv1(nopos)("kd_barang") = dv1(nopos)("kd_barang0").ToString
                    Return
                End If
            Else
                If Not dv1(nopos)("nama_lap0").Equals(kdbarang) And dv1(nopos)("nama_lap0").ToString.Length > 0 Then
                    dv1(nopos)("nama_lap") = dv1(nopos)("nama_lap0").ToString
                    Return
                End If
            End If

        End If

        Dim cn As OleDbConnection = Nothing
        Try
            
            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select kd_barang,nama_barang,nama_lap,qty1,qty2,qty3,jenis,kd_barang_jmn,kd_barang_kmb,satuan1,satuan2,satuan3,hargajual from ms_barang"

            If statkode Then
                sql = String.Format("{0} where kd_barang='{1}'", sql, kdbarang)
            Else
                sql = String.Format("{0} where nama_lap='{1}'", sql, kdbarang)
            End If

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            Dim adabarang As Boolean = False
            If drd.Read Then
                If Not drd(0).ToString.Equals("") Then

                    adabarang = True

                    dv1(nopos)("noid") = 0
                    dv1(nopos)("nohrus") = 100
                    dv1(nopos)("nama_barang") = drd("nama_barang").ToString
                    dv1(nopos)("nama_lap0") = drd("nama_lap").ToString
                    dv1(nopos)("nama_lap") = drd("nama_lap").ToString
                    dv1(nopos)("kd_barang0") = drd("kd_barang").ToString
                    dv1(nopos)("kd_barang") = drd("kd_barang").ToString
                    dv1(nopos)("nama_barang") = drd("nama_barang").ToString
                    dv1(nopos)("jenis_trans") = "JUAL"
                    dv1(nopos)("qty1") = drd("qty1").ToString
                    dv1(nopos)("qty2") = drd("qty2").ToString
                    dv1(nopos)("qty3") = drd("qty3").ToString
                    dv1(nopos)("jenis") = drd("jenis").ToString
                    dv1(nopos)("satuan1") = drd("satuan1").ToString
                    dv1(nopos)("satuan2") = drd("satuan2").ToString
                    dv1(nopos)("satuan3") = drd("satuan3").ToString

                    dv1(nopos)("qty0") = 0
                    dv1(nopos)("qty") = 0
                    dv1(nopos)("satuan") = drd("satuan1").ToString
                    dv1(nopos)("harga") = drd("hargajual").ToString
                    dv1(nopos)("disc_per") = 0
                    dv1(nopos)("disc_rp") = 0
                    dv1(nopos)("jumlah") = 0
                    dv1(nopos)("qtykecil0") = 0
                    dv1(nopos)("qtykecil") = 0
                    dv1(nopos)("hargakecil") = 0
                    dv1(nopos)("jumlah0") = 0


                    If dv1(nopos)("kd_gudang").ToString.Trim.Length = 0 Then
                        If jenisjual.Equals("T") Then
                            dv1(nopos)("kd_gudang") = "G000"
                        Else
                            dv1(nopos)("kd_gudang") = cek_gudang_mobil_default()
                        End If
                    End If

                    ' dv1.EndInit()

                    rsatuan.Items.Clear()
                    rsatuan.Items.Add(drd("satuan1").ToString)
                    rsatuan.Items.Add(drd("satuan2").ToString)
                    rsatuan.Items.Add(drd("satuan3").ToString)

                    If drd("kd_barang_jmn").ToString.Trim.Length > 0 Then

                        Dim dtjamin As DataTable = dv1.ToTable
                        Dim hsilrows As DataRow() = dtjamin.Select(String.Format("kd_barang='{0}' and jenis_trans in ('JUAL','PINJAM')", drd("kd_barang_jmn").ToString))

                        If hsilrows.Count > 0 Then
                            GoTo lewati_jaminan
                        End If

                        Dim sqljamin As String = "select kd_barang,nama_barang,nama_lap,qty1,qty2,qty3,jenis,kd_barang_jmn,kd_barang_kmb,satuan1,satuan2,satuan3,hargajual from ms_barang"

                        sqljamin = String.Format("{0}  where kd_barang='{1}'", sqljamin, drd("kd_barang_jmn").ToString)

                        Dim cmdjamin As OleDbCommand = New OleDbCommand(sqljamin, cn)
                        Dim drdjamin As OleDbDataReader = cmdjamin.ExecuteReader

                        If drdjamin.Read Then
                            If Not drdjamin(0).ToString.Equals("") Then

                                Dim orow As DataRowView = dv1.AddNew

                                orow("noid") = 0
                                orow("nohrus") = 100
                                orow("nama_barang") = drdjamin("nama_barang").ToString
                                orow("nama_lap0") = drdjamin("nama_lap").ToString
                                orow("nama_lap") = drdjamin("nama_lap").ToString
                                orow("kd_barang0") = drdjamin("kd_barang").ToString
                                orow("kd_barang") = drdjamin("kd_barang").ToString
                                orow("nama_barang") = drdjamin("nama_barang").ToString
                                orow("jenis_trans") = "JUAL"
                                orow("qty1") = drdjamin("qty1").ToString
                                orow("qty2") = drdjamin("qty2").ToString
                                orow("qty3") = drdjamin("qty3").ToString
                                orow("jenis") = drdjamin("jenis").ToString
                                orow("satuan1") = drdjamin("satuan1").ToString
                                orow("satuan2") = drdjamin("satuan2").ToString
                                orow("satuan3") = drdjamin("satuan3").ToString

                                orow("qty0") = 0
                                orow("qty") = 0
                                orow("satuan") = drdjamin("satuan1").ToString
                                orow("harga") = drdjamin("hargajual").ToString
                                orow("disc_per") = 0
                                orow("disc_rp") = 0
                                orow("jumlah") = 0
                                orow("qtykecil0") = 0
                                orow("qtykecil") = 0
                                orow("hargakecil") = 0
                                orow("jumlah0") = 0

                                If jenisjual.Equals("T") Then
                                    orow("kd_gudang") = "G000"
                                Else
                                    orow("kd_gudang") = cek_gudang_mobil_default()
                                End If

                                dv1.EndInit()

                            End If
                        End If
                        drdjamin.Close()

                    End If

lewati_jaminan:

                    If Not jenisjual.Equals("T") Then

                        If drd("kd_barang_kmb").ToString.Trim.Length > 0 Then

                            Dim dtkembali As DataTable = dv1.ToTable
                            Dim hsilrows As DataRow() = dtkembali.Select(String.Format("kd_barang='{0}' and jenis_trans in ('JUAL','PINJAM')", drd("kd_barang_kmb").ToString))

                            If hsilrows.Count > 0 Then
                                GoTo lewati_kembali
                            End If

                            Dim sqlkemb As String = "select kd_barang,nama_barang,nama_lap,qty1,qty2,qty3,jenis,kd_barang_jmn,kd_barang_kmb,satuan1,satuan2,satuan3,hargajual from ms_barang"


                            sqlkemb = String.Format("{0}  where kd_barang='{1}'", sqlkemb, drd("kd_barang_kmb").ToString)


                            Dim cmdkemb As OleDbCommand = New OleDbCommand(sqlkemb, cn)
                            Dim drdkemb As OleDbDataReader = cmdkemb.ExecuteReader

                            If drdkemb.Read Then
                                If Not drdkemb(0).ToString.Equals("") Then

                                    Dim orow As DataRowView = dv1.AddNew

                                    orow("noid") = 0
                                    orow("nohrus") = 100
                                    orow("nama_barang") = drdkemb("nama_barang").ToString
                                    orow("nama_lap0") = drdkemb("nama_lap").ToString
                                    orow("nama_lap") = drdkemb("nama_lap").ToString
                                    orow("kd_barang0") = drdkemb("kd_barang").ToString
                                    orow("kd_barang") = drdkemb("kd_barang").ToString
                                    orow("nama_barang") = drdkemb("nama_barang").ToString
                                    orow("jenis_trans") = "KEMBALI"
                                    orow("qty1") = drdkemb("qty1").ToString
                                    orow("qty2") = drdkemb("qty2").ToString
                                    orow("qty3") = drdkemb("qty3").ToString
                                    orow("jenis") = drdkemb("jenis").ToString
                                    orow("satuan1") = drdkemb("satuan1").ToString
                                    orow("satuan2") = drdkemb("satuan2").ToString
                                    orow("satuan3") = drdkemb("satuan3").ToString

                                    orow("qty0") = 0
                                    orow("qty") = 0
                                    orow("satuan") = drdkemb("satuan1").ToString
                                    orow("harga") = drdkemb("hargajual").ToString
                                    orow("disc_per") = 0
                                    orow("disc_rp") = 0
                                    orow("jumlah") = 0
                                    orow("qtykecil0") = 0
                                    orow("qtykecil") = 0
                                    orow("hargakecil") = 0
                                    orow("jumlah0") = 0

                                    'If jenisjual.Equals("T") Then
                                    orow("kd_gudang") = "None"
                                    'Else
                                    '   orow("kd_gudang") = cek_gudang_mobil_default()
                                    'End If

                                    dv1.EndInit()

                                End If
                            End If
                            drdkemb.Close()

                        End If

                    End If

lewati_kembali:

                End If
            End If
            drd.Close()

            If adabarang = False Then
                dv1(nopos).Delete()
            End If


            '' untuk bonus

            If DateValue(ttgl.EditValue) >= DateValue("04/05/2015") Then

                Dim bonusadd As String = ""
                If statkode Then
                    If kdbarang = "B0002" Then
                        bonusadd = "1500"
                    ElseIf kdbarang = "B0003" Then
                        bonusadd = "600"
                    End If
                Else
                    If kdbarang = "1500ML/12" Then
                        bonusadd = "1500"
                    ElseIf kdbarang = "600ML/24" Then
                        bonusadd = "600"
                    End If
                End If

                If bonusadd.Length > 0 Then


                    Dim kdbonus As String = ""
                    If bonusadd = "1500" Then
                        kdbonus = "BN0002"
                    Else
                        kdbonus = "BN0003"
                    End If

                    Dim dtjamin2 As DataTable = dv1.ToTable
                    Dim hsilrows2 As DataRow() = dtjamin2.Select(String.Format("kd_barang='{0}' and jenis_trans in ('JUAL','PINJAM')", kdbonus))

                    If hsilrows2.Count > 0 Then
                        GoTo langsung_keluar2
                    End If

                    Dim sqltoko_bns As String = String.Format("select spromo from ms_toko where kd_toko='{0}'", tkd_toko.EditValue)
                    Dim cmdtoko_bns As OleDbCommand = New OleDbCommand(sqltoko_bns, cn)
                    Dim drdtoko_bns As OleDbDataReader = cmdtoko_bns.ExecuteReader

                    If drdtoko_bns.Read Then
                        If IsNumeric(drdtoko_bns(0).ToString) Then
                            If Integer.Parse(drdtoko_bns(0).ToString) = 0 Then
                                GoTo langsung_keluar2
                            End If
                        End If
                    Else
                        GoTo langsung_keluar2
                    End If

                    Dim namalap_bonus As String
                    Dim namalap_brg As String
                    If bonusadd = "1500" Then
                        namalap_bonus = "1500ML/12 (BONUS)"
                        namalap_brg = "GRAND 1500 ML/12 BOTOL (BONUS)"
                    Else
                        namalap_bonus = "600ML/24 (BONUS)"
                        namalap_brg = "GRAND 600 ML/24 BOTOL (BONUS)"
                    End If

                    Dim orow As DataRowView = dv1.AddNew

                    orow("noid") = 0
                    orow("nohrus") = 100
                    orow("nama_barang") = namalap_brg
                    orow("nama_lap0") = namalap_bonus
                    orow("nama_lap") = namalap_bonus
                    orow("kd_barang0") = kdbonus
                    orow("kd_barang") = kdbonus
                    orow("nama_barang") = namalap_brg
                    orow("jenis_trans") = "JUAL"
                    orow("qty1") = 0
                    orow("qty2") = 0
                    orow("qty3") = 0
                    orow("jenis") = "FISIK"
                    orow("satuan1") = "BTL"
                    orow("satuan2") = "BTL"
                    orow("satuan3") = "-"

                    orow("qty0") = 0
                    orow("qty") = 0
                    orow("satuan") = "BTL"
                    orow("harga") = 0
                    orow("disc_per") = 0
                    orow("disc_rp") = 0
                    orow("jumlah") = 0
                    orow("qtykecil0") = 0
                    orow("qtykecil") = 0
                    orow("hargakecil") = 0
                    orow("jumlah0") = 0
                    orow("kd_gudang") = "G000"

                    dv1.EndInit()

                End If
            End If

langsung_keluar2:

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

    Private Sub cek_kdbarang_retur(ByVal barang As String, ByVal statkode As Boolean, ByVal nopos As String, ByVal qty As Integer)

        If nopos > -1 Then
            If addstat = False Then
                If statkode = True Then
                    If Not dv_ret(nopos)("kd_barang0").Equals(barang) And dv_ret(nopos)("kd_barang0").ToString.Length > 0 Then
                        dv_ret(nopos)("kd_barang") = dv_ret(nopos)("kd_barang0").ToString
                        Return
                    End If
                Else
                    If Not dv_ret(nopos)("nama_lap0").Equals(barang) And dv_ret(nopos)("nama_lap0").ToString.Length > 0 Then
                        dv_ret(nopos)("nama_lap") = dv_ret(nopos)("nama_lap0").ToString
                        Return
                    End If
                End If

            End If
        End If
        

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select kd_barang,nama_barang,nama_lap,qty1,qty2,qty3,satuan1,satuan2,satuan3,hargajual from ms_barang"

            If statkode Then
                sql = String.Format("{0} where kd_barang='{1}'", sql, barang)
            Else
                sql = String.Format("{0} where nama_lap='{1}'", sql, barang)
            End If

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            Dim apaada As Boolean = False

            If drd.Read Then
                If Not drd(0).ToString.Trim.Equals("") Then

                    apaada = True

                    If nopos > -1 Then

                        dv_ret(nopos)("noid") = 0
                        dv_ret(nopos)("kd_barang0") = drd("kd_barang").ToString
                        dv_ret(nopos)("kd_barang") = drd("kd_barang").ToString
                        dv_ret(nopos)("nama_barang") = drd("nama_barang").ToString
                        dv_ret(nopos)("nama_lap0") = drd("nama_lap").ToString
                        dv_ret(nopos)("nama_lap") = drd("nama_lap").ToString
                        dv_ret(nopos)("qty") = qty
                        dv_ret(nopos)("satuan") = drd("satuan1").ToString
                        dv_ret(nopos)("harga") = drd("hargajual").ToString
                        dv_ret(nopos)("disc_per") = 0
                        dv_ret(nopos)("disc_rp") = 0
                        dv_ret(nopos)("jumlah") = 0
                        dv_ret(nopos)("qtykecil") = 0
                        dv_ret(nopos)("qty1") = drd("qty1").ToString
                        dv_ret(nopos)("qty2") = drd("qty2").ToString
                        dv_ret(nopos)("qty3") = drd("qty3").ToString
                        dv_ret(nopos)("satuan1") = drd("satuan1").ToString
                        dv_ret(nopos)("satuan2") = drd("satuan2").ToString
                        dv_ret(nopos)("satuan3") = drd("satuan3").ToString

                        rsatuan_ret.Items.Clear()
                        rsatuan_ret.Items.Add(drd("satuan1").ToString)
                        rsatuan_ret.Items.Add(drd("satuan2").ToString)
                        rsatuan_ret.Items.Add(drd("satuan3").ToString)

                        If dv_ret(nopos)("kd_gudang").ToString.Trim.Length = 0 Then
                            If jenisjual.Equals("T") Then
                                dv_ret(nopos)("kd_gudang") = "G000"
                            Else
                                dv_ret(nopos)("kd_gudang") = cek_gudang_mobil_default()
                            End If
                        End If

                    Else

                        Dim orow As DataRowView = dv_ret.AddNew

                        If Not IsNumeric(orow("noid")) Then
                            orow("noid") = 0
                        End If

                        orow("kd_barang0") = drd("kd_barang").ToString
                        orow("kd_barang") = drd("kd_barang").ToString
                        orow("nama_barang") = drd("nama_barang").ToString
                        orow("nama_lap0") = drd("nama_lap").ToString
                        orow("nama_lap") = drd("nama_lap").ToString
                        orow("qty") = qty
                        orow("satuan") = drd("satuan1").ToString
                        orow("harga") = drd("hargajual").ToString
                        orow("disc_per") = 0
                        orow("disc_rp") = 0
                        orow("jumlah") = 0
                        orow("qtykecil") = 0
                        orow("qty1") = drd("qty1").ToString
                        orow("qty2") = drd("qty2").ToString
                        orow("qty3") = drd("qty3").ToString
                        orow("satuan1") = drd("satuan1").ToString
                        orow("satuan2") = drd("satuan2").ToString
                        orow("satuan3") = drd("satuan3").ToString

                        'If dv1(nopos)("kd_gudang").ToString.Trim.Length = 0 Then
                        If jenisjual.Equals("T") Then
                            orow("kd_gudang") = "G000"
                        Else
                            orow("kd_gudang") = cek_gudang_mobil_default()
                        End If
                        'End If

                        dv_ret.EndInit()

                    End If

                    

                End If
            End If
            drd.Close()

            If apaada = False Then
                dv_ret(nopos).Delete()
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

    Private Sub cek_klebihan_retur()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim cmd As OleDbCommand = New OleDbCommand("select kd_barang_jmn,kd_barang_kmb,qty1,qty2,qty3,satuan1,satuan2,satuan3 from ms_barang where len(kd_barang_kmb) > 0", cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Not drd(0).ToString.Equals("") Then

                    Dim dtjual As DataTable = dv1.ToTable
                    Dim orowjual() As DataRow = dtjual.Select(String.Format("kd_barang='{0}' and (jenis_trans='JUAL' or jenis_trans='PINJAM')", drd(0).ToString))
                    Dim orowkembali() As DataRow = dtjual.Select(String.Format("kd_barang='{0}' and jenis_trans='KEMBALI'", drd(1).ToString))

                    Dim jmljual As Integer = 0
                    Dim jmlkembali As Integer = 0

                    If orowjual.Length > 0 Then

                        For i As Integer = 0 To orowjual.Length - 1
                            jmljual = jmljual + orowjual(i)("qtykecil").ToString
                        Next

                    End If

                    If orowkembali.Length > 0 Then
                        jmlkembali = orowkembali(0)("qtykecil").ToString
                    End If

                    Dim qty1 As Integer = drd("qty1").ToString
                    Dim qty2 As Integer = drd("qty2").ToString
                    Dim qty3 As Integer = drd("qty3").ToString
                    Dim satuan1 As String = drd("satuan1").ToString
                    Dim satuan2 As String = drd("satuan2").ToString
                    Dim satuan3 As String = drd("satuan3").ToString

                    Dim jmlselisih As Integer = jmlkembali - jmljual

                    If jmlselisih > 0 Then

                        Dim barisk As Integer = -1
                        For i As Integer = 0 To dv_ret.Count - 1
                            If dv_ret(i)("kd_barang").ToString.Equals(orowkembali(0)("kd_barang").ToString) Then
                                barisk = i
                                Exit For
                            End If
                        Next

                        Dim qty_ret As Integer = jmlselisih / (qty1 * qty2 * qty3)

                        dv1(Me.BindingContext(dv1).Position)("qty") = jmljual
                        kalkulasi(Me.BindingContext(dv1).Position)

                        If barisk > -1 Then



                            dv_ret(barisk)("qty") = qty_ret
                            kalkulasi_ret(barisk)
                        Else

                            cek_kdbarang_retur(orowkembali(0)("kd_barang").ToString, True, -1, qty_ret)
                            kalkulasi_ret(0)

                        End If

                    End If

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

    Private Sub kalkulasi(ByVal nopos As Integer)

        If Integer.Parse(dv1(nopos)("qty").ToString) = 0 Then

            dv1(nopos)("disc_per") = 0
            dv1(nopos)("disc_rp") = 0
            dv1(nopos)("jumlah") = 0
            dv1(nopos)("hargakecil") = 0

            dv1(nopos)("qtykecil") = 0

            If jenisjual.Equals("T") Then
                If Not spulang = True Then
                    dv1(nopos)("qtykecil0") = 0
                End If
            Else
                dv1(nopos)("qtykecil0") = 0
            End If

            Return
        End If

        Dim jml As String = dv1(nopos)("qty").ToString
        Dim jml1 As Integer
        If Not jml.Equals("") Then
            jml1 = Integer.Parse(jml)
        Else
            jml1 = 0
        End If

        Dim xharga As Double = Double.Parse(dv1(nopos)("harga").ToString)
        Dim disc_rp As Double
        Dim xjumlah As Double = jml1

        Dim vharga2, vharga3 As Double
        Dim vqty1 As Integer = Integer.Parse(dv1(nopos)("qty1").ToString)
        Dim vqty2 As Integer = Integer.Parse(dv1(nopos)("qty2").ToString)
        Dim vqty3 As Integer = Integer.Parse(dv1(nopos)("qty3").ToString)

        Dim kqty As Integer = 0

        dv1(nopos)("jumlah0") = xharga * xjumlah

        If xharga > 0 Then

            vharga2 = xharga / vqty2
            vharga3 = vharga2 / vqty3

            disc_rp = dv1(nopos)("disc_rp").ToString

            If dv1(nopos)("satuan").ToString.Equals(dv1(nopos)("satuan1").ToString) Then
                xjumlah = (xharga * xjumlah) - disc_rp
                kqty = (vqty1 * vqty2 * vqty3) * jml1
            ElseIf dv1(nopos)("satuan").ToString.Equals(dv1(nopos)("satuan2").ToString) Then
                xjumlah = (vharga2 * xjumlah) - disc_rp
                kqty = (vqty3) * jml1
            ElseIf dv1(nopos)("satuan").ToString.Equals(dv1(nopos)("satuan3").ToString) Then
                xjumlah = (vharga3 * xjumlah) - disc_rp
                kqty = jml1
            End If

            dv1(nopos)("jumlah") = xjumlah

        Else

            If dv1(nopos)("satuan").ToString.Equals(dv1(nopos)("satuan1").ToString) Then
                kqty = (vqty1 * vqty2 * vqty3) * jml1
            ElseIf dv1(nopos)("satuan").ToString.Equals(dv1(nopos)("satuan2").ToString) Then
                kqty = (vqty3) * jml1
            ElseIf dv1(nopos)("satuan").ToString.Equals(dv1(nopos)("satuan3").ToString) Then
                kqty = jml1
            End If

            vharga2 = 0
            vharga3 = 0

            dv1(nopos)("jumlah") = 0

        End If

        dv1(nopos)("hargakecil") = vharga3
        dv1(nopos)("qtykecil") = kqty

        If jenisjual.Equals("T") Then
            If Not spulang = True Then
                dv1(nopos)("qty0") = jml
                dv1(nopos)("qtykecil0") = kqty
            End If
        Else
            dv1(nopos)("qty0") = jml
            dv1(nopos)("qtykecil0") = kqty
        End If

    End Sub

    Private Sub kalkulasi_ret(ByVal nopos As Integer)

        If Integer.Parse(dv_ret(nopos)("qty").ToString) = 0 Then
            Return
        End If

        Dim jml As String = Integer.Parse(dv_ret(nopos)("qty").ToString)
        Dim jml1 As Integer
        If Not jml.Equals("") Then
            jml1 = Integer.Parse(jml)
        Else
            jml1 = 0
        End If

        Dim xharga As Double = Double.Parse(dv_ret(nopos)("harga").ToString)
        Dim disc_rp As Double
        Dim xjumlah As Double = jml1

        Dim vharga2, vharga3 As Double
        Dim vqty1 As Integer = Integer.Parse(dv_ret(nopos)("qty1").ToString)
        Dim vqty2 As Integer = Integer.Parse(dv_ret(nopos)("qty2").ToString)
        Dim vqty3 As Integer = Integer.Parse(dv_ret(nopos)("qty3").ToString)

        Dim kqty As Integer = 0

        If xharga > 0 Then

            vharga2 = xharga / vqty2
            vharga3 = vharga2 / vqty3

            disc_rp = dv_ret(nopos)("disc_rp").ToString

            If dv_ret(nopos)("satuan").ToString.Equals(dv_ret(nopos)("satuan1").ToString) Then
                xjumlah = (xharga * xjumlah) - disc_rp
                kqty = (vqty1 * vqty2 * vqty3) * jml1
            ElseIf dv_ret(nopos)("satuan").ToString.Equals(dv_ret(nopos)("satuan2").ToString) Then
                xjumlah = (vharga2 * xjumlah) - disc_rp
                kqty = (vqty3) * jml1
            ElseIf dv_ret(nopos)("satuan").ToString.Equals(dv_ret(nopos)("satuan3").ToString) Then
                xjumlah = (vharga3 * xjumlah) - disc_rp
                kqty = jml1
            End If

            dv_ret(nopos)("jumlah") = xjumlah

        Else

            If dv_ret(nopos)("satuan").ToString.Equals(dv_ret(nopos)("satuan1").ToString) Then
                'xjumlah = (xharga * xjumlah) - disc_rp
                kqty = (vqty1 * vqty2 * vqty3) * jml1
            ElseIf dv_ret(nopos)("satuan").ToString.Equals(dv_ret(nopos)("satuan2").ToString) Then
                'xjumlah = (vharga2 * xjumlah) - disc_rp
                kqty = (vqty3) * jml1
            ElseIf dv_ret(nopos)("satuan").ToString.Equals(dv_ret(nopos)("satuan3").ToString) Then
                'xjumlah = (vharga3 * xjumlah) - disc_rp
                kqty = jml1
            End If

            vharga2 = 0
            vharga3 = 0

            dv_ret(nopos)("jumlah") = 0

        End If

        dv_ret(nopos)("qtykecil") = kqty


    End Sub

    Private Sub hitung_disc_rupiah(ByVal nopos As String)

        If Double.Parse(dv1(nopos)("disc_per").ToString) > 0 Then

            Dim jml As String = dv1(nopos)("qty").ToString
            Dim jml1 As Integer
            If Not jml.Equals("") Then
                jml1 = Integer.Parse(jml)
            Else
                jml1 = 0
            End If

            Dim xharga As Double = Double.Parse(dv1(nopos)("harga").ToString)
            Dim vharga2, vharga3 As Double
            Dim disc_rp As Double
            Dim xjumlah As Double = jml1

            Dim vqty1 As Integer = Integer.Parse(dv1(nopos)("qty1").ToString)
            Dim vqty2 As Integer = Integer.Parse(dv1(nopos)("qty2").ToString)
            Dim vqty3 As Integer = Integer.Parse(dv1(nopos)("qty3").ToString)

            If xharga > 0 Then

                vharga2 = xharga / vqty2
                vharga3 = vharga2 / vqty3

                If dv1(nopos)("satuan").ToString.Equals(dv1(nopos)("satuan1").ToString) Then
                    xjumlah = (xharga * xjumlah)
                ElseIf dv1(nopos)("satuan").ToString.Equals(dv1(nopos)("satuan2").ToString) Then
                    xjumlah = (xharga * vharga2)
                ElseIf dv1(nopos)("satuan").ToString.Equals(dv1(nopos)("satuan3").ToString) Then
                    xjumlah = (xharga * vharga3)
                End If

                disc_rp = xjumlah * (Double.Parse(dv1(nopos)("disc_per").ToString) / 100)

                dv1(nopos)("disc_rp") = disc_rp

            Else
                dv1(nopos)("disc_rp") = 0
            End If

        Else
            dv1(nopos)("disc_rp") = 0
        End If

    End Sub

    Private Sub hitung_disc_persen(ByVal nopos As String)

        If Double.Parse(dv1(nopos)("disc_rp").ToString) > 0 Then

            Dim jml As String = dv1(nopos)("qty").ToString
            Dim jml1 As Integer
            If Not jml.Equals("") Then
                jml1 = Integer.Parse(jml)
            Else
                jml1 = 0
            End If

            Dim xharga As Double = Double.Parse(dv1(nopos)("harga").ToString)
            Dim vharga2, vharga3 As Double
            Dim disc_rp As Double
            Dim xjumlah As Double = jml1

            Dim vqty1 As Integer = Integer.Parse(dv1(nopos)("qty1").ToString)
            Dim vqty2 As Integer = Integer.Parse(dv1(nopos)("qty2").ToString)
            Dim vqty3 As Integer = Integer.Parse(dv1(nopos)("qty3").ToString)

            If xharga > 0 Then

                vharga2 = xharga / vqty2
                vharga3 = vharga2 / vqty3

                If dv1(nopos)("satuan").ToString.Equals(dv1(nopos)("satuan1").ToString) Then
                    xjumlah = (xharga * xjumlah)
                ElseIf dv1(nopos)("satuan").ToString.Equals(dv1(nopos)("satuan2").ToString) Then
                    xjumlah = (xharga * vharga2)
                ElseIf dv1(nopos)("satuan").ToString.Equals(dv1(nopos)("satuan3").ToString) Then
                    xjumlah = (xharga * vharga3)
                End If

                disc_rp = (Double.Parse(dv1(nopos)("disc_rp").ToString) / xjumlah) * 100

                dv1(nopos)("disc_per") = disc_rp

            Else

                dv1(nopos)("disc_per") = 0.0

            End If

        Else
            dv1(nopos)("disc_per") = 0.0
        End If

    End Sub

    Private Function cek_gudang_mobil_default() As String

        Dim hasil As String = ""
        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            '   Dim sql As String = String.Format("select nopol from ms_supirkenek where nopol in (select nopol from ms_gudang) and kd_sales='{0}'", kdsales)
            Dim sql As String = String.Format("select kd_gudang from trspm where kd_sales='{0}' and tglberangkat='{1}'", tkd_sales.Text.Trim, convert_date_to_eng(ttgl.EditValue))
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Not drd(0).ToString.Equals("") Then
                    hasil = drd(0).ToString
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

        Return hasil

    End Function

    Private Sub cekhapus_non(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal kode As String, ByVal nobukti As String)

        Dim satuan As String = dv1(Me.BindingContext(dv1).Position)("satuan").ToString

        Dim sql As String

        If dv1(Me.BindingContext(dv1).Position)("jenis").ToString.Equals("FISIK") Then
            sql = String.Format("select kd_barang_jmn from ms_barang where kd_barang='{0}'", kode)
        Else
            sql = String.Format("select kd_barang_jmn from ms_barang where kd_barang_jmn='{0}'", kode)
        End If

        Dim cmd As OleDbCommand

        If IsNothing(cn) Then

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            cmd = New OleDbCommand(sql, cn)

        Else
            cmd = New OleDbCommand(sql, cn, sqltrans)
        End If


        Dim drd As OleDbDataReader = cmd.ExecuteReader

        If drd.Read Then
            If Not (drd(0).ToString.Equals("")) Then

                Dim kdbarang As String = drd("kd_barang_jmn").ToString

                Dim sqldel As String = String.Format("delete from trfaktur_to2 where nobukti='{0}' and kd_barang='{1}' and satuan='{2}'", nobukti, kdbarang, satuan)
                Using cmddel As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
                    cmddel.ExecuteNonQuery()
                End Using

                For i As Integer = 0 To dv1.Count - 1
                    If dv1(i)("kd_barang").ToString.Equals(kdbarang) And dv1(i)("satuan").ToString.Equals(satuan) Then
                        dv1.Delete(i)
                        Exit For
                    End If
                Next

            End If
        End If

        drd.Close()

        If IsNothing(sqltrans) Then

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End If

    End Sub

    Private Function cekhapus_kembali(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal kode As String, ByVal nobukti As String) As String

        Dim hasil As String = ""
        Dim satuan As String = dv1(Me.BindingContext(dv1).Position)("satuan").ToString

        Dim sql As String

        If dv1(Me.BindingContext(dv1).Position)("jenis").ToString.Equals("FISIK") Then
            sql = String.Format("select kd_barang_kmb from ms_barang where kd_barang='{0}'", kode)
        Else
            sql = String.Format("select kd_barang_kmb from ms_barang where kd_barang_jmn='{0}'", kode)
        End If

        Dim cmd As OleDbCommand

        If IsNothing(cn) Then

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            cmd = New OleDbCommand(sql, cn)

        Else
            cmd = New OleDbCommand(sql, cn, sqltrans)
        End If

        ' Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        If drd.Read Then
            If Not (drd(0).ToString.Equals("")) Then

                Dim kdbarang As String = drd("kd_barang_kmb").ToString
                Dim kdgud As String

                For i As Integer = 0 To dv1.Count - 1
                    If dv1(i)("kd_barang").ToString.Equals(kdbarang) And dv1(i)("satuan").ToString.Equals(satuan) And dv1(i)("jenis_trans").ToString.Equals("KEMBALI") Then

                        Dim qtykecil As Integer = Integer.Parse(dv1(i)("qtykecil").ToString)
                        Dim kdbar As String = dv1(i)("kd_barang").ToString
                        kdgud = dv1(i)("kd_gudang").ToString

                        If kdgud.Equals("None") Then
                            GoTo langsung_delete
                        End If

                        '2. update barang
                        'Dim hasilplusmin2 As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, True, False, False)
                        'If Not hasilplusmin2.Equals("ok") Then
                        '    close_wait()

                        '    If Not IsNothing(sqltrans) Then
                        '        sqltrans.Rollback()
                        '    End If

                        '    MsgBox(hasilplusmin2, vbOKOnly + vbExclamation, "Informasi")
                        '    hasil = "error"
                        '    Exit For
                        'End If

                        'If Not tglkembali_old = "" Then
                        '    If DateValue(tgl_old) <> DateValue(ttgl.EditValue) Then

                        '        If jenisjual.Equals("T") Then
                        '            Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tglkembali_old, kdgud, kdbar, 0, qtykecil, "Jual To (Realisasi)", "-", "BE XXXX XX")
                        '        Else
                        '            Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tglkembali_old, kdgud, kdbar, 0, qtykecil, "Jual To (Realisasi)", dv_spm(0)("kd_supir").ToString, dv_spm(0)("nopol").ToString)
                        '        End If


                        '    End If
                        'End If

                        '3. insert to hist stok
                        'If jenisjual.Equals("T") Then
                        '    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_kembali.EditValue, kdgud, kdbar, qtykecil, 0, "Jual To (Realisasi)", "-", "BE XXXX XX")
                        'Else
                        '    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_kembali.EditValue, kdgud, kdbar, qtykecil, 0, "Jual To (Realisasi)", dv_spm(0)("kd_supir").ToString, dv_spm(0)("nopol").ToString)
                        'End If



langsung_delete:

                        dv1.Delete(i)
                        Exit For
                    End If
                Next

                Dim sqldel As String = String.Format("delete from trfaktur_to3 where nobukti='{0}' and kd_barang='{1}' and satuan='{2}'", nobukti, kdbarang, satuan)
                Using cmddel As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
                    cmddel.ExecuteNonQuery()
                End Using

            End If
        End If

        drd.Close()

        If IsNothing(sqltrans) Then

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End If

        If hasil = "" Then
            hasil = "ok"
        End If

        Return hasil

    End Function

    Private Sub kosongkan()

        tbukti.Text = "<< New >>"

        tnota.Text = ""
        tkd_sales.Text = ""
        tnama_sales.Text = ""

        tkd_toko.Text = ""
        tnama_toko.Text = ""
        talamat_toko.Text = ""

        tlimit.EditValue = 0
        tpiutang.EditValue = 0
        tsisalimit.EditValue = 0

        tnote.Text = ""

        tdisc_per.EditValue = 0.0
        tdisc_rp.EditValue = 0
        tnetto.EditValue = 0

        tlimit.EditValue = 0
        tpiutang.EditValue = 0
        tsisalimit.EditValue = 0

        opengrid()
        'opengrid2()
        opengrid_retur()

        isi_nospm()

        ttot_retur.EditValue = 0
        ttot_kembali.EditValue = 0
        tbrutto.EditValue = 0
        tongkos.EditValue = 0
        tlebih.EditValue = 0

        tbayar.EditValue = 0
        tgiro_gan.EditValue = 0
        tgiro_ca.EditValue = 0
        tlebih_pot.EditValue = 0
        ttot_bay.EditValue = 0
        tsisa_piut.EditValue = 0

        ttop.EditValue = 0

    End Sub

    Private Sub isi()

        Dim nobukti As String = dv(position)("nobukti").ToString
        Dim sql As String = String.Format("SELECT     trfaktur_to.nobukti, trfaktur_to.tanggal, trfaktur_to.tgl_tempo,trfaktur_to.tgl_kembali, trfaktur_to.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, trfaktur_to.kd_karyawan," & _
            "ms_pegawai.nama_karyawan, trfaktur_to.disc_per, trfaktur_to.disc_rp, trfaktur_to.brutto,trfaktur_to.jmlkembali,trfaktur_to.jmlretur, trfaktur_to.netto, trfaktur_to.ket, trfaktur_to.alasan_batal,trfaktur_to.jnis_jual,trfaktur_to.jnis_jual2,trfaktur_to.no_nota,trfaktur_to.ongkos_angkut," & _
            "trfaktur_to.jmlkelebihan,trfaktur_to.jmlbayar,trfaktur_to.jmlgiro,trfaktur_to.jmlgiro_real,trfaktur_to.jmlkelebihan_pot,trfaktur_to.top_toko " & _
            "FROM         trfaktur_to INNER JOIN " & _
            "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko INNER JOIN " & _
            "ms_pegawai ON trfaktur_to.kd_karyawan = ms_pegawai.kd_karyawan where trfaktur_to.nobukti='{0}'", nobukti)

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
                        tnota.EditValue = dread("no_nota").ToString

                        nonota_old = tnota.EditValue.ToString.Trim

                        ttgl.EditValue = DateValue(dread("tanggal").ToString)

                        tgl_old = ttgl.EditValue

                        ttgl_tempo.EditValue = DateValue(dread("tgl_tempo").ToString)


                        If IsDate(dread("tgl_kembali").ToString) Then

                            If tglkembali = "" Then
                                ttgl_kembali.EditValue = DateValue(dread("tgl_kembali").ToString)
                                tglkembali_old = ttgl_kembali.EditValue
                            Else

                                ttgl_kembali.EditValue = DateValue(tglkembali)
                                tglkembali_old = ttgl_kembali.EditValue

                            End If


                        End If

                        tkd_toko.EditValue = dread("kd_toko").ToString

                        kdtoko_old = dread("kd_toko").ToString

                        tnama_toko.EditValue = dread("nama_toko").ToString
                        talamat_toko.EditValue = dread("alamat_toko").ToString

                        tkd_sales.EditValue = dread("kd_karyawan").ToString
                        tnama_sales.EditValue = dread("nama_karyawan").ToString

                        If jenisjual.Equals("T") Then
                            If spulang = True Then
                                cbjenis.EditValue = crbayar_fak
                            Else
                                cbjenis.EditValue = dread("jnis_jual").ToString
                            End If
                        Else
                            cbjenis.EditValue = dread("jnis_jual").ToString
                        End If


                        tnote.EditValue = dread("ket").ToString

                        ceklimitby(cn)

                        If tlimit.EditValue > 0 Then

                            If tpiutang.EditValue > 0 Then
                                tpiutang.EditValue = tpiutang.EditValue - dread("netto").ToString
                            End If

                            tsisalimit.EditValue = tsisalimit.EditValue + dread("netto").ToString

                        End If

                        opengrid()

                        'If spulang = True Then
                        'opengrid2()
                        'End If

                        opengrid_retur()

                        isi_nospm()

                        tbrutto.EditValue = Double.Parse(dread("brutto").ToString)

                        ttot_kembali.EditValue = Double.Parse(dread("jmlkembali").ToString)
                        ttot_retur.EditValue = Double.Parse(dread("jmlretur").ToString)

                        tdisc_per.EditValue = dread("disc_per").ToString
                        tdisc_rp.EditValue = Double.Parse(dread("disc_rp").ToString)

                        tongkos.EditValue = Double.Parse(dread("ongkos_angkut").ToString)

                        tnetto.EditValue = Double.Parse(dread("netto").ToString)

                        tlebih.EditValue = Double.Parse(dread("jmlkelebihan").ToString)
                        tbayar.EditValue = Double.Parse(dread("jmlbayar").ToString)
                        tgiro_gan.EditValue = Double.Parse(dread("jmlgiro").ToString)
                        tgiro_ca.EditValue = Double.Parse(dread("jmlgiro_real").ToString)
                        tlebih_pot.EditValue = Double.Parse(dread("jmlkelebihan_pot").ToString)

                        ttop.EditValue = Integer.Parse(dread("top_toko").ToString)

                        ttot_bay.EditValue = Double.Parse(tbayar.EditValue) + Double.Parse(tgiro_ca.EditValue) + Double.Parse(tlebih_pot.EditValue)
                        tsisa_piut.EditValue = (Double.Parse(tnetto.EditValue) + Double.Parse(tlebih.EditValue)) - Double.Parse(ttot_bay.EditValue)

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

                tbukti.EditValue = ""

                tkd_toko.EditValue = ""
                tnama_toko.EditValue = ""
                talamat_toko.EditValue = ""

                tkd_sales.EditValue = ""
                tnama_sales.EditValue = ""

                tbrutto.EditValue = 0
                tdisc_per.EditValue = 0
                tdisc_rp.EditValue = 0
                tnetto.EditValue = 0

                tnote.EditValue = ""

                tlimit.EditValue = 0
                tpiutang.EditValue = 0
                tsisalimit.EditValue = 0

                ttot_retur.EditValue = 0
                tlebih.EditValue = 0
                tbayar.EditValue = 0
                tgiro_gan.EditValue = 0
                tgiro_ca.EditValue = 0
                tlebih_pot.EditValue = 0

                ttot_bay.EditValue = 0
                tsisa_piut.EditValue = 0

                ttop.EditValue = 0

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

    Private Sub opengrid()

        Dim sql As String = String.Format("SELECT     trfaktur_to2.noid, trfaktur_to2.kd_gudang,trfaktur_to2.kd_barang as kd_barang0, trfaktur_to2.kd_barang, ms_barang.nama_barang,ms_barang.nama_lap as nama_lap0,ms_barang.nama_lap, trfaktur_to2.qty, trfaktur_to2.satuan," & _
            "trfaktur_to2.harga, trfaktur_to2.disc_per, trfaktur_to2.disc_rp, trfaktur_to2.jumlah, trfaktur_to2.qtykecil,trfaktur_to2.hargakecil,trfaktur_to2.jumlah0,ms_barang.qty1,ms_barang.qty2,ms_barang.qty3,ms_barang.jenis,trfaktur_to2.qty0,trfaktur_to2.qtykecil0, " & _
            "ms_barang.satuan1,ms_barang.satuan2,ms_barang.satuan3,trfaktur_to2.jenis_trans,ms_barang.nohrus " & _
            "FROM         trfaktur_to2 INNER JOIN " & _
                      "ms_gudang ON trfaktur_to2.kd_gudang = ms_gudang.kd_gudang INNER JOIN " & _
                      "ms_barang ON trfaktur_to2.kd_barang = ms_barang.kd_barang where trfaktur_to2.nobukti='{0}'", tbukti.Text.Trim)

        Dim sql2 As String = String.Format("SELECT     trfaktur_to3.noid, trfaktur_to3.kd_gudang,trfaktur_to3.kd_barang as kd_barang0, trfaktur_to3.kd_barang, ms_barang.nama_barang,ms_barang.nama_lap as nama_lap0, ms_barang.nama_lap, trfaktur_to3.qty, trfaktur_to3.satuan, " & _
                      "trfaktur_to3.harga, 0 AS Expr1, 0 AS Expr2, trfaktur_to3.jumlah, trfaktur_to3.qtykecil, trfaktur_to3.hargakecil,0,ms_barang.qty1,ms_barang.qty2,ms_barang.qty3,ms_barang.jenis, " & _
                      "0,0,ms_barang.satuan1,ms_barang.satuan2,ms_barang.satuan3,'KEMBALI',ms_barang.nohrus " & _
                "FROM         trfaktur_to3 INNER JOIN " & _
                "ms_barang ON trfaktur_to3.kd_barang = ms_barang.kd_barang where trfaktur_to3.nobukti='{0}'", tbukti.Text.Trim)

        Dim sqlok As String = sql

        If spulang = True Then
            If addstat = False Then

                If dv(position)("statkirim").ToString.Equals("TERKIRIM") Then
                    sqlok = String.Format("{0} union all {1}", sql, sql2)
                End If

            End If
        Else
            If Not jenisjual = "T" Then
                sqlok = String.Format("{0} union all {1}", sql, sql2)
            End If
        End If



        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        grid_ins.DataSource = Nothing

        Try

            open_wait()

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sqlok, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            grid_ins.DataSource = dv1

            totalnetto()

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

    Private Sub opengrid_retur()

        Dim sql As String = String.Format("SELECT     trfaktur_to5.noid, trfaktur_to5.kd_gudang,ms_barang.kd_barang as kd_barang0, ms_barang.kd_barang,ms_barang.nama_lap as nama_lap0,ms_barang.nama_lap, ms_barang.nama_barang, trfaktur_to5.qty, trfaktur_to5.satuan, trfaktur_to5.harga, " & _
        "trfaktur_to5.disc_per, trfaktur_to5.disc_rp, trfaktur_to5.jumlah, trfaktur_to5.qtykecil,ms_barang.qty1,ms_barang.qty2,ms_barang.qty3,ms_barang.satuan1,ms_barang.satuan2,ms_barang.satuan3 " & _
        "FROM         trfaktur_to5 INNER JOIN ms_barang ON trfaktur_to5.kd_barang = ms_barang.kd_barang " & _
        "WHERE trfaktur_to5.nobukti='{0}'", tbukti.Text.Trim)


        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        grid_retur.DataSource = Nothing

        Try


            dv_ret = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager_ret = New DataViewManager(ds)
            dv_ret = dvmanager_ret.CreateDataView(ds.Tables(0))

            grid_retur.DataSource = dv_ret

        Catch ex As OleDb.OleDbException
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub simpan()

        If addstat = False Then

            'If cek_sesuaitgl_nomor() = False Then
            '    MsgBox("Tidak dapat merubah tanggal karna tidak sesuai dengan No Faktur..", vbOKOnly + vbInformation, "Informasi")
            '    Return
            'End If

        End If

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try
            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            If Not jenisjual.Equals("T") Then

                isi_nospm()

                If IsNothing(dv_spm) Then

                    MsgBox("Tidak ditemukan sales " & tnama_sales.Text.Trim & " yang berangkat pada tanggal " & ttgl.Text, vbOKOnly + vbInformation, "Informasi")
                    Return

                End If

                If dv_spm.Count <= 0 Then

                    MsgBox("Tidak ditemukan sales " & tnama_sales.Text.Trim & " yang berangkat pada tanggal " & ttgl.Text, vbOKOnly + vbInformation, "Informasi")
                    Return

                End If

                If addstat = False Then
                    If nonota_old.Trim.Equals(tnota.Text.Trim) Then
                        GoTo monggo_lanjut
                    End If
                End If

                If cek_no_nota(cn) = True Then
                    close_wait()
                    MsgBox("No Nota sudah ada...", vbOKOnly + vbInformation, "Informasi")
                    tnota.Focus()
                    Return
                End If
            End If

monggo_lanjut:

            sqltrans = cn.BeginTransaction

            Dim cmd As OleDbCommand
            Dim cmdtoko As OleDbCommand

            If addstat Then
                Dim bukti As String = cekbukti(cn, sqltrans)
                tbukti.EditValue = bukti

                cektglorder(cn, sqltrans)

                Dim skirim As Integer
                Dim statkirim As String
                Dim spulang As Integer

                If jenisjual = "T" Then
                    skirim = 0
                    statkirim = "BELUM TERKIRIM"
                    spulang = 0
                Else
                    skirim = 1
                    statkirim = "TERKIRIM"
                    spulang = 1
                End If

                '1 . update faktur
                Dim sqlin_faktur As String = String.Format("insert into trfaktur_to (nobukti,tanggal,tgl_tempo,kd_toko,kd_karyawan,disc_per,disc_rp,brutto,netto,ket,jnis_fak,jnis_jual,jmlkembali,tgl_kembali,skirim,statkirim,spulang,netto0,disc_per0,disc_rp0,brutto0,jnis_jual2,no_nota,ongkos_angkut,jmlretur,top_toko) values('{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}',{12},'{13}',{14},'{15}',{16},{17},{18},{19},{20},'{21}','{22}',{23},{24},{25})", _
                                    tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_tempo.EditValue), tkd_toko.EditValue, tkd_sales.EditValue, Replace(tdisc_per.EditValue, ",", "."), Replace(tdisc_rp.EditValue, ",", "."), Replace(tbrutto.EditValue, ",", "."), Replace(tnetto.EditValue, ",", "."), tnote.Text.Trim, jenisjual, cbjenis.EditValue, Replace(ttot_kembali.EditValue, ",", "."), convert_date_to_eng(ttgl_kembali.EditValue), skirim, statkirim, spulang, Replace(tnetto.EditValue, ",", "."), Replace(tdisc_per.EditValue, ",", "."), Replace(tdisc_rp.EditValue, ",", "."), Replace(tbrutto.EditValue, ",", "."), cbjenis.Text.Trim, tnota.Text.Trim, Replace(tongkos.EditValue, ",", "."), Replace(ttot_retur.EditValue, ",", "."), ttop.EditValue)

                cmd = New OleDbCommand(sqlin_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()

                If tlebih.EditValue > 0 Then

                    Dim sqlup_faktur As String = String.Format("update trfaktur_to set jmlkelebihan={0} where nobukti='{1}'", Replace(tlebih.EditValue, ",", "."), tbukti.Text.Trim)

                    Using cmdup_faktur As OleDbCommand = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                        cmdup_faktur.ExecuteNonQuery()
                    End Using

                End If

                If Not jenisjual = "T" Then

                    '2. update piutangtoko
                    Dim sqltoko As String = String.Format("update ms_toko set piutangbeli=piutangbeli + {0} where kd_toko='{1}'", Replace(tnetto.EditValue, ",", "."), tkd_toko.Text.Trim)

                    cmdtoko = New OleDbCommand(sqltoko, cn, sqltrans)
                    cmdtoko.ExecuteNonQuery()

                    Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, tbukti.Text.Trim, tbukti.Text.Trim, ttgl.EditValue, tkd_toko.Text.Trim, tnetto.EditValue, 0, "Jual")

                End If

                Dim sqltoko2 As String = String.Format("update ms_toko2 set jmlpiutang=jmlpiutang + {0} where kd_toko='{1}' and kd_karyawan='{2}'", Replace(tnetto.EditValue, ",", "."), tkd_toko.Text.Trim, tkd_sales.Text.Trim)
                Using cmdtoko2 As OleDbCommand = New OleDbCommand(sqltoko2, cn, sqltrans)
                    cmdtoko2.ExecuteNonQuery()
                End Using


                If jenisjual = "T" Then
                    Clsmy.InsertToLog(cn, "btfaktur_to", 1, 0, 0, 0, tbukti.Text.Trim, tnama_toko.Text.Trim, sqltrans)
                Else
                    Clsmy.InsertToLog(cn, "btfaktur_kv", 1, 0, 0, 0, tbukti.Text.Trim, tnama_toko.Text.Trim, sqltrans)
                End If


            Else

                cektglorder(cn, sqltrans)

                '2. update piutang toko
                Dim sqlct As String = String.Format("select netto,kd_toko,kd_karyawan from trfaktur_to where nobukti='{0}'", tbukti.Text.Trim)

                Dim cmdt As OleDbCommand = New OleDbCommand(sqlct, cn, sqltrans)
                Dim drdt As OleDbDataReader = cmdt.ExecuteReader

                If drdt.Read Then
                    If IsNumeric(drdt("netto").ToString) Then

                        Dim nett_sebelum As Double = drdt("netto").ToString
                        Dim kdsales_sebelum As String = drdt("kd_karyawan").ToString
                        Dim kdtoko_old As String = drdt("kd_toko").ToString

                        Dim jenis As String
                        If spulang = True Then
                            jenis = "Jual (Realisasi)"
                        Else
                            jenis = "Jual (Edit)"
                        End If

                        If Not jenisjual = "T" Then

                            Dim sqluptoko As String = String.Format("update ms_toko set piutangbeli=piutangbeli - {0} where kd_toko='{1}'", Replace(nett_sebelum, ",", "."), kdtoko_old)
                            Dim sqluptoko2 As String = String.Format("update ms_toko set piutangbeli=piutangbeli + {0} where kd_toko='{1}'", Replace(tnetto.EditValue, ",", "."), tkd_toko.Text.Trim)

                            Dim cmdtk As OleDbCommand = New OleDbCommand(sqluptoko, cn, sqltrans)
                            cmdtk.ExecuteNonQuery()

                            Dim cmdtk2 As OleDbCommand = New OleDbCommand(sqluptoko2, cn, sqltrans)
                            cmdtk2.ExecuteNonQuery()

                            Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, tbukti.Text.Trim, tbukti.Text.Trim, ttgl.EditValue, kdtoko_old, 0, nett_sebelum, jenis)
                            Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, tbukti.Text.Trim, tbukti.Text.Trim, ttgl.EditValue, tkd_toko.Text.Trim, tnetto.EditValue, 0, jenis)

                        End If


                        Dim sqluptoko21 As String = String.Format("update ms_toko2 set jmlpiutang=jmlpiutang - {0} where kd_toko='{1}' and kd_karyawan='{2}'", Replace(nett_sebelum, ",", "."), kdtoko_old, kdsales_sebelum)
                        Dim sqluptoko222 As String = String.Format("update ms_toko2 set jmlpiutang=jmlpiutang + {0} where kd_toko='{1}' and kd_karyawan='{2}'", Replace(tnetto.EditValue, ",", "."), tkd_toko.Text.Trim, tkd_sales.Text.Trim)

                        Using cmdtok21 As OleDbCommand = New OleDbCommand(sqluptoko21, cn, sqltrans)
                            cmdtok21.ExecuteNonQuery()
                        End Using

                        Using cmdtok22 As OleDbCommand = New OleDbCommand(sqluptoko222, cn, sqltrans)
                            cmdtok22.ExecuteNonQuery()
                        End Using

                    End If
                End If
                drdt.Close()

                '1. update faktur
                Dim sqlup_faktur As String = String.Format("update trfaktur_to set tanggal='{0}',tgl_tempo='{1}',kd_toko='{2}',kd_karyawan='{3}',disc_per={4},disc_rp={5},brutto={6},netto={7},ket='{8}',jmlkembali={9},tgl_kembali='{10}',no_nota='{11}',ongkos_angkut={12},jmlretur={13},top_toko={14} where nobukti='{15}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_tempo.EditValue), tkd_toko.EditValue, tkd_sales.EditValue, Replace(tdisc_per.EditValue, ",", "."), Replace(tdisc_rp.EditValue, ",", "."), Replace(tbrutto.EditValue, ",", "."), Replace(tnetto.EditValue, ",", "."), tnote.Text.Trim, Replace(ttot_kembali.EditValue, ",", "."), convert_date_to_eng(ttgl_kembali.EditValue), tnota.Text.Trim, Replace(tongkos.EditValue, ",", "."), Replace(ttot_retur.EditValue, ",", "."), ttop.EditValue, tbukti.Text.Trim)

                cmd = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()

                'If tlebih.EditValue > 0 Then

                Dim sqlup_faktur2 As String = String.Format("update trfaktur_to set jmlkelebihan={0} where nobukti='{1}'", Replace(tlebih.EditValue, ",", "."), tbukti.Text.Trim)

                Using cmdup_faktur2 As OleDbCommand = New OleDbCommand(sqlup_faktur2, cn, sqltrans)
                    cmdup_faktur2.ExecuteNonQuery()
                End Using

                'End If

                If jenisjual = "T" Then
                    If spulang = False Then

                        Dim sqlupnetto As String = String.Format("update trfaktur_to set netto0={0},disc_per0={1},disc_rp0={2},brutto0={3} where nobukti='{4}'", Replace(tnetto.EditValue, ",", "."), Replace(tdisc_per.EditValue, ",", "."), Replace(tdisc_rp.EditValue, ",", "."), Replace(tbrutto.EditValue, ",", "."), tbukti.Text.Trim)
                        Using cmdupnetto As OleDbCommand = New OleDbCommand(sqlupnetto, cn, sqltrans)
                            cmdupnetto.ExecuteNonQuery()
                        End Using

                        Dim sqlupjenis As String = String.Format("update trfaktur_to set jnis_jual='{0}',jnis_jual2='{1}' where nobukti='{2}'", cbjenis.Text.Trim, cbjenis.Text.Trim, tbukti.Text.Trim)
                        Using cmdupjenis As OleDbCommand = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                            cmdupjenis.ExecuteNonQuery()
                        End Using

                    Else

                        Dim sqlupjenis As String = String.Format("update trfaktur_to set jnis_jual2='{0}' where nobukti='{1}'", cbjenis.Text.Trim, tbukti.Text.Trim)
                        Using cmdupjenis As OleDbCommand = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                            cmdupjenis.ExecuteNonQuery()
                        End Using

                    End If
                Else

                    Dim sqlupnetto As String = String.Format("update trfaktur_to set netto0={0},disc_per0={1},disc_rp0={2},brutto0={3} where nobukti='{4}'", Replace(tnetto.EditValue, ",", "."), Replace(tdisc_per.EditValue, ",", "."), Replace(tdisc_rp.EditValue, ",", "."), Replace(tbrutto.EditValue, ",", "."), tbukti.Text.Trim)
                    Using cmdupnetto As OleDbCommand = New OleDbCommand(sqlupnetto, cn, sqltrans)
                        cmdupnetto.ExecuteNonQuery()
                    End Using


                    Dim sqlupjenis As String = String.Format("update trfaktur_to set jnis_jual='{0}',jnis_jual2='{1}' where nobukti='{2}'", cbjenis.Text.Trim, cbjenis.Text.Trim, tbukti.Text.Trim)
                    Using cmdupjenis As OleDbCommand = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                        cmdupjenis.ExecuteNonQuery()
                    End Using

                End If

                If jenisjual = "T" Then
                    Clsmy.InsertToLog(cn, "btfaktur_to", 0, 1, 0, 0, tbukti.Text.Trim, tnama_toko.Text.Trim, sqltrans)
                Else
                    Clsmy.InsertToLog(cn, "btfaktur_kv", 0, 1, 0, 0, tbukti.Text.Trim, tnama_toko.Text.Trim, sqltrans)
                End If


            End If

            If addstat = True Then

                If Not cek_beli_pinjam_gallon(cn, sqltrans, False, False).Equals("ok") Then
                    GoTo lanjut_aja
                End If

                If simpan2(cn, sqltrans) = "ok" Then

                    If Not dv1.Count <= 0 Then

                        If simpan3(cn, sqltrans).Equals("ok") Then
                        Else
                            GoTo lanjut_aja
                        End If

                    End If

                Else
                    GoTo lanjut_aja
                End If

                If Not simpan_retur(cn, sqltrans).Equals("ok") Then
                    GoTo lanjut_aja
                End If

            Else

                If Not cek_beli_pinjam_gallon(cn, sqltrans, False, False).Equals("ok") Then
                    GoTo lanjut_aja
                End If

                If Not dv1.Count <= 0 Then

                    If Not (simpan3(cn, sqltrans).Equals("ok")) Then
                        GoTo lanjut_aja
                    Else

                        If Not simpan2(cn, sqltrans) = "ok" Then
                            GoTo lanjut_aja
                        End If

                    End If
                Else

                    If Not simpan2(cn, sqltrans) = "ok" Then
                        GoTo lanjut_aja
                    End If

                End If

                If Not simpan_retur(cn, sqltrans).Equals("ok") Then
                    GoTo lanjut_aja
                End If

            End If

            If Not jenisjual.Equals("T") Then
                simpan_nobuktispm(cn, sqltrans)
            End If

            If addstat = True Then
                insertview()
            Else
                updateview()
            End If

            sqltrans.Commit()

            close_wait()

            '   MsgBox("Data disimpan...", vbOKOnly + vbInformation, "Informasi")

            If addstat = True Then


                If jenisjual.Equals("T") Then

                    If statprint Then
                        If MsgBox("Faktur Akan langsung Diprint ... ??", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
                            langsungprint()
                        End If
                    End If


                    kosongkan()

                    tkd_toko.Focus()
                Else

                    kosongkan()

                    tnota.Focus()
                End If


            Else

                close_wait()

                If jenisjual.Equals("T") Then
                    If statbalik = False Then
                        If statprint Then
                            If MsgBox("Faktur Akan langsung Diprint ... ??", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
                                langsungprint()
                            End If
                        End If
                    End If
                End If

                statbalik = True


                Me.Close()
            End If





lanjut_aja:

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

    Private Function cek_brgkembali(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal kdbarang As String) As Boolean

        Dim hasil As Boolean = False

        If IsNothing(cn) Then

            Dim cn2 As OleDbConnection = Nothing
            Try

                cn2 = New OleDbConnection
                cn2 = Clsmy.open_conn

                Dim sql As String = String.Format("select kd_barang from ms_barang where kd_barang_kmb='{0}'", kdbarang)
                Dim cmd As OleDbCommand = New OleDbCommand(sql, cn2)
                Dim drd As OleDbDataReader = cmd.ExecuteReader

                If drd.Read Then
                    If Not drd(0).ToString.Equals("") Then
                        hasil = True
                    End If
                End If
                drd.Close()

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Finally
                If Not cn2 Is Nothing Then
                    If cn2.State = ConnectionState.Open Then
                        cn2.Close()
                    End If
                End If
            End Try

        Else

            Dim sql As String = String.Format("select kd_barang from ms_barang where kd_barang_kmb='{0}'", kdbarang)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Not drd(0).ToString.Equals("") Then
                    hasil = True
                End If
            End If
            drd.Close()

        End If

        

        Return hasil
    End Function

    Private Function cek_brgjaminan(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal kdbarang As String) As Boolean

        Dim hasil As Boolean = False


        If IsNothing(cn) Then

            Dim cn2 As OleDbConnection = Nothing
            Try

                cn2 = New OleDbConnection
                cn2 = Clsmy.open_conn

                Dim sql As String = String.Format("select kd_barang from ms_barang where kd_barang_jmn='{0}'", kdbarang)
                Dim cmd As OleDbCommand = New OleDbCommand(sql, cn2)
                Dim drd As OleDbDataReader = cmd.ExecuteReader

                If drd.Read Then
                    If Not drd(0).ToString.Equals("") Then
                        hasil = True
                    End If
                End If
                drd.Close()

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Finally
                If Not cn2 Is Nothing Then
                    If cn2.State = ConnectionState.Open Then
                        cn2.Close()
                    End If
                End If
            End Try

        Else

            Dim sql As String = String.Format("select kd_barang from ms_barang where kd_barang_jmn='{0}'", kdbarang)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Not drd(0).ToString.Equals("") Then
                    hasil = True
                End If
            End If
            drd.Close()

        End If

        Return hasil

    End Function

    Private Function cek_brgada_jaminan(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal kdbarang As String) As Boolean

        Dim hasil As Boolean = False


        If IsNothing(cn) Then

            Dim cn2 As OleDbConnection = Nothing
            Try

                cn2 = New OleDbConnection
                cn2 = Clsmy.open_conn

                Dim sql As String = String.Format("select kd_barang from ms_barang where len(kd_barang_jmn)>0 and kd_barang='{0}'", kdbarang)
                Dim cmd As OleDbCommand = New OleDbCommand(sql, cn2)
                Dim drd As OleDbDataReader = cmd.ExecuteReader

                If drd.Read Then
                    If Not drd(0).ToString.Equals("") Then
                        hasil = True
                    End If
                End If
                drd.Close()

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Finally
                If Not cn2 Is Nothing Then
                    If cn2.State = ConnectionState.Open Then
                        cn2.Close()
                    End If
                End If
            End Try

        Else

            Dim sql As String = String.Format("select kd_barang from ms_barang where len(kd_barang_jmn)>0 and kd_barang='{0}'", kdbarang)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Not drd(0).ToString.Equals("") Then
                    hasil = True
                End If
            End If
            drd.Close()

        End If

        Return hasil

    End Function

    Private Function simpan2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim cmd As OleDbCommand
        Dim hasil As String = ""
        Dim adabarangharus As String = ""

        Dim dtjual As DataTable = dv1.ToTable
        Dim orowjual() As DataRow = dtjual.Select("jenis_trans='JUAL' or jenis_trans='PINJAM'")

        If orowjual.Length > 0 Then
            For i As Integer = 0 To orowjual.Length - 1

                Dim kdgud As String = orowjual(i)("kd_gudang").ToString
                Dim kdbar As String = orowjual(i)("kd_barang").ToString
                Dim qty As String = orowjual(i)("qty").ToString
                Dim satuan As String = orowjual(i)("satuan").ToString
                Dim harga As String = orowjual(i)("harga").ToString
                Dim disc_per As String = orowjual(i)("disc_per").ToString
                Dim disc_rp As String = orowjual(i)("disc_rp").ToString
                Dim jumlah As String = orowjual(i)("jumlah").ToString
                Dim qtykecil As String = orowjual(i)("qtykecil").ToString
                Dim hargakecil As String = orowjual(i)("hargakecil").ToString
                Dim jumlah0 As String = orowjual(i)("jumlah").ToString
                Dim noid As String = orowjual(i)("noid").ToString
                Dim jenis As String = orowjual(i)("jenis").ToString
                Dim jenistrans As String = orowjual(i)("jenis_trans").ToString

                Dim qty0 As String = orowjual(i)("qty0").ToString
                Dim qtykecil0 As String = orowjual(i)("qtykecil0").ToString

                If cek_brgada_jaminan(cn, sqltrans, kdbar) = True Then
                    adabarangharus = kdbar
                End If

                If jenistrans.Equals("PINJAM") And jenis.Equals("FISIK") Then

                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox("Barang selain jaminan gallon tidak boleh dipinjam", vbOKOnly + vbInformation, "Informasi")
                    hasil = "error"
                    Exit For


                End If

                If jenistrans.Equals("KEMBALI") Then
                    If cek_brgkembali(cn, sqltrans, kdbar) = False Then

                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox("Barang kembali hanya boleh gallon kosong..", vbOKOnly + vbInformation, "Informasi")
                        hasil = "error"
                        Exit For

                    End If
                End If


                If qty0.Equals("") Then
                    qty0 = 0
                End If

                If qtykecil0.Equals("") Then
                    qtykecil0 = 0
                End If

                Dim qty1 As Integer = Integer.Parse(orowjual(i)("qty1").ToString)
                Dim qty2 As Integer = Integer.Parse(orowjual(i)("qty2").ToString)
                Dim qty3 As Integer = Integer.Parse(orowjual(i)("qty3").ToString)

                ' If i = 0 Then
                cek_tglhist(cn, sqltrans, kdbar)
                '  End If

                If addstat = True Then

                    '1. insert faktur_to
                    Dim sqlins As String = String.Format("insert into trfaktur_to2 (nobukti,kd_gudang,kd_barang,qty,satuan,harga,disc_per,disc_rp,jumlah,qtykecil,hargakecil,jumlah0,qty0,qtykecil0,harga0,disc_per0,disc_rp0,jenis_trans) " & _
                                                         "values('{0}','{1}','{2}',{3},'{4}',{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},'{17}')", tbukti.EditValue, kdgud, kdbar, Replace(qty, ",", "."), satuan, Replace(harga, ",", "."), Replace(disc_per, ",", "."), Replace(disc_rp, ",", "."), Replace(jumlah, ",", "."), Replace(qtykecil, ",", "."), Replace(hargakecil, ",", "."), Replace(jumlah, ",", "."), Replace(qty0, ",", "."), Replace(qtykecil0, ",", "."), Replace(harga, ",", "."), Replace(disc_per, ",", "."), Replace(disc_rp, ",", "."), jenistrans)

                    cmd = New OleDbCommand(sqlins, cn, sqltrans)
                    cmd.ExecuteNonQuery()

                    If jenisjual = "T" Then

                        '2.update(barang)

                        'If jenis = "FISIK" Then

                        '    Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, False, True, False)
                        '    If Not hasilplusmin.Equals("ok") Then
                        '        close_wait()
                        '        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        '        hasil = "error"
                        '        Exit For
                        '    End If

                        'End If



                    Else

                        If jenis = "FISIK" And jenistrans.Equals("JUAL") Then

                            Dim hsilsimkos As String = simpankosong(cn, sqltrans, kdbar, tkd_toko.Text.Trim, qty1, qty2, qty3, qtykecil, True)
                            If Not hsilsimkos.Equals("ok") Then

                                close_wait()
                                If Not IsNothing(sqltrans) Then
                                    sqltrans.Rollback()
                                End If

                                MsgBox(hsilsimkos, vbOKOnly + vbExclamation, "Informasi")
                                hasil = "error"
                                Exit For

                            End If

                            '2. update barang
                            Dim hasilplusmin As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, kdgud, False, False, False)
                            If Not hasilplusmin.Equals("ok") Then
                                close_wait()

                                If Not IsNothing(sqltrans) Then
                                    sqltrans.Rollback()
                                End If

                                MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                                hasil = "error"
                                Exit For
                            End If

                            '3. insert to hist stok
                            Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, kdgud, kdbar, 0, qtykecil, "Jual Kanvas", dv_spm(0)("kd_supir").ToString, dv_spm(0)("nopol").ToString)


                        End If

                    End If

                Else

                    Dim sqlc As String = String.Format("select qtykecil from trfaktur_to2 where noid={0}", noid)
                    Dim cmds As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
                    Dim drds As OleDbDataReader = cmds.ExecuteReader

                    Dim shasil As Boolean
                    shasil = False

                    If drds.HasRows Then
                        If drds.Read Then

                            If IsNumeric(drds(0).ToString) Then

                                shasil = True

                                '1. update faktur to
                                Dim sqlup As String = String.Format("update trfaktur_to2 set kd_gudang='{0}',kd_barang='{1}',qty={2},satuan='{3}',harga={4},disc_per={5},disc_rp={6},jumlah={7},qtykecil={8},hargakecil={9},jenis_trans='{10}' where nobukti='{11}' and noid={12}", _
                                                                    kdgud, kdbar, Replace(qty, ",", "."), satuan, Replace(harga, ",", "."), Replace(disc_per, ",", "."), Replace(disc_rp, ",", "."), Replace(jumlah, ",", "."), Replace(qtykecil, ",", "."), Replace(hargakecil, ",", "."), jenistrans, tbukti.Text.Trim, noid)

                                cmd = New OleDbCommand(sqlup, cn, sqltrans)
                                cmd.ExecuteNonQuery()

                                If jenisjual = "T" Then

                                    If spulang = False Then

                                        Dim sqlup2 As String = String.Format("update trfaktur_to2 set qty0={0},qtykecil0={1},jumlah0={2},harga0={3},disc_per0={4},disc_rp0={5} where noid={6}", Replace(qty0, ",", "."), Replace(qtykecil0, ",", "."), Replace(jumlah, ",", "."), Replace(harga, ",", "."), Replace(disc_per, ",", "."), Replace(disc_rp, ",", "."), noid)
                                        Using cmdup2 As OleDbCommand = New OleDbCommand(sqlup2, cn, sqltrans)
                                            cmdup2.ExecuteNonQuery()
                                        End Using

                                    End If

                                    'End If
                                Else


                                    Dim sqlup3 As String = String.Format("update trfaktur_to2 set qty0={0},qtykecil0={1},jumlah0={2},harga0={3},disc_per0={4},disc_rp0={5} where noid={6}", Replace(qty0, ",", "."), Replace(qtykecil0, ",", "."), Replace(jumlah, ",", "."), Replace(harga, ",", "."), Replace(disc_per, ",", "."), Replace(disc_rp, ",", "."), noid)
                                    Using cmdup3 As OleDbCommand = New OleDbCommand(sqlup3, cn, sqltrans)
                                        cmdup3.ExecuteNonQuery()
                                    End Using

                                    If jenis = "FISIK" And jenistrans.Equals("JUAL") Then

                                        Dim hsilsimkos As String = simpankosong(cn, sqltrans, kdbar, tkd_toko.Text.Trim, qty1, qty2, qty3, drds(0).ToString, False)
                                        If Not hsilsimkos.Equals("ok") Then
                                            close_wait()

                                            If Not IsNothing(sqltrans) Then
                                                sqltrans.Rollback()
                                            End If

                                            MsgBox(hsilsimkos, vbOKOnly + vbExclamation, "Informasi")
                                            hasil = "error"
                                            Exit For
                                        End If

                                        Dim hsilsimkos2 As String = simpankosong(cn, sqltrans, kdbar, tkd_toko.Text.Trim, qty1, qty2, qty3, qtykecil, True)
                                        If Not hsilsimkos2.Equals("ok") Then
                                            close_wait()

                                            If Not IsNothing(sqltrans) Then
                                                sqltrans.Rollback()
                                            End If

                                            MsgBox(hsilsimkos2, vbOKOnly + vbExclamation, "Informasi")
                                            hasil = "error"
                                            Exit For
                                        End If


                                        '2. update barang
                                        Dim hasilplusmin As String = PlusMin_Barang_Kend(cn, sqltrans, drds(0).ToString, kdbar, kdgud, True, False, False)
                                        If Not hasilplusmin.Equals("ok") Then
                                            close_wait()

                                            If Not IsNothing(sqltrans) Then
                                                sqltrans.Rollback()
                                            End If

                                            MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                                            hasil = "error"
                                            Exit For
                                        Else

                                            Dim hasilplusmin2 As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, kdgud, False, False, False)

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

                                        If DateValue(tgl_old) <> DateValue(ttgl.EditValue) Then
                                            Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, kdgud, kdbar, drds(0).ToString, 0, "Jual Kanvas (Edit)", dv_spm(0)("kd_supir").ToString, dv_spm(0)("nopol").ToString)
                                        Else
                                            Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, kdgud, kdbar, drds(0).ToString, 0, "Jual Kanvas (Edit)", dv_spm(0)("kd_supir").ToString, dv_spm(0)("nopol").ToString)
                                        End If

                                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, kdgud, kdbar, 0, qtykecil, "Jual Kanvas (Edit)", dv_spm(0)("kd_supir").ToString, dv_spm(0)("nopol").ToString)


                                    End If

                                    ' akhir dari fisik


                                End If



                            End If

                        End If
                    End If

                    If shasil = False Then

                        '1. insert faktur_to
                        Dim sqlins As String = String.Format("insert into trfaktur_to2 (nobukti,kd_gudang,kd_barang,qty,satuan,harga,disc_per,disc_rp,jumlah,qtykecil,hargakecil,jumlah0,qty0,qtykecil0,harga0,disc_per0,disc_rp0,jenis_trans) " & _
                                                             "values('{0}','{1}','{2}',{3},'{4}',{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},'{17}')", tbukti.EditValue, kdgud, kdbar, Replace(qty, ",", "."), satuan, Replace(harga, ",", "."), Replace(disc_per, ",", "."), Replace(disc_rp, ",", "."), Replace(jumlah, ",", "."), Replace(qtykecil, ",", "."), Replace(hargakecil, ",", "."), Replace(jumlah, ",", "."), Replace(qty0, ",", "."), Replace(qtykecil0, ",", "."), Replace(harga, ",", "."), Replace(disc_per, ",", "."), Replace(disc_rp, ",", "."), jenistrans)

                        cmd = New OleDbCommand(sqlins, cn, sqltrans)
                        cmd.ExecuteNonQuery()

                        If jenisjual = "T" Then

                            '2. update barang
                            'Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, False, True, False)
                            'If Not hasilplusmin.Equals("ok") Then
                            '    close_wait()
                            '    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                            '    hasil = "error"
                            '    Exit For
                            'End If

                        Else

                            If jenis = "FISIK" And jenistrans.Equals("JUAL") Then

                                simpankosong(cn, sqltrans, kdbar, tkd_toko.Text.Trim, qty1, qty2, qty3, qtykecil, True)

                                '2. update barang
                                Dim hasilplusmin As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, kdgud, False, False, False)
                                If Not hasilplusmin.Equals("ok") Then
                                    close_wait()
                                    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                                    hasil = "error"
                                    Exit For
                                End If

                                '3. insert to hist stok
                                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, kdgud, kdbar, 0, qtykecil, "Jual Kanvas", dv_spm(0)("kd_supir").ToString, dv_spm(0)("nopol").ToString)

                            End If
                            ' akhir fisik

                        End If


                    End If


                End If

            Next
        End If

        If jenisjual = "T" And statbalik = False And addstat Then
            If Not adabarangharus = "" Then

                If langsung_simpan3(cn, sqltrans, adabarangharus) = False Then
                    hasil = "error"
                End If

            End If
        End If


        If Not hasil.Equals("error") Then
            hasil = "ok"
        End If

        Return hasil

    End Function

    Private Function langsung_simpan3(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal kdbar As String) As Boolean

        Dim hasil As Boolean = False

        Dim sqlcekdulu As String = String.Format("select * from ms_barang where kd_barang in (select kd_barang_kmb from ms_barang where kd_barang='{0}')", kdbar)
        Dim cmddlu As OleDbCommand = New OleDbCommand(sqlcekdulu, cn, sqltrans)
        Dim drdcekdlu As OleDbDataReader = cmddlu.ExecuteReader

        If drdcekdlu.Read Then
            If Not drdcekdlu("kd_barang").ToString.Equals("") Then

                Dim sql3 As String = String.Format("select noid from trfaktur_to3 where nobukti='{0}' and kd_barang='{1}'", tbukti.Text.Trim, kdbar)
                Dim cmd3 As OleDbCommand = New OleDbCommand(sql3, cn, sqltrans)
                Dim drd3 As OleDbDataReader = cmd3.ExecuteReader

                Dim barangada As Boolean = False

                If drd3.Read Then
                    If IsNumeric(drd3(0).ToString) Then
                        barangada = True
                    End If
                End If
                drd3.Close()

                If barangada = False Then

                    Dim kdbarang As String = drdcekdlu("kd_barang").ToString
                    Dim satuan As String = drdcekdlu("satuan1").ToString
                    Dim hargajual As Double = Double.Parse(drdcekdlu("hargajual").ToString)

                    Dim sqlins As String = String.Format("insert into trfaktur_to3(nobukti,kd_gudang,kd_barang,satuan,qty,qtykecil,harga,hargakecil,jumlah) " & _
                    "values('{0}','G000','{1}','{2}',0,0,{3},{3},0)", tbukti.Text.Trim, kdbarang, satuan, hargajual)

                    Using cmdins As OleDbCommand = New OleDbCommand(sqlins, cn, sqltrans)
                        cmdins.ExecuteNonQuery()
                    End Using

                End If

            End If
        End If
        drdcekdlu.Close()

        hasil = True

        Return hasil

    End Function

    Private Function simpan3(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim cmd As OleDbCommand
        Dim drd As OleDbDataReader

        Dim kdgud As String '= tgudang.EditValue
        Dim kdbar As String
        Dim qtykecil As Integer
        Dim satuan As String
        Dim qty As Integer

        Dim harga As Double
        Dim hargakecil As Double
        Dim jumlah As Double

        Dim hasil As String = ""
        Dim ada As Boolean = False

        Dim sqlfak3 As String = ""

        Dim jjenis As String
        If jenisjual = "T" Then

            If addstat = True Then
                jjenis = "Jual TO"
            Else
                jjenis = "Jual TO (Realisasi)"
            End If

        Else

            If addstat = True Then
                jjenis = "Jual Kanvas"
            Else
                jjenis = "Jual Kanvas (Edit)"
            End If

        End If

        Dim dtkembali As DataTable = dv1.ToTable
        Dim orowskembali() As DataRow = dtkembali.Select("jenis_trans='KEMBALI'")

        If orowskembali.Length > 0 Then
            For i As Integer = 0 To orowskembali.Length - 1

                Dim sql As String = String.Format("select * from trfaktur_to3 inner join ms_barang on trfaktur_to3.kd_barang=ms_barang.kd_barang where nobukti='{0}' and ms_barang.kd_barang='{1}' and satuan='{2}'", _
                                              tbukti.Text.Trim, orowskembali(i)("kd_barang").ToString, orowskembali(i)("satuan"))

                cmd = New OleDbCommand(sql, cn, sqltrans)
                drd = cmd.ExecuteReader

                kdgud = orowskembali(i)("kd_gudang").ToString
                kdbar = orowskembali(i)("kd_barang").ToString
                qtykecil = orowskembali(i)("qtykecil").ToString

                satuan = orowskembali(i)("satuan").ToString
                qty = Integer.Parse(orowskembali(i)("qty").ToString)

                harga = Double.Parse(orowskembali(i)("harga").ToString)
                hargakecil = Double.Parse(orowskembali(i)("hargakecil").ToString)
                jumlah = Double.Parse(orowskembali(i)("jumlah").ToString)

                Dim qty1 As Integer = Integer.Parse(orowskembali(i)("qty1").ToString)
                Dim qty2 As Integer = Integer.Parse(orowskembali(i)("qty2").ToString)
                Dim qty3 As Integer = Integer.Parse(orowskembali(i)("qty3").ToString)

                If orowskembali(i)("jenis_trans").ToString.Equals("KEMBALI") Then
                    If cek_brgkembali(cn, sqltrans, kdbar) = False Then

                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox("Barang kembali hanya boleh gallon kosong..", vbOKOnly + vbInformation, "Informasi")
                        hasil = "error"
                        Exit For

                    End If
                End If

                cek_tglhist(cn, sqltrans, kdbar)

                If drd.Read Then
                    If IsNumeric(drd("noid").ToString) Then

                        Dim qtyold As Integer = Integer.Parse(drd("qtykecil").ToString)

                        '   If kdgud.Equals("None") Then
                        If jenisjual = "T" Then
                            GoTo langsung
                        End If
                        '  End If

                        ' cek apakah barang kosong
                        'Dim sqlcekapa As String = String.Format("select * from trfaktur_to2 where kd_barang in (select kd_barang from ms_barang where kd_barang_kmb='{0}') and nobukti='{1}' and satuan='{2}'", kdbar, tbukti.Text.Trim, satuan)
                        'Dim cmdcekapa As OleDbCommand = New OleDbCommand(sqlcekapa, cn, sqltrans)
                        'Dim drapa As OleDbDataReader = cmdcekapa.ExecuteReader

                        'If drapa.Read Then
                        '    If Not drapa("nobukti").ToString.Equals("") Then

                        '        Dim qty2old As Integer = Integer.Parse(drapa("qtykecil").ToString)
                        '        Dim qty22old As Integer = qty2old

                        '        If qty2old > qtyold Then
                        '            qty2old = qty2old - qtyold
                        '        ElseIf qty2old = qtyold Then
                        '            qty2old = 0
                        '        End If

                        'Dim hsilsimkos2 As String = simpankosong(cn, sqltrans, kdbar, tkd_toko.Text.Trim, qty1, qty2, qty3, qty2old, False)
                        'If Not hsilsimkos2.Equals("ok") Then
                        '    close_wait()

                        '    If Not IsNothing(sqltrans) Then
                        '        sqltrans.Rollback()
                        '    End If

                        '    MsgBox(hsilsimkos2, vbOKOnly + vbExclamation, "Informasi")
                        '    hasil = "error"
                        '    Exit For
                        'End If


                        'If Not kdgud.Equals("None") Then
                        '    '2. update barang
                        '    Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtyold, kdbar, kdgud, False, False, False)
                        '    If Not hasilplusmin.Equals("ok") Then
                        '        close_wait()

                        '        If Not IsNothing(sqltrans) Then
                        '            sqltrans.Rollback()
                        '        End If

                        '        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        '        hasil = "error"
                        '        Exit For
                        '    End If

                        'End If

                        'If qty2old <> 0 Then
                        '    '3. insert to hist stok
                        '    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_kembali.EditValue, kdgud, kdbar, 0, qty22old, jjenis, dv_spm(0)("kd_supir").ToString, dv_spm(0)("nopol").ToString)
                        '    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_kembali.EditValue, kdgud, kdbar, qtyold, 0, jjenis, dv_spm(0)("kd_supir").ToString, dv_spm(0)("nopol").ToString)

                        'End If

                        '    End If
                        'End If
                        'drapa.Close()
langsung:

                        ada = True

                    End If
                End If

                drd.Close()

                'If jenisjual = "T" Then
                '    GoTo langsung_insert_update
                'End If

                'Dim jmlharus As Integer = cek_jml_haruskembali(cn, sqltrans, kdbar)
                'Dim jmlharus2 As Integer = jmlharus

                'If jmlharus > qtykecil Then
                '    jmlharus = jmlharus - qtykecil
                'ElseIf jmlharus = qtykecil Then

                '    jmlharus = 0

                'End If



                'If jmlharus <> 0 Then
                '    '3. insert to hist stok
                '    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_kembali.EditValue, kdgud, kdbar, jmlharus2, 0, jjenis, dv_spm(0)("kd_supir").ToString, dv_spm(0)("nopol").ToString)
                '    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_kembali.EditValue, kdgud, kdbar, 0, qtykecil, jjenis, dv_spm(0)("kd_supir").ToString, dv_spm(0)("nopol").ToString)
                'End If



                'If Not kdgud.Equals("None") Then

                '2. update barang
                'Dim hasilplusmin2 As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, True, False, False)
                'If Not hasilplusmin2.Equals("ok") Then
                '    close_wait()

                '    If Not IsNothing(sqltrans) Then
                '        sqltrans.Rollback()
                '    End If

                '    MsgBox(hasilplusmin2, vbOKOnly + vbExclamation, "Informasi")
                '    hasil = "error"
                '    Exit For
                'End If

                'End If

langsung_insert_update:

                If ada Then
                    sqlfak3 = String.Format("update trfaktur_to3 set kd_gudang='{0}',qty={1},qtykecil={2},harga={3},hargakecil={4},jumlah={5} where nobukti='{6}' and kd_barang='{7}' and satuan='{8}'", _
                                                kdgud, qty, qtykecil, harga, hargakecil, jumlah, tbukti.Text.Trim, orowskembali(i)("kd_barang").ToString, orowskembali(i)("satuan"))
                Else
                    sqlfak3 = String.Format("insert into trfaktur_to3 (nobukti,kd_gudang,kd_barang,satuan,qty,qtykecil,harga,hargakecil,jumlah) values('{0}','{1}','{2}','{3}',{4},{5},{6},{7},{8})", tbukti.Text.Trim, kdgud, kdbar, satuan, qty, qtykecil, harga, hargakecil, jumlah)
                End If

                Using cmdfak As OleDbCommand = New OleDbCommand(sqlfak3, cn, sqltrans)
                    cmdfak.ExecuteNonQuery()
                End Using

            Next
        End If

        If hasil.Equals("") Then
            hasil = "ok"
        End If

        Return hasil

    End Function

    Private Function cek_beli_pinjam_gallon(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal stathpus_brang As Boolean, ByVal langsunghapus_det As Boolean) As String

        Dim hasil As String = "ok"

        If jenisjual = "T" Then
            Return "ok"
        End If

        Dim jmljual As Integer = 0
        Dim jmlpinjam As Integer = 0
        Dim jmlbalik As Integer = 0

        Dim qty1, qty2, qty3 As Integer
        Dim kdbarang_kembali As String = ""

        Dim jjenis As String
        If jenisjual = "T" Then

            If addstat = True Then
                jjenis = "Jual TO"
            Else
                jjenis = "Jual TO (Realisasi)"
            End If

        Else

            If addstat = True Then
                jjenis = "Jual Kanvas"
            Else
                jjenis = "Jual Kanvas (Edit)"
            End If

        End If

        If addstat = False Then

            Dim sql_cek2 As String = String.Format("select trfaktur_to2.jenis_trans,sum(trfaktur_to2.qtykecil) as jml from trfaktur_to2 inner join ms_barang " & _
            "on trfaktur_to2.kd_barang=ms_barang.kd_barang " & _
            "where not(ms_barang.jenis='FISIK') and trfaktur_to2.nobukti='{0}' group by trfaktur_to2.jenis_trans", tbukti.Text.Trim)

            Dim cmd2 As OleDbCommand = New OleDbCommand(sql_cek2, cn, sqltrans)
            Dim drd2 As OleDbDataReader = cmd2.ExecuteReader

            If drd2.HasRows Then
                While drd2.Read

                    If drd2("jenis_trans").ToString.Equals("JUAL") Then
                        jmljual = Integer.Parse(drd2("jml").ToString)
                    ElseIf drd2("jenis_trans").ToString.Equals("PINJAM") Then
                        jmlpinjam = Integer.Parse(drd2("jml").ToString)
                    End If

                End While
            End If
            drd2.Close()

            Dim sql3 As String = String.Format("select trfaktur_to3.qtykecil,trfaktur_to3.kd_barang,ms_barang.qty1,ms_barang.qty2,ms_barang.qty3 from trfaktur_to3 inner join ms_barang on trfaktur_to3.kd_barang=ms_barang.kd_barang " & _
            "where trfaktur_to3.nobukti='{0}'", tbukti.Text.Trim)

            Dim cmd3 As OleDbCommand = New OleDbCommand(sql3, cn, sqltrans)
            Dim drd3 As OleDbDataReader = cmd3.ExecuteReader
            If drd3.Read Then
                If IsNumeric(drd3(0).ToString) Then
                    jmlbalik = Integer.Parse(drd3(0).ToString)
                    kdbarang_kembali = drd3(1).ToString
                    qty1 = drd3(2).ToString
                    qty2 = drd3(3).ToString
                    qty3 = drd3(4).ToString
                End If
            End If
            drd3.Close()

            Dim selgln_ksong As Integer = jmljual - jmlpinjam - jmlbalik
            If selgln_ksong > 0 Then

                Dim hsilsimkos As String = simpankosong(cn, sqltrans, kdbarang_kembali, tkd_toko.Text.Trim, qty1, qty2, qty3, selgln_ksong, False)
                If Not hsilsimkos.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hsilsimkos, vbOKOnly + vbExclamation, "Informasi")
                    hasil = "error"
                    GoTo exit_
                End If

            End If

            If jmljual - jmlpinjam <> 0 Then
                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_kembali.EditValue, "None B", kdbarang_kembali, 0, jmljual - jmlpinjam, jjenis, dv_spm(0)("kd_supir").ToString, dv_spm(0)("nopol").ToString)
            End If

            If jmlbalik <> 0 Then
                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_kembali.EditValue, "None B", kdbarang_kembali, jmlbalik, 0, jjenis, dv_spm(0)("kd_supir").ToString, dv_spm(0)("nopol").ToString)
            End If



            If jmlpinjam > 0 Then

                Dim hsilpinjm As String = Hist_PinjamSewa_Toko(tkd_toko.Text.Trim, kdbarang_kembali, jmlpinjam, cn, sqltrans, False, True)
                If Not hsilpinjm.Equals("ok") Then
                    close_wait()

                    If Not IsNothing(sqltrans) Then
                        sqltrans.Rollback()
                    End If

                    MsgBox(hsilpinjm, vbOKOnly + vbExclamation, "Informasi")
                    hasil = "error"
                    GoTo exit_
                End If

                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_kembali.EditValue, "None P", kdbarang_kembali, 0, jmlpinjam, jjenis, dv_spm(0)("kd_supir").ToString, dv_spm(0)("nopol").ToString)

            End If

            If stathpus_brang = True And langsunghapus_det = True Then

                Dim sqldel_detail As String = String.Format("delete from trfaktur_to2 where noid={0}", dv1(Me.BindingContext(dv1).Position)("noid").ToString)

                Using cmddel_det As OleDbCommand = New OleDbCommand(sqldel_detail, cn, sqltrans)
                    cmddel_det.ExecuteNonQuery()
                End Using

                dv1.Delete(Me.BindingContext(dv1).Position)

            ElseIf stathpus_brang = True And langsunghapus_det = False Then
                GoTo exit_
            End If


        End If


        Dim cmd As OleDbCommand = New OleDbCommand("select kd_barang_jmn,kd_barang_kmb,qty1,qty2,qty3,satuan1,satuan2,satuan3 from ms_barang where len(kd_barang_kmb) > 0", cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        If drd.Read Then
            If Not drd(0).ToString.Equals("") Then

                Dim dtjual As DataTable = dv1.ToTable
                Dim orowjual() As DataRow = dtjual.Select(String.Format("kd_barang='{0}' and (jenis_trans='JUAL' or jenis_trans='PINJAM')", drd(0).ToString))
                Dim orowkembali() As DataRow = dtjual.Select(String.Format("kd_barang='{0}' and jenis_trans='KEMBALI'", drd(1).ToString))

                jmljual = 0
                jmlpinjam = 0
                If orowjual.Length > 0 Then
                    For i As Integer = 0 To orowjual.Length - 1
                        If orowjual(i)("jenis_trans").ToString.Equals("JUAL") Then
                            jmljual = jmljual + orowjual(i)("qtykecil").ToString
                        ElseIf orowjual(i)("jenis_trans").ToString.Equals("PINJAM") Then
                            jmlpinjam = jmlpinjam + orowjual(i)("qtykecil").ToString
                        End If
                    Next
                End If

                If orowkembali.Length > 0 Then
                    jmlbalik = orowkembali(0)("qtykecil").ToString
                    kdbarang_kembali = orowkembali(0)("kd_barang").ToString
                    qty1 = orowkembali(0)("qty1").ToString
                    qty2 = orowkembali(0)("qty2").ToString
                    qty3 = orowkembali(0)("qty3").ToString
                End If

                Dim selgln_ksong As Integer = jmljual - jmlpinjam - jmlbalik
                If selgln_ksong > 0 And orowjual.Length > 0 And orowkembali.Length > 0 Then

                    Dim hsilsimkos As String = simpankosong(cn, sqltrans, kdbarang_kembali, tkd_toko.Text.Trim, qty1, qty2, qty3, selgln_ksong, True)
                    If Not hsilsimkos.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hsilsimkos, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        GoTo exit_
                    End If

                End If

                If jmljual - jmlpinjam <> 0 Then
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_kembali.EditValue, "None B", kdbarang_kembali, jmljual - jmlpinjam, 0, jjenis, dv_spm(0)("kd_supir").ToString, dv_spm(0)("nopol").ToString)
                End If

                If jmlbalik <> 0 Then
                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_kembali.EditValue, "None B", kdbarang_kembali, 0, jmlbalik, jjenis, dv_spm(0)("kd_supir").ToString, dv_spm(0)("nopol").ToString)
                End If

                If jmlpinjam > 0 Then

                    Dim hsilpinjm As String = Hist_PinjamSewa_Toko(tkd_toko.Text.Trim, kdbarang_kembali, jmlpinjam, cn, sqltrans, True, True)
                    If Not hsilpinjm.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hsilpinjm, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        GoTo exit_
                    End If

                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_kembali.EditValue, "None P", kdbarang_kembali, jmlpinjam, 0, jjenis, dv_spm(0)("kd_supir").ToString, dv_spm(0)("nopol").ToString)

                End If

            End If


        End If



exit_:

        Return hasil
    End Function

    Private Function simpan_retur(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim cmd As OleDbCommand
        Dim hasil As String = ""

        Dim jjenis As String
        If jenisjual = "T" Then

            If addstat = True Then
                jjenis = "Jual TO (Retur)"
            Else
                jjenis = "Jual TO (Retur-Edit)"
            End If

        Else

            If addstat = True Then
                jjenis = "Jual Kanvas (Retur)"
            Else
                jjenis = "Jual Kanvas (Retur-Edit)"
            End If

        End If

        Dim kdsupir As String = ""
        Dim nopol As String = ""

        If Not jenisjual = "T" Then
            kdsupir = dv_spm(0)("kd_supir").ToString
            nopol = dv_spm(0)("nopol").ToString
        End If

        For i As Integer = 0 To dv_ret.Count - 1

            Dim kdgud As String = dv_ret(i)("kd_gudang").ToString
            Dim kdbar As String = dv_ret(i)("kd_barang").ToString
            Dim qty As String = dv_ret(i)("qty").ToString
            Dim satuan As String = dv_ret(i)("satuan").ToString
            Dim harga As String = dv_ret(i)("harga").ToString
            Dim disc_per As String = dv_ret(i)("disc_per").ToString
            Dim disc_rp As String = dv_ret(i)("disc_rp").ToString
            Dim jumlah As String = dv_ret(i)("jumlah").ToString
            Dim qtykecil As String = dv_ret(i)("qtykecil").ToString
            Dim noid As String = dv_ret(i)("noid").ToString
            Dim qty1 As String = dv_ret(i)("qty1").ToString
            Dim qty2 As String = dv_ret(i)("qty2").ToString
            Dim qty3 As String = dv_ret(i)("qty3").ToString

            cek_tglhist_retur(cn, sqltrans, kdbar)

            If addstat = True Then

                '1. insert faktur_to
                Dim sqlins As String = String.Format("insert into trfaktur_to5 (nobukti,kd_gudang,kd_barang,qty,satuan,harga,disc_per,disc_rp,jumlah,qtykecil) " & _
                                                     "values('{0}','{1}','{2}',{3},'{4}',{5},{6},{7},{8},{9})", tbukti.EditValue, kdgud, kdbar, Replace(qty, ",", "."), satuan, Replace(harga, ",", "."), Replace(disc_per, ",", "."), Replace(disc_rp, ",", "."), Replace(jumlah, ",", "."), Replace(qtykecil, ",", "."))

                cmd = New OleDbCommand(sqlins, cn, sqltrans)
                cmd.ExecuteNonQuery()

                If Not jenisjual = "T" Then

                    Dim hsilsimkos As String = simpankosong(cn, sqltrans, kdbar, tkd_toko.Text.Trim, qty1, qty2, qty3, qtykecil, False)
                    If Not hsilsimkos.Equals("ok") Then
                        close_wait()

                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hsilsimkos, vbOKOnly + vbExclamation, "Informasi")
                        hasil = "error"
                        Exit For
                    End If

                    Dim sqlcek_kembali As String = String.Format("select kd_barang from ms_barang where kd_barang_kmb='{0}'", kdbar)
                    Dim cmdcek_kembali As OleDbCommand = New OleDbCommand(sqlcek_kembali, cn, sqltrans)
                    Dim drdcek_kembali As OleDbDataReader = cmdcek_kembali.ExecuteReader

                    Dim apakah_kembali As Boolean = False
                    If drdcek_kembali.Read Then
                        If Not drdcek_kembali(0).ToString.Equals("") Then
                            apakah_kembali = True
                        End If
                    End If
                    drdcek_kembali.Close()

                    If apakah_kembali = False Then
                        '2: .update(barang)
                        Dim hasilplusmin As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, kdgud, False, False, False)
                        If Not hasilplusmin.Equals("ok") Then
                            close_wait()

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                            hasil = "error"
                            Exit For
                        End If

                        '3. insert to hist stok
                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, kdgud, kdbar, 0, qtykecil, jjenis, kdsupir, nopol)
                    End If

                End If

            Else

                Dim sqlc As String = String.Format("select qtykecil from trfaktur_to5 where noid={0}", noid)
                Dim cmds As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
                Dim drds As OleDbDataReader = cmds.ExecuteReader

                Dim shasil As Boolean
                shasil = False

                If drds.HasRows Then
                    If drds.Read Then

                        If IsNumeric(drds(0).ToString) Then

                            shasil = True

                            Dim qtyold As Integer = drds("qtykecil").ToString

                            If Not jenisjual = "T" Then

                                If apakah_brg_kembali(cn, sqltrans, kdbar) = False Then

                                    Dim hsilsimkos0 As String = simpankosong(cn, sqltrans, kdbar, tkd_toko.Text.Trim, qty1, qty2, qty3, qtyold, True)
                                    If Not hsilsimkos0.Equals("ok") Then
                                        close_wait()

                                        If Not IsNothing(sqltrans) Then
                                            sqltrans.Rollback()
                                        End If

                                        MsgBox(hsilsimkos0, vbOKOnly + vbExclamation, "Informasi")
                                        hasil = "error"
                                        Exit For
                                    End If

                                    Dim hsilsinkos1 As String = simpankosong(cn, sqltrans, kdbar, tkd_toko.Text.Trim, qty1, qty2, qty3, qtykecil, False)
                                    If Not hsilsinkos1.Equals("ok") Then
                                        close_wait()

                                        If Not IsNothing(sqltrans) Then
                                            sqltrans.Rollback()
                                        End If

                                        MsgBox(hsilsinkos1, vbOKOnly + vbExclamation, "Informasi")
                                        hasil = "error"
                                        Exit For
                                    End If

                                    '2. update barang
                                    Dim hasilplusmin As String = PlusMin_Barang_Kend(cn, sqltrans, qtyold, kdbar, kdgud, True, False, False)
                                    If Not hasilplusmin.Equals("ok") Then
                                        close_wait()

                                        If Not IsNothing(sqltrans) Then
                                            sqltrans.Rollback()
                                        End If

                                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                                        hasil = "error"
                                        Exit For
                                    Else

                                        Dim hasilplusmin2 As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, kdgud, False, False, False)

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

                                    ' 3. insert to hist stok

                                    If DateValue(tgl_old) <> DateValue(ttgl.EditValue) Then
                                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, kdgud, kdbar, qtyold, 0, jjenis, kdsupir, nopol)
                                    Else
                                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, kdgud, kdbar, qtyold, 0, jjenis, kdsupir, nopol)
                                    End If

                                    Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, kdgud, kdbar, 0, qtykecil, jjenis, kdsupir, nopol)

                                End If


                            End If

                            '1. update faktur to
                            Dim sqlup As String = String.Format("update trfaktur_to5 set kd_gudang='{0}',kd_barang='{1}',qty={2},satuan='{3}',harga={4},disc_per={5},disc_rp={6},jumlah={7},qtykecil={8} where nobukti='{9}' and noid={10}", _
                                                                kdgud, kdbar, Replace(qty, ",", "."), satuan, Replace(harga, ",", "."), Replace(disc_per, ",", "."), Replace(disc_rp, ",", "."), Replace(jumlah, ",", "."), Replace(qtykecil, ",", "."), tbukti.Text.Trim, noid)

                            cmd = New OleDbCommand(sqlup, cn, sqltrans)
                            cmd.ExecuteNonQuery()


                        End If
                    End If
                End If

                If shasil = False Then

                    '1. insert faktur_to
                    Dim sqlins As String = String.Format("insert into trfaktur_to5 (nobukti,kd_gudang,kd_barang,qty,satuan,harga,disc_per,disc_rp,jumlah,qtykecil) " & _
                                                         "values('{0}','{1}','{2}',{3},'{4}',{5},{6},{7},{8},{9})", tbukti.EditValue, kdgud, kdbar, Replace(qty, ",", "."), satuan, Replace(harga, ",", "."), Replace(disc_per, ",", "."), Replace(disc_rp, ",", "."), Replace(jumlah, ",", "."), Replace(qtykecil, ",", "."))

                    cmd = New OleDbCommand(sqlins, cn, sqltrans)
                    cmd.ExecuteNonQuery()

                    If Not jenisjual = "T" Then

                        Dim hsilsimkos As String = simpankosong(cn, sqltrans, kdbar, tkd_toko.Text.Trim, qty1, qty2, qty3, qtykecil, False)
                        If Not hsilsimkos.Equals("ok") Then

                            close_wait()

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox(hsilsimkos, vbOKOnly + vbExclamation, "Informasi")
                            hasil = "error"
                            Exit For

                        End If

                        If apakah_brg_kembali(cn, sqltrans, kdbar) = False Then

                            '2. update barang
                            Dim hasilplusmin As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, kdgud, False, False, False)
                            If Not hasilplusmin.Equals("ok") Then
                                close_wait()

                                If Not IsNothing(sqltrans) Then
                                    sqltrans.Rollback()
                                End If

                                MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                                hasil = "error"
                                Exit For
                            End If

                            '3. insert to hist stok
                            Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, kdgud, kdbar, 0, qtykecil, jjenis, kdsupir, nopol)

                        End If

                    End If

                End If
            End If

        Next

        If hasil.Equals("") Then
            hasil = "ok"
        End If

        Return hasil

    End Function


#End Region


    Public ReadOnly Property get_statbalik As String
        Get

            Return statbalik

        End Get
    End Property

    Public ReadOnly Property get_crbayar As String
        Get

            Return cbjenis.Text.Trim

        End Get
    End Property

    Public ReadOnly Property get_namatoko As String
        Get

            Return tnama_toko.Text.Trim

        End Get
    End Property

    Public ReadOnly Property get_alamattoko As String
        Get

            Return talamat_toko.Text.Trim

        End Get
    End Property

    Public ReadOnly Property get_tanggal As String
        Get

            Return ttgl.EditValue

        End Get
    End Property

    Public ReadOnly Property get_netto As String
        Get

            Return tnetto.EditValue

        End Get
    End Property


    'Private Sub opengrid2()

    '    grid2.DataSource = Nothing

    '    Dim sql As String = String.Format("SELECT ms_barang_1.kd_barang, ms_barang_1.nama_barang, ms_barang_1.qty1, ms_barang_1.qty2, ms_barang_1.qty3, " & _
    '        "ms_barang_1.satuan1, ms_barang_1.satuan2, ms_barang_1.satuan3, ms_barang_1.hargajual,trfaktur_to2.satuan,0 as qty, 0.0 as jumlah,0 as qtykecil,0.0 as hargakecil " & _
    '        "FROM  trfaktur_to2 INNER JOIN " & _
    '        "ms_barang ON trfaktur_to2.kd_barang = ms_barang.kd_barang INNER JOIN " & _
    '        "ms_barang AS ms_barang_1 ON ms_barang.kd_barang_kmb = ms_barang_1.kd_barang " & _
    '        "WHERE trfaktur_to2.nobukti='{0}'", tbukti.Text.Trim)

    '    Dim cn As OleDbConnection = Nothing
    '    Dim ds As DataSet

    '    Try

    '        open_wait()

    '        dv2 = Nothing

    '        cn = New OleDbConnection
    '        cn = Clsmy.open_conn

    '        ds = New DataSet()
    '        ds = Clsmy.GetDataSet(sql, cn)

    '        dvmanager2 = New DataViewManager(ds)
    '        dv2 = dvmanager2.CreateDataView(ds.Tables(0))

    '        grid2.DataSource = dv2

    '        cek_onkosong(cn)

    '        close_wait()


    '    Catch ex As OleDb.OleDbException
    '        close_wait()
    '        MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
    '    Finally


    '        If Not cn Is Nothing Then
    '            If cn.State = ConnectionState.Open Then
    '                cn.Close()
    '            End If
    '        End If

    '    End Try

    'End Sub


    Private Sub cek_onkosong(ByVal cn As OleDbConnection)

        Dim cmd As OleDbCommand
        Dim drd As OleDbDataReader

        For i As Integer = 0 To dv2.Count - 1

            Dim sql As String = String.Format("select * from trfaktur_to3 where nobukti='{0}' and kd_barang='{1}' and satuan='{2}'", _
                                              tbukti.Text.Trim, dv2(i)("kd_barang").ToString, dv2(i)("satuan").ToString)
            cmd = New OleDbCommand(sql, cn)
            drd = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd("noid").ToString) Then

                    kdgud_edit = drd("kd_gudang").ToString

                    dv2(i)("qty") = drd("qty").ToString
                    dv2(i)("qtykecil") = drd("qtykecil").ToString
                    dv2(i)("hargajual") = drd("harga").ToString
                    dv2(i)("hargakecil") = drd("hargakecil").ToString
                    dv2(i)("jumlah") = drd("jumlah").ToString

                End If
            End If

            drd.Close()

        Next


    End Sub

    Private Sub totalnetto()

        If IsNothing(dv1) Then

            tdisc_per.EditValue = 0.0
            tdisc_rp.EditValue = 0
            tnetto.EditValue = 0
            ttot_kembali.EditValue = 0
            tbrutto.EditValue = 0
            tongkos.EditValue = 0
            ttot_retur.EditValue = 0
            tlebih.EditValue = 0

            Return
        End If

        If dv1.Count <= 0 Then

            tdisc_per.EditValue = 0.0
            tdisc_rp.EditValue = 0
            tnetto.EditValue = 0
            ttot_kembali.EditValue = 0
            tbrutto.EditValue = 0
            tongkos.EditValue = 0
            ttot_retur.EditValue = 0
            tlebih.EditValue = 0

            Return
        End If

        Dim jumlah As Double = 0
        Dim xjumlah As Double = 0

        For i As Integer = 0 To dv1.Count - 1

            If dv1(i)("jenis_trans").ToString.Equals("JUAL") Or dv1(i)("jenis_trans").ToString.Equals("PINJAM") Then
                jumlah = jumlah + Double.Parse(dv1(i)("jumlah").ToString)
            ElseIf dv1(i)("jenis_trans").ToString.Equals("KEMBALI") Then
                xjumlah = xjumlah + Double.Parse(dv1(i)("jumlah").ToString)
            End If

        Next

        tbrutto.EditValue = jumlah
        ttot_kembali.EditValue = xjumlah

        'If Not IsNothing(dv2) Then

        '    For i As Integer = 0 To dv2.Count - 1
        '        xjumlah = xjumlah + Double.Parse(dv2(i)("jumlah").ToString)
        '    Next

        'End If



        Dim totkembali As Double = ttot_kembali.EditValue

        jumlah = jumlah - totkembali

        Dim xretur As Double = 0

        If Not IsNothing(dv_ret) Then

            For i As Integer = 0 To dv_ret.Count - 1
                xretur = xretur + Double.Parse(dv_ret(i)("jumlah").ToString)
            Next

        End If

        ttot_retur.EditValue = xretur

        jumlah = jumlah - xretur

        Dim diskon As Double = tdisc_rp.EditValue

        If diskon > 0 Then
            jumlah = jumlah - diskon
        End If

        jumlah = jumlah + Double.Parse(tongkos.EditValue)

        If jumlah < 0 Then
            tlebih.EditValue = Replace(jumlah, "-", "")
            tnetto.EditValue = 0
        Else
            tnetto.EditValue = jumlah
            tlebih.EditValue = 0
        End If

    End Sub

    Private Sub ceklimitby(ByVal cn As OleDbConnection)

        Dim sql As String = String.Format("select * from ms_toko2 where kd_toko='{0}' and kd_karyawan='{1}'", tkd_toko.EditValue, tkd_sales.EditValue)

        'If IsNothing(cn) Then
        '    cn = New OleDbConnection
        '    cn = Clsmy.open_conn
        'End If

        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
        Dim dread As OleDbDataReader = cmd.ExecuteReader
        Dim hasil = False

        If dread.HasRows Then
            If dread.Read Then

                Dim limitval As String = dread("limit_val").ToString
                Dim jmlpiut As String = dread("jmlpiutang").ToString

                If Not limitval.Equals("") Then

                    tlimit.EditValue = limitval
                    tpiutang.EditValue = jmlpiut

                    Dim sisa As Double = Double.Parse(limitval) - Double.Parse(jmlpiut)

                    tsisalimit.EditValue = sisa

                    hasil = True

                End If

            End If
        End If

        dread.Close()

        If hasil = False Then
            tlimit.EditValue = 0
            tpiutang.EditValue = 0
            tsisalimit.EditValue = 0
        End If

    End Sub

    Private Function cekbukti(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim sql As String = ""

        Dim nobuktiawal As String
        If jenisjual = "T" Then
            nobuktiawal = String.Format("FK.{0}", initial_user)
        Else
            nobuktiawal = String.Format("FV.{0}", initial_user)
        End If

        Dim tahun As String = Year(ttgl.EditValue)
        tahun = Microsoft.VisualBasic.Right(tahun, 2)
        Dim bulan As String = Month(ttgl.EditValue)

        If bulan.Length = 1 Then
            bulan = "0" & bulan
        End If

        Dim bulantahun As String = String.Format("{0}{1}", tahun, bulan)

        If jenisjual = "T" Then
            'sql = String.Format("select max(nobukti) from trfaktur_to where len(nobukti)=13 and jnis_fak='T' and YEAR(tanggal)='{0}' and MONTH(tanggal)='{1}' and nobukti like '{2}%' and nobukti like '%{3}%'", Year(ttgl.EditValue), Month(ttgl.EditValue), nobuktiawal, bulantahun)
            sql = String.Format("select max(nobukti) from trfaktur_to where len(nobukti)=13 and jnis_fak='T'  and nobukti like '{0}{1}%'", nobuktiawal, bulantahun)
        Else
            'sql = String.Format("select max(nobukti) from trfaktur_to where len(nobukti)=13 and jnis_fak='K' and YEAR(tanggal)='{0}' and MONTH(tanggal)='{1}' and nobukti like '{2}%' and nobukti like '%{3}%'", Year(ttgl.EditValue), Month(ttgl.EditValue), nobuktiawal, bulantahun)
            sql = String.Format("select max(nobukti) from trfaktur_to where len(nobukti)=13 and jnis_fak='K' and nobukti like '{0}{1}%'", nobuktiawal, bulantahun)
        End If

        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim nilai As Integer = 0

        If drd.HasRows Then
            If drd.Read Then

                If Not drd(0).ToString.Equals("") Then
                    nilai = Microsoft.VisualBasic.Right(drd(0).ToString, 5)
                End If

            End If
        End If

        nilai = nilai + 1

        Dim kbukti As String = nilai

        Select Case kbukti.Length
            Case 1
                kbukti = "0000" & nilai
            Case 2
                kbukti = "000" & nilai
            Case 3
                kbukti = "00" & nilai
            Case 4
                kbukti = "0" & nilai
            Case Else
                kbukti = nilai
        End Select

        'Dim jenisnota As String
        'If jenisjual = "T" Then
        '    jenisnota = "FK."
        'Else
        '    jenisnota = "FV."
        'End If

        Return String.Format("{0}{1}{2}{3}", nobuktiawal, tahun, bulan, kbukti)

    End Function

    Private Function cek_no_nota(ByVal cn As OleDbConnection) As Boolean

        Dim sql As String = String.Format("select no_nota from trfaktur_to where sbatal=0 and no_nota='{0}'", tnota.Text.Trim)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim hasil As Boolean = False
        If drd.Read Then
            If Not drd(0).ToString.Equals("") Then
                hasil = True
            End If
        End If

        drd.Close()

        Return hasil

    End Function

    Private Function cek_sesuaitgl_nomor() As Boolean

        Dim nobukti As String = tbukti.Text.Trim
        Dim tahun As Integer = nobukti.Trim.Substring(4, 2)
        Dim bulan As Integer = nobukti.Trim.Substring(6, 2)

        Dim tahunsekarang As Integer = Year(ttgl.EditValue)
        Dim bulansekarang As Integer = Month(ttgl.EditValue)

        tahunsekarang = tahunsekarang.ToString.Substring(2, 2)

        Dim hasil As Boolean = True
        If tahun <> tahunsekarang Then
            hasil = False
        End If

        If bulan <> bulansekarang Then
            hasil = False
        End If

        Return hasil

    End Function

    Private Sub simpan_nobuktispm(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        Dim sqldel As String = String.Format("delete from trfaktur_to4 where nobukti='{0}'", tbukti.Text.Trim)
        Using cmddel As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
            cmddel.ExecuteNonQuery()
        End Using

        For i As Integer = 0 To dv_spm.Count - 1

            Dim sqlin As String = String.Format("insert into trfaktur_to4 (nobukti,nobukti_spm) values('{0}','{1}')", tbukti.Text.Trim, dv_spm(i)("nobukti").ToString)
            Using cmdin As OleDbCommand = New OleDbCommand(sqlin, cn, sqltrans)
                cmdin.ExecuteNonQuery()
            End Using

        Next

    End Sub


    Private Sub cektglorder(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        Dim sql As String = String.Format("select tgl_order,tgl_akhir from ms_toko where kd_toko='{0}'", tkd_toko.Text.Trim)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        If drd.Read Then
            Dim tglhasil As String = drd(0).ToString
            Dim tglakhir As String = drd(1).ToString

            If Not IsDate(tglhasil) Then

                Dim sqlup As String = String.Format("update ms_toko set tgl_order='{0}' where kd_toko='{1}'", convert_date_to_eng(ttgl.EditValue), tkd_toko.Text.Trim)

                Dim cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                cmdup.ExecuteNonQuery()

            End If

            Dim sqlup2 As String = String.Format("update ms_toko set tgl_akhir='{0}' where kd_toko='{1}'", convert_date_to_eng(ttgl.EditValue), tkd_toko.Text.Trim)

            If Not IsDate(tglakhir) Then

                Using cmdup2 As OleDbCommand = New OleDbCommand(sqlup2, cn, sqltrans)
                    cmdup2.ExecuteNonQuery()
                End Using

            Else

                If DateValue(tglakhir) < DateValue(ttgl.EditValue) Then

                    Using cmdup2 As OleDbCommand = New OleDbCommand(sqlup2, cn, sqltrans)
                        cmdup2.ExecuteNonQuery()
                    End Using
                End If

            End If

        End If

        drd.Close()

    End Sub

    Private Sub cek_tglhist(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal kdbarang As String)

        Dim sql As String = String.Format("select tgljual from ms_barang where kd_barang='{0}'", kdbarang)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim hasil As Boolean = False
        Dim tglhasil As String = ""

        If drd.Read Then
            If IsDate(drd(0).ToString) Then
                hasil = True
                tglhasil = drd(0).ToString
            End If
        End If

        Dim sqlup As String = ""
        If hasil = False Then
            sqlup = String.Format("update ms_barang set tgljual='{0}' where kd_barang='{1}'", convert_date_to_eng(ttgl.EditValue), kdbarang)

            Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                cmdup.ExecuteNonQuery()
            End Using

        Else

            If DateValue(ttgl.EditValue) > DateValue(tglhasil) Then
                sqlup = String.Format("update ms_barang set tgljual='{0}' where kd_barang='{1}'", convert_date_to_eng(ttgl.EditValue), kdbarang)

                Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                    cmdup.ExecuteNonQuery()
                End Using

            End If

        End If

    End Sub

    Private Sub cek_tglhist_retur(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal kdbarang As String)

        Dim sql As String = String.Format("select tglretur from ms_barang where kd_barang='{0}'", kdbarang)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim hasil As Boolean = False
        Dim tglhasil As String = ""

        If drd.Read Then
            If IsDate(drd(0).ToString) Then
                hasil = True
                tglhasil = drd(0).ToString
            End If
        End If

        Dim sqlup As String = ""
        If hasil = False Then
            sqlup = String.Format("update ms_barang set tglretur='{0}' where kd_barang='{1}'", convert_date_to_eng(ttgl_kembali.EditValue), kdbarang)

            Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                cmdup.ExecuteNonQuery()
            End Using

        Else

            If DateValue(ttgl.EditValue) > DateValue(tglhasil) Then
                sqlup = String.Format("update ms_barang set tglretur='{0}' where kd_barang='{1}'", convert_date_to_eng(ttgl_kembali.EditValue), kdbarang)

                Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                    cmdup.ExecuteNonQuery()
                End Using

            End If

        End If

    End Sub


    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("nobukti") = tbukti.Text.Trim
        orow("tanggal") = ttgl.Text.Trim
        orow("kd_toko") = tkd_toko.Text.Trim
        orow("nama_toko") = tnama_toko.Text.Trim
        orow("alamat_toko") = talamat_toko.Text.Trim
        orow("kd_karyawan") = tkd_sales.Text.Trim
        orow("nama_karyawan") = tnama_sales.Text.Trim
        orow("ket") = tnote.Text.Trim
        orow("netto") = tnetto.EditValue

        If jenisjual = "T" Then
            orow("skirim") = 0
            orow("spulang") = 0
            orow("statkirim") = "BELUM TERKIRIM"
        Else
            orow("no_nota") = tnota.Text.Trim
            orow("statkirim") = "TERKIRIM"
        End If


        orow("sbatal") = 0

        dv.EndInit()

    End Sub

    Private Sub updateview()

        If jenisjual = "T" Then

            If spulang Then

                dv(position)("netto") = tnetto.EditValue

            Else

                dv(position)("nobukti") = tbukti.Text.Trim
                dv(position)("tanggal") = ttgl.Text.Trim
                dv(position)("kd_toko") = tkd_toko.Text.Trim
                dv(position)("nama_toko") = tnama_toko.Text.Trim
                dv(position)("alamat_toko") = talamat_toko.Text.Trim
                dv(position)("kd_karyawan") = tkd_sales.Text.Trim
                dv(position)("nama_karyawan") = tnama_sales.Text.Trim
                dv(position)("ket") = tnote.Text.Trim
                dv(position)("netto") = tnetto.EditValue

            End If

        Else

            dv(position)("nobukti") = tbukti.Text.Trim
            dv(position)("tanggal") = ttgl.Text.Trim
            dv(position)("kd_toko") = tkd_toko.Text.Trim
            dv(position)("nama_toko") = tnama_toko.Text.Trim
            dv(position)("alamat_toko") = talamat_toko.Text.Trim
            dv(position)("kd_karyawan") = tkd_sales.Text.Trim
            dv(position)("nama_karyawan") = tnama_sales.Text.Trim
            dv(position)("ket") = tnote.Text.Trim
            dv(position)("no_nota") = tnota.Text.Trim
            dv(position)("netto") = tnetto.EditValue

        End If



    End Sub

    Private Sub isi_gudang()

        Dim sql As String = ""

        sql = "select kd_gudang,nama_gudang,def_kosong from ms_gudang where tipe_gudang='FISIK'"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet
        Dim dvg As DataView


        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvg = dvm.CreateDataView(ds.Tables(0))

            dtgudang = dvg.ToTable

            If Not jenisjual = "T" Then

                Dim orow As DataRow = dtgudang.NewRow
                orow("kd_gudang") = "None"
                orow("nama_gudang") = "None"
                dtgudang.Rows.InsertAt(orow, 0)

            Else

                If spulang = False Then

                    Dim orow As DataRow = dtgudang.NewRow
                    orow("kd_gudang") = "None"
                    orow("nama_gudang") = "None"
                    orow("def_kosong") = 0
                    dtgudang.Rows.InsertAt(orow, 0)

                End If

            End If

            tgudang.Properties.DataSource = dtgudang

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

    Private Sub isi_nospm()

        Dim sql As String = String.Format("SELECT     trspm.nobukti, trspm.tglberangkat, trspm.nopol, supir.nama_karyawan AS supir, trspm.note,trspm.kd_supir " & _
            "FROM         ms_pegawai AS supir INNER JOIN " & _
            "trspm ON supir.kd_karyawan = trspm.kd_supir " & _
            "WHERE     (trspm.kd_sales = '{0}') AND (trspm.tglberangkat = '{1}') AND (trspm.sbatal = 0)", tkd_sales.Text.Trim, convert_date_to_eng(ttgl.EditValue))

        Dim cn As OleDbConnection = Nothing

        Try

            dv_spm = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds_spm As DataSet
            ds_spm = New DataSet()
            ds_spm = Clsmy.GetDataSet(sql, cn)

            dvmanager_spm = New DataViewManager(ds_spm)
            dv_spm = dvmanager_spm.CreateDataView(ds_spm.Tables(0))

            grid_spm.DataSource = dv_spm

        Catch ex As OleDb.OleDbException
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    'Private Sub isi_gudang_detail()

    '    Dim sql As String = ""

    '    If jenisjual = "T" Then
    '        sql = "select kd_gudang,nama_gudang from ms_gudang where tipe_gudang='FISIK' order by kd_gudang"
    '    Else
    '        sql = "select kd_gudang,nama_gudang from ms_gudang where tipe_gudang='MOBIL' order by kd_gudang"
    '    End If

    '    Dim cn As OleDbConnection = Nothing
    '    Dim ds As DataSet
    '    Dim dvg As DataView

    '    Try

    '        cn = Clsmy.open_conn
    '        ds = New DataSet
    '        ds = Clsmy.GetDataSet(sql, cn)

    '        Dim dvm As DataViewManager = New DataViewManager(ds)
    '        dvg = dvm.CreateDataView(ds.Tables(0))


    '        For i As Integer = 0 To dvg.Count - 1
    '            r_gudang.Items.Add(dvg(i)("kd_gudang").ToString)
    '        Next


    '    Catch ex As Exception
    '        MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
    '    Finally

    '        If Not cn Is Nothing Then
    '            If cn.State = ConnectionState.Open Then
    '                cn.Close()
    '            End If
    '        End If

    '    End Try

    'End Sub

    Private Sub isi_barang()

        Dim sql As String = ""

        If jenisjual = "T" Then
            sql = "select a.kd_barang,b.nama_barang,b.satuan1,b.satuan2,b.satuan3,a.jmlstok1,a.jmlstok2,a.jmlstok3,b.qty1,b.qty2,b.qty3,b.hargajual" & _
              " from ms_barang2 a inner join ms_barang b on a.kd_barang=b.kd_barang where b.jenis='FISIK' and b.sjual=1"
        Else
            sql = "select a.kd_barang,b.nama_barang,b.satuan1,b.satuan2,b.satuan3,a.jmlstok_k1 as jmlstok1,a.jmlstok_k2 as jmlstok2,a.jmlstok_k3 as jmlstok3,b.qty1,b.qty2,b.qty3,b.hargajual" & _
            " from ms_barang2 a inner join ms_barang b on a.kd_barang=b.kd_barang where b.jenis='FISIK' and b.sjual=1"
        End If

        Dim cn As OleDbConnection = Nothing
        Dim ds_barang As DataSet

        Try

            cn = Clsmy.open_conn
            ds_barang = New DataSet
            ds_barang = Clsmy.GetDataSet(sql, cn)

            'Dim dvm As DataViewManager = New DataViewManager(ds_barang)
            'dv_barang = dvm.CreateDataView(ds_barang.Tables(0))

            'r_barang1.DataSource = dv_barang
            'r_barang2.DataSource = dv_barang

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

    Private Function cektoko_boleh() As Boolean

        Dim hasil = True
        If ins_alltokouser = 1 Then
            Return hasil
        End If


        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select id from ms_usersys4 where kd_toko='{0}' and nama_user='{1}'", tkd_toko.Text.Trim, userprog)
            Dim cmdc As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drdc As OleDbDataReader = cmdc.ExecuteReader

            If drdc.Read Then
                If IsNumeric(drdc(0).ToString) Then
                    If Integer.Parse(drdc(0).ToString) > 0 Then
                        hasil = True
                    Else
                        hasil = False
                    End If
                Else
                    hasil = False
                End If
            Else
                hasil = False
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

        Return hasil
    End Function

    Private Function cek_initial_pajak() As Boolean

        Dim hasil As Boolean = True

        Dim sql As String = String.Format("SELECT ms_group.npw, ms_toko.pk, ms_toko.npw AS npw_toko " & _
        "FROM ms_group INNER JOIN ms_toko ON ms_group.kd_group = ms_toko.kd_group " & _
        "WHERE kd_toko='{0}'", tkd_toko.Text.Trim)

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd("pk").ToString) Then

                    If Not drd("npw").ToString.Equals("-") Then
                        If drd("npw").ToString.Length > 0 Then
                            If Not initial_user.Equals("F") Then
                                hasil = False
                                GoTo langsung
                            End If
                        End If
                    End If

                    If Integer.Parse(drd("pk").ToString) = 1 Then

                        If drd("npw_toko").ToString.Length > 0 Then
                            If Not initial_user.Equals("F") Then
                                hasil = False
                                GoTo langsung
                            End If
                        End If

                    End If

                End If
            End If

langsung:
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


        Return hasil
    End Function

    'Private Sub isi_alasan()

    '    Const sql As String = "select alasan from ms_alasan where tipe='RETUR'"

    '    Dim cn As OleDbConnection = Nothing

    '    Try

    '        cn = New OleDbConnection
    '        cn = Clsmy.open_conn

    '        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
    '        Dim drd As OleDbDataReader = cmd.ExecuteReader

    '        talasan.Properties.Items.Clear()

    '        While drd.Read

    '            talasan.Properties.Items.Add(drd(0).ToString)

    '        End While


    '    Catch ex As Exception
    '        MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
    '    Finally


    '        If Not cn Is Nothing Then
    '            If cn.State = ConnectionState.Open Then
    '                cn.Close()
    '            End If
    '        End If
    '    End Try

    'End Sub


    Private Sub bts_sal_Click(sender As System.Object, e As System.EventArgs) Handles bts_sal.Click
        Dim fs As New fssales With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .kd_toko = tkd_toko.Text.Trim}
        fs.ShowDialog(Me)

        tkd_sales.EditValue = fs.get_KODE
        ' tnama_sales.EditValue = fs.get_NAMA

        tkd_sales_EditValueChanged(sender, Nothing)

    End Sub

    Private Sub tkd_sales_EditValueChanged(sender As Object, e As System.EventArgs) Handles tkd_sales.Validated
        If tkd_sales.Text.Trim.Length > 0 Then

            If tkd_toko.Text.Trim.Length = 0 Then
                tkd_sales.Text = ""
                tnama_sales.Text = ""
                Return
            End If

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select * from ms_toko2 inner join ms_pegawai on ms_toko2.kd_karyawan=ms_pegawai.kd_karyawan " & _
            "where ms_pegawai.bagian='SALES' and ms_pegawai.aktif=1 and ms_pegawai.kd_karyawan='{0}' and ms_toko2.kd_toko='{1}'", tkd_sales.Text.Trim, tkd_toko.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_sales.EditValue = dread("kd_karyawan").ToString
                        tnama_sales.EditValue = dread("nama_karyawan").ToString

                        '   tkd_toko.Text = ""
                        ' tnama_toko.Text = ""
                        '  talamat_toko.Text = ""

                        '  tlimit.EditValue = 0
                        '  tpiutang.EditValue = 0
                        '  tsisalimit.EditValue = 0

                        ceklimitby(cn)

                    Else
                        tkd_sales.EditValue = ""
                        tnama_sales.EditValue = ""

                        ' tkd_toko.Text = ""
                        ' tnama_toko.Text = ""
                        ' talamat_toko.Text = ""

                        'tlimit.EditValue = 0
                        'tpiutang.EditValue = 0
                        'tsisalimit.EditValue = 0

                    End If
                Else
                    tkd_sales.EditValue = ""
                    tnama_sales.EditValue = ""

                    ' tkd_toko.Text = ""
                    ' tnama_toko.Text = ""
                    ' talamat_toko.Text = ""

                    'tlimit.EditValue = 0
                    'tpiutang.EditValue = 0
                    'tsisalimit.EditValue = 0

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

    Private Sub tkd_sales_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_sales.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_sal_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_sales_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_sales.LostFocus
        If tkd_sales.Text.Trim.Length = 0 Then
            tkd_sales.EditValue = ""
            tnama_sales.EditValue = ""

            tlimit.EditValue = 0
            tpiutang.EditValue = 0
            tsisalimit.EditValue = 0

            'tkd_toko_EditValueChanged(sender, Nothing)

        Else

            isi_nospm()

        End If
    End Sub

    Private Sub bts_toko_Click(sender As System.Object, e As System.EventArgs) Handles bts_toko.Click

        Dim valltoko As Boolean
        If spulang = True Then
            valltoko = True
        Else
            valltoko = False
        End If

        Dim fs As New fsoutlet With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .valltoko = valltoko}
        fs.ShowDialog(Me)

        tkd_toko.EditValue = fs.get_KODE
        tnama_toko.EditValue = fs.get_NAMA
        talamat_toko.EditValue = fs.get_ALAMAT

        tkd_toko_EditValueChanged(sender, Nothing)


    End Sub

    Private Sub tkd_toko_EditValueChanged(sender As Object, e As System.EventArgs) Handles tkd_toko.Validated
        If tkd_toko.Text.Trim.Length > 0 Then

            If spulang = False Then

                If cektoko_boleh() = False Then
                    MsgBox("Toko tidak boleh diakses...", vbOKOnly + vbInformation, "Informasi")
                    tkd_toko.Text = ""
                    tnama_toko.Text = ""
                    talamat_toko.Text = ""
                    ttop.EditValue = 0
                    Return
                End If

            End If

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select kd_toko,nama_toko,alamat_toko,top_toko,spromo from ms_toko where kd_toko='{0}' and aktif=1", tkd_toko.Text.Trim)

            Try

                Dim dtjamin2 As DataTable = dv1.ToTable
                Dim hsilrows2 As DataRow() = dtjamin2.Select("kd_barang in ('BN0002','BN0003') and jenis_trans in ('JUAL','PINJAM')")

                Dim adabonus As Boolean = False
                If hsilrows2.Length > 0 Then
                    adabonus = True
                End If

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        'If spulang = True Then

                        If Integer.Parse(dread("spromo").ToString) = 0 And adabonus = True Then
                            MsgBox("Ada barang bonus, toko tidak sesuai..", vbOKOnly + vbInformation, "Informasi")
                            tkd_toko.EditValue = kdtoko_old
                            Return
                        End If

                        'End If

                        tkd_toko.EditValue = dread("kd_toko").ToString
                        tnama_toko.EditValue = dread("nama_toko").ToString
                        talamat_toko.EditValue = dread("alamat_toko").ToString

                        ttop.EditValue = Integer.Parse(dread("top_toko").ToString)

                        ttgl_EditValueChanged(sender, Nothing)

                        ' ceklimitby(cn)

                    Else
                        tkd_toko.EditValue = ""
                        tnama_toko.EditValue = ""
                        talamat_toko.Text = ""

                        tkd_sales.Text = ""
                        tnama_sales.Text = ""

                        tlimit.EditValue = 0
                        tpiutang.EditValue = 0
                        tsisalimit.EditValue = 0

                        ttop.EditValue = 0

                    End If
                Else
                    tkd_toko.EditValue = ""
                    tnama_toko.EditValue = ""
                    talamat_toko.Text = ""

                    tkd_sales.Text = ""
                    tnama_sales.Text = ""

                    tlimit.EditValue = 0
                    tpiutang.EditValue = 0
                    tsisalimit.EditValue = 0
                    ttop.EditValue = 0
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

            If tnama_toko.Text.Trim.Length > 0 Then
                If tkd_sales.Text.Trim.Length > 0 Then
                    tkd_sales_EditValueChanged(sender, Nothing)
                End If
            End If

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

            tlimit.EditValue = 0
            tpiutang.EditValue = 0
            tsisalimit.EditValue = 0

        End If
    End Sub

    Private Sub btadd_Click(sender As System.Object, e As System.EventArgs) Handles btadd.Click
        Using fkar2 As New ffaktur_to3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .dvkembali = dv2, .addstat = True, .position = 0, .jenisjual = jenisjual, .kdsales = tkd_sales.Text.Trim, .tanggalfaktur = ttgl.EditValue}
            fkar2.ShowDialog(Me)

            '  tbrutto.EditValue = GridView1.Columns("jumlah").SummaryItem.SummaryValue

            If SplitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both Then

                SimpleButton1.Focus()
            Else
                tnote.Focus()
            End If

        End Using
    End Sub

    Private Sub btedit_Click(sender As System.Object, e As System.EventArgs) Handles btedit.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        If dv1(Me.BindingContext(dv1).Position)("jenis").Equals("FISIK") Then

            Using fkar2 As New ffaktur_to3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .dvkembali = dv2, .addstat = False, .position = Me.BindingContext(dv1).Position, .jenisjual = jenisjual, .spulang = spulang, .kdsales = tkd_sales.Text.Trim, .tanggalfaktur = ttgl.EditValue}

                If addstat = False Then
                    If spulang Then

                        'fkar2.tjml.Properties.ReadOnly = True
                        fkar2.tsat.Enabled = False

                        '  fkar2.tdisc_per.Properties.ReadOnly = True
                        ' fkar2.tdisc_rp.Properties.ReadOnly = True

                    End If
                End If

                fkar2.ShowDialog(Me)

                tbrutto.EditValue = GridView5.Columns("jumlah").SummaryItem.SummaryValue

            End Using


        Else

            Dim kodebarang As String = cek_kode_kembali(dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString)
            Dim posisi As Integer = 0

            For i As Integer = 0 To dv2.Count - 1
                If dv2(i)("kd_barang").ToString.Equals(kodebarang) And dv2(i)("satuan").ToString.Equals(dv1(Me.BindingContext(dv1).Position)("satuan").ToString) Then
                    posisi = i
                    Exit For
                End If
            Next

            Dim jmljualreal As Integer = 0

            Dim dta As DataTable = dv1.ToTable
            Dim rows() As DataRow = dta.Select(String.Format("kd_barang='{0}' and satuan='{1}'", kdbarang_real, dv1(Me.BindingContext(dv1).Position)("satuan").ToString))
            If rows.Length > 0 Then
                jmljualreal = Integer.Parse(rows(0)("qty").ToString)
            End If

            Using fkar2 As New ffaktur_to4 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .dvkembali = dv2, .addstat = False, .position = Me.BindingContext(dv1).Position, .position_kembali = posisi, .jmlfisik_real = jmljualreal, .jenisjual = jenisjual, .spulang = spulang}
                fkar2.ShowDialog(Me)
            End Using

        End If

        totalnetto()

        If SplitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both Then

            SimpleButton1.Focus()
        Else
            tnote.Focus()
        End If

    End Sub

    Public Function cek_kode_kembali(ByVal kdbarang As String) As String

        Dim cn As OleDbConnection = Nothing
        Dim hasil As String = ""

        kdbarang_real = ""

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select kd_barang_kmb,kd_barang from ms_barang where kd_barang_jmn='{0}'", kdbarang)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Not drd(0).ToString.Equals("") Then
                    hasil = drd(0).ToString
                    kdbarang_real = drd(1).ToString
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

        Return hasil

    End Function



    Private Sub btdel_Click(sender As System.Object, e As System.EventArgs) Handles btdel.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        If dv1(Me.BindingContext(dv1).Position)("jenis_trans").ToString.Equals("KEMBALI") Then
            MsgBox("Barang Kembali tidak dapat dihapus, hapus 19 Ltr-nya...", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If Not dv1(Me.BindingContext(dv1).Position)("jenis").ToString.Equals("FISIK") Then

            If cek_brgjaminan(Nothing, Nothing, dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString) = True Then

                Dim orw1() As DataRow = dv1.Table.Select(String.Format("kd_barang='{0}'", dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString))
                If orw1.Length = 1 Then
                    MsgBox("Jaminan barang tidak dapat dihapus, hapus 19 Ltr-nya...", vbOKOnly + vbInformation, "Informasi")
                    Return
                End If

            End If

        End If


        If addstat = True Then

            Dim kdbar As String = dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString
            Dim jenis As String = dv1(Me.BindingContext(dv1).Position)("jenis").ToString

            If cek_brgjaminan(Nothing, Nothing, dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString) = True Then

                Dim orw1() As DataRow = dv1.Table.Select(String.Format("kd_barang='{0}'", dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString))
                If orw1.Length > 1 Then
                    dv1.Delete(Me.BindingContext(dv1).Position)
                    tbrutto.EditValue = GridView5.Columns("jumlah").SummaryItem.SummaryValue
                End If

            Else

                If cekhapus_kembali(Nothing, Nothing, kdbar, tbukti.Text.Trim) = "ok" Then

                    If jenis = "FISIK" Then
                        cekhapus_non(Nothing, Nothing, kdbar, tbukti.Text.Trim)
                    End If

                    dv1.Delete(Me.BindingContext(dv1).Position)

                    tbrutto.EditValue = GridView5.Columns("jumlah").SummaryItem.SummaryValue

                End If


            End If

            If kdbar = "B0002" Or kdbar = "B0003" Then
                For i As Integer = 0 To dv1.Count - 1
                    If kdbar = "B0002" And dv1(i)("kd_barang").ToString = "BN0002" Then
                        dv1(i).Delete()
                        Return
                    ElseIf kdbar = "B0003" And dv1(i)("kd_barang").ToString = "BN0003" Then
                        dv1(i).Delete()
                        Return
                    End If
                Next
            End If
            

            '' end jika tambah
        Else

            If Integer.Parse(dv1(Me.BindingContext(dv1).Position)("noid").ToString) = 0 Then

                If cekhapus_kembali(Nothing, Nothing, dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString, tbukti.Text.Trim) = "ok" Then

                    If dv1(Me.BindingContext(dv1).Position)("jenis").ToString.Equals("FISIK") Then
                        cekhapus_non(Nothing, Nothing, dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString, tbukti.Text.Trim)
                    End If

                    dv1.Delete(Me.BindingContext(dv1).Position)

                    tbrutto.EditValue = GridView5.Columns("jumlah").SummaryItem.SummaryValue

                End If

                Return
            End If

            Dim cn As OleDbConnection = Nothing
            Dim sqltrans As OleDbTransaction = Nothing

            Try
                Dim qtykecil As Integer = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qtykecil").ToString)
                Dim kdbar As String = dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString
                Dim kdgud As String = dv1(Me.BindingContext(dv1).Position)("kd_gudang").ToString
                Dim jenis As String = dv1(Me.BindingContext(dv1).Position)("jenis").ToString
                Dim jenis_trans As String = dv1(Me.BindingContext(dv1).Position)("jenis_trans").ToString

                Dim qty1 As Integer = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qty1").ToString)
                Dim qty2 As Integer = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qty2").ToString)
                Dim qty3 As Integer = Integer.Parse(dv1(Me.BindingContext(dv1).Position)("qty3").ToString)


                cn = New OleDbConnection
                cn = Clsmy.open_conn

                sqltrans = cn.BeginTransaction

                Dim ifbarangjaminan As Boolean = False
                Dim brgada_jamin As Boolean = False
                If cek_brgada_jaminan(cn, sqltrans, dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString) = True Then
                    If Not jenisjual = "T" Then
                        cek_beli_pinjam_gallon(cn, sqltrans, True, False)
                        brgada_jamin = True
                    End If

                End If

                Dim bukanjaminan As Boolean = False
                If cek_brgjaminan(cn, sqltrans, dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString) = True Then
                    Dim orw1() As DataRow = dv1.Table.Select(String.Format("kd_barang='{0}'", dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString))
                    If orw1.Length > 0 Then
                        If brgada_jamin = False Then
                            ifbarangjaminan = True
                        End If
                    End If
                Else
                    bukanjaminan = True
                End If


                If bukanjaminan Then
                    If Not cekhapus_kembali(cn, sqltrans, kdbar, tbukti.Text.Trim) = "ok" Then
                        GoTo langsung
                    End If

                    If jenis = "FISIK" Then
                        cekhapus_non(cn, sqltrans, kdbar, tbukti.Text.Trim)
                    End If
                End If


                If Not jenisjual = "T" Then

                    If jenis = "FISIK" And jenis_trans.Equals("JUAL") Then
                        Dim hasilsimkos As String = simpankosong(cn, sqltrans, kdbar, tkd_toko.Text.Trim, qty1, qty2, qty3, qtykecil, False)
                        If Not hasilsimkos.Equals("ok") Then
                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox(hasilsimkos, vbOKOnly + vbExclamation, "Informasi")
                            GoTo langsung
                        End If

                        '2. update barang
                        Dim hasilplusmin As String = PlusMin_Barang_Kend(cn, sqltrans, qtykecil, kdbar, kdgud, True, False, False)
                        If Not hasilplusmin.Equals("ok") Then

                            If Not IsNothing(sqltrans) Then
                                sqltrans.Rollback()
                            End If

                            MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                            GoTo langsung
                        End If


                        If addstat = False Then

                            If DateValue(tgl_old) <> DateValue(ttgl.EditValue) Then
                                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, kdgud, kdbar, 0, qtykecil, "Jual Kanvas (Edit)", dv_spm(0)("kd_supir").ToString, dv_spm(0)("nopol").ToString)
                            End If

                        End If

                        '3. insert to hist stok
                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, kdgud, kdbar, qtykecil, 0, "Jual Kanvas (Edit)", dv_spm(0)("kd_supir").ToString, dv_spm(0)("nopol").ToString)

                    End If

                Else

                    If spulang = True Then

                        '2. update barang
                        'Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, True, False, False)
                        'If Not hasilplusmin.Equals("ok") Then

                        '    If Not IsNothing(sqltrans) Then
                        '        sqltrans.Rollback()
                        '    End If

                        '    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        '    GoTo langsung
                        'End If

                        'If addstat = False Then

                        '    If jenis = "FISIK" Then

                        '        If Not tglkembali_old = "" Then
                        '            If DateValue(tgl_old) <> DateValue(ttgl.EditValue) Then
                        '                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tglkembali_old, kdgud, kdbar, 0, qtykecil, "Jual To (Realisasi)", dv_spm(0)("kd_supir").ToString, dv_spm(0)("nopol").ToString)
                        '            End If
                        '        End If

                        '        '3. insert to hist stok
                        '        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl_kembali.EditValue, kdgud, kdbar, qtykecil, 0, "Jual To (Realisasi)", dv_spm(0)("kd_supir").ToString, dv_spm(0)("nopol").ToString)

                        '    End If
                        'End If

                    End If


                End If


                Dim mskcek_beligln As Boolean = False
                If ifbarangjaminan = True Then
                    If Not jenisjual = "T" Then
                        cek_beli_pinjam_gallon(cn, sqltrans, True, True)
                        mskcek_beligln = True
                    End If
                End If

                If mskcek_beligln = False Then

                    Dim sql As String = String.Format("delete from trfaktur_to2 where noid={0}", dv1(Me.BindingContext(dv1).Position)("noid").ToString)

                    Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                    cmd.ExecuteNonQuery()

                    dv1.Delete(Me.BindingContext(dv1).Position)
                End If

                tbrutto.EditValue = GridView5.Columns("jumlah").SummaryItem.SummaryValue

                ' update header------------------------------------------------------------

                '2. update piutang toko
                Dim sqlct As String = String.Format("select netto,kd_toko,kd_karyawan from trfaktur_to where nobukti='{0}'", tbukti.Text.Trim)

                Dim cmdt As OleDbCommand = New OleDbCommand(sqlct, cn, sqltrans)
                Dim drdt As OleDbDataReader = cmdt.ExecuteReader

                If drdt.Read Then
                    If IsNumeric(drdt("netto").ToString) Then

                        Dim nett_sebelum As Double = drdt("netto").ToString
                        Dim kdsales_sebelum As String = drdt("kd_karyawan").ToString

                        'Dim sqluptoko As String = String.Format("update ms_toko set piutangbeli=piutangbeli - {0} where kd_toko='{1}'", Replace(nett_sebelum, ",", "."), tkd_toko.Text.Trim)
                        'Dim sqluptoko2 As String = String.Format("update ms_toko set piutangbeli=piutangbeli + {0} where kd_toko='{1}'", Replace(tnetto.EditValue, ",", "."), tkd_toko.Text.Trim)

                        'Dim cmdtk As OleDbCommand = New OleDbCommand(sqluptoko, cn, sqltrans)
                        'cmdtk.ExecuteNonQuery()

                        'Dim cmdtk2 As OleDbCommand = New OleDbCommand(sqluptoko2, cn, sqltrans)
                        'cmdtk2.ExecuteNonQuery()

                        Dim sqluptoko21 As String = String.Format("update ms_toko2 set jmlpiutang=jmlpiutang - {0} where kd_toko='{1}' and kd_karyawan='{2}'", Replace(nett_sebelum, ",", "."), tkd_toko.Text.Trim, kdsales_sebelum)
                        Dim sqluptoko222 As String = String.Format("update ms_toko2 set jmlpiutang=jmlpiutang + {0} where kd_toko='{1}' and kd_karyawan='{2}'", Replace(tnetto.EditValue, ",", "."), tkd_toko.Text.Trim, tkd_sales.Text.Trim)

                        Using cmdtok21 As OleDbCommand = New OleDbCommand(sqluptoko21, cn, sqltrans)
                            cmdtok21.ExecuteNonQuery()
                        End Using

                        Using cmdtok22 As OleDbCommand = New OleDbCommand(sqluptoko222, cn, sqltrans)
                            cmdtok22.ExecuteNonQuery()
                        End Using

                        Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, tbukti.Text.Trim, tbukti.Text.Trim, ttgl.EditValue, tkd_toko.Text.Trim, 0, nett_sebelum, "Jual (Edit)")
                        Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, tbukti.Text.Trim, tbukti.Text.Trim, ttgl.EditValue, tkd_toko.Text.Trim, tnetto.EditValue, 0, "Jual (Edit)")

                    End If
                End If
                drdt.Close()

                '1. update faktur
                Dim sqlup_faktur As String = String.Format("update trfaktur_to set disc_per={0},disc_rp={1},brutto={2},netto={3},jmlkembali={4},jmlretur={5},jmlkelebihan={6} where nobukti='{7}'", Replace(tdisc_per.EditValue, ",", "."), Replace(tdisc_rp.EditValue, ",", "."), Replace(tbrutto.EditValue, ",", "."), Replace(tnetto.EditValue, ",", "."), Replace(ttot_kembali.EditValue, ",", "."), Replace(ttot_retur.EditValue, ",", "."), Replace(tlebih.EditValue, ",", "."), tbukti.Text.Trim)

                Using cmdupfaktur As OleDbCommand = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                    cmdupfaktur.ExecuteNonQuery()
                End Using


                ' akhir update header ---------------------------------------------------

                If kdbar = "B0002" Or kdbar = "B0003" Then
                    For i As Integer = 0 To dv1.Count - 1
                        If kdbar = "B0002" And dv1(i)("kd_barang").ToString = "BN0002" Then

                            Dim sqldelu As String = String.Format("delete from trfaktur_to2 where nobukti='{0}' and kd_barang='BN0002'", tbukti.EditValue)
                            Using cmddelu As OleDbCommand = New OleDbCommand(sqldelu, cn, sqltrans)
                                cmddelu.ExecuteNonQuery()
                            End Using

                            dv1(i).Delete()
                            GoTo langsung_comit
                        ElseIf kdbar = "B0003" And dv1(i)("kd_barang").ToString = "BN0003" Then

                            Dim sqldelu As String = String.Format("delete from trfaktur_to2 where nobukti='{0}' and kd_barang='BN0003'", tbukti.EditValue)
                            Using cmddelu As OleDbCommand = New OleDbCommand(sqldelu, cn, sqltrans)
                                cmddelu.ExecuteNonQuery()
                            End Using

                            dv1(i).Delete()
                            GoTo langsung_comit
                        End If
                    Next
                End If
langsung_comit:

                sqltrans.Commit()

                MsgBox("Data dihapus...", vbOKOnly + vbInformation, "Informasi")

langsung:

            Catch ex As Exception

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


        End If

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click

        If statsebelumnya = True Then
            statbalik = True
        Else
            statbalik = False
        End If

        Me.Close()
    End Sub

    Private Sub ffaktur_to2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If jenisjual.Equals("T") Then
            tnota.Focus()
        Else
            tnota.Focus()
        End If
    End Sub

    Private Sub ffaktur_to2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        'isi_gudang()
        'isi_alasan()

        r_datagudang()
        r_databarang()

        '  isi_gudang_detail()

        '  isi_barang()

        ttgl_kembali.Properties.ReadOnly = True

        If jenisjual = "T" Then
            Me.Text = "Faktur TO"

            rjenis_barang.Items.Clear()
            rjenis_barang.Items.Add("JUAL")

            If spulang = True Then
                rjenis_barang.Items.Clear()
                rjenis_barang.Items.Add("JUAL")
                rjenis_barang.Items.Add("PINJAM")
                rjenis_barang.Items.Add("KEMBALI")
            End If

        Else
            Me.Text = "Faktur Kanvas"

            rjenis_barang.Items.Clear()
            rjenis_barang.Items.Add("JUAL")
            rjenis_barang.Items.Add("PINJAM")
            rjenis_barang.Items.Add("KEMBALI")

            btload_bukti_Click(sender, Nothing)

            SplitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both

        End If

        If addstat = True Then

            'isi_gudang()


            ttgl.EditValue = Date.Now
            ttgl_tempo.EditValue = Date.Now

            ttgl_kembali.EditValue = Date.Now

            cbjenis.SelectedIndex = 1

            kosongkan()

            If jenisjual = "T" Then

                tgudang.EditValue = "None"
                tgudang.Properties.ReadOnly = True

            Else

                tgudang.EditValue = "None"
                tgudang.Properties.ReadOnly = True

                tnota.Enabled = True

            End If

        Else


            isi()

            If Not jenisjual = "T" Then

                tgudang.EditValue = kdgud_edit
                tgudang.Properties.ReadOnly = True

                '   tnota.Enabled = False

            Else

                If spulang = True Then

                    If kdgud_edit = "None" Then

                        Dim rows() As DataRow = dtgudang.Select("def_kosong='1'")

                        If rows.Length > 0 Then
                            tgudang.EditValue = rows(0)("kd_gudang").ToString
                        End If

                        tgudang.Properties.ReadOnly = False

                    Else

                        tgudang.EditValue = kdgud_edit
                        tgudang.Properties.ReadOnly = True

                    End If




                Else

                    tgudang.EditValue = "None"
                    tgudang.Properties.ReadOnly = True

                End If

            End If



        End If
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tkd_toko.Text.Trim.Length = 0 Then
            MsgBox("Outlet harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tkd_toko.Focus()
            Return
        End If

        If tkd_sales.Text.Trim.Length = 0 Then
            MsgBox("Outlet harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tkd_toko.Focus()
            Return
        End If

        'If IsNothing(dv1) Then
        '    MsgBox("Tidak ada barang yang akan dijual", vbOKOnly + vbInformation, "Informasi")
        '    Return
        'End If

        'If dv1.Count <= 0 Then
        '    MsgBox("Tidak ada barang yang akan dijual", vbOKOnly + vbInformation, "Informasi")
        '    Return
        'End If

        'If Not IsNothing(dv1) Then
        '    GoTo lanjut
        'End If

        If dv1.Count > 0 Then
            GoTo lanjut
        End If

        If jenisjual = "T" Then
            If spulang Then
                GoTo lanjut
            ElseIf addstat = False Then
                GoTo lanjut
            End If
        Else
            GoTo lanjut
        End If

        Dim sql As String = "select * from ms_barang where not(len(kd_barang_jmn)=0) or kd_barang in (" & _
            "select kd_barang_jmn from ms_barang where not(len(kd_barang_jmn)=0)) order by nohrus"

        Dim sqlc As String = String.Format("select spred_to  from ms_toko where kd_toko='{0}'", tkd_toko.Text.Trim)

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn)
            Dim drdc As OleDbDataReader = cmdc.ExecuteReader

            Dim ada As Boolean = False

            If drdc.Read Then
                If drdc(0).ToString.Equals("1") Then
                    ada = True
                End If
            End If
            drdc.Close()

            If ada = False Then
                MsgBox("Tidak ada barang yang akan dijual", vbOKOnly + vbInformation, "Informasi")
                Return
            End If

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            Dim i As Integer = 0
            While drd.Read

                Dim orow As DataRowView = dv1.AddNew

                orow("noid") = 0
                orow("kd_gudang") = "G000"
                orow("nama_gudang") = ""
                orow("kd_barang") = drd("kd_barang").ToString
                orow("nama_barang") = drd("nama_barang").ToString
                orow("qty") = 0
                orow("qty0") = 0
                orow("satuan") = drd("satuan1").ToString
                orow("disc_per") = 0
                orow("disc_rp") = 0
                orow("jumlah") = 0
                orow("qtykecil") = 0
                orow("qtykecil0") = 0
                orow("hargakecil") = 0
                orow("jumlah0") = 0
                orow("harga") = 0

                orow("jenis") = drd("jenis").ToString
                orow("jenis_trans") = "JUAL"

                orow("qty1") = drd("qty1").ToString
                orow("qty2") = drd("qty2").ToString
                orow("qty3") = drd("qty2").ToString

                dv1.EndInit()

            End While

            Dim sql1 As String = "select * from ms_barang where kd_barang in (select kd_barang_kmb from ms_barang where not(kd_barang_kmb is NULL))"
            Dim cmd1 As OleDbCommand = New OleDbCommand(sql1, cn)
            Dim drd1 As OleDbDataReader = cmd1.ExecuteReader

            If drd1.Read Then
                If Not drd1("kd_barang").ToString.Equals("") Then

                    Dim orow1 As DataRowView = dv2.AddNew

                    orow1("kd_barang") = drd1("kd_barang").ToString
                    orow1("nama_barang") = drd1("nama_barang").ToString
                    orow1("qty") = 0
                    orow1("satuan") = drd1("satuan1").ToString

                    orow1("jumlah") = 0
                    orow1("qtykecil") = 0
                    orow1("hargakecil") = 0
                    orow1("hargajual") = 0

                    orow1("qty1") = 0
                    orow1("qty2") = 0
                    orow1("qty3") = 0

                    orow1("jenis_trans") = "JUAL"

                    orow1("satuan1") = drd1("satuan1").ToString
                    orow1("satuan2") = drd1("satuan2").ToString
                    orow1("satuan3") = drd1("satuan3").ToString

                    dv2.EndInit()

                End If
            End If

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Exit Sub
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try


lanjut:


        'If ttot_retur.EditValue > 0 Then
        '    If talasan.Text.Trim.Length = 0 Then
        '        MsgBox("Alasan retur harus diisi..", vbOKOnly + vbInformation, "Informasi")
        '        tabtransaksi.SelectedTabPageIndex = 1
        '        talasan.Focus()
        '    End If
        'End If


        If tnetto.EditValue > tsisalimit.EditValue Then
            MsgBox("Jumlah penjualan melebihi sisa limit..", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If


        If SplitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both Then

            Dim dts As DataTable = dv1.ToTable
            Dim hsrow As DataRow() = dts.Select(String.Format("kd_barang='{0}'", "JM001"))

            Dim dts2 As DataTable = dv1.ToTable
            Dim hsrow2 As DataRow() = dts2.Select(String.Format("kd_barang='{0}'", "G0003"))

            Dim jmljual As Integer = 0
            If hsrow.Length > 0 Then
                For i As Integer = 0 To hsrow.Length - 1
                    jmljual = jmljual + Integer.Parse(hsrow(i)("qtykecil").ToString)
                Next
            End If

            If hsrow.Length > 0 Then
                If hsrow2.Length > 0 Then
                    If Integer.Parse(jmljual) < Integer.Parse(hsrow2(0)("qtykecil").ToString) Then
                        MsgBox("Jumlah kembali tidak boleh lebih besar dari jml kirim,masukkan diretur..", vbOKOnly + vbInformation, "Informasi")
                        Return
                    End If
                End If
            End If

        End If

        If Double.Parse(tnetto.EditValue) < 0 Then
            MsgBox("Netto tidak boleh minus...", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If jenisjual.Equals("T") Then

            If spulang = False Then
                If cek_initial_pajak() = False Then
                    MsgBox("Toko yang akan disimpan adalah PKP (Pajak), anda harus memakai user dengan initial F", vbOKOnly + vbExclamation, "Informasi")
                    tkd_toko.Focus()
                    Return
                End If
            End If

        End If

        For i As Integer = 0 To dv1.Count - 1
            If Not (dv1(i)("jenis_trans").ToString.Equals("JUAL") Or dv1(i)("jenis_trans").ToString.Equals("PINJAM") Or dv1(i)("jenis_trans").ToString.Equals("KEMBALI")) Then
                MsgBox("Jenis transaksi pada detail barang salah, periksa kembali...", vbExclamation + vbInformation, "Konfirmasi")
                Return
            End If
        Next

        If MsgBox("Yakin sudah benar.. ???", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.No Then
            Return
        Else
            simpan()
        End If

    End Sub

    Private Sub cekjmlprint()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqlupprint As String = String.Format("update trfaktur_to set jmlprint=jmlprint+1 where nobukti='{0}'", tbukti.Text.Trim)
            Using cmdupprint As OleDbCommand = New OleDbCommand(sqlupprint, cn)
                cmdupprint.ExecuteNonQuery()
            End Using

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

    Private Sub langsungprint()

        Dim sql1 As String = String.Format("SELECT     trfaktur_to.nobukti, trfaktur_to.tanggal, trfaktur_to.tgl_tempo, ms_toko.kd_toko +' - ' + ms_toko.nama_toko as nama_toko, ms_toko.alamat_toko, ms_pegawai.nama_karyawan + case when len(ms_pegawai.notelp1)>0 then '('+ms_pegawai.notelp1+')' else '' end as nama_karyawan, trfaktur_to.jnis_jual, " & _
                     "trfaktur_to.brutto - trfaktur_to.jmlkembali AS brutto, trfaktur_to.disc_rp AS disc_tot, trfaktur_to.netto,trfaktur_to.ongkos_angkut, " & _
                     "case when LEN(ms_toko.notelp1)>0 then " & _
                    "ms_toko.notelp1 else '' end + " & _
                    "case when LEN(ms_toko.notelp2)>0 then " & _
                    "case when LEN(ms_toko.notelp1)>0 then " & _
                    "' , '+ms_toko.notelp2 else ms_toko.notelp2 end " & _
                    "else '' end + " & _
                    "case when LEN(ms_toko.notelp3) > 0 then " & _
                    "case when LEN(ms_toko.notelp1) > 0 or LEN(ms_toko.notelp2)> 0 then " & _
                    "' , ' + ms_toko.notelp3 else ms_toko.notelp3 end " & _
                    "else '' end as notelp_toko,trfaktur_to.ket " & _
                   "FROM         trfaktur_to INNER JOIN " & _
                     "ms_pegawai ON trfaktur_to.kd_karyawan = ms_pegawai.kd_karyawan INNER JOIN " & _
                     "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko " & _
                   "WHERE     (trfaktur_to.sbatal = 0) AND (trfaktur_to.nobukti = '{0}')", tbukti.Text.Trim)

        Dim sql2 As String = String.Format("SELECT ms_barang.nama_barang, trfaktur_to3.qty, trfaktur_to3.satuan, trfaktur_to3.harga, trfaktur_to3.jumlah, trfaktur_to3.nobukti " & _
        "FROM   trfaktur_to3 INNER JOIN " & _
        "ms_barang ON trfaktur_to3.kd_barang = ms_barang.kd_barang WHERE trfaktur_to3.nobukti='{0}'", tbukti.Text.Trim)

        Dim sql3 As String = "select kd_barang,case when kd_barang='G0003' then nama_barang+' (JUAL)' else nama_barang end as nama_barang,nohrus,0 as qty,'' as satuan,0 as harga,0 as disc,0 as jumlah from ms_barang where hrusfaktur=1"

        Dim sqldetail As String = String.Format("select * from trfaktur_to2 where nobukti='{0}'", tbukti.Text.Trim)

        Dim sqlbonus As String = String.Format("select trfaktur_to.nobukti, trfaktur_to.tanggal, " & _
        "ms_toko.kd_toko +' - ' + ms_toko.nama_toko as nama_toko, ms_toko.alamat_toko,  " & _
        "ms_pegawai.nama_karyawan + case when len(ms_pegawai.notelp1)>0 then '('+ms_pegawai.notelp1+')' else '' end as nama_karyawan, " & _
        "case when LEN(ms_toko.notelp1)>0 then " & _
        "ms_toko.notelp1 else '' end + " & _
        "case when LEN(ms_toko.notelp2)>0 then " & _
        "case when LEN(ms_toko.notelp1)>0 then " & _
        "' , '+ms_toko.notelp2 else ms_toko.notelp2 end " & _
        "else '' end + " & _
        "case when LEN(ms_toko.notelp3) > 0 then " & _
        " case when LEN(ms_toko.notelp1) > 0 or LEN(ms_toko.notelp2)> 0 then " & _
        "' , ' + ms_toko.notelp3 else ms_toko.notelp3 end " & _
        "else '' end as notelp_toko, " & _
        "ms_barang.kd_barang, ms_barang.nama_lap as nama_barang, trfaktur_to2.qtykecil as jml " & _
        "from trfaktur_to inner join trfaktur_to2 on trfaktur_to.nobukti=trfaktur_to2.nobukti " & _
        "inner join ms_toko on trfaktur_to.kd_toko=ms_toko.kd_toko " & _
        "inner join ms_pegawai on trfaktur_to.kd_karyawan=ms_pegawai.kd_karyawan " & _
        "inner join ms_barang on trfaktur_to2.kd_barang=ms_barang.kd_barang " & _
        "where ms_barang.kd_barang in ('BN0002','BN0003') and trfaktur_to.sbatal=0 and trfaktur_to.nobukti='{0}'", tbukti.Text.Trim)


        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New dsinvoice
            ds = Clsmy.GetDataSet(sql1, cn)

            Dim dsinvoice_ku As DataSet
            dsinvoice_ku = New DataSet
            dsinvoice_ku = ds

            Dim ds2 As DataSet = New dsinvoice2
            ds2 = Clsmy.GetDataSet(sql2, cn)

            Dim dsbonus As DataSet = New ds_bonus
            dsbonus = Clsmy.GetDataSet(sqlbonus, cn)

            Dim ds3 As DataSet = New dsbarang_to
            ds3 = Clsmy.GetDataSet(sql3, cn)

            Dim dsdetail As DataSet = New DataSet
            dsdetail = Clsmy.GetDataSet(sqldetail, cn)

            Dim dtdetail As DataTable = dsdetail.Tables(0)

            For i As Integer = 0 To dtdetail.Rows.Count - 1

                Dim kdbar As String = dtdetail(i)("kd_barang").ToString
                Dim qty As Integer = Integer.Parse(dtdetail(i)("qty").ToString)
                Dim satuan As String = dtdetail(i)("satuan").ToString
                Dim harga As Integer = Double.Parse(dtdetail(i)("harga").ToString)
                Dim disc As Integer = Double.Parse(dtdetail(i)("disc_rp").ToString)
                Dim jumlah As Integer = Double.Parse(dtdetail(i)("jumlah").ToString)

                For x As Integer = 0 To ds3.Tables(0).Rows.Count - 1

                    If ds3.Tables(0).Rows(x)("kd_barang").ToString.Equals(kdbar) Then

                        ds3.Tables(0).Rows(x)("qty") = qty
                        ds3.Tables(0).Rows(x)("satuan") = satuan
                        ds3.Tables(0).Rows(x)("harga") = harga
                        ds3.Tables(0).Rows(x)("disc") = disc
                        ds3.Tables(0).Rows(x)("jumlah") = jumlah

                        Exit For

                    End If

                Next

            Next



            Dim ops As New System.Drawing.Printing.PrinterSettings

            ' Dim rinvoice2 As New r_invoice2

            'rinvoice2.DataSource = ds2.Tables(0)
            'rinvoice2.DataMember = rinvoice2.DataMember
            '  rinvoice2.PrinterName = ops.PrinterName
            '  rinvoice2.CreateDocument(True)

            'PrintControl1.PrintingSystem = rinvoice2.PrintingSystem

            Dim rinvoice As New r_invoice() With {.DataSource = ds.Tables(0)}
            rinvoice.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
            rinvoice.DataMember = rinvoice.DataMember

            rinvoice.XrSubreport1.ReportSource = New r_invoice2
            rinvoice.XrSubreport1.ReportSource.DataSource = ds2.Tables(0)
            rinvoice.XrSubreport1.ReportSource.DataMember = rinvoice.XrSubreport1.ReportSource.DataMember

            rinvoice.XrSubreport2.ReportSource = New r_invoice3
            rinvoice.XrSubreport2.ReportSource.DataSource = ds3.Tables(0)
            rinvoice.XrSubreport2.ReportSource.DataMember = rinvoice.XrSubreport2.ReportSource.DataMember


            rinvoice.PrinterName = varprinter1
            rinvoice.CreateDocument()

            Dim jmljual As Integer = 0
            For i As Integer = 0 To dsbonus.Tables(0).Rows.Count - 1
                jmljual = jmljual + Integer.Parse(dsbonus.Tables(0).Rows(i)("jml").ToString)
            Next

            If jmljual > 0 Then
                Dim rbonus As New r_invoice_bns() With {.DataSource = dsbonus.Tables(0)}
                rbonus.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
                rbonus.DataMember = rinvoice.DataMember

                rbonus.CreateDocument()

                rinvoice.Pages.AddRange(rbonus.Pages)
                rinvoice.PrintingSystem.ContinuousPageNumbering = True

            End If

            rinvoice.Print()


        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

        cekjmlprint()

    End Sub

    Private Sub tdisc_per_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tdisc_per.Validated

        'If Not IsNothing(dv1) Then

        '    Dim adabonus As Boolean = False
        '    For i As Integer = 0 To dv1.Count - 1
        '        If dv1(i)("kd_barang").ToString = "BN0002" Or dv1(i)("kd_barang").ToString = "BN0003" Then
        '            adabonus = True
        '        End If
        '    Next

        '    If adabonus = True Then
        '        tdisc_per.EditValue = 0
        '    End If

        'End If


        If tdisc_per.EditValue > 0 Then

            Dim brutto As Double = tbrutto.EditValue
            Dim disc As Double = tdisc_per.EditValue / 100
            Dim hasil As Double = brutto * disc

            tdisc_rp.EditValue = hasil
        Else
            tdisc_rp.EditValue = 0
        End If

        totalnetto()

    End Sub

    Private Sub tdisc_rp_Validated(sender As Object, e As System.EventArgs) Handles tdisc_rp.Validated

        'If Not IsNothing(dv1) Then

        '    Dim adabonus As Boolean = False
        '    For i As Integer = 0 To dv1.Count - 1
        '        If dv1(i)("kd_barang").ToString = "BN0002" Or dv1(i)("kd_barang").ToString = "BN0003" Then
        '            adabonus = True
        '        End If
        '    Next

        '    If adabonus = True Then
        '        tdisc_rp.EditValue = 0
        '    End If

        'End If
        

        If tdisc_rp.EditValue > 0 Then

            Dim brutto As Double = tbrutto.EditValue
            Dim disc As Double = tdisc_rp.EditValue
            Dim hasil As Double = (disc / brutto) * 100

            tdisc_per.EditValue = hasil
        Else
            tdisc_per.EditValue = 0.0
        End If

        totalnetto()

    End Sub

    Private Sub tbrutto_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbrutto.EditValueChanged
        tdisc_per_EditValueChanged(sender, Nothing)
        totalnetto()
    End Sub

    Private Sub GridView2_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs)

        Dim qty As Integer

        If spulang = False Then
            If jenisjual = "T" Then
                dv2(Me.BindingContext(dv2).Position)("jumlah") = 0
                qty = 0
            Else
                qty = Integer.Parse(dv2(Me.BindingContext(dv2).Position)("qty").ToString)
            End If
        Else
            qty = Integer.Parse(dv2(Me.BindingContext(dv2).Position)("qty").ToString)
        End If

        Dim hargajual As Double = Double.Parse(dv2(Me.BindingContext(dv2).Position)("hargajual").ToString)

        Dim satuan1 As String = dv2(Me.BindingContext(dv2).Position)("satuan1").ToString
        Dim satuan2 As String = dv2(Me.BindingContext(dv2).Position)("satuan2").ToString
        Dim satuan3 As String = dv2(Me.BindingContext(dv2).Position)("satuan3").ToString

        Dim satuan As String = dv2(Me.BindingContext(dv2).Position)("satuan").ToString

        Dim vqty1 As Integer = Integer.Parse(dv2(Me.BindingContext(dv2).Position)("qty1").ToString)
        Dim vqty2 As Integer = Integer.Parse(dv2(Me.BindingContext(dv2).Position)("qty2").ToString)
        Dim vqty3 As Integer = Integer.Parse(dv2(Me.BindingContext(dv2).Position)("qty3").ToString)

        Dim jumlah As Double = 0
        Dim kqty As Integer = 0
        Dim hargakecil As Integer = 0

        If hargajual > 0 Then
            hargakecil = hargajual / vqty2
            hargakecil = hargakecil / vqty3
        End If

        If satuan.Equals(satuan1) Then
            kqty = (vqty1 * vqty2 * vqty3) * qty
        ElseIf satuan.Equals(satuan2) Then
            kqty = vqty3 * qty
        ElseIf satuan.Equals(satuan3) Then
            kqty = qty
        End If

        jumlah = hargakecil * kqty

        dv2(Me.BindingContext(dv2).Position)("qty") = qty
        dv2(Me.BindingContext(dv2).Position)("jumlah") = jumlah
        dv2(Me.BindingContext(dv2).Position)("qtykecil") = kqty
        dv2(Me.BindingContext(dv2).Position)("hargakecil") = hargakecil

        totalnetto()

    End Sub

    Private Sub ttgl_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles ttgl.EditValueChanged

        If Not IsDate(ttgl.EditValue) Then
            Return
        End If

        If Not jenisjual = "T" Then

            If spulang = True Then
                ttgl_kembali.EditValue = ttgl.EditValue
            End If

        Else

            If addstat = True And spulang = False Then
                ttgl_kembali.EditValue = ttgl.EditValue
            End If

        End If

        If cbjenis.EditValue = "KREDIT" Then

            If ttop.EditValue = 0 Then
                ttgl_tempo.EditValue = DateAdd(DateInterval.Day, 30, ttgl.EditValue)
            Else
                ttgl_tempo.EditValue = DateAdd(DateInterval.Day, ttop.EditValue, ttgl.EditValue)
            End If

        Else
            ttgl_tempo.EditValue = ttgl.EditValue
        End If

    End Sub

    Private Sub cbjenis_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cbjenis.SelectedIndexChanged

        If jenisjual.Equals("T") Then
            If spulang = True Then
                Return
            End If
        End If

        If cbjenis.EditValue = "KREDIT" Then
            ttgl_tempo.EditValue = DateAdd(DateInterval.Day, 30, ttgl.EditValue)
        Else
            ttgl_tempo.EditValue = ttgl.EditValue
        End If
    End Sub

    Private Sub ttgl_LostFocus(sender As Object, e As System.EventArgs) Handles ttgl.LostFocus

        If IsDate(ttgl.EditValue) Then
            'If DateValue(ttgl.EditValue) >= DateValue('01/01/1990") Then
            isi_nospm()
            'End If
        End If

    End Sub

    Private Sub btload_bukti_Click(sender As System.Object, e As System.EventArgs)

        'If btload_bukti.Text.Trim.Equals("No Bukti SPM <<") Then

        '    Me.Width = 880
        '    Me.Height = 520

        '    btload_bukti.Text = "No Bukti SPM >>"
        'Else

        '    Me.Width = 880
        '    Me.Height = 556

        '    btload_bukti.Text = "No Bukti SPM <<"

        'End If

    End Sub

    'Private Sub r_barang1_EditValueChanged(sender As Object, e As System.EventArgs) Handles r_barang1.EditValueChanged, r_barang2.EditValueChanged

    '    r_satuan.Items.Clear()

    '    With r_satuan.Items
    '        '.Add(dv_barang(Me.BindingContext(dv_barang).Position)("satuan1").ToString)
    '        '.Add(dv_barang(Me.BindingContext(dv_barang).Position)("satuan2").ToString)
    '        '.Add(dv_barang(Me.BindingContext(dv_barang).Position)("satuan3").ToString)
    '    End With

    'End Sub

    'Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

    '    If e.Column.FieldName.Equals("kd_barang") Or e.Column.FieldName.Equals("nama_barang") Then

    '        If dv1(Me.BindingContext(dv1).Position)("kd_gudang").ToString.Equals("") Then
    '            dv1(Me.BindingContext(dv1).Position)("kd_gudang") = "G000"
    '        End If



    '    End If


    'End Sub

    Private Sub tongkos_Validating(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles tongkos.Validating
        totalnetto()
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click

        If IsNothing(dv2) Then
            Return
        End If

        If dv2.Count <= 0 Then
            Return
        End If

        Dim gudangdefault As String = ""
        If Not jenisjual = "T" Then
            gudangdefault = cek_gudang_mobil_default()

            If gudangdefault.ToString.Trim.Length = 0 Then
                MsgBox("Gudang mobil tidak ditemukan...", vbOKOnly + vbInformation, "Informasi")
                Return
            End If

        End If

        Using fkar2 As New ffaktur_to5 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv2, .addstat = False, .position = Me.BindingContext(dv2).Position, .jenisjual = jenisjual, .spulang = spulang, .satuan = dv2(Me.BindingContext(dv2).Position)("satuan").ToString, .harga = dv2(Me.BindingContext(dv2).Position)("hargajual").ToString}
            fkar2.ShowDialog(Me)

            Dim dts As DataTable = dv1.ToTable
            Dim hsrow As DataRow() = dts.Select(String.Format("kd_barang='{0}'", "G0001"))

            If hsrow.Length > 0 Then
                Dim jml As Integer = Integer.Parse(hsrow(0)("qtykecil").ToString)
                Dim jmlbalik As Integer = Integer.Parse(dv2(Me.BindingContext(dv2).Position)("qtykecil").ToString)

                Dim selisih As Integer = 0

                If jmlbalik > jml Then
                    selisih = jmlbalik - jml
                End If

                If selisih > 0 Then

                    Dim vqty1 As Integer = Integer.Parse(dv2(Me.BindingContext(dv2).Position)("qty1").ToString)
                    Dim vqty2 As Integer = Integer.Parse(dv2(Me.BindingContext(dv2).Position)("qty2").ToString)
                    Dim vqty3 As Integer = Integer.Parse(dv2(Me.BindingContext(dv2).Position)("qty3").ToString)

                    Dim hargajual As Double = dv2(Me.BindingContext(dv2).Position)("hargajual").ToString


                    Dim satuan As String = dv2(Me.BindingContext(dv2).Position)("satuan").ToString

                    Dim satuan1 As String = dv2(Me.BindingContext(dv2).Position)("satuan1").ToString
                    Dim satuan2 As String = dv2(Me.BindingContext(dv2).Position)("satuan2").ToString
                    Dim satuan3 As String = dv2(Me.BindingContext(dv2).Position)("satuan3").ToString

                    Dim qtyreal As Integer = jmlbalik - selisih
                    Dim qty = selisih / (vqty1 * vqty2 * vqty3)


                    Dim jumlah As Double = 0
                    Dim jumlahreal As Double = 0
                    Dim hargakecil As Integer = 0
                    Dim kqty As Integer = 0
                    Dim kqtyreal As Integer = 0

                    If hargajual > 0 Then
                        hargakecil = hargajual / vqty2
                        hargakecil = hargakecil / vqty3
                    Else
                        hargakecil = 0
                    End If

                    If satuan.Equals(satuan1) Then
                        kqty = (vqty1 * vqty2 * vqty3) * qty
                        kqtyreal = (vqty1 * vqty2 * vqty3) * qtyreal
                    ElseIf satuan.Equals(satuan2) Then
                        kqty = vqty3 * qty
                        kqtyreal = vqty3 * qtyreal
                    ElseIf satuan.Equals(satuan3) Then
                        kqty = qty
                        kqtyreal = qtyreal
                    End If

                    jumlah = hargakecil * kqty
                    jumlahreal = hargakecil * kqtyreal

                    dv2(Me.BindingContext(dv2).Position)("qty") = qtyreal
                    dv2(Me.BindingContext(dv2).Position)("qtykecil") = kqtyreal
                    dv2(Me.BindingContext(dv2).Position)("jumlah") = jumlahreal
                    dv2(Me.BindingContext(dv2).Position)("hargakecil") = hargakecil

                    Dim apaada As Boolean = False

                    For i As Integer = 0 To dv_ret.Count - 1
                        If dv_ret(i)("kd_barang").Equals(dv2(Me.BindingContext(dv2).Position)("kd_barang").ToString) Then

                            apaada = True

                            dv_ret(i)("qty") = qty
                            dv_ret(i)("qtykecil") = selisih
                            dv_ret(i)("satuan") = satuan
                            dv_ret(i)("disc_per") = 0
                            dv_ret(i)("disc_rp") = 0
                            dv_ret(i)("jumlah") = jumlah
                            dv_ret(i)("harga") = hargajual
                            dv_ret(i)("qty1") = vqty1
                            dv_ret(i)("qty2") = vqty2
                            dv_ret(i)("qty3") = vqty3

                            Exit For

                        End If
                    Next

                    If apaada = False Then

                        Dim orow As DataRowView = dv_ret.AddNew
                        orow("noid") = 0

                        If jenisjual.Equals("T") Then
                            orow("kd_gudang") = "G000"
                        Else
                            orow("kd_gudang") = gudangdefault
                        End If

                        orow("kd_barang") = dv2(Me.BindingContext(dv2).Position)("kd_barang").ToString
                        orow("nama_barang") = dv2(Me.BindingContext(dv2).Position)("nama_barang").ToString
                        orow("qty") = qty
                        orow("satuan") = satuan
                        orow("disc_per") = 0
                        orow("disc_rp") = 0
                        orow("jumlah") = jumlah
                        orow("qtykecil") = kqty
                        orow("harga") = hargajual
                        orow("qty1") = vqty1
                        orow("qty2") = vqty2
                        orow("qty3") = vqty3

                        dv.EndInit()

                    End If

                End If

            End If

            totalnetto()
            tnote.Focus()

        End Using

    End Sub

    Private Sub btadd_ret_Click(sender As System.Object, e As System.EventArgs)

        Using fkar2 As New ffaktur_to6 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv_ret, .addstat = True, .position = 0, .jenisjual = jenisjual, .kdsales = tkd_sales.Text.Trim}
            fkar2.ShowDialog(Me)
            totalnetto()
        End Using

    End Sub

    Private Sub btedit_ret_Click(sender As System.Object, e As System.EventArgs)

        If IsNothing(dv_ret) Then
            Return
        End If

        If dv_ret.Count <= 0 Then
            Return
        End If

        Using fkar2 As New ffaktur_to6 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv_ret, .addstat = False, .position = Me.BindingContext(dv_ret).Position, .jenisjual = jenisjual, .kdsales = tkd_sales.Text.Trim}
            fkar2.ShowDialog(Me)
            totalnetto()
        End Using

    End Sub

    '    Private Sub btdel_ret_Click(sender As System.Object, e As System.EventArgs) Handles btdel_ret.Click

    '        If IsNothing(dv_ret) Then
    '            Return
    '        End If

    '        If dv_ret.Count <= 0 Then
    '            Return
    '        End If

    '        If addstat = True Then
    '            dv_ret.Delete(Me.BindingContext(dv_ret).Position)
    '        Else

    '            Dim cn As OleDbConnection = Nothing
    '            Dim sqltrans As OleDbTransaction = Nothing

    '            Try

    '                If Integer.Parse(dv_ret(Me.BindingContext(dv_ret).Position)("noid").ToString) = 0 Then
    '                    dv_ret.Delete(Me.BindingContext(dv_ret).Position)
    '                    Return
    '                End If

    '                Dim qtykecil As Integer = Integer.Parse(dv_ret(Me.BindingContext(dv_ret).Position)("qtykecil").ToString)
    '                Dim kdbar As String = dv_ret(Me.BindingContext(dv_ret).Position)("kd_barang").ToString
    '                Dim kdgud As String = dv_ret(Me.BindingContext(dv_ret).Position)("kd_gudang").ToString

    '                cn = New OleDbConnection
    '                cn = Clsmy.open_conn

    '                sqltrans = cn.BeginTransaction

    '                Dim sqlc As String = String.Format("select * from trretur2 inner join ms_barang on trretur2.kd_barang=ms_barang.kd_barang where noid={0}", dv_ret(Me.BindingContext(dv_ret).Position)("noid").ToString)
    '                Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
    '                Dim drc As OleDbDataReader = cmdc.ExecuteReader

    '                If drc.Read Then
    '                    If IsNumeric(drc("noid").ToString) Then

    '                        Dim qtyold As Integer = Integer.Parse(drc("qtykecil").ToString)

    '                        If simpankosong_f(cn, sqltrans, kdbar, tkd_toko.Text.Trim, drc("qty1").ToString, drc("qty2").ToString, drc("qty3").ToString, qtyold, True) = True Then
    '                            '3. insert to hist stok
    '                            Clsmy.Insert_HistBarang(cn, sqltrans, tbukti_ret.Text.Trim, ttgl_tempo.EditValue, "None", kdbar, qtyold, 0, "Retur (Edit)", "", "")
    '                        End If

    '                    End If
    '                End If
    '                drc.Close()

    '                Dim sql As String = String.Format("delete from trretur2 where noid={0}", dv_ret(Me.BindingContext(dv_ret).Position)("noid").ToString)

    '                Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
    '                cmd.ExecuteNonQuery()

    '                '2. update barang
    '                Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, False, False, False)
    '                If Not hasilplusmin.Equals("ok") Then

    '                    If Not IsNothing(sqltrans) Then
    '                        sqltrans.Rollback()
    '                    End If

    '                    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
    '                    GoTo langsung
    '                End If

    '                If addstat = False Then

    '                    If DateValue(tgl_old) <> DateValue(ttgl.EditValue) Or nopol_old <> tnopol.EditValue Or supir_old <> tkd_supir.EditValue Then
    '                        Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, kdgud, kdbar, qtykecil, 0, "Retur Barang", supir_old, nopol_old)
    '                    End If

    '                End If

    '                '3. insert to hist stok
    '                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, ttgl.EditValue, kdgud, kdbar, 0, qtykecil, "Retur Barang", tkd_supir.Text.Trim, tnopol.EditValue)

    '                dv1.Delete(Me.BindingContext(dv1).Position)
    '                tbrutto.EditValue = GridView1.Columns("jumlah").SummaryItem.SummaryValue

    '                ' update header ----------------------------------------
    '                '2. update piutang toko
    '                Dim sqlct As String = String.Format("select netto,kd_toko from trretur where nobukti='{0}'", tbukti.Text.Trim)

    '                Dim cmdt As OleDbCommand = New OleDbCommand(sqlct, cn, sqltrans)
    '                Dim drdt As OleDbDataReader = cmdt.ExecuteReader

    '                If drdt.Read Then
    '                    If IsNumeric(drdt("netto").ToString) Then

    '                        Dim nett_sebelum As Double = drdt("netto").ToString

    '                        Dim sqluptoko As String = String.Format("update ms_toko set jumlahretur=jumlahretur - {0} where kd_toko='{1}'", Replace(nett_sebelum, ",", "."), tkd_toko.Text.Trim)
    '                        Dim sqluptoko2 As String = String.Format("update ms_toko set jumlahretur=jumlahretur + {0} where kd_toko='{1}'", Replace(tnetto.EditValue, ",", "."), tkd_toko.Text.Trim)

    '                        Dim cmdtk As OleDbCommand = New OleDbCommand(sqluptoko, cn, sqltrans)
    '                        cmdtk.ExecuteNonQuery()

    '                        Dim cmdtk2 As OleDbCommand = New OleDbCommand(sqluptoko2, cn, sqltrans)
    '                        cmdtk2.ExecuteNonQuery()

    '                    End If
    '                End If
    '                drdt.Close()

    '                '1. update faktur
    '                Dim sqlup_faktur As String = String.Format("update trretur set brutto={0},disc_per={1},disc_rp={2},netto={3} where nobukti='{4}'", Replace(tbrutto.EditValue, ",", "."), Replace(tdisc_per.EditValue, ",", "."), Replace(tdisc_rp.EditValue, ",", "."), Replace(tnetto.EditValue, ",", "."), tbukti.Text.Trim)

    '                Using cmdupfaktur As OleDbCommand = New OleDbCommand(sqlup_faktur, cn, sqltrans)
    '                    cmdupfaktur.ExecuteNonQuery()
    '                End Using


    '                ' akhir update header ----------------------------------------


    '                sqltrans.Commit()

    '                MsgBox("Data dihapus...", vbOKOnly + vbInformation, "Informasi")

    'langsung:

    '            Catch ex As Exception

    '                If Not IsNothing(sqltrans) Then
    '                    sqltrans.Rollback()
    '                End If

    '                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
    '            Finally


    '                If Not cn Is Nothing Then
    '                    If cn.State = ConnectionState.Open Then
    '                        cn.Close()
    '                    End If
    '                End If
    '            End Try

    '        End If

    '    End Sub

    Private Sub btdel_ret_Click(sender As System.Object, e As System.EventArgs) Handles btdel_ret.Click

        If IsNothing(dv_ret) Then
            Return
        End If

        If dv_ret.Count <= 0 Then
            Return
        End If

        If addstat Then
            If Not jenisjual = "T" Then

                dv_ret.Delete(Me.BindingContext(dv_ret).Position)
                totalnetto()
                Return

            End If
        End If

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction


            Dim jjenis As String
            If jenisjual = "T" Then

                If addstat = True Then
                    jjenis = "Jual TO (Retur)"
                Else
                    jjenis = "Jual TO (Retur-Edit)"
                End If

            Else

                If addstat = True Then
                    jjenis = "Jual Kanvas (Retur)"
                Else
                    jjenis = "Jual Kanvas (Retur-Edit)"
                End If

            End If

            Dim kdsupir As String = ""
            Dim nopol As String = ""

            If Not jenisjual = "T" Then
                kdsupir = dv_spm(0)("kd_supir").ToString
                nopol = dv_spm(0)("nopol").ToString
            End If

            Dim kdgud As String = dv_ret(Me.BindingContext(dv_ret).Position)("kd_gudang").ToString
            Dim kdbar As String = dv_ret(Me.BindingContext(dv_ret).Position)("kd_barang").ToString
            Dim qty As String = dv_ret(Me.BindingContext(dv_ret).Position)("qty").ToString
            Dim satuan As String = dv_ret(Me.BindingContext(dv_ret).Position)("satuan").ToString
            Dim harga As String = dv_ret(Me.BindingContext(dv_ret).Position)("harga").ToString
            Dim disc_per As String = dv_ret(Me.BindingContext(dv_ret).Position)("disc_per").ToString
            Dim disc_rp As String = dv_ret(Me.BindingContext(dv_ret).Position)("disc_rp").ToString
            Dim jumlah As String = dv_ret(Me.BindingContext(dv_ret).Position)("jumlah").ToString
            Dim qtykecil As String = dv_ret(Me.BindingContext(dv_ret).Position)("qtykecil").ToString
            Dim noid As String = dv_ret(Me.BindingContext(dv_ret).Position)("noid").ToString
            Dim qty1 As String = dv_ret(Me.BindingContext(dv_ret).Position)("qty1").ToString
            Dim qty2 As String = dv_ret(Me.BindingContext(dv_ret).Position)("qty2").ToString
            Dim qty3 As String = dv_ret(Me.BindingContext(dv_ret).Position)("qty3").ToString

            Dim sqlc As String = String.Format("select qtykecil from trfaktur_to5 where noid={0}", noid)
            Dim cmds As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
            Dim drds As OleDbDataReader = cmds.ExecuteReader

            If drds.HasRows Then
                If drds.Read Then
                    If IsNumeric(drds(0).ToString) Then

                        Dim qtyold As Integer = drds("qtykecil").ToString

                        If Not jenisjual = "T" Then

                            Dim hsilsimkos As String = simpankosong(cn, sqltrans, kdbar, tkd_toko.Text.Trim, qty1, qty2, qty3, qtyold, True)
                            If Not hsilsimkos.Equals("ok") Then
                                If Not IsNothing(sqltrans) Then
                                    sqltrans.Rollback()
                                End If

                                MsgBox(hsilsimkos, vbOKOnly + vbExclamation, "Informasi")
                                Return
                            End If

                            Dim sqlcek_kembali As String = String.Format("select kd_barang from ms_barang where kd_barang_kmb='{0}'", kdbar)
                            Dim cmdcek_kembali As OleDbCommand = New OleDbCommand(sqlcek_kembali, cn, sqltrans)
                            Dim drdcek_kembali As OleDbDataReader = cmdcek_kembali.ExecuteReader

                            Dim apakah_kembali As Boolean = False
                            If drdcek_kembali.Read Then
                                If Not drdcek_kembali(0).ToString.Equals("") Then
                                    apakah_kembali = True
                                End If
                            End If
                            drdcek_kembali.Close()

                            If apakah_kembali = False Then

                                '2. update barang
                                Dim hasilplusmin As String = PlusMin_Barang_Kend(cn, sqltrans, qtyold, kdbar, kdgud, True, False, False)
                                If Not hasilplusmin.Equals("ok") Then

                                    If Not IsNothing(sqltrans) Then
                                        sqltrans.Rollback()
                                    End If

                                    MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                                    Return
                                End If

                                '3. insert to hist stok
                                Clsmy.Insert_HistBarang(cn, sqltrans, tbukti.Text.Trim, tgl_old, kdgud, kdbar, qtykecil, 0, jjenis, kdsupir, nopol)

                            End If

                        End If
                    End If
                End If
            End If

            If noid > 0 Then

                Dim sqldel_ret As String = String.Format("delete from trfaktur_to5 where noid={0}", noid)
                Using cmddel_ret As OleDbCommand = New OleDbCommand(sqldel_ret, cn, sqltrans)
                    cmddel_ret.ExecuteNonQuery()
                End Using

            End If

            dv_ret.Delete(Me.BindingContext(dv_ret).Position)
            totalnetto()

            ' update header------------------------------------------------------------

            '2. update piutang toko

            If Not jenisjual = "T" Then


                Dim sqlct As String = String.Format("select netto,kd_toko,kd_karyawan from trfaktur_to where nobukti='{0}'", tbukti.Text.Trim)

                Dim cmdt As OleDbCommand = New OleDbCommand(sqlct, cn, sqltrans)
                Dim drdt As OleDbDataReader = cmdt.ExecuteReader

                If drdt.Read Then
                    If IsNumeric(drdt("netto").ToString) Then

                        Dim nett_sebelum As Double = drdt("netto").ToString
                        Dim kdsales_sebelum As String = drdt("kd_karyawan").ToString

                        Dim sqluptoko21 As String = String.Format("update ms_toko2 set jmlpiutang=jmlpiutang - {0} where kd_toko='{1}' and kd_karyawan='{2}'", Replace(nett_sebelum, ",", "."), tkd_toko.Text.Trim, kdsales_sebelum)
                        Dim sqluptoko222 As String = String.Format("update ms_toko2 set jmlpiutang=jmlpiutang + {0} where kd_toko='{1}' and kd_karyawan='{2}'", Replace(tnetto.EditValue, ",", "."), tkd_toko.Text.Trim, tkd_sales.Text.Trim)

                        Using cmdtok21 As OleDbCommand = New OleDbCommand(sqluptoko21, cn, sqltrans)
                            cmdtok21.ExecuteNonQuery()
                        End Using

                        Using cmdtok22 As OleDbCommand = New OleDbCommand(sqluptoko222, cn, sqltrans)
                            cmdtok22.ExecuteNonQuery()
                        End Using

                        Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, tbukti.Text.Trim, tbukti.Text.Trim, ttgl.EditValue, tkd_toko.Text.Trim, 0, nett_sebelum, "Jual (Edit)")
                        Clsmy.Insert_HistPiutang_Outl(cn, sqltrans, tbukti.Text.Trim, tbukti.Text.Trim, ttgl.EditValue, tkd_toko.Text.Trim, tnetto.EditValue, 0, "Jual (Edit)")

                    End If
                End If
                drdt.Close()

            End If

            '1. update faktur
            Dim sqlup_faktur As String = String.Format("update trfaktur_to set disc_per={0},disc_rp={1},brutto={2},netto={3},jmlkembali={4},jmlretur={5},jmlkelebihan={6} where nobukti='{7}'", Replace(tdisc_per.EditValue, ",", "."), Replace(tdisc_rp.EditValue, ",", "."), Replace(tbrutto.EditValue, ",", "."), Replace(tnetto.EditValue, ",", "."), Replace(ttot_kembali.EditValue, ",", "."), Replace(ttot_retur.EditValue, ",", "."), Replace(tlebih.EditValue, ",", "."), tbukti.Text.Trim)

            Using cmdupfaktur As OleDbCommand = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                cmdupfaktur.ExecuteNonQuery()
            End Using

            sqltrans.Commit()

            ' akhir update header ---------------------------------------------------

        Catch ex As Exception
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

    Private Sub GridView5_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView5.CellValueChanged

        If IsNothing(dv1) Then
            Return
        End If

        Dim nopos As Integer = Me.BindingContext(dv1).Position

        If dv1(nopos)("jenis_trans").ToString.Equals("KEMBALI") Then
            dv1(nopos)("disc_per") = 0
            dv1(nopos)("disc_rp") = 0

            If Not jenisjual = "T" Then
                dv1(nopos)("kd_gudang") = "None"
            End If

        ElseIf dv1(nopos)("jenis_trans").ToString.Equals("PINJAM") Then
            dv1(nopos)("disc_per") = 0
            dv1(nopos)("disc_rp") = 0
            dv1(nopos)("harga") = 0
            dv1(nopos)("jumlah0") = 0
            dv1(nopos)("jumlah") = 0
        End If

        If dv1(nopos)("kd_barang").ToString.Length > 0 Then
            rsatuan.Items.Clear()
            rsatuan.Items.Add(dv1(nopos)("satuan1").ToString)
            rsatuan.Items.Add(dv1(nopos)("satuan2").ToString)
            rsatuan.Items.Add(dv1(nopos)("satuan3").ToString)
        End If


        If e.Column.FieldName = "nama_lap" Then

            If tkd_toko.EditValue = "" Then
                'dv1(nopos)("nama_lap") = ""
                dv1.Delete(nopos)
                MsgBox("Isi dulu toko..", vbExclamation + vbOK, "Informasi")
                Return
            End If

            cek_kdbarang(e.Value, False, Me.BindingContext(dv1).Position)

            If cek_brgada_jaminan(Nothing, Nothing, dv1(nopos)("kd_barang").ToString) = False Then

                Dim nox() As Integer = Nothing
                Dim a As Integer = 0
                Dim adabrg_bjamin As Boolean = False

                Dim nox15() As Integer = Nothing
                Dim nox60() As Integer = Nothing

                Dim a15 As Integer = 0
                Dim a60 As Integer = 0

                Dim ada15 As Boolean = False
                Dim ada60 As Boolean = False

                For i As Integer = 0 To dv1.Count - 1

                    If dv1(i)("kd_barang").ToString = "B0002" Then
                        ada15 = True
                    End If

                    If dv1(i)("kd_barang").ToString = "B0003" Then
                        ada60 = True
                    End If

                    If dv1(i)("kd_barang").ToString = "BN0002" Then
                        ReDim nox15(a15)
                        nox15(a15) = i
                        a15 = a15 + 1
                    End If

                    If dv1(i)("kd_barang").ToString = "BN0003" Then
                        ReDim nox60(a60)
                        nox60(a60) = i
                        a60 = a60 + 1
                    End If

                    If cek_brgada_jaminan(Nothing, Nothing, dv1(i)("kd_barang").ToString) = True Then
                        adabrg_bjamin = True
                    End If

                    If cek_brgjaminan(Nothing, Nothing, dv1(i)("kd_barang").ToString) = True Then
                        ReDim nox(a)
                        nox(a) = i
                        a = a + 1
                    End If

                    If cek_brgkembali(Nothing, Nothing, dv1(i)("kd_barang").ToString) = True And dv1(i)("jenis_trans").ToString.Equals("KEMBALI") Then
                        ReDim nox(a)
                        nox(a) = i
                        a = a + 1
                    End If

                Next

                If a > 0 And adabrg_bjamin = False Then
                    For i As Integer = 0 To nox.Length - 1
                        dv1(nox(i)).Delete()
                    Next
                End If

                If ada15 = False Then
                    If a15 > 0 Then
                        For i As Integer = 0 To nox15.Length - 1
                            dv1(nox15(i)).Delete()
                        Next
                    End If
                End If

                If ada60 = False Then
                    If a60 > 0 Then
                        For i As Integer = 0 To nox60.Length - 1
                            dv1(nox60(i)).Delete()
                        Next
                    End If
                End If

            End If

            tbrutto.EditValue = GridView5.Columns("jumlah").SummaryItem.SummaryValue

        ElseIf e.Column.FieldName = "kd_barang" Then

            If tkd_toko.EditValue = "" Then
                'dv1(nopos)("kd_barang") = ""
                dv1.Delete(nopos)
                MsgBox("Isi dulu toko..", vbExclamation + vbOK, "Informasi")
                Return
            End If

            cek_kdbarang(e.Value, True, Me.BindingContext(dv1).Position)

            If cek_brgada_jaminan(Nothing, Nothing, dv1(nopos)("kd_barang").ToString) = False Then

                Dim nox() As Integer = Nothing
                Dim a As Integer = 0
                Dim adabrg_bjamin As Boolean = False

                Dim nox15() As Integer = Nothing
                Dim nox60() As Integer = Nothing

                Dim a15 As Integer = 0
                Dim a60 As Integer = 0

                Dim ada15 As Boolean = False
                Dim ada60 As Boolean = False

                For i As Integer = 0 To dv1.Count - 1

                    If dv1(i)("kd_barang").ToString = "B0002" Then
                        ada15 = True
                    End If

                    If dv1(i)("kd_barang").ToString = "B0003" Then
                        ada60 = True
                    End If

                    If dv1(i)("kd_barang").ToString = "BN0002" Then
                        ReDim nox15(a15)
                        nox15(a15) = i
                        a15 = a15 + 1
                    End If

                    If dv1(i)("kd_barang").ToString = "BN0003" Then
                        ReDim nox60(a60)
                        nox60(a60) = i
                        a60 = a60 + 1
                    End If

                    If cek_brgada_jaminan(Nothing, Nothing, dv1(i)("kd_barang").ToString) = True Then
                        adabrg_bjamin = True
                    End If

                    If cek_brgjaminan(Nothing, Nothing, dv1(i)("kd_barang").ToString) = True Then
                        ReDim nox(a)
                        nox(a) = i
                        a = a + 1
                    End If

                    If cek_brgkembali(Nothing, Nothing, dv1(i)("kd_barang").ToString) = True And dv1(i)("jenis_trans").ToString.Equals("KEMBALI") Then
                        ReDim nox(a)
                        nox(a) = i
                        a = a + 1
                    End If

                Next

                If a > 0 And adabrg_bjamin = False Then
                    For i As Integer = 0 To nox.Length - 1
                        dv1(nox(i)).Delete()
                    Next
                End If

                If ada15 = False Then
                    If a15 > 0 Then
                        For i As Integer = 0 To nox15.Length - 1
                            dv1(nox15(i)).Delete()
                        Next
                    End If
                End If

                If ada60 = False Then
                    If a60 > 0 Then
                        For i As Integer = 0 To nox60.Length - 1
                            dv1(nox60(i)).Delete()
                        Next
                    End If
                End If

            End If

            tbrutto.EditValue = GridView5.Columns("jumlah").SummaryItem.SummaryValue

        ElseIf e.Column.FieldName = "jnis_trans" Then

            If dv1(nopos)("kd_gudang").ToString.Trim.Length = 0 Then
                If jenisjual.Equals("T") Then
                    dv1(nopos)("kd_gudang") = "G000"
                Else
                    If e.Equals("KEMBALI") Then
                        dv1(nopos)("kd_gudang") = "G000"
                    Else
                        dv1(nopos)("kd_gudang") = cek_gudang_mobil_default()
                    End If

                End If
            End If



            kalkulasi(nopos)

            tbrutto.EditValue = GridView5.Columns("jumlah").SummaryItem.SummaryValue

        ElseIf e.Column.FieldName = "qty" Then

            'If Not IsNumeric(e.Value) Then
            '    dv1(Me.BindingContext(dv1).Position)("qty") = 0
            'End If

            kalkulasi(nopos)

            cek_klebihan_retur()

            If cek_brgada_jaminan(Nothing, Nothing, dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString) = True Then

                Dim jml As Integer = e.Value

                For i As Integer = 0 To dv1.Count - 1
                    If Not dv1(i)("jenis").ToString.Equals("FISIK") Then

                        dv1(i)("qty") = jml
                        kalkulasi(i)

                    End If
                Next
            End If

            tbrutto.EditValue = GridView5.Columns("jumlah").SummaryItem.SummaryValue

            hitung_bonus()


        ElseIf e.Column.FieldName = "satuan" Then
            kalkulasi(nopos)

            tbrutto.EditValue = GridView5.Columns("jumlah").SummaryItem.SummaryValue

        ElseIf e.Column.FieldName = "harga" Then

            'If Not IsNumeric(e.Value) Then
            '    dv1(Me.BindingContext(dv1).Position)("harga") = 0
            'End If

            kalkulasi(nopos)

            tbrutto.EditValue = GridView5.Columns("jumlah").SummaryItem.SummaryValue

        ElseIf e.Column.FieldName = "disc_per" Then

            Dim adabonus As Boolean = False
            For i As Integer = 0 To dv1.Count - 1
                If dv1(i)("kd_barang").ToString = "BN0002" Or dv1(i)("kd_barang").ToString = "BN0003" Then
                    adabonus = True
                End If
            Next

            If adabonus = True Then
                If dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString = "B0002" Or dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString = "B0003" Or dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString = "BN0002" Or dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString = "BN0003" Then
                    dv1(Me.BindingContext(dv1).Position)("disc_per") = 0
                End If
            End If

            hitung_disc_rupiah(nopos)
            kalkulasi(nopos)

            tbrutto.EditValue = GridView5.Columns("jumlah").SummaryItem.SummaryValue

        ElseIf e.Column.FieldName = "disc_rp" Then

            Dim adabonus As Boolean = False
            For i As Integer = 0 To dv1.Count - 1
                If dv1(i)("kd_barang").ToString = "BN0002" Or dv1(i)("kd_barang").ToString = "BN0003" Then
                    adabonus = True
                End If
            Next

            If adabonus = True Then
                If dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString = "B0002" Or dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString = "B0003" Or dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString = "BN0002" Or dv1(Me.BindingContext(dv1).Position)("kd_barang").ToString = "BN0003" Then
                    dv1(Me.BindingContext(dv1).Position)("disc_per") = 0
                End If
            End If

            hitung_disc_persen(nopos)
            kalkulasi(nopos)

            tbrutto.EditValue = GridView5.Columns("jumlah").SummaryItem.SummaryValue
        ElseIf e.Column.FieldName = "kd_gud" Then

            If Not jenisjual = "T" Then
                dv1(nopos)("kd_gudang") = "None"
            End If

        End If

    End Sub

    'Private Sub GridView5_InvalidValueException(sender As Object, e As DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs) Handles GridView5.InvalidValueException
    '    e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction

    'End Sub

    Private Sub GridView5_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView5.KeyDown

        If e.KeyCode = Keys.Delete Then
            btdel_Click(sender, Nothing)
        End If

    End Sub

    Private Sub GridView4_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView4.CellValueChanged

        If IsNothing(dv_ret) Then
            Return
        End If

        Dim nopos As Integer = Me.BindingContext(dv_ret).Position

        If dv_ret(nopos)("kd_barang").ToString.Length > 0 Then
            rsatuan_ret.Items.Clear()
            rsatuan_ret.Items.Add(dv_ret(nopos)("satuan1").ToString)
            rsatuan_ret.Items.Add(dv_ret(nopos)("satuan2").ToString)
            rsatuan_ret.Items.Add(dv_ret(nopos)("satuan3").ToString)
        End If

        If e.Column.FieldName = "nama_lap" Then
            cek_kdbarang_retur(e.Value, False, nopos, 0)

            totalnetto()

        ElseIf e.Column.FieldName = "kd_barang" Then
            cek_kdbarang_retur(e.Value, True, nopos, 0)

            totalnetto()
        ElseIf e.Column.FieldName = "satuan" Then
            kalkulasi_ret(nopos)
            totalnetto()
        ElseIf e.Column.FieldName = "qty" Then
            kalkulasi_ret(nopos)
            totalnetto()
        ElseIf e.Column.FieldName = "harga" Then
            kalkulasi_ret(nopos)
            totalnetto()
        ElseIf e.Column.FieldName = "disc_rp" Or e.Column.FieldName = "disc_per" Then
            kalkulasi_ret(nopos)
            totalnetto()
        End If

    End Sub

    Private Sub GridView4_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView4.KeyDown
        If e.KeyCode = Keys.Delete Then
            btdel_ret_Click(sender, Nothing)
        End If
    End Sub


End Class