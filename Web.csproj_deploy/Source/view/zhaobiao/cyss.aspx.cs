using System;
using LTP.Common;
using System.Text;
using System.Web;
using System.Data;
namespace SanZi.Web.zhaobiao
{
    public partial class cyss : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.Session["AccountID"] != null)
                {
                    this.tbcun.Text = Public.AccountName;
                    this.year.Text = System.DateTime.Now.Year.ToString();
                    this.month.Text = System.DateTime.Now.Month.ToString();
                    this.day.Text = System.DateTime.Now.Day.ToString();
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
           
        }

        protected void btn_sure_Click(object sender, EventArgs e)
        {
            string strErr = "";
            if ((this.tbcun.Text == "") && (this.tbyss.Text == ""))
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
            if ((this.year.Text == "") && (this.month.Text == "") && (this.day.Text == ""))
            {
                strErr += "请将村委会名称填写完整！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string title = "";
            title = tbcun.Text + "村" + tbyss.Text + "预算书";
            string xmmc = this.tbxmmc.SelectedValue;
            string xmsssj = this.tbxmsssj.Text;
            string xmmx = this.tbxmmx.Text;
            string danwei = this.tbcwh.Text + "村委会";
            string subTime = year.Text + "-" + month.Text + "-" + day.Text;
            SanZi.Model.cyss model = new SanZi.Model.cyss();
            model.Title = title;
            model.xmmc = xmmc;
            model.xmsssj = xmsssj;
            model.xmmx = xmmx;
            model.danwei = danwei;
            model.subTime = subTime;

            SanZi.BLL.CePing bll = new SanZi.BLL.CePing();
            try
            {
                bll.Addcyss(model);
                LTP.Common.MessageBox.ShowAndRedirect(this.Page, "保存成功！", "cyssgl.aspx");
            }
            catch
            { }
        }
    }
}
