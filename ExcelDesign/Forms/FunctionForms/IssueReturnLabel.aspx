<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IssueReturnLabel.aspx.cs" Inherits="ExcelDesign.Forms.FunctionForms.IssueReturnLabel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Issue Return Label</title>
    <link href="../../css/mainpage.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$=tblExistingZendeskTicket]").hide();
            $("[id$=tblNewZendeskTicket]").hide();           
            $("[id$=cbxZendeskTickets]").prop("checked", false);
            $("[id$=cbxNewZendeskTicket]").prop("checked", false);
            $("[id$=cbxDownloadManually]").prop("checked", false);       

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
                }            
            });

            $("[id$=cbxNewZendeskTicket]").change(function () {
                $("[id$=tblExistingZendeskTicket]").hide();
                $("[id$=tblNewZendeskTicket]").show();
                $("[id$=cbxZendeskTickets]").prop("checked", false);
                $("[id$=cbxDownloadManually]").prop("checked", false);

                if ($("[id$=cbxNewZendeskTicket]").prop("checked") == true) {
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

    </script>
</head>
<body>
    <form id="frmIssueReturnLabel" runat="server">
        <asp:Table ID="tblIssueReturnLabel" runat="server" Height="100%" Width="100%">
            <asp:TableHeaderRow HorizontalAlign="Left">
                <asp:TableHeaderCell Text="Order No:" ID="noTitle" ForeColor="#0099FF" Font-Bold="true" Font-Size="Large" />
                <asp:TableHeaderCell ID="tcNo" runat="server" />
            </asp:TableHeaderRow>
            <asp:TableHeaderRow HorizontalAlign="Left">
                <asp:TableHeaderCell Text="External Document No:" ForeColor="#0099FF" Font-Bold="true" Font-Size="Large" />
                <asp:TableHeaderCell ID="tcDocNo" runat="server" />
            </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>
                    <br />
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Text="Shipping Information:" HorizontalAlign="Left" Font-Underline="true" Font-Bold="true" />
            </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Text="Ship from Name:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF" />
                <asp:TableHeaderCell ID="tcShipFromName" runat="server" HorizontalAlign="Left" Style="text-align: left" />
                <asp:TableHeaderCell Text="Ship from City:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF" />
                <asp:TableHeaderCell ID="tcShipFromCity" runat="server" HorizontalAlign="Left" Style="text-align: left" />
            </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Text="Ship from Address 1:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF" />
                <asp:TableHeaderCell ID="tcShipFromAddress1" runat="server" HorizontalAlign="Left" Style="text-align: left" />
                <asp:TableHeaderCell Text="Ship from State:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF" />
                <asp:TableHeaderCell ID="tcShipFromState" runat="server" HorizontalAlign="Left" Style="text-align: left" />
            </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Text="Ship from Address 2:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF" />
                <asp:TableHeaderCell ID="tcShipFromAddress2" runat="server" HorizontalAlign="Left" Style="text-align: left" />
                <asp:TableHeaderCell Text="Ship from Code:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF" />
                <asp:TableHeaderCell ID="tcShipFromCode" runat="server" HorizontalAlign="Left" Style="text-align: left" />
            </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>
                    <br />
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
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
                                        <asp:TableCell Width="20%" Text="Select Zendesk Ticket:" ForeColor="#0099FF" Font-Bold="true" Style="text-align: left; padding-right: 30px" />
                                        <asp:TableCell Width="100%">
                                            <asp:DropDownList ID="ddlZendeskTickets" runat="server" Width="50%" CssClass="inputBox" Style="text-align: left;">
                                            </asp:DropDownList>
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
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                             <asp:TableCell Text="Download Manually:" ForeColor="#0099FF" Font-Bold="true" Style="text-align: left; padding-right: 30px" />
                            <asp:TableCell Width="80%">
                                <asp:CheckBox ID="cbxDownloadManually" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableFooterRow HorizontalAlign="Right">
                            <asp:TableHeaderCell />
                            <asp:TableHeaderCell HorizontalAlign="Right">
                                <asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClientClick="CloseWindow();" />
                                <asp:Button ID="BtnIssueReturnLabel" runat="server" Text="Issue Return Label" OnClick="BtnIssueReturnLabel_Click" />
                            </asp:TableHeaderCell>
                        </asp:TableFooterRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
