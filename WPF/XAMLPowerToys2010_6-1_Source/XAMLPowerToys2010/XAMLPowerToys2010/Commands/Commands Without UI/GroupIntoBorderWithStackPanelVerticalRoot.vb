﻿Imports System
Imports Microsoft.VisualStudio.CommandBars
Imports Extensibility
Imports EnvDTE
Imports EnvDTE80
Imports XAMLPowerToys.UI

Public Class GroupIntoBorderWithStackPanelVerticalRoot
    Inherits CommandBase

#Region " Constructors "

    Public Sub New(ByVal objAddInInstance As AddIn, ByVal objApplication As DTE2, ByVal intCounter As Integer)
        MyBase.new(objAddInInstance, objApplication, intCounter)
        MyBase.Caption = "Border With Root StackPanel - Vertical"
        MyBase.CommandName = "GroupIntoBorderWithStackPanelVerticalRootCommand" & intCounter.ToString
        MyBase.ToolTip = "Group selection into a border with root stackpanel vertical being added."
    End Sub

#End Region

#Region " Methods "

    Public Overrides Function CanExecute(ByVal enumExecuteOption As EnvDTE.vsCommandExecOption) As Boolean
        Return MyBase.CanExecute(enumExecuteOption) AndAlso IsTextSelected()
    End Function

    Public Overrides Sub Execute()

        Try
            GroupInto(String.Concat("<Border>", vbCrLf, "<StackPanel>", vbCrLf), String.Concat("</StackPanel>", vbCrLf, "</Border>", vbCrLf))

        Catch ex As Exception
            Utilities.ShowExceptionMessage("Group Into " & Me.Caption, ex.Message, String.Empty, ex.ToString)
        End Try

    End Sub

    Public Overrides Function GetStatus() As EnvDTE.vsCommandStatus

        If IsTextSelected() Then
            Return CType(vsCommandStatus.vsCommandStatusEnabled + vsCommandStatus.vsCommandStatusSupported, vsCommandStatus)

        Else
            Return CType(vsCommandStatus.vsCommandStatusSupported, vsCommandStatus)
        End If

    End Function

#End Region

End Class
