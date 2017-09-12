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

public partial class Contract_DefinePay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            UtilsPage.SetTextBoxAutoValue(PayMoney, "0");
            SPay.Attributes.Add("readonly", "readonly");
            SPay.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].SPay,'yyyy-mm-dd')");
            EPay.Attributes.Add("readonly", "readonly");
            EPay.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].EPay,'yyyy-mm-dd')");
            InitWebControl();
        }
    }
    /// <summary>
    /// 数据绑定函数
    /// </summary>
    private void InitWebControl()
    {
        NextPayDate.Value = "";
        NextPayMoney.Value = "";
        DataTable dt = CommClass.GetDataTable("select * from CW_ResPayPeriod where cardid='" + Request.QueryString["id"] + "' order by StartPay");
        if (dt.Rows.Count == 0)
        {
            PageClass.BindNoRecords(GridView1, dt);
        }
        else
        {
            GridView1.DataSource = dt.DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
            Label lb = (Label)GridView1.BottomPagerRow.Cells[0].FindControl("ShowPageInfo");
            lb.Text = "记录数：" + dt.Rows.Count.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lb.Text += "总页数：" + (GridView1.PageIndex + 1) + "/" + GridView1.PageCount + "页";
            DropDownList ddl = (DropDownList)GridView1.BottomPagerRow.Cells[0].FindControl("JumpPage");
            ddl.Items.Clear();
            for (int i = 0; i < GridView1.PageCount; i++)
            {
                ddl.Items.Add(new ListItem("第" + (i + 1).ToString() + "页", i.ToString()));
            }
            ddl.SelectedIndex = GridView1.PageIndex;
            //屏蔽功能按钮
            Button1.Enabled = (Request.QueryString["lstate"] != "1");
            if (Request.QueryString["lstate"] != "0")
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridView1.Rows[i].Cells[7].Enabled = false;
                    GridView1.Rows[i].Cells[8].Enabled = false;
                }
            }
            //获取下次收款金额和时间
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["PayState"].ToString() == "0")
                {
                    NextPayDate.Value = dt.Rows[i]["StartPay"].ToString();
                    NextPayMoney.Value = dt.Rows[i]["PayMoney"].ToString();
                    break;
                }
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
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState.ToString().IndexOf("Edit") == -1)
        {
            if (e.Row.Cells[4].Text == "0")
            {
                e.Row.Cells[4].Text = "未支付";
            }
            else
            {
                e.Row.Cells[4].Text = "已支付";
                e.Row.Cells[7].Enabled = false;
            }
            string id = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
            LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
            btnEdit.Attributes.Add("onclick", "return EditRow('" + id + "','" + e.Row.Cells[0].Text + "','" + e.Row.Cells[1].Text + "','"
                + e.Row.Cells[2].Text + "','" + e.Row.Cells[3].Text + "')");
            LinkButton btnPay = (LinkButton)e.Row.FindControl("btnPay");
            btnPay.Attributes["onclick"] = "return LeasePay('" + btnPay.CommandArgument + "','" + id + "');";
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            btnDelete.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除“" + e.Row.Cells[0].Text + "”吗？')");
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "btnDelete")
        {
            LinkButton btnDelete = (LinkButton)e.CommandSource;
            CommClass.ExecuteSQL("delete from CW_ResPayPeriod where id='" + btnDelete.CommandArgument + "'");
            EditID.Value = "";
            InitWebControl();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> ResPayPeriod = new Dictionary<string, string>();
        ResPayPeriod.Add("PeriodName", PeriodName.Text);
        ResPayPeriod.Add("PayMoney", PayMoney.Text);
        ResPayPeriod.Add("StartPay", SPay.Text);
        ResPayPeriod.Add("EndPay", EPay.Text);
        if (EditID.Value == "")
        {
            ResPayPeriod.Add("ID", CommClass.GetRecordID("CW_ResPayPeriod"));
            ResPayPeriod.Add("CardID", Request.QueryString["id"]);
            ResPayPeriod.Add("PayState", "0");
            ResPayPeriod.Add("DoState", "0");
            CommClass.ExecuteSQL("CW_ResPayPeriod", ResPayPeriod);
        }
        else
        {
            //恢复到期处理状态
            if (hidEPay.Value != EPay.Text)
            {
                ResPayPeriod.Add("DoState", "0");
            }
            CommClass.ExecuteSQL("CW_ResPayPeriod", ResPayPeriod, "id='" + EditID.Value + "'");
        }
        PeriodName.Text = "";
        PayMoney.Text = "";
        SPay.Text = "";
        EPay.Text = "";
        EditID.Value = "";
        InitWebControl();
    }
    protected void HidePostBack_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
}
