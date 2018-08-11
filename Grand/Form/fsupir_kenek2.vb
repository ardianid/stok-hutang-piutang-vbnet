Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fsupir_kenek2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Dim kd_spr_old As String = ""
    Dim kd_ken1_old As String = ""
    Dim kd_ken2_old As String = ""
    Dim kd_ken3_old As String = ""
    Dim kd_sales_old As String = ""
    Dim nopol_old As String = ""

    Private Sub kosongkan()

        tkd_sales.Text = ""
        tnama_sales.Text = ""

        tkd_supir.Text = ""
        tnama_supir.Text = ""

        tkd_ken1.Text = ""
        tnama_ken1.Text = ""

        tkd_ken2.Text = ""
        tnama_ken2.Text = ""

        tkd_ken3.Text = ""
        tnama_ken3.Text = ""

    End Sub

    Private Sub isi()

        tnopol.EditValue = dv(position)("nopol").ToString

        tkd_sales.Text = dv(position)("kd_sales").ToString
        tnama_sales.Text = dv(position)("nama_sales").ToString

        tkd_supir.Text = dv(position)("kd_supir").ToString
        tnama_supir.Text = dv(position)("nama_supir").ToString

        tkd_ken1.Text = dv(position)("kd_kenek1").ToString
        tnama_ken1.Text = dv(position)("nama_kenek1").ToString

        tkd_ken2.Text = dv(position)("kd_kenek2").ToString
        tnama_ken2.Text = dv(position)("nama_kenek2").ToString

        tkd_ken3.Text = dv(position)("kd_kenek3").ToString
        tnama_ken3.Text = dv(position)("nama_kenek3").ToString

        kd_spr_old = tkd_supir.Text.Trim
        kd_ken1_old = tkd_ken1.Text.Trim
        kd_ken2_old = tkd_ken2.Text.Trim
        kd_ken3_old = tkd_ken3.Text.Trim

        kd_sales_old = tkd_sales.Text.Trim
        nopol_old = tnopol.EditValue

    End Sub

    Private Function cek_supir(ByVal cn As OleDbConnection) As Boolean

        Dim hasil As Boolean = False

        Dim sql As String = String.Format("select count(kd_supir) from ms_supirkenek where kd_supir='{0}'", tkd_supir.Text.Trim)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        If drd.Read Then
            If IsNumeric(drd(0).ToString) Then

                If addstat = True Then
                    If Integer.Parse(drd(0).ToString) > 0 Then
                        hasil = True
                    End If
                Else
                    If Integer.Parse(drd(0).ToString) > 1 Then
                        hasil = True
                    End If
                End If

            End If
        End If

        Return hasil

    End Function

    Private Function cek_kenek(ByVal cn As OleDbConnection, ByVal kdken As String) As Boolean

        Dim hasil As Boolean = False

        Dim sql As String = String.Format("select count(kd_supir) as jml from ms_supirkenek where kd_kenek1='{0}' or kd_kenek2='{0}' or kd_kenek3='{0}'", kdken)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        If drd.Read Then
            If IsNumeric(drd(0).ToString) Then

                If addstat = True Then

                    If Integer.Parse(drd(0).ToString) > 0 Then
                        hasil = True
                    End If

                Else

                    If Integer.Parse(drd(0).ToString) > 1 Then
                        hasil = True
                    End If

                End If

                

            End If
        End If

        Return hasil

    End Function

    Private Function cek_sales(ByVal cn As OleDbConnection) As Boolean

        Dim hasil As Boolean = False

        Dim sql As String = String.Format("select count(kd_sales) from ms_supirkenek where kd_sales='{0}'", tkd_sales.Text.Trim)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        If drd.Read Then
            If IsNumeric(drd(0).ToString) Then

                If addstat = True Then
                    If Integer.Parse(drd(0).ToString) > 0 Then
                        hasil = True
                    End If
                Else
                    If Integer.Parse(drd(0).ToString) > 1 Then
                        hasil = True
                    End If
                End If

            End If
        End If

        Return hasil

    End Function

    Private Function cek_kendaraan(ByVal cn As OleDbConnection) As Boolean

        Dim hasil As Boolean = False

        Dim sql As String = String.Format("select count(nopol) from ms_supirkenek where nopol='{0}'", tnopol.Text.Trim)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        If drd.Read Then
            If IsNumeric(drd(0).ToString) Then

                If addstat = True Then
                    If Integer.Parse(drd(0).ToString) > 0 Then
                        hasil = True
                    End If
                Else
                    If Integer.Parse(drd(0).ToString) > 1 Then
                        hasil = True
                    End If
                End If

            End If
        End If

        Return hasil

    End Function

    Private Sub isi_nopol()

        Const sql As String = "select * from ms_kendaraan where aktif=1"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet
        Dim dvg As DataView

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvg = dvm.CreateDataView(ds.Tables(0))

            tnopol.Properties.DataSource = dvg

            tnopol.ItemIndex = 0

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

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            'If addstat = True Then
            If cek_supir(cn) = True Then
                MsgBox("Supir sudah ada dalam daftar...", vbOKOnly + vbInformation, "Infromasi")
                tkd_supir.Focus()
                Return
            End If

            If tkd_sales.Text.Trim.Length > 0 Then
                If cek_sales(cn) = True Then
                    MsgBox("Sales sudah ada dalam daftar...", vbOKOnly + vbInformation, "Infromasi")
                    tkd_sales.Focus()
                    Return
                End If
            End If

            If cek_kendaraan(cn) = True Then
                MsgBox("No Polisi sudah ada dalam daftar...", vbOKOnly + vbInformation, "Infromasi")
                tnopol.Focus()
                Return
            End If

            'End If

            If tkd_ken1.Text.Trim.Length > 0 Then
                If cek_kenek(cn, tkd_ken1.Text.Trim) = True Then
                    MsgBox("Kenek I sudah dipakai...", vbOKOnly + vbInformation, "Informasi")
                    tkd_ken1.Focus()
                    Return
                End If
            End If

            If tkd_ken2.Text.Trim.Length > 0 Then
                If cek_kenek(cn, tkd_ken2.Text.Trim) = True Then
                    MsgBox("Kenek II sudah dipakai...", vbOKOnly + vbInformation, "Informasi")
                    tkd_ken2.Focus()
                    Return
                End If
            End If

            If tkd_ken3.Text.Trim.Length > 0 Then
                If cek_kenek(cn, tkd_ken3.Text.Trim) = True Then
                    MsgBox("Kenek III sudah dipakai...", vbOKOnly + vbInformation, "Informasi")
                    tkd_ken3.Focus()
                    Return
                End If
            End If


            sqltrans = cn.BeginTransaction

            Dim comd As OleDbCommand

            If addstat = True Then

                Dim sql_insert As String = String.Format("insert into ms_supirkenek (kd_supir,kd_kenek1,kd_kenek2,kd_kenek3,nopol,kd_sales) values('{0}','{1}','{2}','{3}','{4}','{5}')", tkd_supir.Text.Trim, tkd_ken1.Text.Trim, tkd_ken2.Text.Trim, tkd_ken3.Text.Trim, tnopol.EditValue, tkd_sales.Text.Trim)
                comd = New OleDbCommand(sql_insert, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btsupkenek", 1, 0, 0, 0, tkd_supir.Text.Trim, tkd_ken1.Text.Trim, sqltrans)

                insertview()

            Else

                Dim sql_update As String = String.Format("update ms_supirkenek set kd_kenek1='{0}',kd_kenek2='{1}',kd_kenek3='{2}',nopol='{3}',kd_sales='{4}',kd_supir='{5}' where noid={6}", tkd_ken1.Text.Trim, tkd_ken2.Text.Trim, tkd_ken3.Text.Trim, tnopol.EditValue, tkd_sales.Text.Trim, tkd_supir.Text.Trim, dv(position)("noid").ToString)
                comd = New OleDbCommand(sql_update, cn, sqltrans)
                comd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btsupkenek", 0, 1, 0, 0, tkd_supir.Text.Trim, tkd_ken1.Text.Trim, sqltrans)

                updateview()

            End If

            sqltrans.Commit()
            MsgBox("Data telah disimpan...", vbOKOnly + vbInformation, "Informasi")

            If addstat = True Then
                kosongkan()
                tkd_supir.Focus()
            Else
                Me.Close()
            End If


        Catch ex As Exception
            close_wait()

            If Not IsNothing(sqltrans) Then
                sqltrans.Rollback()
            End If

            MsgBox(ex.ToString)
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try


    End Sub

    Private Sub updateview()

        dv(position)("nopol") = tnopol.EditValue

        dv(position)("kd_sales") = tkd_sales.Text.Trim
        dv(position)("nama_sales") = tnama_sales.Text.Trim

        dv(position)("kd_supir") = tkd_supir.Text.Trim
        dv(position)("nama_supir") = tnama_supir.Text.Trim

        dv(position)("kd_kenek1") = tkd_ken1.Text.Trim
        dv(position)("nama_kenek1") = tnama_ken1.Text.Trim

        dv(position)("kd_kenek2") = tkd_ken2.Text.Trim
        dv(position)("nama_kenek2") = tnama_ken2.Text.Trim

        dv(position)("kd_kenek3") = tkd_ken3.Text.Trim
        dv(position)("nama_kenek3") = tnama_ken3.Text.Trim

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew

        orow("nopol") = tnopol.EditValue

        orow("kd_sales") = tkd_sales.Text.Trim
        orow("nama_sales") = tnama_sales.Text.Trim

        orow("kd_supir") = tkd_supir.Text.Trim
        orow("nama_supir") = tnama_supir.Text.Trim

        orow("kd_kenek1") = tkd_ken1.Text.Trim
        orow("nama_kenek1") = tnama_ken1.Text.Trim

        orow("kd_kenek2") = tkd_ken2.Text.Trim
        orow("nama_kenek2") = tnama_ken2.Text.Trim

        orow("kd_kenek3") = tkd_ken3.Text.Trim
        orow("nama_kenek3") = tnama_ken3.Text.Trim

        dv.EndInit()

    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click


        If tkd_supir.Text.Trim.Length = 0 Then
            MsgBox("Supir tidak boleh kosong...", vbOKOnly + vbExclamation, "Informasi")
            tkd_supir.Focus()
            Return
        End If

        'If tkd_ken1.Text.Trim.Length = 0 Then
        '    MsgBox("Kenek1 tidak boleh kosong...", vbOKOnly + vbExclamation, "Informasi")
        '    tkd_ken1.Focus()
        '    Return
        'End If

        simpan()

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fkab2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tnopol.Focus()
    End Sub

    Private Sub fkab2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        isi_nopol()

        If addstat = True Then

            'tkd_supir.Enabled = True
            'bts_spr.Enabled = True
            kosongkan()
        Else
            'tkd_supir.Enabled = False
            'bts_spr.Enabled = False
            isi()
        End If

    End Sub

    '' sales
    Private Sub tkd_sales_EditValueChanged(sender As Object, e As System.EventArgs) Handles tkd_sales.Validated
        If tkd_sales.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and bagian='SALES'  and kd_karyawan='{0}'", tkd_sales.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_sales.EditValue = dread("kd_karyawan").ToString
                        tnama_sales.EditValue = dread("nama_karyawan").ToString

                    Else
                        tkd_sales.EditValue = ""
                        tnama_sales.EditValue = ""

                    End If
                Else
                    tkd_sales.EditValue = ""
                    tnama_sales.EditValue = ""

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
            bts_sales_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_sales_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_sales.LostFocus
        If tkd_sales.Text.Trim.Length = 0 Then
            tkd_sales.EditValue = ""
            tnama_sales.EditValue = ""
        End If
    End Sub

    Private Sub bts_sales_Click(sender As System.Object, e As System.EventArgs) Handles bts_sles.Click
        Dim fs As New fssales With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_sales.EditValue = fs.get_KODE
        tnama_sales.EditValue = fs.get_NAMA
    End Sub

    '' supir
    Private Sub tkd_supir_EditValueChanged(sender As Object, e As System.EventArgs) Handles tkd_supir.Validated
        If tkd_supir.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and (bagian='SALES' or bagian like 'SUPIR%') and kd_karyawan='{0}'", tkd_supir.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_supir.EditValue = dread("kd_karyawan").ToString
                        tnama_supir.EditValue = dread("nama_karyawan").ToString

                    Else
                        tkd_supir.EditValue = ""
                        tnama_supir.EditValue = ""

                    End If
                Else
                    tkd_supir.EditValue = ""
                    tnama_supir.EditValue = ""

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

    Private Sub tkd_supir_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_supir.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_spr_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_supir_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_supir.LostFocus
        If tkd_supir.Text.Trim.Length = 0 Then
            tkd_supir.EditValue = ""
            tnama_supir.EditValue = ""
        End If
    End Sub

    Private Sub bts_spr_Click(sender As System.Object, e As System.EventArgs) Handles bts_spr.Click
        Dim fs As New fssupir With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .issales = True}
        fs.ShowDialog(Me)

        tkd_supir.EditValue = fs.get_KODE
        tnama_supir.EditValue = fs.get_NAMA
    End Sub

    '' kenek 1

    Private Sub bts_ken1_Click(sender As System.Object, e As System.EventArgs) Handles bts_ken1.Click
        Dim fs As New fskenek With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_ken1.EditValue = fs.get_KODE
        tnama_ken1.EditValue = fs.get_NAMA
    End Sub

    Private Sub tkd_ken1_EditValueChanged(sender As Object, e As System.EventArgs) Handles tkd_ken1.Validated
        If tkd_ken1.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and bagian='KENEK' and kd_karyawan='{0}'", tkd_ken1.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_ken1.EditValue = dread("kd_karyawan").ToString
                        tnama_ken1.EditValue = dread("nama_karyawan").ToString

                    Else
                        tkd_ken1.EditValue = ""
                        tnama_ken1.EditValue = ""

                    End If
                Else
                    tkd_ken1.EditValue = ""
                    tnama_ken1.EditValue = ""

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

    Private Sub tkd_ken1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_ken1.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_ken1_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_ken1_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_ken1.LostFocus
        If tkd_ken1.Text.Trim.Length = 0 Then
            tkd_ken1.EditValue = ""
            tnama_ken1.EditValue = ""
        End If
    End Sub

    '' kenek2

    Private Sub bts_ken2_Click(sender As System.Object, e As System.EventArgs) Handles bts_ken2.Click
        Dim fs As New fskenek With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_ken2.EditValue = fs.get_KODE
        tnama_ken2.EditValue = fs.get_NAMA
    End Sub

    Private Sub tkd_ken2_EditValueChanged(sender As Object, e As System.EventArgs) Handles tkd_ken2.Validated
        If tkd_ken2.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and bagian='KENEK' and kd_karyawan='{0}'", tkd_ken2.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_ken2.EditValue = dread("kd_karyawan").ToString
                        tnama_ken2.EditValue = dread("nama_karyawan").ToString

                    Else
                        tkd_ken2.EditValue = ""
                        tnama_ken2.EditValue = ""

                    End If
                Else
                    tkd_ken2.EditValue = ""
                    tnama_ken2.EditValue = ""

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

    Private Sub tkd_ken2_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_ken2.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_ken2_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_ken2_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_ken2.LostFocus
        If tkd_ken2.Text.Trim.Length = 0 Then
            tkd_ken2.EditValue = ""
            tnama_ken2.EditValue = ""
        End If
    End Sub

    '' kenek 3

    Private Sub bts_ken3_Click(sender As System.Object, e As System.EventArgs) Handles bts_ken3.Click
        Dim fs As New fskenek With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_ken3.EditValue = fs.get_KODE
        tnama_ken3.EditValue = fs.get_NAMA
    End Sub

    Private Sub tkd_ken3_EditValueChanged(sender As Object, e As System.EventArgs) Handles tkd_ken3.Validated
        If tkd_ken3.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and bagian='KENEK' and kd_karyawan='{0}'", tkd_ken3.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_ken3.EditValue = dread("kd_karyawan").ToString
                        tnama_ken3.EditValue = dread("nama_karyawan").ToString

                    Else
                        tkd_ken3.EditValue = ""
                        tnama_ken3.EditValue = ""

                    End If
                Else
                    tkd_ken3.EditValue = ""
                    tnama_ken3.EditValue = ""

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

    Private Sub tkd_ken3_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_ken3.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_ken3_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_ken3_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_ken3.LostFocus
        If tkd_ken3.Text.Trim.Length = 0 Then
            tkd_ken3.EditValue = ""
            tnama_ken3.EditValue = ""
        End If
    End Sub


End Class