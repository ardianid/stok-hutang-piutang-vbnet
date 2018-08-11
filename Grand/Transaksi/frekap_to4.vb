Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class frekap_to4

    Public dv As DataView

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Public kdjalur As String
    Public namajalur As String

    Private Sub opengrid()

        Dim sql As String = ""

        If kdjalur.Equals("-") Then

            sql = String.Format("SELECT    0 as ok, trfaktur_to.nobukti, trfaktur_to.tanggal, trfaktur_to.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, trfaktur_to.kd_karyawan, ms_pegawai.nama_karyawan, trfaktur_to.netto " & _
                "FROM         trfaktur_to INNER JOIN " & _
                      "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko INNER JOIN " & _
                      "ms_pegawai ON trfaktur_to.kd_karyawan = ms_pegawai.kd_karyawan where trfaktur_to.jnis_fak='T' and trfaktur_to.sbatal=0 and trfaktur_to.skirim=0")

        Else
            sql = String.Format("SELECT    0 as ok, trfaktur_to.nobukti, trfaktur_to.tanggal, trfaktur_to.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, trfaktur_to.kd_karyawan, ms_pegawai.nama_karyawan, trfaktur_to.netto " & _
                "FROM         trfaktur_to INNER JOIN " & _
                      "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko INNER JOIN " & _
                      "ms_pegawai ON trfaktur_to.kd_karyawan = ms_pegawai.kd_karyawan where trfaktur_to.jnis_fak='T' and trfaktur_to.sbatal=0 and ms_toko.kd_jalurkirim='{0}' and trfaktur_to.skirim=0", kdjalur)
        End If

            If dv.Count > 0 Then

                Dim x As Integer = 0

                For i As Integer = 0 To dv.Count - 1
                    If dv(i)("noid").Equals(0) Then

                        If x = 0 Then
                            sql = String.Format("{0} and trfaktur_to.nobukti not in ('{1}'", sql, dv(i)("nobukti_fak").ToString)
                        Else
                            sql = String.Format("{0},'{1}'", sql, dv(i)("nobukti_fak").ToString)
                        End If

                        x = x + 1

                    End If
                Next

                If x > 0 Then
                    sql = String.Format("{0})", sql)
                End If

            End If

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

    Private Sub opengrid2()

        Dim sql As String = String.Format("SELECT    0 as ok, trfaktur_to.nobukti, trfaktur_to.tanggal, trfaktur_to.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, trfaktur_to.kd_karyawan, ms_pegawai.nama_karyawan, trfaktur_to.netto " & _
                "FROM         trfaktur_to INNER JOIN " & _
                      "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko INNER JOIN " & _
                      "ms_pegawai ON trfaktur_to.kd_karyawan = ms_pegawai.kd_karyawan where trfaktur_to.jnis_fak='T' and trfaktur_to.sbatal=0 and ms_toko.kd_jalurkirim='{0}' and trfaktur_to.skirim=1 and trfaktur_to.spulang=1 and trfaktur_to.statkirim='BELUM TERKIRIM'", kdjalur)

        If dv.Count > 0 Then

            Dim x As Integer = 0

            For i As Integer = 0 To dv.Count - 1
                If dv(i)("noid").Equals(0) Then

                    If x = 0 Then
                        sql = String.Format("{0} and trfaktur_to.nobukti not in ('{1}'", sql, dv(i)("nobukti_fak").ToString)
                    Else
                        sql = String.Format("{0},'{1}'", sql, dv(i)("nobukti_fak").ToString)
                    End If

                    x = x + 1

                End If
            Next

            If x > 0 Then
                sql = String.Format("{0})", sql)
            End If

        End If

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        grid2.DataSource = Nothing

        Try

            open_wait()

            dv2 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv2 = dvmanager.CreateDataView(ds.Tables(0))

            grid2.DataSource = dv2

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


    Private Sub ComboBoxEdit1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBoxEdit1.SelectedIndexChanged

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        For i As Integer = 0 To dv1.Count - 1
            If ComboBoxEdit1.SelectedIndex = 1 Then
                dv1(i)("ok") = 1
            Else
                dv1(i)("ok") = 0
            End If
        Next

       

    End Sub

    Private Sub ComboBoxEdit2_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBoxEdit2.SelectedIndexChanged

        If IsNothing(dv2) Then
            Return
        End If

        If dv2.Count <= 0 Then
            Return
        End If

        For i As Integer = 0 To dv2.Count - 1
            If ComboBoxEdit2.SelectedIndex = 1 Then
                dv2(i)("ok") = 1
            Else
                dv2(i)("ok") = 0
            End If
        Next

    End Sub

    Private Sub frekap_to4_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated

        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            grid1.Focus()
        Else
            grid2.Focus()
        End If

    End Sub

    Private Sub frekap_to4_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tjalur.Text = namajalur
        tkode.Text = kdjalur
        opengrid()
        opengrid2()

        ComboBoxEdit1.SelectedIndex = 0
        ComboBoxEdit2.SelectedIndex = 0

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If IsNothing(dv1) Then
            GoTo lanjut_sini
        End If

        If dv1.Count <= 0 Then
            GoTo lanjut_sini
        End If

        For i As Integer = 0 To dv1.Count - 1
            If dv1(i)("ok") = 1 Then

                Dim orow As DataRowView = dv.AddNew
                orow("noid") = 0
                orow("nobukti_fak") = dv1(i)("nobukti").ToString
                orow("tanggal") = dv1(i)("tanggal").ToString
                orow("kd_toko") = dv1(i)("kd_toko").ToString
                orow("nama_toko") = dv1(i)("nama_toko").ToString
                orow("alamat_toko") = dv1(i)("alamat_toko").ToString
                orow("kd_karyawan") = dv1(i)("kd_karyawan").ToString
                orow("nama_karyawan") = dv1(i)("nama_karyawan").ToString
                orow("netto") = dv1(i)("netto").ToString
                dv.EndInit()

            End If
        Next

lanjut_sini:

        If IsNothing(dv2) Then
            Me.Close()
            Return
        End If

        If dv2.Count <= 0 Then
            Me.Close()
            Return
        End If

        For i As Integer = 0 To dv2.Count - 1
            If dv2(i)("ok") = 1 Then

                Dim orow As DataRowView = dv.AddNew
                orow("noid") = 0
                orow("nobukti_fak") = dv2(i)("nobukti").ToString
                orow("tanggal") = dv2(i)("tanggal").ToString
                orow("kd_toko") = dv2(i)("kd_toko").ToString
                orow("nama_toko") = dv2(i)("nama_toko").ToString
                orow("alamat_toko") = dv2(i)("alamat_toko").ToString
                orow("kd_karyawan") = dv2(i)("kd_karyawan").ToString
                orow("nama_karyawan") = dv2(i)("nama_karyawan").ToString
                orow("netto") = dv2(i)("netto").ToString
                dv.EndInit()

            End If
        Next

        Me.Close()

    End Sub

End Class