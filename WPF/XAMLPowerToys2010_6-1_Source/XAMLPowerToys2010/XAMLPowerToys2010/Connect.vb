Imports System
Imports Microsoft.VisualStudio.CommandBars
Imports Extensibility
Imports EnvDTE
Imports EnvDTE80
Imports System.Collections.Generic
Imports XAMLPowerToys.UI

Public Class Connect
    Implements IDTExtensibility2
    Implements IDTCommandTarget

#Region " Constants "

    Private Const STR_CODEWINDOW_CONTEXTMENU As String = "CodeWindowContextMenu"
    Private Const STR_XAMLEDITOR_CONTEXTMENU As String = "XAMLEditorContextMenu"
    Private Const STR_XAMLEDITOR_CONTEXTMENU_GROUPINTO_CONTEXTMENU As String = "XAMLEditorContextMenuGroupIntoContextMenu"
    Private Const STR_XAMLEDITOR_CONTEXTMENU_GROUPINTO_CONTEXTMENU_BORDER_CONTEXTMENU As String = "XAMLEditorContextMenuGroupIntoContextMenuBorderContextMenu"
    Private Const STR_XAMLEDITOR_CONTEXTMENU_TOOLS_CONTEXTMENU As String = "XAMLEditorContextMenuToolsContextMenu"

#End Region

#Region " Declarations "

    Private _intCounter As Integer
    Private _objAddInCommandBarPopups As New Dictionary(Of String, CommandBarPopup)
    Private _objAddInCommands As New Dictionary(Of String, CommandBase)
    Private _objAddInInstance As AddIn
    Private _objApplication As DTE2
    Private _objSolutionEvents As SolutionEvents

#End Region

#Region " Properties "

    Public ReadOnly Property AddInCommandBarPopups() As Dictionary(Of String, CommandBarPopup)
        Get
            Return _objAddInCommandBarPopups
        End Get
    End Property

    Public ReadOnly Property AddInCommands() As Dictionary(Of String, CommandBase)
        Get
            Return _objAddInCommands
        End Get
    End Property

    Public Property AddInInstance() As AddIn
        Get
            Return _objAddInInstance
        End Get
        Set(ByVal Value As AddIn)
            _objAddInInstance = Value
        End Set
    End Property

    Public Property Application() As DTE2
        Get
            Return _objApplication
        End Get
        Set(ByVal Value As DTE2)
            _objApplication = Value
        End Set
    End Property

#End Region

#Region " Constructors "

    Public Sub New()
    End Sub

#End Region

#Region " IDTExtensibility2 & IDTCommandTarget Standard Add-In Methods "

    Public Sub Exec(ByVal strCommandName As String, ByVal enumExecuteOption As vsCommandExecOption, ByRef varIn As Object, ByRef varOut As Object, ByRef bolHandled As Boolean) Implements IDTCommandTarget.Exec
        bolHandled = False

        Dim objCommandBase As CommandBase = Nothing

        If Me.AddInCommands.TryGetValue(strCommandName, objCommandBase) AndAlso objCommandBase.CanExecute(enumExecuteOption) Then
            objCommandBase.Execute()
            bolHandled = True
        End If

    End Sub

    Public Sub OnAddInsUpdate(ByRef custom As Array) Implements IDTExtensibility2.OnAddInsUpdate
    End Sub

    Public Sub OnBeginShutdown(ByRef custom As Array) Implements IDTExtensibility2.OnBeginShutdown
    End Sub

    Public Sub OnConnection(ByVal objApplication As Object, ByVal enumConnectMode As ext_ConnectMode, ByVal objAddInInstance As Object, ByRef aryCustom As Array) Implements IDTExtensibility2.OnConnection

        Try
            Me.Application = CType(objApplication, DTE2)
            Me.AddInInstance = CType(objAddInInstance, AddIn)


            Select Case enumConnectMode

                Case ext_ConnectMode.ext_cm_Startup

                    ' The add-in was marked to load on startup
                    ' Do nothing at this point because the IDE may not be fully initialized
                    ' VS will call OnStartupComplete when ready
                Case ext_ConnectMode.ext_cm_AfterStartup
                    ' The add-in was loaded after startup
                    ' Initialize it in the same way that when is loaded on startup calling manually this method:
                    OnStartupComplete(Nothing)
            End Select

        Catch ex As System.Exception
            Utilities.ShowExceptionMessage("OnConnection Error", ex.Message, String.Empty, ex.ToString)
        End Try

    End Sub

    Public Sub OnDisconnection(ByVal disconnectMode As ext_DisconnectMode, ByRef custom As Array) Implements IDTExtensibility2.OnDisconnection

        'this removes or deletes menus in the reverse order they were added.
        Dim objSortedList As New SortedList(Of Integer, CommandBase)

        For Each obj As CommandBase In Me.AddInCommands.Values
            objSortedList.Add(10000 - obj.Counter, obj)
        Next

        For Each obj As CommandBase In objSortedList.Values
            obj.Dispose()
        Next

        Dim objCommandBarPopupSortedList = New SortedList(Of Integer, CommandBarPopup)

        For Each obj In Me.AddInCommandBarPopups.Values
            objCommandBarPopupSortedList.Add(10000 - Integer.Parse(obj.Tag), obj)
        Next

        For Each obj As CommandBarPopup In objCommandBarPopupSortedList.Values
            obj.Delete()
        Next

        objSortedList.Clear()
        objSortedList = Nothing
        objCommandBarPopupSortedList.Clear()
        objCommandBarPopupSortedList = Nothing
        Me.AddInCommands.Clear()
        Me.AddInCommandBarPopups.Clear()
    End Sub

    Public Sub OnStartupComplete(ByRef custom As Array) Implements IDTExtensibility2.OnStartupComplete
        BuildMenu()
        'DumpProjectItems()
    End Sub

    Public Sub QueryStatus(ByVal strCommandName As String, ByVal enumNeededText As vsCommandStatusTextWanted, ByRef enumStatus As vsCommandStatus, ByRef objCommandText As Object) Implements IDTCommandTarget.QueryStatus

        Dim objCommandBase As CommandBase = Nothing

        If Me.AddInCommands.TryGetValue(strCommandName, objCommandBase) Then

            enumStatus = objCommandBase.GetStatus

        Else

            enumStatus = vsCommandStatus.vsCommandStatusUnsupported
        End If

    End Sub

