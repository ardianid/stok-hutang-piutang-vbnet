<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fpr_akum_penjualan_perbulan
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
        Me.ttahun = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tbln1 = New DevExpress.XtraEditors.TextEdit()
        Me.tbln2 = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btload = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ttahun.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbln1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbln2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(70, 26)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(37, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Tahun :"
        '
        'ttahun
        '
        Me.ttahun.Location = New System.Drawing.Point(113, 23)
        Me.ttahun.Name = "ttahun"
        Me.ttahun.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.ttahun.Properties.Mask.EditMask = "f0"
        Me.ttahun.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.ttahun.Size = New System.Drawing.Size(67, 20)
        Me.ttahun.TabIndex = 0
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(52, 52)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(55, 13)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "Dari Bulan :"
        '
        'tbln1
        '
        Me.tbln1.Location = New System.Drawing.Point(113, 49)
        Me.tbln1.Name = "tbln1"
        Me.tbln1.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.tbln1.Properties.Mask.EditMask = "f0"
        Me.tbln1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tbln1.Size = New System.Drawing.Size(67, 20)
        Me.tbln1.TabIndex = 1
        '
        'tbln2
        '
        Me.tbln2.Location = New System.Drawing.Point(113, 75)
        Me.tbln2.Name = "tbln2"
        Me.tbln2.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.tbln2.Properties.Mask.EditMask = "f0"
        Me.tbln2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tbln2.Size = New System.Drawing.Size(67, 20)
        Me.tbln2.TabIndex = 2
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(37, 78)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(70, 13)
        Me.LabelControl3.TabIndex = 4
        Me.LabelControl3.Text = "Sampai Bulan :"
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(120, 127)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(60, 26)
        Me.btclose.TabIndex = 4
        Me.btclose.Text = "&Keluar"
        '
        'btload
        '
        Me.btload.Location = New System.Drawing.Point(54, 127)
        Me.btload.Name = "btload"
        Me.btload.Size = New System.Drawing.Size(60, 26)
        Me.btload.TabIndex = 3
        Me.btload.Text = "&Load"
        '
        'fpr_akum_penjualan_perbulan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(211, 166)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btload)
        Me.Controls.Add(Me.tbln2)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.tbln1)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.ttahun)
        Me.Controls.Add(Me.LabelControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fpr_akum_penjualan_perbulan"
        Me.Text = "Akumulasi Penj Per-Bulan"
        CType(Me.ttahun.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbln1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbln2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttahun As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbln1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tbln2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btload As DevExpress.XtraEditors.SimpleButton
End Class
