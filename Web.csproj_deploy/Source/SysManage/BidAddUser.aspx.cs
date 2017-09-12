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
using System.Text.RegularExpressions;
using System.Xml;

public partial class SysManage_BidAddUser : System.Web.UI.Page
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
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (MainClass.CheckExist("cw_users", "username='" + UserName.Text + "'"))
        {
            ExeScript.Text = "<script>alert('登录名称【" + UserName.Text + "】已存在，请更换别的。')</script>";
        }
        else
        {
            /////////////////////////////////////版权校验//////////////////////////////////////
            if (!ValidateClass.ValidateUser(this.Page, UserName.Text, 0)) { return; }
            ///////////////////////////////////////////////////////////////////////////////////
            MainClass.ExecuteSQL("insert cw_users(id,unitid,realname,username,password,accountid,powers,logincounts)values('"
                + MainClass.GetRecordID("CW_Users") + "','X00001','"
                + "招投标用户','"
                + UserName.Text + "','"
                + Password.Text + "','" + TownList.SelectedValue + "','" + UserInfo.UnitID + "','0')");
            ExeScript.Text = "<script language=javascript>if(confirm('添加成功！您需要继续添加吗？')){" +
                          "location.replace('BidAddUser.aspx');}else{location.replace('BidUserManage.aspx');}</script>";
        }
    }
}
