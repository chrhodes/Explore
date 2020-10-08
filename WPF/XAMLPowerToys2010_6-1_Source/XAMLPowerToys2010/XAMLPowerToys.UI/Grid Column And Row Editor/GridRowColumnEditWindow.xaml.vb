Imports System.Xml
'
Partial Public Class GridRowColumnEditWindow

#Region " Declarations "

    Private _objDocument As XmlDocument

    '
    Private Enum GridAction
        DeleteRow
        DeleteColumn
        InsertRowBefore
        InsertRowAfter
        InsertColumnBefore
        InsertColumnAfter
    End Enum

    Private Enum InsertLocation
        Before
        After
    End Enum

#End Region

#Region " Properties "

    Public Property Document() As XmlDocument
        Get
            Return _objDocument
        End Get
        Set(ByVal Value As XmlDocument)
            _objDocument = Value
        End Set
    End Property

#End Region

#Region " Constructors "

    Public Sub New(ByVal objDocument As XmlDocument)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        _objDocument = objDocument
    End Sub

#End Region

#Region " Load "

    Private Sub AddMissingGridElementsAttributes()

        For Each objXMLNode As XmlNode In (From n As Object In _objDocument.ChildNodes(0).ChildNodes() Where (CType(n, XmlNode).Name.StartsWith("Grid.") = False AndAlso CType(n, XmlNode).NodeType <> XmlNodeType.Whitespace AndAlso CType(n, XmlNode).NodeType <> XmlNodeType.Comment)).ToList

            Dim objAttribute As XmlAttribute = TryCast((From n As Object In objXMLNode.Attributes Where CType(n, XmlAttribute).Name = "Grid.Row").FirstOrDefault, XmlAttribute)

            If objAttribute Is Nothing Then

                Dim objNewGridRowAttribute As XmlAttribute = _objDocument.CreateAttribute("Grid.Row")
                objNewGridRowAttribute.Value = "0"
                objXMLNode.Attributes.Prepend(objNewGridRowAttribute)

            Else
                objXMLNode.Attributes.Prepend(objXMLNode.Attributes.Remove(objAttribute))
            End If

            objAttribute = TryCast((From n As Object In objXMLNode.Attributes Where CType(n, XmlAttribute).Name = "Grid.Column").FirstOrDefault, XmlAttribute)

            If objAttribute Is Nothing Then

                Dim objNewGridRowAttribute As XmlAttribute = _objDocument.CreateAttribute("Grid.Column")
                objNewGridRowAttribute.Value = "0"
                objXMLNode.Attributes.Prepend(objNewGridRowAttribute)

            Else
                objXMLNode.Attributes.Prepend(objXMLNode.Attributes.Remove(objAttribute))
            End If

        Next

        Dim objTest = From n As Object In _objDocument.ChildNodes(0).ChildNodes() Where CType(n, XmlNode).Name = "Grid.RowDefinitions"

        If objTest Is Nothing OrElse objTest.Count = 0 Then

            Dim objRowDefinitionElement As XmlElement = _objDocument.CreateElement("Grid.RowDefinitions")
            Dim objRowDefinitionAttribute As XmlAttribute = _objDocument.CreateAttribute("Height")
            objRowDefinitionAttribute.Value = "*"

            Dim objRowDefinition As XmlElement = _objDocument.CreateElement("RowDefinition")
            objRowDefinition.Attributes.Prepend(objRowDefinitionAttribute)
            objRowDefinitionElement.PrependChild(objRowDefinition)
            _objDocument.ChildNodes(0).PrependChild(objRowDefinitionElement)
        End If

        objTest = From n As Object In _objDocument.ChildNodes(0).ChildNodes() Where CType(n, XmlNode).Name = "Grid.ColumnDefinitions"

        If objTest Is Nothing OrElse objTest.Count = 0 Then

            Dim objColumnDefinitionElement As XmlElement = _objDocument.CreateElement("Grid.ColumnDefinitions")
            Dim objColumnDefinitionAttribute As XmlAttribute = _objDocument.CreateAttribute("Width")
            objColumnDefinitionAttribute.Value = "*"

            Dim objColumnDefinition As XmlElement = _objDocument.CreateElement("ColumnDefinition")
            objColumnDefinition.Attributes.Prepend(objColumnDefinitionAttribute)
            objColumnDefinitionElement.PrependChild(objColumnDefinition)
            _objDocument.ChildNodes(0).PrependChild(objColumnDefinitionElement)
        End If

    End Sub

    Private Sub Window1_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        AddMissingGridElementsAttributes()
        BuildGrid()
        EventManager.RegisterClassHandler(GetType(ContextMenu), MenuItem.ClickEvent, New RoutedEventHandler(AddressOf ContextMenuItem_Click))
    End Sub

