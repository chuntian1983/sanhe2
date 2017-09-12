<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index2.aspx.cs" Inherits="yuxi.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>-</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
            
        }
        .style2
        {
            width: 977px;
            height: 59px;
        }
        .style3
        {
            width: 2138px;
            height: 59px;
        }
        .style4
        {
            width: 2138px;
            height: 104px;
        }
        .style5
        {
            width: 977px;
            height: 104px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    <table id="__01" width="1024" height="768" border="0" cellpadding="0" cellspacing="0">
	<tr>
		<td colspan="3" background="images/in_01.jpg" width="1024" height="205">
        <table>
        <tr>
        <td class="style3"></td>
        <td class="style2"></td>
        </tr>
        <tr>
        <td class="style4"></td>
        <td class="style5">
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Images/sy01.jpg" 
                onclick="ImageButton1_Click" />&nbsp;
            <asp:ImageButton ID="ImageButton2"
                runat="server" ImageUrl="Images/sy02.jpg" onclick="ImageButton2_Click" /></td>
        </tr>
        </table>
			</td>
	</tr>
	<tr>
		<td rowspan="2">
			<img src="images/in_02.jpg" width="84" height="563" alt=""></td>
		<td>
        <div style="background-image:url(images/in_03.jpg); width:845px; height:405px;">
        <div>
            <asp:LinkButton ID="LinkButton2" runat="server" Font-Bold="True" 
                Font-Size="XX-Large" onclick="LinkButton2_Click" Font-Underline="False">环翠区</asp:LinkButton>
            </div>
            <asp:DataList ID="DataList1" runat="server" RepeatColumns="3" 
                RepeatDirection="Horizontal" Width="836px" 
                onitemcommand="DataList1_ItemCommand">
                <ItemTemplate>
                    <table class="style1">
                        <tr>
                            <td  style="height:20px">
                            <div style="padding: 8px 50px 8px 50px; background-position: center center; background-image:url(images/0001.jpg); float: none; font-family: 宋体, Arial, Helvetica, sans-serif; font-size:16px; vertical-align: middle; text-align: center; color: #000000;">
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="view" Width="98%" 
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
