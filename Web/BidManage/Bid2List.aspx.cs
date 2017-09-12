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
using System.Text;

public partial class BidManage_Bid2List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            SDate.Attributes["onclick"] = string.Concat("WdatePicker({dateFmt:'yyyy-MM',alwaysUseStartDate:true})");
            EDate.Attributes["onclick"] = string.Concat("WdatePicker({dateFmt:'yyyy-MM',alwaysUseStartDate:true})");
            UtilsPage.SetTextBoxReadOnly(SDate);
            UtilsPage.SetTextBoxReadOnly(EDate);
            UtilsPage.SetTextBoxAutoValue(BidMoney, 0);
            InitWebControl();
        }
    }
    /// <summary>
    /// 数据绑定函数
    /// </summary>
    protected void InitWebControl()
    {
        string QueryString = "$AccountID='" + UserInfo.AccountID + "'$BookUnit='" + UserInfo.UnitID + "'";
        if (ProjectType.SelectedValue != "-")
        {
            QueryString += "$ProjectType='" + ProjectType.SelectedValue + "'";
        }
        if (BidMoney.Text.Length > 0)
        {
            QueryString += "$BidMoney>=" + BidMoney.Text;
        }
        if (SDate.Text.Length > 0)
        {
            QueryString += "$SDate>='" + SDate.Text + "-01'";
        }
        if (EDate.Text.Length > 0)
        {
            QueryString += "$EDate<='" + EDate.Text + "-31'";
        }
        if (DueType.SelectedValue != "-")
        {
            if (DueType.SelectedValue == "0")
            {
                QueryString += "$EDate>='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            }
            else
            {
                QueryString += "$EDate<='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            }
        }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        DataTable data = MainClass.GetDataTable("select * from projects " + QueryString + " order by id");
        if (data.Rows.Count == 0)
        {
            PageClass.BindNoRecords(GridView1, data);
        }
        else
        {
            GridView1.DataSource = data.DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
            Label lb = (Label)GridView1.BottomPagerRow.Cells[0].FindControl("ShowPageInfo");
            lb.Text = "记录数：" + data.Rows.Count.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        MainClass.ExecuteSQL("delete from projects where id='" + btn.CommandArgument + "'");
        MainClass.ExecuteSQL("delete from projattachs where ProjectID='" + btn.CommandArgument + "'");
        GridView1.EditIndex = -1;
        InitWebControl();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
            btnEdit.Attributes["onclick"] = "location.href='Bid2Edit.aspx?id=" + btnEdit.CommandArgument + "';return false";
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            btnDelete.Attributes["onclick"] = "return confirm('您确定删除吗？')";
            e.Row.Attributes["onmouseover"] = "bgColor=this.style.backgroundColor;this.style.backgroundColor='#dad5cc';fontColor=this.style.color;this.style.color='red';";
            e.Row.Attributes["onmouseout"] = "this.style.backgroundColor=bgColor;this.style.color=fontColor;";
            HiddenField hidEDate = (HiddenField)e.Row.FindControl("hidEDate");
            e.Row.Cells[5].Text += "～" + hidEDate.Value;
            e.Row.Cells[0].Text = string.Concat("<a href='FileShow.aspx?pid=", btnEdit.CommandArgument, "&step=0' target=_blank>", e.Row.Cells[0].Text, "</a>");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
}
