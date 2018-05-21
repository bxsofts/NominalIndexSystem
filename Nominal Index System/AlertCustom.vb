Public Class AlertCustom
    Inherits DevComponents.DotNetBar.Balloon

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents linkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents labelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents reflectionImage1 As DevComponents.DotNetBar.Controls.ReflectionImage
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AlertCustom))
        Me.label2 = New System.Windows.Forms.Label
        Me.linkLabel1 = New System.Windows.Forms.LinkLabel
        Me.reflectionImage1 = New DevComponents.DotNetBar.Controls.ReflectionImage
        Me.labelX1 = New DevComponents.DotNetBar.LabelX
        Me.SuspendLayout()
        '
        'label2
        '
        Me.label2.BackColor = System.Drawing.Color.Transparent
        Me.label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.ForeColor = System.Drawing.Color.Firebrick
        Me.label2.Location = New System.Drawing.Point(16, 144)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(168, 16)
        Me.label2.TabIndex = 7
        Me.label2.Text = "Click 'Enable Balloons' now!"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'linkLabel1
        '
        Me.linkLabel1.BackColor = System.Drawing.Color.Transparent
        Me.linkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linkLabel1.Location = New System.Drawing.Point(48, 168)
        Me.linkLabel1.Name = "linkLabel1"
        Me.linkLabel1.Size = New System.Drawing.Size(152, 16)
        Me.linkLabel1.TabIndex = 5
        Me.linkLabel1.TabStop = True
        Me.linkLabel1.Text = "Click to visit DevComponents"
        '
        'reflectionImage1
        '
        Me.reflectionImage1.BackColor = System.Drawing.Color.Transparent
        Me.reflectionImage1.Dock = System.Windows.Forms.DockStyle.Right
        Me.reflectionImage1.Image = CType(resources.GetObject("reflectionImage1.Image"), System.Drawing.Image)
        Me.reflectionImage1.Location = New System.Drawing.Point(266, 0)
        Me.reflectionImage1.Name = "reflectionImage1"
        Me.reflectionImage1.Size = New System.Drawing.Size(63, 96)
        Me.reflectionImage1.TabIndex = 8
        '
        'labelX1
        '
        Me.labelX1.AutoSize = True
        Me.labelX1.BackColor = System.Drawing.Color.Transparent
        Me.labelX1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelX1.Location = New System.Drawing.Point(6, 6)
        Me.labelX1.Name = "labelX1"
        Me.labelX1.Size = New System.Drawing.Size(130, 18)
        Me.labelX1.TabIndex = 10
        Me.labelX1.Text = "<u><b>Nominal Index System</b></u>"
        '
        'AlertCustom
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(175, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BorderColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.CaptionFont = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ClientSize = New System.Drawing.Size(329, 96)
        Me.Controls.Add(Me.labelX1)
        Me.Controls.Add(Me.reflectionImage1)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.linkLabel1)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(114, Byte), Integer))
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "AlertCustom"
        Me.Style = DevComponents.DotNetBar.eBallonStyle.Office2007Alert
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region


    Private Sub buttonItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Click, reflectionImage1.Click
        Me.Close()
    End Sub
End Class
