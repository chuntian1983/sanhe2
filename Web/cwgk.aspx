<%@ Page Language="C#" AutoEventWireup="true" Inherits="cwgk" Codebehind="cwgk.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater ID="Repeater1" runat="server" 
            onitemdatabound="Repeater1_ItemDataBound">
        <ItemTemplate><table><tr><td><%#Eval("UnitName")%></td><td> <asp:DataList  ID="Repeater2" RepeatColumns="6" runat="server" > <ItemTemplate><table><tr><td><a href='AccountQuery/CommReportgk.aspx?DesignID=000006&id=<%#Eval("id")%>'><%#Eval("AccountName")%></a> </td></tr></table></ItemTemplate>
            </asp:DataList>
            </td></tr>
        </table></ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
