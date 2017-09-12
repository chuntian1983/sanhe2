using System;
using System.Collections.Generic;

using LTP.Common;
using System.Web.UI.WebControls;
using System.Data;

namespace SanZi.Web.CePing
{
    public partial class cpjggl : System.Web.UI.Page
    {
        public SanZi.BLL.CePing bll = new SanZi.BLL.CePing();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                mzcpBind();
            }
        }
        public void mzcpBind()
        {
            string strWhere = "";
            DataSet ds = bll.GetMzcpList(strWhere);
            this.pageBar.RecordCount = ds.Tables[0].Rows.Count;
            this.GridView1.PageIndex = this.pageBar.PageIndex;
            this.GridView1.PageSize = this.pageBar.PageSize;
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
        }
        protected void pageBar_PageIndexChanged(object sender, EventArgs e)
        {
            mzcpBind();
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.ToString() == "DataRow")
            {
                if (e.Row.Cells[1].Text.ToString().Length > 12)
                {
                    string str = e.Row.Cells[1].Text.ToString();
                    str = str.Substring(0, 11) + "...";
                    e.Row.Cells[1].Text = str;
                }
                if (e.Row.Cells[2].Text.ToString().Length > 20)
                {
                    string str = e.Row.Cells[2].Text.ToString();
                    str = str.Substring(0, 19) + "...";
                    e.Row.Cells[2].Text = str;
                }
                if (e.Row.Cells[3].Text.ToString().Length > 11)
                {
                    string str = e.Row.Cells[3].Text.ToString();
                    str = str.Substring(0, 10) + "...";
                    e.Row.Cells[3].Text = str;
                }
                e.Row.Cells[1].Text = "<a href='ChaKanCePingJieGuo.aspx?ID=" + GridView1.DataKeys[e.Row.RowIndex].Value.ToString() + "' >" + e.Row.Cells[1].Text + "</a>";
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "del")
            {
                int delID = int.Parse(e.CommandArgument.ToString());
                try
                {
                    bll.DeleteMzcp(delID);
                    string message = "删除成功！";
                    MessageBox.Show(this, message);
                    mzcpBind();
                }
                catch { }
            }
        }

    }
}
