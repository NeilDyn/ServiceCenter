<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleSalesOrderZendeskTickets.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableData.DataLines.SalesOrderLines.SingleSalesOrderZendeskTickets" %>

<link href="../../../../../css/mainpage.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>
<script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jquery.ui/1.10.2/jquery-ui.min.js"></script>
<script type="text/javascript">

    function DeleteOrderZendeskTicketNo() {
        var currentTicketNo = $("[id$=lblCurrentZendeskTicketNo]").text();
        var updateTicketNo = $("[id$=txtUpdateZendeskTicketNo]").val();
            $.ajax({
                type: "POST",
                url: "ServiceCenter.aspx/DeleteZendeskTicket",
                data: JSON.stringify({ currentTicketNo: currentTicketNo }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (error) {
                    if (error.d.indexOf("Error") == -1) {
                        alert('Successfully deleted Zendesk Ticket No.');
                        __doPostBack('[id$=btnReload', '');
                    } else {
                        alert(error.d);
                    }
                },
                error: function (xhr, status, text) {
                    console.log(xhr.status);
                    console.log(xhr.text);
                    console.log(xhr.responseText);
                },
            });

        CloseOrderDialog();
        return false;
    };

    function UpdateOrderZendeskTicket() {
        var currentTicketNo = $("[id$=lblCurrentZendeskTicketNo]").text();
        var updateTicketNo = $("[id$=txtUpdateZendeskTicketNo]").val();
        if (($.isNumeric(updateTicketNo) && updateTicketNo.length == 7)) {
            $.ajax({
                type: "POST",
                url: "ServiceCenter.aspx/UpdateZendeskTicket",
                data: JSON.stringify({ currentTicketNo: currentTicketNo, updateTicketNo: updateTicketNo }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (error) {
                    if (error.d.indexOf("Error") == -1) {
                        alert('Successfully updated Zendesk Ticket No.');
                        __doPostBack('[id$=btnReload', '');
                    } else {
                        alert(error.d);
                    }
                },
                error: function (xhr, status, text) {
                    console.log(xhr.status);
                    console.log(xhr.text);
                    console.log(xhr.responseText);
                },
            });
        } else {
            alert("Zendesk Ticket # should be only 7 numeric characters.");
        }

        CloseOrderDialog();
        return false;
    };

    function CloseOrderDialog() {
        $('#zendeskOptionsOrders').dialog('close');
        return false;
    };

    function ZendeskOptionsOrders(ticketNo) {
        $('#zendeskOptionsOrders').dialog({
            title: 'Please select an option.',
            show: true,
            modal: true
        });

        $("[id$=lblCurrentZendeskTicketNo]").text(ticketNo);

        return false;
    };
</script>

<div id="zendeskOptionsOrders" style="display: none">
    <asp:Label ID="lblCurrentZendeskTicketNo" runat="server" />
    <br />
    <asp:TextBox ID="txtUpdateZendeskTicketNo" runat="server" />
    <br />
    <asp:Button ID="btnUpdateZendeskTicketNo" Text="Update Zendesk Ticket" runat="server" OnClientClick="UpdateOrderZendeskTicket()"/>
    <asp:Button ID="btnDeleteZendeskTicketNo" Text="Delete Zendesk Ticket" runat="server" OnClientClick="DeleteOrderZendeskTicketNo()"/>
    <asp:Button ID="btnCancelZendeskOptions" Text="Cancel" runat="server" OnClientClick="CloseOrderDialog()"/>
</div>

<asp:Table ID="SingleSalesOrderZendeskTicketsTable" runat="server" Height="100%" Width="75%" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px">
    <asp:TableHeaderRow ForeColor="White" BackColor="#507CD1">
        <asp:TableHeaderCell Text="Zendesk Ticket #" HorizontalAlign="Left" />
        <asp:TableHeaderCell Text="Created Date" HorizontalAlign="Left" />
        <asp:TableHeaderCell Text="Updated Date" HorizontalAlign="Left" />
        <asp:TableHeaderCell Text="Subject" HorizontalAlign="Left" />
        <asp:TableHeaderCell Text="Status" HorizontalAlign="Left" />
        <asp:TableHeaderCell Text="Priority" HorizontalAlign="Left" />
        <asp:TableHeaderCell Text="Update Ticket No" HorizontalAlign="Left" />
    </asp:TableHeaderRow>
</asp:Table>
