Imports DevExpress.XtraBars
Imports System.Data.OleDb
Imports Grand.Clsmy
Imports DevExpress.Skins


Public Class futama

    Sub main()
        'SkinManager.EnableFormSkins()

        'DevExpress.UserSkins.BonusSkins.Register()

    End Sub

    Private Sub disable_bar()

        Dim i As Integer = 0
        For i = 0 To RibbonControl.Items.Count - 1
            If TypeOf RibbonControl.Items(i) Is BarButtonItem Then

                Dim btn As BarButtonItem = CType(RibbonControl.Items(i), BarButtonItem)

                Dim xA As String = btn.Name.ToString

                If xA.Length > 0 Then
                    btn.Visibility = BarItemVisibility.Always
                    btn.Enabled = False
                End If


            End If
        Next

        RibbonPageGroup2.Visible = False
        RibbonPageGroup3.Visible = False
        RibbonPageGroup4.Visible = False
        RibbonPageGroup5.Visible = False

        RibbonPageGroup8.Visible = False
        RibbonPageGroup7.Visible = False
        RibbonPageGroup6.Visible = False
        RibbonPageGroup10.Visible = False
        RibbonPageGroup11.Visible = False

        RibbonPageGroup13.Visible = False

        RibbonControl.Minimized = True

        RibbonPageGroup1.Visible = False
        RibbonPageGroup16.Visible = False
        RibbonPageGroup17.Visible = False
        RibbonPageGroup14.Visible = False
        '  RibbonPageGroup12.Visible = False
        RibbonPageGroup15.Visible = False


        RibbonPage1.Visible = False
        RibbonPage2.Visible = False
        RibbonPage3.Visible = False
        rPenjualan.Visible = False

    End Sub

    Public Sub LoadOtherForm(ByVal fname As Form)

        open_wait()
        Cursor = Cursors.WaitCursor

       
        fname.MdiParent = Me
        fname.Show()


        Cursor = Cursors.Default
        close_wait()

    End Sub


    Private Sub btnuser_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnuser.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fuser.MdiParent = Me
        fuser.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub futama_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Application.Exit()
    End Sub

    Private Sub futama_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        tglperiod1 = DateAdd("m", 0, DateSerial(Year(Today), Month(Today), 1))
        tglperiod1 = DateAdd(DateInterval.Day, -10, DateValue(tglperiod1))

        tglperiod2 = DateAdd("m", 1, DateSerial(Year(Today), Month(Today), 0))
        tglperiod2 = DateAdd(DateInterval.Day, 7, DateValue(tglperiod2))

        disable_bar()

        tstgl.Caption = DateValue(Date.Now)

        tsperiode.Caption = String.Format("Periode : {0}  s.d  {1}", tglperiod1, tglperiod2)

        Try

            Clsmy.LoadPrinterDef()

            Dim cn As OleDbConnection

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            tsserv.Caption = String.Format("Serv : {0}", servernam)

            cn.Close()
            cn.Dispose()

            LoadLOgin()

        Catch ex As Exception

            Dim ambildigit As Integer = ex.ToString.IndexOf("xd1")

            If ambildigit > 0 Then

                MsgBox(ex.ToString, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Error")
                End

            Else
                fsettdbase.MdiParent = Me
                fsettdbase.Show()
            End If

           

        End Try




    End Sub

    Public Sub LoadLOgin()
        Dim fmlogin As New login With {.MdiParent = Me, .WindowState = FormWindowState.Maximized}
        fmlogin.Show()
    End Sub

    Private Sub NO_logof_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles NO_logof.ItemClick

        For Each f As Form In Me.MdiChildren
            f.Close()
        Next

        disable_bar()

        userprog = ""
        pwd = ""
        initial_user = ""

        tuserakt.Caption = "User : "
        tsserv.Caption = "Serv : "

        Dim fmlogin As New login With {.MdiParent = Me, .WindowState = FormWindowState.Maximized}
        fmlogin.Show()

    End Sub

    Private Sub btsched_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btsched.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        fschedule.MdiParent = Me
        fschedule.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        tsjam.Caption = Format(Now, "hh:mm:ss tt")
    End Sub

    Private Sub btkary_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btkary.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        fpegawai.MdiParent = Me
        fpegawai.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

    Private Sub btkab_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btkab.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fkab.MdiParent = Me
        fkab.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btkec_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btkec.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fkec.MdiParent = Me
        fkec.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btkel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btkel.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fkel.MdiParent = Me
        fkel.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btklas_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btklas.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fklas.MdiParent = Me
        fklas.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btjalur_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btjalur.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fjalur_od.MdiParent = Me
        fjalur_od.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btjalur_kr_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btjalur_kr.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fjalur_kr.MdiParent = Me
        fjalur_kr.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btkendaraan_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btkendaraan.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        fkendaraan.MdiParent = Me
        fkendaraan.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

    Private Sub btpasar_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btpasar.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        fpasar.MdiParent = Me
        fpasar.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

    Private Sub btlevsales1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btlevsales1.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        f1sales.MdiParent = Me
        f1sales.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

    Private Sub btlevsales2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btlevsales2.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        f2sales.MdiParent = Me
        f2sales.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

    Private Sub btgudang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btgudang.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fgudang.MdiParent = Me
        fgudang.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btbarang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btbarang.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fbarang.MdiParent = Me
        fbarang.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btbarang_g_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btbarang_g.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fbarang_g.MdiParent = Me
        fbarang_g.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btgroup_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btgroup.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fgroup.MdiParent = Me
        fgroup.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

 
    Private Sub bttoko_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bttoko.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        foutlet.MdiParent = Me
        foutlet.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btfaktur_to_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btfaktur_to.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        ffaktur_to.MdiParent = Me
        ffaktur_to.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

    Private Sub btrekap_to_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btrekap_to.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        frekap_to.MdiParent = Me
        frekap_to.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

    Private Sub btfakt_blk_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btfakt_blk.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        ffaktur_b.MdiParent = Me
        ffaktur_b.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

    Private Sub btsupp_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btsupp.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        fsupplier.MdiParent = Me
        fsupplier.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

    Private Sub btbeli_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btbeli.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        fbeli.MdiParent = Me
        fbeli.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

    Private Sub btadm_g_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btadm_g.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fadm_gud.MdiParent = Me
        fadm_gud.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub NO_settTgl_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles NO_settTgl.ItemClick

        ' open_wait()
        Cursor = Cursors.WaitCursor

        'fadm_gud.MdiParent = Me
        fdateutil.StartPosition = FormStartPosition.CenterParent
        fdateutil.ShowDialog(Me)

        Cursor = Cursors.Default
        ' close_wait()

    End Sub

    Private Sub btspm_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btspm.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        trspm.MdiParent = Me
        trspm.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub bbturun_kv_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbturun_kv.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        ftrun_br.MdiParent = Me
        ftrun_br.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btfaktur_kv_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btfaktur_kv.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        ffaktur_kv.MdiParent = Me
        ffaktur_kv.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btpindahgud_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btpindahgud.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        trpindahgud.MdiParent = Me
        trpindahgud.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btadjust_br_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btadjust_br.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fadjust_br.MdiParent = Me
        fadjust_br.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btretur_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btretur.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fretur.MdiParent = Me
        fretur.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btpinjam_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btpinjam.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fpinjam.MdiParent = Me
        fpinjam.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btpengembalian_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btpengembalian.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fpengembalian.MdiParent = Me
        fpengembalian.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btsewa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btsewa.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fsewa.MdiParent = Me
        fsewa.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btsewa_p_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btsewa_p.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fsewa_p.MdiParent = Me
        fsewa_p.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btpengembalian_sw_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btpengembalian_sw.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fpengembalian_sw.MdiParent = Me
        fpengembalian_sw.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btgiro_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btgiro.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fgiro.MdiParent = Me
        fgiro.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btdtg_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btdtg.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fdtg.MdiParent = Me
        fdtg.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btbayar_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btbayar.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fbayar.MdiParent = Me
        fbayar.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btgiro_cair_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btgiro_cair.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fgiro_cair.MdiParent = Me
        fgiro_cair.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btgiro_tolak_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btgiro_tolak.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fgiro_tolak.MdiParent = Me
        fgiro_tolak.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btkirim_jb_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btkirim_jb.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fkirim_jb.MdiParent = Me
        fkirim_jb.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btbayar_sw_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btbayar_sw.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fbayar_sw.MdiParent = Me
        fbayar_sw.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btbayar_psw_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btbayar_psw.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fbayar_psw.MdiParent = Me
        fbayar_psw.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub NO_ch_pwd_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles NO_ch_pwd.ItemClick
        frubah_pwd.ShowDialog(Me)
    End Sub

    Private Sub NO_Lap1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles NO_Lap1.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        freport.jenislap = 1
        freport.MdiParent = Me
        freport.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

    Private Sub NO_Lap2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles NxO_Lap2.ItemClick
        open_wait()
        Cursor = Cursors.WaitCursor

        freport2.jenislap = 2
        freport2.MdiParent = Me
        freport2.Show()

        Cursor = Cursors.Default
        close_wait()
    End Sub

    Private Sub NO_Lap0_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles NxO_Lap0.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        freport0.jenislap = 0
        freport0.MdiParent = Me
        freport0.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btsupkenek_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btsupkenek.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fsupir_kenek.MdiParent = Me
        fsupir_kenek.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btother_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btother.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fother.MdiParent = Me
        fother.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub bt_bocoran_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bt_bocoran.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        tr_bocoran.MdiParent = Me
        tr_bocoran.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub NO_btsetprint_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles NO_btsetprint.ItemClick

        Cursor = Cursors.WaitCursor

        'fadm_gud.MdiParent = Me
        fsetprinter.StartPosition = FormStartPosition.CenterParent
        fsetprinter.ShowDialog(Me)

        Cursor = Cursors.Default

    End Sub

    Private Sub bt_verifkirim_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bt_verifkirim.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fverif_supirkenek.MdiParent = Me
        fverif_supirkenek.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btbeli_cust_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btbeli_cust.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fbeli_cust.MdiParent = Me
        fbeli_cust.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

End Class