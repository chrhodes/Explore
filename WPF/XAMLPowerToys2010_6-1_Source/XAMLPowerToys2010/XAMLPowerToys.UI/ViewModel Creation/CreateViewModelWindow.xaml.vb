Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Text
Imports XAMLPowerToys.Model
'
Partial Public Class CreateViewModelWindow
    Implements INotifyPropertyChanged

#Region " Declarations "

    Private _bolHasPrivateSetter As Boolean = False
    Private _bolIncludeOnPropertyChanged As Boolean = True
    Private _bolIsPropertyReadOnly As Boolean = False
    Private _bolIsVB As Boolean
    Private _cmdCreateCommand As ICommand
    Private _objClassEntity As ClassEntity
    Private _objCommandsCollection As New ObservableCollection(Of CreateCommandSource)
    Private _strFieldName As String = String.Empty
    Private _strPropertyName As String = String.Empty
    Private _strPropertySignature As String = "Public Property"
    Private _strPropertyType As String = String.Empty
    Private _strViewModelText As String = String.Empty
    Private _bolIncludeOnPropertyChangedEventHandler As Boolean = False
    Private _bolUseHungarianNotationForPrivateFields As Boolean = False
    Private _bolIsPropertyPublic As Boolean = True
    Private _strOnPropertyChangedMethodName As String = "RaisePropertyChanged"

#End Region

#Region " Events "

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

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

#Region " Properties "

    Public Property OnPropertyChangedMethodName() As String
        Get
            Return _strOnPropertyChangedMethodName
        End Get
        Set(ByVal value As String)
            _strOnPropertyChangedMethodName = value
            OnPropertyChanged("OnPropertyChangedMethodName")
        End Set
    End Property

    Public Property IsPropertyPublic() As Boolean
        Get
            Return _bolIsPropertyPublic
        End Get
        Set(ByVal value As Boolean)
            _bolIsPropertyPublic = value
            OnPropertyChanged("IsPropertyPublic")
            SetPropertySignature()
        End Set
    End Property

    Public Property UseHungarianNotationForPrivateFields() As Boolean
        Get
            Return _bolUseHungarianNotationForPrivateFields
        End Get
        Set(ByVal value As Boolean)
            _bolUseHungarianNotationForPrivateFields = value
            OnPropertyChanged("UseHungarianNotationForPrivateFields")
        End Set
    End Property

    Public ReadOnly Property ClassEntity() As ClassEntity
        Get
            Return _objClassEntity
        End Get
    End Property

    Public ReadOnly Property CommandsCollection() As ObservableCollection(Of CreateCommandSource)
        Get
            Return _objCommandsCollection
        End Get
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

    Public Property HasPrivateSetter() As Boolean
        Get
            Return _bolHasPrivateSetter
        End Get
        Set(ByVal Value As Boolean)
            _bolHasPrivateSetter = Value
            OnPropertyChanged("HasPrivateSetter")
        End Set
    End Property

    Public Property IncludeOnPropertyChangedEventHandler() As Boolean
        Get
            Return _bolIncludeOnPropertyChangedEventHandler
        End Get
        Set(ByVal Value As Boolean)
            _bolIncludeOnPropertyChangedEventHandler = Value
            OnPropertyChanged("IncludeOnPropertyChangedEventHandler")
        End Set
    End Property

    Public Property IncludeOnPropertyChanged() As Boolean
        Get
            Return _bolIncludeOnPropertyChanged
        End Get
        Set(ByVal Value As Boolean)
            _bolIncludeOnPropertyChanged = Value
            OnPropertyChanged("IncludeOnPropertyChanged")
        End Set
    End Property

    Public Property IsPropertyReadOnly() As Boolean
        Get
            Return _bolIsPropertyReadOnly
        End Get
        Set(ByVal Value As Boolean)
            _bolIsPropertyReadOnly = Value
            OnPropertyChanged("IsPropertyReadOnly")
            SetPropertySignature()
        End Set
    End Property

    Public ReadOnly Property IsVB() As Boolean
        Get
            Return _bolIsVB
        End Get
    End Property

    Public Property PropertyName() As String
        Get
            Return _strPropertyName
        End Get
        Set(ByVal Value As String)
            _strPropertyName = Value
            OnPropertyChanged("PropertyName")
        End Set
    End Property

    Public Property PropertySignature() As String
        Get
            Return _strPropertySignature
        End Get

        Private Set(ByVal Value As String)
            _strPropertySignature = Value
            OnPropertyChanged("PropertySignature")
        End Set
    End Property

    Public Property PropertyType() As String
        Get
            Return _strPropertyType
        End Get
        Set(ByVal Value As String)
            _strPropertyType = Value
            OnPropertyChanged("PropertyType")
        End Set
    End Property

    Public ReadOnly Property TypeName() As String
        Get
            Return Me.ClassEntity.ClassName
        End Get
    End Property

    Public ReadOnly Property ViewModelText() As String
        Get
            Return _strViewModelText
        End Get
    End Property

    Private ReadOnly Property ExposePropertiesOnViewModel() As Boolean
        Get
            Return Me.lbProperteis.SelectedItems.Count > 0
        End Get
    End Property

    Private ReadOnly Property SelectedPropertyInformationCollection() As IEnumerable(Of PropertyInformation)
        Get
            Return From p In Me.ClassEntity.PropertyInfomation Where p.IsSelected Order By p.Name
        End Get
    End Property

