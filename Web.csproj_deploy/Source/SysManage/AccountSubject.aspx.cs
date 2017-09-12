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

public partial class SysManage_AccountSubject : System.Web.UI.Page
{
    private string[] G_SubjectType ={ "", "资产类", "负债类", "权益类", "成本类", "损益类" };

    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
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
            string[] _SubjectLevel = SubjectLevel.Value.Split(',');
            if (Session["UnitID"].ToString() == "000000")
            {
                UpdateData.Enabled = false;
                NoLowLength.Value = _SubjectLevel[1];
            }
            else
            {
                if (_SubjectLevel.Length >= 3)
                {
                    //NoLowLength.Value = _SubjectLevel[2] + "," + _SubjectLevel[3];
                }
                else
                {
                    PageClass.UrlRedirect("科目长度限制参数设置有误！", 3);
                }
                UpdateData.Attributes.Add("onclick", "return confirm('您确定需要取自模板库吗？')");
            }
            InitWebControl();
        }
    }

    /// <summary>
    /// 数据绑定函数
    /// </summary>
    private void InitWebControl()
    {
        bool isFirstSubject = (GoParent.CommandArgument == "000");
        //获取所有科目代码、科目属性、账式、账户结构、是否明细
        StringBuilder subjects = new StringBuilder();
        DataSet ds = MainClass.GetDataSet("select * from cw_subject where unitid='" + Session["UnitID"].ToString() + "' order by subjectno");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            subjects.AppendFormat("[{0}]", row["subjectno"].ToString());
            subjects.AppendFormat("{0}:", row["SubjectType"].ToString());
            subjects.AppendFormat("{0}:", row["AccountType"].ToString());
            subjects.AppendFormat("{0}:", row["AccountStruct"].ToString());
            subjects.AppendFormat("{0},", row["IsDetail"].ToString());
        }
        AllMainSubject.Value = subjects.ToString();
        //初始化科目表信息
        GoParent.Enabled = !isFirstSubject;
        string QueryString = "$unitid='" + Session["UnitID"].ToString() + "'";
        QueryString += "$ParentNo='" + GoParent.CommandArgument + "'";
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
        ds = MainClass.GetDataSet("select * from cw_subject " + QueryString + " order by subjectno");
        if (ds.Tables[0].Rows.Count == 0)
        {
            PageClass.BindNoRecords(GridView1, ds);
        }
        else
        {
            if (!isFirstSubject && Session["UnitID"].ToString() != "000000")
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
        if (isFirstSubject)
        {
            ParentSubject.Text = "一级科目";
            PSubjectNo.Text = "一级科目";
        }
        else
        {
            string[] SLevel = SubjectLevel.Value.Split(',');
            StringBuilder NameList = new StringBuilder();
            StringBuilder NoList = new StringBuilder();
            for (int i = 1; i < SLevel.Length; i++)
            {
                if (int.Parse(SLevel[i]) <= GoParent.CommandArgument.Length)
                {
                    NoList.Append("/" + GoParent.CommandArgument.Substring(0, int.Parse(SLevel[i])));
                    NameList.Append("/" + MainClass.GetFieldFromNo(GoParent.CommandArgument.Substring(0, int.Parse(SLevel[i])), "SubjectName"));
                }
                else
                {
                    break;
                }
            }
            if (NameList.Length > 0)
            {
                NoList.Remove(0, 1);
                NameList.Remove(0, 1);
            }
            ParentSubject.Text = NameList.ToString();
            PSubjectNo.Text = NoList.ToString();
        }
    }

    protected string GetNextSubjectNo(string ParentNo)
    {
        //获取下级科目长度
        int sLength = ParentNo.Length + 2;
        string pLength = ParentNo.Length.ToString();
        string[] _SubjectLevel = SubjectLevel.Value.Split(',');
        for (int i = 0; i < _SubjectLevel.Length - 1; i++)
        {
            if (pLength == _SubjectLevel[i])
            {
                sLength = int.Parse(_SubjectLevel[i + 1]);
                break;
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
            string PName = e.Row.Cells[1].Text;
            string ParentNo = e.Row.Cells[0].Text;
            if (e.Row.RowIndex == GridView1.EditIndex)
            {
                PName = ((TextBox)e.Row.Cells[1].Controls[0]).Text;
                ParentNo = ((TextBox)e.Row.Cells[0].Controls[0]).Text;
            }
            bool isEdit = NoLowLength.Value.Contains(ParentNo.Length.ToString());
            e.Row.Cells[3].Enabled = false;
            e.Row.Cells[4].Enabled = false;
            e.Row.Cells[5].Enabled = false;
            e.Row.Cells[6].Enabled = isEdit;
            //非编辑状态下单元格处理
            string ID = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
            if (e.Row.RowState.ToString().IndexOf("Edit") == -1)
            {
                Label SubjectType = (Label)e.Row.Cells[2].FindControl("SubjectType");
                SubjectType.Text = G_SubjectType[int.Parse(SubjectType.Text)];
                Label AccountType = (Label)e.Row.Cells[3].FindControl("AccountType");
                AccountType.Text = AccountType.Text == "0" ? "一般金额账" : "数量金额账";
                Label IsEntryData = (Label)e.Row.Cells[4].FindControl("IsEntryData");
                IsEntryData.Text = IsEntryData.Text == "0" ? "否" : "是";
                Label IsDetail = (Label)e.Row.Cells[5].FindControl("IsDetail");
                IsDetail.Text = IsDetail.Text == "0" ? "否" : "是";
            }
            //删除按钮处理
            LinkButton btnDelete = (LinkButton)e.Row.Cells[7].FindControl("btnDelete");
            if (MainClass.CheckExist("cw_subject", "parentno='" + ParentNo + "' and unitid='" + Session["UnitID"].ToString() + "'"))
            {
                btnDelete.Text = "下级科目";
            }
            else
            {
                btnDelete.Text = "删除科目";
                if (isEdit)
                {
                    btnDelete.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除“" + PName + "”吗？')");
                }
                else
                {
                    btnDelete.Enabled = false;
                }
            }
            //鼠标特效
            e.Row.Attributes["onmouseover"] = "this.style.background='#FFCCCC';";
            if (e.Row.RowState == DataControlRowState.Alternate)
            {
                e.Row.Attributes["onmouseout"] = "this.style.background='#EBF0F6';";
            }
            else
            {
                e.Row.Attributes["onmouseout"] = "this.style.background='#FFFFFF';";
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
        if (MainClass.CheckExist("cw_subject", "subjectno='" + SubjectNo.Text + "' and id<>'" + ID + "' and unitid='" + Session["UnitID"].ToString() + "'"))
        {
            InitWebControl();
            SubjectNo = (TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[0];
            SubjectNo.Width = 72;
            ExeScript.Text = "<script>alert('此科目代码【" + SubjectNo.Text + "】已存在，请更换别的。')</script>";
        }
        else if (MainClass.CheckExist("cw_subject", "subjectname='" + SubjectName.Text + "' and id<>'" + ID + "' and parentno='" + GoParent.CommandArgument + "' and unitid='" + Session["UnitID"].ToString() + "'"))
        {
            InitWebControl();
            SubjectNo = (TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[0];
            SubjectNo.Width = 72;
            ExeScript.Text = "<script>alert('同级科目中此科目名称【" + SubjectName.Text + "】已存在，请更换别的。')</script>";
        }
        else
        {
            DropDownList SubjectType = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("SubjectType");
            DropDownList IsDetail = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("IsDetail");
            MainClass.ExecuteSQL("update cw_subject set SubjectNo='" + SubjectNo.Text + "',SubjectName='" + SubjectName.Text.Replace(".", "、").Replace("[", "（").Replace("]", "）") + "' where id='" + ID + "'");
            //修改上下级科目属性
            MainClass.ExecuteSQL("update cw_subject set SubjectType='" + SubjectType.SelectedValue + "' where subjectno like '" + SubjectNo.Text + "%'");
            //修改下级科目代码
            string sno = GridView1.Rows[e.RowIndex].Cells[0].Attributes["OldSubjectNo"];
            if (IsDetail.SelectedValue == "0" && sno != SubjectNo.Text)
            {
                MainClass.ExecuteSQL("update cw_subject set SubjectNo=concat('" + SubjectNo.Text + "',right(SubjectNo,length(SubjectNo)-"
                    + sno.Length.ToString() + ")),parentno=concat('"
                    + SubjectNo.Text + "',right(parentno,length(parentno)-"
                    + sno.Length.ToString() + ")) where parentno like '" + sno + "%' and unitid='" + Session["UnitID"].ToString() + "'");
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
                string ParentNo = MainClass.GetFieldFromID(btnDelete.CommandArgument, "parentno");
                MainClass.ExecuteSQL("delete from cw_subject where id='" + btnDelete.CommandArgument + "'");
                if (ParentNo != "000" && MainClass.CountRecord("cw_subject", "parentno='" + ParentNo + "' and unitid='" + Session["UnitID"].ToString() + "'") == 0)
                {
                    MainClass.ExecuteSQL("update cw_subject set isdetail='1' where subjectno='" + ParentNo + "' and unitid='" + Session["UnitID"].ToString() + "'");
                    //如果下级无科目，则返回上级科目
                    GoParent_Click(GoParent, new EventArgs());
                    return;
                }
            }
            if (btnDelete.Text == "下级科目")
            {
                SubjectNo.Text = "";
                SubjectName.Text = "";
                GoParent.CommandArgument = MainClass.GetFieldFromID(btnDelete.CommandArgument, "subjectno");
            }
            InitWebControl();
        }
    }

    protected void AddSubject_Click(object sender, EventArgs e)
    {
        GridView1.EditIndex = -1;
        if (SubjectNo.Text.StartsWith(GoParent.CommandArgument) == false)
        {
            int sLength = SubjectNo.Text.Length;
            if (sLength <= 3)
            {
                GoParent.CommandArgument = "000";
            }
            else
            {
                string _sLength = sLength.ToString();
                string[] _SubjectLevel = SubjectLevel.Value.Split(',');
                for (int i = 1; i < _SubjectLevel.Length; i++)
                {
                    if (_sLength == _SubjectLevel[i])
                    {
                        sLength = int.Parse(_SubjectLevel[i - 1]);
                        break;
                    }
                }
                GoParent.CommandArgument = SubjectNo.Text.Substring(0, sLength);
            }
        }
        if (MainClass.CheckExist("cw_subject", "subjectno='" + SubjectNo.Text + "' and unitid='" + Session["UnitID"].ToString() + "'"))
        {
            ExeScript.Text = "<script>alert('此科目代码【" + SubjectNo.Text + "】已存在，请更换别的。')</script>";
        }
        else if (MainClass.CheckExist("cw_subject", "subjectname='" + SubjectName.Text + "' and parentno='" + GoParent.CommandArgument + "' and unitid='" + Session["UnitID"].ToString() + "'"))
        {
            ExeScript.Text = "<script>alert('同级科目中此科目名称【" + SubjectName.Text + "】已存在，请更换别的。')</script>";
        }
        else
        {
            MainClass.ExecuteSQL("insert cw_subject(id,unitid,parentno,subjectno,subjectname,subjecttype,AccountType,BeginBalance,IsEntryData,IsDetail,AccountStruct)values('"
                + MainClass.GetRecordID("CW_Subject") + "','"
                + Session["UnitID"].ToString() + "','"
                + GoParent.CommandArgument + "','"
                + SubjectNo.Text + "','"
                + SubjectName.Text.Replace(".", "、").Replace("[", "（").Replace("]", "）") + "','"
                + HSubjectType.Value + "','"
                + HAccountType.Value + "','0','0','1','"
                + HAccountStruct.Value + "')");
            if (HIsDetail.Value == "1")
            {
                MainClass.ExecuteSQL("update cw_subject set isdetail='0' where subjectno='" + GoParent.CommandArgument + "' and unitid='" + Session["UnitID"] + "'");
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
            GoParent.CommandArgument = MainClass.GetFieldFromNo(GoParent.CommandArgument, "parentno");
        }
        if (GoParent.CommandArgument == "NoDataItem") { GoParent.CommandArgument = "000"; }
        InitWebControl();
    }

    protected void UpdateData_Click(object sender, EventArgs e)
    {
        DataRow newRow;
        string ParentID = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + Session["UnitID"].ToString() + "']", "ParentID");
        string Columns = "id,unitid,parentno,subjectno,subjectname,subjecttype,AccountType,BeginBalance,IsEntryData,IsDetail,AccountStruct,AccountFlag";
        DataSet MyData = MainClass.GetDataSet("select " + Columns + " from cw_subject where unitid='" + Session["UnitID"].ToString() + "' order by subjectno");
        DataSet ParentData = MainClass.GetDataSet("select " + Columns + " from cw_subject where unitid='" + ParentID + "' order by subjectno");
        //删除模板库中不存在的科目
        ParentData.Tables[0].PrimaryKey = new DataColumn[] { ParentData.Tables[0].Columns["subjectno"] };
        for (int i = 0; i < MyData.Tables[0].Rows.Count; i++)
        {
            newRow = ParentData.Tables[0].Rows.Find(MyData.Tables[0].Rows[i]["subjectno"].ToString());
            if (newRow == null)
            {
                MyData.Tables[0].Rows[i].Delete();
            }
        }
        //同步更新模板库科目表
        MyData.Tables[0].PrimaryKey = new DataColumn[] { MyData.Tables[0].Columns["subjectno"] };
        for (int i = 0; i < ParentData.Tables[0].Rows.Count; i++)
        {
            newRow = MyData.Tables[0].Rows.Find(ParentData.Tables[0].Rows[i]["subjectno"].ToString());
            if (newRow == null)
            {
                newRow = MyData.Tables[0].NewRow();
                newRow["id"] = MainClass.GetRecordID("CW_Subject");
                newRow["unitid"] = Session["UnitID"];
                newRow["parentno"] = ParentData.Tables[0].Rows[i]["parentno"];
                newRow["subjectno"] = ParentData.Tables[0].Rows[i]["subjectno"];
                newRow["subjectname"] = ParentData.Tables[0].Rows[i]["subjectname"];
                newRow["subjecttype"] = ParentData.Tables[0].Rows[i]["subjecttype"];
                newRow["AccountType"] = ParentData.Tables[0].Rows[i]["AccountType"];
                newRow["BeginBalance"] = ParentData.Tables[0].Rows[i]["BeginBalance"];
                newRow["IsEntryData"] = ParentData.Tables[0].Rows[i]["IsEntryData"];
                newRow["IsDetail"] = ParentData.Tables[0].Rows[i]["IsDetail"];
                newRow["AccountStruct"] = ParentData.Tables[0].Rows[i]["AccountStruct"];
                newRow["AccountFlag"] = ParentData.Tables[0].Rows[i]["AccountFlag"];
                MyData.Tables[0].Rows.Add(newRow);
            }
            else
            {
                newRow["subjectname"] = ParentData.Tables[0].Rows[i]["subjectname"];
                newRow["subjecttype"] = ParentData.Tables[0].Rows[i]["subjecttype"];
                newRow["IsDetail"] = ParentData.Tables[0].Rows[i]["IsDetail"];
                newRow["AccountStruct"] = ParentData.Tables[0].Rows[i]["AccountStruct"];
                newRow["AccountFlag"] = ParentData.Tables[0].Rows[i]["AccountFlag"];
            }
        }
        MainClass.UpdateDataSet(MyData, Columns);
        InitWebControl();
    }
}
