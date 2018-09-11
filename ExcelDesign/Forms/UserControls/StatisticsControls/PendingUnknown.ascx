<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PendingUnknown.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.StatisticsControls.Pending_Unknown" %>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>

<asp:Table ID="tblPendingUnknown" runat="server" Width="100%" Height="100%">
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableHeaderCell Text="All Pending" Font-Bold="false" />
        <asp:TableHeaderCell runat="server" ID="tcAllUnknownPending" />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify" BackColor="#EFF3FB">
        <asp:TableHeaderCell Text="No Inventory Avail." Font-Bold="false" />
        <asp:TableHeaderCell runat="server" ID="tcUnknownNoInvAvail" />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableHeaderCell Text="Older than 48 hours" Font-Bold="false"/>
        <asp:TableHeaderCell runat="server" ID="tcUnknownOlderThan48Hours" />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify" BackColor="#EFF3FB">
        <asp:TableHeaderCell Text="Older Than 72 hours" Font-Bold="false" />
        <asp:TableHeaderCell runat="server" ID="tcUnknownOlderThan72Hours" />
    </asp:TableHeaderRow>
</asp:Table>