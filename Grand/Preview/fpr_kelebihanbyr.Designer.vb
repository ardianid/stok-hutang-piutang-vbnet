<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fpr_kelebihanbyr
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
        Me.bts_supir = New DevExpress.XtraEditors.SimpleButton()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btload = New DevExpress.XtraEditors.SimpleButton()
        Me.tnama_tko = New DevExpress.XtraEditors.TextEdit()
        Me.tkode_tko = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.talamat_tko = New DevExpress.XtraEditors.MemoEdit()
        CType(Me.tnama_tko.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tkode_tko.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.talamat_tko.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'bts_supir
        '
        Me.bts_supir.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bts_supir.Appearance.Options.UseFont = True
        Me.bts_supir.Image = Global.Grand.My.Resources.Resources._1389260518_xmag
        Me.bts_supir.Location = New System.Drawing.Point(359, 11)
        Me.bts_supir.Name = "bts_supir"
        Me.bts_supir.Size = New System.Drawing.Size(25, 21)
        Me.bts_supir.TabIndex = 1
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(309, 112)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 26)
        Me.btclose.TabIndex = 4
        Me.btclose.Text = "&Keluar"
        '
        'btload
        '
        Me.btload.Location = New System.Drawing.Point(228, 112)
        Me.btload.Name = "btload"
        Me.btload.Size = New System.Drawing.Size(75, 26)
        Me.btload.TabIndex = 3
        Me.btload.Text = "&Load"
        '
        'tnama_tko
        '
        Me.tnama_tko.Enabled = False
        Me.tnama_tko.Location = New System.Drawing.Point(135, 12)
        Me.tnama_tko.Name = "tnama_tko"
        Me.tnama_tko.Size = New System.Drawing.Size(218, 20)
        Me.tnama_tko.TabIndex = 18
        '
        'tkode_tko
        '
        Me.tkode_tko.Location = New System.Drawing.Point(83, 12)
        Me.tkode_tko.Name = "tkode_tko"
        Me.tkode_tko.Size = New System.Drawing.Size(48, 20)
        Me.tkode_tko.TabIndex = 0
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(13, 15)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(64, 13)
        Me.LabelControl3.TabIndex = 17
        Me.LabelControl3.Text = "Toko/Outlet :"
        '
        'talamat_tko
        '
        Me.talamat_tko.Enabled = False
        Me.talamat_tko.Location = New System.Drawing.Point(83, 38)
        Me.talamat_tko.Name = "talamat_tko"
        Me.talamat_tko.Size = New System.Drawing.Size(270, 51)
        Me.talamat_tko.TabIndex = 14
        '
        'fpr_kelebihanbyr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(408, 145)
        Me.Controls.Add(Me.bts_supir)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btload)
        Me.Controls.Add(Me.tnama_tko)
        Me.Controls.Add(Me.tkode_tko)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.talamat_tko)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fpr_kelebihanbyr"
        Me.Text = "Kelebihan Bayar"
        CType(Me.tnama_tko.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tkode_tko.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.talamat_tko.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents bts_supir As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tnama_tko As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tkode_tko As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents talamat_tko As DevExpress.XtraEditors.MemoEdit
End Class
