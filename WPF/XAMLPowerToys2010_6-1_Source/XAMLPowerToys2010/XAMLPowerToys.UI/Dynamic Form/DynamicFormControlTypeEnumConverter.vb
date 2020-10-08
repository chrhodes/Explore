'
<ValueConversion(GetType(DynamicFormControlType), GetType(String))> Public Class DynamicFormControlTypeEnumConverter
    Implements IValueConverter

    Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
        Return CType(value, DynamicFormControlType).ToString
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
        Return CType([Enum].Parse(GetType(DynamicFormControlType), value.ToString), DynamicFormControlType)
    End Function

End Class
