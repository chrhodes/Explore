
Public Class RequiredEntryValidationRule
    Inherits ValidationRule

    Public Overrides Function Validate(ByVal value As Object, ByVal cultureInfo As System.Globalization.CultureInfo) As System.Windows.Controls.ValidationResult

        If value Is Nothing OrElse String.IsNullOrEmpty(value.ToString) Then
            Return New ValidationResult(False, "This is a required entry field.")

        Else
            Return ValidationResult.ValidResult
        End If

    End Function

End Class
