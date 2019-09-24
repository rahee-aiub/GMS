<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="A2ZDailyLoanInterest.aspx.cs" Inherits="ATOZWEBGMS.Pages.A2ZDailyLoanInterest" Title="Daily Loan Interest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../Styles/structure.css" rel="stylesheet" />--%>
    <style type="text/css">
        body {
            background: url(../Images/PageBackGround.jpg)no-repeat;
            background-size: cover;
        }
    </style>

       <script language="javascript" type="text/javascript">
           $(function () {
               $("#<%= txtFromDate.ClientID %>").datepicker();
              

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtFromDate.ClientID %>").datepicker();
               

            });

        });

    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <br />

    <div id="DivMain" runat="server" align="center">
        <br />

        <table class="style1" width="800px">
              <thead>
                <tr>
                    <th colspan="4">Daily Loan Interest
                    </th>
                </tr>

            </thead>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="" Width="150px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Transaction Date :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" Enabled="false" CssClass="cls text" Width="120px" Height="27px" Style="text-align: Right" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="6"></asp:TextBox>
                  
                </td>

                
           <td>
               <asp:Label ID="Label2" runat="server" Text="" Width="150px" Font-Size="Large" ForeColor="Red"></asp:Label>
           </td>
             
            </tr>
            
             
            <tr>
                <td></td>
                <td colspan ="5">

                    <asp:Button ID="btnIntCharge" runat="server" Text="Int. Charge"
                        Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button blue"
                        Height="27px" OnClick="btnIntCharge_Click" />
                    &nbsp;
                    
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

    <asp:Label ID="lblLoanIntStat" runat="server" Visible="False"></asp:Label>

     <asp:HiddenField ID="hPartCode" runat="server" />

</asp:Content>
