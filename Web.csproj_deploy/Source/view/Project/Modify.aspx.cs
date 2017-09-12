﻿using System;
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
namespace SanZi.Web.project
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
		SanZi.BLL.Project bll=new SanZi.BLL.Project();
		SanZi.Model.Project model=bll.GetModel(ID);
		this.lblID.Text=model.ID.ToString();
		this.txtProjectName.Text=model.ProjectName;
		this.txtSubTime.Text=model.SubTime.ToString();
		this.txtSubUID.Text=model.SubUID.ToString();
		this.txtDelFlag.Text=model.DelFlag.ToString();

	}

		public void btnUpdate_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtProjectName.Text =="")
			{
				strErr+="ProjectName不能为空！\\n";	
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
			string ProjectName=this.txtProjectName.Text;
			int SubTime=int.Parse(this.txtSubTime.Text);
			int SubUID=int.Parse(this.txtSubUID.Text);
			int DelFlag=int.Parse(this.txtDelFlag.Text);


			SanZi.Model.Project model=new SanZi.Model.Project();
			model.ProjectName=ProjectName;
			model.SubTime=SubTime;
			model.SubUID=SubUID;
			model.DelFlag=DelFlag;

			SanZi.BLL.Project bll=new SanZi.BLL.Project();
			bll.Update(model);

		}

    }
}