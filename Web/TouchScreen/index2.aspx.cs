using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yuxi
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LinkButton2.Text = ValidateClass.GetDataname("000000");
                //DataTable dts = ValidateClass.GetData("000000");
                DataTable dt = ValidateClass.GetData("000000");
                this.DataList1.DataSource = dt;
                this.DataList1.DataBind();
            }
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName=="view")
            {
                string id = e.CommandArgument.ToString();
                //DataTable dt = ValidateClass.GetData(id);
                //this.DataList1.DataSource = dt;
                //this.DataList1.DataBind();
                Response.Redirect("zhen.aspx?kid="+id+"");
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}