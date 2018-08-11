Imports DevExpress.XtraSplashScreen
Imports System.Globalization

Imports System.Windows.Forms
Imports System.Reflection

Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Module Mdlmy

    Public userprog, pwd As String
    Public initial_user As String
    Public dtmenu As DataTable
    Public dtmenu2 As DataTable
    Public ins_alltokouser As Integer
    Public servernam As String

    Public tglperiod1, tglperiod2 As String

    Public dsinvoice_ku As DataSet

    Public varprinter1, varprinter2, varos As String



    Public Class ObjectFinder
        Public Shared Function CreateObjectInstance(ByVal objectName As String) As Object
            ' Creates and returns an instance of any object in the assembly by its type name.

            Dim obj As Object

            Try
                If objectName.LastIndexOf(".") = -1 Then
                    'Appends the root namespace if not specified.
                    objectName = String.Format("{0}.{1}", [Assembly].GetEntryAssembly.GetName.Name, objectName)
                End If

                obj = [Assembly].GetEntryAssembly.CreateInstance(objectName)

            Catch ex As Exception
                obj = Nothing
            End Try
            Return obj

        End Function

        Public Shared Function CreateForm(ByVal formName As String) As Form
            ' Return the instance of the form by specifying its name.
            Return DirectCast(CreateObjectInstance(formName), Form)
        End Function

    End Class

    Public Sub open_wait()

        SplashScreenManager.ShowForm(futama, GetType(waitf), True, True, False)
        ' SplashScreenManager.Default.SetWaitFormDescription("lagi ngetest")

        '  Dlg = New DevExpress.Utils.WaitDialogForm("Loading Components...")
    End Sub

    Public Sub open_wait(ByVal capt As String)

        SplashScreenManager.ShowForm(futama, GetType(waitf), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription(capt)

        '  Dlg = New DevExpress.Utils.WaitDialogForm("Loading Components...")
    End Sub

    Public Sub SetWaitDialog(ByVal capt As String)

        ' SplashScreenManager.ShowForm(futama, GetType(waitf), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription(capt)

        '  Dlg = New DevExpress.Utils.WaitDialogForm("Loading Components...")
    End Sub

    Public Sub open_wait2(ByVal formm As Form)
        SplashScreenManager.ShowForm(formm, GetType(waitf), True, True, False)
    End Sub

    Public Sub close_wait()
        SplashScreenManager.CloseForm(False)
    End Sub

    Public Function convert_date_to_eng(ByVal valdate As String) As String

        If valdate = "" Then
            Return ""
        End If

        valdate = CType(valdate, DateTime).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"))

        Return valdate

    End Function

    Public Function convert_datetime_to_eng(ByVal valdate As String) As String

        If valdate = "" Then
            Return ""
        End If

        valdate = CType(valdate, DateTime).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))

        Return valdate

    End Function

    Public Function Hist_PinjamSewa_Toko(ByVal kdtoko As String, ByVal kdbarang As String, ByVal qtykecil As Integer, ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal add As Boolean, ByVal transpinjam As Boolean) As String

        Dim sqlc As String = String.Format("select * from ms_toko3 where kd_toko='{0}' and kd_barang='{1}'", kdtoko, kdbarang)
        Dim cmds As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
        Dim drc As OleDbDataReader = cmds.ExecuteReader

        Dim sqlb As String = String.Format("select kd_barang,qty1,qty2,qty3 from ms_barang where jenis='FISIK' and kd_barang='{0}'", kdbarang)

        Dim hasilqty As Integer
        Dim qty1, qty2, qty3 As Integer

        Dim jml1 As Integer
        Dim jml2 As Integer
        Dim jml3 As Integer

        Dim konferror As String = ""
        Dim barangtoko As Boolean = False

        If drc.Read Then

            If Not drc("kd_toko").ToString.Equals("") Then

                If transpinjam = True Then
                    If add = True Then
                        hasilqty = Integer.Parse(drc("jml_pinjam").ToString) + qtykecil
                    Else
                        hasilqty = Integer.Parse(drc("jml_pinjam").ToString) - qtykecil
                    End If

                Else

                    If add = True Then
                        hasilqty = Integer.Parse(drc("jml_sewa").ToString) + qtykecil
                    Else
                        hasilqty = Integer.Parse(drc("jml_sewa").ToString) - qtykecil
                    End If

                End If



                barangtoko = True

                GoTo lewati

            End If

        End If

        ' kalau tidak ketemu

        If add = True Then
            hasilqty = qtykecil
        Else
            konferror = kdbarang & " tidak ditemukan dalam hist outlet"
            GoTo langsung
        End If

