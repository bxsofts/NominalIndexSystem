Imports System.Data
Public Class frmSearchCriteria



    Private Sub CloseForm(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        On Error Resume Next
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        On Error Resume Next
        Me.Close()
    End Sub


    Private Sub btnSearchNow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchNow.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(ConString)
            con.Open()
            Dim cmd As OleDb.OleDbCommand = New OleDb.OleDbCommand(Me.txtSQL.Text, con)
            Dim da As New OleDb.OleDbDataAdapter(cmd)
            frmMainInterface.FPBDataSet.profile.Clear()
            da.Fill(frmMainInterface.FPBDataSet.profile)
            con.Close()
            Me.Cursor = Cursors.Default
            frmMainInterface.DisplayRecordCount()
            Me.Cursor = Cursors.Default
            frmMainInterface.ShowAlertMessage("Search finished. Found " & IIf(frmMainInterface.DataGridView.RowCount = 1, "1 Record", frmMainInterface.DataGridView.RowCount & " Records"))
            Application.DoEvents()
        Catch ex As Exception
            DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message, AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.Cursor = Cursors.Default
        End Try

    End Sub
End Class