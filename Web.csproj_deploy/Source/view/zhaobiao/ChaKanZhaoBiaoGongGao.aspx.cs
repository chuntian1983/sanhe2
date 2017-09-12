using System;
using System.Data;
using System.Web.UI;

namespace SanZi.Web.zhaobiao
{
    public partial class ChaKanZhaoBiaoGongGao : System.Web.UI.Page
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
            DataTable dt = bll.GetZhaoBiaoGongGaoByID(ID);
            if (dt.Rows.Count > 0)
            {
                this.lblDeptName.Text = dt.Rows[0]["cwh"].ToString();//单位名称
                this.lblProjectName.Text = dt.Rows[0]["zbgc"].ToString();//项目名称
                this.lblZhaoBiaoNeiRong.Text = dt.Rows[0]["nrqk"].ToString();//招标内容及项目情况
                this.lblBaoMingTiaoJian.Text = dt.Rows[0]["bmtj"].ToString();//报名条件
                this.lblYuanZe.Text = dt.Rows[0]["yz"].ToString();//原则
                this.lblStartDate.Text = dt.Rows[0]["startTime"].ToString();//开始时间
                this.lblEndDate.Text = dt.Rows[0]["finishTime"].ToString();//结束时间
                this.lblAddress.Text = dt.Rows[0]["bmdd"].ToString();//地点
                this.lblTel.Text = dt.Rows[0]["lxfs"].ToString();//联系人及电话
                this.lblPubDate.Text = dt.Rows[0]["SubTime"].ToString();//联系人及电话
                this.Label1.Text = dt.Rows[0]["str"].ToString();
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
