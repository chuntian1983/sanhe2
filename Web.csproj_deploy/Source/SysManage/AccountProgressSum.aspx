<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysManage_AccountProgressSum" Codebehind="AccountProgressSum.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function setYear(o,v)
{
    $(o).value=eval($(o).value)+v;
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">做账进度汇总</span></td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="text-align: center; background-color: #f6f6f6;">
                    进度汇总
                </td>
                <td class="t1" style="text-align: center">
                    汇总年度→
                </td>
                <td class="t2" style="text-align: center">
                    <asp:ImageButton ID="SMinus" runat="server" ImageUrl="~/Images/jian.gif" />
                    <asp:TextBox ID="SelYear" runat="server" BorderWidth="0px" Width="27px" Height="18px">2009</asp:TextBox>&nbsp;
                    <asp:ImageButton ID="SPlus" runat="server" ImageUrl="~/Images/jia.gif" />
                </td>
            </tr>
            <tr>
                <td class="t4" colspan="3" style="text-align: center; vertical-align:top">
                    <table cellpadding="0" cellspacing="0" style="width: 640px; margin-top:2px;">
                        <tr>
                            <td class="t2" style="height: 28px; text-align: center; background-color:#f8f8f8;">
                                <span id="ReportTitle" style="color:green; font-size: 12pt;" runat="server"></span></td>
                        </tr>
                    </table>
                    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        CaptionAlign="Left" OnRowDataBound="GridView1_RowDataBound" 
                        Style="color: navy;" Width="640px" EnableModelValidation="True" 
                        onrowcreated="GridView1_RowCreated">
                        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
                        <Columns>
                            <asp:BoundField DataField="townname" HeaderText="镇街名称">
                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="townid" HeaderText="1月">
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="2月">
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="3月">
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="4月">
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="5月">
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="6月">
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="7月">
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="8月">
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="9月">
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="10月">
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="11月">
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="12月">
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="村总数" >
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle Font-Size="10pt" Height="22px" />
                        <PagerStyle BackColor="White" ForeColor="Olive" />
                        <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" ForeColor="Navy" />
                        <AlternatingRowStyle BackColor="#EBF0F6" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="TotalLevel" runat="server" Value="0" />
    </form>
</body>
</html>
