<%@ Page Language="C#" AutoEventWireup="true" Inherits="FinanceFlow_SelectAsset" Codebehind="SelectAsset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>选择<%=ShowTypeName %></title>
<base target="_self" />
<style type="text/css">
body{font-size:10pt;}
.t1{border-left: 1px solid #FFCCCC; border-top: 1px solid #FFCCCC;height:22px;}
.t2{border-left: 1px solid #FFCCCC; border-right:1px solid #FFCCCC; border-top:1px solid #FFCCCC;height:22px;}
.t3{border-left: 1px solid #FFCCCC; border-top: 1px solid #FFCCCC; border-bottom: 1px solid #FFCCCC;height:22px;}
.t4{border: 1px solid #FFCCCC;height:22px;}
</style>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function selectFarmer(obj)
{
    window.returnValue=obj;
    window.close();
    return false;
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
    resetDialogSize();
}
</script>
</head>
<body style="text-align:center">
    <form id="form1" runat="server">
    <div>
        <table id="Table1" cellpadding="0" cellspacing="0" style="width: 750px; text-align: center" runat="server">
            <tr>
                <td class="t2" colspan="4" style="height: 26px; background-color: #f6f6f6; text-align: center; font-size:12pt">
                    <%=ShowTypeName %>选择</td>
            </tr>
            <tr>
                <td class="t3" rowspan="2" style="width: 30%; vertical-align:top; text-align:left">
                <div id="DivUnitTree" style="WIDTH: 100%; HEIGHT: 330px; overflow-y:scroll;" runat="server">
                    <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="1" ImageSet="XPFileExplorer"
                        NodeIndent="15" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" ShowLines="True">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" />
                        <Nodes>
                            <asp:TreeNode SelectAction="None" Text="资产"></asp:TreeNode>
                        </Nodes>
                        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                            NodeSpacing="0px" VerticalPadding="2px" />
                    </asp:TreeView>
                </div>
                </td>
                <td class="t1" style="width: 18%; height: 30px;">
                    <%=ShowTypeName %>名称：</td>
                <td class="t1" style="width: 40%; text-align: center; height: 30px;">
                    <asp:TextBox ID="AssetName" runat="server" Width="212px" BorderWidth="1px"></asp:TextBox></td>
                <td class="t2" style="width: 14%; text-align: center; height: 30px;">
                    <asp:Button ID="QFarmer" runat="server" Height="25px" Text="-- 查询 --" Width="80px" OnClick="QFarmer_Click" /></td>
            </tr>
            <tr>
                <td class="t4" colspan="3" style="height:300px; vertical-align:top">
                    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        CaptionAlign="Left"
                        OnRowDataBound="GridView1_RowDataBound"
                        Style="color: navy; margin-top:1px" Width="520px" AllowPaging="True">
                        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
                        <Columns>
                            <asp:BoundField HeaderText="资产编号" DataField="AssetNo" >
                                <ItemStyle Width="120px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="资产名称" DataField="AssetName" >
                                <ItemStyle Width="150px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="总数量" DataField="AAmount" >
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="可用数量" DataField="HasAmount" >
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="选择">
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSelect" runat="server">选择</asp:LinkButton>
                                    <asp:HiddenField ID="AssetModel" runat="server" Value='<%# Eval("AssetModel") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle Font-Size="10pt" />
                        <SelectedRowStyle BackColor="#FFC0C0" BorderColor="Yellow" BorderStyle="Dotted" />
                        <PagerStyle BackColor="White" ForeColor="Olive" />
                        <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" ForeColor="Navy" />
                        <AlternatingRowStyle BackColor="#EBF0F6" />
                        <PagerTemplate>
                            <div style="text-align:left">
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
                            </div>
                        </PagerTemplate>
                    </asp:GridView>&nbsp;
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="FASubjectNo" runat="server" Value="151" />
    </div>
    </form>
</body>
</html>
