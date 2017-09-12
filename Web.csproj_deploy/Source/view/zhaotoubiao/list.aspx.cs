using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using LTP.Common;
using System.Text;
using System.Web;

namespace SanZi.Web.view.zhaotoubiao
{
    public partial class list : System.Web.UI.Page
    {
        string strID = string.Empty;
        string strAction = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (HttpContext.Current.Session["AccountID"] != null)
                {
                    this.Label1.Text = Public.AccountName;
                    if (Request.QueryString["zt"] != null)
                    {
                        DaiLiList(Request.QueryString["zt"]);
                        #region 标题
                        switch (Request.QueryString["zt"].ToString())
                        {
                            case "1":
                                lbtitle.Text = "村民代表会议记录";
                                break;
                            case "2":
                                lbtitle.Text = "招标项目申请";
                                break;
                            case "3":
                                lbtitle.Text = "招标项目审批";
                                break;
                            case "4":
                                lbtitle.Text = "预算书录入";
                                break;
                            case "5":
                                lbtitle.Text = "招标文件录入";
                                break;
                            case "6":
                                lbtitle.Text = "投标公告";
                                break;
                            case "7":
                                lbtitle.Text = "参投登记";
                                break;
                            case "8":
                                lbtitle.Text = "竞价招投标";
                                break;
                            case "9":
                                lbtitle.Text = "中标公告";
                                break;
                            case "10":
                                lbtitle.Text = "签订合同";
                                break;
                        }
                        #endregion
                    }
                    else
                    {
                        DaiLiList("1");
                    }
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
            string strsql = "select a.*,b.xmname as zname from cw_ztbfj a,cw_ztbxm b where a.zbid=b.id and b.xmacount='" + HttpContext.Current.Session["AccountID"] + "' and a.xmzt='" + strKeyword + "'";
            DataTable dt = MainClass.GetDataTable(strsql);
          
            DataView dv = new DataView(dt);
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
            if (Request.QueryString["zt"] != null)
            {
                DaiLiList(Request.QueryString["zt"]);
            }
            else
            {
                DaiLiList("1");
            }
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

        protected void rpFileManage_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName=="del")
            {
                string id = e.CommandArgument.ToString();
                string strsql = "delete from cw_ztbfj where id='"+id+"'";
                MainClass.ExecuteSQL(strsql);
                if (Request.QueryString["zt"] != null)
                {
                    DaiLiList(Request.QueryString["zt"]);
                }
                else
                {
                    DaiLiList("1");
                }
            }
        }
    }
}