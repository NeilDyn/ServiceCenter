<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PendingRefund.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.StatisticsControls.PendingRefund" %>

<%@ Register Src="~/Forms/UserControls/StatisticsControls/SalesLInes/RefundOlder72HoursLines.ascx" TagName="RefundOlder72HoursLines" TagPrefix="ro72hl" %>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>
<script type="text/javascript">
        $(document).ready(function () {
            $("[id$=expandRefundOlder72Hours]").hide();
        });

        function expandRefundOlderThan72Hours() {
            $("[id$=expandRefundOlder72Hours]").toggle();
        };
    </script>
<asp:Table ID="tblPendingRefunds" runat="server" Width="100%" Height="100%">
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableHeaderCell Text="Older than 72 hours" Font-Bold="false"/>
        <asp:TableHeaderCell runat="server" ID="tcRefundOlder72Hours" />
    </asp:TableHeaderRow>
    <asp:TableRow runat="server" ID="expandRefundOlder72Hours" TableSection="TableBody" HorizontalAlign="Justify">
    </asp:TableRow>
</asp:Table>
