<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountQuery_GeneralBalance" Codebehind="GeneralBalance.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<style type="text/css" media="print">.Noprint{display:none;}</style> 
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
    function $(o) { return (typeof (o) == "object") ? o : document.getElementById(o); }
    function Number.prototype.str(s) { var a = "" + this; return s.substring(0, s.length - a.length) + a; }
    function OnSelChange(v, t) {
        if (t == "0") {
            $("ReportDate").value = $("ReportDate").value.replace(/\d{4}年/, v + "年");
        }
        else {
            $("ReportDate").value = $("ReportDate").value.replace(/\d{2}月/, v + "月");
        }
    }
    function SelAMonth(v) {
        var a = eval($("QEMonth").value);
        for (var i = $("QEMonth").options.length - 1; i >= 0; i--) {
            $("QEMonth").options.remove(i);
        }
        for (var i = eval(v); i <= 12; i++) {
            $("QEMonth").options.add(new Option(i.str("00") + "月", i.str("00")));
            if (a == i) { $("QEMonth").selectedIndex = $("QEMonth").options.length - 1; }
        }
        SetReportDate();
    }
    function SetReportDate() {
        if ($("QSMonth").value == $("QEMonth").value) {
            $("ReportDate").value = $("SelYear").value + "年" + $("QSMonth").value + "月";
        }
        else {
            $("ReportDate").value = $("SelYear").value + "年" + $("QSMonth").value + "月—" + $("QEMonth").value + "月";
        }
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px" class="Noprint">
            <tr>
                <td class="t1" style="width: 7%; text-align: right">期间：</td>
                <td class="t1" style="width: 27%; text-align: center">
                <asp:DropDownList ID="SelYear" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="QSMonth" runat="server">
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
                </asp:DropDownList>&nbsp; ^&nbsp;
                <asp:DropDownList ID="QEMonth" runat="server">
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
                <td class="t1" style="width: 10%; text-align: right">科目范围：</td>
                <td class="t1" style="width: 27%; text-align: center">
                    <asp:TextBox ID="SubjectNoS" runat="server" Width="79px"></asp:TextBox>&nbsp; ^
                    &nbsp;
                    <asp:TextBox ID="SubjectNoE" runat="server" Width="79px"></asp:TextBox>
                </td>
                <td class="t1" style="width: 7%; text-align: right;">条件：</td>
                <td class="t2" style="width: 22%; text-align: center">
                    <asp:CheckBox ID="isShowDetail" runat="server" Text="显示下级" />
                    <asp:CheckBox ID="isShowZeroSubject" runat="server" Text="显示零项" Checked="True" /></td>
            </tr>
            <tr>
                <td class="t4" colspan="6" style="height: 26px; text-align: center">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查询" Width="200px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input id="Button3" type="button" value="打印报表" style="width:200px" onclick="window.open('../PrintWeb.html','','');" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="OutputDataToExcel" runat="server" OnClick="OutputDataToExcel_Click" Text="导出数据至Excel" Width="120px" /></td>
            </tr>
        </table>
        <hr style="width: 750px; color: red; height: 2px; text-align: left" />
        <!--NoPrintEnd-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="vertical-align:top; height: 235px;">
                    <asp:PlaceHolder ID="ShowPageContent" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="ReportDate" runat="server" />
    <asp:HiddenField ID="RowsCount" runat="server" Value="0" />
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
