using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class BidManage_ProjectEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            TableID.Value = Request.QueryString["id"];
            StepFlag.Value = Request.QueryString["step"];
            DataTable zc = CommClass.GetDataTable("select id,AssetName from cw_assetcard order by cardid");
            foreach (DataRow crow in zc.Rows)
            {
                ZiChan.Items.Add(new ListItem(crow["AssetName"].ToString(), crow["id"].ToString()));
            }
            DataTable zy = CommClass.GetDataTable("select id,ResName from cw_rescard order by cardno");
            foreach (DataRow crow in zy.Rows)
            {
                ZiYuan.Items.Add(new ListItem(crow["ResName"].ToString(), crow["id"].ToString()));
            }
            DataTable data = MainClass.GetDataTable("select ProjectName,ZiChan,ZiYuan from projects where id='" + Request.QueryString["id"] + "'");
            ProjectName.Text = data.Rows[0]["ProjectName"].ToString();
            UtilsPage.InitCheckBoxList(ZiChan, data.Rows[0]["ZiChan"].ToString());
            UtilsPage.InitCheckBoxList(ZiYuan, data.Rows[0]["ZiYuan"].ToString());
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        StringBuilder zc = new StringBuilder();
        foreach (ListItem li in ZiChan.Items)
        {
            if (li.Selected)
            {
                zc.AppendFormat("{0},", li.Value);
            }
        }
        StringBuilder zy = new StringBuilder();
        foreach (ListItem li in ZiYuan.Items)
        {
            if (li.Selected)
            {
                zy.AppendFormat("{0},", li.Value);
            }
        }
        MainClass.ExecuteSQL("update projects set ZiChan='" + zc.ToString() + "',ZiYuan='" + zy.ToString() + "' where id='" + Request.QueryString["id"] + "'");
        PageClass.ShowAlertMsg(this.Page, "保存成功！");
    }
}
