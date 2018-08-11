Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fsoutlet
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Public kdsales As String = ""
    Public valltoko As Boolean = True

    Private Sub cari()

        Dim sql As String = ""

        If kdsales = "" Or kdsales.Equals("cek_outlet") Then
            sql = "select top 1000 kd_toko,nama_toko,alamat_toko from ms_toko where aktif=1"
        ElseIf Not (kdsales.Equals("cek_outlet") And kdsales.Equals("")) Then
            sql = String.Format("select kd_toko,nama_toko,alamat_toko from ms_toko where aktif=1 and kd_toko in (select ms_toko2.kd_toko from ms_toko2 where ms_toko2.kd_karyawan='{0}')", kdsales)
        End If

        If valltoko = False Then
            If ins_alltokouser = 0 Then
                sql = String.Format(" {0} and kd_toko in (select kd_toko from ms_usersys4 where nama_user='{1}')", sql, userprog)
            End If
        End If

        If Not tfind.Text.Trim.Equals("") Then

            Select Case cb_criteria.SelectedIndex
                Case 0

                    If kdsales.Equals("") Then
                        sql = String.Format("{0} and  kd_toko like '%{1}%'", sql, tfind.Text.Trim)
                    Else
                        sql = String.Format("{0} and  kd_toko like '%{1}%'", sql, tfind.Text.Trim)
                    End If


                Case 1

                    If kdsales.Equals("") Then
                        sql = String.Format("{0} and  nama_toko like '%{1}%'", sql, tfind.Text.Trim)
                    Else
                        sql = String.Format("{0} and  nama_toko like '%{1}%'", sql, tfind.Text.Trim)
                    End If

                Case 2

                    If kdsales.Equals("") Then
                        sql = String.Format("{0} and  alamat_toko like '%{1}%'", sql, tfind.Text.Trim)
                    Else
                        sql = String.Format("{0} and  alamat_toko like '%{1}%'", sql, tfind.Text.Trim)
                    End If

            End Select

        End If

        If Not tfind2.Text.Trim.Equals("") Then

            Select Case cb_criteria2.SelectedIndex
                Case 0

                    If kdsales.Equals("") Then
                        sql = String.Format("{0} and  kd_toko like '%{1}%'", sql, tfind2.Text.Trim)
                    Else
                        sql = String.Format("{0} and  kd_toko like '%{1}%'", sql, tfind2.Text.Trim)
                    End If


                Case 1

                    If kdsales.Equals("") Then
                        sql = String.Format("{0} and  nama_toko like '%{1}%'", sql, tfind2.Text.Trim)
                    Else
                        sql = String.Format("{0} and  nama_toko like '%{1}%'", sql, tfind2.Text.Trim)
                    End If

                Case 2

                    If kdsales.Equals("") Then
                        sql = String.Format("{0} and  alamat_toko like '%{1}%'", sql, tfind2.Text.Trim)
                    Else
                        sql = String.Format("{0} and  alamat_toko like '%{1}%'", sql, tfind2.Text.Trim)
                    End If

            End Select

        End If

        If kdsales.Equals("cek_outlet") Then
            sql = String.Format(" {0} order by kd_toko desc", sql)
        Else
            sql = String.Format(" {0} order by nama_toko asc", sql)
        End If

            Dim cn As OleDbConnection = Nothing
            Dim ds As DataSet

            grid1.DataSource = Nothing

            Try

                '  open_wait()

                dv1 = Nothing

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                ds = New DataSet()
                ds = Clsmy.GetDataSet(sql, cn)

                dvmanager = New DataViewManager(ds)
                dv1 = dvmanager.CreateDataView(ds.Tables(0))

                grid1.DataSource = dv1

                '     close_wait()

            Catch ex As OleDb.OleDbException
                '  close_wait()
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Finally


                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If

            End Try


    End Sub

    Public ReadOnly Property get_KODE As String
        Get

            If IsNothing(dv1) Then
                Return ""
                Exit Property
            End If

            If dv1.Count <= 0 Then
                Return ""
                Exit Property
            End If
            Return dv1(Me.BindingContext(dv1).Position)("kd_toko").ToString
        End Get
    End Property

    Public ReadOnly Property get_NAMA As String
        Get

            If IsNothing(dv1) Then
                Return ""
                Exit Property
            End If

            If dv1.Count <= 0 Then
                Return ""
                Exit Property
            End If
            Return dv1(Me.BindingContext(dv1).Position)("nama_toko").ToString
        End Get
    End Property

    Public ReadOnly Property get_ALAMAT As String
        Get

            If IsNothing(dv1) Then
                Return ""
                Exit Property
            End If

            If dv1.Count <= 0 Then
                Return ""
                Exit Property
            End If
            Return dv1(Me.BindingContext(dv1).Position)("alamat_toko").ToString
        End Get
    End Property

    Private Sub tfind_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tfind.KeyDown, tfind2.KeyDown
        If e.KeyCode = 13 Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub grid1_DoubleClick(sender As System.Object, e As System.EventArgs) Handles grid1.DoubleClick
        Me.Close()
    End Sub

    Private Sub grid1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles grid1.KeyDown
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Enter Then
            Me.Close()
        End If
    End Sub

    Private Sub fskec_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tfind.Focus()
        tfind.Select(tfind.Text.Length, 0)
    End Sub

    Private Sub fskec_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then

            Me.Close()
        End If
    End Sub

    Private Sub fskec_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If kdsales.Equals("cek_outlet") Then
            cb_criteria.SelectedIndex = 0
        Else
            cb_criteria.SelectedIndex = 1
        End If

        cb_criteria2.SelectedIndex = 2

        tfind.Text = ""

        cari()

    End Sub

    Private Sub tfind_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tfind.KeyUp, tfind2.KeyUp
        cari()
    End Sub

End Class