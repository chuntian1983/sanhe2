<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SanZi.Web.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>三资监管平台开发修改</title>
    <link href="Style.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
         <table style="width:700px;border:1px solid #000800;margin-top:5px;background:#000800;" cellspacing="1" cellpadding="3" >
            <tr style="background:#ffffff;">
                <td colspan="2" class="tableTitle">三资监管平台</td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">批办卡认证：</td>
                <td class="tableContent">
                    <a href="PiBanKa.aspx" target="_blank">批办卡认证</a>、
                    <a href="PiBanKa/index.aspx" target="_blank">批办卡管理</a>、
                    <a href="templates/pibanka.html" target="_blank">批办卡打印</a>
                    √
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">权利人管理：</td>
                <td class="tableContent">
                    <a href="users/index.aspx" target="_blank">添加、查询、导出权利人</a>  
                    √
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">支出金额设置：</td>
                <td class="tableContent">
                    <a href="PiBanKa/ConditionSet.aspx?deptid=1" target="_blank">村财务支出金额设置</a>
                    √
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">票据识别：</td>
                <td class="tableContent">
                    <a href="CheckPhoto.aspx" target="_blank">票据识别</a>
                    √
                </td>
            </tr>
         </table>
         
         <table style="width:700px;border:1px solid #000800;margin-top:5px;background:#000800;" cellspacing="1" cellpadding="3" >
            <tr style="background:#ffffff;">
                <td colspan="2" class="tableTitle">招投标管理平台</td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle2">代理申请：</td>
                <td class="tableContent">
                    <a href="daili/AddDaiLi.aspx" target="_blank">代理申请录入</a>、
                    <a href="daili/index.aspx" target="_blank">代理申请管理</a>、
                    <a href="templates/daili.html" target="_blank">代理申请打印</a>&nbsp;
                    √
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">村预算书：</td>
                <td class="tableContent">
                    <a href="zhaobiao/cyss.aspx" target="_blank">村预算书录入</a>、
                    <a href="zhaobiao/cyssgl.aspx" target="_blank">村预算书管理</a>、
                    <a href="templates/yusuanshu.html" target="_blank">村预算书打印</a>
                    √
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">招标公告：</td>
                <td class="tableContent">
                    <a href="zhaobiao/zbgg.aspx" target="_blank">招标公告录入</a>、
                    <a href="zhaobiao/zbgggl.aspx" target="_blank">招标公告管理</a>、
                    <a href="templates/zhaobiao.html" target="_blank">招标公告打印</a>
                    √
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">参投登记：</td>
                <td class="tableContent">
                    <a href="zhaobiao/AddProject.aspx" target="_blank">添加项目</a>、
                    <a href="zhaobiao/ProjectManage.aspx" target="_blank">项目管理</a>、
                    <a href="zhaobiao/ctdjb.aspx" target="_blank">参投登记表录入</a>、
                    <a href="zhaobiao/ctdjbgl.aspx" target="_blank">参投登记表管理</a>、
                    <a href="templates/cantoudengji.html" target="_blank">参投登记打印</a>
                    √
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">竞价招投标记录：</td>
                <td class="tableContent">
                   <a href="zhaobiao/jjztb.aspx" target="_blank">竞价招投标记录录入</a>、
                   <a href="zhaobiao/jjztbgl.aspx" target="_blank">竞价招投标管理</a>、
                   <a href="templates/JingJia.html" target="_blank">竞价招投标记录打印</a>
                   √
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">中标公告：</td>
                <td class="tableContent">
                    <a href="zhaobiao/zhongb.aspx" target="_blank">中标公告录入</a>、
                    <a href="zhaobiao/zbgggl.aspx" target="_blank">中标公告管理</a>、
                    <a href="templates/ZhongBiao.html" target="_blank">中标公告打印</a>
                    √
                </td>
            </tr>
         </table>
         <table style="width:700px;border:1px solid #000800;margin-top:5px;background:#000800;" cellspacing="1" cellpadding="3" >
            <tr style="background:#ffffff;">
                <td colspan="2" class="tableTitle">村务公开</td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">网上测评：</td>
                <td class="tableContent">
                    <a href="CePing/Add.aspx" target="_blank">网上测评</a>、
                    <a href="CePing/cpgl.aspx" target="_blank">网上测评管理</a>、
                    √
                    <br />
                    <a href="CePing/cpjg.aspx" target="_blank">民主测评结果录入</a>、
                    <a href="CePing/cpjggl.aspx" target="_blank">民主测评结果管理</a>
                    √
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">我要投诉：</td>
                <td class="tableContent">
                <a href="Tousu/Add.aspx" target="_blank">我要投诉</a>、<a href="Tousu/index.aspx" target="_blank">投诉管理</a>
                √
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">村务公开表：</td>
                <td class="tableContent">
                    <a href="GongKai/Add.aspx" target="_blank">村务公开表录入</a>、
                    <a href="GongKai/Manage.aspx" target="_blank">村务公开表管理</a>√
                </td>
            </tr>
         </table>
    </form>
</body>
</html>
