using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using LTP.Common;

namespace SanZi.Web.zhaobiao
{
    public partial class AddProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddProject_Click(object sender, EventArgs e)
        {
            string strErr = "";
            if(this.tbxmmc.Text.Trim()=="")
            {
                strErr += "项目名称不能为空！";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string xmmc = this.tbxmmc.Text.Trim();
            SanZi.Model.xiangmu model = new SanZi.Model.xiangmu();
            model.Xmmc = xmmc;
            SanZi.BLL.ZhaoBiao bll = new SanZi.BLL.ZhaoBiao();
            try
            {
                bll.Addxiangmu(model);
                LTP.Common.MessageBox.ShowAndRedirect(this.Page, "添加成功！", "ProjectManage.aspx");
            }
            catch { }
        }
    }
}
