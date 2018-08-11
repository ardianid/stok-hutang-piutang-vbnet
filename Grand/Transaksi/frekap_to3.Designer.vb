<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frekap_to3
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
        Me.tbukti = New DevExpress.XtraEditors.TextEdit()
        Me.btadd = New DevExpress.XtraEditors.SimpleButton()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.tbukti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 30)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "No Bukti :"
        '
        'tbukti
        '
        Me.tbukti.Location = New System.Drawing.Point(64, 27)
        Me.tbukti.Name = "tbukti"
        Me.tbukti.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbukti.Size = New System.Drawing.Size(223, 20)
        Me.tbukti.TabIndex = 1
        '
        'btadd
        '
        Me.btadd.Location = New System.Drawing.Point(163, 86)
        Me.btadd.Name = "btadd"
        Me.btadd.Size = New System.Drawing.Size(59, 23)
        Me.btadd.TabIndex = 2
        Me.btadd.Text = "&Tambah"
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(228, 86)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(59, 23)
        Me.btclose.TabIndex = 3
        Me.btclose.Text = "&Keluar"
        '
        'frekap_to3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(307, 124)
        Me.ControlBox = False
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btadd)
        Me.Controls.Add(Me.tbukti)
        Me.Controls.Add(Me.LabelControl1)
        Me.Name = "frekap_to3"
        Me.Text = "Add Faktur"
        CType(Me.tbukti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbukti As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
End Class
