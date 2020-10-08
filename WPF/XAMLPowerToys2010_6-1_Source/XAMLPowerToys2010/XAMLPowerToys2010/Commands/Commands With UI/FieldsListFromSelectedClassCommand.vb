Imports System
Imports Microsoft.VisualStudio.CommandBars
Imports Extensibility
Imports EnvDTE
Imports EnvDTE80
Imports XAMLPowerToys.UI
Imports XAMLPowerToys.Model

Public Class FieldsListFromSelectedClassCommand
    Inherits CommandBase

#Region " Constructors "

    Public Sub New(ByVal objAddInInstance As AddIn, ByVal objApplication As DTE2, ByVal intCounter As Integer)
        MyBase.new(objAddInInstance, objApplication, intCounter)
        MyBase.Caption = "Fields List For Selected Class"
        MyBase.CommandName = "FieldsListFromSelectedClassCommand" & intCounter.ToString
        MyBase.ToolTip = "Show fields list for selected class"
    End Sub

#End Region

#Region " Methods "

    Public Overrides Function CanExecute(ByVal enumExecuteOption As EnvDTE.vsCommandExecOption) As Boolean
        Return MyBase.CanExecute(enumExecuteOption) AndAlso Not IsTextSelected()
    End Function

    Public Overrides Sub Execute()

        Try

            Dim objRemoteTypeReflector As New RemoteTypeReflector
            Dim objClassEntity As ClassEntity = objRemoteTypeReflector.GetClassEntityFromSelectedClass(Me.Application.SelectedItems.Item(1).ProjectItem.ContainingProject, Me.Caption)

            If objClassEntity IsNot Nothing AndAlso objClassEntity.Success Then

                Dim obj As New XAMLPowerToys.UI.FieldsListWindow(objClassEntity)
                obj.Topmost = True
                obj.Show()
            End If

        Catch ex As Exception
            Utilities.ShowExceptionMessage(Me.Caption, ex.Message, String.Empty, ex.ToString)
        End Try

    End Sub

    Public Overrides Function GetStatus() As EnvDTE.vsCommandStatus

        If Not IsTextSelected() Then
            Return CType(vsCommandStatus.vsCommandStatusEnabled + vsCommandStatus.vsCommandStatusSupported, vsCommandStatus)

        Else
            Return CType(vsCommandStatus.vsCommandStatusSupported, vsCommandStatus)
        End If

    End Function

#End Region

End Class
