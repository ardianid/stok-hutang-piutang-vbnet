Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpr_sewa_kemb

    Public nobukti As String

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = String.Format("SELECT     trpengembalian_sw.nobukti, trpengembalian_sw.tanggal, trpengembalian_sw.tgl_masuk, ms_toko.kd_toko,ms_toko.kd_toko+ ' - ' + ms_toko.nama_toko as nama_toko, ms_toko.alamat_toko, " & _
            "trpengembalian_sw.nobukti_sw, trpengembalian_sw.note, ms_barang.kd_barang, ms_barang.nama_barang, trpengembalian_sw2.qty, " & _
            "trpengembalian_sw2.satuan " & _
            "FROM         trpengembalian_sw INNER JOIN " & _
            "trpengembalian_sw2 ON trpengembalian_sw.nobukti = trpengembalian_sw2.nobukti INNER JOIN " & _
            "ms_toko ON trpengembalian_sw.kd_toko = ms_toko.kd_toko INNER JOIN " & _
            "ms_barang ON trpengembalian_sw2.kd_barang = ms_barang.kd_barang " & _
            "WHERE trpengembalian_sw.sbatal=0 and trpengembalian_sw.nobukti='{0}'", nobukti)

            Dim ds As DataSet = New dssewa_kemb
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_sewa_kemb() With {.DataSource = ds.Tables(0)}
            rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
            rrekap.DataMember = rrekap.DataMember
            rrekap.PrinterName = varprinter1
            rrekap.CreateDocument(True)

            PrintControl1.PrintingSystem = rrekap.PrintingSystem

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

    Private Sub fpr_rekapaktur_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        load_print()
    End Sub


End Class