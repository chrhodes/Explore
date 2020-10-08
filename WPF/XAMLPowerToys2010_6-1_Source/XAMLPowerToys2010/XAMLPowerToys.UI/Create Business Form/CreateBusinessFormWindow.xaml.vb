Imports System.Text
Imports System.Windows.Controls.Primitives
Imports System.Windows.Threading
Imports XAMLPowerToys.Model
'
Partial Public Class CreateBusinessFormWindow

#Region " Declarations "

    Private Const _CHAR_ROW_COLUMN_KEY_SEPARATOR As Char = ":"c
    Private Const _INT_COLUMNOFFSET As Integer = 1
    Private Const _INT_ROWOFFSET As Integer = 1
    Private _glColumnDefaultSize As GridLength = New GridLength(0, GridUnitType.Auto)
    Private _glRowDefaultSize As GridLength = New GridLength(0, GridUnitType.Auto)
    Private _intNumberOfColumns As Integer
    Private _intNumberOfRows As Integer
    Private Shared _objClassEntity As ClassEntity = Nothing
    Private _objColumnHeaderComboBoxCollection As New List(Of ComboBox)
    Private _objColumnHeaderTextBlockCollection As New List(Of TextBlock)
    Private _objColumnWidthsCollection As New List(Of GridLength)
    Private _objGridCellCollection As New Dictionary(Of String, CellContent)
    Private _objRowHeaderTextBlockCollection As New List(Of TextBlock)
    Private _objRowHeightsCollection As New List(Of GridLength)
    Private _strBusinessForm As String = String.Empty
    Private WithEvents _objColumnSizePopUp As Popup
    Private WithEvents _objColumnSizePopupTimer As DispatcherTimer
    Private WithEvents _objRowSizePopUp As Popup
    Private WithEvents _objRowSizePopupTimer As DispatcherTimer

#End Region

