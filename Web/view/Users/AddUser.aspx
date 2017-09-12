<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="SanZi.Web.Users.AddUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>添加权利人</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
</head>
<body>
<script type="text/javascript" language="javascript"> 
    function check_input()  {
        /*
        if (document.formAddUser.hidDeptID.value == 0)  {
            alert("请选择所属单位！");
            return false;
        } 
        */
        if (document.formAddUser.dplUserTitle.value == 0)  {
            alert("请选择个人职务！");
            document.formAddUser.dplUserTitle.focus();
            return false;
        } 
        if (document.formAddUser.txtTrueName.value == '')  {
            alert("请输入姓名！");
            document.formAddUser.txtTrueName.focus();
            return false;
        }
        return true;
    }               
    //-->             
</script>

    <form id="formAddUser" runat="server" onsubmit='return check_input();'>
    <div align="center">
         <table style="width:400px;border:1px solid #000800;margin-top:5px;background:#000800;" cellspacing="1" cellpadding="3" >
            <tr style="background:#ffffff;">
                <td colspan="2" class="tableTitle">添加权利人</td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">所属单位：</td>
                <td class="tableContent">
                    <asp:TextBox id="txtDeptName" runat="server" width="150px" value="点击选择所属单位" onfocus="if(this.value=='点击选择所属单位'){this.value='';}"  onblur="if(this.value==''){this.value='点击选择所属单位';}" ></asp:TextBox>
                    <input type="hidden" name="hidDeptID" id="hidDeptID" value="1"/>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">个人职务：</td>
                <td class="tableContent">
                    <asp:DropDownList ID="dplUserTitle" runat="server" width="150px"></asp:DropDownList>
                    <font color="red"><strong>*</strong></font>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">姓名：</td>
                <td class="tableContent">
                <asp:TextBox id="txtTrueName" runat="server" width="150px"></asp:TextBox>
                <font color="red"><strong>*</strong></font>
                </td>
            </tr>
              <tr style="background:#ffffff;">
                <td class="tableTitle">联系电话：</td>
                <td class="tableContent">
                <asp:TextBox id="txtTelPhone" runat="server" width="150px"></asp:TextBox>
                <font color="red"><strong>*</strong></font>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td colspan="2"><asp:Button ID="btnAddUser" runat="server" Text="  提交  "  onclick="btnAddUser_Click" ></asp:Button></td>
            </tr>
         </table>
    </div>
    </form>
</body>
</html>
