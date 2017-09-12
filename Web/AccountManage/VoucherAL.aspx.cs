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

public partial class AccountManage_VoucherAL : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000004")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            ////////////////////////////////////////////////////////////////
            //获取报警参数
            DataTable alarm = MainClass.GetDataTable("select IndexSubject,IndexValue from cw_indexmonitor where UnitID='999999' order by IndexSubject asc");
            StringBuilder alarms = new StringBuilder();
            foreach (DataRow row in alarm.Rows)
            {
                string subjectNo = row["IndexSubject"].ToString();
                subjectNo = subjectNo.Substring(0, subjectNo.IndexOf("."));
                if (subjectNo.StartsWith("101"))
                {
                    alarms.AppendFormat(" or (subjectno like '{0}%' and SumMoney{2}{1})", subjectNo, row["IndexValue"].ToString(), "{0}");
                }
                else
                {
                    alarms.AppendFormat(" or (subjectno like '{0}%' and SumMoney{2}{1})", subjectNo, row["IndexValue"].ToString(), "{1}");
                }
            }
            Alarms.Value += alarms.ToString();
            ////////////////////////////////////////////////////////////////
            AuditVoucher.Attributes.Add("onclick", "return _confirm('您确定需要使所选凭证更改为“审核”状态吗？')");
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
            DataTable vouchers = CommClass.GetDataTable(string.Format("select * from cw_voucher where id in({0})", id));
            StringBuilder tips = new StringBuilder();
            for (int i = 0; i < vouchers.Rows.Count; i++)
            {
                if (vouchers.Rows[i]["IsAuditing"].ToString() == "0")
                {
                    if (vouchers.Rows[i]["IsHasAlarm"].ToString() == "0")
                    {
                        vouchers.Rows[i]["IsAuditing"] = "1";
                        vouchers.Rows[i]["Assessor"] = Session["RealName"].ToString();
                    }
                    else
                    {
                        tips.AppendFormat(",{0}", vouchers.Rows[i]["voucherno"].ToString());
                    }
                }
            }
            CommClass.UpdateDataTable(vouchers);
            //输出超限报警信息
            if (tips.Length > 0)
            {
                tips.Remove(0, 1);
                tips.Insert(0, "以下凭证含有超限分录：\\n\\n");
                tips.Append("\\n\\n以上凭证必须由监督管理部门审核");
                PageClass.ShowAlertMsg(this.Page, tips.ToString());
            }
        }
        InitWebControl();
        //PageClass.ShowAlertMsg(this.Page, "凭证按列表审核成功！");
        //写入操作日志
        CommClass.WriteCTL_Log("100007", "凭证按列表审核，审核月份：" + aYear.Value + "年" + aMonth.Value + "月");
        //--
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox cb = (CheckBox)e.Row.Cells[0].FindControl("CheckBox1");
            LinkButton lb = (LinkButton)e.Row.Cells[5].FindControl("IsAuditing");
            if (e.Row.Cells[3].Text == "0")
            {
                cb.Enabled = true;
                lb.Enabled = true;
                lb.Text = "审&nbsp;&nbsp;&nbsp;&nbsp;核";
                lb.Attributes.Add("onclick", "javascript:return confirm('您确定需要审核凭证[编号：" + e.Row.Cells[1].Text + "]吗？')");
            }
            else
            {
                cb.Enabled = false;
                if (e.Row.Cells[4].Text == "0")
                {
                    lb.Text = "反审核";
                    HiddenField isHasAlarm = (HiddenField)e.Row.Cells[5].FindControl("IsHasAlarm");
                    if (isHasAlarm.Value == "0")
                    {
                        lb.Enabled = true;
                        lb.Attributes.Add("onclick", "javascript:return confirm('您确定需要反审核凭证[编号：" + e.Row.Cells[1].Text + "]吗？')");
                    }
                    else
                    {
                        lb.Enabled = false;
                    }
                }
                else
                {
                    lb.Enabled = false;
                    lb.Text = "已记账";
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
            if (Alarms.Value == "1=2" || CommClass.GetFieldFromID(btn.CommandName, "IsHasAlarm", "cw_voucher") == "0")
            {
                CommClass.ExecuteSQL(string.Format("update cw_voucher set IsAuditing='1',Assessor='{0}' where id='{1}'", Session["RealName"].ToString(), btn.CommandName));
                //PageClass.ShowAlertMsg(this.Page, "凭证：" + _VoucherNo + "，审核成功！");
                //写入操作日志
                CommClass.WriteCTL_Log("100007", string.Format("凭证审核：{0}，凭证日期：{1}",
                    CommClass.GetFieldFromID(btn.CommandName, "voucherno", "cw_voucher"),
                    CommClass.GetFieldFromID(btn.CommandName, "voucherdate", "cw_voucher")));
                //--
            }
            else
            {
                //输出超限报警信息
                DataTable entry = CommClass.GetDataTable(string.Format("select SubjectNo,SubjectName from cw_entry where VoucherID='{0}' and ({1})", btn.CommandName, string.Format(Alarms.Value, "<=-", ">=")));
                StringBuilder tips = new StringBuilder();
                tips.Append("以下分录科目超限：\\n\\n");
                foreach (DataRow row in entry.Rows)
                {
                    string subjectno = row["SubjectNo"].ToString();
                    tips.AppendFormat("- {0}.{1}\\n", subjectno, CommClass.GetFieldFromNo(subjectno, "SubjectName"));
                }
                tips.Append("\\n该凭证含有超限分录，必须由监督管理部门审核");
                PageClass.ShowAlertMsg(this.Page, tips.ToString());
            }
        }
        else
        {
            CommClass.ExecuteSQL(string.Format("update cw_voucher set IsAuditing='0',Assessor='' where id='{0}'", btn.CommandName));
            //PageClass.ShowAlertMsg(this.Page, "凭证：" + _VoucherNo + "，审核成功！");
            //写入操作日志
            CommClass.WriteCTL_Log("100007", string.Format("凭证审核：{0}，凭证日期：{1}",
                CommClass.GetFieldFromID(btn.CommandName, "voucherno", "cw_voucher"),
                CommClass.GetFieldFromID(btn.CommandName, "voucherdate", "cw_voucher")));
            //--
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
