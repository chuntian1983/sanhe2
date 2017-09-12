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
namespace SanZi.Web.pibanka
{
    public partial class Show : System.Web.UI.Page
    {        
        private string strPID;
        protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
                //strPID = Request.Params["id"].Trim().Replace("'","");
                this.lblDeptName.Text = Public.AccountName;
                strPID = Request.QueryString["pid"].Trim().Replace("'", "");
                if (LTP.Common.PageValidate.IsNumber(strPID) && strPID != "")
                {
                    int intPID = int.Parse(strPID);
                    ShowInfo(intPID);
                }
                else
                {
                    LTP.Common.MessageBox.ShowAndRedirect(this.Page,"参数有误！","index.aspx");
                }

			}
		}
		
        /// <summary>
        /// 显示批办卡信息
        /// </summary>
        /// <param name="PID"></param>
	    private void ShowInfo(int PID)
	    {
		    SanZi.BLL.PiBanKa bll=new SanZi.BLL.PiBanKa();
            DataTable dt = bll.GetPiBanKaByPid(PID);
            if (dt.Rows.Count == 0)
            {
                PageClass.ExcuteScript(this.Page, "alert('没有对应批办卡！');window.close()");
                return;
            }
            this.lblDeptName.Text = dt.Rows[0]["DeptName"].ToString();
            this.lblOutReason.Text = dt.Rows[0]["OutReason"].ToString();
            this.lblOutMoney.Text = dt.Rows[0]["OutMoney"].ToString();
            this.lblPassTime.Text = dt.Rows[0]["SubTime"].ToString();
            this.time1.Text = dt.Rows[0]["time1"].ToString();
            this.time2.Text = dt.Rows[0]["time2"].ToString();
            this.time3.Text = dt.Rows[0]["time3"].ToString();
            this.time4.Text = dt.Rows[0]["time4"].ToString();
            this.time5.Text = dt.Rows[0]["time5"].ToString();
            this.time6.Text = dt.Rows[0]["time6"].ToString();
            this.value1.Text = dt.Rows[0]["value1"].ToString();
            this.value2.Text = dt.Rows[0]["value2"].ToString();
            this.value3.Text = dt.Rows[0]["value3"].ToString();
            DataTable dt2 = bll.GetShenPiRen(PID);
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

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Write(" <script>window.opener=null;window.close(); </script>");// 不会弹出询问 
        }
    }
}
