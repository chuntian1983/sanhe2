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
namespace SanZi.Web.pibankabarcode
{
    public partial class Modify : System.Web.UI.Page
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
		this.lblID.Text=model.ID.ToString();
		this.txtPID.Text=model.PID.ToString();
		this.txtBarCode.Text=model.BarCode;
		this.txtSubTime.Text=model.SubTime.ToString();
		this.txtDelFlag.Text=model.DelFlag.ToString();

	}

		public void btnUpdate_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(!PageValidate.IsNumber(txtPID.Text))
			{
				strErr+="PID不是数字！\\n";	
			}
			if(this.txtBarCode.Text =="")
			{
				strErr+="BarCode不能为空！\\n";	
			}
			if(!PageValidate.IsNumber(txtSubTime.Text))
			{
				strErr+="SubTime不是数字！\\n";	
			}
			if(!PageValidate.IsNumber(txtDelFlag.Text))
			{
				strErr+="DelFlag不是数字！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			int PID=int.Parse(this.txtPID.Text);
			string BarCode=this.txtBarCode.Text;
			int SubTime=int.Parse(this.txtSubTime.Text);
			int DelFlag=int.Parse(this.txtDelFlag.Text);


			SanZi.Model.PiBanKaBarcode model=new SanZi.Model.PiBanKaBarcode();
			model.PID=PID;
			model.BarCode=BarCode;
			model.SubTime=SubTime;
			model.DelFlag=DelFlag;

			SanZi.BLL.PiBanKaBarcode bll=new SanZi.BLL.PiBanKaBarcode();
			bll.Update(model);

		}

    }
}
