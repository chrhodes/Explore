Imports XAMLPowerToys.Model
'
Partial Public Class FieldsListWindow

#Region " Declarations "

    Private _dblSaveHeight As Double
    Private _objClassEntity As ClassEntity
    Private _objDataObject As DataObject

#End Region

#Region " Constructors "

    Public Sub New(ByVal objClassEntity As ClassEntity)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        _objClassEntity = objClassEntity
    End Sub

#End Region

#Region " Methods "

    Private Sub btnCollapseExpand_click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

        Dim btn As Button = DirectCast(sender, Button)

        If btn.Content.ToString = "Collapse" Then
            _dblSaveHeight = Me.Height
            Me.Height = 97
            btn.Content = "Expand"

        Else
            Me.Height = _dblSaveHeight
            btn.Content = "Collapse"
        End If

    End Sub

    Private Sub cboControlType_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

        Dim cbo As ComboBox = DirectCast(sender, ComboBox)
        cbo.ItemsSource = (From d As Object In [Enum].GetValues(GetType(ControlType)) Where d.ToString <> "None" Select d Order By d.ToString).ToArray
    End Sub

    Private Sub FieldsListWindow_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded

        For Each obj As PropertyInformation In _objClassEntity.PropertyInfomation

            If obj.TypeName.Contains("Boolean") Then
                obj.FieldListControlType = ControlType.CheckBox

            ElseIf obj.TypeName.Contains("Date") Then
                obj.FieldListControlType = ControlType.DatePicker

            Else
                obj.FieldListControlType = ControlType.TextBox
            End If

        Next

        Me.Title = String.Concat("Fields List For Class: ", _objClassEntity.ClassName)
        Me.lbFields.ItemsSource = _objClassEntity.PropertyInfomation
    End Sub

    Private Function GetControlsForField(ByVal pi As PropertyInformation) As String

        Dim enumUIPlatform As UIPlatform = UIPlatform.WPF

        If _objClassEntity.IsSilverlight Then

            enumUIPlatform = UIPlatform.Silverlight
        End If

        Dim intColumn As Nullable(Of Integer) = Nothing
        Dim intRow As Nullable(Of Integer) = Nothing

        If pi.FieldListIncludeGridAttachedProperties Then
            intColumn = 0
            intRow = 0
        End If

        Dim strReturn As String = String.Empty

        If Me.rdoLabelAndControl.IsChecked OrElse Me.rdoLabelOnly.IsChecked Then
            strReturn = UIControlFactory.Instance.MakeLabelWithoutBinding(enumUIPlatform, intColumn, intRow, pi.LabelText)

            If rdoLabelAndControl.IsChecked Then
                strReturn = String.Concat(strReturn, vbCrLf)
            End If

        End If

        If Me.rdoLabelAndControl.IsChecked OrElse Me.rdoControlOnly.IsChecked Then

            Select Case pi.FieldListControlType

                Case ControlType.DatePicker
                    strReturn = String.Concat(strReturn, UIControlFactory.Instance.MakeDatePicker(enumUIPlatform, intColumn, intRow, pi.Name, Nothing))

                Case ControlType.CheckBox
                    strReturn = String.Concat(strReturn, UIControlFactory.Instance.MakeCheckBox(enumUIPlatform, intColumn, intRow, String.Empty, pi.Name, BindingMode.TwoWay))

                Case ControlType.ComboBox
                    strReturn = String.Concat(strReturn, UIControlFactory.Instance.MakeComboBox(enumUIPlatform, intColumn, intRow, pi.Name, BindingMode.TwoWay))

                    If _objClassEntity.IsSilverlight Then
                        strReturn = String.Concat("<!-- Bind Silverlight ComboBox in code after its ItemsSource has been loaded -->", vbCrLf, strReturn)
                    End If

                Case ControlType.Image
                    strReturn = String.Concat(strReturn, UIControlFactory.Instance.MakeImage(enumUIPlatform, intColumn, intRow, pi.Name))

                Case ControlType.Label
                    strReturn = String.Concat(strReturn, UIControlFactory.Instance.MakeLabel(enumUIPlatform, intColumn, intRow, pi.Name, pi.StringFormat, _objClassEntity.SilverlightVersion))

                Case ControlType.TextBlock
                    strReturn = String.Concat(strReturn, UIControlFactory.Instance.MakeTextBlock(enumUIPlatform, intColumn, intRow, pi.Name, pi.StringFormat, _objClassEntity.SilverlightVersion))

                Case ControlType.TextBox
                    strReturn = String.Concat(strReturn, UIControlFactory.Instance.MakeTextBox(enumUIPlatform, intColumn, intRow, pi.Name, BindingMode.TwoWay, Nothing, Nothing, pi.StringFormat, pi.TypeName.StartsWith("Nullable"), _objClassEntity.SilverlightVersion))
            End Select

        End If

        Return strReturn
    End Function

    Private Sub TextBlockDrag_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)

        If e.LeftButton = MouseButtonState.Pressed Then
            _objDataObject = Nothing
        End If

    End Sub

    Private Sub TextBlockDrag_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseEventArgs)

        If e.LeftButton = MouseButtonState.Released OrElse _objDataObject IsNot Nothing Then
            Exit Sub
        End If

        Dim tb As TextBlock = DirectCast(sender, TextBlock)
        _objDataObject = New DataObject(DataFormats.Text, GetControlsForField(CType(tb.DataContext, PropertyInformation)))
        DragDrop.DoDragDrop(tb, _objDataObject, DragDropEffects.Copy)
    End Sub

#End Region

End Class
