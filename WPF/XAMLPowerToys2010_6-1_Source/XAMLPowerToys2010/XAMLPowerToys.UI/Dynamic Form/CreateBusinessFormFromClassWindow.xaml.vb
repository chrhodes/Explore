Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Text
Imports XAMLPowerToys.Model

Public Class CreateBusinessFormFromClassWindow

#Region " Declarations "

    Private Const _STR_BUSINESSFORM As String = "Business Form"
    Private Const _STR_SILVERLIGHTDATAGRID As String = "Silverlight Data Grid"
    Private Const _STR_SILVERLIGHTDATAFORM As String = "Silverlight Data Form"
    Private Const _STR_WPFDATAGRID As String = "WPF Data Grid"
    Private Const _STR_WPFLISTVIEW As String = "WPF ListView"
    Private _intNumberOfColumnGroups As Integer = 2
    Private _objClassEntity As ClassEntity
    Private _strBusinessForm As String = String.Empty

    '
    Private Enum SelectClassMemberUserControlState
        Minimized
        Restored
    End Enum

#End Region

#Region " Dependency Properties "

    Public Shared ReadOnly ShowFullDynamicFormContentProperty As DependencyProperty = DependencyProperty.Register("ShowFullDynamicFormContent", GetType(Boolean), GetType(Window), New PropertyMetadata(True))

#End Region

#Region " Properties "

    Public ReadOnly Property BusinessForm() As String
        Get
            Return _strBusinessForm
        End Get
    End Property

    Public ReadOnly Property ClassEntity() As ClassEntity
        Get
            Return _objClassEntity
        End Get
    End Property

    Public Property NumberOfColumnGroups() As Integer
        Get
            Return _intNumberOfColumnGroups
        End Get
        Set(ByVal Value As Integer)
            _intNumberOfColumnGroups = Value
        End Set
    End Property

    Public ReadOnly Property PlatformType() As UIPlatform
        Get

            If Me.ClassEntity.IsSilverlight Then
                Return UIPlatform.Silverlight

            Else
                Return UIPlatform.WPF
            End If

        End Get
    End Property

    Public Property ShowFullDynamicFormContent() As Boolean
        Get
            Return CType(GetValue(ShowFullDynamicFormContentProperty), Boolean)
        End Get
        Set(ByVal value As Boolean)
            SetValue(ShowFullDynamicFormContentProperty, value)
        End Set
    End Property

#End Region

#Region " Constructors "

    Public Sub New(ByVal objClassEntity As ClassEntity)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        _objClassEntity = objClassEntity
    End Sub

#End Region

