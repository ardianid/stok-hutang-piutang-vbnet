Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fbayar2_retur

    Public dt As DataTable
    Public dv As DataView
    Public posisi As Integer
    Public nobukti As String

    Public statview As Boolean

    Private dvmanager As Data.DataViewManager
    Private dvretur As Data.DataView

    Private Sub isi()

        ttoko.EditValue = dv(posisi)("nama_toko").ToString
        talamat.EditValue = dv(posisi)("alamat_toko").ToString
        tfaktur.EditValue = dv(posisi)("nobukti").ToString
        ttgl.EditValue = convert_date_to_ind(dv(posisi)("tanggal").ToString)
        tjumlah.EditValue = dv(posisi)("netto").ToString

        open_returpakai()

        For i As Integer = 0 To dt.Rows.Count - 1

            If dt.Rows(i)("nobukti_fak") = tfaktur.Text.Trim Then

                Dim orow As DataRowView = dvretur.AddNew

                orow("nobukti_fak") = dt.Rows(i)("nobukti_fak").ToString
                orow("nobukti_ret") = dt.Rows(i)("nobukti_ret").ToString
                orow("tanggal") = dt.Rows(i)("tanggal").ToString
                orow("alasan_retur") = dt.Rows(i)("alasan_retur").ToString
                orow("netto") = dt.Rows(i)("netto").ToString
                orow("jmlretur") = dt.Rows(i)("jmlretur").ToString

                dvretur.EndInit()

            End If

        Next

    End Sub

    Private Sub open_returpakai()

        Dim sql As String = String.Format("select x1.nobukti_fak,x1.nobukti_ret,x1.jmlretur,x2.tanggal,x2.alasan_retur,x2.netto " & _
            "from trbayar2_ret x1 inner join trretur x2 on x1.nobukti_ret=x2.nobukti " & _
            "where x1.nobukti='{0}'", "<< New >>")

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dvretur = dvmanager.CreateDataView(ds.Tables(0))

            grid1.DataSource = dvretur

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

        'Dim dt As DataTable = New DataTable("test")
        'dt.Columns.Add("nilai", Type.GetType("system.int32"))

        'Dim orow As DataRow = dt.NewRow
        'orow("nilai") = 1

        'dt.Rows.Add(orow)

        'orow("nilai") = 2

        'dt.Rows.Add(orow)

        'orow("nilai") = 3

        'dt.Rows.Add(orow)


        'Dim rata2 As String = dt.Compute("avg (nilai)", String.Empty).ToString
        'Dim min As String = dt.Compute("min (nilai)", String.Empty).ToString
        'Dim max As String = dt.Compute("max (nilai)", String.Empty).ToString


    End Sub

    Private Sub insert_view()

        For i As Integer = 0 To dvretur.Count - 1

            If dvretur(i)("tanggal").ToString.Length > 0 Then

                Dim jmretur As String = dvretur(i)("jmlretur").ToString
                If IsNumeric(jmretur) Then

                    If jmretur > 0 Then

                        Dim rows() As DataRow = dt.Select(String.Format("nobukti_fak='{0}' and nobukti_ret='{1}'", tfaktur.Text.Trim, dvretur(i)("nobukti_ret").ToString))

                        If rows.Length <= 0 Then

                            Dim orow As DataRow = dt.NewRow
                            orow("nobukti_fak") = tfaktur.Text.Trim
                            orow("nobukti_ret") = dvretur(i)("nobukti_ret").ToString

                            orow("tanggal") = dvretur(i)("tanggal").ToString
                            orow("alasan_retur") = dvretur(i)("alasan_retur").ToString
                            orow("netto") = dvretur(i)("netto").ToString
                            orow("jmlretur") = dvretur(i)("jmlretur").ToString

                            dt.Rows.Add(orow)

                        Else

                            rows(0)("nobukti_ret") = dvretur(i)("nobukti_ret").ToString
                            rows(0)("tanggal") = dvretur(i)("tanggal").ToString
                            rows(0)("alasan_retur") = dvretur(i)("alasan_retur").ToString
                            rows(0)("netto") = dvretur(i)("netto").ToString
                            rows(0)("jmlretur") = dvretur(i)("jmlretur").ToString

                        End If

                    End If

                End If

            End If

        Next

    End Sub

    Private Sub ceknoretur(ByVal noretur As String, addnew As Boolean)

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select trretur.nobukti,trretur.tanggal,trretur.alasan_retur,trretur.netto,trretur.jmlpotong from trretur inner join ms_toko on trretur.kd_toko=ms_toko.kd_toko " & _
            "where trretur.netto > 0 and trretur.netto > trretur.jmlpotong and sbatal=0 and trretur.nobukti='{0}'", noretur)

            If dv(posisi)("kd_group").ToString.Equals("-") Then
                sql = String.Format("{0} and ms_toko.kd_toko='{1}'", sql, dv(posisi)("kd_toko").ToString)
            Else
                sql = String.Format("{0} and (ms_toko.kd_toko='{1}' or ms_toko.kd_group='{2}')", sql, dv(posisi)("kd_toko").ToString, dv(posisi)("kd_group").ToString)
            End If

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader


            If drd.Read Then

                If drd("nobukti").ToString.Equals("") Then
                    GoTo kosongkan

                Else


                    If addnew = True Then

                        Dim orow As DataRowView = dvretur.AddNew

                        orow("nobukti_fak") = tfaktur.Text.Trim
                        orow("nobukti_ret") = drd("nobukti").ToString
                        orow("tanggal") = drd("tanggal").ToString
                        orow("alasan_retur") = drd("alasan_retur").ToString
                        orow("netto") = drd("netto").ToString
                        orow("jmlretur") = 0

                        dvretur.EndInit()

                    Else

                        dvretur(Me.BindingContext(dvretur).Position)("nobukti_fak") = tfaktur.Text.Trim
                        dvretur(Me.BindingContext(dvretur).Position)("nobukti_ret") = drd("nobukti").ToString
                        dvretur(Me.BindingContext(dvretur).Position)("tanggal") = drd("tanggal").ToString
                        dvretur(Me.BindingContext(dvretur).Position)("alasan_retur") = drd("alasan_retur").ToString
                        dvretur(Me.BindingContext(dvretur).Position)("netto") = drd("netto").ToString
                        dvretur(Me.BindingContext(dvretur).Position)("jmlretur") = 0

                    End If




                    GoTo finishing

                End If

            Else
                GoTo kosongkan
            End If


