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

public partial class SysManage_SetBeiYongJin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = MainClass.GetDataTable("select id,accountname,define3 from cw_account where unitid='" + UserInfo.UnitID + "'");
            int acount = dt.Rows.Count;
            if (acount == 0)
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
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btnSaveCheckFill_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            TextBox define3 = (TextBox)GridView1.Rows[i].FindControl("Define3");
            MainClass.ExecuteSQL(string.Concat("update cw_account set define3='" + define3.Text + "' where id='", GridView1.DataKeys[i].Value.ToString(), "'"));
        }
    }
}
