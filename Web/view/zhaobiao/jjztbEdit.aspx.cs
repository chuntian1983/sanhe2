using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LTP.Common;

namespace SanZi.Web.zhaobiao
{
    public partial class jjztbEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SanZi.BLL.DaiLi bll = new SanZi.BLL.DaiLi();
                DataSet ds = bll.GetDaiLiList("");
                tbxmmc.DataSource = ds.Tables[0].DefaultView;
                tbxmmc.DataTextField = "ProjectName";
                tbxmmc.DataValueField = "DaiLi_ID";
                tbxmmc.DataBind();
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
                this.tbxmmc.Text = dt.Rows[0]["xmmc"].ToString();//项目名称
                this.tbsubtime.Text = dt.Rows[0]["subTime"].ToString();//时间
                this.tbadress.Text = dt.Rows[0]["adress"].ToString();//地点
                this.tbcyry.Text = dt.Rows[0]["cyry"].ToString();//参加人员
                this.tbzcr.Text = dt.Rows[0]["zcr"].ToString();//主持人
                this.tbcbr.Text = dt.Rows[0]["cbr"].ToString();//唱标人
                this.tbjlr.Text = dt.Rows[0]["jlr"].ToString();//记录人
                this.tbzynr.Text = dt.Rows[0]["zynr"].ToString();//主要内容

            }
            else
            {
                LTP.Common.MessageBox.ShowAndRedirect(this.Page, "参数有误！", "cpjggl.aspx");
            }
        }
        protected void btn_sure_Click(object sender, EventArgs e)
        {
            string strErr = "";
            //if (tbxmmc.Text == "")
            //{
            //    strErr += "项目名称不能为空！\\n";
            //}
            if (tbsubtime.Text == "")
            {
                strErr += "时间不能为空！\\n";
            }
            if (tbadress.Text == "")
            {
                strErr += "地点不能为空！\\n";
            }
            if (this.tbcyry.Text == "")
            {
                strErr += "参加人员不能为空！\\n";
            }
            if (this.tbzcr.Text == "")
            {
                strErr += "主持人不能为空！\\n";
            }
            if (this.tbcbr.Text == "")
            {
                strErr += "唱标人不能为空！\\n";
            }
            if (this.tbjlr.Text == "")
            {
                strErr += "记录人不能为空！\\n";
            }
            if (this.tbzynr.Text == "")
            {
                strErr += "主要内容不能为空！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string xmmc = tbxmmc.SelectedValue;
            string subTime = tbsubtime.Text.Trim();
            string adress = tbadress.Text.Trim();
            string cyry = tbcyry.Text.Trim();
            string zcr = tbzcr.Text.Trim();
            string cbr = tbcbr.Text.Trim();
            string jlr = tbjlr.Text.Trim();
            string zynr = tbzynr.Text.Trim();
            SanZi.Model.jjztb model = new SanZi.Model.jjztb();
            if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
            {
                model.ID = int.Parse(Request.Params["id"]);
                model.Xmmc = xmmc;
                model.SubTime = subTime;
                model.Adress = adress;
                model.Cyry = cyry;
                model.Zcr = zcr;
                model.Cbr = cbr;
                model.Jlr = jlr;
                model.Zynr = zynr;
            }

            SanZi.BLL.ZhaoBiao bll = new SanZi.BLL.ZhaoBiao();
            try
            {
                bll.Updatejjztb(model);
                //string message = "保存成功！";
                //MessageBox.Show(this, message);
                LTP.Common.MessageBox.ShowAndRedirect(this.Page, "保存成功！", "jjztbgl.aspx");
            }
            catch { }
        }
    }
}
