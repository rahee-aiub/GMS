<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="A2ZGoldRcvPayment.aspx.cs" Inherits="ATOZWEBGMS.Pages.A2ZGoldRcvPayment" Title="Gold Receive/Payment" %>

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

        $(function () {
            $("#<%= ddlPayPartyName.ClientID %>").chosen();

            var prm = Sys.WebForms.PageRequestManager.getInstance()

            prm.add_endRequest(function () {
                $("#<%= ddlPayPartyName.ClientID %>").chosen();

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




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--  <br />--%>


    <div runat="server" id="DivMain" align="center">

        <table>
            <tr>
                <td style="vertical-align: top;">
                    <table class="style1">

                        <thead>
                            <tr>
                                <th colspan="4">Pure Gold Receive/Payment
                                </th>
                            </tr>

                        </thead>

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
                                <asp:Label ID="Label15" runat="server" Text="Receive Party Name:" Font-Size="Large"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPartyCode" runat="server" CssClass="cls text" Width="78px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" MaxLength="6" TabIndex="1" AutoPostBack="true" OnTextChanged="txtPartyCode_TextChanged"></asp:TextBox>

                                &nbsp;<asp:DropDownList ID="ddlPartyName" runat="server" CssClass="youpii" class="chzn-select" Width="316px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp; &nbsp; &nbsp;
                    <script type="text/javascript" src="../Script/chosen.jquery.js"></script>
                            </td>

                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Premium:" Width="170px" Font-Size="Large" ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPremurm" runat="server" CssClass="cls text" Width="150px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Height="25px" Font-Size="Large" MaxLength="50" onkeydown="return (event.keyCode !=13);" onkeypress="return IsDecimalKey(event)"></asp:TextBox>

                            </td>
                        </tr>

                        <tr>

                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Payment Party Name:" Font-Size="Large"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPayPartyCode" runat="server" CssClass="cls text" Width="78px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" MaxLength="6" TabIndex="1" AutoPostBack="true" OnTextChanged="txtPayPartyCode_TextChanged"></asp:TextBox>

                                &nbsp;<asp:DropDownList ID="ddlPayPartyName" runat="server" CssClass="youpii" class="chzn-select" Width="316px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlPayPartyName_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp; &nbsp; &nbsp;
                    <script type="text/javascript" src="../Script/chosen.jquery.js"></script>
                            </td>

                        </tr>



                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Pure Wt:" Width="170px" Font-Size="Large" ForeColor="Red"></asp:Label>

                            </td>
                            <td>
                                <asp:TextBox ID="txtGoldPureWt" runat="server" CssClass="cls text" Width="150px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Height="25px" Font-Size="Large" MaxLength="50" onkeydown="return (event.keyCode !=13);" onkeypress="return IsDecimalKey(event)" AutoPostBack="True" OnTextChanged="txtGoldPureWt_TextChanged"></asp:TextBox>

                                &nbsp;

                                                              
                            </td>
                        </tr>
                        
                        



                        

                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Description:" Width="170px" Font-Size="Large" ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txttrnDesc" runat="server" CssClass="cls text" Width="397px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Height="25px" Font-Size="Large" MaxLength="50" onkeydown="return (event.keyCode !=13);"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td></td>

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


    <div id="DivReInput" runat="server">
        <table style="width: 340px; height: 130px; background-color: #e9e9e9;">

            <tr>

                <td style="text-align: center">
                    <asp:Label ID="lblReInput" runat="server" Text=""></asp:Label>

                </td>
            </tr>

            <tr>

                <td style="text-align: center">
                    <asp:TextBox ID="txtReInput" runat="server" CssClass="cls text" Width="250px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        onkeypress="return IsDecimalKey(event)" Style="text-align: center" Font-Size="X-Large" autocomplete="off" AutoPostBack="True" OnTextChanged="txtReInput_TextChanged"></asp:TextBox>

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
    <asp:Label ID="lblPayPartyAccType" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblPayPartyAccno" runat="server" Visible="False"></asp:Label>
</asp:Content>
