Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fkonfirm_hapus

    Private alasanbatal As String

    Public ReadOnly Property get_alasan As String
        Get
            Return alasanbatal
        End Get
    End Property

    Private Sub load_alasan()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select alasan from ms_alasan where tipe='BATAL TRANSAKSI' order by alasan"
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.HasRows Then
                While drd.Read
                    cbalasan.Properties.Items.Add(drd(0).ToString)
                End While
            End If
            drd.Close()

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

    Private Sub fkonfirm_hapus_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        cbalasan.Focus()
    End Sub

    Private Sub fkonfirm_hapus_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        load_alasan()
    End Sub

    Private Sub btok_Click(sender As System.Object, e As System.EventArgs) Handles btok.Click
        If cbalasan.SelectedIndex < 0 Then
            alasanbatal = ""
            MsgBox("Alasan harus sesuai, konfirmasi administrator...", vbOKOnly + vbInformation, "Informasi")
            Me.Close()
        Else
            alasanbatal = cbalasan.EditValue
            Me.Close()
        End If
    End Sub

    Private Sub btcancel_Click(sender As System.Object, e As System.EventArgs) Handles btcancel.Click
        alasanbatal = ""
        Me.Close()
    End Sub

    Private Sub cbalasan_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles cbalasan.KeyDown
        If e.KeyCode = 13 Then
            btok_Click(sender, Nothing)
        ElseIf e.KeyCode = Keys.Escape Then
            btcancel_Click(sender, Nothing)
        End If
    End Sub

End Class