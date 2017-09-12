<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cin.aspx.cs" Inherits="yuxi.cin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
<title>-</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body bgcolor="#FFFFFF" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
    <form id="form1" runat="server">
<!-- Save for Web Slices (0025.jpg) -->
<table id="__01" width="980px" height="768" border="0" cellpadding="0" cellspacing="0">
	<tr>
		<td colspan="2" height=183px; width=980px style="background-image:url('images/cin_01.jpg'); text-align:right; ">
			<%--<img src="images/cin_01.jpg" width="1024" height="183" alt="">--%><div style="top:45px; position:relative">
            <asp:ImageButton ID="ImageButton1" runat="server" 
                ImageUrl="images/1_03(2).gif" onclick="ImageButton1_Click" />
            <asp:ImageButton ID="ImageButton2"
                runat="server" ImageUrl="images/1_04.gif" onclick="ImageButton2_Click" /></div></td>
	</tr>
	<tr>
		<td>
        <div style="background-image:url(images/cin_02.jpg); width:220px; height:585px">
            <asp:DataList ID="DataList1" runat="server" Width="210px" 
                onitemcommand="DataList1_ItemCommand">
                <ItemTemplate>
                    <table cellpadding="2" class="style1">
                        <tr>
                            <td><div style="background-image:url(images/011.jpg); width:210px; text-align: center; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: x-large;">
                                <asp:LinkButton ID="LinkButton1" CommandName="view" CommandArgument='<%#Eval("id") %>' runat="server" Font-Underline="False"><%#Eval("name")%></asp:LinkButton></div>
                                &nbsp;</td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </div>
        
			</td>
		<td>
        <div style="background-image:url(images/cin_03.jpg); width:778px; height:585px"><iframe   id="rightFrame"  name="leftFrame" marginWidth="0"  src=<%=strurl %>
										frameBorder="0" noResize style="Z-INDEX: 2;  WIDTH: 778px; HEIGHT: 100%"
										></iframe></div>
			</td>
	</tr>
</table>
<!-- End Save for Web Slices -->
    </form>
</body>
</html>
