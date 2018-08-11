Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpr_pengembalian

    Public nobukti As String

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = String.Format("SELECT     trpengembalian.nobukti, trpengembalian.tanggal, trpengembalian.tgl_masuk, ms_toko.kd_toko, ms_toko.kd_toko+' - '+ms_toko.nama_toko as nama_toko, ms_toko.alamat_toko, " & _
            "trpengembalian.note, trpengembalian2.kd_barang, ms_barang.nama_barang, trpengembalian2.qty, trpengembalian2.satuan " & _
            "FROM         trpengembalian INNER JOIN " & _
            "trpengembalian2 ON trpengembalian.nobukti = trpengembalian2.nobukti INNER JOIN " & _
            "ms_barang ON trpengembalian2.kd_barang = ms_barang.kd_barang INNER JOIN " & _
            "ms_toko ON trpengembalian.kd_toko = ms_toko.kd_toko WHERE trpengembalian.sbatal=0 and trpengembalian.nobukti='{0}'", nobukti)

            Dim ds As DataSet = New dspengembalian
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_pengembalian() With {.DataSource = ds.Tables(0)}
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