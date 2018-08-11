<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fpr_kirimsupir_bli
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
        Me.ttipe = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btload = New DevExpress.XtraEditors.SimpleButton()
        Me.tnama_supir = New DevExpress.XtraEditors.TextEdit()
        Me.tkd_supir = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl2 = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.tmin_a = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.tnilai_a = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.tmin_b = New DevExpress.XtraEditors.TextEdit()
        Me.tnilai_b = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.bts_supir = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ttipe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnama_supir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tkd_supir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tmin_a.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnilai_a.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.tmin_b.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnilai_b.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ttipe
        '
        Me.ttipe.Location = New System.Drawing.Point(79, 74)
        Me.ttipe.Name = "ttipe"
        Me.ttipe.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttipe.Properties.Items.AddRange(New Object() {"Detail", "Tanda Terima"})
        Me.ttipe.Size = New System.Drawing.Size(270, 20)
        Me.ttipe.TabIndex = 3
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(26, 77)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(47, 13)
        Me.LabelControl3.TabIndex = 195
        Me.LabelControl3.Text = "Tipe Lap :"
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(305, 144)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 28)
        Me.btclose.TabIndex = 5
        Me.btclose.Text = "&Keluar"
        '
        'btload
        '
        Me.btload.Location = New System.Drawing.Point(305, 110)
        Me.btload.Name = "btload"
        Me.btload.Size = New System.Drawing.Size(75, 28)
        Me.btload.TabIndex = 4
        Me.btload.Text = "&Load"
        '
        'tnama_supir
        '
        Me.tnama_supir.Enabled = False
        Me.tnama_supir.Location = New System.Drawing.Point(125, 48)
        Me.tnama_supir.Name = "tnama_supir"
        Me.tnama_supir.Size = New System.Drawing.Size(224, 20)
        Me.tnama_supir.TabIndex = 193
        '
        'tkd_supir
        '
        Me.tkd_supir.Location = New System.Drawing.Point(79, 48)
        Me.tkd_supir.Name = "tkd_supir"
        Me.tkd_supir.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tkd_supir.Size = New System.Drawing.Size(44, 20)
        Me.tkd_supir.TabIndex = 2
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(42, 51)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(31, 13)
        Me.LabelControl5.TabIndex = 194
        Me.LabelControl5.Text = "Supir :"
        '
        'ttgl2
        '
        Me.ttgl2.EditValue = Nothing
        Me.ttgl2.Location = New System.Drawing.Point(204, 22)
        Me.ttgl2.Name = "ttgl2"
        Me.ttgl2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl2.Properties.Mask.EditMask = ""
        Me.ttgl2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl2.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl2.Size = New System.Drawing.Size(96, 20)
        Me.ttgl2.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(181, 25)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(17, 13)
        Me.LabelControl2.TabIndex = 191
        Me.LabelControl2.Text = "S.D"
        '
        'ttgl
        '
        Me.ttgl.EditValue = Nothing
        Me.ttgl.Location = New System.Drawing.Point(79, 22)
        Me.ttgl.Name = "ttgl"
        Me.ttgl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl.Properties.Mask.EditMask = ""
        Me.ttgl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl.Size = New System.Drawing.Size(96, 20)
        Me.ttgl.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(28, 25)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(45, 13)
        Me.LabelControl1.TabIndex = 190
        Me.LabelControl1.Text = "Tanggal :"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(42, 13)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(30, 13)
        Me.LabelControl4.TabIndex = 191
        Me.LabelControl4.Text = "Rit A :"
        '
        'tmin_a
        '
        Me.tmin_a.EditValue = "0"
        Me.tmin_a.Location = New System.Drawing.Point(100, 10)
        Me.tmin_a.Name = "tmin_a"
        Me.tmin_a.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tmin_a.Properties.Appearance.Options.UseFont = True
        Me.tmin_a.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tmin_a.Properties.Mask.EditMask = "n0"
        Me.tmin_a.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tmin_a.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tmin_a.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tmin_a.Size = New System.Drawing.Size(32, 20)
        Me.tmin_a.TabIndex = 192
        '
        'LabelControl6
        '
        Me.LabelControl6.Location = New System.Drawing.Point(78, 13)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(16, 13)
        Me.LabelControl6.TabIndex = 1
        Me.LabelControl6.Text = "<="
        '
        'LabelControl7
        '
        Me.LabelControl7.Location = New System.Drawing.Point(138, 13)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(49, 13)
        Me.LabelControl7.TabIndex = 194
        Me.LabelControl7.Text = "Nilai : Rp. "
        '
        'tnilai_a
        '
        Me.tnilai_a.EditValue = "0"
        Me.tnilai_a.Location = New System.Drawing.Point(193, 10)
        Me.tnilai_a.Name = "tnilai_a"
        Me.tnilai_a.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tnilai_a.Properties.Appearance.Options.UseFont = True
        Me.tnilai_a.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tnilai_a.Properties.Mask.EditMask = "n0"
        Me.tnilai_a.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tnilai_a.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tnilai_a.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tnilai_a.Size = New System.Drawing.Size(70, 20)
        Me.tnilai_a.TabIndex = 2
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.CaptionLocation = DevExpress.Utils.Locations.Bottom
        Me.GroupControl1.Controls.Add(Me.tmin_b)
        Me.GroupControl1.Controls.Add(Me.tnilai_b)
        Me.GroupControl1.Controls.Add(Me.LabelControl8)
        Me.GroupControl1.Controls.Add(Me.LabelControl9)
        Me.GroupControl1.Controls.Add(Me.LabelControl10)
        Me.GroupControl1.Controls.Add(Me.tmin_a)
        Me.GroupControl1.Controls.Add(Me.tnilai_a)
        Me.GroupControl1.Controls.Add(Me.LabelControl4)
        Me.GroupControl1.Controls.Add(Me.LabelControl7)
        Me.GroupControl1.Controls.Add(Me.LabelControl6)
        Me.GroupControl1.Location = New System.Drawing.Point(1, 105)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(281, 83)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Option"
        '
        'tmin_b
        '
        Me.tmin_b.EditValue = "0"
        Me.tmin_b.Location = New System.Drawing.Point(100, 36)
        Me.tmin_b.Name = "tmin_b"
        Me.tmin_b.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tmin_b.Properties.Appearance.Options.UseFont = True
        Me.tmin_b.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tmin_b.Properties.Mask.EditMask = "n0"
        Me.tmin_b.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tmin_b.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tmin_b.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tmin_b.Size = New System.Drawing.Size(32, 20)
        Me.tmin_b.TabIndex = 197
        '
        'tnilai_b
        '
        Me.tnilai_b.EditValue = "0"
        Me.tnilai_b.Location = New System.Drawing.Point(193, 36)
        Me.tnilai_b.Name = "tnilai_b"
        Me.tnilai_b.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tnilai_b.Properties.Appearance.Options.UseFont = True
        Me.tnilai_b.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tnilai_b.Properties.Mask.EditMask = "n0"
        Me.tnilai_b.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tnilai_b.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tnilai_b.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tnilai_b.Size = New System.Drawing.Size(70, 20)
        Me.tnilai_b.TabIndex = 4
        '
        'LabelControl8
        '
        Me.LabelControl8.Location = New System.Drawing.Point(42, 39)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(29, 13)
        Me.LabelControl8.TabIndex = 196
        Me.LabelControl8.Text = "Rit B :"
        '
        'LabelControl9
        '
        Me.LabelControl9.Location = New System.Drawing.Point(138, 39)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Size = New System.Drawing.Size(49, 13)
        Me.LabelControl9.TabIndex = 199
        Me.LabelControl9.Text = "Nilai : Rp. "
        '
        'LabelControl10
        '
        Me.LabelControl10.Location = New System.Drawing.Point(78, 39)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Size = New System.Drawing.Size(8, 13)
        Me.LabelControl10.TabIndex = 3
        Me.LabelControl10.Text = ">"
        '
        'bts_supir
        '
        Me.bts_supir.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bts_supir.Appearance.Options.UseFont = True
        Me.bts_supir.Image = Global.Grand.My.Resources.Resources._1389260518_xmag
        Me.bts_supir.Location = New System.Drawing.Point(355, 47)
        Me.bts_supir.Name = "bts_supir"
        Me.bts_supir.Size = New System.Drawing.Size(25, 21)
        Me.bts_supir.TabIndex = 192
        '
        'fpr_kirimsupir_bli
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(402, 189)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.ttipe)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btload)
        Me.Controls.Add(Me.bts_supir)
        Me.Controls.Add(Me.tnama_supir)
        Me.Controls.Add(Me.tkd_supir)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.ttgl2)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.ttgl)
        Me.Controls.Add(Me.LabelControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fpr_kirimsupir_bli"
        Me.Text = "Insentif Supir (Beli)"
        CType(Me.ttipe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnama_supir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tkd_supir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tmin_a.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnilai_a.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.tmin_b.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnilai_b.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ttipe As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents bts_supir As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tnama_supir As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tkd_supir As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl2 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tmin_a As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tnilai_a As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents tmin_b As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tnilai_b As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
End Class
