Imports XAMLPowerToys.Model

Public Class DragDropHelper

#Region " Declarations "

    Private _bolHasVerticalOrientation As Boolean
    Private _bolIsInFirstHalf As Boolean
    Private _intInsertionIndex As Integer
    Private _objDraggedAdorner As DraggedAdorner
    Private _objDraggedData As Object
    Private _objFormat As DataFormat = DataFormats.GetDataFormat("DragDropItemsControl")
    Private _objInsertionAdorner As InsertionAdorner
    Private Shared _objInstance As DragDropHelper
    Private _objSourceItemContainer As FrameworkElement
    Private _objSourceItemsControl As ItemsControl
    Private _objTargetItemContainer As FrameworkElement
    Private _objTargetItemsControl As ItemsControl
    Private _objTopWindow As Window
    Private _ptInitialMousePosition As Point

#End Region

#Region " Properties "

    Private Shared ReadOnly Property Instance() As DragDropHelper
        Get

            If _objInstance Is Nothing Then
                _objInstance = New DragDropHelper
            End If

            Return _objInstance
        End Get
    End Property

#End Region

#Region " Dependency Properties "

    Public Shared ReadOnly DragDropTemplateProperty As DependencyProperty = DependencyProperty.RegisterAttached("DragDropTemplate", GetType(DataTemplate), GetType(DragDropHelper), New UIPropertyMetadata(Nothing))
    Public Shared ReadOnly IsDragSourceProperty As DependencyProperty = DependencyProperty.RegisterAttached("IsDragSource", GetType(Boolean), GetType(DragDropHelper), New UIPropertyMetadata(False, New PropertyChangedCallback(AddressOf DragDropHelper.IsDragSourceChanged)))
    Public Shared ReadOnly IsDropTargetProperty As DependencyProperty = DependencyProperty.RegisterAttached("IsDropTarget", GetType(Boolean), GetType(DragDropHelper), New UIPropertyMetadata(False, New PropertyChangedCallback(AddressOf DragDropHelper.IsDropTargetChanged)))

#End Region

