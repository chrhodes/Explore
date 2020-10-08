
Public Class CreateCommandSource

#Region " Declarations "

    Private _bolCanExecuteUseAddressOf As Boolean = False
    Private _bolExecuteUseAddressOf As Boolean = False
    Private _bolIncludeCanExecuteMethod As Boolean = False
    Private _bolUseRelayCommand As Boolean = False
    Private _strCanExecuteMethodName As String = String.Empty
    Private _strCommandName As String = String.Empty
    Private _strCommandParameterType As String = String.Empty
    Private _strExecuteMethodName As String = String.Empty
    Private _strFieldName As String = String.Empty

#End Region

#Region " Properties "

    Public ReadOnly Property CanExecuteMethodName() As String
        Get
            Return _strCanExecuteMethodName
        End Get
    End Property

    Public ReadOnly Property CanExecuteUseAddressOf() As Boolean
        Get
            Return _bolCanExecuteUseAddressOf
        End Get
    End Property

    Public ReadOnly Property CommandName() As String
        Get
            Return _strCommandName
        End Get
    End Property

    Public ReadOnly Property CommandParameterType() As String
        Get
            Return _strCommandParameterType
        End Get
    End Property

    Public ReadOnly Property ExecuteMethodName() As String
        Get
            Return _strExecuteMethodName
        End Get
    End Property

    Public ReadOnly Property ExecuteUseAddressOf() As Boolean
        Get
            Return _bolExecuteUseAddressOf
        End Get
    End Property

    Public ReadOnly Property FieldName() As String
        Get
            Return _strFieldName
        End Get
    End Property

    Public ReadOnly Property IncludeCanExecuteMethod() As Boolean
        Get
            Return _bolIncludeCanExecuteMethod
        End Get
    End Property

    Public ReadOnly Property UseRelayCommand() As Boolean
        Get
            Return _bolUseRelayCommand
        End Get
    End Property

#End Region

#Region " Constructor "

    Public Sub New(ByVal bolCanExecuteUseAddressOf As Boolean, ByVal bolExecuteUseAddressOf As Boolean, ByVal bolIncludeCanExecuteMethod As Boolean, ByVal bolUseRelayCommand As Boolean, ByVal strCanExecuteMethodName As String, ByVal strCommandName As String, ByVal strCommandParameterType As String, ByVal strExecuteMethodName As String, ByVal strFieldName As String)
        _bolCanExecuteUseAddressOf = bolCanExecuteUseAddressOf
        _bolExecuteUseAddressOf = bolExecuteUseAddressOf
        _bolIncludeCanExecuteMethod = bolIncludeCanExecuteMethod
        _bolUseRelayCommand = bolUseRelayCommand
        _strCanExecuteMethodName = strCanExecuteMethodName
        _strCommandName = strCommandName
        _strCommandParameterType = strCommandParameterType
        _strExecuteMethodName = strExecuteMethodName
        _strFieldName = strFieldName
    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String
        Return Me.CommandName
    End Function

#End Region

End Class
