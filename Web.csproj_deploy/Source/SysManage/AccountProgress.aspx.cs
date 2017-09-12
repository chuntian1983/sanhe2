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

public partial class SysManage_AccountProgress : System.Web.UI.Page
{
    private int nodeDepth = 0;

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
            if (UnitID != TopUnitID)
            {
                int UnitLevel = 0;
                string mylevel = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + UnitID + "']", "UnitLevel");
                if (mylevel.Length > 0)
                {
                    UnitLevel = int.Parse(TotalLevel.Value) - int.Parse(mylevel);
                }
                TotalLevel.Value = UnitLevel.ToString();
                nodeDepth = UnitLevel;
            }
            nodeDepth = int.Parse(TotalLevel.Value);
            ///////////////////////////////////////////////////////////////////////////////////
            InitSubject(TreeView1.Nodes[0], UnitID, "0");
            TreeView1.Nodes[0].Text = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + UnitID + "']", "UnitName");
            TreeView1.Nodes[0].Value = UnitID;
            TreeView1.Nodes[0].ImageUrl = "../Images/dopen.gif";
            if (TotalLevel.Value == "0")
            {
                TreeView1.Nodes[0].Selected = true;
            }
        }
        nodeDepth = int.Parse(TotalLevel.Value);
        InitWebControl();
    }

    protected void InitSubject(TreeNode _TreeNode, string UnitID, string UnitLevel)
    {
        DataRow[] rows = ValidateClass.GetRegRows("CUnits", "parentid='" + UnitID + "'");
        if (_TreeNode.Depth == nodeDepth)
        {
            _TreeNode.SelectAction = TreeNodeSelectAction.Select;
        }
        else
        {
            if (rows == null ? true : rows.Length == 0)
            {
                _TreeNode.SelectAction = TreeNodeSelectAction.None;
            }
            else
            {
                _TreeNode.SelectAction = TreeNodeSelectAction.Expand;
            }
        }
        for (int i = 0; i < rows.Length; i++)
        {
            _TreeNode.ChildNodes.Add(new TreeNode(rows[i]["UnitName"].ToString(), rows[i]["ID"].ToString()));
            InitSubject(_TreeNode.ChildNodes[i], rows[i]["ID"].ToString(), rows[i]["UnitLevel"].ToString());
        }
    }

    protected void InitWebControl()
    {
        string QueryString = string.Empty;
        if (TotalLevel.Value == "0")
        {
            QueryString = " where unitid='" + Session["UnitID"].ToString() + "'";
        }
        else
        {
            if (TreeView1.SelectedNode != null && TreeView1.SelectedNode.Depth.ToString() == TotalLevel.Value)
            {
                QueryString = " where unitid='" + TreeView1.SelectedNode.Value + "'";
            }
            else
            {
                QueryString = " where unitid='XXXXXX'";
            }
        }
        DataSet ds = MainClass.GetDataSet("select id,accountname,startaccountdate,lastcarrydate from cw_account " + QueryString + " order by levelid,id");
        if (ds.Tables[0].Rows.Count == 0)
        {
            PageClass.BindNoRecords(GridView1, ds);
        }
        else
        {
            GridView1.DataSource = ds.Tables[0].DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
        }
        GridView1.HeaderRow.Cells[1].Text = "做账进度（会计期间：" + SelYear.Text + "年）";
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[1].Text == "&nbsp;" || e.Row.Cells[2].Text == "&nbsp;")
            {
                e.Row.Cells[1].Text = "<table cellpadding=\"0\" cellspacing=\"0\" style=\"width: 345px; text-align:center\"><tr>";
                for (int i = 1; i <= 12; i++)
                {
                    e.Row.Cells[1].Text += "<td class=\"p" + (i < 12 ? "0" : "1") + "\" style=\"background:#F6F6F6\">" + i.ToString("00") + "月</td>";
                }
                e.Row.Cells[1].Text += "</tr></table>";
            }
            else
            {
                if (e.Row.Cells[1].Text.Length < 8) { return; }
                string SD = e.Row.Cells[1].Text.Substring(0, 8);
                string ED = e.Row.Cells[2].Text.Substring(0, 8);
                e.Row.Cells[1].Text = "<table cellpadding=\"0\" cellspacing=\"0\" style=\"width: 345px; text-align:center\"><tr>";
                for (int i = 1; i <= 12; i++)
                {
                    string TempMonth = SelYear.Text + "年" + i.ToString("00") + "月";
                    if (string.Compare(SD, TempMonth) <= 0 && string.Compare(TempMonth, ED) <= 0)
                    {
                        e.Row.Cells[1].Text += "<td class=\"p" + (i < 12 ? "0" : "1") + "\" style=\"background:#FFCCCC\">" + i.ToString("00") + "月</td>";
                    }
                    else
                    {
                        e.Row.Cells[1].Text += "<td class=\"p" + (i < 12 ? "0" : "1") + "\" style=\"background:#F6F6F6\">" + i.ToString("00") + "月</td>";
                    }
                }
                e.Row.Cells[1].Text += "</tr></table>";
                e.Row.Cells[2].Text = ED;
            }
        }
    }
}
