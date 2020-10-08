Public Class Address

    Public Property Street As String = String.Empty
    Public Property StreetTwo As String = String.Empty
    Public Property City As String = String.Empty
    Public Property State As String = String.Empty
    Public Property Zip As String = String.Empty

    Public Sub New()

    End Sub

    Public Sub New(ByVal street As String, ByVal streetTwo As String, ByVal city As String, ByVal state As String, ByVal zip As String)
        _Street = street
        _StreetTwo = streetTwo
        _City = city
        _State = state
        _Zip = zip
    End Sub

End Class
