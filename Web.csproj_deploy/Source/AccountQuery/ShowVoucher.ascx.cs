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

public partial class AccountQuery_ShowVoucher : System.Web.UI.UserControl
{
    private string _VoucherID;
    private string _AccountID;
    private int _PageSize = 0;
    private int _PageIndex = 0;
    private int _RefreshFlag = 0;
    private bool _ShowTipImg = true;
    private bool _ShowReverseState = true;
    private Button _button;

    private bool taoda = false;
    public bool taoDa
    {
        set { taoda = value; }
        get { return taoda; }
    }

    public string VoucherID
    {
        set { _VoucherID = value; }
        get { return _VoucherID; }
    }

    public string AccountID
    {
        set { _AccountID = value; }
        get { return _AccountID; }
    }

    public int PageSize
    {
        set { _PageSize = value; }
        get { return _PageSize; }
    }

    public int PageIndex
    {
        set { _PageIndex = value; }
        get { return _PageIndex; }
    }

    public int RefreshFlag
    {
        set { _RefreshFlag = value; }
        get { return _RefreshFlag; }
    }

    public bool ShowTipImg
    {
        set { _ShowTipImg = value; }
        get { return _ShowTipImg; }
    }

    public bool ShowReverseState
    {
        set { _ShowReverseState = value; }
        get { return _ShowReverseState; }
    }

