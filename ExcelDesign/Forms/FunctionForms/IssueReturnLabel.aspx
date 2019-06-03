<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IssueReturnLabel.aspx.cs" Inherits="ExcelDesign.Forms.FunctionForms.IssueReturnLabel" %>

<%@ Register Src="~/Forms/UserControls/IssueReturnLabel/ZendeskIssueReturnLabel.ascx" TagName="ZendeskIssueReturnLabel" TagPrefix="zirl" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Return Order</title>
    <link href="../../css/mainpage.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            window["PostClipboard"] = null;
        });
    </script>
</head>
<body>
    <form id="IssueReturnLabelForm" runat="server">
        <asp:Table ID="tblIssueReturnLabel" runat="server" Height="100%" Width="100%" Style="padding: 0">
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
            <asp:TableHeaderRow>
                <asp:TableCell ColumnSpan="4">
                    <zirl:ZendeskIssueReturnLabel ID="ZendeskIssueReturnLabelControl" runat="server" />                   
                </asp:TableCell>
            </asp:TableHeaderRow>
            <asp:TableFooterRow HorizontalAlign="Right">
                <asp:TableHeaderCell />
                <asp:TableHeaderCell HorizontalAlign="Right" ColumnSpan="4">
                    <asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClientClick="CloseWindow();" />
                    <asp:Button ID="BtnIssueReturnLabel" runat="server" Text="Issue Return Label" OnClick="BtnIssueReturnLabel_Click" />
                </asp:TableHeaderCell>
            </asp:TableFooterRow>
        </asp:Table>
    </form>
</body>
</html>
