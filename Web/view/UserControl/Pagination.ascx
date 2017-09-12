<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pagination.ascx.cs" Inherits="SanZi.Web.UserControl.Pagination" %>
<table cellspacing="0" cellpadding="0" width="680px" align="center" border="0" id="__tb_pager" runat="server">
	<tr>
		<td align="right" valign="bottom" style="height: 25px;width:680px;">
			共 
			<asp:label id="RecCount" Runat="server" ForeColor="red" Text="0"></asp:label>
			条记录 | 页次:
			<asp:Label ID="CurPage" Runat="server" ForeColor="red" Text="0"></asp:Label>/
			<asp:Label id="PagCount" Runat="server" Text="0"></asp:Label>
			<asp:Label id="FirstPageStatic" Runat="server" Text="[最前页]" Visible="false"></asp:Label>
			<asp:Label id="PreviousPageStatic" Runat="server" Text="[上一页]" Visible="false"></asp:Label>
			<asp:linkbutton id="FirstPage" Runat="server" CommandArgument="First" OnClick="PagerButtonClick">[最前页]</asp:linkbutton>
			<asp:linkbutton id="PreviousPage" Runat="server" CommandArgument="Previous" OnClick="PagerButtonClick">[上一页]</asp:linkbutton>
			<asp:linkbutton id="NextPage" Runat="server" CommandArgument="Next" OnClick="PagerButtonClick" Visible="false">[下一页]</asp:linkbutton>
			<asp:linkbutton id="LastPage" Runat="server" CommandArgument="Last" OnClick="PagerButtonClick" Visible="false">[最末页]</asp:linkbutton>
			<asp:Label ID="NextPageStatic" Runat="server" Text="[下一页]"></asp:Label>
			<asp:Label ID="LastPageStatic" Runat="server" Text="[最末页]"></asp:Label>
			转到 <asp:TextBox id="JumpNum" runat="server" Width="30px" style="border:solid 1px #66CCFF;"></asp:TextBox><asp:Button ID="Go" Text="Go" CommandArgument="Jump"  OnClientClick="javascript:checkPageIndex(event,this);" runat="server" OnClick="PagerButtonClick" CssClass="input"/>
		</td>
	</tr>
</table>
<table cellspacing="0" cellpadding="0" width="680px" align="center" border="0" id="__tb_norecord" runat="server" visible="false">
<tr><td><span style="color:red;">暂无任何记录</span></td></tr></table>
<script type="text/javascript">
String.prototype.trim = function(){ return this.replace(/(^\s*)|(\s*$)/g, "");}
function IsNum(key)
{
	return /^\d+$/.test(key);
}
function checkPageIndex(event,obj)
{     
    var index=obj.previousSibling.value.trim();
    if(!IsNum(index))
    {
        alert('必须是0-9之间的数字字符!');
        if (event.preventDefault) 
        {
            event.preventDefault();
            event.stopPropagation();
        } 
        else 
        {
            event.returnValue = false;
            event.cancelBubble = true;
        }
   }
}
</script>
