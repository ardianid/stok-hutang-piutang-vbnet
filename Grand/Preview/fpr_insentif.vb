Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpr_insentif

    Private Sub fpr_insentif_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub fpr_insentif_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ttgl.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

        ttipe.SelectedIndex = 0

    End Sub

    Private Sub ttipe_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ttipe.SelectedIndexChanged

        tkd_supir.Text = ""
        tnama_supir.Text = ""

    End Sub

    Private Sub bts_supir_Click(sender As System.Object, e As System.EventArgs) Handles bts_supir.Click

        If ttipe.SelectedIndex = 0 Then

            Dim fs As New fssales With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
            fs.ShowDialog(Me)

            tkd_supir.EditValue = fs.get_KODE
            tnama_supir.EditValue = fs.get_NAMA

        ElseIf ttipe.SelectedIndex = 1 Then

            Dim fs As New fssupir With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
            fs.ShowDialog(Me)

            tkd_supir.EditValue = fs.get_KODE
            tnama_supir.EditValue = fs.get_NAMA

        Else

            Dim fs As New fskenek With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
            fs.ShowDialog(Me)

            tkd_supir.EditValue = fs.get_KODE
            tnama_supir.EditValue = fs.get_NAMA

        End If

        

    End Sub

    Private Sub tkd_supir_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_supir.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_supir_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_supir_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_supir.LostFocus
        If tkd_supir.Text.Trim.Length = 0 Then
            tkd_supir.Text = ""
            tnama_supir.Text = ""
        End If
    End Sub

    Private Sub tkd_supir_Validated(sender As Object, e As System.EventArgs) Handles tkd_supir.Validated
        If tkd_supir.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = ""

            If ttipe.SelectedIndex = 0 Then
                sql = String.Format("select * from ms_pegawai where aktif=1 and bagian='SALES' and kd_karyawan='{0}'", tkd_supir.Text.Trim)
            ElseIf ttipe.SelectedIndex = 1 Then
                sql = String.Format("select * from ms_pegawai where aktif=1 and bagian='SUPIR' and kd_karyawan='{0}'", tkd_supir.Text.Trim)
            Else
                sql = String.Format("select * from ms_pegawai where aktif=1 and bagian='KENEK' and kd_karyawan='{0}'", tkd_supir.Text.Trim)
            End If


            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_supir.EditValue = dread("kd_karyawan").ToString
                        tnama_supir.EditValue = dread("nama_karyawan").ToString

                    Else
                        tkd_supir.EditValue = ""
                        tnama_supir.EditValue = ""

                    End If
                Else
                    tkd_supir.EditValue = ""
                    tnama_supir.EditValue = ""

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

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        Cursor = Cursors.WaitCursor

        Dim sql As String = ""

        If ttipe.SelectedIndex = 0 Then
            sql = "SELECT     trfaktur_to.tanggal, ms_pegawai.kd_karyawan+ ' ' +ms_pegawai.nama_karyawan as nama_karyawan"
        Else
            sql = "SELECT     trfaktur_to.tanggal, ms_pegawai.nama_karyawan as nama_karyawan"
        End If

        sql = String.Format("{0}, " & _
            "case when ms_barang.nama_lap ='150ML/50' then " & _
            "(trfaktur_to2.qtykecil - (trfaktur_to2.qtykecil % (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3))) / (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3) " & _
            "else 0 " & _
            "end as '150ML/50', " & _
            "case when ms_barang.nama_lap ='240ML/48' then " & _
            "(trfaktur_to2.qtykecil - (trfaktur_to2.qtykecil % (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3))) / (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3) " & _
            "else 0 " & _
            "end as '240ML/48', " & _
            "case when ms_barang.nama_lap ='500ML/24' then " & _
            "(trfaktur_to2.qtykecil - (trfaktur_to2.qtykecil % (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3))) / (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3) " & _
            "else 0 " & _
            "end as '500ML/24'," & _
            "case when ms_barang.nama_lap ='600ML/24' then " & _
            "(trfaktur_to2.qtykecil - (trfaktur_to2.qtykecil % (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3))) / (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3) " & _
            "else 0 " & _
            "end as '600ML/24', " & _
            "case when ms_barang.nama_lap ='1500ML/12' then " & _
            "(trfaktur_to2.qtykecil - (trfaktur_to2.qtykecil % (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3))) / (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3) " & _
            "else 0 " & _
            "end as '1500ML/12', " & _
            "case when ms_barang.nama_lap ='19LTR' then " & _
            "(trfaktur_to2.qtykecil - (trfaktur_to2.qtykecil % (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3))) / (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3) " & _
            "else 0 " & _
            "end as '19LTR', " & _
            "case when ms_barang.nama_lap ='330ML/24' then " & _
            "(trfaktur_to2.qtykecil - (trfaktur_to2.qtykecil % (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3))) / (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3) " & _
            "else 0 " & _
            "end as '330ML/24',ms_pegawai.ins_gln,ms_pegawai.ins_dus,ms_pegawai.ins_gln_per,ms_pegawai.ins_dus_per,ms_pegawai.hit_gln,ms_pegawai.hit_dus " & _
            "FROM         trfaktur_to INNER JOIN " & _
                  "trfaktur_to2 ON trfaktur_to.nobukti = trfaktur_to2.nobukti INNER JOIN " & _
                  "ms_pegawai ON trfaktur_to.kd_karyawan = ms_pegawai.kd_karyawan INNER JOIN " & _
                  "ms_barang ON trfaktur_to2.kd_barang = ms_barang.kd_barang " & _
                  "where trfaktur_to.sbatal=0 and trfaktur_to.statkirim='TERKIRIM' and ms_barang.jenis='FISIK'", sql)

        sql = String.Format("{0} and trfaktur_to.tanggal>='{1}' and trfaktur_to.tanggal<='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If tkd_supir.Text.Trim.Length > 0 Then
            sql = String.Format(" {0} and trfaktur_to.kd_karyawan='{1}'", sql, tkd_supir.Text.Trim)
        End If

        '  End If

        Dim periode As String = String.Format("{0} s.d {1}", convert_date_to_ind(ttgl.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Dim fs As New fpr_insentif2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .tgl = periode, .tipe = 0, .jenis_lap = ttipe.SelectedIndex}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub


End Class