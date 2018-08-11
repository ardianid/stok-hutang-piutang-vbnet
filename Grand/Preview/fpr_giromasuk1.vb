Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpr_giromasuk1

    Private Sub isiBank()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Const sql As String = "select alasan from ms_alasan where tipe='BANK' order by alasan asc"
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            tbank.Properties.Items.Clear()

            Dim xi As Integer = 0

            While drd.Read

                If xi = 0 Then
                    tbank.Properties.Items.Add("All")
                End If

                tbank.Properties.Items.Add(drd(0).ToString)

                xi = xi + 1

            End While


            If xi > 0 Then
                tbank.SelectedIndex = 0
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

    Private Sub bts_sal_Click(sender As System.Object, e As System.EventArgs) Handles bts_sal.Click
        Dim fs As New fssales With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_sales.EditValue = fs.get_KODE
        tnama_sales.EditValue = fs.get_NAMA
    End Sub

    Private Sub tkd_sales_EditValueChanged(sender As Object, e As System.EventArgs) Handles tkd_sales.EditValueChanged
        If tkd_sales.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and bagian='SALES' and kd_karyawan='{0}'", tkd_sales.Text.Trim)

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
            bts_sal_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_sales_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_sales.LostFocus
        If tkd_sales.Text.Trim.Length = 0 Then
            tkd_sales.EditValue = ""
            tnama_sales.EditValue = ""

        End If
    End Sub

    Private Sub bts_toko_Click(sender As System.Object, e As System.EventArgs) Handles bts_tko.Click
        Dim fs As New fsoutlet With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .kdsales = ""}
        fs.ShowDialog(Me)

        tkd_toko.EditValue = fs.get_KODE
        tnama_toko.EditValue = fs.get_NAMA
        '   talamat_toko.EditValue = fs.get_ALAMAT

        tkd_toko_EditValueChanged(sender, Nothing)


    End Sub

    Private Sub tkd_toko_EditValueChanged(sender As Object, e As System.EventArgs) Handles tkd_toko.Validated
        If tkd_toko.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select kd_toko,nama_toko,alamat_toko from ms_toko where kd_toko='{0}' and aktif=1", tkd_toko.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_toko.EditValue = dread("kd_toko").ToString
                        tnama_toko.EditValue = dread("nama_toko").ToString
                        '    talamat_toko.EditValue = dread("alamat_toko").ToString

                    Else
                        tkd_toko.EditValue = ""
                        tnama_toko.EditValue = ""
                        '   talamat_toko.Text = ""


                    End If
                Else
                    tkd_toko.EditValue = ""
                    tnama_toko.EditValue = ""
                    '    talamat_toko.Text = ""

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

    Private Sub tkd_toko_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_toko.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_toko_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_toko_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_toko.LostFocus
        If tkd_toko.Text.Trim.Length = 0 Then
            tkd_toko.EditValue = ""
            tnama_toko.EditValue = ""
            '    talamat_toko.Text = ""
        End If
    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        Cursor = Cursors.WaitCursor

        Dim sql As String = String.Format("SELECT     ms_giro.nogiro, ms_giro.tgl_giro, ms_giro.namabank, ms_toko.kd_toko, ms_toko.nama_toko, ms_pegawai.kd_karyawan, ms_pegawai.nama_karyawan, " & _
        "ms_giro.jumlah, ms_giro.jumlahpakai, ms_giro.tgl_cair, ms_giro.tgl_tolak " & _
        "FROM         ms_giro INNER JOIN " & _
                      "ms_toko ON ms_giro.kd_toko = ms_toko.kd_toko INNER JOIN " & _
                      "ms_pegawai ON ms_giro.kd_karyawan = ms_pegawai.kd_karyawan where ms_giro.sbatal=0 and ms_giro.tgl_giro >='{0}' and ms_giro.tgl_giro <='{1}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If Not tbank.Text.Trim.Equals("All") Then

            sql = String.Format("{0} and ms_giro.namabank='{1}'", sql, tbank.Text.Trim)

        End If

        If tkd_toko.Text.Trim.Length > 0 Then
            sql = String.Format("{0} and ms_toko.kd_toko='{1}'", sql, tkd_toko.Text.Trim)
        End If

        If tkd_sales.Text.Trim.Length > 0 Then
            sql = String.Format("{0} and ms_pegawai.kd_karyawan='{1}'", sql, tkd_sales.Text.Trim)
        End If


        Dim periode As String = String.Format("{0} s.d {1}", convert_date_to_ind(ttgl.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Dim fs As New fpr_giromasuk2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .tgl = periode}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub

    Private Sub fpr_giromasuk1_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub fpr_giromasuk1_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ttgl.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

        isiBank()

    End Sub


End Class