<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleSalesOrderTrackingNos.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableData.DataLines.SalesOrderLines.SingleSalesOrderTrackingNos" %>

<link href="../../../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<asp:Table ID="tblPackageLines" runat="server" Height="100%" Width="100%" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px">
    <asp:TableHeaderRow ForeColor="White" BackColor="#507CD1"> 
        <asp:TableHeaderCell />
        <asp:TableHeaderCell Text="Package Date" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Carrier" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Tracking No." HorizontalAlign="Left"/>
    </asp:TableHeaderRow>
    <asp:TableHeaderRow>
        <asp:TableHeaderCell />
        <asp:TableHeaderCell ColumnSpan="3">
            <hr class="HeaderLine" />
        </asp:TableHeaderCell>
    </asp:TableHeaderRow>
</asp:Table>