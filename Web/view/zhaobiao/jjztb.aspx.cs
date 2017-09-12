using System;
using LTP.Common;

using System.Web;
using System.Data;

namespace SanZi.Web.zhaobiao
{
    public partial class jjztb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["AccountID"] != null)
            {
                SanZi.BLL.DaiLi bll = new SanZi.BLL.DaiLi();
                DataSet ds = bll.GetDaiLiList("");
                tbxmmc.DataSource = ds.Tables[0].DefaultView;
                tbxmmc.DataTextField = "ProjectName";
                tbxmmc.DataValueField = "DaiLi_ID";
                tbxmmc.DataBind();
            }
            else
            {
                LTP.Common.MessageBox.ShowAndRedirect(this.Page, "请先选择账套", "../../HomePage.aspx");
            }
        }

        protected void btn_sure_Click(object sender, EventArgs e)
        {
            string strErr = "";
            //if(tbxmmc.Text=="")
            //{
            //    strErr += "项目名称不能为空！\\n";
            //}
            if(tbsubtime.Text=="")
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
            model.Xmmc = xmmc;
            model.SubTime = subTime;
            model.Adress = adress;
            model.Cyry = cyry;
            model.Zcr = zcr;
            model.Cbr = cbr;
            model.Jlr = jlr;
            model.Zynr = zynr;
            SanZi.BLL.ZhaoBiao bll = new SanZi.BLL.ZhaoBiao();
            try
            {
                bll.Addjjztb(model);
                //string message = "保存成功！";
                //MessageBox.Show(this, message);
                LTP.Common.MessageBox.ShowAndRedirect(this.Page, "保存成功！", "jjztbgl.aspx");
            }
            catch { }

        }
    }
}
