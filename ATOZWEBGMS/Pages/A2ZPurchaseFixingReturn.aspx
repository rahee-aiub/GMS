<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="A2ZPurchaseFixingReturn.aspx.cs" Inherits="ATOZWEBGMS.Pages.A2ZPurchaseFixingReturn" Title="Purchase - Fixing Return" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../Styles/structure.css" rel="stylesheet" />--%>
    <style type="text/css">
        body {
            background: url(../Images/PageBackGround.jpg)no-repeat;
            background-size: cover;
        }

        .grid_scroll {
            overflow: auto;
            height: 200px;
            width: 1430px;
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


  <%--  <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtDueDate.ClientID %>").datepicker();


            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtDueDate.ClientID %>").datepicker();


            });

        });

    </script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--  <br />--%>


    <div  id="divMain" runat="server" align="center">

        <table>
            <tr>
                <td style="vertical-align: top;">
                    <table class="style1">

                        <thead>
                            <tr>
                                <th colspan="4">Purchase Fixing Return
                                </th>
                            </tr>

                        </thead>


                        <tr>

                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Metal Rate USD:" Font-Size="Large"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRateUSD" runat="server" CssClass="cls text" Width="78px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Height="25px" Font-Size="Large" MaxLength="50" AutoPostBack="True" OnTextChanged="txtRateUSD_TextChanged"></asp:TextBox>
                                
                                &nbsp;Per OZ&nbsp;

                                &nbsp;

                                <asp:Label ID="Label18" runat="server" Text="Metal Rate (AED):" Font-Size="Large"
                                    ForeColor="Red"></asp:Label>
                                &nbsp;
                                 <asp:TextBox ID="txtMetalRate" runat="server" CssClass="cls text" Width="97px" BorderColor="#1293D1" BorderStyle="Ridge"
                                     Height="25px" Font-Size="Large" MaxLength="50" Style="text-align: Right" Enabled="False"></asp:TextBox>
                                  &nbsp;Per GMS</td>
                            </td>


                        </tr>

                        <tr>

                            <td>
                                <asp:Label ID="Label15" runat="server" Text="Party Name:" Font-Size="Large"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPartyCode" runat="server" CssClass="cls text" Width="78px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" MaxLength="6" TabIndex="1"  AutoPostBack="true" OnTextChanged="txtPartyCode_TextChanged"></asp:TextBox>

                                &nbsp;<asp:DropDownList ID="ddlPartyName" runat="server" CssClass="youpii" class="chzn-select" Width="316px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp; &nbsp; &nbsp;
                    <script type="text/javascript" src="../Script/chosen.jquery.js"></script>
                            </td>

                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Address:" Font-Size="Large"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPartyAddress" runat="server" CssClass="cls text" Width="397px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Height="25px" Font-Size="Large" MaxLength="50"></asp:TextBox>
                            </td>

                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label16" runat="server" Text="Location:" Font-Size="Large"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlLocation" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Width="250px" Font-Size="Large">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>

                                    <asp:ListItem Value="2">Dubai (DXB)</asp:ListItem>
                                    <asp:ListItem Value="1">Dhaka (DHK)</asp:ListItem>

                                </asp:DropDownList>

                            </td>

                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Currency :" Width="170px" Font-Size="Large" ForeColor="Red"></asp:Label>

                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="cls text" Width="150px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged">
                                </asp:DropDownList>

                            </td>


                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Pure Wt:" Width="170px" Font-Size="Large" ForeColor="Red"></asp:Label>

                            </td>
                            <td>
                                <asp:TextBox ID="txtGoldPureWt" runat="server" CssClass="cls text" Width="150px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Height="25px" Font-Size="Large" MaxLength="50" AutoPostBack="True" OnTextChanged="txtGoldPureWt_TextChanged" ></asp:TextBox>

                                  &nbsp;

                                  <asp:Label ID="Label7" runat="server" Text="Total Value (AED):" Width="159px" Font-Size="Large" ForeColor="Red"></asp:Label>
                                  <asp:TextBox ID="txtTotalValue" runat="server" CssClass="cls text" Width="150px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Height="25px" Font-Size="Large" Style="text-align: Right" MaxLength="50" ></asp:TextBox>

                            
                            </td>
                        </tr>
                         <tr>
                             <td>

                             </td>

                    <td class="auto-style1">
                    <asp:Button ID="BtnSubmit" runat="server" Text="Update" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" ToolTip="Insert Information" CssClass="button green"
                        OnClientClick="return ValidationBeforeSave()" OnClick="BtnSubmit_Click" Height="27px" />&nbsp;
                  
                   <asp:Button ID="btnCancel" runat="server" Text="Cancel" Font-Bold="True" Font-Size="Large"
                       ForeColor="White" CssClass="button blue"
                       OnClick="btnCancel_Click" Height="27px" />&nbsp;
                     <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                         Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                         CssClass="button red" OnClick="BtnExit_Click" />
                        <br />
                    </td>
                </tr>

                    </table>
                    <%-- <h1>Gold Purchase - Fixed</h1>--%>
                </td>

            </tr>
        </table>

    </div>

    <div id="divPost" runat="server" align="center">
        <br />
        <table class="style1" style="width: 700px; height: 370px; background-color: #e9e9e9;">
            <tr>
                <td style="vertical-align: top;">
                    <table class="style1" style="width: 340px; height: 280px;">
                        <thead>
                            <tr>
                                <th colspan="4">
                                    <asp:Label ID="Label41" runat="server" Text="Fixed - Gold Sale" Font-Size="Large"
                                        ForeColor="Black"></asp:Label>
                                </th>
                            </tr>

                        </thead>

                           <tr>
                            <td>
                                <asp:Label ID="Label26" runat="server" Text="Pure Wt:" Font-Size="Large"
                                    ForeColor="#004600"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTotalNet" runat="server" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" Style="text-align: Right" ReadOnly="True"></asp:TextBox>
                            </td>

                        </tr>
                   

                        <tr>
                            <td>
                                <asp:Label ID="Label27" runat="server" Text="Metal Value:" Font-Size="Large"
                                    ForeColor="#004600"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMetal" runat="server" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" Style="text-align: Right" ReadOnly="True"></asp:TextBox>
                            </td>

                        </tr>
                     
                     
                        <tr>
                            <td>
                                <asp:Label ID="Label30" runat="server" Text="Discount:" Font-Size="Large"
                                    ForeColor="#004600"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDiscount" runat="server"  CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" ReadOnly="True" Style="text-align: Right"></asp:TextBox>
                            </td>

                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label31" runat="server" Text="Total Value:" Font-Size="Large"
                                    ForeColor="#004600"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTotalValueView" runat="server" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" Style="text-align: Right" ReadOnly="True"></asp:TextBox>
                            </td>

                        </tr>



                    </table>
                </td>
                <td style="vertical-align: top;">
                    <table class="style1" style="width: 340px; height: 280px;">
                        <thead>
                            <tr>
                                <th colspan="4">
                                    <asp:Label ID="Label42" runat="server" Text="Conversion Area" Font-Size="Large"
                                        ForeColor="Black"></asp:Label>
                                </th>
                            </tr>

                        </thead>


                        <tr>

                            <td>
                                <asp:Label ID="Label32" runat="server" Text="Convert Currency:" Font-Size="Large"
                                    ForeColor="#004600"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlConvCurrency" runat="server" CssClass="cls text" Width="150px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlConvCurrency_SelectedIndexChanged">
                                </asp:DropDownList>


                            </td>

                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label33" runat="server" Text="Convert Rate:" Font-Size="Large"
                                    ForeColor="#004600"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtConvRate" runat="server" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    onFocus="javascript:this.select()" Font-Size="Large" Style="text-align: Right" AutoPostBack="True" OnTextChanged="txtConvRate_TextChanged"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label34" runat="server" Text="Metal Value:" Font-Size="Large"
                                    ForeColor="#004600"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtConvMetal" runat="server" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" Style="text-align: Right" ReadOnly="True"></asp:TextBox>
                            </td>

                        </tr>
                       
                     


                    
                        <tr>
                            <td>
                                <asp:Label ID="Label37" runat="server" Text="Discount:" Font-Size="Large"
                                    ForeColor="#004600"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtConvDiscount" runat="server" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    onFocus="javascript:this.select()" Font-Size="Large" Style="text-align: Right" ReadOnly="True" AutoPostBack="True" OnTextChanged="txtConvDiscount_TextChanged"></asp:TextBox>
                            </td>

                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label38" runat="server" Text="Net Amount:" Font-Size="Large"
                                    ForeColor="#004600"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtConvNetAmt" runat="server" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" Style="text-align: Right" ReadOnly="True"></asp:TextBox>
                            </td>

                        </tr>



                    </table>
                </td>
            </tr>



            <tr>

                <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnPost" runat="server" Text="Post" Font-Bold="True" Font-Size="Large" OnClientClick="return ValidationBeforeSave()"
                        ForeColor="White" CssClass="button green" Height="30px" OnClick="btnPost_Click" Width="110px" />

                    &nbsp;&nbsp;

                       <asp:Button ID="btnBack" runat="server" Text="Back" Font-Bold="True" Font-Size="Large"
                           ForeColor="White" CssClass="button red" Height="30px" OnClick="btnBack_Click" Width="110px" />
                </td>
            </tr>
        </table>


    </div>

       <div id="DivReInput" runat="server">
        <table style="width: 360px; height: 150px; background-color: #e9e9e9;">
            
            <tr>

                <td style="text-align: center">
                    <asp:Label ID="lblReInput" runat="server" Text=""></asp:Label>

                </td>
            </tr>

            <tr>

                <td style="text-align: center">
                    <asp:TextBox ID="txtReInput" runat="server" CssClass="cls text" Width="250px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    onkeypress="return IsDecimalKey(event)" Style="text-align: center" Font-Size="X-Large" AutoPostBack="True" OnTextChanged="txtReInput_TextChanged"></asp:TextBox>
                    
                </td>
            </tr>
        </table>
    </div>

   
     
        <asp:Label ID="lblLastLPartyNo" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblNewLPartyNo" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="CtrlVoucherNo" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblProcesDate" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblPartyAccType" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblDescription" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblPartyAccno" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblStockAccType" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblStockAccno" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblReInputFlag" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblAEDRate" runat="server" Visible="False"></asp:Label>
    
</asp:Content>
