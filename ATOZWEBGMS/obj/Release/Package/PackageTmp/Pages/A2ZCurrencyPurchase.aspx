<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="A2ZCurrencyPurchase.aspx.cs" Inherits="ATOZWEBGMS.Pages.A2ZCurrencyPurchase" Title="Currency Purchase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../Styles/structure.css" rel="stylesheet" />--%>
    <style type="text/css">
        body {
            background: url(../Images/PageBackGround.jpg)no-repeat;
            background-size: cover;
        }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {
            debugger;
            $("#<%=txtPartyName.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("GMSWebService.asmx/GetCarrPartyName") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("#<%=hPartCode.ClientID %>").val(i.item.val);
                    $("#<%=txtPartyCode.ClientID %>").val(i.item.val);
                },
                minLength: 1,
            });
        });


    </script>

    <%--  <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            var textBox1 = $('input:text[id$=txtPurchaseAmt]').keyup(foo);
            var textBox2 = $('input:text[id$=txtRate]').keyup(foo);

            function foo() {
                var value1 = textBox1.val();
                var value2 = textBox2.val();
                var sum = (value1 * value2);
                $('input:text[id$=txtTotalAmt]').val(sum);
            }

        });
    </script>--%>

    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtDate.ClientID %>").datepicker();

            });

        });

    </script>


    <script language="javascript" type="text/javascript">
        function Comma(Num) { //function to add commas to textboxes
            Num += '';
            Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
            Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
            x = Num.split('.');
            x1 = x[0];
            x2 = x.length > 1 ? '.' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1))
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
            return x1 + x2;


        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <br />

    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="4">Purchase Currency
                    </th>
                </tr>

            </thead>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Date " Width="120px" Font-Size="Large" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="cls text" Width="154px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Cash " Width="120px" Font-Size="Large" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:DropDownList ID="ddlCash" runat="server" CssClass="cls text" Width="150px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large">
                    </asp:DropDownList>

                     <asp:DropDownList ID="ddlAccountName" Visible="false" runat="server" CssClass="cls text" Width="150px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large">
                    </asp:DropDownList>
                    
                </td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Party Name" Width="120px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPartyCode" runat="server" CssClass="cls text" Width="78px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="True" MaxLength="6" OnTextChanged="txtPartyCode_TextChanged" TabIndex="1"></asp:TextBox>
                    &nbsp;<asp:TextBox ID="txtPartyName" runat="server" CssClass="cls text" Width="304px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="1"></asp:TextBox>
                    &nbsp; &nbsp; &nbsp;
                </td>

            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Currency " Width="120px" Font-Size="Large" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="cls text" Width="150px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large">
                    </asp:DropDownList>

                </td>

            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Exchange " Width="120px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <table class="style1">
                        <tr align="center">

                            <td align="center" valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label4" runat="server" Text="Amount" Font-Size="Large" ForeColor="Red"></asp:Label>

                            </td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label6" runat="server" Text="Rate" Font-Size="Large" ForeColor="Red"></asp:Label>

                            </td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label9" runat="server" Text="Total Amt. (BDT)" Font-Size="Large" ForeColor="Red"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtPurchaseAmt" runat="server" CssClass="cls text" Width="108px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge" onkeyup="javascript:this.value=Comma(this.value);"
                                    onkeypress="return IsDecimalKey(event)" Style="text-align: Right" Font-Size="Large" AutoPostBack="True" OnTextChanged="txtPurchaseAmt_TextChanged"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRate" runat="server" CssClass="cls text" Width="88px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    onkeypress="return IsDecimalKey(event)" Style="text-align: Right" Font-Size="Large" AutoPostBack="True" OnTextChanged="txtRate_TextChanged"></asp:TextBox>

                            </td>
                            <td>
                                <asp:TextBox ID="txtTotalAmt" runat="server" CssClass="cls text" Width="175px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge" onkeyup="javascript:this.value=Comma(this.value);"
                                    Style="text-align: Right" Font-Size="Large" Enabled="False"></asp:TextBox>

                            </td>
                        </tr>
                    </table>


                </td>


            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Narration " Font-Size="Large" ForeColor="Red" Width="120px"></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txtNarration" runat="server" CssClass="cls text" Width="400px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" Enabled="False"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td></td>
                <td colspan="5">

                    <asp:Button ID="btnUpdate" runat="server" Text="Update"
                        Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green"
                        OnClientClick="return ValidationBeforeSave()" Height="27px" OnClick="btnUpdate_Click" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                        Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button blue"
                        OnClick="btnCancel_Click" Height="27px" />
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
                        <h2 style="position: absolute; bottom: 30px; left: 35%;">Developed By AtoZ Computer Services - Version 1.0<br />
                            Last Update: June, 2018.</h2>
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

    <asp:HiddenField ID="hPartCode" runat="server" />
</asp:Content>
