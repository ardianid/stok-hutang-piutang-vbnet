<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fbayar_psw3
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
        Me.tjml_byr = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.tjml = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.bts_supir = New DevExpress.XtraEditors.SimpleButton()
        Me.ttgl_sewa = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tnosewa = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.tjml_byr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tjml.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl_sewa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnosewa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(193, 157)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 25)
        Me.btclose.TabIndex = 26
        Me.btclose.Text = "&Keluar"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(112, 157)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 25)
        Me.btsimpan.TabIndex = 25
        Me.btsimpan.Text = "&Simpan"
        '
        'tjml_byr
        '
        Me.tjml_byr.EditValue = "0"
        Me.tjml_byr.Location = New System.Drawing.Point(90, 90)
        Me.tjml_byr.Name = "tjml_byr"
        Me.tjml_byr.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tjml_byr.Properties.Appearance.Options.UseFont = True
        Me.tjml_byr.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tjml_byr.Properties.Mask.EditMask = "n0"
        Me.tjml_byr.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tjml_byr.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tjml_byr.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tjml_byr.Size = New System.Drawing.Size(100, 20)
        Me.tjml_byr.TabIndex = 24
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(13, 93)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(71, 13)
        Me.LabelControl4.TabIndex = 23
        Me.LabelControl4.Text = "Jumlah Bayar :"
        '
        'tjml
        '
        Me.tjml.EditValue = "0"
        Me.tjml.Location = New System.Drawing.Point(90, 64)
        Me.tjml.Name = "tjml"
        Me.tjml.Properties.AllowFocused = False
        Me.tjml.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tjml.Properties.Appearance.Options.UseFont = True
        Me.tjml.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tjml.Properties.Mask.EditMask = "n0"
        Me.tjml.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tjml.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tjml.Properties.ReadOnly = True
        Me.tjml.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tjml.Size = New System.Drawing.Size(100, 20)
        Me.tjml.TabIndex = 22
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(44, 67)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(40, 13)
        Me.LabelControl3.TabIndex = 21
        Me.LabelControl3.Text = "Jumlah :"
        '
        'bts_supir
        '
        Me.bts_supir.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bts_supir.Appearance.Options.UseFont = True
        Me.bts_supir.Image = Global.Grand.My.Resources.Resources._1389260518_xmag
        Me.bts_supir.Location = New System.Drawing.Point(243, 11)
        Me.bts_supir.Name = "bts_supir"
        Me.bts_supir.Size = New System.Drawing.Size(25, 21)
        Me.bts_supir.TabIndex = 20
        '
        'ttgl_sewa
        '
        Me.ttgl_sewa.Enabled = False
        Me.ttgl_sewa.Location = New System.Drawing.Point(90, 38)
        Me.ttgl_sewa.Name = "ttgl_sewa"
        Me.ttgl_sewa.Properties.AllowFocused = False
        Me.ttgl_sewa.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.ttgl_sewa.Properties.ReadOnly = True
        Me.ttgl_sewa.Size = New System.Drawing.Size(148, 20)
        Me.ttgl_sewa.TabIndex = 19
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(34, 41)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(50, 13)
        Me.LabelControl2.TabIndex = 18
        Me.LabelControl2.Text = "Tgl Sewa :"
        '
        'tnosewa
        '
        Me.tnosewa.Location = New System.Drawing.Point(90, 12)
        Me.tnosewa.Name = "tnosewa"
        Me.tnosewa.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tnosewa.Size = New System.Drawing.Size(148, 20)
        Me.tnosewa.TabIndex = 17
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(9, 15)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(75, 13)
        Me.LabelControl1.TabIndex = 16
        Me.LabelControl1.Text = "No Bukti Sewa :"
        '
        'fbayar_psw3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(283, 192)
        Me.ControlBox = False
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.tjml_byr)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.tjml)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.bts_supir)
        Me.Controls.Add(Me.ttgl_sewa)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.tnosewa)
        Me.Controls.Add(Me.LabelControl1)
        Me.Name = "fbayar_psw3"
        Me.Text = "Pembayaran Perpanjangan Sewa"
        CType(Me.tjml_byr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tjml.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl_sewa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnosewa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tjml_byr As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tjml As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents bts_supir As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ttgl_sewa As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tnosewa As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
