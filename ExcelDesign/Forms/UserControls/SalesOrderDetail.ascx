<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesOrderDetail.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.SalesOrderDetail" %>

<link href="../../css/mainpage.css" rel="stylesheet" type="text/css" />

<div id="OrderDetail" runat="server" class="OrderDetail">
    <div style="float:left"><asp:Label ID="lblOrderSequence" Font-Bold="True" runat="server" style="text-decoration:underline" Text="Order" /></div>
    <div style="float:right; margin-right:100px"><asp:Label ID="lblExternalDocumentNo" runat="server" /></div>
    <div style="float:right; margin-right:100px"><asp:Label ID="Label13" runat="server" Text="External Document No:"/></div>  
    <br />                

    <div id="OrderDetailHeaderCaptions" runat="server" class="detailedOrderCaption">
        <asp:Label ID="lblOrderStatusCaption" runat="server" Text="Order Status:" />
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Order Date:" />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Sales Order Number:" />
        <br />
        <asp:Label ID="Label5" runat="server" Text="Channel Name:" />
        <br />
        <asp:Label ID="Label6" runat="server" Text="Zendesk Ticket #:" />
        <br />
        <asp:Label ID="Label7" runat="server" Text="Zendesk Ticket(s):" />
    </div>
    <div id="OrderDetailHeaderInfo" runat="server" class="detailedOrderInfo">
        <asp:Label ID="lblOrderStatus" runat="server" />
        <br />
        <br />
        <asp:Label ID="lblOrderDate" runat="server" />
        <br />
        <asp:Label ID="lblSalesOrderNumber" runat="server" />
        <br />
        <asp:Label ID="lblChannelName" runat="server"/>
        <br />
        <asp:Label ID="lblZendeskTicket" runat="server" />
        <br />
        <asp:Label ID="lblZendeskTicketNo" runat="server" />
    </div>
    <div id="OrderDetailHeaderCaptionsCon" class="detailedOrderCaptionCon">
        <asp:Label ID="Label2" runat="server" Text="Shipment Date:" />
        <br />
        <asp:Label ID="Label8" runat="server" Text="Shipments:" />
        <br />
        <asp:Label ID="Label9" runat="server" Text="Package(s):" />
        <br />
        <asp:Label ID="Label10" runat="server" Text="Ship Method:" />
        <br />
        <asp:Label ID="Label11" runat="server" Text="Tracking #:" />
    </div>
    <div id="OrderDetailHeaderInfoCon" class ="detailedOrderInfoCon">
        <asp:Label ID="lblShipmentDate" runat="server" />
        <br />
        <asp:Label ID="lblShipments" runat="server" />
        <br />
        <asp:Label ID="lblPackages" runat="server" />
        <br />
        <asp:Label ID="lblShipMethod" runat="server" />
        <br />
        <asp:Label ID="lblTrackingNo" runat="server" />
    </div>

    <br />
    <br />
    <asp:Table ID="tblWarranty" runat="server" BorderStyle="Solid" BorderWidth="2px" Height="75px" Width="250px" BorderColor="Black" Caption="Warranty">
        <asp:TableHeaderRow ID="trStatus" runat="server">
            <asp:TableCell ID="tcStatus" runat="server" Font-Bold="True" HorizontalAlign="Right">Status:</asp:TableCell>
            <asp:TableCell ID="tcSetStatus" runat="server"></asp:TableCell>
        </asp:TableHeaderRow>
        <asp:TableRow ID="trPolicy" runat="server">
            <asp:TableCell ID="tcPolicy" runat="server" Font-Bold="True" HorizontalAlign="Right" VerticalAlign="Middle">Policy:</asp:TableCell>
            <asp:TableCell ID="tcSetPolicy" runat="server" Font-Bold="False"></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="trDays" runat="server">
            <asp:TableCell ID="tcDays" runat="server" Font-Bold="True" HorizontalAlign="Right">Days Remaining:</asp:TableCell>
            <asp:TableCell ID="tcSetDays" runat="server"></asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <div runat="server">
        <asp:GridView ID="gdvOrderView" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Height="16px" Width="1200px">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    </div>

    <div class="Options">
        <ul style="height: 31px">
            <li>                          
                <asp:Button ID="Button4" runat="server" Text="Issue Refund" />
            </li>
            <li>                 
                <asp:Button ID="Button3" runat="server" Text="Create Return" />
            </li>
            <li>
                <asp:Button ID="Button2" runat="server" Text="Part Request" />
            </li>
            <li>
                <asp:Button ID="Button1" runat="server" Text="Cancel Order" />
            </li>
        </ul>
    </div>
</div>
    <br />