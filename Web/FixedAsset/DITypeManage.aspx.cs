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

public partial class FixedAsset_DITypeManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000013")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            DID.Attributes.Add("readonly", "readonly");
            LinkSubject.Attributes.Add("readonly", "readonly");
            NLinkSubject.Attributes.Add("readonly", "readonly");
            Button1.Attributes.Add("onclick", "return CheckMod();");
            Button2.Attributes.Add("onclick", "return CheckAdd()");
            Button3.Attributes.Add("onclick", "return confirm('您确定需要删除当前增减方式吗？')");
            LinkSubject.Attributes.Add("onclick", "SelectItem(1)");
            NLinkSubject.Attributes.Add("onclick", "SelectItem(2)");
            ValueAddSubject.Attributes.Add("onclick", "SelectItem(3)");
            NValueAddSubject.Attributes.Add("onclick", "SelectItem(4)");
            InitSubject();
            //写入操作日志
            CommClass.WriteCTL_Log("100018", "资产增减方式管理");
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
        DataSet ds = CommClass.GetDataSet("select id,tname from cw_ditype where ttype='1' order by id");
        foreach(DataRow row in ds.Tables[0].Rows)
        {
            TreeNode node = new TreeNode(row["tname"].ToString(), row["id"].ToString());
            node.SelectAction = TreeNodeSelectAction.Select;
            TreeView1.Nodes[0].ChildNodes[0].ChildNodes.Add(node);
        }
        ds = CommClass.GetDataSet("select id,tname from cw_ditype where ttype='2' order by id");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            TreeNode node = new TreeNode(row["tname"].ToString(), row["id"].ToString());
            node.SelectAction = TreeNodeSelectAction.Select;
            TreeView1.Nodes[0].ChildNodes[1].ChildNodes.Add(node);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (CommClass.CheckExist("cw_ditype", "tname='" + TName.Text + "' and id<>'" + DID.Text + "'"))
        {
            ExeScript.Text = "<script>alert('增减方式名称[" + TName.Text + "]已被使用！');</script>";
        }
        else
        {
            CommClass.ExecuteSQL("update cw_ditype set tname='" + TName.Text + "',linksubject='" + LinkSubject.Text + "',nvaluesubject='" + ValueAddSubject.Text + "' where id='" + DID.Text + "'");
            TreeView1.SelectedNode.Text = TName.Text;
            ExeScript.Text = "<script>alert('编辑保存成功！');</script>";
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (CommClass.CheckExist("cw_ditype", "tname='" + NewDName.Text + "'"))
        {
            ExeScript.Text = "<script>alert('增减方式名称[" + NewDName.Text + "]已被使用！');</script>";
        }
        else
        {
            string NewDID = CommClass.GetRecordID("CW_DIType");
            CommClass.ExecuteSQL("insert into cw_ditype(id,tname,ttype,linksubject,nvaluesubject)values('" + NewDID + "','"
                + NewDName.Text + "','" + DIType.SelectedValue + "','" + NLinkSubject.Text + "','" + NValueAddSubject.Text + "')");
            TreeNode node = new TreeNode(NewDName.Text, NewDID);
            node.SelectAction = TreeNodeSelectAction.Select;
            if (DIType.SelectedValue == "1")
            {
                TreeView1.Nodes[0].ChildNodes[0].ChildNodes.Add(node);
            }
            else
            {
                TreeView1.Nodes[0].ChildNodes[1].ChildNodes.Add(node);
            }
            NewDName.Text = "";
            NewDName.Focus();
            ExeScript.Text = "<script>alert('新类别增加成功！');</script>";
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        CommClass.ExecuteSQL("delete from cw_ditype where id='" + DID.Text + "'");
        TreeView1.SelectedNode.Parent.ChildNodes.Remove(TreeView1.SelectedNode);
        DID.Text = "";
        TName.Text = "";
        LinkSubject.Text = "";
        ValueAddSubject.Text = "";
        Button1.Enabled = false;
        Button3.Enabled = false;
        ExeScript.Text = "<script>alert('增减方式删除成功！');</script>";
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        DID.Text = TreeView1.SelectedNode.Value;
        TName.Text = TreeView1.SelectedNode.Text;
        LinkSubject.Text = CommClass.GetFieldFromID(DID.Text, "linksubject", "cw_ditype");
        ValueAddSubject.Text = CommClass.GetFieldFromID(DID.Text, "nvaluesubject", "cw_ditype");
    }
}
