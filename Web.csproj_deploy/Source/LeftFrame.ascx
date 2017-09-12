<%@ Control Language="C#" AutoEventWireup="true" Inherits="_LeftFrame" Codebehind="LeftFrame.ascx.cs" %>

<script language="JavaScript" type="text/javascript">
<!--
function ChangeUser()
{
    if($("ctl00_LeftFrame1_LPassword").value.length==0)
    {
        $("ctl00_LeftFrame1_LPassword").focus();
        alert("请输入登录密码！");
        return false;
    }
    return true;
}
function ShowVoucher(vid)
{
   window.showModalDialog("Contract/LeaseCardShow.aspx?ctype=0&id="+vid+"&g="+(new Date()).getTime(),"","dialogWidth=770px;dialogHeight=400px;center=yes;");
   return false;
}
function PrintDueCard(ptype,lid,pid)
{
    if(ptype==0)
    {
        window.showModalDialog("Contract/DueNoticeLease.aspx?id="+lid+"&g="+(new Date()).getTime(),"","dialogWidth=650px;dialogHeight:400px;center=yes;");
    }
    else
    {
        window.showModalDialog("Contract/DueNoticePay.aspx?lid="+lid+"&pid="+pid+"&g="+(new Date()).getTime(),"","dialogWidth=650px;dialogHeight:400px;center=yes;");
    }
}
function TableSlect(v)
{
    for(var i=0;i<3;i++)
    {
        if(i==v)
        {
            $("due"+i).style.color="blue";
            $("due"+i).style.backgroundColor="#ffffff";
            $("dueTable"+i).style.display="";
        }
        else
        {
            $("due"+i).style.color="";
            $("due"+i).style.backgroundColor="#f6f6f6";
            $("dueTable"+i).style.display="none";
        }
    }
}
function SetIframeEvent()
{
    var iframe = document.getElementById('ctl00_ContentPlaceHolder1_mFrame');
    iframe.contentWindow.document.oncontextmenu=function(){return false;}
}
window.onload=function()
{
    //window.setInterval("SetIframeEvent()", 200);
}
//-->
</script>
<table cellpadding="0" cellspacing="0" style="width: 203px;">
    <tr>
        <td colspan="2" style="text-align: center; background: #f6f6f6; height: 24px">当前用户信息</td>
    </tr>
    <tr>
        <td class="t1" style="width: 30%; text-align: center;">用户名：</td>
        <td class="t1" style="width: 70%">
            <asp:TextBox ID="TextBox1" runat="server" Width="135px" BorderWidth="0px" ReadOnly="True"></asp:TextBox></td>
    </tr>
    <tr>
        <td class="t1" style="text-align: center">操作员：</td>
        <td class="t1">
        <asp:TextBox ID="TextBox2" runat="server" Width="135px" BorderWidth="0px" ReadOnly="True"></asp:TextBox></td>
    </tr>
    <tr>
        <td class="t1" style="text-align: center; height: 22px;">账套名：</td>
        <td class="t1" style="height: 22px">
        <asp:TextBox ID="TextBox3" runat="server" Width="135px" BorderWidth="0px" ReadOnly="True"></asp:TextBox></td>
    </tr>
    <tr>
        <td class="t1" style="text-align: center">乡镇名：</td>
        <td class="t1">
            <asp:TextBox ID="TextBox4" runat="server" Width="135px" BorderWidth="0px" ReadOnly="True"></asp:TextBox></td>
    </tr>
    <tr>
        <td class="t1" colspan="2" style="text-align: center; background: #f6f6f6">切换账套</td>
    </tr>
    <tr>
        <td class="t1" style="text-align: center">账&nbsp;&nbsp;&nbsp;&nbsp;套：</td>
        <td class="t1" style="text-align: center">
        <asp:DropDownList ID="AccountList" runat="server" Width="140px" AutoPostBack="True" OnSelectedIndexChanged="AccountList_SelectedIndexChanged"></asp:DropDownList></td>
    </tr>
    <tr>
        <td class="t1" colspan="2" style="text-align: center; background: #f6f6f6">切换用户</td>
    </tr>
    <tr>
        <td class="t1" style="text-align: center">操作员：</td>
        <td class="t1" style="text-align: left">
        <asp:DropDownList ID="UserList" runat="server" Width="140px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td class="t1" style="text-align: center">密&nbsp;&nbsp;&nbsp;&nbsp;码：</td>
        <td class="t1" style="text-align: left">
        <asp:TextBox ID="LPassword" runat="server" Width="135px" Height="17px" BorderWidth="1px" TextMode="Password"></asp:TextBox></td>
    </tr>
    <tr>
        <td class="t1" colspan="2" style="height: 33px; text-align: center">
        <asp:Button ID="ChangeUser" runat="server" Text="切换用户" Width="150px" OnClick="ChangeUser_Click" /></td>
    </tr>
    <tr>
        <td class="t1" colspan="2" style="height: 33px; text-align: center;">
        <asp:Label ID="CurAccountDate" runat="server" ForeColor="Blue" Font-Size="11pt" Font-Bold="true"></asp:Label></td>
    </tr>
    <tr>
        <td class="t3" colspan="2" style="height: 191px; text-align: center">
        <script type="text/javascript"> 
        function Year_Month(){ 
        var now = new Date(); 
        var yy = now.getYear(); 
        var mm = now.getMonth()+1; 
        var cl = '<font color="#0000df">'; 
        if (now.getDay() == 0) cl = '<font color="#c00000">'; 
        if (now.getDay() == 6) cl = '<font color="#00c000">'; 
        return(cl + yy + '年' + mm + '月</font>'); } 
        function Date_of_Today(){ 
        var now = new Date(); 
        var cl = '<font color="#ff0000">'; 
        if (now.getDay() == 0) cl = '<font color="#c00000">'; 
        if (now.getDay() == 6) cl = '<font color="#00c000">'; 
        return(cl + now.getDate() + '</font>'); } 
        function Day_of_Today(){ 
        var day = new Array(); 
        day[0] = "星期日"; 
        day[1] = "星期一"; 
        day[2] = "星期二"; 
        day[3] = "星期三"; 
        day[4] = "星期四"; 
        day[5] = "星期五"; 
        day[6] = "星期六"; 
        var now = new Date(); 
        var cl = '<font color="#0000df">'; 
        if (now.getDay() == 0) cl = '<font color="#c00000">'; 
        if (now.getDay() == 6) cl = '<font color="#00c000">'; 
        return(cl + day[now.getDay()] + '</font>'); } 
        function CurentTime(){ 
        var now = new Date(); 
        var hh = now.getHours(); 
        var mm = now.getMinutes(); 
        var ss = now.getTime() % 60000; 
        ss = (ss - (ss % 1000)) / 1000; 
        var clock = hh+':'; 
        if (mm < 10) clock += '0'; 
        clock += mm+':'; 
        if (ss < 10) clock += '0'; 
        clock += ss; 
        return(clock); } 
        function refreshCalendarClock(){ 
        document.all.calendarClock1.innerHTML = Year_Month(); 
        document.all.calendarClock2.innerHTML = Date_of_Today(); 
        document.all.calendarClock3.innerHTML = Day_of_Today(); 
        document.all.calendarClock4.innerHTML = CurentTime(); } 
        document.write('<table cellpadding="0" cellspacing="0" width="150" bgcolor="#f6f6f6" height="157" style="border: 1px solid #ffcccc;">'); 
        document.write('<tr><td align="center" width="100%" height="100%" >【当前系统日期】<br><br>'); 
        document.write('<font id="calendarClock1" style="font-family:宋体;font-size:15pt;line-height:120%"></font><br>'); 
        document.write('<font id="calendarClock2" style="color:#ff0000;font-family:Arial;font-size:30pt;line-height:120%"></font><br>'); 
        document.write('<font id="calendarClock3" style="font-family:宋体;font-size:15pt;line-height:120%"></font><br>'); 
        document.write('<font id="calendarClock4" style="color:#100080;font-family:宋体;font-size:15pt;line-height:120%"></font><br>'); 
        document.write('</td></tr></table>'); 
        setInterval('refreshCalendarClock()',1000); 
        </script>
        </td>
    </tr>
