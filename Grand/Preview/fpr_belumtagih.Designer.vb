<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fpr_belumtagih
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
        Me.bts_sal = New DevExpress.XtraEditors.SimpleButton()
        Me.tnama_sales = New DevExpress.XtraEditors.TextEdit()
        Me.tkd_sales = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.bts_tko = New DevExpress.XtraEditors.SimpleButton()
        Me.tnama_toko = New DevExpress.XtraEditors.TextEdit()
        Me.tkd_toko = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.tnama_sales.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tkd_sales.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnama_toko.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tkd_toko.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(308, 88)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 26)
        Me.btclose.TabIndex = 212
        Me.btclose.Text = "&Keluar"
        '
        'btload
        '
        Me.btload.Location = New System.Drawing.Point(227, 88)
        Me.btload.Name = "btload"
        Me.btload.Size = New System.Drawing.Size(75, 26)
        Me.btload.TabIndex = 211
        Me.btload.Text = "&Load"
        '
        'bts_sal
        '
        Me.bts_sal.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bts_sal.Appearance.Options.UseFont = True
        Me.bts_sal.Image = Global.Grand.My.Resources.Resources._1389260518_xmag
        Me.bts_sal.Location = New System.Drawing.Point(358, 48)
        Me.bts_sal.Name = "bts_sal"
        Me.bts_sal.Size = New System.Drawing.Size(25, 21)
        Me.bts_sal.TabIndex = 210
        '
        'tnama_sales
        '
        Me.tnama_sales.Enabled = False
        Me.tnama_sales.Location = New System.Drawing.Point(134, 49)
        Me.tnama_sales.Name = "tnama_sales"
        Me.tnama_sales.Size = New System.Drawing.Size(218, 20)
        Me.tnama_sales.TabIndex = 215
        '
        'tkd_sales
        '
        Me.tkd_sales.Location = New System.Drawing.Point(82, 49)
        Me.tkd_sales.Name = "tkd_sales"
        Me.tkd_sales.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tkd_sales.Size = New System.Drawing.Size(48, 20)
        Me.tkd_sales.TabIndex = 209
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(44, 52)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(32, 13)
        Me.LabelControl5.TabIndex = 216
        Me.LabelControl5.Text = "Sales :"
        '
        'bts_tko
        '
        Me.bts_tko.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bts_tko.Appearance.Options.UseFont = True
        Me.bts_tko.Image = Global.Grand.My.Resources.Resources._1389260518_xmag
        Me.bts_tko.Location = New System.Drawing.Point(358, 22)
        Me.bts_tko.Name = "bts_tko"
        Me.bts_tko.Size = New System.Drawing.Size(25, 21)
        Me.bts_tko.TabIndex = 208
        '
        'tnama_toko
        '
        Me.tnama_toko.Enabled = False
        Me.tnama_toko.Location = New System.Drawing.Point(134, 23)
        Me.tnama_toko.Name = "tnama_toko"
        Me.tnama_toko.Size = New System.Drawing.Size(218, 20)
        Me.tnama_toko.TabIndex = 214
        '
        'tkd_toko
        '
        Me.tkd_toko.Location = New System.Drawing.Point(82, 23)
        Me.tkd_toko.Name = "tkd_toko"
        Me.tkd_toko.Size = New System.Drawing.Size(48, 20)
        Me.tkd_toko.TabIndex = 207
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(12, 26)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(64, 13)
        Me.LabelControl4.TabIndex = 213
        Me.LabelControl4.Text = "Toko/Outlet :"
        '
        'fpr_belumtagih
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(408, 126)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btload)
        Me.Controls.Add(Me.bts_sal)
        Me.Controls.Add(Me.tnama_sales)
        Me.Controls.Add(Me.tkd_sales)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.bts_tko)
        Me.Controls.Add(Me.tnama_toko)
        Me.Controls.Add(Me.tkd_toko)
        Me.Controls.Add(Me.LabelControl4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fpr_belumtagih"
        Me.Text = "Faktur Belum Tertagih"
        CType(Me.tnama_sales.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tkd_sales.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnama_toko.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tkd_toko.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents bts_sal As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tnama_sales As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tkd_sales As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents bts_tko As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tnama_toko As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tkd_toko As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
End Class
