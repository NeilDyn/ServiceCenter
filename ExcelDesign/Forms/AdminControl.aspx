﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminControl.aspx.cs" Inherits="ExcelDesign.Forms.AdminControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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