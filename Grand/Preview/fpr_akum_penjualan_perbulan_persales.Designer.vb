<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fpr_akum_penjualan_perbulan_persales
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
        Me.tbln2 = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tbln1 = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.ttahun = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.bts_sal = New DevExpress.XtraEditors.SimpleButton()
        Me.tnama_sales = New DevExpress.XtraEditors.TextEdit()
        Me.tkd_sales = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btload = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.tbln2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbln1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttahun.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnama_sales.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tkd_sales.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbln2
        '
        Me.tbln2.Location = New System.Drawing.Point(101, 74)
        Me.tbln2.Name = "tbln2"
        Me.tbln2.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.tbln2.Properties.Mask.EditMask = "f0"
        Me.tbln2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tbln2.Size = New System.Drawing.Size(67, 20)
        Me.tbln2.TabIndex = 15
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(25, 77)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(70, 13)
        Me.LabelControl3.TabIndex = 18
        Me.LabelControl3.Text = "Sampai Bulan :"
        '
        'tbln1
        '
        Me.tbln1.Location = New System.Drawing.Point(101, 48)
        Me.tbln1.Name = "tbln1"
        Me.tbln1.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.tbln1.Properties.Mask.EditMask = "f0"
        Me.tbln1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tbln1.Size = New System.Drawing.Size(67, 20)
        Me.tbln1.TabIndex = 14
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(40, 51)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(55, 13)
        Me.LabelControl2.TabIndex = 17
        Me.LabelControl2.Text = "Dari Bulan :"
        '
        'ttahun
        '
        Me.ttahun.Location = New System.Drawing.Point(101, 22)
        Me.ttahun.Name = "ttahun"
        Me.ttahun.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.ttahun.Properties.Mask.EditMask = "f0"
        Me.ttahun.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.ttahun.Size = New System.Drawing.Size(67, 20)
        Me.ttahun.TabIndex = 13
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(58, 25)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(37, 13)
        Me.LabelControl1.TabIndex = 16
        Me.LabelControl1.Text = "Tahun :"
        '
        'bts_sal
        '
        Me.bts_sal.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bts_sal.Appearance.Options.UseFont = True
        Me.bts_sal.Image = Global.Grand.My.Resources.Resources._1389260518_xmag
        Me.bts_sal.Location = New System.Drawing.Point(377, 99)
        Me.bts_sal.Name = "bts_sal"
        Me.bts_sal.Size = New System.Drawing.Size(25, 21)
        Me.bts_sal.TabIndex = 208
        '
        'tnama_sales
        '
        Me.tnama_sales.Enabled = False
        Me.tnama_sales.Location = New System.Drawing.Point(153, 100)
        Me.tnama_sales.Name = "tnama_sales"
        Me.tnama_sales.Size = New System.Drawing.Size(218, 20)
        Me.tnama_sales.TabIndex = 209
        '
        'tkd_sales
        '
        Me.tkd_sales.Location = New System.Drawing.Point(101, 100)
        Me.tkd_sales.Name = "tkd_sales"
        Me.tkd_sales.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tkd_sales.Size = New System.Drawing.Size(48, 20)
        Me.tkd_sales.TabIndex = 207
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(63, 103)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(32, 13)
        Me.LabelControl5.TabIndex = 210
        Me.LabelControl5.Text = "Sales :"
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(342, 165)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(60, 26)
        Me.btclose.TabIndex = 212
        Me.btclose.Text = "&Keluar"
        '
        'btload
        '
        Me.btload.Location = New System.Drawing.Point(276, 165)
        Me.btload.Name = "btload"
        Me.btload.Size = New System.Drawing.Size(60, 26)
        Me.btload.TabIndex = 211
        Me.btload.Text = "&Load"
        '
        'fpr_akum_penjualan_perbulan_persales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(431, 212)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btload)
        Me.Controls.Add(Me.bts_sal)
        Me.Controls.Add(Me.tnama_sales)
        Me.Controls.Add(Me.tkd_sales)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.tbln2)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.tbln1)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.ttahun)
        Me.Controls.Add(Me.LabelControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fpr_akum_penjualan_perbulan_persales"
        Me.Text = "Akumulasi Penj Per-Bulan Per-Sales"
        CType(Me.tbln2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbln1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttahun.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnama_sales.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tkd_sales.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbln2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbln1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttahun As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents bts_sal As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tnama_sales As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tkd_sales As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btload As DevExpress.XtraEditors.SimpleButton
End Class
