using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using LTP.Common;
using Maticsoft.DBUtility;

namespace SanZi.Web
{
    public partial class PiBanKa : System.Web.UI.Page
    {
        private readonly SanZi.BLL.PiBanKa bll = new SanZi.BLL.PiBanKa();
        public string strErrMsg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
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
                //DebitSubject.Attributes.Add("readonly", "readonly");
                //CreditSubject.Attributes.Add("readonly", "readonly");
                txtDeptName.Attributes.Add("readonly", "readonly");
            }
            AjaxPro.Utility.RegisterTypeForAjax(typeof(PiBanKa));//注册ajaxPro,括号中的参数是当前的类名
            this.txtDebit.Attributes.Add("onblur", "searchContion()");
            this.txtDeptName.Text = Public.AccountName;
        }

        [AjaxPro.AjaxMethod]//申明是ajaxPro方法
        public string GetOutCondition(string inputValue, string deptID)
        {
            bool isNum = RegexValidate(@"^\\d{1,15}(\\.\\d{1,4})?$", inputValue.Trim());
            if (isNum)
            {
                return "提示：支出金额必须为正数！最多四位小数。";
            }
            else
            {
                object money = DbHelperMySQL.GetSingle("select beginbalance from cw_subject where subjectno='" + ConfigurationManager.AppSettings["TuoGuanJin"] + "'");
                decimal m = 0;
                if (money != null)
                {
                    decimal.TryParse(money.ToString(), out m);
                }
                decimal fltMoney = decimal.Parse(inputValue);
                if (m > 0 && fltMoney > m)
                {
                    return "申请金额已超限，无法通过！最大申请金额为" + m.ToString() + "元。";
                }
                int intDeptID = int.Parse(deptID);
                return bll.getCondition(fltMoney, intDeptID);
            }
        }

        /// <summary>
        /// 使用指定正则进行验证
        /// </summary>
        /// <param name="regexString">正则表达式</param>
        /// <param name="validateString">待验证字符串</param>
        /// <returns></returns>
        public static bool RegexValidate(string regexString, string validateString)
        {
            Regex regex = new Regex(regexString);
            return regex.IsMatch(validateString.Trim());
        }

        /// <summary>
        /// 批办卡审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNextStep_Click(object sender, EventArgs e)
        {
            string strDeptName, strReason, strBarCode, strUserID, d, c;
            string time1, time2, time3, time4, time5, time6, value1, value2, value3;
            int intDeptID;
            int SubUID = 0;
            decimal fltMoney;

            StringBuilder sb = new StringBuilder();
            strDeptName = this.txtDeptName.Text.Trim().Replace(",", "");//部门名称
            strReason = this.txtReason.Text.Trim().Replace(",", "");//议定事由
            strBarCode = this.hidBarCode.Value.Trim();//条形码信息
            strUserID = this.hidUID.Value.Trim();//条形码用户ID

            d = this.HiddenField1.Value;
            c = this.HiddenField2.Value;
            time1 = this.time1.Text;
            time2 = this.time2.Text;
            time3 = this.time3.Text;
            time4 = this.time4.Text;
            time5 = this.time5.Text;
            time6 = this.time6.Text;
            value1 = this.value1.Text;
            value2 = this.value2.Text;
            value3 = this.value3.Text;
            intDeptID = int.Parse(this.hidDeptID.Value.Trim());//部门ID
            if (HttpContext.Current.Session["UserID"] != null)
            {
                SubUID = int.Parse(HttpContext.Current.Session["UserID"].ToString());
            }
            fltMoney = decimal.Parse(this.txtDebit.Text.Trim());//支出金额

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

            string strFileName = addFiles.Value;

            //SanZi.BLL.PiBanKa bll = new SanZi.BLL.PiBanKa();
            //int intPID = bll.SavePiBanKa(intDeptID, SubUID, strDeptName, strReason, fltMoney, strUserID, d, c, strFileName, TextBox1.Text, time1, time2, time3, time4, time5, time6, value1, value2, value3);
            //LTP.Common.MessageBox.ShowAndRedirect(this.Page, "批办卡审批通过", "PiBanka/index.aspx?pid=" + intPID);

            if (!checkCondition(intDeptID, fltMoney, strUserID))
            {
                LTP.Common.MessageBox.Show(this.Page, strErrMsg);
            }
            else
            {
                SanZi.BLL.PiBanKa bll = new SanZi.BLL.PiBanKa();
                int intPID = bll.SavePiBanKa(intDeptID, SubUID, strDeptName, strReason, fltMoney, strUserID, d, c, strFileName, TextBox1.Text, time1, time2, time3, time4, time5, time6, value1, value2, value3);
                
                CommClass.ExecuteSQL("update cw_barcode set usestate='1',usedate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where barcode in ('" + strBarCode.Replace(",", "','") + "')");

                LTP.Common.MessageBox.ShowAndRedirect(this.Page, "批办卡审批通过", "PiBanka/index.aspx?pid=" + intPID);
            }
        }

        /// <summary>
        ///  检查是否满足支出金额，最低审批条件
        /// </summary>
        /// <param name="deptid"></param>
        /// <param name="money"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        private bool checkCondition(int deptid, decimal money, string userid)
        {
            if (userid == "")
            {
                this.strErrMsg = "请扫描审批人条形码！";
                return false;
            }
            SanZi.DAL.PiBanKa dal = new SanZi.DAL.PiBanKa();
            SanZi.BLL.PiBanKa bll = new SanZi.BLL.PiBanKa();
            string wh = string.Format("where Step1<={0} and Step2>{0}", money.ToString());
            DataTable titles = dal.GetConditionBy(wh);
            DataTable saoma = bll.GetUserInfoByID(userid);//取所有已扫描条形码，用户信息
            StringBuilder sbReturn = new StringBuilder();
            foreach (DataRow drow in titles.Rows)
            {
                float total_0 = GetMemberCount(drow["UserTitles"].ToString());
                if (total_0 > 0)
                {
                    float total_1 = 0;
                    string[] userTitles = drow["UserTitles"].ToString().Split(',');
                    foreach (string title in userTitles)
                    {
                        if (title.Length > 0 && saoma.Select("TitleName='" + title + "'").Length > 0)
                        {
                            total_1++;
                        }
                    }
                    float rate_0 = float.Parse(drow["Bili"].ToString());
                    float rate_1 = total_1 / total_0;
                    if (rate_1 < rate_0)
                    {
                        sbReturn.AppendFormat("{0}等{1}人员，", drow["UserTitles"].ToString(), drow["BiliShow"].ToString());
                    }
                }
            }
            if (sbReturn.Length == 0)
            {
                return true;
            }
            else
            {
                this.strErrMsg = "需要满足以下条件：" + sbReturn.ToString() + "审核通过。";
                return false;
            }
        }

        private float GetMemberCount(string member)
        {
            string sql = "select count(*) from users where TitleName='" + member.Replace(",", "' or TitleName='") + "'";
            object count = DbHelperMySQL.GetSingle(sql);
            float c = 0;
            if (count != null)
            {
                float.TryParse(count.ToString(), out c);
            }
            return c;
        }

        protected void uploadFiles_Click(object sender, EventArgs e)
        {
            string strFileName1 = this.FileUpload1.PostedFile.FileName.Trim();
            if (strFileName1 != null && strFileName1.LastIndexOf(".") > 0)
            {
                int intLastIndex = strFileName1.LastIndexOf(".");//取得文件名中最后一个"."的索引
                if (intLastIndex < strFileName1.Length)
                {
                    string strUploadType = ",jpg,jpeg,bmp,gif,png,psd,";
                    string strExName = strFileName1.Substring(intLastIndex).Trim();//上传附件扩展名
                    if (strUploadType.IndexOf("," + strFileName1.Substring(intLastIndex + 1).ToLower().Trim() + ",") != -1)
                    {
                        string guid = System.Guid.NewGuid().ToString();
                        string strNewFileName = guid + strExName;
                        this.FileUpload1.PostedFile.SaveAs(Server.MapPath("/view/Images/" + strNewFileName));
                        string strFileName = "/view/Images/" + strNewFileName + "$";
                        divHtml.Value += "<div id='F" + guid + "'><a href='" + strFileName.Replace("$", "") + "' target='_blank'>" + strFileName.Replace("$", "") + "</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href='###' onclick=delFile('F" + guid + "','" + strFileName + "')>删除</a></div>";
                        filesShow.InnerHtml = divHtml.Value;
                        addFiles.Value += strFileName;
                    }
                }
            }
        }
    }
}
