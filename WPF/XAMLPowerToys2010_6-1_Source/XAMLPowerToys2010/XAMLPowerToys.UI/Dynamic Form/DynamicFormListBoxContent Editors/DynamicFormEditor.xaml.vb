Partial Public Class DynamicFormEditor

    Private Sub cboControlType_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

        Dim cbo As ComboBox = CType(sender, ComboBox)
        Dim ary As Array = [Enum].GetNames(GetType(DynamicFormControlType))
        Array.Sort(ary)
        cbo.ItemsSource = ary
        SetRenderInDataColumnTemplateVisibility()
    End Sub

    Private Sub cboControlType_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs)

        If Me.gridDynamicFormControlEditor Is Nothing Then
            'this happens on load because we are wired up in XAML
            Exit Sub

        ElseIf Me.gridDynamicFormControlEditor.Children IsNot Nothing Then
            Me.gridDynamicFormControlEditor.Children.Clear()
        End If

        Dim bolIsSilverlight As Boolean = Me.chkRenderInDataColumnTemplate.Visibility = Windows.Visibility.Visible
        Me.chkRenderInDataColumnTemplate.IsEnabled = True

        Dim enumControlType As DynamicFormControlType = CType([Enum].Parse(GetType(DynamicFormControlType), CType(sender, ComboBox).SelectedValue.ToString), DynamicFormControlType)

        Select Case enumControlType

            Case DynamicFormControlType.DatePicker
                Me.gridDynamicFormControlEditor.Children.Add(New DynamicFormDatePickerEditor)

            Case DynamicFormControlType.CheckBox
                Me.gridDynamicFormControlEditor.Children.Add(New DynamicFormCheckBoxEditor)

            Case DynamicFormControlType.ComboBox
                Me.gridDynamicFormControlEditor.Children.Add(New DynamicFormComboBoxEditor)

                If bolIsSilverlight Then
                    Me.chkRenderInDataColumnTemplate.IsChecked = True
                    Me.chkRenderInDataColumnTemplate.IsEnabled = False
                End If

            Case DynamicFormControlType.Image
                Me.gridDynamicFormControlEditor.Children.Add(New DynamicFormTextBlockEditor)

                If bolIsSilverlight Then
                    Me.chkRenderInDataColumnTemplate.IsChecked = True
                    Me.chkRenderInDataColumnTemplate.IsEnabled = False
                End If

            Case DynamicFormControlType.Label, DynamicFormControlType.TextBlock
                Me.gridDynamicFormControlEditor.Children.Add(New DynamicFormTextBlockEditor)

            Case DynamicFormControlType.TextBox
                Me.gridDynamicFormControlEditor.Children.Add(New DynamicFormTextBoxEditor)

            Case Else
                Throw New ArgumentOutOfRangeException("ControlType", enumControlType, "The programmer did not program this enum value.")
        End Select

    End Sub

    Private Sub cboDescriptionViewerPosition_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

        Dim cbo As ComboBox = CType(sender, ComboBox)
        Dim ary As String() = {"Auto", "BesideContent", "BesideLabel"}
        cbo.ItemsSource = ary
    End Sub

    Private Sub cboLabelPosition_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

        Dim cbo As ComboBox = CType(sender, ComboBox)
        Dim ary As String() = {"Auto", "Left", "Top"}
        cbo.ItemsSource = ary
    End Sub

    Private Sub DynamicFormEditor_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        SetSilverlightDataFormFieldsVisibility()
    End Sub

    Private Sub SetRenderInDataColumnTemplateVisibility()
        Me.chkRenderInDataColumnTemplate.Visibility = Windows.Visibility.Collapsed

        Dim obj As CreateBusinessFormFromClassWindow = TryCast(Helpers.FindAncestorWindow(Me), CreateBusinessFormFromClassWindow)

        If obj IsNot Nothing Then

            If Not obj.ClassEntity.IsSilverlight Then
                Exit Sub
            End If

            If obj.cboSelectObjectToCreate.SelectedValue.ToString <> "Silverlight Data Grid" Then
                Exit Sub
            End If

            Me.chkRenderInDataColumnTemplate.Visibility = Windows.Visibility.Visible
        End If

    End Sub

    Private Sub SetSilverlightDataFormFieldsVisibility()
        Me.gridSilverlightDataFormFields.Visibility = Windows.Visibility.Collapsed

        Dim obj As CreateBusinessFormFromClassWindow = TryCast(Helpers.FindAncestorWindow(Me), CreateBusinessFormFromClassWindow)

        If obj IsNot Nothing Then

            If Not obj.ClassEntity.IsSilverlight Then
                Exit Sub
            End If

            If obj.cboSelectObjectToCreate.SelectedValue.ToString <> "Silverlight Data Form" Then
                Exit Sub
            End If

            Me.gridSilverlightDataFormFields.Visibility = Windows.Visibility.Visible
        End If

    End Sub

End Class
