<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectManage.aspx.cs" Inherits="SanZi.Web.zhaobiao.ProjectManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>项目管理</title>
     <link href="../Style.css" type="text/css" rel="stylesheet"/>
</head>
<body>
<script type="text/javascript" language="javascript">
    function addUser() {
        //		var sFeatures = "dialogwidth=400px;dialogheight=200px;status=no;help=no;scroll=no;center=Yes;status=no;";				
        //	    var url = 'AddUser.aspx';	
        //		var result = window.showModalDialog( encodeURI( url) , null, sFeatures );
        window.location.href("AddProject.aspx");
    }
    function jump(u, d) {
        if (d.length == 0) {
            alert("没有查询到相关文档！");
        }
        else {
            window.open(u + d);
        }
    }
</script>
    <form id="form1" runat="server">
     <table style="width:750px;text-align:left;margin-top:5px;background:#000800;" cellspacing="1" cellpadding="3" >
        <tr style="background:#ffffff;">
            <td  class="tableTitle">项目管理</td>
        </tr>
        <tr style="background:#ffffff;">
            <td class="tableContent">
                <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="查询" width="100px" onclick="btnSearch_Click"/>
            </td>
        </tr>
        <tr style="background:#ffffff;">
            <td class="tableContent">
                <asp:Repeater ID="Repeater1" runat="server">
                    <HeaderTemplate>
                    <table width="750px" border="1" cellspacing="0" cellpadding="4" style="border-collapse:collapse;background:#E3EFFF;">
                    <tr><th style="width:40%">项目名称</th><th style="width:60%">操作</th></tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <tr style="background-color:#FAF3DC;">
                    <td><a href="EditProject.aspx?id=<%#DataBinder.Eval(Container.DataItem, "ID")%>"><%#DataBinder.Eval(Container.DataItem, "xmmc")%></a></td>
                    <td style="text-align:center">
                    <a href="###" onclick="jump('../DaiLi/show.aspx?id=','<%#GetModelID(Eval("ID").ToString(),"0")%>')">代理申请</a>
                    <a href="###" onclick="jump('cyssEdit.aspx?id=','<%#GetModelID(Eval("ID").ToString(),"1")%>')">预算书</a>
                    <a href="###" onclick="jump('zbggEdit.aspx?id=','<%#GetModelID(Eval("ID").ToString(),"2")%>')">招标公告</a>
                    <a href="###" onclick="jump('ctdjbview.aspx?id=','<%#GetModelID(Eval("ID").ToString(),"3")%>')">参投登记</a>
                    <a href="###" onclick="jump('jjztbEdit.aspx?id=','<%#GetModelID(Eval("ID").ToString(),"4")%>')">竞价招投标</a>
                    <a href="###" onclick="jump('zhongbEdit.aspx?id=','<%#GetModelID(Eval("ID").ToString(),"5")%>')">中标公告</a>
                    <a href="ProjectManage.aspx?act=del&id=<%#DataBinder.Eval(Container.DataItem, "ID")%>" onclick="return confirm('确定要删除此项目名称吗？');">删除</a>
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
    </form>
</body>
</html>
