using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using LTP.Common;
using System.Text;
using System.Web;
namespace SanZi.Web.daili
{
    public partial class Index : System.Web.UI.Page
    {
        string strID = string.Empty;
        string strAction = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (HttpContext.Current.Session["AccountID"] != null)
                {
                    DaiLiList(string.Empty);
                    if (Request.QueryString["id"] != null)
                    {
                        strID = Request.QueryString["id"].Trim();
                        strAction = Request.QueryString["act"].Trim();
                        if (strID != "" && strAction != "")
                        {
                            DelDaiLi(strID, strAction);
                        }
                    }
                }
                else
                {
                    LTP.Common.MessageBox.ShowAndRedirect(this.Page, "请先选择账套", "../../HomePage.aspx");
                }
            }
        }

        /// <summary>
        /// 代理列表
        /// </summary>
        /// <param name="strKeyword"></param>
        public void DaiLiList(string strKeyword)
        {
            SanZi.BLL.DaiLi bll = new SanZi.BLL.DaiLi();
            DataView dv = new DataView(bll.GetDaiLiList(strKeyword).Tables[0]);
            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = dv;
            pds.AllowPaging = true;
            pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
            pds.PageSize = AspNetPager1.PageSize;
            rpFileManage.DataSource = dv;
            rpFileManage.DataBind();
        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            DaiLiList("");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            string strKeyword = this.txtKeyword.Text.Trim();
            DaiLiList(strKeyword);
        }

        /// <summary>
        /// 删除代理
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="strAction"></param>
        private void DelDaiLi(string strID, string strAction)
        {
            if (strAction == "del")
            {
                if (LTP.Common.PageValidate.IsNumber(strID))
                {
                    int id = int.Parse(strID);
                    SanZi.BLL.DaiLi bll = new SanZi.BLL.DaiLi();
                    bll.DelDaiLi(id);
                    Page.RegisterStartupScript("ts", "<script  language=javascript>window.location.href='index.aspx';</script>");
                }
            }
        }
    }
}
