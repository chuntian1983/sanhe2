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

public partial class SysManage_SetLimiteFee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            Button1.Attributes["onclick"] = "return _onsubmit();";
            Button2.Attributes["onclick"] = "return confirm('您确定需要清除管理费用设置吗？');";
            ManageSubject.Value = SysConfigs.ManageSubject;
            DataTable accounts = MainClass.GetDataTable("select id,AccountName from cw_account where UnitID='" + Session["UnitID"].ToString() + "'");
            foreach (DataRow account in accounts.Rows)
            {
                AccountList.Items.Add(new ListItem(account["AccountName"].ToString(), account["id"].ToString()));
            }
            if (AccountList.Items.Count == 0)
            {
                PageClass.UrlRedirect("您下属无账套，暂时不可执行该操作！", 3);
            }
            else
            {
                InitWebControl();
            }
        }
    }
    /// <summary>
    /// 数据绑定函数
    /// </summary>
    private void InitWebControl()
    {
        UserInfo.AccountID = AccountList.SelectedValue;
        AllSubjectFee.Value = MainClass.GetSysPara("LimitFee" + AccountList.SelectedValue);
        AllSubjectFee.Value = Regex.Replace(AllSubjectFee.Value, string.Concat("\\[(?!", ManageSubject.Value, ")[^\\]]*?\\]"), "");
        if (AllSubjectFee.Value == "NoDataItem")
        {
            AllSubjectFee.Value = "";
        }
        DataTable dt = CommClass.GetDataTable(string.Concat("select * from cw_subject where subjectno like '", SysConfigs.ManageSubject
            , "%' order by subjectno"));
        if (dt.Rows.Count == 0)
        {
            PageClass.BindNoRecords(GridView1, dt);
        }
        else
        {
            GridView1.DataSource = dt.DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[2].Text == "1")
            {
                e.Row.Cells[2].Text = "是";
            }
            else
            {
                e.Row.Cells[2].Text = "否";
            }
            Match FeeV = Regex.Match(AllSubjectFee.Value, string.Concat("\\[", e.Row.Cells[0].Text, ":\\d*\\]"));
            if (FeeV.Success)
            {
                e.Row.Cells[3].Text = FeeV.Value.Replace(string.Concat("[", e.Row.Cells[0].Text, ":"), "").Replace("]", "");
            }
            else
            {
                e.Row.Cells[3].Text = "-";
            }
            e.Row.Cells[3].Attributes.Add("onclick", string.Concat("SetFee('", e.Row.Cells[3].ClientID, "','", e.Row.Cells[0].Text, "');"));
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        MainClass.SetSysPara("LimitFee" + AccountList.SelectedValue, AllSubjectFee.Value);
        ExeScript.Text = "<script language=javascript>alert('费用控制额设置成功！');</script>";
        InitWebControl();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        MainClass.SetSysPara("LimitFee" + AccountList.SelectedValue, "");
        ExeScript.Text = "<script language=javascript>alert('费用控制额清除成功！');</script>";
        InitWebControl();
    }
    protected void AccountList_SelectedIndexChanged(object sender, EventArgs e)
    {
        InitWebControl();
    }
}
