<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountManage_DoVoucher" Codebehind="DoVoucher.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <meta http-equiv="Content-Language" content="zh-cn" />
    <link type="text/css" href="../Images/css.css" rel="Stylesheet" />
    <style type="text/css">
        .Lightbox
        {
            border: #fff 1px solid;
            display: block;
            z-index: 9999;
            text-align: center;
            position: absolute;
            background-color: #f6f6f6;
            color: Red;
            font-size: 18px;
        }
        .Overlay
        {
            display: block;
            z-index: 9998;
            filter: alpha(opacity=3);
            position: absolute;
            background-color: #000;
            moz-opacity: 0.8;
            opacity: .80;
        }
        .txtFocus{border:1px solid green; color:black}
        .txtBlur{border:1px solid white; color:blue}
    </style>
    <script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
    <script type="text/javascript" id="UpdateCtlTime" src=""></script>
    <script type="text/javascript" id="HideScript" src=""></script>
    <script type="text/javascript">
        function Number.prototype.str(s) { var a = "" + this; return s.substring(0, s.length - a.length) + a; }
        function String.prototype.lenB() { return this.replace(/[^\x00-\xff]/g, "**").length; }
        function $(o) { return (typeof (o) == "object") ? o : document.getElementById(o); }
        function GetCellID(r, c)
        {
            r += 2;
            if (r < 10) { r = "0" + r; }
            if (c == 0) {
                return $("GridView1_ctl" + r + "_ctl00");
            }
            else {
                return $("GridView1_ctl" + r + "_txtComm" + c);
            }
        }
        function OnRowClick(row, rowid) {
            if ($("RowsCount").value == row) { return; }
            //SetObjectPos(rowid, "EntryRowIipDiv0", 0, -1);
            //SetObjectPos(rowid, "EntryRowIipDiv1", 26, -1);
            $("curRowID").value = rowid;
            $("curRowIndex").value = row;
        }
        function OnCellClick(vid, row, col, cellid) {
            if (eval($("RowsCount").value) == row) {
                AddNewEntryRow();
                return;
            }
            var urlPara = "id=" + vid + "&no=" + $("v1" + row).value + "&row=" + row + "&money=" + $("v2" + row).value + "&cl=" + $("v4" + row).value + "&g=" + (new Date()).getTime();
            switch (col) {
                case 0:
                case 2:
                    var arg = {};
                    arg.vid = vid;
                    arg.row = row;
                    arg.col = col;
                    arg.cellid = cellid;
                    arg.GetCellID = GetCellID;
                    arg.DoSelectSubject = DoSelectSubject;
                    arg.ParentDoc = window.document;
                    var returnV = window.showModalDialog("ChooseSubject.aspx?" + urlPara, arg, "dialogWidth=500px;dialogHeight=484px;center=yes;");
                    if (typeof (returnV) != "undefined") {
                        DoSelectSubject(vid, row, col, returnV);
                    }
                    else {
                        $(cellid).focus();
                    }
                    break;
                default:
                    if ($("v1" + row).value == "") {
                        alert("请录入科目！");
                        return;
                    }
                    var returnV = window.showModalDialog("AddEntryData.aspx?" + urlPara, window.document, "dialogWidth=600px;dialogHeight=401px;center=yes;");
                    if (typeof (returnV) != "undefined" && returnV[0] != "" && returnV[0] != "+" && returnV[0] != "-") {
                        WriteMoney(row, returnV[0]);
                        SumMoney();
                        if (returnV[1] != "" && returnV[2] == "1") {
                            $(GetCellID(row, 1)).value = returnV[1];
                            $("v0" + row).value = returnV[1];
                        }
                        $("ModiFlag").value = "1";
                        if (returnV[0].substring(0, 1) == "-") {
                            $(GetCellID(row, 4)).focus();
                        }
                        else {
                            $(GetCellID(row, 3)).focus();
                        }
                    }
                    break;
            }
        }
        function DoSelectSubject(vid, row, col, returnV) {
            if (returnV[7]) {
                row = returnV[7];
            }
            if (row >= eval($("RowsCount").value)) {
                alert("请插入空分录行！");
                if (returnV[8]) {
                    returnV[8].close();
                }
                return;
            }
            $("v1" + row).value = returnV[2];
            $("v3" + row).value = returnV[6];
            $("v4" + row).value = returnV[9];
            if (returnV[5] == "0") {
                if (returnV[3] != "") {
                    WriteMoney(row, returnV[3]);
                }
            }
            else {
                var a = "id=" + vid + "&no=" + $("v1" + row).value + "&row=" + row + "&money=" + $("v2" + row).value + "&g=" + (new Date()).getTime();
                var s = window.showModalDialog("AddEntryData.aspx?" + a, window.document, "dialogWidth=600px;dialogHeight=401px;center=yes;");
                if (typeof (s) != "undefined" && s[0] != "" && s[0] != "+" && s[0] != "-") {
                    WriteMoney(row, s[0]);
                    if (s[1] != "" && s[2] == "1") {
                        $(GetCellID(row, 1)).value = s[1];
                        $("v0" + row).value = s[1];
                    }
                }
            }
            $(GetCellID(row, 0)).innerText = returnV[0];
            $(GetCellID(row, 2)).value = returnV[1];
            $("ModiFlag").value = "1";
            if (returnV[4] == 1) {
                OnCellClick(vid, row + 1, col, GetCellID(row + 1, col));
            }
            else {
                $(GetCellID(row, 3)).focus();
            }
        }
        function WriteMoney(row, returnV) {
            var esubjectNo = $("v1" + row).value;
            //赤字提示
            if ($("RedFigureFlag").value == "1") {
                var tbalance = eval($("v3" + row).value) + eval(returnV);
                for (var i = 0; i < eval($("RowsCount").value); i++) {
                    if (i != eval(row) && $("v1" + i).value == esubjectNo && $("v2" + i).value.length > 0) {
                        tbalance += eval($("v2" + i).value);
                    }
                }
                tbalance = formatFloat(tbalance);
                var tpattern0 = /^(101|102|112|113).*$/;
                var tpattern1 = /^(202|221|241).*$/;
                if (tpattern1.test(esubjectNo)) {
                    if (tbalance > 0) {
                        tbalance = 0 - tbalance;
                        if (!confirm("赤字提示：科目[" + esubjectNo + "]当前余额（" + tbalance + "）已为负数！\n\n是否继续录入发生额？")) {
                            return false;
                        }
                    }
                }
                if (tpattern0.test(esubjectNo)) {
                    if (tbalance < 0) {
                        if (!confirm("赤字提示：科目[" + esubjectNo + "]当前余额（" + tbalance + "）已为负数！\n\n是否继续录入发生额？")) {
                            return false;
                        }
                    }
                }
            }
            //报警校验
            var ebalance = eval(returnV);
            var alarm = $("BalanceAlarm").value.split("$");
            for (var i = 0; i < alarm.length; i++) {
                var ba = alarm[i].split("|");
                //if (ba[0] == esubjectNo && (ba[0].substring(0, 3) == "101" ? ebalance < 0 : ebalance > 0) && Math.abs(ebalance) >= eval(ba[1])) {
                if (ba[0] == esubjectNo && (ba[2] == "0" ? ebalance > 0 : ebalance < 0) && Math.abs(ebalance) >= eval(ba[1])) {
                    if (!confirm("报警：" + (ba[2] == "0" ? "借方" : "贷方") + "科目[" + ba[0] + "]超过限额：" + ba[1] + "，如果继续，该凭证必须由监督管理部门审核！\n\n是否继续录入发生额？")) {
                        return false;
                    }
                }
            }
            //录入科目发生额
            $("v2" + row).value = returnV;
            var cell3 = $(GetCellID(row, 3));
            var cell4 = $(GetCellID(row, 4));
            if (returnV.substring(0, 1) == "-") {
                cell3.value = "";
                cell4.value = formatEntryMoney(eval(returnV));
            }
            else {
                cell4.value = "";
                cell3.value = formatEntryMoney(eval(returnV));
            }
            SumMoney();
        }
        function CheckSubmit() {
            var isHasMoney = false;
            $("AllInfo").value = "";
            $("IsHasAlarm").value = "0";
            for (var i = 0; i < eval($("RowsCount").value); i++) {
                var sno = $("v1" + i);
                if (sno.value.length >= 3 && "101,102".indexOf(sno.value.substr(0, 3)) != -1) {
                    isHasMoney = true;
                    $("v4" + i).value = "";
                }
                var _allinfo = $("v0" + i).value + "!__!" + sno.value + "!__!" + $("v2" + i).value + "!__!" + $("v4" + i).value;
                if (_allinfo != "!__!!__!!__!") {
                    $("AllInfo").value += _allinfo + "!--!";
                }
            }
            if ($("ModiFlag").value == "0") { return true; }
            var TestVoucherNo = /^\d{1,6}$/;
            if (!TestVoucherNo.test($("VoucherNo").value)) {
                alert("凭证编号必须为1-6位数字！");
                $("VoucherNo").focus();
                return false;
            }
            var TestAddons = /^\d{1,3}$/;
            if ($("AddonsCount").value.length > 0 && !TestAddons.test($("AddonsCount").value)) {
                alert("附单张数必须为数字，并且最多为3位数字！");
                $("AddonsCount").focus();
                $("AddonsCount").select();
                return false;
            }
            var mbalance = 0;
            var aflag0 = 0;
            var aflag1 = 0;
            var lead = 0;
            var onloan = 0;
            var cashInfo = "";
            var allSummray = "";
            var hasEntry = false;
            var HasAlarm = false;
            var LFeeSum = 0;
            var LFee = $("AllSubjectFee").value.split("]");
            var alarm = $("BalanceAlarm").value.split("$");
            var manageSubject = $("ManageSubject").value;
            var manageSnoLength = manageSubject.length;
            for (var i = 0; i < eval($("RowsCount").value); i++) {
                var esubjectNo = $("v1" + i).value;
                if ($("v0" + i).value == "" && esubjectNo == "") {
                    continue;
                }
                else {
                    hasEntry = true;
                }
                if ($("v0" + i).value != "" && esubjectNo == "") {
                    alert("第" + (i + 1) + "行分录无科目！");
                    return false;
                }
                if ($("v2" + i).value == "" || "+,-,+0,-0,0".indexOf($("v2" + i).value) != -1) {
                    alert("第 " + (i + 1) + " 行分录发生额为空或零！");
                    return false;
                }
                else {
                    if ($("v2" + i).value.substring(0, 1) == "+") {
                        lead = NumOp(lead, eval($("v2" + i).value.substring(1, $("v2" + i).value.length)), 0);
                    }
                    if ($("v2" + i).value.substring(0, 1) == "-") {
                        onloan = NumOp(onloan, eval($("v2" + i).value.substring(1, $("v2" + i).value.length)), 0);
                    }
                    if ($("v0" + i).value == "") {
                        allSummray += "- 第 " + (i + 1) + " 行无摘要信息\n";
                    }
                }
                //现金流量指定
                //if (isHasMoney && "101,102".indexOf(esubjectNo.substr(0, 3)) == -1 && ($("v4" + i).value == "" || $("v4" + i).value == "000000")) {
                    //cashInfo += "- 第 " + (i + 1) + " 行未指定现金流量\n";
                //}
                //--
                var ebalance = eval($("v2" + i).value);
                //管理费用控制
                var HasFeeSubject = (esubjectNo.substring(0, manageSnoLength) == manageSubject);
                for (var k = 0; k < LFee.length - 1; k++) {
                    var FeeV = LFee[k].split(":");
                    if (FeeV[0] != manageSubject && esubjectNo.length >= FeeV[0].length && FeeV[1] != "0") {
                        if (esubjectNo.substring(0, FeeV[0].length) == FeeV[0]) {
                            HasFeeSubject = true;
                            if (ebalance > eval(FeeV[1])) {
                                alert("科目[" + esubjectNo + "]录入额限制不能大于：" + FeeV[1]);
                                return false;
                            }
                        }
                    }
                }
                if (HasFeeSubject) {
                    LFeeSum = NumOp(LFeeSum, ebalance, 0);
                }
                //报警校验
                if (!HasAlarm) {
                    for (var k = 0; k < alarm.length; k++) {
                        var ba = alarm[k].split("|");
                        //if (ba[0] == esubjectNo && (ba[0].substring(0, 3) == "101" ? ebalance < 0 : ebalance > 0) && Math.abs(ebalance) >= eval(ba[1])) {
                        if (ba[0] == esubjectNo && (ba[2] == "0" ? ebalance > 0 : ebalance < 0) && Math.abs(ebalance) >= eval(ba[1])) {
                            $("IsHasAlarm").value = "1";
                            HasAlarm = true;
                            break;
                        }
                    }
                }
                var esno = esubjectNo.substring(0, 3);
                if (esno == "101" || esno == "102") {
                    aflag0 = 1;
                    if (ebalance < 0) {
                        mbalance -= ebalance;
                    }
                }
                else {
                    aflag1 = 1;
                }
            }
            //管理费用总额控制
            for (var k = 0; k < LFee.length - 1; k++) {
                var FeeV = LFee[k].split(":");
                if (FeeV[0] == manageSubject) {
                    if (LFeeSum > eval(FeeV[1])) {
                        alert("管理费用总额限制不能大于：" + FeeV[1]);
                        return false;
                    }
                    break;
                }
            }
            if (!hasEntry) {
                alert("该凭证没有分录，不可保存！\n\n如果需要清除凭证分录，请作废该凭证。");
                return false;
            }
            if (lead != onloan) {
                alert("借贷不平： 借 - 贷 = " + FormatNum(NumOp(lead, onloan, 1), 1));
                return false;
            }
            var xz0 = eval('<%=TypeParse.StrToDecimal(ConfigurationManager.AppSettings["lowermoeny"], 10000) %>');
            var xz1 = eval('<%=TypeParse.StrToDecimal(CommClass.GetSysPara("lowermoeny"), 10000) %>');
            if (xz1 > xz0) {
                xz1 = xz0;
            }
            if (mbalance > xz1 && aflag0 == 1 && aflag1 == 1) {
                alert("资金的支出必须通过六步工作流程录入");
                return false;
            }
            if (HasAlarm) {
                allSummray += "\n注意：分录余额超限，该凭证必须由监督管理部门审核。\n";
            }
            if (isHasMoney) {
                if (cashInfo.length > 0) {
                    alert(cashInfo);
                    return false;
                }
                $("CashFlowFlag").value = "1";
            }
            if (allSummray != "") {
                if (!confirm(allSummray + "\n是否继续保存？")) {
                    return false;
                }
            }
            return true;
        }
        function CheckModify() {
            var _allinfo;
            $("AllInfo").value = "";
            for (var i = 0; i < eval($("RowsCount").value); i++) {
                _allinfo = $("v0" + i).value + "!__!" + $("v1" + i).value + "!__!" + $("v2" + i).value;
                if (_allinfo != "!__!!__!") { $("AllInfo").value += _allinfo + "!--!"; }
            }
            if ($("ModiFlag").value == "1") {
                return confirm("凭证已修改，但尚未保存，请及时保存，以免意外丢失数据！\n\n是否继续执行当前操作？");
            }
            return true;
        }
        function ModifyVoucherNo(msg) {
            if (CheckModify()) {
                return confirm(msg);
            }
            return false;
        }
        function DelEntryData() {
            if ($("curRowIndex").value == "") {
                alert("当删除行背景色更改后为选定状态，此时才可执行删除操作！\n\n请单击需要删除行。");
                return;
            }
            if (confirm("您确定删除该分录吗？")) {
                var lastrow = eval($("RowsCount").value) - 1;
                for (var i = eval($("curRowIndex").value); i < lastrow; i++) {
                    $("v0" + i).value = $("v0" + (i + 1)).value;
                    $("v1" + i).value = $("v1" + (i + 1)).value;
                    $("v2" + i).value = $("v2" + (i + 1)).value;
                    $("v3" + i).value = $("v3" + (i + 1)).value;
                    $(GetCellID(i, 0)).innerHTML = $(GetCellID(i + 1, 0)).innerHTML;
                    $(GetCellID(i, 1)).value = $(GetCellID(i + 1, 1)).value;
                    $(GetCellID(i, 2)).value = $(GetCellID(i + 1, 2)).value;
                    $(GetCellID(i, 3)).value = $(GetCellID(i + 1, 3)).value;
                    $(GetCellID(i, 4)).value = $(GetCellID(i + 1, 4)).value;
                }
                ClearEntryData(lastrow);
                $("ModiFlag").value = "1";
            }
        }
        function ClearEntryData(row) {
            $("v0" + row).value = "";
            $("v1" + row).value = "";
            $("v2" + row).value = "";
            $("v3" + row).value = "";
            $(GetCellID(row, 0)).innerHTML = "&nbsp;";
            $(GetCellID(row, 1)).value = "";
            $(GetCellID(row, 2)).value = "";
            $(GetCellID(row, 3)).value = "";
            $(GetCellID(row, 4)).value = "";
            SumMoney();
        }
        function CancelVouncher(m) {
            var _allinfo;
            $("AllInfo").value = "";
            for (var i = 0; i < eval($("RowsCount").value); i++) {
                _allinfo = $("v0" + i).value + "!__!" + $("v1" + i).value + "!__!" + $("v2" + i).value;
                if (_allinfo != "!__!!__!") { $("AllInfo").value += _allinfo + "!--!"; }
            }
            return confirm(m);
        }
        function padLeft(str, iLength, pad) {
            var sMsg = str.toString();
            while (sMsg.length < iLength) { sMsg = pad + sMsg; }
            return sMsg;
        }
        function FormatNum(Num, t) {
            var n = "" + Num;
            var dotpos = n.indexOf('.');
            if (dotpos == -1) {
                n += t == 0 ? "00" : ".00";
            }
            else if (dotpos == n.length - 2) {
                n += "0";
            }
            else if (dotpos < n.length - 2) {
                n = n.substring(0, n.indexOf('.') + 3);
            }
            return t == 0 ? n.replace(".", "") : n;
        }
        function NumOp(arg1, arg2, op) {
            var m = Math.pow(10, 2);
            return op == 0 ? formatFloat((arg1 * m + arg2 * m) / m) : formatFloat((arg1 * m - arg2 * m) / m);
        }
        function formatFloat(src) {
            return Math.round(src * Math.pow(10, 2)) / Math.pow(10, 2);
        }
        function AddEntryData() {
            var i = 0;
            var rcount = eval($("RowsCount").value);
            for (; i < rcount; i++) {
                if ($("v1" + i).value == "") {
                    OnCellClick($("VoucherID").value, i, 2, GetCellID(i, 2));
                    break;
                }
            }
            if (i == rcount) {
                AddNewEntryRow();
            }
        }
        function AddNewEntryRow() {
            var _allinfo;
            $("AllInfo").value = "";
            for (var i = 0; i < eval($("RowsCount").value); i++) {
                _allinfo = $("v0" + i).value + "!__!" + $("v1" + i).value + "!__!" + $("v2" + i).value;
                if (_allinfo != "!__!!__!") { $("AllInfo").value += _allinfo + "!--!"; }
            }
            $("CommonFlag").value = "1";
            __doPostBack('AddNewRow', '');
        }
        function ChooseSummary(row) {
            if ($("v1" + row).value == "") {
                alert("请录入科目！");
                return false;
            }
            var returnV = window.showModalDialog("ChooseSummary.aspx?g=" + (new Date()).getTime(), $("v0" + row).value, "dialogWidth=418px;dialogHeight=312px;center=yes;");
            if (typeof (returnV) != "undefined") {
                if (returnV == "") {
                    $(GetCellID(row, 1)).value = "";
                }
                else {
                    $(GetCellID(row, 1)).value = returnV;
                }
                $("v0" + row).value = returnV;
                $("ModiFlag").value = "1";
            }
            return false;
        }
        function AddSummary() {
            if ($("curRowIndex").value == "") {
                alert("当分录行边框颜色变为蓝色后，则为选择状态！\n\n请单击需要形成摘要的分录行。");
                return;
            }
            var a = $("v0" + $("curRowIndex").value).value;
            if (a == "") { alert("无摘要信息，存入失败！"); return; }
            if (confirm("要把“" + a + "”存入摘要库吗？")) {
                $("HideScript").src = "../AccountInit/AddSummary.aspx?Contents=" + escape(a) + "&g=" + (new Date()).getTime();
            }
        }
        function AddAddons(v) {
            var TestAddons = /^\d{1,3}$/;
            if ($("AddonsCount").value.length > 0 && !TestAddons.test($("AddonsCount").value)) {
                alert("附单张数必须为数字，并且最多为3位数字！");
                $("AddonsCount").focus();
                $("AddonsCount").select();
                return false;
            }
            $("ModiFlag").value = "1";
        }
        function UploadFile() {
            var returnV = window.showModalDialog("Appendices.aspx?id=" + $("VoucherID").value + "&g=" + (new Date()).getTime(), $("HasSelAppendices").value, "dialogWidth=750px;dialogHeight=500px;center=yes;");
            if (returnV) {
                $("HasSelAppendices").value = returnV;
                var alength = returnV.split("$").length;
                if (alength > 0 && $("AddonsCount").value.length == 0) {
                    $("AddonsCount").value = alength - 1;
                }
            }
            else {
                $("HasSelAppendices").value = "";
            }
            $("ModiFlag").value = "1";
        }
        function SetVoucherDate() {
            var returnV = window.showModalDialog("../AccountInit/SetAccountDate.aspx?g=" + (new Date()).getTime(), "1", "dialogWidth=330px;dialogHeight=226px;center=yes;");
            if (typeof (returnV) != "undefined") {
                $("ModiFlag").value = "1";
                $("VoucherDate").value = returnV[0];
                if (returnV[1] == "8") { parent.document.getElementById("ctl00_LeftFrame1_CurAccountDate").innerText = "财务日期：" + returnV[0]; }
                if (returnV[1] == "1") { $("mFrame").src = "AccountManage/MonthCarryForward.aspx"; }
            }
        }
        function SetObjectPos(OParent, offsetObj, constTop, constLeft) {
            if (constTop == undefined) {
                constTop = 2;
            }
            if (constLeft == undefined) {
                constLeft = 51;
            }
            $(offsetObj).style.display = "";
            var aTag = $(OParent);
            if (!aTag) { return; }
            var leftpos = aTag.offsetLeft;
            var toppos = aTag.offsetTop + constTop;
            while (aTag = aTag.offsetParent) {
                leftpos += aTag.offsetLeft;
                toppos += aTag.offsetTop;
            }
            $(offsetObj).style.left = leftpos + constLeft;
            $(offsetObj).style.top = toppos;
        }
        function LimitControl(DoOverLay, Overlay, Lightbox) {
            var aTag = $(DoOverLay);
            if (!aTag) { return; }
            var leftpos = aTag.offsetLeft;
            var toppos = aTag.offsetTop;
            var objHeight = aTag.offsetHeight;
            var objWidth = aTag.offsetWidth;
            do {
                aTag = aTag.offsetParent;
                leftpos += aTag.offsetLeft;
                toppos += aTag.offsetTop;
            } while (aTag.tagName != "BODY");
            $(Overlay).style.left = leftpos;
            $(Overlay).style.top = toppos;
            $(Overlay).style.height = objHeight;
            $(Overlay).style.width = objWidth;
            $(Lightbox).style.left = leftpos + (objWidth - $(Lightbox).offsetWidth) / 2;
            $(Lightbox).style.top = toppos + (objHeight - $(Lightbox).offsetHeight) / 2;
        }
        function CommitSave() {
            if ($("ModiFlag").value == "0") {
                alert("当前凭证未做改动，勿需保存！");
                $("GridView1_ctl02_txtComm1").focus();
                return;
            }
            if (CheckSubmit()) {
                $("IsCommit").value = "1";
                $("CurrentDate").value = $("VoucherDate").value;
                $("CurrentCount").value = $("AddonsCount").value;
                $("Overlay").style.display = "";
                $("SetCommit").style.display = "";
                LimitControl("AllTable", "Overlay", "SetCommit");
                $("CurrentCount").focus();
                $("CurrentCount").select();
            }
        }
        function CommitCancel() {
            $("IsCommit").value = "0";
            $("Overlay").style.display = "none";
            $("SetCommit").style.display = "none";
            $("AddonsCount").value = $("CurrentCount").value;
        }
        function CheckVoucherSave() {
            var TestAddons = /^\d{1,3}$/;
            if ($("CurrentCount").value.length > 0 && !TestAddons.test($("CurrentCount").value)) {
                alert("附单张数必须为数字，并且最多为3位数字！");
                $("CurrentCount").focus();
                $("CurrentCount").select();
                return false;
            }
            $("SetCommit").style.display = "none";
            $("Overlay").style.display = "";
            $("Lightbox").style.display = "";
            $("Lightbox").innerHTML = "正在保存凭证，请稍候......";
            LimitControl("AllTable", "Overlay", "Lightbox");
            return true;
        }
        function SetAccountDate(rowid, d) {
            if ($("CurDateCell").value != "") {
                $($("CurDateCell").value).style.color = "red";
                $($("CurDateCell").value).style.backgroundColor = "";
            }
            $(rowid).style.backgroundColor = "red";
            $(rowid).style.color = "white";
            $("CurDateCell").value = rowid;
            $("VoucherDate").value = $("VoucherDate").value.replace(/\d{2}日/, d + "日");
            $("CurrentDate").value = $("CurrentDate").value.replace(/\d{2}日/, d + "日");
        }
        function OnEnterKey() {
            if (event.keyCode != 13 && event.keyCode != 32 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40) {
                return true;
            }
            var target = event.target || event.srcElement;
            var oid = target.getAttribute("id");
            if (oid.indexOf("txtComm") == -1) {
                if (event.keyCode == 13) {
                    if ($("IsCommit").value == "1") {
                        if (CheckVoucherSave()) {
                            __doPostBack('CommitVoucher', '');
                        }
                    }
                    else {
                        CommitSave();
                    }
                }
                return false;
            }
            var oname = oid.split("_");
            var pos = eval(oid.substring(oid.length - 1));
            if (event.keyCode == 13) {
                var row = eval(oname[1].substring(3)) - 2;
                if (pos == "2" && target.value == "") {
                    if ($("IsCommit").value == "1") {
                        if (CheckVoucherSave()) {
                            __doPostBack('CommitVoucher', '');
                        }
                    }
                    else {
                        CommitSave();
                    }
                    return false;
                }
                if (pos == "4") {
                    pos = eval(oname[1].substring(3));
                    var rowsCount = eval($("RowsCount").value) + 2;
                    if (pos < rowsCount) {
                        pos++;
                    }
                    else {
                        if ($("IsCommit").value == "1") {
                            if (CheckVoucherSave()) {
                                __doPostBack('CommitVoucher', '');
                            }
                        }
                        else {
                            CommitSave();
                        }
                        return false;
                    }
                    var p = "" + pos;
                    if (p.length == 1) {
                        p = "0" + p;
                    }
                    //OnCellClick($("VoucherID").value, pos - 2, 2, GetCellID(pos - 2, 2));
                    $(GetCellID(pos - 2, 1)).focus();
                }
                else {
                    JumpNextTextBox(oname, 39);
                }
                return false;
            }
            if (event.keyCode == 32) {
                if (pos == "3") {
                    $(oname[0] + "_" + oname[1] + "_txtComm4").value = $(oname[0] + "_" + oname[1] + "_txtComm3").value;
                    $(oname[0] + "_" + oname[1] + "_txtComm4").focus();
                    $(oname[0] + "_" + oname[1] + "_txtComm4").select();
                    $(oname[0] + "_" + oname[1] + "_txtComm3").value = "";
                }
                if (pos == "4") {
                    $(oname[0] + "_" + oname[1] + "_txtComm3").value = $(oname[0] + "_" + oname[1] + "_txtComm4").value;
                    $(oname[0] + "_" + oname[1] + "_txtComm3").focus();
                    $(oname[0] + "_" + oname[1] + "_txtComm3").select();
                    $(oname[0] + "_" + oname[1] + "_txtComm4").value = "";
                }
            }
            JumpNextTextBox(oname,event.keyCode);
        }
        function JumpNextTextBox(oname,keyCode) {
            var row = eval(oname[1].substring(3)) - 2;
            if (keyCode == 37) {
                var pos = eval(oname[2].substring(oname[2].length - 1));
                if (pos > 1) {
                    pos--;
                }
                else {
                    return false;
                }
                if (pos == 2) {
                    //OnCellClick($("VoucherID").value, row, 2, GetCellID(row, 2));
                    $(GetCellID(row, 2)).focus();
                }
                else {
                    $(oname[0] + "_" + oname[1] + "_txtComm" + pos).focus();
                }
            }
            if (keyCode == 39) {
                var pos = eval(oname[2].substring(oname[2].length - 1));
                if (pos < 4) {
                    pos++;
                }
                else {
                    row++;
                    //OnCellClick($("VoucherID").value, row, 2, GetCellID(row, 2));
                    $(GetCellID(row, 1)).focus();
                    return false;
                }
                if (pos == 2) {
                    //OnCellClick($("VoucherID").value, row, 2, GetCellID(row, 2));
                    $(GetCellID(row, 2)).focus();
                    return false;
                }
                if (pos == 4 && $(GetCellID(row, 3)).value != "") {
                    row++;
                    //OnCellClick($("VoucherID").value, row, 2, GetCellID(row, 2));
                    $(GetCellID(row, 1)).focus();
                    return false;
                }
                $(oname[0] + "_" + oname[1] + "_txtComm" + pos).focus();
            }
            if (keyCode == 38) {
                var pos = eval(oname[1].substring(3));
                if (pos > 2) {
                    pos--;
                }
                else {
                    return false;
                }
                var p = "" + pos;
                if (p.length == 1) {
                    p = "0" + p;
                }
                $(oname[0] + "_ctl" + p + "_" + oname[2]).focus();
            }
            if (keyCode == 40) {
                var pos = eval(oname[1].substring(3));
                var rowsCount = eval($("RowsCount").value) + 1;
                if (pos < rowsCount) {
                    pos++;
                }
                else {
                    return false;
                }
                var p = "" + pos;
                if (p.length == 1) {
                    p = "0" + p;
                }
                $(oname[0] + "_ctl" + p + "_" + oname[2]).focus();
            }
        }
        function RefreshD() {
            $("UpdateCtlTime").src = "UpdateCTLTime.aspx?g=" + (new Date()).getTime();
            setTimeout("RefreshD()", 10000);
        }
        document.onclick = function onClick(ev) {
            ev = ev || window.event;
            var target = ev.target || ev.srcElement;
            var oid = target.getAttribute("id");
            if (("ControlDiv,CellText").indexOf(oid) == -1 || oid == "") {
                $("ControlDiv").style.display = "none";
            }
        }
        window.onload = function () {
            SetTextBoxAttr();
            noAutoInput();
            SumMoney();
            if ($("CTLFlag").value == "0") { RefreshD(); }
            if ($("EnableFlag").value == "1" || $("EnableFlag").value == "2") {
                $("AddEntry").disabled = "disabled";
                $("AddNewRow").disabled = "disabled";
                $("DelEntry").disabled = "disabled";
                $("HAddSummary").disabled = "disabled";
                $("SaveVoucher").disabled = "disabled";
                $("DelVouncher").disabled = "disabled";
                if ($("EnableFlag").value == "2") {
                    $('UVoucherNo').disabled = "disabled";
                    $('DVoucherNo').disabled = "disabled";
                    $('CreateVoucher').disabled = "disabled";
                }
                //$("DoSettle").style.display = "none";
            }
            else {
                $("GridView1_ctl02_txtComm1").focus();
            }
            LimitControl("DoOverLay", "Overlay", "Lightbox");
            if ($("CommonFlag").value == "1") {
                var r = eval($("RowsCount").value) - 1;
                //OnCellClick($("VoucherID").value, r, 2, GetCellID(r, 2));
                //AddEntryData();
                $(GetCellID(r, 2)).focus();
                $("CommonFlag").value = "0";
            }
        }
        function CheckNumber() {
            $("ModiFlag").value = "1";
            return event.keyCode >= 48 && event.keyCode <= 57;
        }
        function SetTextBoxAttr() {
            var inputs = document.getElementById("GridView1").getElementsByTagName("input");
            for (i = 0; i < inputs.length; i++) {
                var txt = inputs[i];
                if (txt.id.indexOf("txtComm") != -1) {
                    txt.onfocus = onTextBoxFocus;
                    txt.onblur = onTextBoxBlur;
                }
            }
            inputs = document.getElementById("GridView1").getElementsByTagName("textarea");
            for (i = 0; i < inputs.length; i++) {
                var txt = inputs[i];
                if (txt.id.indexOf("txtComm") != -1) {
                    txt.onfocus = onTextBoxFocus;
                    txt.onblur = onTextBoxBlur;
                }
            }
        }
        function onTextBoxFocus() {
            var oid = this.getAttribute("id");
            var oname = oid.split("_");
            var row = eval(oname[1].substring(3)) - 2;
            if (eval($("RowsCount").value) == row) { return false; }
            var pos = eval(oid.substring(oid.length - 1));
            OnRowClick(row, oname[0] + "_" + oname[1]);
            if (pos == "1") {
                if ($("v1" + row).value.length == 0) {
                    //$(oid.substring(0, oid.length - 1) + "2").focus();
                    //OnCellClick($("VoucherID").value, row, 2, GetCellID(i, 2));
                }
            }
            if (pos == "2") {
                this.value = $("v1" + row).value;
            }
            if (pos == "3" || pos == "4") {
                if (eval(this.value) == 0) this.value = '';
                if ($("v1" + row).value.length == 0) {
                    $(oid.substring(0, oid.length - 1) + "2").focus();
                    //OnCellClick($("VoucherID").value, row, 2, GetCellID(row, 2));
                    $("v0" + row).value = "";
                    $(GetCellID(row, 1)).value = "";
                }
                else {
                    if (this.value.length == 0 && $("v2" + row).value.length == 0) {
                        var lead = 0;
                        var onloan = 0;
                        for (var i = 0; i < row; i++) {
                            if ($("v2" + i).value != "") {
                                if ($("v2" + i).value.substring(0, 1) == "+") {
                                    lead = NumOp(lead, eval($("v2" + i).value.substring(1, $("v2" + i).value.length)), 0);
                                }
                                if ($("v2" + i).value.substring(0, 1) == "-") {
                                    onloan = NumOp(onloan, eval($("v2" + i).value.substring(1, $("v2" + i).value.length)), 0);
                                }
                            }
                        }
                        if (lead > onloan) {
                            if (pos == "4") {
                                this.value = lead - onloan;
                                this.value = formatEntryMoney(this.value);
                            }
                        }
                        else {
                            if (pos == "3" && lead != onloan) {
                                this.value = onloan - lead;
                                this.value = formatEntryMoney(this.value);
                            }
                        }
                    }
                }
            }
            this.className = 'txtFocus';
            this.select();
            this.focus();
        }
        function onTextBoxBlur() {
            var oid = this.getAttribute("id");
            var oname = oid.split("_");
            var row = eval(oname[1].substring(3)) - 2;
            var pos = eval(oid.substring(oid.length - 1));
            if (pos == "1") {
                if($("v0" + row).value != this.value)
                {
                    $("ModiFlag").value = "1";
                }
                $("v0" + row).value = this.value;
            }
            if (pos == "2") {
                if (this.value.length > 0) {
                    OnSubjectNoBlur(this.getAttribute("snid"), oid, row, this.value);
                    if ($("v0" + row).value.length == 0) {
                        WriteSummray(row, true);
                    }
                }
                else {
                    $("v2" + row).value = "";
                    $(this.getAttribute("snid")).innerHTML = '&nbsp;';
                    $(oid.substring(0, oid.length - 1) + "3").value = "";
                    $(oid.substring(0, oid.length - 1) + "4").value = "";
                }
            }
            if (pos == "3") {
                if (this.value.length == 0 || eval(this.value) == 0) {
                    this.value = "";
                    if ($(oid.substring(0, oid.length - 1) + "4").value == "") {
                        $("v2" + row).value = "";
                    }
                }
                else {
                    $(oid.substring(0, oid.length - 1) + "4").value = "";
                    this.value = formatEntryMoney(eval(this.value));
                    $("v2" + row).value = "+" + this.value;
                }
            }
            if (pos == "4") {
                if (this.value.length == 0 || eval(this.value) == 0) {
                    this.value = '';
                    if ($(oid.substring(0, oid.length - 1) + "3").value == "") {
                        $("v2" + row).value = "";
                    }
                }
                else {
                    $(oid.substring(0, oid.length - 1) + "3").value = "";
                    this.value = formatEntryMoney(eval(this.value));
                    $("v2" + row).value = "-" + this.value;
                }
            }
            this.className = 'txtBlur';
            SumMoney();
        }
        function OnSubjectNoBlur(cellid1, cellid2, row, v) {
            if ($("v1" + row).value != v) {
                $("v1" + row).value = v;
                $("ModiFlag").value = "1";
            }
            if (v == "") {
                $(cellid1).innerHTML = "&nbsp";
                $(cellid2).value = "&nbsp";
            }
            else {
                $("HideScript").src = "GetSubjectName.aspx?cellid1=" + cellid1 + "&cellid2=" + cellid2 + "&row=" + row + "&no=" + v + "&g=" + (new Date()).getTime();
            }
        }
        function WriteSummray(row, pass) {
            if ($("v1" + row).value != "" || pass) {
                $("v0" + row).value = $("GridView1_ctl02_txtComm1").value;
                $(GetCellID(row, 1)).value = $("GridView1_ctl02_txtComm1").value;
            }
        }
        function SumMoney() {
            var crow = eval($("RowsCount").value) + 2;
            if (crow < 10) { crow = "0" + crow; }
            var lead = 0;
            var onloan = 0;
            var hasBank = false;
            for (var i = 0; i < eval($("RowsCount").value); i++) {
                if ($("v1" + i).value.length >= 3 && $("v1" + i).value.substr(0, 3) == "102") {
                    hasBank = true;
                }
                if ($("v2" + i).value != "") {
                    if ($("v2" + i).value.substring(0, 1) == "+") {
                        lead = NumOp(lead, eval($("v2" + i).value.substring(1, $("v2" + i).value.length)), 0);
                    }
                    if ($("v2" + i).value.substring(0, 1) == "-") {
                        onloan = NumOp(onloan, eval($("v2" + i).value.substring(1, $("v2" + i).value.length)), 0);
                    }
                }
            }
            if (hasBank) {
                //$("DoSettle").style.display = "";
            }
            else {
                //$("DoSettle").style.display = "none";
            }
            $("GridView1_ctl" + crow + "_txtComm3").value = formatEntryMoney(eval(lead));
            $("GridView1_ctl" + crow + "_txtComm4").value = formatEntryMoney(eval(onloan));
        }
        function formatEntryMoney(src) {
            src = Math.abs(src);
            src = Math.round(src * Math.pow(10, 2)) / Math.pow(10, 2);
            return src.toFixed(2);
            var m = "" + src.toLocaleString(2);
            if (m.indexOf(".") == 0) {
                m = "0" + m;
            }
            return m;
        }
        function noAutoInput() {
            var inputs = document.getElementById("GridView1").getElementsByTagName("input");
            for (i = 0; i < inputs.length; i++) {
                inputs[i].setAttribute("autocomplete", "off");
            }
            inputs = document.getElementById("GridView1").getElementsByTagName("textarea");
            for (i = 0; i < inputs.length; i++) {
                dochange2(inputs[i]);
            }
        }
        function dochange(txt) {
            if(txt.scrollHeight!=txt.style.posHeight)
            {
                txt.style.posHeight=Math.max(txt.scrollHeight,35);
                SetObjectPos($("AddonsCellID").value, "AddonsDiv");
            }
        }
        function dochange2(txt) {
            if(txt.scrollHeight!=txt.style.posHeight)
            {
                txt.style.posHeight=Math.max(txt.scrollHeight,35);
            }
        }
    </script>
