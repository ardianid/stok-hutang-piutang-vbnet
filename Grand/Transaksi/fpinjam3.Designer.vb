﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fpinjam3
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
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.tstok2 = New DevExpress.XtraEditors.TextEdit()
        Me.tstok1 = New DevExpress.XtraEditors.TextEdit()
        Me.tsat = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.tjml = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tgudang = New DevExpress.XtraEditors.LookUpEdit()
        Me.tbarang = New DevExpress.XtraEditors.LookUpEdit()
        Me.tstok3 = New DevExpress.XtraEditors.TextEdit()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        Me.tbarang0 = New DevExpress.XtraEditors.LookUpEdit()
        CType(Me.tstok2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tstok1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tsat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tjml.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tgudang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbarang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tstok3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbarang0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl7
        '
        Me.LabelControl7.Location = New System.Drawing.Point(35, 76)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(28, 13)
        Me.LabelControl7.TabIndex = 184
        Me.LabelControl7.Text = "Stok :"
        '
        'tstok2
        '
        Me.tstok2.EditValue = "0"
        Me.tstok2.Location = New System.Drawing.Point(140, 73)
        Me.tstok2.Name = "tstok2"
        Me.tstok2.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstok2.Properties.Appearance.Options.UseFont = True
        Me.tstok2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tstok2.Properties.Mask.EditMask = "n0"
        Me.tstok2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tstok2.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tstok2.Properties.ReadOnly = True
        Me.tstok2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tstok2.Size = New System.Drawing.Size(57, 20)
        Me.tstok2.TabIndex = 183
        '
        'tstok1
        '
        Me.tstok1.EditValue = "0"
        Me.tstok1.Location = New System.Drawing.Point(79, 73)
        Me.tstok1.Name = "tstok1"
        Me.tstok1.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstok1.Properties.Appearance.Options.UseFont = True
        Me.tstok1.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tstok1.Properties.Mask.EditMask = "n0"
        Me.tstok1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tstok1.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tstok1.Properties.ReadOnly = True
        Me.tstok1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tstok1.Size = New System.Drawing.Size(57, 20)
        Me.tstok1.TabIndex = 182
        '
        'tsat
        '
        Me.tsat.Location = New System.Drawing.Point(79, 125)
        Me.tsat.Name = "tsat"
        Me.tsat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tsat.Size = New System.Drawing.Size(100, 20)
        Me.tsat.TabIndex = 4
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(22, 128)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(41, 13)
        Me.LabelControl4.TabIndex = 181
        Me.LabelControl4.Text = "Satuan :"
        '
        'tjml
        '
        Me.tjml.EditValue = "0"
        Me.tjml.Location = New System.Drawing.Point(79, 99)
        Me.tjml.Name = "tjml"
        Me.tjml.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tjml.Properties.Appearance.Options.UseFont = True
        Me.tjml.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tjml.Properties.Mask.EditMask = "n0"
        Me.tjml.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tjml.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tjml.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tjml.Size = New System.Drawing.Size(100, 20)
        Me.tjml.TabIndex = 3
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(41, 102)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(22, 13)
        Me.LabelControl3.TabIndex = 180
        Me.LabelControl3.Text = "Jml :"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(25, 50)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(41, 13)
        Me.LabelControl2.TabIndex = 179
        Me.LabelControl2.Text = "Barang :"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(22, 24)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(44, 13)
        Me.LabelControl1.TabIndex = 178
        Me.LabelControl1.Text = "Gudang :"
        '
        'tgudang
        '
        Me.tgudang.Location = New System.Drawing.Point(79, 21)
        Me.tgudang.Name = "tgudang"
        Me.tgudang.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tgudang.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("kd_gudang", 5, "Kode"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama_gudang", "Nama")})
        Me.tgudang.Properties.DisplayMember = "nama_gudang"
        Me.tgudang.Properties.NullText = ""
        Me.tgudang.Properties.ValueMember = "kd_gudang"
        Me.tgudang.Size = New System.Drawing.Size(312, 20)
        Me.tgudang.TabIndex = 0
        '
        'tbarang
        '
        Me.tbarang.Location = New System.Drawing.Point(140, 47)
        Me.tbarang.Name = "tbarang"
        Me.tbarang.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tbarang.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("kd_barang", "Kode", 5, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama_barang", "Nama"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("jmlstok1", "Stok1", 5, DevExpress.Utils.FormatType.Numeric, "n0", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("jmlstok2", "Stok2", 5, DevExpress.Utils.FormatType.Numeric, "n0", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("jmlstok3", "Stok3", 5, DevExpress.Utils.FormatType.Numeric, "n0", False, DevExpress.Utils.HorzAlignment.Far)})
        Me.tbarang.Properties.DisplayMember = "nama_barang"
        Me.tbarang.Properties.NullText = ""
        Me.tbarang.Properties.ValueMember = "kd_barang"
        Me.tbarang.Size = New System.Drawing.Size(251, 20)
        Me.tbarang.TabIndex = 2
        '
        'tstok3
        '
        Me.tstok3.EditValue = "0"
        Me.tstok3.Location = New System.Drawing.Point(203, 73)
        Me.tstok3.Name = "tstok3"
        Me.tstok3.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstok3.Properties.Appearance.Options.UseFont = True
        Me.tstok3.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tstok3.Properties.Mask.EditMask = "n0"
        Me.tstok3.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tstok3.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tstok3.Properties.ReadOnly = True
        Me.tstok3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tstok3.Size = New System.Drawing.Size(57, 20)
        Me.tstok3.TabIndex = 185
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(316, 164)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 25)
        Me.btclose.TabIndex = 6
        Me.btclose.Text = "&Keluar"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(235, 164)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 25)
        Me.btsimpan.TabIndex = 5
        Me.btsimpan.Text = "&Simpan"
        '
        'tbarang0
        '
        Me.tbarang0.Location = New System.Drawing.Point(79, 47)
        Me.tbarang0.Name = "tbarang0"
        Me.tbarang0.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tbarang0.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("kd_barang", "Kode")})
        Me.tbarang0.Properties.DisplayMember = "kd_barang"
        Me.tbarang0.Properties.NullText = ""
        Me.tbarang0.Properties.ValueMember = "kd_barang"
        Me.tbarang0.Size = New System.Drawing.Size(57, 20)
        Me.tbarang0.TabIndex = 1
        '
        'fpinjam3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(418, 201)
        Me.ControlBox = False
        Me.Controls.Add(Me.tbarang0)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.tstok3)
        Me.Controls.Add(Me.LabelControl7)
        Me.Controls.Add(Me.tstok2)
        Me.Controls.Add(Me.tstok1)
        Me.Controls.Add(Me.tsat)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.tjml)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.tgudang)
        Me.Controls.Add(Me.tbarang)
        Me.Name = "fpinjam3"
        Me.Text = "Peminjaman Barang"
        CType(Me.tstok2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tstok1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tsat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tjml.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tgudang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbarang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tstok3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbarang0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tstok2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tstok1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tsat As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tjml As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tgudang As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents tbarang As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents tstok3 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tbarang0 As DevExpress.XtraEditors.LookUpEdit
End Class
