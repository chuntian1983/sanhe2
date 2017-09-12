<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="SanZi.Web.PiBanKaAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr style="background: #ffffff;">
                <td class="tableTitle" width="100">
                    上传票据：
                </td>
                <td class="tableContent">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="300" />
                </td>
            </tr>
            <tr style="background: #ffffff;">
                <td colspan="2" align="center">
                    <asp:Button ID="btnCheck" runat="server" Text="生成凭证" OnClick="btnCheck_Click" />
                    <br />
                    <asp:Label ID="lblResult" runat="server" Text="结果" ForeColor="Red"></asp:Label>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <asp:HiddenField ID="HiddenField3" runat="server" />
                    <asp:HiddenField ID="HiddenField4" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
