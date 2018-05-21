
Option Explicit On

Imports System.Drawing
Imports DevComponents.DotNetBar 'to use dotnetbar components
Imports DevComponents.DotNetBar.Rendering ' to use office 2007 style forms
Imports System.Drawing.Imaging
Imports System.Data
Public Class frmMainInterface




#Region "VARIABLES DECLARATION "

    Dim EditMode As Boolean 'handles the action to be taken by Save button click
    Dim OriginalTIN As String = vbNullString 'TIN number 
    Dim OriginalRCN As String = vbNullString
    Dim SearchSetting As Integer = 0 'Begins with
    Dim Sex As String = "Male" 'male
    Dim OVStatus As Integer = 0 'not ov
    Dim BalloonMessage As DevComponents.DotNetBar.Balloon 'to show the alert message
    Public SelectedRowIndex As Long = -1
    Public SelectedColumnIndex As Integer = -1
    Public RecentRecordsList As Collection
    Dim FPSlipImageFile As String = vbNullString
    Dim FPSlipImageImportLocation As String
    WithEvents devmanager As WIA.DeviceManager
    Dim ColumnHeaderClicked As Boolean = False
#End Region



#Region "APPLICATION LOAD EVENTS"


    Private Sub ApplicationLoadEvents(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        On Error Resume Next
        If FrmPassword.Visible Then FrmPassword.Close()
        Me.Cursor = Cursors.WaitCursor
        Me.ProgressBar.Visible = True
        Me.ProgressBar.Maximum = 29
        Me.ProgressBar.Text = "Loading Application..."

        LoadQuickToolBarSettings()
        Me.ProgressBar.Increment(1)
        Me.RibbonControl1.SelectedRibbonTabItem = Me.tabHome
        Me.RibbonControl1.Expanded = My.Computer.Registry.GetValue(RegistrySettingsPath, "RibbonVisible", 1)
        Me.ProgressBar.Increment(1)
        SetDatagridSortMode()
        Me.ProgressBar.Increment(1)
        LoadDatagridColumnWidth()
        Me.ProgressBar.Increment(1)
        LoadDatagridColumnOrder()
        Me.ProgressBar.Increment(1)
        SetColorTheme()
        Me.ProgressBar.Increment(1)
        EditMode = False
        SetDatagridFont()
        Me.ProgressBar.Increment(1)
        Me.txtTIN.Focus()
        InitializeFields()
        Me.ProgressBar.Increment(1)
        Me.ProgressBar.Text = "Loading Settings..."
        Application.DoEvents()

        Dim hcolor As Integer = My.Computer.Registry.GetValue(RegistrySettingsPath, "HighLightColor", 2) 'blue
        Me.cmbHighLightColor.SelectedIndex = hcolor - 1

        Dim k As String = My.Computer.Registry.GetValue(RegistrySettingsPath, "EnterKeyAction", 2)
        Select Case k
            Case 0 : rdoNoAction.Checked = True
            Case 1 : rdoSearch.Checked = True
            Case 2 : rdoSave.Checked = True
            Case Else : rdoNoAction.Checked = True
        End Select
        EnterKeyAction()
        Me.ProgressBar.Increment(1)
        Me.chkShowToolTips.Checked = My.Computer.Registry.GetValue(RegistrySettingsPath, "ShowToolTips", 1)
        Me.ProgressBar.Increment(1)
        Me.chkShowPopups.Checked = My.Computer.Registry.GetValue(RegistrySettingsPath, "ShowPopups", 1)
        Me.ProgressBar.Increment(1)
        Me.chkPlaySound.Checked = My.Computer.Registry.GetValue(RegistrySettingsPath, "PlaySound", 1)
        Me.ProgressBar.Increment(1)
        Me.cmbAutoCompletionMode.SelectedIndex = My.Computer.Registry.GetValue(RegistrySettingsPath, "AutoCompleteMode", 1)
        Me.ProgressBar.Increment(1)
        Me.chkLoadRecordsAtStartup.Checked = My.Computer.Registry.GetValue(RegistrySettingsPath, "LoadDataAtStatrtup", 1)
        Me.ProgressBar.Increment(1)
        ' UpdateNullFields()
        Me.ProgressBar.Increment(1)
        ShowToolTips()
        Me.ProgressBar.Increment(1)
        ShowPopups()
        Me.ProgressBar.Increment(1)
        PlaySoundOnPopups()
        Me.ProgressBar.Increment(1)
        SetAutoCompleteMode()
        Me.ProgressBar.Increment(1)
        SetOVStatus()
        Me.ProgressBar.Increment(1)
        SetSexStatus()
        Me.ProgressBar.Increment(1)
        GetAutoFields()
        Me.ProgressBar.Increment(1)
        GetFPImageImportLocation()
        Me.ProgressBar.Increment(1)
        Me.chkLoadAutoText.Checked = My.Computer.Registry.GetValue(RegistrySettingsPath, "LoadAutoTextFromDB", 0)
        Me.ProgressBar.Increment(1)
        RecentRecordsList = New Collection
        LoadRecentRecordsList()
        Me.ProgressBar.Increment(1)

        Me.dtLastTrace.MonthCalendar.DisplayMonth = Today()
        Me.ProgressBar.Increment(1)
        devmanager = New WIA.DeviceManager
        devmanager.RegisterEvent(WIA.EventID.wiaEventDeviceConnected)
        Me.ProgressBar.Increment(1)
        devmanager.RegisterEvent(WIA.EventID.wiaEventDeviceDisconnected)
        Me.ProgressBar.Increment(1)

        ConnectToDatabase()

        Me.ProgressBar.Text = "Loading Data..."
        Application.DoEvents()
        If Me.chkLoadRecordsAtStartup.Checked = True Then
            LoadDataToTable() 'loads data to the datagrid
        End If

        If Me.chkLoadAutoText.Checked = True Then
            ReLoadAutoText()
        End If
        UpdateNullValues()
        Me.Cursor = Cursors.Default 'now set the default cursor
        Me.ProgressBar.Visible = False
        If FileIO.FileSystem.FileExists(AppPath & "\FPB.mdb") = False Then 'if database not found

            DevComponents.DotNetBar.MessageBoxEx.Show("A critical error has occured while loading the application." & vbNewLine & "The database file ""FPB.mdb"" not found in the application directory." & vbNewLine & "Please select a previous backup file to restore the database.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            RestoreDatabase()
        End If
        Application.DoEvents()
        Me.Cursor = Cursors.Default
        Me.DataGridView.Cursor = Cursors.Default
        Me.BringToFront()
        ShowAlertMessage("Welcome to " & AppName & vbNewLine & "Have a nice day!")
    End Sub

#End Region



#Region "QUICK TOOLBAR SETTINGS"
    Sub LoadQuickToolBarSettings()
        On Error Resume Next
        Dim layout As String = My.Computer.Registry.GetValue(RegistrySettingsPath, "QTBarLayout", "0,btnNewEntry,btnOpen,btnEdit,btnDelete")
        If layout <> "" And Not layout Is Nothing Then
            RibbonControl1.QatLayout = layout
        End If
        With RibbonControl1.QatFrequentCommands
            .Add(btnNewEntry)
            .Add(btnOpen)
            .Add(btnEdit)
            .Add(btnDelete)
            .Add(btnImport)
            .Add(btnExportData)
            .Add(btnPrint)
            .Add(btnReloadData)
            .Add(btnBackup)
            .Add(btnRestore)
            .Add(btnHelp)
            .Add(btnInfo)
            .Add(btnExit)
        End With

    End Sub


    Sub SaveQuicktoolbarSettings()
        ' Save Quick Access Toolbar layout if it has changed...
        On Error Resume Next
        If RibbonControl1.QatLayoutChanged Then
            My.Computer.Registry.SetValue(RegistrySettingsPath, "QTBarLayout", RibbonControl1.QatLayout, Microsoft.Win32.RegistryValueKind.String)
        End If
    End Sub


    Private Sub RibbonControlExpandedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RibbonControl1.ExpandedChanged
        On Error Resume Next
        If Me.RibbonControl1.Expanded = True Then
            My.Computer.Registry.SetValue(RegistrySettingsPath, "RibbonVisible", 1, Microsoft.Win32.RegistryValueKind.String)
        Else
            My.Computer.Registry.SetValue(RegistrySettingsPath, "RibbonVisible", 0, Microsoft.Win32.RegistryValueKind.String)
        End If
    End Sub
#End Region



#Region "COLOR STYLES" 'sets Color themes for the form


    Public Sub ThemeBlue() Handles btnBlue.Click 'set blue color
        On Error Resume Next
        RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.Blue)
        Me.btnBlue.Checked = True
        Me.btnBlack.Checked = False
        Me.btnSilver.Checked = False
        Me.btnVista.Checked = False
        My.Computer.Registry.SetValue(RegistrySettingsPath, "Style", "Blue", Microsoft.Win32.RegistryValueKind.String)
        My.Computer.Registry.SetValue(RegistrySettingsPath, "UseCustom", "0", Microsoft.Win32.RegistryValueKind.String)

    End Sub



    Public Sub ThemeBlack() Handles btnBlack.Click 'set black color
        On Error Resume Next
        RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.Black)
        Me.btnBlue.Checked = False
        Me.btnBlack.Checked = True
        Me.btnSilver.Checked = False
        Me.btnVista.Checked = False
        My.Computer.Registry.SetValue(RegistrySettingsPath, "Style", "Black", Microsoft.Win32.RegistryValueKind.String)
        My.Computer.Registry.SetValue(RegistrySettingsPath, "UseCustom", "0", Microsoft.Win32.RegistryValueKind.String)
    End Sub



    Public Sub ThemeSilver() Handles btnSilver.Click 'set silver color
        On Error Resume Next
        RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.Silver)
        Me.btnBlue.Checked = False
        Me.btnBlack.Checked = False
        Me.btnSilver.Checked = True
        Me.btnVista.Checked = False
        My.Computer.Registry.SetValue(RegistrySettingsPath, "Style", "Silver", Microsoft.Win32.RegistryValueKind.String)
        My.Computer.Registry.SetValue(RegistrySettingsPath, "UseCustom", "0", Microsoft.Win32.RegistryValueKind.String)
    End Sub


    Public Sub ThemeVista() Handles btnVista.Click 'set Vista color
        On Error Resume Next
        RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.VistaGlass)
        Me.btnBlue.Checked = False
        Me.btnBlack.Checked = False
        Me.btnSilver.Checked = False
        Me.btnVista.Checked = True
        My.Computer.Registry.SetValue(RegistrySettingsPath, "Style", "Vista", Microsoft.Win32.RegistryValueKind.String)
        My.Computer.Registry.SetValue(RegistrySettingsPath, "UseCustom", "0", Microsoft.Win32.RegistryValueKind.String)
    End Sub


    Public Sub ThemeCustom()
        On Error Resume Next

        Dim col As String = My.Computer.Registry.GetValue(RegistrySettingsPath, "CustomColor", Color.Blue.ToArgb.ToString)
        RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(Me, m_BaseColorScheme, CType(Color.FromArgb(col), Color))
        Invalidate()
    End Sub


    Sub SetColorTheme() 'set the color scheme
        On Error Resume Next
        Me.btnBlack.ForeColor = Color.Black
        Me.btnBlue.ForeColor = Color.Blue
        Me.btnSilver.ForeColor = Color.Silver
        Me.btnVista.ForeColor = Color.FromArgb(-16777216)
        Me.btnCustomColor.ForeColor = Color.FromArgb(My.Computer.Registry.GetValue(RegistrySettingsPath, "CustomColor", Color.Blue.ToArgb.ToString))
        Dim style As String

        style = My.Computer.Registry.GetValue(RegistrySettingsPath, "Style", "Blue")

        Select Case style
            Case "Blue"
                Me.btnBlue.Checked = True
                Me.btnBlack.Checked = False
                Me.btnSilver.Checked = False
                Me.btnVista.Checked = False
                RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.Blue)
            Case "Black"
                Me.btnBlue.Checked = False
                Me.btnBlack.Checked = True
                Me.btnSilver.Checked = False
                Me.btnVista.Checked = False
                RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.Black)

            Case "Silver"
                Me.btnBlue.Checked = False
                Me.btnBlack.Checked = False
                Me.btnSilver.Checked = True
                Me.btnVista.Checked = False
                RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.Silver)
            Case "Vista"
                Me.btnBlue.Checked = False
                Me.btnBlack.Checked = False
                Me.btnSilver.Checked = False
                Me.btnVista.Checked = True
                RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.VistaGlass)
            Case Else
                Me.btnBlue.Checked = True
                Me.btnBlack.Checked = False
                Me.btnSilver.Checked = False
                Me.btnVista.Checked = False
                RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.Blue)
        End Select
        m_BaseColorScheme = CType(GlobalManager.Renderer, Office2007Renderer).ColorTable.InitialColorScheme
        Dim UseCustom As String = My.Computer.Registry.GetValue(RegistrySettingsPath, "UseCustom", "0")
        If UseCustom = "1" Then
            ThemeCustom()
        End If
    End Sub


    Private Sub RememberLastColorBeforePreview(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCustomColor.ExpandChange
        On Error Resume Next
        If btnCustomColor.Expanded Then
            ' Remember the starting color scheme to apply if no color is selected during live-preview
            m_ColorSelected = False
            m_BaseColorScheme = CType(GlobalManager.Renderer, Office2007Renderer).ColorTable.InitialColorScheme
        Else
            If Not m_ColorSelected Then
                ' RibbonControl1.Office2007ColorTable = m_BaseColorScheme
                Dim UseCustom As String = My.Computer.Registry.GetValue(RegistrySettingsPath, "UseCustom", "0")
                If UseCustom = "1" Then
                    ThemeCustom()
                Else
                    RibbonControl1.Office2007ColorTable = m_BaseColorScheme
                End If
            End If
        End If
    End Sub


    Private Sub ShowCustomColorPreview(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.ColorPreviewEventArgs) Handles btnCustomColor.ColorPreview
        On Error Resume Next
        RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(Me, m_BaseColorScheme, e.Color)
    End Sub


    Private Sub SetCustomColor(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCustomColor.SelectedColorChanged
        On Error Resume Next
        m_ColorSelected = True ' Indicate that color was selected for buttonStyleCustom_ExpandChange method

        RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(Me, m_BaseColorScheme, CType(btnCustomColor.SelectedColor, Color))
        Invalidate()
        My.Computer.Registry.SetValue(RegistrySettingsPath, "UseCustom", "1", Microsoft.Win32.RegistryValueKind.String)
        My.Computer.Registry.SetValue(RegistrySettingsPath, "CustomColor", btnCustomColor.SelectedColor.ToArgb.ToString, Microsoft.Win32.RegistryValueKind.String)

        Me.btnCustomColor.ForeColor = btnCustomColor.SelectedColor
    End Sub



#End Region


#Region "GENERAL SETTINGS"


    Private Sub ShowSettingsTab(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOptionsMenu.Click
        On Error Resume Next
        Me.RibbonControl1.SelectedRibbonTabItem = Me.tabSettings 'select the first tab as default

    End Sub


    Sub EnterKeyAction() Handles rdoSearch.Click, rdoSave.Click, rdoNoAction.Click
        On Error Resume Next
        If rdoSave.Checked Then
            Me.AcceptButton = btnSave
            My.Computer.Registry.SetValue(RegistrySettingsPath, "EnterKeyAction", 2, Microsoft.Win32.RegistryValueKind.String)
        ElseIf rdoSearch.Checked Then
            Me.AcceptButton = btnSearchNow
            My.Computer.Registry.SetValue(RegistrySettingsPath, "EnterKeyAction", 1, Microsoft.Win32.RegistryValueKind.String)
        ElseIf rdoNoAction.Checked Then
            Dim d As New Button 'dummy key
            Me.AcceptButton = d
            My.Computer.Registry.SetValue(RegistrySettingsPath, "EnterKeyAction", 0, Microsoft.Win32.RegistryValueKind.String)
        ElseIf rdoNoAction.Checked Then
        End If

    End Sub



    Private Sub ShowOrHideDataEntryFields(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.ExpandedChangeEventArgs) Handles ExpandablePanel1.ExpandedChanged
        On Error Resume Next
        If Me.ExpandablePanel1.Expanded = True Then
            Me.ExpandablePanel1.TitleText = "Hide Data Entry Fields"
        Else
            Me.ExpandablePanel1.TitleText = "Show Data Entry Fields"
        End If
    End Sub


    Sub LoadDataAtStatrtupSettings() Handles chkLoadRecordsAtStartup.Click
        On Error Resume Next
        Dim s As Boolean = chkLoadRecordsAtStartup.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0

        My.Computer.Registry.SetValue(RegistrySettingsPath, "LoadDataAtStatrtup", v, Microsoft.Win32.RegistryValueKind.String)

    End Sub


    Private Sub ShowToolTips() Handles chkShowToolTips.Click
        On Error Resume Next
        Dim s As Boolean = chkShowToolTips.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0

        My.Computer.Registry.SetValue(RegistrySettingsPath, "ShowToolTips", v, Microsoft.Win32.RegistryValueKind.String)

        If Me.chkShowToolTips.Checked Then
            Me.SuperTooltip1.Enabled = True
        Else
            Me.SuperTooltip1.Enabled = False
        End If

    End Sub



    Private Sub SaveHighlightColor() Handles cmbHighLightColor.SelectedIndexChanged
        On Error Resume Next
        Dim c As Integer = cmbHighlightColor.SelectedIndex + 1
        My.Computer.Registry.SetValue(RegistrySettingsPath, "HighLightColor", c, Microsoft.Win32.RegistryValueKind.String)
        Me.Highlighter1.FocusHighlightColor = c
        If c = 5 Then
            Me.Highlighter1.ContainerControl = Me.PanelDummy
        Else
            Me.Highlighter1.ContainerControl = Me.ExpandablePanel1
        End If

    End Sub

#End Region


#Region "DATAGRID SETTINGS"


    Public Sub SetDatagridFont()
        On Error Resume Next
        Me.DataGridView.DefaultCellStyle.Font = New Font("Segoe UI", 11, FontStyle.Regular)
        Me.DataGridView.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        '  Me.DataGridView.Columns(0).CellTemplate.Style.Font = New Font("Segoe UI", 10, FontStyle.Bold)
    End Sub


    Sub LoadDatagridColumnWidth()
        On Error Resume Next
        Dim c As Integer
        Dim x As String
        For c = 0 To Me.DataGridView.ColumnCount - 1
            x = "ColumnWidth" & Format(c, "00")
            DataGridView.Columns(c).Width = My.Computer.Registry.GetValue(RegistrySettingsPath, x, DataGridView.Columns(c).Width)

        Next
        Me.DataGridView.RowHeadersWidth = My.Computer.Registry.GetValue(RegistrySettingsPath, "RHWidth", 60)

    End Sub


    Private Sub LoadDefaultWidth(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDefaultWidth.Click, btnResetColumnWidthMenu.Click
        On Error Resume Next

        Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("This action will reset the column widths of the table. Do you want to continue?", AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If reply = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        DataGridView.Columns(0).Width = 100
        DataGridView.Columns(1).Width = 100
        DataGridView.Columns(2).Width = 100
        DataGridView.Columns(3).Width = 175
        DataGridView.Columns(4).Width = 175
        DataGridView.Columns(5).Width = 175
        DataGridView.Columns(6).Width = 175
        DataGridView.Columns(7).Width = 175
        DataGridView.Columns(8).Width = 80
        DataGridView.Columns(9).Width = 100
        DataGridView.Columns(10).Width = 175
        DataGridView.Columns(11).Width = 175
        DataGridView.Columns(12).Width = 175
        DataGridView.Columns(13).Width = 175
        DataGridView.Columns(14).Width = 150
        DataGridView.Columns(15).Width = 100
        Me.DataGridView.RowHeadersWidth = 60
    End Sub


    Private Sub SaveDatagridColumnWidth()
        On Error Resume Next
        Dim c As Integer
        For c = 0 To Me.DataGridView.ColumnCount - 1
            My.Computer.Registry.SetValue(RegistrySettingsPath, "ColumnWidth" & Format(c, "00"), DataGridView.Columns(c).Width, Microsoft.Win32.RegistryValueKind.String)
        Next
        My.Computer.Registry.SetValue(RegistrySettingsPath, "RHWidth", Me.DataGridView.RowHeadersWidth, Microsoft.Win32.RegistryValueKind.String)


    End Sub

    Private Sub SaveDatagridColumnOrder()
        On Error Resume Next
        For i = 0 To Me.DataGridView.ColumnCount - 1
            My.Computer.Registry.SetValue(RegistrySettingsPath, "ColumnOrder" & Format(i, "00"), DataGridView.Columns(i).DisplayIndex, Microsoft.Win32.RegistryValueKind.String)
        Next
    End Sub


    Private Sub LoadDatagridColumnOrder()
        On Error Resume Next
        For i = 0 To Me.DataGridView.ColumnCount - 1
            DataGridView.Columns(i).DisplayIndex = My.Computer.Registry.GetValue(RegistrySettingsPath, "ColumnOrder" & Format(i, "00"), DataGridView.Columns(i).DisplayIndex)
        Next
    End Sub


    Private Sub LoadDatagridColumnDefaultOrder() Handles btnResetColumnOrder.Click, btnResetColumnOrderMenu.Click
        On Error Resume Next

        Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("This action will reset the column order the table. Do you want to continue?", AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If reply = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        For i = 0 To Me.DataGridView.ColumnCount - 1
            DataGridView.Columns(i).DisplayIndex = i
        Next
    End Sub

    Private Sub GenerateSerialNumber(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles DataGridView.CellPainting
        On Error Resume Next
        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center

        Dim f As Font = New Font("Segoe UI", 10, FontStyle.Bold)
        sf.LineAlignment = StringAlignment.Center
        Using b As SolidBrush = New SolidBrush(Me.ForeColor)
            If e.ColumnIndex < 0 AndAlso e.RowIndex < 0 Then
                e.Graphics.DrawString("Sl.No", f, b, e.CellBounds, sf)
                e.Handled = True
            End If

            If e.ColumnIndex < 0 AndAlso e.RowIndex >= 0 Then
                e.Graphics.DrawString((e.RowIndex + 1).ToString, f, b, e.CellBounds, sf)
                e.Handled = True
            End If
        End Using
    End Sub

    Private Sub FreezeColumns() Handles btnFreezeColumns.Click
        On Error Resume Next
        Me.DataGridView.Columns(SelectedColumnIndex).Frozen = Not Me.btnFreezeColumns.Checked
    End Sub
#End Region


#Region "DATAGRID CONTEXT MENU SETTINGS"



    Private Sub MouseOverDatagridAction(ByVal sender As Object, ByVal e As DataGridViewCellMouseEventArgs) Handles DataGridView.CellMouseClick
        On Error Resume Next
        SelectedRowIndex = e.RowIndex
        SelectedColumnIndex = e.ColumnIndex
    End Sub



    Private Sub DataGridContextMenuBarPopupOpen(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.PopupOpenEventArgs) Handles btnGridContextMenuBar1.PopupOpen
        On Error Resume Next
        If SelectedRowIndex < 0 Or SelectedRowIndex > Me.DataGridView.Rows.Count - 1 Then
            e.Cancel = True
            Exit Sub
        End If

        Me.DataGridView.Rows(SelectedRowIndex).Selected = True
        Me.DataGridView.SelectedCells(SelectedColumnIndex).Selected = True
        Me.ProfileBindingSource.Position = SelectedRowIndex
        Dim filename = Me.DataGridView.SelectedCells(16).Value.ToString
        Me.btnViewFPSlipDatagridContext.Enabled = FileIO.FileSystem.FileExists(filename)
        Me.btnLocateFPSlip.Enabled = Me.btnViewFPSlipDatagridContext.Enabled
        DisplayRecordCount()
        SelectedRowIndex = -1
    End Sub

    Private Sub ColumnHeaderContextMenuBarPopupOpen(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.PopupOpenEventArgs) Handles ColumnHeaderContextMenuBar.PopupOpen
        On Error Resume Next
        Me.btnFreezeColumns.Checked = Me.DataGridView.Columns(SelectedColumnIndex).Frozen
        If ColumnHeaderClicked Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
        ColumnHeaderClicked = False
    End Sub

#End Region



#Region "STATUS BAR TEXTS"
    Sub DisplayRecordCount() Handles ProfileBindingSource.PositionChanged
        On Error Resume Next
        Me.lblNumberOfRecords.Text = "Number of Records found: " & Me.ProfileBindingSource.Count
        If DataGridView.RowCount <> 0 Then
            lblCurrentRecord.Text = "Current Position: " & ProfileBindingSource.Position + 1
        Else
            lblCurrentRecord.Text = "Current Position: 0"
        End If

    End Sub

    Private Sub DisplayTime(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        On Error Resume Next
        Me.lblTime.Text = Format(Now, "dd/MM/yyyy h:mm:ss tt")
    End Sub
#End Region


#Region "UPDATE NULL FIELDS"

    Private Sub RemoveNullValuesFromDatabase() Handles btnRemoveNullValues.Click
        UpdateNullValues()
        ShowAlertMessage("Removed Null Values")
    End Sub


    Sub UpdateNullValues()
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        Me.ProgressBar.Visible = True
        Me.StatusBar.RecalcLayout()
        Me.ProgressBar.Text = "Removing Null Values..."
        Me.ProgressBar.Value = 0
        Me.ProgressBar.Maximum = 13

        Me.ProfileTableAdapter.UpdateQueryAddress1("")
        Me.ProgressBar.Increment(1)
        Me.ProfileTableAdapter.UpdateQueryAddress2("")
        Me.ProgressBar.Increment(1)
        Me.ProfileTableAdapter.UpdateQueryAlias1("")
        Me.ProgressBar.Increment(1)
        Me.ProfileTableAdapter.UpdateQueryAlias2("")
        Me.ProgressBar.Increment(1)
        Me.ProfileTableAdapter.UpdateQueryAliasFathers("")
        Me.ProgressBar.Increment(1)
        Me.ProfileTableAdapter.UpdateQueryBirthYear("")
        Me.ProgressBar.Increment(1)
        Me.ProfileTableAdapter.UpdateQueryDescription("")
        Me.ProgressBar.Increment(1)
        Me.ProfileTableAdapter.UpdateQueryFathersName("")
        Me.ProgressBar.Increment(1)
        Me.ProfileTableAdapter.UpdateQueryHenryClass("")
        Me.ProgressBar.Increment(1)
        Me.ProfileTableAdapter.UpdateQueryName("")
        Me.ProgressBar.Increment(1)
        Me.ProfileTableAdapter.UpdateQueryPCN("")
        Me.ProgressBar.Increment(1)
        Me.ProfileTableAdapter.UpdateQueryRCN("")
        Me.ProgressBar.Increment(1)
        Me.ProfileTableAdapter.UpdateQuerySex("")
        Me.ProgressBar.Increment(1)
        Me.ProfileTableAdapter.UpdateQueryTIN("")
        Me.ProgressBar.Increment(1)
        Me.ProfileTableAdapter.UpdateQuerySlipFile("")
        Me.ProgressBar.Increment(1)
        Me.ProgressBar.Visible = False
        Me.Cursor = Cursors.Default
    End Sub
#End Region


#Region "CONNECT TO DATABASE"

    Private Sub ConnectToDatabase()
        On Error Resume Next
        DBPath = AppPath & "\FPB.mdb"
        If DBHasPassword(DBPath) Then
            If FindConString(DBPath, Password, True) = False Then
                frmInputBox.SetTitleandMessage("Enter Password", "The Database password is different from the Password you set. Please enter password of the database to open it. If successful it will be the new password.")
                frmInputBox.AcceptButton = frmInputBox.btnOK
                frmInputBox.txtInputBox.UseSystemPasswordChar = True
                frmInputBox.ShowDialog()
                If frmInputBox.ButtonClicked <> "OK" Then Exit Sub
                Dim TestPassword = frmInputBox.txtInputBox.Text
                If FindConString(DBPath, TestPassword, True) = False Then
                    MessageBoxEx.Show("Wrong password!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                Else
                    Password = TestPassword
                    My.Computer.Registry.SetValue(RegistrySettingsPath, "Password", Password, Microsoft.Win32.RegistryValueKind.String)
                    My.Computer.Registry.SetValue(RegistrySettingsPath, "SetPassword", 0, Microsoft.Win32.RegistryValueKind.String)

                End If

            End If
        Else
            If FindConString(DBPath, "", False) = False Then Exit Sub
        End If

        If Me.ProfileTableAdapter.Connection.State = ConnectionState.Open Then Me.ProfileTableAdapter.Connection.Close()
        Me.ProfileTableAdapter.Connection.ConnectionString = ConString
        Me.ProfileTableAdapter.Connection.Open()

        If Me.AutoTextAdapter.Connection.State = ConnectionState.Open Then Me.AutoTextAdapter.Connection.Close()
        Me.AutoTextAdapter.Connection.ConnectionString = ConString
        Me.AutoTextAdapter.Connection.Open()

        If Me.ProfileTableMergeAdapter.Connection.State = ConnectionState.Open Then Me.ProfileTableMergeAdapter.Connection.Close()
        Me.ProfileTableMergeAdapter.Connection.ConnectionString = ConString
        Me.ProfileTableMergeAdapter.Connection.Open()

        If Me.TempProfileTableAdapter.Connection.State = ConnectionState.Open Then Me.TempProfileTableAdapter.Connection.Close()
        Me.TempProfileTableAdapter.Connection.ConnectionString = ConString
        Me.TempProfileTableAdapter.Connection.Open()

    End Sub

    Private Function FindConString(ByVal DB As String, ByVal TestPassWord As String, ByVal HasPassword As Boolean) As Boolean
        FindConString = False
        ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DB
        Dim cs = ConString
        Try
            If ProfileTableMergeAdapter.Connection.State = Data.ConnectionState.Open Then ProfileTableMergeAdapter.Connection.Close()
            If HasPassword Then
                cs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DB & "; Jet OLEDB:Database Password=" & TestPassWord
                ProfileTableMergeAdapter.Connection.ConnectionString = cs
            Else
                cs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DB
                ProfileTableMergeAdapter.Connection.ConnectionString = cs
            End If

            ProfileTableMergeAdapter.Connection.Open()
            ConString = cs
            FindConString = True
        Catch ex As Exception
            ' MessageBoxEx.Show(ex.Message, AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            FindConString = False
        End Try
    End Function


    Private Sub CloseConnectionToDatabse()
        On Error Resume Next
        If Me.ProfileTableAdapter.Connection.State = ConnectionState.Open Then Me.ProfileTableAdapter.Connection.Close()

        If Me.AutoTextAdapter.Connection.State = ConnectionState.Open Then Me.AutoTextAdapter.Connection.Close()

        If Me.ProfileTableMergeAdapter.Connection.State = ConnectionState.Open Then Me.ProfileTableMergeAdapter.Connection.Close()

        If Me.TempProfileTableAdapter.Connection.State = ConnectionState.Open Then Me.TempProfileTableAdapter.Connection.Close()

        Me.ProfileTableAdapter.Dispose()
        Me.AutoTextAdapter.Dispose()
        Me.ProfileTableMergeAdapter.Dispose()
        Me.TempProfileTableAdapter.Dispose()
    End Sub
#End Region

#Region "LOAD DATA"
    Sub LoadDataToTable() 'loads data to the datagrid
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        If FPBDataSet.HasErrors Then FPBDataSet.RejectChanges()
        Me.ProfileTableAdapter.Fill(Me.FPBDataSet.profile)
        Me.ProfileBindingSource.MoveLast()
        DisplayRecordCount()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ReloadData() Handles btnReloadData.Click
        On Error Resume Next
        LoadDataToTable()
        ShowAlertMessage("Records reloaded. Found " & Me.DataGridView.RowCount & " Records")
    End Sub
#End Region


#Region "SLIP IMAGE GENERAL SETTINGS"

    Private Sub CheckDeviceConnectedStatus(ByVal EventID As String, ByVal DeviceID As String, ByVal ItemID As String) Handles devmanager.OnEvent
        On Error Resume Next

        Dim i As Integer
        If EventID = WIA.EventID.wiaEventDeviceConnected Then
            If Me.devmanager.DeviceInfos.Count = 1 Then
                If Me.devmanager.DeviceInfos.Item(1).Type = WIA.WiaDeviceType.CameraDeviceType Then
                    ShowAlertMessage("Compatible Camera Connected!")
                End If

                If Me.devmanager.DeviceInfos.Item(1).Type = WIA.WiaDeviceType.ScannerDeviceType Then
                    ShowAlertMessage("Compatible Scanner Connected!")
                End If

            ElseIf Me.devmanager.DeviceInfos.Count > 1 Then
                For i = 1 To Me.devmanager.DeviceInfos.Count
                    If Me.devmanager.DeviceInfos.Item(i).Type = WIA.WiaDeviceType.CameraDeviceType Or WIA.WiaDeviceType.ScannerDeviceType Then ShowAlertMessage("Compatible Device Connected!")
                    Exit For
                Next
            End If

        End If
    End Sub


    Private Function ImportImageFromScannerOrCamera(ByVal SaveLocation As String, Optional ByVal FileName As String = vbNullString) As String
        On Error GoTo errhandler
        If Me.devmanager.DeviceInfos.Count = 0 Then
            MessageBoxEx.Show("No compatible Scanner or Camera Device detected. Please connect one!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return vbNullString
            Exit Function
        End If


        If My.Computer.FileSystem.FileExists(SaveLocation & FileName & ".jpeg") Then
            Dim msg As String = vbNullString

            msg = "The FP Slip image " & FileName & ".jpeg" & " already exists in the FP Slip Image Location. Do you want to replace it?"
            Dim reply As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show(msg, AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

            If reply = Windows.Forms.DialogResult.No Then
                Return vbNullString
                Exit Function
            End If
        End If

        Dim dev As WIA.Device
        Dim dg As New WIA.CommonDialog
        Dim SelectedItems As WIA.Items
        Dim img As WIA.ImageFile
        Dim itm As Object

        dev = dg.ShowSelectDevice(WIA.WiaDeviceType.UnspecifiedDeviceType, False, True) 'show select device message
        SelectedItems = dg.ShowSelectItems(dev, WIA.WiaImageIntent.UnspecifiedIntent, WIA.WiaImageBias.MaximizeQuality, True, True, True) 'show the pictures in the device selected

        itm = SelectedItems(1)
        If FileName = vbNullString Then FileName = itm.Properties("Item Name").Value 'use the original name
        img = dg.ShowTransfer(itm, , True) 'transfer the picture to imgfile
        Dim saved As Boolean
        saved = SaveImage(img, SaveLocation, FileName)

        If saved = False Then
            Return vbNullString
        Else
            Return SaveLocation & FileName & ".jpeg" 'return the Photo
        End If

        Exit Function
errhandler:
        If Err.Number = -2145320939 Then
            MessageBoxEx.Show("No compatible Scanner or Camera Device detected. Please connect one!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        ElseIf Err.Number = -2145320860 Then
            ' ShowAlertMessage(Err.Description)
        End If
    End Function


    Private Function SaveImage(ByVal img As WIA.ImageFile, ByVal SaveLocation As String, ByVal FileName As String) As Boolean

        On Error Resume Next
        If My.Computer.FileSystem.DirectoryExists(SaveLocation) = False Then 'if destination directory not exists
            My.Computer.FileSystem.CreateDirectory(SaveLocation) 'then create one!
        End If

        If Strings.Right(SaveLocation, 1) <> "\" Then SaveLocation = SaveLocation & "\"
        If Not img Is Nothing Then

            Dim tempfile As String = SaveLocation & "temp" & "." & img.FileExtension.ToLower
            If My.Computer.FileSystem.FileExists(tempfile) Then
                FileIO.FileSystem.DeleteFile(tempfile)
            End If
            img.SaveFile(tempfile)

            Dim x As Bitmap = New Bitmap(tempfile)
            x.SetResolution(Int(x.HorizontalResolution), Int(x.VerticalResolution))
            ' FileIO.FileSystem.DeleteFile(SaveLocation & FileName & ".jpeg")
            x.Save(SaveLocation & FileName & ".jpeg", ImageFormat.Jpeg)
            x.Dispose()
            FileIO.FileSystem.DeleteFile(tempfile)
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub SetPhotoImportLocation() Handles btnChangeFPSlipImageLocation.Click
        On Error Resume Next
        GetFPImageImportLocation()
        Me.FolderBrowserDialog1.ShowNewFolderButton = True
        Me.FolderBrowserDialog1.Description = "Select location to save scanned FP Slips"
        Me.FolderBrowserDialog1.SelectedPath = FPSlipImageImportLocation
        Dim result As DialogResult = FolderBrowserDialog1.ShowDialog()
        If (result = DialogResult.OK) Then
            FPSlipImageImportLocation = Me.FolderBrowserDialog1.SelectedPath
            My.Computer.Registry.SetValue(RegistrySettingsPath, "FPImageImportLocation", FPSlipImageImportLocation, Microsoft.Win32.RegistryValueKind.String)
            GetFPImageImportLocation()
            ShowAlertMessage("Scanned FP Slips location changed!")
        End If
    End Sub


    Private Sub GetFPImageImportLocation()
        On Error Resume Next
        Dim defaultlocation As String = FileIO.SpecialDirectories.MyDocuments & "\BXSofts\" & AppName & "\Scanned FP Slips"
        FPSlipImageImportLocation = My.Computer.Registry.GetValue(RegistrySettingsPath, "FPImageImportLocation", defaultlocation)

    End Sub


    Private Sub ExplorePhotoLocation() Handles btnExploreFPImportLocation.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        If FileIO.FileSystem.DirectoryExists(FPSlipImageImportLocation) Then
            Call Shell("explorer.exe " & FPSlipImageImportLocation, AppWinStyle.NormalFocus)
        Else
            FileIO.FileSystem.CreateDirectory(FPSlipImageImportLocation)
            Call Shell("explorer.exe " & FPSlipImageImportLocation, AppWinStyle.NormalFocus)
        End If
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub LocateFPSlip() Handles btnLocateFPSlip.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        Dim location As String = vbNullString
        location = Me.DataGridView.SelectedCells(16).Value.ToString
        If location <> vbNullString Then
            If FileIO.FileSystem.FileExists(location) Then
                Call Shell("explorer.exe /select," & location, AppWinStyle.NormalFocus)
            Else
                MessageBoxEx.Show("The specified FP Slip Image file does not exist!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
        Else
            MessageBoxEx.Show("No image to locate!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        Me.Cursor = Cursors.Default
    End Sub

#End Region


#Region "FP SLIP IMAGE FILE SETTINGS"


    Private Sub ViewImageOnDatagridCellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView.CellDoubleClick
        On Error Resume Next
        If e.RowIndex < 0 Or e.RowIndex > Me.DataGridView.Rows.Count - 1 Then
            Exit Sub
        End If
        ViewImageOnDatagridViewDblClick()
    End Sub


    Public Sub SelectFPSlipImage() Handles btnSelectFPSlip.Click, btnSelectDisplayContext.Click 'select a photo from system
        On Error GoTo errhandler

        OpenFileDialog1.Filter = "Picture Files(JPG, JPEG, BMP, TIF, GIF, PNG)|*.jpg;*.jpeg;*.bmp;*.tif;*.gif;*.png"
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Title = "Select FP Slip Image File"
        OpenFileDialog1.AutoUpgradeEnabled = True
        OpenFileDialog1.RestoreDirectory = True 'remember last directory
        Dim SelectedFile As String
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then 'if ok button clicked
            Application.DoEvents() 'first close the selection window
            SelectedFile = OpenFileDialog1.FileName

            Dim getInfo As System.IO.DriveInfo = My.Computer.FileSystem.GetDriveInfo(SelectedFile)
            If getInfo.DriveType <> IO.DriveType.Fixed Then

                Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("The FP Slip Image File you selected is on a removable media. Do you want to copy it to the FP Slip Image Files Location?", AppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If r = Windows.Forms.DialogResult.Yes Then
                    Dim DestinationFile As String = FPSlipImageImportLocation & "\" & OpenFileDialog1.SafeFileName
                    My.Computer.FileSystem.CopyFile(SelectedFile, DestinationFile, FileIO.UIOption.AllDialogs, FileIO.UICancelOption.ThrowException) 'shows replace option
                    SelectedFile = DestinationFile
                End If


                If r = Windows.Forms.DialogResult.Cancel Then Exit Sub
            End If

            FPSlipImageFile = SelectedFile
            Me.picFPSlip.Image = New Bitmap(SelectedFile) 'display the pic
        End If
        Exit Sub
errhandler:
    End Sub


    Private Sub ScanFPSlip() Handles btnScanFPSlip.Click, btnScanDisplayContext.Click 'import photos from camera and scanner
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        If Trim(Me.txtTIN.Text) = vbNullString Then

            DevComponents.DotNetBar.MessageBoxEx.Show("Please enter the TIN Number which is used as the scanned image's File Name", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtTIN.Focus()
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        Dim FileName As String = "FPNo." & Trim(Me.txtTIN.Text)

        FileName = Strings.Replace(FileName, "/", "-")
        If Strings.Right(FPSlipImageImportLocation, 1) <> "\" Then FPSlipImageImportLocation = FPSlipImageImportLocation & "\"
        Dim ScannedImage As String = ImportImageFromScannerOrCamera(FPSlipImageImportLocation, FileName) 'scans the picture and returns the file name with path

        If ScannedImage <> vbNullString Then
            FPSlipImageFile = ScannedImage
            Me.picFPSlip.Image = New Bitmap(ScannedImage)
        End If
        Me.Cursor = Cursors.Default
    End Sub



    Private Sub ExploreFPSlip() Handles btnLocateDisplayContext.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        If FPSlipImageFile <> vbNullString Then
            If FileIO.FileSystem.FileExists(FPSlipImageFile) Then

                Call Shell("explorer.exe /select," & FPSlipImageFile, AppWinStyle.NormalFocus)
            Else
                MessageBoxEx.Show("The specified FP Slip Image file does not exist!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
        Else
            MessageBoxEx.Show("No image to show!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub ClearFPImage()
        On Error Resume Next
        Me.picFPSlip.ClearImage()
        FPSlipImageFile = vbNullString
    End Sub


    Private Sub PreviewFPSlip()
        On Error Resume Next
        If FileIO.FileSystem.FileExists(FPSlipImageFile) = True Then
            Me.picFPSlip.Image = New Bitmap(FPSlipImageFile)
        Else
            Me.picFPSlip.Image = My.Resources.NoDAImage
        End If
    End Sub

    Private Sub ClearFPImageWithMessage() Handles btnClearFPSlip.Click, btnClearDisplayContext.Click
        On Error Resume Next
        ClearFPImage()
        ShowAlertMessage("Image cleared!")
    End Sub

    Private Sub ViewImageOnDatagridViewDblClick() Handles btnViewDASlip.Click, btnViewFPSlipDatagridContext.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        If Me.DataGridView.RowCount = 0 Then
            ShowAlertMessage("No data in the list to show image!")
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        If Me.DataGridView.SelectedCells(16).Value.ToString = "" Then
            MessageBoxEx.Show("No image file is associated with the selected TIN Number!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        If FileIO.FileSystem.FileExists(Me.DataGridView.SelectedCells(16).Value.ToString) Then
            frmFPSlipImageDisplayer.Show()
            frmFPSlipImageDisplayer.WindowState = FormWindowState.Maximized
            frmFPSlipImageDisplayer.BringToFront()
            frmFPSlipImageDisplayer.LoadPicture(Me.DataGridView.SelectedCells(16).Value.ToString(), Me.DataGridView.SelectedCells(0).Value.ToString() & "  -   " & Me.DataGridView.CurrentRow.Cells(6).Value.ToString())
        Else
            MessageBoxEx.Show("The specified FP Slip Image file does not exist!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        Me.Cursor = Cursors.Default
    End Sub


    Sub ViewImageOnFPDisplayDblClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picFPSlip.MouseDoubleClick
        On Error Resume Next
        If e.Button <> Windows.Forms.MouseButtons.Left Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        If FPSlipImageFile <> vbNullString Then
            If FileIO.FileSystem.FileExists(FPSlipImageFile) Then
                frmFPSlipImageDisplayer.Show()
                frmFPSlipImageDisplayer.WindowState = FormWindowState.Maximized
                frmFPSlipImageDisplayer.BringToFront()
                frmFPSlipImageDisplayer.LoadPictureFromViewer(FPSlipImageFile, Me.txtTIN.Text)
            Else
                MessageBoxEx.Show("The specified FP Slip Image file does not exist!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
        Else
            MessageBoxEx.Show("No image to show!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Sub ViewFPImage() Handles btnViewFPSlipDisplayContext.Click
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        If FPSlipImageFile <> vbNullString Then
            If FileIO.FileSystem.FileExists(FPSlipImageFile) Then
                frmFPSlipImageDisplayer.Show()
                frmFPSlipImageDisplayer.WindowState = FormWindowState.Maximized
                frmFPSlipImageDisplayer.BringToFront()
                frmFPSlipImageDisplayer.LoadPictureFromViewer(FPSlipImageFile, Me.DataGridView.SelectedCells(0).Value.ToString)
            Else
                MessageBoxEx.Show("The specified FP Slip Image file does not exist!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
        Else
            MessageBoxEx.Show("No image to show!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub FPSlipContextMenuBarPopupOpen(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.PopupOpenEventArgs) Handles btnPicBoxContextMenuBar.PopupOpen
        On Error Resume Next
        If FPSlipImageFile = vbNullString Then
            Me.btnViewFPSlipDisplayContext.Enabled = False
            Me.btnLocateDisplayContext.Enabled = False
        Else
            Me.btnViewFPSlipDisplayContext.Enabled = True
            Me.btnLocateDisplayContext.Enabled = True
        End If
    End Sub


    Private Sub ImportFPSlipFromContext() Handles btnScanFPSlipContextMenu.Click
        On Error Resume Next

        Dim currentfilename = Me.DataGridView.SelectedCells(16).Value.ToString

        If FileIO.FileSystem.FileExists(currentfilename) Then
            Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("There is already an image file associated with the selected record. Do you want to replace it?", AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If r = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        Me.Cursor = Cursors.WaitCursor

        Dim TIN As String = Me.DataGridView.SelectedCells(0).Value.ToString()
        Dim FileName As String = "FPNo." & TIN
        FileName = Strings.Replace(FileName, "/", "-")
        If Strings.Right(FPSlipImageImportLocation, 1) <> "\" Then FPSlipImageImportLocation = FPSlipImageImportLocation & "\"
        Dim ScannedImage As String = ImportImageFromScannerOrCamera(FPSlipImageImportLocation, FileName) 'scans the picture and returns the file name with path

        If ScannedImage <> vbNullString Then
            FPSlipImageFile = ScannedImage

            Dim oldRow As FPBDataSet.profileRow 'add a new row to insert values
            oldRow = Me.FPBDataSet.profile.FindByTIN(TIN)
            If oldRow IsNot Nothing Then
                With oldRow
                    .SlipFile = FPSlipImageFile
                End With
            End If

            Me.ProfileTableAdapter.ImportSlipFileFromContext(FPSlipImageFile, TIN)

            ShowAlertMessage("Imported one Image")
            FPSlipImageFile = ""
        End If
        Me.Cursor = Cursors.Default
    End Sub





    Private Sub SelectFPSlipFromContext() Handles btnSelectFPSlipContextMenu.Click
        On Error GoTo errhandler

        Dim currentfilename = Me.DataGridView.SelectedCells(16).Value.ToString

        If FileIO.FileSystem.FileExists(currentfilename) Then
            Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("There is already an image file associated with the selected record. Do you want to replace it?", AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If r = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If


        OpenFileDialog1.InitialDirectory = FPSlipImageImportLocation

        OpenFileDialog1.Filter = "Picture Files(JPG, JPEG, BMP, TIF, GIF, PNG)|*.jpg;*.jpeg;*.bmp;*.tif;*.gif;*.png;*.tiff"
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Title = "Select FP Slip Image File"
        OpenFileDialog1.AutoUpgradeEnabled = True
        OpenFileDialog1.RestoreDirectory = True 'remember last directory
        Dim SelectedFile As String
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then 'if ok button clicked
            Application.DoEvents() 'first close the selection window
            SelectedFile = OpenFileDialog1.FileName
            Dim getInfo As System.IO.DriveInfo = My.Computer.FileSystem.GetDriveInfo(SelectedFile)

            Dim TIN As String = Me.DataGridView.SelectedCells(0).Value.ToString()
            If getInfo.DriveType <> IO.DriveType.Fixed Then
                Dim r As DialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("The FP Slip Image File you selected is on a removable media. Do you want to copy it to the FP Slip Image Files Location?", AppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If r = Windows.Forms.DialogResult.Yes Then
                    Dim DestinationFile As String = FPSlipImageImportLocation & "\" & OpenFileDialog1.SafeFileName
                    My.Computer.FileSystem.CopyFile(SelectedFile, DestinationFile, FileIO.UIOption.AllDialogs, FileIO.UICancelOption.ThrowException) 'shows replace option
                    SelectedFile = DestinationFile
                End If
                If r = Windows.Forms.DialogResult.Cancel Then Exit Sub
            End If

            FPSlipImageFile = SelectedFile

            Dim oldRow As FPBDataSet.profileRow 'add a new row to insert values
            oldRow = Me.FPBDataSet.profile.FindByTIN(TIN)
            If oldRow IsNot Nothing Then
                With oldRow
                    .SlipFile = FPSlipImageFile
                End With
            End If

            Me.ProfileTableAdapter.ImportSlipFileFromContext(FPSlipImageFile, TIN)

            ShowAlertMessage("FP Slip Image file updated")
            FPSlipImageFile = ""
        End If


        Exit Sub
errhandler:

    End Sub


#End Region




#Region "SORT"
    Private Sub SetDatagridSortMode()
        On Error Resume Next
        For i = 0 To Me.DataGridView.Columns.Count - 1
            Me.DataGridView.Columns(i).SortMode = DataGridViewColumnSortMode.Programmatic
        Next

    End Sub



    Private Sub SortColumns(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView.ColumnHeaderMouseClick
        On Error Resume Next
        If e.Button <> Windows.Forms.MouseButtons.Left Then
            ColumnHeaderClicked = True
            Exit Sub
        End If
        Dim c = e.ColumnIndex



        If DataGridView.SortOrder = SortOrder.None Or DataGridView.SortOrder = SortOrder.Descending Then
            Me.Cursor = Cursors.WaitCursor
            Me.ProfileBindingSource.Sort = DataGridView.Columns(c).DataPropertyName.ToString() & " ASC"
            Me.Cursor = Cursors.Default
            ShowAlertMessage("Table sorted with " & DataGridView.Columns(c).HeaderText & " in Ascending order!")
            Exit Sub
        End If

        If DataGridView.SortOrder = SortOrder.Ascending Then
            Me.Cursor = Cursors.WaitCursor
            Me.ProfileBindingSource.Sort = DataGridView.Columns(c).DataPropertyName.ToString() & " DESC"
            Me.Cursor = Cursors.Default
            ShowAlertMessage("Table sorted with " & DataGridView.Columns(c).HeaderText & " in Descending order!")
            Exit Sub
        End If




    End Sub
#End Region



#Region " ALERT MESSAGES"



    Private Sub ShowPopups() Handles chkShowPopups.Click
        On Error Resume Next
        Dim s As Boolean = chkShowPopups.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0

        My.Computer.Registry.SetValue(RegistrySettingsPath, "ShowPopups", v, Microsoft.Win32.RegistryValueKind.String)
    End Sub



    Private Sub PlaySoundOnPopups() Handles chkPlaySound.Click
        On Error Resume Next
        Dim s As Boolean = chkPlaySound.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0

        My.Computer.Registry.SetValue(RegistrySettingsPath, "PlaySound", v, Microsoft.Win32.RegistryValueKind.String)
    End Sub




    Public Sub ShowAlertMessage(ByVal msg As String)
        On Error Resume Next
        If Me.chkShowPopups.Checked Then
            If msg.EndsWith("!") = False And msg.EndsWith(".") = False Then
                msg += "."
            End If
            BalloonMessage = New AlertCustom()
            Dim r As System.Drawing.Rectangle = Screen.GetWorkingArea(Me)
            BalloonMessage.Location = New System.Drawing.Point(r.Right - BalloonMessage.Width, r.Bottom - BalloonMessage.Height)
            BalloonMessage.AutoClose = True
            BalloonMessage.AutoCloseTimeOut = 5
            BalloonMessage.Text = msg
            BalloonMessage.AlertAnimation = eAlertAnimation.BottomToTop
            BalloonMessage.Style = eBallonStyle.Alert
            BalloonMessage.BackColor = Me.BackColor
            BalloonMessage.BackColor2 = Me.BackColor
            BalloonMessage.ForeColor = Me.ForeColor
            BalloonMessage.AlertAnimationDuration = 200
            BalloonMessage.Show(False)
            If Me.chkPlaySound.Checked Then System.Media.SystemSounds.Asterisk.Play()
        End If

    End Sub

#End Region



#Region "DATA ENTRY FIELDS SETTINGS"


    Sub InitializeFields() Handles btnClear.Click
        On Error Resume Next
        Me.txtTIN.Focus()
        Me.dtLastTrace.Text = ""
        Me.rdoBothSex.Checked = True
        Dim ctrl As Control
        For Each ctrl In Me.ExpandablePanel1.Controls
            If TypeOf (ctrl) Is TextBox Or TypeOf (ctrl) Is DevComponents.DotNetBar.Controls.MaskedTextBoxAdv Then
                ctrl.Text = ""
            End If
        Next
        Me.rdoBothOV.Checked = True
        FPSlipImageFile = vbNullString
        ClearFPImage()
    End Sub


    Private Sub ClearSelectedFields(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTIN.ButtonCustomClick, txtRCN.ButtonCustomClick, txtDCKD.ButtonCustomClick, txtName.ButtonCustomClick, txtAlias1.ButtonCustomClick, txtAlias2.ButtonCustomClick, txtFathersAlias.ButtonCustomClick, txtFathersName.ButtonCustomClick, txtBirthYear.ButtonCustomClick, txtHenryClass.ButtonCustomClick
        On Error Resume Next
        Dim X As Control = DirectCast(sender, Control)
        X.Text = vbNullString

    End Sub


    Private Sub SelectSex(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles rdoMale.KeyDown, rdoFemale.KeyDown, rdoBothSex.KeyDown
        On Error Resume Next
        If e.KeyCode = Keys.M Then
            Me.rdoMale.Checked = True
        End If
        If e.KeyCode = Keys.F Then
            Me.rdoFemale.Checked = True
        End If
        If e.KeyCode = Keys.U Then
            Me.rdoBothSex.Checked = True
        End If
    End Sub



    Private Sub SelectOV(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles rdoOV.KeyDown, rdoNotOV.KeyDown, rdoBothOV.KeyDown
        On Error Resume Next
        If e.KeyCode = Keys.O Then
            Me.rdoOV.Checked = True
        End If
        If e.KeyCode = Keys.N Then
            Me.rdoNotOV.Checked = True
        End If
        If e.KeyCode = Keys.U Then
            Me.rdoBothOV.Checked = True
        End If
    End Sub

    Private Sub CapitalizeHenryClassification() Handles txtHenryClass.Validated
        On Error Resume Next


        Dim t As String = Me.txtHenryClass.Text
        t = t.Replace("i", "I")
        t = t.Replace("m", "M")
        t = t.Replace("o", "O")
        t = t.Replace("w", "W")
        t = t.Replace("u", "U")
        t = t.Replace("d", "D")
        t = t.Replace("b", "B")
        t = t.Replace("l", "L")
        t = t.Replace("h", "H")
        Me.txtHenryClass.Text = t

    End Sub
#End Region



#Region "PROPER CASE"


    Private Function ConvertToProperCase(ByVal InputText) As String
        On Error Resume Next

        Return Strings.StrConv(InputText, VbStrConv.ProperCase)
    End Function


    Sub ConvertToProperCase(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.Leave, txtAlias1.Leave, txtAlias2.Leave, txtFathersAlias.Leave, txtFathersName.Leave, txtAddress1.Leave, txtAddress2.Leave
        On Error Resume Next

        Dim X As Control = DirectCast(sender, Control)
        X.Text = ConvertToProperCase(X.Text)

    End Sub

#End Region



#Region "MANDATORY FIELDS"



    Function MandatoryFieldsNotFilled() As Boolean
        On Error Resume Next
        If Trim(Me.txtTIN.Text) = vbNullString Or Trim(Me.txtRCN.Text) = vbNullString Or Trim(txtName.Text) = vbNullString Or Trim(txtHenryClass.Text) = vbNullString Then ' Or Me.dtLastTrace.IsEmpty Then
            Return True
        Else
            Return False
        End If
    End Function

    Sub ShowMandatoryFieldsMessage()
        On Error Resume Next
        Dim msg1 As String = "Please fill the following mandatory field(s):" & vbNewLine & vbNewLine
        Dim msg As String = ""
        If Trim(Me.txtTIN.Text) = vbNullString Then
            msg = msg & " TIN" & vbNewLine
        End If
        If Trim(Me.txtRCN.Text) = vbNullString Then
            msg = msg & " RCN" & vbNewLine
        End If
        If Trim(txtName.Text) = vbNullString Then
            msg = msg & " Name" & vbNewLine
        End If
        If Trim(txtHenryClass.Text) = vbNullString Then
            msg = msg & " Henry Classification" & vbNewLine
        End If
        msg1 = msg1 & msg
        DevComponents.DotNetBar.MessageBoxEx.Show(msg1, AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Dim x As String = Trim(Strings.Left(msg, 2))

        If x = "T" Then txtTIN.Focus()
        If x = "R" Then txtRCN.Focus()
        If x = "N" Then txtName.Focus()
        If x = "H" Then txtHenryClass.Focus()

    End Sub

#End Region



#Region "RECENT RECORDS LIST"

    Private Sub SaveRecentRecordsToRegistry(ByVal RecentRecordsName As String)
        On Error Resume Next

        For i As Short = 1 To 9
            If i > RecentRecordsList.Count Then Exit For
            If LCase(RecentRecordsList(i)) = LCase(RecentRecordsName) Then
                RecentRecordsList.Remove(i)
                Exit For
            End If
        Next i

        If RecentRecordsList.Count > 0 Then
            RecentRecordsList.Add(RecentRecordsName, , 1)
        Else
            RecentRecordsList.Add(RecentRecordsName)
        End If

        If RecentRecordsList.Count > 9 Then 'If there are more items than 9 remove the last one
            RecentRecordsList.Remove(10)
        End If

        For i = 1 To 9
            If i > RecentRecordsList.Count Then
                RecentRecordsName = ""
            Else
                RecentRecordsName = RecentRecordsList(i) 'add it
            End If
            ' Add file to the registry
            My.Computer.Registry.SetValue(RegistrySettingsPath, "RecentRecordsList" & i, RecentRecordsName, Microsoft.Win32.RegistryValueKind.String)
        Next
        LoadRecentRecordsList()
    End Sub


    Private Sub LoadRecentRecordsList()
        On Error Resume Next
        RecentRecordsList.Clear()
        For i As Short = 1 To 9
            Dim r As String = My.Computer.Registry.GetValue(RegistrySettingsPath, "RecentRecordsList" & i, "")
            If r <> "" Then
                RecentRecordsList.Add(r)
            End If
        Next

        btnRecentRecords1.Text = "1. " & FileIO.FileSystem.GetName(My.Computer.Registry.GetValue(RegistrySettingsPath, "RecentRecordsList1", ""))
        btnRecentRecords2.Text = "2. " & FileIO.FileSystem.GetName(My.Computer.Registry.GetValue(RegistrySettingsPath, "RecentRecordsList2", ""))
        btnRecentRecords3.Text = "3. " & FileIO.FileSystem.GetName(My.Computer.Registry.GetValue(RegistrySettingsPath, "RecentRecordsList3", ""))
        btnRecentRecords4.Text = "4. " & FileIO.FileSystem.GetName(My.Computer.Registry.GetValue(RegistrySettingsPath, "RecentRecordsList4", ""))
        btnRecentRecords5.Text = "5. " & FileIO.FileSystem.GetName(My.Computer.Registry.GetValue(RegistrySettingsPath, "RecentRecordsList5", ""))
        btnRecentRecords6.Text = "6. " & FileIO.FileSystem.GetName(My.Computer.Registry.GetValue(RegistrySettingsPath, "RecentRecordsList6", ""))
        btnRecentRecords7.Text = "7. " & FileIO.FileSystem.GetName(My.Computer.Registry.GetValue(RegistrySettingsPath, "RecentRecordsList7", ""))
        btnRecentRecords8.Text = "8. " & FileIO.FileSystem.GetName(My.Computer.Registry.GetValue(RegistrySettingsPath, "RecentRecordsList8", ""))
        btnRecentRecords9.Text = "9. " & FileIO.FileSystem.GetName(My.Computer.Registry.GetValue(RegistrySettingsPath, "RecentRecordsList9", ""))
        SetRecentRecordsListVisibility()

    End Sub


    Sub SetRecentRecordsListVisibility()
        On Error Resume Next

        If Me.btnRecentRecords1.Text = "1. " Then Me.btnRecentRecords1.Visible = False
        If Me.btnRecentRecords2.Text = "2. " Then Me.btnRecentRecords2.Visible = False
        If Me.btnRecentRecords3.Text = "3. " Then Me.btnRecentRecords3.Visible = False
        If Me.btnRecentRecords4.Text = "4. " Then Me.btnRecentRecords4.Visible = False
        If Me.btnRecentRecords5.Text = "5. " Then Me.btnRecentRecords5.Visible = False
        If Me.btnRecentRecords6.Text = "6. " Then Me.btnRecentRecords6.Visible = False
        If Me.btnRecentRecords7.Text = "7. " Then Me.btnRecentRecords7.Visible = False
        If Me.btnRecentRecords8.Text = "8. " Then Me.btnRecentRecords8.Visible = False
        If Me.btnRecentRecords9.Text = "9. " Then Me.btnRecentRecords9.Visible = False

        If Me.btnRecentRecords1.Text <> "1. " Then Me.btnRecentRecords1.Visible = True
        If Me.btnRecentRecords2.Text <> "2. " Then Me.btnRecentRecords2.Visible = True
        If Me.btnRecentRecords3.Text <> "3. " Then Me.btnRecentRecords3.Visible = True
        If Me.btnRecentRecords4.Text <> "4. " Then Me.btnRecentRecords4.Visible = True
        If Me.btnRecentRecords5.Text <> "5. " Then Me.btnRecentRecords5.Visible = True
        If Me.btnRecentRecords6.Text <> "6. " Then Me.btnRecentRecords6.Visible = True
        If Me.btnRecentRecords7.Text <> "7. " Then Me.btnRecentRecords7.Visible = True
        If Me.btnRecentRecords8.Text <> "8. " Then Me.btnRecentRecords8.Visible = True
        If Me.btnRecentRecords9.Text <> "9. " Then Me.btnRecentRecords9.Visible = True

    End Sub


    Sub ClearRecentList() Handles btnClearRecentRecordsList.Click
        On Error Resume Next
        For i As Short = 1 To 9
            My.Computer.Registry.SetValue(RegistrySettingsPath, "RecentRecordsList" & i, "", Microsoft.Win32.RegistryValueKind.String)
        Next
        RecentRecordsList.Clear()
        LoadRecentRecordsList()
        ShowAlertMessage("Recent Records List cleared!")
    End Sub


    Sub OpenRecentRecords(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRecentRecords1.Click, btnRecentRecords2.Click, btnRecentRecords3.Click, btnRecentRecords4.Click, btnRecentRecords5.Click, btnRecentRecords6.Click, btnRecentRecords7.Click, btnRecentRecords8.Click, btnRecentRecords9.Click
        On Error GoTo errhandler
        Dim RecentTIN As String = ""
        Select Case DirectCast(sender, ButtonItem).Name
            Case btnRecentRecords1.Name
                RecentTIN = My.Computer.Registry.GetValue(RegistrySettingsPath, "RecentRecordsList1", "")
            Case btnRecentRecords2.Name
                RecentTIN = My.Computer.Registry.GetValue(RegistrySettingsPath, "RecentRecordsList2", "")
            Case btnRecentRecords3.Name
                RecentTIN = My.Computer.Registry.GetValue(RegistrySettingsPath, "RecentRecordsList3", "")
            Case btnRecentRecords4.Name
                RecentTIN = My.Computer.Registry.GetValue(RegistrySettingsPath, "RecentRecordsList4", "")
            Case btnRecentRecords5.Name
                RecentTIN = My.Computer.Registry.GetValue(RegistrySettingsPath, "RecentRecordsList5", "")
            Case btnRecentRecords6.Name
                RecentTIN = My.Computer.Registry.GetValue(RegistrySettingsPath, "RecentRecordsList6", "")
            Case btnRecentRecords7.Name
                RecentTIN = My.Computer.Registry.GetValue(RegistrySettingsPath, "RecentRecordsList7", "")
            Case btnRecentRecords8.Name
                RecentTIN = My.Computer.Registry.GetValue(RegistrySettingsPath, "RecentRecordsList8", "")
            Case btnRecentRecords9.Name
                RecentTIN = My.Computer.Registry.GetValue(RegistrySettingsPath, "RecentRecordsList9", "")

        End Select

        If CheckTINExists(RecentTIN) Then
            Me.ProfileBindingSource.Position = Me.ProfileBindingSource.Find("TIN", RecentTIN)
            Me.DataGridView.Rows(Me.ProfileBindingSource.Position).Selected = True
        Else

            DevComponents.DotNetBar.MessageBoxEx.Show("The selected record does not exist!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
        Exit Sub
errhandler:
        DevComponents.DotNetBar.MessageBoxEx.Show(Err.Description, AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub
#End Region



#Region "SAVE BUTTON ACTION"
    Private Sub SaveData(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click, btnSaveMenu.Click
        On Error Resume Next
        CapitalizeHenryClassification()
        If EditMode Then
            UpdateData()
        Else
            SaveNewEntry()
        End If
    End Sub
#End Region


#Region "NEW DATA ENTRY"




    Private Sub NewEntryButtonAction(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewEntry.Click, btnNewMenu.Click
        On Error Resume Next
        InitializeFields()
        EditMode = False
        Me.btnSave.Text = "Save"
        Me.ExpandablePanel1.Expanded = True
    End Sub




    Sub SaveNewEntry()
        On Error GoTo errhandler
        AddToAutoFillWordsList()
        If MandatoryFieldsNotFilled() Then
            ShowMandatoryFieldsMessage()
            Exit Sub
        End If
        Dim tin As String = Trim(Me.txtTIN.Text)

        If rdoMale.Checked = True Then Sex = "Male"
        If rdoFemale.Checked = True Then Sex = "Female"
        If rdoBothSex.Checked = True Then Sex = vbNullString

        If rdoOV.Checked = True Then OVStatus = 1
        If rdoNotOV.Checked = True Then OVStatus = 0
        If rdoBothOV.Checked = True Then OVStatus = 2


        Dim ov As Boolean
        Select Case OVStatus
            Case 0 : ov = False
            Case 1 : ov = True
            Case 2 : ov = False
        End Select


        If CheckTINExists(tin) Then
            DevComponents.DotNetBar.MessageBoxEx.Show("The TIN you entered already exists. Please enter a different TIN", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtTIN.Focus()
            Exit Sub
        End If

        If CheckRCNExists(Trim(txtRCN.Text)) Then
            DevComponents.DotNetBar.MessageBoxEx.Show("The RCN you entered already exists. Please enter a different RCN", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Me.txtRCN.Focus()
            Exit Sub
        End If
        If FPBDataSet.HasErrors Then FPBDataSet.RejectChanges()
        Me.ProfileTableAdapter.Insert(tin, Trim(txtRCN.Text), Trim(txtName.Text), Trim(txtAlias1.Text), Trim(txtAlias2.Text), Trim(Sex), Trim(txtBirthYear.Text), Trim(txtAddress1.Text), Trim(txtAddress2.Text), Trim(txtDCKD.Text), Trim(txtFathersName.Text), Trim(txtFathersAlias.Text), Trim(txtHenryClass.Text), Trim(txtDescription.Text), dtLastTrace.ValueObject, ov, FPSlipImageFile)

        InsertUsingDataset()

        ShowAlertMessage("New data entered successfully!")
        SaveRecentRecordsToRegistry(tin)

        DisplayRecordCount()
        InitializeFields()
        Me.ProfileBindingSource.MoveLast()

        Exit Sub
errhandler:

        DevComponents.DotNetBar.MessageBoxEx.Show("The data you entered cannot be saved for the following reason:" & vbNewLine & vbNewLine & vbTab & Err.Description, AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        On Error Resume Next
        Me.ProfileTableAdapter.Dispose()
        If Me.FPBDataSet.HasErrors Then FPBDataSet.RejectChanges()
    End Sub



    Sub InsertUsingDataset()
        On Error Resume Next
        Dim ov As Boolean
        Select Case OVStatus
            Case 0 : ov = False
            Case 1 : ov = True
            Case 2 : ov = False
        End Select
        Dim newProfileRow As FPBDataSet.profileRow
        newProfileRow = Me.FPBDataSet.profile.NewprofileRow()
        With newProfileRow
            .TIN = Trim(txtTIN.Text)
            .RCN = Trim(txtRCN.Text)
            .Name = Trim(txtName.Text)
            .AliasN1 = Trim(txtAlias1.Text)
            .AliasN2 = Trim(txtAlias2.Text)
            .Sex = Trim(Sex)
            .BirthYear = Trim(txtBirthYear.Text)
            .Address1 = Trim(txtAddress1.Text)
            .Address2 = Trim(txtAddress2.Text)
            .PCN = Trim(txtDCKD.Text)
            .FathersName = Trim(txtFathersName.Text)
            .AliasFathers1 = Trim(txtFathersAlias.Text)
            .HenryClass = Trim(txtHenryClass.Text)
            .Description = Trim(txtDescription.Text)
            If Me.dtLastTrace.IsEmpty = False Then .LatestTraceDate = dtLastTrace.Value
            .OV = ov
            .SlipFile = FPSlipImageFile
        End With

        Me.FPBDataSet.profile.Rows.Add(newProfileRow)
        If Me.FPBDataSet.HasErrors Then FPBDataSet.RejectChanges()

    End Sub

#End Region



#Region "EDIT DATA"


    Private Sub EditButtonAction(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click, btnEditContextMenu.Click
        On Error Resume Next
        If Me.DataGridView.RowCount = 0 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("No data to edit!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            EditMode = False
            Exit Sub
        End If

        If Me.DataGridView.SelectedRows.Count = 0 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            EditMode = False
            Exit Sub
        End If


        EditMode = True
        Me.btnSave.Text = "Update"
        Me.ExpandablePanel1.Expanded = True
        ClearFPImage()
        With Me.DataGridView
            Me.txtTIN.Text = .SelectedCells(0).Value.ToString
            Me.txtRCN.Text = .SelectedCells(1).Value.ToString
            Me.txtDCKD.Text = .SelectedCells(2).Value.ToString
            Me.txtName.Text = .SelectedCells(3).Value.ToString
            Me.txtAlias1.Text = .SelectedCells(4).Value.ToString
            Me.txtAlias2.Text = .SelectedCells(5).Value.ToString
            Me.txtFathersName.Text = .SelectedCells(6).Value.ToString
            Me.txtFathersAlias.Text = .SelectedCells(7).Value.ToString

            If .SelectedCells(8).Value.ToString = "Female" Then
                Me.rdoFemale.Checked = 1
            ElseIf .SelectedCells(8).Value.ToString = "Male" Then
                Me.rdoMale.Checked = 1
            Else
                Me.rdoBothSex.Checked = 1
            End If

            Me.txtBirthYear.Text = .SelectedCells(9).Value.ToString()
            Me.txtAddress1.Text = .SelectedCells(10).Value.ToString
            Me.txtAddress2.Text = .SelectedCells(11).Value.ToString
            Me.txtHenryClass.Text = .SelectedCells(12).Value.ToString
            Me.txtDescription.Text = .SelectedCells(13).Value.ToString
            Me.dtLastTrace.ValueObject = .SelectedCells(14).Value
            If .SelectedCells(15).Value Then

                Me.rdoOV.Checked = True
            Else
                Me.rdoNotOV.Checked = True
            End If
            FPSlipImageFile = .SelectedCells(16).Value.ToString
            PreviewFPSlip()
        End With
        OriginalTIN = Me.txtTIN.Text
        OriginalRCN = Me.txtRCN.Text
        Me.txtTIN.Focus()

    End Sub




    Sub UpdateData()
        On Error GoTo errhandler
        AddToAutoFillWordsList()

        If MandatoryFieldsNotFilled() Then
            ShowMandatoryFieldsMessage()
            Exit Sub
        End If
        Dim tin As String = Trim(Me.txtTIN.Text)

        If rdoMale.Checked = True Then Sex = "Male"
        If rdoFemale.Checked = True Then Sex = "Female"
        If rdoBothSex.Checked = True Then Sex = vbNullString

        If rdoOV.Checked = True Then OVStatus = 1
        If rdoNotOV.Checked = True Then OVStatus = 0
        If rdoBothOV.Checked = True Then OVStatus = 2


        Dim ov As Boolean
        Select Case OVStatus
            Case 0 : ov = False
            Case 1 : ov = True
            Case 2 : ov = False
        End Select
        If OriginalTIN <> tin Then 'if tin changed
            If CheckTINExists(tin) Then

                DevComponents.DotNetBar.MessageBoxEx.Show("The TIN you entered already exists. Please enter a different TIN", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtTIN.Focus()
                Exit Sub
            End If
        End If

        If OriginalRCN <> Trim(Me.txtRCN.Text) Then 'if rcn changed
            If CheckRCNExists(Trim(txtRCN.Text)) Then

                DevComponents.DotNetBar.MessageBoxEx.Show("The RCN you entered already exists. Please enter a different RCN", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtRCN.Focus()
                Exit Sub
            End If
        End If

        Me.Cursor = Cursors.WaitCursor

        ProfileTableAdapter.UpdateQuery(tin, Trim(txtRCN.Text), Trim(txtName.Text), Trim(txtAlias1.Text), Trim(txtAlias2.Text), Trim(Sex), Trim(txtBirthYear.Text), Trim(txtAddress1.Text), Trim(txtAddress2.Text), Trim(txtDCKD.Text), Trim(txtFathersName.Text), Trim(txtFathersAlias.Text), Trim(txtHenryClass.Text), Trim(txtDescription.Text), dtLastTrace.ValueObject, ov, FPSlipImageFile, OriginalTIN)

        If FPBDataSet.HasErrors Then FPBDataSet.RejectChanges()

        Dim oldRegionRow As FPBDataSet.profileRow
        oldRegionRow = FPBDataSet.profile.FindByTIN(OriginalTIN)
        If oldRegionRow IsNot Nothing Then
            With oldRegionRow
                .TIN = tin
                .RCN = Trim(txtRCN.Text)
                .Name = Trim(txtName.Text)
                .AliasN1 = Trim(txtAlias1.Text)
                .AliasN2 = Trim(txtAlias2.Text)
                .Sex = Trim(Sex)
                .BirthYear = Trim(txtBirthYear.Text)
                .Address1 = Trim(txtAddress1.Text)
                .Address2 = Trim(txtAddress2.Text)
                .PCN = Trim(txtDCKD.Text)
                .FathersName = Trim(txtFathersName.Text)
                .AliasFathers1 = Trim(txtFathersAlias.Text)
                .HenryClass = Trim(txtHenryClass.Text)
                .Description = Trim(txtDescription.Text)
                If Me.dtLastTrace.IsEmpty = False Then
                    .LatestTraceDate = dtLastTrace.Value
                Else
                    .LatestTraceDate = dtLastTrace.ValueObject
                End If
                .OV = ov
                .SlipFile = FPSlipImageFile
            End With
        End If
        If Me.FPBDataSet.HasErrors Then FPBDataSet.RejectChanges()

        ShowAlertMessage("Data updated successfully!")
        SaveRecentRecordsToRegistry(tin)

        InitializeFields()
        EditMode = False
        Me.btnSave.Text = "Save"
        Me.ProfileBindingSource.Position = Me.ProfileBindingSource.Find("TIN", tin)
        Me.Cursor = Cursors.Default
        Exit Sub
errhandler:
        Me.Cursor = Cursors.Default

        DevComponents.DotNetBar.MessageBoxEx.Show("The changes you made cannot be updated for the following reason:" & vbNewLine & vbNewLine & vbTab & Err.Description, AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Me.ProfileBindingSource.Position = Me.ProfileBindingSource.Find("TIN", tin)
    End Sub




    Function CheckTINExists(ByVal t As String) As Boolean
        On Error Resume Next
        Me.AutoTextAdapter.FillByTIN(AutoTextDataSet.profile, t)
        If AutoTextDataSet.profile.Count = 0 Then
            Return False
        Else
            Return True
        End If
    End Function



    Function CheckRCNExists(ByVal r As String) As Boolean
        On Error Resume Next
        Me.AutoTextAdapter.FillByRCN(AutoTextDataSet.profile, r)
        If AutoTextDataSet.profile.Count = 0 Then
            Return False
        Else
            Return True
        End If
    End Function


#End Region


#Region "OPEN RECORD"
    Private Sub OpenRecord() Handles btnOpenContextMenu.Click, btnOpen.Click
        On Error Resume Next

        EditMode = False
        Me.btnSave.Text = "Save"

        If Me.DataGridView.RowCount = 0 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("No data to open!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Me.DataGridView.SelectedRows.Count = 0 Then
            DevComponents.DotNetBar.MessageBoxEx.Show("No data selected!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If


        Me.ExpandablePanel1.Expanded = True
        ClearFPImage()

        With Me.DataGridView
            Me.txtTIN.Text = .SelectedCells(0).Value.ToString
            Me.txtRCN.Text = .SelectedCells(1).Value.ToString
            Me.txtDCKD.Text = .SelectedCells(2).Value.ToString
            Me.txtName.Text = .SelectedCells(3).Value.ToString
            Me.txtAlias1.Text = .SelectedCells(4).Value.ToString
            Me.txtAlias2.Text = .SelectedCells(5).Value.ToString
            Me.txtFathersName.Text = .SelectedCells(6).Value.ToString
            Me.txtFathersAlias.Text = .SelectedCells(7).Value.ToString

            If .SelectedCells(8).Value.ToString = "Female" Then
                Me.rdoFemale.Checked = 1
            ElseIf .SelectedCells(8).Value.ToString = "Male" Then
                Me.rdoMale.Checked = 1
            Else
                Me.rdoBothSex.Checked = 1

            End If

            Me.txtBirthYear.Text = .SelectedCells(9).Value.ToString()
            Me.txtAddress1.Text = .SelectedCells(10).Value.ToString
            Me.txtAddress2.Text = .SelectedCells(11).Value.ToString
            Me.txtHenryClass.Text = .SelectedCells(12).Value.ToString
            Me.txtDescription.Text = .SelectedCells(13).Value.ToString
            Me.dtLastTrace.ValueObject = .SelectedCells(14).Value.ToString
            If .SelectedCells(15).Value Then

                Me.rdoOV.Checked = True
            Else
                Me.rdoNotOV.Checked = True
            End If
            FPSlipImageFile = .SelectedCells(16).Value.ToString
            PreviewFPSlip()
        End With
        OriginalTIN = Me.txtTIN.Text
        OriginalRCN = Me.txtRCN.Text
        Me.txtTIN.Focus()
    End Sub
#End Region



#Region "IMPORT AND EXPORT DATABASE"


    Private Sub ExportDatabase() Handles btnExportData.Click, btnExportMenu.Click
        On Error GoTo errhandler
        Dim Source As String = AppPath & "\FPB.mdb"
        Dim Destination As String
        Me.FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer
        Me.FolderBrowserDialog1.ShowNewFolderButton = True
        Me.FolderBrowserDialog1.Description = "Select location to export database"
        Dim result As DialogResult = FolderBrowserDialog1.ShowDialog()
        If (result = DialogResult.OK) Then
            Dim Path As String = Me.FolderBrowserDialog1.SelectedPath
            If Path = AppPath Then

                DevComponents.DotNetBar.MessageBoxEx.Show("The source and destination are same. Cannot export database", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Destination = Path & "\FPB.mdb"
            My.Computer.FileSystem.CopyFile(Source, Destination, FileIO.UIOption.AllDialogs, FileIO.UICancelOption.ThrowException)
            ShowAlertMessage("Database exported successfully!")
        End If

        Exit Sub
errhandler:
    End Sub



    Sub ImportDatabase() Handles btnImport.Click, btnImportMenu.Click
        On Error Resume Next
        OpenFileDialog1.Filter = "FPB.mdb(*.mdb)|*.mdb"
        OpenFileDialog1.FileName = "FPB.mdb"
        OpenFileDialog1.Title = "Select Database to import data"
        OpenFileDialog1.RestoreDirectory = True
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim SelectedDB As String = OpenFileDialog1.FileName
            If SelectedDB = AppPath & "\FPB.mdb" Then
                DevComponents.DotNetBar.MessageBoxEx.Show("The source and destination files are same. Cannot import data!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Me.Refresh()
            Application.DoEvents()
            If DBHasPassword(SelectedDB) Then
                frmInputBox.SetTitleandMessage("Enter Password", "The selected Database is password protected. Please enter the password.")
                frmInputBox.AcceptButton = frmInputBox.btnOK
                frmInputBox.txtInputBox.UseSystemPasswordChar = True
                frmInputBox.ShowDialog()
                If frmInputBox.ButtonClicked <> "OK" Then Exit Sub
                Dim Pass = frmInputBox.txtInputBox.Text
                If OpenSelectedDB(SelectedDB, Pass, True) = False Then Exit Sub
            Else
                If OpenSelectedDB(SelectedDB, "", False) = False Then Exit Sub
            End If
        End If

        Me.Cursor = Cursors.WaitCursor
        ProfileTableMergeAdapter.Fill(FpbMergeDataSet.profile)
        Dim initialcount As Long = ProfileTableAdapter.CountQuery
        Me.TempProfileTableAdapter.Fill(Me.TempFpbDataSet.profile)
        Me.TempFpbDataSet.Merge(Me.FpbMergeDataSet.profile)
        ' Me.ProgressBar.Visible = True
        Dim finalcount = TempFpbDataSet.profile.Count - 1

        Dim progvalue = finalcount - initialcount + 1
        If progvalue = 0 Then
            ShowAlertMessage("No records were imported!")
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        frmImportData.StartPosition = FormStartPosition.CenterScreen
        frmImportData.Show()

        frmImportData.ProgressBar.Maximum = progvalue
        frmImportData.ProgressBar.Text = "Please Wait. Importing Data ..."
        For i As Long = initialcount To finalcount
            If CancelImport = True Then
                Dim reply As DialogResult

                reply = MessageBoxEx.Show("Do you want to cancel importing of data?", AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                If reply = Windows.Forms.DialogResult.Yes Then
                    Exit For
                Else
                    CancelImport = False
                End If
            End If

            With TempFpbDataSet.profile.Item(i)
                Dim tin = ""
                Dim rcn = ""
                Dim name = ""
                Dim aliasn1 = ""
                Dim aliasn2 = ""
                Dim sex = ""
                Dim birthyear = ""
                Dim address1 = ""
                Dim address2 = ""
                Dim pcn = ""
                Dim fathersname = ""
                Dim aliasfathers1 = ""
                Dim HenryClass = ""
                Dim description = ""
                Me.dtDummy.Text = ""
                Me.dtDummy.Text = .LatestTraceDate
                Dim latesttracedate = Me.dtDummy.ValueObject
                Dim ov = False
                Dim slipfile = ""

                tin = .TIN
                rcn = .RCN
                name = .Name
                aliasn1 = .AliasN1
                aliasn2 = .AliasN2
                sex = .Sex
                birthyear = .BirthYear
                address1 = .Address1
                address2 = .Address2
                pcn = .PCN
                fathersname = .FathersName
                aliasfathers1 = .AliasFathers1
                HenryClass = .HenryClass
                description = .Description

                latesttracedate = IIf(.LatestTraceDate = Nothing, Me.dtDummy.ValueObject, .LatestTraceDate)
                ov = .OV
                slipfile = .SlipFile

                If CheckTINExists(tin) = False Then
                    ProfileTableAdapter.Insert(tin, rcn, name, aliasn1, aliasn2, sex, birthyear, address1, address2, pcn, fathersname, aliasfathers1, HenryClass, description, latesttracedate, ov, slipfile)
                End If
                frmImportData.ProgressBar.Increment(1)
                frmImportData.ProgressBar.Text = "Please Wait. Imported " & frmImportData.ProgressBar.Value & " of " & progvalue & " records"
                Application.DoEvents()
            End With

        Next
        frmImportData.Close()
        UpdateNullValues()

        LoadDataToTable()
        DisplayRecordCount()
        Me.FpbMergeDataSet.Clear()
        Me.TempFpbDataSet.Clear()

        ProfileTableMergeAdapter.Connection.Close()
        finalcount = ProfileTableAdapter.CountQuery
        Dim c = finalcount - initialcount
        If c = 0 Then
            ShowAlertMessage("No records were imported!")
        ElseIf c = 1 Then
            ShowAlertMessage("1 Record imported successfully!")
        Else
            ShowAlertMessage(c & " Records imported successfully!")
        End If
        Me.Cursor = Cursors.Default


    End Sub

    Private Function OpenSelectedDB(ByVal SelectedDB As String, ByVal Pass As String, ByVal HasPassword As Boolean) As Boolean
        OpenSelectedDB = False
        Try
            If ProfileTableMergeAdapter.Connection.State = Data.ConnectionState.Open Then ProfileTableMergeAdapter.Connection.Close()
            If HasPassword Then
                ProfileTableMergeAdapter.Connection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & SelectedDB & "; Jet OLEDB:Database Password=" & Pass
            Else
                ProfileTableMergeAdapter.Connection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & SelectedDB
            End If

            ProfileTableMergeAdapter.Connection.Open()
            OpenSelectedDB = True
        Catch ex As Exception
            MessageBoxEx.Show(ex.Message, AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            OpenSelectedDB = False
        End Try
    End Function

    Private Function DBHasPassword(ByVal DB As String) As Boolean
        DBHasPassword = False

        Try
            If ProfileTableMergeAdapter.Connection.State = Data.ConnectionState.Open Then ProfileTableMergeAdapter.Connection.Close()
            ProfileTableMergeAdapter.Connection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DB
            ProfileTableMergeAdapter.Connection.Open()
            DBHasPassword = False
        Catch ex As Exception
            If ex.Message.ToLower = "not a valid password." Then
                DBHasPassword = True
            Else
                MessageBoxEx.Show(ex.Message, AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End Try
    End Function

#End Region




#Region "DELETE DATA"


    Private Sub UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles DataGridView.UserDeletingRow
        On Error Resume Next
        e.Cancel = True
        Call DeleteSelectedItem()
    End Sub



    Private Sub DeleteSelectedItem() Handles btnDelete.Click, btnDeleteContextMenu.Click
        On Error GoTo errhandler
        If Me.DataGridView.RowCount = 0 Then
            MessageBoxEx.Show("No data to delete!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Exit Sub
        End If

        If Me.DataGridView.SelectedRows.Count = 0 Then
            MessageBoxEx.Show("No data selected!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Exit Sub
        End If

        If Me.chkPreventDeletion.Checked Then
            MessageBoxEx.Show("Please uncheck the box 'Prevent Deletion' next to the Delete button to allow deletion of data.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim reply As DialogResult
        reply = MessageBoxEx.Show("Do you really want to delete the selected data?", AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If reply = Windows.Forms.DialogResult.Yes Then
            Dim tin As String = Me.DataGridView.SelectedCells(0).Value.ToString()
            Me.ProfileTableAdapter.DeleteQuery(tin)

            If Me.FPBDataSet.HasErrors Then FPBDataSet.RejectChanges()
            Dim oldRegionRow As FPBDataSet.profileRow
            oldRegionRow = FPBDataSet.profile.FindByTIN(tin)
            oldRegionRow.Delete()
            If Me.FPBDataSet.HasErrors Then FPBDataSet.RejectChanges()
            ShowAlertMessage("Selected data deleted")
            If Me.DataGridView.SelectedRows.Count = 0 And Me.DataGridView.RowCount <> 0 Then
                Me.DataGridView.Rows(Me.DataGridView.RowCount - 1).Selected = True
            End If
            Me.dtLastTrace.Text = ""
            Me.rdoMale.Checked = True
            InitializeFields()
            Me.rdoNotOV.Checked = True
            EditMode = False
            Me.btnSave.Text = "Save"
            DisplayRecordCount()
        End If
        Exit Sub
errhandler:
        MessageBoxEx.Show(Err.Description, AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

    End Sub


    Sub DeleteAllData() Handles btnDeleteAll.Click
        On Error Resume Next
        If Me.chkPreventDeletion.Checked Then
            MessageBoxEx.Show("Please uncheck the box 'Prevent Deletion' next to the Delete button to allow deletion of data.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        frmInputBox.SetTitleandMessage("Confirm Delete All Records", "Please enter the words 'I want to delete all records' to confirm deletion. This is a security measure.")
        frmInputBox.AcceptButton = frmInputBox.btnCancel
        frmInputBox.txtInputBox.UseSystemPasswordChar = False
        frmInputBox.ShowDialog()
        If frmInputBox.ButtonClicked <> "OK" Then Exit Sub
        If frmInputBox.txtInputBox.Text <> "I want to delete all records" Then
            MessageBoxEx.Show("The words you entered do not match the test words!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub

        End If




        Dim reply As DialogResult


        reply = MessageBoxEx.Show("Caution: This action will delete all data from the database. Do you really want to delete all data?", AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
        If reply = Windows.Forms.DialogResult.Yes Then
            Me.ProfileTableAdapter.DeleteAll()
            ShowAlertMessage("All data deleted permanently!")
            InitializeFields()
            EditMode = False
            LoadDataToTable()
        End If

    End Sub
#End Region



#Region " BACKUP DATABASE"



    Private Sub TakeBackup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackup.Click, btnBackupMenu.Click
        On Error GoTo errhandler

        Dim reply As DialogResult = MessageBoxEx.Show("Click 'Yes' to take backup or 'No' to exit without taking backup.", AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        If reply = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim Source As String = AppPath & "\FPB.mdb"

        Dim Destination As String = My.Computer.Registry.GetValue(RegistrySettingsPath, "BackupPath", FileIO.SpecialDirectories.MyDocuments & "\BXSofts\" & AppName & "\Backups")

        My.Computer.Registry.SetValue(RegistrySettingsPath, "BackupPath", Destination, Microsoft.Win32.RegistryValueKind.String)

        If My.Computer.FileSystem.DirectoryExists(Destination) = False Then
            My.Computer.FileSystem.CreateDirectory(Destination)
        End If
        If Strings.Right(Destination, 1) <> "\" Then Destination = Destination & "\"
        Dim cnt As Long = ProfileTableAdapter.CountQuery

        Dim BackupFileName As String = "FPBBackup-" & Strings.Format(Now, "dd-MM-yyyy-h-mm-ss-tt") & "Records-" & cnt & ".fpb"

        Destination = Destination & BackupFileName
        My.Computer.FileSystem.CopyFile(Source, Destination, True) ', FileIO.UIOption.AllDialogs, FileIO.UICancelOption.ThrowException)
        Application.DoEvents()
        ShowAlertMessage("Database backed up successfully!")
        Application.DoEvents()

        Exit Sub
errhandler:
    End Sub





    Private Sub SetBackupPath(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackupPath.Click
        On Error GoTo errhandler
        Dim Destination As String = My.Computer.Registry.GetValue(RegistrySettingsPath, "BackupPath", FileIO.SpecialDirectories.MyDocuments & "\BXSofts\Nominal Index System\Backups")
        '  Me.FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer
        Me.FolderBrowserDialog1.ShowNewFolderButton = True
        Me.FolderBrowserDialog1.Description = "Select new location for backups"
        Me.FolderBrowserDialog1.SelectedPath = Destination
        Dim result As DialogResult = FolderBrowserDialog1.ShowDialog()
        If (result = DialogResult.OK) Then
            Dim Path As String = Me.FolderBrowserDialog1.SelectedPath
            My.Computer.Registry.SetValue(RegistrySettingsPath, "BackupPath", Path, Microsoft.Win32.RegistryValueKind.String)
            ShowAlertMessage("Backup location changed!")
        End If

        Exit Sub
errhandler:
        ' MsgBox(Err.Description, MsgBoxStyle.Exclamation, AppName)
        MessageBoxEx.Show(Err.Description, AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

    End Sub




    Private Sub btnOpenBackupLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenBackupLocation.Click
        On Error Resume Next
        Dim BackupPath As String = My.Computer.Registry.GetValue(RegistrySettingsPath, "BackupPath", FileIO.SpecialDirectories.MyDocuments & "\BXSofts\" & AppName & "\Backups")

        If FileIO.FileSystem.DirectoryExists(BackupPath) Then
            BackupPath = String.Concat("""", BackupFile, +"""")
            Call Shell("explorer.exe " & BackupPath, AppWinStyle.NormalFocus)
        Else
            FileIO.FileSystem.CreateDirectory(BackupPath)
            BackupPath = String.Concat("""", BackupFile, +"""")
            Call Shell("explorer.exe " & BackupPath, AppWinStyle.NormalFocus)
        End If
    End Sub

#End Region



#Region " RESTORE DATABASE"


    Private Sub RestoreDatabase() Handles btnRestore.Click, btnRestoreMenu.Click
        On Error Resume Next
        restored = False
        frmBackupList.ShowDialog()
        If restored Then
            ShowAlertMessage("Database restored successfully!")
            Me.FPBDataSet.Clear()
            ConnectToDatabase()
            LoadDataToTable()
        End If
        restored = False
    End Sub



    Private Sub RestoreDatabaseManually(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectRestoreFile.Click
        On Error GoTo errhandler


        frmInputBox.SetTitleandMessage("User Authentication Required", "Please enter password to restore Database.")
        frmInputBox.AcceptButton = frmInputBox.btnOK
        frmInputBox.txtInputBox.UseSystemPasswordChar = True
        frmInputBox.ShowDialog()
        If frmInputBox.ButtonClicked <> "OK" Then Exit Sub
        If frmInputBox.txtInputBox.Text <> Password Then
            DevComponents.DotNetBar.MessageBoxEx.Show("Wrong password!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim reply As DialogResult

        reply = MessageBoxEx.Show("Restoring the database will overwrite the existing database." & vbNewLine & "Do you want to continue?", AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
        If reply = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim BackupPath As String = My.Computer.Registry.GetValue(RegistrySettingsPath, "BackupPath", FileIO.SpecialDirectories.MyDocuments & "\BXSofts\" & AppName & "\Backups")

        OpenFileDialog1.Filter = "FPB Backup Files(*.fpb,*.mdb)|*.fpb;*.mdb"
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Title = "Select Backup File"
        OpenFileDialog1.RestoreDirectory = True
        OpenFileDialog1.InitialDirectory = BackupPath
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            BackupFile = OpenFileDialog1.FileName
            If BackupFile = AppPath & "\FPB.mdb" Then
                '    MsgBox("The source and destination are same. Cannot restore database", MsgBoxStyle.Critical, AppName)
                MessageBoxEx.Show("The source and destination are same. Cannot restore database", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Exit Sub
            End If

            '  If IsValidBackupFile(BackupFile) = False Then
            'MessageBoxEx.Show("The file you selected is not a valid Database. Cannot restore database.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            'Exit Sub
            ' End If

            My.Computer.FileSystem.CopyFile(BackupFile, AppPath & "\FPB.mdb", True)
            Application.DoEvents()
            ShowAlertMessage("Database restored successfully!")
            Me.FPBDataSet.Clear()
            ConnectToDatabase()
            LoadDataToTable()

        End If

        Exit Sub
errhandler:
        ' MsgBox(Err.Description, MsgBoxStyle.Exclamation, AppName)
        MessageBoxEx.Show(Err.Description, AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub

    Private Function IsValidBackupFile(ByVal DBPath As String) As Boolean
        IsValidBackupFile = False

        Try
            If DBHasPassword(DBPath) Then
                DBPath = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DBPath & "; Jet OLEDB:Database Password=" & Password
            Else
                DBPath = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DBPath
            End If
            If DoesTableExist("profile", DBPath) Then
                IsValidBackupFile = True
            Else
                IsValidBackupFile = False
            End If
        Catch ex As Exception
            IsValidBackupFile = False
        End Try
    End Function

    Public Function DoesTableExist(ByVal tblName As String, ByVal cnnStr As String) As Boolean
        Try
            ' Open connection to the database
            Dim dbConn As New OleDb.OleDbConnection(cnnStr)
            dbConn.Open()


            Dim restrictions(3) As String
            restrictions(2) = tblName
            Dim dbTbl As DataTable = dbConn.GetSchema("Tables", restrictions)

            If dbTbl.Rows.Count = 0 Then
                'Table does not exist
                DoesTableExist = False
            Else
                'Table exists
                DoesTableExist = True
            End If

            dbTbl.Dispose()
            dbConn.Close()
            dbConn.Dispose()
        Catch ex As Exception
            DoesTableExist = False
        End Try
    End Function
#End Region



#Region "SEARCH DATA"



    Private Sub SearchTextMode(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoBeginsWith.CheckedChanged, rdoFullText.CheckedChanged, rdoAnyWhere.CheckedChanged
        On Error Resume Next

        If rdoBeginsWith.Checked = True Then SearchSetting = 0
        If rdoFullText.Checked = True Then SearchSetting = 1
        If rdoAnyWhere.Checked = True Then SearchSetting = 2
    End Sub


    Private Sub SetSexStatus() Handles rdoMale.CheckedChanged, rdoFemale.CheckedChanged, rdoBothSex.CheckedChanged
        On Error Resume Next
        If rdoMale.Checked = True Then Sex = 0
        If rdoFemale.Checked = True Then Sex = 1
        If rdoBothSex.Checked = True Then Sex = 2

    End Sub



    Private Sub SetOVStatus() Handles rdoOV.CheckedChanged, rdoNotOV.CheckedChanged, rdoBothOV.CheckedChanged
        On Error Resume Next
        If rdoOV.Checked = True Then OVStatus = 1
        If rdoNotOV.Checked = True Then OVStatus = 0
        If rdoBothOV.Checked = True Then OVStatus = 2

    End Sub




    Public Sub SearchOldCode() ' Handles btnSearchNow.Click

        On Error GoTo errhandler
        Me.Cursor = Cursors.WaitCursor
        SetOVStatus()
        SetSexStatus()
        AddToAutoFillWordsList()
        Dim sTIN = txtTIN.Text
        Dim sRCN = txtRCN.Text
        Dim sName = txtName.Text
        Dim sAlias1 = txtAlias1.Text
        Dim sAlias2 = txtAlias2.Text


        If rdoMale.Checked = True Then Sex = "Male"
        If rdoFemale.Checked = True Then Sex = "Female"
        If rdoBothSex.Checked = True Then Sex = "%"

        If rdoOV.Checked = True Then OVStatus = 1
        If rdoNotOV.Checked = True Then OVStatus = 0
        If rdoBothOV.Checked = True Then OVStatus = 2



        Dim sBirthYear = txtBirthYear.Text
        Dim sAddress1 = txtAddress1.Text
        Dim sAddress2 = txtAddress2.Text
        Dim sPCN = txtDCKD.Text
        Dim sFathersName = txtFathersName.Text
        Dim sFathersAlias = txtFathersAlias.Text
        Dim sHenryClass = txtHenryClass.Text
        Dim sDescription = txtDescription.Text
        Dim sLatestTrace = dtLastTrace.Value

        If SearchSetting = 1 Then
            If sTIN = vbNullString Then sTIN = "%"
            If sRCN = vbNullString Then sRCN = "%"
            If sName = vbNullString Then sName = "%"
            If sAlias1 = vbNullString Then sAlias1 = "%"
            If sAlias2 = vbNullString Then sAlias2 = "%"
            If sBirthYear = vbNullString Then sBirthYear = "%"
            If sAddress1 = vbNullString Then sAddress1 = "%"
            If sAddress2 = vbNullString Then sAddress2 = "%"
            If sPCN = vbNullString Then sPCN = "%"
            If sFathersName = vbNullString Then sFathersName = "%"
            If sFathersAlias = vbNullString Then sFathersAlias = "%"
            If sHenryClass = vbNullString Then sHenryClass = "%"
            If sDescription = vbNullString Then sDescription = "%"

        End If


        If Me.dtLastTrace.Text = "" Then
            Select Case SearchSetting
                Case 0 'begins with

                    If OVStatus = 1 Then
                        Me.ProfileTableAdapter.FillByOVIsTrue(Me.FPBDataSet.profile, sTIN & "%", sRCN & "%", sName & "%", sAlias1 & "%", sAlias2 & "%", Sex & "%", sBirthYear & "%", sAddress1 & "%", sAddress2 & "%", sPCN & "%", sFathersName & "%", sFathersAlias & "%", sHenryClass & "%", sDescription & "%")
                    End If

                    If OVStatus = 0 Then
                        Me.ProfileTableAdapter.FillByOVIsFalse(Me.FPBDataSet.profile, sTIN & "%", sRCN & "%", sName & "%", sAlias1 & "%", sAlias2 & "%", Sex & "%", sBirthYear & "%", sAddress1 & "%", sAddress2 & "%", sPCN & "%", sFathersName & "%", sFathersAlias & "%", sHenryClass & "%", sDescription & "%")
                    End If

                    If OVStatus = 2 Then
                        Me.ProfileTableAdapter.FillByAllOV(Me.FPBDataSet.profile, sTIN & "%", sRCN & "%", sName & "%", sAlias1 & "%", sAlias2 & "%", Sex & "%", sBirthYear & "%", sAddress1 & "%", sAddress2 & "%", sPCN & "%", sFathersName & "%", sFathersAlias & "%", sHenryClass & "%", sDescription & "%")

                    End If

                Case 1 'full text

                    If OVStatus = 1 Then

                        Me.ProfileTableAdapter.FillByOVIsTrue(Me.FPBDataSet.profile, sTIN, sRCN, sName, sAlias1, sAlias2, Sex, sBirthYear, sAddress1, sAddress2, sPCN, sFathersName, sFathersAlias, sHenryClass, sDescription)
                    End If

                    If OVStatus = 0 Then

                        Me.ProfileTableAdapter.FillByOVIsFalse(Me.FPBDataSet.profile, sTIN, sRCN, sName, sAlias1, sAlias2, Sex, sBirthYear, sAddress1, sAddress2, sPCN, sFathersName, sFathersAlias, sHenryClass, sDescription)
                    End If

                    If OVStatus = 2 Then

                        Me.ProfileTableAdapter.FillByAllOV(Me.FPBDataSet.profile, sTIN, sRCN, sName, sAlias1, sAlias2, Sex, sBirthYear, sAddress1, sAddress2, sPCN, sFathersName, sFathersAlias, sHenryClass, sDescription)
                    End If



                Case 2 'any where

                    If OVStatus = 1 Then

                        Me.ProfileTableAdapter.FillByOVIsTrue(Me.FPBDataSet.profile, "%" & sTIN & "%", "%" & sRCN & "%", "%" & sName & "%", "%" & sAlias1 & "%", "%" & sAlias2 & "%", Sex, "%" & sBirthYear & "%", "%" & sAddress1 & "%", "%" & sAddress2 & "%", "%" & sPCN & "%", "%" & sFathersName & "%", "%" & sFathersAlias & "%", "%" & sHenryClass & "%", "%" & sDescription & "%")

                    End If

                    If OVStatus = 0 Then

                        Me.ProfileTableAdapter.FillByOVIsFalse(Me.FPBDataSet.profile, "%" & sTIN & "%", "%" & sRCN & "%", "%" & sName & "%", "%" & sAlias1 & "%", "%" & sAlias2 & "%", Sex, "%" & sBirthYear & "%", "%" & sAddress1 & "%", "%" & sAddress2 & "%", "%" & sPCN & "%", "%" & sFathersName & "%", "%" & sFathersAlias & "%", "%" & sHenryClass & "%", "%" & sDescription & "%")

                    End If


                    If OVStatus = 2 Then

                        Me.ProfileTableAdapter.FillByAllOV(Me.FPBDataSet.profile, "%" & sTIN & "%", "%" & sRCN & "%", "%" & sName & "%", "%" & sAlias1 & "%", "%" & sAlias2 & "%", Sex, "%" & sBirthYear & "%", "%" & sAddress1 & "%", "%" & sAddress2 & "%", "%" & sPCN & "%", "%" & sFathersName & "%", "%" & sFathersAlias & "%", "%" & sHenryClass & "%", "%" & sDescription & "%")

                    End If

                Case Else


            End Select
        End If





        If Me.dtLastTrace.Text <> "" Then
            Select Case SearchSetting
                Case 0 'begins with

                    If OVStatus = 1 Then
                        Me.ProfileTableAdapter.FillByDateOVIsTrue(Me.FPBDataSet.profile, sTIN & "%", sRCN & "%", sName & "%", sAlias1 & "%", sAlias2 & "%", Sex & "%", sBirthYear & "%", sAddress1 & "%", sAddress2 & "%", sPCN & "%", sFathersName & "%", sFathersAlias & "%", sHenryClass & "%", sDescription & "%", sLatestTrace)
                    End If

                    If OVStatus = 0 Then
                        Me.ProfileTableAdapter.FillByDateOVisFalse(Me.FPBDataSet.profile, sTIN & "%", sRCN & "%", sName & "%", sAlias1 & "%", sAlias2 & "%", Sex & "%", sBirthYear & "%", sAddress1 & "%", sAddress2 & "%", sPCN & "%", sFathersName & "%", sFathersAlias & "%", sHenryClass & "%", sDescription & "%", sLatestTrace)
                    End If

                    If OVStatus = 2 Then
                        Me.ProfileTableAdapter.FillByDateOVisAny(Me.FPBDataSet.profile, sTIN & "%", sRCN & "%", sName & "%", sAlias1 & "%", sAlias2 & "%", Sex & "%", sBirthYear & "%", sAddress1 & "%", sAddress2 & "%", sPCN & "%", sFathersName & "%", sFathersAlias & "%", sHenryClass & "%", sDescription & "%", sLatestTrace)
                    End If

                Case 1 'full text

                    If OVStatus = 1 Then

                        Me.ProfileTableAdapter.FillByDateOVIsTrue(Me.FPBDataSet.profile, sTIN, sRCN, sName, sAlias1, sAlias2, Sex, sBirthYear, sAddress1, sAddress2, sPCN, sFathersName, sFathersAlias, sHenryClass, sDescription, sLatestTrace)
                    End If

                    If OVStatus = 0 Then

                        Me.ProfileTableAdapter.FillByDateOVisFalse(Me.FPBDataSet.profile, sTIN, sRCN, sName, sAlias1, sAlias2, Sex, sBirthYear, sAddress1, sAddress2, sPCN, sFathersName, sFathersAlias, sHenryClass, sDescription, sLatestTrace)
                    End If

                    If OVStatus = 2 Then

                        Me.ProfileTableAdapter.FillByDateOVisAny(Me.FPBDataSet.profile, sTIN, sRCN, sName, sAlias1, sAlias2, Sex, sBirthYear, sAddress1, sAddress2, sPCN, sFathersName, sFathersAlias, sHenryClass, sDescription, sLatestTrace)
                    End If



                Case 2 'any where

                    If OVStatus = 1 Then

                        Me.ProfileTableAdapter.FillByDateOVIsTrue(Me.FPBDataSet.profile, "%" & sTIN & "%", "%" & sRCN & "%", "%" & sName & "%", "%" & sAlias1 & "%", "%" & sAlias2 & "%", Sex, "%" & sBirthYear & "%", "%" & sAddress1 & "%", "%" & sAddress2 & "%", "%" & sPCN & "%", "%" & sFathersName & "%", "%" & sFathersAlias & "%", "%" & sHenryClass & "%", "%" & sDescription & "%", sLatestTrace)

                    End If

                    If OVStatus = 0 Then

                        Me.ProfileTableAdapter.FillByDateOVisFalse(Me.FPBDataSet.profile, "%" & sTIN & "%", "%" & sRCN & "%", "%" & sName & "%", "%" & sAlias1 & "%", "%" & sAlias2 & "%", Sex, "%" & sBirthYear & "%", "%" & sAddress1 & "%", "%" & sAddress2 & "%", "%" & sPCN & "%", "%" & sFathersName & "%", "%" & sFathersAlias & "%", "%" & sHenryClass & "%", "%" & sDescription & "%", sLatestTrace)

                    End If


                    If OVStatus = 2 Then

                        Me.ProfileTableAdapter.FillByDateOVisAny(Me.FPBDataSet.profile, "%" & sTIN & "%", "%" & sRCN & "%", "%" & sName & "%", "%" & sAlias1 & "%", "%" & sAlias2 & "%", Sex, "%" & sBirthYear & "%", "%" & sAddress1 & "%", "%" & sAddress2 & "%", "%" & sPCN & "%", "%" & sFathersName & "%", "%" & sFathersAlias & "%", "%" & sHenryClass & "%", "%" & sDescription & "%", sLatestTrace)

                    End If

                Case Else


            End Select
        End If
        Me.Cursor = Cursors.Default
        DisplayRecordCount()
        ShowAlertMessage("Search finished. Found " & IIf(Me.DataGridView.RowCount = 1, "1 Record", Me.DataGridView.RowCount & " Records"))
        Application.DoEvents()
        Exit Sub
errhandler:
        MessageBoxEx.Show(Err.Description, AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub


    Public Sub NEWSearch() Handles btnSearchNow.Click




        On Error GoTo errhandler
        Me.Cursor = Cursors.WaitCursor
        SetOVStatus()
        SetSexStatus()
        AddToAutoFillWordsList()

        Dim sTIN = txtTIN.Text
        Dim sRCN = txtRCN.Text
        Dim sName = txtName.Text
        Dim sAlias1 = txtAlias1.Text
        Dim sAlias2 = txtAlias2.Text
        Dim sBirthYear = txtBirthYear.Text
        Dim sAddress1 = txtAddress1.Text
        Dim sAddress2 = txtAddress2.Text
        Dim sPCN = txtDCKD.Text
        Dim sFathersName = txtFathersName.Text
        Dim sFathersAlias = txtFathersAlias.Text
        Dim sHenryClass = txtHenryClass.Text
        Dim sDescription = txtDescription.Text


        If rdoMale.Checked = True Then Sex = "Male"
        If rdoFemale.Checked = True Then Sex = "Female"
        If rdoBothSex.Checked = True Then Sex = "%"

        Dim OV As String = ""

        If rdoOV.Checked = True Then OV = "  YES"
        If rdoNotOV.Checked = True Then OV = " NO"
        If rdoBothOV.Checked = True Then OV = " '%'"

        If sHenryClass = vbNullString Then
            sHenryClass = "HenryClass Like '%'"
        Else
            If sHenryClass.Contains("%") Or sHenryClass.Contains("_") Then
                sHenryClass = "HenryClass Like '" & sHenryClass & "'"
            Else
                sHenryClass = "instr(1, HenryClass, '" & sHenryClass & "', 0)>0 "
            End If
        End If

        If SearchSetting = 0 Then 'begins with
            sTIN += "%" 'append % to end
            sRCN += "%"
            sName += "%"
            sAlias1 += "%"
            sAlias2 += "%"
            sBirthYear += "%"
            sAddress1 += "%"
            sAddress2 += "%"
            sPCN += "%"
            sFathersName += "%"
            sFathersAlias += "%"
            sDescription += "%"
        End If

        If SearchSetting = 1 Then 'full text
            If sTIN = vbNullString Then sTIN = "%"
            If sRCN = vbNullString Then sRCN = "%"
            If sName = vbNullString Then sName = "%"
            If sAlias1 = vbNullString Then sAlias1 = "%"
            If sAlias2 = vbNullString Then sAlias2 = "%"
            If sBirthYear = vbNullString Then sBirthYear = "%"
            If sAddress1 = vbNullString Then sAddress1 = "%"
            If sAddress2 = vbNullString Then sAddress2 = "%"
            If sPCN = vbNullString Then sPCN = "%"
            If sFathersName = vbNullString Then sFathersName = "%"
            If sFathersAlias = vbNullString Then sFathersAlias = "%"
            If sDescription = vbNullString Then sDescription = "%"
        End If




        If SearchSetting = 2 Then 'any where
            sTIN = "%" & sTIN & "%" 'append % to end
            sRCN = "%" & sRCN & "%"
            sName = "%" & sName & "%"
            sAlias1 = "%" & sAlias1 & "%"
            sAlias2 = "%" & sAlias2 & "%"
            sBirthYear = "%" & sBirthYear & "%"
            sAddress1 = "%" & sAddress1 & "%"
            sAddress2 = "%" & sAddress2 & "%"
            sPCN = "%" & sPCN & "%"
            sFathersName = "%" & sFathersName & "%"
            sFathersAlias = "%" & sFathersAlias & "%"
            sDescription = "%" & sDescription & "%"
        End If

        Dim SQLText As String = "Select * from PROFILE order by Name"


        If Me.dtLastTrace.Text = "" Then
            SQLText = "Select * from PROFILE where TIN LIKE '" & sTIN & "' AND RCN LIKE '" & sRCN & "' AND PCN LIKE '" & sPCN & "' AND Name LIKE '" & sName & "' AND AliasN1 LIKE '" & sAlias1 & "' AND AliasN2 LIKE '" & sAlias2 & "' AND FathersName LIKE '" & sFathersName & "' AND AliasFathers1 LIKE '" & sFathersAlias & "' AND Sex LIKE '" & Sex & "' AND BirthYear LIKE '" & sBirthYear & "' AND Address1 LIKE '" & sAddress1 & "' AND Address2 LIKE '" & sAddress2 & "' AND " & sHenryClass & " AND Description LIKE '" & sDescription & "' AND OV LIKE " & OV
        End If




        If Me.dtLastTrace.Text <> "" Then
            SQLText = "Select * from PROFILE where TIN LIKE '" & sTIN & "' AND RCN LIKE '" & sRCN & "' AND PCN LIKE '" & sPCN & "' AND Name LIKE '" & sName & "' AND AliasN1 LIKE '" & sAlias1 & "' AND AliasN2 LIKE '" & sAlias2 & "' AND FathersName LIKE '" & sFathersName & "' AND AliasFathers1 LIKE '" & sFathersAlias & "' AND Sex LIKE '" & Sex & "' AND BirthYear LIKE '" & sBirthYear & "' AND Address1 LIKE '" & sAddress1 & "' AND Address2 LIKE '" & sAddress2 & "' AND " & sHenryClass & " AND LatestTraceDate = #" & dtLastTrace.Value & "# AND Description LIKE '" & sDescription & "' AND OV LIKE " & OV
        End If

        SQLText = SQLText.Replace("%%", "%")

        Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(ConString)
        con.Open()
        Dim cmd As OleDb.OleDbCommand = New OleDb.OleDbCommand(SQLText, con)
        Dim da As New OleDb.OleDbDataAdapter(cmd)
        Me.FPBDataSet.profile.Clear()
        da.Fill(Me.FPBDataSet.profile)
        con.Close()


        Me.Cursor = Cursors.Default
        DisplayRecordCount()
        ShowAlertMessage("Search finished. Found " & IIf(Me.DataGridView.RowCount = 1, "1 Record", Me.DataGridView.RowCount & " Records"))
        Application.DoEvents()
        Exit Sub
errhandler:
        MessageBoxEx.Show(Err.Description, AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub SearchInAliasAlso() Handles btnSearchAliasAlso.Click
        On Error Resume Next
        Dim sName = txtName.Text
        If sName = vbNullString Then
            MessageBoxEx.Show("Please enter a search text in the Name field!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtName.Focus()
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(ConString)
        con.Open()
        Dim cmd As OleDb.OleDbCommand = New OleDb.OleDbCommand(GenerateSQL, con)
        Dim da As New OleDb.OleDbDataAdapter(cmd)
        Me.FPBDataSet.profile.Clear()
        da.Fill(Me.FPBDataSet.profile)
        con.Close()
        Me.Cursor = Cursors.Default
        DisplayRecordCount()
        ShowAlertMessage("Search finished. Found " & IIf(Me.DataGridView.RowCount = 1, "1 Record", Me.DataGridView.RowCount & " Records"))
        Application.DoEvents()
    End Sub


    Public Sub SearchAliasAlsoOLD() ' Handles btnSearchAliasAlso.Click

        On Error Resume Next

        Dim sName = txtName.Text
        If sName = vbNullString Then
            MessageBoxEx.Show("Please enter a search text in the Name field!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtName.Focus()
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        AddToAutoFillWordsList()
        Dim sAlias1 = sName
        Dim sAlias2 = sName
        Dim sFatherName = Me.txtFathersName.Text
        Dim sAliasFathers As String = sFatherName
        Dim sHenryClass As String = Me.txtHenryClass.Text


        If SearchSetting = 1 Then
            If sName = vbNullString Then sName = "%"
            If sAlias1 = vbNullString Then sAlias1 = "%"
            If sAlias2 = vbNullString Then sAlias2 = "%"
            If sHenryClass = vbNullString Then sHenryClass = "%"
            If sFatherName = vbNullString Then sFatherName = "%"
        End If


        Select Case SearchSetting
            Case 0 'begins with
                Me.ProfileTableAdapter.SearchInAliasAndFatherAliasAlso(Me.FPBDataSet.profile, sName & "%", sAlias1 & "%", sAlias2 & "%", sFatherName & "%", sAliasFathers & "%") ', sHenryClass & "%")

                '  Me.ProfileTableAdapter.SearchInAliasAlso(Me.FPBDataSet.profile, sName & "%", sHenryClass & "%", sHenryClass & "%", sFatherName & "%", sHenryClass & "%", sAlias1 & "%", sHenryClass & "%", sAlias2 & "%")

            Case 1 'full text

                Me.ProfileTableAdapter.SearchInAliasAlso(Me.FPBDataSet.profile, sName, sHenryClass, sHenryClass, sFatherName, sHenryClass, sAlias1, sHenryClass, sAlias2)

            Case 2 'any where

                Me.ProfileTableAdapter.SearchInAliasAlso(Me.FPBDataSet.profile, "%" & sName & "%", "%" & sHenryClass & "%", "%" & sHenryClass & "%", "%" & sFatherName & "%", "%" & sHenryClass & "%", "%" & sAlias1 & "%", "%" & sHenryClass & "%", "%" & sAlias2 & "%")

            Case Else


        End Select

        Me.Cursor = Cursors.Default
        DisplayRecordCount()
        ShowAlertMessage("Search finished. Found " & IIf(Me.DataGridView.RowCount = 1, "1 Record", Me.DataGridView.RowCount & " Records"))
        Application.DoEvents()

    End Sub

    Public Function GenerateSQL() As String

        On Error Resume Next
        GenerateSQL = "Select * from profile"

        Dim sName = txtName.Text
        Dim sAlias1 = sName
        Dim sAlias2 = sName
        Dim sFatherName = Me.txtFathersName.Text
        Dim sAliasFathers As String = sFatherName
        Dim sHenryClass As String = Me.txtHenryClass.Text


        If SearchSetting = 1 Then
            If sName = vbNullString Then sName = "%"
            If sAlias1 = vbNullString Then sAlias1 = "%"
            If sAlias2 = vbNullString Then sAlias2 = "%"
            If sHenryClass = vbNullString Then sHenryClass = "%"
            If sFatherName = vbNullString Then sFatherName = "%"
            If sAliasFathers = vbNullString Then sAliasFathers = "%"
        End If


        Select Case SearchSetting
            Case 0 'begins with

                GenerateSQL = "select * from profile where (Name like '" & sName & "%' or aliasN1 like '" & sAlias1 & "%' or aliasN2 like '" & sAlias2 & "%') and (fathersname like '" & sFatherName & "%' or aliasfathers1 like '" & sAliasFathers & "%') and henryclass like '" & sHenryClass & "%'"
            Case 1 'full text

                GenerateSQL = "select * from profile where (Name like '" & sName & "' or aliasN1 like '" & sAlias1 & "' or aliasN2 like '" & sAlias2 & "') and (fathersname like '" & sFatherName & "' or aliasfathers1 like '" & sAliasFathers & "') and henryclass like '" & sHenryClass & "'"
            Case 2 'any where

                GenerateSQL = "select * from profile where (Name like '%" & sName & "%' or aliasN1 like '%" & sAlias1 & "%' or aliasN2 like '%" & sAlias2 & "%') and (fathersname like '%" & sFatherName & "%' or aliasfathers1 like '%" & sAliasFathers & "%') and henryclass like '%" & sHenryClass & "%'"

            Case Else


        End Select
    End Function




    Private Sub FindByTIN() Handles btnFindByTIN.Click
        On Error Resume Next
        If Me.txtTIN.Text = vbNullString Then
            MessageBoxEx.Show("Please enter the TIN!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtTIN.Focus()
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(ConString)
        con.Open()
        Dim cmd As OleDb.OleDbCommand = New OleDb.OleDbCommand("select * from profile where TIN like '" & Me.txtTIN.Text & "'", con)
        Dim da As New OleDb.OleDbDataAdapter(cmd)
        Me.FPBDataSet.profile.Clear()
        da.Fill(Me.FPBDataSet.profile)
        con.Close()
        Me.Cursor = Cursors.Default
        DisplayRecordCount()
        ShowAlertMessage("Search finished. Found " & IIf(Me.DataGridView.RowCount = 1, "1 Record", Me.DataGridView.RowCount & " Records"))
        Application.DoEvents()
    End Sub


    Private Sub FindByRCN() Handles btnFindByRCN.Click
        On Error Resume Next
        If Me.txtRCN.Text = vbNullString Then
            MessageBoxEx.Show("Please enter the RCN!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtRCN.Focus()
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(ConString)
        con.Open()
        Dim cmd As OleDb.OleDbCommand = New OleDb.OleDbCommand("select * from profile where RCN like '" & Me.txtRCN.Text & "'", con)
        Dim da As New OleDb.OleDbDataAdapter(cmd)
        Me.FPBDataSet.profile.Clear()
        da.Fill(Me.FPBDataSet.profile)
        con.Close()
        Me.Cursor = Cursors.Default
        DisplayRecordCount()
        ShowAlertMessage("Search finished. Found " & IIf(Me.DataGridView.RowCount = 1, "1 Record", Me.DataGridView.RowCount & " Records"))
        Application.DoEvents()
    End Sub


    Private Sub ShowSearchCondition(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowSearchcondition.Click
        On Error Resume Next
        SetOVStatus()
        SetSexStatus()

        Dim sTIN = txtTIN.Text
        Dim sRCN = txtRCN.Text
        Dim sName = txtName.Text
        Dim sAlias1 = txtAlias1.Text
        Dim sAlias2 = txtAlias2.Text
        Dim sBirthYear = txtBirthYear.Text
        Dim sAddress1 = txtAddress1.Text
        Dim sAddress2 = txtAddress2.Text
        Dim sPCN = txtDCKD.Text
        Dim sFathersName = txtFathersName.Text
        Dim sFathersAlias = txtFathersAlias.Text
        Dim sHenryClass = txtHenryClass.Text
        Dim sDescription = txtDescription.Text


        If rdoMale.Checked = True Then Sex = "Male"
        If rdoFemale.Checked = True Then Sex = "Female"
        If rdoBothSex.Checked = True Then Sex = "%"

        Dim OV As String = ""

        If rdoOV.Checked = True Then OV = "  YES"
        If rdoNotOV.Checked = True Then OV = " NO"
        If rdoBothOV.Checked = True Then OV = " '%'"

        If sHenryClass = vbNullString Then
            sHenryClass = "HenryClass Like '%'"
        Else
            If sHenryClass.Contains("%") Or sHenryClass.Contains("_") Then
                sHenryClass = "HenryClass Like '" & sHenryClass & "'"
            Else
                sHenryClass = "instr(1, HenryClass, '" & sHenryClass & "', 0)>0 "
            End If
        End If

        If SearchSetting = 0 Then 'begins with
            sTIN += "%" 'append % to end
            sRCN += "%"
            sName += "%"
            sAlias1 += "%"
            sAlias2 += "%"
            sBirthYear += "%"
            sAddress1 += "%"
            sAddress2 += "%"
            sPCN += "%"
            sFathersName += "%"
            sFathersAlias += "%"
            sDescription += "%"
        End If

        If SearchSetting = 1 Then 'full text
            If sTIN = vbNullString Then sTIN = "%"
            If sRCN = vbNullString Then sRCN = "%"
            If sName = vbNullString Then sName = "%"
            If sAlias1 = vbNullString Then sAlias1 = "%"
            If sAlias2 = vbNullString Then sAlias2 = "%"
            If sBirthYear = vbNullString Then sBirthYear = "%"
            If sAddress1 = vbNullString Then sAddress1 = "%"
            If sAddress2 = vbNullString Then sAddress2 = "%"
            If sPCN = vbNullString Then sPCN = "%"
            If sFathersName = vbNullString Then sFathersName = "%"
            If sFathersAlias = vbNullString Then sFathersAlias = "%"
            If sDescription = vbNullString Then sDescription = "%"
        End If




        If SearchSetting = 2 Then 'any where
            sTIN = "%" & sTIN & "%" 'append % to end
            sRCN = "%" & sRCN & "%"
            sName = "%" & sName & "%"
            sAlias1 = "%" & sAlias1 & "%"
            sAlias2 = "%" & sAlias2 & "%"
            sBirthYear = "%" & sBirthYear & "%"
            sAddress1 = "%" & sAddress1 & "%"
            sAddress2 = "%" & sAddress2 & "%"
            sPCN = "%" & sPCN & "%"
            sFathersName = "%" & sFathersName & "%"
            sFathersAlias = "%" & sFathersAlias & "%"
            sDescription = "%" & sDescription & "%"
        End If

        Dim SQLText As String = "Select * from PROFILE order by Name"


        If Me.dtLastTrace.Text = "" Then
            SQLText = "Select * from PROFILE where TIN LIKE '" & sTIN & "' AND RCN LIKE '" & sRCN & "' AND PCN LIKE '" & sPCN & "' AND Name LIKE '" & sName & "' AND AliasN1 LIKE '" & sAlias1 & "' AND AliasN2 LIKE '" & sAlias2 & "' AND FathersName LIKE '" & sFathersName & "' AND AliasFathers1 LIKE '" & sFathersAlias & "' AND Sex LIKE '" & Sex & "' AND BirthYear LIKE '" & sBirthYear & "' AND Address1 LIKE '" & sAddress1 & "' AND Address2 LIKE '" & sAddress2 & "' AND " & sHenryClass & " AND Description LIKE '" & sDescription & "' AND OV LIKE " & OV
        End If




        If Me.dtLastTrace.Text <> "" Then
            SQLText = "Select * from PROFILE where TIN LIKE '" & sTIN & "' AND RCN LIKE '" & sRCN & "' AND PCN LIKE '" & sPCN & "' AND Name LIKE '" & sName & "' AND AliasN1 LIKE '" & sAlias1 & "' AND AliasN2 LIKE '" & sAlias2 & "' AND FathersName LIKE '" & sFathersName & "' AND AliasFathers1 LIKE '" & sFathersAlias & "' AND Sex LIKE '" & Sex & "' AND BirthYear LIKE '" & sBirthYear & "' AND Address1 LIKE '" & sAddress1 & "' AND Address2 LIKE '" & sAddress2 & "' AND " & sHenryClass & " AND LatestTraceDate = #" & dtLastTrace.Value & "# AND Description LIKE '" & sDescription & "' AND OV LIKE " & OV
        End If
        SQLText = SQLText.Replace("%%", "%")

        frmSearchCriteria.WindowState = FormWindowState.Normal
        frmSearchCriteria.StartPosition = FormStartPosition.CenterScreen
        frmSearchCriteria.txtSQL.Text = SQLText
        frmSearchCriteria.Show()
    End Sub


    Private Sub ShowAdvancedSearch() Handles btnAdvancedSearch.Click
        On Error Resume Next
        FrmAdvancedSearch.WindowState = FormWindowState.Normal
        FrmAdvancedSearch.StartPosition = FormStartPosition.CenterScreen
        FrmAdvancedSearch.Show()
        FrmAdvancedSearch.BringToFront()
    End Sub
#End Region



#Region " AUTO COMPLETION WORDS"


    Sub AddToAutoFillWordsList()
        On Error Resume Next
        If Trim(Me.txtName.Text) <> vbNullString And chkName.Checked Then Me.txtName.AutoCompleteCustomSource.Add(Trim(Me.txtName.Text))
        If Trim(Me.txtAlias1.Text) <> vbNullString And chkAlias1.Checked Then Me.txtAlias1.AutoCompleteCustomSource.Add(Trim(Me.txtAlias1.Text))
        If Trim(Me.txtAlias2.Text) <> vbNullString And chkAlias2.Checked Then Me.txtAlias2.AutoCompleteCustomSource.Add(Trim(Me.txtAlias2.Text))
        If Trim(Me.txtFathersAlias.Text) <> vbNullString And chkFatherAlias.Checked Then Me.txtFathersAlias.AutoCompleteCustomSource.Add(Trim(Me.txtFathersAlias.Text))
        If Trim(Me.txtFathersName.Text) <> vbNullString And chkFatherName.Checked Then Me.txtFathersName.AutoCompleteCustomSource.Add(Trim(Me.txtFathersName.Text))
        If Trim(Me.txtHenryClass.Text) <> vbNullString And chkHenry.Checked Then Me.txtHenryClass.AutoCompleteCustomSource.Add(Trim(Me.txtHenryClass.Text))
    End Sub




    Private Sub SetAutoFields(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkName.Click, chkAlias1.Click, chkAlias2.Click, chkFatherAlias.Click, chkFatherName.Click, chkHenry.Click
        On Error Resume Next
        Select Case DirectCast(sender, DevComponents.DotNetBar.CheckBoxItem).Name
            Case chkName.Name
                If chkName.Checked Then
                    My.Computer.Registry.SetValue(RegistrySettingsPath, "CName", 1, Microsoft.Win32.RegistryValueKind.String)
                Else
                    My.Computer.Registry.SetValue(RegistrySettingsPath, "CName", 0, Microsoft.Win32.RegistryValueKind.String)
                    Me.txtName.AutoCompleteCustomSource.Clear()
                End If


            Case chkAlias1.Name

                If chkAlias1.Checked Then

                    My.Computer.Registry.SetValue(RegistrySettingsPath, "Alias1", 1, Microsoft.Win32.RegistryValueKind.String)
                Else

                    My.Computer.Registry.SetValue(RegistrySettingsPath, "Alias1", 0, Microsoft.Win32.RegistryValueKind.String)
                    Me.txtAlias1.AutoCompleteCustomSource.Clear()

                End If


            Case chkAlias2.Name
                If chkAlias2.Checked Then

                    My.Computer.Registry.SetValue(RegistrySettingsPath, "Alias2", 1, Microsoft.Win32.RegistryValueKind.String)
                Else

                    My.Computer.Registry.SetValue(RegistrySettingsPath, "Alias2", 0, Microsoft.Win32.RegistryValueKind.String)
                    Me.txtAlias2.AutoCompleteCustomSource.Clear()

                End If

            Case chkFatherName.Name
                If chkFatherName.Checked Then

                    My.Computer.Registry.SetValue(RegistrySettingsPath, "FatherName", 1, Microsoft.Win32.RegistryValueKind.String)
                Else

                    My.Computer.Registry.SetValue(RegistrySettingsPath, "FatherName", 0, Microsoft.Win32.RegistryValueKind.String)
                    Me.txtFathersName.AutoCompleteCustomSource.Clear()

                End If
            Case chkFatherAlias.Name
                If chkFatherAlias.Checked Then

                    My.Computer.Registry.SetValue(RegistrySettingsPath, "FatherAlias", 1, Microsoft.Win32.RegistryValueKind.String)
                Else

                    My.Computer.Registry.SetValue(RegistrySettingsPath, "FatherAlias", 0, Microsoft.Win32.RegistryValueKind.String)
                    Me.txtFathersAlias.AutoCompleteCustomSource.Clear()
                End If

            Case chkHenry.Name
                If chkHenry.Checked Then

                    My.Computer.Registry.SetValue(RegistrySettingsPath, "Henry", 1, Microsoft.Win32.RegistryValueKind.String)
                Else

                    My.Computer.Registry.SetValue(RegistrySettingsPath, "Henry", 0, Microsoft.Win32.RegistryValueKind.String)
                    Me.txtHenryClass.AutoCompleteCustomSource.Clear()
                End If
        End Select



    End Sub




    Private Sub GetAutoFields()
        On Error Resume Next
        Me.chkName.Checked = My.Computer.Registry.GetValue(RegistrySettingsPath, "CName", 1)
        Me.chkAlias1.Checked = My.Computer.Registry.GetValue(RegistrySettingsPath, "Alias1", 1)
        Me.chkAlias2.Checked = My.Computer.Registry.GetValue(RegistrySettingsPath, "Alias2", 1)
        Me.chkFatherName.Checked = My.Computer.Registry.GetValue(RegistrySettingsPath, "FatherName", 1)
        Me.chkFatherAlias.Checked = My.Computer.Registry.GetValue(RegistrySettingsPath, "FatherAlias", 1)
        Me.chkHenry.Checked = My.Computer.Registry.GetValue(RegistrySettingsPath, "Henry", 0)
    End Sub



    Private Sub LoadAutoCompleteValues(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoadAutoCompleteValues.Click
        On Error Resume Next
        ReLoadAutoText()

        ShowAlertMessage("Loaded auto completion list!")
    End Sub


    Private Sub ClearAutoCompletionList()
        On Error Resume Next
        Me.txtName.AutoCompleteCustomSource.Clear()
        Me.txtAlias1.AutoCompleteCustomSource.Clear()
        Me.txtAlias2.AutoCompleteCustomSource.Clear()
        Me.txtFathersAlias.AutoCompleteCustomSource.Clear()
        Me.txtFathersName.AutoCompleteCustomSource.Clear()
        Me.txtHenryClass.AutoCompleteCustomSource.Clear()

    End Sub



    Private Sub ClearAutoCompletionListManually(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAutoCompletion.Click
        On Error Resume Next
        ClearAutoCompletionList()
        ShowAlertMessage("Auto completion list cleared!")
    End Sub


    Private Sub SetAutoCompleteMode() Handles cmbAutoCompletionMode.SelectedIndexChanged
        On Error Resume Next
        Dim mode As Integer = Me.cmbAutoCompletionMode.SelectedIndex
        Me.txtName.AutoCompleteMode = mode
        Me.txtAlias1.AutoCompleteMode = mode
        Me.txtAlias2.AutoCompleteMode = mode
        Me.txtFathersAlias.AutoCompleteMode = mode
        Me.txtFathersName.AutoCompleteMode = mode
        Me.txtHenryClass.AutoCompleteMode = mode
        My.Computer.Registry.SetValue(RegistrySettingsPath, "AutoCompleteMode", mode, Microsoft.Win32.RegistryValueKind.String)
    End Sub


    Private Sub LoadAutoTextFromDB() Handles chkLoadAutoText.Click
        On Error Resume Next
        Dim s As Boolean = chkLoadAutoText.Checked
        Dim v As Integer
        If s Then v = 1 Else v = 0

        My.Computer.Registry.SetValue(RegistrySettingsPath, "LoadAutoTextFromDB", v, Microsoft.Win32.RegistryValueKind.String)
    End Sub



    Private Sub ReLoadAutoText()
        On Error Resume Next
        Me.ProgressBar.Visible = True
        Me.StatusBar.RecalcLayout()
        Me.Cursor = Cursors.WaitCursor

        ClearAutoCompletionList()
        If Me.chkName.Checked Then
            Me.AutoTextAdapter.FillByName(AutoTextDataSet.profile)
            Me.ProgressBar.Maximum = AutoTextDataSet.profile.Count - 1
            Me.ProgressBar.Text = "Loading Auto Completion Values - Name"
            Me.ProgressBar.Value = 0
            For i As Long = 0 To AutoTextDataSet.profile.Count - 1
                Me.txtName.AutoCompleteCustomSource.Add(AutoTextDataSet.profile(i).Name)
                Me.ProgressBar.Increment(1)
                Application.DoEvents()
            Next (i)
        End If

        If Me.chkAlias1.Checked Then
            Me.AutoTextAdapter.FillByAlias1(AutoTextDataSet.profile)
            Me.ProgressBar.Maximum = AutoTextDataSet.profile.Count - 1
            Me.ProgressBar.Text = "Loading Auto Completion Values - Alias 1"
            Me.ProgressBar.Value = 0
            For i As Long = 0 To AutoTextDataSet.profile.Count - 1
                Me.txtAlias1.AutoCompleteCustomSource.Add(AutoTextDataSet.profile(i).AliasN1)
                Me.ProgressBar.Increment(1)
                Application.DoEvents()
            Next (i)
        End If

        If chkAlias2.Checked Then

            Me.AutoTextAdapter.FillByAlias2(AutoTextDataSet.profile)
            Me.ProgressBar.Maximum = AutoTextDataSet.profile.Count - 1
            Me.ProgressBar.Text = "Loading Auto Completion Values - Alias 2"
            Me.ProgressBar.Value = 0
            For i As Long = 0 To AutoTextDataSet.profile.Count - 1
                Me.txtAlias2.AutoCompleteCustomSource.Add(AutoTextDataSet.profile(i).AliasN2)
                Me.ProgressBar.Increment(1)
                Application.DoEvents()
            Next (i)
        End If


        If chkFatherName.Checked Then
            Me.AutoTextAdapter.FillByFatherName(AutoTextDataSet.profile)
            Me.ProgressBar.Maximum = AutoTextDataSet.profile.Count - 1
            Me.ProgressBar.Text = "Loading Auto Completion Values - Father's Name"
            Me.ProgressBar.Value = 0
            For i As Long = 0 To AutoTextDataSet.profile.Count - 1
                Me.txtFathersName.AutoCompleteCustomSource.Add(AutoTextDataSet.profile(i).FathersName)
                Me.ProgressBar.Increment(1)
                Application.DoEvents()
            Next (i)
        End If

        If chkFatherAlias.Checked Then
            Me.AutoTextAdapter.FillByFathersAlias(AutoTextDataSet.profile)
            Me.ProgressBar.Maximum = AutoTextDataSet.profile.Count - 1
            Me.ProgressBar.Text = "Loading Auto Completion Values - Father's Alias"
            Me.ProgressBar.Value = 0
            For i As Long = 0 To AutoTextDataSet.profile.Count - 1
                Me.txtFathersAlias.AutoCompleteCustomSource.Add(AutoTextDataSet.profile(i).AliasFathers1)
                Me.ProgressBar.Increment(1)
                Application.DoEvents()
            Next (i)
        End If

        If chkHenry.Checked Then
            Me.AutoTextAdapter.FillByHenry(AutoTextDataSet.profile)
            Me.ProgressBar.Maximum = AutoTextDataSet.profile.Count - 1
            Me.ProgressBar.Text = "Loading Auto Completion Values - Henry Classification"
            Me.ProgressBar.Value = 0
            For i As Long = 0 To AutoTextDataSet.profile.Count - 1
                Me.txtHenryClass.AutoCompleteCustomSource.Add(AutoTextDataSet.profile(i).HenryClass)
                Me.ProgressBar.Increment(1)
                Application.DoEvents()
            Next (i)
        End If

        Me.ProgressBar.Visible = False
        Me.Cursor = Cursors.Default

    End Sub

#End Region



#Region "PRINT REPORTS"

    Private Sub PrintSelectedItem() Handles btnPrintSelectedItem.Click
        On Error Resume Next
        If Me.DataGridView.RowCount = 0 Then
            MessageBoxEx.Show("No data to print!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Exit Sub
        End If
        If Me.DataGridView.SelectedRows.Count = 0 Then
            MessageBoxEx.Show("No data selected!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Exit Sub
        End If
        frmView.profileTableAdapter.FillByTIN(frmView.FPBDataSet.profile, Me.DataGridView.SelectedCells(0).Value.ToString)

        frmView.Show()
        frmView.WindowState = FormWindowState.Maximized
        frmView.LoadData()
    End Sub


    Private Sub PrintAll() Handles btnPrintAll.Click
        On Error Resume Next
        frmViewAll.profileTableAdapter.Fill(frmViewAll.FPBDataSet.profile)
        frmViewAll.Show()
    End Sub


    Private Sub PrintCurrentRecords() Handles btnPrint.Click, btnPrintMenu.Click
        On Error Resume Next
        frmViewAll.profileBindingSource.DataSource = Me.ProfileBindingSource.DataSource
        frmViewAll.Show()
    End Sub

#End Region


#Region "HELP"


    Private Sub ShowAboutDialog() Handles btnInfo.Click
        On Error Resume Next
        frmAbout.ShowDialog()
    End Sub


    Private Sub ShowHelp() Handles btnHelp.Click
        On Error Resume Next
        If FileIO.FileSystem.FileExists(AppPath & "\Help.chm") Then
            Call Shell("hh.exe  " & AppPath & "\Help.chm", vbNormalFocus)
        Else
            ShowAlertMessage("Self learning is the best learning!")
        End If
    End Sub

#End Region


#Region "END APPLICATION"



    Sub EndApplication() Handles btnExit.Click, MyBase.FormClosed, btnExitMenu1.Click, btnExitMenu2.Click
        On Error Resume Next
        SaveDatagridColumnWidth()
        SaveDatagridColumnOrder()
        SaveQuicktoolbarSettings()
        ProfileTableAdapter.Connection.Close()
        objMutex.ReleaseMutex()
        objMutex.Close()
        objMutex = Nothing
        End
    End Sub
#End Region



#Region "RANDOM DATA"

    Sub generaterandomdata() 'Handles btnNewEntry.Click
        For i As Long = 6000 To 10000
            Me.ProfileTableAdapter.Insert(i, i, Trim(txtName.Text) & i, Trim(txtAlias1.Text) & i, Trim(txtAlias2.Text) & i, Trim(Sex) & i, Trim(txtBirthYear.Text) & i, Trim(txtAddress1.Text) & i, Trim(txtAddress2.Text) & i, Trim(txtDCKD.Text) & i, Trim(txtFathersName.Text) & i, Trim(txtFathersAlias.Text) & i, Trim(txtHenryClass.Text) & i, Trim(txtDescription.Text) & i, dtLastTrace.ValueObject, True, FPSlipImageFile)
        Next
    End Sub
#End Region


#Region "OPEN DATABASE"

    Private Sub OpenDBLocation() Handles btnOpenDBFolder.Click
        On Error Resume Next
        Dim DBPath = AppPath & "\FPB.mdb"

        If FileIO.FileSystem.FileExists(DBPath) Then

            Call Shell("explorer.exe /select," & DBPath, AppWinStyle.NormalFocus)
        Else
            MessageBoxEx.Show("The FPB.mdb file does not exist!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

    End Sub


    Private Sub OpenDBInAccess() Handles btnOpenDBInAccess.Click
        On Error Resume Next
        Dim DBPath = AppPath & "\FPB.mdb"

        If FileIO.FileSystem.FileExists(DBPath) Then
            Shell("explorer.exe " & DBPath, AppWinStyle.MaximizedFocus)

        Else
            MessageBoxEx.Show("The FPB.mdb file does not exist!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

    End Sub


#End Region

#Region "PASSWORD"

    Private Sub ChangePassword() Handles btnPassword.Click
        On Error Resume Next
        frmInputBox.SetTitleandMessage("User Authentication Required", "Please enter the current password.")
        frmInputBox.AcceptButton = frmInputBox.btnOK
        frmInputBox.txtInputBox.UseSystemPasswordChar = True
        frmInputBox.ShowDialog()
        If frmInputBox.ButtonClicked <> "OK" Then Exit Sub
        If frmInputBox.txtInputBox.Text <> Password Then
            MessageBoxEx.Show("Wrong password!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim OldPassword As String = Password
        FrmResetPassword.txtPassword.Focus()
        FrmResetPassword.ShowDialog()

        If PasswordChanged Then
            ChangeDBPassword(OldPassword, Password)
            ShowAlertMessage("Password changed!")
        End If

    End Sub

    Private Sub ChangeDBPassword(ByVal OldPassword As String, ByVal NewPassWord As String)
        Try
            Me.Cursor = Cursors.WaitCursor
            CloseConnectionToDatabse()
            BackupDatabaseBeforePasswordChange()
            Dim CS = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DBPath & "; Jet OLEDB:Database Password=" & OldPassword & "; Mode=Share Deny Read | Share Deny Write;"
            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(CS)
            con.Open()
            Dim sql As String = "ALTER DATABASE PASSWORD [" & NewPassWord & "] [" & OldPassword & "]"
            Dim cmd = New OleDb.OleDbCommand(sql, con)
            cmd.ExecuteNonQuery()
            con.Close()
            ConnectToDatabase()
        Catch ex As Exception
            MessageBoxEx.Show(ex.Message, AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.Cursor = Cursors.Default
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub LockOrUnLock() Handles btnLock.Click
        On Error Resume Next
        If Me.btnLock.Text = "Lock" Then
            Me.btnEdit.Enabled = False
            Me.btnEditContextMenu.Enabled = False
            Me.btnDelete.Enabled = False
            Me.btnDeleteContextMenu.Enabled = False
            ' Me.btnRestore.Enabled = False
            ' Me.btnRestoreMenu.Enabled = False
            Me.btnLock.Text = "Unlock"
            ShowAlertMessage("Locked")
            Exit Sub
        End If

        If Me.btnLock.Text = "Unlock" Then
            frmInputBox.SetTitleandMessage("User Authentication Required", "Please enter password to unlock.")
            frmInputBox.AcceptButton = frmInputBox.btnOK
            frmInputBox.txtInputBox.UseSystemPasswordChar = True
            frmInputBox.ShowDialog()
            If frmInputBox.ButtonClicked <> "OK" Then Exit Sub
            If frmInputBox.txtInputBox.Text <> Password Then
                MessageBoxEx.Show("Wrong password!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            Me.btnEdit.Enabled = True
            Me.btnEditContextMenu.Enabled = True
            Me.btnDelete.Enabled = True
            Me.btnDeleteContextMenu.Enabled = True
            ' Me.btnRestore.Enabled = True
            ' Me.btnRestoreMenu.Enabled = True
            Me.btnLock.Text = "Lock"
            ShowAlertMessage("Unlocked")
        End If
        Me.StatusBar.RecalcLayout()
    End Sub

    Private Sub BackupDatabaseBeforePasswordChange()
        On Error Resume Next
        Dim Source As String = AppPath & "\FPB.mdb"

        Dim Destination As String = My.Computer.Registry.GetValue(RegistrySettingsPath, "BackupPath", FileIO.SpecialDirectories.MyDocuments & "\BXSofts\" & AppName & "\Backups")

        My.Computer.Registry.SetValue(RegistrySettingsPath, "BackupPath", Destination, Microsoft.Win32.RegistryValueKind.String)

        If My.Computer.FileSystem.DirectoryExists(Destination) = False Then
            My.Computer.FileSystem.CreateDirectory(Destination)
        End If
        If Strings.Right(Destination, 1) <> "\" Then Destination = Destination & "\"

        Dim BackupFileName As String = "FPBBackup-BeforeChangingPasswordOn-" & Strings.Format(Now, "dd-MM-yyyy-h-mm-ss-tt") & ".fpb"

        Destination = Destination & BackupFileName
        My.Computer.FileSystem.CopyFile(Source, Destination, True)
    End Sub

#End Region

  
End Class