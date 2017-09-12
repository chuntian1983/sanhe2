<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountManage_LookVoucher" Codebehind="LookVoucher.aspx.cs" %>

<%@ Register Src="../AccountQuery/ShowVoucher.ascx" TagName="ShowVoucher" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>查看凭证</title>
<base target="_self" />
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function ShowAddons(v)
{
    window.showModalDialog("AppendixShow.aspx?id="+v+"&g="+(new Date()).getTime(),"","dialogWidth=720px;dialogHeight=508px;center=yes;");
}
function ShowEntryData(v,s,r,m)
{
    window.showModalDialog("AddEntryData.aspx?id="+v+"&no="+s+"&row="+r+"&money="+m+"&g="+(new Date()).getTime(),"","dialogWidth=600px;dialogHeight=452px;center=yes;");
}
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
function ShowPiBanKa() {
    var v = '<%=CommClass.GetTableValue("pibanka", "pid", "subid='" + Request.QueryString["id"] + "'", "") %>';
    if (v.length == 0) {
        alert("未查询到对应的批办卡！");
    }
    else {
        window.showModalDialog("../view/PiBanKa/show.aspx?pid=" + v + "&g=" + (new Date()).getTime(), "", "dialogWidth=620px;dialogHeight=508px;center=yes;");
    }
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="height: 350px; text-align: center; vertical-align:top">
                    <!--NoPrintStart-->
                    <div style="width: 100%; height: 100%; overflow-y:scroll;">
                    <!--NoPrintEnd-->
                        <uc1:ShowVoucher ID="ShowVoucher" runat="server" />
                    <!--NoPrintStart-->
                    </div>
                    <!--NoPrintEnd-->
                </td>
            </tr>
            <!--NoPrintStart-->
            <tr>
                <td class="t4" style="height: 32px; text-align: center">
                    <asp:Button ID="Button5" runat="server" Text="- 审核 -" OnClick="Button5_Click" Visible="false" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button3" runat="server" Text="复制当前凭证" OnClick="Button3_Click" Enabled="False" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button4" runat="server" Text="冲红当前凭证" OnClick="Button4_Click" Enabled="False" CommandArgument="DoReverse" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button6" type="button" value="查看批办卡" onclick="ShowPiBanKa();" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button1" type="button" value="打印凭证" onclick="window.open('../PrintWeb.html','','');" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" value="关闭窗口" onclick="window.close();" />
                </td>
            </tr>
            <!--NoPrintEnd-->
        </table>
    </div>
    </form>
</body>
</html>
