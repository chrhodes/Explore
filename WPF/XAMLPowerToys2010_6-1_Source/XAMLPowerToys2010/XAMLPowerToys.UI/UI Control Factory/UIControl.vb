Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.Text
'
<Serializable()> Public Class UIControl
    Implements INotifyPropertyChanged

#Region " Declarations "
    Private _bolGenerateControlName As Boolean
    Private _bolIncludeNotifyOnValidationError As Boolean
    Private _bolIncludeTargetNullValueForNullableBindings As Boolean
    Private _bolIncludeValidatesOnDataErrors As Boolean
    Private _bolIncludeValidatesOnExceptions As Boolean
    Private _enumControlRole As UIControlRole = UIControlRole.Label
    Private _enumUIPlatform As UIPlatform = UIPlatform.WPF
    Private _objControlProperties As New ObservableCollection(Of UIProperty)
    Private _strControlType As String = String.Empty

#End Region

#Region " INotifyPropertyChanged Serializable "

    <NonSerialized()>
    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
    End Sub

#End Region

#Region " Properties "

    Public ReadOnly Property BindingPropertyString() As String
        Get

            Dim strReturn As String = String.Empty

            If _bolIncludeNotifyOnValidationError Then
                strReturn = ", NotifyOnValidationError=True"
            End If

            If _bolIncludeValidatesOnDataErrors Then
                strReturn = String.Concat(strReturn, ", ValidatesOnDataErrors=True")
            End If

            If _bolIncludeValidatesOnExceptions Then
                strReturn = String.Concat(strReturn, ", ValidatesOnExceptions=True")
            End If

            Return strReturn
        End Get
    End Property

    Public ReadOnly Property ControlProperties() As ObservableCollection(Of UIProperty)
        Get
            Return _objControlProperties
        End Get
    End Property

    Public ReadOnly Property ControlRole() As UIControlRole
        Get
            Return _enumControlRole
        End Get
    End Property

    Public ReadOnly Property ControlRoleName() As String
        Get
            Return _enumControlRole.ToString
        End Get
    End Property

    Public Property ControlType() As String
        Get
            Return _strControlType
        End Get
        Set(ByVal Value As String)
            _strControlType = Value
            OnPropertyChanged("ControlType")
        End Set
    End Property

    Public Property GenerateControlName() As Boolean
        Get
            Return _bolGenerateControlName
        End Get
        Set(ByVal Value As Boolean)
            _bolGenerateControlName = Value
            OnPropertyChanged("GenerateControlName")
        End Set
    End Property

    Public Property IncludeNotifyOnValidationError() As Boolean
        Get
            Return _bolIncludeNotifyOnValidationError
        End Get
        Set(ByVal Value As Boolean)
            _bolIncludeNotifyOnValidationError = Value
            OnPropertyChanged("IncludeNotifyOnValidationError")
        End Set
    End Property

    Public Property IncludeTargetNullValueForNullableBindings() As Boolean
        Get
            Return _bolIncludeTargetNullValueForNullableBindings
        End Get
        Set(ByVal Value As Boolean)
            _bolIncludeTargetNullValueForNullableBindings = Value
            OnPropertyChanged("IncludeTargetNullValueForNullableBindings")
        End Set
    End Property

    Public Property IncludeValidatesOnDataErrors() As Boolean
        Get
            Return _bolIncludeValidatesOnDataErrors
        End Get
        Set(ByVal Value As Boolean)
            _bolIncludeValidatesOnDataErrors = Value
            OnPropertyChanged("IncludeValidatesOnDataErrors")
        End Set
    End Property

    Public Property IncludeValidatesOnExceptions() As Boolean
        Get
            Return _bolIncludeValidatesOnExceptions
        End Get
        Set(ByVal Value As Boolean)
            _bolIncludeValidatesOnExceptions = Value
            OnPropertyChanged("IncludeValidatesOnExceptions")
        End Set
    End Property

    Public ReadOnly Property UIPlatform() As UIPlatform
        Get
            Return _enumUIPlatform
        End Get
    End Property

#End Region

#Region " Constructors "

    Public Sub New()
    End Sub

    Public Sub New(ByVal enumUIPlatform As UIPlatform, ByVal enumControlRole As UIControlRole, ByVal strControlType As String)
        _enumUIPlatform = enumUIPlatform
        _enumControlRole = enumControlRole
        _strControlType = strControlType
    End Sub

#End Region

#Region " Methods "

    ''' <summary>
    '''     Returns a string representing this control.  This does not include any data binding properties
    ''' </summary>
    ''' <param name="strStuffing" type="String">
    '''     <para>
    '''         
    '''     </para>
    ''' </param>
    ''' <param name="bolCloseTag" type="Boolean">
    '''     <para>
    '''         If True will completely close the tag.  This is typical of controls that are rendered on one line.
    '''     </para>
    ''' </param>
    ''' <returns>
    '''     A String value representing the control.
    ''' </returns>
    Public Function MakeControlFromDefaults(ByVal strStuffing As String, ByVal bolCloseTag As Boolean, ByVal strPathUsedToCreateNameProperty As String) As String

        Dim sb As New StringBuilder(1024)
        sb.AppendFormat("<{0} ", Me.ControlType)

        If Me.GenerateControlName AndAlso Not String.IsNullOrEmpty(strPathUsedToCreateNameProperty) Then
            sb.AppendFormat(" x:Name=""{0}"" ", String.Concat(GetControlHungarian, strPathUsedToCreateNameProperty))
        End If

        sb.Append(strStuffing)

        For Each obj As UIProperty In Me.ControlProperties

            If Not strStuffing.Contains(obj.ProperyName) Then
                sb.AppendFormat(" {0}=""{1}""", obj.ProperyName, obj.PropertyValue)
            End If

        Next

        If bolCloseTag Then
            sb.Append(" />")

        Else
            sb.Append(" >")
        End If

        Return sb.ToString.Replace("  ", " ")
    End Function

    Private Function GetControlHungarian() As String

        Select Case Me.ControlRole

            Case UIControlRole.Border
                Return "bdr"

            Case UIControlRole.CheckBox
                Return "chk"

            Case UIControlRole.ComboBox
                Return "cbo"

            Case UIControlRole.DataGrid
                Return "dg"

            Case UIControlRole.DatePicker
                Return "dp"

            Case UIControlRole.Grid
                Return "grid"

            Case UIControlRole.Image
                Return "img"

            Case UIControlRole.Label
                Return "lbl"

            Case UIControlRole.TextBlock
                Return "tb"

            Case UIControlRole.TextBox
                Return "txt"

            Case Else
                Return "NOTASSIGNED"
        End Select

    End Function

#End Region

End Class
