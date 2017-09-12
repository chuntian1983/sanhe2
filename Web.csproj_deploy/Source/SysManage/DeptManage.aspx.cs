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

public partial class SysManage_DeptManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000013")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            DeptID.Attributes.Add("readonly", "readonly");
            TreeView1.Nodes[0].Text = UserInfo.AccountName;
            TreeView1.Nodes[0].Value = UserInfo.AccountID;
            Button1.Attributes.Add("onclick", "return CheckMod();");
            Button2.Attributes.Add("onclick", "return CheckAdd()");
            Button3.Attributes.Add("onclick", "return confirm('您确定需要删除当前部门吗？')");
            InitSubject();
            //写入操作日志
            CommClass.WriteCTL_Log("100018", "下属部门管理");
            //--
        }
        if (TreeView1.SelectedNode == null)
        {
            Button1.Enabled = false;
            Button3.Enabled = false;
        }
        else
        {
            Button1.Enabled = true;
            Button3.Enabled = true;
        }
    }

    protected void InitSubject()
    {
        DataSet ds = CommClass.GetDataSet("select id,deptname from cw_department order by id");
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            TreeNode node = new TreeNode(ds.Tables[0].Rows[i]["deptname"].ToString(), ds.Tables[0].Rows[i]["id"].ToString());
            node.SelectAction = TreeNodeSelectAction.Select;
            TreeView1.Nodes[0].ChildNodes.Add(node);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (CommClass.CheckExist("cw_department", "deptname='" + DeptName.Text + "' and id<>'" + DeptID.Text + "'"))
        {
            ExeScript.Text = "<script>alert('部门名称[" + DeptName.Text + "]已被使用！');</script>";
        }
        else
        {
            CommClass.ExecuteSQL("update cw_department set deptname='" + DeptName.Text + "' where id='" + DeptID.Text + "'");
            TreeView1.SelectedNode.Text = DeptName.Text;
            ExeScript.Text = "<script>alert('部门编辑成功！');</script>";
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (CommClass.CheckExist("cw_department", "deptname='" + NewDName.Text + "'"))
        {
            ExeScript.Text = "<script>alert('部门名称[" + NewDName.Text + "]已被使用！');</script>";
        }
        else
        {
            string NewDeptID = CommClass.GetRecordID("CW_Department");
            CommClass.ExecuteSQL("insert into cw_department(id,pid,deptname)values('"
                + NewDeptID + "','"
                + TreeView1.Nodes[0].Value + "','"
                + NewDName.Text + "')");
            TreeNode node = new TreeNode(NewDName.Text, NewDeptID);
            node.SelectAction = TreeNodeSelectAction.Select;
            TreeView1.Nodes[0].ChildNodes.Add(node);
            NewDName.Text = "";
            NewDName.Focus();
            ExeScript.Text = "<script>alert('部门增加成功！');</script>";
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        CommClass.ExecuteSQL("delete from cw_department where id='" + DeptID.Text + "'");
        TreeView1.SelectedNode.Parent.ChildNodes.Remove(TreeView1.SelectedNode);
        DeptID.Text = "";
        DeptName.Text = "";
        Button1.Enabled = false;
        Button3.Enabled = false;
        ExeScript.Text = "<script>alert('部门删除成功！');</script>";
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        DeptID.Text = TreeView1.SelectedNode.Value;
        DeptName.Text = TreeView1.SelectedNode.Text;
    }
}
