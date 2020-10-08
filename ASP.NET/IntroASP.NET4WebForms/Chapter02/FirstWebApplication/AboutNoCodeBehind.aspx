<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>About</h2>
            <p><asp:Label ID="OutputLabel" runat="server"></asp:Label></p>
        </div>
    </form>
</body>
</html>

<script type="text/C#" runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        OutputLabel.Text = DateTime.Now.ToShortTimeString();
    }
</script>
