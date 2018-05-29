<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Demo.aspx.cs" Inherits="ExcelDesign.Forms.Demo" %>
<%@ Register Src="~/Forms/UserControls/SalesOrderHeader.ascx" TagName="SalesOrderHeader" TagPrefix="soh" %>
<%@ Register Src="~/Forms/UserControls/SalesOrderDetail.ascx" TagName="SalesOrderDetail" TagPrefix="sod" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Demo</title>
    <link href="../css/mainpage.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmDemo" runat="server">
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />      
    </form>
</body>
</html>
