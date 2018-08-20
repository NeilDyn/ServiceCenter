<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreatePartRequest.aspx.cs" Inherits="ExcelDesign.Forms.PDAForms.CreatePartRequest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PDA - Create Part Request</title>
    <link href="../../css/mainpage.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmCreatePDAPartRequest" runat="server">
        <asp:Table ID="tblPartRequest" runat="server" Height="100%" Width="100%">
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
                <asp:TableCell ColumnSpan="5">
                    <asp:Table runat="server" ID="tblCreateReturnOrderTableDetails" Height="100%" Width="100%">
                        <asp:TableHeaderRow ForeColor="White" BackColor="#507CD1">
                            <asp:TableHeaderCell Text="Item No." HorizontalAlign="Left" ID="HeaderItem" />
                            <asp:TableHeaderCell Text="Description" HorizontalAlign="Left" Width="30%" ID="HeaderDesc" />
                            <asp:TableHeaderCell Text="Qty" ID="HeaderQty" />
                            <asp:TableHeaderCell Text="Part Request" ID="HeaderPartRequest" Width="8%" />
                            <asp:TableHeaderCell Text="Reason" HorizontalAlign="Left" ID="HeaderReturnReasonCode" />
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
                            <asp:TableCell Width=80%>
                                <asp:TextBox ID="txtNotes" Width="100%" runat="server" TextMode="MultiLine" MaxLength="160" CssClass="inputBox" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Text="Customer Email Address: " ForeColor="#0099FF" Font-Bold="true" Style="text-align: right; padding-right: 30px" />
                            <asp:TableCell Width="80%">
                                <asp:TextBox ID="txtCustEmail" Width="50%" runat="server" CssClass="inputBox" />
                            </asp:TableCell>
                        </asp:TableRow>
                        
                        <asp:TableRow>
                            <asp:TableCell ID="lblZendeskTicketNo" Text="Return Tracking No: " ForeColor="#0099FF" Font-Bold="true" Style="text-align: right; padding-right: 30px" />
                            <asp:TableCell ID="tcZendeskTicketNo">
                                <asp:TextBox ID="txtZendeskTicketNo" runat="server" Width="50%" CssClass="inputBox" />
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
                    <asp:Button ID="btnCreatePartRequest" runat="server" Text="Create Part Request" OnClick="btnCreatePartRequest_Click" />
                </asp:TableHeaderCell>
            </asp:TableFooterRow>
        </asp:Table>
    </form>
</body>
</html>