kosongkan:

            If addnew = False Then

                dvretur.Delete(Me.BindingContext(dvretur).Position)

            End If


finishing:

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

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If e.Column.FieldName = "nobukti_ret" Then

            Dim dtsel As DataTable = New DataTable
            dtsel = dvretur.ToTable
            Dim rows() As DataRow = dtsel.Select(String.Format("nobukti_ret='{0}'", e.Value))

            If rows.Length > 1 Then
                MsgBox("Retur sudah ada dalam daftar...", vbOKOnly + vbInformation, "Informasi")
                dvretur.Delete(Me.BindingContext(dvretur).Position)

            Else
                ceknoretur(e.Value, False)
            End If

        End If

    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As System.EventArgs) Handles GridView1.DoubleClick
        If GridView1.FocusedColumn.FieldName = "nobukti_ret" Then

            rbutton_edit_ButtonClick(sender, Nothing)

        End If
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Delete Then
            btdel_giro_Click(sender, Nothing)
        ElseIf e.KeyCode = Keys.F4 Then

            If GridView1.FocusedColumn.FieldName = "nobukti_ret" Then

                rbutton_edit_ButtonClick(sender, Nothing)

            End If

        End If
    End Sub

    Private Sub fbayar2_giro_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        grid1.Focus()
    End Sub

    Private Sub fbayar2_giro_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        isi()

        If statview = True Then

            GridView1.Columns("nobukti_ret").OptionsColumn.AllowEdit = False
            GridView1.Columns("nobukti_ret").OptionsColumn.AllowFocus = False

            btdel_giro.Enabled = False
            btsimpan.Enabled = False
        End If

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Public ReadOnly Property get_jmlgiro As String
        Get

            If IsNothing(dvretur) Then
                Return 0
                Exit Property
            End If

            If dvretur.Count <= 0 Then
                Return 0
                Exit Property
            End If

            Dim totjumlah As Double = 0

            For i As Integer = 0 To dvretur.Count - 1

                If IsNumeric(dvretur(i)("jmlretur").ToString) Then
                    totjumlah = totjumlah + Double.Parse(dvretur(i)("jmlretur").ToString)
                End If

            Next

            Return totjumlah

        End Get
    End Property

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        insert_view()
        Me.Close()

    End Sub

    Private Sub btdel_giro_Click(sender As System.Object, e As System.EventArgs) Handles btdel_giro.Click

        If statview = True Then
            Return
        End If

        If IsNothing(dvretur) Then
            Return
        End If

        If dvretur.Count <= 0 Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing



        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn


            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            ' delete giro bayar

            Dim noretur As String = dvretur(Me.BindingContext(dvretur).Position)("nobukti_ret").ToString
            Dim jmlretur As Double = dvretur(Me.BindingContext(dvretur).Position)("jmlretur").ToString

            Dim sql_giro As String = String.Format("select * from trbayar2_ret where nobukti='{0}' and nobukti_fak='{1}' and nobukti_ret='{2}'", nobukti, tfaktur.Text.Trim, noretur)
            Dim cmdgiro As OleDbCommand = New OleDbCommand(sql_giro, cn, sqltrans)
            Dim drretur As OleDbDataReader = cmdgiro.ExecuteReader

            Dim ada As Boolean = False

            If drretur.HasRows Then

                While drretur.Read

                    ada = True

                    Dim jmlbayar_retur As Double = Double.Parse(drretur("jmlretur").ToString)

                    Dim sqlup_retur As String = String.Format("update trretur set jmlpotong=jmlpotong - {0} where nobukti='{1}'", jmlbayar_retur, drretur("nobukti_ret").ToString)
                    Using cmdup_retur As OleDbCommand = New OleDbCommand(sqlup_retur, cn, sqltrans)
                        cmdup_retur.ExecuteNonQuery()
                    End Using

                    Dim sqlcek_retur As String = String.Format("select jmlpotong from trretur where nobukti='{0}'", drretur("nobukti_ret").ToString)
                    Dim cmdcek_retur As OleDbCommand = New OleDbCommand(sqlcek_retur, cn, sqltrans)
                    Dim drcek_retur As OleDbDataReader = cmdcek_retur.ExecuteReader

                    If drcek_retur.Read Then

                        If IsNumeric(drcek_retur(0).ToString) Then

                            If drcek_retur(0) <= 0 Then

                                Dim sqlgunaretur As String = String.Format("update trretur set spotong=0 where nobukti='{0}'", drretur("nobukti_ret").ToString)
                                Using cmdgunaretur As OleDbCommand = New OleDbCommand(sqlgunaretur, cn, sqltrans)
                                    cmdgunaretur.ExecuteNonQuery()
                                End Using

                            End If

                        End If

                    End If
                    drcek_retur.Close()

                End While

            End If

            drretur.Close()

            Dim sqldel As String = String.Format("delete from trbayar2_ret where nobukti='{0}' and nobukti_fak='{1}' and nobukti_ret='{2}'", nobukti, tfaktur.Text.Trim, noretur)
            Using cmddel As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
                cmddel.ExecuteNonQuery()
            End Using

            For i As Integer = 0 To dvretur.Count - 1

                If dvretur(i)("nobukti_fak").ToString.Equals(tfaktur.Text.Trim) And dvretur(i)("nobukti_ret").ToString.Equals(noretur) Then
                    dvretur(i).Delete()
                End If

            Next


            For i As Integer = 0 To dt.Rows.Count - 1

                If dt.Rows(i)("nobukti_fak").ToString.Equals(tfaktur.Text.Trim) And dt.Rows(i)("nobukti_ret").ToString.Equals(noretur) Then
                    dt.Rows.RemoveAt(i)
                End If

            Next


            If ada = True Then

                '1. update faktur
                Dim sqlup_faktur As String = String.Format("update trbayar set total=total - {0} where nobukti='{1}'", jmlretur, nobukti)

                Using cmd As OleDbCommand = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using


            End If

            sqltrans.Commit()

            ' ------------------- akhir delete giro

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

    Private Sub rbutton_edit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rbutton_edit.ButtonClick

        Dim fs As New fbayar2_retur2 With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .kdtoko = dv(posisi)("kd_toko").ToString, .kd_group = dv(posisi)("kd_group").ToString}
        fs.ShowDialog(Me)

        Dim nogiro As String = fs.get_noretur

        If nogiro = "" Then
            Return
        End If

        If GridView1.IsNewItemRow(GridView1.FocusedRowHandle) Then

            Dim dtsel As DataTable = New DataTable
            dtsel = dvretur.ToTable
            Dim rows() As DataRow = dtsel.Select(String.Format("nobukti_ret='{0}'", nogiro))

            If rows.Length > 0 Then
                MsgBox("Retur sudah ada dalam daftar...", vbOKOnly + vbInformation, "Informasi")
            Else
                ceknoretur(nogiro, True)
            End If

        Else
            ceknoretur(nogiro, False)
        End If

    End Sub



End Class