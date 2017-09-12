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

public partial class AccountInit_AccountSubject : System.Web.UI.Page
{
    private bool LockFirstSubjectPower = true;
    private string[] G_SubjectType ={ "", "资产类", "负债类", "权益类", "成本类", "损益类" };
    
    protected void Page_Load(object sender, EventArgs e)
    {
        bool isPopWindow = (string.IsNullOrEmpty(Request.QueryString["s"]) == false);
        if (isPopWindow)
        {
            UserInfo.CheckSession2();
        }
        if (!PageClass.CheckVisitQuot("000001$000002$000000")) { return; }
        if (!IsPostBack)
        {
            isLockFirstSubjectPower.Value = SysConfigs.LockFirstSubjectPower;
        }
        LockFirstSubjectPower = (isLockFirstSubjectPower.Value == "1");
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            if (MainClass.GetAccountDate().Year == 1900)
            {
                isStartAccount.Value = "0";
                if (isPopWindow)
                {
                    Response.Clear();
                    Response.Write("<script>window.close();alert('当前账套尚未启用，不能执行该操作！');</script>");
                    Response.End();
                }
                if (Session["Powers"].ToString().IndexOf("000001") == -1)
                {
                    StartAccount.Enabled = false;
                }
                if (Session["CheckIsStartAccount"] != null)
                {
                    Session["CheckIsStartAccount"] = null;
                    ExeScript.Text = "<script>alert('请先启用账套，然后才可执行其它操作！')</script>";
                }
                StartAccount.Attributes["onclick"] = "return FStartAccount();";
            }
            else
            {
                isStartAccount.Value = "1";
                WriteBalance.Disabled = true;
                StartAccount.Text = "反启用账套";
                string LastCarryDate = MainClass.GetFieldFromID(UserInfo.AccountID, "LastCarryDate", "cw_account");
                if (isPopWindow || Session["Powers"].ToString().IndexOf("000001") == -1 || LastCarryDate.Length > 0)
                {
                    StartAccount.Enabled = false;
                }
                else
                {
                     StartAccount.Attributes["onclick"] = "return confirm('您确定需要反启用账套吗？')";
                }
            }
            if (Session["Powers"].ToString().IndexOf("000002") == -1)
            {
                AddSubject.Enabled = false;
                UpdateData.Enabled = false;
            }
            SubjectNo.Attributes.Add("onkeyup", "SubjectNo_onkeyup();");
            SubjectNo.Attributes.Add("onkeypress", "return SubjectNo_onkeypress();");
            AddSubject.Attributes.Add("onclick", "return CheckSubmit();");
            UpdateData.Attributes.Add("onclick", "return confirm('您确定需要取自模板库吗？\\n\\n注意：期初余额非零的科目以及其下级科目不予更新！')");
            UpdateBalance.Attributes.Add("onclick", "return confirm('您确定需要同步更新本级余额至上级吗？')");
            SubjectLevel.Value = SysConfigs.SubjectLevel;
            if (SubjectLevel.Value.Length == 0)
            {
                PageClass.UrlRedirect("科目长度限制参数不能为空！", 3);
            }
            string[] subjectLevel = SubjectLevel.Value.Split(',');
            if (subjectLevel.Length >= 3)
            {
                NoLowLength.Value = subjectLevel[1];
            }
            else
            {
                PageClass.UrlRedirect("科目长度限制参数设置有误！", 3);
            }
            InitWebControl();
        }
    }

    /// <summary>
    /// 数据绑定函数
    /// </summary>
    private void InitWebControl()
    {
        isEditState.Value = GridView1.EditIndex.ToString();
        bool isFirstSubject = (GoParent.CommandArgument == "000");
        //获取所有科目代码、科目属性、账式、账户结构、是否明细
        StringBuilder subjects = new StringBuilder();
        DataSet ds = CommClass.GetDataSet("select * from cw_subject order by subjectno");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            subjects.AppendFormat("[{0}]", row["subjectno"].ToString());
            subjects.AppendFormat("{0}:", row["SubjectType"].ToString());
            subjects.AppendFormat("{0}:", row["AccountType"].ToString());
            subjects.AppendFormat("{0}:", row["AccountStruct"].ToString());
            subjects.AppendFormat("{0},", row["IsDetail"].ToString());
        }
        AllMainSubject.Value = subjects.ToString();
        //--
        GoParent.Enabled = !isFirstSubject;
        UpdateBalance.Enabled = !isFirstSubject;
        ((HtmlTableCell)this.FindControl("M" + OSubjectType.Value)).BgColor = "";
        ((HtmlTableCell)this.FindControl("M" + QSubjectType.Value)).BgColor = "#FFCCCC";
        //初始化科目表信息
        OSubjectType.Value = QSubjectType.Value;
        string QueryString = "$ParentNo='" + GoParent.CommandArgument + "'";
        if (QSubjectType.Value != "0")
        {
            QueryString += "$SubjectType='" + QSubjectType.Value + "'";
        }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        ds = CommClass.GetDataSet("select * from cw_subject " + QueryString + " order by subjectno");
        if (ds.Tables[0].Rows.Count == 0)
        {
            if (!isFirstSubject)
            {
                GoParent_Click(GoParent, new EventArgs());
                return;
            }
        }
        else
        {
            if (!isFirstSubject)
            {
                SubjectNo.Text = GoParent.CommandArgument + GetNextSubjectNo(GoParent.CommandArgument);
            }
            GridView1.DataSource = ds.Tables[0].DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
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
                    NameList.Append("/" + CommClass.GetFieldFromNo(GoParent.CommandArgument.Substring(0, int.Parse(SLevel[i])), "SubjectName"));
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
        return "01";
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string myPowers = Session["Powers"].ToString();
            if ((LockFirstSubjectPower && GoParent.CommandArgument == "000") || myPowers.IndexOf("000002") == -1)
            {
                e.Row.Cells[0].Enabled = false;
                e.Row.Cells[1].Enabled = false;
            }
            if (LockFirstSubjectPower)
            {
                e.Row.Cells[2].Enabled = false;
            }
            Label subjectType = (Label)e.Row.Cells[2].FindControl("SubjectTypeS");
            subjectType.Text = G_SubjectType[int.Parse(subjectType.Text)];
            //非编辑状态下单元格处理
            string ID = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
            if (e.Row.RowState.ToString().IndexOf("Edit") == -1)
            {
                Label accountType = (Label)e.Row.Cells[3].FindControl("AccountType");
                accountType.Text = accountType.Text == "0" ? "一般金额账" : "数量金额账";
                Label isEntryData = (Label)e.Row.Cells[6].FindControl("IsEntryData");
                isEntryData.Text = isEntryData.Text == "0" ? "否" : "是";
                Label isDetail = (Label)e.Row.Cells[7].FindControl("IsDetail");
                isDetail.Text = isDetail.Text == "0" ? "否" : "是";
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
                e.Row.Cells[0].Enabled = false;
            }
            //操作选项按钮处理
            string subjectNo = string.Empty;
            string subjectName = string.Empty;
            if (e.Row.RowIndex == GridView1.EditIndex)
            {
                subjectNo = ((TextBox)e.Row.Cells[0].Controls[0]).Text;
                subjectName = ((TextBox)e.Row.Cells[1].Controls[0]).Text;
            }
            else
            {
                subjectNo = e.Row.Cells[0].Text;
                subjectName = e.Row.Cells[1].Text;
            }
            if (CommClass.CheckExist("cw_subject", "parentno='" + subjectNo + "'"))
            {
                LinkButton btnNext = (LinkButton)e.Row.Cells[8].FindControl("btnNext");
                btnNext.Visible = true;
                if (isStartAccount.Value == "0")
                {
                    e.Row.Attributes.Add("ondblclick", "alert('本科目有明细，请从下级明细科目录入！');");
                }
                e.Row.Cells[7].Enabled = false;
            }
            else
            {
                LinkButton btnDelete = (LinkButton)e.Row.Cells[8].FindControl("btnDelete");
                LinkButton btnLock = (LinkButton)e.Row.Cells[8].FindControl("btnLock");
                HiddenField LockState = (HiddenField)e.Row.Cells[8].FindControl("LockState");
                btnDelete.Visible = true;
                btnLock.Visible = true;
                if (LockState.Value == "1")
                {
                    btnLock.Text = "启用";
                    string msg = "您确定需要启用科目“" + subjectName + "”吗？";
                    btnLock.Attributes.Add("onclick", "javascript:return confirm('" + msg + "')");
                    e.Row.Cells[8].Enabled = false;
                }
                else
                {
                    btnLock.Text = "禁用";
                    string msg = "如果禁用该科目，则在录入凭证时不可选该科目，其它账务查询可以正常使用。\\n\\n您确定需要禁用科目“" + subjectName + "”吗？";
                    btnLock.Attributes.Add("onclick", "javascript:return confirm('" + msg + "')");
                }
                if ((LockFirstSubjectPower && GoParent.CommandArgument == "000" && subjectNo.Length == 3) || myPowers.IndexOf("000002") == -1)
                {
                    btnDelete.Enabled = false;
                    btnLock.Enabled = false;
                }
                else
                {
                    string msg = "如果删除该科目可能会发生以下问题：\\n\\n1、账务、报表不平；\\n\\n2、当月凭证分录无科目名称。\\n\\n您确定需要删除科目“" + subjectName + "”吗？";
                    btnDelete.Attributes.Add("onclick", "javascript:return confirm('" + msg + "')");
                }
                if (isStartAccount.Value == "0")
                {
                    e.Row.Attributes["title"] = "提示：双击明细科目所在行可录入期初余额。";
                    e.Row.Attributes.Add("ondblclick", "PopWBalance('" + ID + "','" + e.Row.Cells[4].ClientID + "','" + e.Row.Cells[5].ClientID + "')");
                }
                if (e.Row.Cells[5].Text != "0.00")
                {
                    e.Row.Cells[7].Enabled = false;
                }
            }
            e.Row.Cells[8].Enabled = (e.Row.Cells[8].Enabled && myPowers.IndexOf("000002") != -1);
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
        TableCell tc = GridView1.Rows[e.NewEditIndex].Cells[7];
        if (tc.Enabled)
        {
            tc.Enabled = SubjectLevel.Value.IndexOf(SubjectNo.Text.Length.ToString()) < SubjectLevel.Value.LastIndexOf(",");
        }
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
            string _sno = SubjectNo.Text;
            InitWebControl();
            SubjectNo = (TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[0];
            SubjectNo.Width = 72;
            ExeScript.Text = "<script>alert('此科目代码【" + _sno + "】已存在，请更换别的。')</script>";
        }
        else if (CommClass.CheckExist("cw_subject", "subjectname='" + SubjectName.Text + "' and id<>'" + ID + "' and parentno='" + GoParent.CommandArgument + "'"))
        {
            string _sname = SubjectName.Text;
            InitWebControl();
            SubjectNo = (TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[0];
            SubjectNo.Width = 72;
            ExeScript.Text = "<script>alert('同级科目中此科目名称【" + _sname + "】已存在，请更换别的。')</script>";
        }
        else
        {
            Label SubjectTypeV = (Label)GridView1.Rows[e.RowIndex].Cells[2].FindControl("SubjectTypeV");
            DropDownList AccountType = (DropDownList)GridView1.Rows[e.RowIndex].Cells[3].FindControl("AccountType");
            DropDownList IsEntryData = (DropDownList)GridView1.Rows[e.RowIndex].Cells[6].FindControl("IsEntryData");
            DropDownList IsDetail = (DropDownList)GridView1.Rows[e.RowIndex].Cells[7].FindControl("IsDetail");
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
                + "' where id='" + ID + "'");
            if (GridView1.Rows[e.RowIndex].Cells[7].Enabled && IsDetail.SelectedValue == "0")
            {
                //添加默认下级科目
                string NextNo = GetNextSubjectNo(SubjectNo.Text);
                string AccountStruct = CommClass.GetFieldFromID(ID, "AccountStruct");
                string NextSubjectNo = SubjectNo.Text.PadRight(SubjectNo.Text.Length + NextNo.Length, '9');
                if (SubjectLevel.Value.IndexOf(NextSubjectNo.Length.ToString()) > 0)
                {
                    CommClass.ExecuteSQL("insert cw_subject(id,parentno,subjectno,subjectname,subjecttype,AccountType,"
                        + "BeginBalance,IsEntryData,IsDetail,AccountStruct)values('"
                        + CommClass.GetRecordID("CW_Subject") + "','"
                        + SubjectNo.Text + "','"
                        + NextSubjectNo + "','其它','"
                        + SubjectTypeV.Text + "','"
                        + AccountType.SelectedValue
                        + "','0','0','1','" + AccountStruct + "')");
                    CommClass.ExecuteSQL("update cw_subject set IsDetail='0' where subjectno='" + SubjectNo.Text + "'");
                }
            }
            else
            {
                //修改所有下级科目代码
                string sno = GridView1.Rows[e.RowIndex].Cells[0].Attributes["OldSubjectNo"];
                if (IsDetail.SelectedValue == "0" && sno != SubjectNo.Text)
                {
                    CommClass.ExecuteSQL("update cw_subject set SubjectNo=concat('" + SubjectNo.Text + "',right(SubjectNo,length(SubjectNo)-"
                        + sno.Length.ToString() + ")),parentno=concat('" + SubjectNo.Text + "',right(parentno,length(parentno)-"
                        + sno.Length.ToString() + ")) where parentno like '" + sno + "%'");
                }
            }
            //修改上下级科目属性
            string FirstSubjectNo = SubjectNo.Text.Substring(0, 3);
            CommClass.ExecuteSQL("update cw_subject set AccountType='" + AccountType.SelectedValue + "',IsEntryData='" + IsEntryData.SelectedValue
                + "' where subjectno like '" + FirstSubjectNo + "%'");
            GridView1.EditIndex = -1;
            InitWebControl();
            //写入操作日志
            CommClass.WriteCTL_Log("100001", string.Format("编辑科目：{0}.{1}", SubjectNo.Text, SubjectName.Text));
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandSource.GetType() == typeof(LinkButton))
        {
            GridView1.EditIndex = -1;
            LinkButton btnDelete = (LinkButton)e.CommandSource;
            switch (e.CommandName)
            {
                case "btnNext":
                    SubjectNo.Text = "";
                    SubjectName.Text = "";
                    GoParent.CommandArgument = CommClass.GetFieldFromID(btnDelete.CommandArgument, "subjectno");
                    break;
                case "btnDelete":
                    string _BeginBalance = CommClass.GetFieldFromID(btnDelete.CommandArgument, "BeginBalance");
                    if (_BeginBalance != "0")
                    {
                        ExeScript.Text = "<script>alert('该科目余额不为零，不能删除！')</script>";
                    }
                    else
                    {
                        string subectNo = CommClass.GetFieldFromID(btnDelete.CommandArgument, "subjectno");
                        if (CommClass.CheckExist("cw_entry", string.Concat("subjectno='", subectNo, "'")))
                        {
                            ExeScript.Text = "<script>alert('该科目已有关联凭证，不能删除！')</script>";
                            break;
                        }
                        string ParentNo = CommClass.GetFieldFromID(btnDelete.CommandArgument, "parentno");
                        CommClass.ExecuteSQL("delete from cw_subject where id='" + btnDelete.CommandArgument + "'");
                        if (ParentNo != "000" && CommClass.CountRecord("cw_subject", "parentno='" + ParentNo + "'") == 0)
                        {
                            CommClass.ExecuteSQL("update cw_subject set isdetail='1' where subjectno='" + ParentNo + "'");
                            //如果下级无科目，则返回上级科目
                            GoParent_Click(GoParent, new EventArgs());
                            return;
                        }
                        //写入操作日志
                        CommClass.WriteCTL_Log("100001", "删除科目：" + CommClass.GetFieldFromID(btnDelete.CommandArgument, "subjectno"));
                    }
                    break;
                case "btnLock":
                    if (btnDelete.Text == "禁用")
                    {
                        CommClass.ExecuteSQL("update cw_subject set islock='1' where id='" + btnDelete.CommandArgument + "'");
                    }
                    else
                    {
                        CommClass.ExecuteSQL("update cw_subject set islock='0' where id='" + btnDelete.CommandArgument + "'");
                    }
                    break;
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
        if (CommClass.CheckExist("cw_subject", string.Format("subjectno='{0}' and BeginBalance<>0 and IsDetail='1'", GoParent.CommandArgument)))
        {
            ExeScript.Text = "<script>alert('上级科目余额非零！')</script>";
        }
        else if (CommClass.CheckExist("cw_subject", "subjectno='" + SubjectNo.Text + "'"))
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
            //写入操作日志
            CommClass.WriteCTL_Log("100001", string.Format("增加科目：{0}.{1}", SubjectNo.Text, SubjectName.Text));
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

    protected void UpdateData_Click(object sender, EventArgs e)
    {
        DataRow newRow;
        string Columns = "id,parentno,subjectno,subjectname,subjecttype,AccountType,BeginBalance,IsEntryData,IsDetail,AccountStruct,AccountFlag";
        DataSet MyData = CommClass.GetDataSet("select " + Columns + " from cw_subject order by subjectno");
        DataSet ParentData = MainClass.GetDataSet("select " + Columns + " from cw_subject where unitid='" + UserInfo.UnitID + "' order by subjectno");
        //删除模板库中不存在的科目
        ParentData.Tables[0].PrimaryKey = new DataColumn[] { ParentData.Tables[0].Columns["subjectno"] };
        DataRow[] CanDelete;
        if (SysConfigs.NotDeleteSuject == "1")
        {
            CanDelete = MyData.Tables[0].Select("parentno='000'and BeginBalance=0 and IsDetail='1'");
        }
        else
        {
            CanDelete = MyData.Tables[0].Select("BeginBalance=0");
        }
        for (int i = 0; i < CanDelete.Length; i++)
        {
            newRow = ParentData.Tables[0].Rows.Find(CanDelete[i]["subjectno"].ToString());
            if (newRow == null)
            {
                CanDelete[i].Delete();
            }
        }
        //同步更新模板库科目表
        ListPredicate listPredicate = new ListPredicate();
        MyData.Tables[0].PrimaryKey = new DataColumn[] { MyData.Tables[0].Columns["subjectno"] };
        for (int i = 0; i < ParentData.Tables[0].Rows.Count; i++)
        {
            string subjectNo = ParentData.Tables[0].Rows[i]["subjectno"].ToString();
            newRow = MyData.Tables[0].Rows.Find(subjectNo);
            if (newRow == null)
            {
                if (listPredicate.CheckStartsWith(subjectNo) == false)
                {
                    newRow = MyData.Tables[0].NewRow();
                    newRow["id"] = CommClass.GetRecordID("CW_Subject");
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
            }
            else
            {
                newRow["subjectname"] = ParentData.Tables[0].Rows[i]["subjectname"];
                newRow["subjecttype"] = ParentData.Tables[0].Rows[i]["subjecttype"];
                newRow["AccountStruct"] = ParentData.Tables[0].Rows[i]["AccountStruct"];
                newRow["AccountFlag"] = ParentData.Tables[0].Rows[i]["AccountFlag"];
                string isDetail = ParentData.Tables[0].Rows[i]["IsDetail"].ToString();
                if (newRow["BeginBalance"].ToString() == "0" || isDetail == "1")
                {
                    newRow["IsDetail"] = isDetail;
                }
                else
                {
                    listPredicate.AddListItem(subjectNo);
                }
            }
        }
        CommClass.UpdateDataSet(MyData, Columns);
        PageClass.ShowAlertMsg(this.Page, "已成功取自模板库！");
        InitWebControl();
        //写入操作日志
        CommClass.WriteCTL_Log("100001", "取自模板库");
    }

    protected void UpdateBalance_Click(object sender, EventArgs e)
    {
        InitWebControl();
        decimal balance = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (GridView1.Rows[i].Cells[4].Text == "借")
            {
                balance += decimal.Parse(GridView1.Rows[i].Cells[5].Text);
            }
            else
            {
                balance -= decimal.Parse(GridView1.Rows[i].Cells[5].Text);
            }
        }
        CommClass.ExecuteSQL(string.Format("update cw_subject set BeginBalance='{0}' where subjectno='{1}'", balance.ToString(), GoParent.CommandArgument));
        PageClass.ShowAlertMsg(this.Page, "已成功更新期余额至上级！");
    }

    protected void StartAccount_Click(object sender, EventArgs e)
    {
        //反启用账套
        isStartAccount.Value = "0";
        Session["isStartAccount"] = null;
        Session["CheckIsStartAccount"] = null;
        StartAccount.Text = "启用账套";
        StartAccount.Attributes["onclick"] = "return FStartAccount();";
        CommClass.ExecuteSQL("delete from cw_subjectbudget");
        CommClass.ExecuteSQL("delete from cw_subjectrec");
        CommClass.ExecuteSQL("delete from cw_subjectsum");
        CommClass.ExecuteSQL("delete from cw_lastmonthsum");
        DataTable vouchers = CommClass.GetDataTable("select id from cw_voucher");
        foreach (DataRow rowv in vouchers.Rows)
        {
            string voucherID = rowv["id"].ToString();
            //删除凭证分录
            DataTable ventry = CommClass.GetDataTable(string.Format("select id from cw_entry where voucherid='{0}'", voucherID));
            foreach (DataRow row in ventry.Rows)
            {
                string entryID = row["id"].ToString();
                CommClass.ExecuteSQL(string.Format("delete from cw_entry where id='{0}'", entryID));
                CommClass.ExecuteSQL(string.Format("delete from cw_entrydata where entryid='{0}'", entryID));
            }
        }
        CommClass.ExecuteSQL("delete from cw_voucher");
        MainClass.ExecuteSQL(string.Format("delete from cw_balancealarm where AccountID='{0}'", UserInfo.AccountID));
        MainClass.ExecuteSQL(string.Format("update cw_account set AccountDate='',StartAccountDate='' where id='{0}'", UserInfo.AccountID));
        PageClass.ExcuteScript(this.Page, string.Format("parent.document.getElementById('ctl00_LeftFrame1_CurAccountDate').innerText='财务日期：{0}'", DateTime.Now.ToString("yyyy年MM月dd日")));
        InitWebControl();
    }
}
