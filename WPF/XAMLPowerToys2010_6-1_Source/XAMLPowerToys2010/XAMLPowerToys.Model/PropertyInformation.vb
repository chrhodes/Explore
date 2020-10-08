Imports System.ComponentModel
'
<Serializable()> Public Class PropertyInformation
    Implements INotifyPropertyChanged

#Region " Declarations "

    Private _bolCanWrite As Boolean = False
    Private _bolFieldListIncludeGridAttachedProperties As Boolean = False
    Private _bolHasBeenUsed As Boolean = False
    Private _enumFieldListControlType As ControlType = ControlType.None
    Private _strName As String = String.Empty
    Private _strStringFormat As String = String.Empty
    Private _strTypeName As String = String.Empty
    Private _strTypeNamespace As String = String.Empty
    Private _bolIsSelected As Boolean = False
    Private _strFieldDescription As String = String.Empty
    Private _strDescriptionViewerPosition As String = "Auto"
    Private _strLabelPosition As String = "Auto"
    Private _objGenericArguments As New List(Of String)
    Private _objPropertyParameters As New List(Of PropertyParameter)

#End Region

#Region " INotifyPropertyChanged Serializable "

    <NonSerialized()>
    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
    End Sub

#End Region

#Region " Properties "

    Public Property GenericArguments() As List(Of String)
        Get
            Return _objGenericArguments
        End Get
        Set(ByVal value As List(Of String))
            _objGenericArguments = value
            OnPropertyChanged("GenericArguments")
        End Set
    End Property

    Public Property PropertyParameters() As List(Of PropertyParameter)
        Get
            Return _objPropertyParameters
        End Get
        Set(ByVal value As List(Of PropertyParameter))
            _objPropertyParameters = value
            OnPropertyChanged("PropertyParameters")
        End Set

    End Property

    Public Property FieldDescription() As String
        Get
            Return _strFieldDescription
        End Get
        Set(ByVal value As String)
            _strFieldDescription = value
            OnPropertyChanged("FieldDescription")
        End Set
    End Property

    Public Property DescriptionViewerPosition() As String
        Get
            Return _strDescriptionViewerPosition
        End Get
        Set(ByVal value As String)
            _strDescriptionViewerPosition = value
            OnPropertyChanged("DescriptionViewerPosition")
        End Set
    End Property

    Public Property LabelPosition() As String
        Get
            Return _strLabelPosition
        End Get
        Set(ByVal value As String)
            _strLabelPosition = value
            OnPropertyChanged("LabelPosition")
        End Set
    End Property

    Public Property IsSelected() As Boolean
        Get
            Return _bolIsSelected
        End Get
        Set(ByVal value As Boolean)
            _bolIsSelected = value
            OnPropertyChanged("IsSelected")
        End Set
    End Property

    Public Property CanWrite() As Boolean
        Get
            Return _bolCanWrite
        End Get
        Set(ByVal Value As Boolean)
            _bolCanWrite = Value
            OnPropertyChanged("CanWrite")
        End Set
    End Property

    Public Property FieldListControlType() As ControlType
        Get
            Return _enumFieldListControlType
        End Get
        Set(ByVal Value As ControlType)
            _enumFieldListControlType = Value
            OnPropertyChanged("FieldListControlType")
        End Set
    End Property

    Public Property FieldListIncludeGridAttachedProperties() As Boolean
        Get
            Return _bolFieldListIncludeGridAttachedProperties
        End Get
        Set(ByVal Value As Boolean)
            _bolFieldListIncludeGridAttachedProperties = Value
            OnPropertyChanged("FieldListIncludeGridAttachedProperties")
        End Set
    End Property

    Public ReadOnly Property FullName() As String
        Get
            Return String.Format("{0}{1} - {2}", _strName, IIf(_bolCanWrite, String.Empty, " (r)"), _strTypeName)
        End Get
    End Property

    Public Property HasBeenUsed() As Boolean
        Get
            Return _bolHasBeenUsed
        End Get
        Set(ByVal Value As Boolean)
            _bolHasBeenUsed = Value
            OnPropertyChanged("HasBeenUsed")
        End Set
    End Property

    Public ReadOnly Property CPrivateFieldName() As String
        Get
            Return String.Concat("_", PascalFieldName)
        End Get
    End Property

    Private ReadOnly Property PascalFieldName() As String
        Get
            Return String.Concat(_strName.Substring(0, 1).ToLower, _strName.Substring(1))
        End Get
    End Property

    Public ReadOnly Property LabelText() As String
        Get

            Dim strValue As String = _strName
            Dim sb As New System.Text.StringBuilder(256)
            Dim bolFoundUpperCase As Boolean = False

            For intX As Integer = 0 To strValue.Length - 1

                If Not bolFoundUpperCase AndAlso Char.IsUpper(strValue, intX) Then
                    bolFoundUpperCase = True

                    If intX = 0 Then
                        sb.Append(strValue.Substring(intX, 1))

                    Else
                        sb.Append(" ")
                        sb.Append(strValue.Substring(intX, 1))
                    End If

                    Continue For
                End If

                If Not bolFoundUpperCase Then
                    Continue For
                End If

                If Char.IsUpper(strValue, intX) Then
                    sb.Append(" ")
                    sb.Append(strValue.Substring(intX, 1))

                ElseIf Char.IsLetterOrDigit(strValue, intX) Then
                    sb.Append(strValue.Substring(intX, 1))
                End If

            Next

            Return sb.ToString
        End Get
    End Property

    Public Property Name() As String
        Get
            Return _strName
        End Get
        Set(ByVal Value As String)
            _strName = Value
            OnPropertyChanged("Name")
        End Set
    End Property

    Public ReadOnly Property NameAndWriteable() As String
        Get

            If _bolCanWrite Then
                Return _strName

            Else
                Return String.Format("{0}  (r)", _strName)
            End If

        End Get
    End Property

    Public ReadOnly Property NameSpaceTypeName() As String
        Get
            Return String.Concat(_strTypeNamespace, ":", _strTypeName)
        End Get
    End Property

    Public Property StringFormat() As String
        Get
            Return _strStringFormat
        End Get
        Set(ByVal Value As String)
            _strStringFormat = Value
            OnPropertyChanged("StringFormat")
        End Set
    End Property

    Public Property TypeName() As String
        Get
            Return _strTypeName
        End Get
        Set(ByVal Value As String)
            _strTypeName = Value
            OnPropertyChanged("TypeName")
        End Set
    End Property

    Public Property TypeNamespace() As String
        Get
            Return _strTypeNamespace
        End Get
        Set(ByVal Value As String)
            _strTypeNamespace = Value
            OnPropertyChanged("TypeNamespace")
        End Set
    End Property

