<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountManage_ChooseSubject" Codebehind="ChooseSubject.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>选择科目</title>
<style type="text/css">
.Lightbox{BORDER:#fff 1px solid;DISPLAY:block;Z-INDEX:9999;TEXT-ALIGN:center;POSITION:absolute;BACKGROUND-COLOR:#f8f8f8;COLOR:Blue; FONT-SIZE:18px;height:25px;width:200px}
.Overlay{DISPLAY:block;Z-INDEX:9998;FILTER:alpha(opacity=3);POSITION:absolute;BACKGROUND-COLOR:#000;moz-opacity:0.8;opacity:.80;}
</style>
<link rel="stylesheet" href="../Images/JqueryDir/screen.css" />
<link rel="stylesheet" href="../Images/JqueryDir/jquery.treeview.css" />
<script type="text/javascript" src="../Images/JqueryDir/jquery.js"></script>
<script type="text/javascript" src="../Images/JqueryDir/jquery.cookie.js"></script>
<script type="text/javascript" src="../Images/JqueryDir/jquery.treeview.js"></script>
<script type="text/javascript" src="../Images/JqueryDir/jquery.treeview.async.js"></script>
<script type="text/javascript" language="javascript">
    $(document).ready(function() {
        $("#ajaxTree").treeview({
            url: "../Images/JqueryDir/DoJson.ashx?st="+$("#ShowType input[checked]").val()
                                               +"&qt="+$("#QSubjectType").val()
                                               +"&qn="+$("#QSubjectNo").val()
                                               +"&qm="+encodeURIComponent($("#QSubjectName").val())
                                               +"&qg="+$("#SubjectGroup").val()+"&g="+(new Date()).getTime(),
            persist: "cookie",
            cookieId: "TreeViewChooseSubject" });
    });
</script>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<style type="text/css">
html{ overflow-x:hidden; }
a{text-decoration:none;}
a:link{color:#009;}
a:visited{color:#800080;}
a:hover,a:active,a:focus{color:#f00;text-decoration:none;}
</style>
<base target="_self" />
<script type="text/javascript" id="GetBabalance" src=""></script>
<script type="text/javascript" id="HideExeScript" src=""></script>
<script type="text/javascript">
function $$(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function OpenSelfWin(v)
{
    $$("QSubjectType").value=v;
    __doPostBack('QSubject','');
}
function ShowMaskLay(maindiv)
{
    var aTag=$$(maindiv);
    if(!aTag)
    {
        aTag=$$("treeContainer");
    }
    var oLay=$$("Overlay");
    var lLay=$$("Lightbox");
    var leftpos=aTag.offsetLeft;
    var toppos=aTag.offsetTop;
    var objHeight=aTag.offsetHeight;
    var objWidth=aTag.offsetWidth;
    do{
       aTag = aTag.offsetParent;
       leftpos += aTag.offsetLeft;
       toppos += aTag.offsetTop;
    }while(aTag.tagName!="BODY");
    oLay.style.display="";
    oLay.style.left=leftpos;
    oLay.style.top=toppos;
    oLay.style.height=objHeight;
    oLay.style.width=objWidth;
    lLay.style.display="";
    lLay.style.left=leftpos+(objWidth-lLay.offsetWidth)/2;
    lLay.style.top=toppos+(objHeight-lLay.offsetHeight)/2-18;
}
function CloseMaskLay()
{
    $$("Overlay").style.display="none";
    $$("Lightbox").style.display="none";
}
function OnTreeClick(Name0,Name1,NodeValue)
{
    $$("NodeName0").value=Name0;
    $$("NodeName1").value=Name1;
    if(Name1.length==0)
    {
        $$("SelectSubject").value=Name0;
    }
    else
    {
        $$("SelectSubject").value=Name0+"/"+Name1;
    }
    $$("NodeValue").value=NodeValue;
    if($$("LeadV").value!="")
    {
        $$("NBalance").value="$$('LeadV').value='"+$$('LeadV').value+"';resetFocus();";
    }
    if($$("OnloanV").value!="")
    {
        $$("NBalance").value="$$('OnloanV').value='"+$$('OnloanV').value+"';resetFocus();";
    }
    $$("HideExeScript").src="GetAccountType.aspx?no="+NodeValue+"&g="+(new Date()).getTime();
}
function _Submit(v) {
 
    if($$("NodeValue").value=="")
    {
        alert("请选择科目！");
        return;
    }
    var _Money = "";
    var patrn=/^\d{1,8}(\.\d{1,2})?$/;
    if ($$("LeadV").value!="")
    {
        $$("LeadV").value=parseFloat($$("LeadV").value);
        if(!patrn.test($$("LeadV").value))
        {
          $$("LeadV").focus();
          alert("录入借方金额格式不正确！最多8位整数和2位小数！");
          return;
        }
        _Money = "+" + $$("LeadV").value;
    }
    else if ($$("OnloanV").value!="")
    {
        $$("OnloanV").value=parseFloat($$("OnloanV").value)
        if(!patrn.test($$("OnloanV").value))
        {
          $$("OnloanV").focus();
          alert("录入贷方金额格式不正确！最多8位整数和2位小数！");
          return;
        }
        _Money = "-" + $$("OnloanV").value;
    }
    var row=window.dialogArguments.row;
    if (v == 1) {
     
        var returnV=new Array($$("NodeName0").value,$$("NodeName1").value,$$("NodeValue").value,_Money,0,$$("AccountType").value,$$("SBalance").value,row,window);
        var vid=window.dialogArguments.vid;
        var col=window.dialogArguments.col;
        ShowMaskLay("form1");
        window.dialogArguments.DoSelectSubject(vid,row,col,returnV);
        CloseMaskLay();
        row+=1;
        window.dialogArguments.row=row;
        var pdoc=window.dialogArguments.ParentDoc.getElementById;
        if(!pdoc("v2"+row))
        {
            window.dialogArguments.DoSelectSubject(vid,row,col,returnV);
            window.close();
        }
        if(pdoc("v2"+row).value=="")
        {
            $$("SelectSubject").value="";
            $$("NodeValue").value="";
            $$("NBalance").value="";
            $$("LeadV").value="";
            $$("OnloanV").value="";
        }
        else
        {
            var nbalance=pdoc("v2"+row).value;
            if(nbalance.length>0&&nbalance.substring(0,1)=="-")
            {
                $$("LeadV").value="";
                $$("OnloanV").value=nbalance.substring(1);
            }
            else
            {
                $$("LeadV").value=nbalance.substring(1);
                $$("OnloanV").value="";
            }
            var gcell=window.dialogArguments.GetCellID;
            OnTreeClick(pdoc(gcell(row,1)).innerText,pdoc(gcell(row,2)).innerText,pdoc("v1"+row).value);
        }
        SetInitValue();
        resetFocus();
    }
    else
    {
        window.returnValue=new Array($$("NodeName0").value,$$("NodeName1").value,$$("NodeValue").value,_Money,v,$$("AccountType").value,$$("SBalance").value,row,window);
        window.close();
    }
}
function Body_OnKeydown()
{
    if(event.keyCode==13){_Submit(1);}
    if(event.keyCode==37){_Submit(0);}
    if(event.keyCode==40){_Submit(1);}
    if(event.keyCode==39){window.close();}
}
function NumOp(arg1,arg2,op)
{ 
    var m=Math.pow(10,2);
    return op==0?formatFloat((arg1*m+arg2*m)/m):formatFloat((arg1*m-arg2*m)/m);
}
function formatFloat(src)
{
    return Math.round(src*Math.pow(10, 2))/Math.pow(10, 2);
}
function InitBody()
{
    OnTreeClick($$("NodeName0").value,$$("NodeName1").value,$$("NodeValue").value);
    SetInitValue();
    resetFocus();
}
function SetInitValue()
{
    if($$("LeadV").value==""&&$$("OnloanV").value=="")
    {
        var doc=window.dialogArguments.ParentDoc.getElementById;
        var crow=eval(doc("RowsCount").value)+2;
        if(crow<10){crow="0"+crow;}
        var lead=0;
        var onloan=0;
        for(var i=0;i<eval(doc("RowsCount").value);i++)
        {
            if(doc("v2"+i).value!="")
            {
                if(doc("v2"+i).value.substring(0,1)=="+")
                {
                    lead=NumOp(lead,eval(doc("v2"+i).value.substring(1,doc("v2"+i).value.length)),0);
                }
                if(doc("v2"+i).value.substring(0,1)=="-")
                {
                    onloan=NumOp(onloan,eval(doc("v2"+i).value.substring(1,doc("v2"+i).value.length)),0);
                }
            }
        }
        if(lead!=0&&onloan==0){$$("OnloanV").value=lead;}
        if(lead==0&&onloan!=0){$$("LeadV").value=onloan;}
        if(lead!=0&&onloan!=0&&lead!=onloan)
        {
            if(lead>onloan)
            {
                setFocus("LeadV");
                $$("OnloanV").value=NumOp(lead,onloan,1);
            }
            else
            {
                setFocus("OnloanV");
                $$("LeadV").value=NumOp(onloan,lead,1);
            }
        }
        else
        {
            setFocus("QSubjectNo");
        }
    }
    else
    {
        setFocus("QSubjectNo");
    }
}
function resetFocus()
{
    if($$("NodeValue").value=="")
    {
        setFocus("QSubjectNo");
    }
    else
    {
        if($$("LeadV").value!="")
        {
            setFocus("LeadV");
        }
        if($$("OnloanV").value!="")
        {
            setFocus("OnloanV");
        }
    }
}
function setFocus(cname)
{
    try
    { 
        $$(cname).focus();
        $$(cname).select();
    }
    catch(e){}
}
function resetDialogSize()
{
    var ua = navigator.userAgent;
    if(ua.lastIndexOf("MSIE 7.0") == -1)
    {
        var height = document.body.offsetHeight;
        var width = document.body.offsetWidth;
        if(ua.lastIndexOf("Windows NT 5.2") == -1)
        {
            window.dialogHeight=(height+53)+"px";
            window.dialogWidth=(width+6)+"px";
        }
        else
        {
            window.dialogHeight=(height+46)+"px";
            window.dialogWidth=(width+6)+"px";
        }
    }
}
window.onload = function()
{
    ShowMaskLay();
    InitBody();
    resetDialogSize();
}
</script>
</head>
<body onkeydown="Body_OnKeydown();">
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" style="width: 500px">
            <tr>
                <td class="t2" colspan="5" style="height: 28px; text-align: center;">
                    <span style="font-size: 16pt; font-family: 隶书">选择科目</span>&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="height: 28px; width:15%; text-align: right;">
                    输出类型：</td>
                <td class="t1" style="height: 28px; width:35%">
                    <asp:RadioButtonList ID="ShowType" runat="server" RepeatDirection="Horizontal" Width="165px">
                        <asp:ListItem Selected="True" Value="0">全部</asp:ListItem>
                        <asp:ListItem Value="1">顶级</asp:ListItem>
                        <asp:ListItem Value="2">明细</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td class="t1" style="height: 28px; width:15%; text-align: right;">
                    科目名称：</td>
                <td class="t1" style="height: 28px; width:25%; text-align:center">
                    <asp:TextBox ID="QSubjectName" runat="server" Width="110px"></asp:TextBox></td>
                <td class="t2" style="height: 28px; width:10%; text-align: center;" rowspan="2">
                    <asp:Button ID="QSubject" runat="server" Text="查询" Height="50px" /></td>
            </tr>
            <tr>
                <td class="t1" style="height: 28px; text-align: right">
                    科目分组：</td>
                <td class="t1" style="height: 28px">
                    <asp:DropDownList ID="SubjectGroup" runat="server">
                        <asp:ListItem Value="000000">选择科目分组</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t1" style="height: 28px; text-align: right">
                    科目代码：</td>
                <td class="t1" style="height: 28px; text-align:center">
                    <asp:TextBox ID="QSubjectNo" runat="server" Width="110px"></asp:TextBox></td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="width: 500px">
            <tr>
                <td id="M1" class="t1" style="height: 22px; text-align: center; width:71px; cursor:hand" onclick="OpenSelfWin(1);" runat="server">
                    资产类</td>
                <td id="M2" class="t1" style="height: 22px; text-align: center; width:80px; cursor:hand" onclick="OpenSelfWin(2);" runat="server">
                    负债类</td>
                <td id="M3" class="t1" style="height: 22px; text-align: center; width:89px; cursor:hand" onclick="OpenSelfWin(3);" runat="server">
                    权益类</td>
                <td id="M4" class="t1" style="height: 22px; text-align: center; width:72px; cursor:hand" onclick="OpenSelfWin(4);" runat="server">
                    成本类</td>
                <td id="M5" class="t1" style="height: 22px; text-align: center; width:80px; cursor:hand" onclick="OpenSelfWin(5);" runat="server">
                    损益类</td>
                <td id="M0" class="t2" style="height: 22px; text-align: center; width:88px; cursor:hand" onclick="OpenSelfWin(0);" runat="server">
                    全部</td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="width: 500px">
            <tr>
                <td id="treeContainerTD" class="t2" colspan="5" style="height: 250px;">
                <div id="treeContainer" style="WIDTH: 100%; HEIGHT: 100%; overflow-y:scroll;">
	                <div style="text-align:left; margin-top:5px; font-size:10pt">
	                <ul class="filetree" style="margin-left:5px">
	                <span class="folder">科目列表</span>
	                <ul id="ajaxTree"></ul>
	                </ul>
	                </div>
                </div>
                </td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 28px; text-align: right">选定科目：</td>
                <td class="t2" colspan="4" style="height: 28px; text-align: center;">
                    <asp:TextBox ID="SelectSubject" runat="server" Width="417px" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 28px; width:15%; text-align: right;">
                    借方金额：</td>
                <td class="t1" style="height: 28px; width:35%; text-align:center">
                    <asp:TextBox ID="LeadV" runat="server" Width="166px"></asp:TextBox></td>
                <td class="t1" style="height: 28px; width:15%; text-align: right;">
                    贷方金额：</td>
                <td class="t2" style="height: 28px; width:35%; text-align:center" colspan="2">
                    <asp:TextBox ID="OnloanV" runat="server" Width="165px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t4" colspan="5" style="height: 30px; text-align: center;">
                <input id="Button2" style="width: 110px" type="button" value="确认并关闭" onclick="_Submit(0);" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input id="Button1" style="width: 200px" type="button" value="确认并录入下一分录" onclick="_Submit(1);" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input id="Button3" style="width: 110px" type="button" onclick="window.close();" value="取消并关闭" /></td>
            </tr>
        </table>
        <div id="Overlay" runat="server" class="Overlay"></div>
        <div id="Lightbox" runat="server" class="Lightbox">正在加载，请等待... ...</div>
        <asp:HiddenField ID="NodeValue" runat="server" />
        <asp:HiddenField ID="NodeName0" runat="server" />
        <asp:HiddenField ID="NodeName1" runat="server" />
        <asp:HiddenField ID="SBalance" runat="server" />
        <asp:HiddenField ID="NBalance" runat="server" />
        <asp:HiddenField ID="AccountType" runat="server" Value="0" />
        <asp:HiddenField ID="QSubjectType" runat="server" Value="1" />
        <asp:HiddenField ID="OSubjectType" runat="server" Value="1" />
        <asp:LinkButton ID="btnDoPostBack" runat="server" OnClick="btnDoPostBack_Click"></asp:LinkButton>
        <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
