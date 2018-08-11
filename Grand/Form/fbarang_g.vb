Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fbarang_g

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private sadd, sedit, sdelete As Boolean
    Dim dtab As New DataTable

    Private Sub LoadGudang()

        Const sql As String = "select * from ms_gudang"
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

    Private Sub LoadBarang()

        grid2.DataSource = Nothing
        dv2 = Nothing
        dtab = Nothing

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Const sql As String = "select kd_barang,nama_barang,0 as ok,0 as jmlstok1,0 as jmlstok2,0 as jmlstok3," & _
            "0 as jmlstok_k1,0 as jmlstok_k2,0 as jmlstok_k3,0 as jmlstok_f1,0 as jmlstok_f2,0 as jmlstok_f3 from ms_barang where jenis='FISIK'"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet


        Try

            open_wait()



            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager2 = New DataViewManager(ds)
            dv2 = dvmanager2.CreateDataView(ds.Tables(0))

            grid2.DataSource = dv2

            load_by_barang(cn)

            If dv1(Me.BindingContext(dv1).Position)("tipe_gudang").Equals("FISIK") Then

                AdvBandedGridView1.Bands("umum").Visible = True

                AdvBandedGridView1.Bands("stokkomp").Visible = True
                AdvBandedGridView1.Bands("stokfisik").Visible = True

                AdvBandedGridView1.Bands("StokMobil").Visible = False


            Else

                AdvBandedGridView1.Bands("umum").Visible = True

                AdvBandedGridView1.Bands("stokkomp").Visible = False
                AdvBandedGridView1.Bands("stokfisik").Visible = False

                AdvBandedGridView1.Bands("StokMobil").Visible = True
                

            End If


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

    Private Sub load_by_barang(ByVal cn As OleDbConnection)

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        If IsNothing(dv2) Then
            Return
        End If

        If dv2.Count <= 0 Then
            Return
        End If

        Dim kd_gudang As String = dv1(Me.BindingContext(dv1).Position)("kd_gudang").ToString


        Dim sql As String = String.Format("select * from ms_barang2 where kd_gudang='{0}'", kd_gudang)
        Dim ds_s As New DataSet
        ds_s = Clsmy.GetDataSet(sql, cn)
        dtab = ds_s.Tables(0)

        Dim kdbar As String
        Dim jmlstok1, jmlstok2, jmlstok3 As String
        Dim jmlstok_k1, jmlstok_k2, jmlstok_k3 As String
        Dim jmlstok_f1, jmlstok_f2, jmlstok_f3 As String
        Dim expres As String = ""

        For i As Integer = 0 To dv2.Count - 1

            kdbar = dv2(i)("kd_barang").ToString
            jmlstok1 = dv2(i)("jmlstok1").ToString
            jmlstok2 = dv2(i)("jmlstok2").ToString
            jmlstok3 = dv2(i)("jmlstok3").ToString

            jmlstok_k1 = dv2(i)("jmlstok_k1").ToString
            jmlstok_k2 = dv2(i)("jmlstok_k2").ToString
            jmlstok_k3 = dv2(i)("jmlstok_k3").ToString

            jmlstok_f1 = dv2(i)("jmlstok_f1").ToString
            jmlstok_f2 = dv2(i)("jmlstok_f2").ToString
            jmlstok_f3 = dv2(i)("jmlstok_f3").ToString

            expres = String.Format("kd_barang='{0}'", kdbar)
            Dim drows() As DataRow = dtab.Select(expres)

            If drows.Length > 0 Then

                dv2(i)("jmlstok1") = drows(0)("jmlstok1")
                dv2(i)("jmlstok2") = drows(0)("jmlstok2")
                dv2(i)("jmlstok3") = drows(0)("jmlstok3")

                dv2(i)("jmlstok_k1") = drows(0)("jmlstok_k1")
                dv2(i)("jmlstok_k2") = drows(0)("jmlstok_k2")
                dv2(i)("jmlstok_k3") = drows(0)("jmlstok_k3")

                dv2(i)("jmlstok_f1") = drows(0)("jmlstok_f1")
                dv2(i)("jmlstok_f2") = drows(0)("jmlstok_f2")
                dv2(i)("jmlstok_f3") = drows(0)("jmlstok_f3")

                dv2(i)("ok") = 1

            Else
                dv2(i)("ok") = 0
            End If

        Next

    End Sub

    Private Sub Get_Aksesform()

        Dim rows() As DataRow = dtmenu.Select(String.Format("NAMAFORM='{0}'", Me.Name.ToUpper.ToString))

        If Convert.ToInt16(rows(0)("t_add")) = 1 Then
            sadd = True
        Else
            sadd = False
        End If

        If Convert.ToInt16(rows(0)("t_edit")) = 1 Then
            sedit = True
        Else
            sedit = False
        End If

        If Convert.ToInt16(rows(0)("t_del")) = 1 Then
            sdelete = True
        Else
            sdelete = False
        End If

    End Sub

    Private Sub simpan()

        If dtab.Rows.Count <= 0 Then
            If sadd = False Then
                MsgBox("Anda tidak berhak untuk proses penambahan barang digudang,hub administrator prog", vbOKOnly + vbInformation, "Informasi")
                Return
            End If
        End If

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            'Dim finf As New finfo With {.StartPosition = FormStartPosition.CenterParent}
            'finf.ShowDialog(Me)
            'finf.Hide()

            'Dim dvinf As DataView = finf.dv1

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            Dim kdgudang As String = dv1(Me.BindingContext(dv1).Position)("kd_gudang").ToString
            Dim expres As String = ""

            Dim kdbar As String = ""
            Dim statok As String = ""
            Dim jmlstok1, jmlstok2, jmlstok3 As Integer
            Dim jmlstok_k1, jmlstok_k2, jmlstok_k3 As Integer
            Dim jmlstok_f1, jmlstok_f2, jmlstok_f3 As Integer

            Dim comd As OleDbCommand
            Dim stater As String = 0
            Dim kdbarerr As String = ""

            For i As Integer = 0 To dv2.Count - 1

                'Dim orow As DataRowView = dvinf.AddNew


                kdbar = dv2(i)("kd_barang").ToString
                statok = dv2(i)("ok").ToString


                Dim sqlinsert As String = String.Format("insert into ms_barang2 (kd_gudang,kd_barang) values('{0}','{1}')", kdgudang, kdbar)
                Dim sqldel As String = String.Format("delete from ms_barang2 where kd_gudang='{0}' and kd_barang='{1}'", kdgudang, kdbar)

                jmlstok1 = Integer.Parse(dv2(i)("jmlstok1").ToString)
                jmlstok2 = Integer.Parse(dv2(i)("jmlstok2").ToString)
                jmlstok3 = Integer.Parse(dv2(i)("jmlstok3").ToString)

                jmlstok_k1 = Integer.Parse(dv2(i)("jmlstok_k1").ToString)
                jmlstok_k2 = Integer.Parse(dv2(i)("jmlstok_k2").ToString)
                jmlstok_k3 = Integer.Parse(dv2(i)("jmlstok_k3").ToString)

                jmlstok_f1 = Integer.Parse(dv2(i)("jmlstok_f1").ToString)
                jmlstok_f2 = Integer.Parse(dv2(i)("jmlstok_f2").ToString)
                jmlstok_f3 = Integer.Parse(dv2(i)("jmlstok_f3").ToString)

                expres = String.Format("kd_barang='{0}'", kdbar)
                Dim drows() As DataRow = dtab.Select(expres)

                If drows.Length > 0 Then

                    If statok.Equals("0") Then

                        If jmlstok1 > 0 Or jmlstok2 > 0 Or jmlstok3 > 0 Or jmlstok_k1 > 0 Or jmlstok_k2 > 0 Or jmlstok_k3 > 0 Or jmlstok_f1 > 0 Or jmlstok_f2 > 0 Or jmlstok_f3 > 0 Then

                            'orow("ket1") = String.Format("{0}. {1}", i + 1, kdbar)
                            'orow("ket2") = "Tidak dapat dirubah karna masih ada stok"

                            stater = 1
                            kdbarerr = kdbar

                            GoTo sini

                        Else

                            If sdelete = False Then
                                'orow("ket1") = String.Format("{0}. {1}", i + 1, kdbar)
                                'orow("ket2") = "Del-Gagal (Hub Administrator)"

                                stater = 2
                                kdbarerr = kdbar

                                GoTo sini

                            Else
                                comd = New OleDbCommand(sqldel, cn, sqltrans)
                                comd.ExecuteNonQuery()

                                Clsmy.InsertToLog(cn, "btbarang_g", 0, 0, 1, 0, kdbar, kdgudang, sqltrans)

                                'orow("ket1") = String.Format("{0}. {1}", i + 1, kdbar)
                                'orow("ket2") = "OK (Del)"
                            End If

                        End If

                    Else
                        'comd = New OleDbCommand(sqlinsert, cn, sqltrans)
                        'comd.ExecuteNonQuery()

                        'orow("ket1") = String.Format("{0}. {1}", i + 1, kdbar)
                        'orow("ket2") = "OK (Ins)"
                    End If

                Else

                    If statok.Equals("1") Then

                        If sadd = False Then
                            'orow("ket1") = String.Format("{0}. {1}", i + 1, kdbar)
                            'orow("ket2") = "Ins-Gagal (Hub Administrator)"

                            stater = 3
                            kdbarerr = kdbar

                            GoTo sini

                        Else
                            comd = New OleDbCommand(sqlinsert, cn, sqltrans)
                            comd.ExecuteNonQuery()

                            Clsmy.InsertToLog(cn, "btbarang_g", 1, 0, 0, 0, kdbar, kdgudang, sqltrans)

                            'orow("ket1") = String.Format("{0}. {1}", i + 1, kdbar)
                            'orow("ket2") = "OK (Ins)"
                        End If

                        
                    End If

                End If

                'dvinf.EndInit()

                Application.DoEvents()

            Next

            sqltrans.Commit()
            close_wait()
            MsgBox("Proses Selesai...", vbOKOnly + vbInformation, "Informasi")

