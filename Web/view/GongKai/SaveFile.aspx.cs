using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SanZi.Web.GongKai
{
    public partial class SaveFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Files.Count > 0)
                {
                    string strFileName = "";//新文件名称
                    string strTitle = "";//标题
                    int intID = 0;//ID
                    string strDeptName = "";//部门名称
                    int intDeptID = 0;//部门ID
                    string strOldFileName = "";//旧文件名称
                    int uid = 0;
                    string lbid = string.Empty;
                    strTitle = HttpUtility.UrlDecode(Request.Params["DocTitle"].Trim().Replace("'", ""));//标题
                    intID = int.Parse(Request.Params["DocID"].Trim().Replace("'", ""));//ID
                    strDeptName = HttpUtility.UrlDecode(Request.Params["DeptName"].Trim().Replace("'", ""));//部门名称
                    intDeptID = int.Parse(Request.Params["DeptID"].Trim().Replace("'", ""));//部门ID
                    strOldFileName = HttpUtility.UrlDecode(Request.Params["FileName"].Trim().Replace("'", ""));//旧文件名称

                    try
                    {
                        int.TryParse(Request.Params["lbid"].Trim().Replace("'", ""), out uid);
                    }
                    catch { }

                    strFileName = System.Guid.NewGuid().ToString() + ".doc";
                    Request.Files[0].SaveAs(Server.MapPath("../GongKai/File/") + strFileName);
                    SanZi.BLL.CunWuGongKai bll = new SanZi.BLL.CunWuGongKai();
                    if (intID == 0)
                    {
                        bll.Add(strTitle, intDeptID, strDeptName, strFileName, uid);
                    }
                    else
                    {
                        bll.Update(intID, strTitle, intDeptID, strDeptName, strFileName);
                    }
                    Response.Write("succeed");
                    Response.End();
                }
            }
        }
    }
}
