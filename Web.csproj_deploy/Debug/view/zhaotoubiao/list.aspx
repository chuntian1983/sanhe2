<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="SanZi.Web.view.zhaotoubiao.list" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
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
            <td  align=center>
                 <asp:Label ID="Label1" runat="server" Text=" "></asp:Label><asp:Label ID="lbtitle" runat="server" Text=" "></asp:Label></td>
        </tr>
        <tr style="background:#ffffff;">
            <td class="tableContent">
                <asp:Repeater ID="rpFileManage" runat="server" 
                    onitemcommand="rpFileManage_ItemCommand">
                    <HeaderTemplate>
                    <table width="100%" border="1" cellspacing="0" cellpadding="4" style="border-collapse:collapse;background:#E3EFFF;">
                    <tr><th style="width:25%">项目名称</th><th style="width:15%">上传文件名称</th><th style="width:20%">操作</th></tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <tr style="background-color:#FAF3DC">
                    
                    <td><a href='../../Show.aspx?id=<%#Eval("id")%>' target="_blank"><%#DataBinder.Eval(Container.DataItem, "zname")%></a></td>
                   
                    <td><%#Eval("xmname")%></td>
                    <td>
                        <a href="../../Show.aspx?id=<%#DataBinder.Eval(Container.DataItem, "id")%>" target="_blank">查看详情</a>&nbsp;&nbsp;
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="del" OnClientClick="return confirm('确定要删除此代理申请吗？');" CommandArgument='<%#Eval("id") %>'>删除</asp:LinkButton>
                        
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
