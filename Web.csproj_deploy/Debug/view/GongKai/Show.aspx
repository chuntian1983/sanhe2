<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="SanZi.Web.GongKai.Show" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查看村务公开信息</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="frmInput" runat="server" action="SaveFile.aspx">
    <%
        string URL = this.Session["FileUrl"].ToString();    
     %>
 <SCRIPT ID=clientEventHandlersJS LANGUAGE=javascript>
<!--

function WebOffice1_NotifyCtrlReady() {
	//LoadOriginalFile接口装载文件,
	//如果是编辑已有文件，则文件路径传给LoadOriginalFile的第一个参数

    document.all.WebOffice1.LoadOriginalFile("<%=URL%>", "<%=docType %>");
 	//document.all.WebOffice1.SetTrackRevisions(1);
    //document.all.WebOffice1.ShowRevisions(1);    

    //屏蔽标准工具栏的前几个按钮
    document.all.WebOffice1.SetToolBarButton2("Standard",1,1);
    document.all.WebOffice1.SetToolBarButton2("Standard",2,1);
    document.all.WebOffice1.SetToolBarButton2("Standard",3,1);
    document.all.WebOffice1.SetToolBarButton2("Standard",6,1);    
           
    //屏蔽文件菜单项
    document.all.WebOffice1.SetToolBarButton2("Worksheet Menu Bar",1,1);         

}

//-->

 </SCRIPT>
<!-- --------------------===  Weboffice初始化完成事件--------------------- -->

<SCRIPT LANGUAGE=javascript FOR=WebOffice1 EVENT=NotifyCtrlReady>
<!--
 WebOffice1_NotifyCtrlReady() // 在装载完Weboffice(执行<object>...</object>)控件后自动执行WebOffice1_NotifyCtrlReady方法
//-->
</SCRIPT>

<script language="javascript" type="text/javascript">
// ---------------------== 关闭页面时调用此函数，关闭文档--------------------- //
function window_onunload() {
	document.all.WebOffice1.Close();
}
 
// ---------------------------== 解除文档保护 ==---------------------------------- //
function UnProtect() {
	document.all.WebOffice1.ProtectDoc(0,1, myform.docPwd.value);
}

// ---------------------------== 设置文档保护 ==---------------------------------- //
function ProtectFull() {
	document.all.WebOffice1.ProtectDoc(1,1, myform.docPwd.value);
}
// -----------------------------== 修订文档 ==------------------------------------ //
function ProtectRevision() {
	document.all.WebOffice1.SetTrackRevisions(1) 
}

// -----------------------------== 隐藏修订 ==------------------------------------ //
function UnShowRevisions() {
	document.all.WebOffice1.ShowRevisions(0);
}

// --------------------------== 显示当前修订 ==---------------------------------- //
function ShowRevisions() {
	document.all.WebOffice1.ShowRevisions(1);

}

// -------------------------== 接受当前所有修订 ------------------------------ //
function AcceptAllRevisions() {
 	document.all.WebOffice1.SetTrackRevisions(4);
}

// ---------------------------== 设置当前操作用户 ==------------------------------- //
function SetUserName() {
	if(myform.UserName.value ==""){
		alert("用户名不能为空");
		myform.UserName.focus();
		return false;
	}
 	document.all.WebOffice1.SetCurrUserName(myform.UserName.value);
}

// -------------------------=== 设置书签套加红头 ===------------------------------ //
function addBookmark() {
	//document.all.WebOffice1.SetFieldValue("mark_1", "北京点聚信息技术有限公司::ADDMARK::");			
}

// -------------------------=== 设置书签套加红头 ===------------------------------ //
function addRedHead() {

}

// -----------------------------== 返回首页 ==------------------------------------ //
function return_onclick() {
	document.all.WebOffice1.Close();
	window.location.href  = "Default.aspx"
}
// 打开本地文件
function docOpen() {
    //alert(myform.DocFilePath.value)
	if(myform.DocFilePath.value == "") {
		alert("文件路径不可以为空");
		myform.DocFilePath.focus();
		return false;
	}
	var flag;
	//LoadOriginalFile接口装载文件

	flag = document.all.WebOffice1.LoadOriginalFile(myform.DocFilePath.value, "<%=docType %>");
	if( 0 == flag){
		alert("文件打开失败，请检查路径是否正确");
		myform.DocFilePath.focus();
		return false;
	}	
}
 

//-->
</script>    
         <table style="width:850px;border:1px solid #000800;margin-top:5px;background:#000800;" cellspacing="1" cellpadding="3" >
            <tr style="background:#ffffff;">
                <td colspan="2" class="tableTitle2">村务公开录入</td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">单位名称：</td>
                <td class="tableContent">
                    <asp:Label ID="lblDeptName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">标题：</td>
                <td class="tableContent">
                    <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableContent" colspan="2">
                    <object id="WebOffice1" height="586" width="100%" style="LEFT: 0px; TOP: 0px"  classid="clsid:E77E049B-23FC-4DB8-B756-60529A35FAD5" codebase=WebOffice.cab#V6,0,0,0>
                    <param name="_ExtentX" value="6350"><param name="_ExtentY" value="6350">
                    </OBJECT>
                </td>
            </tr>
            <tr style="background:#ffffff;height:30px;">
                <td colspan="2">
                   <script type="text/javascript">
                       function tt() {
                           var va = document.getElementById('hidlbid').value;
                           location.href="manage<%=Request.QueryString["p"] %>.aspx?lbid="+va+""
                       }
                   </script>
              
                    <input id="Submit1" type="submit" value="返回" onclick="history.go(-1)" /><input id="hidlbid"  runat=server
                        type="hidden" /></td>
            </tr>
         </table>
    </form>
</body>
</html>
