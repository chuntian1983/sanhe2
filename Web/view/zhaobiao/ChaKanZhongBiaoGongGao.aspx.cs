using System;
using System.Data;
using System.Web.UI;

namespace SanZi.Web.zhaobiao
{
    public partial class ChaKanZhongBiaoGongGao : System.Web.UI.Page
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
            DataTable dt = bll.GetZhongBiaoGongGaoByID(ID);
            if (dt.Rows.Count > 0)
            {
                this.lblNCMC.Text = dt.Rows[0]["ncmc"].ToString();
                this.lblStartDate.Text = dt.Rows[0]["startTime"].ToString();
                this.lblEndDate.Text = dt.Rows[0]["finishTime"].ToString();
                this.lblProjectName.Text = dt.Rows[0]["zbmc"].ToString();
                this.lblDanWeiA.Text = dt.Rows[0]["dwa"].ToString();
                this.lblDanWeiB.Text = dt.Rows[0]["dwb"].ToString();
                this.lblDanWeiC.Text = dt.Rows[0]["dwc"].ToString();
                this.lblNum.Text = dt.Rows[0]["dws"].ToString();
                this.lblZhongBiao.Text = dt.Rows[0]["zbdw"].ToString();
                this.lblOffice.Text=dt.Rows[0]["ztbdw"].ToString();
                this.lblPubDate.Text = dt.Rows[0]["subtime"].ToString();
            }
            else
            {
                LTP.Common.MessageBox.ShowAndRedirect(this.Page, "参数有误！", "cpjggl.aspx");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("zbgongggl.aspx");
        }

    }
}
