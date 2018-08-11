Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class fpr_htoko2

    Public tgl1 As String
    Public tgl2 As String
    Public kdtoko As String

    Dim crReportDocument As cr_htoko

    Dim dvg As DataView

    Private Sub load_print()

        Cursor = Cursors.WaitCursor

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = String.Format("SELECT     htoko.noid, htoko.nobukti, htoko.nobukti2, htoko.tanggal, htoko.kd_toko, ms_toko.nama_toko, ms_toko.alamat_toko, htoko.jmlin, htoko.jmlout, htoko.jenis " & _
                "FROM         htoko INNER JOIN " & _
                "ms_toko ON htoko.kd_toko = ms_toko.kd_toko " & _
                "WHERE htoko.tanggal>='{0}' and htoko.tanggal<='{1}' and htoko.kd_toko='{2}'", convert_date_to_eng(tgl1), convert_date_to_eng(tgl2), kdtoko)

            Dim ds As New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ds1 As New ds_htoko
            ds1.Clear()
            ds1.Tables(0).Merge(ds.Tables(0))

            crReportDocument = New cr_htoko
            crReportDocument.SetDataSource(ds1)

            Dim jmlold As String = cek_jmlold(cn).ToString

            crReportDocument.SetParameterValue("tgl1", convert_date_to_ind(tgl1))
            crReportDocument.SetParameterValue("tgl2", convert_date_to_ind(tgl2))
            crReportDocument.SetParameterValue("ajmlold", jmlold)
            crReportDocument.SetParameterValue("userprint", String.Format("User : {0} | Tgl : {1}", userprog, Date.Now))

            CrystalReportViewer1.ReportSource = crReportDocument
            CrystalReportViewer1.Refresh()

            Cursor = Cursors.Default

        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            Cursor = Cursors.Default

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Function cek_jmlold(ByVal cn As OleDbConnection) As Integer

        Dim jmlold As Double = 0

        Dim sql As String = String.Format("select sum(jmlin)- sum(jmlout) as jmlold from htoko where tanggal <'{0}' and kd_toko='{1}'", convert_date_to_eng(tgl1), kdtoko)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        If drd.Read Then
            If IsNumeric(drd(0).ToString) Then
                jmlold = Double.Parse(drd(0).ToString)
            End If
        End If

        drd.Close()

        Return jmlold

    End Function

    Private Sub fpr_htoko2_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        load_print()
    End Sub

End Class