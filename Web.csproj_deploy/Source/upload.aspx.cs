using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace SanZi.Web
{
    public partial class upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            try
            {
                    ///'检查文件扩展名字
                    HttpPostedFile postedFile = Request.Files[0];
                    string fileName2;
                    fileName2 = System.IO.Path.GetFileName(postedFile.FileName);
                    if (fileName2 != "")
                    {

                        ///注意：可能要修改你的文件夹的匿名写入权限。
                       // postedFile.SaveAs(System.Web.HttpContext.Current.Request.MapPath("/UploadFile/Appendices/") + fileName2);
                        string aid = Request.QueryString["aid"];
                        UserInfo.AccountID = aid;
                        UserInfo.SessionFlag = "1";
                        string appendixid = CommClass.GetRecordID("Appendix");
                        string ext = Path.GetExtension(fileName2);
                        string fname = DateTime.Now.ToString("yyyyMMddHHmmss" + appendixid);
                        string fileName = string.Concat("UploadFile/Appendices/", fname, ext);
                        string fileThum = string.Concat("UploadFile/Appendices/", fname, "_thum", ext);
                        postedFile.SaveAs(Server.MapPath(fileName));
                        UtilsComm.MakeThumbnail(Server.MapPath(fileName), Server.MapPath(fileThum), 90, 90);
                        fileName = "../" + fileName;
                        fileThum = "../" + fileThum;
                        CommClass.ExecuteSQL(string.Concat("insert into cw_syspara(ParaName,ParaValue,DefValue,ParaType,DefPara1)values('Appendix",
                            appendixid, "','", fileName, "','", fileThum, "','1','", Request.QueryString["ym"], "')"));
                    }
                

            }
            catch (System.Exception Ex)
            {
                
                System.Console.WriteLine(Ex.Message);
            }


        }
    }
}