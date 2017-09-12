using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SanZi.Web.view.PiBanKa
{
    public partial class createv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Button2.Attributes["onclick"] = "return check()";
                SanZi.BLL.PiBanKa bll = new SanZi.BLL.PiBanKa();
                DataTable dt = bll.GetPiBanKaByPid(TypeParse.StrToInt(Request.QueryString["pid"], 0));
                DebitSubject.Text = dt.Rows[0]["d"].ToString();
                CreditSubject.Text = dt.Rows[0]["c"].ToString();
                decimal sm0 = TypeParse.StrToDecimal(CommClass.GetSysPara("pibanka" + Request.QueryString["pid"]), 0);
                decimal sm1 = TypeParse.StrToDecimal(dt.Rows[0]["OutMoney"].ToString(), 0);
                decimal sm = sm1 - sm0;
                m0.Text = sm.ToString();
                UtilsPage.SetTextBoxReadOnly(m0);
                UtilsPage.SetTextBoxAutoValue(m1, 0);
                Notes.Text = dt.Rows[0]["zhaiyao"].ToString();
                addons.Value = dt.Rows[0]["lujing"].ToString();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add(DebitSubject.Text, m1.Text);
            Dictionary<string, string> c = new Dictionary<string, string>();
            c.Add(CreditSubject.Text, m1.Text);
            string subID = CommOutCall.CreateNewVoucher("RY", Notes.Text, d, c, addons.Value, true, true);
            SanZi.BLL.PiBanKa bll = new SanZi.BLL.PiBanKa();
            SanZi.Model.PiBanKa model = new SanZi.Model.PiBanKa();
            model.PID = int.Parse(Request.QueryString["pid"]);
            model.subid = subID;
            bll.Add1(model);
            decimal sm = TypeParse.StrToDecimal(CommClass.GetSysPara("pibanka" + Request.QueryString["pid"]), 0);
            sm += TypeParse.StrToDecimal(m1.Text, 0);
            CommClass.SetSysPara("pibanka" + Request.QueryString["pid"], sm.ToString());
            PageClass.ExcuteScript(this.Page, "alert('生成成功！');window.close()");
        }
    }
}