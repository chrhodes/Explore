'
<Serializable()> _
Public Class PropertyParameter

#Region " Declarations "
    Private _strParameterName As String = String.Empty
    Private _strParameterTypeName As String = String.Empty
#End Region

#Region " Properties "

    Public Property ParameterName() As String
        Get
            Return _strParameterName
        End Get
        Set(ByVal value As String)
            _strParameterName = value
        End Set
    End Property

    Public Property ParameterTypeName() As String
        Get
            Return _strParameterTypeName
        End Get
        Set(ByVal value As String)
            _strParameterTypeName = value
        End Set
    End Property
#End Region

#Region " Constructor "

    Public Sub New()

    End Sub

    Public Sub New(ByVal strParameterName As String, ByVal strParameterTypeName As String)
        _strParameterName = strParameterName
        _strParameterTypeName = strParameterTypeName
    End Sub

#End Region

End Class
