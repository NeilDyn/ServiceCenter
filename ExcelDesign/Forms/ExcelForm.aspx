<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExcelForm.aspx.cs" Inherits="ExcelDesign.Forms.ExcelForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Design From Excel Format</title>
    <link href="../css/mainpage.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="ExcelForm" runat="server">
        <div id="ServiceCenterHeader">
            <asp:Image ID="imgLogo" runat="server" ImageUrl="~/images/Logo.png" />
            <asp:Label ID="lblCustomerServicePortal" runat="server" style="margin-left: 200px;" Text="Customer Service Portal" ForeColor="#0099FF" Font-Bold="True" Font-Size="XX-Large" CssClass="HeaderLabel"/>        
        </div>
        <hr class="HeaderLine"/>
        
        <div class="SearchArea" id="SearchArea" style="margin-top: 20px;">        
            <asp:Label ID="Label1" runat="server" Text="Search" ForeColor="#0099FF" Font-Bold="True"/>
            <asp:TextBox ID="txtSearchBox" runat="server" Width="700px" BorderColor="Black" BorderWidth="2px" style="margin-left: 35px" />
            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" style="margin-left: 30px" Text="Search" />
        </div>
     
        <div id="Details" runat="server">

            <div id="CustomerInfoArea" class="CustomerInfo" runat="server">
                <asp:Label ID="lblCustomerInfo" runat="server" Text="Customer Info:" ForeColor="#0099FF" Font-Bold="True" />
                <hr class="SectionBreak"/>

                <div id="DetailedCustomerCaptions" class="detailedCustomerCaption">
                    <asp:Label ID="lblNameCaption" runat="server" Text="Name:" />
                    <br />
                    <asp:Label ID="lblAddress1Caption" runat="server" Text="Address 1:" />
                    <br />
                    <asp:Label ID="lblAddress2Caption" runat="server" Text="Address 2:" />
                    <br />
                    <asp:Label ID="lblShipToContactCaption" runat="server" Text="Ship to Contact:" />
                    <br />
                    <asp:Label ID="lblCityCaption" runat="server" Text="City:" />
                    <br />
                    <asp:Label ID="lblZipCaption" runat="server" Text="Zip:" />
                </div>
                <div id="DetailedCustomerInfo" class="detailedCustomerInfo">
                    <asp:Label ID="lblName" runat="server" />
                    <br />
                    <asp:Label ID="lblAddress1" runat="server" />
                    <br />
                    <asp:Label ID="lblAddress2" runat="server" />
                    <br />
                    <asp:Label ID="lblShipToContact" runat="server"/>
                    <br />
                    <asp:Label ID="lblCity" runat="server" />
                    <br />
                    <asp:Label ID="lblZip" runat="server" />
                </div>
                <div id="DetailedCustomerCaptionsCon" class="detailedCustomerCaptionCon">
                    <asp:Label ID="lblStateCaption" runat="server" Text="State:" />
                    <br />
                    <asp:Label ID="lblCountryCaption" runat="server" Text="Country:" />
                </div>
                <div id="DetailedCustomerInfoCon" class="detailedCustomerInfoCon">
                    <asp:Label ID="lblState" runat="server" />
                    <br />
                    <asp:Label ID="lblCountry" runat="server" />
                </div>
            </div>

            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />

            <div id="OrderInfoHeader" class="OrderInfoHeader" runat="server" >
                <div style="float:left"><asp:Label ID="Label12" runat="server" Text="Order Info:" ForeColor="#0099FF" Font-Bold="True" /></div>
                <div style="float:right; margin-right:100px"><asp:Label ID="Label14" Text="" runat="server" ForeColor="#0099FF" Font-Bold="True" /> </div>
                <div style="float:right; margin-right:100px"><asp:Label ID="Label15" runat="server" Text="Totals Orders:" ForeColor="#0099FF" Font-Bold="True" /></div>
                <br />
                <div style="float:none"><hr class="SectionBreak"/></div>
                <div id="OrderDetail" runat="server">
                    <div style="float:left"><asp:Label ID="lblOrderSequence" Font-Bold="True" runat="server" style="text-decoration:underline" Text="Order 1" /></div>
                    <div style="float:right; margin-right:100px"><asp:Label ID="lblExternalDocumentNo" runat="server" /></div>
                    <div style="float:right; margin-right:100px"><asp:Label ID="Label13" runat="server" Text="External Document No:"/></div>  
                    <br />                

                    <div id="OrderDetailHeaderCaptions" runat="server" class="detailedOrderCaption">
                        <asp:Label ID="lblOrderStatusCaption" runat="server" Text="Order Status:" />
                        <br />
                        <br />
                        <asp:Label ID="Label3" runat="server" Text="Order Date:" />
                        <br />
                        <asp:Label ID="Label4" runat="server" Text="Sales Order Number:" />
                        <br />
                        <asp:Label ID="Label5" runat="server" Text="Channel Name:" />
                        <br />
                        <asp:Label ID="Label6" runat="server" Text="Zendesk Ticket #:" />
                        <br />
                        <asp:Label ID="Label7" runat="server" Text="Zendesk Ticket(s):" />
                    </div>
                    <div id="OrderDetailHeaderInfo" runat="server" class="detailedOrderInfo">
                        <asp:Label ID="lblOrderStatus" runat="server" />
                        <br />
                        <br />
                        <asp:Label ID="lblOrderDate" runat="server" />
                        <br />
                        <asp:Label ID="lblSalesOrderNumber" runat="server" />
                        <br />
                        <asp:Label ID="lblChannelName" runat="server"/>
                        <br />
                        <asp:Label ID="lblZendeskTicket" runat="server" />
                        <br />
                        <asp:Label ID="lblZendeskTicketNo" runat="server" />
                    </div>
                    <div id="OrderDetailHeaderCaptionsCon" class="detailedOrderCaptionCon">
                        <asp:Label ID="Label2" runat="server" Text="Shipment Date:" />
                        <br />
                        <asp:Label ID="Label8" runat="server" Text="Shipments:" />
                        <br />
                        <asp:Label ID="Label9" runat="server" Text="Package(s):" />
                        <br />
                        <asp:Label ID="Label10" runat="server" Text="Ship Method:" />
                        <br />
                        <asp:Label ID="Label11" runat="server" Text="Tracking #:" />
                    </div>
                    <div id="OrderDetailHeaderInfoCon" class ="detailedOrderInfoCon">
                        <asp:Label ID="lblShipmentDate" runat="server" />
                        <br />
                        <asp:Label ID="lblShipments" runat="server" />
                        <br />
                        <asp:Label ID="lblPackages" runat="server" />
                        <br />
                        <asp:Label ID="lblShipMethod" runat="server" />
                        <br />
                        <asp:Label ID="lblTrackingNo" runat="server" />
                    </div>

                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />

                    <div>
                        <asp:GridView ID="gdvOrderView" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Height="16px" Width="1080px">
                            <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </div>

                    <div class="Options">
                        <ul>
                            <li>                          
                                <asp:Button ID="Button4" runat="server" Text="Issue Refund" />
                            </li>
                            <li>                 
                                <asp:Button ID="Button3" runat="server" Text="Create Return" />
                            </li>
                            <li>
                                <asp:Button ID="Button2" runat="server" Text="Part Request" />
                            </li>
                            <li>
                                <asp:Button ID="Button1" runat="server" Text="Cancel Order" />
                            </li>
                        </ul>
                    </div>
                </div>  
            </div>  
             
        </div>          
    </form>
</body>
</html>
