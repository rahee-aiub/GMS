﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="A2ZCashSummaryReport.aspx.cs" Inherits="ATOZWEBGMS.Pages.A2ZCashSummaryReport" %>

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

                <td class="auto-style1">
                    <asp:Label ID="lblfdate" runat="server" Text="Balance Date :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>

                <td class="auto-style1">

                    <asp:TextBox ID="txtfdate" runat="server" CssClass="cls text" Width="145px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" TabIndex="4"></asp:TextBox>

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

    <asp:HiddenField ID="hPartCode" runat="server" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
