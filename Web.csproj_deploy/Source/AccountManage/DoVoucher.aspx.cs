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
using System.Text.RegularExpressions;
using System.Text;

public partial class AccountManage_DoVoucher : System.Web.UI.Page
{
    DataSet Entry;
    DataSet AllVoucher;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000003")) { return; }
        ExeScript.Text = "";
        curRowID.Value = "";
        curRowIndex.Value = "";
        if (!IsPostBack)
        {
            DateTime _AccountDate = MainClass.GetAccountDate();
            //检测是否强制进行年末收支自动结转
            string lastCarryDate = MainClass.GetFieldFromID(UserInfo.AccountID, "LastCarryDate", "cw_account");
            if (lastCarryDate == string.Format("{0}年12月31日", _AccountDate.Year.ToString()))
            {
                string accountYear = MainClass.GetFieldFromID(UserInfo.AccountID, "AccountYear", "cw_account");
                if (accountYear.IndexOf(_AccountDate.AddYears(1).Year.ToString()) == -1)
                {
                    PageClass.UrlRedirect("请进行年末收支自动结转操作！", 3);
                    return;
                }
                else
                {
                    if (_AccountDate.Month != 1)
                    {
                        string YearCarryVoucher = MainClass.GetFieldFromID(UserInfo.AccountID, "YearCarryVoucher", "cw_account");
                        string[] carryVoucherNo = YearCarryVoucher.Split('-');
                        if (carryVoucherNo.Length == 2)
                        {
                            CarryVoucherNo.Value = carryVoucherNo[1];
                        }
                        ExeScript2.Text = "<script>$('UVoucherNo').disabled='disabled';$('DVoucherNo').disabled='disabled';</script>";
                    }
                }
            }
            ////////////////////////////////////////////////////////////////
            string QueryString = "where VoucherDate like '" + _AccountDate.ToString("yyyy年MM月") + "%'";
            ViewState["AllVoucher"] = CommClass.GetDataSet("select * from cw_voucher " + QueryString + " order by voucherno");
            DelVouncher.Attributes.Add("onclick", "return confirm('您确定需要删除当前凭证吗？');");
            UVoucherNo.Attributes.Add("onclick", "return ModifyVoucherNo('您确定需要执行凭证编号前提操作吗？');");
            DVoucherNo.Attributes.Add("onclick", "return ModifyVoucherNo('您确定需要执行凭证编号后推操作吗？');");
            ////////////////////////////////////////////////////////////////
            GridView2.Attributes.Add("onselectstart", "return false;");
            VoucherNo.Attributes.Add("onkeypress", "return CheckNumber();");
            AddonsCount.Attributes.Add("onblur", "AddAddons(this.value);");
            AddonsCount.Attributes.Add("onkeypress", "return CheckNumber();");
            CurrentCount.Attributes.Add("onkeypress", "return CheckNumber();");
            AddNewRow.Attributes.Add("onclick", "return CheckModify();");
            PreVoucher.Attributes.Add("onclick", "return CheckModify();");
            NextVoucher.Attributes.Add("onclick", "return CheckModify();");
            CreateVoucher.Attributes.Add("onclick", "return CheckModify();");
            VoucherNoList.Attributes.Add("onchange", "$('ModiFlag').Value='0';");
            VoucherDate.Attributes.Add("onclick", "SetVoucherDate();");
            VoucherDate.Attributes.Add("readonly", "readonly");
            CurrentDate.Attributes.Add("readonly", "readonly");
            CommitVoucher.Attributes.Add("onclick", "return CheckVoucherSave();");
            ////////////////////////////////////////////////////////////////
            aYear.Value = _AccountDate.Year.ToString();
            aMonth.Value = _AccountDate.Month.ToString("00");
            aDay.Value = _AccountDate.Day.ToString("00");
            tYear.Value = DateTime.Now.Year.ToString();
            tMonth.Value = DateTime.Now.Month.ToString("00");
            tDay.Value = DateTime.Now.Day.ToString("00");
            string[] WeekDay ={ "日", "一", "二", "三", "四", "五", "六" };
            tWeek.Value = WeekDay[Convert.ToInt32(DateTime.Now.DayOfWeek)];
            ///////////////////////////////////////////////////////////////
            AccountName.Text = UserInfo.AccountName;
            RedFigureFlag.Value = CommClass.GetSysPara("RedFigure");
            //获取费用控制额
            ManageSubject.Value = SysConfigs.ManageSubject;
            AllSubjectFee.Value = MainClass.GetSysPara("LimitFee" + UserInfo.AccountID);
            if (AllSubjectFee.Value == "NoDataItem")
            {
                AllSubjectFee.Value = "";
            }
            else
            {
                AllSubjectFee.Value = Regex.Replace(AllSubjectFee.Value, string.Concat("\\[(?!", ManageSubject.Value, ")[^\\]]*?\\]"), "");
                AllSubjectFee.Value = AllSubjectFee.Value.Replace("[", "");
            }
            //获取报警参数
            DataTable alarm = MainClass.GetDataTable("select IndexSubject,IndexValue,IndexType from cw_indexmonitor where UnitID='999999' and (IndexType='0' or IndexType='3') order by IndexSubject asc");
            StringBuilder alarms = new StringBuilder();
            foreach (DataRow row in alarm.Rows)
            {
                string subjectNo = row["IndexSubject"].ToString();
                alarms.AppendFormat("{0}|{1}|{2}$", subjectNo.Substring(0, subjectNo.IndexOf(".")), row["IndexValue"].ToString(), row["IndexType"].ToString());
            }
            BalanceAlarm.Value = alarms.ToString();
            //校验：同时只能一个会话可访问账套凭证
            if (MainClass.GetFieldFromID(UserInfo.AccountID, "SessionID", "cw_account") != Session.SessionID)
            {
                string CTLDateTime = MainClass.GetFieldFromID(UserInfo.AccountID, "CTLDateTime", "cw_account");
                if (CTLDateTime.Length > 0 && CTLDateTime != "NoDataItem")
                {
                    TimeSpan ts = DateTime.Now.Subtract(Convert.ToDateTime(CTLDateTime));
                    if (Math.Abs(ts.TotalSeconds) <= 30) { CTLFlag.Value = "1"; }
                }
            }
        }
        AllVoucher = (DataSet)ViewState["AllVoucher"];
        AllVoucher.Tables[0].PrimaryKey = new DataColumn[] { AllVoucher.Tables[0].Columns["id"] };
        if (!IsPostBack)
        {
            CreateVoucher_Click(CreateVoucher, new EventArgs());
            if (!DbHelper.ValidateVerifySignedHash()) { return; }
        }
        //清空原始凭证记录缓存
        if (ModiFlag.Value == "0")
        {
            for (int i = Session.Keys.Count - 1; i >= 0; i--)
            {
                if (Session.Keys[i].ToString().StartsWith("EntryData"))
                {
                    Session.RemoveAt(i);
                }
            }
        }
        VoucherNoList.Focus();
    }
    /// <summary>
    /// 初始化日期选择框
    /// </summary>
    protected void InitDateControl()
    {
        DateTime AccountDate = MainClass.GetAccountDate();
        //创建数据表
        DataTable dt = new DataTable();
        dt.Columns.Add("id");
        ((BoundField)GridView2.Columns[0]).DataField = "id";
        for (int k = 0; k < 7; k++)
        {
            dt.Rows.Add(new string[] { "" });
        }
        //绑定日期表格
        GridView2.DataSource = dt.DefaultView;
        GridView2.DataBind();
        string WeekDay = "日一二三四五六";
        for (int i = 0; i < 7; i++)
        {
            GridView2.Rows[0].Cells[i].Text = WeekDay.Substring(i, 1);
            GridView2.Rows[0].Style["background"] = "#f6f6f6";
        }
        int ShowDay = 1;
        bool StartShowFlag = false;
        int FirstDayWeek = (int)Convert.ToDateTime(AccountDate.ToString("yyyy-MM-01")).DayOfWeek;
        int _DaysInMonth = DateTime.DaysInMonth(AccountDate.Year, AccountDate.Month);
        for (int i = 1; i < 7; i++)
        {
            for (int k = 0; k < 7; k++)
            {
                if (ShowDay > _DaysInMonth)
                {
                    break;
                }
                if (FirstDayWeek != k && !StartShowFlag)
                {
                    continue;
                }
                else
                {
                    StartShowFlag = true;
                }
                string cellID = GridView2.Rows[i].Cells[k].ClientID;
                GridView2.Rows[i].Cells[k].Text = ShowDay.ToString("00");
                GridView2.Rows[i].Cells[k].Attributes["onclick"] = string.Concat("SetAccountDate('", cellID, "','", ShowDay.ToString("00"), "')");
                GridView2.Rows[i].Cells[k].Attributes["onmouseover"] = "this.className='mouseover2';";
                GridView2.Rows[i].Cells[k].Attributes["onmouseout"] = "this.className='';";
                if (ShowDay == AccountDate.Day)
                {
                    CurDateCell.Value = cellID;
                    GridView2.Rows[i].Cells[k].Attributes["style"] = "background:red;color:white";
                }
                ShowDay++;
            }
        }
    }
    /// <summary>
    /// 初始化窗体控件
    /// </summary>
    protected void InitWebControl()
    {
        InitDateControl();
        int VRowIndex = int.Parse(RecordRowIndex.Value);
        if (VRowIndex == 0)
        {
            PreVoucher.Enabled = false;
        }
        else
        {
            PreVoucher.Enabled = true;
        }
        if (VRowIndex == AllVoucher.Tables[0].Rows.Count - 1)
        {
            NextVoucher.Enabled = false;
        }
        else
        {
            NextVoucher.Enabled = true;
        }
        DataRow row = AllVoucher.Tables[0].Rows[VRowIndex];
        bool isNewVoucher = row.RowState == DataRowState.Added;
        VoucherDate.Text = row["voucherdate"].ToString();
        VoucherNo.Text = row["voucherno"].ToString();
        Director.Text = row["Director"].ToString();
        Accountant.Text = row["Accountant"].ToString();
        Assessor.Text = row["Assessor"].ToString();
        DoBill.Text = row["DoBill"].ToString();
        //冲红凭证红色显示/////////////////////////////////////
        if (row["ReverseVoucherID"].ToString().Length > 0)
        {
            for (int i = 3; i < GridView1.Columns.Count; i++)
            {
                if (GridView1.Columns[i].ItemStyle.CssClass == "bb" || GridView1.Columns[i].ItemStyle.CssClass == "dd")
                {
                    GridView1.Columns[i].ItemStyle.CssClass += "1";
                }
                else
                {
                    break;
                }
            }
        }
        else
        {
            for (int i = 3; i < GridView1.Columns.Count; i++)
            {
                switch (GridView1.Columns[i].ItemStyle.CssClass)
                {
                    case "bb1":
                        GridView1.Columns[i].ItemStyle.CssClass = "bb";
                        break;
                    case "dd1":
                        GridView1.Columns[i].ItemStyle.CssClass = "dd";
                        break;
                }
            }
        }
        ///////////////////////////////////////////////////////
        if (VoucherID.Value != row["id"].ToString())
        {
            AllInfo.Value = "";
            ModiFlag.Value = "0";
            VoucherID.Value = row["id"].ToString();
        }
        if (row["DelFlag"].ToString() == "0")
        {
            CancelVouncher.Text = "作废凭证";
            CancelVouncher.Attributes.Add("onclick", "return CancelVouncher('您确认需要作废该凭证吗？');");
        }
        else
        {
            CancelVouncher.Text = "取消作废";
            CancelVouncher.Attributes.Add("onclick", "return CancelVouncher('您确认需要取消作废该凭证吗？');");
        }
        if (row["IsAuditing"].ToString() == "1" || row["DelFlag"].ToString() == "1")
        {
            EnableFlag.Value = "1";
            Overlay.Attributes["style"] = "display:";
            Lightbox.Attributes["style"] = "display:";
        }
        else
        {
            EnableFlag.Value = "0";
            Overlay.Attributes["style"] = "display:none";
            Lightbox.Attributes["style"] = "display:none";
        }
        if (row["IsAuditing"].ToString() == "1")
        {
            CancelVouncher.Enabled = false;
            CommitVoucher.Enabled = false;
            CreateVoucher.Enabled = true;
            Lightbox.InnerHtml = "已审核，禁止操作！";
        }
        else
        {
            CancelVouncher.Enabled = true;
            CommitVoucher.Enabled = true;
            CreateVoucher.Enabled = true;
            Lightbox.InnerHtml = "已作废，禁止操作！";
        }
        if (CTLFlag.Value == "1")
        {
            EnableFlag.Value = "2";
            Overlay.Attributes["style"] = "display:";
            Lightbox.Attributes["style"] = "display:";
            Lightbox.InnerHtml = "已有用户正在使用，请30秒后再尝试刷新！";
        }
        //检测是否可前提凭证号
        decimal pvno = decimal.Parse(VoucherNo.Text) - 1;
        UVoucherNo.Enabled = VRowIndex == 0 ? pvno > 100000 : AllVoucher.Tables[0].Rows[VRowIndex - 1]["voucherno"].ToString() != pvno.ToString();
        DVoucherNo.Enabled = !isNewVoucher;
        DelVouncher.Enabled = !isNewVoucher;
        CreateVoucher.Enabled = !isNewVoucher;
        //年末收支自动结转凭证不可删除
        if (string.Compare(VoucherNo.Text, CarryVoucherNo.Value) <= 0)
        {
            PageClass.ExcuteScript(this.Page, "$('DelVouncher').disabled='disabled';");
        }
        //凭证分录处理
        if (AllInfo.Value.Length > 0)
        {
            SaveTempVoucher();
        }
        else
        {
            Entry = CommClass.GetDataSet("select * from cw_entry where voucherid='" + VoucherID.Value + "' order by id asc");
        }
        if (Entry.Tables[0].Rows.Count > int.Parse(RowsCount.Value))
        {
            RowsCount.Value = Entry.Tables[0].Rows.Count.ToString();
        }
        //创建凭证表格
        CreateGridView();
        //初始化表格值
        for (int m = 0; m < Entry.Tables[0].Rows.Count; m++)
        {
            ((TextBox)GridView1.Rows[m].Cells[0].FindControl("txtComm1")).Text = Entry.Tables[0].Rows[m]["vsummary"].ToString();
            //分录的科目名称
            string SubjectNo = Entry.Tables[0].Rows[m]["subjectno"].ToString();
            GridView1.Rows[m].Cells[1].Text = CommClass.GetFieldFromNo(SubjectNo.Substring(0, 3), "SubjectName");
            if (SubjectNo.Length > 3)
            {
                ((TextBox)GridView1.Rows[m].Cells[2].FindControl("txtComm2")).Text = CommClass.GetDetailSubject(SubjectNo);
            }
            ((HiddenField)this.FindControl("v0" + m.ToString())).Value = Entry.Tables[0].Rows[m]["vsummary"].ToString();
            ((HiddenField)this.FindControl("v1" + m.ToString())).Value = Entry.Tables[0].Rows[m]["subjectno"].ToString();
            //流量指定初始化
            //string cashItemID = CommClass.GetTableValue("cw_cashflow", "CashItemID", string.Concat("EntryID='", Entry.Tables[0].Rows[m]["id"].ToString(), "'"));
            //if (cashItemID != "NoDataItem")
            //{
            //    ((HiddenField)this.FindControl("v4" + m.ToString())).Value = cashItemID;
            //}
            //分录发生额的处理
            decimal sumMoney = TypeParse.StrToDecimal(Entry.Tables[0].Rows[m]["summoney"].ToString(), 0);
            if (sumMoney < 0)
            {
                ((HiddenField)this.FindControl("v2" + m.ToString())).Value = sumMoney.ToString();
                sumMoney = Math.Abs(sumMoney);
                ((TextBox)GridView1.Rows[m].Cells[4].FindControl("txtComm4")).Text = sumMoney.ToString("#.00");
            }
            else
            {
                ((HiddenField)this.FindControl("v2" + m.ToString())).Value = "+" + sumMoney.ToString();
                ((TextBox)GridView1.Rows[m].Cells[3].FindControl("txtComm3")).Text = sumMoney.ToString("#.00");
            }
        }
        //输出凭证列表
        VoucherNoList.Items.Clear();
        foreach(DataRow vrows in AllVoucher.Tables[0].Rows)
        {
            VoucherNoList.Items.Add(new ListItem(vrows["voucherno"].ToString(), vrows["id"].ToString()));
        }
        VoucherNoList.Text = VoucherID.Value;
        //--
        if (isNewVoucher)
        {
            VoucherNo.BorderWidth = 1;
            VoucherNo.Attributes.Remove("readonly");
        }
        else
        {
            VoucherNo.BorderWidth = 0;
            VoucherNo.Attributes["readonly"] = "readonly";
        }
    }
    protected void VoucherList_SelectedIndexChanged(object sender, EventArgs e)
    {
        RecordRowIndex.Value = VoucherNoList.SelectedIndex.ToString();
        InitWebControl();
    }
    /// <summary>
    /// 临时保存凭证
    /// </summary>
    /// <returns></returns>
    protected bool SaveTempVoucher()
    {
        int cl = 0;
        string[] RowCollections = Regex.Split(AllInfo.Value, "!--!");
        Entry = CommClass.GetDataSet("select * from cw_entry where voucherid='" + VoucherID.Value + "' order by id asc");
        //CashFlow = CommClass.GetDataSet("select * from cw_cashflow where voucherid='" + VoucherID.Value + "' order by EntryID asc");
        for (int i = 0; i < RowCollections.Length - 1; i++)
        {
            if (RowCollections[i].Length == 0)
            {
                continue;
            }
            string entryID = string.Empty;
            string[] CellCollections = Regex.Split(RowCollections[i], "!__!");
            if (i < Entry.Tables[0].Rows.Count)
            {
                entryID = Entry.Tables[0].Rows[i]["id"].ToString();
                Entry.Tables[0].Rows[i]["vsummary"] = CellCollections[0];
                Entry.Tables[0].Rows[i]["subjectno"] = CellCollections[1];
                if (CellCollections[2].Length > 0)
                {
                    Entry.Tables[0].Rows[i]["summoney"] = CellCollections[2];
                }
                else
                {
                    Entry.Tables[0].Rows[i]["summoney"] = 0;
                }
            }
            else
            {
                DataRow NewDataRow = Entry.Tables[0].NewRow();
                entryID = CommClass.GetRecordID("CW_Entry");
                NewDataRow["id"] = entryID;
                NewDataRow["VoucherID"] = VoucherID.Value;
                NewDataRow["vsummary"] = CellCollections[0];
                NewDataRow["subjectno"] = CellCollections[1];
                if (CellCollections[2].Length > 0)
                {
                    NewDataRow["summoney"] = CellCollections[2];
                }
                else
                {
                    NewDataRow["summoney"] = 0;
                }
                Entry.Tables[0].Rows.Add(NewDataRow);
            }
            //流量指定保存
            //if (CashFlowFlag.Value == "1" && CellCollections[3].Length > 0 && CellCollections[3] != "000000")
            //{
            //    if (cl < CashFlow.Tables[0].Rows.Count)
            //    {
            //        CashFlow.Tables[0].Rows[cl]["EntryID"] = Entry.Tables[0].Rows[i]["id"];
            //        CashFlow.Tables[0].Rows[cl]["CashItemID"] = CellCollections[3];
            //    }
            //    else
            //    {
            //        DataRow NewDataRow = CashFlow.Tables[0].NewRow();
            //        NewDataRow["id"] = CommClass.GetRecordID("CW_CashFlow");
            //        NewDataRow["VoucherID"] = VoucherID.Value;
            //        NewDataRow["EntryID"] = entryID;
            //        NewDataRow["CashItemID"] = CellCollections[3];
            //        CashFlow.Tables[0].Rows.Add(NewDataRow);
            //    }
            //    cl++;
            //}
        }
        int k = RowCollections.Length - 1;
        while (k < Entry.Tables[0].Rows.Count)
        {
            CommClass.ExecuteSQL("delete from cw_entrydata where entryid='" + Entry.Tables[0].Rows[k]["id"].ToString() + "'");
            Entry.Tables[0].Rows[k].Delete();
            k++;
        }
        return RowCollections.Length > 1 || k > RowCollections.Length - 1;
    }
    /// <summary>
    /// 创建凭证分录行
    /// </summary>
    protected void CreateGridView()
    {
        //创建数据表
        DataTable dt = new DataTable();
        dt.Columns.Add("id");
        for (int k = 0; k < int.Parse(RowsCount.Value) + 1; k++)
        {
            dt.Rows.Add(new string[] { "" });
        }
        //绑定表格
        GridView1.DataSource = dt.DefaultView;
        GridView1.DataBind();
        //设置统计行
        int _RecordRowIndex = int.Parse(RecordRowIndex.Value);
        GridViewRow gCountRow = GridView1.Rows[GridView1.Rows.Count - 1];
        string ID = AllVoucher.Tables[0].Rows[_RecordRowIndex]["id"].ToString();
        AddonsCount.Text = AllVoucher.Tables[0].Rows[_RecordRowIndex]["AddonsCount"].ToString();
        HasSelAppendices.Value = AllVoucher.Tables[0].Rows[_RecordRowIndex]["Addons"].ToString();
        AddonsCellID.Value = gCountRow.Cells[0].ClientID;
        gCountRow.Cells[0].Attributes["ondblclick"] = "";
        gCountRow.Cells[0].Attributes["style"] = "color:red;";
        gCountRow.Cells[0].Text = "附单数：_________________张____<a href='javascript:void(0)' onclick='UploadFile()'>上传</a>".Replace("_", "&nbsp;");
        PageClass.ExcuteScript(this.Page, string.Concat("SetObjectPos('", AddonsCellID.Value, "','AddonsDiv');"));
        gCountRow.Cells[2].ColumnSpan = 2;
        gCountRow.Cells[2].Text = PageClass.PadLeftM("<font color='red'>合", 24, "&nbsp;") + PageClass.PadLeftM("计</font>", 24, "&nbsp;");
        gCountRow.Cells[1].Visible = false;
        gCountRow.Cells[3].Style["text-align"] = "center";
        gCountRow.Cells[4].Style["text-align"] = "center";
        TextBox txtComm3 = (TextBox)gCountRow.Cells[3].FindControl("txtComm3");
        TextBox txtComm4 = (TextBox)gCountRow.Cells[4].FindControl("txtComm4");
        UtilsPage.SetTextBoxReadOnly(txtComm3);
        UtilsPage.SetTextBoxReadOnly(txtComm4);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string ID = "X" + e.Row.RowIndex.ToString("00000");
            string rowIndex = e.Row.RowIndex.ToString();
            if (e.Row.RowIndex < Entry.Tables[0].Rows.Count)
            {
                ID = Entry.Tables[0].Rows[e.Row.RowIndex]["id"].ToString();
            }
            e.Row.Attributes.Add("onclick", "OnRowClick(" + rowIndex + ",'" + e.Row.ClientID + "')");
            e.Row.Cells[0].Attributes.Add("oncontextmenu", "return ChooseSummary(" + rowIndex + ");");
            TextBox txt1 = (TextBox)e.Row.FindControl("txtComm1");
            txt1.Attributes.Add("ondblclick", "WriteSummray(" + rowIndex + ");");
            TextBox txt2 = (TextBox)e.Row.FindControl("txtComm2");
            txt2.Attributes["snid"] = e.Row.Cells[1].ClientID;
            TextBox txt3 = (TextBox)e.Row.FindControl("txtComm3");
            TextBox txt4 = (TextBox)e.Row.FindControl("txtComm4");
            UtilsPage.SetTextBoxOnlyNumber(txt3);
            UtilsPage.SetTextBoxOnlyNumber(txt4);
            e.Row.Cells[1].Attributes.Add("ondblclick", "OnCellClick('" + ID + "'," + rowIndex + ",0,'" + txt2.ClientID + "')");
            e.Row.Cells[2].Attributes.Add("ondblclick", "OnCellClick('" + ID + "'," + rowIndex + ",2,'" + txt2.ClientID + "')");
            e.Row.Cells[3].Attributes.Add("ondblclick", "OnCellClick('" + ID + "'," + rowIndex + ",3,'" + txt3.ClientID + "')");
            e.Row.Cells[4].Attributes.Add("ondblclick", "OnCellClick('" + ID + "'," + rowIndex + ",4,'" + txt4.ClientID + "')");
            //摘要
            HiddenField hf = new HiddenField();
            hf.ID = "v0" + rowIndex;
            form1.Controls.Add(hf);
            //科目代码
            hf = new HiddenField();
            hf.ID = "v1" + rowIndex;
            form1.Controls.Add(hf);
            //科目余额
            hf = new HiddenField();
            hf.ID = "v2" + rowIndex;
            form1.Controls.Add(hf);
            //结转余额
            hf = new HiddenField();
            hf.ID = "v3" + rowIndex;
            form1.Controls.Add(hf);
            //流量指定
            hf = new HiddenField();
            hf.ID = "v4" + rowIndex;
            form1.Controls.Add(hf);
        }
    }
    protected void AddNewRow_Click(object sender, EventArgs e)
    {
        int nc = TypeParse.StrToInt(TextBox2.Text, 1);
        int sc = int.Parse(RowsCount.Value) + nc;
        RowsCount.Value = sc.ToString();
        InitWebControl();
    }
    protected void PreVoucher_Click(object sender, EventArgs e)
    {
        RowsCount.Value = RowsCountMin.Value;
        int RowID = int.Parse(RecordRowIndex.Value) - 1;
        if (RowID >= 0)
        {
            RecordRowIndex.Value = RowID.ToString();
        }
        InitWebControl();
    }
    protected void NextVoucher_Click(object sender, EventArgs e)
    {
        RowsCount.Value = RowsCountMin.Value;
        int RowID = int.Parse(RecordRowIndex.Value) + 1;
        if (RowID <= AllVoucher.Tables[0].Rows.Count - 1)
        {
            RecordRowIndex.Value = RowID.ToString();
        }
        InitWebControl();
    }
    /// <summary>
    /// 新建空凭证
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CreateVoucher_Click(object sender, EventArgs e)
    {
        string LastVoucherNo = "100000";
        int LastRowIndex = AllVoucher.Tables[0].Rows.Count - 1;
        if (LastRowIndex >= 0)
        {
            if (AllVoucher.Tables[0].Rows[LastRowIndex].RowState == DataRowState.Added)
            {
                if (RecordRowIndex.Value == LastRowIndex.ToString())
                {
                    ExeScript.Text = "<script>alert('最后一个凭证尚未保存，不能创建新的凭证！');</script>";
                }
                else
                {
                    RecordRowIndex.Value = LastRowIndex.ToString();
                }
                InitWebControl();
                return;
            }
            else
            {
                LastVoucherNo = AllVoucher.Tables[0].Rows[LastRowIndex]["voucherno"].ToString();
            }
        }
        RowsCount.Value = RowsCountMin.Value;
        int _LastVoucherNo = int.Parse(LastVoucherNo) + 1;
        DataRow NewDataRow = AllVoucher.Tables[0].NewRow();
        NewDataRow["id"] = CommClass.GetRecordID("CW_Voucher");
        NewDataRow["voucherno"] = _LastVoucherNo.ToString();
        NewDataRow["voucherfrom"] = "GA";
        NewDataRow["voucherdate"] = string.Format("{0}年{1}月{2}日", aYear.Value, aMonth.Value, aDay.Value);
        NewDataRow["IsAuditing"] = "0";
        NewDataRow["IsRecord"] = "0";
        NewDataRow["Director"] = MainClass.GetFieldFromID(UserInfo.AccountID, "director", "cw_account");
        NewDataRow["DoBill"] = Session["RealName"].ToString();
        NewDataRow["Assessor"] = "";
        NewDataRow["Accountant"] = "";
        NewDataRow["Addons"] = "";
        NewDataRow["AddonsCount"] = "";
        NewDataRow["IsHasAlarm"] = "0";
        NewDataRow["DelFlag"] = "0";
        AllVoucher.Tables[0].Rows.Add(NewDataRow);
        ViewState["AllVoucher"] = AllVoucher;
        RecordRowIndex.Value = (AllVoucher.Tables[0].Rows.Count - 1).ToString();
        if (ModiFlag.Value == "0") { InitWebControl(); }
    }
    /// <summary>
    /// 保存凭证
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void SaveVoucher_Click(object sender, EventArgs e)
    {
        string UseDate = ValidateClass.ReadXMLNodeText("FinancialDB/RegInfo", "UseDate");
        string NowVoucherDate = CurrentDate.Text.Replace("年", "-").Replace("月", "-").Replace("日", "");
        if (string.Compare(NowVoucherDate, UseDate) > 0)
        {
            InitWebControl();
            PageClass.ShowAlertMsg(this.Page, string.Concat("您可以使用期限为：", UseDate, "，请联系我公司进行续期。"));
            return;
        }
        string voucherNo = VoucherNo.Text;
        int _RecordRowIndex = int.Parse(RecordRowIndex.Value);
        bool isNewVoucher = AllVoucher.Tables[0].Rows[_RecordRowIndex].RowState == DataRowState.Added;
        if (isNewVoucher)
        {
            //检测凭证编号是否已存在
            for (int i = 0; i < AllVoucher.Tables[0].Rows.Count - 1; i++)
            {
                if (AllVoucher.Tables[0].Rows[i]["voucherno"].ToString() == voucherNo)
                {
                    InitWebControl();
                    VoucherNo.Text = voucherNo;
                    ExeScript.Text = "<script>alert('凭证编号：" + voucherNo + "已存在！');</script>";
                    return;
                }
            }
        }
        else
        {
            //删除最后一张新增的空凭证
            AllVoucher.Tables[0].Rows[AllVoucher.Tables[0].Rows.Count - 1].Delete();
        }
        //保存凭证分录
        if (SaveTempVoucher())
        {
            CommClass.UpdateDataSet(Entry);
            //CommClass.UpdateDataSet(CashFlow);
        }
        CashFlowFlag.Value = "0";
        //保存凭证信息
        AllVoucher.Tables[0].Rows[_RecordRowIndex]["voucherno"] = VoucherNo.Text;
        AllVoucher.Tables[0].Rows[_RecordRowIndex]["voucherdate"] = CurrentDate.Text;
        AllVoucher.Tables[0].Rows[_RecordRowIndex]["addonscount"] = CurrentCount.Text;
        AllVoucher.Tables[0].Rows[_RecordRowIndex]["addons"] = HasSelAppendices.Value;
        //预警设置
        string voucherID = AllVoucher.Tables[0].Rows[_RecordRowIndex]["id"].ToString();
        if (IsHasAlarm.Value == "0")
        {
            if (AllVoucher.Tables[0].Rows[_RecordRowIndex]["IsHasAlarm"].ToString() == "1")
            {
                MainClass.ExecuteSQL(string.Format("delete from cw_balancealarm where accountid='{0}' and voucherid='{1}'", UserInfo.AccountID, voucherID));
                AllVoucher.Tables[0].Rows[_RecordRowIndex]["IsHasAlarm"] = "0";
            }
        }
        else
        {
            AllVoucher.Tables[0].Rows[_RecordRowIndex]["IsHasAlarm"] = "1";
            if (MainClass.CheckExist("cw_balancealarm", string.Format("accountid='{0}' and voucherid='{1}'", UserInfo.AccountID, voucherID)) == false)
            {
                MainClass.ExecuteSQL(string.Format("insert into cw_balancealarm(id,UnitID,AccountID,VoucherID,VoucherNo,VoucherDate,AlarmType,DoState,BookTime)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                    new object[] { MainClass.GetRecordID("CW_BalanceAlarm"), UserInfo.UnitID, UserInfo.AccountID, voucherID, voucherNo, CurrentDate.Text, "0", "0", DateTime.Now.ToString() }));
            }

        }
        IsHasAlarm.Value = "0";
        //--
        CommClass.UpdateDataSet(AllVoucher);
        ViewState["AllVoucher"] = AllVoucher;
        string FASubjectNo = SysConfigs.FixedAssetSubject;
        string AutoCreateAssetCard = SysConfigs.AutoCreateAssetCard;
        //保存原始凭证
        for (int i = 0; i < Entry.Tables[0].Rows.Count; i++)
        {
            string EntryDataT = "EntryData" + i.ToString("000000");
            if (Session[EntryDataT] != null)
            {
                DataSet EntryData = (DataSet)Session[EntryDataT];
                if (Entry.Tables[0].Rows[i].RowState != DataRowState.Deleted)
                {
                    for (int m = 0; m < EntryData.Tables[0].Rows.Count; m++)
                    {
                        if (EntryData.Tables[0].Rows[m].RowState != DataRowState.Deleted)
                        {
                            //关联分录编号
                            EntryData.Tables[0].Rows[m]["entryid"] = Entry.Tables[0].Rows[i]["id"].ToString();
                            //创建资产卡片
                            string ClassID = Entry.Tables[0].Rows[i]["subjectno"].ToString();
                            if (AutoCreateAssetCard == "1" && isNewVoucher && ClassID.StartsWith(FASubjectNo))
                            {
                                CreateFixedAssetCard(Entry.Tables[0].Rows[i]["subjectno"].ToString(), EntryData.Tables[0].Rows[m]);
                            }
                        }
                    }
                }
                CommClass.UpdateDataSet(EntryData);
                Session.Remove(EntryDataT);
            }
        }
        //创建新凭证
        CreateVoucher_Click(CreateVoucher, new EventArgs());
        IsCommit.Value = "0";
        ModiFlag.Value = "0";
        if (!isNewVoucher)
        {
            RecordRowIndex.Value = _RecordRowIndex.ToString();
        }
        InitWebControl();
        PageClass.ShowAlertMsg(this.Page, string.Concat("凭证[编号：", voucherNo, "]保存成功！"));
        //写入操作日志
        if (isNewVoucher)
        {
            CommClass.WriteCTL_Log("100006", "录入凭证：" + voucherNo + "，凭证日期：" + CurrentDate.Text);
        }
        else
        {
            CommClass.WriteCTL_Log("100006", "编辑凭证：" + voucherNo + "，凭证日期：" + CurrentDate.Text);
        }
    }
    /// <summary>
    /// 创建资产卡片
    /// </summary>
    /// <param name="ClassID"></param>
    /// <param name="entryDataRow"></param>
    private void CreateFixedAssetCard(string ClassID, DataRow entryDataRow)
    {
        //资产类别参数
        string CName = string.Empty;
        string UseLife = string.Empty;
        string SVRate = string.Empty;
        string MUnit = string.Empty;
        string DeprMethod = string.Empty;
        string LinkSubject = string.Empty;
        string DeprSubject = string.Empty;
        DateTime accountDate = MainClass.GetAccountDate();
        string AccountDate = accountDate.ToString("yyyy-MM-dd");
        DataRow assetPara = CommClass.GetDataRow("select CName,UseLife,SVRate,MUnit,DeprMethod,LinkSubject,DeprSubject from cw_assetclass where id='" + ClassID + "'");
        if (assetPara == null)
        {
            CName = CommClass.GetFieldFromNo(ClassID, "SubjectName");
            UseLife = "0.0";
            SVRate = "0";
            DeprMethod = "0";
            LinkSubject = string.Format("{0}.{1}", ClassID, CName);
            DeprSubject = SysConfigs.MonthDeprSubject;
        }
        else
        {
            CName = assetPara["CName"].ToString();
            UseLife = assetPara["UseLife"].ToString();
            SVRate = assetPara["SVRate"].ToString();
            MUnit = assetPara["MUnit"].ToString();
            DeprMethod = assetPara["DeprMethod"].ToString();
            LinkSubject = assetPara["LinkSubject"].ToString();
            DeprSubject = assetPara["DeprSubject"].ToString();
        }
        decimal oldPrice = TypeParse.StrToDecimal(entryDataRow["Price"].ToString(), 0) * TypeParse.StrToDecimal(entryDataRow["Amount"].ToString(), 0);
        decimal JingCZ = oldPrice * TypeParse.StrToDecimal(SVRate, 0);
        string OldPrice = oldPrice.ToString();
        //创建资产卡片
        Dictionary<string, string> sql = new Dictionary<string, string>();
        sql.Add("ID", CommClass.GetRecordID("CW_AssetID"));
        sql.Add("CardID", CommClass.GetRecordID("CW_AssetCard"));
        sql.Add("AssetNo", CommClass.GetRecordID("CW_AssetNo"));
        sql.Add("AssetName", CName);
        sql.Add("ClassID", ClassID);
        sql.Add("CName", CName);
        sql.Add("AssetModel", "");
        sql.Add("DeptName", string.Format("{0}.{1}", UserInfo.AccountID, UserInfo.AccountName));
        sql.Add("AddType", "");
        sql.Add("Depositary", "");
        sql.Add("UseState", "101");
        sql.Add("UseLife", UseLife);
        sql.Add("DeprMethod", "0");
        sql.Add("SUseDate", AccountDate);
        sql.Add("UsedMonths", "0");
        sql.Add("CurrencyType", "人民币");
        sql.Add("OldPrice", OldPrice);
        sql.Add("OldPrice0", OldPrice);
        sql.Add("JingCZL", SVRate);
        sql.Add("JingCZ", JingCZ.ToString());
        sql.Add("ZheJiu", "0");
        sql.Add("ZheJiu0", "0");
        sql.Add("MonthZJL", "0");
        sql.Add("MonthZJE", "0");
        sql.Add("NewPrice", OldPrice);
        sql.Add("DeprSubject", DeprSubject);
        sql.Add("AssetItem", "");
        sql.Add("AUnit", MUnit);
        sql.Add("AAmount", entryDataRow["Amount"].ToString());
        sql.Add("HasAmount", "0");
        sql.Add("APrice", entryDataRow["Price"].ToString());
        sql.Add("APicture", "");
        sql.Add("AssetAdmin", "");
        sql.Add("DeprState", "0");
        sql.Add("AssetState", "0");
        sql.Add("CVoucher", "0");
        sql.Add("BookTime", DateTime.Now.ToString("yyyy-MM-dd"));
        sql.Add("BookDate", AccountDate);
        sql.Add("BookMan", Session["RealName"].ToString());
        CommClass.ExecuteSQL("cw_assetcard", sql);
    }
    /// <summary>
    /// 作废凭证
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CancelVouncher_Click(object sender, EventArgs e)
    {
        int _RecordRowIndex = int.Parse(RecordRowIndex.Value);
        if (AllVoucher.Tables[0].Rows[_RecordRowIndex].RowState == DataRowState.Added)
        {
            ExeScript.Text = "<script>alert('当前凭证为新建尚未保存的凭证，不需作废！');</script>";
        }
        else
        {
            //写入操作日志
            CommClass.WriteCTL_Log("100006", string.Format("作废凭证：{0}，凭证日期：{1}",
                AllVoucher.Tables[0].Rows[_RecordRowIndex]["voucherno"].ToString(),
                AllVoucher.Tables[0].Rows[_RecordRowIndex]["voucherdate"].ToString()));
            //--
            if (AllVoucher.Tables[0].Rows[_RecordRowIndex]["DelFlag"].ToString() == "0")
            {
                AllVoucher.Tables[0].Rows[_RecordRowIndex]["DelFlag"] = "1";
            }
            else
            {
                AllVoucher.Tables[0].Rows[_RecordRowIndex]["DelFlag"] = "0";
            }
            //删除最后一张新增的空凭证
            if (_RecordRowIndex != AllVoucher.Tables[0].Rows.Count - 1)
            {
                AllVoucher.Tables[0].Rows[AllVoucher.Tables[0].Rows.Count - 1].Delete();
            }
            CommClass.UpdateDataSet(AllVoucher);
            ViewState["AllVoucher"] = AllVoucher;
            ModiFlag.Value = "1";
            CreateVoucher_Click(CreateVoucher, new EventArgs());
            ModiFlag.Value = "0";
            RecordRowIndex.Value = _RecordRowIndex.ToString();
        }
        InitWebControl();
    }
    /// <summary>
    /// 删除凭证
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DelVouncher_Click(object sender, EventArgs e)
    {
        int _RecordRowIndex = int.Parse(RecordRowIndex.Value);
        if (AllVoucher.Tables[0].Rows[_RecordRowIndex].RowState == DataRowState.Added)
        {
            ExeScript.Text = "<script>alert('当前凭证为新建尚未保存的凭证，不需删除！');</script>";
            InitWebControl();
        }
        else
        {
            //写入操作日志
            CommClass.WriteCTL_Log("100006", string.Format("删除凭证：{0}，凭证日期：{1}",
                AllVoucher.Tables[0].Rows[_RecordRowIndex]["voucherno"].ToString(),
                AllVoucher.Tables[0].Rows[_RecordRowIndex]["voucherdate"].ToString()));
            //--
            string voucherID = AllVoucher.Tables[0].Rows[_RecordRowIndex]["id"].ToString();
            //删除限额报警
            MainClass.ExecuteSQL(string.Format("delete from cw_balancealarm where AccountID='{0}' and VoucherID='{1}'", UserInfo.AccountID, voucherID));
            //删除凭证分录
            DataTable ventry = CommClass.GetDataTable(string.Format("select id from cw_entry where voucherid='{0}'", voucherID));
            foreach (DataRow row in ventry.Rows)
            {
                string entryID = row["id"].ToString();
                CommClass.ExecuteSQL(string.Format("delete from cw_entry where id='{0}'", entryID));
                CommClass.ExecuteSQL(string.Format("delete from cw_entrydata where entryid='{0}'", entryID));
            }
            //删除最后一张新增的空凭证
            AllVoucher.Tables[0].Rows[AllVoucher.Tables[0].Rows.Count - 1].Delete();
            //--
            AllVoucher.Tables[0].Rows[_RecordRowIndex].Delete();
            CommClass.UpdateDataSet(AllVoucher);
            ViewState["AllVoucher"] = AllVoucher;
            ModiFlag.Value = "0";
            CreateVoucher_Click(CreateVoucher, new EventArgs());
        }
    }
    /// <summary>
    /// 凭证号前提
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void UVoucherNo_Click(object sender, EventArgs e)
    {
        int _RecordRowIndex = int.Parse(RecordRowIndex.Value);
        if (_RecordRowIndex != AllVoucher.Tables[0].Rows.Count - 1)
        {
            AllVoucher.Tables[0].Rows[AllVoucher.Tables[0].Rows.Count - 1].Delete();
            //凭证编号前提
            for (int i = _RecordRowIndex; i < AllVoucher.Tables[0].Rows.Count; i++)
            {
                if (AllVoucher.Tables[0].Rows[i]["IsHasAlarm"].ToString() == "1")
                {
                    break;
                }
                else
                {
                    decimal voucherNo = decimal.Parse(AllVoucher.Tables[0].Rows[i]["VoucherNo"].ToString()) - 1;
                    AllVoucher.Tables[0].Rows[i]["VoucherNo"] = voucherNo.ToString();
                }
            }
            CommClass.UpdateDataSet(AllVoucher);
            ViewState["AllVoucher"] = AllVoucher;
            string tempModiFlag = ModiFlag.Value;
            ModiFlag.Value = "1";
            CreateVoucher_Click(CreateVoucher, new EventArgs());
            ModiFlag.Value = tempModiFlag;
        }
        RecordRowIndex.Value = _RecordRowIndex.ToString();
        InitWebControl();
    }
    /// <summary>
    /// 凭证号后推
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DVoucherNo_Click(object sender, EventArgs e)
    {
        int _RecordRowIndex = int.Parse(RecordRowIndex.Value);
        if (_RecordRowIndex != AllVoucher.Tables[0].Rows.Count - 1)
        {
            AllVoucher.Tables[0].Rows[AllVoucher.Tables[0].Rows.Count - 1].Delete();
            //凭证编号前提
            for (int i = _RecordRowIndex; i < AllVoucher.Tables[0].Rows.Count; i++)
            {
                if (AllVoucher.Tables[0].Rows[i]["IsHasAlarm"].ToString() == "1")
                {
                    break;
                }
                else
                {
                    decimal voucherNo = decimal.Parse(AllVoucher.Tables[0].Rows[i]["VoucherNo"].ToString()) + 1;
                    AllVoucher.Tables[0].Rows[i]["VoucherNo"] = voucherNo.ToString();
                }
            }
            CommClass.UpdateDataSet(AllVoucher);
            ViewState["AllVoucher"] = AllVoucher;
            string tempModiFlag = ModiFlag.Value;
            ModiFlag.Value = "1";
            CreateVoucher_Click(CreateVoucher, new EventArgs());
            ModiFlag.Value = tempModiFlag;
        }
        RecordRowIndex.Value = _RecordRowIndex.ToString();
        InitWebControl();
    }
}
