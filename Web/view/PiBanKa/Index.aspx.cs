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
using System.Collections.Generic;
using LTP.Common;

namespace SanZi.Web.pibanka
{
    public partial class index : System.Web.UI.Page
    {
        string strPID = string.Empty;
        string strAction = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            PiBanKaList(string.Empty);
            if (Request.QueryString["pid"] != null && Request.QueryString["act"] != null)
            {
                strPID = Request.QueryString["pid"].Trim();
                strAction = Request.QueryString["act"].Trim();
                if (strPID != "" && strAction != "")
                {
                    DelPiBanKa(strPID, strAction);
                }
            }

        }

        public void PiBanKaList(string strKeyword)
        {
            SanZi.BLL.Users bll = new SanZi.BLL.Users();
            DataView dv = new DataView(bll.GetPiBanKaList(strKeyword).Tables[0]);
            AspNetPager1.RecordCount = dv.Count;

            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = dv;
            pds.AllowPaging = true;
            pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
            pds.PageSize = AspNetPager1.PageSize;
            rpFileManage.DataSource = dv;
            rpFileManage.DataBind();
            for (int i = 0; i < rpFileManage.Items.Count; i++)
            {
                Label lab = (Label)rpFileManage.Items[i].FindControl("Label1");
                lab.Text = Public.AccountName;
                HiddenField hidd = (HiddenField)rpFileManage.Items[i].FindControl("HiddenField1");
                LinkButton lbn = (LinkButton)rpFileManage.Items[i].FindControl("LinkButton1");
                if (lbn.CommandName.Length > 0)
                {
                    //lbn.Text = "查看凭证";
                    //lbn.Attributes["onclick"] = "return ShowVoucher('" + lbn.CommandName + "','" + hidd.Value + "')";
                }
                lbn.Attributes["onclick"] = "return CVoucher('" + hidd.Value + "')";
            }
        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            PiBanKaList("");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            string strKeyword = this.txtKeyword.Text.Trim();
            PiBanKaList(strKeyword);
        }

        private void DelPiBanKa(string strPID, string strAction)
        {
            if (strAction == "del")
            {
                if (LTP.Common.PageValidate.IsNumber(strPID))
                {
                    int pid = int.Parse(strPID);
                    SanZi.BLL.PiBanKa bll = new SanZi.BLL.PiBanKa();
                    bll.DelPiBanKa(pid);
                    Page.RegisterStartupScript("ts", "<script  language=javascript>window.location.href='index.aspx';</script>");
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton lbn = (LinkButton)sender;
            string[] str = lbn.CommandArgument.Split('|');
            //调用示例：
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add(str[1], str[0]);
            Dictionary<string, string> c = new Dictionary<string, string>();
            c.Add(str[2], str[0]);
            string subID = CommOutCall.CreateNewVoucher("RY", str[3], d, c, str[5], true, true);
            SanZi.BLL.PiBanKa bll = new SanZi.BLL.PiBanKa();
            SanZi.Model.PiBanKa model = new SanZi.Model.PiBanKa();
            model.PID = int.Parse(str[4]);
            model.subid = subID;
            bll.Add1(model);
            Response.Redirect("Index.aspx");
        }
    }
}
