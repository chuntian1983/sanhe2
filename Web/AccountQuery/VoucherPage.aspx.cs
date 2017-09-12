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
using System.Reflection;

public partial class AccountManage_VoucherPage : System.Web.UI.Page
{
    DataSet AllVoucher;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["s"]) == false)
        {
            UserInfo.CheckSession2();
        }
        if (!IsPostBack)
        {
            DateTime AccountDate = MainClass.GetAccountDate();
            if (string.IsNullOrEmpty(Request.QueryString["s"]) == false)
            {
                if (AccountDate.Year == 1900)
                {
                    Response.Clear();
                    Response.Write("<script>window.close();alert('当前账套尚未启用，不能执行该操作！');</script>");
                    Response.End();
                }
                else
                {
                    ExecuteScript.Text = "<script>$('Button4').disabled='disabled';</script>";
                }
            }
        }
        if (!PageClass.CheckVisitQuot("000014$100004")) { return; }
        if (!IsPostBack)
        {
            MainClass.InitAccountYear(QYear);
            DateTime AccountDate = MainClass.GetAccountDate();
            if (string.IsNullOrEmpty(Request.QueryString["s"]) == false)
            {
                if (AccountDate.Year == 1900)
                {
                    Response.Clear();
                    Response.Write("<script>window.close();alert('当前账套尚未启用，不能执行该操作！000');</script>");
                    Response.End();
                }
                else
                {
                    PageClass.ExcuteScript(this.Page, "$('Button4').disabled='disabled';");
                }
            }
            QSMonth.Attributes["onchange"] = "SelAMonth(this.value);";
            QSMonth.Text = AccountDate.Month.ToString("00");
            QEMonth.Text = AccountDate.Month.ToString("00");
            GetAllVoucher();
            //写入操作日志
            CommClass.WriteCTL_Log("100015", "凭证查询：凭证单张查询");
            //--
        }
        AllVoucher = (DataSet)Session["AllVoucher"];
        if (!IsPostBack)
        {
            InitWebControl();
        }
    }
    protected void GetAllVoucher()
    {
        string sYearMonth = QYear.SelectedValue + "年" + QSMonth.SelectedValue + "月01日";
        string eYearMonth = QYear.SelectedValue + "年" + QEMonth.SelectedValue + "月31日";
        string QueryString = "$voucherdate between '" + sYearMonth + "' and '" + eYearMonth + "'";
        if (VoucherNoS.Text != "")
        {
            if (VoucherNoE.Text.Length > 0)
            {
                QueryString += "$VoucherNo between '" + VoucherNoS.Text + "' and '" + VoucherNoE.Text + "'";
            }
            else
            {
                QueryString += "$VoucherNo='" + VoucherNoS.Text + "'";
            }
        }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        AllVoucher = CommClass.GetDataSet("select * from cw_voucher " + QueryString + " order by left(voucherdate,8),voucherno");
        Session["AllVoucher"] = AllVoucher;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //统计附单张数
        decimal Addons = 0;
        string TempVNo = string.Empty;
        foreach (DataRow row in AllVoucher.Tables[0].Rows)
        {
            if (row["AddonsCount"].ToString().Length > 0 && TempVNo != row["voucherno"].ToString())
            {
                Addons += decimal.Parse(row["AddonsCount"].ToString());
                TempVNo = row["voucherno"].ToString();
            }
        }
        TextBox1.Text = Addons.ToString();
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
    protected void InitWebControl()
    {
        if (AllVoucher.Tables[0].Rows.Count > 0)
        {
            //初始化凭证信息
            int _RecordRowIndex = int.Parse(RecordRowIndex.Value);
            if (_RecordRowIndex > AllVoucher.Tables[0].Rows.Count - 1 || _RecordRowIndex < 0)
            {
                _RecordRowIndex = 0;
                RecordRowIndex.Value = "0";
            }
            UserControl ctl = (UserControl)LoadControl("../AccountQuery/ShowVoucher.ascx");
            Type ctlType = ctl.GetType();
            PropertyInfo VoucherID = ctlType.GetProperty("VoucherID");
            VoucherID.SetValue(ctl, AllVoucher.Tables[0].Rows[_RecordRowIndex]["id"].ToString(), null);
            ShowPageContent.Controls.Add(ctl);
        }
        else
        {
            LiteralControl showNoData = new LiteralControl("<center><br><br>没有查询到凭证！</center>");
            ShowPageContent.Controls.Add(showNoData);
        }
    }
    protected void PreVoucher_Click(object sender, EventArgs e)
    {
        if (int.Parse(RecordRowIndex.Value) > 0)
        {
            RecordRowIndex.Value = (int.Parse(RecordRowIndex.Value) - 1).ToString();
        }
        InitWebControl();
    }
    protected void NextVoucher_Click(object sender, EventArgs e)
    {
        if (int.Parse(RecordRowIndex.Value) < AllVoucher.Tables[0].Rows.Count - 1)
        {
            RecordRowIndex.Value = (int.Parse(RecordRowIndex.Value) + 1).ToString();
        }
        InitWebControl();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        RecordRowIndex.Value = "0";
        InitWebControl();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        RecordRowIndex.Value = (AllVoucher.Tables[0].Rows.Count - 1).ToString();
        InitWebControl();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        GetAllVoucher();
        InitWebControl();
    }
}
