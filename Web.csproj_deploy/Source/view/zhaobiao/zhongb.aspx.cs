using System;
using LTP.Common;
using System.Web;
using System.Data;

namespace SanZi.Web.zhaobiao
{
    public partial class zhongb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (HttpContext.Current.Session["AccountID"] != null)
                {
                    this.tbncmc.Text = Public.AccountName;
                    this.tbsubtime.Text = System.DateTime.Now.ToString("yyyy年MM月dd日");
                    SanZi.BLL.DaiLi bll = new SanZi.BLL.DaiLi();
                    DataSet ds = bll.GetDaiLiList("");
                    tbzbmc.DataSource = ds.Tables[0].DefaultView;
                    tbzbmc.DataTextField = "ProjectName";
                    tbzbmc.DataValueField = "DaiLi_ID";
                    tbzbmc.DataBind();
                }
                else
                {
                    LTP.Common.MessageBox.ShowAndRedirect(this.Page, "请先选择账套", "../../HomePage.aspx");
                }
              
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bool flag = true;
            string strErr="";
            if(tbncmc.Text=="")
            {
                flag = false;
            }
            //if (tbzbmc.Text == "")
            //{
            //    flag = false;
            //}
            if (tbdwa.Text == "")
            {
                flag = false;
            }
            if (tbstarttime.Text == "")
            {
                flag = false;
            }
            if (this.tbfinishtime.Text == "")
            {
                flag = false;
            }
            if (this.tbzbdw.Text == "")
            {
                flag = false;
            }
            if (this.tbdws.Text == "")
            {
                flag = false;
            }
            if (!flag)
            {
                strErr += "请将中标公告内容填写完整！\\n";
            }
            if (!PageValidate.IsNumber(tbdws.Text))
            {
                strErr += "单位数的格式不正确！\\n";
            }
            if(this.tbztbdw.Text=="")
            {
                strErr += "请填写办公室名称！\\n";
            }
            if(this.tbsubtime.Text=="")
            {
                strErr += "请填写公告日期！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string ncmc = this.tbncmc.Text.Trim();
            string startTime = this.tbstarttime.Text.Trim();
            string finishTime = this.tbfinishtime.Text.Trim();
            string zbmc = this.tbzbmc.SelectedValue;
            string dwa = this.tbdwa.Text.Trim();
            string dwb = this.tbdwb.Text.Trim();
            string dwc = this.tbdwc.Text.Trim();
            string dws = this.tbdws.Text.Trim();
            string zbdw = this.tbzbdw.Text.Trim();
            string ztbdw = this.tbztbdw.Text.Trim();
            string subtime = this.tbsubtime.Text.Trim();
            SanZi.Model.zhongb model = new SanZi.Model.zhongb();
            model.Ncmc = ncmc;
            model.StartTime = startTime;
            model.FinishTime = finishTime;
            model.Zbmc = zbmc;
            model.Dwa = dwa;
            model.Dwb = dwb;
            model.Dwc = dwc;
            model.Dws = dws;
            model.Zbdw = zbdw;
            model.Ztbdw = ztbdw;
            model.SubTime = subtime;
            SanZi.BLL.ZhaoBiao bll = new SanZi.BLL.ZhaoBiao();
            try
            {
                bll.Addzbgg(model);
                //string message = "保存成功！";
                //MessageBox.Show(this, message);
                LTP.Common.MessageBox.ShowAndRedirect(this.Page, "保存成功！", "zbgongggl.aspx");
            }
            catch { }

        }
    }
}
