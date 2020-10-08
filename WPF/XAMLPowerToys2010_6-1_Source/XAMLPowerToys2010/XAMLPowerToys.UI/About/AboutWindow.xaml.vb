Partial Public Class AboutWindow

    Private Sub About_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Me.tbVersion.Text = String.Concat("Version: ", Version)
    End Sub

    Private Sub Blog_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        System.Diagnostics.Process.Start("http://karlshifflett.wordpress.com/xaml-power-toys/")
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        DialogResult = True
    End Sub

    Private Function Version() As String
        With System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location)
            Return String.Concat(.FileMajorPart, ".", .FileMinorPart, ".", .FileBuildPart, ".", .FilePrivatePart)
        End With
    End Function

End Class
