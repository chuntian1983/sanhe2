using System;
using System.Web.UI.WebControls;
using System.Data;
using LTP.Common;
using System.Web;

namespace SanZi.Web.zhaobiao
{
    public partial class zbgongggl : System.Web.UI.Page
    {
        public SanZi.BLL.ZhaoBiao bll = new SanZi.BLL.ZhaoBiao();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.Session["AccountID"] != null)
                {
                    zbggBind();
                }
                else
                {
                    LTP.Common.MessageBox.ShowAndRedirect(this.Page, "请先选择账套", "../../HomePage.aspx");
                } 
            }
        }

        public void zbggBind()
        {
            string strWhere = "";
            DataSet ds = bll.GetZbgonggList(strWhere);
            this.pageBar.RecordCount = ds.Tables[0].Rows.Count;
            this.GridView1.PageIndex = this.pageBar.PageIndex;
            this.GridView1.PageSize = this.pageBar.PageSize;
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
        }
        protected void pageBar_PageIndexChanged(object sender, EventArgs e)
        {
            zbggBind();
        }

        protected void Delete(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int delID = int.Parse(btn.CommandArgument.ToString());
            try
            {
                bll.DeleteZhongb(delID);
                string message = "删除成功！";
                MessageBox.Show(this, message);
                zbggBind();
            }
            catch { }

        }
        protected void Edit(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            Response.Redirect("zhongbEdit.aspx?id=" + btn.CommandArgument);
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.ToString() == "DataRow")
            {
                //double time = double.Parse(e.Row.Cells[4].Text.ToString());
                //e.Row.Cells[4].Text = ((DateTime)LTP.Common.TimeParser.ConvertIntDateTime(time)).ToString("yyyy-MM-dd hh:mm:ss");
                //if (e.Row.Cells[1].Text.ToString().Length > 11)
                //{
                //    string str = e.Row.Cells[1].Text.ToString();
                //    str = str.Substring(0, 10) + "...";
                //    e.Row.Cells[1].Text = str;
                //}
                e.Row.Cells[1].Text = "<a href='ChaKanZhongBiaoGongGao.aspx?ID=" + GridView1.DataKeys[e.Row.RowIndex].Value.ToString() + "'>" + e.Row.Cells[1].Text + "</a>";
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                btnDelete.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除吗？')");
            }
        }
    }
}
