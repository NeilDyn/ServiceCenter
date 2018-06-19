<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleReturnOrderReceipts.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableData.DataLines.ReturnOrderLines.SingleReturnOrderReceipts" %>

<link href="../../../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<asp:Table ID="tblReceiptLines" runat="server" Height="100%" Width="100%" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px">
    <asp:TableHeaderRow ForeColor="White" BackColor="#507CD1">
        <asp:TableHeaderCell />
        <asp:TableHeaderCell Text="Receipt No." HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Receipt Date" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Item" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Description"  HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Qty" />
        <asp:TableHeaderCell Text="Ship Method" HorizontalAlign="Left"/>
    </asp:TableHeaderRow>
    <asp:TableHeaderRow>
        <asp:TableHeaderCell />
        <asp:TableHeaderCell ColumnSpan="6">
            <hr class="HeaderLine" />
        </asp:TableHeaderCell>
    </asp:TableHeaderRow>
</asp:Table>