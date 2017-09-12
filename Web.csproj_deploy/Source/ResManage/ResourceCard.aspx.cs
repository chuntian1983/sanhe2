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

public partial class ResManage_ResourceCard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000016")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
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
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (CommClass.CheckExist("CW_ResCard", "CardNo='" + CardNo.Text + "'"))
        {
            ExeScript.Text = "<script>alert('该卡片编号[" + CardNo.Text + "]已存在！');</script>";
            return;
        }
        if (CardNo.Text == "[自动编号]" || CardNo.Text.Length == 0)
        {
            CardNo.Text = CommClass.GetRecordID("CW_ResCard");
        }
        if (ResNo.Text == "[自动编号]" || ResNo.Text.Length == 0)
        {
            ResNo.Text = CommClass.GetRecordID("CW_ResNo");
        }
        string uploadFile = UtilsPage.UploadFiles();
        Dictionary<string, string> ResCard = new Dictionary<string, string>();
        ResCard.Add("ID", CommClass.GetRecordID("CW_ResourceID"));
        ResCard.Add("CardNo", CardNo.Text);
        ResCard.Add("ResNo", ResNo.Text);
        ResCard.Add("ResName", ResName.Text);
        ResCard.Add("ClassID", ClassID.Text);
        ResCard.Add("ClassName", ClassName.Text);
        ResCard.Add("ResUnit", ResUnit.Text);
        ResCard.Add("ResAmount", ResAmount.Text);
        ResCard.Add("HasAmount", "0");
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
        ResCard.Add("BookTime", DateTime.Now.ToString());
        ResCard.Add("BookDate", BookDate.Text);
        ResCard.Add("ResPicture", uploadFile);
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
        CommClass.ExecuteSQL("CW_ResCard", ResCard);
        ExeScript.Text = "<script>alert('资源卡片录入成功！');location.href='ResourceCard.aspx';</script>";
        //写入操作日志
        CommClass.WriteCTL_Log("100018", "录入资源卡片，编号：" + CardNo.Text);
        //--
    }
}
