Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Rendering
Imports System.Threading 'to create a mutex which will ensure that only one application is running

Module modMain
    Public AppName As String = "Nominal Index System"
    Public RegistrySettingsPath As String = "HKEY_CURRENT_USER\Software\BXSofts\Nominal Index System\Settings"
    Public AppPath As String = My.Application.Info.DirectoryPath
    Public BackupFile As String = ""
    Public restored As Boolean = False
    Public m_ColorSelected As Boolean = False 'style color
    Public m_BaseColorScheme As eOffice2007ColorScheme = eOffice2007ColorScheme.Blue 'style scheme
    Public objMutex As Mutex 'mutex object
    Public MergeCon As String
    Public CancelImport As Boolean = False
    Public Password As String = ""
    Public PasswordChanged As Boolean = False
    Public DBPath As String = vbNullString
    Public ConString As String = vbNullString
End Module