#End Region

#Region " Constructor "

    Public Sub New(ByVal objClassEntity As ClassEntity, ByVal bolIsVB As Boolean)
        _objClassEntity = objClassEntity
        _bolIsVB = bolIsVB
        _strPropertyType = _objClassEntity.ClassName
        _strPropertyName = _objClassEntity.ClassName

        If _bolIsVB Then
            _strFieldName = "_obj" & _objClassEntity.ClassName

        Else

            Dim strClassName As String = _objClassEntity.ClassName
            Mid(strClassName, 1, 1) = LCase(Mid(strClassName, 1, 1))
            _strFieldName = "_" & strClassName
        End If

        Me.DataContext = Me
        InitializeComponent()
    End Sub

#End Region

#Region " PropertyChanged Methods "

    Private Overloads Sub OnPropertyChanged(ByVal strPropertyName As String)

        If Me.PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
        End If

    End Sub

#End Region

#Region " Command Methods "

    Private Function CanCreateExecute(ByVal parma As Object) As Boolean

        If String.IsNullOrEmpty(_strPropertyName) Then
            Return False
        End If

        If String.IsNullOrEmpty(_strPropertyType) Then
            Return False
        End If

        If String.IsNullOrEmpty(_strFieldName) Then
            Return False
        End If

        Return True
    End Function

    Private Sub CreateExecute(ByVal param As Object)

        If CanCreateExecute(param) Then

            If IsVB Then
                CreateVBViewModelText()

            Else
                CreateCSharpViewModelText()
            End If

            Me.DialogResult = True
        End If

    End Sub

#End Region

#Region " Methods "

    Private Sub btnAddCommand_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

        Dim frm As New CreateCommandWindow(Me.IsVB)

        If frm.ShowDialog = True Then
            Me.CommandsCollection.Add(frm.CreateCommandSource)
        End If

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.DialogResult = False
    End Sub

    Private Sub cboPropertyType_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.cboPropertyType.RemoveHandler(ComboBox.SelectionChangedEvent, New SelectionChangedEventHandler(AddressOf cboPropertyType_SelectionChanged))
        Me.cboPropertyType.ItemsSource = GetPropertyTypes()
        Me.cboPropertyType.SelectedIndex = -1
        Me.cboPropertyType.AddHandler(ComboBox.SelectionChangedEvent, New SelectionChangedEventHandler(AddressOf cboPropertyType_SelectionChanged))
    End Sub

    Private Sub cboPropertyType_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs)

        If Me.cboPropertyType.SelectedItem Is Nothing OrElse Me.cboPropertyType.SelectedIndex = -1 Then
            Exit Sub
        End If

        Me.PropertyType = Me.cboPropertyType.SelectedItem.ToString
    End Sub

    Private Sub CreateViewModelWindow_Unloaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Unloaded
        Me.cboPropertyType.RemoveHandler(ComboBox.SelectionChangedEvent, New SelectionChangedEventHandler(AddressOf cboPropertyType_SelectionChanged))
        Me.cboPropertyChangedMethodNames.RemoveHandler(ComboBox.SelectionChangedEvent, New SelectionChangedEventHandler(AddressOf cboPropertyChangedMethodNames__SelectionChanged))
    End Sub

    Private Function GetPropertyTypes() As IList(Of String)

        Dim obj As New List(Of String)
        obj.Add(Me.TypeName)
        obj.Add(String.Format("List(Of {0})", Me.TypeName))
        obj.Add(String.Format("ObservableCollection(Of {0})", Me.TypeName))
        obj.Add(String.Format("ReadOnlyObservableCollection(Of {0})", Me.TypeName))
        obj.Add(String.Format("IEnumerable(Of {0})", Me.TypeName))
        obj.Add(String.Format("IList(Of {0})", Me.TypeName))
        Return obj
    End Function

    Private Sub SetPropertySignature()
        Me.PropertySignature = String.Format("{0}{1}Property", IIf(Me.IsPropertyPublic, "Public ", "Private ").ToString, IIf(Me.IsPropertyReadOnly, "ReadOnly ", " ").ToString)
    End Sub

