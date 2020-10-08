Imports System.IO
Imports System.Reflection
Imports System.Runtime.Serialization.Formatters.Binary
Imports XAMLPowerToys.Model
Imports System.Windows.Data
Imports System.Text
Imports System.Runtime.Serialization

Public Class UIControlFactory

#Region " Declarations "

    'these values are also in the Installer.vb
    Private Shared ReadOnly _strSaveSettingsFolderName As String = Path.Combine(Environment.GetEnvironmentVariable("APPDATA"), "Little Richie Software\XAML Power Toys 2010\")
    Private Shared ReadOnly _strSaveSettingsFileName As String = Path.Combine(Environment.GetEnvironmentVariable("APPDATA"), "Little Richie Software\XAML Power Toys 2010\XAMLPowerToys.Settings")
    Private Shared _instance As UIControlFactory
    Private _objUIControls As UIControls

#End Region

#Region " Properties "

    Public Shared ReadOnly Property Instance() As UIControlFactory
        Get

            If _instance Is Nothing Then
                _instance = New UIControlFactory
                _instance.UIControls = _instance.Load
            End If

            Return _instance
        End Get
    End Property

    Public Property UIControls() As UIControls
        Get
            Return _objUIControls
        End Get
        Set(ByVal Value As UIControls)
            _objUIControls = Value
        End Set
    End Property

#End Region

#Region " Constructors "

    Private Sub New()
    End Sub

#End Region

#Region " Control Creators "

    Public Function MakeCheckBox(ByVal enumUIPlatform As UIPlatform, ByVal intColumn As Nullable(Of Integer), ByVal intRow As Nullable(Of Integer), ByVal strContent As String, ByVal strPath As String, ByVal enumBindingMode As BindingMode) As String

        Dim ctrl As UIControl = GetUIControl(ControlType.CheckBox, enumUIPlatform)
        Dim sb As New StringBuilder(1024)

        If intColumn.HasValue Then
            sb.AppendFormat(" Grid.Column=""{0}"" ", intColumn.Value)
        End If

        If intRow.HasValue Then
            sb.AppendFormat(" Grid.Row=""{0}"" ", intRow.Value)
        End If

        If enumUIPlatform = UIPlatform.WPF Then
            sb.AppendFormat("Content=""{0}"" IsChecked=""{{Binding Path={1}, Mode={2}{3}}}"" ", strContent, strPath, enumBindingMode.ToString, IIf(enumBindingMode = BindingMode.TwoWay, String.Concat(", UpdateSourceTrigger=PropertyChanged", ctrl.BindingPropertyString), String.Empty))

        Else
            sb.AppendFormat("Content=""{0}"" IsChecked=""{{Binding Path={1}, Mode={2}{3}}}"" ", strContent, strPath, enumBindingMode.ToString, IIf(enumBindingMode = BindingMode.TwoWay, ctrl.BindingPropertyString, String.Empty))
        End If

        Return ctrl.MakeControlFromDefaults(sb.ToString, True, strPath)
    End Function

    Public Function MakeComboBox(ByVal enumUIPlatform As UIPlatform, ByVal intColumn As Nullable(Of Integer), ByVal intRow As Nullable(Of Integer), ByVal strPath As String, ByVal enumBindingMode As BindingMode) As String

        Dim ctrl As UIControl = GetUIControl(ControlType.ComboBox, enumUIPlatform)
        Dim sb As New StringBuilder(1024)

        If intColumn.HasValue Then
            sb.AppendFormat(" Grid.Column=""{0}"" ", intColumn.Value)
        End If

        If intRow.HasValue Then
            sb.AppendFormat(" Grid.Row=""{0}"" ", intRow.Value)
        End If

        If enumUIPlatform = UIPlatform.WPF Then
            sb.AppendFormat(" SelectedValue=""{{Binding Path={0}, Mode={1}{2}}}"" ", strPath, enumBindingMode.ToString, IIf(enumBindingMode = BindingMode.TwoWay, String.Concat(", UpdateSourceTrigger=PropertyChanged", ctrl.BindingPropertyString), String.Empty))

        Else
            sb.AppendFormat(" SelectedItem=""{{Binding Path={0}, Mode={1}{2}}}"" ", strPath, enumBindingMode.ToString, IIf(enumBindingMode = BindingMode.TwoWay, ctrl.BindingPropertyString, String.Empty))
        End If

        Return ctrl.MakeControlFromDefaults(sb.ToString, True, strPath)
    End Function

    Public Function MakeDatePicker(ByVal enumUIPlatform As UIPlatform, ByVal intColumn As Nullable(Of Integer), ByVal intRow As Nullable(Of Integer), ByVal strPath As String, ByVal intWidth As Nullable(Of Integer)) As String

        Dim ctrl As UIControl = GetUIControl(ControlType.DatePicker, enumUIPlatform)
        Dim sb As New StringBuilder(1024)

        If intColumn.HasValue Then
            sb.AppendFormat(" Grid.Column=""{0}"" ", intColumn.Value)
        End If

        If intRow.HasValue Then
            sb.AppendFormat(" Grid.Row=""{0}"" ", intRow.Value)
        End If

        If intWidth.HasValue Then
            sb.AppendFormat(" Width=""{0}"" ", intWidth.Value)
            sb.Append(" HorizontalAlignment=""Left"" ")
        End If

        sb.AppendFormat("SelectedDate=""{{Binding Path={0}, Mode=TwoWay}}"" ", strPath)
        Return ctrl.MakeControlFromDefaults(sb.ToString, True, strPath)
    End Function

    Public Function MakeImage(ByVal enumUIPlatform As UIPlatform, ByVal intColumn As Nullable(Of Integer), ByVal intRow As Nullable(Of Integer), ByVal strPath As String) As String

        Dim ctrl As UIControl = GetUIControl(ControlType.Image, enumUIPlatform)
        Dim sb As New StringBuilder(1024)

        If intColumn.HasValue Then
            sb.AppendFormat(" Grid.Column=""{0}"" ", intColumn.Value)
        End If

        If intRow.HasValue Then
            sb.AppendFormat(" Grid.Row=""{0}"" ", intRow.Value)
        End If

        sb.AppendFormat("Source=""{{Binding Path={0}}}"" ", strPath)
        Return ctrl.MakeControlFromDefaults(sb.ToString, True, strPath)
    End Function

    Public Function MakeLabel(ByVal enumUIPlatform As UIPlatform, ByVal intColumn As Nullable(Of Integer), ByVal intRow As Nullable(Of Integer), ByVal strContent As String, ByVal strStringFormat As String, ByVal strSilverlightVersion As String) As String

        Dim ctrl As UIControl = GetUIControl(ControlType.Label, enumUIPlatform)
        Dim sb As New StringBuilder(1024)

        If intColumn.HasValue Then
            sb.AppendFormat(" Grid.Column=""{0}"" ", intColumn.Value)
        End If

        If intRow.HasValue Then
            sb.AppendFormat(" Grid.Row=""{0}"" ", intRow.Value)
        End If

        If enumUIPlatform = UIPlatform.WPF OrElse Not strSilverlightVersion.StartsWith("3") Then

            If Not String.IsNullOrEmpty(strStringFormat) Then
                sb.AppendFormat(" Content=""{{Binding Path={0}}}"" ContentStringFormat=""{1}""", strContent, strStringFormat.Replace("{", "\{").Replace("}", "\}"))

            Else
                sb.AppendFormat(" Content=""{{Binding Path={0}}"" ", strContent)
            End If

        Else
            sb.AppendFormat(" Content=""{{Binding Path={0}}"" ", strContent)
        End If

        Return ctrl.MakeControlFromDefaults(sb.ToString, True, strContent)
    End Function

    Public Function MakeLabelWithoutBinding(ByVal enumUIPlatform As UIPlatform, ByVal intColumn As Nullable(Of Integer), ByVal intRow As Nullable(Of Integer), ByVal strContent As String) As String

        Dim ctrl As UIControl = GetUIControl(ControlType.Label, enumUIPlatform)
        Dim sb As New StringBuilder(1024)

        If intColumn.HasValue Then
            sb.AppendFormat(" Grid.Column=""{0}"" ", intColumn.Value)
        End If

        If intRow.HasValue Then
            sb.AppendFormat(" Grid.Row=""{0}"" ", intRow.Value)
        End If

        If ctrl.ControlType.ToLower.Contains("label") Then
            sb.AppendFormat(" Content=""{0}"" ", strContent)

        Else
            sb.AppendFormat(" Text=""{0}"" ", strContent)
        End If

        Return ctrl.MakeControlFromDefaults(sb.ToString, True, String.Empty)
    End Function

    Public Function MakeTextBlock(ByVal enumUIPlatform As UIPlatform, ByVal intColumn As Nullable(Of Integer), ByVal intRow As Nullable(Of Integer), ByVal strPath As String, ByVal strStringFormat As String, ByVal strSilverlightVersion As String) As String

        If Not String.IsNullOrEmpty(strStringFormat) Then
            strStringFormat = String.Format(", StringFormat={0}", strStringFormat.Replace("{", "\{").Replace("}", "\}"))
        End If

        If enumUIPlatform = UIPlatform.Silverlight AndAlso strSilverlightVersion.StartsWith("3") Then
            strStringFormat = String.Empty
        End If

        Dim ctrl As UIControl = GetUIControl(ControlType.TextBlock, enumUIPlatform)
        Dim sb As New StringBuilder(1024)

        If intColumn.HasValue Then
            sb.AppendFormat(" Grid.Column=""{0}"" ", intColumn.Value)
        End If

        If intRow.HasValue Then
            sb.AppendFormat(" Grid.Row=""{0}"" ", intRow.Value)
        End If

        sb.AppendFormat("Text=""{{Binding Path={0}{1}}}"" ", strPath, strStringFormat)
        Return ctrl.MakeControlFromDefaults(sb.ToString, True, strPath)
    End Function

    Public Function MakeTextBox(ByVal enumUIPlatform As UIPlatform, ByVal intColumn As Nullable(Of Integer), ByVal intRow As Nullable(Of Integer), ByVal strPath As String, ByVal enumBindingMode As BindingMode, ByVal intWidth As Nullable(Of Integer), ByVal intMaximumLength As Nullable(Of Integer), ByVal strStringFormat As String, ByVal bolIsSouceNullable As Boolean, ByVal strSilverlightTFM As String) As String

        If Not String.IsNullOrEmpty(strStringFormat) Then
            strStringFormat = String.Format(", StringFormat={0}", strStringFormat.Replace("{", "\{").Replace("}", "\}"))
        End If

        If enumUIPlatform = UIPlatform.Silverlight And strSilverlightTFM.StartsWith("3") Then
            strStringFormat = String.Empty
        End If

        Dim ctrl As UIControl = GetUIControl(ControlType.TextBox, enumUIPlatform)
        Dim sb As New StringBuilder(1024)

        If intColumn.HasValue Then
            sb.AppendFormat(" Grid.Column=""{0}"" ", intColumn.Value)
        End If

        If intRow.HasValue Then
            sb.AppendFormat(" Grid.Row=""{0}"" ", intRow.Value)
        End If

        If enumUIPlatform = UIPlatform.WPF Then
            sb.AppendFormat("Text=""{{Binding Path={0}, Mode={1}{2}{3}}}"" ", strPath, enumBindingMode.ToString, IIf(enumBindingMode = BindingMode.TwoWay, String.Concat(", UpdateSourceTrigger=LostFocus", ctrl.BindingPropertyString, strStringFormat), String.Empty), IIf(bolIsSouceNullable AndAlso ctrl.IncludeTargetNullValueForNullableBindings, ", TargetNullValue=''", String.Empty))

        Else
            If strSilverlightTFM.StartsWith("3") Then
                sb.AppendFormat("Text=""{{Binding Path={0}, Mode={1}{2}}}"" ", strPath, enumBindingMode.ToString, IIf(enumBindingMode = BindingMode.TwoWay, ctrl.BindingPropertyString, String.Empty))
            ElseIf String.IsNullOrWhiteSpace(strStringFormat) Then
                sb.AppendFormat("Text=""{{Binding Path={0}, Mode={1}{2}{3}}}"" ", strPath, enumBindingMode.ToString, IIf(enumBindingMode = BindingMode.TwoWay, ctrl.BindingPropertyString, String.Empty), IIf(bolIsSouceNullable AndAlso ctrl.IncludeTargetNullValueForNullableBindings, ", TargetNullValue=''", String.Empty))
            Else
                sb.AppendFormat("Text=""{{Binding Path={0}, Mode={1}{2}{3}}}"" ", strPath, enumBindingMode.ToString, IIf(enumBindingMode = BindingMode.TwoWay, String.Concat(ctrl.BindingPropertyString, strStringFormat), String.Empty), IIf(bolIsSouceNullable AndAlso ctrl.IncludeTargetNullValueForNullableBindings, ", TargetNullValue=''", String.Empty))
            End If

        End If

        If intWidth.HasValue Then
            sb.AppendFormat(" Width=""{0}""", intWidth.Value)

        ElseIf enumUIPlatform = UIPlatform.Silverlight Then
            sb.Append(" HorizontalAlignment=""Stretch"" ")
        End If

        If intMaximumLength.HasValue Then
            sb.AppendFormat(" MaxLength=""{0}""", intMaximumLength.Value)
        End If

        Return ctrl.MakeControlFromDefaults(sb.ToString, True, strPath)
    End Function

#End Region

#Region " Methods "

    Public Function GetUIControl(ByVal enumUIControlType As ControlType, ByVal enumUIPlatform As UIPlatform) As UIControl
        Return _objUIControls.GetUIControl(enumUIControlType, enumUIPlatform)
    End Function

    Public Function GetUIControl(ByVal enumUIControlRole As UIControlRole, ByVal enumUIPlatform As UIPlatform) As UIControl
        Return _objUIControls.GetUIControl(enumUIControlRole, enumUIPlatform)
    End Function

    Public Function GetUIControlsForPlatform(ByVal enumUIPlatform As UIPlatform) As List(Of UIControl)
        Return _objUIControls.GetUIControlsForPlatform(enumUIPlatform)
    End Function

    Public Function Load() As UIControls

        If Not Directory.Exists(_strSaveSettingsFolderName) Then
            Directory.CreateDirectory(_strSaveSettingsFolderName)
        End If

        If Not File.Exists(_strSaveSettingsFileName) Then
            CreateDefaults()
            Save(False)
            Utilities.ShowInformationMessage("Settings File Created", "Your settings file has been created for you.  You can configure your settings using the Set Control Defaults command.")

        Else

            Try

                If _objUIControls IsNot Nothing Then
                    _objUIControls.Clear()
                    _objUIControls = Nothing
                End If

                Using fs As New System.IO.FileStream(_strSaveSettingsFileName, System.IO.FileMode.Open)
                    _objUIControls = CType(Deserialize(fs), UIControls)
                End Using

            Catch ex As Exception
                Utilities.ShowInformationMessage("Settings File", "Unable to load previous settings file.  Creating new settings file.", String.Empty, ex.ToString)
                CreateDefaults()
                Save(False)
            End Try

        End If

        Return _objUIControls
    End Function

    Public Sub Save(ByVal bolShowSaveMessage As Boolean)

        Dim objListToRemove As New List(Of UIProperty)

        For Each objUIControl As UIControl In Me.UIControls
            objListToRemove.Clear()

            For Each obj As UIProperty In objUIControl.ControlProperties

                If String.IsNullOrEmpty(obj.PropertyValue) OrElse String.IsNullOrEmpty(obj.ProperyName) OrElse obj.PropertyValue = "ChangeMe" OrElse obj.ProperyName = "ChangeMe" Then
                    objListToRemove.Add(obj)
                End If

            Next

            If objListToRemove.Count > 0 Then

                For Each obj As UIProperty In objListToRemove
                    objUIControl.ControlProperties.Remove(obj)
                Next

            End If

        Next

        Try
            Using fs As New System.IO.FileStream(_strSaveSettingsFileName, System.IO.FileMode.Create)
                Serialize(fs, _objUIControls)
            End Using

            If bolShowSaveMessage Then
                Utilities.ShowInformationMessage("Saved Settings File Location", "Settings saved to: " & _strSaveSettingsFileName)
            End If

        Catch ex As Exception
            Utilities.ShowExceptionMessage("Bummer: Exception While Saving Settings", ex.Message, String.Empty, ex.ToString)
        End Try

    End Sub

    Private Function CreateBorder(ByVal enumUIPlatform As UIPlatform) As UIControl

        Dim obj As New UIControl(enumUIPlatform, UIControlRole.Border, UIControlRole.Border.ToString)
        With obj
            .ControlProperties.Add(New UIProperty("BorderBrush", "LightGray"))
            .ControlProperties.Add(New UIProperty("BorderThickness", "1"))
            .ControlProperties.Add(New UIProperty("CornerRadius", "10"))
            .ControlProperties.Add(New UIProperty("Padding", "10"))
        End With
        Return obj
    End Function

    Private Function CreateCheckBox(ByVal enumUIPlatform As UIPlatform) As UIControl

        Dim obj As New UIControl(enumUIPlatform, UIControlRole.CheckBox, UIControlRole.CheckBox.ToString)
        With obj

            If enumUIPlatform = UIPlatform.WPF Then
                .IncludeNotifyOnValidationError = True
                .IncludeValidatesOnDataErrors = True
                .IncludeValidatesOnExceptions = True

            ElseIf enumUIPlatform = UIPlatform.Silverlight Then
                .IncludeNotifyOnValidationError = True
                .IncludeValidatesOnDataErrors = True
                .IncludeValidatesOnExceptions = True
            End If

        End With
        Return obj
    End Function

    Private Function CreateComboBox(ByVal enumUIPlatform As UIPlatform) As UIControl

        Dim obj As New UIControl(enumUIPlatform, UIControlRole.ComboBox, UIControlRole.ComboBox.ToString)
        With obj

            If enumUIPlatform = UIPlatform.WPF Then
                .IncludeNotifyOnValidationError = True
                .IncludeValidatesOnDataErrors = True
                .IncludeValidatesOnExceptions = True
                .ControlProperties.Add(New UIProperty("IsSynchronizedWithCurrentItem", "True"))

            ElseIf enumUIPlatform = UIPlatform.Silverlight Then
                .IncludeNotifyOnValidationError = True
                .IncludeValidatesOnDataErrors = True
                .IncludeValidatesOnExceptions = True
            End If

        End With
        Return obj
    End Function

    Private Function CreateDataGrid(ByVal enumUIPlatform As UIPlatform) As UIControl

        Dim obj As New UIControl(enumUIPlatform, UIControlRole.DataGrid, UIControlRole.DataGrid.ToString)
        With obj

            If enumUIPlatform = UIPlatform.WPF Then
                .ControlType = "DataGrid"
                .ControlProperties.Add(New UIProperty("AutoGenerateColumns", "False"))
                .ControlProperties.Add(New UIProperty("AlternationCount", "2"))

            ElseIf enumUIPlatform = UIPlatform.Silverlight Then
                .ControlType = "sdk:DataGrid"
                .ControlProperties.Add(New UIProperty("AutoGenerateColumns", "False"))
            End If

        End With
        Return obj
    End Function

    Private Function CreateDatePicker(ByVal enumUIPlatform As UIPlatform) As UIControl

        Dim obj As New UIControl(enumUIPlatform, UIControlRole.DatePicker, UIControlRole.DatePicker.ToString)
        With obj

            If enumUIPlatform = UIPlatform.WPF Then
                .ControlType = "DatePicker"
                .ControlProperties.Add(New UIProperty("SelectedDateFormat", "Short"))

            ElseIf enumUIPlatform = UIPlatform.Silverlight Then
                .ControlType = "sdk:DatePicker"
                .ControlProperties.Add(New UIProperty("SelectedDateFormat", "Short"))
            End If

        End With
        Return obj
    End Function

    Private Sub CreateDefaults()
        _objUIControls = New UIControls
        With _objUIControls
            .Add(CreateBorder(UIPlatform.Silverlight))
            .Add(CreateBorder(UIPlatform.WPF))
            '
            .Add(CreateCheckBox(UIPlatform.Silverlight))
            .Add(CreateCheckBox(UIPlatform.WPF))
            '
            .Add(CreateComboBox(UIPlatform.Silverlight))
            .Add(CreateComboBox(UIPlatform.WPF))
            '
            .Add(CreateGrid(UIPlatform.Silverlight))
            .Add(CreateGrid(UIPlatform.WPF))
            '
            .Add(CreateImage(UIPlatform.Silverlight))
            .Add(CreateImage(UIPlatform.WPF))
            '
            .Add(CreateLabel(UIPlatform.Silverlight))
            .Add(CreateLabel(UIPlatform.WPF))
            '
            .Add(CreateTextBlock(UIPlatform.Silverlight))
            .Add(CreateTextBlock(UIPlatform.WPF))
            '
            .Add(CreateTextBox(UIPlatform.Silverlight))
            .Add(CreateTextBox(UIPlatform.WPF))
            '
            .Add(CreateDataGrid(UIPlatform.Silverlight))
            .Add(CreateDataGrid(UIPlatform.WPF))
            '
            .Add(CreateDatePicker(UIPlatform.Silverlight))
            .Add(CreateDatePicker(UIPlatform.WPF))
        End With
    End Sub

    Private Function CreateGrid(ByVal enumUIPlatform As UIPlatform) As UIControl
        Return New UIControl(enumUIPlatform, UIControlRole.Grid, UIControlRole.Grid.ToString)
    End Function

    Private Function CreateImage(ByVal enumUIPlatform As UIPlatform) As UIControl
        Return New UIControl(enumUIPlatform, UIControlRole.Image, UIControlRole.Image.ToString)
    End Function

    Private Function CreateLabel(ByVal enumUIPlatform As UIPlatform) As UIControl

        If enumUIPlatform = UIPlatform.Silverlight Then
            Return New UIControl(enumUIPlatform, UIControlRole.Label, UIControlRole.Label.ToString) With {.ControlType = "sdk:Label"}

        Else
            Return New UIControl(enumUIPlatform, UIControlRole.Label, UIControlRole.Label.ToString)
        End If

    End Function

    Private Function CreateTextBlock(ByVal enumUIPlatform As UIPlatform) As UIControl
        Return New UIControl(enumUIPlatform, UIControlRole.TextBlock, UIControlRole.TextBlock.ToString)
    End Function

    Private Function CreateTextBox(ByVal enumUIPlatform As UIPlatform) As UIControl

        Dim obj As New UIControl(enumUIPlatform, UIControlRole.TextBox, UIControlRole.TextBox.ToString)
        With obj

            If enumUIPlatform = UIPlatform.WPF Then
                .IncludeNotifyOnValidationError = True
                .IncludeValidatesOnDataErrors = True
                .IncludeValidatesOnExceptions = True

            ElseIf enumUIPlatform = UIPlatform.Silverlight Then
                .IncludeNotifyOnValidationError = True
                .IncludeValidatesOnDataErrors = True
                .IncludeValidatesOnExceptions = True
            End If

            .ControlProperties.Add(New UIProperty("HorizontalAlignment", "Left"))
            .ControlProperties.Add(New UIProperty("VerticalAlignment", "Top"))
        End With
        Return obj
    End Function

#End Region

#Region " Serialization Acid Trip "

    'The below three functions took FOREVER to correct until I read this thread.
    'this mess is required because the Deserialize method does not load assemblies the way you "think" it would.
    'the below assembly resolve function allows the Deserialize method to find the assembly its in.
    'the thread has the full story.
    '
    'http://social.msdn.microsoft.com/Forums/en-US/netfxbcl/thread/e5f0c371-b900-41d8-9a5b-1052739f2521/

    Private Function Deserialize(ByVal incomingData As System.IO.Stream) As Object
        Dim objBinaryFormatter As New BinaryFormatter
        Dim objReturn As Object = Nothing
        AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf CurrentDomain_AssemblyResolve
        objReturn = objBinaryFormatter.Deserialize(incomingData)
        RemoveHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf CurrentDomain_AssemblyResolve
        Return objReturn
    End Function

    Private Sub Serialize(ByVal serializationStream As System.IO.Stream, ByVal target As Object)
        Dim objBinaryFormatter As New BinaryFormatter
        AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf CurrentDomain_AssemblyResolve
        objBinaryFormatter.Serialize(serializationStream, target)
        RemoveHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf CurrentDomain_AssemblyResolve
    End Sub

    Private Shared Function CurrentDomain_AssemblyResolve(ByVal sender As Object, ByVal e As ResolveEventArgs) As Assembly
        Dim asyResult As Assembly = Nothing
        Dim strAssemblyShortName As String = e.Name.Split(","c)(0)
        Dim aryAssemblies As Assembly() = AppDomain.CurrentDomain.GetAssemblies
        For Each asy As Assembly In aryAssemblies
            If strAssemblyShortName = asy.FullName.Split(","c)(0) Then
                asyResult = asy
                Exit For
            End If
        Next
        Return asyResult
    End Function

#End Region

End Class
