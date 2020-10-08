Imports System
Imports Microsoft.VisualStudio.CommandBars
Imports Extensibility
Imports EnvDTE
Imports EnvDTE80
Imports System.Text
Imports XAMLPowerToys.UI

Public MustInherit Class CommandBase
    Implements IDisposable

#Region " Declarations "

    Private _bolDisposedValue As Boolean = False
    Private _intCounter As Integer
    Private _objAddInInstance As AddIn
    Private _objApplication As DTE2
    Private _objCommandBaseCommandBarControl As CommandBarControl
    Private _strCaption As String = String.Empty
    Private _strCommandName As String = String.Empty
    Private _strToolTip As String = String.Empty

#End Region

#Region " Properties "

    Protected Property AddInInstance() As AddIn
        Get
            Return _objAddInInstance
        End Get
        Set(ByVal Value As AddIn)
            _objAddInInstance = Value
        End Set
    End Property

    Protected Property Application() As DTE2
        Get
            Return _objApplication
        End Get
        Set(ByVal Value As DTE2)
            _objApplication = Value
        End Set
    End Property

    Public Property Caption() As String
        Get
            Return _strCaption
        End Get
        Set(ByVal Value As String)
            _strCaption = Value
        End Set
    End Property

    Public Property CommandName() As String
        Get
            Return _strCommandName
        End Get
        Set(ByVal Value As String)
            _strCommandName = Value
        End Set
    End Property

    Public ReadOnly Property Counter() As Integer
        Get
            Return _intCounter
        End Get
    End Property

    Public Property ToolTip() As String
        Get
            Return _strToolTip
        End Get
        Set(ByVal Value As String)
            _strToolTip = Value
        End Set
    End Property

#End Region

#Region " Constructors "

    Public Sub New(ByVal objAddInInstance As AddIn, ByVal objApplication As DTE2, ByVal intCounter As Integer)
        _objAddInInstance = objAddInInstance
        _objApplication = objApplication
        _intCounter = intCounter
    End Sub

#End Region

#Region " Methods "

    Public Overridable Function CanExecute(ByVal enumExecuteOption As vsCommandExecOption) As Boolean
        Return enumExecuteOption = vsCommandExecOption.vsCommandExecOptionDoDefault
    End Function

    Public MustOverride Sub Execute()

    '
    Public Overridable Function GetStatus() As vsCommandStatus
        Return CType(vsCommandStatus.vsCommandStatusEnabled + vsCommandStatus.vsCommandStatusSupported, vsCommandStatus)
    End Function

    Public Sub RegisterCommandBarControl(ByVal objParentCommandBarPopup As CommandBarPopup)

        Dim objCommand As Command = Nothing

        Try
            objCommand = Me.Application.Commands.Item(GetFullCommandName(Me.CommandName))

        Catch ex As Exception
            'just ignore 
        End Try

        If objCommand Is Nothing Then
            objCommand = Me.Application.Commands.AddNamedCommand(Me.AddInInstance, Me.CommandName, String.Empty, String.Empty, False, 0, Nothing, vsCommandStatus.vsCommandStatusSupported Or vsCommandStatus.vsCommandStatusEnabled)
        End If

        _objCommandBaseCommandBarControl = CType(objCommand.AddControl(objParentCommandBarPopup.CommandBar, objParentCommandBarPopup.CommandBar.Controls.Count + 1), CommandBarControl)
        _objCommandBaseCommandBarControl.Caption = Me.Caption
        _objCommandBaseCommandBarControl.TooltipText = Me.ToolTip
    End Sub

#End Region

