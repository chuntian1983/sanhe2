using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using Telerik.WebControls;

/// <summary>
/// 进程校验类
/// </summary>
public class @HttpModuleDo : IHttpModule
{
    void @IHttpModule.Init(HttpApplication context)
    {
        context.AcquireRequestState += new EventHandler(context_AcquireRequestState);
    }

    void context_AcquireRequestState(object sender, EventArgs e)
    {
        HttpApplication application = (HttpApplication)sender;
        HttpContext context = application.Context;
        string errShowPageUrl = SysConfigs.ErrShowPageUrl;
        if (Regex.IsMatch(context.Request.Path, string.Concat(".*/(", SysConfigs.ForbidFolders, ")/.*"), RegexOptions.IgnoreCase))
        {
            context.Response.Redirect(errShowPageUrl, true);
            return;
        }
        string forbidFiles = string.Concat(".*\\.(", SysConfigs.ForbidExtensions, ")");
        if (RadUploadContext.Current == null)
        {
            for (int i = 0; i < context.Request.Files.Count; i++)
            {
                if (Regex.IsMatch(context.Request.Files[i].FileName, forbidFiles, RegexOptions.IgnoreCase))
                {
                    context.Response.Redirect(errShowPageUrl, true);
                    return;
                }
            }
        }
        else
        {
            foreach (UploadedFile file in RadUploadContext.Current.UploadedFiles)
            {
                if (Regex.IsMatch(file.FileName, forbidFiles, RegexOptions.IgnoreCase))
                {
                    context.Response.Redirect(errShowPageUrl, true);
                    return;
                }
            }
        }
    }

    void @IHttpModule.Dispose()
    {

    }
}
