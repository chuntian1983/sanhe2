<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addmoney.aspx.cs" Inherits="SanZi.Web.AccountManage.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <style type="text/css">
        .table{
	border: 1px solid #939393;
	margin-top: 5px;
	margin-bottom: 5px;
	width:550px;
	
}
td {
	color:#333333;
	font-size:12px;
    line-height: 18px;
}
        .style1
        {
            width:25%;
        }
        .style2
        {
            width: 20%;
        }
        .style3
        {
            width: 15%;
        }
        .style4
        {
            width: 18%;
        }
        input{ width:80px;}
    </style>
</head>
<body   onload= "if(window!=top)parent.location.reload() "> 

    <form id="frmScanBarCode" method="post" runat="server" target="meizz">
    <iframe   name=meizz   width=0   height=0   frameborder=0   style="display:none"></iframe>
    <div>
    <table id="myGrid" border='1' class="table"> 
    <tr style="text-align:center">   
<td class="style3">摘要</td><td class="style2">
    总账科目</td><td class="style1">
    明细科目</td><td class="style4">
    借方</td><td style="width:50px">贷方</td>   
</tr>   
<tr>   
<td class="style3">
    <asp:TextBox ID="TextBox1"  runat="server"></asp:TextBox></td>
    <td class="style2">
    <asp:Label ID="Label1" runat="server"  Text=" "  ></asp:Label></td>
    <td class="style1">
        <asp:Label ID="Label11" runat="server"  Text=" " ></asp:Label></td>
    <td class="style4">
    <asp:TextBox ID="TextBox12" runat="server"  ></asp:TextBox></td><td>
        <asp:TextBox ID="TextBox13"  runat="server"></asp:TextBox></td>   
</tr>   
<tr>   
<td class="style3">
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td><td class="style2">
    <asp:Label ID="Label2" runat="server" Text=" "  >   </asp:Label></td>
    <td class="style1">
        <asp:Label ID="Label22" runat="server" Text=" "  ></asp:Label></td>
    <td class="style4">
    <asp:TextBox ID="TextBox22" runat="server"></asp:TextBox></td><td>
        <asp:TextBox ID="TextBox23" runat="server"></asp:TextBox></td>   
</tr>  
<tr>   
<td class="style3">
    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td><td class="style2">
    <asp:Label ID="Label3" runat="server" Text=" " ></asp:Label></td>
    <td class="style1">
        <asp:Label ID="Label32" runat="server" Text=" " ></asp:Label></td>
    <td class="style4">
    <asp:TextBox ID="TextBox32" runat="server"></asp:TextBox></td><td>
        <asp:TextBox ID="TextBox33" runat="server"></asp:TextBox></td>   
</tr>  
<tr>   
<td class="style3">
    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td><td class="style2">
    <asp:Label ID="Label4" runat="server" Text=" " ></asp:Label></td>
    <td class="style1">
        <asp:Label ID="Label42" runat="server" Text=" " ></asp:Label></td>
    <td class="style4">
    <asp:TextBox ID="TextBox42" runat="server"></asp:TextBox></td><td>
        <asp:TextBox ID="TextBox43" runat="server"></asp:TextBox></td>   
</tr>  
<tr>   
<td class="style3">
    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td><td class="style2">
    <asp:Label ID="Label5" runat="server"  Text=" "></asp:Label></td>
    <td class="style1">
        <asp:Label ID="Label52" runat="server" Text=" " ></asp:Label></td>
    <td class="style4">
    <asp:TextBox ID="TextBox52" runat="server"></asp:TextBox></td><td>
        <asp:TextBox ID="TextBox53" runat="server"></asp:TextBox></td>   
</tr>  
<tr>   
<td class="style3">
    <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td><td class="style2">
    <asp:Label ID="Label6" runat="server" Text=" " ></asp:Label></td>
    <td class="style1">
        <asp:Label ID="Label62" runat="server"  Text=" "></asp:Label></td>
    <td class="style4">
    <asp:TextBox ID="TextBox62" runat="server"></asp:TextBox></td><td>
        <asp:TextBox ID="TextBox63" runat="server"></asp:TextBox></td>   
