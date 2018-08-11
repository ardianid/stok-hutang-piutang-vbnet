<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ftutup_bku
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
        Me.btproses = New DevExpress.XtraEditors.SimpleButton()
        Me.MemoEdit1 = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.cbbulan = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.MemoEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbbulan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btproses
        '
        Me.btproses.Location = New System.Drawing.Point(359, 12)
        Me.btproses.Name = "btproses"
        Me.btproses.Size = New System.Drawing.Size(89, 35)
        Me.btproses.TabIndex = 0
        Me.btproses.Text = "PROSES"
        '
        'MemoEdit1
        '
        Me.MemoEdit1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MemoEdit1.Location = New System.Drawing.Point(12, 53)
        Me.MemoEdit1.Name = "MemoEdit1"
        Me.MemoEdit1.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.MemoEdit1.Size = New System.Drawing.Size(436, 188)
        Me.MemoEdit1.TabIndex = 1
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 8)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(33, 13)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "Bulan :"
        '
        'cbbulan
        '
        Me.cbbulan.EditValue = "-- Pilih Bulan --"
        Me.cbbulan.Location = New System.Drawing.Point(12, 27)
        Me.cbbulan.Name = "cbbulan"
        Me.cbbulan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbbulan.Properties.Items.AddRange(New Object() {"Januari", "Februari", "Maret", "April", "Mei ", "Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember"})
        Me.cbbulan.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.cbbulan.Size = New System.Drawing.Size(246, 20)
        Me.cbbulan.TabIndex = 3
        '
        'ftutup_bku
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(459, 248)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.MemoEdit1)
        Me.Controls.Add(Me.btproses)
        Me.Controls.Add(Me.cbbulan)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ftutup_bku"
        Me.Text = "Tutup Buku"
        CType(Me.MemoEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbbulan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btproses As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents MemoEdit1 As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbbulan As DevExpress.XtraEditors.ComboBoxEdit
End Class
