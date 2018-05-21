Public Class frmFPSlipImageDisplayer
    Dim PictureFile As String
    Dim zoom As Boolean = False


    Public Sub LoadPicture(ByVal PicFile As String, ByVal Title As String)
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        PictureFile = PicFile
        EnableButtons()
        Me.picDASlip.ClearImage()
        Me.sldrZoom.Value = Me.sldrZoom.Minimum
        Me.lblTIN.Text = Title
        LoadDADetails()
        If FileIO.FileSystem.FileExists(PicFile) = False Then
            Me.picDASlip.Image = My.Resources.NoDAImage
            zoom = False
            Me.btnPrintImage.Enabled = False
            Me.btnZoomIn.Enabled = False
            Me.btnZoomOut.Enabled = False
            Me.sldrZoom.Enabled = False
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        Me.picDASlip.Image = New System.Drawing.Bitmap(PicFile)
        Me.Cursor = Cursors.Default
        zoom = True
        Me.btnPrintImage.Enabled = True
        Me.btnZoomIn.Enabled = True
        Me.btnZoomOut.Enabled = True
        Me.sldrZoom.Enabled = True
        ' Me.sldrZoom.Value = Me.sldrZoom.Minimum + 20
        Me.picDASlip.ViewMode = iViewCore.PictureBox.EViewMode.FitImage
    End Sub

    Public Sub LoadPictureFromViewer(ByVal PicFile As String, ByVal Title As String)
        On Error Resume Next
        Me.Cursor = Cursors.WaitCursor
        PictureFile = PicFile
        EnableButtons()
        Me.picDASlip.ClearImage()
        Me.sldrZoom.Value = Me.sldrZoom.Minimum
        Me.lblTIN.Text = Title
        Me.lblRCN.Text = ""
        Me.lblName.Text = ""
        Me.lblFather.Text = ""
        Me.lblHenry.Text = ""


        If FileIO.FileSystem.FileExists(PicFile) = False Then
            Me.picDASlip.Image = My.Resources.NoDAImage
            zoom = False
            Me.btnPrintImage.Enabled = False
            Me.btnZoomIn.Enabled = False
            Me.btnZoomOut.Enabled = False
            Me.sldrZoom.Enabled = False
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        Me.picDASlip.Image = New System.Drawing.Bitmap(PicFile)
        Me.Cursor = Cursors.Default
        zoom = True
        Me.btnPrintImage.Enabled = True
        Me.btnZoomIn.Enabled = True
        Me.btnZoomOut.Enabled = True
        Me.sldrZoom.Enabled = True
        Me.sldrZoom.Value = Me.sldrZoom.Minimum + 20

    End Sub

    Private Sub ZoomOnMouseWheelScroll(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picDASlip.MouseWheel
        On Error Resume Next
        If zoom = False Then Exit Sub
        Dim ZoomLevel As Integer = CInt(e.Delta * SystemInformation.MouseWheelScrollLines / 120)
        If ZoomLevel = 3 Then
            If Me.sldrZoom.Value >= Me.sldrZoom.Maximum Then Exit Sub
            Me.sldrZoom.Value = Me.sldrZoom.Value + 10
        End If
        If ZoomLevel = -3 Then
            If Me.sldrZoom.Value <= Me.sldrZoom.Minimum Then Exit Sub
            Me.sldrZoom.Value = Me.sldrZoom.Value - 10
        End If
    End Sub


    Private Sub ZoomOnSliderMovement() Handles sldrZoom.ValueChanged
        On Error Resume Next
        Me.picDASlip.Zoom(sldrZoom.Value)

    End Sub

    Private Sub ZoomIn() Handles btnZoomIn.Click
        On Error Resume Next
        If btnZoomIn.Enabled = False Then Exit Sub
        If Me.sldrZoom.Value >= Me.sldrZoom.Maximum Then Exit Sub
        Me.sldrZoom.Value = Me.sldrZoom.Value + 10
    End Sub


    Private Sub ZoomOut() Handles btnZoomOut.Click
        On Error Resume Next
        If btnZoomOut.Enabled = False Then Exit Sub
        If Me.sldrZoom.Value <= Me.sldrZoom.Minimum Then Exit Sub
        Me.sldrZoom.Value = Me.sldrZoom.Value - 10
    End Sub

    Private Sub frmDASlipImageDisplayer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        On Error Resume Next
        If e.KeyCode = Keys.Add Then ZoomIn()
        If e.KeyCode = Keys.Subtract Then ZoomOut()
    End Sub



    Private Sub frmDASlipImageDisplayer_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        On Error Resume Next
        If (e.KeyCode = Keys.Right Or e.KeyCode = Keys.Up) Then ShowNextImage()
        If (e.KeyCode = Keys.Left Or e.KeyCode = Keys.Down) Then ShowPreviousImage()
    End Sub



    Private Sub frmDASlipImage_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        On Error Resume Next
        Me.SuperTooltip1.Enabled = frmMainInterface.chkShowToolTips.Checked
        Me.picDASlip.ClearImage()
        EnableButtons()
    End Sub


    Sub EnableButtons()
        On Error Resume Next
        If frmMainInterface.ProfileBindingSource.Position = frmMainInterface.ProfileBindingSource.Count - 1 Then
            Me.btnNext.Enabled = False
        Else
            Me.btnNext.Enabled = True
        End If

        If frmMainInterface.ProfileBindingSource.Position = 0 Then
            Me.btnPrevious.Enabled = False
        Else
            Me.btnPrevious.Enabled = True
        End If
    End Sub

    Private Sub ShowPreviousImage() Handles btnPrevious.Click
        On Error Resume Next
        frmMainInterface.ProfileBindingSource.MovePrevious()
        PictureFile = frmMainInterface.DataGridView.CurrentRow.Cells(16).Value.ToString()
        LoadPicture(PictureFile, frmMainInterface.DataGridView.CurrentRow.Cells(0).Value.ToString())

    End Sub

    Private Sub ShowNextImage() Handles btnNext.Click
        On Error Resume Next
        frmMainInterface.ProfileBindingSource.MoveNext()
        PictureFile = frmMainInterface.DataGridView.CurrentRow.Cells(16).Value.ToString()
        LoadPicture(PictureFile, frmMainInterface.DataGridView.CurrentRow.Cells(0).Value.ToString())
       
    End Sub


    Private Sub PrintImage() Handles btnPrintImage.Click
        On Error GoTo errhandler
        If FileIO.FileSystem.FileExists(PictureFile) = False Then Exit Sub
        Dim dg As New WIA.CommonDialog
        dg.ShowPhotoPrintingWizard(PictureFile)
        Exit Sub
errhandler:
        DevComponents.DotNetBar.MessageBoxEx.Show(Err.Description, AppName, MessageBoxButtons.OK, MessageBoxIcon.Error)

    End Sub

    Sub LoadDADetails()
        On Error Resume Next
        Me.lblTIN.Text = frmMainInterface.DataGridView.CurrentRow.Cells(0).Value.ToString()
        Me.lblRCN.Text = frmMainInterface.DataGridView.CurrentRow.Cells(1).Value.ToString()
        Me.lblName.Text = frmMainInterface.DataGridView.CurrentRow.Cells(3).Value.ToString()
        Me.lblFather.Text = frmMainInterface.DataGridView.CurrentRow.Cells(6).Value.ToString()
        Me.lblHenry.Text = frmMainInterface.DataGridView.CurrentRow.Cells(12).Value.ToString()
        
    End Sub

   
End Class