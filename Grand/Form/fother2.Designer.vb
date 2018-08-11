<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fother2
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
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.ttipe = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.tval = New DevExpress.XtraEditors.TextEdit()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ttipe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tval.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(24, 53)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(21, 13)
        Me.LabelControl2.TabIndex = 8
        Me.LabelControl2.Text = "Val :"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(18, 27)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(27, 13)
        Me.LabelControl1.TabIndex = 7
        Me.LabelControl1.Text = "Tipe :"
        '
        'ttipe
        '
        Me.ttipe.Location = New System.Drawing.Point(51, 24)
        Me.ttipe.Name = "ttipe"
        Me.ttipe.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttipe.Properties.Items.AddRange(New Object() {"BANK", "BATAL TRANSAKSI", "RETUR"})
        Me.ttipe.Size = New System.Drawing.Size(272, 20)
        Me.ttipe.TabIndex = 0
        '
        'tval
        '
        Me.tval.Location = New System.Drawing.Point(51, 50)
        Me.tval.Name = "tval"
        Me.tval.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tval.Size = New System.Drawing.Size(272, 20)
        Me.tval.TabIndex = 1
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(248, 98)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 23)
        Me.btclose.TabIndex = 3
        Me.btclose.Text = "&Keluar"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(167, 98)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 23)
        Me.btsimpan.TabIndex = 2
        Me.btsimpan.Text = "&Simpan"
        '
        'fother2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(344, 132)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.ttipe)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.tval)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fother2"
        Me.Text = "Other"
        CType(Me.ttipe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tval.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttipe As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents tval As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
End Class
