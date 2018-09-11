<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminControl.aspx.cs" Inherits="ExcelDesign.Forms.AdminControl" %>

<!DOCTYPE html>

<%@ Register Src="~/Headers/Navbar.ascx" TagName="AdminNavbar" TagPrefix="anav" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Control</title>
    <link rel="icon" type="image/ico" href="../images/icon.ico" />
    <anav:AdminNavbar ID="AdminNavar" runat="server" />
</head>
<body>
    <form id="frmAdminControl" runat="server">        
        <asp:RadioButtonList ID="rblModeSelection" runat="server">
            <asp:ListItem Value="Production Mode"></asp:ListItem>
            <asp:ListItem Value="Development Mode"></asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <asp:Button ID="BtnSetMode" runat="server" Text="Set Mode" OnClick="BtnSetMode_Click" />
    </form>
</body>
</html>
