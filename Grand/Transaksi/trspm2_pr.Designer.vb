<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class trspm2_pr
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.bt_gln = New DevExpress.XtraEditors.SimpleButton()
        Me.bt_dus = New DevExpress.XtraEditors.SimpleButton()
        Me.bt_all = New DevExpress.XtraEditors.SimpleButton()
        Me.btcanc = New DevExpress.XtraEditors.SimpleButton()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 12)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(264, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Silahkan pilih opsi bukti perintah muat yang akan diprint"
        '
        'bt_gln
        '
        Me.bt_gln.Location = New System.Drawing.Point(12, 43)
        Me.bt_gln.Name = "bt_gln"
        Me.bt_gln.Size = New System.Drawing.Size(93, 37)
        Me.bt_gln.TabIndex = 1
        Me.bt_gln.Text = "&GALLON"
        '
        'bt_dus
        '
        Me.bt_dus.Location = New System.Drawing.Point(111, 43)
        Me.bt_dus.Name = "bt_dus"
        Me.bt_dus.Size = New System.Drawing.Size(93, 37)
        Me.bt_dus.TabIndex = 2
        Me.bt_dus.Text = "&DUS"
        '
        'bt_all
        '
        Me.bt_all.Location = New System.Drawing.Point(210, 43)
        Me.bt_all.Name = "bt_all"
        Me.bt_all.Size = New System.Drawing.Size(93, 37)
        Me.bt_all.TabIndex = 3
        Me.bt_all.Text = "&ALL/CAMPURAN"
        '
        'btcanc
        '
        Me.btcanc.Location = New System.Drawing.Point(309, 43)
        Me.btcanc.Name = "btcanc"
        Me.btcanc.Size = New System.Drawing.Size(93, 37)
        Me.btcanc.TabIndex = 4
        Me.btcanc.Text = "&BATAL"
        '
        'trspm2_pr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(416, 88)
        Me.Controls.Add(Me.btcanc)
        Me.Controls.Add(Me.bt_all)
        Me.Controls.Add(Me.bt_dus)
        Me.Controls.Add(Me.bt_gln)
        Me.Controls.Add(Me.LabelControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "trspm2_pr"
        Me.Text = "Konfirm Print"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents bt_gln As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents bt_dus As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents bt_all As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btcanc As DevExpress.XtraEditors.SimpleButton
End Class
