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
using ZedGraph.Web;

public partial class AccountCollect_MonitorResourceChart : System.Web.UI.Page
{
    private int nodeDepth = 0;
    private double sum = 0;
    private string sumCondition = string.Empty;
    private DataTable SumDatas = new DataTable();
    private int RunLevel = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            Overlay.Attributes["style"] = "display:none";
            Lightbox.Attributes["style"] = "display:none";
            Button1.Attributes.Add("onclick", "return SubmitForm();");
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
            ///////////////////////////////////////////////////////////////////////////////////
            SumDatas.Columns.Add("id");
            SumDatas.Columns.Add("aname");
            SumDatas.Columns.Add("ymonth");
            SumDatas.Columns.Add("sum", typeof(double));
            SumDatas.PrimaryKey = new DataColumn[] { SumDatas.Columns["id"] };
            ViewState["SumDatas"] = SumDatas;
            InitSubject("资源", "000");
        }
        nodeDepth = int.Parse(TotalLevel.Value);
        if (TreeView1.SelectedNode != null)
        {
            if (!TreeView1.SelectedNode.Value.StartsWith("$"))
            {
                TreeView1.SelectedNode.ToggleExpandState();
                if ((bool)TreeView1.SelectedNode.Expanded.Value)
                {
                    TreeView1.SelectedNode.ImageUrl = "../Images/dopen.gif";
                }
                else
                {
                    TreeView1.SelectedNode.ImageUrl = "../Images/dclose.gif";
                }
            }
        }
        ZedGraphWeb1.Visible = false;
        if (IsPostBack)
        {
            SumDatas = (DataTable)ViewState["SumDatas"];
        }
    }

    protected void InitSubject(string CName, string ParentID)
    {
        QList.Items.Add(new ListItem(new string('.', RunLevel * 6) + CName, ParentID));
        DataSet ds = MainClass.GetDataSet("select id,classname from cw_resclass where parentid='" + ParentID + "' order by id asc");
        RunLevel++;
        if (ParentID == "000") { RunLevel = 1; }
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            InitSubject(row["classname"].ToString(), row["id"].ToString());
        }
        RunLevel--;
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
            //GAccountList.Value = TreeView1.SelectedNode.Value.Substring(1);
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

    protected void GetCollectData(string UnitID)
    {
        if (UnitID.StartsWith("$"))
        {
            CalculateSum(UnitID.Substring(1));
        }
        else
        {
            DataSet ds = MainClass.GetDataSet("select id from cw_account where unitid='" + UnitID + "'");
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                CalculateSum(row["id"].ToString());
            }
        }
        DataRow[] rows = ValidateClass.GetRegRows("CUnits", "parentid='" + UnitID + "'");
        if (rows != null)
        {
            for (int i = 0; i < rows.Length; i++)
            {
                GetCollectData(rows[i]["id"].ToString());
            }
        }
    }

    protected void CalculateSum(string accountID)
    {
        string _accountID = string.Format("{0}{1}{2}{3}", accountID, SelYear.Text, SelMonth.SelectedValue, QList.SelectedValue);
        DataRow sumRow = SumDatas.Rows.Find(_accountID);
        if (sumRow == null)
        {
            DataRow newRow = SumDatas.NewRow();
            newRow["id"] = _accountID;
            UserInfo.AccountID = accountID;
            double temp = 0;
            double.TryParse(CommClass.GetTableValue("cw_rescard", "sum(ResAmount)", sumCondition), out temp);
            newRow["sum"] = temp;
            SumDatas.Rows.Add(newRow);
            sum += temp;
        }
        else
        {
            sum += double.Parse(sumRow["sum"].ToString());
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ZedGraphWeb1.Visible = true;
        int year = int.Parse(SelYear.Text);
        Dictionary<string, double> dics = new Dictionary<string, double>();
        FillDictionary(dics, year.ToString());
        //绘制统计分析图
        DrawZedGraph drawGraph = new DrawZedGraph(ZedGraphWeb1, GraphType.SelectedValue, dics.Count);
        drawGraph.Title = "统计分析图";
        drawGraph.BarText = "金额";
        drawGraph.data = dics;
        drawGraph.XAxisTitle = TreeView1.SelectedNode.Text;
        drawGraph.YAxisTitle = "统计金额";
        ZedGraphWeb1.RenderGraph += new ZedGraphWebControlEventHandler(drawGraph.Draw);
    }

    private void FillDictionary(Dictionary<string, double> dics, string year)
    {
        //创建数据表
        DataTable datas = new DataTable();
        datas.Columns.Add("key");
        datas.Columns.Add("value", typeof(double));
        //按项统计
        sumCondition = string.Format("(BookDate like '{0}-{1}%') and (ClassID like '{2}%')", SelYear.Text, SelMonth.SelectedValue, QList.SelectedValue);
        if (TreeView1.SelectedNode.ChildNodes.Count == 0)
        {
            sum = 0;
            GetCollectData(TreeView1.SelectedNode.Value);
            datas.Rows.Add(new object[] { TreeView1.SelectedNode.Text, sum });
        }
        else
        {
            foreach (TreeNode node in TreeView1.SelectedNode.ChildNodes)
            {
                sum = 0;
                GetCollectData(node.Value);
                datas.Rows.Add(new object[] { node.Text, sum });
            }
        }
        //查询参数
        string rowFilter = string.Empty;
        if (ValueType.Items[0].Selected)
        {
            rowFilter = "value>=0";
        }
        else
        {
            rowFilter = "value>0";
        }
        //标签数值字典
        DataView dv = new DataView(datas, rowFilter, OrderType.SelectedValue, DataViewRowState.CurrentRows);
        for (int i = 0; i < dv.Count; i++)
        {
            double _sum = double.Parse(dv[i]["value"].ToString());

            dics.Add(dv[i]["key"].ToString(), _sum);
        }
    }
}
