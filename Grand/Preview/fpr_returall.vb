﻿Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpr_returall

    Private Sub fpr_htoko_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub fpr_htoko_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ttgl.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

        tjenis.SelectedIndex = 0

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

        Dim sql As String = String.Format("select trfaktur_balik.tanggal,trfaktur_to.nobukti,ms_toko.nama_toko, " & _
        "ms_barang.nama_lap,trfaktur_to5.qty,trfaktur_to5.harga,0 as disc_rp,trfaktur_to5.jumlah,1 as 'sdah_pot' " & _
        "from trfaktur_to5 inner join trfaktur_to on trfaktur_to5.nobukti=trfaktur_to.nobukti " & _
        "inner join ms_toko on trfaktur_to.kd_toko=ms_toko.kd_toko " & _
        "inner join ms_barang on trfaktur_to5.kd_barang=ms_barang.kd_barang " & _
        "inner join trfaktur_balik2 on trfaktur_balik2.nobukti_fak=trfaktur_to.nobukti " & _
        "inner join trfaktur_balik on trfaktur_balik.nobukti=trfaktur_balik2.nobukti " & _
        "where trfaktur_to.sbatal=0 and trfaktur_balik.sbatal=0 and " & _
        "trfaktur_balik.tanggal>='{0}' and trfaktur_balik.tanggal<='{1}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If tkode_tko.Text.Trim.Length > 0 Then
            sql = String.Format("{0} and ms_toko.kd_toko='{1}'", sql, tkode_tko.Text.Trim)
        End If

        Dim sql2 As String = String.Format("select trretur.tgl_masuk,trretur.nobukti, ms_toko.nama_toko, " & _
        "ms_barang.nama_lap, trretur2.qty, trretur2.harga, trretur2.disc_rp, trretur2.jumlah, trretur.spotong " & _
        "from trretur inner join trretur2 on trretur.nobukti=trretur2.nobukti " & _
        "inner join ms_toko on trretur.kd_toko=ms_toko.kd_toko " & _
        "inner join ms_barang on trretur2.kd_barang=ms_barang.kd_barang " & _
        "where trretur.sbatal=0 and trretur.tanggal>='{0}' and trretur.tanggal<='{1}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If tkode_tko.Text.Trim.Length > 0 Then
            sql2 = String.Format("{0} and ms_toko.kd_toko='{1}'", sql2, tkode_tko.Text.Trim)
        End If

        Dim sqljadi As String = ""

        If tjenis.EditValue = "All" Then
            sqljadi = String.Format("{0} union all {1}", sql, sql2)
        ElseIf tjenis.EditValue = "Potong Langsung" Then
            sqljadi = sql
        Else
            sqljadi = sql2
        End If

        Dim periode As String = String.Format("Periode : {0} s.d {1}", convert_date_to_ind(ttgl.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Dim fs As New fpr_returall2 With {.WindowState = FormWindowState.Maximized, .sql = sqljadi, .tgl = periode}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub


End Class