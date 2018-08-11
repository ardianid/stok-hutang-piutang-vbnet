Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class fpr_histfaktur

    Dim crReportDocument As cr_histfaktur

    Dim sales As String
    Dim outlet As String
    Dim alamat_outlet As String
    Dim tglfaktur As String
    Dim nilaifaktur As Double

    Private Sub loaddata()

        Cursor = Cursors.WaitCursor

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            If cek_header(cn) = False Then
                MsgBox("No Faktur tidak ditemukan...", vbOKOnly + vbExclamation, "Informasi")
                tbukti.Focus()
                Return
            End If

            Dim sql As String = ""

            If cbkriteria.SelectedIndex = 0 Then

                sql = String.Format("select trbayar.nobukti,trbayar.tanggal,(trbayar2.jmltunai + trbayar2.jmltrans) as jmltunai,trbayar2.jmlretur,trbayar2.disc_susulan,trbayar2.jmlkelebihan_dr, " & _
            "trbayar2.pembulatan,trbayar2.jmlgiro as girogantung,0 as girocair,0 as girotolak,'Bayar' as jenis " & _
            "from trbayar2 inner join trbayar on trbayar2.nobukti=trbayar.nobukti " & _
            "where trbayar.sbatal=0 and trbayar2.nobukti_fak='{0}' " & _
            "union all " & _
            "select tr_bg.nobukti +'('+tr_bg2.nogiro+')' as nobukti,tr_bg.tanggal, " & _
            "0 as tunai,0 as jmlretur,0 as disc_susulan,0 as jmlkelebihan_dr, " & _
            "0 as pembulatan,0 as jmlgiro,trbayar2_giro.jmlgiro as girocair,0 as girotolak,'Giro Cair' " & _
            "from tr_bg2 inner join tr_bg on tr_bg2.nobukti=tr_bg.nobukti " & _
            "inner join trbayar2_giro on tr_bg2.nogiro=trbayar2_giro.nogiro " & _
            "inner join trbayar on trbayar.nobukti=trbayar2_giro.nobukti " & _
            "where tr_bg.jenis=1 and tr_bg.sbatal=0 and trbayar.sbatal=0 and trbayar2_giro.nobukti_fak ='{0}' " & _
            "union all " & _
            "select tr_bg.nobukti +'('+tr_bg2.nogiro+')' as nobukti,tr_bg.tanggal, " & _
            "0 as tunai,0 as jmlretur,0 as disc_susulan,0 as jmlkelebihan_dr, " & _
            "0 as pembulatan,0 as jmlgiro,0 as girocair,trbayar2_giro.jmlgiro_batal as girotolak,'Giro Tolak' " & _
            "from tr_bg2 inner join tr_bg on tr_bg2.nobukti=tr_bg.nobukti " & _
            "inner join trbayar2_giro on tr_bg2.nogiro=trbayar2_giro.nogiro " & _
            "inner join trbayar on trbayar.nobukti=trbayar2_giro.nobukti " & _
            "where tr_bg.jenis=2 and tr_bg.sbatal=0 and trbayar.sbatal=0 and trbayar2_giro.nobukti_fak ='{0}'", tbukti.Text.Trim)

            Else

                sql = String.Format("select trbayar.nobukti,trbayar.tanggal,(trbayar2.jmltunai + trbayar2.jmltrans) as jmltunai,trbayar2.jmlretur,trbayar2.disc_susulan,trbayar2.jmlkelebihan_dr, " & _
            "trbayar2.pembulatan,trbayar2.jmlgiro as girogantung,0 as girocair,0 as girotolak,'Bayar' as jenis " & _
            "from trbayar2 inner join trbayar on trbayar2.nobukti=trbayar.nobukti " & _
            "where trbayar.sbatal=0 and trbayar2.nobukti_fak in (select nobukti from trfaktur_to where no_nota='{0}') " & _
            "union all " & _
            "select tr_bg.nobukti +'('+tr_bg2.nogiro+')' as nobukti,tr_bg.tanggal, " & _
            "0 as tunai,0 as jmlretur,0 as disc_susulan,0 as jmlkelebihan_dr, " & _
            "0 as pembulatan,0 as jmlgiro,trbayar2_giro.jmlgiro as girocair,0 as girotolak,'Giro Cair' " & _
            "from tr_bg2 inner join tr_bg on tr_bg2.nobukti=tr_bg.nobukti " & _
            "inner join trbayar2_giro on tr_bg2.nogiro=trbayar2_giro.nogiro " & _
            "inner join trbayar on trbayar.nobukti=trbayar2_giro.nobukti " & _
            "where tr_bg.jenis=1 and tr_bg.sbatal=0 and trbayar.sbatal=0 and trbayar2_giro.nobukti_fak  in (select nobukti from trfaktur_to where no_nota='{0}') " & _
            "union all " & _
            "select tr_bg.nobukti +'('+tr_bg2.nogiro+')' as nobukti,tr_bg.tanggal, " & _
            "0 as tunai,0 as jmlretur,0 as disc_susulan,0 as jmlkelebihan_dr, " & _
            "0 as pembulatan,0 as jmlgiro,0 as girocair,trbayar2_giro.jmlgiro_batal as girotolak,'Giro Tolak' " & _
            "from tr_bg2 inner join tr_bg on tr_bg2.nobukti=tr_bg.nobukti " & _
            "inner join trbayar2_giro on tr_bg2.nogiro=trbayar2_giro.nogiro " & _
            "inner join trbayar on trbayar.nobukti=trbayar2_giro.nobukti " & _
            "where tr_bg.jenis=2 and tr_bg.sbatal=0 and trbayar.sbatal=0 and trbayar2_giro.nobukti_fak  in (select nobukti from trfaktur_to where no_nota='{0}') ", tbukti.Text.Trim)

            End If
            

            Dim ds As New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ds1 As New ds_histbayarfak
            ds1.Clear()
            ds1.Tables(0).Merge(ds.Tables(0))

            crReportDocument = New cr_histfaktur
            crReportDocument.SetDataSource(ds1)

            crReportDocument.SetParameterValue("nofaktur", tbukti.Text.Trim)
            crReportDocument.SetParameterValue("tanggal", tglfaktur)
            crReportDocument.SetParameterValue("sales", sales)
            crReportDocument.SetParameterValue("outlet", outlet)
            crReportDocument.SetParameterValue("alamat_outlet", alamat_outlet)
            crReportDocument.SetParameterValue("nilai", nilaifaktur)
            crReportDocument.SetParameterValue("userprint", String.Format("User : {0} | Tgl : {1}", userprog, Date.Now))

            CrystalReportViewer1.ReportSource = crReportDocument
            CrystalReportViewer1.Refresh()

            Cursor = Cursors.Default

        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            Cursor = Cursors.Default

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Function cek_header(ByVal cn As OleDbConnection) As Boolean

        Dim hasil As Boolean = False
        Dim sql As String = ""

        If cbkriteria.SelectedIndex = 0 Then
            sql = String.Format("SELECT     trfaktur_to.nobukti, trfaktur_to.tanggal, ms_pegawai.kd_karyawan+ ' - ' +ms_pegawai.nama_karyawan as sales, " & _
                "ms_toko.kd_toko+ ' - '+ms_toko.nama_toko as outlet, ms_toko.alamat_toko, trfaktur_to.netto " & _
                 "FROM         ms_pegawai INNER JOIN trfaktur_to ON ms_pegawai.kd_karyawan = trfaktur_to.kd_karyawan INNER JOIN " & _
                "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko where trfaktur_to.nobukti='{0}'", tbukti.Text.Trim)
        Else
            sql = String.Format("SELECT     trfaktur_to.nobukti, trfaktur_to.tanggal, ms_pegawai.kd_karyawan+ ' - ' +ms_pegawai.nama_karyawan as sales, " & _
                "ms_toko.kd_toko+ ' - '+ms_toko.nama_toko as outlet, ms_toko.alamat_toko, trfaktur_to.netto " & _
                 "FROM         ms_pegawai INNER JOIN trfaktur_to ON ms_pegawai.kd_karyawan = trfaktur_to.kd_karyawan INNER JOIN " & _
                "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko where trfaktur_to.no_nota='{0}'", tbukti.Text.Trim)
        End If

        

        Dim cmd As OleDbCommand = New OleDbCommand(Sql, cn)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        If drd.Read Then

            If Not drd("nobukti").ToString.Equals("") Then

                hasil = True

                sales = drd("sales").ToString
                outlet = drd("outlet").ToString
                alamat_outlet = drd("alamat_toko").ToString
                tglfaktur = drd("tanggal").ToString
                nilaifaktur = Double.Parse(drd("netto").ToString)

            End If

        End If

        drd.Close()

        Return hasil

    End Function

    Private Sub SimpleButton2_Click(sender As System.Object, e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub fpr_histfaktur_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        cbkriteria.Focus()
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click

        If tbukti.Text.Trim.Length = 0 Then
            MsgBox("No faktur harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tbukti.Focus()
            Return
        End If

        loaddata()
    End Sub

    Private Sub tbukti_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tbukti.KeyDown
        If e.KeyCode = 13 Then
            SimpleButton1_Click(sender, Nothing)
        End If
    End Sub

    Private Sub fpr_histfaktur_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        cbkriteria.SelectedIndex = 0
    End Sub

End Class