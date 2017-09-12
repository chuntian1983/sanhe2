using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class _LeftFrame : System.Web.UI.UserControl
{
    public StringBuilder DueContracts = new StringBuilder();

    private string m_CardType;
    public string CardType
    {
        set { m_CardType = value; }
        get { return m_CardType; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            ChangeUser.Attributes.Add("onclick", "return ChangeUser();");
            UserList.Attributes.Add("onchange", "$('ctl00_LeftFrame1_LPassword').focus();");
            InitAccountList();
            TextBox1.Text = Session["UserName"].ToString();
            TextBox2.Text = Session["RealName"].ToString();
            TextBox3.Text = AccountList.SelectedItem.Text;
            TextBox4.Text = ValidateClass.ReadXMLNodeText(string.Format("FinancialDB/CUnits[ID='{0}']/UnitName", UserInfo.UnitID));
            AccountList_SelectedIndexChanged(AccountList, new EventArgs());
            CheckAccount();
        }
    }
    private void InitAccountList()
    {
        AccountList.Items.Clear();
        DataTable dt = MainClass.GetDataTable(string.Format("select id,accountname from cw_account where unitid='{0}' and id in ('{1}') order by levelid,id", UserInfo.UnitID, UserInfo.MyAccount.Replace("$", "','")));
        foreach (DataRow row in dt.Rows)
        {
            AccountList.Items.Add(new ListItem(row["accountname"].ToString(), row["id"].ToString()));
        }
        AccountList.Text = UserInfo.AccountID;
    }
    protected void ChangeUser_Click(object sender, EventArgs e)
    {
        if (LPassword.Text.Length > 0)
        {
            DataRow row = MainClass.GetDataRow("select * from cw_users where username='" + UserList.SelectedValue + "'");
            if (LPassword.Text == row["password"].ToString())
            {
                Session["UserType"] = "0";
                Session["UserID"] = row["id"].ToString();
                Session["RealName"] = row["realname"].ToString();
                Session["UserName"] = row["username"].ToString();
                Session["Powers"] = row["powers"].ToString();
                Session["MyAccount"] = row["accountid"].ToString();
                TextBox1.Text = row["username"].ToString();
                TextBox2.Text = row["realname"].ToString();
                TextBox3.Text = AccountList.SelectedItem.Text;
                UserInfo.AccountID = AccountList.SelectedValue;
                CheckAccount();
                InitAccountList();
            }
            else
            {
                PageClass.ShowAlertMsg(this.Page, "密码错误，请核实！");
            }
            LPassword.Text = "";
        }
    }
    protected void AccountList_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool CFlag = false;
        UserList.Items.Clear();
        string userName = Session["UserName"].ToString();
        DataTable dt = MainClass.GetDataTable(string.Format("select username,realname from cw_users where username<>'FinancailDefaultAdmin' and unitid='{0}' and accountid like '%{1}%' order by id", UserInfo.UnitID, AccountList.SelectedValue));
        foreach (DataRow row in dt.Rows)
        {
            string username = row["username"].ToString();
            if (username == userName)
            {
                CFlag = true;
                TextBox1.Text = username;
                TextBox2.Text = row["realname"].ToString();
            }
            UserList.Items.Add(new ListItem(string.Format("{0}（{1}）", row["realname"].ToString(), username), username));
        }
        if (CFlag)
        {
            TextBox3.Text = AccountList.SelectedItem.Text;
            UserInfo.AccountID = AccountList.SelectedValue;
            UserList.Text = TextBox1.Text;
            CheckAccount();
        }
        else
        {
            PageClass.ExcuteScript(this.Page, "SetFrameUrl($('ctl00_mFrameSrc').value);", "SetFrameUrl");
        }
        if (m_CardType != null)
        {
            GetDueContracts();
        }
    }
    private void CheckAccount()
    {
        string AccountDate = MainClass.GetTableValue("cw_account", "accountdate", string.Format("id='{0}'", UserInfo.AccountID));
        if (AccountDate.Length > 0)
        {
            Session["isStartAccount"] = "Yes";
            CurAccountDate.Text = "财务日期：" + AccountDate;
            PageClass.ExcuteScript(this.Page, "SetFrameUrl($('ctl00_mFrameSrc').value);", "SetFrameUrl");
        }
        else
        {
            Session["isStartAccount"] = null;
            CurAccountDate.Text = "财务日期：" + DateTime.Now.ToString("yyyy年MM月dd日");
            if (Session["Powers"].ToString().IndexOf("000001") == -1)
            {
                PageClass.ExcuteScript(this.Page, "SetFrameUrl('ErrorTip.aspx?errTip=当前账套尚未启用，请联系相应操作员启用账套！');", "SetFrameUrl");
            }
            else
            {
                PageClass.ExcuteScript(this.Page, "SetFrameUrl('AccountInit/AccountSubject.aspx');", "SetFrameUrl");
            }
        }
    }
    private void GetDueContracts()
    {
        string tab = string.Empty;
        DataTable dt = new DataTable();
        string QueryString = string.Empty;
        string YearMonth = DateTime.Now.ToString("yyyy-MM");
        string LastMonthDay = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString();
        string rowStyle = "style='height:25px;border-bottom: 1px dotted #CCCCCC;cursor:hand'";
        string ContractType = (m_CardType == "0" ? "资产" : "资源");
        //收款到期
        DueContracts.Append("<table id='dueTable2' cellpadding=0 cellspacing=0 style='width:284px;'>");
        QueryString = "select a.id lid,a.resourcename,b.id,b.PeriodName from cw_resleasecard a,cw_respayperiod b where b.CardID=a.id and a.CardType='{2}' and a.LeaseState='0' and b.PayState='0' and b.DoState='0' and b.EndPay>='{0}-01' and b.EndPay<='{0}-{1}' order by b.EndPay asc";
        dt = CommClass.GetDataTable(string.Format(QueryString, YearMonth, LastMonthDay, CardType));
        if (dt.Rows.Count == 0)
        {
            DueContracts.Append("<tr><td>无</td></tr>");
        }
        else
        {
            foreach (DataRow row in dt.Rows)
            {
                DueContracts.Append("<tr onmouseover=\"this.style.background='#E0EFF6\';\" onmouseout=\"this.style.background='';\">");
                DueContracts.AppendFormat("<td {0} onclick=PrintDueCard(1,'{1}','{2}')><img src=Images/dot2.jpg>&nbsp;{5}：{3}，支付批次：{4}</td>",
                    new string[] { rowStyle, row["lid"].ToString(), row["id"].ToString(), row["ResourceName"].ToString(), row["PeriodName"].ToString(), ContractType });
                DueContracts.Append("</tr>");
            }
            tab = "2";
        }
        DueContracts.Append("</table>\n");
        //合同到期
        DueContracts.Append("<table id='dueTable1' cellpadding=0 cellspacing=0 style='width:284px;'>");
        QueryString = "select id,ResourceName,EndLease from cw_resleasecard where CardType='{2}' and DoState='0' and LeaseState='0' and EndLease>='{0}-01' and EndLease<='{0}-{1}' order by EndLease asc";
        dt = CommClass.GetDataTable(string.Format(QueryString, YearMonth, LastMonthDay, CardType));
        if (dt.Rows.Count == 0)
        {
            DueContracts.Append("<tr><td>无</td></tr>");
        }
        else
        {
            foreach (DataRow row in dt.Rows)
            {
                DueContracts.Append("<tr onmouseover=\"this.style.background='#E0EFF6\';\" onmouseout=\"this.style.background='';\">");
                DueContracts.AppendFormat("<td {0} onclick=PrintDueCard(0,'{1}')><img src=Images/dot2.jpg>&nbsp;{4}：{2}，到期日期：{3}</td>",
                    new string[] { rowStyle, row["id"].ToString(), row["ResourceName"].ToString(), row["EndLease"].ToString(), ContractType });
                DueContracts.Append("</tr>");
            }
            tab = "1";
        }
        DueContracts.Append("</table>\n");
        //收款提醒
        DueContracts.Append("<table id='dueTable0' cellpadding=0 cellspacing=0 style='width:284px;'>");
        QueryString = "select id,ResourceName,NextPayDate from cw_resleasecard where CardType='{2}' and DoState='0' and LeaseState='0' and NextPayDate>='{0}-01' and NextPayDate<='{0}-{1}' order by NextPayDate asc";
        dt = CommClass.GetDataTable(string.Format(QueryString, YearMonth, LastMonthDay, CardType));
        if (dt.Rows.Count == 0)
        {
            DueContracts.Append("<tr><td>无</td></tr>");
        }
        else
        {
            foreach (DataRow row in dt.Rows)
            {
                DueContracts.Append("<tr onmouseover=\"this.style.background='#E0EFF6\';\" onmouseout=\"this.style.background='';\">");
                DueContracts.AppendFormat("<td {0} onclick=ShowVoucher('{1}')><img src=Images/dot2.jpg>&nbsp;{4}：{2}，收款日期：{3}</td>",
                    new string[] { rowStyle, row["id"].ToString(), row["ResourceName"].ToString(), row["NextPayDate"].ToString(), ContractType });
                DueContracts.Append("</tr>");
            }
            tab = "0";
        }
        DueContracts.Append("</table>");
        if (tab.Length == 0)
        {
            showmsg.Visible = false;
        }
        else
        {
            showmsg.Visible = true;
            ExeScript.Text = string.Format("<script>TableSlect({0});</script>", tab);
        }
    }
}
