<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScanBarcode.aspx.cs" Inherits="SanZi.Web.ScanPiBankaBarcode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>扫描条形码信息</title>
    <link href="Style.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="js/functionforjs.js"></script>
</head>
<body>
    <form id="frmScanBarCode" runat="server">
    <table style="width: 450px; border: 1px solid #000800; margin-top: 5px; background: #000800;"
        cellspacing="1" cellpadding="3">
        <tr style="background: #ffffff;">
            <td class="tableTitle2">
                扫描条形码信息
            </td>
        </tr>
        <tr style="background: #ffffff;">
            <td class="tableContent">
                <textarea id="txtScanBarCode" tabindex="1" cols="60" rows="6" onkeydown="keyDown(event)"></textarea>
            </td>
        </tr>
        <tr style="background: #ffffff;">
            <td id="tipMessage" class="tipMsg">
                请扫描用户条形码
            </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript">
        var inputObj = document.getElementById("txtScanBarCode");
        var inputValue = ""; //输入信息
        var confirmedInfo = ""; //已确认用户信息
        var arrUserInfo = new Array(); //用户信息数组
        var arrUserID = new Array();
        var arrBarcode = new Array(); //用户条形码数组
        var userCount = 0; //用户计数器
        var errMsg = ""; //错误提示信息
        var strTableHead = "<table cellSpacing=1 cellPadding=4 width=350 align=center bgColor=#999999 border=0 >";
        var strTableFoot = "</table>";

        var parentWindow = window.dialogArguments; //父窗口对象

        window.onload = function () {
            inputObj.value = "";
            inputObj.focus();
            confirmedInfo = parentWindow.confirmedInfo;
            arrUserInfo = parentWindow.arrUserInfo;
            arrBarcode = parentWindow.arrBarcode;
            userCount = parentWindow.userCount;
            errMsg = parentWindow.errMsg;
            document.getElementById("tipMessage").innerHTML = errMsg + strTableHead + confirmedInfo + strTableFoot;
        }

        function keyDown(event) {
            if (event.keyCode == 13) {
                inputValue = inputObj.value.replace(/\'\r\n/ig, ""); //条形码信息
                if (inputValue == "" || inputValue == null) {
                    inputObj.value = "";
                    inputObj.focus();
                    document.getElementById("tipMessage").innerHTML = "请扫描用户条形码！";
                }
                else {
                    searchUserInfo(inputValue.replace(/\r\n|\n/g, ''));
                    inputObj.value = "";
                    inputObj.focus();
                }
            }
        }

        //检查条形码信息
        function searchUserInfo(str) {
            SanZi.Web.ScanPiBankaBarcode.searchUserInfo(str, searchUserInfo_callback);
        }

        //回调函数,显示结果
        function searchUserInfo_callback(res) {
            var strResult = res.value;
            if (strResult != null) {
                var strResultArray = strResult.split("|");
                var strFlag = strResultArray[0];

                if (strFlag == "ok") {
                    errMsg = "";
                    var keyLocation = JS_cruel_search(arrBarcode, inputValue); //检查是否已扫描
                    var lineData = "";
                    if (keyLocation == -1) {
                        keyLocation = JS_cruel_search(arrUserID, strResultArray[2]);
                    }
                    if (keyLocation == -1) {
                        arrBarcode[userCount] = inputValue; //条形码数组
                        arrUserID[userCount] = strResultArray[2];
                        arrUserInfo[userCount] = new Array(strResultArray[1], strResultArray[2], strResultArray[3], strResultArray[4], strResultArray[5]); //返回消息数组
                        lineData = "<tr bgcolor=#edf2f7><td>" + strResultArray[1] + "</td><td>" + strResultArray[3] + "</td>";
                        lineData += "<td>" + strResultArray[4] + "</td>";
                        //lineData+="<td><a href='#' onclick=delUser('"+strResultArray[5]+"')>删除</a></td>";
                        lineData += "</tr>";
                        confirmedInfo = lineData + confirmedInfo;
                    }
                    else {
                        errMsg = "提示：已扫描（" + strResultArray[4] + "-" + strResultArray[3] + "-" + strResultArray[1] + "）<br/>";
                    }
                    userCount = userCount + 1; //用户计数器
                }
                else {
                    //errMsg = "条形码信息有误，没有找到权利人信息！";
                    errMsg = strResultArray[1];
                }

                parentWindow.confirmedInfo = confirmedInfo;
                parentWindow.arrUserInfo = arrUserInfo;
                parentWindow.arrBarcode = arrBarcode;
                parentWindow.userCount = userCount;
                parentWindow.errMsg = errMsg;
                parentWindow.document.getElementById("hidBarCode").value = arrBarcode.join(",");
                parentWindow.document.getElementById("hidUID").value = arrUserID.join(",");
                parentWindow.document.getElementById("userInfo").innerHTML = errMsg + strTableHead + confirmedInfo + strTableFoot;
                //parentWindow.document.getElementById("txtReason").value=arrBarcode.join(",");

                document.getElementById("tipMessage").innerHTML = errMsg + strTableHead + confirmedInfo + strTableFoot;
            }
        }

        function searchBarCode(str) {
            hidBarCode = document.getElementById("hidBarCodeStr").value;
            var dataArray = hidBarCode.split("#"); //字符分割
            var arrLength = dataArray.length;
            var intReturn = -1;
            for (var i = 0; i < (arrLength - 1); i++) {
                if (dataArray[i] == str) {
                    intReturn = i;
                }
            }
            return intReturn;
        }

        //array数据查找
        function JS_cruel_search(data, key) {
            if (data.length == 0) {
                return -1;
            }
            else {
                var strArr = data.join(",");
                if (strArr.indexOf(key) == -1) {
                    return -1;
                }
                else {
                    return 1;
                }
            }
        }

        //删除用户信息
        function delUser(Barcode) {
            var arrLength = arrBarcode.length;
            for (var i = 0; i < arrLength; i++) {
                if (arrBarcode[i] == Barcode) {
                    arrBarcode.splice(i, 1);
                    arrUserInfo.splice(i, 1);
                    arrUserID.splice(i, 1);
                }
            }

            if (i > -1) {
                confirmedInfo = "";
                errMsg = "";
                userCount = userCount - 1;
                var lineData = "";
                if (userCount > 0) {
                    for (var j = 0; j < (arrLength - 1); j++) {
                        lineData = "<tr bgcolor=#edf2f7><td>" + arrUserInfo[j][0] + "</td><td>" + arrUserInfo[j][2];
                        lineData += "</td><td>" + [j][3] + "</td>";
                        //confirmedInfo+="<td><a href='#' onclick=delUser('"+[j][4]+"')>删除</a></td>";
                        lineData += "</tr>";
                        confirmedInfo = lineData + confirmedInfo;
                    }
                }
                parentWindow.confirmedInfo = confirmedInfo;
                parentWindow.arrUserInfo = arrUserInfo;
                parentWindow.arrBarcode = arrBarcode;
                parentWindow.userCount = userCount;
                parentWindow.errMsg = errMsg;
                parentWindow.document.getElementById("hidBarCode").value = arrBarcode.join(",");
                parentWindow.document.getElementById("hidUID").value = arrUserID.join(",");
                parentWindow.document.getElementById("userInfo").innerHTML = errMsg + strTableHead + confirmedInfo + strTableFoot;

                document.getElementById("tipMessage").innerHTML = errMsg + strTableHead + confirmedInfo + strTableFoot;
            }
        }      
    </script>
    </form>
</body>
</html>
