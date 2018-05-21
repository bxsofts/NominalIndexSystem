Imports Microsoft.Reporting.WinForms
Imports System.Data
Public Class frmView


    Public Sub LoadData() ' Handles 'MyBase.Load
        On Error Resume Next
        If Me.profileTableAdapter.Connection.State = ConnectionState.Open Then Me.profileTableAdapter.Connection.Close()
        Me.profileTableAdapter.Connection.ConnectionString = ConString
        Me.profileTableAdapter.Connection.Open()

        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        Me.ReportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent
        Me.ReportViewer1.ZoomPercent = 100
    End Sub
End Class