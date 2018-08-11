<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fkel2
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
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tnama = New DevExpress.XtraEditors.TextEdit()
        Me.tkode = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tkd_kec = New DevExpress.XtraEditors.TextEdit()
        Me.tnama_kec = New DevExpress.XtraEditors.TextEdit()
        Me.bts_kec = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tkode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tkd_kec.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnama_kec.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(365, 137)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 23)
        Me.btclose.TabIndex = 4
        Me.btclose.Text = "&Keluar"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(284, 137)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 23)
        Me.btsimpan.TabIndex = 3
        Me.btsimpan.Text = "&Simpan"
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(38, 77)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(51, 13)
        Me.LabelControl3.TabIndex = 13
        Me.LabelControl3.Text = "Nama Kel :"
        '
        'tnama
        '
        Me.tnama.Location = New System.Drawing.Point(95, 74)
        Me.tnama.Name = "tnama"
        Me.tnama.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tnama.Size = New System.Drawing.Size(302, 20)
        Me.tnama.TabIndex = 2
        '
        'tkode
        '
        Me.tkode.Location = New System.Drawing.Point(95, 48)
        Me.tkode.Name = "tkode"
        Me.tkode.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tkode.Size = New System.Drawing.Size(302, 20)
        Me.tkode.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(41, 51)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(48, 13)
        Me.LabelControl2.TabIndex = 9
        Me.LabelControl2.Text = "Kode Kel :"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(29, 25)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(60, 13)
        Me.LabelControl1.TabIndex = 6
        Me.LabelControl1.Text = "Kecamatan :"
        '
        'tkd_kec
        '
        Me.tkd_kec.Location = New System.Drawing.Point(95, 22)
        Me.tkd_kec.Name = "tkd_kec"
        Me.tkd_kec.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tkd_kec.Size = New System.Drawing.Size(57, 20)
        Me.tkd_kec.TabIndex = 0
        '
        'tnama_kec
        '
        Me.tnama_kec.Enabled = False
        Me.tnama_kec.Location = New System.Drawing.Point(155, 22)
        Me.tnama_kec.Name = "tnama_kec"
        Me.tnama_kec.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tnama_kec.Size = New System.Drawing.Size(242, 20)
        Me.tnama_kec.TabIndex = 14
        '
        'bts_kec
        '
        Me.bts_kec.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bts_kec.Appearance.Options.UseFont = True
        Me.bts_kec.Image = Global.Grand.My.Resources.Resources._1389260518_xmag
        Me.bts_kec.Location = New System.Drawing.Point(401, 21)
        Me.bts_kec.Name = "bts_kec"
        Me.bts_kec.Size = New System.Drawing.Size(25, 21)
        Me.bts_kec.TabIndex = 9
        '
        'fkel2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(483, 172)
        Me.ControlBox = False
        Me.Controls.Add(Me.bts_kec)
        Me.Controls.Add(Me.tnama_kec)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.tnama)
        Me.Controls.Add(Me.tkode)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.tkd_kec)
        Me.Name = "fkel2"
        Me.Text = "Kelurahan"
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tkode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tkd_kec.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnama_kec.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tnama As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tkode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tkd_kec As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tnama_kec As DevExpress.XtraEditors.TextEdit
    Friend WithEvents bts_kec As DevExpress.XtraEditors.SimpleButton
End Class
