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

public partial class AccountManage_AddEntryData : System.Web.UI.Page
{
    DataSet EntryData;
    string DataSetName = "EntryData" + HttpContext.Current.Request.QueryString["row"].PadLeft(6, '0');

    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            SubjectNo.Text = Request.QueryString["no"].ToString();
            Button1.Attributes.Add("onclick", "return CheckMoney();");
            SubjectName.Text = CommClass.GetFieldFromNo(Request.QueryString["no"].ToString(), "SubjectName");
            if (SubjectName.Text == "NoDataItem")
            {
                Response.Clear();
                Response.Write("<script>window.close();alert('科目不存在！');</script>");
                Response.End();
            }
            AccountType.Value = CommClass.GetFieldFromNo(SubjectNo.Text, "AccountType");
            EntryMoney.Attributes.Add("onkeypress", "return (event.keyCode>=48&&event.keyCode<=57)||event.keyCode==46;");
            string money = Request.QueryString["money"].ToString().Trim();
            if (money.Length > 0)
            {
                EntryMoney.Text = money;
                BalanceType.Text = EntryMoney.Text.Substring(0, 1);
            }
            if (EntryMoney.Text.StartsWith("+") || EntryMoney.Text.StartsWith("-"))
            {
                EntryMoney.Text = EntryMoney.Text.Substring(1);
            }
            if (AccountType.Value == "0")
            {
                CanUse.Enabled = false;
                ESummary.Enabled = false;
                Balance.Enabled = false;
                Amount.Enabled = false;
                SUnit.Enabled = false;
                BalanceType.AutoPostBack = false;
                UtilsPage.SetTextBoxAutoValue(EntryMoney, "0");
                PageClass.ExcuteScript(this.Page, "$('Button3').disabled='disabled';");
            }
            else
            {
                if (Session[DataSetName] == null || Request.QueryString["row"].IndexOf("Q") != -1)
                {
                    Session[DataSetName] = CommClass.GetDataSet("select * from cw_entrydata where entryid='" + Request.QueryString["id"] + "' order by id");
                }
                string subjectID = CommClass.GetTableValue("cw_subject", "id", string.Format("subjectno='{0}'", SubjectNo.Text));
                SUnit.Text = CommClass.GetTableValue("cw_subjectdata", "SUnit", string.Format("SubjectID='{0}'", subjectID));
                if (SUnit.Text == "NoDataItem")
                {
                    SUnit.Text = "";
                }
                hidSUnit.Value = SUnit.Text;
                UtilsPage.SetTextBoxAutoValue(Balance, "0");
                UtilsPage.SetTextBoxAutoValue(Amount, "0");
                InitWebControl();
            }
        }
    }
    /// <summary>
    /// 数据绑定函数
    /// </summary>
    private void InitWebControl()
    {
        int NowRowCount = 0;
        EntryData = (DataSet)Session[DataSetName];
        for (int i = 0; i < EntryData.Tables[0].Rows.Count; i++)
        {
            if (EntryData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
            {
                NowRowCount++;
            }
        }
        if (NowRowCount == 0)
        {
            PageClass.BindNoRecords(GridView1, EntryData);
            VSummary.Value = "";
        }
        else
        {
            GridView1.DataSource = EntryData.Tables[0].DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
            Label lb = (Label)GridView1.BottomPagerRow.Cells[0].FindControl("ShowPageInfo");
            lb.Text = "记录数：" + EntryData.Tables[0].Rows.Count.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lb.Text += "总页数：" + (GridView1.PageIndex + 1) + "/" + GridView1.PageCount + "页";
            DropDownList ddl = (DropDownList)GridView1.BottomPagerRow.Cells[0].FindControl("JumpPage");
            ddl.Items.Clear();
            for (int i = 0; i < GridView1.PageCount; i++)
            {
                ddl.Items.Add(new ListItem("第" + (i + 1).ToString() + "页", i.ToString()));
            }
            ddl.SelectedIndex = GridView1.PageIndex;
        }
        Button1.Enabled = true;
        EntryMoney.Attributes["readonly"] = "readonly";
        decimal _EntryMoney = 0;
        object sumMoney = EntryData.Tables[0].Compute("sum(balance)", "");
        if (sumMoney != null)
        {
            _EntryMoney = TypeParse.StrToDecimal(sumMoney, 0);
        }
        if (_EntryMoney >= 0)
        {
            EntryMoney.Text = _EntryMoney.ToString();
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState.ToString().IndexOf("Edit") == -1)
        {
            if (e.Row.RowIndex == 0 && e.Row.Cells[0].Text != "&nbsp;")
            {
                VSummary.Value = e.Row.Cells[0].Text;
            }
            if (e.Row.Cells[2].Text == "0.00")
            {
                e.Row.Cells[1].Text = "0.00";
            }
            else
            {
                decimal a = TypeParse.StrToDecimal(e.Row.Cells[2].Text,0);
                decimal b = TypeParse.StrToDecimal(e.Row.Cells[4].Text,0);
                decimal p = b / a;
                e.Row.Cells[1].Text = p.ToString("#0.00");
            }
            e.Row.Cells[3].Text = BalanceType.SelectedItem.Text;
            LinkButton edtDelete = (LinkButton)e.Row.Cells[5].FindControl("edtDelete");
            edtDelete.Attributes.Add("onclick", string.Concat("return EditRow('", GridView1.DataKeys[e.Row.RowIndex].Value.ToString()
                , "','", e.Row.Cells[0].Text, "','", e.Row.Cells[4].Text, "','", e.Row.Cells[2].Text, "')"));
            LinkButton btnDelete = (LinkButton)e.Row.Cells[6].FindControl("btnDelete");
            btnDelete.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除“" + e.Row.Cells[0].Text + "”吗？')");
            if (Request.QueryString["row"].IndexOf("Q") != -1)
            {
                e.Row.Cells[5].Enabled = false;
                e.Row.Cells[6].Enabled = false;
            }
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "btnDelete")
        {
            LinkButton btnDelete = (LinkButton)e.CommandSource;
            EntryData = (DataSet)Session[DataSetName];
            EntryData.Tables[0].PrimaryKey = new DataColumn[] { EntryData.Tables[0].Columns["id"] };
            EntryData.Tables[0].Rows.Find(btnDelete.CommandArgument).Delete();
            Session[DataSetName] = EntryData;
            ESummary.Text = "";
            Amount.Text = "0";
            Balance.Text = "0";
            EditID.Value = "";
            ESummary.Focus();
            InitWebControl();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        DataRow newRow;
        EntryData = (DataSet)Session[DataSetName];
        if (EditID.Value == "")
        {
            newRow = EntryData.Tables[0].NewRow();
        }
        else
        {
            EntryData.Tables[0].PrimaryKey = new DataColumn[] { EntryData.Tables[0].Columns["id"] };
            newRow = EntryData.Tables[0].Rows.Find((object)EditID.Value);
        }
        newRow["id"] = CommClass.GetRecordID("CW_EntryData");
        newRow["entryid"] = Request.QueryString["id"];
        newRow["esummary"] = ESummary.Text;
        newRow["amount"] = Amount.Text;
        newRow["balance"] = Balance.Text;
        if (EditID.Value == "")
        {
            EntryData.Tables[0].Rows.Add(newRow);
        }
        Session[DataSetName] = EntryData;
        ESummary.Text = "";
        Amount.Text = "0";
        Balance.Text = "0";
        EditID.Value = "";
        ESummary.Focus();
        //修改计量单位
        if (hidSUnit.Value != SUnit.Text)
        {
            string subjectID = CommClass.GetTableValue("cw_subject", "id", string.Format("subjectno='{0}'", SubjectNo.Text));
            if (CommClass.CheckExist("cw_subjectdata", string.Format("subjectid='{0}'", subjectID)))
            {
                CommClass.ExecuteSQL(string.Format("update cw_subjectdata set SUnit='{1}' where SubjectID='{0}'", subjectID, SUnit.Text));
            }
            else
            {
                CommClass.ExecuteSQL(string.Format("insert into cw_subjectdata(SubjectID,Amount,Balance,SUnit)values('{0}','0','0','{1}')", subjectID, SUnit.Text));
            }
        }
        InitWebControl();
    }
    protected void BalanceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        InitWebControl();
    }
}
