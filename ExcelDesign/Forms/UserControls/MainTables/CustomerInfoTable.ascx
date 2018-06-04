<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerInfoTable.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.CustomerInfo.MainTables.CustomerInfoTable" %>
<%@ Register Src="~/Forms/UserControls/TableHeaders/SingleCustomerTableHeader.ascx" TagName="CustomerHeader" TagPrefix="scth" %>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<asp:Table ID="tblCustomerInfo" runat="server" style="margin-left: 100px; margin-right:100px;" Width="85%">
    <asp:TableHeaderRow TableSection="TableHeader">
        <asp:TableHeaderCell Text="Customer Info:" HorizontalAlign="Left" ForeColor="#0099FF" Font-Bold="True" Font-Size="Large"/>
        <asp:TableHeaderCell Text="Total Customers:" HorizontalAlign="Right" ForeColor="#0099FF" Font-Bold="True" Font-Size="Large"/>
        <asp:TableHeaderCell runat="server" ID="thcTotalCustomers" HorizontalAlign="Right" ForeColor="#0099FF" Font-Bold="True" Font-Size="Large"/>
    </asp:TableHeaderRow>
    <asp:TableRow>
        <asp:TableCell ColumnSpan="3">
            <hr class="TableHeaderLine"/>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
