using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SanZi.Web.view.zhaotoubiao
{
    public partial class xiangxi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    this.Menu1.TabIndex = 0;
                    this.MultiView1.SetActiveView(View1);
                    string zid = Request.QueryString["id"].ToString();
                    string strsql = "select * from cw_ztbfj where zbid='" + zid + "' and xmzt='1'";
                    DataTable dt = MainClass.GetDataTable(strsql);
                    if (dt.Rows.Count > 0)
                    {
                        this.rep.DataSource = dt;
                        this.rep.DataBind();
                    }
                }

            }
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            this.MultiView1.SetActiveView(View1);
            if (Request.QueryString["id"] != null)
            {
                string zid = Request.QueryString["id"].ToString();
                string strsql = "select * from cw_ztbfj where zbid='"+zid+"' and xmzt='"+this.Menu1.SelectedValue+"'";
                DataTable dt = MainClass.GetDataTable(strsql);
                if (dt.Rows.Count > 0)
                {
                    this.rep.DataSource = dt;
                    this.rep.DataBind();
                }
                else
                {
                    this.rep.DataSource = dt;
                    this.rep.DataBind();
                }
            }
        }
    }
}