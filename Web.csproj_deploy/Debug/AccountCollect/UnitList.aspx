<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountCollect_UnitList" Codebehind="UnitList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>单位选择</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<base target="_self" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function onClose()
{
    window.returnValue=new Array($("UnitName").value,$("GAccountList").value);
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
</script>
</head>
<body onunload="onClose()">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 250px">
            <tr>
                <td class="t2" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">单位选择</span>&nbsp;</td>
            </tr>
            <tr>
                <td class="t2" style="height: 320px; text-align: left;">
                <div style="WIDTH: 100%; HEIGHT: 100%; overflow-y:scroll;">
                    <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="1" ImageSet="XPFileExplorer" NodeIndent="15" ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                        <SelectedNodeStyle BackColor="#F6F6F6" Font-Underline="False" HorizontalPadding="0px"
                            VerticalPadding="0px" BorderStyle="Solid" BorderWidth="1px" />
                        <Nodes>
                            <asp:TreeNode SelectAction="None" Text="一级科目" Value="000"></asp:TreeNode>
                        </Nodes>
                        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
                    </asp:TreeView>
                 </div>
                </td>
            </tr>
            <tr>
                <td class="t4" style="height: 20px; text-align: center">
                    <input id="Button1" type="button" value="确定并关闭" onclick="window.close();" /></td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="UnitID" runat="server" />
    <asp:HiddenField ID="UnitName" runat="server" />
    <asp:HiddenField ID="GAccountList" runat="server" />
    <asp:HiddenField ID="TotalLevel" runat="server" Value="0" />
    </form>
</body>
</html>
