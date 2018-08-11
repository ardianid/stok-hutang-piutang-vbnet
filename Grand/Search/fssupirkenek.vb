Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fssupirkenek

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView
    Public ifkanvas As Boolean = False

    Private Sub cari()

        Dim sql As String = ""

        sql = "SELECT     ms_supir.kd_karyawan AS kd_supir, ms_supir.nama_karyawan AS nama_supir, ms_kenek1.kd_karyawan AS kd_kenek1, " & _
            " ms_kenek1.nama_karyawan AS nama_kenek1, ms_kenek2.kd_karyawan AS kd_kenek2, ms_kenek2.nama_karyawan AS nama_kenek2, " & _
            "ms_kenek3.kd_karyawan AS kd_kenek3, ms_kenek3.nama_karyawan AS nama_kenek3, ms_supirkenek.nopol, ms_sales.kd_karyawan AS kd_sales, " & _
             "  ms_sales.nama_karyawan AS nama_sales " & _
            "FROM         ms_pegawai AS ms_supir INNER JOIN " & _
             " ms_supirkenek ON ms_supir.kd_karyawan = ms_supirkenek.kd_supir LEFT OUTER JOIN " & _
             "ms_pegawai AS ms_sales ON ms_supirkenek.kd_sales = ms_sales.kd_karyawan LEFT OUTER JOIN " & _
               " ms_pegawai AS ms_kenek3 ON ms_supirkenek.kd_kenek3 = ms_kenek3.kd_karyawan LEFT OUTER JOIN " & _
              " ms_pegawai AS ms_kenek2 ON ms_supirkenek.kd_kenek2 = ms_kenek2.kd_karyawan LEFT OUTER JOIN " & _
               " ms_pegawai AS ms_kenek1 ON ms_supirkenek.kd_kenek1 = ms_kenek1.kd_karyawan"

        If Not tfind.Text.Trim.Equals("") Then

            Select Case cb_criteria.SelectedIndex
                Case 0
                    sql = String.Format("{0} where  ms_supirkenek.nopol like '%{1}%'", sql, tfind.Text.Trim)
                Case 1
                    sql = String.Format("{0} where  ms_sales.nama_karyawan like '%{1}%'", sql, tfind.Text.Trim)
                Case 2
                    sql = String.Format("{0} where ms_supir.nama_karyawan like '%{1}%'", sql, tfind.Text.Trim)
            End Select

        End If

        If ifkanvas = True Then
            If Not tfind.Text.Trim.Equals("") Then
                sql = String.Format(" {0} and ms_supirkenek.nopol in (select kd_gudang from ms_gudang where tipe_gudang='MOBIL')", sql)
            Else
                sql = String.Format(" {0} where ms_supirkenek.nopol in (select kd_gudang from ms_gudang where tipe_gudang='MOBIL')", sql)
            End If
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

    Public ReadOnly Property get_nopol As String
        Get

            If IsNothing(dv1) Then
                Return ""
                Exit Property
            End If

            If dv1.Count <= 0 Then
                Return ""
                Exit Property
            End If
            Return dv1(Me.BindingContext(dv1).Position)("nopol").ToString
        End Get
    End Property

    Public ReadOnly Property get_sales As String
        Get

            If IsNothing(dv1) Then
                Return ""
                Exit Property
            End If

            If dv1.Count <= 0 Then
                Return ""
                Exit Property
            End If
            Return dv1(Me.BindingContext(dv1).Position)("kd_sales").ToString
        End Get
    End Property

    Public ReadOnly Property get_supir As String
        Get

            If IsNothing(dv1) Then
                Return ""
                Exit Property
            End If

            If dv1.Count <= 0 Then
                Return ""
                Exit Property
            End If
            Return dv1(Me.BindingContext(dv1).Position)("kd_supir").ToString
        End Get
    End Property

    Private Sub tfind_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tfind.KeyDown
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

        cb_criteria.SelectedIndex = 2

        tfind.Text = ""

        cari()

    End Sub

    Private Sub tfind_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tfind.KeyUp
        cari()
    End Sub

End Class