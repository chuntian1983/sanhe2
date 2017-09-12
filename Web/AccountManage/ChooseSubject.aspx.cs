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

public partial class AccountManage_ChooseSubject : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            Overlay.Attributes["style"] = "display:none";
            Lightbox.Attributes["style"] = "display:none";
            LeadV.Attributes.Add("onkeypress", "return (event.keyCode>=48&&event.keyCode<=57)||event.keyCode==46;");
            OnloanV.Attributes.Add("onkeypress", "return (event.keyCode>=48&&event.keyCode<=57)||event.keyCode==46;");
            LeadV.Attributes.Add("onkeyup", "$$('OnloanV').value='';");
            OnloanV.Attributes.Add("onkeyup", "$$('LeadV').value='';");
            QSubjectName.Attributes.Add("onkeyup", "$$('QSubjectNo').value='';");
            QSubjectNo.Attributes.Add("onkeyup", "$$('QSubjectName').value='';");
            //--
            SelectSubject.Attributes["readonly"] = "readonly";
            string subjectNo = Request.QueryString["no"];
            if (string.IsNullOrEmpty(subjectNo) == false && subjectNo.Length > 0)
            {
                NodeName0.Value = CommClass.GetFieldFromNo(subjectNo.Substring(0, 3), "SubjectName");
                NodeName1.Value = subjectNo.Length > 3 ? CommClass.GetDetailSubject(subjectNo) : "";
                NodeValue.Value = subjectNo;
            }
            //--
            if (Request.QueryString["money"].ToString().IndexOf("-") != -1)
            {
                LeadV.Text = "";
                OnloanV.Text = Request.QueryString["money"].ToString().Replace("-", "").Trim();
            }
            else
            {
                LeadV.Text = Request.QueryString["money"].ToString().Trim();
                OnloanV.Text = "";
            }
            DataSet ds = CommClass.GetDataSet("select id,groupname from cw_subjectgroup");
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                SubjectGroup.Items.Add(new ListItem(row["groupname"].ToString(), row["id"].ToString()));
            }
        }
        HtmlTableCell cell0 = ((HtmlTableCell)this.FindControl("M" + OSubjectType.Value));
        HtmlTableCell cell1 = ((HtmlTableCell)this.FindControl("M" + QSubjectType.Value));
        cell0.Style["color"] = "black";
        cell1.Style["color"] = "blue";
        cell0.Style["text-decoration"] = "none";
        cell1.Style["text-decoration"] = "underline";
        cell0.Style["background-color"] = "";
        cell1.Style["background-color"] = "#f8f8f8";
        OSubjectType.Value = QSubjectType.Value;
    }


    protected void btnDoPostBack_Click(object sender, EventArgs e)
    {
        //--
    }
}
