Partial Public Class ImageEditor

    Private Sub ImageEditor_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded

        Dim objBinding As New Binding
        objBinding.Path = New PropertyPath("BindingPath")
        objBinding.Mode = BindingMode.TwoWay
        objBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged

        If CreateBusinessFormWindow.ClassEntity Is Nothing OrElse CreateBusinessFormWindow.ClassEntity.PropertyInfomation.Count = 0 Then
            Me.txtBindingPath.Visibility = Windows.Visibility.Visible
            Me.cboBindingPath.Visibility = Windows.Visibility.Collapsed
            Me.txtBindingPath.SetBinding(TextBox.TextProperty, objBinding)

        Else
            Me.txtBindingPath.Visibility = Windows.Visibility.Collapsed
            Me.cboBindingPath.Visibility = Windows.Visibility.Visible
            Me.cboBindingPath.SetBinding(ComboBox.SelectedValueProperty, objBinding)
            Me.cboBindingPath.ItemsSource = CreateBusinessFormWindow.ClassEntity.PropertyInfomation
        End If

    End Sub

End Class
