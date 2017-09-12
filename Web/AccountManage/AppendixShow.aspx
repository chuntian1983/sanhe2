<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountManage_AppendixShow" Codebehind="AppendixShow.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>凭证附单</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<base target="_self" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
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
window.onload = function()
{
    resetDialogSize();
}
</script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 720px">
            <tr style="background-color:#f8f8f8">
                <td class="t1" style="text-align: center;">
                附单列表
                </td>
                <td class="t2" style="height: 25px; text-align: center;">
                附单查看
                </td>
            </tr>
            <tr>
                <td class="t3" style="width: 150px; height: 479px; text-align: center; vertical-align:top">
                    <div id="SelAppendices" style="width: 100%; height: 100%;overflow-y:scroll;">
                        <div style="height:5px" />
                        <asp:DataList ID="AppendixList" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Table" CellSpacing="0" CellPadding="0" OnItemDataBound="AppendixList_ItemDataBound">
                            <ItemTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" style="text-align:center; height: 100px; width:100px;">
                                  <tr>
                                    <td><a  href="javascript:void(0)" onclick="$('ImgShow').src='<%# Eval("ParaValue") %>'"><img alt="" src="<%# Eval("DefValue") %>" border="0" /></a></td>
                                  </tr>
                                </table>
                                <img alt="" src="<%# Eval("ParaValue") %>" style="display:none" />
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </td>
                <td class="t4" style="width: 570px; height: 479px; text-align: center; vertical-align:top">
                    <div style="width: 100%; height: 100%;overflow-y:scroll;">
                        <img id="ImgShow" alt="" src="../Images/NoAddons.jpg" onclick="window.open(this.src,'','')" style="border-width:0px; cursor:hand; margin:5px" />
                    </div>
                </td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>
