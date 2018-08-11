<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fkonfirm_hapus
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
        Me.cbalasan = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btok = New DevExpress.XtraEditors.SimpleButton()
        Me.btcancel = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.cbalasan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cbalasan
        '
        Me.cbalasan.Location = New System.Drawing.Point(12, 12)
        Me.cbalasan.Name = "cbalasan"
        Me.cbalasan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbalasan.Size = New System.Drawing.Size(330, 20)
        Me.cbalasan.TabIndex = 1
        '
        'btok
        '
        Me.btok.Location = New System.Drawing.Point(186, 38)
        Me.btok.Name = "btok"
        Me.btok.Size = New System.Drawing.Size(75, 23)
        Me.btok.TabIndex = 2
        Me.btok.Text = "&Ok"
        '
        'btcancel
        '
        Me.btcancel.Location = New System.Drawing.Point(267, 38)
        Me.btcancel.Name = "btcancel"
        Me.btcancel.Size = New System.Drawing.Size(75, 23)
        Me.btcancel.TabIndex = 3
        Me.btcancel.Text = "&Cancel"
        '
        'fkonfirm_hapus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(357, 69)
        Me.ControlBox = False
        Me.Controls.Add(Me.btcancel)
        Me.Controls.Add(Me.btok)
        Me.Controls.Add(Me.cbalasan)
        Me.Name = "fkonfirm_hapus"
        Me.Text = "Alasan Batal"
        CType(Me.cbalasan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cbalasan As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btcancel As DevExpress.XtraEditors.SimpleButton
End Class
