<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysManage_ReportDesign" Codebehind="ReportDesign.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function Number.prototype.str(s){var a=""+this;return s.substring(0,s.length-a.length)+a;}
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function OnRowClick(row,rowid)
{
    if(row==0){return;}
    if($("curRowID").value!="")
    {
        $($("curRowID").value).style.backgroundColor="";
    }
    $(rowid).style.backgroundColor="#f6f6f6";
    $("curRowID").value=rowid;
    $("curRowIndex").value=row;
}
function OnCell0Click(cellid)
{
    $("ControlDiv").style.display="";
    var v=$(cellid).innerHTML.replace(/&nbsp;&nbsp;/g," ");
    if(v=="&nbsp;"||v==" "){v="";}
    $("CellText").value=v;
    $("CellText").select();
    $("TempC").value=cellid;
    SetObjectPos(cellid,"ControlDiv");
}
function OnCell1Click(cellid)
{
    $("ControlDiv").style.display="none";
    var returnV=window.showModalDialog("../AccountInit/DefineExpr.aspx?f=0&g="+(new Date()).getTime(),$(cellid).ItemExpr,"dialogWidth=402px;dialogHeight:359px;center=yes;");
    if(typeof(returnV)!="undefined")
    {
        $(cellid).ItemExpr=returnV;
    }
}
function OnCell2Click(cellid)
{
    if($(cellid).innerHTML.indexOf("TempInput")!=-1){return;}
    var v=$(cellid).innerText;
    if(v==" "){v="";}
    $(cellid).innerHTML="<input type=text id=TempInput onblur=\"$('"+cellid+"').innerHTML=this.value;\" value=\""+v+"\" class=TempInput2>";
    $("TempInput").focus();
    $("TempInput").select();
}
function wCellText()
{
    $("ControlDiv").style.display="none";
    $($("TempC").value).innerHTML=$("CellText").value.replace(/ /g,"&nbsp;&nbsp;");
}
function CheckSelRow(v)
{
    $("ControlDiv").style.display="none";
    if($("curRowIndex").value=="0")
    {
        alert("请选择需要执行操作的行，当行背景颜色发生变化时为选定。");
        return false;
    }
    return v==0?confirm('您确定在此行后插入数据吗？'):confirm('您确定删除此行数据吗？');
}
function SetObjectPos(OParent,offsetObj)
{
    var aTag = document.getElementById(OParent);
    var leftpos=aTag.offsetLeft;
    var toppos=aTag.offsetTop;
    while(aTag = aTag.offsetParent)
    {
        leftpos += aTag.offsetLeft;
        toppos += aTag.offsetTop;
    }
    document.getElementById(offsetObj).style.left=leftpos;
    document.getElementById(offsetObj).style.top=toppos;
}
function _OnSubmit()
{
    $("RowItemText").value="";
    switch($("ReportList").value)
    {
        case "000005":
            //收益分配表
            for(var i=1;i<eval($("RowsCount").value);i++)
            {
                $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(0).innerHTML+"!_1_!";
                $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(1).innerHTML+"!_1_!";
                $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(2).ItemExpr+"!_1_!!_1_!!_1_!!_1_!!_1_!!_0_!";
            }
            break;
        default:
            //默认标准报表
            for(var i=1;i<eval($("RowsCount").value);i++)
            {
                $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(0).innerHTML+"!_1_!";
                $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(1).innerHTML+"!_1_!";
                $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(2).ItemExpr+"!_1_!";
                $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(3).ItemExpr+"!_1_!";
                $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(4).innerHTML+"!_1_!";
                $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(5).innerHTML+"!_1_!";
                $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(6).ItemExpr+"!_1_!";
                $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(7).ItemExpr+"!_0_!";
            }
            break;
    }
    $("RowItemText").value=$("RowItemText").value.replace(/&nbsp;&nbsp;/g," ");
    $("RowItemText").value=$("RowItemText").value.replace(/&nbsp;/g,"");
}
function UpMoveData()
{
    if($("ReportList").value=="000005")
    {
        alert("该报表禁止此操作！");
        return false;
    }
    if($("curRowIndex").value=="0")
    {
        alert("请选择需要执行操作的行，当行背景颜色发生变化时为选定。");
        return false;
    }
    var k=$("MoveDirect_0").checked?0:4;
    var sindex=($("curRowIndex").value=="1")?1:eval($("curRowIndex").value)-1;
    var eindex=eval($("RowsCount").value)-1;
    for(var i=sindex;i<=eindex;i++)
    {
        var row0=$("GridView1").rows.item(i);
        var row1=$("GridView1").rows.item(i+1);
        if(row0&&row1)
        {
            row0.cells.item(0+k).innerHTML=row1.cells.item(0+k).innerHTML;
            row0.cells.item(1+k).innerHTML=row1.cells.item(1+k).innerHTML;
            row0.cells.item(2+k).ItemExpr=row1.cells.item(2+k).ItemExpr;
            row0.cells.item(3+k).ItemExpr=row1.cells.item(3+k).ItemExpr;
        }
    }
    var row2=$("GridView1").rows.item(eindex);
    if(row2)
    {
        row2.cells.item(0+k).innerHTML="";
        row2.cells.item(1+k).innerHTML="";
        row2.cells.item(2+k).ItemExpr="";
        row2.cells.item(3+k).ItemExpr="";
    }
}
function DnMoveData()
{
    if($("ReportList").value=="000005")
    {
        alert("该报表禁止此操作！");
        return false;
    }
    if($("curRowIndex").value=="0")
    {
        alert("请选择需要执行操作的行，当行背景颜色发生变化时为选定。");
        return false;
    }
    var k=$("MoveDirect_0").checked?0:4;
    var sindex=eval($("curRowIndex").value);
    var eindex=eval($("RowsCount").value)-1;
    for(var i=eindex;i>=sindex;i--)
    {
        var row0=$("GridView1").rows.item(i);
        var row1=$("GridView1").rows.item(i-1);
        if(row0&&row1)
        {
            row0.cells.item(0+k).innerHTML=row1.cells.item(0+k).innerHTML;
            row0.cells.item(1+k).innerHTML=row1.cells.item(1+k).innerHTML;
            row0.cells.item(2+k).ItemExpr=row1.cells.item(2+k).ItemExpr;
            row0.cells.item(3+k).ItemExpr=row1.cells.item(3+k).ItemExpr;
        }
    }
    var row2=$("GridView1").rows.item(sindex);
    if(row2)
    {
        row2.cells.item(0+k).innerHTML="";
        row2.cells.item(1+k).innerHTML="";
        row2.cells.item(2+k).ItemExpr="";
        row2.cells.item(3+k).ItemExpr="";
    }
}
document.onclick = function onClick(ev)
{
     ev = ev || window.event;   
     var target = ev.target || ev.srcElement;
     var oid=target.getAttribute("id");
     if(("ControlDiv,CellText,"+$("TempC").value).indexOf(oid)==-1||oid=="")
     {
         if($("ControlDiv").style.display==""){wCellText();}
         $("ControlDiv").style.display="none";
     }
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t3" style="width: 10%; height: 31px; text-align: center">
                    选择报表：</td>
                <td class="t3" style="width: 17%; height: 31px; text-align: center">
                    <asp:DropDownList ID="ReportList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ReportList_SelectedIndexChanged">
                        <asp:ListItem Value="000003">收支明细汇总表</asp:ListItem>
                        <asp:ListItem Value="000004">资产负债汇总表</asp:ListItem>
                        <asp:ListItem Value="000005">收益分配汇总表</asp:ListItem>
                        <asp:ListItem Value="000006">财务公开榜</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t3" style="width: 46%; height: 31px; text-align: center">
                    <asp:Button ID="SaveSheet" runat="server" Text="保存报表" OnClick="SaveSheet_Click" Width="80px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="InsertRow" runat="server" Text="插入行" OnClick="InsertRow_Click" Width="70px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="DeleteRow" runat="server" Text="删除行" OnClick="DeleteRow_Click" Width="70px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="UpdateData" runat="server" Text="取自模板库" OnClick="UpdateData_Click" Width="80px" /></td>
                <td class="t3" style="width: 15%; height: 31px; text-align: center">
                    <asp:RadioButtonList ID="MoveDirect" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">左侧</asp:ListItem>
                        <asp:ListItem Value="1">右侧</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td class="t4" style="width: 12%; height: 31px; text-align: center">
                    <input id="Button1" type="button" value="上移" onclick="UpMoveData()" />
                    <input id="Button2" type="button" value="下移" onclick="DnMoveData()" /></td>
            </tr>
        </table>
        <hr style="width: 750px; color:Red; height:2px; text-align:left" />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span id="ReportTitle" style="color:Blue; font-size: 20pt;" runat="server"></span></td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="width:45%; text-align: left">
                    &nbsp; <span style="color: red">编制单位：</span></td>
                <td style="width:25%; text-align: left">
                    <span style="color: red">报表年月：</span><span style="color: blue"></span></td>
                <td style="width:30%; text-align: right">
                    <span style="color: red">单位：元&nbsp;&nbsp;</span></td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CaptionAlign="Left" CssClass="onlyborder" Width="750px" ShowHeader="False">
            <PagerSettings Visible="False" />
            <RowStyle Font-Size="10pt" Height="21px" />
        </asp:GridView>
        <!--#include file="../AccountQuery/ReportBottom.aspx"-->
    </div>
    <asp:HiddenField ID="RowsCount" runat="server" Value="0" />
    <asp:HiddenField ID="curRowID" runat="server" />
    <asp:HiddenField ID="curRowIndex" runat="server" Value="0" />
    <asp:HiddenField ID="RowItemText" runat="server" />
    <asp:HiddenField ID="AllDesignID" runat="server" />
    <asp:HiddenField ID="TempC" runat="server" />
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    <div id="ControlDiv" style="border-right: red 1px solid; border-top: red 1px solid;
        display: none; z-index: 1; left: 194px; border-left: red 1px solid; width: 156px;
        border-bottom: red 1px solid; position: absolute; top: 298px; height: 23px; background-color: #f6f6f6;
        text-align: center">
        <asp:TextBox ID="CellText" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="152px"></asp:TextBox>
    </div>
    </form>
</body>
</html>
