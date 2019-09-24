<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="A2ZLoanInterestPayment.aspx.cs" Inherits="ATOZWEBGMS.Pages.A2ZLoanInterestPayment" Title="Interest Payment" %>

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


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <br />

    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="4">Loan Interest Payment
                    </th>
                </tr>

            </thead>




            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Party Code :" Width="150px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                      <asp:TextBox ID="txtPartyCode" runat="server" CssClass="cls text" Width="78px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="True" MaxLength="6" OnTextChanged="txtPartyCode_TextChanged" TabIndex="1"></asp:TextBox>
                    &nbsp;<asp:TextBox ID="txtPartyName" runat="server" CssClass="cls text" Width="220px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="1"></asp:TextBox>
                &nbsp; &nbsp; &nbsp;
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
                    <asp:Label ID="Label11" runat="server" Text="Loan Type :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlLoanType" runat="server" CssClass="cls text" Width="316px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlLoanType_SelectedIndexChanged" TabIndex="3">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <%--<asp:ListItem Value="61">Long Term Loan (LLT)</asp:ListItem>
                        <asp:ListItem Value="62">Short Term Loan (SLT)</asp:ListItem>--%>
                        
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Principal Balance :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPrincipal" runat="server" Style="text-align: right" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" ReadOnly="True"></asp:TextBox>
                </td>

            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="Loan Acc. No. :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAccNo" runat="server" CssClass="cls text" Width="343px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlAccNo_SelectedIndexChanged" TabIndex="2">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Interest Balance :" Font-Size="Large" ForeColor="Red"></asp:Label>
          
                   </td>
                <td>
                    <asp:TextBox ID="txtIntBalance" runat="server" Style="text-align: right" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="7" ReadOnly="True"></asp:TextBox>
                </td>

            </tr>

            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Currency :" Font-Size="Large" ForeColor="Red"></asp:Label>
          
                   </td>
                <td>
                    <asp:TextBox ID="txtCurrency" runat="server" Style="text-align: right" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="7" ReadOnly="True"></asp:TextBox>
                </td>

            </tr>

            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Interest Rate % :" Font-Size="Large" ForeColor="Red"></asp:Label>
          
                   </td>
                <td>
                    <asp:TextBox ID="txtInterestRate" runat="server" Style="text-align: right" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="7" ReadOnly="True"></asp:TextBox>
                </td>

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
                        Font-Size="Large" TabIndex="8"></asp:TextBox>
               <%-- </td>
                
                <td>--%>
                    &nbsp;&nbsp;
                    <asp:Label ID="lblAccountName" runat="server" Text="Account Name :" Font-Size="Large" ForeColor="Red"></asp:Label>

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
                    <asp:Label ID="Label7" runat="server" Text="Loan Interest Amount :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLoanIntAmount" runat="server" CssClass="cls text" Width="150px" Height="27px" Style="text-align: Right" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="6"></asp:TextBox>
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
                        Font-Size="Large" TabIndex="9"></asp:TextBox>
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
