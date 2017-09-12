<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lbmanagelist.aspx.cs" Inherits="SanZi.Web.view.GongKai.lbmanagelist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>类别管理列表</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EnableModelValidation="True" onrowcommand="GridView1_RowCommand">
            <Columns>
                <asp:BoundField DataField="name" HeaderText="名称" />
                <asp:TemplateField HeaderText="操作" SortExpression="id">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" OnClientClick="return confirm('你确认要删除吗？')" runat="server" CausesValidation="False" Visible="true" CommandName="Del" CommandArgument='<%# Eval("id") %>'
                                            Text="删除"></asp:LinkButton>
                        
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            
        </asp:GridView>
    </div>
    </form>
</body>
</html>
