<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PendingReplacements.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.StatisticsControls.PendingReplacements" %>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>

<asp:Table ID="tblPendingReplacements" runat="server" Width="100%" Height="100%">
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableHeaderCell Text="All Pending" Font-Bold="false" />
        <asp:TableHeaderCell runat="server" ID="tcAllReplacementsPending" />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify" BackColor="#EFF3FB">
        <asp:TableHeaderCell Text="No Inventory Avail." Font-Bold="false" />
        <asp:TableHeaderCell runat="server" ID="tcReplacementNoInvAvail" />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableHeaderCell Text="Older than 24 hours" Font-Bold="false"/>
        <asp:TableHeaderCell runat="server" ID="tcReplacementOlderThan24Hours" />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify" BackColor="#EFF3FB">
        <asp:TableHeaderCell Text="Older than 48 hours" Font-Bold="false"/>
        <asp:TableHeaderCell runat="server" ID="tcReplacementOlderThan48Hours" />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableHeaderCell Text="Older Than 72 hours" Font-Bold="false" />
        <asp:TableHeaderCell runat="server" ID="tcReplacementOlderThan72Hours" />
    </asp:TableHeaderRow>
</asp:Table>
