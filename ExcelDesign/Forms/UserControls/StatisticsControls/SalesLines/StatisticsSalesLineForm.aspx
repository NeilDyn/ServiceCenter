﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatisticsSalesLineForm.aspx.cs" Inherits="ExcelDesign.Forms.UserControls.StatisticsControls.SalesLines.StatisticsSalesLineForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../../css/mainpage.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>
    <script>
        $(document).ready(function () {
            $("[id$=cbxSelectAll]").click(function () {
                if ($(this).is(':checked')) {
                    $("[id*=cbxProcess]").prop('checked', true);
                }
                else {
                    $("[id*=cbxProcess]").prop('checked', false);
                };
            });
        });

        function UpdateREQReturnActions() {
            var rmaList = "";
            var singleLine = "";

            $("[id*=ddlREQReturnSelect_]").each(function () {
                if ($(this).val() != 'Unknown') {
                    var intIndex = 0;

                    if ($(this).val() == "Refund") {
                        intIndex = 2;
                    }

                    if ($(this).val() == "Exchange") {
                        intIndex = 1;
                    }

                    if (rmaList == "") {
                        singleLine = $(this).attr('id').substr(19, $(this).attr('id').length);
                        rmaList = $("[id$=docNo_" + singleLine + "]").text().trim();
                        rmaList += ":" + $("[id$=itemNo_" + singleLine + "]").text().trim();
                        rmaList += ":" + intIndex;
                    }
                    else {
                        singleLine = $(this).attr('id').substr(19, $(this).attr('id').length);
                        rmaList += "," + $("[id$=docNo_" + singleLine + "]").text().trim();
                        rmaList += ":" + $("[id$=itemNo_" + singleLine + "]").text().trim();
                        rmaList += ":" + intIndex;
                    }
                }
            });

            if (rmaList != "") {              
                rmaList += ",";
                $.ajax({
                    type: "POST",
                    url: "StatisticsSalesLineForm.aspx/UpdateREQReturnActions",
                    data: JSON.stringify({ rmaList: rmaList}),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (error) {
                        if (error.d.indexOf("Error") == -1) {
                            alert("Selected item(s) have been updated successfully.");
                            parent.window.close();
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
            }
            return false;
        };

        function ProcessItems() {
            var rmaList = "";
            var singleLine = "";
            var type = "<%= this.pendingList%>";

            $("[id*=cbxProcess]").each(function () {
                if ($(this).is(':checked')) {
                    if (rmaList == "") {
                        singleLine = $(this).attr('id').substr(10, $(this).attr('id').length);
                        rmaList = $("[id$=docNoInv_" + singleLine + "]").text().trim();
                    }
                    else {
                        singleLine = $(this).attr('id').substr(10, $(this).attr('id').length);
                        rmaList += "," + $("[id$=docNoInv_" + singleLine + "]").text().trim();
                    }
                }
            });

            if (rmaList != "") {              
                rmaList += ",";
                $.ajax({
                    type: "POST",
                    url: "StatisticsSalesLineForm.aspx/ProcessItems",
                    data: JSON.stringify({ rmaList: rmaList, type: type }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (error) {
                        if (error.d.indexOf("Error") == -1) {
                            alert("Selected item(s) have been processed successfully.");
                            parent.window.close();
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
            }
            return false;
        };
    </script>
</head>
<body>
    <form id="frmStatisticsSalesLineDetail" runat="server">
        <asp:Table ID="tblStatisticsSalesLines" runat="server" Height="100%" Width="100%" ScrollBars="Auto">
            <asp:TableHeaderRow HorizontalAlign="Justify" ForeColor="White" BackColor="#507CD1">
                <asp:TableHeaderCell Text="Customer No" />
                <asp:TableHeaderCell Text="Document No" />
                <asp:TableHeaderCell Text="External Document No" />
                <asp:TableHeaderCell Text="Created Date" />
                <asp:TableHeaderCell Text="Item No" />
                <asp:TableHeaderCell Text="Description" />
                <asp:TableHeaderCell Text="Qty" />
                <asp:TableHeaderCell Text="REQ Return Action" runat="server" ID="reqReturnAction"/>
                <asp:TableHeaderCell Text="Status" />
                <asp:TableHeaderCell Text="Refund Processing" ID="RefundProcessing" Visible="false" />
                <asp:TableHeaderCell Text="Process" ID="ProcessColumn" Visible="false" />
                <asp:TableHeaderCell Text="Exchange Order No" ID="ExchangeOrderNo" Visible="false" />
            </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                <asp:TableHeaderCell ColumnSpan="10">
                <hr class="HeaderLine" />
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </form>
</body>
</html>
