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
namespace SanZi.Web.tousu2
{
    public partial class Show : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					int ID = int.Parse(Request.Params["id"]);
					ShowInfo(ID);
				}
			}
		}
		
	private void ShowInfo(int ID)
	{
		SanZi.BLL.Tousu bll=new SanZi.BLL.Tousu();
        DataTable dt = bll.GetTouSuByID(ID);
        if (dt.Rows.Count > 0)
        {
            this.lblDeptName.Text = dt.Rows[0]["DeptName"].ToString();
            this.lblContent.Text = dt.Rows[0]["Content"].ToString();
            this.lblIP.Text = dt.Rows[0]["SubIP"].ToString();
            this.lblTime.Text = dt.Rows[0]["SubTime"].ToString();
        }
        else
        {
            LTP.Common.MessageBox.ShowAndRedirect(this.Page, "参数有误！", "tsgl.aspx");
        }
	}

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Write(" <script>window.opener=null;window.close(); </script>");
    }


    }
}
