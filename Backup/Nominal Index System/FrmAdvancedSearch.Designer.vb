<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAdvancedSearch
    Inherits DevComponents.DotNetBar.Office2007Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAdvancedSearch))
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.listViewEx1 = New DevComponents.DotNetBar.Controls.ListViewEx
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx
        Me.dtDummy = New DevComponents.Editors.DateTimeAdv.DateTimeInput
        Me.btnClearAllFields = New DevComponents.DotNetBar.ButtonX
        Me.btnDeselectAll = New DevComponents.DotNetBar.ButtonX
        Me.btnSelectAll = New DevComponents.DotNetBar.ButtonX
        Me.ContextMenuBar1 = New DevComponents.DotNetBar.ContextMenuBar
        Me.ButtonItem1 = New DevComponents.DotNetBar.ButtonItem
        Me.ButtonItem2 = New DevComponents.DotNetBar.ButtonItem
        Me.btn0 = New DevComponents.DotNetBar.ButtonItem
        Me.btn1 = New DevComponents.DotNetBar.ButtonItem
        Me.btn2 = New DevComponents.DotNetBar.ButtonItem
        Me.btn3 = New DevComponents.DotNetBar.ButtonItem
        Me.btn4 = New DevComponents.DotNetBar.ButtonItem
        Me.btn5 = New DevComponents.DotNetBar.ButtonItem
        Me.btn6 = New DevComponents.DotNetBar.ButtonItem
        Me.btn7 = New DevComponents.DotNetBar.ButtonItem
        Me.btn8 = New DevComponents.DotNetBar.ButtonItem
        Me.btn9 = New DevComponents.DotNetBar.ButtonItem
        Me.btn10 = New DevComponents.DotNetBar.ButtonItem
        Me.btn11 = New DevComponents.DotNetBar.ButtonItem
        Me.btn12 = New DevComponents.DotNetBar.ButtonItem
        Me.btn13 = New DevComponents.DotNetBar.ButtonItem
        Me.btn14 = New DevComponents.DotNetBar.ButtonItem
        Me.btn15 = New DevComponents.DotNetBar.ButtonItem
        Me.btn16 = New DevComponents.DotNetBar.ButtonItem
        Me.ButtonItem6 = New DevComponents.DotNetBar.ButtonItem
        Me.ButtonItem8 = New DevComponents.DotNetBar.ButtonItem
        Me.ButtonItem10 = New DevComponents.DotNetBar.ButtonItem
        Me.ButtonItem11 = New DevComponents.DotNetBar.ButtonItem
        Me.ButtonItem12 = New DevComponents.DotNetBar.ButtonItem
        Me.ButtonItem13 = New DevComponents.DotNetBar.ButtonItem
        Me.ButtonItem14 = New DevComponents.DotNetBar.ButtonItem
        Me.ButtonItem15 = New DevComponents.DotNetBar.ButtonItem
        Me.ButtonItem7 = New DevComponents.DotNetBar.ButtonItem
        Me.ButtonItem9 = New DevComponents.DotNetBar.ButtonItem
        Me.ButtonItem16 = New DevComponents.DotNetBar.ButtonItem
        Me.btnUndo = New DevComponents.DotNetBar.ButtonItem
        Me.btnCut = New DevComponents.DotNetBar.ButtonItem
        Me.btnCopy = New DevComponents.DotNetBar.ButtonItem
        Me.btnPaste = New DevComponents.DotNetBar.ButtonItem
        Me.btnDelete = New DevComponents.DotNetBar.ButtonItem
        Me.btnSelectAllText = New DevComponents.DotNetBar.ButtonItem
        Me.txtSQL = New DevComponents.DotNetBar.Controls.TextBoxX
        Me.chkWildCharacter = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.DataGridViewX1 = New DevComponents.DotNetBar.Controls.DataGridViewX
        Me.btnSearch = New DevComponents.DotNetBar.ButtonX
        Me.btnGenerateSQL = New DevComponents.DotNetBar.ButtonX
        Me.PanelEx1.SuspendLayout()
        CType(Me.dtDummy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ContextMenuBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'listViewEx1
        '
        Me.listViewEx1.Alignment = System.Windows.Forms.ListViewAlignment.Left
        '
        '
        '
        Me.listViewEx1.Border.Class = "ListViewBorder"
        Me.listViewEx1.CheckBoxes = True
        Me.listViewEx1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.listViewEx1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listViewEx1.FullRowSelect = True
        Me.listViewEx1.GridLines = True
        Me.listViewEx1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.listViewEx1.LabelEdit = True
        Me.listViewEx1.Location = New System.Drawing.Point(12, 14)
        Me.listViewEx1.MultiSelect = False
        Me.listViewEx1.Name = "listViewEx1"
        Me.listViewEx1.ShowItemToolTips = True
        Me.listViewEx1.Size = New System.Drawing.Size(268, 435)
        Me.listViewEx1.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.listViewEx1.TabIndex = 1
        Me.listViewEx1.TabStop = False
        Me.listViewEx1.UseCompatibleStateImageBehavior = False
        Me.listViewEx1.View = System.Windows.Forms.View.Details
        Me.listViewEx1.Visible = False
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Value"
        Me.ColumnHeader1.Width = 250
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.dtDummy)
        Me.PanelEx1.Controls.Add(Me.btnClearAllFields)
        Me.PanelEx1.Controls.Add(Me.btnDeselectAll)
        Me.PanelEx1.Controls.Add(Me.btnSelectAll)
        Me.PanelEx1.Controls.Add(Me.ContextMenuBar1)
        Me.PanelEx1.Controls.Add(Me.chkWildCharacter)
        Me.PanelEx1.Controls.Add(Me.DataGridViewX1)
        Me.PanelEx1.Controls.Add(Me.listViewEx1)
        Me.PanelEx1.Controls.Add(Me.btnSearch)
        Me.PanelEx1.Controls.Add(Me.btnGenerateSQL)
        Me.PanelEx1.Controls.Add(Me.txtSQL)
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(921, 719)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 3
        '
        'dtDummy
        '
        Me.dtDummy.AutoAdvance = True
        Me.dtDummy.AutoSelectDate = True
        Me.dtDummy.BackColor = System.Drawing.SystemColors.Window
        '
        '
        '
        Me.dtDummy.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.dtDummy.ButtonClear.Image = CType(resources.GetObject("dtDummy.ButtonClear.Image"), System.Drawing.Image)
        Me.dtDummy.ButtonClear.Visible = True
        Me.dtDummy.ButtonDropDown.Visible = True
        Me.dtDummy.CustomFormat = "dd/MM/yyyy"
        Me.dtDummy.FocusHighlightEnabled = True
        Me.dtDummy.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtDummy.Format = DevComponents.Editors.eDateTimePickerFormat.Custom
        Me.dtDummy.Location = New System.Drawing.Point(785, 284)
        Me.dtDummy.MaxDate = New Date(2100, 12, 31, 0, 0, 0, 0)
        Me.dtDummy.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        '
        '
        '
        Me.dtDummy.MonthCalendar.AnnuallyMarkedDates = New Date(-1) {}
        '
        '
        '
        Me.dtDummy.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window
        Me.dtDummy.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.dtDummy.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.dtDummy.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.dtDummy.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.dtDummy.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.dtDummy.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.dtDummy.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.dtDummy.MonthCalendar.DisplayMonth = New Date(2008, 7, 1, 0, 0, 0, 0)
        Me.dtDummy.MonthCalendar.MarkedDates = New Date(-1) {}
        Me.dtDummy.MonthCalendar.MonthlyMarkedDates = New Date(-1) {}
        '
        '
        '
        Me.dtDummy.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.dtDummy.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.dtDummy.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.dtDummy.MonthCalendar.TodayButtonVisible = True
        Me.dtDummy.Name = "dtDummy"
        Me.dtDummy.Size = New System.Drawing.Size(108, 29)
        Me.dtDummy.TabIndex = 106
        Me.dtDummy.Visible = False
        '
        'btnClearAllFields
        '
        Me.btnClearAllFields.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnClearAllFields.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnClearAllFields.Location = New System.Drawing.Point(785, 191)
        Me.btnClearAllFields.Name = "btnClearAllFields"
        Me.btnClearAllFields.Size = New System.Drawing.Size(118, 58)
        Me.btnClearAllFields.TabIndex = 9
        Me.btnClearAllFields.Text = "Clear"
        '
        'btnDeselectAll
        '
        Me.btnDeselectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnDeselectAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnDeselectAll.Location = New System.Drawing.Point(785, 103)
        Me.btnDeselectAll.Name = "btnDeselectAll"
        Me.btnDeselectAll.Size = New System.Drawing.Size(118, 58)
        Me.btnDeselectAll.TabIndex = 8
        Me.btnDeselectAll.Text = "Deselect All"
        '
        'btnSelectAll
        '
        Me.btnSelectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSelectAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnSelectAll.Location = New System.Drawing.Point(785, 14)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(118, 58)
        Me.btnSelectAll.TabIndex = 7
        Me.btnSelectAll.Text = "Select All"
        '
        'ContextMenuBar1
        '
        Me.ContextMenuBar1.DockSide = DevComponents.DotNetBar.eDockSide.Document
        Me.ContextMenuBar1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ContextMenuBar1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ButtonItem1})
        Me.ContextMenuBar1.Location = New System.Drawing.Point(781, 352)
        Me.ContextMenuBar1.Name = "ContextMenuBar1"
        Me.ContextMenuBar1.Size = New System.Drawing.Size(126, 25)
        Me.ContextMenuBar1.Stretch = True
        Me.ContextMenuBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.ContextMenuBar1.TabIndex = 6
        Me.ContextMenuBar1.TabStop = False
        Me.ContextMenuBar1.Text = "ContextMenuBar1"
        '
        'ButtonItem1
        '
        Me.ButtonItem1.AutoExpandOnClick = True
        Me.ButtonItem1.ImagePaddingHorizontal = 8
        Me.ButtonItem1.Name = "ButtonItem1"
        Me.ButtonItem1.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ButtonItem2, Me.ButtonItem6, Me.ButtonItem7, Me.btnUndo, Me.btnCut, Me.btnCopy, Me.btnPaste, Me.btnDelete, Me.btnSelectAllText})
        Me.ButtonItem1.Text = "ButtonItem1"
        '
        'ButtonItem2
        '
        Me.ButtonItem2.ImagePaddingHorizontal = 8
        Me.ButtonItem2.Name = "ButtonItem2"
        Me.ButtonItem2.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btn0, Me.btn1, Me.btn2, Me.btn3, Me.btn4, Me.btn5, Me.btn6, Me.btn7, Me.btn8, Me.btn9, Me.btn10, Me.btn11, Me.btn12, Me.btn13, Me.btn14, Me.btn15, Me.btn16})
        Me.ButtonItem2.Text = "Insert Field"
        '
        'btn0
        '
        Me.btn0.ImagePaddingHorizontal = 8
        Me.btn0.Name = "btn0"
        Me.btn0.Text = "Address 1"
        '
        'btn1
        '
        Me.btn1.ImagePaddingHorizontal = 8
        Me.btn1.Name = "btn1"
        Me.btn1.Text = "Address 2"
        '
        'btn2
        '
        Me.btn2.ImagePaddingHorizontal = 8
        Me.btn2.Name = "btn2"
        Me.btn2.Text = "Birth Year"
        '
        'btn3
        '
        Me.btn3.ImagePaddingHorizontal = 8
        Me.btn3.Name = "btn3"
        Me.btn3.Text = "DC/KD/S"
        '
        'btn4
        '
        Me.btn4.ImagePaddingHorizontal = 8
        Me.btn4.Name = "btn4"
        Me.btn4.Text = "Description"
        '
        'btn5
        '
        Me.btn5.ImagePaddingHorizontal = 8
        Me.btn5.Name = "btn5"
        Me.btn5.Text = "Father's Alias"
        '
        'btn6
        '
        Me.btn6.ImagePaddingHorizontal = 8
        Me.btn6.Name = "btn6"
        Me.btn6.Text = "Father's Name"
        '
        'btn7
        '
        Me.btn7.ImagePaddingHorizontal = 8
        Me.btn7.Name = "btn7"
        Me.btn7.Text = "First Alias"
        '
        'btn8
        '
        Me.btn8.ImagePaddingHorizontal = 8
        Me.btn8.Name = "btn8"
        Me.btn8.Text = "Henry Classification"
        '
        'btn9
        '
        Me.btn9.ImagePaddingHorizontal = 8
        Me.btn9.Name = "btn9"
        Me.btn9.Text = "Latest Trace Date"
        '
        'btn10
        '
        Me.btn10.ImagePaddingHorizontal = 8
        Me.btn10.Name = "btn10"
        Me.btn10.Text = "Name"
        '
        'btn11
        '
        Me.btn11.ImagePaddingHorizontal = 8
        Me.btn11.Name = "btn11"
        Me.btn11.Text = "OV Status"
        '
        'btn12
        '
        Me.btn12.ImagePaddingHorizontal = 8
        Me.btn12.Name = "btn12"
        Me.btn12.Text = "RCN"
        '
        'btn13
        '
        Me.btn13.ImagePaddingHorizontal = 8
        Me.btn13.Name = "btn13"
        Me.btn13.Text = "Second Alias"
        '
        'btn14
        '
        Me.btn14.ImagePaddingHorizontal = 8
        Me.btn14.Name = "btn14"
        Me.btn14.Text = "Sex"
        '
        'btn15
        '
        Me.btn15.ImagePaddingHorizontal = 8
        Me.btn15.Name = "btn15"
        Me.btn15.Text = "Slip File"
        '
        'btn16
        '
        Me.btn16.ImagePaddingHorizontal = 8
        Me.btn16.Name = "btn16"
        Me.btn16.Text = "TIN"
        '
        'ButtonItem6
        '
        Me.ButtonItem6.ImagePaddingHorizontal = 8
        Me.ButtonItem6.Name = "ButtonItem6"
        Me.ButtonItem6.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ButtonItem8, Me.ButtonItem10, Me.ButtonItem11, Me.ButtonItem12, Me.ButtonItem13, Me.ButtonItem14, Me.ButtonItem15})
        Me.ButtonItem6.Text = "Insert Operator"
        '
        'ButtonItem8
        '
        Me.ButtonItem8.ImagePaddingHorizontal = 8
        Me.ButtonItem8.Name = "ButtonItem8"
        Me.ButtonItem8.Text = "="
        '
        'ButtonItem10
        '
        Me.ButtonItem10.ImagePaddingHorizontal = 8
        Me.ButtonItem10.Name = "ButtonItem10"
        Me.ButtonItem10.Text = "<>"
        '
        'ButtonItem11
        '
        Me.ButtonItem11.ImagePaddingHorizontal = 8
        Me.ButtonItem11.Name = "ButtonItem11"
        Me.ButtonItem11.Text = "<"
        '
        'ButtonItem12
        '
        Me.ButtonItem12.ImagePaddingHorizontal = 8
        Me.ButtonItem12.Name = "ButtonItem12"
        Me.ButtonItem12.Text = ">"
        '
        'ButtonItem13
        '
        Me.ButtonItem13.ImagePaddingHorizontal = 8
        Me.ButtonItem13.Name = "ButtonItem13"
        Me.ButtonItem13.Text = "<="
        '
        'ButtonItem14
        '
        Me.ButtonItem14.ImagePaddingHorizontal = 8
        Me.ButtonItem14.Name = "ButtonItem14"
        Me.ButtonItem14.Text = ">="
        '
        'ButtonItem15
        '
        Me.ButtonItem15.ImagePaddingHorizontal = 8
        Me.ButtonItem15.Name = "ButtonItem15"
        Me.ButtonItem15.Text = "LIKE"
        '
        'ButtonItem7
        '
        Me.ButtonItem7.ImagePaddingHorizontal = 8
        Me.ButtonItem7.Name = "ButtonItem7"
        Me.ButtonItem7.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ButtonItem9, Me.ButtonItem16})
        Me.ButtonItem7.Text = "Insert Logical Operator"
        '
        'ButtonItem9
        '
        Me.ButtonItem9.ImagePaddingHorizontal = 8
        Me.ButtonItem9.Name = "ButtonItem9"
        Me.ButtonItem9.Text = "AND"
        '
        'ButtonItem16
        '
        Me.ButtonItem16.ImagePaddingHorizontal = 8
        Me.ButtonItem16.Name = "ButtonItem16"
        Me.ButtonItem16.Text = "OR"
        '
        'btnUndo
        '
        Me.btnUndo.BeginGroup = True
        Me.btnUndo.ImagePaddingHorizontal = 8
        Me.btnUndo.Name = "btnUndo"
        Me.btnUndo.Text = "Undo"
        '
        'btnCut
        '
        Me.btnCut.BeginGroup = True
        Me.btnCut.ImagePaddingHorizontal = 8
        Me.btnCut.Name = "btnCut"
        Me.btnCut.Text = "Cut"
        '
        'btnCopy
        '
        Me.btnCopy.ImagePaddingHorizontal = 8
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Text = "Copy"
        '
        'btnPaste
        '
        Me.btnPaste.ImagePaddingHorizontal = 8
        Me.btnPaste.Name = "btnPaste"
        Me.btnPaste.Text = "Paste"
        '
        'btnDelete
        '
        Me.btnDelete.ImagePaddingHorizontal = 8
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Text = "Delete"
        '
        'btnSelectAllText
        '
        Me.btnSelectAllText.BeginGroup = True
        Me.btnSelectAllText.ImagePaddingHorizontal = 8
        Me.btnSelectAllText.Name = "btnSelectAllText"
        Me.btnSelectAllText.Text = "Select All"
        '
        'txtSQL
        '
        '
        '
        '
        Me.txtSQL.Border.Class = "TextBoxBorder"
        Me.ContextMenuBar1.SetContextMenuEx(Me.txtSQL, Me.ButtonItem1)
        Me.txtSQL.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSQL.Location = New System.Drawing.Point(3, 509)
        Me.txtSQL.Multiline = True
        Me.txtSQL.Name = "txtSQL"
        Me.txtSQL.Size = New System.Drawing.Size(762, 198)
        Me.txtSQL.TabIndex = 2
        '
        'chkWildCharacter
        '
        Me.chkWildCharacter.Location = New System.Drawing.Point(782, 521)
        Me.chkWildCharacter.Name = "chkWildCharacter"
        Me.chkWildCharacter.Size = New System.Drawing.Size(127, 23)
        Me.chkWildCharacter.TabIndex = 5
        Me.chkWildCharacter.Text = "Use wild character"
        '
        'DataGridViewX1
        '
        Me.DataGridViewX1.AllowUserToAddRows = False
        Me.DataGridViewX1.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewX1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewX1.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewX1.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.DataGridViewX1.Location = New System.Drawing.Point(3, 3)
        Me.DataGridViewX1.Name = "DataGridViewX1"
        Me.DataGridViewX1.RowTemplate.Height = 30
        Me.DataGridViewX1.Size = New System.Drawing.Size(762, 498)
        Me.DataGridViewX1.TabIndex = 1
        '
        'btnSearch
        '
        Me.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnSearch.Location = New System.Drawing.Point(785, 649)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlF)
        Me.btnSearch.Size = New System.Drawing.Size(118, 58)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.Text = "SEARCH"
        '
        'btnGenerateSQL
        '
        Me.btnGenerateSQL.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateSQL.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateSQL.Location = New System.Drawing.Point(785, 562)
        Me.btnGenerateSQL.Name = "btnGenerateSQL"
        Me.btnGenerateSQL.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlG)
        Me.btnGenerateSQL.Size = New System.Drawing.Size(118, 58)
        Me.btnGenerateSQL.TabIndex = 3
        Me.btnGenerateSQL.Text = "Generate SQL Code"
        '
        'FrmAdvancedSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(921, 719)
        Me.Controls.Add(Me.PanelEx1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmAdvancedSearch"
        Me.Text = "Advanced Search"
        Me.TitleText = "<b>Advanced Search</b>"
        Me.PanelEx1.ResumeLayout(False)
        CType(Me.dtDummy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ContextMenuBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents listViewEx1 As DevComponents.DotNetBar.Controls.ListViewEx
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents txtSQL As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents btnGenerateSQL As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnSearch As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents DataGridViewX1 As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents chkWildCharacter As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents ContextMenuBar1 As DevComponents.DotNetBar.ContextMenuBar
    Friend WithEvents ButtonItem1 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem2 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btn0 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btn1 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btn2 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btn3 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btn4 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btn5 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btn6 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btn7 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btn8 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btn9 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btn10 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btn11 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btn12 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btn13 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btn14 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btn15 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btn16 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem6 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem8 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem10 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem11 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem12 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem13 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem14 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem15 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem7 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem9 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem16 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCut As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCopy As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnPaste As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDelete As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnClearAllFields As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnDeselectAll As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnSelectAll As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnUndo As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnSelectAllText As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents dtDummy As DevComponents.Editors.DateTimeAdv.DateTimeInput
End Class
