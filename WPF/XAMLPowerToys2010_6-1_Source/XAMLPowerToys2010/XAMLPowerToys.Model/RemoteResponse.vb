'
<Serializable()> Public Class RemoteResponse(Of T)

#Region " Declarations "

    Private _enumResponseStatus As ResponseStatus = ResponseStatus.Success
    Private _exException As Exception = Nothing
    Private _objResult As T
    Private _strCustomMessage As String = String.Empty

#End Region

#Region " Properties "

    Public ReadOnly Property CustomMessageAndException() As String
        Get

            Dim strExceptionMessage As String = String.Empty

            If _exException IsNot Nothing Then
                strExceptionMessage = _exException.Message
            End If

            If String.IsNullOrEmpty(_strCustomMessage) Then
                Return strExceptionMessage

            Else
                Return String.Concat(_strCustomMessage, IIf(String.IsNullOrEmpty(strExceptionMessage), String.Empty, " Message: " & strExceptionMessage))
            End If

        End Get
    End Property

    Public ReadOnly Property Exception() As Exception
        Get
            Return _exException
        End Get
    End Property

    Public ReadOnly Property ResponseStatus() As ResponseStatus
        Get
            Return _enumResponseStatus
        End Get
    End Property

    Public ReadOnly Property Result() As T
        Get
            Return _objResult
        End Get
    End Property

#End Region

#Region " Constructors "

    Public Sub New(ByVal objResult As T, ByVal enumResponseStatus As ResponseStatus, ByVal exException As Exception, ByVal strCustomMessage As String)
        _objResult = objResult
        _enumResponseStatus = enumResponseStatus
        _exException = exException
        _strCustomMessage = strCustomMessage
    End Sub

    Public Sub New()

    End Sub

#End Region

End Class