</table><br /><a href="http://www.nongyou.com.cn" target="_blank">技术支持单位：农友软件公司</a>
<div style="position:absolute;right:0;width:300px;height:251px;border:1px solid black;background-color:#f6f6f6;" id="showmsg" visible="false" runat="server">
<table cellpadding="0" cellspacing="0" style="width:300px;text-align:left">
    <tr>
        <td id="due0" style="width:30%;height:30px;text-align:center;border-bottom:1px solid black;border-right:1px solid black;cursor:hand" onclick="TableSlect(0)">【等待收款】</td>
        <td id="due1" style="width:30%;height:30px;text-align:center;border-bottom:1px solid black;border-right:1px solid black;cursor:hand" onclick="TableSlect(1)">【合同到期】</td>
        <td id="due2" style="width:30%;height:30px;text-align:center;border-bottom:1px solid black;border-right:1px solid black;cursor:hand" onclick="TableSlect(2)">【收款到期】</td>
        <td style="width:10%;text-align:center;border-bottom:1px solid black;cursor:hand" onclick="$('ctl00_LeftFrame1_showmsg').style.display='none';">
        <img alt="" src="Images/close.gif" /></td>
    </tr>
    <tr>
        <td colspan="4">
        <div style="width:300px;height:220px;overflow-y:scroll;vertical-align:top">
        <%=DueContracts.ToString() %>
        </div>
        </td>
    </tr>
</table>
</div>
<script type="text/javascript">
function rightBottomAd()
{
    var abc = document.getElementById("ctl00_LeftFrame1_showmsg");
    if(abc)
    {
        abc.style.top = document.documentElement.scrollTop+document.documentElement.clientHeight-abc.offsetHeight+"px"; 
        setTimeout(function(){rightBottomAd();},50);
    }
}
rightBottomAd();
</script>
<asp:Label ID="ExeScript" runat="server" Text="Label"></asp:Label>
