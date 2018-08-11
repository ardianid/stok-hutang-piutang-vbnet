Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fbayar_dtg

    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Public dvdtg As DataView

    Private Sub cari()

        Dim sql As String = "select a.nobukti,a.tanggal,a.tgl_tagih,b.kd_karyawan,b.nama_karyawan " & _
        "from trdaftar_tgh a inner join ms_pegawai b on a.kd_kolek=b.kd_karyawan where a.sbatal=0 and spulang=0"

        If Not tfind.Text.Trim.Equals("") Then
            sql = String.Format("{0} and a.nobukti like '%{1}%'", sql, tfind.Text.Trim)
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

    Private Sub isi()

        Dim nodtg As String = dv1(Me.BindingContext(dv1).Position)("nobukti").ToString
        Dim tanggal As String = dv1(Me.BindingContext(dv1).Position)("tanggal").ToString
        Dim tgl_tagih As String = dv1(Me.BindingContext(dv1).Position)("tgl_tagih").ToString
        Dim nama_karyawan As String = dv1(Me.BindingContext(dv1).Position)("nama_karyawan").ToString
        Dim kd_karyawan As String = dv1(Me.BindingContext(dv1).Position)("kd_karyawan").ToString

        Dim dta As DataTable = dvdtg.ToTable
        Dim rows() As DataRow = dta.Select(String.Format("nobukti='{0}'", nodtg))

        If rows.Length > 0 Then
            MsgBox("No DTG sudah ada dalam daftar", vbOKOnly + vbInformation, "Informasi")
            tfind.Focus()
            Return
        End If

        Dim orow As DataRowView = dvdtg.AddNew
        orow("nobukti") = nodtg
        orow("tanggal") = tanggal
        orow("tgl_tagih") = tgl_tagih
        orow("kd_karyawan") = kd_karyawan
        orow("nama_karyawan") = nama_karyawan

        dvdtg.EndInit()

    End Sub

    Private Sub tfind_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tfind.KeyDown
        If e.KeyCode = 13 Then
            isi()
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub grid1_DoubleClick(sender As System.Object, e As System.EventArgs) Handles grid1.DoubleClick
        isi()
        Me.Close()
    End Sub

    Private Sub grid1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles grid1.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            isi()
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

        tfind.Text = ""

        cari()

    End Sub

    Private Sub tfind_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tfind.KeyUp
        cari()
    End Sub

End Class