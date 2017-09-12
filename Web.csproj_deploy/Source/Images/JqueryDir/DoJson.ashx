<%@ WebHandler Language="C#" Class="get_nodes" Debug="true" %>

//<%

using System;
using System.Web;
using System.Data;
using System.IO;
using System.Text;
using System.Web.SessionState;

public class get_nodes : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.Charset = "UTF-8";
        if (HttpContext.Current.Session["SessionFlag"] == null)
        {
            context.Response.Write("[{text:'登录已过期，请重新登录！',id:'no',classes:'file',expanded:false}]");
        }
        else
        {
            StringBuilder json = new StringBuilder();
            switch (context.Request.QueryString["t"])
            {
                case "0":
                case "1":
                case "2":
                case "4":
                    GetJsonData.GetSubjectList(context.Request.QueryString["t"], ref json);
                    break;
                case "u":
                    GetJsonData.GetJsonUnit(ref json);
                    break;
                default:
                    GetJsonData.GetSubject_Choose(ref json);
                    break;
            }
            string _json = json.ToString();
            if (_json.Length == 0 || _json == "[]")
            {
                context.Response.Write("[{text:'无',id:'no',classes:'file',expanded:false}]");
            }
            else
            {
                context.Response.Write(_json);
            }
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }
}

//%>
