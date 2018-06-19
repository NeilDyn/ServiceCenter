<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleSalesOrderShipments.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableData.DataLines.SalesOrderLines.SingleSalesOrderShipments" %>

<link href="../../../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<asp:Table ID="tblShipmentLines" runat="server" Height="100%" Width="100%">
    <asp:TableHeaderRow>
        <asp:TableHeaderCell />
        <asp:TableHeaderCell Text="Shipment No." HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Shipment Date" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Item" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Description" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Qty" />
        <asp:TableHeaderCell Text="Ship Method" HorizontalAlign="Left"/>
    </asp:TableHeaderRow>
    <asp:TableHeaderRow>
        <asp:TableHeaderCell />
        <asp:TableHeaderCell ColumnSpan="6">
            <hr class="HeaderLine" />
        </asp:TableHeaderCell>
    </asp:TableHeaderRow>
</asp:Table>
