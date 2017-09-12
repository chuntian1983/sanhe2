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
using LTP.Common;

namespace SanZi.Web.zhaobiao
{
    public partial class jjztbgl : System.Web.UI.Page
    {
        public SanZi.BLL.ZhaoBiao bll = new SanZi.BLL.ZhaoBiao();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                if (HttpContext.Current.Session["AccountID"] != null)
                {
                    jjztbBind();
                }
                else
                {
                    LTP.Common.MessageBox.ShowAndRedirect(this.Page, "请先选择账套", "../../HomePage.aspx");
                }
            }
        }

        public void jjztbBind()
        {
            string strWhere = "";
            DataSet ds = bll.GetJjztbList(strWhere);
            this.pageBar.RecordCount = ds.Tables[0].Rows.Count;
            this.GridView1.PageIndex = this.pageBar.PageIndex;
            this.GridView1.PageSize = this.pageBar.PageSize;
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
        }
        protected void pageBar_PageIndexChanged(object sender, EventArgs e)
        {
            jjztbBind();
        }

       
        protected void Delete(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int delID = int.Parse(btn.CommandArgument.ToString());
            try
            {
                bll.DeleteJjztb(delID);
                string message = "删除成功！";
                MessageBox.Show(this, message);
                jjztbBind();
            }
            catch { }

        }
        protected void Edit(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            Response.Redirect("jjztbEdit.aspx?id=" + btn.CommandArgument);
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[8].Text.ToString().Length > 9)
                {
                    string str = e.Row.Cells[8].Text.ToString();
                    str = str.Substring(0, 8) + "...";
                    e.Row.Cells[8].Text = str;
                }
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                btnDelete.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除吗？')");
                LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
                string strWhere = "zbmc='" + e.Row.Cells[1].Text + "'";
                DataSet ds = bll.GetZbgonggList(strWhere);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnDelete.Enabled = false;
                    btnEdit.Enabled = false;
                }
                bool isNum = LTP.Common.PageValidate.IsNumber(e.Row.Cells[2].Text);
                if (isNum)
                {
                    e.Row.Cells[1].Text = Maticsoft.DBUtility.DbHelperMySQL.GetSingle("select ProjectName from daili where DaiLi_ID=" + e.Row.Cells[1].Text).ToString();
                }
                e.Row.Cells[1].Text = "<a href='ChaKanZhaoTouBiao.aspx?ID=" + GridView1.DataKeys[e.Row.RowIndex].Value.ToString() + "'>" + e.Row.Cells[1].Text + "</a>";
            }
        }
    }
}
