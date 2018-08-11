Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fbayar_psw2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private Sub kosongkan()

        tbukti.Text = "<< New >>"

        tkd_supir.Text = ""
        tnama_supir.Text = ""
        talamat.Text = ""
        tnote.Text = ""

        opengrid()

    End Sub

    Private Sub opengrid()

        Dim sql As String = String.Format("SELECT     tr_bayar_psw2.noid, tr_bayar_psw2.nobukti_sw, trsewa3.netto, tr_bayar_psw2.jumlah, trsewa3.tanggal " & _
            "FROM         tr_bayar_psw2 INNER JOIN " & _
            "trsewa3 ON tr_bayar_psw2.nobukti_sw = trsewa3.nobukti " & _
            "WHERE tr_bayar_psw2.nobukti='{0}'", tbukti.Text.Trim)

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

    Private Sub isi()

        Dim nobukti As String = dv(position)("nobukti").ToString

        Dim sql As String = String.Format("SELECT     tr_bayar_psw.nobukti, tr_bayar_psw.tanggal, ms_toko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, tr_bayar_psw.note " & _
            "FROM         tr_bayar_psw INNER JOIN " & _
            "ms_toko ON tr_bayar_psw.kd_toko = ms_toko.kd_toko " & _
            "WHERE tr_bayar_psw.nobukti='{0}'", nobukti)

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

                        '    tgl_old = ttgl.EditValue


                        tkd_supir.EditValue = dread("kd_toko").ToString
                        tnama_supir.EditValue = dread("nama_toko").ToString
                        talamat.EditValue = dread("alamat_toko").ToString
                        tnote.EditValue = dread("note").ToString

                        opengrid()

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

    Private Function cekbukti(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String


        Dim tahun As String = Year(ttgl.EditValue)
        tahun = Microsoft.VisualBasic.Right(tahun, 2)
        Dim bulan As String = Month(ttgl.EditValue)

        If bulan.Length = 1 Then
            bulan = "0" & bulan
        End If

        Dim tahunbulan As String = String.Format("{0}{1}", tahun, bulan)

        Dim sql As String = String.Format("select max(nobukti) from tr_bayar_psw where YEAR(tanggal)='{0}' and MONTH(tanggal)='{1}' and nobukti like '%{2}%'", Year(ttgl.EditValue), Month(ttgl.EditValue), tahunbulan)

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

        Return String.Format("PPW.{0}{1}{2}", tahun, bulan, kbukti)

    End Function

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try
            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            Dim cmd As OleDbCommand

            If addstat Then
                Dim bukti As String = cekbukti(cn, sqltrans)
                tbukti.EditValue = bukti

                '1 . update faktur
                Dim sqlin_faktur As String = String.Format("insert into tr_bayar_psw (nobukti,tanggal,kd_toko,note,jumlah) values('{0}','{1}','{2}','{3}',{4})", tbukti.Text.Trim, _
                                                            convert_date_to_eng(ttgl.EditValue), tkd_supir.EditValue, tnote.Text.Trim, Replace(GridView1.Columns("jumlah").SummaryItem.SummaryValue, ",", "."))


                cmd = New OleDbCommand(sqlin_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btbayar_psw", 1, 0, 0, 0, tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), sqltrans)

            Else

                '1. update faktur
                Dim sqlup_faktur As String = String.Format("update tr_bayar_psw set tanggal='{0}',kd_toko='{1}',note='{2}',jumlah={3} where nobukti='{4}'", convert_date_to_eng(ttgl.EditValue), tkd_supir.EditValue, tnote.Text.Trim, Replace(GridView1.Columns("jumlah").SummaryItem.SummaryValue, ",", "."), tbukti.Text.Trim)

                cmd = New OleDbCommand(sqlup_faktur, cn, sqltrans)
                cmd.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btbayar_psw", 0, 1, 0, 0, tbukti.Text.Trim, convert_date_to_eng(ttgl.EditValue), sqltrans)

            End If

            If simpan2(cn, sqltrans) = "ok" Then

                If addstat = True Then
                    insertview()
                Else
                    updateview()
                End If

                sqltrans.Commit()

                close_wait()

                MsgBox("Data disimpan...", vbOKOnly + vbInformation, "Informasi")

                If addstat = True Then
                    kosongkan()
                    ttgl.Focus()
                Else
                    close_wait()
                    Me.Close()
                End If

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

    Private Function simpan2(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String

        Dim hasil As String = ""

        For i As Integer = 0 To dv1.Count - 1

            Dim nobukti_sw As String = dv1(i)("nobukti_sw").ToString
            Dim jumlah As Double = Double.Parse(dv1(i)("jumlah").ToString)

            If addstat = True Then

                Dim sqlins As String = String.Format("insert into tr_bayar_psw2 (nobukti,nobukti_sw,jumlah) values('{0}','{1}',{2})", tbukti.Text.Trim, nobukti_sw, jumlah)
                Using cmins As OleDbCommand = New OleDbCommand(sqlins, cn, sqltrans)
                    cmins.ExecuteNonQuery()
                End Using

                Dim sqltrsewa As String = String.Format("update trsewa3 set jmlbayar=jmlbayar + {0} where nobukti='{1}'", jumlah, nobukti_sw)
                Using cmdtrsewa As OleDbCommand = New OleDbCommand(sqltrsewa, cn, sqltrans)
                    cmdtrsewa.ExecuteNonQuery()
                End Using

                Dim sqltoko As String = String.Format("update ms_toko set piutangsewa=piutangsewa - {0} where kd_toko='{1}'", jumlah, tkd_supir.Text.Trim)
                Using cmdtoko As OleDbCommand = New OleDbCommand(sqltoko, cn, sqltrans)
                    cmdtoko.ExecuteNonQuery()
                End Using

            Else

                Dim ada As Boolean = False

                Dim sqlc As String = String.Format("select * from tr_bayar_psw2 where nobukti='{0}' and nobukti_sw='{1}'", tbukti.Text.Trim, nobukti_sw)
                Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
                Dim drdc As OleDbDataReader = cmdc.ExecuteReader

                If drdc.Read Then
                    If Not drdc("nobukti").ToString.Equals("") Then

                        ada = True

                        Dim jumlahsebelum As Double = Double.Parse(drdc("jumlah").ToString)

                        Dim sqltrsewa_old As String = String.Format("update trsewa3 set jmlbayar=jmlbayar - {0} where nobukti='{1}'", jumlahsebelum, nobukti_sw)
                        Using cmdtrsewa_old As OleDbCommand = New OleDbCommand(sqltrsewa_old, cn, sqltrans)
                            cmdtrsewa_old.ExecuteNonQuery()
                        End Using

                        Dim sqltoko_old As String = String.Format("update ms_toko set piutangsewa=piutangsewa + {0} where kd_toko='{1}'", jumlahsebelum, nobukti_sw)
                        Using cmdtoko_old As OleDbCommand = New OleDbCommand(sqltoko_old, cn, sqltrans)
                            cmdtoko_old.ExecuteNonQuery()
                        End Using

                    End If
                End If

                Dim sqltrsewa As String = String.Format("update trsewa3 set jmlbayar=jmlbayar + {0} where nobukti='{1}'", jumlah, nobukti_sw)
                Using cmdtrsewa As OleDbCommand = New OleDbCommand(sqltrsewa, cn, sqltrans)
                    cmdtrsewa.ExecuteNonQuery()
                End Using

                Dim sqltoko As String = String.Format("update ms_toko set piutangsewa=piutangsewa - {0} where kd_toko='{1}'", jumlah, tkd_supir.Text.Trim)
                Using cmdtoko As OleDbCommand = New OleDbCommand(sqltoko, cn, sqltrans)
                    cmdtoko.ExecuteNonQuery()
                End Using

                If ada = False Then
                    Dim sqlins As String = String.Format("insert into tr_bayar_psw2 (nobukti,nobukti_sw,jumlah) values('{0}','{1}',{2})", tbukti.Text.Trim, nobukti_sw, jumlah)
                    Using cmins As OleDbCommand = New OleDbCommand(sqlins, cn, sqltrans)
                        cmins.ExecuteNonQuery()
                    End Using
                Else
                    Dim sqlup As String = String.Format("update tr_bayar_psw2 set jumlah={0} where nobukti='{1}' and nobukti_sw='{2}'", jumlah, tbukti.Text.Trim, nobukti_sw)
                    Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                        cmdup.ExecuteNonQuery()
                    End Using
                End If
                

            End If

        Next

        If Not hasil.Equals("error") Then
            hasil = "ok"
        End If

        Return hasil

    End Function

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("nobukti") = tbukti.Text.Trim
        orow("tanggal") = ttgl.Text.Trim
        '   orow("kd_toko") = tkd_supir.EditValue
        orow("nama_toko") = tnama_supir.Text.Trim
        orow("alamat_toko") = talamat.EditValue
        orow("note") = tnote.Text.Trim
        orow("jumlah") = GridView1.Columns("jumlah").SummaryItem.SummaryValue
        orow("sbatal") = 0

        dv.EndInit()

    End Sub

    Private Sub updateview()

        dv(position)("nobukti") = tbukti.Text.Trim
        dv(position)("tanggal") = ttgl.Text.Trim
        '   dv(position)("kd_toko") = tkd_supir.EditValue
        dv(position)("nama_toko") = tnama_supir.Text.Trim
        dv(position)("alamat_toko") = talamat.EditValue
        dv(position)("note") = tnote.Text.Trim
        dv(position)("jumlah") = GridView1.Columns("jumlah").SummaryItem.SummaryValue

    End Sub

    Private Sub btadd_Click(sender As System.Object, e As System.EventArgs) Handles btadd.Click
        Using fkar2 As New fbayar_psw3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0, .kdtoko = tkd_supir.Text.Trim}
            fkar2.ShowDialog(Me)
        End Using
    End Sub

    Private Sub btedit_Click(sender As System.Object, e As System.EventArgs) Handles btedit.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Using fkar2 As New fbayar_psw3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = Me.BindingContext(dv1).Position, .kdtoko = tkd_supir.Text.Trim}
            fkar2.ShowDialog(Me)
        End Using

    End Sub

    Private Sub btdel_Click(sender As System.Object, e As System.EventArgs) Handles btdel.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        If addstat = True Then
            dv1.Delete(Me.BindingContext(dv1).Position)
        Else

            Dim cn As OleDbConnection = Nothing
            Dim sqltrans As OleDbTransaction = Nothing

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                sqltrans = cn.BeginTransaction

                Dim jumlah As Double = Double.Parse(dv1(Me.BindingContext(dv1).Position)("jumlah").ToString)
                Dim nosewa As String = dv1(Me.BindingContext(dv1).Position)("nobukti_sw").ToString

                Dim sqltrsewa As String = String.Format("update trsewa3 set jmlbayar=jmlbayar - {0} where nobukti='{1}'", jumlah, nosewa)
                Using cmdtrsewa As OleDbCommand = New OleDbCommand(sqltrsewa, cn, sqltrans)
                    cmdtrsewa.ExecuteNonQuery()
                End Using

                Dim sqltoko As String = String.Format("update ms_toko set piutangsewa=piutangsewa + {0} where kd_toko='{1}'", jumlah, tkd_supir.Text.Trim)
                Using cmdtoko As OleDbCommand = New OleDbCommand(sqltoko, cn, sqltrans)
                    cmdtoko.ExecuteNonQuery()
                End Using

                Dim sqlbayar As String = String.Format("update tr_bayar_psw set jumlah=jumlah-{0} where nobukti='{1}'", jumlah, tbukti.Text.Trim)
                Using cmdbayar As OleDbCommand = New OleDbCommand(sqlbayar, cn, sqltrans)
                    cmdbayar.ExecuteNonQuery()
                End Using

                Dim sqldetail As String = String.Format("delete from tr_bayar_psw2 where noid={0}", dv1(Me.BindingContext(dv1).Position)("noid").ToString)
                Using cmddetail As OleDbCommand = New OleDbCommand(sqldetail, cn, sqltrans)
                    cmddetail.ExecuteNonQuery()
                End Using

                sqltrans.Commit()

                dv1.Delete(Me.BindingContext(dv1).Position)

                MsgBox("Data dihapus...", vbOKOnly + vbInformation, "Informasi")

langsung:


            Catch ex As Exception

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


        End If

    End Sub

    Private Sub bts_supir_Click(sender As System.Object, e As System.EventArgs) Handles bts_supir.Click

        Dim fs As New fsoutlet With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal}
        fs.ShowDialog(Me)

        tkd_supir.EditValue = fs.get_KODE
        tnama_supir.EditValue = fs.get_NAMA
        talamat.EditValue = fs.get_ALAMAT

    End Sub

    Private Sub tkd_supir_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_supir.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_supir_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_supir_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_supir.LostFocus
        If tkd_supir.Text.Trim.Length = 0 Then
            tkd_supir.Text = ""
            tnama_supir.Text = ""
        End If
    End Sub

    Private Sub tkd_supir_Validated(sender As Object, e As System.EventArgs) Handles tkd_supir.Validated
        If tkd_supir.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select kd_toko,nama_toko,alamat_toko from ms_toko where kd_toko='{0}' and aktif=1", tkd_supir.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_supir.EditValue = dread("kd_toko").ToString
                        tnama_supir.EditValue = dread("nama_toko").ToString
                        talamat.EditValue = dread("alamat_toko").ToString

                    Else
                        tkd_supir.EditValue = ""
                        tnama_supir.EditValue = ""
                        talamat.Text = ""


                    End If
                Else
                    tkd_supir.EditValue = ""
                    tnama_supir.EditValue = ""
                    talamat.Text = ""

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

        End If
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tkd_supir.EditValue = "" Then
            MsgBox("Outlet tidak boleh kosong..", vbOKOnly + vbInformation, "Informasi")
            tkd_supir.Focus()
            Return
        End If

        If IsNothing(dv1) Then
            MsgBox("Tidak ada nota yang diproses..", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If dv1.Count <= 0 Then
            MsgBox("Tidak ada nota yang diproses..", vbOKOnly + vbInformation, "Informasi")
            Return
        End If

        If MsgBox("Yakin sudah benar ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
            simpan()
        End If

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub fbeli2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub fbeli2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        ttgl.EditValue = Date.Now

        If addstat Then
            kosongkan()
        Else

            tkd_supir.Enabled = False
            bts_supir.Enabled = False

            isi()
        End If

    End Sub


End Class