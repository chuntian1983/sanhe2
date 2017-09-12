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
using Telerik.WebControls;
using System.Text;

public partial class FinanceFlow_ProjectInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000003")) { return; }
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            lbnUploadFile0.Attributes.Add("onclick", "return UploadFile(0);");
            lbnUploadFile1.Attributes.Add("onclick", "return UploadFile(1);");
            lbnUploadFile2.Attributes.Add("onclick", "return UploadFile(2);");
            lbnUploadFile3.Attributes.Add("onclick", "return UploadFile(3);");
            lbnUploadFile4.Attributes.Add("onclick", "return UploadFile(4);");
            UtilsPage.SetTextBoxAutoValue(ProjectBudget, "0");
            UtilsPage.SetTextBoxAutoValue(RealFund, "0");
            UtilsPage.SetTextBoxCalendar(AuditDate0, "");
            UtilsPage.SetTextBoxCalendar(AuditDate1, "");
            UtilsPage.SetTextBoxCalendar(AuditDate2, "");
            UtilsPage.SetTextBoxCalendar(AuditDate3, "");
            UtilsPage.SetTextBoxCalendar(AuditDate4, "");
            CommClass.InitSysPara(ProjectType, "100001");
            if (Request.QueryString["id"] != null && Request.QueryString["id"].Length > 0)
            {
                ProjectID.Value = Request.QueryString["id"];
                InitWebControl();
            }
            else
            {
                InitAppendices();
            }
        }
    }
    private void InitWebControl()
    {
        DataRow row = CommClass.GetDataRow(string.Concat("select * from CW_Project where id='", ProjectID.Value, "'"));
        if (row != null)
        {
            ProjectName.Text = row["ProjectName"].ToString();
            ProjectType.Text = row["ProjectType"].ToString();
            ProjectLocation.Text = row["ProjectLocation"].ToString();
            FundSource.Text = row["FundSource"].ToString();
            //--
            ProjectBudget.Text = row["ProjectBudget"].ToString();
            ProjectLeader.Text = row["ProjectLeader"].ToString();
            ProjectSupervisor.Text = row["ProjectSupervisor"].ToString();
            ProjectIntroduction.Text = row["ProjectIntroduction"].ToString();
            AuditOpinion0.Text = row["AuditOpinion0"].ToString();
            AuditDate0.Text = row["AuditDate0"].ToString();
            AuditOpinion1.Text = row["AuditOpinion1"].ToString();
            AuditDate1.Text = row["AuditDate1"].ToString();
            //--
            RealFund.Text = row["RealFund"].ToString();
            CompletionNotes.Text = row["CompletionNotes"].ToString();
            AuditOpinion2.Text = row["AuditOpinion2"].ToString();
            AuditDate2.Text = row["AuditDate2"].ToString();
            AuditOpinion3.Text = row["AuditOpinion3"].ToString();
            AuditDate3.Text = row["AuditDate3"].ToString();
            AuditOpinion4.Text = row["AuditOpinion4"].ToString();
            AuditDate4.Text = row["AuditDate4"].ToString();
            //--
            Appendices.Value = row["Appendices"].ToString();
            string[] afiles = Appendices.Value.Split('#');
            if (afiles.Length == 6)
            {
                Appendices0.Value = afiles[0];
                Appendices1.Value = afiles[1];
                Appendices2.Value = afiles[2];
                Appendices3.Value = afiles[3];
                Appendices4.Value = afiles[4];
                Appendices5.Value = afiles[5];
            }
            InitAppendices();
        }
    }
    protected void InitAppendices()
    {
        //附件初始化
        DataTable files = new DataTable();
        files.Columns.Add("id", typeof(int));
        files.Columns.Add("delid");
        files.Columns.Add("url");
        int index = 1;
        string[] afiles = Appendices.Value.Replace("#", "").Split('$');
        foreach (string file in afiles)
        {
            if (file.Length > 0)
            {
                DataRow frow = files.NewRow();
                frow["id"] = (index++).ToString("000");
                frow["delid"] = file;
                if (file.StartsWith("Appendix"))
                {
                    frow["url"] = CommClass.GetSysPara(file);
                }
                else
                {
                    frow["url"] = file;
                }
                files.Rows.Add(frow);
            }
        }
        if (files.Rows.Count == 0)
        {
            PageClass.BindNoRecords(GridView1, files);
        }
        else
        {
            GridView1.DataSource = files.DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
        }
        PageClass.ExcuteScript(this.Page, "TableSelect($('ShowFlag').value)");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        bool isnew = (ProjectID.Value == "000000");
        Dictionary<string, string> sql = new Dictionary<string, string>();
        //基本信息
        if (isnew)
        {
            ProjectID.Value = CommClass.GetRecordID("CW_Project");
            sql.Add("ID", ProjectID.Value);
            sql.Add("CreateDate", DateTime.Now.ToString());
        }
        sql.Add("ProjectName", ProjectName.Text);
        sql.Add("ProjectType", ProjectType.SelectedValue);
        sql.Add("ProjectLocation", ProjectLocation.Text);
        sql.Add("FundSource", FundSource.Text);
        //预算审批表
        sql.Add("ProjectBudget", ProjectBudget.Text);
        sql.Add("ProjectLeader", ProjectLeader.Text);
        sql.Add("ProjectSupervisor", ProjectSupervisor.Text);
        sql.Add("ProjectIntroduction", ProjectIntroduction.Text);
        sql.Add("AuditOpinion0", AuditOpinion0.Text);
        sql.Add("AuditDate0", AuditDate0.Text);
        sql.Add("AuditOpinion1", AuditOpinion1.Text);
        sql.Add("AuditDate1", AuditDate1.Text);
        //竣工备案表
        sql.Add("RealFund", RealFund.Text);
        sql.Add("CompletionNotes", CompletionNotes.Text);
        sql.Add("AuditOpinion2", AuditOpinion2.Text);
        sql.Add("AuditDate2", AuditDate2.Text);
        sql.Add("AuditOpinion3", AuditOpinion3.Text);
        sql.Add("AuditDate3", AuditDate3.Text);
        sql.Add("AuditOpinion4", AuditOpinion4.Text);
        sql.Add("AuditDate4", AuditDate4.Text);
        //附件管理
        Appendices.Value = string.Concat(Appendices0.Value, "#", Appendices1.Value, "#", Appendices2.Value, "#", Appendices3.Value, "#", Appendices4.Value, "#", Appendices5.Value);
        sql.Add("Appendices", Appendices.Value);
        if (isnew)
        {
            CommClass.ExecuteSQL("CW_Project", sql);
        }
        else
        {
            CommClass.ExecuteSQL("CW_Project", sql, string.Concat("id='", ProjectID.Value, "'"));
        }
        InitAppendices();
        PageClass.ShowAlertMsg(this.Page, "工程信息保存成功！");
    }
    private string UploadFiles()
    {
        try
        {
            StringBuilder fileList = new StringBuilder();
            foreach (UploadedFile file in RadUploadContext.Current.UploadedFiles)
            {
                string fileName = "../UploadFile/" + string.Format(("{0:yyyyMMddHHmmssfff}"), DateTime.Now) + file.GetExtension();
                file.SaveAs(HttpContext.Current.Server.MapPath(fileName));
                fileList.Append(fileName);
            }
            return fileList.ToString();
        }
        catch
        {
            return "";
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string filePath = Server.MapPath(btn.CommandArgument);
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }
        string delID = btn.CommandArgument + "$";
        Appendices.Value = Appendices.Value.Replace(delID, "");
        Appendices0.Value = Appendices0.Value.Replace(delID, "");
        Appendices1.Value = Appendices1.Value.Replace(delID, "");
        Appendices2.Value = Appendices2.Value.Replace(delID, "");
        Appendices3.Value = Appendices3.Value.Replace(delID, "");
        Appendices4.Value = Appendices4.Value.Replace(delID, "");
        Appendices5.Value = Appendices5.Value.Replace(delID, "");
        InitAppendices();
    }
    protected void btnUploadFile_Click(object sender, EventArgs e)
    {
        string ufile = UploadFiles();
        Appendices.Value += ufile + "$";
        Appendices5.Value += ufile + "$";
        InitAppendices();
    }
}
