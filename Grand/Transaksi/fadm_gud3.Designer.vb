<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fadm_gud3
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
        Me.tbarang = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tjenis = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.tsat = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.tjml = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.tjml2 = New DevExpress.XtraEditors.TextEdit()
        Me.tbarang0 = New DevExpress.XtraEditors.LookUpEdit()
        CType(Me.tbarang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tjenis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tsat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tjml.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tjml2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbarang0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(39, 15)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(41, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Barang :"
        '
        'tbarang
        '
        Me.tbarang.Location = New System.Drawing.Point(147, 12)
        Me.tbarang.Name = "tbarang"
        Me.tbarang.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tbarang.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("kd_barang", "Kode", 7, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama_barang", "Nama"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("jmlstok_f1", "Qty1", 5, DevExpress.Utils.FormatType.Numeric, "n0", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("jmlstok_f2", "Qty2", 5, DevExpress.Utils.FormatType.Numeric, "n0", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("jmlstok_f3", "Qty3", 5, DevExpress.Utils.FormatType.Numeric, "n0", False, DevExpress.Utils.HorzAlignment.Far)})
        Me.tbarang.Properties.DisplayMember = "nama_barang"
        Me.tbarang.Properties.NullText = ""
        Me.tbarang.Properties.ValueMember = "kd_barang"
        Me.tbarang.Size = New System.Drawing.Size(190, 20)
        Me.tbarang.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(12, 40)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(68, 13)
        Me.LabelControl2.TabIndex = 170
        Me.LabelControl2.Text = "Jenis Barang :"
        '
        'tjenis
        '
        Me.tjenis.Location = New System.Drawing.Point(86, 37)
        Me.tjenis.Name = "tjenis"
        Me.tjenis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tjenis.Properties.Items.AddRange(New Object() {"Barang Masuk", "Barang Keluar"})
        Me.tjenis.Size = New System.Drawing.Size(251, 20)
        Me.tjenis.TabIndex = 2
        '
        'tsat
        '
        Me.tsat.Location = New System.Drawing.Point(86, 89)
        Me.tsat.Name = "tsat"
        Me.tsat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tsat.Size = New System.Drawing.Size(132, 20)
        Me.tsat.TabIndex = 5
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(39, 92)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(41, 13)
        Me.LabelControl4.TabIndex = 175
        Me.LabelControl4.Text = "Satuan :"
        '
        'tjml
        '
        Me.tjml.EditValue = "0"
        Me.tjml.Location = New System.Drawing.Point(86, 63)
        Me.tjml.Name = "tjml"
        Me.tjml.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tjml.Properties.Appearance.Options.UseFont = True
        Me.tjml.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tjml.Properties.Mask.EditMask = "n0"
        Me.tjml.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tjml.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tjml.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tjml.Size = New System.Drawing.Size(34, 20)
        Me.tjml.TabIndex = 3
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(58, 66)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(22, 13)
        Me.LabelControl3.TabIndex = 174
        Me.LabelControl3.Text = "Jml :"
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(262, 126)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 23)
        Me.btclose.TabIndex = 7
        Me.btclose.Text = "&Keluar"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(181, 126)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 23)
        Me.btsimpan.TabIndex = 6
        Me.btsimpan.Text = "&Simpan"
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(126, 66)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(52, 13)
        Me.LabelControl5.TabIndex = 176
        Me.LabelControl5.Text = "Jml Bocor :"
        '
        'tjml2
        '
        Me.tjml2.EditValue = "0"
        Me.tjml2.Location = New System.Drawing.Point(184, 63)
        Me.tjml2.Name = "tjml2"
        Me.tjml2.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tjml2.Properties.Appearance.Options.UseFont = True
        Me.tjml2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tjml2.Properties.Mask.EditMask = "n0"
        Me.tjml2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tjml2.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tjml2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tjml2.Size = New System.Drawing.Size(34, 20)
        Me.tjml2.TabIndex = 4
        '
        'tbarang0
        '
        Me.tbarang0.Location = New System.Drawing.Point(86, 12)
        Me.tbarang0.Name = "tbarang0"
        Me.tbarang0.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tbarang0.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("kd_barang", "Kode")})
        Me.tbarang0.Properties.DisplayMember = "kd_barang"
        Me.tbarang0.Properties.NullText = ""
        Me.tbarang0.Properties.ValueMember = "kd_barang"
        Me.tbarang0.Size = New System.Drawing.Size(58, 20)
        Me.tbarang0.TabIndex = 0
        '
        'fadm_gud3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(356, 158)
        Me.Controls.Add(Me.tbarang0)
        Me.Controls.Add(Me.tjml2)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.tsat)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.tjml)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.tjenis)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.tbarang)
        Me.Controls.Add(Me.LabelControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fadm_gud3"
        Me.Text = "In Out Gudang"
        CType(Me.tbarang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tjenis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tsat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tjml.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tjml2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbarang0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbarang As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tjenis As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents tsat As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tjml As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tjml2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tbarang0 As DevExpress.XtraEditors.LookUpEdit
End Class
