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

public partial class FixedAsset_SelectItem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000013")) { return; }
        if (!IsPostBack)
        {
            switch (Request.QueryString["t"])
            {
                case "0":
                    TreeView1.Nodes[0].Value = SysConfigs.FixedAssetSubject;
                    TreeView1.Nodes[0].Text = "固定资产";
                    InitSubject0(TreeView1.Nodes[0], TreeView1.Nodes[0].Value, "");
                    break;
                case "1":
                    TreeView1.Nodes[0].Value = UserInfo.AccountID;
                    TreeView1.Nodes[0].Text = UserInfo.AccountName;
                    InitSubject1(TreeView1.Nodes[0], TreeView1.Nodes[0].Value);
                    break;
                case "2":
                    TreeView1.Nodes[0].Value = "000";
                    TreeView1.Nodes[0].Text = "一级科目";
                    InitSubject2(TreeView1.Nodes[0], "000");
                    break;
            }
            TreeView1.ExpandDepth = 1;
        }
    }

    protected void InitSubject0(TreeNode _TreeNode, string ParentNo, string ClickStr)
    {
        DataSet ds = CommClass.GetDataSet("select id,cname,uselife,svrate,deprmethod,deprsubject,munit from cw_assetclass where pid='" + ParentNo + "' order by id");
        if (ds.Tables[0].Rows.Count == 0)
        {
            if (ParentNo == "000000") { return; }
            _TreeNode.SelectAction = TreeNodeSelectAction.None;
            _TreeNode.Text = "<a href=\"###\" onclick=\"" + ClickStr + "\">" + _TreeNode.Text + "</a>";
        }
        else
        {
            _TreeNode.SelectAction = TreeNodeSelectAction.Expand;
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            _TreeNode.ChildNodes.Add(new TreeNode(ds.Tables[0].Rows[i]["cname"].ToString(), ds.Tables[0].Rows[i]["id"].ToString()));
            ClickStr = "OnTreeClick(new Array('"
                + ds.Tables[0].Rows[i]["id"].ToString() + "','"
                + ds.Tables[0].Rows[i]["cname"].ToString() + "','"
                + ds.Tables[0].Rows[i]["uselife"].ToString() + "','"
                + ds.Tables[0].Rows[i]["svrate"].ToString() + "','"
                + ds.Tables[0].Rows[i]["deprmethod"].ToString() + "','"
                + ds.Tables[0].Rows[i]["deprsubject"].ToString() + "','"
                + ds.Tables[0].Rows[i]["munit"].ToString() + "'));";
            InitSubject0(_TreeNode.ChildNodes[i], ds.Tables[0].Rows[i]["id"].ToString(), ClickStr);
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
}
