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

public partial class AccountManage_VoucherList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000014$100004")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            AName.Text = UserInfo.AccountName;
            InitSubject(TreeView1.Nodes[0], "000");
            QSubjectNo.Attributes["onclick"] = "SetObjectPos('QSubjectNo')";
            CheckBox1.Attributes["onclick"] = "if(this.checked)SetSuperQuery()";
            MainClass.InitAccountYear(QYear);
            DateTime AccountDate = MainClass.GetAccountDate();
            ReportDate.Text = AccountDate.ToString("yyyy年MM月");
            QYear.Attributes["onchange"] = "OnSelChange(this.value,0);";
            QSMonth.Attributes["onchange"] = "SelAMonth(this.value);";
            QEMonth.Attributes["onchange"] = "OnSelChange(this.value,1);";
            QSMonth.Text = AccountDate.Month.ToString("00");
            QEMonth.Text = AccountDate.Month.ToString("00");
            InitWebControl();
            //写入操作日志
            CommClass.WriteCTL_Log("100015", "凭证查询：凭证列表查询");
            //--
        }
    }
    protected void InitSubject(TreeNode _TreeNode, string ParentNo)
    {
        DataSet ds = CommClass.GetDataSet("select subjectno,subjectname from cw_subject where parentno='" + ParentNo + "' order by subjectno");
        if (ds.Tables[0].Rows.Count == 0)
        {
            if (ParentNo == "000") { return; }
            _TreeNode.SelectAction = TreeNodeSelectAction.None;
            string ClickStr = "OnTreeClick('" + _TreeNode.Text.Replace(_TreeNode.Value + "&nbsp;.&nbsp;", "") + "[" + _TreeNode.Value + "]');";
            _TreeNode.Text = "<a href=\"###\" onclick=\"" + ClickStr + "\">" + _TreeNode.Text + "</a>";
        }
        else
        {
            _TreeNode.SelectAction = TreeNodeSelectAction.Expand;
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            _TreeNode.ChildNodes.Add(new TreeNode(ds.Tables[0].Rows[i]["subjectno"].ToString() + "&nbsp;.&nbsp;"
                + ds.Tables[0].Rows[i]["subjectname"].ToString(), ds.Tables[0].Rows[i]["subjectno"].ToString()));
            InitSubject(_TreeNode.ChildNodes[i], ds.Tables[0].Rows[i]["subjectno"].ToString());
        }
    }
    /// <summary>
    /// 数据绑定函数
    /// </summary>
    private void InitWebControl()
    {
        string sYearMonth = QYear.SelectedValue + "年" + QSMonth.SelectedValue + "月01日";
        string eYearMonth = QYear.SelectedValue + "年" + QEMonth.SelectedValue + "月31日";
        string QueryString = "$voucherdate between '" + sYearMonth + "' and '" + eYearMonth + "'";
        if (QSubjectNo.Text.Length > 0)
        {
            string[] SubjectNo = QSubjectNo.Text.Replace("]", "").Split('[');
            if (SubjectNo.Length == 2)
            {
                QueryString += "$SubjectNo like '" + SubjectNo[1].Trim() + "%'";
            }
            else
            {
                QueryString += "$SubjectNo like '" + QSubjectNo.Text + "%'";
            }
        }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        DataSet ds = new DataSet();
        string QColumns = "id,voucherid,voucherno,voucherdate,vsummary,subjectno,subjectname,summoney,AddonsCount";
        if (CheckBox1.Checked)
        {
            string QQueryString = " where " + QGroup.Value;
            ds = CommClass.GetDataSet("select " + QColumns + " from cw_voucherentry " + QQueryString + " order by id");
            if (ds == null)
            {
                ds = CommClass.GetDataSet("select " + QColumns + " from cw_voucherentry " + QueryString + " order by id");
            }
        }
        else
        {
            ds = CommClass.GetDataSet("select " + QColumns + " from cw_voucherentry " + QueryString + " order by left(voucherdate,8),voucherno");
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //统计附单张数
        decimal Addons = 0;
        string TempVNo = string.Empty;
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            if (row["AddonsCount"].ToString().Length > 0 && TempVNo != row["voucherno"].ToString())
            {
                Addons += decimal.Parse(row["AddonsCount"].ToString());
                TempVNo = row["voucherno"].ToString();
            }
        }
        TextBox1.Text = Addons.ToString();
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (ds.Tables[0].Rows.Count == 0)
        {
            OutputDataToExcel.Enabled = false;
            PageClass.BindNoRecords(GridView1, ds);
        }
        else
        {
            OutputDataToExcel.Enabled = true;
            GridView1.AllowPaging = CheckBox2.Checked;
            GridView1.DataSource = ds.Tables[0].DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.Columns[7].Visible = true;
            GridView1.DataBind();
            GridView1.Columns[7].Visible = false;
            if (GridView1.AllowPaging)
            {
                Label lb = (Label)GridView1.BottomPagerRow.Cells[0].FindControl("ShowPageInfo");
                lb.Text = "记录数：" + ds.Tables[0].Rows.Count.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                lb.Text += "总页数：" + (GridView1.PageIndex + 1) + "/" + GridView1.PageCount + "页";
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string SubjectNo = e.Row.Cells[3].Text;
            string SubjectName = e.Row.Cells[4].Text;
            if (SubjectName == "&nbsp;")
            {
                e.Row.Cells[3].Text = CommClass.GetFieldFromNo(SubjectNo.Substring(0, 3), "SubjectName");
                if (SubjectNo.Length > 3)
                {
                    e.Row.Cells[4].Text = CommClass.GetDetailSubject(SubjectNo);
                }
            }
            else
            {
                int sindex = SubjectName.IndexOf('/');
                if (sindex == -1)
                {
                    e.Row.Cells[3].Text = SubjectName;
                    e.Row.Cells[4].Text = "&nbsp;";
                }
                else
                {
                    e.Row.Cells[3].Text = SubjectName.Substring(0, sindex);
                    if (sindex < SubjectName.Length - 1)
                    {
                        e.Row.Cells[4].Text = SubjectName.Substring(sindex + 1);
                    }
                    else
                    {
                        e.Row.Cells[4].Text = "&nbsp;";
                    }
                }
            }
            if (e.Row.Cells[6].Text.IndexOf("-") == -1)
            {
                e.Row.Cells[5].Text = "借";
            }
            else
            {
                e.Row.Cells[5].Text = "贷";
                e.Row.Cells[6].Text = e.Row.Cells[6].Text.Substring(1);
            }
            e.Row.Attributes.Add("ondblclick", "ShowVoucher('" + e.Row.Cells[7].Text + "');");
            e.Row.Attributes["title"] = "提示：双击行可以查看详细凭证。";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
    protected void OutputDataToExcel_Click(object sender, EventArgs e)
    {
        InitWebControl();
        PageClass.ToExcel(GridView1);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
}
