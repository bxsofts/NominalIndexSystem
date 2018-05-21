
Imports System.Drawing
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Rendering

Public NotInheritable Class frmSplashScreen


    Private Sub frmSplashScreen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        On Error Resume Next
        SetTheme()
        If My.Application.Info.Title <> "" Then
            ApplicationTitle.Text = My.Application.Info.Title
        Else
            ApplicationTitle.Text = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Version.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)

        Copyright.Text = My.Application.Info.Copyright
        Me.lblbaiju.Text = My.Application.Info.CompanyName
    End Sub

    Private Sub SetTheme()
        On Error Resume Next
        Dim style As String
        style = My.Computer.Registry.GetValue(RegistrySettingsPath, "Style", "Blue")

        Select Case style
            Case "Blue"
                RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.Blue)
            Case "Black"
                RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.Black)

            Case "Silver"
                RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.Silver)
            Case "Vista"
                RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.VistaGlass)

            Case Else
                RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.Blue)
        End Select

        Dim UseCustom As String = My.Computer.Registry.GetValue(RegistrySettingsPath, "UseCustom", 0)
        If UseCustom = "1" Then
            m_BaseColorScheme = CType(GlobalManager.Renderer, Office2007Renderer).ColorTable.InitialColorScheme
            Dim col As String = My.Computer.Registry.GetValue(RegistrySettingsPath, "CustomColor", Color.Blue.ToArgb.ToString)
            RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(Me, m_BaseColorScheme, CType(Color.FromArgb(col), Color))
            Invalidate()
        End If
    End Sub

End Class
