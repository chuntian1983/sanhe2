<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysManage_SetLimiteFee" Codebehind="SetLimiteFee.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function SetFee(cellid,sno)
{
    if($(cellid).innerHTML.indexOf("TempInput")!=-1){return;}
    var cellText=$(cellid).innerText;
    var keypress="onKeypress='return event.keyCode>=48&&event.keyCode<=57;'"
    $(cellid).innerHTML="<input type=text id=TempInput "+keypress+" onblur=\"_OnBlur('"+cellid+"','"+sno+"',this.value,'"+cellText+"');\" value=\""+cellText+"\" class=TempInput1>";
    $("TempInput").focus();
    $("TempInput").select();
}
function _OnBlur(cellid,sno,v,oldv)
{
    var patrn=/\d/;
    if(v=="0"||!patrn.test(v))
    {
        $(cellid).innerText="-";
        $("AllSubjectFee").value=$("AllSubjectFee").value.replace("["+sno+":"+oldv+"]","");
    }
    else
    {
        $(cellid).innerText=v;
        if($("AllSubjectFee").value.indexOf("["+sno+":"+oldv+"]")==-1)
        {
            $("AllSubjectFee").value+="["+sno+":"+$(cellid).innerText+"]";
        }
        else
        {
            $("AllSubjectFee").value=$("AllSubjectFee").value.replace("["+sno+":"+oldv+"]","["+sno+":"+$(cellid).innerText+"]");
        }
    }
}
function _onsubmit()
{
    var LFeeSum=0;
    var CFeeSum=0;
    var manageSubject=$("ManageSubject").value;
    var LFee=$("AllSubjectFee").value.replace(/(\[|-)/g,"").split("]");
    for(var k=0;k<LFee.length-1;k++)
    {
        var FeeV=LFee[k].split(":");
        if(FeeV[0]==manageSubject)
        {
            CFeeSum+=parseFloat(FeeV[1]);
        }
        else
        {
            LFeeSum+=parseFloat(FeeV[1]);
        }
    }
    if(CFeeSum<LFeeSum)
    {
        if(confirm("管理费用控制额不能小于明细科目控制额合计数！\n\n是否自动用明细科目控制额合计数替换管理费用控制额？"))
        {
            var ms="["+manageSubject+":";
            if($("AllSubjectFee").value.indexOf(ms+CFeeSum+"]")==-1)
            {
                $("AllSubjectFee").value+=ms+LFeeSum+"]";
            }
            else
            {
                $("AllSubjectFee").value=$("AllSubjectFee").value.replace(ms+CFeeSum+"]",ms+LFeeSum+"]");
            }
        }
        else
        {
            return false;
        }
    }
    $("AllSubjectFee").value=$("AllSubjectFee").value.replace("-","");
    return true;
}
</script>
</head>
<body onpaste="return false">
    <form id="form1" runat="server" autocomplete="off">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">设置与调整费用控制额</span>
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t3" style="height: 25px; width: 20%; text-align:center; height:35px">
                    账套列表：</td>
                <td class="t4" style="height: 25px; width: 80%; text-align:left">
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="AccountList" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="AccountList_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
            CaptionAlign="Left"
            OnRowDataBound="GridView1_RowDataBound"
            Style="color: navy" Width="750px">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField HeaderText="科目代码" DataField="subjectno" ReadOnly="True" >
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="科目名称" DataField="subjectname" ReadOnly="True" >
                    <ItemStyle Width="370px" />
                </asp:BoundField>
                <asp:BoundField DataField="isdetail" HeaderText="是否明细" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="费用控制标准" DataField="subjectno">
                    <ItemStyle HorizontalAlign="Right" Width="200px" />
                </asp:BoundField>
            </Columns>
            <RowStyle Font-Size="10pt" Height="22px" />
            <SelectedRowStyle BackColor="#FFC0C0" BorderColor="Yellow" BorderStyle="Dotted" />
            <PagerStyle BackColor="White" ForeColor="Olive" />
            <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" ForeColor="Navy" />
            <AlternatingRowStyle BackColor="#EBF0F6" />
        </asp:GridView>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 42px; text-align: center">
                    <asp:Button ID="Button1" runat="server" Height="28px" Text="保存设置" Width="112px" OnClick="Button1_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Height="28px" Text="清除设置" Width="112px" OnClick="Button2_Click" />
                </td>
            </tr>
        </table>
        <br />
        <asp:HiddenField ID="AllSubjectFee" runat="server" />
        <asp:HiddenField ID="ManageSubject" runat="server" />
        <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
