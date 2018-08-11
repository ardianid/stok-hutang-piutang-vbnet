<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ffaktur_to3
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
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tjml = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.tsat = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.tdisc_rp = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.tjumlah = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        Me.tstok1 = New DevExpress.XtraEditors.TextEdit()
        Me.tstok2 = New DevExpress.XtraEditors.TextEdit()
        Me.tstok3 = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.tharga = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.tdisc_per = New DevExpress.XtraEditors.TextEdit()
        Me.tgudang = New DevExpress.XtraEditors.LookUpEdit()
        Me.tbarang = New DevExpress.XtraEditors.LookUpEdit()
        Me.tbarang0 = New DevExpress.XtraEditors.LookUpEdit()
        CType(Me.tjml.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tsat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tdisc_rp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tjumlah.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tstok1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tstok2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tstok3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tharga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tdisc_per.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tgudang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbarang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbarang0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(15, 15)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(44, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Gudang :"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(18, 37)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(41, 13)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "Barang :"
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(37, 83)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(22, 13)
        Me.LabelControl3.TabIndex = 4
        Me.LabelControl3.Text = "Jml :"
        '
        'tjml
        '
        Me.tjml.EditValue = "0"
        Me.tjml.Location = New System.Drawing.Point(65, 80)
        Me.tjml.Name = "tjml"
        Me.tjml.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tjml.Properties.Appearance.Options.UseFont = True
        Me.tjml.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tjml.Properties.Mask.EditMask = "n0"
        Me.tjml.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tjml.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tjml.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tjml.Size = New System.Drawing.Size(153, 20)
        Me.tjml.TabIndex = 3
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(15, 106)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(41, 13)
        Me.LabelControl4.TabIndex = 147
        Me.LabelControl4.Text = "Satuan :"
        '
        'tsat
        '
        Me.tsat.Location = New System.Drawing.Point(65, 103)
        Me.tsat.Name = "tsat"
        Me.tsat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tsat.Size = New System.Drawing.Size(153, 20)
        Me.tsat.TabIndex = 4
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(30, 153)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(26, 13)
        Me.LabelControl5.TabIndex = 149
        Me.LabelControl5.Text = "Disc :"
        '
        'tdisc_rp
        '
        Me.tdisc_rp.EditValue = "0"
        Me.tdisc_rp.Location = New System.Drawing.Point(120, 150)
        Me.tdisc_rp.Name = "tdisc_rp"
        Me.tdisc_rp.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tdisc_rp.Properties.Appearance.Options.UseFont = True
        Me.tdisc_rp.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tdisc_rp.Properties.Mask.EditMask = "n0"
        Me.tdisc_rp.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tdisc_rp.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tdisc_rp.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tdisc_rp.Size = New System.Drawing.Size(98, 20)
        Me.tdisc_rp.TabIndex = 7
        '
        'LabelControl12
        '
        Me.LabelControl12.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl12.Location = New System.Drawing.Point(105, 154)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Size = New System.Drawing.Size(13, 13)
        Me.LabelControl12.TabIndex = 151
        Me.LabelControl12.Text = "%"
        '
        'tjumlah
        '
        Me.tjumlah.EditValue = "0"
        Me.tjumlah.Location = New System.Drawing.Point(65, 174)
        Me.tjumlah.Name = "tjumlah"
        Me.tjumlah.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tjumlah.Properties.Appearance.Options.UseFont = True
        Me.tjumlah.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tjumlah.Properties.Mask.EditMask = "n0"
        Me.tjumlah.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tjumlah.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tjumlah.Properties.ReadOnly = True
        Me.tjumlah.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tjumlah.Size = New System.Drawing.Size(153, 20)
        Me.tjumlah.TabIndex = 153
        '
        'LabelControl6
        '
        Me.LabelControl6.Location = New System.Drawing.Point(16, 177)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(40, 13)
        Me.LabelControl6.TabIndex = 154
        Me.LabelControl6.Text = "Jumlah :"
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(264, 212)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(67, 23)
        Me.btclose.TabIndex = 9
        Me.btclose.Text = "&Keluar"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(191, 212)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(67, 23)
        Me.btsimpan.TabIndex = 8
        Me.btsimpan.Text = "&Simpan"
        '
        'tstok1
        '
        Me.tstok1.EditValue = "0"
        Me.tstok1.Location = New System.Drawing.Point(65, 57)
        Me.tstok1.Name = "tstok1"
        Me.tstok1.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstok1.Properties.Appearance.Options.UseFont = True
        Me.tstok1.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tstok1.Properties.Mask.EditMask = "n0"
        Me.tstok1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tstok1.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tstok1.Properties.ReadOnly = True
        Me.tstok1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tstok1.Size = New System.Drawing.Size(49, 20)
        Me.tstok1.TabIndex = 154
        '
        'tstok2
        '
        Me.tstok2.EditValue = "0"
        Me.tstok2.Location = New System.Drawing.Point(117, 57)
        Me.tstok2.Name = "tstok2"
        Me.tstok2.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstok2.Properties.Appearance.Options.UseFont = True
        Me.tstok2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tstok2.Properties.Mask.EditMask = "n0"
        Me.tstok2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tstok2.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tstok2.Properties.ReadOnly = True
        Me.tstok2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tstok2.Size = New System.Drawing.Size(49, 20)
        Me.tstok2.TabIndex = 155
        '
        'tstok3
        '
        Me.tstok3.EditValue = "0"
        Me.tstok3.Location = New System.Drawing.Point(169, 57)
        Me.tstok3.Name = "tstok3"
        Me.tstok3.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstok3.Properties.Appearance.Options.UseFont = True
        Me.tstok3.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tstok3.Properties.Mask.EditMask = "n0"
        Me.tstok3.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tstok3.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tstok3.Properties.ReadOnly = True
        Me.tstok3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tstok3.Size = New System.Drawing.Size(49, 20)
        Me.tstok3.TabIndex = 156
        '
        'LabelControl7
        '
        Me.LabelControl7.Location = New System.Drawing.Point(31, 60)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(28, 13)
        Me.LabelControl7.TabIndex = 159
        Me.LabelControl7.Text = "Stok :"
        '
        'tharga
        '
        Me.tharga.EditValue = "0"
        Me.tharga.Location = New System.Drawing.Point(65, 126)
        Me.tharga.Name = "tharga"
        Me.tharga.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tharga.Properties.Appearance.Options.UseFont = True
        Me.tharga.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tharga.Properties.Mask.EditMask = "n0"
        Me.tharga.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tharga.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tharga.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tharga.Size = New System.Drawing.Size(153, 20)
        Me.tharga.TabIndex = 5
        '
        'LabelControl8
        '
        Me.LabelControl8.Location = New System.Drawing.Point(20, 129)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(36, 13)
        Me.LabelControl8.TabIndex = 160
        Me.LabelControl8.Text = "Harga :"
        '
        'tdisc_per
        '
        Me.tdisc_per.EditValue = "0"
        Me.tdisc_per.Location = New System.Drawing.Point(65, 150)
        Me.tdisc_per.Name = "tdisc_per"
        Me.tdisc_per.Properties.Mask.EditMask = "n"
        Me.tdisc_per.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tdisc_per.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tdisc_per.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tdisc_per.Size = New System.Drawing.Size(36, 20)
        Me.tdisc_per.TabIndex = 6
        '
        'tgudang
        '
        Me.tgudang.Location = New System.Drawing.Point(65, 12)
        Me.tgudang.Name = "tgudang"
        Me.tgudang.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tgudang.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("kd_gudang", 5, "Kode"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama_gudang", "Nama")})
        Me.tgudang.Properties.DisplayMember = "nama_gudang"
        Me.tgudang.Properties.NullText = ""
        Me.tgudang.Properties.ValueMember = "kd_gudang"
        Me.tgudang.Size = New System.Drawing.Size(266, 20)
        Me.tgudang.TabIndex = 0
        '
        'tbarang
        '
        Me.tbarang.Location = New System.Drawing.Point(126, 34)
        Me.tbarang.Name = "tbarang"
        Me.tbarang.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tbarang.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama_barang", "Nama")})
        Me.tbarang.Properties.DisplayMember = "nama_barang"
        Me.tbarang.Properties.NullText = ""
        Me.tbarang.Properties.ValueMember = "kd_barang"
        Me.tbarang.Size = New System.Drawing.Size(205, 20)
        Me.tbarang.TabIndex = 2
        '
        'tbarang0
        '
        Me.tbarang0.Location = New System.Drawing.Point(65, 34)
        Me.tbarang0.Name = "tbarang0"
        Me.tbarang0.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tbarang0.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("kd_barang", "Kode")})
        Me.tbarang0.Properties.DisplayMember = "kd_barang"
        Me.tbarang0.Properties.NullText = ""
        Me.tbarang0.Properties.ValueMember = "kd_barang"
        Me.tbarang0.Size = New System.Drawing.Size(58, 20)
        Me.tbarang0.TabIndex = 1
        '
        'ffaktur_to3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(343, 247)
        Me.ControlBox = False
        Me.Controls.Add(Me.tbarang0)
        Me.Controls.Add(Me.tdisc_per)
        Me.Controls.Add(Me.tharga)
        Me.Controls.Add(Me.LabelControl8)
        Me.Controls.Add(Me.LabelControl7)
        Me.Controls.Add(Me.tstok3)
        Me.Controls.Add(Me.tstok2)
        Me.Controls.Add(Me.tstok1)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.tjumlah)
        Me.Controls.Add(Me.tdisc_rp)
        Me.Controls.Add(Me.LabelControl12)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.tsat)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.tjml)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.tgudang)
        Me.Controls.Add(Me.tbarang)
        Me.Name = "ffaktur_to3"
        Me.Text = "Faktur TO (Barang)"
        CType(Me.tjml.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tsat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tdisc_rp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tjumlah.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tstok1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tstok2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tstok3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tharga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tdisc_per.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tgudang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbarang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbarang0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tjml As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tsat As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tdisc_rp As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tjumlah As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tstok3 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tstok2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tstok1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tharga As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tdisc_per As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tgudang As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents tbarang As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents tbarang0 As DevExpress.XtraEditors.LookUpEdit
End Class
