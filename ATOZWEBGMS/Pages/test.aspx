<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="ATOZWEBGMS.Pages.test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js"></script>


    <%-- <link " rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.0/jquery-confirm.min.css"/>
    <script  type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.0/jquery-confirm.min.js"></script>--%>

    <script type="text/javascript">
        $(function () {
            var textBox1 = $('input:text[id$=TextBox1]').keyup(foo);
            var textBox2 = $('input:text[id$=TextBox2]').keyup(foo);



            function foo() {
                var value1 = textBox1.val();
                var value2 = textBox2.val();
                var sum = (value1 * value2);
                $('input:text[id$=TextBox3]').val(sum);
            }



        });

        function drawChart() {
            debugger;
            var F = $('input:text[id$=TextBox1]').val();
            var T = $('input:text[id$=TextBox2]').val();

            if (F == T) {
                alert('ok');
                return true;
            }
            else {
                alert('not ok');
                return false;
            }


        }


    </script>




    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/blitzer/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
      
        $(function () {
          
            $("[id*=btnDelete]").removeAttr("onclick");
            $("#dialog").dialog({
                modal: true,
                autoOpen: false,
                title: "Confirmation",
                width: 350,
                height: 160,
                buttons: [
                {
                    id: "Yes",
                    text: "Yes",
                    click: function () {
                        $("[id*=btnDelete]").attr("rel", "delete");
                        $("[id*=btnDelete]").click();
                    }
                },
                {
                    id: "No",
                    text: "No",
                    click: function () {
                        $(this).dialog('close');
                    }
                }
                ]
            });
            $("[id*=btnDelete]").click(function () {
                if ($(this).attr("rel") != "delete") {
                    $('#dialog').dialog('open');
                    return false;
                } else {
                    __doPostBack(this.name, '');
                }
            });
        });

    </script>

  
   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div>

        <table>
            <tr>
                <td>AED</td>
                <td>
                    <asp:TextBox runat="server" ID="TextBox1" Style="text-align: right" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" onkeypress="return IsDecimalKey(event)"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Rate</td>
                <td>
                    <asp:TextBox runat="server" ID="TextBox2" Style="text-align: right" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>BDT</td>
                <td>
                    <asp:TextBox runat="server" ID="TextBox3" Style="text-align: right" CssClass="cls text" Width="150px" Height="27px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="hey" />
                </td>
            </tr>
        </table>
    </div>

    <asp:Button ID="btnDelete" runat="server" Text="Delete"  UseSubmitBehavior="false" OnClick="btnDelete_Click" />
    <div id="dialog" style="display: none" align="center">
        Do you want to delete this record?
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
