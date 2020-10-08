Imports System.ComponentModel
Imports System.Windows.Data
'
<Serializable()>
Public Class CellContent
    Implements INotifyPropertyChanged

#Region " Declarations "

    Private _enumBindingMode As BindingMode = BindingMode.TwoWay
    Private _intColumn As Integer
    Private _intMaximumLength As Nullable(Of Integer)
    Private _intRow As Integer
    Private _intWidth As Nullable(Of Integer)
    Private _strBindingPath As String = String.Empty
    Private _strControlLabel As String = String.Empty
    Private _strControlType As ControlType = ControlType.None
    Private _strDataType As String = String.Empty
    Private _strStringFormat As String = String.Empty

#End Region

#Region " INotifyPropertyChanged Serializable "

    <NonSerialized()>
    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
    End Sub

#End Region

#Region " Properties "

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

    Public ReadOnly Property Column() As Integer
        Get
            Return _intColumn
        End Get
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

    Public Property ControlType() As ControlType
        Get
            Return _strControlType
        End Get
        Set(ByVal Value As ControlType)
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

    Public Property MaximumLength() As Nullable(Of Integer)
        Get
            Return _intMaximumLength
        End Get
        Set(ByVal Value As Nullable(Of Integer))
            _intMaximumLength = Value
            OnPropertyChanged("MaximumLength")
        End Set
    End Property

    Public ReadOnly Property Row() As Integer
        Get
            Return _intRow
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

    Public Sub New(ByVal intRow As Integer, ByVal intColumn As Integer)
        _intRow = intRow
        _intColumn = intColumn
    End Sub

    Public Sub New(ByVal strDataType As String, ByVal intRow As Integer, ByVal intColumn As Integer)
        _strDataType = strDataType
        _intRow = intRow
        _intColumn = intColumn
    End Sub

#End Region

End Class
