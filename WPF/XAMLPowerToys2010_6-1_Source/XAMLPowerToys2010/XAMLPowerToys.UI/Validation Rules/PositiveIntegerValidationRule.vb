
Public Class PositiveIntegerValidationRule
    Inherits ValidationRule

    Public Overrides Function Validate(ByVal value As Object, ByVal cultureInfo As System.Globalization.CultureInfo) As System.Windows.Controls.ValidationResult

        If value Is Nothing OrElse value.ToString = String.Empty Then
            Return ValidationResult.ValidResult
        End If

        Dim intTest As Integer = -1

        If Not Integer.TryParse(value.ToString, intTest) OrElse intTest < 0 Then
            Return New ValidationResult(False, "Value must be a postive integer.")

        Else
            Return ValidationResult.ValidResult
        End If

    End Function

End Class
