Imports System.ComponentModel
Imports System.Configuration.Install
Imports System.IO
Imports System.Text

Public Class Installer

    Private Const _STR_ADDIN_FILENAME As String = "XAMLPowerToys2010.addin"
    Private Const _STR_ADDINS_FOLDER As String = "\Application Data\Microsoft\MSEnvShared\Addins"
    Private _strAddInRootFolder As String = System.Environment.GetEnvironmentVariable("ALLUSERSPROFILE")
    Private _strSaveSettingsFileName As String = Path.Combine(Environment.GetEnvironmentVariable("APPDATA"), "Little Richie Software\XAML Power Toys 2010\XAMLPowerToys.Settings")

    Public Sub New()
        MyBase.New()
        'This call is required by the Component Designer.
        InitializeComponent()
        'Add initialization code after the call to InitializeComponent
    End Sub

    Public Overrides Sub Install(ByVal stateSaver As System.Collections.IDictionary)
        MyBase.Install(stateSaver)

        If Not Directory.Exists(AddInFolder) Then
            Directory.CreateDirectory(AddInFolder)
        End If

        Dim strInstallFolder As String = Me.Context.Parameters.Item("InstallFolder")
        Dim strProductName As String = "XAMLPowerToys2010.dll"
        Dim sb As New StringBuilder(2048)
        sb.AppendLine("<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>")
        sb.AppendLine("<Extensibility xmlns=""http://schemas.microsoft.com/AutomationExtensibility"">")
        sb.AppendLine("  <HostApplication>")
        sb.AppendLine("		<Name>Microsoft Visual Studio</Name>")
        sb.AppendLine("		<Version>10.0</Version>")
        sb.AppendLine("	 </HostApplication>")
        sb.AppendLine("	 <Addin>")
        sb.AppendLine("		<FriendlyName>XAML Power Toys 2010</FriendlyName>")
        sb.AppendLine("		<Description>Visual Studio 2010 Add-in that empowers WPF and Silverlight developers when editing XAML.</Description>")
        sb.AppendFormat("		<Assembly>{0}</Assembly>{1}", Path.Combine(strInstallFolder, strProductName), vbCrLf)
        sb.AppendLine("		<FullClassName>XAMLPowerToys.Connect</FullClassName>")
        sb.AppendLine("		<LoadBehavior>1</LoadBehavior>")
        sb.AppendLine("		<CommandPreload>0</CommandPreload>")
        sb.AppendLine("		<CommandLineSafe>0</CommandLineSafe>")
        sb.AppendLine("	 </Addin>")
        sb.AppendLine("</Extensibility>")
        My.Computer.FileSystem.WriteAllText(AddInFullPath, sb.ToString, False)
    End Sub

    Protected Overrides Sub OnAfterRollback(ByVal savedState As System.Collections.IDictionary)
        MyBase.OnAfterRollback(savedState)

        If File.Exists(AddInFullPath) Then
            File.Delete(AddInFullPath)
        End If

    End Sub

    Protected Overrides Sub OnBeforeUninstall(ByVal savedState As System.Collections.IDictionary)
        MyBase.OnBeforeUninstall(savedState)

        If File.Exists(AddInFullPath) Then
            File.Delete(AddInFullPath)
        End If

    End Sub

    Private ReadOnly Property AddInFolder() As String
        Get
            Return String.Concat(_strAddInRootFolder, _STR_ADDINS_FOLDER)
        End Get
    End Property

    Private ReadOnly Property AddInFullPath() As String
        Get
            Return Path.Combine(AddInFolder, _STR_ADDIN_FILENAME)
        End Get
    End Property

End Class
