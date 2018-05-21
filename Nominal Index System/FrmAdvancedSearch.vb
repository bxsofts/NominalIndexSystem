Imports System.Data
Public Class FrmAdvancedSearch
    Dim SQLTextChanged As Boolean = False


    Private Sub FrmAdvancedSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error Resume Next
        Me.chkWildCharacter.Checked = True
        Me.DataGridViewX1.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Regular)
        Me.DataGridViewX1.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)

        For i = 0 To 16
            Me.listViewEx1.Items.Add(frmMainInterface.DataGridView.Columns(i).HeaderText)
        Next



        Me.DataGridViewX1.Columns.Add("Select", "Select")
        Me.DataGridViewX1.Columns.Add("Field", "Field")
        Me.DataGridViewX1.Columns.Add("Operator", "Operator")
        Me.DataGridViewX1.Columns.Add("Value", "Value")
        Me.DataGridViewX1.Columns.Add("Logic", "Logical Operator")

        Me.DataGridViewX1.Columns(0).Width = 50
        Me.DataGridViewX1.Columns(1).Width = 150
        Me.DataGridViewX1.Columns(2).Width = 125
        Me.DataGridViewX1.Columns(3).Width = 250
        Me.DataGridViewX1.Columns(4).Width = 125

        Me.DataGridViewX1.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        For i = 0 To 16

            Dim dgrow = New DataGridViewRow()
            Dim dgselect = New DataGridViewCheckBoxCell
            Dim dgfield = New DataGridViewTextBoxCell
            Dim dgoperator = New DataGridViewComboBoxCell
            Dim dgvalue = New DataGridViewTextBoxCell
            Dim dglogic = New DataGridViewComboBoxCell
            Dim dgovstatus = New DataGridViewComboBoxCell
            Dim dgsex = New DataGridViewComboBoxCell

            dgselect.Value = False
            dgfield.Value = Me.listViewEx1.Items(i).Text

            dgoperator.Items.Add("")
            dgoperator.Items.Add("=")
            dgoperator.Items.Add("<>")
            dgoperator.Items.Add("<")
            dgoperator.Items.Add(">")
            dgoperator.Items.Add("<=")
            dgoperator.Items.Add(">=")
            dgoperator.Items.Add("LIKE")
            dgoperator.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
            dgoperator.DisplayStyleForCurrentCellOnly = True
            dgoperator.Value = dgoperator.Items(0)

            If i <> 15 And i <> 8 Then dgvalue.Value = ""

            dglogic.Items.Add("")
            dglogic.Items.Add("AND")
            dglogic.Items.Add("OR")
            dglogic.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
            dglogic.DisplayStyleForCurrentCellOnly = True
            dglogic.Value = dglogic.Items(0)


            dgrow.Cells.Add(dgselect)
            dgrow.Cells.Add(dgfield)
            dgrow.Cells.Add(dgoperator)
            If i <> 15 And i <> 8 Then dgrow.Cells.Add(dgvalue)

            If i = 8 Then
                dgsex.Items.Add("")
                dgsex.Items.Add("MALE")
                dgsex.Items.Add("FEMALE")
                dgsex.Items.Add("ANY")
                dgsex.Items.Add("NOT GIVEN")
                dgsex.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
                dgsex.DisplayStyleForCurrentCellOnly = True
                dgsex.Value = dgsex.Items(0)
                dgrow.Cells.Add(dgsex)
            End If

            If i = 15 Then
                dgovstatus.Items.Add("")
                dgovstatus.Items.Add("YES")
                dgovstatus.Items.Add("NO")
                dgovstatus.Items.Add("ANY")
                dgovstatus.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
                dgovstatus.DisplayStyleForCurrentCellOnly = True
                dgovstatus.Value = dgovstatus.Items(0)
                dgrow.Cells.Add(dgovstatus)
            End If
            dgrow.Cells.Add(dglogic)
            dgrow.Height = 25
            Me.DataGridViewX1.Rows.Add(dgrow)

        Next

        Me.txtSQL.Text = "Select * from PROFILE order by Name"
        SQLTextChanged = False
    End Sub



    Private Sub GenerateSQL(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateSQL.Click
        Try
            Dim sql As String = "Select * from PROFILE where "
            Dim SelectedCount As Integer = 0

            For i = 0 To 16
                If Me.DataGridViewX1.Rows(i).Cells(0).Value = True Then
                    SelectedCount = SelectedCount + 1
                End If
            Next

            If SelectedCount = 0 Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Please select at least one field.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Dim ProcessedCount As Integer = 0

            For i = 0 To 16
                If Me.DataGridViewX1.Rows(i).Cells(0).Value = True Then
                    With Me.DataGridViewX1.Rows(i)
                        Dim field As String = .Cells(1).Value.ToString
                        Dim condition As String = FindValueOperator(i, 2)
                        Dim value As String = FindValue(i, 3)
                        Dim logic As String = FindLogicOperator(i, 4)

                        If condition = "" And field <> "Henry Classification" Then
                            DevComponents.DotNetBar.MessageBoxEx.Show("Please enter the operator for the field " & field, AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.DataGridViewX1.Focus()
                            .Cells(2).Selected = True
                            Exit Sub
                        End If


                        If field = "OV Status" And value = "ANY" Then
                            condition = "LIKE"
                            value = "'%'"
                        End If

                        If field = "OV Status" And value = "" Then
                            condition = "LIKE"
                            value = "'%'"
                        End If

                       
                        If field = "Sex" And value = "ANY" Then
                            condition = "LIKE"
                            value = "%"
                        End If

                        If field = "Sex" And value = "NOT GIVEN" Then
                            ' condition = "LIKE"
                            value = ""
                        End If
                       

                        If ProcessedCount < SelectedCount - 1 And SelectedCount <> 1 Then
                            If logic = "" Then
                                DevComponents.DotNetBar.MessageBoxEx.Show("Please enter the logic operator for the field " & field, AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.DataGridViewX1.Focus()
                                .Cells(4).Selected = True
                                Exit Sub
                            End If
                        End If

                        If field <> "Latest Trace Date" And field <> "OV Status" And field <> "Sex" And field <> "Henry Classification" Then

                            If chkWildCharacter.Checked And condition = "LIKE" Then
                                value = "'" & value & "%'"
                            Else
                                value = "'" & value & "'"
                            End If

                        End If

                        If field = "Latest Trace Date" Then
                            value = GenerateDate(value)
                        End If

                        If field = "Sex" Then
                            value = "'" & value & "'"
                        End If

                        If field = "Henry Classification" Then
                            If value = "" Then
                                value = " Henry Classification LIKE '%' "
                            Else
                                If value.Contains("%") Or value.Contains("_") Then
                                    value = "Henry Classification Like '" & value & "'"
                                Else
                                    value = " instr(1, Henry Classification, '" & value & "', 0)>0 "
                                End If
                            End If
                        End If


                        If ProcessedCount = SelectedCount - 1 Then logic = ""
                        If field <> "Henry Classification" Then
                            sql = sql & field & " " & condition & " " & value & " " & logic & " "

                        Else
                            sql = sql & value & " " & logic & " "
                        End If
                        ProcessedCount = ProcessedCount + 1
                    End With
                End If
            Next

            sql = sql.Replace("  ", " ")
            sql = sql.Replace("%%", "%")

            sql = sql & " order by Name"
            Me.txtSQL.Text = sql
        Catch ex As Exception
            Dim msg As String = ex.Message
            For i = 0 To 16
                msg = Strings.Replace(msg, frmMainInterface.DataGridView.Columns(i).DataPropertyName, frmMainInterface.DataGridView.Columns(i).HeaderText, , , CompareMethod.Text)
                ' msg = msg.Replace(frmMainInterface.DataGridView.Columns(i).DataPropertyName.ToUpper, frmMainInterface.DataGridView.Columns(i).HeaderText.ToUpper)
            Next
            DevComponents.DotNetBar.MessageBoxEx.Show(msg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function FindValueOperator(ByVal row As Integer, ByVal column As Integer) As String
        FindValueOperator = ""
        Try
            FindValueOperator = Me.DataGridViewX1.Rows(row).Cells(column).Value.ToString
        Catch ex As Exception
            FindValueOperator = ""
        End Try
    End Function

    Private Function FindValue(ByVal row As Integer, ByVal column As Integer) As String
        FindValue = ""
        Try
            FindValue = Me.DataGridViewX1.Rows(row).Cells(column).Value.ToString
        Catch ex As Exception
            FindValue = ""
        End Try
    End Function

    Private Function FindLogicOperator(ByVal row As Integer, ByVal column As Integer) As String
        FindLogicOperator = ""
        Try
            FindLogicOperator = Me.DataGridViewX1.Rows(row).Cells(column).Value.ToString
        Catch ex As Exception
            FindLogicOperator = ""
        End Try
    End Function

    Private Sub Search(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Dim SQLText As String = Me.txtSQL.Text
            If SQLText = "" Then
                SQLText = "Select * from PROFILE order by Name"
            End If
            If Trim(SQLText.ToUpper).StartsWith("DELETE") Then
                DevComponents.DotNetBar.MessageBoxEx.Show("Deletion of data is not allowed", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor


            For i = 0 To 16
                SQLText = Strings.Replace(SQLText, frmMainInterface.DataGridView.Columns(i).HeaderText, frmMainInterface.DataGridView.Columns(i).DataPropertyName, , , CompareMethod.Text)
            Next


            Dim con As OleDb.OleDbConnection = New OleDb.OleDbConnection(ConString)
            con.Open()
            Dim cmd As OleDb.OleDbCommand = New OleDb.OleDbCommand(SQLText, con)
            Dim da As New OleDb.OleDbDataAdapter(cmd)
            frmMainInterface.FPBDataSet.profile.Clear()
            da.Fill(frmMainInterface.FPBDataSet.profile)
            con.Close()
            Me.Cursor = Cursors.Default
            frmMainInterface.DisplayRecordCount()
            frmMainInterface.ShowAlertMessage("Search finished. Found " & IIf(frmMainInterface.DataGridView.RowCount = 1, "1 Record", frmMainInterface.DataGridView.RowCount & " Records"))
            Application.DoEvents()
        Catch ex As Exception
            Dim msg As String = ex.Message
            For i = 0 To 16
                msg = Strings.Replace(msg, frmMainInterface.DataGridView.Columns(i).DataPropertyName, frmMainInterface.DataGridView.Columns(i).HeaderText, , , CompareMethod.Text)
            Next
            DevComponents.DotNetBar.MessageBoxEx.Show(msg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.Cursor = Cursors.Default
        End Try


    End Sub

    Private Function GenerateDate(ByVal value As String) As String
        GenerateDate = value
        Try
            Dim s = Strings.Split(value, "/")
            GenerateDate = "#" & s(1) & "/" & s(0) & "/" & s(2) & "#"
        Catch ex As Exception
            GenerateDate = "#" & value & "#"
        End Try
    End Function

    Private Sub InsertValues(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn0.Click, btn1.Click, btn2.Click, ButtonItem8.Click, ButtonItem9.Click, ButtonItem10.Click, ButtonItem11.Click, ButtonItem12.Click, ButtonItem13.Click, ButtonItem14.Click, ButtonItem15.Click, ButtonItem16.Click, btn3.Click, btn4.Click, btn5.Click, btn6.Click, btn7.Click, btn8.Click, btn9.Click, btn10.Click, btn11.Click, btn12.Click, btn13.Click, btn14.Click, btn15.Click, btn16.Click
        On Error Resume Next
        Me.txtSQL.ScrollToCaret()
        Dim text = DirectCast(sender, DevComponents.DotNetBar.ButtonItem).Text
        Me.txtSQL.Paste(" " & text & " ")

    End Sub

    Private Sub PreventContextMenu(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.PopupOpenEventArgs) Handles ContextMenuBar1.PopupOpen
        On Error Resume Next
        e.Cancel = Not Me.txtSQL.Focused

        Me.btnUndo.Enabled = False
        Me.btnCut.Enabled = False
        Me.btnCopy.Enabled = False
        Me.btnPaste.Enabled = False
        Me.btnDelete.Enabled = False
        Me.btnSelectAllText.Enabled = False

        If My.Computer.Clipboard.ContainsText Then
            Me.btnPaste.Enabled = True
        End If

        If Me.txtSQL.SelectionLength <> 0 Then
            Me.btnCut.Enabled = True
            Me.btnCopy.Enabled = True
            Me.btnDelete.Enabled = True
        End If

        If Me.txtSQL.TextLength <> 0 Then
            Me.btnSelectAllText.Enabled = True
        End If
        If SQLTextChanged Then Me.btnUndo.Enabled = True
    End Sub

    Private Sub CopyPaste(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPaste.Click, btnCopy.Click, btnCut.Click, btnDelete.Click, btnUndo.Click, btnSelectAllText.Click
        On Error Resume Next
        Select Case DirectCast(sender, DevComponents.DotNetBar.ButtonItem).Name
            Case btnCut.Name
                Me.txtSQL.Cut()
            Case btnCopy.Name
                Me.txtSQL.Copy()
            Case btnPaste.Name
                Me.txtSQL.ScrollToCaret()
                Me.txtSQL.Paste(My.Computer.Clipboard.GetText)
            Case btnDelete.Name
                Dim text = Me.txtSQL.SelectedText.Replace(Me.txtSQL.SelectedText, "")
                Me.txtSQL.ScrollToCaret()
                Me.txtSQL.Paste(text)
            Case btnUndo.Name
                Me.txtSQL.Undo()
            Case btnSelectAllText.Name
                Me.txtSQL.SelectAll()
        End Select
    End Sub

    Private Sub AutoSelectOpertaor(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridViewX1.CellEndEdit
        On Error Resume Next
        If e.ColumnIndex <> 0 Then
            If Me.DataGridViewX1.CurrentRow.Cells(e.ColumnIndex).Value <> "" Then
                Me.DataGridViewX1.CurrentRow.Cells(0).Value = True
            End If

            If e.RowIndex = 15 And e.ColumnIndex = 3 Then
                If Me.DataGridViewX1.CurrentRow.Cells(3).Value = "ANY" Then
                    Me.DataGridViewX1.CurrentRow.Cells(2).Value = "LIKE"
                End If
            End If

        End If
    End Sub

    Private Sub SelectAll() Handles btnSelectAll.Click
        On Error Resume Next
        For i = 0 To 16
            Me.DataGridViewX1.Rows(i).Cells(0).Value = True
        Next
    End Sub

    Private Sub DeSelectAll() Handles btnDeselectAll.Click
        On Error Resume Next
        For i = 0 To 16
            Me.DataGridViewX1.Rows(i).Cells(0).Value = False
        Next
    End Sub

    Private Sub ClearAllFields() Handles btnClearAllFields.Click
        On Error Resume Next
        For i = 0 To 16
            Me.DataGridViewX1.Rows(i).Cells(2).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(3).Value = ""
            Me.DataGridViewX1.Rows(i).Cells(4).Value = ""
        Next
    End Sub

    Private Sub txtSQL_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSQL.GotFocus
        On Error Resume Next
        Me.btnUndo.Enabled = False
    End Sub


    Private Sub txtSQL_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSQL.TextChanged
        On Error Resume Next
        SQLTextChanged = True
    End Sub
End Class