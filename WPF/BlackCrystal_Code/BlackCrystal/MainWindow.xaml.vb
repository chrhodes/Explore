Class MainWindow 

    Private Sub MainWindow_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded

    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim x As New System.Data.DataSet
        x.Tables.Add("tabla1")
        x.Tables(0).Columns.Add("Columna1")
        x.Tables(0).Columns.Add("Columna2")
        x.Tables(0).Columns.Add("Columna3")
        x.Tables(0).Columns.Add("Columna4")

        For i As Integer = 0 To 20
            x.Tables(0).Rows.Add(New String() {i.ToString, "prueba", i.ToString, "prueba"})
        Next
        x.AcceptChanges()
        Me.DataContext = x
    End Sub
End Class