#Region " Helpers "

    Private Function GetFullCommandName(ByVal strCommandName As String) As String
        Return String.Concat(Me.AddInInstance.ProgID, ".", strCommandName)
    End Function

    Protected Sub GroupInto(ByVal strWrapperTop As String, ByVal strWrapperBottom As String)

        Dim objSelectedCodeBlock As TextSelection = CType(Me.Application.ActiveDocument.Selection, TextSelection)
        Dim objEditPoint As EditPoint = objSelectedCodeBlock.TopPoint.CreateEditPoint()
        Dim aryVbCrLf() As String = {vbCrLf}
        Dim strSelectedLines() As String = objSelectedCodeBlock.Text.Trim.Split(aryVbCrLf, StringSplitOptions.None)
        objSelectedCodeBlock.Delete()

        Dim sb As New StringBuilder(4096)
        sb.AppendLine(strWrapperTop)

        For Each s As String In strSelectedLines
            sb.AppendLine(s)
        Next

        sb.AppendLine(strWrapperBottom)
        objEditPoint.Insert(sb.ToString)
    End Sub

    Protected Function IsTextSelected() As Boolean

        If Me.Application.ActiveDocument IsNot Nothing AndAlso Me.Application.ActiveDocument.Selection IsNot Nothing Then

            Dim ts As TextSelection = TryCast(Me.Application.ActiveDocument.Selection, TextSelection)

            If ts IsNot Nothing Then
                Return ts.Text.Length > 0
            End If

        End If

        Return False
    End Function

    Protected Sub SetAllRowsAndColumnsToAuto(ByVal sb As StringBuilder)

        Dim intBeingSearchIndex As Integer
        Dim intIndex As Integer
        Dim intOpenIndex As Integer
        Dim intCloseIndex As Integer

        Do
            intIndex = sb.ToString.IndexOf("<RowDefinition Height=", intBeingSearchIndex)

            If intIndex > -1 Then
                intOpenIndex = intIndex + 23
                intBeingSearchIndex = intOpenIndex
                intCloseIndex = sb.ToString.IndexOf(Chr(34), intOpenIndex + 1)
                sb.Remove(intOpenIndex, intCloseIndex - intOpenIndex)
                sb.Insert(intOpenIndex, "Auto")

            Else
                Exit Do
            End If

        Loop

        intBeingSearchIndex = 0

        Do
            intIndex = sb.ToString.IndexOf("<ColumnDefinition Width=", intBeingSearchIndex)

            If intIndex > -1 Then
                intOpenIndex = intIndex + 25
                intBeingSearchIndex = intOpenIndex
                intCloseIndex = sb.ToString.IndexOf(Chr(34), intOpenIndex + 1)
                sb.Remove(intOpenIndex, intCloseIndex - intOpenIndex)
                sb.Insert(intOpenIndex, "Auto")

            Else
                Exit Do
            End If

        Loop

        sb.Replace("   ", " ").Replace("  ", " ")
        sb.Replace(" >", ">")
    End Sub

    Protected Sub StripUnWantedProperty(ByVal strPropertyToStrip As String, ByVal sb As StringBuilder)

        Dim intMarginIndex As Integer
        Dim intMarginOpenIndex As Integer
        Dim intMarginCloseIndex As Integer
        Dim bolMarginsRemoved As Boolean = False
        strPropertyToStrip = String.Concat(strPropertyToStrip, "=")

        Do
            intMarginIndex = sb.ToString.IndexOf(strPropertyToStrip, StringComparison.InvariantCultureIgnoreCase)

            If intMarginIndex > -1 Then
                intMarginOpenIndex = sb.ToString.IndexOf(Chr(34), intMarginIndex)

                If intMarginOpenIndex > intMarginIndex Then
                    intMarginCloseIndex = sb.ToString.IndexOf(Chr(34), intMarginOpenIndex + 1)

                    If intMarginCloseIndex > intMarginIndex Then
                        sb.Remove(intMarginIndex, intMarginCloseIndex - intMarginIndex + 1)
                        bolMarginsRemoved = True

                    Else
                        Exit Do
                    End If

                Else
                    Exit Do
                End If

            Else
                Exit Do
            End If

        Loop

        sb.Replace("   ", " ").Replace("  ", " ")
        sb.Replace(" >", ">")
    End Sub

#End Region

#Region " IDisposable "

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Sub Dispose(ByVal bolDisposing As Boolean)

        If Not _bolDisposedValue Then

            If bolDisposing Then

                If _objCommandBaseCommandBarControl IsNot Nothing Then
                    _objCommandBaseCommandBarControl.Delete()
                    _objCommandBaseCommandBarControl = Nothing
                End If

            End If

        End If

        _bolDisposedValue = True
    End Sub

#End Region

End Class
