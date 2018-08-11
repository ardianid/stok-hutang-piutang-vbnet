<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fsetprinter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fsetprinter))
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.cbprint1 = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cbprint2 = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.SimpleButton2 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.cbos = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.cbprint1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbprint2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.cbos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(283, 79)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(57, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "PRINTER I :"
        '
        'cbprint1
        '
        Me.cbprint1.Location = New System.Drawing.Point(356, 76)
        Me.cbprint1.Name = "cbprint1"
        Me.cbprint1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbprint1.Size = New System.Drawing.Size(310, 20)
        Me.cbprint1.TabIndex = 0
        '
        'cbprint2
        '
        Me.cbprint2.Location = New System.Drawing.Point(356, 109)
        Me.cbprint2.Name = "cbprint2"
        Me.cbprint2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbprint2.Size = New System.Drawing.Size(310, 20)
        Me.cbprint2.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(279, 112)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(61, 13)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "PRINTER II :"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.BackColor = System.Drawing.Color.White
        Me.LabelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.LabelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl3.Location = New System.Drawing.Point(2, 21)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(255, 263)
        Me.LabelControl3.TabIndex = 4
        Me.LabelControl3.Text = resources.GetString("LabelControl3.Text")
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.LabelControl3)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(259, 286)
        Me.GroupControl1.TabIndex = 5
        Me.GroupControl1.Text = "NOTE"
        '
        'SimpleButton2
        '
        Me.SimpleButton2.Location = New System.Drawing.Point(482, 167)
        Me.SimpleButton2.Name = "SimpleButton2"
        Me.SimpleButton2.Size = New System.Drawing.Size(89, 48)
        Me.SimpleButton2.TabIndex = 3
        Me.SimpleButton2.Text = "&OK"
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Location = New System.Drawing.Point(577, 167)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(89, 48)
        Me.SimpleButton1.TabIndex = 4
        Me.SimpleButton1.Text = "&KELUAR"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(319, 245)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(21, 13)
        Me.LabelControl4.TabIndex = 6
        Me.LabelControl4.Text = "OS :"
        Me.LabelControl4.Visible = False
        '
        'cbos
        '
        Me.cbos.Location = New System.Drawing.Point(356, 242)
        Me.cbos.Name = "cbos"
        Me.cbos.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbos.Properties.Items.AddRange(New Object() {"Windows XP", "Windows 7,8,10"})
        Me.cbos.Size = New System.Drawing.Size(310, 20)
        Me.cbos.TabIndex = 2
        Me.cbos.Visible = False
        '
        'fsetprinter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(687, 286)
        Me.Controls.Add(Me.cbos)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.SimpleButton2)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.cbprint2)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.cbprint1)
        Me.Controls.Add(Me.LabelControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fsetprinter"
        Me.Text = "Printer"
        CType(Me.cbprint1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbprint2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.cbos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbprint1 As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cbprint2 As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents SimpleButton2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbos As DevExpress.XtraEditors.ComboBoxEdit
End Class
