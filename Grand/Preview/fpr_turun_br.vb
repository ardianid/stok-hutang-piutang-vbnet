Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpr_turun_br

    Public nobukti As String

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = String.Format("select a.nobukti,a.tanggal,a.tgl_turun,a.nobukti_spm,a.kd_gudang,b.kd_barang,c.nama_lap as nama_barang,b.qty,b.satuan,b.qty_tr,b.kd_gudang as nopol,c.nohrus " & _
                "from trturun_br a inner join trturun_br2 b on a.nobukti=b.nobukti " & _
                "inner join ms_barang c on b.kd_barang=c.kd_barang " & _
                "where a.sbatal=0 and a.nobukti='{0}' " & _
                "union all " & _
                "select x1.nobukti,null as tanggal,null as tgl_turun,'' as nobukti_spm,'' as kd_gudang,x2.kd_barang,x2.nama_lap +' *(Ksg)' as nama_barang,0 as qty_a,x1.satuan,x1.qty,'' as nopol,x2.nohrus " & _
                "from trturun_br3 x1 inner join ms_barang x2 on x1.kd_barang=x2.kd_barang " & _
                "where x1.nobukti='{0}'", nobukti)

                'Dim sql2 As String = String.Format("SELECT trturun_br3.nobukti,ms_barang.kd_barang, ms_barang.nama_barang, trturun_br3.kd_gudang, trturun_br3.qty, trturun_br3.satuan " & _
                '"FROM trturun_br3 INNER JOIN " & _
                '"ms_barang ON trturun_br3.kd_barang = ms_barang.kd_barang where trturun_br3.nobukti='{0}'", nobukti)

            Dim ds As DataSet = New dsturun_brg
            ds = Clsmy.GetDataSet(sql, cn)

                'Dim ds2 As DataSet = New dsturun_brg2
                'ds2 = Clsmy.GetDataSet(sql2, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_turun_brang() With {.DataSource = ds.Tables(0)}
            rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
            rrekap.DataMember = rrekap.DataMember

                'rrekap.XrSubreport1.ReportSource = New r_turun_brang2
                'rrekap.XrSubreport1.ReportSource.DataSource = ds2.Tables(0)
                'rrekap.XrSubreport1.ReportSource.DataMember = rrekap.XrSubreport1.ReportSource.DataMember

            rrekap.PrinterName = varprinter2
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