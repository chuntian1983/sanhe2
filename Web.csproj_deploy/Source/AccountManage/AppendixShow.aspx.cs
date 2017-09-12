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
using Telerik.WebControls;
using System.Text;

public partial class AccountManage_AppendixShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            InitWebControl();
        }
    }
    private void InitWebControl()
    {
        string appendices = string.Empty;
        switch (Request.QueryString["atype"])
        {
            case "0":
            case "1":
            case "2":
                appendices = CommClass.GetTableValue("cw_flowlist", "Appendices" + Request.QueryString["atype"], "id='" + Request.QueryString["id"] + "'", "");
                break;
            default:
                appendices = CommClass.GetTableValue("cw_voucher", "addons", "id='" + Request.QueryString["id"] + "'", "");
                break;
        }
        string sql = string.Concat("select ParaName,ParaType,ParaValue,DefValue from cw_syspara where ParaName in ('", appendices.Replace("$", "','"), "') order by ParaName");
        DataTable appendix = CommClass.GetDataTable(sql);
        if (appendix.Rows.Count == 0)
        {
            PageClass.ExcuteScript(this.Page, "window.close();alert('很抱歉，没有查询到可显示附件！');");
        }
        else
        {
            AppendixList.DataSource = appendix.DefaultView;
            AppendixList.DataKeyField = "ParaName";
            AppendixList.DataBind();
            PageClass.ExcuteScript(this.Page, string.Concat("$('ImgShow').src='", appendix.Rows[0]["ParaValue"].ToString(), "'"));
        }
    }
    protected void AppendixList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        //--
    }
}
