<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fdtg_psar
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
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.talamat = New DevExpress.XtraEditors.TextEdit()
        CType(Me.tsales.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.talamat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(335, 102)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(50, 25)
        Me.btclose.TabIndex = 3
        Me.btclose.Text = "&Keluar"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(279, 102)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(50, 25)
        Me.btsimpan.TabIndex = 2
        Me.btsimpan.Text = "&Ok"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(49, 28)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(34, 13)
        Me.LabelControl1.TabIndex = 6
        Me.LabelControl1.Text = "Pasar :"
        '
        'tsales
        '
        Me.tsales.Location = New System.Drawing.Point(89, 25)
        Me.tsales.Name = "tsales"
        Me.tsales.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tsales.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("kd_pasar", 5, "Kode"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama_pasar", 15, "Nama"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama_kel", "Kelurahan")})
        Me.tsales.Properties.DisplayMember = "nama_pasar"
        Me.tsales.Properties.NullText = ""
        Me.tsales.Properties.ValueMember = "kd_pasar"
        Me.tsales.Size = New System.Drawing.Size(296, 20)
        Me.tsales.TabIndex = 0
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(28, 54)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(55, 13)
        Me.LabelControl2.TabIndex = 18
        Me.LabelControl2.Text = "Kelurahan :"
        '
        'talamat
        '
        Me.talamat.Enabled = False
        Me.talamat.Location = New System.Drawing.Point(89, 51)
        Me.talamat.Name = "talamat"
        Me.talamat.Size = New System.Drawing.Size(296, 20)
        Me.talamat.TabIndex = 1
        '
        'fdtg_psar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(424, 144)
        Me.ControlBox = False
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.tsales)
        Me.Controls.Add(Me.talamat)
        Me.Name = "fdtg_psar"
        Me.Text = "Pasar"
        CType(Me.tsales.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.talamat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tsales As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents talamat As DevExpress.XtraEditors.TextEdit
End Class
