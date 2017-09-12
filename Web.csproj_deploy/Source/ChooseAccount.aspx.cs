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
using System.IO;

public partial class _ChooseAccount : System.Web.UI.Page
{
    private int nodeDepth = 0;
    StringBuilder AllNodeText = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            Overlay.Attributes["style"] = "display:none";
            Lightbox.Attributes["style"] = "display:none";
            AccountID.Attributes["readonly"] = "readonly";
            AccountName.Attributes["readonly"] = "readonly";
            Button1.Attributes.Add("onclick", "if($('AccountID').value.length==0){alert('请选择查询账套！');return false;}");
            Button2.Attributes.Add("onclick", "return ShowConfirm('您确定需要导出账套['+$('AccountName').value+']的审计数据吗？','导出审计数据')");
            if (UserInfo.AccountID != null)
            {
                AccountID.Text = UserInfo.AccountID;
                AccountName.Text = UserInfo.AccountName;
            }
            SMinus.Attributes["onclick"] = "return setYear('SelYear',-1);";
            SPlus.Attributes["onclick"] = "return setYear('SelYear',1);";
            SelYear.Attributes["readonly"] = "readonly";
            SelYear.Text = DateTime.Now.Year.ToString();
            if (Session["UserType"].ToString() == "4")
            {
                Button2.Enabled = true;
            }
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
            InitAccountList(TreeView1.Nodes[0], UnitID);
            TreeView1.Nodes[0].Text = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + UnitID + "']", "UnitName");
            TreeView1.Nodes[0].Value = UnitID;
        }
    }

    protected void InitAccountList(TreeNode _TreeNode, string ParentID)
    {
        bool HasChild = false;
        if (_TreeNode.Depth == nodeDepth && _TreeNode.ChildNodes.Count == 0)
        {
            DataTable dt = MainClass.GetDataTable("select id,accountname,accountdate from cw_account where unitid='" + ParentID + "' order by levelid,id");
            if (dt.Rows.Count > 0)
            {
                HasChild = true;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TreeNode tn = new TreeNode(dt.Rows[i]["accountname"].ToString(), dt.Rows[i]["id"].ToString());
                tn.SelectAction = TreeNodeSelectAction.None;
                string ClickStr;
                if (Request.QueryString["t"] == "1" || dt.Rows[i]["accountdate"].ToString().Length > 0)
                {
                    ClickStr = "setAccount('" + tn.Value + "','" + tn.Text + "')";
                }
                else
                {
                    ClickStr = "alert('该账套尚未启用！');";
                }
                tn.Text = "<a href=\"###\" onclick=\"" + ClickStr + "\">账套：" + tn.Text + "</a>";
                _TreeNode.ChildNodes.Add(tn);
            }
        }
        DataRow[] rows = ValidateClass.GetRegRows("CUnits", "parentid='" + ParentID + "'");
        if (rows != null || HasChild)
        {
            _TreeNode.SelectAction = TreeNodeSelectAction.Expand;
        }
        else
        {
            _TreeNode.SelectAction = TreeNodeSelectAction.None;
            return;
        }
        for (int i = 0; i < rows.Length; i++)
        {
            _TreeNode.ChildNodes.Add(new TreeNode(rows[i]["unitname"].ToString(), rows[i]["id"].ToString()));
            InitAccountList(_TreeNode.ChildNodes[_TreeNode.ChildNodes.Count - 1], rows[i]["id"].ToString());
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        UserInfo.AccountID = AccountID.Text;
        PageClass.ShowAlertMsg(this.Page, "查询账套设置成功！");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Session.Remove("AccountName");
        UserInfo.AccountID = AccountID.Text;
        string AccountYears = MainClass.GetTableValue("cw_account", "AccountYear", "id='" + AccountID.Text + "'");
        if (AccountYears.IndexOf(SelYear.Text) == -1)
        {
            PageClass.ShowAlertMsg(this.Page, "本年无数据，导出失败！");
        }
        else
        {
            string AODataName = string.Concat("AOData_", AccountID.Text, "_", SelYear.Text);
            string AODataPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "BackupDB\\", AODataName, ".mdb");
            if (!File.Exists(AODataPath))
            {
                File.Copy(SysConfigs.GetAppDataFilePath("UFTemplate.mdb"), AODataPath, true);
                using (DataProvider AccDbProvider = new DataProvider(CommClass.ConnString))
                {
                    OutputAOData.OutputData(SelYear.Text, AODataPath, AccDbProvider);
                }
            }
            PageClass.DoZipSingleFile(AODataPath, string.Concat(AppDomain.CurrentDomain.BaseDirectory, "BackupDB\\", AODataName, ".zip"));
            File.Delete(AODataPath);
            PageClass.ExcuteScript(this.Page, string.Concat("ShowZipInfo('", AODataName, "');"));
        }
    }
}
