<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="PiBanKa.aspx.cs"
    Inherits="SanZi.Web.PiBanKa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>现金支出议定审核批办卡</title>
    <link href="../Style.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" id="HideScript" src=""></script>
    <script type="text/javascript" language="javascript" src="js/functionforjs.js"></script>
    <script type="text/javascript" language="javascript" src="js/getcalendar.js"></script>
    <script type="text/javascript">
        function $(o) { return (typeof (o) == "object") ? o : document.getElementById(o); }
        function OnCellClick(id) {
            var returnV = window.showModalDialog("../AccountInit/SelAllSubject.aspx?t=1&filter=101|102&g=" + (new Date()).getTime(), "", "dialogWidth=600px;dialogHeight=401px;center=yes;");
            if (typeof (returnV) != "undefined" && returnV[0] != "" && returnV[0] != "+" && returnV[0] != "-") {

                $(id).value = returnV[0];
                $("HiddenField2").value = returnV[1];
            }
        }
        function OnCellClick1(id) {
            var returnV = window.showModalDialog("../AccountInit/SelAllSubject.aspx?t=1&g=" + (new Date()).getTime(), "", "dialogWidth=600px;dialogHeight=401px;center=yes;");
            if (typeof (returnV) != "undefined" && returnV[0] != "" && returnV[0] != "+" && returnV[0] != "-") {

                $(id).value = returnV[0];
                $("HiddenField1").value = returnV[1];
            }
        }
        function delFile(divID, fileID) {
            var add = document.getElementById("addFiles");
            add.value = add.value.replace(fileID, "");
            var div = document.getElementById(divID);
            div.parentNode.removeChild(div);
            document.getElementById("divHtml").value = document.getElementById("filesShow").innerHTML;
            document.getElementById("HideScript").src = "DelFile.aspx?p=" + fileID.replace("$", "") + "&g=" + (new Date()).getTime();
        }
    </script>
