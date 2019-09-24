<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="A2ZCurrencySale.aspx.cs" Inherits="ATOZWEBGMS.Pages.A2ZCurrencySale" Title="Currency Sale" %>

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
              $("#<%= ddlPartyName.ClientID %>").chosen();

              var prm = Sys.WebForms.PageRequestManager.getInstance()

              prm.add_endRequest(function () {
                  $("#<%= ddlPartyName.ClientID %>").chosen();

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

    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="4">Sale Currency
                    </th>
                </tr>
            </thead>
         
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Party Name" Width="120px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtPartyCode" runat="server" CssClass="cls text" Width="115px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="True" MaxLength="6" OnTextChanged="txtPartyCode_TextChanged" TabIndex="1" autocomplete="off"></asp:TextBox>
                    &nbsp;<asp:TextBox ID="txtPartyName" runat="server" Visible="false" CssClass="cls text" Width="304px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="1"></asp:TextBox>
                    <asp:DropDownList ID="ddlPartyName" runat="server" CssClass="youpii" class="chzn-select" Width="316px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged" ></asp:DropDownList>
                    &nbsp; &nbsp; &nbsp;
                    <script type="text/javascript" src="../Script/chosen.jquery.js"></script>
                </td>              
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Ledger Code :" Width="120px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAccNo" runat="server" Enabled="false" CssClass="cls text" Width="115px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                          Font-Size="Large" TabIndex="1" autocomplete="off"></asp:TextBox>
                    <asp:DropDownList ID="ddlAccNo" runat="server" CssClass="cls text" Width="200px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlAccNo_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label11" runat="server" Text="Ledger Currency :" Visible="false" Width="120px" Font-Size="Large" ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtLedgerCurrency" runat="server" Visible="false" CssClass="cls text" Style="text-align: right" Width="60px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large"  Enabled="false" ></asp:TextBox>
                    </td>
                    <td>
                    <asp:Label ID="Label12" runat="server" Text="Ledger Balance :" Width="140px" Font-Size="Large" ForeColor="Red"></asp:Label>
                        </td>
                    <td>
                    <asp:TextBox ID="txtledgerBalance" runat="server" CssClass="cls text" Style="text-align: right" Width="180px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large"  Enabled="false" ></asp:TextBox>
                </td>
                                
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Sale Currency " Width="120px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="cls text" Width="150px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>                
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Cash " Width="120px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCash" runat="server" CssClass="cls text" Width="150px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="ddlCash_SelectedIndexChanged">
                    </asp:DropDownList>
                   </td>
                    <td>
                    <asp:Label ID="Label2" runat="server" Text="Balance :" Width="82px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                    <td>
                    <asp:TextBox ID="txtBalance" runat="server" CssClass="cls text" Style="text-align: right" Width="180px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large"  Enabled="false" ></asp:TextBox>
                </td>                 
            </tr>
           
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Sale " Width="120px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td colspan="3">
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
                                <asp:TextBox ID="txtPurchaseAmt" runat="server" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge" onkeyup="javascript:this.value=Comma(this.value);"
                                    onkeypress="return IsDecimalKey(event)" Style="text-align: Right" Font-Size="Large" AutoPostBack="True" OnTextChanged="txtPurchaseAmt_TextChanged" autocomplete="off"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRate" runat="server" CssClass="cls text" Width="88px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    onkeypress="return IsDecimalKey(event)" Style="text-align: Right" Font-Size="Large" AutoPostBack="True" OnTextChanged="txtRate_TextChanged" autocomplete="off"></asp:TextBox>
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
                <td colspan="3">
                    <asp:TextBox ID="txtNarration" runat="server" CssClass="cls text" Width="435px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" Enabled="False"></asp:TextBox>
                </td>                 
            </tr>
              <tr>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="Description " Font-Size="Large" ForeColor="Red" Width="120px"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="cls text" Width="435px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" ></asp:TextBox>
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
    <asp:Label ID="lblReInputFlag" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="CtrlAccType" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="CtrlAccNo" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblMsgFlag" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblLdgCurrencyCode" runat="server" Visible="False"></asp:Label>
    <asp:HiddenField ID="hPartCode" runat="server" />

</asp:Content>
