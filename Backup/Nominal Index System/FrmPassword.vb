Imports System.Data
Public Class FrmPassword


    Private Sub FormLoadEvents(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error Resume Next

        Dim dotnetversion As String = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5", "version", 0)
        If dotnetversion.StartsWith("3.5") = False Then
            Dim ShowDotNetVersion As String = My.Computer.Registry.GetValue(RegistrySettingsPath, "ShowDotNetVersion", 1)

            If ShowDotNetVersion = 1 Then
                Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Microsoft .NET 3.5 is not installed. The application will not work correctly until it is installed." & vbNewLine & "Do you want to see this message again next time?", AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
                If r = Windows.Forms.DialogResult.Yes Then My.Computer.Registry.SetValue(RegistrySettingsPath, "ShowDotNetVersion", 1, Microsoft.Win32.RegistryValueKind.String)
                If r = Windows.Forms.DialogResult.No Then My.Computer.Registry.SetValue(RegistrySettingsPath, "ShowDotNetVersion", 0, Microsoft.Win32.RegistryValueKind.String)
            End If

        End If


        Dim setpassword As String = My.Computer.Registry.GetValue(RegistrySettingsPath, "SetPassword", 1)
        Password = My.Computer.Registry.GetValue(RegistrySettingsPath, "Password", "")
        If setpassword = "0" Then
            frmMainInterface.Show()
            Me.Close()
        End If
        DBPath = AppPath & "\FPB.mdb"
        If DBHasPassword(DBPath) Then
            If OldPasswordMatches(Password) = False Then
                lblMessage.Text = "The current database has a password. Please enter it as the Password."
            End If
        End If
        Me.txtConfirm.Text = ""
        Me.txtPassword.Text = ""
        Me.txtPassword.Focus()
    End Sub

    Private Sub SavePassword(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        On Error Resume Next
        If lblMessage.Text <> "The current database has a password. Please enter it as the Password." Then
            If Me.txtPassword.TextLength < 6 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Please enter atleast six characters for the password.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtPassword.Focus()
                Exit Sub
            End If
        End If
        If Me.txtConfirm.Text <> Me.txtPassword.Text Then
            DevComponents.DotNetBar.MessageBoxEx.Show("The passwords do not match. Please enter again.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtConfirm.Text = ""
            Me.txtPassword.Text = ""
            Me.txtPassword.Focus()
            Exit Sub
        End If


        Dim OldPassword = Password
        Password = Me.txtPassword.Text
        If ChangedDBPassword(OldPassword, Password) Then
            My.Computer.Registry.SetValue(RegistrySettingsPath, "Password", Password, Microsoft.Win32.RegistryValueKind.String)
            My.Computer.Registry.SetValue(RegistrySettingsPath, "SetPassword", 0, Microsoft.Win32.RegistryValueKind.String)
            DevComponents.DotNetBar.MessageBoxEx.Show("Password accepted!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            frmMainInterface.Show()
            Me.Close()
        End If

       
    End Sub


    Private Function ChangedDBPassword(ByVal OldPassword As String, ByVal NewPassWord As String) As Boolean
        ChangedDBPassword = False
        Try
            BackupDatabase()
            DBPath = AppPath & "\FPB.mdb"
            If DBHasPassword(DBPath) = False Then
                OldPassword = ""
            End If
            Dim CS = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DBPath & "; Jet OLEDB:Database Password=" & OldPassword & "; Mode=Share Deny Read | Share Deny Write;"
            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(CS)
            con.Open()
            Dim sql As String = "ALTER DATABASE PASSWORD [" & NewPassWord & "] [" & OldPassword & "]"
            Dim cmd = New OleDb.OleDbCommand(sql, con)
            cmd.ExecuteNonQuery()
            con.Close()
            ChangedDBPassword = True
        Catch ex As Exception
            DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message, AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ChangedDBPassword = False
        End Try

    End Function

    Private Function DBHasPassword(ByVal DB As String) As Boolean
        DBHasPassword = False

        Try

            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DB)
            con.Open()
            DBHasPassword = False
            con.Close()
        Catch ex As Exception
            If ex.Message.ToLower = "not a valid password." Then
                DBHasPassword = True
            Else
                ' DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message, AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End Try
    End Function


    Private Function OldPasswordMatches(ByVal OldPassword As String) As Boolean
        OldPasswordMatches = False
        Try
            Dim CS = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DBPath & "; Jet OLEDB:Database Password=" & OldPassword
            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(CS)
            con.Open()

            OldPasswordMatches = True
            con.Close()
        Catch ex As Exception
            ' DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message, AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            OldPasswordMatches = False
        End Try

    End Function

    Private Sub BackupDatabase()
        On Error Resume Next
        Dim Source As String = AppPath & "\FPB.mdb"

        Dim Destination As String = My.Computer.Registry.GetValue(RegistrySettingsPath, "BackupPath", FileIO.SpecialDirectories.MyDocuments & "\BXSofts\" & AppName & "\Backups")

        My.Computer.Registry.SetValue(RegistrySettingsPath, "BackupPath", Destination, Microsoft.Win32.RegistryValueKind.String)

        If My.Computer.FileSystem.DirectoryExists(Destination) = False Then
            My.Computer.FileSystem.CreateDirectory(Destination)
        End If
        If Strings.Right(Destination, 1) <> "\" Then Destination = Destination & "\"

        Dim BackupFileName As String = "FPBBackup-BeforeAddingPasswordOn-" & Strings.Format(Now, "dd-MM-yyyy-h-mm-ss-tt") & ".fpb"

        Destination = Destination & BackupFileName
        My.Computer.FileSystem.CopyFile(Source, Destination, True)
    End Sub
End Class