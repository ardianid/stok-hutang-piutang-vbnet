<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fpasar2
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
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tkd_kel = New DevExpress.XtraEditors.TextEdit()
        Me.tnama_kel = New DevExpress.XtraEditors.TextEdit()
        Me.bts_kec = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tnama = New DevExpress.XtraEditors.TextEdit()
        Me.tkode = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.tkd_kel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnama_kel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tkode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(41, 26)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(55, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Kelurahan :"
        '
        'tkd_kel
        '
        Me.tkd_kel.Location = New System.Drawing.Point(102, 23)
        Me.tkd_kel.Name = "tkd_kel"
        Me.tkd_kel.Size = New System.Drawing.Size(56, 20)
        Me.tkd_kel.TabIndex = 1
        '
        'tnama_kel
        '
        Me.tnama_kel.Enabled = False
        Me.tnama_kel.Location = New System.Drawing.Point(162, 23)
        Me.tnama_kel.Name = "tnama_kel"
        Me.tnama_kel.Size = New System.Drawing.Size(275, 20)
        Me.tnama_kel.TabIndex = 2
        '
        'bts_kec
        '
        Me.bts_kec.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bts_kec.Appearance.Options.UseFont = True
        Me.bts_kec.Image = Global.Grand.My.Resources.Resources._1389260518_xmag
        Me.bts_kec.Location = New System.Drawing.Point(441, 22)
        Me.bts_kec.Name = "bts_kec"
        Me.bts_kec.Size = New System.Drawing.Size(25, 21)
        Me.bts_kec.TabIndex = 10
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(32, 78)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(64, 13)
        Me.LabelControl3.TabIndex = 17
        Me.LabelControl3.Text = "Nama Pasar :"
        '
        'tnama
        '
        Me.tnama.Location = New System.Drawing.Point(102, 75)
        Me.tnama.Name = "tnama"
        Me.tnama.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tnama.Size = New System.Drawing.Size(335, 20)
        Me.tnama.TabIndex = 15
        '
        'tkode
        '
        Me.tkode.Location = New System.Drawing.Point(102, 49)
        Me.tkode.Name = "tkode"
        Me.tkode.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tkode.Size = New System.Drawing.Size(335, 20)
        Me.tkode.TabIndex = 14
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(35, 52)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(61, 13)
        Me.LabelControl2.TabIndex = 16
        Me.LabelControl2.Text = "Kode Pasar :"
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(395, 143)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 23)
        Me.btclose.TabIndex = 19
        Me.btclose.Text = "&Keluar"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(314, 143)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 23)
        Me.btsimpan.TabIndex = 18
        Me.btsimpan.Text = "&Simpan"
        '
        'fpasar2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(507, 178)
        Me.ControlBox = False
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.tnama)
        Me.Controls.Add(Me.tkode)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.bts_kec)
        Me.Controls.Add(Me.tnama_kel)
        Me.Controls.Add(Me.tkd_kel)
        Me.Controls.Add(Me.LabelControl1)
        Me.Name = "fpasar2"
        Me.Text = "Pasar"
        CType(Me.tkd_kel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnama_kel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tkode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tkd_kel As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tnama_kel As DevExpress.XtraEditors.TextEdit
    Friend WithEvents bts_kec As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tnama As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tkode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
End Class
