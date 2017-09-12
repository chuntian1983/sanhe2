<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountManage_Appendices" Codebehind="Appendices.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>凭证附单</title>
<link rel="stylesheet" type="text/css" href="/uploadify/uploadify.css" />

<script src="/uploadify/jquery.min.js" type="text/javascript"></script>
<script src="/uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("#file_upload").uploadify({
            swf: '/uploadify/uploadify.swf',
            uploader: '/uploadify/upload.ashx?act=2&ym=' + $('#YearMonth').val(),
            width: 50,
            height: 20,
            queueID: 'uploadqueue',
            buttonText: '上传',
            progressData: 'percentage',
            fileTypeDesc: '图片文件',
            fileTypeExts: '*.jpg;*.png;*.gif;*.bmp;*.jpeg',
            fileSizeLimit: '5120KB',
            removeCompleted: false,
            removeTimeout: 0,
            onDialogClose: function (queueData) {
                if (queueData.filesQueued > 0) {
                    $("#divdata").css("display", "none");
                    $("#uploadqueue").css("display", "");
                }
            },
            onQueueComplete: function (queueData) {
                $("#divdata").css("display", "none");
                $("#uploadqueue").css("display", "");
                __doPostBack('DoPostBack', '');
            }
        });
        var obj = g("HasSelAppendices");
        if (obj.value == "") {
            obj.value = window.dialogArguments;
        }
        var imgs = obj.value.split("$");
        obj.value = "";
        for (var i = 0; i < imgs.length; i++) {
            AddAppendix(imgs[i]);
        }
        resetDialogSize();
    });
</script>
<script type="text/javascript">
    var FScanX;
    window.onload = function () {
        // resetDialogSize();
        FScanX = document.getElementById("scanx");

        //loadimg();
    }
    function Scan() {
        FScanX = document.getElementById("FScanX");

        FScanX.ScanShowUI = "0"; //1为显示，0为不显示
        FScanX.ShowThumb = "0"; //1为显示，0为不显示

        FScanX.ScanSourceType = "0";
        FScanX.ScanPixelType = "2"; //灰阶1彩色2
        FScanX.ScanResolution = "200"; //分辨率
        FScanX.Brightness = "0";
        FScanX.Contrast = "0";
        var FilePath = "D:\\test";
        var FilePrefix = "img";
        var StartIndex = "1";
        var IndexLength = "5";
        FScanX.SetImageName(FilePath, FilePrefix, StartIndex, IndexLength);

        FScanX.ImageFormat = "1";
        FScanX.CompressionRate = "70";
        FScanX.TiffCompressType = "0";
        FScanX.PDFCompressType = "0";
        FScanX.Scan();

        var ScanImageCount = FScanX.GetScanImageCount();
        for (var i = 1; i <= ScanImageCount; i++) {
            upfile(FScanX.GetScanImagePath(i));
        }
          __doPostBack('DoPostBack', '');
        //        var ScanImageCount = FScanX.GetScanImageCount();
        //        document.getElementById("ScanImageCount").value = ScanImageCount;
        //        document.getElementById("StartIndex").value = parseInt(StartIndex) + ScanImageCount;
    }
    function upfile(fpath) {

        var result = FScanX.bUpLoadImage(fpath, 'localhost', '7945', '/upload.aspx?aid=<%=UserInfo.AccountID%>&ym=' + $('#YearMonth').val());
    }

    /*function GetBar() {
    var BarCount = FScanX.GetBarCount();
    //alert(BarCount);
    var BarData = "";
    for (var i = 0; i < BarCount;i++)
    {
    BarData = BarData + FScanX.GetBarData(i)+"\n";
    }
    document.getElementById("BarData").value = BarData;
    }*/
    
    </script>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<base target="_self" />
