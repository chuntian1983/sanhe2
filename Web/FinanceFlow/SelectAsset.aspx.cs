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

public partial class FinanceFlow_SelectAsset : System.Web.UI.Page
{
    public string ShowTypeName = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            if (Request.QueryString["atype"] == "1")
            {
                ShowTypeName = "资产";
                FASubjectNo.Value = SysConfigs.FixedAssetSubject;
                TreeView1.Nodes[0].Value = FASubjectNo.Value;
                InitAsset(TreeView1.Nodes[0], FASubjectNo.Value);
            }
            else
            {
                ShowTypeName = "资源";
                FASubjectNo.Value = "000";
                TreeView1.Nodes[0].Text = "资源";
                TreeView1.Nodes[0].Value = FASubjectNo.Value;
                InitResource(TreeView1.Nodes[0], FASubjectNo.Value);
                GridView1.Columns[0].HeaderText = "资源编号";
                GridView1.Columns[1].HeaderText = "资源名称";
            }
            InitWebControl();
        }
    }
    protected void InitAsset(TreeNode _TreeNode, string ParentID)
    {
        DataTable assets = CommClass.GetDataTable("select id,cname from cw_assetclass where pid='" + ParentID + "' order by id");
        if (assets.Rows.Count == 0)
        {
            _TreeNode.SelectAction = TreeNodeSelectAction.Select;
        }
        else
        {
            _TreeNode.SelectAction = TreeNodeSelectAction.SelectExpand;
        }
        for (int i = 0; i < assets.Rows.Count; i++)
        {
            _TreeNode.ChildNodes.Add(new TreeNode(assets.Rows[i]["cname"].ToString(), assets.Rows[i]["id"].ToString()));
            InitAsset(_TreeNode.ChildNodes[i], assets.Rows[i]["id"].ToString());
        }
    }
    protected void InitResource(TreeNode _TreeNode, string ParentID)
    {
        DataTable assets = MainClass.GetDataTable("select id,classname from cw_resclass where parentid='" + ParentID + "' order by id");
        if (assets.Rows.Count == 0)
        {
            _TreeNode.SelectAction = TreeNodeSelectAction.Select;
        }
        else
        {
            _TreeNode.SelectAction = TreeNodeSelectAction.SelectExpand;
        }
        for (int i = 0; i < assets.Rows.Count; i++)
        {
            _TreeNode.ChildNodes.Add(new TreeNode(assets.Rows[i]["classname"].ToString(), assets.Rows[i]["id"].ToString()));
            InitResource(_TreeNode.ChildNodes[i], assets.Rows[i]["id"].ToString());
        }
    }
    private void InitWebControl()
    {
        StringBuilder sql = new StringBuilder();
        if (Request.QueryString["atype"] == "1")
        {
            sql.Append("select id,AssetNo,AssetName,AAmount,(AAmount-HasAmount) HasAmount,AssetModel from cw_assetcard ");
            if (TreeView1.SelectedNode == null)
            {
                sql.AppendFormat("where ClassID like '{0}%' ", FASubjectNo.Value);
            }
            else
            {
                sql.AppendFormat("where ClassID like '{0}%' ", TreeView1.SelectedNode.Value);
            }
            if (AssetName.Text.Length > 0)
            {
                sql.Append(" and AssetName like '%" + AssetName.Text + "%'");
            }
            sql.Append("order by AssetNo");
        }
        else
        {
            sql.Append("select id,ResNo AssetNo,ResName AssetName,ResAmount AAmount,(ResAmount-HasAmount) HasAmount,Locality AssetModel from cw_rescard ");
            if (TreeView1.SelectedNode == null)
            {
                sql.AppendFormat("where ClassID like '{0}%' ", FASubjectNo.Value);
            }
            else
            {
                sql.AppendFormat("where ClassID like '{0}%' ", TreeView1.SelectedNode.Value);
            }
            if (AssetName.Text.Length > 0)
            {
                sql.Append(" and ResName like '%" + AssetName.Text + "%'");
            }
            sql.Append("order by ResNo");
        }
        DataTable dt = CommClass.GetDataTable(sql.ToString());
        if (dt.Rows.Count == 0)
        {
            PageClass.BindNoRecords(GridView1, dt);
        }
        else
        {
            GridView1.DataSource = dt.DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
            Label lb = (Label)GridView1.BottomPagerRow.Cells[0].FindControl("ShowPageInfo");
            lb.Text = "记录数：" + dt.Rows.Count.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnSelect = (LinkButton)e.Row.FindControl("btnSelect");
            StringBuilder farmerInfo = new StringBuilder();
            farmerInfo.Append("return selectFarmer(new Array('" + e.Row.Cells[1].Text.Replace("&nbsp;", ""));
            farmerInfo.Append("','" + ((HiddenField)e.Row.FindControl("AssetModel")).Value);
            farmerInfo.Append("','" + GridView1.DataKeys[e.Row.RowIndex].Value.ToString());
            farmerInfo.Append("'));");
            btnSelect.Attributes.Add("onclick", farmerInfo.ToString());
            e.Row.Attributes.Add("ondblclick", farmerInfo.ToString());
        }
    }
    protected void QFarmer_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        InitWebControl();
    }
}
