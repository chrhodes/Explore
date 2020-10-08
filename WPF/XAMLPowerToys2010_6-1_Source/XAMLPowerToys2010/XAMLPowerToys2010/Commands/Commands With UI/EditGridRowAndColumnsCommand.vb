Imports System
Imports Microsoft.VisualStudio.CommandBars
Imports Extensibility
Imports EnvDTE
Imports EnvDTE80
Imports XAMLPowerToys.UI
Imports System.Xml
Imports System.IO
Imports System.Text

'
Public Class EditGridRowAndColumnsCommand
    Inherits CommandBase

#Region " Declarations "

    Private _objAddedNamespaces As New Generic.List(Of String)

#End Region

#Region " Constructors "

    Public Sub New(ByVal objAddInInstance As AddIn, ByVal objApplication As DTE2, ByVal intCounter As Integer)
        MyBase.new(objAddInInstance, objApplication, intCounter)
        MyBase.Caption = "Edit Grid Columns and Rows"
        MyBase.CommandName = "EditGridRowAndColumnsCommand" & intCounter.ToString
        MyBase.ToolTip = "Edit grid columns and rows."
    End Sub

#End Region

#Region " Methods "

    Private Sub AddNameSpaces(ByVal strXMLIn As String, ByVal objNameSpaceManager As XmlNamespaceManager)

        Dim ht As New Hashtable
        Dim intLastIndexFound As Integer = -1

        Do
            intLastIndexFound = strXMLIn.IndexOf(":", intLastIndexFound + 1)

            If intLastIndexFound > -1 Then

                For intX As Integer = intLastIndexFound To 0 Step -1

                    If strXMLIn.Substring(intX, 1) = " " OrElse strXMLIn.Substring(intX, 1) = "<" Then

                        Dim strNameSpace As String = strXMLIn.Substring(intX + 1, intLastIndexFound - intX - 1)

                        If Not ht.ContainsKey(strNameSpace) Then
                            ht.Add(strNameSpace, strNameSpace)
                            objNameSpaceManager.AddNamespace(strNameSpace, String.Format("urn:{0}", strNameSpace))
                            _objAddedNamespaces.Add(String.Format("xmlns:{0}=""urn:{0}""", strNameSpace))
                        End If

                        Continue Do
                    End If

                Next

                Exit Do

            Else
                Exit Do
            End If

        Loop

    End Sub

    Public Overrides Function CanExecute(ByVal enumExecuteOption As EnvDTE.vsCommandExecOption) As Boolean
        Return MyBase.CanExecute(enumExecuteOption) AndAlso IsTextSelected()
    End Function

    Public Overrides Sub Execute()

        Try

            If _objAddedNamespaces Is Nothing Then
                _objAddedNamespaces = New Generic.List(Of String)

            Else
                _objAddedNamespaces.Clear()
            End If

            Dim objSelectedCodeBlock As TextSelection = CType(Me.Application.ActiveDocument.Selection, TextSelection)
            Dim strXAML As String = objSelectedCodeBlock.Text.TrimStart(Chr(10), Chr(13), Chr(9), Chr(32)).TrimEnd(Chr(10), Chr(13), Chr(9), Chr(32))

            If Not strXAML.StartsWith("<Grid", StringComparison.InvariantCultureIgnoreCase) OrElse Not strXAML.EndsWith("</Grid>", StringComparison.InvariantCultureIgnoreCase) Then
                Utilities.ShowInformationMessage("You must select a grid", "Your selection must begin and end with Grid tags.")
                Exit Sub
            End If

            Dim objNameTable As New NameTable()
            Dim objNameSpaceManager As New XmlNamespaceManager(objNameTable)
            AddNameSpaces(strXAML, objNameSpaceManager)

            Dim objXmlParserContext As New XmlParserContext(Nothing, objNameSpaceManager, Nothing, XmlSpace.None)
            Dim objDocument As New System.Xml.XmlDocument
            objDocument.PreserveWhitespace = True
            objDocument.XmlResolver = Nothing

            Dim objXMLSettings As New Xml.XmlReaderSettings
            objXMLSettings.ValidationFlags = Xml.Schema.XmlSchemaValidationFlags.None
            objXMLSettings.ValidationType = Xml.ValidationType.None

            Dim reader As Xml.XmlReader = Xml.XmlReader.Create(New StringReader(strXAML), objXMLSettings, objXmlParserContext)
            objDocument.Load(reader)
            reader.Close()
            reader = Nothing

            Dim objGridRowColumnEditForm As New GridRowColumnEditWindow(objDocument)

            If objGridRowColumnEditForm.ShowDialog = True Then

                Dim sb As New StringBuilder(10240)
                Dim objWriterSettings As Xml.XmlWriterSettings = New Xml.XmlWriterSettings
                With objWriterSettings
                    .Indent = True
                    .NewLineOnAttributes = False
                    .OmitXmlDeclaration = True
                    .OmitXmlDeclaration = True
                End With

                Dim writer As Xml.XmlWriter = Xml.XmlWriter.Create(sb, objWriterSettings)
                objGridRowColumnEditForm.Document.Save(writer)

                For Each s As String In _objAddedNamespaces
                    sb.Replace(s, String.Empty)
                Next

                sb.Replace(" >", ">")
                sb.Replace(String.Format("Tag=""New"""), String.Empty)
                sb.Replace("    ", " ")
                sb.Replace("   ", " ")
                sb.Replace("  ", " ")

                Dim objEditPoint As EditPoint = objSelectedCodeBlock.TopPoint.CreateEditPoint()
                objSelectedCodeBlock.Delete()
                objEditPoint.Insert(sb.ToString)
            End If

        Catch ex As Exception
            Utilities.ShowExceptionMessage(Me.Caption, ex.Message, String.Empty, ex.ToString)
        End Try

    End Sub

    Public Overrides Function GetStatus() As EnvDTE.vsCommandStatus

        If IsTextSelected() Then
            Return CType(vsCommandStatus.vsCommandStatusEnabled + vsCommandStatus.vsCommandStatusSupported, vsCommandStatus)

        Else
            Return CType(vsCommandStatus.vsCommandStatusSupported, vsCommandStatus)
        End If

    End Function

#End Region

End Class
