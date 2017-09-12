using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using LTP.Common;
namespace SanZi.Web.ceping
{
    public partial class Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Master.TabTitle="我要测评：";
           this.txtDeptName.Text = Public.AccountName; 
            this.year.Text = System.DateTime.Now.Year.ToString();
            this.month.Text = System.DateTime.Now.Month.ToString();
            this.day.Text = System.DateTime.Now.Day.ToString();
        }

        		protected void btnAdd_Click(object sender, EventArgs e)
		{
			
			string strErr="";
            //if(this.txtEvaluation.Text =="")
            //{
            //    strErr+="综合评价不能为空！\\n";	
            //}
            //if(!PageValidate.IsNumber(txtSubUID.Text))
            //{
            //    strErr+="SubUID不是数字！\\n";	
            //}
            //if(this.txtSubIP.Text =="")
            //{
            //    strErr+="SubIP不能为空！\\n";	
            //}
            //if(!PageValidate.IsNumber(txtSubTime.Text))
            //{
            //    strErr+="SubTime不是数字！\\n";	
            //}
            //if(!PageValidate.IsNumber(txtOptionChecked.Text)
            //{
            //    strErr+="OptionChecked不是数字！\\n";	
            //}
            if(txtOptionChecked.Text=="")
            {
                strErr+="请选择综合评价！\\n";	
            }

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
            string Evaluation="";
            int SubUID = 0;
            string SubIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"]; //IP
            if (SubIP == null || SubIP == "") SubIP = Request.ServerVariables["REMOTE_ADDR"];//Request.UserHostAddress;

            string DeptName = this.txtDeptName.Text.Trim();//单位名称
            int DeptID = int.Parse(Request.Form["hidDeptID"].ToString());//单位名称ID

            System.DateTime time = System.DateTime.Now;
            //int SubTime = int.Parse(this.txtSubTime.Text);
			int OptionChecked=int.Parse(this.txtOptionChecked.Text);

			SanZi.Model.CePing model=new SanZi.Model.CePing();
            model.DeptID = DeptID;
            model.DeptName = DeptName;
			model.Evaluation=Evaluation;
            if (HttpContext.Current.Session["UserID"] != null)
            {
                SubUID = int.Parse(HttpContext.Current.Session["UserID"].ToString());
            }
			model.SubUID=SubUID;
			model.SubIP=SubIP;
            model.SubTime = (int)LTP.Common.TimeParser.ConvertDateTimeInt(time);
			model.OptionChecked=OptionChecked;

			SanZi.BLL.CePing bll=new SanZi.BLL.CePing();
            try
            {
                bll.Add(model);
                //Page.RegisterStartupScript("cp", "<script  language=javascript>alert('测评成功！')</script>");   
                LTP.Common.MessageBox.ShowAndRedirect(this.Page, "测评成功！", "Add.aspx");
            }
            catch
            { }

		}

    }
}
