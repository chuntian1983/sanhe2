using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SanZi.Web.GongKai
{
    public partial class Show : System.Web.UI.Page
    {
        public string docType = "doc";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["dt"] != null && Request.QueryString["dt"].Trim() != "")
            {
                docType = Request.QueryString["dt"];
            }
            if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
            {
                string strID = Request.Params["id"].Trim();
                ShowInfo(strID);
            }
            if (Request.QueryString["lbid"] != null)
            {
                this.hidlbid.Value = Request.QueryString["lbid"];
            }
        }

        private void ShowInfo(string strID)
        {
            int id = int.Parse(strID);
            SanZi.BLL.CunWuGongKai bll = new SanZi.BLL.CunWuGongKai();
            DataTable dt = bll.getInfoByID(id);
            this.lblDeptName.Text = dt.Rows[0]["DeptName"].ToString();
            this.lblTitle.Text = dt.Rows[0]["Title"].ToString();

            string url = "http://" + Request.ServerVariables["HTTP_HOST"].ToString() + Request.ServerVariables["PATH_INFO"].ToString();  //获得URL的值
            int i = url.LastIndexOf("/");
            url = url.Substring(0, i) + "/File/" + dt.Rows[0]["FileName"].ToString().Trim();
            this.Session["FileUrl"] = url; //定义Sesssion变量

            dt.Clear();
        }

       

       
    }
}
