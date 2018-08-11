Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpr_penjproduk

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

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        Cursor = Cursors.WaitCursor

        Dim sql As String = String.Format("SELECT trfaktur_to.nobukti, CASE WHEN LEN(trfaktur_to.no_nota)=0 or trfaktur_to.no_nota is NULL THEN trfaktur_to.nobukti ELSE trfaktur_to.no_nota END AS no_nota, trfaktur_to.tanggal, trfaktur_to.kd_toko, " & _
        "ms_toko.nama_toko, ms_toko.alamat_toko, ms_pegawai.kd_karyawan, ms_pegawai.nama_karyawan, trfaktur_to2.kd_barang, ms_barang.nama_barang, " & _
        "case when trfaktur_to2.qty = 0 and (not(trfaktur_to5.qty=NULL) or trfaktur_to5.qty >0)  then " & _
        "- trfaktur_to5.qty " & _
        "else " & _
        "trfaktur_to2.qty - case when trfaktur_to5.qty IS NULL then 0 else trfaktur_to5.qty end end as qty " & _
        ", ms_barang.nourut_lap " & _
        "FROM ms_barang INNER JOIN trfaktur_to2 INNER JOIN " & _
        "ms_pegawai INNER JOIN trfaktur_to ON ms_pegawai.kd_karyawan = trfaktur_to.kd_karyawan INNER JOIN " & _
        "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko ON trfaktur_to2.nobukti = trfaktur_to.nobukti ON ms_barang.kd_barang = trfaktur_to2.kd_barang LEFT OUTER JOIN " & _
        "trfaktur_to5 ON trfaktur_to2.kd_barang = trfaktur_to5.kd_barang AND trfaktur_to2.nobukti = trfaktur_to5.nobukti " & _
        "WHERE     ms_barang.jenis = 'FISIK' AND trfaktur_to.sbatal = 0 AND trfaktur_to.statkirim = 'TERKIRIM' and trfaktur_to.tanggal >='{0}' and trfaktur_to.tanggal <='{1}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If tkd_sales.Text.Trim.Length > 0 Then
            sql = String.Format("{0} and trfaktur_to.kd_karyawan='{1}'", sql, tkd_sales.Text.Trim)
        End If

        sql = String.Format("{0} union all ", sql)

        sql = String.Format("{0} SELECT trfaktur_to.nobukti, CASE WHEN LEN(trfaktur_to.no_nota)=0 or trfaktur_to.no_nota is NULL THEN trfaktur_to.nobukti ELSE trfaktur_to.no_nota END AS no_nota, trfaktur_to.tanggal, trfaktur_to.kd_toko, " & _
        "ms_toko.nama_toko, ms_toko.alamat_toko, ms_pegawai.kd_karyawan, ms_pegawai.nama_karyawan, trfaktur_to5.kd_barang, ms_barang.nama_barang, " & _
        "trfaktur_to5.qty, ms_barang.nourut_lap " & _
        "FROM trfaktur_to5 inner join ms_barang on trfaktur_to5.kd_barang=ms_barang.kd_barang " & _
        "inner join trfaktur_to on trfaktur_to.nobukti=trfaktur_to5.nobukti " & _
        "inner join ms_toko on trfaktur_to.kd_toko=ms_toko.kd_toko " & _
        "inner join ms_pegawai on trfaktur_to.kd_karyawan=ms_pegawai.kd_karyawan " & _
        "WHERE ms_barang.jenis = 'FISIK' AND trfaktur_to.sbatal = 0 AND trfaktur_to.statkirim = 'TERKIRIM' " & _
        "and  trfaktur_to.tanggal>='{1}' and trfaktur_to.tanggal<='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If tkd_sales.Text.Trim.Length > 0 Then
            sql = String.Format("{0} and trfaktur_to.kd_karyawan='{1}'", sql, tkd_sales.Text.Trim)
        End If

        sql = String.Format(" {0} and trfaktur_to5.kd_barang not in (select kd_barang from trfaktur_to2 inner join trfaktur_to " & _
        "on trfaktur_to.nobukti=trfaktur_to2.nobukti where  " & _
        "trfaktur_to.sbatal = 0 AND trfaktur_to.statkirim = 'TERKIRIM' and trfaktur_to.tanggal>='{1}' and trfaktur_to.tanggal<='{2}')", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If tkd_sales.Text.Trim.Length > 0 Then
            sql = String.Format("{0} and trfaktur_to.kd_karyawan='{1}')", sql.Substring(0, sql.Length - 1), tkd_sales.Text.Trim)
        End If

        sql = String.Format(" {0} and trfaktur_to5.kd_barang not in (select kd_barang from trfaktur_to3 inner join trfaktur_to " & _
        "on trfaktur_to.nobukti=trfaktur_to3.nobukti where  " & _
        "trfaktur_to.sbatal = 0 AND trfaktur_to.statkirim = 'TERKIRIM' and trfaktur_to.tanggal>='{1}' and trfaktur_to.tanggal<='{2}')", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If tkd_sales.Text.Trim.Length > 0 Then
            sql = String.Format("{0} and trfaktur_to.kd_karyawan='{1}')", sql.Substring(0, sql.Length - 1), tkd_sales.Text.Trim)
        End If

        sql = String.Format("{0} union all ", sql)

        sql = String.Format("{0} SELECT     trfaktur_to.nobukti, case when LEN(trfaktur_to.no_nota)=0 or trfaktur_to.no_nota is NULL then trfaktur_to.nobukti else  trfaktur_to.no_nota end no_nota, trfaktur_to.tanggal, ms_toko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, ms_pegawai.kd_karyawan, " & _
                      "ms_pegawai.nama_karyawan, ms_barang.kd_barang, ms_barang.nama_barang, " & _
           "case when v_barang_jaminan_kembali2.qty=0 and v_barang_jaminan_kembali2.jaminan > 0 then " & _
   "-v_barang_jaminan_kembali2.jaminan + case when trfaktur_to5.qty IS NULL then 0 else -trfaktur_to5.qty end " & _
   "else " & _
        "v_barang_jaminan_kembali2.kembali + case when trfaktur_to5.qty IS NULL then 0 else trfaktur_to5.qty end " & _
   "end as qty,100 " & _
        "FROM         v_barang_jaminan_kembali2 INNER JOIN " & _
                      "trfaktur_to ON v_barang_jaminan_kembali2.nobukti = trfaktur_to.nobukti INNER JOIN " & _
                      "ms_barang ON v_barang_jaminan_kembali2.kd_barang_kmb = ms_barang.kd_barang INNER JOIN " & _
                      "ms_pegawai ON trfaktur_to.kd_karyawan = ms_pegawai.kd_karyawan INNER JOIN " & _
                      "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko LEFT OUTER JOIN " & _
                      "trfaktur_to5 ON v_barang_jaminan_kembali2.kd_barang_kmb = trfaktur_to5.kd_barang AND v_barang_jaminan_kembali2.nobukti = trfaktur_to5.nobukti " & _
                "WHERE     trfaktur_to.sbatal = 0 AND trfaktur_to.statkirim = 'TERKIRIM'  and trfaktur_to.tanggal >='{1}' and trfaktur_to.tanggal <='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If tkd_sales.Text.Trim.Length > 0 Then
            sql = String.Format("{0} and trfaktur_to.kd_karyawan='{1}'", sql, tkd_sales.Text.Trim)
        End If


        Dim periode As String = String.Format("{0} s.d {1}", convert_date_to_ind(ttgl.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Dim fs As New fpr_penjproduk2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .tgl = periode, .jdlkary = 1}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub

    Private Sub fpr_giromasuk1_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub fpr_giromasuk1_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ttgl.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

    End Sub


End Class