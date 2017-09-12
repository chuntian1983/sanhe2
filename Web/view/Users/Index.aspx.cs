using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.IO;
using System.Runtime.InteropServices;

namespace SanZi.Web.Users
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Overlay.Attributes["style"] = "display:none";
                Lightbox.Attributes["style"] = "display:none";

                getUsersList(string.Empty);
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    string strUID = Request.Params["id"].Trim().Replace("'", "");
                    string strAction = Request.QueryString["act"].Trim();
                    if (strUID != "" && strAction != "")
                    {
                        DelUser(strUID, strAction);
                    }
                }
            }
        }

        private void getUsersList(string strKeyword)
        {
            SanZi.BLL.Users bll = new SanZi.BLL.Users();
            DataView dv = new DataView(bll.GetList(strKeyword).Tables[0]);
            AspNetPager1.RecordCount = dv.Count;

            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = dv;
            pds.AllowPaging = true;
            pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
            pds.PageSize = AspNetPager1.PageSize;
            Repeater1.DataSource = pds;
            Repeater1.DataBind();

        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            getUsersList(string.Empty);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            string strKeyword = this.txtKeyword.Text.Trim();
            getUsersList(strKeyword);
        }

        /// <summary>
        /// 导出权利人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExportUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("checkprint.aspx", true);
        }

        private void DelUser(string strUID, string strAction)
        {
            if (strAction == "del")
            {
                if (LTP.Common.PageValidate.IsNumber(strUID))
                {
                    int uid = int.Parse(strUID);
                    SanZi.BLL.Users bll = new SanZi.BLL.Users();
                    bll.DelUser(uid);
                    CommClass.ExecuteSQL("delete from cw_barcode where UserID='" + uid + "'");
                    Page.RegisterStartupScript("ts", "<script  language=javascript>window.location.href='index.aspx';</script>");
                }
            }
        }
    }
}
