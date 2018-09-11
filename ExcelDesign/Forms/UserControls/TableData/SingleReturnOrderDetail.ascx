<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleReturnOrderDetail.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableData.SingleReturnOrderDetail" %>
<%@ Register Src="~/Forms/UserControls/TableData/DataLines/ReturnOrderLines/SingleReturnOrderReceipts.ascx" TagName="SingleReturnOrderReceipts" TagPrefix="sror" %>
<%@ Register Src="~/Forms/UserControls/TableData/DataLines/ReturnOrderLines/SingleReturnOrderPackages.ascx" TagName="SingleReturnOrderPackages" TagPrefix="srop" %>
<%@ Register Src="~/Forms/UserControls/TableData/DataLines/ReturnOrderLines/SingleReturnOrderComments.ascx" TagName="SingleReturnOrderComments" TagPrefix="sroc" %>
<%@ Register Src="~/Forms/UserControls/TableData/DataLines/ReturnOrderLines/SingleReturnOrderExchangeNos.ascx" TagName="SingleReturnOrderExchangeNos" TagPrefix="sroen" %>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>
<script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jquery.ui/1.10.2/jquery-ui.min.js"></script>
<link rel="stylesheet" href="//ajax.aspnetcdn.com/ajax/jquery.ui/1.10.2/themes/ui-lightness/jquery-ui.css" type="text/css" />
<script type="text/javascript">
    $(document).ready(function () {
        $("[id$=expandReceipts_<%= this.CustID %>_<%= this.CountID %>]").hide();
        $("[id$=expandReceives_<%= this.CustID %>_<%= this.CountID %>]").hide();
        $("[id*=showMoreReturnLines_<%= this.CustID %>_<%= this.CountID %>]").hide();
        $("[id*=expandMultipleExchange<%= this.CustID %>_<%= this.CountID %>]").hide();
        $("[id$=expandReturnComments_<%= this.CustID %>_<%= this.CountID %>]").hide();
        var updateRMAWin;
        var createExchangeWin;

        $("[id$=btnCreateExchange<%= this.CustID %>_<%= this.CountID %>]").click(function () {
            var width = 1500;
            var height = 500;
            var left = (screen.width - width) + 500;
            var top = (screen.height - height) * 0.5;

            if (typeof (createExchangeWin) == 'undefined' || createExchangeWin.closed) {
                if ("<%= this.tcIMEINo.Text %>" != "") {
                    if ("<%= this.CanExchangePDA %>" == "true") {
                        createExchangeWin = window.open("FunctionForms/CreateExchange.aspx?RMANo=<%= this.RMANo %>&ExternalDocumentNo=<%= this.DocNo %>",
                            null,
                            "left=" + left + ",width=" + width + ",height=" + height + ",top=" + top + ",status=no,resizable=no,toolbar=no,location=no,menubar=no,directories=no");
                    } else {
                        alert("You do not have the required permission to create a PDA Exchange Order.");
                    }
                } else {
                    if ("<%= this.CanExchange %>" == "true") {
                        createExchangeWin = window.open("FunctionForms/CreateExchange.aspx?RMANo=<%= this.RMANo %>&ExternalDocumentNo=<%= this.DocNo %>",
                            null,
                            "left=" + left + ",width=" + width + ",height=" + height + ",top=" + top + ",status=no,resizable=no,toolbar=no,location=no,menubar=no,directories=no");
                    } else {
                        alert("You do not have the required permission to create an Exchange Order.");
                    }
                }

                function checkIfWinClosed(intervalID) {
                    if (createExchangeWin.closed) {
                        __doPostBack('[id$=btnReload', '');
                        clearInterval(intervalID);
                    }
                }
                var interval = setInterval(function () {
                    checkIfWinClosed(interval);
                }, 1000);
            } else {
                alert('Please close the current active Create Exchange Order dialog window before trying to open a new instance.');
            }
        });

        $("[id$=btnIssueRefund<%= this.CustID %>_<%= this.CountID %>]").click(function () {
            alert("Hi, return <%= this.Rh.RMANo %> can be refunded.");
        });

        $("[id$=btnPrintRMAInstructions<%= this.CustID%>_<%= this.CountID %>]").click(function () {
            window.open("FunctionForms/RMAPDFForm.aspx?RMANo=<%= this.Rh.RMANo %>", "_blank");
        });

        $("[id$=btnUpdateRMA<%= this.CustID %>_<%= this.CountID %>]").click(function () {
            if ("<%= this.tcReturnStatus.Text%>" == "Open") {
                var width = 1500;
                var height = 500;
                var left = (screen.width - width) + 500;
                var top = (screen.height - height) * 0.5;

                if (typeof (updateRMAWin) == 'undefined' || updateRMAWin.closed) {
                    if ("<%= this.tcIMEINo.Text %>" != "") {
                    if ("<%= this.CanReturnPDA%>" == "true") {
                        updateRMAWin = window.open("PDAForms/CreateRMA.aspx?No=<%= this.RMANo %>&ExternalDocumentNo=<%= this.DocNo %>&CreateOrUpdate=<%= true %>&ReturnTrackingNo=<%= this.Rh.ReturnTrackingNo %>",
                            null, "left=" + left + ",width=" + width + ",height=" + height + ",top=" + top + ",status=no,resizable=no,toolbar=no,location=no,menubar=no,directories=no");
                    } else {
                        alert("You do not have the required permission to update a PDA Replacement Return Order");
                    }
                } else {
                    if ("<%= this.CanReturn %>" == "true") {
                        updateRMAWin = window.open("FunctionForms/CreateReturn.aspx?No=<%= this.RMANo %>&ExternalDocumentNo=<%= this.DocNo %>&CreateOrUpdate=<%= true %>&ReturnTrackingNo=<%= this.Rh.ReturnTrackingNo %>",
                                null, "left=" + left + ",width=" + width + ",height=" + height + ",top=" + top + ",status=no,resizable=no,toolbar=no,location=no,menubar=no,directories=no");
                        } else {
                            alert("You do not have the required permission to update a return order.");
                        }
                    }

                    function checkIfWinClosed(intervalID) {
                        if (updateRMAWin.closed) {
                            __doPostBack('[id$=btnReload', '');
                            clearInterval(intervalID);
                        }
                    }
                    var interval = setInterval(function () {
                        checkIfWinClosed(interval);
                    }, 1000);
                } else {
                    alert('Please close the current active Update RMA dialog window before trying to open a new instance.');
                }
            } else {
                alert("Return Order <%= this.RMANo %> has already been fully received and cannot be updated.");
            }
        });

        $("[id$=btnIssueReturnLabel<%= this.CustID %>_<%= this.CountID %>]").click(function () {
            if ("<%= this.UPSLabelCreated %>" == "false") {
                if ("<%= this.CanIssueLabel %>" == "true") {
                    if ("<%= this.tcReturnStatus.Text %>" == "Open") {
                    var rmaNo = "<%= this.Rh.RMANo %>";
                        var emailIn = prompt("Please enter a valid email address:");

                        if (emailIn == null || emailIn == "") {
                            alert("Invalid email address entered.");
                        }
                        else {
                            if (validateEmail(emailIn)) {
                                $.ajax({
                                    type: "POST",
                                    url: "ServiceCenter.aspx/IssueReturnLabel",
                                    data: JSON.stringify({ rmaNo: rmaNo, email: emailIn }),
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (error) {
                                        if (error.d.indexOf("Error") == -1) {
                                            alert("Return Label Created for Return: " + rmaNo + " and is being processed and will be emailed within 1-2 hours");
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
                                alert("Invalid email address entered.")
                            }
                        }
                    } else {
                        alert("Only Return Orders that have an 'OPEN' return status can be issued Return Labels.");
                    }
                } else {
                    alert("You do not have the required permission to issue a return label.");
                }
            } else {
                alert("UPS Return Label has already been created.");
            }
        });
    });

    function expandMoreReturnLines<%= this.CustID %><%= this.CountID %>(lineID) {
        if ($("a#expandMoreClickReturnLine_<%=this.CustID %>_<%= this.CountID %>_" + lineID).text() == "Show More") {
            $("a#expandMoreClickReturnLine_<%=this.CustID %>_<%= this.CountID %>_" + lineID).text("Show Less");
        }
        else {
            $("a#expandMoreClickReturnLine_<%=this.CustID %>_<%= this.CountID %>_" + lineID).text("Show More");
        }

        $("[id*=showMoreReturnLines_<%= this.CustID %>_<%= this.CountID %>_" + lineID + "]").toggle();
    };

    function expandReceipts<%=this.CustID %><%= this.CountID %>() {
        $("[id$=expandReceipts_<%= this.CustID %>_<%= this.CountID %>]").toggle();
    };


    function expandReceives<%=this.CustID %><%= this.CountID %>() {
        $("[id$=expandReceives_<%= this.CustID %>_<%= this.CountID %>]").toggle();
    };

    function expandMultipleExchange<%=this.CustID %><%= this.CountID %>() {
        $("[id*=expandMultipleExchange<%= this.CustID %>_<%= this.CountID %>]").toggle();
    };

    function expandReturnComments<%=this.CustID %><%= this.CountID %>() {
        $("[id*=expandReturnComments_<%= this.CustID %>_<%= this.CountID %>]").toggle();
        return false;
    };

    function validateEmail(email) {
        var re = /^(?:[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])$/;
        return re.test(email);
    };

</script>
<asp:Table ID="tblSingleReturnOrderDetail" runat="server" Height="100%" Width="100%">
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Return Status:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcReturnStatus" />
        <asp:TableCell Text="Exchange Status:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcExchangeStatus" />
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <br />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Date Created:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" Font />
        <asp:TableCell runat="server" ID="tcDateCreated" />
        <asp:TableCell Text="Receipt Date:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcReceiptDate" />
        <asp:TableCell Text="Return Tracking #:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcReturnTrackingNo" />
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Channel Name:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcChannelName" />
        <asp:TableCell Text="Receipts:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcReceiptsTotal" />
        <asp:TableCell Text="Order Date:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcOrderDate" />
    </asp:TableRow>
    <asp:TableRow runat="server" ID="expandReceipts" TableSection="TableBody" HorizontalAlign="Justify">
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Zendesk Ticket #:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcZendeskTicketNo">
            <asp:TextBox ID="txtZendeskTicketNo" runat="server"></asp:TextBox>
        </asp:TableCell>
        <asp:TableCell Text="Package(s):" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcPackagesCount" />
        <asp:TableCell Text="Email:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcEmail" />
    </asp:TableRow>
    <asp:TableRow runat="server" ID="expandReceives" TableSection="TableBody" HorizontalAlign="Justify">
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Zendesk Ticket(s):" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcZendeskTickets" />
        <asp:TableCell Text="UPS Return Label Created:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcUPSReturnLabelCreated" />
        <asp:TableCell Text="Exchange Order No(s):" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcExchangeOrderNo" />
    </asp:TableRow>
    <asp:TableRow runat="server" ID="expandMultipleExchange" TableSection="TableBody" HorizontalAlign="Justify">
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell ID="lblReturnComment" Text="Comments:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell>
            <asp:ImageButton ID="imgReturnComments" runat="server" ImageUrl="~/images/sketch.png" Width="25" />
        </asp:TableCell>
        <asp:TableCell Text="IMEI No:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcIMEINo" />
    </asp:TableRow>
    <asp:TableRow runat="server" ID="expandReturnComments" TableSection="TableBody" HorizontalAlign="Justify">
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell />
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell ColumnSpan="12">
            <asp:Table runat="server" ID="tblReturnDetailLines" Height="100%" Width="100%">
                <asp:TableHeaderRow ForeColor="White" BackColor="#507CD1">
                    <asp:TableHeaderCell Text="Item No." HorizontalAlign="Left" />
                    <asp:TableHeaderCell Text="Description" HorizontalAlign="Left" Width="25%" />
                    <asp:TableHeaderCell Text="Qty" />
                    <asp:TableHeaderCell Text="Qty Received" />
                    <asp:TableHeaderCell Text="Qty Exchanged" />
                    <asp:TableHeaderCell Text="Qty Refunded" />
                    <asp:TableHeaderCell Text="Price" HorizontalAlign="Left" />
                    <asp:TableHeaderCell Text="Line Amt" HorizontalAlign="Left" />
                    <asp:TableHeaderCell Text="Return Reason" />
                    <asp:TableHeaderCell Text="REQ Return Action" />
                    <asp:TableHeaderCell Text="Serial #" />
                    <asp:TableHeaderCell Text="" />
                </asp:TableHeaderRow>
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell ColumnSpan="12">
                        <hr class="HeaderLine"/>
                    </asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
