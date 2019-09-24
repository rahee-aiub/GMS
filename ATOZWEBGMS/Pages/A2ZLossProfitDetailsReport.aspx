<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="A2ZLossProfitDetailsReport.aspx.cs" Inherits="ATOZWEBGMS.Pages.A2ZLossProfitDetailsReport" Title="Loss Profit Details" %>

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
                    <th colspan="6">Loss/Profit Details Report
                    </th>
                </tr>

            </thead>

            <tr>
                <td colspan="8">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtFixingProfit" runat="server" GroupName="RptOptPrm" Text="Fixing Loss/Profit Detail Report" Style="font-weight: 700" Font-Italic="True" Checked="True" AutoPostBack="true" OnCheckedChanged="rbtFixingProfit_CheckedChanged" />

                </td>
            </tr>

            <tr>
                <td colspan="8">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtPMCProfit" runat="server" GroupName="RptOptPrm" Text="Purity,Making,Carring Loss/Profit Report" Style="font-weight: 700" Font-Italic="True" AutoPostBack="true" OnCheckedChanged="rbtPMCProfit_CheckedChanged" />

                </td>
            </tr>

            <tr>
                <td colspan="8">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtGoldProfit" runat="server" GroupName="RptOptPrm" Text="Gold Profit Analysis Report" Style="font-weight: 700" Font-Italic="True" AutoPostBack="true" OnCheckedChanged="rbtGoldProfit_CheckedChanged" />

                </td>
            </tr>

            <tr>
                <td colspan="8">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtCurrencyProfitAnalysis" runat="server" GroupName="RptOptPrm" Text="Currency Profit Analysis Report" Style="font-weight: 700" Font-Italic="True" AutoPostBack="true" OnCheckedChanged="rbtCurrencyProfitAnalysis_CheckedChanged" />

                </td>
            </tr>


            <tr>
                <td colspan="8">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtCurrencyProfitPurchaseSales" runat="server" GroupName="RptOptPrm" Text="Currency Profit by Purchase & Sales Report" Style="font-weight: 700" Font-Italic="True" AutoPostBack="true" OnCheckedChanged="rbtCurrencyProfitPurchaseSales_CheckedChanged" />

                </td>
            </tr>

             <tr>
                <td colspan="8">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtGoldCurrencyProfitPurchaseSales" runat="server" GroupName="RptOptPrm" Text="Gold Currency Profit by Purchase & Sales Report" Style="font-weight: 700" Font-Italic="True" AutoPostBack="true" OnCheckedChanged="rbtGoldCurrencyProfitPurchaseSales_CheckedChanged" />

                </td>
            </tr>
            
            <tr>
                <td colspan="8">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtOfficeExpense" runat="server" GroupName="RptOptPrm" Text="Office Expense Report Summary" Style="font-weight: 700" Font-Italic="True" AutoPostBack="true" OnCheckedChanged="rbtOfficeExpense_CheckedChanged" />

                </td>
            </tr>
              <tr>
                <td colspan="8">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtOfficeExpenseDetails" runat="server" GroupName="RptOptPrm" Text="Office Expense Report Details" Style="font-weight: 700" Font-Italic="True" AutoPostBack="true" OnCheckedChanged="rbtOfficeExpenseDetails_CheckedChanged" />

                </td>
            </tr>

               <tr>
                <td colspan="8">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtLoanSummary" runat="server" GroupName="RptOptPrm" Text="Loan Summary Report" Style="font-weight: 700" Font-Italic="True" AutoPostBack="true" OnCheckedChanged="rbtLoanSummary_CheckedChanged"/>

                </td>
            </tr>

               <tr>
                <td colspan="8">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtGmsAdjustmentAndDiscount" runat="server" GroupName="RptOptPrm" Text="Adjustment/Discount Report" Style="font-weight: 700" Font-Italic="True" AutoPostBack="true" OnCheckedChanged="rbtGmsAdjustmentAndDiscount_CheckedChanged"/>

                </td>
            </tr>

             
              <tr>
                <td colspan="8">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtLossProfit" runat="server" GroupName="RptOptPrm" Text="Loss/Profit Report" Style="font-weight: 700" Font-Italic="True" AutoPostBack="true" OnCheckedChanged="rbtLossProfit_CheckedChanged"/>

                </td>
            </tr>

            <tr>
                <td colspan="8">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtProfitDtl" runat="server" GroupName="RptOptPrm" Text="DM Report" Style="font-weight: 700" Font-Italic="True" AutoPostBack="true" OnCheckedChanged="rbtProfitDtl_CheckedChanged"/>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="" Width="150px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Date :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="cls text" Width="110px" Height="27px" Style="text-align: Right" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="6"></asp:TextBox>

                </td>

                <td>&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label1" runat="server" Visible="false" Text="To Date :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" Visible ="false" CssClass="cls text" Width="110px" Height="27px" Style="text-align: Right" BorderColor="#1293D1" BorderStyle="Ridge"
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
