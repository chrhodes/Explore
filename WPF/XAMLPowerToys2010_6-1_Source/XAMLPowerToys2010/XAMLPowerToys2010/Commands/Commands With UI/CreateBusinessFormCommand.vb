Imports System
Imports Microsoft.VisualStudio.CommandBars
Imports Extensibility
Imports EnvDTE
Imports EnvDTE80
Imports XAMLPowerToys.UI
Imports XAMLPowerToys.Model
Imports System.Xml
Imports System.IO
Imports System.Text
Imports XAMLPowerToys.Helpers
'
Public Class CreateBusinessFormCommand
    Inherits CommandBase

#Region " Constructors "

    Public Sub New(ByVal objAddInInstance As AddIn, ByVal objApplication As DTE2, ByVal intCounter As Integer)
        MyBase.new(objAddInInstance, objApplication, intCounter)
        MyBase.Caption = "Create Business Form"
        MyBase.CommandName = "CreateBusinessFormCommand" & intCounter.ToString
        MyBase.ToolTip = "Create business form."
    End Sub

#End Region

#Region " Methods "

    Public Overrides Function CanExecute(ByVal enumExecuteOption As EnvDTE.vsCommandExecOption) As Boolean
        Return MyBase.CanExecute(enumExecuteOption) AndAlso Not IsTextSelected()
    End Function

    Public Overrides Sub Execute()

        Try

            Dim objClassEntity As ClassEntity = Nothing
            Dim aryGuids As String() = Helpers.GetProjectTypeGuids(Me.Application.ActiveDocument.ProjectItem.ContainingProject).Split(";"c)

            If IsProjectSilverlight(aryGuids) Then
                objClassEntity = New ClassEntity(String.Empty, True)
                objClassEntity.SilverlightVersion = Me.Application.ActiveDocument.ProjectItem.ContainingProject.Properties.Item("TargetFrameworkMoniker").Value.ToString.Replace("Silverlight,Version=v", String.Empty)
            End If

            Dim objCreateBusinessFormWindow As New CreateBusinessFormWindow(objClassEntity)
            Dim bolResult As Nullable(Of Boolean) = objCreateBusinessFormWindow.ShowDialog

            If bolResult.HasValue AndAlso bolResult.Value = True Then

                Dim ts As TextSelection = CType(Me.Application.ActiveDocument.Selection, TextSelection)
                ts.Insert(objCreateBusinessFormWindow.BusinessForm)
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
