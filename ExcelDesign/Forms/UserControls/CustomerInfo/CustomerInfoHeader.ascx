<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerInfoHeader.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.CustomerInfoHeader" %>
<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<div id="CustomerInfoArea" class="CustomerInfoHeader" runat="server">
    <div style="float:left"><asp:Label ID="lblCustomerInfo" runat="server" Text="Customer Info:" ForeColor="#0099FF" Font-Bold="True" /></div>
    <div style="float:right; margin-right:100px"><asp:Label ID="lblTotalCustomers" runat="server" ForeColor="#0099FF" Font-Bold="True" /></div>
    <div style="float:right; margin-right:100px"><asp:Label ID="Label2" runat="server" Text="Total Customers:" ForeColor="#0099FF" Font-Bold="True" /></div>
    <br />
    <div style="float:none"><hr class="SectionBreak"/></div>
</div>