lewati:

        Dim cmdb As OleDbCommand = New OleDbCommand(sqlb, cn, sqltrans)
        Dim drb As OleDbDataReader = cmdb.ExecuteReader

        If drb.Read Then
            If drb("kd_barang").Equals("") Then
                qty1 = 0
                qty2 = 0
                qty3 = 0
            Else

                qty1 = Integer.Parse(drb("qty1").ToString)
                qty2 = Integer.Parse(drb("qty2").ToString)
                qty3 = Integer.Parse(drb("qty3").ToString)

            End If
        Else
            qty1 = 0
            qty2 = 0
            qty3 = 0
        End If



        If qty1 > 0 Then

            Dim totqty As Integer = qty1 * qty2 * qty3

            If hasilqty >= totqty Then

                Dim sisa As Integer = hasilqty Mod totqty

                jml1 = (hasilqty - sisa) / totqty

                If sisa > qty2 Then
                    jml2 = sisa
                    sisa = sisa Mod qty2

                    jml2 = (jml2 - sisa) / qty2
                    jml3 = sisa

                Else
                    jml2 = sisa
                    jml3 = 0
                End If

            End If

        End If

        Dim sql As String = ""

        If transpinjam = True Then

            If barangtoko = False Then
                sql = String.Format("insert into ms_toko3 (kd_toko,kd_barang,jml_pinjam1,jml_pinjam2,jml_pinjam3,jml_pinjam) values('{0}','{1}',{2},{3},{4},{5})", kdtoko, kdbarang, jml1, jml2, jml3, hasilqty)
            Else
                sql = String.Format("update ms_toko3 set jml_pinjam1={0},jml_pinjam2={1},jml_pinjam3={2},jml_pinjam={3} where kd_toko='{4}' and kd_barang='{5}'", jml1, jml2, jml3, hasilqty, kdtoko, kdbarang)
            End If


        Else

            If barangtoko = False Then
                sql = String.Format("insert into ms_toko3 (kd_toko,kd_barang,jml_sewa1,jml_sewa2,jml_sewa3,jml_sewa) values('{0}','{1}',{2},{3},{4},{5})", kdtoko, kdbarang, jml1, jml2, jml3, hasilqty)
            Else
                sql = String.Format("update ms_toko3 set jml_sewa1={0},jml_sewa2={1},jml_sewa3={2},jml_sewa={3} where kd_toko='{4}' and kd_barang='{5}'", jml1, jml2, jml3, hasilqty, kdtoko, kdbarang)
            End If


        End If



        Using cmdok As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
            cmdok.ExecuteNonQuery()
        End Using

        konferror = "ok"

