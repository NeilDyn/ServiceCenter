﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StatisticsUserControl.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.StatisticsControls.StatisticsUserControl" %>

<%@ Register Src="~/Forms/UserControls/StatisticsControls/PendingReplacements.ascx" TagName="PendingReplacements" TagPrefix="prc" %>
<%@ Register Src="~/Forms/UserControls/StatisticsControls/PendingRefund.ascx" TagName="PendingRefunds" TagPrefix="prd" %>
<%@ Register Src="~/Forms/UserControls/StatisticsControls/PendingUnknown.ascx" TagName="PendingUnknown" TagPrefix="pu" %>
<%@ Register Src="~/Forms/UserControls/StatisticsControls/PendingSQApproval.ascx" TagName="PendingSQApproval" TagPrefix="psqa" %>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("[id$=expandReplacementDetails]").hide();
        $("[id$=expandRefundDetails]").hide();
        $("[id$=expandPendingSQApproval]").hide();
        $("[id$=expandPendingUnknown]").hide();

        var statisticsSalesLine;
    });

    function expandReplacements() {
        $("[id$=expandReplacementDetails]").toggle();
    };

    function expandRefunds() {
        $("[id$=expandRefundDetails]").toggle();
    };

    function expandPendingSQQuotes() {
        $("[id$=expandPendingSQApproval]").toggle();
    };

    function expandPendingUnknown() {
        $("[id$=expandPendingUnknown]").toggle();
    };

    function OpenSalesLineWindow(pendingList, pendingType) {
        var width = 1500;
        var height = 800;
        var left = (screen.height - width) + 1500;
        var top = (screen.height - height);

        if (typeof (partRequestWindow) == 'undefined' || partRequestWindow.closed) {
            if (window.location.href.indexOf("Forms") > -1) {
                statisticsSalesLine = window.open("UserControls/StatisticsControls/SalesLines/StatisticsSalesLineForm.aspx?PendingList=" + pendingList + "&PendingType=" + pendingType,
                    null, "left=" + left + ",width=" + width + ",height=" + height + ",top=" + top + ",status=no,resizable=no,toolbar=no,location=no,menubar=no,directories=no");
            } else {
                statisticsSalesLine = window.open("Forms/UserControls/StatisticsControls/SalesLines/StatisticsSalesLineForm.aspx?PendingList=" + pendingList + "&PendingType=" + pendingType,
                null, "left=" + left + ",width=" + width + ",height=" + height + ",top=" + top + ",status=no,resizable=no,toolbar=no,location=no,menubar=no,directories=no");
            }

            function checkIfWinClosed(intervalID) {
                if (statisticsSalesLine.closed) {
                    clearInterval(intervalID);
                }
            }

            var interval = setInterval(function () {
                checkIfWinClosed(interval);
            }, 1000);

        } else {
            alert('Please close the current active Statistics dialog window before trying to open a new instance.');
        }
    }
</script>
<asp:Table runat="server" ID="tblStatistics" Height="100%" Width="30%" HorizontalAlign="Right">
    <asp:TableHeaderRow HorizontalAlign="Justify" ForeColor="White" BackColor="#507CD1" Font-Size="Larger" Font-Bold="true">
        <asp:TableHeaderCell Text="Returns" ColumnSpan="2" Width="50%" />
        <asp:TableHeaderCell Text="Part Requests" ColumnSpan="2" Width="50%" />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow>
        <asp:TableHeaderCell ColumnSpan="2">
                    <hr class="HeaderLine" />
        </asp:TableHeaderCell>
        <asp:TableHeaderCell ColumnSpan="2">
                    <hr class="HeaderLine" />
        </asp:TableHeaderCell>
    </asp:TableHeaderRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell Text="Pending Replacement" Font-Bold="true" />
        <asp:TableCell runat="server" ID="tcPendingReplacements" />
        <asp:TableCell Text="Pending SQ Approval" Font-Bold="true" />
        <asp:TableCell runat="server" ID="tcPendingSQApproval" />
    </asp:TableRow>
    <asp:TableRow runat="server" TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell runat="server" ID="expandReplacementDetails" ColumnSpan="2" Width="50%" Height="100%"/>
        <asp:TableCell runat="server" ID="expandPendingSQApproval" ColumnSpan="2" Width="50%" Height="100%" />
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" BackColor="#EFF3FB" HorizontalAlign="Justify">
        <asp:TableCell Text="Pending Refund" Font-Bold="true" />
        <asp:TableCell runat="server" ID="tcPendingRefunds" />
    </asp:TableRow>
    <asp:TableRow runat="server" TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell runat="server" ID="expandRefundDetails" ColumnSpan="2" Width="50%" Height="100%" />
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell Text="Pending Unknown" Font-Bold="true" />
        <asp:TableCell runat="server" ID="tcPendingUnknown" />
    </asp:TableRow>
    <asp:TableRow runat="server" TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell runat="server" ID="expandPendingUnknown" ColumnSpan="2" Width="50%" Height="100%" />
    </asp:TableRow>
</asp:Table>
