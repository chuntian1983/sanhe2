<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xiangxi.aspx.cs" Inherits="SanZi.Web.view.zhaotoubiao.xiangxi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>项目详细信息</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" 
            OnMenuItemClick="Menu1_MenuItemClick" BackColor="#FFFBD6" 
            DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="0.8em" 
            ForeColor="#990000" StaticSubMenuIndent="10px">
                                        <DynamicHoverStyle BackColor="#990000" ForeColor="White" />
                                        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                                        <DynamicMenuStyle BackColor="#FFFBD6" />
                                        <DynamicSelectedStyle BackColor="#FFCC66" />
                                        <Items>
                                            <asp:MenuItem Text="村名代表会议记录" Value="1" Selected="True"></asp:MenuItem>
                                            <asp:MenuItem Text="招标项目申请" Value="2"></asp:MenuItem>
                                            <asp:MenuItem Text="招标项目审批" Value="3"></asp:MenuItem>
                                            <asp:MenuItem Text="预算书录入" Value="4"></asp:MenuItem>
                                            <asp:MenuItem Text="招标文件录入" Value="5"></asp:MenuItem>
<asp:MenuItem Text="招标公告" Value="6"></asp:MenuItem>
<asp:MenuItem Text="参投公告" Value="7"></asp:MenuItem>
<asp:MenuItem Text="竞价招投标" Value="8"></asp:MenuItem>
<asp:MenuItem Text="中标公告" Value="9"></asp:MenuItem>
                                            <asp:MenuItem Text="签订合同" Value="10"></asp:MenuItem>
                                        </Items>
                                        <StaticMenuItemStyle CssClass="tabItem" BorderWidth="2px" 
                                            HorizontalPadding="5px" VerticalPadding="2px" />
                                        <StaticSelectedStyle CssClass="tabSelected" BackColor="#FFCC66" />
                                        <StaticHoverStyle CssClass="tabHover" BackColor="#990000" ForeColor="White" />
                                    </asp:Menu>

        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server" >
                <table>
                <asp:Repeater runat=server ID="rep"><ItemTemplate>
                <tr>
                <td>
                    <img alt="" src='../../UploadFile/ztb/<%#Eval("xmpath")%>' />
                </td>
                </tr>
                </ItemTemplate></asp:Repeater>
                </table>
            </asp:View>
        </asp:MultiView></div>
    </form>
</body>
</html>
