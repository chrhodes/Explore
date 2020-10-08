Imports System.ComponentModel
'
Partial Public Class CreateCommandWindow
    Implements INotifyPropertyChanged

#Region " Declarations "

    Private _bolAutoAppendExecute As Boolean = False
    Private _bolIsVB As Boolean = False
    Private _cmdCreateCommand As ICommand
    Private _objCreateCommandSource As CreateCommandSource = Nothing
    Private _strCanExecuteMethodName As String = String.Empty
    Private _strCommandName As String = String.Empty
    Private _strCommandParameterType As String = String.Empty
    Private _strExecuteMethodName As String = String.Empty
    Private _strFieldName As String = String.Empty

#End Region

#Region " Events "

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

#End Region

#Region " Properties "

    Public Property AutoAppendExecute() As Boolean
        Get
            Return _bolAutoAppendExecute
        End Get
        Set(ByVal Value As Boolean)
            _bolAutoAppendExecute = Value
            OnPropertyChanged("AutoAppendExecute")
            UIControlFactory.Instance.UIControls.AutoAppendExecute = _bolAutoAppendExecute
        End Set
    End Property

    Public Property CanExecuteMethodName() As String
        Get
            Return _strCanExecuteMethodName
        End Get
        Set(ByVal Value As String)
            _strCanExecuteMethodName = Value
            OnPropertyChanged("CanExecuteMethodName")
        End Set
    End Property

    Public Property CommandName() As String
        Get
            Return _strCommandName
        End Get
        Set(ByVal Value As String)
            _strCommandName = Value
            OnPropertyChanged("CommandName")
            SetCommandMethodNames()
        End Set
    End Property

    Public Property CommandParameterType() As String
        Get
            Return _strCommandParameterType
        End Get
        Set(ByVal Value As String)
            _strCommandParameterType = Value
            OnPropertyChanged("CommandParameterType")
        End Set
    End Property

    Public Property CreateCommandSource() As CreateCommandSource
        Get
            Return _objCreateCommandSource
        End Get

        Private Set(ByVal Value As CreateCommandSource)
            _objCreateCommandSource = Value
        End Set
    End Property

    Public Property ExecuteMethodName() As String
        Get
            Return _strExecuteMethodName
        End Get
        Set(ByVal Value As String)
            _strExecuteMethodName = Value
            OnPropertyChanged("ExecuteMethodName")
        End Set
    End Property

    Public Property FieldName() As String
        Get
            Return _strFieldName
        End Get
        Set(ByVal Value As String)
            _strFieldName = Value
            OnPropertyChanged("FieldName")
        End Set
    End Property

    Public ReadOnly Property IsVB() As Boolean
        Get
            Return _bolIsVB
        End Get
    End Property

#End Region

#Region " Command Properties "

    Public ReadOnly Property CreateCommand() As ICommand
        Get

            If _cmdCreateCommand Is Nothing Then
                _cmdCreateCommand = New RelayCommand(AddressOf CreateExecute, AddressOf CanCreateExecute)
            End If

            Return _cmdCreateCommand
        End Get
    End Property

#End Region

#Region " Command Methods "

    Private Function CanCreateExecute(ByVal parma As Object) As Boolean

        If String.IsNullOrEmpty(_strCommandName) Then
            Return False
        End If

        If String.IsNullOrEmpty(_strFieldName) Then
            Return False
        End If

        If String.IsNullOrEmpty(_strExecuteMethodName) Then
            Return False
        End If

        If Me.chkIncludeCanExecuteMethod.IsChecked Then

            If String.IsNullOrEmpty(_strCanExecuteMethodName) Then
                Return False
            End If

        End If

        Return True
    End Function

    Private Sub CreateExecute(ByVal param As Object)
        UIControlFactory.Instance.Save(False)
        Me.CreateCommandSource = New CreateCommandSource(Me.rdoCanExecuteUseAddressOf.IsChecked.Value, Me.rdoExecuteUseAddressOf.IsChecked.Value, Me.chkIncludeCanExecuteMethod.IsChecked.Value, Me.rdoRelayCommand.IsChecked.Value, Me.CanExecuteMethodName, Me.CommandName, Me.CommandParameterType, Me.ExecuteMethodName, Me.FieldName)
        Me.DialogResult = True
    End Sub

