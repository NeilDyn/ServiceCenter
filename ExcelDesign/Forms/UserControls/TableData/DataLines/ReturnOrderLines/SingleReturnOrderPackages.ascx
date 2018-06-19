<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleReturnOrderPackages.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableData.DataLines.ReturnOrderLines.SingleReturnOrderPackages" %>

<link href="../../../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<asp:Table ID="tblReturnPackageLines" runat="server" Height="100%" Width="100%">
    <asp:TableHeaderRow>
        <asp:TableHeaderCell />
        <asp:TableHeaderCell Text="Receive No." HorizontalAlign="Left" />
        <asp:TableHeaderCell Text="Receive Date" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Item" HorizontalAlign="Left" />
        <asp:TableHeaderCell Text="Description" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Qty" />
        <asp:TableHeaderCell Text="Serial No." HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Carrier" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Tracking No." HorizontalAlign="Left"/>
    </asp:TableHeaderRow>
    <asp:TableHeaderRow>
        <asp:TableHeaderCell />
        <asp:TableHeaderCell ColumnSpan="8">
            <hr class="HeaderLine" />
        </asp:TableHeaderCell>
    </asp:TableHeaderRow>
</asp:Table>