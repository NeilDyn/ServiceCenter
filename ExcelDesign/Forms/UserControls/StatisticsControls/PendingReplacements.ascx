<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PendingReplacements.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.StatisticsControls.PendingReplacements" %>

<%@ Register Src="~/Forms/UserControls/StatisticsControls/SalesLInes/ReplacementNoInvLines.ascx" TagName="ReplacementNoInvLines" TagPrefix="rnil" %>
<%@ Register Src="~/Forms/UserControls/StatisticsControls/SalesLInes/ReplacementOlder72HoursLines.ascx" TagName="ReplacementOlder72HoursLines" TagPrefix="rl72hl" %>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("[id$=expandReplacementNoInventory]").hide();
        $("[id$=expand72HoursReplacement]").hide();
    });

    function expandInvNotAvail() {
        $("[id$=expandReplacementNoInventory]").toggle();
    };

    function expandReplacementsOlderThan72Hours() {
        $("[id$=expand72HoursReplacement]").toggle();
    };
</script>

<asp:Table ID="tblPendingReplacements" runat="server" Width="100%" Height="100%">
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableHeaderCell Text="No Inventory Avail." Font-Bold="false" />
        <asp:TableHeaderCell runat="server" ID="tcReplacementNoInvAvail" />
    </asp:TableHeaderRow>
    <asp:TableRow runat="server" ID="expandReplacementNoInventory" TableSection="TableBody" HorizontalAlign="Justify">
    </asp:TableRow>
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify" BackColor="#EFF3FB">
        <asp:TableHeaderCell Text="Older Than 72 hours" Font-Bold="false" />
        <asp:TableHeaderCell runat="server" ID="tcReplacementOlderThan72Hours" />
    </asp:TableHeaderRow>
    <asp:TableRow runat="server" ID="expand72HoursReplacement" TableSection="TableBody" HorizontalAlign="Justify">
    </asp:TableRow>
</asp:Table>