langsung:

        Return konferror

    End Function

    Public Function convert_date_to_ind(ByVal valdate As String) As String

        If valdate = "" Then
            Return ""
        End If

        valdate = CType(valdate, Date).ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("id-ID"))

        Return valdate

    End Function

    Public Function PlusMin_Barang(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal qtykecil As Integer, _
                                   ByVal kdbar As String, ByVal kdgud As String, ByVal additem As Boolean, _
                                   ByVal perhitungansaja As Boolean, ByVal hanyabarang2 As Boolean, Optional ByVal nonkonfirm_min As Boolean = True) As String

        Dim cmdcek_brang As OleDbCommand
        Dim drd_barang As OleDbDataReader

        Dim sqlcekbarang As String = String.Format("select a.kd_posisi,a.jmlstok,b.qty1,b.qty2,b.qty3,b.jmlstok as jmlstok2 from ms_barang2 a inner join ms_barang b on a.kd_barang=b.kd_barang where b.jenis='FISIK' and a.kd_barang='{0}' and a.kd_gudang='{1}'", kdbar, kdgud)
        cmdcek_brang = New OleDbCommand(sqlcekbarang, cn, sqltrans)
        drd_barang = cmdcek_brang.ExecuteReader

        Dim konferror As String = ""

        If drd_barang.HasRows Then
            If drd_barang.Read Then
                If IsNumeric(drd_barang("kd_posisi").ToString) Then


                    Dim jmlstok As Integer = Integer.Parse(drd_barang("jmlstok").ToString)
                    Dim jmlstok2 As Integer = Integer.Parse(drd_barang("jmlstok2").ToString)

                    Dim qty1 As Integer = Integer.Parse(drd_barang("qty1").ToString)
                    Dim qty2 As Integer = Integer.Parse(drd_barang("qty2").ToString)
                    Dim qty3 As Integer = Integer.Parse(drd_barang("qty3").ToString)

                    Dim jml1, jml2, jml3 As Integer
                    Dim jml11, jml12, jml13 As Integer

                    Dim totqty As Integer = qty1 * qty2 * qty3

                    If additem = True Then

                        jmlstok = jmlstok + qtykecil
                        jmlstok2 = jmlstok2 + qtykecil

                    Else

                        If nonkonfirm_min = True Then
                            If jmlstok < qtykecil Then
                                konferror = kdbar & " melebihi stok yang ada..."
                            End If
                        End If

                        jmlstok = jmlstok - qtykecil
                        jmlstok2 = jmlstok2 - qtykecil

                    End If

                    ' by gudang

                    If jmlstok >= totqty Then

                        Dim sisa As Integer = jmlstok Mod totqty

                        jml1 = (jmlstok - sisa) / totqty

                        If sisa > qty2 Then
                            jml2 = sisa
                            sisa = sisa Mod qty2

                            jml2 = (jml2 - sisa) / qty2
                            jml3 = sisa

                        Else
                            jml2 = sisa
                            jml3 = 0
                        End If
                    End If

                    ' end by gudang

                    ' by item

                    If jmlstok2 >= totqty Then

                        Dim sisa As Integer = jmlstok2 Mod totqty

                        jml11 = (jmlstok2 - sisa) / totqty

                        If sisa > qty2 Then
                            jml12 = sisa
                            sisa = sisa Mod qty2

                            jml12 = (jml12 - sisa) / qty2
                            jml13 = sisa

                        Else
                            jml12 = sisa
                            jml13 = 0
                        End If
                    End If

                    ' end by item                   


                    If konferror.Equals("") Then

                        If perhitungansaja = False Then

                            Dim sqlup1 As String = String.Format("update ms_barang2 set jmlstok1={0},jmlstok2={1},jmlstok3={2},jmlstok={3} where kd_gudang='{4}' and kd_barang='{5}'", jml1, jml2, jml3, jmlstok, kdgud, kdbar)
                            Dim sqlup As String = String.Format("update ms_barang set jmlstok1={0},jmlstok2={1},jmlstok3={2},jmlstok={3} where kd_barang='{4}'", jml11, jml12, jml13, jmlstok2, kdbar)

                            Using cmd1 As OleDbCommand = New OleDbCommand(sqlup1, cn, sqltrans)
                                cmd1.ExecuteNonQuery()
                            End Using

                            If hanyabarang2 = False Then
                                Using cmd2 As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                                    cmd2.ExecuteNonQuery()
                                End Using
                            End If


                        End If

                        konferror = "ok"

                    End If

                Else

                    konferror = kdbar & " tidak ditemukan..."

                End If
            Else
                konferror = kdbar & " tidak ditemukan..."
            End If
        Else
            konferror = kdbar & " tidak ditemukan..."
        End If

        drd_barang.Close()

        Return konferror

    End Function

    Public Function PlusMin_Barang_Fsk(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal qtykecil As Integer, _
                                   ByVal kdbar As String, ByVal kdgud As String, ByVal additem As Boolean, _
                                   ByVal perhitungansaja As Boolean, ByVal hanyabarang2 As Boolean) As String

        Dim cmdcek_brang As OleDbCommand
        Dim drd_barang As OleDbDataReader

        Dim sqlcekbarang As String = String.Format("select a.kd_posisi,a.jmlstok_f as jmlstok,b.qty1,b.qty2,b.qty3,b.jmlstok_f as jmlstok2 from ms_barang2 a inner join ms_barang b on a.kd_barang=b.kd_barang where b.jenis='FISIK' and a.kd_barang='{0}' and a.kd_gudang='{1}'", kdbar, kdgud)
        cmdcek_brang = New OleDbCommand(sqlcekbarang, cn, sqltrans)
        drd_barang = cmdcek_brang.ExecuteReader

        Dim konferror As String = ""

        If drd_barang.HasRows Then
            If drd_barang.Read Then
                If IsNumeric(drd_barang("kd_posisi").ToString) Then


                    Dim jmlstok As Integer = Integer.Parse(drd_barang("jmlstok").ToString)
                    Dim jmlstok2 As Integer = Integer.Parse(drd_barang("jmlstok2").ToString)

                    Dim qty1 As Integer = Integer.Parse(drd_barang("qty1").ToString)
                    Dim qty2 As Integer = Integer.Parse(drd_barang("qty2").ToString)
                    Dim qty3 As Integer = Integer.Parse(drd_barang("qty3").ToString)

                    Dim jml1, jml2, jml3 As Integer
                    Dim jml11, jml12, jml13 As Integer

                    Dim totqty As Integer = qty1 * qty2 * qty3

                    If additem = True Then

                        jmlstok = jmlstok + qtykecil
                        jmlstok2 = jmlstok2 + qtykecil

                    Else

                        If jmlstok < qtykecil Then
                            konferror = kdbar & " melebihi stok yang ada..."
                        End If

                        jmlstok = jmlstok - qtykecil
                        jmlstok2 = jmlstok2 - qtykecil

                    End If

                    ' by gudang

                    If jmlstok >= totqty Then

                        Dim sisa As Integer = jmlstok Mod totqty

                        jml1 = (jmlstok - sisa) / totqty

                        If sisa > qty2 Then
                            jml2 = sisa
                            sisa = sisa Mod qty2

                            jml2 = (jml2 - sisa) / qty2
                            jml3 = sisa

                        Else
                            jml2 = sisa
                            jml3 = 0
                        End If
                    End If

                    ' end by gudang

                    ' by item

                    If jmlstok2 >= totqty Then

                        Dim sisa As Integer = jmlstok2 Mod totqty

                        jml11 = (jmlstok2 - sisa) / totqty

                        If sisa > qty2 Then
                            jml12 = sisa
                            sisa = sisa Mod qty2

                            jml12 = (jml12 - sisa) / qty2
                            jml13 = sisa

                        Else
                            jml12 = sisa
                            jml13 = 0
                        End If
                    End If

                    ' end by item                   


                    If konferror.Equals("") Then

                        If perhitungansaja = False Then



                            Dim sqlup1 As String = String.Format("update ms_barang2 set jmlstok_f1={0},jmlstok_f2={1},jmlstok_f3={2},jmlstok_f={3} where kd_gudang='{4}' and kd_barang='{5}'", jml1, jml2, jml3, jmlstok, kdgud, kdbar)
                            Dim sqlup As String = String.Format("update ms_barang set jmlstok_f1={0},jmlstok_f2={1},jmlstok_f3={2},jmlstok_f={3} where kd_barang='{4}'", jml11, jml12, jml13, jmlstok2, kdbar)

                            Using cmd1 As OleDbCommand = New OleDbCommand(sqlup1, cn, sqltrans)
                                cmd1.ExecuteNonQuery()
                            End Using

                            If hanyabarang2 = False Then
                                Using cmd2 As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                                    cmd2.ExecuteNonQuery()
                                End Using
                            End If



                        End If

                        konferror = "ok"

                    End If

                Else

                    konferror = kdbar & " tidak ditemukan..."

                End If
            Else
                konferror = kdbar & " tidak ditemukan..."
            End If
        Else
            konferror = kdbar & " tidak ditemukan..."
        End If

        drd_barang.Close()

        Return konferror

    End Function

    Public Function PlusMin_Barang_Kend(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal qtykecil As Integer, _
                                   ByVal kdbar As String, ByVal kdgud As String, ByVal additem As Boolean, _
                                   ByVal perhitungansaja As Boolean, ByVal hanyabarang2 As Boolean) As String

        Dim cmdcek_brang As OleDbCommand
        Dim drd_barang As OleDbDataReader

        Dim sqlcekbarang As String = String.Format("select a.kd_posisi,a.jmlstok_k as jmlstok,b.qty1,b.qty2,b.qty3,b.jmlstok_k as jmlstok2 from ms_barang2 a inner join ms_barang b on a.kd_barang=b.kd_barang where b.jenis='FISIK' and a.kd_barang='{0}' and a.kd_gudang='{1}'", kdbar, kdgud)
        cmdcek_brang = New OleDbCommand(sqlcekbarang, cn, sqltrans)
        drd_barang = cmdcek_brang.ExecuteReader

        Dim konferror As String = ""

        If drd_barang.HasRows Then
            If drd_barang.Read Then
                If IsNumeric(drd_barang("kd_posisi").ToString) Then


                    Dim jmlstok As Integer = Integer.Parse(drd_barang("jmlstok").ToString)
                    Dim jmlstok2 As Integer = Integer.Parse(drd_barang("jmlstok2").ToString)

                    Dim qty1 As Integer = Integer.Parse(drd_barang("qty1").ToString)
                    Dim qty2 As Integer = Integer.Parse(drd_barang("qty2").ToString)
                    Dim qty3 As Integer = Integer.Parse(drd_barang("qty3").ToString)

                    Dim jml1, jml2, jml3 As Integer
                    Dim jml11, jml12, jml13 As Integer

                    Dim totqty As Integer = qty1 * qty2 * qty3

                    If additem = True Then

                        jmlstok = jmlstok + qtykecil
                        jmlstok2 = jmlstok2 + qtykecil

                    Else

                        If jmlstok < qtykecil Then
                            konferror = kdbar & " melebihi stok yang ada..."
                        End If

                        jmlstok = jmlstok - qtykecil
                        jmlstok2 = jmlstok2 - qtykecil

                    End If

                    ' by gudang

                    If jmlstok >= totqty Then

                        Dim sisa As Integer = jmlstok Mod totqty

                        jml1 = (jmlstok - sisa) / totqty

                        If sisa > qty2 Then
                            jml2 = sisa
                            sisa = sisa Mod qty2

                            jml2 = (jml2 - sisa) / qty2
                            jml3 = sisa

                        Else
                            jml2 = sisa
                            jml3 = 0
                        End If
                    End If

                    ' end by gudang

                    ' by item

                    If jmlstok2 >= totqty Then

                        Dim sisa As Integer = jmlstok2 Mod totqty

                        jml11 = (jmlstok2 - sisa) / totqty

                        If sisa > qty2 Then
                            jml12 = sisa
                            sisa = sisa Mod qty2

                            jml12 = (jml12 - sisa) / qty2
                            jml13 = sisa

                        Else
                            jml12 = sisa
                            jml13 = 0
                        End If
                    End If

                    ' end by item                   


                    If konferror.Equals("") Then

                        If perhitungansaja = False Then

                            Dim sqlup1 As String = String.Format("update ms_barang2 set jmlstok_k1={0},jmlstok_k2={1},jmlstok_k3={2},jmlstok_k={3} where kd_gudang='{4}' and kd_barang='{5}'", jml1, jml2, jml3, jmlstok, kdgud, kdbar)
                            Dim sqlup As String = String.Format("update ms_barang set jmlstok_k1={0},jmlstok_k2={1},jmlstok_k3={2},jmlstok_k={3} where kd_barang='{4}'", jml11, jml12, jml13, jmlstok2, kdbar)

                            Using cmd1 As OleDbCommand = New OleDbCommand(sqlup1, cn, sqltrans)
                                cmd1.ExecuteNonQuery()
                            End Using

                            If hanyabarang2 = False Then
                                Using cmd2 As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                                    cmd2.ExecuteNonQuery()
                                End Using
                            End If

                        End If

                        konferror = "ok"

                    End If

                Else

                    konferror = kdbar & " tidak ditemukan..."

                End If
            Else
                konferror = kdbar & " tidak ditemukan..."
            End If
        Else
            konferror = kdbar & " tidak ditemukan..."
        End If

        drd_barang.Close()

        Return konferror

    End Function


    '' khusus adjustment
    Public Function PlusMin_Barang_Adj(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal qtykecil As Integer, ByVal qtyselisih As Integer, _
                                   ByVal kdbar As String, ByVal kdgud As String, ByVal additem As Boolean) As String

        Dim cmdcek_brang As OleDbCommand
        Dim drd_barang As OleDbDataReader

        Dim sqlcekbarang As String = String.Format("select a.kd_posisi,a.jmlstok,b.qty1,b.qty2,b.qty3,b.jmlstok as jmlstok2 from ms_barang2 a inner join ms_barang b on a.kd_barang=b.kd_barang where b.jenis='FISIK' and a.kd_barang='{0}' and a.kd_gudang='{1}'", kdbar, kdgud)
        cmdcek_brang = New OleDbCommand(sqlcekbarang, cn, sqltrans)
        drd_barang = cmdcek_brang.ExecuteReader

        Dim konferror As String = ""

        If drd_barang.HasRows Then
            If drd_barang.Read Then
                If IsNumeric(drd_barang("kd_posisi").ToString) Then


                    Dim jmlstok As Integer = Integer.Parse(drd_barang("jmlstok").ToString)
                    Dim jmlstok2 As Integer = Integer.Parse(drd_barang("jmlstok2").ToString)

                    Dim qty1 As Integer = Integer.Parse(drd_barang("qty1").ToString)
                    Dim qty2 As Integer = Integer.Parse(drd_barang("qty2").ToString)
                    Dim qty3 As Integer = Integer.Parse(drd_barang("qty3").ToString)

                    Dim jml1, jml2, jml3 As Integer
                    Dim jml11, jml12, jml13 As Integer

                    Dim totqty As Integer = qty1 * qty2 * qty3

                    If additem = True Then

                        jmlstok = jmlstok + qtyselisih


                        If qtykecil < 0 Then
                            jmlstok2 = jmlstok2 - Math.Abs(qtyselisih)
                        Else
                            jmlstok2 = jmlstok2 + qtyselisih
                        End If

                    Else

                        'If jmlstok < qtykecil Then
                        '    konferror = kdbar & " melebihi stok yang ada..."
                        'End If

                        jmlstok2 = jmlstok2 - jmlstok

                        jmlstok = qtykecil

                        jmlstok2 = jmlstok2 + qtykecil

                    End If

                    ' by gudang

                    If jmlstok >= totqty Then

                        Dim sisa As Integer = jmlstok Mod totqty

                        jml1 = (jmlstok - sisa) / totqty

                        If sisa > qty2 Then
                            jml2 = sisa
                            sisa = sisa Mod qty2

                            jml2 = (jml2 - sisa) / qty2
                            jml3 = sisa

                        Else
                            jml2 = sisa
                            jml3 = 0
                        End If
                    End If

                    ' end by gudang

                    ' by item

                    If jmlstok2 >= totqty Then

                        Dim sisa As Integer = jmlstok2 Mod totqty

                        jml11 = (jmlstok2 - sisa) / totqty

                        If sisa > qty2 Then
                            jml12 = sisa
                            sisa = sisa Mod qty2

                            jml12 = (jml12 - sisa) / qty2
                            jml13 = sisa

                        Else
                            jml12 = sisa
                            jml13 = 0
                        End If
                    End If

                    ' end by item                   


                    If konferror.Equals("") Then


                        Dim sqlup1 As String = String.Format("update ms_barang2 set jmlstok1={0},jmlstok2={1},jmlstok3={2},jmlstok={3} where kd_gudang='{4}' and kd_barang='{5}'", jml1, jml2, jml3, jmlstok, kdgud, kdbar)
                        Dim sqlup As String = String.Format("update ms_barang set jmlstok1={0},jmlstok2={1},jmlstok3={2},jmlstok={3} where kd_barang='{4}'", jml11, jml12, jml13, jmlstok2, kdbar)

                        Using cmd1 As OleDbCommand = New OleDbCommand(sqlup1, cn, sqltrans)
                            cmd1.ExecuteNonQuery()
                        End Using

                        Using cmd2 As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                            cmd2.ExecuteNonQuery()
                        End Using


                    End If

                    konferror = "ok"



                Else

                    konferror = kdbar & " tidak ditemukan..."

                End If
            Else
                konferror = kdbar & " tidak ditemukan..."
            End If
        Else
            konferror = kdbar & " tidak ditemukan..."
        End If

        drd_barang.Close()

        Return konferror

    End Function

    Public Function PlusMin_Barang_Fsk_Adj(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal qtykecil As Integer, ByVal qtyselisih As Integer, _
                                   ByVal kdbar As String, ByVal kdgud As String, ByVal additem As Boolean) As String

        Dim cmdcek_brang As OleDbCommand
        Dim drd_barang As OleDbDataReader

        Dim sqlcekbarang As String = String.Format("select a.kd_posisi,a.jmlstok_f as jmlstok,b.qty1,b.qty2,b.qty3,b.jmlstok_f as jmlstok2 from ms_barang2 a inner join ms_barang b on a.kd_barang=b.kd_barang where b.jenis='FISIK' and a.kd_barang='{0}' and a.kd_gudang='{1}'", kdbar, kdgud)
        cmdcek_brang = New OleDbCommand(sqlcekbarang, cn, sqltrans)
        drd_barang = cmdcek_brang.ExecuteReader

        Dim konferror As String = ""

        If drd_barang.HasRows Then
            If drd_barang.Read Then
                If IsNumeric(drd_barang("kd_posisi").ToString) Then


                    Dim jmlstok As Integer = Integer.Parse(drd_barang("jmlstok").ToString)
                    Dim jmlstok2 As Integer = Integer.Parse(drd_barang("jmlstok2").ToString)

                    Dim qty1 As Integer = Integer.Parse(drd_barang("qty1").ToString)
                    Dim qty2 As Integer = Integer.Parse(drd_barang("qty2").ToString)
                    Dim qty3 As Integer = Integer.Parse(drd_barang("qty3").ToString)

                    Dim jml1, jml2, jml3 As Integer
                    Dim jml11, jml12, jml13 As Integer

                    Dim totqty As Integer = qty1 * qty2 * qty3

                    If additem = True Then

                        jmlstok = jmlstok + qtyselisih

                        If qtykecil < 0 Then
                            jmlstok2 = jmlstok2 - Math.Abs(qtyselisih)
                        Else
                            jmlstok2 = jmlstok2 + qtyselisih
                        End If

                    Else

                        'If jmlstok < qtykecil Then
                        '    konferror = kdbar & " melebihi stok yang ada..."
                        'End If

                        jmlstok2 = jmlstok2 - jmlstok

                        jmlstok = qtykecil

                        jmlstok2 = jmlstok2 + qtykecil

                    End If

                    ' by gudang

                    If jmlstok >= totqty Then

                        Dim sisa As Integer = jmlstok Mod totqty

                        jml1 = (jmlstok - sisa) / totqty

                        If sisa > qty2 Then
                            jml2 = sisa
                            sisa = sisa Mod qty2

                            jml2 = (jml2 - sisa) / qty2
                            jml3 = sisa

                        Else
                            jml2 = sisa
                            jml3 = 0
                        End If
                    End If

                    ' end by gudang

                    ' by item

                    If jmlstok2 >= totqty Then

                        Dim sisa As Integer = jmlstok2 Mod totqty

                        jml11 = (jmlstok2 - sisa) / totqty

                        If sisa > qty2 Then
                            jml12 = sisa
                            sisa = sisa Mod qty2

                            jml12 = (jml12 - sisa) / qty2
                            jml13 = sisa

                        Else
                            jml12 = sisa
                            jml13 = 0
                        End If
                    End If

                    ' end by item                   


                    If konferror.Equals("") Then

                        Dim sqlup1 As String = String.Format("update ms_barang2 set jmlstok_f1={0},jmlstok_f2={1},jmlstok_f3={2},jmlstok_f={3} where kd_gudang='{4}' and kd_barang='{5}'", jml1, jml2, jml3, jmlstok, kdgud, kdbar)
                        Dim sqlup As String = String.Format("update ms_barang set jmlstok_f1={0},jmlstok_f2={1},jmlstok_f3={2},jmlstok_f={3} where kd_barang='{4}'", jml11, jml12, jml13, jmlstok2, kdbar)

                        Using cmd1 As OleDbCommand = New OleDbCommand(sqlup1, cn, sqltrans)
                            cmd1.ExecuteNonQuery()
                        End Using

                        Using cmd2 As OleDbCommand = New OleDbCommand(sqlup, cn, sqltrans)
                            cmd2.ExecuteNonQuery()
                        End Using


                        konferror = "ok"

                    End If

                Else

                    konferror = kdbar & " tidak ditemukan..."

                End If
            Else
                konferror = kdbar & " tidak ditemukan..."
            End If
        Else
            konferror = kdbar & " tidak ditemukan..."
        End If

        drd_barang.Close()

        Return konferror

    End Function

    Public Function simpankosong(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal kdbar As String, ByVal kd_toko As String, _
                             ByVal qty1 As Integer, ByVal qty2 As Integer, ByVal qty3 As Integer, ByVal qtykecil As Integer, ByVal addstat As Boolean) As String

        Dim hasil As String = ""

        ' cek apakah barang kosong
        Dim sqlcekapa As String = String.Format("select kd_barang_kmb from ms_barang where kd_barang_kmb='{0}'", kdbar)
        Dim cmdcekapa As OleDbCommand = New OleDbCommand(sqlcekapa, cn, sqltrans)
        Dim drapa As OleDbDataReader = cmdcekapa.ExecuteReader

        If drapa.Read Then
            If Not drapa(0).ToString.Equals("") Then

                Dim sqlkosong As String = String.Format("select noid,jml from ms_toko4 where kd_toko='{0}' and kd_barang='{1}'", kd_toko, kdbar)
                Dim cmdkosong As OleDbCommand = New OleDbCommand(sqlkosong, cn, sqltrans)
                Dim drkosong As OleDbDataReader = cmdkosong.ExecuteReader

                Dim hasilcek As Integer = 0

                If drkosong.Read Then

                    If IsNumeric(drkosong(0).ToString) Then

                        hasilcek = 1

                        Dim totqty As Integer = qty1 * qty2 * qty3
                        Dim hasilqty As Integer = Integer.Parse(drkosong(1).ToString)
                        Dim jml1 As Integer = 0
                        Dim jml2 As Integer = 0
                        Dim jml3 As Integer = 0

                        If addstat = True Then
                            hasilqty = hasilqty + qtykecil
                        Else
                            hasilqty = hasilqty - qtykecil

                            'If hasilqty < 0 Then
                            '    hasil = String.Format("Stok barang {0} pada toko {1} tidak mencukupi", kdbar, kd_toko)
                            '    GoTo lanjut
                            'End If

                        End If

                        If hasilqty >= totqty Then

                            Dim sisa As Integer = hasilqty Mod totqty

                            jml1 = (hasilqty - sisa) / totqty

                            If sisa > qty2 Then
                                jml2 = sisa
                                sisa = sisa Mod qty2

                                jml2 = (jml2 - sisa) / qty2
                                jml3 = sisa

                            Else
                                jml2 = sisa
                                jml3 = 0
                            End If

                        End If

                        Dim sqlup_ks As String = String.Format("update ms_toko4 set jml={0},jml1={1},jml2={2},jml3={3} where noid='{4}'", hasilqty, jml1, jml2, jml3, drkosong(0).ToString)
                        Using cmdup_ks As OleDbCommand = New OleDbCommand(sqlup_ks, cn, sqltrans)
                            cmdup_ks.ExecuteNonQuery()
                        End Using

                    End If

                End If
                drkosong.Close()

                If hasilcek = 0 And addstat Then

                    Dim totqty As Integer = qty1 * qty2 * qty3
                    Dim hasilqty As Integer = 0
                    Dim jml1 As Integer = 0
                    Dim jml2 As Integer = 0
                    Dim jml3 As Integer = 0

                    hasilqty = hasilqty + qtykecil

                    If hasilqty >= totqty Then

                        Dim sisa As Integer = hasilqty Mod totqty

                        jml1 = (hasilqty - sisa) / totqty

                        If sisa > qty2 Then
                            jml2 = sisa
                            sisa = sisa Mod qty2

                            jml2 = (jml2 - sisa) / qty2
                            jml3 = sisa

                        Else
                            jml2 = sisa
                            jml3 = 0
                        End If

                    End If

                    Dim sqlins_ks As String = String.Format("insert into ms_toko4 (kd_toko,kd_barang,jml,jml1,jml2,jml3) values('{0}','{1}',{2},{3},{4},{5})", kd_toko, kdbar, hasilqty, jml1, jml2, jml3)
                    Using cmdins_ks As OleDbCommand = New OleDbCommand(sqlins_ks, cn, sqltrans)
                        cmdins_ks.ExecuteNonQuery()
                    End Using

                    'Else
                    '    hasil = String.Format("Stok barang {0} pada toko {1} tidak mencukupi", kdbar, kd_toko)
                    '    GoTo lanjut
                End If

            End If
        End If
        drapa.Close()

        If hasil.Equals("") Then
            hasil = "ok"
        End If

