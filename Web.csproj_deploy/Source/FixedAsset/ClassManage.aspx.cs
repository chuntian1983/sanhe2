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

public partial class FixedAsset_ClassManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000013")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            ClassID.Text = "[自动编号]";
            ClassID.Attributes.Add("onclick", "if(this.value=='[自动编号]')this.value='';");
            ClassID.Attributes.Add("onblur", "if(this.value.length==0)this.value='[自动编号]';");
            UtilsPage.SetTextBoxAutoValue(UseLife0, "0");
            UtilsPage.SetTextBoxAutoValue(SVRate, "0");
            ClassIDP.Attributes.Add("readonly", "readonly");
            ParentName.Attributes.Add("readonly", "readonly");
            LinkSubject.Attributes.Add("readonly", "readonly");
            LinkSubject.Attributes.Add("onclick", "SelectItem(1)");
            DeprSubject.Attributes.Add("readonly", "readonly");
            DeprSubject.Attributes.Add("onclick", "SelectItem(2)");
            Button1.Attributes.Add("onclick", "return CheckMod();");
            Button3.Attributes.Add("onclick", "return confirm('注意：该操作将删除类别以及与类别关联的所有卡片！\\n\\n您确定需要删除当前类别吗？')");
            UpdateAssetClass.Attributes.Add("onclick", "return confirm('注意：同步后将删除科目表不存在的类别，则会影响类别与卡片的关联！\\n\\n您确定把资产类别与科目表同步吗？')");
            FASubjectNo.Value = SysConfigs.FixedAssetSubject;
            TreeView1.Nodes[0].Value = FASubjectNo.Value;
            InitSubject(TreeView1.Nodes[0], FASubjectNo.Value);
            //写入操作日志
            CommClass.WriteCTL_Log("100018", "资产类别管理");
            //--
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
            ExpandAssetClass(_TreeNode);
        }
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
            InitSubject(_TreeNode.ChildNodes[i], assets.Rows[i]["id"].ToString());
        }
    }

    protected void ExpandAssetClass(TreeNode node)
    {
        node.Expand();
        if (node.Parent != null)
        {
            ExpandAssetClass(node.Parent);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string NewClassID = ClassIDP.Text;
        if (ClassID.Text == "[自动编号]" || ClassID.Text.Length == 0)
        {
            NewClassID += CommClass.GetRecordID("CW_AssetClass");
        }
        else
        {
            NewClassID += ClassID.Text;
        }
        if (CEditState.Value == "1")
        {
            string olength = OldClassID.Value.Length.ToString();
            //类别编辑状态
            if (NewClassID != OldClassID.Value && CommClass.CheckExist("cw_assetclass", "id='" + NewClassID + "'"))
            {
                ExeScript.Text = "<script>alert('该卡片编号[" + NewClassID + "]已存在！');</script>";
                return;
            }
            //更新资产卡片
            CommClass.ExecuteSQL(string.Format("update cw_assetcard set ClassID=concat('{0}',right(ClassID,length(ClassID)-{1})) where ClassID like '{2}%'", NewClassID, olength, OldClassID.Value));
            //更新类别表
            CommClass.ExecuteSQL("update cw_assetclass set "
                + "id='" + NewClassID + "',"
                + "cname='" + CName.Text + "',"
                + "uselife='" + UseLife0.Text + "." + UseLife1.SelectedValue + "',"
                + "svrate='" + SVRate.Text + "',"
                + "munit='" + MUnit.Text + "',"
                + "deprmethod='" + DeprMethod.SelectedValue + "',"
                + "linksubject='" + LinkSubject.Text + "',"
                + "deprsubject='" + DeprSubject.Text + "' where id='" + OldClassID.Value + "'");
            //更新下级类别
            CommClass.ExecuteSQL(string.Format("update cw_assetclass set ID=concat('{0}',right(ID,length(ID)-{1})),PID=concat('{0}',right(PID,length(PID)-{1})) where PID like '{2}%'", NewClassID, olength, OldClassID.Value));
            //--
            SelectClass.Value = NewClassID;
            OldClassID.Value = NewClassID;
            TreeView1.Nodes[0].ChildNodes.Clear();
            InitSubject(TreeView1.Nodes[0], FASubjectNo.Value);
            ExeScript.Text = "<script>alert('类别编辑成功！');</script>";
        }
        else
        {
            //类别新增状态
            if (CommClass.CheckExist("cw_assetclass", "id='" + NewClassID + "'"))
            {
                ExeScript.Text = "<script>alert('该卡片编号[" + NewClassID + "]已存在！');</script>";
                return;
            }
            CommClass.ExecuteSQL("insert into cw_assetclass(id,pid,cname,uselife,svrate,munit,deprmethod,linksubject,deprsubject)values("
                + "'" + NewClassID + "',"
                + "'" + TreeView1.SelectedNode.Value + "',"
                + "'" + CName.Text + "',"
                + "'" + (UseLife0.Text.Length == 0 ? "0" : UseLife0.Text) + "." + UseLife1.SelectedValue + "',"
                + "'" + SVRate.Text + "',"
                + "'" + MUnit.Text + "',"
                + "'" + DeprMethod.SelectedValue + "',"
                + "'" + LinkSubject.Text + "',"
                + "'" + DeprSubject.Text + "')");
            CEditState.Value = "0";
            TreeView1.Nodes[0].ChildNodes.Clear();
            InitSubject(TreeView1.Nodes[0], FASubjectNo.Value);
            Button2_Click(Button2, new EventArgs());
            ExeScript.Text = "<script>alert('类别增加成功！');</script>";
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
            ClassID.Text = "[自动编号]";
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
        UseLife0.Text = "";
        UseLife1.SelectedIndex = 0;
        SVRate.Text = "";
        MUnit.Text = "";
        DeprMethod.SelectedIndex = 1;
        LinkSubject.Text = "";
        DeprSubject.Text = "";
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
        CommClass.ExecuteSQL(string.Concat("delete from cw_assetclass where id='", node.Value, "'"));
        CommClass.ExecuteSQL(string.Concat("delete from cw_assetcard where ClassID='", node.Value, "'"));
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
            EditState.Text = "固定资产";
            ParentName.Text = "固定资产";
            ClassIDP.Text = "";
            ClassID.Text = "[自动编号]";
            CName.Text = "";
            UseLife0.Text = "";
            UseLife1.SelectedIndex = 0;
            SVRate.Text = "";
            MUnit.Text = "";
            DeprMethod.SelectedIndex = 1;
            LinkSubject.Text = "";
            DeprSubject.Text = "";
            Button1.Enabled = false;
            Button3.Enabled = false;
        }
        else
        {
            DataTable cls = CommClass.GetDataTable("select id,pid,cname,uselife,svrate,munit,deprmethod,linksubject,deprsubject from cw_assetclass where id='"
                + TreeView1.SelectedNode.Value + "'");
            ParentName.Text = TreeView1.SelectedNode.Parent.Text;
            OldClassID.Value = cls.Rows[0]["id"].ToString();
            ClassIDP.Text = cls.Rows[0]["pid"].ToString();
            ClassID.Text = cls.Rows[0]["id"].ToString().Substring(ClassIDP.Text.Length);
            CName.Text = cls.Rows[0]["cname"].ToString();
            int dotpos = cls.Rows[0]["uselife"].ToString().IndexOf(".");
            if (dotpos > 0)
            {
                UseLife0.Text = cls.Rows[0]["uselife"].ToString().Substring(0, dotpos);
                UseLife1.Text = cls.Rows[0]["uselife"].ToString().Substring(dotpos + 1);
            }
            SVRate.Text = cls.Rows[0]["svrate"].ToString();
            MUnit.Text = cls.Rows[0]["munit"].ToString();
            DeprMethod.Text = cls.Rows[0]["deprmethod"].ToString();
            LinkSubject.Text = cls.Rows[0]["linksubject"].ToString();
            DeprSubject.Text = cls.Rows[0]["deprsubject"].ToString();
            EditState.Text = "正在编辑类别【" + CName.Text + "】";
            Button1.Enabled = true;
            Button3.Enabled = true;
        }
    }
    protected void UpdateAssetClass_Click(object sender, EventArgs e)
    {
        DataRow newRow;
        string MonthDeprSubject = SysConfigs.MonthDeprSubject;
        MonthDeprSubject = MonthDeprSubject + "." + CommClass.GetTableValue("cw_subject", "subjectname", "subjectno='" + MonthDeprSubject + "'");
        if (MonthDeprSubject.EndsWith("NoDataItem"))
        {
            MonthDeprSubject = "";
        }
        DataSet MyData = CommClass.GetDataSet("select * from cw_assetclass order by id");
        DataSet ParentData = CommClass.GetDataSet("select subjectno,subjectname,parentno from cw_subject where parentno like '" + FASubjectNo.Value + "%' order by subjectno");
        MyData.Tables[0].PrimaryKey = new DataColumn[] { MyData.Tables[0].Columns["id"] };
        ParentData.Tables[0].PrimaryKey = new DataColumn[] { ParentData.Tables[0].Columns["subjectno"] };
        for (int i = 0; i < MyData.Tables[0].Rows.Count; i++)
        {
            newRow = ParentData.Tables[0].Rows.Find(MyData.Tables[0].Rows[i]["id"].ToString());
            if (newRow == null)
            {
                MyData.Tables[0].Rows[i].Delete();
            }
        }
        for (int i = 0; i < ParentData.Tables[0].Rows.Count; i++)
        {
            newRow = MyData.Tables[0].Rows.Find(ParentData.Tables[0].Rows[i]["subjectno"].ToString());
            if (newRow == null)
            {
                newRow = MyData.Tables[0].NewRow();
                newRow["id"] = ParentData.Tables[0].Rows[i]["subjectno"];
                newRow["pid"] = ParentData.Tables[0].Rows[i]["parentno"];
                newRow["cname"] = ParentData.Tables[0].Rows[i]["subjectname"];
                newRow["uselife"] = "0.0";
                newRow["svrate"] = "0";
                newRow["deprmethod"] = "1";
                newRow["linksubject"] = ParentData.Tables[0].Rows[i]["subjectno"] + "." + ParentData.Tables[0].Rows[i]["subjectname"];
                newRow["deprsubject"] = MonthDeprSubject;
                MyData.Tables[0].Rows.Add(newRow);
            }
            else
            {
                newRow["id"] = ParentData.Tables[0].Rows[i]["subjectno"];
                newRow["pid"] = ParentData.Tables[0].Rows[i]["parentno"];
                newRow["cname"] = ParentData.Tables[0].Rows[i]["subjectname"];
                newRow["deprsubject"] = MonthDeprSubject;
            }
        }
        CommClass.UpdateDataSet(MyData);
        ParentName.Text = "";
        ClassIDP.Text = "";
        ClassID.Text = "[自动编号]";
        CName.Text = "";
        UseLife0.Text = "";
        UseLife1.SelectedIndex = 0;
        SVRate.Text = "";
        MUnit.Text = "";
        DeprMethod.SelectedIndex = 1;
        LinkSubject.Text = "";
        DeprSubject.Text = "";
        CEditState.Value = "0";
        EditState.Text = "";
        TreeView1.Nodes[0].ChildNodes.Clear();
        InitSubject(TreeView1.Nodes[0], FASubjectNo.Value);
        Button1.Enabled = false;
        Button2.Enabled = false;
        Button3.Enabled = false;
    }
}
