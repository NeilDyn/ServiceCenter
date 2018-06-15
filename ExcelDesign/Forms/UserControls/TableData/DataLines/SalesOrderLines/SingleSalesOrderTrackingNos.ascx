<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleSalesOrderTrackingNos.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableData.DataLines.SalesOrderLines.SingleSalesOrderTrackingNos" %>

<link href="../../../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<asp:Table ID="tblPackageLines" runat="server" Height="100%" Width="100%">
    <asp:TableHeaderRow>
        <asp:TableHeaderCell />
        <asp:TableHeaderCell Text="Package Date" />
        <asp:TableHeaderCell Text="Carrier" />
        <asp:TableHeaderCell Text="Tracking No." />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow>
        <asp:TableHeaderCell />
        <asp:TableHeaderCell ColumnSpan="3">
            <hr class="HeaderLine" />
        </asp:TableHeaderCell>
    </asp:TableHeaderRow>
</asp:Table>