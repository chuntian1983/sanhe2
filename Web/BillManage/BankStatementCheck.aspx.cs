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

public partial class BillManage_BankStatementCheck : System.Web.UI.Page
{
    private string[] arrSettleType = { "&nbsp;", "现金支票", "转账支票", "电汇凭证", "贷记凭证", "商业承兑汇票", "银行承兑汇票" };

    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            UtilsPage.SetTextBoxAutoValue(SpanDays, "0");
            UtilsPage.SetTextBoxCalendar(SDate, "");
            UtilsPage.SetTextBoxCalendar(EDate, "");
            UtilsPage.SetTextBoxCalendar(VSDate, "yyyy年MM月dd日");
            UtilsPage.SetTextBoxCalendar(VEDate, "yyyy年MM月dd日");
            VSDate.Text = "";
            SettleSubject.Attributes["onclick"] = "selSubject('SettleSubject','&filter=102')";
            UtilsPage.SetTextBoxReadOnly(SettleSubject);
            CheckBankStatement0.Attributes["onclick"] = "return confirm('您确定把已勾选的项目设定为已对账状态吗？')";
            CheckBankStatement1.Attributes["onclick"] = "return confirm('您确定把所有项目设定为已对账状态吗？')";
            InitWebControl();
        }
    }
    /// <summary>
    /// 数据绑定函数
    /// </summary>
    protected void InitWebControl()
    {
        string QueryString = "$(CheckState='0' or CheckState is null)";
        if (SDate.Text.Length > 0)
        {
            QueryString += "$SettleDate>='" + SDate.Text + "'";
        }
        if (EDate.Text.Length > 0)
        {
            QueryString += "$SettleDate<='" + EDate.Text + "'";
        }
        if (SettleNo.Text.Length > 0)
        {
            QueryString += "$(SettleNo like '%" + SettleNo.Text + "%')";
        }
        if (SettleType.SelectedValue != "-")
        {
            QueryString += "$SettleType='" + SettleType.SelectedValue + "'";
        }
        if (VSDate.Text.Length > 0)
        {
            QueryString += "$VoucherDate>='" + VSDate.Text + "'";
        }
        if (VEDate.Text.Length > 0)
        {
            QueryString += "$VoucherDate<='" + VEDate.Text + "'";
        }
        //--
        string qs = "$(SubjectNo like '102%')" + QueryString;
        if (SettleSubject.Text.Length > 0)
        {
            string sno = SettleSubject.Text.Substring(0, SettleSubject.Text.IndexOf("."));
            qs += "$(SubjectNo like '" + sno + "%')";
            QueryString += "$(SettleSubject like '" + sno + "%')";
        }
        if (QueryString.Length > 0)
        {
            qs = " where " + qs.Substring(1, qs.Length - 1).Replace("$", " and ");
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        DataSet ds = CommClass.GetDataSet("select * from cw_billsettle " + QueryString + " order by VoucherDate,id");
        if (ds.Tables[0].Rows.Count == 0)
        {
            PageClass.BindNoRecords(GridView1, ds);
        }
        else
        {
            GridView1.DataSource = ds.Tables[0].DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
        }
        ds = CommClass.GetDataSet("select * from cw_voucherentry " + qs + " order by VoucherDate,id");
        if (ds.Tables[0].Rows.Count == 0)
        {
            PageClass.BindNoRecords(GridView2, ds);
        }
        else
        {
            GridView2.DataSource = ds.Tables[0].DefaultView;
            GridView2.DataKeyNames = new string[] { "id" };
            GridView2.DataBind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Text = arrSettleType[TypeParse.StrToInt(e.Row.Cells[4].Text, 0)];
            string doMoney = e.Row.Cells[6].Text;
            if (doMoney.StartsWith("-"))
            {
                e.Row.Cells[6].Text = doMoney.Substring(1);
                e.Row.Cells[5].Text = "贷方";
            }
            else
            {
                e.Row.Cells[6].Text = doMoney;
                e.Row.Cells[5].Text = "借方";
            }
            e.Row.Attributes["onmouseover"] = "bgColor=this.style.backgroundColor;this.style.backgroundColor='#dad5cc';fontColor=this.style.color;this.style.color='red';";
            e.Row.Attributes["onmouseout"] = "this.style.backgroundColor=bgColor;this.style.color=fontColor;";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
    protected void CheckBankStatement_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        bool isAll = (btn.ID == "CheckBankStatement1");
        string checkDate = DateTime.Now.ToString("yyyy-MM-dd");
        foreach (GridViewRow grow in GridView1.Rows)
        {
            HiddenField bid = (HiddenField)grow.FindControl("hidBillID");
            if (isAll)
            {
                CommClass.ExecuteSQL(string.Concat("update cw_billsettle set CheckState='1',CheckDate='", checkDate, "' where id='", bid.Value, "'"));
            }
            else
            {
                CheckBox chk = (CheckBox)grow.FindControl("SelChecked");
                if (chk.Checked)
                {
                    CommClass.ExecuteSQL(string.Concat("update cw_billsettle set CheckState='1',CheckDate='", checkDate, "' where id='", bid.Value, "'"));
                }
            }
        }
        foreach (GridViewRow grow in GridView2.Rows)
        {
            HiddenField bid = (HiddenField)grow.FindControl("hidBillID");
            if (isAll)
            {
                CommClass.ExecuteSQL(string.Concat("update cw_entry set CheckState='1',CheckDate='", checkDate, "' where id='", bid.Value, "'"));
            }
            else
            {
                CheckBox chk = (CheckBox)grow.FindControl("SelChecked");
                if (chk.Checked)
                {
                    CommClass.ExecuteSQL(string.Concat("update cw_entry set CheckState='1',CheckDate='", checkDate, "' where id='", bid.Value, "'"));
                }
            }
        }
        InitWebControl();
    }
}