    public Button button
    {
        set { _button = value; }
        get { return _button; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        AccountName.Text = UserInfo.AccountName;
        if (AccountID != null && AccountID.Length > 0)
        {
            UserInfo.AccountID = AccountID;
        }
        if (RefreshFlag == 0)
        {
            InitVoucherInfo();
        }
    }

    public void @InitVoucherInfo()
    {
        //初始化凭证信息
        DataSet ds = CommClass.GetDataSet("select * from cw_voucher where id='" + VoucherID + "'");
        if (ds.Tables[0].Rows.Count == 0) { return; }
        //冲红凭证红色显示/////////////////////////////////////
        bool isReverseVoucher = (ds.Tables[0].Rows[0]["ReverseVoucherID"].ToString().Length > 0);
        if (button != null)
        {
            if (isReverseVoucher)
            {
                button.Text = "取消冲红状态";
                button.CommandArgument = "CancelReverse";
            }
            else
            {
                if (CommClass.CheckExist("cw_voucher", "ReverseVoucherID='" + VoucherID + "'"))
                {
                    button.Enabled = false;
                    //button.Text = "删除冲红凭证";
                    //button.CommandArgument = "DelReverse";
                }
            }
        }
        if (_ShowReverseState && isReverseVoucher)
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
        VoucherDate.Text = ds.Tables[0].Rows[0]["voucherdate"].ToString();
        VoucherNo.Text = ds.Tables[0].Rows[0]["voucherno"].ToString();
        Director.Text = ds.Tables[0].Rows[0]["Director"].ToString();
        Accountant.Text = ds.Tables[0].Rows[0]["Accountant"].ToString();
        Assessor.Text = ds.Tables[0].Rows[0]["Assessor"].ToString();
        DoBill.Text = ds.Tables[0].Rows[0]["DoBill"].ToString();
        //超限预警标示输出
        AlarmTip.Visible = (_ShowTipImg && ds.Tables[0].Rows[0]["IsHasAlarm"].ToString() == "1");
        AlarmTip.Alt = "该凭证必须由监督管理部门审核。";
        //审核记账标示输出
        string commStyle = "background-position: center bottom;vertical-align: top; background-repeat: no-repeat; height: 103px;";
        if (taoda)
        {
            VoucherHead.Attributes["style"] = commStyle;
            if (ds.Tables[0].Rows[0]["IsAuditing"].ToString() == "0")
            {
                pzheadbg.ImageUrl = "../Images/pzhead_2.jpg";
            }
            else
            {
                if (ds.Tables[0].Rows[0]["IsRecord"].ToString() == "0")
                {
                    pzheadbg.ImageUrl = "../Images/pzhead0_2.jpg";
                }
                else
                {
                    pzheadbg.ImageUrl = "../Images/pzhead1_2.jpg";
                }
            }
            PageClass.ExcuteScript(this.Page, string.Concat("setposition('", VoucherHead.ClientID, "','", pzheadbg.ClientID, "')"));
        }
        else
        {
            pzheadbg.Visible = false;
            if (ds.Tables[0].Rows[0]["IsAuditing"].ToString() == "0")
            {
                VoucherHead.Attributes["style"] = commStyle + "background-image: url(../Images/pzhead.jpg)";
            }
            else
            {
                if (ds.Tables[0].Rows[0]["IsRecord"].ToString() == "0")
                {
                    VoucherHead.Attributes["style"] = commStyle + "background-image: url(../Images/pzhead0.jpg)";
                }
                else
                {
                    VoucherHead.Attributes["style"] = commStyle + "background-image: url(../Images/pzhead1.jpg)";
                }
            }
        }
        DataSet Entry = CommClass.GetDataSet("select * from cw_entry where voucherid='" + VoucherID + "' order by id asc");
        if (Entry.Tables[0].Rows.Count == 0) { return; }
        //凭证分录输出设置
        if (PageSize == 0)
        {
            PageIndex = 0;
            PageSize = Entry.Tables[0].Rows.Count < 7 ? 7 : Entry.Tables[0].Rows.Count;
        }
        int PageCount = Entry.Tables[0].Rows.Count / PageSize;
        if (Entry.Tables[0].Rows.Count % PageSize != 0)
        {
            PageCount++;
        }
        //创建数据表
        DataTable dt = new DataTable();
        dt.Columns.Add("id");
        ((BoundField)GridView1.Columns[0]).DataField = "id";
        for (int k = 0; k < PageSize; k++)
        {
            dt.Rows.Add(new string[] { "" });
        }
        if (PageIndex == PageCount - 1)
        {
            dt.Rows.Add(new string[] { "" });
        }
        //绑定表格
        GridView1.DataSource = dt.DefaultView;
        GridView1.DataBind();
        //初始化表值
        int HasCounts = PageSize * PageIndex;
        int LimitCounts = Entry.Tables[0].Rows.Count - HasCounts;
        for (int m = 0; m < PageSize && m < LimitCounts; m++)
        {
            GridViewRow GRow = GridView1.Rows[m];
            DataRow eRow = Entry.Tables[0].Rows[m + HasCounts];
            GRow.Cells[0].Text = eRow["vsummary"].ToString();
            string SubjectNo = eRow["subjectno"].ToString();
            string SubjectName = eRow["subjectname"].ToString();
            if (SubjectName.Length == 0)
            {
                if (!string.IsNullOrEmpty(SubjectNo))
                {
                    GRow.Cells[1].Text = CommClass.GetFieldFromNo(SubjectNo.Substring(0, 3), "SubjectName");
                    if (SubjectNo.Length > 3)
                    {
                        GRow.Cells[2].Text = CommClass.GetDetailSubject(SubjectNo);
                    }
                }
            }
            else
            {
                int sindex = SubjectName.IndexOf('/');
                if (sindex == -1)
                {
                    GRow.Cells[1].Text = SubjectName;
                }
                else
                {
                    GRow.Cells[1].Text = SubjectName.Substring(0, sindex);
                    if (sindex < SubjectName.Length - 1)
                    {
                        GRow.Cells[2].Text = SubjectName.Substring(sindex + 1);
                    }
                }
            }
            //////////////////////////////////////////////////////////////
            //输出凭证分录
            decimal sumMoney = TypeParse.StrToDecimal(eRow["summoney"].ToString(), 0);
            if (sumMoney > 0)
            {
                GRow.Cells[3].Text = sumMoney.ToString("#.00");
            }
            else
            {
                sumMoney = Math.Abs(sumMoney);
                GRow.Cells[4].Text = sumMoney.ToString("#.00");
            }
            GRow.Cells[2].Attributes["title"] = string.Format("科目代码：{0}", SubjectNo);
            //////////////////////////////////////////////////////////////
            //输出显示原始凭证事件
            if (CommClass.GetFieldFromNo(SubjectNo, "AccountType") == "1")
            {
                GRow.Attributes["ondblclick"] = "ShowEntryData('" + eRow["id"].ToString() + "','" + SubjectNo + "','Q" + m.ToString() + "','" + eRow["summoney"].ToString() + "');";
                GRow.Attributes["title"] = "提示：双击行可以查看原始凭证。";
            }
            //////////////////////////////////////////////////////////////
        }
        //输出合计行
        if (PageIndex == PageCount - 1)
        {
            decimal Lead = 0;
            decimal Onloan = 0;
            foreach (DataRow row in Entry.Tables[0].Rows)
            {
                string _SumMoney = row["summoney"].ToString();
                if (_SumMoney.StartsWith("-"))
                {
                    Onloan -= Convert.ToDecimal(_SumMoney);
                }
                else
                {
                    Lead += Convert.ToDecimal(_SumMoney);
                }
            }
            GridViewRow CRow = GridView1.Rows[GridView1.Rows.Count - 1];
            string AddonsCount = ds.Tables[0].Rows[0]["AddonsCount"].ToString();
            CRow.Cells[0].Attributes["style"] = "cursor:hand";
            CRow.Cells[0].Attributes["onclick"] = "ShowAddons('" + VoucherID + "');";
            CRow.Cells[0].Text = "&nbsp;&nbsp;<font color=red>附单张数：&nbsp;&nbsp;" + AddonsCount + "&nbsp;&nbsp;&nbsp;&nbsp;张</font>";
            CRow.Cells[1].Text = "<center>" + PageClass.GetNumber2CN(Lead) + "</center>";
            CRow.Cells[1].Width = (Unit)(CRow.Cells[1].Width.Value + CRow.Cells[2].Width.Value);
            CRow.Cells[1].ColumnSpan = 2;
            CRow.Cells[2].Visible = false;
            CRow.Cells[3].Text = Lead.ToString("#.00");
            CRow.Cells[4].Text = Onloan.ToString("#.00");
        }
        //输出分页页次
        if (PageCount > 1)
        {
            ShowPage.Text = "[页次：" + (PageIndex + 1).ToString() + "  /  " + PageCount.ToString() + "]";
        }
        else
        {
            ShowPage.Text = "";
        }
    }
}