#End Region

#Region " Methods "

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.DialogResult = False
    End Sub

    Private Sub btnCreate_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.DialogResult = True
    End Sub

    Private Sub BuildGrid()

        Dim cm As New ContextMenu
        Dim mi As New MenuItem With {.Header = "Column"}
        mi.Items.Add(New MenuItem With {.Header = "Insert Before", .Tag = GridAction.InsertColumnBefore})
        mi.Items.Add(New MenuItem With {.Header = "Insert After", .Tag = GridAction.InsertColumnAfter})
        mi.Items.Add(New MenuItem With {.Header = "Delete", .Tag = GridAction.DeleteColumn, .Foreground = New SolidColorBrush(Colors.Red)})
        cm.Items.Add(mi)
        mi = New MenuItem With {.Header = "Row"}
        mi.Items.Add(New MenuItem With {.Header = "Insert Above", .Tag = GridAction.InsertRowBefore})
        mi.Items.Add(New MenuItem With {.Header = "Insert Below", .Tag = GridAction.InsertRowAfter})
        mi.Items.Add(New MenuItem With {.Header = "Delete", .Tag = GridAction.DeleteRow, .Foreground = New SolidColorBrush(Colors.Red)})
        cm.Items.Add(mi)

        Dim objRowDefinitions As List(Of RowDefinition) = GetRowDefinitions()
        Dim objColumnDefinition As List(Of ColumnDefinition) = GetColumnDefinitions()
        Me.gridLayout.Children.Clear()
        Me.gridLayout.RowDefinitions.Clear()
        Me.gridLayout.ColumnDefinitions.Clear()
        Me.gridLayout.ShowGridLines = True

        For Each objRow As RowDefinition In objRowDefinitions
            Me.gridLayout.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(40)})
        Next

        For Each objColumn As ColumnDefinition In objColumnDefinition
            Me.gridLayout.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(80)})
        Next

        Dim intRow As Integer
        Dim intColumn As Integer

        For Each objRow As RowDefinition In objRowDefinitions
            intColumn = 0

            For Each objColumn As ColumnDefinition In objColumnDefinition

                Dim objRectangle As New Rectangle With {.Margin = New Thickness(5), .Stroke = New SolidColorBrush(Colors.Gray), .StrokeThickness = 1, .Fill = New SolidColorBrush(Colors.WhiteSmoke), .VerticalAlignment = Windows.VerticalAlignment.Stretch, .HorizontalAlignment = Windows.HorizontalAlignment.Stretch}

                If intRow Mod 2 <> 0 Then
                    objRectangle.Fill = New SolidColorBrush(Colors.AntiqueWhite)
                End If

                If objRow.Tag IsNot Nothing OrElse objColumn.Tag IsNot Nothing Then
                    objRectangle.Stroke = New SolidColorBrush(Colors.Blue)
                    objRectangle.StrokeThickness = 2
                End If

                objRectangle.SetValue(Grid.RowProperty, intRow)
                objRectangle.SetValue(Grid.ColumnProperty, intColumn)
                objRectangle.ContextMenu = cm
                Me.gridLayout.Children.Add(objRectangle)
                intColumn += 1
            Next

            intRow += 1
        Next

    End Sub

    Private Sub ContextMenuItem_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)

        Dim objRectangle As Rectangle = TryCast(TryCast(sender, ContextMenu).PlacementTarget, Rectangle)
        Dim intRectangleRow As Integer
        Dim intRectangleColumn As Integer

        If objRectangle IsNot Nothing Then
            intRectangleRow = CType(objRectangle.GetValue(Grid.RowProperty), Integer)
            intRectangleColumn = CType(objRectangle.GetValue(Grid.ColumnProperty), Integer)
        End If

        Select Case CType(CType(e.OriginalSource, MenuItem).Tag, GridAction)

            Case GridAction.DeleteColumn
                DeleteColumn(intRectangleColumn)

            Case GridAction.DeleteRow
                DeleteRow(intRectangleRow)

            Case GridAction.InsertColumnAfter
                InsertColumn(intRectangleColumn, InsertLocation.After)

            Case GridAction.InsertColumnBefore
                InsertColumn(intRectangleColumn, InsertLocation.Before)

            Case GridAction.InsertRowAfter
                InsertRow(intRectangleRow, InsertLocation.After)

            Case GridAction.InsertRowBefore
                InsertRow(intRectangleRow, InsertLocation.Before)

            Case Else
                Throw New ArgumentOutOfRangeException("Tag", CType(CType(e.OriginalSource, MenuItem).Tag, GridAction), "The GridAction passed in has not yet been programmed.")
        End Select

    End Sub

    Private Sub DeleteColumn(ByVal intColumn As Integer)

        Dim objColumnDefinitions As List(Of ColumnDefinition) = GetColumnDefinitions()

        If objColumnDefinitions Is Nothing OrElse objColumnDefinitions.Count <= 1 Then
            Exit Sub
        End If

        Dim objRemoveMe As XmlNode = Nothing
        Dim objRemoveMyWhiteSpace As XmlNode = Nothing
        Dim intX As Integer
        Dim objColumnDefinitionCollection As XmlNode = TryCast((From n As Object In _objDocument.ChildNodes(0).ChildNodes() Where CType(n, XmlNode).Name = "Grid.ColumnDefinitions").First, XmlNode)

        If Not objColumnDefinitionCollection Is Nothing Then

            For Each objColumn As XmlNode In objColumnDefinitionCollection.ChildNodes

                If objColumn.NodeType <> XmlNodeType.Whitespace AndAlso objColumn.NodeType <> XmlNodeType.Whitespace Then

                    If intX = intColumn Then
                        objRemoveMe = objColumn
                        Exit For
                    End If

                    intX += 1

                ElseIf objColumn.NodeType = XmlNodeType.Whitespace Then
                    objRemoveMyWhiteSpace = objColumn
                End If

            Next

            If objRemoveMe IsNot Nothing Then

                If objRemoveMyWhiteSpace IsNot Nothing Then
                    objColumnDefinitionCollection.RemoveChild(objRemoveMyWhiteSpace)
                End If

                objColumnDefinitionCollection.RemoveChild(objRemoveMe)
                IncrementColumnsOnOrAfter(intColumn, -1)
                BuildGrid()
            End If

        Else
            Exit Sub
        End If

    End Sub

    Private Sub DeleteRow(ByVal intRow As Integer)

        Dim objRowDefinitions As List(Of RowDefinition) = GetRowDefinitions()

        If objRowDefinitions Is Nothing OrElse objRowDefinitions.Count <= 1 Then
            Exit Sub
        End If

        Dim objRemoveMe As XmlNode = Nothing
        Dim objRemoveMyWhiteSpace As XmlNode = Nothing
        Dim intX As Integer
        Dim objRowDefinitionCollection As XmlNode = TryCast((From n As Object In _objDocument.ChildNodes(0).ChildNodes() Where CType(n, XmlNode).Name = "Grid.RowDefinitions").First, XmlNode)

        If Not objRowDefinitionCollection Is Nothing Then

            For Each objRow As XmlNode In objRowDefinitionCollection.ChildNodes

                If objRow.NodeType <> XmlNodeType.Whitespace AndAlso objRow.NodeType <> XmlNodeType.Comment Then

                    If intX = intRow Then
                        objRemoveMe = objRow
                        Exit For
                    End If

                    intX += 1

                ElseIf objRow.NodeType = XmlNodeType.Whitespace Then
                    objRemoveMyWhiteSpace = objRow
                End If

            Next

            If objRemoveMe IsNot Nothing Then

                If objRemoveMyWhiteSpace IsNot Nothing Then
                    objRowDefinitionCollection.RemoveChild(objRemoveMyWhiteSpace)
                End If

                objRowDefinitionCollection.RemoveChild(objRemoveMe)
                IncrementRowsOnOrAfter(intRow, -1)
                BuildGrid()
            End If

        Else
            Exit Sub
        End If

    End Sub

    Private Function GetColumnDefinitions() As List(Of ColumnDefinition)

        Dim obj As New List(Of ColumnDefinition)
        Dim objColumnDefinitionCollection As XmlNode = TryCast((From n As Object In _objDocument.ChildNodes(0).ChildNodes() Where CType(n, XmlNode).Name = "Grid.ColumnDefinitions").First, XmlNode)

        If Not objColumnDefinitionCollection Is Nothing Then

            For Each objColumn As XmlNode In objColumnDefinitionCollection.ChildNodes

                If objColumn.NodeType <> XmlNodeType.Whitespace AndAlso objColumn.NodeType <> XmlNodeType.Comment Then

                    Dim objNewColumnDefinition As ColumnDefinition = Nothing
                    Dim objAttribute As XmlAttribute = TryCast(objColumn.Attributes.GetNamedItem("Width"), XmlAttribute)

                    If Not objAttribute Is Nothing Then

                        If String.Compare(objAttribute.Value, "Auto", True) = 0 Then
                            objNewColumnDefinition = New ColumnDefinition With {.Width = New GridLength(0, GridUnitType.Auto)}

                        ElseIf objAttribute.Value.Contains("*") Then

                            Dim intStarWidth As Integer

                            If Not Integer.TryParse(objAttribute.Value.Replace("*", String.Empty), intStarWidth) Then
                                intStarWidth = 1
                            End If

                            objNewColumnDefinition = New ColumnDefinition With {.Width = New GridLength(intStarWidth, GridUnitType.Star)}

                        Else
                            objNewColumnDefinition = New ColumnDefinition With {.Width = New GridLength(CType(objAttribute.Value, Integer))}
                        End If

                    Else
                        objNewColumnDefinition = New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)}
                    End If

                    If objColumn.Attributes.GetNamedItem("Tag") IsNot Nothing Then
                        objNewColumnDefinition.Tag = "New"
                    End If

                    obj.Add(objNewColumnDefinition)
                End If

            Next

        Else
            obj.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})
        End If

        Return obj
    End Function

    Private Function GetRowDefinitions() As List(Of RowDefinition)

        Dim obj As New List(Of RowDefinition)
        Dim objRowDefinitionCollection As XmlNode = TryCast((From n As Object In _objDocument.ChildNodes(0).ChildNodes() Where CType(n, XmlNode).Name = "Grid.RowDefinitions").First, XmlNode)

        If Not objRowDefinitionCollection Is Nothing Then

            For Each objRow As XmlNode In objRowDefinitionCollection.ChildNodes

                If objRow.NodeType <> XmlNodeType.Whitespace AndAlso objRow.NodeType <> XmlNodeType.Comment Then

                    Dim objNewRowDefinition As RowDefinition = Nothing

                    If objRow.Attributes.GetNamedItem("Tag") IsNot Nothing Then
                    End If

                    Dim objAttribute As XmlAttribute = TryCast(objRow.Attributes.GetNamedItem("Height"), XmlAttribute) 'TryCast((From n In objRow.Attributes Where CType(n, XmlAttribute).Name = "Height").First, XmlAttribute)

                    If Not objAttribute Is Nothing Then

                        If String.Compare(objAttribute.Value, "Auto", True) = 0 Then
                            objNewRowDefinition = New RowDefinition With {.Height = New GridLength(0, GridUnitType.Auto)}

                        ElseIf objAttribute.Value.Contains("*") Then

                            Dim intStarHeight As Integer

                            If Not Integer.TryParse(objAttribute.Value.Replace("*", String.Empty), intStarHeight) Then
                                intStarHeight = 1
                            End If

                            objNewRowDefinition = New RowDefinition With {.Height = New GridLength(intStarHeight, GridUnitType.Star)}

                        Else
                            objNewRowDefinition = New RowDefinition With {.Height = New GridLength(CType(objAttribute.Value, Integer))}
                        End If

                    Else
                        objNewRowDefinition = New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)}
                    End If

                    If objRow.Attributes.GetNamedItem("Tag") IsNot Nothing Then
                        objNewRowDefinition.Tag = "New"
                    End If

                    obj.Add(objNewRowDefinition)
                End If

            Next

        Else
            obj.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})
        End If

        Return obj
    End Function

    Private Sub IncrementColumnsOnOrAfter(ByVal intColumn As Integer, ByVal intIncrementValue As Integer)

        For Each objXMLNode As XmlNode In (From n As Object In _objDocument.ChildNodes(0).ChildNodes() Where (CType(n, XmlNode).Name.StartsWith("Grid.") = False AndAlso CType(n, XmlNode).NodeType <> XmlNodeType.Whitespace AndAlso CType(n, XmlNode).NodeType <> XmlNodeType.Comment)).ToList

            Dim objAttribute As XmlAttribute = CType((From n As Object In objXMLNode.Attributes Where CType(n, XmlAttribute).Name = "Grid.Column").FirstOrDefault, XmlAttribute)
            Dim intGridColumn As Integer = Integer.Parse(objAttribute.Value)

            If intGridColumn >= intColumn Then
                objAttribute.Value = (IIf(intGridColumn + intIncrementValue > 0, intGridColumn + intIncrementValue, 0)).ToString
            End If

        Next

    End Sub

    Private Sub IncrementRowsOnOrAfter(ByVal intRow As Integer, ByVal intIncrementValue As Integer)

        For Each objXMLNode As XmlNode In (From n As Object In _objDocument.ChildNodes(0).ChildNodes() Where (CType(n, XmlNode).Name.StartsWith("Grid.") = False AndAlso CType(n, XmlNode).NodeType <> XmlNodeType.Whitespace AndAlso CType(n, XmlNode).NodeType <> XmlNodeType.Comment)).ToList

            Dim objAttribute As XmlAttribute = CType((From n As Object In objXMLNode.Attributes Where CType(n, XmlAttribute).Name = "Grid.Row").FirstOrDefault, XmlAttribute)
            Dim intGridRow As Integer = Integer.Parse(objAttribute.Value)

            If intGridRow >= intRow Then
                objAttribute.Value = (IIf(intGridRow + intIncrementValue > 0, intGridRow + intIncrementValue, 0)).ToString
            End If

        Next

    End Sub

    Private Sub InsertColumn(ByVal intSourceColumn As Integer, ByVal enumInsertLocation As InsertLocation)

        Dim objColumnDefinitionNodeCollection As XmlNode = TryCast((From n As Object In _objDocument.ChildNodes(0).ChildNodes() Where CType(n, XmlNode).Name = "Grid.ColumnDefinitions").First, XmlNode)
        Dim objColumnDefinitionWhiteSpaceNode As XmlNode = Nothing
        Dim objColumnDefinitionNodesWithoutWhiteSpaceCollection As New List(Of XmlNode)

        For Each objNode As XmlNode In objColumnDefinitionNodeCollection.ChildNodes

            If objNode.NodeType <> XmlNodeType.Whitespace AndAlso objNode.NodeType <> XmlNodeType.Comment Then
                objColumnDefinitionNodesWithoutWhiteSpaceCollection.Add(objNode)

            ElseIf objNode.NodeType = XmlNodeType.Whitespace AndAlso objColumnDefinitionWhiteSpaceNode Is Nothing Then
                objColumnDefinitionWhiteSpaceNode = objNode.CloneNode(True)
            End If

        Next

        Dim objColumnDefinitions As List(Of ColumnDefinition) = GetColumnDefinitions()
        Dim objNewColumnElement As XmlElement = CType(_objDocument.CreateNode(XmlNodeType.Element, "", "ColumnDefinition", String.Empty), XmlElement)
        objNewColumnElement.SetAttribute("Width", ParseGridDefinitionLength(objColumnDefinitions(intSourceColumn).Width))
        objNewColumnElement.SetAttribute("Tag", "New")

        If enumInsertLocation = InsertLocation.Before Then
            objColumnDefinitionNodeCollection.InsertBefore(objNewColumnElement, objColumnDefinitionNodesWithoutWhiteSpaceCollection(intSourceColumn))

            If objColumnDefinitionWhiteSpaceNode IsNot Nothing Then
                objColumnDefinitionNodeCollection.InsertBefore(objColumnDefinitionWhiteSpaceNode, objColumnDefinitionNodesWithoutWhiteSpaceCollection(intSourceColumn))
            End If

            IncrementColumnsOnOrAfter(intSourceColumn, 1)

        ElseIf enumInsertLocation = InsertLocation.After Then
            objColumnDefinitionNodeCollection.InsertAfter(objNewColumnElement, objColumnDefinitionNodesWithoutWhiteSpaceCollection(intSourceColumn))

            If objColumnDefinitionWhiteSpaceNode IsNot Nothing Then
                objColumnDefinitionNodeCollection.InsertAfter(objColumnDefinitionWhiteSpaceNode, objColumnDefinitionNodesWithoutWhiteSpaceCollection(intSourceColumn))
            End If

            IncrementColumnsOnOrAfter(intSourceColumn + 1, 1)

        Else
            Throw New ArgumentOutOfRangeException("enumInsertLocation", enumInsertLocation, "The value passed in was not programmed.")
        End If

        BuildGrid()
    End Sub

    Private Sub InsertRow(ByVal intSourceRow As Integer, ByVal enumInsertLocation As InsertLocation)

        Dim objRowDefinitionNodeCollection As XmlNode = TryCast((From n As Object In _objDocument.ChildNodes(0).ChildNodes() Where CType(n, XmlNode).Name = "Grid.RowDefinitions").First, XmlNode)
        Dim objRowDefinitionWhiteSpaceNode As XmlNode = Nothing
        Dim objRowDefinitionNodesWithoutWhiteSpaceCollection As New List(Of XmlNode)

        For Each objNode As XmlNode In objRowDefinitionNodeCollection.ChildNodes

            If objNode.NodeType <> XmlNodeType.Whitespace AndAlso objNode.NodeType <> XmlNodeType.Comment Then
                objRowDefinitionNodesWithoutWhiteSpaceCollection.Add(objNode)

            ElseIf objNode.NodeType = XmlNodeType.Whitespace AndAlso objRowDefinitionWhiteSpaceNode Is Nothing Then
                objRowDefinitionWhiteSpaceNode = objNode.CloneNode(True)
            End If

        Next

        Dim objRowDefinitions As List(Of RowDefinition) = GetRowDefinitions()
        Dim objNewRowElement As XmlElement = CType(_objDocument.CreateNode(XmlNodeType.Element, "", "RowDefinition", String.Empty), XmlElement)
        objNewRowElement.SetAttribute("Height", ParseGridDefinitionLength(objRowDefinitions(intSourceRow).Height))
        objNewRowElement.SetAttribute("Tag", "New")

        If enumInsertLocation = InsertLocation.Before Then
            objRowDefinitionNodeCollection.InsertBefore(objNewRowElement, objRowDefinitionNodesWithoutWhiteSpaceCollection(intSourceRow))

            If objRowDefinitionWhiteSpaceNode IsNot Nothing Then
                objRowDefinitionNodeCollection.InsertBefore(objRowDefinitionWhiteSpaceNode, objRowDefinitionNodesWithoutWhiteSpaceCollection(intSourceRow))
            End If

            IncrementRowsOnOrAfter(intSourceRow, 1)

        ElseIf enumInsertLocation = InsertLocation.After Then
            objRowDefinitionNodeCollection.InsertAfter(objNewRowElement, objRowDefinitionNodesWithoutWhiteSpaceCollection(intSourceRow))

            If objRowDefinitionWhiteSpaceNode IsNot Nothing Then
                objRowDefinitionNodeCollection.InsertAfter(objRowDefinitionWhiteSpaceNode, objRowDefinitionNodesWithoutWhiteSpaceCollection(intSourceRow))
            End If

            IncrementRowsOnOrAfter(intSourceRow + 1, 1)

        Else
            Throw New ArgumentOutOfRangeException("enumInsertLocation", enumInsertLocation, "The value passed in was not programmed.")
        End If

        BuildGrid()
    End Sub

    Private Function ParseGridDefinitionLength(ByVal obj As GridLength) As String

        If obj.IsAuto Then
            Return "Auto"

        ElseIf obj.IsStar Then

            If obj.Value <> 1 AndAlso obj.Value <> 0 Then
                Return String.Format("{0}*", obj.Value)

            Else
                Return "*"
            End If

        Else
            Return obj.Value.ToString
        End If

    End Function

#End Region

End Class
