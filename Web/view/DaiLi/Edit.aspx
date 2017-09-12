<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="SanZi.Web.DaiLi.Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>代理申请编辑</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
    <script type="text/javascript" language="javascript" src="../js/functionforjs.js"></script>
       <script type="text/javascript" language="javascript" src="../js/getcalendar.js"></script>
</head>
<body>
    <form id="frmDaiLi" name="frmPiBanKa" runat="server" onSubmit="return checkinput(document.frmDaiLi)">
    <div align="center">
         <table style="width:600px;border:1px solid #000800;margin-top:5px;background:#000800;" cellspacing="1" cellpadding="3" >
            <tr style="background:#ffffff;">
                <td colspan="4" class="tableTitle2">代理申请</td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">申请村：</td>
                <td class="tableContent">
                    <asp:TextBox ID="txtVillageName" runat="server"  width="150px"></asp:TextBox>
                    <input type="hidden" name="hidDeptID" id="hidDeptID" value="1" runat="server"/><font color="red"><strong>*</strong></font>
                </td>
                <td class="tableTitle">日期：</td>
                <td class="tableContent">
                    <asp:TextBox ID="txtApplyDate" runat="server" Text="" width="150px"></asp:TextBox><font color="red"><strong>*</strong></font>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">项目背景：</td>
                <td class="tableContent" colspan="3">
                    <asp:TextBox ID="txtProjectBackGround" runat="server" Columns="50" Rows="6" 
                        TextMode="MultiLine" tabindex="1" Width="350px"></asp:TextBox><font color="red"><strong>*</strong></font>
                        <div id="tipReason" class="tipMsg" style="display:none;text-align:left;"></div>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">项目名称：</td>
                <td class="tableContent">
                    <asp:TextBox ID="txtProjectName" runat="server" Text="" width="150px"></asp:TextBox><font color="red"><strong>*</strong></font>
                </td>
                <td class="tableTitle">估算价：</td>
                <td class="tableContent">
                    <asp:TextBox ID="txtEstimateValue" runat="server" Text="" width="100px"></asp:TextBox>元<font color="red"><strong>*</strong></font>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">项目类型：</td>
                <td class="tableContent" colspan="3">
                    <asp:RadioButton ID="radioZhiChu" runat="server" Text="支出项目"  
                        GroupName="projectType" Checked="True"/>
                    <asp:RadioButton ID="radioShouRu" runat="server" Text="收入项目"  GroupName="projectType"/>
                </td>
            </tr>
             <tr style="background: #ffffff;">
                <td class="tableTitle">
                   支部提议：
                </td>
                <td class="tableContent"  colspan="3">
                    因 <asp:TextBox ID="time8" runat="server" Width="100px"></asp:TextBox> 于 <asp:TextBox ID="time9" runat="server" Width="100px"></asp:TextBox>村党支部提出拟对 <asp:TextBox ID="value5" runat="server" Width="100px"></asp:TextBox>事项实行招标。
                </td>
            </tr>
             <tr style="background: #ffffff;">
                <td class="tableTitle">
                   两委商议：
                </td>
                <td class="tableContent"  colspan="3">
                     于 <asp:TextBox ID="time1" runat="server" Width="100px"></asp:TextBox>召开村两委联席会商议。
                </td>
            </tr>
             <tr style="background: #ffffff;">
                <td class="tableTitle">
                    党员大会意见：
                </td>
                <td class="tableContent"  colspan="3">
                     于 <asp:TextBox ID="time2" runat="server" Width="100px"></asp:TextBox>就议案征求党员意见，（<asp:TextBox ID="value1" runat="server" Width="40px"></asp:TextBox>）通过。
                </td>
            </tr>
             <tr style="background: #ffffff;">
                <td class="tableTitle">
                    议案公开：
                </td>
                <td class="tableContent"  colspan="3">
                     于 <asp:TextBox ID="time3" runat="server" Width="100px"></asp:TextBox>至<asp:TextBox ID="time4" runat="server" Width="100px"></asp:TextBox>议案公开（不少于三天）。
                </td>
            </tr>
             <tr style="background: #ffffff;">
                <td class="tableTitle">
                    村民代表会议决议：
                </td>
                <td class="tableContent"  colspan="3">
                     于<asp:TextBox ID="time5" runat="server" Width="100px"></asp:TextBox>召开村民代表会议,应参会 <asp:TextBox ID="value2" runat="server" Width="30px"></asp:TextBox>人，实参会 <asp:TextBox ID="value4" runat="server" Width="30px"></asp:TextBox>人，<asp:TextBox ID="value3" runat="server" Width="30px"></asp:TextBox>人通过，<asp:DropDownList 
                         ID="DropDownList1" runat="server">
                         <asp:ListItem>否</asp:ListItem>
                         <asp:ListItem Selected="True">是</asp:ListItem>
                     </asp:DropDownList>
                     超过应参数人数的1/2。
                </td>
            </tr>
            <tr style="background: #ffffff;">
                <td class="tableTitle">
                    结果公开：
                </td>
                <td class="tableContent"  colspan="3">
                     于 <asp:TextBox ID="time6" runat="server" Width="100px"></asp:TextBox>至 <asp:TextBox ID="time7" runat="server" Width="100px"></asp:TextBox>结果公开（不少于三天）。
                   
                </td>
            </tr>
            <tr style="background:#ffffff;"id="barcodeInfoArea" style="display:none">
                <td class="tableTitle">村民代表信息：</td>
                <td class="tableContent" colspan="3">
                    <input id="btnScanBarcode" type="button" value="扫描条形码" onclick="openScanBarcodePage()" tabindex="3" />
                    <input type="hidden" id="hidBarCode" value="" runat="server"/>
                    <input type="hidden" id="hidUID" value="" runat="server"/>
                    <asp:HiddenField ID="dailiid" runat="server" />
                    <div id="userInfo" class="tipMsg"></div>
                    <asp:Label ID="lblShenPiRen" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td colspan="4">
                    <asp:Button ID="btnSave" runat="server" Text=" 保存 " tabindex="4" 
                        onclick="btnSave_Click" />
                </td>
            </tr>
         </table>
    </div>
    
