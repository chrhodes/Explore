Imports System
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Windows
Imports System.Windows.Documents
Imports System.Windows.Media

Public Class InsertionAdorner
    Inherits Adorner

#Region " Declarations "

    Private _bolIsInFirstHalf As Boolean
    Private _bolIsSeparatorHorizontal As Boolean
    Private _objAdornerLayer As AdornerLayer
    Private Shared _objpen As Pen
    Private Shared _objTriangle As PathGeometry

#End Region

#Region " Properties "

    Public Property IsInFirstHalf() As Boolean
        Get
            Return _bolIsInFirstHalf
        End Get
        Set(ByVal value As Boolean)
            _bolIsInFirstHalf = value
        End Set
    End Property

    Private Shared ReadOnly Property Pen() As Pen
        Get

            If (_objpen Is Nothing) Then
                _objpen = New Pen
            End If

            Return _objpen
        End Get
    End Property

    Private Shared ReadOnly Property Triangle() As PathGeometry
        Get

            If _objTriangle Is Nothing Then
                _objTriangle = New PathGeometry
            End If

            Return _objTriangle
        End Get
    End Property

#End Region

#Region " Constructors "

    Public Sub New(ByVal bolIsSeparatorHorizontal As Boolean, ByVal isInFirstHalf As Boolean, ByVal adornedElement As UIElement, ByVal objAdornerLayer As AdornerLayer)
        MyBase.New(adornedElement)
        _bolIsSeparatorHorizontal = bolIsSeparatorHorizontal
        Me.IsInFirstHalf = isInFirstHalf
        _objAdornerLayer = objAdornerLayer
        MyBase.IsHitTestVisible = False
        _objAdornerLayer.Add(Me)
    End Sub

    Shared Sub New()
        _objpen = New Pen()
        _objpen.Brush = Brushes.Gray
        _objpen.Freeze()

        Dim firstLine As New LineSegment(New Point(0, -5), False)
        firstLine.Freeze()

        Dim secondLine As New LineSegment(New Point(0, 5), False)
        secondLine.Freeze()

        Dim figure As New PathFigure
        figure.StartPoint = New Point(5, 0)
        figure.Segments.Add(firstLine)
        figure.Segments.Add(secondLine)
        figure.Freeze()
        _objTriangle = New PathGeometry
        _objTriangle.Figures.Add(figure)
        _objTriangle.Freeze()
    End Sub

#End Region

#Region " Methods "

    Public Sub Detach()
        _objAdornerLayer.Remove(Me)
    End Sub

    Protected Overrides Sub OnRender(ByVal drawingContext As DrawingContext)

        Dim startPoint As Point
        Dim endPoint As Point
        Me.CalculateStartAndEndPoint(startPoint, endPoint)
        drawingContext.DrawLine(InsertionAdorner.Pen, startPoint, endPoint)

        If _bolIsSeparatorHorizontal Then
            Me.DrawTriangle(drawingContext, startPoint, 0)
            Me.DrawTriangle(drawingContext, endPoint, 180)

        Else
            Me.DrawTriangle(drawingContext, startPoint, 90)
            Me.DrawTriangle(drawingContext, endPoint, -90)
        End If

    End Sub

    Private Sub CalculateStartAndEndPoint(<Out()> ByRef startPoint As Point, <Out()> ByRef endPoint As Point)
        startPoint = New Point
        endPoint = New Point

        Dim dblWidth As Double = MyBase.AdornedElement.RenderSize.Width
        Dim dblHeight As Double = MyBase.AdornedElement.RenderSize.Height

        If _bolIsSeparatorHorizontal Then
            endPoint.X = dblWidth

            If Not Me.IsInFirstHalf Then
                startPoint.Y = dblHeight
                endPoint.Y = dblHeight
            End If

        Else
            endPoint.Y = dblHeight

            If Not Me.IsInFirstHalf Then
                startPoint.X = dblWidth
                endPoint.X = dblWidth
            End If

        End If

    End Sub

    Private Sub DrawTriangle(ByVal drawingContext As DrawingContext, ByVal origin As Point, ByVal angle As Double)
        drawingContext.PushTransform(New TranslateTransform(origin.X, origin.Y))
        drawingContext.PushTransform(New RotateTransform(angle))
        drawingContext.DrawGeometry(InsertionAdorner.Pen.Brush, Nothing, Triangle)
        drawingContext.Pop()
        drawingContext.Pop()
    End Sub

#End Region

End Class
