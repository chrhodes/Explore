Imports System.Windows.Data
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports XAMLPowerToys.Model
'
Partial Public Class TextBoxEditor

    Private Sub cboStringFormats_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs)
        Me.txtStringFormat.Text = Me.cboStringFormat.SelectedValue.ToString
    End Sub

    Private Sub TextBoxEditor_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Me.cboStringFormat.ItemsSource = Helpers.GetSampleFormats
        Me.cboStringFormat.SelectedIndex = -1

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

        Me.cboStringFormat.AddHandler(ComboBox.SelectionChangedEvent, New SelectionChangedEventHandler(AddressOf cboStringFormats_SelectionChanged))
    End Sub

    Private Sub TextBoxEditor_Unloaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Unloaded
        Me.cboStringFormat.RemoveHandler(ComboBox.SelectionChangedEvent, New SelectionChangedEventHandler(AddressOf cboStringFormats_SelectionChanged))
    End Sub

End Class
