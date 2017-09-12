<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SanZi.Web.pibanka.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>批办卡管理</title>
    <link href="../Style.css" type="text/css" rel="stylesheet" />
<script type="text/javascript">
    function ShowVoucher(vid, pid) {
        window.showModalDialog("/AccountManage/LookVoucher.aspx?id=" + vid + "&pid="+pid+"&g=" + (new Date()).getTime(), "", "dialogWidth=767px;dialogHeight=385px;center=yes;");
        return false;
    }
    function CVoucher(pid) {
        window.showModalDialog("createv.aspx?pid=" + pid + "&g=" + (new Date()).getTime(), "", "dialogWidth=405px;dialogHeight=200px;center=yes;");
        return false;
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; text-align: left; margin-top: 5px; background: #000800;"
        cellspacing="1" cellpadding="3">
        <tr style="background: #ffffff;">
            <td colspan="2" class="tableTitle">
                批办卡管理
            </td>
        </tr>
        <tr style="background: #ffffff;">
            <td class="tableContent">
                <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="查询" Width="100px" OnClick="btnSearch_Click" />
            </td>
        </tr>
        <tr style="background: #ffffff;">
            <td class="tableContent">
                <asp:Repeater ID="rpFileManage" runat="server">
                    <HeaderTemplate>
                        <table width="100%" border="1" cellspacing="0" cellpadding="4" style="border-collapse: collapse;
                            background: #E3EFFF;">
                            <tr>
                                <th style="width: 15%">
                                    单位名称
                                </th>
                                <th style="width: 10%">
                                    支出金额
                                </th>
                                <th style="width: 15%">
                                    议定事由
                                </th>
                                  <th style="width: 10%">
                                   电话
                                </th>
                                
                                <th style="width: 15%">
                                    操作
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="background-color: #FAF3DC">
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "OutMoney")%>元
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "OutReason")%>
                            </td>
                         
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "SubTime")%>
                            </td>

                            <td>
                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "PID")%>' ></asp:HiddenField>
                                <a href="Show.aspx?pid=<%#DataBinder.Eval(Container.DataItem, "PID")%>" target="_blank">
                                    查看审批人</a>
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CommandName='<%#DataBinder.Eval(Container.DataItem, "subid")%>' CommandArgument='<%#DataBinder.Eval(Container.DataItem, "OutMoney")+"|"+DataBinder.Eval(Container.DataItem, "d")+"|"+DataBinder.Eval(Container.DataItem, "c")+"|"+DataBinder.Eval(Container.DataItem, "zhaiyao")+"|"+DataBinder.Eval(Container.DataItem, "PID")+"|"+DataBinder.Eval(Container.DataItem, "lujing")%>'>
                                生成凭证
                                </asp:LinkButton>
                                <a href="index.aspx?act=del&pid=<%#DataBinder.Eval(Container.DataItem, "PID")%>"
                                    onclick="return confirm('确定要删除此批办卡吗？');">删除</a>
                        </tr>
                    </ItemTemplate>
                    
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageSize="10" AlwaysShow="True"
                    OnPageChanged="AspNetPager1_PageChanged" ShowCustomInfoSection="Left" CustomInfoSectionWidth="40%"
                    ShowPageIndexBox="always" PageIndexBoxType="DropDownList" CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录">
                </webdiyer:AspNetPager>
                </asp:Repeater>
                
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
