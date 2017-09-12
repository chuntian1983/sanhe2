<%@ Page Language="C#" AutoEventWireup="true" Inherits="BidManage_FileList" Codebehind="FileList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="False" AllowSorting="True" Style="color:navy" Width="530px"
            AutoGenerateColumns="False" CaptionAlign="Left" OnRowDataBound="GridView1_RowDataBound">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="FileName" HeaderText="文件名">
                    <ItemStyle Width="350px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="UploadTime" HeaderText="上传时间">
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="操作">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandName='<%# Bind("id") %>' CommandArgument='<%# Bind("FilePath") %>' OnClick="btnDelete_Click">删除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle Font-Size="10pt" Height="22px" />
            <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" Height="25px" ForeColor="Navy" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
