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
namespace SanZi.Web.daili
{
    public partial class Show : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					int id = int.Parse(Request.Params["id"]);
                    ShowInfo(id);
				}
			}
		}

        private void ShowInfo(int id)
	    {
            SanZi.BLL.DaiLi bll = new SanZi.BLL.DaiLi();
            DataTable dt = bll.GetDaiLiByPid(id);
            if (dt.Rows.Count > 0)
            {
                this.lblDeptName.Text = dt.Rows[0]["DeptName"].ToString();
                this.lblApplyDate.Text = dt.Rows[0]["ApplyDate"].ToString();
                this.lblBackGround.Text = dt.Rows[0]["ZhiBu_YuanYin"].ToString();
                this.lblProjectName.Text = dt.Rows[0]["ProjectName"].ToString();
                this.lblMoney.Text = dt.Rows[0]["EstimateValue"].ToString();
                this.lblProjectType.Text = dt.Rows[0]["ProjectType"].ToString();
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

	    }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Write(" <script>window.opener=null;window.close(); </script>");// 不会弹出询问 
        }


    }
}
