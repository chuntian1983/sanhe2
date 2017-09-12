<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fukuan.aspx.cs" Inherits="SanZi.Web.fukuan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>村组付款</title>
    <link href="Style.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript">

        function $(o) { return (typeof (o) == "object") ? o : document.getElementById(o); }
        function OnCellClick1(id) {
            var returnV = window.showModalDialog("../AccountInit/SelAllSubject.aspx?t=1&filter=101|102&g=" + (new Date()).getTime(), "", "dialogWidth=600px;dialogHeight=401px;center=yes;");
            if (typeof (returnV) != "undefined" && returnV[0] != "" && returnV[0] != "+" && returnV[0] != "-") {

                $(id).value = returnV[0];
                $("HiddenField2").value = returnV[1];
            }
        }
        function OnCellClick(id) {
            var returnV = window.showModalDialog("../AccountInit/SelAllSubject.aspx?t=1&filter=202&g=" + (new Date()).getTime(), "", "dialogWidth=600px;dialogHeight=401px;center=yes;");
            if (typeof (returnV) != "undefined" && returnV[0] != "" && returnV[0] != "+" && returnV[0] != "-") {

                $(id).value = returnV[0];
                $("HiddenField1").value = returnV[1];
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 500px; border: 1px solid #000800; margin-top: 5px; background: #000800;"
        cellspacing="1" cellpadding="3">
        <tr style="background: #ffffff; font-size:15px">
            <td class="tableTitle" colspan="2">
                村组付款</td>
        </tr>
        <tr style="background: #ffffff; display:none">
            <td class="tableTitle" width="100">
                上传票据：
            </td>
            <td class="tableContent">
                <asp:FileUpload ID="FileUpload1" runat="server" Width="300" />
            </td>
        </tr>
        <tr style="background: #ffffff;">
            <td class="tableTitle">
                支出金额：
            </td>
            <td class="tableContent">
                <asp:TextBox ID="txtDebit" runat="server" Width="150px"></asp:TextBox>元 <font color="red">
                    <strong>*</strong></font>
                <div id="tipDebit" class="tipMsg" style="display: none; text-align: left;">
                </div>
                <div id="showCondition" style="display: none; text-align: left;">
                </div>
            </td>
        </tr>
        <tr style="background: #ffffff;">
            <td class="tableTitle">
                借方科目：
            </td>
            <td class="tableContent">
                <asp:TextBox ID="DebitSubject" runat="server" Width="150px" ondblclick="OnCellClick('DebitSubject')" onfocus="this.blur()"></asp:TextBox>
                <font color="red"><strong>*</strong></font>
            </td>
        </tr>
        <tr style="background: #ffffff;">
            <td class="tableTitle">
                贷方科目：
            </td>
            <td class="tableContent">
                <asp:TextBox ID="CreditSubject" runat="server" Width="150px" ondblclick="OnCellClick1('CreditSubject')" onfocus="this.blur()"></asp:TextBox>
                <font color="red"><strong>*</strong></font>
            </td>
        </tr>
         <tr style="background: #ffffff;">
            <td class="tableTitle">
                摘要录入：
            </td>
            <td class="tableContent">
                <asp:TextBox ID="TextBox1" runat="server" Width="150px" ></asp:TextBox>
                <font color="red"><strong>*</strong></font>
            </td>
        </tr>
        <tr style="background: #ffffff;">
            <td colspan="2">
                <!--
                    <input id="btnCheckPhoto" type="button" value="检查票据" onclick="chekcPhoto()"/>
                    -->
                <asp:Button ID="btnCheckPic" runat="server" Text="检查票据" OnClick="btnCheckPic_Click" Visible="false" />
                <asp:Button ID="btnCheck" runat="server" Text="生成凭证" OnClick="btnCheck_Click" />
                <br />
                <asp:Label ID="lblResult" runat="server" Text="结果" ForeColor="Red"></asp:Label>
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="HiddenField2" runat="server" />
                <asp:HiddenField ID="HiddenField3" runat="server" />
            </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript" defer="defer"> 
<!--
        function chekcPhoto() {
            SanZi.Web.uploadPic.checkPhoto(chekcPhoto_callback);
        }

        function chekcPhoto_callback(res) {
            var strResult = res.value;
            if (strResult != null) {
                var strResultArray = strResult.split("|");
                var strFlag = strResultArray[0];
                if (strFlag == "ok") {
                    alert(strResultArray[1]);
                }
                else {
                    alert(strResultArray[1]);
                }
            }
        }
    
-->
    </script>
    </form>
</body>
</html>
