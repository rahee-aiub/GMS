<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShortcutKeys.aspx.cs" Inherits="ATOZWEBGOLD.Pages.ShortcutKeys" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scrpt/jquery-1.4.1-vsdoc.js"></script>
    <script src="../Scrpt/jquery-1.4.1.js"></script>
    <script src="../Scrpt/jquery-1.4.1.min.js"></script>

    <script type="text/javascript">
        $(function () {
            $(document).keyup(function (e) {
                var key = (e.keyCode ? e.keyCode : e.charCode);
                switch (key) {
                    case 120:
                        navigateUrl($('a[id$=lnk1]'));
                        break;
                    case 113:
                        navigateUrl($('a[id$=lnk2]'));
                        break;
                    case 114:
                        navigateUrl($('a[id$=lnk3]'));
                        break;
                    default:;
                }
            });
            function navigateUrl(jObj) {
                window.location.href = $(jObj).attr("href");
            }
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>

            <p>
                Press 
        <b>F1 for My Blog Page</b>
                <br />
                <b>F2 for My ASP.NET Posts Page</b>
                <br />
                <b>F3 for About Me Page</b>
            </p>
            <div>
                <asp:LinkButton ID="lnk1" runat="server" OnClick="lnk1_Click" ></asp:LinkButton>
               
                <br />
                <asp:HyperLink ID="lnk2" runat="server" >My ASP.NET Posts</asp:HyperLink>
                <br />
                <asp:HyperLink ID="lnk3" runat="server" >About Me</asp:HyperLink>
            </div>

        </div>
    </form>
</body>
</html>
