Partial Public Class DynamicFormCheckBoxEditor

    Private Sub cboBindingMode_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

        Dim cbo As ComboBox = CType(sender, ComboBox)

        If cbo.ItemsSource Is Nothing Then

            Dim objHelper As New Helpers
            cbo.ItemsSource = objHelper.GetSortedEnumNames(GetType(BindingMode))
        End If

    End Sub

End Class
