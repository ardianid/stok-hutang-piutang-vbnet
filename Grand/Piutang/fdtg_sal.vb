Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Public Class fdtg_sal

    Public dv As DataView

    Private Sub isi_sales()

        Dim sql As String = ""

        sql = "select kd_karyawan,nama_karyawan from ms_pegawai where bagian='SALES' and aktif=1"

        Dim cn As OleDbConnection = Nothing
        Dim ds As DataSet
        Dim dvg As DataView

        Try

            cn = Clsmy.open_conn
            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dvm As DataViewManager = New DataViewManager(ds)
            dvg = dvm.CreateDataView(ds.Tables(0))

            tsales.Properties.DataSource = dvg

            'tgudang.ItemIndex = 0

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

    Private Sub isi()

        Dim dta As DataTable = dv.ToTable
        Dim rows() As DataRow = dta.Select(String.Format("kd_karyawan='{0}'", tsales.EditValue))

        If rows.Length > 0 Then
            MsgBox("Sales sudah ada dalam daftar", vbOKOnly + vbInformation, "Informasi")
            tsales.Focus()
            Return
        End If

        Dim orow As DataRowView = dv.AddNew
        orow("kd_karyawan") = tsales.EditValue
        orow("nama_karyawan") = tsales.Text.Trim

        dv.EndInit()

    End Sub

    Private Sub btclose_Click(sender As System.Object, e As System.EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click
        If tsales.EditValue.Equals("") Then
            Return

        End If

        isi()

    End Sub

    Private Sub fdtg_sal_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tsales.Focus()
    End Sub

    Private Sub fdtg_sal_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        isi_sales()
    End Sub


End Class