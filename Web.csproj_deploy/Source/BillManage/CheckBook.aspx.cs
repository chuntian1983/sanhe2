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

public partial class BillManage_CheckBook : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            UtilsPage.SetTextBoxAutoValue(BillPeriod, "0");
            UtilsPage.SetTextBoxAutoValue(BillCount, "0");
            UtilsPage.SetTextBoxCalendar(BillDate);
            DataTable subject = CommClass.GetDataTable("select subjectno,subjectname from cw_subject where parentno='102'");
            foreach (DataRow sub in subject.Rows)
            {
                string sno = string.Concat(sub["subjectno"].ToString(), ".", sub["subjectname"].ToString());
                BillBank.Items.Add(new ListItem(sno, sno));
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> DicFeilds = new Dictionary<string, string>();
        DicFeilds.Add("BillBank", BillBank.SelectedValue);
        DicFeilds.Add("BillCurrency", BillCurrency.Text);
        DicFeilds.Add("BillType", BillType.SelectedValue);
        DicFeilds.Add("BillDate", BillDate.Text);
        DicFeilds.Add("BillPeriod", BillPeriod.Text);
        DicFeilds.Add("UseState", "0");
        DicFeilds.Add("BillNo", "");
        DicFeilds.Add("ID", "");
        string format = new string('0', BillNoStart.Text.Length);
        int count = int.Parse(BillCount.Text);
        int start = int.Parse(BillNoStart.Text);
        int end = start + count;
        for (int i = start; i < end; i++)
        {
            DicFeilds["ID"] = CommClass.GetRecordID("cw_billcheck");
            DicFeilds["BillNo"] = BillNoPre.Text + i.ToString(format);
            CommClass.ExecuteSQL("cw_billcheck", DicFeilds);
        }
        BillNoStart.Text = "";
        BillCount.Text = "0";
        RefreshFlag.Value = "1";
        PageClass.ShowAlertMsg(this.Page, "支票登记成功！");
    }
}
