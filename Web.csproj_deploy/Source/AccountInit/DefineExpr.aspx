<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountInit_DefineExpr" Codebehind="DefineExpr.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>公式定义</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<style type="text/css">
.Lightbox{BORDER:#fff 1px solid;DISPLAY:block;Z-INDEX:9999;TEXT-ALIGN:center;POSITION:absolute;BACKGROUND-COLOR:#f6f6f6;COLOR:Red;FONT-SIZE:18px;}
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
            url: "../Images/JqueryDir/DoJson.ashx?t=4&f=2&g="+(new Date()).getTime(),
            persist: "cookie",
            cookieId: "TreeViewChooseSubject" });
    });
</script>
<style type="text/css">
html{ overflow-x:hidden; }
a{text-decoration:none;}
a:link{color:#009;}
a:visited{color:#800080;}
a:hover,a:active,a:focus{color:#f00;text-decoration:none;}
.noborder{border-style:none;}
</style>
<script type="text/javascript">
function String.prototype.lenB(){return this.replace(/[^\x00-\xff]/g,"**").length;} 
function $$(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function d(v)
{
    if("+-*/".indexOf($$("AllExprItems").value.substring($$("AllExprItems").value.length-1))==-1)
    {
        $$("AllExprItems").value+=v;
    }
}
function SetC()
{
    if("+-*/".indexOf($$("AllExprItems").value.substring($$("AllExprItems").value.length-1))==-1)
    {
        alert("请选择运算符或输入运算符！");
        return;
    }
    if($$("ConType1").checked){$$("AllExprItems").value+=$$("Constant").value;}
    if($$("ConType2").checked){$$("AllExprItems").value+="本表行列["+$$("RowNo").value+":"+$$("CellNo").value+"]";}
    SetCancel();
}
function DelExprItem()
{
    if($$("AllExprItems").value.length>0)
    {
        if("+-*/".indexOf($$("AllExprItems").value.substring($$("AllExprItems").value.length-1))==-1)
        {
            for(var i=$$("AllExprItems").value.length-1;i>=0;i--)
            {
                if("+-*/".indexOf($$("AllExprItems").value.substring($$("AllExprItems").value.length-1))==-1)
                {
                    $$("AllExprItems").value=$$("AllExprItems").value.substring(0,$$("AllExprItems").value.length-1);
                }
            }
        }
        else
        {
            $$("AllExprItems").value=$$("AllExprItems").value.substring(0,$$("AllExprItems").value.length-1);
        }
    }
}
function SubmitExpr()
{
    if("+-*/".indexOf($$("AllExprItems").value.substring($$("AllExprItems").value.length-1))==-1)
    {
        window.returnValue=$$("AllExprItems").value;
    }
    else
    {
        window.returnValue=$$("AllExprItems").value.substring(0,$$("AllExprItems").value.length-1);
    }
    window.close();
}
function LimitControl(DoOverLay,Overlay,Lightbox)
{
    var aTag=$$(DoOverLay);
    var leftpos=aTag.offsetLeft;
    var toppos=aTag.offsetTop+145;
    var objHeight=aTag.offsetHeight;
    var objWidth=aTag.offsetWidth;
    do{
       aTag = aTag.offsetParent;
       leftpos += aTag.offsetLeft;
       toppos += aTag.offsetTop;
    }while(aTag.tagName!="BODY");
    $$(Overlay).style.left=leftpos;
    $$(Overlay).style.top=toppos;
    $$(Overlay).style.height=objHeight;
    $$(Overlay).style.width=objWidth;
    $$(Lightbox).style.left=leftpos+(objWidth-$$(Lightbox).offsetWidth)/2;
    $$(Lightbox).style.top=toppos+(objHeight-$$(Lightbox).offsetHeight)/2;
}
function ShowConstant()
{
    $$("Overlay").style.display="";
    $$("SetItem").style.display="";
    $$("Constant").value="";
    $$("RowNo").value="";
    if($$('ConType1').checked)
    {
        $$("Constant").focus();
    }
    else
    {
        $$("RowNo").focus();
    }
}
function SetCancel()
{
    $$("Overlay").style.display="none";
    $$("SetItem").style.display="none";
}
function OnTreeClick(v)
{
    if($$("ExprItems").selectedIndex==-1)
    {
        alert("请选择公式项！");
        return;
    }
    if("+-*/".indexOf($$("AllExprItems").value.substring($$("AllExprItems").value.length-1))==-1)
    {
        alert("请选择运算符或输入运算符！");
        return;
    }
    $$("AllExprItems").value+=v+":"+$$("ExprItems").options[$$("ExprItems").selectedIndex].text;
}
function ShowMaskLay()
{
    var aTag=$$("treeContainer");
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
    resetDialogSize();
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 400px">
            <tr>
                <td class="t2" colspan="2" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">公式定义</span>&nbsp;</td>
            </tr>
            <tr>
                <td class="t2" colspan="2" style="width: 400px; height: 29px; text-align: center;">
                    <input id="Button1" type="button" value="+" style="width:40px" onclick="d(this.value);" />&nbsp;
                    <input id="Button2" type="button" value="-" style="width:40px" onclick="d(this.value);" />&nbsp;
                    <input id="Button3" type="button" value="*" style="width:40px" onclick="d(this.value);" />&nbsp;
                    <input id="Button4" type="button" value="/" style="width:40px" onclick="d(this.value);" />&nbsp;&nbsp;
                    <input id="Button5" type="button" value="取常数" style="width:50px" onclick="ShowConstant();" />&nbsp;
                    <input id="Button7" type="button" value="删项" style="width:40px" onclick="DelExprItem();" />&nbsp;
                    <input id="Button6" type="button" value="确定" style="width:40px" onclick="SubmitExpr();" />&nbsp;
                    <input id="Button8" type="button" value="取消" style="width:40px" onclick="window.close();" /></td>
            </tr>
            <tr>
                <td class="t2" colspan="2" style="vertical-align: top;">
                    <asp:TextBox ID="AllExprItems" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="80px" TextMode="MultiLine" Width="396px" ForeColor="blue"></asp:TextBox></td>
            </tr>
            <tr id="DoOverLay">
                <td class="t3" rowspan="2" style="text-align:center; width:400px; height:210px">
                    <div id="treeContainer" style="WIDTH: 100%; HEIGHT: 100%; overflow-y:scroll;">
                        <div style="text-align:left; margin-top:5px; font-size:10pt">
                        <ul class="filetree" style="margin-left:5px">
                        <span class="folder">科目列表</span>
                        <ul id="ajaxTree"></ul>
                        </ul>
                        </div>
                    </div>
                </td>
                <td class="t2" style="text-align: center; height:20px; background:#f6f6f6">公式项</td>
            </tr>
            <tr>
                <td class="t4" style="vertical-align: top; text-align: center">
                    <asp:ListBox ID="ExprItems" runat="server" Height="190px" Width="155px" CssClass="noborder">
                        <asp:ListItem Selected="True">借方金额余额</asp:ListItem>
                        <asp:ListItem>贷方金额余额</asp:ListItem>
                        <asp:ListItem>本月借方金额</asp:ListItem>
                        <asp:ListItem>本月贷方金额</asp:ListItem>
                        <asp:ListItem>月初借方余额</asp:ListItem>
                        <asp:ListItem>月初贷方余额</asp:ListItem>
                        <asp:ListItem>年初借方余额</asp:ListItem>
                        <asp:ListItem>年初贷方余额</asp:ListItem>
                        <asp:ListItem>本年借方累计</asp:ListItem>
                        <asp:ListItem>本年贷方累计</asp:ListItem>
                        <asp:ListItem>月初借方分析余额</asp:ListItem>
                        <asp:ListItem>月初贷方分析余额</asp:ListItem>
                        <asp:ListItem>年初借方分析余额</asp:ListItem>
                        <asp:ListItem>年初贷方分析余额</asp:ListItem>
                        <asp:ListItem>年初明细借方分析余额</asp:ListItem>
                        <asp:ListItem>年初明细贷方分析余额</asp:ListItem>
                        <asp:ListItem>明细科目借方分析余额</asp:ListItem>
                        <asp:ListItem>明细科目贷方分析余额</asp:ListItem>
                    </asp:ListBox></td>
            </tr>
        </table>
    </div>
    <div id="Overlay" class="Overlay"></div>
    <div id="Lightbox" runat="server" class="Lightbox">正在加载，请等待... ...</div>
    <div id="SetItem" class="Lightbox">
        <table cellpadding="0" cellspacing="0" style="width: 400px">
            <tr>
                <td class="t2" colspan="2" style="height: 65px; text-align: center;">
                <span id="PageTitle" style="font-size: 18pt; font-family: 隶书" runat="server">常数项设置</span></td>
            </tr>
            <tr>
                <td class="t2" colspan="1" rowspan="1" style="width: 25%; text-align: center">
                    <asp:RadioButton ID="ConType1" runat="server" Text="常 数 项" Checked="True" GroupName="ConType" />
                    </td>
                <td class="t2" colspan="1" style="width: 75%; height: 35px; text-align: left">
                    常数：<asp:TextBox ID="Constant" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t2" colspan="1" rowspan="1" style="text-align: center">
                    <asp:RadioButton ID="ConType2" runat="server" Text="本表行列" GroupName="ConType" /></td>
                <td class="t2" colspan="1" style="height: 35px; text-align: left;">
                    行次：<asp:TextBox ID="RowNo" runat="server" Width="50px"></asp:TextBox>&nbsp;
                    列次：<asp:DropDownList ID="CellNo" runat="server">
                        <asp:ListItem>01</asp:ListItem>
                        <asp:ListItem Selected="True">02</asp:ListItem>
                        <asp:ListItem>03</asp:ListItem>
                        <asp:ListItem>04</asp:ListItem>
                        <asp:ListItem>05</asp:ListItem>
                        <asp:ListItem>06</asp:ListItem>
                        <asp:ListItem>07</asp:ListItem>
                        <asp:ListItem>08</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t2" colspan="2" style="height: 40px; text-align: center;">
                <input id="Button9" type="button" value="确定" onclick="SetC();" style="width:68px" />&nbsp;&nbsp;&nbsp;&nbsp;
                <input id="CancelSet" type="button" value="取消" onclick="SetCancel();" style="width:68px" /></td>
            </tr>
            <tr>
                <td class="t4" colspan="2" style="height: 30px; text-align: center">
                <span style="font-size: 10pt">注：行次即为表中行次一列中的值，必须一致！</span></td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
        SetCancel();
        $$("AllExprItems").value=dialogArguments;
        LimitControl('DoOverLay','Overlay','SetItem');
    </script>
</body>
</html>