#Region " Methods "

    Public Shared Function GetDragDropTemplate(ByVal obj As DependencyObject) As DataTemplate
        Return DirectCast(obj.GetValue(DragDropHelper.DragDropTemplateProperty), DataTemplate)
    End Function

    Public Shared Function GetIsDragSource(ByVal obj As DependencyObject) As Boolean
        Return CBool(obj.GetValue(DragDropHelper.IsDragSourceProperty))
    End Function

    Public Shared Function GetIsDropTarget(ByVal obj As DependencyObject) As Boolean
        Return CBool(obj.GetValue(DragDropHelper.IsDropTargetProperty))
    End Function

    Public Shared Sub SetDragDropTemplate(ByVal obj As DependencyObject, ByVal value As DataTemplate)
        obj.SetValue(DragDropHelper.DragDropTemplateProperty, value)
    End Sub

    Public Shared Sub SetIsDragSource(ByVal obj As DependencyObject, ByVal value As Boolean)
        obj.SetValue(DragDropHelper.IsDragSourceProperty, value)
    End Sub

    Public Shared Sub SetIsDropTarget(ByVal obj As DependencyObject, ByVal value As Boolean)
        obj.SetValue(DragDropHelper.IsDropTargetProperty, value)
    End Sub

    Private Sub CreateInsertionAdorner()

        If _objTargetItemContainer IsNot Nothing Then
            _objInsertionAdorner = New InsertionAdorner(_bolHasVerticalOrientation, _bolIsInFirstHalf, _objTargetItemContainer, AdornerLayer.GetAdornerLayer(_objTargetItemContainer))
        End If

    End Sub

    Private Sub DecideDropTarget(ByVal e As DragEventArgs)

        Dim intTargetItemsControlCount As Integer = _objTargetItemsControl.Items.Count
        Dim objDraggedItem As Object = e.Data.GetData(_objFormat.Name)

        If Me.IsDropDataTypeAllowed(objDraggedItem) Then

            If intTargetItemsControlCount > 0 Then
                _bolHasVerticalOrientation = DynamicFormUtilities.HasVerticalOrientation(TryCast(_objTargetItemsControl.ItemContainerGenerator.ContainerFromIndex(0), FrameworkElement))
                _objTargetItemContainer = DynamicFormUtilities.GetItemContainer(_objTargetItemsControl, TryCast(e.OriginalSource, Visual))

                If _objTargetItemContainer IsNot Nothing Then
                    _bolIsInFirstHalf = DynamicFormUtilities.IsInFirstHalf(_objTargetItemContainer, e.GetPosition(_objTargetItemContainer), _bolHasVerticalOrientation)
                    _intInsertionIndex = _objTargetItemsControl.ItemContainerGenerator.IndexFromContainer(_objTargetItemContainer)

                    If Not _bolIsInFirstHalf Then
                        _intInsertionIndex += 1
                    End If

                Else
                    _objTargetItemContainer = TryCast(_objTargetItemsControl.ItemContainerGenerator.ContainerFromIndex((intTargetItemsControlCount - 1)), FrameworkElement)
                    _bolIsInFirstHalf = False
                    _intInsertionIndex = intTargetItemsControlCount
                End If

            Else
                _objTargetItemContainer = Nothing
                _intInsertionIndex = 0
            End If

        Else
            _objTargetItemContainer = Nothing
            _intInsertionIndex = -1
            e.Effects = DragDropEffects.None
        End If

    End Sub

    Private Sub DragSource_PreviewMouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)

        Dim objFE As FrameworkElement = DynamicFormUtilities.GetItemContainer(DirectCast(sender, ItemsControl), TryCast(e.OriginalSource, Visual))

        If objFE IsNot Nothing AndAlso TypeOf objFE.DataContext Is PropertyInformation AndAlso CType(objFE.DataContext, PropertyInformation).HasBeenUsed Then
            e.Handled = True
            Exit Sub
        End If

        _ptInitialMousePosition = e.GetPosition(_objTopWindow)
        _objSourceItemsControl = DirectCast(sender, ItemsControl)
        _objTopWindow = TryCast(DynamicFormUtilities.FindAncestor(GetType(Window), _objSourceItemsControl), Window)
        _objSourceItemContainer = DynamicFormUtilities.GetItemContainer(_objSourceItemsControl, TryCast(e.OriginalSource, Visual))

        If _objSourceItemContainer IsNot Nothing Then
            _objDraggedData = _objSourceItemContainer.DataContext
        End If

    End Sub

    Private Sub DragSource_PreviewMouseLeftButtonUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        _objDraggedData = Nothing
    End Sub

    Private Sub DragSource_PreviewMouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)

        If _objDraggedData IsNot Nothing AndAlso DynamicFormUtilities.IsMovementBigEnough(_ptInitialMousePosition, e.GetPosition(_objTopWindow)) Then

            Dim objData As New DataObject(_objFormat.Name, _objDraggedData)
            Dim bolPreviousAllowDrop As Boolean = _objTopWindow.AllowDrop
            _objTopWindow.AllowDrop = True
            AddHandler _objTopWindow.DragEnter, New DragEventHandler(AddressOf Me.TopWindow_DragEnter)
            AddHandler _objTopWindow.DragOver, New DragEventHandler(AddressOf Me.TopWindow_DragOver)
            AddHandler _objTopWindow.DragLeave, New DragEventHandler(AddressOf Me.TopWindow_DragLeave)

            Dim effects As DragDropEffects = DragDrop.DoDragDrop(DirectCast(sender, DependencyObject), objData, DragDropEffects.Move)
            Me.RemoveDraggedAdorner()
            _objTopWindow.AllowDrop = bolPreviousAllowDrop
            RemoveHandler _objTopWindow.DragEnter, New DragEventHandler(AddressOf Me.TopWindow_DragEnter)
            RemoveHandler _objTopWindow.DragOver, New DragEventHandler(AddressOf Me.TopWindow_DragOver)
            RemoveHandler _objTopWindow.DragLeave, New DragEventHandler(AddressOf Me.TopWindow_DragLeave)
            _objDraggedData = Nothing
        End If

    End Sub

    Private Sub DropTarget_PreviewDragEnter(ByVal sender As Object, ByVal e As DragEventArgs)
        _objTargetItemsControl = DirectCast(sender, ItemsControl)
        Me.DecideDropTarget(e)

        If e.Data.GetData(_objFormat.Name) IsNot Nothing Then
            Me.ShowDraggedAdorner(e.GetPosition(_objTopWindow))
            Me.CreateInsertionAdorner()
        End If

        e.Handled = True
    End Sub

    Private Sub DropTarget_PreviewDragLeave(ByVal sender As Object, ByVal e As DragEventArgs)

        If e.Data.GetData(_objFormat.Name) IsNot Nothing Then
            Me.RemoveInsertionAdorner()
        End If

        e.Handled = True
    End Sub

    Private Sub DropTarget_PreviewDragOver(ByVal sender As Object, ByVal e As DragEventArgs)
        Me.DecideDropTarget(e)

        If e.Data.GetData(_objFormat.Name) IsNot Nothing Then
            Me.ShowDraggedAdorner(e.GetPosition(_objTopWindow))
            Me.UpdateInsertionAdornerPosition()
        End If

        e.Handled = True
    End Sub

    Private Sub DropTarget_PreviewDrop(ByVal sender As Object, ByVal e As DragEventArgs)

        Dim objDraggedItem As Object = e.Data.GetData(_objFormat.Name)
        Dim intIndexRemoved As Integer = -1

        If objDraggedItem IsNot Nothing Then

            If (e.Effects And DragDropEffects.Move) <> DragDropEffects.None Then
                intIndexRemoved = DynamicFormUtilities.RemoveItemFromItemsControl(_objSourceItemsControl, objDraggedItem)
            End If

            If (((intIndexRemoved <> -1) AndAlso (_objSourceItemsControl Is _objTargetItemsControl)) AndAlso (intIndexRemoved < _intInsertionIndex)) Then
                _intInsertionIndex -= 1
            End If

            DynamicFormUtilities.InsertItemInItemsControl(_objTargetItemsControl, objDraggedItem, _intInsertionIndex)
            Me.RemoveDraggedAdorner()
            Me.RemoveInsertionAdorner()
        End If

        e.Handled = True
    End Sub

    Private Shared Sub IsDragSourceChanged(ByVal obj As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)

        Dim objDragSource As ItemsControl = TryCast(obj, ItemsControl)

        If objDragSource IsNot Nothing Then

            If Object.Equals(e.NewValue, True) Then
                AddHandler objDragSource.PreviewMouseLeftButtonDown, New MouseButtonEventHandler(AddressOf DragDropHelper.Instance.DragSource_PreviewMouseLeftButtonDown)
                AddHandler objDragSource.PreviewMouseLeftButtonUp, New MouseButtonEventHandler(AddressOf DragDropHelper.Instance.DragSource_PreviewMouseLeftButtonUp)
                AddHandler objDragSource.PreviewMouseMove, New MouseEventHandler(AddressOf DragDropHelper.Instance.DragSource_PreviewMouseMove)

            Else
                RemoveHandler objDragSource.PreviewMouseLeftButtonDown, New MouseButtonEventHandler(AddressOf DragDropHelper.Instance.DragSource_PreviewMouseLeftButtonDown)
                RemoveHandler objDragSource.PreviewMouseLeftButtonUp, New MouseButtonEventHandler(AddressOf DragDropHelper.Instance.DragSource_PreviewMouseLeftButtonUp)
                RemoveHandler objDragSource.PreviewMouseMove, New MouseEventHandler(AddressOf DragDropHelper.Instance.DragSource_PreviewMouseMove)
            End If

        End If

    End Sub

    Private Function IsDropDataTypeAllowed(ByVal objDraggedItem As Object) As Boolean

        If _objSourceItemsControl Is _objTargetItemsControl Then

            If TypeOf _objSourceItemsControl.ItemsSource Is ListCollectionView Then
                Return False

            ElseIf TypeOf objDraggedItem Is PropertyInformation Then
                'this prevents the left ListBox from dropping items on itself.
                Return False
            End If

        End If

        If TypeOf objDraggedItem Is DynamicFormEditor Then
            Return True

        ElseIf TypeOf objDraggedItem Is PropertyInformation Then
            Return True

        Else
            Return False
        End If

    End Function

    Private Shared Sub IsDropTargetChanged(ByVal obj As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)

        Dim objDropTarget As ItemsControl = TryCast(obj, ItemsControl)

        If objDropTarget IsNot Nothing Then

            If Object.Equals(e.NewValue, True) Then
                objDropTarget.AllowDrop = True
                AddHandler objDropTarget.PreviewDrop, New DragEventHandler(AddressOf DragDropHelper.Instance.DropTarget_PreviewDrop)
                AddHandler objDropTarget.PreviewDragEnter, New DragEventHandler(AddressOf DragDropHelper.Instance.DropTarget_PreviewDragEnter)
                AddHandler objDropTarget.PreviewDragOver, New DragEventHandler(AddressOf DragDropHelper.Instance.DropTarget_PreviewDragOver)
                AddHandler objDropTarget.PreviewDragLeave, New DragEventHandler(AddressOf DragDropHelper.Instance.DropTarget_PreviewDragLeave)

            Else
                objDropTarget.AllowDrop = False
                RemoveHandler objDropTarget.PreviewDrop, New DragEventHandler(AddressOf DragDropHelper.Instance.DropTarget_PreviewDrop)
                RemoveHandler objDropTarget.PreviewDragEnter, New DragEventHandler(AddressOf DragDropHelper.Instance.DropTarget_PreviewDragEnter)
                RemoveHandler objDropTarget.PreviewDragOver, New DragEventHandler(AddressOf DragDropHelper.Instance.DropTarget_PreviewDragOver)
                RemoveHandler objDropTarget.PreviewDragLeave, New DragEventHandler(AddressOf DragDropHelper.Instance.DropTarget_PreviewDragLeave)
            End If

        End If

    End Sub

    Private Sub RemoveDraggedAdorner()

        If _objDraggedAdorner IsNot Nothing Then
            _objDraggedAdorner.Detach()
            _objDraggedAdorner = Nothing
        End If

    End Sub

    Private Sub RemoveInsertionAdorner()

        If _objInsertionAdorner IsNot Nothing Then
            _objInsertionAdorner.Detach()
            _objInsertionAdorner = Nothing
        End If

    End Sub

    Private Sub ShowDraggedAdorner(ByVal currentPosition As Point)

        If _objDraggedAdorner Is Nothing Then
            _objDraggedAdorner = New DraggedAdorner(_objDraggedData, DragDropHelper.GetDragDropTemplate(_objSourceItemsControl), _objSourceItemContainer, AdornerLayer.GetAdornerLayer(_objSourceItemsControl))
        End If

        _objDraggedAdorner.SetPosition((currentPosition.X - _ptInitialMousePosition.X), (currentPosition.Y - _ptInitialMousePosition.Y))
    End Sub

    Private Sub TopWindow_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs)
        Me.ShowDraggedAdorner(e.GetPosition(_objTopWindow))
        e.Effects = DragDropEffects.None
        e.Handled = True
    End Sub

    Private Sub TopWindow_DragLeave(ByVal sender As Object, ByVal e As DragEventArgs)
        Me.RemoveDraggedAdorner()
        e.Handled = True
    End Sub

    Private Sub TopWindow_DragOver(ByVal sender As Object, ByVal e As DragEventArgs)
        Me.ShowDraggedAdorner(e.GetPosition(_objTopWindow))
        e.Effects = DragDropEffects.None
        e.Handled = True
    End Sub

    Private Sub UpdateInsertionAdornerPosition()

        If _objInsertionAdorner IsNot Nothing Then
            _objInsertionAdorner.IsInFirstHalf = _bolIsInFirstHalf
            _objInsertionAdorner.InvalidateVisual()
        End If

    End Sub

#End Region

End Class
