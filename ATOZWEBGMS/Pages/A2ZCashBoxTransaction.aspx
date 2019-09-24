<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="A2ZCashBoxTransaction.aspx.cs" Inherits="ATOZWEBGMS.Pages.A2ZCashBoxTransaction"
    Title="Cashbox Transaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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

        function cashbox() {


            <%--var ddlTrnCode = document.getElementById('<%=ddlTrnCode.ClientID%>');
            var txtKeyNo = document.getElementById('<%=txtKeyNo.ClientID%>').value;



            if (ddlTrnCode.selectedIndex == '' || ddlTrnCode.length == 0)
                alert('Please select Transaction Code.');
            else if (txtKeyNo == '' || txtKeyNo.length == 0)
                alert('Please Input No..');

            else
                return confirm('Are you sure you want save the data');
            return false;--%>
        }
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
    <div id="DivMain" align="center" runat="server">
        <br />
        <br />
        <table class="style1" style="align-items: center">
            <thead>
                <tr>
                    <th colspan="3">Cash Box Transaction
                    </th>
                </tr>
            </thead>
            <tbody>

                

                <tr>

                    <td colspan="8">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      <asp:RadioButton ID="rbtOptReceive" runat="server" GroupName="GLRptGrpPrm" Font-Size="X-Large" Text="Receive" Style="font-weight: 700" Font-Italic="True" AutoPostBack="true" OnCheckedChanged="rbtOptReceive_CheckedChanged" />
                        &nbsp;&nbsp; &nbsp;&nbsp;  
                           <asp:RadioButton ID="rbtOptPayment" runat="server" GroupName="GLRptGrpPrm" Font-Size="X-Large" Text="Payment" Style="font-weight: 700" Font-Italic="True" AutoPostBack="true" OnCheckedChanged="rbtOptPayment_CheckedChanged" />

                        &nbsp;&nbsp; &nbsp;&nbsp;  
                           <asp:RadioButton ID="rbtOptJournal" runat="server" GroupName="GLRptGrpPrm" Font-Size="X-Large" Text="Journal" Style="font-weight: 700" Font-Italic="True" AutoPostBack="true" OnCheckedChanged="rbtOptJournal_CheckedChanged" />

                    </td>


                </tr>


                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Currency :" Width="170px" Font-Size="Large" ForeColor="Red"></asp:Label>

                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="cls text" Width="150px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged">
                        </asp:DropDownList>

                    </td>
                    </tr>


                <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="By Cash " Width="120px" Font-Size="Large" ForeColor="Red"></asp:Label>

                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCash" runat="server" CssClass="cls text" Width="150px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="ddlCash_SelectedIndexChanged">
                            </asp:DropDownList>

                            <asp:DropDownList ID="ddlAccountName" Visible="false" runat="server" CssClass="cls text" Width="150px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Font-Size="Large">
                            </asp:DropDownList>

                        </td>
                   
                    </tr>


                <tr>
                    <td>
                        <asp:Label ID="lblPartyName" runat="server" Text="" Width="120px" Font-Size="Large" ForeColor="Red"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtPartyCode" runat="server" CssClass="cls text" Width="115px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Large" AutoPostBack="True" MaxLength="6" OnTextChanged="txtPartyCode_TextChanged" TabIndex="1"></asp:TextBox>
                        &nbsp;<asp:TextBox ID="txtPartyName" runat="server" Visible="false" CssClass="cls text" Width="304px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Large" TabIndex="1" ></asp:TextBox>

                        <asp:DropDownList ID="ddlPartyName" runat="server" CssClass="youpii" class="chzn-select" Width="316px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged">
                        </asp:DropDownList>
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

                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtLedgerCurrency" runat="server" Visible="false" CssClass="cls text" Style="text-align: right" Width="60px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" Enabled="false"></asp:TextBox>
                    </td>
                    


                </tr>


                

                    

               

                 <tr>
                    <td>
                        <asp:Label ID="lblCashBoxAmount" runat="server" Text="" Width="120px" Font-Size="Large" ForeColor="Red"></asp:Label>
                    </td>

                    <td>
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="cls text" Width="150px" Height="24px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Large" onkeypress="return IsDecimalKey(event)" AutoPostBack="True" OnTextChanged="txtAmount_TextChanged" onkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox>
                        
                    </td>
                </tr>

                <%--<tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Transaction Code" Width="167px" Font-Size="Large" ForeColor="Red"></asp:Label>
                    </td>


                    <td>
                        <asp:DropDownList ID="ddlTrnCode" runat="server" AutoPostBack="True" CssClass="cls text" Width="256px" Height="28px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium"
                            OnSelectedIndexChanged="ddlTrnCode_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>--%>

               
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Narration" Width="120px" Font-Size="Large" ForeColor="Red"></asp:Label>
                    </td>

                    <td>
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="cls text" Width="356px" Height="24px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium"></asp:TextBox>
                        
                    </td>
                </tr>
               





            </tbody>
        </table>
    </div>
    <div id="Div1" align="center" runat="server">
        <table class="style1">
            <%-- <tbody>--%>
            <tr>
                <td>
                    <asp:Label ID="lblLedgerBalance" runat="server" Font-Size="X-Large" Text="Current Ledger Balance : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtledgerBalance" runat="server" Enabled="false" Style="text-align: right" Text="" Font-Size="X-Large"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblCashBalance" runat="server" Font-Size="X-Large" Text="Current Cash Balance : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCashBalance" runat="server" Enabled="false" Style="text-align: right" Text="" Font-Size="X-Large"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblMessage" runat="server" Width="500px"></asp:Label>
                </td>
            </tr>
            <tr>
                <th></th>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="button  green size-80"
                        OnClick="btnSave_Click" OnClientClick="return ValidationBeforeSave()" Height="27" />
                    &nbsp;<asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="button red size-80 "
                        OnClick="btnExit_Click" Height="27" />
                </td>
            </tr>
            <%-- </tbody>--%>
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

    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblIDName" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlKeyNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCtrlFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblBranchNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblProcDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblNewSRL" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlVchNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblYear" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblTrnMode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCompanyName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblBranchName" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblflag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlBackUpStat" runat="server" Text="" Visible="false"></asp:Label>

    <asp:TextBox ID="txtComposeSms" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtMobileNo" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="lblSMSPort" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="lblSMSTransaction" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="lblInputAmount" runat="server" Visible="false"></asp:TextBox>


    <asp:TextBox ID="lblMsgFlag" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="CtrlAccType" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="lblLdgCurrencyCode" runat="server" Visible="false"></asp:TextBox>

    <asp:TextBox ID="CtrlVoucherNo" runat="server" Visible="false"></asp:TextBox>
    
    
</asp:Content>
