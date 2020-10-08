Imports XAMLPowerToys.Model
Partial Public Class GridCellEditor

    Private Sub ComboBox_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs)

        If Me.gridCellEditor Is Nothing Then
            'this happens on load because we are wired up in XAML
            Exit Sub

        ElseIf Me.gridCellEditor.Children IsNot Nothing Then
            Me.gridCellEditor.Children.Clear()
        End If

        Dim enumControlType As ControlType = CType([Enum].Parse(GetType(ControlType), CType(sender, ComboBox).SelectedValue.ToString), ControlType)

        Select Case enumControlType

            Case ControlType.DatePicker
                Me.gridCellEditor.Children.Add(New DatePickerEditor)

            Case ControlType.CheckBox
                Me.gridCellEditor.Children.Add(New CheckBoxEditor)

            Case ControlType.ComboBox
                Me.gridCellEditor.Children.Add(New ComboBoxEditor)

            Case ControlType.Label
                Me.gridCellEditor.Children.Add(New LabelEditor)

            Case ControlType.TextBlock
                Me.gridCellEditor.Children.Add(New TextBlockEditor)

            Case ControlType.None

                'do nothing
            Case ControlType.TextBox
                Me.gridCellEditor.Children.Add(New TextBoxEditor)

            Case ControlType.Image
                Me.gridCellEditor.Children.Add(New ImageEditor)

            Case Else
                Throw New ArgumentOutOfRangeException("ControlType", enumControlType, "The programmer did not program this enum value.")
        End Select

    End Sub

    Private Sub GridCellEditor_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        cboControlType.ItemsSource = (From d As Object In [Enum].GetValues(GetType(ControlType)) Select d Order By d.ToString).ToArray
    End Sub

End Class
