<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fpr_pasar
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
        Me.bts_kel = New DevExpress.XtraEditors.SimpleButton()
        Me.tnama_kel = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl23 = New DevExpress.XtraEditors.LabelControl()
        Me.tkd_kel = New DevExpress.XtraEditors.TextEdit()
        Me.bts_kec = New DevExpress.XtraEditors.SimpleButton()
        Me.tnama_kec = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tkd_kec = New DevExpress.XtraEditors.TextEdit()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btload = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.tnama_kel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tkd_kel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnama_kec.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tkd_kec.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'bts_kel
        '
        Me.bts_kel.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bts_kel.Appearance.Options.UseFont = True
        Me.bts_kel.Image = Global.Grand.My.Resources.Resources._1389260518_xmag
        Me.bts_kel.Location = New System.Drawing.Point(390, 48)
        Me.bts_kel.Name = "bts_kel"
        Me.bts_kel.Size = New System.Drawing.Size(25, 21)
        Me.bts_kel.TabIndex = 3
        '
        'tnama_kel
        '
        Me.tnama_kel.Enabled = False
        Me.tnama_kel.Location = New System.Drawing.Point(144, 49)
        Me.tnama_kel.Name = "tnama_kel"
        Me.tnama_kel.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tnama_kel.Size = New System.Drawing.Size(242, 20)
        Me.tnama_kel.TabIndex = 22
        '
        'LabelControl23
        '
        Me.LabelControl23.Location = New System.Drawing.Point(23, 52)
        Me.LabelControl23.Name = "LabelControl23"
        Me.LabelControl23.Size = New System.Drawing.Size(55, 13)
        Me.LabelControl23.TabIndex = 20
        Me.LabelControl23.Text = "Kelurahan :"
        '
        'tkd_kel
        '
        Me.tkd_kel.Location = New System.Drawing.Point(84, 49)
        Me.tkd_kel.Name = "tkd_kel"
        Me.tkd_kel.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tkd_kel.Size = New System.Drawing.Size(57, 20)
        Me.tkd_kel.TabIndex = 2
        '
        'bts_kec
        '
        Me.bts_kec.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bts_kec.Appearance.Options.UseFont = True
        Me.bts_kec.Image = Global.Grand.My.Resources.Resources._1389260518_xmag
        Me.bts_kec.Location = New System.Drawing.Point(390, 22)
        Me.bts_kec.Name = "bts_kec"
        Me.bts_kec.Size = New System.Drawing.Size(25, 21)
        Me.bts_kec.TabIndex = 1
        '
        'tnama_kec
        '
        Me.tnama_kec.Enabled = False
        Me.tnama_kec.Location = New System.Drawing.Point(144, 23)
        Me.tnama_kec.Name = "tnama_kec"
        Me.tnama_kec.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tnama_kec.Size = New System.Drawing.Size(242, 20)
        Me.tnama_kec.TabIndex = 26
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(18, 26)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(60, 13)
        Me.LabelControl1.TabIndex = 24
        Me.LabelControl1.Text = "Kecamatan :"
        '
        'tkd_kec
        '
        Me.tkd_kec.Location = New System.Drawing.Point(84, 23)
        Me.tkd_kec.Name = "tkd_kec"
        Me.tkd_kec.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tkd_kec.Size = New System.Drawing.Size(57, 20)
        Me.tkd_kec.TabIndex = 0
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(349, 89)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(66, 28)
        Me.btclose.TabIndex = 45
        Me.btclose.Text = "&Keluar"
        '
        'btload
        '
        Me.btload.Location = New System.Drawing.Point(277, 89)
        Me.btload.Name = "btload"
        Me.btload.Size = New System.Drawing.Size(66, 28)
        Me.btload.TabIndex = 44
        Me.btload.Text = "&Load"
        '
        'fpr_pasar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(434, 125)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btload)
        Me.Controls.Add(Me.bts_kec)
        Me.Controls.Add(Me.tnama_kec)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.tkd_kec)
        Me.Controls.Add(Me.bts_kel)
        Me.Controls.Add(Me.tnama_kel)
        Me.Controls.Add(Me.LabelControl23)
        Me.Controls.Add(Me.tkd_kel)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fpr_pasar"
        Me.Text = "Data Pasar"
        CType(Me.tnama_kel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tkd_kel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnama_kec.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tkd_kec.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents bts_kel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tnama_kel As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl23 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tkd_kel As DevExpress.XtraEditors.TextEdit
    Friend WithEvents bts_kec As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tnama_kec As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tkd_kec As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btload As DevExpress.XtraEditors.SimpleButton
End Class
