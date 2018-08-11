<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class f2sales2
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
        Me.tspv = New DevExpress.XtraEditors.LookUpEdit()
        Me.caktif = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tkode = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.tnama = New DevExpress.XtraEditors.LookUpEdit()
        Me.tket = New DevExpress.XtraEditors.MemoEdit()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.tspv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.caktif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tkode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tket.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(35, 22)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(58, 13)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Supervisor :"
        '
        'tspv
        '
        Me.tspv.Location = New System.Drawing.Point(99, 19)
        Me.tspv.Name = "tspv"
        Me.tspv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tspv.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("kd_sales1", 5, "Kd Spv"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("kd_karyawan", 7, "Kode"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama_karyawan", "Nama")})
        Me.tspv.Properties.DisplayMember = "nama_karyawan"
        Me.tspv.Properties.NullText = ""
        Me.tspv.Properties.ValueMember = "kd_sales1"
        Me.tspv.Size = New System.Drawing.Size(288, 20)
        Me.tspv.TabIndex = 0
        '
        'caktif
        '
        Me.caktif.Location = New System.Drawing.Point(333, 45)
        Me.caktif.Name = "caktif"
        Me.caktif.Properties.Caption = "Aktif"
        Me.caktif.Size = New System.Drawing.Size(54, 19)
        Me.caktif.TabIndex = 2
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(70, 100)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(23, 13)
        Me.LabelControl3.TabIndex = 12
        Me.LabelControl3.Text = "Ket :"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(59, 74)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(34, 13)
        Me.LabelControl2.TabIndex = 10
        Me.LabelControl2.Text = "Nama :"
        '
        'tkode
        '
        Me.tkode.Location = New System.Drawing.Point(99, 45)
        Me.tkode.Name = "tkode"
        Me.tkode.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tkode.Size = New System.Drawing.Size(228, 20)
        Me.tkode.TabIndex = 1
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(62, 48)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(31, 13)
        Me.LabelControl4.TabIndex = 6
        Me.LabelControl4.Text = "Kode :"
        '
        'tnama
        '
        Me.tnama.Location = New System.Drawing.Point(99, 71)
        Me.tnama.Name = "tnama"
        Me.tnama.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tnama.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("kd_karyawan", 7, "Kode"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama_karyawan", "Nama")})
        Me.tnama.Properties.DisplayMember = "nama_karyawan"
        Me.tnama.Properties.NullText = ""
        Me.tnama.Properties.ValueMember = "kd_karyawan"
        Me.tnama.Size = New System.Drawing.Size(288, 20)
        Me.tnama.TabIndex = 3
        '
        'tket
        '
        Me.tket.Location = New System.Drawing.Point(99, 97)
        Me.tket.Name = "tket"
        Me.tket.Size = New System.Drawing.Size(288, 48)
        Me.tket.TabIndex = 4
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(312, 197)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 23)
        Me.btclose.TabIndex = 6
        Me.btclose.Text = "&Keluar"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(231, 197)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 23)
        Me.btsimpan.TabIndex = 5
        Me.btsimpan.Text = "&Simpan"
        '
        'f2sales2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(436, 230)
        Me.ControlBox = False
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.caktif)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.tkode)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.tnama)
        Me.Controls.Add(Me.tket)
        Me.Controls.Add(Me.tspv)
        Me.Controls.Add(Me.LabelControl1)
        Me.Name = "f2sales2"
        Me.Text = "Sales Lev 1.2"
        CType(Me.tspv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.caktif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tkode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tket.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tspv As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents caktif As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tkode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tnama As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents tket As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
End Class
