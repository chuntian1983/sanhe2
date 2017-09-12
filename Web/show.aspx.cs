using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SanZi.Web.view.zhaotoubiao
{
    public partial class show : System.Web.UI.Page
    {
        public string strpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"]!=null)
                {
                    string strsql = "select * from cw_ztbfj where id='"+Request.QueryString["id"]+"'";
                    DataTable dt = MainClass.GetDataTable(strsql);
                    if (dt.Rows.Count>0)
                    {
                         strpath = dt.Rows[0]["xmpath"].ToString();
                        
                        
                        
                    }
                }
            }
        }
       
    }
}