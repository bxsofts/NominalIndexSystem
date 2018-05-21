<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFPSlipImageDisplayer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFPSlipImageDisplayer))
        Me.picDASlip = New iViewCore.PictureBox
        Me.Bar = New DevComponents.DotNetBar.Bar
        Me.btnPrintImage = New DevComponents.DotNetBar.ButtonItem
        Me.btnPrevious = New DevComponents.DotNetBar.ButtonItem
        Me.btnNext = New DevComponents.DotNetBar.ButtonItem
        Me.btnZoomIn = New DevComponents.DotNetBar.ButtonItem
        Me.btnZoomOut = New DevComponents.DotNetBar.ButtonItem
        Me.sldrZoom = New DevComponents.DotNetBar.SliderItem
        Me.SuperTooltip1 = New DevComponents.DotNetBar.SuperTooltip
        Me.ExpandablePanel1 = New DevComponents.DotNetBar.ExpandablePanel
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX
        Me.lblRCN = New DevComponents.DotNetBar.LabelX
        Me.LabelX13 = New DevComponents.DotNetBar.LabelX
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX
        Me.LabelX7 = New DevComponents.DotNetBar.LabelX
        Me.LabelX10 = New DevComponents.DotNetBar.LabelX
        Me.lblFather = New DevComponents.DotNetBar.LabelX
        Me.lblHenry = New DevComponents.DotNetBar.LabelX
        Me.lblTIN = New DevComponents.DotNetBar.LabelX
        Me.lblName = New DevComponents.DotNetBar.LabelX
        Me.LabelX28 = New DevComponents.DotNetBar.LabelX
        Me.LabelX23 = New DevComponents.DotNetBar.LabelX
        Me.LabelX27 = New DevComponents.DotNetBar.LabelX
        Me.LabelX24 = New DevComponents.DotNetBar.LabelX
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx
        CType(Me.Bar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ExpandablePanel1.SuspendLayout()
        Me.PanelEx1.SuspendLayout()
        Me.SuspendLayout()
        '
        'picDASlip
        '
        Me.picDASlip.AutoScrollMargin = New System.Drawing.Size(5, 5)
        Me.picDASlip.AutoScrollMinSize = New System.Drawing.Size(5, 5)
        Me.picDASlip.BackColor = System.Drawing.Color.Gray
        Me.picDASlip.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.picDASlip.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picDASlip.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picDASlip.Image = Nothing
        Me.picDASlip.Location = New System.Drawing.Point(0, 0)
        Me.picDASlip.Name = "picDASlip"
        Me.picDASlip.Size = New System.Drawing.Size(810, 541)
        Me.picDASlip.TabIndex = 21
        Me.picDASlip.ViewMode = iViewCore.PictureBox.EViewMode.FullSize
        '
        'Bar
        '
        Me.Bar.AccessibleDescription = "DotNetBar Bar (Bar)"
        Me.Bar.AccessibleName = "DotNetBar Bar"
        Me.Bar.AccessibleRole = System.Windows.Forms.AccessibleRole.StatusBar
        Me.Bar.BarType = DevComponents.DotNetBar.eBarType.StatusBar
        Me.Bar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Bar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.ResizeHandle
        Me.Bar.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnPrintImage, Me.btnPrevious, Me.btnNext, Me.btnZoomIn, Me.btnZoomOut, Me.sldrZoom})
        Me.Bar.Location = New System.Drawing.Point(0, 541)
        Me.Bar.Name = "Bar"
        Me.Bar.Size = New System.Drawing.Size(1226, 57)
        Me.Bar.Stretch = True
        Me.Bar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar.TabIndex = 22
        Me.Bar.TabStop = False
        '
        'btnPrintImage
        '
        Me.btnPrintImage.BeginGroup = True
        Me.btnPrintImage.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnPrintImage.Image = CType(resources.GetObject("btnPrintImage.Image"), System.Drawing.Image)
        Me.btnPrintImage.ImagePaddingHorizontal = 8
        Me.btnPrintImage.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far
        Me.btnPrintImage.Name = "btnPrintImage"
        Me.btnPrintImage.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlP)
        Me.SuperTooltip1.SetSuperTooltip(Me.btnPrintImage, New DevComponents.DotNetBar.SuperTooltipInfo("Fingerprint Information System", "Print (Ctrl+P)", "Print the DA Slip image.", CType(resources.GetObject("btnPrintImage.SuperTooltip"), System.Drawing.Image), Nothing, DevComponents.DotNetBar.eTooltipColor.Yellow))
        Me.btnPrintImage.Text = "Print"
        '
        'btnPrevious
        '
        Me.btnPrevious.BeginGroup = True
        Me.btnPrevious.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnPrevious.Image = CType(resources.GetObject("btnPrevious.Image"), System.Drawing.Image)
        Me.btnPrevious.ImagePaddingHorizontal = 8
        Me.btnPrevious.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far
        Me.btnPrevious.Name = "btnPrevious"
        Me.SuperTooltip1.SetSuperTooltip(Me.btnPrevious, New DevComponents.DotNetBar.SuperTooltipInfo("Fingerprint Information System", "Previous (Left or Down Arrow Key)", "Display the previous DA Slip image.", CType(resources.GetObject("btnPrevious.SuperTooltip"), System.Drawing.Image), Nothing, DevComponents.DotNetBar.eTooltipColor.Yellow))
        Me.btnPrevious.Text = "Previous"
        '
        'btnNext
        '
        Me.btnNext.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnNext.Image = CType(resources.GetObject("btnNext.Image"), System.Drawing.Image)
        Me.btnNext.ImagePaddingHorizontal = 8
        Me.btnNext.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far
        Me.btnNext.Name = "btnNext"
        Me.SuperTooltip1.SetSuperTooltip(Me.btnNext, New DevComponents.DotNetBar.SuperTooltipInfo("Fingerprint Information System", "Next (Right or Up Arrow Key)", "Display the next DA Slip image", CType(resources.GetObject("btnNext.SuperTooltip"), System.Drawing.Image), Nothing, DevComponents.DotNetBar.eTooltipColor.Yellow))
        Me.btnNext.Text = "Next"
        '
        'btnZoomIn
        '
        Me.btnZoomIn.BeginGroup = True
        Me.btnZoomIn.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnZoomIn.Image = CType(resources.GetObject("btnZoomIn.Image"), System.Drawing.Image)
        Me.btnZoomIn.ImagePaddingHorizontal = 8
        Me.btnZoomIn.Name = "btnZoomIn"
        Me.SuperTooltip1.SetSuperTooltip(Me.btnZoomIn, New DevComponents.DotNetBar.SuperTooltipInfo("Fingerprint Information System", "Zoom In (+)", "Zoom the image in.", CType(resources.GetObject("btnZoomIn.SuperTooltip"), System.Drawing.Image), Nothing, DevComponents.DotNetBar.eTooltipColor.Yellow))
        Me.btnZoomIn.Text = "Zoom In"
        '
        'btnZoomOut
        '
        Me.btnZoomOut.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnZoomOut.Image = CType(resources.GetObject("btnZoomOut.Image"), System.Drawing.Image)
        Me.btnZoomOut.ImagePaddingHorizontal = 8
        Me.btnZoomOut.Name = "btnZoomOut"
        Me.SuperTooltip1.SetSuperTooltip(Me.btnZoomOut, New DevComponents.DotNetBar.SuperTooltipInfo("Fingerprint Information System", "Zoom Out (-)", "Zoom the image out", CType(resources.GetObject("btnZoomOut.SuperTooltip"), System.Drawing.Image), Nothing, DevComponents.DotNetBar.eTooltipColor.Yellow))
        Me.btnZoomOut.Text = "Zoom Out"
        '
        'sldrZoom
        '
        Me.sldrZoom.BeginGroup = True
        Me.sldrZoom.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far
        Me.sldrZoom.Maximum = 500
        Me.sldrZoom.Minimum = 25
        Me.sldrZoom.Name = "sldrZoom"
        Me.sldrZoom.Step = 5
        Me.sldrZoom.Text = "Zoom"
        Me.sldrZoom.TrackMarker = False
        Me.sldrZoom.Value = 25
        Me.sldrZoom.Width = 250
        '
        'ExpandablePanel1
        '
        Me.ExpandablePanel1.AnimationTime = 200
        Me.ExpandablePanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.ExpandablePanel1.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.RightToLeft
        Me.ExpandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.ExpandablePanel1.Controls.Add(Me.LabelX3)
        Me.ExpandablePanel1.Controls.Add(Me.lblRCN)
        Me.ExpandablePanel1.Controls.Add(Me.LabelX13)
        Me.ExpandablePanel1.Controls.Add(Me.LabelX5)
        Me.ExpandablePanel1.Controls.Add(Me.LabelX6)
        Me.ExpandablePanel1.Controls.Add(Me.LabelX7)
        Me.ExpandablePanel1.Controls.Add(Me.LabelX10)
        Me.ExpandablePanel1.Controls.Add(Me.lblFather)
        Me.ExpandablePanel1.Controls.Add(Me.lblHenry)
        Me.ExpandablePanel1.Controls.Add(Me.lblTIN)
        Me.ExpandablePanel1.Controls.Add(Me.lblName)
        Me.ExpandablePanel1.Controls.Add(Me.LabelX28)
        Me.ExpandablePanel1.Controls.Add(Me.LabelX23)
        Me.ExpandablePanel1.Controls.Add(Me.LabelX27)
        Me.ExpandablePanel1.Controls.Add(Me.LabelX24)
        Me.ExpandablePanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ExpandablePanel1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExpandablePanel1.Location = New System.Drawing.Point(0, 0)
        Me.ExpandablePanel1.Name = "ExpandablePanel1"
        Me.ExpandablePanel1.Size = New System.Drawing.Size(416, 541)
        Me.ExpandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.ExpandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.ExpandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.ExpandablePanel1.Style.GradientAngle = 90
        Me.ExpandablePanel1.TabIndex = 23
        Me.ExpandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.ExpandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner
        Me.ExpandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.ExpandablePanel1.TitleStyle.GradientAngle = 90
        Me.ExpandablePanel1.TitleText = "Details"
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        Me.LabelX3.Location = New System.Drawing.Point(138, 78)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(6, 20)
        Me.LabelX3.TabIndex = 137
        Me.LabelX3.Text = ":"
        '
        'lblRCN
        '
        Me.lblRCN.AutoSize = True
        Me.lblRCN.Location = New System.Drawing.Point(150, 78)
        Me.lblRCN.Name = "lblRCN"
        Me.lblRCN.Size = New System.Drawing.Size(29, 20)
        Me.lblRCN.TabIndex = 136
        Me.lblRCN.Text = "RCN"
        Me.lblRCN.WordWrap = True
        '
        'LabelX13
        '
        Me.LabelX13.AutoSize = True
        Me.LabelX13.Location = New System.Drawing.Point(12, 78)
        Me.LabelX13.Name = "LabelX13"
        Me.LabelX13.Size = New System.Drawing.Size(29, 20)
        Me.LabelX13.TabIndex = 135
        Me.LabelX13.Text = "RCN"
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        Me.LabelX5.Location = New System.Drawing.Point(138, 163)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(6, 20)
        Me.LabelX5.TabIndex = 120
        Me.LabelX5.Text = ":"
        '
        'LabelX6
        '
        Me.LabelX6.AutoSize = True
        Me.LabelX6.Location = New System.Drawing.Point(138, 207)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(6, 20)
        Me.LabelX6.TabIndex = 115
        Me.LabelX6.Text = ":"
        '
        'LabelX7
        '
        Me.LabelX7.AutoSize = True
        Me.LabelX7.Location = New System.Drawing.Point(138, 42)
        Me.LabelX7.Name = "LabelX7"
        Me.LabelX7.Size = New System.Drawing.Size(6, 20)
        Me.LabelX7.TabIndex = 113
        Me.LabelX7.Text = ":"
        '
        'LabelX10
        '
        Me.LabelX10.AutoSize = True
        Me.LabelX10.Location = New System.Drawing.Point(138, 121)
        Me.LabelX10.Name = "LabelX10"
        Me.LabelX10.Size = New System.Drawing.Size(6, 20)
        Me.LabelX10.TabIndex = 114
        Me.LabelX10.Text = ":"
        '
        'lblFather
        '
        Me.lblFather.AutoSize = True
        Me.lblFather.Location = New System.Drawing.Point(150, 163)
        Me.lblFather.Name = "lblFather"
        Me.lblFather.Size = New System.Drawing.Size(91, 20)
        Me.lblFather.TabIndex = 110
        Me.lblFather.Text = "Father's Name"
        Me.lblFather.WordWrap = True
        '
        'lblHenry
        '
        Me.lblHenry.AutoSize = True
        Me.lblHenry.Location = New System.Drawing.Point(150, 207)
        Me.lblHenry.Name = "lblHenry"
        Me.lblHenry.Size = New System.Drawing.Size(83, 20)
        Me.lblHenry.TabIndex = 105
        Me.lblHenry.Text = "Classification"
        Me.lblHenry.WordWrap = True
        '
        'lblTIN
        '
        Me.lblTIN.AutoSize = True
        Me.lblTIN.Location = New System.Drawing.Point(150, 42)
        Me.lblTIN.Name = "lblTIN"
        Me.lblTIN.Size = New System.Drawing.Size(25, 20)
        Me.lblTIN.TabIndex = 103
        Me.lblTIN.Text = "TIN"
        Me.lblTIN.WordWrap = True
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(150, 121)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(39, 20)
        Me.lblName.TabIndex = 104
        Me.lblName.Text = "Name"
        Me.lblName.WordWrap = True
        '
        'LabelX28
        '
        Me.LabelX28.AutoSize = True
        Me.LabelX28.Location = New System.Drawing.Point(12, 163)
        Me.LabelX28.Name = "LabelX28"
        Me.LabelX28.Size = New System.Drawing.Size(91, 20)
        Me.LabelX28.TabIndex = 100
        Me.LabelX28.Text = "Father's Name"
        '
        'LabelX23
        '
        Me.LabelX23.AutoSize = True
        Me.LabelX23.Location = New System.Drawing.Point(12, 207)
        Me.LabelX23.Name = "LabelX23"
        Me.LabelX23.Size = New System.Drawing.Size(83, 20)
        Me.LabelX23.TabIndex = 95
        Me.LabelX23.Text = "Classification"
        '
        'LabelX27
        '
        Me.LabelX27.AutoSize = True
        Me.LabelX27.Location = New System.Drawing.Point(12, 42)
        Me.LabelX27.Name = "LabelX27"
        Me.LabelX27.Size = New System.Drawing.Size(25, 20)
        Me.LabelX27.TabIndex = 93
        Me.LabelX27.Text = "TIN"
        '
        'LabelX24
        '
        Me.LabelX24.AutoSize = True
        Me.LabelX24.Location = New System.Drawing.Point(12, 121)
        Me.LabelX24.Name = "LabelX24"
        Me.LabelX24.Size = New System.Drawing.Size(39, 20)
        Me.LabelX24.TabIndex = 94
        Me.LabelX24.Text = "Name"
        '
        'PanelEx1
        '
        Me.PanelEx1.AutoScroll = True
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.picDASlip)
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(416, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(810, 541)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 24
        Me.PanelEx1.Text = "PanelEx1"
        '
        'frmFPSlipImageDisplayer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1226, 598)
        Me.Controls.Add(Me.PanelEx1)
        Me.Controls.Add(Me.ExpandablePanel1)
        Me.Controls.Add(Me.Bar)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmFPSlipImageDisplayer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FP Slip Image"
        Me.TitleText = "<b>FP Slip Image</b>"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.Bar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ExpandablePanel1.ResumeLayout(False)
        Me.ExpandablePanel1.PerformLayout()
        Me.PanelEx1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents picDASlip As iViewCore.PictureBox
    Friend WithEvents Bar As DevComponents.DotNetBar.Bar
    Friend WithEvents btnPrevious As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnNext As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnZoomOut As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnZoomIn As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents sldrZoom As DevComponents.DotNetBar.SliderItem
    Friend WithEvents btnPrintImage As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents SuperTooltip1 As DevComponents.DotNetBar.SuperTooltip
    Friend WithEvents ExpandablePanel1 As DevComponents.DotNetBar.ExpandablePanel
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents LabelX28 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX23 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX27 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX24 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblFather As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblHenry As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblTIN As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblName As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX7 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX10 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblRCN As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX13 As DevComponents.DotNetBar.LabelX
End Class
