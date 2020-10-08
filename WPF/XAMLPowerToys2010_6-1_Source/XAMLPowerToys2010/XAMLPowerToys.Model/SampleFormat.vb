'
<Serializable()>
Public Class SampleFormat

#Region " Declarations "

    Private _strDataType As String = String.Empty
    Private _strExample As String = String.Empty
    Private _strStringFormat As String = String.Empty

#End Region

#Region " Properties "

    Public Property DataType() As String
        Get
            Return _strDataType
        End Get
        Set(ByVal Value As String)
            _strDataType = Value
        End Set
    End Property

    Public Property Example() As String
        Get
            Return _strExample
        End Get
        Set(ByVal Value As String)
            _strExample = Value
        End Set
    End Property

    Public Property StringFormat() As String
        Get
            Return _strStringFormat
        End Get
        Set(ByVal Value As String)
            _strStringFormat = Value
        End Set
    End Property

    Public ReadOnly Property StringFormatAndExample() As String
        Get
            Return Me.ToString
        End Get
    End Property

    Public ReadOnly Property StringFormatParsedValue() As String
        Get
            Return _strStringFormat.Replace("\", String.Empty)
        End Get
    End Property

#End Region

#Region " Constructors "

    Public Sub New()
    End Sub

    Public Sub New(ByVal strDataType As String, ByVal strExample As String, ByVal strStringFormat As String)
        _strDataType = strDataType
        _strExample = strExample
        _strStringFormat = strStringFormat
    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String
        Return String.Format("{0} - {1}", _strStringFormat.Replace("\", String.Empty), _strExample)
    End Function

#End Region

End Class
