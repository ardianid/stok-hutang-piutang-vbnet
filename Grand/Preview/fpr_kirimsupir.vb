Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpr_kirimsupir

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
            tnama_supir.EditValue = fs.get_NAMA

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

            sql = String.Format("select * from ms_pegawai where aktif=1 and bagian like 'SUPIR%' and kd_karyawan='{0}'", tkd_supir.Text.Trim)


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
            sql = "select tanggal,kd_karyawan+ ' - ' + nama_karyawan as nama_karyawan"
        Else
            sql = "select tanggal,nama_karyawan as nama_karyawan"
        End If

        sql = String.Format(" {0},ins_dus,ins_gln,ins_dus_per,ins_gln_per,hit_dus,hit_gln, " & _
        "case when nama_lap ='150ML/50' then " & _
            "(qtykecil - (qtykecil % (qty1 * qty2 * qty3))) / (qty1 * qty2 * qty3) else 0 end as '150ML/50', " & _
        "case when nama_lap ='240ML/48' then " & _
            "(qtykecil - (qtykecil % (qty1 * qty2 * qty3))) / (qty1 * qty2 * qty3) else 0 end as '240ML/48', " & _
        "case when nama_lap ='500ML/24' then " & _
            "(qtykecil - (qtykecil % (qty1 * qty2 * qty3))) / (qty1 * qty2 * qty3) else 0 end as '500ML/24', " & _
        "case when nama_lap ='600ML/24' then " & _
            "(qtykecil - (qtykecil % (qty1 * qty2 * qty3))) / (qty1 * qty2 * qty3) else 0 end as '600ML/24', " & _
        "case when nama_lap ='1500ML/12' then " & _
            "(qtykecil - (qtykecil % (qty1 * qty2 * qty3))) / (qty1 * qty2 * qty3) else 0 end as '1500ML/12', " & _
        "case when nama_lap ='19LTR' then " & _
            "(qtykecil - (qtykecil % (qty1 * qty2 * qty3))) / (qty1 * qty2 * qty3) else 0 end as '19LTR', " & _
        "case when nama_lap ='330ML/24' then " & _
            "(qtykecil - (qtykecil % (qty1 * qty2 * qty3))) / (qty1 * qty2 * qty3) else 0 end as '330ML/24' " & _
        "from v_kirim_supir", sql)

        sql = String.Format("{0} where tanggal>='{1}' and tanggal<='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If tkd_supir.Text.Trim.Length > 0 Then
            sql = String.Format(" {0} and kd_karyawan='{1}'", sql, tkd_supir.Text.Trim)
        End If

        sql = String.Format(" {0} and kd_karyawan in (select kd_karyawan from ms_pegawai where bagian='SUPIR')", sql)

        Dim periode As String = String.Format("{0} s.d {1}", convert_date_to_ind(ttgl.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Dim fs As New fpr_insentif2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .tgl = periode, .tipe = 1, .jenis_lap = ttipe.SelectedIndex}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub

End Class