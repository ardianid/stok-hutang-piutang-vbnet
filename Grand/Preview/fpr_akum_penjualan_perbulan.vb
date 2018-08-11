Public Class fpr_akum_penjualan_perbulan

    Private Sub fpr_akum_penjualan_perbulan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        ttahun.Focus()
    End Sub

    Private Sub fpr_akum_penjualan_perbulan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ttahun.EditValue = Year(Date.Now)
        tbln1.EditValue = Month(Date.Now)
        tbln2.EditValue = Month(Date.Now)

    End Sub

    Private Sub btclose_Click(sender As Object, e As EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btload_Click(sender As Object, e As EventArgs) Handles btload.Click

        If ttahun.EditValue = 0 Then
            MsgBox("Tahun Salah..", vbOKOnly + vbInformation, "Informasi")
            ttahun.Focus()
            Return
        End If

        If tbln1.EditValue = 0 Then
            MsgBox("Bln Salah..", vbOKOnly + vbInformation, "Informasi")
            tbln1.Focus()
            Return
        End If

        If tbln2.EditValue = 0 Then
            MsgBox("Bln Salah..", vbOKOnly + vbInformation, "Informasi")
            tbln2.Focus()
            Return
        End If

        If tbln2.EditValue < tbln1.EditValue Then
            MsgBox("Bulan 2 tidak boleh lebih kecil dari bulan 1", vbOKOnly + vbInformation, "Informasi")
            tbln2.Focus()
            Return
        End If

        Dim sql As String = String.Format("select month(trfaktur_to.tanggal) as bln, " & _
        "case when month(trfaktur_to.tanggal)=1 then " & _
        " 'Januari' " & _
        "when month(trfaktur_to.tanggal) =2 then " & _
        "'Februari' " & _
        "when month(trfaktur_to.tanggal)=3 then " & _
        "'Maret' " & _
        "when month(trfaktur_to.tanggal)=4 then " & _
        "'April' " & _
        "when month(trfaktur_to.tanggal)=5 then " & _
        "'Mei' " & _
        "when month(trfaktur_to.tanggal)=6 then " & _
        "'Juni' " & _
        "when month(trfaktur_to.tanggal)=7 then " & _
        "'Juli' " & _
        "when month(trfaktur_to.tanggal)=8 then " & _
        "'Agustus' " & _
        "when month(trfaktur_to.tanggal)=9 then " & _
        "'September' " & _
        "when month(trfaktur_to.tanggal)=10 then " & _
        "'Oktober' " & _
        "when month(trfaktur_to.tanggal)=11 then " & _
        "'November' " & _
        "else 'Desember' end as nama_bln, " & _
        "ms_barang.kd_barang,ms_barang.nama_lap, " & _
        "sum(trfaktur_to2.qtykecil) / (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3) as jml " & _
        "from trfaktur_to inner join trfaktur_to2 on trfaktur_to.nobukti=trfaktur_to2.nobukti " & _
        "inner join ms_barang on trfaktur_to2.kd_barang=ms_barang.kd_barang " & _
        "where ms_barang.jenis='FISIK' and trfaktur_to.sbatal=0 and trfaktur_to.statkirim='TERKIRIM' " & _
        "and YEAR(trfaktur_to.tanggal)={0} and month(trfaktur_to.tanggal)>={1} and month(trfaktur_to.tanggal)<={2} " & _
        "group by trfaktur_to.tanggal,ms_barang.kd_barang,ms_barang.nama_lap, " & _
        "ms_barang.qty1,ms_barang.qty2,ms_barang.qty3 ", ttahun.EditValue, tbln1.EditValue, tbln2.EditValue)

        Dim periode As String = String.Format("Tahun : {0}", ttahun.EditValue)

        Dim fs As New fpr_akum_penjualan_perbulan2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .tgl = periode, .jenislap = 1}
        fs.ShowDialog()

    End Sub

End Class