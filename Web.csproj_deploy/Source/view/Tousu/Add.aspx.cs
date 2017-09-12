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
namespace SanZi.Web.tousu
{
    public partial class Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            this.txtDeptName.Text = Public.AccountName;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
		{
			string strErr="";
			if(this.txtContent.Text =="")
			{
                strErr += "请填写投诉内容！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}

            string DeptName = this.txtDeptName.Text.Trim();//单位名称
            int DeptID = 0;// int.Parse(Request.Form["hidDeptID"].ToString());//单位名称ID
			string Content=this.txtContent.Text.Replace("'","");//投诉内容
			int SubUID=0;//用户ID
            string SubIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"]; //IP
            if (SubIP == null || SubIP == "") SubIP = Request.ServerVariables["REMOTE_ADDR"];//Request.UserHostAddress;

            //System.DateTime time = System.DateTime.Now;
            //int SubTime = (int)LTP.Common.TimeParser.ConvertDateTimeInt(time);
            string strSubTime = System.DateTime.Now.ToString("yyyy-MM-dd");

			SanZi.Model.Tousu model=new SanZi.Model.Tousu();
            model.TFlag = 0;
            model.DeptID = DeptID;
            model.DeptName = DeptName;
			model.Content=Content;
            if(HttpContext.Current.Session["UserID"]!=null)
            {
                SubUID = int.Parse(HttpContext.Current.Session["UserID"].ToString());
            }
			model.SubUID=SubUID;
			model.SubIP=SubIP;
			model.SubTime=strSubTime;


			SanZi.BLL.Tousu bll=new SanZi.BLL.Tousu();
            try
            {
                bll.Add(model);
                Response.Clear();
                Response.Write("<script>alert('投诉成功！谢谢参与！');window.close();</script>");
                Response.End();
            }
            catch { }

		}

    }
}
