﻿Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpr_terimapiutang

    Private Sub fpr_htoko_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub fpr_htoko_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ttgl.EditValue = Date.Now
        ttgl2.EditValue = Date.Now
    End Sub

    Private Sub tkode_tko_Validated(sender As Object, e As System.EventArgs) Handles tkode_tko.Validated

        If tkode_tko.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select kd_toko,nama_toko,alamat_toko from ms_toko where kd_toko='{0}' and aktif=1", tkode_tko.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkode_tko.EditValue = dread("kd_toko").ToString
                        tnama_tko.EditValue = dread("nama_toko").ToString
                        talamat_tko.EditValue = dread("alamat_toko").ToString

                    Else
                        tkode_tko.EditValue = ""
                        tnama_tko.EditValue = ""
                        talamat_tko.Text = ""


                    End If
                Else
                    tkode_tko.EditValue = ""
                    tnama_tko.EditValue = ""
                    talamat_tko.Text = ""

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

    Private Sub bts_supir_Click(sender As System.Object, e As System.EventArgs) Handles bts_supir.Click

        Dim fs As New fsoutlet With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .kdsales = ""}
        fs.ShowDialog(Me)

        tkode_tko.EditValue = fs.get_KODE
        tnama_tko.EditValue = fs.get_NAMA
        talamat_tko.EditValue = fs.get_ALAMAT

        tkode_tko_Validated(sender, Nothing)

    End Sub

    Private Sub tkd_toko_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkode_tko.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_supir_Click(sender, Nothing)
        End If
    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub tkode_tko_LostFocus(sender As Object, e As System.EventArgs) Handles tkode_tko.LostFocus
        If tkode_tko.Text.Trim.Length = 0 Then
            tnama_tko.Text = ""
            talamat_tko.Text = ""
        End If
    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        Cursor = Cursors.WaitCursor

        Dim sql As String = String.Format("SELECT    trbayar.nobukti,trfaktur_to.tanggal, " & _
        "case when len(trfaktur_to.no_nota)=0 or trfaktur_to.no_nota is null then trbayar2.nobukti_fak else trfaktur_to.no_nota end as nobukti_fak , " & _
        "ms_toko.nama_toko, trbayar2.sisapiutang, trbayar2.jmltunai + trbayar2.jmlgiro + trbayar2.disc_susulan AS jml,trbayar2.jmltunai + trbayar2.jmlgiro + trbayar2.disc_susulan AS jml2, " & _
        "trbayar2.disc_susulan,'01. PEMBAYARAN' as jenisbayar " & _
        "FROM         ms_toko INNER JOIN " & _
        "trfaktur_to ON ms_toko.kd_toko = trfaktur_to.kd_toko INNER JOIN " & _
        "trbayar INNER JOIN " & _
        "trbayar2 ON trbayar.nobukti = trbayar2.nobukti ON trfaktur_to.nobukti = trbayar2.nobukti_fak " & _
        "WHERE     trbayar.sbatal = 0 and (trbayar2.jmltunai + trbayar2.jmlgiro +  trbayar2.disc_susulan + trbayar2.jmlkelebihan_dr) > 0 and trbayar.tanggal>='{0}' and trbayar.tanggal<='{1}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If tkode_tko.Text.Trim.Length > 0 Then
            sql = String.Format("{0} and ms_toko.kd_toko='{1}'", sql, tkode_tko.Text.Trim)
        End If

        Dim sql2 As String = String.Format("SELECT trbayar.nobukti,trfaktur_to.tanggal, " & _
        "case when len(trfaktur_to.no_nota)=0 or trfaktur_to.no_nota is null then trfaktur_to.nobukti else trfaktur_to.no_nota end, " & _
        "ms_toko.nama_toko,trbayar2_kemb.jmlfak,trbayar2_kemb.jmlbayar,-trbayar2_kemb.jmlbayar,0,'02. PENGEMBALIAN UANG' " & _
        "FROM trbayar2_kemb inner join trfaktur_to on trbayar2_kemb.nobukti_fak= trfaktur_to.nobukti " & _
        "inner join ms_toko on trfaktur_to.kd_toko=ms_toko.kd_toko " & _
        "inner join trbayar on trbayar2_kemb.nobukti=trbayar.nobukti " & _
        "WHERE     trbayar.sbatal = 0 and trbayar.tanggal>='{0}' and trbayar.tanggal<='{1}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If tkode_tko.Text.Trim.Length > 0 Then
            sql2 = String.Format("{0} and ms_toko.kd_toko='{1}'", sql2, tkode_tko.Text.Trim)
        End If

        Dim sql_gab As String = String.Format("{0} union all {1}", sql, sql2)

        Dim periode As String = String.Format("{0} s.d {1}", convert_date_to_ind(ttgl.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Dim fs As New fpr_terimapiutang2 With {.WindowState = FormWindowState.Maximized, .sql = sql_gab, .tgl = periode}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub


End Class