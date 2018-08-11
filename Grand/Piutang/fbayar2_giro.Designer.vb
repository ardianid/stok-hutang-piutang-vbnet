<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fbayar2_giro
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fbayar2_giro))
        Me.grid1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btfind_giro = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LabelControl15 = New DevExpress.XtraEditors.LabelControl()
        Me.ttoko = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.talamat = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tfaktur = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btdel_giro = New DevExpress.XtraEditors.SimpleButton()
        Me.tjumlah = New DevExpress.XtraEditors.TextEdit()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btfind_giro, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttoko.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.talamat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tfaktur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tjumlah.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grid1
        '
        Me.grid1.Dock = System.Windows.Forms.DockStyle.Top
        Me.grid1.Location = New System.Drawing.Point(0, 0)
        Me.grid1.MainView = Me.GridView1
        Me.grid1.Name = "grid1"
        Me.grid1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.btfind_giro})
        Me.grid1.Size = New System.Drawing.Size(674, 219)
        Me.grid1.TabIndex = 0
        Me.grid1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn3, Me.GridColumn1, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7})
        Me.GridView1.GridControl = Me.grid1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.GridView1.OptionsView.ShowFooter = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Tanggal"
        Me.GridColumn2.FieldName = "tgl_giro"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 86
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Bank"
        Me.GridColumn3.FieldName = "namabank"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 83
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "No Giro"
        Me.GridColumn1.ColumnEdit = Me.btfind_giro
        Me.GridColumn1.FieldName = "nogiro"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 107
        '
        'btfind_giro
        '
        Me.btfind_giro.AutoHeight = False
        Me.btfind_giro.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.btfind_giro.Name = "btfind_giro"
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Sales/Kolektor"
        Me.GridColumn4.FieldName = "nama_karyawan"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 107
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn5.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn5.Caption = "Jml Giro"
        Me.GridColumn5.DisplayFormat.FormatString = "n0"
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn5.FieldName = "jumlah"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "jumlah", "{0:n0}")})
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 79
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn6.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn6.Caption = "Jml Bayar"
        Me.GridColumn6.DisplayFormat.FormatString = "n0"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn6.FieldName = "jmlgiro"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "jmlgiro", "{0:n0}")})
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 6
        Me.GridColumn6.Width = 102
        '
        'GridColumn7
        '
        Me.GridColumn7.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn7.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn7.Caption = "Terpakai"
        Me.GridColumn7.DisplayFormat.FormatString = "c0"
        Me.GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn7.FieldName = "jmlpakai"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "jmlpakai", "{0:n0}")})
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 5
        Me.GridColumn7.Width = 92
        '
        'LabelControl15
        '
        Me.LabelControl15.Location = New System.Drawing.Point(41, 231)
        Me.LabelControl15.Name = "LabelControl15"
        Me.LabelControl15.Size = New System.Drawing.Size(37, 13)
        Me.LabelControl15.TabIndex = 166
        Me.LabelControl15.Text = "Outlet :"
        '
        'ttoko
        '
        Me.ttoko.Location = New System.Drawing.Point(84, 228)
        Me.ttoko.Name = "ttoko"
        Me.ttoko.Properties.ReadOnly = True
        Me.ttoko.Size = New System.Drawing.Size(250, 20)
        Me.ttoko.TabIndex = 167
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(38, 257)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(40, 13)
        Me.LabelControl1.TabIndex = 168
        Me.LabelControl1.Text = "Alamat :"
        '
        'talamat
        '
        Me.talamat.Location = New System.Drawing.Point(84, 254)
        Me.talamat.Name = "talamat"
        Me.talamat.Properties.ReadOnly = True
        Me.talamat.Size = New System.Drawing.Size(250, 35)
        Me.talamat.TabIndex = 169
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(24, 298)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(54, 13)
        Me.LabelControl2.TabIndex = 170
        Me.LabelControl2.Text = "No Faktur :"
        '
        'tfaktur
        '
        Me.tfaktur.Location = New System.Drawing.Point(84, 295)
        Me.tfaktur.Name = "tfaktur"
        Me.tfaktur.Properties.ReadOnly = True
        Me.tfaktur.Size = New System.Drawing.Size(250, 20)
        Me.tfaktur.TabIndex = 171
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(33, 324)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(45, 13)
        Me.LabelControl3.TabIndex = 172
        Me.LabelControl3.Text = "Tanggal :"
        '
        'ttgl
        '
        Me.ttgl.Location = New System.Drawing.Point(84, 321)
        Me.ttgl.Name = "ttgl"
        Me.ttgl.Properties.ReadOnly = True
        Me.ttgl.Size = New System.Drawing.Size(87, 20)
        Me.ttgl.TabIndex = 173
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(187, 324)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(40, 13)
        Me.LabelControl4.TabIndex = 174
        Me.LabelControl4.Text = "Jumlah :"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(480, 306)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(69, 31)
        Me.btsimpan.TabIndex = 1
        Me.btsimpan.Text = "&Simpan"
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(555, 306)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(69, 31)
        Me.btclose.TabIndex = 2
        Me.btclose.Text = "&Keluar"
        '
        'btdel_giro
        '
        Me.btdel_giro.Image = CType(resources.GetObject("btdel_giro.Image"), System.Drawing.Image)
        Me.btdel_giro.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight
        Me.btdel_giro.Location = New System.Drawing.Point(585, 225)
        Me.btdel_giro.Name = "btdel_giro"
        Me.btdel_giro.Size = New System.Drawing.Size(84, 23)
        Me.btdel_giro.TabIndex = 176
        Me.btdel_giro.Text = "Hapus Giro"
        '
        'tjumlah
        '
        Me.tjumlah.EditValue = "0"
        Me.tjumlah.Location = New System.Drawing.Point(234, 321)
        Me.tjumlah.Name = "tjumlah"
        Me.tjumlah.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tjumlah.Properties.Appearance.Options.UseFont = True
        Me.tjumlah.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tjumlah.Properties.Mask.EditMask = "n0"
        Me.tjumlah.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tjumlah.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tjumlah.Properties.ReadOnly = True
        Me.tjumlah.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tjumlah.Size = New System.Drawing.Size(100, 20)
        Me.tjumlah.TabIndex = 177
        '
        'fbayar2_giro
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(674, 350)
        Me.Controls.Add(Me.tjumlah)
        Me.Controls.Add(Me.btdel_giro)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.ttgl)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.tfaktur)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.ttoko)
        Me.Controls.Add(Me.LabelControl15)
        Me.Controls.Add(Me.grid1)
        Me.Controls.Add(Me.talamat)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fbayar2_giro"
        Me.Text = "Giro"
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btfind_giro, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttoko.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.talamat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tfaktur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tjumlah.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grid1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LabelControl15 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttoko As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents talamat As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tfaktur As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btdel_giro As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tjumlah As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btfind_giro As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
End Class
