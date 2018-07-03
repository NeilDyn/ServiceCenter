<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateReturn.aspx.cs" Inherits="ExcelDesign.Forms.FunctionForms.CreateReturn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Return Order</title>
    <link href="../css/mainpage.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

        });

        function CloseWindow() {
            var c = window.confirm("Are you sure you wish to cancel creating this return?");
            if (c == true) {
                parent.window.close();
            };
        };

        function CloseAfterCreate() {
            parent.window.close();
        };

        function OpenCreatedRMA() {
            var width = 1000;
            var height = 400;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            window.open("CreatedRMA.aspx?PrintRMAInstructions=<%= this.printRMA %>",
                null,
                "left=" + left + ",width=" + width + ",height=" + height + ",top=" + top + ",status=no,resizable=no,toolbar=no,location=no,menubar=no,directories=no");
        }
    </script>
</head>
<body>
    <form id="frmCreateReturnOrder" runat="server">
        <asp:Table ID="tblRMAInfo" runat="server" Height="100%" Width="100%">
            <asp:TableRow>
                <asp:TableHeaderCell Text="Order No:" />
                <asp:TableHeaderCell ID="tcOrderNo" runat="server" />
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableHeaderCell Text="External Document No:" />
                <asp:TableHeaderCell ID="tcDocNo" runat="server" />
            </asp:TableRow>
            <%-- <asp:TableRow>
                <asp:TableCell Text="Return Reason: " />
                <asp:TableCell>
                    <asp:DropDownList ID="ddlReturnReason" runat="server" Width="50%"/>
                </asp:TableCell>
            </asp:TableRow>--%>
            <asp:TableRow>
                <asp:TableCell Text="Defect Options: " />
                <asp:TableCell>
                    <asp:DropDownList ID="ddlDefectOptions" runat="server" Width="50%" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="5">
                    <asp:Table runat="server" ID="tblCreateReturnOrderTableDetails" Height="100%" Width="100%">
                        <asp:TableHeaderRow ForeColor="White" BackColor="#507CD1">
                            <asp:TableHeaderCell Text="Item No." HorizontalAlign="Left" ID="HeaderItem" />
                            <asp:TableHeaderCell Text="Description" HorizontalAlign="Left" Width="30%" ID="HeaderDesc" />
                            <asp:TableHeaderCell Text="Qty" ID="HeaderQty" />
                            <asp:TableHeaderCell Text="Action Qty." ID="HeaderActionQty" />
                            <asp:TableHeaderCell Text="Return Reason Code" HorizontalAlign="Left" ID="HeaderReturnReasonCode" />
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
                <asp:TableCell Text="Notes: " />
                <asp:TableCell Width="100%">
                    <asp:TextBox ID="txtNotes" Width="100%" runat="server" TextMode="MultiLine" MaxLength="160"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Customer Email Address: " />
                <asp:TableCell Width="100%">
                    <asp:TextBox ID="txtCustEmail" Width="50%" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Include Resource Lines: " Width="20%" />
                <asp:TableCell>
                    <asp:CheckBox ID="cbxResources" runat="server" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Print RMA Instructions: " />
                <asp:TableCell>
                    <asp:CheckBox ID="cbxPrintRMA" runat="server" Checked="true" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Create Return Label: " />
                <asp:TableCell>
                    <asp:CheckBox ID="cbxCreateLable" runat="server" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableFooterRow HorizontalAlign="Right">
                <asp:TableHeaderCell />
                <asp:TableHeaderCell HorizontalAlign="Right">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="CloseWindow();" />
                </asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right">
                    <asp:Button ID="btnCreateRMA" runat="server" Text="Create RMA" OnClick="BtnCreateRMA_Click" />
                </asp:TableHeaderCell>
            </asp:TableFooterRow>
        </asp:Table>
    </form>
</body>
</html>