#End Region

#Region " Constructor "

    Public Sub New(ByVal bolIsVB As Boolean)
        _bolIsVB = bolIsVB

        If _bolIsVB Then
            _strCommandParameterType = "Object"

        Else
            _strCommandParameterType = "object"
        End If

        _bolAutoAppendExecute = UIControlFactory.Instance.UIControls.AutoAppendExecute
        Me.DataContext = Me
        InitializeComponent()
    End Sub

#End Region

#Region " Methods "

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.DialogResult = False
    End Sub

    Private Sub cboCommandName_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.cboCommandName.RemoveHandler(ComboBox.SelectionChangedEvent, New SelectionChangedEventHandler(AddressOf cboCommandName_SelectionChanged))
        Me.cboCommandName.ItemsSource = GetCommandNames()
        Me.cboCommandName.SelectedIndex = -1
        Me.cboCommandName.AddHandler(ComboBox.SelectionChangedEvent, New SelectionChangedEventHandler(AddressOf cboCommandName_SelectionChanged))
    End Sub

    Private Sub cboCommandName_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs)

        If Me.cboCommandName.SelectedItem Is Nothing OrElse Me.cboCommandName.SelectedIndex = -1 Then
            Exit Sub
        End If

        Me.CommandName = Me.cboCommandName.SelectedItem.ToString & "Command"
    End Sub

    Private Sub chkIncludeCanExecuteMethod_Checked(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles chkIncludeCanExecuteMethod.Checked

        If Me.bdrCanExecuteNotUsed IsNot Nothing Then
            Me.bdrCanExecuteNotUsed.Visibility = Windows.Visibility.Collapsed
        End If

    End Sub

    Private Sub chkIncludeCanExecuteMethod_Unchecked(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles chkIncludeCanExecuteMethod.Unchecked

        If Me.bdrCanExecuteNotUsed IsNot Nothing Then
            Me.bdrCanExecuteNotUsed.Visibility = Windows.Visibility.Visible
        End If

    End Sub

    Private Function GetCommandNames() As IList(Of String)

        Dim obj As New List(Of String)
        obj.Add("New")
        obj.Add("Save")
        obj.Add("Update")
        obj.Add("Delete")
        obj.Add("Insert")
        obj.Add("Select")
        obj.Add("Remove")
        obj.Add("Add")
        obj.Add("Lookup")
        obj.Add("Create")
        obj.Add("Modify")
        obj.Add("Extract")
        obj.Add("Next")
        obj.Add("Last")
        obj.Add("Previous")
        obj.Add("First")
        obj.Add("Stop")
        obj.Add("Cancel")
        obj.Sort()
        Return obj
    End Function

    Private Sub SetCommandMethodNames()

        Dim strCommandName As String = Me.CommandName

        If _bolIsVB Then
            Me.FieldName = String.Concat("_cmd", Me.CommandName)

        Else
            Mid(strCommandName, 1, 1) = LCase(Mid(strCommandName, 1, 1))
            Me.FieldName = String.Concat("_", strCommandName)
        End If

        strCommandName = Me.CommandName.Replace("Command", "")

        If _bolAutoAppendExecute Then
            Me.ExecuteMethodName = String.Concat(strCommandName, "Execute")
            Me.CanExecuteMethodName = String.Concat("Can", strCommandName, "Execute")

        Else
            Me.ExecuteMethodName = strCommandName
            Me.CanExecuteMethodName = String.Concat("Can", strCommandName)
        End If

    End Sub

#End Region

#Region " PropertyChanged Methods "

    Private Overloads Sub OnPropertyChanged(ByVal strPropertyName As String)

        If Me.PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
        End If

    End Sub

#End Region

End Class
