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

public partial class BillManage_CheckEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            UtilsPage.SetTextBoxAutoValue(BillPeriod, "0");
            UtilsPage.SetTextBoxCalendar(BillDate);
            DataTable subject = CommClass.GetDataTable("select subjectno,subjectname from cw_subject where parentno='102'");
            foreach (DataRow sub in subject.Rows)
            {
                string sno = string.Concat(sub["subjectno"].ToString(), ".", sub["subjectname"].ToString());
                BillBank.Items.Add(new ListItem(sno, sno));
            }
            DataRow brow = CommClass.GetDataRow("select * from cw_billcheck where id='" + Request.QueryString["bid"] + "'");
            BillBank.Text = brow["BillBank"].ToString();
            BillCurrency.Text = brow["BillCurrency"].ToString();
            BillType.Text = brow["BillType"].ToString();
            BillNo.Text = brow["BillNo"].ToString();
            BillDate.Text = brow["BillDate"].ToString();
            BillPeriod.Text = brow["BillPeriod"].ToString();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (CommClass.CheckExist("cw_billcheck", string.Concat("BillNo='", BillNo.Text, "' and id<>'", Request.QueryString["bid"], "'")))
        {
            PageClass.ShowAlertMsg(this.Page, string.Concat("支票编码[", BillNo.Text, "]已存在"));
        }
        else
        {
            Dictionary<string, string> DicFeilds = new Dictionary<string, string>();
            DicFeilds.Add("BillBank", BillBank.SelectedValue);
            DicFeilds.Add("BillCurrency", BillCurrency.Text);
            DicFeilds.Add("BillType", BillType.SelectedValue);
            DicFeilds.Add("BillDate", BillDate.Text);
            DicFeilds.Add("BillPeriod", BillPeriod.Text);
            if (BillNoPre.Text.Length > 0)
            {
                CommClass.ExecuteSQL("cw_billcheck", DicFeilds, string.Concat("UseState='0' and BillNo like '", BillNoPre.Text, "%'"));
            }
            DicFeilds.Add("BillNo", BillNo.Text);
            CommClass.ExecuteSQL("cw_billcheck", DicFeilds, string.Concat("id='", Request.QueryString["bid"], "'"));
            PageClass.ShowAlertMsg(this.Page, "支票保存成功！");
            RefreshFlag.Value = "1";
        }
    }
}
