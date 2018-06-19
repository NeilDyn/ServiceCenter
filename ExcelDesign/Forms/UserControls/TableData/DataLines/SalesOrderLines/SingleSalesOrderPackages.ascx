<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleSalesOrderPackages.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableData.DataLines.SalesOrderLines.SingleSalesOrderPackages" %>

<link href="../../../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<asp:Table ID="tblSalesPackageLines" runat="server" Height="100%" Width="100%">
    <asp:TableHeaderRow>
        <asp:TableHeaderCell/>
        <asp:TableHeaderCell Text="Package No." HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Package Date" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Item" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Description" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Qty" />
        <asp:TableHeaderCell Text="Serial No." HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Carrier" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Tracking No." HorizontalAlign="Left" />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow>
        <asp:TableHeaderCell />
        <asp:TableHeaderCell ColumnSpan="8">
            <hr class="HeaderLine" />
        </asp:TableHeaderCell>
    </asp:TableHeaderRow>
</asp:Table>