</head>
<body style="font-size: 10pt" onkeydown="return OnEnterKey();">
    <form id="form1" runat="server">
    <div>
        <br />
        <table id="AllTable" cellpadding="0" cellspacing="0" style="width: 750px">
            <tr id="DoOverLay">
                <td class="t2" style="background-position: center center; background-repeat: no-repeat;
                    height: 270px; text-align: center; vertical-align: top">
                    <table cellpadding="0" cellspacing="0" style="width: 690px">
                        <tr>
                            <td style="vertical-align: top; height: 103px; background-position: center bottom;
                                background-image: url(../Images/pzhead.jpg); background-repeat: no-repeat;" colspan="4">
                                <table cellpadding="0" cellspacing="0" style="width: 657px">
                                    <tr>
                                        <td class="pzhead" colspan="3" style="height: 40px; text-align: left; vertical-align: bottom">
                                            <asp:TextBox ID="AccountName" runat="server" ReadOnly="True" Width="150px" Font-Bold="True"
                                                ForeColor="Blue" BorderWidth="0px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 46%; height: 22px;">
                                            &nbsp;
                                        </td>
                                        <td class="pzhead" style="width: 40%; text-align: left; vertical-align: bottom">
                                            <asp:TextBox ID="VoucherDate" runat="server" Font-Bold="True" ForeColor="Blue" Width="112px"
                                                BorderWidth="0px"></asp:TextBox>
                                        </td>
                                        <td class="pzhead" style="width: 14%; text-align: left; vertical-align: top">
                                            <asp:TextBox ID="VoucherNo" runat="server" Width="50px" Font-Bold="True" ForeColor="Blue"
                                                Height="15px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 17px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 118px; text-align: center; vertical-align: top" colspan="4">
                                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CssClass="gridview"
                                    AutoGenerateColumns="False" CaptionAlign="Left" ShowHeader="False" Width="671px"
                                    OnRowDataBound="GridView1_RowDataBound">
                                    <RowStyle Font-Size="10pt" Height="21px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="摘要">
                                            <ItemStyle CssClass="dd" Width="160px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtComm1" runat="server" CssClass="txtBlur" Style="text-align:left; overflow:hidden;" TextMode="MultiLine" onpropertychange="dochange(this)"></asp:TextBox>
                                            </ItemTemplate>
                                            <ControlStyle Width="157px" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="总账科目">
                                            <ItemStyle CssClass="dd" Width="104px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="明细科目">
                                            <ItemStyle CssClass="dd" Width="140px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtComm2" runat="server" CssClass="txtBlur" Style="text-align:left; overflow:hidden;" TextMode="MultiLine" onpropertychange="dochange(this)"></asp:TextBox>
                                            </ItemTemplate>
                                            <ControlStyle Width="137px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="l0">
                                            <ItemStyle CssClass="dd" Width="122px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtComm3" runat="server" CssClass="txtBlur" Style="text-align:right;"></asp:TextBox>
                                            </ItemTemplate>
                                            <ControlStyle Width="119px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="o0">
                                            <ItemStyle CssClass="bb" Width="122px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtComm4" runat="server" CssClass="txtBlur" Style="text-align:right;"></asp:TextBox>
                                            </ItemTemplate>
                                            <ControlStyle Width="119px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings Visible="False" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 24px; width: 35%; text-align: left">
                                &nbsp; &nbsp; &nbsp; <strong><span style="color: Red">主管会计：</span></strong><asp:Label
                                    ID="Director" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                            <td style="height: 24px; width: 23%; text-align: left">
                                <span style="color: Red"><strong>记账：</strong></span><asp:Label ID="Accountant" runat="server"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td style="height: 24px; width: 22%; text-align: left">
                                <span style="color: Red"><strong>审核：</strong></span><asp:Label ID="Assessor" runat="server"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td style="height: 24px; width: 20%; text-align: left">
                                <span style="color: Red"><strong>制证：</strong></span><asp:Label ID="DoBill" runat="server"
                                    ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="BtnCtlPanel">
                <td class="t4" style="height: 35px; text-align: center">
                    <input id="AddEntry" type="button" value="增加分录" onclick="AddEntryData();" style="width: 60px" tabindex="0" />&nbsp;
                    <input id="DelEntry" type="button" value="删除分录" onclick="DelEntryData();" style="width: 60px" />
                    <asp:TextBox ID="TextBox2" MaxLength="2" runat="server" Style="border:1px solid #000000; width:40px; height:19px"></asp:TextBox>&nbsp;<asp:Button ID="AddNewRow" runat="server" Text="增加行" OnClick="AddNewRow_Click" Width="50px" />&nbsp;
                    <input id="HAddSummary" type="button" value="形成摘要" onclick="AddSummary();" style="width: 60px;" />&nbsp;
                    <asp:Button ID="PreVoucher" runat="server" Text="上张" OnClick="PreVoucher_Click" Width="35px" />&nbsp;
                    <asp:DropDownList ID="VoucherNoList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="VoucherList_SelectedIndexChanged">
                    </asp:DropDownList>&nbsp;
                    <asp:Button ID="NextVoucher" runat="server" Text="下张" OnClick="NextVoucher_Click"
                        Width="35px" />&nbsp;
                    <asp:Button ID="UVoucherNo" runat="server" Text="号前提" OnClick="UVoucherNo_Click"
                        Width="50px" />&nbsp;
                    <asp:Button ID="DVoucherNo" runat="server" Text="号后推" OnClick="DVoucherNo_Click"
                        Width="50px" />&nbsp;
                    <asp:Button ID="CancelVouncher" runat="server" Text="作废凭证" OnClick="CancelVouncher_Click"
                        Width="68px" Visible="false" />
                    <input id="SaveVoucher" type="button" value="保存凭证" onclick="CommitSave();" style="width: 60px" />&nbsp;
                    <asp:Button ID="DelVouncher" runat="server" Text="删除" OnClick="DelVouncher_Click"
                        Width="40px" />&nbsp;
                    <asp:Button ID="CreateVoucher" runat="server" Text="新建凭证" OnClick="CreateVoucher_Click"
                        Width="60px" />
                </td>
            </tr>
        </table>
    </div>
    <div id="Overlay" runat="server" class="Overlay">
    </div>
    <div id="Lightbox" runat="server" class="Lightbox">
    </div>
    <div id="SetCommit" class="Lightbox">
        <table cellpadding="0" cellspacing="0" style="width: 671px">
            <tr>
                <td class="t2" colspan="4" style="height: 65px; text-align: center">
                    <span id="PageTitle" style="font-size: 18pt; font-family: 隶书" runat="server">请确认以下各项内容</span>
                </td>
            </tr>
            <tr>
                <td class="t1" colspan="1" style="height: 31px; text-align: center; width: 253px;">
                    凭证日期：
                </td>
                <td class="t1" colspan="1" style="height: 31px; text-align: center; width: 293px;">
                    <asp:TextBox ID="CurrentDate" runat="server" BorderWidth="1px" Width="182px" ForeColor="Blue"
                        CssClass="pcenter"></asp:TextBox>
                </td>
                <td class="t2" colspan="2" rowspan="2" style="text-align: center">
                    <asp:GridView ID="GridView2" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        CaptionAlign="Left" ShowHeader="False" Width="275px">
                        <PagerSettings Visible="False" />
                        <RowStyle Font-Size="10pt" Height="15px" />
                        <Columns>
                            <asp:BoundField>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="t1" colspan="1" style="height: 33px; text-align: center; width: 253px;">
                    附单张数：
                </td>
                <td class="t1" colspan="1" style="height: 33px; text-align: center; width: 293px;">
                    <asp:TextBox ID="CurrentCount" runat="server" BorderWidth="1px" Font-Bold="True"
                        ForeColor="Blue" MaxLength="3" Width="182px" CssClass="pcenter"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 60px; text-align: center">
                    <asp:Button ID="CommitVoucher" runat="server" Text="保存凭证" OnClick="SaveVoucher_Click"
                        Width="68px" />&nbsp;&nbsp;
                    <input id="CancelCommit" type="button" value="取消" onclick="CommitCancel();" style="width: 68px" />
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">$("SetCommit").style.display = "none";</script>
    <div id="ControlDiv" style="border-right: red 1px solid; border-top: red 1px solid;
        display: none; z-index: 1; left: 519px; border-left: red 1px solid; width: 95px;
        border-bottom: red 1px solid; position: absolute; top: 133px; height: 20px; background-color: #f6f6f6;
        text-align: center">
        <asp:TextBox ID="CellText" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="93px"></asp:TextBox>
    </div>
    <div id="AddonsDiv" style="z-index: 1; left: 243px; width: 60px; position: absolute;
        top: 428px; text-align: center">
        <asp:TextBox ID="AddonsCount" runat="server" BorderStyle="Solid" Style="border: 0px solid red;
            border-bottom-width: 1px; text-align: right; color: Red; font-size: 10pt" MaxLength="3"
            Width="45px"></asp:TextBox>
    </div>
    <div id="EntryRowIipDiv0" style="z-index: 999; left: 243px; width: 669px; position: absolute;
        top: 428px; text-align: center; height:2px; display:none; background-color:Blue">
    </div>
    <div id="EntryRowIipDiv1" style="z-index: 999; left: 243px; width: 669px; position: absolute;
        top: 428px; text-align: center; height:2px; display:none; background-color:Blue">
    </div>
    <asp:HiddenField ID="RedFigureFlag" runat="server" Value="0" />
    <asp:HiddenField ID="EnableFlag" runat="server" Value="0" />
    <asp:HiddenField ID="CommonFlag" runat="server" Value="0" />
    <asp:HiddenField ID="CTLFlag" runat="server" Value="0" />
    <asp:HiddenField ID="TempVoucher" runat="server" />
    <asp:HiddenField ID="TempValue" runat="server" />
    <asp:HiddenField ID="VoucherID" runat="server" />
    <asp:HiddenField ID="AddonsCellID" runat="server" />
    <asp:HiddenField ID="HasSelAppendices" runat="server" />
    <asp:HiddenField ID="AllInfo" runat="server" />
    <asp:HiddenField ID="RowsCountMin" runat="server" Value="5" />
    <asp:HiddenField ID="RowsCount" runat="server" Value="5" />
    <asp:HiddenField ID="curRowID" runat="server" />
    <asp:HiddenField ID="curRowIndex" runat="server" />
    <asp:HiddenField ID="RecordRowIndex" runat="server" Value="0" />
    <asp:HiddenField ID="ModiFlag" runat="server" Value="0" />
    <asp:HiddenField ID="aDay" runat="server" Value="2" />
    <asp:HiddenField ID="aMonth" runat="server" Value="1" />
    <asp:HiddenField ID="aYear" runat="server" Value="2008" />
    <asp:HiddenField ID="tDay" runat="server" Value="20" />
    <asp:HiddenField ID="tMonth" runat="server" Value="5" />
    <asp:HiddenField ID="tYear" runat="server" Value="2008" />
    <asp:HiddenField ID="tWeek" runat="server" Value="一" />
    <asp:HiddenField ID="CurDateCell" runat="server" />
    <asp:HiddenField ID="AllSubjectFee" runat="server" />
    <asp:HiddenField ID="ManageSubject" runat="server" Value="541" />
    <asp:HiddenField ID="BalanceAlarm" runat="server" />
    <asp:HiddenField ID="IsHasAlarm" runat="server" Value="0" />
    <asp:HiddenField ID="CashFlowFlag" runat="server" Value="0" />
    <asp:HiddenField ID="LastVoucherFlag" runat="server" />
    <asp:HiddenField ID="CurEntryRow" runat="server" />
    <asp:HiddenField ID="IsCommit" runat="server" Value="0" />
    <asp:HiddenField ID="CarryVoucherNo" runat="server" Value="000000" />
    <script type="text/javascript">SetObjectPos($("AddonsCellID").value, "AddonsDiv");</script>
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    <asp:Label ID="ExeScript2" runat="server"></asp:Label>
    </form>
</body>
</html>
