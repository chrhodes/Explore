Imports System.Reflection
Imports XAMLPowerToys.Model
Imports EnvDTE
Imports EnvDTE80
Imports System.IO
Imports XAMLPowerToys.UI
Imports XAMLPowerToys.Helpers
Imports System.Text
Imports VSLangProj
Imports System.Collections.Generic
Imports System.Runtime.Remoting.Lifetime
Imports XAMLPowerToys.ReflectionLoader
Imports System.Runtime.Remoting

Public Class RemoteTypeReflector

#Region " Declarations "

    Dim WithEvents _objSecondaryAppDomain As AppDomain = Nothing

#End Region

#Region " Constructors "

    Public Sub New()
    End Sub

#End Region

#Region " Methods "

    Private Function GetAssemblyInformation(ByVal objTargetProject As Project) As String

        If ((objTargetProject.Kind = PrjKind.prjKindVBProject) OrElse (objTargetProject.Kind = PrjKind.prjKindCSharpProject)) AndAlso Not IsProjectBlackListed(Helpers.GetProjectTypeGuids(objTargetProject).Split(";"c)) Then
            Return GetAssemblyPath(objTargetProject)
        End If

        Return String.Empty
    End Function

    Private Function GetProjectReferences(ByVal objTargetProject As Project) As List(Of String)

        Dim objList As New List(Of String)
        Dim objVSProject As VSProject = CType(objTargetProject.Object, VSProject)

        For Each objRef As Reference In objVSProject.References

            If XAMLPowerToys.UI.Helpers.IsMicrosoftAssembly(objRef.Name) Then
                Continue For
            End If

            If String.IsNullOrEmpty(objRef.Path) Then
                Utilities.ShowInformationMessage("Broken Reference Found", String.Format("The {0} reference is broken or unresolved.  It will be ingnored for now, but you need to correct it or removed the unused reference."))
                Continue For
            End If

            objList.Add(objRef.Path)
        Next

        Return objList
    End Function

    Public Function GetClassEntitiesForSelectedProject(ByVal objTargetProject As Project, ByVal strNameOfSourceCommand As String) As AssembliesNamespacesClasses

        Dim strAssemblyPath As String = GetAssemblyInformation(objTargetProject)

        If String.IsNullOrEmpty(strAssemblyPath) Then
            'this should never happen because the menu option would be disabled, however just in case
            Throw New Exception("The project associated with the selected file is either not vb, cs or is blacklisted")
        End If

        Dim objRemoteWorker As XAMLPowerToys.ReflectionLoader.RemoteWorker = Nothing
        Dim objRemoteResponse As RemoteResponse(Of AssembliesNamespacesClasses) = Nothing

        Try
            Dim objAppSetup As New AppDomainSetup
            objAppSetup.ApplicationBase = Path.GetDirectoryName(strAssemblyPath)
            objAppSetup.DisallowApplicationBaseProbing = False
            objAppSetup.ShadowCopyFiles = "True"

            _objSecondaryAppDomain = AppDomain.CreateDomain("SecondaryAppDomain", Nothing, objAppSetup)

            AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf _objSecondaryAppDomain_AssemblyResolve
            objRemoteWorker = TryCast(_objSecondaryAppDomain.CreateInstanceFromAndUnwrap(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly.Location), "XAMLPowerToys.ReflectionLoader.dll"), "XAMLPowerToys.ReflectionLoader.RemoteWorker"), XAMLPowerToys.ReflectionLoader.RemoteWorker)

            If objRemoteWorker IsNot Nothing Then

                Dim bolIsSilverlight As Boolean = IsProjectSilverlight(Helpers.GetProjectTypeGuids(objTargetProject).Split(";"c))
                objRemoteResponse = objRemoteWorker.GetClassEntityFromUserSelectedClass(strAssemblyPath, bolIsSilverlight, strNameOfSourceCommand, GetProjectReferences(objTargetProject))

                If objRemoteResponse.ResponseStatus <> ResponseStatus.Success Then
                    Utilities.ShowExceptionMessage("Unable To Reflect Type", "The following exception was returned. " & objRemoteResponse.CustomMessageAndException, String.Empty, objRemoteResponse.Exception.ToString)
                End If

            Else
                Utilities.ShowExceptionMessage("Unable To Create Worker", "Can't create Secondary AppDomain RemoteWorker class. CreateInstance and Unwrap methods returned Nothing.")
            End If

        Catch ex As FileNotFoundException
            Utilities.ShowExceptionMessage("File Not Found", "File not found." & vbCrLf & vbCrLf & "Have you built your application?" & vbCrLf & vbCrLf & ex.Message, String.Empty, ex.ToString)
        Catch ex As Exception
            Utilities.ShowExceptionMessage("Unable To Create Secondary AppDomain RemoteWorker", ex.Message, String.Empty, ex.ToString)
        Finally
            RemoveHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf _objSecondaryAppDomain_AssemblyResolve

            objRemoteWorker = Nothing

            If _objSecondaryAppDomain IsNot Nothing Then
                Try
                    AppDomain.Unload(_objSecondaryAppDomain)
                Catch ex As Exception
                    Utilities.ShowExceptionMessage("AppDomain.Unload Exception", ex.Message, String.Empty, ex.ToString)
                End Try

            End If
            _objSecondaryAppDomain = Nothing
        End Try

        If objRemoteResponse Is Nothing OrElse objRemoteResponse.ResponseStatus <> ResponseStatus.Success Then
            Return Nothing
        Else
            Return objRemoteResponse.Result
        End If
    End Function

    Public Function GetClassEntityFromSelectedClass(ByVal objTargetProject As Project, ByVal strNameOfSourceCommand As String) As XAMLPowerToys.Model.ClassEntity


        'TODO karl you left off here.  must ensure that the SL verions is added
        'Dim strSilverlightVersion As String = String.Empty
        'If bolIsSilverlight Then
        '    strSilverlightVersion = Me.Application.ActiveDocument.ProjectItem.ContainingProject.Properties.Item("TargetFrameworkMoniker").Value.ToString.Replace("Silverlight,Version=v", String.Empty)
        'End If


        Dim strAssemblyPath As String = GetAssemblyInformation(objTargetProject)

        If String.IsNullOrEmpty(strAssemblyPath) Then
            'this should never happen because the menu option would be disabled, however just in case
            Throw New Exception("The project associated with the selected file is either not vb, cs or is blacklisted")
        End If

        Dim objRemoteWorker As XAMLPowerToys.ReflectionLoader.RemoteWorker = Nothing
        Dim objRemoteResponse As RemoteResponse(Of AssembliesNamespacesClasses) = Nothing

        Try
            Dim objAppSetup As New AppDomainSetup
            objAppSetup.ApplicationBase = Path.GetDirectoryName(strAssemblyPath)
            objAppSetup.DisallowApplicationBaseProbing = False
            objAppSetup.ShadowCopyFiles = "True"

            _objSecondaryAppDomain = AppDomain.CreateDomain("SecondaryAppDomain", Nothing, objAppSetup)

            AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf _objSecondaryAppDomain_AssemblyResolve
            objRemoteWorker = TryCast(_objSecondaryAppDomain.CreateInstanceFromAndUnwrap(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly.Location), "XAMLPowerToys.ReflectionLoader.dll"), "XAMLPowerToys.ReflectionLoader.RemoteWorker"), XAMLPowerToys.ReflectionLoader.RemoteWorker)

            If objRemoteWorker IsNot Nothing Then

                Dim bolIsSilverlight As Boolean = IsProjectSilverlight(Helpers.GetProjectTypeGuids(objTargetProject).Split(";"c))
                objRemoteResponse = objRemoteWorker.GetClassEntityFromUserSelectedClass(strAssemblyPath, bolIsSilverlight, strNameOfSourceCommand, GetProjectReferences(objTargetProject))

                If objRemoteResponse.ResponseStatus <> ResponseStatus.Success Then
                    Utilities.ShowExceptionMessage("Unable To Reflect Type", "The following exception was returned. " & objRemoteResponse.CustomMessageAndException, String.Empty, objRemoteResponse.Exception.ToString)
                End If

            Else
                Utilities.ShowExceptionMessage("Unable To Create Worker", "Can't create Secondary AppDomain RemoteWorker class. CreateInstance and Unwrap methods returned Nothing.")
            End If

        Catch ex As FileNotFoundException
            Utilities.ShowExceptionMessage("File Not Found", "File not found." & vbCrLf & vbCrLf & "Have you built your application?" & vbCrLf & vbCrLf & ex.Message, String.Empty, ex.ToString)
        Catch ex As Exception
            Utilities.ShowExceptionMessage("Unable To Create Secondary AppDomain RemoteWorker", ex.Message, String.Empty, ex.ToString)
        Finally
            RemoveHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf _objSecondaryAppDomain_AssemblyResolve

            objRemoteWorker = Nothing

            If _objSecondaryAppDomain IsNot Nothing Then
                Try
                    AppDomain.Unload(_objSecondaryAppDomain)
                Catch ex As Exception
                    Utilities.ShowExceptionMessage("AppDomain.Unload Exception", ex.Message, String.Empty, ex.ToString)
                End Try

            End If
            _objSecondaryAppDomain = Nothing
        End Try

        If objRemoteResponse Is Nothing OrElse objRemoteResponse.ResponseStatus <> ResponseStatus.Success Then
            Return Nothing
        End If

        Dim frm As SelectClassFromAssembliesWindow = Nothing
        Dim objClassEntity As ClassEntity = Nothing

        frm = New SelectClassFromAssembliesWindow(objRemoteResponse.Result, strNameOfSourceCommand)

        If frm.ShowDialog() = True Then
            frm.SelectedAssemblyNamespaceClass.ClassEntity.Success = True
            If frm.SelectedAssemblyNamespaceClass.ClassEntity.IsSilverlight Then
                frm.SelectedAssemblyNamespaceClass.ClassEntity.SilverlightVersion = objTargetProject.Properties.Item("TargetFrameworkMoniker").Value.ToString.Replace("Silverlight,Version=v", String.Empty)
            End If

            Return frm.SelectedAssemblyNamespaceClass.ClassEntity
        Else
            Return Nothing
        End If

    End Function

    Private Function _objSecondaryAppDomain_AssemblyResolve(ByVal sender As Object, ByVal args As System.ResolveEventArgs) As System.Reflection.Assembly

        Dim strName As String = args.Name

        For Each asy As Assembly In AppDomain.CurrentDomain.GetAssemblies
            Dim strFoundName As String = asy.FullName

            If strFoundName = strName Then
                Return asy
            End If
        Next

        Return Nothing

    End Function

#End Region

End Class
