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
namespace SanZi.Web.pibankabarcode
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
					//ShowInfo(ID);
				}
			}
		}
		
	private void ShowInfo(int ID)
	{
		SanZi.BLL.PiBanKaBarcode bll=new SanZi.BLL.PiBanKaBarcode();
		SanZi.Model.PiBanKaBarcode model=bll.GetModel(ID);
		this.lblPID.Text=model.PID.ToString();
		this.lblBarCode.Text=model.BarCode;
		this.lblSubTime.Text=model.SubTime.ToString();
		this.lblDelFlag.Text=model.DelFlag.ToString();

	}


    }
}
