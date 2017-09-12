<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProject.aspx.cs" Inherits="SanZi.Web.zhaobiao.AddProject" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Document</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
</head>
<body>
<script type="text/javascript" language="javascript">
    function check_input() {
        if (document.getElementById("tbxmmc").value == "" || document.getElementById("tbxmmc").value == null) {
            alert("椤圭洰鍚嶇О涓嶈兘涓虹┖");
            return false;
        }
    }
    //-->             
</script>

    <form id="formAddUser" runat="server" onsubmit='return check_input();'>
    <div align="center">
         <table style="width:400px;border:1px solid #000800;margin-top:5px;background:#000800;" cellspacing="1" cellpadding="3" >
            <tr style="background:#ffffff;">
                <td colspan="2" class="tableTitle">娣诲姞椤圭洰</td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">椤圭洰鍚嶇О:</td>
                <td class="tableContent">
                    <asp:TextBox id="tbxmmc" runat="server" width="150px"></asp:TextBox><font color="red"><strong>*</strong></font>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td colspan="2"><asp:Button ID="btnAddProject" runat="server" Text="  鎻愪氦  " onclick="btnAddProject_Click" ></asp:Button></td>
            </tr>
         </table>
    </div>
    </form>
</body>
</html>
