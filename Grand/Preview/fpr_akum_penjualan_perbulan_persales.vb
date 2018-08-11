Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy
Public Class fpr_akum_penjualan_perbulan_persales

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

        'If tkode_tko.Text.Trim.Length = 0 Then
        '    MsgBox("Toko/Outlet harus diisi..", vbOKOnly + vbInformation, "Informasi")
        '    tkode_tko.Focus()
        '    Return
        'End If

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
        "sum(trfaktur_to2.qtykecil) / (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3) as jml, " & _
        "ms_pegawai.kd_karyawan,ms_pegawai.nama_karyawan " & _
        "from trfaktur_to inner join trfaktur_to2 on trfaktur_to.nobukti=trfaktur_to2.nobukti " & _
        "inner join ms_barang on trfaktur_to2.kd_barang=ms_barang.kd_barang " & _
        "inner join ms_pegawai on trfaktur_to.kd_karyawan=ms_pegawai.kd_karyawan " & _
        "where ms_barang.jenis='FISIK' and trfaktur_to.sbatal=0 and trfaktur_to.statkirim='TERKIRIM' " & _
        "and YEAR(trfaktur_to.tanggal)={0} and month(trfaktur_to.tanggal)>={1} and month(trfaktur_to.tanggal)<={2}", ttahun.EditValue, tbln1.EditValue, tbln2.EditValue)

        If tkd_sales.Text.Trim.Length > 0 Then
            sql = String.Format("{0} and ms_pegawai.kd_karyawan='{1}'", sql, tkd_sales.Text.Trim)
        End If


        sql = String.Format("{0} group by trfaktur_to.tanggal,ms_barang.kd_barang,ms_barang.nama_lap, " & _
        "ms_barang.qty1,ms_barang.qty2,ms_barang.qty3,ms_pegawai.kd_karyawan,ms_pegawai.nama_karyawan", sql)

        Dim periode As String = ttahun.EditValue
        '  Dim namatoko As String = String.Format("{0} - {1}", tkode_tko.Text.Trim, tnama_tko.Text.Trim)

        Dim fs As New fpr_akum_penjualan_perbulan2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .tgl = periode, .jenislap = 3, .namatoko = ""}
        fs.ShowDialog()

    End Sub


End Class