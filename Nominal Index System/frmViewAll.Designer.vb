<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewAll
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
        Me.components = New System.ComponentModel.Container
        Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewAll))
        Me.profileBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.FPBDataSet = New Nominal_Index_System.FPBDataSet
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer
        Me.profileTableAdapter = New Nominal_Index_System.FPBDataSetTableAdapters.profileTableAdapter
        CType(Me.profileBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FPBDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'profileBindingSource
        '
        Me.profileBindingSource.DataMember = "profile"
        Me.profileBindingSource.DataSource = Me.FPBDataSet
        '
        'FPBDataSet
        '
        Me.FPBDataSet.DataSetName = "FPBDataSet"
        Me.FPBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource1.Name = "FPBDataSet_profile"
        ReportDataSource1.Value = Me.profileBindingSource
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportViewer1.LocalReport.DisplayName = "Nominal Index Register"
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "Nominal_Index_System.Print.rdlc"
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(673, 266)
        Me.ReportViewer1.TabIndex = 0
        '
        'profileTableAdapter
        '
        Me.profileTableAdapter.ClearBeforeFill = True
        '
        'frmViewAll
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(673, 266)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmViewAll"
        Me.Text = "Print"
        Me.TitleText = "<b>Print</b>"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.profileBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FPBDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents profileBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents FPBDataSet As Nominal_Index_System.FPBDataSet
    Friend WithEvents profileTableAdapter As Nominal_Index_System.FPBDataSetTableAdapters.profileTableAdapter
End Class
