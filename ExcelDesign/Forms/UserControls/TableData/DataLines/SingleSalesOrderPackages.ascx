<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleSalesOrderPackages.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableData.DataLines.SingleSalesOrderPackages" %>

<link href="../../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<asp:Table ID="tblPackageLines" runat="server" Height="100%" Width="100%">
    <asp:TableHeaderRow>
        <asp:TableHeaderCell Text="Package No." />
        <asp:TableHeaderCell Text="Package Date" />
        <asp:TableHeaderCell Text="Item" />
        <asp:TableHeaderCell Text="Description" />
        <asp:TableHeaderCell Text="Qty" />
        <asp:TableHeaderCell Text="Serial No." />
        <asp:TableHeaderCell Text="Carrier" />
        <asp:TableHeaderCell Text="Tracking No." />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow>
        <asp:TableHeaderCell ColumnSpan="8">
            <hr class="HeaderLine" />
        </asp:TableHeaderCell>
    </asp:TableHeaderRow>
</asp:Table>