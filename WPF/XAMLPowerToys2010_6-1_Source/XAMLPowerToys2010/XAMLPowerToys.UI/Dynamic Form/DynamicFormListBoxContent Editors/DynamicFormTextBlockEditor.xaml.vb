Imports XAMLPowerToys.Model
Partial Public Class DynamicFormTextBlockEditor

    Private Sub cboStringFormat_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.cboStringFormat.RemoveHandler(ComboBox.SelectionChangedEvent, New SelectionChangedEventHandler(AddressOf cboStringFormat_SelectionChanged))
        Me.cboStringFormat.ItemsSource = Helpers.GetSampleFormats
        Me.cboStringFormat.SelectedIndex = -1
        Me.cboStringFormat.AddHandler(ComboBox.SelectionChangedEvent, New SelectionChangedEventHandler(AddressOf cboStringFormat_SelectionChanged))
    End Sub

    Private Sub cboStringFormat_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs)

        If Me.cboStringFormat.SelectedItem Is Nothing OrElse Me.cboStringFormat.SelectedIndex = -1 Then
            Exit Sub
        End If

        CType(Me.cboStringFormat.DataContext, DynamicFormListBoxContent).StringFormat = CType(Me.cboStringFormat.SelectedItem, SampleFormat).StringFormat
    End Sub

    Private Sub DynamicFormTextBoxEditor_Unloaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Unloaded
        Me.cboStringFormat.RemoveHandler(ComboBox.SelectionChangedEvent, New SelectionChangedEventHandler(AddressOf cboStringFormat_SelectionChanged))
    End Sub

End Class
