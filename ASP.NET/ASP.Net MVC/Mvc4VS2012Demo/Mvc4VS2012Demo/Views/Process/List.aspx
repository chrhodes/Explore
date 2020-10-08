<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<System.Diagnostics.Process>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   Process List Title
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Process List</h2>
    <ul>
        <%foreach (var process in this.Model) { %>

        <%--<li><%= process.ProcessName %></li>--%>
        <li><%= Html.ActionLink(process.ProcessName, "Details", new { id=process.Id } ) %></li>

        <% } %>
    </ul>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
