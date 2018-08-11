Public Class fpr_pegawai 

    Private Sub fpr_pegawai_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        cbgol.Focus()
    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        Cursor = Cursors.WaitCursor

        Dim sql As String = "select kd_karyawan,nama_karyawan,bagian,alamat from ms_pegawai	where aktif=1"

        If Not cbgol.EditValue.Equals("ALL") Then
            sql = String.Format("{0} and bagian='{1}'", sql, cbgol.EditValue)
        End If

        Dim fs As New fpr_pegawai2 With {.WindowState = FormWindowState.Maximized, .sql = sql}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fpr_pegawai_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        cbgol.SelectedIndex = 0
    End Sub

End Class