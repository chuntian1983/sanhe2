using System;
using System.Xml;
using System.Web.UI;
using System.Web.UI.WebControls;
using LTP.Common;
using System.Web;
using System.Configuration;
using System.Text;
using System.Data;

namespace SanZi.Web.pibanka
{
    public partial class ConditionSet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int lowermoeny = TypeParse.StrToInt(ConfigurationManager.AppSettings["lowermoeny"], 10000);
                DataTable dt = MainClass.GetDataTable("select paraname,paratype,paravalue from cw_syspara where paratype='100001'");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListItem lt = new ListItem(dt.Rows[i]["paravalue"].ToString(), dt.Rows[i]["paraname"].ToString());
                    UserTitles.Items.Add(lt);
                }
                UtilsPage.SetTextBoxAutoValue2(Step1, 0);
                UtilsPage.SetTextBoxAutoValue2(Step2, 0);
                UtilsPage.SetTextBoxAutoValue2(Step3, 0);
            }
        }

        public void ShowResult()
        {
            Step3.Text = CommClass.GetSysPara("lowermoeny");
            DataTable data = CommClass.GetDataTable("select * from contidion order by Step1,Step2,ID");
            int acount = data.Rows.Count;
            if (acount == 0)
            {
                PageClass.BindNoRecords(GridView1, data);
            }
            else
            {
                GridView1.DataSource = data.DefaultView;
                GridView1.DataKeyNames = new string[] { "id" };
                GridView1.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dics = new Dictionary<string, string>();
            dics.Add("Step1", Step1.Text);
            dics.Add("Step2", Step2.Text);
            dics.Add("Bili", Bili.SelectedValue);
            dics.Add("BiliShow", Bili.SelectedItem.Text);
            StringBuilder titles = new StringBuilder();
            foreach (ListItem li in UserTitles.Items)
            {
                if (li.Selected)
                {
                    titles.AppendFormat(",{0}", li.Text);
                }
            }
            titles.Remove(0, 1);
            dics.Add("UserTitles", titles.ToString());
            CommClass.ExecuteSQL("contidion", dics);
            ShowResult();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            UserInfo.AccountID = AccountID.Value;
            aname.Text = UserInfo.AccountName;
            setm.Visible = true;
            ShowResult();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            CommClass.ExecuteSQL("delete from contidion where id=" + btn.CommandArgument);
            ShowResult();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //--
            }
        }

        protected void btnSave0_Click(object sender, EventArgs e)
        {
            CommClass.SetSysPara("lowermoeny", Step3.Text);
            ShowResult();
        }
    }
}
