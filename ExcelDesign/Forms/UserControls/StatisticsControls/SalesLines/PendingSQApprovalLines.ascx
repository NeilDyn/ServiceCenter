<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PendingSQApprovalLines.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.StatisticsControls.SalesLInes.PendingSQApprovalLines" %>

<link href="../../../../css/mainpage.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>

<asp:Table ID="tblPendingSQApprovalLines" runat="server" Height="100%" Width="100%">
    <asp:TableHeaderRow HorizontalAlign="Justify" ForeColor="White" BackColor="#507CD1">
        <asp:TableHeaderCell Text ="CustomerNo" />
        <asp:TableHeaderCell Text="Document No" />
        <asp:TableHeaderCell Text="Created Date" />
        <asp:TableHeaderCell Text="Item No" />
        <asp:TableHeaderCell Text="Description" />
        <asp:TableHeaderCell Text="Qty" />
        <asp:TableHeaderCell Text="Status" />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow>
        <asp:TableHeaderCell ColumnSpan="7">
                    <hr class="HeaderLine" />
        </asp:TableHeaderCell>
    </asp:TableHeaderRow>
</asp:Table>