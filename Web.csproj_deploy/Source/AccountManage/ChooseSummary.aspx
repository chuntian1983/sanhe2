<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountManage_ChooseSummary" Codebehind="ChooseSummary.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>常用摘要管理</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<base target="_self" />
<script type="text/javascript" id="HideExeScript" src=""></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function WinClose()
{
    window.returnValue=$("Summary").value;
    window.close();
}
function CheckSubmit()
{
    if($("Summary").value=="")
    {
        alert("无摘要信息，存入失败！");
        return false;
    }
    return true;
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
window.onload = resetDialogSize;
</script>
</head>
<body class="margin0">
    <form id="form1" runat="server" onsubmit="return CheckSubmit();">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 400px">
            <tr>
                <td class="t2" style="height: 28px; text-align: center">
                    <asp:TextBox ID="Summary" runat="server" Width="170px"></asp:TextBox>&nbsp;
                    <input id="Button2" onclick="WinClose();" type="button" value="录入摘要" />&nbsp;&nbsp;
                    <asp:Button ID="AddSummary" runat="server" Text="加入摘要库" OnClick="AddSummary_Click" />&nbsp;&nbsp;
                    <input id="Button1" onclick="window.close();" type="button" value="取消" /></td>
            </tr>
            <tr>
                <td class="t4" style="height: 280px; text-align: center">
                <div style="WIDTH: 100%; HEIGHT: 100%; overflow-y:scroll;">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="d4" ShowHeader="False" Width="395px" OnRowDataBound="GridView1_RowDataBound">
                        <PagerSettings Visible="False" />
                        <AlternatingRowStyle BackColor="#F6F6F6" />
                        <RowStyle Font-Size="10pt" Height="21px" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundField DataField="contents" >
                                <ItemStyle Width="350px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Bind("id") %>'
                                        CommandName="btnDelete" OnClick="btnDelete_Click">删除</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">if($("Summary").value==""){$("Summary").value=dialogArguments;}</script>
</body>
</html>
