Public Class frmImportData



    
    Private Sub btnCancelImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelImport.Click
        CancelImport = True
        ' Me.Close()
    End Sub

    Private Sub frmImportData_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CancelImport = False
        Me.Cursor = Cursors.WaitCursor
        Me.btnCancelImport.Cursor = Cursors.Default
    End Sub
End Class