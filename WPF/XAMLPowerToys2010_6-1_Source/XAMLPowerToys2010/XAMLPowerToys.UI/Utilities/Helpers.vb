Imports System.Text
Imports System.ComponentModel
Imports XAMLPowerToys.Model

Public Class Helpers

#Region " Constructors "

    Public Sub New()
    End Sub

#End Region

#Region " Methods "

    Public Shared Function IsMicrosoftAssembly(ByVal strAssemblyName As String) As Boolean
        'second copy in XAMLPowerToys.ReflectionLoader
        strAssemblyName = strAssemblyName.ToLower

        If strAssemblyName.StartsWith("system") Then Return True

        If strAssemblyName.StartsWith("mscorlib") Then Return True

        If strAssemblyName.StartsWith("presentationframework") Then Return True

        If strAssemblyName.StartsWith("presentationcore") Then Return True

        If strAssemblyName.StartsWith("microsoft") Then Return True

        If strAssemblyName.StartsWith("windowsbase") Then Return True

        If strAssemblyName.StartsWith("wpftoolkit") Then Return True

        If strAssemblyName.StartsWith("uiautomationprovider") Then Return True

        Return False
    End Function

    Public Shared Function DynamicFormEditorFactory(ByVal pi As PropertyInformation) As DynamicFormEditor

        Dim objListBoxContent As New DynamicFormListBoxContent
        With objListBoxContent
            .AssociatedLabel = Helpers.ParsePropertyNameForLabel(pi.Name)
            .CanWrite = pi.CanWrite

            If pi.CanWrite Then
                .BindingMode = BindingMode.TwoWay

            Else
                .BindingMode = BindingMode.OneWay
            End If

            .BindingPath = pi.Name

            If pi.TypeName.IndexOf("Boolean") = -1 Then

                If pi.CanWrite Then
                    .ControlType = DynamicFormControlType.TextBox

                Else
                    .ControlType = DynamicFormControlType.TextBlock
                End If

            Else
                .ControlType = DynamicFormControlType.CheckBox
            End If

            .DataType = pi.TypeName
            .TypeNamespace = pi.TypeNamespace

            If .DataType.Contains("Int32") Then
                .DataType = "Integer"

            ElseIf .DataType.Contains("Int16") Then
                .DataType = "Short"

            ElseIf .DataType.Contains("Int64") Then
                .DataType = "Long"
            End If

            If pi.TypeName.Contains("Decimal") Then
                .StringFormat = "{0:c}"

            ElseIf pi.TypeName.Contains("Date") Then
                .StringFormat = "{0:d}"

            Else
                .StringFormat = String.Empty
            End If

        End With
        Return New DynamicFormEditor With {.DataContext = objListBoxContent}
    End Function

    Public Shared Function FindAncestorWindow(ByVal objDependencyObject As DependencyObject) As Window

        While (objDependencyObject) IsNot Nothing
            objDependencyObject = VisualTreeHelper.GetParent(objDependencyObject)

            If TypeOf objDependencyObject Is Window Then
                Exit While
            End If

        End While

        Return TryCast(objDependencyObject, Window)
    End Function

    Public Shared Function GetSampleFormats() As ListCollectionView

        Dim obj As New List(Of SampleFormat)
        obj.Add(New SampleFormat("Date", "12/25/1965", "{0:d}"))
        obj.Add(New SampleFormat("Date", "Saturday, December 25, 1965", "{0:D}"))
        obj.Add(New SampleFormat("Date", "Saturday, December 25, 1965 7:25 AM", "{0:f}"))
        obj.Add(New SampleFormat("Date", "Saturday, December 25, 1965 7:25:42 AM", "{0:F}"))
        obj.Add(New SampleFormat("Date", "12/25/1965 7:25 AM", "{0:g}"))
        obj.Add(New SampleFormat("Date", "12/25/1965 7:25:42 AM", "{0:G}"))
        obj.Add(New SampleFormat("Date", "December 25", "{0:M}"))
        obj.Add(New SampleFormat("Date", "Sat, 25 Dec 1965 7:25:42 GMT", "{0:R}"))
        obj.Add(New SampleFormat("Double, Decimal", "$75,234.89", "{0:c}"))
        obj.Add(New SampleFormat("Double, Decimal", "75234.89", "{0:F}"))
        obj.Add(New SampleFormat("Double, Decimal", "75234", "{0:F0}"))
        obj.Add(New SampleFormat("Double, Decimal", "75234.8933", "{0:F4}"))
        obj.Add(New SampleFormat("Double, Decimal", "75,234.89", "{0:N}"))
        obj.Add(New SampleFormat("Double, Decimal", "75,234.8933", "{0:N4}"))
        obj.Add(New SampleFormat("Double, Decimal", "7,523,489.33 %", "{0:P}"))
        obj.Add(New SampleFormat("Integer, Short", "42", "{0:D}"))
        obj.Add(New SampleFormat("Integer, Short", "00042", "{0:D5}"))
        obj.Add(New SampleFormat("Integer, Short", "42", "{0:F0}"))
        obj.Add(New SampleFormat("Integer, Short", "2a", "{0:x}"))
        obj.Add(New SampleFormat("Integer, Short", "2A", "{0:X}"))
        obj.Add(New SampleFormat("Integer, Short", "00002A", "{0:X6}"))

        Dim objSampleFormatList As New ListCollectionView(obj)
        objSampleFormatList.GroupDescriptions.Add(New PropertyGroupDescription("DataType"))
        objSampleFormatList.SortDescriptions.Add(New SortDescription("DataType", ListSortDirection.Ascending))
        objSampleFormatList.SortDescriptions.Add(New SortDescription("Example", ListSortDirection.Ascending))
        Return objSampleFormatList
    End Function

    Public Function GetSortedEnumNames(ByVal t As Type) As String()

        If t.IsEnum Then

            Dim strOut() As String = [Enum].GetNames(t)
            Array.Sort(strOut)
            Return strOut

        Else
            Throw New ArgumentException("Must be an enum", "t")
        End If

    End Function

    Public Shared Function ParsePropertyNameForLabel(ByVal strToParse As String) As String

        Dim sb As New StringBuilder(256)
        Dim bolFoundUpperCase As Boolean = False
        Dim bolOnlyUpperCase As Boolean = True

        For intX As Integer = 0 To strToParse.Length - 1

            If Not Char.IsUpper(strToParse, intX) Then
                bolOnlyUpperCase = False
                Exit For
            End If

        Next

        If bolOnlyUpperCase Then
            Return strToParse
        End If

        For intX As Integer = 0 To strToParse.Length - 1

            If Not bolFoundUpperCase AndAlso Char.IsUpper(strToParse, intX) Then
                bolFoundUpperCase = True

                If intX = 0 Then
                    sb.Append(strToParse.Substring(intX, 1))

                Else
                    sb.Append(" ")
                    sb.Append(strToParse.Substring(intX, 1))
                End If

                Continue For
            End If

            If Not bolFoundUpperCase Then
                Continue For
            End If

            If Char.IsUpper(strToParse, intX) Then
                sb.Append(" ")
                sb.Append(strToParse.Substring(intX, 1))

            ElseIf Char.IsLetterOrDigit(strToParse, intX) Then
                sb.Append(strToParse.Substring(intX, 1))
            End If

        Next

        Return sb.ToString
    End Function

#End Region

End Class
