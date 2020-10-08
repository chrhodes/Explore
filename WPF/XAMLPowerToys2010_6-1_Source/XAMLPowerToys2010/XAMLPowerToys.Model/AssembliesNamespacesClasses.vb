'
<Serializable()> _
Public Class AssembliesNamespacesClasses
    Inherits List(Of AssembliesNamespacesClass)

    Public Sub New()

    End Sub

    Public ReadOnly Property SelectedItem As AssembliesNamespacesClass
        Get
            Return TryCast((From x In Me Where x.IsSelected = True Select x).SingleOrDefault, AssembliesNamespacesClass)
        End Get
    End Property

End Class