</tr>  
<tr>   
<td class="style3">
    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox></td><td class="style2">
    <asp:Label ID="Label7" runat="server"  Text=" "></asp:Label></td>
    <td class="style1">
        <asp:Label ID="Label72" runat="server" Text=" " ></asp:Label></td>
    <td class="style4">
    <asp:TextBox ID="TextBox72" runat="server"></asp:TextBox></td><td>
        <asp:TextBox ID="TextBox73" runat="server"></asp:TextBox></td>   
</tr>  
<tr>   
<td class="style3">
    <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox></td><td class="style2">
    <asp:Label ID="Label8" runat="server"  Text=" "></asp:Label></td>
    <td class="style1">
        <asp:Label ID="Label82" runat="server" Text=" " ></asp:Label></td>
    <td class="style4">
    <asp:TextBox ID="TextBox82" runat="server"></asp:TextBox></td><td>
        <asp:TextBox ID="TextBox83" runat="server"></asp:TextBox></td>   
</tr>  
</table>  
<script language="JavaScript">
<!--

    var dd = document.getElementById("myGrid").getElementsByTagName("input");
    for (var i = 0; i < dd.length; i++) {
        dd[i].onkeydown = four(i);
    } //for
    function four_s(d, ojb) {
        var r = ojb.createTextRange();
        r.moveStart('character', d);
        r.collapse(true);
        r.select();
    }
    function four(d) {
        return function () {
            var iekey = event.keyCode;
            if (iekey == 38) {
                //                var a = (12 + d + (iekey - 39) * 3) % 12;
                var bb = d - 3;
                if (bb >= 0) {


                    //                dd[a].focus();
                    dd[bb].focus();
                }
            }
            if (iekey == 40) {
                var bbb = d + 3;
                if (bbb < dd.length) {


                    dd[bbb].focus();
                }

            }
            if (iekey == 37) {
                var bbb = d -1;
                if (bbb>=0&&bbb <= dd.length) {


                    dd[bbb].focus();
                }

            }
            if (iekey == 39) {
                var bbb = d + 1;
                if (bbb>=0&&bbb < dd.length) {


                    dd[bbb].focus();
                }

            }
            if (iekey == 13) {
               var bbb = d + 3;
                if (bbb < dd.length) {


                    dd[bbb].focus();
                }

            }
            //if
//            if (iekey == 37 || iekey == 39) {
//                var sel = document.selection.createRange();
//                sel.setEndPoint("StartToStart", dd[d].createTextRange())
//                var s = sel.text.length
//                if (iekey == 37 && s == 0) {
//                    var a = (d - 1) % 12;
//                    dd[a].focus();
//                    four_s(dd[a].value.length, dd[a])
//                    return false;
//                } //if
//                if (iekey == 39 && s == dd[d].value.length) {
//                    var a = (d + 1) % 12;
//                    dd[a].focus();
//                    four_s(0, dd[a])
//                    return false;
//                } //if
//            } //if
        }
    }
