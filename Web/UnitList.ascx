<%@ Control Language="C#" AutoEventWireup="true" Inherits="UnitList" Codebehind="UnitList.ascx.cs" %>

<div>
    <table cellpadding="0" cellspacing="0" style="width: 203px">
        <tr>
            <td class="t1" style="height: 28px; text-align: center">
                <span style="font-size: 13pt; font-family: 隶书">单位选择</span>&nbsp;</td>
        </tr>
        <tr>
            <td class="t1" style="height: 500px; text-align: left;">
            <div style="WIDTH: 100%; HEIGHT: 100%; overflow-y:scroll;">
                <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="1" ImageSet="XPFileExplorer" NodeIndent="15" ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                    <ParentNodeStyle Font-Bold="False" />
                    <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                    <SelectedNodeStyle BackColor="#F6F6F6" Font-Underline="False" HorizontalPadding="0px"
                        VerticalPadding="0px" BorderStyle="Solid" BorderWidth="1px" />
                    <Nodes>
                        <asp:TreeNode SelectAction="None" Text="一级科目" Value="000"></asp:TreeNode>
                    </Nodes>
                    <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
                </asp:TreeView>
             </div>
            </td>
        </tr>
    </table>
</div>
<asp:HiddenField ID="UnitID" runat="server" Value="000000" />
<asp:HiddenField ID="UnitName" runat="server" />
<asp:HiddenField ID="GAccountList" runat="server" />
<asp:HiddenField ID="TotalLevel" runat="server" Value="0" />
<asp:HiddenField ID="UT" runat="server" />
