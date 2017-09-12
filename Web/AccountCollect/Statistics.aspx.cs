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

public partial class AccountCollect_Statistics : System.Web.UI.Page
{
    private int nodeDepth = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            Overlay.Attributes["style"] = "display:none";
            Lightbox.Attributes["style"] = "display:none";
            Button1.Attributes.Add("onclick", "return SubmitForm();");
            StatisticExpr.Attributes["readonly"] = "readonly";
            StatisticExpr.Attributes["ondblclick"] = "SelectExpr('StatisticExpr');";
            SMinus.Attributes["onclick"] = "return setYear('SelYear',-1);";
            SPlus.Attributes["onclick"] = "return setYear('SelYear',1);";
            SelYear.Attributes["readonly"] = "readonly";
            SelYear.Text = DateTime.Now.Year.ToString();
            SelMonth.Text = DateTime.Now.Month.ToString("00");
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
            if (UnitID != TopUnitID)
            {
                int UnitLevel = 0;
                string mylevel = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + UnitID + "']", "UnitLevel");
                if (mylevel.Length > 0)
                {
                    UnitLevel = int.Parse(TotalLevel.Value) - int.Parse(mylevel);
                }
                TotalLevel.Value = UnitLevel.ToString();
            }
            nodeDepth = int.Parse(TotalLevel.Value);
            ///////////////////////////////////////////////////////////////////////////////////
            DataTable UnitTable = ValidateClass.GetRegTable("CUnits");
            InitUnitList(TreeView1.Nodes[0], UnitID, UnitTable);
            TreeView1.Nodes[0].Text = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + UnitID + "']", "UnitName");
            TreeView1.Nodes[0].Value = UnitID;
            TreeView1.Nodes[0].ImageUrl = "../Images/dopen.gif";
            if (TotalLevel.Value == "0")
            {
                DataTable dt = MainClass.GetDataTable("select id,accountname,accountdate from cw_account where unitid='" + TreeView1.Nodes[0].Value + "' order by levelid,id");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TreeNode tn = new TreeNode(dt.Rows[i]["accountname"].ToString(), "$" + dt.Rows[i]["id"].ToString());
                    tn.SelectAction = TreeNodeSelectAction.Select;
                    TreeView1.Nodes[0].ChildNodes.Add(tn);
                }
            }
        }
        nodeDepth = int.Parse(TotalLevel.Value);
        if (TreeView1.SelectedNode != null)
        {
            if (!TreeView1.SelectedNode.Value.StartsWith("$"))
            {
                TreeView1.SelectedNode.Expand();
                TreeView1.SelectedNode.ImageUrl = "../Images/dopen.gif";
                //TreeView1.SelectedNode.ToggleExpandState();
                //if ((bool)TreeView1.SelectedNode.Expanded.Value)
                //{
                //    TreeView1.SelectedNode.ImageUrl = "../Images/dopen.gif";
                //}
                //else
                //{
                //    TreeView1.SelectedNode.ImageUrl = "../Images/dclose.gif";
                //}
            }
        }
    }

    protected void InitUnitList(TreeNode _TreeNode, string ParentID, DataTable UnitTable)
    {
        _TreeNode.ImageUrl = "../Images/dclose.gif";
        _TreeNode.SelectAction = TreeNodeSelectAction.Select;
        DataView DV = new DataView(UnitTable, "parentid='" + ParentID + "'", "", DataViewRowState.CurrentRows);
        for (int i = 0; i < DV.Count; i++)
        {
            _TreeNode.ChildNodes.Add(new TreeNode(DV[i]["unitname"].ToString(), DV[i]["id"].ToString()));
            InitUnitList(_TreeNode.ChildNodes[_TreeNode.ChildNodes.Count - 1], DV[i]["id"].ToString(), UnitTable);
        }
    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        StatisticUnit.Text = TreeView1.SelectedNode.Text;
        if (TreeView1.SelectedNode.Value.StartsWith("$"))
        {
            GAccountList.Value = TreeView1.SelectedNode.Value.Substring(1);
        }
        else
        {
            if (TotalLevel.Value != "0") { InitTreeNode(TreeView1.Nodes[0]); }
            if (TreeView1.SelectedNode.Depth == nodeDepth && TreeView1.SelectedNode.ChildNodes.Count == 0)
            {
                DataSet ds = MainClass.GetDataSet("select id,accountname,accountdate from cw_account where unitid='" + TreeView1.SelectedNode.Value + "' order by levelid,id");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    TreeNode tn = new TreeNode(ds.Tables[0].Rows[i]["accountname"].ToString(), "$" + ds.Tables[0].Rows[i]["id"].ToString());
                    tn.SelectAction = TreeNodeSelectAction.Select;
                    TreeView1.SelectedNode.ChildNodes.Add(tn);
                }
            }
            StringBuilder AllAccount = new StringBuilder();
            GetCollectAccount(ref AllAccount, TreeView1.SelectedNode.Value);
            GAccountList.Value = AllAccount.ToString();
        }
        //Button1_Click(Button1, new EventArgs());
    }

    private void InitTreeNode(TreeNode _TreeNode)
    {
        foreach (TreeNode node in _TreeNode.ChildNodes)
        {
            if (TreeView1.SelectedNode.ValuePath.IndexOf(node.ValuePath) == -1 && node.Depth == nodeDepth && node.ChildNodes.Count > 0)
            {
                node.ChildNodes.Clear();
            }
            else
            {
                InitTreeNode(node);
            }
            if (TreeView1.SelectedNode.ValuePath.IndexOf(node.Value) == -1 && node.Depth <= nodeDepth)
            {
                node.ImageUrl = "../Images/dclose.gif";
                node.CollapseAll();
            }
        }
    }

    protected void GetCollectAccount(ref StringBuilder AllAccount, string UnitID)
    {
        DataSet ds = MainClass.GetDataSet("select id from cw_account where unitid='" + UnitID + "'");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            AllAccount.Append(row["id"].ToString() + "$");
        }
        DataRow[] rows = ValidateClass.GetRegRows("CUnits", "parentid='" + UnitID + "'");
        if (rows != null)
        {
            for (int i = 0; i < rows.Length; i++)
            {
                GetCollectAccount(ref AllAccount, rows[i]["id"].ToString());
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ClsCalculate clsCalculate = new ClsCalculate();
        clsCalculate.DesignID = "000000";
        clsCalculate.AccountList = GAccountList.Value;
        clsCalculate.ReportDate = string.Format("{0}年{1}月", SelYear.Text, SelMonth.SelectedValue);
        StatisticValue.Text = decimal.Parse(clsCalculate.GetExprValue(StatisticExpr.Text).ToString()).ToString("#0.00");
    }
}
