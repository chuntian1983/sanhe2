using System;
using System.Collections.Generic;
using LTP.Common;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SanZi.Web.zhaobiao
{
    public partial class zbggEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    SanZi.BLL.DaiLi bll = new SanZi.BLL.DaiLi();
                    DataSet ds = bll.GetDaiLiList("");
                    tbzbgc.DataSource = ds.Tables[0].DefaultView;
                    tbzbgc.DataTextField = "ProjectName";
                    tbzbgc.DataValueField = "DaiLi_ID";
                    tbzbgc.DataBind();
                    int ID = int.Parse(Request.Params["id"]);
                    ShowInfo(ID);
                }
                this.tbcwh.Text = Public.AccountName;
                this.tbsubtime.Text = System.DateTime.Now.ToString("yyyy年MM月dd日");
            }
        }
        private void ShowInfo(int ID)
        {
            SanZi.BLL.ZhaoBiao bll = new SanZi.BLL.ZhaoBiao();
            DataTable dt = bll.GetZhaoBiaoGongGaoByID(ID);
            if (dt.Rows.Count > 0)
            {
                this.tbcwh.Text = dt.Rows[0]["cwh"].ToString();//单位名称
                this.tbzbgc.Text = dt.Rows[0]["zbgc"].ToString();//项目名称
                this.tbnrqk.Text = dt.Rows[0]["nrqk"].ToString();//招标内容及项目情况
                this.tbbmtj.Text = dt.Rows[0]["bmtj"].ToString();//报名条件
                this.tbyz.Text = dt.Rows[0]["yz"].ToString();//原则
                this.tbstarttime.Text = dt.Rows[0]["startTime"].ToString();//开始时间
                this.tbfinishtime.Text = dt.Rows[0]["finishTime"].ToString();//结束时间
                this.tbbmdd.Text = dt.Rows[0]["bmdd"].ToString();//地点
                this.tblxfs.Text = dt.Rows[0]["lxfs"].ToString();//联系人及电话
                this.tbsubtime.Text = dt.Rows[0]["SubTime"].ToString();//联系人及电话
                this.TextBox1.Text = dt.Rows[0]["str"].ToString();//联系人及电话
            }
            else
            {
                LTP.Common.MessageBox.ShowAndRedirect(this.Page, "参数有误！", "cpjggl.aspx");
            }
        }

        protected void btn_sure_Click(object sender, EventArgs e)
        {
            string strErr = "";
            if ((this.tbcwh.Text == ""))
            {
                strErr += "请将主题填写完整！\\n";
            }
            if (this.tbnrqk.Text == "")
            {
                strErr += "请填写招标内容及项目情况！\\n";
            }
            if (this.tbbmtj.Text == "")
            {
                strErr += "请填写报名条件！\\n";
            }
            if (this.tbyz.Text == "")
            {
                strErr += "请将招标程序填写完整！\\n";
            }
            if ((this.tbstarttime.Text == "") && (this.tbfinishtime.Text == ""))
            {
                strErr += "请将报名时间填写完整！\\n";
            }
            if (tbbmdd.Text == "")
            {
                strErr += "请将报名地点填写完整！\\n";
            }
            if (tblxfs.Text == "")
            {
                strErr += "请填写联系人及电话！\\n";
            }
            if (tbsubtime.Text == "")
            {
                strErr += "请填写时间！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string cwh = tbcwh.Text.Trim();
            string zbgc = tbzbgc.SelectedValue;
            string nrqk = tbnrqk.Text.Trim();
            string bmtj = tbbmtj.Text.Trim();
            string yz = tbyz.Text.Trim();
            string startTime = tbstarttime.Text;
            string finishtime = tbfinishtime.Text;
            string bmdd = tbbmdd.Text.Trim();
            string lxfs = tblxfs.Text.Trim();
            string subTime = tbsubtime.Text;
            string str = TextBox1.Text;
            SanZi.Model.zbgg model = new SanZi.Model.zbgg();
            if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
            {
                model.ID = int.Parse(Request.Params["id"]);
                model.Cwh = cwh;
                model.Zbgc = zbgc;
                model.Nrqk = nrqk;
                model.Bmtj = bmtj;
                model.Yz = yz;
                model.StartTime = startTime;
                model.FinishTime = finishtime;
                model.Bmdd = bmdd;
                model.Lxfs = lxfs;
                model.SubTime = subTime;
                model.str = str;
            }
           
            SanZi.BLL.ZhaoBiao bll = new SanZi.BLL.ZhaoBiao();
            try
            {
                bll.Update(model);
                //string message = "保存成功！";
                //MessageBox.Show(this,message);
                LTP.Common.MessageBox.ShowAndRedirect(this.Page, "保存成功！", "zbgggl.aspx");
            }
            catch { }
        }
    }
}
