Imports Mono.Cecil
Imports System.IO
Imports System.Runtime.Remoting.Lifetime
Imports XAMLPowerToys.Model

'
''' <summary>
''' This class uses the Mono.Cecil.dll to reflect all types visible by the 
''' </summary>
''' <remarks></remarks>
Public Class RemoteWorker
    Inherits MarshalByRefObject

#Region " Constructor "

    Public Sub New()
    End Sub

#End Region

#Region " Methods "

    Private Function GetAssemblyFullPath(ByVal strTargetProjectPath As String, ByVal strAssemblyName As String) As String

        If File.Exists(Path.Combine(strTargetProjectPath, strAssemblyName & ".dll")) Then
            Return Path.Combine(strTargetProjectPath, strAssemblyName & ".dll")
        End If

        If File.Exists(Path.Combine(strTargetProjectPath, strAssemblyName & ".exe")) Then
            Return Path.Combine(strTargetProjectPath, strAssemblyName & ".exe")
        End If

        Return ""
    End Function


    Public Function GetClassEntityFromUserSelectedClass(ByVal strAssemblyPath As String, ByVal bolIsSilverlight As Boolean, ByVal strNameOfSourceCommand As String, ByVal objReferencesList As List(Of String)) As RemoteResponse(Of AssembliesNamespacesClasses)

        Dim objData As New AssembliesNamespacesClasses
        Dim strTargetProjectPath As String = Path.GetDirectoryName(strAssemblyPath)
        Dim asyTargetAssemblyDefinition As AssemblyDefinition = AssemblyFactory.GetAssembly(strAssemblyPath)
        Dim ex As Exception = Nothing

        Dim htAssembliesToLoad As New Hashtable

        'load this assembly
        htAssembliesToLoad.Add(strAssemblyPath.ToLower, Nothing)

        'load up all referenced assemblies for above strAssemblyPath
        For Each asyName As AssemblyNameReference In asyTargetAssemblyDefinition.MainModule.AssemblyReferences
            If Not IsMicrosoftAssembly(asyName.Name) Then
                Dim strAssemblyFullPath As String = GetAssemblyFullPath(strTargetProjectPath, asyName.Name)
                If Not String.IsNullOrEmpty(strAssemblyFullPath) AndAlso Not htAssembliesToLoad.ContainsKey(strAssemblyFullPath.ToLower) Then
                    htAssembliesToLoad.Add(strAssemblyFullPath.ToLower, Nothing)
                End If
            End If
        Next

        'load up all assemblies referenced in the project but that are not loaded yet.
        For Each strName As String In objReferencesList
            If Not htAssembliesToLoad.ContainsKey(Path.GetFileName(strName.ToLower)) Then
                htAssembliesToLoad.Add(strName.ToLower, Nothing)
            End If
        Next

        For Each strAsyPath As Object In htAssembliesToLoad.Keys
            'load target assembly
            asyTargetAssemblyDefinition = AssemblyFactory.GetAssembly(strAsyPath.ToString)
            LoadAssemblyClasses(asyTargetAssemblyDefinition, bolIsSilverlight, objData, ex, htAssembliesToLoad)

            If ex IsNot Nothing Then
                Return New RemoteResponse(Of AssembliesNamespacesClasses)(Nothing, ResponseStatus.Exception, ex, String.Format("Unable to load types from target assembly: ", asyTargetAssemblyDefinition.Name))
            End If
        Next

        Return New RemoteResponse(Of AssembliesNamespacesClasses)(objData, ResponseStatus.Success, Nothing, Nothing)

    End Function

    Private Function CanWrite(ByVal objProperty As PropertyDefinition) As Boolean

        If objProperty.SetMethod Is Nothing Then Return False

        If objProperty.SetMethod.IsPublic Then Return True

        Return False
    End Function

    ''' <summary>
    ''' Yep, this sucks because objType.BaseType can have different types in it.
    ''' The base type may or may not be in an assembly we have loaded.  However, as long as it is referenced and we have a path to it,
    ''' it will be loaded and the base type properites added to the list for the TypeDefinition.
    ''' This function also recurses to get the type and all its base classes.
    ''' </summary>
    ''' <param name="asy">This is the assembly definition for the target TypeDefinition</param>
    ''' <param name="objType">TypeDefinition to get properties for</param>
    ''' <param name="htAssembliesToLoad">This is has table of all referenced and solution assemblies.  It holds the paths to each assembly so it can be loaded if required.</param>
    ''' <returns>All properties for the TypedDefinition loaded in a List(Of PropertyDefinition)</returns>
    Private Function GetAllProperitesForType(ByVal asy As AssemblyDefinition, ByVal objType As TypeDefinition, ByVal htAssembliesToLoad As Hashtable) As List(Of PropertyDefinition)

        Dim returnValue As New List(Of PropertyDefinition)
        For Each item As PropertyDefinition In objType.Properties
            returnValue.Add(item)
        Next

        If objType.BaseType IsNot Nothing AndAlso Not objType.BaseType Is objType.Module.Import(GetType(Object)) AndAlso objType.BaseType.Scope IsNot Nothing Then

            Dim strBaseTypeAssemblyName As String = Nothing

            Dim td As TypeDefinition = TryCast(objType.BaseType, TypeDefinition)
            If td IsNot Nothing Then
                Dim md As ModuleDefinition = TryCast(td.Scope, ModuleDefinition)
                If md IsNot Nothing Then
                    strBaseTypeAssemblyName = md.Name.ToLower
                End If
            End If

            If strBaseTypeAssemblyName Is Nothing Then
                Dim anr As AssemblyNameReference = TryCast(objType.BaseType.Scope, AssemblyNameReference)
                If anr IsNot Nothing Then
                    strBaseTypeAssemblyName = anr.Name.ToLower
                End If
            End If

            If Not String.IsNullOrWhiteSpace(strBaseTypeAssemblyName) Then

                Dim asyTargetAssemblyDefinition As AssemblyDefinition = Nothing

                For Each strAsyName In htAssembliesToLoad.Keys
                    If strAsyName.ToString.EndsWith(strBaseTypeAssemblyName) OrElse strAsyName.ToString.IndexOf(strBaseTypeAssemblyName) > -1 Then
                        asyTargetAssemblyDefinition = AssemblyFactory.GetAssembly(strAsyName.ToString)
                        Exit For
                    End If
                Next

                If asyTargetAssemblyDefinition IsNot Nothing Then
                    For Each objBaseTypeDefinition As TypeDefinition In asyTargetAssemblyDefinition.MainModule.Types
                        If objBaseTypeDefinition.IsClass AndAlso objBaseTypeDefinition.Name = objType.BaseType.Name Then
                            returnValue.AddRange(GetAllProperitesForType(asy, objBaseTypeDefinition, htAssembliesToLoad))
                            Exit For
                        End If
                    Next
                End If
            End If
        End If

        Return returnValue
    End Function

    Private Sub LoadAssemblyClasses(ByVal asy As AssemblyDefinition, ByVal bolIsSilverlight As Boolean, ByVal objData As List(Of AssembliesNamespacesClass), ByRef exOut As Exception, ByVal htAssembliesToLoad As Hashtable)

        Try

            For Each objType As TypeDefinition In asy.MainModule.Types

                If objType.IsPublic AndAlso objType.IsClass AndAlso Not objType.IsAbstract AndAlso Not objType.Name.Contains("<Module>") AndAlso Not objType.Name.Contains("AnonymousType") AndAlso Not objType.Name.StartsWith("_") AndAlso Not objType.Name.EndsWith("AssemblyInfo") Then

                    Dim bolPreviouslyLoaded As Boolean = False

                    For Each anc As AssembliesNamespacesClass In objData
                        If objType.Name = anc.TypeName AndAlso objType.Namespace = anc.Namespace AndAlso asy.Name.Name = anc.AssemblyName Then
                            bolPreviouslyLoaded = True
                            Exit For
                        End If
                    Next

                    If Not bolPreviouslyLoaded Then

                        If objType.BaseType Is Nothing OrElse objType.BaseType.Name <> "MulticastDelegate" Then

                            Dim objClassEntity As New ClassEntity(objType.Name, bolIsSilverlight)

                            'Original code, now been replaced by following line
                            'For Each objProperty As PropertyDefinition In objType.Properties
                            For Each objProperty As PropertyDefinition In GetAllProperitesForType(asy, objType, htAssembliesToLoad)

                                If objProperty.GetMethod IsNot Nothing AndAlso objProperty.GetMethod.IsPublic Then
                                    Dim pi As New PropertyInformation(CanWrite(objProperty), objProperty.Name, FormatPropertyTypeName(objProperty), objProperty.PropertyType.Namespace)

                                    If objProperty.PropertyType IsNot Nothing AndAlso TypeOf objProperty.PropertyType Is Mono.Cecil.GenericInstanceType Then
                                        Dim obj As Mono.Cecil.GenericInstanceType = CType(objProperty.PropertyType, Mono.Cecil.GenericInstanceType)
                                        If obj.HasGenericArguments Then
                                            For Each tr As TypeReference In obj.GenericArguments
                                                pi.GenericArguments.Add(tr.Name)
                                            Next
                                        End If
                                    End If

                                    If objProperty.HasParameters Then
                                        For Each pd As ParameterDefinition In objProperty.Parameters
                                            pi.PropertyParameters.Add(New PropertyParameter(pd.Name, pd.ParameterType.Name))
                                        Next
                                    End If
                                    objClassEntity.PropertyInfomation.Add(pi)
                                End If

                            Next

                            objData.Add(New AssembliesNamespacesClass(asy.Name.Name, objType.Namespace, objType.Name, objClassEntity))
                        End If
                    End If
                End If

            Next

        Catch ex As Exception
            exOut = ex
        End Try

    End Sub

    Private Function FormatPropertyTypeName(ByVal objProperty As PropertyDefinition) As String
        Dim strName As String = objProperty.PropertyType.Name
        Dim strFullName As String = objProperty.PropertyType.FullName

        If Not strName.Contains("`") Then
            Return strName
        End If

        strName = strName.Remove(strName.IndexOf("`"), 2)

        If objProperty.PropertyType Is Nothing OrElse Not TypeOf objProperty.PropertyType Is Mono.Cecil.GenericInstanceType OrElse strFullName.IndexOf(">") = -1 Then
            Return strName
        End If

        Dim sb As New System.Text.StringBuilder(512)
        sb.AppendFormat("{0}(Of ", strName)

        Dim obj As Mono.Cecil.GenericInstanceType = CType(objProperty.PropertyType, Mono.Cecil.GenericInstanceType)
        If obj.HasGenericArguments Then
            For Each tr As TypeReference In obj.GenericArguments
                sb.Append(tr.Name)
                sb.Append(", ")
            Next

        Else

            Return strName
        End If

        sb.Length = sb.Length - 2
        sb.Append(")")
        Return sb.ToString
    End Function

    Private Function IsMicrosoftAssembly(ByVal strAssemblyName As String) As Boolean
        'second copy in XAMLPowerToys.UI.Helpers
        strAssemblyName = strAssemblyName.ToLower

        If strAssemblyName.StartsWith("system") Then Return True

        If strAssemblyName.StartsWith("mscorlib") Then Return True

        If strAssemblyName.StartsWith("presentationframework") Then Return True

        If strAssemblyName.StartsWith("presentationcore") Then Return True

        If strAssemblyName.StartsWith("microsoft") Then Return True

        If strAssemblyName.StartsWith("windowsbase") Then Return True

        If strAssemblyName.StartsWith("wpftoolkit") Then Return True

        If strAssemblyName.StartsWith("uiautomationprovider") Then Return True

        Return False
    End Function
#End Region

End Class
