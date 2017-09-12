using System;
using System.Collections;
using System.Web.UI;
using LTP.Common;
using System.Text;
using System.Web;

namespace SanZi.Web.daili
{
    public partial class AddDaiLi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (HttpContext.Current.Session["AccountID"] != null)
                {
                    this.txtVillageName.Text = Public.AccountName;
                    this.txtApplyDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                    this.radioZhiChu.Attributes.Add("onclick", "changeZhiChu('" + this.radioZhiChu.ClientID + "')");
                    this.radioShouRu.Attributes.Add("onclick", "changeShouRu('" + this.radioShouRu.ClientID + "')");
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
                }
                else 
                {
                    LTP.Common.MessageBox.ShowAndRedirect(this.Page, "请先选择账套", "../../HomePage.aspx");
                }
               
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strDeptName, strDeptID, strApplyDate;
            string strBackGround, strProjectName, strEstimateValue;
            string time1, time2, time3, time4, time5, time6, time7, time8, time9, value1, value2, value3, value4, value5, value6;
            string strBarCode, strUserID;
            int intSubUID = 0;
            strDeptName = this.txtVillageName.Text.Trim();//申请村
            strDeptID = this.hidDeptID.Value.Trim();//单位ID
            strApplyDate = this.txtApplyDate.Text.Trim();//申请日期
            strBackGround = this.txtProjectBackGround.Text.Trim();//项目背景
            strProjectName = this.txtProjectName.Text.Trim();//项目名称
            strEstimateValue = this.txtEstimateValue.Text.Trim();//估算价
            
            string strProjectType=string.Empty;//项目类型
            strBarCode = this.hidBarCode.Value.Trim();//条形码信息
            strUserID = this.hidUID.Value.Trim();//条形码用户ID
            time1 = this.time1.Text;
            time2 = this.time2.Text;
            time3 = this.time3.Text;
            time4 = this.time4.Text;
            time5 = this.time5.Text;
            time6 = this.time6.Text;
            time7 = this.time7.Text;
            time8 = this.time8.Text;
            time9 = this.time9.Text;
            value1 = this.value1.Text;
            value2 = this.value2.Text;
            value3 = this.value3.Text;
            value4 = this.value4.Text;
            value5 = this.value5.Text;
            value6 = this.DropDownList1.SelectedValue;
            if(this.radioShouRu.Checked)
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
           
            SanZi.Model.xiangmu model1 = new SanZi.Model.xiangmu();
            model1.Xmmc = strProjectName;
            SanZi.BLL.ZhaoBiao bll1 = new SanZi.BLL.ZhaoBiao();
            try
            {
                bll.SaveDaiLi(intDeptID, intSubUID, strDeptName, strApplyDate, strBackGround, strProjectName, strProjectType, strEstimateValue, strUserID, time1, time2, time3, time4, time5, time6, time7, time8, time9, value1, value2, value3, value4, value5, value6);
                bll1.Addxiangmu(model1);
            }
            catch { }
            LTP.Common.MessageBox.ShowAndRedirect(this.Page, "保存成功", "index.aspx");
        }
    }
}
