Public Class fdateutil 

    Private Sub fdateutil_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ttgl1.EditValue = DateValue(tglperiod1)
        ttgl2.EditValue = DateValue(tglperiod2)
    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btsave_Click(sender As System.Object, e As System.EventArgs) Handles btsave.Click

        If DateValue(ttgl1.EditValue) > DateValue(ttgl2.EditValue) Then
            MsgBox("Periode tanggal salah, periksa kembali...", vbOKOnly + vbExclamation, "Informasi")
            ttgl1.Focus()
            Return
        End If

        tglperiod1 = ttgl1.EditValue
        tglperiod2 = ttgl2.EditValue

        futama.tsperiode.Caption = String.Format("Periode : {0}  s.d  {1}", tglperiod1, tglperiod2)

        MsgBox("Sett Periode telah dilakukan...", vbOKOnly + vbInformation, "Informasi")
        Me.Close()

    End Sub

End Class