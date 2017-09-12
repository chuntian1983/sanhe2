<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" Inherits="AccountCollect_MonitorFixedAsset" Codebehind="MonitorFixedAsset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if($("GAccountList").value=="")
    {
      alert("没有可监控单位！");
      return false;
    }
    return true;
}
function setYear(o,v)
{
    var m=eval($(o).value)+v;
    $(o).value=m;
    $("ReportDate").value=$("ReportDate").value.replace(/\d{4}年/,m+"年");
    return false;
}
function setMonth(v)
{
    var rdate=$("ReportDate").value;
    if(v.length==0)
    {
        $("ReportDate").value=rdate.replace(/\d{0,2}月/,"");
        return;
    }
    if(rdate.indexOf("月")==-1)
    {
        $("ReportDate").value=$("ReportDate").value+v+"月";
    }
    else
    {
        $("ReportDate").value=$("ReportDate").value.replace(/\d{2}月/,v+"月");
    }
}
function ShowVoucher(vid,aid)
{
   window.showModalDialog("../FixedAsset/PrintFACard.aspx?id="+vid+"&aid="+aid+"&g="+(new Date()).getTime(),"","dialogWidth=770px;dialogHeight=420px;center=yes;");
   return false;
}
function SelectUnit()
{
    var returnV=window.showModalDialog("UnitList.aspx?g="+(new Date()).getTime(),"","dialogWidth=200px;dialogHeight=380px;center=yes;");
    if(typeof(returnV)!="undefined")
    {
        if(returnV[0].length>0)
        {
            $("UnitName").value=returnV[0];
            $("AName").innerText=returnV[0];
            $("GAccountList").value=returnV[1];
        }
    }
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span id="ReportTitle" style="color:green; font-size: 16pt;" runat="server">资产监管</span></td>
            </tr>
        </table>
        <br />
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 10%; height: 30px; text-align: center">
                    监控单位：</td>
                <td class="t1" colspan="2" style="width: 30%; text-align: left">
                    &nbsp;&nbsp;
                    <asp:TextBox ID="UnitName" runat="server" Width="150px"></asp:TextBox></td>
                <td class="t1" style="width: 8%; text-align: center">
                    年月：</td>
                <td class="t1" style="width: 22%; text-align: center">
                    <asp:ImageButton ID="SMinus" runat="server" ImageUrl="~/Images/jian.gif" />
                    <asp:TextBox ID="SelYear" runat="server" BorderWidth="0px" Width="27px" Height="18px">2009</asp:TextBox>&nbsp;
                    <asp:ImageButton ID="SPlus" runat="server" ImageUrl="~/Images/jia.gif" />
                    <asp:DropDownList ID="SelMonth" runat="server">
                        <asp:ListItem Value="">不限</asp:ListItem>
                        <asp:ListItem Value="01">01月</asp:ListItem>
                        <asp:ListItem Value="02">02月</asp:ListItem>
                        <asp:ListItem Value="03">03月</asp:ListItem>
                        <asp:ListItem Value="04">04月</asp:ListItem>
                        <asp:ListItem Value="05">05月</asp:ListItem>
                        <asp:ListItem Value="06">06月</asp:ListItem>
                        <asp:ListItem Value="07">07月</asp:ListItem>
                        <asp:ListItem Value="08">08月</asp:ListItem>
                        <asp:ListItem Value="09">09月</asp:ListItem>
                        <asp:ListItem Value="10">10月</asp:ListItem>
                        <asp:ListItem Value="11">11月</asp:ListItem>
                        <asp:ListItem Value="12">12月</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t1" style="width: 8%; text-align: center">
                    &nbsp;指标：</td>
                <td class="t2" style="width: 22%; text-align: center">
                    <asp:TextBox ID="IndexValue" runat="server">0</asp:TextBox></td>
            </tr>
            <tr>
                <td class="t3" style="height: 30px; text-align: center">
                    资源类别：</td>
                <td class="t3" colspan="2" style="text-align: left">
                    &nbsp;&nbsp;
                    <asp:DropDownList ID="QList" runat="server">
                    </asp:DropDownList></td>
                <td class="t4" colspan="5" style="text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查询" Width="100px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" onclick="window.open('../PrintWeb.html','','');" type="button" value="打印报表" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="OutputDataToExcel" runat="server" OnClick="OutputDataToExcel_Click" Text="导出数据至Excel" Width="120px" /></td>
            </tr>
        </table>
        <br />
        <!--NoPrintEnd-->
        <table cellpadding="0" cellspacing="0" style="width: 750px; font-size:10pt;">
            <tr>
                <td style="width:41%; text-align: left">
                    &nbsp;编制单位：<asp:Label ID="AName" runat="server"></asp:Label></td>
                <td style="width:29%; text-align: left">
                    报表年月：<asp:TextBox ID="ReportDate" runat="server" BorderWidth="0px" Width="70px"></asp:TextBox></td>
                <td style="width:30%; text-align: right">
                    单位：元&nbsp;&nbsp;</td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" Height="1px" OnRowDataBound="GridView1_RowDataBound"
            PageSize="18" Style="color: navy" Width="750px">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="accountname" HeaderText="账套名称">
                    <ItemStyle Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="assetno" HeaderText="资产编号">
                    <ItemStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="assetname" HeaderText="资产名称">
                    <ItemStyle Width="170px" />
                </asp:BoundField>
                <asp:BoundField DataField="oldprice" HeaderText="原值" DataFormatString="{0:f}" HtmlEncode="False">
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="累计折旧" DataField="zhejiu" DataFormatString="{0:f}" HtmlEncode="False">
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="净残值" DataField="jingcz" DataFormatString="{0:f}" HtmlEncode="False">
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="折旧率" DataField="monthzjl">
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="本月折旧" DataField="thiszj" DataFormatString="{0:f}" HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Right" Width="70px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="查看">
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnShow" runat="server" CommandArgument='<%# Bind("id") %>' CommandName='<%# Bind("accountid") %>' OnClick="btnShow_Click">卡片</asp:LinkButton>
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
        <!--#include file="../AccountQuery/ReportBottom.aspx"-->
    </div>
    <asp:HiddenField ID="GAccountList" runat="server" />
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
