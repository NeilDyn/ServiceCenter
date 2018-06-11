<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleReturnOrderReceipts.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableData.DataLines.ReturnOrderLines.SingleReturnOrderReceipts" %>

<link href="../../../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<asp:Table ID="tblReceiveLines" runat="server" Height="100%" Width="100%">
    <asp:TableHeaderRow>
        <asp:TableHeaderCell Text="Receive No." />
        <asp:TableHeaderCell Text="Receive Date" />
        <asp:TableHeaderCell Text="Item" />
        <asp:TableHeaderCell Text="Description" />
        <asp:TableHeaderCell Text="Qty" />
        <asp:TableHeaderCell Text="Ship Method" />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow>
        <asp:TableHeaderCell ColumnSpan="6">
            <hr class="HeaderLine" />
        </asp:TableHeaderCell>
    </asp:TableHeaderRow>
</asp:Table>