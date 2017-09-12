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
namespace SanZi.Web.usertitle
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
					//ShowInfo(TitleID);
				}
			}
		}
			
	private void ShowInfo(int TitleID)
	{
		SanZi.BLL.UserTitle bll=new SanZi.BLL.UserTitle();
		SanZi.Model.UserTitle model=bll.GetModel(TitleID);
		this.lblTitleID.Text=model.TitleID.ToString();
		this.txtTitleName.Text=model.TitleName;
		this.txtTitleDesc.Text=model.TitleDesc;
		this.txtSubTime.Text=model.SubTime.ToString();
		this.txtSubUID.Text=model.SubUID.ToString();
		this.txtDelFlag.Text=model.DelFlag.ToString();

	}

		public void btnUpdate_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtTitleName.Text =="")
			{
				strErr+="TitleName不能为空！\\n";	
			}
			if(this.txtTitleDesc.Text =="")
			{
				strErr+="TitleDesc不能为空！\\n";	
			}
			if(!PageValidate.IsNumber(txtSubTime.Text))
			{
				strErr+="SubTime不是数字！\\n";	
			}
			if(!PageValidate.IsNumber(txtSubUID.Text))
			{
				strErr+="SubUID不是数字！\\n";	
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
			string TitleName=this.txtTitleName.Text;
			string TitleDesc=this.txtTitleDesc.Text;
			int SubTime=int.Parse(this.txtSubTime.Text);
			int SubUID=int.Parse(this.txtSubUID.Text);
			int DelFlag=int.Parse(this.txtDelFlag.Text);


			SanZi.Model.UserTitle model=new SanZi.Model.UserTitle();
			model.TitleName=TitleName;
			model.TitleDesc=TitleDesc;
			model.SubTime=SubTime;
			model.SubUID=SubUID;
			model.DelFlag=DelFlag;

			SanZi.BLL.UserTitle bll=new SanZi.BLL.UserTitle();
			bll.Update(model);

		}

    }
}
