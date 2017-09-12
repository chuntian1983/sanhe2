using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace SanZi.Web.view.Users
{
    public partial class checkprint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int bt = TypeParse.StrToInt(ConfigurationManager.AppSettings["BarPrintType"], 0);
                if (bt == 0 || CommClass.GetSysPara("hascheckprint") == "1")
                {
                    Response.Redirect("barcode.aspx", true);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string cname = ValidateClass.ReadXMLNodeText("FinancialDB/RegInfo", "CustomName");
            string ccode = PageClass.Hash_MD5(cname).Substring(0, 6).ToLower();
            if (TextBox1.Text == ccode)
            {
                CommClass.SetSysPara("hascheckprint", "1");
                Response.Redirect("barcode.aspx", true);
            }
            else
            {
                PageClass.ShowAlertMsg(this.Page, "打印密码错误，请核实！");
            }
        }
    }
}