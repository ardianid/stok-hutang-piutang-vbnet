﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fpegawai
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fpegawai))
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
        Me.tsnonakt = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
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
        Me.calamat = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cjkel = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ctgl = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ctelp1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.chp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.caktif = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cgolongan = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.XpPageSelector1 = New DevExpress.Xpo.XPPageSelector(Me.components)
        CType(Me.bn1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.bn1.SuspendLayout()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'bn1
        '
        Me.bn1.AddNewItem = Nothing
        Me.bn1.CountItem = Me.ToolStripLabel2
        Me.bn1.DeleteItem = Nothing
        Me.bn1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bn1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.bn1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton2, Me.ToolStripButton3, Me.ToolStripSeparator2, Me.ToolStripTextBox1, Me.ToolStripLabel2, Me.ToolStripSeparator3, Me.ToolStripButton4, Me.ToolStripButton5, Me.ToolStripSeparator4, Me.tsadd, Me.tsedit, Me.tsdel, Me.tsnonakt, Me.ToolStripSeparator1, Me.tsref, Me.ToolStripSeparator5, Me.ToolStripLabel3, Me.tcbofind, Me.ToolStripButton9, Me.tfind, Me.tsfind})
        Me.bn1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.bn1.Location = New System.Drawing.Point(0, 0)
        Me.bn1.MoveFirstItem = Me.ToolStripButton2
        Me.bn1.MoveLastItem = Me.ToolStripButton5
        Me.bn1.MoveNextItem = Me.ToolStripButton4
        Me.bn1.MovePreviousItem = Me.ToolStripButton3
        Me.bn1.Name = "bn1"
        Me.bn1.PositionItem = Me.ToolStripTextBox1
        Me.bn1.Size = New System.Drawing.Size(1064, 36)
        Me.bn1.TabIndex = 128
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
        Me.tsdel.Text = "&Hapus"
        Me.tsdel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsdel.ToolTipText = "Hapus data"
        Me.tsdel.Visible = False
        '
        'tsnonakt
        '
        Me.tsnonakt.AutoSize = False
        Me.tsnonakt.ForeColor = System.Drawing.Color.Black
        Me.tsnonakt.Image = CType(resources.GetObject("tsnonakt.Image"), System.Drawing.Image)
        Me.tsnonakt.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsnonakt.Name = "tsnonakt"
        Me.tsnonakt.Size = New System.Drawing.Size(70, 33)
        Me.tsnonakt.Text = "&Non Aktifkan"
        Me.tsnonakt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsnonakt.ToolTipText = "Non aktifkan karyawan (Keluar)"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 36)
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
        Me.tcbofind.Items.AddRange(New Object() {"Kode", "Nama", "Alamat", "Bagian"})
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
        Me.grid1.Cursor = System.Windows.Forms.Cursors.Default
        Me.grid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grid1.Location = New System.Drawing.Point(0, 36)
        Me.grid1.MainView = Me.GridView1
        Me.grid1.Name = "grid1"
        Me.grid1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit3})
        Me.grid1.Size = New System.Drawing.Size(1064, 328)
        Me.grid1.TabIndex = 129
        Me.grid1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ckode, Me.cnama, Me.calamat, Me.cjkel, Me.ctgl, Me.ctelp1, Me.chp, Me.caktif, Me.cgolongan})
        Me.GridView1.GridControl = Me.grid1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'ckode
        '
        Me.ckode.Caption = "Kode"
        Me.ckode.FieldName = "kd_karyawan"
        Me.ckode.Name = "ckode"
        Me.ckode.OptionsColumn.AllowEdit = False
        Me.ckode.Visible = True
        Me.ckode.VisibleIndex = 2
        Me.ckode.Width = 45
        '
        'cnama
        '
        Me.cnama.Caption = "Nama"
        Me.cnama.FieldName = "nama_karyawan"
        Me.cnama.Name = "cnama"
        Me.cnama.OptionsColumn.AllowEdit = False
        Me.cnama.Visible = True
        Me.cnama.VisibleIndex = 3
        Me.cnama.Width = 91
        '
        'calamat
        '
        Me.calamat.Caption = "Alamat"
        Me.calamat.FieldName = "alamat"
        Me.calamat.Name = "calamat"
        Me.calamat.OptionsColumn.AllowEdit = False
        Me.calamat.Visible = True
        Me.calamat.VisibleIndex = 5
        Me.calamat.Width = 199
        '
        'cjkel
        '
        Me.cjkel.Caption = "Jenis Kelamin"
        Me.cjkel.FieldName = "jenis_kelamin"
        Me.cjkel.Name = "cjkel"
        Me.cjkel.OptionsColumn.AllowEdit = False
        Me.cjkel.Visible = True
        Me.cjkel.VisibleIndex = 6
        Me.cjkel.Width = 82
        '
        'ctgl
        '
        Me.ctgl.Caption = "Tgl Lahir"
        Me.ctgl.FieldName = "tgl_lahir"
        Me.ctgl.Name = "ctgl"
        Me.ctgl.OptionsColumn.AllowEdit = False
        Me.ctgl.Visible = True
        Me.ctgl.VisibleIndex = 4
        Me.ctgl.Width = 77
        '
        'ctelp1
        '
        Me.ctelp1.Caption = "No Telp1"
        Me.ctelp1.FieldName = "notelp1"
        Me.ctelp1.Name = "ctelp1"
        Me.ctelp1.OptionsColumn.AllowEdit = False
        Me.ctelp1.Visible = True
        Me.ctelp1.VisibleIndex = 7
        Me.ctelp1.Width = 100
        '
        'chp
        '
        Me.chp.Caption = "No Telp2"
        Me.chp.FieldName = "notelp2"
        Me.chp.Name = "chp"
        Me.chp.OptionsColumn.AllowEdit = False
        Me.chp.Visible = True
        Me.chp.VisibleIndex = 8
        Me.chp.Width = 132
        '
        'caktif
        '
        Me.caktif.AppearanceHeader.Options.UseTextOptions = True
        Me.caktif.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.caktif.Caption = "Aktif?"
        Me.caktif.ColumnEdit = Me.RepositoryItemCheckEdit3
        Me.caktif.FieldName = "aktif"
        Me.caktif.Name = "caktif"
        Me.caktif.OptionsColumn.AllowEdit = False
        Me.caktif.Visible = True
        Me.caktif.VisibleIndex = 0
        Me.caktif.Width = 34
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
        'cgolongan
        '
        Me.cgolongan.Caption = "Bagian"
        Me.cgolongan.FieldName = "bagian"
        Me.cgolongan.Name = "cgolongan"
        Me.cgolongan.OptionsColumn.AllowEdit = False
        Me.cgolongan.Visible = True
        Me.cgolongan.VisibleIndex = 1
        Me.cgolongan.Width = 52
        '
        'fpegawai
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1064, 364)
        Me.Controls.Add(Me.grid1)
        Me.Controls.Add(Me.bn1)
        Me.Name = "fpegawai"
        Me.Text = "Karyawan"
        CType(Me.bn1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.bn1.ResumeLayout(False)
        Me.bn1.PerformLayout()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
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
    Friend WithEvents calamat As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cjkel As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ctgl As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ctelp1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents chp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents caktif As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cgolongan As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents XpPageSelector1 As DevExpress.Xpo.XPPageSelector
    Friend WithEvents tsnonakt As System.Windows.Forms.ToolStripButton
End Class
