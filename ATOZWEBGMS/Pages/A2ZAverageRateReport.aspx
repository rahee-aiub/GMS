<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="A2ZAverageRateReport.aspx.cs" Inherits="ATOZWEBGMS.Pages.A2ZAverageRateReport" Title="Average Currency Exchange Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../Styles/structure.css" rel="stylesheet" />--%>
    <style type="text/css">
        body {
            background: url(../Images/PageBackGround.jpg)no-repeat;
            background-size: cover;
        }
    </style>

    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtFromDate.ClientID %>").datepicker();
            $("#<%= txtToDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtFromDate.ClientID %>").datepicker();
                $("#<%= txtToDate.ClientID %>").datepicker();

            });

        });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <br />

    <div align="center">
        <br />

        <table class="style1" width="800px">
            <thead>
                <tr>
                    <th colspan="6">Average Rate Report
                    </th>
                </tr>

            </thead>

            <tr>
                <td colspan="8">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtAvgCurrencyRate" runat="server" GroupName="RptAvgRate" Text="Average Currency Exchange Rate" Style="font-weight: 700" Font-Italic="True" Checked="True" AutoPostBack="true" OnCheckedChanged="rbtAvgCurrencyRate_CheckedChanged" />

                </td>
            </tr>

            <tr>
                <td colspan="8">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtAvgMetalRate" runat="server" GroupName="RptAvgRate" Text="Average Metal Rate" Style="font-weight: 700" Font-Italic="True" AutoPostBack="true" OnCheckedChanged="rbtAvgMetalRate_CheckedChanged" />

                </td>
            </tr>

            <tr>
                <td colspan="8">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtAvgMakingRate" runat="server" GroupName="RptAvgRate" Text="Average Making Rate" Style="font-weight: 700" Font-Italic="True" AutoPostBack="true" OnCheckedChanged="rbtAvgMakingRate_CheckedChanged" />

                </td>
            </tr>

            <tr>
                <td colspan="8">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtAvgStoneMakingRate" runat="server" GroupName="RptAvgRate" Text="Average Stone Making Rate" Style="font-weight: 700" Font-Italic="True" AutoPostBack="true" OnCheckedChanged="rbtAvgStoneMakingRate_CheckedChanged" />

                </td>
            </tr>

            <tr>
                <td colspan="8">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtAvgCarryingRate" runat="server" GroupName="RptAvgRate" Text="Average Carrying Rate" Style="font-weight: 700" Font-Italic="True" AutoPostBack="true" OnCheckedChanged="rbtAvgCarryingRate_CheckedChanged" />

                </td>
            </tr>

            <tr>
                <td></td>

                <td>
                    <asp:CheckBox ID="ChkAllLocation" runat="server" Text="All Location " Height="30" Font-Size="Large" ForeColor="Red" Checked="true" TabIndex="5" AutoPostBack="true" OnCheckedChanged="ChkAllLocation_CheckedChanged"></asp:CheckBox>
                </td>
                <td>
                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="cls text" Width="150px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large">
                        <asp:ListItem Value="1">Dhaka (DHK)</asp:ListItem>
                        <asp:ListItem Value="2">Dubai (DXB)</asp:ListItem>

                    </asp:DropDownList>
                </td>
                <td></td>
                <td></td>
                <td></td>

            </tr>

            <tr>
                <td></td>

                <td>
                    <asp:CheckBox ID="ChkAllKarat" runat="server" Text="All Karat " Height="30" Font-Size="Large" ForeColor="Red" Checked="true" TabIndex="5" AutoPostBack="true" OnCheckedChanged="ChkAllKarat_CheckedChanged"></asp:CheckBox>
                </td>
                <td>
                    <asp:DropDownList ID="ddlKarat" runat="server" CssClass="cls text" Width="150px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Sel-</asp:ListItem>
                        <asp:ListItem Value="22">22</asp:ListItem>
                        <asp:ListItem Value="21">21</asp:ListItem>
                        <asp:ListItem Value="18">18</asp:ListItem>

                    </asp:DropDownList>
                </td>
                <td></td>
                <td></td>
                <td></td>

            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="" Width="150px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Currency :" Width="170px" Font-Size="Large" ForeColor="Red"></asp:Label>

                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="cls text" Width="150px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large">
                    </asp:DropDownList>

                </td>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="" Width="150px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="" Width="150px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="From Date :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="cls text" Width="110px" Height="27px" Style="text-align: Right" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="6"></asp:TextBox>

                </td>

                <td>&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label1" runat="server" Text="To Date :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="cls text" Width="110px" Height="27px" Style="text-align: Right" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="6"></asp:TextBox>

                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="" Width="150px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>

            </tr>


            <tr>
                <td></td>
                <td colspan="5">

                    <asp:Button ID="btnView" runat="server" Text="Preview/Print"
                        Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button blue"
                        OnClientClick="return ValidationBeforeSave()" Height="27px" OnClick="btnView_Click" />
                    &nbsp;
                    &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Font-Bold="True" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" Height="27px" />
                    <br />

                </td>
            </tr>
        </table>
    </div>






    <div align="center">
        <table>
            <thead>
                <tr>
                    <th>
                        <h2 style="position: absolute; bottom: 30px; left: 35%;">Developed By AtoZ Computer Services - Version 2.0<br />
                            Last Update: February, 2019.</h2>
                    </th>
                </tr>
            </thead>
        </table>
    </div>

    <asp:Label ID="lblLastLPartyNo" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblProcessDate" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblNewLPartyNo" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="hdnNewAccNo" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="ctrlNewAccNo" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="CtrlVoucherNo" runat="server" Visible="False"></asp:Label>

    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="CtrlProcDate" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblCurrencyCode" runat="server" Visible="False"></asp:Label>

    <asp:Label ID="CtrlProgFlag" runat="server" Visible="False"></asp:Label>
    <asp:HiddenField ID="hPartCode" runat="server" />

</asp:Content>
