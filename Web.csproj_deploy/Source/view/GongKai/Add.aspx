<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="Add.aspx.cs" Inherits="SanZi.Web.GongKai.add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>村务公开录入</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <p>
        
    </p>
<script type="text/javascript" language="javascript"> 
    function check_input()  {
        if (document.frmInput.txtDeptName.value == "")  {
            alert("请选择单位名称！");
            document.frmInput.txtDeptName.focus();
            return false;
        } 
        
        if (document.frmInput.txtTitleName.value == "")  {
            alert("请输入标题！");
            document.frmInput.txtTitleName.focus();
            return false;
        }

        try {
        debugger
            var webObj=document.getElementById("WebOffice1");
		    webObj.HttpInit();			//初始化Http引擎
		    // 添加相应的Post元素 
		    webObj.HttpAddPostString("DeptID", frmInput.hidDeptID.value);//部门ID
		    webObj.HttpAddPostString("DeptName", encodeURIComponent(frmInput.txtDeptName.value));//部门名称
		    webObj.HttpAddPostString("DocTitle", encodeURIComponent(frmInput.txtTitleName.value));//标题
		    webObj.HttpAddPostString("DocID", frmInput.hidDocID.value);//ID
		    webObj.HttpAddPostString("FileName", encodeURIComponent(frmInput.hidFileName.value)); //文件名称
		    webObj.HttpAddPostString("lbid", encodeURIComponent(frmInput.hidlbid.value)); //类别id

		    webObj.HttpAddPostCurrFile("DocContent","");		// 上传文件
		    returnValue = webObj.HttpPost("<%=strurl%>"); // 判断上传是否成功

            if(returnValue=="succeed")
            {
                alert("保存成功！");
                location.href = "Manage1.aspx";
            }
            else if(returnValue=="failed")
            {
                alert("文件上传失败");
            }
        }
        catch(e)
        {
		    alert("异常\r\nError:"+e+"\r\nError Code:"+e.number+"\r\nError Des:"+e.description);
        }
        return true;
    }               
    //-->             
</script>

    <form id="frmInput" runat="server" action="SaveFile.aspx">
    <%
        string URL = this.Session["URL"].ToString();    
     %>
 <SCRIPT ID=clientEventHandlersJS LANGUAGE=javascript>
<!--

function WebOffice1_NotifyCtrlReady() {
	//LoadOriginalFile接口装载文件,
	//如果是编辑已有文件，则文件路径传给LoadOriginalFile的第一个参数

    document.all.WebOffice1.LoadOriginalFile("<%=URL%>", "xls");
    
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

	flag = document.all.WebOffice1.LoadOriginalFile(myform.DocFilePath.value, "xls");
	if( 0 == flag){
		alert("文件打开失败，请检查路径是否正确");
		myform.DocFilePath.focus();
		return false;
	}	
}
 
// -----------------------------== 保存文档 ==------------------------------------ //
function SaveDoc() {
	 if(myform.DocTitle.value ==""){
		alert("标题不可为空")
		myform.DocTitle.focus();
		return false;
	}
	//恢复被屏蔽的菜单项和快捷键
    document.all.WebOffice1.SetToolBarButton2("Standard",1,3);
    document.all.WebOffice1.SetToolBarButton2("Standard",2,3);
    document.all.WebOffice1.SetToolBarButton2("Standard",3,3);
    document.all.WebOffice1.SetToolBarButton2("Standard",6,3);           

    //恢复文件菜单项
    document.all.WebOffice1.SetToolBarButton2("Worksheet Menu Bar",1,4);         

//初始化Http引擎
	document.all.WebOffice1.HttpInit();			

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
                    <asp:TextBox id="txtDeptName" runat="server" width="150px" MaxLength="50" value="点击选择所属单位" onfocus="if(this.value=='点击选择所属单位'){this.value='';}"  onblur="if(this.value==''){this.value='点击选择所属单位';}" ></asp:TextBox>
                    <input type="hidden" name="hidDeptID" runat=server id="hidDeptID" value="1"/>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">标题：</td>
                <td class="tableContent">
                <asp:TextBox id="txtTitleName" runat="server" width="500px" MaxLength="100"></asp:TextBox>
                <font color="red"><strong>*</strong></font>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableContent" colspan="2">
                    <input type="hidden" name="hidDocID" id="hidDocID" value="0"/>
                    <input type="hidden" name="hidFileName" id="hidFileName" value=""  runat="server"/>
                    <object id="WebOffice1" height="586" width="100%" style="LEFT: 0px; TOP: 0px"  classid="clsid:E77E049B-23FC-4DB8-B756-60529A35FAD5" codebase=WebOffice.cab#V6,0,0,0>
                    <param name="_ExtentX" value="6350"><param name="_ExtentY" value="6350">
                    </OBJECT>
                </td>
            </tr>
            <tr style="background:#ffffff;height:30px;">
                <td colspan="2" align="center">
                    <input id="btnSave" type="button" value="  提交  " onclick="return check_input();"/>
                </td>
            </tr>
         </table><input id="hidlbid" runat=server type="hidden" />
    </form>
</body>
</html>
