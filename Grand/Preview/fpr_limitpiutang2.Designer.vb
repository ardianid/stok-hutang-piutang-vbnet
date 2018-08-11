<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fpr_limitpiutang2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fpr_limitpiutang2))
        Me.grid1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ckd_sales = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cnamasales = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ckdtoko = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cnamaoputlet = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.climit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cpiut = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.csisa = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.tsprint = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bn1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.bn1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grid1
        '
        Me.grid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grid1.Location = New System.Drawing.Point(0, 36)
        Me.grid1.MainView = Me.GridView1
        Me.grid1.Name = "grid1"
        Me.grid1.Size = New System.Drawing.Size(867, 258)
        Me.grid1.TabIndex = 1
        Me.grid1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ckd_sales, Me.cnamasales, Me.ckdtoko, Me.cnamaoputlet, Me.climit, Me.cpiut, Me.csisa})
        Me.GridView1.GridControl = Me.grid1
        Me.GridView1.GroupCount = 1
        Me.GridView1.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "limit_val", Me.climit, "{0:n0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "jmlpiutang", Me.cpiut, "{0:n0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "sisalimit", Me.csisa, "{0:n0}")})
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.AutoExpandAllGroups = True
        Me.GridView1.OptionsView.AllowCellMerge = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.cnamasales, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'ckd_sales
        '
        Me.ckd_sales.Caption = "Kd Sales"
        Me.ckd_sales.FieldName = "kd_karyawan"
        Me.ckd_sales.Name = "ckd_sales"
        Me.ckd_sales.OptionsColumn.AllowEdit = False
        Me.ckd_sales.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.ckd_sales.Width = 57
        '
        'cnamasales
        '
        Me.cnamasales.Caption = "Nama Sales"
        Me.cnamasales.FieldName = "nama_karyawan"
        Me.cnamasales.Name = "cnamasales"
        Me.cnamasales.OptionsColumn.AllowEdit = False
        Me.cnamasales.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cnamasales.Visible = True
        Me.cnamasales.VisibleIndex = 1
        Me.cnamasales.Width = 133
        '
        'ckdtoko
        '
        Me.ckdtoko.Caption = "Kd Outlet"
        Me.ckdtoko.FieldName = "kd_toko"
        Me.ckdtoko.Name = "ckdtoko"
        Me.ckdtoko.OptionsColumn.AllowEdit = False
        Me.ckdtoko.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.ckdtoko.Visible = True
        Me.ckdtoko.VisibleIndex = 0
        Me.ckdtoko.Width = 62
        '
        'cnamaoputlet
        '
        Me.cnamaoputlet.Caption = "Nama Outlet"
        Me.cnamaoputlet.FieldName = "nama_toko"
        Me.cnamaoputlet.Name = "cnamaoputlet"
        Me.cnamaoputlet.OptionsColumn.AllowEdit = False
        Me.cnamaoputlet.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cnamaoputlet.Visible = True
        Me.cnamaoputlet.VisibleIndex = 1
        Me.cnamaoputlet.Width = 195
        '
        'climit
        '
        Me.climit.AppearanceHeader.Options.UseTextOptions = True
        Me.climit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.climit.Caption = "Limit"
        Me.climit.DisplayFormat.FormatString = "n0"
        Me.climit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.climit.FieldName = "limit_val"
        Me.climit.GroupFormat.FormatString = "n0"
        Me.climit.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.climit.Name = "climit"
        Me.climit.OptionsColumn.AllowEdit = False
        Me.climit.Visible = True
        Me.climit.VisibleIndex = 2
        Me.climit.Width = 89
        '
        'cpiut
        '
        Me.cpiut.AppearanceHeader.Options.UseTextOptions = True
        Me.cpiut.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cpiut.Caption = "Piutang"
        Me.cpiut.DisplayFormat.FormatString = "n0"
        Me.cpiut.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cpiut.FieldName = "jmlpiutang"
        Me.cpiut.Name = "cpiut"
        Me.cpiut.OptionsColumn.AllowEdit = False
        Me.cpiut.Visible = True
        Me.cpiut.VisibleIndex = 3
        Me.cpiut.Width = 89
        '
        'csisa
        '
        Me.csisa.AppearanceHeader.Options.UseTextOptions = True
        Me.csisa.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.csisa.Caption = "Sisa Limit"
        Me.csisa.DisplayFormat.FormatString = "n0"
        Me.csisa.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.csisa.FieldName = "sisalimit"
        Me.csisa.Name = "csisa"
        Me.csisa.OptionsColumn.AllowEdit = False
        Me.csisa.Visible = True
        Me.csisa.VisibleIndex = 4
        Me.csisa.Width = 106
        '
        'bn1
        '
        Me.bn1.AddNewItem = Nothing
        Me.bn1.CountItem = Me.ToolStripLabel2
        Me.bn1.DeleteItem = Nothing
        Me.bn1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bn1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.bn1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton2, Me.ToolStripButton3, Me.ToolStripSeparator2, Me.ToolStripTextBox1, Me.ToolStripLabel2, Me.ToolStripSeparator3, Me.ToolStripButton4, Me.ToolStripButton5, Me.ToolStripSeparator4, Me.tsprint, Me.ToolStripSeparator7})
        Me.bn1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.bn1.Location = New System.Drawing.Point(0, 0)
        Me.bn1.MoveFirstItem = Me.ToolStripButton2
        Me.bn1.MoveLastItem = Me.ToolStripButton5
        Me.bn1.MoveNextItem = Me.ToolStripButton4
        Me.bn1.MovePreviousItem = Me.ToolStripButton3
        Me.bn1.Name = "bn1"
        Me.bn1.PositionItem = Me.ToolStripTextBox1
        Me.bn1.Size = New System.Drawing.Size(867, 36)
        Me.bn1.TabIndex = 136
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
        'tsprint
        '
        Me.tsprint.AutoSize = False
        Me.tsprint.ForeColor = System.Drawing.Color.Black
        Me.tsprint.Image = CType(resources.GetObject("tsprint.Image"), System.Drawing.Image)
        Me.tsprint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsprint.Name = "tsprint"
        Me.tsprint.Size = New System.Drawing.Size(49, 33)
        Me.tsprint.Text = "&Print"
        Me.tsprint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsprint.ToolTipText = "Print Faktur"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 36)
        '
        'fpr_limitpiutang2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(867, 294)
        Me.Controls.Add(Me.grid1)
        Me.Controls.Add(Me.bn1)
        Me.Name = "fpr_limitpiutang2"
        Me.Text = "Limit Piutang Sales by Outlet"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bn1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.bn1.ResumeLayout(False)
        Me.bn1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grid1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ckd_sales As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cnamasales As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ckdtoko As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cnamaoputlet As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents climit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cpiut As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents csisa As DevExpress.XtraGrid.Columns.GridColumn
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
    Friend WithEvents tsprint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
End Class
