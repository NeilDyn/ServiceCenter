<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleReturnOrderDetail.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableData.SingleReturnOrderDetail" %>
<%@ Register Src="~/Forms/UserControls/TableData/DataLines/ReturnOrderLines/SingleReturnOrderReceipts.ascx" TagName="SingleReturnOrderReceipts" TagPrefix="sror" %>
<%@ Register Src="~/Forms/UserControls/TableData/DataLines/ReturnOrderLines/SingleReturnOrderPackages.ascx" TagName="SingleReturnOrderPackages" TagPrefix="srop" %>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("[id$=expandReceipts_<%= this.CustID %>_<%= this.CountID %>]").hide();   
        $("[id$=expandReceives_<%= this.CustID %>_<%= this.CountID %>]").hide();
        $("[id*=showMoreReturnLines_<%= this.CustID %>_<%= this.CountID %>]").hide();
        
        $("[id$=btnCreateExchange<%= this.CustID %>_<%= this.CountID %>]").click(function () {
            alert("Hi, return <%= this.Rh.RMANo %> can be exchanged.");
        });

        $("[id$=btnIssueRefund<%= this.CustID %>_<%= this.CountID %>]").click(function () {
            alert("Hi, return <%= this.Rh.RMANo %> can be refunded.");
        });
    });

    function expandMoreReturnLines<%= this.CustID %><%= this.CountID %>(lineID) {
        if ($("a#expandMoreClickReturnLine_<%=this.CustID %>_<%= this.CountID %>_" + lineID).text() == "Show More") {
            $("a#expandMoreClickReturnLine_<%=this.CustID %>_<%= this.CountID %>_" + lineID).text("Show Less");
        }
        else {
            $("a#expandMoreClickReturnLine_<%=this.CustID %>_<%= this.CountID %>_" + lineID).text("Show More");
        }

        $("[id*=showMoreReturnLines_<%= this.CustID %>_<%= this.CountID %>_" + lineID + "]").toggle();      
    };

    function expandReceipts<%=this.CustID %><%= this.CountID %>() {
            $("[id$=expandReceipts_<%= this.CustID %>_<%= this.CountID %>]").toggle();
    };


    function expandReceives<%=this.CustID %><%= this.CountID %>() {
            $("[id$=expandReceives_<%= this.CustID %>_<%= this.CountID %>]").toggle();
    };
</script>
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
        <asp:TableCell runat="server" ID="tcReturnTrackingNo" />
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
    <asp:TableRow runat="server" ID="expandReceipts" TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell><br /></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Zendesk Ticket #:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcZendeskTicketNo" />
        <asp:TableCell Text="Package(s):" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcPackagesCount" />
    </asp:TableRow>
    <asp:TableRow runat="server" ID="expandReceives" TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell><br /></asp:TableCell>
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