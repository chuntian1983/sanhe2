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
using System.IO;

public partial class AccountInit_SystemSubject : System.Web.UI.Page
{
    private string[] G_SubjectType ={ "", "资产类", "负债类", "权益类", "成本类", "损益类" };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000000")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            SubjectNo.Attributes.Add("onkeyup", "SubjectNo_onkeyup();");
            SubjectNo.Attributes.Add("onkeypress", "return SubjectNo_onkeypress();");
            AddSubject.Attributes.Add("onclick", "return CheckSubmit();");
            SubjectLevel.Value = SysConfigs.SubjectLevel;
            if (SubjectLevel.Value.Length == 0)
            {
                PageClass.UrlRedirect("科目长度限制参数不能为空！", 3);
            }
            InitWebControl();
        }
    }

    /// <summary>
    /// 数据绑定函数
    /// </summary>
    private void InitWebControl()
    {
        //获取所有科目代码、科目属性、账式、账户结构、是否明细
        AllMainSubject.Value = "";
        DataSet ds = CommClass.GetDataSet("select * from cw_subject");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            AllMainSubject.Value += "[" + row["subjectno"].ToString() + "]" + row["SubjectType"].ToString() + ":"
                + row["AccountType"].ToString() + ":" + row["AccountStruct"].ToString() + ":" + row["IsDetail"].ToString() + ",";
        }
        //初始化科目表信息
        if (GoParent.CommandArgument == "000")
        {
            GoParent.Enabled = false;
            GoParent.Text = "返回上级科目";
        }
        else
        {
            GoParent.Enabled = true;
            GoParent.Text = "返回上级科目[" + GoParent.CommandArgument + "_" + CommClass.GetFieldFromNo(GoParent.CommandArgument, "subjectname") + "]";
        }
        string QueryString = "$ParentNo='" + GoParent.CommandArgument + "'";
        if (Request.QueryString["stype"] == null)
        {
            M0.BgColor = "#FFCCCC";
        }
        else
        {
            ((HtmlTableCell)this.FindControl("M" + Request.QueryString["stype"])).BgColor = "#FFCCCC";
            QueryString += "$SubjectType='" + Request.QueryString["stype"] + "'";
        }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        ds = CommClass.GetDataSet("select * from cw_subject " + QueryString + " order by subjectno");
        if (ds.Tables[0].Rows.Count == 0)
        {
            PageClass.BindNoRecords(GridView1, ds);
        }
        else
        {
            if (GoParent.CommandArgument != "000")
            {
                SubjectNo.Text = GoParent.CommandArgument + GetNextSubjectNo(GoParent.CommandArgument);
            }
            GridView1.DataSource = ds.Tables[0].DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
        }
        if (SubjectNo.Text.Length == 0)
        {
            SubjectNo.Focus();
        }
        else
        {
            SubjectName.Focus();
        }
    }

    protected string GetNextSubjectNo(string ParentNo)
    {
        //获取下级科目长度
        int sLength = ParentNo.Length + 2;
        string[] _SubjectLevel = SubjectLevel.Value.Split(',');
        for (int i = 0; i < _SubjectLevel.Length - 1; i++)
        {
            if (ParentNo.Length.ToString() == _SubjectLevel[i])
            {
                sLength = int.Parse(_SubjectLevel[i + 1]);
            }
        }
        //查找下一个未用的编号
        sLength = sLength - ParentNo.Length;
        for (int i = 1; i < 9999; i++)
        {
            string NextNo = i.ToString().PadLeft(sLength, '0');
            if (AllMainSubject.Value.IndexOf(string.Format("[{0}{1}]", ParentNo, NextNo)) == -1)
            {
                return NextNo;
            }
        }
        return "";
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //非编辑状态下单元格处理
            string ID = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
            if (e.Row.RowState.ToString().IndexOf("Edit") == -1)
            {
                Label SubjectType = (Label)e.Row.Cells[2].FindControl("SubjectType");
                SubjectType.Text = G_SubjectType[int.Parse(SubjectType.Text)];
                Label AccountType = (Label)e.Row.Cells[3].FindControl("AccountType");
                AccountType.Text = AccountType.Text == "0" ? "一般金额账" : "数量金额账";
                Label IsEntryData = (Label)e.Row.Cells[6].FindControl("IsEntryData");
                IsEntryData.Text = IsEntryData.Text == "0" ? "否" : "是";
                Label IsDetail = (Label)e.Row.Cells[7].FindControl("IsDetail");
                IsDetail.Text = IsDetail.Text == "0" ? "否" : "是";
            }
            //科目方向处理
            if (e.Row.Cells[5].Text == "0.00")
            {
                e.Row.Cells[4].Text = "平";
            }
            else
            {
                e.Row.Cells[4].Text = e.Row.Cells[5].Text.IndexOf("-") == -1 ? "借" : "贷";
                e.Row.Cells[5].Text = e.Row.Cells[5].Text.Replace("-", "");
            }
            //删除按钮处理
            LinkButton btnDelete = (LinkButton)e.Row.Cells[8].FindControl("btnDelete");
            string PName = e.Row.Cells[1].Text;
            string ParentNo = e.Row.Cells[0].Text;
            if (e.Row.RowIndex == GridView1.EditIndex)
            {
                PName = ((TextBox)e.Row.Cells[1].Controls[0]).Text;
                ParentNo = ((TextBox)e.Row.Cells[0].Controls[0]).Text;
            }
            if (CommClass.CheckExist("cw_subject", "parentno='" + ParentNo + "'"))
            {
                btnDelete.Text = "下级科目";
                //锁定明细属性
                //e.Row.Cells[7].Enabled = false;
            }
            else
            {
                btnDelete.Text = "删除科目";
                btnDelete.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除“" + PName + "”吗？')");
            }
        }
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        InitWebControl();
        TextBox SubjectNo = (TextBox)GridView1.Rows[e.NewEditIndex].Cells[0].Controls[0];
        SubjectNo.Width = 72;
        GridView1.Rows[e.NewEditIndex].Cells[0].Attributes.Add("OldSubjectNo", SubjectNo.Text);
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        InitWebControl();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
        TextBox SubjectNo = (TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[0];
        TextBox SubjectName = (TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0];
        if (CommClass.CheckExist("cw_subject", "subjectno='" + SubjectNo.Text + "' and id<>'" + ID + "'"))
        {
            InitWebControl();
            SubjectNo = (TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[0];
            SubjectNo.Width = 72;
            ExeScript.Text = "<script>alert('此科目代码【" + SubjectNo.Text + "】已存在，请更换别的。')</script>";
        }
        else if (CommClass.CheckExist("cw_subject", "subjectname='" + SubjectName.Text + "' and id<>'" + ID + "' and parentno='" + GoParent.CommandArgument + "'"))
        {
            InitWebControl();
            SubjectNo = (TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[0];
            SubjectNo.Width = 72;
            ExeScript.Text = "<script>alert('同级科目中此科目名称【" + SubjectName.Text + "】已存在，请更换别的。')</script>";
        }
        else
        {
            DropDownList SubjectType = (DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("SubjectType");
            DropDownList AccountType = (DropDownList)GridView1.Rows[e.RowIndex].Cells[3].FindControl("AccountType");
            DropDownList IsEntryData = (DropDownList)GridView1.Rows[e.RowIndex].Cells[6].FindControl("IsEntryData");
            DropDownList IsDetail = (DropDownList)GridView1.Rows[e.RowIndex].Cells[7].FindControl("IsDetail");
            string _SubjectType = GoParent.CommandArgument == "000" ? "',SubjectType='" + SubjectType.SelectedValue + "'" : "'";
            string ParentNo = string.Empty;
            if (GoParent.CommandArgument == "000")
            {
                ParentNo = "000";
            }
            else
            {
                ParentNo = SubjectNo.Text.Substring(0, GoParent.CommandArgument.Length);
            }
            CommClass.ExecuteSQL("update cw_subject set SubjectNo='" + SubjectNo.Text
                + "',ParentNo='" + ParentNo
                + "',SubjectName='" + SubjectName.Text.Replace(".", "、").Replace("[", "（").Replace("]", "）")
                + "',AccountType='" + AccountType.SelectedValue
                + "',IsEntryData='" + IsEntryData.SelectedValue
                + "',IsDetail='" + IsDetail.SelectedValue
                + _SubjectType
                + " where id='" + ID + "'");
            //添加下级默认科目
            if (GridView1.Rows[e.RowIndex].Cells[7].Enabled && IsDetail.SelectedValue == "0")
            {
                string NextNo = GetNextSubjectNo(SubjectNo.Text);
                string AccountStruct = MainClass.GetFieldFromID(ID, "AccountStruct");
                string NextSubjectNo = SubjectNo.Text.PadRight(SubjectNo.Text.Length + NextNo.Length, '9');
                if (SubjectLevel.Value.IndexOf(NextSubjectNo.Length.ToString()) > 0)
                {
                    CommClass.ExecuteSQL("insert cw_subject(id,parentno,subjectno,subjectname,subjecttype,AccountType,"
                        + "BeginBalance,IsEntryData,IsDetail,AccountStruct)values('"
                        + CommClass.GetRecordID("CW_Subject") + "','"
                        + SubjectNo.Text + "','"
                        + NextSubjectNo + "','其它','"
                        + SubjectType.SelectedValue + "','"
                        + AccountType.SelectedValue + "','0','0','1','"
                        + AccountStruct + "')");
                    CommClass.ExecuteSQL("update cw_subject set IsDetail='0' where subjectno='" + SubjectNo.Text + "'");
                }
            }
            else
            {
                string sno = GridView1.Rows[e.RowIndex].Cells[0].Attributes["OldSubjectNo"];
                if (IsDetail.SelectedValue == "0" && sno != SubjectNo.Text)
                {
                    CommClass.ExecuteSQL("update cw_subject set SubjectNo=concat('" + SubjectNo.Text + "',right(SubjectNo,length(SubjectNo)-"
                        + sno.Length.ToString() + ")),parentno=concat('"
                        + SubjectNo.Text + "',right(parentno,length(parentno)-"
                        + sno.Length.ToString() + ")) where parentno like '" + sno + "%'");
                }
            }
            GridView1.EditIndex = -1;
            InitWebControl();
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "btnDelete")
        {
            GridView1.EditIndex = -1;
            LinkButton btnDelete = (LinkButton)e.CommandSource;
            if (btnDelete.Text == "删除科目")
            {
                GridView1.EditIndex = -1;
                string ParentNo = CommClass.GetFieldFromID(btnDelete.CommandArgument, "parentno");
                CommClass.ExecuteSQL("delete from cw_subject where id='" + btnDelete.CommandArgument + "'");
                if (ParentNo != "000" && CommClass.CountRecord("cw_subject", "parentno='" + ParentNo + "'") == 0)
                {
                    CommClass.ExecuteSQL("update cw_subject set isdetail='1' where subjectno='" + ParentNo + "'");
                    //如果下级无科目，则返回上级科目
                    GoParent_Click(GoParent, new EventArgs());
                    return;
                }
            }
            if (btnDelete.Text == "下级科目")
            {
                SubjectNo.Text = "";
                SubjectName.Text = "";
                GoParent.CommandArgument = CommClass.GetFieldFromID(btnDelete.CommandArgument, "subjectno");
            }
            InitWebControl();
        }
    }

    protected void AddSubject_Click(object sender, EventArgs e)
    {
        GridView1.EditIndex = -1;
        if (CommClass.CheckExist("cw_subject", "subjectno='" + SubjectNo.Text + "'"))
        {
            ExeScript.Text = "<script>alert('此科目代码【" + SubjectNo.Text + "】已存在，请更换别的。')</script>";
        }
        else if (CommClass.CheckExist("cw_subject", "subjectname='" + SubjectName.Text + "' and parentno='" + GoParent.CommandArgument + "'"))
        {
            ExeScript.Text = "<script>alert('同级科目中此科目名称【" + SubjectName.Text + "】已存在，请更换别的。')</script>";
        }
        else
        {
            CommClass.ExecuteSQL("insert cw_subject(id,parentno,subjectno,subjectname,subjecttype,AccountType,BeginBalance,IsEntryData,IsDetail,AccountStruct)values('"
                + CommClass.GetRecordID("CW_Subject") + "','"
                + GoParent.CommandArgument + "','"
                + SubjectNo.Text + "','"
                + SubjectName.Text.Replace(".", "、").Replace("[", "（").Replace("]", "）") + "','"
                + HSubjectType.Value + "','"
                + HAccountType.Value + "','0','0','1','"
                + HAccountStruct.Value + "')");
            if (HIsDetail.Value == "1")
            {
                CommClass.ExecuteSQL("update cw_subject set isdetail='0' where subjectno='" + GoParent.CommandArgument + "'");
            }
            SubjectNo.Text = "";
            SubjectName.Text = "";
            SubjectName.Focus();
        }
        InitWebControl();
    }

    protected void GoParent_Click(object sender, EventArgs e)
    {
        GridView1.EditIndex = -1;
        SubjectNo.Text = "";
        SubjectName.Text = "";
        if (GoParent.CommandArgument != "000")
        {
            GoParent.CommandArgument = CommClass.GetFieldFromNo(GoParent.CommandArgument, "parentno");
        }
        if (GoParent.CommandArgument == "NoDataItem") { GoParent.CommandArgument = "000"; }
        InitWebControl();
    }

    protected void OutSubjectData_Click(object sender, EventArgs e)
    {
        StreamWriter sw = File.CreateText(Server.MapPath("../BackupDB") + "\\cw_subject.sql");
        DataTable TableRecords = CommClass.GetDataTable("select * from cw_subject order by subjectno");
        sw.Write("'" + TableRecords.Columns[0].ColumnName + "'");
        for (int k = 1; k < TableRecords.Columns.Count; k++)
        {
            sw.Write(",'" + TableRecords.Columns[k].ColumnName + "'");
        }
        sw.WriteLine();
        decimal id = 0;
        foreach (DataRow row in TableRecords.Rows)
        {
            sw.Write("'" + (++id).ToString("0000000000") + "'");
            for (int k = 1; k < TableRecords.Columns.Count; k++)
            {
                sw.Write(",'" + row[k].ToString() + "'");
            }
            sw.WriteLine();
        }
        sw.Flush();
        sw.Close();
    }
}
