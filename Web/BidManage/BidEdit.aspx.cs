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

public partial class BidManage_BidEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            switch (Request.QueryString["step"])
            {
                case "1":
                    PageTitle.InnerText = "招标项目申请";
                    break;
                case "2":
                    PageTitle.InnerText = "招标项目审批";
                    break;
                case "3":
                    PageTitle.InnerText = "预算书";
                    break;
                case "4":
                    PageTitle.InnerText = "招标文件";
                    break;
                case "5":
                    PageTitle.InnerText = "投标公告";
                    break;
                case "6":
                    PageTitle.InnerText = "参投登记";
                    break;
                case "7":
                    PageTitle.InnerText = "竞价招投标";
                    break;
                case "8":
                    PageTitle.InnerText = "中标公告";
                    break;
                case "9":
                    PageTitle.InnerText = "签订合同";
                    break;
            }
            DataTable data = MainClass.GetDataTable("select * from projects where AccountID='" + UserInfo.AccountID + "' order by id");
            foreach (DataRow drow in data.Rows)
            {
                ProjectList.Items.Add(new ListItem(drow["ProjectName"].ToString(), drow["ID"].ToString()));
            }
            if (ProjectList.Items.Count == 0)
            {
                PageClass.UrlRedirect("请添加项目！", 3);
            }
            else
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id"].Length > 0)
                {
                    ProjectList.Text = Request.QueryString["id"];
                }
            }
            StepFlag.Value = Request.QueryString["step"];
            ProjectList.Attributes["onchange"] = "initdata()";
        }
    }
}
