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

namespace SanZi.Web
{
    public partial class manage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    getCunWuGongKai(string.Empty);
                    if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                    {
                        string strID = Request.Params["id"].Trim().Replace("'", "");
                        string strAction = Request.QueryString["act"].Trim();
                        if (strID != "" && strAction != "")
                        {
                            DelCunWuGongKai(strID, strAction);
                        }
                    }
                }
                catch { ClientScript.RegisterClientScriptBlock(this.GetType(),"sd","<script>alert('请选择帐套！')</script>"); }
            }
        }

        private void getCunWuGongKai(string strKeyword)
        {
            string subid = string.Empty;
            if (Request.QueryString["lbid"] != null)
            {
                subid = Request.QueryString["lbid"];
            }
            SanZi.BLL.CunWuGongKai bll = new SanZi.BLL.CunWuGongKai();
            DataView dv = new DataView(bll.GetList(strKeyword, subid).Tables[0]);
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
            getCunWuGongKai(string.Empty);
        }

        #region 查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            string strKeyword = this.txtKeyword.Text.Trim();
            getCunWuGongKai(strKeyword);
        }
        #endregion

        private void DelCunWuGongKai(string strID, string strAction)
        {
            if (strAction == "del")
            {
                if (LTP.Common.PageValidate.IsNumber(strID))
                {
                    int id = int.Parse(strID);
                    SanZi.BLL.CunWuGongKai bll = new SanZi.BLL.CunWuGongKai();
                    bll.Delete(id);
                    Page.RegisterStartupScript("ts", "<script  language=javascript>window.location.href='manage.aspx';</script>");
                }
            }
        }

    }
}
