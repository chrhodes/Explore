Imports System
Imports System.Collections
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports XAMLPowerToys.Model

Public Class DynamicFormUtilities

#Region " Methods "

    Public Shared Function FindAncestor(ByVal ancestorType As Type, ByVal objVisual As Visual) As FrameworkElement

        Do While objVisual IsNot Nothing AndAlso Not ancestorType.IsInstanceOfType(objVisual)
            objVisual = DirectCast(VisualTreeHelper.GetParent(objVisual), Visual)
        Loop

        Return TryCast(objVisual, FrameworkElement)
    End Function

    Public Shared Function GetItemContainer(ByVal itemsControl As ItemsControl, ByVal bottomMostVisual As Visual) As FrameworkElement

        Dim itemContainer As FrameworkElement = Nothing

        If itemsControl IsNot Nothing AndAlso bottomMostVisual IsNot Nothing AndAlso itemsControl.Items.Count >= 1 Then

            Dim firstContainer As DependencyObject = itemsControl.ItemContainerGenerator.ContainerFromIndex(0)

            If firstContainer Is Nothing Then
                Return itemContainer
            End If

            itemContainer = DynamicFormUtilities.FindAncestor(firstContainer.GetType, bottomMostVisual)

            If itemContainer Is Nothing OrElse itemContainer.DataContext Is Nothing Then
                Return itemContainer
            End If

            Dim itemContainerVerify As FrameworkElement = TryCast(itemsControl.ItemContainerGenerator.ContainerFromItem(itemContainer.DataContext), FrameworkElement)

            If Not itemContainer Is itemContainerVerify Then
                itemContainer = Nothing
            End If

        End If

        Return itemContainer
    End Function

    Public Shared Function HasVerticalOrientation(ByVal itemContainer As FrameworkElement) As Boolean

        Dim bolHasVerticalOrientation As Boolean = True

        If Not itemContainer Is Nothing Then

            Dim objPanel As Panel = TryCast(VisualTreeHelper.GetParent(itemContainer), Panel)
            Dim objStackPanel As StackPanel = TryCast(objPanel, StackPanel)

            If objStackPanel IsNot Nothing Then
                Return objStackPanel.Orientation = Orientation.Vertical
            End If

            Dim objWrapPanel As WrapPanel = TryCast(objPanel, WrapPanel)

            If objWrapPanel IsNot Nothing Then
                bolHasVerticalOrientation = objWrapPanel.Orientation = Orientation.Vertical
            End If

        End If

        Return bolHasVerticalOrientation
    End Function

    Public Shared Sub InsertItemInItemsControl(ByVal itemsControl As ItemsControl, ByVal itemToInsert As Object, ByVal insertionIndex As Integer)

        If itemToInsert IsNot Nothing Then

            Dim objItemsSource As IEnumerable = itemsControl.ItemsSource

            If objItemsSource IsNot Nothing AndAlso TypeOf itemToInsert Is DynamicFormEditor Then

                Dim strPropertyName As String = CType(DirectCast(itemToInsert, DynamicFormEditor).DataContext, DynamicFormListBoxContent).BindingPath
                Dim objCollectionView As CollectionView = TryCast(CollectionViewSource.GetDefaultView(itemsControl.ItemsSource), CollectionView)

                If objCollectionView IsNot Nothing Then

                    For Each obj As PropertyInformation In objCollectionView

                        If obj.Name = strPropertyName Then
                            obj.HasBeenUsed = False
                            Exit For
                        End If

                    Next

                    objCollectionView.Refresh()
                End If

            ElseIf objItemsSource Is Nothing AndAlso TypeOf itemToInsert Is PropertyInformation Then
                'this occurs when dragging from the left side field list to the form fields listings
                itemsControl.Items.Insert(insertionIndex, Helpers.DynamicFormEditorFactory(CType(itemToInsert, PropertyInformation)))

            ElseIf objItemsSource Is Nothing AndAlso TypeOf itemToInsert Is DynamicFormEditor Then
                itemsControl.Items.Insert(insertionIndex, itemToInsert)
            End If

        End If

    End Sub

    Public Shared Function IsInFirstHalf(ByVal container As FrameworkElement, ByVal clickedPoint As Point, ByVal bolHasVerticalOrientation As Boolean) As Boolean

        If bolHasVerticalOrientation Then
            Return (clickedPoint.Y < (container.ActualHeight / 2))
        End If

        Return (clickedPoint.X < (container.ActualWidth / 2))
    End Function

    Public Shared Function IsMovementBigEnough(ByVal initialMousePosition As Point, ByVal currentPosition As Point) As Boolean
        Return ((Math.Abs(CDbl((currentPosition.X - initialMousePosition.X))) >= SystemParameters.MinimumHorizontalDragDistance) OrElse (Math.Abs(CDbl((currentPosition.Y - initialMousePosition.Y))) >= SystemParameters.MinimumVerticalDragDistance))
    End Function

    Public Shared Function RemoveItemFromItemsControl(ByVal itemsControl As ItemsControl, ByVal itemToRemove As Object) As Integer

        Dim indexToBeRemoved As Integer = -1

        If itemToRemove IsNot Nothing Then
            indexToBeRemoved = itemsControl.Items.IndexOf(itemToRemove)

            If indexToBeRemoved = -1 Then
                Return indexToBeRemoved
            End If

            Dim itemsSource As IEnumerable = itemsControl.ItemsSource

            If itemsSource IsNot Nothing AndAlso itemsControl.ItemsSource.GetType.IsGenericType Then
                DirectCast(itemToRemove, PropertyInformation).HasBeenUsed = True

                Dim objCollectionView As CollectionView = TryCast(CollectionViewSource.GetDefaultView(itemsControl.ItemsSource), CollectionView)

                If objCollectionView IsNot Nothing Then
                    objCollectionView.Refresh()
                End If

                Return indexToBeRemoved
            End If

            If itemsSource Is Nothing Then
                itemsControl.Items.RemoveAt(indexToBeRemoved)
                Return indexToBeRemoved
            End If

        End If

        Return indexToBeRemoved
    End Function

#End Region

End Class
