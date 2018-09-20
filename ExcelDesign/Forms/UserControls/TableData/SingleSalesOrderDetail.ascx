<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleSalesOrderDetail.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableData.SingleSalesOrderDetail" %>
<%@ Register Src="~/Forms/UserControls/TableData/DataLines/SalesOrderLines/SingleSalesOrderShipments.ascx" TagName="SingleSalesOrderShipments" TagPrefix="ssos" %>
<%@ Register Src="~/Forms/UserControls/TableData/DataLines/SalesOrderLines/SingleSalesOrderPackages.ascx" TagName="SingleSalesOrderPackages" TagPrefix="ssop" %>
<%@ Register Src="~/Forms/UserControls/TableData/DataLines/SalesOrderLines/SingleSalesOrderTrackingNos.ascx" TagName="SingleSalesOrderTrackingNos" TagPrefix="ssotn" %>
<%@ Register Src="~/Forms/UserControls/TableData/DataLines/SalesOrderLines/SingleSalesOrderComments.ascx" TagName="SingleSalesOrderComments" TagPrefix="ssoc" %>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>
<script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jquery.ui/1.10.2/jquery-ui.min.js"></script>
<link rel="stylesheet" href="//ajax.aspnetcdn.com/ajax/jquery.ui/1.10.2/themes/ui-lightness/jquery-ui.css" type="text/css" />
<script type="text/javascript">
    $(document).ready(function () {
        $("[id$=expandShipments_<%= this.CustID %>_<%= this.CountID %>]").hide();
        $("[id$=expandSerialNos_<%= this.CustID %>_<%= this.CountID %>]").hide();
        $("[id$=expandPackages_<%= this.CustID %>_<%= this.CountID %>]").hide();
        $("[id$=expandOrderComments_<%= this.CustID %>_<%= this.CountID %>]").hide();
        $("[id*=showMoreOrderLines_<%= this.CustID %>_<%= this.CountID %>]").hide();
        var createReturnWindow;
        var partRequestWindow;

        $("[id$=btnCancelOrder_<%= this.CustID %>_<%= this.CountID %>]").click(function () {
            if ("<%= this.tcOrderStatus.Text%>" == "OrderCreated") {
                if ("<%= this.Sh.WarrantyProp.IsPDA %>" == "YES") {
                    if ("<%= this.CanCancelPDAOrder%>" == "true") {
                        var orderNo = "<%= this.OrderNo %>";

                        $.ajax({
                            type: "POST",
                            url: "ServiceCenter.aspx/CancelOrder",
                            data: JSON.stringify({ orderNo: orderNo }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (error) {
                                if (error.d.indexOf("Error") == -1) {
                                    alert("Order " + rmaNo + " cancelled.");
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
                        alert("You do not have the required permission to Cancel PDA Order");
                    }
                } else {
                    if ("<%= this.CanCancelOrder%>" == "true") {
                        var orderNo = "<%= this.OrderNo %>";

                        $.ajax({
                            type: "POST",
                            url: "ServiceCenter.aspx/CancelOrder",
                            data: JSON.stringify({ orderNo: orderNo }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (error) {
                                if (error.d.indexOf("Error") == -1) {
                                    alert("Order " + rmaNo + " cancelled.");
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
                        alert("You do not have the required permission to Cancel Order");
                    }
                }
            } else {
                alert("Order <%= this.OrderNo%> must be not be shipped to be cancelled.");
            }
        });

        $("[id$=btnPartRequest_<%= this.CustID %>_<%= this.CountID %>]").click(function () {
            if ("<%= this.tcStatus.Text.ToUpper() %>" == "OPEN" || <%= this.Sh.WarrantyProp.DaysRemaining %> > -15) {
                if ("<%= this.tcOrderStatus.Text.ToUpper() %>" == "SHIPPED") {
                    var width = 1500;
                    var height = 800;
                    var left = (screen.height - width) + 1500;
                    var top = (screen.height - height);

                    if (typeof (partRequestWindow) == 'undefined' || partRequestWindow.closed) {
                        if ("<%= this.Sh.WarrantyProp.IsPDA %>" == "YES") {
                        if ("<%= this.CanCreatePDAPartRequest%>" == "true") {
                            partRequestWindow = window.open("PDAForms/CreatePartRequest.aspx?No=<%= this.OrderNo %>&ExternalDocumentNo=<%= this.DocNo %>",
                                null, "left=" + left + ",width=" + width + ",height=" + height + ",top=" + top + ",status=no,resizable=no,toolbar=no,location=no,menubar=no,directories=no");
                        } else {
                            alert("You do not have the required permission to create a PDA Part Request.");
                        }
                    } else {
                        if ("<%= this.CanCreatePartRequest%>" == "true") {
                            partRequestWindow = window.open("FunctionForms/CreatePartialRequest.aspx?No=<%= this.OrderNo %>&ExternalDocumentNo=<%= this.DocNo %>",
                                null, "left=" + left + ",width=" + width + ",height=" + height + ",top=" + top + ",status=no,resizable=no,toolbar=no,location=no,menubar=no,directories=no");
                        } else {
                            alert("You do not have the required permission to create a Part Request.");
                        }
                    }

                    function checkIfWinClosed(intervalID) {
                        if (partRequestWindow.closed) {
                            __doPostBack('[id$=btnReload', '');
                            clearInterval(intervalID);
                        }
                    }
                    var interval = setInterval(function () {
                        checkIfWinClosed(interval);
                    }, 1000);

                } else {
                    alert('Please close the current active Part Request dialog window before trying to open a new instance.');
                }

            } else {
                alert("Order <%= this.OrderNo %> has not been shipped and cannot be Part Requested.");
                }
            } else {
                alert("Order <%= this.OrderNo %> is out of warranty range.");
            }
        });

        $("[id$=btnCreateReturn_<%= this.CustID %>_<%= this.CountID %>]").click(function () {
            if ("<%= this.tcStatus.Text.ToUpper() %>" == "OPEN") {
                if ("<%= this.tcOrderStatus.Text.ToUpper() %>" == "SHIPPED") {
                    var width = 1500;
                    var height = 800;
                    var left = (screen.width - width) + 1500;
                    var top = (screen.height - height);

                    if (typeof (createReturnWindow) == 'undefined' || createReturnWindow.closed) {
                        if ("<%= this.Sh.WarrantyProp.IsPDA %>" == "YES") {
                        if ("<%= this.CanReturnPDA %>" == "true") {
                            createReturnWindow = window.open("PDAForms/CreateRMA.aspx?No=<%= this.OrderNo %>&ExternalDocumentNo=<%= this.DocNo %>&CreateOrUpdate=<%= false %>",
                                null, "left=" + left + ",width=" + width + ",height=" + height + ",top=" + top + ",status=no,resizable=no,toolbar=no,location=no,menubar=no,directories=no");
                        } else {
                            alert("You do not have the required permission to create a PDA Replacement Return Order.");
                        }
                    } else {
                        if ("<%= this.CanReturn %>" == "true") {
                            createReturnWindow = window.open("FunctionForms/CreateReturn.aspx?No=<%= this.OrderNo %>&ExternalDocumentNo=<%= this.DocNo %>&CreateOrUpdate=<%= false %>",
                                null, "left=" + left + ",width=" + width + ",height=" + height + ",top=" + top + ",status=no,resizable=no,toolbar=no,location=no,menubar=no,directories=no");
                        } else {
                            alert("You do not have the required permission to create a Return Order.");
                        }
                    }

                    function checkIfWinClosed(intervalID) {
                        if (createReturnWindow.closed) {
                            __doPostBack('[id$=btnReload', '');
                            clearInterval(intervalID);
                        }
                    }
                    var interval = setInterval(function () {
                        checkIfWinClosed(interval);
                    }, 1000);
                } else {
                    alert('Please close the current active Create Return Order dialog window before trying to open a new instance.');
                }
            } else {
                alert("Order <%= this.OrderNo %> has no shipped items and cannot be returned.");
                }
            }
            else {
                alert("Warranty status is not OPEN for current order: <%= this.OrderNo %>.");
            }
        });

        $("[id$=btnIssueRefund_<%= this.CustID %>_<%= this.CountID %>]").click(function () {
            alert("Hi, order <%= this.Sh.SalesOrderNo %> can be Refunded.");
        });
    });

    function expandMoreOrderLines<%= this.CustID %><%= this.CountID %>(lineID) {
        if ($("a#expandMoreClickOrderLine_<%=this.CustID %>_<%= this.CountID %>_" + lineID).text() == "Show More") {
            $("a#expandMoreClickOrderLine_<%=this.CustID %>_<%= this.CountID %>_" + lineID).text("Show Less");
        }
        else {
            $("a#expandMoreClickOrderLine_<%=this.CustID %>_<%= this.CountID %>_" + lineID).text("Show More");
        }

        $("[id*=showMoreOrderLines_<%= this.CustID %>_<%= this.CountID %>_" + lineID + "]").toggle();
    };

    function expandShipments<%=this.CustID %><%= this.CountID %>() {
        $("[id$=expandShipments_<%= this.CustID %>_<%= this.CountID %>]").toggle();
    };

    function expandSerialNos<%=this.CustID %><%= this.CountID %>() {
        $("[id$=expandSerialNos_<%= this.CustID %>_<%= this.CountID %>]").toggle();
    };

    function expandPackages<%=this.CustID %><%= this.CountID %>() {
        $("[id$=expandPackages_<%= this.CustID %>_<%= this.CountID %>]").toggle();
    };

    function expandOrderComments<%=this.CustID %><%= this.CountID %>() {
        $("[id$=expandOrderComments_<%= this.CustID %>_<%= this.CountID %>]").toggle();
        return false;
    };
</script>

<asp:Table ID="tblSingleSalesOrderDetail" runat="server" Height="100%" Width="100%">
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Order Status:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcOrderStatus" />
        <asp:TableCell Text="Is Exchange Order:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcIsExchangeOrder" />
        <asp:TableCell Text="RMA #:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" ID="tcRMANoTitle" />
        <asp:TableCell runat="server" ID="tcRMANo" ForeColor="Red" />
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell />
        <asp:TableCell />
        <asp:TableCell Text="Is Part Request:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcIsPartRequest" />
        <asp:TableCell Text="Original Order No:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" ID="tcOrderNoTitle" />
        <asp:TableCell runat="server" ID="tcOriginalOrderNo" />
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <br />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Order Date:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcOrderDate" />
        <asp:TableCell Text="Shipment Date:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcShipmentDate" />
        <asp:TableCell Text="Warranty" Font-Bold="true" HorizontalAlign="Center" ColumnSpan="2" />
        <asp:TableCell />
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Sales Order No:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" ID="lblOrderLabelNo" />
        <asp:TableCell runat="server" ID="tcSalesOrderNo" />
        <asp:TableCell Text="Shipments:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcShipmentsTotal" />
        <asp:TableCell Text="Status:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcStatus" />
        <asp:TableCell />
    </asp:TableRow>
    <asp:TableRow runat="server" ID="expandShipments" TableSection="TableBody" HorizontalAlign="Justify">
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Channel Name:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcChannelName" />
        <asp:TableCell Text="Package(s):" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcPackagesCount" />
        <asp:TableCell Text="Policy:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcPolicy" />
    </asp:TableRow>
    <asp:TableRow runat="server" ID="expandPackages" TableSection="TableBody" HorizontalAlign="Justify">
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Zendesk Ticket #:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcZendeskTicketNo">
            <asp:TextBox ID="txtZendeskNo" runat="server"></asp:TextBox>
        </asp:TableCell>
        <asp:TableCell Text="Ship Method:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcShipMethod" />
        <asp:TableCell Text="Days Remaining:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcDaysRemaining" />
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell Text="Zendesk Ticket(s):" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcZendeskTickets" />
        <asp:TableCell Text="Tracking #:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcTrackingNo" />
        <asp:TableCell Text="Warranty Type:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server" ID="tcWarrantyType" />
    </asp:TableRow>
    <asp:TableRow runat="server" ID="expandSerialNos" TableSection="TableBody" HorizontalAlign="Justify">
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell ID="lblOrderComment" Text="Comments:" Font-Bold="true" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableCell runat="server">
            <asp:ImageButton ID="imgOrderComments" runat="server" ImageUrl="~/images/sketch.png" Width="25" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server" ID="expandOrderComments" TableSection="TableBody" HorizontalAlign="Justify">
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell runat="server" ID="tcPDAStamp" ColumnSpan="8"> 
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell ColumnSpan="8">
            <asp:Table runat="server" ID="tblOrderDetailLines" Height="100%" Width="100%">
                <asp:TableHeaderRow ForeColor="White" BackColor="#507CD1">
                    <asp:TableHeaderCell Text="Item No." HorizontalAlign="Left" />
                    <asp:TableHeaderCell Text="Description" HorizontalAlign="Left" Width="30%" />
                    <asp:TableHeaderCell Text="Qty" />
                    <asp:TableHeaderCell Text="Qty Shipped" />
                    <asp:TableHeaderCell Text="Price" HorizontalAlign="Left" />
                    <asp:TableHeaderCell Text="Line Amt" HorizontalAlign="Left" />
                    <asp:TableHeaderCell Text="Serial #" HorizontalAlign="Center" />
                    <asp:TableHeaderCell Text="" />
                </asp:TableHeaderRow>
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell ColumnSpan="8">
                        <hr class="HeaderLine"/>
                    </asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
