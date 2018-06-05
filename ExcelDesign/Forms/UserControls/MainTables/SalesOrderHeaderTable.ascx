<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesOrderHeaderTable.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.MainTables.SalesOrderHeaderTable" %>
<%@ Register Src="~/Forms/UserControls/TableHeaders/SingleSalesOrderTableHeader.ascx" TagName="SingleSalesOrderTableHeader" TagPrefix="ssoth" %>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<asp:Table ID="tblSalesOrderHeader" runat="server" Width="100%" Height="100%">
    <asp:TableHeaderRow TableSection="TableHeader">
        <asp:TableHeaderCell runat="server" ID="ExpandCurrentCustomer" Width="2%">
            <asp:Button ID="btnExpandCurrentCustomer" runat="server" Text="+" />     
        </asp:TableHeaderCell>
        <asp:TableHeaderCell Text="Order Info:" HorizontalAlign="Left" ForeColor="#0099FF" Font-Bold="True" Font-Size="Large"/>
        <asp:TableHeaderCell Text="Total Orders:" HorizontalAlign="Right" ForeColor="#0099FF" Font-Bold="True" Font-Size="Large"/>
        <asp:TableHeaderCell runat="server" ID="thcTotalOrders" HorizontalAlign="Right" ForeColor="#0099FF" Font-Bold="True" Font-Size="Large"/>
    </asp:TableHeaderRow>
    <asp:TableRow>
        <asp:TableCell />
        <asp:TableCell ColumnSpan="3"> <hr class="TableHeaderLine"/></asp:TableCell>
    </asp:TableRow>
</asp:Table>