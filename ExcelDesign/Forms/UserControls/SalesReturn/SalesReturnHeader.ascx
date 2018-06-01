<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesReturnHeader.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.SalesReturnHeader" %>
<link href="../../css/mainpage.css" rel="stylesheet" type="text/css" />

<div id="ReturnInfoHeader" class="ReturnInfoHeader" runat="server" >
    <div style="float:left"><asp:Label ID="Label12" runat="server" Text="Return Info:" ForeColor="#0099FF" Font-Bold="True" /></div>
    <div style="float:right; margin-right:100px"><asp:Label ID="lblTotalReturnCount" Text="" runat="server" ForeColor="#0099FF" Font-Bold="True" /> </div>
    <div style="float:right; margin-right:100px"><asp:Label ID="Label15" runat="server" Text="Totals Returns:" ForeColor="#0099FF" Font-Bold="True" /></div>
    <br />
    <div style="float:none"><hr class="SectionBreak"/></div> 
</div>  