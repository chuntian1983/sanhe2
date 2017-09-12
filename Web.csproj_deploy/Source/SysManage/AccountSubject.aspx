<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" Inherits="SysManage_AccountSubject" Codebehind="AccountSubject.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function FStartAccount()
{
    var returnV=window.showModalDialog("StartAccount.aspx?g="+(new Date()).getTime(),"","dialogWidth=500px;dialogHeight=424px;center=yes;");
    if(typeof(returnV)!="undefined")
    {
        $("StartAccount").disabled="disabled";
        $("WriteBalance").disabled="disabled";
    }
}
function PopWBalance(VID,Cell0,Cell1)
{
    var returnV=window.showModalDialog("PopWBalance.aspx?id="+VID+"&g="+(new Date()).getTime(),VID,"dialogWidth=400px;dialogHeight=323px;center=yes;");
    if(typeof(returnV)!="undefined")
    {
        $(Cell0).innerText=returnV[0];
        $(Cell1).innerText=returnV[1];
    }
}
function SelectChange(obj,SelectValue)
{
    if(SelectValue!="000")
    {
        $(obj).disabled="disabled";
    }
    else
    {
       $(obj).disabled="";
    }
}
function CheckSubmit()
{
    var patrn1=/\d/;
    var sl=$("SubjectNo").value.length;
    if(sl==0)
    {
      $("SubjectNo").focus();
      alert("科目代码不能为空！");
      return false;
    }
    if(!patrn1.test($("SubjectNo").value))
    {
      $("SubjectNo").focus();
      alert("科目代码必须为数字！");
      return false;
    }
    var slevel=$("SubjectLevel").value+",";
    var a=$("SubjectLevel").value.split(",");
    if(slevel.indexOf(sl+",")==-1||$("SubjectNo").value=="000")
    {
      $("SubjectNo").focus();
      alert("科目代码长度不正确！");
      return false;
    }
    if(sl>eval(a[a.length-1]))
    {
      $("SubjectNo").focus();
      alert("科目长度不能超过"+a[a.length-1]+"个数字！");
      return false;
    }
    if($("NoLowLength").value.indexOf(sl)==-1)
    {
        $("SubjectNo").focus();
        alert("您无权增加该级科目！");
        return false;
    }
    for(var i=2;i<a.length;i++)
    {
        if(eval(a[i])==$("SubjectNo").value.length)
        {
            var pno="["+$("SubjectNo").value.substring(0,eval(a[i-1]))+"]";
            if($("AllMainSubject").value.indexOf(pno)==-1)
            {
                $("SubjectNo").focus();
                alert("上级科目"+pno+"不存在，不能增加该科目！");
                return false;
            }
        }
    }
    if($("SubjectName").value=="")
    {
      $("SubjectName").focus();
      alert("科目名称不能为空！");
      return false;
    }
    if($("SubjectName").value.length>18)
    {
      $("SubjectName").focus();
      alert("科目名称长度不能超过18个汉字或字符！");
      return false;
    }
    var patrn2=/(\/|\[|\])+/;
    if(patrn2.test($("SubjectName").value))
    {
      $("SubjectName").focus();
      alert("科目名称中不能含有以下字符：/ ] [");
      return false;
    }
    $("HSubjectType").value=$("SubjectType").value;
    if($("AccountStruct_0").checked)
    {
        $("HAccountStruct").value="0";
    }
    else
    {
        $("HAccountStruct").value="1";
    }
    return true;
}
function SubjectNo_onkeyup()
{
    $("SubjectType").disabled="";
    $("AccountStruct").disabled="";
    var sLength=$("SubjectNo").value.length;
    var a=$("AllMainSubject").value.split(",");
    var b=$("SubjectLevel").value.split(",");
    for(var i=1;i<b.length;i++)
    {
        if(sLength>eval(b[i-1])&&sLength<=eval(b[i]))
        {
            var c="["+$("SubjectNo").value.substring(0,eval(b[i-1]))+"]";
            var pos=$("AllMainSubject").value.indexOf(c);
            if(pos!=-1)
            {
                $("SubjectType").disabled="disabled";
                $("AccountStruct").disabled="disabled";
                $("HAccountType").value=$("AllMainSubject").value.substr(pos+c.length+2,1);
                $("SubjectType").selectedIndex=eval($("AllMainSubject").value.substr(pos+c.length,1))-1;
                if($("AllMainSubject").value.substr(pos+c.length+4,1)=="0")
                {
                    $("AccountStruct_0").checked=true;
                    $("AccountStruct_1").checked=false;
                }
                else
                {
                    $("AccountStruct_0").checked=false;
                    $("AccountStruct_1").checked=true;
                }
                $("HIsDetail").value=$("AllMainSubject").value.substr(pos+c.length+6,1);
            }
        }
    }
}
function SubjectNo_onkeypress()
{
    var keynum;
    var keychar;
    var numcheck;
    if(window.event)
    {
        keynum = event.keyCode;
    }
    else if(event.which)
    {
        keynum = event.which;
    }
    keychar = String.fromCharCode(keynum);
    numcheck = /\d/;
    if(!numcheck.test(keychar)){return false;}
    var sellength;
    if (document.selection)
    {
        var sel=document.selection.createRange().text;
        sellength=sel.length;
    }
    else
    {
        sellength=$("SubjectNo").selectionEnd-$("SubjectNo").selectionStart;
    }
    var a=$("SubjectLevel").value.split(",");
    if($("SubjectNo").value.length>=eval(a[a.length-1])&&sellength<1)
    {
        alert("科目长度不能超过"+a[a.length-1]+"个数字！");
        return false;
    }
}
</script>
</head>
<body onpaste="return false">
    <form id="form1" runat="server" autocomplete="off">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">科目模板库维护</span>
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    上级代码：</td>
                <td class="t1" style="width: 35%">
                    <asp:TextBox ID="PSubjectNo" runat="server" BorderWidth="1px" ReadOnly="True" Width="252px"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: right">
                    上级名称：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="ParentSubject" runat="server" BorderWidth="1px" ReadOnly="True" Width="252px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    科目代码：</td>
                <td class="t1" style="width: 35%">
                    <asp:TextBox ID="SubjectNo" runat="server" Width="252px"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: right">
                    科目名称：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="SubjectName" runat="server" Width="252px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    科目属性：</td>
                <td class="t1" style="width: 35%">
                    <asp:DropDownList ID="SubjectType" runat="server">
                        <asp:ListItem Value="1">资产类</asp:ListItem>
                        <asp:ListItem Value="2">负债类</asp:ListItem>
                        <asp:ListItem Value="3">权益类</asp:ListItem>
                        <asp:ListItem Value="4">成本类</asp:ListItem>
                        <asp:ListItem Value="5">损益类</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t1" style="width: 15%; text-align: right">
                    余额方向：</td>
                <td class="t2" style="width: 35%">
                    <asp:RadioButtonList ID="AccountStruct" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="0" Selected="True">余额在借方</asp:ListItem>
                        <asp:ListItem Value="1">余额在贷方</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 35px; text-align: center">
                    <asp:Button ID="AddSubject" runat="server" Height="26px" Text="添加科目" Width="212px" OnClick="AddSubject_Click" />
                    &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="UpdateData" runat="server" Height="26px" Text="取自模板库" Width="212px" OnClick="UpdateData_Click" />
                    &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="GoParent" runat="server" Height="26px" Text="返回上级科目" Width="211px" OnClick="GoParent_Click" CommandArgument="000" />
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td id="M0" class="t3" style="height: 22px; text-align: center; width:16%; cursor:hand" onclick="location.href='AccountSubject.aspx';" runat="server">
                    全部</td>
                <td id="M1" class="t3" style="height: 22px; text-align: center; width:16%; cursor:hand" onclick="location.href='AccountSubject.aspx?stype=1';" runat="server">
                    资产类</td>
                <td id="M2" class="t3" style="height: 22px; text-align: center; width:16%; cursor:hand" onclick="location.href='AccountSubject.aspx?stype=2';" runat="server">
                    负债类</td>
                <td id="M3" class="t3" style="height: 22px; text-align: center; width:16%; cursor:hand" onclick="location.href='AccountSubject.aspx?stype=3';" runat="server">
                    权益类</td>
                <td id="M4" class="t3" style="height: 22px; text-align: center; width:16%; cursor:hand" onclick="location.href='AccountSubject.aspx?stype=4';" runat="server">
                    成本类</td>
                <td id="M5" class="t4" style="height: 22px; text-align: center; width:16%; cursor:hand" onclick="location.href='AccountSubject.aspx?stype=5';" runat="server">
                    损益类</td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CaptionAlign="Left" Style="color: navy"
            Width="750px" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit">
            <Columns>
                <asp:BoundField DataField="subjectno" HeaderText="科目编号">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="subjectname" HeaderText="科目名称">
                    <ItemStyle Width="200px" />
                    <ControlStyle Width="196px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="科目属性">
                    <EditItemTemplate>
                        <asp:DropDownList ID="SubjectType" runat="server" SelectedValue='<%# Bind("SubjectType") %>'>
                            <asp:ListItem Value="1">资产类</asp:ListItem>
                            <asp:ListItem Value="2">负债类</asp:ListItem>
                            <asp:ListItem Value="3">权益类</asp:ListItem>
                            <asp:ListItem Value="4">成本类</asp:ListItem>
                            <asp:ListItem Value="5">损益类</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemStyle Width="105px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="SubjectType" runat="server" Text='<%# Bind("SubjectType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="账式">
                    <EditItemTemplate>
                        <asp:DropDownList ID="AccountType" runat="server" SelectedValue='<%# Bind("AccountType") %>'>
                            <asp:ListItem Value="0">一般金额账</asp:ListItem>
                            <asp:ListItem Value="1">数量金额账</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemStyle Width="95px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="AccountType" runat="server" Text='<%# Bind("AccountType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="原始凭证">
                    <EditItemTemplate>
                        <asp:DropDownList ID="IsEntryData" runat="server" SelectedValue='<%# Bind("isentrydata") %>'>
                            <asp:ListItem Value="1">是</asp:ListItem>
                            <asp:ListItem Value="0">否</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="65px" />
                    <ItemTemplate>
                        <asp:Label ID="IsEntryData" runat="server" Text='<%# Bind("isentrydata") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="明细">
                    <EditItemTemplate>
                        <asp:DropDownList ID="IsDetail" runat="server" SelectedValue='<%# Bind("isdetail") %>'>
                            <asp:ListItem Value="1">是</asp:ListItem>
                            <asp:ListItem Value="0">否</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                    <ItemTemplate>
                        <asp:Label ID="IsDetail" runat="server" Text='<%# Bind("isdetail") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField HeaderText="编辑" ShowEditButton="True">
                    <ItemStyle HorizontalAlign="Center" Width="65px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="操作选项">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Bind("id") %>' CommandName="btnDelete">删除科目</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle Font-Size="10pt" Height="22px" />
            <PagerStyle BackColor="White" ForeColor="Olive" />
            <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" ForeColor="Navy" />
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <AlternatingRowStyle BackColor="#EBF0F6" />
        </asp:GridView>
        <asp:Label ID="ExeScript" runat="server"></asp:Label>
        <asp:HiddenField ID="AllMainSubject" runat="server" />
        <asp:HiddenField ID="HSubjectType" runat="server" Value="0" />
        <asp:HiddenField ID="HAccountStruct" runat="server" Value="0" />
        <asp:HiddenField ID="HAccountType" runat="server" Value="0" />
        <asp:HiddenField ID="HIsDetail" runat="server" Value="0" />
        <asp:HiddenField ID="SubjectLevel" runat="server" />
        <asp:HiddenField ID="NoLowLength" runat="server" Value="5" />
        <script type="text/javascript">SubjectNo_onkeyup();</script>
    </div>
    </form>
</body>
</html>
