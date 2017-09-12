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

public partial class SysManage_AccountProgressSum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            SMinus.Attributes["onclick"] = "return setYear('SelYear',-1);";
            SPlus.Attributes["onclick"] = "return setYear('SelYear',1);";
            SelYear.Attributes["readonly"] = "readonly";
            SelYear.Text = DateTime.Now.Year.ToString();
            ///////////////////////////////////////////////////////////////////////////////////
            string UnitID = Session["UnitID"].ToString();
            string TopUnitID = ValidateClass.ReadXMLNodeText("FinancialDB/RegInfo", "TopUnitID");
            if (TopUnitID.Length == 0)
            {
                TopUnitID = "000000";
            }
            if (UnitID == "000000")
            {
                UnitID = TopUnitID;
            }
            TotalLevel.Value = ValidateClass.ReadXMLNodeText("FinancialDB/RegInfo", "LastLevel");
        }
        InitWebControl();
    }

    protected void InitWebControl()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("townid");
        dt.Columns.Add("townname");
        DataRow[] rows = ValidateClass.GetRegRows("CUnits", "UnitLevel='" + TotalLevel.Value + "'");
        foreach (DataRow row in rows)
        {
            DataRow nrow = dt.NewRow();
            nrow["townid"] = row["ID"];
            nrow["townname"] = row["UnitName"];
            dt.Rows.Add(nrow);
        }
        DataRow crow = dt.NewRow();
        crow["townid"] = "-";
        crow["townname"] = "合计";
        dt.Rows.Add(crow);
        GridView1.DataSource = dt.DefaultView;
        GridView1.DataBind();
        foreach (GridViewRow grow in GridView1.Rows)
        {
            string townid = grow.Cells[1].Text;
            if (townid == "-")
            {
                continue;
            }
            for (int i = 1; i <= 12; i++)
            {
                grow.Cells[i].Text = MainClass.GetTableValue("cw_account", "count(id)", string.Concat("unitid='", townid, "' and (LastCarryDate>='", SelYear.Text, "年", i.ToString("00"), "月') group by unitid"), "");
            }
            grow.Cells[13].Text = MainClass.GetTableValue("cw_account", "count(id)", string.Concat("unitid='", townid, "' group by unitid"), "");
        }
        int blength = GridView1.Rows.Count - 1;
        for (int i = 1; i <= 13; i++)
        {
            int c = 0;
            for (int k = 0; k < blength; k++)
            {
                c += TypeParse.StrToInt(GridView1.Rows[k].Cells[i].Text, 0);
            }
            if (c > 0)
            {
                GridView1.Rows[blength].Cells[i].Text = c.ToString();
            }
        }
        ReportTitle.InnerText = "做账进度（会计期间：" + SelYear.Text + "年）";
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //--
        }
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
}
