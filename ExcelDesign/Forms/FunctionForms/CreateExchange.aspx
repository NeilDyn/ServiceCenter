<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateExchange.aspx.cs" Inherits="ExcelDesign.Forms.FunctionForms.CreateExchange" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Exchange Order</title>
    <link href="../../css/mainpage.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function CloseWindow() {
            var c;

            c = window.confirm("Are you sure you wish to cancel creating this exchange?");

            if (c == true) {
                parent.window.close();
            };
        };

        function OpenCreateExchange() {
            var width = 1500;
            var height = 500;
            var left = (screen.width - width) + 500;
            var top = (screen.height - height) * 0.5;
            window.open("CreatedExchange.aspx",
                null,
                "left=" + left + ",width=" + width + ",height=" + height + ",top=" + top + ",status=no,resizable=no,toolbar=no,location=no,menubar=no,directories=no");
        };
    </script>
</head>
<body>
    <form id="frmCreateExchange" runat="server">
        <asp:Table ID="tblExhangeInfo" runat="server" Width="100%" Height="100%">
            <asp:TableHeaderRow HorizontalAlign="Left">
                <asp:TableHeaderCell Text="RMA No:" ID="rmaTitle" ForeColor="#0099FF" Font-Bold="true" Font-Size="Large" />
                <asp:TableHeaderCell ID="tcRMANo" runat="server" />
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
                <asp:TableHeaderCell Text="Shipping Information:" HorizontalAlign="Left" Font-Underline="true" Font-Bold="true"/>
            </asp:TableHeaderRow>
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell Text="Ship-to Name:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF"/>
                    <asp:TableHeaderCell runat="server" ID="tcShipToName" HorizontalAlign="Left" Style="text-align: left" />
                    <asp:TableHeaderCell Text="Ship-to Address 1:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF"/>
                    <asp:TableHeaderCell runat="server" ID="tcShipToAddress1" HorizontalAlign="Left" Style="text-align: left" />
                </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                    <asp:TableHeaderCell Text="Ship-to Address 2:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF"/>
                    <asp:TableHeaderCell runat="server" ID="tcShipToAddress2" HorizontalAlign="Left" Style="text-align: left" />
                    <asp:TableHeaderCell Text="Ship-to Contact:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF"/>
                    <asp:TableHeaderCell runat="server" ID="tcShipToContact" HorizontalAlign="Left" Style="text-align: left" />
                </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                    <asp:TableHeaderCell Text="Ship-to City:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF"/>
                    <asp:TableHeaderCell runat="server" ID="tcShipToCity" HorizontalAlign="Left" Style="text-align: left" />
                    <asp:TableHeaderCell Text="Ship-to State:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF"/>
                    <asp:TableHeaderCell runat="server" ID="tcShipToState" HorizontalAlign="Left" Style="text-align: left" />
                </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                    <asp:TableHeaderCell Text="Ship-to Zip:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF"/>
                    <asp:TableHeaderCell runat="server" ID="tcShipToZip" HorizontalAlign="Left" Style="text-align: left" />
                    <asp:TableHeaderCell Text="Ship-to Country:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF"/>
                    <asp:TableHeaderCell runat="server" ID="tcShipToCountry" HorizontalAlign="Left" Style="text-align: left" />
                </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="4">
                    <asp:Table runat="server" ID="tblCreateReturnOrderTableDetails" Height="100%" Width="100%">
                        <asp:TableHeaderRow ForeColor="White" BackColor="#507CD1">
                            <asp:TableHeaderCell Text="Item No." HorizontalAlign="Left" ID="HeaderItem" />
                            <asp:TableHeaderCell Text="Description" HorizontalAlign="Left" Width="60%" ID="HeaderDesc" />
                            <asp:TableHeaderCell Text="Qty Received" ID="HeaderQty" Width="10%" />
                            <asp:TableHeaderCell Text="Action Qty." ID="HeaderActionQty" Width="8%" />
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
                <asp:TableHeaderCell />
                <asp:TableHeaderCell />
                <asp:TableHeaderCell HorizontalAlign="Right">
                    <asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClientClick="CloseWindow();" />
                    <asp:Button ID="BtnCreateExchange" runat="server" Text="Create Exchange" OnClick="BtnCreateExchange_Click" />
                </asp:TableHeaderCell>
            </asp:TableFooterRow>
        </asp:Table>     
    </form>
</body>
</html>
