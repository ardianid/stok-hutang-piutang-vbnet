﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fpr_belicust
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
        Me.tnama_sales = New DevExpress.XtraEditors.TextEdit()
        Me.tkd_sales = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.tnama_toko = New DevExpress.XtraEditors.TextEdit()
        Me.tkd_toko = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl2 = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.bts_sal = New DevExpress.XtraEditors.SimpleButton()
        Me.bts_tko = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.tnama_sales.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tkd_sales.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnama_toko.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tkd_toko.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(319, 117)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 26)
        Me.btclose.TabIndex = 5
        Me.btclose.Text = "&Keluar"
        '
        'btload
        '
        Me.btload.Location = New System.Drawing.Point(238, 117)
        Me.btload.Name = "btload"
        Me.btload.Size = New System.Drawing.Size(75, 26)
        Me.btload.TabIndex = 4
        Me.btload.Text = "&Load"
        '
        'tnama_sales
        '
        Me.tnama_sales.Enabled = False
        Me.tnama_sales.Location = New System.Drawing.Point(145, 73)
        Me.tnama_sales.Name = "tnama_sales"
        Me.tnama_sales.Size = New System.Drawing.Size(218, 20)
        Me.tnama_sales.TabIndex = 219
        '
        'tkd_sales
        '
        Me.tkd_sales.Location = New System.Drawing.Point(93, 73)
        Me.tkd_sales.Name = "tkd_sales"
        Me.tkd_sales.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tkd_sales.Size = New System.Drawing.Size(48, 20)
        Me.tkd_sales.TabIndex = 3
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(55, 76)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(32, 13)
        Me.LabelControl5.TabIndex = 220
        Me.LabelControl5.Text = "Sales :"
        '
        'tnama_toko
        '
        Me.tnama_toko.Enabled = False
        Me.tnama_toko.Location = New System.Drawing.Point(145, 47)
        Me.tnama_toko.Name = "tnama_toko"
        Me.tnama_toko.Size = New System.Drawing.Size(218, 20)
        Me.tnama_toko.TabIndex = 218
        '
        'tkd_toko
        '
        Me.tkd_toko.Location = New System.Drawing.Point(93, 47)
        Me.tkd_toko.Name = "tkd_toko"
        Me.tkd_toko.Size = New System.Drawing.Size(48, 20)
        Me.tkd_toko.TabIndex = 2
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(23, 50)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(64, 13)
        Me.LabelControl4.TabIndex = 217
        Me.LabelControl4.Text = "Toko/Outlet :"
        '
        'ttgl2
        '
        Me.ttgl2.EditValue = Nothing
        Me.ttgl2.Location = New System.Drawing.Point(248, 21)
        Me.ttgl2.Name = "ttgl2"
        Me.ttgl2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl2.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl2.Properties.Mask.EditMask = ""
        Me.ttgl2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl2.Size = New System.Drawing.Size(115, 20)
        Me.ttgl2.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(219, 24)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(17, 13)
        Me.LabelControl2.TabIndex = 216
        Me.LabelControl2.Text = "S.D"
        '
        'ttgl
        '
        Me.ttgl.EditValue = Nothing
        Me.ttgl.Location = New System.Drawing.Point(93, 21)
        Me.ttgl.Name = "ttgl"
        Me.ttgl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl.Properties.Mask.EditMask = ""
        Me.ttgl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl.Size = New System.Drawing.Size(115, 20)
        Me.ttgl.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(42, 24)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(45, 13)
        Me.LabelControl1.TabIndex = 215
        Me.LabelControl1.Text = "Tanggal :"
        '
        'bts_sal
        '
        Me.bts_sal.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bts_sal.Appearance.Options.UseFont = True
        Me.bts_sal.Image = Global.Grand.My.Resources.Resources._1389260518_xmag
        Me.bts_sal.Location = New System.Drawing.Point(369, 72)
        Me.bts_sal.Name = "bts_sal"
        Me.bts_sal.Size = New System.Drawing.Size(25, 21)
        Me.bts_sal.TabIndex = 212
        '
        'bts_tko
        '
        Me.bts_tko.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bts_tko.Appearance.Options.UseFont = True
        Me.bts_tko.Image = Global.Grand.My.Resources.Resources._1389260518_xmag
        Me.bts_tko.Location = New System.Drawing.Point(369, 46)
        Me.bts_tko.Name = "bts_tko"
        Me.bts_tko.Size = New System.Drawing.Size(25, 21)
        Me.bts_tko.TabIndex = 210
        '
        'fpr_belicust
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(426, 155)
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
        Me.Controls.Add(Me.ttgl2)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.ttgl)
        Me.Controls.Add(Me.LabelControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fpr_belicust"
        Me.Text = "Pembelian Dari Outlet"
        CType(Me.tnama_sales.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tkd_sales.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnama_toko.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tkd_toko.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents ttgl2 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