#Region " Methods "

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        DialogResult = False
    End Sub

    Private Sub btnClearnAllFields_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        ClearAllListBoxFields()
    End Sub

    Private Sub btnCreateForm_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.txtBindingPropertyPrefix.Text = Me.txtBindingPropertyPrefix.Text.Trim

        If Me.txtBindingPropertyPrefix.Text.Length > 0 AndAlso Not Me.txtBindingPropertyPrefix.Text.Trim.EndsWith(".") Then
            Me.txtBindingPropertyPrefix.Text = Me.txtBindingPropertyPrefix.Text & "."
        End If

        Select Case CType(Me.cboSelectObjectToCreate.SelectedValue, String)

            Case _STR_BUSINESSFORM
                CreateBusinessForm()

            Case _STR_WPFLISTVIEW
                CreateListView()

            Case _STR_WPFDATAGRID
                CreateWPFDataGrid()

            Case _STR_SILVERLIGHTDATAGRID
                CreateSilverlightDataGrid()

            Case _STR_SILVERLIGHTDATAFORM
                CreateSilverlightDataForm()

            Case Else
                MessageBox.Show("Selection " & Me.cboSelectObjectToCreate.SelectedValue.ToString & ", not programmed", "Bummer", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK)
        End Select

    End Sub

    Private Sub cboSelectObjectToCreate_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs)

        If Me.cboSelectObjectToCreate Is Nothing OrElse Me.wpBusinessForm Is Nothing OrElse Me.cboSelectObjectToCreate.SelectedIndex = -1 Then
            Exit Sub
        End If

        Me.wpBusinessForm.Visibility = Windows.Visibility.Collapsed
        Me.wpListView.Visibility = Windows.Visibility.Collapsed
        Me.wpWPFDataGrid.Visibility = Windows.Visibility.Collapsed
        Me.wpSilverlightDataGrid.Visibility = Windows.Visibility.Collapsed
        Me.wpSilverlightDataForm.Visibility = Windows.Visibility.Collapsed

        If String.Compare(_STR_BUSINESSFORM, CType(Me.cboSelectObjectToCreate.SelectedValue, String)) = 0 Then
            Me.wpBusinessForm.Visibility = Windows.Visibility.Visible

        ElseIf String.Compare(_STR_WPFLISTVIEW, CType(Me.cboSelectObjectToCreate.SelectedValue, String)) = 0 Then
            Me.wpListView.Visibility = Windows.Visibility.Visible

        ElseIf String.Compare(_STR_WPFDATAGRID, CType(Me.cboSelectObjectToCreate.SelectedValue, String)) = 0 Then
            Me.wpWPFDataGrid.Visibility = Windows.Visibility.Visible

        ElseIf String.Compare(_STR_SILVERLIGHTDATAGRID, CType(Me.cboSelectObjectToCreate.SelectedValue, String)) = 0 Then
            Me.wpSilverlightDataGrid.Visibility = Windows.Visibility.Visible

        ElseIf String.Compare(_STR_SILVERLIGHTDATAFORM, CType(Me.cboSelectObjectToCreate.SelectedValue, String)) = 0 Then
            Me.wpSilverlightDataForm.Visibility = Windows.Visibility.Visible
        End If

        ClearAllListBoxFields()
        ClearColumnsExceptFirstColumn(1)
    End Sub

    Private Sub ClearAllListBoxFields()

        For Each obj As Object In Me.gridColumnsContainer.Children

            If TypeOf obj Is ListBox Then
                DirectCast(obj, ListBox).Items.Clear()
            End If

        Next

        For Each pi As PropertyInformation In Me.ClassEntity.PropertyInfomation
            pi.HasBeenUsed = False
        Next

        Dim objCollectionView As CollectionView = TryCast(CollectionViewSource.GetDefaultView(Me.ClassEntity.PropertyInfomation), CollectionView)

        If objCollectionView IsNot Nothing Then
            objCollectionView.Refresh()
        End If

    End Sub

    Private Sub ClearColumnsExceptFirstColumn(ByVal intNumberOfColumnGroups As Integer)

        If _intNumberOfColumnGroups <> intNumberOfColumnGroups Then

            If intNumberOfColumnGroups > _intNumberOfColumnGroups Then

                For intX As Integer = _intNumberOfColumnGroups To intNumberOfColumnGroups - 1
                    Me.gridColumnsContainer.ColumnDefinitions.Insert(Me.gridColumnsContainer.ColumnDefinitions.Count - 2, New ColumnDefinition With {.Width = New GridLength(425, GridUnitType.Pixel), .MinWidth = 50})
                    Me.gridColumnsContainer.ColumnDefinitions.Insert(Me.gridColumnsContainer.ColumnDefinitions.Count - 2, New ColumnDefinition With {.Width = New GridLength(0, GridUnitType.Auto)})

                    Dim lb As ListBox = DynamicFormContentListBoxFactory(Me.gridColumnsContainer.ColumnDefinitions.Count - 2)
                    Me.gridColumnsContainer.Children.Add(lb)

                    Dim objGridSplitter As New GridSplitter With {.HorizontalAlignment = Windows.HorizontalAlignment.Right}
                    objGridSplitter.SetValue(Grid.ColumnProperty, Me.gridColumnsContainer.ColumnDefinitions.Count - 2)
                    Me.gridColumnsContainer.Children.Add(objGridSplitter)
                Next

            Else

                Dim intLastColumnIndexToKeep As Integer = (intNumberOfColumnGroups * 2) - 1
                Dim objListOfGridSplittersToRemove As New List(Of GridSplitter)
                Dim objListOfListBoxesToRemove As New List(Of ListBox)

                For Each obj As Object In Me.gridColumnsContainer.Children

                    If TypeOf obj Is GridSplitter Then

                        If CType(CType(obj, GridSplitter).GetValue(Grid.ColumnProperty), Integer) > intLastColumnIndexToKeep Then
                            objListOfGridSplittersToRemove.Add(CType(obj, GridSplitter))
                        End If

                    ElseIf TypeOf obj Is ListBox Then

                        If CType(CType(obj, ListBox).GetValue(Grid.ColumnProperty), Integer) > intLastColumnIndexToKeep Then
                            objListOfListBoxesToRemove.Add(CType(obj, ListBox))
                        End If

                    End If

                Next

                For Each obj As GridSplitter In objListOfGridSplittersToRemove
                    Me.gridColumnsContainer.Children.Remove(obj)
                Next

                For Each objListBox As ListBox In objListOfListBoxesToRemove

                    For Each objDynamicFormEditor As DynamicFormEditor In objListBox.Items

                        Dim strPropertyName As String = CType(objDynamicFormEditor.DataContext, DynamicFormListBoxContent).BindingPath

                        For Each objPi As PropertyInformation In Me.ClassEntity.PropertyInfomation

                            If objPi.Name = strPropertyName Then
                                objPi.HasBeenUsed = False
                            End If

                        Next

                    Next

                    objListBox.Items.Clear()
                    Me.gridColumnsContainer.Children.Remove(objListBox)
                Next

                For intx = Me.gridColumnsContainer.ColumnDefinitions.Count - 1 To intLastColumnIndexToKeep Step -1
                    Me.gridColumnsContainer.ColumnDefinitions.RemoveAt(intx)
                Next

                Me.gridColumnsContainer.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(0, GridUnitType.Auto)})
            End If

            Dim objCollectionView As CollectionView = TryCast(CollectionViewSource.GetDefaultView(Me.ClassEntity.PropertyInfomation), CollectionView)

            If objCollectionView IsNot Nothing Then
                objCollectionView.Refresh()
            End If

            _intNumberOfColumnGroups = intNumberOfColumnGroups
        End If

    End Sub

    Private Sub CreateBusinessForm()

        Dim bolInsertingTitleRow As Boolean = Not String.IsNullOrEmpty(Me.txtFormTitle.Text)
        Dim objColumnGroupListBox As New List(Of ListBox)

        For Each obj As Object In Me.gridColumnsContainer.Children

            If TypeOf obj Is ListBox Then
                objColumnGroupListBox.Add(DirectCast(obj, ListBox))
            End If

        Next

        Dim intNumberOfColumns As Integer = (objColumnGroupListBox.Count * 3) - 1
        Dim intNumberOfRows As Integer
        Dim intLastGridRowIndex As Integer

        For Each lb As ListBox In objColumnGroupListBox
            intNumberOfRows = Math.Max(intNumberOfRows, lb.Items.Count)
        Next

        If intNumberOfColumns = 0 OrElse intNumberOfRows = 0 Then
            MessageBox.Show("You do not have any properties added to the layout.", "Invalid Layout", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            Exit Sub
        End If

        intLastGridRowIndex = intNumberOfRows

        Dim sb As New StringBuilder(10240)

        If Me.chkWrapInBorder.IsChecked.HasValue AndAlso Me.chkWrapInBorder.IsChecked.Value = True Then
            sb.AppendLine(UIControlFactory.Instance.GetUIControl(UIControlRole.Border, Me.PlatformType).MakeControlFromDefaults(String.Empty, False, String.Empty))
        End If

        sb.AppendLine(UIControlFactory.Instance.GetUIControl(UIControlRole.Grid, Me.PlatformType).MakeControlFromDefaults(String.Empty, False, String.Empty))
        sb.AppendLine("<Grid.RowDefinitions>")

        If bolInsertingTitleRow Then
            sb.AppendLine("<RowDefinition Height=""Auto"" />")
            intLastGridRowIndex += 1
        End If

        For intX As Integer = 1 To intNumberOfRows
            sb.AppendLine("<RowDefinition Height=""Auto"" />")
        Next

        If Me.chkIncludeButtonRow.IsChecked.HasValue AndAlso Me.chkIncludeButtonRow.IsChecked.Value = True Then
            sb.AppendLine("<RowDefinition Height=""Auto"" />")
        End If

        sb.AppendLine("</Grid.RowDefinitions>")
        sb.AppendLine("<Grid.ColumnDefinitions>")

        For intX As Integer = 0 To objColumnGroupListBox.Count - 1
            sb.AppendFormat("<ColumnDefinition Width=""{0}"" />{1}", 100, vbCrLf)
            sb.AppendLine("<ColumnDefinition Width=""Auto"" />")

            If intX < objColumnGroupListBox.Count - 1 Then
                'this inserts the spacer column between the groups of columns
                sb.AppendFormat("<ColumnDefinition Width=""{0}"" />{1}", 10, vbCrLf)
            End If

        Next

        sb.AppendLine("</Grid.ColumnDefinitions>")
        sb.AppendLine()

        If bolInsertingTitleRow Then
            sb.AppendLine(UIControlFactory.Instance.GetUIControl(UIControlRole.TextBlock, Me.PlatformType).MakeControlFromDefaults(String.Format(" Grid.Column=""0"" Grid.Row=""0"" Grid.ColumnSpan=""{0}"" Text=""{1}"" ", intNumberOfColumns, Me.txtFormTitle.Text), True, String.Empty))
        End If

        Dim intCurrentRow As Integer

        For intX As Integer = 0 To objColumnGroupListBox.Count - 1

            If bolInsertingTitleRow Then
                intCurrentRow = 1

            Else
                intCurrentRow = 0
            End If

            For Each objDynamicFormEditor As DynamicFormEditor In objColumnGroupListBox(intX).Items

                Dim objField As DynamicFormListBoxContent = CType(objDynamicFormEditor.DataContext, DynamicFormListBoxContent)

                If Not String.IsNullOrEmpty(objField.AssociatedLabel) Then
                    sb.AppendLine(UIControlFactory.Instance.MakeLabelWithoutBinding(Me.PlatformType, intX * 3, intCurrentRow, objField.AssociatedLabel))
                End If

                intCurrentRow += 1
            Next

            sb.AppendLine()
        Next

        sb.AppendLine()

        For intX As Integer = 0 To objColumnGroupListBox.Count - 1

            If bolInsertingTitleRow Then
                intCurrentRow = 1

            Else
                intCurrentRow = 0
            End If

            For Each objDynamicFormEditor As DynamicFormEditor In objColumnGroupListBox(intX).Items

                Dim objField As DynamicFormListBoxContent = CType(objDynamicFormEditor.DataContext, DynamicFormListBoxContent)
                Dim strBindingPath As String = String.Concat(Me.txtBindingPropertyPrefix.Text, objField.BindingPath)

                Select Case objField.ControlType

                    Case DynamicFormControlType.DatePicker
                        sb.AppendLine(UIControlFactory.Instance.MakeDatePicker(Me.PlatformType, (intX * 3) + 1, intCurrentRow, strBindingPath, objField.Width))

                    Case DynamicFormControlType.CheckBox
                        sb.AppendLine(UIControlFactory.Instance.MakeCheckBox(Me.PlatformType, (intX * 3) + 1, intCurrentRow, objField.ControlLabel, strBindingPath, objField.BindingMode))

                    Case DynamicFormControlType.ComboBox
                        sb.AppendLine(UIControlFactory.Instance.MakeComboBox(Me.PlatformType, (intX * 3) + 1, intCurrentRow, strBindingPath, objField.BindingMode))

                    Case DynamicFormControlType.Image
                        sb.AppendLine(UIControlFactory.Instance.MakeImage(Me.PlatformType, (intX * 3) + 1, intCurrentRow, strBindingPath))

                    Case DynamicFormControlType.Label

                        If ClassEntity.IsSilverlight Then
                            sb.AppendLine(WriteSilverlightStringFomatComment(objField.StringFormat))
                        End If

                        sb.AppendLine(UIControlFactory.Instance.MakeLabel(Me.PlatformType, (intX * 3) + 1, intCurrentRow, strBindingPath, objField.StringFormat, ClassEntity.SilverlightVersion))

                    Case DynamicFormControlType.TextBlock

                        If ClassEntity.IsSilverlight Then
                            sb.AppendLine(WriteSilverlightStringFomatComment(objField.StringFormat))
                        End If

                        sb.AppendLine(UIControlFactory.Instance.MakeTextBlock(Me.PlatformType, (intX * 3) + 1, intCurrentRow, strBindingPath, objField.StringFormat, ClassEntity.SilverlightVersion))

                    Case DynamicFormControlType.TextBox

                        If ClassEntity.IsSilverlight Then
                            sb.AppendLine(WriteSilverlightStringFomatComment(objField.StringFormat))
                        End If

                        sb.AppendLine(UIControlFactory.Instance.MakeTextBox(Me.PlatformType, (intX * 3) + 1, intCurrentRow, strBindingPath, BindingMode.TwoWay, objField.Width, objField.MaximumLength, objField.StringFormat, objField.DataType.StartsWith("Nullable"), ClassEntity.SilverlightVersion))
                End Select

                intCurrentRow += 1
            Next

            sb.AppendLine()
        Next

        If Me.chkIncludeButtonRow.IsChecked.HasValue AndAlso Me.chkIncludeButtonRow.IsChecked.Value = True Then

            If Not Me.ClassEntity.IsSilverlight Then
                sb.AppendFormat("<Grid Grid.Column=""0"" Grid.Row=""{0}"" Grid.ColumnSpan=""{1}"" Grid.IsSharedSizeScope=""True"" HorizontalAlignment=""Right"">", intLastGridRowIndex, intNumberOfColumns)
                sb.AppendLine()
                sb.AppendLine("    <Grid.ColumnDefinitions>")
                sb.AppendLine("        <ColumnDefinition SharedSizeGroup=""Buttons"" />")
                sb.AppendLine("        <ColumnDefinition SharedSizeGroup=""Buttons"" />")
                sb.AppendLine("    </Grid.ColumnDefinitions>")
                sb.AppendLine("    <Button Content=""OK"" Padding=""3.5,0,3.5,0"" Margin=""3"" />")
                sb.AppendLine("    <Button Content=""Cancel"" Padding=""3.5,0,3.5,0"" Grid.Column=""1"" Margin=""3.5""/>")
                sb.AppendLine("</Grid>")
                sb.AppendLine("")

            Else
                sb.AppendFormat("<Grid Grid.Column=""0"" Grid.Row=""{0}"" Grid.ColumnSpan=""{1}"" HorizontalAlignment=""Right"">", intLastGridRowIndex, intNumberOfColumns)
                sb.AppendLine()
                sb.AppendLine("    <Grid.ColumnDefinitions>")
                sb.AppendLine("        <ColumnDefinition />")
                sb.AppendLine("        <ColumnDefinition />")
                sb.AppendLine("    </Grid.ColumnDefinitions>")
                sb.AppendLine("    <Button Content=""OK"" Padding=""3.5,0,3.5,0"" Margin=""3.5"" />")
                sb.AppendLine("    <Button Content=""Cancel"" Padding=""3.5,0,3.5,0"" Grid.Column=""1"" Margin=""3.5""/>")
                sb.AppendLine("</Grid>")
                sb.AppendLine("")
            End If

        End If

        sb.Replace(" >", ">")
        sb.Replace("    ", " ")
        sb.Replace("   ", " ")
        sb.Replace("  ", " ")
        sb.AppendLine(GetCloseTagForControlFromDefaults(UIControlRole.Grid))

        If Me.chkWrapInBorder.IsChecked.HasValue AndAlso Me.chkWrapInBorder.IsChecked.Value = True Then
            sb.AppendLine(GetCloseTagForControlFromDefaults(UIControlRole.Border))
        End If

        _strBusinessForm = sb.ToString
        Me.DialogResult = True
    End Sub

    Private Sub CreateSilverlightDataForm()

        Dim objColumnGroupListBox As New List(Of ListBox)

        For Each obj As Object In Me.gridColumnsContainer.Children

            If TypeOf obj Is ListBox Then
                objColumnGroupListBox.Add(DirectCast(obj, ListBox))
            End If

        Next

        Dim intNumberOfColumns As Integer = objColumnGroupListBox.Count + 1
        Dim intNumberOfRows As Integer
        Dim intLastGridRowIndex As Integer

        For Each lb As ListBox In objColumnGroupListBox
            intNumberOfRows = Math.Max(intNumberOfRows, lb.Items.Count)
        Next

        If intNumberOfColumns = 0 OrElse intNumberOfRows = 0 Then
            MessageBox.Show("You do not have any properties added to the layout.", "Invalid Layout", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            Exit Sub
        End If

        intLastGridRowIndex = intNumberOfRows

        Dim sb As New StringBuilder(10240)
        sb.AppendLine("")
        sb.AppendLine("<!-- Add to your root tag if required")
        sb.AppendLine("")
        sb.AppendLine("xmlns:controls=""clr-namespace:System.Windows.Controls;assembly=System.Windows.Controls"" ")
        sb.AppendLine("xmlns:dataFormToolkit=""clr-namespace:System.Windows.Controls;assembly=System.Windows.Controls.Data.DataForm.Toolkit"" ")
        sb.AppendLine("")
        sb.AppendLine("-->")
        sb.AppendLine("")
        sb.Append("<dataFormToolkit:DataForm  AutoGenerateFields=""False"" ")

        If Not String.IsNullOrEmpty(Me.txtDataFormHeader.Text) Then
            sb.AppendFormat("Header=""{0}"" ", Me.txtDataFormHeader.Text)
        End If

        sb.AppendLine(">")

        '--------------------------------------------------------------------------------------------------------------------------------------------------
        'this builds a separate stringbuilder that will be used to create the three templates as required.
        Dim sb2 As New StringBuilder(10240)
        sb2.AppendLine("<dataFormToolkit:DataForm.EditTemplate>")
        sb2.AppendLine("<DataTemplate>")
        sb2.AppendLine(UIControlFactory.Instance.GetUIControl(UIControlRole.Grid, Me.PlatformType).MakeControlFromDefaults(String.Empty, False, String.Empty))
        sb2.AppendLine("<Grid.RowDefinitions>")

        For intX As Integer = 1 To intNumberOfRows
            sb2.AppendLine("<RowDefinition Height=""Auto"" />")
        Next

        sb2.AppendLine("</Grid.RowDefinitions>")
        sb2.AppendLine("<Grid.ColumnDefinitions>")

        For intX As Integer = 0 To objColumnGroupListBox.Count - 1
            sb2.AppendLine("<ColumnDefinition />")

            If intX < objColumnGroupListBox.Count - 1 Then
                'this inserts the spacer column between the groups of columns
                sb2.AppendFormat("<ColumnDefinition Width=""{0}"" />{1}", 10, vbCrLf)
            End If

        Next

        sb2.AppendLine("</Grid.ColumnDefinitions>")
        sb2.AppendLine()

        Dim intCurrentRow As Integer
        Dim intCurrentColumn As Integer

        For intX As Integer = 0 To objColumnGroupListBox.Count - 1
            intCurrentRow = 0
            intCurrentColumn = intX * 2

            For Each objDynamicFormEditor As DynamicFormEditor In objColumnGroupListBox(intX).Items

                Dim objField As DynamicFormListBoxContent = CType(objDynamicFormEditor.DataContext, DynamicFormListBoxContent)
                Dim strBindingPath As String = String.Concat(Me.txtBindingPropertyPrefix.Text, objField.BindingPath)
                sb2.AppendFormat("<dataFormToolkit:DataField Grid.Row=""{0}"" Grid.Column=""{1}"" ", intCurrentRow, intCurrentColumn)

                If Not String.IsNullOrEmpty(objField.FieldDescription) Then
                    sb2.AppendFormat("Description=""{0}"" ", objField.FieldDescription)
                    sb2.AppendFormat("DescriptionViewerPosition=""{0}"" ", objField.DescriptionViewerPosition)
                End If

                If Not String.IsNullOrEmpty(objField.AssociatedLabel) Then
                    sb2.AppendFormat("Label=""{0}"" ", objField.AssociatedLabel)
                    sb2.AppendFormat("LabelPosition=""{0}"" ", objField.LabelPosition)
                End If

                sb2.AppendLine(">")

                Dim strStringFormatNotice As String = WriteSilverlightStringFomatComment(objField.StringFormat)

                Select Case objField.ControlType

                    Case DynamicFormControlType.DatePicker
                        sb2.AppendLine(UIControlFactory.Instance.MakeDatePicker(Me.PlatformType, Nothing, Nothing, strBindingPath, objField.Width))

                    Case DynamicFormControlType.CheckBox
                        sb2.AppendLine(UIControlFactory.Instance.MakeCheckBox(Me.PlatformType, Nothing, Nothing, objField.ControlLabel, strBindingPath, objField.BindingMode))

                    Case DynamicFormControlType.ComboBox
                        sb2.AppendLine(UIControlFactory.Instance.MakeComboBox(Me.PlatformType, Nothing, Nothing, strBindingPath, objField.BindingMode))

                    Case DynamicFormControlType.Image
                        sb2.AppendLine(UIControlFactory.Instance.MakeImage(Me.PlatformType, Nothing, Nothing, strBindingPath))

                    Case DynamicFormControlType.Label

                        If ClassEntity.IsSilverlight AndAlso Not String.IsNullOrEmpty(strStringFormatNotice) Then
                            sb2.AppendLine(strStringFormatNotice)
                        End If

                        sb2.AppendLine(UIControlFactory.Instance.MakeLabel(Me.PlatformType, Nothing, Nothing, strBindingPath, objField.StringFormat, ClassEntity.SilverlightVersion))

                    Case DynamicFormControlType.TextBlock

                        If ClassEntity.IsSilverlight AndAlso Not String.IsNullOrEmpty(strStringFormatNotice) Then
                            sb2.AppendLine(strStringFormatNotice)
                        End If

                        sb2.AppendLine(UIControlFactory.Instance.MakeTextBlock(Me.PlatformType, Nothing, Nothing, strBindingPath, objField.StringFormat, ClassEntity.SilverlightVersion))

                    Case DynamicFormControlType.TextBox

                        If ClassEntity.IsSilverlight AndAlso Not String.IsNullOrEmpty(strStringFormatNotice) Then
                            sb2.AppendLine(strStringFormatNotice)
                        End If

                        sb2.AppendLine(UIControlFactory.Instance.MakeTextBox(Me.PlatformType, Nothing, Nothing, strBindingPath, BindingMode.TwoWay, objField.Width, objField.MaximumLength, objField.StringFormat, objField.DataType.StartsWith("Nullable"), ClassEntity.SilverlightVersion))
                End Select

                sb2.AppendLine("</dataFormToolkit:DataField>")
                sb2.AppendLine("")
                intCurrentRow += 1
            Next

        Next

        sb2.AppendLine(GetCloseTagForControlFromDefaults(UIControlRole.Grid))
        sb2.AppendLine("</DataTemplate>")
        sb2.AppendLine("</dataFormToolkit:DataForm.EditTemplate>")

        '--------------------------------------------------------------------------------------------------------------------------------------------------
        If Me.chkRenderEditTemplate.IsChecked = True Then
            sb.Append(sb2.ToString)
        End If

        If Me.chkRenderReadOnlyTemplate.IsChecked = True Then
            sb2.AppendLine("")
            sb2.AppendLine("")
            sb.Append(sb2.ToString.Replace(".EditTemplate", ".ReadOnlyTemplate"))
        End If

        If Me.chkRenderNewItemTemplate.IsChecked = True Then
            sb2.AppendLine("")
            sb2.AppendLine("")
            sb.Append(sb2.ToString.Replace(".EditTemplate", ".NewItemTemplate"))
        End If

        sb.Append("</dataFormToolkit:DataForm>")
        sb.Replace(" >", ">")
        sb.Replace("    ", " ")
        sb.Replace("   ", " ")
        sb.Replace("  ", " ")
        _strBusinessForm = sb.ToString
        Me.DialogResult = True
    End Sub

    Private Sub CreateBusinessFormFromClass_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        InitialLayoutOfDynamicForms()
        ShowFullDynamicFormContent = True
        Me.Title = String.Concat("Create Business Form For Class: ", Me.ClassEntity.ClassName)

        Dim obj As New List(Of String)
        obj.Add(_STR_BUSINESSFORM)

        If Me.ClassEntity.IsSilverlight Then
            obj.Add(_STR_SILVERLIGHTDATAGRID)
            obj.Add(_STR_SILVERLIGHTDATAFORM)

        Else
            obj.Add(_STR_WPFLISTVIEW)
            obj.Add(_STR_WPFDATAGRID)
        End If

        Me.cboSelectObjectToCreate.ItemsSource = obj
        Me.cboSelectObjectToCreate.SelectedIndex = 0
    End Sub

    Private Sub CreateListView()

        Dim objListBox As ListBox = Nothing

        For Each obj As Object In Me.gridColumnsContainer.Children

            If TypeOf obj Is ListBox Then
                objListBox = DirectCast(obj, ListBox)
                Exit For
            End If

        Next

        If objListBox Is Nothing Then
            MessageBox.Show("Unable to get the ListBox used for layout.", "Missing ListBox", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            Exit Sub
        End If

        If objListBox.Items.Count = 0 Then
            MessageBox.Show("You do not have any properties added to the layout.", "Invalid Layout", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            Exit Sub
        End If

        Dim sb As New StringBuilder(10240)
        sb.AppendLine("<ListView>")
        sb.AppendLine("    <ListView.ItemContainerStyle>")
        sb.AppendLine("        <Style TargetType=""ListViewItem"">")
        sb.AppendLine("            <Setter Property=""HorizontalContentAlignment"" Value=""Stretch"" />")
        sb.AppendLine("        </Style>")
        sb.AppendLine("    </ListView.ItemContainerStyle>")
        sb.AppendLine("    <ListView.View>")
        sb.AppendLine("        <GridView>")

        For Each objDynamicFormEditor As DynamicFormEditor In objListBox.Items

            Dim objField As DynamicFormListBoxContent = CType(objDynamicFormEditor.DataContext, DynamicFormListBoxContent)
            Dim strBindingPath As String = String.Concat(Me.txtBindingPropertyPrefix.Text, objField.BindingPath)

            If String.IsNullOrEmpty(objField.StringFormat) Then
                sb.AppendFormat("<GridViewColumn Header=""{0}"" DisplayMemberBinding=""{{Binding Path={1}}}"" />", objField.AssociatedLabel, strBindingPath)

            Else
                sb.AppendFormat("<GridViewColumn Header=""{0}"" >", objField.AssociatedLabel)
                sb.AppendLine()
                sb.AppendLine("    <GridViewColumn.CellTemplate>")
                sb.AppendLine("        <DataTemplate>")

                If objField.DataType.Contains("Decimal") OrElse objField.DataType.Contains("Double") OrElse objField.DataType.Contains("Integer") Then
                    sb.AppendFormat("            <TextBlock TextAlignment=""Right"" Text=""{{Binding Path={0}, StringFormat={1}}}"" />", strBindingPath, objField.StringFormat.Replace("{", "\{").Replace("}", "\}"))
                    sb.AppendLine()

                Else
                    sb.AppendFormat("            <TextBlock Text=""{{Binding Path={0}, StringFormat={1}}}"" />", strBindingPath, objField.StringFormat.Replace("{", "\{").Replace("}", "\}"))
                    sb.AppendLine()
                End If

                sb.AppendLine("        </DataTemplate>")
                sb.AppendLine("    </GridViewColumn.CellTemplate>")
                sb.AppendLine("</GridViewColumn>")
            End If

            sb.AppendLine()
        Next

        sb.AppendLine("        </GridView>")
        sb.AppendLine("    </ListView.View>")
        sb.AppendLine("</ListView>")
        sb.Replace(" >", ">")
        sb.Replace("    ", " ")
        sb.Replace("   ", " ")
        sb.Replace("  ", " ")
        _strBusinessForm = sb.ToString
        Me.DialogResult = True
    End Sub

    Private Sub CreateSilverlightDataGrid()

        Dim objListBox As ListBox = Nothing

        For Each obj As Object In Me.gridColumnsContainer.Children

            If TypeOf obj Is ListBox Then
                objListBox = DirectCast(obj, ListBox)
                Exit For
            End If

        Next

        If objListBox Is Nothing Then
            MessageBox.Show("Unable to get the ListBox used for layout.", "Missing ListBox", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            Exit Sub
        End If

        If objListBox.Items.Count = 0 Then
            MessageBox.Show("You do not have any properties added to the layout.", "Invalid Layout", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            Exit Sub
        End If

        Dim bolHasDatePicker As Boolean = False
        Dim bolHeaderHasContent As Boolean = False
        Dim sb As New StringBuilder(10240)
        Dim sbHeader As New StringBuilder(1024)
        Dim strDataGridTag As String = UIControlFactory.Instance.GetUIControl(UIControlRole.DataGrid, UIPlatform.Silverlight).MakeControlFromDefaults(String.Empty, False, String.Empty)
        Dim strDataGridNamespace As String = String.Empty

        If strDataGridTag.Contains(":") Then
            strDataGridNamespace = strDataGridTag.Substring(1, strDataGridTag.IndexOf(":"))
            sbHeader.AppendLine("<!--The following namespace declarations may be necessary for you to add to the root element of this XAML file.-->")
            sbHeader.AppendLine(String.Format("<!--xmlns:{0}=""clr-namespace:System.Windows.Controls;assembly=System.Windows.Controls.Data""-->", strDataGridNamespace.Replace(":", String.Empty)))
            bolHeaderHasContent = True
        End If

        sb.AppendLine(strDataGridTag)
        sb.AppendFormat("<{0}DataGrid.Columns>", strDataGridNamespace)
        sb.AppendLine()

        For Each objDynamicFormEditor As DynamicFormEditor In objListBox.Items

            Dim objField As DynamicFormListBoxContent = CType(objDynamicFormEditor.DataContext, DynamicFormListBoxContent)
            Dim strBindingPath As String = String.Concat(Me.txtBindingPropertyPrefix.Text, objField.BindingPath)

            If objField.RenderAsGridTempldateColumn OrElse objField.ControlType = DynamicFormControlType.Image OrElse objField.ControlType = DynamicFormControlType.ComboBox OrElse objField.ControlType = DynamicFormControlType.DatePicker Then

                Select Case objField.ControlType

                    Case DynamicFormControlType.DatePicker
                        bolHasDatePicker = True
                        sb.AppendLine(String.Format("<{0}DataGridTemplateColumn Header=""{1}"" SortMemberPath=""{2}""> ", strDataGridNamespace, objField.AssociatedLabel, strBindingPath))
                        sb.AppendLine(String.Format("<{0}DataGridTemplateColumn.CellTemplate> ", strDataGridNamespace))
                        sb.AppendLine("<DataTemplate>")
                        sb.AppendLine(WriteSilverlightStringFomatComment(objField.StringFormat))
                        sb.AppendLine(UIControlFactory.Instance.MakeTextBlock(UIPlatform.Silverlight, Nothing, Nothing, strBindingPath, objField.StringFormat, ClassEntity.SilverlightVersion))
                        sb.AppendLine("</DataTemplate>")
                        sb.AppendLine(String.Format("</{0}DataGridTemplateColumn.CellTemplate> ", strDataGridNamespace))
                        sb.AppendLine(String.Format("<{0}DataGridTemplateColumn.CellEditingTemplate> ", strDataGridNamespace))
                        sb.AppendLine("<DataTemplate>")
                        sb.AppendLine(WriteSilverlightStringFomatComment(objField.StringFormat))
                        sb.AppendLine(UIControlFactory.Instance.MakeDatePicker(UIPlatform.Silverlight, Nothing, Nothing, strBindingPath, objField.Width))
                        sb.AppendLine("</DataTemplate>")
                        sb.AppendLine(String.Format("</{0}DataGridTemplateColumn.CellEditingTemplate> ", strDataGridNamespace))
                        sb.AppendLine(String.Format("</{0}DataGridTemplateColumn> ", strDataGridNamespace))

                    Case DynamicFormControlType.CheckBox
                        sb.AppendLine(String.Format("<{0}DataGridTemplateColumn Header=""{1}"" SortMemberPath=""{2}""> ", strDataGridNamespace, objField.AssociatedLabel, strBindingPath))
                        sb.AppendLine(String.Format("<{0}DataGridTemplateColumn.CellTemplate> ", strDataGridNamespace))
                        sb.AppendLine("<DataTemplate>")
                        sb.AppendLine(UIControlFactory.Instance.MakeCheckBox(UIPlatform.Silverlight, Nothing, Nothing, String.Empty, strBindingPath, BindingMode.TwoWay))
                        sb.AppendLine("</DataTemplate>")
                        sb.AppendLine(String.Format("</{0}DataGridTemplateColumn.CellTemplate> ", strDataGridNamespace))
                        sb.AppendLine(String.Format("</{0}DataGridTemplateColumn> ", strDataGridNamespace))

                    Case DynamicFormControlType.ComboBox
                        sb.AppendLine(String.Format("<{0}DataGridTemplateColumn Header=""{1}"" SortMemberPath=""{2}""> ", strDataGridNamespace, objField.AssociatedLabel, strBindingPath))
                        sb.AppendLine(String.Format("<{0}DataGridTemplateColumn.CellTemplate> ", strDataGridNamespace))
                        sb.AppendLine("<DataTemplate>")
                        sb.AppendLine("<!-- Bind Silverlight ComboBox in code after its ItemsSource has been loaded -->")
                        sb.AppendLine(UIControlFactory.Instance.MakeComboBox(UIPlatform.Silverlight, Nothing, Nothing, strBindingPath, BindingMode.TwoWay))
                        sb.AppendLine("</DataTemplate>")
                        sb.AppendLine(String.Format("</{0}DataGridTemplateColumn.CellTemplate> ", strDataGridNamespace))
                        sb.AppendLine(String.Format("</{0}DataGridTemplateColumn> ", strDataGridNamespace))

                    Case DynamicFormControlType.Image
                        sb.AppendLine(String.Format("<{0}DataGridTemplateColumn Header=""{1}"" SortMemberPath=""{2}""> ", strDataGridNamespace, objField.AssociatedLabel, strBindingPath))
                        sb.AppendLine(String.Format("<{0}DataGridTemplateColumn.CellTemplate> ", strDataGridNamespace))
                        sb.AppendLine("<DataTemplate>")
                        sb.AppendLine(String.Format("<Image Source=""{0}""/>", objField.BindingPath))
                        sb.AppendLine("</DataTemplate>")
                        sb.AppendLine(String.Format("</{0}DataGridTemplateColumn.CellTemplate> ", strDataGridNamespace))
                        sb.AppendLine(String.Format("</{0}DataGridTemplateColumn> ", strDataGridNamespace))

                    Case DynamicFormControlType.Label
                        sb.AppendLine(String.Format("<{0}DataGridTemplateColumn Header=""{1}"" SortMemberPath=""{2}""> ", strDataGridNamespace, objField.AssociatedLabel, strBindingPath))
                        sb.AppendLine(String.Format("<{0}DataGridTemplateColumn.CellTemplate> ", strDataGridNamespace))
                        sb.AppendLine("<DataTemplate>")
                        sb.AppendLine(WriteSilverlightStringFomatComment(objField.StringFormat))
                        sb.AppendLine(UIControlFactory.Instance.MakeLabel(UIPlatform.Silverlight, Nothing, Nothing, objField.AssociatedLabel, objField.StringFormat, ClassEntity.SilverlightVersion))
                        sb.AppendLine("</DataTemplate>")
                        sb.AppendLine(String.Format("</{0}DataGridTemplateColumn.CellTemplate> ", strDataGridNamespace))
                        sb.AppendLine(String.Format("</{0}DataGridTemplateColumn> ", strDataGridNamespace))

                    Case DynamicFormControlType.TextBlock
                        sb.AppendLine(String.Format("<{0}DataGridTemplateColumn Header=""{1}"" SortMemberPath=""{2}""> ", strDataGridNamespace, objField.AssociatedLabel, strBindingPath))
                        sb.AppendLine(String.Format("<{0}DataGridTemplateColumn.CellTemplate> ", strDataGridNamespace))
                        sb.AppendLine("<DataTemplate>")
                        sb.AppendLine(WriteSilverlightStringFomatComment(objField.StringFormat))
                        sb.AppendLine(UIControlFactory.Instance.MakeTextBlock(UIPlatform.Silverlight, Nothing, Nothing, objField.BindingPath, objField.StringFormat, ClassEntity.SilverlightVersion))
                        sb.AppendLine("</DataTemplate>")
                        sb.AppendLine(String.Format("</{0}DataGridTemplateColumn.CellTemplate> ", strDataGridNamespace))
                        sb.AppendLine(String.Format("</{0}DataGridTemplateColumn> ", strDataGridNamespace))

                    Case DynamicFormControlType.TextBox
                        sb.AppendLine(String.Format("<{0}DataGridTemplateColumn Header=""{1}"" SortMemberPath=""{2}""> ", strDataGridNamespace, objField.AssociatedLabel, strBindingPath))
                        sb.AppendLine(String.Format("<{0}DataGridTemplateColumn.CellTemplate> ", strDataGridNamespace))
                        sb.AppendLine("<DataTemplate>")
                        sb.AppendLine(WriteSilverlightStringFomatComment(objField.StringFormat))
                        sb.AppendLine(UIControlFactory.Instance.MakeTextBlock(UIPlatform.Silverlight, Nothing, Nothing, strBindingPath, objField.StringFormat, ClassEntity.SilverlightVersion))
                        sb.AppendLine("</DataTemplate>")
                        sb.AppendLine(String.Format("</{0}DataGridTemplateColumn.CellTemplate> ", strDataGridNamespace))
                        sb.AppendLine(String.Format("<{0}DataGridTemplateColumn.CellEditingTemplate> ", strDataGridNamespace))
                        sb.AppendLine("<DataTemplate>")
                        sb.AppendLine(WriteSilverlightStringFomatComment(objField.StringFormat))
                        sb.AppendLine(UIControlFactory.Instance.MakeTextBox(UIPlatform.Silverlight, Nothing, Nothing, strBindingPath, BindingMode.TwoWay, objField.Width, objField.MaximumLength, String.Empty, objField.DataType.StartsWith("Nullable"), ClassEntity.SilverlightVersion))
                        sb.AppendLine("</DataTemplate>")
                        sb.AppendLine(String.Format("</{0}DataGridTemplateColumn.CellEditingTemplate> ", strDataGridNamespace))
                        sb.AppendLine(String.Format("</{0}DataGridTemplateColumn> ", strDataGridNamespace))
                End Select

            Else

                Select Case objField.ControlType

                    Case DynamicFormControlType.CheckBox
                        sb.AppendFormat("<{0}DataGridCheckBoxColumn Header=""{1}"" Binding=""{{Binding {2}}}"" SortMemberPath=""{2}"" /> ", strDataGridNamespace, objField.AssociatedLabel, strBindingPath)

                    Case DynamicFormControlType.Label, DynamicFormControlType.TextBlock
                        sb.AppendLine(String.Format("<{0}DataGridTextColumn IsReadOnly=""True"" Header=""{1}"" Binding=""{{Binding {2}}}"" SortMemberPath=""{2}"" />", strDataGridNamespace, objField.AssociatedLabel, strBindingPath))

                    Case DynamicFormControlType.TextBox
                        sb.AppendLine(String.Format("<{0}DataGridTextColumn Header=""{1}"" Binding=""{{Binding {2}}}"" SortMemberPath=""{2}"" />", strDataGridNamespace, objField.AssociatedLabel, strBindingPath))
                End Select

                sb.AppendLine()
            End If

        Next

        sb.AppendFormat("</{0}DataGrid.Columns>", strDataGridNamespace)
        sb.AppendLine(GetCloseTagForControlFromDefaults(UIControlRole.DataGrid))
        sb.AppendLine()
        sb.Replace(" >", ">")
        sb.Replace("    ", " ")
        sb.Replace("   ", " ")
        sb.Replace("  ", " ")

        If bolHeaderHasContent AndAlso bolHasDatePicker Then
            sbHeader.AppendLine("<!--xmlns:controls=""clr-namespace:System.Windows.Controls;assembly=System.Windows.Controls""-->")

        ElseIf Not bolHeaderHasContent AndAlso bolHasDatePicker Then
            sbHeader.AppendLine("<!--The following namespace declarations may be necessary for you to add to the root element of this XAML file.-->")
            sbHeader.AppendLine("<!--xmlns:controls=""clr-namespace:System.Windows.Controls;assembly=System.Windows.Controls""-->")
            bolHeaderHasContent = True
        End If

        If bolHeaderHasContent Then
            _strBusinessForm = String.Concat(sbHeader.ToString, sb.ToString)

        Else
            _strBusinessForm = sb.ToString
        End If

        Me.DialogResult = True
    End Sub

    Private Sub CreateWPFDataGrid()

        Dim objListBox As ListBox = Nothing

        For Each obj As Object In Me.gridColumnsContainer.Children

            If TypeOf obj Is ListBox Then
                objListBox = DirectCast(obj, ListBox)
                Exit For
            End If

        Next

        If objListBox Is Nothing Then
            MessageBox.Show("Unable to get the ListBox used for layout.", "Missing ListBox", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            Exit Sub
        End If

        If objListBox.Items.Count = 0 Then
            MessageBox.Show("You do not have any properties added to the layout.", "Invalid Layout", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            Exit Sub
        End If

        Dim sb As New StringBuilder(10240)
        Dim strDataGridTag As String = UIControlFactory.Instance.GetUIControl(UIControlRole.DataGrid, UIPlatform.WPF).MakeControlFromDefaults(String.Empty, False, String.Empty)
        Dim strDataGridNamespace As String = String.Empty

        If strDataGridTag.Contains(":") Then
            strDataGridNamespace = strDataGridTag.Substring(1, strDataGridTag.IndexOf(":"))
            sb.AppendLine("<!--The following namespace declaration may be necessary for you to add to the root element of this XAML file.-->")
            sb.AppendLine(String.Format("<!--xmlns:{0}=""http://schemas.microsoft.com/wpf/2008/toolkit""-->", strDataGridNamespace.Replace(":", String.Empty)))
        End If

        sb.AppendLine(strDataGridTag)
        sb.AppendFormat("<{0}DataGrid.Columns>", strDataGridNamespace)
        sb.AppendLine()

        For Each objDynamicFormEditor As DynamicFormEditor In objListBox.Items

            Dim objField As DynamicFormListBoxContent = CType(objDynamicFormEditor.DataContext, DynamicFormListBoxContent)
            Dim strBindingPath As String = String.Concat(Me.txtBindingPropertyPrefix.Text, objField.BindingPath)

            Select Case objField.ControlType

                Case DynamicFormControlType.DatePicker
                    sb.AppendLine(String.Format("<{0}DataGridTemplateColumn Header=""{1}"" SortMemberPath=""{2}""> ", strDataGridNamespace, objField.AssociatedLabel, strBindingPath))
                    sb.AppendLine(String.Format("<{0}DataGridTemplateColumn.CellTemplate> ", strDataGridNamespace))
                    sb.AppendLine("<DataTemplate>")
                    sb.AppendLine(UIControlFactory.Instance.MakeTextBlock(UIPlatform.WPF, Nothing, Nothing, objField.BindingPath, "{0:d}", ClassEntity.SilverlightVersion))
                    sb.AppendLine("</DataTemplate>")
                    sb.AppendLine(String.Format("</{0}DataGridTemplateColumn.CellTemplate> ", strDataGridNamespace))
                    sb.AppendLine(String.Format("<{0}DataGridTemplateColumn.CellEditingTemplate> ", strDataGridNamespace))
                    sb.AppendLine("<DataTemplate>")
                    sb.AppendLine(UIControlFactory.Instance.MakeDatePicker(UIPlatform.WPF, Nothing, Nothing, objField.BindingPath, objField.Width))
                    sb.AppendLine("</DataTemplate>")
                    sb.AppendLine(String.Format("</{0}DataGridTemplateColumn.CellEditingTemplate> ", strDataGridNamespace))
                    sb.AppendLine(String.Format("</{0}DataGridTemplateColumn> ", strDataGridNamespace))

                Case DynamicFormControlType.CheckBox
                    sb.AppendFormat("<{0}DataGridCheckBoxColumn Binding=""{{Binding {1}}}"" Header=""{2}""/> ", strDataGridNamespace, strBindingPath, objField.AssociatedLabel)

                Case DynamicFormControlType.ComboBox
                    sb.AppendFormat("<{0}DataGridComboBoxColumn Binding=""{{Binding {1}}}"" Header=""{2}""/> ", strDataGridNamespace, strBindingPath, objField.AssociatedLabel)

                Case DynamicFormControlType.Image

                    'will be added in the future when this ColumnType is added to the DataGrid
                Case DynamicFormControlType.Label, DynamicFormControlType.TextBlock, DynamicFormControlType.TextBox

                    If String.IsNullOrEmpty(objField.StringFormat) Then
                        sb.AppendFormat("<{0}DataGridTextColumn Binding=""{{Binding {1}}}"" Header=""{2}""/> ", strDataGridNamespace, strBindingPath, objField.AssociatedLabel)

                    Else
                        sb.AppendFormat("<{0}DataGridTextColumn Binding=""{{Binding {1}, StringFormat={2}}}"" Header=""{3}""/> ", strDataGridNamespace, strBindingPath, objField.StringFormat.Replace("{", "\{").Replace("}", "\}"), objField.AssociatedLabel)
                    End If

            End Select

            sb.AppendLine()
        Next

        sb.AppendFormat("</{0}DataGrid.Columns>", strDataGridNamespace)
        sb.AppendLine(GetCloseTagForControlFromDefaults(UIControlRole.DataGrid))
        sb.AppendLine()
        sb.Replace(" >", ">")
        sb.Replace("    ", " ")
        sb.Replace("   ", " ")
        sb.Replace("  ", " ")
        _strBusinessForm = sb.ToString
        Me.DialogResult = True
    End Sub

    Private Function DynamicFormContentListBoxFactory(ByVal intGridColumn As Integer) As ListBox

        Dim lb As New ListBox With {.HorizontalAlignment = Windows.HorizontalAlignment.Stretch, .HorizontalContentAlignment = Windows.HorizontalAlignment.Stretch, .VerticalAlignment = Windows.VerticalAlignment.Stretch, .Background = New SolidColorBrush(Colors.WhiteSmoke)}
        lb.SetValue(DragDropHelper.IsDragSourceProperty, True)
        lb.SetValue(DragDropHelper.IsDropTargetProperty, True)
        lb.SetValue(DragDropHelper.DragDropTemplateProperty, Me.FindResource("dynamicFormDragDropDataTemplate"))
        lb.ToolTip = "Drag properties here to create layout."
        lb.SetValue(Grid.ColumnProperty, intGridColumn)
        Return lb
    End Function

    Private Function GetCloseTagForControlFromDefaults(ByVal enumUIControlRole As UIControlRole) As String
        Return String.Format("</{0}>", UIControlFactory.Instance.GetUIControl(enumUIControlRole, CType(IIf(Me.ClassEntity.IsSilverlight, UIPlatform.Silverlight, UIPlatform.WPF), UIPlatform)).ControlType)
    End Function

    Private Sub hlJaime_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

        Dim objHyperlink As Hyperlink = DirectCast(sender, Hyperlink)
        Dim psi As New System.Diagnostics.ProcessStartInfo
        psi.FileName = objHyperlink.NavigateUri.AbsoluteUri
        psi.UseShellExecute = True
        System.Diagnostics.Process.Start(psi)
    End Sub

    Private Sub InitialLayoutOfDynamicForms()

        Dim objCollectionView As CollectionView = CType(CollectionViewSource.GetDefaultView(_objClassEntity.PropertyInfomation), CollectionView)
        objCollectionView.GroupDescriptions.Add(New PropertyGroupDescription("HasBeenUsed"))
        objCollectionView.SortDescriptions.Add(New SortDescription("HasBeenUsed", ListSortDirection.Ascending))
        objCollectionView.SortDescriptions.Add(New SortDescription("Name", ListSortDirection.Ascending))
        Me.gridColumnsContainer.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(425, GridUnitType.Pixel), .MinWidth = 50})
        Me.gridColumnsContainer.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(0, GridUnitType.Auto)})

        Dim lb As ListBox = DynamicFormContentListBoxFactory(0)
        Me.gridColumnsContainer.Children.Add(lb)
        Me.gridColumnsContainer.Children.Add(New GridSplitter With {.HorizontalAlignment = Windows.HorizontalAlignment.Right})
        Me.txtNumberOfColumnGroups.Text = "1"
        Me.txtNumberOfColumnGroupsDataForm.Text = "1"
        Me.NumberOfColumnGroups = 1
        Me.lbFields.ItemsSource = Me.ClassEntity.PropertyInfomation
    End Sub

    Private Sub txtNumberOfColumnGroups_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.KeyEventArgs)

        If e.Key = Key.Enter Then

            Dim intNumberOfColumnGroups As Integer

            If Integer.TryParse(CType(sender, TextBox).Text, intNumberOfColumnGroups) AndAlso intNumberOfColumnGroups >= 1 Then
                ClearColumnsExceptFirstColumn(intNumberOfColumnGroups)

            Else
                MessageBox.Show("The number of column groups must be an integer greater than or equal to one, please reenter.", "Invalid Data", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            End If

        End If

    End Sub

    Private Function WriteSilverlightStringFomatComment(ByVal strStringFormat As String) As String

        If ClassEntity.SilverlightVersion.StartsWith("3") AndAlso Not String.IsNullOrEmpty(strStringFormat) Then
            Return String.Format("<!-- TODO - Add formatting converter for format: {0} -->", strStringFormat)
        End If
        Return String.Empty
    End Function

#End Region

End Class
