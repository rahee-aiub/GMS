<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="A2ZAccountStatement.aspx.cs" Inherits="ATOZWEBGMS.Pages.A2ZAccountStatement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

      <style type="text/css">
        body {
            background: url(../Images/PageBackGround.jpg)no-repeat;
            background-size: cover;
        }
    </style>
    <%--<link href="../Styles/cupertino/jquery-ui-1.8.18.custom.css" rel="stylesheet" />--%>
    <%--<script src="../scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="../Styles/styletext.css" rel="stylesheet" />--%>

    <%-- <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>--%>


    <%--<script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtfdate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtfdate.ClientID %>").datepicker();

            });

        });

        $(function () {
            $("#<%= txttdate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txttdate.ClientID %>").datepicker();

            });

        });
    </script>--%>


    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtfdate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtfdate.ClientID %>").datepicker();

             });

        });

         $(function () {
             $("#<%= txttdate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txttdate.ClientID %>").datepicker();

            });

        });

    </script>

   <%-- <script type="text/javascript">

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


    <style type="text/css">
        .auto-style1 {
            height: 37px;
        }
    </style>


    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 250px;
            /*width: 520px;*/
            width: 520px;
            margin: 0 auto;
        }

        .grid1_scroll {
            overflow: auto;
            height: 300px;
            width: 1050px;
            margin: 0 auto;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            /*width: 490px;*/
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .auto-style1 {
            width: 225px;
        }
    </style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">


    <br />

    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="7">
                        <asp:Label ID="lblStatementFunc" runat="server" Text="Label"></asp:Label>

                    </th>
                </tr>

            </thead>

            <tr>
                <td>
                    <asp:Label ID="lblAccType" runat="server" Font-Size="Large" Text="A/C Type :" ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style8">
                    <asp:TextBox ID="txtAccType" runat="server" CssClass="cls text" Width="100px" Height="25px" Font-Size="Large" AutoPostBack="True" OnTextChanged="txtAccType_TextChanged"></asp:TextBox>

                    &nbsp;
                                                
                           <asp:DropDownList ID="ddlAccType" runat="server" CssClass="cls text" Height="25px" 
                               Width="335px" Font-Size="Large" TabIndex="0" AutoPostBack="True" OnSelectedIndexChanged="ddlAccType_SelectedIndexChanged">
                               <asp:ListItem Value="0">-Select-</asp:ListItem>
                           </asp:DropDownList>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Party Code :" Width="150px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                      <asp:TextBox ID="txtPartyCode" runat="server" CssClass="cls text" Width="100px" Height="25px"
                        Font-Size="Large" AutoPostBack="True" MaxLength="6" OnTextChanged="txtPartyCode_TextChanged" TabIndex="1"></asp:TextBox>
                    &nbsp;<asp:TextBox ID="txtPartyName" runat="server" Visible="false" CssClass="cls text" Width="220px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="1"></asp:TextBox>

                    <asp:DropDownList ID="ddlPartyName" runat="server" CssClass="youpii" class="chzn-select" Width="316px" Height="27px"
                        Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged" ></asp:DropDownList>
                    &nbsp; &nbsp; &nbsp;
                    <script type="text/javascript" src="../Script/chosen.jquery.js"></script>
                </td>
                
            </tr>

         
            <tr>
                <td>
                    <asp:CheckBox ID="chkAllCurrency" runat="server" AutoPostBack="True" OnCheckedChanged="chkAllCurrency_CheckedChanged" Font-Size="Large" ForeColor="Red" Text="   All" />
                    &nbsp;
                    <asp:Label ID="Label3" runat="server" Text="Currency " Width="120px" Font-Size="Large" ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="cls text" Width="150px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged" >
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>

                </td>
                
                
            </tr> 


            <tr>
                <td>
                    <asp:Label ID="lblAccountNo" runat="server" Text="Ledger Code :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAccountNo" runat="server" CssClass="cls text" Width="160px" Height="25px"
                        Font-Size="Large" AutoPostBack="true" OnTextChanged="txtAccountNo_TextChanged"></asp:TextBox>

                     <asp:Label ID="lblAccCurrency" runat="server" Font-Size="Large" Text=""></asp:Label>


                </td>
            </tr>







            <tr>

                <td class="auto-style1">
                    <asp:Label ID="lblfdate" runat="server" Text="From Date" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>

                <td class="auto-style1">

                    <asp:TextBox ID="txtfdate" runat="server" CssClass="cls text" Width="145px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" TabIndex="4"></asp:TextBox>


                    

                </td>



            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbltdate" runat="server" Text="To Date" Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>


                <td>
                    <asp:TextBox ID="txttdate" runat="server" CssClass="cls text" Width="145px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png"></asp:TextBox>

                    <asp:TextBox ID="tOpenDt" runat="server" CssClass="cls text" Width="50px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" Visible="False"></asp:TextBox>

                    <asp:TextBox ID="tMaturityDt" runat="server" CssClass="cls text" Width="40px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" Visible="False"></asp:TextBox>

                    <asp:TextBox ID="tRenewalDt" runat="server" CssClass="cls text" Width="40px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" Visible="False"></asp:TextBox>

                    <asp:TextBox ID="tAccPeriod" runat="server" CssClass="cls text" Width="37px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" Visible="False"></asp:TextBox>

                    <asp:TextBox ID="tOrgAmt" runat="server" CssClass="cls text" Width="28px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="tPrincipleAmt" runat="server" CssClass="cls text" Width="36px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="tIntRate" runat="server" CssClass="cls text" Width="51px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" Visible="False"></asp:TextBox>

                    <%--AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate--%>

                    <asp:TextBox ID="tAccLoanSancAmt" runat="server" CssClass="cls text" Width="33px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" Visible="False"></asp:TextBox>

                    <asp:TextBox ID="tAccFixedMthInt" runat="server" CssClass="cls text" Width="33px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" Visible="False"></asp:TextBox>


                    <asp:TextBox ID="tAccLoanSancDate" runat="server" CssClass="cls text" Width="33px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png" Visible="False"></asp:TextBox>

                    <asp:TextBox ID="tAccDisbAmt" runat="server" CssClass="cls text" Width="33px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png" Visible="False"></asp:TextBox>

                    <asp:TextBox ID="tAccDisbDate" runat="server" CssClass="cls text" Width="33px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png" Visible="False"></asp:TextBox>

                    <%--AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt--%>

                    <asp:TextBox ID="tAccNoInstl" runat="server" CssClass="cls text" Width="33px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png" Visible="False"></asp:TextBox>

                    <asp:TextBox ID="tAccLoanInstlAmt" runat="server" CssClass="cls text" Width="33px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png" Visible="False"></asp:TextBox>

                    <asp:TextBox ID="tAccLoanLastInstlAmt" runat="server" CssClass="cls text" Width="33px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png" Visible="False"></asp:TextBox>

                </td>

            </tr>

            <tr>


                <td>
                    <asp:CheckBox ID="ChkAccStatus" runat="server" Visible="false" Text="Active A/c" Height="30" Font-Size="Large" ForeColor="#CC6600" Checked="true" TabIndex="5" AutoPostBack="true" OnCheckedChanged="ChkAccStatus_CheckedChanged"></asp:CheckBox>
                </td>
                <td>

                </td>

            </tr>

            
            <tr>
                <td colspan=" 4"></td>

            </tr>
            <tr>

                <td></td>

                <td>


                    <asp:Button ID="BtnView" runat="server" Text="Preview/Print" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" Height="27px" Width="150px" CssClass="button green" OnClick="BtnView_Click" />&nbsp;
                      &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Font-Bold="True" Height="27px" Width="100px" CssClass="button red" ToolTip="Exit Page" CausesValidation="False"
                        OnClick="BtnExit_Click" />



                </td>
            </tr>

        </table>
    </div>

    <div id="Dtl2" runat="server" align="center" class="grid1_scroll">


        <asp:GridView ID="gvGroupAccInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvGroupAccInfo_RowDataBound" OnSelectedIndexChanged="gvGroupAccInfo_SelectedIndexChanged">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>



                <asp:BoundField HeaderText="Acc Type" DataField="AccType" HeaderStyle-Width="95px" ItemStyle-Width="95px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />

                <asp:BoundField HeaderText="Acc Title" DataField="AccTypeDescription" HeaderStyle-Width="300px" ItemStyle-Width="300px" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" />

                <asp:BoundField HeaderText="A/c No." DataField="AccNo" HeaderStyle-Width="140px" ItemStyle-Width="140px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />

                 <asp:BoundField HeaderText="Currency" DataField="CurrencyName" HeaderStyle-Width="140px" ItemStyle-Width="140px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />

                
                <asp:TemplateField HeaderText="Status" Visible="false" HeaderStyle-Width="200px" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="AccStatus" runat="server" Text='<%#Eval("AccStatus") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Status" HeaderStyle-Width="180px" ItemStyle-Width="180px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="AccStatusdesc" runat="server" Text='<%#Eval("AccStatusDescription") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px">
                    <ItemTemplate>
                        <asp:LinkButton Text="Select" ID="lnkSelect" runat="server" CommandName="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>



    </div>



    <asp:Label ID="CtrlAccType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblModule" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblBegFinYear" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblProcDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblflag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblcls" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblTrnCode" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlProgFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlAccStatus" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblMemType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblMemNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblBranchNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlBranchNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblUserLabel" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblUnitFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblIDName" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblPreAddressLine1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblPreTelephone" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblPreMobile" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblAutoRenewal" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblDepositAmount" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblTotDepositAmount" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblCompanyName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblBranchName" runat="server" Text="" Visible="false"></asp:Label>


    <asp:Label ID="lblUnitAddress1" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblDivident" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="MSGFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="MultiAccFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlOldAccNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="txtPreVillageCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="txtPrePostOfficeCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="txtPreThanaCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="txtPreDistrictCode" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="txtPreVillageDesc" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="txtPrePostOfficeDesc" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="txtPreThanaDesc" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="txtPreDistrictDesc" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCurrency" runat="server" Text="" Visible="false"></asp:Label>

    <asp:HiddenField ID="hPartCode" runat="server" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
