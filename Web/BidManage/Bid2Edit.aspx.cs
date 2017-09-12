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

public partial class BidManage_Bid2Edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return checksubmit();");
            UtilsPage.SetTextBoxAutoValue(BiaoDiMoeny, 0);
            UtilsPage.SetTextBoxAutoValue(BidMoney, 0);
            UtilsPage.SetTextBoxCalendar(BidDate, "");
            UtilsPage.SetTextBoxCalendar(SDate, "");
            UtilsPage.SetTextBoxCalendar(EDate, "");
            if (Request.QueryString["id"] != null && Request.QueryString["id"].Length > 0)
            {
                TableID.Value = Request.QueryString["id"];
                DataRow brow = MainClass.GetDataRow("select * from projects where id='" + TableID.Value + "'");
                ProjectName.Text = brow["ProjectName"].ToString();
                ProjectType.Text = brow["ProjectType"].ToString();
                BiaoDiMoeny.Text = brow["BiaoDiMoeny"].ToString();
                BidMoney.Text = brow["BidMoney"].ToString();
                BidDate.Text = brow["BidDate"].ToString();
                SDate.Text = brow["SDate"].ToString();
                EDate.Text = brow["EDate"].ToString();
            }
            else
            {
                TableID.Value = Guid.NewGuid().ToString("N");
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> DicFeilds = new Dictionary<string, string>();
        DicFeilds.Add("ProjectName", ProjectName.Text);
        DicFeilds.Add("ProjectType", ProjectType.SelectedValue);
        DicFeilds.Add("BiaoDiMoeny", BiaoDiMoeny.Text);
        DicFeilds.Add("BidMoney", BidMoney.Text);
        DicFeilds.Add("BidDate", BidDate.Text);
        DicFeilds.Add("SDate", SDate.Text);
        DicFeilds.Add("EDate", EDate.Text);
        if (MainClass.CheckExist("projects", string.Concat("id='", TableID.Value, "'")))
        {
            MainClass.ExecuteSQL("projects", DicFeilds, string.Concat("id='", TableID.Value, "'"));
        }
        else
        {
            DicFeilds.Add("ID", TableID.Value);
            DicFeilds.Add("TownID", Session["TownID"].ToString());
            DicFeilds.Add("AccountID", UserInfo.AccountID);
            DicFeilds.Add("Booker", UserInfo.UserName);
            DicFeilds.Add("BookTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DicFeilds.Add("BookUnit", UserInfo.UnitID);
            MainClass.ExecuteSQL("projects", DicFeilds);
        }
        PageClass.ShowAlertMsg(this.Page, "保存成功！");
    }
}
