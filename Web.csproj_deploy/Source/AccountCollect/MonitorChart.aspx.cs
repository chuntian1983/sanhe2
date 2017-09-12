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

public partial class AccountCollect_MonitorChart : System.Web.UI.Page
{
    private int nodeDepth = 0;
    private double sum = 0;
    private DataTable SumDatas = new DataTable();
    private ClsCalculate clsCalculate = new ClsCalculate();

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
            if (Request.QueryString["sno"] == "101")
            {
                StatisticExpr.Text = "现金[101]:借方金额余额+银行存款[102]:借方金额余额";
                StatisticExpr.TextMode = TextBoxMode.SingleLine;
                StatisticExpr.Height = 18;
                ReportTitle.InnerHtml = "现金统计分析图";
            }
            else
            {
                StatisticExpr.Attributes["ondblclick"] = "SelectExpr('StatisticExpr');";
            }
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
        }
        nodeDepth = int.Parse(TotalLevel.Value);
        if (TreeView1.SelectedNode != null)
        {
            if (!TreeView1.SelectedNode.Value.StartsWith("$"))
            {
                TreeView1.SelectedNode.Expand();
                TreeView1.SelectedNode.ImageUrl = "../Images/dopen.gif";
                //TreeView1.SelectedNode.ToggleExpandState();
                //if (TreeView1.SelectedNode.Expanded.HasValue && (bool)TreeView1.SelectedNode.Expanded.Value)
                //{
                //    TreeView1.SelectedNode.ImageUrl = "../Images/dopen.gif";
                //}
                //else
                //{
                //    TreeView1.SelectedNode.ImageUrl = "../Images/dclose.gif";
                //}
            }
        }
        ZedGraphWeb1.Visible = false;
        if (IsPostBack)
        {
            SumDatas = (DataTable)ViewState["SumDatas"];
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
        string _accountID = accountID + clsCalculate.ReportDate;
        DataRow sumRow = SumDatas.Rows.Find(_accountID);
        if (sumRow == null)
        {
            DataRow newRow = SumDatas.NewRow();
            newRow["id"] = _accountID;
            UserInfo.AccountID = accountID;
            double temp = double.Parse(clsCalculate.GetExprValue(StatisticExpr.Text).ToString());
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
        clsCalculate.DesignID = "000000";
        //统计项变化时清除内存
        if (StatisticExpr.Text != HasStatisticExpr.Value)
        {
            SumDatas.Rows.Clear();
            SumDatas.AcceptChanges();
        }
        int year = int.Parse(SelYear.Text);
        Dictionary<string, double> dics = new Dictionary<string, double>();
        Dictionary<string, double> dics2 = new Dictionary<string, double>();
        FillDictionary(dics, year.ToString());
        switch (CompareType.SelectedValue)
        {
            case "1":
                year--;
                FillDictionary(dics2, year.ToString());
                break;
            case "2":
                year++;
                FillDictionary(dics2, year.ToString());
                break;
        }
        //绘制统计分析图
        DrawZedGraph drawGraph = new DrawZedGraph(ZedGraphWeb1, GraphType.SelectedValue, Math.Max(dics.Count, dics2.Count));
        drawGraph.Title = "统计分析图";
        switch (CompareType.SelectedValue)
        {
            case "0":
                drawGraph.BarText = "金额";
                drawGraph.data = dics;
                break;
            case "1":
                drawGraph.BarText = "去年";
                drawGraph.BarText2 = "今年";
                drawGraph.data = dics2;
                drawGraph.data2 = dics;
                break;
            case "2":
                drawGraph.BarText = "今年";
                drawGraph.BarText2 = "明年";
                drawGraph.data = dics;
                drawGraph.data2 = dics2;
                break;
        }
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
        if (SumType.SelectedValue == "0")
        {
            //按项统计
            clsCalculate.ReportDate = string.Format("{0}年{1}月", year, SelMonth.SelectedValue);
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
        }
        else
        {
            //按月统计
            string unitID = TreeView1.SelectedNode.Value;
            for (int i = 1; i <= 12; i++)
            {
                string month = i.ToString("00月");
                clsCalculate.ReportDate = string.Format("{0}年{1}", year, month);
                sum = 0;
                GetCollectData(unitID);
                datas.Rows.Add(new object[] { month, sum });
            }
        }
        //查询参数
        StringBuilder rowFilter = new StringBuilder(9);
        rowFilter.Append("value");
        if (ValueType.Items[2].Selected)
        {
            rowFilter.Append("<");
        }
        if (ValueType.Items[1].Selected)
        {
            rowFilter.Append(">");
        }
        if (ValueType.Items[0].Selected)
        {
            rowFilter.Append("=");
        }
        rowFilter.Append("0");
        if (rowFilter.Length == 9)
        {
            rowFilter.Length = 0;
        }
        //标签数值字典
        DataView dv = new DataView(datas, rowFilter.ToString(), OrderType.SelectedValue, DataViewRowState.CurrentRows);
        for (int i = 0; i < dv.Count; i++)
        {
            double _sum = double.Parse(dv[i]["value"].ToString());
            if (ValueType.Items[1].Selected == false)
            {
                _sum = Math.Abs(_sum);
            }
            dics.Add(dv[i]["key"].ToString(), _sum);
        }
    }
}
