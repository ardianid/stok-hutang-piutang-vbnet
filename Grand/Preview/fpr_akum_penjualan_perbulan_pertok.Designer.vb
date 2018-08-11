<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fpr_akum_penjualan_perbulan_pertok
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
        Me.btload = New DevExpress.XtraEditors.SimpleButton()
        Me.tbln2 = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tbln1 = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.ttahun = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.bts_supir = New DevExpress.XtraEditors.SimpleButton()
        Me.tnama_tko = New DevExpress.XtraEditors.TextEdit()
        Me.tkode_tko = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.talamat_tko = New DevExpress.XtraEditors.MemoEdit()
        CType(Me.tbln2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbln1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttahun.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnama_tko.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tkode_tko.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.talamat_tko.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(342, 214)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(60, 26)
        Me.btclose.TabIndex = 6
        Me.btclose.Text = "&Keluar"
        '
        'btload
        '
        Me.btload.Location = New System.Drawing.Point(276, 214)
        Me.btload.Name = "btload"
        Me.btload.Size = New System.Drawing.Size(60, 26)
        Me.btload.TabIndex = 5
        Me.btload.Text = "&Load"
        '
        'tbln2
        '
        Me.tbln2.Location = New System.Drawing.Point(101, 64)
        Me.tbln2.Name = "tbln2"
        Me.tbln2.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.tbln2.Properties.Mask.EditMask = "f0"
        Me.tbln2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tbln2.Size = New System.Drawing.Size(67, 20)
        Me.tbln2.TabIndex = 2
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(25, 67)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(70, 13)
        Me.LabelControl3.TabIndex = 12
        Me.LabelControl3.Text = "Sampai Bulan :"
        '
        'tbln1
        '
        Me.tbln1.Location = New System.Drawing.Point(101, 38)
        Me.tbln1.Name = "tbln1"
        Me.tbln1.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.tbln1.Properties.Mask.EditMask = "f0"
        Me.tbln1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tbln1.Size = New System.Drawing.Size(67, 20)
        Me.tbln1.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(40, 41)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(55, 13)
        Me.LabelControl2.TabIndex = 9
        Me.LabelControl2.Text = "Dari Bulan :"
        '
        'ttahun
        '
        Me.ttahun.Location = New System.Drawing.Point(101, 12)
        Me.ttahun.Name = "ttahun"
        Me.ttahun.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.ttahun.Properties.Mask.EditMask = "f0"
        Me.ttahun.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.ttahun.Size = New System.Drawing.Size(67, 20)
        Me.ttahun.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(58, 15)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(37, 13)
        Me.LabelControl1.TabIndex = 6
        Me.LabelControl1.Text = "Tahun :"
        '
        'bts_supir
        '
        Me.bts_supir.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bts_supir.Appearance.Options.UseFont = True
        Me.bts_supir.Image = Global.Grand.My.Resources.Resources._1389260518_xmag
        Me.bts_supir.Location = New System.Drawing.Point(377, 89)
        Me.bts_supir.Name = "bts_supir"
        Me.bts_supir.Size = New System.Drawing.Size(25, 21)
        Me.bts_supir.TabIndex = 4
        '
        'tnama_tko
        '
        Me.tnama_tko.Enabled = False
        Me.tnama_tko.Location = New System.Drawing.Point(153, 90)
        Me.tnama_tko.Name = "tnama_tko"
        Me.tnama_tko.Size = New System.Drawing.Size(218, 20)
        Me.tnama_tko.TabIndex = 23
        '
        'tkode_tko
        '
        Me.tkode_tko.Location = New System.Drawing.Point(101, 90)
        Me.tkode_tko.Name = "tkode_tko"
        Me.tkode_tko.Size = New System.Drawing.Size(48, 20)
        Me.tkode_tko.TabIndex = 3
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(31, 93)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(64, 13)
        Me.LabelControl4.TabIndex = 22
        Me.LabelControl4.Text = "Toko/Outlet :"
        '
        'talamat_tko
        '
        Me.talamat_tko.Enabled = False
        Me.talamat_tko.Location = New System.Drawing.Point(101, 116)
        Me.talamat_tko.Name = "talamat_tko"
        Me.talamat_tko.Size = New System.Drawing.Size(270, 51)
        Me.talamat_tko.TabIndex = 21
        '
        'fpr_akum_penjualan_perbulan_pertok
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(419, 251)
        Me.Controls.Add(Me.bts_supir)
        Me.Controls.Add(Me.tnama_tko)
        Me.Controls.Add(Me.tkode_tko)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.talamat_tko)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btload)
        Me.Controls.Add(Me.tbln2)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.tbln1)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.ttahun)
        Me.Controls.Add(Me.LabelControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fpr_akum_penjualan_perbulan_pertok"
        Me.Text = "Akumulasi Penj Per-Bulan Per-Toko"
        CType(Me.tbln2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbln1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttahun.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnama_tko.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tkode_tko.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.talamat_tko.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tbln2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbln1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttahun As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents bts_supir As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tnama_tko As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tkode_tko As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents talamat_tko As DevExpress.XtraEditors.MemoEdit
End Class
