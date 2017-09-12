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
using System.Text;

public partial class AccountInit_WriteBalance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000002$000000")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            InitSubject(TreeView1.Nodes[0], "000");
            form1.Attributes.Add("onsubmit", "return SubmitForm();");
            CreditSubject.Value = SysConfigs.CreditSubject;
            CurSelSubject.Attributes.Add("readonly", "readonly");
            BalanceType.Attributes.Add("onchange", "SelectAccountType()");
            MonthBalance.Attributes.Add("onkeyup", "WriteBalance()");
            MonthBalance.Attributes.Add("onkeypress", "return CheckWrite();");
            UtilsPage.SetTextBoxAutoValue(MonthBalance, "0");
            UtilsPage.SetTextBoxAutoValue(SCount, "0");
            Button1.Attributes["onclick"] = "if($('CurSubjectNo').value==''){alert('请选择科目！');return false;}";
        }
    }

    protected void InitSubject(TreeNode _TreeNode, string ParentNo)
    {
        DataSet ds = CommClass.GetDataSet("select subjectno,subjectname from cw_subject where parentno='" + ParentNo + "' order by subjectno");
        if (ds.Tables[0].Rows.Count == 0)
        {
            if (ParentNo == "000") { return; }
            _TreeNode.SelectAction = TreeNodeSelectAction.None;
            string ClickStr = "SelectSubject('" + _TreeNode.Text.Replace(_TreeNode.Value + "&nbsp;.&nbsp;", "")
                + "[" + _TreeNode.Value + "]','" + _TreeNode.Value + "',0);";
            _TreeNode.Text = "<a href=\"###\" onclick=\"" + ClickStr + "\">" + _TreeNode.Text + "</a>";
        }
        else
        {
            _TreeNode.SelectAction = TreeNodeSelectAction.Expand;
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            _TreeNode.ChildNodes.Add(new TreeNode(ds.Tables[0].Rows[i]["subjectno"].ToString() + "&nbsp;.&nbsp;"
                + ds.Tables[0].Rows[i]["subjectname"].ToString(), ds.Tables[0].Rows[i]["subjectno"].ToString()));
            InitSubject(_TreeNode.ChildNodes[i], ds.Tables[0].Rows[i]["subjectno"].ToString());
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string CID = CommClass.GetFieldFromNo(CurSubjectNo.Value, "id");
        StringBuilder SQLString = new StringBuilder();
        string NewBalance = (MonthBalanceT.Value.Length == 0 || MonthBalanceT.Value == "0") ? "0" : (BalanceType.SelectedValue + MonthBalanceT.Value);
        string OldBalance = CommClass.GetFieldFromNo(CurSubjectNo.Value, "BeginBalance");
        if (NewBalance != OldBalance)
        {
            string ID = CID;
            decimal SubBalance = decimal.Parse(NewBalance) - decimal.Parse(OldBalance);
            while (true)
            {
                ID = CommClass.GetFieldFromNo(CommClass.GetFieldFromID(ID, "parentno"), "id");
                if (ID == "NoDataItem") { break; }
                SQLString.Append("update cw_subject set BeginBalance=BeginBalance+(" + SubBalance.ToString() + ") where id='" + ID + "'#sql#");
            }
        }
        SQLString.Append("update cw_subject set BeginBalance=" + NewBalance + " where id='" + CID + "'#sql#");
        CommClass.ExecuteTransaction(SQLString.ToString());
        if (AccountType.Value == "1")
        {
            //数量金额账录入
            DataSet ds = CommClass.GetDataSet("select * from cw_subjectdata where subjectid='" + CID + "'");
            if (ds.Tables[0].Rows.Count == 0)
            {
                DataRow row = ds.Tables[0].NewRow();
                row["subjectid"] = CID;
                row["amount"] = SCount.Text;
                row["balance"] = NewBalance;
                row["SUnit"] = SUnit.Text;
                row["SType"] = SType.Text;
                row["SClass"] = SClass.Text;
                ds.Tables[0].Rows.Add(row);
            }
            else
            {
                ds.Tables[0].Rows[0]["amount"] = SCount.Text;
                ds.Tables[0].Rows[0]["balance"] = NewBalance;
                ds.Tables[0].Rows[0]["SUnit"] = SUnit.Text;
                ds.Tables[0].Rows[0]["SType"] = SType.Text;
                ds.Tables[0].Rows[0]["SClass"] = SClass.Text;
            }
            CommClass.UpdateDataSet(ds);
        }
        ExeScript.Text = "<script>alert('期初余额录入成功！');</script>";
    }
}
