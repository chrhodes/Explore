Imports System.Xml
'
<Serializable()>
Public Class ExtractProperty

#Region " Declarations "

    Private _bolIsSelected As Boolean = False
    Private _objXmlAttribute As XmlAttribute = Nothing

#End Region

#Region " Properties "

    Public ReadOnly Property XmlAttribute() As XmlAttribute
        Get
            Return _objXmlAttribute
        End Get
    End Property

    Public ReadOnly Property PropertyName() As String
        Get
            Return _objXmlAttribute.Name
        End Get
    End Property

    Public ReadOnly Property PropertyValue() As String
        Get
            Return _objXmlAttribute.Value
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

#End Region

#Region " Constructor "

    Public Sub New()
    End Sub

    Public Sub New(ByVal objXmlAttribute As XmlAttribute)
        _objXmlAttribute = objXmlAttribute
    End Sub

#End Region

End Class
