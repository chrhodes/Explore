Imports System.ComponentModel
Imports XAMLPowerToys.Model

Partial Public Class SelectClassFromAssembliesWindow

#Region " Declarations "

    Private _objAssemblyNamespaceClassCollectionView As CollectionView
    Private _objSelectedAssemblyNamespaceClass As AssembliesNamespacesClass

#End Region

#Region " Constructors "

    Public Sub New(ByVal objAssembliesNamespacesClass As AssembliesNamespacesClasses, ByVal strNameOfSourceCommand As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim cvs As New CollectionViewSource
        _objAssemblyNamespaceClassCollectionView = CType(CollectionViewSource.GetDefaultView(objAssembliesNamespacesClass), CollectionView)
        With _objAssemblyNamespaceClassCollectionView
            .GroupDescriptions.Clear()
            .SortDescriptions.Clear()
            .GroupDescriptions.Add(New PropertyGroupDescription("AssemblyName"))
            .GroupDescriptions.Add(New PropertyGroupDescription("Namespace"))
            .SortDescriptions.Add(New SortDescription("AssemblyName", ListSortDirection.Ascending))
            .SortDescriptions.Add(New SortDescription("Namespace", ListSortDirection.Ascending))
            .SortDescriptions.Add(New SortDescription("TypeName", ListSortDirection.Ascending))
        End With
        Me.tvObjects.ItemsSource = _objAssemblyNamespaceClassCollectionView.Groups
        Me.tbCommandCaption.Text = String.Concat("For ", strNameOfSourceCommand)
    End Sub

#End Region

#Region " Methods "

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.DialogResult = False
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.DialogResult = True
    End Sub

    Private Sub tvObjects_SelectedItemChanged(ByVal sender As Object, ByVal e As System.Windows.RoutedPropertyChangedEventArgs(Of Object)) Handles tvObjects.SelectedItemChanged

        If TypeOf e.NewValue Is AssembliesNamespacesClass Then
            Me.btnNext.IsEnabled = True
            _objSelectedAssemblyNamespaceClass = CType(e.NewValue, AssembliesNamespacesClass)

        Else
            Me.btnNext.IsEnabled = False
            _objSelectedAssemblyNamespaceClass = Nothing
        End If

    End Sub

    Public ReadOnly Property SelectedAssemblyNamespaceClass() As AssembliesNamespacesClass
        Get
            Return _objSelectedAssemblyNamespaceClass
        End Get
    End Property

#End Region

End Class
