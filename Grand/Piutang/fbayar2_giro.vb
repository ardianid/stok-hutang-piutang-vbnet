Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fbayar2_giro

    Public dt As DataTable
    Public dv As DataView
    Public posisi As Integer
    Public nobukti As String
    Public statview As Boolean

    Private dvmanager As Data.DataViewManager
    Private dvgiro As Data.DataView

    Private Sub isi()

        ttoko.EditValue = dv(posisi)("nama_toko").ToString
        talamat.EditValue = dv(posisi)("alamat_toko").ToString
        tfaktur.EditValue = dv(posisi)("nobukti").ToString
        ttgl.EditValue = convert_date_to_ind(dv(posisi)("tanggal").ToString)
        tjumlah.EditValue = dv(posisi)("netto").ToString

        open_giropakai()

        For i As Integer = 0 To dt.Rows.Count - 1

            If dt.Rows(i)("nobukti_fak") = tfaktur.Text.Trim Then

                Dim orow As DataRowView = dvgiro.AddNew

                orow("nobukti_fak") = dt.Rows(i)("nobukti_fak").ToString
                orow("jumlah") = dt.Rows(i)("jumlah").ToString
                orow("nogiro") = dt.Rows(i)("nogiro").ToString
                orow("jmlgiro") = dt.Rows(i)("jmlgiro").ToString
                orow("namabank") = dt.Rows(i)("namabank").ToString
                orow("tgl_giro") = dt.Rows(i)("tgl_giro").ToString
                orow("nama_karyawan") = dt.Rows(i)("nama_karyawan").ToString

                dvgiro.EndInit()

            End If

            

        Next


    End Sub

    Private Sub open_giropakai()

        Dim sql As String = String.Format("select a.nobukti_fak,b.jumlah, a.nogiro,a.jmlgiro,b.namabank,b.tgl_giro,ms_pegawai.nama_karyawan " & _
            "from trbayar2_giro a inner join ms_giro b on a.nogiro=b.nogiro inner join ms_pegawai on b.kd_karyawan=ms_pegawai.kd_karyawan " & _
            "where b.sbatal=0 and a.nobukti='{0}'", "<< New >>")

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dvgiro = dvmanager.CreateDataView(ds.Tables(0))

            grid1.DataSource = dvgiro

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

    Private Sub insert_view()

        For i As Integer = 0 To dvgiro.Count - 1

            If dvgiro(i)("tgl_giro").ToString.Length > 0 Then

                Dim jmgiro As String = dvgiro(i)("jmlgiro").ToString

                If IsNumeric(jmgiro) Then

                    If jmgiro > 0 Then

                        Dim rows() As DataRow = dt.Select(String.Format("nobukti_fak='{0}' and nogiro='{1}'", tfaktur.Text.Trim, dvgiro(i)("nogiro").ToString))

                        If rows.Length <= 0 Then

                            Dim orow As DataRow = dt.NewRow
                            orow("nobukti_fak") = tfaktur.Text.Trim
                            orow("nogiro") = dvgiro(i)("nogiro").ToString
                            orow("jumlah") = dvgiro(i)("jumlah").ToString
                            orow("jmlgiro") = dvgiro(i)("jmlgiro").ToString
                            orow("namabank") = dvgiro(i)("namabank").ToString
                            orow("tgl_giro") = dvgiro(i)("tgl_giro").ToString
                            orow("nama_karyawan") = dvgiro(i)("nama_karyawan").ToString

                            dt.Rows.Add(orow)

                        Else

                            rows(0)("nogiro") = dvgiro(i)("nogiro").ToString
                            rows(0)("jumlah") = dvgiro(i)("jumlah").ToString
                            rows(0)("jmlgiro") = dvgiro(i)("jmlgiro").ToString
                            rows(0)("namabank") = dvgiro(i)("namabank").ToString
                            rows(0)("tgl_giro") = dvgiro(i)("tgl_giro").ToString
                            rows(0)("nama_karyawan") = dvgiro(i)("nama_karyawan").ToString

                        End If

                    End If

                End If

            End If

        Next

    End Sub

    Private Sub ceknogiro(ByVal nogiro As String, addnew As Boolean)

        Dim cn As OleDbConnection = Nothing

        Try


            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select ms_giro.nogiro,ms_giro.tgl_giro,ms_giro.namabank,ms_pegawai.nama_karyawan,ms_giro.jumlah " & _
        "from ms_giro inner join ms_pegawai on ms_giro.kd_karyawan=ms_pegawai.kd_karyawan " & _
        "inner join ms_toko on ms_giro.kd_toko=ms_toko.kd_toko " & _
        "where ms_giro.scair = 0 And ms_giro.stolak = 0 and  ms_giro.jumlah > ms_giro.jumlahpakai and ms_giro.nogiro='{0}'", nogiro)



            If dv(posisi)("kd_group").ToString.Equals("-") Then
                sql = String.Format("{0} and ms_toko.kd_toko='{1}'", sql, dv(posisi)("kd_toko").ToString)
            Else
                sql = String.Format("{0} and (ms_toko.kd_toko='{1}' or ms_toko.kd_group='{2}')", sql, dv(posisi)("kd_toko").ToString, dv(posisi)("kd_group").ToString)
            End If

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader


            If drd.Read Then

                If drd("nogiro").ToString.Equals("") Then
                    GoTo kosongkan

                Else


                    If addnew = True Then

                        Dim orow As DataRowView = dvgiro.AddNew

                        orow("nobukti_fak") = tfaktur.Text.Trim
                        orow("nogiro") = drd("nogiro").ToString
                        orow("tgl_giro") = drd("tgl_giro").ToString
                        orow("namabank") = drd("namabank").ToString
                        orow("nama_karyawan") = drd("nama_karyawan").ToString
                        orow("jumlah") = drd("jumlah").ToString
                        orow("jmlgiro") = 0

                        dvgiro.EndInit()

                    Else

                        dvgiro(Me.BindingContext(dvgiro).Position)("nobukti_fak") = tfaktur.Text.Trim
                        dvgiro(Me.BindingContext(dvgiro).Position)("nogiro") = drd("nogiro").ToString
                        dvgiro(Me.BindingContext(dvgiro).Position)("tgl_giro") = drd("tgl_giro").ToString
                        dvgiro(Me.BindingContext(dvgiro).Position)("namabank") = drd("namabank").ToString
                        dvgiro(Me.BindingContext(dvgiro).Position)("nama_karyawan") = drd("nama_karyawan").ToString
                        dvgiro(Me.BindingContext(dvgiro).Position)("jumlah") = drd("jumlah").ToString
                        dvgiro(Me.BindingContext(dvgiro).Position)("jmlgiro") = 0

                    End If


                   

                    GoTo finishing

                End If

            Else
                GoTo kosongkan
            End If


