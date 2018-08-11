Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fsrekap
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private Sub cari()

        'Dim sql As String = "SELECT     trrekap_to.nobukti, trrekap_to.tgl, trrekap_to.tglkirim, ms_pegawai.nama_karyawan AS nama_supir, ms_jalur_kirim.nama_jalur, trrekap_to.nopol " & _
        '        "FROM         trrekap_to INNER JOIN " & _
        '        "ms_pegawai ON trrekap_to.kd_supir = ms_pegawai.kd_karyawan INNER JOIN " & _
        '        "ms_jalur_kirim ON trrekap_to.kd_jalur = ms_jalur_kirim.kd_jalur where trrekap_to.sbatal=0 and trrekap_to.smuat=1 and trrekap_to.spulang=0"

        Dim sql As String = "SELECT     trrekap_to.nobukti, trrekap_to.tgl, trrekap_to.tglkirim, ms_pegawai.nama_karyawan AS nama_supir, ms_jalur_kirim.nama_jalur, trrekap_to.nopol,trrekap_to.tot_nota,trrekap_to.sfaktur_kosong " & _
                "FROM         trrekap_to LEFT JOIN " & _
                "ms_pegawai ON trrekap_to.kd_supir = ms_pegawai.kd_karyawan INNER JOIN " & _
                "ms_jalur_kirim ON trrekap_to.kd_jalur = ms_jalur_kirim.kd_jalur where trrekap_to.sbatal=0 and trrekap_to.spulang=0"

        If Not tfind.Text.Trim.Equals("") Then

            Select Case cb_criteria.SelectedIndex
                Case 0 ' nobukti
                    sql = String.Format("{0} and  nobukti like '%{1}%'", sql, tfind.Text.Trim)
                Case 1  ' nopol
                    sql = String.Format("{0} and  nopol like '%{1}%'", sql, tfind.Text.Trim)
                Case 2 ' supir
                    sql = String.Format("{0} and  nama_karyawan like '%{1}%'", sql, tfind.Text.Trim)
                Case 3 ' jalur
                    sql = String.Format("{0} and  nama_jalur like '%{1}%'", sql, tfind.Text.Trim)
                Case 4
                    sql = String.Format("{0} and  nobukti in (select nobukti from trrekap_to2 where nobukti_fak='{1}')", sql, tfind.Text.Trim)
            End Select

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

    Public ReadOnly Property get_Nobukti As String
        Get

            If IsNothing(dv1) Then
                Return ""
                Exit Property
            End If

            If dv1.Count <= 0 Then
                Return ""
                Exit Property
            End If
            Return dv1(Me.BindingContext(dv1).Position)("nobukti").ToString
        End Get
    End Property

    Public ReadOnly Property get_Nopol As String
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
            Return dv1(Me.BindingContext(dv1).Position)("nama_supir").ToString
        End Get
    End Property

    Public ReadOnly Property get_tglkirim As String
        Get

            If IsNothing(dv1) Then
                Return ""
                Exit Property
            End If

            If dv1.Count <= 0 Then
                Return ""
                Exit Property
            End If
            Return dv1(Me.BindingContext(dv1).Position)("tglkirim").ToString
        End Get
    End Property

    Public ReadOnly Property get_jalur As String
        Get

            If IsNothing(dv1) Then
                Return ""
                Exit Property
            End If

            If dv1.Count <= 0 Then
                Return ""
                Exit Property
            End If
            Return dv1(Me.BindingContext(dv1).Position)("nama_jalur").ToString
        End Get
    End Property

    Public ReadOnly Property get_stat_fak_kosong As String
        Get

            If IsNothing(dv1) Then
                Return 0
                Exit Property
            End If

            If dv1.Count <= 0 Then
                Return 0
                Exit Property
            End If
            Return dv1(Me.BindingContext(dv1).Position)("sfaktur_kosong").ToString
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

        cb_criteria.SelectedIndex = 0

        tfind.Text = ""

        cari()

    End Sub

    Private Sub tfind_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tfind.KeyUp
        cari()
    End Sub

End Class