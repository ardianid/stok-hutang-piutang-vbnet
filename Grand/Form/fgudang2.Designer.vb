<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fgudang2
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
        Me.tnama = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.ttipe = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.tket = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        Me.lbnopol = New DevExpress.XtraEditors.LabelControl()
        Me.tnopol = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.cdef = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.tkode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttipe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tket.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnopol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cdef.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(21, 15)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(31, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Kode :"
        '
        'tkode
        '
        Me.tkode.Location = New System.Drawing.Point(58, 12)
        Me.tkode.Name = "tkode"
        Me.tkode.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tkode.Size = New System.Drawing.Size(309, 20)
        Me.tkode.TabIndex = 0
        '
        'tnama
        '
        Me.tnama.Location = New System.Drawing.Point(58, 38)
        Me.tnama.Name = "tnama"
        Me.tnama.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tnama.Size = New System.Drawing.Size(309, 20)
        Me.tnama.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(18, 41)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(34, 13)
        Me.LabelControl2.TabIndex = 4
        Me.LabelControl2.Text = "Nama :"
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(25, 67)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(27, 13)
        Me.LabelControl3.TabIndex = 5
        Me.LabelControl3.Text = "Tipe :"
        '
        'ttipe
        '
        Me.ttipe.Location = New System.Drawing.Point(58, 64)
        Me.ttipe.Name = "ttipe"
        Me.ttipe.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttipe.Properties.Items.AddRange(New Object() {"FISIK", "MOBIL"})
        Me.ttipe.Size = New System.Drawing.Size(138, 20)
        Me.ttipe.TabIndex = 2
        '
        'tket
        '
        Me.tket.Location = New System.Drawing.Point(58, 90)
        Me.tket.Name = "tket"
        Me.tket.Size = New System.Drawing.Size(309, 57)
        Me.tket.TabIndex = 4
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(29, 93)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(23, 13)
        Me.LabelControl4.TabIndex = 7
        Me.LabelControl4.Text = "Ket :"
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(292, 235)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 23)
        Me.btclose.TabIndex = 7
        Me.btclose.Text = "&Keluar"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(211, 235)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 23)
        Me.btsimpan.TabIndex = 6
        Me.btsimpan.Text = "&Simpan"
        '
        'lbnopol
        '
        Me.lbnopol.Location = New System.Drawing.Point(211, 67)
        Me.lbnopol.Name = "lbnopol"
        Me.lbnopol.Size = New System.Drawing.Size(34, 13)
        Me.lbnopol.TabIndex = 8
        Me.lbnopol.Text = "Nopol :"
        '
        'tnopol
        '
        Me.tnopol.Location = New System.Drawing.Point(251, 64)
        Me.tnopol.Name = "tnopol"
        Me.tnopol.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tnopol.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nopol", "Nopol"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tipe", 13, "Tipe")})
        Me.tnopol.Properties.DisplayMember = "nopol"
        Me.tnopol.Properties.NullText = ""
        Me.tnopol.Properties.ValueMember = "nopol"
        Me.tnopol.Size = New System.Drawing.Size(116, 20)
        Me.tnopol.TabIndex = 3
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(29, 164)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(269, 13)
        Me.LabelControl5.TabIndex = 9
        Me.LabelControl5.Text = "Apakah gudang ini default untuk barang kosong masuk :"
        '
        'cdef
        '
        Me.cdef.Location = New System.Drawing.Point(56, 183)
        Me.cdef.Name = "cdef"
        Me.cdef.Properties.Caption = "&Ya"
        Me.cdef.Size = New System.Drawing.Size(75, 19)
        Me.cdef.TabIndex = 5
        '
        'fgudang2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(386, 270)
        Me.ControlBox = False
        Me.Controls.Add(Me.cdef)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.tnopol)
        Me.Controls.Add(Me.lbnopol)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.tnama)
        Me.Controls.Add(Me.tkode)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.ttipe)
        Me.Controls.Add(Me.tket)
        Me.Name = "fgudang2"
        Me.Text = "Gudang"
        CType(Me.tkode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttipe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tket.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnopol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cdef.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tkode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tnama As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttipe As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents tket As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lbnopol As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tnopol As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cdef As DevExpress.XtraEditors.CheckEdit
End Class
