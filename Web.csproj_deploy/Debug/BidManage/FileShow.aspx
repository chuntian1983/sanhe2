<%@ Page Language="C#" AutoEventWireup="true" Inherits="BidManage_FileShow" Codebehind="FileShow.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>查看文件</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<style type="text/css">
.mshow{height:25px;text-align:center;cursor:hand}
</style>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function OpenSelfWin(v)
{
    $("QSubjectType").value=v;
    __doPostBack('DoPostBack', '');
}
</script>
</head>
<body style="margin:0px;text-align:center">
    <form id="form1" runat="server">
    <asp:HiddenField ID="OSubjectType" runat="server" Value="0" />
    <asp:HiddenField ID="QSubjectType" runat="server" Value="0" />
    <table cellpadding="0" cellspacing="0" style="width:100%">
        <tr>
            <td style="height:35px;text-align:center;background-color:#f1fbf0">
                <span id="PageTitle" runat="server" style="font-size: 16pt; font-family: 隶书"></span>&nbsp;</td>
        </tr>
    </table>
    <table id="menus" runat="server" visible="false" cellpadding="0" cellspacing="0" style="width:100%">
        <tr>
            <td id="M0" class="t3 mshow" onclick="OpenSelfWin(0);" runat="server">村民代表会议</td>
            <td id="M1" class="t3 mshow" onclick="OpenSelfWin(1);" runat="server">招标项目申请</td>
            <td id="M2" class="t3 mshow" onclick="OpenSelfWin(2);" runat="server">招标项目审批</td>
            <td id="M3" class="t3 mshow" onclick="OpenSelfWin(3);" runat="server">预算书</td>
            <td id="M4" class="t3 mshow" onclick="OpenSelfWin(4);" runat="server">招标文件</td>
            <td id="M5" class="t3 mshow" onclick="OpenSelfWin(5);" runat="server">投标公告</td>
            <td id="M6" class="t3 mshow" onclick="OpenSelfWin(6);" runat="server">参投登记</td>
            <td id="M7" class="t3 mshow" onclick="OpenSelfWin(7);" runat="server">竞价招投标</td>
            <td id="M8" class="t3 mshow" onclick="OpenSelfWin(8);" runat="server">中标公告</td>
            <td id="M9" class="t3 mshow" onclick="OpenSelfWin(9);" runat="server">签订合同</td>
            <td id="M10" class="t4 mshow" onclick="OpenSelfWin(10);" runat="server">项目管理</td>
        </tr>
    </table>
    <asp:LinkButton ID="DoPostBack" runat="server" onclick="DoPostBack_Click"></asp:LinkButton>
    <br />
    <asp:DataList ID="AppendixList" Style="width:100%" runat="server" CellSpacing="0" CellPadding="0">
        <ItemTemplate>
            <img src='<%# Eval("FilePath") %>' alt="" />
        </ItemTemplate>
    </asp:DataList>
    </form>
</body>
</html>
