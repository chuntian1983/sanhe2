<%@ Page Language="C#" AutoEventWireup="true" Inherits="Contract_DueNoticeLease" Codebehind="DueNoticeLease.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>通知单打印</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function resetDialogSize()
{
    var ua = navigator.userAgent;
    if(ua.lastIndexOf("MSIE 7.0") == -1)
    {
        var height = document.body.offsetHeight;
        var width = document.body.offsetWidth;
        if(ua.lastIndexOf("Windows NT 5.2") == -1)
        {
            window.dialogHeight=(height+53)+"px";
            window.dialogWidth=(width+6)+"px";
        }
        else
        {
            window.dialogHeight=(height+46)+"px";
            window.dialogWidth=(width+6)+"px";
        }
    }
}
window.onload = resetDialogSize;
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center;">
    <div style="margin: 5px; border:1px dotted gray; width: 632px; height: 330px;">
        <table cellpadding="0" cellspacing="0" style="margin:5px; font-size:12pt; width: 615px; height: 315px;">
            <tr>
                <td style="font-size:15pt; text-align:center; height:45px">
                    租赁到期通知书（存根）
                </td>
            </tr>
            <tr>
                <td style="text-align:left; height:25px">
                    <asp:Label ID="AName" runat="server"></asp:Label>村委：
                </td>
            </tr>
            <tr>
                <td style="text-align:left; vertical-align:top; line-height:35px; height:auto;">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;您村居民
                    <asp:Label ID="LeaseHolder" runat="server" Width="154px" CssClass="textCenter"></asp:Label>
                    租赁的<asp:Label ID="ResourceName" runat="server" Width="301px" CssClass="textCenter"></asp:Label>
                    将于<asp:Label ID="EndLease" runat="server" Width="145px" CssClass="textCenter"></asp:Label>
                    到期，请在接到通知后，做好下期租赁的招投标准备工作，并报三资代理服务中心备案。
                </td>
            </tr>
            <tr>
                <td style="text-align:right; height:25px">
                    三资代理服务中心
                </td>
            </tr>
            <tr>
                <td style="text-align:right; height:25px">
                    <asp:Label ID="PrintDate" runat="server" Width="150px"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <!--NoPrintStart-->
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 632px; margin-top: 15px">
            <tr>
                <td style="background-position: center center; background-repeat: no-repeat; height: 32px; text-align: center">
                    <input id="Button1" type="button" value="打印通知单" onclick="window.open('../PrintWeb.html','','');" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" value="关闭窗口" onclick="window.close();" />
                </td>
            </tr>
        </table>
    </div>
    <!--NoPrintEnd-->
    </div>
    </form>
</body>
</html>
