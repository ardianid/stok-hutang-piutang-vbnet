Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpr_kirimsupir_bli

    Private Sub fpr_insentif_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub fpr_insentif_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ttgl.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

        ttipe.SelectedIndex = 0

    End Sub

    Private Sub bts_supir_Click(sender As System.Object, e As System.EventArgs) Handles bts_supir.Click

        Dim fs As New fssupir With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_supir.EditValue = fs.get_KODE

        tkd_supir_Validated(Nothing, Nothing)

        '   tnama_supir.EditValue = fs.get_NAMA

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

            sql = String.Format("select * from ms_pegawai where aktif=1 and bagian='SUPIR' and kd_karyawan='{0}'", tkd_supir.Text.Trim)


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
            sql = "SELECT ms_pegawai.kd_karyawan + '-' + ms_pegawai.nama_karyawan as nama_karyawan,trbeli.tanggal,trbeli.nosj,trbeli.nopol,"
            'Else
            '    sql = "SELECT ms_pegawai.nama_karyawan,trbeli.tanggal,trbeli.nosj,trbeli.nopol,"
            'End If

            sql = String.Format("{0} " & _
            "case when ms_barang.nama_lap ='150ML/50' then " & _
                "trbeli2.qty else 0 end as '150ML/50', " & _
            "case when ms_barang.nama_lap ='240ML/48' then " & _
                "trbeli2.qty else 0 end as '240ML/48', " & _
            "case when ms_barang.nama_lap ='500ML/24' then " & _
                "trbeli2.qty else 0 end as '500ML/24', " & _
            "case when ms_barang.nama_lap ='600ML/24' then " & _
                "trbeli2.qty else 0 end as '600ML/24', " & _
            "case when ms_barang.nama_lap ='1500ML/12' then " & _
                "trbeli2.qty else 0 end as '1500ML/12', " & _
            "case when ms_barang.nama_lap ='19LTR' then " & _
                "trbeli2.qty else 0 end as '19LTR', " & _
            "case when ms_barang.nama_lap ='330ML/24' then " & _
                "trbeli2.qty else 0 end as '330ML/24' " & _
            "FROM         trbeli INNER JOIN " & _
            "trbeli2 ON trbeli.nobukti = trbeli2.nobukti INNER JOIN " & _
            "ms_barang ON trbeli2.kd_barang = ms_barang.kd_barang INNER JOIN " & _
            "ms_pegawai ON trbeli.kd_karyawan = ms_pegawai.kd_karyawan " & _
            "WHERE trbeli.sbatal=0 and ms_pegawai.bagian='SUPIR' " & _
            "and trbeli.tanggal>='{1}' and trbeli.tanggal<='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        Else

            sql = String.Format("SELECT     dbo.ms_pegawai.nama_karyawan, " & _
                "case when COUNT(distinct nosj) <={0} then " & _
                "COUNT(distinct nosj) else 0 end as rit_a, " & _
                "case when COUNT(distinct nosj) >{1} then " & _
                "COUNT(distinct nosj) else 0 end as rit_b, " & _
                "case when COUNT(distinct nosj) <={0} then " & _
                "sum(trbeli2.qty) else 0 end as jml_a, " & _
                "case when COUNT(distinct nosj) >{1} then " & _
                "sum(trbeli2.qty) else 0 end as jml_b, " & _
                "{2} as per_a,{3} as per_b " & _
                "FROM         dbo.trbeli INNER JOIN " & _
                      "dbo.trbeli2 ON dbo.trbeli.nobukti = dbo.trbeli2.nobukti INNER JOIN " & _
                      "dbo.ms_barang ON dbo.trbeli2.kd_barang = dbo.ms_barang.kd_barang INNER JOIN " & _
                      "dbo.ms_pegawai ON dbo.trbeli.kd_karyawan = dbo.ms_pegawai.kd_karyawan " & _
                "WHERE     (dbo.trbeli.sbatal = 0) AND (dbo.ms_pegawai.bagian = 'SUPIR') " & _
                "and trbeli.tanggal>='{4}' and trbeli.tanggal<='{5}' " & _
                "GROUP BY dbo.ms_pegawai.nama_karyawan", Replace(tmin_a.EditValue, ",", "."), Replace(tmin_b.EditValue, ",", "."), Replace(tnilai_a.EditValue, ",", "."), Replace(tnilai_b.EditValue, ",", "."), convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        End If

        If tkd_supir.Text.Trim.Length > 0 Then
            sql = String.Format(" {0} and ms_pegawai.kd_karyawan='{1}'", sql, tkd_supir.Text.Trim)
        End If

        Dim periode As String = String.Format("{0} s.d {1}", convert_date_to_ind(ttgl.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Dim fs As New fpr_insentif2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .tgl = periode, .tipe = 3, .jenis_lap = ttipe.SelectedIndex}

        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub

    Private Sub ttipe_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ttipe.SelectedIndexChanged

        If ttipe.SelectedIndex = 0 Then
            tmin_a.Enabled = False
            tmin_b.Enabled = False
            tnilai_a.Enabled = False
            tnilai_b.Enabled = False

            tmin_a.EditValue = 0
            tmin_b.EditValue = 0

            tnilai_a.EditValue = 0
            tnilai_b.EditValue = 0


        Else

            tmin_a.Enabled = True
            tmin_b.Enabled = True
            tnilai_a.Enabled = True
            tnilai_b.Enabled = True

            tmin_a.EditValue = 25
            tmin_b.EditValue = 25

            tnilai_a.EditValue = 10
            tnilai_b.EditValue = 20

        End If

    End Sub

    Private Sub tmin_a_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tmin_a.EditValueChanged
        If IsNumeric(tmin_a.EditValue) Then
            tmin_b.EditValue = tmin_a.EditValue
        End If
    End Sub

End Class