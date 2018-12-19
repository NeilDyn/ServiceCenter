<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleSalesOrderZendeskTickets.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableData.DataLines.SalesOrderLines.SingleSalesOrderZendeskTickets" %>

<link href="../../../../../css/mainpage.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>
<script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jquery.ui/1.10.2/jquery-ui.min.js"></script>
<script type="text/javascript">
    function UpdateZendeskTicket(ticketNo) {
        var currentTicketNo = ticketNo;
        var updateTicketNo = prompt("Please insert a Zendesk Ticket #.");
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

        return false;
    };
</script>
<asp:Table ID="SingleSalesOrderZendeskTicketsTable" runat="server" Height="100%" Width="50%" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px">
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