<script type="text/javascript" language="javascript" defer="defer"> 
<!--

    function changeZhiChu(objID) {
        if (document.getElementById(objID).checked) {
            //document.getElementById("barcodeInfoArea").style.display = "none";
        }
    }

    function changeShouRu(objID) {
        if (document.getElementById(objID).checked) {
            //document.getElementById("barcodeInfoArea").style.display = "block";
        }
    }

    function checkinput(obj) {
        if (Trim(obj.txtVillageName.value) == '') {
            alert("请输入申请村！");
            obj.txtVillageName.focus();
            return false;
        }
        if (Trim(obj.txtApplyDate.value) == '') {
            alert("请输入日期！");
            obj.txtApplyDate.focus();
            return false;
        }
        if (Trim(obj.txtApplyDate.value) == '') {
            alert("请输入日期！");
            obj.txtApplyDate.focus();
            return false;
        }
        if (Trim(obj.txtProjectBackGround.value) == '') {
            alert("请输入项目背景！");
            obj.txtProjectBackGround.focus();
            return false;
        }
        if (Trim(obj.txtProjectName.value) == '') {
            alert("请输入项目名称！");
            obj.txtProjectName.focus();
            return false;
        }

        var estimateValue = Trim(obj.txtEstimateValue.value);
        if (estimateValue == '') {
            alert("请输入项目估算价！");
            obj.txtEstimateValue.focus();
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


    //显示提示信息
    function showTipMsg(div, value) {
        document.getElementById(div).style.display = "block";
        document.getElementById(div).innerHTML = value;
    }

    //查询支出金额审批条件
    function searchContion() {
        var inputContent = Trim(document.getElementById("txtDebit").value).replace(/\'\r\n/ig, "");
        var deptID = Trim(document.getElementById("hidDeptID").value);
        if (IsFloat(inputContent) && inputContent != "") {
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
        var url = '../ScanBarcode.aspx';
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

