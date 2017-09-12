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

public partial class ResManage_SelectItem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000013")) { return; }
        if (!IsPostBack)
        {
            switch (Request.QueryString["t"])
            {
                case "0":
                    TreeView1.Nodes[0].Value = "000";
                    TreeView1.Nodes[0].Text = "资源";
                    TreeView1.Nodes[0].SelectAction = TreeNodeSelectAction.Expand;
                    InitSubject0(TreeView1.Nodes[0], TreeView1.Nodes[0].Value, "");
                    break;
                case "1":
                    TreeView1.Nodes[0].Value = UserInfo.AccountID;
                    TreeView1.Nodes[0].Text = UserInfo.AccountName;
                    InitSubject1(TreeView1.Nodes[0], TreeView1.Nodes[0].Value);
                    break;
                case "2":
                    TreeView1.Nodes[0].Value = "000";
                    TreeView1.Nodes[0].Text = "科目列表";
                    InitSubject2(TreeView1.Nodes[0], "000");
                    break;
                case "3":
                    TreeView1.Nodes[0].Value = "000";
                    TreeView1.Nodes[0].Text = "资源列表";
                    InitSubject3(TreeView1.Nodes[0], "000");
                    break;
            }
            TreeView1.ExpandDepth = 1;
        }
    }

    protected void InitSubject0(TreeNode _TreeNode, string ParentNo, string Measures)
    {
        DataSet ds = MainClass.GetDataSet("select id,ClassName,Measures from cw_resclass where parentid='" + ParentNo + "' order by id");
        if (ds.Tables[0].Rows.Count == 0)
        {
            _TreeNode.SelectAction = TreeNodeSelectAction.None;
            _TreeNode.Text = string.Format("<a href=\"###\" onclick=\"OnTreeClick(new Array('{0}','{1}','{2}'));\">{1}</a>", _TreeNode.Value, _TreeNode.Text, Measures);
        }
        else
        {
            _TreeNode.SelectAction = TreeNodeSelectAction.Expand;
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            _TreeNode.ChildNodes.Add(new TreeNode(ds.Tables[0].Rows[i]["ClassName"].ToString(), ds.Tables[0].Rows[i]["id"].ToString()));
            InitSubject0(_TreeNode.ChildNodes[i], ds.Tables[0].Rows[i]["id"].ToString(), ds.Tables[0].Rows[i]["Measures"].ToString());
        }
    }

    protected void InitSubject1(TreeNode _TreeNode, string ParentNo)
    {
        DataSet ds = CommClass.GetDataSet("select id,deptname from cw_department where pid='" + ParentNo + "' order by id");
        if (ds.Tables[0].Rows.Count == 0)
        {
            _TreeNode.SelectAction = TreeNodeSelectAction.None;
            string ClickStr = "OnTreeClick(new Array('" + _TreeNode.Value + "','" + _TreeNode.Text + "'));";
            _TreeNode.Text = "<a href=\"###\" onclick=\"" + ClickStr + "\">" + _TreeNode.Text + "</a>";
        }
        else
        {
            _TreeNode.SelectAction = TreeNodeSelectAction.Expand;
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            _TreeNode.ChildNodes.Add(new TreeNode(ds.Tables[0].Rows[i]["deptname"].ToString(), ds.Tables[0].Rows[i]["id"].ToString()));
            InitSubject1(_TreeNode.ChildNodes[i], ds.Tables[0].Rows[i]["id"].ToString());
        }
    }

    protected void InitSubject2(TreeNode _TreeNode, string ParentNo)
    {
        DataSet ds = CommClass.GetDataSet("select subjectno,subjectname from cw_subject where parentno='" + ParentNo + "' order by subjectno");
        if (ds.Tables[0].Rows.Count == 0)
        {
            if (ParentNo == "000") { return; }
            _TreeNode.SelectAction = TreeNodeSelectAction.None;
            string ClickStr = "OnTreeClick(new Array('" + _TreeNode.Value + "','" + _TreeNode.Text + "'));";
            _TreeNode.Text = "<a href=\"###\" onclick=\"" + ClickStr + "\">" + _TreeNode.Text + "</a>";
        }
        else
        {
            _TreeNode.SelectAction = TreeNodeSelectAction.Expand;
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            _TreeNode.ChildNodes.Add(new TreeNode(ds.Tables[0].Rows[i]["subjectname"].ToString(), ds.Tables[0].Rows[i]["subjectno"].ToString()));
            InitSubject2(_TreeNode.ChildNodes[i], ds.Tables[0].Rows[i]["subjectno"].ToString());
        }
    }

    protected void InitSubject3(TreeNode _TreeNode, string ParentNo)
    {
        DataSet ds = CommClass.GetDataSet("select id,ResName,ResUnit,ResAmount from cw_rescard order by id");
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string ClickStr = "OnTreeClick(new Array('"
                + ds.Tables[0].Rows[i]["id"].ToString() + "','"
                + ds.Tables[0].Rows[i]["ResName"].ToString() + "','"
                + ds.Tables[0].Rows[i]["ResUnit"].ToString() + "','"
                + ds.Tables[0].Rows[i]["ResAmount"].ToString() + "'));";
            TreeNode node = new TreeNode();
            node.Text = "<a href=\"###\" onclick=\"" + ClickStr + "\">" + ds.Tables[0].Rows[i]["ResName"].ToString() + "</a>";
            node.Value = ds.Tables[0].Rows[i]["id"].ToString();
            _TreeNode.ChildNodes.Add(node);
        }
    }
}