#End Region

#Region " Menu "

    Private Sub BuildMenu()

        Dim objVisualStudioXAMLContextMenuCommandBar As CommandBar
        Dim objVisualStudioCodeWindowContextMenuCommandBar As CommandBar

        Try
            objVisualStudioXAMLContextMenuCommandBar = CType(Me.Application.CommandBars, CommandBars).Item("XAML Editor")
            If objVisualStudioXAMLContextMenuCommandBar Is Nothing Then
                Throw New Exception("Unable to get the 'XAML Editor' command bar.")
            End If

            objVisualStudioCodeWindowContextMenuCommandBar = CType(Me.Application.CommandBars, CommandBars).Item("Code Window")
            If objVisualStudioCodeWindowContextMenuCommandBar Is Nothing Then
                Throw New Exception("Unable to get the 'Code Editor' command bar.")
            End If

            '==================  VISUAL STUDIO CODE WINDOW MENU  ================== 
            'Add root level context menu item
            AddCommandBarPopup(objVisualStudioCodeWindowContextMenuCommandBar, STR_CODEWINDOW_CONTEXTMENU, "XAML Power Toys", 1)
            '
            '
            'Create ViewModel command
            AddCommand(Me.AddInCommandBarPopups(STR_CODEWINDOW_CONTEXTMENU), New CreateViewModelCommandFromSelectedClassCommand(Me.AddInInstance, Me.Application, GetNextCounter))
            '
            '
            '==================  VISUAL STUDIO XAML EDITOR MENU  ================== 
            'Add root level context menu item
            AddCommandBarPopup(objVisualStudioXAMLContextMenuCommandBar, STR_XAMLEDITOR_CONTEXTMENU, "XAML Power Toys", 1)
            '
            'Group Into context sub menu
            AddCommandBarSubPopup(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU), STR_XAMLEDITOR_CONTEXTMENU_GROUPINTO_CONTEXTMENU, "Group Into")
            'Group Into border context sub menu
            AddCommandBarSubPopup(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU_GROUPINTO_CONTEXTMENU), STR_XAMLEDITOR_CONTEXTMENU_GROUPINTO_CONTEXTMENU_BORDER_CONTEXTMENU, "Border")
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU_GROUPINTO_CONTEXTMENU_BORDER_CONTEXTMENU), New GroupIntoBorderNoChildRoot(Me.AddInInstance, Me.Application, GetNextCounter))
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU_GROUPINTO_CONTEXTMENU_BORDER_CONTEXTMENU), New GroupIntoBorderWithGridRoot(Me.AddInInstance, Me.Application, GetNextCounter))
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU_GROUPINTO_CONTEXTMENU_BORDER_CONTEXTMENU), New GroupIntoBorderWithStackPanelVerticalRoot(Me.AddInInstance, Me.Application, GetNextCounter))
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU_GROUPINTO_CONTEXTMENU_BORDER_CONTEXTMENU), New GroupIntoBorderWithStackPanelHorizontalRoot(Me.AddInInstance, Me.Application, GetNextCounter))
            '
            'continue adding group into commands
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU_GROUPINTO_CONTEXTMENU), New GroupIntoCanvas(Me.AddInInstance, Me.Application, GetNextCounter))
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU_GROUPINTO_CONTEXTMENU), New GroupIntoDockPanel(Me.AddInInstance, Me.Application, GetNextCounter))
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU_GROUPINTO_CONTEXTMENU), New GroupIntoGrid(Me.AddInInstance, Me.Application, GetNextCounter))
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU_GROUPINTO_CONTEXTMENU), New GroupIntoScrollViewer(Me.AddInInstance, Me.Application, GetNextCounter))
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU_GROUPINTO_CONTEXTMENU), New GroupIntoStackPanelVertical(Me.AddInInstance, Me.Application, GetNextCounter))
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU_GROUPINTO_CONTEXTMENU), New GroupIntoStackPanelHorizontal(Me.AddInInstance, Me.Application, GetNextCounter))
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU_GROUPINTO_CONTEXTMENU), New GroupIntoUniformGrid(Me.AddInInstance, Me.Application, GetNextCounter))
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU_GROUPINTO_CONTEXTMENU), New GroupIntoViewbox(Me.AddInInstance, Me.Application, GetNextCounter))
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU_GROUPINTO_CONTEXTMENU), New GroupIntoWrapPanel(Me.AddInInstance, Me.Application, GetNextCounter))
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU_GROUPINTO_CONTEXTMENU), New GroupIntoGroupBox(Me.AddInInstance, Me.Application, GetNextCounter))
            '
            '
            'Edit grid columns and rows
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU), New EditGridRowAndColumnsCommand(Me.AddInInstance, Me.Application, GetNextCounter))
            '
            '
            'Extract Selected Properties To Style
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU), New ExtractSelectedPropertiesToStyleCommand(Me.AddInInstance, Me.Application, GetNextCounter))
            '
            'Create business form
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU), New CreateBusinessFormCommand(Me.AddInInstance, Me.Application, GetNextCounter))
            '
            'Create business form from selected class
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU), New CreateFormListViewDataGridFromSelectedClassCommand(Me.AddInInstance, Me.Application, GetNextCounter))
            '
            'Fields List from selected class
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU), New FieldsListFromSelectedClassCommand(Me.AddInInstance, Me.Application, GetNextCounter))
            '
            'Remove all margins
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU), New RemoveMarginsCommand(Me.AddInInstance, Me.Application, GetNextCounter))
            '
            'Set Grid To Auto Layout
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU), New ChangeGridToFlowLayout(Me.AddInInstance, Me.Application, GetNextCounter))
            '
            'Chainsaw
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU), New ChainsawDesignerExtraProperties(Me.AddInInstance, Me.Application, GetNextCounter))
            '
            'Tools context sub menu
            AddCommandBarSubPopup(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU), STR_XAMLEDITOR_CONTEXTMENU_TOOLS_CONTEXTMENU, "Tools")

            'Tools menu commands
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU_TOOLS_CONTEXTMENU), New ControlDefaultsCommand(Me.AddInInstance, Me.Application, GetNextCounter))
            AddCommand(Me.AddInCommandBarPopups(STR_XAMLEDITOR_CONTEXTMENU_TOOLS_CONTEXTMENU), New AboutCommand(Me.AddInInstance, Me.Application, GetNextCounter))


        Catch ex As Exception
            Utilities.ShowExceptionMessage("BuildMenu Exception", ex.Message, String.Empty, ex.ToString)
        End Try

    End Sub