#End Region

#Region " Create ViewModel Text Methods "

    Private Sub CreateCSharpViewModelText()

        If _strPropertyType.StartsWith("Nullable") Then
            _strPropertyType = _strPropertyType.Replace("Nullable(Of ", "").Replace(")", "").Trim & "?"

        Else
            Dim intOf As Integer = _strPropertyType.IndexOf("(Of")

            If intOf > -1 Then
                _strPropertyType = _strPropertyType.Replace("(Of ", "<").Replace(")", ">")
            End If

        End If

        Dim sb As New StringBuilder(4096)

        If IncludeOnPropertyChangedEventHandler Then
            sb.AppendLine("// : System.ComponentModel.INotifyPropertyChanged")
            sb.AppendLine("// developer, please place the above at the end of your class name")
            sb.AppendLine("")
        End If

        '
        '
        '
        If ExposePropertiesOnViewModel Then
            sb.AppendLine("")
            sb.AppendLine("//TODO developers please add your constructors in the below constructor region.")
            sb.AppendLine("//     be sure to include an overloaded constructor that takes a model type.")
            sb.AppendLine("")
        End If

        '
        '
        'declarations
        sb.AppendLine("#region Declarations")
        sb.AppendLine("")

        For Each obj As CreateCommandSource In (From x In Me.CommandsCollection Order By x.CommandName).ToList
            sb.AppendFormat("ICommand {0};{1}", obj.FieldName, vbCrLf)
        Next

        sb.AppendFormat("{0} {1};{2}", _strPropertyType, _strFieldName, vbCrLf)
        sb.AppendLine("")
        sb.AppendLine("#endregion //Declarations")
        sb.AppendLine("")

        '
        '
        'events
        If IncludeOnPropertyChangedEventHandler Then
            sb.AppendLine("#region Events ")
            sb.AppendLine("")
            sb.AppendLine("public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;")
            sb.AppendLine("")
            sb.AppendLine("#endregion //Events")
            sb.AppendLine("")
        End If

        '
        '
        'properties
        sb.AppendLine("#region Properties")
        sb.AppendLine("")

        If IsPropertyPublic Then
            sb.Append("public")

        Else
            sb.Append("private")
        End If

        sb.AppendFormat(" {0} {1}{2}", _strPropertyType, _strPropertyName, vbCrLf)
        sb.AppendLine("{")
        sb.AppendFormat("get {{ return {0}; }}{1}", _strFieldName, vbCrLf)

        If Not _bolIsPropertyReadOnly Then

            If _bolHasPrivateSetter Then
                sb.Append("private ")
            End If

            sb.AppendLine("set")
            sb.AppendLine("{")
            sb.AppendFormat("{0} = value;{1}", _strFieldName, vbCrLf)

            If _bolIncludeOnPropertyChanged Then
                sb.AppendFormat("{0}(""{1}"");{2}", Me.OnPropertyChangedMethodName, _strPropertyName, vbCrLf)
            End If

            sb.AppendLine("}")
        End If

        sb.AppendLine("}")
        sb.AppendLine("")

        If ExposePropertiesOnViewModel Then

            For Each obj As PropertyInformation In SelectedPropertyInformationCollection

                Dim strTypeName As String = obj.TypeName
                If strTypeName.StartsWith("Nullable") Then
                    strTypeName = strTypeName.Replace("Nullable(Of ", "").Replace(")", "").Trim & "?"

                Else
                    Dim intOf As Integer = strTypeName.IndexOf("(Of")

                    If intOf > -1 Then
                        strTypeName = strTypeName.Replace("(Of ", "<").Replace(")", ">")
                    End If

                End If

                If obj.Name = "Item" AndAlso obj.PropertyParameters.Count = 1 Then
                    sb.AppendFormat("public {0} this[{1}]{2}", strTypeName, obj.CSPropertyParameterString, vbCrLf)
                ElseIf obj.PropertyParameters.Count > 0 Then
                    sb.AppendFormat("public {0} {1}[{2}]{3}", strTypeName, obj.Name, obj.CSPropertyParameterString, vbCrLf)
                Else
                    sb.AppendFormat("public {0} {1}{2}", strTypeName, obj.Name, vbCrLf)
                End If

                sb.AppendLine("{")

                If obj.Name = "Item" AndAlso obj.PropertyParameters.Count = 1 Then
                    sb.AppendFormat("get {{ return {0}[{1}]; }}{2}", _strFieldName, obj.PropertyParameters(0).ParameterName, vbCrLf)
                Else
                    sb.AppendFormat("get {{ return {0}.{1}; }}{2}", _strFieldName, obj.Name, vbCrLf)
                End If

                If obj.CanWrite Then
                    sb.AppendLine("set")
                    sb.AppendLine("{")
                    sb.AppendFormat("{0}.{1} = value;{2}", _strFieldName, obj.Name, vbCrLf)

                    If _bolIncludeOnPropertyChanged Then
                        sb.AppendFormat("{0}(""{1}"");{2}", Me.OnPropertyChangedMethodName, obj.Name, vbCrLf)
                    End If

                    sb.AppendLine("}")
                End If

                sb.AppendLine("}")
                sb.AppendLine("")
            Next

            sb.AppendLine("")
        End If

        sb.AppendLine("#endregion //Properties")
        sb.AppendLine("")
        '
        '
        'command properties
        sb.AppendLine("#region Command Properties")

        For Each obj As CreateCommandSource In (From x In Me.CommandsCollection Order By x.CommandName).ToList

            Dim bolUsesCommandParameter As Boolean = Not String.IsNullOrWhiteSpace(obj.CommandParameterType)

            sb.AppendLine("")
            sb.AppendFormat("public ICommand {0}{1}", obj.CommandName, vbCrLf)
            sb.AppendLine("{")
            sb.AppendLine("get")
            sb.AppendLine("{")
            sb.AppendFormat("if ({0} == null){1}", obj.FieldName, vbCrLf)
            sb.AppendLine("{")
            sb.AppendFormat("{0} = new ", obj.FieldName)

            If obj.UseRelayCommand Then
                sb.Append("RelayCommand")

            Else
                sb.Append("DelegateCommand")
            End If

            If bolUsesCommandParameter Then
                sb.AppendFormat("<{0}>", obj.CommandParameterType)
            End If

            sb.Append("(")

            If obj.ExecuteUseAddressOf Then
                sb.Append(obj.ExecuteMethodName)

            Else
                If bolUsesCommandParameter Then
                    sb.AppendFormat("param => {0}(param)", obj.ExecuteMethodName)
                Else
                    sb.AppendFormat("() => {0}()", obj.ExecuteMethodName)
                End If

            End If

            If obj.IncludeCanExecuteMethod Then
                sb.Append(", ")

                If obj.CanExecuteUseAddressOf Then
                    sb.Append(obj.CanExecuteMethodName)

                Else
                    If bolUsesCommandParameter Then
                        sb.AppendFormat("param => {0}(param)", obj.CanExecuteMethodName)
                    Else
                        sb.AppendFormat("() => {0}()", obj.CanExecuteMethodName)
                    End If

                End If

            End If

            sb.AppendLine(");")
            sb.AppendLine("}")
            sb.AppendFormat("return {0};{1}", obj.FieldName, vbCrLf)
            sb.AppendLine("}")
            sb.AppendLine("}")
        Next

        sb.AppendLine("")
        sb.AppendLine("#endregion //Command Properties")
        sb.AppendLine("")
        '
        '
        'constructor region
        sb.AppendLine("#region Constructors")
        sb.AppendLine("")
        sb.AppendLine("//TODO developers add your constructors here")
        sb.AppendLine("")
        sb.AppendLine("#endregion //Constructors")
        sb.AppendLine("")
        '
        '
        'command methods region
        sb.AppendLine("#region Command Methods")

        For Each obj As CreateCommandSource In (From x In Me.CommandsCollection Order By x.CommandName).ToList

            Dim bolUsesCommandParameter As Boolean = Not String.IsNullOrWhiteSpace(obj.CommandParameterType)

            If obj.IncludeCanExecuteMethod Then
                sb.AppendLine("")

                If bolUsesCommandParameter Then
                    sb.AppendFormat("bool {0}({1} param){2}", obj.CanExecuteMethodName, obj.CommandParameterType, vbCrLf)

                Else
                    sb.AppendFormat("bool {0}(){1}", obj.CanExecuteMethodName, vbCrLf)
                End If

                sb.AppendLine("{")
                sb.AppendLine("")
                sb.AppendLine("}")
            End If

            sb.AppendLine("")

            If bolUsesCommandParameter Then
                sb.AppendFormat("void {0}({1} param){2}", obj.ExecuteMethodName, obj.CommandParameterType, vbCrLf)

            Else
                sb.AppendFormat("void {0}(){1}", obj.ExecuteMethodName, vbCrLf)
            End If

            sb.AppendLine("{")
            sb.AppendLine("")
            sb.AppendLine("}")
        Next

        sb.AppendLine("")
        sb.AppendLine("#endregion //Command Methods")
        sb.AppendLine("")

        '
        '
        'INPC region
        If IncludeOnPropertyChangedEventHandler Then
            sb.AppendLine("#region INotifyProperty Changed Method ")
            sb.AppendLine("")
            sb.AppendFormat("protected void {0}(string propertyName){1}", Me.OnPropertyChangedMethodName, vbCrLf)
            sb.AppendLine("{")
            sb.AppendLine(" var handler = this.PropertyChanged;")
            sb.AppendLine("if (handler != null)")
            sb.AppendLine("{")
            sb.AppendLine("handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));")
            sb.AppendLine("}")
            sb.AppendLine("}")
            sb.AppendLine("")
            sb.AppendLine("#endregion //INotifyProperty Changed Method")
            sb.AppendLine("")
        End If

        _strViewModelText = sb.ToString
    End Sub

    Private Sub CreateVBViewModelText()

        Dim sb As New StringBuilder(4096)

        If IncludeOnPropertyChangedEventHandler Then
            sb.AppendLine("Implements System.ComponentModel.INotifyPropertyChanged")
            sb.AppendLine("")
        End If

        '
        '
        'declarations
        sb.AppendLine("#Region "" Declarations """)
        sb.AppendLine("")

        For Each obj As CreateCommandSource In (From x In Me.CommandsCollection Order By x.CommandName).ToList
            sb.AppendFormat("Private {0} As ICommand {1}", obj.FieldName, vbCrLf)
        Next

        sb.AppendFormat("Private {0} As {1}{2}", _strFieldName, _strPropertyType, vbCrLf)
        sb.AppendLine("")
        sb.AppendLine("#End Region")
        sb.AppendLine("")

        '
        '
        'events
        If IncludeOnPropertyChangedEventHandler Then
            sb.AppendLine("#Region "" Events """)
            sb.AppendLine("")
            sb.AppendLine("Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged")
            sb.AppendLine("")
            sb.AppendLine("#End Region")
            sb.AppendLine("")
        End If

        '
        '
        'properties
        sb.AppendLine("#Region "" Properties """)
        sb.AppendLine("")
        sb.Append(PropertySignature)
        sb.AppendFormat(" {0}() As {1}{2}", _strPropertyName, _strPropertyType, vbCrLf)
        sb.AppendLine("Get")
        sb.AppendFormat("Return {0}{1}", _strFieldName, vbCrLf)
        sb.AppendLine("End Get")

        If Not _bolIsPropertyReadOnly Then

            If _bolHasPrivateSetter Then
                sb.Append("Private ")
            End If

            sb.AppendFormat("Set(ByVal Value As {0}){1}", _strPropertyType, vbCrLf)
            sb.AppendFormat("{0} = Value{1}", _strFieldName, vbCrLf)

            If _bolIncludeOnPropertyChanged Then
                sb.AppendFormat("{0}(""{1}""){2}", Me.OnPropertyChangedMethodName, _strPropertyName, vbCrLf)
            End If

            sb.AppendLine("End Set")
        End If

        sb.AppendLine("End Property")
        sb.AppendLine("")

        If ExposePropertiesOnViewModel Then

            For Each obj As PropertyInformation In SelectedPropertyInformationCollection

                If obj.CanWrite Then
                    sb.Append("Public Property")

                Else
                    sb.Append("Public ReadOnly Property")
                End If

                Dim strPropertyName As String = obj.Name
                If strPropertyName = "Error" Then
                    strPropertyName = "[Error]"
                End If

                sb.AppendFormat(" {0}({1}) As {2}{3}", strPropertyName, obj.VBPropertyParameterString, obj.VBTypeName, vbCrLf)
                sb.AppendLine("Get")

                If strPropertyName = "Item" AndAlso obj.PropertyParameters.Count = 1 Then
                    sb.AppendFormat("Return {0}.{1}({2}){3}", _strFieldName, obj.Name, obj.PropertyParameters(0).ParameterName, vbCrLf)
                Else
                    sb.AppendFormat("Return {0}.{1}{2}", _strFieldName, obj.Name, vbCrLf)
                End If

                sb.AppendLine("End Get")

                If obj.CanWrite Then
                    sb.AppendFormat("Set(ByVal Value As {0}){1}", obj.VBTypeName, vbCrLf)
                    sb.AppendFormat("{0}.{1} = Value{2}", _strFieldName, obj.Name, vbCrLf)

                    If _bolIncludeOnPropertyChanged Then
                        sb.AppendFormat("{0}(""{1}"");{2}", Me.OnPropertyChangedMethodName, _strPropertyName, vbCrLf)
                    End If

                    sb.AppendLine("End Set")
                End If

                sb.AppendLine("End Property")
                sb.AppendLine("")
            Next

            sb.AppendLine("")
        End If

        sb.AppendLine("#End Region")
        sb.AppendLine("")
        '
        '
        'command properties
        sb.AppendLine("#Region "" Command Properties """)

        For Each obj As CreateCommandSource In (From x In Me.CommandsCollection Order By x.CommandName).ToList

            Dim bolUsesCommandParameter As Boolean = Not String.IsNullOrWhiteSpace(obj.CommandParameterType)

            sb.AppendLine("")
            sb.AppendFormat("Public ReadOnly Property {0}() As ICommand{1}", obj.CommandName, vbCrLf)
            sb.AppendLine("Get")
            sb.AppendFormat("If {0} Is Nothing Then{1}", obj.FieldName, vbCrLf)
            sb.AppendFormat("{0} = New ", obj.FieldName)

            If obj.UseRelayCommand Then
                sb.Append("RelayCommand")

            Else
                sb.Append("DelegateCommand")
            End If

            If bolUsesCommandParameter Then
                sb.AppendFormat("(Of {0})", obj.CommandParameterType)
            End If

            sb.Append("(")

            If obj.ExecuteUseAddressOf Then
                sb.AppendFormat("AddressOf {0}", obj.ExecuteMethodName)

            Else
                If bolUsesCommandParameter Then
                    sb.AppendFormat("Sub(param As {0}) {1}(param)", obj.CommandParameterType, obj.ExecuteMethodName)
                Else
                    sb.AppendFormat("Sub() {0}()", obj.ExecuteMethodName)
                End If

            End If

            If obj.IncludeCanExecuteMethod Then
                sb.Append(", ")

                If obj.CanExecuteUseAddressOf Then
                    sb.AppendFormat("AddressOf {0}", obj.CanExecuteMethodName)

                Else
                    If bolUsesCommandParameter Then
                        sb.AppendFormat("Function(param As {0}) {1}(param)", obj.CommandParameterType, obj.CanExecuteMethodName)
                    Else
                        sb.AppendFormat("Function() {0}()", obj.CanExecuteMethodName)
                    End If

                End If

            End If

            sb.AppendLine(")")
            sb.AppendLine("End If")
            sb.AppendFormat("Return {0}{1}", obj.FieldName, vbCrLf)
            sb.AppendLine("End Get")
            sb.AppendLine("End Property")
        Next

        sb.AppendLine("")
        sb.AppendLine("#End Region")
        sb.AppendLine("")
        '
        '
        'constructor region
        sb.AppendLine("#Region "" Constructors """)
        sb.AppendLine("")
        sb.AppendLine("Public Sub New()")
        sb.AppendLine("")
        sb.AppendLine("End Sub")
        sb.AppendLine("")

        If ExposePropertiesOnViewModel AndAlso _strPropertyType.IndexOf("(Of") = -1 AndAlso _strPropertyType.IndexOf("<") = -1 Then
            sb.AppendFormat("Public Sub New({0} As {1})", _strFieldName.Replace("_", ""), _strPropertyType)
            sb.AppendLine("")
            sb.AppendFormat("{0} = {1}", _strFieldName, _strFieldName.Replace("_", ""))
            sb.AppendLine("")
            sb.AppendLine("End Sub")
            sb.AppendLine("")
        End If

        sb.AppendLine("#End Region")
        sb.AppendLine("")
        '
        '
        'command methods region
        sb.AppendLine("#Region "" Command Methods """)

        For Each obj As CreateCommandSource In (From x In Me.CommandsCollection Order By x.CommandName).ToList

            Dim bolUsesCommandParameter As Boolean = Not String.IsNullOrWhiteSpace(obj.CommandParameterType)

            If obj.IncludeCanExecuteMethod Then
                sb.AppendLine("")

                If bolUsesCommandParameter Then
                    sb.AppendFormat("Private Function {0}(ByVal param As {1}) As Boolean{2}", obj.CanExecuteMethodName, obj.CommandParameterType, vbCrLf)

                Else
                    sb.AppendFormat("Private Function {0}() As Boolean{1}", obj.CanExecuteMethodName, vbCrLf)
                End If

                sb.AppendLine("")
                sb.AppendLine("End Function")
            End If

            sb.AppendLine("")

            If bolUsesCommandParameter Then
                sb.AppendFormat("Private Sub {0}(ByVal param As {1}){2}", obj.ExecuteMethodName, obj.CommandParameterType, vbCrLf)
                sb.AppendLine("")
                sb.AppendLine("End Sub")

            Else
                sb.AppendFormat("Private Sub {0}(){1}", obj.ExecuteMethodName, vbCrLf)
                sb.AppendLine("")
                sb.AppendLine("End Function")
            End If

        Next

        sb.AppendLine("")
        sb.AppendLine("#End Region")
        sb.AppendLine("")

        '
        '
        'INPC region
        If IncludeOnPropertyChangedEventHandler Then
            sb.AppendLine("#Region "" INotifyProperty Changed Method """)
            sb.AppendLine("")
            sb.AppendFormat("Protected Sub {0}(ByVal strPropertyName As String){1}", Me.OnPropertyChangedMethodName, vbCrLf)
            sb.AppendLine("")
            sb.AppendLine("If Me.PropertyChangedEvent IsNot Nothing Then")
            sb.AppendLine("RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(strPropertyName))")
            sb.AppendLine("End If")
            sb.AppendLine("")
            sb.AppendLine("End Sub")
            sb.AppendLine("")
            sb.AppendLine("#End Region")
        End If

        _strViewModelText = sb.ToString
    End Sub
    
    Private Sub cboPropertyChangedMethodNames_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.cboPropertyChangedMethodNames.RemoveHandler(ComboBox.SelectionChangedEvent, New SelectionChangedEventHandler(AddressOf cboPropertyChangedMethodNames__SelectionChanged))
        Me.cboPropertyChangedMethodNames.Items.Add("RaisePropertyChanged")
        Me.cboPropertyChangedMethodNames.Items.Add("OnPropertyChanged")
        Me.cboPropertyChangedMethodNames.Items.Add("NotifyPropertyChanged")
        Me.cboPropertyChangedMethodNames.Items.Add("FirePropertyChanged")
        Me.cboPropertyChangedMethodNames.SelectedIndex = -1
        Me.cboPropertyChangedMethodNames.AddHandler(ComboBox.SelectionChangedEvent, New SelectionChangedEventHandler(AddressOf cboPropertyChangedMethodNames__SelectionChanged))
    End Sub
    
    Private Sub cboPropertyChangedMethodNames__SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs)

        If Me.cboPropertyChangedMethodNames.SelectedValue Is Nothing OrElse Me.cboPropertyChangedMethodNames.SelectedIndex = -1 Then
            Exit Sub
        End If

        Me.OnPropertyChangedMethodName = Me.cboPropertyChangedMethodNames.SelectedValue.ToString
    End Sub

#End Region
End Class
