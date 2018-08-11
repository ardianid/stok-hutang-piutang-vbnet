<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fdtg_outl
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
        Me.talamat = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.tsales.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.talamat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(320, 152)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(50, 25)
        Me.btclose.TabIndex = 2
        Me.btclose.Text = "&Keluar"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(264, 152)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(50, 25)
        Me.btsimpan.TabIndex = 1
        Me.btsimpan.Text = "&Ok"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(31, 32)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(37, 13)
        Me.LabelControl1.TabIndex = 14
        Me.LabelControl1.Text = "Outlet :"
        '
        'tsales
        '
        Me.tsales.Location = New System.Drawing.Point(74, 29)
        Me.tsales.Name = "tsales"
        Me.tsales.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tsales.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("kd_toko", 5, "Kode"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama_toko", 15, "Nama"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("alamat_toko", "Alamat")})
        Me.tsales.Properties.DisplayMember = "nama_toko"
        Me.tsales.Properties.NullText = ""
        Me.tsales.Properties.ValueMember = "kd_toko"
        Me.tsales.Size = New System.Drawing.Size(296, 20)
        Me.tsales.TabIndex = 0
        '
        'talamat
        '
        Me.talamat.Enabled = False
        Me.talamat.Location = New System.Drawing.Point(74, 55)
        Me.talamat.Name = "talamat"
        Me.talamat.Size = New System.Drawing.Size(296, 48)
        Me.talamat.TabIndex = 15
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(28, 58)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(40, 13)
        Me.LabelControl2.TabIndex = 16
        Me.LabelControl2.Text = "Alamat :"
        '
        'fdtg_outl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(408, 189)
        Me.ControlBox = False
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.talamat)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.tsales)
        Me.Name = "fdtg_outl"
        Me.Text = "Outlet"
        CType(Me.tsales.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.talamat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tsales As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents talamat As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
End Class
