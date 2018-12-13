﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleReturnOrderZendeskTickets.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableData.DataLines.ReturnOrderLines.SingleReturnOrderZendeskTickets" %>

<link href="../../../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<asp:Table ID="SingleReturnOrderZendeskTicketsTable" runat="server" Height="100%" Width="50%" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px">
    <asp:TableHeaderRow ForeColor="White" BackColor="#507CD1">
        <asp:TableHeaderCell Text="Zendesk Ticket #" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Created Date" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Updated Date" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Status" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Text="Priority" HorizontalAlign="Left"/>
    </asp:TableHeaderRow>
</asp:Table>