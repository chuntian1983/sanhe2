<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cun.aspx.cs" Inherits="yuxi.cun" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>-</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
            
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    <table id="__01" width="1024" height="768" border="0" cellpadding="0" cellspacing="0">
	<tr>
		<td colspan="3">
			<img src="images/in_01.jpg" width="1024" height="205" alt=""></td>
	</tr>
	<tr>
		<td rowspan="2">
			<img src="images/in_02.jpg" width="84" height="563" alt=""></td>
		<td>
        <div style="background-image:url(images/in_03.jpg);overflow-y:auto; overflow-x:no; width:845px; height:405px;">
        <div>
            <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click" 
                Font-Bold="True" Font-Size="XX-Large" Font-Underline="False">荣成市</asp:LinkButton>
            <asp:Label ID="Label1" runat="server" Text="-&gt;&gt;"></asp:Label>
            <asp:LinkButton ID="LinkButton3" runat="server" Font-Size="XX-Large" 
                onclick="LinkButton3_Click" Font-Underline="False">LinkButton</asp:LinkButton>
            <asp:Label ID="Label2" runat="server" Text="-&gt;&gt;"></asp:Label>
            <asp:LinkButton ID="LinkButton4" runat="server" Font-Size="XX-Large" 
                onclick="LinkButton4_Click" Font-Underline="False">LinkButton</asp:LinkButton>
            </div>
            <asp:DataList ID="DataList1" runat="server" RepeatColumns="3" 
                RepeatDirection="Horizontal" Width="836px" 
                onitemcommand="DataList1_ItemCommand">
                <ItemStyle Font-Size="12px" />
                <ItemTemplate>
                    <table class="style1">
                        <tr>
                            <td  style="height:20px">
                            <div style="padding: 8px 50px 8px 50px; background-position: center center; background-image:url(images/0001.jpg); float: none; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 14px; vertical-align: middle; text-align: center; color: #000000;">
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="view" 
                                    CommandArgument='<%#Eval("id") %>' Font-Underline="False"><%#Eval("name") %></asp:LinkButton></div>
                               </td>
                            
                        </tr>
                        
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </div>
			</td>
		<td rowspan="2">
			<img src="images/in_04.jpg" width="95" height="563" alt=""></td>
	</tr>
	<tr>
		<td>
			<img src="images/in_05.jpg" width="845" height="158" alt=""></td>
	</tr>
</table>
    </div>
    </form>
</body>
</html>
