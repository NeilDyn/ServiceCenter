<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ZendeskIssueReturnLabel.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.IssueReturnLabel.ZendeskIssueReturnLabel" %>

<!-- <link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" /> -->
<script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("[id$=tblExistingZendeskTicket]").hide();
        $("[id$=tblNewZendeskTicket]").hide();
        $("[id$=selectZendeskTicketDropdown]").hide();
        $("[id$=insertZendeskTicketTextbox]").hide();
        $("[id$=cbxZendeskTickets]").prop("checked", false);
        $("[id$=cbxNewZendeskTicket]").prop("checked", false);
        $("[id$=cbxDownloadManually]").prop("checked", false);
        $("[id$=cbxSelectZendeskTicket]").prop("checked", false);
        $("[id$=cbxInsertZendeskTicket]").prop("checked", false);

        $("[id$=cbxSelectZendeskTicket]").change(function () {
            $("[id$=cbxInsertZendeskTicket]").prop("checked", false);

            if ($("[id$=cbxSelectZendeskTicket]").prop("checked") == true) {
                $("[id$=txtFromEmail]").val("");
                $("[id$=txtToEmail]").val("");
                $("[id$=txtInsertZendeskTicket]").val("");

                $("[id$=insertZendeskTicketTextbox]").hide();
                $("[id$=selectZendeskTicketDropdown]").show();
            } else {
                $("[id$=selectZendeskTicketDropdown]").hide();
                $("[id$=ddlZendeskTickets]").prop('selectedIndex', 0);
                $("[id$=txtFromEmail]").val("");
                $("[id$=txtToEmail]").val("");
            }
        });

        $("[id$=cbxInsertZendeskTicket]").change(function () {
            $("[id$=cbxSelectZendeskTicket]").prop("checked", false);

            if ($("[id$=cbxInsertZendeskTicket]").prop("checked") == true) {
                $("[id$=txtFromEmail]").val("");
                $("[id$=txtToEmail]").val("");

                $("[id$=selectZendeskTicketDropdown]").hide();
                $("[id$=insertZendeskTicketTextbox]").show();
            } else {
                 $("[id$=insertZendeskTicketTextbox]").hide();
                 $("[id$=txtInsertZendeskTicket]").val("");              
            }
        });

        $("[id$=cbxZendeskTickets]").change(function () {
            $("[id$=cbxNewZendeskTicket]").prop("checked", false);
            $("[id$=cbxDownloadManually]").prop("checked", false);

            if ($("[id$=cbxZendeskTickets]").prop("checked") == true) {
                $("[id$=tblExistingZendeskTicket]").show();
                $("[id$=tblNewZendeskTicket]").hide();
                var tickets2 = <%= this.zendeskTicketsParsed %>;

                if (tickets2 != null) {
                    $.each(tickets2, function (index, value) {
                        if (value.TicketNo == $("[id$=ddlZendeskTickets]").val()) {
                            $("[id$=txtFromEmail]").val(value.FromEmailAddress);
                            $("[id$=txtToEmail]").val(value.ToEmailsAddress);
                        }
                    });
                }
            } else {
                $("[id$=tblExistingZendeskTicket]").hide();
                $("[id$=ddlZendeskTickets]").prop('selectedIndex', 0);
                $("[id$=txtFromEmail]").val("");
                $("[id$=txtToEmail]").val("");

                $("[id$=cbxSelectZendeskTicket]").prop("checked", false);
                $("[id$=cbxInsertZendeskTicket]").prop("checked", false);
                $("[id$=insertZendeskTicketTextbox]").hide();
                $("[id$=txtInsertZendeskTicket]").val("");    
            }
        });

        $("[id$=cbxNewZendeskTicket]").change(function () {
            $("[id$=tblExistingZendeskTicket]").hide();
            $("[id$=tblNewZendeskTicket]").show();
            $("[id$=cbxZendeskTickets]").prop("checked", false);
            $("[id$=cbxDownloadManually]").prop("checked", false);

            if ($("[id$=cbxNewZendeskTicket]").prop("checked") == true) {
                $("[id$=tblNewZendeskTicket]").show();
            } else {
                $("[id$=tblNewZendeskTicket]").hide();
            }
        });

        $("[id$=cbxDownloadManually]").change(function () {
            $("[id$=tblNewZendeskTicket]").hide();
            $("[id$=tblExistingZendeskTicket]").hide();
            $("[id$=cbxZendeskTickets]").prop("checked", false);
            $("[id$=cbxNewZendeskTicket]").prop("checked", false);
        });

        $("[id$=ddlZendeskTickets]").change(function () {
            var counter = 0;
            var tickets = <%= this.zendeskTicketsParsed %>;
            var selectedIndex = $(this);

            if (selectedIndex != null) {
                $.each(tickets, function (index, value) {
                    if (value.TicketNo == selectedIndex.val()) {
                        $("[id$=txtFromEmail]").val(value.FromEmailAddress);
                        $("[id$=txtToEmail]").val(value.ToEmailsAddress);
                    }
                });
            };
        });

        $("[id$=txtInsertZendeskTicket]").on('input', function () {
            VerifyZendeskTicket($("[id$=txtInsertZendeskTicket]").val());
        });
    });

    function CloseWindow() {
        var c;

        c = window.confirm("Are you sure you wish to cancel issuing this return label?");

        if (c == true) {
            parent.window.close();
        }
    };

    function CloseAfterCreate() {
        parent.window.close();
    };

    function VerifyZendeskTicket(ticketNo) {
        $.ajax({
            type: "POST",
            url: "../../../Webservices/WebServiceFunctions.asmx/GetTicketInformation",
            data: JSON.stringify({ ticketNo: ticketNo }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var zendesk = data.d;
                if (zendesk != null) {
                    $("[id$=txtFromEmail]").val(zendesk.FromEmailAddress);
                    $("[id$=txtToEmail]").val(zendesk.ToEmailsAddress);
                } else {
                    $("[id$=txtFromEmail]").val("");
                    $("[id$=txtToEmail]").val("");
                }
            },
            error: function (xhr, status, text) {
                console.log(xhr.status);
                console.log(xhr.text);
                console.log(xhr.responseText);
            },
        });    
    };

