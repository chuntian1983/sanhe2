using System;
using System.Collections.Generic;
using LTP.Common;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SanZi.Web.zhaobiao
{
    public partial class cyssEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    SanZi.BLL.DaiLi bll = new SanZi.BLL.DaiLi();
                    DataSet ds = bll.GetDaiLiList("");
                    tbxmmc.DataSource = ds.Tables[0].DefaultView;
                    tbxmmc.DataTextField = "ProjectName";
                    tbxmmc.DataValueField = "DaiLi_ID";
                    tbxmmc.DataBind();
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
                this.tbyss.Text = dt.Rows[0]["Title"].ToString();//标题
                this.tbxmmc.Text = dt.Rows[0]["xmmc"].ToString();//项目名称
                this.tbxmsssj.Text = dt.Rows[0]["xmsssj"].ToString();//项目实施时间
                this.tbxmmx.Text = dt.Rows[0]["xmmx"].ToString();//项目明细
                this.tbcwh.Text = dt.Rows[0]["danwei"].ToString();//单位名称
                this.lblDate.Text = dt.Rows[0]["SubTime"].ToString();//时间

            }
            else
            {
                LTP.Common.MessageBox.ShowAndRedirect(this.Page, "参数有误！", "cpjggl.aspx");
            }
        }
        protected void btn_sure_Click(object sender, EventArgs e)
        {
            string strErr = "";
            if (this.tbyss.Text == "")
            {
                strErr += "请将标题填写完整！\\n";
            }
            //if (this.tbxmmc.Text == "")
            //{
            //    strErr += "项目名称不能为空！\\n";
            //}
            if (this.tbxmsssj.Text == "")
            {
                strErr += "项目实施时间不能为空！\\n";
            }
            if (this.tbxmmx.Text == "")
            {
                strErr += "项目明细不能为空！\\n";
            }
            if (this.tbcwh.Text == "")
            {
                strErr += "请填写完整的村委会名称！\\n";
            }
           
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string title = "";
            title = tbyss.Text ;
            string xmmc = this.tbxmmc.SelectedValue;
            string xmsssj = this.tbxmsssj.Text;
            string xmmx = this.tbxmmx.Text;
            string danwei = this.tbcwh.Text + "村委会";
           
            SanZi.Model.cyss model = new SanZi.Model.cyss();
            if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
            {
                model.Title = title;
                model.xmmc = xmmc;
                model.xmsssj = xmsssj;
                model.xmmx = xmmx;
                model.danwei = danwei;
                model.ID = int.Parse(Request.Params["id"]);
                model.subTime = System.DateTime.Now.ToString("yyyy-MM-dd");
            }
            
            SanZi.BLL.CePing bll = new SanZi.BLL.CePing();
            try
            {
                bll.Updatecyss(model);
                LTP.Common.MessageBox.ShowAndRedirect(this.Page, "保存成功！", "cyssgl.aspx");
            }
            catch
            { }
        }
    }
}