kosongkan:

            If addnew = False Then

                dvgiro.Delete(Me.BindingContext(dvgiro).Position)

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

        If GridView1.FocusedColumn.FieldName = "nogiro" Then


            Dim dtsel As DataTable = New DataTable
            dtsel = dvgiro.ToTable
            Dim rows() As DataRow = dtsel.Select(String.Format("nogiro='{0}'", e.Value))

            If rows.Length > 1 Then
                MsgBox("Giro sudah ada dalam daftar...", vbOKOnly + vbInformation, "Informasi")
                dvgiro.Delete(Me.BindingContext(dvgiro).Position)

            Else
                ceknogiro(e.Value, False)
            End If

        End If

    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As System.EventArgs) Handles GridView1.DoubleClick
        If GridView1.FocusedColumn.FieldName = "nogiro" Then

            btfind_giro_ButtonClick(sender, Nothing)

        End If
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Delete Then
            btdel_giro_Click(sender, Nothing)
        ElseIf e.KeyCode = Keys.F4 Then

            If GridView1.FocusedColumn.FieldName = "nogiro" Then

                btfind_giro_ButtonClick(sender, Nothing)

            End If

        End If
    End Sub

    Private Sub fbayar2_giro_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        grid1.Focus()
    End Sub

    Private Sub fbayar2_giro_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        isi()

        If statview = True Then

            GridView1.Columns("nogiro").OptionsColumn.AllowEdit = False
            GridView1.Columns("nogiro").OptionsColumn.AllowFocus = False

            btdel_giro.Enabled = False
            btsimpan.Enabled = False
        End If

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Public ReadOnly Property get_jmlgiro As String
        Get

            If IsNothing(dvgiro) Then
                Return 0
                Exit Property
            End If

            If dvgiro.Count <= 0 Then
                Return 0
                Exit Property
            End If

            Dim totjumlah As Double = 0

            For i As Integer = 0 To dvgiro.Count - 1

                If IsNumeric(dvgiro(i)("jmlgiro").ToString) Then
                    totjumlah = totjumlah + Double.Parse(dvgiro(i)("jmlgiro").ToString)
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

        If IsNothing(dvgiro) Then
            Return
        End If

        If dvgiro.Count <= 0 Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn


            sqltrans = cn.BeginTransaction

            ' delete giro bayar

            Dim nogiro As String = dvgiro(Me.BindingContext(dvgiro).Position)("nogiro").ToString
            Dim jmlgiro As Double = dvgiro(Me.BindingContext(dvgiro).Position)("jmlgiro").ToString

            Dim sql_giro As String = String.Format("select * from trbayar2_giro where nobukti='{0}' and nobukti_fak='{1}' and nogiro='{2}'", nobukti, tfaktur.Text.Trim, nogiro)
            Dim cmdgiro As OleDbCommand = New OleDbCommand(sql_giro, cn, sqltrans)
            Dim drgiro As OleDbDataReader = cmdgiro.ExecuteReader

            If drgiro.HasRows Then

                While drgiro.Read

                    Dim jmlbayargiro As Double = Double.Parse(drgiro("jmlgiro").ToString)

                    Dim sqlgiro As String = String.Format("update ms_giro set jumlahpakai=jumlahpakai - {0} where nogiro='{1}'", jmlbayargiro, drgiro("nogiro").ToString)
                    Using cmdgiro2 As OleDbCommand = New OleDbCommand(sqlgiro, cn, sqltrans)
                        cmdgiro2.ExecuteNonQuery()
                    End Using

                    Dim sqlcek_giro As String = String.Format("select jumlahpakai from ms_giro where nogiro='{0}'", drgiro("nogiro").ToString)
                    Dim cmdcek_giro As OleDbCommand = New OleDbCommand(sqlcek_giro, cn, sqltrans)
                    Dim drcek_giro As OleDbDataReader = cmdcek_giro.ExecuteReader

                    If drcek_giro.Read Then

                        If IsNumeric(drcek_giro(0).ToString) Then

                            If drcek_giro(0) <= 0 Then

                                Dim sqlgunagiro As String = String.Format("update ms_giro set sgunakan=0 where nogiro='{0}'", drgiro("nogiro").ToString)
                                Using cmdgunagiro As OleDbCommand = New OleDbCommand(sqlgunagiro, cn, sqltrans)
                                    cmdgunagiro.ExecuteNonQuery()
                                End Using

                            End If

                        End If

                    End If
                    drcek_giro.Close()

                End While

            End If

            drgiro.Close()

            Dim sqldel As String = String.Format("delete from trbayar2_giro where nobukti='{0}' and nobukti_fak='{1}' and nogiro='{2}'", nobukti, tfaktur.Text.Trim, nogiro)
            Using cmddel As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
                cmddel.ExecuteNonQuery()
            End Using

            For i As Integer = 0 To dvgiro.Count - 1

                If dvgiro(i)("nobukti_fak").ToString.Equals(tfaktur.Text.Trim) And dvgiro(i)("nogiro").ToString.Equals(nogiro) Then
                    dvgiro(i).Delete()
                End If

            Next


            For i As Integer = 0 To dt.Rows.Count - 1

                If dt.Rows(i)("nobukti_fak").ToString.Equals(tfaktur.Text.Trim) And dt.Rows(i)("nogiro").ToString.Equals(nogiro) Then
                    dt.Rows.RemoveAt(i)
                End If

            Next


            sqltrans.Commit()
            ' ------------------- akhir delete giro

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

    Private Sub btfind_giro_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles btfind_giro.ButtonClick


        Dim fs As New fbayar2_giro2 With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .kdtoko = dv(posisi)("kd_toko").ToString, .kd_group = dv(posisi)("kd_group").ToString}
        fs.ShowDialog(Me)

        Dim nogiro As String = fs.get_nogiro

        If nogiro = "" Then
            Return
        End If

        If GridView1.IsNewItemRow(GridView1.FocusedRowHandle) Then

            Dim dtsel As DataTable = New DataTable
            dtsel = dvgiro.ToTable
            Dim rows() As DataRow = dtsel.Select(String.Format("nogiro='{0}'", nogiro))

            If rows.Length > 0 Then
                MsgBox("Giro sudah ada dalam daftar...", vbOKOnly + vbInformation, "Informasi")
            Else
                ceknogiro(nogiro, True)
            End If

        Else
            ceknogiro(nogiro, False)
        End If

        

    End Sub

End Class