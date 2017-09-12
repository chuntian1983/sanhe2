<%@ Page Language="C#" AutoEventWireup="true" Inherits="FinanceFlow_AssetApply" Codebehind="AssetApply.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if($("FlowName").value=="")
    {
      $("FlowName").focus();
      alert("流程名称不能为空！");
      return false;
    }
    if($("ApplyDate").value=="")
    {
      alert("请选择请示日期！");
      return false;
    }
    if($("ApplyCount").value=="0")
    {
      alert("请示内容列表不能为空！");
      return false;
    }
    return true;
}
function CheckSubmit2()
{
    if($("AssetName").value=="")
    {
      alert("请选择资产！");
      return false;
    }
    var patrn=/^\d{1,8}(\.\d{1,2})?$/;
    if($("LAmount").value=="0"||!patrn.test($("LAmount").value))
    {
      $("LAmount").focus();
      $("LAmount").select();
      alert("租赁数量必须是非零数字！");
      return false;
    }
    if($("LYears").value=="0"||!patrn.test($("LYears").value))
    {
      $("LYears").focus();
      $("LYears").select();
      alert("租赁年限必须是非零数字！");
      return false;
    }
    if($("MyBasePrice").value=="0"||!patrn.test($("MyBasePrice").value))
    {
      $("MyBasePrice").focus();
      $("MyBasePrice").select();
      alert("单位底价必须是非零数字！");
      return false;
    }
    if($("LTotalBalance").value=="0"||!patrn.test($("LTotalBalance").value))
    {
      $("LTotalBalance").focus();
      $("LTotalBalance").select();
      alert("总金额必须是非零数字！");
      return false;
    }
    return true;
}
function ShowContract(vid)
{
   window.showModalDialog("../Contract/LeaseCardShow.aspx?ctype=0&id="+vid+"&g="+(new Date()).getTime(),"","dialogWidth=770px;dialogHeight=400px;center=yes;");
   return false;
}
function CreateContract(aid,rid)
{
   if(window.showModalDialog("../Contract/LeaseCard.aspx?ctype=0&rid="+rid+"&aid="+aid+"&g="+(new Date()).getTime(),"","dialogWidth=750px;dialogHeight:480px;center=yes;")!="000000")
   {
       __doPostBack('doPostBackButton','');
   }
   return false;
}
function UploadFile()
{
    var returnV=window.showModalDialog("../AccountManage/Appendices.aspx?g="+(new Date()).getTime(),$("HasSelAppendices").value,"dialogWidth=720px;dialogHeight=508px;center=yes;");
    if(returnV)
    {
        $("HasSelAppendices").value=returnV;
    }
    return false;
}
function ShowAppendices(v,atype)
{
    window.showModalDialog("../AccountManage/AppendixShow.aspx?atype="+atype+"&id="+v+"&g="+(new Date()).getTime(),"","dialogWidth=720px;dialogHeight=508px;center=yes;");
    return false;
}
function SelectAsset()
{
    var info=window.showModalDialog("SelectAsset.aspx?atype=1&g="+(new Date()).getTime(),"","dialogWidth=750px;dialogHeight:365px;center=yes;");
    if(info)
    {
        $("AssetName").value=info[0];
        $("AssetModel").value=info[1];
        $("AssetCardID").value=info[2];
    }
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">资产租赁请示</span></td>
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
                    <asp:TextBox ID="FlowName" runat="server" Width="240px"></asp:TextBox></td>
                <td class="t1" style="width: 112px; height: 25px; text-align: right">
                    请示日期：</td>
                <td class="t2" style="width: 35%; height: 25px">
                    <asp:TextBox ID="ApplyDate" runat="server" Width="80px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lbnUploadFile" runat="server">上传附件</asp:LinkButton></td>
            </tr>
        </table>
        <table id="ReplyArea" cellpadding="0" cellspacing="0" style="width: 750px" runat="server" visible="false">
            <tr>
                <td class="t2" style="height: 10px;" colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 25px; text-align: right">
                    批复意见：</td>
                <td class="t1" style="width: 35%;">
                    <asp:Label ID="AuditState" runat="server" Width="100px"></asp:Label></td>
                <td class="t1" style="width: 15%; text-align: right">
                    批复日期：</td>
                <td class="t2" style="width: 35%;">
                    <asp:TextBox ID="ReplyDate" runat="server" Width="80px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lbnShowAppendices" runat="server">查看附件</asp:LinkButton></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 25px; text-align: right">
                    批复内容：</td>
                <td class="t2" colspan="3" style="width: 85%;">
                    <asp:Label ID="AuditOpinion" runat="server" Width="100px"></asp:Label></td>
            </tr>
        </table>
        <table id="EditArea" cellpadding="0" cellspacing="0" style="width: 750px" runat="server">
            <tr>
                <td class="t2" colspan="4" style="height: 35px; text-align: center">
                    <asp:Button ID="SaveApply" runat="server" Height="26px" OnClick="SaveApply_Click" Text="保存" Width="150px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="SaveAndSubmit" runat="server" Height="26px" OnClick="SaveApply_Click" Text="保存并立即生效" Width="150px" /></td>
            </tr>
            <tr>
                <td class="t2" colspan="4" style="height: 25px; text-align: center; background:#f6f6f6;">
                    请示内容列表</td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 25px; text-align: right">
                    资产名称：</td>
                <td class="t1" style="width: 35%; height: 25px">
                    <asp:TextBox ID="AssetName" runat="server" Width="240px"></asp:TextBox>
                    <asp:HiddenField ID="AssetCardID" runat="server" /></td>
                <td class="t1" style="width: 18%; height: 25px; text-align: right">
                    规格型号：</td>
                <td class="t2" style="width: 32%; height: 25px">
                    <asp:TextBox ID="AssetModel" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 25px; text-align: right">
                    租赁数量：</td>
                <td class="t1" style="width: 35%; height: 25px">
                    <asp:TextBox ID="LAmount" runat="server" Width="100px"></asp:TextBox></td>
                <td class="t1" style="width: 18%; height: 25px; text-align: right">
                    租赁年限：</td>
                <td class="t2" style="width: 32%; height: 25px">
                    <asp:TextBox ID="LYears" runat="server" Width="100px">0</asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 25px; text-align: right">
                    单位底价：</td>
                <td class="t1" style="width: 35%; height: 25px">
                    <asp:TextBox ID="MyBasePrice" runat="server" Width="100px"></asp:TextBox></td>
                <td class="t1" style="width: 18%; height: 25px; text-align: right">
                    总&nbsp;&nbsp;金&nbsp;&nbsp;额：</td>
                <td class="t2" style="width: 32%; height: 25px">
                    <asp:TextBox ID="LTotalBalance" runat="server" Width="100px">0</asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 25px; text-align: right">
                    支付方式：</td>
                <td class="t1" style="width: 35%; height: 25px">
                    <asp:TextBox ID="PayType" runat="server" Width="100px"></asp:TextBox></td>
                <td class="t1" style="width: 18%; height: 25px; text-align: right">
                    每次收款：</td>
                <td class="t2" style="width: 32%; height: 25px">
                    <asp:TextBox ID="PayMoney" runat="server" Width="100px">0</asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    备注：</td>
                <td class="t1">
                    <asp:TextBox ID="ApplyNotes" runat="server" Width="240px"></asp:TextBox></td>
                <td class="t2" colspan="2" style="text-align: center">
                    <asp:Button ID="AddItem" runat="server" Height="23px" OnClick="AddItem_Click" Text="添加" Width="125px" />
                    <asp:LinkButton ID="doPostBackButton" runat="server" OnClick="doPostBackButton_Click"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 1px;">&nbsp;</td>
            </tr>
            <tr>
                <td style="height: 10px;"></td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowDataBound="GridView1_RowDataBound" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
            PageSize="15" Style="color: navy" Width="1004px">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="AssetCardID" HeaderText="资产名称" ReadOnly="true">
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
                <asp:BoundField DataField="AssessBasePrice" HeaderText="评估底价" HtmlEncode="False" DataFormatString="{0:f}" ReadOnly="true">
                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                    <ControlStyle Width="95%" />
                </asp:BoundField>
                <asp:BoundField DataField="LTotalBalance" HeaderText="金总额" HtmlEncode="False" DataFormatString="{0:f}">
                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                    <ControlStyle Width="95%" />
                </asp:BoundField>
                <asp:BoundField DataField="PayType" HeaderText="支付方式">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                    <ControlStyle Width="95%" />
                </asp:BoundField>
                <asp:BoundField DataField="PayMoney" HeaderText="每次收款" HtmlEncode="False" DataFormatString="{0:f}">
                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                    <ControlStyle Width="95%" />
                </asp:BoundField>
                <asp:BoundField DataField="ApplyNotes" HeaderText="备注">
                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                    <ControlStyle Width="95%" />
                </asp:BoundField>
                <asp:CommandField HeaderText="编辑" ShowEditButton="True">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="删除">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Bind("id") %>' OnClick="btnDelete_Click">删除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="关联合同" Visible="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnCreate" runat="server" CommandArgument='<%# Bind("id") %>'>生成</asp:LinkButton>
                        <asp:HiddenField ID="hidContractID" runat="server" Value='<%# Eval("ContractID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
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
        <asp:HiddenField ID="FlowID" runat="server" />
        <asp:HiddenField ID="FlowState" runat="server" />
        <asp:HiddenField ID="FlowCurrent" runat="server" />
        <asp:HiddenField ID="ApplyCount" runat="server" Value="0" />
        <asp:HiddenField ID="HasSelAppendices" runat="server" />
    </div>
    </form>
</body>
</html>
