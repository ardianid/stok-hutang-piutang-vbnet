Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fgiro_tolak

    Private bs1 As BindingSource
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private IsLoadForm As Boolean

    Private Sub opendata()

        Dim sql As String = String.Format("SELECT nobukti, tanggal, total, note, sbatal FROM tr_bg where jenis=2 and tanggal >='{0}' and  tanggal <='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))

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

    Private Sub opengrid_detail()

        If IsLoadForm Then
            Return
        End If

        Dim nobukti As String
        If IsNothing(dv1) Then
            nobukti = "xx1"
        ElseIf dv1.Count <= 0 Then
            nobukti = "xx1"
        Else
            nobukti = dv1(bs1.Position)("nobukti").ToString
        End If

        Dim sql As String = String.Format("SELECT ms_giro.nogiro, ms_giro.tgl_giro,ms_giro.tgl_jt, ms_giro.namabank, ms_toko.nama_toko, ms_toko.alamat_toko, ms_giro.jumlah " & _
        "FROM  tr_bg2 INNER JOIN ms_giro ON tr_bg2.nogiro = ms_giro.nogiro INNER JOIN ms_toko ON ms_giro.kd_toko = ms_toko.kd_toko " & _
        "WHERE tr_bg2.nobukti='{0}'", nobukti)

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        grid2.DataSource = Nothing

        Try

            '  open_wait()

            dv2 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager2 = New DataViewManager(ds)
            dv2 = dvmanager2.CreateDataView(ds.Tables(0))

            grid2.DataSource = dv2

            '   close_wait()


        Catch ex As OleDb.OleDbException
            '  close_wait()
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

        sql = String.Format("SELECT nobukti, tanggal, total, note, sbatal FROM tr_bg where jenis=2 and tanggal >='{0}' and  tanggal <='{1}'", convert_date_to_eng(tglperiod1), convert_date_to_eng(tglperiod2))

        Select Case tcbofind.SelectedIndex
            Case 0 ' nobukti
                sql = String.Format("{0} and nobukti like '%{1}%'", sql, tfind.Text.Trim)
            Case 1 ' tanggal

                If Not IsDate(tfind.Text.Trim) Then
                    MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                    Return
                Else

                    If tfind.Text.Trim.Length < 10 Or tfind.Text.Trim.Length > 10 Then
                        MsgBox("Koreksi Tanggal....", vbOKOnly + vbExclamation, "Informasi")
                        Return
                    End If

                End If

                sql = String.Format("{0} and tanggal='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))

            Case 2
                sql = String.Format("{0} and nobukti in (select nobukti from tr_bg2 where nogiro like '%{1}%')", sql, tfind.Text.Trim)
            Case 3
                sql = String.Format("{0} and note like '%{1}%'", sql, tfind.Text.Trim)

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

        Dim nobukti As String = dv1(Me.BindingContext(bs1).Position)("nobukti").ToString

        Dim sql As String = String.Format("update tr_bg set sbatal=1,alasan_batal='{0}' where nobukti='{1}'", alasan.ToUpper, nobukti)

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


            hapus2(cn, sqltrans, nobukti)

            Clsmy.InsertToLog(cn, "btgiro_tolak", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("nobukti").ToString, dv1(Me.BindingContext(bs1).Position)("tanggal").ToString, sqltrans)

            sqltrans.Commit()

            dv1(bs1.Position)("sbatal") = 1

            close_wait()

            MsgBox("Data telah dibatalkan...", vbOKOnly + vbInformation, "Informasi")


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

    Private Sub hapus2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal nobukti As String)

        Dim sql As String = String.Format("SELECT ms_giro.jumlah, ms_giro.jumlahpakai, ms_giro.nogiro " & _
            "FROM tr_bg2 INNER JOIN ms_giro ON tr_bg2.nogiro = ms_giro.nogiro " & _
            "WHERE tr_bg2.nobukti='{0}'", nobukti)

        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        If drd.HasRows Then
            While drd.Read

                Dim sqlgiro As String = String.Format("update ms_giro set stolak=0,tgl_tolak=null where nogiro='{0}'", drd("nogiro").ToString)
                Using cmdgiro As OleDbCommand = New OleDbCommand(sqlgiro, cn, sqltrans)
                    cmdgiro.ExecuteNonQuery()
                End Using


                Dim sqllain2 As String = String.Format("select a.jmlgiro_batal as jmlgiro,b.kd_toko,b.nobukti,a.nogiro,b.kd_karyawan,a.nobukti as nobukti_byr " & _
                    "from trbayar2_giro a inner join trfaktur_to b on a.nobukti_fak=b.nobukti " & _
                    "inner join tr_bg2 c on c.nogiro=a.nogiro " & _
                    "inner join trbayar d on a.nobukti=d.nobukti " & _
                    "where d.sbatal=0 and a.nogiro='{0}' and c.nobukti='{1}'", drd("nogiro").ToString, nobukti)

                Dim cmdlain2 As OleDbCommand = New OleDbCommand(sqllain2, cn, sqltrans)
                Dim drlain2 As OleDbDataReader = cmdlain2.ExecuteReader

                If drlain2.HasRows Then

                    While drlain2.Read

                        Dim totalup2 As Double = Double.Parse(drlain2("jmlgiro").ToString)

                        Dim sqlfak2 As String = String.Format("update trfaktur_to set jmlgiro=jmlgiro + {0} where nobukti='{1}'", totalup2, drlain2("nobukti").ToString)
                        Using cmdfak2 As OleDbCommand = New OleDbCommand(sqlfak2, cn, sqltrans)
                            cmdfak2.ExecuteNonQuery()
                        End Using

                        Dim sqlbayar2 As String = String.Format("update trbayar2_giro set jmlgiro=jmlgiro + {0},jmlgiro_batal=jmlgiro_batal - {0} where nobukti='{1}' and nobukti_fak='{2}' and nogiro='{3}'", totalup2, drlain2("nobukti_byr").ToString, drlain2("nobukti").ToString, drd("nogiro").ToString)
                        Using cmdbayar2 As OleDbCommand = New OleDbCommand(sqlbayar2, cn, sqltrans)
                            cmdbayar2.ExecuteNonQuery()
                        End Using

                    End While

                End If

                drlain2.Close()

            End While
        End If

        drd.Close()

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

    Private Sub cekbatal_onserver()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select sbatal from tr_bg where nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)
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
            MsgBox("Data telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        If MsgBox("Yakin akan dibatalkan ?", vbYesNo + vbQuestion, "Konfirmasi") = vbNo Then
            Exit Sub
        End If

        hapus()

    End Sub

    Private Sub tsadd_Click(sender As System.Object, e As System.EventArgs) Handles tsadd.Click

        IsLoadForm = True

        Using fkar2 As New fgiro_cair2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0, .statview = False, .jenistrans = 2}
            fkar2.ShowDialog()
            IsLoadForm = False
        End Using

    End Sub

    Private Sub tsedit_Click(sender As System.Object, e As System.EventArgs) Handles tsedit.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        If dv1(bs1.Position)("sbatal").ToString.Equals("1") Then
            MsgBox("Data telah dibatalkan...", vbOKOnly + vbExclamation, "Informasi")
            Return
        End If

        IsLoadForm = True

        Using fkar2 As New fgiro_cair2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position, .statview = False, .jenistrans = 2}
            fkar2.ShowDialog()

            IsLoadForm = False

        End Using

    End Sub

    Private Sub tsview_Click(sender As System.Object, e As System.EventArgs) Handles tsview.Click

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        IsLoadForm = True

        Using fkar2 As New fgiro_cair2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position, .statview = True, .jenistrans = 2}

            fkar2.btsimpan.Enabled = False

            fkar2.ShowDialog()

            IsLoadForm = False

        End Using

    End Sub

    Private Sub GridView1_Click(sender As Object, e As System.EventArgs) Handles GridView1.Click
        opengrid_detail()
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        opengrid_detail()
    End Sub


End Class