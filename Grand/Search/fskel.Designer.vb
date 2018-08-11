<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fskel
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
        Me.grid1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ckode_kab = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ckode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cnama = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.tfind = New DevExpress.XtraEditors.TextEdit()
        Me.cb_criteria = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tfind.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cb_criteria.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grid1
        '
        Me.grid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grid1.Location = New System.Drawing.Point(2, 26)
        Me.grid1.MainView = Me.GridView1
        Me.grid1.Name = "grid1"
        Me.grid1.Size = New System.Drawing.Size(684, 392)
        Me.grid1.TabIndex = 5
        Me.grid1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ckode_kab, Me.ckode, Me.cnama, Me.GridColumn1})
        Me.GridView1.GridControl = Me.grid1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'ckode_kab
        '
        Me.ckode_kab.Caption = "Kecamatan"
        Me.ckode_kab.FieldName = "nama_kec"
        Me.ckode_kab.Name = "ckode_kab"
        Me.ckode_kab.OptionsColumn.AllowEdit = False
        Me.ckode_kab.Visible = True
        Me.ckode_kab.VisibleIndex = 1
        Me.ckode_kab.Width = 140
        '
        'ckode
        '
        Me.ckode.Caption = "Kode"
        Me.ckode.FieldName = "kd_kel"
        Me.ckode.Name = "ckode"
        Me.ckode.OptionsColumn.AllowEdit = False
        Me.ckode.Visible = True
        Me.ckode.VisibleIndex = 2
        Me.ckode.Width = 50
        '
        'cnama
        '
        Me.cnama.Caption = "Kelurahan"
        Me.cnama.FieldName = "nama_kel"
        Me.cnama.Name = "cnama"
        Me.cnama.OptionsColumn.AllowEdit = False
        Me.cnama.Visible = True
        Me.cnama.VisibleIndex = 3
        Me.cnama.Width = 301
        '
        'tfind
        '
        Me.tfind.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tfind.Location = New System.Drawing.Point(106, 0)
        Me.tfind.Name = "tfind"
        Me.tfind.Size = New System.Drawing.Size(580, 20)
        Me.tfind.TabIndex = 4
        '
        'cb_criteria
        '
        Me.cb_criteria.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cb_criteria.Location = New System.Drawing.Point(2, 0)
        Me.cb_criteria.Name = "cb_criteria"
        Me.cb_criteria.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cb_criteria.Properties.Items.AddRange(New Object() {"Kode", "Nama", "Kecamatan"})
        Me.cb_criteria.Size = New System.Drawing.Size(100, 20)
        Me.cb_criteria.TabIndex = 3
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Kabupaten"
        Me.GridColumn1.FieldName = "nama_kab"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 151
        '
        'fskel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(688, 420)
        Me.Controls.Add(Me.grid1)
        Me.Controls.Add(Me.tfind)
        Me.Controls.Add(Me.cb_criteria)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fskel"
        Me.Text = "Search Kelurahan"
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tfind.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cb_criteria.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grid1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ckode_kab As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ckode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cnama As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents tfind As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cb_criteria As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
End Class
