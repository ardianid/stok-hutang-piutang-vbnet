<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fgiro2
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
        Me.tnogiro = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tbank = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.bts_toko = New DevExpress.XtraEditors.SimpleButton()
        Me.tnama_toko = New DevExpress.XtraEditors.TextEdit()
        Me.tkd_toko = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.bts_sal = New DevExpress.XtraEditors.SimpleButton()
        Me.tnama_sales = New DevExpress.XtraEditors.TextEdit()
        Me.tkd_sales = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.talamat_toko = New DevExpress.XtraEditors.MemoEdit()
        Me.tjumlah = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.tnote = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl_jt = New DevExpress.XtraEditors.DateEdit()
        CType(Me.tnogiro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbank.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnama_toko.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tkd_toko.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnama_sales.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tkd_sales.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.talamat_toko.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tjumlah.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl_jt.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl_jt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tnogiro
        '
        Me.tnogiro.Location = New System.Drawing.Point(83, 12)
        Me.tnogiro.Name = "tnogiro"
        Me.tnogiro.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tnogiro.Size = New System.Drawing.Size(158, 20)
        Me.tnogiro.TabIndex = 0
        '
        'LabelControl9
        '
        Me.LabelControl9.Location = New System.Drawing.Point(35, 15)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Size = New System.Drawing.Size(42, 13)
        Me.LabelControl9.TabIndex = 180
        Me.LabelControl9.Text = "No Giro :"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(34, 41)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(43, 13)
        Me.LabelControl4.TabIndex = 182
        Me.LabelControl4.Text = "Tgl Giro :"
        '
        'ttgl
        '
        Me.ttgl.EditValue = Nothing
        Me.ttgl.Location = New System.Drawing.Point(83, 38)
        Me.ttgl.Name = "ttgl"
        Me.ttgl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl.Properties.Mask.EditMask = ""
        Me.ttgl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl.Size = New System.Drawing.Size(97, 20)
        Me.ttgl.TabIndex = 1
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(47, 93)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(30, 13)
        Me.LabelControl1.TabIndex = 183
        Me.LabelControl1.Text = "Bank :"
        '
        'tbank
        '
        Me.tbank.Location = New System.Drawing.Point(83, 90)
        Me.tbank.Name = "tbank"
        Me.tbank.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tbank.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbank.Size = New System.Drawing.Size(158, 20)
        Me.tbank.TabIndex = 3
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(37, 171)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(40, 13)
        Me.LabelControl3.TabIndex = 193
        Me.LabelControl3.Text = "Alamat :"
        '
        'bts_toko
        '
        Me.bts_toko.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bts_toko.Appearance.Options.UseFont = True
        Me.bts_toko.Image = Global.Grand.My.Resources.Resources._1389260518_xmag
        Me.bts_toko.Location = New System.Drawing.Point(314, 142)
        Me.bts_toko.Name = "bts_toko"
        Me.bts_toko.Size = New System.Drawing.Size(25, 21)
        Me.bts_toko.TabIndex = 200
        '
        'tnama_toko
        '
        Me.tnama_toko.Enabled = False
        Me.tnama_toko.Location = New System.Drawing.Point(129, 142)
        Me.tnama_toko.Name = "tnama_toko"
        Me.tnama_toko.Size = New System.Drawing.Size(179, 20)
        Me.tnama_toko.TabIndex = 190
        '
        'tkd_toko
        '
        Me.tkd_toko.Location = New System.Drawing.Point(83, 142)
        Me.tkd_toko.Name = "tkd_toko"
        Me.tkd_toko.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tkd_toko.Size = New System.Drawing.Size(44, 20)
        Me.tkd_toko.TabIndex = 5
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(40, 145)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(37, 13)
        Me.LabelControl2.TabIndex = 188
        Me.LabelControl2.Text = "Outlet :"
        '
        'bts_sal
        '
        Me.bts_sal.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bts_sal.Appearance.Options.UseFont = True
        Me.bts_sal.Image = Global.Grand.My.Resources.Resources._1389260518_xmag
        Me.bts_sal.Location = New System.Drawing.Point(314, 115)
        Me.bts_sal.Name = "bts_sal"
        Me.bts_sal.Size = New System.Drawing.Size(25, 21)
        Me.bts_sal.TabIndex = 100
        '
        'tnama_sales
        '
        Me.tnama_sales.Enabled = False
        Me.tnama_sales.Location = New System.Drawing.Point(129, 116)
        Me.tnama_sales.Name = "tnama_sales"
        Me.tnama_sales.Size = New System.Drawing.Size(179, 20)
        Me.tnama_sales.TabIndex = 186
        '
        'tkd_sales
        '
        Me.tkd_sales.Location = New System.Drawing.Point(83, 116)
        Me.tkd_sales.Name = "tkd_sales"
        Me.tkd_sales.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tkd_sales.Size = New System.Drawing.Size(44, 20)
        Me.tkd_sales.TabIndex = 4
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(45, 119)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(32, 13)
        Me.LabelControl5.TabIndex = 194
        Me.LabelControl5.Text = "Sales :"
        '
        'talamat_toko
        '
        Me.talamat_toko.Enabled = False
        Me.talamat_toko.Location = New System.Drawing.Point(83, 166)
        Me.talamat_toko.Name = "talamat_toko"
        Me.talamat_toko.Size = New System.Drawing.Size(260, 39)
        Me.talamat_toko.TabIndex = 192
        '
        'tjumlah
        '
        Me.tjumlah.EditValue = "0"
        Me.tjumlah.Location = New System.Drawing.Point(83, 211)
        Me.tjumlah.Name = "tjumlah"
        Me.tjumlah.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tjumlah.Properties.Appearance.Options.UseFont = True
        Me.tjumlah.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tjumlah.Properties.Mask.EditMask = "n0"
        Me.tjumlah.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tjumlah.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tjumlah.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tjumlah.Size = New System.Drawing.Size(158, 20)
        Me.tjumlah.TabIndex = 6
        '
        'LabelControl6
        '
        Me.LabelControl6.Location = New System.Drawing.Point(37, 214)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(40, 13)
        Me.LabelControl6.TabIndex = 196
        Me.LabelControl6.Text = "Jumlah :"
        '
        'tnote
        '
        Me.tnote.Location = New System.Drawing.Point(83, 237)
        Me.tnote.Name = "tnote"
        Me.tnote.Size = New System.Drawing.Size(260, 20)
        Me.tnote.TabIndex = 7
        '
        'LabelControl7
        '
        Me.LabelControl7.Location = New System.Drawing.Point(54, 240)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(23, 13)
        Me.LabelControl7.TabIndex = 198
        Me.LabelControl7.Text = "Ket :"
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(268, 282)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 25)
        Me.btclose.TabIndex = 9
        Me.btclose.Text = "&Keluar"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(187, 282)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 25)
        Me.btsimpan.TabIndex = 8
        Me.btsimpan.Text = "&Simpan"
        '
        'LabelControl8
        '
        Me.LabelControl8.Location = New System.Drawing.Point(42, 67)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(35, 13)
        Me.LabelControl8.TabIndex = 199
        Me.LabelControl8.Text = "Tgl JT :"
        '
        'ttgl_jt
        '
        Me.ttgl_jt.EditValue = Nothing
        Me.ttgl_jt.Location = New System.Drawing.Point(83, 64)
        Me.ttgl_jt.Name = "ttgl_jt"
        Me.ttgl_jt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl_jt.Properties.Mask.EditMask = ""
        Me.ttgl_jt.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl_jt.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl_jt.Size = New System.Drawing.Size(97, 20)
        Me.ttgl_jt.TabIndex = 2
        '
        'fgiro2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(381, 322)
        Me.ControlBox = False
        Me.Controls.Add(Me.ttgl_jt)
        Me.Controls.Add(Me.LabelControl8)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.LabelControl7)
        Me.Controls.Add(Me.tnote)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.tjumlah)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.bts_toko)
        Me.Controls.Add(Me.tnama_toko)
        Me.Controls.Add(Me.tkd_toko)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.bts_sal)
        Me.Controls.Add(Me.tnama_sales)
        Me.Controls.Add(Me.tkd_sales)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.talamat_toko)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.ttgl)
        Me.Controls.Add(Me.tnogiro)
        Me.Controls.Add(Me.LabelControl9)
        Me.Controls.Add(Me.tbank)
        Me.Name = "fgiro2"
        Me.Text = "Data Giro"
        CType(Me.tnogiro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbank.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnama_toko.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tkd_toko.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnama_sales.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tkd_sales.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.talamat_toko.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tjumlah.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl_jt.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl_jt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tnogiro As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbank As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents bts_toko As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tnama_toko As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tkd_toko As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents bts_sal As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tnama_sales As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tkd_sales As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents talamat_toko As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents tjumlah As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tnote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl_jt As DevExpress.XtraEditors.DateEdit
End Class
