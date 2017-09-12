<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SanZi.Web.Users.index" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>权利人管理</title>
     <link href="../Style.css" type="text/css" rel="stylesheet"/>
<style type="text/css">
.Lightbox{BORDER:#fff 1px solid;DISPLAY:block;Z-INDEX:9999;TEXT-ALIGN:center;POSITION:absolute;BACKGROUND-COLOR:#f6f6f6;COLOR:Blue; FONT-SIZE:18px;height:60px;width:500px}
.Overlay{DISPLAY:block;Z-INDEX:9998;FILTER:alpha(opacity=3);POSITION:absolute;BACKGROUND-COLOR:#000;moz-opacity:0.8;opacity:.80;}
</style>
</head>
<body id="HBody">
<script type="text/javascript" language="javascript">     
    function addUser()
    {
//		var sFeatures = "dialogwidth=400px;dialogheight=200px;status=no;help=no;scroll=no;center=Yes;status=no;";				
//	    var url = 'AddUser.aspx';	
//		var result = window.showModalDialog( encodeURI( url) , null, sFeatures );
        window.location.href("AddUser.aspx");
    }
    function $(o) { return (typeof (o) == "object") ? o : document.getElementById(o); }
    function ShowZipInfo(dir) {
        ShowProsess("<br /><a href='" + dir + "' target=_blank>下载</a>&nbsp;&nbsp;<a href='###' onclick='CancelProsess()'>取消</a>");
        return false;
    }
    function CancelProsess() {
        $("Overlay").style.display = "none";
        $("Lightbox").style.display = "none";
    }
    function ShowProsess(msg) {
        $("Lightbox").innerHTML = msg;
        $("Overlay").style.display = "";
        $("Lightbox").style.display = "";
        LimitControl();
    }
    function LimitControl() {
        var aTag = $("HBody");
        var leftpos = aTag.offsetLeft;
        var toppos = aTag.offsetTop;
        var objHeight = aTag.offsetHeight;
        var objWidth = aTag.offsetWidth;
        $("Overlay").style.left = leftpos;
        $("Overlay").style.top = toppos;
        $("Overlay").style.height = objHeight;
        $("Overlay").style.width = "750px";
        $("Lightbox").style.left = leftpos + (objWidth - $("Lightbox").offsetWidth) / 2;
        $("Lightbox").style.top = toppos + (objHeight - $("Lightbox").offsetHeight) / 2;
    }                    
</script>
    <form id="form1" runat="server">
     <table style="width:600px;text-align:left;margin-top:5px;background:#000800;" cellspacing="1" cellpadding="3" >
        <tr style="background:#ffffff;">
            <td class="tableTitle">权利人管理</td>
        </tr>
        <tr style="background:#ffffff;">
            <td class="tableContent">
                <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="查询" width="100px" onclick="btnSearch_Click"/>&nbsp;&nbsp;
                <input id="btnAddUser" type="button" value="添加" onclick="addUser()" style="width:100px;"/> &nbsp;&nbsp;
                <asp:Button ID="btnExportUser" runat="server" Text="导出" width="100px"  onclick="btnExportUser_Click"/>
            </td>
        </tr>
        <tr style="background:#ffffff;">
            <td class="tableContent">
                <asp:Repeater ID="Repeater1" runat="server">
                    <HeaderTemplate>
                    <table width="100%" border="1" cellspacing="0" cellpadding="4" style="border-collapse:collapse;background:#E3EFFF;">
                    <tr><th style="width:15%">单位名称</th><th style="width:10%">姓名</th><th style="width:10%">职务</th><th style="width:15%">用户编号</th><th style="width:15%">电话</th><th style="width:10%">操作</th></tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <tr style="background-color:#FAF3DC">
                    <td><%#DataBinder.Eval(Container.DataItem, "DeptName")%></td>
                    <td><%#DataBinder.Eval(Container.DataItem, "TrueName")%></td>
                    <td><%#DataBinder.Eval(Container.DataItem, "TitleName")%></td>
                    <td><%#DataBinder.Eval(Container.DataItem, "BarCode")%></td>
                    <td><%#DataBinder.Eval(Container.DataItem, "TelPhone")%></td>
                    <td style="text-align:center"><a href="EditUser.aspx?id=<%#DataBinder.Eval(Container.DataItem, "UserID")%>">修改</a>
                    <a href="index.aspx?act=del&id=<%#DataBinder.Eval(Container.DataItem, "UserID")%>" onclick="return confirm('确定要删除此权利人吗？');">删除</a>
                    </td>
                    </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                    </table>
                    </FooterTemplate>
                    </asp:Repeater>
                <webdiyer:aspnetpager id="AspNetPager1" runat="server" PageSize="10" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged" ShowCustomInfoSection="Left" CustomInfoSectionWidth="40%" ShowPageIndexBox="always" PageIndexBoxType="DropDownList"
                CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"></webdiyer:aspnetpager>
                    
                </asp:Repeater>
            </td>
        </tr>
     </table>
        <div id="Overlay" runat="server" class="Overlay"></div>
        <div id="Lightbox" runat="server" class="Lightbox"></div>
    </form>
</body>
</html>
