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

namespace SanZi.Web.DaiLi
{
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                time1.Attributes.Add("readonly", "readonly");
                time1.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].time1,'yyyy-mm-dd')");
                time2.Attributes.Add("readonly", "readonly");
                time2.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].time2,'yyyy-mm-dd')");
                time3.Attributes.Add("readonly", "readonly");
                time3.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].time3,'yyyy-mm-dd')");
                time4.Attributes.Add("readonly", "readonly");
                time4.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].time4,'yyyy-mm-dd')");
                time5.Attributes.Add("readonly", "readonly");
                time5.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].time5,'yyyy-mm-dd')");
                time6.Attributes.Add("readonly", "readonly");
                time6.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].time6,'yyyy-mm-dd')");
                time7.Attributes.Add("readonly", "readonly");
                time7.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].time7,'yyyy-mm-dd')");
               
                time9.Attributes.Add("readonly", "readonly");
                time9.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].time9,'yyyy-mm-dd')");
                this.txtApplyDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                this.radioZhiChu.Attributes.Add("onclick", "changeZhiChu('" + this.radioZhiChu.ClientID + "')");
                this.radioShouRu.Attributes.Add("onclick", "changeShouRu('" + this.radioShouRu.ClientID + "')");
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					int id = int.Parse(Request.Params["id"]);
                    ShowInfo(id);
				}

            }
        }
        //显示信息
         private void ShowInfo(int id)
	    {
            SanZi.BLL.DaiLi bll = new SanZi.BLL.DaiLi();
            DataTable dt = bll.GetDaiLiByPid(id);
            if (dt.Rows.Count > 0)
            {
                this.txtVillageName.Text = dt.Rows[0]["VillageName"].ToString();
                this.txtApplyDate.Text = dt.Rows[0]["ApplyDate"].ToString();
                this.txtProjectBackGround.Text = dt.Rows[0]["ZhiBu_YuanYin"].ToString();
                this.txtProjectName.Text = dt.Rows[0]["ProjectName"].ToString();
                this.txtEstimateValue.Text = dt.Rows[0]["EstimateValue"].ToString();
                this.time1.Text = dt.Rows[0]["time1"].ToString();
                this.time2.Text = dt.Rows[0]["time2"].ToString();
                this.time3.Text = dt.Rows[0]["time3"].ToString();
                this.time4.Text = dt.Rows[0]["time4"].ToString();
                this.time5.Text = dt.Rows[0]["time5"].ToString();
                this.time6.Text = dt.Rows[0]["time6"].ToString();
                this.time7.Text = dt.Rows[0]["time7"].ToString();
                this.time8.Text = dt.Rows[0]["time8"].ToString();
                this.time9.Text = dt.Rows[0]["time9"].ToString();
                this.value1.Text = dt.Rows[0]["value1"].ToString();
                this.value2.Text = dt.Rows[0]["value2"].ToString();
                this.value3.Text = dt.Rows[0]["value3"].ToString();
                this.value4.Text = dt.Rows[0]["value4"].ToString();
                this.value5.Text = dt.Rows[0]["value5"].ToString();
                this.DropDownList1.Text = dt.Rows[0]["value6"].ToString();
                if (dt.Rows[0]["ProjectType"].ToString() == "支出项目")
                {
                radioZhiChu.Checked = true;
                 }
                else
                {
                radioShouRu.Checked = true;
                 }
                //int addTime = int.Parse(dt.Rows[0]["OutMoney"].ToString());
                //this.lblPassTime.Text = ((DateTime)LTP.Common.TimeParser.ConvertIntDateTime(addTime)).ToString("yyyy-MM-dd hh:mm:ss");//LTP.Common.TimeParser.ConvertIntDateTime(addTime).ToString();

                DataTable dt2 = bll.GetShenPiRen(id);
                int intNum = dt2.Rows.Count;
                StringBuilder sb = new StringBuilder();
                sb.Append("<table cellSpacing=1 cellPadding=4 width=100%  bgColor=#999999 border=0 >");
                for (int i = 0; i < intNum; i++)
                {
                    sb.Append("<tr bgcolor=#edf2f7><td>" + dt2.Rows[i]["TrueName"].ToString() + "</td>");
                    sb.Append("<td>" + dt2.Rows[i]["DeptName"].ToString() + "</td><td>" + dt2.Rows[i]["TitleName"].ToString() + "</td></tr>");
                }
                sb.Append("</table>");
                this.lblShenPiRen.Text = sb.ToString();
            }
            else
            {
                LTP.Common.MessageBox.ShowAndRedirect(this.Page,"参数有误！","index.aspx");
            }
            SanZi.BLL.ZhaoBiao bll1 = new SanZi.BLL.ZhaoBiao();
            string str = " dailiid = " + id.ToString();
            DataSet ds = bll1.selectxiangmu(str);
            if (ds.Tables[0].Rows.Count > 0) 
            {
                dailiid.Value = ds.Tables[0].Rows[0]["dailiid"].ToString();
            }
	    }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strDeptName, strDeptID, strApplyDate;
            string strBackGround, strProjectName, strEstimateValue;
           // string time1, time2, time3, time4, time5, time6, time7, time8, time9, value1, value2, value3, value4, value5;
            string strBarCode, strUserID;
            int intSubUID = 0;
            strDeptName = this.txtVillageName.Text.Trim();//申请村
            strDeptID = this.hidDeptID.Value.Trim();//单位ID
            strApplyDate = this.txtApplyDate.Text.Trim();//申请日期
            strBackGround = this.txtProjectBackGround.Text.Trim();//项目背景
            strProjectName = this.txtProjectName.Text.Trim();//项目名称
            strEstimateValue = this.txtEstimateValue.Text.Trim();//估算价

            string strProjectType = string.Empty;//项目类型
            strBarCode = this.hidBarCode.Value.Trim();//条形码信息
            strUserID = this.hidUID.Value.Trim();//条形码用户ID
           
            if (this.radioShouRu.Checked)
            {
                strProjectType = "收入项目";
            }
            else
            {
                strProjectType = "支出项目";
            }

            int intDeptID = int.Parse(strDeptID);
            if (HttpContext.Current.Session["UserID"] != null)
            {
                intSubUID = int.Parse(HttpContext.Current.Session["UserID"].ToString());
            }

            #region userid去空
            if (strUserID != "")
            {
                StringBuilder sb = new StringBuilder();
                string[] arrUserID = strUserID.Split(',');
                ArrayList al = new ArrayList();
                foreach (string str in arrUserID)
                {
                    if (str.Trim() != "")
                    {
                        sb.Append(str.ToString() + ",");
                    }
                }
                strUserID = sb.ToString().TrimEnd(',');//已扫描用户ID
            }
            #endregion

            SanZi.BLL.DaiLi bll = new SanZi.BLL.DaiLi();
            SanZi.Model.DaiLi model = new SanZi.Model.DaiLi();
            if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
            {
            model.DaiLi_ID = int.Parse(Request.Params["id"]);
            model.VillageName = strDeptName;
            model.ApplyDate = strApplyDate;
            model.ProjectName = txtProjectName.Text;
            model.ProjectType = strProjectType;
            model.EstimateValue = strEstimateValue;
            model.ZhiBu_YuanYin = strBackGround;
            model.SubUID =intSubUID;
            model.time1 = this.time1.Text;
            model.time2 = this.time2.Text;
            model.time3 = this.time3.Text;
            model.time4 = this.time4.Text;
            model.time5 = this.time5.Text;
            model.time6 = this.time6.Text;
            model.time7 = this.time7.Text;
            model.time8 = this.time8.Text;
            model.time9 = this.time9.Text;
            model.value1 = this.value1.Text;
            model.value2 = this.value2.Text;
            model.value3 = this.value3.Text;
            model.value4 = this.value4.Text;
            model.value5 = this.value5.Text;
            model.value6 = this.DropDownList1.SelectedValue;
            }
           
            SanZi.Model.xiangmu model1 = new SanZi.Model.xiangmu();
            model1.DaiLiId = dailiid.Value;
            model1.Xmmc = strProjectName;
            SanZi.BLL.ZhaoBiao bll1 = new SanZi.BLL.ZhaoBiao();
            try
            {
                bll.Update(model);
                bll1.UpdateXM(model1);
            }
            catch { }
            LTP.Common.MessageBox.ShowAndRedirect(this.Page, "保存成功", "index.aspx");
        }
    }
}
