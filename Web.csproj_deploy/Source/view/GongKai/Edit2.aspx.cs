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
    public partial class Edit2 : System.Web.UI.Page
    {
        public string strurl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
            {
                string strID = Request.Params["id"].Trim();
                ShowInfo(strID);
            }
        }

        private void ShowInfo(string strID)
        {
            int id = int.Parse(strID);
            SanZi.BLL.CunWuGongKai bll = new SanZi.BLL.CunWuGongKai();
            DataTable dt = bll.getInfoByID(id);
            this.txtDeptName.Text = dt.Rows[0]["DeptName"].ToString();
            this.hidDeptID.Value = dt.Rows[0]["DeptID"].ToString();
            this.hidDocID.Value = dt.Rows[0]["ID"].ToString();
            this.hidFileName.Value = dt.Rows[0]["FileName"].ToString();
            this.txtTitleName.Text = dt.Rows[0]["Title"].ToString();
            this.txtDeptName.Text = UserInfo.AccountName;
            string url = "http://" + Request.ServerVariables["HTTP_HOST"].ToString() + Request.ServerVariables["PATH_INFO"].ToString();  //获得URL的值
            int i = url.LastIndexOf("/");
            url = url.Substring(0, i) + "/File/" + dt.Rows[0]["FileName"].ToString().Trim();
            this.Session["FileUrl"] = url; //定义Sesssion变量
            strurl = url.Substring(0, i) + "/savefile.aspx";
            dt.Clear();
        }
    }
}
