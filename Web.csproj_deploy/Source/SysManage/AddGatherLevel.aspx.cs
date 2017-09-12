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

public partial class SysManage_AddGatherLevel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            Overlay.Attributes["style"] = "display:none";
            Lightbox.Attributes["style"] = "display:none";
            Button1.Attributes.Add("onclick", "$('PostFlag').value='1';return CheckSubmit();");
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
        if (MainClass.CheckExist("cw_collectlevel", string.Format("LevelName='{0}' and unitid='{1}'", LevelName.Text, Session["UnitID"].ToString())))
        {
            ExeScript.Text = "<script>alert('二级汇总单位【" + LevelName.Text + "】已存在，请更换别的。')</script>";
        }
        else
        {
            StringBuilder CollectUnits = new StringBuilder("$-");
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
            MainClass.ExecuteSQL("insert cw_collectlevel(id,unitid,levelname,collectparent,collectunits)values('"
                + MainClass.GetRecordID("CW_CollectLevel") + "','"
                + Session["UnitID"].ToString() + "','"
                + LevelName.Text + "','"
                + CollectParent.Value + "','"
                + CollectUnits.ToString() + "')");
            ExeScript.Text = "<script language=javascript>if(confirm('添加成功！您需要继续添加吗？')){" +
                          "location.replace('AddGatherLevel.aspx');}else{location.replace('GatherLevel.aspx');}</script>";
        }
    }
}
