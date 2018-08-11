Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpr_penjproduk_ken

    Private Sub bts_sal_Click(sender As System.Object, e As System.EventArgs) Handles bts_sal.Click
        Dim fs As New fskenek With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_sales.EditValue = fs.get_KODE
        tnama_sales.EditValue = fs.get_NAMA
    End Sub

    Private Sub tkd_sales_EditValueChanged(sender As Object, e As System.EventArgs) Handles tkd_sales.EditValueChanged
        If tkd_sales.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select * from ms_pegawai where aktif=1 and bagian='KENEK' and kd_karyawan='{0}'", tkd_sales.Text.Trim)

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

        Dim sql As String = String.Format("SELECT     trfaktur_to.nobukti, CASE WHEN LEN(trfaktur_to.no_nota)=0 or trfaktur_to.no_nota is NULL THEN trfaktur_to.nobukti ELSE trfaktur_to.no_nota END AS no_nota, trfaktur_to.tanggal, trfaktur_to.kd_toko, " & _
        "ms_toko.nama_toko, ms_toko.alamat_toko, v_kirim_kenek0.kd_karyawan, v_kirim_kenek0.nama_karyawan, trfaktur_to2.kd_barang, ms_barang.nama_barang, " & _
        "CASE WHEN trfaktur_to2.qty = 0 AND (NOT (trfaktur_to5.qty = NULL) OR  " & _
        "trfaktur_to5.qty > 0) THEN - trfaktur_to5.qty ELSE trfaktur_to2.qty END AS qty, ms_barang.nourut_lap " & _
        "FROM  v_kirim_kenek0 INNER JOIN ms_barang INNER JOIN ms_toko INNER JOIN " & _
        "trfaktur_to ON ms_toko.kd_toko = trfaktur_to.kd_toko INNER JOIN " & _
        "trfaktur_to2 ON trfaktur_to.nobukti = trfaktur_to2.nobukti ON ms_barang.kd_barang = trfaktur_to2.kd_barang ON  " & _
        "v_kirim_kenek0.nobukti = trfaktur_to.nobukti LEFT OUTER JOIN " & _
        "trfaktur_to5 ON trfaktur_to2.kd_barang = trfaktur_to5.kd_barang AND trfaktur_to2.nobukti = trfaktur_to5.nobukti " & _
        "WHERE ms_barang.jenis = 'FISIK' AND trfaktur_to.sbatal = 0 AND trfaktur_to.statkirim = 'TERKIRIM' and trfaktur_to.tanggal >='{0}' and trfaktur_to.tanggal <='{1}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If tkd_sales.Text.Trim.Length > 0 Then
            sql = String.Format("{0} and v_kirim_kenek0.kd_karyawan='{1}'", sql, tkd_sales.Text.Trim)
        End If

        sql = String.Format("{0} union all ", sql)

        sql = String.Format("{0} SELECT trfaktur_to.nobukti, CASE WHEN LEN(trfaktur_to.no_nota)=0 or trfaktur_to.no_nota is NULL THEN trfaktur_to.nobukti ELSE trfaktur_to.no_nota END AS no_nota, trfaktur_to.tanggal, trfaktur_to.kd_toko,  " & _
        "ms_toko.nama_toko, ms_toko.alamat_toko, v_kirim_kenek0.kd_karyawan, v_kirim_kenek0.nama_karyawan, trfaktur_to5.kd_barang, ms_barang.nama_barang, " & _
        "trfaktur_to5.qty, ms_barang.nourut_lap " & _
        "FROM         trfaktur_to5 INNER JOIN " & _
        "ms_barang ON trfaktur_to5.kd_barang = ms_barang.kd_barang INNER JOIN " & _
        "trfaktur_to ON trfaktur_to.nobukti = trfaktur_to5.nobukti INNER JOIN " & _
        "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko INNER JOIN " & _
        "v_kirim_kenek0 ON trfaktur_to.nobukti = v_kirim_kenek0.nobukti " & _
        "WHERE     ms_barang.jenis = 'FISIK' AND trfaktur_to.sbatal = 0 AND trfaktur_to.statkirim = 'TERKIRIM' " & _
        "and  trfaktur_to.tanggal>='{1}' and trfaktur_to.tanggal<='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If tkd_sales.Text.Trim.Length > 0 Then
            sql = String.Format("{0} and v_kirim_kenek0.kd_karyawan='{1}'", sql, tkd_sales.Text.Trim)
        End If

        sql = String.Format(" {0} and trfaktur_to5.kd_barang not in (select kd_barang from trfaktur_to2 inner join trfaktur_to " & _
        "on trfaktur_to.nobukti=trfaktur_to2.nobukti " & _
        "inner join v_kirim_kenek0 on trfaktur_to.nobukti=v_kirim_kenek0.nobukti " & _
        "where  " & _
        "trfaktur_to.sbatal = 0 AND trfaktur_to.statkirim = 'TERKIRIM'  and trfaktur_to.tanggal>='{1}' and trfaktur_to.tanggal<='{2}')", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If tkd_sales.Text.Trim.Length > 0 Then
            sql = String.Format("{0} and v_kirim_kenek0.kd_karyawan='{1}')", sql.Substring(0, sql.Length - 1), tkd_sales.Text.Trim)
        End If

        sql = String.Format(" {0} and trfaktur_to5.kd_barang not in (select kd_barang from trfaktur_to3 inner join trfaktur_to " & _
        "on trfaktur_to.nobukti=trfaktur_to3.nobukti " & _
        "inner join v_kirim_kenek0 on trfaktur_to.nobukti=v_kirim_kenek0.nobukti " & _
        " where  " & _
        "trfaktur_to.sbatal = 0 AND trfaktur_to.statkirim = 'TERKIRIM' and trfaktur_to.tanggal>='{1}' and trfaktur_to.tanggal<='{2}')", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If tkd_sales.Text.Trim.Length > 0 Then
            sql = String.Format("{0} and v_kirim_kenek0.kd_karyawan='{1}')", sql.Substring(0, sql.Length - 1), tkd_sales.Text.Trim)
        End If

        sql = String.Format("{0} union all ", sql)

        sql = String.Format("{0} SELECT   trfaktur_to.nobukti, CASE WHEN LEN(trfaktur_to.no_nota)=0 or trfaktur_to.no_nota is NULL THEN trfaktur_to.nobukti ELSE trfaktur_to.no_nota END AS no_nota, trfaktur_to.tanggal, ms_toko.kd_toko, " & _
        "ms_toko.nama_toko, ms_toko.alamat_toko, v_kirim_kenek0.kd_karyawan, v_kirim_kenek0.nama_karyawan, ms_barang.kd_barang, ms_barang.nama_barang, " & _
        "CASE WHEN v_barang_jaminan_kembali2.qty = 0 AND  " & _
        "v_barang_jaminan_kembali2.jaminan > 0 THEN - v_barang_jaminan_kembali2.jaminan + CASE WHEN trfaktur_to5.qty IS NULL " & _
        "THEN 0 ELSE - trfaktur_to5.qty END ELSE v_barang_jaminan_kembali2.kembali + CASE WHEN trfaktur_to5.qty IS NULL THEN 0 ELSE trfaktur_to5.qty END END AS qty,100 AS Expr1 " & _
        "FROM         v_barang_jaminan_kembali2 INNER JOIN " & _
                      "trfaktur_to ON v_barang_jaminan_kembali2.nobukti = trfaktur_to.nobukti INNER JOIN " & _
                      "ms_barang ON v_barang_jaminan_kembali2.kd_barang_kmb = ms_barang.kd_barang INNER JOIN " & _
                      "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko INNER JOIN " & _
                      "v_kirim_kenek0 ON trfaktur_to.nobukti = v_kirim_kenek0.nobukti LEFT OUTER JOIN " & _
                      "trfaktur_to5 ON v_barang_jaminan_kembali2.kd_barang_kmb = trfaktur_to5.kd_barang AND v_barang_jaminan_kembali2.nobukti = trfaktur_to5.nobukti " & _
        "WHERE   trfaktur_to.sbatal = 0 AND trfaktur_to.statkirim = 'TERKIRIM'  and trfaktur_to.tanggal >='{1}' and trfaktur_to.tanggal <='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If tkd_sales.Text.Trim.Length > 0 Then
            sql = String.Format("{0} and v_kirim_kenek0.kd_karyawan='{1}'", sql, tkd_sales.Text.Trim)
        End If


        Dim periode As String = String.Format("{0} s.d {1}", convert_date_to_ind(ttgl.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Dim fs As New fpr_penjproduk2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .tgl = periode, .jdlkary = 3}
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