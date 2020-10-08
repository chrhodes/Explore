
Public NotInheritable Class Utilities
    Public Shared Function ShowExceptionMessage(ByVal strHeading As String, ByVal strMessage As String) As System.Windows.MessageBoxResult
        Return MessageBox.Show(strMessage, strHeading, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)
    End Function

    Public Shared Function ShowExceptionMessage(ByVal strHeading As String, ByVal strMessage As String, ByVal strFooter As String, ByVal strAdditionalDetails As String) As System.Windows.MessageBoxResult

#If DEBUG Then
        Return MessageBox.Show(strMessage & vbCrLf & vbCrLf & strFooter & vbCrLf & vbCrLf & strAdditionalDetails, strHeading, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)

#Else
        Return MessageBox.Show(strMessage & vbCrLf & vbCrLf & strFooter, strHeading, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)
#End If

    End Function

    Public Shared Function ShowInformationMessage(ByVal strHeading As String, ByVal strMessage As String) As System.Windows.MessageBoxResult
        Return MessageBox.Show(strMessage, strHeading, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK)
    End Function

    Public Shared Function ShowInformationMessage(ByVal strHeading As String, ByVal strMessage As String, ByVal strFooter As String, ByVal strAdditionalDetails As String) As System.Windows.MessageBoxResult

#If DEBUG Then
        Return MessageBox.Show(strMessage & vbCrLf & vbCrLf & strFooter & vbCrLf & vbCrLf & strAdditionalDetails, strHeading, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK)
#Else
        Return MessageBox.Show(strMessage & vbCrLf & vbCrLf & strFooter, strHeading, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK)
#End If

    End Function

End Class
