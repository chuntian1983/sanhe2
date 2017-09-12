using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yuxi
{
    public partial class cunnew : System.Web.UI.Page
    {
        public string kid;
        public string name;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["kid"]!=null)
                {
                     kid = Request.QueryString["kid"];
                    name = ValidateClass.GetDataname(kid);
                    LinkButton1.Text = name;
                    LinkButton1.OnClientClick = "return fasle";

                    UserInfo.SessionFlag = "SessionFlag";
                    UserInfo.AccountID = kid;
                    Session["UserID"] = "000000";
                    Session["RealName"] = "000000";
                    Session["UserName"] = "000000";
                    Session["Powers"] = "000001$000002$000003$000004$000005$000006$000007$000008$000014$000009$000012$000010$000011$000015$000013$000016$";
                    Session["MyAccount"] = kid;
                    Session["UserType"] = "0";
                    Session["UnitID"] = "0";
                    Session["UnitName"] = "0";
                    string AccountDate = MainClass.GetTableValue("cw_account", "accountdate", string.Format("id='{0}'", UserInfo.AccountID));
                    if (AccountDate.Length > 0)
                    {
                        Session["isStartAccount"] = "Yes";
                    }
                    else
                    {
                        Session["isStartAccount"] = null;
                    }
                }
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            if (Request.QueryString["kid"] != null)
            {
                string kid = Request.QueryString["kid"];
                Response.Redirect("cunnew.aspx?kid=" + kid + "");
            }
        }
    }
}