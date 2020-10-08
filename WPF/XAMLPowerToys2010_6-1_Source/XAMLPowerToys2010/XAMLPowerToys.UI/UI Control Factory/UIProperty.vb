Imports System.ComponentModel
Imports System.Runtime.Serialization
'
<Serializable()> Public Class UIProperty
    Implements INotifyPropertyChanged

#Region " Declarations "

    Private _strPropertyValue As String = String.Empty
    Private _strProperyName As String = String.Empty

#End Region

#Region " Properties "

    Public Property PropertyValue() As String
        Get
            Return _strPropertyValue
        End Get
        Set(ByVal Value As String)
            _strPropertyValue = Value
            OnPropertyChanged("PropertyValue")
        End Set
    End Property

    Public Property ProperyName() As String
        Get
            Return _strProperyName
        End Get
        Set(ByVal Value As String)
            _strProperyName = Value
            OnPropertyChanged("ProperyName")
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

    Public Sub New()
    End Sub

    Public Sub New(ByVal strProperyName As String, ByVal strPropertyValue As String)
        _strProperyName = strProperyName
        _strPropertyValue = strPropertyValue
    End Sub

#End Region

End Class
