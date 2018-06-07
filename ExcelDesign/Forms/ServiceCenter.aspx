﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceCenter.aspx.cs" Inherits="ExcelDesign.Forms.ServiceCenter" %>
<%@ Register Src="~/Forms/UserControls/SingleControls/MultipleCustomers.ascx" TagName="MultipleCustomers" TagPrefix="mc" %>
<%@ Register Src="~/Forms/UserControls/MainTables/CustomerInfoTable.ascx" TagName="CustomerInfoTable" TagPrefix="tib" %>
<head runat="server">
    <title>Customer Service Center</title>
    <link href="../css/mainpage.css" rel="stylesheet" type="text/css" />
</head>
<html xmlns="http://www.w3.org/1999/xhtml">
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
        <br />     
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    </form>
</body>
</html>
