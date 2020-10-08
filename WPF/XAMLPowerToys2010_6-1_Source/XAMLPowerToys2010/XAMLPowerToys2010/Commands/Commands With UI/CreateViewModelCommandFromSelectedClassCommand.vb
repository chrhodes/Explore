Imports System
Imports Microsoft.VisualStudio.CommandBars
Imports Extensibility
Imports EnvDTE
Imports EnvDTE80
Imports XAMLPowerToys.UI
Imports XAMLPowerToys.Model
Imports VSLangProj

Public Class CreateViewModelCommandFromSelectedClassCommand
    Inherits CommandBase

#Region " Constructors "

    Public Sub New(ByVal objAddInInstance As AddIn, ByVal objApplication As DTE2, ByVal intCounter As Integer)
        MyBase.new(objAddInInstance, objApplication, intCounter)
        MyBase.Caption = "Create ViewModel For Class"
        MyBase.CommandName = "CreateViewModelCommand" & intCounter.ToString
        MyBase.ToolTip = "Create ViewModel For Class"
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

            If objClassEntity IsNot Nothing Then

                Dim obj As New XAMLPowerToys.UI.CreateViewModelWindow(objClassEntity, MyBase.Application.ActiveWindow.Caption.EndsWith(".vb"))
                Dim bolResult As Nullable(Of Boolean) = obj.ShowDialog

                If bolResult.HasValue AndAlso bolResult.Value = True Then

                    Try
                        My.Computer.Clipboard.Clear()
                        My.Computer.Clipboard.SetText(obj.ViewModelText)

                    Catch ex As Exception
                        'Had to do this to avoid useless exception message when running this code in a VPC, since Vista & VPC and the Clipboard don't play nice together sometimes.
                        'the operation works, you just get an exception for no reason.
                    End Try

                    Utilities.ShowInformationMessage("Ready To Paste", "Position cursor inside a ViewModel file and execute a paste operation.")
                End If

            End If

        Catch ex As Exception
            Utilities.ShowExceptionMessage(Me.Caption, ex.Message, String.Empty, ex.ToString)
        End Try

    End Sub

    Public Overrides Function GetStatus() As EnvDTE.vsCommandStatus
        If MyBase.AddInInstance.DTE.ActiveDocument Is Nothing Then
            Return CType(vsCommandStatus.vsCommandStatusUnsupported + vsCommandStatus.vsCommandStatusInvisible, vsCommandStatus)
        ElseIf MyBase.AddInInstance.DTE.ActiveDocument.Name.EndsWith("vb") OrElse MyBase.AddInInstance.DTE.ActiveDocument.Name.EndsWith("cs") Then
            Return CType(vsCommandStatus.vsCommandStatusEnabled + vsCommandStatus.vsCommandStatusSupported, vsCommandStatus)
        Else
            Return CType(vsCommandStatus.vsCommandStatusUnsupported + vsCommandStatus.vsCommandStatusInvisible, vsCommandStatus)
        End If

    End Function

#End Region

End Class
