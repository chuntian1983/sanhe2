using System;
using System.Data;
using System.Web.UI;

namespace SanZi.Web.zhaobiao
{
    public partial class ChaKanYuSuanShu : System.Web.UI.Page
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
            SanZi.BLL.ZhaoBiao bll = new SanZi.BLL.ZhaoBiao();
            DataTable dt = bll.GetYuSuanShuByID(ID);
            if (dt.Rows.Count > 0)
            {
                this.lblTitle.Text = dt.Rows[0]["Title"].ToString();//标题
                this.lblProjectName2.Text = dt.Rows[0]["xmmc"].ToString();//项目名称
                this.lblPuttingTime.Text = dt.Rows[0]["xmsssj"].ToString();//项目实施时间
                this.lblDetail.Text = dt.Rows[0]["xmmx"].ToString();//项目明细
                this.lblVillage.Text = dt.Rows[0]["danwei"].ToString();//单位名称
                this.lblDate.Text = dt.Rows[0]["SubTime"].ToString();//时间

            }
            else
            {
                LTP.Common.MessageBox.ShowAndRedirect(this.Page, "参数有误！", "cpjggl.aspx");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("cyssgl.aspx");
        }

    }
}
