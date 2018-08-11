Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpegawai

    Private bs1 As BindingSource
    Private ds As DataSet
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private Sub opendata()

        Const sql As String = "select top 1000 * from ms_pegawai order by kd_karyawan asc"
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

        sql = "select * from ms_pegawai where "

        Select Case tcbofind.SelectedIndex
            Case 0 ' kode
                sql = String.Format("{0} kd_karyawan='{1}'", sql, tfind.Text.Trim)
            Case 1 ' nama
                sql = String.Format("{0} nama_karyawan like '%{1}%'", sql, tfind.Text.Trim)
            Case 2 ' alamat
                sql = String.Format("{0} alamat like '%{1}%'", sql, tfind.Text.Trim)
            Case 3 ' bagian
                sql = String.Format("{0} bagian like '%{1}%'", sql, tfind.Text.Trim)
        End Select

        sql = String.Format("{0}  order by kd_karyawan asc", sql)

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

        Dim sql As String = String.Format("delete from ms_pegawai where kd_karyawan='{0}'", dv1(Me.BindingContext(bs1).Position)("kd_karyawan").ToString)
        ' Dim sql2 As String = String.Format("delete from ms_karyawan2 where nip='{0}'", dv1(Me.BindingContext(bs1).Position)("nip").ToString)
        ' Dim sql3 As String = String.Format("delete from ms_karyawan3 where nip='{0}'", dv1(Me.BindingContext(bs1).Position)("nip").ToString)

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            'comd = New OleDbCommand(sql3, cn, sqltrans)
            'comd.ExecuteNonQuery()

            'comd = New OleDbCommand(sql2, cn, sqltrans)
            'comd.ExecuteNonQuery()

            comd = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            Clsmy.InsertToLog(cn, "btkary", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("kd_karyawan").ToString, dv1(Me.BindingContext(bs1).Position)("nama_karyawan").ToString, sqltrans)

            sqltrans.Commit()

            dv1.Delete(bs1.Position)

            close_wait()

            MsgBox("Data telah dihapus...", vbOKOnly + vbInformation, "Informasi")

            '  opendata()

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

    Private Sub nonaktifkan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            Dim tahun As String = Year(DateValue(Date.Now))
            Dim kodeawal As String = String.Format("NA.{0}", Microsoft.VisualBasic.Right(tahun, 2))

            Dim sqlcek As String = String.Format("select MAX(kd_karyawan) as kode from ms_pegawai where aktif=0 and kd_karyawan like '{0}%'", kodeawal)
            Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn, sqltrans)
            Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

            Dim nomorakhir As String = 0
            If drdcek.Read Then
                If Not drdcek(0).Equals("") Then
                    nomorakhir = Microsoft.VisualBasic.Right(drdcek(0).ToString, 4)
                End If
            End If

            Dim nomorsekarang As Integer = Integer.Parse(nomorakhir)
            nomorsekarang = nomorsekarang + 1

            nomorakhir = nomorsekarang

            If nomorakhir.Length = 1 Then
                nomorakhir = String.Format("000{0}", nomorakhir)
            ElseIf nomorakhir.Length = 2 Then
                nomorakhir = String.Format("00{0}", nomorakhir)
            ElseIf nomorakhir.Length = 3 Then
                nomorakhir = String.Format("0{0}", nomorakhir)
            End If

            Dim nipbaru As String = String.Format("{0}{1}", kodeawal, nomorakhir)
            Dim niplama As String = dv1(Me.BindingContext(bs1).Position)("kd_karyawan").ToString

            Dim sql As String = ""

            ' hbarang_gudang
            sql = String.Format("update hbarang_gudang set supirsales='{0}' where supirsales='{1}'", nipbaru, niplama)
            Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()
            End Using

            'hbarang_kendaraan
            sql = String.Format("update hbarang_kendaraan set supirsales='{0}' where supirsales='{1}'", nipbaru, niplama)
            Using cmd2 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd2.ExecuteNonQuery()
            End Using

            'ms_giro
            sql = String.Format("update ms_giro set kd_karyawan='{0}' where kd_karyawan='{1}'", nipbaru, niplama)
            Using cmd3 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd3.ExecuteNonQuery()
            End Using

            'ms_pegawai
            sql = String.Format("update ms_pegawai set kd_karyawan='{0}',aktif=0 where kd_karyawan='{1}'", nipbaru, niplama)
            Using cmd4 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd4.ExecuteNonQuery()
            End Using

            'ms_sales1
            sql = String.Format("update ms_sales1 set kd_karyawan='{0}' where kd_karyawan='{1}'", nipbaru, niplama)
            Using cmd5 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd5.ExecuteNonQuery()
            End Using

            'ms_sales2
            sql = String.Format("update ms_sales2 set kd_karyawan='{0}' where kd_karyawan='{1}'", nipbaru, niplama)
            Using cmd6 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd6.ExecuteNonQuery()
            End Using

            'supirkenek1
            sql = String.Format("update ms_supirkenek set kd_sales='{0}' where kd_sales='{1}'", nipbaru, niplama)
            Using cmd7 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd7.ExecuteNonQuery()
            End Using

            'supirkenek2
            sql = String.Format("update ms_supirkenek set kd_supir='{0}' where kd_supir='{1}'", nipbaru, niplama)
            Using cmd8 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd8.ExecuteNonQuery()
            End Using

            'supirkenek3
            sql = String.Format("update ms_supirkenek set kd_kenek1='{0}' where kd_kenek1='{1}'", nipbaru, niplama)
            Using cmd9 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd9.ExecuteNonQuery()
            End Using

            'supirkenek4
            sql = String.Format("update ms_supirkenek set kd_kenek2='{0}' where kd_kenek2='{1}'", nipbaru, niplama)
            Using cmd10 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd10.ExecuteNonQuery()
            End Using

            'supirkenek5
            sql = String.Format("update ms_supirkenek set kd_kenek3='{0}' where kd_kenek3='{1}'", nipbaru, niplama)
            Using cmd11 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd11.ExecuteNonQuery()
            End Using

            'toko2
            sql = String.Format("update ms_toko2 set kd_karyawan='{0}' where kd_karyawan='{1}'", nipbaru, niplama)
            Using cmd12 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd12.ExecuteNonQuery()
            End Using

            'belicust
            sql = String.Format("update tr_belicust set kd_sales='{0}' where kd_sales='{1}'", nipbaru, niplama)
            Using cmd13 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd13.ExecuteNonQuery()
            End Using

            'kirim jb
            sql = String.Format("update tr_kirimjb set kd_supir='{0}' where kd_supir='{1}'", nipbaru, niplama)
            Using cmd14 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd14.ExecuteNonQuery()
            End Using

            'adm_gud1
            sql = String.Format("update tradm_gud set kd_krani='{0}' where kd_krani='{1}'", nipbaru, niplama)
            Using cmd15 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd15.ExecuteNonQuery()
            End Using

            'adm_gud2
            sql = String.Format("update tradm_gud set kd_supir='{0}' where kd_supir='{1}'", nipbaru, niplama)
            Using cmd16 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd16.ExecuteNonQuery()
            End Using

            'bayar_sales
            sql = String.Format("update trbayar_sles set kd_karyawan='{0}' where kd_karyawan='{1}'", nipbaru, niplama)
            Using cmd17 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd17.ExecuteNonQuery()
            End Using

            'bayar_kemb
            sql = String.Format("update trbayar2_kemb set kd_karyawan='{0}' where kd_karyawan='{1}'", nipbaru, niplama)
            Using cmd17_1 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd17_1.ExecuteNonQuery()
            End Using

            'beli
            sql = String.Format("update trbeli set kd_karyawan='{0}' where kd_karyawan='{1}'", nipbaru, niplama)
            Using cmd18 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd18.ExecuteNonQuery()
            End Using

            'daftar_tgh
            sql = String.Format("update trdaftar_tgh set kd_kolek='{0}' where kd_kolek='{1}'", nipbaru, niplama)
            Using cmd19 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd19.ExecuteNonQuery()
            End Using

            'daftar_tgh_sal
            sql = String.Format("update trdaftar_tgh_sal set kd_sales='{0}' where kd_sales='{1}'", nipbaru, niplama)
            Using cmd20 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd20.ExecuteNonQuery()
            End Using

            'faktur_to
            sql = String.Format("update trfaktur_to set kd_karyawan='{0}' where kd_karyawan='{1}'", nipbaru, niplama)
            Using cmd21 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd21.ExecuteNonQuery()
            End Using

            'pindah_gud
            sql = String.Format("update trpindahgud set kd_karyawan='{0}' where kd_karyawan='{1}'", nipbaru, niplama)
            Using cmd22 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd22.ExecuteNonQuery()
            End Using

            'rekap_to1
            sql = String.Format("update trrekap_to set kd_supir='{0}' where kd_supir='{1}'", nipbaru, niplama)
            Using cmd23 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd23.ExecuteNonQuery()
            End Using

            'rekap_to2
            sql = String.Format("update trrekap_to set kd_kenek1='{0}' where kd_kenek1='{1}'", nipbaru, niplama)
            Using cmd24 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd24.ExecuteNonQuery()
            End Using

            'rekap_to3
            sql = String.Format("update trrekap_to set kd_kenek2='{0}' where kd_kenek2='{1}'", nipbaru, niplama)
            Using cmd25 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd25.ExecuteNonQuery()
            End Using

            'rekap_to4
            sql = String.Format("update trrekap_to set kd_kenek3='{0}' where kd_kenek3='{1}'", nipbaru, niplama)
            Using cmd26 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd26.ExecuteNonQuery()
            End Using

            'retur
            sql = String.Format("update trretur set kd_supir='{0}' where kd_supir='{1}'", nipbaru, niplama)
            Using cmd27 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd27.ExecuteNonQuery()
            End Using

            'spm1
            sql = String.Format("update trspm set kd_sales='{0}' where kd_sales='{1}'", nipbaru, niplama)
            Using cmd28 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd28.ExecuteNonQuery()
            End Using

            'spm2
            sql = String.Format("update trspm set kd_kenek1='{0}' where kd_kenek1='{1}'", nipbaru, niplama)
            Using cmd29 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd29.ExecuteNonQuery()
            End Using

            'spm3
            sql = String.Format("update trspm set kd_kenek2='{0}' where kd_kenek2='{1}'", nipbaru, niplama)
            Using cmd30 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd30.ExecuteNonQuery()
            End Using

            'spm4
            sql = String.Format("update trspm set kd_kenek3='{0}' where kd_kenek3='{1}'", nipbaru, niplama)
            Using cmd31 As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd31.ExecuteNonQuery()
            End Using

            close_wait()
            sqltrans.Commit()
            MsgBox("Karyawan Dinonaktifkan..", vbOKOnly + vbInformation, "Informasi")

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
            tsnonakt.Enabled = True
        Else
            tsdel.Enabled = False
            tsnonakt.Enabled = False
        End If

        'If Convert.ToInt16(rows(0)("t_lap")) = 1 Then

        'Else

        'End If

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
        Using fkar2 As New fpegawai2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0}
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

        Using fkar2 As New fpegawai2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.ShowDialog()
        End Using

    End Sub

    Private Sub tsnonakt_Click(sender As System.Object, e As System.EventArgs) Handles tsnonakt.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        If MsgBox("Yakin akan dinonaktifkan ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
            nonaktifkan()
        End If

    End Sub

End Class