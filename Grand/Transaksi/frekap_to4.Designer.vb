<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frekap_to4
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
        Me.tjalur = New DevExpress.XtraEditors.TextEdit()
        Me.grid1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cnobuk = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ctanggal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ctoko = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.calamat = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.csales = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cnetto = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cok = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rok = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ComboBoxEdit1 = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.tkode = New DevExpress.XtraEditors.TextEdit()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grid2 = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ComboBoxEdit2 = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.tjalur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rok, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ComboBoxEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tkode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        Me.XtraTabPage2.SuspendLayout()
        CType(Me.grid2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ComboBoxEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(414, 15)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(55, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Jalur Kirim :"
        '
        'tjalur
        '
        Me.tjalur.Enabled = False
        Me.tjalur.Location = New System.Drawing.Point(520, 12)
        Me.tjalur.Name = "tjalur"
        Me.tjalur.Size = New System.Drawing.Size(247, 20)
        Me.tjalur.TabIndex = 1
        '
        'grid1
        '
        Me.grid1.Location = New System.Drawing.Point(3, 29)
        Me.grid1.MainView = Me.GridView1
        Me.grid1.Name = "grid1"
        Me.grid1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rok})
        Me.grid1.Size = New System.Drawing.Size(751, 327)
        Me.grid1.TabIndex = 1
        Me.grid1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cnobuk, Me.ctanggal, Me.ctoko, Me.calamat, Me.csales, Me.cnetto, Me.cok})
        Me.GridView1.GridControl = Me.grid1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'cnobuk
        '
        Me.cnobuk.Caption = "No Bukti"
        Me.cnobuk.FieldName = "nobukti"
        Me.cnobuk.Name = "cnobuk"
        Me.cnobuk.OptionsColumn.AllowEdit = False
        Me.cnobuk.Visible = True
        Me.cnobuk.VisibleIndex = 1
        Me.cnobuk.Width = 76
        '
        'ctanggal
        '
        Me.ctanggal.Caption = "Tanggal"
        Me.ctanggal.FieldName = "tanggal"
        Me.ctanggal.Name = "ctanggal"
        Me.ctanggal.OptionsColumn.AllowEdit = False
        Me.ctanggal.Visible = True
        Me.ctanggal.VisibleIndex = 2
        Me.ctanggal.Width = 77
        '
        'ctoko
        '
        Me.ctoko.Caption = "Outlet"
        Me.ctoko.FieldName = "nama_toko"
        Me.ctoko.Name = "ctoko"
        Me.ctoko.OptionsColumn.AllowEdit = False
        Me.ctoko.Visible = True
        Me.ctoko.VisibleIndex = 4
        Me.ctoko.Width = 99
        '
        'calamat
        '
        Me.calamat.Caption = "Alamat"
        Me.calamat.FieldName = "alamat_toko"
        Me.calamat.Name = "calamat"
        Me.calamat.OptionsColumn.AllowEdit = False
        Me.calamat.Visible = True
        Me.calamat.VisibleIndex = 5
        Me.calamat.Width = 244
        '
        'csales
        '
        Me.csales.Caption = "Sales"
        Me.csales.FieldName = "nama_karyawan"
        Me.csales.Name = "csales"
        Me.csales.OptionsColumn.AllowEdit = False
        Me.csales.Visible = True
        Me.csales.VisibleIndex = 3
        Me.csales.Width = 50
        '
        'cnetto
        '
        Me.cnetto.AppearanceCell.Options.UseTextOptions = True
        Me.cnetto.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cnetto.AppearanceHeader.Options.UseTextOptions = True
        Me.cnetto.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cnetto.Caption = "Netto"
        Me.cnetto.DisplayFormat.FormatString = "n0"
        Me.cnetto.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cnetto.FieldName = "netto"
        Me.cnetto.Name = "cnetto"
        Me.cnetto.OptionsColumn.AllowEdit = False
        Me.cnetto.Visible = True
        Me.cnetto.VisibleIndex = 6
        Me.cnetto.Width = 77
        '
        'cok
        '
        Me.cok.AppearanceCell.Options.UseTextOptions = True
        Me.cok.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cok.AppearanceHeader.Options.UseTextOptions = True
        Me.cok.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cok.Caption = "Add?"
        Me.cok.ColumnEdit = Me.rok
        Me.cok.FieldName = "ok"
        Me.cok.Name = "cok"
        Me.cok.Visible = True
        Me.cok.VisibleIndex = 0
        Me.cok.Width = 35
        '
        'rok
        '
        Me.rok.AutoHeight = False
        Me.rok.DisplayValueChecked = "1"
        Me.rok.DisplayValueUnchecked = "0"
        Me.rok.Name = "rok"
        Me.rok.ValueChecked = 1
        Me.rok.ValueUnchecked = 0
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(590, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 186
        Me.Label4.Text = "Option :"
        '
        'ComboBoxEdit1
        '
        Me.ComboBoxEdit1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxEdit1.Location = New System.Drawing.Point(642, 3)
        Me.ComboBoxEdit1.Name = "ComboBoxEdit1"
        Me.ComboBoxEdit1.Properties.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[False]
        Me.ComboBoxEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ComboBoxEdit1.Properties.Items.AddRange(New Object() {"-Pilih opsi Cepat-", "Cek All", "Un-Check All"})
        Me.ComboBoxEdit1.Size = New System.Drawing.Size(108, 20)
        Me.ComboBoxEdit1.TabIndex = 0
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(640, 405)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(61, 23)
        Me.btsimpan.TabIndex = 2
        Me.btsimpan.Text = "&OK"
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(707, 405)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(61, 23)
        Me.btclose.TabIndex = 3
        Me.btclose.Text = "&Close"
        '
        'tkode
        '
        Me.tkode.Enabled = False
        Me.tkode.Location = New System.Drawing.Point(475, 12)
        Me.tkode.Name = "tkode"
        Me.tkode.Size = New System.Drawing.Size(42, 20)
        Me.tkode.TabIndex = 189
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Location = New System.Drawing.Point(13, 12)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.XtraTabPage1
        Me.XtraTabControl1.Size = New System.Drawing.Size(759, 392)
        Me.XtraTabControl1.TabIndex = 190
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.XtraTabPage2})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.grid1)
        Me.XtraTabPage1.Controls.Add(Me.ComboBoxEdit1)
        Me.XtraTabPage1.Controls.Add(Me.Label4)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(753, 364)
        Me.XtraTabPage1.Text = "Belum Dikirim"
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.Label1)
        Me.XtraTabPage2.Controls.Add(Me.grid2)
        Me.XtraTabPage2.Controls.Add(Me.ComboBoxEdit2)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(753, 364)
        Me.XtraTabPage2.Text = "Kirim Ulang"
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(590, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 187
        Me.Label1.Text = "Option :"
        '
        'grid2
        '
        Me.grid2.Location = New System.Drawing.Point(3, 29)
        Me.grid2.MainView = Me.GridView2
        Me.grid2.Name = "grid2"
        Me.grid2.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.grid2.Size = New System.Drawing.Size(751, 327)
        Me.grid2.TabIndex = 3
        Me.grid2.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'GridView2
        '
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7})
        Me.GridView2.GridControl = Me.grid2
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsFind.AlwaysVisible = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "No Bukti"
        Me.GridColumn1.FieldName = "nobukti"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        Me.GridColumn1.Width = 76
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Tanggal"
        Me.GridColumn2.FieldName = "tanggal"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 2
        Me.GridColumn2.Width = 77
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Outlet"
        Me.GridColumn3.FieldName = "nama_toko"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 4
        Me.GridColumn3.Width = 99
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Alamat"
        Me.GridColumn4.FieldName = "alamat_toko"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 5
        Me.GridColumn4.Width = 244
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Sales"
        Me.GridColumn5.FieldName = "nama_karyawan"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 3
        Me.GridColumn5.Width = 50
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn6.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn6.Caption = "Netto"
        Me.GridColumn6.DisplayFormat.FormatString = "n0"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn6.FieldName = "netto"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 6
        Me.GridColumn6.Width = 77
        '
        'GridColumn7
        '
        Me.GridColumn7.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn7.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn7.Caption = "Add?"
        Me.GridColumn7.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.GridColumn7.FieldName = "ok"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        Me.GridColumn7.Width = 35
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
        'ComboBoxEdit2
        '
        Me.ComboBoxEdit2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxEdit2.Location = New System.Drawing.Point(642, 3)
        Me.ComboBoxEdit2.Name = "ComboBoxEdit2"
        Me.ComboBoxEdit2.Properties.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[False]
        Me.ComboBoxEdit2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ComboBoxEdit2.Properties.Items.AddRange(New Object() {"-Pilih opsi Cepat-", "Cek All", "Un-Check All"})
        Me.ComboBoxEdit2.Size = New System.Drawing.Size(108, 20)
        Me.ComboBoxEdit2.TabIndex = 2
        '
        'frekap_to4
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(782, 434)
        Me.Controls.Add(Me.tkode)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.tjalur)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frekap_to4"
        Me.Text = "Load Faktur By Jalur"
        CType(Me.tjalur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rok, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ComboBoxEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tkode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        Me.XtraTabPage1.PerformLayout()
        Me.XtraTabPage2.ResumeLayout(False)
        Me.XtraTabPage2.PerformLayout()
        CType(Me.grid2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ComboBoxEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tjalur As DevExpress.XtraEditors.TextEdit
    Friend WithEvents grid1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cnobuk As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ctanggal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ctoko As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents calamat As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents csales As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cnetto As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxEdit1 As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cok As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rok As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tkode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grid2 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ComboBoxEdit2 As DevExpress.XtraEditors.ComboBoxEdit
End Class
