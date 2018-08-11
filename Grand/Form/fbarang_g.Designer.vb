<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fbarang_g
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
        Me.grid2 = New DevExpress.XtraGrid.GridControl()
        Me.AdvBandedGridView1 = New DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView()
        Me.umum = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.cok = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.BandedGridColumn1 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.BandedGridColumn2 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.stokkomp = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.cst1 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.cst2 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.cst3 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.stokfisik = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.csf1 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.csf2 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.csf3 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.StokMobil = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.csk1 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.csk2 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.csk3 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ckode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cnama = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ctipe = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grid1 = New DevExpress.XtraGrid.GridControl()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btsave = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ComboBoxEdit1 = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.grid2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AdvBandedGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.ComboBoxEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grid2
        '
        Me.grid2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grid2.Location = New System.Drawing.Point(0, 0)
        Me.grid2.MainView = Me.AdvBandedGridView1
        Me.grid2.Name = "grid2"
        Me.grid2.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.grid2.Size = New System.Drawing.Size(797, 355)
        Me.grid2.TabIndex = 136
        Me.grid2.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.AdvBandedGridView1})
        '
        'AdvBandedGridView1
        '
        Me.AdvBandedGridView1.Bands.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.umum, Me.stokkomp, Me.stokfisik, Me.StokMobil})
        Me.AdvBandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {Me.BandedGridColumn1, Me.BandedGridColumn2, Me.cst1, Me.cst2, Me.cst3, Me.csf1, Me.csf2, Me.csf3, Me.csk1, Me.csk2, Me.csk3, Me.cok})
        Me.AdvBandedGridView1.GridControl = Me.grid2
        Me.AdvBandedGridView1.Name = "AdvBandedGridView1"
        Me.AdvBandedGridView1.OptionsView.ShowGroupPanel = False
        '
        'umum
        '
        Me.umum.Columns.Add(Me.cok)
        Me.umum.Columns.Add(Me.BandedGridColumn1)
        Me.umum.Columns.Add(Me.BandedGridColumn2)
        Me.umum.Name = "umum"
        Me.umum.Width = 294
        '
        'cok
        '
        Me.cok.AppearanceCell.Options.UseTextOptions = True
        Me.cok.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cok.AppearanceHeader.Options.UseTextOptions = True
        Me.cok.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cok.Caption = "Ok?"
        Me.cok.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.cok.FieldName = "ok"
        Me.cok.Name = "cok"
        Me.cok.Visible = True
        Me.cok.Width = 37
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.DisplayValueChecked = "1"
        Me.RepositoryItemCheckEdit1.DisplayValueUnchecked = "0"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = 1
        Me.RepositoryItemCheckEdit1.ValueUnchecked = 0
        '
        'BandedGridColumn1
        '
        Me.BandedGridColumn1.Caption = "Kode"
        Me.BandedGridColumn1.FieldName = "kd_barang"
        Me.BandedGridColumn1.Name = "BandedGridColumn1"
        Me.BandedGridColumn1.OptionsColumn.AllowEdit = False
        Me.BandedGridColumn1.Visible = True
        Me.BandedGridColumn1.Width = 54
        '
        'BandedGridColumn2
        '
        Me.BandedGridColumn2.Caption = "Nama"
        Me.BandedGridColumn2.FieldName = "nama_barang"
        Me.BandedGridColumn2.Name = "BandedGridColumn2"
        Me.BandedGridColumn2.OptionsColumn.AllowEdit = False
        Me.BandedGridColumn2.Visible = True
        Me.BandedGridColumn2.Width = 203
        '
        'stokkomp
        '
        Me.stokkomp.AppearanceHeader.Options.UseTextOptions = True
        Me.stokkomp.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.stokkomp.Caption = "Stok Komputer"
        Me.stokkomp.Columns.Add(Me.cst1)
        Me.stokkomp.Columns.Add(Me.cst2)
        Me.stokkomp.Columns.Add(Me.cst3)
        Me.stokkomp.Name = "stokkomp"
        Me.stokkomp.Width = 152
        '
        'cst1
        '
        Me.cst1.AppearanceCell.Options.UseTextOptions = True
        Me.cst1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cst1.AppearanceHeader.Options.UseTextOptions = True
        Me.cst1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cst1.Caption = "-1-"
        Me.cst1.FieldName = "jmlstok1"
        Me.cst1.Name = "cst1"
        Me.cst1.OptionsColumn.AllowEdit = False
        Me.cst1.Visible = True
        Me.cst1.Width = 49
        '
        'cst2
        '
        Me.cst2.AppearanceCell.Options.UseTextOptions = True
        Me.cst2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cst2.AppearanceHeader.Options.UseTextOptions = True
        Me.cst2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cst2.Caption = "-2-"
        Me.cst2.FieldName = "jmlstok2"
        Me.cst2.Name = "cst2"
        Me.cst2.OptionsColumn.AllowEdit = False
        Me.cst2.Visible = True
        Me.cst2.Width = 49
        '
        'cst3
        '
        Me.cst3.AppearanceCell.Options.UseTextOptions = True
        Me.cst3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cst3.AppearanceHeader.Options.UseTextOptions = True
        Me.cst3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cst3.Caption = "-3-"
        Me.cst3.FieldName = "jmlstok3"
        Me.cst3.Name = "cst3"
        Me.cst3.OptionsColumn.AllowEdit = False
        Me.cst3.Visible = True
        Me.cst3.Width = 54
        '
        'stokfisik
        '
        Me.stokfisik.AppearanceHeader.Options.UseTextOptions = True
        Me.stokfisik.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.stokfisik.Caption = "Stok Fisik"
        Me.stokfisik.Columns.Add(Me.csf1)
        Me.stokfisik.Columns.Add(Me.csf2)
        Me.stokfisik.Columns.Add(Me.csf3)
        Me.stokfisik.Name = "stokfisik"
        Me.stokfisik.Width = 163
        '
        'csf1
        '
        Me.csf1.AppearanceCell.Options.UseTextOptions = True
        Me.csf1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.csf1.AppearanceHeader.Options.UseTextOptions = True
        Me.csf1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.csf1.Caption = "-1-"
        Me.csf1.FieldName = "jmlstok_f1"
        Me.csf1.Name = "csf1"
        Me.csf1.OptionsColumn.AllowEdit = False
        Me.csf1.Visible = True
        Me.csf1.Width = 52
        '
        'csf2
        '
        Me.csf2.AppearanceCell.Options.UseTextOptions = True
        Me.csf2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.csf2.AppearanceHeader.Options.UseTextOptions = True
        Me.csf2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.csf2.Caption = "-2-"
        Me.csf2.FieldName = "jmlstok_f2"
        Me.csf2.Name = "csf2"
        Me.csf2.OptionsColumn.AllowEdit = False
        Me.csf2.Visible = True
        Me.csf2.Width = 52
        '
        'csf3
        '
        Me.csf3.AppearanceCell.Options.UseTextOptions = True
        Me.csf3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.csf3.AppearanceHeader.Options.UseTextOptions = True
        Me.csf3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.csf3.Caption = "-3-"
        Me.csf3.FieldName = "jmlstok_f3"
        Me.csf3.Name = "csf3"
        Me.csf3.OptionsColumn.AllowEdit = False
        Me.csf3.Visible = True
        Me.csf3.Width = 59
        '
        'StokMobil
        '
        Me.StokMobil.AppearanceHeader.Options.UseTextOptions = True
        Me.StokMobil.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.StokMobil.Caption = "Stok Mobil"
        Me.StokMobil.Columns.Add(Me.csk1)
        Me.StokMobil.Columns.Add(Me.csk2)
        Me.StokMobil.Columns.Add(Me.csk3)
        Me.StokMobil.Name = "StokMobil"
        Me.StokMobil.Width = 166
        '
        'csk1
        '
        Me.csk1.AppearanceCell.Options.UseTextOptions = True
        Me.csk1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.csk1.AppearanceHeader.Options.UseTextOptions = True
        Me.csk1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.csk1.Caption = "-1-"
        Me.csk1.FieldName = "jmlstok_k1"
        Me.csk1.Name = "csk1"
        Me.csk1.OptionsColumn.AllowEdit = False
        Me.csk1.Visible = True
        Me.csk1.Width = 53
        '
        'csk2
        '
        Me.csk2.AppearanceCell.Options.UseTextOptions = True
        Me.csk2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.csk2.AppearanceHeader.Options.UseTextOptions = True
        Me.csk2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.csk2.Caption = "-2-"
        Me.csk2.FieldName = "jmlstok_k2"
        Me.csk2.Name = "csk2"
        Me.csk2.OptionsColumn.AllowEdit = False
        Me.csk2.Visible = True
        Me.csk2.Width = 53
        '
        'csk3
        '
        Me.csk3.AppearanceCell.Options.UseTextOptions = True
        Me.csk3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.csk3.AppearanceHeader.Options.UseTextOptions = True
        Me.csk3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.csk3.Caption = "-3-"
        Me.csk3.FieldName = "jmlstok_k3"
        Me.csk3.Name = "csk3"
        Me.csk3.OptionsColumn.AllowEdit = False
        Me.csk3.Visible = True
        Me.csk3.Width = 60
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ckode, Me.cnama, Me.ctipe})
        Me.GridView1.GridControl = Me.grid1
        Me.GridView1.GroupCount = 1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.AutoExpandAllGroups = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.ctipe, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'ckode
        '
        Me.ckode.Caption = "Kode"
        Me.ckode.FieldName = "kd_gudang"
        Me.ckode.Name = "ckode"
        Me.ckode.OptionsColumn.AllowEdit = False
        Me.ckode.Visible = True
        Me.ckode.VisibleIndex = 0
        Me.ckode.Width = 72
        '
        'cnama
        '
        Me.cnama.Caption = "Nama"
        Me.cnama.FieldName = "nama_gudang"
        Me.cnama.Name = "cnama"
        Me.cnama.OptionsColumn.AllowEdit = False
        Me.cnama.Visible = True
        Me.cnama.VisibleIndex = 1
        Me.cnama.Width = 226
        '
        'ctipe
        '
        Me.ctipe.Caption = "Tipe"
        Me.ctipe.FieldName = "tipe_gudang"
        Me.ctipe.Name = "ctipe"
        Me.ctipe.OptionsColumn.AllowEdit = False
        Me.ctipe.Visible = True
        Me.ctipe.VisibleIndex = 0
        Me.ctipe.Width = 59
        '
        'grid1
        '
        Me.grid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grid1.Location = New System.Drawing.Point(0, 0)
        Me.grid1.MainView = Me.GridView1
        Me.grid1.Name = "grid1"
        Me.grid1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit3})
        Me.grid1.Size = New System.Drawing.Size(293, 407)
        Me.grid1.TabIndex = 136
        Me.grid1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1, Me.GridView2})
        '
        'RepositoryItemCheckEdit3
        '
        Me.RepositoryItemCheckEdit3.AutoHeight = False
        Me.RepositoryItemCheckEdit3.DisplayValueChecked = "1"
        Me.RepositoryItemCheckEdit3.DisplayValueUnchecked = "0"
        Me.RepositoryItemCheckEdit3.Name = "RepositoryItemCheckEdit3"
        Me.RepositoryItemCheckEdit3.ValueChecked = 1
        Me.RepositoryItemCheckEdit3.ValueUnchecked = 0
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.grid1
        Me.GridView2.Name = "GridView2"
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.SplitContainerControl1.Panel1.CaptionLocation = DevExpress.Utils.Locations.Left
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.grid1)
        Me.SplitContainerControl1.Panel1.ShowCaption = True
        Me.SplitContainerControl1.Panel1.Text = "Gudang"
        Me.SplitContainerControl1.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.SplitContainerControl1.Panel2.CaptionLocation = DevExpress.Utils.Locations.Bottom
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.btclose)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.btsave)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.PanelControl1)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.grid2)
        Me.SplitContainerControl1.Panel2.ShowCaption = True
        Me.SplitContainerControl1.Panel2.Text = "Barang"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1123, 411)
        Me.SplitContainerControl1.SplitterPosition = 316
        Me.SplitContainerControl1.TabIndex = 0
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'btclose
        '
        Me.btclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btclose.Location = New System.Drawing.Point(706, 358)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(82, 25)
        Me.btclose.TabIndex = 139
        Me.btclose.Text = "&Keluar"
        '
        'btsave
        '
        Me.btsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btsave.Location = New System.Drawing.Point(617, 358)
        Me.btsave.Name = "btsave"
        Me.btsave.Size = New System.Drawing.Size(83, 25)
        Me.btsave.TabIndex = 138
        Me.btsave.Text = "&Simpan"
        '
        'PanelControl1
        '
        Me.PanelControl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelControl1.Controls.Add(Me.Label4)
        Me.PanelControl1.Controls.Add(Me.ComboBoxEdit1)
        Me.PanelControl1.Location = New System.Drawing.Point(624, 3)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(170, 30)
        Me.PanelControl1.TabIndex = 137
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(5, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Option :"
        '
        'ComboBoxEdit1
        '
        Me.ComboBoxEdit1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxEdit1.Location = New System.Drawing.Point(57, 5)
        Me.ComboBoxEdit1.Name = "ComboBoxEdit1"
        Me.ComboBoxEdit1.Properties.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[False]
        Me.ComboBoxEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ComboBoxEdit1.Properties.Items.AddRange(New Object() {"-Pilih opsi Cepat-", "Cek All", "Un-Check All"})
        Me.ComboBoxEdit1.Size = New System.Drawing.Size(105, 20)
        Me.ComboBoxEdit1.TabIndex = 1
        '
        'fbarang_g
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1123, 411)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Name = "fbarang_g"
        Me.Text = "Barang By Gudang"
        CType(Me.grid2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AdvBandedGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.ComboBoxEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grid2 As DevExpress.XtraGrid.GridControl
    Friend WithEvents AdvBandedGridView1 As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
    Friend WithEvents BandedGridColumn1 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents BandedGridColumn2 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents cst1 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents cst2 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents cst3 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents csf1 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents csf2 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents csf3 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents csk1 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents csk2 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents csk3 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ckode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cnama As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ctipe As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grid1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxEdit1 As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cok As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents btsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents umum As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents stokkomp As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents stokfisik As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents StokMobil As DevExpress.XtraGrid.Views.BandedGrid.GridBand
End Class
