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
using LTP.Common;
namespace SanZi.Web.users
{
    public partial class Modify : System.Web.UI.Page
    {
        public string strUID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
		{
            if (!Page.IsPostBack)
            {
                this.txtDeptName.Text = Public.AccountName; ;
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    string strUID = Request.Params["id"].Trim();
                    ShowInfo(strUID);
                }
            }

		}
			
	    private void ShowInfo(string strUID)
	    {
            int uid = int.Parse(strUID);
            SanZi.BLL.Users bllUser = new SanZi.BLL.Users();
            DataTable dt = bllUser.getUserInfoByID(uid);
            this.txtDeptName.Text = dt.Rows[0]["DeptName"].ToString();
            this.txtTrueName.Text = dt.Rows[0]["TrueName"].ToString();
            this.txtTelPhone.Text = dt.Rows[0]["TelPhone"].ToString();
            int intTitleID = int.Parse(dt.Rows[0]["TitleID"].ToString());
            this.hidUserID.Value = uid.ToString();
            string tname = dt.Rows[0]["TitleName"].ToString();
            dt.Clear();

            this.dplUserTitle.Items.Add(new ListItem("请选择", "0"));
            dt = MainClass.GetDataTable("select paraname,paratype,paravalue from cw_syspara where paratype='100001'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem lt = new ListItem(dt.Rows[i]["paravalue"].ToString(), dt.Rows[i]["paraname"].ToString());
                this.dplUserTitle.Items.Add(lt);
            }
            this.dplUserTitle.SelectedIndex = this.dplUserTitle.Items.IndexOf(dplUserTitle.Items.FindByText(tname));

            DataTable bars = CommClass.GetDataTable("select * from cw_barcode where UserID='" + strUID + "'");
            GridView1.DataSource = bars.DefaultView;
            GridView1.DataBind();
	    }


        protected void btnEditUser_Click(object sender, EventArgs e)
        {
            string strDeptName, strTrueName, strTitleName, strTelPhone;
            int userid, titleid, deptid;
            strDeptName = this.txtDeptName.Text.Trim().Replace("'", "");
            strTrueName = this.txtTrueName.Text.Trim();
            strTelPhone = this.txtTelPhone.Text.Trim();
            deptid = int.Parse(this.hidDeptID.Value.Trim());
            strTitleName = this.dplUserTitle.SelectedItem.Text;
            titleid = this.dplUserTitle.SelectedIndex;
            userid = int.Parse(this.hidUserID.Value.Trim());

            SanZi.BLL.Users bll = new SanZi.BLL.Users();
            bll.UpdateUser(deptid, strDeptName, titleid, strTitleName, strTelPhone, strTrueName, userid);

            LTP.Common.MessageBox.ShowAndRedirect(this.Page, "修改成功", "index.aspx");
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].Text == "1")
                {
                    e.Row.Cells[1].Text = "已使用";
                }
                else
                {
                    e.Row.Cells[1].Text = "未使用";
                }
            }
        }
    }
}
