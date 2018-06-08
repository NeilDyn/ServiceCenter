﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleSalesOrderDetail.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableData.SingleSalesOrderDetail" %>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("[id$=btnCancelOrder_<%= this.CustID %>_<%= this.CountID %>]").click(function () {
            alert("Hi, order can be cancelled.");
        });
    });
</script>

<asp:Table ID="tblSingleSalesOrderDetail" runat="server" Height="100%" Width="100%">
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Order Status:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcOrderStatus" />
    </asp:TableRow>
        <asp:TableRow>
        <asp:TableCell>
            <br />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Order Date:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcOrderDate" />
        <asp:TableCell Text="Shipment Date:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcShipmentDate" />
        <asp:TableCell Text="Warranty" Font-Bold="true" HorizontalAlign="Center" ColumnSpan="2"/>
        <asp:TableCell />
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Sales Order No:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcSalesOrderNo" />
        <asp:TableCell Text="Shipments:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcShipmentsTotal" />
        <asp:TableCell Text="Status:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcStatus" />
        <asp:TableCell />
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Channel Name:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcChannelName" />
        <asp:TableCell Text="Package(s):" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcPackagesCount" />
        <asp:TableCell Text="Policy:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcPolicy" />
        <asp:TableCell />
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Zendesk Ticket #:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcZendeskTicketNo" />
        <asp:TableCell Text="Ship Method:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcShipMethod" />
        <asp:TableCell Text="Days Remaining:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcDaysRemaining" />
        <asp:TableCell />
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Zendesk Ticket(s):" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcZendeskTickets" />
        <asp:TableCell Text="Tracking #:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcTrackingNo" />
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell />
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell />
        <asp:TableCell ColumnSpan ="8" >
            <asp:Table runat="server" ID="tblOrderDetailLines" Height="100%" Width="100%">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell Text="Item No." />
                    <asp:TableHeaderCell Text="Description"/>
                    <asp:TableHeaderCell Text="Qty" />
                    <asp:TableHeaderCell Text="Qty Shipped" />
                    <asp:TableHeaderCell Text="Price" />
                    <asp:TableHeaderCell Text="Line Amt" />
                    <asp:TableHeaderCell Text="Serial #"/>
                    <asp:TableHeaderCell Text="" />
                </asp:TableHeaderRow>
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell ColumnSpan="8">
                        <hr class="HeaderLine"/>
                    </asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
