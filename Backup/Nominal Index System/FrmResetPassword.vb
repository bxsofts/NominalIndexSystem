Public Class FrmResetPassword

    Private Sub FrmResetPassword_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error Resume Next
        PasswordChanged = False
        Me.txtConfirm.Clear()
        Me.txtPassword.Clear()
        Me.txtPassword.Focus()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        On Error Resume Next
        Me.Close()
        PasswordChanged = False
    End Sub

    Private Sub SavePassword(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        On Error Resume Next
        If Me.txtPassword.TextLength < 6 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Please enter atleast six characters for the password.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtPassword.Focus()
            Exit Sub
        End If

        If Me.txtConfirm.Text <> Me.txtPassword.Text Then
            DevComponents.DotNetBar.MessageBoxEx.Show("The passwords do not match. Please enter again.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtConfirm.Text = ""
            Me.txtPassword.Text = ""
            Me.txtPassword.Focus()
            Exit Sub
        End If
        Password = Me.txtPassword.Text
        My.Computer.Registry.SetValue(RegistrySettingsPath, "Password", Password, Microsoft.Win32.RegistryValueKind.String)
        My.Computer.Registry.SetValue(RegistrySettingsPath, "SetPassword", 0, Microsoft.Win32.RegistryValueKind.String)
        PasswordChanged = True
        Me.Close()
    End Sub

End Class