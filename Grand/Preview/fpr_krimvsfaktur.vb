Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpr_krimvsfaktur

    Private Sub fpr_insentif_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub fpr_insentif_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ttgl.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

    End Sub

    Private Sub bts_supir_Click(sender As System.Object, e As System.EventArgs) Handles bts_supir.Click

        Dim fs As New fssales With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
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

            sql = String.Format("select * from ms_pegawai where aktif=1 and bagian='SALES' and kd_karyawan='{0}'", tkd_supir.Text.Trim)


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

        sql = String.Format("SELECT     ms_pegawai.nama_karyawan AS sales, trspm.tglberangkat AS tanggal, trturun_br2.qty, trturun_br2.qty_tr,0 as qty_fak " & _
             "FROM         ms_pegawai INNER JOIN " & _
                       "trspm ON ms_pegawai.kd_karyawan = trspm.kd_sales INNER JOIN " & _
                       "trturun_br INNER JOIN " & _
                       "trturun_br2 ON trturun_br.nobukti = trturun_br2.nobukti ON trspm.nobukti = trturun_br.nobukti_spm " & _
             "where trspm.sbatal=0 and trturun_br.sbatal=0 and trspm.tglberangkat>='{0}' and trspm.tglberangkat<='{1}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))


        If tkd_supir.Text.Trim.Length > 0 Then
            sql = String.Format(" {0} and trspm.kd_sales='{1}'", sql, tkd_supir.Text.Trim)
        End If


        sql = String.Format(" {0} UNION ALL ", sql)

        sql = String.Format(" {0} SELECT     ms_pegawai.nama_karyawan AS sales, trfaktur_to.tanggal,0 as qty,0 as qty_tr,sum(trfaktur_to2.qty) as qty_fak " & _
            "FROM     trfaktur_to INNER JOIN " & _
                      "trfaktur_to2 ON trfaktur_to.nobukti = trfaktur_to2.nobukti INNER JOIN  " & _
                      "ms_pegawai ON trfaktur_to.kd_karyawan = ms_pegawai.kd_karyawan INNER JOIN " & _
                      "ms_barang on trfaktur_to2.kd_barang=ms_barang.kd_barang " & _
        "WHERE  trfaktur_to.sbatal=0 and ms_barang.jenis='FISIK' and trfaktur_to.nobukti in (select trfaktur_to4.nobukti from trfaktur_to4 " & _
        "inner join trspm on trfaktur_to4.nobukti_spm=trfaktur_to4.nobukti_spm inner join trturun_br on trspm.nobukti=trturun_br.nobukti_spm " & _
        "where trspm.sbatal=0 and trturun_br.sbatal=0) and trfaktur_to.tanggal>='{1}' and trfaktur_to.tanggal <='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If tkd_supir.Text.Trim.Length > 0 Then
            sql = String.Format(" {0} and trfaktur_to.kd_karyawan='{1}'", sql, tkd_supir.Text.Trim)
        End If

        sql = String.Format(" {0}  group by ms_pegawai.nama_karyawan, trfaktur_to.tanggal", sql)

        Dim periode As String = String.Format("{0} s.d {1}", convert_date_to_ind(ttgl.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Dim fs As New fpr_kirimvsfaktur2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .tgl = periode}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub


End Class