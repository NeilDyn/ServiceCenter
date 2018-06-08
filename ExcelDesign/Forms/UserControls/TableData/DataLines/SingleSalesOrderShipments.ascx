<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleSalesOrderShipments.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableData.DataLines.SingleSalesOrderShipments" %>

<link href="../../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<asp:Table ID="tblShipmentLines" runat="server" Height="100%" Width="100%">
    <asp:TableHeaderRow>
        <asp:TableHeaderCell Text="Shipment No." />
        <asp:TableHeaderCell Text="Shipment Date" />
        <asp:TableHeaderCell Text="Item" />
        <asp:TableHeaderCell Text="Description" />
        <asp:TableHeaderCell Text="Qty" />
        <asp:TableHeaderCell Text="Ship Method" />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow>
        <asp:TableHeaderCell ColumnSpan="6">
            <hr class="HeaderLine" />
        </asp:TableHeaderCell>
    </asp:TableHeaderRow>
</asp:Table>
