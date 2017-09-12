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

public partial class BidManage_FileShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string title = "项目名称：" + MainClass.GetTableValue("projects", "ProjectName", "id='" + Request.QueryString["pid"] + "'", "");
            QSubjectType.Value = Request.QueryString["step"];
            switch (QSubjectType.Value)
            {
                case "0":
                    title += "—村民代表会议";
                    break;
                case "1":
                    title += "—招标项目申请";
                    break;
                case "2":
                    title += "—招标项目审批";
                    break;
                case "3":
                    title += "—预算书";
                    break;
                case "4":
                    title += "—招标文件";
                    break;
                case "5":
                    title += "—投标公告";
                    break;
                case "6":
                    title += "—参投登记";
                    break;
                case "7":
                    title += "—竞价招投标";
                    break;
                case "8":
                    title += "—中标公告";
                    break;
                case "9":
                    title += "—签订合同";
                    break;
                case "10":
                    title += "—项目管理";
                    break;
                default:
                    QSubjectType.Value = "0";
                    menus.Visible = true;
                    break;
            }
            PageTitle.InnerHtml = title;
            DoPostBack_Click(DoPostBack, null);
        }
    }

    protected void DoPostBack_Click(object sender, EventArgs e)
    {
        ((HtmlTableCell)this.FindControl("M" + OSubjectType.Value)).BgColor = "";
        ((HtmlTableCell)this.FindControl("M" + QSubjectType.Value)).BgColor = "#d4ecfc";
        OSubjectType.Value = QSubjectType.Value;
        string QueryString = "$ProjectID='" + Request.QueryString["pid"] + "'$StepFlag='" + QSubjectType.Value + "'";
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        DataTable data = MainClass.GetDataTable("select FilePath from projattachs " + QueryString + " order by id");
        AppendixList.DataSource = data.DefaultView;
        AppendixList.DataBind();
    }
}
