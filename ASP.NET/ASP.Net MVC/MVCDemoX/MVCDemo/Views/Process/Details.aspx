<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Diagnostics.Process>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Details</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>
    
    <%= this.Model.ProcessName  %><br />
    <%= this.Model.Id %>

</asp:Content>
