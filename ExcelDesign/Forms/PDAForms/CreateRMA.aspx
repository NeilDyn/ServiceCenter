<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateRMA.aspx.cs" Inherits="ExcelDesign.Forms.PDAForms.CreateRMA" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PDA - Create Return Order</title>
    <link href="../../css/mainpage.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function CloseWindow() {
            var c;

            if ("<%= this.btnCreateRMA.Text %>" == "Update RMA") {
                c = window.confirm("Are you sure you wish to cancel updating this return?");
            } else {
                c = window.confirm("Are you sure you wish to cancel creating this return?");
            }

            if (c == true) {
                parent.window.close();
            };
        };

        function CloseAfterCreate() {
            parent.window.close();
        };

        function OpenCreatedRMA() {
            var width = 1500;
            var height = 500;
            var left = (screen.width - width) + 500;
            var top = (screen.height - height) * 0.5;

            window.open("CreatedPDARMA.aspx?PrintRMAInstructions=<%= this.printRMA %>&OrderNo=<%= this.tcNo.Text%>&ExternalDocumentNo=<%= this.tcDocNo.Text%>",
                null,
                "left=" + left + ",width=" + width + ",height=" + height + ",top=" + top + ",status=no,resizable=no,toolbar=no,location=no,menubar=no,directories=no");
        };
    </script>
</head>
<body>
    <form id="frmCreatePDARMA" runat="server">
        <asp:Table ID="tblPDARMAInfo" runat="server" Height="100%" Width="100%">
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
                <asp:TableHeaderCell Text="Ship to Name:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF" />
                <asp:TableHeaderCell runat="server" HorizontalAlign="Left" Style="text-align: left">
                    <asp:TextBox ID="txtShipToName" runat="server" CssClass="inputBox" />
                    &nbsp<asp:RequiredFieldValidator ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtShipToName" runat="server" />
                </asp:TableHeaderCell>
                <asp:TableHeaderCell Text="Ship to City:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF" />
                <asp:TableHeaderCell runat="server" HorizontalAlign="Left" Style="text-align: left">
                    <asp:TextBox ID="txtShipToCity" runat="server" CssClass="inputBox" />
                    &nbsp<asp:RequiredFieldValidator ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtShipToCity" runat="server" />
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Text="Ship to Address 1:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF" />
                <asp:TableHeaderCell runat="server" HorizontalAlign="Left" Style="text-align: left">
                    <asp:TextBox ID="txtShipToAddress1" runat="server" CssClass="inputBox" />
                    &nbsp<asp:RequiredFieldValidator ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtShipToAddress1" runat="server" />
                </asp:TableHeaderCell>
                <asp:TableHeaderCell Text="Ship to State:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF" />
                <asp:TableHeaderCell runat="server" HorizontalAlign="Left" Style="text-align: left">
                    <asp:TextBox ID="txtShipToState" runat="server" CssClass="inputBox" />
                    &nbsp<asp:RequiredFieldValidator ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtShipToState" runat="server" />
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Text="Ship to Address 2:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF" />
                <asp:TableHeaderCell runat="server" HorizontalAlign="Left" Style="text-align: left">
                    <asp:TextBox ID="txtShipToAddress2" runat="server" CssClass="inputBox" />
                </asp:TableHeaderCell>
                <asp:TableHeaderCell Text="Ship to Code:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF" />
                <asp:TableHeaderCell runat="server" HorizontalAlign="Left" Style="text-align: left">
                    <asp:TextBox ID="txtShipToCode" runat="server" CssClass="inputBox" />
                    &nbsp<asp:RequiredFieldValidator ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtShipToCode" runat="server" />
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
             <asp:TableHeaderRow>
                <asp:TableHeaderCell>
                    <br />
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="6">
                    <asp:Table runat="server" ID="tblCreateReturnOrderTableDetails" Height="100%" Width="100%">
                        <asp:TableHeaderRow ForeColor="White" BackColor="#507CD1">
                            <asp:TableHeaderCell Text="Item No." HorizontalAlign="Left" ID="HeaderItem" />
                            <asp:TableHeaderCell Text="Description" HorizontalAlign="Left" Width="30%" ID="HeaderDesc" />
                            <asp:TableHeaderCell Text="Qty" ID="HeaderQty" />
                            <asp:TableHeaderCell Text="Action Qty." ID="HeaderActionQty" Width="8%" />
                            <asp:TableHeaderCell Text="Return Reason Code" HorizontalAlign="Left" ID="HeaderReturnReasonCode" />
                            <asp:TableHeaderCell Text="REQ Return Action" HorizontalAlign="Left" ID="HeaderReturnAction" />
                        </asp:TableHeaderRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <br />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="4">
                    <asp:Table runat="server" Height="100%" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell Text="Notes: " ForeColor="#0099FF" Font-Bold="true" Style="text-align: right; padding-right: 30px" />
                            <asp:TableCell Width="100%">
                                <asp:TextBox ID="txtNotes" Width="100%" runat="server" TextMode="MultiLine" MaxLength="160" CssClass="inputBox" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Text="Customer Email Address: " ForeColor="#0099FF" Font-Bold="true" Style="text-align: right; padding-right: 30px" />
                            <asp:TableCell Width="100%">
                                <asp:TextBox ID="txtCustEmail" Width="50%" runat="server" CssClass="inputBox" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Text="Include Resource Lines: " Width="20%" ForeColor="#0099FF" Font-Bold="true" Style="text-align: right; padding-right: 30px" />
                            <asp:TableCell>
                                <asp:CheckBox ID="cbxResources" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Text="Print RMA Instructions: " ForeColor="#0099FF" Font-Bold="true" Style="text-align: right; padding-right: 30px" />
                            <asp:TableCell>
                                <asp:CheckBox ID="cbxPrintRMA" runat="server" Checked="true" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ID="lblCreateLabel" Text="Create Return Label: " ForeColor="#0099FF" Font-Bold="true" Style="text-align: right; padding-right: 30px" />
                            <asp:TableCell>
                                <asp:CheckBox ID="cbxCreateLabel" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ID="lblInsertTrackingNo" Text="Return Tracking No: " ForeColor="#0099FF" Font-Bold="true" Style="text-align: right; padding-right: 30px" />
                            <asp:TableCell ID="tcInsertTrackingNo">
                                <asp:TextBox ID="txtInsertTrackingNo" runat="server" Width="50%" CssClass="inputBox" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableFooterRow HorizontalAlign="Right">
                <asp:TableHeaderCell />
                <asp:TableHeaderCell />
                <asp:TableHeaderCell />
                <asp:TableHeaderCell HorizontalAlign="Right">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="CloseWindow();" />
                    <asp:Button ID="btnCancelRMA" runat="server" Text="Cancel RMA" OnClick="BtnCancelRMA_Click" />
                    <asp:Button ID="btnCreateRMA" runat="server" Text="Create RMA" OnClick="BtnCreateRMA_Click" />
                </asp:TableHeaderCell>
            </asp:TableFooterRow>
        </asp:Table>
    </form>
</body>
</html>
