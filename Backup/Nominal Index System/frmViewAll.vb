Imports Microsoft.Reporting.WinForms

Public Class frmViewAll



    Private Sub frmView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
      

        On Error Resume Next
        If Me.profileTableAdapter.Connection.State = Data.ConnectionState.Open Then Me.profileTableAdapter.Connection.Close()
        Me.profileTableAdapter.Connection.ConnectionString = ConString
        Me.profileTableAdapter.Connection.Open()

        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        Me.ReportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent
        Me.ReportViewer1.ZoomPercent = 100
    End Sub
End Class