lanjut:
        Return hasil

    End Function

    Public Function simpankosong_f(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal kdbar As String, ByVal kd_toko As String, _
                         ByVal qty1 As Integer, ByVal qty2 As Integer, ByVal qty3 As Integer, ByVal qtykecil As Integer, ByVal addstat As Boolean) As String

        Dim hasil As String = ""

        ' cek apakah barang kosong
        Dim sqlcekapa As String = String.Format("select kd_barang_kmb from ms_barang where kd_barang_kmb='{0}'", kdbar)
        Dim cmdcekapa As OleDbCommand = New OleDbCommand(sqlcekapa, cn, sqltrans)
        Dim drapa As OleDbDataReader = cmdcekapa.ExecuteReader

        If drapa.Read Then
            If Not drapa(0).ToString.Equals("") Then

                Dim sqlkosong As String = String.Format("select noid,jml from ms_toko4 where kd_toko='{0}' and kd_barang='{1}'", kd_toko, kdbar)
                Dim cmdkosong As OleDbCommand = New OleDbCommand(sqlkosong, cn, sqltrans)
                Dim drkosong As OleDbDataReader = cmdkosong.ExecuteReader

                Dim hasilcek As Integer = 0

                If drkosong.Read Then

                    If IsNumeric(drkosong(0).ToString) Then

                        hasilcek = 1

                        Dim totqty As Integer = qty1 * qty2 * qty3
                        Dim hasilqty As Integer = Integer.Parse(drkosong(1).ToString)
                        Dim jml1 As Integer = 0
                        Dim jml2 As Integer = 0
                        Dim jml3 As Integer = 0

                        If addstat = True Then
                            hasilqty = hasilqty + qtykecil
                        Else
                            hasilqty = hasilqty - qtykecil

                            'If hasilqty < 0 Then
                            '    hasil = String.Format("Stok barang {0} pada toko {1} tidak mencukupi", kdbar, kd_toko)
                            '    GoTo lanjut
                            'End If

                        End If

                        If hasilqty >= totqty Then

                            Dim sisa As Integer = hasilqty Mod totqty

                            jml1 = (hasilqty - sisa) / totqty

                            If sisa > qty2 Then
                                jml2 = sisa
                                sisa = sisa Mod qty2

                                jml2 = (jml2 - sisa) / qty2
                                jml3 = sisa

                            Else
                                jml2 = sisa
                                jml3 = 0
                            End If

                        End If

                        Dim sqlup_ks As String = String.Format("update ms_toko4 set jml={0},jml1={1},jml2={2},jml3={3} where noid='{4}'", hasilqty, jml1, jml2, jml3, drkosong(0).ToString)
                        Using cmdup_ks As OleDbCommand = New OleDbCommand(sqlup_ks, cn, sqltrans)
                            cmdup_ks.ExecuteNonQuery()
                        End Using

                    End If

                End If
                drkosong.Close()

                If hasilcek = 0 And addstat Then

                    Dim totqty As Integer = qty1 * qty2 * qty3
                    Dim hasilqty As Integer = 0
                    Dim jml1 As Integer = 0
                    Dim jml2 As Integer = 0
                    Dim jml3 As Integer = 0

                    hasilqty = hasilqty + qtykecil

                    If hasilqty >= totqty Then

                        Dim sisa As Integer = hasilqty Mod totqty

                        jml1 = (hasilqty - sisa) / totqty

                        If sisa > qty2 Then
                            jml2 = sisa
                            sisa = sisa Mod qty2

                            jml2 = (jml2 - sisa) / qty2
                            jml3 = sisa

                        Else
                            jml2 = sisa
                            jml3 = 0
                        End If

                    End If

                    Dim sqlins_ks As String = String.Format("insert into ms_toko4 (kd_toko,kd_barang,jml,jml1,jml2,jml3) values('{0}','{1}',{2},{3},{4},{5})", kd_toko, kdbar, hasilqty, jml1, jml2, jml3)
                    Using cmdins_ks As OleDbCommand = New OleDbCommand(sqlins_ks, cn, sqltrans)
                        cmdins_ks.ExecuteNonQuery()
                    End Using

                    'Else
                    '    hasil = String.Format("Stok barang {0} pada toko {1} tidak mencukupi", kdbar, kd_toko)
                    '    GoTo lanjut
                End If

            End If
        End If
        drapa.Close()

        If hasil.Equals("") Then
            hasil = "ok"
        End If

lanjut:

        Return hasil

    End Function

    Public Function apakah_brg_kembali(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction, ByVal kdbar As String) As Boolean

        Dim sqlcekapa As String = String.Format("select kd_barang_kmb from ms_barang where kd_barang_kmb='{0}'", kdbar)
        Dim cmd As OleDbCommand = New OleDbCommand(sqlcekapa, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim hasil As Boolean = False

        If drd.Read Then
            If Not drd(0).ToString.Equals("") Then
                hasil = True
            End If
        End If
        drd.Close()

        Return hasil

    End Function

End Module
