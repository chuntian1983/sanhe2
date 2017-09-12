using System;
using LTP.Common;

namespace SanZi.Web.CePing
{
    public partial class cpjg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.tbcun.Text = Public.AccountName;
                this.year1.Text = System.DateTime.Now.Year.ToString();
                this.month1.Text = System.DateTime.Now.Month.ToString();
                this.day1.Text = System.DateTime.Now.Day.ToString();
                this.year2.Text = System.DateTime.Now.Year.ToString();
                this.month2.Text = System.DateTime.Now.Month.ToString();
                this.day2.Text = System.DateTime.Now.Day.ToString();

            }
        }
        protected void btn_sure_Click(object sender, EventArgs e)
        {
            string strErr = "请填写完整";
            bool flag = true;
            if(this.tbxian.Text =="")
            {
                flag=false;	
            }
            if(this.tbcunm.Text =="")
            {
                flag = false;	
            }
            if (tbcun.Text == "")
            {
                flag = false;
            }
            if (this.tbzrs.Text == "")
            {
                flag = false;
            }
            if (this.tbmyrs.Text == "")
            {
                flag = false;
            }
            if (this.tbbmyrs.Text == "")
            {
                flag = false;
            }
            if (this.tbjbmy.Text == "")
            {
                flag = false;
            }
            if (this.tbzhen.Text == "")
            {
                flag = false;
            }
            if (!flag)
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string Title = "";
            Title = this.tbcun.Text + "村三资管理民主测评结果";
            string Content = "";
            Content += "根据《" + tbxian.Text + "市三资管理方法》公示测评监督制有关规定，";
            Content += this.tbcunm.Text + "村于"+this.year1.Text+"年"+this.month1.Text+"月"+this.day1.Text+"日";
            Content += "对村" + this.DropDownList1.SelectedValue.ToString() + "三资管理情况进行测评，";
            Content += "共" + this.tbzrs.Text + "人参加（超过村民总数5%），其中满意"+this.tbmyrs.Text+"人，基本满意";
            Content += this.tbjbmy.Text + "人，不满意" + this.tbbmyrs.Text+" 人。";
            string danwei = "";
            danwei = this.tbzhen.Text + "（镇、街道）三资托管服务中心";
            //System.DateTime time = System.DateTime.Now;
            //int AddTime = (int)LTP.Common.TimeParser.ConvertDateTimeInt(time);
            string AddTime = year2.Text + "-" + month2.Text + "-" + day2.Text;
            SanZi.Model.mzcp model = new SanZi.Model.mzcp();
            model.Title = Title;
            model.Content = Content;
            model.DanWei = danwei;
            model.AddTime = AddTime;
            SanZi.BLL.CePing bll = new SanZi.BLL.CePing();
            try
            {
                bll.Addcpjg(model);
               // Page.RegisterStartupScript("cpjg", "<script  language=javascript>alert('保存成功！')</script>");
                LTP.Common.MessageBox.ShowAndRedirect(this.Page, "保存成功！", "cpjggl.aspx");
            }
            catch
            { }
     
        }

    }
}
