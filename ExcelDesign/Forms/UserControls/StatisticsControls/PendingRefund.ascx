<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PendingRefund.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.StatisticsControls.PendingRefund" %>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>

<asp:Table ID="tblPendingRefunds" runat="server" Width="100%" Height="100%" Font-Size="Medium">
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableHeaderCell Text="All Pending" Font-Bold="false" />
        <asp:TableHeaderCell runat="server" ID="tcAllRefundsPending" />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableHeaderCell Text="Older than 24 hours" Font-Bold="false"/>
        <asp:TableHeaderCell runat="server" ID="tcRefundOlder24Hours" />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableHeaderCell Text="Older than 48 hours" Font-Bold="false"/>
        <asp:TableHeaderCell runat="server" ID="tcRefundOlder48Hours" />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableHeaderCell Text="Older than 72 hours" Font-Bold="false"/>
        <asp:TableHeaderCell runat="server" ID="tcRefundOlder72Hours" />
     </asp:TableHeaderRow>
</asp:Table>
