Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpr_rekapaktur

    Public nobukti As String

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            'Dim sql As String = String.Format("SELECT     trrekap_to.nobukti, trrekap_to.tgl, trrekap_to.tglmuat, trrekap_to.tglkirim, ms_supir.nama_karyawan AS nama_supir, ms_kenek1.nama_karyawan AS nama_kenek1, " & _
            '          "ms_kenek2.nama_karyawan AS nama_kenek2, ms_kenek3.nama_karyawan AS nama_kenek3, ms_jalur_kirim.nama_jalur, trrekap_to.nopol, trrekap_to.note, " & _
            '          "ms_barang.nama_barang, trfaktur_to2.qtykecil, ms_barang.satuan1, ms_barang.satuan2, ms_barang.satuan3, ms_gudang.kd_gudang, ms_gudang.nama_gudang,  " & _
            '          "ms_barang.qty1, ms_barang.qty2, ms_barang.qty3,ms_barang.nourut_lap,trrekap_to.tot_nota " & _
            '            "FROM         trrekap_to INNER JOIN " & _
            '          "trrekap_to2 ON trrekap_to.nobukti = trrekap_to2.nobukti INNER JOIN " & _
            '          "trfaktur_to ON trrekap_to2.nobukti_fak = trfaktur_to.nobukti INNER JOIN " & _
            '          "trfaktur_to2 ON trfaktur_to.nobukti = trfaktur_to2.nobukti INNER JOIN " & _
            '          "ms_barang ON trfaktur_to2.kd_barang = ms_barang.kd_barang INNER JOIN " & _
            '          "ms_gudang ON trfaktur_to2.kd_gudang = ms_gudang.kd_gudang LEFT OUTER JOIN " & _
            '          "ms_jalur_kirim ON trrekap_to.kd_jalur = ms_jalur_kirim.kd_jalur LEFT OUTER JOIN " & _
            '          "ms_pegawai AS ms_kenek3 ON trrekap_to.kd_kenek3 = ms_kenek3.kd_karyawan LEFT OUTER JOIN " & _
            '          "ms_pegawai AS ms_kenek2 ON trrekap_to.kd_kenek2 = ms_kenek2.kd_karyawan LEFT OUTER JOIN " & _
            '          "ms_pegawai AS ms_kenek1 ON trrekap_to.kd_kenek1 = ms_kenek1.kd_karyawan LEFT OUTER JOIN " & _
            '          "ms_pegawai AS ms_supir ON trrekap_to.kd_supir = ms_supir.kd_karyawan " & _
            '            "WHERE trrekap_to.sbatal = 0 AND ms_barang.jenis = 'FISIK' AND trrekap_to.nobukti = '{0}'", nobukti)

            Dim sql As String = String.Format("SELECT     trrekap_to.nobukti, trrekap_to.tgl, trrekap_to.tglmuat, trrekap_to.tglkirim, trrekap_to.nopol, supir.nama_karyawan AS nama_supir, " & _
                      "kenek1.nama_karyawan AS nama_kenek1, kenek2.nama_karyawan AS nama_kenek2, kenek3.nama_karyawan AS nama_kenek3, ms_jalur_kirim.nama_jalur, " & _
                      "trrekap_to.tot_nota, ms_barang.kd_barang, ms_barang.nama_lap as nama_barang, (trrekap_to3.jml / (ms_barang.qty1 * ms_barang.qty2 * ms_barang.qty3)) as jml , ms_barang.satuan1,ms_barang.nourut_lap as nohrus " & _
                      "FROM         ms_pegawai AS kenek1 RIGHT OUTER JOIN " & _
                      "ms_pegawai AS kenek2 RIGHT OUTER JOIN " & _
                      "ms_jalur_kirim RIGHT OUTER JOIN " & _
                      "trrekap_to INNER JOIN " & _
                      "trrekap_to3 ON trrekap_to.nobukti = trrekap_to3.nobukti INNER JOIN " & _
                      "ms_barang ON trrekap_to3.kd_barang = ms_barang.kd_barang ON ms_jalur_kirim.kd_jalur = trrekap_to.kd_jalur LEFT OUTER JOIN " & _
                      "ms_pegawai AS kenek3 ON trrekap_to.kd_kenek3 = kenek3.kd_karyawan ON kenek2.kd_karyawan = trrekap_to.kd_kenek2 ON  " & _
                      "kenek1.kd_karyawan = trrekap_to.kd_kenek1 LEFT OUTER JOIN " & _
                      "ms_pegawai AS supir ON trrekap_to.kd_supir = supir.kd_karyawan " & _
                      "WHERE not(ms_barang.kd_barang in ('BN0002','BN0003')) and trrekap_to.sbatal=0 and trrekap_to.nobukti='{0}'", nobukti)

            Dim sqlsub As String = String.Format("SELECT     trrekap_to.nobukti, " & _
            "ms_barang.kd_barang, ms_barang.nama_lap as nama_barang, sum(trrekap_to3.jml) as jml " & _
            "FROM         ms_pegawai AS kenek1 RIGHT OUTER JOIN  " & _
            "ms_pegawai AS kenek2 RIGHT OUTER JOIN  " & _
            "ms_jalur_kirim RIGHT OUTER JOIN  " & _
            "trrekap_to INNER JOIN  " & _
            "trrekap_to3 ON trrekap_to.nobukti = trrekap_to3.nobukti INNER JOIN  " & _
            "ms_barang ON trrekap_to3.kd_barang = ms_barang.kd_barang ON ms_jalur_kirim.kd_jalur = trrekap_to.kd_jalur LEFT OUTER JOIN  " & _
            "ms_pegawai AS kenek3 ON trrekap_to.kd_kenek3 = kenek3.kd_karyawan ON kenek2.kd_karyawan = trrekap_to.kd_kenek2 ON   " & _
            "kenek1.kd_karyawan = trrekap_to.kd_kenek1 LEFT OUTER JOIN  " & _
            "ms_pegawai AS supir ON trrekap_to.kd_supir = supir.kd_karyawan  " & _
            "WHERE ms_barang.kd_barang in ('BN0002','BN0003') and trrekap_to.sbatal=0 and trrekap_to.nobukti='{0}' " & _
            "group by trrekap_to.nobukti,ms_barang.kd_barang, ms_barang.nama_lap", nobukti)

            Dim ds As DataSet = New dsrekap2
            ds = Clsmy.GetDataSet(sql, cn)

            Dim dsbonus As DataSet = New DataSet
            dsbonus = Clsmy.GetDataSet(sqlsub, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_rekapfak2() With {.DataSource = ds.Tables(0)}
            rrekap.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
            rrekap.DataMember = rrekap.DataMember

            If dsbonus.Tables(0).Rows.Count > 0 Then
                If Integer.Parse(dsbonus.Tables(0).Rows(0)("jml").ToString) > 0 Then
                    rrekap.XrSubreport1.ReportSource = New r_rekapfak_bonus
                    rrekap.XrSubreport1.ReportSource.DataSource = dsbonus.Tables(0)
                    rrekap.XrSubreport1.ReportSource.DataMember = rrekap.XrSubreport1.ReportSource.DataMember
                Else
                    rrekap.XrSubreport1.Visible = False
                End If
            Else
                rrekap.XrSubreport1.Visible = False
            End If

            rrekap.XrSubreport2.ReportSource = New r_rekapfak2_detail
            rrekap.XrSubreport2.ReportSource.DataSource = ds.Tables(0)
            rrekap.XrSubreport2.ReportSource.DataMember = rrekap.XrSubreport2.ReportSource.DataMember


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

    Private Sub cekjmlprint()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqlupprint As String = String.Format("update trrekap_to set jmlprint=jmlprint+1 where nobukti='{0}'", nobukti)
            Using cmdupprint As OleDbCommand = New OleDbCommand(sqlupprint, cn)
                cmdupprint.ExecuteNonQuery()
            End Using

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

    Private Sub PrintPreviewBarItem9_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles PrintPreviewBarItem9.ItemClick
        cekjmlprint()
    End Sub

    Private Sub PrintPreviewBarItem8_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles PrintPreviewBarItem8.ItemClick
        cekjmlprint()
    End Sub

End Class