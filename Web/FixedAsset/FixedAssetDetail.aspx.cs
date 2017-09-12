﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class FixedAsset_FixedAssetDetail : System.Web.UI.Page
{
    private int RunLevel = 0;
    private string QName = "cname";
    private string QTable = "cw_assetclass";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000013$100021")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            AName.Text = UserInfo.AccountName;
            ReportDate.Text = MainClass.GetAccountDate().ToString("yyyy年MM月");
            if (QType.SelectedValue == "0")
            {
                InitSubject("固定资产", SysConfigs.FixedAssetSubject);
            }
            else
            {
                InitDepartment();
            }
            InitWebControl();
        }
    }

    protected void InitDepartment()
    {
        QList.Items.Add(new ListItem(UserInfo.AccountName, "000000"));
        DataSet ds = CommClass.GetDataSet("select id," + QName + " from " + QTable + " order by id");
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            QList.Items.Add(new ListItem(ds.Tables[0].Rows[i][QName].ToString(), ds.Tables[0].Rows[i]["id"].ToString()));
        }
    }

    protected void InitSubject(string CName, string ParentID)
    {
        QList.Items.Add(new ListItem(new string('.', RunLevel * 6) + CName, ParentID));
        DataSet ds = CommClass.GetDataSet("select id,cname from cw_assetclass where pid='" + ParentID + "' order by id asc");
        RunLevel++;
        if (ParentID == SysConfigs.FixedAssetSubject) { RunLevel = 1; }
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            InitSubject(row["cname"].ToString(), row["id"].ToString());
        }
        RunLevel--;
    }

    /// <summary>
    /// 数据绑定函数
    /// </summary>
    protected void InitWebControl()
    {
        string QueryString = string.Empty;
        if (CardID.Text.Length > 0) { QueryString += "$cardid='" + CardID.Text + "'"; }
        if (AssetNo.Text.Length > 0) { QueryString += "$assetno='" + AssetNo.Text + "'"; }
        if (AssetName.Text.Length > 0) { QueryString += "$assetname like '%" + AssetName.Text + "%'"; }
        if (QType.SelectedValue == "0")
        {
            QueryString += "$ClassID like '" + QList.SelectedValue + "%'";
        }
        else
        {
            if (QList.SelectedValue != "000000")
            {
                QueryString += "$DeptName like '%" + QList.SelectedValue + "%'";
            }
        }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        DataSet ds = CommClass.GetDataSet("select * from cw_assetcard " + QueryString + " order by cardid desc");
        if (ds.Tables[0].Rows.Count == 0)
        {
            OutputDataToExcel.Enabled = false;
            PageClass.BindNoRecords(GridView1, ds);
        }
        else
        {
            OutputDataToExcel.Enabled = true;
            GridView1.AllowPaging = CheckBox2.Checked;
            GridView1.DataSource = ds.Tables[0].DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
            if (GridView1.AllowPaging)
            {
                Label lb = (Label)GridView1.BottomPagerRow.Cells[0].FindControl("ShowPageInfo");
                lb.Text = "记录数：" + ds.Tables[0].Rows.Count.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                lb.Text += "总页数：" + (GridView1.PageIndex + 1) + "/" + GridView1.PageCount + "页";
                DropDownList ddl = (DropDownList)GridView1.BottomPagerRow.Cells[0].FindControl("JumpPage");
                ddl.Items.Clear();
                for (int i = 0; i < GridView1.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem("第" + (i + 1).ToString() + "页", i.ToString()));
                }
                ddl.SelectedIndex = GridView1.PageIndex;
            }
        }
    }
    protected void JumpPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridView1.PageIndex = Convert.ToInt32(ddl.SelectedValue);
        InitWebControl();
    }
    protected void FirstPage_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        InitWebControl();
    }
    protected void PreviousPage_Click(object sender, EventArgs e)
    {
        if (GridView1.PageIndex > 0)
        {
            GridView1.PageIndex -= 1;
            InitWebControl();
        }
    }
    protected void NextPage_Click(object sender, EventArgs e)
    {
        if (GridView1.PageIndex < GridView1.PageCount)
        {
            GridView1.PageIndex += 1;
            InitWebControl();
        }
    }
    protected void LastPage_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = GridView1.PageCount;
        InitWebControl();
    }
    protected void QType_SelectedIndexChanged(object sender, EventArgs e)
    {
        QList.Items.Clear();
        if (QType.SelectedValue == "0")
        {
            QName = "cname";
            QTable = "cw_assetclass";
            InitSubject("固定资产", SysConfigs.FixedAssetSubject);
        }
        else
        {
            QName = "deptname";
            QTable = "cw_department";
            InitDepartment();
        }
        InitWebControl();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = e.Row.Cells[0].Text.Substring(e.Row.Cells[0].Text.IndexOf(".") + 1);
        }
    }
    protected void QList_SelectedIndexChanged(object sender, EventArgs e)
    {
        InitWebControl();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
    protected void OutputDataToExcel_Click(object sender, EventArgs e)
    {
        InitWebControl();
        PageClass.ToExcel(GridView1);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
}
