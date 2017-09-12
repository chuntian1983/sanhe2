<%@ Page Language="C#" AutoEventWireup="true" Inherits="FinanceFlow_AssetReply" Codebehind="AssetReply.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function UploadFile()
{
    var returnV=window.showModalDialog("../AccountManage/Appendices.aspx?g="+(new Date()).getTime(),$("HasSelAppendices").value,"dialogWidth=720px;dialogHeight=508px;center=yes;");
    if(returnV)
    {
        $("HasSelAppendices").value=returnV;
    }
    return false;
}
function ShowAppendices(v)
{
    window.showModalDialog("../AccountManage/AppendixShow.aspx?atype=0&id="+v+"&g="+(new Date()).getTime(),"","dialogWidth=720px;dialogHeight=508px;center=yes;");
    return false;
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">资产租赁批复</span></td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td colspan="4" style="height: 10px;"></td>
            </tr>
            <tr>
                <td class="t2" colspan="4" style="height: 25px; background:#f6f6f6; text-align: center;">&nbsp;流程基本信息</td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    乡镇名称：</td>
                <td class="t1" style="height: 25px">
                    <asp:Label ID="TownName" runat="server"></asp:Label>&nbsp;</td>
                <td class="t1" style="height: 25px; text-align: right; width: 112px;">
                    账套名称：</td>
                <td class="t2" style="height: 25px">
                    <asp:Label ID="AccountName" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 25px; text-align: right">
                    流程名称：</td>
                <td class="t1" style="width: 35%; height: 25px">
                    <asp:Label ID="FlowName" runat="server" Width="240px"></asp:Label></td>
                <td class="t1" style="width: 112px; height: 25px; text-align: right">
                    请示附件：</td>
                <td class="t2" style="width: 35%; height: 25px">
                    &nbsp;<asp:LinkButton ID="lbnShowAppendices" runat="server">查看</asp:LinkButton></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    批复日期：</td>
                <td class="t1">
                    <asp:TextBox ID="ReplyDate" runat="server" Width="100px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    批复意见：</td>
                <td class="t2">
                    <asp:RadioButtonList ID="AuditState" runat="server" RepeatDirection="Horizontal" Width="158px">
                        <asp:ListItem Selected="True" Value="1">同意</asp:ListItem>
                        <asp:ListItem Value="0">否决</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 25px; text-align: right">
                    批复内容：</td>
                <td class="t2" colspan="3" style="width: 85%;">
                    <asp:TextBox ID="AuditOpinion" runat="server" TextMode="MultiLine" Width="550px"></asp:TextBox>
                    <asp:LinkButton ID="lbnUploadFile" runat="server">上传附件</asp:LinkButton></td>
            </tr>
            <tr>
                <td class="t2" colspan="4" style="height: 25px; background:#f6f6f6; text-align: center;">&nbsp;请示内容</td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowDataBound="GridView1_RowDataBound" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
            PageSize="15" Style="color: navy" Width="1004px">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="AssetCardID" HeaderText="资产名称" ReadOnly="True">
                    <ItemStyle Width="100px" />
                    <ControlStyle Width="95%" />
                </asp:BoundField>
                <asp:BoundField DataField="AssetModel" HeaderText="规格型号">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                    <ControlStyle Width="95%" />
                </asp:BoundField>
                <asp:BoundField DataField="LAmount" HeaderText="租赁数量" HtmlEncode="False" DataFormatString="{0:f}">
                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                    <ControlStyle Width="95%" />
                </asp:BoundField>
                <asp:BoundField DataField="LYears" HeaderText="租赁年限" HtmlEncode="False" DataFormatString="{0:f}">
                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                    <ControlStyle Width="95%" />
                </asp:BoundField>
                <asp:BoundField DataField="MyBasePrice" HeaderText="单位底价" HtmlEncode="False" DataFormatString="{0:f}">
                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                    <ControlStyle Width="95%" />
                </asp:BoundField>
                <asp:BoundField DataField="AssessBasePrice" HeaderText="评估底价" HtmlEncode="False" DataFormatString="{0:f}" ReadOnly="True">
                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                    <ControlStyle Width="95%" />
                </asp:BoundField>
                <asp:BoundField DataField="LTotalBalance" HeaderText="金总额" HtmlEncode="False" DataFormatString="{0:f}">
                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                    <ControlStyle Width="95%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="支付方式">
                    <ItemTemplate>
                        <asp:TextBox ID="PayType" runat="server" Text='<%# Bind("PayType") %>'></asp:TextBox>
                    </ItemTemplate>
                    <ControlStyle Width="95%" />
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="每次收款">
                    <ItemTemplate>
                        <asp:TextBox ID="PayMoney" runat="server" Text='<%# Bind("PayMoney", "{0:f}") %>'></asp:TextBox>
                    </ItemTemplate>
                    <ControlStyle Width="95%" />
                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                </asp:TemplateField>
                <asp:BoundField DataField="ApplyNotes" HeaderText="备注">
                    <ItemStyle Width="200px" HorizontalAlign="Center" />
                    <ControlStyle Width="95%" />
                </asp:BoundField>
            </Columns>
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
            <RowStyle Font-Size="10pt" />
            <SelectedRowStyle BackColor="#FFC0C0" BorderColor="Yellow" BorderStyle="Dotted" />
            <PagerStyle BackColor="White" ForeColor="Olive" />
            <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" ForeColor="Navy" />
            <AlternatingRowStyle BackColor="#EBF0F6" />
        </asp:GridView>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="l r b" colspan="4" style="height: 35px; text-align: center">
                    <asp:Button ID="SaveReply" runat="server" Height="26px" OnClick="SaveReply_Click" Text="保存" Width="150px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="SaveAndSubmit" runat="server" Height="26px" OnClick="SaveReply_Click" Text="保存并立即生效" Width="150px" /></td>
            </tr>
        </table>
        <asp:HiddenField ID="FlowID" runat="server" />
        <asp:HiddenField ID="FlowState" runat="server" />
        <asp:HiddenField ID="FlowCurrent" runat="server" />
        <asp:HiddenField ID="HasSelAppendices" runat="server" />
    </div>
    </form>
</body>
</html>
