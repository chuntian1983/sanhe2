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
using LTP.Common;
using System.IO;

public partial class AccountManage_Appendices : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            MainClass.InitAccountYear(SelYear);
            DateTime AccountDate = MainClass.GetAccountDate();
            YearMonth.Value = AccountDate.ToString("yyyy-MM-dd");
            SelYear.Text = AccountDate.Year.ToString();
            SelMonth.Text = AccountDate.Month.ToString("00");
            InitWebControl();
        }
    }
    private void InitWebControl()
    {
        string sql = "select ParaName,ParaType,ParaValue,DefValue from cw_syspara where ParaName like 'Appendix%'";
        if (ShowType.SelectedValue != "X")
        {
            sql = string.Concat(sql, " and ParaType='", ShowType.SelectedValue, "'");
        }
        sql = string.Concat(sql, " and DefPara1 like '", SelYear.SelectedValue, "-", SelMonth.SelectedValue, "%' order by ParaName");
        DataTable appendix = CommClass.GetDataTable(sql);
        AppendixList.DataSource = appendix.DefaultView;
        AppendixList.DataKeyField = "ParaName";
        AppendixList.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
    protected void btnControl_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        switch (btn.ID)
        {
            case "btnHide":
                if (btn.CommandArgument == "0")
                {
                    CommClass.ExecuteSQL(string.Concat("update cw_syspara set ParaType='1' where ParaName='", btn.CommandName, "'"));
                }
                else
                {
                    CommClass.ExecuteSQL(string.Concat("update cw_syspara set ParaType='0' where ParaName='", btn.CommandName, "'"));
                }
                break;
            case "btnDelete":
                CommClass.ExecuteSQL(string.Concat("delete from cw_syspara where ParaName='", btn.CommandName, "'"));
                System.IO.File.Delete(Server.MapPath(btn.CommandArgument.Replace("_thum", "")));
                System.IO.File.Delete(Server.MapPath(btn.CommandArgument));
                break;
        }
        InitWebControl();
    }
    protected void AppendixList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        LinkButton btn = (LinkButton)e.Item.FindControl("btnHide");
        if (btn.CommandArgument == "0")
        {
            btn.Text = "显示";
        }
        else
        {
            btn.Text = "隐藏";
        }
    }
    protected void ShowType_SelectedIndexChanged(object sender, EventArgs e)
    {
        InitWebControl();
    }

    protected void DoPostBack_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
}
