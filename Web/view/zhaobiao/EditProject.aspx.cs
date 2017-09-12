using System;
using System.Data;
using System.Web.UI;

namespace SanZi.Web.zhaobiao
{
    public partial class EditProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    string strID = Request.Params["id"].Trim();
                    ShowInfo(strID);
                }
            }
        }

        private void ShowInfo(string strID)
        {
            int id = int.Parse(strID);
            SanZi.BLL.ZhaoBiao bll = new SanZi.BLL.ZhaoBiao();
            DataTable dt = bll.getProjectByID(id);

            this.tbxmmc.Text = dt.Rows[0]["xmmc"].ToString();
            this.hidProjectID.Value = dt.Rows[0]["id"].ToString();

            dt.Clear();
        }

        protected void btnAddProject_Click(object sender, EventArgs e)
        {
            string strProjectName = this.tbxmmc.Text.Trim().Replace("'", "");
            int pid = int.Parse(this.hidProjectID.Value.Trim());

            SanZi.BLL.ZhaoBiao bll = new SanZi.BLL.ZhaoBiao();
            bll.UpdateProject(pid,strProjectName);

            LTP.Common.MessageBox.ShowAndRedirect(this.Page, "修改成功", "ProjectManage.aspx");
        }
    }
}
