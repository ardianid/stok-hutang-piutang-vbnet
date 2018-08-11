Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpr_inout_aslbrg

    Private Sub fpr_inout_aslbrg_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub fpr_inout_aslbrg_Load(sender As Object, e As EventArgs) Handles Me.Load

        ttgl.EditValue = DateValue(Date.Now)
        ttgl2.EditValue = DateValue(Date.Now)

        cbtgl.SelectedIndex = 0

        tshift.SelectedIndex = 0
        tasal.SelectedIndex = 0
        tjenislap.SelectedIndex = 0

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        Cursor = Cursors.WaitCursor

        Dim sql As String = "SELECT tradm_gud.tglmuat, tradm_gud.tglberangkat, tradm_gud.asal_barang, ms_barang.kd_barang, " & _
        "ms_barang.nama_lap as nama_barang, " & _
        "tradm_gud2.qtyin, tradm_gud2.qtyout, tradm_gud.shit,  " & _
        "case when tradm_gud.nobukti_trans='-' then  tradm_gud.nobukti_gd else tradm_gud.nobukti_trans end as no_nota,ms_pegawai.nama_karyawan " & _
        "FROM tradm_gud INNER JOIN " & _
        "tradm_gud2 ON tradm_gud.nobukti = tradm_gud2.nobukti INNER JOIN " & _
        "ms_barang ON tradm_gud2.kd_barang = ms_barang.kd_barang " & _
        "inner join ms_pegawai on tradm_gud.kd_supir=ms_pegawai.kd_karyawan WHERE tradm_gud.sbatal=0"

        If cbtgl.SelectedIndex = 0 Then
            sql = String.Format("{0} and tradm_gud.tglmuat>='{1}' and tradm_gud.tglmuat<='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))
        Else
            sql = String.Format("{0} and tradm_gud.tglberangkat>='{1}' and tradm_gud.tglberangkat<='{2}'", sql, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))
        End If

        If tshift.EditValue.ToString.Length > 0 Then
            sql = String.Format("{0} and tradm_gud.shit='{1}'", sql, tshift.EditValue)
        End If

        If tasal.EditValue.ToString.Length > 0 Then
            sql = String.Format("{0} and tradm_gud.asal_barang='{1}'", sql, tasal.EditValue)
        End If

        Dim jnislap As Integer
        If tjenislap.SelectedIndex = 0 Then
            jnislap = 1
        Else
            jnislap = 2
        End If

        Dim periode As String = String.Format("Periode : {0} s.d {1}", convert_date_to_ind(ttgl.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Dim fs As New fpr_inout_aslbrg2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .tgl = periode, .jenislap = jnislap}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub


End Class