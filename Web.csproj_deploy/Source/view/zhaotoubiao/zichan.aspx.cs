using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LTP.Common;

namespace SanZi.Web.view.zhaotoubiao
{
    public partial class zichan : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.Session["AccountID"] != null)
                {
                    UserInfo.AccountID = HttpContext.Current.Session["AccountID"].ToString();
                    DataSet ds = CommClass.GetDataSet("select * from cw_assetcard  order by cardid desc");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.CheckBoxList1.DataSource = ds;
                        this.CheckBoxList1.DataTextField = "assetname";
                        this.CheckBoxList1.DataValueField = "id";
                        this.CheckBoxList1.DataBind();
                    }
                }
                else
                {

                    LTP.Common.MessageBox.ShowAndRedirect(this.Page, "请先选择账套", "../../HomePage.aspx");
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string strzyid = "";
            if (Request.QueryString["id"] != null)
            {
                string strid = Request.QueryString["id"];
                for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
                {
                    if (this.CheckBoxList1.Items[i].Selected)
                    {
                        strzyid += CheckBoxList1.Items[i].Value + "|";
                    }
                }
                string strsql = "update cw_ztbxm set xmzyid='" + strzyid + "',xmzylb='1' where id='" + strid + "'";
                if (MainClass.ExecuteSQL(strsql) > 0)
                {
                    MessageBox.Show(this, "选择成功！");

                }
            }
        }
    }
}