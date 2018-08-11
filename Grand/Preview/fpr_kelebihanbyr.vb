Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpr_kelebihanbyr

    Private Sub tkode_tko_LostFocus(sender As Object, e As System.EventArgs) Handles tkode_tko.LostFocus
        If tkode_tko.Text.Trim.Length = 0 Then
            tnama_tko.Text = ""
            talamat_tko.Text = ""
        End If
    End Sub

    Private Sub tkode_tko_Validated(sender As Object, e As System.EventArgs) Handles tkode_tko.Validated

        If tkode_tko.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select kd_toko,nama_toko,alamat_toko from ms_toko where kd_toko='{0}' and aktif=1", tkode_tko.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkode_tko.EditValue = dread("kd_toko").ToString
                        tnama_tko.EditValue = dread("nama_toko").ToString
                        talamat_tko.EditValue = dread("alamat_toko").ToString

                    Else
                        tkode_tko.EditValue = ""
                        tnama_tko.EditValue = ""
                        talamat_tko.Text = ""


                    End If
                Else
                    tkode_tko.EditValue = ""
                    tnama_tko.EditValue = ""
                    talamat_tko.Text = ""

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

    Private Sub bts_supir_Click(sender As System.Object, e As System.EventArgs) Handles bts_supir.Click

        Dim fs As New fsoutlet With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .kdsales = ""}
        fs.ShowDialog(Me)

        tkode_tko.EditValue = fs.get_KODE
        tnama_tko.EditValue = fs.get_NAMA
        talamat_tko.EditValue = fs.get_ALAMAT

        tkode_tko_Validated(sender, Nothing)

    End Sub

    Private Sub tkd_toko_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkode_tko.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_supir_Click(sender, Nothing)
        End If
    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        'If tkode_tko.Text.Trim.Length = 0 Then
        '    MsgBox("Outlet harus diisi...", vbOKOnly + vbInformation, "Informasi")
        '    tkode_tko.Focus()
        '    Return
        'End If

        Cursor = Cursors.WaitCursor

        Dim sql As String = "select trfaktur_to.nobukti,ms_toko.nama_toko as outlet, " & _
        "trfaktur_to.tanggal,trfaktur_to.netto,trfaktur_to.jmlbayar + trfaktur_to.jmlgiro_real as jmlbayar, trfaktur_to.jmlkelebihan - trfaktur_to.jmlkelebihan_pot as sisapot " & _
        "from trfaktur_to inner join ms_toko on trfaktur_to.kd_toko=ms_toko.kd_toko inner join ms_pegawai on trfaktur_to.kd_karyawan=ms_pegawai.kd_karyawan " & _
        "where (trfaktur_to.jmlkelebihan - trfaktur_to.jmlkelebihan_pot) > 0"

        If tkode_tko.Text.Trim.Length > 0 Then
            sql = String.Format("{0} and ms_toko.kd_toko='{1}'", sql, tkode_tko.Text.Trim)
        End If

        Dim fs As New fpr_kelebihanbyr2 With {.WindowState = FormWindowState.Maximized, .sql = sql}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub

    Private Sub fpr_kelebihanbyr_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tkode_tko.Focus()
    End Sub

End Class