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
using MySql.Data.MySqlClient;

public partial class cwgk : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //UserInfo.SessionFlag = "SessionFlag";
            //UserInfo.AccountID = TreeView1.SelectedValue.Substring(1);
            //Session["UserID"] = row["id"].ToString();
            //Session["RealName"] = row["realname"].ToString();
            //Session["UserName"] = row["username"].ToString();
            Session["Powers"] = "000000";
            //Session["MyAccount"] = row["accountid"].ToString();
            //Session["UserType"] = "0";
            DataTable dt;
            DataSet ds = new DataSet();
            
            ds.ReadXml(ValidateClass.RegFilePath);
            
            GridView gv = new GridView();
            
            DataRow[] drs= ds.Tables[1].Select(" UnitLevel='2'");
            dt = ds.Tables[1].Clone();
            for (int i = 0; i < drs.Length; i++)
            {
                
                dt.Rows.Add(drs[i].ItemArray);
            }
           
            
            this.Repeater1.DataSource = dt;
            this.Repeater1.DataBind();
           
           
            
        }
        
    }

    
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataList rep = e.Item.FindControl("Repeater2") as DataList;//找到里层的repeater对象
            DataRowView rowv = (DataRowView)e.Item.DataItem;//找到分类Repeater关联的数据项 
            string typeid = rowv["id"].ToString(); ; //获取填充子类的id 
            string strsql = "select * from cw_account where UnitID='" + typeid + "'";
            MySqlConnection con = new MySqlConnection("server=localhost;database=CW_Databaserc;uid=root;pwd=123456");
            con.Open();
            MySqlCommand com = new MySqlCommand(strsql, con);
            MySqlDataAdapter da = new MySqlDataAdapter(com);

            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            rep.DataSource = ds;
            rep.DataBind();
        }
    }
}
