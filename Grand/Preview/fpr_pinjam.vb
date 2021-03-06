﻿Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpr_pinjam

    Public nobukti As String

    Private Sub load_print()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn()

            Dim sql As String = String.Format("SELECT     trpinjam.nobukti, trpinjam.tanggal, trpinjam.tgl_keluar, ms_toko.kd_toko,ms_toko.kd_toko+ ' - ' +ms_toko.nama_toko as nama_toko, ms_toko.alamat_toko, trpinjam.note, ms_barang.kd_barang, " & _
            "ms_barang.nama_barang, trpinjam2.qty, trpinjam2.satuan " & _
            "FROM         trpinjam INNER JOIN " & _
            "trpinjam2 ON trpinjam.nobukti = trpinjam2.nobukti INNER JOIN " & _
            "ms_toko ON trpinjam.kd_toko = ms_toko.kd_toko INNER JOIN " & _
            "ms_barang ON trpinjam2.kd_barang = ms_barang.kd_barang " & _
            "WHERE trpinjam.sbatal=0 and trpinjam.nobukti='{0}'", nobukti)

            Dim ds As DataSet = New dspiinjam
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings
            Dim rrekap As New r_pinjam() With {.DataSource = ds.Tables(0)}
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