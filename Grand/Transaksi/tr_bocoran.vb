Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class tr_bocoran

    Private bs1 As BindingSource
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private dvmanager3 As Data.DataViewManager
    Private dv3 As Data.DataView

    Private addstat As Boolean = False

    Private Sub opengrid2()

        grid2.DataSource = Nothing
        dv2 = Nothing

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        If addstat Then
            Return
        End If

        Dim sql As String = String.Format("SELECT  tr_bocoran2.noid, tr_bocoran2.kd_gudang, tr_bocoran2.kd_barang, ms_barang.nama_barang, tr_bocoran2.qty, tr_bocoran2.qtykecil, tr_bocoran2.satuan " & _
            "FROM  tr_bocoran2 INNER JOIN ms_barang ON tr_bocoran2.kd_barang = ms_barang.kd_barang where tr_bocoran2.nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)


        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager2 = New DataViewManager(ds)
            dv2 = dvmanager2.CreateDataView(ds.Tables(0))

            grid2.DataSource = dv2


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

    Private Sub opengrid3()

        grid3.DataSource = Nothing
        dv3 = Nothing

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        If addstat Then
            Return
        End If

        Dim sql As String = String.Format("SELECT     tr_bocoran3.noid, tr_bocoran3.nobukti, tr_bocoran3.kd_gudang, tr_bocoran3.kd_barang, ms_barang.nama_barang, tr_bocoran3.qty, tr_bocoran3.qtykecil, tr_bocoran3.satuan,ms_barang.qty1,ms_barang.qty2,ms_barang.qty3 " & _
            "FROM         tr_bocoran3 INNER JOIN ms_barang ON tr_bocoran3.kd_barang = ms_barang.kd_barang where tr_bocoran3.nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)


        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager3 = New DataViewManager(ds)
            dv3 = dvmanager3.CreateDataView(ds.Tables(0))

            grid3.DataSource = dv3

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


    Private Sub opendata()

        Dim sql As String = String.Format("SELECT     tr_bocoran.nobukti, tr_bocoran.tanggal, tr_bocoran.tgl_keluar, tr_bocoran.nobukti_gd, tr_bocoran.note, tr_bocoran.sbatal, ms_pegawai.nama_karyawan, tradm_gud.shit,tradm_gud.nobukti_gd as nodo " & _
            "FROM         ms_pegawai INNER JOIN " & _
            "tradm_gud ON ms_pegawai.kd_karyawan = tradm_gud.kd_krani RIGHT OUTER JOIN " & _
            "tr_bocoran ON tradm_gud.nobukti = tr_bocoran.nobukti_gd where tr_bocoran.tanggal >='{0}' and  tr_bocoran.tanggal <='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))


        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing

        Try

            open_wait()

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New DataSet()
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

        sql = String.Format("SELECT     tr_bocoran.nobukti, tr_bocoran.tanggal, tr_bocoran.tgl_keluar, tr_bocoran.nobukti_gd, tr_bocoran.note, tr_bocoran.sbatal, ms_pegawai.nama_karyawan, tradm_gud.shit,tradm_gud.nobukti_gd as nodo " & _
            "FROM         ms_pegawai INNER JOIN " & _
            "tradm_gud ON ms_pegawai.kd_karyawan = tradm_gud.kd_krani RIGHT OUTER JOIN " & _
            "tr_bocoran ON tradm_gud.nobukti = tr_bocoran.nobukti_gd where tr_bocoran.tanggal >='{0}' and  tr_bocoran.tanggal <='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))

        Select Case tcbofind.SelectedIndex
            Case 0 ' nobukti
                sql = String.Format("{0} and tr_bocoran.nobukti like '%{1}%'", sql, tfind.Text.Trim)
            Case 1
                sql = String.Format("{0} and tradm_gud.nobukti_gd like '%{1}%'", sql, tfind.Text.Trim)
            Case 2 ' tanggal

                If Not IsDate(tfind.Text.Trim) Then
                    MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                    Return
                Else

                    If tfind.Text.Trim.Length < 10 Or tfind.Text.Trim.Length > 10 Then
                        MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                        Return
                    End If

                End If

                sql = String.Format("{0} and tr_bocoran.tanggal='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))
            Case 3
                sql = String.Format("{0} and tr_bocoran.nobukti_gd like '%{1}%'", sql, tfind.Text.Trim)
            Case 4 ' supp
                sql = String.Format("{0} and ms_pegawai.nama_karyawan like '%{1}%'", sql, tfind.Text.Trim)
        End Select

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New DataSet()
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

        Dim alasan = ""
        Using falasanb As New fkonfirm_hapus With {.WindowState = FormWindowState.Normal, .StartPosition = FormStartPosition.CenterScreen}
            falasanb.ShowDialog()
            alasan = falasanb.get_alasan
        End Using

        If alasan = "" Then
            Return
        End If

        Dim sql As String = String.Format("update tr_bocoran set sbatal=1,alasan_batal='{0}' where nobukti='{1}'", alasan.ToUpper, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString)
        Dim sqlupadm As String = String.Format("update tradm_gud set sambil=0 where nobukti='{0}'", dv1(bs1.Position)("nobukti_gd").ToString)

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand = Nothing
        Dim cmdtoko As OleDbCommand = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            comd = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            Using cmdadm As OleDbCommand = New OleDbCommand(sqlupadm, cn, sqltrans)
                cmdadm.ExecuteNonQuery()
            End Using


            If hapus_detail(cn, sqltrans) = True Then
                Clsmy.InsertToLog(cn, "bt_bocoran", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString, dv1(Me.BindingContext(bs1).Position)("tanggal").ToString, sqltrans)

                sqltrans.Commit()

                dv1(bs1.Position)("sbatal") = 1

                close_wait()

                MsgBox("Data telah dibatalkan...", vbOKOnly + vbInformation, "Informasi")
            End If


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

    Private Function hapus_detail(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As Boolean

        Dim hasil = True

        Dim nobukti As String = dv1(Me.BindingContext(bs1).Position)("nobukti").ToString
        Dim tanggal As String = dv1(Me.BindingContext(bs1).Position)("tanggal").ToString

        Dim sql As String = String.Format("select * from tr_bocoran2 where nobukti='{0}'", nobukti)

        Dim comd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = comd.ExecuteReader

        If drd.HasRows Then
            While drd.Read

                Dim qtykecil As Integer = Integer.Parse(drd("qtykecil").ToString)
                Dim kdbar As String = drd("kd_barang").ToString
                Dim kdgud As String = drd("kd_gudang").ToString


                If IsNumeric(drd("noid").ToString) Then

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil, kdbar, kdgud, True, False, False)
                    If Not hasilplusmin.Equals("ok") Then

                        close_wait()
                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        hasil = False
                        Exit While
                    End If

                    '3. insert to hist stok
                    Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tanggal, kdgud, kdbar, qtykecil, 0, "Serv Bocoran (Batal)", "-", "BE XXXX XX")

                End If

            End While
        End If

        drd.Close()


        '' masuk ke barang kosong


        Dim sql3 As String = String.Format("select * from tr_bocoran3 where nobukti='{0}'", nobukti)

        Dim comd3 As OleDbCommand = New OleDbCommand(sql3, cn, sqltrans)
        Dim drd3 As OleDbDataReader = comd3.ExecuteReader

        If drd3.HasRows Then
            While drd3.Read

                Dim qtykecil3 As Integer = Integer.Parse(drd3("qtykecil").ToString)
                Dim kdbar3 As String = drd3("kd_barang").ToString
                Dim kdgud3 As String = drd3("kd_gudang").ToString


                If IsNumeric(drd3("noid").ToString) Then

                    '2. update barang
                    Dim hasilplusmin As String = PlusMin_Barang(cn, sqltrans, qtykecil3, kdbar3, kdgud3, False, False, False)
                    If Not hasilplusmin.Equals("ok") Then

                        close_wait()
                        If Not IsNothing(sqltrans) Then
                            sqltrans.Rollback()
                        End If

                        MsgBox(hasilplusmin, vbOKOnly + vbExclamation, "Informasi")
                        hasil = False
                        Exit While
                    End If

                    '3. insert to hist stok
                    Clsmy.Insert_HistBarang(cn, sqltrans, nobukti, tanggal, kdgud3, kdbar3, 0, qtykecil3, "Serv Bocoran (Batal)", "-", "BE XXXX XX")

                End If

            End While
        End If

        drd3.Close()

        Return hasil

    End Function

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

    Private Sub cekbatal_onserver()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select sbatal from tr_bocoran where nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd(0).ToString) Then
                    dv1(bs1.Position)("sbatal") = drd(0).ToString
                End If
            End If
            drd.Close()


        Catch ex As Exception

        End Try

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
        tfind.Text = ""
        opendata()
    End Sub

    Private Sub tsdel_Click(sender As System.Object, e As System.EventArgs) Handles tsdel.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        cekbatal_onserver()

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Faktur telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If MsgBox("Yakin akan dibatalkan ?", vbYesNo + vbQuestion, "Konfirmasi") = vbNo Then
            Exit Sub
        End If

        hapus()

    End Sub

    Private Sub tsadd_Click(sender As System.Object, e As System.EventArgs) Handles tsadd.Click

        addstat = True

        Using fkar2 As New tr_bocoran2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0}
            fkar2.ShowDialog()
            addstat = False
        End Using
    End Sub

    Private Sub tsedit_Click(sender As System.Object, e As System.EventArgs) Handles tsedit.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        cekbatal_onserver()

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Faktur telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        addstat = True
        Using fkar2 As New tr_bocoran2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.ShowDialog()
            addstat = False
        End Using

    End Sub

    Private Sub tsview_Click(sender As System.Object, e As System.EventArgs) Handles tsview.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        Using fkar2 As New tr_bocoran2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.btsimpan.Enabled = False
            fkar2.btadd.Enabled = False
            fkar2.btedit.Enabled = False
            fkar2.btdel.Enabled = False
            fkar2.ShowDialog()
        End Using

    End Sub

    Private Sub GridView1_Click(sender As Object, e As System.EventArgs) Handles GridView1.Click
        opengrid2()
        opengrid3()
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        opengrid2()
        opengrid3()
    End Sub

    Private Sub GridView1_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles GridView1.SelectionChanged
        opengrid2()
        opengrid3()
    End Sub

End Class