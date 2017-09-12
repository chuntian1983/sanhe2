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
		SanZi.BLL.CePing bll=new SanZi.BLL.CePing();
		SanZi.Model.CePing model=bll.GetModel(ID);
		this.lblID.Text=model.ID.ToString();
		this.txtEvaluation.Text=model.Evaluation;
		this.txtSubUID.Text=model.SubUID.ToString();
		this.txtSubIP.Text=model.SubIP;
		this.txtSubTime.Text=model.SubTime.ToString();
		this.txtOptionChecked.Text=model.OptionChecked.ToString();

	}

		public void btnUpdate_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtEvaluation.Text =="")
			{
				strErr+="Evaluation不能为空！\\n";	
			}
			if(!PageValidate.IsNumber(txtSubUID.Text))
			{
				strErr+="SubUID不是数字！\\n";	
			}
			if(this.txtSubIP.Text =="")
			{
				strErr+="SubIP不能为空！\\n";	
			}
			if(!PageValidate.IsNumber(txtSubTime.Text))
			{
				strErr+="SubTime不是数字！\\n";	
			}
			if(!PageValidate.IsNumber(txtOptionChecked.Text))
			{
				strErr+="OptionChecked不是数字！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string Evaluation=this.txtEvaluation.Text;
			int SubUID=int.Parse(this.txtSubUID.Text);
			string SubIP=this.txtSubIP.Text;
			int SubTime=int.Parse(this.txtSubTime.Text);
			int OptionChecked=int.Parse(this.txtOptionChecked.Text);


			SanZi.Model.CePing model=new SanZi.Model.CePing();
			model.Evaluation=Evaluation;
			model.SubUID=SubUID;
			model.SubIP=SubIP;
			model.SubTime=SubTime;
			model.OptionChecked=OptionChecked;

			SanZi.BLL.CePing bll=new SanZi.BLL.CePing();
			bll.Update(model);

		}

    }
}
