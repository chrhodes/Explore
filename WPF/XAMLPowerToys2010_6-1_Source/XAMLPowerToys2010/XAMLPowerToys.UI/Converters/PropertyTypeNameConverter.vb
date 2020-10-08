'
<ValueConversion(GetType(String), GetType(String))> Public Class PropertyTypeNameConverter
    Implements IValueConverter

    Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert

        If value Is Nothing OrElse String.IsNullOrEmpty(value.ToString) Then
            Return String.Empty

        Else
            Return String.Format("Data Type - {0}       ", value.ToString)
        End If

    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
        Throw New NotSupportedException
    End Function

End Class
