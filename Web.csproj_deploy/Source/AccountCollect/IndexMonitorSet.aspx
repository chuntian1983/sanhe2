<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountCollect_IndexMonitorSet" Codebehind="IndexMonitorSet.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if($("IndexSubject").value=="")
    {
      $("IndexSubject").focus();
      alert("请选择指标监控科目！");
      return false;
    }
    if($("IndexValue").value=="0")
    {
      $("IndexValue").focus();
      alert("指标限额不能为零！");
      return false;
    }
    return true;
}
function SelSubject(o)
{
    var returnV=window.showModalDialog("../AccountInit/SelAllSubject.aspx?t=0&f=0&g="+(new Date()).getTime(),"","dialogWidth=402px;dialogHeight:387px;center=yes;");
    if(typeof(returnV)!="undefined")
    {
        o.value=returnV[1]+"."+returnV[0];
    }
}
function OpenMonitor(id,flag)
{
    switch(flag)
    {
        case "0":
            window.showModalDialog("IndexMonitorShow0.aspx?id="+id+"&g="+(new Date()).getTime(),"","dialogWidth=780px;dialogHeight:550px;center=yes;");
            break;
        case "1":
            window.showModalDialog("IndexMonitorShow1.aspx?id="+id+"&g="+(new Date()).getTime(),"","dialogWidth=780px;dialogHeight:600px;center=yes;");
            break;
        case "2":
            window.showModalDialog("IndexMonitorShow2.aspx?id="+id+"&g="+(new Date()).getTime(),"","dialogWidth=780px;dialogHeight:600px;center=yes;");
            break;
     }
     return false;
}
</script>
</head>
<body id="HBody">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">指标参数设置</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 12%; height: 33px; text-align: right; font-size:10pt">
                    设置科目：</td>
                <td class="t1" style="width: 35%; height: 33px;">
                    &nbsp;<asp:TextBox ID="IndexSubject" runat="server" Width="240px" MaxLength="50" BackColor="#E0E0E0" BorderWidth="1px"></asp:TextBox></td>
                <td class="t1" style="width: 12%; text-align: right; height: 33px; font-size:10pt">
                    指标类型：</td>
                <td class="t2" style="width: 41%; height: 33px; text-align: center">
                    <asp:RadioButtonList ID="IndexType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" Selected="True">单笔发生额</asp:ListItem>
                        <asp:ListItem Value="1">月累计发生额</asp:ListItem>
                        <asp:ListItem Value="2">年累计发生额</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td class="t3" style="font-size: 10pt; width: 10%; height: 33px; text-align: right">
                    指标限额：</td>
                <td class="t3" style="height: 33px;">
                    &nbsp;<asp:TextBox ID="IndexValue" runat="server" Width="120px" MaxLength="50" BorderWidth="1px">0</asp:TextBox></td>
                <td class="t4" colspan="2" style="height: 33px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加指标" Width="150px" /></td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowDataBound="GridView1_RowDataBound" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
            Style="color: navy" Width="750px" PageSize="15">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="id" HeaderText="编号" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="IndexSubject" HeaderText="设置科目">
                    <ItemStyle Width="200px" />
                    <ControlStyle Width="180px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="指标类型">
                    <EditItemTemplate>
                        <asp:DropDownList ID="IndexTypeEdit" runat="server" SelectedValue='<%# Bind("IndexType") %>'>
                            <asp:ListItem Value="0">单笔发生额</asp:ListItem>
                            <asp:ListItem Value="1">月累计发生额</asp:ListItem>
                            <asp:ListItem Value="2">年累计发生额</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="IndexTypeShow" runat="server" Text='<%# Bind("IndexType") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="120px" />
                    <ItemStyle Width="140px" />
                </asp:TemplateField>
                <asp:BoundField DataField="IndexValue" HeaderText="指标限额" HtmlEncode="False" DataFormatString="{0:f}">
                    <ItemStyle HorizontalAlign="Right" Width="120px" />
                    <ControlStyle Width="100px" />
                </asp:BoundField>
                <asp:CommandField HeaderText="编辑" ShowEditButton="True">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="查看">
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnMonitor" runat="server" CommandArgument='<%# Bind("IndexType") %>' CommandName="btnMonitor">查看</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="删除">
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Bind("id") %>' CommandName="btnDelete" OnClick="btnDelete_Click">删除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle Font-Size="10pt" />
            <SelectedRowStyle BackColor="#FFC0C0" BorderColor="Yellow" BorderStyle="Dotted" />
            <PagerStyle BackColor="White" ForeColor="Olive" />
            <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" ForeColor="Navy" />
            <AlternatingRowStyle BackColor="#EBF0F6" />
            <PagerTemplate>
                &nbsp;<asp:LinkButton ID="FirstPage" runat="server" Font-Size="10pt" OnClick="FirstPage_Click">首页</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="PreviousPage" runat="server" Font-Size="10pt" OnClick="PreviousPage_Click">上一页</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="NextPage" runat="server" Font-Size="10pt" OnClick="NextPage_Click">下一页</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="LastPage" runat="server" Font-Size="10pt" OnClick="LastPage_Click">尾页</asp:LinkButton>
                &nbsp;
                <asp:Label ID="ShowPageInfo" runat="server" Font-Size="10pt" Text="总页数："></asp:Label>
                &nbsp;&nbsp;
                <asp:Label ID="Label1" runat="server" Font-Size="10pt" ForeColor="Navy" Text="跳转到："></asp:Label>
                <asp:DropDownList ID="JumpPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="JumpPage_SelectedIndexChanged">
                </asp:DropDownList>
            </PagerTemplate>
        </asp:GridView>
      </div>
    </form>
</body>
</html>
