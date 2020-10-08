Imports System
Imports Microsoft.VisualStudio.CommandBars
Imports Extensibility
Imports EnvDTE
Imports EnvDTE80
Imports XAMLPowerToys.UI

Public Class ChainsawDesignerExtraProperties
    Inherits CommandBase

#Region " Constructors "

    Public Sub New(ByVal objAddInInstance As AddIn, ByVal objApplication As DTE2, ByVal intCounter As Integer)
        MyBase.new(objAddInInstance, objApplication, intCounter)
        MyBase.Caption = "Chainsaw - Minimize Designer XAML"
        MyBase.CommandName = "ChainsawDesignerExtraPropertiesCommand" & intCounter.ToString
        MyBase.ToolTip = "Use this function only on designer generated XAML.  Warning this thing is a chainsaw!"
    End Sub

#End Region

#Region " Methods "

    Public Overrides Function CanExecute(ByVal enumExecuteOption As EnvDTE.vsCommandExecOption) As Boolean
        Return MyBase.CanExecute(enumExecuteOption) AndAlso IsTextSelected()
    End Function

    Public Overrides Sub Execute()

        Try

            If MessageBox.Show("This chainsaw will remove all Margins, MinHeights, MinWidths, x:Name and Name properties and set all rows and columns to Auto size.", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then

                Dim objSelectedCodeBlock As TextSelection = CType(Me.Application.ActiveDocument.Selection, TextSelection)
                Dim sb As New System.Text.StringBuilder(objSelectedCodeBlock.Text.TrimStart(Chr(10), Chr(13), Chr(9), Chr(32)).TrimEnd(Chr(10), Chr(13), Chr(9), Chr(32)))
                StripUnWantedProperty("Margin", sb)
                StripUnWantedProperty("MinHeight", sb)
                StripUnWantedProperty("MinWidth", sb)
                StripUnWantedProperty("x:Name", sb)
                StripUnWantedProperty("Name", sb)
                SetAllRowsAndColumnsToAuto(sb)

                Dim objEditPoint As EditPoint = objSelectedCodeBlock.TopPoint.CreateEditPoint()
                objSelectedCodeBlock.Delete()
                objEditPoint.Insert(sb.ToString)
            End If

        Catch ex As Exception
            Utilities.ShowExceptionMessage(Me.Caption, ex.Message, String.Empty, ex.ToString)
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
