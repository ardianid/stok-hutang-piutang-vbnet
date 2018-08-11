Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fbayar_psw3

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Public kdtoko As String

    Private Sub isi()

        tnosewa.EditValue = dv(position)("nobukti_sw").ToString
        ttgl_sewa.EditValue = convert_date_to_ind(dv(position)("tanggal").ToString)
        tjml.EditValue = dv(position)("netto").ToString
        tjml_byr.EditValue = dv(position)("jumlah").ToString

    End Sub

    Private Sub kosongkan()

        tnosewa.EditValue = ""
        ttgl_sewa.EditValue = ""

        tjml.EditValue = 0
        tjml_byr.EditValue = 0

    End Sub

    Private Sub tnosewa_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tnosewa.KeyDown
        If e.KeyCode = Keys.F4 Then
            bts_supir_Click(sender, Nothing)
        End If
    End Sub

    Private Sub tnosewa_LostFocus(sender As Object, e As System.EventArgs) Handles tnosewa.LostFocus
        If tnosewa.Text.Trim.Length = 0 Then
            ttgl_sewa.EditValue = ""
            tjml.EditValue = 0
        End If
    End Sub

    Private Sub tnosewa_Validated(sender As System.Object, e As System.EventArgs) Handles tnosewa.Validated

        If tnosewa.Text.Trim.Length > 0 Then

            Dim cn As OleDbConnection = Nothing
            Dim sql As String = String.Format("select trsewa3.nobukti,trsewa3.tanggal,trsewa3.netto " & _
                "from trsewa3 inner join trsewa on trsewa3.nobukti_sw=trsewa.nobukti " & _
                "where trsewa3.netto > trsewa3.jmlbayar and trsewa3.sbatal=0 and  trsewa3.nobukti='{0}' and trsewa.kd_toko='{1}'", tnosewa.Text.Trim, kdtoko)

            Try

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                Dim comd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim dread As OleDbDataReader = comd.ExecuteReader

                If dread.HasRows Then
                    If dread.Read Then

                        tnosewa.EditValue = dread("nobukti").ToString
                        ttgl_sewa.EditValue = convert_date_to_ind(dread("tanggal").ToString)
                        tjml.EditValue = dread("netto").ToString

                    Else
                        tnosewa.EditValue = ""
                        ttgl_sewa.EditValue = ""
                        tjml.EditValue = 0
                    End If
                Else
                    tnosewa.EditValue = ""
                    ttgl_sewa.EditValue = ""
                    tjml.EditValue = 0
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

    Private Sub bts_supir_Click(sender As System.Object, e As System.EventArgs) Handles bts_supir.Click

        Dim fs As New fspsewa With {.StartPosition = FormStartPosition.CenterParent, .WindowState = FormWindowState.Normal, .kdtoko = kdtoko}
        fs.ShowDialog(Me)

        tnosewa.Text = fs.get_NoBukti
        tnosewa_Validated(sender, Nothing)

    End Sub

    Private Sub ffaktur_to3_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If addstat = True Then
            tnosewa.Focus()
        Else
            tjml_byr.Focus()
        End If
    End Sub

    Private Sub ffaktur_to3_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If addstat = True Then
            tnosewa.Enabled = True

            kosongkan()

        Else

            tnosewa.Enabled = False

            isi()

        End If
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click

        If tnosewa.EditValue = "" Then
            MsgBox("Barang harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tnosewa.Focus()
            Return
        End If

        If tjml_byr.EditValue = 0 Then
            MsgBox("Jumlah bayar harus diisi...", vbOKOnly + vbInformation, "Informasi")
            tjml_byr.Focus()
            Return
        End If

        If addstat = True Then
            insertview()
        Else
            updateview()
        End If

    End Sub

    Private Sub insertview()

        Dim orow As DataRowView = dv.AddNew
        orow("noid") = 0
        orow("nobukti_sw") = tnosewa.EditValue
        orow("tanggal") = ttgl_sewa.EditValue

        orow("netto") = tjml.EditValue
        orow("jumlah") = tjml_byr.EditValue


        dv.EndInit()

        kosongkan()
        tnosewa.Focus()

    End Sub

    Private Sub updateview()

        dv(position)("jumlah") = tjml_byr.EditValue

        Me.Close()

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub


End Class