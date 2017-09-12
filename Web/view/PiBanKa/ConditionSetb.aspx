<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConditionSetb.aspx.cs" Inherits="SanZi.Web.pibanka.ConditionSetb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>村财务支出金额设置</title>
    <link href="../../Style.css" type="text/css" rel="stylesheet"/>
    <script type="text/javascript" language="javascript" src="../js/functionforjs.js"></script>
    <script type="text/javascript">
        function sel() {
            var v = window.showModalDialog("../../ChooseAccount.aspx?t=1&g=" + (new Date()).getTime(), "", "dialogWidth=770px;dialogHeight=480px;center=yes;");
            if (v && v.length > 0) {
                document.getElementById("AccountID").value = v;
                __doPostBack('LinkButton1', '');
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit='return check_input();'>
    <asp:HiddenField ID="AccountID" runat="server" />
    <div align="center">
         <div style="height:35px"><input id="Button1" type="button" value="选择设置账套" onclick="sel()" /><asp:LinkButton 
                 ID="LinkButton1" runat="server" onclick="LinkButton1_Click"></asp:LinkButton>
         </div>
         <table id="setm" runat="server" style="width:600px;border:1px solid #000800;margin-top:5px;background:#000800;" cellspacing="1" cellpadding="3" >
            <tr style="background:#ffffff;">
                <td colspan="2" class="tableTitle2">村财务支出金额设置（账套：<%=aname%>）
                <input type="hidden" name="hidDeptID" id="hidDeptID" value="1"/></td>
            </tr>
            
            <tr style="background:#ffffff;">
                <td class="tableTitle">
                    <span id="stepANum"><asp:Label ID="lblStepA" runat="server" Text="500"></asp:Label></span>元以下：
                 </td>
                <td class="tableContent">
                    <asp:DropDownList ID="txtStepA" runat="server">
                    </asp:DropDownList>
                    <div id="tipReason" class="tipMsg" style="text-align:left;">需要村支部书记、村委会主任审核通过。</div>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle"><span id="stepANum2"><asp:Label ID="lblStepA2" runat="server" Text="500"></asp:Label></span>元以上<br />
                <span id="stepBNum"><asp:Label ID="lblStepBNum" runat="server" Text="5000"></asp:Label></span>元以下：</td>
                <td class="tableContent">
                    <asp:TextBox ID="txtStepA2" runat="server" width="150px"></asp:TextBox>~
                    <asp:TextBox ID="txtStepB" runat="server" width="150px"></asp:TextBox>
                    <div id="Div1" class="tipMsg" style="text-align:left;">需要村两委全体成员超过2/3、村务监督委员会成员超过2/3审核通过。</div>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle"><span id="stepBNum2"><asp:Label ID="lblStepBNum2" runat="server" Text="5000"></asp:Label></span>元以上：</td>
                <td class="tableContent">
                    <asp:TextBox ID="txtStepB2" runat="server" width="150px"></asp:TextBox>
                    <div id="Div2" class="tipMsg" style="text-align:left;">需要村委会成员和村民代表成员总人数超过1/2、村务监督委员会成员超过2/3审核通过。</div>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td colspan="2">
                    <asp:Button ID="btnSave" runat="server" Text=" 保存 " tabindex="4" 
                        onclick="btnSave_Click" />
                </td>
            </tr>
         </table>
    </div>
<script type="text/javascript" language="javascript" defer="defer"> 
<!--
    function changeStepA(objID)
    {
        var stepA=document.getElementById(objID).value;
        if(IsFloat(stepA) && stepA!="")
        {
            document.getElementById("txtStepA2").value=stepA;
            document.getElementById("stepANum").innerHTML=stepA;
            document.getElementById("stepANum2").innerHTML=stepA;
        }
        else
        {
            alert("支出金额只能是数字！");
            document.getElementById(objID).focus();
        }
    }
    
    function changeStepA2(objID)
    {
        var stepA2=document.getElementById(objID).value;
        if(IsFloat(stepA2) && stepA2!="")
        {
            document.getElementById("txtStepA").value=stepA2;
            document.getElementById("stepANum").innerHTML=stepA2;
            document.getElementById("stepANum2").innerHTML=stepA2;
        }
        else
        {
            alert("支出金额只能是数字！");
            document.getElementById(objID).focus();
        }
    }
    
    function changeStepB(objID)
    {
        var stepB=document.getElementById(objID).value;
        if(IsFloat(stepB) && stepB!="")
        {
            document.getElementById("txtStepB2").value=stepB;
            document.getElementById("stepBNum").innerHTML=stepB;
            document.getElementById("stepBNum2").innerHTML=stepB;
        }
        else
        {
            alert("支出金额只能是数字！");
            document.getElementById(objID).focus();
        }
    }
    
    function changeStepB2(objID)
    {
        var stepB2=document.getElementById(objID).value;
        if(IsFloat(stepB2) && stepB2!="")
        {
            document.getElementById("txtStepB").value=stepB2;
            document.getElementById("stepBNum").innerHTML=stepB2;
            document.getElementById("stepBNum2").innerHTML=stepB2;
        }
        else
        {
            alert("支出金额只能是数字！");
            document.getElementById(objID).focus();
        }
    }
    
    function check_input()  {
        if (!document.all("txtStepA")) {
            return true;
        }
        var stepA=Trim(document.getElementById("txtStepA").value);
        var stepA2=Trim(document.getElementById("txtStepA2").value);
        var stepB=Trim(document.getElementById("txtStepB").value);
        var stepB2 = Trim(document.getElementById("txtStepB2").value);
        if (eval(stepA2) > eval(stepB)) {
            alert("支出金额下限不能大于上限！");
            return false;
        }
        if(stepA=="" || stepA2=="" || stepB=="" || stepB2=="")
        {
            alert("支出金额！");
            return false;
        }
        if(!IsFloat(stepA) || !IsFloat(stepA2) || !IsFloat(stepB) || !IsFloat(stepB2))
        {
            alert("支出金额只能是数字！");
            return false;
        }
        
        return true;
    }  
    //-->             
</script>
    </form>
</body>
</html>
