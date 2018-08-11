<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fadjust_br
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fadjust_br))
        Me.bn1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsadd = New System.Windows.Forms.ToolStripButton()
        Me.tsedit = New System.Windows.Forms.ToolStripButton()
        Me.tsdel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsview = New System.Windows.Forms.ToolStripButton()
        Me.tsref = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel3 = New System.Windows.Forms.ToolStripLabel()
        Me.tcbofind = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripButton9 = New System.Windows.Forms.ToolStripButton()
        Me.tfind = New System.Windows.Forms.ToolStripTextBox()
        Me.tsfind = New System.Windows.Forms.ToolStripButton()
        Me.grid1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ckode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cnama = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.stbatal = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.stkirim = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.stpulang = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.bn1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.bn1.SuspendLayout()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.stbatal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.stkirim, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.stpulang, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'bn1
        '
        Me.bn1.AddNewItem = Nothing
        Me.bn1.CountItem = Me.ToolStripLabel2
        Me.bn1.DeleteItem = Nothing
        Me.bn1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bn1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.bn1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton2, Me.ToolStripButton3, Me.ToolStripSeparator2, Me.ToolStripTextBox1, Me.ToolStripLabel2, Me.ToolStripSeparator3, Me.ToolStripButton4, Me.ToolStripButton5, Me.ToolStripSeparator4, Me.tsadd, Me.tsedit, Me.tsdel, Me.ToolStripSeparator6, Me.ToolStripSeparator1, Me.tsview, Me.tsref, Me.ToolStripSeparator5, Me.ToolStripLabel3, Me.tcbofind, Me.ToolStripButton9, Me.tfind, Me.tsfind})
        Me.bn1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.bn1.Location = New System.Drawing.Point(0, 0)
        Me.bn1.MoveFirstItem = Me.ToolStripButton2
        Me.bn1.MoveLastItem = Me.ToolStripButton5
        Me.bn1.MoveNextItem = Me.ToolStripButton4
        Me.bn1.MovePreviousItem = Me.ToolStripButton3
        Me.bn1.Name = "bn1"
        Me.bn1.PositionItem = Me.ToolStripTextBox1
        Me.bn1.Size = New System.Drawing.Size(913, 36)
        Me.bn1.TabIndex = 139
        Me.bn1.Text = "BindingNavigator1"
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.ForeColor = System.Drawing.Color.Black
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(36, 33)
        Me.ToolStripLabel2.Text = "of {0}"
        Me.ToolStripLabel2.ToolTipText = "Total number of items"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton2.Size = New System.Drawing.Size(23, 33)
        Me.ToolStripButton2.Tag = "True"
        Me.ToolStripButton2.Text = "Move first"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton3.Size = New System.Drawing.Size(23, 33)
        Me.ToolStripButton3.Tag = "True"
        Me.ToolStripButton3.Text = "Move previous"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 36)
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.AccessibleName = "Position"
        Me.ToolStripTextBox1.AutoSize = False
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(58, 21)
        Me.ToolStripTextBox1.Text = "0"
        Me.ToolStripTextBox1.ToolTipText = "Current position"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 36)
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton4.Size = New System.Drawing.Size(23, 33)
        Me.ToolStripButton4.Tag = "True"
        Me.ToolStripButton4.Text = "Move next"
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton5.Image = CType(resources.GetObject("ToolStripButton5.Image"), System.Drawing.Image)
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton5.Size = New System.Drawing.Size(23, 33)
        Me.ToolStripButton5.Tag = "True"
        Me.ToolStripButton5.Text = "Move last"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 36)
        '
        'tsadd
        '
        Me.tsadd.AutoSize = False
        Me.tsadd.ForeColor = System.Drawing.Color.Black
        Me.tsadd.Image = CType(resources.GetObject("tsadd.Image"), System.Drawing.Image)
        Me.tsadd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsadd.Name = "tsadd"
        Me.tsadd.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tsadd.Size = New System.Drawing.Size(49, 33)
        Me.tsadd.Text = "&Tambah"
        Me.tsadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsadd.ToolTipText = "Tambah data"
        '
        'tsedit
        '
        Me.tsedit.AutoSize = False
        Me.tsedit.ForeColor = System.Drawing.Color.Black
        Me.tsedit.Image = CType(resources.GetObject("tsedit.Image"), System.Drawing.Image)
        Me.tsedit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsedit.Name = "tsedit"
        Me.tsedit.Size = New System.Drawing.Size(49, 33)
        Me.tsedit.Text = "&Edit"
        Me.tsedit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsedit.ToolTipText = "Rubah data"
        '
        'tsdel
        '
        Me.tsdel.AutoSize = False
        Me.tsdel.ForeColor = System.Drawing.Color.Black
        Me.tsdel.Image = CType(resources.GetObject("tsdel.Image"), System.Drawing.Image)
        Me.tsdel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsdel.Name = "tsdel"
        Me.tsdel.Size = New System.Drawing.Size(49, 33)
        Me.tsdel.Text = "&Batal"
        Me.tsdel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsdel.ToolTipText = "Batal"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 36)
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 36)
        '
        'tsview
        '
        Me.tsview.AutoSize = False
        Me.tsview.ForeColor = System.Drawing.Color.Black
        Me.tsview.Image = CType(resources.GetObject("tsview.Image"), System.Drawing.Image)
        Me.tsview.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsview.Name = "tsview"
        Me.tsview.Size = New System.Drawing.Size(49, 33)
        Me.tsview.Text = "&View"
        Me.tsview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsview.ToolTipText = "View"
        '
        'tsref
        '
        Me.tsref.AutoSize = False
        Me.tsref.ForeColor = System.Drawing.Color.Black
        Me.tsref.Image = CType(resources.GetObject("tsref.Image"), System.Drawing.Image)
        Me.tsref.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsref.Name = "tsref"
        Me.tsref.Size = New System.Drawing.Size(49, 33)
        Me.tsref.Text = "&Refresh"
        Me.tsref.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsref.ToolTipText = "Refresh Data"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 36)
        '
        'ToolStripLabel3
        '
        Me.ToolStripLabel3.ForeColor = System.Drawing.Color.Black
        Me.ToolStripLabel3.Name = "ToolStripLabel3"
        Me.ToolStripLabel3.Size = New System.Drawing.Size(36, 33)
        Me.ToolStripLabel3.Text = "Cari  :"
        '
        'tcbofind
        '
        Me.tcbofind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tcbofind.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.tcbofind.Items.AddRange(New Object() {"No Bukti", "Tanggal", "Gudang", "Ket"})
        Me.tcbofind.Name = "tcbofind"
        Me.tcbofind.Size = New System.Drawing.Size(125, 36)
        Me.tcbofind.ToolTipText = "Kriteria pencarian"
        '
        'ToolStripButton9
        '
        Me.ToolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton9.Image = CType(resources.GetObject("ToolStripButton9.Image"), System.Drawing.Image)
        Me.ToolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton9.Name = "ToolStripButton9"
        Me.ToolStripButton9.Size = New System.Drawing.Size(23, 33)
        Me.ToolStripButton9.Text = "ToolStripButton1"
        Me.ToolStripButton9.ToolTipText = "-"
        '
        'tfind
        '
        Me.tfind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tfind.Name = "tfind"
        Me.tfind.Size = New System.Drawing.Size(116, 36)
        Me.tfind.ToolTipText = "Data yang akan dicari"
        '
        'tsfind
        '
        Me.tsfind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsfind.Image = CType(resources.GetObject("tsfind.Image"), System.Drawing.Image)
        Me.tsfind.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsfind.Name = "tsfind"
        Me.tsfind.Size = New System.Drawing.Size(23, 33)
        Me.tsfind.Text = "&Cari"
        Me.tsfind.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsfind.ToolTipText = "Proses pencarian"
        '
        'grid1
        '
        Me.grid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grid1.Location = New System.Drawing.Point(0, 36)
        Me.grid1.MainView = Me.GridView1
        Me.grid1.Name = "grid1"
        Me.grid1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.stkirim, Me.stpulang, Me.stbatal})
        Me.grid1.Size = New System.Drawing.Size(913, 360)
        Me.grid1.TabIndex = 140
        Me.grid1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ckode, Me.cnama, Me.GridColumn3, Me.GridColumn1, Me.GridColumn2, Me.GridColumn7})
        Me.GridView1.GridControl = Me.grid1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'ckode
        '
        Me.ckode.Caption = "No Bukti"
        Me.ckode.FieldName = "nobukti"
        Me.ckode.Name = "ckode"
        Me.ckode.OptionsColumn.AllowEdit = False
        Me.ckode.Visible = True
        Me.ckode.VisibleIndex = 0
        Me.ckode.Width = 86
        '
        'cnama
        '
        Me.cnama.Caption = "Tanggal"
        Me.cnama.FieldName = "tanggal"
        Me.cnama.Name = "cnama"
        Me.cnama.OptionsColumn.AllowEdit = False
        Me.cnama.Visible = True
        Me.cnama.VisibleIndex = 1
        Me.cnama.Width = 77
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Jenis"
        Me.GridColumn3.FieldName = "jenis"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Gudang"
        Me.GridColumn1.FieldName = "kd_gudang"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 3
        Me.GridColumn1.Width = 104
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Ket"
        Me.GridColumn2.FieldName = "note"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 4
        Me.GridColumn2.Width = 517
        '
        'GridColumn7
        '
        Me.GridColumn7.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn7.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn7.Caption = "Batal"
        Me.GridColumn7.ColumnEdit = Me.stbatal
        Me.GridColumn7.FieldName = "sbatal"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 5
        Me.GridColumn7.Width = 38
        '
        'stbatal
        '
        Me.stbatal.AutoHeight = False
        Me.stbatal.DisplayValueChecked = "1"
        Me.stbatal.DisplayValueUnchecked = "0"
        Me.stbatal.Name = "stbatal"
        Me.stbatal.ValueChecked = 1
        Me.stbatal.ValueUnchecked = 0
        '
        'stkirim
        '
        Me.stkirim.AutoHeight = False
        Me.stkirim.DisplayValueChecked = "1"
        Me.stkirim.DisplayValueUnchecked = "0"
        Me.stkirim.Name = "stkirim"
        Me.stkirim.ValueChecked = 1
        Me.stkirim.ValueUnchecked = 0
        '
        'stpulang
        '
        Me.stpulang.AutoHeight = False
        Me.stpulang.DisplayValueChecked = "1"
        Me.stpulang.DisplayValueUnchecked = "0"
        Me.stpulang.Name = "stpulang"
        Me.stpulang.ValueChecked = 1
        Me.stpulang.ValueUnchecked = 0
        '
        'fadjust_br
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(913, 396)
        Me.Controls.Add(Me.grid1)
        Me.Controls.Add(Me.bn1)
        Me.Name = "fadjust_br"
        Me.Text = "Adjustment Barang"
        CType(Me.bn1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.bn1.ResumeLayout(False)
        Me.bn1.PerformLayout()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.stbatal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.stkirim, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.stpulang, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents bn1 As System.Windows.Forms.BindingNavigator
    Friend WithEvents ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripTextBox1 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton4 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton5 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsadd As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsedit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsdel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsview As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsref As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabel3 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tcbofind As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripButton9 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tfind As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents tsfind As System.Windows.Forms.ToolStripButton
    Friend WithEvents grid1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ckode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cnama As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents stbatal As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents stkirim As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents stpulang As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
End Class
