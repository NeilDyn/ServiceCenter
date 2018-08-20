<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleReturnOrderComments.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableData.DataLines.ReturnOrderLines.SingleReturnOrderComments" %>

<link href="../../../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<asp:Table ID="SingleReturnOrderCommentsTable" runat="server" Height="100%" Width="50%" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px">
    <asp:TableHeaderRow ForeColor="White" BackColor="#507CD1">
        <asp:TableHeaderCell Text="Date" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Comment" HorizontalAlign="Left"/>
    </asp:TableHeaderRow>
</asp:Table>