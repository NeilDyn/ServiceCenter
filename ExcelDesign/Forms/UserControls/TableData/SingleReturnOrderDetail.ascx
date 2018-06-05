<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleReturnOrderDetail.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableData.SingleReturnOrderDetail" %>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<asp:Table ID="tblSingleReturnOrderDetail" runat="server" Height="100%" Width="100%">
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Return Status:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcReturnStatus" />
    </asp:TableRow>
        <asp:TableRow>
        <asp:TableCell>
            <br />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Date Created:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcDateCreated" />
        <asp:TableCell Text="Receipt Date:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcReceiptDate" />
        <asp:TableCell Text="Return Tracking #:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcReturnTrackingNo" Font-Underline="true" style="text-decoration-color:blue"/>
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Channel Name:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcChannelName" />
        <asp:TableCell Text="Receipts:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcReceiptsTotal" />
        <asp:TableCell Text="Order Date:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcOrderDate" />
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Zendesk Ticket #:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcZendeskTicketNo" />
        <asp:TableCell Text="Package(s):" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcPackagesCount" />
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Zendesk Ticket(s):" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcZendeskTickets" />
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell />
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell />
        <asp:TableCell ColumnSpan ="8" >
            <asp:Table runat="server" ID="tblReturnDetailLines" Height="100%" Width="100%">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell Text="Item No." />
                    <asp:TableHeaderCell Text="Description"/>
                    <asp:TableHeaderCell Text="Qty" />
                    <asp:TableHeaderCell Text="Qty Received" />
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