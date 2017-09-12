using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SanZi.Web.view.GongKai
{
    public partial class lblist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strsql = "select * from cwgk_lbb";
                DataSet ds = MainClass.GetDataSet(strsql);// CommClass.GetDataSet(strsql);
                if (ds.Tables[0].Rows.Count>0)
                {
                    this.DataList1.DataSource = ds;
                    this.DataList1.DataBind();
                }
            }
        }
    }
}
