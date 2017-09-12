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

public partial class ResManage_ResourceCardModify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000016")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            DelFile.Attributes.Add("onclick", "return confirm('您确定需要删除附件吗？')");
            BookDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            BookDate.Attributes.Add("readonly", "readonly");
            BookDate.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].BookDate,'yyyy-mm-dd')");
            ClassID.Attributes.Add("readonly", "readonly");
            ClassName.Attributes.Add("readonly", "readonly");
            ClassID.Attributes.Add("onclick", "SelectItem(0)");
            ClassName.Attributes.Add("onclick", "SelectItem(0)");
            DeptName.Attributes.Add("readonly", "readonly");
            DeptName.Attributes.Add("onclick", "SelectItem(1)");
            CardNo.Text = "[自动编号]";
            ResNo.Text = "[自动编号]";
            CardNo.Attributes.Add("onclick", "if(this.value=='[自动编号]')this.value='';");
            ResNo.Attributes.Add("onclick", "if(this.value=='[自动编号]')this.value='';");
            CardNo.Attributes.Add("onblur", "if(this.value.length==0)this.value='[自动编号]';");
            ResNo.Attributes.Add("onblur", "if(this.value.length==0)this.value='[自动编号]';");
            UtilsPage.SetTextBoxAutoValue(RelateFarmers, "0");
            UtilsPage.SetTextBoxAutoValue(ResAmount, "0");
            InitWebControl();
            InitModifyLog();
        }
    }
    private void InitWebControl()
    {
        //初始化控件值
        DataRow row = CommClass.GetDataRow("select * from cw_rescard where id='" + Request.QueryString["id"] + "'");
        OldCardNo.Value = row["CardNo"].ToString();
        CardNo.Text = row["CardNo"].ToString();
        ResNo.Text = row["ResNo"].ToString();
        ResName.Text = row["ResName"].ToString();
        ClassID.Text = row["ClassID"].ToString();
        ClassName.Text = row["ClassName"].ToString();
        ResUnit.Text = row["ResUnit"].ToString();
        ResAmount.Text = row["ResAmount"].ToString();
        HasAmount.Value = row["HasAmount"].ToString();
        DeptName.Text = row["DeptName"].ToString();
        Locality.Text = row["Locality"].ToString();
        RelateFarmers.Text = row["RelateFarmers"].ToString();
        BorderE.Text = row["BorderE"].ToString();
        BorderW.Text = row["BorderW"].ToString();
        BorderS.Text = row["BorderS"].ToString();
        BorderN.Text = row["BorderN"].ToString();
        ResUsage.Text = row["ResUsage"].ToString();
        UsedState.Text = row["UsedState"].ToString();
        BookType.Text = row["BookType"].ToString();
        Notes.Text = row["Notes"].ToString();
        ShowFile.NavigateUrl = row["ResPicture"].ToString();
        DelFile.Enabled = ShowFile.NavigateUrl.Length != 0;
        try
        {
            for (int k = 4; k <= 18; k++)
            {
                TextBox tbox = (TextBox)this.Page.FindControl("name" + k.ToString());
                tbox.Text = row["name" + k.ToString()].ToString();
            }
        }
        catch { }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (CardNo.Text != OldCardNo.Value && CommClass.CheckExist("CW_ResCard", "CardNo='" + CardNo.Text + "'"))
        {
            ExeScript.Text = "<script>alert('该卡片编号[" + CardNo.Text + "]已存在！');</script>";
            return;
        }
        string uploadFile = UtilsPage.UploadFiles();
        Dictionary<string, string> ResCard = new Dictionary<string, string>();
        ResCard.Add("CardNo", CardNo.Text);
        ResCard.Add("ResNo", ResNo.Text);
        ResCard.Add("ResName", ResName.Text);
        ResCard.Add("ClassID", ClassID.Text);
        ResCard.Add("ClassName", ClassName.Text);
        ResCard.Add("ResUnit", ResUnit.Text);
        ResCard.Add("ResAmount", ResAmount.Text);
        ResCard.Add("DeptName", DeptName.Text);
        ResCard.Add("Locality", Locality.Text);
        ResCard.Add("RelateFarmers", RelateFarmers.Text);
        ResCard.Add("BorderE", BorderE.Text);
        ResCard.Add("BorderW", BorderW.Text);
        ResCard.Add("BorderS", BorderS.Text);
        ResCard.Add("BorderN", BorderN.Text);
        ResCard.Add("ResUsage", ResUsage.Text);
        ResCard.Add("UsedState", UsedState.SelectedValue);
        ResCard.Add("Notes", Notes.Text);
        ResCard.Add("BookType", BookType.SelectedValue);
        ResCard.Add("ModifyDate", BookDate.Text);
        if (uploadFile.Length != 0)
        {
            ShowFile.NavigateUrl = uploadFile;
            ResCard.Add("ResPicture", uploadFile);
        }
        try
        {
            DataTable table = CommClass.GetDataTable("select * from cw_rescard where 1=2");
            if (table.Columns.Contains("name4") == false)
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("alter table cw_rescard ");
                for (int k = 4; k <= 18; k++)
                {
                    sql.AppendFormat("add name{0} varchar(255) default NULL,", k.ToString());
                }
                sql.Remove(sql.Length - 1, 1);
                CommClass.ExecuteSQL(sql.ToString());
            }
            for (int k = 4; k <= 18; k++)
            {
                TextBox tbox = (TextBox)this.Page.FindControl("name" + k.ToString());
                ResCard.Add("name" + k.ToString(), tbox.Text);
            }
        }
        catch { }
        CommClass.ExecuteSQL("CW_ResCard", ResCard, "id='" + Request.QueryString["id"] + "'");
        DelFile.Enabled = ShowFile.NavigateUrl.Length != 0;
        ExeScript.Text = "<script>alert('资源卡片保存成功！');</script>";
        //写入操作日志
        CommClass.WriteCTL_Log("100018", "资源变更，编号：" + CardNo.Text);
        //变更记录
        if (ChangeType.SelectedValue != "选择项目" && ChangeNotes.Text.Length > 0)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("id", CommClass.GetRecordID("CW_Logs"));
            dic.Add("logcontent", ChangeNotes.Text);
            dic.Add("loguser", UserInfo.UserName);
            dic.Add("logname", UserInfo.RealName);
            dic.Add("loguid", "XXXXXX");
            dic.Add("logpid", "XXXXXX");
            dic.Add("logtime", DateTime.Now.ToString());
            dic.Add("logdefine1", "000031-" + Request.QueryString["id"]);
            dic.Add("logdefine2", HttpContext.Current.Request.UserHostAddress);
            dic.Add("logdefine3", ChangeType.SelectedValue);
            CommClass.ExecuteSQL("cw_logs", dic);
            ChangeType.SelectedIndex = 0;
            ChangeNotes.Text = "";
        }
        InitModifyLog();
    }
    protected void DelFile_Click(object sender, EventArgs e)
    {
        string filePath = Server.MapPath(ShowFile.NavigateUrl);
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }
        CommClass.ExecuteSQL("update cw_rescard set ResPicture='' where id='" + Request.QueryString["id"] + "'");
        DelFile.Enabled = false;
        ShowFile.NavigateUrl = "";
    }
    protected void InitModifyLog()
    {
        DataSet ds = CommClass.GetDataSet(string.Concat("select * from cw_logs ", "where logdefine1='000031-", Request.QueryString["id"], "' order by id desc"));
        if (ds.Tables[0].Rows.Count == 0)
        {
            PageClass.BindNoRecords(GridView1, ds);
        }
        else
        {
            GridView1.DataSource = ds.Tables[0].DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
        }
    }
}