#Region " Properties "

    Public ReadOnly Property BusinessForm() As String
        Get
            Return _strBusinessForm
        End Get
    End Property

    Public Shared ReadOnly Property ClassEntity() As ClassEntity
        Get
            Return _objClassEntity
        End Get
    End Property

    Public Property ColumnDefaultSize() As GridLength
        Get
            Return _glColumnDefaultSize
        End Get
        Set(ByVal Value As GridLength)
            _glColumnDefaultSize = Value
        End Set
    End Property

    Public ReadOnly Property ColumnHeaderComboBoxCollection() As List(Of ComboBox)
        Get
            Return _objColumnHeaderComboBoxCollection
        End Get
    End Property

    Public ReadOnly Property ColumnHeaderTextBlockCollection() As List(Of TextBlock)
        Get
            Return _objColumnHeaderTextBlockCollection
        End Get
    End Property

    Public Property ColumnSizePopUp() As Popup
        Get
            Return _objColumnSizePopUp
        End Get
        Set(ByVal Value As Popup)
            _objColumnSizePopUp = Value
        End Set
    End Property

    Public Property ColumnSizePopupTimer() As DispatcherTimer
        Get
            Return _objColumnSizePopupTimer
        End Get
        Set(ByVal Value As DispatcherTimer)
            _objColumnSizePopupTimer = Value
        End Set
    End Property

    Public ReadOnly Property ColumnWidthsCollection() As List(Of GridLength)
        Get
            Return _objColumnWidthsCollection
        End Get
    End Property

    Public ReadOnly Property GridCellCollection() As Dictionary(Of String, CellContent)
        Get
            Return _objGridCellCollection
        End Get
    End Property

    Public Property NumberOfColumns() As Integer
        Get
            Return _intNumberOfColumns
        End Get
        Set(ByVal Value As Integer)
            _intNumberOfColumns = Value
        End Set
    End Property

    Public Property NumberOfRows() As Integer
        Get
            Return _intNumberOfRows
        End Get
        Set(ByVal Value As Integer)
            _intNumberOfRows = Value
        End Set
    End Property

    Public Property RowDefaultSize() As GridLength
        Get
            Return _glRowDefaultSize
        End Get
        Set(ByVal Value As GridLength)
            _glRowDefaultSize = Value
        End Set
    End Property

    Public ReadOnly Property RowHeaderTextBlockCollection() As List(Of TextBlock)
        Get
            Return _objRowHeaderTextBlockCollection
        End Get
    End Property

    Public ReadOnly Property RowHeightsCollection() As List(Of GridLength)
        Get
            Return _objRowHeightsCollection
        End Get
    End Property

    Public Property RowSizePopUp() As Popup
        Get
            Return _objRowSizePopUp
        End Get
        Set(ByVal Value As Popup)
            _objRowSizePopUp = Value
        End Set
    End Property

    Public Property RowSizePopupTimer() As DispatcherTimer
        Get
            Return _objRowSizePopupTimer
        End Get
        Set(ByVal Value As DispatcherTimer)
            _objRowSizePopupTimer = Value
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

    Private Sub btnAllColumnsAuto_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.ColumnDefaultSize = New GridLength(0, GridUnitType.Auto)

        For intX As Integer = 1 To Me.gridLayout.ColumnDefinitions.Count - 1
            Me.ColumnWidthsCollection.Item(intX) = Me.ColumnDefaultSize
            Me.ColumnHeaderTextBlockCollection(intX).Text = ParseGridLength(Me.ColumnDefaultSize)
        Next

        Me.txtColumnSize.Text = String.Empty
    End Sub

    Private Sub btnAllColumnsStar_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.ColumnDefaultSize = New GridLength(1, GridUnitType.Star)

        For intX As Integer = 1 To Me.gridLayout.ColumnDefinitions.Count - 1
            Me.ColumnWidthsCollection.Item(intX) = Me.ColumnDefaultSize
            Me.ColumnHeaderTextBlockCollection(intX).Text = ParseGridLength(Me.ColumnDefaultSize)
        Next

        Me.txtColumnSize.Text = String.Empty
    End Sub

    Private Sub btnAllRowsAuto_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.RowDefaultSize = New GridLength(0, GridUnitType.Auto)

        For intX As Integer = 1 To Me.gridLayout.RowDefinitions.Count - 1
            Me.RowHeightsCollection.Item(intX) = Me.RowDefaultSize
            Me.RowHeaderTextBlockCollection(intX).Text = ParseGridLength(Me.RowDefaultSize)
        Next

        Me.txtRowSize.Text = String.Empty
    End Sub

    Private Sub btnAllRowsStar_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.RowDefaultSize = New GridLength(1, GridUnitType.Star)

        For intX As Integer = 1 To Me.gridLayout.RowDefinitions.Count - 1
            Me.RowHeightsCollection.Item(intX) = Me.RowDefaultSize
            Me.RowHeaderTextBlockCollection(intX).Text = ParseGridLength(Me.RowDefaultSize)
        Next

        Me.txtRowSize.Text = String.Empty
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.DialogResult = False
    End Sub

    Private Sub btnCreate_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

        Dim sb As New StringBuilder(10240)
        sb.AppendLine("<Grid>")
        sb.Append(vbTab)
        sb.AppendLine("<Grid.RowDefinitions>")

        Dim bolSkipFirst As Boolean = True

        For Each obj As GridLength In Me.RowHeightsCollection

            If bolSkipFirst Then
                bolSkipFirst = False

            Else
                sb.Append(vbTab)
                sb.Append(vbTab)

                If obj.IsStar Then
                    sb.AppendLine("<RowDefinition Height=""*"" />")

                ElseIf obj.IsAuto Then
                    sb.AppendLine("<RowDefinition Height=""Auto"" />")

                Else
                    sb.AppendFormat("<RowDefinition Height=""{0}"" />{1}", obj.Value.ToString, vbCrLf)
                End If

            End If

        Next

        sb.Append(vbTab)
        sb.AppendLine("</Grid.RowDefinitions>")
        sb.Append(vbTab)
        sb.AppendLine("<Grid.ColumnDefinitions>")
        bolSkipFirst = True

        For Each obj As GridLength In Me.ColumnWidthsCollection

            If bolSkipFirst Then
                bolSkipFirst = False

            Else
                sb.Append(vbTab)
                sb.Append(vbTab)

                If obj.IsStar Then
                    sb.AppendLine("<ColumnDefinition Width=""*"" />")

                ElseIf obj.IsAuto Then
                    sb.AppendLine("<ColumnDefinition Width=""Auto"" />")

                Else
                    sb.AppendFormat("<ColumnDefinition Width=""{0}"" />{1}", obj.Value.ToString, vbCrLf)
                End If

            End If

        Next

        sb.Append(vbTab)
        sb.AppendLine("</Grid.ColumnDefinitions>")
        sb.AppendLine()

        For intColumn As Integer = 1 To Me.gridLayout.ColumnDefinitions.Count - 1

            For intRow As Integer = 1 To Me.gridLayout.RowDefinitions.Count - 1

                If Me.GridCellCollection.ContainsKey(MakeKey(intRow, intColumn)) AndAlso Me.GridCellCollection(MakeKey(intRow, intColumn)).ControlType <> ControlType.None Then
                    sb.Append(vbTab)

                    Dim obj As CellContent = Me.GridCellCollection(MakeKey(intRow, intColumn))

                    If ClassEntity IsNot Nothing AndAlso ClassEntity.IsSilverlight Then

                        If Not String.IsNullOrEmpty(obj.StringFormat) Then
                            sb.AppendLine(String.Format("<!-- TODO - Add formatting converter for format: {0} -->", obj.StringFormat))
                        End If

                    End If

                    sb.AppendLine(ControlFactory(obj))
                End If

            Next

            sb.AppendLine()
        Next

        sb.Replace(" >", ">")
        sb.Replace("    ", " ")
        sb.Replace("   ", " ")
        sb.Replace("  ", " ")
        sb.AppendLine("</Grid>")
        _strBusinessForm = sb.ToString

        If _strBusinessForm.IndexOf("CHANGEME") > -1 Then
            _strBusinessForm = String.Concat(vbCrLf, "<!-- Search for and change all instances of CHANGEME -->", vbCrLf, vbCrLf, _strBusinessForm)
        End If

        Me.DialogResult = True
    End Sub

    Private Sub cboColumnHeader_SelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs)

        Dim cbo As ComboBox = DirectCast(sender, ComboBox)
        Dim intColumn As Integer = CType(cbo.Tag, Integer)

        If cbo.SelectedValue.ToString = "Select" Then
            Exit Sub
        End If

        Dim enumControlType As ControlType = CType([Enum].Parse(GetType(ControlType), cbo.SelectedValue.ToString), ControlType)

        For intRow As Integer = 1 To Me.NumberOfRows - 1
            Me.GridCellCollection(MakeKey(intRow, intColumn)).ControlType = enumControlType
        Next

        LayoutGrid()
    End Sub

    Private Function ControlFactory(ByVal obj As CellContent) As String

        Dim enumUIPlatform As UIPlatform = UIPlatform.WPF

        If ClassEntity IsNot Nothing AndAlso ClassEntity.IsSilverlight Then

            enumUIPlatform = UIPlatform.Silverlight
        End If

        Dim intColumn As Integer = obj.Column - _INT_COLUMNOFFSET
        Dim intRow As Integer = obj.Row - _INT_ROWOFFSET

        If String.IsNullOrEmpty(obj.BindingPath) Then
            obj.BindingPath = "CHANGEME"
        End If

        Select Case obj.ControlType

            Case ControlType.DatePicker
                Return UIControlFactory.Instance.MakeDatePicker(enumUIPlatform, intColumn, intRow, obj.BindingPath, obj.Width)

            Case ControlType.CheckBox
                Return UIControlFactory.Instance.MakeCheckBox(enumUIPlatform, intColumn, intRow, obj.ControlLabel, obj.BindingPath, obj.BindingMode)

            Case ControlType.ComboBox
                Return UIControlFactory.Instance.MakeComboBox(enumUIPlatform, intColumn, intRow, obj.BindingPath, obj.BindingMode)

            Case ControlType.Image
                Return UIControlFactory.Instance.MakeImage(enumUIPlatform, intColumn, intRow, obj.BindingPath)

            Case ControlType.Label
                Return UIControlFactory.Instance.MakeLabelWithoutBinding(enumUIPlatform, intColumn, intRow, obj.ControlLabel)

            Case ControlType.TextBlock
                Return UIControlFactory.Instance.MakeTextBlock(enumUIPlatform, intColumn, intRow, obj.BindingPath, obj.StringFormat, ClassEntity.SilverlightVersion)

            Case ControlType.TextBox
                Return UIControlFactory.Instance.MakeTextBox(enumUIPlatform, intColumn, intRow, obj.BindingPath, obj.BindingMode, obj.Width, obj.MaximumLength, obj.StringFormat, obj.DataType.StartsWith("Nullable"), ClassEntity.SilverlightVersion)
        End Select

        Return ""
    End Function

    Private Sub CreateBusinessForm_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Me.NumberOfColumns = 2
        Me.NumberOfRows = 4
        Me.txtNumberOfColumns.Text = Me.NumberOfColumns.ToString
        Me.txtNumberOfRows.Text = Me.NumberOfRows.ToString
        Me.NumberOfColumns += _INT_COLUMNOFFSET
        Me.NumberOfRows += _INT_ROWOFFSET

        For intX As Integer = 1 To Me.NumberOfColumns
            Me.ColumnWidthsCollection.Add(New GridLength(0, GridUnitType.Auto))
        Next

        For intX As Integer = 1 To Me.NumberOfRows
            Me.RowHeightsCollection.Add(New GridLength(0, GridUnitType.Auto))
        Next

        If ClassEntity IsNot Nothing AndAlso ClassEntity.PropertyInfomation IsNot Nothing AndAlso ClassEntity.PropertyInfomation.Count > 0 Then
            Me.tbBusinessClassTitle.Visibility = Windows.Visibility.Visible
            Me.tbBusinessClassTitle.Text = String.Format("Create Form For Class: {0}, Number of Properties: {1}", ClassEntity.ClassName, ClassEntity.PropertyInfomation.Count - 1)
        End If

        LayoutGrid()
    End Sub

    Private Function GetColumnFromKey(ByVal strKey As String) As Integer

        Dim aryRowColumn() As String = strKey.Split(_CHAR_ROW_COLUMN_KEY_SEPARATOR)
        Return Integer.Parse(aryRowColumn(1))
    End Function

    Private Function GetRowFromKey(ByVal strKey As String) As Integer

        Dim aryRowColumn() As String = strKey.Split(_CHAR_ROW_COLUMN_KEY_SEPARATOR)
        Return Integer.Parse(aryRowColumn(0))
    End Function

    Private Sub LayoutGrid()

        'remove all previous handlers
        For Each cbo As ComboBox In Me.ColumnHeaderComboBoxCollection
            cbo.RemoveHandler(ComboBox.SelectionChangedEvent, New SelectionChangedEventHandler(AddressOf cboColumnHeader_SelectionChanged))
        Next

        Me.ColumnHeaderTextBlockCollection.Clear()
        Me.ColumnHeaderTextBlockCollection.Add(New TextBlock)
        Me.RowHeaderTextBlockCollection.Clear()
        Me.RowHeaderTextBlockCollection.Add(New TextBlock)
        Me.ColumnHeaderComboBoxCollection.Clear()
        Me.gridLayout.Children.Clear()
        Me.gridLayout.ColumnDefinitions.Clear()
        Me.gridLayout.RowDefinitions.Clear()

        For intX = 1 To Me.NumberOfColumns
            Me.gridLayout.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(0, GridUnitType.Auto), .MinWidth = 75})
        Next

        For intX = 1 To Me.NumberOfRows
            Me.gridLayout.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(0, GridUnitType.Auto)})
        Next

        'this adds alternating color to each row
        For intX = 0 To Me.gridLayout.RowDefinitions.Count - 1

            If intX Mod 2 <> 0 Then

                Dim objRectangle As New Rectangle With {.Fill = New SolidColorBrush(Colors.WhiteSmoke), .VerticalAlignment = Windows.VerticalAlignment.Stretch, .HorizontalAlignment = Windows.HorizontalAlignment.Stretch}
                objRectangle.SetValue(Grid.RowProperty, intX)
                objRectangle.SetValue(Grid.ColumnSpanProperty, Me.NumberOfColumns)
                Me.gridLayout.Children.Add(objRectangle)
            End If

        Next

        'start in column 1 not 0
        For intX = 1 To Me.gridLayout.ColumnDefinitions.Count - 1

            Dim cbo As New ComboBox
            Dim ary As Array = [Enum].GetNames(GetType(ControlType))
            Array.Sort(ary)

            For Each s As String In ary

                If s = "None" Then
                    cbo.Items.Add("Select")

                Else
                    cbo.Items.Add(s)
                End If

            Next

            cbo.FontSize = 10
            cbo.SelectedValue = "Select"
            cbo.VerticalAlignment = Windows.VerticalAlignment.Top
            cbo.Margin = New Thickness(10)
            cbo.AddHandler(ComboBox.SelectionChangedEvent, New SelectionChangedEventHandler(AddressOf cboColumnHeader_SelectionChanged))
            cbo.IsTabStop = False
            cbo.Tag = intX
            Me.ColumnHeaderComboBoxCollection.Add(cbo)

            Dim sp As New StackPanel
            sp.Children.Add(cbo)

            Dim tb As New TextBlock With {.Tag = intX, .Margin = New Thickness(5), .HorizontalAlignment = Windows.HorizontalAlignment.Center, .Text = ParseGridLength(Me.ColumnWidthsCollection(intX))}
            tb.AddHandler(TextBlock.MouseRightButtonDownEvent, New MouseButtonEventHandler(AddressOf ColumnTextBlock_MouseRightButtonDown))
            tb.ToolTip = "Right click to edit this column's size"
            Me.ColumnHeaderTextBlockCollection.Add(tb)
            sp.Children.Add(tb)
            sp.SetValue(Grid.ColumnProperty, intX)
            Me.gridLayout.Children.Add(sp)
        Next

        For intRow As Integer = 1 To Me.NumberOfRows - 1

            For intColumn As Integer = 0 To Me.NumberOfColumns - 1

                If intColumn = 0 Then

                    Dim tb As New TextBlock With {.Tag = intRow, .Margin = New Thickness(5), .HorizontalAlignment = Windows.HorizontalAlignment.Center, .VerticalAlignment = Windows.VerticalAlignment.Center, .Text = ParseGridLength(Me.RowHeightsCollection.Item(intRow))}
                    tb.AddHandler(TextBlock.MouseRightButtonDownEvent, New MouseButtonEventHandler(AddressOf RowTextBlock_MouseRightButtonDown))
                    tb.ToolTip = "Right click to edit this row's size"
                    tb.SetValue(Grid.RowProperty, intRow)
                    tb.SetValue(Grid.ColumnProperty, intColumn)
                    Me.RowHeaderTextBlockCollection.Add(tb)
                    Me.gridLayout.Children.Add(tb)

                Else

                    If Not Me.GridCellCollection.ContainsKey(MakeKey(intRow, intColumn)) Then
                        Me.GridCellCollection.Add(MakeKey(intRow, intColumn), New CellContent(intRow, intColumn))
                    End If

                    Dim objGridCellEditor As New GridCellEditor
                    objGridCellEditor.SetValue(Grid.RowProperty, intRow)
                    objGridCellEditor.SetValue(Grid.ColumnProperty, intColumn)
                    objGridCellEditor.DataContext = Me.GridCellCollection(MakeKey(intRow, intColumn))
                    Me.gridLayout.Children.Add(objGridCellEditor)
                End If

            Next

        Next

    End Sub

    Private Function MakeKey(ByVal intRow As Integer, ByVal intColumn As Integer) As String
        Return String.Format("{0}{1}{2}", intRow, _CHAR_ROW_COLUMN_KEY_SEPARATOR.ToString, intColumn)
    End Function

    Private Function ParseGridLength(ByVal obj As GridLength) As String

        If obj.IsAuto Then
            Return "Auto"

        ElseIf obj.IsStar Then
            Return "Star"

        Else
            Return obj.Value.ToString
        End If

    End Function

    Private Sub txtAllColumnsWidth_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Input.KeyEventArgs)

        If e.Key = Key.Enter Then

            Dim intColumnWidth As Integer

            If Integer.TryParse(CType(sender, TextBox).Text, intColumnWidth) AndAlso intColumnWidth >= 0 Then
                Me.ColumnDefaultSize = New GridLength(intColumnWidth, GridUnitType.Pixel)

                For intX As Integer = 1 To Me.gridLayout.ColumnDefinitions.Count - 1
                    Me.ColumnWidthsCollection.Item(intX) = Me.ColumnDefaultSize
                    Me.ColumnHeaderTextBlockCollection(intX).Text = ParseGridLength(Me.ColumnDefaultSize)
                Next

            Else
                MessageBox.Show("The column width must be an integer greater than or equal to zero, please reenter.", "Invalid Data", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            End If

        End If

    End Sub

    Private Sub txtAllRowsHeight_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Input.KeyEventArgs)

        If e.Key = Key.Enter Then

            Dim intRowHeight As Integer

            If Integer.TryParse(CType(sender, TextBox).Text, intRowHeight) AndAlso intRowHeight >= 0 Then
                Me.RowDefaultSize = New GridLength(intRowHeight, GridUnitType.Pixel)

                For intX As Integer = 1 To Me.gridLayout.RowDefinitions.Count - 1
                    Me.RowHeightsCollection.Item(intX) = Me.RowDefaultSize
                    Me.RowHeaderTextBlockCollection(intX).Text = ParseGridLength(Me.RowDefaultSize)
                Next

            Else
                MessageBox.Show("The row height must be an integer greater than or equal to zero, please reenter.", "Invalid Data", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            End If

        End If

    End Sub

    Private Sub txtNumberOfColumns_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Input.KeyEventArgs)

        If e.Key = Key.Enter Then

            Dim intColumns As Integer

            If Integer.TryParse(CType(sender, TextBox).Text, intColumns) Then

                If intColumns < 51 AndAlso intColumns > 0 Then
                    intColumns += 1

                    'user removed one or more columns
                    If intColumns < Me.NumberOfColumns Then

                        Dim objRemoveList As New List(Of String)

                        For Each s As String In Me.GridCellCollection.Keys

                            If GetColumnFromKey(s) > intColumns Then
                                objRemoveList.Add(s)
                            End If

                        Next

                        For Each s As String In objRemoveList
                            Me.GridCellCollection.Remove(s)
                        Next

                        Me.ColumnWidthsCollection.RemoveRange(intColumns, Me.NumberOfColumns - intColumns)
                    End If

                    Me.NumberOfColumns = intColumns

                    If Me.ColumnWidthsCollection.Count < Me.NumberOfColumns Then

                        For intX As Integer = 1 To Me.NumberOfColumns - Me.ColumnWidthsCollection.Count
                            Me.ColumnWidthsCollection.Add(Me.ColumnDefaultSize)
                        Next

                    End If

                    LayoutGrid()
                    e.Handled = True

                Else
                    MessageBox.Show("The number of columns must be equal to or less than 50, please reenter.", "Invalid Data", MessageBoxButton.OK, MessageBoxImage.Exclamation)
                End If

            Else
                MessageBox.Show("The number of columns must be an integer, please reenter.", "Invalid Data", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            End If

        End If

    End Sub

    Private Sub txtNumberOfRows_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Input.KeyEventArgs)

        If e.Key = Key.Enter Then

            Dim intRows As Integer

            If Integer.TryParse(CType(sender, TextBox).Text, intRows) Then

                If intRows < 51 AndAlso intRows > 0 Then
                    intRows += 1

                    'user removed one or more rows
                    If intRows < Me.NumberOfRows Then

                        Dim objRemoveList As New List(Of String)

                        For Each s As String In Me.GridCellCollection.Keys

                            If GetRowFromKey(s) > intRows Then
                                objRemoveList.Add(s)
                            End If

                        Next

                        For Each s As String In objRemoveList
                            Me.GridCellCollection.Remove(s)
                        Next

                        Me.RowHeightsCollection.RemoveRange(intRows, Me.NumberOfRows - intRows)
                    End If

                    Me.NumberOfRows = intRows

                    If Me.RowHeightsCollection.Count < Me.NumberOfRows Then

                        For intX As Integer = 1 To Me.NumberOfRows - Me.RowHeightsCollection.Count
                            Me.RowHeightsCollection.Add(Me.RowDefaultSize)
                        Next

                    End If

                    LayoutGrid()
                    e.Handled = True

                Else
                    MessageBox.Show("The number of rows must be equal to or less than 50, please reenter.", "Invalid Data", MessageBoxButton.OK, MessageBoxImage.Exclamation)
                End If

            Else
                MessageBox.Show("The number of rows must be an integer, please reenter.", "Invalid Data", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            End If

        End If

    End Sub

#End Region

#Region " Row/Column Popup Methods "

    Private Sub _objColumnSizePopUp_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _objColumnSizePopUp.Closed

        If Me.ColumnSizePopupTimer IsNot Nothing Then
            Me.ColumnSizePopupTimer.Stop()
            Me.ColumnSizePopupTimer = Nothing
        End If

    End Sub

    Private Sub _objColumnSizePopUp_MouseEnter(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles _objColumnSizePopUp.MouseEnter
        Me.ColumnSizePopupTimer.Stop()
        Me.ColumnSizePopUp.StaysOpen = False
    End Sub

    Private Sub _objColumnSizePopupTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _objColumnSizePopupTimer.Tick
        Me.ColumnSizePopUp.IsOpen = False
    End Sub

    Private Sub _objRowSizePopUp_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _objRowSizePopUp.Closed

        If Me.RowSizePopupTimer IsNot Nothing Then
            Me.RowSizePopupTimer.Stop()
            Me.RowSizePopupTimer = Nothing
        End If

    End Sub

    Private Sub _objRowSizePopUp_MouseEnter(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles _objRowSizePopUp.MouseEnter
        Me.RowSizePopupTimer.Stop()
        Me.RowSizePopUp.StaysOpen = False
    End Sub

    Private Sub _objRowSizePopupTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _objRowSizePopupTimer.Tick
        Me.RowSizePopUp.IsOpen = False
    End Sub

    Private Sub btnPopupColumnAutoSize_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

        Dim intColumn As Integer = Integer.Parse(Me.ColumnSizePopUp.Tag.ToString)
        Me.ColumnWidthsCollection(intColumn) = New GridLength(0, GridUnitType.Auto)
        Me.ColumnHeaderTextBlockCollection(intColumn).Text = ParseGridLength(Me.ColumnWidthsCollection(intColumn))
        Me.ColumnSizePopUp.IsOpen = False
    End Sub

    Private Sub btnPopupColumnStarSize_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

        Dim intColumn As Integer = Integer.Parse(Me.ColumnSizePopUp.Tag.ToString)
        Me.ColumnWidthsCollection(intColumn) = New GridLength(1, GridUnitType.Star)
        Me.ColumnHeaderTextBlockCollection(intColumn).Text = ParseGridLength(Me.ColumnWidthsCollection(intColumn))
        Me.ColumnSizePopUp.IsOpen = False
    End Sub

    Private Sub btnPopupRowAutoSize_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

        Dim intRow As Integer = Integer.Parse(Me.RowSizePopUp.Tag.ToString)
        Me.RowHeightsCollection.Item(intRow) = New GridLength(0, GridUnitType.Auto)
        Me.RowHeaderTextBlockCollection(intRow).Text = ParseGridLength(Me.RowHeightsCollection.Item(intRow))
        Me.RowSizePopUp.IsOpen = False
    End Sub

    Private Sub btnPopupRowStarSize_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

        Dim intRow As Integer = Integer.Parse(Me.RowSizePopUp.Tag.ToString)
        Me.RowHeightsCollection.Item(intRow) = New GridLength(1, GridUnitType.Star)
        Me.RowHeaderTextBlockCollection(intRow).Text = ParseGridLength(Me.RowHeightsCollection.Item(intRow))
        Me.RowSizePopUp.IsOpen = False
    End Sub

    Private Sub ColumnTextBlock_MouseRightButtonDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)

        Dim tb As TextBlock = CType(sender, TextBlock)
        Me.ColumnSizePopUp = CType(Me.FindResource("columnPopUp"), Popup)
        With Me.ColumnSizePopUp
            .Tag = tb.Tag
            .StaysOpen = True
            .PlacementTarget = tb
            .VerticalOffset = -5
            .IsOpen = True
        End With
        Me.ColumnSizePopupTimer = New DispatcherTimer
        With Me.ColumnSizePopupTimer
            .Interval = New TimeSpan(0, 0, 1)
            .Start()
        End With
    End Sub

    Private Sub RowTextBlock_MouseRightButtonDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)

        Dim tb As TextBlock = CType(sender, TextBlock)
        Me.RowSizePopUp = CType(Me.FindResource("rowPopUp"), Popup)
        With Me.RowSizePopUp
            .Tag = tb.Tag
            .StaysOpen = True
            .PlacementTarget = tb
            .VerticalOffset = -5
            .IsOpen = True
        End With
        Me.RowSizePopupTimer = New DispatcherTimer
        With Me.RowSizePopupTimer
            .Interval = New TimeSpan(0, 0, 1)
            .Start()
        End With
    End Sub

    Private Sub txtPopupColumnWidth_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Input.KeyEventArgs)

        If e.Key = Key.Enter Then

            Dim txt As TextBox = CType(sender, TextBox)
            Dim intColumn As Integer = Integer.Parse(Me.ColumnSizePopUp.Tag.ToString)
            Dim intWidth As Integer

            If Integer.TryParse(txt.Text, intWidth) AndAlso intWidth >= 0 Then
                Me.ColumnHeaderTextBlockCollection(intColumn).Text = intWidth.ToString
                Me.ColumnWidthsCollection.Item(intColumn) = New GridLength(intWidth)
                Me.ColumnSizePopUp.IsOpen = False
                txt.Text = String.Empty

            Else
                MessageBox.Show("The column width must be an integer greater than or equal to zero, please reenter.", "Invalid Data", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            End If

        End If

    End Sub

    Private Sub txtPopupRowHeight_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Input.KeyEventArgs)

        If e.Key = Key.Enter Then

            Dim txt As TextBox = CType(sender, TextBox)
            Dim intRow As Integer = Integer.Parse(Me.RowSizePopUp.Tag.ToString)
            Dim intHeight As Integer

            If Integer.TryParse(txt.Text, intHeight) AndAlso intHeight >= 0 Then
                Me.RowHeaderTextBlockCollection(intRow).Text = intHeight.ToString
                Me.RowHeightsCollection.Item(intRow) = New GridLength(intHeight)
                Me.RowSizePopUp.IsOpen = False
                txt.Text = String.Empty

            Else
                MessageBox.Show("The row height must be an integer greater than or equal to zero, please reenter.", "Invalid Data", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            End If

        End If

    End Sub

#End Region

End Class
