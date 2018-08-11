<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fkendaraan2
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
        Me.tnopol = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.ttipe = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tket = New DevExpress.XtraEditors.MemoEdit()
        Me.caktif = New DevExpress.XtraEditors.CheckEdit()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.tnopol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttipe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tket.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.caktif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(16, 15)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(34, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Nopol :"
        '
        'tnopol
        '
        Me.tnopol.Location = New System.Drawing.Point(56, 12)
        Me.tnopol.Name = "tnopol"
        Me.tnopol.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tnopol.Size = New System.Drawing.Size(152, 20)
        Me.tnopol.TabIndex = 0
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(23, 41)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(27, 13)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "Tipe :"
        '
        'ttipe
        '
        Me.ttipe.Location = New System.Drawing.Point(56, 38)
        Me.ttipe.Name = "ttipe"
        Me.ttipe.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttipe.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.ttipe.Properties.Items.AddRange(New Object() {"MOBIL WTL", "EXPEDISI WTL", "EXPEDISI LUAR", "LAIN-LAIN"})
        Me.ttipe.Size = New System.Drawing.Size(214, 20)
        Me.ttipe.TabIndex = 2
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(27, 67)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(23, 13)
        Me.LabelControl3.TabIndex = 4
        Me.LabelControl3.Text = "Ket :"
        '
        'tket
        '
        Me.tket.Location = New System.Drawing.Point(56, 64)
        Me.tket.Name = "tket"
        Me.tket.Size = New System.Drawing.Size(214, 64)
        Me.tket.TabIndex = 3
        '
        'caktif
        '
        Me.caktif.Location = New System.Drawing.Point(214, 13)
        Me.caktif.Name = "caktif"
        Me.caktif.Properties.Caption = "&Aktif"
        Me.caktif.Size = New System.Drawing.Size(56, 19)
        Me.caktif.TabIndex = 1
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(114, 183)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 23)
        Me.btsimpan.TabIndex = 4
        Me.btsimpan.Text = "&Simpan"
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(195, 183)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 23)
        Me.btclose.TabIndex = 5
        Me.btclose.Text = "&Keluar"
        '
        'fkendaraan2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(295, 216)
        Me.ControlBox = False
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.caktif)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.tnopol)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.ttipe)
        Me.Controls.Add(Me.tket)
        Me.Name = "fkendaraan2"
        Me.Text = "Kendaraan"
        CType(Me.tnopol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttipe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tket.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.caktif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tnopol As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttipe As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tket As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents caktif As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
End Class
