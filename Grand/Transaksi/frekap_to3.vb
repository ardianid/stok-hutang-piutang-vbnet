Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class frekap_to3

    Public dv As DataView

    Private Sub insert()

        Dim rows() As DataRow = dv.Table.Select(String.Format("nobukti_fak='{0}'", tbukti.Text.Trim))

        If rows.Length > 0 Then
            If rows(0)("nobukti_fak").ToString.Equals(tbukti.Text.Trim) Then
                MsgBox("Faktur sudah ada didalam daftar..", vbOKOnly + vbExclamation, "Informasi")
                tbukti.Focus()
                Return
            End If
        End If


        Dim cn As OleDbConnection = Nothing
        Dim sql As String = String.Format("SELECT     trfaktur_to.nobukti, trfaktur_to.tanggal, trfaktur_to.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, trfaktur_to.kd_karyawan, ms_pegawai.nama_karyawan, trfaktur_to.netto " & _
                "FROM         trfaktur_to INNER JOIN " & _
                      "ms_toko ON trfaktur_to.kd_toko = ms_toko.kd_toko INNER JOIN " & _
                      "ms_pegawai ON trfaktur_to.kd_karyawan = ms_pegawai.kd_karyawan where trfaktur_to.jnis_fak='T' and trfaktur_to.sbatal=0 and trfaktur_to.nobukti='{0}' and (trfaktur_to.skirim=0 or trfaktur_to.statkirim='BELUM TERKIRIM') and nobukti not in (select trrekap_to2.nobukti_fak from trrekap_to2 inner join trrekap_to on trrekap_to2.nobukti=trrekap_to.nobukti where sbatal=0 and spulang=0)", tbukti.Text.Trim)

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then

                Dim orow As DataRowView = dv.AddNew
                orow("noid") = 0
                orow("nobukti_fak") = drd("nobukti").ToString
                orow("tanggal") = drd("tanggal").ToString
                orow("kd_toko") = drd("kd_toko").ToString
                orow("nama_toko") = drd("nama_toko").ToString
                orow("alamat_toko") = drd("alamat_toko").ToString
                orow("kd_karyawan") = drd("kd_karyawan").ToString
                orow("nama_karyawan") = drd("nama_karyawan").ToString
                orow("netto") = drd("netto").ToString
                orow.EndEdit()

                tbukti.Text = ""
                tbukti.Focus()

            Else
                MsgBox("No bukti tidak ditemukan...", vbOKOnly + vbExclamation, "Informasi")
                tbukti.Focus()
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

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btadd_Click(sender As System.Object, e As System.EventArgs) Handles btadd.Click

        If tbukti.Text.Trim.Length > 0 Then
            insert()
        End If

    End Sub

    Private Sub tbukti_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tbukti.KeyDown
        If e.KeyCode = 13 Then
            If tbukti.Text.Trim.Length > 0 Then
                insert()
            End If
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub frekap_to3_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tbukti.Focus()
    End Sub

End Class