<script type="text/javascript">
function g(o) { return (typeof (o) == "object") ? o : document.getElementById(o); }
function AddAppendix(aid) {
    if (aid.length == 0) { return; }
    if (g("HasSelAppendices").value.indexOf(aid) == -1) {
        var au = g("A" + aid);
        if (au) {
            var aurl = au.href;
            var iurl = g("Img" + aid).src;
            var oDiv = document.createElement("DIV");
            oDiv.id = "D" + aid;
            oDiv.innerHTML += "<a href='" + aurl + "' target=_blank><img src='" + iurl + "' border=0 /></a><br>";
            oDiv.innerHTML += "<a href='javascript:void(0)' onclick=_removeChild('" + aid + "')>删除</a><br>";
            g("SelAppendices").appendChild(oDiv);
        }
        g("HasSelAppendices").value += aid + "$";
    }
    else {
        alert("该附单已被选择！");
    }
}
function _removeChild(aid) {
    g("SelAppendices").removeChild(g("D" + aid));
    g("HasSelAppendices").value = g("HasSelAppendices").value.replace(aid + "$", "");
}
function resetDialogSize() {
    var ua = navigator.userAgent;
    if (ua.lastIndexOf("MSIE 7.0") == -1) {
        var height = document.body.offsetHeight;
        var width = document.body.offsetWidth;
        if (ua.lastIndexOf("Windows NT 5.2") == -1) {
            window.dialogHeight = (height + 53) + "px";
            window.dialogWidth = (width + 6) + "px";
        }
        else {
            window.dialogHeight = (height + 46) + "px";
            window.dialogWidth = (width + 6) + "px";
        }
    }
}
</script>



