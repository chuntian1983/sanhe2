using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using LTP.Common;
using System.Text;
using System.Web;

namespace SanZi.Web.zhaobiao
{
    public partial class ProjectManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (HttpContext.Current.Session["AccountID"] != null)
                {
                    getProjectList(string.Empty);
                    if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim() != "")
                    {
                        string strID = Request.QueryString["id"].Trim().Replace("'", "");
                        string strAction = Request.QueryString["act"].Trim();
                        if (strID != "" && strAction != "")
                        {
                            DelProject(strID, strAction);
                        }
                    }
                }
                else
                {
                    LTP.Common.MessageBox.ShowAndRedirect(this.Page, "请先选择账套", "../../HomePage.aspx");
                }
            }
        }

        private void getProjectList(string strKeyword)
        {
            SanZi.BLL.ZhaoBiao bll = new SanZi.BLL.ZhaoBiao();
            DataView dv = new DataView(bll.getProjectList(strKeyword).Tables[0]);
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
            getProjectList(string.Empty);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            string strKeyword = this.txtKeyword.Text.Trim();
            getProjectList(strKeyword);
        }

        private void DelProject(string strID, string strAction)
        {
            if (strAction == "del")
            {
                if (LTP.Common.PageValidate.IsNumber(strID))
                {
                    int id = int.Parse(strID);
                    SanZi.BLL.ZhaoBiao bll = new SanZi.BLL.ZhaoBiao();
                    bll.DelProject(id);
                    Page.RegisterStartupScript("ts", "<script  language=javascript>window.location.href='ProjectManage.aspx';</script>");
                }
            }
        }

        public string GetModelID(string id, string flag)
        {
            switch (flag)
            {
                case "0":
                    string name = getValue("select xmmc from xiangmu where id='" + id + "'");
                    return getValue("select DaiLi_ID from daili where ProjectName='" + name + "'");
                case "1":
                    return getValue("select id from cyss where xmmc='" + id + "'");
                case "2":
                    return getValue("select id from zbgg where zbgc='" + id + "'");
                case "3":
                    return id;
                case "4":
                    return getValue("select id from jjztb where xmmc='" + id + "'");
                case "5":
                    return getValue("select id from zhongb where zbmc='" + id + "'");
            }
            return "";
        }

        private string getValue(string sql)
        {
            object v = Maticsoft.DBUtility.DbHelperMySQL.GetSingle(sql);
            if (v == null)
            {
                return "";
            }
            else
            {
                return v.ToString();
            }
        }
    }
}
