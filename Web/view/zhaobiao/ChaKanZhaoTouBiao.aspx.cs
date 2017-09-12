using System;
using System.Data;
using System.Web.UI;

namespace SanZi.Web.zhaobiao
{
    public partial class ChaKanZhaoTouBiao : System.Web.UI.Page
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
            DataTable dt = bll.GetZhaoTouBiaoByID(ID);
            if (dt.Rows.Count > 0)
            {
                this.lblProjectName.Text = dt.Rows[0]["xmmc"].ToString();//项目名称
                this.lblShiJian.Text = dt.Rows[0]["subTime"].ToString();//时间
                this.lblDiDian.Text = dt.Rows[0]["adress"].ToString();//地点
                this.lblCanJiaRenYuan.Text = dt.Rows[0]["cyry"].ToString();//参加人员
                this.lblZhuChiRen.Text = dt.Rows[0]["zcr"].ToString();//主持人
                this.lblChangBiaoRen.Text = dt.Rows[0]["cbr"].ToString();//唱标人
                this.lblJiLuRen.Text = dt.Rows[0]["jlr"].ToString();//记录人
                this.lblZhuYaoNeiRong.Text = dt.Rows[0]["zynr"].ToString();//主要内容

            }
            else
            {
                LTP.Common.MessageBox.ShowAndRedirect(this.Page, "参数有误！", "cpjggl.aspx");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("jjztbgl.aspx");
        }


    }
}
