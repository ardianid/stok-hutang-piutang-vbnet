Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fpr_bocoran

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btload_Click(sender As System.Object, e As System.EventArgs) Handles btload.Click

        Cursor = Cursors.WaitCursor

        Dim sql As String = String.Format("SELECT     tradm_gud.nobukti, tradm_gud.tanggal as tglmuat, tradm_gud.nobukti_gd, tradm_gud.nopol,ms_pegawai.nama_karyawan as nama_krani, " & _
            "sum(v_bocoranjabung.[19LTR]) as [19LTR], " & _
            "sum(v_bocoranjabung.[150ML/50]) as [150ML/50], " & _
            "sum(v_bocoranjabung.[240ML/48]) as [240ML/48], " & _
            "sum(v_bocoranjabung.[330ML/248]) as [330ML/248], " & _
            "sum(v_bocoranjabung.[500ML/24]) as [500ML/24], " & _
            "sum(v_bocoranjabung.[600ML/24]) as [600ML/24], " & _
            "sum(v_bocoranjabung.[1500ML/12]) as [1500ML/12], " & _
            "sum(v_bocoranjabung.[19LTR.b]) as [19LTR.b], " & _
            "sum(v_bocoranjabung.[150ML/50.b]) as [150ML/50.b], " & _
            "sum(v_bocoranjabung.[240ML/48.b]) as [240ML/48.b], " & _
            "sum(v_bocoranjabung.[330ML/248.b]) as [330ML/248.b], " & _
            "sum(v_bocoranjabung.[500ML/24.b]) as [500ML/24.b],  " & _
            "sum(v_bocoranjabung.[600ML/24.b]) as [600ML/24.b], " & _
            "sum(v_bocoranjabung.[1500ML/12.b]) as [1500ML/12.b] " & _
            "FROM v_bocoranjabung INNER JOIN tradm_gud ON v_bocoranjabung.nobukti = tradm_gud.nobukti " & _
            "inner join ms_pegawai on tradm_gud.kd_krani=ms_pegawai.kd_karyawan " & _
            "WHERE tradm_gud.sbatal=0 and tradm_gud.jenistrans='TR PEMB' and tradm_gud.sambil=1 " & _
            " and (v_bocoranjabung.[19LTR.b] >0 or v_bocoranjabung.[150ML/50.b] >0 or v_bocoranjabung.[240ML/48.b] >0 or v_bocoranjabung.[330ML/248.b] >0 " & _
            "or v_bocoranjabung.[500ML/24.b] >0 or v_bocoranjabung.[600ML/24.b] >0 or v_bocoranjabung.[1500ML/12.b] >0) " & _
            "and tradm_gud.tglberangkat >='{0}' and tradm_gud.tglberangkat <='{1}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If tnopol.Text.Trim.Length > 0 Then
            sql = String.Format(" {0} and tradm_gud.nopol='{1}'", sql, tnopol.Text.Trim)
        End If

        If tnosj.Text.Trim.Length > 0 Then
            sql = String.Format(" {0} and tradm_gud.nobukti_gd='{1}'", sql, tnosj.Text.Trim)
        End If

        sql = String.Format(" {0} group by tradm_gud.nobukti, tradm_gud.tanggal, tradm_gud.nobukti_gd, tradm_gud.nopol,ms_pegawai.nama_karyawan", sql)

        Dim periode As String = String.Format("{0} s.d {1}", convert_date_to_ind(ttgl.EditValue), convert_date_to_ind(ttgl2.EditValue))

        Dim fs As New fpr_bocoran2 With {.WindowState = FormWindowState.Maximized, .sql = sql, .tgl = periode}
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