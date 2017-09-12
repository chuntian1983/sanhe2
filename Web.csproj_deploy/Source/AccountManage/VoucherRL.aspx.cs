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

public partial class AccountManage_VoucherRL : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000005")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            AuditVoucher.Attributes.Add("onclick", "return _confirm('您确定需要使所选凭证更改为“记账”状态吗？')");
            QVDate.Attributes.Add("readonly", "readonly");
            QVDate.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].QVDate,'yyyy年mm月dd日')");
            DateTime _AccountDate = MainClass.GetAccountDate();
            aYear.Value = _AccountDate.Year.ToString();
            aMonth.Value = _AccountDate.Month.ToString("00");
            aDay.Value = _AccountDate.Day.ToString("00");
            tYear.Value = DateTime.Now.Year.ToString();
            tMonth.Value = DateTime.Now.Month.ToString("00");
            tDay.Value = DateTime.Now.Day.ToString("00");
            string[] WeekDay ={ "日", "一", "二", "三", "四", "五", "六" };
            tWeek.Value = WeekDay[Convert.ToInt32(DateTime.Now.DayOfWeek)];
            InitWebControl();
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }

    /// <summary>
    /// 数据绑定函数
    /// </summary>
    private void InitWebControl()
    {
        string QueryString = "$DelFlag='0'";
        if (aMonth.Value == "12")
        {
            string LastCarryDate = MainClass.GetFieldFromID(UserInfo.AccountID, "LastCarryDate", "cw_account");
            if (LastCarryDate.IndexOf("12月") != -1)
            {
                string YearCarryVoucher = MainClass.GetFieldFromID(UserInfo.AccountID, "YearCarryVoucher", "cw_account");
                string[] CarryVoucherNo = YearCarryVoucher.Split('-');
                QueryString += "$VoucherNo>'" + CarryVoucherNo[0] + "'";
            }
        }
        if (QVDate.Text.Length > 0)
        {
            QueryString += "$VoucherDate='" + QVDate.Text + "'";
        }
        else
        {
            QueryString += "$VoucherDate like '" + aYear.Value + "年" + aMonth.Value + "月" + "%'";
        }
        if (VoucherNo.Text != "")
        {
            if (VoucherNo2.Text.Length > 0)
            {
                QueryString += "$VoucherNo between '" + VoucherNo.Text + "' and '" + VoucherNo2.Text + "'";
            }
            else
            {
                QueryString += "$VoucherNo='" + VoucherNo.Text + "'";
            }
        }
        if (IsAuditing.SelectedValue != "000000") { QueryString += "$IsAuditing='" + IsAuditing.SelectedValue + "'"; }
        if (IsRecord.SelectedValue != "000000") { QueryString += "$IsRecord='" + IsRecord.SelectedValue + "'"; }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        DataSet ds = CommClass.GetDataSet("select * from cw_voucher " + QueryString + " order by voucherno");
        if (ds.Tables[0].Rows.Count == 0)
        {
            AuditVoucher.Enabled = false;
            PageClass.BindNoRecords(GridView1, ds);
        }
        else
        {
            AuditVoucher.Enabled = true;
            GridView1.DataSource = ds.Tables[0].DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
            Label lb = (Label)GridView1.BottomPagerRow.Cells[0].FindControl("ShowPageInfo");
            lb.Text = "记录数：" + ds.Tables[0].Rows.Count.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lb.Text += "总页数：" + (GridView1.PageIndex + 1) + "/" + GridView1.PageCount + "页";
            if (GridView1.AllowPaging)
            {
                DropDownList ddl = (DropDownList)GridView1.BottomPagerRow.Cells[0].FindControl("JumpPage");
                ddl.Items.Clear();
                for (int i = 0; i < GridView1.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem("第" + (i + 1).ToString() + "页", i.ToString()));
                }
                ddl.SelectedIndex = GridView1.PageIndex;
            }
        }
    }
    protected void JumpPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridView1.PageIndex = Convert.ToInt32(ddl.SelectedValue);
        InitWebControl();
    }

    protected void FirstPage_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        InitWebControl();
    }

    protected void PreviousPage_Click(object sender, EventArgs e)
    {
        if (GridView1.PageIndex > 0)
        {
            GridView1.PageIndex -= 1;
            InitWebControl();
        }
    }

    protected void NextPage_Click(object sender, EventArgs e)
    {
        if (GridView1.PageIndex < GridView1.PageCount)
        {
            GridView1.PageIndex += 1;
            InitWebControl();
        }
    }

    protected void LastPage_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = GridView1.PageCount;
        InitWebControl();
    }
    protected void AuditVoucher_Click(object sender, EventArgs e)
    {
        string id = "''";
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1");
            if (cb.Checked)
            {
                id += ",'" + GridView1.DataKeys[i].Value.ToString() + "'";
            }
        }
        if (id != "''")
        {
            CommClass.ExecuteSQL("update cw_voucher set IsRecord='1',Accountant='" + Session["RealName"].ToString() + "' where id in(" + id + ")");
        }
        InitWebControl();
        PageClass.ShowAlertMsg(this.Page, "凭证按列表记账成功！");
        //写入操作日志
        CommClass.WriteCTL_Log("100008", "凭证按列表记账，记账月份：" + aYear.Value + "年" + aMonth.Value + "月");
        //--
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox cb = (CheckBox)e.Row.Cells[0].FindControl("CheckBox1");
            LinkButton lb = (LinkButton)e.Row.Cells[5].FindControl("IsRecord");
            if (e.Row.Cells[3].Text == "0")
            {
                cb.Enabled = false;
                lb.Enabled = false;
                lb.Text = "未审核";
            }
            else
            {
                if (e.Row.Cells[4].Text == "0")
                {
                    cb.Enabled = true;
                    lb.Enabled = true;
                    lb.Text = "记&nbsp;&nbsp;&nbsp;&nbsp;账";
                    lb.Attributes.Add("onclick", "javascript:return confirm('您确定需要记账凭证[编号：" + e.Row.Cells[1].Text + "]吗？')");
                }
                else
                {
                    cb.Enabled = false;
                    lb.Enabled = true;
                    lb.Text = "反记账";
                    lb.Attributes.Add("onclick", "javascript:return confirm('您确定需要反记账凭证[编号：" + e.Row.Cells[1].Text + "]吗？')");
                }
            }
            if (e.Row.Cells[3].Text == "1")
            {
                e.Row.Cells[3].Text = "已审核";
            }
            else
            {
                e.Row.Cells[3].Text = "未审核";
            }
            if (e.Row.Cells[4].Text == "1")
            {
                e.Row.Cells[4].Text = "已记账";
            }
            else
            {
                e.Row.Cells[4].Text = "未记账";
            }
            //查看凭证详细
            LinkButton lb2 = (LinkButton)e.Row.Cells[6].FindControl("LookVoucher");
            lb2.Attributes.Add("href", "###");
            lb2.Attributes.Add("onclick", "ShowVoucher('" + lb2.CommandArgument + "');");
        }
    }
    protected void IsAuditing_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        if (btn.CommandArgument == "0")
        {
            CommClass.ExecuteSQL(string.Format("update cw_voucher set IsRecord='1',Accountant='{0}' where id='{1}'", Session["RealName"].ToString(), btn.CommandName));
        }
        else
        {
            CommClass.ExecuteSQL(string.Format("update cw_voucher set IsRecord='0',Accountant='' where id='{0}'", btn.CommandName));
        }
        InitWebControl();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.CommandArgument == "0")
        {
            Button1.Text = "分页显示";
            Button1.CommandArgument = "1";
            GridView1.AllowPaging = false;
        }
        else
        {
            Button1.Text = "无分页显示";
            Button1.CommandArgument = "0";
            GridView1.AllowPaging = true;
        }
        InitWebControl();
    }
}