</head>
<body>
    <form id="frmPiBanKa" name="frmPiBanKa" runat="server" onsubmit="return checkinput(document.frmPiBanKa)">
    <asp:HiddenField runat="server" ID="TestData" />
    <div id="loadinfo" style="visibility: hidden; position: absolute; left: 0px; top: 0px;
        background-color: Red; color: White;">
        Loading
    </div>
    <div align="center">
        <table style="width: 600px; border: 1px solid #000800; margin-top: 5px; background: #000800;"
            cellspacing="1" cellpadding="3">
            <tr style="background: #ffffff;">
                <td colspan="2" class="tableTitle2">
                    现金支出议定审核批办卡
                </td>
            </tr>
            <tr style="background: #ffffff;">
                <td class="tableTitle">
                    单位名称：
                </td>
                <td class="tableContent">
                    <asp:TextBox ID="txtDeptName" runat="server" Text="清河村"></asp:TextBox>
                    <input type="hidden" name="hidDeptID" id="hidDeptID" value="1" runat="server" />
                </td>
            </tr>
            <tr style="background: #ffffff;">
                <td class="tableTitle">
                    议定事由：
                </td>
                <td class="tableContent">
                    <asp:TextBox ID="txtReason" runat="server" Columns="50" Rows="6" TextMode="MultiLine"
                        TabIndex="1" Width="350px"></asp:TextBox><font color="red"><strong>*</strong></font>
                    <div id="tipReason" class="tipMsg" style="display: none; text-align: left;">
                    </div>
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
                    <asp:TextBox ID="DebitSubject" runat="server" Width="150px" ondblclick="OnCellClick1('DebitSubject')"
                        onfocus="this.blur()"></asp:TextBox>
                    <font color="red"><strong>*</strong></font>
                </td>
            </tr>
            <tr style="background: #ffffff;">
                <td class="tableTitle">
                    贷方科目：
                </td>
                <td class="tableContent">
                    <asp:TextBox ID="CreditSubject" runat="server" Width="150px" ondblclick="OnCellClick('CreditSubject')"
                        onfocus="this.blur()"></asp:TextBox>
                    <font color="red"><strong>*</strong></font>
                </td>
            </tr>
            <tr style="background: #ffffff;">
                <td class="tableTitle">
                    摘要：
                </td>
                <td class="tableContent">
                    <asp:TextBox ID="TextBox1" runat="server" Width="150px"></asp:TextBox>
                    <font color="red"><strong>*</strong></font>
                </td>
            </tr>
            <tr style="background: #ffffff;">
                <td class="tableTitle">
                    上传附单：
                </td>
                <td class="tableContent">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="221px" />
                    <font color="red"><strong>*</strong></font>
                    <asp:Button ID="uploadFiles" runat="server" Text="上传" OnClick="uploadFiles_Click" />
                    <div id="filesShow" runat="server">
                    </div>
                    <asp:HiddenField ID="addFiles" runat="server" />
                    <asp:HiddenField ID="divHtml" runat="server" />
                </td>
            </tr>
            <tr style="background: #ffffff;">
                <td class="tableTitle">
                    党员代表意见：
                </td>
                <td class="tableContent">
                    于
                    <asp:TextBox ID="time1" runat="server" Width="100px"></asp:TextBox>就议案征求党员意见，（<asp:TextBox
                        ID="value1" runat="server" Width="40px"></asp:TextBox>）通过。
                </td>
            </tr>
            <tr style="background: #ffffff;">
                <td class="tableTitle">
                    议案公开：
                </td>
                <td class="tableContent">
                    于
                    <asp:TextBox ID="time2" runat="server" Width="100px"></asp:TextBox>至<asp:TextBox
                        ID="time3" runat="server" Width="100px"></asp:TextBox>议案公开（不少于三天）。
                </td>
            </tr>
            <tr style="background: #ffffff;">
                <td class="tableTitle">
                    村民代表会议决议：
                </td>
                <td class="tableContent">
                    于<asp:TextBox ID="time4" runat="server" Width="100px"></asp:TextBox>召开村民代表会议决议,共
                    <asp:TextBox ID="value2" runat="server" Width="30px"></asp:TextBox>人参加，<asp:TextBox
                        ID="value3" runat="server" Width="30px"></asp:TextBox>人通过。
                </td>
            </tr>
            <tr style="background: #ffffff;">
                <td class="tableTitle">
                    结果公开：
                </td>
                <td class="tableContent">
                    于
                    <asp:TextBox ID="time5" runat="server" Width="100px"></asp:TextBox>至
                    <asp:TextBox ID="time6" runat="server" Width="100px"></asp:TextBox>结果公开（不少于三天）。
                </td>
            </tr>
            <tr style="background: #ffffff;">
                <td class="tableTitle">
                    审批人信息：
                </td>
                <td class="tableContent">
                    <input id="btnScanBarcode" type="button" value="扫描条形码" onclick="openScanBarcodePage()"
                        tabindex="3" />
                    <input type="hidden" id="hidBarCode" value="" runat="server" />
                    <asp:HiddenField ID="hidUID" Value="" runat="server" />
                    <div id="userInfo" class="tipMsg">
                    </div>
                </td>
            </tr>
            <tr style="background: #ffffff;">
                <td colspan="2">
                    <asp:Button ID="btnNextStep" runat="server" Text=" 下一步 " TabIndex="4" OnClick="btnNextStep_Click" />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript" language="javascript" defer="defer"> 
    <!--
        var confirmedInfo = ""; //已确认用户信息
        var arrUserInfo = new Array(); //用户信息数组
        var arrUserID = new Array();
        var arrBarcode = new Array(); //用户条形码数组
        var userCount = 0; //用户计数器
        var errMsg = ""; //错误提示信息
        var strTableHead = "<table cellSpacing=1 cellPadding=4 width=350 align=center bgColor=#999999 border=0 >";
        var strTableFoot = "</table>";
        function test() {
            alert("ddd");
        }
        function checkinput(obj) {
            if (Trim(obj.txtReason.value) == '') {
                alert("请输入议定事由！");
                obj.txtReason.focus();
                return false;
            }

            var outMoney = Trim(obj.txtDebit.value);
            if (outMoney == '') {
                alert("请输入支出金额！");
                obj.txtDebit.focus();
                return false;
            }
            if (!IsFloat(outMoney)) {
                alert("支出金额必须为数字！");
                obj.txtDebit.focus();
                return false;
            }

            /*
            if(userCount==0)
            {
            alert("请扫描用户条形码信息！");
            return false;
            }
            */

        }

        //显示提示信息
        function showTipMsg(div, value) {
            if (value.length == 0) {
                document.getElementById(div).style.display = "none";
                document.getElementById(div).innerHTML = value;
            }
            else {
                document.getElementById(div).style.display = "block";
                document.getElementById(div).innerHTML = value;
            }
        }

        //查询支出金额审批条件
        function searchContion() {
            var inputContent = Trim(document.getElementById("txtDebit").value).replace(/\'\r\n/ig, "");
            var deptID = Trim(document.getElementById("hidDeptID").value);
            if (IsFloat(inputContent) && inputContent != "") {
                showTipMsg("tipDebit", "");
                SanZi.Web.PiBanKa.GetOutCondition(inputContent, deptID, outCondition_callback);
            }
            else {
                showTipMsg("tipDebit", "请输入支出金额。");
                showTipMsg("showCondition", "");
            }
        }

        //查询支出金额审批条件 回调函数
        function outCondition_callback(res) {
            var strResult = res.value;
            if (strResult != null) {
                //document.getElementById("showCondition").style.display="block";
                //document.getElementById("showCondition").innerHTML=strResult;
                showTipMsg("showCondition", strResult);
            }
        }

        function openScanBarcodePage() {
            window.paramw = "扫描批办卡条形码信息";
            var sFeatures = "dialogwidth=500px;dialogheight=400px;status=no;help=no;scroll=no;center=Yes;status=no;";
            var url = 'ScanBarcode.aspx?g=' + (new Date()).getTime();
            var result = window.showModalDialog(encodeURI(url), window, sFeatures);
            
        }

        //array数据查找
        function JS_cruel_search(data, key) {
            re = new RegExp(key, [""])
            return (data.toString().replace(re, "┢").replace(/[^,┢]/g, "")).indexOf("┢")
        }

        //删除用户信息
        function delUser(Barcode) {
            var arrLength = arrBarcode.length;
            for (var i = 0; i < arrLength; i++) {
                if (arrBarcode[i] == Barcode) {
                    arrBarcode.splice(i, 1);
                    arrUserInfo.splice(i, 1);
                }
            }

            if (i > -1) {
                confirmedInfo = "";
                errMsg = "";
                userCount = userCount - 1;
                if (userCount > 0) {
                    for (var j = 0; j < (arrLength - 1); j++) {
                        confirmedInfo += "<tr bgcolor=#edf2f7><td>" + arrUserInfo[j][0] + "</td><td>" + arrUserInfo[j][1] + "</td><td>" + [j][2] + "</td><td><a href='#' onclick=delUser('" + [j][3] + "')>删除</a></td></tr>";
                    }
                }
                parentWindow.confirmedInfo = confirmedInfo;
                parentWindow.arrUserInfo = arrUserInfo;
                parentWindow.arrBarcode = arrBarcode;
                parentWindow.userCount = userCount;
                parentWindow.errMsg = errMsg;
                parentWindow.document.getElementById("userInfo").innerHTML = errMsg + "<table cellSpacing=1 cellPadding=4 width=350 align=center bgColor=#999999 border=0 >" + confirmedInfo + "</table>";

                document.getElementById("tipMessage").innerHTML = errMsg + "<table cellSpacing=1 cellPadding=4 width=350 align=center bgColor=#999999 border=0 >" + confirmedInfo + "</table>";
            }

        }

        //删除用户信息
        function delUser(Barcode) {
            var arrLength = arrBarcode.length;
            for (var i = 0; i < arrLength; i++) {
                if (arrBarcode[i] == Barcode) {
                    arrBarcode.splice(i, 1);
                    arrUserInfo.splice(i, 1);
                }
            }
            if (i > -1) {
                confirmedInfo = "";
                errMsg = "";
                userCount = userCount - 1;
                if (userCount > 0) {
                    for (var j = 0; j < (arrLength - 1); j++) {
                        confirmedInfo += "<tr bgcolor=#edf2f7><td>" + arrUserInfo[j][0] + "</td><td>" + arrUserInfo[j][2];
                        confirmedInfo += "</td><td>" + [j][3] + "</td><td><a href='#' onclick=delUser('" + [j][4] + "')>删除</a></td></tr>";
                    }
                }
                parentWindow.confirmedInfo = confirmedInfo;
                parentWindow.arrUserInfo = arrUserInfo;
                parentWindow.arrBarcode = arrBarcode;
                parentWindow.userCount = userCount;
                parentWindow.errMsg = errMsg;
                parentWindow.document.getElementById("userInfo").innerHTML = errMsg + strTableHead + confirmedInfo + strTableFoot;

                document.getElementById("tipMessage").innerHTML = errMsg + strTableHead + confirmedInfo + strTableFoot;
            }

        }
        function getServerTime() {
            // 调用服务端方法
            //调用方法:类名.方法名 (参数为指定一个回调函数)
            SanZi.Web.PiBanKa.GetServerTime(getServerTime_callback);
        }
        function getServerTime_callback(res) //回调函数,显示结果
        {
            alert(res.value);
        }
   
    //-->             
    </script>
    </form>
</body>
</html>
