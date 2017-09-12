<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lblist.aspx.cs" Inherits="SanZi.Web.view.GongKai.lblist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<style type="text/css">
    td{ font-size:14px;}
</style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span style="font-size: 18pt; font-family: 隶书">村务公开列表</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 30px; text-align: center">项目名称</td>
            </tr>
        </table>
        <br />
        <!--NoPrintEnd-->
        
        <asp:DataList ID="DataList1" runat="server" RepeatColumns="6" CellPadding="4" 
            ForeColor="#333333">
            <AlternatingItemStyle BackColor="White" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <ItemStyle BackColor="#EFF3FB" />
            <ItemTemplate>
            <table><tr><td><a href="manage2.aspx?lbid=<%#Eval("id")%>"> <%#Eval("name") %></a></td></tr></table>
                
            </ItemTemplate>
            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </asp:DataList>
        
  
        
      
        </div>
    </form>
</body>
</html>
