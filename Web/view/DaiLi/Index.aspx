﻿<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SanZi.Web.daili.Index" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>代理申请管理</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
     <table style="width:100%;text-align:left;margin-top:5px;background:#000800;" cellspacing="1" cellpadding="3" >
        <tr style="background:#ffffff;">
            <td colspan="2" class="tableTitle">代理申请管理</td>
        </tr>
        <tr style="background:#ffffff;">
            <td class="tableContent">
                <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="查询" width="100px" onclick="btnSearch_Click"/>
            </td>
        </tr>
        <tr style="background:#ffffff;">
            <td class="tableContent">
                <asp:Repeater ID="rpFileManage" runat="server">
                    <HeaderTemplate>
                    <table width="100%" border="1" cellspacing="0" cellpadding="4" style="border-collapse:collapse;background:#E3EFFF;">
                    <tr><th style="width:15%">申请村</th><th style="width:25%">项目名称</th><th style="width:15%">估算价</th><th style="width:15%">项目类型</th><th style="width:10%">申请日期</th><th style="width:20%">操作</th></tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <tr style="background-color:#FAF3DC">
                    <td><%#DataBinder.Eval(Container.DataItem, "VillageName")%></td>
                    <td><a href="Show.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DaiLi_ID")%>" target="_blank"><%#DataBinder.Eval(Container.DataItem, "ProjectName")%></a></td>
                    <td><%#DataBinder.Eval(Container.DataItem, "EstimateValue")%>元</td>
                    <td><%#DataBinder.Eval(Container.DataItem, "ProjectType")%></td>
                    <td><%#DataBinder.Eval(Container.DataItem, "ApplyDate")%></td>
                    <td>
                        <a href="Show.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DaiLi_ID")%>" target="_blank">查看详情</a>&nbsp;&nbsp;
                        <a href="Edit.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DaiLi_ID")%>">编辑</a>&nbsp;&nbsp;
                        <a href="index.aspx?act=del&id=<%#DataBinder.Eval(Container.DataItem, "DaiLi_ID")%>" onclick="return confirm('确定要删除此代理申请吗？');">删除</a>
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