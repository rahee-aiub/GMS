<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="A2ZGoldIn.aspx.cs" Inherits="ATOZWEBGMS.Pages.A2ZGoldIn" Title="Gold In" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

        .grid_scroll2 {
            overflow: auto;
            height: 500px;
            width: 920px;
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




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div id="DivMain" runat="server">
        <div align="center">

            <table>
                <tr>
                    <td style="vertical-align: top;">
                        <table class="style1">
                            <thead>
                                <tr>
                                    <th colspan="4">Gold In
                                    </th>
                                </tr>

                            </thead>


                            <tr>

                                <td>
                                    <asp:Label ID="Label15" runat="server" Text="Carrier Name :" Font-Size="Large"
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
                                    <asp:Label ID="Label9" runat="server" Text="Address :" Font-Size="Large"
                                        ForeColor="Red"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPartyAddress" runat="server" ReadOnly="true" CssClass="cls text" Width="397px" BorderColor="#1293D1" BorderStyle="Ridge"
                                        Height="25px" Font-Size="Large" MaxLength="50"></asp:TextBox>
                                </td>

                            </tr>


                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Carring Rate :" Font-Size="Large"
                                        ForeColor="Red"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCarringRate" runat="server" CssClass="cls text" Width="100px" BorderColor="#1293D1" BorderStyle="Ridge"
                                        Height="25px" Font-Size="Large" Style="text-align: Left" MaxLength="50" onFocus="javascript:this.select();" onkeydown="return (event.keyCode !=13);"></asp:TextBox>

                                    &nbsp;<asp:Label ID="Label5" runat="server" Text="(Per Gram)" Font-Size="Large"
                                        ForeColor="Red"></asp:Label>

                                </td>

                            </tr>


                            <tr>
                                <td>
                                    <asp:Label ID="Label16" runat="server" Text="From Location :" Font-Size="Large"
                                        ForeColor="Red"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFromLocation" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                                        Width="150px" Font-Size="Large" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlFromLocation_SelectedIndexChanged">
                                        <asp:ListItem Value="2">Dubai (DXB)</asp:ListItem>
                                        <asp:ListItem Value="1">Dhaka (DHK)</asp:ListItem>

                                    </asp:DropDownList>


                                    &nbsp;&nbsp;&nbsp;&nbsp;

                      
                                <asp:Label ID="Label4" runat="server" Text="To Location :" Font-Size="Large"
                                    ForeColor="Red"></asp:Label>

                                    <asp:DropDownList ID="ddlToLocation" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                                        Width="150px" Font-Size="Large" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlToLocation_SelectedIndexChanged">
                                        <asp:ListItem Value="1">Dhaka (DHK)</asp:ListItem>
                                        <asp:ListItem Value="2">Dubai (DXB)</asp:ListItem>
                                    </asp:DropDownList>

                                </td>

                            </tr>


                        </table>

                    </td>

                </tr>
            </table>

        </div>

        <br />

        <div align="center">
            <asp:Panel ID="pnlProperty" runat="server" Height="320px">
                <table class="style1">
                    <tr>

                        <td>
                            <h6>
                                <asp:Label ID="Label14" runat="server" Text="Code" ForeColor="Red"></asp:Label></h6>
                            <asp:DropDownList ID="ddlCode" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Width="90px" Font-Size="Small" AutoPostBack="true" OnSelectedIndexChanged="ddlCode_SelectedIndexChanged">
                            </asp:DropDownList>

                        </td>
                        <td>
                            <h6>
                                <asp:Label ID="Label10" runat="server" Text="karat" ForeColor="Red"></asp:Label></h6>
                            <asp:DropDownList ID="ddlKarat" runat="server" Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                                Width="70px" Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="ddlKarat_SelectedIndexChanged">
                                <asp:ListItem Value="0">-Sel-</asp:ListItem>
                                <asp:ListItem Value="22">22</asp:ListItem>
                                <asp:ListItem Value="21">21</asp:ListItem>
                                <asp:ListItem Value="18">18</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <h6>
                                <asp:Label ID="Label11" runat="server" Text="Item Name" ForeColor="Red"></asp:Label></h6>
                            <asp:TextBox ID="txtItemName" runat="server" Width="200px" Height="25px" Font-Size="Small"
                                CssClass="cls text" AutoPostBack="true" OnTextChanged="txtItemName_TextChanged" BorderColor="#1293D1"></asp:TextBox>
                        </td>
                        <td>
                            <h6>
                                <asp:Label ID="Label12" runat="server" Text="Purity" ForeColor="Red"></asp:Label></h6>
                            <asp:TextBox ID="txtPurity" runat="server" Width="60px" Height="25px" Style="text-align: Right" Font-Size="Small" onkeypress="return IsDecimalKey(event)"
                                CssClass="cls text" ReadOnly="true" BorderColor="#1293D1"></asp:TextBox>
                        </td>
                        <td>
                            <h6>
                                <asp:Label ID="Label6" runat="server" Text="Gross Wt" ForeColor="Red"></asp:Label></h6>
                            <asp:TextBox ID="txtGrossWt" runat="server" Width="70px" Height="25px" Style="text-align: Right" Font-Size="Small" onkeypress="return IsDecimalKey(event)"
                                CssClass="cls text" AutoPostBack="True" OnTextChanged="txtGrossWt_TextChanged" BorderColor="#1293D1"></asp:TextBox>
                        </td>
                        <td>
                            <h6>
                                <asp:Label ID="Label13" runat="server" Text="Stone Wt" ForeColor="Red"></asp:Label></h6>
                            <asp:TextBox ID="txtStoneWt" runat="server" Width="70px" Height="25px" Font-Size="Small" Style="text-align: Right" onkeypress="return IsDecimalKey(event)"
                                CssClass="cls text" AutoPostBack="True" OnTextChanged="txtStoneWt_TextChanged" BorderColor="#1293D1"></asp:TextBox>
                        </td>
                        <td>
                            <h6>
                                <asp:Label ID="Label1" runat="server" Text="Net Wt" ForeColor="Red"></asp:Label></h6>
                            <asp:TextBox ID="txtNetWt" runat="server" Width="70px" Height="25px" Font-Size="Small" Style="text-align: Right" onkeypress="return IsDecimalKey(event)"
                                CssClass="cls text" ReadOnly="True" BorderColor="#1293D1"></asp:TextBox>
                        </td>
                        <td>
                            <h6>
                                <asp:Label ID="Label2" runat="server" Text="Pure Wt" ForeColor="Red"></asp:Label></h6>
                            <asp:TextBox ID="txtPureWt" runat="server" Width="70px" Height="25px" Font-Size="Small" Style="text-align: Right" onkeypress="return IsDecimalKey(event)"
                                CssClass="cls text" ReadOnly="True" BorderColor="#1293D1"></asp:TextBox>
                        </td>

                        <td>
                            <h6>
                                <asp:Label ID="Label19" runat="server" Text="Making Rate" ForeColor="Red"></asp:Label></h6>
                            <asp:TextBox ID="txtMakingRate" runat="server" ReadOnly="true" Width="90px" Height="25px" Font-Size="Small" Style="text-align: Right" onkeypress="return IsDecimalKey(event)"
                                CssClass="cls text" AutoPostBack="True" OnTextChanged="txtMakingRate_TextChanged" BorderColor="#1293D1"></asp:TextBox>
                        </td>
                        <td>
                            <h6>
                                <asp:Label ID="Label20" runat="server" Text="Stone Making" ForeColor="Red"></asp:Label></h6>
                            <asp:TextBox ID="txtStoneMakingRate" runat="server" ReadOnly="true" Width="90px" Height="25px" Font-Size="Small" Style="text-align: Right" onkeypress="return IsDecimalKey(event)"
                                CssClass="cls text" AutoPostBack="True" OnTextChanged="txtStoneMaking_TextChanged" BorderColor="#1293D1"></asp:TextBox>
                        </td>
                        <td>
                            <h6>
                                <asp:Label ID="Label21" runat="server" Text="Making Value" ForeColor="Red"></asp:Label></h6>
                            <asp:TextBox ID="txtMakingValue" runat="server" Width="100px" Height="25px" Font-Size="Small" Style="text-align: Right" onkeypress="return IsDecimalKey(event)"
                                CssClass="cls text" ReadOnly="True" BorderColor="#1293D1"></asp:TextBox>
                        </td>
                        <td>
                            <h6>
                                <asp:Label ID="Label22" runat="server" Text="Stone Value" ForeColor="Red"></asp:Label></h6>
                            <asp:TextBox ID="txtStoneMakingValue" runat="server" Width="100px" Height="25px" Font-Size="Small" Style="text-align: Right" onkeypress="return IsDecimalKey(event)"
                                CssClass="cls text" ReadOnly="True" BorderColor="#1293D1"></asp:TextBox>
                        </td>

                        <td>
                            <h6>
                                <asp:Label ID="Label24" runat="server" Text="Total Value" ForeColor="Red"></asp:Label></h6>
                            <asp:TextBox ID="txtTotalValue" runat="server" Width="100px" Height="25px" Font-Size="Small" Style="text-align: Right" onkeypress="return IsDecimalKey(event)"
                                CssClass="cls text" ReadOnly="True" BorderColor="#1293D1"></asp:TextBox>
                        </td>

                        <td>
                            <h6>
                                <asp:Label ID="Label7" runat="server" Text="Total Carring" ForeColor="Red"></asp:Label></h6>
                            <asp:TextBox ID="txtTotalCarring" runat="server" Width="100px" Height="25px" Font-Size="Small" Style="text-align: Right" onkeypress="return IsDecimalKey(event)"
                                CssClass="cls text" ReadOnly="True" BorderColor="#1293D1"></asp:TextBox>
                        </td>

                        <td>
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="BtnAddItem" runat="server" Text="Add" Width="80px" Font-Size="Large"
                                ForeColor="White" Height="27px" Font-Bold="True" AccessKey="A"
                                CssClass="button green" OnClick="BtnAddItem_Click" />
                        </td>
                    </tr>
                </table>

                <div align="center" class="grid_scroll">
                    <asp:GridView ID="gvItemDetails" runat="server" HeaderStyle-BackColor="YellowGreen" Width="1410px"
                        AutoGenerateColumns="False" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" EnableModelValidation="True" OnRowDeleting="gvItemDetails_RowDeleting" OnRowDataBound="gvItemDetails_RowDataBound">
                        <HeaderStyle BackColor="YellowGreen" />
                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                        <AlternatingRowStyle BackColor="WhiteSmoke" />
                        <Columns>
                            <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ItemGroupName" HeaderText="Code">
                                <HeaderStyle Width="90px" />
                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Karat" HeaderText="Karat">
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                <HeaderStyle Width="230px" />
                                <ItemStyle HorizontalAlign="Left" Width="230px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Purity" HeaderText="Purity">
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GrossWt" HeaderText="GrossWt">
                                <HeaderStyle Width="90px" />
                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="StoneWt" HeaderText="StoneWt">
                                <HeaderStyle Width="90px" />
                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NetWt" HeaderText="NetWt">
                                <HeaderStyle Width="90px" />
                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PureWt" HeaderText="PureWt">
                                <HeaderStyle Width="90px" />
                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MakingRate" HeaderText="Making Rate" DataFormatString="{0:0,0.00}">
                                <HeaderStyle Width="90px" />
                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="StoneMakingRate" HeaderText="Stone Making" DataFormatString="{0:0,0.00}">
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MakingValue" HeaderText="Making Value" DataFormatString="{0:0,0.00}">
                                <HeaderStyle Width="140px" />
                                <ItemStyle HorizontalAlign="Right" Width="140px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="StoneMakingValue" HeaderText="Stone Value" DataFormatString="{0:0,0.00}">
                                <HeaderStyle Width="140px" />
                                <ItemStyle HorizontalAlign="Right" Width="140px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalMetalValue" Visible="false" HeaderText="Total Metal Value">
                                <HeaderStyle Width="140px" />
                                <ItemStyle HorizontalAlign="Right" Width="140px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalValue" HeaderText="Total Value" DataFormatString="{0:0,0.00}">
                                <HeaderStyle Width="140px" />
                                <ItemStyle HorizontalAlign="Right" Width="140px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CarringValue" HeaderText="Carring Value" DataFormatString="{0:0,0.00}">
                                <HeaderStyle Width="140px" />
                                <ItemStyle HorizontalAlign="Right" Width="140px" />
                            </asp:BoundField>
                            <asp:CommandField ShowDeleteButton="True" HeaderStyle-Width="70px" ItemStyle-Width="70px">
                                <ControlStyle Font-Bold="True" ForeColor="#FF3300" />
                            </asp:CommandField>
                        </Columns>

                    </asp:GridView>
                </div>

            </asp:Panel>

            <div align="center">
                <table class="style1">
                    <tr>
                        <td>
                            <asp:Label ID="Label17" runat="server" Text="Total:" Width="100px" Height="25px" Font-Size="Medium" Font-Bold="true" ForeColor="Red"></asp:Label>
                            Gross Wt: 
                    <asp:Label ID="lblGrossWt" runat="server" Text="" Width="100px" Height="25px" Font-Size="Medium" Font-Bold="true" ForeColor="Blue"></asp:Label>
                            Stone Wt: 
                    <asp:Label ID="lblStoneWt" runat="server" Text="" Width="100px" Height="25px" Font-Size="Medium" Font-Bold="true" ForeColor="Blue"></asp:Label>
                            Net Wt: 
                    <asp:Label ID="lblNetWt" runat="server" Text="" Width="100px" Height="25px" Font-Size="Medium" Font-Bold="true" ForeColor="Blue"></asp:Label>
                            Pure Wt:
                    <asp:Label ID="lblPureWt" runat="server" Text="" Width="100px" Height="25px" Font-Size="Medium" Font-Bold="true" ForeColor="Red"></asp:Label>
                            Making:
                    <asp:Label ID="lblMaking" runat="server" Text="" Width="100px" Height="25px" Font-Size="Medium" Font-Bold="true" ForeColor="Blue"></asp:Label>
                            Stone:
                    <asp:Label ID="lblStoneValue" runat="server" Text="" Width="100px" Height="25px" Font-Size="Medium" Font-Bold="true" ForeColor="Blue"></asp:Label>
                            <asp:Label ID="lblMetalValue" runat="server" Width="100px" Height="25px" Visible="false" Text="" Font-Bold="true" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                            Total:
                    <asp:Label ID="lblTotalValue" runat="server" Text="" Width="100px" Height="25px" Font-Size="Medium" Font-Bold="true" ForeColor="Red"></asp:Label>
                            Carring:
                    <asp:Label ID="lblTotalCarring" runat="server" Text="" Width="110px" Height="25px" Font-Size="Medium" Font-Bold="true" ForeColor="Red"></asp:Label>

                        </td>

                    </tr>

                    <tr>

                        <td class="auto-style1" style="text-align: center">
                            <asp:Button ID="BtnSubmit" runat="server" Text="Update" Font-Size="Large" ForeColor="White"
                                Font-Bold="True" ToolTip="Insert Information" CssClass="button green" AccessKey="S"
                                OnClientClick="return ValidationBeforeSave()" OnClick="BtnSubmit_Click" Height="27px" />&nbsp;
                    &nbsp;
                   <asp:Button ID="btnCancel" runat="server" Text="Cancel" Font-Bold="True" Font-Size="Large"
                       ForeColor="White" CssClass="button blue" AccessKey="R"
                       OnClick="btnCancel_Click" Height="27px" />&nbsp;
                        <asp:Button ID="btnView" runat="server" Text="View Stock" Font-Size="Large" ForeColor="White"
                            Font-Bold="True" ToolTip="Insert Information" CssClass="button" AccessKey="Z"
                            OnClick="btnView_Click" Height="27px" />&nbsp;

                     <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                         Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" AccessKey="W" CausesValidation="False"
                         CssClass="button red" OnClick="BtnExit_Click" />
                            <br />
                        </td>
                    </tr>
                </table>
            </div>

        </div>
    </div>

    <div id="divPost" runat="server" align="center">
        <br />
        <table class="style1" style="width: 700px; height: 470px; background-color: #e9e9e9;">
            <tr>
                <td style="vertical-align: top;">
                           <table class="style1" style="width: 340px; height: 380px;">
                        <thead>
                            <tr>
                                <th colspan="4">
                                    <asp:Label ID="Label25" runat="server" Text="Gold In - Details" Font-Size="Large"
                                        ForeColor="Black"></asp:Label>
                                </th>
                            </tr>

                        </thead>


                        <tr>

                            <td>
                                <asp:Label ID="Label26" runat="server" Text="Total Gross:" Font-Size="Large"
                                    ForeColor="#004600"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTotalGross" runat="server" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" Style="text-align: Right" ReadOnly="True"></asp:TextBox>


                            </td>

                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label27" runat="server" Text="Total Net:" Font-Size="Large"
                                    ForeColor="#004600"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTotalNet" runat="server" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" Style="text-align: Right" ReadOnly="True"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label28" runat="server" Text="Metal:" Font-Size="Large"
                                    ForeColor="#004600"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMetal" runat="server" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" Style="text-align: Right" ReadOnly="True"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label29" runat="server" Text="Making:" Font-Size="Large"
                                    ForeColor="#004600"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMaking" runat="server" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" Style="text-align: Right" ReadOnly="True"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label30" runat="server" Text="Stone:" Font-Size="Large"
                                    ForeColor="#004600"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStone" runat="server" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" Style="text-align: Right" ReadOnly="True"></asp:TextBox>
                            </td>

                        </tr>
                     
                        <tr>
                            <td>
                                <asp:Label ID="Label32" runat="server" Text="Total Value:" Font-Size="Large"
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
                     <table class="style1" style="width: 340px; height: 380px;">
                        <thead>
                            <tr>
                                <th colspan="4">
                                    <asp:Label ID="Label41" runat="server" Text="Carring Value Modifincation" Font-Size="Large"
                                        ForeColor="Black"></asp:Label>
                                </th>
                            </tr>

                        </thead>
                        <tr>
                            <td>
                                <asp:Label ID="Label36" runat="server" Text="Carring Rate:" Font-Size="Large"
                                    ForeColor="#004600"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtConvCarringRate" runat="server" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" Style="text-align: Right" ReadOnly="True"></asp:TextBox>
                            </td>

                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Total Carring Value:" Font-Size="Large"
                                    ForeColor="#004600"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtConvCarringValue" runat="server" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" Style="text-align: Right" ReadOnly="True"></asp:TextBox>
                            </td>

                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label18" runat="server" Text="Input Carring Value:" Font-Size="Large"
                                    ForeColor="#004600"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtInputConvCarringValue" runat="server" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge" onkeypress="return IsDecimalKey(event)"
                                    Font-Size="Large" Style="text-align: Right" AutoPostBack="True" OnTextChanged="txtInputConvCarringValue_TextChanged"></asp:TextBox>
                            </td>

                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label23" runat="server" Text="Amount Adjustment:" Font-Size="Large"
                                    ForeColor="#004600"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtConvAdjAmount" runat="server" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" Style="text-align: Right" ReadOnly="True"></asp:TextBox>
                            </td>

                        </tr>



                        </table>
                </td>
            </tr>
            <tr>

                <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;


                                <asp:Button ID="btnPost" runat="server" Text="Post" Font-Bold="True" Font-Size="Large" OnClientClick="return ValidationBeforeSave()"
                                    ForeColor="White" CssClass="button green" Height="30px" OnClick="btnPost_Click" Width="110px" />

                    &nbsp; &nbsp; &nbsp;

                                <asp:Button ID="btnConvBack" runat="server" Text="Back" Font-Bold="True" Font-Size="Large"
                                    ForeColor="White" CssClass="button red" Height="30px" OnClick="btnConvBack_Click" Width="110px" />

                </td>
            </tr>
        </table>



    </div>
    <div runat="server" align="center" id="DivStockView">
        <table class="style1" style="width: 920px; background-color: #e9e9e9;">
            <thead>
                <tr>
                    <th colspan="4">
                        <asp:Label ID="Label42" runat="server" Text="Stock Details - " Font-Size="Large"
                            ForeColor="Black"></asp:Label>
                        <asp:Label ID="lblStockLocation" runat="server" Text="" Font-Size="Large"
                            ForeColor="Black"></asp:Label>

                    </th>
                </tr>

            </thead>
        </table>
        <div class="grid_scroll2">


            <table class="style1" style="width: 900px; height: 500px; background-color: #e9e9e9;">


                <tr>
                    <td style="vertical-align: top">
                        <asp:GridView ID="gvItemStockDetails" runat="server" HeaderStyle-BackColor="YellowGreen" Width="870px"
                            AutoGenerateColumns="False" AlternatingRowStyle-BackColor="SkyBlue" RowStyle-Height="10px" EnableModelValidation="True" BackColor="#009999">
                            <HeaderStyle BackColor="Orange" />
                            <RowStyle BackColor="#FFFFCC" ForeColor="#8C4510" />
                            <AlternatingRowStyle BackColor="WhiteSmoke" />
                            <Columns>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ItemGroupName" HeaderText="Code">
                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Karat" HeaderText="Karat">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Purity" Visible="false" HeaderText="Purity">
                                    <HeaderStyle Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="GrossWt" HeaderText="Gross Wt" DataFormatString="{0:0,0.00}">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="StoneWt" HeaderText="Stone Wt" DataFormatString="{0:0,0.00}">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NetWt" HeaderText="Net Wt" DataFormatString="{0:0,0.00}">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MakingRate" HeaderText="Making Rate" DataFormatString="{0:0,0.00}">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="StoneMakingRate" HeaderText="Stone Making" DataFormatString="{0:0,0.00}">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:BoundField>

                            </Columns>

                        </asp:GridView>

                    </td>
                </tr>


            </table>
        </div>

        <table class="style1" style="width: 920px; background-color: #e9e9e9;">
            <tr>
                <td style="text-align: center">

                    <asp:Button ID="btnBack" runat="server" Text="Back" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="30px" Width="130px" Font-Bold="True" AccessKey="Q" ToolTip="Back Page" CausesValidation="False"
                        CssClass="button red" OnClick="btnBack_Click" />

                </td>
            </tr>
        </table>
    </div>



    <asp:Label ID="lblPartyAccType" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblPartyAccNo" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblProcesDate" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="CtrlVoucherNo" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblDescription" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblStockAccType" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblStockAccno" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblBalGrossWt" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblBalStoneWt" runat="server" Visible="False"></asp:Label>

    <asp:Label ID="lblMsgFlag" runat="server" Visible="False"></asp:Label>

</asp:Content>
