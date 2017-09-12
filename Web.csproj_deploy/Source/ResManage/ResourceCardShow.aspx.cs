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

public partial class ResManage_ResourceCardShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["aid"]))
        {
            if (!PageClass.CheckVisitQuot("000016")) { return; }
        }
        else
        {
            UserInfo.CheckSession2();
            UserInfo.AccountID = Request.QueryString["aid"];
        }
        if (!IsPostBack)
        {
            //初始化控件值
            DataRow row = CommClass.GetDataRow("select * from cw_rescard where id='" + Request.QueryString["id"] + "'");
            CardNo.Text = row["CardNo"].ToString();
            BookDate.Text = row["BookDate"].ToString();
            ResNo.Text = row["ResNo"].ToString();
            ResName.Text = row["ResName"].ToString();
            ClassID.Text = row["ClassID"].ToString();
            ClassName.Text = row["ClassName"].ToString();
            ResUnit.Text = row["ResUnit"].ToString();
            ResAmount.Text = row["ResAmount"].ToString();
            ResModel.Text = row["ResModel"].ToString();
            DeptName.Text = row["DeptName"].ToString();
            Locality.Text = row["Locality"].ToString();
            RelateFarmers.Text = row["RelateFarmers"].ToString();
            ResUsage.Text = row["ResUsage"].ToString();
            BorderE.Text = row["BorderE"].ToString();
            BorderW.Text = row["BorderW"].ToString();
            BorderS.Text = row["BorderS"].ToString();
            BorderN.Text = row["BorderN"].ToString();
            switch (row["UsedState"].ToString())
            {
                case "0":
                    UsedState.Text = "使用中";
                    break;
                case "1":
                    UsedState.Text = "未使用";
                    break;
                case "2":
                    UsedState.Text = "已荒废";
                    break;
            }
            BookType.Text = row["BookType"].ToString();
            ResPicture.ImageUrl = row["ResPicture"].ToString();
            if (ResPicture.ImageUrl.Length == 0)
            {
                DivAPicture.Visible = false;
            }
            DeptName.Text = DeptName.Text.Substring(DeptName.Text.IndexOf(".") + 1);
            try
            {
                for (int k = 4; k <= 18; k++)
                {
                    Label tbox = (Label)this.Page.FindControl("name" + k.ToString());
                    tbox.Text = row["name" + k.ToString()].ToString() + "&nbsp;";
                }
            }
            catch { }
        }
    }
}
