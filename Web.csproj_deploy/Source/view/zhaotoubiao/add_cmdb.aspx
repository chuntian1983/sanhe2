<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add_cmdb.aspx.cs" Inherits="SanZi.Web.view.zhaotoubiao.add_cmdb" %>

<%@ Register src="../../fileuploaduser.ascx" tagname="fileuploaduser" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
                    <input type="hidden" id="hidUID" value="" runat="server"/>
                    <input type="hidden" id="hidBarCode" value="" runat="server"/>
                    <input type="hidden" name="hidDeptID" id="hidDeptID" value="1" runat="server"/>
    <div>
    <table style="width:600px;border:1px solid #000800;margin-top:5px;background:#000800;" cellspacing="1" cellpadding="3" >
            <tr style="background:#ffffff;">
                <td class="tableTitle" colspan="2" align=center>
                    <asp:Label ID="lbtitle" runat="server" Text=" "></asp:Label>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">所属单位：</td>
                <td class="tableContent">
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">项目名称：</td>
                <td class="tableContent">
                    <asp:TextBox ID="txtProjectName" runat="server" Text="" width="150px"></asp:TextBox>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    </asp:DropDownList>
                    <font color="red"><strong>*</strong></font></td>
            </tr>
             <tr style="background: #ffffff;">
                <td class="tableTitle">
                    图片上传：
                </td>
                <td class="tableContent">
                   <asp:Panel ID="Pan_UpFile" runat="server" Height="200px" ScrollBars="Auto" Width="250px">
               <table id="Tab_UpDownFile" runat="server" cellpadding="0" cellspacing="0" enableviewstate="true">
                  <tr>
                     <td style="width: 100px; height: 30px">
                         <asp:FileUpload ID="FileUpload1" runat="server" />
                         
                     </td>
                  </tr>
              </table>
            </asp:Panel>
<asp:Button ID="BtnAdd" runat="server" BorderColor="Gray" BorderWidth="1px" 
                             OnClick="BtnAdd_Click" Text="添加文件" />
                    <asp:TextBox ID="TextBox1" runat="server" Width="85px"></asp:TextBox>
                    个（2个以上请输入数字点击添加文件按钮）</td>
            </tr>
            <tr style="background:#ffffff;">
                <td colspan="2" align=center>

                   &nbsp;

                   <asp:Button ID="BtnUpFile" runat="server" OnClick="BtnUpFile_Click" Text="上传文件" BorderColor="Gray" BorderWidth="1px" />

                </td>
            </tr>
         </table>
    </div>
    </form>
</body>
</html>
