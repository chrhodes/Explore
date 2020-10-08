<%@ Page language="c#" Codebehind="AdminForm.aspx.cs" AutoEventWireup="false" Inherits="RemoteAdmin.AdminForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AdminForm</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="AdminForm" method="post" encType="multipart/form-data" runat="server">
			<table id="CommandTable" width="90%" runat="server">
				<tr>
					<td>Program to run:<br>
						<asp:textbox id="FilePath" runat="server" width="100%"></asp:textbox></td>
				</tr>
				<tr>
					<td>Command line arguments:<br>
						<asp:textbox id="Args" runat="server" width="100%"></asp:textbox></td>
				</tr>
				<tr>
					<td>Working directory:<br>
						<asp:textbox id="WorkingDir" runat="server" width="100%"></asp:textbox></td>
				</tr>
				<tr>
					<td>Standard input:<br>
						<asp:textbox id="Input" runat="server" width="100%" TextMode="MultiLine" Height="100"></asp:textbox></td>
				</tr>
				<tr>
					<td align="right"><asp:button id="Submit" runat="server" Text="Submit" Width="85"></asp:button></td>
				</tr>
				<tr>
					<td><pre id="Output" runat="server"></pre>
					</td>
				</tr>
			</table>
			<table id="LoadTable" style="WIDTH: 567px; HEIGHT: 81px" runat="server">
				<tr>
					<td>File to upload:<br>
						<INPUT id="MyFile" style="WIDTH: 560px; HEIGHT: 22px" type="file" size="74" name="MyFile" runat="server">
					</td>
				</tr>
				<tr>
					<td align="middle"><asp:button id="Upload" runat="server" Text="Upload"></asp:button></td>
				</tr>
				<tr>
					<td><pre id="Message" runat="server"></pre>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
