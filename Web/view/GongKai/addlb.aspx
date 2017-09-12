<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addlb.aspx.cs" Inherits="SanZi.Web.view.GongKai.addlb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>添加公开表</title>
    <link href="../../Style.css" type="text/css" rel="stylesheet"/>
    
</head>
<body>
<script type="text/javascript" language="javascript">
    function check_input() {
        /*
        if (document.formAddUser.hidDeptID.value == 0)  {
        alert("请选择所属单位！");
        return false;
        } 
        */
//        if (document.formAddUser.dplUserTitle.value == 0) {
//            alert("请选择个人职务！");
//            document.formAddUser.dplUserTitle.focus();
//            return false;
//        }
//        if (document.formAddUser.txtTrueName.value == '') {
//            alert("请输入姓名！");
//            document.formAddUser.txtTrueName.focus();
//            return false;
//        }
        return true;
    }
    //-->             
</script>

    <form id="formAddUser" runat="server" onsubmit='return check_input();'>
    <div align="center">
         <table style="width:600px;border:1px solid #000800;margin-top:5px;background:#000800;" cellspacing="1" cellpadding="3" >
            <tr style="background:#ffffff;">
                <td colspan="2" align=center; class="tableTitle">添加公开表</td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">所属单位：</td>
                <td class="tableContent">
                    <asp:Label ID="lbssdw" runat="server" Text="Label"></asp:Label>
                    <input type="hidden" name="hidDeptID" id="hidDeptID" value="1"/>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">公开表名称：</td>
                <td class="tableContent">
                    <asp:TextBox id="txtname" runat="server" width="348px" ></asp:TextBox>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">类别名称：</td>
                <td class="tableContent">
                    <asp:DropDownList ID="ddllbmc" runat="server" width="150px">
                        <asp:ListItem>村务公开</asp:ListItem>
                        <asp:ListItem>党务公开</asp:ListItem>
                    </asp:DropDownList>
                    <font color="red"><strong>*</strong></font>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">上传公开表样表：</td>
                <td class="tableContent">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="346px" />
                <font color="red"><strong>*</strong></font>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td colspan="2"><asp:Button ID="btnAddUser" runat="server" Text="  提交  "  onclick="btnAddUser_Click" ></asp:Button></td>
            </tr>
         </table>
         <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EnableModelValidation="True" onrowcommand="GridView1_RowCommand" Width="600px">
            <Columns>
                <asp:BoundField DataField="name" HeaderText="名称" ItemStyle-Width="30%" />
                <asp:TemplateField HeaderText="文件名">
                    <ItemTemplate>
                        <a href="../gongkai/<%# Eval("filename") %>" target="_blank"><%# Eval("filename")%></a>
                    </ItemTemplate>
                    <ItemStyle Width="60%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" SortExpression="id">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" OnClientClick="return confirm('你确认要删除吗？')" runat="server" CausesValidation="False" Visible="true"
                         CommandName="Del" CommandArgument='<%# Eval("id") %>' Text="删除"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="10%" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
