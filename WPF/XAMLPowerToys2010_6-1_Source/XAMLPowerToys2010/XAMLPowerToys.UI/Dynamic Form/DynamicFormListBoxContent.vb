Imports System.ComponentModel

Public Class DynamicFormListBoxContent
    Implements INotifyPropertyChanged

#Region " Declarations "

    Private _bolCanWrite As Boolean = True
    Private _bolRenderAsGridTempldateColumn As Nullable(Of Boolean) = False
    Private _enumBindingMode As BindingMode = BindingMode.TwoWay
    Private _intMaximumLength As Nullable(Of Integer)
    Private _intWidth As Nullable(Of Integer)
    Private _strAssociatedLabel As String = String.Empty
    Private _strBindingPath As String = String.Empty
    Private _strControlLabel As String = String.Empty
    Private _strControlType As DynamicFormControlType = DynamicFormControlType.TextBox
    Private _strDataType As String = String.Empty
    Private _strStringFormat As String = String.Empty
    Private _strTypeNamespace As String = String.Empty
    Private _strFieldDescription As String = String.Empty
    Private _strDescriptionViewerPosition As String = "Auto"
    Private _strLabelPosition As String = "Auto"

#End Region

#Region " Events "

    Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

#End Region

#Region " Properties "

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

    Public Property AssociatedLabel() As String
        Get
            Return _strAssociatedLabel
        End Get
        Set(ByVal Value As String)
            _strAssociatedLabel = Value
            OnPropertyChanged("AssociatedLabel")
        End Set
    End Property

    Public Property BindingMode() As BindingMode
        Get
            Return _enumBindingMode
        End Get
        Set(ByVal Value As BindingMode)
            _enumBindingMode = Value
            OnPropertyChanged("BindingMode")
        End Set
    End Property

    Public Property BindingPath() As String
        Get
            Return _strBindingPath
        End Get
        Set(ByVal Value As String)
            _strBindingPath = Value
            OnPropertyChanged("BindingPath")
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

    Public Property ControlLabel() As String
        Get
            Return _strControlLabel
        End Get
        Set(ByVal Value As String)
            _strControlLabel = Value
            OnPropertyChanged("ControlLabel")
        End Set
    End Property

    Public Property ControlType() As DynamicFormControlType
        Get
            Return _strControlType
        End Get
        Set(ByVal Value As DynamicFormControlType)
            _strControlType = Value
            OnPropertyChanged("ControlType")
        End Set
    End Property

    Public Property DataType() As String
        Get
            Return _strDataType
        End Get
        Set(ByVal Value As String)
            _strDataType = Value
            OnPropertyChanged("DataType")
        End Set
    End Property

    Public ReadOnly Property FullName() As String
        Get
            Return String.Format("{0}{1} - {2}", _strBindingPath, IIf(_bolCanWrite, String.Empty, " (r)"), Me.NameSpaceTypeName)
        End Get
    End Property

    Public Property MaximumLength() As Nullable(Of Integer)
        Get
            Return _intMaximumLength
        End Get
        Set(ByVal Value As Nullable(Of Integer))
            _intMaximumLength = Value
            OnPropertyChanged("MaximumLength")
        End Set
    End Property

    Public ReadOnly Property NameAndWriteable() As String
        Get

            If _bolCanWrite Then
                Return _strBindingPath

            Else
                Return String.Format("{0}  (r)", _strBindingPath)
            End If

        End Get
    End Property

    Public ReadOnly Property NameSpaceTypeName() As String
        Get
            Return String.Concat(_strTypeNamespace, ":", _strDataType)
        End Get
    End Property

    Public Property RenderAsGridTempldateColumn() As Nullable(Of Boolean)
        Get
            Return _bolRenderAsGridTempldateColumn
        End Get
        Set(ByVal Value As Nullable(Of Boolean))
            _bolRenderAsGridTempldateColumn = Value
            OnPropertyChanged("RenderAsGridTempldateColumn")
        End Set
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

    Public Property TypeNamespace() As String
        Get
            Return _strTypeNamespace
        End Get
        Set(ByVal Value As String)
            _strTypeNamespace = Value
            OnPropertyChanged("TypeNamespace")
        End Set
    End Property

    Public Property Width() As Nullable(Of Integer)
        Get
            Return _intWidth
        End Get
        Set(ByVal Value As Nullable(Of Integer))
            _intWidth = Value
            OnPropertyChanged("Width")
        End Set
    End Property

#End Region

#Region " Constructors "

    Public Sub New()
    End Sub

#End Region

#Region " Methods "

    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)

        Dim handler As PropertyChangedEventHandler = Me.PropertyChangedEvent

        If handler IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
        End If

    End Sub

#End Region

End Class
