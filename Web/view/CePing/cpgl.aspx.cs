using System;
using System.Web.UI.WebControls;
using System.Data;
using LTP.Common;

namespace SanZi.Web.CePing
{
    public partial class cpgl : System.Web.UI.Page
    {
        public SanZi.BLL.CePing bll = new SanZi.BLL.CePing();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                cepingBand();
            }
        }
        public void cepingBand()
        {
            string strWhere="";
            DataSet ds=bll.GetList(strWhere);
            this.pageBar.RecordCount = ds.Tables[0].Rows.Count;
            this.GridView1.PageIndex = this.pageBar.PageIndex;
            this.GridView1.PageSize = this.pageBar.PageSize;
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
        }
        protected void pageBar_PageIndexChanged(object sender, EventArgs e)
        {
            cepingBand();
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.ToString() == "DataRow")
            {
                string zhpj = e.Row.Cells[2].Text.ToString();
                switch (zhpj)
                {
                    case "1":
                        e.Row.Cells[2].Text = "满意";
                        break;
                    case "2":
                        e.Row.Cells[2].Text = "基本满意";
                        break;
                    case "3":
                        e.Row.Cells[2].Text = "不满意";
                        break;
                    default:
                        break;

                }
                double time=double.Parse(e.Row.Cells[4].Text.ToString());
                e.Row.Cells[4].Text =((DateTime)LTP.Common.TimeParser.ConvertIntDateTime(time)).ToString("yyyy-MM-dd hh:mm:ss");
                //e.Row.Cells[1].Text = "<a href='?ID=" + GridView1.DataKeys[e.Row.RowIndex].Value.ToString() + "'>" + e.Row.Cells[1].Text + "</a>";
            }
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
                    cepingBand();
                }
                catch { }
            }
        }
    }
}
