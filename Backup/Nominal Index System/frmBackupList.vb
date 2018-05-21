

Public Class frmBackupList

    Dim BalloonMessage As DevComponents.DotNetBar.Balloon 'balloon mesaage
    Dim BackupPath As String = My.Computer.Registry.GetValue(RegistrySettingsPath, "BackupPath", FileIO.SpecialDirectories.MyDocuments & "\BXSofts\" & AppName & "\Backups")

    Private Sub frmBackupList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error Resume Next
        Me.listViewEx1.Items.Clear()


        For Each foundFile As String In My.Computer.FileSystem.GetFiles(BackupPath, FileIO.SearchOption.SearchTopLevelOnly, "FPBBackup*.fpb")
            If foundFile Is Nothing Then
                Exit Sub
            End If

            Me.listViewEx1.Items.Add(My.Computer.FileSystem.GetName(foundFile))
        Next
        For i As Integer = 0 To Me.listViewEx1.Items.Count - 1
            listViewEx1.Items(i).ImageIndex = 0
        Next
    End Sub


    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRestore.Click, listViewEx1.DoubleClick
        On Error GoTo errhandler
        If Me.listViewEx1.Items.Count = 0 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("No backup files in the list", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
            Exit Sub
        End If
        If Me.listViewEx1.SelectedItems.Count = 0 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Please select a file", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        frmInputBox.SetTitleandMessage("User Authentication Required", "Please enter password to restore Database.")
        frmInputBox.AcceptButton = frmInputBox.btnOK
        frmInputBox.txtInputBox.UseSystemPasswordChar = True
        frmInputBox.ShowDialog()
        If frmInputBox.ButtonClicked <> "OK" Then Exit Sub
        If frmInputBox.txtInputBox.Text <> Password Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Wrong password!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim result As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Restoring the database will overwrite the existing database." & vbNewLine & "Do you want to continue?", AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)

        If result = Windows.Forms.DialogResult.Yes Then
            BackupFile = BackupPath & "\" & Me.listViewEx1.FocusedItem.SubItems(0).Text
            '  MsgBox(BackupFile)
            My.Computer.FileSystem.CopyFile(BackupFile, AppPath & "\FPB.mdb", True)
            Application.DoEvents()
            restored = True
            frmMainInterface.LoadDataToTable()
            Me.Close()
        End If
        Exit Sub
errhandler:
        DevComponents.DotNetBar.MessageBoxEx.Show(Err.Description, AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        restored = False
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        On Error Resume Next
        Me.Close()
    End Sub

    Private Sub CloseForm(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        On Error Resume Next
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub RemoveBackupFile() Handles btnRemove.Click
        On Error Resume Next
        If Me.listViewEx1.Items.Count = 0 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("No backup files in the list", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
            Exit Sub
        End If
        If Me.listViewEx1.SelectedItems.Count = 0 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Please select a file", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        frmInputBox.SetTitleandMessage("User Authentication Required", "Please enter password to remove the Backup File.")
        frmInputBox.AcceptButton = frmInputBox.btnOK
        frmInputBox.txtInputBox.UseSystemPasswordChar = True
        frmInputBox.ShowDialog()
        If frmInputBox.ButtonClicked <> "OK" Then Exit Sub
        If frmInputBox.txtInputBox.Text <> Password Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Wrong password!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim result As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Do you really want to remove the selected backup file?", AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)

        If result = Windows.Forms.DialogResult.Yes Then
            My.Computer.FileSystem.DeleteFile(BackupPath & "\" & Me.listViewEx1.FocusedItem.SubItems(0).Text, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
            Me.listViewEx1.FocusedItem.Remove()
            Application.DoEvents()
            frmMainInterface.ShowAlertMessage("Selected backup file deleted to the Recycle Bin!")
        End If
    End Sub
End Class