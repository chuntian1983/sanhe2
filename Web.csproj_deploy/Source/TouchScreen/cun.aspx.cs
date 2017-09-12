using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yuxi
{
    public partial class cun : System.Web.UI.Page
    {
        string kid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["kid"] != null)
                {
                    kid = Request.QueryString["kid"];
                    string cid = Request.QueryString["cid"];

                    LinkButton3.Text = ValidateClass.GetDataname(kid);
                    LinkButton4.Text = ValidateClass.GetDataname(cid);
                    DataTable dt = ValidateClass.GetData(cid);
                    this.DataList1.DataSource = dt;
                    this.DataList1.DataBind();
                }
            }
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "view")
            {
                string id = e.CommandArgument.ToString();
                //DataTable dt = ValidateClass.GetData(id);
                //this.DataList1.DataSource = dt;
                //this.DataList1.DataBind();
                Response.Redirect("cunnew.aspx?qid=" + Request.QueryString["kid"] + "&zid=" + Request.QueryString["cid"] + "&kid=" + id + "");
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {

        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["kid"]!=null)
            {
                string kid = Request.QueryString["kid"];
                Response.Redirect("zhen.aspx?kid="+kid+"");
            }
        }
    }
}