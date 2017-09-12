<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manage2.aspx.cs" Inherits="SanZi.Web.GongKai.Manage2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>村务公开管理</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"></head>
<body>
    <form id="form1" runat="server">
     <table style="width:100%;text-align:left;margin-top:5px;background:#000800;" cellspacing="1" cellpadding="3" >
        <tr style="background:#ffffff;">
            <td colspan="2" class="tableTitle">村务公开管理</td>
        </tr>
        <tr style="background:#ffffff;">
            <td class="tableContent">
                <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="查询" width="100px" onclick="btnSearch_Click"/>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <input id="Button1" type="button" value="添加" onclick="location.href='Add2.aspx?lbid=<%=Request.QueryString["lbid"] %>'" />
            </td>
        </tr>
        <tr style="background:#ffffff;">
            <td class="tableContent">
                <asp:Repeater ID="Repeater1" runat="server">
                    <HeaderTemplate>
                    <table width="100%" border="1" cellspacing="0" cellpadding="4" style="border-collapse:collapse;background:#E3EFFF;">
                    <tr><th style="width:20%">单位名称</th><th style="width:60%">标题</th><th style="width:20%">操作</th></tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <tr style="background-color:#FAF3DC">
                        <td><%#DataBinder.Eval(Container.DataItem, "DeptName")%></td>
                        <td><a href="Show.aspx?dt=1&p=2&id=<%#DataBinder.Eval(Container.DataItem, "ID")%>&lbid=<%#Eval("subuid") %>" title="查看村务公开详细信息"><%#DataBinder.Eval(Container.DataItem, "Title")%></a></td>
                        <td><a href="Edit2.aspx?id=<%#DataBinder.Eval(Container.DataItem, "ID")%>&lbid=<%#Eval("subuid") %>">修改</a>  <a href="Manage1.aspx?act=del&id=<%#DataBinder.Eval(Container.DataItem, "ID")%>" onclick="return confirm('确定要删除此村务公开表吗？');">删除</a> </td>
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
