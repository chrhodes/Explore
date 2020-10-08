
Public Class DraggedAdorner
    Inherits Adorner

#Region " Declarations "

    Private _dblLeft As Double
    Private _dblTop As Double
    Private _objAdornerLayer As AdornerLayer
    Private _objContentPresenter As ContentPresenter

#End Region

#Region " Properties "

    Protected Overrides ReadOnly Property VisualChildrenCount() As Integer
        Get
            Return 1
        End Get
    End Property

#End Region

#Region " Constructors "

    Public Sub New(ByVal dragDropData As Object, ByVal dragDropTemplate As DataTemplate, ByVal adornedElement As UIElement, ByVal adornerLayer As AdornerLayer)
        MyBase.New(adornedElement)
        _objAdornerLayer = adornerLayer
        _objContentPresenter = New ContentPresenter
        With _objContentPresenter
            .Content = dragDropData
            .ContentTemplate = dragDropTemplate
            .Opacity = 0.7
        End With
        _objAdornerLayer.Add(Me)
    End Sub

#End Region

#Region " Methods "

    Public Sub Detach()
        _objAdornerLayer.Remove(Me)
    End Sub

    Public Overrides Function GetDesiredTransform(ByVal transform As GeneralTransform) As GeneralTransform

        Dim result As New GeneralTransformGroup
        result.Children.Add(MyBase.GetDesiredTransform(transform))
        result.Children.Add(New TranslateTransform(_dblLeft, _dblTop))
        Return result
    End Function

    Public Sub SetPosition(ByVal left As Double, ByVal top As Double)
        _dblLeft = left
        _dblTop = top

        If _objAdornerLayer IsNot Nothing Then
            Try
                _objAdornerLayer.Update(MyBase.AdornedElement)
            Catch ex As InvalidOperationException
                'ignore - this happens when working over a Terminal Session.
            End Try

        End If

    End Sub

    Protected Overrides Function ArrangeOverride(ByVal sizFinalSize As Size) As Size
        _objContentPresenter.Arrange(New Rect(sizFinalSize))
        Return sizFinalSize
    End Function

    Protected Overrides Function GetVisualChild(ByVal index As Integer) As Visual
        Return _objContentPresenter
    End Function

    Protected Overrides Function MeasureOverride(ByVal constraint As Size) As Size
        _objContentPresenter.Measure(constraint)
        Return _objContentPresenter.DesiredSize
    End Function

#End Region

End Class
