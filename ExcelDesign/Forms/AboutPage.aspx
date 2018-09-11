<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AboutPage.aspx.cs" Inherits="ExcelDesign.Forms.AboutPage" %>

<%@ Register Src="~/Headers/Navbar.ascx" TagName="AboutNavbar" TagPrefix="aboutnav" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>About Page</title>
    <link href="../css/mainpage.css" rel="stylesheet" type="text/css" />
    <aboutnav:AboutNavbar ID="AboutNavbar" runat="server" />
</head>
<body>
    <form id="frmAboutPage" runat="server">
        <asp:Table ID="tblAboutObjects" runat="server" Width="100%" Height="100%">
            <asp:TableHeaderRow BackColor="#507CD1" ForeColor="White">
                <asp:TableHeaderCell Text="Object ID" />
                <asp:TableHeaderCell Text="Object Name" />
                <asp:TableHeaderCell Text="Object Type" />
                <asp:TableHeaderCell Text="Object Version List" />
                <asp:TableHeaderCell Text="Object Date" />
                <asp:TableHeaderCell Text="Object Time" />
                <asp:TableHeaderCell Text="Object Compiled" />
            </asp:TableHeaderRow>
        </asp:Table>
    </form>
</body>
</html>
