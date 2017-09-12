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
using System.Collections.Generic;

public partial class AccountQuery_CommReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (Request.QueryString["DesignID"])
        {
            case "000003":
                ReportTitle.InnerText = "收 支 明 细 表";
                if (!PageClass.CheckVisitQuot("000009$100008")) { return; }
                break;
            case "000004":
                ReportTitle.InnerText = "资 产 负 债 表";
                if (!PageClass.CheckVisitQuot("000009$100009")) { return; }
                break;
            case "000006":
                ReportTitle.InnerText = "财 务 公 开 榜";
                if (!PageClass.CheckVisitQuot("000009$100012")) { return; }
                break;
        }
        if (!IsPostBack)
        {
            AName.Text = UserInfo.AccountName;
            GridView1.Attributes.Add("onselectstart", "return false;");
            SaveSheet.Attributes["onclick"] = "return confirm('您确定需要保存报表参数设置吗？');";
            GetTemplate.Attributes["onclick"] = "return confirm('您确定需要执行取自模板库操作吗？');";
            form1.Attributes["onsubmit"] = "_OnSubmit();";
            CellText.Attributes["onblur"] = "wCellText()";
            DateTime AccountDate = MainClass.GetAccountDate();
            ReportDate.Text = AccountDate.ToString("yyyy年MM月");
            ReportDate.Attributes["readonly"] = "readonly";
            SelYear.Attributes["onchange"] = "OnSelChange(this.value,0);";
            SelMonth.Attributes["onchange"] = "OnSelChange(this.value,1);";
            MainClass.InitAccountYear(SelYear);
            SelYear.Text = AccountDate.Year.ToString();
            SelMonth.Text = AccountDate.Month.ToString("00");
            if (Session["Powers"].ToString().IndexOf("000015") == -1)
            {
                SaveSheet.Enabled = false;
                GetTemplate.Enabled = false;
            }
            InitWebControl();
            //写入操作日志
            CommClass.WriteCTL_Log("100016", "报表查询：" + ReportTitle.InnerText.Replace(" ", "")); 
            //--
        }
    }
    protected void InitWebControl()
    {
        curRowID.Value = "";
        curRowIndex.Value = "";
        CommClass.InitGridView(Request.QueryString["DesignID"], ReportDate.Text, GridView1);
        if (GridView1.Rows.Count < 35)
        {
            GridView1.RowStyle.Height = 800 / GridView1.Rows.Count;
        }
        RowsCount.Value = GridView1.Rows.Count.ToString();
        if (Session["Powers"].ToString().IndexOf("000015") == -1)
        {
            for (int i = 1; i < GridView1.Rows.Count; i++)
            {
                GridView1.Rows[i].Attributes.Remove("onclick");
                for (int k = 0; k < GridView1.Columns.Count; k++)
                {
                    GridView1.Rows[i].Cells[k].Attributes.Remove("onclick");
                    GridView1.Rows[i].Cells[k].Attributes.Remove("ondblclick");
                }
            }
        }
    }
    protected void QDataClick_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
    protected void SaveSheet_Click(object sender, EventArgs e)
    {
        CommClass.SaveGridView(Request.QueryString["DesignID"], RowItemText.Value, GridViewRowFlag.Value);
        InitWebControl();
    }
    protected void GetTemplate_Click(object sender, EventArgs e)
    {
        CommClass.GetTemplate(Request.QueryString["DesignID"]);
        InitWebControl();
    }
    protected void OutputDataToExcel_Click(object sender, EventArgs e)
    {
        PageClass.ToExcel(GridView1);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
}
