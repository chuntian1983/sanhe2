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

public partial class SysManage_GetRegInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string CustomName = ValidateClass.ReadXMLNodeText("FinancialDB/RegInfo", "CustomName");
        string GetUseDate = ValidateClass.ReadXMLNodeText("FinancialDB/RegInfo", "UseDate");
        if (CustomName.Length == 0 && GetUseDate.Length == 0)
        {
            Response.Write("HasGetRegInfoSuccess!_!未注册!_!未注册");
        }
        else
        {
            Response.Write("HasGetRegInfoSuccess!_!" + CustomName + "!_!" + GetUseDate);
        }
        //string SerialNumber = ValidateClass.ValidateClient();
        //if (SerialNumber.Length == 0)
        //{
        //    Response.Write("HasGetRegInfoSuccess!_!未注册!_!未注册");
        //}
        //else
        //{
        //    CWWebValidate.Service ValidateService = new CWWebValidate.Service();
        //    string CustomName = ValidateClass.ReadXMLNodeText("FinancialDB/RegInfo", "CustomName");
        //    string GetUseDate = ValidateService.GetFieldValue(SerialNumber, "EndUseDate");
        //    if (CustomName.Length == 0 && GetUseDate.Length == 0)
        //    {
        //        Response.Write("HasGetRegInfoSuccess!_!未注册!_!未注册");
        //    }
        //    else
        //    {
        //        Response.Write("HasGetRegInfoSuccess!_!" + CustomName + "!_!" + GetUseDate);
        //    }
        //}
    }
}
