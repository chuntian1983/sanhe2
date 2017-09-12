<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportData.aspx.cs" Inherits="SanZi.Web.ResManage.ImportData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" border="1" style="width: 750px; margin-top:20px">
            <tr>
                <td class="t1" style="height: 33px; text-align: center; font-size:15px" 
                    colspan="2">
                    农村集体经济组织清产核资数据导入
                </td>
            </tr>
            <tr>
                <td class="t1" style="height: 33px; text-align: center; width:15%">
                    导入参数：</td>
                <td class="t2" style="height: 33px; width:85%; text-align:left;">
                    <div style="margin-top:5px; margin-bottom:5px">
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="自动创建资产明细科目" />
                    <br />
                    <span style="color:red; font-size:9pt">
                    注：该参数仅对表四、五有效，如果勾选，则自动按照资产类别在相应科目下创建资产明细科目，如果不勾选，则把所有资产关联到相应科目下属其他科目中。</span>
                    <br />
                    <asp:CheckBox ID="CheckBox2" runat="server" Text="导入期初余额" />
                    <br />
                    <span style="color:red; font-size:9pt">
                    注：该参数仅对表十有效，如果勾选，则会把清查核实金额作为建账期初余额，特别提醒，仅当当前账套尚未启用时使用该功能。</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="t1" style="height: 33px; text-align: center; width:15%">
                    数据样表：</td>
                <td class="t2" style="height: 33px; width:85%; text-align:left">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="3" 
                        RepeatDirection="Horizontal" Width="605px" Style="margin-left:6px">
                        <asp:ListItem Value="3" Selected="True">（表四）固定资产（建筑）</asp:ListItem>
                        <asp:ListItem Value="4">（表五）固定资产（设备）</asp:ListItem>
                        <asp:ListItem Value="9">（表十）债权</asp:ListItem>
                        <asp:ListItem Value="11">（表十二）资源</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="t3" style="height: 33px; text-align: center; width:15%">
                上传数据：</td>
                <td class="t4" style="height: 33px; width:85%; text-align: center">
                <asp:FileUpload ID="FileUpload1" runat="server" Width="505px" unselectable="on" />&nbsp;&nbsp;
                <asp:Button ID="ImportExcelData" runat="server" Text="上传" Width="97px" OnClick="ImportExcelData_Click" /></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
