Imports System.Collections.ObjectModel
Imports System.ComponentModel
'
<Serializable()> Public Class ClassEntity
    Implements INotifyPropertyChanged

#Region " Declarations "

    Private _strSilverlightVersion As String = String.Empty
    Private _bolIsSilverlight As Boolean = False
    Private _bolSuccess As Boolean = False
    Private _exException As Exception = Nothing
    Private _objPropertyInfomation As New ObservableCollection(Of PropertyInformation)
    Private _strClassName As String = String.Empty

#End Region

#Region " Properties "

    Public Property SilverlightVersion As String
        Get
            Return _strSilverlightVersion
        End Get
        Set(ByVal value As String)
            _strSilverlightVersion = value
            OnPropertyChanged("SilverlightVersion")
        End Set
    End Property

    Public Property ClassName() As String
        Get
            Return _strClassName
        End Get
        Set(ByVal Value As String)
            _strClassName = Value
            OnPropertyChanged("ClassName")
        End Set
    End Property

    Public Property Exception() As Exception
        Get
            Return _exException
        End Get
        Set(ByVal Value As Exception)
            _exException = Value
            OnPropertyChanged("Exception")
        End Set
    End Property

    Public Property IsSilverlight() As Boolean
        Get
            Return _bolIsSilverlight
        End Get
        Set(ByVal Value As Boolean)
            _bolIsSilverlight = Value
            OnPropertyChanged("IsSilverlight")
        End Set
    End Property

    Public Property PropertyInfomation() As ObservableCollection(Of PropertyInformation)
        Get
            Return _objPropertyInfomation
        End Get
        Set(ByVal Value As ObservableCollection(Of PropertyInformation))
            _objPropertyInfomation = Value
        End Set
    End Property

    Public Property Success() As Boolean
        Get
            Return _bolSuccess
        End Get
        Set(ByVal Value As Boolean)
            _bolSuccess = Value
            OnPropertyChanged("Success")
        End Set
    End Property

#End Region

#Region " INotifyPropertyChanged Serializable "

    <NonSerialized()>
    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
    End Sub

#End Region

#Region " Constructors "

    Public Sub New(ByVal strClassName As String, ByVal bolIsSilverlight As Boolean)
        _strClassName = strClassName
        _bolIsSilverlight = bolIsSilverlight
    End Sub

    Public Sub New()

    End Sub
#End Region

End Class
