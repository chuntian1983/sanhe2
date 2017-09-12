using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using LTP.Common;
using System.Text;
using System.Web;

namespace SanZi.Web.view.zhaotoubiao
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
            
            string strsql = "select * from cw_ztbxm where xmacount='" + HttpContext.Current.Session["AccountID"] + "'";
            DataTable dt = MainClass.GetDataTable(strsql);
            DataView dv = new DataView(dt);
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
                    string strsql = "delete from cw_ztbxm where id='"+id+"'";
                    MainClass.ExecuteSQL(strsql);
                    getProjectList("");
                    
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

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                string strlb = drv["xmzylb"].ToString();
                string strid=drv["xmzyid"].ToString();
                Label lbzc=e.Item.FindControl("lbzc") as Label;
                lbzc.Text = "";
                string[] strlist=strid.Split('|');
                string strids = "";
                if (strlist.Length > 0)
                {
                    for (int j = 0; j < strlist.Length; j++)
                    {
                        if (strlist[j].Length > 0)
                        {
                            strids += strlist[j] + ",";
                        }

                    }
                    strids = strids.Trim().TrimEnd(',');
                    DataSet ds = new DataSet();
                    switch (strlb)
                    {
                        case "1":
                            UserInfo.AccountID = HttpContext.Current.Session["AccountID"].ToString();
                            ds = new DataSet();
                            ds = CommClass.GetDataSet("select * from cw_assetcard  where id in (" + strids + ")");
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    lbzc.Text += ds.Tables[0].Rows[i]["assetname"].ToString() + "|";
                                }
                                lbzc.Text = lbzc.Text.TrimEnd('|');
                            }
                            break;
                        case "2":
                            UserInfo.AccountID = HttpContext.Current.Session["AccountID"].ToString();
                            ds = new DataSet();
                            ds = CommClass.GetDataSet("select * from cw_rescard where id in (" + strids + ")");
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    lbzc.Text += ds.Tables[0].Rows[i]["resname"].ToString() + "|";
                                }
                                lbzc.Text = lbzc.Text.TrimEnd('|');
                            }
                            break;
                    }
                }
            }
        }
    }
}