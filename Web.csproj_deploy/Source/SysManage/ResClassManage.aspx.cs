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

public partial class SysManage_ResClassManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            ClassID.Text = "[自动编号]";
            ClassID.Attributes.Add("onclick", "if(this.value=='[自动编号]')this.value='';");
            ClassID.Attributes.Add("onblur", "if(this.value.length==0)this.value='[自动编号]';");
            ClassIDP.Attributes.Add("readonly", "readonly");
            ParentName.Attributes.Add("readonly", "readonly");
            Button1.Attributes.Add("onclick", "return CheckMod();");
            Button3.Attributes.Add("onclick", "return confirm('您确定需要删除当前类别吗？')");
            FASubjectNo.Value = "000";
            TreeView1.Nodes[0].Value = FASubjectNo.Value;
            InitSubject(TreeView1.Nodes[0], FASubjectNo.Value);
        }
        if (TreeView1.SelectedNode == null)
        {
            Button1.Enabled = false;
            Button2.Enabled = false;
            Button3.Enabled = false;
        }
        else
        {
            Button2.Enabled = true;
        }
    }

    protected void InitSubject(TreeNode _TreeNode, string ParentID)
    {
        if (_TreeNode.Value == SelectClass.Value)
        {
            _TreeNode.Selected = true;
        }
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
            InitSubject(_TreeNode.ChildNodes[i], assets.Rows[i]["id"].ToString());
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string NewClassID = ClassIDP.Text;
        if (ClassID.Text == "[自动编号]" || ClassID.Text.Length == 0)
        {
            NewClassID += CommClass.GetRecordID("CW_ResClass");
        }
        else
        {
            NewClassID += ClassID.Text;
        }
        if (CEditState.Value == "1")
        {
            //类别编辑状态
            if (NewClassID != OldClassID.Value && MainClass.CheckExist("cw_resclass", "id='" + NewClassID + "'"))
            {
                ExeScript.Text = "<script>alert('该卡片编号[" + NewClassID + "]已存在！');</script>";
                return;
            }
            MainClass.ExecuteSQL("update cw_resclass set "
                + "id='" + NewClassID + "',"
                + "classname='" + CName.Text + "',"
                + "measures='" + MUnit.Text + "',"
                + "Notes='" + CNotes.Text + "' where id='" + OldClassID.Value + "'");
            TreeView1.SelectedNode.Text = CName.Text;
            TreeView1.SelectedNode.Value = NewClassID;
            SelectClass.Value = NewClassID;
            OldClassID.Value = NewClassID;
            ExeScript.Text = "<script>alert('类别编辑成功！');</script>";
        }
        else
        {
            //类别新增状态
            if (MainClass.CheckExist("cw_resclass", "id='" + NewClassID + "'"))
            {
                ExeScript.Text = "<script>alert('该卡片编号[" + NewClassID + "]已存在！');</script>";
                return;
            }
            MainClass.ExecuteSQL("insert into cw_resclass(id,parentid,classname,measures,notes)values("
                + "'" + NewClassID + "',"
                + "'" + TreeView1.SelectedNode.Value + "',"
                + "'" + CName.Text + "',"
                + "'" + MUnit.Text + "',"
                + "'" + CNotes.Text + "')");
            CEditState.Value = "0";
            TreeView1.Nodes[0].ChildNodes.Clear();
            InitSubject(TreeView1.Nodes[0], FASubjectNo.Value);
            ExeScript.Text = "<script>alert('类别增加成功！');</script>";
            Button2_Click(Button2, new EventArgs());
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (CEditState.Value == "2")
        {
            TreeView1.Enabled = true;
            TreeView1.Nodes[0].ChildNodes.Clear();
            InitSubject(TreeView1.Nodes[0], FASubjectNo.Value);
            TreeView1_SelectedNodeChanged(TreeView1, new EventArgs());
            Button2.Text = "增加下级类别";
        }
        else
        {
            CName.Text = "";
            CName.Focus();
            ClassIDP.Text = TreeView1.SelectedNode.Value;
            int childCounts = TreeView1.SelectedNode.ChildNodes.Count;
            if (childCounts == 0)
            {
                ClassID.Text = "001";
            }
            else
            {
                ClassID.Text = (++childCounts).ToString("000");
            }
            MUnit.Text = "";
            CNotes.Text = "";
            if (TreeView1.SelectedNode.Value == FASubjectNo.Value)
            {
                ParentName.Text = "新增一级类别";
                EditState.Text = "新增一级类别";
            }
            else
            {
                ParentName.Text = TreeView1.SelectedNode.Text;
                EditState.Text = "正在增加【" + ParentName.Text + "】的下级类别";
            }
            Button1.Enabled = true;
            Button3.Enabled = false;
            Button2.Text = "取消增加类别";
            CEditState.Value = "2";
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        ParentName.Text = "";
        ClassIDP.Text = "";
        ClassID.Text = "[自动编号]";
        CName.Text = "";
        MUnit.Text = "";
        CNotes.Text = "";
        CEditState.Value = "0";
        EditState.Text = "";
        DelAssetClass(TreeView1.SelectedNode);
        TreeView1.Nodes[0].ChildNodes.Clear();
        InitSubject(TreeView1.Nodes[0], FASubjectNo.Value);
        Button1.Enabled = false;
        Button2.Enabled = false;
        Button3.Enabled = false;
        ExeScript.Text = "<script>alert('类别删除成功！');</script>";
    }
    protected void DelAssetClass(TreeNode node)
    {
        MainClass.ExecuteSQL("delete from cw_resclass where id='" + node.Value + "'");
        for (int i = 0; i < node.ChildNodes.Count; i++)
        {
            DelAssetClass(node.ChildNodes[i]);
        }
        node.Parent.ChildNodes.Remove(node);
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        CEditState.Value = "1";
        Button2.Text = "增加下级类别";
        SelectClass.Value = TreeView1.SelectedNode.Value;
        if (TreeView1.SelectedNode.Value == FASubjectNo.Value)
        {
            EditState.Text = "资源";
            ParentName.Text = "资源";
            ClassIDP.Text = "";
            ClassID.Text = "[自动编号]";
            CName.Text = "";
            MUnit.Text = "";
            CNotes.Text = "";
            Button1.Enabled = false;
            Button3.Enabled = false;
        }
        else
        {
            DataTable cls = MainClass.GetDataTable("select id,parentid,classname,measures,notes from cw_resclass where id='" + TreeView1.SelectedNode.Value + "'");
            ParentName.Text = TreeView1.SelectedNode.Parent.Text;
            OldClassID.Value = cls.Rows[0]["id"].ToString();
            ClassIDP.Text = cls.Rows[0]["parentid"].ToString();
            ClassID.Text = cls.Rows[0]["id"].ToString().Substring(ClassIDP.Text.Length);
            CName.Text = cls.Rows[0]["classname"].ToString();
            MUnit.Text = cls.Rows[0]["measures"].ToString();
            CNotes.Text = cls.Rows[0]["notes"].ToString();
            EditState.Text = "正在编辑类别【" + CName.Text + "】";
            Button1.Enabled = true;
            Button3.Enabled = true;
        }
    }
}