#End Region

#Region " Constructors "

    Public Sub New(ByVal bolCanWrite As Boolean, ByVal strName As String, ByVal strTypeName As String, ByVal strTypeNamespace As String)
        _bolCanWrite = bolCanWrite
        _strName = strName
        _strTypeName = strTypeName
        _strTypeNamespace = strTypeNamespace

        If strTypeName.Contains("Decimal") Then
            _strStringFormat = "{0:c}"

        ElseIf strTypeName.Contains("Date") Then
            _strStringFormat = "{0:d}"
        End If

    End Sub

    Public Sub New()
    End Sub

#End Region

#Region " Methods "

    Public Function VBPropertyParameterString() As String

        If _objPropertyParameters.Count = 0 Then
            Return String.Empty
        End If

        Dim strReturn As String = String.Empty

        For Each obj As PropertyParameter In _objPropertyParameters
            strReturn = String.Concat(strReturn, obj.ParameterName, " As ", obj.ParameterTypeName, ", ")
        Next

        Return strReturn.Substring(0, strReturn.Length - 2)

    End Function


    Public Function CSPropertyParameterString() As String

        If _objPropertyParameters.Count = 0 Then
            Return String.Empty
        End If

        Dim strReturn As String = String.Empty

        For Each obj As PropertyParameter In _objPropertyParameters
            strReturn = String.Concat(strReturn, obj.ParameterTypeName, " ", obj.ParameterName, ", ")
        Next

        Return strReturn.Substring(0, strReturn.Length - 2)

    End Function

    Public Function VBTypeName() As String

        If Me.TypeName.EndsWith("]") Then
            Return Me.TypeName.Replace("[", "(").Replace("]", ")")

        Else
            Return Me.TypeName
        End If

    End Function

    Public Function VBPrivateFieldName(ByVal bolUseHungarian As Boolean) As String

        If bolUseHungarian Then
            Return String.Concat(GetHungarian, PascalFieldName)

        Else
            Return String.Concat("_", PascalFieldName)
        End If

    End Function

    Private Function GetHungarian() As String

        Select Case Me.TypeName

            Case "Boolean"
                Return "_bol"

            Case "Byte"
                Return "_byt"

            Case "Char"
                Return "_chr"

            Case "DateTime"
                Return "_dat"

            Case "Decimal"
                Return "_dec"

            Case "Double"
                Return "_dbl"

            Case "Int16"
                Return "_i16"

            Case "Integer", "Int32"
                Return "_int"

            Case "Int64"
                Return "_i64"

            Case "Single"
                Return "_sng"

            Case "String"
                Return "_str"

            Case Else
                Return "_obj"
        End Select

    End Function

#End Region

End Class
