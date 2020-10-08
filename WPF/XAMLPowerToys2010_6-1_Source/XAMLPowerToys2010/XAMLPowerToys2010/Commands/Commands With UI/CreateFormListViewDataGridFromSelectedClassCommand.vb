Imports System
Imports Microsoft.VisualStudio.CommandBars
Imports Extensibility
Imports EnvDTE
Imports EnvDTE80
Imports XAMLPowerToys.UI
Imports XAMLPowerToys.Model

Public Class CreateFormListViewDataGridFromSelectedClassCommand
    Inherits CommandBase

#Region " Constructors "

    Public Sub New(ByVal objAddInInstance As AddIn, ByVal objApplication As DTE2, ByVal intCounter As Integer)
        MyBase.new(objAddInInstance, objApplication, intCounter)
        MyBase.Caption = "Create Form, ListView or DataGrid From Selected Class"
        MyBase.CommandName = "CreateFormListViewDataGridFromSelectedClassCommand" & intCounter.ToString
        MyBase.ToolTip = "Create Form, ListView or DataGrid From Selected Class"
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

                Dim obj As New XAMLPowerToys.UI.CreateBusinessFormFromClassWindow(objClassEntity)
                Dim bolResult As Nullable(Of Boolean) = obj.ShowDialog

                If bolResult.HasValue AndAlso bolResult.Value = True Then

                    Try
                        My.Computer.Clipboard.Clear()
                        My.Computer.Clipboard.SetText(obj.BusinessForm)

                    Catch ex As Exception
                        'Had to do this to avoid useless exception message when running this code in a VPC, since Vista & VPC and the Clipboard don't play nice together sometimes.
                        'the operation works, you just get an exception for no reason.
                    End Try

                    Utilities.ShowInformationMessage("Ready To Paste", "Position cursor inside a XAML file in the XAML editor and execute a paste operation.")
                End If

            Else
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