//-->
    function fu() {

    var patrn=/^\d{1,8}(\.\d{1,2})?$/;
    if (document.getElementById('TextBox12').value!="")
    {
        document.getElementById('TextBox12').value=parseFloat(document.getElementById('TextBox12').value);
        if(!patrn.test(document.getElementById('TextBox12').value))
        {
          document.getElementById('TextBox12').focus();
          alert("录入借方金额格式不正确！最多8位整数和2位小数！");
          return false;
        }
        
    }
    if (document.getElementById('TextBox22').value!="")
    {
        document.getElementById('TextBox22').value=parseFloat(document.getElementById('TextBox22').value);
        if(!patrn.test(document.getElementById('TextBox22').value))
        {
          document.getElementById('TextBox22').focus();
          alert("录入借方金额格式不正确！最多8位整数和2位小数！");
          return false;
        }
        
    }
    if (document.getElementById('TextBox32').value!="")
    {
        document.getElementById('TextBox32').value=parseFloat(document.getElementById('TextBox32').value);
        if(!patrn.test(document.getElementById('TextBox12').value))
        {
          document.getElementById('TextBox32').focus();
          alert("录入借方金额格式不正确！最多8位整数和2位小数！");
          return false;
        }
        
    }
    if (document.getElementById('TextBox42').value!="")
    {
        document.getElementById('TextBox42').value=parseFloat(document.getElementById('TextBox42').value);
        if(!patrn.test(document.getElementById('TextBox42').value))
        {
          document.getElementById('TextBox42').focus();
          alert("录入借方金额格式不正确！最多8位整数和2位小数！");
          return false;
        }
        
    }
    if (document.getElementById('TextBox52').value!="")
    {
        document.getElementById('TextBox52').value=parseFloat(document.getElementById('TextBox52').value);
        if(!patrn.test(document.getElementById('TextBox52').value))
        {
          document.getElementById('TextBox52').focus();
          alert("录入借方金额格式不正确！最多8位整数和2位小数！");
          return false;
        }
        
    }
    if (document.getElementById('TextBox62').value!="")
    {
        document.getElementById('TextBox62').value=parseFloat(document.getElementById('TextBox62').value);
        if(!patrn.test(document.getElementById('TextBox62').value))
        {
          document.getElementById('TextBox62').focus();
          alert("录入借方金额格式不正确！最多8位整数和2位小数！");
          return false;
        }
        
    }
    if (document.getElementById('TextBox72').value!="")
    {
        document.getElementById('TextBox72').value=parseFloat(document.getElementById('TextBox72').value);
        if(!patrn.test(document.getElementById('TextBox72').value))
        {
          document.getElementById('TextBox72').focus();
          alert("录入借方金额格式不正确！最多8位整数和2位小数！");
          return false;
        }
        
    }
    if (document.getElementById('TextBox82').value!="")
    {
        document.getElementById('TextBox82').value=parseFloat(document.getElementById('TextBox82').value);
        if(!patrn.test(document.getElementById('TextBox82').value))
        {
          document.getElementById('TextBox82').focus();
          alert("录入借方金额格式不正确！最多8位整数和2位小数！");
          return false;
        }
        
    }
if (document.getElementById('TextBox13').value!="")
    {
        document.getElementById('TextBox13').value=parseFloat(document.getElementById('TextBox13').value);
        if(!patrn.test(document.getElementById('TextBox13').value))
        {
          document.getElementById('TextBox13').focus();
          alert("录入贷方金额格式不正确！最多8位整数和2位小数！");
          return false;
        }
        
    }
    if (document.getElementById('TextBox23').value!="")
    {
        document.getElementById('TextBox23').value=parseFloat(document.getElementById('TextBox23').value);
        if(!patrn.test(document.getElementById('TextBox23').value))
        {
          document.getElementById('TextBox23').focus();
          alert("录入贷方金额格式不正确！最多8位整数和2位小数！");
          return false;
        }
        
    }
    if (document.getElementById('TextBox33').value!="")
    {
        document.getElementById('TextBox33').value=parseFloat(document.getElementById('TextBox33').value);
        if(!patrn.test(document.getElementById('TextBox33').value))
        {
          document.getElementById('TextBox33').focus();
          alert("录入贷方金额格式不正确！最多8位整数和2位小数！");
          return false;
        }
        
    }
    if (document.getElementById('TextBox43').value!="")
    {
        document.getElementById('TextBox43').value=parseFloat(document.getElementById('TextBox43').value);
        if(!patrn.test(document.getElementById('TextBox43').value))
        {
          document.getElementById('TextBox43').focus();
          alert("录入贷方金额格式不正确！最多8位整数和2位小数！");
          return false;
        }
        
    }
    if (document.getElementById('TextBox53').value!="")
    {
        document.getElementById('TextBox53').value=parseFloat(document.getElementById('TextBox53').value);
        if(!patrn.test(document.getElementById('TextBox53').value))
        {
          document.getElementById('TextBox53').focus();
          alert("录入贷方金额格式不正确！最多8位整数和2位小数！");
          return false;
        }
        
    }
    if (document.getElementById('TextBox63').value!="")
    {
        document.getElementById('TextBox63').value=parseFloat(document.getElementById('TextBox63').value);
        if(!patrn.test(document.getElementById('TextBox63').value))
        {
          document.getElementById('TextBox63').focus();
          alert("录入贷方金额格式不正确！最多8位整数和2位小数！");
          return false;
        }
        
    }
    if (document.getElementById('TextBox73').value!="")
    {
        document.getElementById('TextBox73').value=parseFloat(document.getElementById('TextBox73').value);
        if(!patrn.test(document.getElementById('TextBox73').value))
        {
          document.getElementById('TextBox73').focus();
          alert("录入贷方金额格式不正确！最多8位整数和2位小数！");
          return false;
        }
        
    }
    if (document.getElementById('TextBox83').value!="")
    {
        document.getElementById('TextBox83').value=parseFloat(document.getElementById('TextBox83').value);
        if(!patrn.test(document.getElementById('TextBox83').value))
        {
          document.getElementById('TextBox83').focus();
          alert("录入贷方金额格式不正确！最多8位整数和2位小数！");
          return false;
        }
        
    }
    

        var info='';
        var je=Array();
        var count=<%=count %>;
        for (var i = 0; i < count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (document.getElementById('TextBox12').value.length==0)
                            {
                                money = "-" + document.getElementById('TextBox13').value;
                            }
                            else { money ="+"+ document.getElementById('TextBox12').value; }
                            info +=document.getElementById('TextBox1').value + "!__!" +document.getElementById('Hidden1').value + "!__!" + money + "!--!";
                            je.push(money);
                            break;
                            case 1:
                            if (document.getElementById('TextBox22').value.length==0)
                            {
                                money = "-" + document.getElementById('TextBox23').value;
                            }
                            else { money ="+"+ document.getElementById('TextBox22').value; }
                            info +=document.getElementById('TextBox2').value + "!__!" +document.getElementById('Hidden2').value + "!__!" + money + "!--!";
                            je.push(money);
                            break;
                            case 2:
                            if (document.getElementById('TextBox32').value.length==0)
                            {
                                money = "-" + document.getElementById('TextBox33').value;
                            }
                            else { money ="+"+ document.getElementById('TextBox32').value; }
                            info +=document.getElementById('TextBox3').value + "!__!" +document.getElementById('Hidden3').value + "!__!" + money + "!--!";
                            je.push(money);
                            break;
                            case 3:
                            if (document.getElementById('TextBox42').value.length==0)
                            {
                                money = "-" + document.getElementById('TextBox43').value;
                            }
                            else { money ="+"+ document.getElementById('TextBox42').value; }
                            info +=document.getElementById('TextBox4').value + "!__!" +document.getElementById('Hidden4').value + "!__!" + money + "!--!";
                            je.push(money);
                            break;
                            case 4:
                            if (document.getElementById('TextBox52').value.length==0)
                            {
                                money = "-" + document.getElementById('TextBox53').value;
                            }
                            else { money ="+"+ document.getElementById('TextBox52').value; }
                            info +=document.getElementById('TextBox5').value + "!__!" +document.getElementById('Hidden5').value + "!__!" + money + "!--!";
                            je.push(money);
                            break;
                        case 5:
                            if (document.getElementById('TextBox62').value.length==0)
                            {
                                money = "-" + document.getElementById('TextBox63').value;
                            }
                            else { money ="+"+ document.getElementById('TextBox62').value; }
                            info +=document.getElementById('TextBox6').value + "!__!" +document.getElementById('Hidden6').value + "!__!" + money + "!--!";
                            je.push(money);
                            break;
                            case 6:
                            if (document.getElementById('TextBox72').value.length==0)
                            {
                                money = "-" + document.getElementById('TextBox73').value;
                            }
                            else { money ="+"+ document.getElementById('TextBox72').value; }
                            info +=document.getElementById('TextBox7').value + "!__!" +document.getElementById('Hidden7').value + "!__!" + money + "!--!";
                            je.push(money);
                            break;
                            case 7:
                            if (document.getElementById('TextBox82').value.length==0)
                            {
                                money = "-" + document.getElementById('TextBox83').value;
                            }
                            else { money ="+"+ document.getElementById('TextBox82').value; }
                            info +=document.getElementById('TextBox8').value + "!__!" +document.getElementById('Hidden8').value + "!__!" + money + "!--!";
                            je.push(money);
                            break;
                        
                    }
                }
                
        window.returnValue = info;
        
        window.close();
    }
</script> 
    
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="确定" 
        Visible="False" />
    <input id="Hidden1" runat=server type="hidden" />
    <input id="Hidden2" runat=server type="hidden" />
    <input id="Hidden3" runat=server type="hidden" />
    <input id="Hidden4" runat=server type="hidden" />
    <input id="Hidden5" runat=server type="hidden" />
    <input id="Hidden6" runat=server type="hidden" />
    <input id="Hidden7" runat=server type="hidden" />
    <input id="Hidden8" runat=server type="hidden" />
    <input id="Button2" type="button" value=" 确 定 " onclick="fu();" /></div></form>
</body>
</html>
