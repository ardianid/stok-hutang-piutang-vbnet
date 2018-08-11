<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ffaktur_to5
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
        Me.tharga = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.tjumlah = New DevExpress.XtraEditors.TextEdit()
        Me.tsatuan = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tjml = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tnama = New DevExpress.XtraEditors.TextEdit()
        Me.tkode = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.tharga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tjumlah.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tsatuan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tjml.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tkode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(310, 173)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 25)
        Me.btclose.TabIndex = 171
        Me.btclose.Text = "&Keluar"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(229, 173)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 25)
        Me.btsimpan.TabIndex = 169
        Me.btsimpan.Text = "&Simpan"
        '
        'tharga
        '
        Me.tharga.EditValue = "0"
        Me.tharga.Location = New System.Drawing.Point(71, 90)
        Me.tharga.Name = "tharga"
        Me.tharga.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tharga.Properties.Appearance.Options.UseFont = True
        Me.tharga.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tharga.Properties.Mask.EditMask = "n0"
        Me.tharga.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tharga.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tharga.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tharga.Size = New System.Drawing.Size(112, 20)
        Me.tharga.TabIndex = 167
        '
        'LabelControl8
        '
        Me.LabelControl8.Location = New System.Drawing.Point(28, 93)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(36, 13)
        Me.LabelControl8.TabIndex = 177
        Me.LabelControl8.Text = "Harga :"
        '
        'LabelControl6
        '
        Me.LabelControl6.Location = New System.Drawing.Point(24, 119)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(40, 13)
        Me.LabelControl6.TabIndex = 176
        Me.LabelControl6.Text = "Jumlah :"
        '
        'tjumlah
        '
        Me.tjumlah.EditValue = "0"
        Me.tjumlah.Location = New System.Drawing.Point(71, 116)
        Me.tjumlah.Name = "tjumlah"
        Me.tjumlah.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tjumlah.Properties.Appearance.Options.UseFont = True
        Me.tjumlah.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tjumlah.Properties.Mask.EditMask = "n0"
        Me.tjumlah.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tjumlah.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tjumlah.Properties.ReadOnly = True
        Me.tjumlah.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tjumlah.Size = New System.Drawing.Size(112, 20)
        Me.tjumlah.TabIndex = 175
        '
        'tsatuan
        '
        Me.tsatuan.Enabled = False
        Me.tsatuan.Location = New System.Drawing.Point(70, 64)
        Me.tsatuan.Name = "tsatuan"
        Me.tsatuan.Size = New System.Drawing.Size(113, 20)
        Me.tsatuan.TabIndex = 174
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(23, 67)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(41, 13)
        Me.LabelControl2.TabIndex = 173
        Me.LabelControl2.Text = "Satuan :"
        '
        'tjml
        '
        Me.tjml.EditValue = "0"
        Me.tjml.Location = New System.Drawing.Point(70, 38)
        Me.tjml.Name = "tjml"
        Me.tjml.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tjml.Properties.Appearance.Options.UseFont = True
        Me.tjml.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tjml.Properties.Mask.EditMask = "n0"
        Me.tjml.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tjml.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tjml.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tjml.Size = New System.Drawing.Size(43, 20)
        Me.tjml.TabIndex = 165
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(42, 41)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(22, 13)
        Me.LabelControl3.TabIndex = 172
        Me.LabelControl3.Text = "Jml :"
        '
        'tnama
        '
        Me.tnama.Enabled = False
        Me.tnama.Location = New System.Drawing.Point(131, 12)
        Me.tnama.Name = "tnama"
        Me.tnama.Size = New System.Drawing.Size(254, 20)
        Me.tnama.TabIndex = 170
        '
        'tkode
        '
        Me.tkode.Enabled = False
        Me.tkode.Location = New System.Drawing.Point(70, 12)
        Me.tkode.Name = "tkode"
        Me.tkode.Size = New System.Drawing.Size(57, 20)
        Me.tkode.TabIndex = 168
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(23, 15)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(41, 13)
        Me.LabelControl1.TabIndex = 166
        Me.LabelControl1.Text = "Barang :"
        '
        'ffaktur_to5
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(399, 208)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.tharga)
        Me.Controls.Add(Me.LabelControl8)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.tjumlah)
        Me.Controls.Add(Me.tsatuan)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.tjml)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.tnama)
        Me.Controls.Add(Me.tkode)
        Me.Controls.Add(Me.LabelControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ffaktur_to5"
        Me.Text = "Faktur TO (Barang Kembali)"
        CType(Me.tharga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tjumlah.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tsatuan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tjml.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tkode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tharga As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tjumlah As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tsatuan As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tjml As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tnama As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tkode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
