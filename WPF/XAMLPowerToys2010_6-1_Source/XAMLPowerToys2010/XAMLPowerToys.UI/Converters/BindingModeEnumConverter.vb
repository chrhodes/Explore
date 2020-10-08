'
<ValueConversion(GetType(BindingMode), GetType(String))> Public Class BindingModeEnumConverter
    Implements IValueConverter

    Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
        Return CType(value, BindingMode).ToString
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
        Return CType([Enum].Parse(GetType(BindingMode), value.ToString), BindingMode)
    End Function

End Class
