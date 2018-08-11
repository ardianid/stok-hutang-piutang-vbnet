Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpr_fakturnonkirim


    Private Sub fpr_insentif_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub fpr_insentif_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ttgl.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

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

        sql = String.Format("SELECT     trrekap_to.nobukti, trrekap_to.tglkirim, trrekap_to.kd_supir, ms_pegawai.nama_karyawan AS nama_supir, trrekap_to.nopol, trrekap_to2.nobukti_fak, ms_toko.kd_toko, " & _
            "ms_toko.nama_toko, trfaktur_to.netto " & _
            "FROM         ms_toko INNER JOIN " & _
                      "trfaktur_to ON ms_toko.kd_toko = trfaktur_to.kd_toko INNER JOIN " & _
                      "trrekap_to INNER JOIN " & _
                      "trrekap_to2 ON trrekap_to.nobukti = trrekap_to2.nobukti ON trfaktur_to.nobukti = trrekap_to2.nobukti_fak INNER JOIN " & _
                      "ms_pegawai ON trrekap_to.kd_supir = ms_pegawai.kd_karyawan " & _
            "WHERE trrekap_to.sbatal=0 and not(trrekap_to2.statkirim='TERKIRIM') and trrekap_to.tglkirim>='{0}' and trrekap_to.tglkirim<='{1}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If tkd_supir.Text.Trim.Length > 0 Then
            sql = String.Format(" {0} and ms_pegawai.kd_karyawan='{1}'", sql, tkd_supir.Text.Trim)
        End If

        Dim periode As String = String.Format("{0} s.d {1}", convert_date_to_ind(ttgl.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Dim fs As New fpr_fakturbtal2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .tgl = periode,.jenis=1}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub


End Class