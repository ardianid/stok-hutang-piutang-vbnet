Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpr_realisasi_krm

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

        Dim sql As String = "SELECT     trfaktur_to.tanggal, trfaktur_to.nobukti, ms_toko.nama_toko, trfaktur_to.netto,'1. FAKTUR' as jenis " & _
            "FROM         trfaktur_to INNER JOIN " & _
            "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko " & _
            "WHERE sbatal=0 and statkirim='TERKIRIM' and spulang=1 and jnis_fak='T'"


        If cbtanggal.SelectedIndex = 1 Then
            sql = String.Format(" {0} and nobukti in (select trfaktur_balik2.nobukti_fak from trfaktur_balik inner join trfaktur_balik2 on trfaktur_balik.nobukti=trfaktur_balik2.nobukti where trfaktur_balik.tanggal>='{1}' and trfaktur_balik.tanggal<='{2}')", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))
        Else
            sql = String.Format(" {0} and trfaktur_to.tanggal>='{1}' and trfaktur_to.tanggal<='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))
        End If

        If cbojnis.SelectedIndex = 0 Then
            sql = String.Format("{0} and jnis_jual2='KREDIT'", sql)
        Else
            sql = String.Format("{0} and not(jnis_jual2='KREDIT')", sql)
        End If

        If tkd_sales.Text.Trim.Length > 0 Then
            sql = String.Format("{0} and kd_karyawan='{1}'", sql, tkd_sales.Text.Trim)
        End If

        'If cbojnis.SelectedIndex <> 0 And tkd_sales.Text.Trim.Length = 0 Then

        '    sql = String.Format(" {0} union all select trretur.tanggal,trretur.nobukti,ms_toko.nama_toko,trretur.netto,'2. RETUR' " & _
        '    "from trretur inner join ms_toko on trretur.kd_toko=ms_toko.kd_toko " & _
        '    "where trretur.sbatal=0 and trretur.nobukti in ( " & _
        '    "select nobukti from ms_logact where userid in ('vivi','septi','siti')) " & _
        '    "and trretur.tanggal>='{1}' and trretur.tanggal<='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))


        'End If

        Dim periode As String = String.Format("{0} s.d {1}", convert_date_to_ind(ttgl.EditValue), convert_date_to_ind(ttgl2.EditValue))
        Dim jdulnya As String
        Dim subjudulnya As String

        If cbojnis.SelectedIndex = 0 Then
            jdulnya = "LAPORAN REALISASI PENJUALAN (KREDIT)"
            subjudulnya = "Total Penjualan (Kredit) : "
        Else
            jdulnya = "LAPORAN REALISASI PENJUALAN (TUNAI)"
            subjudulnya = "Total Penjualan (Tunai) : "
        End If

        Dim fs As New fpr_realisasi_krm2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .tgl = periode, .judul = jdulnya, .subjudul = subjudulnya}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub

    Private Sub fpr_giromasuk1_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub fpr_giromasuk1_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        cbtanggal.SelectedIndex = 1

        ttgl.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

        cbojnis.SelectedIndex = 0

        If Date.Now > DateValue("05/10/2014") Then
            cbtanggal.Enabled = False
        End If

    End Sub

End Class