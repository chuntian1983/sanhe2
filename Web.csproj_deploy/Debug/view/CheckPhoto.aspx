<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckPhoto.aspx.cs" Inherits="SanZi.Web.CheckPhoto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>票据识别</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center;">
        <img  src="images/a.jpg" width="600"/><br />
        <asp:Button ID="btnCheckPhoto" runat="server" Text="票据识别" 
            onclick="btnCheckPhoto_Click" />
        <asp:Label ID="lblResult" runat="server"></asp:Label>
    </div>
    
    </form>
</body>
</html>
