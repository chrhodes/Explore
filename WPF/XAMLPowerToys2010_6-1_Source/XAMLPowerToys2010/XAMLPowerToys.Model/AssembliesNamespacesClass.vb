Imports System.ComponentModel
Imports XAMLPowerToys.Model
'
<Serializable()> Public Class AssembliesNamespacesClass

#Region " Declarations "

    Private _bolIsSelected As Boolean = False
    Private _strNamespace As String = String.Empty
    Private _strTypeName As String = String.Empty
    Private _strAssemblyName As String = String.Empty
    Private _objClassEntity As ClassEntity

#End Region

#Region " Properties "

    Public Property ClassEntity() As ClassEntity
        Get
            Return _objClassEntity
        End Get
        Set(ByVal value As ClassEntity)
            _objClassEntity = value
        End Set
    End Property

    Public ReadOnly Property AssemblyName() As String
        Get
            Return _strAssemblyName
        End Get
    End Property

    Public Property IsSelected() As Boolean
        Get
            Return _bolIsSelected
        End Get
        Set(ByVal Value As Boolean)
            _bolIsSelected = Value
        End Set
    End Property

    Public ReadOnly Property [Namespace]() As String
        Get
            Return _strNamespace
        End Get
    End Property

    Public ReadOnly Property TypeName() As String
        Get
            Return _strTypeName
        End Get
    End Property

#End Region

#Region " Constructors "

    Public Sub New(ByVal strAssemblyName As String, ByVal strNamespace As String, ByVal strTypeName As String, ByVal objClassEntity As ClassEntity)
        _strAssemblyName = strAssemblyName
        _strNamespace = strNamespace
        _strTypeName = strTypeName
        _objClassEntity = objClassEntity
    End Sub

    Public Sub New()

    End Sub
#End Region

End Class
