using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yuxi
{
    public partial class zhen : System.Web.UI.Page
    {
        private string kid = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LinkButton2.Text = ValidateClass.GetDataname("000000");
                bindData();
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
                if (Request.QueryString["kid"] != null)
                {
                    kid = Request.QueryString["kid"];
                    Response.Redirect("cunnew.aspx?qid=" + Request.QueryString["kid"] + "&zid=" + id + "&kid=" + id + "");
                }
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        void bindData()
        {
            if (Request.QueryString["kid"] != null)
            {
                kid = Request.QueryString["kid"];


                LinkButton3.Text = ValidateClass.GetDataname(kid);

                DataTable dts = ValidateClass.GetData(kid);
                
                // ps.DataSource = dt;
                AspNetPager1.RecordCount = dts.Rows.Count;
                PagedDataSource pds = new PagedDataSource();
                pds.DataSource = dts.DefaultView;
                pds.AllowPaging = true;
                pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
                pds.PageSize = AspNetPager1.PageSize;
                this.DataList1.DataSource = pds;
                this.DataList1.DataBind();
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            bindData();
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