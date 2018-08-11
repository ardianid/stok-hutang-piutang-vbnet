Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpr_selisihtimb

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        Cursor = Cursors.WaitCursor

        Dim sql As String = String.Format("select tradm_gud.nobukti,tradm_gud.tanggal as tglmuat,tradm_gud.nopol,tradm_gud.jmltimb, " & _
            "sum(v_selisihtimb.[19LTR]) as [19LTR], " & _
            "sum(v_selisihtimb.[150ML/50]) as [150ML/50], " & _
            "sum(v_selisihtimb.[240ML/48]) as [240ML/48], " & _
            "sum(v_selisihtimb.[330ML/248]) as [330ML/248], " & _
            "sum(v_selisihtimb.[500ML/24]) as [500ML/24], " & _
            "SUM(v_selisihtimb.[600ML/24]) as [600ML/24], " & _
            "SUM(v_selisihtimb.[1500ML/12]) as [1500ML/12], " & _
            "SUM(v_selisihtimb.[alm]) as [alm], " & _
            "SUM(v_selisihtimb.[plastik]) as [plastik] " & _
            "from v_selisihtimb inner join tradm_gud on v_selisihtimb.nobukti=tradm_gud.nobukti " & _
            "where tradm_gud.sbatal=0 and tradm_gud.jenistrans='TR PEMB' and tradm_gud.sambil=1 " & _
            "and tradm_gud.tglberangkat >='{0}' and tradm_gud.tglberangkat <='{1}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If tnopol.Text.Trim.Length > 0 Then
            sql = String.Format(" {0} and tradm_gud.nopol='{1}'", sql, tnopol.Text.Trim)
        End If

        sql = String.Format(" {0} group by tradm_gud.nobukti,tradm_gud.tanggal,tradm_gud.nopol,tradm_gud.jmltimb", sql)

        Dim periode As String = String.Format("{0} s.d {1}", convert_date_to_ind(ttgl.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Dim fs As New fpr_selisihtimb2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .tgl = periode}
        fs.ShowDialog()

        Cursor = Cursors.Default

    End Sub

    Private Sub fpr_giromasuk1_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub fpr_giromasuk1_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ttgl.EditValue = Date.Now
        ttgl2.EditValue = Date.Now

    End Sub

End Class