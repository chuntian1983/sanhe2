<%@ WebHandler Language="C#" Class="_upload" %>

using System;
using System.Web;
using System.Data;
using System.IO;
using System.Text;
using System.Web.SessionState;

public class _upload : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
{
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "UTF-8";
        HttpRequest request = context.Request;
        if (request.QueryString["act"] == "0")
        {
            string filename = Path.GetFileName(request.Files[0].FileName);
            string filepath = "/UploadFile/ztb/" + Guid.NewGuid().ToString("N") + Path.GetExtension(request.Files[0].FileName);
            request.Files[0].SaveAs(context.Server.MapPath(filepath));
            MainClass.ExecuteSQL(string.Concat("insert into projattachs(ProjectID,StepFlag,FileName,FilePath,UploadTime)values('",
                request.QueryString["pid"], "','",
                request.QueryString["step"], "','",
                filename, "','",
                filepath, "','", DateTime.Now.ToString(), "')"));
            context.Response.Write("1");
        }
        if (context.Request.QueryString["act"] == "1")
        {
            //删除队列
        }
        if (context.Request.QueryString["act"] == "2")
        {
            string appendixid = CommClass.GetRecordID("Appendix");
            string ext = Path.GetExtension(request.Files[0].FileName);
            string fname = DateTime.Now.ToString("yyyyMMddHHmmss" + appendixid);
            string fileName = string.Concat("../UploadFile/Appendices/", fname, ext);
            string fileThum = string.Concat("../UploadFile/Appendices/", fname, "_thum", ext);
            request.Files[0].SaveAs(context.Server.MapPath(fileName));
            UtilsComm.MakeThumbnail(context.Server.MapPath(fileName), context.Server.MapPath(fileThum), 90, 90);
            CommClass.ExecuteSQL(string.Concat("insert into cw_syspara(ParaName,ParaValue,DefValue,ParaType,DefPara1)values('Appendix",
                appendixid, "','", fileName, "','", fileThum, "','1','", context.Request.QueryString["ym"], "')"));
        }
        
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
}