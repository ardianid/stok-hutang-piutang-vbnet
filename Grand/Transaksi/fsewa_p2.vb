Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fsewa_p2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private Sub kosongkan()
        tbukti.Text = "<< New >>"

        tkd_toko.Text = ""
        tnama_toko.Text = ""
        talamat_toko.Text = ""

        tnote.Text = ""

        tdisc_per.EditValue = 0.0
        tdisc_rp.EditValue = 0
        tnetto.EditValue = 0
        tbrutto.EditValue = 0

        '  tsewa.Text = ""

        opengrid_hist()
        opengrid_barang()

    End Sub

    Private Sub opengrid_hist()

        Dim sql As String = String.Format("select nobukti2,tanggal1,tanggal2 from hsewa where nobukti='{0}'", tsewa.Text.Trim)


        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        grid1.DataSource = Nothing

        Try

            open_wait()

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            grid1.DataSource = dv1

            close_wait()


        Catch ex As OleDb.OleDbException
            close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub opengrid_barang()

        Dim sql As String = String.Format("select b.nama_barang,a.qty,a.satuan from trsewa2 a inner join ms_barang b on a.kd_barang=b.kd_barang where a.nobukti='{0}'", tsewa.Text.Trim)


        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet

        grid2.DataSource = Nothing

        Try

            open_wait()

            dv2 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager2 = New DataViewManager(ds)
            dv2 = dvmanager2.CreateDataView(ds.Tables(0))

            grid2.DataSource = dv2

            close_wait()


        Catch ex As OleDb.OleDbException
            close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub isi()

        Dim nobukti As String = dv(position)("nobukti").ToString
        Dim sql As String = String.Format("SELECT trsewa3.nobukti, trsewa3.tanggal, trsewa3.tanggal1, trsewa3.tanggal2, trsewa3.nobukti_sw, ms_toko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, " & _
        "trsewa3.brutto, trsewa3.disc_per, trsewa3.disc_rp, trsewa3.netto, trsewa3.note " & _
        "FROM  ms_toko INNER JOIN " & _
        "trsewa ON ms_toko.kd_toko = trsewa.kd_toko INNER JOIN " & _
        "trsewa3 ON trsewa.nobukti = trsewa3.nobukti_sw WHERE trsewa3.nobukti='{0}'", nobukti)

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim dread As OleDbDataReader = comd.ExecuteReader
            Dim hasil As Boolean

            If dread.HasRows Then
                If dread.Read Then

                    If Not dread("nobukti").ToString.Equals("") Then

                        hasil = True

                        tbukti.EditValue = dread("nobukti").ToString
                        ttgl.EditValue = DateValue(dread("tanggal").ToString)

                        ttgl_sewa1.EditValue = DateValue(dread("tanggal1").ToString)
                        ttgl_sewa2.EditValue = DateValue(dread("tanggal2").ToString)

                        tkd_toko.EditValue = dread("kd_toko").ToString
                        tnama_toko.EditValue = dread("nama_toko").ToString
                        talamat_toko.EditValue = dread("alamat_toko").ToString

                        isi_nosewa(dread("nobukti_sw").ToString)

                        tsewa.EditValue = dread("nobukti_sw").ToString

                        tbrutto.EditValue = dread("brutto").ToString
                        tdisc_per.EditValue = dread("disc_per").ToString
                        tdisc_rp.EditValue = dread("disc_rp").ToString
                        tnetto.EditValue = dread("netto").ToString

                        tnote.EditValue = dread("note").ToString

                        opengrid_hist()
                        opengrid_barang()

                    Else
                        hasil = False
                    End If


                Else
                    hasil = False
                End If
            Else
                hasil = False
            End If

            If hasil = False Then

                kosongkan()

            End If

            dread.Close()

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub totalnetto()

        Dim jumlah As Double = 0

        Dim diskon As Double = tdisc_rp.EditValue

        If diskon > 0 Then
            jumlah = tbrutto.EditValue - diskon
        Else
            jumlah = tbrutto.EditValue
        End If

        tnetto.EditValue = jumlah

    End Sub

    Private Function cekbukti(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim sql As String = ""

        Dim tahun As String = Year(ttgl.EditValue)
        tahun = Microsoft.VisualBasic.Right(tahun, 2)
        Dim bulan As String = Month(ttgl.EditValue)

        If bulan.Length = 1 Then
            bulan = "0" & bulan
        End If

        sql = String.Format("select max(nobukti) from trsewa3 where nobukti like '%PSW.{0}%'", String.Format("{0}{1}", tahun, bulan))

        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim nilai As Integer = 0

        If drd.HasRows Then
            If drd.Read Then

                If Not drd(0).ToString.Equals("") Then
                    nilai = Microsoft.VisualBasic.Right(drd(0).ToString, 4)
                End If

            End If
        End If

        nilai = nilai + 1
        Dim kbukti As String = nilai

        Select Case kbukti.Length
            Case 1
                kbukti = "000" & nilai
            Case 2
                kbukti = "00" & nilai
            Case 3
                kbukti = "0" & nilai
            Case Else
                kbukti = nilai
        End Select

        Dim jenisnota As String
        jenisnota = "PSW."


        Return String.Format("{0}{1}{2}{3}", jenisnota, tahun, bulan, kbukti)

    End Function

    Private Sub isi_nosewa(ByVal nosewa As String)

        Dim sql As String = ""

        If nosewa = "" Then
            sql = String.Format("select nobukti from trsewa where sbatal=0 and skembali=0 and kd_toko='{0}'", tkd_toko.Text.Trim)
        Else
            sql = String.Format("select nobukti from trsewa where nobukti='{0}'", nosewa)
        End If



        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet
        Dim dvg As DataView

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvg = dvm.CreateDataView(ds.Tables(0))

            tsewa.Properties.DataSource = dvg

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try
            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            Dim cmd As OleDbCommand
            Dim cmdtoko As OleDbCommand

            If addstat Then
                Dim bukti As String = cekbukti(cn, sqltrans)
                tbukti.EditValue = bukti

                '1 . update faktur
                Dim sqlin_faktur As String = String.Format("insert into trsewa3 (nobukti,tanggal,tanggal1,tanggal2,nobukti_sw,disc_per,disc_rp,brutto,netto,note) values('{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8},'{9}')", _
                                    tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_sewa1.EditValue), convert_date_to_eng(ttgl_sewa2.EditValue), tsewa.Text.Trim, Replace(tdisc_per.EditValue, ",", "."), Replace(tdisc_rp.EditValue, ",", "."), Replace(tbrutto.EditValue, ",", "."), Replace(tnetto.EditValue, ",", "."), tnote.Text.Trim)


                cmd = New OleDbCommand(sqlin_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()

                '2. update piutangtoko
                Dim sqltoko As String = String.Format("update ms_toko set piutangsewa=piutangsewa + {0} where kd_toko='{1}'", Replace(tnetto.EditValue, ",", "."), tkd_toko.Text.Trim)

                cmdtoko = New OleDbCommand(sqltoko, cn, sqltrans)
                cmdtoko.ExecuteNonQuery()

                ' hist sewa
                Dim sqlhist As String = String.Format("insert into hsewa (nobukti,nobukti2,tanggal1,tanggal2) values('{0}','{1}','{2}','{3}')", tsewa.Text.Trim, tbukti.Text.Trim, convert_date_to_eng(ttgl_sewa1.EditValue), convert_date_to_eng(ttgl_sewa2.EditValue))
                Using cmdhist As OleDbCommand = New OleDbCommand(sqlhist, cn, sqltrans)
                    cmdhist.ExecuteNonQuery()
                End Using

                Clsmy.InsertToLog(cn, "btsewa_p", 1, 0, 0, 0, tbukti.Text.Trim, ttgl.EditValue, sqltrans)


            Else

                '2. update piutang toko
                Dim sqlct As String = String.Format("select a.netto,b.kd_toko from trsewa3 a inner join trsewa b on a.nobukti_sw=b.nobukti where a.nobukti='{0}'", tbukti.Text.Trim)

                Dim cmdt As OleDbCommand = New OleDbCommand(sqlct, cn, sqltrans)
                Dim drdt As OleDbDataReader = cmdt.ExecuteReader

                If drdt.Read Then
                    If IsNumeric(drdt("netto").ToString) Then

                        Dim nett_sebelum As Double = drdt("netto").ToString

                        Dim sqluptoko As String = String.Format("update ms_toko set piutangsewa=piutangsewa - {0} where kd_toko='{1}'", Replace(nett_sebelum, ",", "."), tkd_toko.Text.Trim)
                        Dim sqluptoko2 As String = String.Format("update ms_toko set piutangsewa=piutangsewa + {0} where kd_toko='{1}'", Replace(tnetto.EditValue, ",", "."), tkd_toko.Text.Trim)

                        Dim cmdtk As OleDbCommand = New OleDbCommand(sqluptoko, cn, sqltrans)
                        cmdtk.ExecuteNonQuery()

                        Dim cmdtk2 As OleDbCommand = New OleDbCommand(sqluptoko2, cn, sqltrans)
                        cmdtk2.ExecuteNonQuery()

                    End If
                End If
                drdt.Close()

                '1. update faktur
                Dim sqlup_faktur As String = String.Format("update trsewa3 set tanggal='{0}',tanggal1='{1}',tanggal2='{2}',nobukti_sw='{3}',disc_per={4},disc_rp={5},brutto={6},netto={7},note='{8}' where nobukti='{9}'", convert_date_to_eng(ttgl.EditValue), convert_date_to_eng(ttgl_sewa1.EditValue), convert_date_to_eng(ttgl_sewa2.EditValue), tsewa.Text.Trim, Replace(tdisc_per.EditValue, ",", "."), Replace(tdisc_rp.EditValue, ",", "."), Replace(tbrutto.EditValue, ",", "."), Replace(tnetto.EditValue, ",", "."), tnote.Text.Trim, tbukti.Text.Trim)

                cmd = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()


                Dim sqlhist As String = String.Format("update hsewa set tanggal1='{0}',tanggal2='{1}' where nobukti='{2}' and nobukti2='{3}'", convert_date_to_eng(ttgl_sewa1.EditValue), convert_date_to_eng(ttgl_sewa2.EditValue), tsewa.Text.Trim, tbukti.Text.Trim)
                Using cmdhist As OleDbCommand = New OleDbCommand(sqlhist, cn, sqltrans)
                    cmdhist.ExecuteNonQuery()
                End Using

                Clsmy.InsertToLog(cn, "btsewa_p", 0, 1, 0, 0, tbukti.Text.Trim, ttgl.EditValue, sqltrans)

            End If

            sqltrans.Commit()

                close_wait()

                MsgBox("Data disimpan...", vbOKOnly + vbInformation, "Informasi")

            If addstat = True Then

                insertview()

                kosongkan()
                ttgl.Focus()
            Else
                close_wait()

                updateview()

                Me.Close()
            End If



        Catch ex As Exception
            close_wait()

            If Not IsNothing(sqltrans) Then
                sqltrans.Rollback()
            End If

            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try


    End Sub

    Private Sub cek_netto()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select netto from trsewa where nobukti='{0}'", tsewa.Text.Trim)
            Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = comd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd(0).ToString) Then
                    tbrutto.EditValue = drd(0).ToString
                Else
                    tbrutto.EditValue = 0
                End If
            Else
                tbrutto.EditValue = 0
            End If


        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("nobukti") = tbukti.Text.Trim
        orow("nobukti_sw") = tsewa.Text.Trim
        orow("tanggal") = ttgl.Text.Trim
        orow("tanggal1") = ttgl_sewa1.Text.Trim
        orow("tanggal2") = ttgl_sewa2.Text.Trim
        orow("kd_toko") = tkd_toko.Text.Trim
        orow("nama_toko") = tnama_toko.Text.Trim
        orow("alamat_toko") = talamat_toko.Text.Trim
        orow("netto") = tnetto.EditValue
        orow("sbatal") = 0
        orow("jmlbayar") = 0

        dv.EndInit()

    End Sub

    Private Sub updateview()

        dv(position)("nobukti_sw") = tsewa.Text.Trim
        dv(position)("tanggal") = ttgl.Text.Trim
        dv(position)("tanggal1") = ttgl_sewa1.Text.Trim
        dv(position)("tanggal2") = ttgl_sewa2.Text.Trim
        dv(position)("kd_toko") = tkd_toko.Text.Trim
        dv(position)("nama_toko") = tnama_toko.Text.Trim
        dv(position)("alamat_toko") = talamat_toko.Text.Trim
        dv(position)("netto") = tnetto.EditValue

    End Sub

    Private Sub bts_toko_Click(sender As System.Object, e As System.EventArgs) Handles bts_toko.Click
        Dim fs As New fsoutlet With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .kdsales = ""}
        fs.ShowDialog(Me)

        tkd_toko.EditValue = fs.get_KODE
        tnama_toko.EditValue = fs.get_NAMA
        talamat_toko.EditValue = fs.get_ALAMAT

        tkd_toko_EditValueChanged(sender, Nothing)


    End Sub

    Private Sub tkd_toko_EditValueChanged(sender As Object, e As System.EventArgs) Handles tkd_toko.Validated
        If tkd_toko.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select kd_toko,nama_toko,alamat_toko from ms_toko where kd_toko='{0}' and aktif=1", tkd_toko.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_toko.EditValue = dread("kd_toko").ToString
                        tnama_toko.EditValue = dread("nama_toko").ToString
                        talamat_toko.EditValue = dread("alamat_toko").ToString

                    Else
                        tkd_toko.EditValue = ""
                        tnama_toko.EditValue = ""
                        talamat_toko.Text = ""


                    End If
                Else
                    tkd_toko.EditValue = ""
                    tnama_toko.EditValue = ""
                    talamat_toko.Text = ""

                End If

                dread.Close()

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Finally


                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If
            End Try

            isi_nosewa("")

        End If
    End Sub

    Private Sub tkd_toko_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_toko.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_toko_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_toko_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_toko.LostFocus
        If tkd_toko.Text.Trim.Length = 0 Then
            tkd_toko.EditValue = ""
            tnama_toko.EditValue = ""
            talamat_toko.Text = ""
        End If
    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub frekap_to2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tkd_toko.Focus()
    End Sub

    Private Sub frekap_to2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ttgl.EditValue = Date.Now

        ttgl_sewa1.EditValue = Date.Now
        ttgl_sewa2.EditValue = Date.Now

        If addstat = False Then

            tkd_toko.Enabled = False
            bts_toko.Enabled = False
            tsewa.Enabled = False

            isi()
        Else
            kosongkan()
        End If

    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tkd_toko.Text.Trim.Length = 0 Then
            MsgBox("Outlet harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tkd_toko.Focus()
            Return
        End If

        If tsewa.Text.Trim.Length = 0 Then
            MsgBox("Nobukti sewa tidak boleh kosong", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If MsgBox("Yakin sudah benar.. ???", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.No Then
            Return
        Else
            simpan()
        End If

    End Sub

    Private Sub tdisc_per_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tdisc_per.Validated

        If tdisc_per.EditValue > 0 Then

            Dim brutto As Double = tbrutto.EditValue
            Dim disc As Double = tdisc_per.EditValue / 100
            Dim hasil As Double = brutto * disc

            tdisc_rp.EditValue = hasil
        Else
            tdisc_rp.EditValue = 0
        End If

        totalnetto()

    End Sub

    Private Sub tdisc_rp_Validated(sender As Object, e As System.EventArgs) Handles tdisc_rp.Validated

        If tdisc_rp.EditValue > 0 Then

            Dim brutto As Double = tbrutto.EditValue
            Dim disc As Double = tdisc_rp.EditValue
            Dim hasil As Double = (disc / brutto) * 100

            tdisc_per.EditValue = hasil
        Else
            tdisc_per.EditValue = 0.0
        End If

        totalnetto()

    End Sub

    Private Sub tbrutto_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbrutto.EditValueChanged
        tdisc_per_EditValueChanged(sender, Nothing)
        totalnetto()
    End Sub

    Private Sub tsewa_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tsewa.EditValueChanged

        opengrid_hist()
        opengrid_barang()

        cek_netto()

    End Sub

End Class