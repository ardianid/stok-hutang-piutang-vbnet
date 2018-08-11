Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fgiro_cair3

    Private dvmanager As Data.DataViewManager
    Private dv As Data.DataView

    Private Sub open_giro()

        Dim sql As String = "SELECT ms_giro.nogiro, ms_giro.tgl_giro,ms_giro.tgl_jt, ms_giro.namabank, ms_giro.jumlah,ms_giro.sgunakan " & _
                "FROM ms_giro WHERE ms_giro.sbatal=0 and ms_giro.scair=0 and ms_giro.stolak=0 "

        If tfind.Text.Trim.Length > 0 Then

            sql = String.Format("{0} and ms_giro.nogiro like '%{1}%'", sql, tfind.Text.Trim)

        End If

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv = dvmanager.CreateDataView(ds.Tables(0))

            grid1.DataSource = dv

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

    Public ReadOnly Property get_nogiro As String
        Get

            If IsNothing(dv) Then
                Return ""
                Exit Property
            End If

            If dv.Count <= 0 Then
                Return ""
                Exit Property
            End If

            Return dv(Me.BindingContext(dv).Position)("nogiro").ToString

        End Get
    End Property

    Private Sub fbayar2_giro2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tfind.Focus()
    End Sub

    Private Sub fbayar2_giro2_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        open_giro()
    End Sub

    Private Sub tfind_EditValueChanged(sender As Object, e As System.EventArgs) Handles tfind.EditValueChanged
        open_giro()
    End Sub

    Private Sub tfind_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tfind.KeyDown

        If e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            Me.Close()
        End If

    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As System.EventArgs) Handles GridView1.DoubleClick
        Me.Close()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown

        If e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            Me.Close()
        End If
    End Sub


End Class