<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleSalesOrderTableHeader.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableHeaders.SingleSalesOrderTableHeader" %>
<%@ Register Src="~/Forms/UserControls/TableData/SingleSalesOrderDetail.ascx" TagName="SingleSalesOrderDetail" TagPrefix="ssod" %>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<asp:Table ID="tblSingleSalesOrderTableHeader" runat="server" Width="85%" Height="100%">
    <asp:TableHeaderRow TableSection="TableHeader" runat="server">
        <asp:TableHeaderCell runat="server" ID="ExpandCurrentCustomer" Width="2%">
            <asp:Button ID="btnExpandCurrentCustomer" runat="server" Text="+" />     
        </asp:TableHeaderCell>
        <asp:TableHeaderCell runat="server" ID="SalesOrderSequence" Font-Bold="true" Font-Underline="true" Font-Size="Large" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Font-Bold="true" Text="External Document No:" HorizontalAlign="Right" style="text-align:right"/>
        <asp:TableHeaderCell runat="server" ID="thcExternalDocumentNo" HorizontalAlign="Right" style="text-align:left"/>
    </asp:TableHeaderRow>
</asp:Table>