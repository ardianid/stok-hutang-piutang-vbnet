<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fdtg_ord
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
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tsales = New DevExpress.XtraEditors.LookUpEdit()
        CType(Me.tsales.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(243, 76)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(50, 25)
        Me.btclose.TabIndex = 2
        Me.btclose.Text = "&Keluar"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(187, 76)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(50, 25)
        Me.btsimpan.TabIndex = 1
        Me.btsimpan.Text = "&Ok"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(31, 32)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(30, 13)
        Me.LabelControl1.TabIndex = 6
        Me.LabelControl1.Text = "Jalur :"
        '
        'tsales
        '
        Me.tsales.Location = New System.Drawing.Point(69, 29)
        Me.tsales.Name = "tsales"
        Me.tsales.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tsales.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("kd_jalur", 5, "Kode"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama_jalur", "Nama")})
        Me.tsales.Properties.DisplayMember = "nama_jalur"
        Me.tsales.Properties.NullText = ""
        Me.tsales.Properties.ValueMember = "kd_jalur"
        Me.tsales.Size = New System.Drawing.Size(224, 20)
        Me.tsales.TabIndex = 0
        '
        'fdtg_ord
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(333, 118)
        Me.ControlBox = False
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.tsales)
        Me.Name = "fdtg_ord"
        Me.Text = "Jalur Order"
        CType(Me.tsales.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tsales As DevExpress.XtraEditors.LookUpEdit
End Class
