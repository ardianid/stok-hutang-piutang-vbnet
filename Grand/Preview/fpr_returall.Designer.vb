﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fpr_returall
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
        Me.bts_supir = New DevExpress.XtraEditors.SimpleButton()
        Me.tnama_tko = New DevExpress.XtraEditors.TextEdit()
        Me.tkode_tko = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.talamat_tko = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl2 = New DevExpress.XtraEditors.DateEdit()
        Me.ttgl = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.tjenis = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.tnama_tko.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tkode_tko.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.talamat_tko.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tjenis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(309, 171)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 26)
        Me.btclose.TabIndex = 6
        Me.btclose.Text = "&Keluar"
        '
        'btload
        '
        Me.btload.Location = New System.Drawing.Point(228, 171)
        Me.btload.Name = "btload"
        Me.btload.Size = New System.Drawing.Size(75, 26)
        Me.btload.TabIndex = 5
        Me.btload.Text = "&Load"
        '
        'bts_supir
        '
        Me.bts_supir.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bts_supir.Appearance.Options.UseFont = True
        Me.bts_supir.Image = Global.Grand.My.Resources.Resources._1389260518_xmag
        Me.bts_supir.Location = New System.Drawing.Point(359, 37)
        Me.bts_supir.Name = "bts_supir"
        Me.bts_supir.Size = New System.Drawing.Size(25, 21)
        Me.bts_supir.TabIndex = 3
        '
        'tnama_tko
        '
        Me.tnama_tko.Enabled = False
        Me.tnama_tko.Location = New System.Drawing.Point(135, 38)
        Me.tnama_tko.Name = "tnama_tko"
        Me.tnama_tko.Size = New System.Drawing.Size(218, 20)
        Me.tnama_tko.TabIndex = 60
        '
        'tkode_tko
        '
        Me.tkode_tko.Location = New System.Drawing.Point(83, 38)
        Me.tkode_tko.Name = "tkode_tko"
        Me.tkode_tko.Size = New System.Drawing.Size(48, 20)
        Me.tkode_tko.TabIndex = 2
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(13, 41)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(64, 13)
        Me.LabelControl3.TabIndex = 59
        Me.LabelControl3.Text = "Toko/Outlet :"
        '
        'talamat_tko
        '
        Me.talamat_tko.Enabled = False
        Me.talamat_tko.Location = New System.Drawing.Point(83, 64)
        Me.talamat_tko.Name = "talamat_tko"
        Me.talamat_tko.Size = New System.Drawing.Size(270, 51)
        Me.talamat_tko.TabIndex = 58
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(211, 17)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(15, 13)
        Me.LabelControl2.TabIndex = 57
        Me.LabelControl2.Text = "s.d"
        '
        'ttgl2
        '
        Me.ttgl2.EditValue = Nothing
        Me.ttgl2.Location = New System.Drawing.Point(236, 12)
        Me.ttgl2.Name = "ttgl2"
        Me.ttgl2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl2.Properties.Mask.EditMask = ""
        Me.ttgl2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl2.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl2.Size = New System.Drawing.Size(117, 20)
        Me.ttgl2.TabIndex = 1
        '
        'ttgl
        '
        Me.ttgl.EditValue = Nothing
        Me.ttgl.Location = New System.Drawing.Point(83, 12)
        Me.ttgl.Name = "ttgl"
        Me.ttgl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl.Properties.Mask.EditMask = ""
        Me.ttgl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl.Size = New System.Drawing.Size(117, 20)
        Me.ttgl.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(32, 15)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(45, 13)
        Me.LabelControl1.TabIndex = 56
        Me.LabelControl1.Text = "Tanggal :"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(16, 124)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(61, 13)
        Me.LabelControl4.TabIndex = 61
        Me.LabelControl4.Text = "Jenis Retur :"
        '
        'tjenis
        '
        Me.tjenis.Location = New System.Drawing.Point(83, 121)
        Me.tjenis.Name = "tjenis"
        Me.tjenis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tjenis.Properties.Items.AddRange(New Object() {"All", "Potong Langsung", "Potong Tagihan"})
        Me.tjenis.Size = New System.Drawing.Size(270, 20)
        Me.tjenis.TabIndex = 4
        '
        'fpr_returall
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(396, 210)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btload)
        Me.Controls.Add(Me.bts_supir)
        Me.Controls.Add(Me.tnama_tko)
        Me.Controls.Add(Me.tkode_tko)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.talamat_tko)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.ttgl2)
        Me.Controls.Add(Me.ttgl)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.tjenis)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fpr_returall"
        Me.Text = "Laporan Retur Barang"
        CType(Me.tnama_tko.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tkode_tko.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.talamat_tko.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tjenis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents bts_supir As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tnama_tko As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tkode_tko As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents talamat_tko As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl2 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents ttgl As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tjenis As DevExpress.XtraEditors.ComboBoxEdit
End Class
