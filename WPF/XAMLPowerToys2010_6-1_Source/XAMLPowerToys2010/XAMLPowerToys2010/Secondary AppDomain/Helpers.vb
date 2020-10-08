Imports System.IO
Imports EnvDTE
Imports EnvDTE80

Public Class Helpers

#Region " Declarations "

    Private Shared _htBlackListedProjectTypes As Hashtable

#End Region

#Region " Methods "

    Public Shared Function GetAssemblyPath(ByVal vsProject As Project) As String

        Dim strFullPath As String = Path.GetDirectoryName(vsProject.FullName)
        Dim strOutputPath As String = vsProject.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value.ToString()
        Dim strOutputDirectory As String = Path.Combine(strFullPath, strOutputPath)
        Dim strOutputFileName As String = vsProject.Properties.Item("OutputFileName").Value.ToString()
        Dim strAssemblyPath As String = Path.Combine(strOutputDirectory, strOutputFileName)
        Return strAssemblyPath
    End Function

    Public Shared Function IsProjectSilverlight(ByVal aryGuids As String()) As Boolean

        For Each strGuid As String In aryGuids

            If String.Compare("{A1591282-1198-4647-A2B1-27E5FF5F6F3B}", strGuid, True) = 0 Then Return True
        Next

        Return False
    End Function

    Public Shared Function IsProjectBlackListed(ByVal aryGuids As String()) As Boolean

        'some are here because I have not tested them, other because I don't want the add-in trying to load web sites or test projects.
        '
        If _htBlackListedProjectTypes Is Nothing Then
            _htBlackListedProjectTypes = New Hashtable
            _htBlackListedProjectTypes.Add("{349C5851-65DF-11DA-9384-00065B846F21}", String.Empty) 'Web Application 
            _htBlackListedProjectTypes.Add("{E24C65DC-7377-472B-9ABA-BC803B73C61A}", String.Empty) 'Web Site 
            _htBlackListedProjectTypes.Add("{C252FEB5-A946-4202-B1D4-9916A0590387}", String.Empty) 'Visual Database Tools
            _htBlackListedProjectTypes.Add("{A9ACE9BB-CECE-4E62-9AA4-C7E7C5BD2124}", String.Empty) 'Database
            _htBlackListedProjectTypes.Add("{4F174C21-8C12-11D0-8340-0000F80270F8}", String.Empty) 'Database other
            _htBlackListedProjectTypes.Add("{3AC096D0-A1C2-E12C-1390-A8335801FDAB}", String.Empty) 'Test 
            _htBlackListedProjectTypes.Add("{D59BE175-2ED0-4C54-BE3D-CDAA9F3214C8}", String.Empty) 'Workflow VB
            _htBlackListedProjectTypes.Add("{14822709-B5A1-4724-98CA-57A101D1B079}", String.Empty) 'Workflow C#
            _htBlackListedProjectTypes.Add("{978C614F-708E-4E1A-B201-565925725DBA}", String.Empty) 'SET UP
        End If

        For Each strGuid As String In aryGuids

            If _htBlackListedProjectTypes.ContainsKey(strGuid.ToUpper) Then Return True
        Next

        Return False
    End Function

#End Region

#Region " Carlos J. Quintero (Microsoft MVP) Get Project Type Guids "

    'Carlos is the Visual Studio Add-In Grand Master and Microsoft MVP
    '
    'I learned how to figure out if a project is a Silverlight project from these two posts, then wrote a little of my own code
    'http://www.mztools.com/Articles/2007/MZ2007016.aspx
    'http://www.mztools.com/Articles/2007/MZ2007012.aspx
    '
    '
    Friend Shared Function GetProjectTypeGuids(ByVal proj As EnvDTE.Project) As String

        Dim sProjectTypeGuids As String = ""
        Dim objService As Object
        Dim objIVsSolution As Microsoft.VisualStudio.Shell.Interop.IVsSolution
        Dim objIVsHierarchy As Microsoft.VisualStudio.Shell.Interop.IVsHierarchy = Nothing
        Dim objIVsAggregatableProject As Microsoft.VisualStudio.Shell.Interop.IVsAggregatableProject
        Dim iResult As Integer
        objService = GetService(proj.DTE, GetType(Microsoft.VisualStudio.Shell.Interop.IVsSolution))
        objIVsSolution = CType(objService, Microsoft.VisualStudio.Shell.Interop.IVsSolution)
        iResult = objIVsSolution.GetProjectOfUniqueName(proj.UniqueName, objIVsHierarchy)

        If iResult = 0 Then
            objIVsAggregatableProject = CType(objIVsHierarchy, Microsoft.VisualStudio.Shell.Interop.IVsAggregatableProject)
            iResult = objIVsAggregatableProject.GetAggregateProjectTypeGuids(sProjectTypeGuids)
        End If

        Return sProjectTypeGuids
    End Function

    Private Shared Function GetService(ByVal serviceProvider As Object, ByVal guid As System.Guid) As Object

        Dim objService As Object = Nothing
        Dim objIServiceProvider As Microsoft.VisualStudio.OLE.Interop.IServiceProvider
        Dim objIntPtr As IntPtr
        Dim hr As Integer
        Dim objSIDGuid As Guid
        Dim objIIDGuid As Guid
        objSIDGuid = guid
        objIIDGuid = objSIDGuid
        objIServiceProvider = CType(serviceProvider, Microsoft.VisualStudio.OLE.Interop.IServiceProvider)
        hr = objIServiceProvider.QueryService(objSIDGuid, objIIDGuid, objIntPtr)

        If hr <> 0 Then
            System.Runtime.InteropServices.Marshal.ThrowExceptionForHR(hr)

        ElseIf Not objIntPtr.Equals(IntPtr.Zero) Then
            objService = System.Runtime.InteropServices.Marshal.GetObjectForIUnknown(objIntPtr)
            System.Runtime.InteropServices.Marshal.Release(objIntPtr)
        End If

        Return objService
    End Function

    Private Shared Function GetService(ByVal serviceProvider As Object, ByVal type As System.Type) As Object
        Return GetService(serviceProvider, type.GUID)
    End Function

#End Region

End Class