</script>
<asp:Table runat="server" Height="100%" Width="100%">
    <asp:TableRow>
        <asp:TableCell ColumnSpan="4">
            <asp:Table runat="server" Height="100%" Width="100%" ID="tblZendeskInformation">
                <asp:TableRow runat="server" ID="trExistingZendeskTicket">
                    <asp:TableCell Text="Zendesk Ticket:" ForeColor="#0099FF" Font-Bold="true" Style="text-align: left; padding-right: 30px" />
                    <asp:TableCell Width="80%">
                        <asp:CheckBox ID="cbxZendeskTickets" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4">
                        <asp:Table runat="server" Height="100%" Width="100%" ID="tblExistingZendeskTicket">
                            <asp:TableRow>
                                <asp:TableCell Width="20%" Text="Select a Zendesk Ticket" ForeColor="#0099FF" Font-Bold="true" Style="text-align: left; padding-right: 30px" />
                                <asp:TableCell Width="80%">
                                    <asp:CheckBox ID="cbxSelectZendeskTicket" runat="server" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="selectZendeskTicketDropdown">
                                <asp:TableCell Width="20%" Text="Select Zendesk Ticket:" ForeColor="#0099FF" Font-Bold="true" Style="text-align: left; padding-right: 30px" />
                                <asp:TableCell Width="100%">
                                    <asp:DropDownList ID="ddlZendeskTickets" runat="server" Width="50%" CssClass="inputBox" Style="text-align: left;">
                                    </asp:DropDownList>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Width="20%" Text="Insert a Zendesk Ticket" ForeColor="#0099FF" Font-Bold="true" Style="text-align: left; padding-right: 30px" />
                                <asp:TableCell Width="80%">
                                    <asp:CheckBox ID="cbxInsertZendeskTicket" runat="server" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="insertZendeskTicketTextbox">
                                <asp:TableCell Width="20%" Text="Insert Zendesk Ticket:" ForeColor="#0099FF" Font-Bold="true" Style="text-align: left; padding-right: 30px" />
                                <asp:TableCell Width="80%">
                                    <asp:TextBox ID="txtInsertZendeskTicket" Width="50%" runat="server" CssClass="inputBox" TextMode="Number"/>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Text="From:" ForeColor="#0099FF" Font-Bold="true" Style="text-align: left; padding-right: 30px" />
                                <asp:TableCell Width="100%">
                                    <asp:TextBox ID="txtFromEmail" Width="50%" runat="server" CssClass="inputBox" Enabled="false" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Text="To:" ForeColor="#0099FF" Font-Bold="true" Style="text-align: left; padding-right: 30px" />
                                <asp:TableCell Width="100%">
                                    <asp:TextBox ID="txtToEmail" Width="50%" runat="server" CssClass="inputBox" Enabled="false" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="trNewZendeskTicket">
                    <asp:TableCell Text="New Zendesk Ticket:" ForeColor="#0099FF" Font-Bold="true" Style="text-align: left; padding-right: 30px" />
                    <asp:TableCell Width="80%">
                        <asp:CheckBox ID="cbxNewZendeskTicket" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4">
                        <asp:Table runat="server" Height="100%" Width="100%" ID="tblNewZendeskTicket">
                            <asp:TableRow>
                                <asp:TableCell Text="Customer Email Address:" ForeColor="#0099FF" Font-Bold="true" Style="text-align: left; padding-right: 30px" />
                                <asp:TableCell Width="80%">
                                    <asp:TextBox ID="txtcustEmailAddress" Width="50%" runat="server" CssClass="inputBox" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Text="Customer Name:" ForeColor="#0099FF" Font-Bold="true" Style="text-align: left; padding-right: 30px" />
                                <asp:TableCell Width="80%">
                                    <asp:TextBox ID="txtCustomerName" Width="50%" runat="server" CssClass="inputBox" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Text="Download Manually:" ForeColor="#0099FF" Font-Bold="true" Style="text-align: left; padding-right: 30px" />
                    <asp:TableCell Width="80%">
                        <asp:CheckBox ID="cbxDownloadManually" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>               
            </asp:Table>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
