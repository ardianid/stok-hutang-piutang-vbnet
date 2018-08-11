Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fsupir_kenek

    Private bs1 As BindingSource
    Private ds As DataSet
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private Sub opendata()

        Const sql As String = "SELECT   ms_supirkenek.noid,  ms_supir.kd_karyawan AS kd_supir, ms_supir.nama_karyawan AS nama_supir, ms_kenek1.kd_karyawan AS kd_kenek1, " & _
            " ms_kenek1.nama_karyawan AS nama_kenek1, ms_kenek2.kd_karyawan AS kd_kenek2, ms_kenek2.nama_karyawan AS nama_kenek2, " & _
            "ms_kenek3.kd_karyawan AS kd_kenek3, ms_kenek3.nama_karyawan AS nama_kenek3, ms_supirkenek.nopol, ms_sales.kd_karyawan AS kd_sales, " & _
             "  ms_sales.nama_karyawan AS nama_sales " & _
            "FROM         ms_pegawai AS ms_supir INNER JOIN " & _
             " ms_supirkenek ON ms_supir.kd_karyawan = ms_supirkenek.kd_supir LEFT OUTER JOIN " & _
             "ms_pegawai AS ms_sales ON ms_supirkenek.kd_sales = ms_sales.kd_karyawan LEFT OUTER JOIN " & _
               " ms_pegawai AS ms_kenek3 ON ms_supirkenek.kd_kenek3 = ms_kenek3.kd_karyawan LEFT OUTER JOIN " & _
              " ms_pegawai AS ms_kenek2 ON ms_supirkenek.kd_kenek2 = ms_kenek2.kd_karyawan LEFT OUTER JOIN " & _
               " ms_pegawai AS ms_kenek1 ON ms_supirkenek.kd_kenek1 = ms_kenek1.kd_karyawan"
        Dim cn As OleDbConnection = Nothing

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

            bs1 = New BindingSource
            bs1.DataSource = dv1
            bn1.BindingSource = bs1

            grid1.DataSource = bs1

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

    Private Sub cari()

        'bs1.DataSource = Nothing
        grid1.DataSource = Nothing
        Dim cn As OleDbConnection = Nothing

        Dim sql As String = ""
        Dim scbo As Integer = tcbofind.SelectedIndex

        sql = "SELECT   ms_supirkenek.noid,  ms_supir.kd_karyawan AS kd_supir, ms_supir.nama_karyawan AS nama_supir, ms_kenek1.kd_karyawan AS kd_kenek1, " & _
            " ms_kenek1.nama_karyawan AS nama_kenek1, ms_kenek2.kd_karyawan AS kd_kenek2, ms_kenek2.nama_karyawan AS nama_kenek2, " & _
            "ms_kenek3.kd_karyawan AS kd_kenek3, ms_kenek3.nama_karyawan AS nama_kenek3, ms_supirkenek.nopol, ms_sales.kd_karyawan AS kd_sales, " & _
             "  ms_sales.nama_karyawan AS nama_sales " & _
            "FROM         ms_pegawai AS ms_supir INNER JOIN " & _
             " ms_supirkenek ON ms_supir.kd_karyawan = ms_supirkenek.kd_supir LEFT OUTER JOIN " & _
             "ms_pegawai AS ms_sales ON ms_supirkenek.kd_sales = ms_sales.kd_karyawan LEFT OUTER JOIN " & _
               " ms_pegawai AS ms_kenek3 ON ms_supirkenek.kd_kenek3 = ms_kenek3.kd_karyawan LEFT OUTER JOIN " & _
              " ms_pegawai AS ms_kenek2 ON ms_supirkenek.kd_kenek2 = ms_kenek2.kd_karyawan LEFT OUTER JOIN " & _
               " ms_pegawai AS ms_kenek1 ON ms_supirkenek.kd_kenek1 = ms_kenek1.kd_karyawan"

        Select Case tcbofind.SelectedIndex
            Case 0
                sql = String.Format("{0} WHERE ms_supirkenek.nopol like '%{1}%'", sql, tfind.Text.Trim)
            Case 1
                sql = String.Format("{0} WHERE ms_sales.nama_karyawan like '%{1}%'", sql, tfind.Text.Trim)
            Case 2 ' kode
                sql = String.Format("{0} WHERE ms_supir.nama_karyawan like '%{1}%'", sql, tfind.Text.Trim)
            Case 3 ' nama
                sql = String.Format("{0} WHERE ms_kenek1.nama_karyawan like '%{1}%' or ms_kenek2.nama_karyawan like '%{1}%' or ms_kenek3.nama_karyawan like '%{1}%' ", sql, tfind.Text.Trim)
        End Select

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            bs1.DataSource = dv1
            bn1.BindingSource = bs1

            grid1.DataSource = bs1

            close_wait()

        Catch ex As Exception
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

    Private Sub hapus()

        Dim sql As String = String.Format("delete from ms_supirkenek where kd_supir='{0}' and kd_kenek1='{1}' and kd_kenek2='{2}' and kd_kenek3='{3}'",
                                          dv1(Me.BindingContext(bs1).Position)("kd_supir").ToString, dv1(Me.BindingContext(bs1).Position)("kd_kenek1").ToString,
                                          dv1(Me.BindingContext(bs1).Position)("kd_kenek2").ToString, dv1(Me.BindingContext(bs1).Position)("kd_kenek3").ToString)

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            comd = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            Clsmy.InsertToLog(cn, "btsupkenek", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("kd_supir").ToString, dv1(Me.BindingContext(bs1).Position)("kd_kenek1").ToString, sqltrans)

            sqltrans.Commit()

            close_wait()

            MsgBox("Data telah dihapus...", vbOKOnly + vbInformation, "Informasi")

            opendata()

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

    Private Sub Get_Aksesform()

        Dim rows() As DataRow = dtmenu.Select(String.Format("NAMAFORM='{0}'", Me.Name.ToUpper.ToString))

        If Convert.ToInt16(rows(0)("t_add")) = 1 Then
            tsadd.Enabled = True
        Else
            tsadd.Enabled = False
        End If

        If Convert.ToInt16(rows(0)("t_edit")) = 1 Then
            tsedit.Enabled = True
        Else
            tsedit.Enabled = False
        End If

        If Convert.ToInt16(rows(0)("t_del")) = 1 Then
            tsdel.Enabled = True
        Else
            tsdel.Enabled = False
        End If

    End Sub

    Private Sub fuser_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        tcbofind.SelectedIndex = 0

        Get_Aksesform()

        opendata()
    End Sub

    Private Sub tsfind_Click(sender As System.Object, e As System.EventArgs) Handles tsfind.Click
        cari()
    End Sub

    Private Sub tfind_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tfind.KeyDown
        If e.KeyCode = 13 Then
            cari()
        End If
    End Sub

    Private Sub tsref_Click(sender As System.Object, e As System.EventArgs) Handles tsref.Click
        tsfind.Text = ""
        opendata()
    End Sub

    Private Sub tsdel_Click(sender As System.Object, e As System.EventArgs) Handles tsdel.Click

        If dv1.Count < 1 Then
            Exit Sub
        End If

        If MsgBox("Yakin akan dihapus ?", vbYesNo + vbQuestion, "Konfirmasi") = vbNo Then
            Exit Sub
        End If

        hapus()

    End Sub

    Private Sub tsadd_Click(sender As System.Object, e As System.EventArgs) Handles tsadd.Click
        Using fkar2 As New fsupir_kenek2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0}
            fkar2.ShowDialog()
        End Using
    End Sub

    Private Sub tsedit_Click(sender As System.Object, e As System.EventArgs) Handles tsedit.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        Using fkar2 As New fsupir_kenek2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.ShowDialog()
        End Using

    End Sub



End Class