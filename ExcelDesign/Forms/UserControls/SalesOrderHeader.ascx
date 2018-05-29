<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesOrderHeader.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.SalesOrderHeader" %>
<link href="../../css/mainpage.css" rel="stylesheet" type="text/css" />

<div id="OrderInfoHeader" class="OrderInfoHeader" runat="server" >
    <div style="float:left"><asp:Label ID="Label12" runat="server" Text="Order Info:" ForeColor="#0099FF" Font-Bold="True" /></div>
    <div style="float:right; margin-right:100px"><asp:Label ID="lblTotalOrderCount" Text="" runat="server" ForeColor="#0099FF" Font-Bold="True" /> </div>
    <div style="float:right; margin-right:100px"><asp:Label ID="Label15" runat="server" Text="Totals Orders:" ForeColor="#0099FF" Font-Bold="True" /></div>
    <br />
    <div style="float:none"><hr class="SectionBreak"/></div>
 
</div>  