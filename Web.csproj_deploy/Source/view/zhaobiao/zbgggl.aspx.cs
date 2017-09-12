using System;
using System.Web.UI.WebControls;
using System.Data;
using LTP.Common;
using System.Text;
using System.Web;

namespace SanZi.Web.zhaobiao
{
    public partial class zbgggl : System.Web.UI.Page
    {
        public SanZi.BLL.ZhaoBiao bll = new SanZi.BLL.ZhaoBiao();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.Session["AccountID"] != null)
                {
                    zhongbBind();
                }

                else
                {
                    LTP.Common.MessageBox.ShowAndRedirect(this.Page, "请先选择账套", "../../HomePage.aspx");
                }
            }
        }
        public void zhongbBind()
        {
            string strWhere = "";
            DataSet ds = bll.GetZbggList(strWhere);
            this.pageBar.RecordCount = ds.Tables[0].Rows.Count;
            this.GridView1.PageIndex = this.pageBar.PageIndex;
            this.GridView1.PageSize = this.pageBar.PageSize;
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
        }
        protected void pageBar_PageIndexChanged(object sender, EventArgs e)
        {
            zhongbBind();
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
                zhongbBind();
            }
            catch { }

        }
        protected void Edit(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            Response.Redirect("zbggEdit.aspx?id=" + btn.CommandArgument);
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.ToString() == "DataRow")
            {
                //Label labstart=(Label)e.Row.Cells[3].FindControl("Labstart");
                //string startTime = labstart.Text;
                //Label labfinish = (Label)e.Row.Cells[3].FindControl("Labfinish");
                //string finishTime = labfinish.Text;
                //string zbsj = startTime + "—" + finishTime;
                //e.Row.Cells[2].Text = zbsj;
                e.Row.Cells[1].Text = "<a href='ChaKanZhaoBiaoGongGao.aspx?ID=" + GridView1.DataKeys[e.Row.RowIndex].Value.ToString() + "'>" + e.Row.Cells[1].Text + "</a>";
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                btnDelete.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除吗？')");
                LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
                string strWhere = "zbmc='" + e.Row.Cells[2].Text + "'";
                DataSet ds = bll.GetZbgonggList(strWhere);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnDelete.Enabled = false;
                    btnEdit.Enabled = false;
                }
                bool isNum = LTP.Common.PageValidate.IsNumber(e.Row.Cells[2].Text);
                if (isNum)
                {
                    e.Row.Cells[2].Text = Maticsoft.DBUtility.DbHelperMySQL.GetSingle("select ProjectName from daili where DaiLi_ID=" + e.Row.Cells[2].Text).ToString();
                }
            }
        }
    }
}
