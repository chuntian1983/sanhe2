using System;
using LTP.Common;
using System.Text;
using System.Web;
using System.Data;

namespace SanZi.Web.zhaobiao
{
    public partial class zbgg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.Session["AccountID"] != null)
                {
                    this.tbcwh.Text = Public.AccountName;
                    this.tbsubtime.Text = System.DateTime.Now.ToString("yyyy年MM月dd日");
                    SanZi.BLL.DaiLi bll = new SanZi.BLL.DaiLi();
                    DataSet ds = bll.GetDaiLiList("");
                    tbzbgc.DataSource = ds.Tables[0].DefaultView;
                    tbzbgc.DataTextField = "ProjectName";
                    tbzbgc.DataValueField = "DaiLi_ID";
                    tbzbgc.DataBind();
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
            if ((this.tbcwh.Text == ""))
            {
                strErr += "请将主题填写完整！\\n";
            }
            if (this.tbnrqk.Text == "")
            {
                strErr += "请填写招标内容及项目情况！\\n";
            }
            if (this.tbbmtj.Text == "")
            {
                strErr += "请填写报名条件！\\n";
            }
            if (this.tbyz.Text == "")
            {
                strErr += "请将招标程序填写完整！\\n";
            }
            if ((this.tbstarttime.Text == "") && (this.tbfinishtime.Text == ""))
            {
                strErr += "请将报名时间填写完整！\\n";
            }
            if (tbbmdd.Text == "")
            {
                strErr += "请将报名地点填写完整！\\n";
            }
            if (tblxfs.Text == "")
            {
                strErr += "请填写联系人及电话！\\n";
            }
            if (tbsubtime.Text == "")
            {
                strErr += "请填写时间！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string cwh = tbcwh.Text.Trim();
            string zbgc = tbzbgc.SelectedValue;
            string nrqk = tbnrqk.Text.Trim();
            string bmtj = tbbmtj.Text.Trim();
            string yz = tbyz.Text.Trim();
            string startTime = tbstarttime.Text;
            string finishtime = tbfinishtime.Text;
            string bmdd = tbbmdd.Text.Trim();
            string lxfs = tblxfs.Text.Trim();
            string subTime = tbsubtime.Text;
            string str = TextBox1.Text;
            SanZi.Model.zbgg model = new SanZi.Model.zbgg();
            model.Cwh = cwh;
            model.Zbgc = zbgc;
            model.Nrqk = nrqk;
            model.Bmtj = bmtj;
            model.Yz = yz;
            model.StartTime = startTime;
            model.FinishTime = finishtime;
            model.Bmdd = bmdd;
            model.Lxfs = lxfs;
            model.SubTime = subTime;
            model.str = str;
            SanZi.BLL.ZhaoBiao bll = new SanZi.BLL.ZhaoBiao();
            bll.Add(model);
            //string message = "保存成功！";
            //MessageBox.Show(this,message);
            LTP.Common.MessageBox.ShowAndRedirect(this.Page, "保存成功！", "zbgggl.aspx");
        }
    }
}
