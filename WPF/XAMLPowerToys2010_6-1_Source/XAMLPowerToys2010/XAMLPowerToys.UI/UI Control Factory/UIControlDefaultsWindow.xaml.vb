Imports System.Collections.ObjectModel
Imports System.ComponentModel
'
Partial Public Class UIControlDefaultsWindow

#Region " Methods "

    Private Sub btnAddNewUIProperty_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

        If Me.bdrContainer.DataContext IsNot Nothing Then

            Dim objUIProperty As New UIProperty
            CType(Me.bdrContainer.DataContext, UIControl).ControlProperties.Add(New UIProperty("ChangeMe", "ChangeMe"))
        End If

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        DialogResult = False
    End Sub

    Private Sub btnDeleteUIProperty_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

        If Me.bdrContainer.DataContext IsNot Nothing Then

            Dim objUIProperty As UIProperty = CType(CType(sender, Button).DataContext, UIProperty)

            If MessageBox.Show(String.Format("Are you sure you want to delete this Control Property:{0}Name: {1}{0}Value: {2}", vbCrLf, objUIProperty.ProperyName, objUIProperty.PropertyValue), "Delete Control Property?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) = MessageBoxResult.Yes Then
                CType(Me.bdrContainer.DataContext, UIControl).ControlProperties.Remove(objUIProperty)
            End If

        End If

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        UIControlFactory.Instance.Save(True)
        DialogResult = True
    End Sub

    Private Sub lbControls_SelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles lbControls.SelectionChanged

        If Me.lbControls Is Nothing OrElse Me.lbControls.SelectedItem Is Nothing Then
            Exit Sub
        End If

        Dim objUIControl As UIControl = CType(Me.lbControls.SelectedItem, UIControl)

        Select Case objUIControl.ControlRole

            Case UIControlRole.Border, UIControlRole.Grid
                Me.chkGenerateControlName.IsEnabled = False

            Case Else
                Me.chkGenerateControlName.IsEnabled = True
        End Select

        Select Case objUIControl.ControlRole

            Case UIControlRole.TextBox
                Me.chkIncludeTargetNullValueForNullableBindings.IsEnabled = True

            Case Else
                Me.chkIncludeTargetNullValueForNullableBindings.IsEnabled = False
        End Select

        Select Case objUIControl.ControlRole

            Case UIControlRole.DataGrid, UIControlRole.Border, UIControlRole.Grid, UIControlRole.Image, UIControlRole.Label, UIControlRole.TextBlock
                Me.chkIncludeNotifyOnValidationError.IsEnabled = False
                Me.chkIncludeValidatesOnDataErrors.IsEnabled = False
                Me.chkIncludeValidatesOnExceptions.IsEnabled = False

            Case UIControlRole.CheckBox, UIControlRole.ComboBox, UIControlRole.TextBox, UIControlRole.DatePicker
                Me.chkIncludeNotifyOnValidationError.IsEnabled = True
                Me.chkIncludeValidatesOnDataErrors.IsEnabled = True
                Me.chkIncludeValidatesOnExceptions.IsEnabled = True

            Case Else
                Throw New ArgumentOutOfRangeException("ControlRole", CType(Me.lbControls.SelectedItem, UIControl).ControlRole, "Programmer did not program this value.")
        End Select

    End Sub

    Private Sub UIControlDefaultsMaintenance_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded

        Dim objUIControls As UIControls = UIControlFactory.Instance.UIControls
        Dim objCollectionView As CollectionView = CType(CollectionViewSource.GetDefaultView(objUIControls), CollectionView)
        objCollectionView.GroupDescriptions.Clear()
        objCollectionView.SortDescriptions.Clear()
        objCollectionView.GroupDescriptions.Add(New PropertyGroupDescription("UIPlatform"))
        objCollectionView.SortDescriptions.Add(New SortDescription("UIPlatform", ListSortDirection.Ascending))
        objCollectionView.SortDescriptions.Add(New SortDescription("ControlRoleName", ListSortDirection.Ascending))
        Me.lbControls.ItemsSource = objUIControls
        Me.lbControls.SelectedIndex = 0
    End Sub

#End Region

End Class
