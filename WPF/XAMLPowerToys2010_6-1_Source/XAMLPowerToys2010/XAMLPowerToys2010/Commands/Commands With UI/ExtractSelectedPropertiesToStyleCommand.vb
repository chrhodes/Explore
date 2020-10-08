Imports System
Imports Microsoft.VisualStudio.CommandBars
Imports Extensibility
Imports EnvDTE
Imports EnvDTE80
Imports XAMLPowerToys.UI
Imports System.Xml
Imports System.IO
Imports System.Text
Imports XAMLPowerToys.Model
Imports XAMLPowerToys.Helpers

'
Public Class ExtractSelectedPropertiesToStyleCommand
    Inherits CommandBase

#Region " Declarations "

    Private _objAddedNamespaces As New Generic.List(Of String)

#End Region

#Region " Constructors "

    Public Sub New(ByVal objAddInInstance As AddIn, ByVal objApplication As DTE2, ByVal intCounter As Integer)
        MyBase.new(objAddInInstance, objApplication, intCounter)
        MyBase.Caption = "Extract Properties To Style"
        MyBase.CommandName = "ExtractSelectedPropertiesToStyleCommand" & intCounter.ToString
        MyBase.ToolTip = "Extract selected properties to style."
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

            Dim bolIsSilverlight As Boolean = IsProjectSilverlight(Helpers.GetProjectTypeGuids(Me.Application.SelectedItems.Item(1).ProjectItem.ContainingProject).Split(";"c))
            Dim strSilverlightVersion As String = String.Empty
            If bolIsSilverlight Then
                strSilverlightVersion = Me.Application.ActiveDocument.ProjectItem.ContainingProject.Properties.Item("TargetFrameworkMoniker").Value.ToString.Replace("Silverlight,Version=v", String.Empty)
            End If
            Dim objExtractSelectedPropertiesToStyle As New ExtractSelectedPropertiesToStyleWindow(objDocument, bolIsSilverlight, strSilverlightVersion)
            Dim bolResult As Nullable(Of Boolean) = objExtractSelectedPropertiesToStyle.ShowDialog

            If bolResult.HasValue AndAlso bolResult.Value = True Then

                Dim sb As New StringBuilder(10240)
                Dim objWriterSettings As Xml.XmlWriterSettings = New Xml.XmlWriterSettings
                With objWriterSettings
                    .Indent = True
                    .NewLineOnAttributes = False
                    .OmitXmlDeclaration = True
                    .OmitXmlDeclaration = True
                End With

                Dim writer As Xml.XmlWriter = Xml.XmlWriter.Create(sb, objWriterSettings)
                objExtractSelectedPropertiesToStyle.Document.Save(writer)

                For Each s As String In _objAddedNamespaces
                    sb.Replace(s, String.Empty)
                Next

                sb.Replace(" >", ">")
                sb.Replace("    ", " ")
                sb.Replace("   ", " ")
                sb.Replace("  ", " ")

                Dim objEditPoint As EditPoint = objSelectedCodeBlock.TopPoint.CreateEditPoint()
                objSelectedCodeBlock.Delete()
                objEditPoint.Insert(sb.ToString)
                sb.Length = 0

                If bolIsSilverlight Then
                    sb.AppendFormat("<Style TargetType=|{0}|", objExtractSelectedPropertiesToStyle.TypeName)

                Else
                    sb.AppendFormat("<Style TargetType=|{{x:Type {0}}}|", objExtractSelectedPropertiesToStyle.TypeName)
                End If

                If Not String.IsNullOrEmpty(objExtractSelectedPropertiesToStyle.StyleName) Then
                    sb.AppendFormat(" x:Key=|{0}|>", objExtractSelectedPropertiesToStyle.StyleName)

                Else
                    sb.Append(">")
                End If

                sb.Append(vbCrLf)

                For Each obj As ExtractProperty In objExtractSelectedPropertiesToStyle.ExtractedProperties

                    If obj.IsSelected Then
                        sb.AppendFormat("<Setter Property=|{0}| Value=|{1}| />{2}", obj.PropertyName, obj.PropertyValue, vbCrLf)
                    End If

                Next

                sb.AppendLine("</Style>")
                My.Computer.Clipboard.Clear()
                My.Computer.Clipboard.SetText(sb.Replace("|", Chr(34)).ToString)
                Utilities.ShowInformationMessage("Paste Style", "Place insertion point and paste created style into the resource section of a XAML document.")
            End If

        Catch ex As XmlException
            Utilities.ShowExceptionMessage(Me.Caption, "Exception parsing selected XAML.  Please ensure you have selected the begin and end tag of the desired UI Element and that the XAML is valid.", String.Empty, ex.ToString)

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