</head>
<body style="margin:0px" onunload="window.returnValue=g('HasSelAppendices').value;">
    <form id="form1" runat="server">
    <asp:HiddenField ID="HasSelAppendices" runat="server" />
    <asp:HiddenField ID="YearMonth" runat="server" />
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 720px">
            <tr style="background-color:#f8f8f8">
                <td class="t1" style="width: 150px; text-align: center;">
                已选附单
                </td>
                <td colspan="7" class="t2" style="height: 25px; text-align: center;">
                附单列表
                </td>
            </tr>
            <tr>
                <td rowspan="3" class="t3" 
                    style="height: 458px; text-align: center; vertical-align:top">
                    <div id="SelAppendices" style="width: 100%; height: 100%;overflow-y:scroll;"><div style="height:5px" /></div>
                </td>
                <td colspan="7" class="t2" 
                    style="height: 420px; text-align: center; vertical-align:top">
                    <div id="divdata" style="width:100%;height:100%;overflow-y:scroll;display:block">
                        <asp:DataList ID="AppendixList" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table" CellSpacing="0" CellPadding="0" OnItemDataBound="AppendixList_ItemDataBound">
                            <ItemTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" style="text-align:center; height: 120px; width:135px;">
                                  <tr>
                                    <td><a id="A<%# Eval("ParaName") %>" href="<%# Eval("ParaValue") %>" target="_blank"><img id="Img<%# Eval("ParaName") %>" alt="" src="<%# Eval("DefValue") %>" border="0" /></a></td>
                                  </tr>
                                  <tr>
                                    <td>
                                        <a href="javascript:void(0)" onclick="AddAppendix('<%# Eval("ParaName") %>')">选择</a>
                                        &nbsp;&nbsp;
                                        <asp:LinkButton ID="btnHide" CommandName='<%# Bind("ParaName") %>' CommandArgument='<%# Bind("ParaType") %>' OnClick="btnControl_Click" runat="server">隐藏</asp:LinkButton>
                                        &nbsp;&nbsp;
                                        <asp:LinkButton ID="btnDelete" CommandName='<%# Bind("ParaName") %>' CommandArgument='<%# Bind("DefValue") %>' OnClick="btnControl_Click" runat="server">删除</asp:LinkButton>
                                    </td>
                                  </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div id="uploadqueue" style="display:none;width:100%;height:400px;overflow-x:hidden;overflow-y:scroll"></div>
                </td>
            </tr>
            <tr>
                <td class="t3" style="height: 28px; text-align: center; background-color:#f8f8f8; width:80px">
                    附单查询：
                </td>
                <td class="t3" style="height: 28px; text-align: center; background-color:#f8f8f8; width:170px">
                    <asp:RadioButtonList ID="ShowType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="X">全部</asp:ListItem>
                        <asp:ListItem Selected="True" Value="1">显示</asp:ListItem>
                        <asp:ListItem Value="0">隐藏</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="t3" style="height: 28px; text-align: center; background-color:#f8f8f8; width:140px">
                    <asp:DropDownList ID="SelYear" runat="server"></asp:DropDownList>
                    <asp:DropDownList ID="SelMonth" runat="server">
                        <asp:ListItem Value="01">01月</asp:ListItem>
                        <asp:ListItem Value="02">02月</asp:ListItem>
                        <asp:ListItem Value="03">03月</asp:ListItem>
                        <asp:ListItem Value="04">04月</asp:ListItem>
                        <asp:ListItem Value="05">05月</asp:ListItem>
                        <asp:ListItem Value="06">06月</asp:ListItem>
                        <asp:ListItem Value="07">07月</asp:ListItem>
                        <asp:ListItem Value="08">08月</asp:ListItem>
                        <asp:ListItem Value="09">09月</asp:ListItem>
                        <asp:ListItem Value="10">10月</asp:ListItem>
                        <asp:ListItem Value="11">11月</asp:ListItem>
                        <asp:ListItem Value="12">12月</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t3" style="height: 28px; text-align: center; background-color:#f8f8f8; width:60px">
                    <asp:Button ID="Button2" runat="server" Text="查询" OnClick="Button2_Click" Width="49px" />
                </td>
                <td class="t b" style="height: 28px; text-align: center; background-color:#f8f8f8; width:60px">
                    <div style="margin-top:6px"><input id="file_upload" name="file_upload" type="file" multiple="true"></div>
                </td>
                <td class="t b r" style="height: 28px; text-align: center; background-color:#f8f8f8; width:60px">
                    <input onclick="Scan();" id="btnsaomiao" style="width:40px" type="button" value="扫描" /></td>
                <td class="t b r" style="height: 28px; text-align: center; background-color:#f8f8f8; width:60px">
                    <input id="Button3" type="button" value="关闭" onclick="window.close();" />
                    <asp:LinkButton ID="DoPostBack" runat="server" onclick="DoPostBack_Click"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="t3" style="height: 28px; text-align: center; background-color:#f8f8f8; width:80px">
                    &nbsp;</td>
                <td class="t3" style="height: 28px; text-align: center; background-color:#f8f8f8; width:170px">
                    <object id="FScanX" classid="clsid:9134F6A9-1FF8-420F-9E9E-DDD374C19715" 
                        height="1" width="1">
                    </object>
                </td>
                <td class="t3" style="height: 28px; text-align: center; background-color:#f8f8f8; width:140px">
                    <input onclick="FScanX.bUpLoadImage('D:\\test\\img00001.jpg', 'localhost', '7945','/upload.aspx');" id="btnsaomiao0" style="width:40px; display:none;" type="button" 
                        value="上传" /></td>
                <td class="t3" style="height: 28px; text-align: center; background-color:#f8f8f8; width:60px">
                    &nbsp;</td>
                <td class="t b" style="height: 28px; text-align: center; background-color:#f8f8f8; width:60px">
                    &nbsp;</td>
                <td class="t b r" style="height: 28px; text-align: center; background-color:#f8f8f8; width:60px">
                    &nbsp;</td>
                <td class="t b r" style="height: 28px; text-align: center; background-color:#f8f8f8; width:60px">
                    &nbsp;</td>
            </tr>
            </table>
        </div>
    </form>
</body>
</html>
