Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpr_bayarpiutang

    Private Sub bts_toko_Click(sender As System.Object, e As System.EventArgs) Handles bts_tko.Click
        Dim fs As New fsoutlet With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .kdsales = ""}
        fs.ShowDialog(Me)

        tkd_toko.EditValue = fs.get_KODE
        tnama_toko.EditValue = fs.get_NAMA
        '   talamat_toko.EditValue = fs.get_ALAMAT

        tkd_toko_EditValueChanged(sender, Nothing)


    End Sub

    Private Sub tkd_toko_EditValueChanged(sender As Object, e As System.EventArgs) Handles tkd_toko.Validated
        If tkd_toko.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select kd_toko,nama_toko,alamat_toko from ms_toko where kd_toko='{0}' and aktif=1", tkd_toko.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_toko.EditValue = dread("kd_toko").ToString
                        tnama_toko.EditValue = dread("nama_toko").ToString
                        '    talamat_toko.EditValue = dread("alamat_toko").ToString

                    Else
                        tkd_toko.EditValue = ""
                        tnama_toko.EditValue = ""
                        '   talamat_toko.Text = ""


                    End If
                Else
                    tkd_toko.EditValue = ""
                    tnama_toko.EditValue = ""
                    '    talamat_toko.Text = ""

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

    Private Sub tkd_toko_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_toko.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_toko_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_toko_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_toko.LostFocus
        If tkd_toko.Text.Trim.Length = 0 Then
            tkd_toko.EditValue = ""
            tnama_toko.EditValue = ""
            '    talamat_toko.Text = ""
        End If
    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fpr_bayarpiutang_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tjenis.Focus()
    End Sub

    Private Sub fpr_bayarpiutang_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        tjenis.SelectedIndex = 0

        ttgl.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        Cursor = Cursors.WaitCursor

        Dim sql As String = "SELECT     trbayar.nobukti, trbayar.tanggal, trbayar.tglbayar, trbayar2.nobukti_fak, ms_toko.kd_toko, ms_toko.nama_toko, trbayar2.sisapiutang, trbayar2.jmltunai,trbayar2.jmltrans, " & _
        "trbayar2.jmlgiro, trbayar2.jmlretur, trbayar2.disc_susulan, trbayar2.jmlkelebihan_dr, trbayar2.jumlah,trfaktur_to.tanggal as tanggal_fak " & _
        "FROM         ms_toko INNER JOIN " & _
        "trfaktur_to ON ms_toko.kd_toko = trfaktur_to.kd_toko INNER JOIN " & _
        "trbayar INNER JOIN " & _
        "trbayar2 ON trbayar.nobukti = trbayar2.nobukti ON trfaktur_to.nobukti = trbayar2.nobukti_fak " & _
        "WHERE trbayar.sbatal = 0"

        If tjenis.SelectedIndex = 0 Then
            sql = String.Format("{0} and trbayar.tanggal>='{1}' and trbayar.tanggal<='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))
        Else
            sql = String.Format("{0} and trbayar.tglbayar>='{1}' and trbayar.tglbayar<='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))
        End If

        If tkd_toko.Text.Trim.Length > 0 Then
            sql = String.Format("{0} and ms_toko.kd_toko='{1}'", sql, tkd_toko.Text.Trim)
        End If

        Dim periode As String = String.Format("{0} s.d {1}", convert_date_to_ind(ttgl.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Dim fs As New fpr_bayarpiutang2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .tgl = periode}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub

End Class