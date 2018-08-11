Imports System.Data
Imports System.Data.OleDb
Imports Grand.Clsmy

Imports DevExpress.XtraReports.UI

Public Class fpr_invoice2

    Public sql1 As String
    Public sql2 As String
    Public sql3 As String
    Public sqlbonus As String
    Public sqldetail As String
    Public nobukti As String


    Private Sub loadfaktur()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet = New dsinvoice
            ds = Clsmy.GetDataSet(sql1, cn)

            dsinvoice_ku = New DataSet
            dsinvoice_ku = ds

            Dim ds2 As DataSet = New dsinvoice2
            ds2 = Clsmy.GetDataSet(sql2, cn)

            Dim dsbonus As DataSet = New ds_bonus
            dsbonus = Clsmy.GetDataSet(sqlbonus, cn)

            Dim ds3 As DataSet = New dsbarang_to
            ds3 = Clsmy.GetDataSet(sql3, cn)

            Dim dsdetail As DataSet = New DataSet
            dsdetail = Clsmy.GetDataSet(sqldetail, cn)

            Dim dtdetail As DataTable = dsdetail.Tables(0)

            For i As Integer = 0 To dtdetail.Rows.Count - 1

                Dim kdbar As String = dtdetail(i)("kd_barang").ToString
                Dim qty As Integer = Integer.Parse(dtdetail(i)("qty").ToString)
                Dim satuan As String = dtdetail(i)("satuan").ToString
                Dim harga As Integer = Double.Parse(dtdetail(i)("harga").ToString)
                Dim disc As Integer = Double.Parse(dtdetail(i)("disc_rp").ToString)
                Dim jumlah As Integer = Double.Parse(dtdetail(i)("jumlah").ToString)

                For x As Integer = 0 To ds3.Tables(0).Rows.Count - 1

                    If ds3.Tables(0).Rows(x)("kd_barang").ToString.Equals(kdbar) Then

                        ds3.Tables(0).Rows(x)("qty") = qty
                        ds3.Tables(0).Rows(x)("satuan") = satuan
                        ds3.Tables(0).Rows(x)("harga") = harga
                        ds3.Tables(0).Rows(x)("disc") = disc
                        ds3.Tables(0).Rows(x)("jumlah") = jumlah

                        Exit For

                    End If

                Next

            Next



            Dim ops As New System.Drawing.Printing.PrinterSettings

            ' Dim rinvoice2 As New r_invoice2

            'rinvoice2.DataSource = ds2.Tables(0)
            'rinvoice2.DataMember = rinvoice2.DataMember
            '  rinvoice2.PrinterName = ops.PrinterName
            '  rinvoice2.CreateDocument(True)

            'PrintControl1.PrintingSystem = rinvoice2.PrintingSystem

            Dim rinvoice As New r_invoice() With {.DataSource = ds.Tables(0)}
            rinvoice.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
            rinvoice.DataMember = rinvoice.DataMember

            rinvoice.XrSubreport1.ReportSource = New r_invoice2
            rinvoice.XrSubreport1.ReportSource.DataSource = ds2.Tables(0)
            rinvoice.XrSubreport1.ReportSource.DataMember = rinvoice.XrSubreport1.ReportSource.DataMember

            rinvoice.XrSubreport2.ReportSource = New r_invoice3
            rinvoice.XrSubreport2.ReportSource.DataSource = ds3.Tables(0)
            rinvoice.XrSubreport2.ReportSource.DataMember = rinvoice.XrSubreport2.ReportSource.DataMember

            rinvoice.PrinterName = varprinter1
            rinvoice.CreateDocument()

            Dim jmljual As Integer = 0
            For i As Integer = 0 To dsbonus.Tables(0).Rows.Count - 1
                jmljual = jmljual + Integer.Parse(dsbonus.Tables(0).Rows(i)("jml").ToString)
            Next

            If jmljual > 0 Then
                Dim rbonus As New r_invoice_bns() With {.DataSource = dsbonus.Tables(0)}
                rbonus.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
                rbonus.DataMember = rinvoice.DataMember

                rbonus.CreateDocument()

                rinvoice.Pages.AddRange(rbonus.Pages)
                rinvoice.PrintingSystem.ContinuousPageNumbering = True

            End If

            
            PrintControl1.PrintingSystem = rinvoice.PrintingSystem

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

            Dim sqlupprint As String = String.Format("update trfaktur_to set jmlprint=jmlprint+1 where nobukti='{0}'", nobukti)
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

    Private Sub fpr_invoice2_Load(sender As Object, e As System.EventArgs) Handles Me.load
        loadfaktur()
    End Sub

    Private Sub PrintPreviewBarItem9_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles PrintPreviewBarItem9.ItemClick
        cekjmlprint()
    End Sub

    Private Sub PrintPreviewBarItem8_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles PrintPreviewBarItem8.ItemClick
        cekjmlprint()
    End Sub

End Class