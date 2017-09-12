<%@ Control Language="C#" AutoEventWireup="true" Inherits="AccountQuery_ShowVoucher" Codebehind="ShowVoucher.ascx.cs" %>

<asp:Image ID="pzheadbg" runat="server" style="z-index: -1; position: absolute; left:100px; top:200px; border-width:0px" />
<table cellpadding="0" cellspacing="0" style="width: 748px">
    <tr>
        <td style="vertical-align: top; text-align: center">
            <table cellpadding="0" cellspacing="0" style="width: 690px">
                <tr>
                    <td id="VoucherHead" colspan="4" runat="server">
                        <table cellpadding="0" cellspacing="0" style="width: 657px">
                            <tr>
                                <td class="pzhead" colspan="2" style="height: 42px; text-align: left; vertical-align:bottom">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="AccountName" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="true"></asp:Label>
                                    <!--NoPrintStart--><img id="AlarmTip" alt="" src="../Images/light.gif" runat="server" /><!--NoPrintEnd--></td>
                                <td class="pzhead" colspan="1" style="height: 42px; text-align: right; vertical-align:top">
                                    <asp:Label ID="ShowPage" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 46%; height: 20px;">&nbsp;</td>
                                <td class="pzhead" style="width: 40%; text-align: left;">
                                    <asp:Label ID="VoucherDate" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="true"></asp:Label></td>
                                <td class="pzhead" style="text-align: left;">
                                    <asp:Label ID="VoucherNo" runat="server" ForeColor="Blue" Font-Names="宋体" Font-Bold="true"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="3" style="height: 17px;">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="vertical-align: top; text-align: center">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="gridview" ShowHeader="False" Width="671px">
                            <RowStyle Font-Size="10pt" Height="21px" />
                            <Columns>
                                <asp:BoundField HeaderText="摘要">
                                    <ItemStyle CssClass="dd" HorizontalAlign="Left" Width="160px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="总账科目">
                                    <ItemStyle CssClass="dd" HorizontalAlign="Left" Width="104px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="明细科目">
                                    <ItemStyle CssClass="dd" HorizontalAlign="Left" Width="140px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="l0">
                                    <ItemStyle CssClass="dd" HorizontalAlign="Right" Width="122px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="l1">
                                    <ItemStyle CssClass="bb" HorizontalAlign="Right" Width="122px" />
                                </asp:BoundField>
                            </Columns>
                            <PagerSettings Visible="False" />
                            <RowStyle Height="22px" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="width: 35%; height: 24px; text-align: left">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <span style="color: red"><strong>主管会计：</strong></span>
                        <asp:Label ID="Director" runat="server" ForeColor="Red"></asp:Label></td>
                    <td style="width: 23%; height: 24px; text-align: left">
                        <span style="color: red"><strong>记账：</strong></span>
                        <asp:Label ID="Accountant" runat="server" ForeColor="Red"></asp:Label></td>
                    <td style="width: 22%; height: 24px; text-align: left">
                        <span style="color: red"><strong>审核：</strong></span>
                        <asp:Label ID="Assessor" runat="server" ForeColor="Red"></asp:Label></td>
                    <td style="width: 20%; height: 24px; text-align: left">
                        <span style="color: red"><strong>制证：</strong></span>
                        <asp:Label ID="DoBill" runat="server" ForeColor="Red"></asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
