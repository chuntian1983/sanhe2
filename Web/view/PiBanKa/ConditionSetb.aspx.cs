using System;
using System.Xml;
using System.Web.UI;
using LTP.Common;
using System.Web;
using System.Configuration;

namespace SanZi.Web.pibanka
{
    public partial class ConditionSetb : System.Web.UI.Page
    {
        public string aname = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int lowermoeny = TypeParse.StrToInt(ConfigurationManager.AppSettings["lowermoeny"], 10000);
                for (int i = 1000; i <= lowermoeny; )
                {
                    txtStepA.Items.Add(i.ToString());
                    i = i + 1000;
                }
                ShowResult();
            }
        }

        public void ShowResult()
        {
            if (HttpContext.Current.Session["AccountID"] == null)
            {
                setm.Visible = false;
                return;
            }
            setm.Visible = true;
            aname = UserInfo.AccountName;
            string strDeptID = "1";
            if (Request.QueryString["deptid"] != null)
            {
                strDeptID = Request.QueryString["deptid"].ToString().Trim().Replace("'", "");//部门ID
            }

            bool isNum = LTP.Common.PageValidate.IsNumber(strDeptID);
            if (isNum)
            {
                int intDeptID = int.Parse(strDeptID);
                //LTP.Common.MessageBox.Show(this.Page, strDeptID);
                string strStepA = "";//500
                string strStepB = "";//5000
                string strContID = "";//条件ID

                SanZi.BLL.PiBanKa bll = new SanZi.BLL.PiBanKa();
                bll.getConditionByDept(intDeptID, ref strStepA, ref strStepB, ref strContID);

                this.lblStepA.Text = strStepA;
                this.txtStepA.Text = strStepA;
                this.txtStepA2.Text = strStepA;
                this.txtStepB.Text = strStepB;
                this.txtStepB2.Text = strStepB;
                this.lblStepA2.Text = strStepA;
                this.lblStepBNum.Text = strStepB;
                this.lblStepBNum2.Text = strStepB;

                this.txtStepA.Attributes.Add("onchange", "changeStepA('" + this.txtStepA.ClientID + "')");
                this.txtStepA2.Attributes.Add("onchange", "changeStepA2('" + this.txtStepA2.ClientID + "')");
                this.txtStepB.Attributes.Add("onchange", "changeStepB('" + this.txtStepB.ClientID + "')");
                this.txtStepB2.Attributes.Add("onchange", "changeStepB2('" + this.txtStepB2.ClientID + "')");
            }
            else
            {
                LTP.Common.MessageBox.ShowAndRedirects(this.Page, "参数有误！", "ConditionSet.aspx?deptid=1");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strStepA = this.txtStepA.Text.Trim();
            string strStepA2 = this.txtStepA2.Text.Trim();
            string strStepB = this.txtStepB.Text.Trim();
            string strStepB2 = this.txtStepB2.Text.Trim();

            string strDeptID = Request.Form["hidDeptID"].ToString();
            if (strStepA == "" || strStepA2 == "" || strStepB == "" || strStepB2 == "")
            {
                LTP.Common.MessageBox.Show(this.Page, "支出金额不能为空！");
            }
            else
            {
                bool blStepA, blStepA2, blStepB, blStepB2;
                blStepA = LTP.Common.PageValidate.IsNumber(strStepA);
                blStepA2 = LTP.Common.PageValidate.IsNumber(strStepA2);
                blStepB = LTP.Common.PageValidate.IsNumber(strStepB);
                blStepB2 = LTP.Common.PageValidate.IsNumber(strStepB);
                if (!blStepA || !blStepA2 || !blStepB || !blStepB2)
                {
                    LTP.Common.MessageBox.Show(this.Page, "支出金额只能是数字！！");
                }
                else
                {
                    SanZi.BLL.PiBanKa bll = new SanZi.BLL.PiBanKa();
                    bll.SetCondition(float.Parse(strStepA), float.Parse(strStepB), int.Parse(strDeptID));
                    LTP.Common.MessageBox.ShowAndRedirect(this.Page, "支出金额设置成功！", "ConditionSet.aspx?deptid=1");
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            UserInfo.AccountID = AccountID.Value;
            ShowResult();
        }
    }
}
