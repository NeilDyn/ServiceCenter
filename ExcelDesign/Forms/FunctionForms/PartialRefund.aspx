<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PartialRefund.aspx.cs" Inherits="ExcelDesign.Forms.FunctionForms.PartialRefund" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Partial Refund</title>
    <link href="../../css/mainpage.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function CloseWindow() {
            var c;

            c = window.confirm("Are you sure you wish to cancel the Partial Refund this order");

            if (c == true) {
                parent.window.close();
            };
        };

        function CloseAfterCancel() {
            parent.window.close();
        };
    </script>
</head>
<body>
    <form id="frmPartialRefund" runat="server">
        <asp:Table ID="tblPartialRefundInfo" runat="server" Height="100%" Width="100%">
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
            <asp:TableRow>
                <asp:TableCell ColumnSpan="6">
                    <asp:Table runat="server" ID="tblPartialRefundDetails" Height="100%" Width="100%">
                        <asp:TableHeaderRow ForeColor="White" BackColor="#507CD1">
                            <asp:TableHeaderCell Text="Item No." HorizontalAlign="Left" ID="HeaderItem" />
                            <asp:TableHeaderCell Text="Description" HorizontalAlign="Left" Width="30%" ID="HeaderDesc" />
                            <asp:TableHeaderCell Text="Qty" ID="HeaderQty" />
                            <asp:TableHeaderCell Text="Action Qty." ID="HeaderActionQty" Width="8%" />
                            <asp:TableHeaderCell Text="Reason Code" HorizontalAlign="Left" ID="HeaderReasonCode" />
                            <asp:TableHeaderCell Text="Refund Option" HorizontalAlign="Left" ID="HeaderRefundOption" />
                        </asp:TableHeaderRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <br />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableFooterRow HorizontalAlign="Right">
                <asp:TableHeaderCell />
                <asp:TableHeaderCell HorizontalAlign="Right">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="CloseWindow();" />
                    <asp:Button ID="btnCreatePartialRefund" runat="server" Text="Create Partial Refund" OnClick="btnCreatePartialRefund_Click" />
                </asp:TableHeaderCell>
            </asp:TableFooterRow>
        </asp:Table>
    </form>
</body>
</html>
