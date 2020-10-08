Imports System
Imports Microsoft.VisualStudio.CommandBars
Imports Extensibility
Imports EnvDTE
Imports EnvDTE80
Imports XAMLPowerToys.UI

Public Class AboutCommand
    Inherits CommandBase

#Region " Constructors "

    Public Sub New(ByVal objAddInInstance As AddIn, ByVal objApplication As DTE2, ByVal intCounter As Integer)
        MyBase.new(objAddInInstance, objApplication, intCounter)
        MyBase.Caption = "About"
        MyBase.CommandName = "AboutCommand" & intCounter.ToString
        MyBase.ToolTip = "About XAML Power Toys"
    End Sub

#End Region

#Region " Methods "

    Public Overrides Sub Execute()

        Dim obj As New XAMLPowerToys.UI.AboutWindow
        obj.ShowDialog()
    End Sub

#End Region

End Class
