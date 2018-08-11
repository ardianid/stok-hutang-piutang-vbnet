Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy
Public Class fpr_akum_penjualan_perbulan_pertok

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
        "ms_toko.kd_toko,ms_toko.nama_toko " & _
        "from trfaktur_to inner join trfaktur_to2 on trfaktur_to.nobukti=trfaktur_to2.nobukti " & _
        "inner join ms_barang on trfaktur_to2.kd_barang=ms_barang.kd_barang " & _
        "inner join ms_toko on trfaktur_to.kd_toko=ms_toko.kd_toko " & _
        "where ms_barang.jenis='FISIK' and trfaktur_to.sbatal=0 and trfaktur_to.statkirim='TERKIRIM' " & _
        "and YEAR(trfaktur_to.tanggal)={0} and month(trfaktur_to.tanggal)>={1} and month(trfaktur_to.tanggal)<={2}", ttahun.EditValue, tbln1.EditValue, tbln2.EditValue)

        If tkode_tko.Text.Trim.Length > 0 Then
            sql = String.Format("{0} and trfaktur_to.kd_toko='{1}'", sql, tkode_tko.Text.Trim)
        End If


        sql = String.Format("{0} group by trfaktur_to.tanggal,ms_barang.kd_barang,ms_barang.nama_lap, " & _
        "ms_barang.qty1,ms_barang.qty2,ms_barang.qty3,ms_toko.kd_toko,ms_toko.nama_toko", sql)

        Dim periode As String = ttahun.EditValue
        Dim namatoko As String = String.Format("{0} - {1}", tkode_tko.Text.Trim, tnama_tko.Text.Trim)

        Dim fs As New fpr_akum_penjualan_perbulan2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .tgl = periode, .jenislap = 2, .namatoko = namatoko}
        fs.ShowDialog()

    End Sub


End Class