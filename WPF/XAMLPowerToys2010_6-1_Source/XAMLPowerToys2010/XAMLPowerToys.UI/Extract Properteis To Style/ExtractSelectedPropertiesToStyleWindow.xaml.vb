Imports System.Xml
Imports XAMLPowerToys.Model
Partial Public Class ExtractSelectedPropertiesToStyleWindow

#Region " Declarations "

    Private _strSilverlightVersion As String = String.Empty
    Private _bolHasStyleSet As Boolean = False
    Private _cmdExtractCommand As ICommand
    Private _objDocument As XmlDocument
    Private _objExtractedProperties As New List(Of ExtractProperty)
    Private _strStyleName As String = String.Empty
    Private _strTypeName As String = String.Empty
    Private _bolIsSilverlight As Boolean = False

#End Region

#Region " Command Properties "

    Public ReadOnly Property ExtractCommand() As ICommand
        Get

            If _cmdExtractCommand Is Nothing Then
                _cmdExtractCommand = New RelayCommand(AddressOf ExtractExecute, AddressOf CanExtractExecute)
            End If

            Return _cmdExtractCommand
        End Get
    End Property

#End Region

#Region " Properties "

    Public ReadOnly Property SilverlightVersion As String
        Get
            Return _strSilverlightVersion
        End Get
    End Property

    Public ReadOnly Property IsSilverlight() As Boolean
        Get
            Return _bolIsSilverlight
        End Get
    End Property

    Public Property Document() As XmlDocument
        Get
            Return _objDocument
        End Get

        Private Set(ByVal Value As XmlDocument)
            _objDocument = Value
        End Set
    End Property

    Public Property ExtractedProperties() As List(Of ExtractProperty)
        Get
            Return _objExtractedProperties
        End Get

        Private Set(ByVal Value As List(Of ExtractProperty))
            _objExtractedProperties = Value
        End Set
    End Property

    Public Property StyleName() As String
        Get
            Return _strStyleName
        End Get
        Set(ByVal Value As String)
            _strStyleName = Value
        End Set
    End Property

    Public ReadOnly Property TypeName() As String
        Get
            Return _strTypeName
        End Get
    End Property

#End Region

#Region " Constructor "

    Public Sub New(ByVal objDocument As XmlDocument, ByVal bolIsSilverlight As Boolean, ByVal strSilverlightVersion As String)
        _bolIsSilverlight = bolIsSilverlight
        _strSilverlightVersion = strSilverlightVersion
        _objDocument = objDocument
        GetProperties()
        Me.DataContext = Me
        InitializeComponent()

        If bolIsSilverlight AndAlso strSilverlightVersion.StartsWith("3") Then
            Me.tbIsSilverlight3RequiredAstrick.Visibility = Visibility.Visible
            Me.tbIsSilverlight3RequiredMessage.Visibility = Visibility.Visible
        End If
    End Sub

#End Region

#Region " Command Methods "

    Private Function CanExtractExecute(ByVal param As Object) As Boolean

        If IsSilverlight AndAlso SilverlightVersion = "3.0" AndAlso String.IsNullOrEmpty(Me.StyleName.Trim) Then
            Return False
        End If

        Return (From x In Me.ExtractedProperties Where x.IsSelected = True).Count > 0
    End Function

    Private Sub ExtractExecute(ByVal param As Object)

        If CanExtractExecute(param) Then
            '
            RemoveSelectedProperties()
            '
            InsertStyleProperty()
            '
            Me.DialogResult = True
        End If

    End Sub

#End Region

#Region " Methods "

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.DialogResult = False
    End Sub

    Private Sub GetProperties()
        _strTypeName = _objDocument.ChildNodes(0).Name

        For Each atr As XmlAttribute In _objDocument.ChildNodes(0).Attributes

            If atr.Name.Contains(".") OrElse
                atr.Name = "Name" OrElse
                atr.Name = "x:Name" OrElse
                atr.Name.StartsWith("xmlns") OrElse
                atr.Name.StartsWith("Command") OrElse
                atr.Name.StartsWith("Click") Then

                'TODO - developers you can add more checks here as desired
            ElseIf atr.Name <> "Style" Then
                Me.ExtractedProperties.Add(New ExtractProperty(atr))

            Else
                _bolHasStyleSet = True
                Utilities.ShowInformationMessage("Root Element Has Style", "The selected root UI Element already has a style property.  The style will not be set when Extract is executed.")
            End If

        Next

    End Sub

    Private Sub InsertStyleProperty()

        If Not String.IsNullOrEmpty(Me.StyleName) AndAlso Not _bolHasStyleSet Then

            Dim objNewAttribute As XmlAttribute = _objDocument.CreateAttribute("Style")
            objNewAttribute.Value = String.Format("{{StaticResource {0}}}", Me.StyleName)
            _objDocument.ChildNodes(0).Attributes.Append(objNewAttribute)
        End If

    End Sub

    Private Sub RemoveSelectedProperties()

        For Each obj As ExtractProperty In Me.ExtractedProperties

            If obj.IsSelected Then
                _objDocument.ChildNodes(0).Attributes.Remove(obj.XmlAttribute)
            End If

        Next

    End Sub

#End Region

End Class
