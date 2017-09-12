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

public partial class SysManage_EditGatherLevel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            Overlay.Attributes["style"] = "display:none";
            Lightbox.Attributes["style"] = "display:none";
            Button1.Attributes.Add("onclick", "$('PostFlag').value='1';return CheckSubmit();");
            DataRow row = MainClass.GetDataRow("select LevelName,CollectParent,CollectUnits from cw_collectlevel where id='" + Request.QueryString["id"] + "'");
            if (row != null)
            {
                LevelName.Text = row["LevelName"].ToString();
                CollectParent.Value = row["CollectParent"].ToString();
                string collectUnits = row["CollectUnits"].ToString();
                string[] g = collectUnits.Replace("$-", "-").Split('-');
                collectUnits = string.Concat(g[1], g[0].Replace("$", "$_"));
                SelectNodes.Value = collectUnits.Replace("$", "c") + CollectParent.Value;
            }
            TotalLevel.Value = ValidateClass.ReadXMLNodeText("FinancialDB/RegInfo", "LastLevel");
            string UnitID = Session["UnitID"].ToString();
            if (UnitID == "000000")
            {
                string topUnitID = ValidateClass.ReadXMLNodeText("FinancialDB/RegInfo", "TopUnitID");
                if (topUnitID.Length == 0)
                {
                    topUnitID = "000000";
                }
                UnitID = topUnitID;
            }
            TopUnitID.Value = UnitID;
            rootNode.InnerHtml = "&nbsp;" + ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + TopUnitID.Value + "']", "UnitName");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (MainClass.CheckExist("cw_collectlevel", string.Format("LevelName='{0}' and id<>'{1}' and unitid='{2}'", LevelName.Text, Request.QueryString["id"], Session["UnitID"].ToString())))
        {
            ExeScript.Text = "<script>alert('二级汇总单位【" + LevelName.Text + "】已存在，请更换别的。')</script>";
        }
        else
        {
            StringBuilder CollectUnits = new StringBuilder("-");
            string[] selNodes = SelectNodes.Value.Split('c');
            foreach (string n in selNodes)
            {
                if (n.Length > 0)
                {
                    if (n.StartsWith("_"))
                    {
                        CollectUnits.Insert(0, string.Concat("$", n.Substring(1)));
                    }
                    else
                    {
                        CollectUnits.AppendFormat("${0}", n);
                    }
                }
            }
            MainClass.ExecuteSQL("update cw_collectlevel set "
                + "LevelName='" + LevelName.Text + "',"
                + "CollectParent='" + CollectParent.Value + "',"
                + "CollectUnits='" + CollectUnits.ToString() + "' "
                + " where id='" + Request.QueryString["id"] + "'");
            //--
            SelectNodes.Value = SelectNodes.Value + CollectParent.Value;
            ExeScript.Text = "<script>alert('数据保存成功！')</script>";
        }
    }
}