#End Region

#Region " Helpers "

    Private Sub AddCommand(ByVal objParentCommandBarPopup As CommandBarPopup, ByVal objNewCommand As CommandBase)
        Me.AddInCommands.Add(GetFullCommandName(objNewCommand.CommandName), objNewCommand)
        objNewCommand.RegisterCommandBarControl(objParentCommandBarPopup)
    End Sub

    Private Sub AddCommandBarPopup(ByVal objParentCommandBar As CommandBar, ByVal strContextMenuName As String, ByVal strCaption As String, ByVal intPosition As Integer)
        Me.AddInCommandBarPopups.Add(strContextMenuName, BuildCommandBarPopup(objParentCommandBar, strContextMenuName, strCaption, intPosition))
    End Sub

    Private Sub AddCommandBarSubPopup(ByVal objParentCommandBarPopup As CommandBarPopup, ByVal strContextMenuName As String, ByVal strCaption As String)
        Me.AddInCommandBarPopups.Add(strContextMenuName, BuildSubCommandBarPopup(objParentCommandBarPopup, strContextMenuName, strCaption))
    End Sub

    Private Function BuildCommandBarPopup(ByVal objParent As CommandBar, ByVal strCommandBarName As String, ByVal strCaption As String, ByVal intPosition As Integer) As CommandBarPopup

        Dim obj As CommandBarPopup = DirectCast(objParent.Controls.Add(Microsoft.VisualStudio.CommandBars.MsoControlType.msoControlPopup, System.Type.Missing, System.Type.Missing, intPosition, True), CommandBarPopup)
        obj.CommandBar.Name = strCommandBarName
        obj.Caption = strCaption
        obj.Tag = GetNextCounter.ToString
        Return obj
    End Function

    Private Function BuildSubCommandBarPopup(ByVal objParent As CommandBarPopup, ByVal strCommandBarName As String, ByVal strCaption As String) As CommandBarPopup

        Dim obj As CommandBarPopup = DirectCast(objParent.Controls.Add(Microsoft.VisualStudio.CommandBars.MsoControlType.msoControlPopup, System.Type.Missing, System.Type.Missing, objParent.Controls.Count + 1, True), CommandBarPopup)
        obj.CommandBar.Name = strCommandBarName
        obj.Caption = strCaption
        obj.Tag = GetNextCounter.ToString
        Return obj
    End Function

    Private Function GetFullCommandName(ByVal strCommandName As String) As String
        Return String.Concat(Me.AddInInstance.ProgID, ".", strCommandName)
    End Function

    Private Function GetNextCounter() As Integer
        _intCounter += 1
        Return _intCounter
    End Function

