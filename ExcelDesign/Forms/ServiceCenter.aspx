<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceCenter.aspx.cs" Inherits="ExcelDesign.Forms.ServiceCenter" %>
<%@ Register Src="~/Forms/UserControls/CustomerInfo.ascx" TagName="CustomerInfo" TagPrefix="ci" %>
<%@ Register Src="~/Forms/UserControls/SalesOrderHeader.ascx" TagName="SalesOrderHeader" TagPrefix="soh" %>
<%@ Register Src="~/Forms/UserControls/SalesOrderDetail.ascx" TagName="SalesOrderDetail" TagPrefix="sod" %>
<%@ Register Src="~/Forms/UserControls/SalesReturnHeader.ascx" TagName="SalesReturnHeader" TagPrefix="srh" %>
<%@ Register Src="~/Forms/UserControls/SalesReturnDetail.ascx" TagName="SalesReturnDetail" TagPrefix="srd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Service Center</title>
    <link href="../css/mainpage.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmOrderDetails" runat="server">
        <div id="ServiceCenterHeader">
            <asp:Image ID="imgLogo" runat="server" ImageUrl="~/images/Logo.png" />
            <asp:Label ID="lblCustomerServicePortal" runat="server" style="margin-left: 200px;" Text="Customer Service Portal" ForeColor="#0099FF" Font-Bold="True" Font-Size="XX-Large" CssClass="HeaderLabel"/>        
        </div>
        <hr class="HeaderLine"/>
        
        <div class="SearchArea" id="SearchArea" style="margin-top: 20px;">        
            <asp:Label ID="Label1" runat="server" Text="Search" ForeColor="#0099FF" Font-Bold="True"/>
            <asp:TextBox ID="txtSearchBox" runat="server" Width="700px" BorderColor="Black" BorderWidth="2px" style="margin-left: 35px" />
            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" style="margin-left: 30px" Text="Search" />
        </div>      
    </form>
</body>
</html>
