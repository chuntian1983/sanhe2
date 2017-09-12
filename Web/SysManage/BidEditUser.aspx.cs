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
using System.Xml;

public partial class SysManage_BidEditUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            TownList.Items.Add(new ListItem(ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + UserInfo.UnitID + "']", "UnitName"), UserInfo.UnitID));
            string RegFilePath = Server.MapPath("../App_Data/GrantCert.xml");
            XmlDocument xml = new XmlDocument();
            xml.Load(RegFilePath);
            XmlNodeList nodelist = xml.SelectNodes("FinancialDB/CUnits[ParentID='" + UserInfo.UnitID + "']");
            foreach (XmlNode node in nodelist)
            {
                TownList.Items.Add(new ListItem(node.ChildNodes[2].InnerText, node.ChildNodes[0].InnerText));
            }
            InitWebControl();
        }
    }
    protected void InitWebControl()
    {
        DataRow row = MainClass.GetDataRow("select * from cw_users where id='" + Request.QueryString["id"] + "'");
        UserName.Text = row["UserName"].ToString();
        ListItem town = TownList.Items.FindByValue(row["accountid"].ToString());
        if (town != null)
        {
            TownList.Text = row["accountid"].ToString();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (MainClass.CheckExist("cw_users", "username='" + UserName.Text + "' and id<>'" + Request.QueryString["id"] + "'"))
        {
            ExeScript.Text = "<script>alert('登录名称【" + UserName.Text + "】已存在，请更换别的。')</script>";
        }
        else
        {
            if (Password.Text.Length == 0)
            {
                MainClass.ExecuteSQL("update cw_users set username='" + UserName.Text + "',accountid='" + TownList.SelectedValue + "' where id='" + Request.QueryString["id"] + "'");
            }
            else
            {
                MainClass.ExecuteSQL("update cw_users set "
                    + "username='" + UserName.Text + "',"
                    + "password='" + Password.Text + "',accountid='" + TownList.SelectedValue + "' where id='" + Request.QueryString["id"] + "'");
            }
            ExeScript.Text = "<script>alert('数据保存成功！')</script>";
        }
        InitWebControl();
    }
}
