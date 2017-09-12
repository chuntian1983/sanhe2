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

public partial class AccountInit_SubjectGroup : System.Web.UI.Page
{
    private string hasGroupNo = string.Empty;
    private string myGroupNo = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000002$000000")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            Button1.Attributes["onclick"] = "return CheckAdd();";
            TreeView1.Nodes[0].Value = SysConfigs.GroupingSubject;
            TreeView1.Nodes[0].Text = CommClass.GetFieldFromNo(TreeView1.Nodes[0].Value, "subjectname");
            InitWebControl();
        }
    }

    protected void InitSubject(TreeNode _TreeNode, string ParentNo)
    {
        DataSet ds = CommClass.GetDataSet("select id,subjectno,subjectname from cw_subject where parentno='" + ParentNo + "' order by subjectno");
        if (ds.Tables[0].Rows.Count > 0)
        {
            _TreeNode.SelectAction = TreeNodeSelectAction.Expand;
        }
        else
        {
            _TreeNode.SelectAction = TreeNodeSelectAction.None;
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string subjectNo = ds.Tables[0].Rows[i]["subjectno"].ToString();
            if (myGroupNo.IndexOf(subjectNo + "$") != -1 || hasGroupNo.IndexOf(subjectNo + "$") == -1)
            {
                _TreeNode.ChildNodes.Add(new TreeNode(string.Concat(subjectNo, "&nbsp;.&nbsp;", ds.Tables[0].Rows[i]["subjectname"].ToString()), subjectNo));
                InitSubject(_TreeNode.ChildNodes[_TreeNode.ChildNodes.Count - 1], subjectNo);
            }
        }
    }

    /// <summary>
    /// 数据绑定函数
    /// </summary>
    private void InitWebControl()
    {
        Button1.Enabled = GridView1.EditIndex == -1;
        DataSet ds = CommClass.GetDataSet("select * from cw_subjectgroup order by id desc");
        if (ds.Tables[0].Rows.Count == 0)
        {
            PageClass.BindNoRecords(GridView1, ds);
        }
        else
        {
            GridView1.DataSource = ds.Tables[0].DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
            Label lb = (Label)GridView1.BottomPagerRow.Cells[0].FindControl("ShowPageInfo");
            lb.Text = "记录数：" + ds.Tables[0].Rows.Count.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lb.Text += "总页数：" + (GridView1.PageIndex + 1) + "/" + GridView1.PageCount + "页";
            if (GridView1.AllowPaging)
            {
                DropDownList ddl = (DropDownList)GridView1.BottomPagerRow.Cells[0].FindControl("JumpPage");
                ddl.Items.Clear();
                for (int i = 0; i < GridView1.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem("第" + (i + 1).ToString() + "页", i.ToString()));
                }
                ddl.SelectedIndex = GridView1.PageIndex;
            }
            StringBuilder allno = new StringBuilder();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                allno.AppendFormat("{0}$", row["SubjectNo"].ToString());
            }
            hasGroupNo = allno.ToString();
        }
        TreeView1.Nodes[0].ChildNodes.Clear();
        string subjectGroup = CommClass.GetSysPara("SubjectGroup");
        if (subjectGroup.Length > 0)
        {
            TreeView1.Nodes[0].Text = "分组科目";
            subjectGroup = subjectGroup.Replace(",", "','");
            DataTable subject = CommClass.GetDataTable("select subjectno,subjectname from cw_subject where subjectno in ('" + subjectGroup + "')");
            foreach (DataRow srow in subject.Rows)
            {
                TreeNode node = new TreeNode(srow[1].ToString(), srow[0].ToString());
                node.SelectAction = TreeNodeSelectAction.Expand;
                TreeView1.Nodes[0].ChildNodes.Add(node);
                InitSubject(node, srow[0].ToString());
            }
        }
        else
        {
            InitSubject(TreeView1.Nodes[0], TreeView1.Nodes[0].Value);
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

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        myGroupNo = ((HiddenField)GridView1.Rows[e.NewEditIndex].FindControl("GroupSubjectNo")).Value;
        InitWebControl();
        InitTreeCheckbox(TreeView1.Nodes[0].ChildNodes);
    }

    protected void InitTreeCheckbox(TreeNodeCollection _TreeNode)
    {
        foreach (TreeNode tn in _TreeNode)
        {
            if (myGroupNo.IndexOf(tn.Value + "$") != -1)
            {
                tn.Checked = true;
            }
            else
            {
                tn.Checked = false;
            }
            if (tn.ChildNodes.Count > 0)
            {
                InitTreeCheckbox(tn.ChildNodes);
            }
        }
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        for (int i = TreeView1.CheckedNodes.Count - 1; i >= 0; i--)
        {
            TreeView1.CheckedNodes[i].Checked = false;
        }
        InitWebControl();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
        TextBox N = (TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[0];
        if (CommClass.CheckExist("cw_subjectgroup", "groupname='" + N.Text + "' and id<>'" + ID + "'"))
        {
            ExeScript.Text = "<script>alert('此分组名称【" + N.Text + "】已存在，请更换别的。')</script>";
        }
        else
        {
            StringBuilder CheckNode = new StringBuilder();
            for (int i = TreeView1.CheckedNodes.Count - 1; i >= 0; i--)
            {
                CheckNode.Append(TreeView1.CheckedNodes[i].Value + "$");
                TreeView1.CheckedNodes[i].Checked = false;
            }
            CommClass.ExecuteSQL("update cw_subjectgroup set "
                + "groupname='" + ((TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[0]).Text.Trim() + "',"
                + "subjectno='" + CheckNode.ToString() + "' "
                + "where id='" + ID + "'");
            GridView1.EditIndex = -1;
        }
        InitWebControl();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string gname = e.Row.Cells[0].Controls.Count == 0 ? e.Row.Cells[0].Text : ((TextBox)e.Row.Cells[0].Controls[0]).Text;
            LinkButton btnDelete = (LinkButton)e.Row.Cells[2].FindControl("btnDelete");
            btnDelete.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除分组“" + gname + "”吗？')");
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        GridView1.EditIndex = -1;
        LinkButton btnDelete = (LinkButton)sender;
        CommClass.ExecuteSQL("delete from cw_subjectgroup where id='" + btnDelete.CommandArgument + "'");
        for (int i = TreeView1.CheckedNodes.Count - 1; i >= 0; i--)
        {
            TreeView1.CheckedNodes[i].Checked = false;
        }
        InitWebControl();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView1.EditIndex = -1;
        if (CommClass.CheckExist("cw_subjectgroup", "groupname='" + GroupName.Text + "'"))
        {
            ExeScript.Text = "<script>alert('此分组名称【" + GroupName.Text + "】已存在，请更换别的。')</script>";
        }
        else
        {
            StringBuilder CheckNode = new StringBuilder();
            for (int i = 0; i < TreeView1.CheckedNodes.Count; i++)
            {
                CheckNode.Append(TreeView1.CheckedNodes[i].Value + "$");
            }
            for (int i = TreeView1.CheckedNodes.Count - 1; i >= 0; i--)
            {
                TreeView1.CheckedNodes[i].Checked = false;
            }
            CommClass.ExecuteSQL("insert cw_subjectgroup(id,groupname,subjectno)values('" + CommClass.GetRecordID("CW_SubjectGroup")
                + "','" + GroupName.Text + "','" + CheckNode.ToString() + "')");
            ExeScript.Text = "<script>$('GroupName').value='';$('GroupName').focus();alert('添加成功！');</script>";
        }
        InitWebControl();
    }
}
