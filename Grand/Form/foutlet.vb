Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class foutlet

    Private bs1 As BindingSource
    Private ds As DataSet
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager_sal As Data.DataViewManager
    Private dv_sal As Data.DataView

    Private Sub opendata()

        Dim sql As String = "select top 500 aktif,kd_toko,nama_toko,alamat_toko,notelp1,notelp2,notelp3 from ms_toko"

        Select Case cbstatus.SelectedIndex
            Case 1
                sql = String.Format("{0} where aktif=1", sql)
            Case 2
                sql = String.Format("{0} where aktif=0", sql)
        End Select

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

        If Not IsNothing(dv1) Then
            If dv1.Count > 0 Then
                bs1.MoveFirst()
            Else
                isi_sales()
            End If
        End If

    End Sub

    Private Sub isi_sales()

        gridsal.DataSource = Nothing
        dv_sal = Nothing

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim kdtoko As String
        kdtoko = dv1(bs1.Position)("kd_toko").ToString

        Dim sql As String = String.Format("select a.kd_karyawan,b.nama_karyawan,a.limit_val,a.jmlpiutang from ms_toko2 a inner join ms_pegawai b on a.kd_karyawan=b.kd_karyawan where a.kd_toko='{0}'", kdtoko)

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet



        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager_sal = New DataViewManager(ds)
            dv_sal = dvmanager_sal.CreateDataView(ds.Tables(0))

            gridsal.DataSource = dv_sal


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

    Private Sub cari()

        'bs1.DataSource = Nothing
        grid1.DataSource = Nothing
        Dim cn As OleDbConnection = Nothing

        '  Dim sql As String = ""
        Dim scbo As Integer = tcbofind.SelectedIndex

        Dim sql As String = "select top 2000 aktif,kd_toko,nama_toko,alamat_toko,notelp1,notelp2,notelp3 from ms_toko"

        Select Case cbstatus.SelectedIndex
            Case 1
                sql = String.Format("{0} where aktif=1", sql)
            Case 2
                sql = String.Format("{0} where aktif=0", sql)
            Case Else
                sql = String.Format("{0} where (aktif=1 or aktif=0)", sql)
        End Select

        Select Case tcbofind.SelectedIndex
            Case 0 ' kode
                sql = String.Format("{0} and kd_toko='{1}'", sql, tfind.Text.Trim)
            Case 1 ' nama
                sql = String.Format("{0} and nama_toko like '%{1}%'", sql, tfind.Text.Trim)
            Case 2
                sql = String.Format("{0} and alamat_toko like '%{1}%'", sql, tfind.Text.Trim)
            Case 3
                sql = String.Format("{0} and kd_toko in (select kd_toko from ms_toko2 inner join ms_pegawai " & _
                "on ms_toko2.kd_karyawan=ms_pegawai.kd_karyawan where ms_pegawai.nama_karyawan like '%{1}%' and ms_pegawai.bagian='SALES')", sql, tfind.Text.Trim)
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

        Dim sql As String = String.Format("delete from ms_toko where kd_toko='{0}'", dv1(Me.BindingContext(bs1).Position)("kd_toko").ToString)
        Dim sql2 As String = String.Format("delete from ms_toko2 where kd_toko='{0}'", dv1(Me.BindingContext(bs1).Position)("kd_toko").ToString)
        Dim sql3 As String = String.Format("delete from ms_toko3 where kd_toko='{0}'", dv1(Me.BindingContext(bs1).Position)("kd_toko").ToString)

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand = Nothing
        Dim comd2 As OleDbCommand = Nothing
        Dim comd3 As OleDbCommand = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            comd3 = New OleDbCommand(sql3, cn, sqltrans)
            comd3.ExecuteNonQuery()

            comd2 = New OleDbCommand(sql2, cn, sqltrans)
            comd2.ExecuteNonQuery()

            comd = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            Clsmy.InsertToLog(cn, "bttoko", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("kd_toko").ToString, "", sqltrans)

            sqltrans.Commit()

            dv1.Delete(bs1.Position)

            close_wait()

            MsgBox("Data telah dihapus...", vbOKOnly + vbInformation, "Informasi")



            'opendata()

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

        If Convert.ToInt16(rows(0)("t_active")) = 1 Then
            tsview.Enabled = True
        Else
            tsview.Enabled = False
        End If

    End Sub

    Private Sub fuser_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Get_Aksesform()

        cbstatus.SelectedIndex = 1
        tcbofind.SelectedIndex = 0

        ' opendata()
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
        Using fkar2 As New foutlet2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0}
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

        Using fkar2 As New foutlet2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.ShowDialog()
        End Using

    End Sub

    Private Sub cbstatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cbstatus.SelectedIndexChanged
        tfind.Text = ""
        opendata()
    End Sub

    Private Sub tsview_Click(sender As System.Object, e As System.EventArgs) Handles tsview.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        Using fkar2 As New foutlet2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.btsimpan.Enabled = False
            fkar2.ShowDialog()
        End Using

    End Sub

    Private Sub GridView1_Click(sender As Object, e As System.EventArgs) Handles GridView1.Click
        isi_sales()
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        isi_sales()
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        isi_sales()
    End Sub

    Private Sub GridView1_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles GridView1.SelectionChanged
        isi_sales()
    End Sub

End Class