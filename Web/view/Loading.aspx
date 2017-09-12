<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Loading.aspx.cs" Inherits="SanZi.Web.Loading" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AjaxPro onLoading</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="loadinfo" style="visibility:hidden;position:absolute;left:0px;top:0px;background-color:Red;color:White;">Loading...</div>
   
        <input id="Button1" type="button" value="Get ServerTime" onclick ="javascript:GetTime();void(0)" />
        <script type="text/javascript" defer="defer">
        
        // loading效果
        AjaxPro.onLoading = function(b) 
        {
            var a = document.getElementById("loadinfo");
            a.style.visibility = b ? "visible" : "hidden";
        }

        function GetTime() 
        {
            // 调用服务端方法
            //调用方法:类名.方法名 (参数为指定一个回调函数)
            SanZi.Web.Loading.GetServerTime(callback);
        }

        function callback(res)  //回调函数,显示结果
        {
            alert(res.value);
        }
        </script>
    </div>
    </form>
</body>
</html>
