<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="A2ZGoldInvoiceReport.aspx.cs" Inherits="ATOZWEBGMS.Pages.A2ZGoldInvoiceReport" Title="Gold Invoice Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../Styles/structure.css" rel="stylesheet" />--%>
    <style type="text/css">
        body {
            background: url(../Images/PageBackGround.jpg)no-repeat;
            background-size: cover;
        }
    </style>

       <%--<script language="javascript" type="text/javascript">
           $(function () {
               $("#<%= txtFromDate.ClientID %>").datepicker();
              

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtFromDate.ClientID %>").datepicker();
               

            });

        });

    </script>--%>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <br />

    <div id="DivMain" runat="server" align="center">
        <br />

        <table class="style1" width="800px">
              <thead>
                <tr>
                    <th colspan="4">Invoice Report
                    </th>
                </tr>

            </thead>

            <%--<tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="" Width="150px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Invoice Type :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlInvoiceType" runat="server" CssClass="cls text" Width="316px" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Gold Purchase Unfixed</asp:ListItem>
                        <asp:ListItem Value="2">Gold Purchase Fixed</asp:ListItem>
                        <asp:ListItem Value="3">Gold In</asp:ListItem>
                        <asp:ListItem Value="4">Gold Sale Unfixed</asp:ListItem>
                        <asp:ListItem Value="5">Gold Sale Fixed</asp:ListItem>
                        
                    </asp:DropDownList>
                </td>
                
                <td>
               <asp:Label ID="Label5" runat="server" Text="" Width="150px" Font-Size="Large" ForeColor="Red"></asp:Label>
           </td>
            </tr>--%>


            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="" Width="150px" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Invoice No. :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtVchNo" runat="server" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="6" AutoPostBack="true" OnTextChanged="txtVchNo_TextChanged"></asp:TextBox>
                  
                </td>

                
           <td>
               <asp:Label ID="Label2" runat="server" Text="" Width="150px" Font-Size="Large" ForeColor="Red"></asp:Label>
           </td>
             
            </tr>
            
             
            <tr>
                <td></td>
                <td colspan ="5">

                    <asp:Button ID="btnView" runat="server" Text="Preview/Print"
                        Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button blue"
                        OnClientClick="return ValidationBeforeSave()" Height="27px" OnClick="btnView_Click" />
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

    <asp:Label ID="CtrlRecordType" runat="server" Visible="False"></asp:Label>

     <asp:HiddenField ID="hPartCode" runat="server" />

</asp:Content>
