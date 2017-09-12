<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountQuery_DefineReportList" Codebehind="DefineReportList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<style type="text/css">
.Lightbox{BORDER:#fff 1px solid;DISPLAY:block;Z-INDEX:9999;TEXT-ALIGN:center;POSITION:absolute;BACKGROUND-COLOR:#f6f6f6;COLOR:Blue; FONT-SIZE:18px;height:60px;width:300px}
.Overlay{DISPLAY:block;Z-INDEX:9998;FILTER:alpha(opacity=3);POSITION:absolute;BACKGROUND-COLOR:#000;moz-opacity:0.8;opacity:.80;}
</style>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if($("ReportName").value=="")
    {
      $("ReportName").focus();
      alert("报表名称不能为空！");
      return false;
    }
    return true;
}
</script>
</head>
<body id="HBody">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">自定义报表管理</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 10%; height: 33px; text-align: right; font-size:10pt">
                    报表名称：</td>
                <td class="t1" colspan="3" style="height: 33px; text-align: center">
                    <asp:TextBox ID="ReportName" runat="server" Width="417px"></asp:TextBox></td>
                <td class="t4" colspan="2" rowspan="2" style="width: 30%; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="创建报表" Width="150px" Height="25px" />
                    <hr style="width: 150px; color:gray; height:1px;" />
                    <input id="Button4" onclick="location.href='DefineReport.aspx';" type="button" value="查询报表" style="width:150px; height:25px" />
                </td>
            </tr>
            <tr>
                <td class="t3" style="font-size: 10pt; width: 10%; height: 33px; text-align: right">
                    报表描述：</td>
                <td class="t3" colspan="3" style="height: 33px; text-align: center">
                    <asp:TextBox ID="ReportNote" runat="server" TextMode="MultiLine" Width="417px"></asp:TextBox></td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowDataBound="GridView1_RowDataBound" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
            Style="color: navy" Width="750px" PageSize="15">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="id" HeaderText="编号" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <ControlStyle Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="reportname" HeaderText="报表名称">
                    <ItemStyle Width="150px" />
                    <ControlStyle Width="145px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="报表描述">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtReportNote" runat="server" Text='<%# Bind("reportnote") %>' TextMode="MultiLine"></asp:TextBox>
                    </EditItemTemplate>
                    <ControlStyle Width="360px" />
                    <ItemStyle Width="368px" />
                    <ItemTemplate>
                        <asp:Label ID="lblReportNote" runat="server" Text='<%# Bind("reportnote") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField HeaderText="编辑" ShowEditButton="True">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="删除">
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Bind("id") %>'
                            CommandName="btnDelete" OnClick="btnDelete_Click">删除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle Font-Size="10pt" />
            <SelectedRowStyle BackColor="#FFC0C0" BorderColor="Yellow" BorderStyle="Dotted" />
            <PagerStyle BackColor="White" ForeColor="Olive" />
            <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" ForeColor="Navy" />
            <AlternatingRowStyle BackColor="#EBF0F6" />
            <PagerTemplate>
                &nbsp;<asp:LinkButton ID="FirstPage" runat="server" Font-Size="10pt" OnClick="FirstPage_Click">首页</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="PreviousPage" runat="server" Font-Size="10pt" OnClick="PreviousPage_Click">上一页</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="NextPage" runat="server" Font-Size="10pt" OnClick="NextPage_Click">下一页</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="LastPage" runat="server" Font-Size="10pt" OnClick="LastPage_Click">尾页</asp:LinkButton>
                &nbsp;
                <asp:Label ID="ShowPageInfo" runat="server" Font-Size="10pt" Text="总页数："></asp:Label>
                &nbsp;&nbsp;
                <asp:Label ID="Label1" runat="server" Font-Size="10pt" ForeColor="Navy" Text="跳转到："></asp:Label>
                <asp:DropDownList ID="JumpPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="JumpPage_SelectedIndexChanged">
                </asp:DropDownList>
            </PagerTemplate>
        </asp:GridView>
        <asp:Label ID="ExeScript" runat="server"></asp:Label></div>
    </form>
</body>
</html>
