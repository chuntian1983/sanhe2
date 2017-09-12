<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConditionSet.aspx.cs" Inherits="SanZi.Web.pibanka.ConditionSet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>村财务支出金额设置</title>
    <link href="../../Style.css" type="text/css" rel="stylesheet"/>
    <script type="text/javascript" language="javascript" src="../js/functionforjs.js"></script>
    <script type="text/javascript">
        function sel() {
            var v = window.showModalDialog("../../ChooseAccount.aspx?t=1&g=" + (new Date()).getTime(), "", "dialogWidth=770px;dialogHeight=480px;center=yes;");
            if (v && v.length > 0) {
                document.getElementById("AccountID").value = v;
                __doPostBack('LinkButton1', '');
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="AccountID" runat="server" />
    <div align="center">
         <div style="height:35px"><input id="Button1" type="button" value="选择设置账套" onclick="sel()" /><asp:LinkButton 
                 ID="LinkButton1" runat="server" onclick="LinkButton1_Click"></asp:LinkButton>
         </div>
         <table id="setm" runat="server" style="width:650px;border:1px solid #000800;margin-top:5px;background:#000800;" cellspacing="1" cellpadding="3" visible="false">
            <tr style="background-color:#f8f8f8">
                <td class="tableTitle2" colspan="3">
                村财务支出金额设置（账套：<asp:Label ID="aname" runat="server"></asp:Label>）
                <input type="hidden" name="hidDeptID" id="hidDeptID" value="1"/></td>
            </tr>
            <tr style="background-color:#ffffff;height:35px;">
                <td class="tableTitle">
                    最低监管金额：</td>
                <td class="tableContent" colspan="2">
                    <asp:TextBox ID="Step3" runat="server" Width="80px"></asp:TextBox>&nbsp;&nbsp;
                    <asp:Button ID="btnSave0" runat="server" Text="保存设置" onclick="btnSave0_Click" />
                    注：小于该金额的分录可以直接填制凭证。
                </td>
            </tr>
            <tr style="background-color:#f8f8f8">
                <td class="tableTitle2" colspan="3">
                    条形码控制管理</td>
            </tr>
            <tr style="background-color:#ffffff;">
                <td class="tableTitle" style="width:17%" rowspan="2">
                    村级职务：</td>
                <td class="tableContent" style="width:68%" rowspan="2">
                    <asp:CheckBoxList ID="UserTitles" runat="server" RepeatColumns="3" 
                        RepeatDirection="Horizontal">
                    </asp:CheckBoxList>
                </td>
                <td class="tableContent" style="width:15%; text-align:center; background-color:#f8f8f8; font-weight:bold">
                    所占比例</td>
            </tr>
            <tr style="background-color:#ffffff;">
                <td class="tableContent" style="text-align:center;">
                    <asp:DropDownList ID="Bili" runat="server">
                        <asp:ListItem Value="1">全部</asp:ListItem>
                        <asp:ListItem Value="0.67">三分之二</asp:ListItem>
                        <asp:ListItem Value="0.5">二分之一</asp:ListItem>
                        <asp:ListItem Value="0.33">三分之一</asp:ListItem>
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <tr style="background-color:#ffffff;height:35px;">
                <td class="tableTitle">
                    区间金额：</td>
                <td class="tableContent" colspan="2">
                    <asp:TextBox ID="Step1" runat="server" Width="80px"></asp:TextBox>&nbsp;∽
                    <asp:TextBox ID="Step2" runat="server" Width="80px"></asp:TextBox>&nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="添加区间" onclick="btnSave_Click" />
                </td>
            </tr>
            <tr style="background-color:#ffffff;height:35px;">
                <td colspan="3" style="height:35px;">
                    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AllowPaging="false"
                        AutoGenerateColumns="False" CaptionAlign="Left" 
                        Style="color: navy; margin:5px; width:635px" PageSize="15" 
                        onrowdatabound="GridView1_RowDataBound">
                        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
                        <Columns>
                            <asp:TemplateField HeaderText="区间金额">
                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                                <ItemTemplate>
                                    <asp:Label ID="Step1" runat="server" Text='<%# Bind("Step1") %>'></asp:Label>&nbsp;∽
                                    <asp:Label ID="Step2" runat="server" Text='<%# Bind("Step2") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="UserTitles" HeaderText="村级职务">
                                <ItemStyle HorizontalAlign="Left" Width="335px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BiliShow" HeaderText="通过比例">
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="删除">
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Bind("id") %>'
                                        CommandName="btnDelete" OnClick="btnDelete_Click">删除</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle Font-Size="10pt" />
                        <SelectedRowStyle BackColor="#FFC0C0" BorderColor="Yellow" BorderStyle="Dotted" />
                        <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" ForeColor="Navy" />
                        <AlternatingRowStyle BackColor="#EBF0F6" />
                    </asp:GridView>
                </td>
            </tr>
         </table>
    </div>
    </form>
</body>
</html>
