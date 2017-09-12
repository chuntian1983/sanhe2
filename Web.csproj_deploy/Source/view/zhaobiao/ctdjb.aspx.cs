using System;
using System.Data;
using System.Web.UI.WebControls;
using LTP.Common;
using System.Web;

namespace SanZi.Web.zhaobiao
{
    public partial class ctdjb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.Session["AccountID"] != null)
                {
                ddlxmBand();
                ddlctdjBand();
                this.tbsubtime.Text = System.DateTime.Now.ToString("yyyy年MM月dd日"); }

                else
                {
                    LTP.Common.MessageBox.ShowAndRedirect(this.Page, "请先选择账套", "../../HomePage.aspx");
                }
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            string strErr = "";

            if (this.ddlxmmc.SelectedValue == "0")
            {
                strErr += "请选择项目！\\n";
            }
            else
            {

                if (this.tbsubtime.Text == "")
                {
                    strErr += "日期不能为空！\\n";
                }
                if (this.tbxh.Text == "")
                {
                    strErr += "序号不能为空！\\n";
                }
                if (this.tbtbr.Text == "")
                {
                    strErr += "投标人不能为空！\\n";
                }
                if (this.tbzizhi.Text == "")
                {
                    strErr += "资质不能为空！\\n";
                }
                if (this.tbfzr.Text == "")
                {
                    strErr += "负责人不能为空！\\n";
                }
                if (this.tblxdh.Text == "")
                {
                    strErr += "联系电话不能为空！\\n";
                }
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string xmID = this.ddlxmmc.SelectedValue.ToString();
            string xmmc = this.ddlxmmc.SelectedItem.Text.Trim();
            string subtime = Convert.ToDateTime(this.tbsubtime.Text.Trim()).ToString("yyyy-MM-dd");
            string xh = this.tbxh.Text.Trim();
            string tbr = this.tbtbr.Text.Trim();
            string zizhi = this.tbzizhi.Text.Trim();
            string fzr = this.tbfzr.Text.Trim();
            string lxdh = this.tblxdh.Text.Trim();

            SanZi.Model.ctdjb model = new SanZi.Model.ctdjb();
            model.XmID = Convert.ToInt32(xmID);
            model.Xmmc = xmmc;
            model.SubTime = subtime;
            model.Xh = xh;
            model.Tbr = tbr;
            model.Zizhi = zizhi;
            model.Fzr = fzr;
            model.Lxdh = lxdh;
            //Response.Write(lxdh);
            SanZi.BLL.ZhaoBiao bll = new SanZi.BLL.ZhaoBiao();
            try
            {
                bll.Addctdjb(model);
                string message = "保存成功！";
                MessageBox.Show(this, message);
                ddlctdjBand();
            }
            catch { }
        }

        //绑定项目下拉框
        public void ddlxmBand()
       {
           // SanZi.BLL.ZhaoBiao bll=new SanZi.BLL.ZhaoBiao();
           // string strWhere="";
           //DataSet ds = new DataSet();
           //ds = bll.selectxiangmu(strWhere);
           //DataTable dt = ds.Tables[0];
           //this.ddlxmmc.Items.Add(new ListItem("请选择项目","0"));
           //for (int i = 0; i < dt.Rows.Count; i++)
           //{
           //    ddlxmmc.Items.Add(new ListItem(dt.Rows[i]["xmmc"].ToString(),dt.Rows[i]["ID"].ToString()));
           //}
           SanZi.BLL.DaiLi bll = new SanZi.BLL.DaiLi();
           DataSet ds = bll.GetDaiLiList("");
           ddlxmmc.DataSource = ds.Tables[0].DefaultView;
           ddlxmmc.DataTextField = "ProjectName";
           ddlxmmc.DataValueField = "DaiLi_ID";
           ddlxmmc.DataBind();
       }

        //绑定gridview

        public void ddlctdjBand()
        {
            if (ddlxmmc.Items.Count > 0)
            {
                SanZi.BLL.ZhaoBiao bll = new SanZi.BLL.ZhaoBiao();
                string strWhere = " xmID=" + ddlxmmc.SelectedValue.ToString() + " order by xh";
                DataSet ds = new DataSet();
                ds = bll.selectctdj(strWhere);
                DataTable dt = ds.Tables[0];
                this.GridView1.DataSource = dt;
                this.GridView1.DataBind();
            }
        }

        protected void ddlxmmc_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlctdjBand();
        }

    }
}