sini:

            Select Case stater
                Case 1
                    close_wait()
                    sqltrans.Rollback()
                    MsgBox(kdbarerr & " - Data Stok sudah masuk, tidak dapat dirubah....", vbOKOnly + vbInformation, "Informasi")
                Case 2
                    close_wait()
                    sqltrans.Rollback()
                    MsgBox(kdbarerr & " - Del Gagal (Hub Administrator)", vbOKOnly + vbInformation, "Informasi")
                Case 3
                    close_wait()
                    sqltrans.Rollback()
                    MsgBox(kdbarerr & " - Ins Gagal (Hub Administrator)", vbOKOnly + vbInformation, "Informasi")
            End Select



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

    Private Sub ComboBoxEdit1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBoxEdit1.SelectedIndexChanged

        If IsNothing(dv2) Then
            Exit Sub
        End If

        If dv2.Count > 0 Then

            Dim a As Integer = 0
            For a = 0 To dv2.Count - 1

                If ComboBoxEdit1.SelectedIndex = 1 Then

                    dv2(a)("ok") = 1 ' lap

                ElseIf ComboBoxEdit1.SelectedIndex = 2 Then

                    dv2(a)("ok") = 0 ' lap

                End If

            Next


            grid2.Refresh()

        End If

    End Sub

    Private Sub GridView1_Click(sender As Object, e As System.EventArgs) Handles GridView1.Click
        GridView1_SelectionChanged(sender, Nothing)
    End Sub

    Private Sub GridView1_SelectionChanged(sender As System.Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles GridView1.SelectionChanged

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        LoadBarang()

    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        GridView1_SelectionChanged(sender, Nothing)
    End Sub

    Private Sub fbarang_g_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        GridView1.Focus()
    End Sub

    Private Sub fbarang_g_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ComboBoxEdit1.SelectedIndex = 0

        Get_Aksesform()
        LoadGudang()


    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btsave_Click(sender As System.Object, e As System.EventArgs) Handles btsave.Click

        If IsNothing(dv1) Then
            Return
        End If

        If IsNothing(dv2) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        If dv2.Count <= 0 Then
            Return
        End If

        If MsgBox("Yakin sudah benar... ?", vbYesNo + vbInformation, "Informasi") = vbNo Then
            Return
        End If

        simpan()

    End Sub


End Class