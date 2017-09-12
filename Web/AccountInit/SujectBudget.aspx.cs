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

public partial class AccountInit_SujectBudget : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000003$000000")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            //初始化需要设置全年预算的科目
            DataTable subject = CommClass.GetDataTable("select parentno,subjectno,subjectname from cw_subject where subjectno like '5%' or subjectno like '212%' order by subjectno");
            InitSubject(TreeView1.Nodes[0], subject);
            TreeView1.Nodes[0].Text = "预算科目";
            //
            DateTime ADate = MainClass.GetAccountDate();
            form1.Attributes.Add("onsubmit", "return SubmitForm();");
            CurSelSubject.Attributes.Add("readonly", "readonly");
            BudgetYear.Attributes.Add("readonly", "readonly");
            BudgetYear.Text = ADate.Year.ToString();
            UtilsPage.SetTextBoxAutoValue(BudgetBalance, "0");
            Button1.Attributes["onclick"] = "if($('CurSubjectNo').value==''){alert('请选择科目！');return false;}";
            if (ADate.Month == 1 || MainClass.GetFieldFromID(UserInfo.AccountID, "LastCarryDate", "cw_account").Length == 0)
            {
                DataTable budget = CommClass.GetDataTable(string.Concat("select SubjectNo,BudgetYear,BudgetBalance from cw_subjectbudget where BudgetYear='", BudgetYear.Text, "'"));
                budget.PrimaryKey = new DataColumn[] { budget.Columns["SubjectNo"] };
                foreach (TreeNode node in TreeView1.Nodes[0].ChildNodes)
                {
                    CheckSetBudget(node, budget);
                }
                CommClass.UpdateDataTable(budget);
                //写入操作日志
                CommClass.WriteCTL_Log("100012", "设置全年预算，年份：" + BudgetYear.Text);
            }
            else
            {
                Button1.Enabled = false;
            }
        }
    }

    private void CheckSetBudget(TreeNode node, DataTable budget)
    {
        if (budget.Rows.Find(node.Value) == null)
        {
            DataRow row = budget.NewRow();
            row["SubjectNo"] = node.Value;
            row["BudgetYear"] = BudgetYear.Text;
            row["BudgetBalance"] = 0;
            budget.Rows.Add(row);
        }
        foreach (TreeNode n in node.ChildNodes)
        {
            CheckSetBudget(n, budget);
        }
    }

    protected void InitSubject(TreeNode treeNode, DataTable subject)
    {
        DataRow[] rows = subject.Select(string.Concat("parentno='", treeNode.Value, "'"));
        if (rows.Length > 0)
        {
            treeNode.SelectAction = TreeNodeSelectAction.Expand;
            treeNode.Text = string.Format("{1}&nbsp;.&nbsp;{0}", treeNode.Text, treeNode.Value);
            foreach (DataRow row in rows)
            {
                TreeNode FNode = new TreeNode(row["subjectname"].ToString(), row["subjectno"].ToString());
                treeNode.ChildNodes.Add(FNode);
                InitSubject(FNode, subject);
            }
        }
        else
        {
            treeNode.SelectAction = TreeNodeSelectAction.None;
            treeNode.Text = string.Format("<a href=\"###\" onclick=\"SelectSubject('{0}[{1}]','{1}',0);\">{1}&nbsp;.&nbsp;{0}</a>", treeNode.Text, treeNode.Value);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (BudgetBalance.Text != BudgetBalanceOld.Value)
        {
            string subjectNo = CurSubjectNo.Value;
            StringBuilder SQLString = new StringBuilder();
            decimal SubBalance = decimal.Parse(BudgetBalance.Text) - decimal.Parse(BudgetBalanceOld.Value);
            while (true)
            {
                subjectNo = CommClass.GetFieldFromNo(subjectNo, "parentno");
                if (subjectNo == "NoDataItem" || subjectNo == "000") { break; }
                SQLString.AppendFormat("update cw_subjectbudget set budgetbalance=budgetbalance+({0}) where subjectno='{1}' and budgetyear='{2}'#sql#", SubBalance, subjectNo, BudgetYear.Text);
            }
            SQLString.AppendFormat("update cw_subjectbudget set budgetbalance={0} where subjectno='{1}' and budgetyear='{2}'#sql#", BudgetBalance.Text, CurSubjectNo.Value, BudgetYear.Text);
            CommClass.ExecuteTransaction(SQLString.ToString());
        }
        ExeScript.Text = "<script>alert('全年预算设置成功！');</script>";
    }
}
