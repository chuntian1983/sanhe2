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
using System.Collections.Generic;

public partial class SysManage_SetSignName : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return confirm('您确定需要保存报表底部签字设置吗？')");
            Button2.Attributes.Add("onclick", "return confirm('您确定需要恢复报表底部签字默认设置吗？')");
            List<string> SignName = DefConfigs.GetReportSignNameList();
            SignName0.Text = SignName[0];
            SignName1.Text = SignName[1];
            SignName2.Text = SignName[2];
            SignName3.Text = SignName[3];
            SignName4.Text = SignName[4];
            IsShowDate.Checked = (SignName[5].Length != 0);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Session.Remove("ReportSignName");
        MainClass.ExecuteSQL(string.Format("update CW_ReportSignName set ReportSignName='{0}${1}${2}${3}${4}${5}' where unitid='{6}'",
            new string[] { SignName0.Text, SignName1.Text, SignName2.Text, SignName3.Text, SignName4.Text, (IsShowDate.Checked ? "1" : "0"), Session["UnitID"].ToString() }));
        ExeScript.Text = "<script>alert('报表底部签字设置成功！')</script>";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        MainClass.ExecuteSQL(string.Format("update CW_ReportSignName set ReportSignName=null where unitid='{0}'", Session["UnitID"].ToString()));
        Session.Remove("ReportSignName");
        List<string> SignName = DefConfigs.GetReportSignNameList();
        SignName0.Text = SignName[0];
        SignName1.Text = SignName[1];
        SignName2.Text = SignName[2];
        SignName3.Text = SignName[3];
        SignName4.Text = SignName[4];
        IsShowDate.Checked = (SignName[5].Length != 0);
        ExeScript.Text = "<script>alert('报表底部签字已恢复默认设置！')</script>";
    }
}
