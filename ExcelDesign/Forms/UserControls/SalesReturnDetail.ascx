<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesReturnDetail.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.SalesReturnDetail" %>
<link href="../../css/mainpage.css" rel="stylesheet" type="text/css" />

<div id="ReturnDetail" runat="server" class="ReturnDetail">
    <div style="float:left"><asp:Label ID="lblOrderSequence" Font-Bold="True" runat="server" style="text-decoration:underline" Text="Order" /></div>
    <div style="float:right; margin-right:100px"><asp:Label ID="lblExternalDocumentNo" runat="server" /></div>
    <div style="float:right; margin-right:100px"><asp:Label ID="Label13" runat="server" Text="External Document No:"/></div>  
    <div style="float:right; margin-right:100px"><asp:Label ID="lblRMANo" runat="server" /></div>
    <div style="float:right; margin-right:100px"><asp:Label ID="Label12" runat="server" Text="RMA No:"/></div>  
    <br />                

    <div id="ReturnDetailHeaderCaptions" runat="server" class="detailedReturnCaption">
        <asp:Label ID="lblReturnStatusCaption" runat="server" Text="Return Status:" />
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Date Created:" />
        <br />
        <asp:Label ID="Label5" runat="server" Text="Channel Name:" />
        <br />
        <asp:Label ID="Label6" runat="server" Text="Zendesk Ticket #:" />
        <br />
        <asp:Label ID="Label7" runat="server" Text="Zendesk Ticket(s):" />
    </div>
    <div id="ReturnDetailHeaderInfo" runat="server" class="detailedReturnInfo">
        <asp:Label ID="lblReturnStatus" runat="server" />
        <br />
        <br />
        <asp:Label ID="lblDateCreated" runat="server" />
        <br />
        <asp:Label ID="lblChannelName" runat="server"/>
        <br />
        <asp:Label ID="lblZendeskTicket" runat="server" />
        <br />
        <asp:Label ID="lblZendeskTicketNo" runat="server" />
    </div>
    <div id="ReturnDetailHeaderCaptionsCon" class="returnOrderCaptionCon">
        <asp:Label ID="Label2" runat="server" Text="Receipt Date:" />
        <br />
        <asp:Label ID="Label8" runat="server" Text="Receipts:" />
        <br />
        <asp:Label ID="Label9" runat="server" Text="Package(s):" />
        <br />
    </div>
    <div id="ReturnDetailHeaderInfoCon" class ="returnOrderInfoCon">
        <asp:Label ID="lblReceiptDate" runat="server" />
        <br />
        <asp:Label ID="lblReceipts" runat="server" />
        <br />
        <asp:Label ID="lblPackages" runat="server" />
        <br />
    </div>
    <div id="ReturnDetailHeaderCaptionsCon2" class ="returnOrderCaptionCon2">

        <asp:Label ID="Label14" runat="server" Text="Return Tracking #:"></asp:Label>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Order Date:"></asp:Label>
    </div>
    <div id="ReturnDetailHeaderInfoCon2" class ="returnOrderInfoCon2">
        <asp:Label ID="lblReturnTrackingNo" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lblOrderDate" runat="server" Text=""></asp:Label>
     </div>

    <br />
    <br />

    <div runat="server">
        <asp:GridView ID="gdvReturnVView" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Height="16px" Width="100%">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>

        <div class="Options">
        <ul style="height: 32px">
            <li>        
                <asp:Button ID="btnIssueRefund" runat="server" Text="Issue Refund" />                              
            </li>
            <li>                 
                <asp:Button ID="btnCreateExchange" runat="server" Text="Create Exchange" />
            </li>
        </ul>
    </div>
    </div>
</div>
    <br />