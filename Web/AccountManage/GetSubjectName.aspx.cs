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

public partial class AccountInit_GetSubjectName : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["SessionFlag"] == null)
        {
            Response.Write("$('v1" + Request.QueryString["row"] + "').value='';");
            Response.Write("$('" + Request.QueryString["cellid1"] + "').innerHTML='&nbsp;';");
            Response.Write("$('" + Request.QueryString["cellid2"] + "').innerHTML='&nbsp;';");
            Response.Write("WriteMoney(" + Request.QueryString["row"] + ",0);");
            Response.Write("alert('登录已过期，请重新登录！');");
            return;
        }
        string SubjectNo = Request.QueryString["no"];
        string IsDetail = CommClass.GetFieldFromNo(SubjectNo, "IsDetail");
        if (IsDetail == "NoDataItem")
        {
            Response.Write("$('v1" + Request.QueryString["row"] + "').value='';");
            Response.Write("$('" + Request.QueryString["cellid1"] + "').innerHTML='&nbsp;';");
            Response.Write("$('" + Request.QueryString["cellid2"] + "').innerHTML='&nbsp;';");
            Response.Write("WriteMoney(" + Request.QueryString["row"] + ",0);");
            Response.Write("alert('科目表中无此科目【" + Request.QueryString["no"] + "】！');");
        }
        else
        {
            if (IsDetail == "0")
            {
                Response.Write("$('v1" + Request.QueryString["row"] + "').value='';");
                Response.Write("$('" + Request.QueryString["cellid1"] + "').innerHTML='&nbsp;';");
                Response.Write("$('" + Request.QueryString["cellid2"] + "').innerHTML='&nbsp;';");
                Response.Write("WriteMoney(" + Request.QueryString["row"] + ",0);");
                Response.Write("alert('此科目【" + Request.QueryString["no"] + "】非明细科目！');");
            }
            else
            {
                string MSubject = CommClass.GetFieldFromNo(SubjectNo.Substring(0, 3), "SubjectName");
                Response.Write("$('" + Request.QueryString["cellid1"] + "').innerHTML='" + MSubject + "';");
                if (SubjectNo.Length > 3)
                {
                    Response.Write("$('" + Request.QueryString["cellid2"] + "').innerHTML='" + CommClass.GetDetailSubject(SubjectNo) + "';");
                }
                else
                {
                    Response.Write("$('" + Request.QueryString["cellid2"] + "').innerHTML='&nbsp;';");
                }
                //获取科目当前余额
                string ThisMonth = MainClass.GetAccountDate().ToString("yyyy年MM月");
                string BeginBalance = CommClass.GetFieldFromNo(SubjectNo, "BeginBalance");
                string ThisBalance = CommClass.GetTableValue("cw_voucherentry", "sum(summoney)", string.Format("subjectno='{0}' and voucherdate like '{1}%'", SubjectNo, ThisMonth));
                decimal TotalBalance = TypeParse.StrToDecimal(BeginBalance, 0) + TypeParse.StrToDecimal(ThisBalance, 0);
                Response.Write("$('v3" + Request.QueryString["row"] + "').value='" + TotalBalance.ToString() + "';");
            }
        }
    }
}
