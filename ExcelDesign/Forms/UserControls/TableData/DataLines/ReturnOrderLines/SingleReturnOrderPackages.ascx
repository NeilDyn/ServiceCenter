<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleReturnOrderPackages.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableData.DataLines.ReturnOrderLines.SingleReturnOrderPackages" %>

<link href="../../../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<asp:Table ID="tblReturnPackageLines" runat="server" Height="100%" Width="100%">
    <asp:TableHeaderRow ForeColor="White" BackColor="#507CD1">
        <asp:TableHeaderCell />
        <asp:TableHeaderCell Text="Receive No." />
        <asp:TableHeaderCell Text="Receive Date" />
        <asp:TableHeaderCell Text="Item" />
        <asp:TableHeaderCell Text="Description" />
        <asp:TableHeaderCell Text="Qty" />
        <asp:TableHeaderCell Text="Serial No." />
        <asp:TableHeaderCell Text="Carrier" />
        <asp:TableHeaderCell Text="Tracking No." />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow>
        <asp:TableHeaderCell />
        <asp:TableHeaderCell ColumnSpan="8">
            <hr class="HeaderLine" />
        </asp:TableHeaderCell>
    </asp:TableHeaderRow>
</asp:Table>