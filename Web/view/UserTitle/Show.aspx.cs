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
namespace SanZi.Web.usertitle
{
    public partial class Show : System.Web.UI.Page
    {        
        		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					string id = Request.Params["id"];
					//ShowInfo(TitleID);
				}
			}
		}
		
	private void ShowInfo(int TitleID)
	{
		SanZi.BLL.UserTitle bll=new SanZi.BLL.UserTitle();
		SanZi.Model.UserTitle model=bll.GetModel(TitleID);
		this.lblTitleName.Text=model.TitleName;
		this.lblTitleDesc.Text=model.TitleDesc;
		this.lblSubTime.Text=model.SubTime.ToString();
		this.lblSubUID.Text=model.SubUID.ToString();
		this.lblDelFlag.Text=model.DelFlag.ToString();

	}


    }
}
