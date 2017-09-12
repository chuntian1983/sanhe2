<%@ Page Language="C#" AutoEventWireup="true" Inherits="BidManage_ProjectEdit" Codebehind="ProjectEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link rel="stylesheet" type="text/css" href="/uploadify/uploadify.css" />
<script src="/uploadify/jquery.min.js" type="text/javascript"></script>
<script src="/uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("#file_upload").uploadify({
            swf: '/uploadify/uploadify.swf',
            uploader: '/uploadify/upload.ashx?act=0&step=10&pid=' + $("#ProjectList").val(),
            width: 100,
            height: 30,
            queueID: 'uploadqueue',
            buttonText: '-&nbsp;&nbsp;&nbsp;选择文件&nbsp;&nbsp;&nbsp;-',
            progressData: 'percentage',
            fileTypeDesc: '图片文件',
            fileTypeExts: '*.jpg;*.png;*.gif;*.bmp;*.jpeg',
            fileSizeLimit: '5120KB',
            removeCompleted: false,
            removeTimeout: 0,
            onUploadStart: function (file) {
                //$("#file_upload").uploadify('settings', 'pid', $("#ProjectList").val());
            },
            onDialogClose: function (queueData) {
                if (queueData.filesQueued > 0) {
                    $("#mFrame").css("display", "none");
                    $("#uploadqueue").css("display", "");
                }
            },
            onQueueComplete: function (queueData) {
                $("#mFrame").css("display", "");
                $("#mFrame").attr("src", "FileList.aspx?step=10&pid=" + $("#ProjectList").val() + "&g=" + (new Date()).getTime());
                $("#uploadqueue").css("display", "none");
            }
        });
        initdata();
    });
</script>
<link rel="Stylesheet" type="text/css" href="../Images/css.css" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function g(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function checksubmit()
{
    return true;
}
function initdata() {
    window.frames["mFrame"].location.href = "FileList.aspx?step=" + g("StepFlag").value + "&pid=" + g("TableID").value + "&g=" + (new Date()).getDate();
}
</script>
</head>
<body style="margin:0px;text-align:center">
    <form id="form1" runat="server">
    <asp:HiddenField ID="TableID" runat="server" />
    <asp:HiddenField ID="StepFlag" runat="server" />
    <table cellpadding="0" cellspacing="0" style="width: 650px">
        <tr>
            <td style="height: 35px; text-align: center">
                <span id="PageTitle" runat="server" style="font-size: 16pt; font-family: 隶书">项目管理</span>&nbsp;</td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" style="width: 650px; text-align:left">
        <tr>
            <td class="t1" style="width:15%; height:29px; text-align: center">
                项目名称：</td>
            <td class="t2" style="width:85%">
                &nbsp;<asp:Label ID="ProjectName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="t1" style="width:15%; height:29px; text-align: center">
                资产列表：</td>
            <td class="t2" style="width:85%">
                <asp:CheckBoxList ID="ZiChan" runat="server" RepeatColumns="4" 
                    RepeatDirection="Horizontal" Style="width:98%"></asp:CheckBoxList>&nbsp;
            </td>
        </tr>
        <tr>
            <td class="t1" style="width:15%; height:29px; text-align: center">
                资源列表：</td>
            <td class="t2" style="width:85%">
                <asp:CheckBoxList ID="ZiYuan" runat="server" RepeatColumns="4" 
                    RepeatDirection="Horizontal" Style="width:98%"></asp:CheckBoxList>&nbsp;
            </td>
        </tr>
        <tr style="display:none">
            <td class="t1" style="height:29px; text-align: center">
                文件列表：</td>
            <td class="t2" style="text-align:left">
	            <div style="margin:2px">
                <input id="file_upload" name="file_upload" type="file" multiple="true"></div>
                <iframe id="mFrame" style="display:block;width:550px;height:220px;border:0px;margin-left:2px" frameborder="0" runat="server"></iframe>
                <div id="uploadqueue" style="display:none;width:550px;height:220px;overflow-x:hidden;overflow-y:scroll"></div>
            </td>
        </tr>
        <tr>
            <td class="t4" colspan="2" style="height: 40px; text-align: center">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="- 保存 -" Width="100px" Height="30px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input id="Button2" type="button" style="width:100px;height:30px" value="- 返回列表 -" onclick="location.href='ProjectList.aspx'" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
