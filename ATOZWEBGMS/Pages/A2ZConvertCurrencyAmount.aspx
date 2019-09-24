<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="A2ZConvertCurrencyAmount.aspx.cs" Inherits="ATOZWEBGMS.Pages.A2ZConvertCurrencyAmount" Title="Convert Currency Amount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../Styles/structure.css" rel="stylesheet" />--%>
    <style type="text/css">
        body {
            background: url(../Images/PageBackGround.jpg)no-repeat;
            background-size: cover;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are You Sure You Want to Update Information?');
        }


    </script>



     <link href="../Styles/cupertino/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />

    <link href="../Styles/chosen.css" rel="stylesheet" />
    <script src="../scripts/jquery-1.12.4.js" type="text/javascript"></script>
    <script src="../scripts/jquery-ui.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= ddlFromCash.ClientID %>").chosen();
            $("#<%= ddlToCash.ClientID %>").chosen();

            var prm = Sys.WebForms.PageRequestManager.getInstance()

            prm.add_endRequest(function () {
                $("#<%= ddlFromCash.ClientID %>").chosen();
                $("#<%= ddlToCash.ClientID %>").chosen();

              });


        });

    </script>

    <script language="javascript" type="text/javascript">
        $(function () {

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $(".youpii").chosen();
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

    <div id="DivMain" runat="server" align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="4">Convert Currency Amount
                    </th>
                </tr>

            </thead>



            <tr>
                <td>
                    <asp:Label ID="lblFromCurrency" runat="server" Text="Convert Currency :" Width="170px" Font-Size="Large" ForeColor="Red"></asp:Label>

                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlFromCurrency" runat="server" CssClass="cls text" Width="150px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="ddlFromCurrency_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>


            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblFromCash" runat="server" Text="From Cash :" Width="120px" Font-Size="Large" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:DropDownList ID="ddlFromCash" runat="server" CssClass="youpii" class="chzn-select" Width="250px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="ddlFromCash_SelectedIndexChanged">
                    </asp:DropDownList>
                    <script type="text/javascript" src="../Script/chosen.jquery.js"></script>

                </td>
                <td>&nbsp;&nbsp;
                    <asp:Label ID="lblFromBalance" runat="server" Text="Balance :" Width="82px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>

                    <asp:TextBox ID="txtFromBalance" runat="server" CssClass="cls text" Style="text-align: right" Width="180px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" Enabled="false"></asp:TextBox>

                </td>
            </tr>
        </table>
        <br />
        <table class="style1">


            <tr>
                <td>
                    <asp:Label ID="lblToCurrency" runat="server" Text="To Currency :" Width="170px" Font-Size="Large" ForeColor="Red"></asp:Label>

                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlToCurrency" runat="server" CssClass="cls text" Width="150px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="ddlToCurrency_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>


            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblToCash" runat="server" Text="To Cash :" Width="120px" Font-Size="Large" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:DropDownList ID="ddlToCash" runat="server" CssClass="youpii" class="chzn-select" Width="250px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="ddlToCash_SelectedIndexChanged">
                    </asp:DropDownList>

                    <script type="text/javascript" src="../Script/chosen.jquery.js"></script>

                </td>
                <td>&nbsp;&nbsp;
                    <asp:Label ID="lblToBalance" runat="server" Text="Balance :" Width="82px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>

                    <asp:TextBox ID="txtToBalance" runat="server" CssClass="cls text" Style="text-align: right" Width="180px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" Enabled="false"></asp:TextBox>

                </td>
            </tr>



        </table>



        <br />
        <table class="style1">

            <tr>
                <td></td>

                <td colspan="2">

                    <table class="style1">
                        <tr>
                            <td>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblAmount" runat="server" Text="Convert Amount" Font-Size="Large" ForeColor="Red"></asp:Label>
                            </td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="Rate" Font-Size="Large" ForeColor="Red"></asp:Label>
                            </td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblConvertAmt" runat="server" Text="To Amount" Font-Size="Large" ForeColor="Red"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtAmt" runat="server" CssClass="cls text" Width="160px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge" onkeyup="javascript:this.value=Comma(this.value);"
                                    onkeypress="return IsDecimalKey(event)" Style="text-align: Right" Font-Size="Large" AutoPostBack="True" OnTextChanged="txtAmt_TextChanged"></asp:TextBox>

                            </td>
                            <td>
                                <asp:TextBox ID="txtRate" runat="server" CssClass="cls text" Width="88px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    onkeypress="return IsDecimalKey(event)" Style="text-align: Right" Font-Size="Large" AutoPostBack="True" OnTextChanged="txtRate_TextChanged"></asp:TextBox>

                            </td>
                            <td>
                                <asp:TextBox ID="txtConvertAmt" runat="server" CssClass="cls text" Width="175px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge" onkeyup="javascript:this.value=Comma(this.value);"
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
                <td colspan="3">
                    <asp:TextBox ID="txtNarration" runat="server" CssClass="cls text" Width="600px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
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

    <div id="DivReInput" runat="server">
        <table style="width: 340px; height: 130px; background-color: #e9e9e9;">

            <tr>

                <td style="text-align: center">
                    <asp:Label ID="lblReInput" runat="server" Text=""></asp:Label>

                </td>
            </tr>

            <tr>

                <td style="text-align: center">
                    <asp:TextBox ID="txtReInput" runat="server" CssClass="cls text" Width="250px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge" onkeyup="javascript:this.value=Comma(this.value);"
                        onkeypress="return IsDecimalKey(event)" Style="text-align: center" Font-Size="X-Large" AutoPostBack="True" OnTextChanged="txtReInput_TextChanged"></asp:TextBox>

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
    <asp:Label ID="CtrlAccNo" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="CtrlAccType" runat="server" Visible="False"></asp:Label>

    <asp:Label ID="lblReInputFlag" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblMsgFlag" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblLdgCurrencyCode" runat="server" Visible="False"></asp:Label>

    <asp:HiddenField ID="hPartCode" runat="server" />
</asp:Content>
