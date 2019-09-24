<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="A2ZDailyReverseTransaction.aspx.cs" Inherits="ATOZWEBGMS.Pages.A2ZDailyReverseTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


     <style type="text/css">
        body {
            background: url(../Images/PageBackGround.jpg)no-repeat;
            background-size: cover;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save information?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Reverse Transaction?');
        }

    </script>

    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 385px;
            margin: 0 auto;
            width: 1300px;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            /*width: 1150px;*/
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <br />
    <div align="center">

        <table class="style1">

            <thead>
                <tr>
                    <th colspan="3">Daily Reverse Transaction
                    </th>
                </tr>

            </thead>

        <%--</table>

        <table>--%>
           
            <tr>
                <td>
                    <asp:Label ID="lblVoucherNo" runat="server" Text="Voucher No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="cls text" Width="192px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large"></asp:TextBox>

                    <asp:Button ID="BtnSearch" runat="server" Text="Search" Font-Bold="True" Font-Size="Medium"
                        ForeColor="White" CssClass="button green" OnClick="BtnSearch_Click" />

                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />

    <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvDetailInfo_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
               

                 <asp:BoundField HeaderText="Func Opt." DataField="FuncOptDesc" HeaderStyle-Width="300px" ItemStyle-Width="300px" />

                 <asp:BoundField HeaderText="Description" DataField="TrnDesc" HeaderStyle-Width="300px" ItemStyle-Width="300px" />

              
                <asp:TemplateField HeaderText="Debit Amount" HeaderStyle-Width="150px" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="DrAmount" runat="server"  Enabled="false" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("TrnDebitAmt"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Credit Amount" HeaderStyle-Width="150px" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="CrAmount" runat="server"  Enabled="false" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("TrnCreditAmt"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
              
            </Columns>

        </asp:GridView>
        <%-- </div>--%>

        <table>
            <tr>
                <td></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDelete" runat="server" Text="Reverse" Font-Bold="True" Font-Size="Medium" Height="27px" Width="120px"
                    ForeColor="White" CssClass="button green" OnClientClick="return ValidationBeforeUpdate()" OnClick="btnDelete_Click" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Font-Bold="True" Font-Size="Medium" Height="27px" Width="120px"
                    ForeColor="White" CssClass="button blue" OnClick="btnCancel_Click" />
                    &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>
        </table>

    </div>

    <div id="DivExit" runat="server">
        <table style="width: 340px; height: 130px; background-color: #e9e9e9;">
            
            <tr>

                <td style="text-align: center">
                    <asp:Label ID="Label5" runat="server" Text="Day End Confirmation Done"></asp:Label>

                </td>
            </tr>

            <tr>

                <td style="text-align: center">
                    <asp:Button ID="btnOkay" runat="server" Text="OK"
                        Height="27" Width="96px" OnClick="btnOkay_Click" />
                    
                </td>
            </tr>
        </table>
    </div>


    <asp:Label ID="CtrlTrnDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="ValidityFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAtyClass" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlBackUpStat" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="HdnFuncOpt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="HdnFOpt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="HdnModule" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnPrmValue" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="HdnAccountNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblGLAccBal" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblVchAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblVchTrnType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblTrnRevFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblGLBalanceType" runat="server" Text="" Visible="false"></asp:Label>

     <asp:Label ID="lblTrnID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblChgFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCanFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblRecID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblBranchNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblUnitFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblPayType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="ErrMsg" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>

