using System;
using System.Web.UI.WebControls;
using LTP.Common;
using System.Data;
namespace SanZi.Web.tousu2
{
    public partial class tsgl : System.Web.UI.Page
    {
        public SanZi.BLL.Tousu bll = new SanZi.BLL.Tousu();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                tousuBind();
            }
        }

        public void tousuBind()
        {
            string strWhere = " DelFlag=0 and tflag=1";
            DataSet ds = bll.GetList(strWhere);
            this.pageBar.RecordCount = ds.Tables[0].Rows.Count;
            this.GridView1.PageIndex = this.pageBar.PageIndex;
            this.GridView1.PageSize = this.pageBar.PageSize;
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
        }
        protected void pageBar_PageIndexChanged(object sender, EventArgs e)
        {
            tousuBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "del")
            {
                int delID = int.Parse(e.CommandArgument.ToString());
                try
                {
                    bll.Delete(delID);
                    string message = "删除成功！";
                    MessageBox.Show(this, message);
                    tousuBind();
                }
                catch { }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.ToString() == "DataRow")
            {
                //e.Row.Cells[4].Text = e.Row.Cells[4].Text.ToString();
                if (e.Row.Cells[1].Text.ToString().Length > 11)
                {
                    string str = e.Row.Cells[1].Text.ToString();
                    str = str.Substring(0, 10) + "...";
                    e.Row.Cells[1].Text = str;
                }
            e.Row.Cells[1].Text = "<a href='show.aspx?ID=" + GridView1.DataKeys[e.Row.RowIndex].Value.ToString() + "' target='_blank'>" + e.Row.Cells[1].Text + "</a>";
            }
        }
    }
}
