Imports System
Imports Microsoft.VisualStudio.CommandBars
Imports Extensibility
Imports EnvDTE
Imports EnvDTE80
Imports XAMLPowerToys.UI

Public Class ControlDefaultsCommand
    Inherits CommandBase

#Region " Constructors "

    Public Sub New(ByVal objAddInInstance As AddIn, ByVal objApplication As DTE2, ByVal intCounter As Integer)
        MyBase.new(objAddInInstance, objApplication, intCounter)
        MyBase.Caption = "Set Control Defaults"
        MyBase.CommandName = "SetControlDefaultsCommand" & intCounter.ToString
        MyBase.ToolTip = "Set control defaults for controls created by this software."
    End Sub

#End Region

#Region " Methods "

    Public Overrides Sub Execute()

        Dim obj As New XAMLPowerToys.UI.UIControlDefaultsWindow
        obj.ShowDialog()
        obj = Nothing
    End Sub

#End Region

End Class
