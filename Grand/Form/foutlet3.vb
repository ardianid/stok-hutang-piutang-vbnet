Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class foutlet3

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private Sub updateview()

        dv(position)("kd_karyawan") = tkd_sales.EditValue
        dv(position)("nama_karyawan") = tnama_sales.Text.Trim
        dv(position)("limit_val") = tlimit.EditValue

        Me.Close()

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew

        orow("kd_karyawan") = tkd_sales.EditValue
        orow("nama_karyawan") = tnama_sales.EditValue
        orow("limit_val") = tlimit.EditValue
        orow("jmlpiutang") = 0

        dv.EndInit()

        tlimit.EditValue = 0
        tkd_sales.Focus()

    End Sub

    Private Sub bts_sal_Click(sender As System.Object, e As System.EventArgs) Handles bts_sal.Click
        Dim fs As New fssales With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .kd_toko = ""}
        fs.ShowDialog(Me)

        tkd_sales.EditValue = fs.get_KODE

        tkd_sales_EditValueChanged(sender, Nothing)

    End Sub

    Private Sub tkd_sales_EditValueChanged(sender As Object, e As System.EventArgs) Handles tkd_sales.Validated
        If tkd_sales.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("SELECT kd_karyawan,nama_karyawan FROM ms_pegawai where bagian='SALES' and aktif=1 and kd_karyawan='{0}'", tkd_sales.Text.Trim)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tkd_sales.EditValue = dread("kd_karyawan").ToString
                        tnama_sales.EditValue = dread("nama_karyawan").ToString

                    Else
                        tkd_sales.EditValue = ""
                        tnama_sales.EditValue = ""

                    End If
                Else
                    tkd_sales.EditValue = ""
                    tnama_sales.EditValue = ""
                End If

                dread.Close()

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Finally


                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If
            End Try

        End If
    End Sub

    Private Sub tkd_sales_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tkd_sales.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_sal_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tkd_sales_LostFocus(sender As Object, e As System.EventArgs) Handles tkd_sales.LostFocus
        If tkd_sales.Text.Trim.Length = 0 Then
            tkd_sales.EditValue = ""
            tnama_sales.EditValue = ""
        End If
    End Sub

    Private Sub foutlet3_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If addstat = True Then
            tkd_sales.Focus()
        Else
            tlimit.Focus()
        End If
    End Sub

    Private Sub foutlet3_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        If addstat = True Then

            tkd_sales.Enabled = True
            bts_sal.Enabled = True

            tlimit.EditValue = 0

        Else

            tkd_sales.Enabled = False
            bts_sal.Enabled = False

            tkd_sales.EditValue = dv(position)("kd_karyawan").ToString

            tkd_sales_EditValueChanged(sender, Nothing)

            tlimit.EditValue = dv(position)("limit_val").ToString

        End If

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tkd_sales.Text.Trim.Length = 0 Then
            MsgBox("Sales harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tkd_sales.Focus()
            Return
        End If

        If tlimit.EditValue = 0 Then
            MsgBox("Limit harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tlimit.Focus()
            Return
        End If

        If addstat = True Then
            insertview()
        Else
            updateview()
        End If

    End Sub

End Class