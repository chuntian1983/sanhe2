using System;
using System.Web.UI.WebControls;
using System.Data;
using LTP.Common;

namespace SanZi.Web.zhaobiao
{
    public partial class ctdjbview : System.Web.UI.Page
    {
        public SanZi.BLL.ZhaoBiao bll= new SanZi.BLL.ZhaoBiao();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if ((Request["ID"].ToString() != "")&&(Request["ID"] != null))
                {
                    this.LabxmID.Text = Request["ID"].ToString();
                }
                ctdjbBind();
            }
        }
         public void ctdjbBind()
        {
            string strWhere = "";
            if (LabxmID.Text != "")
            {
                strWhere = " xmID=" + this.LabxmID.Text+" order by xh";
            }
            else
            {
                strWhere = " 1>2";
                this.trxm.Visible = false;
            }
            DataSet ds = bll.GetCtdjbList(strWhere);
            this.pageBar.RecordCount = ds.Tables[0].Rows.Count;
            this.GridView1.PageIndex = this.pageBar.PageIndex;
            this.GridView1.PageSize = this.pageBar.PageSize;
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.Labxmmc.Text = ds.Tables[0].Rows[0]["xmmc"].ToString();
                this.LabsunTime.Text =  DateTime.Parse(ds.Tables[0].Rows[0]["subTime"].ToString()).ToString("yyyy年MM月dd日");
            }
            else
            {
                this.trxm.Visible = false;
            }
        }

         protected void pageBar_PageIndexChanged(object sender, EventArgs e)
         {
             ctdjbBind();
         }

             protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
             {
                 if (e.Row.RowType.ToString() == "DataRow")
                 {
                     e.Row.Cells[1].Text = "<a href='?ID=" + GridView1.DataKeys[e.Row.RowIndex].Value.ToString() + "'>" + e.Row.Cells[1].Text + "</a>";
                 }
             }

             protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
             {
                 if (e.CommandName == "del")
                 {
                     int delID = int.Parse(e.CommandArgument.ToString());
                     try
                     {
                         bll.DeleteCtdjb(delID);
                         string message = "删除成功！";
                         MessageBox.Show(this, message);
                         ctdjbBind();
                     }
                     catch { }
                 }
             }
        
    }
}
