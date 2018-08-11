Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fbayar2_retur2

    Public kdtoko As String
    Public kd_group As String

    Private dvmanager As Data.DataViewManager
    Private dv As Data.DataView

    Private Sub open_retur()

        Dim sql As String = "select trretur.nobukti,trretur.tanggal,trretur.alasan_retur,trretur.netto,trretur.jmlpotong from trretur inner join ms_toko on trretur.kd_toko=ms_toko.kd_toko " & _
            "where trretur.netto>0 and trretur.netto > trretur.jmlpotong and sbatal=0"

        If kd_group.Equals("-") Then
            sql = String.Format("{0} and ms_toko.kd_toko='{1}'", sql, kdtoko)
        Else
            sql = String.Format("{0} and (ms_toko.kd_toko='{1}' or ms_toko.kd_group='{2}')", sql, kdtoko, kd_group)
        End If

        If tfind.Text.Trim.Length > 0 Then

            sql = String.Format("{0} and nobukti like '%{1}%'", sql, tfind.Text.Trim)

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

    Public ReadOnly Property get_noretur As String
        Get

            If IsNothing(dv) Then
                Return ""
                Exit Property
            End If

            If dv.Count <= 0 Then
                Return ""
                Exit Property
            End If

            Return dv(Me.BindingContext(dv).Position)("nobukti").ToString

        End Get
    End Property

    Private Sub fbayar2_giro2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tfind.Focus()
    End Sub

    Private Sub fbayar2_giro2_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        open_retur()
    End Sub

    Private Sub tfind_EditValueChanged(sender As Object, e As System.EventArgs) Handles tfind.EditValueChanged
        open_retur()
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