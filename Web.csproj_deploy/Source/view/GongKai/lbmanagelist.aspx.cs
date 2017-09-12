using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SanZi.Web.view.GongKai
{
    public partial class lbmanagelist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetBind();
            }
        }
        protected void GetBind()
        {
            string strsql = "select * from cwgk_lbb";
            DataSet ds = MainClass.GetDataSet(strsql);
            this.GridView1.DataSource = ds;
            this.GridView1.DataBind();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string gcid = e.CommandArgument.ToString();
            if (e.CommandName == "Del")
            {

                string strsql = "delete from cwgk_lbb where id='"+gcid+"'";
                MainClass.ExecuteSQL(strsql);
                GetBind();
            }
            
        }
    }
}
