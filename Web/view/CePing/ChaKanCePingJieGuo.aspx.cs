using System;
using System.Data;
using System.Web.UI;

namespace SanZi.Web.CePing
{
    public partial class ChaKanCePingJieGuo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    int ID = int.Parse(Request.Params["id"]);
                    ShowInfo(ID);
                }
            }
        }

        private void ShowInfo(int ID)
        {
            SanZi.BLL.CePing bll = new SanZi.BLL.CePing();
            DataTable dt = bll.GetCePingJieGuoByID(ID);
            if (dt.Rows.Count > 0)
            {
                this.lblTitle.Text = dt.Rows[0]["Title"].ToString();
                this.lblContent.Text = dt.Rows[0]["Content"].ToString();
                this.lblTime.Text = dt.Rows[0]["AddTime"].ToString();
            }
            else
            {
                LTP.Common.MessageBox.ShowAndRedirect(this.Page, "参数有误！", "cpjggl.aspx");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("cpjggl.aspx");
        }

    }
}
