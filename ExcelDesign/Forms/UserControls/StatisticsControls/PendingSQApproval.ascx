<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PendingSQApproval.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.StatisticsControls.PendingSQApproval" %>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>

<asp:Table ID="tblPendingSQApproval" runat="server" Width="100%" Height="100%">
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableHeaderCell Text="All Pending" Font-Bold="false" />
        <asp:TableHeaderCell runat="server" ID="tcAllSQPending" />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableHeaderCell Text="No Inventory Avail." Font-Bold="false" />
        <asp:TableHeaderCell runat="server" ID="tcSQNoInvAvail" />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableHeaderCell Text="Older than 24 hours" Font-Bold="false"/>
        <asp:TableHeaderCell runat="server" ID="tcSQOlderThan24Hours" />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableHeaderCell Text="Older than 48 hours" Font-Bold="false"/>
        <asp:TableHeaderCell runat="server" ID="tcSQOlderThan48Hours" />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify" >
        <asp:TableHeaderCell Text="Older Than 72 hours" Font-Bold="false" />
        <asp:TableHeaderCell runat="server" ID="tcSQOlderThan72Hours" />
    </asp:TableHeaderRow>
</asp:Table>