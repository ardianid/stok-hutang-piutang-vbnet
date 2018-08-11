<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fgroup2
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
        Me.tkode = New DevExpress.XtraEditors.TextEdit()
        Me.tnpw = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tnama = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.talamat = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.ttipe = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.tkode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnpw.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.talamat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttipe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(19, 15)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(31, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Kode :"
        '
        'tkode
        '
        Me.tkode.Location = New System.Drawing.Point(56, 12)
        Me.tkode.Name = "tkode"
        Me.tkode.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tkode.Size = New System.Drawing.Size(306, 20)
        Me.tkode.TabIndex = 1
        '
        'tnpw
        '
        Me.tnpw.Location = New System.Drawing.Point(56, 38)
        Me.tnpw.Name = "tnpw"
        Me.tnpw.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tnpw.Size = New System.Drawing.Size(306, 20)
        Me.tnpw.TabIndex = 3
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(14, 41)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(36, 13)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "NPWP :"
        '
        'tnama
        '
        Me.tnama.Location = New System.Drawing.Point(56, 78)
        Me.tnama.Name = "tnama"
        Me.tnama.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tnama.Size = New System.Drawing.Size(306, 20)
        Me.tnama.TabIndex = 4
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(16, 81)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(34, 13)
        Me.LabelControl3.TabIndex = 5
        Me.LabelControl3.Text = "Nama :"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(10, 107)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(40, 13)
        Me.LabelControl4.TabIndex = 7
        Me.LabelControl4.Text = "Alamat :"
        '
        'talamat
        '
        Me.talamat.Location = New System.Drawing.Point(56, 104)
        Me.talamat.Name = "talamat"
        Me.talamat.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.talamat.Size = New System.Drawing.Size(306, 95)
        Me.talamat.TabIndex = 6
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(23, 208)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(27, 13)
        Me.LabelControl5.TabIndex = 8
        Me.LabelControl5.Text = "Tipe :"
        '
        'ttipe
        '
        Me.ttipe.Location = New System.Drawing.Point(56, 205)
        Me.ttipe.Name = "ttipe"
        Me.ttipe.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttipe.Properties.Items.AddRange(New Object() {"Non-Group", "Group"})
        Me.ttipe.Size = New System.Drawing.Size(135, 20)
        Me.ttipe.TabIndex = 9
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(287, 260)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 23)
        Me.btclose.TabIndex = 11
        Me.btclose.Text = "&Keluar"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(206, 260)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 23)
        Me.btsimpan.TabIndex = 10
        Me.btsimpan.Text = "&Simpan"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl6.Location = New System.Drawing.Point(56, 61)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(167, 11)
        Me.LabelControl6.TabIndex = 12
        Me.LabelControl6.Text = "* Kalau NPWP diisi harus 20 Digit"
        '
        'fgroup2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(377, 289)
        Me.ControlBox = False
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.tnama)
        Me.Controls.Add(Me.tnpw)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.tkode)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.talamat)
        Me.Controls.Add(Me.ttipe)
        Me.Name = "fgroup2"
        Me.Text = "Group Outlet"
        CType(Me.tkode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnpw.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.talamat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttipe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tkode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tnpw As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tnama As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents talamat As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttipe As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
End Class
