Imports Grand.Clsmy
Imports System.Data
Imports System.Data.OleDb
Imports DevExpress.XtraBars

Public Class login

    Private Sub login_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        tuser.Focus()
    End Sub

    Private Sub btbatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btbatal.Click
        Application.Exit()
    End Sub

    Private Sub btmasuk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btmasuk.Click

        If tuser.Text = "" Then
            MsgBox("Nama user harus diisi", MsgBoxStyle.Information, "Informasi")
            tuser.Focus()
            Exit Sub
        End If

        If tpwd.Text = "" Then
            MsgBox("Password harus diisi", MsgBoxStyle.Information, "Informasi")
            tpwd.Focus()
            Exit Sub
        End If

        open_wait()

        Dim cn As OleDbConnection = New OleDbConnection
        userprog = tuser.Text.Trim
        pwd = tpwd.Text.Trim

        Try


            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select nonaktif,jenisuser,initial_us,alltoko from ms_usersys where namauser='{0}' and pwd=HASHBYTES('md5','{1}')", tuser.Text.Trim, tpwd.Text.Trim)
            Dim comd = New OleDbCommand(sql, cn)
            Dim dre As OleDbDataReader = comd.ExecuteReader

            If dre.Read Then

                If dre(0).ToString = "1" Then
                    close_wait()
                    MsgBox("User anda sudah tidak aktif, hubungi admin", vbOKOnly + vbInformation, "Informasi")
                Else

                    initial_user = dre("initial_us").ToString
                    ins_alltokouser = Integer.Parse(dre("alltoko").ToString)

                    setmenu()
                    setmenu2()
                    futama.tuserakt.Caption = "User : " & userprog.Trim
                    futama.tsserv.Caption = "Serv : " & servernam

                    Me.Close()


                End If

            Else
                close_wait()
                MsgBox("User/Password tidak ditemukan...", vbOKOnly + vbInformation, "Informasi")
                tuser.Focus()
            End If

            close_wait()

        Catch ex As Exception

            close_wait()

            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Informasi")

        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                    cn.Dispose()
                End If
            End If

        End Try


    End Sub

    Public Sub setmenu()

        Dim ds As DataSet
        Dim sql As String = String.Format("select a.kodemenu,a.t_active,a.t_add,a.t_edit,a.t_del,a.t_lap,b.namaform,b.submenu2,b.submenu1 from ms_usersys2 a inner join ms_menu b on a.kodemenu=b.kodemenu where a.namauser='{0}'", userprog.Trim)


        Dim cn2 As New OleDbConnection

        Dim fmuser As Integer = 0
        Dim fmmaster As Integer = 0
        Dim fmarea As Integer = 0
        Dim fmoutlet As Integer = 0
        Dim fmbarang_gudang As Integer = 0

        Dim fmgudang As Integer = 0
        Dim fmbelikirim As Integer = 0
        Dim fmto As Integer = 0
        Dim fmkanvas As Integer = 0
        Dim fmpinjamsewa As Integer = 0

        Dim fmpiutang As Integer = 0

        Try

            cn2 = Clsmy.open_conn

            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn2)

            dtmenu = New DataTable
            dtmenu.Clear()

            dtmenu = ds.Tables(0)

            If ds.Tables(0).Rows.Count > 0 Then


                Dim i As Integer = 0
                For i = 0 To futama.RibbonControl.Items.Count - 1
                    If TypeOf futama.RibbonControl.Items(i) Is BarButtonItem Then

                        Dim btnbar As BarButtonItem = CType(futama.RibbonControl.Items(i), BarButtonItem)

                        If btnbar.Name.ToString.Trim.Length > 0 Then

                            If btnbar.Name.Substring(0, 2).ToUpper = "NO" Or btnbar.Name.Substring(0, 1).ToUpper = "X" Or btnbar.Name.Substring(0, 3).ToUpper = "LAP" Then
                                btnbar.Visibility = BarItemVisibility.Always
                                btnbar.Enabled = True
                            Else

                                Dim namabar As String = btnbar.Name.Trim

                                Dim rows() As DataRow = dtmenu.Select(String.Format("kodemenu='{0}'", namabar))
                                Dim i2 As Integer = 0

                                If rows.Length = 0 Then
                                    btnbar.Visibility = BarItemVisibility.Never
                                    btnbar.Enabled = False
                                Else

                                    For i2 = 0 To rows.GetUpperBound(0)


                                        If Convert.ToInt16(rows(i2)("t_active")) = 1 Then

                                            btnbar.Visibility = BarItemVisibility.Always
                                            btnbar.Enabled = True

                                            If rows(i2)("submenu2").ToString.Equals("RibbonPageGroup1") Then
                                                fmuser = 1
                                            ElseIf rows(i2)("submenu2").ToString.Equals("RibbonPageGroup2") Then
                                                fmmaster = 1
                                            ElseIf rows(i2)("submenu2").ToString.Equals("RibbonPageGroup3") Then
                                                fmarea = 1
                                            ElseIf rows(i2)("submenu2").ToString.Equals("RibbonPageGroup4") Then
                                                fmoutlet = 1
                                            ElseIf rows(i2)("submenu2").ToString.Equals("RibbonPageGroup5") Then
                                                fmbarang_gudang = 1
                                            ElseIf rows(i2)("submenu2").ToString.Equals("RibbonPageGroup8") Then
                                                fmgudang = 1
                                            ElseIf rows(i2)("submenu2").ToString.Equals("RibbonPageGroup7") Then
                                                fmbelikirim = 1
                                            ElseIf rows(i2)("submenu2").ToString.Equals("RibbonPageGroup6") Then
                                                fmto = 1
                                            ElseIf rows(i2)("submenu2").ToString.Equals("RibbonPageGroup10") Then
                                                fmkanvas = 1
                                            ElseIf rows(i2)("submenu2").ToString.Equals("RibbonPageGroup11") Then
                                                fmpinjamsewa = 1
                                            ElseIf rows(i2)("submenu2").ToString.Equals("RibbonPageGroup13") Then
                                                fmpiutang = 1
                                            End If

                                        Else

                                            btnbar.Visibility = BarItemVisibility.Never
                                            btnbar.Enabled = False

                                        End If

                                    Next

                                End If

                            End If

                        End If


                    End If
                Next


                'If fmuser = 1 Then
                '    futama.RibbonPageGroup1.Visible = True
                'Else
                '    futama.RibbonPageGroup1.Visible = False
                'End If

                If fmmaster = 1 Then
                    futama.RibbonPageGroup2.Visible = True
                Else
                    futama.RibbonPageGroup2.Visible = False
                End If

                If fmarea = 1 Then
                    futama.RibbonPageGroup3.Visible = True
                Else
                    futama.RibbonPageGroup3.Visible = False
                End If

                If fmoutlet = 1 Then
                    futama.RibbonPageGroup4.Visible = True
                Else
                    futama.RibbonPageGroup4.Visible = False
                End If

                If fmbarang_gudang = 1 Then
                    futama.RibbonPageGroup5.Visible = True
                Else
                    futama.RibbonPageGroup5.Visible = False
                End If

                'If (fmuser + fmmaster + fmarea + fmoutlet + fmbarang_gudang) > 0 Then
                '    futama.RibbonPageGroup5.Visible = True
                'End If

                '--------------------------------------------

                If fmgudang = 1 Then
                    futama.RibbonPageGroup8.Visible = True
                Else
                    futama.RibbonPageGroup8.Visible = False
                End If


                If fmbelikirim = 1 Then
                    futama.RibbonPageGroup7.Visible = True
                Else
                    futama.RibbonPageGroup7.Visible = False
                End If

                If fmto = 1 Then
                    futama.RibbonPageGroup6.Visible = True
                Else
                    futama.RibbonPageGroup6.Visible = False
                End If

                If fmkanvas = 1 Then
                    futama.RibbonPageGroup10.Visible = True
                Else
                    futama.RibbonPageGroup10.Visible = False
                End If

                If fmpinjamsewa = 1 Then
                    futama.RibbonPageGroup11.Visible = True
                Else
                    futama.RibbonPageGroup11.Visible = False
                End If

                '---------------------------------------------------------

                If fmpiutang = 1 Then
                    futama.RibbonPageGroup13.Visible = True
                Else
                    futama.RibbonPageGroup13.Visible = False
                End If


                '------------------------------------------------

                futama.RibbonControl.Minimized = False


                ' futama.RibbonPageGroup16.Visible = True
                futama.RibbonPageGroup17.Visible = True
                futama.RibbonPageGroup14.Visible = True
                '  futama.RibbonPageGroup12.Visible = True
                ' futama.RibbonPageGroup15.Visible = True
                futama.RibbonPageGroup1.Visible = True

                '--------------------------------------

                futama.RibbonPage3.Visible = True

                If fmmaster = 1 Or fmarea = 1 Or fmoutlet = 1 Or fmbarang_gudang = 1 Then
                    futama.RibbonPage1.Visible = True
                Else
                    futama.RibbonPage1.Visible = False
                End If

                If fmgudang = 1 Or fmbelikirim = 1 Or fmto = 1 Or fmkanvas = 1 Or fmpinjamsewa = 1 Then
                    futama.rPenjualan.Visible = True
                Else
                    futama.rPenjualan.Visible = False
                End If

                If fmpiutang = 1 Then
                    futama.RibbonPage2.Visible = True
                Else
                    futama.RibbonPage2.Visible = False
                End If

                If Not cn2 Is Nothing Then
                    If cn2.State = ConnectionState.Open Then
                        cn2.Close()
                        cn2.Dispose()
                    End If
                End If


            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Informasi")
        Finally
            If Not cn2 Is Nothing Then
                If cn2.State = ConnectionState.Open Then
                    cn2.Close()
                    cn2.Dispose()
                End If
            End If
        End Try

    End Sub

    Public Sub setmenu2()

        Dim ds As DataSet
        Dim sql As String = String.Format("select a.kodemenu,a.t_lap,b.namaform from ms_usersys3 a inner join ms_menu b on a.kodemenu=b.kodemenu where a.namauser='{0}'", userprog.Trim)


        Dim cn2 As New OleDbConnection

        Try

            cn2 = Clsmy.open_conn

            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn2)

            dtmenu2 = New DataTable
            dtmenu2.Clear()

            dtmenu2 = ds.Tables(0)

                If Not cn2 Is Nothing Then
                    If cn2.State = ConnectionState.Open Then
                        cn2.Close()
                        cn2.Dispose()
                    End If
                End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Informasi")
        Finally
            If Not cn2 Is Nothing Then
                If cn2.State = ConnectionState.Open Then
                    cn2.Close()
                    cn2.Dispose()
                End If
            End If
        End Try

    End Sub

    Private Sub tuser_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tuser.KeyDown
        If e.KeyCode = 13 Then
            tpwd.Focus()
        End If
    End Sub

    Private Sub tpwd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tpwd.KeyDown
        If e.KeyCode = 13 Then
            btmasuk.PerformClick()
        End If
    End Sub



End Class