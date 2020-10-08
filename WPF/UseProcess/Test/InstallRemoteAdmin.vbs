	Option Explicit
	
	
	Dim sAppDir
	sAppDir = "c:\RemoteWebAdmin"
	sAppDir = InputBox("Please confirm the target directory path", "", sAppDir)

	dim sParent
	sParent = ""
	sParent = UCase(InputBox("Please confirm the website name", "", sParent))
	

	Dim sPort
	sPort = "80"
	sPort = UCase(InputBox("Please confirm the port number", "", sPort))

	Dim sSourceDir
	sSourceDir = Left(Wscript.ScriptFullName, InstrRev(Wscript.ScriptFullName,"\"))
	
        Dim oFS
	Set oFS = CreateObject("Scripting.FileSystemObject")
	if (oFS.FolderExists(sAppDir)) then
		oFS.DeleteFolder sAppDir, True
	end if
	oFS.CreateFolder sAppDir
	oFS.CreateFolder sAppDir & "\RemoteAdmin"
	oFS.CreateFolder sAppDir & "\RemoteAdmin\Bin"
	oFS.CopyFile sSourceDir & "RemoteAdmin\AdminForm.aspx", sAppDir & "\RemoteAdmin\", True	
	oFS.CopyFile sSourceDir & "RemoteAdmin\Bin\CommandLib.dll", sAppDir & "\RemoteAdmin\Bin\", True	
	oFS.CopyFile sSourceDir & "RemoteAdmin\Bin\RemoteAdmin.dll", sAppDir & "\RemoteAdmin\Bin\", True
	Set oFS = Nothing	
	
	CreateLocalWebDir sAppDir & "\RemoteAdmin", "RemoteWebAdmin", sPort, sParent
	
	WScript.Echo "Script ran to completion"


sub CreateLocalWebDir(sDirPath, sWebName, sPort, sParent)
	
	Dim oMachIne
	Set oMachIne = GetObject("IIS://localhost")

	Dim oService
	Dim oServer
	Dim oRoot
	Dim sBIndIngs
	Dim bFound
	Dim sSiteName
	bFound = False

	Dim sBoundIP
	For Each oService In oMachIne
		If oService.Class = "IIsWebService" Then
			For Each oServer In oService
				If oServer.Class = "IIsWebServer" Then
					sBIndIngs = oServer.ServerBIndIngs
					If InStr(sBIndIngs(0), ":" & sPort & ":") > 0 Then
						sSiteName = oServer.Name
						For Each oRoot In oServer
							If oRoot.Class = "IIsWebVirtualDir" Then
								If sParent = "" or UCase(oServer.ServerComment) = sParent Then
									sBoundIP = Left(sBindings(0), Instr(sBindings(0), ":")-1)
									bFound = True
									Exit For
								End If							
							End If
						Next
					Else
						bFound = False
					End If
				End If
				If bFound Then
					Exit For
				End If
			Next
		End If
		If bFound Then
			Exit For
		End If
	Next

	If Not bFound Then
		WScript.Echo "Cannot find parent website : " & sParent & ", " & sPort
		WScript.Quit	
	End If


	On Error Resume Next
	Call oRoot.Delete("IIsWebVirtualDir", sWebName)
	On Error Goto 0
	Dim oNewDir
	Set oNewDir = oRoot.Create("IIsWebVirtualDir", sWebName)
	oNewDir.AppCreate True
	oNewDir.AppFriendlyName = sWebName
	oNewDir.Path = sDirPath
	oNewDir.AccessRead = True
	oNewDir.AccessScript = True
	' oNewDir.EnableDirBrowsing = True
	' app protection: Low = 0; High = 1; Medium = 2
	oNewDir.AppIsolated = 2 
	oNewDir.AuthAnonymous = True
	oNewDir.AuthNTLM = False
	oNewDir.AuthBasic = False 
	oNewDir.AccessSSL = False
	oNewDir.AccessSSLRequireCert = False
	' oNewDir.AccessSSLNegociateCert = False

	oNewDir.SetInfo

	Set oNewDir = Nothing
	Set oRoot = Nothing
	Set oServer = Nothing
	Set oService = Nothing
	Set oMachine = Nothing
 
	Set oNewDir = GetObject("IIS://LocalHost/W3SVC/" & sSiteName & "/ROOT/" & sWebName)
	Dim objSec
	Set objSec = oNewDir.IPSecurity
	objSec.GrantByDefault = False
	oNewDir.IPSecurity = objSec
	oNewDir.SetInfo
	Set oNewDir = Nothing

	if sBoundIP = "" then
		sBoundIP = "127.0.0.1"
	end if
	sBoundIP = InputBox("Please enter the ip address that is allowed to access this application", "", sBoundIP)

	Set oNewDir = GetObject("IIS://LocalHost/W3SVC/" & sSiteName & "/ROOT/" & sWebName)
	Set objSec = oNewDir.IPSecurity
	Dim listIPGrant
	listIPGrant = objSec.IPGrant
	Redim listIPGrant(1)
	listIPGrant(0) = sBoundIP
	objSec.IPGrant = listIPGrant
	oNewDir.IPSecurity = objSec
	oNewDir.SetInfo
	Set oNewDir = Nothing


End sub

