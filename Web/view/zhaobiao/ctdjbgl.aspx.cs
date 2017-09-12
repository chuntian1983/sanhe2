using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LTP.Common;
using System.Web;
namespace SanZi.Web.zhaobiao
{
    public partial class ctdjbgl : System.Web.UI.Page
    {
        public SanZi.BLL.ZhaoBiao bll = new SanZi.BLL.ZhaoBiao();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (HttpContext.Current.Session["AccountID"] != null)
                {
                    xiangmuBind();
                }
                else
                {
                    LTP.Common.MessageBox.ShowAndRedirect(this.Page, "请先选择账套", "../../HomePage.aspx");
                }
            }
        }

        public void xiangmuBind()
        {
            string strWhere = "";
            DataSet ds = bll.GetXiangmuList(strWhere);
            this.pageBar.RecordCount = ds.Tables[0].Rows.Count;
            this.GridView1.PageIndex = this.pageBar.PageIndex;
            this.GridView1.PageSize = this.pageBar.PageSize;
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
        }

        protected void pageBar_PageIndexChanged(object sender, EventArgs e)
        {
            xiangmuBind();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "view")
            {
                string ID = e.CommandArgument.ToString();
                Page.RegisterClientScriptBlock("new","<script>window.open('ctdjbview.aspx?ID="+ID+"')</script>");
            }
        }
    }
}
