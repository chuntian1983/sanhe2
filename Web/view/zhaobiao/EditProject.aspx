<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditProject.aspx.cs" Inherits="SanZi.Web.zhaobiao.EditProject" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>修改项目名称</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
</head>
<body>
<script type="text/javascript" language="javascript">
    function check_input() {
        if (document.getElementById("tbxmmc").value == ""||document.getElementById("tbxmmc").value==null) {
            alert("项目名称不能为空！");
            return false;
        } 
    }               
    //-->             
</script>

    <form id="formEditUser" runat="server" onsubmit='return check_input();'>
    <div align="center">
         <table style="width:400px;border:1px solid #000800;margin-top:5px;background:#000800;" cellspacing="1" cellpadding="3" >
            <tr style="background:#ffffff;">
                <td colspan="2" class="tableTitle">修改项目名称</td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">项目名称：</td>
                <td class="tableContent">
                    <asp:TextBox id="tbxmmc" runat="server" width="150px"></asp:TextBox><font color="red"><strong>*</strong></font>
                    <input type="hidden" name="hidProjectID" id="hidProjectID" value="0" runat="server"/>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td colspan="2"><asp:Button ID="btnEditProject" runat="server" Text="  修改  " onclick="btnAddProject_Click" ></asp:Button></td>
            </tr>
         </table>
    </div>
    </form>
</body>
</html>
