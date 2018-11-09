<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompletedExchanges.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.StatisticsControls.CompletedExchanges" %>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>

<asp:Table ID="tblCompletedExchanges" runat="server" Width="100%" Height="100%">
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableHeaderCell Text="Today" Font-Bold="false" />
        <asp:TableHeaderCell runat="server" ID="tcExchangeToday" />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableHeaderCell Text="Older than 24 hours" Font-Bold="false"/>
        <asp:TableHeaderCell runat="server" ID="tcExchangeOlderThan24Hours" />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableHeaderCell Text="Older than 48 hours" Font-Bold="false"/>
        <asp:TableHeaderCell runat="server" ID="tcExchangeOlderThan48Hours" />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableHeaderCell Text="Older than 72 hours" Font-Bold="false"/>
        <asp:TableHeaderCell runat="server" ID="tcExchangeOlderThan72Hours" />
     </asp:TableHeaderRow>
</asp:Table>
