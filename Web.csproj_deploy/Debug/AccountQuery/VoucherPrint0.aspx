<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountManage_VoucherPrint0" Codebehind="VoucherPrint0.aspx.cs" %>

<%@ Register Src="../AccountQuery/ShowVoucher.ascx" TagName="ShowVoucher" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>查看凭证</title>
<base target="_self" />
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function setposition(a,b)
{
    var aTag=$(a);
    var leftpos=aTag.offsetLeft+9;
    var toppos=aTag.offsetTop;
    while(aTag = aTag.offsetParent)
    {
        leftpos += aTag.offsetLeft;
        toppos += aTag.offsetTop;
    }
    $(b).style.left=leftpos;
    $(b).style.top=toppos;
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px; margin-top:45px">
            <tr>
                <td style="height: 350px; text-align: center; vertical-align:top">
                    <uc1:ShowVoucher ID="ShowVoucher" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
