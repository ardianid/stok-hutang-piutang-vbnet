<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fpr_pegawai
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
        Me.LabelControl15 = New DevExpress.XtraEditors.LabelControl()
        Me.cbgol = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btload = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.cbgol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl15
        '
        Me.LabelControl15.Location = New System.Drawing.Point(25, 32)
        Me.LabelControl15.Name = "LabelControl15"
        Me.LabelControl15.Size = New System.Drawing.Size(39, 13)
        Me.LabelControl15.TabIndex = 41
        Me.LabelControl15.Text = "Bagian :"
        '
        'cbgol
        '
        Me.cbgol.Location = New System.Drawing.Point(73, 29)
        Me.cbgol.Name = "cbgol"
        Me.cbgol.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbgol.Properties.Items.AddRange(New Object() {"ALL", "SALES", "PENJUALAN", "PIUTANG", "PAJAK", "KASIR", "HRD", "KRANI", "GUDANG", "SUPIR", "KENEK", "SATPAM", "LAIN-LAIN"})
        Me.cbgol.Size = New System.Drawing.Size(171, 20)
        Me.cbgol.TabIndex = 40
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(183, 71)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(61, 24)
        Me.btclose.TabIndex = 43
        Me.btclose.Text = "&Keluar"
        '
        'btload
        '
        Me.btload.Location = New System.Drawing.Point(116, 71)
        Me.btload.Name = "btload"
        Me.btload.Size = New System.Drawing.Size(61, 24)
        Me.btload.TabIndex = 42
        Me.btload.Text = "&Load"
        '
        'fpr_pegawai
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(271, 107)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btload)
        Me.Controls.Add(Me.LabelControl15)
        Me.Controls.Add(Me.cbgol)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fpr_pegawai"
        Me.Text = "Laporan Pegawai"
        CType(Me.cbgol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl15 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbgol As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btload As DevExpress.XtraEditors.SimpleButton
End Class
