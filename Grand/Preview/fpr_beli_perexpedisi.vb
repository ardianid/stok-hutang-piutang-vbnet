Public Class fpr_beli_perexpedisi 

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fpr_beli_perexpedisi_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub fpr_beli_perexpedisi_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        tjenis.SelectedIndex = 1

        ttgl.EditValue = DateValue(Date.Now)
        ttgl2.EditValue = DateValue(Date.Now)

    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        Dim sql As String = "SELECT ms_pegawai.nama_karyawan, count(tradm_gud.nobukti) as jml " & _
        "FROM tradm_gud INNER JOIN ms_pegawai ON tradm_gud.kd_supir = ms_pegawai.kd_karyawan " & _
        "where tradm_gud.sbatal=0 and ms_pegawai.bagian='SUPIR EXPEDISI' and tradm_gud.jenistrans='TR PEMB' and not(ms_pegawai.nama_karyawan like '%WTL%') "

        If tjenis.SelectedIndex = 0 Then
            sql = String.Format(" {0} and tradm_gud.tanggal>='{1}' and tradm_gud.tanggal<='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))
        Else
            sql = String.Format(" {0} and tradm_gud.tglberangkat>='{1}' and tradm_gud.tglberangkat<='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))
        End If


        sql = String.Format(" {0} and nobukti not in (select tradm_gud2.nobukti from tradm_gud2 inner join ms_barang on tradm_gud2.kd_barang=ms_barang.kd_barang where " & _
        "ms_barang.kelompok='PLAT') group by ms_pegawai.nama_karyawan", sql)

        Dim periode As String = ""

        If tjenis.SelectedIndex = 0 Then
            periode = String.Format("Tgl SJ : {0} s.d {1}", convert_date_to_ind(ttgl.EditValue), convert_date_to_ind(ttgl2.EditValue))
        Else
            periode = String.Format("Tgl Sampai : {0} s.d {1}", convert_date_to_ind(ttgl.EditValue), convert_date_to_ind(ttgl2.EditValue))
        End If

        Dim fs As New fpr_beli_perexpedisi2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .tgl = periode}
        fs.ShowDialog()

    End Sub

End Class