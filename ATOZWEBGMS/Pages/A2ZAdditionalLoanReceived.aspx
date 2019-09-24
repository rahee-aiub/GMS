<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="A2ZAdditionalLoanReceived.aspx.cs" Inherits="ATOZWEBGMS.Pages.A2ZAdditionalLoanReceived" Title="New Loan Receive" %>

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

    <%--<script type="text/javascript">

        $(document).ready(function () {
            $("#<%=txtPartyName.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("GMSWebService.asmx/GetPartyName") %>',
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


    </script>--%>

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
                    <th colspan="4">Additional Loan Receive
                    </th>
                </tr>

            </thead>

            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="Loan Type :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlLoanType" runat="server" CssClass="cls text" Width="316px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlLoanType_SelectedIndexChanged" TabIndex="3">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                       <%-- <asp:ListItem Value="61">Long Term Loan (LLT)</asp:ListItem>
                        <asp:ListItem Value="62">Short Term Loan (SLT)</asp:ListItem>--%>
                        
                    </asp:DropDownList>
                </td>
                
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Current Balanace :" Width="180px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCurrentBalance" runat="server" Style="text-align: right" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Party Code :" Width="150px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                      <asp:TextBox ID="txtPartyCode" runat="server" CssClass="cls text" Width="78px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="True" MaxLength="6" OnTextChanged="txtPartyCode_TextChanged" TabIndex="1" autocomplete="off"></asp:TextBox>
                    &nbsp;<asp:DropDownList ID="ddlPartyName" runat="server" CssClass="youpii" class="chzn-select" Width="316px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged" ></asp:DropDownList>
                    &nbsp; &nbsp; &nbsp;
                    <script type="text/javascript" src="../Script/chosen.jquery.js"></script>
                </td>
                
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Interest Rate % :" Font-Size="Large" ForeColor="Red"></asp:Label>
          
                   </td>
                <td>
                    <asp:TextBox ID="txtInterestRate" runat="server" Style="text-align: right" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="7" ReadOnly="True"></asp:TextBox>
                </td>

                <%--<td>
                    <asp:Label ID="Label3" runat="server" Text="Currency :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCurrency" runat="server" Style="text-align: right" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" ReadOnly="True"></asp:TextBox>
                </td>--%>

            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Currency :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="cls text" Width="316px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td></td>
                <td></td>

            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="Ledger Code :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAccNo" runat="server" CssClass="cls text" Width="120px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                          Font-Size="Large" TabIndex="1" autocomplete="off"></asp:TextBox>
                    <asp:DropDownList ID="ddlAccNo" runat="server" CssClass="cls text" Width="200px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlAccNo_SelectedIndexChanged" TabIndex="2">
                    </asp:DropDownList>
                </td>
                
                <td></td>
                <td></td>

            </tr>

        </table>
        <br />

        <table class="style1">
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Transaction Type :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTransactionType" runat="server" CssClass="cls text" Width="150px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlTransactionType_SelectedIndexChanged" TabIndex="5">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                         <asp:ListItem Value="1">Cash</asp:ListItem>
                        <asp:ListItem Value="47">Cheque</asp:ListItem>
                    </asp:DropDownList>
                      &nbsp; &nbsp; &nbsp; 
                </td>
          <td>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblChequeNo" runat="server" Text="Cheque No. :" Font-Size="Large" ForeColor="Red"></asp:Label>

                <%--</td>
                <td colspan="5">--%>
                     &nbsp;
                     <asp:TextBox ID="txtChequeNo" runat="server" CssClass="cls text" Width="100px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="8" autocomplete="off"></asp:TextBox>
               <%-- </td>
                
                <td>--%>
                    &nbsp;&nbsp;
                    <asp:Label ID="lblAccountName" runat="server" Text="Cash Name :" Font-Size="Large" ForeColor="Red"></asp:Label>

                <%--</td>
                <td colspan="5">--%>
                     &nbsp;
                     <asp:DropDownList ID="ddlAccountName" runat="server" CssClass="cls text" Width="250px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="5">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>
               

            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Additional Loan Amount :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAdditionalLoanAmount" runat="server" CssClass="cls text" Width="150px" Height="27px" Style="text-align: Right" BorderColor="#1293D1" BorderStyle="Ridge" onkeyup="javascript:this.value=Comma(this.value);"
                        Font-Size="Large" TabIndex="6" autocomplete="off" AutoPostBack="true" OnTextChanged="txtAdditionalLoanAmount_TextChanged"></asp:TextBox>
                      &nbsp; &nbsp; &nbsp; 
                </td>
           
               <td></td>
            </tr>
            
             <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Transaction Descritiopn :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtTranDesc" runat="server" CssClass="cls text" Width="577px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="9" autocomplete="off"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td></td>
                <td colspan ="5">

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
                                    onkeypress="return IsDecimalKey(event)" Style="text-align: center" Font-Size="X-Large" autocomplete="off" AutoPostBack="True" OnTextChanged="txtReInput_TextChanged"></asp:TextBox>
                    
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
    <asp:Label ID="lblMsgFlag" runat="server" Visible="False"></asp:Label>
     <asp:HiddenField ID="hPartCode" runat="server" />

</asp:Content>
