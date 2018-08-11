<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fbayar_sw2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fbayar_sw2))
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl = New DevExpress.XtraEditors.DateEdit()
        Me.tbukti = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl15 = New DevExpress.XtraEditors.LabelControl()
        Me.tnama_supir = New DevExpress.XtraEditors.TextEdit()
        Me.tkd_supir = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.bts_supir = New DevExpress.XtraEditors.SimpleButton()
        Me.talamat = New DevExpress.XtraEditors.MemoEdit()
        Me.grid1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.tnote = New DevExpress.XtraEditors.TextEdit()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.btadd = New DevExpress.XtraEditors.SimpleButton()
        Me.btdel = New DevExpress.XtraEditors.SimpleButton()
        Me.btedit = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ttgl.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbukti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnama_supir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tkd_supir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.talamat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(19, 51)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(45, 13)
        Me.LabelControl4.TabIndex = 159
        Me.LabelControl4.Text = "Tanggal :"
        '
        'ttgl
        '
        Me.ttgl.EditValue = Nothing
        Me.ttgl.Location = New System.Drawing.Point(70, 48)
        Me.ttgl.Name = "ttgl"
        Me.ttgl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl.Properties.Mask.EditMask = ""
        Me.ttgl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl.Size = New System.Drawing.Size(104, 20)
        Me.ttgl.TabIndex = 1
        '
        'tbukti
        '
        Me.tbukti.Location = New System.Drawing.Point(70, 22)
        Me.tbukti.Name = "tbukti"
        Me.tbukti.Properties.ReadOnly = True
        Me.tbukti.Size = New System.Drawing.Size(104, 20)
        Me.tbukti.TabIndex = 0
        '
        'LabelControl15
        '
        Me.LabelControl15.Location = New System.Drawing.Point(21, 25)
        Me.LabelControl15.Name = "LabelControl15"
        Me.LabelControl15.Size = New System.Drawing.Size(43, 13)
        Me.LabelControl15.TabIndex = 158
        Me.LabelControl15.Text = "No Bukti:"
        '
        'tnama_supir
        '
        Me.tnama_supir.Enabled = False
        Me.tnama_supir.Location = New System.Drawing.Point(357, 22)
        Me.tnama_supir.Name = "tnama_supir"
        Me.tnama_supir.Size = New System.Drawing.Size(179, 20)
        Me.tnama_supir.TabIndex = 167
        '
        'tkd_supir
        '
        Me.tkd_supir.Location = New System.Drawing.Point(311, 22)
        Me.tkd_supir.Name = "tkd_supir"
        Me.tkd_supir.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tkd_supir.Size = New System.Drawing.Size(44, 20)
        Me.tkd_supir.TabIndex = 2
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(268, 25)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(37, 13)
        Me.LabelControl2.TabIndex = 168
        Me.LabelControl2.Text = "Outlet :"
        '
        'bts_supir
        '
        Me.bts_supir.Appearance.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bts_supir.Appearance.Options.UseFont = True
        Me.bts_supir.Image = Global.Grand.My.Resources.Resources._1389260518_xmag
        Me.bts_supir.Location = New System.Drawing.Point(542, 21)
        Me.bts_supir.Name = "bts_supir"
        Me.bts_supir.Size = New System.Drawing.Size(25, 21)
        Me.bts_supir.TabIndex = 3
        '
        'talamat
        '
        Me.talamat.Location = New System.Drawing.Point(311, 49)
        Me.talamat.Name = "talamat"
        Me.talamat.Properties.ReadOnly = True
        Me.talamat.Size = New System.Drawing.Size(256, 41)
        Me.talamat.TabIndex = 169
        '
        'grid1
        '
        Me.grid1.Location = New System.Drawing.Point(5, 5)
        Me.grid1.MainView = Me.GridView1
        Me.grid1.Name = "grid1"
        Me.grid1.Size = New System.Drawing.Size(559, 188)
        Me.grid1.TabIndex = 0
        Me.grid1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn6, Me.GridColumn9})
        Me.GridView1.GridControl = Me.grid1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridView1.OptionsView.ShowFooter = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "No Sewa"
        Me.GridColumn1.FieldName = "nobukti_sw"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 113
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Tgl Sewa"
        Me.GridColumn2.FieldName = "tanggal"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 171
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn6.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn6.Caption = "Jumlah"
        Me.GridColumn6.DisplayFormat.FormatString = "n0"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn6.FieldName = "netto"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 2
        Me.GridColumn6.Width = 111
        '
        'GridColumn9
        '
        Me.GridColumn9.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn9.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn9.Caption = "Jml Bayar"
        Me.GridColumn9.DisplayFormat.FormatString = "n0"
        Me.GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn9.FieldName = "jumlah"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "jumlah", "{0:n0}")})
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 3
        Me.GridColumn9.Width = 146
        '
        'LabelControl14
        '
        Me.LabelControl14.Location = New System.Drawing.Point(15, 347)
        Me.LabelControl14.Name = "LabelControl14"
        Me.LabelControl14.Size = New System.Drawing.Size(23, 13)
        Me.LabelControl14.TabIndex = 172
        Me.LabelControl14.Text = "Ket :"
        '
        'tnote
        '
        Me.tnote.Location = New System.Drawing.Point(44, 347)
        Me.tnote.Name = "tnote"
        Me.tnote.Size = New System.Drawing.Size(540, 20)
        Me.tnote.TabIndex = 5
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(509, 384)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 25)
        Me.btclose.TabIndex = 7
        Me.btclose.Text = "&Keluar"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(428, 384)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 25)
        Me.btsimpan.TabIndex = 6
        Me.btsimpan.Text = "&Simpan"
        '
        'GroupControl2
        '
        Me.GroupControl2.CaptionImage = CType(resources.GetObject("GroupControl2.CaptionImage"), System.Drawing.Image)
        Me.GroupControl2.CaptionLocation = DevExpress.Utils.Locations.Bottom
        Me.GroupControl2.Controls.Add(Me.btadd)
        Me.GroupControl2.Controls.Add(Me.btdel)
        Me.GroupControl2.Controls.Add(Me.btedit)
        Me.GroupControl2.Controls.Add(Me.grid1)
        Me.GroupControl2.Location = New System.Drawing.Point(12, 96)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(572, 245)
        Me.GroupControl2.TabIndex = 4
        Me.GroupControl2.Text = "Bukti Sewa"
        '
        'btadd
        '
        Me.btadd.Image = CType(resources.GetObject("btadd.Image"), System.Drawing.Image)
        Me.btadd.Location = New System.Drawing.Point(366, 204)
        Me.btadd.Name = "btadd"
        Me.btadd.Size = New System.Drawing.Size(64, 23)
        Me.btadd.TabIndex = 0
        Me.btadd.Text = "&Tambah"
        Me.btadd.ToolTip = "Tambah Barang"
        '
        'btdel
        '
        Me.btdel.Image = CType(resources.GetObject("btdel.Image"), System.Drawing.Image)
        Me.btdel.Location = New System.Drawing.Point(503, 204)
        Me.btdel.Name = "btdel"
        Me.btdel.Size = New System.Drawing.Size(61, 23)
        Me.btdel.TabIndex = 2
        Me.btdel.Text = "&Hapus"
        Me.btdel.ToolTip = "Hapus Barang"
        '
        'btedit
        '
        Me.btedit.Image = CType(resources.GetObject("btedit.Image"), System.Drawing.Image)
        Me.btedit.Location = New System.Drawing.Point(436, 205)
        Me.btedit.Name = "btedit"
        Me.btedit.Size = New System.Drawing.Size(61, 23)
        Me.btedit.TabIndex = 1
        Me.btedit.Text = "&Edit"
        Me.btedit.ToolTip = "Edit Barang"
        '
        'fbayar_sw2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 416)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.LabelControl14)
        Me.Controls.Add(Me.tnote)
        Me.Controls.Add(Me.bts_supir)
        Me.Controls.Add(Me.tnama_supir)
        Me.Controls.Add(Me.tkd_supir)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.ttgl)
        Me.Controls.Add(Me.tbukti)
        Me.Controls.Add(Me.LabelControl15)
        Me.Controls.Add(Me.talamat)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fbayar_sw2"
        Me.Text = "Pembayaran Sewa Barang"
        CType(Me.ttgl.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbukti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnama_supir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tkd_supir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.talamat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl As DevExpress.XtraEditors.DateEdit
    Friend WithEvents tbukti As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl15 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents bts_supir As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tnama_supir As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tkd_supir As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents talamat As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents grid1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tnote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btdel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btedit As DevExpress.XtraEditors.SimpleButton
End Class