#End Region

#Region " Dump CommandBars - Extra Goodie "

    '''' <summary>
    ''''     This method will dump all menus and their clild items to a disk file.
    ''''     Commented out for so that it does not run without the developers knowledge
    '''' </summary>
    'Private Sub DumpCommandBars()
    '    Dim intX As Integer = 1
    '    System.IO.File.WriteAllText("c:\DumpAllCommandBars.txt", vbCrLf)
    '    For Each obj As CommandBar In CType(Application.CommandBars, CommandBars)
    '        System.IO.File.AppendAllText("c:\DumpAllCommandBars.txt", obj.Name & " " & intX.ToString & vbCrLf)
    '        For Each objChild2 As CommandBarControl In obj.Controls
    '            System.IO.File.AppendAllText("c:\DumpAllCommandBars.txt", "    " & objChild2.Caption & vbCrLf)
    '        Next
    '    Next
    'End Sub


    'Private Sub DumpProjectItem(ByVal pi As ProjectItem)
    '    If pi.FileCount > 0 Then
    '        If pi.Kind = "{6BB5F8EE-4483-11D3-8BCF-00C04F8EC28C}" Then
    '            For intX As Short = 1 To pi.FileCount
    '                System.IO.File.AppendAllText("c:\DumpProjectItems.txt", String.Format("Name: {0}  FileName: {1}", pi.Name, pi.FileNames(intX)) & vbCrLf & vbCrLf)
    '                If pi.Name.EndsWith(".cs") Then
    '                    Dim window As Window = pi.Open(Constants.vsViewKindCode)
    '                    window.Activate()
    '                Else
    '                    pi.Open(EnvDTE.Constants.vsViewKindPrimary)
    '                End If
    '            Next
    '        End If
    '    End If

    '    If pi.ProjectItems.Count > 0 Then
    '        For Each npi As ProjectItem In pi.ProjectItems
    '            DumpProjectItem(npi)
    '        Next
    '    End If
    'End Sub

    'Private Sub DumpProjectItems()
    '    System.IO.File.WriteAllText("c:\DumpProjectItems.txt", vbCrLf)

    '    Dim proj As Project = CType(Me.Application.ActiveSolutionProjects(0), Project)
    '    For Each pi As ProjectItem In proj.ProjectItems
    '        DumpProjectItem(pi)
    '    Next
    'End Sub


#End Region

End Class
