<%@ Page Language="C#" AutoEventWireup="true" Inherits="FinanceFlow_ProjectInfo" Codebehind="ProjectInfo.aspx.cs" %>
<%@ Register TagPrefix="radU" Namespace="Telerik.WebControls" Assembly="RadUpload.Net2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>合同卡片录入</title>
<base target="_self" />
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if($("ProjectName").value.length==0)
    {
        $("ProjectName").focus();
        alert("请输入工程名称！");
        return false;
    }
    if($("ProjectType").value.length==0)
    {
        $("ProjectType").focus();
        alert("请选择工程类型！");
        return false;
    }
    return confirm("您确定需要保存工程信息吗");
}
function TableSelect(v)
{
    for(var i=0;i<4;i++)
    {
        if(v==i)
        {
            $("Table"+i).style.color="blue";
            $("Table"+i).style.backgroundColor="";
            $("Info"+i).style.display="";
        }
        else
        {
            $("Table"+i).style.color="";
            $("Table"+i).style.backgroundColor="#f6f6f6";
            $("Info"+i).style.display="none";
        }
    }
    $("ProjectName1").value=$("ProjectName").value;
    $("ProjectName2").value=$("ProjectName").value;
    $("ShowFlag").value=v;
}
function UploadFile(abox)
{
    var returnV=window.showModalDialog("../AccountManage/Appendices.aspx?g="+(new Date()).getTime(),$("Appendices"+abox).value,"dialogWidth=720px;dialogHeight=508px;center=yes;");
    if(returnV)
    {
        $("Appendices"+abox).value=returnV;
    }
    return false;
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t2" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">工程项目录入</span>&nbsp;
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="width: 750px; text-align:center;">
            <tr>
                <td id="Table0" class="t1" style="height:22px; width:25%; cursor:hand; color:blue" onclick="TableSelect(0)">基本信息</td>
                <td id="Table1" class="t1" style="height:22px; width:25%; cursor:hand; background-color:#f6f6f6" onclick="TableSelect(1)">预算审批表</td>
                <td id="Table2" class="t1" style="height:22px; width:25%; cursor:hand; background-color:#f6f6f6" onclick="TableSelect(2)">竣工备案表</td>
                <td id="Table3" class="t2" style="height:22px; width:25%; cursor:hand; background-color:#f6f6f6" onclick="TableSelect(3)">附件</td>
            </tr>
            <tr>
                <td colspan="4" class="t2" style="height:10px; font-size:0pt">&nbsp;</td>
            </tr>
        </table>
        <table id="Info0" cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 15%; text-align: right; height: 35px;">
                    工程名称：</td>
                <td class="t1" style="width: 35%;">
                    <asp:TextBox ID="ProjectName" runat="server" Width="250px" BorderWidth="1px"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: right">
                    工程类型：</td>
                <td class="t2" style="width: 35%;">
                    <asp:DropDownList ID="ProjectType" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t1" style="height: 35px; text-align: right">
                    工程地点：</td>
                <td class="t2" colspan="3">
                    <asp:TextBox ID="ProjectLocation" runat="server" BorderWidth="1px" Width="624px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 35px; text-align: right">
                    资金来源：</td>
                <td class="t2" colspan="3">
                    <asp:TextBox ID="FundSource" runat="server" BorderWidth="1px" Width="624px"></asp:TextBox></td>
            </tr>
        </table>
        <table id="Info1" cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 15%; text-align: right; height: 35px;">
                    工程名称：</td>
                <td class="t1" style="width: 35%;">
                    <asp:TextBox ID="ProjectName1" runat="server" Width="250px" BorderWidth="1px" BackColor="#F6F6F6"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: right">
                    预算资金：</td>
                <td class="t2" style="width: 35%;" colspan="2">
                    <asp:TextBox ID="ProjectBudget" runat="server" Width="180px" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right; height: 35px;">
                    项目负责人：</td>
                <td class="t1" style="width: 35%;">
                    <asp:TextBox ID="ProjectLeader" runat="server" Width="180px" BorderWidth="1px"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: right">
                    项目监督人：</td>
                <td class="t2" style="width: 35%;" colspan="2">
                    <asp:TextBox ID="ProjectSupervisor" runat="server" Width="180px" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    工程简介：</td>
                <td class="t2" colspan="4">
                    <asp:TextBox ID="ProjectIntroduction" runat="server" BorderWidth="1px" Width="624px" Height="100px" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right; height: 35px;">
                    村“两委”意见：</td>
                <td class="t1" style="width: 60%;" colspan="3">
                    <asp:TextBox ID="AuditOpinion0" runat="server" Width="435px" BorderWidth="1px"></asp:TextBox></td>
                <td class="t2" style="width: 25%; text-align:center">
                    日期：<asp:TextBox ID="AuditDate0" runat="server" Width="70px" BorderWidth="1px"></asp:TextBox>
                    &nbsp;&nbsp;<asp:LinkButton ID="lbnUploadFile0" runat="server">上传附件</asp:LinkButton></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right; height: 35px;">
                    村民代表会意见：</td>
                <td class="t1" style="width: 60%;" colspan="3">
                    <asp:TextBox ID="AuditOpinion1" runat="server" Width="435px" BorderWidth="1px"></asp:TextBox></td>
                <td class="t2" style="width: 25%; text-align:center">
                    日期：<asp:TextBox ID="AuditDate1" runat="server" Width="70px" BorderWidth="1px"></asp:TextBox>
                    &nbsp;&nbsp;<asp:LinkButton ID="lbnUploadFile1" runat="server">上传附件</asp:LinkButton></td>
            </tr>
        </table>
        <table id="Info2" cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 15%; text-align: right; height: 35px;">
                    工程名称：</td>
                <td class="t1" style="width: 35%;">
                    <asp:TextBox ID="ProjectName2" runat="server" Width="250px" BorderWidth="1px" BackColor="#F6F6F6"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: right">
                    实用资金：</td>
                <td class="t2" style="width: 35%;" colspan="2">
                    <asp:TextBox ID="RealFund" runat="server" Width="180px" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    竣工简介：</td>
                <td class="t2" colspan="4">
                    <asp:TextBox ID="CompletionNotes" runat="server" BorderWidth="1px" Width="624px" Height="100px" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right; height: 35px;">
                    验收代表意见：</td>
                <td class="t1" style="width: 60%;" colspan="3">
                    <asp:TextBox ID="AuditOpinion2" runat="server" Width="435px" BorderWidth="1px"></asp:TextBox></td>
                <td class="t2" style="width: 25%; text-align:center">
                    日期：<asp:TextBox ID="AuditDate2" runat="server" Width="70px" BorderWidth="1px"></asp:TextBox>
                    &nbsp;&nbsp;<asp:LinkButton ID="lbnUploadFile2" runat="server">上传附件</asp:LinkButton></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right; height: 35px;">
                    村“两委”意见：</td>
                <td class="t1" style="width: 60%;" colspan="3">
                    <asp:TextBox ID="AuditOpinion3" runat="server" Width="435px" BorderWidth="1px"></asp:TextBox></td>
                <td class="t2" style="width: 25%; text-align:center">
                    日期：<asp:TextBox ID="AuditDate3" runat="server" Width="70px" BorderWidth="1px"></asp:TextBox>
                    &nbsp;&nbsp;<asp:LinkButton ID="lbnUploadFile3" runat="server">上传附件</asp:LinkButton></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right; height: 35px;">
                    村民代表会意见：</td>
                <td class="t1" style="width: 60%;" colspan="3">
                    <asp:TextBox ID="AuditOpinion4" runat="server" Width="435px" BorderWidth="1px"></asp:TextBox></td>
                <td class="t2" style="width: 25%; text-align:center">
                    日期：<asp:TextBox ID="AuditDate4" runat="server" Width="70px" BorderWidth="1px"></asp:TextBox>
                    &nbsp;&nbsp;<asp:LinkButton ID="lbnUploadFile4" runat="server">上传附件</asp:LinkButton></td>
            </tr>
        </table>
        <table id="Info3" cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="text-align: center; width:10%; height:35px">
                    附件：</td>
                <td class="t2" colspan="3" style="text-align: center; width:90%">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="582px" unselectable="on" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnUploadFile" runat="server" Text="上传" Width="60px" OnClick="btnUploadFile_Click" /></td>
            </tr>
            <tr>
                <td class="t2" colspan="4" style="text-align:center">
                    <asp:GridView ID="GridView1" runat="server" AllowSorting="True"
                        AutoGenerateColumns="False" CaptionAlign="Left" Style="color: navy; margin:3px" Width="730px">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="编号" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="附件地址">
                                <ItemTemplate>
                                    <a href='<%# Eval("url") %>' target="_blank"><%# Eval("url") %></a>
                                </ItemTemplate>
                                <ItemStyle Width="510px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除">
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Bind("delid") %>' OnClick="btnDelete_Click">删除</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle Font-Size="10pt" />
                        <SelectedRowStyle BackColor="#FFC0C0" BorderColor="Yellow" BorderStyle="Dotted" />
                        <PagerStyle BackColor="White" ForeColor="Olive" />
                        <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" ForeColor="Navy" />
                        <AlternatingRowStyle BackColor="#EBF0F6" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t2" style="height:10px; font-size:0pt">&nbsp;</td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 40px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="保存工程信息" Width="277px" Height="33px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" style="width: 130px; height: 32px" value="工程列表" onclick="location.href='ProjectList.aspx';" /></td>
            </tr>
        </table>
    </div>
    <div id="layer1" style="z-index: 1; left: 218px; width: 268px; position: absolute;
        top: 214px; height: 22px">
        <radu:radprogressmanager id="Radprogressmanager1" runat="server" width="100%"></radu:radprogressmanager>
        <radu:radprogressarea id="progressArea1" runat="server" width="100%"></radu:radprogressarea>
    </div>
    <asp:HiddenField ID="ProjectID" runat="server" Value="000000" />
    <asp:HiddenField ID="ShowFlag" runat="server" Value="0" />
    <asp:HiddenField ID="Appendices" runat="server" />
    <asp:HiddenField ID="Appendices0" runat="server" />
    <asp:HiddenField ID="Appendices1" runat="server" />
    <asp:HiddenField ID="Appendices2" runat="server" />
    <asp:HiddenField ID="Appendices3" runat="server" />
    <asp:HiddenField ID="Appendices4" runat="server" />
    <asp:HiddenField ID="Appendices5" runat="server" />
    </form>
</body>
</html>
