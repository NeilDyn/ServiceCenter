<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="ExcelDesign.Forms.Statistics" %>

<%@ Register Src="~/Headers/Navbar.ascx" TagName="StatisticsNavbar" TagPrefix="statsnav" %>
<%@ Register Src="~/Forms/UserControls/StatisticsControls/PendingReplacements.ascx" TagName="PendingReplacements" TagPrefix="prc" %>
<%@ Register Src="~/Forms/UserControls/StatisticsControls/PendingRefund.ascx" TagName="PendingRefunds" TagPrefix="prd" %>
<%@ Register Src="~/Forms/UserControls/StatisticsControls/SalesLInes/PendingSQApprovalLines.ascx" TagName="PendingSQLApprovalLines" TagPrefix="psqal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Statistics Panel</title>
    <link href="../css/mainpage.css" rel="stylesheet" type="text/css" />
    <link rel="icon" type="image/ico" href="../images/icon.ico" />
    <script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$=expandReplacementDetails]").hide();
            $("[id$=expandRefundDetails]").hide();
            $("[id$=expandPendingSQApprovalDetails]").hide();           
        });

        function expandReplacements() {
            $("[id$=expandReplacementDetails]").toggle();
        };

        function expandRefunds() {
            $("[id$=expandRefundDetails]").toggle();
        };

        function expandPendingSQQuotes() {
            $("[id$=expandPendingSQApprovalDetails]").toggle();
        };
    </script>
</head>
<body>
    <form id="frmStatistics" runat="server">

        <statsnav:StatisticsNavbar ID="StatisticsNavbar" runat="server" />

        <asp:Table runat="server" ID="tblStatistics" Style="margin-left: 100px; margin-right: 100px;" Width="85%">
            <asp:TableHeaderRow Font-Bold="true" HorizontalAlign="Justify" ForeColor="White" BackColor="#507CD1" Font-Size="X-Large">
                <asp:TableHeaderCell Text="Actionable Items" ColumnSpan="4"/>
            </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                <asp:TableHeaderCell ColumnSpan="4">
                    <hr class="HeaderLine" />
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableHeaderRow HorizontalAlign="Justify" ForeColor="White" BackColor="#507CD1" Font-Size="Larger" Font-Bold="true">
                <asp:TableHeaderCell Text="Returns" ColumnSpan="2" Width="50%"/>
                <asp:TableHeaderCell Text="Part Requests" ColumnSpan="2" Width="50%"/>
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
                <asp:TableCell Text="Pending Replacement" Font-Bold="true"/>
                <asp:TableCell runat="server" ID="tcPendingReplacements" />
                <asp:TableCell Text="Pending SQ Approval" Font-Bold="true"/>
                <asp:TableCell runat="server" ID="tcPendingSQApproval" />
            </asp:TableRow>
            <asp:TableRow runat="server" TableSection="TableBody" HorizontalAlign="Justify">
                <asp:TableCell runat="server" ID="expandReplacementDetails" ColumnSpan="2" Width="50%" Height="100%">
                </asp:TableCell>  
                <asp:TableCell runat="server" ID="expandPendingSQApprovalDetails" ColumnSpan="2" Width="50%" Height="100%"/>
            </asp:TableRow>
            <asp:TableRow TableSection="TableBody" BackColor="#EFF3FB" HorizontalAlign="Justify">
                <asp:TableCell Text="Pending Refund" Font-Bold="true"/>
                <asp:TableCell runat="server" ID="tcPendingRefunds" />
            </asp:TableRow>
            <asp:TableRow runat="server" TableSection="TableBody" HorizontalAlign="Justify">
                <asp:TableCell runat="server" ID="expandRefundDetails" ColumnSpan="2" Width="50%" Height="100%"/>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

            